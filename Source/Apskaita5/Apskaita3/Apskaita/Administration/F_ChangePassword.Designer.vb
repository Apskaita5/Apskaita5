﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F_ChangePassword
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
        Me.PasswordTextBox = New System.Windows.Forms.TextBox
        Me.RepeatedPasswordTextBox = New System.Windows.Forms.TextBox
        Me.OkButtonI = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.OldPasswordTextBox = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'PasswordTextBox
        '
        Me.PasswordTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PasswordTextBox.Location = New System.Drawing.Point(13, 64)
        Me.PasswordTextBox.MaxLength = 50
        Me.PasswordTextBox.Name = "PasswordTextBox"
        Me.PasswordTextBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.PasswordTextBox.Size = New System.Drawing.Size(182, 20)
        Me.PasswordTextBox.TabIndex = 1
        Me.PasswordTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'RepeatedPasswordTextBox
        '
        Me.RepeatedPasswordTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RepeatedPasswordTextBox.Location = New System.Drawing.Point(13, 105)
        Me.RepeatedPasswordTextBox.MaxLength = 50
        Me.RepeatedPasswordTextBox.Name = "RepeatedPasswordTextBox"
        Me.RepeatedPasswordTextBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.RepeatedPasswordTextBox.Size = New System.Drawing.Size(182, 20)
        Me.RepeatedPasswordTextBox.TabIndex = 2
        Me.RepeatedPasswordTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'OkButtonI
        '
        Me.OkButtonI.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.OkButtonI.Location = New System.Drawing.Point(71, 137)
        Me.OkButtonI.Name = "OkButtonI"
        Me.OkButtonI.Size = New System.Drawing.Size(62, 23)
        Me.OkButtonI.TabIndex = 3
        Me.OkButtonI.Text = "Ok"
        Me.OkButtonI.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(49, 48)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(113, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Naujas slaptažodis"
        '
        'OldPasswordTextBox
        '
        Me.OldPasswordTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OldPasswordTextBox.Location = New System.Drawing.Point(13, 23)
        Me.OldPasswordTextBox.MaxLength = 50
        Me.OldPasswordTextBox.Name = "OldPasswordTextBox"
        Me.OldPasswordTextBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.OldPasswordTextBox.Size = New System.Drawing.Size(182, 20)
        Me.OldPasswordTextBox.TabIndex = 0
        Me.OldPasswordTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(49, 7)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(109, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Senas slaptažodis"
        '
        'Label3
        '
        Me.Label3.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(30, 89)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(154, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Pakartoti naują slaptažodį"
        '
        'F_ChangePassword
        '
        Me.AcceptButton = Me.OkButtonI
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(209, 167)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.OldPasswordTextBox)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.OkButtonI)
        Me.Controls.Add(Me.RepeatedPasswordTextBox)
        Me.Controls.Add(Me.PasswordTextBox)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "F_ChangePassword"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.TopMost = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PasswordTextBox As System.Windows.Forms.TextBox
    Friend WithEvents RepeatedPasswordTextBox As System.Windows.Forms.TextBox
    Friend WithEvents OkButtonI As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents OldPasswordTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
