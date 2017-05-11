Imports ApskaitaObjects.ActiveReports

Public Class F_DebtStatementPersonDialog

    Private _ListViewManager As DataListViewEditControlManager(Of DebtStatementPerson)

    Private _SelectedEntries As List(Of DebtStatementPerson) = Nothing

    Private ReadOnly _DataSource As DebtStatementItemList = Nothing
    Private ReadOnly _RequireEmail As Boolean = False


    Public ReadOnly Property SelectedEntries() As List(Of DebtStatementPerson)
        Get
            Return _SelectedEntries
        End Get
    End Property


    Public Sub New(ByVal dataSource As DebtStatementItemList, ByVal requireEmail As Boolean)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        _DataSource = dataSource
        _RequireEmail = requireEmail

    End Sub


    Private Sub F_DebtStatementPersonDialog_FormClosing(ByVal sender As Object, _
        ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        MyCustomSettings.GetListViewLayOut(ReportDataListView)
    End Sub

    Private Sub F_AdvanceReportInfoList_Load(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load

        _ListViewManager = New DataListViewEditControlManager(Of DebtStatementPerson) _
            (ReportDataListView, Nothing, Nothing, Nothing, Nothing, Nothing)

        _ListViewManager.AddCancelButton = False
        _ListViewManager.AddButtonHandler("Pasirinkti", "Pasirinkti", AddressOf SelectItem)

        MyCustomSettings.SetListViewLayOut(ReportDataListView)

        If Me.WindowState = FormWindowState.Maximized Then Me.WindowState = FormWindowState.Normal

        DebtStatementPersonBindingSource.DataSource = _DataSource.GetPersonList()

    End Sub

    
    Private Sub SelectItem(ByVal item As DebtStatementPerson)

        If item Is Nothing Then Exit Sub

        If _RequireEmail AndAlso String.IsNullOrEmpty(item.PersonEmail.Trim) Then
            MsgBox("Klaida. Pasirinktam kontrahentui nėra nustatytas epašto adresas.", MsgBoxStyle.Exclamation, "Klaida.")
            Exit Sub
        End If

        _SelectedEntries = New List(Of DebtStatementPerson)
        _SelectedEntries.Add(item)

        Me.DialogResult = DialogResult.OK
        Me.Close()

    End Sub

    Private Sub nOkButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles nOkButton.Click

        If Me.ReportDataListView.CheckedObjects Is Nothing OrElse _
            Me.ReportDataListView.CheckedObjects.Count < 1 Then
            MsgBox("Klaida. Nepasirinktas nė vienas kontrahentas.", MsgBoxStyle.Exclamation, "Klaida.")
            Exit Sub
        End If

        _SelectedEntries = New List(Of DebtStatementPerson)
        Dim personsWithoutEmail As New List(Of String)
        For Each selectedItem As Object In Me.ReportDataListView.CheckedObjects
            Dim item As DebtStatementPerson = DirectCast(selectedItem, DebtStatementPerson)
            _SelectedEntries.Add(item)
            If String.IsNullOrEmpty(item.PersonEmail.Trim) Then personsWithoutEmail.Add(item.PersonName)
        Next

        If _RequireEmail AndAlso personsWithoutEmail.Count > 0 Then
            MsgBox("Klaida. Ne visiems pasirinktiems kontrahentams yra nustatyti epašto adresai: " & _
                String.Join(", ", personsWithoutEmail.ToArray()), MsgBoxStyle.Exclamation, "Klaida.")
            Exit Sub
        End If

        Me.DialogResult = DialogResult.OK
        Me.Close()

    End Sub

    Private Sub nCancelButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles nCancelButton.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

End Class