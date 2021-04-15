Namespace CachedInfoLists
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class WorkTimeClassInfoListControl
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
            CType(Me.baseDataListView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'baseDataListView
            '
            Me.baseDataListView.AllColumns.Add(Me.OlvColumn1)
            Me.baseDataListView.AllColumns.Add(Me.OlvColumn2)
            Me.baseDataListView.AllColumns.Add(Me.OlvColumn3)
            Me.baseDataListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.OlvColumn1, Me.OlvColumn2, Me.OlvColumn3})
            Me.baseDataListView.Size = New System.Drawing.Size(434, 267)
            '
            'OlvColumn1
            '
            Me.OlvColumn1.AspectName = "Code"
            Me.OlvColumn1.Text = "Kodas"
            Me.OlvColumn1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            Me.OlvColumn1.Width = 74
            '
            'OlvColumn2
            '
            Me.OlvColumn2.AspectName = "Name"
            Me.OlvColumn2.Text = "Pavadinimas"
            Me.OlvColumn2.Width = 188
            '
            'OlvColumn3
            '
            Me.OlvColumn3.AspectName = "TypeHumanReadable"
            Me.OlvColumn3.Text = "Tipas"
            Me.OlvColumn3.Width = 107
            '
            'WorkTimeClassInfoListControl
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.Name = "WorkTimeClassInfoListControl"
            Me.Size = New System.Drawing.Size(434, 267)
            CType(Me.baseDataListView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents OlvColumn1 As BrightIdeasSoftware.OLVColumn
        Friend WithEvents OlvColumn2 As BrightIdeasSoftware.OLVColumn
        Friend WithEvents OlvColumn3 As BrightIdeasSoftware.OLVColumn

    End Class
End Namespace
