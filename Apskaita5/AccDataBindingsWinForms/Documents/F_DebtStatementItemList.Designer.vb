<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F_DebtStatementItemList
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(F_DebtStatementItemList))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.RefreshButton = New System.Windows.Forms.Button
        Me.AccountAccGridComboBox = New AccControlsWinForms.AccListComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.DateFromDateTimePicker = New System.Windows.Forms.DateTimePicker
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.DateToDateTimePicker = New System.Windows.Forms.DateTimePicker
        Me.StatementDateTimePicker = New System.Windows.Forms.DateTimePicker
        Me.Label4 = New System.Windows.Forms.Label
        Me.ReportDataListView = New BrightIdeasSoftware.DataListView
        Me.OlvColumn1 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn2 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn3 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn4 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn5 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn6 = New BrightIdeasSoftware.OLVColumn
        Me.DebtStatementItemListBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ProgressFiller1 = New AccControlsWinForms.ProgressFiller
        Me.ProgressFiller2 = New AccControlsWinForms.ProgressFiller
        Me.SignWithFacsimileCheckBox = New System.Windows.Forms.CheckBox
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.ReportDataListView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DebtStatementItemListBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 10
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel1.Controls.Add(Me.RefreshButton, 9, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.AccountAccGridComboBox, 7, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 3, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.DateFromDateTimePicker, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label3, 6, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.DateToDateTimePicker, 4, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.StatementDateTimePicker, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label4, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.SignWithFacsimileCheckBox, 4, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(928, 56)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'RefreshButton
        '
        Me.RefreshButton.Image = Global.AccDataBindingsWinForms.My.Resources.Resources.Button_Reload_icon_24p
        Me.RefreshButton.Location = New System.Drawing.Point(891, 3)
        Me.RefreshButton.Name = "RefreshButton"
        Me.TableLayoutPanel1.SetRowSpan(Me.RefreshButton, 2)
        Me.RefreshButton.Size = New System.Drawing.Size(33, 34)
        Me.RefreshButton.TabIndex = 5
        Me.RefreshButton.UseVisualStyleBackColor = True
        '
        'AccountAccGridComboBox
        '
        Me.AccountAccGridComboBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AccountAccGridComboBox.EmptyValueString = ""
        Me.AccountAccGridComboBox.FilterString = ""
        Me.AccountAccGridComboBox.FormattingEnabled = True
        Me.AccountAccGridComboBox.InstantBinding = True
        Me.AccountAccGridComboBox.Location = New System.Drawing.Point(683, 3)
        Me.AccountAccGridComboBox.Name = "AccountAccGridComboBox"
        Me.AccountAccGridComboBox.Size = New System.Drawing.Size(182, 21)
        Me.AccountAccGridComboBox.TabIndex = 2
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(347, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.Label2.Size = New System.Drawing.Size(24, 18)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "iki:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'DateFromDateTimePicker
        '
        Me.DateFromDateTimePicker.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DateFromDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateFromDateTimePicker.Location = New System.Drawing.Point(107, 3)
        Me.DateFromDateTimePicker.Name = "DateFromDateTimePicker"
        Me.DateFromDateTimePicker.Size = New System.Drawing.Size(214, 20)
        Me.DateFromDateTimePicker.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(617, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.Label3.Size = New System.Drawing.Size(60, 18)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "Sąskaita:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(3, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Padding = New System.Windows.Forms.Padding(0, 7, 0, 0)
        Me.Label1.Size = New System.Drawing.Size(98, 20)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Laikotarpis nuo:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'DateToDateTimePicker
        '
        Me.DateToDateTimePicker.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DateToDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateToDateTimePicker.Location = New System.Drawing.Point(377, 3)
        Me.DateToDateTimePicker.Name = "DateToDateTimePicker"
        Me.DateToDateTimePicker.Size = New System.Drawing.Size(214, 20)
        Me.DateToDateTimePicker.TabIndex = 1
        '
        'StatementDateTimePicker
        '
        Me.StatementDateTimePicker.Dock = System.Windows.Forms.DockStyle.Fill
        Me.StatementDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.StatementDateTimePicker.Location = New System.Drawing.Point(107, 30)
        Me.StatementDateTimePicker.Name = "StatementDateTimePicker"
        Me.StatementDateTimePicker.Size = New System.Drawing.Size(214, 20)
        Me.StatementDateTimePicker.TabIndex = 3
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(3, 27)
        Me.Label4.Name = "Label4"
        Me.Label4.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.Label4.Size = New System.Drawing.Size(98, 29)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Aktų Data:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'ReportDataListView
        '
        Me.ReportDataListView.AllColumns.Add(Me.OlvColumn1)
        Me.ReportDataListView.AllColumns.Add(Me.OlvColumn2)
        Me.ReportDataListView.AllColumns.Add(Me.OlvColumn3)
        Me.ReportDataListView.AllColumns.Add(Me.OlvColumn4)
        Me.ReportDataListView.AllColumns.Add(Me.OlvColumn5)
        Me.ReportDataListView.AllColumns.Add(Me.OlvColumn6)
        Me.ReportDataListView.AllowColumnReorder = True
        Me.ReportDataListView.AutoGenerateColumns = False
        Me.ReportDataListView.CausesValidation = False
        Me.ReportDataListView.CellEditUseWholeCell = False
        Me.ReportDataListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.OlvColumn1, Me.OlvColumn2, Me.OlvColumn3, Me.OlvColumn4, Me.OlvColumn5, Me.OlvColumn6})
        Me.ReportDataListView.Cursor = System.Windows.Forms.Cursors.Default
        Me.ReportDataListView.DataSource = Me.DebtStatementItemListBindingSource
        Me.ReportDataListView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ReportDataListView.FullRowSelect = True
        Me.ReportDataListView.HeaderWordWrap = True
        Me.ReportDataListView.HideSelection = False
        Me.ReportDataListView.IncludeColumnHeadersInCopy = True
        Me.ReportDataListView.Location = New System.Drawing.Point(0, 56)
        Me.ReportDataListView.Name = "ReportDataListView"
        Me.ReportDataListView.RenderNonEditableCheckboxesAsDisabled = True
        Me.ReportDataListView.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.Submenu
        Me.ReportDataListView.SelectedBackColor = System.Drawing.Color.PaleGreen
        Me.ReportDataListView.SelectedForeColor = System.Drawing.Color.Black
        Me.ReportDataListView.ShowCommandMenuOnRightClick = True
        Me.ReportDataListView.ShowFilterMenuOnRightClick = False
        Me.ReportDataListView.ShowImagesOnSubItems = True
        Me.ReportDataListView.ShowItemToolTips = True
        Me.ReportDataListView.Size = New System.Drawing.Size(928, 513)
        Me.ReportDataListView.SortGroupItemsByPrimaryColumn = False
        Me.ReportDataListView.SpaceBetweenGroups = 5
        Me.ReportDataListView.TabIndex = 1
        Me.ReportDataListView.UnfocusedSelectedBackColor = System.Drawing.Color.PaleGreen
        Me.ReportDataListView.UnfocusedSelectedForeColor = System.Drawing.Color.Black
        Me.ReportDataListView.UseCellFormatEvents = True
        Me.ReportDataListView.UseCompatibleStateImageBehavior = False
        Me.ReportDataListView.UseFilterIndicator = True
        Me.ReportDataListView.UseFiltering = True
        Me.ReportDataListView.UseHotItem = True
        Me.ReportDataListView.UseNotifyPropertyChanged = True
        Me.ReportDataListView.View = System.Windows.Forms.View.Details
        '
        'OlvColumn1
        '
        Me.OlvColumn1.AspectName = "PersonDescription"
        Me.OlvColumn1.CellEditUseWholeCell = True
        Me.OlvColumn1.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn1.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn1.IsEditable = False
        Me.OlvColumn1.IsVisible = False
        Me.OlvColumn1.Text = "Kontrahentas"
        Me.OlvColumn1.Width = 24
        '
        'OlvColumn2
        '
        Me.OlvColumn2.AspectName = "Date"
        Me.OlvColumn2.AspectToStringFormat = "{0:yyyy-MM-dd}"
        Me.OlvColumn2.CellEditUseWholeCell = True
        Me.OlvColumn2.Groupable = False
        Me.OlvColumn2.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn2.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn2.IsEditable = False
        Me.OlvColumn2.Sortable = False
        Me.OlvColumn2.Text = "Data"
        Me.OlvColumn2.UseFiltering = False
        Me.OlvColumn2.Width = 114
        '
        'OlvColumn3
        '
        Me.OlvColumn3.AspectName = "DocumentNo"
        Me.OlvColumn3.CellEditUseWholeCell = True
        Me.OlvColumn3.Groupable = False
        Me.OlvColumn3.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn3.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn3.IsEditable = False
        Me.OlvColumn3.Sortable = False
        Me.OlvColumn3.Text = "Dok. Nr."
        Me.OlvColumn3.UseFiltering = False
        Me.OlvColumn3.Width = 109
        '
        'OlvColumn4
        '
        Me.OlvColumn4.AspectName = "Content"
        Me.OlvColumn4.CellEditUseWholeCell = True
        Me.OlvColumn4.Groupable = False
        Me.OlvColumn4.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn4.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn4.IsEditable = False
        Me.OlvColumn4.Sortable = False
        Me.OlvColumn4.Text = "Aprašymas"
        Me.OlvColumn4.UseFiltering = False
        Me.OlvColumn4.Width = 278
        '
        'OlvColumn5
        '
        Me.OlvColumn5.AspectName = "TransactionDebit"
        Me.OlvColumn5.AspectToStringFormat = "{0:##,0.00}"
        Me.OlvColumn5.CellEditUseWholeCell = True
        Me.OlvColumn5.Groupable = False
        Me.OlvColumn5.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn5.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn5.IsEditable = False
        Me.OlvColumn5.Sortable = False
        Me.OlvColumn5.Text = "Debetas"
        Me.OlvColumn5.UseFiltering = False
        Me.OlvColumn5.Width = 78
        '
        'OlvColumn6
        '
        Me.OlvColumn6.AspectName = "TransactionCredit"
        Me.OlvColumn6.AspectToStringFormat = "{0:##,0.00}"
        Me.OlvColumn6.CellEditUseWholeCell = True
        Me.OlvColumn6.Groupable = False
        Me.OlvColumn6.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn6.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn6.IsEditable = False
        Me.OlvColumn6.Sortable = False
        Me.OlvColumn6.Text = "Kreditas"
        Me.OlvColumn6.UseFiltering = False
        Me.OlvColumn6.Width = 77
        '
        'DebtStatementItemListBindingSource
        '
        Me.DebtStatementItemListBindingSource.DataSource = GetType(ApskaitaObjects.ActiveReports.DebtStatementItem)
        '
        'ProgressFiller1
        '
        Me.ProgressFiller1.Location = New System.Drawing.Point(167, 62)
        Me.ProgressFiller1.Name = "ProgressFiller1"
        Me.ProgressFiller1.Size = New System.Drawing.Size(26, 29)
        Me.ProgressFiller1.TabIndex = 5
        Me.ProgressFiller1.Visible = False
        '
        'ProgressFiller2
        '
        Me.ProgressFiller2.Location = New System.Drawing.Point(212, 57)
        Me.ProgressFiller2.Name = "ProgressFiller2"
        Me.ProgressFiller2.Size = New System.Drawing.Size(34, 34)
        Me.ProgressFiller2.TabIndex = 6
        Me.ProgressFiller2.Visible = False
        '
        'SignWithFacsimileCheckBox
        '
        Me.SignWithFacsimileCheckBox.AutoSize = True
        Me.TableLayoutPanel1.SetColumnSpan(Me.SignWithFacsimileCheckBox, 4)
        Me.SignWithFacsimileCheckBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SignWithFacsimileCheckBox.Location = New System.Drawing.Point(377, 30)
        Me.SignWithFacsimileCheckBox.Name = "SignWithFacsimileCheckBox"
        Me.SignWithFacsimileCheckBox.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.SignWithFacsimileCheckBox.Size = New System.Drawing.Size(136, 20)
        Me.SignWithFacsimileCheckBox.TabIndex = 4
        Me.SignWithFacsimileCheckBox.Text = "Pasirašyti Faksimile"
        Me.SignWithFacsimileCheckBox.UseVisualStyleBackColor = True
        '
        'F_DebtStatementItemList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(928, 569)
        Me.Controls.Add(Me.ReportDataListView)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.ProgressFiller2)
        Me.Controls.Add(Me.ProgressFiller1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "F_DebtStatementItemList"
        Me.ShowInTaskbar = False
        Me.Text = "Skolų suderinimo aktai"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.ReportDataListView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DebtStatementItemListBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DateFromDateTimePicker As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents DateToDateTimePicker As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents AccountAccGridComboBox As AccControlsWinForms.AccListComboBox
    Friend WithEvents RefreshButton As System.Windows.Forms.Button
    Friend WithEvents ReportDataListView As BrightIdeasSoftware.DataListView
    Friend WithEvents OlvColumn1 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn2 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn3 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn4 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn5 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn6 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents ProgressFiller1 As AccControlsWinForms.ProgressFiller
    Friend WithEvents ProgressFiller2 As AccControlsWinForms.ProgressFiller
    Friend WithEvents DebtStatementItemListBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents StatementDateTimePicker As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents SignWithFacsimileCheckBox As System.Windows.Forms.CheckBox
End Class
