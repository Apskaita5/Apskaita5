Imports System.Windows.Forms

Namespace WebControls

    Public Module CommonMethods

        Private _CurrencyRateFactoryDictionary As Dictionary(Of String, CurrencyRateFactoryBase) = Nothing


        ''' <summary>
        ''' Gets an instance of <see cref="CurrencyRateFactoryBase">CurrencyRateFactoryBase</see>
        ''' implementation for a particular base currency that could be used to fetch currency 
        ''' rates from web services.
        ''' </summary>
        ''' <param name="baseCurrency">a base currency (ISO 4217 code) to get the rate factory for</param>
        ''' <remarks></remarks>
        Public Function GetCurrencyRateFactory(ByVal baseCurrency As String) As CurrencyRateFactoryBase

            If baseCurrency Is Nothing OrElse String.IsNullOrEmpty(baseCurrency.Trim) Then
                Throw New ArgumentNullException("baseCurrency")
            End If

            InitCurrencyRateFactories()

            If Not _CurrencyRateFactoryDictionary.ContainsKey(baseCurrency.Trim.ToUpper) Then
                Throw New NotImplementedException(String.Format("Currency {0} is not implemented in WebControls.", _
                    baseCurrency.Trim.ToUpper))
            End If

            Return _CurrencyRateFactoryDictionary(baseCurrency.Trim.ToUpper())

        End Function

        ''' <summary>
        ''' Gets a currency rate for a particular currency for a particular date
        ''' from a web service using a winforms progress form.
        ''' </summary>
        ''' <param name="currencyCode">a currency (ISO 4217 code) to get a rate for</param>
        ''' <param name="asOfDate">a date to get a currency rate at</param>
        ''' <param name="factory">a web service to use</param>
        ''' <remarks></remarks>
        Public Function GetCurrencyRateWithProgress(ByVal currencyCode As String, _
            ByVal asOfDate As Date, ByVal factory As CurrencyRateFactoryBase) As CurrencyRate

            If factory Is Nothing Then
                Throw New ArgumentNullException("factory")
            End If

            Dim param As New CurrencyRate.CurrencyRateParam(asOfDate, currencyCode)

            If currencyCode Is Nothing OrElse String.IsNullOrEmpty(currencyCode.Trim) _
                OrElse currencyCode.Trim.ToUpper() = factory.BaseCurrency.Trim.ToUpper() Then
                Return New CurrencyRate(param, 1.0)
            End If

            Dim paramList As New List(Of CurrencyRate.CurrencyRateParam)
            paramList.Add(param)

            Using frm As New DownloadCurrencyRatesForm(factory, paramList)
                Dim result As DialogResult = frm.ShowDialog()
                If result = DialogResult.Cancel Then
                    Return Nothing
                ElseIf frm.DownloadException IsNot Nothing Then
                    ShowError(frm.DownloadException, Nothing)
                    Return Nothing
                ElseIf frm.Result.Count < 1 Then
                    ShowError(New Exception("Web request returned null."), Nothing)
                    Return Nothing
                Else
                    Return frm.Result(0)
                End If
            End Using

        End Function

        ''' <summary>
        ''' Gets a list of currency rates for particular currencies for particular dates
        ''' from a web service using a winforms progress form.
        ''' </summary>
        ''' <param name="params">currencies (ISO 4217 codes) and dates to get the rate data for</param>
        ''' <param name="factory">a web service to use</param>
        ''' <remarks></remarks>
        Public Function GetCurrencyRateListWithProgress(ByVal params() As CurrencyRate.CurrencyRateParam, _
            ByVal factory As CurrencyRateFactoryBase) As List(Of CurrencyRate)

            If factory Is Nothing Then
                Throw New ArgumentNullException("factory")
            ElseIf params Is Nothing OrElse params.Length < 1 Then
                Throw New ArgumentException("Klaida. Nenurodyta nė viena valiuta.", "params")
            End If

            Dim paramList As List(Of CurrencyRate.CurrencyRateParam) = CurrencyRate. _
                GetCleanParams(params, factory.BaseCurrency)

            If paramList.Count < 1 Then
                Throw New ArgumentException("Klaida. Nenurodyta nė viena valiuta.", "params")
            End If

            Using frm As New DownloadCurrencyRatesForm(factory, paramList)
                Dim result As DialogResult = frm.ShowDialog()
                If result = DialogResult.Cancel Then
                    Return Nothing
                ElseIf frm.DownloadException IsNot Nothing Then
                    ShowError(frm.DownloadException, Nothing)
                    Return Nothing
                Else
                    Return frm.Result
                End If
            End Using

        End Function

        ''' <summary>
        ''' Adds a custom currency rate factory to the factory cache. A default factory for a particular
        ''' currency (if exists) gets replaced by the new factory.
        ''' </summary>
        ''' <param name="factory">a custom currency rate factory to add, i.e. a custom class
        ''' that inherits <see cref="CurrencyRateFactoryBase">CurrencyRateFactoryBase</see></param>
        Public Sub AddCurrencyRateFactory(ByVal factory As CurrencyRateFactoryBase)

            If factory Is Nothing Then
                Throw New ArgumentNullException("factory")
            End If

            If _CurrencyRateFactoryDictionary Is Nothing Then
                InitCurrencyRateFactories()
            End If

            If _CurrencyRateFactoryDictionary.ContainsKey(factory.BaseCurrency.Trim.ToUpper) Then
                _CurrencyRateFactoryDictionary.Remove(factory.BaseCurrency.Trim.ToUpper)
            End If

            _CurrencyRateFactoryDictionary.Add(factory.BaseCurrency.Trim.ToUpper, factory)

        End Sub

        Private Sub InitCurrencyRateFactories()

            If Not _CurrencyRateFactoryDictionary Is Nothing Then Exit Sub

            _CurrencyRateFactoryDictionary = New Dictionary(Of String, CurrencyRateFactoryBase)

            AddCurrencyRateFactory(New CurrencyRateFactoryLbLtl)
            AddCurrencyRateFactory(New CurrencyRateFactoryLbEur)

        End Sub



    End Module

End Namespace
