Namespace Settings.XmlProxies

    <Serializable()> _
    Public Class CodeProxy

        Private _Type As CodeType = CodeType.GpmDeclaration
        Private _Code As Integer = 0
        Private _Name As String = ""
        Private _IsObsolete As Boolean = False

        Public Property [Type]() As CodeType
            Get
                Return _Type
            End Get
            Set(ByVal value As CodeType)
                If _Type <> value Then
                    _Type = value
                End If
            End Set
        End Property

        Public Property Code() As Integer
            Get
                Return _Code
            End Get
            Set(ByVal value As Integer)
                If _Code <> value Then
                    _Code = value
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

        Public Property IsObsolete() As Boolean
            Get
                Return _IsObsolete
            End Get
            Set(ByVal value As Boolean)
                If _IsObsolete <> value Then
                    _IsObsolete = value
                End If
            End Set
        End Property

    End Class

End Namespace