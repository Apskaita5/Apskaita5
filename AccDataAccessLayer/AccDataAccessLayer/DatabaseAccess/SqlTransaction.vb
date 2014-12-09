Namespace DatabaseAccess

    <Serializable()> _
    Public Class SqlTransaction
        Implements IDisposable

        Private _TransactionExisted As Boolean = False
        Private _TransactionCommited As Boolean = False


        Public ReadOnly Property TransactionExisted() As Boolean
            Get
                Return _TransactionExisted
            End Get
        End Property


        Public Sub New()

            If TransactionExists() Then
                _TransactionExisted = True
                Exit Sub
            End If

            TransactionBegin()

        End Sub

        Public Sub Commit()
            If _TransactionExisted Then Exit Sub
            TransactionCommit()
            _TransactionCommited = True
        End Sub

        Private Sub ExceptionThrown()

        End Sub

        Private disposedValue As Boolean = False        ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    ' TODO: free unmanaged resources when explicitly called
                    If Not _TransactionExisted AndAlso Not _TransactionCommited Then

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


