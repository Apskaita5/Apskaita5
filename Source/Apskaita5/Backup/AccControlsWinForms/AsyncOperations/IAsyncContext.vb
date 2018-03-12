''' <summary>
''' An interface required by CSLA framework async operations
''' in order to transfer Principal and context data between threads.
''' </summary>
''' <remarks>See <see cref="MethodInvoker(Of T)">MethodInvoker</see>
''' class SetThreadContext method for details.</remarks>
Friend Interface IAsyncContext

    ReadOnly Property ClientContext() As System.Collections.Specialized.HybridDictionary
    ReadOnly Property GlobalContext() As System.Collections.Specialized.HybridDictionary
    ReadOnly Property Principal() As System.Security.Principal.IPrincipal

End Interface
