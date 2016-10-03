Imports System.Windows.Forms
Imports BrightIdeasSoftware
Imports System.ComponentModel

Public Class F_TypicalAccountInfoDialog

    Private _SelectedItems As TypicalAccountInfo() = Nothing
    Private _SearchString As String = ""


    Public ReadOnly Property SelectedItems() As TypicalAccountInfo()
        Get
            Return _SelectedItems
        End Get
    End Property


    Private Sub F_TypicalAccountInfoDialog_FormClosing(ByVal sender As Object, _
        ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        MyCustomSettings.GetFormLayout(Me)
        MyCustomSettings.GetListViewLayOut(Me.ReportDataListView)
    End Sub

    Private Sub F_TypicalAccountInfoDialog_Load(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load

        MyCustomSettings.SetFormLayout(Me)
        MyCustomSettings.SetListViewLayOut(Me.ReportDataListView)

        Me.TypicalAccountInfoBindingSource.DataSource = TypicalAccountInfo.GetTypicalAccountList()

        ReportDataListView.Select()

    End Sub


    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click

        If Me.ReportDataListView.CheckedObjects Is Nothing OrElse _
            Me.ReportDataListView.CheckedObjects.Count < 1 Then
            MsgBox("Klaida. Nepasirinkta nei viena sąskaita.", _
                MsgBoxStyle.Exclamation, "Klaida")
            Exit Sub
        End If

        Dim items As New List(Of TypicalAccountInfo)

        For Each item As TypicalAccountInfo In Me.ReportDataListView.CheckedObjects
            items.Add(item)
        Next

        _SelectedItems = items.ToArray()

        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()

    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub


    Private Sub CheckAllCheckBox_CheckedChanged(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles CheckAllCheckBox.CheckedChanged

        If ReportDataListView.GetItemCount() < 1 Then Exit Sub

        ReportDataListView.UncheckAll()

        If Not CheckAllCheckBox.Checked Then Exit Sub

        If ReportDataListView.GetItemCount() = DirectCast(TypicalAccountInfoBindingSource. _
            DataSource, BindingList(Of TypicalAccountInfo)).Count Then
            ReportDataListView.CheckAll()
            Exit Sub
        End If

        For Each item As TypicalAccountInfo In ReportDataListView.FilteredObjects
            ReportDataListView.CheckObject(item)
        Next

    End Sub

    Sub Form1_KeyPress(ByVal sender As Object, _
        ByVal e As KeyPressEventArgs) Handles Me.KeyPress, ReportDataListView.KeyPress

        If Char.IsLetterOrDigit(e.KeyChar) Then

            _SearchString = _SearchString & e.KeyChar

            Me.ReportDataListView.AdditionalFilter = _
                    TextMatchFilter.Contains(Me.ReportDataListView, _SearchString)

            e.Handled = True

        End If

    End Sub

    Sub Form1_Keydown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown

        Dim handled As Boolean = False

        If _SearchString.Length > 0 Then

            If e.KeyCode = Keys.Delete Then

                _SearchString = ""
                handled = True

            ElseIf e.KeyCode = Keys.Back Then

                _SearchString = _SearchString.Substring(0, _SearchString.Length - 1)
                handled = True

            End If

        End If

        If handled Then
            Me.ReportDataListView.AdditionalFilter = _
                TextMatchFilter.Contains(Me.ReportDataListView, _SearchString)
            e.Handled = True
        End If

    End Sub

End Class
