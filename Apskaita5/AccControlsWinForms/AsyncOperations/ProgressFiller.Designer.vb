<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ProgressFiller
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar
        Me.CancelActionButton = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(99, 75)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(202, 23)
        Me.ProgressBar1.TabIndex = 0
        '
        'CancelActionButton
        '
        Me.CancelActionButton.Location = New System.Drawing.Point(159, 104)
        Me.CancelActionButton.Name = "CancelActionButton"
        Me.CancelActionButton.Size = New System.Drawing.Size(75, 23)
        Me.CancelActionButton.TabIndex = 1
        Me.CancelActionButton.Text = "Atšaukti"
        Me.CancelActionButton.UseVisualStyleBackColor = True
        '
        'ProgressFiller
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.CancelActionButton)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Name = "ProgressFiller"
        Me.Size = New System.Drawing.Size(422, 191)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents CancelActionButton As System.Windows.Forms.Button

End Class
