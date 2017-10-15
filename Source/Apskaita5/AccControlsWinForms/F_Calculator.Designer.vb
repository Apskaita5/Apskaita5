<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F_Calculator
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.CalculatorUserControl1 = New AccControlsWinForms.CalculatorUserControl
        Me.SuspendLayout()
        '
        'CalculatorUserControl1
        '
        Me.CalculatorUserControl1.CausesValidation = False
        Me.CalculatorUserControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CalculatorUserControl1.IsDialog = True
        Me.CalculatorUserControl1.Location = New System.Drawing.Point(0, 0)
        Me.CalculatorUserControl1.Name = "CalculatorUserControl1"
        Me.CalculatorUserControl1.Size = New System.Drawing.Size(244, 227)
        Me.CalculatorUserControl1.TabIndex = 0
        '
        'F_Calculator
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(244, 227)
        Me.Controls.Add(Me.CalculatorUserControl1)
        Me.MaximizeBox = False
        Me.Name = "F_Calculator"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "Calculator"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CalculatorUserControl1 As AccControlsWinForms.CalculatorUserControl
End Class
