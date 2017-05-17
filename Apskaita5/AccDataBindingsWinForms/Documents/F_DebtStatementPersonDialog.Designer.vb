<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F_DebtStatementPersonDialog
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
        Me.ReportDataListView = New BrightIdeasSoftware.DataListView
        Me.OlvColumn2 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn3 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn5 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn4 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn6 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn1 = New BrightIdeasSoftware.OLVColumn
        Me.DebtStatementPersonBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Panel2.SuspendLayout()
        CType(Me.ReportDataListView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DebtStatementPersonBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.AutoSize = True
        Me.Panel2.Controls.Add(Me.nCancelButton)
        Me.Panel2.Controls.Add(Me.nOkButton)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 409)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(0, 0, 0, 4)
        Me.Panel2.Size = New System.Drawing.Size(442, 37)
        Me.Panel2.TabIndex = 3
        '
        'nCancelButton
        '
        Me.nCancelButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.nCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.nCancelButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nCancelButton.Location = New System.Drawing.Point(355, 7)
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
        Me.nOkButton.Location = New System.Drawing.Point(274, 7)
        Me.nOkButton.Name = "nOkButton"
        Me.nOkButton.Size = New System.Drawing.Size(75, 23)
        Me.nOkButton.TabIndex = 0
        Me.nOkButton.Text = "OK"
        Me.nOkButton.UseVisualStyleBackColor = True
        '
        'ReportDataListView
        '
        Me.ReportDataListView.AllColumns.Add(Me.OlvColumn2)
        Me.ReportDataListView.AllColumns.Add(Me.OlvColumn3)
        Me.ReportDataListView.AllColumns.Add(Me.OlvColumn5)
        Me.ReportDataListView.AllColumns.Add(Me.OlvColumn4)
        Me.ReportDataListView.AllColumns.Add(Me.OlvColumn6)
        Me.ReportDataListView.AllColumns.Add(Me.OlvColumn1)
        Me.ReportDataListView.AllowColumnReorder = True
        Me.ReportDataListView.AutoGenerateColumns = False
        Me.ReportDataListView.CausesValidation = False
        Me.ReportDataListView.CellEditUseWholeCell = False
        Me.ReportDataListView.CheckBoxes = True
        Me.ReportDataListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.OlvColumn2, Me.OlvColumn3, Me.OlvColumn6})
        Me.ReportDataListView.Cursor = System.Windows.Forms.Cursors.Default
        Me.ReportDataListView.DataSource = Me.DebtStatementPersonBindingSource
        Me.ReportDataListView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ReportDataListView.FullRowSelect = True
        Me.ReportDataListView.HasCollapsibleGroups = False
        Me.ReportDataListView.HeaderWordWrap = True
        Me.ReportDataListView.HideSelection = False
        Me.ReportDataListView.IncludeColumnHeadersInCopy = True
        Me.ReportDataListView.Location = New System.Drawing.Point(0, 0)
        Me.ReportDataListView.Name = "ReportDataListView"
        Me.ReportDataListView.RenderNonEditableCheckboxesAsDisabled = True
        Me.ReportDataListView.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.Submenu
        Me.ReportDataListView.SelectedBackColor = System.Drawing.Color.PaleGreen
        Me.ReportDataListView.SelectedForeColor = System.Drawing.Color.Black
        Me.ReportDataListView.ShowCommandMenuOnRightClick = True
        Me.ReportDataListView.ShowGroups = False
        Me.ReportDataListView.ShowImagesOnSubItems = True
        Me.ReportDataListView.ShowItemCountOnGroups = True
        Me.ReportDataListView.ShowItemToolTips = True
        Me.ReportDataListView.Size = New System.Drawing.Size(442, 409)
        Me.ReportDataListView.TabIndex = 4
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
        'OlvColumn2
        '
        Me.OlvColumn2.AspectName = "PersonName"
        Me.OlvColumn2.CellEditUseWholeCell = True
        Me.OlvColumn2.HeaderCheckBox = True
        Me.OlvColumn2.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn2.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn2.IsEditable = False
        Me.OlvColumn2.Text = "Pavadinimas"
        Me.OlvColumn2.Width = 232
        '
        'OlvColumn3
        '
        Me.OlvColumn3.AspectName = "PersonCode"
        Me.OlvColumn3.CellEditUseWholeCell = True
        Me.OlvColumn3.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn3.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn3.IsEditable = False
        Me.OlvColumn3.Text = "Kodas"
        Me.OlvColumn3.Width = 85
        '
        'OlvColumn5
        '
        Me.OlvColumn5.AspectName = "PersonVatCode"
        Me.OlvColumn5.CellEditUseWholeCell = True
        Me.OlvColumn5.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn5.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn5.IsEditable = False
        Me.OlvColumn5.IsVisible = False
        Me.OlvColumn5.Text = "PVM Kodas"
        Me.OlvColumn5.Width = 100
        '
        'OlvColumn4
        '
        Me.OlvColumn4.AspectName = "PersonAddress"
        Me.OlvColumn4.CellEditUseWholeCell = True
        Me.OlvColumn4.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn4.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn4.IsEditable = False
        Me.OlvColumn4.IsVisible = False
        Me.OlvColumn4.Text = "Adresas"
        Me.OlvColumn4.Width = 100
        '
        'OlvColumn6
        '
        Me.OlvColumn6.AspectName = "PersonEmail"
        Me.OlvColumn6.CellEditUseWholeCell = True
        Me.OlvColumn6.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn6.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn6.IsEditable = False
        Me.OlvColumn6.Text = "Epaštas"
        Me.OlvColumn6.Width = 117
        '
        'OlvColumn1
        '
        Me.OlvColumn1.AspectName = "PersonId"
        Me.OlvColumn1.CellEditUseWholeCell = True
        Me.OlvColumn1.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn1.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn1.IsEditable = False
        Me.OlvColumn1.IsVisible = False
        Me.OlvColumn1.Text = "ID"
        Me.OlvColumn1.Width = 50
        '
        'DebtStatementPersonBindingSource
        '
        Me.DebtStatementPersonBindingSource.DataSource = GetType(ApskaitaObjects.ActiveReports.DebtStatementPerson)
        '
        'F_DebtStatementPersonDialog
        '
        Me.AcceptButton = Me.nOkButton
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.nCancelButton
        Me.ClientSize = New System.Drawing.Size(442, 446)
        Me.ControlBox = False
        Me.Controls.Add(Me.ReportDataListView)
        Me.Controls.Add(Me.Panel2)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "F_DebtStatementPersonDialog"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.Text = "Pasirinkite kontrahentus"
        Me.Panel2.ResumeLayout(False)
        CType(Me.ReportDataListView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DebtStatementPersonBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents nCancelButton As System.Windows.Forms.Button
    Friend WithEvents nOkButton As System.Windows.Forms.Button
    Friend WithEvents ReportDataListView As BrightIdeasSoftware.DataListView
    Friend WithEvents OlvColumn1 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn2 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn3 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn4 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn5 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn6 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents DebtStatementPersonBindingSource As System.Windows.Forms.BindingSource
End Class
