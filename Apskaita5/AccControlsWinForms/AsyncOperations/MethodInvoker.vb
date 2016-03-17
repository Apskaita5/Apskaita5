Imports System.Reflection

''' <summary>
''' A helper object that allows to execute an arbitrary method async.
''' </summary>
''' <typeparam name="T">a class which method should be executed async</typeparam>
''' <remarks>Encapsulates a <see cref="System.ComponentModel.BackgroundWorker">BackgroundWorker</see>.</remarks>
Friend Class MethodInvoker(Of T)

    Private _Worker As System.ComponentModel.BackgroundWorker = Nothing

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


    ''' <summary>
    ''' Executes an arbitrary method of class <see cref="T">T</see> async.
    ''' </summary>
    ''' <param name="instance">an instance of class <see cref="T">T</see>
    ''' to execute the method on (if the method is static then null)</param>
    ''' <param name="methodName">a name of the method to execute</param>
    ''' <param name="allowCancel">whether to allow the user to cancel 
    ''' the method execution</param>
    ''' <param name="methodParams">params for the method to execute</param>
    ''' <remarks>Raises <see cref="FetchCompleted">FetchCompleted</see>
    ''' when the method execution is completed or canceled.</remarks>
    Public Sub InvokeAsync(ByVal instance As T, ByVal methodName As String, _
        ByVal allowCancel As Boolean, ByVal ParamArray methodParams As Object())

        If Not _Worker Is Nothing Then Exit Sub

        _Worker = New System.ComponentModel.BackgroundWorker()
        _Worker.WorkerSupportsCancellation = allowCancel
        _Worker.WorkerReportsProgress = False

        AddHandler _Worker.RunWorkerCompleted, AddressOf Fetch_RunWorkerCompleted
        AddHandler _Worker.DoWork, AddressOf Fetch_DoWork

        _Worker.RunWorkerAsync(New AsyncArguments(Of T)(instance, methodName, methodParams))

    End Sub

    ''' <summary>
    ''' Executes an arbitrary method of class <see cref="T">T</see> sync.
    ''' </summary>
    ''' <param name="instance">an instance of class <see cref="T">T</see>
    ''' to execute the method on (if the method is static then null)</param>
    ''' <param name="methodName">a name of the method to execute</param>
    ''' <param name="methodParams">params for the method to execute</param>
    ''' <remarks>Raises <see cref="FetchCompleted">FetchCompleted</see>
    ''' when the method execution is completed or canceled.</remarks>
    Public Function Invoke(ByVal instance As T, ByVal methodName As String, _
        ByVal ParamArray methodParams As Object()) As Object

        Dim method As MethodInfo = GetMethodInfo(Of T)((Not instance Is Nothing), _
            methodName, methodParams)

        Return method.Invoke(instance, methodParams)

    End Function

    ''' <summary>
    ''' Cancels current async operation (if any) and returns true on success.
    ''' </summary>
    ''' <remarks>Releases encapsulated background worker. The real operation
    ''' is not (cannot be) canceled, however the operation result is not used anymore.</remarks>
    Public Function Cancel() As Boolean

        If _Worker Is Nothing Then Return True

        If Not _Worker.WorkerSupportsCancellation Then Return False

        Try
            RemoveHandler _Worker.DoWork, AddressOf Fetch_DoWork
        Catch ex As Exception
        End Try
        Try
            RemoveHandler _Worker.RunWorkerCompleted, AddressOf Fetch_RunWorkerCompleted
        Catch ex As Exception
        End Try
        Try
            _Worker.Dispose()
        Catch ex As Exception
        End Try
        _Worker = Nothing

        OnFetchCompleted(New AsyncResult(Nothing, GetType(T), Nothing, _
            New Exception("Operacija buvo atšaukta vartotojo.")))

        Return True

    End Function


    Private Sub Fetch_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs)

        Dim request As AsyncArguments(Of T) = DirectCast(e.Argument, AsyncArguments(Of T))

        SetThreadContext(request)

        Try

            Dim method As MethodInfo = GetMethodInfo(Of T)((Not request.ObjectInstance Is Nothing), _
                request.MethodName, request.MethodParamArray)

            Dim resultType As Type = method.ReturnType
            If resultType Is Nothing Then
                resultType = GetType(T)
            End If

            e.Result = New AsyncResult(method.Invoke(request.ObjectInstance, _
                request.MethodParamArray), resultType, _
                Csla.ApplicationContext.GlobalContext, Nothing)

        Catch ex As Exception

            If Not ex.InnerException Is Nothing Then
                e.Result = New AsyncResult(Nothing, GetType(T), _
                    Csla.ApplicationContext.GlobalContext, _
                    New Exception(ex.InnerException.Message, ex))
            Else
                e.Result = New AsyncResult(Nothing, GetType(T), _
                    Csla.ApplicationContext.GlobalContext, ex)
            End If

        End Try

    End Sub

    Private Sub Fetch_RunWorkerCompleted(ByVal sender As Object, _
            ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)

        Try
            RemoveHandler _Worker.DoWork, AddressOf Fetch_DoWork
        Catch ex As Exception
        End Try
        Try
            RemoveHandler _Worker.RunWorkerCompleted, AddressOf Fetch_RunWorkerCompleted
        Catch ex As Exception
        End Try
        _Worker.Dispose()
        _Worker = Nothing

        If e.Error Is Nothing Then

            Dim result As AsyncResult = TryCast(e.Result, AsyncResult)

            If result Is Nothing Then

                OnFetchCompleted(New AsyncResult(Nothing, GetType(T), Nothing, _
                    New InvalidOperationException("RunWorkerCompleted did not return AsyncResult.")))

            Else

                SetThreadContext(result)

                OnFetchCompleted(result)

            End If

        Else

            OnFetchCompleted(New AsyncResult(Nothing, GetType(T), Nothing, e.Error))

        End If

    End Sub

    Private Shared Sub SetThreadContext(ByVal request As IAsyncContext)

        If Not request.Principal Is Nothing Then
            Csla.ApplicationContext.User = request.Principal
        End If

        Dim contextParams(1) As Object
        If request.ClientContext Is Nothing Then
            contextParams(0) = Csla.ApplicationContext.ClientContext
        Else
            contextParams(0) = request.ClientContext
        End If
        contextParams(1) = request.GlobalContext

        GetType(Csla.ApplicationContext).GetMethod("SetContext", BindingFlags.NonPublic _
            OrElse BindingFlags.Static).Invoke(Nothing, contextParams)

    End Sub


    Friend Shared Function GetMethodInfo(Of TC)(ByVal isInstanceMethod As Boolean, _
        ByVal methodName As String, ByVal params As Object()) As MethodInfo

        Dim paramCount As Integer
        If params Is Nothing Then
            paramCount = 0
        Else
            paramCount = params.Length
        End If

        Dim methods As MethodInfo()

        If isInstanceMethod Then
            methods = GetType(TC).GetMethods(BindingFlags.Public OrElse BindingFlags.Instance)
        Else
            methods = GetType(TC).GetMethods(BindingFlags.Public OrElse BindingFlags.Static)
        End If

        For Each m As MethodInfo In methods

            If m.Name.Trim.ToLower = methodName.Trim.ToLower Then

                Dim methodParams As ParameterInfo() = m.GetParameters

                If methodParams.Length = 0 AndAlso paramCount = 0 Then

                    Return m

                ElseIf methodParams.Length = paramCount Then

                    Dim paramsIdentical As Boolean = True
                    For i As Integer = 1 To methodParams.Length
                        If Not IsMethodParam(params(i - 1), methodParams(i - 1)) Then
                            paramsIdentical = False
                            Exit For
                        End If
                    Next

                    If paramsIdentical Then Return m

                End If

            End If

        Next

        Throw New Exception(String.Format("Method {0} for type {1} with param count {2} is unknown.", _
            methodName, GetType(TC).FullName, paramCount.ToString))

    End Function

    Private Shared Function IsMethodParam(ByVal param As Object, ByVal paramInfo As ParameterInfo) As Boolean

        If param Is Nothing Then

            If paramInfo.ParameterType.IsValueType Then
                Return False
            Else
                Return True
            End If

        End If

        If param.GetType Is paramInfo.ParameterType Then Return True

        If paramInfo.ParameterType.IsInterface _
            AndAlso Array.IndexOf(param.GetType.GetInterfaces, _
            paramInfo.ParameterType) >= 0 Then Return True

        Return False

    End Function


    ''' <summary>
    ''' Represents an argument for backgroud worker to pass the data required to invoke a method.
    ''' </summary>
    ''' <typeparam name="TC"></typeparam>
    ''' <remarks></remarks>
    Private Class AsyncArguments(Of TC)
        Implements IAsyncContext

        Private _ObjectInstance As TC
        Private _MethodName As String
        Private _MethodParamArray As Object()
        Private _Principal As System.Security.Principal.IPrincipal
        Private _ClientContext As System.Collections.Specialized.HybridDictionary
        Private _GlobalContext As System.Collections.Specialized.HybridDictionary


        Public ReadOnly Property ObjectInstance() As TC
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ObjectInstance
            End Get
        End Property

        Public ReadOnly Property MethodName() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _MethodName.Trim
            End Get
        End Property

        Public ReadOnly Property MethodParamArray() As Object()
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _MethodParamArray
            End Get
        End Property

        Public ReadOnly Property Principal() As System.Security.Principal.IPrincipal _
            Implements IAsyncContext.Principal
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Principal
            End Get
        End Property

        Public ReadOnly Property ClientContext() As System.Collections.Specialized.HybridDictionary _
            Implements IAsyncContext.ClientContext
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ClientContext
            End Get
        End Property

        Public ReadOnly Property GlobalContext() As System.Collections.Specialized.HybridDictionary _
            Implements IAsyncContext.GlobalContext
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _GlobalContext
            End Get
        End Property


        Public Sub New(ByVal instance As TC, ByVal methodName As String, _
            ByVal ParamArray methodParams As Object())

            _ObjectInstance = instance
            _MethodName = methodName
            _MethodParamArray = methodParams
            _Principal = Csla.ApplicationContext.User
            _ClientContext = Csla.ApplicationContext.ClientContext
            _GlobalContext = Csla.ApplicationContext.GlobalContext

        End Sub

    End Class

End Class
