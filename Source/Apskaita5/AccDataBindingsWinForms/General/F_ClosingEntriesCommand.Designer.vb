﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Friend Class F_ClosingEntriesCommand
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(F_ClosingEntriesCommand))
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.ICancelButton = New System.Windows.Forms.Button
        Me.IOkButton = New System.Windows.Forms.Button
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.ConsolidatedAccountAccGridComboBox = New AccControlsWinForms.AccListComboBox
        Me.CurrentProfitAccountAccGridComboBox = New AccControlsWinForms.AccListComboBox
        Me.FormerProfitAccountAccGridComboBox = New AccControlsWinForms.AccListComboBox
        Me.ProgressFiller1 = New AccControlsWinForms.ProgressFiller
        Me.ClosingDateAccDatePicker = New AccControlsWinForms.AccDatePicker
        Me.Panel2.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.AutoSize = True
        Me.Panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel2.Controls.Add(Me.ICancelButton)
        Me.Panel2.Controls.Add(Me.IOkButton)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 129)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(338, 32)
        Me.Panel2.TabIndex = 1
        '
        'ICancelButton
        '
        Me.ICancelButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ICancelButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ICancelButton.Location = New System.Drawing.Point(237, 6)
        Me.ICancelButton.Name = "ICancelButton"
        Me.ICancelButton.Size = New System.Drawing.Size(89, 23)
        Me.ICancelButton.TabIndex = 1
        Me.ICancelButton.Text = "Atšaukti"
        Me.ICancelButton.UseVisualStyleBackColor = True
        '
        'IOkButton
        '
        Me.IOkButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.IOkButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IOkButton.Location = New System.Drawing.Point(133, 6)
        Me.IOkButton.Name = "IOkButton"
        Me.IOkButton.Size = New System.Drawing.Size(89, 23)
        Me.IOkButton.TabIndex = 0
        Me.IOkButton.Text = "Ok"
        Me.IOkButton.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label3, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Label4, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.ConsolidatedAccountAccGridComboBox, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.CurrentProfitAccountAccGridComboBox, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.FormerProfitAccountAccGridComboBox, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.ClosingDateAccDatePicker, 1, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 5
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(338, 129)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(67, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.Label1.Size = New System.Drawing.Size(95, 18)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Uždarymo data:"
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(61, 26)
        Me.Label2.Name = "Label2"
        Me.Label2.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.Label2.Size = New System.Drawing.Size(101, 18)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Suvestinė sąsk.:"
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(63, 53)
        Me.Label3.Name = "Label3"
        Me.Label3.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.Label3.Size = New System.Drawing.Size(99, 18)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Rezultato sąsk.:"
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(3, 80)
        Me.Label4.Name = "Label4"
        Me.Label4.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.Label4.Size = New System.Drawing.Size(159, 18)
        Me.Label4.TabIndex = 3
        Me.Label4.Text = "Praėj. laik. rezultato sąsk.:"
        '
        'ConsolidatedAccountAccGridComboBox
        '
        Me.ConsolidatedAccountAccGridComboBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ConsolidatedAccountAccGridComboBox.EmptyValueString = ""
        Me.ConsolidatedAccountAccGridComboBox.InstantBinding = True
        Me.ConsolidatedAccountAccGridComboBox.Location = New System.Drawing.Point(168, 29)
        Me.ConsolidatedAccountAccGridComboBox.Name = "ConsolidatedAccountAccGridComboBox"
        Me.ConsolidatedAccountAccGridComboBox.Size = New System.Drawing.Size(147, 21)
        Me.ConsolidatedAccountAccGridComboBox.TabIndex = 1
        '
        'CurrentProfitAccountAccGridComboBox
        '
        Me.CurrentProfitAccountAccGridComboBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CurrentProfitAccountAccGridComboBox.EmptyValueString = ""
        Me.CurrentProfitAccountAccGridComboBox.InstantBinding = True
        Me.CurrentProfitAccountAccGridComboBox.Location = New System.Drawing.Point(168, 56)
        Me.CurrentProfitAccountAccGridComboBox.Name = "CurrentProfitAccountAccGridComboBox"
        Me.CurrentProfitAccountAccGridComboBox.Size = New System.Drawing.Size(147, 21)
        Me.CurrentProfitAccountAccGridComboBox.TabIndex = 2
        '
        'FormerProfitAccountAccGridComboBox
        '
        Me.FormerProfitAccountAccGridComboBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FormerProfitAccountAccGridComboBox.EmptyValueString = ""
        Me.FormerProfitAccountAccGridComboBox.InstantBinding = True
        Me.FormerProfitAccountAccGridComboBox.Location = New System.Drawing.Point(168, 83)
        Me.FormerProfitAccountAccGridComboBox.Name = "FormerProfitAccountAccGridComboBox"
        Me.FormerProfitAccountAccGridComboBox.Size = New System.Drawing.Size(147, 21)
        Me.FormerProfitAccountAccGridComboBox.TabIndex = 3
        '
        'ProgressFiller1
        '
        Me.ProgressFiller1.Location = New System.Drawing.Point(167, 14)
        Me.ProgressFiller1.Name = "ProgressFiller1"
        Me.ProgressFiller1.Size = New System.Drawing.Size(133, 50)
        Me.ProgressFiller1.TabIndex = 2
        Me.ProgressFiller1.Visible = False
        '
        'ClosingDateAccDatePicker
        '
        Me.ClosingDateAccDatePicker.BoldedDates = Nothing
        Me.ClosingDateAccDatePicker.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ClosingDateAccDatePicker.Location = New System.Drawing.Point(168, 3)
        Me.ClosingDateAccDatePicker.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.ClosingDateAccDatePicker.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.ClosingDateAccDatePicker.Name = "ClosingDateAccDatePicker"
        Me.ClosingDateAccDatePicker.ReadOnly = False
        Me.ClosingDateAccDatePicker.ShowWeekNumbers = True
        Me.ClosingDateAccDatePicker.Size = New System.Drawing.Size(147, 20)
        Me.ClosingDateAccDatePicker.TabIndex = 0
        '
        'F_ClosingEntriesCommand
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(338, 161)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.ProgressFiller1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "F_ClosingEntriesCommand"
        Me.ShowInTaskbar = False
        Me.Text = "5/6 klasių uždarymas"
        Me.Panel2.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents ICancelButton As System.Windows.Forms.Button
    Friend WithEvents IOkButton As System.Windows.Forms.Button
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents ConsolidatedAccountAccGridComboBox As AccControlsWinForms.AccListComboBox
    Friend WithEvents CurrentProfitAccountAccGridComboBox As AccControlsWinForms.AccListComboBox
    Friend WithEvents FormerProfitAccountAccGridComboBox As AccControlsWinForms.AccListComboBox
    Friend WithEvents ProgressFiller1 As AccControlsWinForms.ProgressFiller
    Friend WithEvents ClosingDateAccDatePicker As AccControlsWinForms.AccDatePicker
End Class
