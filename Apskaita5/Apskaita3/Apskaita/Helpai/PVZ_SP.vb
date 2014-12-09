Public Class PVZ_SP

    Private Sub PVZ_SP_FormClosing(ByVal sender As Object, _
        ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        GetFormLayout(Me)
    End Sub

    Private Sub PVZ_SP_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Isvedimas.Text = My.Resources.SP
        SetFormLayout(Me)
    End Sub
End Class