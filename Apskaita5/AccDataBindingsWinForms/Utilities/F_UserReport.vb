Imports AccControlsWinForms
Imports ApskaitaObjects.HelperLists
Imports ApskaitaObjects.ActiveReports
Imports AccDataBindingsWinForms.CachedInfoLists

Public Class F_UserReport

    Private ReadOnly _RequiredCachedLists As Type() = New Type() _
        {GetType(UserReportInfoList)}

    Private _CurrentReport As UserReport = Nothing
    Private _QueryManager As CslaActionExtenderQueryObject = Nothing
    Private _Loading As Boolean = True

    Private Sub F_UserReport_Activated(ByVal sender As Object, _
        ByVal e As System.EventArgs) Handles Me.Activated

        If _Loading Then
            _Loading = False
            Exit Sub
        End If

        PrepareCache(Me, _RequiredCachedLists)

    End Sub

    Private Sub F_UserReport_FormClosing(ByVal sender As Object, _
        ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Not _CurrentReport Is Nothing AndAlso _
            Not _CurrentReport.ReportDataSet Is Nothing Then
            _CurrentReport.ReportDataSet.Dispose()
            _CurrentReport = Nothing
        End If
        MyCustomSettings.GetFormLayout(Me)
    End Sub

    Private Sub F_UserReport_Load(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load

        If Not SetDataSources() Then Exit Sub


    End Sub

    Private Function SetDataSources() As Boolean

        If Not PrepareCache(Me, _RequiredCachedLists) Then Return False

        Try

            LoadUserReportInfoListToListCombo(UserReportInfoListAccListComboBox)

            _QueryManager = New CslaActionExtenderQueryObject(Me, ProgressFiller1)

        Catch ex As Exception
            ShowError(ex)
            DisableAllControls(Me)
            Return False
        End Try

        MyCustomSettings.SetFormLayout(Me)

        Return True

    End Function


    Private Sub RefreshButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles RefreshButton.Click

        Dim report As UserReportInfo = Nothing
        Try
            report = DirectCast(UserReportInfoListAccListComboBox.SelectedValue, UserReportInfo)
        Catch ex As Exception
        End Try
        If report Is Nothing Then
            MsgBox("Klaida. Nepasirinktas ataskaitos tipas.", MsgBoxStyle.Exclamation, "Klaida.")
            Exit Sub
        End If

        Dim params As List(Of KeyValuePair(Of String, Object)) = Nothing

        If report.Params.Count > 0 Then
            Using frm As New F_UserReportParamsDialog(report)
                If frm.ShowDialog() <> DialogResult.OK Then Exit Sub
                params = frm.Params
            End Using
        End If

        ' UserReport.GetUserReport(report, params)
        _QueryManager.InvokeQuery(Of UserReport)(Nothing, "GetUserReport", True, _
            AddressOf OnReportFetched, report, params)

    End Sub

    Private Sub OnReportFetched(ByVal result As Object, ByVal exceptionHandled As Boolean)

        If result Is Nothing Then Exit Sub

        If Not _CurrentReport Is Nothing AndAlso _
            Not _CurrentReport.ReportDataSet Is Nothing Then
            _CurrentReport.ReportDataSet.Dispose()
            _CurrentReport = Nothing
        End If

        _CurrentReport = DirectCast(result, UserReport)

        Try
            Using busy As New StatusBusy

                Me.RdlViewer1.SourceRdl = _CurrentReport.ReportRdlContent

                For Each table As DataTable In _CurrentReport.ReportDataSet.Tables
                    Me.RdlViewer1.Report.DataSets(table.TableName).SetData(table)
                Next

                Dim paramString As String = ""
                For Each param As KeyValuePair(Of String, Object) In _CurrentReport.ReportParams
                    If param.Value Is Nothing Then
                        paramString = paramString & String.Format("&{0}={1}", _
                        param.Key, "null")
                    Else
                        paramString = paramString & String.Format("&{0}={1}", _
                            param.Key, param.Value.ToString)
                    End If
                Next
                Me.RdlViewer1.Parameters = paramString

                Me.RdlViewer1.Rebuild()

            End Using
        Catch ex As Exception
            ShowError(ex)
        End Try

    End Sub

End Class