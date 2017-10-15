<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CalculatorUserControl
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.OkButton = New System.Windows.Forms.Button
        Me.PercentageButton = New System.Windows.Forms.Button
        Me.SqrtButton = New System.Windows.Forms.Button
        Me.DivideButton = New System.Windows.Forms.Button
        Me.Button9 = New System.Windows.Forms.Button
        Me.Button8 = New System.Windows.Forms.Button
        Me.BackSpaceButton = New System.Windows.Forms.Button
        Me.CeButton = New System.Windows.Forms.Button
        Me.CButton = New System.Windows.Forms.Button
        Me.ResultTextBox = New System.Windows.Forms.TextBox
        Me.Button7 = New System.Windows.Forms.Button
        Me.Button4 = New System.Windows.Forms.Button
        Me.Button5 = New System.Windows.Forms.Button
        Me.Button6 = New System.Windows.Forms.Button
        Me.MultiplyButton = New System.Windows.Forms.Button
        Me.Button1 = New System.Windows.Forms.Button
        Me.Button2 = New System.Windows.Forms.Button
        Me.Button3 = New System.Windows.Forms.Button
        Me.MinusButton = New System.Windows.Forms.Button
        Me.Button0 = New System.Windows.Forms.Button
        Me.SignButton = New System.Windows.Forms.Button
        Me.SeparatorButton = New System.Windows.Forms.Button
        Me.SumButton = New System.Windows.Forms.Button
        Me.ReverseDivideButton = New System.Windows.Forms.Button
        Me.ResultButton = New System.Windows.Forms.Button
        Me.DiscardButton = New System.Windows.Forms.Button
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 6
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 16.66667!))
        Me.TableLayoutPanel1.Controls.Add(Me.OkButton, 5, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.PercentageButton, 4, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.SqrtButton, 4, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.DivideButton, 3, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Button9, 2, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Button8, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.BackSpaceButton, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.CeButton, 2, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.CButton, 4, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.ResultTextBox, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Button7, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Button4, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.Button5, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.Button6, 2, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.MultiplyButton, 3, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.Button1, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.Button2, 1, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.Button3, 2, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.MinusButton, 3, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.Button0, 0, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.SignButton, 1, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.SeparatorButton, 2, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.SumButton, 3, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.ReverseDivideButton, 4, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.ResultButton, 4, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.DiscardButton, 5, 4)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 6
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(259, 217)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OkButton
        '
        Me.OkButton.CausesValidation = False
        Me.OkButton.Dock = System.Windows.Forms.DockStyle.Fill
        Me.OkButton.Image = Global.AccControlsWinForms.My.Resources.Resources.Accept_24x24
        Me.OkButton.Location = New System.Drawing.Point(218, 67)
        Me.OkButton.Name = "OkButton"
        Me.TableLayoutPanel1.SetRowSpan(Me.OkButton, 2)
        Me.OkButton.Size = New System.Drawing.Size(38, 70)
        Me.OkButton.TabIndex = 24
        Me.OkButton.TabStop = False
        Me.OkButton.UseVisualStyleBackColor = True
        '
        'PercentageButton
        '
        Me.PercentageButton.CausesValidation = False
        Me.PercentageButton.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PercentageButton.Location = New System.Drawing.Point(175, 105)
        Me.PercentageButton.Name = "PercentageButton"
        Me.PercentageButton.Size = New System.Drawing.Size(37, 32)
        Me.PercentageButton.TabIndex = 22
        Me.PercentageButton.TabStop = False
        Me.PercentageButton.Text = "%"
        Me.PercentageButton.UseVisualStyleBackColor = True
        '
        'SqrtButton
        '
        Me.SqrtButton.CausesValidation = False
        Me.SqrtButton.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SqrtButton.Location = New System.Drawing.Point(175, 67)
        Me.SqrtButton.Name = "SqrtButton"
        Me.SqrtButton.Size = New System.Drawing.Size(37, 32)
        Me.SqrtButton.TabIndex = 20
        Me.SqrtButton.TabStop = False
        Me.SqrtButton.Text = "Sqrt"
        Me.SqrtButton.UseVisualStyleBackColor = True
        '
        'DivideButton
        '
        Me.DivideButton.CausesValidation = False
        Me.DivideButton.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DivideButton.Location = New System.Drawing.Point(132, 67)
        Me.DivideButton.Name = "DivideButton"
        Me.DivideButton.Size = New System.Drawing.Size(37, 32)
        Me.DivideButton.TabIndex = 7
        Me.DivideButton.TabStop = False
        Me.DivideButton.Text = "/"
        Me.DivideButton.UseVisualStyleBackColor = True
        '
        'Button9
        '
        Me.Button9.CausesValidation = False
        Me.Button9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button9.Location = New System.Drawing.Point(89, 67)
        Me.Button9.Name = "Button9"
        Me.Button9.Size = New System.Drawing.Size(37, 32)
        Me.Button9.TabIndex = 6
        Me.Button9.TabStop = False
        Me.Button9.Text = "9"
        Me.Button9.UseVisualStyleBackColor = True
        '
        'Button8
        '
        Me.Button8.CausesValidation = False
        Me.Button8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button8.Location = New System.Drawing.Point(46, 67)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(37, 32)
        Me.Button8.TabIndex = 5
        Me.Button8.TabStop = False
        Me.Button8.Text = "8"
        Me.Button8.UseVisualStyleBackColor = True
        '
        'BackSpaceButton
        '
        Me.BackSpaceButton.CausesValidation = False
        Me.TableLayoutPanel1.SetColumnSpan(Me.BackSpaceButton, 2)
        Me.BackSpaceButton.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BackSpaceButton.Location = New System.Drawing.Point(3, 29)
        Me.BackSpaceButton.Name = "BackSpaceButton"
        Me.BackSpaceButton.Size = New System.Drawing.Size(80, 32)
        Me.BackSpaceButton.TabIndex = 0
        Me.BackSpaceButton.TabStop = False
        Me.BackSpaceButton.Text = "BackSpace"
        Me.BackSpaceButton.UseVisualStyleBackColor = True
        '
        'CeButton
        '
        Me.CeButton.CausesValidation = False
        Me.TableLayoutPanel1.SetColumnSpan(Me.CeButton, 2)
        Me.CeButton.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CeButton.Location = New System.Drawing.Point(89, 29)
        Me.CeButton.Name = "CeButton"
        Me.CeButton.Size = New System.Drawing.Size(80, 32)
        Me.CeButton.TabIndex = 1
        Me.CeButton.TabStop = False
        Me.CeButton.Text = "CE"
        Me.CeButton.UseVisualStyleBackColor = True
        '
        'CButton
        '
        Me.CButton.CausesValidation = False
        Me.TableLayoutPanel1.SetColumnSpan(Me.CButton, 2)
        Me.CButton.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CButton.Location = New System.Drawing.Point(175, 29)
        Me.CButton.Name = "CButton"
        Me.CButton.Size = New System.Drawing.Size(81, 32)
        Me.CButton.TabIndex = 2
        Me.CButton.TabStop = False
        Me.CButton.Text = "C"
        Me.CButton.UseVisualStyleBackColor = True
        '
        'ResultTextBox
        '
        Me.ResultTextBox.CausesValidation = False
        Me.TableLayoutPanel1.SetColumnSpan(Me.ResultTextBox, 6)
        Me.ResultTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ResultTextBox.Location = New System.Drawing.Point(3, 3)
        Me.ResultTextBox.Name = "ResultTextBox"
        Me.ResultTextBox.ReadOnly = True
        Me.ResultTextBox.Size = New System.Drawing.Size(253, 20)
        Me.ResultTextBox.TabIndex = 3
        Me.ResultTextBox.TabStop = False
        Me.ResultTextBox.Text = "0"
        Me.ResultTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Button7
        '
        Me.Button7.CausesValidation = False
        Me.Button7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button7.Location = New System.Drawing.Point(3, 67)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(37, 32)
        Me.Button7.TabIndex = 4
        Me.Button7.TabStop = False
        Me.Button7.Text = "7"
        Me.Button7.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.CausesValidation = False
        Me.Button4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button4.Location = New System.Drawing.Point(3, 105)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(37, 32)
        Me.Button4.TabIndex = 8
        Me.Button4.TabStop = False
        Me.Button4.Text = "4"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.CausesValidation = False
        Me.Button5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button5.Location = New System.Drawing.Point(46, 105)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(37, 32)
        Me.Button5.TabIndex = 9
        Me.Button5.TabStop = False
        Me.Button5.Text = "5"
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.CausesValidation = False
        Me.Button6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button6.Location = New System.Drawing.Point(89, 105)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(37, 32)
        Me.Button6.TabIndex = 10
        Me.Button6.TabStop = False
        Me.Button6.Text = "6"
        Me.Button6.UseVisualStyleBackColor = True
        '
        'MultiplyButton
        '
        Me.MultiplyButton.CausesValidation = False
        Me.MultiplyButton.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MultiplyButton.Location = New System.Drawing.Point(132, 105)
        Me.MultiplyButton.Name = "MultiplyButton"
        Me.MultiplyButton.Size = New System.Drawing.Size(37, 32)
        Me.MultiplyButton.TabIndex = 11
        Me.MultiplyButton.TabStop = False
        Me.MultiplyButton.Text = "*"
        Me.MultiplyButton.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.CausesValidation = False
        Me.Button1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button1.Location = New System.Drawing.Point(3, 143)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(37, 32)
        Me.Button1.TabIndex = 12
        Me.Button1.TabStop = False
        Me.Button1.Text = "1"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.CausesValidation = False
        Me.Button2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button2.Location = New System.Drawing.Point(46, 143)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(37, 32)
        Me.Button2.TabIndex = 13
        Me.Button2.TabStop = False
        Me.Button2.Text = "2"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.CausesValidation = False
        Me.Button3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button3.Location = New System.Drawing.Point(89, 143)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(37, 32)
        Me.Button3.TabIndex = 14
        Me.Button3.TabStop = False
        Me.Button3.Text = "3"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'MinusButton
        '
        Me.MinusButton.CausesValidation = False
        Me.MinusButton.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MinusButton.Location = New System.Drawing.Point(132, 143)
        Me.MinusButton.Name = "MinusButton"
        Me.MinusButton.Size = New System.Drawing.Size(37, 32)
        Me.MinusButton.TabIndex = 15
        Me.MinusButton.TabStop = False
        Me.MinusButton.Text = "-"
        Me.MinusButton.UseVisualStyleBackColor = True
        '
        'Button0
        '
        Me.Button0.CausesValidation = False
        Me.Button0.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Button0.Location = New System.Drawing.Point(3, 181)
        Me.Button0.Name = "Button0"
        Me.Button0.Size = New System.Drawing.Size(37, 33)
        Me.Button0.TabIndex = 16
        Me.Button0.TabStop = False
        Me.Button0.Text = "0"
        Me.Button0.UseVisualStyleBackColor = True
        '
        'SignButton
        '
        Me.SignButton.CausesValidation = False
        Me.SignButton.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SignButton.Image = Global.AccControlsWinForms.My.Resources.Resources.PlusMinus_18x18
        Me.SignButton.Location = New System.Drawing.Point(46, 181)
        Me.SignButton.Name = "SignButton"
        Me.SignButton.Size = New System.Drawing.Size(37, 33)
        Me.SignButton.TabIndex = 17
        Me.SignButton.TabStop = False
        Me.SignButton.UseVisualStyleBackColor = True
        '
        'SeparatorButton
        '
        Me.SeparatorButton.CausesValidation = False
        Me.SeparatorButton.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SeparatorButton.Location = New System.Drawing.Point(89, 181)
        Me.SeparatorButton.Name = "SeparatorButton"
        Me.SeparatorButton.Size = New System.Drawing.Size(37, 33)
        Me.SeparatorButton.TabIndex = 18
        Me.SeparatorButton.TabStop = False
        Me.SeparatorButton.Text = "."
        Me.SeparatorButton.UseVisualStyleBackColor = True
        '
        'SumButton
        '
        Me.SumButton.CausesValidation = False
        Me.SumButton.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SumButton.Location = New System.Drawing.Point(132, 181)
        Me.SumButton.Name = "SumButton"
        Me.SumButton.Size = New System.Drawing.Size(37, 33)
        Me.SumButton.TabIndex = 19
        Me.SumButton.TabStop = False
        Me.SumButton.Text = "+"
        Me.SumButton.UseVisualStyleBackColor = True
        '
        'ReverseDivideButton
        '
        Me.ReverseDivideButton.CausesValidation = False
        Me.ReverseDivideButton.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ReverseDivideButton.Location = New System.Drawing.Point(175, 143)
        Me.ReverseDivideButton.Name = "ReverseDivideButton"
        Me.ReverseDivideButton.Size = New System.Drawing.Size(37, 32)
        Me.ReverseDivideButton.TabIndex = 21
        Me.ReverseDivideButton.TabStop = False
        Me.ReverseDivideButton.Text = "1/x"
        Me.ReverseDivideButton.UseVisualStyleBackColor = True
        '
        'ResultButton
        '
        Me.ResultButton.CausesValidation = False
        Me.ResultButton.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ResultButton.Location = New System.Drawing.Point(175, 181)
        Me.ResultButton.Name = "ResultButton"
        Me.ResultButton.Size = New System.Drawing.Size(37, 33)
        Me.ResultButton.TabIndex = 23
        Me.ResultButton.TabStop = False
        Me.ResultButton.Text = "="
        Me.ResultButton.UseVisualStyleBackColor = True
        '
        'DiscardButton
        '
        Me.DiscardButton.CausesValidation = False
        Me.DiscardButton.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DiscardButton.Image = Global.AccControlsWinForms.My.Resources.Resources.Cancel_24x24
        Me.DiscardButton.Location = New System.Drawing.Point(218, 143)
        Me.DiscardButton.Name = "DiscardButton"
        Me.TableLayoutPanel1.SetRowSpan(Me.DiscardButton, 2)
        Me.DiscardButton.Size = New System.Drawing.Size(38, 71)
        Me.DiscardButton.TabIndex = 25
        Me.DiscardButton.TabStop = False
        Me.DiscardButton.UseVisualStyleBackColor = True
        '
        'CalculatorUserControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CausesValidation = False
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Name = "CalculatorUserControl"
        Me.Size = New System.Drawing.Size(259, 217)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents BackSpaceButton As System.Windows.Forms.Button
    Friend WithEvents CeButton As System.Windows.Forms.Button
    Friend WithEvents CButton As System.Windows.Forms.Button
    Friend WithEvents DivideButton As System.Windows.Forms.Button
    Friend WithEvents Button9 As System.Windows.Forms.Button
    Friend WithEvents Button8 As System.Windows.Forms.Button
    Friend WithEvents ResultTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Button5 As System.Windows.Forms.Button
    Friend WithEvents Button6 As System.Windows.Forms.Button
    Friend WithEvents MultiplyButton As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents MinusButton As System.Windows.Forms.Button
    Friend WithEvents Button0 As System.Windows.Forms.Button
    Friend WithEvents SignButton As System.Windows.Forms.Button
    Friend WithEvents SeparatorButton As System.Windows.Forms.Button
    Friend WithEvents SumButton As System.Windows.Forms.Button
    Friend WithEvents OkButton As System.Windows.Forms.Button
    Friend WithEvents PercentageButton As System.Windows.Forms.Button
    Friend WithEvents SqrtButton As System.Windows.Forms.Button
    Friend WithEvents ReverseDivideButton As System.Windows.Forms.Button
    Friend WithEvents ResultButton As System.Windows.Forms.Button
    Friend WithEvents DiscardButton As System.Windows.Forms.Button

End Class
