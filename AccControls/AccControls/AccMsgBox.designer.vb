<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AccMsgBox
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
        Me.TextPanel = New System.Windows.Forms.Panel
        Me.MsgTxt = New System.Windows.Forms.Label
        Me.ButtonsPanel = New System.Windows.Forms.SplitContainer
        Me.ExceptionText = New System.Windows.Forms.TextBox
        Me.TextPanel.SuspendLayout()
        Me.ButtonsPanel.Panel2.SuspendLayout()
        Me.ButtonsPanel.SuspendLayout()
        Me.SuspendLayout()
        '
        'TextPanel
        '
        Me.TextPanel.Controls.Add(Me.MsgTxt)
        Me.TextPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TextPanel.Location = New System.Drawing.Point(0, 0)
        Me.TextPanel.Margin = New System.Windows.Forms.Padding(0)
        Me.TextPanel.Name = "TextPanel"
        Me.TextPanel.Size = New System.Drawing.Size(329, 78)
        Me.TextPanel.TabIndex = 1
        '
        'MsgTxt
        '
        Me.MsgTxt.AutoSize = True
        Me.MsgTxt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MsgTxt.Location = New System.Drawing.Point(5, 5)
        Me.MsgTxt.Margin = New System.Windows.Forms.Padding(0)
        Me.MsgTxt.Name = "MsgTxt"
        Me.MsgTxt.Size = New System.Drawing.Size(39, 13)
        Me.MsgTxt.TabIndex = 0
        Me.MsgTxt.Text = "Label1"
        Me.MsgTxt.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'ButtonsPanel
        '
        Me.ButtonsPanel.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.ButtonsPanel.IsSplitterFixed = True
        Me.ButtonsPanel.Location = New System.Drawing.Point(0, 78)
        Me.ButtonsPanel.Name = "ButtonsPanel"
        Me.ButtonsPanel.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'ButtonsPanel.Panel2
        '
        Me.ButtonsPanel.Panel2.Controls.Add(Me.ExceptionText)
        Me.ButtonsPanel.Panel2Collapsed = True
        Me.ButtonsPanel.Size = New System.Drawing.Size(329, 50)
        Me.ButtonsPanel.SplitterDistance = 25
        Me.ButtonsPanel.TabIndex = 2
        '
        'ExceptionText
        '
        Me.ExceptionText.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.ExceptionText.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExceptionText.Location = New System.Drawing.Point(0, 0)
        Me.ExceptionText.Multiline = True
        Me.ExceptionText.Name = "ExceptionText"
        Me.ExceptionText.ReadOnly = True
        Me.ExceptionText.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.ExceptionText.Size = New System.Drawing.Size(150, 46)
        Me.ExceptionText.TabIndex = 0
        '
        'AccMsgBox
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CausesValidation = False
        Me.ClientSize = New System.Drawing.Size(329, 128)
        Me.Controls.Add(Me.TextPanel)
        Me.Controls.Add(Me.ButtonsPanel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "AccMsgBox"
        Me.Opacity = 0.9
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.TopMost = True
        Me.TextPanel.ResumeLayout(False)
        Me.TextPanel.PerformLayout()
        Me.ButtonsPanel.Panel2.ResumeLayout(False)
        Me.ButtonsPanel.Panel2.PerformLayout()
        Me.ButtonsPanel.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TextPanel As System.Windows.Forms.Panel
    Friend WithEvents MsgTxt As System.Windows.Forms.Label
    Friend WithEvents ButtonsPanel As System.Windows.Forms.SplitContainer
    Friend WithEvents ExceptionText As System.Windows.Forms.TextBox

End Class
