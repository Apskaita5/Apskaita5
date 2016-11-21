Namespace DataAggregator
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class F_DataAggregator
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
            Me.AggregateSumListDataListView = New BrightIdeasSoftware.DataListView
            Me.OlvColumn1 = New BrightIdeasSoftware.OLVColumn
            Me.OlvColumn2 = New BrightIdeasSoftware.OLVColumn
            Me.OlvColumn3 = New BrightIdeasSoftware.OLVColumn
            Me.OlvColumn4 = New BrightIdeasSoftware.OLVColumn
            CType(Me.AggregateSumListDataListView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'AggregateSumListDataListView
            '
            Me.AggregateSumListDataListView.AllColumns.Add(Me.OlvColumn1)
            Me.AggregateSumListDataListView.AllColumns.Add(Me.OlvColumn2)
            Me.AggregateSumListDataListView.AllColumns.Add(Me.OlvColumn3)
            Me.AggregateSumListDataListView.AllColumns.Add(Me.OlvColumn4)
            Me.AggregateSumListDataListView.AllowColumnReorder = True
            Me.AggregateSumListDataListView.AutoGenerateColumns = False
            Me.AggregateSumListDataListView.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.SingleClickAlways
            Me.AggregateSumListDataListView.CellEditEnterChangesRows = True
            Me.AggregateSumListDataListView.CellEditTabChangesRows = True
            Me.AggregateSumListDataListView.CellEditUseWholeCell = False
            Me.AggregateSumListDataListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.OlvColumn1, Me.OlvColumn2, Me.OlvColumn3, Me.OlvColumn4})
            Me.AggregateSumListDataListView.Cursor = System.Windows.Forms.Cursors.Default
            Me.AggregateSumListDataListView.DataSource = Nothing
            Me.AggregateSumListDataListView.Dock = System.Windows.Forms.DockStyle.Fill
            Me.AggregateSumListDataListView.FullRowSelect = True
            Me.AggregateSumListDataListView.HasCollapsibleGroups = False
            Me.AggregateSumListDataListView.HeaderWordWrap = True
            Me.AggregateSumListDataListView.HideSelection = False
            Me.AggregateSumListDataListView.IncludeColumnHeadersInCopy = True
            Me.AggregateSumListDataListView.Location = New System.Drawing.Point(0, 0)
            Me.AggregateSumListDataListView.Name = "AggregateSumListDataListView"
            Me.AggregateSumListDataListView.RenderNonEditableCheckboxesAsDisabled = True
            Me.AggregateSumListDataListView.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.Submenu
            Me.AggregateSumListDataListView.SelectedBackColor = System.Drawing.Color.PaleGreen
            Me.AggregateSumListDataListView.SelectedForeColor = System.Drawing.Color.Black
            Me.AggregateSumListDataListView.ShowCommandMenuOnRightClick = True
            Me.AggregateSumListDataListView.ShowGroups = False
            Me.AggregateSumListDataListView.ShowImagesOnSubItems = True
            Me.AggregateSumListDataListView.ShowItemCountOnGroups = True
            Me.AggregateSumListDataListView.ShowItemToolTips = True
            Me.AggregateSumListDataListView.Size = New System.Drawing.Size(415, 335)
            Me.AggregateSumListDataListView.TabIndex = 3
            Me.AggregateSumListDataListView.UnfocusedSelectedBackColor = System.Drawing.Color.PaleGreen
            Me.AggregateSumListDataListView.UnfocusedSelectedForeColor = System.Drawing.Color.Black
            Me.AggregateSumListDataListView.UseCellFormatEvents = True
            Me.AggregateSumListDataListView.UseCompatibleStateImageBehavior = False
            Me.AggregateSumListDataListView.UseFilterIndicator = True
            Me.AggregateSumListDataListView.UseFiltering = True
            Me.AggregateSumListDataListView.UseHotItem = True
            Me.AggregateSumListDataListView.UseNotifyPropertyChanged = True
            Me.AggregateSumListDataListView.View = System.Windows.Forms.View.Details
            '
            'OlvColumn1
            '
            Me.OlvColumn1.AspectName = "Name"
            Me.OlvColumn1.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.OlvColumn1.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
            Me.OlvColumn1.IsEditable = False
            Me.OlvColumn1.Text = "Pavadinimas"
            Me.OlvColumn1.Width = 154
            '
            'OlvColumn2
            '
            Me.OlvColumn2.AspectName = "ValueString"
            Me.OlvColumn2.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.OlvColumn2.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
            Me.OlvColumn2.IsEditable = False
            Me.OlvColumn2.Text = "Vertė"
            Me.OlvColumn2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            Me.OlvColumn2.Width = 71
            '
            'OlvColumn3
            '
            Me.OlvColumn3.AspectName = "RoundOrder"
            Me.OlvColumn3.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.OlvColumn3.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
            Me.OlvColumn3.Text = "Apvalinti Skaitmenų"
            Me.OlvColumn3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            Me.OlvColumn3.Width = 74
            '
            'OlvColumn4
            '
            Me.OlvColumn4.AspectName = "AggregateFunction"
            Me.OlvColumn4.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.OlvColumn4.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
            Me.OlvColumn4.Text = "Funkcija"
            Me.OlvColumn4.Width = 110
            '
            'F_DataAggregator
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.ClientSize = New System.Drawing.Size(415, 335)
            Me.Controls.Add(Me.AggregateSumListDataListView)
            Me.Name = "F_DataAggregator"
            Me.ShowIcon = False
            Me.ShowInTaskbar = False
            Me.Text = "Duomenų agregatorius"
            CType(Me.AggregateSumListDataListView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents AggregateSumListDataListView As BrightIdeasSoftware.DataListView
        Friend WithEvents OlvColumn1 As BrightIdeasSoftware.OLVColumn
        Friend WithEvents OlvColumn2 As BrightIdeasSoftware.OLVColumn
        Friend WithEvents OlvColumn3 As BrightIdeasSoftware.OLVColumn
        Private WithEvents OlvColumn4 As BrightIdeasSoftware.OLVColumn
    End Class
End Namespace