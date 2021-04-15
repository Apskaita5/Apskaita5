Namespace WebControls
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Friend Class DownloadDataForm
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
            Me.ProgressBar1 = New System.Windows.Forms.ProgressBar
            Me.CancelDownloadButton = New System.Windows.Forms.Button
            Me.SuspendLayout()
            '
            'ProgressBar1
            '
            Me.ProgressBar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                        Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.ProgressBar1.Location = New System.Drawing.Point(12, 12)
            Me.ProgressBar1.Name = "ProgressBar1"
            Me.ProgressBar1.Size = New System.Drawing.Size(294, 23)
            Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous
            Me.ProgressBar1.TabIndex = 0
            '
            'CancelDownloadButton
            '
            Me.CancelDownloadButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
            Me.CancelDownloadButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.CancelDownloadButton.Location = New System.Drawing.Point(231, 41)
            Me.CancelDownloadButton.Name = "CancelDownloadButton"
            Me.CancelDownloadButton.Size = New System.Drawing.Size(75, 23)
            Me.CancelDownloadButton.TabIndex = 1
            Me.CancelDownloadButton.Text = "Atšaukti"
            Me.CancelDownloadButton.UseVisualStyleBackColor = True
            '
            'CheckForUpdateForm
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CancelButton = Me.CancelDownloadButton
            Me.ClientSize = New System.Drawing.Size(318, 72)
            Me.ControlBox = False
            Me.Controls.Add(Me.CancelDownloadButton)
            Me.Controls.Add(Me.ProgressBar1)
            Me.Name = "CheckForUpdateForm"
            Me.ShowIcon = False
            Me.ShowInTaskbar = False
            Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
            Me.Text = "Ieškoma atnaujinimo..."
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
        Friend WithEvents CancelDownloadButton As System.Windows.Forms.Button
    End Class
End Namespace
