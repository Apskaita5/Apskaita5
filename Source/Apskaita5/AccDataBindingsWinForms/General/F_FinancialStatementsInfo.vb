﻿Imports ApskaitaObjects.ActiveReports
Imports System.Drawing
Imports AccControlsWinForms
Imports AccDataBindingsWinForms.Printing
Imports AccDataBindingsWinForms.CachedInfoLists
Imports ApskaitaObjects.Attributes
Imports BrightIdeasSoftware

Friend Class F_FinancialStatementsInfo
    Implements ISupportsPrinting

    Private ReadOnly _RequiredCachedLists As Type() = New Type() _
        {GetType(HelperLists.AccountInfoList)}

    Private _FormManager As CslaActionExtenderReportForm(Of FinancialStatementsInfo)
    Private _AccountsListViewManager As DataListViewEditControlManager(Of AccountTurnoverInfo)
    Private _BalanceListViewManager As DataListViewEditControlManager(Of BalanceSheetInfo)
    Private _IncomeListViewManager As DataListViewEditControlManager(Of IncomeStatementInfo)

    Private _PrintDropDown As Windows.Forms.ToolStripDropDown = Nothing
    Private _PrintPreviewDropDown As Windows.Forms.ToolStripDropDown = Nothing
    Private _EmailDropDown As Windows.Forms.ToolStripDropDown = Nothing


    Private Sub F_FinancialStatementsInfo_Load(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load

        If Not SetDataSources() Then Exit Sub

        _FormManager = New CslaActionExtenderReportForm(Of FinancialStatementsInfo) _
            (Me, FinancialStatementsInfoBindingSource, Nothing, _RequiredCachedLists, _
            RefreshButton, ProgressFiller1, "GetFinancialStatementsInfo", _
            AddressOf GetReportParams)

        _FormManager.ManageDataListViewStates(AccountTurnoverListDataListView, _
            BalanceSheetDataListView, IncomeStatementDataListView)

    End Sub

    Private Function SetDataSources() As Boolean

        If Not PrepareCache(Me, _RequiredCachedLists) Then Return False

        Try

            PrepareControl(ClosingSummaryAccountAccGridComboBox, New AccountFieldAttribute( _
                ValueRequiredLevel.Optional, False, 3))
            ClosingSummaryAccountAccGridComboBox.SelectedValue = _
                GetCurrentCompany.GetDefaultAccount(General.DefaultAccountType.ClosingSummary)

            _AccountsListViewManager = New DataListViewEditControlManager(Of AccountTurnoverInfo) _
                (AccountTurnoverListDataListView, Nothing, Nothing, Nothing, Nothing, Nothing)

            _AccountsListViewManager.AddCancelButton = False
            _AccountsListViewManager.AddButtonHandler("Detaliau", "Rodyti sąskaitos detalią apyvartą", _
                AddressOf ShowItemDetails)

            _BalanceListViewManager = New DataListViewEditControlManager(Of BalanceSheetInfo) _
                (BalanceSheetDataListView, Nothing, Nothing, Nothing, Nothing, Nothing)

            _IncomeListViewManager = New DataListViewEditControlManager(Of IncomeStatementInfo) _
                (IncomeStatementDataListView, Nothing, Nothing, Nothing, Nothing, Nothing)

        Catch ex As Exception
            ShowError(ex, Nothing)
            DisableAllControls(Me)
            Return False
        End Try

        DateFirstPeriodStartAccDatePicker.Value = New Date(Today.Year - 1, 1, 1)
        DateSecondPeriodStartAccDatePicker.Value = New Date(Today.Year, 1, 1)

        Return True

    End Function


    Private Function GetReportParams() As Object()

        Dim selectedAccount As Long = 0
        Try
            selectedAccount = Convert.ToInt64(ClosingSummaryAccountAccGridComboBox.SelectedValue)
        Catch ex As Exception
        End Try

        Return New Object() {DateFirstPeriodStartAccDatePicker.Value.Date, _
            DateSecondPeriodStartAccDatePicker.Value.Date, _
            DateSecondPeriodEndAccDatePicker.Value.Date, selectedAccount, _
            IncludeWithoutTurnoverCheckBox.Checked}

    End Function

    Private Sub ShowItemDetails(ByVal item As AccountTurnoverInfo)
        If item Is Nothing Then Exit Sub
        ShowAccountTurnover(item.ID, _FormManager.DataSource.FirstPeriodDateStart, _
            _FormManager.DataSource.SecondPeriodDateEnd)
    End Sub

    Private Sub DataListView_FormatCell(ByVal sender As Object, _
        ByVal e As FormatCellEventArgs) Handles BalanceSheetDataListView.FormatCell

        If e.Column.AspectName <> "Name" AndAlso e.Column.AspectName <> "Number" Then Exit Sub

        Dim current As BalanceSheetInfo = Nothing
        Try
            current = CType(e.Model, BalanceSheetInfo)
        Catch ex As Exception
            ShowError(ex, e.Model)
            Exit Sub
        End Try
        If current Is Nothing Then Exit Sub

        Dim nPadding As Integer = (current.Level - 2) * 10
        If nPadding < 0 Then nPadding = 0
        e.SubItem.CellPadding = New Rectangle(nPadding, 0, 0, 0)
        If current.Level = 2 Then
            e.SubItem.BackColor = Color.LightSlateGray
            e.SubItem.Font = New Font(BalanceSheetDataListView.Font, FontStyle.Bold)
        End If

    End Sub

    Private Sub TabControl1_SelectedIndexChanged(ByVal sender As Object, _
        ByVal e As System.EventArgs) Handles TabControl1.SelectedIndexChanged
        If TabControl1.SelectedTab Is AccountTurnOverInfoListTabPage Then
            AccountTurnoverListDataListView.Select()
        ElseIf TabControl1.SelectedTab Is BalanceSheetTabPage Then
            BalanceSheetDataListView.Select()
        ElseIf TabControl1.SelectedTab Is IncomeStatementTabPage Then
            IncomeStatementDataListView.Select()
        End If
    End Sub


    Public Function GetMailDropDownItems() As System.Windows.Forms.ToolStripDropDown _
        Implements ISupportsPrinting.GetMailDropDownItems

        If _EmailDropDown Is Nothing Then
            _EmailDropDown = New ToolStripDropDown
            _EmailDropDown.Items.Add("Siųsti išplėstinį žiniaraštį", Nothing, AddressOf OnMailClick)
        End If

        Return _EmailDropDown

    End Function

    Public Function GetPrintDropDownItems() As System.Windows.Forms.ToolStripDropDown _
        Implements ISupportsPrinting.GetPrintDropDownItems

        If _PrintDropDown Is Nothing Then
            _PrintDropDown = New ToolStripDropDown
            _PrintDropDown.Items.Add("Spausdinti išplėstinį žiniaraštį", Nothing, AddressOf OnPrintClick)
        End If

        Return _PrintDropDown

    End Function

    Public Function GetPrintPreviewDropDownItems() As System.Windows.Forms.ToolStripDropDown _
        Implements ISupportsPrinting.GetPrintPreviewDropDownItems

        If _PrintPreviewDropDown Is Nothing Then
            _PrintPreviewDropDown = New ToolStripDropDown
            _PrintPreviewDropDown.Items.Add("Spausdinti išplėstinį žiniaraštį", Nothing, AddressOf OnPrintPreviewClick)
        End If

        Return _PrintPreviewDropDown

    End Function

    Public Sub OnMailClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Implements ISupportsPrinting.OnMailClick

        If _FormManager.DataSource Is Nothing Then Exit Sub

        Dim version As Integer = TabControl1.SelectedIndex + 1
        If TabControl1.SelectedIndex = 0 AndAlso GetSenderText(sender).ToLower.Contains("išplėstinį") Then
            version = 0
        End If

        Using frm As New F_SendObjToEmail(_FormManager.DataSource, version)
            frm.ShowDialog()
        End Using

    End Sub

    Public Sub OnPrintClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Implements ISupportsPrinting.OnPrintClick

        If _FormManager.DataSource Is Nothing Then Exit Sub

        Dim version As Integer = 0
        Dim filterDescription As String = ""
        Dim visibleIndexes As List(Of Integer) = Nothing
        Dim exportFileName As String = GetExportFileName(GetSenderText(sender), _
            version, filterDescription, visibleIndexes)

        Try
            If visibleIndexes Is Nothing Then
                PrintObject(_FormManager.DataSource, False, version, exportFileName, _
                    Me, filterDescription)
            Else
                PrintObject(_FormManager.DataSource, False, version, exportFileName, _
                    Me, filterDescription, visibleIndexes)
            End If
        Catch ex As Exception
            ShowError(ex, New Object() {_FormManager.DataSource, visibleIndexes})
        End Try

    End Sub

    Public Sub OnPrintPreviewClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Implements ISupportsPrinting.OnPrintPreviewClick

        If _FormManager.DataSource Is Nothing Then Exit Sub

        Dim version As Integer = 0
        Dim filterDescription As String = ""
        Dim visibleIndexes As List(Of Integer) = Nothing
        Dim exportFileName As String = GetExportFileName(GetSenderText(sender), _
            version, filterDescription, visibleIndexes)

        Try
            If visibleIndexes Is Nothing Then
                PrintObject(_FormManager.DataSource, True, version, exportFileName, _
                    Me, filterDescription)
            Else
                PrintObject(_FormManager.DataSource, True, version, exportFileName, _
                    Me, filterDescription, visibleIndexes)
            End If
        Catch ex As Exception
            ShowError(ex, New Object() {_FormManager.DataSource, visibleIndexes})
        End Try

    End Sub

    Public Function SupportsEmailing() As Boolean _
        Implements ISupportsPrinting.SupportsEmailing
        Return True
    End Function

    Private Function GetExportFileName(ByVal senderText As String, _
        ByRef version As Integer, ByRef filterDescription As String, _
        ByRef visibleIndexes As List(Of Integer)) As String

        Dim result As String = ""

        If TabControl1.SelectedTab Is AccountTurnOverInfoListTabPage Then
            result = "SaskaituApyvartos"
            If senderText.ToLower.Contains("išplėstinį") Then
                version = 0
            Else
                version = 1
            End If
            filterDescription = _AccountsListViewManager.GetCurrentFilterDescription()
            visibleIndexes = _AccountsListViewManager.GetDisplayOrderIndexes()
        ElseIf TabControl1.SelectedTab Is BalanceSheetTabPage Then
            result = "balansas"
            version = 2
            filterDescription = _BalanceListViewManager.GetCurrentFilterDescription()
            visibleIndexes = _BalanceListViewManager.GetDisplayOrderIndexes()
        ElseIf TabControl1.SelectedTab Is IncomeStatementTabPage Then
            result = "pelno_nuostolio_ataskaita"
            version = 3
            filterDescription = _IncomeListViewManager.GetCurrentFilterDescription()
            visibleIndexes = _IncomeListViewManager.GetDisplayOrderIndexes()
        End If

        Return result

    End Function

End Class