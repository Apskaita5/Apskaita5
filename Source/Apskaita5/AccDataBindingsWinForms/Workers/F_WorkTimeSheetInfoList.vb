Imports ApskaitaObjects.Workers
Imports ApskaitaObjects.ActiveReports
Imports AccControlsWinForms
Imports AccDataBindingsWinForms.Printing

Friend Class F_WorkTimeSheetInfoList
    Implements ISupportsPrinting

    Private _FormManager As CslaActionExtenderReportForm(Of WorkTimeSheetInfoList)
    Private _ListViewManager As DataListViewEditControlManager(Of WorkTimeSheetInfo)
    Private _QueryManager As CslaActionExtenderQueryObject


    Private Sub F_WorkTimeSheetInfoList_Load(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load

        If Not SetDataSources() Then Exit Sub

    End Sub

    Private Function SetDataSources() As Boolean

        Try

            _ListViewManager = New DataListViewEditControlManager(Of WorkTimeSheetInfo) _
                (WorkTimeSheetInfoListDataListView, ContextMenuStrip1, Nothing, _
                 Nothing, Nothing, Nothing)

            _ListViewManager.AddCancelButton = True
            _ListViewManager.AddButtonHandler("Keisti", "Keisti darbo laiko apskaitos žiniaraščio duomenis.", _
                AddressOf ChangeItem)
            _ListViewManager.AddButtonHandler("Ištrinti", "Pašalinti darbo laiko apskaitos žiniaraščio duomenis iš duomenų bazės.", _
                AddressOf DeleteItem)

            _ListViewManager.AddMenuItemHandler(ChangeItem_MenuItem, AddressOf ChangeItem)
            _ListViewManager.AddMenuItemHandler(DeleteItem_MenuItem, AddressOf DeleteItem)
            _ListViewManager.AddMenuItemHandler(NewItem_MenuItem, AddressOf NewItem)

            _QueryManager = New CslaActionExtenderQueryObject(Me, ProgressFiller2)

            ' WorkTimeSheetInfoList.GetWorkTimeSheetInfoList(dateFrom, dateTo)
            _FormManager = New CslaActionExtenderReportForm(Of WorkTimeSheetInfoList) _
                (Me, WorkTimeSheetInfoListBindingSource, Nothing, Nothing, RefreshButton, _
                 ProgressFiller1, "GetWorkTimeSheetInfoList", AddressOf GetReportParams)

            _FormManager.ManageDataListViewStates(WorkTimeSheetInfoListDataListView)

        Catch ex As Exception
            ShowError(ex, Nothing)
            DisableAllControls(Me)
            Return False
        End Try

        RefreshButton.Enabled = WorkTimeSheetInfoList.CanGetObject
        NewButton.Enabled = Workers.WorkTimeSheet.CanAddObject

        DateFromAccDatePicker.Value = Today.AddMonths(-3)
        Dim dateTo As Date = Today.AddMonths(-1)
        dateTo = New Date(dateTo.Year, dateTo.Month, Date.DaysInMonth(dateTo.Year, dateTo.Month))
        DateToAccDatePicker.Value = dateTo

        Return True

    End Function


    Private Function GetReportParams() As Object()
        ' WorkTimeSheetInfoList.GetWorkTimeSheetInfoList(DateFromDateTimePicker.Value, DateToDateTimePicker.Value)
        Return New Object() {DateFromAccDatePicker.Value, DateToAccDatePicker.Value}
    End Function

    Private Sub NewButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles NewButton.Click
        NewItem(Nothing)
    End Sub

    Private Sub ChangeItem(ByVal item As WorkTimeSheetInfo)
        If item Is Nothing Then Exit Sub
        ' WorkTimeSheet.GetWorkTimeSheet(item.ID)
        _QueryManager.InvokeQuery(Of WorkTimeSheet)(Nothing, "GetWorkTimeSheet", True, _
            AddressOf OpenObjectEditForm, item.ID)
    End Sub

    Private Sub DeleteItem(ByVal item As WorkTimeSheetInfo)

        If item Is Nothing Then Exit Sub

        If CheckIfObjectEditFormOpen(Of WorkTimeSheet)(item.ID, True, True) Then Exit Sub

        If Not YesOrNo("Ar tikrai norite pašalinti darbo laiko žiniaraščio duomenis?") Then Exit Sub

        ' WorkTimeSheet.DeleteWorkTimeSheet(item.ID)
        _QueryManager.InvokeQuery(Of WorkTimeSheet)(Nothing, "DeleteWorkTimeSheet", False, _
            AddressOf OnItemDeleted, item.ID)

    End Sub

    Private Sub OnItemDeleted(ByVal result As Object, ByVal exceptionHandled As Boolean)
        If exceptionHandled Then Exit Sub
        If Not YesOrNo("Darbo laiko apskaitos žiniaraščio duomenys sėkmingai pašalinti. Atnaujinti sąrašą?") Then Exit Sub
        RefreshButton.PerformClick()
    End Sub

    Private Sub NewItem(ByVal item As WorkTimeSheetInfo)
        OpenNewForm(Of WorkTimeSheet)()
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
            PrintObject(_FormManager.DataSource, False, 0, "TabeliuSarasas", Me, _
                _ListViewManager.GetCurrentFilterDescription(), _
                _ListViewManager.GetDisplayOrderIndexes())
        Catch ex As Exception
            ShowError(ex, _FormManager.DataSource)
        End Try
    End Sub

    Public Sub OnPrintPreviewClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Implements ISupportsPrinting.OnPrintPreviewClick
        If _FormManager.DataSource Is Nothing Then Exit Sub
        Try
            PrintObject(_FormManager.DataSource, True, 0, "TabeliuSarasas", Me, _
                _ListViewManager.GetCurrentFilterDescription(), _
                _ListViewManager.GetDisplayOrderIndexes())
        Catch ex As Exception
            ShowError(ex, _FormManager.DataSource)
        End Try
    End Sub

    Public Function SupportsEmailing() As Boolean _
        Implements ISupportsPrinting.SupportsEmailing
        Return True
    End Function

End Class