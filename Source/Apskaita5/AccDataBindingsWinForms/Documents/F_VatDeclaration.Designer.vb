<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F_VatDeclaration
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(F_VatDeclaration))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.MonthComboBox = New System.Windows.Forms.ComboBox
        Me.RefreshButton = New System.Windows.Forms.Button
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.YearComboBox = New System.Windows.Forms.ComboBox
        Me.ExportFFDataButton = New AccControlsWinForms.AccButton
        Me.CustomPeriodCheckBox = New System.Windows.Forms.CheckBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.SubtotalsDataListView = New BrightIdeasSoftware.DataListView
        Me.OlvColumn9 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn10 = New BrightIdeasSoftware.OLVColumn
        Me.SubtotalsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.VatDeclarationBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label4 = New System.Windows.Forms.Label
        Me.ItemsDataListView = New BrightIdeasSoftware.DataListView
        Me.OlvColumn2 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn1 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn3 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn4 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn5 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn6 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn7 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn8 = New BrightIdeasSoftware.OLVColumn
        Me.ItemsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.Label5 = New System.Windows.Forms.Label
        Me.ProgressFiller1 = New AccControlsWinForms.ProgressFiller
        Me.ProgressFiller2 = New AccControlsWinForms.ProgressFiller
        Me.DateAccDatePicker = New AccControlsWinForms.AccDatePicker
        Me.PeriodStartAccDatePicker = New AccControlsWinForms.AccDatePicker
        Me.PeriodEndAccDatePicker = New AccControlsWinForms.AccDatePicker
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.SubtotalsDataListView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SubtotalsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.VatDeclarationBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.ItemsDataListView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ItemsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 13
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 21.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.MonthComboBox, 7, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.RefreshButton, 9, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label3, 6, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 3, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.YearComboBox, 4, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.ExportFFDataButton, 11, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.CustomPeriodCheckBox, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label6, 3, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label7, 6, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.DateAccDatePicker, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.PeriodStartAccDatePicker, 4, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.PeriodEndAccDatePicker, 7, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(731, 57)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'MonthComboBox
        '
        Me.MonthComboBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.MonthComboBox.FormattingEnabled = True
        Me.MonthComboBox.Location = New System.Drawing.Point(456, 3)
        Me.MonthComboBox.Name = "MonthComboBox"
        Me.MonthComboBox.Size = New System.Drawing.Size(127, 21)
        Me.MonthComboBox.TabIndex = 2
        '
        'RefreshButton
        '
        Me.RefreshButton.Image = Global.AccDataBindingsWinForms.My.Resources.Resources.Button_Reload_icon_24p
        Me.RefreshButton.Location = New System.Drawing.Point(609, 3)
        Me.RefreshButton.Name = "RefreshButton"
        Me.TableLayoutPanel1.SetRowSpan(Me.RefreshButton, 2)
        Me.RefreshButton.Size = New System.Drawing.Size(32, 32)
        Me.RefreshButton.TabIndex = 6
        Me.RefreshButton.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(401, 6)
        Me.Label3.Margin = New System.Windows.Forms.Padding(3, 6, 3, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(49, 13)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Mėnuo:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(3, 6)
        Me.Label1.Margin = New System.Windows.Forms.Padding(3, 6, 3, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(38, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Data:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(200, 6)
        Me.Label2.Margin = New System.Windows.Forms.Padding(3, 6, 3, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(42, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Metai:"
        '
        'YearComboBox
        '
        Me.YearComboBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.YearComboBox.FormattingEnabled = True
        Me.YearComboBox.Location = New System.Drawing.Point(248, 3)
        Me.YearComboBox.Name = "YearComboBox"
        Me.YearComboBox.Size = New System.Drawing.Size(127, 21)
        Me.YearComboBox.TabIndex = 1
        '
        'ExportFFDataButton
        '
        Me.ExportFFDataButton.BorderStyleDown = System.Windows.Forms.Border3DStyle.Sunken
        Me.ExportFFDataButton.BorderStyleNormal = System.Windows.Forms.Border3DStyle.Raised
        Me.ExportFFDataButton.BorderStyleUp = System.Windows.Forms.Border3DStyle.Raised
        Me.ExportFFDataButton.ButtonStyle = AccControlsWinForms.rsButtonStyle.DropDownWithSep
        Me.ExportFFDataButton.Checked = False
        Me.ExportFFDataButton.DropDownSepWidth = 12
        Me.ExportFFDataButton.FocusRectangle = False
        Me.ExportFFDataButton.Image = Global.AccDataBindingsWinForms.My.Resources.Resources.FromFillerIcon_24x24alt
        Me.ExportFFDataButton.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ExportFFDataButton.ImagePadding = 2
        Me.ExportFFDataButton.Location = New System.Drawing.Point(667, 3)
        Me.ExportFFDataButton.Name = "ExportFFDataButton"
        Me.TableLayoutPanel1.SetRowSpan(Me.ExportFFDataButton, 2)
        Me.ExportFFDataButton.Size = New System.Drawing.Size(40, 32)
        Me.ExportFFDataButton.TabIndex = 7
        Me.ExportFFDataButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.ExportFFDataButton.TextPadding = 2
        '
        'CustomPeriodCheckBox
        '
        Me.CustomPeriodCheckBox.AutoSize = True
        Me.CustomPeriodCheckBox.CheckAlign = System.Drawing.ContentAlignment.TopRight
        Me.TableLayoutPanel1.SetColumnSpan(Me.CustomPeriodCheckBox, 2)
        Me.CustomPeriodCheckBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CustomPeriodCheckBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CustomPeriodCheckBox.Location = New System.Drawing.Point(3, 30)
        Me.CustomPeriodCheckBox.Name = "CustomPeriodCheckBox"
        Me.CustomPeriodCheckBox.Size = New System.Drawing.Size(171, 24)
        Me.CustomPeriodCheckBox.TabIndex = 3
        Me.CustomPeriodCheckBox.Text = "Kitas periodas"
        Me.CustomPeriodCheckBox.TextAlign = System.Drawing.ContentAlignment.TopRight
        Me.CustomPeriodCheckBox.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.Location = New System.Drawing.Point(200, 33)
        Me.Label6.Margin = New System.Windows.Forms.Padding(3, 6, 3, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(42, 24)
        Me.Label6.TabIndex = 9
        Me.Label6.Text = "Nuo:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Location = New System.Drawing.Point(401, 33)
        Me.Label7.Margin = New System.Windows.Forms.Padding(3, 6, 3, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(49, 24)
        Me.Label7.TabIndex = 10
        Me.Label7.Text = "Iki:"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 57)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SubtotalsDataListView)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Panel1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.AutoScroll = True
        Me.SplitContainer1.Panel2.Controls.Add(Me.ItemsDataListView)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Panel2)
        Me.SplitContainer1.Panel2.Controls.Add(Me.ProgressFiller1)
        Me.SplitContainer1.Size = New System.Drawing.Size(731, 232)
        Me.SplitContainer1.SplitterDistance = 130
        Me.SplitContainer1.TabIndex = 1
        '
        'SubtotalsDataListView
        '
        Me.SubtotalsDataListView.AllColumns.Add(Me.OlvColumn9)
        Me.SubtotalsDataListView.AllColumns.Add(Me.OlvColumn10)
        Me.SubtotalsDataListView.AllowColumnReorder = True
        Me.SubtotalsDataListView.AutoGenerateColumns = False
        Me.SubtotalsDataListView.CausesValidation = False
        Me.SubtotalsDataListView.CellEditUseWholeCell = False
        Me.SubtotalsDataListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.OlvColumn9, Me.OlvColumn10})
        Me.SubtotalsDataListView.Cursor = System.Windows.Forms.Cursors.Default
        Me.SubtotalsDataListView.DataSource = Me.SubtotalsBindingSource
        Me.SubtotalsDataListView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SubtotalsDataListView.FullRowSelect = True
        Me.SubtotalsDataListView.HasCollapsibleGroups = False
        Me.SubtotalsDataListView.HeaderWordWrap = True
        Me.SubtotalsDataListView.HideSelection = False
        Me.SubtotalsDataListView.IncludeColumnHeadersInCopy = True
        Me.SubtotalsDataListView.Location = New System.Drawing.Point(0, 24)
        Me.SubtotalsDataListView.Name = "SubtotalsDataListView"
        Me.SubtotalsDataListView.RenderNonEditableCheckboxesAsDisabled = True
        Me.SubtotalsDataListView.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.Submenu
        Me.SubtotalsDataListView.SelectedBackColor = System.Drawing.Color.PaleGreen
        Me.SubtotalsDataListView.SelectedForeColor = System.Drawing.Color.Black
        Me.SubtotalsDataListView.ShowCommandMenuOnRightClick = True
        Me.SubtotalsDataListView.ShowGroups = False
        Me.SubtotalsDataListView.ShowImagesOnSubItems = True
        Me.SubtotalsDataListView.ShowItemCountOnGroups = True
        Me.SubtotalsDataListView.ShowItemToolTips = True
        Me.SubtotalsDataListView.Size = New System.Drawing.Size(130, 208)
        Me.SubtotalsDataListView.TabIndex = 0
        Me.SubtotalsDataListView.UnfocusedSelectedBackColor = System.Drawing.Color.PaleGreen
        Me.SubtotalsDataListView.UnfocusedSelectedForeColor = System.Drawing.Color.Black
        Me.SubtotalsDataListView.UseCellFormatEvents = True
        Me.SubtotalsDataListView.UseCompatibleStateImageBehavior = False
        Me.SubtotalsDataListView.UseFilterIndicator = True
        Me.SubtotalsDataListView.UseFiltering = True
        Me.SubtotalsDataListView.UseHotItem = True
        Me.SubtotalsDataListView.UseNotifyPropertyChanged = True
        Me.SubtotalsDataListView.View = System.Windows.Forms.View.Details
        '
        'OlvColumn9
        '
        Me.OlvColumn9.AspectName = "Code"
        Me.OlvColumn9.CellEditUseWholeCell = True
        Me.OlvColumn9.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn9.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn9.IsEditable = False
        Me.OlvColumn9.Text = "Laukelio Kodas"
        Me.OlvColumn9.Width = 70
        '
        'OlvColumn10
        '
        Me.OlvColumn10.AspectName = "Sum"
        Me.OlvColumn10.CellEditUseWholeCell = True
        Me.OlvColumn10.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn10.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn10.IsEditable = False
        Me.OlvColumn10.Text = "Suma"
        Me.OlvColumn10.Width = 50
        '
        'SubtotalsBindingSource
        '
        Me.SubtotalsBindingSource.DataMember = "Subtotals"
        Me.SubtotalsBindingSource.DataSource = Me.VatDeclarationBindingSource
        '
        'VatDeclarationBindingSource
        '
        Me.VatDeclarationBindingSource.DataSource = GetType(ApskaitaObjects.ActiveReports.VatDeclaration)
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(130, 24)
        Me.Panel1.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(6, 6)
        Me.Label4.Margin = New System.Windows.Forms.Padding(3, 6, 3, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(120, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Sumos Laukeliuose:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'ItemsDataListView
        '
        Me.ItemsDataListView.AllColumns.Add(Me.OlvColumn2)
        Me.ItemsDataListView.AllColumns.Add(Me.OlvColumn1)
        Me.ItemsDataListView.AllColumns.Add(Me.OlvColumn3)
        Me.ItemsDataListView.AllColumns.Add(Me.OlvColumn4)
        Me.ItemsDataListView.AllColumns.Add(Me.OlvColumn5)
        Me.ItemsDataListView.AllColumns.Add(Me.OlvColumn6)
        Me.ItemsDataListView.AllColumns.Add(Me.OlvColumn7)
        Me.ItemsDataListView.AllColumns.Add(Me.OlvColumn8)
        Me.ItemsDataListView.AllowColumnReorder = True
        Me.ItemsDataListView.AutoGenerateColumns = False
        Me.ItemsDataListView.CausesValidation = False
        Me.ItemsDataListView.CellEditUseWholeCell = False
        Me.ItemsDataListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.OlvColumn2, Me.OlvColumn3, Me.OlvColumn4, Me.OlvColumn5, Me.OlvColumn6, Me.OlvColumn7, Me.OlvColumn8})
        Me.ItemsDataListView.Cursor = System.Windows.Forms.Cursors.Default
        Me.ItemsDataListView.DataSource = Me.ItemsBindingSource
        Me.ItemsDataListView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ItemsDataListView.FullRowSelect = True
        Me.ItemsDataListView.HasCollapsibleGroups = False
        Me.ItemsDataListView.HeaderWordWrap = True
        Me.ItemsDataListView.HideSelection = False
        Me.ItemsDataListView.IncludeColumnHeadersInCopy = True
        Me.ItemsDataListView.Location = New System.Drawing.Point(0, 24)
        Me.ItemsDataListView.Name = "ItemsDataListView"
        Me.ItemsDataListView.RenderNonEditableCheckboxesAsDisabled = True
        Me.ItemsDataListView.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.Submenu
        Me.ItemsDataListView.SelectedBackColor = System.Drawing.Color.PaleGreen
        Me.ItemsDataListView.SelectedForeColor = System.Drawing.Color.Black
        Me.ItemsDataListView.ShowCommandMenuOnRightClick = True
        Me.ItemsDataListView.ShowGroups = False
        Me.ItemsDataListView.ShowImagesOnSubItems = True
        Me.ItemsDataListView.ShowItemCountOnGroups = True
        Me.ItemsDataListView.ShowItemToolTips = True
        Me.ItemsDataListView.Size = New System.Drawing.Size(597, 208)
        Me.ItemsDataListView.TabIndex = 0
        Me.ItemsDataListView.UnfocusedSelectedBackColor = System.Drawing.Color.PaleGreen
        Me.ItemsDataListView.UnfocusedSelectedForeColor = System.Drawing.Color.Black
        Me.ItemsDataListView.UseCellFormatEvents = True
        Me.ItemsDataListView.UseCompatibleStateImageBehavior = False
        Me.ItemsDataListView.UseFilterIndicator = True
        Me.ItemsDataListView.UseFiltering = True
        Me.ItemsDataListView.UseHotItem = True
        Me.ItemsDataListView.UseNotifyPropertyChanged = True
        Me.ItemsDataListView.View = System.Windows.Forms.View.Details
        '
        'OlvColumn2
        '
        Me.OlvColumn2.AspectName = "Document"
        Me.OlvColumn2.CellEditUseWholeCell = True
        Me.OlvColumn2.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn2.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn2.IsEditable = False
        Me.OlvColumn2.Text = "Dokumentas"
        Me.OlvColumn2.Width = 100
        '
        'OlvColumn1
        '
        Me.OlvColumn1.AspectName = "ID"
        Me.OlvColumn1.CellEditUseWholeCell = True
        Me.OlvColumn1.DisplayIndex = 0
        Me.OlvColumn1.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn1.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn1.IsEditable = False
        Me.OlvColumn1.IsVisible = False
        Me.OlvColumn1.Text = "ID"
        Me.OlvColumn1.Width = 50
        '
        'OlvColumn3
        '
        Me.OlvColumn3.AspectName = "Item"
        Me.OlvColumn3.CellEditUseWholeCell = True
        Me.OlvColumn3.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn3.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn3.IsEditable = False
        Me.OlvColumn3.Text = "Eilutė"
        Me.OlvColumn3.Width = 100
        '
        'OlvColumn4
        '
        Me.OlvColumn4.AspectName = "ItemSum"
        Me.OlvColumn4.CellEditUseWholeCell = True
        Me.OlvColumn4.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn4.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn4.IsEditable = False
        Me.OlvColumn4.Text = "Suma Eilutėje"
        Me.OlvColumn4.Width = 50
        '
        'OlvColumn5
        '
        Me.OlvColumn5.AspectName = "ItemVatRate"
        Me.OlvColumn5.CellEditUseWholeCell = True
        Me.OlvColumn5.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn5.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn5.IsEditable = False
        Me.OlvColumn5.Text = "PVM Tarifas"
        Me.OlvColumn5.Width = 50
        '
        'OlvColumn6
        '
        Me.OlvColumn6.AspectName = "ItemVatSum"
        Me.OlvColumn6.CellEditUseWholeCell = True
        Me.OlvColumn6.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn6.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn6.IsEditable = False
        Me.OlvColumn6.Text = "PVM suma eilutėje"
        Me.OlvColumn6.Width = 50
        '
        'OlvColumn7
        '
        Me.OlvColumn7.AspectName = "FieldCode"
        Me.OlvColumn7.CellEditUseWholeCell = True
        Me.OlvColumn7.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn7.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn7.IsEditable = False
        Me.OlvColumn7.Text = "Kodas Deklaracijoje"
        Me.OlvColumn7.Width = 100
        '
        'OlvColumn8
        '
        Me.OlvColumn8.AspectName = "FieldSum"
        Me.OlvColumn8.CellEditUseWholeCell = True
        Me.OlvColumn8.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn8.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn8.IsEditable = False
        Me.OlvColumn8.Text = "Suma Deklaracijoje"
        Me.OlvColumn8.Width = 50
        '
        'ItemsBindingSource
        '
        Me.ItemsBindingSource.DataMember = "Items"
        Me.ItemsBindingSource.DataSource = Me.VatDeclarationBindingSource
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(597, 24)
        Me.Panel2.TabIndex = 6
        '
        'Label5
        '
        Me.Label5.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.Location = New System.Drawing.Point(6, 6)
        Me.Label5.Margin = New System.Windows.Forms.Padding(3, 6, 3, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(98, 13)
        Me.Label5.TabIndex = 6
        Me.Label5.Text = "Detalios eilutės:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'ProgressFiller1
        '
        Me.ProgressFiller1.Location = New System.Drawing.Point(-143, 16)
        Me.ProgressFiller1.Name = "ProgressFiller1"
        Me.ProgressFiller1.Size = New System.Drawing.Size(62, 40)
        Me.ProgressFiller1.TabIndex = 2
        Me.ProgressFiller1.Visible = False
        '
        'ProgressFiller2
        '
        Me.ProgressFiller2.Location = New System.Drawing.Point(87, 54)
        Me.ProgressFiller2.Name = "ProgressFiller2"
        Me.ProgressFiller2.Size = New System.Drawing.Size(62, 40)
        Me.ProgressFiller2.TabIndex = 3
        Me.ProgressFiller2.Visible = False
        '
        'DateAccDatePicker
        '
        Me.DateAccDatePicker.BoldedDates = Nothing
        Me.DateAccDatePicker.Location = New System.Drawing.Point(47, 3)
        Me.DateAccDatePicker.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.DateAccDatePicker.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.DateAccDatePicker.Name = "DateAccDatePicker"
        Me.DateAccDatePicker.ReadOnly = False
        Me.DateAccDatePicker.ShowWeekNumbers = True
        Me.DateAccDatePicker.Size = New System.Drawing.Size(127, 20)
        Me.DateAccDatePicker.TabIndex = 0
        Me.DateAccDatePicker.Value = New Date(2017, 10, 13, 0, 0, 0, 0)
        '
        'PeriodStartAccDatePicker
        '
        Me.PeriodStartAccDatePicker.BoldedDates = Nothing
        Me.PeriodStartAccDatePicker.Location = New System.Drawing.Point(248, 30)
        Me.PeriodStartAccDatePicker.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.PeriodStartAccDatePicker.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.PeriodStartAccDatePicker.Name = "PeriodStartAccDatePicker"
        Me.PeriodStartAccDatePicker.ReadOnly = False
        Me.PeriodStartAccDatePicker.ShowWeekNumbers = True
        Me.PeriodStartAccDatePicker.Size = New System.Drawing.Size(127, 20)
        Me.PeriodStartAccDatePicker.TabIndex = 4
        Me.PeriodStartAccDatePicker.Value = New Date(2017, 10, 13, 0, 0, 0, 0)
        '
        'PeriodEndAccDatePicker
        '
        Me.PeriodEndAccDatePicker.BoldedDates = Nothing
        Me.PeriodEndAccDatePicker.Location = New System.Drawing.Point(456, 30)
        Me.PeriodEndAccDatePicker.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.PeriodEndAccDatePicker.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.PeriodEndAccDatePicker.Name = "PeriodEndAccDatePicker"
        Me.PeriodEndAccDatePicker.ReadOnly = False
        Me.PeriodEndAccDatePicker.ShowWeekNumbers = True
        Me.PeriodEndAccDatePicker.Size = New System.Drawing.Size(127, 20)
        Me.PeriodEndAccDatePicker.TabIndex = 5
        Me.PeriodEndAccDatePicker.Value = New Date(2017, 10, 13, 0, 0, 0, 0)
        '
        'F_VatDeclaration
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(731, 289)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.ProgressFiller2)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "F_VatDeclaration"
        Me.ShowInTaskbar = False
        Me.Text = "PVM Deklaracija"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.SubtotalsDataListView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SubtotalsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.VatDeclarationBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.ItemsDataListView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ItemsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents MonthComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents YearComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents RefreshButton As System.Windows.Forms.Button
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SubtotalsDataListView As BrightIdeasSoftware.DataListView
    Friend WithEvents ItemsDataListView As BrightIdeasSoftware.DataListView
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents VatDeclarationBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ItemsBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents SubtotalsBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ProgressFiller1 As AccControlsWinForms.ProgressFiller
    Friend WithEvents ProgressFiller2 As AccControlsWinForms.ProgressFiller
    Friend WithEvents OlvColumn1 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn2 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn3 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn4 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn5 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn6 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn7 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn8 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn9 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn10 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents ExportFFDataButton As AccControlsWinForms.AccButton
    Friend WithEvents CustomPeriodCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents DateAccDatePicker As AccControlsWinForms.AccDatePicker
    Friend WithEvents PeriodStartAccDatePicker As AccControlsWinForms.AccDatePicker
    Friend WithEvents PeriodEndAccDatePicker As AccControlsWinForms.AccDatePicker
End Class
