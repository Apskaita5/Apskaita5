Imports ApskaitaObjects.ActiveReports
Imports AccControlsWinForms
Imports AccDataBindingsWinForms.CachedInfoLists
Imports AccDataBindingsWinForms.Printing
Imports ApskaitaObjects.Attributes

Public Class F_SharesAccountEntryList
    Implements ISupportsPrinting

    Private ReadOnly _RequiredCachedLists As Type() = New Type() _
        {GetType(HelperLists.PersonInfoList), GetType(HelperLists.SharesClassInfoList)}

    Private _FormManager As CslaActionExtenderReportForm(Of SharesAccountEntryList)
    Private _ListViewManager As DataListViewEditControlManager(Of SharesAccountEntry)
    Private _QueryManager As CslaActionExtenderQueryObject

    Private _PrintDropDown As Windows.Forms.ToolStripDropDown = Nothing
    Private _PrintPreviewDropDown As Windows.Forms.ToolStripDropDown = Nothing
    Private _EmailDropDown As Windows.Forms.ToolStripDropDown = Nothing


    Private Sub F_SharesAccountEntryList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not SetDataSources() Then Exit Sub
    End Sub

    Private Function SetDataSources() As Boolean

        If Not PrepareCache(Me, GetType(HelperLists.PersonGroupInfoList),
            GetType(HelperLists.AccountInfoList)) Then Return False

        Try

            _ListViewManager = New DataListViewEditControlManager(Of SharesAccountEntry) _
                (ItemsDataListView, ContextMenuStrip1, Nothing, Nothing, Nothing, Nothing)

            _ListViewManager.AddCancelButton = True
            _ListViewManager.AddButtonHandler("Keisti", "Keisti operacijos duomenis.",
                AddressOf ChangeItem)
            _ListViewManager.AddButtonHandler("Ištrinti", "Pašalinti operacijos duomenis iš duomenų bazės.",
                AddressOf DeleteItem)

            _ListViewManager.AddMenuItemHandler(ChangeItem_MenuItem, AddressOf ChangeItem)
            _ListViewManager.AddMenuItemHandler(DeleteItem_MenuItem, AddressOf DeleteItem)

            _QueryManager = New CslaActionExtenderQueryObject(Me, ProgressFiller2)

            'SharesAccountEntryList.GetSharesAccountEntryList(ShareHolderAccListComboBox.SelectedValue,
            '    ClassAccListComboBox.SelectedValue, AsOfDateAccDatePicker.Value)
            _FormManager = New CslaActionExtenderReportForm(Of SharesAccountEntryList) _
                (Me, SharesAccountEntryListBindingSource, Nothing, _RequiredCachedLists,
                 RefreshButton, ProgressFiller1, "GetSharesAccountEntryList", AddressOf GetReportParams)

            _FormManager.ManageDataListViewStates(ItemsDataListView)

            PrepareControl(ShareHolderAccListComboBox, New PersonFieldAttribute(
                ValueRequiredLevel.Optional))
            PrepareControl(ClassAccListComboBox, New SharesClassFieldAttribute(
                ValueRequiredLevel.Optional))

        Catch ex As Exception
            ShowError(ex, Nothing)
            DisableAllControls(Me)
            Return False
        End Try

        Return True

    End Function


    Private Function GetReportParams() As Object()
        Return New Object() {ShareHolderAccListComboBox.SelectedValue,
                ClassAccListComboBox.SelectedValue, AsOfDateAccDatePicker.Value.Date}
    End Function

    Private Sub ChangeItem(ByVal item As SharesAccountEntry)
        If item Is Nothing Then Exit Sub

        ' General.SharesOperation.GetSharesOperation(item.ID)
        _QueryManager.InvokeQuery(Of General.SharesOperation)(Nothing, "GetSharesOperation", True,
            AddressOf OpenObjectEditForm, item.ID)

    End Sub

    Private Sub DeleteItem(ByVal item As SharesAccountEntry)

        If item Is Nothing Then Exit Sub

        If CheckIfObjectEditFormOpen(Of General.SharesOperation)(item.ID, True, True) Then Exit Sub

        If Not YesOrNo("Ar tikrai norite pašalinti pasirinktos operacijos " &
            "duomenis iš duomenų bazės?") Then Exit Sub

        ' General.SharesOperation.DeleteSharesOperation(item.ID)
        _QueryManager.InvokeQuery(Of General.SharesOperation)(Nothing, "DeleteSharesOperation", False,
            AddressOf OnOperationDeleted, item.ID)

    End Sub

    Private Sub OnOperationDeleted(ByVal result As Object, ByVal exceptionHandled As Boolean)
        If exceptionHandled Then Exit Sub
        MsgBox("Operacijos duomenys sėkmingai pašalinti iš duomenų bazės.",
            MsgBoxStyle.Information, "Info")
        RefreshButton.PerformClick()
    End Sub


    Public Function GetMailDropDownItems() As System.Windows.Forms.ToolStripDropDown _
        Implements ISupportsPrinting.GetMailDropDownItems
        If _EmailDropDown Is Nothing Then
            _EmailDropDown = New ToolStripDropDown
            _EmailDropDown.Items.Add("Sąskaitos išrašas", Nothing, AddressOf OnMailClick)
        End If

        Return _EmailDropDown
    End Function

    Public Function GetPrintDropDownItems() As System.Windows.Forms.ToolStripDropDown _
        Implements ISupportsPrinting.GetPrintDropDownItems
        If _PrintDropDown Is Nothing Then
            _PrintDropDown = New ToolStripDropDown
            _PrintDropDown.Items.Add("Sąskaitos išrašas", Nothing, AddressOf OnPrintClick)

        End If

        Return _PrintDropDown
    End Function

    Public Function GetPrintPreviewDropDownItems() As System.Windows.Forms.ToolStripDropDown _
        Implements ISupportsPrinting.GetPrintPreviewDropDownItems
        If _PrintPreviewDropDown Is Nothing Then
            _PrintPreviewDropDown = New ToolStripDropDown
            _PrintPreviewDropDown.Items.Add("Sąskaitos išrašas", Nothing, AddressOf OnPrintPreviewClick)

        End If

        Return _PrintPreviewDropDown
    End Function

    Public Sub OnMailClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Implements ISupportsPrinting.OnMailClick

        If _FormManager.DataSource Is Nothing Then Exit Sub

        Using frm As New F_SendObjToEmail(_FormManager.DataSource, Convert.ToInt32(IIf(GetSenderText(sender).ToLower.Contains("išrašas"), 1, 0)))
            frm.ShowDialog()
        End Using

    End Sub

    Public Sub OnPrintClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Implements ISupportsPrinting.OnPrintClick
        If _FormManager.DataSource Is Nothing Then Exit Sub
        Try
            PrintObject(_FormManager.DataSource, False, Convert.ToInt32(IIf(GetSenderText(sender).ToLower.Contains("išrašas"), 1, 0)),
                "AkcijuSaskaita", Me, _ListViewManager.GetCurrentFilterDescription(),
                _ListViewManager.GetDisplayOrderIndexes())
        Catch ex As Exception
            ShowError(ex, _FormManager.DataSource)
        End Try
    End Sub

    Public Sub OnPrintPreviewClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Implements ISupportsPrinting.OnPrintPreviewClick
        If _FormManager.DataSource Is Nothing Then Exit Sub
        Try
            PrintObject(_FormManager.DataSource, True, Convert.ToInt32(IIf(GetSenderText(sender).ToLower.Contains("išrašas"), 1, 0)),
                "AkcijuSaskaita", Me, _ListViewManager.GetCurrentFilterDescription(),
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
