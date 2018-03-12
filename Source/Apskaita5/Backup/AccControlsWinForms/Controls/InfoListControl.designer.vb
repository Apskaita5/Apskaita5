<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class InfoListControl
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.baseDataListView = New BrightIdeasSoftware.DataListView
        CType(Me.baseDataListView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'baseDataListView
        '
        Me.baseDataListView.AllowColumnReorder = True
        Me.baseDataListView.AutoGenerateColumns = False
        Me.baseDataListView.CausesValidation = False
        Me.baseDataListView.CellEditUseWholeCell = False
        Me.baseDataListView.Cursor = System.Windows.Forms.Cursors.Default
        Me.baseDataListView.DataSource = Nothing
        Me.baseDataListView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.baseDataListView.HasCollapsibleGroups = False
        Me.baseDataListView.HeaderWordWrap = True
        Me.baseDataListView.HideSelection = False
        Me.baseDataListView.HighlightBackgroundColor = System.Drawing.Color.Empty
        Me.baseDataListView.HighlightForegroundColor = System.Drawing.Color.Empty
        Me.baseDataListView.IncludeColumnHeadersInCopy = True
        Me.baseDataListView.IsSearchOnSortColumn = False
        Me.baseDataListView.Location = New System.Drawing.Point(0, 0)
        Me.baseDataListView.Margin = New System.Windows.Forms.Padding(0)
        Me.baseDataListView.Name = "baseDataListView"
        Me.baseDataListView.RenderNonEditableCheckboxesAsDisabled = True
        Me.baseDataListView.ShowCommandMenuOnRightClick = True
        Me.baseDataListView.ShowGroups = False
        Me.baseDataListView.Size = New System.Drawing.Size(434, 355)
        Me.baseDataListView.Sorting = System.Windows.Forms.SortOrder.Ascending
        Me.baseDataListView.TabIndex = 0
        Me.baseDataListView.UseCompatibleStateImageBehavior = False
        Me.baseDataListView.UseFilterIndicator = True
        Me.baseDataListView.UseFiltering = True
        Me.baseDataListView.UseHotItem = True
        Me.baseDataListView.UseNotifyPropertyChanged = True
        Me.baseDataListView.View = System.Windows.Forms.View.Details
        '
        'InfoListControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoValidate = System.Windows.Forms.AutoValidate.Disable
        Me.CausesValidation = False
        Me.Controls.Add(Me.baseDataListView)
        Me.Name = "InfoListControl"
        Me.Size = New System.Drawing.Size(434, 355)
        CType(Me.baseDataListView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Protected WithEvents baseDataListView As BrightIdeasSoftware.DataListView

End Class
