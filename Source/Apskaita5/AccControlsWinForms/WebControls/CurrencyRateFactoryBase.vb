Namespace WebControls

    ''' <summary>
    ''' Represents a base class for currency rate factories for a particular web services.
    ''' </summary>
    ''' <remarks></remarks>
    Public MustInherit Class CurrencyRateFactoryBase

        ''' <summary>
        ''' Gets a base currency (ISO 4217 code) of the factory, i.e. currency with respect to which the rates are fetched.
        ''' </summary>
        ''' <remarks></remarks>
        Public MustOverride ReadOnly Property BaseCurrency() As String

        ''' <summary>
        ''' Gets a currency rate for a particular currency for a particular date.
        ''' </summary>
        ''' <param name="currencyCode">a currency (ISO 4217 code) to get a rate for</param>
        ''' <param name="asOfDate">a date to get a currency rate at</param>
        ''' <remarks></remarks>
        Public MustOverride Function GetCurrencyRate(ByVal currencyCode As String, ByVal asOfDate As Date) As CurrencyRate

        ''' <summary>
        ''' Gets a list of currency rates for particular currencies for particular dates.
        ''' </summary>
        ''' <param name="params">currencies (ISO 4217 codes) and dates to get the rate data for</param>
        ''' <remarks></remarks>
        Public MustOverride Function GetCurrencyRateList(ByVal params() As CurrencyRate.CurrencyRateParam) As List(Of CurrencyRate)

        ''' <summary>
        ''' Gets an URL to download currency rates for a particular date.
        ''' </summary>
        ''' <param name="asOfDate">a date to download the currency rates for</param>
        ''' <remarks></remarks>
        Public MustOverride Function GetDownloadUrl(ByVal asOfDate As Date) As String

        ''' <summary>
        ''' Converts raw data downloaded from the <see cref="GetDownloadUrl">download url</see> 
        ''' to a list of currency rates.
        ''' </summary>
        ''' <param name="rawData">raw downloaded data (from <see cref="GetDownloadUrl">download url</see>)</param>
        ''' <param name="asOfDate">a date that the currency rates were downloaded for</param>
        ''' <param name="params">currencies and dates to get the rate data for</param>
        ''' <remarks></remarks>
        Public MustOverride Function RawDataToCurrencyRateList(ByVal rawData As Byte(), ByVal asOfDate As Date, _
            ByVal params As List(Of CurrencyRate.CurrencyRateParam)) As List(Of CurrencyRate)

    End Class

End Namespace