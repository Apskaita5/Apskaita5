Namespace CachedInfoLists

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class UserReportInfoListControl
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
            Me.baseDataListView.AllColumns.Add(Me.OlvColumn2)
            Me.baseDataListView.AllColumns.Add(Me.OlvColumn3)
            Me.baseDataListView.AllColumns.Add(Me.OlvColumn1)
            Me.baseDataListView.AllColumns.Add(Me.OlvColumn4)
            Me.baseDataListView.AllColumns.Add(Me.OlvColumn5)
            Me.baseDataListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.OlvColumn2, Me.OlvColumn3})
            '
            'OlvColumn1
            '
            Me.OlvColumn1.AspectName = "FileName"
            Me.OlvColumn1.CellEditUseWholeCell = True
            Me.OlvColumn1.DisplayIndex = 2
            Me.OlvColumn1.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.OlvColumn1.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
            Me.OlvColumn1.IsEditable = False
            Me.OlvColumn1.IsVisible = False
            Me.OlvColumn1.Text = "Failas"
            Me.OlvColumn1.Width = 100
            '
            'OlvColumn2
            '
            Me.OlvColumn2.AspectName = "Name"
            Me.OlvColumn2.CellEditUseWholeCell = True
            Me.OlvColumn2.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.OlvColumn2.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
            Me.OlvColumn2.IsEditable = False
            Me.OlvColumn2.Text = "Ataskaitos Pavadinimas"
            Me.OlvColumn2.Width = 272
            '
            'OlvColumn3
            '
            Me.OlvColumn3.AspectName = "Author"
            Me.OlvColumn3.CellEditUseWholeCell = True
            Me.OlvColumn3.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.OlvColumn3.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
            Me.OlvColumn3.IsEditable = False
            Me.OlvColumn3.Text = "Ataskaitos Autorius"
            Me.OlvColumn3.Width = 160
            '
            'OlvColumn4
            '
            Me.OlvColumn4.AspectName = "FileAdded"
            Me.OlvColumn4.AspectToStringFormat = "yyyy-MM-dd HH:mm:ss"
            Me.OlvColumn4.CellEditUseWholeCell = True
            Me.OlvColumn4.DisplayIndex = 3
            Me.OlvColumn4.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.OlvColumn4.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
            Me.OlvColumn4.IsEditable = False
            Me.OlvColumn4.IsVisible = False
            Me.OlvColumn4.Text = "Įkelta"
            Me.OlvColumn4.Width = 50
            '
            'OlvColumn5
            '
            Me.OlvColumn5.AspectName = "FileLastUpdated"
            Me.OlvColumn5.AspectToStringFormat = "yyyy-MM-dd HH:mm:ss"
            Me.OlvColumn5.CellEditUseWholeCell = True
            Me.OlvColumn5.DisplayIndex = 4
            Me.OlvColumn5.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
            Me.OlvColumn5.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
            Me.OlvColumn5.IsEditable = False
            Me.OlvColumn5.IsVisible = False
            Me.OlvColumn5.Text = "Pakeista"
            Me.OlvColumn5.Width = 50
            '
            'UserReportInfoListControl
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.Name = "UserReportInfoListControl"
            CType(Me.baseDataListView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub

        Friend WithEvents OlvColumn1 As BrightIdeasSoftware.OLVColumn
        Friend WithEvents OlvColumn2 As BrightIdeasSoftware.OLVColumn
        Friend WithEvents OlvColumn3 As BrightIdeasSoftware.OLVColumn
        Friend WithEvents OlvColumn4 As BrightIdeasSoftware.OLVColumn
        Friend WithEvents OlvColumn5 As BrightIdeasSoftware.OLVColumn

    End Class

End Namespace