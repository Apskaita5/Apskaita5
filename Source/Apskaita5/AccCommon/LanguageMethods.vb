Public Module LanguageMethods

    ''' <summary>
    ''' An array of all the legal ISO 639-1 language codes.
    ''' </summary>
    ''' <remarks></remarks>
    Private ReadOnly ValidLanguages As String() = New String() {"aa", "ab", "ae", "af", "ak", "am", _
        "an", "ar", "as", "av", "ay", "az", "ba", "be", "bg", "bh", "bi", "bm", "bn", "bo", "br", _
        "bs", "ca", "ce", "ch", "co", "cr", "cs", "cu", "cv", "cy", "da", "de", "dv", "dz", "ee", _
        "el", "en", "eo", "es", "et", "eu", "fa", "ff", "fi", "fj", "fo", "fr", "fy", "ga", "gd", _
        "gl", "gn", "gu", "gv", "ha", "he", "hi", "ho", "hr", "ht", "hu", "hy", "hz", "ia", "id", _
        "ie", "ig", "ii", "ik", "io", "is", "it", "iu", "ja", "jv", "ka", "kg", "ki", "kj", "kk", _
        "kl", "km", "kn", "ko", "kr", "ks", "ku", "kv", "kw", "ky", "la", "lb", "lg", "li", "ln", _
        "lo", "lt", "lu", "lv", "mg", "mh", "mi", "mk", "ml", "mn", "mr", "ms", "mt", "my", "na", _
        "nb", "nd", "ne", "ng", "nl", "nn", "no", "nr", "nv", "ny", "oc", "oj", "om", "or", "os", _
        "pa", "pi", "pl", "ps", "pt", "qu", "rm", "rn", "ro", "ru", "rw", "sa", "sc", "sd", "se", _
        "sg", "si", "sh", "sk", "sl", "sm", "sn", "so", "sq", "sr", "ss", "st", "su", "sv", "sw", _
        "ta", "te", "tg", "th", "ti", "tk", "tl", "tn", "to", "tr", "ts", "tt", "tw", "ty", "ug", _
        "uk", "ur", "uz", "ve", "vi", "vo", "wa", "wo", "xh", "yi", "yo", "za", "zh-cn", "zh-tw", "zu"}

    Private ReadOnly DefaultBaseLanguage As String = "lt"

    Private ReadOnly LanguageCodeResourcePrefix As String = "LanguageCode_"

    'Private _LanguageDictionary As Dictionary(Of String, String) = Nothing

    '''' <summary>
    '''' Gets a language dictionary where key is a ISO 639-1 language code 
    '''' and the value is a human readable language name in lithuanian.
    '''' </summary>
    '''' <value></value>
    '''' <returns></returns>
    '''' <remarks></remarks>
    'Public ReadOnly Property LanguageDictionary() As Dictionary(Of String, String)
    '    Get
    '        If _LanguageDictionary Is Nothing Then FetchLanguageDictionary()
    '        Return _LanguageDictionary
    '    End Get
    'End Property

    'Private Sub FetchLanguageDictionary()
    '    _LanguageDictionary = New Dictionary(Of String, String)
    '    For Each s As String In My.Resources.LanguageCodes.Split(New String() {vbCrLf}, _
    '        StringSplitOptions.RemoveEmptyEntries)
    '        _LanguageDictionary.Add(GetField(s, vbTab, 0).Trim.ToUpper, GetField(s, vbTab, 1).Trim.ToLower)
    '    Next
    'End Sub

    '''' <summary>
    '''' Gets a human readable language name in lithuanian by ISO 639-1 language code.
    '''' </summary>
    '''' <param name="languageCode">ISO 639-1 language code.</param>
    '''' <param name="throwOnUnknownLanguage">Whether to throw an exception 
    '''' if the <paramref name="languageCode"/> is invalid.</param>
    '''' <returns>A human readable language name in lithuanian.</returns>
    '''' <remarks></remarks>
    'Public Function GetLanguageName(ByVal languageCode As String, _
    '    Optional ByVal throwOnUnknownLanguage As Boolean = True) As String

    '    If languageCode Is Nothing OrElse String.IsNullOrEmpty(languageCode.Trim) Then Return ""

    '    If LanguageDictionary.ContainsKey(languageCode.Trim.ToUpper) Then
    '        Return LanguageDictionary.Item(languageCode.Trim.ToUpper)
    '    End If

    '    If throwOnUnknownLanguage Then
    '        Throw New Exception("Language identified by ISO 639-1 code '" & languageCode & "' is unknown.")
    '    End If

    '    Return ""

    'End Function

    '''' <summary>
    '''' Gets an ISO 639-1 language code by human readable language name in lithuanian.
    '''' </summary>
    '''' <param name="languageName">A human readable language name in lithuanian.</param>
    '''' <param name="throwOnUnknownLanguage">Whether to throw an exception 
    '''' if the <paramref name="languageName"/> is invalid.</param>
    '''' <returns>An ISO 639-1 language code.</returns>
    '''' <remarks></remarks>
    'Public Function GetLanguageCode(ByVal languageName As String, _
    '    Optional ByVal throwOnUnknownLanguage As Boolean = True) As String

    '    If languageName Is Nothing OrElse String.IsNullOrEmpty(languageName.Trim) Then Return ""

    '    If LanguageDictionary.ContainsValue(languageName.Trim.ToLower) Then

    '        For Each d As KeyValuePair(Of String, String) In _LanguageDictionary
    '            If d.Value.Trim.ToLower = languageName.Trim.ToLower Then Return d.Key
    '        Next

    '    End If

    '    If throwOnUnknownLanguage Then
    '        Throw New ArgumentException("Language identified by the lithuanian name '" _
    '            & languageName & "' is unknown.")
    '    End If

    '    Return ""

    'End Function

    '''' <summary>
    '''' Gets a list of human readable language names in lithuanian.
    '''' </summary>
    '''' <param name="addEmptyLine">Whether to insert an empty string in the begining of the list.</param>
    '''' <returns>A list of human readable language names in lithuanian.</returns>
    '''' <remarks></remarks>
    'Public Function GetLanguageList(Optional ByVal addEmptyLine As Boolean = True) As List(Of String)

    '    Dim result As New List(Of String)

    '    For Each d As KeyValuePair(Of String, String) In LanguageDictionary
    '        result.Add(d.Value)
    '    Next

    '    result.Sort()

    '    If addEmptyLine Then result.Insert(0, "")

    '    Return result

    'End Function

    ''' <summary>
    ''' Validates language code. Returns true if the <paramref name="languageCode">languageCode</paramref> 
    ''' is an ISO 639-1 language code or is null or empty.
    ''' </summary>
    ''' <param name="languageCode">A language code to check.</param>
    ''' <remarks></remarks>
    Public Function IsLanguageCodeValid(ByVal languageCode As String) As Boolean
        If StringIsNullOrEmpty(languageCode) Then Return True
        Return Not Array.IndexOf(ValidLanguages, languageCode.Trim.ToLower) < 0
    End Function

    ''' <summary>
    ''' Gets a human readable language name in lithuanian by ISO 639-1 language code.
    ''' </summary>
    ''' <param name="languageCode">ISO 639-1 language code.</param>
    ''' <param name="throwOnUnknownLanguage">Whether to throw an exception 
    ''' if the <paramref name="languageCode"/> is invalid.</param>
    ''' <returns>A human readable language name in lithuanian.</returns>
    ''' <remarks></remarks>
    Public Function GetLanguageName(ByVal languageCode As String, _
        Optional ByVal throwOnUnknownLanguage As Boolean = True) As String

        If StringIsNullOrEmpty(languageCode) Then Return ""

        If Not IsLanguageCodeValid(languageCode) Then
            If throwOnUnknownLanguage Then
                Throw New Exception(String.Format(My.Resources.Common_InvalidLanguageCode, languageCode))
            End If
            Return ""
        End If

        Dim result As String = ""
        Try
            result = My.Resources.ResourceManager.GetString(LanguageCodeResourcePrefix _
                & languageCode.Trim.ToLower.Replace("-", "_"))
        Catch ex As Exception
        End Try

        If StringIsNullOrEmpty(result) AndAlso throwOnUnknownLanguage Then
            Throw New Exception(String.Format(My.Resources.Common_localizedLanguageNameMissing, _
                languageCode, System.Globalization.CultureInfo.CurrentUICulture.Name))
        End If

        If result Is Nothing Then result = ""

        Return result

    End Function

    ''' <summary>
    ''' Gets an ISO 639-1 language code by human readable language name in lithuanian.
    ''' </summary>
    ''' <param name="languageName">A human readable language name in lithuanian.</param>
    ''' <param name="throwOnUnknownLanguage">Whether to throw an exception 
    ''' if the <paramref name="languageName"/> is invalid.</param>
    ''' <returns>An ISO 639-1 language code.</returns>
    ''' <remarks></remarks>
    Public Function GetLanguageCode(ByVal languageName As String, _
        Optional ByVal throwOnUnknownLanguage As Boolean = True) As String

        If StringIsNullOrEmpty(languageName) Then Return ""

        For Each value As String In ValidLanguages
            If GetLanguageName(value, False).Trim.ToLower = languageName.Trim.ToLower Then
                Return value
            End If
        Next

        If throwOnUnknownLanguage Then
            Throw New ArgumentException(String.Format(My.Resources.Common_InvalidLanguageName, _
                languageName))
        End If

        Return ""

    End Function

    ''' <summary>
    ''' Gets a list of human readable language names in lithuanian.
    ''' </summary>
    ''' <param name="addEmptyLine">Whether to insert an empty string in the begining of the list.</param>
    ''' <returns>A list of human readable language names in lithuanian.</returns>
    ''' <remarks></remarks>
    Public Function GetLanguageNameList(Optional ByVal addEmptyLine As Boolean = True) As List(Of String)

        Dim result As New List(Of String)

        For Each value As String In ValidLanguages
            Dim name As String = GetLanguageName(value, False)
            If Not StringIsNullOrEmpty(name) Then result.Add(name)
        Next

        result.Sort()

        If addEmptyLine Then result.Insert(0, "")

        Return result

    End Function

    ''' <summary>
    ''' Gets a list of ISO 639-1 language codes.
    ''' </summary>
    ''' <param name="addEmptyLine">whether to insert an empty string in the begining of the list</param>
    ''' <returns>A list of ISO 639-1 language codes.</returns>
    ''' <remarks></remarks>
    Public Function GetLanguageCodeList(Optional ByVal addEmptyLine As Boolean = True) As List(Of String)

        Dim result As New List(Of String)

        For Each value As String In ValidLanguages
            result.Add(value)
        Next

        result.Sort()

        If addEmptyLine Then result.Insert(0, "")

        Return result

    End Function

    ''' <summary>
    ''' Compares ISO 639-1 language codes and returnes true if they are the same.
    ''' </summary>
    ''' <param name="languageCode1">First language code to compare.</param>
    ''' <param name="languageCode2">Second language code to compare.</param>
    ''' <param name="baseLanguageCode">Base language code.</param>
    ''' <remarks>Null, empty or invalid language code is considered as a base language.</remarks>
    Public Function LanguagesEquals(ByVal languageCode1 As String, ByVal languageCode2 As String, _
        ByVal baseLanguageCode As String) As Boolean

        Dim validatedBaseCode As String = baseLanguageCode
        If StringIsNullOrEmpty(baseLanguageCode) OrElse Not IsLanguageCodeValid(baseLanguageCode) Then
            validatedBaseCode = DefaultBaseLanguage
        End If

        Dim validatedCode1 As String = languageCode1
        If StringIsNullOrEmpty(languageCode1) OrElse Not IsLanguageCodeValid(languageCode1) Then
            validatedCode1 = validatedBaseCode
        End If

        Dim validatedCode2 As String = languageCode2
        If StringIsNullOrEmpty(languageCode2) OrElse Not IsLanguageCodeValid(languageCode2) Then
            validatedCode2 = validatedBaseCode
        End If

        Return (validatedCode1.Trim.ToUpper() = validatedCode2.Trim.ToUpper())

    End Function


End Module
