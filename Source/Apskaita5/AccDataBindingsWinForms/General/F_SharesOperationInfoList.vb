Imports ApskaitaObjects.ActiveReports
Imports AccControlsWinForms
Imports AccDataBindingsWinForms.CachedInfoLists
Imports AccDataBindingsWinForms.Printing
Imports ApskaitaObjects.Attributes

Public Class F_SharesOperationInfoList
    Implements ISupportsPrinting

    Private ReadOnly _RequiredCachedLists As Type() = New Type() _
        {GetType(HelperLists.PersonInfoList), GetType(HelperLists.SharesClassInfoList)}

    Private _FormManager As CslaActionExtenderReportForm(Of SharesOperationInfoList)
    Private _ListViewManager As DataListViewEditControlManager(Of SharesOperationInfo)
    Private _QueryManager As CslaActionExtenderQueryObject


    Private Sub F_SharesOperationInfoList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not SetDataSources() Then Exit Sub
    End Sub

    Private Function SetDataSources() As Boolean

        If Not PrepareCache(Me, GetType(HelperLists.PersonGroupInfoList),
            GetType(HelperLists.AccountInfoList)) Then Return False

        Try

            _ListViewManager = New DataListViewEditControlManager(Of SharesOperationInfo) _
                (ItemsDataListView, ContextMenuStrip1, Nothing, Nothing, Nothing, Nothing)

            _ListViewManager.AddCancelButton = True
            _ListViewManager.AddButtonHandler("Keisti", "Keisti operacijos duomenis.",
                AddressOf ChangeItem)
            _ListViewManager.AddButtonHandler("Ištrinti", "Pašalinti operacijos duomenis iš duomenų bazės.",
                AddressOf DeleteItem)

            _ListViewManager.AddMenuItemHandler(ChangeItem_MenuItem, AddressOf ChangeItem)
            _ListViewManager.AddMenuItemHandler(DeleteItem_MenuItem, AddressOf DeleteItem)

            _QueryManager = New CslaActionExtenderQueryObject(Me, ProgressFiller2)

            'SharesOperationInfoList.GetSharesOperationInfoList(ShareHolderAccListComboBox.SelectedValue,
            '     CompanySharesCheckBox.Checked, ClassAccListComboBox.SelectedValue,
            '     DateBeginAccDatePicker.Value.Date, DateEndAccDatePicker.Value.Date)
            _FormManager = New CslaActionExtenderReportForm(Of SharesOperationInfoList) _
                (Me, SharesOperationInfoListBindingSource, Nothing, _RequiredCachedLists,
                 RefreshButton, ProgressFiller1, "GetSharesOperationInfoList", AddressOf GetReportParams)

            _FormManager.ManageDataListViewStates(ItemsDataListView)

            PrepareControl(ShareHolderAccListComboBox, New PersonFieldAttribute(
                ValueRequiredLevel.Optional))
            PrepareControl(ClassAccListComboBox, New SharesClassFieldAttribute(
                ValueRequiredLevel.Optional))
            DateBeginAccDatePicker.Value = Today.AddYears(-5)

        Catch ex As Exception
            ShowError(ex, Nothing)
            DisableAllControls(Me)
            Return False
        End Try

        Return True

    End Function


    Private Function GetReportParams() As Object()
        Return New Object() {ShareHolderAccListComboBox.SelectedValue,
                 CompanySharesCheckBox.Checked, ClassAccListComboBox.SelectedValue,
                 DateBeginAccDatePicker.Value.Date, DateEndAccDatePicker.Value.Date}
    End Function

    Private Sub ChangeItem(ByVal item As SharesOperationInfo)
        If item Is Nothing Then Exit Sub

        ' General.SharesOperation.GetSharesOperation(item.ID)
        _QueryManager.InvokeQuery(Of General.SharesOperation)(Nothing, "GetSharesOperation", True,
            AddressOf OpenObjectEditForm, item.ID)

    End Sub

    Private Sub DeleteItem(ByVal item As SharesOperationInfo)

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
            PrintObject(_FormManager.DataSource, False, 0, "OperacijosSuAkcijomis", Me,
                _ListViewManager.GetCurrentFilterDescription(), _ListViewManager.GetDisplayOrderIndexes())
        Catch ex As Exception
            ShowError(ex, _FormManager.DataSource)
        End Try
    End Sub

    Public Sub OnPrintPreviewClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Implements ISupportsPrinting.OnPrintPreviewClick
        If _FormManager.DataSource Is Nothing Then Exit Sub
        Try
            PrintObject(_FormManager.DataSource, True, 0, "OperacijosSuAkcijomis", Me,
                _ListViewManager.GetCurrentFilterDescription(), _ListViewManager.GetDisplayOrderIndexes())
        Catch ex As Exception
            ShowError(ex, _FormManager.DataSource)
        End Try
    End Sub

    Public Function SupportsEmailing() As Boolean _
        Implements ISupportsPrinting.SupportsEmailing
        Return True
    End Function

End Class
