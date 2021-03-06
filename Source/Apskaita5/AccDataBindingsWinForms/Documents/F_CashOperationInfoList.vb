﻿Imports ApskaitaObjects.ActiveReports
Imports ApskaitaObjects.HelperLists
Imports ApskaitaObjects.Documents
Imports AccControlsWinForms
Imports AccDataBindingsWinForms.CachedInfoLists
Imports AccDataBindingsWinForms.Printing
Imports ApskaitaObjects.Attributes

Friend Class F_CashOperationInfoList
    Implements ISupportsPrinting

    Private ReadOnly _RequiredCachedLists As Type() = New Type() _
        {GetType(HelperLists.PersonInfoList), GetType(HelperLists.CashAccountInfoList)}

    Private _FormManager As CslaActionExtenderReportForm(Of CashOperationInfoList)
    Private _ListViewManager As DataListViewEditControlManager(Of CashOperationInfo)
    Private _QueryManager As CslaActionExtenderQueryObject

    Private PrintDropDown As Windows.Forms.ToolStripDropDown = Nothing
    Private PrintPreviewDropDown As Windows.Forms.ToolStripDropDown = Nothing
    Private EmailDropDown As Windows.Forms.ToolStripDropDown = Nothing


    Private Sub F_CashOperationInfoList_Load(ByVal sender As Object, _
        ByVal e As System.EventArgs) Handles Me.Load

        If Not SetDataSources() Then Exit Sub

    End Sub

    Private Function SetDataSources() As Boolean

        If Not PrepareCache(Me, _RequiredCachedLists) Then Exit Function

        Try

            _ListViewManager = New DataListViewEditControlManager(Of CashOperationInfo) _
                (CashOperationInfoListDataListView, ContextMenuStrip1, Nothing, _
                 Nothing, Nothing, Nothing)

            _ListViewManager.AddCancelButton = True
            _ListViewManager.AddButtonHandler("Keisti", "Keisti mokėjimo dokumento duomenis.", _
                AddressOf ChangeItem)
            _ListViewManager.AddButtonHandler("Ištrinti", "Pašalinti mokėjimo dokumento duomenis iš duomenų bazės.", _
                AddressOf DeleteItem)

            _ListViewManager.AddMenuItemHandler(ChangeItem_MenuItem, AddressOf ChangeItem)
            _ListViewManager.AddMenuItemHandler(DeleteItem_MenuItem, AddressOf DeleteItem)

            _QueryManager = New CslaActionExtenderQueryObject(Me, ProgressFiller2)

            ' CashOperationInfoList.GetCashOperationInfoList(cashAccount, dateFrom, dateTo, personFilter)
            _FormManager = New CslaActionExtenderReportForm(Of CashOperationInfoList) _
                (Me, CashOperationInfoListBindingSource, Nothing, _RequiredCachedLists, RefreshButton, _
                 ProgressFiller1, "GetCashOperationInfoList", AddressOf GetReportParams)

            _FormManager.ManageDataListViewStates(CashOperationInfoListDataListView)

            PrepareControl(AccountAccGridComboBox, New CashAccountFieldAttribute( _
                ValueRequiredLevel.Optional, False))
            PrepareControl(PersonAccGridComboBox, New PersonFieldAttribute( _
                ValueRequiredLevel.Optional, True, True, True))

        Catch ex As Exception
            ShowError(ex, Nothing)
            DisableAllControls(Me)
            Return False
        End Try

        DateFromAccDatePicker.Value = Today.Subtract(New TimeSpan(30, 0, 0, 0))

        Return True

    End Function


    Private Function GetReportParams() As Object()

        Dim personFilter As HelperLists.PersonInfo = Nothing
        Try
            personFilter = DirectCast(PersonAccGridComboBox.SelectedValue, HelperLists.PersonInfo)
        Catch ex As Exception
        End Try

        Dim accountFilter As HelperLists.CashAccountInfo = Nothing
        Try
            accountFilter = DirectCast(AccountAccGridComboBox.SelectedValue, HelperLists.CashAccountInfo)
        Catch ex As Exception
        End Try

        'CashOperationInfoList.GetCashOperationInfoList(accountFilter, DateFromDateTimePicker.Value.Date, _
        '    DateToDateTimePicker.Value.Date, personFilter)
        Return New Object() {accountFilter, DateFromAccDatePicker.Value.Date, _
          DateToAccDatePicker.Value.Date, personFilter}

    End Function

    Private Sub ChangeItem(ByVal item As CashOperationInfo)

        If item Is Nothing Then Exit Sub

        If item.OperationType = DocumentType.BankOperation Then
            'BankOperation.GetBankOperation(item.ID)
            _QueryManager.InvokeQuery(Of BankOperation)(Nothing, "GetBankOperation", True, _
                AddressOf OpenObjectEditForm, item.ID)
        ElseIf item.OperationType = DocumentType.TillIncomeOrder Then
            'TillIncomeOrder.GetTillIncomeOrder(item.ID)
            _QueryManager.InvokeQuery(Of TillIncomeOrder)(Nothing, "GetTillIncomeOrder", True, _
                AddressOf OpenObjectEditForm, item.ID)
        ElseIf item.OperationType = DocumentType.TillSpendingOrder Then
            'TillSpendingsOrder.GetTillSpendingsOrder(item.ID)
            _QueryManager.InvokeQuery(Of TillSpendingsOrder)(Nothing, "GetTillSpendingsOrder", True, _
                AddressOf OpenObjectEditForm, item.ID)
        ElseIf item.OperationType = DocumentType.None Then
            OpenJournalEntryEditForm(_QueryManager, item.ID)
        Else
            MsgBox(String.Format("Klaida. Lėšų operacijos tipas '{0}' neimplementuotas.", _
                item.OperationTypeHumanReadable), MsgBoxStyle.Exclamation, "Klaida")
        End If

    End Sub

    Private Sub DeleteItem(ByVal item As CashOperationInfo)

        If item Is Nothing Then Exit Sub

        If item.OperationType = DocumentType.BankOperation Then
            If CheckIfObjectEditFormOpen(Of BankOperation)(item.ID, True, True) Then Exit Sub
        ElseIf item.OperationType = DocumentType.TillIncomeOrder Then
            If CheckIfObjectEditFormOpen(Of TillIncomeOrder)(item.ID, True, True) Then Exit Sub
        ElseIf item.OperationType = DocumentType.TillSpendingOrder Then
            If CheckIfObjectEditFormOpen(Of TillSpendingsOrder)(item.ID, True, True) Then Exit Sub
        ElseIf item.OperationType = DocumentType.None Then
            If CheckIfObjectEditFormOpen(Of General.JournalEntry)(item.ID, True, True) Then Exit Sub
        Else
            MsgBox(String.Format("Klaida. Lėšų operacijos tipas '{0}' neimplementuotas.", _
                item.OperationTypeHumanReadable), MsgBoxStyle.Exclamation, "Klaida")
        End If

        If Not YesOrNo("Ar tikrai norite pašalinti dokumento duomenis iš duomenų bazės?") Then Exit Sub

        If item.OperationType = DocumentType.BankOperation Then
            'BankOperation.DeleteBankOperation(item.ID)
            _QueryManager.InvokeQuery(Of BankOperation)(Nothing, "DeleteBankOperation", True, _
                AddressOf OnItemDeleted, item.ID)
        ElseIf item.OperationType = DocumentType.TillIncomeOrder Then
            'TillIncomeOrder.DeleteTillIncomeOrder(item.ID)
            _QueryManager.InvokeQuery(Of TillIncomeOrder)(Nothing, "DeleteTillIncomeOrder", True, _
                AddressOf OnItemDeleted, item.ID)
        ElseIf item.OperationType = DocumentType.TillSpendingOrder Then
            'TillSpendingsOrder.DeleteTillSpendingsOrder(item.ID)
            _QueryManager.InvokeQuery(Of TillSpendingsOrder)(Nothing, "DeleteTillSpendingsOrder", True, _
                AddressOf OnItemDeleted, item.ID)
        ElseIf item.OperationType = DocumentType.None Then
            'General.JournalEntry.DeleteJournalEntry(item.ID)
            _QueryManager.InvokeQuery(Of General.JournalEntry)(Nothing, "DeleteJournalEntry", True, _
                AddressOf OnItemDeleted, item.ID)
        Else
            MsgBox(String.Format("Klaida. Lėšų operacijos tipas '{0}' neimplementuotas.", _
                item.OperationTypeHumanReadable), MsgBoxStyle.Exclamation, "Klaida")
        End If

    End Sub

    Private Sub OnItemDeleted(ByVal result As Object, ByVal exceptionHandled As Boolean)
        If exceptionHandled Then Exit Sub
        If Not YesOrNo("Dokumento duomenys sėkmingai pašalinti iš įmonės duomenų bazės. Atnaujinti sąrašą?") Then Exit Sub
        RefreshButton.PerformClick()
    End Sub


    Public Function GetMailDropDownItems() As System.Windows.Forms.ToolStripDropDown _
       Implements ISupportsPrinting.GetMailDropDownItems

        If EmailDropDown Is Nothing Then
            EmailDropDown = New ToolStripDropDown
            EmailDropDown.Items.Add("Siųsti kasos knygą", Nothing, AddressOf OnMailClick)
        End If

        Return EmailDropDown

    End Function

    Public Function GetPrintDropDownItems() As System.Windows.Forms.ToolStripDropDown _
        Implements ISupportsPrinting.GetPrintDropDownItems

        If PrintDropDown Is Nothing Then
            PrintDropDown = New ToolStripDropDown
            PrintDropDown.Items.Add("Spausdinti kasos knygą", Nothing, AddressOf OnPrintClick)
        End If

        Return PrintDropDown

    End Function

    Public Function GetPrintPreviewDropDownItems() As System.Windows.Forms.ToolStripDropDown _
        Implements ISupportsPrinting.GetPrintPreviewDropDownItems

        If PrintPreviewDropDown Is Nothing Then
            PrintPreviewDropDown = New ToolStripDropDown
            PrintPreviewDropDown.Items.Add("Spausdinti kasos knygą", Nothing, AddressOf OnPrintPreviewClick)
        End If

        Return PrintPreviewDropDown

    End Function

    Public Sub OnMailClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Implements ISupportsPrinting.OnMailClick
        If _FormManager.DataSource Is Nothing Then Exit Sub

        Using frm As New F_SendObjToEmail(_FormManager.DataSource, _
            DirectCast(IIf(GetSenderText(sender).ToLower.Contains("kasos knygą"), 1, 0), Int32))
            frm.ShowDialog()
        End Using

    End Sub

    Public Sub OnPrintClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Implements ISupportsPrinting.OnPrintClick
        If _FormManager.DataSource Is Nothing Then Exit Sub
        Try
            PrintObject(_FormManager.DataSource, False, DirectCast(IIf(GetSenderText(sender).ToLower.Contains("kasos knygą"), 1, 0), Int32), _
                "LesuApyvarta", Me, _ListViewManager.GetCurrentFilterDescription(), _
                _ListViewManager.GetDisplayOrderIndexes())
        Catch ex As Exception
            ShowError(ex, _FormManager.DataSource)
        End Try
    End Sub

    Public Sub OnPrintPreviewClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Implements ISupportsPrinting.OnPrintPreviewClick
        If _FormManager.DataSource Is Nothing Then Exit Sub
        Try
            PrintObject(_FormManager.DataSource, True, DirectCast(IIf(GetSenderText(sender).ToLower.Contains("kasos knygą"), 1, 0), Int32), _
                "LesuApyvarta", Me, _ListViewManager.GetCurrentFilterDescription(), _
                _ListViewManager.GetDisplayOrderIndexes())
        Catch ex As Exception
            ShowError(ex, _FormManager.DataSource)
        End Try
    End Sub

    Public Function SupportsEmailing() As Boolean _
        Implements ISupportsPrinting.SupportsEmailing
        Return True
    End Function


    Private Sub CashOperationInfoListBindingSource_DataSourceChanged(ByVal sender As Object, _
        ByVal e As System.EventArgs) Handles CashOperationInfoListBindingSource.DataSourceChanged

        If _FormManager Is Nothing OrElse _FormManager.DataSource Is Nothing Then Exit Sub

        BalanceStartAccTextBox.DecimalValue = _FormManager.DataSource.BalanceStart
        BalanceBookEntriesStartAccTextBox.DecimalValue = _FormManager.DataSource.BalanceBookEntryStart
        BalanceLTLStartAccTextBox.DecimalValue = _FormManager.DataSource.BalanceLTLStart
        TurnoverDebitAccTextBox.DecimalValue = _FormManager.DataSource.TurnoverDebit
        TurnoverCreditAccTextBox.DecimalValue = _FormManager.DataSource.TurnoverCredit
        TurnoverBookEntriesDebitAccTextBox.DecimalValue = _FormManager.DataSource.TurnoverBookEntryDebit
        TurnoverBookEntriesCreditAccTextBox.DecimalValue = _FormManager.DataSource.TurnoverBookEntryCredit
        TurnoverLTLDebitAccTextBox.DecimalValue = _FormManager.DataSource.TurnoverLTLDebit
        TurnoverLTLCreditAccTextBox.DecimalValue = _FormManager.DataSource.TurnoverLTLCredit
        TurnoverInListLTLDebitAccTextBox.DecimalValue = _FormManager.DataSource.TurnOverInListLTLDebit
        TurnoverInListLTLCreditAccTextBox.DecimalValue = _FormManager.DataSource.TurnoverInListLTLCredit
        BalanceEndAccTextBox.DecimalValue = _FormManager.DataSource.BalanceEnd
        BalanceBookEntriesEndAccTextBox.DecimalValue = _FormManager.DataSource.BalanceBookEntryEnd
        BalanceLTLEndAccTextBox.DecimalValue = _FormManager.DataSource.BalanceLTLEnd

        CashOperationInfoListDataListView.Select()

    End Sub

End Class