Imports ApskaitaObjects.ActiveReports
Imports ApskaitaObjects.HelperLists

Public Class F_WorkerHolidayInfo
    Implements ISupportsPrinting

    Private Obj As WorkerHolidayInfo = Nothing
    Private Loading As Boolean = True


    Private Sub F_WorkerHolidayInfo_Activated(ByVal sender As Object, _
        ByVal e As System.EventArgs) Handles Me.Activated

        If Me.WindowState = FormWindowState.Maximized AndAlso MyCustomSettings.AutoSizeForm Then _
            Me.WindowState = FormWindowState.Normal

        If Loading Then
            Loading = False
            Exit Sub
        End If

        If Not PrepareCache(Me, GetType(HelperLists.PersonInfoList)) Then Exit Sub

    End Sub

    Private Sub F_WorkerHolidayInfo_FormClosing(ByVal sender As Object, _
        ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        GetDataGridViewLayOut(HolidayCalculatedListDataGridView)
        GetDataGridViewLayOut(HolidaySpentListDataGridView)
        GetFormLayout(Me)

    End Sub

    Private Sub F_WorkerHolidayInfo_Load(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load

        If Not SetDataSources() Then Exit Sub

        AddDGVColumnSelector(HolidayCalculatedListDataGridView)
        AddDGVColumnSelector(HolidaySpentListDataGridView)

        SetDataGridViewLayOut(HolidayCalculatedListDataGridView)
        SetDataGridViewLayOut(HolidaySpentListDataGridView)
        SetFormLayout(Me)

    End Sub


    Private Sub RefreshButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles RefreshButton.Click

        If LabourContractComboBox.SelectedItem Is Nothing OrElse _
            Not DirectCast(LabourContractComboBox.SelectedItem, ShortLabourContract).Number > 0 Then
            MsgBox("Klaida. Nepasirinkta darbo sutartis.", MsgBoxStyle.Exclamation, "Klaida.")
            Exit Sub
        End If

        Dim nSerial As String = CType(LabourContractComboBox.SelectedItem, ShortLabourContract).Serial.Trim
        Dim nNumber As Integer = CType(LabourContractComboBox.SelectedItem, ShortLabourContract).Number

        Using bm As New BindingsManager(WorkerHolidayInfoBindingSource, _
            HolidayCalculatedListBindingSource, HolidaySpentListBindingSource, False, True)

            Try
                Obj = LoadObject(Of WorkerHolidayInfo)(Nothing, "GetWorkerHolidayInfo", True, _
                    DateDateTimePicker.Value, nSerial, nNumber, ForCompensationCheckBox.Checked)
            Catch ex As Exception
                ShowError(ex)
                Exit Sub
            End Try

            bm.SetNewDataSource(Obj)

        End Using

    End Sub

    Private Sub RefreshLabourContractsButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles RefreshLabourContractsButton.Click

        Dim currentWorker As HelperLists.PersonInfo = Nothing
        Try
            currentWorker = DirectCast(WorkerAccGridComboBox.SelectedValue, HelperLists.PersonInfo)
        Catch ex As Exception
        End Try
        If currentWorker Is Nothing OrElse Not currentWorker.ID > 0 Then
            MsgBox("Klaida. Nepasirinktas darbuotojas.", MsgBoxStyle.Exclamation, "Klaida.")
            Exit Sub
        End If

        Dim contractList As ShortLabourContractList
        Try
            contractList = LoadObject(Of ShortLabourContractList) _
                (Nothing, "GetList", True, currentWorker.ID)
        Catch ex As Exception
            ShowError(ex)
            Exit Sub
        End Try

        LabourContractComboBox.DataSource = Nothing
        LabourContractComboBox.DataSource = contractList
        LabourContractComboBox.SelectedIndex = -1

        MsgBox("Darbo sutarčių sąrašas gautas.", MsgBoxStyle.Information, "Info")

    End Sub

    Private Sub HolidaySpentListDataGridView_CellDoubleClick(ByVal sender As System.Object, _
        ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles HolidaySpentListDataGridView.CellDoubleClick

        If Obj Is Nothing OrElse e.RowIndex < 0 Then Exit Sub

        Dim selectedItem As HolidaySpentItem = Nothing
        Try
            selectedItem = DirectCast(HolidaySpentListDataGridView.Rows(e.RowIndex).DataBoundItem, HolidaySpentItem)
        Catch ex As Exception
        End Try

        If selectedItem Is Nothing OrElse Not selectedItem.DocumentID > 0 Then Exit Sub

        If selectedItem.Type = HolidaySpentItemType.Spent OrElse _
            selectedItem.Type = HolidaySpentItemType.Compensated Then
            MDIParent1.LaunchForm(GetType(F_WageSheet), False, False, _
                selectedItem.DocumentID, selectedItem.DocumentID)
        ElseIf selectedItem.Type = HolidaySpentItemType.Correction AndAlso _
            selectedItem.DocumentDate.Date = Obj.ContractDate.Date Then
            MDIParent1.LaunchForm(GetType(F_LabourContract), False, False, _
                selectedItem.DocumentID, selectedItem.DocumentID, False)
        ElseIf selectedItem.Type = HolidaySpentItemType.Correction Then
            MDIParent1.LaunchForm(GetType(F_LabourContractUpdate), False, False, _
                selectedItem.DocumentID, selectedItem.DocumentID)
        End If

    End Sub


    Public Function GetMailDropDownItems() As System.Windows.Forms.ToolStripDropDown _
        Implements ISupportsPrinting.GetMailDropDownItems
        Return Nothing
    End Function

    Public Function GetPrintDropDownItems() As System.Windows.Forms.ToolStripDropDown _
        Implements ISupportsPrinting.GetPrintDropDownItems
        Return Nothing
    End Function

    Public Function GetPrintPreviewDropDownItems() As System.Windows.Forms.ToolStripDropDown _
        Implements ISupportsPrinting.GetPrintPreviewDropDownItems
        Return Nothing
    End Function

    Public Sub OnMailClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Implements ISupportsPrinting.OnMailClick

        If Obj Is Nothing Then Exit Sub

        Using frm As New F_SendObjToEmail(Obj, 0)
            frm.ShowDialog()
        End Using

    End Sub

    Public Sub OnPrintClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Implements ISupportsPrinting.OnPrintClick

        If Obj Is Nothing Then Exit Sub

        Try
            PrintObject(Obj, False, 0)
        Catch ex As Exception
            ShowError(ex)
        End Try

    End Sub

    Public Sub OnPrintPreviewClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Implements ISupportsPrinting.OnPrintPreviewClick

        If Obj Is Nothing Then Exit Sub

        Try
            PrintObject(Obj, True, 0)
        Catch ex As Exception
            ShowError(ex)
        End Try

    End Sub

    Public Function SupportsEmailing() As Boolean _
        Implements ISupportsPrinting.SupportsEmailing
        Return True
    End Function


    Private Function SetDataSources() As Boolean

        If Not PrepareCache(Me, GetType(HelperLists.PersonInfoList)) Then Return False

        Try

            LoadPersonInfoListToGridCombo(WorkerAccGridComboBox, True, False, False, True)

        Catch ex As Exception
            ShowError(ex)
            DisableAllControls(Me)
            Return False
        End Try

        Return True

    End Function

End Class