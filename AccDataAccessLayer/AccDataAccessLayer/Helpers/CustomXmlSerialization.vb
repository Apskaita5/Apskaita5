Imports System.Xml
Imports System.Reflection

Public Module CustomXmlSerialization

    Private Const Name_Field As String = "Field"
    Private Const Name_FieldName As String = "FieldName"
    Private Const Name_FieldType As String = "FieldType"
    Private Const Name_FieldTypeIsTrivial As String = "FieldTypeIsTrivial"
    Private Const Name_FieldValueIsNull As String = "FieldValueIsNull"
    Private Const Name_IsICustomXmlSerialized As String = "IsICustomXmlSerialized"
    Private Const Name_IsBinarySerialized As String = "IsBinarySerialized"
    Private Const Name_IsCollectionNode As String = "IsCollectionNode"
    Private Const ValueKey_Null As String = "Null"
    Private Const ValueKey_CustomXmlSerialized As String = "CustomXmlSerialized"

    Public Sub AddChildNode(ByRef GroupNode As XmlNode, ByVal fieldName As String, _
        ByVal fieldValue As Object, Optional ByVal fieldType As Type = Nothing)
        GroupNode.AppendChild(GetNode(GroupNode.OwnerDocument, fieldName, fieldValue, fieldType))
    End Sub

    Public Function CustomXmlSerializeObj(ByVal Obj As Object, _
        ByVal objName As String) As XmlDocument
        If Obj Is Nothing OrElse TypeOf Obj Is DBNull Then Throw _
            New ArgumentNullException("Object with null value cannot be serialized.")
        Dim result As New XmlDocument()
        result.AppendChild(result.CreateXmlDeclaration("1.0", "UTF8", "yes"))
        result.AppendChild(GetNode(result, objName, Obj, Nothing))
        Return result
    End Function

    Public Sub CustomXmlSerializeObj(ByVal FileName As String, ByVal Obj As Object, _
        ByVal objName As String)
        If Obj Is Nothing OrElse TypeOf Obj Is DBNull Then Throw _
            New ArgumentNullException("Object with null value cannot be serialized.")
        If FileName Is Nothing OrElse String.IsNullOrEmpty(FileName.Trim) Then _
            Throw New ArgumentNullException("Path to xml file is not spefified.")

        Using w As New System.Xml.XmlTextWriter(FileName, Text.Encoding.UTF8)
            w.Formatting = Xml.Formatting.Indented
            CustomXmlSerializeObj(Obj, objName).Save(w)
            w.Close()
        End Using

    End Sub

    Public Sub SetValues(ByRef Obj As Object, ByVal node As XmlNode)

        Dim fieldName As String = node.Attributes.GetNamedItem(Name_FieldName).Value
        Dim fieldTypeIsTrivial As Boolean = GetBooleanString( _
            node.Attributes.GetNamedItem(Name_FieldTypeIsTrivial).Value)
        Dim fieldValueIsNull As Boolean = GetBooleanString( _
            node.Attributes.GetNamedItem(Name_FieldValueIsNull).Value)
        Dim IsICustomXmlSerialized As Boolean = GetBooleanString( _
            node.Attributes.GetNamedItem(Name_IsICustomXmlSerialized).Value)
        Dim IsBinarySerialized As Boolean = GetBooleanString( _
            node.Attributes.GetNamedItem(Name_IsBinarySerialized).Value)
        Dim IsCollectionNode As Boolean = GetBooleanString( _
            node.Attributes.GetNamedItem(Name_IsCollectionNode).Value)

        Dim fieldType As Type = GetDeSerializedType( _
            node.Attributes.GetNamedItem(Name_FieldType).Value, fieldTypeIsTrivial)
        Dim serializedFieldValue As String
        If Not IsICustomXmlSerialized AndAlso Not IsCollectionNode Then
            serializedFieldValue = node.InnerText
        Else
            serializedFieldValue = ""
        End If

        If fieldTypeIsTrivial Then
            Obj = GetDeSerializedValue(serializedFieldValue, fieldType)
        ElseIf fieldValueIsNull Then
            Obj = Nothing
        ElseIf IsBinarySerialized Then
            Obj = GetBinaryDeSerializedValue(serializedFieldValue, fieldName)
        End If

        Dim CallCustomDeSerialize As Boolean = IsCollectionNode OrElse IsICustomXmlSerialized

        If IsCollectionNode OrElse IsICustomXmlSerialized Then

            Dim FI As FieldInfo() = Obj.GetType.GetFields(BindingFlags.NonPublic _
                Or BindingFlags.Instance)

            For Each n As XmlNode In node.ChildNodes

                fieldName = n.Attributes.GetNamedItem(Name_FieldName).Value
                fieldTypeIsTrivial = GetBooleanString( _
                    n.Attributes.GetNamedItem(Name_FieldTypeIsTrivial).Value)
                fieldValueIsNull = GetBooleanString( _
                    n.Attributes.GetNamedItem(Name_FieldValueIsNull).Value)
                IsICustomXmlSerialized = GetBooleanString( _
                    n.Attributes.GetNamedItem(Name_IsICustomXmlSerialized).Value)
                IsBinarySerialized = GetBooleanString( _
                    n.Attributes.GetNamedItem(Name_IsBinarySerialized).Value)
                IsCollectionNode = GetBooleanString( _
                    n.Attributes.GetNamedItem(Name_IsCollectionNode).Value)
                If Not IsICustomXmlSerialized AndAlso Not IsCollectionNode Then
                    serializedFieldValue = n.InnerText
                Else
                    serializedFieldValue = ""
                End If
                fieldType = GetDeSerializedType(n.Attributes.GetNamedItem(Name_FieldType).Value, _
                    fieldTypeIsTrivial)

                If fieldTypeIsTrivial OrElse fieldValueIsNull OrElse IsBinarySerialized Then
                    For Each f As FieldInfo In FI
                        If fieldName.Trim.ToLower = f.Name.Trim.ToLower AndAlso _
                            f.MemberType = MemberTypes.Field Then

                            Dim FT As Type = f.FieldType

                            If fieldTypeIsTrivial Then
                                f.SetValue(Obj, GetDeSerializedValue(serializedFieldValue, fieldType))
                            ElseIf fieldValueIsNull Then
                                f.SetValue(Obj, Nothing)
                            ElseIf IsBinarySerialized Then
                                f.SetValue(Obj, GetBinaryDeSerializedValue(serializedFieldValue, fieldName))
                            End If

                            Exit For

                        End If
                    Next
                End If

            Next

        End If

        If CallCustomDeSerialize Then

            Dim MI As MethodInfo = Obj.GetType.GetMethod("DeSerialize", BindingFlags.NonPublic _
                Or BindingFlags.Public Or BindingFlags.Instance)
            MI.Invoke(Obj, New Object() {node})

        End If

    End Sub

    Public Sub CustomXmlDeSerializeObj(ByRef Obj As Object, ByVal XmlSource As String)

        If Obj Is Nothing OrElse TypeOf Obj Is DBNull Then Throw _
            New ArgumentNullException("Object with null value cannot be deserialized.")

        Dim result As New XmlDocument()
        result.LoadXml(XmlSource)
        If TypeOf result.FirstChild Is XmlDeclaration Then
            SetValues(Obj, result.ChildNodes(1))
        Else
            SetValues(Obj, result.FirstChild)
        End If

    End Sub

    Public Sub CustomXmlDeSerializeObj(ByVal FileName As String, ByRef Obj As Object)

        If Obj Is Nothing OrElse TypeOf Obj Is DBNull Then Throw _
            New ArgumentNullException("Object with null value cannot be deserialized.")

        If FileName Is Nothing OrElse String.IsNullOrEmpty(FileName.Trim) Then _
            Throw New ArgumentNullException("Path to xml file is not spefified.")

        If Not IO.File.Exists(FileName) Then Throw New IO.FileNotFoundException( _
            "File " & FileName & " not found.")

        Dim d As Xml.XmlDocument = OpenXmlFile(FileName)

        If TypeOf d.FirstChild Is XmlDeclaration Then
            SetValues(Obj, d.ChildNodes(1))
        Else
            SetValues(Obj, d.FirstChild)
        End If

    End Sub

    Public Function GetCollectionNode(ByVal node As XmlNode) As XmlNode
        For Each n As XmlNode In node.ChildNodes
            If GetBooleanString(n.Attributes.GetNamedItem(Name_IsCollectionNode).Value) Then Return n
        Next
        Return Nothing
    End Function

    Public Function GetChildNodeForField(ByVal node As XmlNode, ByVal fieldName As String) As XmlNode
        For Each n As XmlNode In node.ChildNodes
            If n.Attributes.GetNamedItem(Name_FieldName).Value.Trim.ToLower _
                = fieldName.Trim.ToLower Then Return n
        Next
        Return Nothing
    End Function

    Public Function OpenXmlFile(ByVal FileName As String) As XmlDocument

        Dim sett As New Xml.XmlReaderSettings
        sett.NameTable = New Xml.NameTable
        Dim xmlns As New Xml.XmlNamespaceManager(sett.NameTable)
        Dim context As New Xml.XmlParserContext(Nothing, xmlns, "", _
            Xml.XmlSpace.Default, Text.Encoding.UTF8)

        Dim result As New Xml.XmlDocument

        Using r As Xml.XmlReader = Xml.XmlReader.Create(FileName, sett, context)
            result.Load(r)
        End Using

        Return result

    End Function


    Private Function GetNode(ByVal OwnerXmlDocument As XmlDocument, ByVal fieldName As String, _
        ByVal fieldValue As Object, ByVal fieldType As Type) As XmlNode

        If fieldName Is Nothing OrElse String.IsNullOrEmpty(fieldName.Trim) Then _
            Throw New NullReferenceException("Field name cannot be null or empty.")
        If OwnerXmlDocument Is Nothing Then Throw New NullReferenceException( _
            "OwnerXmlDocument cannot be null.")
        If fieldValue Is Nothing AndAlso fieldType Is Nothing Then Throw New NullReferenceException( _
            "Field value and field type cannot be both null.")

        If fieldType Is Nothing Then fieldType = fieldValue.GetType

        Dim TypeIsTrivial As Boolean = IsTypeTrivial(fieldType)
        Dim IsValueNull As Boolean = (fieldValue Is Nothing OrElse TypeOf fieldValue Is DBNull)
        Dim IsICustomXmlSerialized As Boolean = (Not fieldType.GetInterface( _
            "ICustomXmlSerialized", True) Is Nothing AndAlso Not IsValueNull)
        Dim IsBinarySerialized As Boolean = (Not TypeIsTrivial AndAlso _
            Not IsICustomXmlSerialized AndAlso Not IsValueNull)

        Dim result As XmlNode = OwnerXmlDocument.CreateNode(XmlNodeType.Element, _
            Name_Field, "")

        Dim fieldNameNode As XmlAttribute = DirectCast(OwnerXmlDocument.CreateNode( _
            XmlNodeType.Attribute, Name_FieldName, ""), XmlAttribute)
        Dim fieldTypeNode As XmlAttribute = DirectCast(OwnerXmlDocument.CreateNode(XmlNodeType.Attribute, _
            Name_FieldType, ""), XmlAttribute)
        Dim fieldTypeIsTrivialNode As XmlAttribute = DirectCast(OwnerXmlDocument.CreateNode(XmlNodeType.Attribute, _
            Name_FieldTypeIsTrivial, ""), XmlAttribute)
        Dim fieldValueIsNullNode As XmlAttribute = DirectCast(OwnerXmlDocument.CreateNode(XmlNodeType.Attribute, _
            Name_FieldValueIsNull, ""), XmlAttribute)
        Dim fieldIsICustomXmlSerialized As XmlAttribute = DirectCast(OwnerXmlDocument.CreateNode(XmlNodeType.Attribute, _
            Name_IsICustomXmlSerialized, ""), XmlAttribute)
        Dim fieldIsBinarySerialized As XmlAttribute = DirectCast(OwnerXmlDocument.CreateNode(XmlNodeType.Attribute, _
            Name_IsBinarySerialized, ""), XmlAttribute)
        Dim fieldIsCollectionNode As XmlAttribute = DirectCast(OwnerXmlDocument.CreateNode(XmlNodeType.Attribute, _
            Name_IsCollectionNode, ""), XmlAttribute)

        fieldNameNode.Value = fieldName.Trim
        fieldTypeNode.Value = GetSerializedType(fieldType)
        fieldTypeIsTrivialNode.Value = GetBooleanString(TypeIsTrivial)
        fieldIsICustomXmlSerialized.Value = GetBooleanString(IsICustomXmlSerialized)
        fieldIsBinarySerialized.Value = GetBooleanString(IsBinarySerialized)
        fieldValueIsNullNode.Value = GetBooleanString(IsValueNull)
        fieldIsCollectionNode.Value = GetBooleanString(False)

        If IsValueNull AndAlso TypeIsTrivial Then
            fieldValue = GetSerializedNullValue(fieldType)
        ElseIf IsValueNull Then
            fieldValue = Nothing
        End If

        If TypeIsTrivial Then
            result.InnerText = GetSerializedValue(fieldValue, fieldType)
        ElseIf IsValueNull Then
            result.InnerText = ValueKey_Null
        ElseIf IsBinarySerialized Then
            result.InnerText = GetBinarySerializedValue(fieldValue)
        End If

        result.Attributes.Append(fieldNameNode)
        result.Attributes.Append(fieldTypeNode)
        result.Attributes.Append(fieldTypeIsTrivialNode)
        result.Attributes.Append(fieldValueIsNullNode)
        result.Attributes.Append(fieldIsICustomXmlSerialized)
        result.Attributes.Append(fieldIsBinarySerialized)
        result.Attributes.Append(fieldIsCollectionNode)

        If IsICustomXmlSerialized Then

            If DirectCast(fieldValue, ICustomXmlSerialized).IsSerializedCollection Then
                result.AppendChild(GetCollectionNode(OwnerXmlDocument, fieldValue, fieldName))
            End If

            Dim MI As MethodInfo = fieldType.GetMethod("Serialize", BindingFlags.NonPublic _
                Or BindingFlags.Public Or BindingFlags.Instance)
            MI.Invoke(fieldValue, New Object() {result})

        End If

        Return result

    End Function

    Private Function GetCollectionNode(ByVal OwnerXmlDocument As XmlDocument, _
        ByVal fieldValue As Object, ByVal fieldName As String) As XmlNode

        If fieldValue Is Nothing OrElse TypeOf fieldValue Is DBNull Then _
            Throw New ArgumentNullException("Null collection cannot be serialized.")

        If Not TypeOf fieldValue Is ICustomXmlSerialized Then _
            Throw New ArgumentNullException("Collection, that does not implement " _
            & "ICustomXmlSerialized, cannot be serialized")

        If Not DirectCast(fieldValue, ICustomXmlSerialized).IsSerializedCollection Then _
            Throw New ArgumentException("Object " & fieldName.Trim & " of type " _
            & fieldValue.GetType.FullName & " is not marked as collection.")

        Dim result As XmlNode = OwnerXmlDocument.CreateNode(XmlNodeType.Element, _
            Name_Field, "")

        Dim fieldNameNode As XmlAttribute = DirectCast(OwnerXmlDocument.CreateNode(XmlNodeType.Attribute, _
            Name_FieldName, ""), XmlAttribute)
        Dim fieldTypeNode As XmlAttribute = DirectCast(OwnerXmlDocument.CreateNode(XmlNodeType.Attribute, _
            Name_FieldType, ""), XmlAttribute)
        Dim fieldTypeIsTrivialNode As XmlAttribute = DirectCast(OwnerXmlDocument.CreateNode(XmlNodeType.Attribute, _
            Name_FieldTypeIsTrivial, ""), XmlAttribute)
        Dim fieldValueIsNullNode As XmlAttribute = DirectCast(OwnerXmlDocument.CreateNode(XmlNodeType.Attribute, _
            Name_FieldValueIsNull, ""), XmlAttribute)
        Dim fieldIsICustomXmlSerialized As XmlAttribute = DirectCast(OwnerXmlDocument.CreateNode(XmlNodeType.Attribute, _
            Name_IsICustomXmlSerialized, ""), XmlAttribute)
        Dim fieldIsBinarySerialized As XmlAttribute = DirectCast(OwnerXmlDocument.CreateNode(XmlNodeType.Attribute, _
            Name_IsBinarySerialized, ""), XmlAttribute)
        Dim fieldIsCollectionNode As XmlAttribute = DirectCast(OwnerXmlDocument.CreateNode(XmlNodeType.Attribute, _
            Name_IsCollectionNode, ""), XmlAttribute)

        fieldNameNode.Value = fieldName.Trim
        fieldTypeNode.Value = fieldValue.GetType.FullName
        fieldTypeIsTrivialNode.Value = GetBooleanString(False)
        fieldIsICustomXmlSerialized.Value = GetBooleanString(True)
        fieldIsBinarySerialized.Value = GetBooleanString(False)
        fieldValueIsNullNode.Value = GetBooleanString(False)
        fieldIsCollectionNode.Value = GetBooleanString(True)

        result.Attributes.Append(fieldNameNode)
        result.Attributes.Append(fieldTypeNode)
        result.Attributes.Append(fieldTypeIsTrivialNode)
        result.Attributes.Append(fieldValueIsNullNode)
        result.Attributes.Append(fieldIsICustomXmlSerialized)
        result.Attributes.Append(fieldIsBinarySerialized)
        result.Attributes.Append(fieldIsCollectionNode)

        Return result

    End Function


    Private Function GetSerializedValue(ByVal Obj As Object, ByVal ObjType As Type) As String

        If Obj Is Nothing Then Return GetSerializedNullValue(ObjType)

        Dim InvCult As System.Globalization.CultureInfo = _
            System.Globalization.CultureInfo.InvariantCulture

        If TypeOf Obj Is String Then
            Return DirectCast(Obj, String)
        ElseIf TypeOf Obj Is Double Then
            Return Convert.ToString(DirectCast(Obj, Double), InvCult)
        ElseIf TypeOf Obj Is Byte() Then
            Return ConvertByteArrayStringUTF8(DirectCast(Obj, Byte()))
        ElseIf TypeOf Obj Is Boolean Then
            Return Convert.ToString(DirectCast(Obj, Boolean), InvCult)
        ElseIf TypeOf Obj Is Byte Then
            Return Convert.ToString(DirectCast(Obj, Byte), InvCult)
        ElseIf TypeOf Obj Is SByte Then
            Return Convert.ToString(DirectCast(Obj, SByte), InvCult)
        ElseIf TypeOf Obj Is Char Then
            Return Convert.ToString(DirectCast(Obj, Char), InvCult)
        ElseIf TypeOf Obj Is Short Then
            Return Convert.ToString(DirectCast(Obj, Short), InvCult)
        ElseIf TypeOf Obj Is UShort Then
            Return Convert.ToString(DirectCast(Obj, UShort), InvCult)
        ElseIf TypeOf Obj Is Integer Then
            Return Convert.ToString(DirectCast(Obj, Integer), InvCult)
        ElseIf TypeOf Obj Is UInteger Then
            Return Convert.ToString(DirectCast(Obj, UInteger), InvCult)
        ElseIf TypeOf Obj Is Long Then
            Return Convert.ToString(DirectCast(Obj, Long), InvCult)
        ElseIf TypeOf Obj Is ULong Then
            Return Convert.ToString(DirectCast(Obj, ULong), InvCult)
        ElseIf TypeOf Obj Is Single Then
            Return Convert.ToString(DirectCast(Obj, Single), InvCult)
        ElseIf TypeOf Obj Is Decimal Then
            Return Convert.ToString(DirectCast(Obj, Decimal), InvCult)
        ElseIf TypeOf Obj Is Date Then
            Return Convert.ToString(DirectCast(Obj, Date), InvCult)
        Else
            Throw New NotImplementedException("Custom XML serialization of type " _
                & Obj.GetType.FullName & " not implemented.")
        End If

    End Function

    Private Function GetSerializedNullValue(ByVal ObjType As Type) As String

        Dim InvCult As System.Globalization.CultureInfo = _
            System.Globalization.CultureInfo.InvariantCulture

        If ObjType Is GetType(String) Then
            Return DirectCast(GetTrivialTypeNullValue(ObjType), String)
        ElseIf ObjType Is GetType(Double) Then
            Return Convert.ToString(DirectCast(GetTrivialTypeNullValue(ObjType), Double), InvCult)
        ElseIf ObjType Is GetType(Byte()) Then
            Return DirectCast("", String)
        ElseIf ObjType Is GetType(Boolean) Then
            Return Convert.ToString(DirectCast(GetTrivialTypeNullValue(ObjType), Boolean), InvCult)
        ElseIf ObjType Is GetType(Byte) Then
            Return Convert.ToString(DirectCast(GetTrivialTypeNullValue(ObjType), Byte), InvCult)
        ElseIf ObjType Is GetType(SByte) Then
            Return Convert.ToString(DirectCast(GetTrivialTypeNullValue(ObjType), SByte), InvCult)
        ElseIf ObjType Is GetType(Char) Then
            Return Convert.ToString(DirectCast(GetTrivialTypeNullValue(ObjType), Char), InvCult)
        ElseIf ObjType Is GetType(Short) Then
            Return Convert.ToString(DirectCast(GetTrivialTypeNullValue(ObjType), Short), InvCult)
        ElseIf ObjType Is GetType(UShort) Then
            Return Convert.ToString(DirectCast(GetTrivialTypeNullValue(ObjType), UShort), InvCult)
        ElseIf ObjType Is GetType(Integer) Then
            Return Convert.ToString(DirectCast(GetTrivialTypeNullValue(ObjType), Integer), InvCult)
        ElseIf ObjType Is GetType(UInteger) Then
            Return Convert.ToString(DirectCast(GetTrivialTypeNullValue(ObjType), UInteger), InvCult)
        ElseIf ObjType Is GetType(Long) Then
            Return Convert.ToString(DirectCast(GetTrivialTypeNullValue(ObjType), Long), InvCult)
        ElseIf ObjType Is GetType(ULong) Then
            Return Convert.ToString(DirectCast(GetTrivialTypeNullValue(ObjType), ULong), InvCult)
        ElseIf ObjType Is GetType(Single) Then
            Return Convert.ToString(DirectCast(GetTrivialTypeNullValue(ObjType), Single), InvCult)
        ElseIf ObjType Is GetType(Decimal) Then
            Return Convert.ToString(DirectCast(GetTrivialTypeNullValue(ObjType), Decimal), InvCult)
        ElseIf ObjType Is GetType(Date) Then
            Return Convert.ToString(DirectCast(GetTrivialTypeNullValue(ObjType), Date), InvCult)
        Else
            Throw New NotImplementedException("Custom XML serialization of type " _
                & ObjType.GetType.FullName & " not implemented.")
        End If

    End Function

    Private Function GetTrivialTypeNullValue(ByVal ObjType As Type) As Object

        If ObjType Is GetType(String) Then
            Return DirectCast("", String)
        ElseIf ObjType Is GetType(Double) Then
            Return DirectCast(0.0, Double)
        ElseIf ObjType Is GetType(Byte()) Then
            Return Nothing
        ElseIf ObjType Is GetType(Boolean) Then
            Return False
        ElseIf ObjType Is GetType(Byte) Then
            Return Convert.ToByte(0)
        ElseIf ObjType Is GetType(SByte) Then
            Return Convert.ToSByte(0)
        ElseIf ObjType Is GetType(Char) Then
            Return Convert.ToChar("")
        ElseIf ObjType Is GetType(Short) Then
            Dim r As Short = 0
            Return r
        ElseIf ObjType Is GetType(UShort) Then
            Dim r As UShort = 0
            Return r
        ElseIf ObjType Is GetType(Integer) Then
            Return DirectCast(0, Integer)
        ElseIf ObjType Is GetType(UInteger) Then
            Dim r As UInteger = 0
            Return r
        ElseIf ObjType Is GetType(Long) Then
            Dim r As Long = 0
            Return r
        ElseIf ObjType Is GetType(ULong) Then
            Dim r As ULong = 0
            Return r
        ElseIf ObjType Is GetType(Single) Then
            Return Convert.ToSingle(0)
        ElseIf ObjType Is GetType(Decimal) Then
            Return Convert.ToDecimal(0)
        ElseIf ObjType Is GetType(Date) Then
            Return Date.MinValue
        Else
            Throw New NotImplementedException("Custom XML serialization of type " _
                & ObjType.GetType.FullName & " not implemented.")
        End If

    End Function

    Private Function GetBinarySerializedValue(ByVal fieldValue As Object) As String

        If fieldValue Is Nothing Then Throw New ArgumentNullException( _
            "Object for binary serialization cannot be null.")
        Try
            Using MS As New IO.MemoryStream()
                Dim BF As New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter
                BF.Serialize(MS, fieldValue)
                Dim result As String = ConvertByteArrayStringUTF8(MS.ToArray)
                MS.Close()
                Return result
            End Using
        Catch ex As Exception
            If ex.Message.ToLower.Contains("utf8") Then Throw ex
            Throw New Exception("Binary serialization of type " _
                & fieldValue.GetType.FullName & " failed. " & ex.Message, ex)
        End Try

    End Function

    Private Function GetBinaryDeSerializedValue(ByVal binarySerializedString As String, _
        ByVal fieldName As String) As Object

        If binarySerializedString Is Nothing OrElse _
            String.IsNullOrEmpty(binarySerializedString.Trim) Then Return Nothing

        Try
            Using MS As New IO.MemoryStream(ConvertByteArrayStringUTF8(binarySerializedString))
                Dim BF As New System.Runtime.Serialization.Formatters.Binary.BinaryFormatter
                Dim result As Object = BF.Deserialize(MS)
                MS.Close()
                Return result
            End Using
        Catch ex As Exception
            If ex.Message.ToLower.Contains("utf8") Then Throw ex
            Throw New Exception("Binary deserialization of field " _
                & fieldName & " failed. " & ex.Message, ex)
        End Try

    End Function

    Private Function ConvertByteArrayStringUTF8(ByVal byteArray As Byte()) As String

        Try
            Dim encoder As New System.Text.UTF8Encoding
            Dim utf8Decode As System.Text.Decoder = encoder.GetDecoder()

            Dim charCount As Integer = utf8Decode.GetCharCount(byteArray, 0, byteArray.Length)
            Dim decoded_char(charCount) As Char
            utf8Decode.GetChars(byteArray, 0, byteArray.Length, decoded_char, 0)

            Return New String(decoded_char)

        Catch ex As Exception
            Throw New Exception("Binary serialization failed when converting byte() to UTF8 string.", ex)
        End Try

    End Function

    Private Function ConvertByteArrayStringUTF8(ByVal byteString As String) As Byte()
        Try
            Return System.Text.Encoding.UTF8.GetBytes(byteString)
        Catch ex As Exception
            Throw New Exception("Binary deserialization failed when converting UTF8 string to byte().", ex)
        End Try
    End Function

    Private Function GetSerializedType(ByVal ObjType As Type) As String

        'If ObjType Is GetType(String) OrElse ObjType Is GetType(Double) _
        '    OrElse ObjType Is GetType(Byte()) OrElse ObjType Is GetType(Boolean) _
        '    OrElse ObjType Is GetType(Byte) OrElse ObjType Is GetType(SByte) _
        '    OrElse ObjType Is GetType(Char) OrElse ObjType Is GetType(Short) _
        '    OrElse ObjType Is GetType(UShort) OrElse ObjType Is GetType(Integer) _
        '    OrElse ObjType Is GetType(UInteger) OrElse ObjType Is GetType(Long) _
        '    OrElse ObjType Is GetType(ULong) OrElse ObjType Is GetType(Single) _
        '    OrElse ObjType Is GetType(Decimal) OrElse ObjType Is GetType(Date) Then

        '    Return ObjType.Name

        'End If

        Return ObjType.FullName

    End Function

    Private Function IsTypeTrivial(ByVal ObjType As Type) As Boolean

        Return (ObjType Is GetType(String) OrElse ObjType Is GetType(Double) _
            OrElse ObjType Is GetType(Byte()) OrElse ObjType Is GetType(Boolean) _
            OrElse ObjType Is GetType(Byte) OrElse ObjType Is GetType(SByte) _
            OrElse ObjType Is GetType(Char) OrElse ObjType Is GetType(Short) _
            OrElse ObjType Is GetType(UShort) OrElse ObjType Is GetType(Integer) _
            OrElse ObjType Is GetType(UInteger) OrElse ObjType Is GetType(Long) _
            OrElse ObjType Is GetType(ULong) OrElse ObjType Is GetType(Single) _
            OrElse ObjType Is GetType(Decimal) OrElse ObjType Is GetType(Date))

    End Function

    Private Function IsTypeTrivial(ByVal typeString As String) As Boolean

        Return (GetType(String).Name.Trim.ToLower = typeString.Trim.ToLower _
            OrElse GetType(Double).Name.Trim.ToLower = typeString.Trim.ToLower _
            OrElse GetType(Byte()).Name.Trim.ToLower = typeString.Trim.ToLower _
            OrElse GetType(Boolean).Name.Trim.ToLower = typeString.Trim.ToLower _
            OrElse GetType(Byte).Name.Trim.ToLower = typeString.Trim.ToLower _
            OrElse GetType(SByte).Name.Trim.ToLower = typeString.Trim.ToLower _
            OrElse GetType(Char).Name.Trim.ToLower = typeString.Trim.ToLower _
            OrElse GetType(Short).Name.Trim.ToLower = typeString.Trim.ToLower _
            OrElse GetType(UShort).Name.Trim.ToLower = typeString.Trim.ToLower _
            OrElse GetType(Integer).Name.Trim.ToLower = typeString.Trim.ToLower _
            OrElse GetType(UInteger).Name.Trim.ToLower = typeString.Trim.ToLower _
            OrElse GetType(Long).Name.Trim.ToLower = typeString.Trim.ToLower _
            OrElse GetType(ULong).Name.Trim.ToLower = typeString.Trim.ToLower _
            OrElse GetType(Single).Name.Trim.ToLower = typeString.Trim.ToLower _
            OrElse GetType(Decimal).Name.Trim.ToLower = typeString.Trim.ToLower _
            OrElse GetType(Date).Name.Trim.ToLower = typeString.Trim.ToLower)

    End Function

    Private Function GetDeSerializedValue(ByVal value As String, ByVal ObjType As Type) As Object

        If value Is Nothing Then value = ""

        Dim InvCult As System.Globalization.CultureInfo = _
            System.Globalization.CultureInfo.InvariantCulture

        If ObjType Is GetType(String) Then
            Return value.Trim
        ElseIf ObjType Is GetType(Double) Then
            Return Convert.ToDouble(value.Trim, InvCult)
        ElseIf ObjType Is GetType(Byte()) Then
            If String.IsNullOrEmpty(value.Trim) Then Return Nothing
            Return ConvertByteArrayStringUTF8(value)
        ElseIf ObjType Is GetType(Boolean) Then
            Return Convert.ToBoolean(value.Trim, InvCult)
        ElseIf ObjType Is GetType(Byte) Then
            Return Convert.ToByte(value.Trim, InvCult)
        ElseIf ObjType Is GetType(SByte) Then
            Return Convert.ToSByte(value.Trim, InvCult)
        ElseIf ObjType Is GetType(Char) Then
            Return Convert.ToChar(value.Trim, InvCult)
        ElseIf ObjType Is GetType(Short) Then
            Return Convert.ToInt16(value, InvCult)
        ElseIf ObjType Is GetType(UShort) Then
            Return Convert.ToInt16(value, InvCult)
        ElseIf ObjType Is GetType(Integer) Then
            Return Convert.ToInt32(value, InvCult)
        ElseIf ObjType Is GetType(UInteger) Then
            Return Convert.ToInt32(value, InvCult)
        ElseIf ObjType Is GetType(Long) Then
            Return Convert.ToInt64(value, InvCult)
        ElseIf ObjType Is GetType(ULong) Then
            Return Convert.ToInt64(value, InvCult)
        ElseIf ObjType Is GetType(Single) Then
            Return Convert.ToSingle(value, InvCult)
        ElseIf ObjType Is GetType(Decimal) Then
            Return Convert.ToDecimal(value, InvCult)
        ElseIf ObjType Is GetType(Date) Then
            Return Convert.ToDateTime(value, InvCult)
        Else
            Throw New NotImplementedException("Custom XML deserialization of type " _
                & ObjType.GetType.FullName & " not implemented.")
        End If

    End Function

    Private Function GetDeSerializedType(ByVal typeString As String, ByVal isTrivialType As Boolean) As Type

        Return Type.GetType(typeString.Trim, isTrivialType, True)

    End Function

    Private Function GetBooleanString(ByVal value As Boolean) As String
        Return Convert.ToString(value, System.Globalization.CultureInfo.InvariantCulture)
    End Function

    Private Function GetBooleanString(ByVal value As String) As Boolean
        Return Convert.ToBoolean(value, System.Globalization.CultureInfo.InvariantCulture)
    End Function

End Module