Public Class ToolStripHelper(Of T)
    Implements IDisposable

    Friend Const CancelButtonCaption As String = "Atšaukti"
    Friend Const CancelButtonToolTip As String = "Nieko nedaryti."

    Public Delegate Function ActionAvailableForSelectedItemDelegate(ByVal selectedItem As T, _
        ByVal actionName As String) As Boolean

    Private _Delegate As ActionAvailableForSelectedItemDelegate = Nothing

    Private WithEvents _grid As DataGridView
    Private WithEvents _contextMenuStrip As ContextMenuStrip
    Private _handledbuttons As New Dictionary(Of String, KeyValuePair(Of String, DelegateContainer(Of T)))
    Private _AddCancelButton As Boolean = True
    Private _DialogText As String = ""


    Public Sub New(ByRef grid As DataGridView, ByRef contextMenuStrip As ContextMenuStrip, _
        ByVal dialogText As String, ByVal addCancelButton As Boolean, _
        Optional ByVal actionIsAvailable As ActionAvailableForSelectedItemDelegate = Nothing)

        If grid Is Nothing Then Throw New ArgumentNullException("grid")
        If contextMenuStrip Is Nothing Then Throw New ArgumentNullException("contextMenuStrip")

        _grid = grid
        _contextMenuStrip = contextMenuStrip
        _AddCancelButton = addCancelButton
        _DialogText = dialogText
        _Delegate = actionIsAvailable
        If _DialogText Is Nothing Then _DialogText = ""
        _DialogText = _DialogText.Trim

        AddHandler _grid.CellMouseClick, AddressOf Grid_CellMouseClick
        AddHandler _grid.CellDoubleClick, AddressOf Grid_CellDoubleClick
        AddHandler _grid.Disposed, AddressOf Grid_Disposing
        AddHandler _contextMenuStrip.Disposed, AddressOf MenuStrip_Disposing

    End Sub

    Private Sub New()
    End Sub


    Public Sub AddMenuItemHandler(ByRef menuItem As ToolStripMenuItem, _
        ByVal delegateHandler As DelegateContainer(Of T))

        If menuItem Is Nothing Then Throw New ArgumentNullException("menuItem")
        If delegateHandler Is Nothing Then Throw New ArgumentNullException("delegateHandler")

        menuItem.Tag = delegateHandler
        AddHandler menuItem.Click, AddressOf MenuItem_Click

    End Sub

    Public Sub AddButtonHandler(ByVal caption As String, ByVal toolTip As String, _
        ByVal clickHandler As DelegateContainer(Of T))

        If Not _handledbuttons.ContainsKey(caption.Trim) Then _handledbuttons.Add( _
            caption.Trim, New KeyValuePair(Of String, DelegateContainer(Of T)) _
            (toolTip, clickHandler))

    End Sub


    Private Sub Grid_CellMouseClick(ByVal sender As Object, _
        ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs)

        If e.Button <> Windows.Forms.MouseButtons.Right OrElse e.RowIndex < 0 Then Exit Sub

        Dim currentGrid As DataGridView = Nothing
        Try
            currentGrid = DirectCast(sender, DataGridView)
        Catch ex As Exception
        End Try
        If currentGrid Is Nothing Then Exit Sub

        Dim currentItem As T = Nothing
        Try
            currentItem = DirectCast(currentGrid.Rows(e.RowIndex).DataBoundItem, T)
        Catch ex As Exception
        End Try
        If currentItem Is Nothing Then Exit Sub

        If Not ConfigureContextMenuStrip(currentItem) Then Exit Sub

        currentGrid.ClearSelection()
        currentGrid.Rows(e.RowIndex).Selected = True

        _contextMenuStrip.Tag = currentItem

        _contextMenuStrip.Show(currentGrid, currentGrid.PointToClient(Cursor.Position))

    End Sub

    Private Sub MenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs)

        If _contextMenuStrip Is Nothing OrElse _contextMenuStrip.Tag Is Nothing _
            OrElse Not TypeOf _contextMenuStrip.Tag Is T Then Exit Sub

        Dim clickedItem As ToolStripMenuItem = Nothing
        Try
            clickedItem = DirectCast(sender, ToolStripMenuItem)
        Catch ex As Exception
        End Try
        If clickedItem Is Nothing OrElse clickedItem.Tag Is Nothing _
            OrElse Not TypeOf clickedItem.Tag Is DelegateContainer(Of T) Then Exit Sub

        DirectCast(clickedItem.Tag, DelegateContainer(Of T)).Invoke( _
            DirectCast(_contextMenuStrip.Tag, T))

    End Sub

    Private Sub Grid_CellDoubleClick(ByVal sender As Object, _
        ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)

        If _handledbuttons.Count < 1 Then Exit Sub

        If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Exit Sub

        Dim currentGrid As DataGridView = Nothing
        Try
            currentGrid = DirectCast(sender, DataGridView)
        Catch ex As Exception
        End Try
        If currentGrid Is Nothing OrElse (Not currentGrid.ReadOnly AndAlso _
            Not currentGrid.Columns(e.ColumnIndex).ReadOnly) Then Exit Sub

        Dim currentItem As T = Nothing
        Try
            currentItem = DirectCast(currentGrid.Rows(e.RowIndex).DataBoundItem, T)
        Catch ex As Exception
        End Try
        If currentItem Is Nothing Then Exit Sub

        Dim buttons As ButtonStructure() = GetButtons(currentItem)

        If buttons Is Nothing Then Exit Sub

        Dim answer As String = Ask(_DialogText, buttons)

        If Not _handledbuttons.ContainsKey(answer.Trim) OrElse _
            _handledbuttons(answer.Trim).Value Is Nothing Then Exit Sub

        _handledbuttons(answer.Trim).Value.Invoke(currentItem)

    End Sub

    Private Sub Grid_Disposing(ByVal sender As Object, ByVal e As EventArgs)

        Dim currentGrid As DataGridView = Nothing
        Try
            currentGrid = DirectCast(sender, DataGridView)
        Catch ex As Exception
        End Try
        If currentGrid Is Nothing Then Exit Sub

        Try
            RemoveHandler currentGrid.CellMouseClick, AddressOf Grid_CellMouseClick
        Catch ex As Exception
        End Try
        Try
            RemoveHandler currentGrid.CellDoubleClick, AddressOf Grid_CellDoubleClick
        Catch ex As Exception
        End Try
        Try
            RemoveHandler currentGrid.Disposed, AddressOf Grid_Disposing
        Catch ex As Exception
        End Try

    End Sub

    Private Sub MenuStrip_Disposing(ByVal sender As Object, ByVal e As EventArgs)

        Dim currentMenuStrip As ContextMenuStrip = Nothing
        Try
            currentMenuStrip = DirectCast(sender, ContextMenuStrip)
        Catch ex As Exception
        End Try
        If currentMenuStrip Is Nothing Then Exit Sub

        Try
            RemoveHandler currentMenuStrip.ItemClicked, AddressOf MenuItem_Click
        Catch ex As Exception
        End Try
        Try
            RemoveHandler currentMenuStrip.Disposed, AddressOf MenuStrip_Disposing
        Catch ex As Exception
        End Try

    End Sub


    Private Function ConfigureContextMenuStrip(ByVal item As T) As Boolean

        If _contextMenuStrip Is Nothing OrElse Not _contextMenuStrip.Items.Count > 0 Then Return False

        If _Delegate Is Nothing Then Return True

        Dim anyActionAvailable As Boolean = False

        For Each child As ToolStripItem In _contextMenuStrip.Items
            If TypeOf child Is ToolStripMenuItem Then
                ConfigureContextMenuStrip(item, DirectCast(child, ToolStripMenuItem), anyActionAvailable)
            End If
        Next

        Return anyActionAvailable

    End Function

    Private Sub ConfigureContextMenuStrip(ByVal item As T, ByRef menuItem As ToolStripMenuItem, _
        ByRef anyActionAvailable As Boolean)

        If Not menuItem.Tag Is Nothing AndAlso TypeOf menuItem.Tag Is DelegateContainer(Of T) Then
            menuItem.Available = _Delegate.Invoke(item, DirectCast(menuItem.Tag, DelegateContainer(Of T)).GetActionName)
            If menuItem.Available Then anyActionAvailable = True
        End If

        For Each child As ToolStripItem In menuItem.DropDownItems
            If TypeOf child Is ToolStripMenuItem Then
                ConfigureContextMenuStrip(item, DirectCast(child, ToolStripMenuItem), anyActionAvailable)
            End If
        Next

    End Sub

    Private Function GetButtons(ByVal item As T) As ButtonStructure()

        Dim result As New List(Of ButtonStructure)

        For Each k As KeyValuePair(Of String, KeyValuePair(Of String, DelegateContainer(Of T))) In _handledbuttons
            If Not k.Value.Value Is Nothing Then
                If _Delegate Is Nothing OrElse _Delegate.Invoke(item, k.Value.Value.GetActionName) Then
                    result.Add(New ButtonStructure(k.Key, k.Value.Key))
                End If
            End If
        Next

        If result.Count < 1 Then Return Nothing

        If _AddCancelButton Then result.Add(New ButtonStructure(CancelButtonCaption, CancelButtonToolTip))

        Return result.ToArray

    End Function


