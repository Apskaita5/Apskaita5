Public Module CurrencyMethods

    ''' <summary>
    ''' An array of all the legal ISO 4217 currency codes.
    ''' </summary>
    ''' <remarks></remarks>
    Public CurrencyCodes As String() = {"LTL", "USD", "EUR", "RUB", "AFN", "ALL",
        "DZD", "AED", "AMD", "ANG", "AOA", "ARS", "AUD", "AWG", "AZN", "BAM", "BBD", "BDT",
        "BGN", "BHD", "BIF", "BMD", "BND", "BOB", "BRL", "BSD", "BTN", "BWP", "BYN", "BYR", "BZD",
        "CAD", "CDF", "CHF", "CLP", "CNY", "COP", "CRC", "CUP", "CVE", "CYP", "CZK", "DJF",
        "DKK", "DOP", "DZD", "EEK", "EGP", "ERN", "ETB", "FJD", "FKP", "GBP", "GEL", "GGP",
        "GHS", "GIP", "GMD", "GNF", "GTQ", "GYD", "HKD", "HNL", "HRK", "HTG", "HUF", "IDR",
        "ILS", "IMP", "INR", "IQD", "IRR", "ISK", "JEP", "JMD", "JOD", "JPY", "KES", "KGS",
        "KHR", "KMF", "KPW", "KRW", "KWD", "KYD", "KZT", "LAK", "LBP", "LKR", "LRD", "LSL",
        "LVL", "LYD", "MAD", "MDL", "MGA", "MKD", "MMK", "MNT", "MOP", "MRO", "MTL", "MUR",
        "MVR", "MWK", "MXN", "MYR", "MZN", "NAD", "NGN", "NIO", "NOK", "NPR", "NZD", "OMR",
        "PAB", "PEN", "PGK", "PHP", "PKR", "PLN", "PYG", "QAR", "RON", "RSD", "RWF", "SAR",
        "SBD", "SCR", "SDG", "SEK", "SGD", "SHP", "SLL", "SOS", "SPL", "SRD", "STD", "SVC",
        "SYP", "SZL", "THB", "TJS", "TMM", "TND", "TOP", "TRY", "TTD", "TVD", "TWD", "TZS",
        "UAH", "UGX", "UYU", "UZS", "VEF", "VND", "VUV", "WST", "XAF", "XAG", "XAU", "XCD",
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

        If StringIsNullOrEmpty(validatedCurrency1) Then
            validatedCurrency1 = baseCurrency
        End If

        Dim validatedCurrency2 As String = currency2

        If StringIsNullOrEmpty(validatedCurrency2) Then
            validatedCurrency2 = baseCurrency
        End If

        Return (validatedCurrency1.Trim.ToUpper = validatedCurrency2.Trim.ToUpper)

    End Function

    ''' <summary>
    ''' Returns true if the currency <paramref name="currencyCode" /> identified by an ISO 4217 code 
    ''' is the base company currency <paramref name="baseCurrency" />.
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

        If StringIsNullOrEmpty(baseCurrency) Then
            Throw New ArgumentNullException("baseCurrency")
        End If

        If StringIsNullOrEmpty(currencyCode) Then

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
        ByVal currencyRateSignificantDigits As Integer, ByVal defaultAmount As Double) As Double

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

    ''' <summary>
    ''' Converts <paramref name="amountToConvert">amountToConvert</paramref> to the base currency.
    ''' </summary>
    ''' <param name="amountToConvert">Amount to convert accounted in <paramref name="currency" />.</param>
    ''' <param name="currency">ISO 4217 code for the currency that the <paramref name="amountToConvert" /> is accounted in.</param>
    ''' <param name="currencyRate">Currency rate with respect to the <paramref name="baseCurrency" />
    ''' for the currency that the <paramref name="amountToConvert" /> is accounted in.</param>
    ''' <param name="baseCurrency">Base currency identified by ISO 4217 code.</param>
    ''' <param name="amountSignificantDigits">Round order applied to the result.</param>
    ''' <param name="currencyRateSignificantDigits">Round order applied to <paramref name="currencyRate" />.</param>
    ''' <remarks></remarks>
    Public Function ConvertToBaseCurrency(ByVal amountToConvert As Double, ByVal currency As String, _
        ByVal currencyRate As Double, ByVal baseCurrency As String, ByVal amountSignificantDigits As Integer, _
        ByVal currencyRateSignificantDigits As Integer) As Double

        If IsBaseCurrency(currency, baseCurrency) Then
            Return CRound(amountToConvert, amountSignificantDigits)
        End If

        Dim validatedCurrencyRate As Double = CRound(currencyRate, currencyRateSignificantDigits)
        Dim validatedAmount As Double = CRound(amountToConvert, amountSignificantDigits)

        Return CRound(validatedAmount * validatedCurrencyRate, amountSignificantDigits)

    End Function

    ''' <summary>
    ''' Converts <paramref name="amountToConvert">amountToConvert</paramref> to the target currency.
    ''' </summary>
    ''' <param name="amountToConvert">Amount to convert accounted in <paramref name="baseCurrency" />.</param>
    ''' <param name="targetCurrency">ISO 4217 code for the currency to convert the <paramref name="amountToConvert" /> to.</param>
    ''' <param name="currencyRate">Currency rate with respect to the <paramref name="baseCurrency" />
    ''' for the currency to convert the <paramref name="amountToConvert" /> to.</param>
    ''' <param name="baseCurrency">Base currency identified by ISO 4217 code.</param>
    ''' <param name="amountSignificantDigits">Round order applied to the result.</param>
    ''' <param name="currencyRateSignificantDigits">Round order applied to <paramref name="currencyRate" />.</param>
    ''' <param name="defaultAmount">Return value in case a conversion is not possible, 
    ''' e.g. <paramref name="currencyRate" /> is zero.</param>
    ''' <remarks></remarks>
    Public Function ConvertFromBaseCurrency(ByVal amountToConvert As Double, ByVal targetCurrency As String, _
        ByVal currencyRate As Double, ByVal baseCurrency As String, ByVal amountSignificantDigits As Integer, _
        ByVal currencyRateSignificantDigits As Integer, ByVal defaultAmount As Double) As Double

        If IsBaseCurrency(targetCurrency, baseCurrency) Then
            Return CRound(amountToConvert, amountSignificantDigits)
        End If

        Dim validatedCurrencyRate As Double = CRound(currencyRate, currencyRateSignificantDigits)
        Dim validatedAmount As Double = CRound(amountToConvert, amountSignificantDigits)

        If CRound(currencyRate, currencyRateSignificantDigits) > 0.0 Then
            Return CRound(validatedAmount / validatedCurrencyRate, amountSignificantDigits)
        Else
            Return CRound(defaultAmount, amountSignificantDigits)
        End If

    End Function

    ''' <summary>
    ''' Checks if the <paramref name="currencyCode">currency code</paramref> is a valid ISO 4217 code.
    ''' </summary>
    ''' <param name="currencyCode">A currency code to check.</param>
    ''' <param name="emptyCurrencyIsInvalid">Whether to consider a null or empty string as an invalid currency.
    ''' Default - consider a null or empty string as the base currency, i.e. valid.</param>
    ''' <returns><code>True</code> if the <paramref name="currencyCode" /> is a valid ISO 4217 code..</returns>
    ''' <remarks></remarks>
    Public Function IsValidCurrency(ByVal currencyCode As String, _
        Optional ByVal emptyCurrencyIsInvalid As Boolean = False) As Boolean

        If StringIsNullOrEmpty(currencyCode) Then
            Return Not emptyCurrencyIsInvalid
        End If

        Return Array.IndexOf(CurrencyCodes, currencyCode.Trim.ToUpper()) >= 0

    End Function

End Module
