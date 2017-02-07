Imports System.Windows.Forms

Namespace WebControls

    Public Class PersonInfoListDialog

        Private _DataSource As PersonInfo()
        Private _ChosenPersonInfo As PersonInfo = Nothing


        Public ReadOnly Property ChosenPersonInfo() As PersonInfo
            Get
                Return _ChosenPersonInfo
            End Get
        End Property


        Public Sub New(ByVal dataSource As PersonInfo())

            InitializeComponent()

            _DataSource = dataSource
        End Sub


        Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
            If PersonInfoDataGridView.SelectedRows Is Nothing OrElse PersonInfoDataGridView.SelectedRows.Count < 1 Then
                MsgBox("Klaida. Nepasirinktas asmuo.", MsgBoxStyle.Exclamation, "Klaida")
                Exit Sub
            End If
            _ChosenPersonInfo = DirectCast(PersonInfoDataGridView.SelectedRows(0).DataBoundItem, PersonInfo)
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.Close()
        End Sub

        Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
            Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.Close()
        End Sub

        Private Sub PersonInfoListDialog_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
            If Not _DataSource Is Nothing AndAlso _DataSource.Length > 0 Then
                Me.PersonInfoBindingSource.DataSource = _DataSource
            End If
        End Sub

        Private Sub PersonInfoDataGridView_CellContentClick(ByVal sender As System.Object, _
                                                            ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) _
            Handles PersonInfoDataGridView.CellDoubleClick
            If e.RowIndex < 0 Then Exit Sub
            _ChosenPersonInfo = DirectCast(PersonInfoDataGridView.Rows(e.RowIndex).DataBoundItem, PersonInfo)
            Me.DialogResult = System.Windows.Forms.DialogResult.OK
            Me.Close()
        End Sub

    End Class

End Namespace