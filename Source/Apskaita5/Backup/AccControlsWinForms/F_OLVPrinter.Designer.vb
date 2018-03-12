<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F_OLVPrinter
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
        Dim PenData4 As BrightIdeasSoftware.PenData = New BrightIdeasSoftware.PenData
        Dim SolidBrushData5 As BrightIdeasSoftware.SolidBrushData = New BrightIdeasSoftware.SolidBrushData
        Dim SolidBrushData6 As BrightIdeasSoftware.SolidBrushData = New BrightIdeasSoftware.SolidBrushData
        Dim PenData5 As BrightIdeasSoftware.PenData = New BrightIdeasSoftware.PenData
        Dim SolidBrushData7 As BrightIdeasSoftware.SolidBrushData = New BrightIdeasSoftware.SolidBrushData
        Dim PenData6 As BrightIdeasSoftware.PenData = New BrightIdeasSoftware.PenData
        Dim SolidBrushData8 As BrightIdeasSoftware.SolidBrushData = New BrightIdeasSoftware.SolidBrushData
        Dim LinearGradientBrushData2 As BrightIdeasSoftware.LinearGradientBrushData = New BrightIdeasSoftware.LinearGradientBrushData
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.groupBox1 = New System.Windows.Forms.GroupBox
        Me.tbWatermark = New System.Windows.Forms.TextBox
        Me.tbFooter = New System.Windows.Forms.TextBox
        Me.tbHeader = New System.Windows.Forms.TextBox
        Me.label5 = New System.Windows.Forms.Label
        Me.label4 = New System.Windows.Forms.Label
        Me.label3 = New System.Windows.Forms.Label
        Me.groupBox2 = New System.Windows.Forms.GroupBox
        Me.cbListHeaderOnEveryPage = New System.Windows.Forms.CheckBox
        Me.cbPrintSelection = New System.Windows.Forms.CheckBox
        Me.cbShrinkToFit = New System.Windows.Forms.CheckBox
        Me.cbUseGridLines = New System.Windows.Forms.CheckBox
        Me.rbStyleOTT = New System.Windows.Forms.RadioButton
        Me.rbStyleModern = New System.Windows.Forms.RadioButton
        Me.rbStyleMinimal = New System.Windows.Forms.RadioButton
        Me.label6 = New System.Windows.Forms.Label
        Me.groupBox3 = New System.Windows.Forms.GroupBox
        Me.radioButton3 = New System.Windows.Forms.RadioButton
        Me.radioButton1 = New System.Windows.Forms.RadioButton
        Me.radioButton2 = New System.Windows.Forms.RadioButton
        Me.radioButton4 = New System.Windows.Forms.RadioButton
        Me.numericUpDown1 = New System.Windows.Forms.NumericUpDown
        Me.label2 = New System.Windows.Forms.Label
        Me.button3 = New System.Windows.Forms.Button
        Me.button2 = New System.Windows.Forms.Button
        Me.button1 = New System.Windows.Forms.Button
        Me.listViewPrinter1 = New BrightIdeasSoftware.ListViewPrinter
        Me.printPreviewControl1 = New System.Windows.Forms.PrintPreviewControl
        Me.Panel1.SuspendLayout()
        Me.groupBox1.SuspendLayout()
        Me.groupBox2.SuspendLayout()
        Me.groupBox3.SuspendLayout()
        CType(Me.numericUpDown1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.groupBox1)
        Me.Panel1.Controls.Add(Me.groupBox2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(840, 96)
        Me.Panel1.TabIndex = 1
        '
        'groupBox1
        '
        Me.groupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.groupBox1.Controls.Add(Me.tbWatermark)
        Me.groupBox1.Controls.Add(Me.tbFooter)
        Me.groupBox1.Controls.Add(Me.tbHeader)
        Me.groupBox1.Controls.Add(Me.label5)
        Me.groupBox1.Controls.Add(Me.label4)
        Me.groupBox1.Controls.Add(Me.label3)
        Me.groupBox1.Location = New System.Drawing.Point(368, 10)
        Me.groupBox1.Name = "groupBox1"
        Me.groupBox1.Size = New System.Drawing.Size(465, 80)
        Me.groupBox1.TabIndex = 2
        Me.groupBox1.TabStop = False
        Me.groupBox1.Text = "Settings"
        '
        'tbWatermark
        '
        Me.tbWatermark.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbWatermark.Location = New System.Drawing.Point(88, 56)
        Me.tbWatermark.Name = "tbWatermark"
        Me.tbWatermark.Size = New System.Drawing.Size(369, 20)
        Me.tbWatermark.TabIndex = 5
        '
        'tbFooter
        '
        Me.tbFooter.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbFooter.Location = New System.Drawing.Point(88, 35)
        Me.tbFooter.Name = "tbFooter"
        Me.tbFooter.Size = New System.Drawing.Size(369, 20)
        Me.tbFooter.TabIndex = 3
        Me.tbFooter.Text = "{1:f}\t\tPsl. {0}"
        '
        'tbHeader
        '
        Me.tbHeader.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbHeader.Location = New System.Drawing.Point(88, 13)
        Me.tbHeader.Name = "tbHeader"
        Me.tbHeader.Size = New System.Drawing.Size(369, 20)
        Me.tbHeader.TabIndex = 1
        Me.tbHeader.Text = "\tListViewPrinter"
        '
        'label5
        '
        Me.label5.AutoSize = True
        Me.label5.Location = New System.Drawing.Point(6, 59)
        Me.label5.Name = "label5"
        Me.label5.Size = New System.Drawing.Size(62, 13)
        Me.label5.TabIndex = 4
        Me.label5.Text = "&Watermark:"
        '
        'label4
        '
        Me.label4.AutoSize = True
        Me.label4.Location = New System.Drawing.Point(6, 38)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(40, 13)
        Me.label4.TabIndex = 2
        Me.label4.Text = "&Footer:"
        '
        'label3
        '
        Me.label3.AutoSize = True
        Me.label3.Location = New System.Drawing.Point(6, 16)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(45, 13)
        Me.label3.TabIndex = 0
        Me.label3.Text = "&Header:"
        '
        'groupBox2
        '
        Me.groupBox2.Controls.Add(Me.cbListHeaderOnEveryPage)
        Me.groupBox2.Controls.Add(Me.cbPrintSelection)
        Me.groupBox2.Controls.Add(Me.cbShrinkToFit)
        Me.groupBox2.Controls.Add(Me.cbUseGridLines)
        Me.groupBox2.Controls.Add(Me.rbStyleOTT)
        Me.groupBox2.Controls.Add(Me.rbStyleModern)
        Me.groupBox2.Controls.Add(Me.rbStyleMinimal)
        Me.groupBox2.Controls.Add(Me.label6)
        Me.groupBox2.Location = New System.Drawing.Point(11, 10)
        Me.groupBox2.Name = "groupBox2"
        Me.groupBox2.Size = New System.Drawing.Size(351, 79)
        Me.groupBox2.TabIndex = 1
        Me.groupBox2.TabStop = False
        Me.groupBox2.Text = "Formatting"
        '
        'cbListHeaderOnEveryPage
        '
        Me.cbListHeaderOnEveryPage.AutoSize = True
        Me.cbListHeaderOnEveryPage.Checked = True
        Me.cbListHeaderOnEveryPage.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbListHeaderOnEveryPage.Location = New System.Drawing.Point(159, 58)
        Me.cbListHeaderOnEveryPage.Name = "cbListHeaderOnEveryPage"
        Me.cbListHeaderOnEveryPage.Size = New System.Drawing.Size(149, 17)
        Me.cbListHeaderOnEveryPage.TabIndex = 8
        Me.cbListHeaderOnEveryPage.Text = "List header on every page"
        Me.cbListHeaderOnEveryPage.UseVisualStyleBackColor = True
        '
        'cbPrintSelection
        '
        Me.cbPrintSelection.AutoSize = True
        Me.cbPrintSelection.Location = New System.Drawing.Point(9, 58)
        Me.cbPrintSelection.Name = "cbPrintSelection"
        Me.cbPrintSelection.Size = New System.Drawing.Size(114, 17)
        Me.cbPrintSelection.TabIndex = 7
        Me.cbPrintSelection.Text = "Print only selection"
        Me.cbPrintSelection.UseVisualStyleBackColor = True
        '
        'cbShrinkToFit
        '
        Me.cbShrinkToFit.AutoSize = True
        Me.cbShrinkToFit.Checked = True
        Me.cbShrinkToFit.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbShrinkToFit.Location = New System.Drawing.Point(159, 41)
        Me.cbShrinkToFit.Name = "cbShrinkToFit"
        Me.cbShrinkToFit.Size = New System.Drawing.Size(79, 17)
        Me.cbShrinkToFit.TabIndex = 6
        Me.cbShrinkToFit.Text = "Shrink to fit"
        Me.cbShrinkToFit.UseVisualStyleBackColor = True
        '
        'cbUseGridLines
        '
        Me.cbUseGridLines.AutoSize = True
        Me.cbUseGridLines.Checked = True
        Me.cbUseGridLines.CheckState = System.Windows.Forms.CheckState.Checked
        Me.cbUseGridLines.Location = New System.Drawing.Point(9, 41)
        Me.cbUseGridLines.Name = "cbUseGridLines"
        Me.cbUseGridLines.Size = New System.Drawing.Size(89, 17)
        Me.cbUseGridLines.TabIndex = 5
        Me.cbUseGridLines.Text = "Use grid lines"
        Me.cbUseGridLines.UseVisualStyleBackColor = True
        '
        'rbStyleOTT
        '
        Me.rbStyleOTT.AutoSize = True
        Me.rbStyleOTT.Location = New System.Drawing.Point(177, 17)
        Me.rbStyleOTT.Name = "rbStyleOTT"
        Me.rbStyleOTT.Size = New System.Drawing.Size(87, 17)
        Me.rbStyleOTT.TabIndex = 3
        Me.rbStyleOTT.Text = "Over the top!"
        Me.rbStyleOTT.UseVisualStyleBackColor = True
        '
        'rbStyleModern
        '
        Me.rbStyleModern.AutoSize = True
        Me.rbStyleModern.Checked = True
        Me.rbStyleModern.Location = New System.Drawing.Point(111, 17)
        Me.rbStyleModern.Name = "rbStyleModern"
        Me.rbStyleModern.Size = New System.Drawing.Size(61, 17)
        Me.rbStyleModern.TabIndex = 2
        Me.rbStyleModern.TabStop = True
        Me.rbStyleModern.Text = "Modern"
        Me.rbStyleModern.UseVisualStyleBackColor = True
        '
        'rbStyleMinimal
        '
        Me.rbStyleMinimal.AutoSize = True
        Me.rbStyleMinimal.Location = New System.Drawing.Point(45, 17)
        Me.rbStyleMinimal.Name = "rbStyleMinimal"
        Me.rbStyleMinimal.Size = New System.Drawing.Size(60, 17)
        Me.rbStyleMinimal.TabIndex = 1
        Me.rbStyleMinimal.Text = "Minimal"
        Me.rbStyleMinimal.UseVisualStyleBackColor = True
        '
        'label6
        '
        Me.label6.AutoSize = True
        Me.label6.Location = New System.Drawing.Point(6, 19)
        Me.label6.Name = "label6"
        Me.label6.Size = New System.Drawing.Size(33, 13)
        Me.label6.TabIndex = 0
        Me.label6.Text = "&Style:"
        '
        'groupBox3
        '
        Me.groupBox3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.groupBox3.Controls.Add(Me.radioButton3)
        Me.groupBox3.Controls.Add(Me.radioButton1)
        Me.groupBox3.Controls.Add(Me.radioButton2)
        Me.groupBox3.Controls.Add(Me.radioButton4)
        Me.groupBox3.Location = New System.Drawing.Point(730, 191)
        Me.groupBox3.Name = "groupBox3"
        Me.groupBox3.Size = New System.Drawing.Size(95, 95)
        Me.groupBox3.TabIndex = 11
        Me.groupBox3.TabStop = False
        Me.groupBox3.Text = "Magnification:"
        '
        'radioButton3
        '
        Me.radioButton3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.radioButton3.AutoSize = True
        Me.radioButton3.Location = New System.Drawing.Point(15, 71)
        Me.radioButton3.Name = "radioButton3"
        Me.radioButton3.Size = New System.Drawing.Size(45, 17)
        Me.radioButton3.TabIndex = 3
        Me.radioButton3.Text = "&50%"
        Me.radioButton3.UseVisualStyleBackColor = True
        '
        'radioButton1
        '
        Me.radioButton1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.radioButton1.AutoSize = True
        Me.radioButton1.Location = New System.Drawing.Point(15, 37)
        Me.radioButton1.Name = "radioButton1"
        Me.radioButton1.Size = New System.Drawing.Size(51, 17)
        Me.radioButton1.TabIndex = 1
        Me.radioButton1.Text = "&200%"
        Me.radioButton1.UseVisualStyleBackColor = True
        '
        'radioButton2
        '
        Me.radioButton2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.radioButton2.AutoSize = True
        Me.radioButton2.Location = New System.Drawing.Point(15, 54)
        Me.radioButton2.Name = "radioButton2"
        Me.radioButton2.Size = New System.Drawing.Size(51, 17)
        Me.radioButton2.TabIndex = 2
        Me.radioButton2.Text = "&100%"
        Me.radioButton2.UseVisualStyleBackColor = True
        '
        'radioButton4
        '
        Me.radioButton4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.radioButton4.AutoSize = True
        Me.radioButton4.Checked = True
        Me.radioButton4.Location = New System.Drawing.Point(15, 19)
        Me.radioButton4.Name = "radioButton4"
        Me.radioButton4.Size = New System.Drawing.Size(47, 17)
        Me.radioButton4.TabIndex = 0
        Me.radioButton4.TabStop = True
        Me.radioButton4.Text = "&Auto"
        Me.radioButton4.UseVisualStyleBackColor = True
        '
        'numericUpDown1
        '
        Me.numericUpDown1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.numericUpDown1.Location = New System.Drawing.Point(776, 319)
        Me.numericUpDown1.Maximum = New Decimal(New Integer() {6, 0, 0, 0})
        Me.numericUpDown1.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numericUpDown1.Name = "numericUpDown1"
        Me.numericUpDown1.Size = New System.Drawing.Size(41, 20)
        Me.numericUpDown1.TabIndex = 13
        Me.numericUpDown1.Value = New Decimal(New Integer() {2, 0, 0, 0})
        '
        'label2
        '
        Me.label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.label2.AutoSize = True
        Me.label2.Location = New System.Drawing.Point(730, 321)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(40, 13)
        Me.label2.TabIndex = 12
        Me.label2.Text = "&Pages:"
        '
        'button3
        '
        Me.button3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.button3.Location = New System.Drawing.Point(730, 162)
        Me.button3.Name = "button3"
        Me.button3.Size = New System.Drawing.Size(95, 23)
        Me.button3.TabIndex = 10
        Me.button3.Text = "&Print..."
        Me.button3.UseVisualStyleBackColor = True
        '
        'button2
        '
        Me.button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.button2.Location = New System.Drawing.Point(730, 133)
        Me.button2.Name = "button2"
        Me.button2.Size = New System.Drawing.Size(95, 23)
        Me.button2.TabIndex = 9
        Me.button2.Text = "Print Pre&view..."
        Me.button2.UseVisualStyleBackColor = True
        '
        'button1
        '
        Me.button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.button1.Location = New System.Drawing.Point(730, 104)
        Me.button1.Name = "button1"
        Me.button1.Size = New System.Drawing.Size(95, 23)
        Me.button1.TabIndex = 8
        Me.button1.Text = "Page &Setup..."
        Me.button1.UseVisualStyleBackColor = True
        '
        'listViewPrinter1
        '
        '
        '
        '
        SolidBrushData5.Color = System.Drawing.Color.Maroon
        PenData4.Brush = SolidBrushData5
        Me.listViewPrinter1.CellFormat.AllBorderPenData = PenData4
        Me.listViewPrinter1.CellFormat.BottomBorderPenData = PenData4
        Me.listViewPrinter1.CellFormat.CanWrap = True
        Me.listViewPrinter1.CellFormat.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!)
        Me.listViewPrinter1.CellFormat.LeftBorderPenData = PenData4
        Me.listViewPrinter1.CellFormat.RightBorderPenData = PenData4
        SolidBrushData6.Color = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.listViewPrinter1.CellFormat.TextBrushData = SolidBrushData6
        Me.listViewPrinter1.CellFormat.TopBorderPenData = PenData4
        Me.listViewPrinter1.Footer = "{1}" & Global.Microsoft.VisualBasic.ChrW(9) & Global.Microsoft.VisualBasic.ChrW(9) & "Page #{0}"
        '
        '
        '
        Me.listViewPrinter1.FooterFormat.Font = New System.Drawing.Font("Verdana", 10.0!, System.Drawing.FontStyle.Italic)
        '
        '
        '
        SolidBrushData7.Color = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer))
        PenData5.Brush = SolidBrushData7
        PenData5.Width = 10.0!
        Me.listViewPrinter1.GroupHeaderFormat.AllBorderPenData = PenData5
        Me.listViewPrinter1.GroupHeaderFormat.BottomBorderPenData = PenData5
        Me.listViewPrinter1.GroupHeaderFormat.Font = New System.Drawing.Font("Verdana", 10.0!, System.Drawing.FontStyle.Bold)
        Me.listViewPrinter1.GroupHeaderFormat.LeftBorderPenData = PenData5
        Me.listViewPrinter1.GroupHeaderFormat.RightBorderPenData = PenData5
        Me.listViewPrinter1.GroupHeaderFormat.TopBorderPenData = PenData5
        Me.listViewPrinter1.Header = "ListViewPrinterDemo"
        '
        '
        '
        SolidBrushData8.Color = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        PenData6.Brush = SolidBrushData8
        PenData6.Width = 4.0!
        Me.listViewPrinter1.HeaderFormat.AllBorderPenData = PenData6
        LinearGradientBrushData2.FromColor = System.Drawing.Color.Aqua
        LinearGradientBrushData2.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal
        LinearGradientBrushData2.ToColor = System.Drawing.Color.Pink
        Me.listViewPrinter1.HeaderFormat.BackgroundBrushData = LinearGradientBrushData2
        Me.listViewPrinter1.HeaderFormat.BottomBorderPenData = PenData6
        Me.listViewPrinter1.HeaderFormat.Font = New System.Drawing.Font("Verdana", 24.0!)
        Me.listViewPrinter1.HeaderFormat.LeftBorderPenData = PenData6
        Me.listViewPrinter1.HeaderFormat.RightBorderPenData = PenData6
        Me.listViewPrinter1.HeaderFormat.TopBorderPenData = PenData6
        Me.listViewPrinter1.IsListHeaderOnEachPage = False
        '
        '
        '
        Me.listViewPrinter1.ListHeaderFormat.CanWrap = True
        Me.listViewPrinter1.ListHeaderFormat.Font = New System.Drawing.Font("Verdana", 12.0!)
        Me.listViewPrinter1.Watermark = "Top Secret"
        '
        'printPreviewControl1
        '
        Me.printPreviewControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.printPreviewControl1.AutoZoom = False
        Me.printPreviewControl1.Columns = 2
        Me.printPreviewControl1.Document = Me.listViewPrinter1
        Me.printPreviewControl1.Location = New System.Drawing.Point(11, 104)
        Me.printPreviewControl1.Name = "printPreviewControl1"
        Me.printPreviewControl1.Size = New System.Drawing.Size(702, 282)
        Me.printPreviewControl1.TabIndex = 14
        Me.printPreviewControl1.Zoom = 0.45765611633875108
        '
        'F_OLVPrinter
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(840, 398)
        Me.Controls.Add(Me.printPreviewControl1)
        Me.Controls.Add(Me.groupBox3)
        Me.Controls.Add(Me.numericUpDown1)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.button3)
        Me.Controls.Add(Me.button2)
        Me.Controls.Add(Me.button1)
        Me.Controls.Add(Me.Panel1)
        Me.Name = "F_OLVPrinter"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Panel1.ResumeLayout(False)
        Me.groupBox1.ResumeLayout(False)
        Me.groupBox1.PerformLayout()
        Me.groupBox2.ResumeLayout(False)
        Me.groupBox2.PerformLayout()
        Me.groupBox3.ResumeLayout(False)
        Me.groupBox3.PerformLayout()
        CType(Me.numericUpDown1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Private WithEvents groupBox1 As System.Windows.Forms.GroupBox
    Private WithEvents tbWatermark As System.Windows.Forms.TextBox
    Private WithEvents tbFooter As System.Windows.Forms.TextBox
    Private WithEvents tbHeader As System.Windows.Forms.TextBox
    Private WithEvents label5 As System.Windows.Forms.Label
    Private WithEvents label4 As System.Windows.Forms.Label
    Private WithEvents label3 As System.Windows.Forms.Label
    Private WithEvents groupBox2 As System.Windows.Forms.GroupBox
    Private WithEvents cbListHeaderOnEveryPage As System.Windows.Forms.CheckBox
    Private WithEvents cbPrintSelection As System.Windows.Forms.CheckBox
    Private WithEvents cbShrinkToFit As System.Windows.Forms.CheckBox
    Private WithEvents cbUseGridLines As System.Windows.Forms.CheckBox
    Private WithEvents rbStyleOTT As System.Windows.Forms.RadioButton
    Private WithEvents rbStyleModern As System.Windows.Forms.RadioButton
    Private WithEvents rbStyleMinimal As System.Windows.Forms.RadioButton
    Private WithEvents label6 As System.Windows.Forms.Label
    Private WithEvents groupBox3 As System.Windows.Forms.GroupBox
    Private WithEvents radioButton3 As System.Windows.Forms.RadioButton
    Private WithEvents radioButton1 As System.Windows.Forms.RadioButton
    Private WithEvents radioButton2 As System.Windows.Forms.RadioButton
    Private WithEvents radioButton4 As System.Windows.Forms.RadioButton
    Private WithEvents numericUpDown1 As System.Windows.Forms.NumericUpDown
    Private WithEvents label2 As System.Windows.Forms.Label
    Private WithEvents button3 As System.Windows.Forms.Button
    Private WithEvents button2 As System.Windows.Forms.Button
    Private WithEvents button1 As System.Windows.Forms.Button
    Private WithEvents listViewPrinter1 As BrightIdeasSoftware.ListViewPrinter
    Private WithEvents printPreviewControl1 As System.Windows.Forms.PrintPreviewControl
End Class
