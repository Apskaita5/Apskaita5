
Imports System.Windows.Forms
Imports System.Drawing

''' <summary>
''' A control that displays a busy animation and blocks the parent form
''' while an async method is executed.
''' </summary>
''' <remarks></remarks>
Public Class ProgressFiller

    Private WithEvents _Operation As Object = Nothing
    Private _Result As Object
    Private _Exception As Exception
    Private _IsRunning As Boolean = False
    Private _Initialized As Boolean = False

    Public Event AsyncOperationCompleted As EventHandler


    ''' <summary>
    ''' Gets a result of the last operation (if any).
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly Property Result() As Object
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            Return _Result
        End Get
    End Property

    ''' <summary>
    ''' Gets an exception that occured during the last operation (if any).
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly Property [Exception]() As Exception
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            Return _Exception
        End Get
    End Property

    ''' <summary>
    ''' Whether an operation is currently in progress.
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly Property IsRunning() As Boolean
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            Return _IsRunning
        End Get
    End Property


    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.SetStyle(ControlStyles.Opaque, True)
        Me.Visible = False

    End Sub


    ''' <summary>
    ''' Executes a method specified async, displays a busy animation 
    ''' and blocks the parent form while the method is executed
    ''' </summary>
    ''' <typeparam name="T">a class which method should be executed async</typeparam>
    ''' <param name="instance">an instance of class T to execute the method on 
    ''' (if the method is static then null)</param>
    ''' <param name="methodName">a name of the method to execute</param>
    ''' <param name="allowCancel">whether to allow the user to cancel 
    ''' the method execution</param>
    ''' <param name="methodParams">params for the method to execute</param>
    ''' <remarks>Raises <see cref="AsyncOperationCompleted">AsyncOperationCompleted</see>
    ''' when the method execution is completed or canceled.</remarks>
    Public Sub RunOperationAsync(Of T)(ByVal instance As T, ByVal methodName As String, _
        ByVal allowCancel As Boolean, ByVal ParamArray methodParams As Object())

        If Not _Initialized Then
            Dim parentFrm As Form = Me.ParentForm
            AddHandler parentFrm.SizeChanged, AddressOf ParentForm_SizeChanged
            _Initialized = True
        End If

        Me.Location = New Point(0, 0)
        Me.Visible = True
        Me.Size = Me.ParentForm.ClientSize
        Me.BringToFront()
        Me.Label1.Visible = allowCancel

        _Result = Nothing
        _Exception = Nothing

        Dim operation As New MethodInvoker(Of T)
        AddHandler operation.FetchCompleted, AddressOf OperationCompleted
        _Operation = operation

        Me.Focus()

        _IsRunning = True

        operation.InvokeAsync(instance, methodName, allowCancel, methodParams)

    End Sub


    Private Sub OperationCompleted(ByVal sender As Object, ByVal e As EventArgs)

        If _Operation Is Nothing Then Exit Sub

        _Result = DirectCast(e, AsyncResult).ObjectInstance
        _Exception = DirectCast(e, AsyncResult).Exception

        Me.Visible = False
        Me.SendToBack()
        _Operation = Nothing

        _IsRunning = False

        RaiseEvent AsyncOperationCompleted(Me, New EventArgs)

    End Sub


    Private Sub ProgressFiller_SizeChanged(ByVal sender As Object, _
        ByVal e As System.EventArgs) Handles Me.SizeChanged

        Dim xCoord As Integer = ((Me.Size.Width / 2) - (PictureBox1.Size.Width / 2))
        If xCoord < 0 Then xCoord = 0

        Dim yCoord As Integer = ((Me.Size.Height / 2) - (PictureBox1.Size.Height / 2))
        If yCoord < 0 Then yCoord = 0

        PictureBox1.Location = New System.Drawing.Point(xCoord, yCoord)
        Label1.Location = New System.Drawing.Point(xCoord + 3, yCoord + 213)

        Me.Invalidate()

    End Sub

    Private Sub ParentForm_SizeChanged(ByVal sender As Object, ByVal e As EventArgs)
        If Not Me.Visible Then Exit Sub
        Me.Size = Me.ParentForm.ClientSize
    End Sub

    Private Sub ProgressFiller_KeyDown(ByVal sender As Object, _
        ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown

        If e.KeyData <> Keys.Escape Then Exit Sub

        e.Handled = True

        CancelProgress()

    End Sub


    Public Sub CancelProgress()

        If _Operation Is Nothing Then Exit Sub

        _Operation.Cancel()

    End Sub

End Class
