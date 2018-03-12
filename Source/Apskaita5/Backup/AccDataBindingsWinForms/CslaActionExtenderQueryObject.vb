Imports System.Windows.Forms
Imports AccControlsWinForms

''' <summary>
''' Extender control providing automation around
''' fetching CSLA .NET readonly business objects (query objects).
''' </summary>
''' <remarks>A progress control for queries cannot be the same as the one
''' for a CslaActionExtenderReportForm or a CslaActionExtenderEditForm.</remarks>
Public Class CslaActionExtenderQueryObject

    Public Delegate Sub QueryObjectFetched(ByVal result As Object, ByVal exceptionHandled As Boolean)

    Private _ParentForm As Form = Nothing
    Private _ProgressControl As ProgressFiller = Nothing
    Private _CallBackDelegate As QueryObjectFetched = Nothing


    ''' <summary>
    ''' Creates a new CslaActionExtenderQueryObject instance.
    ''' </summary>
    ''' <param name="parentForm">a parent form that is managed by the instance</param>
    ''' <param name="progressControl">a progress control for fetching a report async</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal parentForm As Form, ByVal progressControl As ProgressFiller)

        If parentForm Is Nothing Then
            Throw New ArgumentNullException("parentForm")
        ElseIf progressControl Is Nothing Then
            Throw New ArgumentNullException("progressControl")
        End If

        _ParentForm = parentForm
        _ProgressControl = progressControl

        AddHandler progressControl.AsyncOperationCompleted, AddressOf AsyncOperationCompleted
        AddHandler parentForm.FormClosing, AddressOf Form_FormClosing

    End Sub

    Private Sub New()
        ' do not allow creation of an empty instance
    End Sub


    Private Sub Form_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs)

        Try
            RemoveHandler _ParentForm.FormClosing, AddressOf Form_FormClosing
        Catch ex As Exception
        End Try
        Try
            RemoveHandler _ProgressControl.AsyncOperationCompleted, AddressOf AsyncOperationCompleted
        Catch ex As Exception
        End Try

        _ParentForm = Nothing
        _ProgressControl = Nothing
        _CallBackDelegate = Nothing

    End Sub

    Private Sub AsyncOperationCompleted(ByVal sender As Object, ByVal e As System.EventArgs)

        If Not _ProgressControl.Exception Is Nothing Then

            ShowError(_ProgressControl.Exception)

            If Not _CallBackDelegate Is Nothing Then
                _CallBackDelegate.Invoke(Nothing, True)
            End If

        ElseIf Not _ProgressControl.Result Is Nothing Then

            If Not _CallBackDelegate Is Nothing Then
                _CallBackDelegate.Invoke(_ProgressControl.Result, False)
            End If

        Else

            If Not _CallBackDelegate Is Nothing Then
                _CallBackDelegate.Invoke(Nothing, False)
            End If

        End If

        _CallBackDelegate = Nothing

    End Sub


    ''' <summary>
    ''' Invokes a query object method and returns a result by the callBackDelegate
    ''' provided.
    ''' </summary>
    ''' <typeparam name="T">a type of the class which method should be invoked</typeparam>
    ''' <param name="instance">an instance of class T to execute the method on 
    ''' (if the method is static then null)</param>
    ''' <param name="methodName">a name of the method to invoke</param>
    ''' <param name="allowCancel">whether to allow the user to cancel method execution</param>
    ''' <param name="callBackDelegate">a method that is invoked with the
    ''' result of the method invocation (if any)</param>
    ''' <param name="params">method params (if any)</param>
    ''' <remarks></remarks>
    Public Sub InvokeQuery(Of T)(ByVal instance As T, ByVal methodName As String, _
        ByVal allowCancel As Boolean, ByVal callBackDelegate As QueryObjectFetched, _
        ByVal ParamArray params As Object())

        If methodName Is Nothing OrElse String.IsNullOrEmpty(methodName.Trim) Then
            Throw New ArgumentNullException("methodName")
        End If

        If MyCustomSettings.UseThreadingForDataTransfer Then

            _CallBackDelegate = callBackDelegate

            If params Is Nothing OrElse params.Length < 1 Then
                _ProgressControl.RunOperationAsync(Of T)(instance, methodName, True)
            Else
                _ProgressControl.RunOperationAsync(Of T)(instance, methodName, True, params)
            End If

        Else

            Dim result As Object = Nothing

            Try
                Using busy As New StatusBusy()
                    result = InvokeMethod(Of T)(instance, methodName, params)
                End Using
            Catch ex As Exception
                ShowError(ex)
                If Not callBackDelegate Is Nothing Then
                    callBackDelegate.Invoke(Nothing, True)
                End If
                Exit Sub
            End Try

            If Not callBackDelegate Is Nothing Then
                callBackDelegate.Invoke(result, False)
            End If

        End If

    End Sub

End Class