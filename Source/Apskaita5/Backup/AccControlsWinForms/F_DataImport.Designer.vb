<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F_DataImport
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
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.SourceTextBox = New System.Windows.Forms.TextBox
        Me.FieldsTabDelimitedCheckBox = New System.Windows.Forms.CheckBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.FieldDelimiterTextBox = New System.Windows.Forms.TextBox
        Me.LinesCrLfDelimitedCheckBox = New System.Windows.Forms.CheckBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.QuotationMarksUsedCheckBox = New System.Windows.Forms.CheckBox
        Me.LinesDelimiterTextBox = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.QuotationMarkTextBox = New System.Windows.Forms.TextBox
        Me.RefreshButton = New System.Windows.Forms.Button
        Me.DataGridView1 = New System.Windows.Forms.DataGridView
        Me.AddColumnButton = New System.Windows.Forms.Button
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.ImportButton = New System.Windows.Forms.Button
        Me.DiscardButton = New System.Windows.Forms.Button
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 10
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 21.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.SourceTextBox, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.FieldsTabDelimitedCheckBox, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label6, 2, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.FieldDelimiterTextBox, 3, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.LinesCrLfDelimitedCheckBox, 5, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label4, 7, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.QuotationMarksUsedCheckBox, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.LinesDelimiterTextBox, 8, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 2, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.QuotationMarkTextBox, 3, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.RefreshButton, 8, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.AddColumnButton, 7, 2)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 3
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(903, 90)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'SourceTextBox
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.SourceTextBox, 8)
        Me.SourceTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SourceTextBox.Location = New System.Drawing.Point(135, 3)
        Me.SourceTextBox.Name = "SourceTextBox"
        Me.SourceTextBox.ReadOnly = True
        Me.SourceTextBox.Size = New System.Drawing.Size(744, 20)
        Me.SourceTextBox.TabIndex = 6
        '
        'FieldsTabDelimitedCheckBox
        '
        Me.FieldsTabDelimitedCheckBox.AutoSize = True
        Me.FieldsTabDelimitedCheckBox.Location = New System.Drawing.Point(3, 29)
        Me.FieldsTabDelimitedCheckBox.Name = "FieldsTabDelimitedCheckBox"
        Me.FieldsTabDelimitedCheckBox.Size = New System.Drawing.Size(115, 17)
        Me.FieldsTabDelimitedCheckBox.TabIndex = 6
        Me.FieldsTabDelimitedCheckBox.Text = "Laukai atskirti TAB"
        Me.FieldsTabDelimitedCheckBox.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Dock = System.Windows.Forms.DockStyle.Right
        Me.Label1.Location = New System.Drawing.Point(86, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.Label1.Size = New System.Drawing.Size(43, 26)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Šaltinis:"
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(155, 26)
        Me.Label6.Name = "Label6"
        Me.Label6.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.Label6.Size = New System.Drawing.Size(85, 18)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "Laukų skirtukas:"
        '
        'FieldDelimiterTextBox
        '
        Me.FieldDelimiterTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FieldDelimiterTextBox.Location = New System.Drawing.Point(246, 29)
        Me.FieldDelimiterTextBox.MaxLength = 10
        Me.FieldDelimiterTextBox.Name = "FieldDelimiterTextBox"
        Me.FieldDelimiterTextBox.Size = New System.Drawing.Size(184, 20)
        Me.FieldDelimiterTextBox.TabIndex = 8
        Me.FieldDelimiterTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'LinesCrLfDelimitedCheckBox
        '
        Me.LinesCrLfDelimitedCheckBox.AutoSize = True
        Me.LinesCrLfDelimitedCheckBox.Location = New System.Drawing.Point(456, 29)
        Me.LinesCrLfDelimitedCheckBox.Name = "LinesCrLfDelimitedCheckBox"
        Me.LinesCrLfDelimitedCheckBox.Size = New System.Drawing.Size(121, 17)
        Me.LinesCrLfDelimitedCheckBox.TabIndex = 7
        Me.LinesCrLfDelimitedCheckBox.Text = "Eilutės atskirtos CrLf"
        Me.LinesCrLfDelimitedCheckBox.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(603, 26)
        Me.Label4.Name = "Label4"
        Me.Label4.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.Label4.Size = New System.Drawing.Size(86, 18)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Eilučių skirtukas:"
        '
        'QuotationMarksUsedCheckBox
        '
        Me.QuotationMarksUsedCheckBox.AutoSize = True
        Me.QuotationMarksUsedCheckBox.Location = New System.Drawing.Point(3, 55)
        Me.QuotationMarksUsedCheckBox.Name = "QuotationMarksUsedCheckBox"
        Me.QuotationMarksUsedCheckBox.Size = New System.Drawing.Size(126, 17)
        Me.QuotationMarksUsedCheckBox.TabIndex = 8
        Me.QuotationMarksUsedCheckBox.Text = "Naudojamos kabutės"
        Me.QuotationMarksUsedCheckBox.UseVisualStyleBackColor = True
        '
        'LinesDelimiterTextBox
        '
        Me.LinesDelimiterTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LinesDelimiterTextBox.Location = New System.Drawing.Point(695, 29)
        Me.LinesDelimiterTextBox.MaxLength = 10
        Me.LinesDelimiterTextBox.Name = "LinesDelimiterTextBox"
        Me.LinesDelimiterTextBox.Size = New System.Drawing.Size(184, 20)
        Me.LinesDelimiterTextBox.TabIndex = 7
        Me.LinesDelimiterTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(191, 52)
        Me.Label2.Name = "Label2"
        Me.Label2.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.Label2.Size = New System.Drawing.Size(49, 18)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Kabutės:"
        '
        'QuotationMarkTextBox
        '
        Me.QuotationMarkTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.QuotationMarkTextBox.Location = New System.Drawing.Point(246, 55)
        Me.QuotationMarkTextBox.MaxLength = 10
        Me.QuotationMarkTextBox.Name = "QuotationMarkTextBox"
        Me.QuotationMarkTextBox.Size = New System.Drawing.Size(184, 20)
        Me.QuotationMarkTextBox.TabIndex = 8
        Me.QuotationMarkTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'RefreshButton
        '
        Me.RefreshButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RefreshButton.Image = Global.AccControlsWinForms.My.Resources.Resources.Button_Reload_icon_24p
        Me.RefreshButton.Location = New System.Drawing.Point(841, 55)
        Me.RefreshButton.Name = "RefreshButton"
        Me.RefreshButton.Size = New System.Drawing.Size(38, 32)
        Me.RefreshButton.TabIndex = 9
        Me.RefreshButton.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToOrderColumns = True
        Me.DataGridView1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomCenter
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DataGridView1.Location = New System.Drawing.Point(0, 90)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersWidth = 20
        Me.DataGridView1.Size = New System.Drawing.Size(903, 455)
        Me.DataGridView1.TabIndex = 1
        '
        'AddColumnButton
        '
        Me.AddColumnButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AddColumnButton.Image = Global.AccControlsWinForms.My.Resources.Resources.add_column_24x24
        Me.AddColumnButton.Location = New System.Drawing.Point(651, 55)
        Me.AddColumnButton.Name = "AddColumnButton"
        Me.AddColumnButton.Size = New System.Drawing.Size(38, 32)
        Me.AddColumnButton.TabIndex = 10
        Me.AddColumnButton.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.DiscardButton)
        Me.Panel1.Controls.Add(Me.ImportButton)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 545)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(903, 35)
        Me.Panel1.TabIndex = 2
        '
        'ImportButton
        '
        Me.ImportButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ImportButton.Location = New System.Drawing.Point(714, 6)
        Me.ImportButton.Name = "ImportButton"
        Me.ImportButton.Size = New System.Drawing.Size(75, 23)
        Me.ImportButton.TabIndex = 0
        Me.ImportButton.Text = "Ok"
        Me.ImportButton.UseVisualStyleBackColor = True
        '
        'DiscardButton
        '
        Me.DiscardButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DiscardButton.Location = New System.Drawing.Point(804, 6)
        Me.DiscardButton.Name = "DiscardButton"
        Me.DiscardButton.Size = New System.Drawing.Size(75, 23)
        Me.DiscardButton.TabIndex = 1
        Me.DiscardButton.Text = "Atšaukti"
        Me.DiscardButton.UseVisualStyleBackColor = True
        '
        'F_DataImport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(903, 580)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "F_DataImport"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "Data Import"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents FieldsTabDelimitedCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents QuotationMarkTextBox As System.Windows.Forms.TextBox
    Friend WithEvents QuotationMarksUsedCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents LinesDelimiterTextBox As System.Windows.Forms.TextBox
    Friend WithEvents FieldDelimiterTextBox As System.Windows.Forms.TextBox
    Friend WithEvents LinesCrLfDelimitedCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents SourceTextBox As System.Windows.Forms.TextBox
    Friend WithEvents RefreshButton As System.Windows.Forms.Button
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents AddColumnButton As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents DiscardButton As System.Windows.Forms.Button
    Friend WithEvents ImportButton As System.Windows.Forms.Button
End Class
