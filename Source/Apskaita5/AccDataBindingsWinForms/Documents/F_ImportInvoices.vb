Imports System.IO
Imports AccControlsWinForms
Imports AccControlsWinForms.WebControls
Imports AccDataBindingsWinForms.CachedInfoLists
Imports ApskaitaObjects.Extensibility

Public Class F_ImportInvoices
    Implements ISingleInstanceForm

    Private ReadOnly _RequiredCachedLists As Type() = New Type() _
        {GetType(HelperLists.PersonInfoList), GetType(HelperLists.AccountInfoList),
         GetType(HelperLists.VatDeclarationSchemaInfoList)}

    Private _QueryManager As CslaActionExtenderQueryObject

    Private _Options As New InvoiceImportOptions
    Private _adaptersForInvoicesMade As New List(Of IInvoiceAdapter)
    Private _adaptersForInvoicesReceived As New List(Of IInvoiceAdapter)


    Private Sub F_ImportInvoices_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        If Not PrepareCache(Me, _RequiredCachedLists) Then Exit Sub

        Try
            Dim adapters As List(Of IInvoiceAdapter) = InvoiceImportPluginManager.GetAdapters()
            For Each adapter As IInvoiceAdapter In adapters
                If adapter.ForInvoicesMade Then
                    _adaptersForInvoicesMade.Add(adapter)
                Else
                    _adaptersForInvoicesReceived.Add(adapter)
                End If
            Next
        Catch ex As Exception
            ShowError(New Exception("Klaida. Nepavyko įkrauti failų importo adapterių (pluginų): " _
                + ex.Message, ex))
            DisableAllControls(Me)
            Exit Sub
        End Try

        Try
            SetupDefaultControls(Of InvoiceImportOptions)(Me, _
                                                          InvoiceImportOptionsBindingSource, _Options)
            AdapterComboBox.DataSource = _adaptersForInvoicesReceived

            If _adaptersForInvoicesReceived.Count > 0 Then _Options.Adapter = _adaptersForInvoicesReceived(0)

            InvoiceImportOptionsBindingSource.DataSource = _Options

            _QueryManager = New CslaActionExtenderQueryObject(Me, ProgressFiller1)

        Catch ex As Exception
            ShowError(ex, Nothing)
            DisableAllControls(Me)
        End Try

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        If Not _Options.IsValid Then
            MsgBox(_Options.GetAllBrokenRules(), _
                   MsgBoxStyle.Exclamation, "Klaida.")
            Exit Sub
        End If

        If _Options.HasWarnings() AndAlso Not YesOrNo(String.Format( _
            "Importo parametruose gali būti klaidų:{0}{1}{2}Ar tikrai norite tęsti?", _
            vbCrLf, _Options.GetAllWarnings(), vbCrLf)) Then
            Exit Sub
        End If

        Dim filePath As String = String.Empty

        Using ofd As New OpenFileDialog
            If Not StringIsNullOrEmpty(_Options.Adapter.FileExtension) Then _
            ofd.Filter = String.Format("Sąskaitų failai (*.{0})|*.{0}|Visi failai|*.*", _
                _Options.Adapter.FileExtension)
            ofd.Multiselect = False
            If ofd.ShowDialog() <> Windows.Forms.DialogResult.OK _
                OrElse StringIsNullOrEmpty(ofd.FileName) _
                OrElse Not IO.File.Exists(ofd.FileName) Then Exit Sub
            filePath = ofd.FileName
        End Using

        Dim result As List(Of InvoiceInfo.InvoiceInfo) = Nothing

        Try
            Using fs As FileStream = File.Open(filePath, FileMode.Open)
                result = _Options.ReadInvoiceData(fs)
            End Using
        Catch ex As Exception
            ShowError(ex)
            Exit Sub
        End Try

        If result Is Nothing OrElse result.Count < 1 Then
            MsgBox("Faile nėra nė vienos sąskaitos duomenų.", _
                   MsgBoxStyle.Exclamation, "Klaida.")
            Exit Sub
        End If

        Dim minDate As Date = Date.MaxValue
        Dim maxDate As Date = Date.MinValue
        Dim currencyRatesRequired As New List(Of CurrencyRate.CurrencyRateParam)
        Dim baseCurrency as String = GetCurrentCompany().BaseCurrency
        For Each invoice As InvoiceInfo.InvoiceInfo In result
            If invoice.Date.Date > maxDate.Date Then maxDate = invoice.Date.Date
            If invoice.Date.Date < minDate.Date Then minDate = invoice.Date.Date
            If Not StringIsNullOrEmpty(invoice.CurrencyCode) AndAlso invoice.CurrencyCode.Trim.ToUpperInvariant() _
                <> baseCurrency.Trim().ToUpperInvariant() Then
                currencyRatesRequired.Add(New CurrencyRate.CurrencyRateParam(invoice.Date, invoice.CurrencyCode))
            End If
        Next

        Try
            Dim currencyRates As List(Of CurrencyRate) = GetCurrencyRates(currencyRatesRequired)
            SetValuesForBaseCurrency(result, baseCurrency, currencyRates)
        Catch ex As Exception
            ShowError(New Exception("Nepavyko gauti reikalingų valiutų kursų: " + ex.Message, ex))
            Exit Sub
        End Try

        If Not YesOrNo(String.Format( _
            "Faile rasti {0} sąskaitų faktūrų duomenys laikotarpiu nuo {1:yyyy-MM-dd} iki {2:yyyy-MM-dd}.{3}Importuoti?", _
            result.Count, minDate, maxDate, vbCrLf)) Then
            Exit Sub
        End If

        Try
            ' _Options.ImportInvoices(result)
            _QueryManager.InvokeQuery(Of InvoiceImportOptions)(_Options, "ImportInvoices", _
                False, AddressOf OnImportCompleted, result)
        Catch ex As Exception
            ShowError(ex)
            Exit Sub
        End Try

    End Sub

    Private Function GetCurrencyRates(params As List(of CurrencyRate.CurrencyRateParam)) As List(of CurrencyRate)

        If params.Count < 1 Then Return new List(Of CurrencyRate)
           
        For i As Integer = params.Count - 1 To 0 Step -1
            For j As Integer = 0 To i-1
                If params(i).Date.Date = params(j).Date.Date AndAlso _
                   params(i).CurrencyCode.Trim.ToUpperInvariant() = params(j).CurrencyCode.Trim.ToUpperInvariant() Then
                   params.RemoveAt(i)
                    Exit For
                End If
            Next
        Next

        Dim factory As CurrencyRateFactoryBase = WebControls.GetCurrencyRateFactory(GetCurrentCompany.BaseCurrency)
        Return WebControls.GetCurrencyRateListWithProgress(params.ToArray(), factory)

    End Function

    Private Sub SetValuesForBaseCurrency(invoices As List(Of InvoiceInfo.InvoiceInfo), baseCurrency As String, _
                                         rates As List(Of CurrencyRate))
        For Each invoice As InvoiceInfo.InvoiceInfo In invoices
            
            If StringIsNullOrEmpty(invoice.CurrencyCode) OrElse invoice.CurrencyCode.Trim.ToUpperInvariant() _
                = baseCurrency.Trim().ToUpperInvariant() Then

                For Each invoiceItem As InvoiceInfo.InvoiceItemInfo In invoice.InvoiceItems
                    invoiceItem.DiscountLTL = invoiceItem.Discount
                    invoiceItem.DiscountVatLTL = invoiceItem.DiscountVat
                    invoiceItem.SumLTL = invoiceItem.Sum
                    invoiceItem.SumVatLTL = invoiceItem.SumVat
                    invoiceItem.UnitValueLTL = invoiceItem.UnitValue
                    invoiceItem.SumTotalLTL = invoiceItem.SumTotal 
                Next
                invoice.CurrencyRate = 1.0
                invoice.CalculateSubtotalsLTL()
                
            Else   

                Dim currency As CurrencyRate = Nothing
                For Each rate As CurrencyRate In rates
                    If rate.Date.Date = invoice.Date.Date AndAlso invoice.CurrencyCode.Trim.ToUpperInvariant() _
                     = rate.CurrencyCode.Trim().ToUpperInvariant()

                        currency = rate
                        Exit For

                    End If
                Next

                If currency Is Nothing Then Throw New Exception(String.Format( _
                    "Failed to identify rate for currency {0} on {1:yyyyMMdd}", invoice.CurrencyCode, invoice.Date))

                For Each invoiceItem As InvoiceInfo.InvoiceItemInfo In invoice.InvoiceItems
                    invoiceItem.DiscountLTL = invoiceItem.Discount * currency.Rate
                    invoiceItem.DiscountVatLTL = invoiceItem.DiscountVat * currency.Rate
                    invoiceItem.SumLTL = invoiceItem.Sum * currency.Rate
                    invoiceItem.SumVatLTL = invoiceItem.SumVat * currency.Rate
                    invoiceItem.UnitValueLTL = invoiceItem.UnitValue * currency.Rate
                    invoiceItem.SumTotalLTL = invoiceItem.SumTotal * currency.Rate
                Next
                invoice.CurrencyRate = currency.Rate
                invoice.CalculateSubtotalsLTL()

            End If

        Next
    End Sub

    Private Sub OnImportCompleted(ByVal result As Object, ByVal exceptionHandled As Boolean)

        If result Is Nothing Then Exit Sub

        MsgBox(DirectCast(result, String), MsgBoxStyle.Information, "Info")

    End Sub

    Private Sub ForInvoicesMadeCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles ForInvoicesMadeCheckBox.CheckedChanged

        _Options.ForInvoicesMade = ForInvoicesMadeCheckBox.Checked
        AdapterComboBox.SelectedIndex = -1
        AdapterComboBox.SelectedText = string.Empty

        If ForInvoicesMadeCheckBox.Checked Then
            AdapterComboBox.DataSource = _adaptersForInvoicesMade
            If _adaptersForInvoicesMade.Count > 0 Then
                _Options.Adapter = _adaptersForInvoicesMade(0)
            End If
        Else
            AdapterComboBox.DataSource = _adaptersForInvoicesReceived
            If _adaptersForInvoicesReceived.Count > 0 Then
                _Options.Adapter = _adaptersForInvoicesReceived(0)
            End If
        End If

    End Sub

End Class
