Namespace Settings.XmlProxies

    <Serializable()> _
    Public Class ConventionalAccount

        Private _ID As Long = 0
        Private _Name As String = ""
        Private _FinancialStatementItemName As String = ""
        Private _IsDefaultAccount As Boolean = False
        Private _DefaultAccountType As General.DefaultAccountType = General.DefaultAccountType.Buyers


        Public Property ID() As Long
            Get
                Return _ID
            End Get
            Set(ByVal value As Long)
                If _ID <> value Then
                    _ID = value
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

        Public Property FinancialStatementItemName() As String
            Get
                Return _FinancialStatementItemName
            End Get
            Set(ByVal value As String)
                If value Is Nothing Then value = ""
                If _FinancialStatementItemName.Trim <> value.Trim Then
                    _FinancialStatementItemName = value.Trim
                End If
            End Set
        End Property

        Public Property IsDefaultAccount() As Boolean
            Get
                Return _IsDefaultAccount
            End Get
            Set(ByVal value As Boolean)
                If _IsDefaultAccount <> value Then
                    _IsDefaultAccount = value
                End If
            End Set
        End Property

        Public Property DefaultAccountType() As General.DefaultAccountType
            Get
                Return _DefaultAccountType
            End Get
            Set(ByVal value As General.DefaultAccountType)
                If _DefaultAccountType <> value Then
                    _DefaultAccountType = value
                End If
            End Set
        End Property

    End Class

End Namespace