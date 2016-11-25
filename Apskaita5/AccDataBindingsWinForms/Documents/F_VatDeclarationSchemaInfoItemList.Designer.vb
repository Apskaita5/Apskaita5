<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F_VatDeclarationSchemaInfoItemList
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(F_VatDeclarationSchemaInfoItemList))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.OpenFileButton = New System.Windows.Forms.Button
        Me.SaveFileButton = New System.Windows.Forms.Button
        Me.NewObjectButton = New System.Windows.Forms.Button
        Me.RefreshButton = New System.Windows.Forms.Button
        Me.VatDeclarationSchemaInfoItemListBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.VatDeclarationSchemaInfoItemListDataListView = New BrightIdeasSoftware.DataListView
        Me.OlvColumn2 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn1 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn3 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn4 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn7 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn8 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn5 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn9 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn10 = New BrightIdeasSoftware.OLVColumn
        Me.ProgressFiller1 = New AccControlsWinForms.ProgressFiller
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ChangeItem_MenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.DeleteItem_MenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ProgressFiller2 = New AccControlsWinForms.ProgressFiller
        Me.Panel1.SuspendLayout()
        CType(Me.VatDeclarationSchemaInfoItemListBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.VatDeclarationSchemaInfoItemListDataListView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.AutoSize = True
        Me.Panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel1.Controls.Add(Me.OpenFileButton)
        Me.Panel1.Controls.Add(Me.SaveFileButton)
        Me.Panel1.Controls.Add(Me.NewObjectButton)
        Me.Panel1.Controls.Add(Me.RefreshButton)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Padding = New System.Windows.Forms.Padding(0, 0, 0, 3)
        Me.Panel1.Size = New System.Drawing.Size(968, 46)
        Me.Panel1.TabIndex = 1
        '
        'OpenFileButton
        '
        Me.OpenFileButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.OpenFileButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OpenFileButton.Image = Global.AccDataBindingsWinForms.My.Resources.Resources.folder_open_icon_24p
        Me.OpenFileButton.Location = New System.Drawing.Point(771, 7)
        Me.OpenFileButton.Name = "OpenFileButton"
        Me.OpenFileButton.Size = New System.Drawing.Size(33, 33)
        Me.OpenFileButton.TabIndex = 9
        Me.OpenFileButton.UseVisualStyleBackColor = True
        '
        'SaveFileButton
        '
        Me.SaveFileButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SaveFileButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SaveFileButton.Image = Global.AccDataBindingsWinForms.My.Resources.Resources.Actions_document_save_icon_24p
        Me.SaveFileButton.Location = New System.Drawing.Point(824, 7)
        Me.SaveFileButton.Name = "SaveFileButton"
        Me.SaveFileButton.Size = New System.Drawing.Size(33, 33)
        Me.SaveFileButton.TabIndex = 8
        Me.SaveFileButton.UseVisualStyleBackColor = True
        '
        'NewObjectButton
        '
        Me.NewObjectButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NewObjectButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NewObjectButton.Image = Global.AccDataBindingsWinForms.My.Resources.Resources.Action_file_new_icon_24p
        Me.NewObjectButton.Location = New System.Drawing.Point(875, 7)
        Me.NewObjectButton.Name = "NewObjectButton"
        Me.NewObjectButton.Size = New System.Drawing.Size(33, 33)
        Me.NewObjectButton.TabIndex = 7
        Me.NewObjectButton.UseVisualStyleBackColor = True
        '
        'RefreshButton
        '
        Me.RefreshButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RefreshButton.Image = Global.AccDataBindingsWinForms.My.Resources.Resources.Button_Reload_icon_24p
        Me.RefreshButton.Location = New System.Drawing.Point(923, 7)
        Me.RefreshButton.Name = "RefreshButton"
        Me.RefreshButton.Size = New System.Drawing.Size(33, 33)
        Me.RefreshButton.TabIndex = 6
        Me.RefreshButton.UseVisualStyleBackColor = True
        '
        'VatDeclarationSchemaInfoItemListBindingSource
        '
        Me.VatDeclarationSchemaInfoItemListBindingSource.DataSource = GetType(ApskaitaObjects.ActiveReports.VatDeclarationSchemaInfoItem)
        '
        'VatDeclarationSchemaInfoItemListDataListView
        '
        Me.VatDeclarationSchemaInfoItemListDataListView.AllColumns.Add(Me.OlvColumn2)
        Me.VatDeclarationSchemaInfoItemListDataListView.AllColumns.Add(Me.OlvColumn1)
        Me.VatDeclarationSchemaInfoItemListDataListView.AllColumns.Add(Me.OlvColumn3)
        Me.VatDeclarationSchemaInfoItemListDataListView.AllColumns.Add(Me.OlvColumn4)
        Me.VatDeclarationSchemaInfoItemListDataListView.AllColumns.Add(Me.OlvColumn10)
        Me.VatDeclarationSchemaInfoItemListDataListView.AllColumns.Add(Me.OlvColumn9)
        Me.VatDeclarationSchemaInfoItemListDataListView.AllColumns.Add(Me.OlvColumn7)
        Me.VatDeclarationSchemaInfoItemListDataListView.AllColumns.Add(Me.OlvColumn8)
        Me.VatDeclarationSchemaInfoItemListDataListView.AllColumns.Add(Me.OlvColumn5)
        Me.VatDeclarationSchemaInfoItemListDataListView.AllowColumnReorder = True
        Me.VatDeclarationSchemaInfoItemListDataListView.AutoGenerateColumns = False
        Me.VatDeclarationSchemaInfoItemListDataListView.CausesValidation = False
        Me.VatDeclarationSchemaInfoItemListDataListView.CellEditUseWholeCell = False
        Me.VatDeclarationSchemaInfoItemListDataListView.CheckBoxes = True
        Me.VatDeclarationSchemaInfoItemListDataListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.OlvColumn2, Me.OlvColumn3, Me.OlvColumn4, Me.OlvColumn10, Me.OlvColumn9, Me.OlvColumn7, Me.OlvColumn8, Me.OlvColumn5})
        Me.VatDeclarationSchemaInfoItemListDataListView.Cursor = System.Windows.Forms.Cursors.Default
        Me.VatDeclarationSchemaInfoItemListDataListView.DataSource = Me.VatDeclarationSchemaInfoItemListBindingSource
        Me.VatDeclarationSchemaInfoItemListDataListView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.VatDeclarationSchemaInfoItemListDataListView.FullRowSelect = True
        Me.VatDeclarationSchemaInfoItemListDataListView.HasCollapsibleGroups = False
        Me.VatDeclarationSchemaInfoItemListDataListView.HeaderWordWrap = True
        Me.VatDeclarationSchemaInfoItemListDataListView.HideSelection = False
        Me.VatDeclarationSchemaInfoItemListDataListView.HighlightBackgroundColor = System.Drawing.Color.PaleGreen
        Me.VatDeclarationSchemaInfoItemListDataListView.HighlightForegroundColor = System.Drawing.Color.Black
        Me.VatDeclarationSchemaInfoItemListDataListView.IncludeColumnHeadersInCopy = True
        Me.VatDeclarationSchemaInfoItemListDataListView.Location = New System.Drawing.Point(0, 46)
        Me.VatDeclarationSchemaInfoItemListDataListView.Name = "VatDeclarationSchemaInfoItemListDataListView"
        Me.VatDeclarationSchemaInfoItemListDataListView.RenderNonEditableCheckboxesAsDisabled = True
        Me.VatDeclarationSchemaInfoItemListDataListView.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.Submenu
        Me.VatDeclarationSchemaInfoItemListDataListView.SelectedBackColor = System.Drawing.Color.PaleGreen
        Me.VatDeclarationSchemaInfoItemListDataListView.SelectedForeColor = System.Drawing.Color.Black
        Me.VatDeclarationSchemaInfoItemListDataListView.ShowCommandMenuOnRightClick = True
        Me.VatDeclarationSchemaInfoItemListDataListView.ShowGroups = False
        Me.VatDeclarationSchemaInfoItemListDataListView.ShowImagesOnSubItems = True
        Me.VatDeclarationSchemaInfoItemListDataListView.ShowItemCountOnGroups = True
        Me.VatDeclarationSchemaInfoItemListDataListView.ShowItemToolTips = True
        Me.VatDeclarationSchemaInfoItemListDataListView.Size = New System.Drawing.Size(968, 317)
        Me.VatDeclarationSchemaInfoItemListDataListView.TabIndex = 4
        Me.VatDeclarationSchemaInfoItemListDataListView.UnfocusedSelectedBackColor = System.Drawing.Color.PaleGreen
        Me.VatDeclarationSchemaInfoItemListDataListView.UnfocusedSelectedForeColor = System.Drawing.Color.Black
        Me.VatDeclarationSchemaInfoItemListDataListView.UseCellFormatEvents = True
        Me.VatDeclarationSchemaInfoItemListDataListView.UseCompatibleStateImageBehavior = False
        Me.VatDeclarationSchemaInfoItemListDataListView.UseFilterIndicator = True
        Me.VatDeclarationSchemaInfoItemListDataListView.UseFiltering = True
        Me.VatDeclarationSchemaInfoItemListDataListView.UseHotItem = True
        Me.VatDeclarationSchemaInfoItemListDataListView.UseNotifyPropertyChanged = True
        Me.VatDeclarationSchemaInfoItemListDataListView.View = System.Windows.Forms.View.Details
        '
        'OlvColumn2
        '
        Me.OlvColumn2.AspectName = "Name"
        Me.OlvColumn2.CellEditUseWholeCell = True
        Me.OlvColumn2.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn2.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn2.IsEditable = False
        Me.OlvColumn2.Text = "Pavadinimas"
        Me.OlvColumn2.Width = 174
        '
        'OlvColumn1
        '
        Me.OlvColumn1.AspectName = "ID"
        Me.OlvColumn1.CellEditUseWholeCell = True
        Me.OlvColumn1.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn1.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn1.IsEditable = False
        Me.OlvColumn1.IsVisible = False
        Me.OlvColumn1.Text = "ID"
        Me.OlvColumn1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn1.Width = 50
        '
        'OlvColumn3
        '
        Me.OlvColumn3.AspectName = "Description"
        Me.OlvColumn3.CellEditUseWholeCell = True
        Me.OlvColumn3.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn3.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn3.IsEditable = False
        Me.OlvColumn3.Text = "Aprašymas"
        Me.OlvColumn3.Width = 497
        '
        'OlvColumn4
        '
        Me.OlvColumn4.AspectName = "VatRate"
        Me.OlvColumn4.CellEditUseWholeCell = True
        Me.OlvColumn4.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn4.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn4.IsEditable = False
        Me.OlvColumn4.Text = "Tarifas (%)"
        Me.OlvColumn4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn4.Width = 64
        '
        'OlvColumn7
        '
        Me.OlvColumn7.AspectName = "TradedTypeHumanReadable"
        Me.OlvColumn7.CellEditUseWholeCell = True
        Me.OlvColumn7.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn7.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn7.IsEditable = False
        Me.OlvColumn7.Text = "Apyvartos Tipas"
        Me.OlvColumn7.Width = 100
        '
        'OlvColumn8
        '
        Me.OlvColumn8.AspectName = "ExternalCode"
        Me.OlvColumn8.CellEditUseWholeCell = True
        Me.OlvColumn8.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn8.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn8.IsEditable = False
        Me.OlvColumn8.Text = "Išorinis Kodas"
        Me.OlvColumn8.Width = 100
        '
        'OlvColumn5
        '
        Me.OlvColumn5.AspectName = "IsObsolete"
        Me.OlvColumn5.CellEditUseWholeCell = True
        Me.OlvColumn5.CheckBoxes = True
        Me.OlvColumn5.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn5.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn5.IsEditable = False
        Me.OlvColumn5.IsHeaderVertical = True
        Me.OlvColumn5.Text = "Istorinė"
        Me.OlvColumn5.Width = 29
        '
        'OlvColumn9
        '
        Me.OlvColumn9.AspectName = "TaxCode"
        Me.OlvColumn9.CellEditUseWholeCell = True
        Me.OlvColumn9.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn9.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn9.IsEditable = False
        Me.OlvColumn9.Text = "PVM Kodas"
        Me.OlvColumn9.Width = 100
        '
        'OlvColumn10
        '
        Me.OlvColumn10.AspectName = "VatRateIsNull"
        Me.OlvColumn10.CellEditUseWholeCell = True
        Me.OlvColumn10.CheckBoxes = True
        Me.OlvColumn10.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn10.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn10.IsEditable = False
        Me.OlvColumn10.IsHeaderVertical = True
        Me.OlvColumn10.Text = "Be PVM Tarifo"
        Me.OlvColumn10.Width = 34
        '
        'ProgressFiller1
        '
        Me.ProgressFiller1.Location = New System.Drawing.Point(177, 74)
        Me.ProgressFiller1.Name = "ProgressFiller1"
        Me.ProgressFiller1.Size = New System.Drawing.Size(167, 50)
        Me.ProgressFiller1.TabIndex = 5
        Me.ProgressFiller1.Visible = False
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
        'ProgressFiller2
        '
        Me.ProgressFiller2.Location = New System.Drawing.Point(401, 72)
        Me.ProgressFiller2.Name = "ProgressFiller2"
        Me.ProgressFiller2.Size = New System.Drawing.Size(211, 51)
        Me.ProgressFiller2.TabIndex = 6
        Me.ProgressFiller2.Visible = False
        '
        'F_VatDeclarationSchemaInfoItemList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(968, 363)
        Me.Controls.Add(Me.VatDeclarationSchemaInfoItemListDataListView)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.ProgressFiller1)
        Me.Controls.Add(Me.ProgressFiller2)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "F_VatDeclarationSchemaInfoItemList"
        Me.ShowInTaskbar = False
        Me.Text = "PVM deklaravimo schemos"
        Me.Panel1.ResumeLayout(False)
        CType(Me.VatDeclarationSchemaInfoItemListBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.VatDeclarationSchemaInfoItemListDataListView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents NewObjectButton As System.Windows.Forms.Button
    Friend WithEvents RefreshButton As System.Windows.Forms.Button
    Friend WithEvents VatDeclarationSchemaInfoItemListBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents VatDeclarationSchemaInfoItemListDataListView As BrightIdeasSoftware.DataListView
    Friend WithEvents OlvColumn1 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn2 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn3 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn4 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn5 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn7 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn8 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn9 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn10 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents ProgressFiller1 As AccControlsWinForms.ProgressFiller
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ChangeItem_MenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents DeleteItem_MenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ProgressFiller2 As AccControlsWinForms.ProgressFiller
    Friend WithEvents OpenFileButton As System.Windows.Forms.Button
    Friend WithEvents SaveFileButton As System.Windows.Forms.Button
End Class
