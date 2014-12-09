Public Interface ICustomXmlSerialized

    Sub Serialize(ByRef node As System.Xml.XmlNode)

    Sub DeSerialize(ByVal node As System.Xml.XmlNode)

    Function IsSerializedCollection() As Boolean

End Interface
