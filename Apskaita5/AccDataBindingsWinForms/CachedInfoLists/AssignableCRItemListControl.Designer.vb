Namespace CachedInfoLists
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class AssignableCRItemListControl
        Inherits AccControlsWinForms.InfoListControl

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
            Me.OlvColumn1 = New BrightIdeasSoftware.OLVColumn
            CType(Me.baseDataListView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'baseDataListView
            '
            Me.baseDataListView.AllColumns.Add(Me.OlvColumn1)
            Me.baseDataListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.OlvColumn1})
            Me.baseDataListView.ShowFilterMenuOnRightClick = False
            Me.baseDataListView.ShowSortIndicators = False
            Me.baseDataListView.Size = New System.Drawing.Size(384, 355)
            Me.baseDataListView.Sorting = System.Windows.Forms.SortOrder.None
            '
            'OlvColumn1
            '
            Me.OlvColumn1.AspectName = "Name"
            Me.OlvColumn1.FillsFreeSpace = True
            Me.OlvColumn1.Groupable = False
            Me.OlvColumn1.IsEditable = False
            Me.OlvColumn1.Sortable = False
            Me.OlvColumn1.Text = "Finansinės Atskaitomybės Eilutė"
            Me.OlvColumn1.Width = 381
            '
            'AssignableCRItemListControl
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.Name = "AssignableCRItemListControl"
            Me.Size = New System.Drawing.Size(384, 355)
            CType(Me.baseDataListView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents OlvColumn1 As BrightIdeasSoftware.OLVColumn

    End Class
End Namespace