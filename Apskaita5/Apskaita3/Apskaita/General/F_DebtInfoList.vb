Imports ApskaitaObjects.ActiveReports
Public Class F_DebtInfoList
    Implements ISupportsPrinting

    Private Obj As DebtInfoList
    Private Loading As Boolean = True

    Private Sub F_DebtInfoList_Activated(ByVal sender As Object, _
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

    Private Sub F_DebtInfoList_FormClosing(ByVal sender As Object, _
        ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        GetDataGridViewLayOut(DebtInfoListDataGridView)
        GetFormLayout(Me)
    End Sub

    Private Sub F_DebtInfoList_Load(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load

        If Not SetDataSources() Then Exit Sub

        DateFromDateTimePicker.Value = Today.Subtract(New TimeSpan(30, 0, 0, 0))
        AccountAccGridComboBox.SelectedValue = GetCurrentCompany.GetDefaultAccount( _
            General.DefaultAccountType.Buyers)

        AddDGVColumnSelector(DebtInfoListDataGridView)

        SetDataGridViewLayOut(DebtInfoListDataGridView)
        SetFormLayout(Me)

    End Sub


    Private Sub RefreshButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles RefreshButton.Click

        Using bm As New BindingsManager(DebtInfoListBindingSource, _
            Nothing, Nothing, False, True)

            Try
                Obj = LoadObject(Of ActiveReports.DebtInfoList)(Nothing, "GetDebtInfoList", True, _
                    DateFromDateTimePicker.Value.Date, DateToDateTimePicker.Value.Date, _
                    AccountAccGridComboBox.SelectedValue, IsBuyerRadioButton.Checked, _
                    PersonGroupComboBox.SelectedItem)
            Catch ex As Exception
                ShowError(ex)
                Exit Sub
            End Try

            Obj.ApplyFilter(ShowZeroDebtsCheckBox.Checked)

            bm.SetNewDataSource(Obj.GetSortedList)

        End Using

        DebtInfoListDataGridView.Select()

    End Sub

    Private Sub ShowZeroDebtsCheckBox_CheckedChanged(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles ShowZeroDebtsCheckBox.CheckedChanged

        If Obj Is Nothing Then Exit Sub

        Obj.ApplyFilter(ShowZeroDebtsCheckBox.Checked)

    End Sub

    Private Sub IsBuyerRadioButton_CheckedChanged(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles IsBuyerRadioButton.CheckedChanged
        If IsBuyerRadioButton.Checked Then
            AccountAccGridComboBox.SelectedValue = GetCurrentCompany.GetDefaultAccount( _
                General.DefaultAccountType.Buyers)
        Else
            AccountAccGridComboBox.SelectedValue = GetCurrentCompany.GetDefaultAccount( _
                General.DefaultAccountType.Suppliers)
        End If
    End Sub

    Private Sub DebtInfoListDataGridView_CellDoubleClick(ByVal sender As System.Object, _
        ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) _
        Handles DebtInfoListDataGridView.CellDoubleClick

        If e.RowIndex < 0 OrElse Obj Is Nothing Then Exit Sub

        Dim tmp As DebtInfo = Nothing
        Try
            tmp = CType(DebtInfoListDataGridView.Rows(e.RowIndex).DataBoundItem, DebtInfo)
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
            Obj.Account, Obj.DateFrom, Obj.DateTo, tmp.PersonID)

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