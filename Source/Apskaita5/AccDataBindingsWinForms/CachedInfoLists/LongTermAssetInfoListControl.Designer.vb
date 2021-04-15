Namespace CachedInfoLists
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Public Class LongTermAssetInfoListControl
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
            Me.OlvColumn8 = New BrightIdeasSoftware.OLVColumn
            Me.OlvColumn9 = New BrightIdeasSoftware.OLVColumn
            Me.OlvColumn10 = New BrightIdeasSoftware.OLVColumn
            Me.OlvColumn11 = New BrightIdeasSoftware.OLVColumn
            CType(Me.baseDataListView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'baseDataListView
            '
            Me.baseDataListView.AllColumns.Add(Me.OlvColumn7)
            Me.baseDataListView.AllColumns.Add(Me.OlvColumn11)
            Me.baseDataListView.AllColumns.Add(Me.OlvColumn8)
            Me.baseDataListView.AllColumns.Add(Me.OlvColumn9)
            Me.baseDataListView.AllColumns.Add(Me.OlvColumn10)
            Me.baseDataListView.AllColumns.Add(Me.OlvColumn3)
            Me.baseDataListView.AllColumns.Add(Me.OlvColumn4)
            Me.baseDataListView.AllColumns.Add(Me.OlvColumn1)
            Me.baseDataListView.AllColumns.Add(Me.OlvColumn5)
            Me.baseDataListView.AllColumns.Add(Me.OlvColumn6)
            Me.baseDataListView.AllColumns.Add(Me.OlvColumn2)
            Me.baseDataListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.OlvColumn7, Me.OlvColumn9, Me.OlvColumn3, Me.OlvColumn4, Me.OlvColumn1, Me.OlvColumn5, Me.OlvColumn6, Me.OlvColumn2})
            Me.baseDataListView.Size = New System.Drawing.Size(795, 242)
            '
            'OlvColumn1
            '
            Me.OlvColumn1.AspectName = "AccountAcquisition"
            Me.OlvColumn1.Text = "Įsigijimo Savik. Sąsk."
            Me.OlvColumn1.Width = 96
            '
            'OlvColumn2
            '
            Me.OlvColumn2.AspectName = "AfterAcquisitionAccountValue"
            Me.OlvColumn2.AspectToStringFormat = "{0:##,0.00}"
            Me.OlvColumn2.Text = "Įsigijimo Savikaina"
            Me.OlvColumn2.Width = 93
            '
            'OlvColumn3
            '
            Me.OlvColumn3.AspectName = "AcquisitionDate"
            Me.OlvColumn3.AspectToStringFormat = "{0:yyyy-MM-dd}"
            Me.OlvColumn3.Text = "Įsigijimo Data"
            Me.OlvColumn3.Width = 77
            '
            'OlvColumn4
            '
            Me.OlvColumn4.AspectName = "AcquisitionJournalEntryDocNumber"
            Me.OlvColumn4.Text = "Įsigijimo Dok. Nr."
            Me.OlvColumn4.Width = 85
            '
            'OlvColumn5
            '
            Me.OlvColumn5.AspectName = "AfterAmmount"
            Me.OlvColumn5.Text = "Kiekis"
            Me.OlvColumn5.Width = 64
            '
            'OlvColumn6
            '
            Me.OlvColumn6.AspectName = "AfterValue"
            Me.OlvColumn6.AspectToStringFormat = "{0:##,0.00}"
            Me.OlvColumn6.Text = "Balansinė Vertė"
            Me.OlvColumn6.Width = 82
            '
            'OlvColumn7
            '
            Me.OlvColumn7.AspectName = "Name"
            Me.OlvColumn7.Text = "Pavadinimas"
            Me.OlvColumn7.Width = 210
            '
            'OlvColumn8
            '
            Me.OlvColumn8.AspectName = "MeasureUnit"
            Me.OlvColumn8.DisplayIndex = 2
            Me.OlvColumn8.IsVisible = False
            Me.OlvColumn8.Text = "Mato Vnt."
            '
            'OlvColumn9
            '
            Me.OlvColumn9.AspectName = "InventoryNumber"
            Me.OlvColumn9.Text = "Inventorinis Nr."
            Me.OlvColumn9.Width = 83
            '
            'OlvColumn10
            '
            Me.OlvColumn10.AspectName = "CustomGroup"
            Me.OlvColumn10.DisplayIndex = 4
            Me.OlvColumn10.IsVisible = False
            Me.OlvColumn10.Text = "Grupė"
            '
            'OlvColumn11
            '
            Me.OlvColumn11.AspectName = "ID"
            Me.OlvColumn11.DisplayIndex = 1
            Me.OlvColumn11.IsVisible = False
            Me.OlvColumn11.Text = "ID"
            '
            'LongTermAssetInfoListControl
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.Name = "LongTermAssetInfoListControl"
            Me.Size = New System.Drawing.Size(795, 242)
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
