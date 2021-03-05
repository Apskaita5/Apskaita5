<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F_ImportInvoices
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
        Me.components = New System.ComponentModel.Container()
        Dim DefaultAccountLabel As System.Windows.Forms.Label
        Dim DefaultContentLabel As System.Windows.Forms.Label
        Dim DefaultLineContentLabel As System.Windows.Forms.Label
        Dim DefaultMeasureUnitLabel As System.Windows.Forms.Label
        Dim DefaultVatAccountLabel As System.Windows.Forms.Label
        Dim InvoiceIdPrefixLabel As System.Windows.Forms.Label
        Dim AdapterLabel As System.Windows.Forms.Label
        Dim DefaultVatSchemaLabel As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(F_ImportInvoices))
        Me.InvoiceImportOptionsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DefaultAccountAccListComboBox = New AccControlsWinForms.AccListComboBox()
        Me.DefaultContentTextBox = New System.Windows.Forms.TextBox()
        Me.DefaultLineContentTextBox = New System.Windows.Forms.TextBox()
        Me.DefaultMeasureUnitTextBox = New System.Windows.Forms.TextBox()
        Me.DefaultVatAccountAccListComboBox = New AccControlsWinForms.AccListComboBox()
        Me.ForInvoicesMadeCheckBox = New System.Windows.Forms.CheckBox()
        Me.InvoiceIdPrefixTextBox = New System.Windows.Forms.TextBox()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.DefaultVatSchemaAccListComboBox = New AccControlsWinForms.AccListComboBox()
        Me.AdapterComboBox = New System.Windows.Forms.ComboBox()
        Me.ProgressFiller1 = New AccControlsWinForms.ProgressFiller()
        Me.ErrorWarnInfoProvider1 = New AccControlsWinForms.ErrorWarnInfoProvider(Me.components)
        DefaultAccountLabel = New System.Windows.Forms.Label()
        DefaultContentLabel = New System.Windows.Forms.Label()
        DefaultLineContentLabel = New System.Windows.Forms.Label()
        DefaultMeasureUnitLabel = New System.Windows.Forms.Label()
        DefaultVatAccountLabel = New System.Windows.Forms.Label()
        InvoiceIdPrefixLabel = New System.Windows.Forms.Label()
        AdapterLabel = New System.Windows.Forms.Label()
        DefaultVatSchemaLabel = New System.Windows.Forms.Label()
        CType(Me.InvoiceImportOptionsBindingSource,System.ComponentModel.ISupportInitialize).BeginInit
        Me.TableLayoutPanel1.SuspendLayout
        CType(Me.ErrorWarnInfoProvider1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'DefaultAccountLabel
        '
        DefaultAccountLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        DefaultAccountLabel.AutoSize = true
        DefaultAccountLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        DefaultAccountLabel.Location = New System.Drawing.Point(284, 30)
        DefaultAccountLabel.Name = "DefaultAccountLabel"
        DefaultAccountLabel.Size = New System.Drawing.Size(64, 26)
        DefaultAccountLabel.TabIndex = 1
        DefaultAccountLabel.Text = "Sąskaita:"
        DefaultAccountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'DefaultContentLabel
        '
        DefaultContentLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        DefaultContentLabel.AutoSize = true
        DefaultContentLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        DefaultContentLabel.Location = New System.Drawing.Point(3, 82)
        DefaultContentLabel.Name = "DefaultContentLabel"
        DefaultContentLabel.Size = New System.Drawing.Size(90, 26)
        DefaultContentLabel.TabIndex = 3
        DefaultContentLabel.Text = "Turinys:"
        DefaultContentLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'DefaultLineContentLabel
        '
        DefaultLineContentLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        DefaultLineContentLabel.AutoSize = true
        DefaultLineContentLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        DefaultLineContentLabel.Location = New System.Drawing.Point(3, 108)
        DefaultLineContentLabel.Name = "DefaultLineContentLabel"
        DefaultLineContentLabel.Size = New System.Drawing.Size(90, 26)
        DefaultLineContentLabel.TabIndex = 5
        DefaultLineContentLabel.Text = "Eilutės turinys:"
        DefaultLineContentLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'DefaultMeasureUnitLabel
        '
        DefaultMeasureUnitLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        DefaultMeasureUnitLabel.AutoSize = true
        DefaultMeasureUnitLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        DefaultMeasureUnitLabel.Location = New System.Drawing.Point(539, 56)
        DefaultMeasureUnitLabel.Name = "DefaultMeasureUnitLabel"
        DefaultMeasureUnitLabel.Size = New System.Drawing.Size(90, 26)
        DefaultMeasureUnitLabel.TabIndex = 7
        DefaultMeasureUnitLabel.Text = "Mato Vnt.:"
        DefaultMeasureUnitLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'DefaultVatAccountLabel
        '
        DefaultVatAccountLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        DefaultVatAccountLabel.AutoSize = true
        DefaultVatAccountLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        DefaultVatAccountLabel.Location = New System.Drawing.Point(539, 30)
        DefaultVatAccountLabel.Name = "DefaultVatAccountLabel"
        DefaultVatAccountLabel.Size = New System.Drawing.Size(90, 26)
        DefaultVatAccountLabel.TabIndex = 9
        DefaultVatAccountLabel.Text = "PVM Sąskaita:"
        DefaultVatAccountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'InvoiceIdPrefixLabel
        '
        InvoiceIdPrefixLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        InvoiceIdPrefixLabel.AutoSize = true
        InvoiceIdPrefixLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        InvoiceIdPrefixLabel.Location = New System.Drawing.Point(3, 30)
        InvoiceIdPrefixLabel.Name = "InvoiceIdPrefixLabel"
        InvoiceIdPrefixLabel.Size = New System.Drawing.Size(90, 26)
        InvoiceIdPrefixLabel.TabIndex = 13
        InvoiceIdPrefixLabel.Text = "Sistemos ID:"
        InvoiceIdPrefixLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'AdapterLabel
        '
        AdapterLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        AdapterLabel.AutoSize = true
        AdapterLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        AdapterLabel.Location = New System.Drawing.Point(284, 0)
        AdapterLabel.Name = "AdapterLabel"
        AdapterLabel.Size = New System.Drawing.Size(64, 30)
        AdapterLabel.TabIndex = 15
        AdapterLabel.Text = "Adapteris:"
        AdapterLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'DefaultVatSchemaLabel
        '
        DefaultVatSchemaLabel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        DefaultVatSchemaLabel.AutoSize = true
        DefaultVatSchemaLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        DefaultVatSchemaLabel.Location = New System.Drawing.Point(3, 56)
        DefaultVatSchemaLabel.Name = "DefaultVatSchemaLabel"
        DefaultVatSchemaLabel.Size = New System.Drawing.Size(90, 26)
        DefaultVatSchemaLabel.TabIndex = 15
        DefaultVatSchemaLabel.Text = "PVM Schema:"
        DefaultVatSchemaLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'InvoiceImportOptionsBindingSource
        '
        Me.InvoiceImportOptionsBindingSource.DataSource = GetType(ApskaitaObjects.Extensibility.InvoiceImportOptions)
        '
        'DefaultAccountAccListComboBox
        '
        Me.DefaultAccountAccListComboBox.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.InvoiceImportOptionsBindingSource, "DefaultAccount", true))
        Me.DefaultAccountAccListComboBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DefaultAccountAccListComboBox.EmptyValueString = ""
        Me.DefaultAccountAccListComboBox.Location = New System.Drawing.Point(354, 33)
        Me.DefaultAccountAccListComboBox.Name = "DefaultAccountAccListComboBox"
        Me.DefaultAccountAccListComboBox.Size = New System.Drawing.Size(159, 20)
        Me.DefaultAccountAccListComboBox.TabIndex = 2
        '
        'DefaultContentTextBox
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.DefaultContentTextBox, 7)
        Me.DefaultContentTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.InvoiceImportOptionsBindingSource, "DefaultContent", true))
        Me.DefaultContentTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DefaultContentTextBox.Location = New System.Drawing.Point(99, 85)
        Me.DefaultContentTextBox.Name = "DefaultContentTextBox"
        Me.DefaultContentTextBox.Size = New System.Drawing.Size(695, 20)
        Me.DefaultContentTextBox.TabIndex = 4
        '
        'DefaultLineContentTextBox
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.DefaultLineContentTextBox, 7)
        Me.DefaultLineContentTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.InvoiceImportOptionsBindingSource, "DefaultLineContent", true))
        Me.DefaultLineContentTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DefaultLineContentTextBox.Location = New System.Drawing.Point(99, 111)
        Me.DefaultLineContentTextBox.Name = "DefaultLineContentTextBox"
        Me.DefaultLineContentTextBox.Size = New System.Drawing.Size(695, 20)
        Me.DefaultLineContentTextBox.TabIndex = 6
        '
        'DefaultMeasureUnitTextBox
        '
        Me.DefaultMeasureUnitTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.InvoiceImportOptionsBindingSource, "DefaultMeasureUnit", true))
        Me.DefaultMeasureUnitTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DefaultMeasureUnitTextBox.Location = New System.Drawing.Point(635, 59)
        Me.DefaultMeasureUnitTextBox.Name = "DefaultMeasureUnitTextBox"
        Me.DefaultMeasureUnitTextBox.Size = New System.Drawing.Size(159, 20)
        Me.DefaultMeasureUnitTextBox.TabIndex = 8
        '
        'DefaultVatAccountAccListComboBox
        '
        Me.DefaultVatAccountAccListComboBox.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.InvoiceImportOptionsBindingSource, "DefaultVatAccount", true))
        Me.DefaultVatAccountAccListComboBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DefaultVatAccountAccListComboBox.EmptyValueString = ""
        Me.DefaultVatAccountAccListComboBox.Location = New System.Drawing.Point(635, 33)
        Me.DefaultVatAccountAccListComboBox.Name = "DefaultVatAccountAccListComboBox"
        Me.DefaultVatAccountAccListComboBox.Size = New System.Drawing.Size(159, 20)
        Me.DefaultVatAccountAccListComboBox.TabIndex = 10
        '
        'ForInvoicesMadeCheckBox
        '
        Me.ForInvoicesMadeCheckBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.ForInvoicesMadeCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("CheckState", Me.InvoiceImportOptionsBindingSource, "ForInvoicesMade", true))
        Me.ForInvoicesMadeCheckBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ForInvoicesMadeCheckBox.Location = New System.Drawing.Point(99, 3)
        Me.ForInvoicesMadeCheckBox.Name = "ForInvoicesMadeCheckBox"
        Me.ForInvoicesMadeCheckBox.Size = New System.Drawing.Size(159, 24)
        Me.ForInvoicesMadeCheckBox.TabIndex = 12
        Me.ForInvoicesMadeCheckBox.Text = "Išrašytos Sąskaitos"
        Me.ForInvoicesMadeCheckBox.UseVisualStyleBackColor = true
        '
        'InvoiceIdPrefixTextBox
        '
        Me.InvoiceIdPrefixTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.InvoiceImportOptionsBindingSource, "InvoiceIdPrefix", true))
        Me.InvoiceIdPrefixTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.InvoiceIdPrefixTextBox.Location = New System.Drawing.Point(99, 33)
        Me.InvoiceIdPrefixTextBox.Name = "InvoiceIdPrefixTextBox"
        Me.InvoiceIdPrefixTextBox.Size = New System.Drawing.Size(159, 20)
        Me.InvoiceIdPrefixTextBox.TabIndex = 14
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 9
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 22!))
        Me.TableLayoutPanel1.Controls.Add(Me.Button1, 7, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.DefaultVatSchemaAccListComboBox, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.DefaultLineContentTextBox, 1, 4)
        Me.TableLayoutPanel1.Controls.Add(DefaultLineContentLabel, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.DefaultContentTextBox, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(DefaultContentLabel, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(DefaultVatSchemaLabel, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.AdapterComboBox, 4, 0)
        Me.TableLayoutPanel1.Controls.Add(AdapterLabel, 3, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.ForInvoicesMadeCheckBox, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(InvoiceIdPrefixLabel, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.InvoiceIdPrefixTextBox, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(DefaultAccountLabel, 3, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.DefaultAccountAccListComboBox, 4, 1)
        Me.TableLayoutPanel1.Controls.Add(DefaultMeasureUnitLabel, 6, 2)
        Me.TableLayoutPanel1.Controls.Add(DefaultVatAccountLabel, 6, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.DefaultVatAccountAccListComboBox, 7, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.DefaultMeasureUnitTextBox, 7, 2)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 6
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(819, 179)
        Me.TableLayoutPanel1.TabIndex = 15
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.Button1.AutoSize = true
        Me.Button1.Image = Global.AccDataBindingsWinForms.My.Resources.Resources.folder_open_icon_24p
        Me.Button1.Location = New System.Drawing.Point(755, 137)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(39, 30)
        Me.Button1.TabIndex = 16
        Me.Button1.UseVisualStyleBackColor = true
        '
        'DefaultVatSchemaAccListComboBox
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.DefaultVatSchemaAccListComboBox, 4)
        Me.DefaultVatSchemaAccListComboBox.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.InvoiceImportOptionsBindingSource, "DefaultVatSchema", true))
        Me.DefaultVatSchemaAccListComboBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DefaultVatSchemaAccListComboBox.EmptyValueString = ""
        Me.DefaultVatSchemaAccListComboBox.Location = New System.Drawing.Point(99, 59)
        Me.DefaultVatSchemaAccListComboBox.Name = "DefaultVatSchemaAccListComboBox"
        Me.DefaultVatSchemaAccListComboBox.Size = New System.Drawing.Size(414, 20)
        Me.DefaultVatSchemaAccListComboBox.TabIndex = 16
        '
        'AdapterComboBox
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.AdapterComboBox, 4)
        Me.AdapterComboBox.DataBindings.Add(New System.Windows.Forms.Binding("SelectedItem", Me.InvoiceImportOptionsBindingSource, "Adapter", true))
        Me.AdapterComboBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AdapterComboBox.FormattingEnabled = true
        Me.AdapterComboBox.Location = New System.Drawing.Point(354, 3)
        Me.AdapterComboBox.Name = "AdapterComboBox"
        Me.AdapterComboBox.Size = New System.Drawing.Size(440, 21)
        Me.AdapterComboBox.TabIndex = 16
        '
        'ProgressFiller1
        '
        Me.ProgressFiller1.Location = New System.Drawing.Point(409, 160)
        Me.ProgressFiller1.Name = "ProgressFiller1"
        Me.ProgressFiller1.Size = New System.Drawing.Size(220, 238)
        Me.ProgressFiller1.TabIndex = 16
        Me.ProgressFiller1.Visible = false
        '
        'ErrorWarnInfoProvider1
        '
        Me.ErrorWarnInfoProvider1.ContainerControl = Me
        Me.ErrorWarnInfoProvider1.DataSource = Me.InvoiceImportOptionsBindingSource
        '
        'F_ImportInvoices
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(819, 179)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.ProgressFiller1)
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.Name = "F_ImportInvoices"
        Me.Text = "Sąskaitų faktūrų importas"
        CType(Me.InvoiceImportOptionsBindingSource,System.ComponentModel.ISupportInitialize).EndInit
        Me.TableLayoutPanel1.ResumeLayout(false)
        Me.TableLayoutPanel1.PerformLayout
        CType(Me.ErrorWarnInfoProvider1,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)

End Sub

    Friend WithEvents InvoiceImportOptionsBindingSource As BindingSource
    Friend WithEvents DefaultAccountAccListComboBox As AccControlsWinForms.AccListComboBox
    Friend WithEvents DefaultContentTextBox As TextBox
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents DefaultVatSchemaAccListComboBox As AccControlsWinForms.AccListComboBox
    Friend WithEvents DefaultLineContentTextBox As TextBox
    Friend WithEvents AdapterComboBox As ComboBox
    Friend WithEvents ForInvoicesMadeCheckBox As CheckBox
    Friend WithEvents InvoiceIdPrefixTextBox As TextBox
    Friend WithEvents DefaultVatAccountAccListComboBox As AccControlsWinForms.AccListComboBox
    Friend WithEvents DefaultMeasureUnitTextBox As TextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents ProgressFiller1 As AccControlsWinForms.ProgressFiller
    Friend WithEvents ErrorWarnInfoProvider1 As AccControlsWinForms.ErrorWarnInfoProvider
End Class
