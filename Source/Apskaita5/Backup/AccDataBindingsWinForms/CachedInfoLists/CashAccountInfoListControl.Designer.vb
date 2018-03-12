Namespace CachedInfoLists
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class CashAccountInfoListControl
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
            Me.OlvColumn2 = New BrightIdeasSoftware.OLVColumn
            Me.OlvColumn3 = New BrightIdeasSoftware.OLVColumn
            Me.OlvColumn4 = New BrightIdeasSoftware.OLVColumn
            Me.OlvColumn5 = New BrightIdeasSoftware.OLVColumn
            Me.OlvColumn6 = New BrightIdeasSoftware.OLVColumn
            Me.OlvColumn7 = New BrightIdeasSoftware.OLVColumn
            Me.OlvColumn8 = New BrightIdeasSoftware.OLVColumn
            CType(Me.baseDataListView, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            '
            'baseDataListView
            '
            Me.baseDataListView.AllColumns.Add(Me.OlvColumn2)
            Me.baseDataListView.AllColumns.Add(Me.OlvColumn4)
            Me.baseDataListView.AllColumns.Add(Me.OlvColumn3)
            Me.baseDataListView.AllColumns.Add(Me.OlvColumn8)
            Me.baseDataListView.AllColumns.Add(Me.OlvColumn5)
            Me.baseDataListView.AllColumns.Add(Me.OlvColumn6)
            Me.baseDataListView.AllColumns.Add(Me.OlvColumn7)
            Me.baseDataListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.OlvColumn2, Me.OlvColumn4, Me.OlvColumn3, Me.OlvColumn8})
            Me.baseDataListView.Size = New System.Drawing.Size(467, 355)
            '
            'OlvColumn2
            '
            Me.OlvColumn2.AspectName = "Account"
            Me.OlvColumn2.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
            Me.OlvColumn2.Text = "Sąsk. Nr."
            Me.OlvColumn2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            Me.OlvColumn2.Width = 79
            '
            'OlvColumn3
            '
            Me.OlvColumn3.AspectName = "Name"
            Me.OlvColumn3.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
            Me.OlvColumn3.Text = "Pavadinimas"
            Me.OlvColumn3.Width = 263
            '
            'OlvColumn4
            '
            Me.OlvColumn4.AspectName = "CurrencyCode"
            Me.OlvColumn4.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
            Me.OlvColumn4.Text = "Valiuta"
            Me.OlvColumn4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            '
            'OlvColumn5
            '
            Me.OlvColumn5.AspectName = "ID"
            Me.OlvColumn5.DisplayIndex = 4
            Me.OlvColumn5.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
            Me.OlvColumn5.IsVisible = False
            Me.OlvColumn5.Text = "ID"
            Me.OlvColumn5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            '
            'OlvColumn6
            '
            Me.OlvColumn6.AspectName = "BankName"
            Me.OlvColumn6.DisplayIndex = 5
            Me.OlvColumn6.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
            Me.OlvColumn6.IsVisible = False
            Me.OlvColumn6.Text = "Bankas"
            '
            'OlvColumn7
            '
            Me.OlvColumn7.AspectName = "BankAccountNumber"
            Me.OlvColumn7.DisplayIndex = 6
            Me.OlvColumn7.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
            Me.OlvColumn7.IsVisible = False
            Me.OlvColumn7.Text = "Banko Sąsk. (IBAN) Nr."
            '
            'OlvColumn8
            '
            Me.OlvColumn8.AspectName = "IsObsolete"
            Me.OlvColumn8.CheckBoxes = True
            Me.OlvColumn8.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
            Me.OlvColumn8.IsHeaderVertical = True
            Me.OlvColumn8.Text = "Istorinė"
            Me.OlvColumn8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
            '
            'CashAccountInfoListControl
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.Name = "CashAccountInfoListControl"
            Me.Size = New System.Drawing.Size(467, 355)
            CType(Me.baseDataListView, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents OlvColumn2 As BrightIdeasSoftware.OLVColumn
        Friend WithEvents OlvColumn3 As BrightIdeasSoftware.OLVColumn
        Friend WithEvents OlvColumn4 As BrightIdeasSoftware.OLVColumn
        Friend WithEvents OlvColumn5 As BrightIdeasSoftware.OLVColumn
        Friend WithEvents OlvColumn6 As BrightIdeasSoftware.OLVColumn
        Friend WithEvents OlvColumn7 As BrightIdeasSoftware.OLVColumn
        Friend WithEvents OlvColumn8 As BrightIdeasSoftware.OLVColumn

    End Class
End Namespace