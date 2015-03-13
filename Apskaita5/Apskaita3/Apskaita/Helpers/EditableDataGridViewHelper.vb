Public Class EditableDataGridViewHelper
    Implements IDisposable

    Private WithEvents _grid As DataGridView

    Public Sub New(ByVal grid As DataGridView)
        If grid Is Nothing Then Throw New ArgumentNullException("grid")
        _grid = grid
        If grid.AllowUserToAddRows Then
            AddHandler _grid.RowLeave, AddressOf OnRowLeave
        End If
        AddHandler _grid.DataError, AddressOf OnDataError
        AddHandler _grid.KeyDown, AddressOf OnKeyDown
        AddHandler _grid.CellEndEdit, AddressOf OnCellEndEdit
        AddHandler _grid.Disposed, AddressOf OnDisposing
    End Sub

    Private Sub New()

    End Sub


    Private Sub OnRowLeave(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)

        Dim CurrentGrid As DataGridView = Nothing
        Try
            CurrentGrid = DirectCast(sender, DataGridView)
        Catch ex As Exception
        End Try
        If CurrentGrid Is Nothing Then Exit Sub

        If CurrentGrid.Rows(e.RowIndex).IsNewRow AndAlso _
            e.RowIndex = CurrentGrid.Rows.Count - 1 Then CurrentGrid.CancelEdit()

    End Sub

    Private Sub OnDataError(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewDataErrorEventArgs)
        e.ThrowException = False
        e.Cancel = True
    End Sub

    Private Sub OnKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            SendKeys.Send("{Tab}")
            e.Handled = True
        End If
    End Sub

    Private Sub OnCellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)

        Dim currentGrid As DataGridView = Nothing
        Try
            currentGrid = DirectCast(sender, DataGridView)
        Catch ex As Exception
        End Try
        If currentGrid Is Nothing Then Exit Sub

        If Not currentGrid.Focused Then currentGrid.Select()

        If (currentGrid.AllowUserToAddRows OrElse e.RowIndex < (currentGrid.Rows.Count - 1)) _
            AndAlso currentGrid.Columns(e.ColumnIndex).DisplayIndex < GetMaxVisibleDisplayIndex(currentGrid) Then
            SendKeys.Send("{up}")
            For i As Integer = 0 To currentGrid.Columns(e.ColumnIndex).DisplayIndex
                SendKeys.Send("{Tab}")
            Next
        End If

    End Sub

    Private Function GetMaxVisibleDisplayIndex(ByVal grid As DataGridView) As Integer
        Dim result As Integer = 0
        For Each col As DataGridViewColumn In grid.Columns
            If col.Visible AndAlso col.DisplayIndex > result Then result = col.DisplayIndex
        Next
        Return result
    End Function

    Private Sub OnDisposing(ByVal sender As Object, ByVal e As System.EventArgs)

        Dim currentGrid As DataGridView = Nothing
        Try
            currentGrid = DirectCast(sender, DataGridView)
        Catch ex As Exception
        End Try
        If currentGrid Is Nothing Then Exit Sub

        Try
            RemoveHandler currentGrid.RowLeave, AddressOf OnRowLeave
        Catch ex As Exception
        End Try
        Try
            RemoveHandler currentGrid.DataError, AddressOf OnDataError
        Catch ex As Exception
        End Try
        Try
            RemoveHandler currentGrid.KeyDown, AddressOf OnKeyDown
        Catch ex As Exception
        End Try
        Try
            RemoveHandler currentGrid.CellEndEdit, AddressOf OnCellEndEdit
        Catch ex As Exception
        End Try
        Try
            RemoveHandler currentGrid.Disposed, AddressOf OnDisposing
        Catch ex As Exception
        End Try

    End Sub

#Region " IDisposable Support "

    Private disposedValue As Boolean = False        ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: free other state (managed objects).
                If Not _grid Is Nothing AndAlso Not _grid.IsDisposed Then
                    Try
                        RemoveHandler _grid.RowLeave, AddressOf OnRowLeave
                    Catch ex As Exception
                    End Try
                    Try
                        RemoveHandler _grid.DataError, AddressOf OnDataError
                    Catch ex As Exception
                    End Try
                    Try
                        RemoveHandler _grid.KeyDown, AddressOf OnKeyDown
                    Catch ex As Exception
                    End Try
                    Try
                        RemoveHandler _grid.CellEndEdit, AddressOf OnCellEndEdit
                    Catch ex As Exception
                    End Try
                    Try
                        RemoveHandler _grid.Disposed, AddressOf OnDisposing
                    Catch ex As Exception
                    End Try
                End If
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
