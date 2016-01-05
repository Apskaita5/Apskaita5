Imports ApskaitaObjects.ActiveReports
Public Class F_AssetSelectionList

    Dim Obj As LongTermAssetInfoList
    Dim _Result As Integer() = Nothing


    Public ReadOnly Property Result() As Integer()
        Get
            Return _Result
        End Get
    End Property
    

    Private Sub F_AssetSelectionList_FormClosing(ByVal sender As Object, _
        ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        GetDataGridViewLayOut(LongTermAssetInfoListDataGridView)
        GetFormLayout(Me)

    End Sub

    Private Sub F_AssetSelectionList_Load(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            Obj = LoadObject(Of LongTermAssetInfoList)(Nothing, _
                "GetLongTermAssetInfoList", False, Today, Today.AddYears(50), Nothing)
        Catch ex As Exception
            ShowError(ex)
            DisableAllControls(Me)
            Exit Sub
        End Try

        LongTermAssetInfoListBindingSource.DataSource = Obj.GetFilteredList(False)

        SetDataGridViewLayOut(LongTermAssetInfoListDataGridView)
        SetFormLayout(Me)

    End Sub


    Private Sub nOkButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles nOkButton.Click

        If Obj Is Nothing OrElse Obj.Count < 1 Then Exit Sub

        Try
            _Result = GetCheckedAssetsIds()
        Catch ex As Exception
            ShowError(ex)
            Exit Sub
        End Try


        Me.Hide()
        Me.Close()

    End Sub

    Private Sub nCancelButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles nCancelButton.Click
        _Result = Nothing
        Me.Hide()
        Me.Close()
    End Sub


    Private Function GetCheckedAssetsIds() As Integer()

        Dim result As New List(Of Integer)

        Try
            LongTermAssetInfoListDataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit)
        Catch ex As Exception
        End Try

        For Each dgr As DataGridViewRow In LongTermAssetInfoListDataGridView.Rows

            If Not LongTermAssetInfoListDataGridView.Item(SelectionDataGridViewColumn.Index, dgr.Index).Value Is Nothing AndAlso _
                DirectCast(LongTermAssetInfoListDataGridView.Item(SelectionDataGridViewColumn.Index, dgr.Index).Value, Boolean) AndAlso _
                Not dgr.DataBoundItem Is Nothing AndAlso DirectCast(dgr.DataBoundItem, LongTermAssetInfo).ID > 0 Then

                result.Add(CType(dgr.DataBoundItem, LongTermAssetInfo).ID)

            End If

        Next

        If result.Count < 1 Then Throw New Exception("Klaida. Nepasirinktas nė vienas ilgalaikis turtas.")

        Return result.ToArray

    End Function

End Class