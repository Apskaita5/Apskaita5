<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F_NewGoodsProductionOperation
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
        Me.ProductionCalculationInfoListAccListComboBox = New AccControlsWinForms.AccListComboBox
        Me.UseCalculationCheckBox = New System.Windows.Forms.CheckBox
        Me.OperationDateLabel = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.GoodsInfoListAccListComboBox = New AccControlsWinForms.AccListComboBox
        Me.WarehouseFromInfoListAccListComboBox = New AccControlsWinForms.AccListComboBox
        Me.WarehouseToInfoListAccListComboBox = New AccControlsWinForms.AccListComboBox
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel
        Me.nOkButton = New System.Windows.Forms.Button
        Me.nCancelButton = New System.Windows.Forms.Button
        Me.ProgressFiller1 = New AccControlsWinForms.ProgressFiller
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 4
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.ProductionCalculationInfoListAccListComboBox, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.UseCalculationCheckBox, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.OperationDateLabel, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label3, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.GoodsInfoListAccListComboBox, 2, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.WarehouseFromInfoListAccListComboBox, 2, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.WarehouseToInfoListAccListComboBox, 2, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 2, 4)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 5
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(455, 142)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'ProductionCalculationInfoListAccListComboBox
        '
        Me.ProductionCalculationInfoListAccListComboBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ProductionCalculationInfoListAccListComboBox.EmptyValueString = ""
        Me.ProductionCalculationInfoListAccListComboBox.FilterString = ""
        Me.ProductionCalculationInfoListAccListComboBox.FormattingEnabled = True
        Me.ProductionCalculationInfoListAccListComboBox.InstantBinding = True
        Me.ProductionCalculationInfoListAccListComboBox.Location = New System.Drawing.Point(153, 3)
        Me.ProductionCalculationInfoListAccListComboBox.Name = "ProductionCalculationInfoListAccListComboBox"
        Me.ProductionCalculationInfoListAccListComboBox.Size = New System.Drawing.Size(279, 21)
        Me.ProductionCalculationInfoListAccListComboBox.TabIndex = 13
        '
        'UseCalculationCheckBox
        '
        Me.UseCalculationCheckBox.AutoSize = True
        Me.UseCalculationCheckBox.Location = New System.Drawing.Point(132, 3)
        Me.UseCalculationCheckBox.Name = "UseCalculationCheckBox"
        Me.UseCalculationCheckBox.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.UseCalculationCheckBox.Size = New System.Drawing.Size(15, 17)
        Me.UseCalculationCheckBox.TabIndex = 1
        Me.UseCalculationCheckBox.UseVisualStyleBackColor = True
        '
        'OperationDateLabel
        '
        Me.OperationDateLabel.AutoSize = True
        Me.OperationDateLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.OperationDateLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OperationDateLabel.Location = New System.Drawing.Point(3, 0)
        Me.OperationDateLabel.Name = "OperationDateLabel"
        Me.OperationDateLabel.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.OperationDateLabel.Size = New System.Drawing.Size(123, 27)
        Me.OperationDateLabel.TabIndex = 6
        Me.OperationDateLabel.Text = "Kalkuliacija:"
        Me.OperationDateLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(3, 27)
        Me.Label3.Name = "Label3"
        Me.Label3.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.Label3.Size = New System.Drawing.Size(123, 27)
        Me.Label3.TabIndex = 9
        Me.Label3.Text = "Gaminys:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(3, 54)
        Me.Label1.Name = "Label1"
        Me.Label1.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.Label1.Size = New System.Drawing.Size(123, 27)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Sandėlis Gaminiams:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(3, 81)
        Me.Label2.Name = "Label2"
        Me.Label2.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.Label2.Size = New System.Drawing.Size(123, 27)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Sandėlis Žaliavoms:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'GoodsInfoListAccListComboBox
        '
        Me.GoodsInfoListAccListComboBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GoodsInfoListAccListComboBox.EmptyValueString = ""
        Me.GoodsInfoListAccListComboBox.FilterString = ""
        Me.GoodsInfoListAccListComboBox.FormattingEnabled = True
        Me.GoodsInfoListAccListComboBox.InstantBinding = True
        Me.GoodsInfoListAccListComboBox.Location = New System.Drawing.Point(153, 30)
        Me.GoodsInfoListAccListComboBox.Name = "GoodsInfoListAccListComboBox"
        Me.GoodsInfoListAccListComboBox.Size = New System.Drawing.Size(279, 21)
        Me.GoodsInfoListAccListComboBox.TabIndex = 10
        '
        'WarehouseFromInfoListAccListComboBox
        '
        Me.WarehouseFromInfoListAccListComboBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WarehouseFromInfoListAccListComboBox.EmptyValueString = ""
        Me.WarehouseFromInfoListAccListComboBox.FilterString = ""
        Me.WarehouseFromInfoListAccListComboBox.FormattingEnabled = True
        Me.WarehouseFromInfoListAccListComboBox.InstantBinding = True
        Me.WarehouseFromInfoListAccListComboBox.Location = New System.Drawing.Point(153, 84)
        Me.WarehouseFromInfoListAccListComboBox.Name = "WarehouseFromInfoListAccListComboBox"
        Me.WarehouseFromInfoListAccListComboBox.Size = New System.Drawing.Size(279, 21)
        Me.WarehouseFromInfoListAccListComboBox.TabIndex = 11
        '
        'WarehouseToInfoListAccListComboBox
        '
        Me.WarehouseToInfoListAccListComboBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WarehouseToInfoListAccListComboBox.EmptyValueString = ""
        Me.WarehouseToInfoListAccListComboBox.FilterString = ""
        Me.WarehouseToInfoListAccListComboBox.FormattingEnabled = True
        Me.WarehouseToInfoListAccListComboBox.InstantBinding = True
        Me.WarehouseToInfoListAccListComboBox.Location = New System.Drawing.Point(153, 57)
        Me.WarehouseToInfoListAccListComboBox.Name = "WarehouseToInfoListAccListComboBox"
        Me.WarehouseToInfoListAccListComboBox.Size = New System.Drawing.Size(279, 21)
        Me.WarehouseToInfoListAccListComboBox.TabIndex = 12
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
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(150, 108)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(305, 34)
        Me.TableLayoutPanel2.TabIndex = 14
        '
        'nOkButton
        '
        Me.nOkButton.Location = New System.Drawing.Point(131, 3)
        Me.nOkButton.Name = "nOkButton"
        Me.nOkButton.Size = New System.Drawing.Size(75, 23)
        Me.nOkButton.TabIndex = 0
        Me.nOkButton.Text = "Ok"
        Me.nOkButton.UseVisualStyleBackColor = True
        '
        'nCancelButton
        '
        Me.nCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.nCancelButton.Location = New System.Drawing.Point(227, 3)
        Me.nCancelButton.Name = "nCancelButton"
        Me.nCancelButton.Size = New System.Drawing.Size(75, 23)
        Me.nCancelButton.TabIndex = 1
        Me.nCancelButton.Text = "Atšaukti"
        Me.nCancelButton.UseVisualStyleBackColor = True
        '
        'ProgressFiller1
        '
        Me.ProgressFiller1.Location = New System.Drawing.Point(195, 17)
        Me.ProgressFiller1.Name = "ProgressFiller1"
        Me.ProgressFiller1.Size = New System.Drawing.Size(109, 32)
        Me.ProgressFiller1.TabIndex = 1
        Me.ProgressFiller1.Visible = False
        '
        'F_NewGoodsProductionOperation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(455, 142)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.ProgressFiller1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "F_NewGoodsProductionOperation"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Nauja prekių gamybos operacija"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents UseCalculationCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents OperationDateLabel As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GoodsInfoListAccListComboBox As AccControlsWinForms.AccListComboBox
    Friend WithEvents WarehouseFromInfoListAccListComboBox As AccControlsWinForms.AccListComboBox
    Friend WithEvents WarehouseToInfoListAccListComboBox As AccControlsWinForms.AccListComboBox
    Friend WithEvents ProductionCalculationInfoListAccListComboBox As AccControlsWinForms.AccListComboBox
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents nOkButton As System.Windows.Forms.Button
    Friend WithEvents nCancelButton As System.Windows.Forms.Button
    Friend WithEvents ProgressFiller1 As AccControlsWinForms.ProgressFiller
End Class
