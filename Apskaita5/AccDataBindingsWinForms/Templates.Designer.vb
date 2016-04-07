<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Templates
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
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.EditableDataListView = New BrightIdeasSoftware.DataListView
        Me.ReportDataListView = New BrightIdeasSoftware.DataListView
        CType(Me.EditableDataListView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ReportDataListView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(110, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Editable DataListView"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 166)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(104, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Report DataListView"
        '
        'EditableDataListView
        '
        Me.EditableDataListView.AllowColumnReorder = True
        Me.EditableDataListView.AutoGenerateColumns = False
        Me.EditableDataListView.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.SingleClickAlways
        Me.EditableDataListView.CellEditEnterChangesRows = True
        Me.EditableDataListView.CellEditTabChangesRows = True
        Me.EditableDataListView.Cursor = System.Windows.Forms.Cursors.Default
        Me.EditableDataListView.DataSource = Nothing
        Me.EditableDataListView.FullRowSelect = True
        Me.EditableDataListView.HasCollapsibleGroups = False
        Me.EditableDataListView.HeaderUsesThemes = True
        Me.EditableDataListView.HeaderWordWrap = True
        Me.EditableDataListView.HideSelection = False
        Me.EditableDataListView.HighlightBackgroundColor = System.Drawing.Color.PaleGreen
        Me.EditableDataListView.HighlightForegroundColor = System.Drawing.Color.Black
        Me.EditableDataListView.IncludeColumnHeadersInCopy = True
        Me.EditableDataListView.Location = New System.Drawing.Point(15, 25)
        Me.EditableDataListView.Name = "EditableDataListView"
        Me.EditableDataListView.RenderNonEditableCheckboxesAsDisabled = True
        Me.EditableDataListView.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.Submenu
        Me.EditableDataListView.SelectedBackColor = System.Drawing.Color.PaleGreen
        Me.EditableDataListView.SelectedForeColor = System.Drawing.Color.Black
        Me.EditableDataListView.ShowCommandMenuOnRightClick = True
        Me.EditableDataListView.ShowGroups = False
        Me.EditableDataListView.ShowImagesOnSubItems = True
        Me.EditableDataListView.ShowItemCountOnGroups = True
        Me.EditableDataListView.ShowItemToolTips = True
        Me.EditableDataListView.Size = New System.Drawing.Size(700, 123)
        Me.EditableDataListView.TabIndex = 2
        Me.EditableDataListView.UnfocusedSelectedBackColor = System.Drawing.Color.PaleGreen
        Me.EditableDataListView.UnfocusedSelectedForeColor = System.Drawing.Color.Black
        Me.EditableDataListView.UseCellFormatEvents = True
        Me.EditableDataListView.UseCompatibleStateImageBehavior = False
        Me.EditableDataListView.UseFilterIndicator = True
        Me.EditableDataListView.UseFiltering = True
        Me.EditableDataListView.UseHotItem = True
        Me.EditableDataListView.UseNotifyPropertyChanged = True
        Me.EditableDataListView.View = System.Windows.Forms.View.Details
        '
        'ReportDataListView
        '
        Me.ReportDataListView.AllowColumnReorder = True
        Me.ReportDataListView.AutoGenerateColumns = False
        Me.ReportDataListView.CausesValidation = False
        Me.ReportDataListView.Cursor = System.Windows.Forms.Cursors.Default
        Me.ReportDataListView.DataSource = Nothing
        Me.ReportDataListView.FullRowSelect = True
        Me.ReportDataListView.HasCollapsibleGroups = False
        Me.ReportDataListView.HeaderUsesThemes = True
        Me.ReportDataListView.HeaderWordWrap = True
        Me.ReportDataListView.HideSelection = False
        Me.ReportDataListView.HighlightBackgroundColor = System.Drawing.Color.PaleGreen
        Me.ReportDataListView.HighlightForegroundColor = System.Drawing.Color.Black
        Me.ReportDataListView.IncludeColumnHeadersInCopy = True
        Me.ReportDataListView.Location = New System.Drawing.Point(15, 182)
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
        Me.ReportDataListView.Size = New System.Drawing.Size(700, 123)
        Me.ReportDataListView.TabIndex = 3
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
        'Templates
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(747, 442)
        Me.Controls.Add(Me.ReportDataListView)
        Me.Controls.Add(Me.EditableDataListView)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "Templates"
        Me.Text = "Templates"
        CType(Me.EditableDataListView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ReportDataListView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents EditableDataListView As BrightIdeasSoftware.DataListView
    Friend WithEvents ReportDataListView As BrightIdeasSoftware.DataListView
End Class
