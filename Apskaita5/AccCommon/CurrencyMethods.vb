Public Module CurrencyMethods

    Public CurrencyCodes As String() = {"LTL", "USD", "EUR", "RUB", "AFN", "ALL", _
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

    ''' <summary>
    ''' Checks if currencies <paramref name="currency1" /> and <paramref name="currency2" /> 
    ''' identified by ISO 4217 codes represents the same currency assuming empty code string is 
    ''' <paramref name="baseCurrency" />.
    ''' </summary>
    ''' <param name="currency1">First currency ISO 4217 code to check for equality.</param>
    ''' <param name="currency2">Second currency ISO 4217 code to check for equality.</param>
    ''' <param name="baseCurrency">Base currency identified by ISO 4217 code.</param>
    ''' <returns><code>True</code> if <paramref name="currency1" /> and <paramref name="currency2" /> 
    ''' represent the same currency.</returns>
    ''' <remarks></remarks>
    Public Function CurrenciesEquals(ByVal currency1 As String, ByVal currency2 As String, _
        ByVal baseCurrency As String) As Boolean

        If baseCurrency Is Nothing OrElse String.IsNullOrEmpty(baseCurrency.Trim) Then
            Throw New ArgumentNullException("Parameter baseCurrency cannot be null or empty for method Utilities.CurrenciesEquals.")
        End If

        Dim validatedCurrency1 As String = currency1

        If validatedCurrency1 Is Nothing OrElse String.IsNullOrEmpty(validatedCurrency1.Trim) Then
            validatedCurrency1 = baseCurrency
        End If

        Dim validatedCurrency2 As String = currency2

        If validatedCurrency2 Is Nothing OrElse String.IsNullOrEmpty(validatedCurrency2.Trim) Then
            validatedCurrency2 = baseCurrency
        End If

        Return (validatedCurrency1.Trim.ToUpper = validatedCurrency2.Trim.ToUpper)

    End Function

    ''' <summary>
    ''' Checks if currencies <paramref name="currency1" /> and <paramref name="currency2" /> 
    ''' identified by ISO 4217 codes represents the same currency assuming empty code string is 
    ''' <paramref name="baseCurrency" />.
    ''' </summary>
    ''' <param name="currencyCode">Currency ISO 4217 code to check for equality with the <paramref name="baseCurrency" />.</param>
    ''' <param name="baseCurrency">Base currency identified by ISO 4217 code.</param>
    ''' <returns><code>True</code> if <paramref name="currencyCode" /> and <paramref name="baseCurrency" /> 
    ''' represent the same currency.</returns>
    ''' <remarks></remarks>
    Public Function IsBaseCurrency(ByVal currencyCode As String, ByVal baseCurrency As String) As Boolean
        Return CurrenciesEquals(currencyCode, baseCurrency, baseCurrency)
    End Function

    ''' <summary>
    ''' Ensures that the currency code is not null or empty defaulting to <paramref name="baseCurrency" />.
    ''' If <paramref name="currencyCode" /> is null or empty returns <paramref name="baseCurrency" />.
    ''' Otherwise returns trimed uppercased <paramref name="currencyCode" />
    ''' </summary>
    ''' <param name="currencyCode">Currency ISO 4217 code to check for null or empty state.</param>
    ''' <param name="baseCurrency">Base currency identified by ISO 4217 code.</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetCurrencySafe(ByVal currencyCode As String, ByVal baseCurrency As String) As String

        If baseCurrency Is Nothing OrElse String.IsNullOrEmpty(baseCurrency.Trim) Then
            Throw New ArgumentNullException("Parameter baseCurrency cannot be null or empty for method Utilities.GetCurrencySafe.")
        End If

        If currencyCode Is Nothing OrElse String.IsNullOrEmpty(currencyCode.Trim) Then

            Return baseCurrency

        Else

            Return currencyCode.Trim.ToUpper

        End If

    End Function

    ''' <summary>
    ''' Converts <paramref name="amountToConvert" /> accounted in currency <paramref name="originalCurrency" />
    ''' with conversion rate <paramref name="originalCurrencyRate" /> with respect to <paramref name="baseCurrency" />
    ''' to amount accounted in currency <paramref name="targetCurrency" /> with conversion rate 
    ''' <paramref name="targetCurrencyRate" /> with respect to <paramref name="baseCurrency" />.
    ''' </summary>
    ''' <param name="amountToConvert">Amount to convert accounted in <paramref name="originalCurrency" />.</param>
    ''' <param name="originalCurrency">ISO 4217 code for the currency that the <paramref name="amountToConvert" /> is accounted in.</param>
    ''' <param name="originalCurrencyRate">Currency rate with respect to the <paramref name="baseCurrency" />
    ''' for the currency that the <paramref name="amountToConvert" /> is accounted in.</param>
    ''' <param name="targetCurrency">ISO 4217 code for the currency that the returned value is accounted in.</param>
    ''' <param name="targetCurrencyRate">Currency rate with respect to the <paramref name="baseCurrency" />
    ''' for the currency that the returned value is accounted in.</param>
    ''' <param name="baseCurrency">Base currency identified by ISO 4217 code.</param>
    ''' <param name="amountSignificantDigits">Round order applied to the result.</param>
    ''' <param name="currencyRateSignificantDigits">Round order applied to <paramref name="originalCurrencyRate" /> 
    ''' and <paramref name="targetCurrencyRate" />.</param>
    ''' <param name="defaultAmount">Return value in case a conversion is not possible, 
    ''' e.g. <paramref name="targetCurrencyRate" /> is zero.</param>
    ''' <returns><paramref name="amountToConvert" /> converted to <paramref name="targetCurrency" />.</returns>
    ''' <remarks></remarks>
    Public Function ConvertCurrency(ByVal amountToConvert As Double, ByVal originalCurrency As String, _
        ByVal originalCurrencyRate As Double, ByVal targetCurrency As String, ByVal targetCurrencyRate As Double, _
        ByVal baseCurrency As String, ByVal amountSignificantDigits As Integer, _
        ByVal currencyRateSignificantDigits As Integer, _
        ByVal defaultAmount As Double) As Double

        ' zero remains zero in any currency
        If CRound(amountToConvert, amountSignificantDigits) = 0 Then
            Return 0
        End If

        ' if the currencies are the same, amount is the same
        If CurrenciesEquals(originalCurrency, targetCurrency, baseCurrency) Then
            Return CRound(amountToConvert, amountSignificantDigits)
        End If

        ' base currency rate with respect to itself is always 1
        Dim validatedOriginalCurrencyRate As Double = originalCurrencyRate

        If IsBaseCurrency(originalCurrency, baseCurrency) Then
            validatedOriginalCurrencyRate = 1
        End If

        Dim validatedTargetCurrencyRate As Double = targetCurrencyRate

        If IsBaseCurrency(targetCurrency, baseCurrency) Then
            validatedTargetCurrencyRate = 1
        End If

        ' apply currencyRateSignificantDigits
        validatedOriginalCurrencyRate = CRound(validatedOriginalCurrencyRate, currencyRateSignificantDigits)
        validatedTargetCurrencyRate = CRound(validatedTargetCurrencyRate, currencyRateSignificantDigits)

        ' do calculus if possible
        If CRound(validatedTargetCurrencyRate, currencyRateSignificantDigits) > 0 Then
            Return CRound(CRound(amountToConvert * validatedOriginalCurrencyRate, amountSignificantDigits) _
                / validatedTargetCurrencyRate, amountSignificantDigits)
        Else
            Return CRound(defaultAmount, amountSignificantDigits)
        End If

    End Function

End Module
