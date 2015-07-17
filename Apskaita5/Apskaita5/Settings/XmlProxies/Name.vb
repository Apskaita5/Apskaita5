Namespace Settings.XmlProxies

    <Serializable()> _
    Public Class Name

        Private _Type As NameType = NameType.SodraBranch
        Private _Name As String = ""

        Public Property [Type]() As NameType
            Get
                Return _Type
            End Get
            Set(ByVal value As NameType)
                If _Type <> value Then
                    _Type = value
                End If
            End Set
        End Property

        Public Property Name() As String
            Get
                Return _Name
            End Get
            Set(ByVal value As String)
                If value Is Nothing Then value = ""
                If _Name.Trim <> value.Trim Then
                    _Name = value.Trim
                End If
            End Set
        End Property

    End Class

End Namespace