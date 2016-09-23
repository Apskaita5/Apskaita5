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


        Private Sub New()

        End Sub


        Private Sub ValidationEventHandler(ByVal sender As Object, ByVal args As ValidationEventArgs)
            If args.Severity = XmlSeverityType.Error Then
                _errors.Add(args)
            End If
        End Sub


        Private Function GetErrors() As String

            Dim result As String = ""
            Dim errorCount As Integer = 0

            For Each i As ValidationEventArgs In _errors
                If i.Severity = XmlSeverityType.Error Then
                    result = AddWithNewLine(result, String.Format( _
                        "* {0}", i.Message), False)
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

            For Each i As ValidationEventArgs In _errors
                If i.Severity = XmlSeverityType.Warning Then
                    result = AddWithNewLine(result, String.Format( _
                        "* {0}", i.Message), False)
                    warningCount += 1
                End If
            Next

            If warningCount > 0 Then
                result = String.Format("ĮSPĖJIMAS. Duomenys galimai neatitinka (XSD) reikalavimų formai ir (ar) turiniui. Rasta {0} perspėjimų:{1}{2}", _
                    warningCount.ToString, vbCrLf, result)
            End If

            Return result

        End Function


        ''' <summary>
        ''' Validates xmlSource against xsd schema. Returns true if the xml
        ''' is valid (no xsd errors, but could be some warnings).
        ''' </summary>
        ''' <param name="xmlSource">an XML string</param>
        ''' <param name="xsdFilePath">an XSD file path</param>
        ''' <param name="errorMessage">an out parameter that returns
        ''' error(s) message (an empty string if none)</param>
        ''' <param name="warningMessage">an out parameter that returns
        ''' warning(s) message (an empty string if none)</param>
        ''' <remarks></remarks>
        Public Shared Function Validate(ByVal xmlSource As String, _
            ByVal xsdFilePath As String, ByRef errorMessage As String, _
            ByRef warningMessage As String) As Boolean

            Dim encodedString As Byte() = System.Text.Encoding.UTF8.GetBytes(xmlSource)

            Dim errorBuilder As New XmlValidationErrorBuilder()

            ' Put the byte array into a stream and rewind it to the beginning
            Using ms As New MemoryStream(encodedString)

                ms.Flush()
                ms.Position = 0

                Dim doc As New XmlDocument()

                doc.Load(ms)
                doc.Schemas.Add(Nothing, xsdFilePath)

                doc.Validate(New ValidationEventHandler(AddressOf errorBuilder.ValidationEventHandler))

            End Using

            errorMessage = errorBuilder.GetErrors()
            warningMessage = errorBuilder.GetWarnings()

            Return StringIsNullOrEmpty(errorMessage)

        End Function

    End Class

End Namespace