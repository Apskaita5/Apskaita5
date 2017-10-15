Imports ApskaitaObjects.Workers
Imports AccControlsWinForms
Imports AccDataBindingsWinForms.CachedInfoLists
Imports ApskaitaObjects.HelperLists
Imports ApskaitaObjects.Attributes
Imports ApskaitaObjects.Documents

Public Class F_CustomVatOperationList

    Private ReadOnly _RequiredCachedLists As Type() = New Type() _
        {GetType(VatDeclarationSchemaInfoList)}

    Private WithEvents _FormManager As CslaActionExtenderEditForm(Of CustomVatOperationList)
    Private _ListViewManager As DataListViewEditControlManager(Of CustomVatOperation)
    Private _QueryManager As CslaActionExtenderQueryObject


    Private Sub F_CustomVatOperationList_Load(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load

        If Not SetDataSources() Then Exit Sub

        Try

            _FormManager = New CslaActionExtenderEditForm(Of CustomVatOperationList) _
                (Me, Me.CustomVatOperationListBindingSource, _
                 CustomVatOperationList.NewCustomVatOperationList(), _
                _RequiredCachedLists, IOkButton, IApplyButton, ICancelButton, _
                Nothing, ProgressFiller1)

            _FormManager.ManageDataListViewStates(Me.ItemsDataListView)

        Catch ex As Exception
            ShowError(ex)
            DisableAllControls(Me)
            Exit Sub
        End Try

        Me.DateFromDateTimePicker.Value = Today.AddMonths(-4)

    End Sub

    Private Function SetDataSources() As Boolean

        If Not PrepareCache(Me, _RequiredCachedLists) Then Return False

        Try

            _ListViewManager = New DataListViewEditControlManager(Of CustomVatOperation) _
                (Me.ItemsDataListView, ContextMenuStrip1, AddressOf OnItemsDelete, AddressOf OnAddItem, _
                 AddressOf ItemActionIsAvailable, Nothing)

            _ListViewManager.AddCancelButton = True
            _ListViewManager.AddButtonHandler("Apskaičiuoti PVM", "Apskaičiuoti PVM sumą taikant PVM schemos tarifą.", _
                AddressOf CalculateVat)
            _ListViewManager.AddButtonHandler("Rodyti Dokumentą", "Rodyti susietą dokumentą.", _
                AddressOf ShowDocument)
            _ListViewManager.AddButtonHandler("Operacija", "Rodyti susieto dokumento kontavimus.", _
                AddressOf ShowJournalEntry)

            _ListViewManager.AddMenuItemHandler(Me.CalculateVat_MenuItem, AddressOf CalculateVat)
            _ListViewManager.AddMenuItemHandler(Me.ShowDocument_MenuItem, AddressOf ShowDocument)
            _ListViewManager.AddMenuItemHandler(Me.ShowJournalEntry_MenuItem, AddressOf ShowJournalEntry)

            _QueryManager = New CslaActionExtenderQueryObject(Me, ProgressFiller2)

        Catch ex As Exception
            ShowError(ex)
            DisableAllControls(Me)
            Return False
        End Try

        DateFromDateTimePicker.Value = Today.AddYears(-1)

        Return True

    End Function


    Private Sub OnItemsDelete(ByVal items As CustomVatOperation())
        If items Is Nothing OrElse items.Length < 1 OrElse _FormManager.DataSource Is Nothing Then Exit Sub
        For Each item As CustomVatOperation In items
            _FormManager.DataSource.Remove(item)
        Next
    End Sub

    Private Sub OnAddItem()
        _FormManager.DataSource.AddNew()
    End Sub

    Private Function ItemActionIsAvailable(ByVal item As CustomVatOperation, _
        ByVal action As String) As Boolean

        If item Is Nothing OrElse action Is Nothing Then Return False

        If action.Trim.ToUpperInvariant() = "ShowJournalEntry".ToUpperInvariant() _
            OrElse action.Trim.ToUpperInvariant() = "ShowDocument".ToUpperInvariant() Then

            Return (item.JournalEntryId > 0)

        End If

        Return True

    End Function

    Private Sub CalculateVat(ByVal item As CustomVatOperation)
        If item Is Nothing Then Exit Sub
        Try
            item.CalculateVat()
        Catch ex As Exception
            ShowError(ex)
        End Try
    End Sub

    Private Sub ShowJournalEntry(ByVal item As CustomVatOperation)
        If item Is Nothing OrElse Not item.JournalEntryId > 0 Then Exit Sub
        OpenJournalEntryEditForm(_QueryManager, item.JournalEntryId)
    End Sub

    Private Sub ShowDocument(ByVal item As CustomVatOperation)
        If item Is Nothing OrElse Not item.JournalEntryId > 0 Then Exit Sub
        OpenObjectEditForm(_QueryManager, item.JournalEntryId, item.JournalEntryType)
    End Sub


    Private Sub RefreshButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles RefreshButton.Click

        'CustomVatOperationList.GetCustomVatOperationList(DateFromDateTimePicker.Value, _
        '    DateToDateTimePicker.Value, ByJournalEntryCheckBox.Checked)
        _QueryManager.InvokeQuery(Of CustomVatOperationList)(Nothing, _
            "GetCustomVatOperationList", True, AddressOf OnNewListFetched, _
            DateFromDateTimePicker.Value, DateToDateTimePicker.Value, ByJournalEntryCheckBox.Checked)

    End Sub

    Private Sub OnNewListFetched(ByVal result As Object, ByVal exceptionHandled As Boolean)

        If result Is Nothing Then Exit Sub

        _FormManager.AddNewDataSource(DirectCast(result, CustomVatOperationList))

    End Sub

    Private Sub AddByJournalEntriesButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles AddByJournalEntriesButton.Click

        Dim selectedEntries As New List(Of Integer)

        Using dlg As New F_JournalEntryInfoList(_FormManager.DataSource.DateFrom, _
            _FormManager.DataSource.DateTo, False)
            If dlg.ShowDialog() <> DialogResult.OK OrElse dlg.SelectedEntries Is Nothing _
                OrElse dlg.SelectedEntries.Count < 1 Then Exit Sub
            For Each item As ActiveReports.JournalEntryInfo In dlg.SelectedEntries
                selectedEntries.Add(item.Id)
            Next
        End Using

        ' CustomVatOperationList.NewCustomVatOperationList(selectedEntries.ToArray())
        _QueryManager.InvokeQuery(Of CustomVatOperationList)(Nothing, _
            "NewCustomVatOperationList", True, AddressOf OnNewRangeFetched, _
            selectedEntries.ToArray())

    End Sub

    Private Sub OnNewRangeFetched(ByVal result As Object, ByVal exceptionHandled As Boolean)

        If result Is Nothing Then Exit Sub

        Try
            _FormManager.DataSource.AddNewRange(DirectCast(result, CustomVatOperationList))
        Catch ex As Exception
            ShowError(ex)
        End Try

    End Sub

End Class