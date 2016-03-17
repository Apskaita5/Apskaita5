Friend Class AsyncResult
    Inherits System.EventArgs
    Implements IAsyncContext

    Private _ObjectInstance As Object
    Private _ObjectType As Type
    Private _GlobalContext As System.Collections.Specialized.HybridDictionary
    Private _Exception As Exception

    Public ReadOnly Property ObjectInstance() As Object
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            Return _ObjectInstance
        End Get
    End Property

    Public ReadOnly Property ObjectType() As Type
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            Return _ObjectType
        End Get
    End Property

    Public ReadOnly Property GlobalContext() As System.Collections.Specialized.HybridDictionary _
        Implements IAsyncContext.GlobalContext
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            Return _GlobalContext
        End Get
    End Property

    Public ReadOnly Property ClientContext() As System.Collections.Specialized.HybridDictionary _
        Implements IAsyncContext.ClientContext
        Get
            Return Nothing
        End Get
    End Property

    Public ReadOnly Property Principal() As System.Security.Principal.IPrincipal _
        Implements IAsyncContext.Principal
        Get
            Return Nothing
        End Get
    End Property

    Public ReadOnly Property [Exception]() As Exception
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            Return _Exception
        End Get
    End Property


    Public Sub New(ByVal instance As Object, ByVal objectType As Type, _
        ByVal globalContext As System.Collections.Specialized.HybridDictionary, _
        ByVal nException As Exception)
        _ObjectInstance = instance
        _ObjectType = objectType
        _GlobalContext = globalContext
        _Exception = nException
    End Sub

End Class
