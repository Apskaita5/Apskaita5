Imports Csla.Core
Public Class AsyncArguments(Of T)
    Implements IAsyncContext

    Private _ObjectInstance As T
    Private _MethodName As String
    Private _MethodParamArray As Object()
    Private _AllowCancel As Boolean
    Private _Principal As System.Security.Principal.IPrincipal
    Private _ClientContext As System.Collections.Specialized.HybridDictionary
    Private _GlobalContext As System.Collections.Specialized.HybridDictionary


    Public ReadOnly Property ObjectInstance() As T
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

    Public ReadOnly Property AllowCancel() As Boolean
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            Return _AllowCancel
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


    Public Sub New(ByVal nObjectInstance As T, ByVal nMethodName As String, _
        ByVal nAllowCancel As Boolean, ByVal ParamArray nMethodParamArray As Object())

        _ObjectInstance = nObjectInstance
        _MethodName = nMethodName
        _AllowCancel = nAllowCancel
        _MethodParamArray = nMethodParamArray
        _Principal = Csla.ApplicationContext.User
        _ClientContext = Csla.ApplicationContext.ClientContext
        _GlobalContext = Csla.ApplicationContext.GlobalContext

    End Sub

End Class