#Region " IDisposable Support "

    Private disposedValue As Boolean = False        ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: free other state (managed objects).

                If Not _contextMenuStrip Is Nothing AndAlso Not _contextMenuStrip.Disposing Then

                    For Each item As ToolStripMenuItem In _contextMenuStrip.Items
                        RemoveToolStripMenuItemHandler(item)
                    Next
                    Try
                        RemoveHandler _contextMenuStrip.Disposed, AddressOf MenuStrip_Disposing
                    Catch ex As Exception
                    End Try

                End If

                If Not _grid Is Nothing AndAlso Not _grid.Disposing Then

                    Try
                        RemoveHandler _grid.CellMouseClick, AddressOf Grid_CellMouseClick
                    Catch ex As Exception
                    End Try
                    Try
                        RemoveHandler _grid.CellDoubleClick, AddressOf Grid_CellDoubleClick
                    Catch ex As Exception
                    End Try
                    Try
                        RemoveHandler _grid.Disposed, AddressOf Grid_Disposing
                    Catch ex As Exception
                    End Try

                End If

            End If

            ' TODO: free your own state (unmanaged objects).
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    Private Sub RemoveToolStripMenuItemHandler(ByVal item As ToolStripMenuItem)

        If item Is Nothing Then Exit Sub

        Try
            RemoveHandler item.Click, AddressOf MenuItem_Click
        Catch ex As Exception
        End Try

        For Each subItem As ToolStripMenuItem In item.DropDownItems
            RemoveToolStripMenuItemHandler(subItem)
        Next

        item.Dispose()

    End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

#End Region

End Class
