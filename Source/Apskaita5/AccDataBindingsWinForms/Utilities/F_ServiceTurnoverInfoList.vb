Imports ApskaitaObjects.ActiveReports
Imports AccControlsWinForms
Imports AccDataBindingsWinForms.Printing
Imports AccDataBindingsWinForms.CachedInfoLists
Imports ApskaitaObjects.Attributes

Friend Class F_ServiceTurnoverInfoList
    Implements ISupportsPrinting

    Private _FormManager As CslaActionExtenderReportForm(Of ServiceTurnoverInfoList)
    Private _ListViewManager As DataListViewEditControlManager(Of ServiceTurnoverInfo)


    Private Sub F_ServiceTurnoverInfoList_Load(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            _ListViewManager = New DataListViewEditControlManager(Of ServiceTurnoverInfo) _
                (ServiceTurnoverInfoListDataListView, Nothing, Nothing, _
                 Nothing, Nothing, Nothing)

            ' ServiceTurnoverInfoList.GetServiceTurnoverInfoList(dateFrom, dateTo, showWithoutTurnover, tradedType)
            _FormManager = New CslaActionExtenderReportForm(Of ServiceTurnoverInfoList) _
                (Me, ServiceTurnoverInfoListBindingSource, Nothing, Nothing, RefreshButton, _
                 ProgressFiller1, "GetServiceTurnoverInfoList", AddressOf GetReportParams)

            _FormManager.ManageDataListViewStates(ServiceTurnoverInfoListDataListView)

            PrepareControl(TradedTypeComboBox, New LocalizedEnumFieldAttribute( _
                GetType(Documents.TradedItemType), False, ""))

        Catch ex As Exception
            ShowError(ex, Nothing)
            DisableAllControls(Me)
        End Try

        DateFromAccDatePicker.Value = Today.Subtract(New TimeSpan(30, 0, 0, 0))

    End Sub


    Private Function GetReportParams() As Object()

        Dim tradedType As Documents.TradedItemType = Documents.TradedItemType.All
        Try
            tradedType = Utilities.ConvertLocalizedName(Of Documents.TradedItemType) _
                (TradedTypeComboBox.SelectedValue.ToString())
        Catch ex As Exception
        End Try

        Return New Object() {DateFromAccDatePicker.Value.Date, DateToAccDatePicker.Value.Date, _
            ServicesWithoutTurnoverCheckBox.Checked, tradedType}

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
            PrintObject(_FormManager.DataSource, False, 0, "PaslauguApyvarta", Me, _
                _ListViewManager.GetCurrentFilterDescription(), _
                _ListViewManager.GetDisplayOrderIndexes())
        Catch ex As Exception
            ShowError(ex, _FormManager.DataSource)
        End Try
    End Sub

    Public Sub OnPrintPreviewClick(ByVal sender As Object, ByVal e As System.EventArgs) _
        Implements ISupportsPrinting.OnPrintPreviewClick
        If _FormManager.DataSource Is Nothing Then Exit Sub
        Try
            PrintObject(_FormManager.DataSource, True, 0, "PaslauguApyvarta", Me, _
                _ListViewManager.GetCurrentFilterDescription(), _
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
