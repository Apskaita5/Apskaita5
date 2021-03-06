﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F_DatabaseStructureEditor
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
        Me.components = New System.ComponentModel.Container()
        Dim DescriptionLabel As System.Windows.Forms.Label
        Dim CharsetNameLabel As System.Windows.Forms.Label
        Dim Label1 As System.Windows.Forms.Label
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(F_DatabaseStructureEditor))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.LoadGaugeButton = New System.Windows.Forms.Button()
        Me.GetCreateSqlButton = New System.Windows.Forms.Button()
        Me.CharsetNameTextBox = New System.Windows.Forms.TextBox()
        Me.DatabaseStructureBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.SaveFileAsButton = New System.Windows.Forms.Button()
        Me.SaveFileButton = New System.Windows.Forms.Button()
        Me.DescriptionTextBox = New System.Windows.Forms.TextBox()
        Me.OpenFileButton = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.LocalFilePasswordTextBox = New System.Windows.Forms.TextBox()
        Me.LoadDbSchemaButton = New System.Windows.Forms.Button()
        Me.DatabaseListComboBox = New System.Windows.Forms.ComboBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.DatabaseStructureSourceLabel1 = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TablesTabPage = New System.Windows.Forms.TabPage()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.TableListDataGridView = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CharsetName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EngineName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TableListBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.FieldListDataGridView = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn11 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TypeDataGridViewComboBoxColumn = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.DataGridViewTextBoxColumn10 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn9 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewCheckBoxColumn1 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.DataGridViewCheckBoxColumn2 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.DataGridViewCheckBoxColumn3 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.DataGridViewCheckBoxColumn4 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Unique = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.Indexed = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.IndexName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FieldListBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.StoredProceduresTabPage = New System.Windows.Forms.TabPage()
        Me.StoredProcedureListDataGridView = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.StoredProcedureListBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DataGridViewComboBoxColumn1 = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.CopyDocumentationToClipboardButton = New System.Windows.Forms.Button()
        DescriptionLabel = New System.Windows.Forms.Label()
        CharsetNameLabel = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        CType(Me.DatabaseStructureBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TablesTabPage.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.TableListDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TableListBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FieldListDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FieldListBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.StoredProceduresTabPage.SuspendLayout()
        CType(Me.StoredProcedureListDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.StoredProcedureListBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DescriptionLabel
        '
        DescriptionLabel.AutoSize = True
        DescriptionLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DescriptionLabel.Location = New System.Drawing.Point(22, 76)
        DescriptionLabel.Name = "DescriptionLabel"
        DescriptionLabel.Size = New System.Drawing.Size(71, 13)
        DescriptionLabel.TabIndex = 4
        DescriptionLabel.Text = "Aprašymas:"
        '
        'CharsetNameLabel
        '
        CharsetNameLabel.AutoSize = True
        CharsetNameLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        CharsetNameLabel.Location = New System.Drawing.Point(3, 124)
        CharsetNameLabel.Name = "CharsetNameLabel"
        CharsetNameLabel.Size = New System.Drawing.Size(90, 13)
        CharsetNameLabel.TabIndex = 6
        CharsetNameLabel.Text = "Charset Name:"
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label1.Location = New System.Drawing.Point(14, 39)
        Label1.Name = "Label1"
        Label1.Size = New System.Drawing.Size(76, 13)
        Label1.TabIndex = 5
        Label1.Text = "Slaptažodis:"
        '
        'Panel1
        '
        Me.Panel1.AutoSize = True
        Me.Panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel1.Controls.Add(Me.CopyDocumentationToClipboardButton)
        Me.Panel1.Controls.Add(Me.LoadGaugeButton)
        Me.Panel1.Controls.Add(Me.GetCreateSqlButton)
        Me.Panel1.Controls.Add(CharsetNameLabel)
        Me.Panel1.Controls.Add(Me.CharsetNameTextBox)
        Me.Panel1.Controls.Add(DescriptionLabel)
        Me.Panel1.Controls.Add(Me.SaveFileAsButton)
        Me.Panel1.Controls.Add(Me.SaveFileButton)
        Me.Panel1.Controls.Add(Me.DescriptionTextBox)
        Me.Panel1.Controls.Add(Me.OpenFileButton)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel1.Size = New System.Drawing.Size(887, 147)
        Me.Panel1.TabIndex = 0
        '
        'LoadGaugeButton
        '
        Me.LoadGaugeButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LoadGaugeButton.AutoSize = True
        Me.LoadGaugeButton.Image = Global.ApskaitaWUI.My.Resources.Resources.database_settings_icon_24
        Me.LoadGaugeButton.Location = New System.Drawing.Point(620, 12)
        Me.LoadGaugeButton.Name = "LoadGaugeButton"
        Me.LoadGaugeButton.Size = New System.Drawing.Size(30, 30)
        Me.LoadGaugeButton.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.LoadGaugeButton, "Load Current Database Template")
        Me.LoadGaugeButton.UseVisualStyleBackColor = True
        '
        'GetCreateSqlButton
        '
        Me.GetCreateSqlButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GetCreateSqlButton.Image = Global.ApskaitaWUI.My.Resources.Resources.sqlquery_16x16
        Me.GetCreateSqlButton.Location = New System.Drawing.Point(784, 12)
        Me.GetCreateSqlButton.Name = "GetCreateSqlButton"
        Me.GetCreateSqlButton.Size = New System.Drawing.Size(30, 30)
        Me.GetCreateSqlButton.TabIndex = 8
        Me.ToolTip1.SetToolTip(Me.GetCreateSqlButton, "Get Create Database SQL Script")
        Me.GetCreateSqlButton.UseVisualStyleBackColor = True
        '
        'CharsetNameTextBox
        '
        Me.CharsetNameTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CharsetNameTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DatabaseStructureBindingSource, "CharsetName", True))
        Me.CharsetNameTextBox.Location = New System.Drawing.Point(93, 121)
        Me.CharsetNameTextBox.Name = "CharsetNameTextBox"
        Me.CharsetNameTextBox.Size = New System.Drawing.Size(770, 20)
        Me.CharsetNameTextBox.TabIndex = 7
        '
        'DatabaseStructureBindingSource
        '
        Me.DatabaseStructureBindingSource.DataSource = GetType(AccDataAccessLayer.DatabaseAccess.DatabaseStructure.DatabaseStructure)
        '
        'SaveFileAsButton
        '
        Me.SaveFileAsButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SaveFileAsButton.AutoSize = True
        Me.SaveFileAsButton.Image = Global.ApskaitaWUI.My.Resources.Resources.filesaveas_24x24
        Me.SaveFileAsButton.Location = New System.Drawing.Point(738, 12)
        Me.SaveFileAsButton.Name = "SaveFileAsButton"
        Me.SaveFileAsButton.Size = New System.Drawing.Size(30, 30)
        Me.SaveFileAsButton.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.SaveFileAsButton, "Save As File With Database Structure Template")
        Me.SaveFileAsButton.UseVisualStyleBackColor = True
        '
        'SaveFileButton
        '
        Me.SaveFileButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SaveFileButton.AutoSize = True
        Me.SaveFileButton.Image = Global.ApskaitaWUI.My.Resources.Resources.Actions_document_save_icon_24p
        Me.SaveFileButton.Location = New System.Drawing.Point(702, 12)
        Me.SaveFileButton.Name = "SaveFileButton"
        Me.SaveFileButton.Size = New System.Drawing.Size(30, 30)
        Me.SaveFileButton.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.SaveFileButton, "Save File With Database Structure Template")
        Me.SaveFileButton.UseVisualStyleBackColor = True
        '
        'DescriptionTextBox
        '
        Me.DescriptionTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DescriptionTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DatabaseStructureBindingSource, "Description", True))
        Me.DescriptionTextBox.Location = New System.Drawing.Point(93, 73)
        Me.DescriptionTextBox.Multiline = True
        Me.DescriptionTextBox.Name = "DescriptionTextBox"
        Me.DescriptionTextBox.Size = New System.Drawing.Size(770, 42)
        Me.DescriptionTextBox.TabIndex = 5
        '
        'OpenFileButton
        '
        Me.OpenFileButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OpenFileButton.AutoSize = True
        Me.OpenFileButton.Image = Global.ApskaitaWUI.My.Resources.Resources.folder_open_icon_24p
        Me.OpenFileButton.Location = New System.Drawing.Point(666, 12)
        Me.OpenFileButton.Name = "OpenFileButton"
        Me.OpenFileButton.Size = New System.Drawing.Size(30, 30)
        Me.OpenFileButton.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.OpenFileButton, "Open File With Database Structure Template")
        Me.OpenFileButton.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel2.BackColor = System.Drawing.SystemColors.Info
        Me.Panel2.Controls.Add(Label1)
        Me.Panel2.Controls.Add(Me.LocalFilePasswordTextBox)
        Me.Panel2.Controls.Add(Me.LoadDbSchemaButton)
        Me.Panel2.Controls.Add(Me.DatabaseListComboBox)
        Me.Panel2.Location = New System.Drawing.Point(3, 3)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(611, 64)
        Me.Panel2.TabIndex = 1
        '
        'LocalFilePasswordTextBox
        '
        Me.LocalFilePasswordTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LocalFilePasswordTextBox.Location = New System.Drawing.Point(90, 36)
        Me.LocalFilePasswordTextBox.Name = "LocalFilePasswordTextBox"
        Me.LocalFilePasswordTextBox.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.LocalFilePasswordTextBox.Size = New System.Drawing.Size(450, 20)
        Me.LocalFilePasswordTextBox.TabIndex = 2
        Me.LocalFilePasswordTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'LoadDbSchemaButton
        '
        Me.LoadDbSchemaButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.LoadDbSchemaButton.AutoSize = True
        Me.LoadDbSchemaButton.Image = Global.ApskaitaWUI.My.Resources.Resources.database_search_icon_24
        Me.LoadDbSchemaButton.Location = New System.Drawing.Point(569, 9)
        Me.LoadDbSchemaButton.Name = "LoadDbSchemaButton"
        Me.LoadDbSchemaButton.Size = New System.Drawing.Size(30, 30)
        Me.LoadDbSchemaButton.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.LoadDbSchemaButton, "Inspect Selected Database Structure")
        Me.LoadDbSchemaButton.UseVisualStyleBackColor = True
        '
        'DatabaseListComboBox
        '
        Me.DatabaseListComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DatabaseListComboBox.FormattingEnabled = True
        Me.DatabaseListComboBox.Location = New System.Drawing.Point(3, 9)
        Me.DatabaseListComboBox.Name = "DatabaseListComboBox"
        Me.DatabaseListComboBox.Size = New System.Drawing.Size(537, 21)
        Me.DatabaseListComboBox.TabIndex = 0
        Me.ToolTip1.SetToolTip(Me.DatabaseListComboBox, "Accessible Databases")
        '
        'Panel3
        '
        Me.Panel3.AutoSize = True
        Me.Panel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel3.Controls.Add(Me.DatabaseStructureSourceLabel1)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel3.Location = New System.Drawing.Point(0, 522)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Padding = New System.Windows.Forms.Padding(0, 3, 0, 5)
        Me.Panel3.Size = New System.Drawing.Size(887, 26)
        Me.Panel3.TabIndex = 1
        '
        'DatabaseStructureSourceLabel1
        '
        Me.DatabaseStructureSourceLabel1.AutoSize = True
        Me.DatabaseStructureSourceLabel1.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.DatabaseStructureBindingSource, "DatabaseStructureSource", True))
        Me.DatabaseStructureSourceLabel1.Location = New System.Drawing.Point(15, 8)
        Me.DatabaseStructureSourceLabel1.Name = "DatabaseStructureSourceLabel1"
        Me.DatabaseStructureSourceLabel1.Size = New System.Drawing.Size(0, 13)
        Me.DatabaseStructureSourceLabel1.TabIndex = 1
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TablesTabPage)
        Me.TabControl1.Controls.Add(Me.StoredProceduresTabPage)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 147)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(887, 375)
        Me.TabControl1.TabIndex = 2
        '
        'TablesTabPage
        '
        Me.TablesTabPage.Controls.Add(Me.SplitContainer1)
        Me.TablesTabPage.Location = New System.Drawing.Point(4, 22)
        Me.TablesTabPage.Name = "TablesTabPage"
        Me.TablesTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.TablesTabPage.Size = New System.Drawing.Size(879, 349)
        Me.TablesTabPage.TabIndex = 0
        Me.TablesTabPage.Text = "Tables"
        Me.TablesTabPage.UseVisualStyleBackColor = True
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.AutoScroll = True
        Me.SplitContainer1.Panel1.Controls.Add(Me.TableListDataGridView)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.AutoScroll = True
        Me.SplitContainer1.Panel2.Controls.Add(Me.FieldListDataGridView)
        Me.SplitContainer1.Size = New System.Drawing.Size(873, 343)
        Me.SplitContainer1.SplitterDistance = 288
        Me.SplitContainer1.TabIndex = 0
        '
        'TableListDataGridView
        '
        Me.TableListDataGridView.AutoGenerateColumns = False
        Me.TableListDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn5, Me.CharsetName, Me.EngineName, Me.DataGridViewTextBoxColumn6})
        Me.TableListDataGridView.DataSource = Me.TableListBindingSource
        Me.TableListDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableListDataGridView.Location = New System.Drawing.Point(0, 0)
        Me.TableListDataGridView.Name = "TableListDataGridView"
        Me.TableListDataGridView.Size = New System.Drawing.Size(288, 343)
        Me.TableListDataGridView.TabIndex = 0
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.DataPropertyName = "Name"
        Me.DataGridViewTextBoxColumn5.HeaderText = "Name"
        Me.DataGridViewTextBoxColumn5.MaxInputLength = 255
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        '
        'CharsetName
        '
        Me.CharsetName.DataPropertyName = "CharsetName"
        Me.CharsetName.HeaderText = "Charset"
        Me.CharsetName.Name = "CharsetName"
        '
        'EngineName
        '
        Me.EngineName.DataPropertyName = "EngineName"
        Me.EngineName.HeaderText = "Engine"
        Me.EngineName.Name = "EngineName"
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn6.DataPropertyName = "Description"
        Me.DataGridViewTextBoxColumn6.HeaderText = "Description"
        Me.DataGridViewTextBoxColumn6.MaxInputLength = 2000
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        '
        'TableListBindingSource
        '
        Me.TableListBindingSource.AllowNew = True
        Me.TableListBindingSource.DataMember = "TableList"
        Me.TableListBindingSource.DataSource = Me.DatabaseStructureBindingSource
        '
        'FieldListDataGridView
        '
        Me.FieldListDataGridView.AutoGenerateColumns = False
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.FieldListDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.FieldListDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn7, Me.DataGridViewTextBoxColumn11, Me.TypeDataGridViewComboBoxColumn, Me.DataGridViewTextBoxColumn10, Me.DataGridViewTextBoxColumn9, Me.DataGridViewCheckBoxColumn1, Me.DataGridViewCheckBoxColumn2, Me.DataGridViewCheckBoxColumn3, Me.DataGridViewCheckBoxColumn4, Me.Unique, Me.Indexed, Me.IndexName})
        Me.FieldListDataGridView.DataSource = Me.FieldListBindingSource
        Me.FieldListDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FieldListDataGridView.Location = New System.Drawing.Point(0, 0)
        Me.FieldListDataGridView.Name = "FieldListDataGridView"
        Me.FieldListDataGridView.Size = New System.Drawing.Size(581, 343)
        Me.FieldListDataGridView.TabIndex = 0
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.DataPropertyName = "Name"
        Me.DataGridViewTextBoxColumn7.HeaderText = "Name"
        Me.DataGridViewTextBoxColumn7.MaxInputLength = 255
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        '
        'DataGridViewTextBoxColumn11
        '
        Me.DataGridViewTextBoxColumn11.DataPropertyName = "Description"
        Me.DataGridViewTextBoxColumn11.HeaderText = "Description"
        Me.DataGridViewTextBoxColumn11.MaxInputLength = 2000
        Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
        '
        'TypeDataGridViewComboBoxColumn
        '
        Me.TypeDataGridViewComboBoxColumn.DataPropertyName = "Type"
        Me.TypeDataGridViewComboBoxColumn.HeaderText = "Tipas"
        Me.TypeDataGridViewComboBoxColumn.Name = "TypeDataGridViewComboBoxColumn"
        Me.TypeDataGridViewComboBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.TypeDataGridViewComboBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'DataGridViewTextBoxColumn10
        '
        Me.DataGridViewTextBoxColumn10.DataPropertyName = "EnumValues"
        Me.DataGridViewTextBoxColumn10.HeaderText = "EnumValues"
        Me.DataGridViewTextBoxColumn10.MaxInputLength = 1000
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.DataPropertyName = "Length"
        Me.DataGridViewTextBoxColumn9.HeaderText = "Length"
        Me.DataGridViewTextBoxColumn9.MaxInputLength = 10
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        '
        'DataGridViewCheckBoxColumn1
        '
        Me.DataGridViewCheckBoxColumn1.DataPropertyName = "NotNull"
        Me.DataGridViewCheckBoxColumn1.HeaderText = "NotNull"
        Me.DataGridViewCheckBoxColumn1.Name = "DataGridViewCheckBoxColumn1"
        '
        'DataGridViewCheckBoxColumn2
        '
        Me.DataGridViewCheckBoxColumn2.DataPropertyName = "Autoincrement"
        Me.DataGridViewCheckBoxColumn2.HeaderText = "Autoincrement"
        Me.DataGridViewCheckBoxColumn2.Name = "DataGridViewCheckBoxColumn2"
        '
        'DataGridViewCheckBoxColumn3
        '
        Me.DataGridViewCheckBoxColumn3.DataPropertyName = "PrimaryKey"
        Me.DataGridViewCheckBoxColumn3.HeaderText = "PrimaryKey"
        Me.DataGridViewCheckBoxColumn3.Name = "DataGridViewCheckBoxColumn3"
        '
        'DataGridViewCheckBoxColumn4
        '
        Me.DataGridViewCheckBoxColumn4.DataPropertyName = "Unsigned"
        Me.DataGridViewCheckBoxColumn4.HeaderText = "Unsigned"
        Me.DataGridViewCheckBoxColumn4.Name = "DataGridViewCheckBoxColumn4"
        '
        'Unique
        '
        Me.Unique.DataPropertyName = "Unique"
        Me.Unique.HeaderText = "Unique"
        Me.Unique.Name = "Unique"
        '
        'Indexed
        '
        Me.Indexed.DataPropertyName = "Indexed"
        Me.Indexed.HeaderText = "Indexed"
        Me.Indexed.Name = "Indexed"
        Me.Indexed.ReadOnly = True
        '
        'IndexName
        '
        Me.IndexName.DataPropertyName = "IndexName"
        Me.IndexName.HeaderText = "Index name"
        Me.IndexName.MaxInputLength = 100
        Me.IndexName.Name = "IndexName"
        '
        'FieldListBindingSource
        '
        Me.FieldListBindingSource.AllowNew = True
        Me.FieldListBindingSource.DataMember = "FieldList"
        Me.FieldListBindingSource.DataSource = Me.TableListBindingSource
        '
        'StoredProceduresTabPage
        '
        Me.StoredProceduresTabPage.Controls.Add(Me.StoredProcedureListDataGridView)
        Me.StoredProceduresTabPage.Location = New System.Drawing.Point(4, 22)
        Me.StoredProceduresTabPage.Name = "StoredProceduresTabPage"
        Me.StoredProceduresTabPage.Padding = New System.Windows.Forms.Padding(3)
        Me.StoredProceduresTabPage.Size = New System.Drawing.Size(909, 349)
        Me.StoredProceduresTabPage.TabIndex = 1
        Me.StoredProceduresTabPage.Text = "Stored Procedures"
        Me.StoredProceduresTabPage.UseVisualStyleBackColor = True
        '
        'StoredProcedureListDataGridView
        '
        Me.StoredProcedureListDataGridView.AutoGenerateColumns = False
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.StoredProcedureListDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.StoredProcedureListDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn4, Me.DataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn3})
        Me.StoredProcedureListDataGridView.DataSource = Me.StoredProcedureListBindingSource
        Me.StoredProcedureListDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.StoredProcedureListDataGridView.Location = New System.Drawing.Point(3, 3)
        Me.StoredProcedureListDataGridView.Name = "StoredProcedureListDataGridView"
        Me.StoredProcedureListDataGridView.Size = New System.Drawing.Size(903, 343)
        Me.StoredProcedureListDataGridView.TabIndex = 0
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "Name"
        Me.DataGridViewTextBoxColumn1.HeaderText = "Name"
        Me.DataGridViewTextBoxColumn1.MaxInputLength = 255
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "Description"
        Me.DataGridViewTextBoxColumn4.HeaderText = "Description"
        Me.DataGridViewTextBoxColumn4.MaxInputLength = 2000
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "SourceCode"
        Me.DataGridViewTextBoxColumn2.HeaderText = "Source Code"
        Me.DataGridViewTextBoxColumn2.MaxInputLength = 5000
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "SourceCodeComparable"
        Me.DataGridViewTextBoxColumn3.HeaderText = "SourceCodeComparable"
        Me.DataGridViewTextBoxColumn3.MaxInputLength = 5000
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        '
        'StoredProcedureListBindingSource
        '
        Me.StoredProcedureListBindingSource.AllowNew = True
        Me.StoredProcedureListBindingSource.DataMember = "StoredProcedureList"
        Me.StoredProcedureListBindingSource.DataSource = Me.DatabaseStructureBindingSource
        '
        'DataGridViewComboBoxColumn1
        '
        Me.DataGridViewComboBoxColumn1.DataPropertyName = "Type"
        Me.DataGridViewComboBoxColumn1.HeaderText = "Tipas"
        Me.DataGridViewComboBoxColumn1.Name = "DataGridViewComboBoxColumn1"
        Me.DataGridViewComboBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewComboBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        '
        'CopyDocumentationToClipboardButton
        '
        Me.CopyDocumentationToClipboardButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CopyDocumentationToClipboardButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CopyDocumentationToClipboardButton.Location = New System.Drawing.Point(820, 12)
        Me.CopyDocumentationToClipboardButton.Name = "CopyDocumentationToClipboardButton"
        Me.CopyDocumentationToClipboardButton.Size = New System.Drawing.Size(43, 30)
        Me.CopyDocumentationToClipboardButton.TabIndex = 9
        Me.CopyDocumentationToClipboardButton.Text = "Doc"
        Me.ToolTip1.SetToolTip(Me.CopyDocumentationToClipboardButton, "Copy database documentation to clipboard.")
        Me.CopyDocumentationToClipboardButton.UseVisualStyleBackColor = True
        '
        'F_DatabaseStructureEditor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(887, 548)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "F_DatabaseStructureEditor"
        Me.ShowInTaskbar = False
        Me.Text = "Duomenų bazės struktūros redaktorius"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.DatabaseStructureBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TablesTabPage.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.TableListDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TableListBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FieldListDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FieldListBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.StoredProceduresTabPage.ResumeLayout(False)
        CType(Me.StoredProcedureListDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.StoredProcedureListBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents SaveFileAsButton As System.Windows.Forms.Button
    Friend WithEvents SaveFileButton As System.Windows.Forms.Button
    Friend WithEvents OpenFileButton As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents LoadDbSchemaButton As System.Windows.Forms.Button
    Friend WithEvents DatabaseListComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents DatabaseStructureSourceLabel1 As System.Windows.Forms.Label
    Friend WithEvents DatabaseStructureBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DescriptionTextBox As System.Windows.Forms.TextBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TablesTabPage As System.Windows.Forms.TabPage
    Friend WithEvents StoredProceduresTabPage As System.Windows.Forms.TabPage
    Friend WithEvents StoredProcedureListDataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents StoredProcedureListBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents TableListDataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents TableListBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents FieldListDataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents FieldListBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents LoadGaugeButton As System.Windows.Forms.Button
    Friend WithEvents CharsetNameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CharsetName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents EngineName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewComboBoxColumn1 As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn11 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TypeDataGridViewComboBoxColumn As System.Windows.Forms.DataGridViewComboBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewCheckBoxColumn1 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents DataGridViewCheckBoxColumn2 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents DataGridViewCheckBoxColumn3 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents DataGridViewCheckBoxColumn4 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Unique As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Indexed As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents IndexName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents GetCreateSqlButton As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents LocalFilePasswordTextBox As System.Windows.Forms.TextBox
    Friend WithEvents CopyDocumentationToClipboardButton As Button
End Class
