Imports ApskaitaObjects.ActiveReports
Public Class F_UnsettledPersonInfoList
    Implements ISupportsPrinting

    Private Obj As UnsettledPersonInfoList
    Private Loading As Boolean = True


    Private Sub F_UnsettledPersonInfoList_Activated(ByVal sender As Object, _
        ByVal e As System.EventArgs) Handles Me.Activated

        If Me.WindowState = FormWindowState.Maximized AndAlso MyCustomSettings.AutoSizeForm Then _
            Me.WindowState = FormWindowState.Normal

        If Loading Then
            Loading = False
            Exit Sub
        End If

        If Not PrepareCache(Me, GetType(HelperLists.PersonGroupInfoList), _
            GetType(HelperLists.AccountInfoList)) Then Exit Sub

    End Sub

    Private Sub F_UnsettledPersonInfoList_FormClosing(ByVal sender As Object, _
        ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        GetDataGridViewLayOut(UnsettledPersonInfoListDataGridView)
        GetDataGridViewLayOut(ItemsDataGridView)
        GetFormLayout(Me)
    End Sub

    Private Sub F_UnsettledPersonInfoList_Load(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load

        If Not SetDataSources() Then Exit Sub

        AccountAccGridComboBox.SelectedValue = GetCurrentCompany.GetDefaultAccount( _
            General.DefaultAccountType.Buyers)

        AddDGVColumnSelector(UnsettledPersonInfoListDataGridView)
        AddDGVColumnSelector(ItemsDataGridView)

        SetDataGridViewLayOut(UnsettledPersonInfoListDataGridView)
        SetDataGridViewLayOut(ItemsDataGridView)
        SetFormLayout(Me)

    End Sub


    Private Sub RefreshButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles RefreshButton.Click

        Using bm As New BindingsManager(UnsettledPersonInfoListBindingSource, _
            ItemsSortedBindingSource, Nothing, False, True)

            Try
                Obj = LoadObject(Of ActiveReports.UnsettledPersonInfoList)(Nothing, _
                    "GetUnsettledPersonInfoList", True, AsOfDateDateTimePicker.Value, _
                    ForBuyersRadioButton.Checked, AccountAccGridComboBox.SelectedValue, _
                    MarginOfErrorAccTextBox.DecimalValue, PersonGroupComboBox.SelectedItem)
            Catch ex As Exception
                ShowError(ex)
                Exit Sub
            End Try

            bm.SetNewDataSource(Obj.GetSortedList)

        End Using

        UnsettledPersonInfoListDataGridView.Select()

    End Sub

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

    Private Sub UnsettledPersonInfoListDataGridView_CellDoubleClick(ByVal sender As System.Object, _
        ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) _
        Handles UnsettledPersonInfoListDataGridView.CellDoubleClick

        If e.RowIndex < 0 OrElse Obj Is Nothing Then Exit Sub

        Dim tmp As UnsettledPersonInfo = Nothing
        Try
            tmp = CType(UnsettledPersonInfoListDataGridView.Rows(e.RowIndex).DataBoundItem, UnsettledPersonInfo)
        Catch ex As Exception
            ShowError(ex)
            Exit Sub
        End Try
        If tmp Is Nothing Then
            MsgBox("Klaida. Nepavyko nustatyti pasirinkto asmens.", _
                MsgBoxStyle.Exclamation, "Klaida.")
            Exit Sub
        End If

        MDIParent1.LaunchForm(GetType(F_AccountTurnoverInfo), False, False, 0, _
            Obj.Account, Obj.AsOfDate.AddMonths(-3), Obj.AsOfDate, tmp.ID)

    End Sub

    Private Sub ItemsDataGridView_CellDoubleClick(ByVal sender As System.Object, _
        ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) _
        Handles ItemsDataGridView.CellDoubleClick

        If e.RowIndex < 0 OrElse Obj Is Nothing Then Exit Sub

        Dim tmp As UnsettledDocumentInfo = Nothing
        Try
            tmp = CType(ItemsDataGridView.Rows(e.RowIndex).DataBoundItem, UnsettledDocumentInfo)
        Catch ex As Exception
            ShowError(ex)
            Exit Sub
        End Try
        If tmp Is Nothing Then
            MsgBox("Klaida. Nepavyko nustatyti pasirinkto dokumento.", _
                MsgBoxStyle.Exclamation, "Klaida.")
            Exit Sub
        End If

        OpenDocumentInEditForm(tmp.ID, tmp.DocType)

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

        If Obj Is Nothing Then Exit Sub

        Using frm As New F_SendObjToEmail(Obj, 0)
            frm.ShowDialog()
        End Using

    End Sub

    Public Sub OnPrintClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Implements ISupportsPrinting.OnPrintClick
        If Obj Is Nothing Then Exit Sub
        Try
            PrintObject(Obj, False, 0)
        Catch ex As Exception
            ShowError(ex)
        End Try
    End Sub

    Public Sub OnPrintPreviewClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Implements ISupportsPrinting.OnPrintPreviewClick
        If Obj Is Nothing Then Exit Sub
        Try
            PrintObject(Obj, True, 0)
        Catch ex As Exception
            ShowError(ex)
        End Try
    End Sub

    Public Function SupportsEmailing() As Boolean _
        Implements ISupportsPrinting.SupportsEmailing
        Return True
    End Function


    Private Function SetDataSources() As Boolean

        If Not PrepareCache(Me, GetType(HelperLists.PersonGroupInfoList), _
            GetType(HelperLists.AccountInfoList)) Then Return False

        Try

            LoadPersonGroupInfoListToCombo(PersonGroupComboBox)
            LoadAccountInfoListToGridCombo(AccountAccGridComboBox, True, 1, 2, 3, 4)

        Catch ex As Exception
            ShowError(ex)
            DisableAllControls(Me)
            Return False
        End Try

        Return True

    End Function

End Class