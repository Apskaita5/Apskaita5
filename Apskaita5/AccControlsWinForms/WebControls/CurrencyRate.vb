Namespace WebControls

    ''' <summary>
    ''' Represents an abstract currency rate data that is downloaded from a web service
    ''' using a <see cref="CurrencyRateFactoryBase">currency rate factory</see>.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class CurrencyRate

        ''' <summary>
        ''' a date that the currency rate was downloaded for
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly [Date] As Date

        ''' <summary>
        ''' a currency (ISO 4217 code) that the rate was downloaded for
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly CurrencyCode As String

        ''' <summary>
        ''' a downloaded currency rate value
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Rate As Double


        ''' <summary>
        ''' Creates a new CurrencyRate instance.
        ''' </summary>
        ''' <param name="param">a currency rate parameter that the rate was downloaded for</param>
        ''' <param name="rate">a downloaded currency rate</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal param As CurrencyRateParam, ByVal rate As Double)
            Me.Date = param.Date
            Me.CurrencyCode = param.CurrencyCode
            Me.Rate = rate
        End Sub


        ''' <summary>
        ''' Gets a rate for a particular currency from the downloaded currency rate list.
        ''' </summary>
        ''' <param name="source">a downloaded currency rate list</param>
        ''' <param name="asOfDate">a date to look the rate for</param>
        ''' <param name="currencyCode">a currency (ISO 4217 code) to look the rate for</param>
        ''' <param name="baseCurrency">a base currency (ISO 4217 code)</param>
        ''' <remarks></remarks>
        Public Shared Function GetRate(ByVal source As List(Of CurrencyRate), ByVal asOfDate As Date, _
            ByVal currencyCode As String, ByVal baseCurrency As String) As Double

            If currencyCode Is Nothing OrElse String.IsNullOrEmpty(currencyCode.Trim) _
               OrElse currencyCode.Trim.ToUpper() = baseCurrency.Trim.ToUpper() Then
                Return 1.0
            End If

            If source Is Nothing OrElse source.Count < 1 Then Return 0.0

            For Each item As CurrencyRate In source
                If item.Date.Date = asOfDate.Date AndAlso item.CurrencyCode.Trim.ToUpper() _
                                                          = currencyCode.Trim.ToUpper() Then
                    Return item.Rate
                End If
            Next

            Return 0.0

        End Function

        ''' <summary>
        ''' Gets a clean currency rate parameter list by removing duplicate entries and entries 
        ''' with the base currency.
        ''' </summary>
        ''' <param name="params">a parameters to clean</param>
        ''' <param name="baseCurrency">a base currency (ISO 4217 code)</param>
        ''' <remarks>only ment for use by concrete <see cref="CurrencyRateFactoryBase">CurrencyRateFactoryBase</see>
        ''' implementations</remarks>
        Public Shared Function GetCleanParams(ByVal params() As CurrencyRateParam, _
            ByVal baseCurrency As String) As List(Of CurrencyRateParam)

            Dim result As New List(Of CurrencyRateParam)

            If params Is Nothing OrElse params.Length < 1 Then Return result

            For Each param As CurrencyRateParam In params
                If Not param.IsBaseCurrency(baseCurrency) AndAlso Not ListAlreadyContainsParam(result, param) Then
                    result.Add(param)
                End If
            Next

            Return result

        End Function

        ''' <summary>
        ''' Gets a list of distict dates within a currency rate parameter list.
        ''' </summary>
        ''' <param name="list">a parameters to get the distinct dates for</param>
        ''' <remarks>only ment for use by concrete <see cref="CurrencyRateFactoryBase">CurrencyRateFactoryBase</see>
        ''' implementations</remarks>
        Public Shared Function GetDistinctDates(ByVal list As List(Of CurrencyRateParam)) As List(Of Date)

            Dim result As New List(Of Date)

            If list Is Nothing OrElse list.Count < 1 Then Return result

            For Each param As CurrencyRateParam In list
                If Not result.Contains(param.Date.Date) Then result.Add(param.Date.Date)
            Next

            Return result

        End Function


        Private Shared Function ListAlreadyContainsParam(ByVal list As List(Of CurrencyRateParam), _
            ByVal param As CurrencyRateParam) As Boolean
            For Each item As CurrencyRateParam In list
                If item = param Then Return True
            Next
            Return False
        End Function


        ''' <summary>
        ''' Represents a parameter used to download currency rate, i.e. currency code and the date
        ''' to download the rate data for.
        ''' </summary>
        ''' <remarks></remarks>
        Public Class CurrencyRateParam

            ''' <summary>
            ''' a date to download the rate for
            ''' </summary>
            ''' <remarks></remarks>
            Public ReadOnly [Date] As Date

            ''' <summary>
            ''' a currency (ISO 4217 code) to download the rate for
            ''' </summary>
            ''' <remarks></remarks>
            Public ReadOnly CurrencyCode As String


            ''' <summary>
            ''' Creates a new instance of CurrencyRateParam.
            ''' </summary>
            ''' <param name="asOfDate">a date to download the rate for</param>
            ''' <param name="currencyCode">a currency (ISO 4217 code) to download the rate for</param>
            ''' <remarks></remarks>
            Public Sub New(ByVal asOfDate As Date, ByVal currencyCode As String)
                Me.Date = asOfDate.Date
                If currencyCode Is Nothing Then
                    Me.CurrencyCode = ""
                Else
                    Me.CurrencyCode = currencyCode.Trim.ToUpper()
                End If
            End Sub


            Public Shared Operator =(ByVal item1 As CurrencyRateParam, ByVal item2 As CurrencyRateParam) As Boolean
                Return item1.CurrencyCode.Trim.ToUpper() = item2.CurrencyCode.Trim.ToUpper() _
                       AndAlso item1.Date.Date = item2.Date.Date
            End Operator

            Public Shared Operator <>(ByVal item1 As CurrencyRateParam, ByVal item2 As CurrencyRateParam) As Boolean
                Return Not item1 = item2
            End Operator


            Friend Function IsBaseCurrency(ByVal baseCurrency As String) As Boolean
                If baseCurrency Is Nothing Then baseCurrency = ""
                Return (CurrencyCode Is Nothing OrElse String.IsNullOrEmpty(CurrencyCode.Trim) _
                    OrElse CurrencyCode.Trim.ToUpper = baseCurrency.Trim.ToUpper())
            End Function

        End Class


    End Class

End Namespace