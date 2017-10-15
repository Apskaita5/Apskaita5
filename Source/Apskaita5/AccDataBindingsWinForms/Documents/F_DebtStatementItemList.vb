Imports ApskaitaObjects.ActiveReports
Imports AccControlsWinForms
Imports AccDataBindingsWinForms.CachedInfoLists
Imports ApskaitaObjects.Attributes

Public Class F_DebtStatementItemList
    Implements ISupportsPrinting

    Private ReadOnly _RequiredCachedLists As Type() = New Type() _
        {GetType(HelperLists.AccountInfoList)}

    Private _FormManager As CslaActionExtenderReportForm(Of DebtStatementItemList)
    Private _ListViewManager As DataListViewEditControlManager(Of DebtStatementItem)
    Private _QueryManager As CslaActionExtenderQueryObject
    Private _EmailDropDown As Windows.Forms.ToolStripDropDown = Nothing


    Private Sub F_DebtStatementItemList_Load(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load
        If Not SetDataSources() Then Exit Sub
    End Sub

    Private Function SetDataSources() As Boolean

        If Not PrepareCache(Me, GetType(HelperLists.AccountInfoList)) Then Return False

        Try

            _ListViewManager = New DataListViewEditControlManager(Of DebtStatementItem) _
                (ReportDataListView, Nothing, Nothing, Nothing, Nothing, Nothing)

            _ListViewManager.AddCancelButton = False
            _ListViewManager.AddButtonHandler("Dokumentas", "Dokumento informacija", AddressOf ShowDetails)

            _FormManager = New CslaActionExtenderReportForm(Of DebtStatementItemList) _
                (Me, DebtStatementItemListBindingSource, Nothing, _RequiredCachedLists, _
                 RefreshButton, ProgressFiller1, "GetDebtStatementItemList", AddressOf GetReportParams)

            _FormManager.ManageDataListViewStates(ReportDataListView)

            _QueryManager = New CslaActionExtenderQueryObject(Me, ProgressFiller2)

            PrepareControl(AccountAccGridComboBox, New AccountFieldAttribute( _
                ValueRequiredLevel.Optional, False, 1, 2, 3, 4))

            DateFromDateTimePicker.Value = Today.AddYears(-1)
            AccountAccGridComboBox.SelectedValue = GetCurrentCompany.GetDefaultAccount( _
                General.DefaultAccountType.Buyers)

        Catch ex As Exception
            ShowError(ex)
            DisableAllControls(Me)
            Return False
        End Try

        Return True

    End Function


    Private Function GetReportParams() As Object()
        Return New Object() {DateFromDateTimePicker.Value.Date, DateToDateTimePicker.Value.Date, _
            AccountAccGridComboBox.SelectedValue}
    End Function

    Private Sub ShowDetails(ByVal item As DebtStatementItem)

        If item Is Nothing OrElse Not item.JournalEntryId > 0 Then Exit Sub

        OpenObjectEditForm(_QueryManager, item.JournalEntryId, item.DocumentType)

    End Sub

    Private Sub ReportDataListView_BeforeCreatingGroups(ByVal sender As Object, ByVal e As BrightIdeasSoftware.CreateGroupsEventArgs) Handles ReportDataListView.BeforeCreatingGroups
        e.Parameters.PrimarySort = Me.OlvColumn2
        e.Parameters.PrimarySortOrder = SortOrder.Ascending
        e.Parameters.SecondarySort = Me.OlvColumn4
        e.Parameters.SecondarySortOrder = SortOrder.Ascending
    End Sub

    Private Sub ReportDataListView_FormatCell(ByVal sender As Object, _
        ByVal e As BrightIdeasSoftware.FormatCellEventArgs) Handles ReportDataListView.FormatCell

        If _FormManager.DataSource Is Nothing Then Exit Sub

        If e.ColumnIndex = Me.OlvColumn2.Index AndAlso Not e.Model Is Nothing Then
            Dim item As DebtStatementItem = DirectCast(e.Model, DebtStatementItem)
            If item.Date.Date < _FormManager.DataSource.PeriodStart.Date OrElse _
                item.Date.Date > _FormManager.DataSource.PeriodEnd Then

                e.SubItem.Text = ""

            End If
        ElseIf e.ColumnIndex = Me.OlvColumn1.Index AndAlso Not e.Model Is Nothing Then
            e.SubItem.Text = ""
        End If

    End Sub


    Public Function GetMailDropDownItems() As System.Windows.Forms.ToolStripDropDown _
        Implements ISupportsPrinting.GetMailDropDownItems

        If _EmailDropDown Is Nothing Then
            _EmailDropDown = New ToolStripDropDown
            _EmailDropDown.Items.Add("Siųsti kontrahentams", Nothing, AddressOf OnMailClick)
        End If

        Return _EmailDropDown

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

        If GetSenderText(sender).ToLower.Contains("kontrahentams") Then

            DebtStatementItemListPrintView.EmailToPersons(_FormManager.DataSource, _
                StatementDateTimePicker.Value, SignWithFacsimileCheckBox.Checked)

        Else

            DebtStatementItemListPrintView.Email(_FormManager.DataSource, _
                StatementDateTimePicker.Value, SignWithFacsimileCheckBox.Checked)

        End If

    End Sub

    Public Sub OnPrintClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Implements ISupportsPrinting.OnPrintClick
        DebtStatementItemListPrintView.Print(_FormManager.DataSource, _
            StatementDateTimePicker.Value, SignWithFacsimileCheckBox.Checked, Me, False)
    End Sub

    Public Sub OnPrintPreviewClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Implements ISupportsPrinting.OnPrintPreviewClick
        DebtStatementItemListPrintView.Print(_FormManager.DataSource, _
            StatementDateTimePicker.Value, SignWithFacsimileCheckBox.Checked, Me, True)
    End Sub

    Public Function SupportsEmailing() As Boolean _
        Implements ISupportsPrinting.SupportsEmailing
        Return True
    End Function

End Class