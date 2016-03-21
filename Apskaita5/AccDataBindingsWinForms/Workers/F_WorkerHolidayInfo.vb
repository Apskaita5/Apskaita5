Imports ApskaitaObjects.Workers
Imports ApskaitaObjects.ActiveReports
Imports ApskaitaObjects.HelperLists
Imports AccControlsWinForms
Imports AccDataBindingsWinForms.Printing
Imports AccDataBindingsWinForms.CachedInfoLists

Friend Class F_WorkerHolidayInfo
    Implements ISupportsPrinting

    Private ReadOnly _RequiredCachedLists As Type() = New Type() _
        {GetType(HelperLists.PersonInfoList)}

    Private _FormManager As CslaActionExtenderReportForm(Of WorkerHolidayInfo)
    Private _CalculatedListViewManager As DataListViewEditControlManager(Of HolidayCalculationPeriod)
    Private _SpentListViewManager As DataListViewEditControlManager(Of HolidaySpentItem)
    Private _QueryManager As CslaActionExtenderQueryObject


    Private Sub F_WorkerHolidayInfo_Load(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load

        If Not SetDataSources() Then Exit Sub

    End Sub

    Private Function SetDataSources() As Boolean

        If Not PrepareCache(Me, _RequiredCachedLists) Then Return False

        Try

            _CalculatedListViewManager = New DataListViewEditControlManager(Of HolidayCalculationPeriod) _
                (HolidayCalculatedListDataListView, Nothing, Nothing, Nothing, Nothing)

            _SpentListViewManager = New DataListViewEditControlManager(Of HolidaySpentItem) _
                (HolidaySpentListDataListView, Nothing, Nothing, Nothing, Nothing)

            _SpentListViewManager.AddCancelButton = False
            _SpentListViewManager.AddButtonHandler("Dokumentas", "Rodyti dokumentą.", _
                AddressOf ShowHolidayAffectingDocument)

            _QueryManager = New CslaActionExtenderQueryObject(Me, ProgressFiller2)

            ' WorkerHolidayInfo.GetWorkerHolidayInfo(nDate, nSerial, nNumber, isForCompensation)
            _FormManager = New CslaActionExtenderReportForm(Of WorkerHolidayInfo) _
                (Me, WorkerHolidayInfoBindingSource, Nothing, _RequiredCachedLists, RefreshButton, _
                 ProgressFiller1, "GetWorkerHolidayInfo", AddressOf GetReportParams)

            _FormManager.ManageDataListViewStates(HolidayCalculatedListDataListView)
            _FormManager.ManageDataListViewStates(HolidaySpentListDataListView)

            LoadPersonInfoListToListCombo(WorkerAccGridComboBox, True, False, False, True)

        Catch ex As Exception
            ShowError(ex)
            DisableAllControls(Me)
            Return False
        End Try

        RefreshButton.Enabled = WorkerHolidayInfo.CanGetObject

        Return True

    End Function


    Private Function GetReportParams() As Object()

        Dim nSerial As String = CType(LabourContractComboBox.SelectedItem, ShortLabourContract).Serial.Trim
        Dim nNumber As Integer = CType(LabourContractComboBox.SelectedItem, ShortLabourContract).Number
        Try
            nSerial = CType(LabourContractComboBox.SelectedItem, ShortLabourContract).Serial.Trim
            nNumber = CType(LabourContractComboBox.SelectedItem, ShortLabourContract).Number
        Catch ex As Exception
        End Try

        ' WorkerHolidayInfo.GetWorkerHolidayInfo(DateDateTimePicker.Value, nSerial, nNumber, ForCompensationCheckBox.Checked)
        Return New Object() {DateDateTimePicker.Value, nSerial, nNumber, ForCompensationCheckBox.Checked}

    End Function

    Private Sub ShowHolidayAffectingDocument(ByVal item As HolidaySpentItem)

        If item Is Nothing OrElse Not item.DocumentID > 0 Then Exit Sub

        If item.Type = HolidaySpentItemType.Spent OrElse _
            item.Type = HolidaySpentItemType.Compensated Then
            ' WageSheet.GetWageSheet(item.DocumentID)
            _QueryManager.InvokeQuery(Of WageSheet)(Nothing, "GetWageSheet", True, _
                AddressOf OpenObjectEditForm, item.DocumentID)
        ElseIf item.Type = HolidaySpentItemType.Correction AndAlso _
            item.DocumentDate.Date = _FormManager.DataSource.ContractDate.Date Then
            ' Contract.GetContract(item.DocumentID)
            _QueryManager.InvokeQuery(Of Contract)(Nothing, "GetContract", True, _
                AddressOf OpenObjectEditForm, item.DocumentID)
        ElseIf item.Type = HolidaySpentItemType.Correction Then
            ' ContractUpdate.GetContractUpdate(item.DocumentID)
            _QueryManager.InvokeQuery(Of ContractUpdate)(Nothing, "GetContractUpdate", True, _
                AddressOf OpenObjectEditForm, item.DocumentID)
        End If

    End Sub

    Private Sub RefreshLabourContractsButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles RefreshLabourContractsButton.Click

        Dim currentWorker As HelperLists.PersonInfo = Nothing
        Try
            currentWorker = DirectCast(WorkerAccGridComboBox.SelectedValue, HelperLists.PersonInfo)
        Catch ex As Exception
        End Try
        If currentWorker Is Nothing OrElse currentWorker.IsEmpty Then
            MsgBox("Klaida. Nepasirinktas darbuotojas.", MsgBoxStyle.Exclamation, "Klaida.")
            Exit Sub
        End If

        ' ShortLabourContractList.GetList(currentWorker.ID)
        _QueryManager.InvokeQuery(Of ShortLabourContractList)(Nothing, "GetList", True, _
            AddressOf OnContractsFetched, currentWorker.ID)

    End Sub

    Private Sub OnContractsFetched(ByVal result As Object, ByVal exceptionHandled As Boolean)

        If result Is Nothing Then Exit Sub

        Dim contractList As ShortLabourContractList = DirectCast(result, ShortLabourContractList)

        LabourContractComboBox.DataSource = Nothing
        LabourContractComboBox.DataSource = contractList
        If contractList.Count > 0 Then
            LabourContractComboBox.SelectedIndex = contractList.Count - 1
        Else
            LabourContractComboBox.SelectedIndex = -1
            MsgBox("Klaida. Šiam darbuotojui nėra registruotų darbo sutarčių.", _
                MsgBoxStyle.Exclamation, "Klaida.")
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

        If _FormManager.DataSource Is Nothing Then Exit Sub

        Using frm As New F_SendObjToEmail(_FormManager.DataSource, 0)
            frm.ShowDialog()
        End Using

    End Sub

    Public Sub OnPrintClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Implements ISupportsPrinting.OnPrintClick

        If _FormManager.DataSource Is Nothing Then Exit Sub

        Try
            PrintObject(_FormManager.DataSource, False, 0, "PazymaSukauptosAtostogos", Me, _
                _CalculatedListViewManager.GetCurrentFilterDescription() & vbCrLf _
                & _SpentListViewManager.GetCurrentFilterDescription(), _
                _CalculatedListViewManager.GetDisplayOrderIndexes(), _
                _SpentListViewManager.GetDisplayOrderIndexes())
        Catch ex As Exception
            ShowError(ex)
        End Try

    End Sub

    Public Sub OnPrintPreviewClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Implements ISupportsPrinting.OnPrintPreviewClick

        If _FormManager.DataSource Is Nothing Then Exit Sub

        Try
            PrintObject(_FormManager.DataSource, True, 0, "PazymaSukauptosAtostogos", Me, _
                _CalculatedListViewManager.GetCurrentFilterDescription() & vbCrLf _
                & _SpentListViewManager.GetCurrentFilterDescription(), _
                _CalculatedListViewManager.GetDisplayOrderIndexes(), _
                _SpentListViewManager.GetDisplayOrderIndexes())
        Catch ex As Exception
            ShowError(ex)
        End Try

    End Sub

    Public Function SupportsEmailing() As Boolean _
        Implements ISupportsPrinting.SupportsEmailing
        Return True
    End Function

End Class