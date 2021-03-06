﻿Imports ApskaitaObjects.Documents
Imports AccControlsWinForms
Imports AccDataBindingsWinForms.Printing
Imports AccDataBindingsWinForms.CachedInfoLists
Imports AccControlsWinForms.WebControls

Friend Class F_AdvanceReport
    Implements ISupportsPrinting, IObjectEditForm, ISupportsChronologicValidator

    Private ReadOnly _RequiredCachedLists As Type() = New Type() _
        {GetType(HelperLists.PersonInfoList), GetType(HelperLists.AccountInfoList), _
        GetType(HelperLists.TaxRateInfoList), GetType(HelperLists.VatDeclarationSchemaInfoList)}

    Private WithEvents _FormManager As CslaActionExtenderEditForm(Of AdvanceReport)
    Private _ListViewManager As DataListViewEditControlManager(Of AdvanceReportItem)
    Private _QueryManager As CslaActionExtenderQueryObject

    Private _DocumentToEdit As AdvanceReport = Nothing


    Public ReadOnly Property ObjectID() As Integer Implements IObjectEditForm.ObjectID
        Get
            If _FormManager Is Nothing OrElse _FormManager.DataSource Is Nothing Then
                If _DocumentToEdit Is Nothing OrElse _DocumentToEdit.IsNew Then
                    Return Integer.MinValue
                Else
                    Return _DocumentToEdit.ID
                End If
            End If
            Return _FormManager.DataSource.ID
        End Get
    End Property

    Public ReadOnly Property ObjectType() As System.Type Implements IObjectEditForm.ObjectType
        Get
            Return GetType(AdvanceReport)
        End Get
    End Property


    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public Sub New(ByVal documentToEdit As AdvanceReport)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        _DocumentToEdit = documentToEdit

    End Sub


    Private Sub F_AdvanceReport_Load(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load

        If _DocumentToEdit Is Nothing Then
            _DocumentToEdit = AdvanceReport.NewAdvanceReport()
        End If

        If Not SetDataSources() Then Exit Sub

        Try

            _FormManager = New CslaActionExtenderEditForm(Of AdvanceReport) _
                (Me, AdvanceReportBindingSource, _DocumentToEdit, _
                _RequiredCachedLists, IOkButton, IApplyButton, ICancelButton, _
                Nothing, ProgressFiller1)

            _FormManager.ManageDataListViewStates(ReportItemsDataListView)

        Catch ex As Exception
            ShowError(ex, Nothing)
            DisableAllControls(Me)
            Exit Sub
        End Try

        ConfigureButtons()

        If Not _FormManager.DataSource.IsNew AndAlso Not StringIsNullOrEmpty(_FormManager.DataSource.FetchWarnings) Then
            MsgBox(_FormManager.DataSource.FetchWarnings, MsgBoxStyle.Exclamation, "Perspėjimas")
        End If

    End Sub

    Private Function SetDataSources() As Boolean

        If Not PrepareCache(Me, _RequiredCachedLists) Then Return False

        Try

            _ListViewManager = New DataListViewEditControlManager(Of AdvanceReportItem) _
                (ReportItemsDataListView, Nothing, AddressOf OnItemsDelete, _
                 AddressOf OnItemAdd, Nothing, _DocumentToEdit)

            _ListViewManager.AddCancelButton = False
            _ListViewManager.AddButtonHandler("Atidaryti Sąskaitą", _
                "Atidaryti susietą sąskaitą faktūrą", AddressOf OnItemClicked)

            _QueryManager = New CslaActionExtenderQueryObject(Me, ProgressFiller2)

            SetupDefaultControls(Of AdvanceReport)(Me, _
                AdvanceReportBindingSource, _DocumentToEdit)

        Catch ex As Exception
            ShowError(ex, Nothing)
            DisableAllControls(Me)
            Return False
        End Try

        Return True

    End Function


    Private Sub OnItemsDelete(ByVal items As AdvanceReportItem())
        If items Is Nothing OrElse items.Length < 1 OrElse _FormManager.DataSource Is Nothing Then Exit Sub
        If Not _FormManager.DataSource.ChronologicValidator.FinancialDataCanChange Then
            MsgBox(String.Format("Klaida. Eilučių pašalinti neleidžiama:{0}{1}", vbCrLf, _
                _FormManager.DataSource.ChronologicValidator.FinancialDataCanChangeExplanation), _
                MsgBoxStyle.Exclamation, "Klaida")
            Exit Sub
        End If
        For Each item As AdvanceReportItem In items
            _FormManager.DataSource.ReportItems.Remove(item)
        Next
    End Sub

    Private Sub OnItemAdd()
        If _FormManager.DataSource Is Nothing Then Exit Sub
        If Not _FormManager.DataSource.ChronologicValidator.FinancialDataCanChange Then
            MsgBox(String.Format("Klaida. Keisti dokumento finansinių duomenų negalima, įskaitant eilučių pridėjimą ar ištrynimą:{0}{1}", _
                vbCrLf, _FormManager.DataSource.ChronologicValidator.FinancialDataCanChangeExplanation), _
                MsgBoxStyle.Exclamation, "Klaida")
            Exit Sub
        End If
        _FormManager.DataSource.ReportItems.AddNew()
    End Sub

    Private Sub OnItemClicked(ByVal item As AdvanceReportItem)

        If item Is Nothing OrElse Not item.InvoiceID > 0 Then Exit Sub

        If item.InvoiceIsMade Then
            ' InvoiceMade.GetInvoiceMade(item.InvoiceID)
            _QueryManager.InvokeQuery(Of InvoiceMade)(Nothing, "GetInvoiceMade", _
                True, AddressOf OpenObjectEditForm, item.InvoiceID)
        Else
            ' InvoiceReceived.GetInvoiceReceived(item.InvoiceID)
            _QueryManager.InvokeQuery(Of InvoiceReceived)(Nothing, "GetInvoiceReceived", _
                True, AddressOf OpenObjectEditForm, item.InvoiceID)
        End If

    End Sub

    Private Sub AddAdvanceReportItemButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles AddAdvanceReportItemButton.Click

        If _FormManager.DataSource Is Nothing Then Exit Sub

        Using dlg As New F_InvoiceInfoList(_FormManager.DataSource.Date.AddMonths(-1), _
            _FormManager.DataSource.Date, False)

            If dlg.ShowDialog() <> DialogResult.OK OrElse dlg.SelectedInvoices Is Nothing _
                OrElse dlg.SelectedInvoices.Count < 1 Then Exit Sub

            Try
                _FormManager.DataSource.AddAdvanceReportItemWithInvoices(dlg.SelectedInvoices)
            Catch ex As Exception
                ShowError(ex, New Object() {_FormManager.DataSource, dlg.SelectedInvoices})
                Exit Sub
            End Try

        End Using

    End Sub

    Private Sub GetCurrencyRatesButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles GetCurrencyRatesButton.Click

        If _FormManager.DataSource Is Nothing OrElse IsBaseCurrency(_FormManager.DataSource.CurrencyCode, _
            GetCurrentCompany.BaseCurrency) Then Exit Sub

        If Not YesOrNo("Gauti valiutos kursą?") Then Exit Sub

        Dim factory As CurrencyRateFactoryBase = Nothing
        Dim result As CurrencyRate = Nothing
        Try
            factory = WebControls.GetCurrencyRateFactory(GetCurrentCompany.BaseCurrency)
            result = WebControls.GetCurrencyRateWithProgress(_FormManager.DataSource.CurrencyCode.Trim.ToUpper, _
                _FormManager.DataSource.Date, factory)
        Catch ex As Exception
            ShowError(ex, _FormManager.DataSource)
            Exit Sub
        End Try

        If Not result Is Nothing Then
            _FormManager.DataSource.CurrencyRate = result.Rate
        End If

    End Sub

    Private Sub ViewJournalEntryButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles ViewJournalEntryButton.Click
        If _FormManager.DataSource Is Nothing OrElse Not _FormManager.DataSource.ID > 0 Then Exit Sub
        OpenJournalEntryEditForm(_QueryManager, _FormManager.DataSource.ID)
    End Sub


    Public Function GetMailDropDownItems() As System.Windows.Forms.ToolStripDropDown _
       Implements ISupportsPrinting.GetMailDropDownItems
        Return Nothing
    End Function

    Public Function GetPrintDropDownItems() As System.Windows.Forms.ToolStripDropDown _
        Implements ISupportsPrinting.GetPrintDropDownItems
        Return Nothing
    End Function

    Public Function GetPrintPreviewDropDownItems() As System.Windows.Forms.ToolStripDropDown _
        Implements ISupportsPrinting.GetPrintPreviewDropDownItems
        Return Nothing
    End Function

    Public Sub OnMailClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Implements ISupportsPrinting.OnMailClick
        If _FormManager.DataSource Is Nothing Then Exit Sub

        Using frm As New F_SendObjToEmail(_FormManager.DataSource, 0)
            frm.ShowDialog()
        End Using

    End Sub

    Public Sub OnPrintClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Implements ISupportsPrinting.OnPrintClick
        If _FormManager.DataSource Is Nothing Then Exit Sub
        Try
            PrintObject(_FormManager.DataSource, False, 0, "AvansoApyskaita", Me, "")
        Catch ex As Exception
            ShowError(ex, _FormManager.DataSource)
        End Try
    End Sub

    Public Sub OnPrintPreviewClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Implements ISupportsPrinting.OnPrintPreviewClick
        If _FormManager.DataSource Is Nothing Then Exit Sub
        Try
            PrintObject(_FormManager.DataSource, True, 0, "AvansoApyskaita", Me, "")
        Catch ex As Exception
            ShowError(ex, _FormManager.DataSource)
        End Try
    End Sub

    Public Function SupportsEmailing() As Boolean _
        Implements ISupportsPrinting.SupportsEmailing
        Return True
    End Function


    Public Function ChronologicContent() As String _
        Implements ISupportsChronologicValidator.ChronologicContent
        If _FormManager.DataSource Is Nothing Then Return ""
        Return _FormManager.DataSource.ChronologicValidator.LimitsExplanation
    End Function

    Public Function HasChronologicContent() As Boolean _
        Implements ISupportsChronologicValidator.HasChronologicContent

        Return Not _FormManager.DataSource Is Nothing AndAlso _
            Not StringIsNullOrEmpty(_FormManager.DataSource.ChronologicValidator.LimitsExplanation)

    End Function


    Private Sub _FormManager_DataSourceStateHasChanged(ByVal sender As Object, _
        ByVal e As System.EventArgs) Handles _FormManager.DataSourceStateHasChanged
        ConfigureButtons()
    End Sub

    Private Sub ConfigureButtons()

        If _FormManager.DataSource Is Nothing Then Exit Sub

        AccountAccGridComboBox.Enabled = _FormManager.DataSource.ChronologicValidator.FinancialDataCanChange
        CurrencyCodeComboBox.Enabled = _FormManager.DataSource.ChronologicValidator.FinancialDataCanChange
        CurrencyRateAccTextBox.ReadOnly = Not _FormManager.DataSource.ChronologicValidator.FinancialDataCanChange
        GetCurrencyRatesButton.Enabled = _FormManager.DataSource.ChronologicValidator.FinancialDataCanChange
        'SetListViewColumnReadOnly()
        'DataGridViewCheckBoxColumn1.ReadOnly = Not _FormManager.DataSource.ChronologicValidator.FinancialDataCanChange
        'DataGridViewCheckBoxColumn2.ReadOnly = Not _FormManager.DataSource.ChronologicValidator.FinancialDataCanChange
        'DataGridViewTextBoxColumn6.ReadOnly = Not _FormManager.DataSource.ChronologicValidator.FinancialDataCanChange
        'DataGridViewTextBoxColumn7.ReadOnly = Not _FormManager.DataSource.ChronologicValidator.FinancialDataCanChange
        'DataGridViewTextBoxColumn8.ReadOnly = Not _FormManager.DataSource.ChronologicValidator.FinancialDataCanChange
        'DataGridViewTextBoxColumn9.ReadOnly = Not _FormManager.DataSource.ChronologicValidator.FinancialDataCanChange
        'DataGridViewTextBoxColumn11.ReadOnly = Not _FormManager.DataSource.ChronologicValidator.FinancialDataCanChange
        'DataGridViewTextBoxColumn14.ReadOnly = Not _FormManager.DataSource.ChronologicValidator.FinancialDataCanChange
        'DataGridViewTextBoxColumn16.ReadOnly = Not _FormManager.DataSource.ChronologicValidator.FinancialDataCanChange
        'CurrencyRateChangeEffect.ReadOnly = Not _FormManager.DataSource.ChronologicValidator.FinancialDataCanChange
        'AccountCurrencyRateChangeEffectDataGridViewColumn.ReadOnly = Not _FormManager.DataSource.ChronologicValidator.FinancialDataCanChange

        AddAdvanceReportItemButton.Enabled = _FormManager.DataSource.ChronologicValidator.FinancialDataCanChange

    End Sub

End Class