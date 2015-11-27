<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F_AssetSelectionList
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
        Me.components = New System.ComponentModel.Container
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.nCancelButton = New System.Windows.Forms.Button
        Me.nOkButton = New System.Windows.Forms.Button
        Me.LongTermAssetInfoListBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.LongTermAssetInfoListDataGridView = New System.Windows.Forms.DataGridView
        Me.SelectionDataGridViewColumn = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn11 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn78 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Panel2.SuspendLayout()
        CType(Me.LongTermAssetInfoListBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.LongTermAssetInfoListDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.AutoSize = True
        Me.Panel2.Controls.Add(Me.nCancelButton)
        Me.Panel2.Controls.Add(Me.nOkButton)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 220)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(0, 0, 0, 4)
        Me.Panel2.Size = New System.Drawing.Size(694, 37)
        Me.Panel2.TabIndex = 4
        '
        'nCancelButton
        '
        Me.nCancelButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.nCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.nCancelButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nCancelButton.Location = New System.Drawing.Point(607, 7)
        Me.nCancelButton.Name = "nCancelButton"
        Me.nCancelButton.Size = New System.Drawing.Size(75, 23)
        Me.nCancelButton.TabIndex = 2
        Me.nCancelButton.Text = "Atšaukti"
        Me.nCancelButton.UseVisualStyleBackColor = True
        '
        'nOkButton
        '
        Me.nOkButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.nOkButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nOkButton.Location = New System.Drawing.Point(517, 7)
        Me.nOkButton.Name = "nOkButton"
        Me.nOkButton.Size = New System.Drawing.Size(75, 23)
        Me.nOkButton.TabIndex = 0
        Me.nOkButton.Text = "OK"
        Me.nOkButton.UseVisualStyleBackColor = True
        '
        'LongTermAssetInfoListBindingSource
        '
        Me.LongTermAssetInfoListBindingSource.DataSource = GetType(ApskaitaObjects.ActiveReports.LongTermAssetInfo)
        '
        'LongTermAssetInfoListDataGridView
        '
        Me.LongTermAssetInfoListDataGridView.AllowUserToAddRows = False
        Me.LongTermAssetInfoListDataGridView.AllowUserToDeleteRows = False
        Me.LongTermAssetInfoListDataGridView.AutoGenerateColumns = False
        Me.LongTermAssetInfoListDataGridView.CausesValidation = False
        Me.LongTermAssetInfoListDataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.BottomCenter
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.LongTermAssetInfoListDataGridView.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.LongTermAssetInfoListDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.LongTermAssetInfoListDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SelectionDataGridViewColumn, Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn7, Me.DataGridViewTextBoxColumn6, Me.DataGridViewTextBoxColumn8, Me.DataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn11, Me.DataGridViewTextBoxColumn5, Me.DataGridViewTextBoxColumn4, Me.DataGridViewTextBoxColumn78})
        Me.LongTermAssetInfoListDataGridView.DataSource = Me.LongTermAssetInfoListBindingSource
        Me.LongTermAssetInfoListDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LongTermAssetInfoListDataGridView.Location = New System.Drawing.Point(0, 0)
        Me.LongTermAssetInfoListDataGridView.Name = "LongTermAssetInfoListDataGridView"
        Me.LongTermAssetInfoListDataGridView.RowHeadersVisible = False
        Me.LongTermAssetInfoListDataGridView.Size = New System.Drawing.Size(694, 220)
        Me.LongTermAssetInfoListDataGridView.TabIndex = 5
        '
        'SelectionDataGridViewColumn
        '
        Me.SelectionDataGridViewColumn.HeaderText = ""
        Me.SelectionDataGridViewColumn.Name = "SelectionDataGridViewColumn"
        Me.SelectionDataGridViewColumn.Width = 40
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "ID"
        Me.DataGridViewTextBoxColumn1.HeaderText = "ID"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Visible = False
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.DataPropertyName = "AcquisitionJournalEntryID"
        Me.DataGridViewTextBoxColumn7.HeaderText = "Įsigijimo BŽ ID"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.ReadOnly = True
        Me.DataGridViewTextBoxColumn7.Visible = False
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.DataPropertyName = "AcquisitionDate"
        DataGridViewCellStyle4.Format = "d"
        Me.DataGridViewTextBoxColumn6.DefaultCellStyle = DataGridViewCellStyle4
        Me.DataGridViewTextBoxColumn6.HeaderText = "Įsigijimo Data"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.DataPropertyName = "AcquisitionJournalEntryDocNumber"
        Me.DataGridViewTextBoxColumn8.HeaderText = "Įsigijimo Dok. Nr."
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.ReadOnly = True
        Me.DataGridViewTextBoxColumn8.Visible = False
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "Name"
        Me.DataGridViewTextBoxColumn2.HeaderText = "Pavadinimas"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Width = 200
        '
        'DataGridViewTextBoxColumn11
        '
        Me.DataGridViewTextBoxColumn11.DataPropertyName = "InventoryNumber"
        Me.DataGridViewTextBoxColumn11.HeaderText = "Inv. Nr."
        Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
        Me.DataGridViewTextBoxColumn11.ReadOnly = True
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.DataPropertyName = "CustomGroup"
        Me.DataGridViewTextBoxColumn5.HeaderText = "Grupė"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        Me.DataGridViewTextBoxColumn5.Width = 200
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "LegalGroup"
        Me.DataGridViewTextBoxColumn4.HeaderText = "Grupė Pgl. Įstatymą"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.Visible = False
        '
        'DataGridViewTextBoxColumn78
        '
        Me.DataGridViewTextBoxColumn78.DataPropertyName = "AfterAmmount"
        Me.DataGridViewTextBoxColumn78.HeaderText = "Kiekis"
        Me.DataGridViewTextBoxColumn78.Name = "DataGridViewTextBoxColumn78"
        Me.DataGridViewTextBoxColumn78.ReadOnly = True
        Me.DataGridViewTextBoxColumn78.Width = 50
        '
        'F_AssetSelectionList
        '
        Me.AcceptButton = Me.nOkButton
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.nCancelButton
        Me.CausesValidation = False
        Me.ClientSize = New System.Drawing.Size(694, 257)
        Me.ControlBox = False
        Me.Controls.Add(Me.LongTermAssetInfoListDataGridView)
        Me.Controls.Add(Me.Panel2)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "F_AssetSelectionList"
        Me.ShowIcon = False
        Me.Text = "Pasirinkite norimą ilgalaikį turtą"
        Me.Panel2.ResumeLayout(False)
        CType(Me.LongTermAssetInfoListBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.LongTermAssetInfoListDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents nCancelButton As System.Windows.Forms.Button
    Friend WithEvents nOkButton As System.Windows.Forms.Button
    Friend WithEvents LongTermAssetInfoListBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents LongTermAssetInfoListDataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents SelectionDataGridViewColumn As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn11 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn78 As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
