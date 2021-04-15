Imports System.Text

Namespace WebControls

    ''' <summary>
    ''' Represents an implementation of <see cref="CurrencyRateFactoryBase">CurrencyRateFactoryBase</see>
    ''' for Lithuanian Bank web service used to fetch official curreny rates with respect to EUR.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class CurrencyRateFactoryLbEur
        Inherits CurrencyRateFactoryBase

        Public Const BASE_CURRENCY As String = "EUR"


        ''' <summary>
        ''' Gets a base currency (ISO 4217 code) of the factory, i.e. currency with respect to which 
        ''' the rates are fetched. Returns EUR for this implementation.
        ''' </summary>
        ''' <remarks></remarks>
        Public Overrides ReadOnly Property BaseCurrency() As String
            Get
                Return BASE_CURRENCY
            End Get
        End Property


        ''' <summary>
        ''' Gets a currency rate for a particular currency for a particular date.
        ''' </summary>
        ''' <param name="currencyCode">a currency (ISO 4217 code) to get a rate for</param>
        ''' <param name="asOfDate">a date to get a currency rate at</param>
        ''' <remarks></remarks>
        Public Overrides Function GetCurrencyRate(ByVal currencyCode As String, ByVal asOfDate As Date) As CurrencyRate

            Dim param As New CurrencyRate.CurrencyRateParam(asOfDate, currencyCode)

            If currencyCode Is Nothing OrElse String.IsNullOrEmpty(currencyCode.Trim) _
                OrElse currencyCode.Trim.ToUpper() = BASE_CURRENCY.Trim.ToUpper() Then
                Return New CurrencyRate(param, 1.0)
            End If

            Dim paramList As New List(Of CurrencyRate.CurrencyRateParam)
            paramList.Add(param)

            Dim urlForDate As New Uri(GetDownloadUrl(asOfDate))

            Dim dataStream As Byte() = Nothing

            Using client As New System.Net.WebClient
                dataStream = client.DownloadData(urlForDate)
            End Using

            Dim result As New List(Of CurrencyRate)

            result.AddRange(RawDataToCurrencyRateList(dataStream, asOfDate, paramList))

            If result.Count > 0 Then
                Return result(0)
            Else
                Return New CurrencyRate(param, 0.0)
            End If

        End Function

        ''' <summary>
        ''' Gets a list of currency rates for particular currencies for particular dates.
        ''' </summary>
        ''' <param name="params">currencies (ISO 4217 codes) and dates to get the rate data for</param>
        ''' <remarks></remarks>
        Public Overrides Function GetCurrencyRateList(ByVal params() As CurrencyRate.CurrencyRateParam) As List(Of CurrencyRate)

            If params Is Nothing OrElse params.Length < 1 Then
                Return New List(Of CurrencyRate)()
            End If

            Dim paramList As List(Of CurrencyRate.CurrencyRateParam) = CurrencyRate. _
                GetCleanParams(params, BASE_CURRENCY)

            If paramList.Count < 1 Then Return New List(Of CurrencyRate)()

            Dim result As New List(Of CurrencyRate)

            Using client As New System.Net.WebClient

                For Each asOfDate As Date In CurrencyRate.GetDistinctDates(paramList)

                    Dim urlForDate As New Uri(GetDownloadUrl(asOfDate))

                    Dim dataStream As Byte() = client.DownloadData(urlForDate)

                    result.AddRange(RawDataToCurrencyRateList(dataStream, asOfDate, paramList))

                Next

            End Using

            Return result

        End Function


        ''' <summary>
        ''' Gets an URL to download currency rates for a particular date.
        ''' </summary>
        ''' <param name="asOfDate">a date to download the currency rates for</param>
        ''' <remarks></remarks>
        Public Overrides Function GetDownloadUrl(ByVal asOfDate As Date) As String
            Return String.Format("http://www.lb.lt/fxrates_csv.lb?tp=LT&rs=&dte={0}&ln=lt", _
                asOfDate.ToString("yyyy-MM-dd"))
        End Function

        ''' <summary>
        ''' Converts raw data downloaded from the <see cref="GetDownloadUrl">download url</see> 
        ''' to a list of currency rates.
        ''' </summary>
        ''' <param name="rawData">raw downloaded data (from <see cref="GetDownloadUrl">download url</see>)</param>
        ''' <param name="asOfDate">a date that the currency rates were downloaded for</param>
        ''' <param name="params">currencies and dates to get the rate data for</param>
        ''' <remarks></remarks>
        Public Overrides Function RawDataToCurrencyRateList(ByVal rawData() As Byte, _
            ByVal asOfDate As Date, ByVal params As List(Of CurrencyRate.CurrencyRateParam)) As List(Of CurrencyRate)

            Dim webResponseString As String = Encoding.UTF8.GetString(rawData)

            Dim result As New List(Of CurrencyRate)

            Dim currentLine As String()
            Dim currentRate As Double
            Dim currentCurrencyCode As String
            Dim isFound As Boolean

            For Each param As CurrencyRate.CurrencyRateParam In params

                If param.Date.Date = asOfDate.Date Then

                    isFound = False

                    For Each line As String In webResponseString.Split(New String() {vbCrLf}, _
                        StringSplitOptions.RemoveEmptyEntries)

                        currentLine = line.Split(New Char() {","c}, StringSplitOptions.None)
                        currentCurrencyCode = currentLine(1).Trim.ToUpper

                        If currentCurrencyCode = param.CurrencyCode Then

                            currentRate = Double.Parse(currentLine(2).Trim, Globalization.NumberStyles.Any, _
                                System.Globalization.CultureInfo.InvariantCulture)
                            If currentRate > 0 Then
                                currentRate = Math.Round(1 / currentRate, 6)
                            End If

                            result.Add(New CurrencyRate(param, currentRate))

                            isFound = True

                            Exit For

                        End If

                    Next

                    If Not isFound Then
                        result.Add(New CurrencyRate(param, 0.0))
                    End If

                End If

            Next

            Return result

        End Function

    End Class

End Namespace
