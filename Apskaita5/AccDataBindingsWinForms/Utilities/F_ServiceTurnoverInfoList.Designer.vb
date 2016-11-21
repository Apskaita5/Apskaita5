<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Friend Class F_ServiceTurnoverInfoList
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(F_ServiceTurnoverInfoList))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.RefreshButton = New System.Windows.Forms.Button
        Me.ServicesWithoutTurnoverCheckBox = New System.Windows.Forms.CheckBox
        Me.TradedTypeComboBox = New System.Windows.Forms.ComboBox
        Me.DateToDateTimePicker = New System.Windows.Forms.DateTimePicker
        Me.Label3 = New System.Windows.Forms.Label
        Me.DateFromDateTimePicker = New System.Windows.Forms.DateTimePicker
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.ServiceTurnoverInfoListBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ServiceTurnoverInfoListDataListView = New BrightIdeasSoftware.DataListView
        Me.OlvColumn1 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn2 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn3 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn4 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn5 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn6 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn7 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn8 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn9 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn10 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn11 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn12 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn13 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn14 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn15 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn16 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn17 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn18 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn19 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn20 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn21 = New BrightIdeasSoftware.OLVColumn
        Me.ProgressFiller1 = New AccControlsWinForms.ProgressFiller
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.ServiceTurnoverInfoListBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ServiceTurnoverInfoListDataListView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 12
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 200.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.RefreshButton, 10, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.ServicesWithoutTurnoverCheckBox, 9, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.TradedTypeComboBox, 7, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.DateToDateTimePicker, 4, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label3, 6, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.DateFromDateTimePicker, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 3, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(834, 38)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'RefreshButton
        '
        Me.RefreshButton.Image = Global.AccDataBindingsWinForms.My.Resources.Resources.Button_Reload_icon_24p
        Me.RefreshButton.Location = New System.Drawing.Point(771, 3)
        Me.RefreshButton.Name = "RefreshButton"
        Me.RefreshButton.Size = New System.Drawing.Size(33, 32)
        Me.RefreshButton.TabIndex = 1
        Me.RefreshButton.UseVisualStyleBackColor = True
        '
        'ServicesWithoutTurnoverCheckBox
        '
        Me.ServicesWithoutTurnoverCheckBox.AutoSize = True
        Me.ServicesWithoutTurnoverCheckBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ServicesWithoutTurnoverCheckBox.Location = New System.Drawing.Point(622, 5)
        Me.ServicesWithoutTurnoverCheckBox.Margin = New System.Windows.Forms.Padding(3, 5, 3, 3)
        Me.ServicesWithoutTurnoverCheckBox.Name = "ServicesWithoutTurnoverCheckBox"
        Me.ServicesWithoutTurnoverCheckBox.Size = New System.Drawing.Size(143, 17)
        Me.ServicesWithoutTurnoverCheckBox.TabIndex = 1
        Me.ServicesWithoutTurnoverCheckBox.Text = "Įtraukti be apyvartos"
        Me.ServicesWithoutTurnoverCheckBox.UseVisualStyleBackColor = True
        '
        'TradedTypeComboBox
        '
        Me.TradedTypeComboBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TradedTypeComboBox.FormattingEnabled = True
        Me.TradedTypeComboBox.Location = New System.Drawing.Point(402, 3)
        Me.TradedTypeComboBox.Name = "TradedTypeComboBox"
        Me.TradedTypeComboBox.Size = New System.Drawing.Size(194, 21)
        Me.TradedTypeComboBox.TabIndex = 2
        '
        'DateToDateTimePicker
        '
        Me.DateToDateTimePicker.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DateToDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateToDateTimePicker.Location = New System.Drawing.Point(214, 3)
        Me.DateToDateTimePicker.Name = "DateToDateTimePicker"
        Me.DateToDateTimePicker.Size = New System.Drawing.Size(114, 20)
        Me.DateToDateTimePicker.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(354, 5)
        Me.Label3.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(42, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Tipas:"
        '
        'DateFromDateTimePicker
        '
        Me.DateFromDateTimePicker.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DateFromDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateFromDateTimePicker.Location = New System.Drawing.Point(43, 3)
        Me.DateFromDateTimePicker.Name = "DateFromDateTimePicker"
        Me.DateFromDateTimePicker.Size = New System.Drawing.Size(114, 20)
        Me.DateFromDateTimePicker.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(3, 5)
        Me.Label1.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(34, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Nuo:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(183, 5)
        Me.Label2.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(25, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Iki:"
        '
        'ServiceTurnoverInfoListBindingSource
        '
        Me.ServiceTurnoverInfoListBindingSource.DataSource = GetType(ApskaitaObjects.ActiveReports.ServiceTurnoverInfo)
        '
        'ServiceTurnoverInfoListDataListView
        '
        Me.ServiceTurnoverInfoListDataListView.AllColumns.Add(Me.OlvColumn2)
        Me.ServiceTurnoverInfoListDataListView.AllColumns.Add(Me.OlvColumn1)
        Me.ServiceTurnoverInfoListDataListView.AllColumns.Add(Me.OlvColumn3)
        Me.ServiceTurnoverInfoListDataListView.AllColumns.Add(Me.OlvColumn4)
        Me.ServiceTurnoverInfoListDataListView.AllColumns.Add(Me.OlvColumn5)
        Me.ServiceTurnoverInfoListDataListView.AllColumns.Add(Me.OlvColumn6)
        Me.ServiceTurnoverInfoListDataListView.AllColumns.Add(Me.OlvColumn7)
        Me.ServiceTurnoverInfoListDataListView.AllColumns.Add(Me.OlvColumn8)
        Me.ServiceTurnoverInfoListDataListView.AllColumns.Add(Me.OlvColumn9)
        Me.ServiceTurnoverInfoListDataListView.AllColumns.Add(Me.OlvColumn10)
        Me.ServiceTurnoverInfoListDataListView.AllColumns.Add(Me.OlvColumn11)
        Me.ServiceTurnoverInfoListDataListView.AllColumns.Add(Me.OlvColumn12)
        Me.ServiceTurnoverInfoListDataListView.AllColumns.Add(Me.OlvColumn13)
        Me.ServiceTurnoverInfoListDataListView.AllColumns.Add(Me.OlvColumn14)
        Me.ServiceTurnoverInfoListDataListView.AllColumns.Add(Me.OlvColumn15)
        Me.ServiceTurnoverInfoListDataListView.AllColumns.Add(Me.OlvColumn16)
        Me.ServiceTurnoverInfoListDataListView.AllColumns.Add(Me.OlvColumn17)
        Me.ServiceTurnoverInfoListDataListView.AllColumns.Add(Me.OlvColumn18)
        Me.ServiceTurnoverInfoListDataListView.AllColumns.Add(Me.OlvColumn19)
        Me.ServiceTurnoverInfoListDataListView.AllColumns.Add(Me.OlvColumn20)
        Me.ServiceTurnoverInfoListDataListView.AllColumns.Add(Me.OlvColumn21)
        Me.ServiceTurnoverInfoListDataListView.AllowColumnReorder = True
        Me.ServiceTurnoverInfoListDataListView.AutoGenerateColumns = False
        Me.ServiceTurnoverInfoListDataListView.CausesValidation = False
        Me.ServiceTurnoverInfoListDataListView.CellEditUseWholeCell = False
        Me.ServiceTurnoverInfoListDataListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.OlvColumn2, Me.OlvColumn9, Me.OlvColumn11, Me.OlvColumn12, Me.OlvColumn13, Me.OlvColumn14, Me.OlvColumn15, Me.OlvColumn16, Me.OlvColumn17, Me.OlvColumn18, Me.OlvColumn19, Me.OlvColumn20, Me.OlvColumn21})
        Me.ServiceTurnoverInfoListDataListView.Cursor = System.Windows.Forms.Cursors.Default
        Me.ServiceTurnoverInfoListDataListView.DataSource = Me.ServiceTurnoverInfoListBindingSource
        Me.ServiceTurnoverInfoListDataListView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ServiceTurnoverInfoListDataListView.FullRowSelect = True
        Me.ServiceTurnoverInfoListDataListView.HasCollapsibleGroups = False
        Me.ServiceTurnoverInfoListDataListView.HeaderWordWrap = True
        Me.ServiceTurnoverInfoListDataListView.HideSelection = False
        Me.ServiceTurnoverInfoListDataListView.IncludeColumnHeadersInCopy = True
        Me.ServiceTurnoverInfoListDataListView.Location = New System.Drawing.Point(0, 38)
        Me.ServiceTurnoverInfoListDataListView.Name = "ServiceTurnoverInfoListDataListView"
        Me.ServiceTurnoverInfoListDataListView.RenderNonEditableCheckboxesAsDisabled = True
        Me.ServiceTurnoverInfoListDataListView.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.Submenu
        Me.ServiceTurnoverInfoListDataListView.SelectedBackColor = System.Drawing.Color.PaleGreen
        Me.ServiceTurnoverInfoListDataListView.SelectedForeColor = System.Drawing.Color.Black
        Me.ServiceTurnoverInfoListDataListView.ShowCommandMenuOnRightClick = True
        Me.ServiceTurnoverInfoListDataListView.ShowGroups = False
        Me.ServiceTurnoverInfoListDataListView.ShowImagesOnSubItems = True
        Me.ServiceTurnoverInfoListDataListView.ShowItemCountOnGroups = True
        Me.ServiceTurnoverInfoListDataListView.ShowItemToolTips = True
        Me.ServiceTurnoverInfoListDataListView.Size = New System.Drawing.Size(834, 362)
        Me.ServiceTurnoverInfoListDataListView.TabIndex = 4
        Me.ServiceTurnoverInfoListDataListView.UnfocusedSelectedBackColor = System.Drawing.Color.PaleGreen
        Me.ServiceTurnoverInfoListDataListView.UnfocusedSelectedForeColor = System.Drawing.Color.Black
        Me.ServiceTurnoverInfoListDataListView.UseCellFormatEvents = True
        Me.ServiceTurnoverInfoListDataListView.UseCompatibleStateImageBehavior = False
        Me.ServiceTurnoverInfoListDataListView.UseFilterIndicator = True
        Me.ServiceTurnoverInfoListDataListView.UseFiltering = True
        Me.ServiceTurnoverInfoListDataListView.UseHotItem = True
        Me.ServiceTurnoverInfoListDataListView.UseNotifyPropertyChanged = True
        Me.ServiceTurnoverInfoListDataListView.View = System.Windows.Forms.View.Details
        '
        'OlvColumn1
        '
        Me.OlvColumn1.AspectName = "ID"
        Me.OlvColumn1.CellEditUseWholeCell = True
        Me.OlvColumn1.DisplayIndex = 1
        Me.OlvColumn1.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn1.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn1.IsEditable = False
        Me.OlvColumn1.IsVisible = False
        Me.OlvColumn1.Text = "ID"
        Me.OlvColumn1.ToolTipText = ""
        Me.OlvColumn1.Width = 100
        '
        'OlvColumn2
        '
        Me.OlvColumn2.AspectName = "Name"
        Me.OlvColumn2.CellEditUseWholeCell = True
        Me.OlvColumn2.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn2.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn2.IsEditable = False
        Me.OlvColumn2.Text = "Pavadinimas"
        Me.OlvColumn2.ToolTipText = ""
        Me.OlvColumn2.Width = 214
        '
        'OlvColumn3
        '
        Me.OlvColumn3.AspectName = "TradedType"
        Me.OlvColumn3.CellEditUseWholeCell = True
        Me.OlvColumn3.DisplayIndex = 2
        Me.OlvColumn3.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn3.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn3.IsEditable = False
        Me.OlvColumn3.IsVisible = False
        Me.OlvColumn3.Text = "Tipas"
        Me.OlvColumn3.ToolTipText = ""
        Me.OlvColumn3.Width = 100
        '
        'OlvColumn4
        '
        Me.OlvColumn4.AspectName = "AccountSales"
        Me.OlvColumn4.CellEditUseWholeCell = True
        Me.OlvColumn4.DisplayIndex = 3
        Me.OlvColumn4.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn4.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn4.IsEditable = False
        Me.OlvColumn4.IsVisible = False
        Me.OlvColumn4.Text = "Pardavimų Sąsk."
        Me.OlvColumn4.ToolTipText = ""
        Me.OlvColumn4.Width = 100
        '
        'OlvColumn5
        '
        Me.OlvColumn5.AspectName = "AccountPurchase"
        Me.OlvColumn5.CellEditUseWholeCell = True
        Me.OlvColumn5.DisplayIndex = 4
        Me.OlvColumn5.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn5.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn5.IsEditable = False
        Me.OlvColumn5.IsVisible = False
        Me.OlvColumn5.Text = "Pirkimų Sąsk."
        Me.OlvColumn5.ToolTipText = ""
        Me.OlvColumn5.Width = 100
        '
        'OlvColumn6
        '
        Me.OlvColumn6.AspectName = "DefaultRateVatSales"
        Me.OlvColumn6.AspectToStringFormat = "{0:##,0.00}"
        Me.OlvColumn6.CellEditUseWholeCell = True
        Me.OlvColumn6.DisplayIndex = 5
        Me.OlvColumn6.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn6.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn6.IsEditable = False
        Me.OlvColumn6.IsVisible = False
        Me.OlvColumn6.Text = "Pardavimų PVM %"
        Me.OlvColumn6.ToolTipText = ""
        Me.OlvColumn6.Width = 100
        '
        'OlvColumn7
        '
        Me.OlvColumn7.AspectName = "DefaultRateVatPurchase"
        Me.OlvColumn7.AspectToStringFormat = "{0:##,0.00}"
        Me.OlvColumn7.CellEditUseWholeCell = True
        Me.OlvColumn7.DisplayIndex = 6
        Me.OlvColumn7.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn7.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn7.IsEditable = False
        Me.OlvColumn7.IsVisible = False
        Me.OlvColumn7.Text = "Pirkimų PVM %"
        Me.OlvColumn7.ToolTipText = ""
        Me.OlvColumn7.Width = 100
        '
        'OlvColumn8
        '
        Me.OlvColumn8.AspectName = "AccountVatPurchase"
        Me.OlvColumn8.CellEditUseWholeCell = True
        Me.OlvColumn8.DisplayIndex = 7
        Me.OlvColumn8.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn8.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn8.IsEditable = False
        Me.OlvColumn8.IsVisible = False
        Me.OlvColumn8.Text = "Pirkimų PVM Sąsk."
        Me.OlvColumn8.ToolTipText = ""
        Me.OlvColumn8.Width = 100
        '
        'OlvColumn9
        '
        Me.OlvColumn9.AspectName = "ServiceCode"
        Me.OlvColumn9.CellEditUseWholeCell = True
        Me.OlvColumn9.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn9.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn9.IsEditable = False
        Me.OlvColumn9.Text = "Kodas"
        Me.OlvColumn9.ToolTipText = ""
        Me.OlvColumn9.Width = 61
        '
        'OlvColumn10
        '
        Me.OlvColumn10.AspectName = "IsObsolete"
        Me.OlvColumn10.CellEditUseWholeCell = True
        Me.OlvColumn10.CheckBoxes = True
        Me.OlvColumn10.DisplayIndex = 9
        Me.OlvColumn10.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn10.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn10.IsEditable = False
        Me.OlvColumn10.IsVisible = False
        Me.OlvColumn10.Text = "Istoriniai"
        Me.OlvColumn10.ToolTipText = ""
        Me.OlvColumn10.Width = 100
        '
        'OlvColumn11
        '
        Me.OlvColumn11.AspectName = "PurchasedAmount"
        Me.OlvColumn11.AspectToStringFormat = "{0:##,0.0000}"
        Me.OlvColumn11.CellEditUseWholeCell = True
        Me.OlvColumn11.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn11.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn11.IsEditable = False
        Me.OlvColumn11.Text = "Įsigyta vnt."
        Me.OlvColumn11.ToolTipText = ""
        Me.OlvColumn11.Width = 76
        '
        'OlvColumn12
        '
        Me.OlvColumn12.AspectName = "PurchasedSum"
        Me.OlvColumn12.AspectToStringFormat = "{0:##,0.00}"
        Me.OlvColumn12.CellEditUseWholeCell = True
        Me.OlvColumn12.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn12.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn12.IsEditable = False
        Me.OlvColumn12.Text = "Įsigyta Vertė"
        Me.OlvColumn12.ToolTipText = ""
        Me.OlvColumn12.Width = 84
        '
        'OlvColumn13
        '
        Me.OlvColumn13.AspectName = "PurchasedAmountReturned"
        Me.OlvColumn13.AspectToStringFormat = "{0:##,0.0000}"
        Me.OlvColumn13.CellEditUseWholeCell = True
        Me.OlvColumn13.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn13.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn13.IsEditable = False
        Me.OlvColumn13.Text = "Įsigijimų Grąžinta vnt."
        Me.OlvColumn13.ToolTipText = ""
        Me.OlvColumn13.Width = 86
        '
        'OlvColumn14
        '
        Me.OlvColumn14.AspectName = "PurchasedSumReturned"
        Me.OlvColumn14.AspectToStringFormat = "{0:##,0.00}"
        Me.OlvColumn14.CellEditUseWholeCell = True
        Me.OlvColumn14.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn14.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn14.IsEditable = False
        Me.OlvColumn14.Text = "Įsigijimų Grąžinta Vertė"
        Me.OlvColumn14.ToolTipText = ""
        Me.OlvColumn14.Width = 83
        '
        'OlvColumn15
        '
        Me.OlvColumn15.AspectName = "PurchasedSumReductions"
        Me.OlvColumn15.AspectToStringFormat = "{0:##,0.00}"
        Me.OlvColumn15.CellEditUseWholeCell = True
        Me.OlvColumn15.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn15.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn15.IsEditable = False
        Me.OlvColumn15.Text = "Įsigijimų Vertė Sumažinta"
        Me.OlvColumn15.ToolTipText = ""
        Me.OlvColumn15.Width = 82
        '
        'OlvColumn16
        '
        Me.OlvColumn16.AspectName = "SoldAmount"
        Me.OlvColumn16.AspectToStringFormat = "{0:##,0.0000}"
        Me.OlvColumn16.CellEditUseWholeCell = True
        Me.OlvColumn16.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn16.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn16.IsEditable = False
        Me.OlvColumn16.Text = "Parduota vnt."
        Me.OlvColumn16.ToolTipText = ""
        Me.OlvColumn16.Width = 81
        '
        'OlvColumn17
        '
        Me.OlvColumn17.AspectName = "SoldSum"
        Me.OlvColumn17.AspectToStringFormat = "{0:##,0.00}"
        Me.OlvColumn17.CellEditUseWholeCell = True
        Me.OlvColumn17.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn17.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn17.IsEditable = False
        Me.OlvColumn17.Text = "Parduota Vertė"
        Me.OlvColumn17.ToolTipText = ""
        Me.OlvColumn17.Width = 100
        '
        'OlvColumn18
        '
        Me.OlvColumn18.AspectName = "SoldAmountReturned"
        Me.OlvColumn18.AspectToStringFormat = "{0:##,0.0000}"
        Me.OlvColumn18.CellEditUseWholeCell = True
        Me.OlvColumn18.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn18.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn18.IsEditable = False
        Me.OlvColumn18.Text = "Pardavimų Grąžinta vnt."
        Me.OlvColumn18.ToolTipText = ""
        Me.OlvColumn18.Width = 100
        '
        'OlvColumn19
        '
        Me.OlvColumn19.AspectName = "SoldSumReturned"
        Me.OlvColumn19.AspectToStringFormat = "{0:##,0.00}"
        Me.OlvColumn19.CellEditUseWholeCell = True
        Me.OlvColumn19.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn19.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn19.IsEditable = False
        Me.OlvColumn19.Text = "Pardavimų Grąžinta Vertė"
        Me.OlvColumn19.ToolTipText = ""
        Me.OlvColumn19.Width = 100
        '
        'OlvColumn20
        '
        Me.OlvColumn20.AspectName = "SoldSumReductions"
        Me.OlvColumn20.AspectToStringFormat = "{0:##,0.00}"
        Me.OlvColumn20.CellEditUseWholeCell = True
        Me.OlvColumn20.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn20.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn20.IsEditable = False
        Me.OlvColumn20.Text = "Pardavimų Vertė Sumažinta"
        Me.OlvColumn20.ToolTipText = ""
        Me.OlvColumn20.Width = 100
        '
        'OlvColumn21
        '
        Me.OlvColumn21.AspectName = "SoldSumDiscounts"
        Me.OlvColumn21.AspectToStringFormat = "{0:##,0.00}"
        Me.OlvColumn21.CellEditUseWholeCell = True
        Me.OlvColumn21.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn21.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn21.IsEditable = False
        Me.OlvColumn21.Text = "Suteikta Nuolaidų"
        Me.OlvColumn21.ToolTipText = ""
        Me.OlvColumn21.Width = 100
        '
        'ProgressFiller1
        '
        Me.ProgressFiller1.Location = New System.Drawing.Point(183, 71)
        Me.ProgressFiller1.Name = "ProgressFiller1"
        Me.ProgressFiller1.Size = New System.Drawing.Size(154, 57)
        Me.ProgressFiller1.TabIndex = 5
        Me.ProgressFiller1.Visible = False
        '
        'F_ServiceTurnoverInfoList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(834, 400)
        Me.Controls.Add(Me.ServiceTurnoverInfoListDataListView)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.ProgressFiller1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "F_ServiceTurnoverInfoList"
        Me.ShowInTaskbar = False
        Me.Text = "Paslaugų apyvartos ataskaita"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.ServiceTurnoverInfoListBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ServiceTurnoverInfoListDataListView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents ServicesWithoutTurnoverCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents TradedTypeComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents DateFromDateTimePicker As System.Windows.Forms.DateTimePicker
    Friend WithEvents DateToDateTimePicker As System.Windows.Forms.DateTimePicker
    Friend WithEvents RefreshButton As System.Windows.Forms.Button
    Friend WithEvents ServiceTurnoverInfoListBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ServiceTurnoverInfoListDataListView As BrightIdeasSoftware.DataListView
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
    Friend WithEvents OlvColumn11 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn12 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn13 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn14 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn15 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn16 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn17 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn18 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn19 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn20 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn21 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents ProgressFiller1 As AccControlsWinForms.ProgressFiller
End Class
