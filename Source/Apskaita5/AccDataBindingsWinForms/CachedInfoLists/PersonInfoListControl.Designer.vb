Imports AccControlsWinForms

Namespace CachedInfoLists

    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class PersonInfoListControl
        Inherits InfoListControl

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
            Me.OlvColumn6 = New BrightIdeasSoftware.OLVColumn
            Me.OlvColumn7 = New BrightIdeasSoftware.OLVColumn
            Me.OlvColumn8 = New BrightIdeasSoftware.OLVColumn
            Me.OlvColumn9 = New BrightIdeasSoftware.OLVColumn
            Me.OlvColumn10 = New BrightIdeasSoftware.OLVColumn
            Me.OlvColumn11 = New BrightIdeasSoftware.OLVColumn
            CType(Me.baseDataListView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'baseDataListView
            '
            Me.baseDataListView.AllColumns.Add(Me.OlvColumn1)
            Me.baseDataListView.AllColumns.Add(Me.OlvColumn2)
            Me.baseDataListView.AllColumns.Add(Me.OlvColumn3)
            Me.baseDataListView.AllColumns.Add(Me.OlvColumn4)
            Me.baseDataListView.AllColumns.Add(Me.OlvColumn5)
            Me.baseDataListView.AllColumns.Add(Me.OlvColumn6)
            Me.baseDataListView.AllColumns.Add(Me.OlvColumn7)
            Me.baseDataListView.AllColumns.Add(Me.OlvColumn8)
            Me.baseDataListView.AllColumns.Add(Me.OlvColumn9)
            Me.baseDataListView.AllColumns.Add(Me.OlvColumn10)
            Me.baseDataListView.AllColumns.Add(Me.OlvColumn11)
            Me.baseDataListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.OlvColumn1, Me.OlvColumn2, Me.OlvColumn3})
            Me.baseDataListView.IsSearchOnSortColumn = False
            '
            'OlvColumn1
            '
            Me.OlvColumn1.AspectName = "Name"
            Me.OlvColumn1.Text = "Pavadinimas"
            Me.OlvColumn1.Width = 189
            '
            'OlvColumn2
            '
            Me.OlvColumn2.AspectName = "Code"
            Me.OlvColumn2.Text = "Kodas"
            Me.OlvColumn2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            Me.OlvColumn2.Width = 90
            '
            'OlvColumn3
            '
            Me.OlvColumn3.AspectName = "Address"
            Me.OlvColumn3.Text = "Adresas"
            Me.OlvColumn3.Width = 152
            '
            'OlvColumn4
            '
            Me.OlvColumn4.AspectName = "CodeVAT"
            Me.OlvColumn4.IsVisible = False
            Me.OlvColumn4.Text = "PVM Kodas"
            '
            'OlvColumn5
            '
            Me.OlvColumn5.AspectName = "CurrencyCode"
            Me.OlvColumn5.IsVisible = False
            Me.OlvColumn5.Text = "Valiuta"
            Me.OlvColumn5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            '
            'OlvColumn6
            '
            Me.OlvColumn6.AspectName = "LanguageName"
            Me.OlvColumn6.IsVisible = False
            Me.OlvColumn6.Text = "Kalba"
            Me.OlvColumn6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            '
            'OlvColumn7
            '
            Me.OlvColumn7.AspectName = "InternalCode"
            Me.OlvColumn7.IsVisible = False
            Me.OlvColumn7.Text = "Vidinis Kodas"
            Me.OlvColumn7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            '
            'OlvColumn8
            '
            Me.OlvColumn8.AspectName = "Email"
            Me.OlvColumn8.IsVisible = False
            Me.OlvColumn8.Text = "E-paštas"
            '
            'OlvColumn9
            '
            Me.OlvColumn9.AspectName = "IsClient"
            Me.OlvColumn9.CheckBoxes = True
            Me.OlvColumn9.IsVisible = False
            Me.OlvColumn9.Text = "Pirkėjas"
            '
            'OlvColumn10
            '
            Me.OlvColumn10.AspectName = "IsSupplier"
            Me.OlvColumn10.CheckBoxes = True
            Me.OlvColumn10.IsVisible = False
            Me.OlvColumn10.Text = "Tiekėjas"
            '
            'OlvColumn11
            '
            Me.OlvColumn11.AspectName = "IsWorker"
            Me.OlvColumn11.CheckBoxes = True
            Me.OlvColumn11.IsVisible = False
            Me.OlvColumn11.Text = "Darbuotojas"
            '
            'PersonInfoListControl
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.Name = "PersonInfoListControl"
            CType(Me.baseDataListView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents OlvColumn1 As BrightIdeasSoftware.OLVColumn
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
End Namespace