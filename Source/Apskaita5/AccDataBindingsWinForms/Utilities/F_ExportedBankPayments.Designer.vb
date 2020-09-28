<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F_ExportedBankPayments
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
        Me.components = New System.ComponentModel.Container()
        Dim SubtotalLabel As System.Windows.Forms.Label
        Dim AccountLabel As System.Windows.Forms.Label
        Dim Label1 As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(F_ExportedBankPayments))
        Me.SubtotalTextBox = New System.Windows.Forms.TextBox()
        Me.ExportedBankPaymentsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.ExportButton = New System.Windows.Forms.Button()
        Me.AdaptersComboBox = New System.Windows.Forms.ComboBox()
        Me.AccountAccListComboBox = New AccControlsWinForms.AccListComboBox()
        Me.PaymentsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ItemsDataListView = New BrightIdeasSoftware.DataListView()
        Me.OlvColumn2 = CType(New BrightIdeasSoftware.OLVColumn(),BrightIdeasSoftware.OLVColumn)
        Me.OlvColumn1 = CType(New BrightIdeasSoftware.OLVColumn(),BrightIdeasSoftware.OLVColumn)
        Me.OlvColumn3 = CType(New BrightIdeasSoftware.OLVColumn(),BrightIdeasSoftware.OLVColumn)
        Me.OlvColumn4 = CType(New BrightIdeasSoftware.OLVColumn(),BrightIdeasSoftware.OLVColumn)
        Me.OlvColumn5 = CType(New BrightIdeasSoftware.OLVColumn(),BrightIdeasSoftware.OLVColumn)
        Me.OlvColumn6 = CType(New BrightIdeasSoftware.OLVColumn(),BrightIdeasSoftware.OLVColumn)
        Me.OlvColumn7 = CType(New BrightIdeasSoftware.OLVColumn(),BrightIdeasSoftware.OLVColumn)
        Me.ErrorWarnInfoProvider1 = New AccControlsWinForms.ErrorWarnInfoProvider(Me.components)
        SubtotalLabel = New System.Windows.Forms.Label()
        AccountLabel = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        CType(Me.ExportedBankPaymentsBindingSource,System.ComponentModel.ISupportInitialize).BeginInit
        Me.TableLayoutPanel1.SuspendLayout
        CType(Me.PaymentsBindingSource,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.ItemsDataListView,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.ErrorWarnInfoProvider1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'SubtotalLabel
        '
        SubtotalLabel.AutoSize = true
        SubtotalLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        SubtotalLabel.Location = New System.Drawing.Point(696, 0)
        SubtotalLabel.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        SubtotalLabel.Name = "SubtotalLabel"
        SubtotalLabel.Padding = New System.Windows.Forms.Padding(0, 2, 0, 0)
        SubtotalLabel.Size = New System.Drawing.Size(35, 15)
        SubtotalLabel.TabIndex = 1
        SubtotalLabel.Text = "Viso:"
        '
        'AccountLabel
        '
        AccountLabel.AutoSize = true
        AccountLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        AccountLabel.Location = New System.Drawing.Point(2, 0)
        AccountLabel.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        AccountLabel.Name = "AccountLabel"
        AccountLabel.Padding = New System.Windows.Forms.Padding(0, 2, 0, 0)
        AccountLabel.Size = New System.Drawing.Size(98, 15)
        AccountLabel.TabIndex = 2
        AccountLabel.Text = "Banko sąskaita:"
        '
        'Label1
        '
        Label1.AutoSize = true
        Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Label1.Location = New System.Drawing.Point(2, 24)
        Label1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Label1.Name = "Label1"
        Label1.Padding = New System.Windows.Forms.Padding(0, 2, 0, 0)
        Label1.Size = New System.Drawing.Size(103, 15)
        Label1.TabIndex = 5
        Label1.Text = "Eksporto Versija:"
        '
        'SubtotalTextBox
        '
        Me.SubtotalTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.ExportedBankPaymentsBindingSource, "Subtotal", true))
        Me.SubtotalTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SubtotalTextBox.Location = New System.Drawing.Point(735, 2)
        Me.SubtotalTextBox.Margin = New System.Windows.Forms.Padding(2)
        Me.SubtotalTextBox.Name = "SubtotalTextBox"
        Me.SubtotalTextBox.Size = New System.Drawing.Size(139, 20)
        Me.SubtotalTextBox.TabIndex = 2
        Me.SubtotalTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'ExportedBankPaymentsBindingSource
        '
        Me.ExportedBankPaymentsBindingSource.DataSource = GetType(ApskaitaObjects.Documents.BankDataExchangeProviders.ExportedBankPayments)
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 6
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 15!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20!))
        Me.TableLayoutPanel1.Controls.Add(Me.ExportButton, 4, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.AdaptersComboBox, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Label1, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(AccountLabel, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.SubtotalTextBox, 4, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.AccountAccListComboBox, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(SubtotalLabel, 3, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(2)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 2
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(896, 63)
        Me.TableLayoutPanel1.TabIndex = 4
        '
        'ExportButton
        '
        Me.ExportButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.ExportButton.AutoSize = true
        Me.ExportButton.Image = Global.AccDataBindingsWinForms.My.Resources.Resources.Actions_document_save_icon_32p
        Me.ExportButton.Location = New System.Drawing.Point(836, 26)
        Me.ExportButton.Margin = New System.Windows.Forms.Padding(2)
        Me.ExportButton.Name = "ExportButton"
        Me.ExportButton.Size = New System.Drawing.Size(38, 38)
        Me.ExportButton.TabIndex = 5
        Me.ExportButton.UseVisualStyleBackColor = true
        '
        'AdaptersComboBox
        '
        Me.AdaptersComboBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AdaptersComboBox.FormattingEnabled = true
        Me.AdaptersComboBox.Location = New System.Drawing.Point(109, 26)
        Me.AdaptersComboBox.Margin = New System.Windows.Forms.Padding(2)
        Me.AdaptersComboBox.Name = "AdaptersComboBox"
        Me.AdaptersComboBox.Size = New System.Drawing.Size(568, 21)
        Me.AdaptersComboBox.TabIndex = 5
        '
        'AccountAccListComboBox
        '
        Me.AccountAccListComboBox.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.ExportedBankPaymentsBindingSource, "Account", true))
        Me.AccountAccListComboBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AccountAccListComboBox.EmptyValueString = ""
        Me.AccountAccListComboBox.Location = New System.Drawing.Point(109, 2)
        Me.AccountAccListComboBox.Margin = New System.Windows.Forms.Padding(2)
        Me.AccountAccListComboBox.Name = "AccountAccListComboBox"
        Me.AccountAccListComboBox.Size = New System.Drawing.Size(568, 20)
        Me.AccountAccListComboBox.TabIndex = 3
        '
        'PaymentsBindingSource
        '
        Me.PaymentsBindingSource.DataMember = "Payments"
        Me.PaymentsBindingSource.DataSource = Me.ExportedBankPaymentsBindingSource
        '
        'ItemsDataListView
        '
        Me.ItemsDataListView.AllColumns.Add(Me.OlvColumn2)
        Me.ItemsDataListView.AllColumns.Add(Me.OlvColumn1)
        Me.ItemsDataListView.AllColumns.Add(Me.OlvColumn3)
        Me.ItemsDataListView.AllColumns.Add(Me.OlvColumn4)
        Me.ItemsDataListView.AllColumns.Add(Me.OlvColumn5)
        Me.ItemsDataListView.AllColumns.Add(Me.OlvColumn6)
        Me.ItemsDataListView.AllColumns.Add(Me.OlvColumn7)
        Me.ItemsDataListView.AllowColumnReorder = true
        Me.ItemsDataListView.AutoGenerateColumns = false
        Me.ItemsDataListView.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.SingleClickAlways
        Me.ItemsDataListView.CellEditEnterChangesRows = true
        Me.ItemsDataListView.CellEditTabChangesRows = true
        Me.ItemsDataListView.CellEditUseWholeCell = false
        Me.ItemsDataListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.OlvColumn2, Me.OlvColumn3, Me.OlvColumn4, Me.OlvColumn5, Me.OlvColumn6, Me.OlvColumn7})
        Me.ItemsDataListView.Cursor = System.Windows.Forms.Cursors.Default
        Me.ItemsDataListView.DataSource = Me.PaymentsBindingSource
        Me.ItemsDataListView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ItemsDataListView.FullRowSelect = true
        Me.ItemsDataListView.HasCollapsibleGroups = false
        Me.ItemsDataListView.HeaderWordWrap = true
        Me.ItemsDataListView.HideSelection = false
        Me.ItemsDataListView.HighlightBackgroundColor = System.Drawing.Color.PaleGreen
        Me.ItemsDataListView.HighlightForegroundColor = System.Drawing.Color.Black
        Me.ItemsDataListView.IncludeColumnHeadersInCopy = true
        Me.ItemsDataListView.Location = New System.Drawing.Point(0, 63)
        Me.ItemsDataListView.Name = "ItemsDataListView"
        Me.ItemsDataListView.RenderNonEditableCheckboxesAsDisabled = true
        Me.ItemsDataListView.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.Submenu
        Me.ItemsDataListView.SelectedBackColor = System.Drawing.Color.PaleGreen
        Me.ItemsDataListView.SelectedForeColor = System.Drawing.Color.Black
        Me.ItemsDataListView.ShowCommandMenuOnRightClick = true
        Me.ItemsDataListView.ShowGroups = false
        Me.ItemsDataListView.ShowImagesOnSubItems = true
        Me.ItemsDataListView.ShowItemCountOnGroups = true
        Me.ItemsDataListView.ShowItemToolTips = true
        Me.ItemsDataListView.Size = New System.Drawing.Size(896, 407)
        Me.ItemsDataListView.TabIndex = 5
        Me.ItemsDataListView.UnfocusedSelectedBackColor = System.Drawing.Color.PaleGreen
        Me.ItemsDataListView.UnfocusedSelectedForeColor = System.Drawing.Color.Black
        Me.ItemsDataListView.UseCellFormatEvents = true
        Me.ItemsDataListView.UseCompatibleStateImageBehavior = false
        Me.ItemsDataListView.UseFilterIndicator = true
        Me.ItemsDataListView.UseFiltering = true
        Me.ItemsDataListView.UseHotItem = true
        Me.ItemsDataListView.UseNotifyPropertyChanged = true
        Me.ItemsDataListView.View = System.Windows.Forms.View.Details
        '
        'OlvColumn2
        '
        Me.OlvColumn2.AspectName = "Receiver"
        Me.OlvColumn2.CellEditUseWholeCell = true
        Me.OlvColumn2.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.OlvColumn2.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn2.Text = "Gavėjas"
        Me.OlvColumn2.Width = 234
        '
        'OlvColumn1
        '
        Me.OlvColumn1.AspectName = "UniqueID"
        Me.OlvColumn1.CellEditUseWholeCell = true
        Me.OlvColumn1.DisplayIndex = 1
        Me.OlvColumn1.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.OlvColumn1.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn1.IsVisible = false
        Me.OlvColumn1.Text = "Unikalus Nr."
        Me.OlvColumn1.Width = 100
        '
        'OlvColumn3
        '
        Me.OlvColumn3.AspectName = "ReceiverBankAccount"
        Me.OlvColumn3.CellEditUseWholeCell = true
        Me.OlvColumn3.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.OlvColumn3.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn3.IsEditable = false
        Me.OlvColumn3.Text = "Įprastinė banko sąsk."
        Me.OlvColumn3.Width = 166
        '
        'OlvColumn4
        '
        Me.OlvColumn4.AspectName = "CustomBankAccount"
        Me.OlvColumn4.CellEditUseWholeCell = true
        Me.OlvColumn4.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.OlvColumn4.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn4.Text = "Banko sąsk. šiam pavedimui"
        Me.OlvColumn4.Width = 175
        '
        'OlvColumn5
        '
        Me.OlvColumn5.AspectName = "Amount"
        Me.OlvColumn5.AspectToStringFormat = "{0:##,0.00}"
        Me.OlvColumn5.CellEditUseWholeCell = true
        Me.OlvColumn5.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.OlvColumn5.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn5.Text = "Suma"
        Me.OlvColumn5.Width = 80
        '
        'OlvColumn6
        '
        Me.OlvColumn6.AspectName = "Description"
        Me.OlvColumn6.CellEditUseWholeCell = true
        Me.OlvColumn6.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.OlvColumn6.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn6.Text = "Paskirtis (aprašymas)"
        Me.OlvColumn6.Width = 137
        '
        'OlvColumn7
        '
        Me.OlvColumn7.AspectName = "PurposeCode"
        Me.OlvColumn7.CellEditUseWholeCell = true
        Me.OlvColumn7.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0,Byte))
        Me.OlvColumn7.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn7.Text = "Įmokos kodas"
        Me.OlvColumn7.Width = 100
        '
        'ErrorWarnInfoProvider1
        '
        Me.ErrorWarnInfoProvider1.ContainerControl = Me
        Me.ErrorWarnInfoProvider1.DataSource = Me.ExportedBankPaymentsBindingSource
        '
        'F_ExportedBankPayments
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(896, 470)
        Me.Controls.Add(Me.ItemsDataListView)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"),System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "F_ExportedBankPayments"
        Me.ShowInTaskbar = false
        Me.Text = "Eksportuojami į el. banką mokėjimai"
        CType(Me.ExportedBankPaymentsBindingSource,System.ComponentModel.ISupportInitialize).EndInit
        Me.TableLayoutPanel1.ResumeLayout(false)
        Me.TableLayoutPanel1.PerformLayout
        CType(Me.PaymentsBindingSource,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.ItemsDataListView,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.ErrorWarnInfoProvider1,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)

End Sub

    Friend WithEvents ExportedBankPaymentsBindingSource As BindingSource
    Friend WithEvents SubtotalTextBox As TextBox
    Friend WithEvents AccountAccListComboBox As AccControlsWinForms.AccListComboBox
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents ExportButton As Button
    Friend WithEvents AdaptersComboBox As ComboBox
    Friend WithEvents PaymentsBindingSource As BindingSource
    Friend WithEvents ItemsDataListView As BrightIdeasSoftware.DataListView
    Friend WithEvents OlvColumn1 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn2 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn3 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn4 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn5 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn6 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn7 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents ErrorWarnInfoProvider1 As AccControlsWinForms.ErrorWarnInfoProvider
End Class
