<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F_NewGoodsOperation(Of T)
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
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.OperationDateDateTimePicker = New System.Windows.Forms.DateTimePicker
        Me.GoodsInfoListAccListComboBox = New AccControlsWinForms.AccListComboBox
        Me.WarehouseFromInfoListAccListComboBox = New AccControlsWinForms.AccListComboBox
        Me.WarehouseToInfoListAccListComboBox = New AccControlsWinForms.AccListComboBox
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel
        Me.nOkButton = New System.Windows.Forms.Button
        Me.nCancelButton = New System.Windows.Forms.Button
        Me.OperationDateLabel = New System.Windows.Forms.Label
        Me.GoodsLabel = New System.Windows.Forms.Label
        Me.WarehouseFromLabel = New System.Windows.Forms.Label
        Me.WarehouseToLabel = New System.Windows.Forms.Label
        Me.ProgressFiller1 = New AccControlsWinForms.ProgressFiller
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 3
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.OperationDateDateTimePicker, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.GoodsInfoListAccListComboBox, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.WarehouseFromInfoListAccListComboBox, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.WarehouseToInfoListAccListComboBox, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.OperationDateLabel, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.GoodsLabel, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.WarehouseFromLabel, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.WarehouseToLabel, 0, 3)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 5
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(437, 142)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'OperationDateDateTimePicker
        '
        Me.OperationDateDateTimePicker.Dock = System.Windows.Forms.DockStyle.Fill
        Me.OperationDateDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.OperationDateDateTimePicker.Location = New System.Drawing.Point(140, 3)
        Me.OperationDateDateTimePicker.Name = "OperationDateDateTimePicker"
        Me.OperationDateDateTimePicker.Size = New System.Drawing.Size(274, 20)
        Me.OperationDateDateTimePicker.TabIndex = 0
        '
        'GoodsInfoListAccListComboBox
        '
        Me.GoodsInfoListAccListComboBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GoodsInfoListAccListComboBox.EmptyValueString = ""
        Me.GoodsInfoListAccListComboBox.FilterString = ""
        Me.GoodsInfoListAccListComboBox.FormattingEnabled = True
        Me.GoodsInfoListAccListComboBox.InstantBinding = True
        Me.GoodsInfoListAccListComboBox.Location = New System.Drawing.Point(140, 29)
        Me.GoodsInfoListAccListComboBox.Name = "GoodsInfoListAccListComboBox"
        Me.GoodsInfoListAccListComboBox.Size = New System.Drawing.Size(274, 21)
        Me.GoodsInfoListAccListComboBox.TabIndex = 1
        '
        'WarehouseFromInfoListAccListComboBox
        '
        Me.WarehouseFromInfoListAccListComboBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WarehouseFromInfoListAccListComboBox.EmptyValueString = ""
        Me.WarehouseFromInfoListAccListComboBox.FilterString = ""
        Me.WarehouseFromInfoListAccListComboBox.FormattingEnabled = True
        Me.WarehouseFromInfoListAccListComboBox.InstantBinding = True
        Me.WarehouseFromInfoListAccListComboBox.Location = New System.Drawing.Point(140, 56)
        Me.WarehouseFromInfoListAccListComboBox.Name = "WarehouseFromInfoListAccListComboBox"
        Me.WarehouseFromInfoListAccListComboBox.Size = New System.Drawing.Size(274, 21)
        Me.WarehouseFromInfoListAccListComboBox.TabIndex = 2
        '
        'WarehouseToInfoListAccListComboBox
        '
        Me.WarehouseToInfoListAccListComboBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WarehouseToInfoListAccListComboBox.EmptyValueString = ""
        Me.WarehouseToInfoListAccListComboBox.FilterString = ""
        Me.WarehouseToInfoListAccListComboBox.FormattingEnabled = True
        Me.WarehouseToInfoListAccListComboBox.InstantBinding = True
        Me.WarehouseToInfoListAccListComboBox.Location = New System.Drawing.Point(140, 83)
        Me.WarehouseToInfoListAccListComboBox.Name = "WarehouseToInfoListAccListComboBox"
        Me.WarehouseToInfoListAccListComboBox.Size = New System.Drawing.Size(274, 21)
        Me.WarehouseToInfoListAccListComboBox.TabIndex = 3
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.AutoSize = True
        Me.TableLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.TableLayoutPanel2.ColumnCount = 4
        Me.TableLayoutPanel1.SetColumnSpan(Me.TableLayoutPanel2, 2)
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 15.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel2.Controls.Add(Me.nOkButton, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.nCancelButton, 3, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(0, 107)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(417, 35)
        Me.TableLayoutPanel2.TabIndex = 4
        '
        'nOkButton
        '
        Me.nOkButton.Location = New System.Drawing.Point(243, 3)
        Me.nOkButton.Name = "nOkButton"
        Me.nOkButton.Size = New System.Drawing.Size(75, 23)
        Me.nOkButton.TabIndex = 0
        Me.nOkButton.Text = "Ok"
        Me.nOkButton.UseVisualStyleBackColor = True
        '
        'nCancelButton
        '
        Me.nCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.nCancelButton.Location = New System.Drawing.Point(339, 3)
        Me.nCancelButton.Name = "nCancelButton"
        Me.nCancelButton.Size = New System.Drawing.Size(75, 23)
        Me.nCancelButton.TabIndex = 1
        Me.nCancelButton.Text = "Atšaukti"
        Me.nCancelButton.UseVisualStyleBackColor = True
        '
        'OperationDateLabel
        '
        Me.OperationDateLabel.AutoSize = True
        Me.OperationDateLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.OperationDateLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OperationDateLabel.Location = New System.Drawing.Point(3, 0)
        Me.OperationDateLabel.Name = "OperationDateLabel"
        Me.OperationDateLabel.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.OperationDateLabel.Size = New System.Drawing.Size(131, 26)
        Me.OperationDateLabel.TabIndex = 5
        Me.OperationDateLabel.Text = "Inventorizacijos Data:"
        Me.OperationDateLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'GoodsLabel
        '
        Me.GoodsLabel.AutoSize = True
        Me.GoodsLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GoodsLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GoodsLabel.Location = New System.Drawing.Point(3, 26)
        Me.GoodsLabel.Name = "GoodsLabel"
        Me.GoodsLabel.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.GoodsLabel.Size = New System.Drawing.Size(131, 27)
        Me.GoodsLabel.TabIndex = 6
        Me.GoodsLabel.Text = "Prekės:"
        Me.GoodsLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'WarehouseFromLabel
        '
        Me.WarehouseFromLabel.AutoSize = True
        Me.WarehouseFromLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WarehouseFromLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.WarehouseFromLabel.Location = New System.Drawing.Point(3, 53)
        Me.WarehouseFromLabel.Name = "WarehouseFromLabel"
        Me.WarehouseFromLabel.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.WarehouseFromLabel.Size = New System.Drawing.Size(131, 27)
        Me.WarehouseFromLabel.TabIndex = 7
        Me.WarehouseFromLabel.Text = "Iš Sandėlio:"
        Me.WarehouseFromLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'WarehouseToLabel
        '
        Me.WarehouseToLabel.AutoSize = True
        Me.WarehouseToLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WarehouseToLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.WarehouseToLabel.Location = New System.Drawing.Point(3, 80)
        Me.WarehouseToLabel.Name = "WarehouseToLabel"
        Me.WarehouseToLabel.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.WarehouseToLabel.Size = New System.Drawing.Size(131, 27)
        Me.WarehouseToLabel.TabIndex = 8
        Me.WarehouseToLabel.Text = "Į Sandėlį:"
        Me.WarehouseToLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'ProgressFiller1
        '
        Me.ProgressFiller1.Location = New System.Drawing.Point(153, 19)
        Me.ProgressFiller1.Name = "ProgressFiller1"
        Me.ProgressFiller1.Size = New System.Drawing.Size(180, 46)
        Me.ProgressFiller1.TabIndex = 1
        Me.ProgressFiller1.Visible = False
        '
        'F_NewGoodsOperation
        '
        Me.AcceptButton = Me.nOkButton
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.nCancelButton
        Me.ClientSize = New System.Drawing.Size(437, 142)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.ProgressFiller1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "F_NewGoodsOperation"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "F_NewGoodsOperation"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OperationDateDateTimePicker As System.Windows.Forms.DateTimePicker
    Friend WithEvents GoodsInfoListAccListComboBox As AccControlsWinForms.AccListComboBox
    Friend WithEvents WarehouseFromInfoListAccListComboBox As AccControlsWinForms.AccListComboBox
    Friend WithEvents WarehouseToInfoListAccListComboBox As AccControlsWinForms.AccListComboBox
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents nOkButton As System.Windows.Forms.Button
    Friend WithEvents nCancelButton As System.Windows.Forms.Button
    Friend WithEvents OperationDateLabel As System.Windows.Forms.Label
    Friend WithEvents GoodsLabel As System.Windows.Forms.Label
    Friend WithEvents WarehouseFromLabel As System.Windows.Forms.Label
    Friend WithEvents WarehouseToLabel As System.Windows.Forms.Label
    Friend WithEvents ProgressFiller1 As AccControlsWinForms.ProgressFiller
End Class
