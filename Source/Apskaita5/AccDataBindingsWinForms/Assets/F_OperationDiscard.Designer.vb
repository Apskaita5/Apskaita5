<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Friend Class F_OperationDiscard
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
        Dim AccountCostsLabel As System.Windows.Forms.Label
        Dim AmountToDiscardLabel As System.Windows.Forms.Label
        Dim ContentLabel As System.Windows.Forms.Label
        Dim DateLabel As System.Windows.Forms.Label
        Dim DocumentNumberLabel As System.Windows.Forms.Label
        Dim IDLabel As System.Windows.Forms.Label
        Dim InsertDateLabel As System.Windows.Forms.Label
        Dim UpdateDateLabel As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(F_OperationDiscard))
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.nCancelButton = New System.Windows.Forms.Button
        Me.ApplyButton = New System.Windows.Forms.Button
        Me.nOkButton = New System.Windows.Forms.Button
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.ViewJournalEntryButton = New System.Windows.Forms.Button
        Me.ContentTextBox = New System.Windows.Forms.TextBox
        Me.OperationDiscardBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.IsComplexActCheckBox = New System.Windows.Forms.CheckBox
        Me.IDTextBox = New System.Windows.Forms.TextBox
        Me.DocumentNumberTextBox = New System.Windows.Forms.TextBox
        Me.AccountCostsAccGridComboBox = New AccControlsWinForms.AccListComboBox
        Me.AmountToDiscardAccTextBox = New AccControlsWinForms.AccTextBox
        Me.UpdateDateTextBox = New System.Windows.Forms.TextBox
        Me.InsertDateTextBox = New System.Windows.Forms.TextBox
        Me.DateAccDatePicker = New AccControlsWinForms.AccDatePicker
        Me.ProgressFiller2 = New AccControlsWinForms.ProgressFiller
        Me.ProgressFiller1 = New AccControlsWinForms.ProgressFiller
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.BackgroundInfoPanel1 = New AccDataBindingsWinForms.BackgroundInfoPanel
        Me.ErrorWarnInfoProvider1 = New AccControlsWinForms.ErrorWarnInfoProvider(Me.components)
        AccountCostsLabel = New System.Windows.Forms.Label
        AmountToDiscardLabel = New System.Windows.Forms.Label
        ContentLabel = New System.Windows.Forms.Label
        DateLabel = New System.Windows.Forms.Label
        DocumentNumberLabel = New System.Windows.Forms.Label
        IDLabel = New System.Windows.Forms.Label
        InsertDateLabel = New System.Windows.Forms.Label
        UpdateDateLabel = New System.Windows.Forms.Label
        Me.Panel2.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.OperationDiscardBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        CType(Me.ErrorWarnInfoProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'AccountCostsLabel
        '
        AccountCostsLabel.AutoSize = True
        AccountCostsLabel.Dock = System.Windows.Forms.DockStyle.Fill
        AccountCostsLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        AccountCostsLabel.Location = New System.Drawing.Point(284, 52)
        AccountCostsLabel.Name = "AccountCostsLabel"
        AccountCostsLabel.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        AccountCostsLabel.Size = New System.Drawing.Size(97, 27)
        AccountCostsLabel.TabIndex = 0
        AccountCostsLabel.Text = "Sąnaudų Sąsk.:"
        AccountCostsLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'AmountToDiscardLabel
        '
        AmountToDiscardLabel.AutoSize = True
        AmountToDiscardLabel.Dock = System.Windows.Forms.DockStyle.Fill
        AmountToDiscardLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        AmountToDiscardLabel.Location = New System.Drawing.Point(3, 52)
        AmountToDiscardLabel.Name = "AmountToDiscardLabel"
        AmountToDiscardLabel.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        AmountToDiscardLabel.Size = New System.Drawing.Size(111, 27)
        AmountToDiscardLabel.TabIndex = 2
        AmountToDiscardLabel.Text = "Nurašomas Kiekis:"
        AmountToDiscardLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'ContentLabel
        '
        ContentLabel.AutoSize = True
        ContentLabel.Dock = System.Windows.Forms.DockStyle.Fill
        ContentLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        ContentLabel.Location = New System.Drawing.Point(3, 79)
        ContentLabel.Name = "ContentLabel"
        ContentLabel.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        ContentLabel.Size = New System.Drawing.Size(111, 26)
        ContentLabel.TabIndex = 4
        ContentLabel.Text = "Turinys:"
        ContentLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'DateLabel
        '
        DateLabel.AutoSize = True
        DateLabel.Dock = System.Windows.Forms.DockStyle.Fill
        DateLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DateLabel.Location = New System.Drawing.Point(3, 26)
        DateLabel.Name = "DateLabel"
        DateLabel.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        DateLabel.Size = New System.Drawing.Size(111, 26)
        DateLabel.TabIndex = 6
        DateLabel.Text = "Data:"
        DateLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'DocumentNumberLabel
        '
        DocumentNumberLabel.AutoSize = True
        DocumentNumberLabel.Dock = System.Windows.Forms.DockStyle.Fill
        DocumentNumberLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DocumentNumberLabel.Location = New System.Drawing.Point(284, 26)
        DocumentNumberLabel.Name = "DocumentNumberLabel"
        DocumentNumberLabel.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        DocumentNumberLabel.Size = New System.Drawing.Size(97, 26)
        DocumentNumberLabel.TabIndex = 8
        DocumentNumberLabel.Text = "Dok. Nr.:"
        DocumentNumberLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'IDLabel
        '
        IDLabel.AutoSize = True
        IDLabel.Dock = System.Windows.Forms.DockStyle.Fill
        IDLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        IDLabel.Location = New System.Drawing.Point(3, 0)
        IDLabel.Name = "IDLabel"
        IDLabel.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        IDLabel.Size = New System.Drawing.Size(111, 26)
        IDLabel.TabIndex = 10
        IDLabel.Text = "ID:"
        IDLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'InsertDateLabel
        '
        InsertDateLabel.AutoSize = True
        InsertDateLabel.Dock = System.Windows.Forms.DockStyle.Fill
        InsertDateLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        InsertDateLabel.Location = New System.Drawing.Point(284, 0)
        InsertDateLabel.Name = "InsertDateLabel"
        InsertDateLabel.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        InsertDateLabel.Size = New System.Drawing.Size(97, 26)
        InsertDateLabel.TabIndex = 12
        InsertDateLabel.Text = "Įtraukta:"
        InsertDateLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'UpdateDateLabel
        '
        UpdateDateLabel.AutoSize = True
        UpdateDateLabel.Dock = System.Windows.Forms.DockStyle.Fill
        UpdateDateLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        UpdateDateLabel.Location = New System.Drawing.Point(547, 0)
        UpdateDateLabel.Name = "UpdateDateLabel"
        UpdateDateLabel.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        UpdateDateLabel.Size = New System.Drawing.Size(60, 26)
        UpdateDateLabel.TabIndex = 14
        UpdateDateLabel.Text = "Pakeista:"
        UpdateDateLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Panel2
        '
        Me.Panel2.AutoSize = True
        Me.Panel2.Controls.Add(Me.nCancelButton)
        Me.Panel2.Controls.Add(Me.ApplyButton)
        Me.Panel2.Controls.Add(Me.nOkButton)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 388)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(0, 0, 0, 4)
        Me.Panel2.Size = New System.Drawing.Size(784, 42)
        Me.Panel2.TabIndex = 1
        '
        'nCancelButton
        '
        Me.nCancelButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.nCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.nCancelButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nCancelButton.Location = New System.Drawing.Point(697, 12)
        Me.nCancelButton.Name = "nCancelButton"
        Me.nCancelButton.Size = New System.Drawing.Size(75, 23)
        Me.nCancelButton.TabIndex = 3
        Me.nCancelButton.Text = "Atšaukti"
        Me.nCancelButton.UseVisualStyleBackColor = True
        '
        'ApplyButton
        '
        Me.ApplyButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ApplyButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ApplyButton.Location = New System.Drawing.Point(616, 12)
        Me.ApplyButton.Name = "ApplyButton"
        Me.ApplyButton.Size = New System.Drawing.Size(75, 23)
        Me.ApplyButton.TabIndex = 2
        Me.ApplyButton.Text = "Taikyti"
        Me.ApplyButton.UseVisualStyleBackColor = True
        '
        'nOkButton
        '
        Me.nOkButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.nOkButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nOkButton.Location = New System.Drawing.Point(535, 12)
        Me.nOkButton.Name = "nOkButton"
        Me.nOkButton.Size = New System.Drawing.Size(75, 23)
        Me.nOkButton.TabIndex = 1
        Me.nOkButton.Text = "OK"
        Me.nOkButton.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(784, 388)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.AutoScroll = True
        Me.TabPage1.Controls.Add(Me.TableLayoutPanel1)
        Me.TabPage1.Controls.Add(Me.ProgressFiller2)
        Me.TabPage1.Controls.Add(Me.ProgressFiller1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(776, 362)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Operacijos duomenys"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 9
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.ViewJournalEntryButton, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(IDLabel, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.ContentTextBox, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(ContentLabel, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.IsComplexActCheckBox, 6, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.IDTextBox, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.DocumentNumberTextBox, 4, 1)
        Me.TableLayoutPanel1.Controls.Add(DocumentNumberLabel, 3, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.AccountCostsAccGridComboBox, 4, 2)
        Me.TableLayoutPanel1.Controls.Add(AccountCostsLabel, 3, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.AmountToDiscardAccTextBox, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(AmountToDiscardLabel, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.UpdateDateTextBox, 7, 0)
        Me.TableLayoutPanel1.Controls.Add(UpdateDateLabel, 6, 0)
        Me.TableLayoutPanel1.Controls.Add(DateLabel, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(InsertDateLabel, 3, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.InsertDateTextBox, 4, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.DateAccDatePicker, 1, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 5
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(770, 356)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'ViewJournalEntryButton
        '
        Me.ViewJournalEntryButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ViewJournalEntryButton.Image = Global.AccDataBindingsWinForms.My.Resources.Resources.lektuvelis_16
        Me.ViewJournalEntryButton.Location = New System.Drawing.Point(257, 0)
        Me.ViewJournalEntryButton.Margin = New System.Windows.Forms.Padding(0)
        Me.ViewJournalEntryButton.Name = "ViewJournalEntryButton"
        Me.ViewJournalEntryButton.Size = New System.Drawing.Size(24, 24)
        Me.ViewJournalEntryButton.TabIndex = 20
        Me.ViewJournalEntryButton.UseVisualStyleBackColor = True
        '
        'ContentTextBox
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.ContentTextBox, 7)
        Me.ContentTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OperationDiscardBindingSource, "Content", True))
        Me.ContentTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ContentTextBox.Location = New System.Drawing.Point(120, 82)
        Me.ContentTextBox.MaxLength = 255
        Me.ContentTextBox.Name = "ContentTextBox"
        Me.ContentTextBox.Size = New System.Drawing.Size(627, 20)
        Me.ContentTextBox.TabIndex = 4
        '
        'OperationDiscardBindingSource
        '
        Me.OperationDiscardBindingSource.DataSource = GetType(ApskaitaObjects.Assets.OperationDiscard)
        '
        'IsComplexActCheckBox
        '
        Me.IsComplexActCheckBox.AutoSize = True
        Me.TableLayoutPanel1.SetColumnSpan(Me.IsComplexActCheckBox, 2)
        Me.IsComplexActCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("CheckState", Me.OperationDiscardBindingSource, "IsComplexAct", True))
        Me.IsComplexActCheckBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.IsComplexActCheckBox.Enabled = False
        Me.IsComplexActCheckBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IsComplexActCheckBox.Location = New System.Drawing.Point(547, 55)
        Me.IsComplexActCheckBox.Name = "IsComplexActCheckBox"
        Me.IsComplexActCheckBox.Size = New System.Drawing.Size(200, 21)
        Me.IsComplexActCheckBox.TabIndex = 17
        Me.IsComplexActCheckBox.TabStop = False
        Me.IsComplexActCheckBox.Text = "Kompleksinis dok."
        Me.IsComplexActCheckBox.UseVisualStyleBackColor = True
        '
        'IDTextBox
        '
        Me.IDTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OperationDiscardBindingSource, "ID", True))
        Me.IDTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.IDTextBox.Location = New System.Drawing.Point(120, 3)
        Me.IDTextBox.Name = "IDTextBox"
        Me.IDTextBox.ReadOnly = True
        Me.IDTextBox.Size = New System.Drawing.Size(134, 20)
        Me.IDTextBox.TabIndex = 11
        Me.IDTextBox.TabStop = False
        Me.IDTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'DocumentNumberTextBox
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.DocumentNumberTextBox, 4)
        Me.DocumentNumberTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OperationDiscardBindingSource, "DocumentNumber", True))
        Me.DocumentNumberTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DocumentNumberTextBox.Location = New System.Drawing.Point(387, 29)
        Me.DocumentNumberTextBox.MaxLength = 30
        Me.DocumentNumberTextBox.Name = "DocumentNumberTextBox"
        Me.DocumentNumberTextBox.Size = New System.Drawing.Size(360, 20)
        Me.DocumentNumberTextBox.TabIndex = 1
        Me.DocumentNumberTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'AccountCostsAccGridComboBox
        '
        Me.AccountCostsAccGridComboBox.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.OperationDiscardBindingSource, "AccountCosts", True))
        Me.AccountCostsAccGridComboBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AccountCostsAccGridComboBox.EmptyValueString = ""
        Me.AccountCostsAccGridComboBox.FilterString = ""
        Me.AccountCostsAccGridComboBox.FormattingEnabled = True
        Me.AccountCostsAccGridComboBox.InstantBinding = True
        Me.AccountCostsAccGridComboBox.Location = New System.Drawing.Point(387, 55)
        Me.AccountCostsAccGridComboBox.Name = "AccountCostsAccGridComboBox"
        Me.AccountCostsAccGridComboBox.Size = New System.Drawing.Size(134, 21)
        Me.AccountCostsAccGridComboBox.TabIndex = 3
        '
        'AmountToDiscardAccTextBox
        '
        Me.AmountToDiscardAccTextBox.DataBindings.Add(New System.Windows.Forms.Binding("DecimalValue", Me.OperationDiscardBindingSource, "AmountToDiscard", True))
        Me.AmountToDiscardAccTextBox.DecimalLength = 0
        Me.AmountToDiscardAccTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AmountToDiscardAccTextBox.KeepBackColorWhenReadOnly = False
        Me.AmountToDiscardAccTextBox.Location = New System.Drawing.Point(120, 55)
        Me.AmountToDiscardAccTextBox.Name = "AmountToDiscardAccTextBox"
        Me.AmountToDiscardAccTextBox.NegativeValue = False
        Me.AmountToDiscardAccTextBox.Size = New System.Drawing.Size(134, 20)
        Me.AmountToDiscardAccTextBox.TabIndex = 2
        Me.AmountToDiscardAccTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'UpdateDateTextBox
        '
        Me.UpdateDateTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OperationDiscardBindingSource, "UpdateDate", True))
        Me.UpdateDateTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UpdateDateTextBox.Location = New System.Drawing.Point(613, 3)
        Me.UpdateDateTextBox.Name = "UpdateDateTextBox"
        Me.UpdateDateTextBox.ReadOnly = True
        Me.UpdateDateTextBox.Size = New System.Drawing.Size(134, 20)
        Me.UpdateDateTextBox.TabIndex = 15
        Me.UpdateDateTextBox.TabStop = False
        Me.UpdateDateTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'InsertDateTextBox
        '
        Me.InsertDateTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OperationDiscardBindingSource, "InsertDate", True))
        Me.InsertDateTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.InsertDateTextBox.Location = New System.Drawing.Point(387, 3)
        Me.InsertDateTextBox.Name = "InsertDateTextBox"
        Me.InsertDateTextBox.ReadOnly = True
        Me.InsertDateTextBox.Size = New System.Drawing.Size(134, 20)
        Me.InsertDateTextBox.TabIndex = 13
        Me.InsertDateTextBox.TabStop = False
        Me.InsertDateTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'DateAccDatePicker
        '
        Me.DateAccDatePicker.BoldedDates = Nothing
        Me.DateAccDatePicker.DataBindings.Add(New System.Windows.Forms.Binding("Value", Me.OperationDiscardBindingSource, "Date", True))
        Me.DateAccDatePicker.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DateAccDatePicker.Location = New System.Drawing.Point(120, 29)
        Me.DateAccDatePicker.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.DateAccDatePicker.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.DateAccDatePicker.Name = "DateAccDatePicker"
        Me.DateAccDatePicker.ReadOnly = False
        Me.DateAccDatePicker.ShowWeekNumbers = True
        Me.DateAccDatePicker.Size = New System.Drawing.Size(134, 20)
        Me.DateAccDatePicker.TabIndex = 0
        Me.DateAccDatePicker.Value = New Date(2017, 10, 13, 0, 0, 0, 0)
        '
        'ProgressFiller2
        '
        Me.ProgressFiller2.Location = New System.Drawing.Point(306, 31)
        Me.ProgressFiller2.Name = "ProgressFiller2"
        Me.ProgressFiller2.Size = New System.Drawing.Size(145, 56)
        Me.ProgressFiller2.TabIndex = 2
        Me.ProgressFiller2.Visible = False
        '
        'ProgressFiller1
        '
        Me.ProgressFiller1.Location = New System.Drawing.Point(140, 31)
        Me.ProgressFiller1.Name = "ProgressFiller1"
        Me.ProgressFiller1.Size = New System.Drawing.Size(143, 57)
        Me.ProgressFiller1.TabIndex = 1
        Me.ProgressFiller1.Visible = False
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.BackgroundInfoPanel1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(776, 362)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Turto duomenys"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'BackgroundInfoPanel1
        '
        Me.BackgroundInfoPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackgroundInfoPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BackgroundInfoPanel1.Location = New System.Drawing.Point(3, 3)
        Me.BackgroundInfoPanel1.Name = "BackgroundInfoPanel1"
        Me.BackgroundInfoPanel1.Size = New System.Drawing.Size(770, 356)
        Me.BackgroundInfoPanel1.TabIndex = 0
        '
        'ErrorWarnInfoProvider1
        '
        Me.ErrorWarnInfoProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.ErrorWarnInfoProvider1.BlinkStyleInformation = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.ErrorWarnInfoProvider1.BlinkStyleWarning = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.ErrorWarnInfoProvider1.ContainerControl = Me
        Me.ErrorWarnInfoProvider1.DataSource = Me.OperationDiscardBindingSource
        '
        'F_OperationDiscard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 430)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Panel2)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "F_OperationDiscard"
        Me.ShowInTaskbar = False
        Me.Text = "Ilgalaikio turto nurašymo operacija"
        Me.Panel2.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.OperationDiscardBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        CType(Me.ErrorWarnInfoProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents nCancelButton As System.Windows.Forms.Button
    Friend WithEvents ApplyButton As System.Windows.Forms.Button
    Friend WithEvents nOkButton As System.Windows.Forms.Button
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents IsComplexActCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents OperationDiscardBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents UpdateDateTextBox As System.Windows.Forms.TextBox
    Friend WithEvents InsertDateTextBox As System.Windows.Forms.TextBox
    Friend WithEvents IDTextBox As System.Windows.Forms.TextBox
    Friend WithEvents DocumentNumberTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ContentTextBox As System.Windows.Forms.TextBox
    Friend WithEvents AmountToDiscardAccTextBox As AccControlsWinForms.AccTextBox
    Friend WithEvents AccountCostsAccGridComboBox As AccControlsWinForms.AccListComboBox
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ViewJournalEntryButton As System.Windows.Forms.Button
    Friend WithEvents BackgroundInfoPanel1 As AccDataBindingsWinForms.BackgroundInfoPanel
    Friend WithEvents ProgressFiller2 As AccControlsWinForms.ProgressFiller
    Friend WithEvents ProgressFiller1 As AccControlsWinForms.ProgressFiller
    Friend WithEvents ErrorWarnInfoProvider1 As AccControlsWinForms.ErrorWarnInfoProvider
    Friend WithEvents DateAccDatePicker As AccControlsWinForms.AccDatePicker
End Class
