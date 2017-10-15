Imports ApskaitaObjects.ActiveReports
Imports AccControlsWinForms
Imports AccDataBindingsWinForms.CachedInfoLists
Imports AccDataBindingsWinForms.Printing
Imports ApskaitaObjects.Attributes

Friend Class F_UnsettledPersonInfoList
    Implements ISupportsPrinting

    Private ReadOnly _RequiredCachedLists As Type() = New Type() _
        {GetType(HelperLists.PersonGroupInfoList), GetType(HelperLists.AccountInfoList)}

    Private _FormManager As CslaActionExtenderReportForm(Of UnsettledPersonInfoList)
    Private _ListViewManager As DataListViewEditControlManager(Of UnsettledDocumentInfo)
    Private _QueryManager As CslaActionExtenderQueryObject


    Private Sub F_UnsettledPersonInfoList_FormClosed(ByVal sender As Object, _
        ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        BrightIdeasSoftware.TreeListView.IgnoreMissingAspects = False
    End Sub

    Private Sub F_UnsettledPersonInfoList_Load(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load

        If Not SetDataSources() Then Exit Sub

        AccountAccGridComboBox.SelectedValue = GetCurrentCompany.GetDefaultAccount( _
            General.DefaultAccountType.Buyers)

        UnsettledPersonInfoListDataTreeView.CanExpandGetter = AddressOf CanExpand
        UnsettledPersonInfoListDataTreeView.ChildrenGetter = AddressOf ChildrenGetter

    End Sub

    Private Function SetDataSources() As Boolean

        If Not PrepareCache(Me, _RequiredCachedLists) Then Return False

        Try

            _ListViewManager = New DataListViewEditControlManager(Of UnsettledDocumentInfo) _
                (UnsettledPersonInfoListDataTreeView, Nothing, Nothing, _
                Nothing, Nothing, Nothing)

            ' UnsettledPersonInfoList.GetUnsettledPersonInfoList()
            _FormManager = New CslaActionExtenderReportForm(Of UnsettledPersonInfoList) _
                (Me, UnsettledPersonInfoListBindingSource, Nothing, _RequiredCachedLists, _
                 RefreshButton, ProgressFiller1, "GetUnsettledPersonInfoList", _
                 AddressOf GetReportParams)

            _FormManager.ManageDataListViewStates(UnsettledPersonInfoListDataTreeView)

            PrepareControl(PersonGroupComboBox, New PersonGroupFieldAttribute( _
                ValueRequiredLevel.Optional))
            PrepareControl(AccountAccGridComboBox, New AccountFieldAttribute( _
                ValueRequiredLevel.Optional, False, 1, 2, 3, 4))

            _QueryManager = New CslaActionExtenderQueryObject(Me, ProgressFiller2)

            BrightIdeasSoftware.TreeListView.IgnoreMissingAspects = True

        Catch ex As Exception
            ShowError(ex)
            DisableAllControls(Me)
            Return False
        End Try

        Return True

    End Function


    Private Function CanExpand(ByVal item As Object) As Boolean
        If item Is Nothing Then Return False
        If TypeOf item Is UnsettledPersonInfo Then
            Return (DirectCast(item, UnsettledPersonInfo).Items.Count > 0)
        Else
            Return False
        End If
    End Function

    Private Function ChildrenGetter(ByVal item As Object) As UnsettledDocumentInfoList
        If item Is Nothing OrElse Not TypeOf item Is UnsettledPersonInfo Then Return Nothing
        Return DirectCast(item, UnsettledPersonInfo).Items
    End Function

    Private Function GetReportParams() As Object()
        Return New Object() {AsOfDateDateTimePicker.Value, ForBuyersRadioButton.Checked, _
            AccountAccGridComboBox.SelectedValue, MarginOfErrorAccTextBox.DecimalValue, _
            PersonGroupComboBox.SelectedItem}
    End Function


    Private Sub ForBuyersRadioButton_CheckedChanged(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles ForBuyersRadioButton.CheckedChanged
        If ForBuyersRadioButton.Checked Then
            AccountAccGridComboBox.SelectedValue = GetCurrentCompany.GetDefaultAccount( _
                General.DefaultAccountType.Buyers)
        Else
            AccountAccGridComboBox.SelectedValue = GetCurrentCompany.GetDefaultAccount( _
                General.DefaultAccountType.Suppliers)
        End If
    End Sub

    Private Sub UnsettledPersonInfoListDataTreeView_CellClick(ByVal sender As Object, _
        ByVal e As BrightIdeasSoftware.CellClickEventArgs) _
        Handles UnsettledPersonInfoListDataTreeView.CellClick

        If e.Model Is Nothing OrElse e.ClickCount <> 2 Then Exit Sub

        If TypeOf e.Model Is UnsettledPersonInfo Then

            Dim frm As New F_BookEntryInfoListParent(_FormManager.DataSource.Account, _
                _FormManager.DataSource.AsOfDate.AddMonths(-3), _FormManager.DataSource.AsOfDate, _
                DirectCast(e.Model, UnsettledPersonInfo).ID)
            frm.MdiParent = CurrentMdiParent
            frm.Show()

        ElseIf TypeOf e.Model Is UnsettledDocumentInfo Then

            OpenObjectEditForm(_QueryManager, DirectCast(e.Model, UnsettledDocumentInfo).ID, _
                DirectCast(e.Model, UnsettledDocumentInfo).DocType)

        End If

    End Sub

    Private Sub UnsettledPersonInfoListBindingSource_DataSourceChanged(ByVal sender As Object, _
        ByVal e As System.EventArgs) Handles UnsettledPersonInfoListBindingSource.DataSourceChanged
        If Not UnsettledPersonInfoListBindingSource.DataSource Is Nothing Then
            Try
                UnsettledPersonInfoListDataTreeView.SetObjects(UnsettledPersonInfoListBindingSource.DataSource)
            Catch ex As Exception
            End Try
        End If
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
            PrintObject(_FormManager.DataSource, False, 0, "NeapmoketiDokumentai", Me, _
                "")
        Catch ex As Exception
            ShowError(ex)
        End Try
    End Sub

    Public Sub OnPrintPreviewClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Implements ISupportsPrinting.OnPrintPreviewClick
        If _FormManager.DataSource Is Nothing Then Exit Sub
        Try
            PrintObject(_FormManager.DataSource, True, 0, "NeapmoketiDokumentai", Me, _
                "")
        Catch ex As Exception
            ShowError(ex)
        End Try
    End Sub

    Public Function SupportsEmailing() As Boolean _
        Implements ISupportsPrinting.SupportsEmailing
        Return True
    End Function

End Class