Imports AccControlsWinForms
Imports ApskaitaObjects.HelperLists
Imports AccDataBindingsWinForms.CachedInfoLists

Friend Class F_TemplateJournalEntryInfoList

    Private ReadOnly _RequiredCachedLists As Type() = New Type() _
        {GetType(TemplateJournalEntryInfoList)}

    Private _FormManager As CslaActionExtenderReportForm(Of TemplateJournalEntryInfoList)
    Private _ListViewManager As DataListViewEditControlManager(Of TemplateJournalEntryInfo)
    Private _QueryManager As CslaActionExtenderQueryObject


    Private Sub F_Templates_Load(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load

        If Not SetDataSources() Then Exit Sub

        Try

            _FormManager = New CslaActionExtenderReportForm(Of TemplateJournalEntryInfoList) _
                (Me, TemplateJournalEntryListBindingSource, TemplateJournalEntryInfoList.GetList(), _
                 _RequiredCachedLists, RefreshButton, ProgressFiller1, "GetList", _
                 AddressOf GetReportParams)

            _FormManager.ManageDataListViewStates(TemplateJournalEntryListDataListView)

        Catch ex As Exception
            ShowError(ex, Nothing)
            DisableAllControls(Me)
        End Try

    End Sub

    Private Function SetDataSources() As Boolean

        If Not PrepareCache(Me, _RequiredCachedLists) Then Return False

        Try

            _ListViewManager = New DataListViewEditControlManager(Of TemplateJournalEntryInfo) _
                (TemplateJournalEntryListDataListView, ContextMenuStrip1, _
                 Nothing, Nothing, Nothing, Nothing)

            _ListViewManager.AddCancelButton = True
            _ListViewManager.AddButtonHandler("Keisti", "Keisti bendrojo žurnalo šabloną.", _
                AddressOf ChangeItem)
            _ListViewManager.AddButtonHandler("Ištrinti", "Pašalinti bendrojo žurnalo šabloną iš duomenų bazės.", _
                AddressOf DeleteItem)

            _ListViewManager.AddMenuItemHandler(ChangeItem_MenuItem, AddressOf ChangeItem)
            _ListViewManager.AddMenuItemHandler(DeleteItem_MenuItem, AddressOf DeleteItem)
            _ListViewManager.AddMenuItemHandler(NewItem_MenuItem, AddressOf NewItem)

            _QueryManager = New CslaActionExtenderQueryObject(Me, ProgressFiller2)

        Catch ex As Exception
            ShowError(ex, Nothing)
            DisableAllControls(Me)
            Return False
        End Try

        Return True

    End Function


    Private Function GetReportParams() As Object()
        Return Nothing
    End Function

    Private Sub ChangeItem(ByVal item As HelperLists.TemplateJournalEntryInfo)
        If item Is Nothing Then Exit Sub
        ' General.TemplateJournalEntry.GetTemplateJournalEntry(item.ID)
        _QueryManager.InvokeQuery(Of General.TemplateJournalEntry)(Nothing, _
            "GetTemplateJournalEntry", True, AddressOf OpenObjectEditForm, item.ID)
    End Sub

    Private Sub DeleteItem(ByVal item As HelperLists.TemplateJournalEntryInfo)

        If item Is Nothing Then Exit Sub

        If CheckIfObjectEditFormOpen(Of General.TemplateJournalEntry)(item.ID, True, True) Then Exit Sub

        If Not YesOrNo("Ar tikrai norite pašalinti bendrojo žurnalo šabloną iš duomenų bazės?") Then Exit Sub

        ' General.TemplateJournalEntry.DeleteTemplateJournalEntry(item.ID)
        _QueryManager.InvokeQuery(Of General.TemplateJournalEntry)(Nothing, _
            "DeleteTemplateJournalEntry", False, AddressOf OnItemDeleted, item.ID)

    End Sub

    Private Sub OnItemDeleted(ByVal result As Object, ByVal exceptionHandled As Boolean)

        If exceptionHandled Then Exit Sub

        MsgBox("Bendojo žurnalo šablonas sėkmingai pašalintas iš įmonės duomenų bazės.", _
            MsgBoxStyle.Information, "Info")

        RefreshButton.PerformClick()

    End Sub

    Private Sub Nauja_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles NewTemplateButton.Click
        NewItem(Nothing)
    End Sub

    Private Sub NewItem(ByVal item As HelperLists.TemplateJournalEntryInfo)
        OpenNewForm(Of General.TemplateJournalEntry)()
    End Sub

End Class
