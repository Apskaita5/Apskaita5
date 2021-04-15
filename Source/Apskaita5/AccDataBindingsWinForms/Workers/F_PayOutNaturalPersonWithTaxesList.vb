Imports ApskaitaObjects.Workers
Imports AccControlsWinForms
Imports AccDataBindingsWinForms.CachedInfoLists
Imports ApskaitaObjects.HelperLists
Imports ApskaitaObjects.Attributes
Imports AccDataBindingsWinForms.Printing

Public Class F_PayOutNaturalPersonWithTaxesList
    Implements ISupportsPrinting

    Private ReadOnly _RequiredCachedLists As Type() = New Type() _
        {GetType(PersonInfoList), GetType(CodeInfoList), GetType(TaxRateInfoList)}

    Private WithEvents _FormManager As CslaActionExtenderEditForm(Of PayOutNaturalPersonWithTaxesList)
    Private _ListViewManager As DataListViewEditControlManager(Of PayOutNaturalPersonWithTaxes)
    Private _QueryManager As CslaActionExtenderQueryObject
    Private _ForItem As IndirectRelationInfo = Nothing

    Public Sub New()
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Public Sub New(forItem As IndirectRelationInfo)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _ForItem = forItem
    End Sub


    Private Sub F_PayOutNaturalPersonWithTaxesList_Load(ByVal sender As System.Object,
        ByVal e As System.EventArgs) Handles MyBase.Load

        If Not SetDataSources() Then Exit Sub

        Try

            _FormManager = New CslaActionExtenderEditForm(Of PayOutNaturalPersonWithTaxesList) _
                (Me, Me.PayOutNaturalPersonWithoutTaxesListBindingSource,
                 PayOutNaturalPersonWithTaxesList.NewPayOutNaturalPersonWithTaxesList(),
                _RequiredCachedLists, IOkButton, IApplyButton, ICancelButton,
                Nothing, ProgressFiller1)

            _FormManager.ManageDataListViewStates(Me.ItemsDataListView)

        Catch ex As Exception
            ShowError(ex, Nothing)
            DisableAllControls(Me)
            Exit Sub
        End Try

        If Not _ForItem Is Nothing Then
            DateFromAccDatePicker.Value = _ForItem.Date
            DateToAccDatePicker.Value = _ForItem.Date
            RefreshButton.PerformClick()
        End If

    End Sub

    Private Function SetDataSources() As Boolean

        If Not PrepareCache(Me, _RequiredCachedLists) Then Return False

        Try

            _ListViewManager = New DataListViewEditControlManager(Of PayOutNaturalPersonWithTaxes) _
                (Me.ItemsDataListView, ContextMenuStrip1, AddressOf OnItemsDelete, Nothing,
                 Nothing, Nothing)

            _ListViewManager.AddCancelButton = True
            _ListViewManager.AddButtonHandler("BŽ Operacija", "Žiūrėti operacijos duomenis duomenis.",
                AddressOf JournalEntryItem)
            _ListViewManager.AddButtonHandler("Nauja Operacija", "Sukurti mokesčių paskaičiavimo operaciją.",
                AddressOf NewJournalEntryItem)

            _ListViewManager.AddMenuItemHandler(Me.JournalEntry_MenuItem, AddressOf JournalEntryItem)
            _ListViewManager.AddMenuItemHandler(Me.Receiver_MenuItem, AddressOf NewJournalEntryItem)

            _QueryManager = New CslaActionExtenderQueryObject(Me, ProgressFiller2)

            PrepareControl(PersonFilterAccListComboBox, New PersonFieldAttribute(
                ValueRequiredLevel.Optional, False, True, True))

        Catch ex As Exception
            ShowError(ex, Nothing)
            DisableAllControls(Me)
            Return False
        End Try

        DateFromAccDatePicker.Value = Today.AddYears(-1)

        Return True

    End Function


    Private Sub OnItemsDelete(ByVal items As PayOutNaturalPersonWithTaxes())
        If items Is Nothing OrElse items.Length < 1 OrElse _FormManager.DataSource Is Nothing Then Exit Sub
        For Each item As PayOutNaturalPersonWithTaxes In items
            _FormManager.DataSource.Remove(item)
        Next
    End Sub

    Private Sub JournalEntryItem(ByVal item As PayOutNaturalPersonWithTaxes)
        If item Is Nothing Then Exit Sub
        OpenJournalEntryEditForm(_QueryManager, item.JournalEntryID)
    End Sub

    Private Sub NewJournalEntryItem(ByVal item As PayOutNaturalPersonWithTaxes)
        If item Is Nothing Then Exit Sub
        OpenObjectEditForm(item.GetNewJournalEntry())
    End Sub

    Private Sub RefreshButton_Click(ByVal sender As System.Object,
        ByVal e As System.EventArgs) Handles RefreshButton.Click

        Dim selectedPerson As PersonInfo = Nothing
        Try
            selectedPerson = DirectCast(PersonFilterAccListComboBox.SelectedValue, PersonInfo)
        Catch ex As Exception
        End Try

        'PayOutNaturalPersonWithTaxesList.GetPayOutNaturalPersonWithTaxesList(
        '    DateFromAccDatePicker.Value, DateToAccDatePicker.Value, selectedPerson)
        _QueryManager.InvokeQuery(Of PayOutNaturalPersonWithTaxesList)(Nothing,
            "GetPayOutNaturalPersonWithTaxesList", True, AddressOf OnNewListFetched,
            DateFromAccDatePicker.Value, DateToAccDatePicker.Value, selectedPerson)

    End Sub

    Private Sub OnNewListFetched(ByVal result As Object, ByVal exceptionHandled As Boolean)

        If result Is Nothing Then Exit Sub

        _FormManager.AddNewDataSource(DirectCast(result, PayOutNaturalPersonWithTaxesList))

    End Sub

    Private Sub AddNewItemsButton_Click(ByVal sender As System.Object,
        ByVal e As System.EventArgs) Handles AddNewItemsButton.Click

        Dim selectedEntries As List(Of ActiveReports.JournalEntryInfo)

        Using dlg As New F_JournalEntryInfoList(_FormManager.DataSource.DateFrom,
            _FormManager.DataSource.DateTo, False)
            If dlg.ShowDialog() <> DialogResult.OK OrElse dlg.SelectedEntries Is Nothing _
                OrElse dlg.SelectedEntries.Count < 1 Then Exit Sub
            selectedEntries = dlg.SelectedEntries
        End Using

        'PayOutNaturalPersonWithTaxesList.CreatePayOutNaturalPersonWithTaxesList(
        '    selectedEntries.ToArray())
        _QueryManager.InvokeQuery(Of PayOutNaturalPersonWithTaxesList)(Nothing,
            "CreatePayOutNaturalPersonWithTaxesList", True, AddressOf OnNewRangeFetched,
            DirectCast(selectedEntries.ToArray(), Object))

    End Sub

    Private Sub OnNewRangeFetched(ByVal result As Object, ByVal exceptionHandled As Boolean)

        If result Is Nothing Then Exit Sub

        Try
            _FormManager.DataSource.AddNewRange(DirectCast(result, PayOutNaturalPersonWithTaxesList))
        Catch ex As Exception
            ShowError(ex, New Object() {_FormManager.DataSource, result})
        End Try

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
            PrintObject(_FormManager.DataSource, False, 0, "A_Ismokos", Me,
                _ListViewManager.GetCurrentFilterDescription(),
                _ListViewManager.GetDisplayOrderIndexes())
        Catch ex As Exception
            ShowError(ex, _FormManager.DataSource)
        End Try
    End Sub

    Public Sub OnPrintPreviewClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Implements ISupportsPrinting.OnPrintPreviewClick
        If _FormManager.DataSource Is Nothing Then Exit Sub
        Try
            PrintObject(_FormManager.DataSource, True, 0, "A_Ismokos", Me,
                _ListViewManager.GetCurrentFilterDescription(),
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
