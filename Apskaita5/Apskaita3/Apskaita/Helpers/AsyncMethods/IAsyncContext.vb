Public Interface IAsyncContext

    ReadOnly Property ClientContext() As System.Collections.Specialized.HybridDictionary
    ReadOnly Property GlobalContext() As System.Collections.Specialized.HybridDictionary
    ReadOnly Property Principal() As System.Security.Principal.IPrincipal

End Interface
