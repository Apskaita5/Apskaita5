<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Friend Class F_VatDeclarationSchema
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
        Dim IDLabel As System.Windows.Forms.Label
        Dim InsertDateLabel As System.Windows.Forms.Label
        Dim UpdateDateLabel As System.Windows.Forms.Label
        Dim TradedTypeHumanReadableLabel As System.Windows.Forms.Label
        Dim VatRateLabel As System.Windows.Forms.Label
        Dim NameLabel As System.Windows.Forms.Label
        Dim DescriptionLabel As System.Windows.Forms.Label
        Dim ExternalCodeLabel As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(F_VatDeclarationSchema))
        Me.TaxCodeLabel = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.VatRateIsNullCheckBox = New System.Windows.Forms.CheckBox()
        Me.VatDeclarationSchemaBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.VatRateComboBox = New System.Windows.Forms.ComboBox()
        Me.ExternalCodeTextBox = New System.Windows.Forms.TextBox()
        Me.IsObsoleteCheckBox = New System.Windows.Forms.CheckBox()
        Me.UpdateDateTextBox = New System.Windows.Forms.TextBox()
        Me.IDTextBox = New System.Windows.Forms.TextBox()
        Me.InsertDateTextBox = New System.Windows.Forms.TextBox()
        Me.TradedTypeHumanReadableComboBox = New System.Windows.Forms.ComboBox()
        Me.DescriptionTextBox = New System.Windows.Forms.TextBox()
        Me.NameTextBox = New System.Windows.Forms.TextBox()
        Me.TaxCodeAccListComboBox = New AccControlsWinForms.AccListComboBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.ICancelButton = New System.Windows.Forms.Button()
        Me.IOkButton = New System.Windows.Forms.Button()
        Me.IApplyButton = New System.Windows.Forms.Button()
        Me.DeclarationEntriesBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DeclarationEntriesDataListView = New BrightIdeasSoftware.DataListView()
        Me.OlvColumn2 = CType(New BrightIdeasSoftware.OLVColumn(),BrightIdeasSoftware.OLVColumn)
        Me.OlvColumn1 = CType(New BrightIdeasSoftware.OLVColumn(),BrightIdeasSoftware.OLVColumn)
        Me.OlvColumn4 = CType(New BrightIdeasSoftware.OLVColumn(),BrightIdeasSoftware.OLVColumn)
        Me.OlvColumn3 = CType(New BrightIdeasSoftware.OLVColumn(),BrightIdeasSoftware.OLVColumn)
        Me.OlvColumn6 = CType(New BrightIdeasSoftware.OLVColumn(),BrightIdeasSoftware.OLVColumn)
        Me.ProgressFiller1 = New AccControlsWinForms.ProgressFiller()
        Me.ErrorWarnInfoProvider1 = New AccControlsWinForms.ErrorWarnInfoProvider(Me.components)
        Me.VatIsVirtualCheckBox = New System.Windows.Forms.CheckBox()
        IDLabel = New System.Windows.Forms.Label()
        InsertDateLabel = New System.Windows.Forms.Label()
        UpdateDateLabel = New System.Windows.Forms.Label()
        TradedTypeHumanReadableLabel = New System.Windows.Forms.Label()
        VatRateLabel = New System.Windows.Forms.Label()
        NameLabel = New System.Windows.Forms.Label()
        DescriptionLabel = New System.Windows.Forms.Label()
        ExternalCodeLabel = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1.SuspendLayout
        CType(Me.VatDeclarationSchemaBindingSource,System.ComponentModel.ISupportInitialize).BeginInit
        Me.Panel2.SuspendLayout
        CType(Me.DeclarationEntriesBindingSource,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.DeclarationEntriesDataListView,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.ErrorWarnInfoProvider1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'IDLabel
        '
        IDLabel.AutoSize = true
        IDLabel.Dock = System.Windows.Forms.DockStyle.Fill
        IDLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        IDLabel.Location = New System.Drawing.Point(3, 0)
        IDLabel.Name = "IDLabel"
        IDLabel.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        IDLabel.Size = New System.Drawing.Size(102, 26)
        IDLabel.TabIndex = 2
        IDLabel.Text = "ID:"
        IDLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'InsertDateLabel
        '
        InsertDateLabel.AutoSize = true
        InsertDateLabel.Dock = System.Windows.Forms.DockStyle.Fill
        InsertDateLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        InsertDateLabel.Location = New System.Drawing.Point(359, 0)
        InsertDateLabel.Name = "InsertDateLabel"
        InsertDateLabel.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        InsertDateLabel.Size = New System.Drawing.Size(71, 26)
        InsertDateLabel.TabIndex = 3
        InsertDateLabel.Text = "Įtraukta:"
        InsertDateLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'UpdateDateLabel
        '
        UpdateDateLabel.AutoSize = true
        UpdateDateLabel.Dock = System.Windows.Forms.DockStyle.Fill
        UpdateDateLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        UpdateDateLabel.Location = New System.Drawing.Point(627, 0)
        UpdateDateLabel.Name = "UpdateDateLabel"
        UpdateDateLabel.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        UpdateDateLabel.Size = New System.Drawing.Size(78, 26)
        UpdateDateLabel.TabIndex = 5
        UpdateDateLabel.Text = "Pakeista:"
        UpdateDateLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'TradedTypeHumanReadableLabel
        '
        TradedTypeHumanReadableLabel.AutoSize = true
        TradedTypeHumanReadableLabel.Dock = System.Windows.Forms.DockStyle.Fill
        TradedTypeHumanReadableLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        TradedTypeHumanReadableLabel.Location = New System.Drawing.Point(3, 26)
        TradedTypeHumanReadableLabel.Name = "TradedTypeHumanReadableLabel"
        TradedTypeHumanReadableLabel.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        TradedTypeHumanReadableLabel.Size = New System.Drawing.Size(102, 30)
        TradedTypeHumanReadableLabel.TabIndex = 1
        TradedTypeHumanReadableLabel.Text = "Apyvartos Tipas:"
        TradedTypeHumanReadableLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'VatRateLabel
        '
        VatRateLabel.AutoSize = true
        VatRateLabel.Dock = System.Windows.Forms.DockStyle.Fill
        VatRateLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        VatRateLabel.Location = New System.Drawing.Point(359, 26)
        VatRateLabel.Name = "VatRateLabel"
        VatRateLabel.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        VatRateLabel.Size = New System.Drawing.Size(71, 30)
        VatRateLabel.TabIndex = 3
        VatRateLabel.Text = "Tarifas (%):"
        VatRateLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'NameLabel
        '
        NameLabel.AutoSize = true
        NameLabel.Dock = System.Windows.Forms.DockStyle.Fill
        NameLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        NameLabel.Location = New System.Drawing.Point(3, 82)
        NameLabel.Name = "NameLabel"
        NameLabel.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        NameLabel.Size = New System.Drawing.Size(102, 26)
        NameLabel.TabIndex = 1
        NameLabel.Text = "Pavadinimas:"
        NameLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'DescriptionLabel
        '
        DescriptionLabel.AutoSize = true
        DescriptionLabel.Dock = System.Windows.Forms.DockStyle.Fill
        DescriptionLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        DescriptionLabel.Location = New System.Drawing.Point(3, 108)
        DescriptionLabel.Name = "DescriptionLabel"
        DescriptionLabel.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        DescriptionLabel.Size = New System.Drawing.Size(102, 108)
        DescriptionLabel.TabIndex = 1
        DescriptionLabel.Text = "Aprašymas:"
        DescriptionLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'ExternalCodeLabel
        '
        ExternalCodeLabel.AutoSize = true
        ExternalCodeLabel.Dock = System.Windows.Forms.DockStyle.Fill
        ExternalCodeLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        ExternalCodeLabel.Location = New System.Drawing.Point(359, 56)
        ExternalCodeLabel.Name = "ExternalCodeLabel"
        ExternalCodeLabel.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        ExternalCodeLabel.Size = New System.Drawing.Size(71, 26)
        ExternalCodeLabel.TabIndex = 6
        ExternalCodeLabel.Text = "Kodas:"
        '
        'TaxCodeLabel
        '
        Me.TaxCodeLabel.AutoSize = true
        Me.TaxCodeLabel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TaxCodeLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.TaxCodeLabel.Location = New System.Drawing.Point(3, 56)
        Me.TaxCodeLabel.Name = "TaxCodeLabel"
        Me.TaxCodeLabel.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.TaxCodeLabel.Size = New System.Drawing.Size(102, 26)
        Me.TaxCodeLabel.TabIndex = 5
        Me.TaxCodeLabel.Text = "PVM Kodas:"
        Me.TaxCodeLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 9
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 21!))
        Me.TableLayoutPanel1.Controls.Add(Me.VatRateIsNullCheckBox, 6, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.VatRateComboBox, 4, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.ExternalCodeTextBox, 4, 2)
        Me.TableLayoutPanel1.Controls.Add(ExternalCodeLabel, 3, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.IsObsoleteCheckBox, 6, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.UpdateDateTextBox, 7, 0)
        Me.TableLayoutPanel1.Controls.Add(UpdateDateLabel, 6, 0)
        Me.TableLayoutPanel1.Controls.Add(VatRateLabel, 3, 1)
        Me.TableLayoutPanel1.Controls.Add(IDLabel, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.IDTextBox, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(TradedTypeHumanReadableLabel, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.InsertDateTextBox, 4, 0)
        Me.TableLayoutPanel1.Controls.Add(InsertDateLabel, 3, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TradedTypeHumanReadableComboBox, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.DescriptionTextBox, 1, 4)
        Me.TableLayoutPanel1.Controls.Add(DescriptionLabel, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(NameLabel, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.NameTextBox, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.TaxCodeLabel, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.TaxCodeAccListComboBox, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.VatIsVirtualCheckBox, 7, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 5
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(901, 216)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'VatRateIsNullCheckBox
        '
        Me.VatRateIsNullCheckBox.AutoSize = true
        Me.VatRateIsNullCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("CheckState", Me.VatDeclarationSchemaBindingSource, "VatRateIsNull", true))
        Me.VatRateIsNullCheckBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.VatRateIsNullCheckBox.Location = New System.Drawing.Point(627, 29)
        Me.VatRateIsNullCheckBox.Name = "VatRateIsNullCheckBox"
        Me.VatRateIsNullCheckBox.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.VatRateIsNullCheckBox.Size = New System.Drawing.Size(78, 20)
        Me.VatRateIsNullCheckBox.TabIndex = 8
        Me.VatRateIsNullCheckBox.Text = "Be Tarifo"
        Me.VatRateIsNullCheckBox.UseVisualStyleBackColor = true
        '
        'VatDeclarationSchemaBindingSource
        '
        Me.VatDeclarationSchemaBindingSource.DataSource = GetType(ApskaitaObjects.Documents.VatDeclarationSchema)
        '
        'VatRateComboBox
        '
        Me.VatRateComboBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.VatDeclarationSchemaBindingSource, "VatRate", true))
        Me.VatRateComboBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.VatRateComboBox.FormattingEnabled = true
        Me.VatRateComboBox.Location = New System.Drawing.Point(436, 29)
        Me.VatRateComboBox.Name = "VatRateComboBox"
        Me.VatRateComboBox.Size = New System.Drawing.Size(165, 21)
        Me.VatRateComboBox.TabIndex = 7
        '
        'ExternalCodeTextBox
        '
        Me.ExternalCodeTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.VatDeclarationSchemaBindingSource, "ExternalCode", true))
        Me.ExternalCodeTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ExternalCodeTextBox.Location = New System.Drawing.Point(436, 59)
        Me.ExternalCodeTextBox.Name = "ExternalCodeTextBox"
        Me.ExternalCodeTextBox.Size = New System.Drawing.Size(165, 20)
        Me.ExternalCodeTextBox.TabIndex = 7
        '
        'IsObsoleteCheckBox
        '
        Me.IsObsoleteCheckBox.AutoSize = true
        Me.TableLayoutPanel1.SetColumnSpan(Me.IsObsoleteCheckBox, 2)
        Me.IsObsoleteCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("CheckState", Me.VatDeclarationSchemaBindingSource, "IsObsolete", true))
        Me.IsObsoleteCheckBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.IsObsoleteCheckBox.Location = New System.Drawing.Point(627, 59)
        Me.IsObsoleteCheckBox.Name = "IsObsoleteCheckBox"
        Me.IsObsoleteCheckBox.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.IsObsoleteCheckBox.Size = New System.Drawing.Size(117, 20)
        Me.IsObsoleteCheckBox.TabIndex = 4
        Me.IsObsoleteCheckBox.Text = "Nebenaudojama"
        Me.IsObsoleteCheckBox.UseVisualStyleBackColor = true
        '
        'UpdateDateTextBox
        '
        Me.UpdateDateTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.VatDeclarationSchemaBindingSource, "UpdateDate", true))
        Me.UpdateDateTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UpdateDateTextBox.Location = New System.Drawing.Point(711, 3)
        Me.UpdateDateTextBox.Name = "UpdateDateTextBox"
        Me.UpdateDateTextBox.ReadOnly = true
        Me.UpdateDateTextBox.Size = New System.Drawing.Size(165, 20)
        Me.UpdateDateTextBox.TabIndex = 6
        Me.UpdateDateTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'IDTextBox
        '
        Me.IDTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.VatDeclarationSchemaBindingSource, "ID", true))
        Me.IDTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.IDTextBox.Location = New System.Drawing.Point(111, 3)
        Me.IDTextBox.Name = "IDTextBox"
        Me.IDTextBox.ReadOnly = true
        Me.IDTextBox.Size = New System.Drawing.Size(222, 20)
        Me.IDTextBox.TabIndex = 3
        Me.IDTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'InsertDateTextBox
        '
        Me.InsertDateTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.VatDeclarationSchemaBindingSource, "InsertDate", true))
        Me.InsertDateTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.InsertDateTextBox.Location = New System.Drawing.Point(436, 3)
        Me.InsertDateTextBox.Name = "InsertDateTextBox"
        Me.InsertDateTextBox.ReadOnly = true
        Me.InsertDateTextBox.Size = New System.Drawing.Size(165, 20)
        Me.InsertDateTextBox.TabIndex = 4
        Me.InsertDateTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TradedTypeHumanReadableComboBox
        '
        Me.TradedTypeHumanReadableComboBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.VatDeclarationSchemaBindingSource, "TradedTypeHumanReadable", true))
        Me.TradedTypeHumanReadableComboBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TradedTypeHumanReadableComboBox.FormattingEnabled = true
        Me.TradedTypeHumanReadableComboBox.Location = New System.Drawing.Point(111, 29)
        Me.TradedTypeHumanReadableComboBox.Name = "TradedTypeHumanReadableComboBox"
        Me.TradedTypeHumanReadableComboBox.Size = New System.Drawing.Size(222, 21)
        Me.TradedTypeHumanReadableComboBox.TabIndex = 2
        '
        'DescriptionTextBox
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.DescriptionTextBox, 7)
        Me.DescriptionTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.VatDeclarationSchemaBindingSource, "Description", true))
        Me.DescriptionTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DescriptionTextBox.Location = New System.Drawing.Point(111, 111)
        Me.DescriptionTextBox.MaxLength = 1000
        Me.DescriptionTextBox.Multiline = true
        Me.DescriptionTextBox.Name = "DescriptionTextBox"
        Me.DescriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.DescriptionTextBox.Size = New System.Drawing.Size(765, 102)
        Me.DescriptionTextBox.TabIndex = 2
        '
        'NameTextBox
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.NameTextBox, 7)
        Me.NameTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.VatDeclarationSchemaBindingSource, "Name", true))
        Me.NameTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.NameTextBox.Location = New System.Drawing.Point(111, 85)
        Me.NameTextBox.Name = "NameTextBox"
        Me.NameTextBox.Size = New System.Drawing.Size(765, 20)
        Me.NameTextBox.TabIndex = 2
        '
        'TaxCodeAccListComboBox
        '
        Me.TaxCodeAccListComboBox.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.VatDeclarationSchemaBindingSource, "TaxCode", true))
        Me.TaxCodeAccListComboBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TaxCodeAccListComboBox.EmptyValueString = ""
        Me.TaxCodeAccListComboBox.Location = New System.Drawing.Point(111, 59)
        Me.TaxCodeAccListComboBox.Name = "TaxCodeAccListComboBox"
        Me.TaxCodeAccListComboBox.Size = New System.Drawing.Size(222, 20)
        Me.TaxCodeAccListComboBox.TabIndex = 4
        '
        'Panel2
        '
        Me.Panel2.AutoSize = true
        Me.Panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel2.Controls.Add(Me.ICancelButton)
        Me.Panel2.Controls.Add(Me.IOkButton)
        Me.Panel2.Controls.Add(Me.IApplyButton)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 449)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(901, 32)
        Me.Panel2.TabIndex = 4
        '
        'ICancelButton
        '
        Me.ICancelButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.ICancelButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.ICancelButton.Location = New System.Drawing.Point(800, 6)
        Me.ICancelButton.Name = "ICancelButton"
        Me.ICancelButton.Size = New System.Drawing.Size(89, 23)
        Me.ICancelButton.TabIndex = 3
        Me.ICancelButton.Text = "Atšaukti"
        Me.ICancelButton.UseVisualStyleBackColor = true
        '
        'IOkButton
        '
        Me.IOkButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.IOkButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.IOkButton.Location = New System.Drawing.Point(594, 6)
        Me.IOkButton.Name = "IOkButton"
        Me.IOkButton.Size = New System.Drawing.Size(89, 23)
        Me.IOkButton.TabIndex = 1
        Me.IOkButton.Text = "Ok"
        Me.IOkButton.UseVisualStyleBackColor = true
        '
        'IApplyButton
        '
        Me.IApplyButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.IApplyButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.IApplyButton.Location = New System.Drawing.Point(698, 6)
        Me.IApplyButton.Name = "IApplyButton"
        Me.IApplyButton.Size = New System.Drawing.Size(89, 23)
        Me.IApplyButton.TabIndex = 2
        Me.IApplyButton.Text = "Išsaugoti"
        Me.IApplyButton.UseVisualStyleBackColor = true
        '
        'DeclarationEntriesBindingSource
        '
        Me.DeclarationEntriesBindingSource.DataMember = "DeclarationEntries"
        Me.DeclarationEntriesBindingSource.DataSource = Me.VatDeclarationSchemaBindingSource
        '
        'DeclarationEntriesDataListView
        '
        Me.DeclarationEntriesDataListView.AllColumns.Add(Me.OlvColumn2)
        Me.DeclarationEntriesDataListView.AllColumns.Add(Me.OlvColumn1)
        Me.DeclarationEntriesDataListView.AllColumns.Add(Me.OlvColumn4)
        Me.DeclarationEntriesDataListView.AllColumns.Add(Me.OlvColumn3)
        Me.DeclarationEntriesDataListView.AllColumns.Add(Me.OlvColumn6)
        Me.DeclarationEntriesDataListView.AllowColumnReorder = true
        Me.DeclarationEntriesDataListView.AutoGenerateColumns = false
        Me.DeclarationEntriesDataListView.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.SingleClickAlways
        Me.DeclarationEntriesDataListView.CellEditEnterChangesRows = true
        Me.DeclarationEntriesDataListView.CellEditTabChangesRows = true
        Me.DeclarationEntriesDataListView.CellEditUseWholeCell = false
        Me.DeclarationEntriesDataListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.OlvColumn2, Me.OlvColumn4, Me.OlvColumn3, Me.OlvColumn6})
        Me.DeclarationEntriesDataListView.Cursor = System.Windows.Forms.Cursors.Default
        Me.DeclarationEntriesDataListView.DataSource = Me.DeclarationEntriesBindingSource
        Me.DeclarationEntriesDataListView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DeclarationEntriesDataListView.FullRowSelect = true
        Me.DeclarationEntriesDataListView.HasCollapsibleGroups = false
        Me.DeclarationEntriesDataListView.HeaderWordWrap = true
        Me.DeclarationEntriesDataListView.HideSelection = false
        Me.DeclarationEntriesDataListView.HighlightBackgroundColor = System.Drawing.Color.PaleGreen
        Me.DeclarationEntriesDataListView.HighlightForegroundColor = System.Drawing.Color.Black
        Me.DeclarationEntriesDataListView.IncludeColumnHeadersInCopy = true
        Me.DeclarationEntriesDataListView.Location = New System.Drawing.Point(0, 216)
        Me.DeclarationEntriesDataListView.Name = "DeclarationEntriesDataListView"
        Me.DeclarationEntriesDataListView.RenderNonEditableCheckboxesAsDisabled = true
        Me.DeclarationEntriesDataListView.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.Submenu
        Me.DeclarationEntriesDataListView.SelectedBackColor = System.Drawing.Color.PaleGreen
        Me.DeclarationEntriesDataListView.SelectedForeColor = System.Drawing.Color.Black
        Me.DeclarationEntriesDataListView.ShowCommandMenuOnRightClick = true
        Me.DeclarationEntriesDataListView.ShowGroups = false
        Me.DeclarationEntriesDataListView.ShowImagesOnSubItems = true
        Me.DeclarationEntriesDataListView.ShowItemCountOnGroups = true
        Me.DeclarationEntriesDataListView.ShowItemToolTips = true
        Me.DeclarationEntriesDataListView.Size = New System.Drawing.Size(901, 233)
        Me.DeclarationEntriesDataListView.TabIndex = 5
        Me.DeclarationEntriesDataListView.UnfocusedSelectedBackColor = System.Drawing.Color.PaleGreen
        Me.DeclarationEntriesDataListView.UnfocusedSelectedForeColor = System.Drawing.Color.Black
        Me.DeclarationEntriesDataListView.UseCellFormatEvents = true
        Me.DeclarationEntriesDataListView.UseCompatibleStateImageBehavior = false
        Me.DeclarationEntriesDataListView.UseFilterIndicator = true
        Me.DeclarationEntriesDataListView.UseFiltering = true
        Me.DeclarationEntriesDataListView.UseHotItem = true
        Me.DeclarationEntriesDataListView.UseNotifyPropertyChanged = true
        Me.DeclarationEntriesDataListView.View = System.Windows.Forms.View.Details
        '
        'OlvColumn2
        '
        Me.OlvColumn2.AspectName = "FieldCode"
        Me.OlvColumn2.CellEditUseWholeCell = true
        Me.OlvColumn2.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.OlvColumn2.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn2.IsVisible = false
        Me.OlvColumn2.Text = "Laukelio Kodas"
        Me.OlvColumn2.Width = 91
        '
        'OlvColumn1
        '
        Me.OlvColumn1.AspectName = "ID"
        Me.OlvColumn1.CellEditUseWholeCell = true
        Me.OlvColumn1.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.OlvColumn1.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn1.IsEditable = false
        Me.OlvColumn1.IsVisible = false
        Me.OlvColumn1.Text = "ID"
        Me.OlvColumn1.Width = 50
        '
        'OlvColumn4
        '
        Me.OlvColumn4.AspectName = "TypeHumanReadable"
        Me.OlvColumn4.CellEditUseWholeCell = true
        Me.OlvColumn4.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.OlvColumn4.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn4.Text = "Veiksmas"
        Me.OlvColumn4.Width = 132
        '
        'OlvColumn3
        '
        Me.OlvColumn3.AspectName = "InclusionPercentage"
        Me.OlvColumn3.CellEditUseWholeCell = true
        Me.OlvColumn3.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.OlvColumn3.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn3.Text = "Įtraukimo %"
        Me.OlvColumn3.Width = 74
        '
        'OlvColumn6
        '
        Me.OlvColumn6.AspectName = "Remarks"
        Me.OlvColumn6.CellEditUseWholeCell = true
        Me.OlvColumn6.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.OlvColumn6.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn6.Text = "Pastabos"
        Me.OlvColumn6.Width = 598
        '
        'ProgressFiller1
        '
        Me.ProgressFiller1.Location = New System.Drawing.Point(154, 279)
        Me.ProgressFiller1.Name = "ProgressFiller1"
        Me.ProgressFiller1.Size = New System.Drawing.Size(164, 64)
        Me.ProgressFiller1.TabIndex = 6
        Me.ProgressFiller1.Visible = false
        '
        'ErrorWarnInfoProvider1
        '
        Me.ErrorWarnInfoProvider1.ContainerControl = Me
        Me.ErrorWarnInfoProvider1.DataSource = Me.VatDeclarationSchemaBindingSource
        '
        'VatIsVirtualCheckBox
        '
        Me.VatIsVirtualCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("CheckState", Me.VatDeclarationSchemaBindingSource, "VatIsVirtual", true))
        Me.VatIsVirtualCheckBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.VatIsVirtualCheckBox.Location = New System.Drawing.Point(711, 29)
        Me.VatIsVirtualCheckBox.Name = "VatIsVirtualCheckBox"
        Me.VatIsVirtualCheckBox.Size = New System.Drawing.Size(104, 24)
        Me.VatIsVirtualCheckBox.TabIndex = 21
        Me.VatIsVirtualCheckBox.Text = "Netiesioginis"
        Me.VatIsVirtualCheckBox.UseVisualStyleBackColor = true
        '
        'F_VatDeclarationSchema
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(901, 481)
        Me.Controls.Add(Me.DeclarationEntriesDataListView)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.ProgressFiller1)
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.Name = "F_VatDeclarationSchema"
        Me.ShowInTaskbar = false
        Me.Text = "PVM deklaravimo schema"
        Me.TableLayoutPanel1.ResumeLayout(false)
        Me.TableLayoutPanel1.PerformLayout
        CType(Me.VatDeclarationSchemaBindingSource,System.ComponentModel.ISupportInitialize).EndInit
        Me.Panel2.ResumeLayout(false)
        CType(Me.DeclarationEntriesBindingSource,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.DeclarationEntriesDataListView,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.ErrorWarnInfoProvider1,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents NameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents VatDeclarationSchemaBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents IsObsoleteCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents UpdateDateTextBox As System.Windows.Forms.TextBox
    Friend WithEvents IDTextBox As System.Windows.Forms.TextBox
    Friend WithEvents InsertDateTextBox As System.Windows.Forms.TextBox
    Friend WithEvents TradedTypeHumanReadableComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents DescriptionTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents ICancelButton As System.Windows.Forms.Button
    Friend WithEvents IOkButton As System.Windows.Forms.Button
    Friend WithEvents IApplyButton As System.Windows.Forms.Button
    Friend WithEvents ErrorWarnInfoProvider1 As AccControlsWinForms.ErrorWarnInfoProvider
    Friend WithEvents DeclarationEntriesBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DeclarationEntriesDataListView As BrightIdeasSoftware.DataListView
    Friend WithEvents OlvColumn1 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn2 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn3 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn4 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn6 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents ProgressFiller1 As AccControlsWinForms.ProgressFiller
    Friend WithEvents VatRateComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents TaxCodeAccListComboBox As AccControlsWinForms.AccListComboBox
    Friend WithEvents TaxCodeLabel As System.Windows.Forms.Label
    Friend WithEvents ExternalCodeTextBox As System.Windows.Forms.TextBox
    Friend WithEvents VatRateIsNullCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents VatIsVirtualCheckBox As CheckBox
End Class
