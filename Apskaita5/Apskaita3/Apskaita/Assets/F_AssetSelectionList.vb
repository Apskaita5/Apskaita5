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
                "GetLongTermAssetInfoList", False, Today.AddMonths(-1), _
                Today, Nothing)
        Catch ex As Exception
            ShowError(ex)
            DisableAllControls(Me)
            Exit Sub
        End Try

        LongTermAssetInfoListBindingSource.DataSource = Obj

        SetDataGridViewLayOut(LongTermAssetInfoListDataGridView)
        SetFormLayout(Me)

    End Sub


    Private Sub nOkButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles nOkButton.Click

        If Obj Is Nothing OrElse Obj.Count < 1 Then Exit Sub

        Dim list As New List(Of Integer)

        For Each row As DataGridViewRow In LongTermAssetInfoListDataGridView.Rows
            If DirectCast(row.Cells(SelectionDataGridViewColumn.Name).Value, Boolean) Then
                Try
                    list.Add(DirectCast(row.DataBoundItem, LongTermAssetInfo).ID)
                Catch ex As Exception
                End Try
            End If
        Next

        _Result = list.ToArray()

        Me.Hide()
        Me.Close()

    End Sub

    Private Sub nCancelButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles nCancelButton.Click
        _Result = Nothing
        Me.Hide()
        Me.Close()
    End Sub

End Class