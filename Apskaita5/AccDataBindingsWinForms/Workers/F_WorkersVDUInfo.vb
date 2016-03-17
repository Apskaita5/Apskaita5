Imports ApskaitaObjects.ActiveReports
Imports ApskaitaObjects.HelperLists
Imports AccControlsWinForms
Imports AccDataBindingsWinForms.Printing
Imports AccDataBindingsWinForms.CachedInfoLists

Friend Class F_WorkersVDUInfo
    Implements ISupportsPrinting

    Private ReadOnly _RequiredCachedLists As Type() = New Type() _
        {GetType(HelperLists.PersonInfoList)}

    Private _FormManager As CslaActionExtenderReportForm(Of WorkersVDUInfo)
    Private _WageListViewManager As DataListViewEditControlManager(Of WageVDUInfo)
    Private _BonusListViewManager As DataListViewEditControlManager(Of BonusVDUInfo)
    Private _QueryManager As CslaActionExtenderQueryObject


    Private Sub F_WorkersVDUInfo_Load(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load

        If Not SetDataSources() Then Exit Sub

    End Sub

    Private Function SetDataSources() As Boolean

        If Not PrepareCache(Me, _RequiredCachedLists) Then Return False

        Try

            _WageListViewManager = New DataListViewEditControlManager(Of WageVDUInfo) _
                (WageListDataListView, Nothing, Nothing, Nothing, Nothing)

            _BonusListViewManager = New DataListViewEditControlManager(Of BonusVDUInfo) _
                (BonusListListDataListView, Nothing, Nothing, Nothing, Nothing)

            '_SpentListViewManager.AddCancelButton = False
            '_SpentListViewManager.AddButtonHandler("Dokumentas", "Rodyti dokumentą.", _
            '    AddressOf ShowHolidayAffectingDocument)

            _QueryManager = New CslaActionExtenderQueryObject(Me, ProgressFiller2)

            ' WorkersVDUInfo.GetWorkersVDUInfo(nSerial, nNumber, nDate, includeCurrentMonth)
            _FormManager = New CslaActionExtenderReportForm(Of WorkersVDUInfo) _
                (Me, WorkersVDUInfoBindingSource, Nothing, _RequiredCachedLists, RefreshButton, _
                 ProgressFiller1, "GetWorkersVDUInfo", AddressOf GetReportParams)

            _FormManager.ManageDataListViewStates(BonusListListDataListView)
            _FormManager.ManageDataListViewStates(WageListDataListView)

            LoadPersonInfoListToListCombo(WorkerAccGridComboBox, True, False, False, True)

        Catch ex As Exception
            ShowError(ex)
            DisableAllControls(Me)
            Return False
        End Try

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

        ' WorkersVDUInfo.GetWorkersVDUInfo(nSerial, nNumber, DateDateTimePicker.Value, IncludeCurrentCheckBox.Checked)
        Return New Object() {nSerial, nNumber, DateDateTimePicker.Value, IncludeCurrentCheckBox.Checked}

    End Function

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
        _QueryManager.InvokeQuery(Of ShortLabourContractList)(Nothing, "", True, _
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
            PrintObject(_FormManager.DataSource, False, 0, "VduPazyma", Me, _
                _WageListViewManager.GetCurrentFilterDescription() & vbCrLf _
                & _BonusListViewManager.GetCurrentFilterDescription(), _
                _WageListViewManager.GetDisplayOrderIndexes(), _
                _BonusListViewManager.GetDisplayOrderIndexes())
        Catch ex As Exception
            ShowError(ex)
        End Try

    End Sub

    Public Sub OnPrintPreviewClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Implements ISupportsPrinting.OnPrintPreviewClick

        If _FormManager.DataSource Is Nothing Then Exit Sub

        Try
            PrintObject(_FormManager.DataSource, True, 0, "VduPazyma", Me, _
                _WageListViewManager.GetCurrentFilterDescription() & vbCrLf _
                & _BonusListViewManager.GetCurrentFilterDescription(), _
                _WageListViewManager.GetDisplayOrderIndexes(), _
                _BonusListViewManager.GetDisplayOrderIndexes())
        Catch ex As Exception
            ShowError(ex)
        End Try

    End Sub

    Public Function SupportsEmailing() As Boolean _
        Implements ISupportsPrinting.SupportsEmailing
        Return True
    End Function

End Class