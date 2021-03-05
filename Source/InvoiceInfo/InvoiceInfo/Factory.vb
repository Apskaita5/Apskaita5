Imports System.Xml.Serialization
Imports System.Xml
Imports System.Text
Public Module Factory

    ''' <summary>
    ''' Serializes an object to XML string. 
    ''' </summary>
    ''' <typeparam name="T">a type of the object to serialize</typeparam>
    ''' <param name="obj">the object to serialize</param>
    ''' <remarks>Used for consistent (de)serialization for compatibility.</remarks>
    Public Function ToXmlString(Of T)(ByVal obj As T) As String

        Dim serializer As New XmlSerializer(GetType(T))
        Dim settings As New XmlWriterSettings

        settings.Indent = True
        settings.IndentChars = " "
        settings.Encoding = New UTF8Encoding(False)

        Using ms As New IO.MemoryStream
            Using writer As XmlWriter = XmlWriter.Create(ms, settings)
                serializer.Serialize(writer, obj)
                Return settings.Encoding.GetString(ms.ToArray())
            End Using
        End Using

    End Function

    ''' <summary>
    ''' Deserializes XML string into an object of type T.
    ''' </summary>
    ''' <typeparam name="T">a type of the object to deserialize</typeparam>
    ''' <remarks>Used for consistent (de)serialization for compatibility.</remarks>
    Public Function FromXmlString(Of T)(ByVal xmlString As String) As T

        Dim serializer As New XmlSerializer(GetType(T))

        Using ms As New IO.MemoryStream((New UTF8Encoding(False)).GetBytes(xmlString))
            Return DirectCast(serializer.Deserialize(ms), T)
        End Using

    End Function

End Module
