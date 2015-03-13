Public Class DelegateContainer(Of T)

    Public Delegate Sub ProcessSelectedItemDelegate(ByVal selectedItem As T)

    Private _Delegate As ProcessSelectedItemDelegate = Nothing

    Public Sub New(ByVal delegateToStore As ProcessSelectedItemDelegate)
        If delegateToStore Is Nothing Then Throw New ArgumentNullException("delegateToStore")
        _Delegate = delegateToStore
    End Sub

    Private Sub New()

    End Sub

    Public Sub Invoke(ByVal selectedItem As T)
        If Not _Delegate Is Nothing Then _Delegate.Invoke(selectedItem)
    End Sub

End Class
