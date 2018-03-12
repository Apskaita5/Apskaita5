<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F_ConsolidatedReport
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(F_ConsolidatedReport))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.GetNewFormButton = New System.Windows.Forms.Button
        Me.SaveInDatabaseButton = New System.Windows.Forms.Button
        Me.SaveAsFileButton = New System.Windows.Forms.Button
        Me.OpenFileButton = New System.Windows.Forms.Button
        Me.FetchFromDatabaseButton = New System.Windows.Forms.Button
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.ConsolidatedReportTreeListView = New BrightIdeasSoftware.TreeListView
        Me.OlvColumn1 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn9 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn2 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn3 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn4 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn5 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn6 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn7 = New BrightIdeasSoftware.OLVColumn
        Me.Panel1.SuspendLayout()
        CType(Me.ConsolidatedReportTreeListView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.AutoSize = True
        Me.Panel1.Controls.Add(Me.GetNewFormButton)
        Me.Panel1.Controls.Add(Me.SaveInDatabaseButton)
        Me.Panel1.Controls.Add(Me.SaveAsFileButton)
        Me.Panel1.Controls.Add(Me.OpenFileButton)
        Me.Panel1.Controls.Add(Me.FetchFromDatabaseButton)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(851, 53)
        Me.Panel1.TabIndex = 1
        '
        'GetNewFormButton
        '
        Me.GetNewFormButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GetNewFormButton.Image = Global.AccDataBindingsWinForms.My.Resources.Resources.Action_file_new_icon_24p
        Me.GetNewFormButton.Location = New System.Drawing.Point(131, 7)
        Me.GetNewFormButton.Name = "GetNewFormButton"
        Me.GetNewFormButton.Size = New System.Drawing.Size(35, 33)
        Me.GetNewFormButton.TabIndex = 5
        Me.ToolTip1.SetToolTip(Me.GetNewFormButton, "Nauja finansinės atskaitomybės struktūra")
        Me.GetNewFormButton.UseVisualStyleBackColor = True
        '
        'SaveInDatabaseButton
        '
        Me.SaveInDatabaseButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SaveInDatabaseButton.Image = Global.AccDataBindingsWinForms.My.Resources.Resources.database_save
        Me.SaveInDatabaseButton.Location = New System.Drawing.Point(61, 7)
        Me.SaveInDatabaseButton.Name = "SaveInDatabaseButton"
        Me.SaveInDatabaseButton.Size = New System.Drawing.Size(43, 43)
        Me.SaveInDatabaseButton.TabIndex = 4
        Me.ToolTip1.SetToolTip(Me.SaveInDatabaseButton, "Išsaugoti finansinės atskaitomybės struktūrą įmonės duomenų bazėje")
        Me.SaveInDatabaseButton.UseVisualStyleBackColor = True
        '
        'SaveAsFileButton
        '
        Me.SaveAsFileButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.SaveAsFileButton.Image = Global.AccDataBindingsWinForms.My.Resources.Resources.filesaveas_24x24
        Me.SaveAsFileButton.Location = New System.Drawing.Point(213, 7)
        Me.SaveAsFileButton.Name = "SaveAsFileButton"
        Me.SaveAsFileButton.Size = New System.Drawing.Size(35, 33)
        Me.SaveAsFileButton.TabIndex = 3
        Me.ToolTip1.SetToolTip(Me.SaveAsFileButton, "Išsaugoti atskaitomybės struktūrą faile")
        Me.SaveAsFileButton.UseVisualStyleBackColor = True
        '
        'OpenFileButton
        '
        Me.OpenFileButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OpenFileButton.Image = Global.AccDataBindingsWinForms.My.Resources.Resources.folder_open_icon_24p
        Me.OpenFileButton.Location = New System.Drawing.Point(172, 7)
        Me.OpenFileButton.Name = "OpenFileButton"
        Me.OpenFileButton.Size = New System.Drawing.Size(35, 33)
        Me.OpenFileButton.TabIndex = 2
        Me.ToolTip1.SetToolTip(Me.OpenFileButton, "Atidaryti finansinės atskaitomybės struktūros failą")
        Me.OpenFileButton.UseVisualStyleBackColor = True
        '
        'FetchFromDatabaseButton
        '
        Me.FetchFromDatabaseButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FetchFromDatabaseButton.Image = Global.AccDataBindingsWinForms.My.Resources.Resources.database_refresh
        Me.FetchFromDatabaseButton.Location = New System.Drawing.Point(12, 7)
        Me.FetchFromDatabaseButton.Name = "FetchFromDatabaseButton"
        Me.FetchFromDatabaseButton.Size = New System.Drawing.Size(43, 43)
        Me.FetchFromDatabaseButton.TabIndex = 1
        Me.ToolTip1.SetToolTip(Me.FetchFromDatabaseButton, "Gauti įmonės finansinės atskaitomybės struktūrą iš duomenų bazės")
        Me.FetchFromDatabaseButton.UseVisualStyleBackColor = True
        '
        'ConsolidatedReportTreeListView
        '
        Me.ConsolidatedReportTreeListView.AllColumns.Add(Me.OlvColumn1)
        Me.ConsolidatedReportTreeListView.AllColumns.Add(Me.OlvColumn9)
        Me.ConsolidatedReportTreeListView.AllColumns.Add(Me.OlvColumn2)
        Me.ConsolidatedReportTreeListView.AllColumns.Add(Me.OlvColumn3)
        Me.ConsolidatedReportTreeListView.AllColumns.Add(Me.OlvColumn4)
        Me.ConsolidatedReportTreeListView.AllColumns.Add(Me.OlvColumn5)
        Me.ConsolidatedReportTreeListView.AllColumns.Add(Me.OlvColumn6)
        Me.ConsolidatedReportTreeListView.AllColumns.Add(Me.OlvColumn7)
        Me.ConsolidatedReportTreeListView.AllowColumnReorder = True
        Me.ConsolidatedReportTreeListView.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.SingleClick
        Me.ConsolidatedReportTreeListView.CellEditEnterChangesRows = True
        Me.ConsolidatedReportTreeListView.CellEditUseWholeCell = False
        Me.ConsolidatedReportTreeListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.OlvColumn1, Me.OlvColumn9, Me.OlvColumn2, Me.OlvColumn3})
        Me.ConsolidatedReportTreeListView.CopySelectionOnControlCUsesDragSource = False
        Me.ConsolidatedReportTreeListView.Cursor = System.Windows.Forms.Cursors.Default
        Me.ConsolidatedReportTreeListView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ConsolidatedReportTreeListView.FullRowSelect = True
        Me.ConsolidatedReportTreeListView.HasCollapsibleGroups = False
        Me.ConsolidatedReportTreeListView.HeaderWordWrap = True
        Me.ConsolidatedReportTreeListView.HideSelection = False
        Me.ConsolidatedReportTreeListView.IncludeColumnHeadersInCopy = True
        Me.ConsolidatedReportTreeListView.IncludeHiddenColumnsInDataTransfer = True
        Me.ConsolidatedReportTreeListView.IsSimpleDragSource = True
        Me.ConsolidatedReportTreeListView.IsSimpleDropSink = True
        Me.ConsolidatedReportTreeListView.Location = New System.Drawing.Point(0, 53)
        Me.ConsolidatedReportTreeListView.Name = "ConsolidatedReportTreeListView"
        Me.ConsolidatedReportTreeListView.RenderNonEditableCheckboxesAsDisabled = True
        Me.ConsolidatedReportTreeListView.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.Submenu
        Me.ConsolidatedReportTreeListView.ShowCommandMenuOnRightClick = True
        Me.ConsolidatedReportTreeListView.ShowFilterMenuOnRightClick = False
        Me.ConsolidatedReportTreeListView.ShowGroups = False
        Me.ConsolidatedReportTreeListView.ShowImagesOnSubItems = True
        Me.ConsolidatedReportTreeListView.ShowItemCountOnGroups = True
        Me.ConsolidatedReportTreeListView.ShowItemToolTips = True
        Me.ConsolidatedReportTreeListView.Size = New System.Drawing.Size(851, 449)
        Me.ConsolidatedReportTreeListView.TabIndex = 2
        Me.ConsolidatedReportTreeListView.UseCellFormatEvents = True
        Me.ConsolidatedReportTreeListView.UseCompatibleStateImageBehavior = False
        Me.ConsolidatedReportTreeListView.UseFilterIndicator = True
        Me.ConsolidatedReportTreeListView.UseHotItem = True
        Me.ConsolidatedReportTreeListView.UseNotifyPropertyChanged = True
        Me.ConsolidatedReportTreeListView.UseTranslucentHotItem = True
        Me.ConsolidatedReportTreeListView.View = System.Windows.Forms.View.Details
        Me.ConsolidatedReportTreeListView.VirtualMode = True
        '
        'OlvColumn1
        '
        Me.OlvColumn1.AspectName = "VisibleIndex"
        Me.OlvColumn1.AutoCompleteEditor = False
        Me.OlvColumn1.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None
        Me.OlvColumn1.CellEditUseWholeCell = True
        Me.OlvColumn1.Groupable = False
        Me.OlvColumn1.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn1.IsHeaderVertical = True
        Me.OlvColumn1.Sortable = False
        Me.OlvColumn1.Text = "Eiliškumas"
        Me.OlvColumn1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn1.ToolTipText = "Numeruoti eilutes tokia seka, kokia norite, kad būtų matoma ""spausdintoje"" ataska" & _
            "itoje"
        Me.OlvColumn1.UseFiltering = False
        Me.OlvColumn1.Width = 93
        '
        'OlvColumn9
        '
        Me.OlvColumn9.AspectName = "DisplayedNumber"
        Me.OlvColumn9.AutoCompleteEditor = False
        Me.OlvColumn9.AutoCompleteEditorMode = System.Windows.Forms.AutoCompleteMode.None
        Me.OlvColumn9.CellEditUseWholeCell = True
        Me.OlvColumn9.Groupable = False
        Me.OlvColumn9.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn9.Sortable = False
        Me.OlvColumn9.Text = "Nr."
        Me.OlvColumn9.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        Me.OlvColumn9.UseFiltering = False
        Me.OlvColumn9.Width = 63
        '
        'OlvColumn2
        '
        Me.OlvColumn2.AspectName = "Name"
        Me.OlvColumn2.CellEditUseWholeCell = True
        Me.OlvColumn2.Groupable = False
        Me.OlvColumn2.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn2.Sortable = False
        Me.OlvColumn2.Text = "Eilutės Pavadinimas"
        Me.OlvColumn2.UseFiltering = False
        Me.OlvColumn2.Width = 469
        '
        'OlvColumn3
        '
        Me.OlvColumn3.AspectName = "IsCredit"
        Me.OlvColumn3.CheckBoxes = True
        Me.OlvColumn3.Groupable = False
        Me.OlvColumn3.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn3.IsHeaderVertical = True
        Me.OlvColumn3.Sortable = False
        Me.OlvColumn3.Text = "Kreditinis Likutis"
        Me.OlvColumn3.UseFiltering = False
        Me.OlvColumn3.Width = 54
        '
        'OlvColumn4
        '
        Me.OlvColumn4.AspectName = "HasAccountsAssigned"
        Me.OlvColumn4.CheckBoxes = True
        Me.OlvColumn4.DisplayIndex = 4
        Me.OlvColumn4.Groupable = False
        Me.OlvColumn4.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn4.IsEditable = False
        Me.OlvColumn4.IsHeaderVertical = True
        Me.OlvColumn4.IsVisible = False
        Me.OlvColumn4.Sortable = False
        Me.OlvColumn4.Text = "Priskirta Sąskaitų"
        Me.OlvColumn4.UseFiltering = False
        '
        'OlvColumn5
        '
        Me.OlvColumn5.AspectName = "ID"
        Me.OlvColumn5.Groupable = False
        Me.OlvColumn5.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn5.IsEditable = False
        Me.OlvColumn5.IsVisible = False
        Me.OlvColumn5.Sortable = False
        Me.OlvColumn5.Text = "ID"
        Me.OlvColumn5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn5.UseFiltering = False
        '
        'OlvColumn6
        '
        Me.OlvColumn6.AspectName = "Left"
        Me.OlvColumn6.Groupable = False
        Me.OlvColumn6.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn6.IsEditable = False
        Me.OlvColumn6.IsVisible = False
        Me.OlvColumn6.Sortable = False
        Me.OlvColumn6.Text = "Lft"
        Me.OlvColumn6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn6.UseFiltering = False
        '
        'OlvColumn7
        '
        Me.OlvColumn7.AspectName = "Right"
        Me.OlvColumn7.Groupable = False
        Me.OlvColumn7.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn7.IsEditable = False
        Me.OlvColumn7.IsVisible = False
        Me.OlvColumn7.Sortable = False
        Me.OlvColumn7.Text = "Rgt"
        Me.OlvColumn7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn7.UseFiltering = False
        '
        'F_ConsolidatedReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(851, 502)
        Me.Controls.Add(Me.ConsolidatedReportTreeListView)
        Me.Controls.Add(Me.Panel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "F_ConsolidatedReport"
        Me.ShowInTaskbar = False
        Me.Text = "Finansinės atskaitomybės dokumentų struktūra"
        Me.Panel1.ResumeLayout(False)
        CType(Me.ConsolidatedReportTreeListView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents GetNewFormButton As System.Windows.Forms.Button
    Friend WithEvents SaveInDatabaseButton As System.Windows.Forms.Button
    Friend WithEvents SaveAsFileButton As System.Windows.Forms.Button
    Friend WithEvents OpenFileButton As System.Windows.Forms.Button
    Friend WithEvents FetchFromDatabaseButton As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents ConsolidatedReportTreeListView As BrightIdeasSoftware.TreeListView
    Friend WithEvents OlvColumn1 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn9 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn2 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn3 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn4 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn5 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn6 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn7 As BrightIdeasSoftware.OLVColumn
End Class
