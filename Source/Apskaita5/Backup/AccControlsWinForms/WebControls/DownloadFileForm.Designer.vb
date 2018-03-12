Namespace WebControls
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class DownloadFileForm
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
            Me.CancelUpdateButton = New System.Windows.Forms.Button
            Me.ProgressBar1 = New System.Windows.Forms.ProgressBar
            Me.SuspendLayout()
            '
            'CancelUpdateButton
            '
            Me.CancelUpdateButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CancelUpdateButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CancelUpdateButton.Location = New System.Drawing.Point(223, 41)
            Me.CancelUpdateButton.Name = "CancelUpdateButton"
            Me.CancelUpdateButton.Size = New System.Drawing.Size(76, 23)
            Me.CancelUpdateButton.TabIndex = 3
            Me.CancelUpdateButton.Text = "Atšaukti"
            Me.CancelUpdateButton.UseVisualStyleBackColor = True
            '
            'ProgressBar1
            '
            Me.ProgressBar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ProgressBar1.Location = New System.Drawing.Point(12, 12)
            Me.ProgressBar1.Name = "ProgressBar1"
            Me.ProgressBar1.Size = New System.Drawing.Size(287, 23)
            Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous
            Me.ProgressBar1.TabIndex = 2
            '
            'DownloadFileForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CausesValidation = False
            Me.ClientSize = New System.Drawing.Size(311, 74)
            Me.ControlBox = False
            Me.Controls.Add(Me.CancelUpdateButton)
            Me.Controls.Add(Me.ProgressBar1)
            Me.Name = "DownloadFileForm"
            Me.ShowIcon = False
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "DownloadFileForm"
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents CancelUpdateButton As System.Windows.Forms.Button
        Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    End Class
End Namespace