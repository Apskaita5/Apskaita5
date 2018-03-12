Imports AccDataAccessLayer.DatabaseAccess
Imports AccDataAccessLayer.Security
Friend Class ChangedDatabase
    Implements IDisposable

    Private _OldDatabaseName As String
    Private disposedValue As Boolean = False        ' To detect redundant calls

    Public Sub New(ByVal NameOfDatabaseToUse As String)

        Dim CurrentIdentity As AccIdentity = GetCurrentIdentity()
        _OldDatabaseName = CurrentIdentity.Database
        CurrentIdentity.Database = NameOfDatabaseToUse

    End Sub

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                GetCurrentIdentity().Database = _OldDatabaseName
            End If
            ' TODO: free shared unmanaged resources
        End If
        Me.disposedValue = True
    End Sub

#Region " IDisposable Support "
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
