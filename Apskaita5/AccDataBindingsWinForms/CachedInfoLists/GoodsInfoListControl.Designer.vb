Namespace CachedInfoLists
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class GoodsInfoListControl
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
            Me.OlvColumn2 = New BrightIdeasSoftware.OLVColumn
            Me.OlvColumn3 = New BrightIdeasSoftware.OLVColumn
            Me.OlvColumn4 = New BrightIdeasSoftware.OLVColumn
            Me.OlvColumn5 = New BrightIdeasSoftware.OLVColumn
            CType(Me.baseDataListView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'baseDataListView
            '
            Me.baseDataListView.AllColumns.Add(Me.OlvColumn1)
            Me.baseDataListView.AllColumns.Add(Me.OlvColumn2)
            Me.baseDataListView.AllColumns.Add(Me.OlvColumn3)
            Me.baseDataListView.AllColumns.Add(Me.OlvColumn5)
            Me.baseDataListView.AllColumns.Add(Me.OlvColumn4)
            Me.baseDataListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.OlvColumn1, Me.OlvColumn2, Me.OlvColumn3, Me.OlvColumn5})
            '
            'OlvColumn1
            '
            Me.OlvColumn1.AspectName = "Name"
            Me.OlvColumn1.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
            Me.OlvColumn1.Text = "Pavadinimas"
            Me.OlvColumn1.Width = 222
            '
            'OlvColumn2
            '
            Me.OlvColumn2.AspectName = "GoodsCode"
            Me.OlvColumn2.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
            Me.OlvColumn2.Text = "Kodas"
            Me.OlvColumn2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            Me.OlvColumn2.Width = 71
            '
            'OlvColumn3
            '
            Me.OlvColumn3.AspectName = "GoodsBarcode"
            Me.OlvColumn3.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
            Me.OlvColumn3.Text = "Barkodas"
            Me.OlvColumn3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            Me.OlvColumn3.Width = 97
            '
            'OlvColumn4
            '
            Me.OlvColumn4.AspectName = "ID"
            Me.OlvColumn4.DisplayIndex = 4
            Me.OlvColumn4.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
            Me.OlvColumn4.IsVisible = False
            Me.OlvColumn4.Text = "ID"
            Me.OlvColumn4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            '
            'OlvColumn5
            '
            Me.OlvColumn5.AspectName = "IsObsolete"
            Me.OlvColumn5.CheckBoxes = True
            Me.OlvColumn5.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
            Me.OlvColumn5.Text = "Istorinės"
            Me.OlvColumn5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            '
            'GoodsInfoListControl
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.Name = "GoodsInfoListControl"
            CType(Me.baseDataListView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents OlvColumn1 As BrightIdeasSoftware.OLVColumn
        Friend WithEvents OlvColumn2 As BrightIdeasSoftware.OLVColumn
        Friend WithEvents OlvColumn3 As BrightIdeasSoftware.OLVColumn
        Friend WithEvents OlvColumn5 As BrightIdeasSoftware.OLVColumn
        Friend WithEvents OlvColumn4 As BrightIdeasSoftware.OLVColumn

    End Class
End Namespace