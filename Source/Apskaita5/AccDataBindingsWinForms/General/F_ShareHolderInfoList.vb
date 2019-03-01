Imports ApskaitaObjects.ActiveReports
Imports AccControlsWinForms
Imports AccDataBindingsWinForms.Printing

Public Class F_ShareHolderInfoList
    Implements ISupportsPrinting

    Private ReadOnly _RequiredCachedLists As Type() = New Type() {}

    Private _FormManager As CslaActionExtenderReportForm(Of ShareHolderInfoList)
    Private _ListViewManager As DataListViewEditControlManager(Of ShareHolderInfo)


    Private Sub F_ShareHolderInfoList_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not SetDataSources() Then Exit Sub
    End Sub

    Private Function SetDataSources() As Boolean

        Try

            _ListViewManager = New DataListViewEditControlManager(Of ShareHolderInfo) _
                (ItemsDataListView, Nothing, Nothing, Nothing, Nothing, Nothing)

            ' ShareHolderInfoList.GetShareHolderInfoList(AsOfDateAccDatePicker.Value)
            _FormManager = New CslaActionExtenderReportForm(Of ShareHolderInfoList) _
                (Me, Me.ShareHolderInfoListBindingSource, Nothing, _RequiredCachedLists,
                 RefreshButton, ProgressFiller1, "GetShareHolderInfoList", AddressOf GetReportParams)

            _FormManager.ManageDataListViewStates(ItemsDataListView)

        Catch ex As Exception
            ShowError(ex, Nothing)
            DisableAllControls(Me)
            Return False
        End Try

        Return True

    End Function


    Private Function GetReportParams() As Object()
        Return New Object() {AsOfDateAccDatePicker.Value.Date}
    End Function


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
            PrintObject(_FormManager.DataSource, False, 0, "AkcininkuSarasas", Me,
                _ListViewManager.GetCurrentFilterDescription(), _ListViewManager.GetDisplayOrderIndexes())
        Catch ex As Exception
            ShowError(ex, _FormManager.DataSource)
        End Try
    End Sub

    Public Sub OnPrintPreviewClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Implements ISupportsPrinting.OnPrintPreviewClick
        If _FormManager.DataSource Is Nothing Then Exit Sub
        Try
            PrintObject(_FormManager.DataSource, True, 0, "AkcininkuSarasas", Me,
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