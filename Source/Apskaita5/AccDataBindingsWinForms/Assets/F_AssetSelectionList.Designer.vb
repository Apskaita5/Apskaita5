<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Friend Class F_AssetSelectionList
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
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.nCancelButton = New System.Windows.Forms.Button
        Me.nOkButton = New System.Windows.Forms.Button
        Me.LongTermAssetInfoListBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.LongTermAssetInfoListDataListView = New BrightIdeasSoftware.DataListView
        Me.OlvColumn6 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn4 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn2 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn3 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn5 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn7 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn8 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn9 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn10 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn1 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn11 = New BrightIdeasSoftware.OLVColumn
        Me.ProgressFiller1 = New AccControlsWinForms.ProgressFiller
        Me.Panel2.SuspendLayout()
        CType(Me.LongTermAssetInfoListBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LongTermAssetInfoListDataListView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.AutoSize = True
        Me.Panel2.Controls.Add(Me.nCancelButton)
        Me.Panel2.Controls.Add(Me.nOkButton)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 220)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(0, 0, 0, 4)
        Me.Panel2.Size = New System.Drawing.Size(694, 37)
        Me.Panel2.TabIndex = 4
        '
        'nCancelButton
        '
        Me.nCancelButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.nCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.nCancelButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nCancelButton.Location = New System.Drawing.Point(607, 7)
        Me.nCancelButton.Name = "nCancelButton"
        Me.nCancelButton.Size = New System.Drawing.Size(75, 23)
        Me.nCancelButton.TabIndex = 2
        Me.nCancelButton.Text = "Atšaukti"
        Me.nCancelButton.UseVisualStyleBackColor = True
        '
        'nOkButton
        '
        Me.nOkButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.nOkButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nOkButton.Location = New System.Drawing.Point(517, 7)
        Me.nOkButton.Name = "nOkButton"
        Me.nOkButton.Size = New System.Drawing.Size(75, 23)
        Me.nOkButton.TabIndex = 0
        Me.nOkButton.Text = "OK"
        Me.nOkButton.UseVisualStyleBackColor = True
        '
        'LongTermAssetInfoListBindingSource
        '
        Me.LongTermAssetInfoListBindingSource.DataSource = GetType(ApskaitaObjects.ActiveReports.LongTermAssetInfo)
        '
        'LongTermAssetInfoListDataListView
        '
        Me.LongTermAssetInfoListDataListView.AllColumns.Add(Me.OlvColumn6)
        Me.LongTermAssetInfoListDataListView.AllColumns.Add(Me.OlvColumn4)
        Me.LongTermAssetInfoListDataListView.AllColumns.Add(Me.OlvColumn2)
        Me.LongTermAssetInfoListDataListView.AllColumns.Add(Me.OlvColumn3)
        Me.LongTermAssetInfoListDataListView.AllColumns.Add(Me.OlvColumn5)
        Me.LongTermAssetInfoListDataListView.AllColumns.Add(Me.OlvColumn7)
        Me.LongTermAssetInfoListDataListView.AllColumns.Add(Me.OlvColumn8)
        Me.LongTermAssetInfoListDataListView.AllColumns.Add(Me.OlvColumn9)
        Me.LongTermAssetInfoListDataListView.AllColumns.Add(Me.OlvColumn10)
        Me.LongTermAssetInfoListDataListView.AllColumns.Add(Me.OlvColumn1)
        Me.LongTermAssetInfoListDataListView.AllColumns.Add(Me.OlvColumn11)
        Me.LongTermAssetInfoListDataListView.AllowColumnReorder = True
        Me.LongTermAssetInfoListDataListView.AutoGenerateColumns = False
        Me.LongTermAssetInfoListDataListView.CausesValidation = False
        Me.LongTermAssetInfoListDataListView.CellEditUseWholeCell = False
        Me.LongTermAssetInfoListDataListView.CheckBoxes = True
        Me.LongTermAssetInfoListDataListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.OlvColumn6, Me.OlvColumn4, Me.OlvColumn7, Me.OlvColumn10, Me.OlvColumn1, Me.OlvColumn11})
        Me.LongTermAssetInfoListDataListView.Cursor = System.Windows.Forms.Cursors.Default
        Me.LongTermAssetInfoListDataListView.DataSource = Me.LongTermAssetInfoListBindingSource
        Me.LongTermAssetInfoListDataListView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LongTermAssetInfoListDataListView.FullRowSelect = True
        Me.LongTermAssetInfoListDataListView.HasCollapsibleGroups = False
        Me.LongTermAssetInfoListDataListView.HeaderWordWrap = True
        Me.LongTermAssetInfoListDataListView.HideSelection = False
        Me.LongTermAssetInfoListDataListView.IncludeColumnHeadersInCopy = True
        Me.LongTermAssetInfoListDataListView.Location = New System.Drawing.Point(0, 0)
        Me.LongTermAssetInfoListDataListView.Name = "LongTermAssetInfoListDataListView"
        Me.LongTermAssetInfoListDataListView.RenderNonEditableCheckboxesAsDisabled = True
        Me.LongTermAssetInfoListDataListView.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.Submenu
        Me.LongTermAssetInfoListDataListView.SelectedBackColor = System.Drawing.Color.PaleGreen
        Me.LongTermAssetInfoListDataListView.SelectedForeColor = System.Drawing.Color.Black
        Me.LongTermAssetInfoListDataListView.ShowCommandMenuOnRightClick = True
        Me.LongTermAssetInfoListDataListView.ShowGroups = False
        Me.LongTermAssetInfoListDataListView.ShowImagesOnSubItems = True
        Me.LongTermAssetInfoListDataListView.ShowItemCountOnGroups = True
        Me.LongTermAssetInfoListDataListView.ShowItemToolTips = True
        Me.LongTermAssetInfoListDataListView.Size = New System.Drawing.Size(694, 220)
        Me.LongTermAssetInfoListDataListView.TabIndex = 5
        Me.LongTermAssetInfoListDataListView.UnfocusedSelectedBackColor = System.Drawing.Color.PaleGreen
        Me.LongTermAssetInfoListDataListView.UnfocusedSelectedForeColor = System.Drawing.Color.Black
        Me.LongTermAssetInfoListDataListView.UseCellFormatEvents = True
        Me.LongTermAssetInfoListDataListView.UseCompatibleStateImageBehavior = False
        Me.LongTermAssetInfoListDataListView.UseFilterIndicator = True
        Me.LongTermAssetInfoListDataListView.UseFiltering = True
        Me.LongTermAssetInfoListDataListView.UseHotItem = True
        Me.LongTermAssetInfoListDataListView.UseNotifyPropertyChanged = True
        Me.LongTermAssetInfoListDataListView.View = System.Windows.Forms.View.Details
        '
        'OlvColumn6
        '
        Me.OlvColumn6.AspectName = "Name"
        Me.OlvColumn6.CellEditUseWholeCell = True
        Me.OlvColumn6.HeaderCheckBox = True
        Me.OlvColumn6.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn6.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn6.IsEditable = False
        Me.OlvColumn6.Text = "Pavadinimas"
        Me.OlvColumn6.ToolTipText = ""
        Me.OlvColumn6.Width = 200
        '
        'OlvColumn4
        '
        Me.OlvColumn4.AspectName = "AcquisitionDate"
        Me.OlvColumn4.AspectToStringFormat = "{0:d}"
        Me.OlvColumn4.CellEditUseWholeCell = True
        Me.OlvColumn4.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn4.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn4.IsEditable = False
        Me.OlvColumn4.Text = "Įsigijimo Data"
        Me.OlvColumn4.ToolTipText = ""
        Me.OlvColumn4.Width = 100
        '
        'OlvColumn2
        '
        Me.OlvColumn2.AspectName = "ID"
        Me.OlvColumn2.CellEditUseWholeCell = True
        Me.OlvColumn2.DisplayIndex = 2
        Me.OlvColumn2.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn2.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn2.IsEditable = False
        Me.OlvColumn2.IsVisible = False
        Me.OlvColumn2.Text = "ID"
        Me.OlvColumn2.ToolTipText = ""
        Me.OlvColumn2.Width = 100
        '
        'OlvColumn3
        '
        Me.OlvColumn3.AspectName = "AcquisitionJournalEntryID"
        Me.OlvColumn3.CellEditUseWholeCell = True
        Me.OlvColumn3.DisplayIndex = 3
        Me.OlvColumn3.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn3.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn3.IsEditable = False
        Me.OlvColumn3.IsVisible = False
        Me.OlvColumn3.Text = "Įsigijimo BŽ ID"
        Me.OlvColumn3.ToolTipText = ""
        Me.OlvColumn3.Width = 100
        '
        'OlvColumn5
        '
        Me.OlvColumn5.AspectName = "AcquisitionJournalEntryDocNumber"
        Me.OlvColumn5.CellEditUseWholeCell = True
        Me.OlvColumn5.DisplayIndex = 4
        Me.OlvColumn5.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn5.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn5.IsEditable = False
        Me.OlvColumn5.IsVisible = False
        Me.OlvColumn5.Text = "Įsigijimo Dok. Nr."
        Me.OlvColumn5.ToolTipText = ""
        Me.OlvColumn5.Width = 100
        '
        'OlvColumn7
        '
        Me.OlvColumn7.AspectName = "InventoryNumber"
        Me.OlvColumn7.CellEditUseWholeCell = True
        Me.OlvColumn7.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn7.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn7.IsEditable = False
        Me.OlvColumn7.Text = "Inv. Nr."
        Me.OlvColumn7.ToolTipText = ""
        Me.OlvColumn7.Width = 89
        '
        'OlvColumn8
        '
        Me.OlvColumn8.AspectName = "CustomGroup"
        Me.OlvColumn8.CellEditUseWholeCell = True
        Me.OlvColumn8.DisplayIndex = 3
        Me.OlvColumn8.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn8.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn8.IsEditable = False
        Me.OlvColumn8.IsVisible = False
        Me.OlvColumn8.Text = "Grupė"
        Me.OlvColumn8.ToolTipText = ""
        Me.OlvColumn8.Width = 200
        '
        'OlvColumn9
        '
        Me.OlvColumn9.AspectName = "LegalGroup"
        Me.OlvColumn9.CellEditUseWholeCell = True
        Me.OlvColumn9.DisplayIndex = 4
        Me.OlvColumn9.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn9.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn9.IsEditable = False
        Me.OlvColumn9.IsVisible = False
        Me.OlvColumn9.Text = "Grupė Pgl. Įstatymą"
        Me.OlvColumn9.ToolTipText = ""
        Me.OlvColumn9.Width = 100
        '
        'OlvColumn10
        '
        Me.OlvColumn10.AspectName = "AfterAmmount"
        Me.OlvColumn10.CellEditUseWholeCell = True
        Me.OlvColumn10.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn10.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn10.IsEditable = False
        Me.OlvColumn10.Text = "Kiekis"
        Me.OlvColumn10.ToolTipText = ""
        Me.OlvColumn10.Width = 50
        '
        'OlvColumn1
        '
        Me.OlvColumn1.AspectName = "AfterValuePerUnit"
        Me.OlvColumn1.AspectToStringFormat = "{0:##,0.0000}"
        Me.OlvColumn1.Text = "Vnt. Balansinė Vertė"
        Me.OlvColumn1.Width = 78
        '
        'OlvColumn11
        '
        Me.OlvColumn11.AspectName = "AfterValue"
        Me.OlvColumn11.AspectToStringFormat = "{0:##,0.00}"
        Me.OlvColumn11.Text = "Viso Balansinė Vertė"
        Me.OlvColumn11.Width = 77
        '
        'ProgressFiller1
        '
        Me.ProgressFiller1.Location = New System.Drawing.Point(184, 14)
        Me.ProgressFiller1.Name = "ProgressFiller1"
        Me.ProgressFiller1.Size = New System.Drawing.Size(167, 63)
        Me.ProgressFiller1.TabIndex = 6
        Me.ProgressFiller1.Visible = False
        '
        'F_AssetSelectionList
        '
        Me.AcceptButton = Me.nOkButton
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.nCancelButton
        Me.CausesValidation = False
        Me.ClientSize = New System.Drawing.Size(694, 257)
        Me.ControlBox = False
        Me.Controls.Add(Me.LongTermAssetInfoListDataListView)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.ProgressFiller1)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "F_AssetSelectionList"
        Me.ShowIcon = False
        Me.Text = "Pasirinkite norimą ilgalaikį turtą"
        Me.Panel2.ResumeLayout(False)
        CType(Me.LongTermAssetInfoListBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LongTermAssetInfoListDataListView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents nCancelButton As System.Windows.Forms.Button
    Friend WithEvents nOkButton As System.Windows.Forms.Button
    Friend WithEvents LongTermAssetInfoListBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents LongTermAssetInfoListDataListView As BrightIdeasSoftware.DataListView
    Friend WithEvents OlvColumn2 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn3 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn4 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn5 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn6 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn7 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn8 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn9 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn10 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents ProgressFiller1 As AccControlsWinForms.ProgressFiller
    Friend WithEvents OlvColumn1 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn11 As BrightIdeasSoftware.OLVColumn
End Class
