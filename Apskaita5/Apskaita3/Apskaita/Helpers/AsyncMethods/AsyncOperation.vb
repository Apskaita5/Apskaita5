Imports System.Reflection
Public Class AsyncOperation(Of T)

    Private BW As System.ComponentModel.BackgroundWorker = Nothing

    ''' <summary>
    ''' Event raised when the operation has completed.
    ''' </summary>
    ''' <remarks>
    ''' <para>
    ''' If your application is running in WPF, this event
    ''' will be raised on the UI thread automatically.
    ''' </para><para>
    ''' If your application is running in Windows Forms,
    ''' this event will be raised on a background thread.
    ''' If you also set DataPortal.SynchronizationObject
    ''' to a Windows Forms form or control, then the event
    ''' will be raised on the UI thread automatically.
    ''' </para><para>
    ''' In any other environment (such as ASP.NET), this
    ''' event will be raised on a background thread.
    ''' </para>
    ''' </remarks>
    Public Event FetchCompleted As EventHandler

    ''' <summary>
    ''' Raises the event.
    ''' </summary>
    ''' <param name="e">
    ''' The parameter provided to the event handler.
    ''' </param>
    ''' <remarks>
    ''' <para>
    ''' If your application is running in WPF, this event
    ''' will be raised on the UI thread automatically.
    ''' </para><para>
    ''' If your application is running in Windows Forms,
    ''' this event will be raised on a background thread.
    ''' If you also set DataPortal.SynchronizationObject
    ''' to a Windows Forms form or control, then the event
    ''' will be raised on the UI thread automatically.
    ''' </para><para>
    ''' In any other environment (such as ASP.NET), this
    ''' event will be raised on a background thread.
    ''' </para>
    ''' </remarks>
    Protected Overridable Sub OnFetchCompleted(ByVal e As AsyncResult)
        RaiseEvent FetchCompleted(Me, e)
    End Sub

    Public Sub BeginInvoke(ByVal nObjectInstance As T, ByVal nMethodName As String, _
        ByVal nAllowCancel As Boolean, ByVal ParamArray nMethodParamArray As Object())

        If Not BW Is Nothing Then Exit Sub

        BW = New System.ComponentModel.BackgroundWorker()
        BW.WorkerSupportsCancellation = nAllowCancel
        BW.WorkerReportsProgress = False
        AddHandler BW.RunWorkerCompleted, AddressOf Fetch_RunWorkerCompleted
        AddHandler BW.DoWork, AddressOf Fetch_DoWork
        BW.RunWorkerAsync(New AsyncArguments(Of T)(nObjectInstance, nMethodName, nAllowCancel, nMethodParamArray))

    End Sub

    Public Function Cancel() As Boolean
        If BW Is Nothing OrElse Not BW.WorkerSupportsCancellation Then Return False
        BW.CancelAsync()
        Return True
    End Function

    Private Sub Fetch_RunWorkerCompleted(ByVal sender As Object, _
        ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)

        If e.Error Is Nothing Then
            Dim result As AsyncResult = TryCast(e.Result, AsyncResult)
            If result IsNot Nothing Then
                Dim obj As T = Nothing
                If result.ObjectInstance IsNot Nothing Then obj = DirectCast(result.ObjectInstance, T)
                SetThreadContext(result)
                BW.Dispose()
                BW = Nothing
                OnFetchCompleted(New AsyncResult(obj, GetType(T), result.GlobalContext, result.Exception))
                Return
            End If
        End If

        BW.Dispose()
        BW = Nothing
        OnFetchCompleted(New AsyncResult(Nothing, GetType(T), Nothing, e.Error))

    End Sub

    Private Sub Fetch_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs)

        Dim request As AsyncArguments(Of T) = TryCast(e.Argument, AsyncArguments(Of T))

        SetThreadContext(request)

        Dim result As T = Nothing

        Try
            result = DirectCast(GetMethodInfo(GetType(T), (Not request.ObjectInstance Is Nothing), _
                request.MethodName, request.MethodParamArray).Invoke(request.ObjectInstance, _
                request.MethodParamArray), T)
            e.Result = New AsyncResult(result, GetType(T), Csla.ApplicationContext.GlobalContext, Nothing)
        Catch ex As Exception
            If Not ex.InnerException Is Nothing Then
                e.Result = New AsyncResult(Nothing, GetType(T), Csla.ApplicationContext.GlobalContext, _
                    New Exception(ex.InnerException.Message, ex))
            Else
                e.Result = New AsyncResult(Nothing, GetType(T), Csla.ApplicationContext.GlobalContext, ex)
            End If
        End Try

    End Sub

    

    Public Shared Sub SetThreadContext(ByVal request As IAsyncContext)
        If Not request.Principal Is Nothing Then Csla.ApplicationContext.User = request.Principal
        Dim ContextParams(1) As Object
        If request.ClientContext Is Nothing Then
            ContextParams(0) = Csla.ApplicationContext.ClientContext
        Else
            ContextParams(0) = request.ClientContext
        End If
        ContextParams(1) = request.GlobalContext
        GetType(Csla.ApplicationContext).GetMethod("SetContext", BindingFlags.NonPublic _
            OrElse BindingFlags.Static).Invoke(Nothing, ContextParams)
    End Sub

End Class
