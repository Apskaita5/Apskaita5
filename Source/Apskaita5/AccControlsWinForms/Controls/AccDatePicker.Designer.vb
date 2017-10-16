<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AccDatePicker
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
        Me.DropdownButton = New System.Windows.Forms.Button
        Me.ValueTextBox = New AccControlsWinForms.AccDateTextBox
        Me.SuspendLayout()
        '
        'DropdownButton
        '
        Me.DropdownButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DropdownButton.Location = New System.Drawing.Point(109, 0)
        Me.DropdownButton.Margin = New System.Windows.Forms.Padding(0)
        Me.DropdownButton.Name = "DropdownButton"
        Me.DropdownButton.Size = New System.Drawing.Size(20, 20)
        Me.DropdownButton.TabIndex = 1
        Me.DropdownButton.TabStop = False
        Me.DropdownButton.Text = "V"
        Me.DropdownButton.UseVisualStyleBackColor = True
        '
        'ValueTextBox
        '
        Me.ValueTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ValueTextBox.Location = New System.Drawing.Point(0, 0)
        Me.ValueTextBox.MaxLength = 10
        Me.ValueTextBox.Name = "ValueTextBox"
        Me.ValueTextBox.Size = New System.Drawing.Size(109, 20)
        Me.ValueTextBox.TabIndex = 2
        '
        'AccDatePicker
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.ValueTextBox)
        Me.Controls.Add(Me.DropdownButton)
        Me.Name = "AccDatePicker"
        Me.Size = New System.Drawing.Size(129, 20)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DropdownButton As System.Windows.Forms.Button
    Friend WithEvents ValueTextBox As AccControlsWinForms.AccDateTextBox

End Class
