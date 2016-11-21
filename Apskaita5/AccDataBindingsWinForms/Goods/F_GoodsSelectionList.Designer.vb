<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Friend Class F_GoodsSelectionList
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
        Me.GoodsInfoListBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.GoodsInfoListDataListView = New BrightIdeasSoftware.DataListView
        Me.OlvColumn3 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn2 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn4 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn5 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn6 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn7 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn8 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn9 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn10 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn11 = New BrightIdeasSoftware.OLVColumn
        Me.Panel2.SuspendLayout()
        CType(Me.GoodsInfoListBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GoodsInfoListDataListView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.AutoSize = True
        Me.Panel2.Controls.Add(Me.nCancelButton)
        Me.Panel2.Controls.Add(Me.nOkButton)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 313)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(0, 0, 0, 4)
        Me.Panel2.Size = New System.Drawing.Size(627, 37)
        Me.Panel2.TabIndex = 5
        '
        'nCancelButton
        '
        Me.nCancelButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.nCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.nCancelButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nCancelButton.Location = New System.Drawing.Point(540, 7)
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
        Me.nOkButton.Location = New System.Drawing.Point(450, 7)
        Me.nOkButton.Name = "nOkButton"
        Me.nOkButton.Size = New System.Drawing.Size(75, 23)
        Me.nOkButton.TabIndex = 0
        Me.nOkButton.Text = "OK"
        Me.nOkButton.UseVisualStyleBackColor = True
        '
        'GoodsInfoListBindingSource
        '
        Me.GoodsInfoListBindingSource.DataSource = GetType(ApskaitaObjects.HelperLists.GoodsInfo)
        '
        'GoodsInfoListDataListView
        '
        Me.GoodsInfoListDataListView.AllColumns.Add(Me.OlvColumn3)
        Me.GoodsInfoListDataListView.AllColumns.Add(Me.OlvColumn2)
        Me.GoodsInfoListDataListView.AllColumns.Add(Me.OlvColumn4)
        Me.GoodsInfoListDataListView.AllColumns.Add(Me.OlvColumn5)
        Me.GoodsInfoListDataListView.AllColumns.Add(Me.OlvColumn6)
        Me.GoodsInfoListDataListView.AllColumns.Add(Me.OlvColumn7)
        Me.GoodsInfoListDataListView.AllColumns.Add(Me.OlvColumn8)
        Me.GoodsInfoListDataListView.AllColumns.Add(Me.OlvColumn9)
        Me.GoodsInfoListDataListView.AllColumns.Add(Me.OlvColumn10)
        Me.GoodsInfoListDataListView.AllColumns.Add(Me.OlvColumn11)
        Me.GoodsInfoListDataListView.AllowColumnReorder = True
        Me.GoodsInfoListDataListView.AutoGenerateColumns = False
        Me.GoodsInfoListDataListView.CausesValidation = False
        Me.GoodsInfoListDataListView.CellEditUseWholeCell = False
        Me.GoodsInfoListDataListView.CheckBoxes = True
        Me.GoodsInfoListDataListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.OlvColumn3, Me.OlvColumn5, Me.OlvColumn6, Me.OlvColumn7, Me.OlvColumn11})
        Me.GoodsInfoListDataListView.Cursor = System.Windows.Forms.Cursors.Default
        Me.GoodsInfoListDataListView.DataSource = Me.GoodsInfoListBindingSource
        Me.GoodsInfoListDataListView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GoodsInfoListDataListView.FullRowSelect = True
        Me.GoodsInfoListDataListView.HasCollapsibleGroups = False
        Me.GoodsInfoListDataListView.HeaderWordWrap = True
        Me.GoodsInfoListDataListView.HideSelection = False
        Me.GoodsInfoListDataListView.IncludeColumnHeadersInCopy = True
        Me.GoodsInfoListDataListView.Location = New System.Drawing.Point(0, 0)
        Me.GoodsInfoListDataListView.Name = "GoodsInfoListDataListView"
        Me.GoodsInfoListDataListView.RenderNonEditableCheckboxesAsDisabled = True
        Me.GoodsInfoListDataListView.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.Submenu
        Me.GoodsInfoListDataListView.SelectedBackColor = System.Drawing.Color.PaleGreen
        Me.GoodsInfoListDataListView.SelectedForeColor = System.Drawing.Color.Black
        Me.GoodsInfoListDataListView.ShowCommandMenuOnRightClick = True
        Me.GoodsInfoListDataListView.ShowGroups = False
        Me.GoodsInfoListDataListView.ShowImagesOnSubItems = True
        Me.GoodsInfoListDataListView.ShowItemCountOnGroups = True
        Me.GoodsInfoListDataListView.ShowItemToolTips = True
        Me.GoodsInfoListDataListView.Size = New System.Drawing.Size(627, 313)
        Me.GoodsInfoListDataListView.TabIndex = 6
        Me.GoodsInfoListDataListView.UnfocusedSelectedBackColor = System.Drawing.Color.PaleGreen
        Me.GoodsInfoListDataListView.UnfocusedSelectedForeColor = System.Drawing.Color.Black
        Me.GoodsInfoListDataListView.UseCellFormatEvents = True
        Me.GoodsInfoListDataListView.UseCompatibleStateImageBehavior = False
        Me.GoodsInfoListDataListView.UseFilterIndicator = True
        Me.GoodsInfoListDataListView.UseFiltering = True
        Me.GoodsInfoListDataListView.UseHotItem = True
        Me.GoodsInfoListDataListView.UseNotifyPropertyChanged = True
        Me.GoodsInfoListDataListView.View = System.Windows.Forms.View.Details
        '
        'OlvColumn3
        '
        Me.OlvColumn3.AspectName = "Name"
        Me.OlvColumn3.CellEditUseWholeCell = True
        Me.OlvColumn3.HeaderCheckBox = True
        Me.OlvColumn3.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn3.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn3.IsEditable = False
        Me.OlvColumn3.Text = "Pavadinimas"
        Me.OlvColumn3.ToolTipText = ""
        Me.OlvColumn3.Width = 255
        '
        'OlvColumn2
        '
        Me.OlvColumn2.AspectName = "ID"
        Me.OlvColumn2.CellEditUseWholeCell = True
        Me.OlvColumn2.DisplayIndex = 1
        Me.OlvColumn2.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn2.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn2.IsEditable = False
        Me.OlvColumn2.IsVisible = False
        Me.OlvColumn2.Text = "ID"
        Me.OlvColumn2.ToolTipText = ""
        Me.OlvColumn2.Width = 100
        '
        'OlvColumn4
        '
        Me.OlvColumn4.AspectName = "MeasureUnit"
        Me.OlvColumn4.CellEditUseWholeCell = True
        Me.OlvColumn4.DisplayIndex = 2
        Me.OlvColumn4.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn4.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn4.IsEditable = False
        Me.OlvColumn4.IsVisible = False
        Me.OlvColumn4.Text = "Mato Vnt."
        Me.OlvColumn4.ToolTipText = ""
        Me.OlvColumn4.Width = 100
        '
        'OlvColumn5
        '
        Me.OlvColumn5.AspectName = "AccountingMethod"
        Me.OlvColumn5.CellEditUseWholeCell = True
        Me.OlvColumn5.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn5.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn5.IsEditable = False
        Me.OlvColumn5.Text = "Apskaitos Metodas"
        Me.OlvColumn5.ToolTipText = ""
        Me.OlvColumn5.Width = 100
        '
        'OlvColumn6
        '
        Me.OlvColumn6.AspectName = "GoodsBarcode"
        Me.OlvColumn6.CellEditUseWholeCell = True
        Me.OlvColumn6.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn6.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn6.IsEditable = False
        Me.OlvColumn6.Text = "Barkodas"
        Me.OlvColumn6.ToolTipText = ""
        Me.OlvColumn6.Width = 100
        '
        'OlvColumn7
        '
        Me.OlvColumn7.AspectName = "GoodsCode"
        Me.OlvColumn7.CellEditUseWholeCell = True
        Me.OlvColumn7.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn7.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn7.IsEditable = False
        Me.OlvColumn7.Text = "Vidinis Kodas"
        Me.OlvColumn7.ToolTipText = ""
        Me.OlvColumn7.Width = 100
        '
        'OlvColumn8
        '
        Me.OlvColumn8.AspectName = "AccountSalesNetCosts"
        Me.OlvColumn8.CellEditUseWholeCell = True
        Me.OlvColumn8.DisplayIndex = 6
        Me.OlvColumn8.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn8.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn8.IsEditable = False
        Me.OlvColumn8.IsVisible = False
        Me.OlvColumn8.Text = "Pardavimų Sąsk."
        Me.OlvColumn8.ToolTipText = ""
        Me.OlvColumn8.Width = 100
        '
        'OlvColumn9
        '
        Me.OlvColumn9.AspectName = "DefaultVatRateSales"
        Me.OlvColumn9.AspectToStringFormat = "{0:##,0.00}"
        Me.OlvColumn9.CellEditUseWholeCell = True
        Me.OlvColumn9.DisplayIndex = 7
        Me.OlvColumn9.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn9.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn9.IsEditable = False
        Me.OlvColumn9.IsVisible = False
        Me.OlvColumn9.Text = "Pardavimo PVM (%)"
        Me.OlvColumn9.ToolTipText = ""
        Me.OlvColumn9.Width = 100
        '
        'OlvColumn10
        '
        Me.OlvColumn10.AspectName = "DefaultVatRatePurchase"
        Me.OlvColumn10.AspectToStringFormat = "{0:##,0.00}"
        Me.OlvColumn10.CellEditUseWholeCell = True
        Me.OlvColumn10.DisplayIndex = 8
        Me.OlvColumn10.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn10.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn10.IsEditable = False
        Me.OlvColumn10.IsVisible = False
        Me.OlvColumn10.Text = "Pirkimo PVM (%)"
        Me.OlvColumn10.ToolTipText = ""
        Me.OlvColumn10.Width = 100
        '
        'OlvColumn11
        '
        Me.OlvColumn11.AspectName = "IsObsolete"
        Me.OlvColumn11.CellEditUseWholeCell = True
        Me.OlvColumn11.CheckBoxes = True
        Me.OlvColumn11.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn11.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn11.IsEditable = False
        Me.OlvColumn11.Text = "Nebenaud."
        Me.OlvColumn11.ToolTipText = ""
        Me.OlvColumn11.Width = 66
        '
        'F_GoodsSelectionList
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(627, 350)
        Me.Controls.Add(Me.GoodsInfoListDataListView)
        Me.Controls.Add(Me.Panel2)
        Me.MinimizeBox = False
        Me.Name = "F_GoodsSelectionList"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "Pasirinkite prekes"
        Me.Panel2.ResumeLayout(False)
        CType(Me.GoodsInfoListBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GoodsInfoListDataListView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents nCancelButton As System.Windows.Forms.Button
    Friend WithEvents nOkButton As System.Windows.Forms.Button
    Friend WithEvents GoodsInfoListBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents GoodsInfoListDataListView As BrightIdeasSoftware.DataListView
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
End Class
