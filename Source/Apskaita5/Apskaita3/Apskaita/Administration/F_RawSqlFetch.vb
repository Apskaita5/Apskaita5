Imports AccDataAccessLayer.DatabaseAccess.DatabaseStructure
Imports AccDataAccessLayer
Public Class F_RawSqlFetch
    Dim Obj As DatabaseStructure

    Private Sub F_Q_Browser_Activated(ByVal sender As System.Object, _
            ByVal e As System.EventArgs) Handles MyBase.Activated
        If Me.WindowState = FormWindowState.Maximized AndAlso MyCustomSettings.AutoSizeForm Then _
            Me.WindowState = FormWindowState.Normal
    End Sub

    Private Sub F_RawSqlFetch_Load(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            Using busy As New StatusBusy
                Obj = DatabaseStructure.GetDatabaseStructure()
            End Using
        Catch ex As Exception
            ShowError(ex, Nothing)
            DisableAllControls(Me)
            Exit Sub
        End Try

        For Each item As DatabaseTable In Obj.TableList
            Dim currentTableNode As System.Windows.Forms.TreeNode = _
                DatabaseGaugeTreeView.Nodes.Add(item.Name)
            currentTableNode.ToolTipText = item.Description
            For Each fieldItem As DatabaseField In item.FieldList
                Dim currentFieldNode As System.Windows.Forms.TreeNode = _
                    currentTableNode.Nodes.Add(fieldItem.Name)
                currentFieldNode.ToolTipText = fieldItem.GetFieldDbDefinition( _
                    GetSqlGenerator(SqlServerType.MySQL)) & vbCrLf & fieldItem.Description
            Next
        Next

        For Each item As DatabaseStoredProcedure In Obj.StoredProcedureList
            Dim currentProcedureNode As System.Windows.Forms.TreeNode = _
                DatabaseGaugeTreeView.Nodes.Add(item.Name)
            currentProcedureNode.ToolTipText = item.Description
        Next

    End Sub

    Private Sub ExecuteButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles ExecuteButton.Click

        ExecutedInLabel.Text = ""

        Try

            Using busy As New StatusBusy

                dim watch as new System.Diagnostics.Stopwatch()
                watch.Start()

                Dim result As RawSQLFetch = RawSQLFetch.GetRawSQLFetch(SqlQueryTextBox.Text.Trim)

                watch.Stop()

                ExecutedInLabel.Text = String.Format("Executed in {0} sec. ({1} ms)", _
                 watch.Elapsed.Seconds, watch.ElapsedMilliseconds)

                If Not ResultDataGridView.DataSource Is Nothing Then
                    CType(ResultDataGridView.DataSource, DataTable).Dispose()
                    ResultDataGridView.DataSource = Nothing
                End If

                ResultDataGridView.DataSource = result.GetDataTable

            End Using

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Error")
            Exit Sub
        End Try

    End Sub

    Private Sub SqlQueryTextBox_DragDrop(ByVal sender As Object, _
        ByVal e As System.Windows.Forms.DragEventArgs) Handles SqlQueryTextBox.DragDrop

        If Not String.IsNullOrEmpty(SqlQueryTextBox.Text) _
            AndAlso SqlQueryTextBox.Text.Chars(SqlQueryTextBox.Text.Length - 1) <> " " AndAlso _
            SqlQueryTextBox.Text.Chars(SqlQueryTextBox.Text.Length - 1) <> "," AndAlso _
            SqlQueryTextBox.Text.Chars(SqlQueryTextBox.Text.Length - 1) <> "." Then

            SqlQueryTextBox.Text = SqlQueryTextBox.Text & ", " & _
                e.Data.GetData(System.Windows.Forms.DataFormats.Text).ToString()

        Else
            SqlQueryTextBox.Text = SqlQueryTextBox.Text & e.Data.GetData( _
                System.Windows.Forms.DataFormats.Text).ToString()
        End If

    End Sub

    Private Sub DatabaseGaugeTreeView_ItemDrag(ByVal sender As Object, _
        ByVal e As System.Windows.Forms.ItemDragEventArgs) Handles DatabaseGaugeTreeView.ItemDrag
        DatabaseGaugeTreeView.DoDragDrop(CType(e.Item, System.Windows.Forms.TreeNode).Text, _
            System.Windows.Forms.DragDropEffects.Copy)
    End Sub

    Private Sub SqlQueryTextBox_DragEnter(ByVal sender As Object, _
        ByVal e As System.Windows.Forms.DragEventArgs) Handles SqlQueryTextBox.DragEnter
        If (e.Data.GetDataPresent(System.Windows.Forms.DataFormats.Text)) Then
            e.Effect = System.Windows.Forms.DragDropEffects.Copy
        Else
            e.Effect = System.Windows.Forms.DragDropEffects.None
        End If
    End Sub

End Class