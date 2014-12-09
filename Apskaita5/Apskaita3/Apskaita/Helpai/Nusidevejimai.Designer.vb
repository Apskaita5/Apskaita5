<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Nusidevejimai
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Nusidevejimai))
        Me.Isvedimas = New System.Windows.Forms.RichTextBox
        Me.SuspendLayout()
        '
        'Isvedimas
        '
        Me.Isvedimas.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Isvedimas.Location = New System.Drawing.Point(0, 0)
        Me.Isvedimas.Name = "Isvedimas"
        Me.Isvedimas.Size = New System.Drawing.Size(642, 466)
        Me.Isvedimas.TabIndex = 0
        Me.Isvedimas.Text = ""
        '
        'Nusidevejimai
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(642, 466)
        Me.Controls.Add(Me.Isvedimas)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Nusidevejimai"
        Me.Text = "Nusidevejimo normos metais"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Isvedimas As System.Windows.Forms.RichTextBox
End Class
