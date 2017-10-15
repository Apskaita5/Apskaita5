Imports AccDataAccessLayer.DatabaseAccess
Imports System.Security

Public Class AccServiceConnection
    Implements IDbConnection

    Private _Connection As String
    ' the connection string; of format directory=
    Private _Open As Boolean = False


    Public Sub New(ByVal conn As String)
        ConnectionString = conn
    End Sub


    Public ReadOnly Property IsOpen() As Boolean
        Get
            Return _Open
        End Get
    End Property

#Region "IDbConnection Members"

    Public Sub ChangeDatabase(ByVal databaseName As String) _
        Implements IDbConnection.ChangeDatabase
        Throw New NotImplementedException("ChangeDatabase method not supported.")
    End Sub

    Public Function BeginTransaction(ByVal il As IsolationLevel) As IDbTransaction _
        Implements IDbConnection.BeginTransaction
        Throw New NotImplementedException("BeginTransaction method not supported.")
    End Function

    Private Function BeginTransaction() As IDbTransaction _
        Implements IDbConnection.BeginTransaction
        Throw New NotImplementedException("BeginTransaction method not supported.")
    End Function

    Public ReadOnly Property State() As System.Data.ConnectionState _
        Implements IDbConnection.State
        Get
            If _Open Then
                Return ConnectionState.Open
            Else
                Return ConnectionState.Closed
            End If
        End Get
    End Property

    Public Property ConnectionString() As String _
        Implements IDbConnection.ConnectionString
        Get
            Return _Connection
        End Get
        Set(ByVal value As String)
            _Connection = value
        End Set
    End Property

    Public Function CreateCommand() As IDbCommand _
        Implements IDbConnection.CreateCommand
        Return New AccServiceCommand(Me)
    End Function

    Public Sub Open() Implements IDbConnection.Open
        If Not GetCurrentIdentity().IsAuthenticatedWithDB Then
            Throw New SecurityException("User is not authenticated (or connected to a database).")
        End If
        _Open = True
    End Sub

    Public Sub Close() Implements IDbConnection.Close
        _Open = False
    End Sub

    Public ReadOnly Property Database() As String _
        Implements IDbConnection.Database
        Get
            If Not GetCurrentIdentity.IsAuthenticatedWithDB Then
                Return ""
            Else
                Return GetCurrentIdentity.Database
            End If
        End Get
    End Property

    Public ReadOnly Property ConnectionTimeout() As Integer _
        Implements IDbConnection.ConnectionTimeout
        Get
            Return ConnectionTimeout
        End Get
    End Property

#End Region

#Region "IDisposable Members"

    Private disposedValue As Boolean = False        ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: free other state (managed objects).
            End If

            ' TODO: free your own state (unmanaged objects).
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

#End Region

End Class
