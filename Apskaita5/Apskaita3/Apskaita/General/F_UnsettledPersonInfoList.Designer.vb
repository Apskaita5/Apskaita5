<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F_UnsettledPersonInfoList
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
        Me.components = New System.ComponentModel.Container
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(F_UnsettledPersonInfoList))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.RefreshButton = New System.Windows.Forms.Button
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel
        Me.PersonGroupComboBox = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.MarginOfErrorAccTextBox = New AccControls.AccTextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel
        Me.ForSuppliersRadioButton = New System.Windows.Forms.RadioButton
        Me.Label4 = New System.Windows.Forms.Label
        Me.ForBuyersRadioButton = New System.Windows.Forms.RadioButton
        Me.AccountAccGridComboBox = New AccControls.AccGridComboBox
        Me.AsOfDateDateTimePicker = New System.Windows.Forms.DateTimePicker
        Me.Label2 = New System.Windows.Forms.Label
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.UnsettledPersonInfoListDataGridView = New System.Windows.Forms.DataGridView
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn9 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.UnsettledPersonInfoListBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ItemsDataGridView = New System.Windows.Forms.DataGridView
        Me.ItemsSortedBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DataGridViewTextBoxColumn10 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn14 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn11 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn12 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn15 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn16 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Debt = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Panel1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.UnsettledPersonInfoListDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.UnsettledPersonInfoListBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ItemsDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ItemsSortedBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.AutoSize = True
        Me.Panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel1.Controls.Add(Me.RefreshButton)
        Me.Panel1.Controls.Add(Me.TableLayoutPanel1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(874, 61)
        Me.Panel1.TabIndex = 0
        '
        'RefreshButton
        '
        Me.RefreshButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RefreshButton.Image = Global.ApskaitaWUI.My.Resources.Resources.Button_Reload_icon_24p
        Me.RefreshButton.Location = New System.Drawing.Point(831, 18)
        Me.RefreshButton.Name = "RefreshButton"
        Me.RefreshButton.Size = New System.Drawing.Size(33, 34)
        Me.RefreshButton.TabIndex = 1
        Me.RefreshButton.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel3, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 0, 1)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(825, 58)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 5
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 21.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.PersonGroupComboBox, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.Label3, 2, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.MarginOfErrorAccTextBox, 3, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(51, 30)
        Me.TableLayoutPanel3.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 1
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(774, 30)
        Me.TableLayoutPanel3.TabIndex = 2
        '
        'PersonGroupComboBox
        '
        Me.PersonGroupComboBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PersonGroupComboBox.FormattingEnabled = True
        Me.PersonGroupComboBox.Location = New System.Drawing.Point(2, 1)
        Me.PersonGroupComboBox.Margin = New System.Windows.Forms.Padding(2, 1, 2, 1)
        Me.PersonGroupComboBox.Name = "PersonGroupComboBox"
        Me.PersonGroupComboBox.Size = New System.Drawing.Size(462, 21)
        Me.PersonGroupComboBox.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label3.Location = New System.Drawing.Point(489, 3)
        Me.Label3.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(60, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Paklaida:"
        '
        'MarginOfErrorAccTextBox
        '
        Me.MarginOfErrorAccTextBox.DecimalValue = 5
        Me.MarginOfErrorAccTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MarginOfErrorAccTextBox.Location = New System.Drawing.Point(554, 1)
        Me.MarginOfErrorAccTextBox.Margin = New System.Windows.Forms.Padding(2, 1, 2, 1)
        Me.MarginOfErrorAccTextBox.Name = "MarginOfErrorAccTextBox"
        Me.MarginOfErrorAccTextBox.NegativeValue = False
        Me.MarginOfErrorAccTextBox.Size = New System.Drawing.Size(196, 20)
        Me.MarginOfErrorAccTextBox.TabIndex = 6
        Me.MarginOfErrorAccTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label1.Location = New System.Drawing.Point(10, 6)
        Me.Label1.Margin = New System.Windows.Forms.Padding(3, 6, 3, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(38, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Data:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 8
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 21.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.ForSuppliersRadioButton, 6, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.Label4, 2, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.ForBuyersRadioButton, 5, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.AccountAccGridComboBox, 3, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.AsOfDateDateTimePicker, 0, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(51, 0)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(774, 30)
        Me.TableLayoutPanel2.TabIndex = 1
        '
        'ForSuppliersRadioButton
        '
        Me.ForSuppliersRadioButton.AutoSize = True
        Me.ForSuppliersRadioButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.ForSuppliersRadioButton.Location = New System.Drawing.Point(680, 3)
        Me.ForSuppliersRadioButton.Name = "ForSuppliersRadioButton"
        Me.ForSuppliersRadioButton.Size = New System.Drawing.Size(70, 17)
        Me.ForSuppliersRadioButton.TabIndex = 4
        Me.ForSuppliersRadioButton.Text = "Tiekėjai"
        Me.ForSuppliersRadioButton.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label4.Location = New System.Drawing.Point(272, 3)
        Me.Label4.Margin = New System.Windows.Forms.Padding(3, 3, 3, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(60, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Sąskaita:"
        '
        'ForBuyersRadioButton
        '
        Me.ForBuyersRadioButton.AutoSize = True
        Me.ForBuyersRadioButton.Checked = True
        Me.ForBuyersRadioButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.ForBuyersRadioButton.Location = New System.Drawing.Point(607, 3)
        Me.ForBuyersRadioButton.Name = "ForBuyersRadioButton"
        Me.ForBuyersRadioButton.Size = New System.Drawing.Size(67, 17)
        Me.ForBuyersRadioButton.TabIndex = 3
        Me.ForBuyersRadioButton.TabStop = True
        Me.ForBuyersRadioButton.Text = "Pirkėjai"
        Me.ForBuyersRadioButton.UseVisualStyleBackColor = True
        '
        'AccountAccGridComboBox
        '
        Me.AccountAccGridComboBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AccountAccGridComboBox.EmptyValueString = ""
        Me.AccountAccGridComboBox.FilterPropertyName = ""
        Me.AccountAccGridComboBox.FormattingEnabled = True
        Me.AccountAccGridComboBox.InstantBinding = True
        Me.AccountAccGridComboBox.Location = New System.Drawing.Point(337, 1)
        Me.AccountAccGridComboBox.Margin = New System.Windows.Forms.Padding(2, 1, 2, 1)
        Me.AccountAccGridComboBox.Name = "AccountAccGridComboBox"
        Me.AccountAccGridComboBox.Size = New System.Drawing.Size(245, 21)
        Me.AccountAccGridComboBox.TabIndex = 7
        '
        'AsOfDateDateTimePicker
        '
        Me.AsOfDateDateTimePicker.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AsOfDateDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.AsOfDateDateTimePicker.Location = New System.Drawing.Point(2, 1)
        Me.AsOfDateDateTimePicker.Margin = New System.Windows.Forms.Padding(2, 1, 2, 1)
        Me.AsOfDateDateTimePicker.Name = "AsOfDateDateTimePicker"
        Me.AsOfDateDateTimePicker.Size = New System.Drawing.Size(245, 20)
        Me.AsOfDateDateTimePicker.TabIndex = 8
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label2.Location = New System.Drawing.Point(3, 36)
        Me.Label2.Margin = New System.Windows.Forms.Padding(3, 6, 3, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(45, 13)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Grupė:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 61)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.AutoScroll = True
        Me.SplitContainer1.Panel1.Controls.Add(Me.UnsettledPersonInfoListDataGridView)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.AutoScroll = True
        Me.SplitContainer1.Panel2.Controls.Add(Me.ItemsDataGridView)
        Me.SplitContainer1.Size = New System.Drawing.Size(874, 444)
        Me.SplitContainer1.SplitterDistance = 252
        Me.SplitContainer1.TabIndex = 1
        '
        'UnsettledPersonInfoListDataGridView
        '
        Me.UnsettledPersonInfoListDataGridView.AllowUserToAddRows = False
        Me.UnsettledPersonInfoListDataGridView.AllowUserToDeleteRows = False
        Me.UnsettledPersonInfoListDataGridView.AllowUserToOrderColumns = True
        Me.UnsettledPersonInfoListDataGridView.AutoGenerateColumns = False
        Me.UnsettledPersonInfoListDataGridView.CausesValidation = False
        Me.UnsettledPersonInfoListDataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomCenter
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.UnsettledPersonInfoListDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.UnsettledPersonInfoListDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.UnsettledPersonInfoListDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn3, Me.DataGridViewTextBoxColumn5, Me.DataGridViewTextBoxColumn4, Me.DataGridViewTextBoxColumn6, Me.DataGridViewTextBoxColumn7, Me.DataGridViewTextBoxColumn8, Me.DataGridViewTextBoxColumn9})
        Me.UnsettledPersonInfoListDataGridView.DataSource = Me.UnsettledPersonInfoListBindingSource
        Me.UnsettledPersonInfoListDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UnsettledPersonInfoListDataGridView.Location = New System.Drawing.Point(0, 0)
        Me.UnsettledPersonInfoListDataGridView.Name = "UnsettledPersonInfoListDataGridView"
        Me.UnsettledPersonInfoListDataGridView.ReadOnly = True
        Me.UnsettledPersonInfoListDataGridView.RowHeadersVisible = False
        Me.UnsettledPersonInfoListDataGridView.Size = New System.Drawing.Size(874, 252)
        Me.UnsettledPersonInfoListDataGridView.TabIndex = 0
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "ID"
        Me.DataGridViewTextBoxColumn1.HeaderText = "ID"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Visible = False
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "Name"
        Me.DataGridViewTextBoxColumn2.HeaderText = "Pavadinimas (Vardas, Pavardė)"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "Code"
        Me.DataGridViewTextBoxColumn3.HeaderText = "Įmonės (asmens) kodas"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.DataPropertyName = "CodeVat"
        Me.DataGridViewTextBoxColumn5.HeaderText = "PVM kodas"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        Me.DataGridViewTextBoxColumn5.Visible = False
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "Address"
        Me.DataGridViewTextBoxColumn4.HeaderText = "Adresas"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.DataPropertyName = "Email"
        Me.DataGridViewTextBoxColumn6.HeaderText = "E-paštas"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.DataPropertyName = "CodeInternal"
        Me.DataGridViewTextBoxColumn7.HeaderText = "Vidinis kodas"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.ReadOnly = True
        Me.DataGridViewTextBoxColumn7.Visible = False
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.DataPropertyName = "ContactInfo"
        Me.DataGridViewTextBoxColumn8.HeaderText = "Kontaktinė info"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.ReadOnly = True
        Me.DataGridViewTextBoxColumn8.Visible = False
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.DataPropertyName = "Debt"
        DataGridViewCellStyle2.Format = "##,0.00"
        Me.DataGridViewTextBoxColumn9.DefaultCellStyle = DataGridViewCellStyle2
        Me.DataGridViewTextBoxColumn9.HeaderText = "Skola"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        Me.DataGridViewTextBoxColumn9.ReadOnly = True
        '
        'UnsettledPersonInfoListBindingSource
        '
        Me.UnsettledPersonInfoListBindingSource.DataSource = GetType(ApskaitaObjects.ActiveReports.UnsettledPersonInfo)
        '
        'ItemsDataGridView
        '
        Me.ItemsDataGridView.AllowUserToAddRows = False
        Me.ItemsDataGridView.AllowUserToDeleteRows = False
        Me.ItemsDataGridView.AllowUserToOrderColumns = True
        Me.ItemsDataGridView.AutoGenerateColumns = False
        Me.ItemsDataGridView.CausesValidation = False
        Me.ItemsDataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomCenter
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ItemsDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.ItemsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.ItemsDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn10, Me.DataGridViewTextBoxColumn14, Me.DataGridViewTextBoxColumn11, Me.DataGridViewTextBoxColumn12, Me.DataGridViewTextBoxColumn15, Me.DataGridViewTextBoxColumn16, Me.Debt})
        Me.ItemsDataGridView.DataSource = Me.ItemsSortedBindingSource
        Me.ItemsDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ItemsDataGridView.Location = New System.Drawing.Point(0, 0)
        Me.ItemsDataGridView.Name = "ItemsDataGridView"
        Me.ItemsDataGridView.ReadOnly = True
        Me.ItemsDataGridView.RowHeadersVisible = False
        Me.ItemsDataGridView.Size = New System.Drawing.Size(874, 188)
        Me.ItemsDataGridView.TabIndex = 0
        '
        'ItemsSortedBindingSource
        '
        Me.ItemsSortedBindingSource.DataMember = "ItemsSorted"
        Me.ItemsSortedBindingSource.DataSource = Me.UnsettledPersonInfoListBindingSource
        '
        'DataGridViewTextBoxColumn10
        '
        Me.DataGridViewTextBoxColumn10.DataPropertyName = "ID"
        Me.DataGridViewTextBoxColumn10.HeaderText = "BŽ ID"
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        Me.DataGridViewTextBoxColumn10.ReadOnly = True
        Me.DataGridViewTextBoxColumn10.Visible = False
        '
        'DataGridViewTextBoxColumn14
        '
        Me.DataGridViewTextBoxColumn14.DataPropertyName = "DocTypeHumanReadable"
        Me.DataGridViewTextBoxColumn14.HeaderText = "Dok. Tipas"
        Me.DataGridViewTextBoxColumn14.Name = "DataGridViewTextBoxColumn14"
        Me.DataGridViewTextBoxColumn14.ReadOnly = True
        '
        'DataGridViewTextBoxColumn11
        '
        Me.DataGridViewTextBoxColumn11.DataPropertyName = "Date"
        DataGridViewCellStyle4.Format = "d"
        DataGridViewCellStyle4.NullValue = Nothing
        Me.DataGridViewTextBoxColumn11.DefaultCellStyle = DataGridViewCellStyle4
        Me.DataGridViewTextBoxColumn11.HeaderText = "Data"
        Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
        Me.DataGridViewTextBoxColumn11.ReadOnly = True
        '
        'DataGridViewTextBoxColumn12
        '
        Me.DataGridViewTextBoxColumn12.DataPropertyName = "DocNo"
        Me.DataGridViewTextBoxColumn12.HeaderText = "Dok. Nr."
        Me.DataGridViewTextBoxColumn12.Name = "DataGridViewTextBoxColumn12"
        Me.DataGridViewTextBoxColumn12.ReadOnly = True
        '
        'DataGridViewTextBoxColumn15
        '
        Me.DataGridViewTextBoxColumn15.DataPropertyName = "Content"
        Me.DataGridViewTextBoxColumn15.HeaderText = "Turinys"
        Me.DataGridViewTextBoxColumn15.Name = "DataGridViewTextBoxColumn15"
        Me.DataGridViewTextBoxColumn15.ReadOnly = True
        '
        'DataGridViewTextBoxColumn16
        '
        Me.DataGridViewTextBoxColumn16.DataPropertyName = "SumInDocument"
        DataGridViewCellStyle5.Format = "##,0.00"
        Me.DataGridViewTextBoxColumn16.DefaultCellStyle = DataGridViewCellStyle5
        Me.DataGridViewTextBoxColumn16.HeaderText = "Dok. Suma"
        Me.DataGridViewTextBoxColumn16.Name = "DataGridViewTextBoxColumn16"
        Me.DataGridViewTextBoxColumn16.ReadOnly = True
        '
        'Debt
        '
        Me.Debt.DataPropertyName = "Debt"
        DataGridViewCellStyle6.Format = "##,0.00"
        Me.Debt.DefaultCellStyle = DataGridViewCellStyle6
        Me.Debt.HeaderText = "Skola"
        Me.Debt.Name = "Debt"
        Me.Debt.ReadOnly = True
        '
        'F_UnsettledPersonInfoList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(874, 505)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "F_UnsettledPersonInfoList"
        Me.ShowInTaskbar = False
        Me.Text = "Neapmokėti dokumentai"
        Me.Panel1.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.UnsettledPersonInfoListDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.UnsettledPersonInfoListBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ItemsDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ItemsSortedBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents PersonGroupComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents MarginOfErrorAccTextBox As AccControls.AccTextBox
    Friend WithEvents AccountAccGridComboBox As AccControls.AccGridComboBox
    Friend WithEvents AsOfDateDateTimePicker As System.Windows.Forms.DateTimePicker
    Friend WithEvents ForBuyersRadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents ForSuppliersRadioButton As System.Windows.Forms.RadioButton
    Friend WithEvents RefreshButton As System.Windows.Forms.Button
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents UnsettledPersonInfoListDataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents UnsettledPersonInfoListBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ItemsDataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents ItemsSortedBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn14 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn11 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn12 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn15 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn16 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Debt As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
