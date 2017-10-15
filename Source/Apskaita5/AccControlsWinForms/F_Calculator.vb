Public Class F_Calculator

    Private Sub F_Calculator_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CalculatorUserControl1.IsDialog = False
        CalculatorUserControl1.Focus()
    End Sub
End Class