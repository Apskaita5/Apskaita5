Imports System.Xml.Schema
Imports System.Xml
Imports System.IO

Namespace ActiveReports.Declarations

    ''' <summary>
    ''' Represents an encapsulated method to validate xml against xml schema (xsd).
    ''' </summary>
    ''' <remarks></remarks>
    Public NotInheritable Class XmlValidationErrorBuilder

        Private _errors As New List(Of ValidationEventArgs)()
        Private _source As String

        Private Sub New(ByVal source As String)
            _source = source
        End Sub


        Private Sub ValidationEventHandler(ByVal sender As Object, ByVal args As ValidationEventArgs)
            _errors.Add(args)
        End Sub


        Private Function GetErrors() As String

            Dim result As String = ""
            Dim errorCount As Integer = 0
            Dim lines As String() = _source.Split(New String() _
                {Environment.NewLine}, StringSplitOptions.None)

            For Each i As ValidationEventArgs In _errors
                If i.Severity = XmlSeverityType.Error Then
                    result = AddWithNewLine(result, String.Format( _
                        "* {0}", i.Message), False)
                    Dim fragment As String = GetFragment(lines, i.Exception.LineNumber)
                    If Not StringIsNullOrEmpty(fragment) Then
                        result = AddWithNewLine(result, "XML fragment:", False)
                        result = AddWithNewLine(result, fragment, False)
                    End If
                    errorCount += 1
                End If
            Next

            If errorCount > 0 Then
                result = String.Format("Duomenys neatitinka (XSD) reikalavimų struktūrai ir (ar) turiniui. Rastos {0} klaidos:{1}{2}", _
                    errorCount.ToString, vbCrLf, result)
            End If

            Return result

        End Function

        Private Function GetWarnings() As String

            Dim result As String = ""
            Dim warningCount As Integer = 0
            Dim lines As String() = _source.Split(New String() _
                {Environment.NewLine}, StringSplitOptions.None)

            For Each i As ValidationEventArgs In _errors
                If i.Severity = XmlSeverityType.Warning Then
                    result = AddWithNewLine(result, String.Format( _
                        "* {0}", i.Message), False)
                    Dim fragment As String = GetFragment(lines, i.Exception.LineNumber)
                    If Not StringIsNullOrEmpty(fragment) Then
                        result = AddWithNewLine(result, "XML fragment:", False)
                        result = AddWithNewLine(result, fragment, False)
                    End If
                    warningCount += 1
                End If
            Next

            If warningCount > 0 Then
                result = String.Format("ĮSPĖJIMAS. Duomenys galimai neatitinka (XSD) reikalavimų formai ir (ar) turiniui. Rasta {0} perspėjimų:{1}{2}", _
                    warningCount.ToString, vbCrLf, result)
            End If

            Return result

        End Function

        Private Function GetFragment(ByVal lines As String(), _
            ByVal lineNumber As Integer) As String

            If lineNumber < 1 OrElse lines Is Nothing OrElse _
                lines.Length < lineNumber Then Return ""

            Dim startLine As Integer = lineNumber - 5
            If startLine < 1 Then startLine = 1
            Dim endLine As Integer = lineNumber + 5
            If endLine > lines.Length Then endLine = lines.Length

            Dim result As String = ""
            For i As Integer = startLine To endLine
                result = result & Environment.NewLine & lines(i - 1)
            Next

            Return result.Trim

        End Function


        '''' <summary>
        '''' Validates xmlSource against xsd schema. Returns true if the xml
        '''' is valid (no xsd errors, but could be some warnings).
        '''' </summary>
        '''' <param name="xmlSource">an XML string</param>
        '''' <param name="xsdFilePath">an XSD file path</param>
        '''' <param name="errorMessage">an out parameter that returns
        '''' error(s) message (an empty string if none)</param>
        '''' <param name="warningMessage">an out parameter that returns
        '''' warning(s) message (an empty string if none)</param>
        '''' <remarks></remarks>
        'Public Shared Function Validate2(ByVal xmlSource As String, _
        '    ByVal xsdFilePath As String, ByRef errorMessage As String, _
        '    ByRef warningMessage As String) As Boolean

        '    Dim encodedString As Byte() = System.Text.Encoding.UTF8.GetBytes(xmlSource)

        '    Dim errorBuilder As New XmlValidationErrorBuilder(xmlSource)

        '    ' Put the byte array into a stream and rewind it to the beginning
        '    Using ms As New MemoryStream(encodedString)

        '        ms.Flush()
        '        ms.Position = 0

        '        Dim doc As New XmlDocument()

        '        doc.Load(ms)
        '        doc.Schemas.Add(Nothing, xsdFilePath)

        '        doc.Validate(New ValidationEventHandler(AddressOf errorBuilder.ValidationEventHandler))

        '    End Using

        '    errorMessage = errorBuilder.GetErrors()
        '    warningMessage = errorBuilder.GetWarnings()

        '    Return StringIsNullOrEmpty(errorMessage)

        'End Function

        ''' <summary>
        ''' Validates xmlSource against xsd schema. Returns true if the xml
        ''' is valid (no xsd errors, but could be some warnings).
        ''' </summary>
        ''' <param name="xmlSource">an XML string</param>
        ''' <param name="xsdFilePath">an XSD file path</param>
        ''' <param name="xsdNamespace">an XSD namespace to use</param>
        ''' <param name="errorMessage">an out parameter that returns
        ''' error(s) message (an empty string if none)</param>
        ''' <param name="warningMessage">an out parameter that returns
        ''' warning(s) message (an empty string if none)</param>
        ''' <remarks></remarks>
        Public Shared Function Validate(ByVal xmlSource As String, _
            ByVal xsdFilePath As String, ByVal xsdNamespace As String, _
            ByRef errorMessage As String, ByRef warningMessage As String) As Boolean

            Dim schemas As New XmlSchemaSet()
            Dim settings As New XmlReaderSettings()
            Dim errorBuilder As New XmlValidationErrorBuilder(xmlSource)

            schemas.Add(xsdNamespace, xsdFilePath)
            settings.Schemas = schemas
            settings.ValidationType = ValidationType.Schema
            AddHandler settings.ValidationEventHandler, AddressOf errorBuilder.ValidationEventHandler

            ' Put the byte array into a stream and rewind it to the beginning
            Using ms As New MemoryStream(System.Text.Encoding.UTF8.GetBytes(xmlSource))

                ms.Flush()
                ms.Position = 0

                Dim doc As New XmlDocument()

                doc.Load(XmlReader.Create(ms, settings))

            End Using

            errorMessage = errorBuilder.GetErrors()
            warningMessage = errorBuilder.GetWarnings()

            Return StringIsNullOrEmpty(errorMessage)

        End Function

    End Class

End Namespace
