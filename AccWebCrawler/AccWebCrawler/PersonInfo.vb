Public Class PersonInfo

    Private _Code As String = ""
    Private _Name As String = ""
    Private _Address As String = ""
    Private _VatCode As String = ""
    Private _Message As String = ""


    Public ReadOnly Property Code() As String
        Get
            Return _Code.Trim
        End Get
    End Property

    Public Property Name() As String
        Get
            Return _Name.Trim
        End Get
        Friend Set(ByVal value As String)
            If value Is Nothing Then value = ""
            _Name = value.Trim
        End Set
    End Property

    Public Property Address() As String
        Get
            Return _Address.Trim
        End Get
        Friend Set(ByVal value As String)
            If value Is Nothing Then value = ""
            _Address = value.Trim
        End Set
    End Property

    Public Property VatCode() As String
        Get
            Return _VatCode.Trim
        End Get
        Friend Set(ByVal value As String)
            If value Is Nothing Then value = ""
            _VatCode = value.Trim
        End Set
    End Property

    Public Property Message() As String
        Get
            Return _Message.Trim
        End Get
        Friend Set(ByVal value As String)
            If value Is Nothing Then value = ""
            _Message = value.Trim
        End Set
    End Property


    Public Sub New(ByVal nCode As String)
        If nCode Is Nothing Then nCode = ""
        _Code = nCode.Trim
    End Sub

End Class
