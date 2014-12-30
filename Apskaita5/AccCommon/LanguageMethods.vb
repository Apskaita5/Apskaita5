Public Module LanguageMethods

    Private _LanguageDictionary As Dictionary(Of String, String) = Nothing

    ''' <summary>
    ''' Gets a language dictionary where key is a ISO 639-1 language code 
    ''' and the value is a human readable language name in lithuanian.
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public ReadOnly Property LanguageDictionary() As Dictionary(Of String, String)
        Get
            If _LanguageDictionary Is Nothing Then FetchLanguageDictionary()
            Return _LanguageDictionary
        End Get
    End Property

    Private Sub FetchLanguageDictionary()
        _LanguageDictionary = New Dictionary(Of String, String)
        For Each s As String In My.Resources.LanguageCodes.Split(New String() {vbCrLf}, _
            StringSplitOptions.RemoveEmptyEntries)
            _LanguageDictionary.Add(GetField(s, vbTab, 0).Trim.ToUpper, GetField(s, vbTab, 1).Trim.ToLower)
        Next
    End Sub

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

        If languageCode Is Nothing OrElse String.IsNullOrEmpty(languageCode.Trim) Then Return ""

        If LanguageDictionary.ContainsKey(languageCode.Trim.ToUpper) Then
            Return LanguageDictionary.Item(languageCode.Trim.ToUpper)
        End If

        If throwOnUnknownLanguage Then
            Throw New Exception("Language identified by ISO 639-1 code '" & languageCode & "' is unknown.")
        End If

        Return ""

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

        If languageName Is Nothing OrElse String.IsNullOrEmpty(languageName.Trim) Then Return ""

        If LanguageDictionary.ContainsValue(languageName.Trim.ToLower) Then

            For Each d As KeyValuePair(Of String, String) In _LanguageDictionary
                If d.Value.Trim.ToLower = languageName.Trim.ToLower Then Return d.Key
            Next

        End If

        If throwOnUnknownLanguage Then
            Throw New ArgumentException("Language identified by the lithuanian name '" _
                & languageName & "' is unknown.")
        End If

        Return ""

    End Function

    ''' <summary>
    ''' Gets a list of human readable language names in lithuanian.
    ''' </summary>
    ''' <param name="addEmptyLine">Whether to insert an empty string in the begining of the list.</param>
    ''' <returns>A list of human readable language names in lithuanian.</returns>
    ''' <remarks></remarks>
    Public Function GetLanguageList(Optional ByVal addEmptyLine As Boolean = True) As List(Of String)

        Dim result As New List(Of String)

        For Each d As KeyValuePair(Of String, String) In LanguageDictionary
            result.Add(d.Value)
        Next

        result.Sort()

        If addEmptyLine Then result.Insert(0, "")

        Return result

    End Function

End Module
