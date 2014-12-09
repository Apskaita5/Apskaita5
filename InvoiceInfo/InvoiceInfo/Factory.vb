Imports System.Xml.Serialization
Imports System.Xml
Imports System.Text
Public Module Factory

    Public Function ToXmlString(Of T)(ByVal Obj As T) As String

        Dim serializer As New XmlSerializer(GetType(T))
        Dim settings As New XmlWriterSettings

        settings.Indent = True
        settings.IndentChars = " "
        settings.Encoding = New System.Text.UnicodeEncoding()

        Using ms As New IO.MemoryStream
            Using writer As XmlWriter = XmlWriter.Create(ms, settings)
                serializer.Serialize(writer, Obj)
                Return Encoding.Unicode.GetString(ms.ToArray())
            End Using
        End Using

    End Function

    Public Function FromXmlString(Of T)(ByVal xmlString As String) As T

        Dim serializer As New XmlSerializer(GetType(T))

        Using ms As New IO.MemoryStream(Encoding.Unicode.GetBytes(xmlString))
            Return DirectCast(serializer.Deserialize(ms), T)
        End Using

    End Function

End Module
