Namespace DatabaseAccess

    <Serializable()> _
    Public Class SqlTransaction
        Implements IDisposable

        Private _IsTransactionOwner As Boolean = True
        Private _TransactionCommited As Boolean = False

        Public ReadOnly Property IsTransactionOwner() As Boolean
            Get
                Return _IsTransactionOwner
            End Get
        End Property


        Public Sub New()

            If TransactionExists() Then
                _IsTransactionOwner = False
                Exit Sub
            End If

            TransactionBegin()

        End Sub

        Public Sub Commit()
            If Not _IsTransactionOwner OrElse _TransactionCommited Then Exit Sub
            TransactionCommit()
            _TransactionCommited = True
        End Sub

        Public Sub SetNonSqlException(ByVal ex As Exception)
            If Not _IsTransactionOwner OrElse _TransactionCommited Then Exit Sub
            If TransactionExists() Then
                TransactionRollBack(ex)
            End If
        End Sub

        Private disposedValue As Boolean = False        ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free unmanaged resources when explicitly called
                    If _IsTransactionOwner AndAlso TransactionExists() Then
                        TransactionRollBack(New Exception("Klaida. Neužbaigta SQL transakcija."))
                    End If
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

End Namespace


