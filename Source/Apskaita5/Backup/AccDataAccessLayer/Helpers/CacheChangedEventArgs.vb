Public Class CacheChangedEventArgs
    Inherits EventArgs

    Private _Type As Type

    Public ReadOnly Property [Type]() As Type
        Get
            Return _Type
        End Get
    End Property

    Public Sub New(ByVal nType As Type)
        _Type = nType
    End Sub

    Private Sub New()

    End Sub

End Class
