﻿Imports ApskaitaObjects.ActiveReports

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Friend Class F_LongTermAssetComplexDocumentInfoList
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(F_LongTermAssetComplexDocumentInfoList))
        Me.Label1 = New System.Windows.Forms.Label
        Me.RefreshButton = New System.Windows.Forms.Button
        Me.Label10 = New System.Windows.Forms.Label
        Me.LongTermAssetComplexDocumentInfoListBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ChangeItem_MenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DeleteItem_MenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.LongTermAssetComplexDocumentInfoListDataListView = New BrightIdeasSoftware.DataListView
        Me.OlvColumn2 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn1 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn3 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn4 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn5 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn6 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn7 = New BrightIdeasSoftware.OLVColumn
        Me.ProgressFiller1 = New AccControlsWinForms.ProgressFiller
        Me.ProgressFiller2 = New AccControlsWinForms.ProgressFiller
        Me.DateFromAccDatePicker = New AccControlsWinForms.AccDatePicker
        Me.DateToAccDatePicker = New AccControlsWinForms.AccDatePicker
        CType(Me.LongTermAssetComplexDocumentInfoListBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.LongTermAssetComplexDocumentInfoListDataListView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(257, 6)
        Me.Label1.Margin = New System.Windows.Forms.Padding(3, 6, 3, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(25, 13)
        Me.Label1.TabIndex = 81
        Me.Label1.Text = "Iki:"
        '
        'RefreshButton
        '
        Me.RefreshButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RefreshButton.Image = Global.AccDataBindingsWinForms.My.Resources.Resources.Button_Reload_icon_24p
        Me.RefreshButton.Location = New System.Drawing.Point(499, 0)
        Me.RefreshButton.Margin = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.RefreshButton.Name = "RefreshButton"
        Me.RefreshButton.Size = New System.Drawing.Size(35, 32)
        Me.RefreshButton.TabIndex = 2
        Me.RefreshButton.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(3, 6)
        Me.Label10.Margin = New System.Windows.Forms.Padding(3, 6, 3, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(34, 13)
        Me.Label10.TabIndex = 78
        Me.Label10.Text = "Nuo:"
        '
        'LongTermAssetComplexDocumentInfoListBindingSource
        '
        Me.LongTermAssetComplexDocumentInfoListBindingSource.DataSource = GetType(ApskaitaObjects.ActiveReports.LongTermAssetComplexDocumentInfo)
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.AutoSize = True
        Me.TableLayoutPanel1.ColumnCount = 9
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33444!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33444!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33112!))
        Me.TableLayoutPanel1.Controls.Add(Me.Label10, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.RefreshButton, 6, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 3, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.DateFromAccDatePicker, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.DateToAccDatePicker, 4, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(750, 35)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ChangeItem_MenuItem, Me.DeleteItem_MenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(108, 48)
        '
        'ChangeItem_MenuItem
        '
        Me.ChangeItem_MenuItem.Name = "ChangeItem_MenuItem"
        Me.ChangeItem_MenuItem.Size = New System.Drawing.Size(107, 22)
        Me.ChangeItem_MenuItem.Text = "Keisti"
        '
        'DeleteItem_MenuItem
        '
        Me.DeleteItem_MenuItem.Name = "DeleteItem_MenuItem"
        Me.DeleteItem_MenuItem.Size = New System.Drawing.Size(107, 22)
        Me.DeleteItem_MenuItem.Text = "Ištrinti"
        '
        'LongTermAssetComplexDocumentInfoListDataListView
        '
        Me.LongTermAssetComplexDocumentInfoListDataListView.AllColumns.Add(Me.OlvColumn2)
        Me.LongTermAssetComplexDocumentInfoListDataListView.AllColumns.Add(Me.OlvColumn1)
        Me.LongTermAssetComplexDocumentInfoListDataListView.AllColumns.Add(Me.OlvColumn3)
        Me.LongTermAssetComplexDocumentInfoListDataListView.AllColumns.Add(Me.OlvColumn4)
        Me.LongTermAssetComplexDocumentInfoListDataListView.AllColumns.Add(Me.OlvColumn5)
        Me.LongTermAssetComplexDocumentInfoListDataListView.AllColumns.Add(Me.OlvColumn6)
        Me.LongTermAssetComplexDocumentInfoListDataListView.AllColumns.Add(Me.OlvColumn7)
        Me.LongTermAssetComplexDocumentInfoListDataListView.AllowColumnReorder = True
        Me.LongTermAssetComplexDocumentInfoListDataListView.AutoGenerateColumns = False
        Me.LongTermAssetComplexDocumentInfoListDataListView.CausesValidation = False
        Me.LongTermAssetComplexDocumentInfoListDataListView.CellEditUseWholeCell = False
        Me.LongTermAssetComplexDocumentInfoListDataListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.OlvColumn2, Me.OlvColumn3, Me.OlvColumn4, Me.OlvColumn5, Me.OlvColumn7})
        Me.LongTermAssetComplexDocumentInfoListDataListView.Cursor = System.Windows.Forms.Cursors.Default
        Me.LongTermAssetComplexDocumentInfoListDataListView.DataSource = Me.LongTermAssetComplexDocumentInfoListBindingSource
        Me.LongTermAssetComplexDocumentInfoListDataListView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LongTermAssetComplexDocumentInfoListDataListView.FullRowSelect = True
        Me.LongTermAssetComplexDocumentInfoListDataListView.HasCollapsibleGroups = False
        Me.LongTermAssetComplexDocumentInfoListDataListView.HeaderWordWrap = True
        Me.LongTermAssetComplexDocumentInfoListDataListView.HideSelection = False
        Me.LongTermAssetComplexDocumentInfoListDataListView.IncludeColumnHeadersInCopy = True
        Me.LongTermAssetComplexDocumentInfoListDataListView.Location = New System.Drawing.Point(0, 35)
        Me.LongTermAssetComplexDocumentInfoListDataListView.Name = "LongTermAssetComplexDocumentInfoListDataListView"
        Me.LongTermAssetComplexDocumentInfoListDataListView.RenderNonEditableCheckboxesAsDisabled = True
        Me.LongTermAssetComplexDocumentInfoListDataListView.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.Submenu
        Me.LongTermAssetComplexDocumentInfoListDataListView.SelectedBackColor = System.Drawing.Color.PaleGreen
        Me.LongTermAssetComplexDocumentInfoListDataListView.SelectedForeColor = System.Drawing.Color.Black
        Me.LongTermAssetComplexDocumentInfoListDataListView.ShowCommandMenuOnRightClick = True
        Me.LongTermAssetComplexDocumentInfoListDataListView.ShowGroups = False
        Me.LongTermAssetComplexDocumentInfoListDataListView.ShowImagesOnSubItems = True
        Me.LongTermAssetComplexDocumentInfoListDataListView.ShowItemCountOnGroups = True
        Me.LongTermAssetComplexDocumentInfoListDataListView.ShowItemToolTips = True
        Me.LongTermAssetComplexDocumentInfoListDataListView.Size = New System.Drawing.Size(750, 469)
        Me.LongTermAssetComplexDocumentInfoListDataListView.TabIndex = 4
        Me.LongTermAssetComplexDocumentInfoListDataListView.UnfocusedSelectedBackColor = System.Drawing.Color.PaleGreen
        Me.LongTermAssetComplexDocumentInfoListDataListView.UnfocusedSelectedForeColor = System.Drawing.Color.Black
        Me.LongTermAssetComplexDocumentInfoListDataListView.UseCellFormatEvents = True
        Me.LongTermAssetComplexDocumentInfoListDataListView.UseCompatibleStateImageBehavior = False
        Me.LongTermAssetComplexDocumentInfoListDataListView.UseFilterIndicator = True
        Me.LongTermAssetComplexDocumentInfoListDataListView.UseFiltering = True
        Me.LongTermAssetComplexDocumentInfoListDataListView.UseHotItem = True
        Me.LongTermAssetComplexDocumentInfoListDataListView.UseNotifyPropertyChanged = True
        Me.LongTermAssetComplexDocumentInfoListDataListView.View = System.Windows.Forms.View.Details
        '
        'OlvColumn2
        '
        Me.OlvColumn2.AspectName = "Date"
        Me.OlvColumn2.AspectToStringFormat = "{0:d}"
        Me.OlvColumn2.CellEditUseWholeCell = True
        Me.OlvColumn2.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn2.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn2.IsEditable = False
        Me.OlvColumn2.Text = "Data"
        Me.OlvColumn2.ToolTipText = ""
        Me.OlvColumn2.Width = 100
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
        'OlvColumn3
        '
        Me.OlvColumn3.AspectName = "OperationType"
        Me.OlvColumn3.CellEditUseWholeCell = True
        Me.OlvColumn3.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn3.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn3.IsEditable = False
        Me.OlvColumn3.Text = "Tipas"
        Me.OlvColumn3.ToolTipText = ""
        Me.OlvColumn3.Width = 107
        '
        'OlvColumn4
        '
        Me.OlvColumn4.AspectName = "Content"
        Me.OlvColumn4.CellEditUseWholeCell = True
        Me.OlvColumn4.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn4.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn4.IsEditable = False
        Me.OlvColumn4.Text = "Turinys (aprašas)"
        Me.OlvColumn4.ToolTipText = ""
        Me.OlvColumn4.Width = 250
        '
        'OlvColumn5
        '
        Me.OlvColumn5.AspectName = "CorrespondingAccount"
        Me.OlvColumn5.CellEditUseWholeCell = True
        Me.OlvColumn5.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn5.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn5.IsEditable = False
        Me.OlvColumn5.Text = "Koresp. sąsk."
        Me.OlvColumn5.ToolTipText = ""
        Me.OlvColumn5.Width = 100
        '
        'OlvColumn6
        '
        Me.OlvColumn6.AspectName = "AttachedJournalEntryID"
        Me.OlvColumn6.CellEditUseWholeCell = True
        Me.OlvColumn6.DisplayIndex = 5
        Me.OlvColumn6.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn6.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn6.IsEditable = False
        Me.OlvColumn6.IsVisible = False
        Me.OlvColumn6.Text = "Susietos BŽ ID"
        Me.OlvColumn6.ToolTipText = ""
        Me.OlvColumn6.Width = 100
        '
        'OlvColumn7
        '
        Me.OlvColumn7.AspectName = "AttachedJournalEntry"
        Me.OlvColumn7.CellEditUseWholeCell = True
        Me.OlvColumn7.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn7.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn7.IsEditable = False
        Me.OlvColumn7.Text = "Susietas BŽ"
        Me.OlvColumn7.ToolTipText = ""
        Me.OlvColumn7.Width = 188
        '
        'ProgressFiller1
        '
        Me.ProgressFiller1.Location = New System.Drawing.Point(149, 66)
        Me.ProgressFiller1.Name = "ProgressFiller1"
        Me.ProgressFiller1.Size = New System.Drawing.Size(94, 54)
        Me.ProgressFiller1.TabIndex = 5
        Me.ProgressFiller1.Visible = False
        '
        'ProgressFiller2
        '
        Me.ProgressFiller2.Location = New System.Drawing.Point(276, 65)
        Me.ProgressFiller2.Name = "ProgressFiller2"
        Me.ProgressFiller2.Size = New System.Drawing.Size(137, 54)
        Me.ProgressFiller2.TabIndex = 6
        Me.ProgressFiller2.Visible = False
        '
        'DateFromAccDatePicker
        '
        Me.DateFromAccDatePicker.BoldedDates = Nothing
        Me.DateFromAccDatePicker.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DateFromAccDatePicker.Location = New System.Drawing.Point(43, 3)
        Me.DateFromAccDatePicker.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.DateFromAccDatePicker.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.DateFromAccDatePicker.Name = "DateFromAccDatePicker"
        Me.DateFromAccDatePicker.ReadOnly = False
        Me.DateFromAccDatePicker.ShowWeekNumbers = True
        Me.DateFromAccDatePicker.Size = New System.Drawing.Size(188, 29)
        Me.DateFromAccDatePicker.TabIndex = 0
        '
        'DateToAccDatePicker
        '
        Me.DateToAccDatePicker.BoldedDates = Nothing
        Me.DateToAccDatePicker.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DateToAccDatePicker.Location = New System.Drawing.Point(288, 3)
        Me.DateToAccDatePicker.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.DateToAccDatePicker.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.DateToAccDatePicker.Name = "DateToAccDatePicker"
        Me.DateToAccDatePicker.ReadOnly = False
        Me.DateToAccDatePicker.ShowWeekNumbers = True
        Me.DateToAccDatePicker.Size = New System.Drawing.Size(188, 29)
        Me.DateToAccDatePicker.TabIndex = 1
        '
        'F_LongTermAssetComplexDocumentInfoList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(750, 504)
        Me.Controls.Add(Me.LongTermAssetComplexDocumentInfoListDataListView)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.ProgressFiller2)
        Me.Controls.Add(Me.ProgressFiller1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "F_LongTermAssetComplexDocumentInfoList"
        Me.ShowInTaskbar = False
        Me.Text = "Kompleksiniai operacijų su ilgalaikiu turtu dokumentai"
        CType(Me.LongTermAssetComplexDocumentInfoListBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.LongTermAssetComplexDocumentInfoListDataListView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents RefreshButton As System.Windows.Forms.Button
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents LongTermAssetComplexDocumentInfoListBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ChangeItem_MenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteItem_MenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents LongTermAssetComplexDocumentInfoListDataListView As BrightIdeasSoftware.DataListView
    Friend WithEvents OlvColumn1 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn2 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn3 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn4 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn5 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn6 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn7 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents ProgressFiller1 As AccControlsWinForms.ProgressFiller
    Friend WithEvents ProgressFiller2 As AccControlsWinForms.ProgressFiller
    Friend WithEvents DateFromAccDatePicker As AccControlsWinForms.AccDatePicker
    Friend WithEvents DateToAccDatePicker As AccControlsWinForms.AccDatePicker
End Class
