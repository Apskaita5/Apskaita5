Imports ApskaitaObjects.ActiveReports
Imports AccControlsWinForms

Friend Class F_AssetSelectionList

    Private _Result As Integer() = Nothing
    Private _QueryManager As CslaActionExtenderQueryObject


    Public ReadOnly Property Result() As Integer()
        Get
            Return _Result
        End Get
    End Property


    Private Sub F_AssetSelectionList_FormClosing(ByVal sender As Object, _
        ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        MyCustomSettings.GetListViewLayOut(LongTermAssetInfoListDataListView)
        MyCustomSettings.GetFormLayout(Me)

    End Sub

    Private Sub F_AssetSelectionList_Load(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            _QueryManager = New CslaActionExtenderQueryObject(Me, ProgressFiller1)

            'LongTermAssetInfoList.GetLongTermAssetInfoList(Today, Today.AddYears(50), Nothing)
            _QueryManager.InvokeQuery(Of LongTermAssetInfoList)(Nothing, _
                "GetLongTermAssetInfoList", True, AddressOf OnDataSourceFetched, Today, Today.AddYears(50), Nothing)

        Catch ex As Exception
            ShowError(ex, Nothing)
            DisableAllControls(Me)
            Exit Sub
        End Try

    End Sub

    Private Sub OnDataSourceFetched(ByVal result As Object, ByVal exceptionHandled As Boolean)

        If exceptionHandled Then
            DisableAllControls(Me)
            Exit Sub
        ElseIf result Is Nothing Then
            MsgBox("Klaida. Dėl nežinomų priežasčių nepavyko gauti ilgalaikio turto sąrašo.", _
                MsgBoxStyle.Exclamation, "Klaida")
            DisableAllControls(Me)
            Exit Sub
        End If

        Try
            _LongTermAssetInfoListBindingSource.DataSource = DirectCast(result, LongTermAssetInfoList).GetFilteredList(False)
        Catch ex As Exception
            ShowError(ex, result)
            DisableAllControls(Me)
            Exit Sub
        End Try

        MyCustomSettings.SetListViewLayOut(LongTermAssetInfoListDataListView)
        MyCustomSettings.SetFormLayout(Me)

    End Sub


    Private Sub nOkButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles nOkButton.Click

        Try
            _Result = GetCheckedAssetsIds()
        Catch ex As Exception
            ShowError(ex, Nothing)
            Exit Sub
        End Try

        If _Result Is Nothing OrElse _Result.Length < 1 Then
            MsgBox("Klaida. Nepasirinktas nė vienas ilgalaikis turtas.", MsgBoxStyle.Exclamation, "Klaida")
            Exit Sub
        End If

        Me.Close()

    End Sub

    Private Sub nCancelButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles nCancelButton.Click
        _Result = Nothing
        Me.Close()
    End Sub


    Private Function GetCheckedAssetsIds() As Integer()

        Dim result As New List(Of Integer)

        If LongTermAssetInfoListDataListView.CheckedObjects Is Nothing OrElse _
            LongTermAssetInfoListDataListView.CheckedObjects.Count < 1 Then
            Return result.ToArray()
        End If

        For Each info As LongTermAssetInfo In LongTermAssetInfoListDataListView.CheckedObjects
            result.Add(info.ID)
        Next

        Return result.ToArray

    End Function

End Class