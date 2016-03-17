<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Friend Class F_NewInvoiceAdapterForGoodsOperation(Of T)
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
        Me.Label3 = New System.Windows.Forms.Label
        Me.WarehouseInfoListAccGridComboBox = New AccControlsWinForms.AccListComboBox
        Me.SelectGoodsInfoButton = New System.Windows.Forms.Button
        Me.GoodsInfoListAccGridComboBox = New AccControlsWinForms.AccListComboBox
        Me.BarCodeTextBox = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.ICancelButton = New System.Windows.Forms.Button
        Me.IOkButton = New System.Windows.Forms.Button
        Me.ProgressFiller1 = New AccControlsWinForms.ProgressFiller
        Me.SuspendLayout()
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(9, 42)
        Me.Label3.Margin = New System.Windows.Forms.Padding(3)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 13)
        Me.Label3.TabIndex = 68
        Me.Label3.Text = "Sandėlis:"
        '
        'WarehouseInfoListAccGridComboBox
        '
        Me.WarehouseInfoListAccGridComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.WarehouseInfoListAccGridComboBox.EmptyValueString = ""
        Me.WarehouseInfoListAccGridComboBox.FilterString = ""
        Me.WarehouseInfoListAccGridComboBox.FormattingEnabled = True
        Me.WarehouseInfoListAccGridComboBox.InstantBinding = True
        Me.WarehouseInfoListAccGridComboBox.Location = New System.Drawing.Point(69, 39)
        Me.WarehouseInfoListAccGridComboBox.Name = "WarehouseInfoListAccGridComboBox"
        Me.WarehouseInfoListAccGridComboBox.Size = New System.Drawing.Size(510, 21)
        Me.WarehouseInfoListAccGridComboBox.TabIndex = 66
        '
        'SelectGoodsInfoButton
        '
        Me.SelectGoodsInfoButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SelectGoodsInfoButton.Image = Global.AccDataBindingsWinForms.My.Resources.Resources.Button_Reload_icon_16p
        Me.SelectGoodsInfoButton.Location = New System.Drawing.Point(555, 10)
        Me.SelectGoodsInfoButton.Margin = New System.Windows.Forms.Padding(0)
        Me.SelectGoodsInfoButton.Name = "SelectGoodsInfoButton"
        Me.SelectGoodsInfoButton.Size = New System.Drawing.Size(24, 24)
        Me.SelectGoodsInfoButton.TabIndex = 65
        Me.SelectGoodsInfoButton.UseVisualStyleBackColor = True
        '
        'GoodsInfoListAccGridComboBox
        '
        Me.GoodsInfoListAccGridComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GoodsInfoListAccGridComboBox.EmptyValueString = ""
        Me.GoodsInfoListAccGridComboBox.FilterString = ""
        Me.GoodsInfoListAccGridComboBox.FormattingEnabled = True
        Me.GoodsInfoListAccGridComboBox.InstantBinding = True
        Me.GoodsInfoListAccGridComboBox.Location = New System.Drawing.Point(12, 12)
        Me.GoodsInfoListAccGridComboBox.Name = "GoodsInfoListAccGridComboBox"
        Me.GoodsInfoListAccGridComboBox.Size = New System.Drawing.Size(315, 21)
        Me.GoodsInfoListAccGridComboBox.TabIndex = 63
        '
        'BarCodeTextBox
        '
        Me.BarCodeTextBox.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BarCodeTextBox.Location = New System.Drawing.Point(394, 12)
        Me.BarCodeTextBox.MaxLength = 100
        Me.BarCodeTextBox.Name = "BarCodeTextBox"
        Me.BarCodeTextBox.Size = New System.Drawing.Size(158, 20)
        Me.BarCodeTextBox.TabIndex = 64
        Me.BarCodeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(330, 16)
        Me.Label2.Margin = New System.Windows.Forms.Padding(0, 5, 3, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 13)
        Me.Label2.TabIndex = 67
        Me.Label2.Text = "Barkodas:"
        '
        'ICancelButton
        '
        Me.ICancelButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ICancelButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ICancelButton.Location = New System.Drawing.Point(490, 76)
        Me.ICancelButton.Name = "ICancelButton"
        Me.ICancelButton.Size = New System.Drawing.Size(89, 23)
        Me.ICancelButton.TabIndex = 70
        Me.ICancelButton.Text = "Atšaukti"
        Me.ICancelButton.UseVisualStyleBackColor = True
        '
        'IOkButton
        '
        Me.IOkButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.IOkButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IOkButton.Location = New System.Drawing.Point(394, 76)
        Me.IOkButton.Name = "IOkButton"
        Me.IOkButton.Size = New System.Drawing.Size(89, 23)
        Me.IOkButton.TabIndex = 69
        Me.IOkButton.Text = "Ok"
        Me.IOkButton.UseVisualStyleBackColor = True
        '
        'ProgressFiller1
        '
        Me.ProgressFiller1.Location = New System.Drawing.Point(144, 70)
        Me.ProgressFiller1.Name = "ProgressFiller1"
        Me.ProgressFiller1.Size = New System.Drawing.Size(84, 28)
        Me.ProgressFiller1.TabIndex = 71
        Me.ProgressFiller1.Visible = False
        '
        'F_NewInvoiceAdapterForGoodsOperation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(591, 108)
        Me.Controls.Add(Me.ICancelButton)
        Me.Controls.Add(Me.IOkButton)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.WarehouseInfoListAccGridComboBox)
        Me.Controls.Add(Me.SelectGoodsInfoButton)
        Me.Controls.Add(Me.GoodsInfoListAccGridComboBox)
        Me.Controls.Add(Me.BarCodeTextBox)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.ProgressFiller1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "F_NewInvoiceAdapterForGoodsOperation"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "InvoiceAdapterForGoodsOperation"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents WarehouseInfoListAccGridComboBox As AccControlsWinForms.AccListComboBox
    Friend WithEvents SelectGoodsInfoButton As System.Windows.Forms.Button
    Friend WithEvents GoodsInfoListAccGridComboBox As AccControlsWinForms.AccListComboBox
    Friend WithEvents BarCodeTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ICancelButton As System.Windows.Forms.Button
    Friend WithEvents IOkButton As System.Windows.Forms.Button
    Friend WithEvents ProgressFiller1 As AccControlsWinForms.ProgressFiller
End Class
