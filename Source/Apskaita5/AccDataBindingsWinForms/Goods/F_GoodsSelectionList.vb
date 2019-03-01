Imports ApskaitaObjects.HelperLists
Imports ApskaitaObjects.Documents
Imports AccControlsWinForms
Imports AccDataBindingsWinForms.CachedInfoLists

Friend Class F_GoodsSelectionList

    Private _Result As Integer() = Nothing


    Public ReadOnly Property Result() As Integer()
        Get
            Return _Result
        End Get
    End Property


    Private Sub F_GoodsSelectionList_FormClosing(ByVal sender As Object, _
        ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        MyCustomSettings.GetListViewLayOut(GoodsInfoListDataListView)
        MyCustomSettings.GetFormLayout(Me)
    End Sub

    Private Sub F_GoodsSelectionList_Load(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load

        If Not PrepareCache(Me, GetType(GoodsInfoList)) Then Exit Sub

        Try
            GoodsInfoListBindingSource.DataSource = GoodsInfoList.GetCachedFilteredList( _
                False, False, TradedItemType.All, Nothing)
        Catch ex As Exception
            ShowError(ex, Nothing)
            DisableAllControls(Me)
            Exit Sub
        End Try

        MyCustomSettings.SetListViewLayOut(GoodsInfoListDataListView)
        MyCustomSettings.SetFormLayout(Me)

    End Sub


    Private Sub nOkButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles nOkButton.Click

        Try
            _Result = GetCheckedGoodsIds()
        Catch ex As Exception
            ShowError(ex, Nothing)
            Exit Sub
        End Try

        If _Result Is Nothing OrElse _Result.Length < 1 Then
            MsgBox("Klaida. Nepasirinkta nė viena prekė.", MsgBoxStyle.Exclamation, "Klaida")
            Exit Sub
        End If

        Me.Close()

    End Sub

    Private Sub nCancelButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles nCancelButton.Click
        _Result = Nothing
        Me.Close()
    End Sub


    Private Function GetCheckedGoodsIds() As Integer()

        Dim result As New List(Of Integer)

        If GoodsInfoListDataListView.CheckedObjects Is Nothing OrElse GoodsInfoListDataListView.CheckedObjects.Count < 1 Then
            Return result.ToArray()
        End If

        For Each info As GoodsInfo In GoodsInfoListDataListView.CheckedObjects
            result.Add(info.ID)
        Next

        Return result.ToArray

    End Function

End Class