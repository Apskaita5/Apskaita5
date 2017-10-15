Namespace Extensibility

    <Serializable()> _
    Public Class RestCredentials

        Private _Name As String = ""
        Private _Token As String = ""
        Private _Database As String = ""

        Public Property Name() As String
            Get
                Return _Name
            End Get
            Set(ByVal value As String)
                _Name = value
            End Set
        End Property

        Public Property Token() As String
            Get
                Return _Token
            End Get
            Set(ByVal value As String)
                _Token = value
            End Set
        End Property

        Public Property Database() As String
            Get
                Return _Database
            End Get
            Set(ByVal value As String)
                _Database = value
            End Set
        End Property


        Public Sub New()
        End Sub

    End Class

End Namespace