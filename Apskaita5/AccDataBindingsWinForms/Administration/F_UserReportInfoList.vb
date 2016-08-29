Imports AccControlsWinForms
Imports ApskaitaObjects.HelperLists
Imports ApskaitaObjects.Settings

Public Class F_UserReportInfoList

    Private _FormManager As CslaActionExtenderReportForm(Of UserReportInfoList)
    Private _ListViewManager As DataListViewEditControlManager(Of UserReportInfo)
    Private _QueryManager As CslaActionExtenderQueryObject


    Private Sub F_UserReportInfoList_Load(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load

        If Not SetDataSources() Then Exit Sub

        RefreshButton.PerformClick()

    End Sub

    Private Function SetDataSources() As Boolean

        Try

            _ListViewManager = New DataListViewEditControlManager(Of UserReportInfo) _
                (UserReportInfoListDataListView, ContextMenuStrip1, Nothing, _
                 Nothing, Nothing)

            _ListViewManager.AddCancelButton = True
            _ListViewManager.AddButtonHandler("Parsisiųsti", "Parsisiųsti ataskaitos failą (*.rdl).", _
                AddressOf DownloadItem)
            _ListViewManager.AddButtonHandler("Ištrinti", "Pašalinti ataskaitą.", _
                AddressOf DeleteItem)

            _ListViewManager.AddMenuItemHandler(DownloadItem_MenuItem, AddressOf DownloadItem)
            _ListViewManager.AddMenuItemHandler(DeleteItem_MenuItem, AddressOf DeleteItem)

            _QueryManager = New CslaActionExtenderQueryObject(Me, ProgressFiller2)

            ' UserReportInfoList.GetList()
            _FormManager = New CslaActionExtenderReportForm(Of UserReportInfoList) _
                (Me, UserReportInfoListBindingSource, Nothing, Nothing, RefreshButton, _
                 ProgressFiller1, "GetList", AddressOf GetReportParams)

            _FormManager.SetBeforeFetchHandler(AddressOf BeforeFetch)

            _FormManager.ManageDataListViewStates(UserReportInfoListDataListView)

        Catch ex As Exception
            ShowError(ex)
            DisableAllControls(Me)
            Return False
        End Try

        Return True

    End Function


    Private Function GetReportParams() As Object()
        Return New Object() {}
    End Function

    Private Sub BeforeFetch()
        UserReportInfoList.InvalidateCache()
    End Sub

    Private Sub DownloadItem(ByVal item As UserReportInfo)
        If item Is Nothing Then Exit Sub
        ' CommandDownloadUserReport.TheCommand(item.FileName)
        _QueryManager.InvokeQuery(Of CommandDownloadUserReport)(Nothing, _
            "TheCommand", True, AddressOf OnItemDownloaded, item.FileName)
    End Sub

    Private Sub OnItemDownloaded(ByVal result As Object, ByVal exceptionHandled As Boolean)
        If exceptionHandled Then Exit Sub

        If result Is Nothing OrElse Not TypeOf result Is String OrElse _
            StringIsNullOrEmpty(DirectCast(result, String)) Then
            MsgBox("Klaida. Nepavyko parsisiųsti ataskaitos.", _
                MsgBoxStyle.Exclamation, "Klaida.")
            Exit Sub
        End If

        Dim fileName As String = ""

        Using sfd As New SaveFileDialog
            sfd.Filter = "rdl failai|*.rdl|Visi failai|*.*"
            sfd.CheckFileExists = False
            sfd.AddExtension = True
            sfd.DefaultExt = ".rdl"
            sfd.FileName = "report.rdl"
            If sfd.ShowDialog() <> Windows.Forms.DialogResult.OK Then Exit Sub
            fileName = sfd.FileName.Trim
        End Using

        If StringIsNullOrEmpty(fileName) Then Exit Sub

        Try
            IO.File.WriteAllText(fileName, DirectCast(result, String), _
                System.Text.Encoding.UTF8)
        Catch ex As Exception
            ShowError(New Exception(String.Format("Klaida. Nepavyko išsaugoti failo: {0}", ex.Message), ex))
            Exit Sub
        End Try

        MsgBox("Failas sėkmingai išsaugotas.", MsgBoxStyle.Information, "Info")

    End Sub

    Private Sub DeleteItem(ByVal item As UserReportInfo)

        If item Is Nothing Then Exit Sub

        If Not YesOrNo("Ar tikrai norite pašalinti ataskaitą?") Then Exit Sub

        ' CommandDeleteUserReport.TheCommand(item.FileName)
        _QueryManager.InvokeQuery(Of CommandDeleteUserReport)(Nothing, _
            "TheCommand", False, AddressOf OnItemDeleted, item.FileName)

    End Sub

    Private Sub OnItemDeleted(ByVal result As Object, ByVal exceptionHandled As Boolean)
        If exceptionHandled Then Exit Sub
        If Not YesOrNo("Ataskaita sėkmingai pašalinta. Atnaujinti sąrašą?") Then Exit Sub
        RefreshButton.PerformClick()
    End Sub


    Private Sub UploadUserReportButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles UploadUserReportButton.Click

        Dim fileName As String = ""

        Using ofd As New OpenFileDialog
            ofd.InitialDirectory = AppPath()
            ofd.Multiselect = False
            ofd.Filter = "rdl failai|*.rdl|Visi failai|*.*"
            If ofd.ShowDialog() <> Windows.Forms.DialogResult.OK Then Exit Sub
            fileName = ofd.FileName.Trim
        End Using

        If StringIsNullOrEmpty(fileName) Then Exit Sub

        ' CommandUploadUserReport.TheCommand(fileName, System.Text.Encoding.UTF8)
        _QueryManager.InvokeQuery(Of CommandUploadUserReport)(Nothing, _
            "TheCommand", True, AddressOf OnItemUploaded, fileName, _
            System.Text.Encoding.UTF8)

    End Sub

    Private Sub OnItemUploaded(ByVal result As Object, ByVal exceptionHandled As Boolean)
        If exceptionHandled Then Exit Sub
        If Not YesOrNo("Nauja ataskaita sėkmingai įkelta. Atnaujinti sąrašą?") Then Exit Sub
        RefreshButton.PerformClick()
    End Sub

End Class