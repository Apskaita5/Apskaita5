<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AccUserContr
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.TblLayoutPnl = New System.Windows.Forms.TableLayoutPanel
        Me.Mtgc2 = New AccControls.MTGCComboBox
        Me.TxtBox = New AccControls.AccBox
        Me.Mtgc1 = New AccControls.MTGCComboBox
        Me.TblLayoutPnl.SuspendLayout()
        Me.SuspendLayout()
        '
        'TblLayoutPnl
        '
        Me.TblLayoutPnl.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.TblLayoutPnl.ColumnCount = 3
        Me.TblLayoutPnl.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 63.96396!))
        Me.TblLayoutPnl.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 36.03604!))
        Me.TblLayoutPnl.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 142.0!))
        Me.TblLayoutPnl.Controls.Add(Me.Mtgc2, 2, 0)
        Me.TblLayoutPnl.Controls.Add(Me.TxtBox, 1, 0)
        Me.TblLayoutPnl.Controls.Add(Me.Mtgc1, 0, 0)
        Me.TblLayoutPnl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TblLayoutPnl.Location = New System.Drawing.Point(0, 0)
        Me.TblLayoutPnl.Name = "TblLayoutPnl"
        Me.TblLayoutPnl.RowCount = 1
        Me.TblLayoutPnl.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TblLayoutPnl.Size = New System.Drawing.Size(323, 27)
        Me.TblLayoutPnl.TabIndex = 0
        '
        'Mtgc2
        '
        Me.Mtgc2.BorderStyle = AccControls.MTGCComboBox.TipiBordi.Fixed3D
        Me.Mtgc2.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.Mtgc2.ColumnNum = 1
        Me.Mtgc2.ColumnWidth = "121"
        Me.Mtgc2.DisplayMember = "Text"
        Me.Mtgc2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Mtgc2.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.Mtgc2.DropDownBackColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(210, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.Mtgc2.DropDownForeColor = System.Drawing.Color.Black
        Me.Mtgc2.DropDownStyle = AccControls.MTGCComboBox.CustomDropDownStyle.DropDown
        Me.Mtgc2.DropDownWidth = 141
        Me.Mtgc2.GridLineColor = System.Drawing.Color.LightGray
        Me.Mtgc2.GridLineHorizontal = False
        Me.Mtgc2.GridLineVertical = False
        Me.Mtgc2.LoadingType = AccControls.MTGCComboBox.CaricamentoCombo.ComboBoxItem
        Me.Mtgc2.Location = New System.Drawing.Point(183, 3)
        Me.Mtgc2.ManagingFastMouseMoving = True
        Me.Mtgc2.ManagingFastMouseMovingInterval = 30
        Me.Mtgc2.Name = "Mtgc2"
        Me.Mtgc2.Size = New System.Drawing.Size(137, 21)
        Me.Mtgc2.TabIndex = 4
        '
        'TxtBox
        '
        Me.TxtBox.AddZeros = True
        Me.TxtBox.AllowLith = True
        Me.TxtBox.Apvalinimas = CType(2, Byte)
        Me.TxtBox.BackColor = System.Drawing.Color.White
        Me.TxtBox.ColorOnError = System.Drawing.Color.LightPink
        Me.TxtBox.ColorOnOk = System.Drawing.Color.White
        Me.TxtBox.ColorOnWarning = System.Drawing.Color.Yellow
        Me.TxtBox.DecimalValue = 0
        Me.TxtBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TxtBox.EmptyAsError = False
        Me.TxtBox.EmptyAsWarn = False
        Me.TxtBox.InputType = AccControls.AccBox.Tip.Skaicius
        Me.TxtBox.Location = New System.Drawing.Point(118, 3)
        Me.TxtBox.MaxLength = 20
        Me.TxtBox.MaxNumber = 999999999999
        Me.TxtBox.Name = "TxtBox"
        Me.TxtBox.NegativeValue = False
        Me.TxtBox.Size = New System.Drawing.Size(59, 20)
        Me.TxtBox.TabIndex = 2
        Me.TxtBox.Text = "0,00"
        Me.TxtBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.TxtBox.UseSeparator = AccControls.AccBox.Separator.OnValidate
        Me.TxtBox.ZeroAsEmpty = True
        '
        'Mtgc1
        '
        Me.Mtgc1.BorderStyle = AccControls.MTGCComboBox.TipiBordi.Fixed3D
        Me.Mtgc1.CharacterCasing = System.Windows.Forms.CharacterCasing.Normal
        Me.Mtgc1.ColumnNum = 1
        Me.Mtgc1.ColumnWidth = "121"
        Me.Mtgc1.DisplayMember = "Text"
        Me.Mtgc1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Mtgc1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed
        Me.Mtgc1.DropDownBackColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(210, Byte), Integer), CType(CType(238, Byte), Integer))
        Me.Mtgc1.DropDownForeColor = System.Drawing.Color.Black
        Me.Mtgc1.DropDownStyle = AccControls.MTGCComboBox.CustomDropDownStyle.DropDown
        Me.Mtgc1.DropDownWidth = 141
        Me.Mtgc1.GridLineColor = System.Drawing.Color.LightGray
        Me.Mtgc1.GridLineHorizontal = False
        Me.Mtgc1.GridLineVertical = False
        Me.Mtgc1.LoadingType = AccControls.MTGCComboBox.CaricamentoCombo.ComboBoxItem
        Me.Mtgc1.Location = New System.Drawing.Point(3, 3)
        Me.Mtgc1.ManagingFastMouseMoving = True
        Me.Mtgc1.ManagingFastMouseMovingInterval = 30
        Me.Mtgc1.Name = "Mtgc1"
        Me.Mtgc1.Size = New System.Drawing.Size(109, 21)
        Me.Mtgc1.TabIndex = 3
        '
        'AccUserContr
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.TblLayoutPnl)
        Me.Name = "AccUserContr"
        Me.Size = New System.Drawing.Size(323, 27)
        Me.TblLayoutPnl.ResumeLayout(False)
        Me.TblLayoutPnl.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TblLayoutPnl As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TxtBox As AccBox
    Friend WithEvents Mtgc1 As MTGCComboBox
    Friend WithEvents Mtgc2 As MTGCComboBox

End Class
