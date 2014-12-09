Public Structure CurrencyRate
    Friend Shared ReadOnly CurrencyCodes As String() = {"LTL", "USD", "EUR", "RUB", "AFN", "ALL", _
        "DZD", "AED", "AMD", "ANG", "AOA", "ARS", "AUD", "AWG", "AZN", "BAM", "BBD", "BDT", _
        "BGN", "BHD", "BIF", "BMD", "BND", "BOB", "BRL", "BSD", "BTN", "BWP", "BYR", "BZD", _
        "CAD", "CDF", "CHF", "CLP", "CNY", "COP", "CRC", "CUP", "CVE", "CYP", "CZK", "DJF", _
        "DKK", "DOP", "DZD", "EEK", "EGP", "ERN", "ETB", "FJD", "FKP", "GBP", "GEL", "GGP", _
        "GHS", "GIP", "GMD", "GNF", "GTQ", "GYD", "HKD", "HNL", "HRK", "HTG", "HUF", "IDR", _
        "ILS", "IMP", "INR", "IQD", "IRR", "ISK", "JEP", "JMD", "JOD", "JPY", "KES", "KGS", _
        "KHR", "KMF", "KPW", "KRW", "KWD", "KYD", "KZT", "LAK", "LBP", "LKR", "LRD", "LSL", _
        "LVL", "LYD", "MAD", "MDL", "MGA", "MKD", "MMK", "MNT", "MOP", "MRO", "MTL", "MUR", _
        "MVR", "MWK", "MXN", "MYR", "MZN", "NAD", "NGN", "NIO", "NOK", "NPR", "NZD", "OMR", _
        "PAB", "PEN", "PGK", "PHP", "PKR", "PLN", "PYG", "QAR", "RON", "RSD", "RWF", "SAR", _
        "SBD", "SCR", "SDG", "SEK", "SGD", "SHP", "SLL", "SOS", "SPL", "SRD", "STD", "SVC", _
        "SYP", "SZL", "THB", "TJS", "TMM", "TND", "TOP", "TRY", "TTD", "TVD", "TWD", "TZS", _
        "UAH", "UGX", "UYU", "UZS", "VEF", "VND", "VUV", "WST", "XAF", "XAG", "XAU", "XCD", _
        "XDR", "XOF", "XPD", "XPF", "XPT", "YER", "ZAR", "ZMK", "ZWD"}

    Public ReadOnly [Date] As Date
    Public ReadOnly CurrencyCode As String
    Public ReadOnly Rate As Double

    Public Sub New(ByVal nDate As Date, ByVal nCurrencyCode As String, _
        Optional ByVal ThrowOnInvalidCurrency As Boolean = True)
        If nCurrencyCode Is Nothing OrElse String.IsNullOrEmpty(nCurrencyCode.Trim) _
            OrElse Array.IndexOf(CurrencyCodes, nCurrencyCode.Trim.ToUpper) < 0 Then _
            nCurrencyCode = "LTL"
        If Array.IndexOf(CurrencyCodes, nCurrencyCode.Trim.ToUpper) < 0 Then
            If ThrowOnInvalidCurrency Then
                Throw New Exception("Klaida. Valiutos kodas " & _
                    nCurrencyCode.Trim.ToUpper & " nežinomas.")
            Else
                nCurrencyCode = "LTL"
            End If
        End If
        Me.Date = nDate.Date
        Me.Rate = 0
        Me.CurrencyCode = nCurrencyCode.Trim.ToUpper
    End Sub

    Friend Sub New(ByVal nCurrencyRate As CurrencyRate, ByVal nRate As Double)
        Me.Date = nCurrencyRate.Date
        Me.Rate = nRate
        Me.CurrencyCode = nCurrencyRate.CurrencyCode
    End Sub

End Structure
