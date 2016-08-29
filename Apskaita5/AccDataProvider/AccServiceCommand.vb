
Imports fyiReporting.Data

Public Class AccServiceCommand
    Implements IDbCommand

    ' connection we're running under
    Private ReadOnly _Connection As AccServiceConnection
    ' command to execute
    Private _Command As String
    ' command parameters to use
    Private _Parameters As New DataParameterCollection()


    Public Sub New(ByVal conn As AccServiceConnection)
        _Connection = conn
    End Sub


#Region "IDbCommand Members"

    Public Sub Cancel() Implements IDbCommand.Cancel
        Throw New NotImplementedException("Cancel not implemented")
    End Sub

    Public Sub Prepare() Implements IDbCommand.Prepare
        ' Prepare is a noop
    End Sub

    Public Property CommandType() As CommandType Implements IDbCommand.CommandType
        Get
            Throw New NotImplementedException("CommandType not implemented")
        End Get
        Set(ByVal value As System.Data.CommandType)
            Throw New NotImplementedException("CommandType not implemented")
        End Set
    End Property

    Public Function ExecuteReader(ByVal behavior As CommandBehavior) As IDataReader _
        Implements IDbCommand.ExecuteReader
        If Not (behavior = CommandBehavior.SingleResult OrElse behavior = CommandBehavior.SchemaOnly) Then
            Throw New ArgumentException("ExecuteReader supports SingleResult and SchemaOnly only.")
        End If
        Return New AccServiceDataReader(behavior, _Connection, Me)
    End Function


    Private Function System_Data_IDbCommand_ExecuteReader() As IDataReader _
        Implements IDbCommand.ExecuteReader
        Return ExecuteReader(CommandBehavior.SingleResult)
    End Function

    Public Function ExecuteScalar() As Object Implements IDbCommand.ExecuteScalar
        Throw New NotImplementedException("ExecuteScalar not implemented")
    End Function

    Public Function ExecuteNonQuery() As Integer Implements IDbCommand.ExecuteNonQuery
        Throw New NotImplementedException("ExecuteNonQuery not implemented")
    End Function

    Public Property CommandTimeout() As Integer _
        Implements IDbCommand.CommandTimeout
        Get
            Return 0
        End Get
        Set(ByVal value As Integer)
            ' do nothing
        End Set
    End Property

    Public Function CreateParameter() As IDbDataParameter _
        Implements IDbCommand.CreateParameter
        Return New AccServiceDataParameter()
    End Function

    Public Property Connection() As IDbConnection _
        Implements IDbCommand.Connection
        Get
            Return Me._Connection
        End Get
        Set(ByVal value As IDbConnection)
            Throw New NotImplementedException("Setting Connection not implemented")
        End Set
    End Property

    Public Property UpdatedRowSource() As UpdateRowSource _
        Implements IDbCommand.UpdatedRowSource
        Get
            Throw New NotImplementedException("UpdatedRowSource not implemented")
        End Get
        Set(ByVal value As System.Data.UpdateRowSource)
            Throw New NotImplementedException("UpdatedRowSource not implemented")
        End Set
    End Property

    Public Property CommandText() As String _
        Implements IDbCommand.CommandText
        Get
            Return _Command
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then
                _Command = ""
            Else
                _Command = value.Trim
            End If
        End Set
    End Property

    Public ReadOnly Property Parameters() As IDataParameterCollection _
        Implements IDbCommand.Parameters
        Get
            Return _Parameters
        End Get
    End Property

    Public Property Transaction() As IDbTransaction _
        Implements IDbCommand.Transaction
        Get
            Throw New NotImplementedException("Transaction not implemented")
        End Get
        Set(ByVal value As IDbTransaction)
            Throw New NotImplementedException("Transaction not implemented")
        End Set
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
