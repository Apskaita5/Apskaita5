<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class VatDeclarationSchemaInfoListControl
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
        Me.OlvColumn6 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn7 = New BrightIdeasSoftware.OLVColumn
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
        Me.baseDataListView.AllColumns.Add(Me.OlvColumn7)
        Me.baseDataListView.AllColumns.Add(Me.OlvColumn6)
        Me.baseDataListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.OlvColumn2, Me.OlvColumn3, Me.OlvColumn4})
        '
        'OlvColumn1
        '
        Me.OlvColumn1.AspectName = "ID"
        Me.OlvColumn1.DisplayIndex = 2
        Me.OlvColumn1.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn1.IsVisible = False
        Me.OlvColumn1.Text = "ID"
        Me.OlvColumn1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn1.Width = 30
        '
        'OlvColumn2
        '
        Me.OlvColumn2.AspectName = "VatRate"
        Me.OlvColumn2.AspectToStringFormat = "{0:##,0.00}"
        Me.OlvColumn2.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn2.Text = "Tarifas"
        Me.OlvColumn2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn2.Width = 51
        '
        'OlvColumn3
        '
        Me.OlvColumn3.AspectName = "Name"
        Me.OlvColumn3.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn3.Text = "Pavadinimas"
        Me.OlvColumn3.Width = 187
        '
        'OlvColumn4
        '
        Me.OlvColumn4.AspectName = "Description"
        Me.OlvColumn4.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn4.Text = "Aprašymas"
        Me.OlvColumn4.Width = 192
        '
        'OlvColumn5
        '
        Me.OlvColumn5.AspectName = "TradedTypeHumanReadable"
        Me.OlvColumn5.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn5.IsVisible = False
        Me.OlvColumn5.Text = "Apyvartos Tipas"
        '
        'OlvColumn6
        '
        Me.OlvColumn6.AspectName = "IsObsolete"
        Me.OlvColumn6.CheckBoxes = True
        Me.OlvColumn6.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn6.IsHeaderVertical = True
        Me.OlvColumn6.IsVisible = False
        Me.OlvColumn6.Text = "Istorinė"
        Me.OlvColumn6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'OlvColumn7
        '
        Me.OlvColumn7.AspectName = "ExternalCode"
        Me.OlvColumn7.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn7.IsVisible = False
        Me.OlvColumn7.Text = "Išorinis Kodas"
        '
        'VatDeclarationSchemaInfoListControl
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.Name = "VatDeclarationSchemaInfoListControl"
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

End Class
