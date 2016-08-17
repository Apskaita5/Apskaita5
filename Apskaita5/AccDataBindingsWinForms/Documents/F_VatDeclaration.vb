Imports AccControlsWinForms
Imports ApskaitaObjects.ActiveReports

Public Class F_VatDeclaration

    Private _FormManager As CslaActionExtenderReportForm(Of VatDeclaration)
    Private _ItemsListViewManager As DataListViewEditControlManager(Of VatDeclarationItem)
    Private _SubtotalsListViewManager As DataListViewEditControlManager(Of VatDeclarationSubtotal)
    Private _QueryManager As CslaActionExtenderQueryObject

    Private Sub F_VatDeclaration_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Not SetDataSources() Then Exit Sub

    End Sub

    Private Function SetDataSources() As Boolean

        YearComboBox.Items.Clear()
        For i As Integer = Today.Year - 50 To Today.Year
            YearComboBox.Items.Add(i)
        Next
        YearComboBox.SelectedItem = Today.AddMonths(-1).Year

        MonthComboBox.Items.Clear()
        For i As Integer = 1 To 12
            MonthComboBox.Items.Add(i)
        Next
        MonthComboBox.SelectedItem = Today.AddMonths(-1).Month

        Try

            _ItemsListViewManager = New DataListViewEditControlManager(Of VatDeclarationItem) _
                (ItemsDataListView, Nothing, Nothing, Nothing, Nothing)

            _SubtotalsListViewManager = New DataListViewEditControlManager(Of VatDeclarationSubtotal) _
                (SubtotalsDataListView, Nothing, Nothing, Nothing, Nothing)

            _ItemsListViewManager.AddCancelButton = False
            _ItemsListViewManager.AddButtonHandler("Keisti", _
                "atidaryti dokumentą", AddressOf EditItem)

            _FormManager = New CslaActionExtenderReportForm(Of VatDeclaration) _
                (Me, VatDeclarationBindingSource, Nothing, Nothing, _
                 RefreshButton, ProgressFiller1, "GetVatDeclaration", _
                 AddressOf GetReportParams)

            _FormManager.ManageDataListViewStates(ItemsDataListView, SubtotalsDataListView)

            _QueryManager = New CslaActionExtenderQueryObject(Me, ProgressFiller2)

        Catch ex As Exception
            ShowError(ex)
            DisableAllControls(Me)
            Return False
        End Try

        Return True

    End Function


    Private Function GetReportParams() As Object()

        Dim year As Integer = 0
        Dim month As Integer = 0
        Try
            year = YearComboBox.SelectedItem
        Catch ex As Exception
        End Try
        Try
            month = MonthComboBox.SelectedItem
        Catch ex As Exception
        End Try

        ' VatDeclaration.GetVatDeclaration(DateDateTimePicker.Value, year, month)
        Return New Object() {DateDateTimePicker.Value.Date, year, month}

    End Function

    Private Sub EditItem(ByVal item As VatDeclarationItem)
        OpenObjectEditForm(_QueryManager, item.ID, item.DocumentType)
    End Sub

    Private Sub ExportFFDataButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles ExportFFDataButton.Click

        If _FormManager.DataSource Is Nothing Then Exit Sub

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

        Dim version As Integer = 2
        'If GetSenderText(sender).Trim.ToLower.Contains("1") Then
        '    version = 1
        'Else
        '    version = 2
        'End If

        Dim declaration As IVatDeclaration = New Declarations.DeclarationFR0600_2
        'If version = 1 Then
        '    declaration = New InvoiceRegisterFR0672_1
        'Else
        '    declaration = New InvoiceRegisterFR0671_2
        'End If

        Try

            Dim warnings As String = ""

            Using busy As New StatusBusy
                _FormManager.DataSource.SaveToFfData(fileName, declaration, warnings)
            End Using

            Dim message As String
            If StringIsNullOrEmpty(warnings) Then
                message = "Failas sėkmingai išsaugotas. Atidaryti?"
            Else
                message = "DĖMESIO. Išsaugant failą pastebėtos klaidos:" _
                & vbCrLf & warnings & vbCrLf & vbCrLf & "Atidaryti išsaugotą failą?"
            End If

            If YesOrNo(message) Then
                System.Diagnostics.Process.Start(fileName)
            End If

        Catch ex As Exception
            ShowError(ex)
        End Try

    End Sub

End Class