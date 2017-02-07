Imports System.Text
Imports System.Globalization

Namespace WebControls

    ''' <summary>
    ''' Represents an implementation of <see cref="CurrencyRateFactoryBase">CurrencyRateFactoryBase</see>
    ''' for Lithuanian Bank web service used to fetch official curreny rates with respect to LTL.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class CurrencyRateFactoryLbLtl
        Inherits CurrencyRateFactoryBase

        Public Const BASE_CURRENCY As String = "LTL"


        ''' <summary>
        ''' Gets a base currency (ISO 4217 code) of the factory, i.e. currency with respect to which 
        ''' the rates are fetched. Returns LTL for this implementation.
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

            For Each asOfDate As Date In CurrencyRate.GetDistinctDates(paramList)

                Dim urlForDate As New Uri(GetDownloadUrl(asOfDate))

                Dim dataStream As Byte() = Nothing

                Using client As New System.Net.WebClient
                    dataStream = client.DownloadData(urlForDate)
                End Using

                result.AddRange(RawDataToCurrencyRateList(dataStream, asOfDate, paramList))

            Next

            Return result

        End Function


        'Public Overrides Function GetCurrencyRate(ByVal currencyCode As String, ByVal asOfDate As Date) As CurrencyRate

        '    Dim param As New CurrencyRate.CurrencyRateParam(asOfDate, currencyCode)

        '    If currencyCode Is Nothing OrElse String.IsNullOrEmpty(currencyCode.Trim) _
        '        OrElse currencyCode.Trim.ToUpper() = BASE_CURRENCY.Trim.ToUpper() Then
        '        Return New CurrencyRate(param, 1.0)
        '    End If

        '    Dim paramList As New List(Of CurrencyRate.CurrencyRateParam)
        '    paramList.Add(param)

        '    Dim request As HttpWebRequest = GetRequestForCurrencies(asOfDate)

        '    Dim response As HttpWebResponse = request.GetResponse

        '    Dim result As New List(Of CurrencyRate)

        '    Using sr As New IO.StreamReader(response.GetResponseStream())
        '        Try
        '            result.AddRange(ParseResultForCurrencies(sr.ReadToEnd, paramList, asOfDate))
        '        Catch ex As Exception
        '            response.Close()
        '            sr.Close()
        '            Throw
        '        End Try
        '        sr.Close()
        '    End Using

        '    If result.Count > 0 Then
        '        Return result(0)
        '    Else
        '        Return New CurrencyRate(param, 0.0)
        '    End If

        'End Function

        'Public Overrides Function GetCurrencyRateList(ByVal params() As CurrencyRate.CurrencyRateParam) As List(Of CurrencyRate)

        '    If params Is Nothing OrElse params.Length < 1 Then
        '        Return New List(Of CurrencyRate)()
        '    End If

        '    Dim paramList As List(Of CurrencyRate.CurrencyRateParam) = CurrencyRate. _
        '        GetCleanParams(params, BASE_CURRENCY)

        '    If paramList.Count < 1 Then Return New List(Of CurrencyRate)()

        '    Dim result As New List(Of CurrencyRate)

        '    For Each asOfDate As Date In CurrencyRate.GetDistinctDates(paramList)

        '        Dim request As HttpWebRequest = GetRequestForCurrencies(asOfDate)

        '        Dim response As HttpWebResponse = request.GetResponse

        '        Using sr As New IO.StreamReader(response.GetResponseStream())
        '            Try
        '                result.AddRange(ParseResultForCurrencies(sr.ReadToEnd, paramList, asOfDate))
        '            Catch ex As Exception
        '                response.Close()
        '                sr.Close()
        '                Throw
        '            End Try
        '            sr.Close()
        '        End Using

        '    Next

        '    Return result

        'End Function


        'Private Function GetRequestForCurrencies(ByVal asOfDate As Date) As HttpWebRequest

        '    Dim request As HttpWebRequest = WebRequest.Create("http://www.lb.lt/exchange/Results.asp")

        '    request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; rv:11.0) Gecko/20100101 Firefox/15.0"
        '    request.Accept = "Accept: image/gif, image/jpeg, image/pjpeg, image/pjpeg, application/x-shockwave-flash, application/x-ms-application, application/x-ms-xbap, application/vnd.ms-xpsdocument, application/xaml+xml, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, */*"
        '    request.Headers.Add("Accept-Language: en-us")
        '    request.Headers.Add("Accept-Encoding: gzip, deflate")
        '    request.ContentType = "application/x-www-form-urlencoded"
        '    request.KeepAlive = True
        '    request.CookieContainer = New CookieContainer()
        '    request.Method = WebRequestMethods.Http.Post
        '    request.Referer = "http://www.lb.lt/exchange/Results.asp"
        '    Dim byteArray As Byte() = Encoding.ASCII.GetBytes( _
        '        String.Format("Lang=L&id=7713&ord=1&dir=ASC&Y={0}&M={1}&D={2}&DD=D&WW=W&vykdyti=Vykdyti", _
        '        asOfDate.Year.ToString, asOfDate.Month.ToString, asOfDate.Day.ToString))
        '    request.ContentLength = byteArray.Length
        '    Dim newStream As IO.Stream = request.GetRequestStream()
        '    newStream.Write(byteArray, 0, byteArray.Length)
        '    newStream.Close()

        '    Return request

        'End Function

        'Private Function ParseResultForCurrencies(ByVal webResponseString As String, _
        '    ByVal params As List(Of CurrencyRate.CurrencyRateParam), ByVal asOfDate As Date) As List(Of CurrencyRate)

        '    Dim result As New List(Of CurrencyRate)

        '    Dim d As New HtmlDocument
        '    d.LoadHtml(webResponseString.Trim)

        '    Dim StructuredTable As DataSet = ConvertHtmlToDataSet(webResponseString)

        '    If StructuredTable.Tables(5).Rows.Count > 2 Then

        '        For Each dr As DataRow In StructuredTable.Tables(5).Rows
        '            If Not Integer.TryParse(dr.Item(1).ToString, New Integer) Then dr.Item(1) = 1
        '        Next
        '        StructuredTable.Tables(5).Rows.RemoveAt(0)
        '        StructuredTable.Tables(5).Rows.RemoveAt(0)

        '        For Each param As CurrencyRate.CurrencyRateParam In params

        '            If param.Date.Date = asOfDate.Date Then

        '                For Each dr As DataRow In StructuredTable.Tables(5).Rows
        '                    If dr.Item(3).ToString.Trim.ToUpper.Replace("*", "") _
        '                        = param.CurrencyCode.Trim.ToUpper() Then

        '                        result.Add(New CurrencyRate(param, _
        '                            Double.Parse(dr.Item(4).ToString.Trim) _
        '                            / Integer.Parse(dr.Item(1).ToString.Trim)))

        '                        Exit For

        '                    End If
        '                Next

        '            End If

        '        Next

        '    End If

        '    Return result

        'End Function

        ''' <summary>
        ''' Gets an URL to download currency rates for a particular date.
        ''' </summary>
        ''' <param name="asOfDate">a date to download the currency rates for</param>
        ''' <remarks></remarks>
        Public Overrides Function GetDownloadUrl(ByVal asOfDate As Date) As String
            Return String.Format("http://www.lb.lt/exchange/Results.asp?Lang=L&id=7713&ord=1&dir=ASC&Y={0}&M={1}&D={2}&DD=D&WW=W&vykdyti=Vykdyti&S=csv", _
                asOfDate.Year.ToString(), asOfDate.Month.ToString(), asOfDate.Day.ToString())
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
            Dim currentCurrencyCode As String
            Dim isFound As Boolean

            For Each param As CurrencyRate.CurrencyRateParam In params

                If param.Date.Date = asOfDate.Date Then

                    isFound = False

                    For Each line As String In webResponseString.Split(New String() {vbCrLf}, _
                        StringSplitOptions.RemoveEmptyEntries)

                        currentLine = line.Split(New Char() {","c}, StringSplitOptions.None)
                        currentCurrencyCode = currentLine(1).Trim.ToUpper

                        If currentCurrencyCode.Trim.ToUpper() = param.CurrencyCode.Trim.ToUpper() Then

                            Dim perCurrencyAmount As Integer = 1
                            Try
                                perCurrencyAmount = Integer.Parse(currentLine(2).Trim)
                            Catch ex As Exception
                            End Try
                            If perCurrencyAmount < 1 Then perCurrencyAmount = 1
                            Dim currencyRate As Double = 0
                            Try
                                currencyRate = Double.Parse(currentLine(3).Trim, NumberStyles.Any, _
                                    CultureInfo.InvariantCulture)
                            Catch ex As Exception
                            End Try
                            currencyRate = CRound(currencyRate / perCurrencyAmount, 6)

                            result.Add(New CurrencyRate(param, currencyRate))

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