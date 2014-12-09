Public Class F_AsyncLoad

    Private WithEvents _Operation As Object
    Private _Result As Object
    Private _Exception As Exception

    Public ReadOnly Property Result() As Object
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            Return _Result
        End Get
    End Property

    Public ReadOnly Property [Exception]() As Exception
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            Return _Exception
        End Get
    End Property


    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

    End Sub


    Public Sub RunAsyncOperation(Of T)(ByVal nObjectInstance As T, ByVal nMethodName As String, _
        ByVal nAllowCancel As Boolean, ByVal ParamArray nMethodParamArray As Object())

        Dim Operation As New AsyncOperation(Of T)
        AddHandler Operation.FetchCompleted, AddressOf OperationCompleted
        _Operation = Operation
        Operation.BeginInvoke(nObjectInstance, nMethodName, nAllowCancel, nMethodParamArray)

    End Sub

    Private Sub OperationCompleted(ByVal sender As Object, ByVal e As EventArgs)
        _Result = DirectCast(e, AsyncResult).ObjectInstance
        _Exception = DirectCast(e, AsyncResult).Exception
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub CancelButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles CancelButton.Click

        If _Operation.Cancel() Then
            _Result = Nothing
            _Exception = New Exception("Klaida. Operacija buvo atšaukta vartotojo.")
            Me.Hide()
            Me.Close()
        End If

    End Sub

    Private Sub F_AsyncLoad_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Deactivate
        Me.Activate()
    End Sub

End Class