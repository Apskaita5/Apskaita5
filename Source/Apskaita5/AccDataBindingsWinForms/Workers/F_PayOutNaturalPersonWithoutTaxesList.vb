Imports ApskaitaObjects.Workers
Imports AccControlsWinForms
Imports AccDataBindingsWinForms.CachedInfoLists
Imports ApskaitaObjects.HelperLists
Imports ApskaitaObjects.Attributes
Imports ApskaitaObjects.General

Public Class F_PayOutNaturalPersonWithoutTaxesList

    Private ReadOnly _RequiredCachedLists As Type() = New Type() _
        {GetType(PersonInfoList), GetType(CodeInfoList)}

    Private WithEvents _FormManager As CslaActionExtenderEditForm(Of PayOutNaturalPersonWithoutTaxesList)
    Private _ListViewManager As DataListViewEditControlManager(Of PayOutNaturalPersonWithoutTaxes)
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


    Private Sub F_PayOutNaturalPersonWithoutTaxesList_Load(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load

        If Not SetDataSources() Then Exit Sub

        Try

            _FormManager = New CslaActionExtenderEditForm(Of PayOutNaturalPersonWithoutTaxesList) _
                (Me, Me.PayOutNaturalPersonWithoutTaxesListBindingSource,
                 PayOutNaturalPersonWithoutTaxesList.NewPayOutNaturalPersonWithoutTaxesList(),
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

            _ListViewManager = New DataListViewEditControlManager(Of PayOutNaturalPersonWithoutTaxes) _
                (Me.ItemsDataListView, ContextMenuStrip1, AddressOf OnItemsDelete, Nothing, _
                 Nothing, Nothing)

            _ListViewManager.AddCancelButton = True
            _ListViewManager.AddButtonHandler("Operacija", "Žiūrėti operacijos duomenis duomenis.", _
                AddressOf JournalEntryItem)
            _ListViewManager.AddButtonHandler("Gavėjas", "Žiūrėti gavėjo duomenis.", _
                AddressOf ReceiverItem)

            _ListViewManager.AddMenuItemHandler(Me.JournalEntry_MenuItem, AddressOf JournalEntryItem)
            _ListViewManager.AddMenuItemHandler(Me.Receiver_MenuItem, AddressOf ReceiverItem)

            _QueryManager = New CslaActionExtenderQueryObject(Me, ProgressFiller2)

            PrepareControl(PersonFilterAccListComboBox, New PersonFieldAttribute( _
                ValueRequiredLevel.Optional, False, True, True))

        Catch ex As Exception
            ShowError(ex, Nothing)
            DisableAllControls(Me)
            Return False
        End Try

        DateFromAccDatePicker.Value = Today.AddYears(-1)

        Return True

    End Function


    Private Sub OnItemsDelete(ByVal items As PayOutNaturalPersonWithoutTaxes())
        If items Is Nothing OrElse items.Length < 1 OrElse _FormManager.DataSource Is Nothing Then Exit Sub
        For Each item As PayOutNaturalPersonWithoutTaxes In items
            _FormManager.DataSource.Remove(item)
        Next
    End Sub

    Private Sub JournalEntryItem(ByVal item As PayOutNaturalPersonWithoutTaxes)
        If item Is Nothing Then Exit Sub
        OpenJournalEntryEditForm(_QueryManager, item.JournalEntryID)
    End Sub

    Private Sub ReceiverItem(ByVal item As PayOutNaturalPersonWithoutTaxes)
        If item Is Nothing Then Exit Sub
        ' Person.GetPerson(id)
        If item.PaymentReceiver <> PersonInfo.Empty Then
            _QueryManager.InvokeQuery(Of Person)(Nothing, "GetPerson", True, _
                AddressOf OpenObjectEditForm, item.PaymentReceiver.ID)
        ElseIf item.JournalEntryPersonID > 0 Then
            _QueryManager.InvokeQuery(Of Person)(Nothing, "GetPerson", True, _
                AddressOf OpenObjectEditForm, item.JournalEntryPersonID)
        End If
    End Sub

    Private Sub RefreshButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles RefreshButton.Click

        Dim selectedPerson As PersonInfo = Nothing
        Try
            selectedPerson = DirectCast(PersonFilterAccListComboBox.SelectedValue, PersonInfo)
        Catch ex As Exception
        End Try

        'PayOutNaturalPersonWithoutTaxesList.GetPayOutNaturalPersonWithoutTaxesList( _
        '    DateFromDateTimePicker.Value, DateToDateTimePicker.Value, selectedPerson)
        _QueryManager.InvokeQuery(Of PayOutNaturalPersonWithoutTaxesList)(Nothing, _
            "GetPayOutNaturalPersonWithoutTaxesList", True, AddressOf OnNewListFetched, _
            DateFromAccDatePicker.Value, DateToAccDatePicker.Value, selectedPerson)

    End Sub

    Private Sub OnNewListFetched(ByVal result As Object, ByVal exceptionHandled As Boolean)

        If result Is Nothing Then Exit Sub

        _FormManager.AddNewDataSource(DirectCast(result, PayOutNaturalPersonWithoutTaxesList))

    End Sub

    Private Sub AddNewItemsButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles AddNewItemsButton.Click

        Dim selectedEntries As New List(Of Integer)

        Using dlg As New F_JournalEntryInfoList(_FormManager.DataSource.DateFrom, _
            _FormManager.DataSource.DateTo, False)
            If dlg.ShowDialog() <> DialogResult.OK OrElse dlg.SelectedEntries Is Nothing _
                OrElse dlg.SelectedEntries.Count < 1 Then Exit Sub
            For Each item As ActiveReports.JournalEntryInfo In dlg.SelectedEntries
                selectedEntries.Add(item.Id)
            Next
        End Using

        'PayOutNaturalPersonWithoutTaxesList.CreatePayOutNaturalPersonWithoutTaxesList( _
        '    selectedEntries.ToArray())
        _QueryManager.InvokeQuery(Of PayOutNaturalPersonWithoutTaxesList)(Nothing, _
            "CreatePayOutNaturalPersonWithoutTaxesList", True, AddressOf OnNewRangeFetched, _
            selectedEntries.ToArray())

    End Sub

    Private Sub OnNewRangeFetched(ByVal result As Object, ByVal exceptionHandled As Boolean)

        If result Is Nothing Then Exit Sub

        Try
            _FormManager.DataSource.AddNewRange(DirectCast(result, PayOutNaturalPersonWithoutTaxesList))
        Catch ex As Exception
            ShowError(ex, New Object() {_FormManager.DataSource, result})
        End Try

    End Sub

    Private Sub ExportFFDataButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles ExportFFDataButton.Click

        If _FormManager.DataSource Is Nothing OrElse _FormManager.DataSource.Count < 1 Then Exit Sub

        Dim fileName As String = ""

        Using sfd As New SaveFileDialog
            sfd.Filter = "FFData failai|*.ffdata|Visi failai|*.*"
            sfd.CheckFileExists = False
            sfd.AddExtension = True
            sfd.DefaultExt = ".ffdata"
            If sfd.ShowDialog() <> Windows.Forms.DialogResult.OK Then Exit Sub
            fileName = sfd.FileName.Trim
        End Using

        If StringIsNullOrEmpty(fileName) Then Exit Sub

        Dim version As Integer = 5
        'If GetSenderText(sender).Trim.ToLower.Contains("1") Then
        '    version = 1
        'Else
        '    version = 2
        'End If

        Dim declaration As ActiveReports.IPayOutsNaturalPersonsWithoutTaxesDeclaration = _
            New ActiveReports.Declarations.DeclarationFR0471_5()
        'If version = 1 Then
        '    declaration = New InvoiceRegisterFR0672_1
        'Else
        '    declaration = New InvoiceRegisterFR0671_2
        'End If

        Try

            Dim result As String = ""

            Using busy As New StatusBusy
                result = declaration.GetFfDataString(_FormManager.DataSource)
                IO.File.WriteAllText(fileName, result, New System.Text.UTF8Encoding)
            End Using

            If YesOrNo("Failas sėkmingai išsaugotas. Atidaryti?") Then
                System.Diagnostics.Process.Start(fileName)
            End If

        Catch ex As Exception
            ShowError(ex, _FormManager.DataSource)
        End Try

    End Sub

End Class