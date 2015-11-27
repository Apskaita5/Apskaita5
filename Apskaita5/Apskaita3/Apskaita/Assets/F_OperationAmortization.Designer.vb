<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F_OperationAmortization
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
        Dim ContentLabel As System.Windows.Forms.Label
        Dim AccountCostsLabel As System.Windows.Forms.Label
        Dim AmortizationCalculationsLabel As System.Windows.Forms.Label
        Dim DateLabel As System.Windows.Forms.Label
        Dim DocumentNumberLabel As System.Windows.Forms.Label
        Dim IDLabel As System.Windows.Forms.Label
        Dim InsertDateLabel As System.Windows.Forms.Label
        Dim UpdateDateLabel As System.Windows.Forms.Label
        Dim TotalValueChangeLabel As System.Windows.Forms.Label
        Dim RevaluedPortionTotalValueChangeLabel As System.Windows.Forms.Label
        Dim UnitValueChangeLabel As System.Windows.Forms.Label
        Dim RevaluedPortionUnitValueChangeLabel As System.Windows.Forms.Label
        Dim AmortizationCalculatedForMonthsLabel As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(F_OperationAmortization))
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.nCancelButton = New System.Windows.Forms.Button
        Me.ApplyButton = New System.Windows.Forms.Button
        Me.nOkButton = New System.Windows.Forms.Button
        Me.LimitationsButton = New System.Windows.Forms.Button
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.CalculateAmortizationButton = New System.Windows.Forms.Button
        Me.AmortizationCalculatedForMonthsAccTextBox = New AccControls.AccTextBox
        Me.OperationAmortizationBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ViewJournalEntryButton = New System.Windows.Forms.Button
        Me.RevaluedPortionUnitValueChangeAccTextBox = New AccControls.AccTextBox
        Me.UnitValueChangeAccTextBox = New AccControls.AccTextBox
        Me.RevaluedPortionTotalValueChangeAccTextBox = New AccControls.AccTextBox
        Me.UpdateDateTextBox = New System.Windows.Forms.TextBox
        Me.ContentTextBox = New System.Windows.Forms.TextBox
        Me.AccountCostsAccGridComboBox = New AccControls.AccGridComboBox
        Me.DateDateTimePicker = New System.Windows.Forms.DateTimePicker
        Me.IDTextBox = New System.Windows.Forms.TextBox
        Me.InsertDateTextBox = New System.Windows.Forms.TextBox
        Me.DocumentNumberTextBox = New System.Windows.Forms.TextBox
        Me.TotalValueChangeAccTextBox = New AccControls.AccTextBox
        Me.AmortizationCalculationsTextBox = New System.Windows.Forms.TextBox
        Me.BackgroundInfoPanel1 = New ApskaitaWUI.BackgroundInfoPanel
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.TabPage3 = New System.Windows.Forms.TabPage
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        ContentLabel = New System.Windows.Forms.Label
        AccountCostsLabel = New System.Windows.Forms.Label
        AmortizationCalculationsLabel = New System.Windows.Forms.Label
        DateLabel = New System.Windows.Forms.Label
        DocumentNumberLabel = New System.Windows.Forms.Label
        IDLabel = New System.Windows.Forms.Label
        InsertDateLabel = New System.Windows.Forms.Label
        UpdateDateLabel = New System.Windows.Forms.Label
        TotalValueChangeLabel = New System.Windows.Forms.Label
        RevaluedPortionTotalValueChangeLabel = New System.Windows.Forms.Label
        UnitValueChangeLabel = New System.Windows.Forms.Label
        RevaluedPortionUnitValueChangeLabel = New System.Windows.Forms.Label
        AmortizationCalculatedForMonthsLabel = New System.Windows.Forms.Label
        Me.Panel2.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.OperationAmortizationBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ContentLabel
        '
        ContentLabel.AutoSize = True
        ContentLabel.Dock = System.Windows.Forms.DockStyle.Fill
        ContentLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        ContentLabel.Location = New System.Drawing.Point(3, 50)
        ContentLabel.Name = "ContentLabel"
        ContentLabel.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        ContentLabel.Size = New System.Drawing.Size(139, 25)
        ContentLabel.TabIndex = 5
        ContentLabel.Text = "Aprašymas:"
        ContentLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'AccountCostsLabel
        '
        AccountCostsLabel.AutoSize = True
        AccountCostsLabel.Dock = System.Windows.Forms.DockStyle.Fill
        AccountCostsLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        AccountCostsLabel.Location = New System.Drawing.Point(532, 25)
        AccountCostsLabel.Name = "AccountCostsLabel"
        AccountCostsLabel.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        AccountCostsLabel.Size = New System.Drawing.Size(97, 25)
        AccountCostsLabel.TabIndex = 6
        AccountCostsLabel.Text = "Sąnaudų Sąsk.:"
        AccountCostsLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'AmortizationCalculationsLabel
        '
        AmortizationCalculationsLabel.AutoSize = True
        AmortizationCalculationsLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        AmortizationCalculationsLabel.Location = New System.Drawing.Point(5, 13)
        AmortizationCalculationsLabel.Name = "AmortizationCalculationsLabel"
        AmortizationCalculationsLabel.Size = New System.Drawing.Size(82, 13)
        AmortizationCalculationsLabel.TabIndex = 8
        AmortizationCalculationsLabel.Text = "Skaičiavimai:"
        AmortizationCalculationsLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'DateLabel
        '
        DateLabel.AutoSize = True
        DateLabel.Dock = System.Windows.Forms.DockStyle.Fill
        DateLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DateLabel.Location = New System.Drawing.Point(3, 25)
        DateLabel.Name = "DateLabel"
        DateLabel.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        DateLabel.Size = New System.Drawing.Size(139, 25)
        DateLabel.TabIndex = 9
        DateLabel.Text = "Data:"
        DateLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'DocumentNumberLabel
        '
        DocumentNumberLabel.AutoSize = True
        DocumentNumberLabel.Dock = System.Windows.Forms.DockStyle.Fill
        DocumentNumberLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DocumentNumberLabel.Location = New System.Drawing.Point(311, 25)
        DocumentNumberLabel.Name = "DocumentNumberLabel"
        DocumentNumberLabel.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        DocumentNumberLabel.Size = New System.Drawing.Size(57, 25)
        DocumentNumberLabel.TabIndex = 11
        DocumentNumberLabel.Text = "Dok Nr.:"
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
        IDLabel.Size = New System.Drawing.Size(139, 25)
        IDLabel.TabIndex = 13
        IDLabel.Text = "ID:"
        IDLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'InsertDateLabel
        '
        InsertDateLabel.AutoSize = True
        InsertDateLabel.Dock = System.Windows.Forms.DockStyle.Fill
        InsertDateLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        InsertDateLabel.Location = New System.Drawing.Point(311, 0)
        InsertDateLabel.Name = "InsertDateLabel"
        InsertDateLabel.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        InsertDateLabel.Size = New System.Drawing.Size(57, 25)
        InsertDateLabel.TabIndex = 15
        InsertDateLabel.Text = "Įtraukta:"
        InsertDateLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'UpdateDateLabel
        '
        UpdateDateLabel.AutoSize = True
        UpdateDateLabel.Dock = System.Windows.Forms.DockStyle.Fill
        UpdateDateLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        UpdateDateLabel.Location = New System.Drawing.Point(532, 0)
        UpdateDateLabel.Name = "UpdateDateLabel"
        UpdateDateLabel.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        UpdateDateLabel.Size = New System.Drawing.Size(97, 25)
        UpdateDateLabel.TabIndex = 17
        UpdateDateLabel.Text = "Pakeista:"
        UpdateDateLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'TotalValueChangeLabel
        '
        TotalValueChangeLabel.AutoSize = True
        TotalValueChangeLabel.Dock = System.Windows.Forms.DockStyle.Fill
        TotalValueChangeLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        TotalValueChangeLabel.Location = New System.Drawing.Point(3, 75)
        TotalValueChangeLabel.Name = "TotalValueChangeLabel"
        TotalValueChangeLabel.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        TotalValueChangeLabel.Size = New System.Drawing.Size(139, 25)
        TotalValueChangeLabel.TabIndex = 9
        TotalValueChangeLabel.Text = "Amortizacija Viso:"
        TotalValueChangeLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'RevaluedPortionTotalValueChangeLabel
        '
        RevaluedPortionTotalValueChangeLabel.AutoSize = True
        RevaluedPortionTotalValueChangeLabel.Dock = System.Windows.Forms.DockStyle.Fill
        RevaluedPortionTotalValueChangeLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        RevaluedPortionTotalValueChangeLabel.Location = New System.Drawing.Point(3, 100)
        RevaluedPortionTotalValueChangeLabel.Name = "RevaluedPortionTotalValueChangeLabel"
        RevaluedPortionTotalValueChangeLabel.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        RevaluedPortionTotalValueChangeLabel.Size = New System.Drawing.Size(139, 25)
        RevaluedPortionTotalValueChangeLabel.TabIndex = 10
        RevaluedPortionTotalValueChangeLabel.Text = "Perkainotai Daliai Viso:"
        RevaluedPortionTotalValueChangeLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'UnitValueChangeLabel
        '
        UnitValueChangeLabel.AutoSize = True
        UnitValueChangeLabel.Dock = System.Windows.Forms.DockStyle.Fill
        UnitValueChangeLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        UnitValueChangeLabel.Location = New System.Drawing.Point(311, 75)
        UnitValueChangeLabel.Name = "UnitValueChangeLabel"
        UnitValueChangeLabel.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        UnitValueChangeLabel.Size = New System.Drawing.Size(57, 25)
        UnitValueChangeLabel.TabIndex = 4
        UnitValueChangeLabel.Text = "Vienetui:"
        UnitValueChangeLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'RevaluedPortionUnitValueChangeLabel
        '
        RevaluedPortionUnitValueChangeLabel.AutoSize = True
        RevaluedPortionUnitValueChangeLabel.Dock = System.Windows.Forms.DockStyle.Fill
        RevaluedPortionUnitValueChangeLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        RevaluedPortionUnitValueChangeLabel.Location = New System.Drawing.Point(311, 100)
        RevaluedPortionUnitValueChangeLabel.Name = "RevaluedPortionUnitValueChangeLabel"
        RevaluedPortionUnitValueChangeLabel.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        RevaluedPortionUnitValueChangeLabel.Size = New System.Drawing.Size(57, 25)
        RevaluedPortionUnitValueChangeLabel.TabIndex = 5
        RevaluedPortionUnitValueChangeLabel.Text = "Vienetui:"
        RevaluedPortionUnitValueChangeLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'AmortizationCalculatedForMonthsLabel
        '
        AmortizationCalculatedForMonthsLabel.AutoSize = True
        AmortizationCalculatedForMonthsLabel.Dock = System.Windows.Forms.DockStyle.Fill
        AmortizationCalculatedForMonthsLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        AmortizationCalculatedForMonthsLabel.Location = New System.Drawing.Point(532, 75)
        AmortizationCalculatedForMonthsLabel.Name = "AmortizationCalculatedForMonthsLabel"
        AmortizationCalculatedForMonthsLabel.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        AmortizationCalculatedForMonthsLabel.Size = New System.Drawing.Size(97, 25)
        AmortizationCalculatedForMonthsLabel.TabIndex = 3
        AmortizationCalculatedForMonthsLabel.Text = "Už mėn.:"
        AmortizationCalculatedForMonthsLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Panel2
        '
        Me.Panel2.AutoSize = True
        Me.Panel2.Controls.Add(Me.nCancelButton)
        Me.Panel2.Controls.Add(Me.ApplyButton)
        Me.Panel2.Controls.Add(Me.nOkButton)
        Me.Panel2.Controls.Add(Me.LimitationsButton)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 383)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(0, 0, 0, 4)
        Me.Panel2.Size = New System.Drawing.Size(805, 44)
        Me.Panel2.TabIndex = 2
        '
        'nCancelButton
        '
        Me.nCancelButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.nCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.nCancelButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nCancelButton.Location = New System.Drawing.Point(718, 12)
        Me.nCancelButton.Name = "nCancelButton"
        Me.nCancelButton.Size = New System.Drawing.Size(75, 23)
        Me.nCancelButton.TabIndex = 2
        Me.nCancelButton.Text = "Atšaukti"
        Me.nCancelButton.UseVisualStyleBackColor = True
        '
        'ApplyButton
        '
        Me.ApplyButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ApplyButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ApplyButton.Location = New System.Drawing.Point(637, 12)
        Me.ApplyButton.Name = "ApplyButton"
        Me.ApplyButton.Size = New System.Drawing.Size(75, 23)
        Me.ApplyButton.TabIndex = 1
        Me.ApplyButton.Text = "Taikyti"
        Me.ApplyButton.UseVisualStyleBackColor = True
        '
        'nOkButton
        '
        Me.nOkButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.nOkButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nOkButton.Location = New System.Drawing.Point(556, 12)
        Me.nOkButton.Name = "nOkButton"
        Me.nOkButton.Size = New System.Drawing.Size(75, 23)
        Me.nOkButton.TabIndex = 0
        Me.nOkButton.Text = "OK"
        Me.nOkButton.UseVisualStyleBackColor = True
        '
        'LimitationsButton
        '
        Me.LimitationsButton.Image = Global.ApskaitaWUI.My.Resources.Resources.Action_lock_icon_16p
        Me.LimitationsButton.Location = New System.Drawing.Point(12, 9)
        Me.LimitationsButton.Name = "LimitationsButton"
        Me.LimitationsButton.Size = New System.Drawing.Size(28, 28)
        Me.LimitationsButton.TabIndex = 20
        Me.LimitationsButton.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 9
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 21.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.CalculateAmortizationButton, 6, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.AmortizationCalculatedForMonthsAccTextBox, 7, 3)
        Me.TableLayoutPanel1.Controls.Add(AmortizationCalculatedForMonthsLabel, 6, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.ViewJournalEntryButton, 2, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.RevaluedPortionUnitValueChangeAccTextBox, 4, 4)
        Me.TableLayoutPanel1.Controls.Add(RevaluedPortionUnitValueChangeLabel, 3, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.UnitValueChangeAccTextBox, 4, 3)
        Me.TableLayoutPanel1.Controls.Add(UnitValueChangeLabel, 3, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.RevaluedPortionTotalValueChangeAccTextBox, 1, 4)
        Me.TableLayoutPanel1.Controls.Add(RevaluedPortionTotalValueChangeLabel, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(DocumentNumberLabel, 3, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.UpdateDateTextBox, 7, 0)
        Me.TableLayoutPanel1.Controls.Add(TotalValueChangeLabel, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.ContentTextBox, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(ContentLabel, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.AccountCostsAccGridComboBox, 7, 1)
        Me.TableLayoutPanel1.Controls.Add(AccountCostsLabel, 6, 1)
        Me.TableLayoutPanel1.Controls.Add(UpdateDateLabel, 6, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.DateDateTimePicker, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(DateLabel, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(IDLabel, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.IDTextBox, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.InsertDateTextBox, 4, 0)
        Me.TableLayoutPanel1.Controls.Add(InsertDateLabel, 3, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.DocumentNumberTextBox, 4, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TotalValueChangeAccTextBox, 1, 3)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 6
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(791, 351)
        Me.TableLayoutPanel1.TabIndex = 4
        '
        'CalculateAmortizationButton
        '
        Me.CalculateAmortizationButton.Enabled = False
        Me.CalculateAmortizationButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CalculateAmortizationButton.Location = New System.Drawing.Point(529, 100)
        Me.CalculateAmortizationButton.Margin = New System.Windows.Forms.Padding(0)
        Me.CalculateAmortizationButton.Name = "CalculateAmortizationButton"
        Me.CalculateAmortizationButton.Size = New System.Drawing.Size(101, 23)
        Me.CalculateAmortizationButton.TabIndex = 21
        Me.CalculateAmortizationButton.Text = "Paskaičiuoti"
        Me.CalculateAmortizationButton.UseVisualStyleBackColor = True
        '
        'AmortizationCalculatedForMonthsAccTextBox
        '
        Me.AmortizationCalculatedForMonthsAccTextBox.DataBindings.Add(New System.Windows.Forms.Binding("DecimalValue", Me.OperationAmortizationBindingSource, "AmortizationCalculatedForMonths", True))
        Me.AmortizationCalculatedForMonthsAccTextBox.DecimalLength = 0
        Me.AmortizationCalculatedForMonthsAccTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AmortizationCalculatedForMonthsAccTextBox.KeepBackColorWhenReadOnly = False
        Me.AmortizationCalculatedForMonthsAccTextBox.Location = New System.Drawing.Point(635, 78)
        Me.AmortizationCalculatedForMonthsAccTextBox.Name = "AmortizationCalculatedForMonthsAccTextBox"
        Me.AmortizationCalculatedForMonthsAccTextBox.NegativeValue = False
        Me.AmortizationCalculatedForMonthsAccTextBox.Size = New System.Drawing.Size(132, 20)
        Me.AmortizationCalculatedForMonthsAccTextBox.TabIndex = 21
        Me.AmortizationCalculatedForMonthsAccTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'OperationAmortizationBindingSource
        '
        Me.OperationAmortizationBindingSource.DataSource = GetType(ApskaitaObjects.Assets.OperationAmortization)
        '
        'ViewJournalEntryButton
        '
        Me.ViewJournalEntryButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ViewJournalEntryButton.Image = Global.ApskaitaWUI.My.Resources.Resources.lektuvelis_16
        Me.ViewJournalEntryButton.Location = New System.Drawing.Point(284, 0)
        Me.ViewJournalEntryButton.Margin = New System.Windows.Forms.Padding(0)
        Me.ViewJournalEntryButton.Name = "ViewJournalEntryButton"
        Me.ViewJournalEntryButton.Size = New System.Drawing.Size(24, 24)
        Me.ViewJournalEntryButton.TabIndex = 19
        Me.ViewJournalEntryButton.UseVisualStyleBackColor = True
        '
        'RevaluedPortionUnitValueChangeAccTextBox
        '
        Me.RevaluedPortionUnitValueChangeAccTextBox.DataBindings.Add(New System.Windows.Forms.Binding("DecimalValue", Me.OperationAmortizationBindingSource, "RevaluedPortionUnitValueChange", True))
        Me.RevaluedPortionUnitValueChangeAccTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RevaluedPortionUnitValueChangeAccTextBox.KeepBackColorWhenReadOnly = False
        Me.RevaluedPortionUnitValueChangeAccTextBox.Location = New System.Drawing.Point(374, 103)
        Me.RevaluedPortionUnitValueChangeAccTextBox.Name = "RevaluedPortionUnitValueChangeAccTextBox"
        Me.RevaluedPortionUnitValueChangeAccTextBox.Size = New System.Drawing.Size(132, 20)
        Me.RevaluedPortionUnitValueChangeAccTextBox.TabIndex = 6
        Me.RevaluedPortionUnitValueChangeAccTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'UnitValueChangeAccTextBox
        '
        Me.UnitValueChangeAccTextBox.DataBindings.Add(New System.Windows.Forms.Binding("DecimalValue", Me.OperationAmortizationBindingSource, "UnitValueChange", True))
        Me.UnitValueChangeAccTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UnitValueChangeAccTextBox.KeepBackColorWhenReadOnly = False
        Me.UnitValueChangeAccTextBox.Location = New System.Drawing.Point(374, 78)
        Me.UnitValueChangeAccTextBox.Name = "UnitValueChangeAccTextBox"
        Me.UnitValueChangeAccTextBox.Size = New System.Drawing.Size(132, 20)
        Me.UnitValueChangeAccTextBox.TabIndex = 5
        Me.UnitValueChangeAccTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'RevaluedPortionTotalValueChangeAccTextBox
        '
        Me.RevaluedPortionTotalValueChangeAccTextBox.DataBindings.Add(New System.Windows.Forms.Binding("DecimalValue", Me.OperationAmortizationBindingSource, "RevaluedPortionTotalValueChange", True))
        Me.RevaluedPortionTotalValueChangeAccTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RevaluedPortionTotalValueChangeAccTextBox.KeepBackColorWhenReadOnly = False
        Me.RevaluedPortionTotalValueChangeAccTextBox.Location = New System.Drawing.Point(148, 103)
        Me.RevaluedPortionTotalValueChangeAccTextBox.Name = "RevaluedPortionTotalValueChangeAccTextBox"
        Me.RevaluedPortionTotalValueChangeAccTextBox.Size = New System.Drawing.Size(132, 20)
        Me.RevaluedPortionTotalValueChangeAccTextBox.TabIndex = 11
        Me.RevaluedPortionTotalValueChangeAccTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'UpdateDateTextBox
        '
        Me.UpdateDateTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OperationAmortizationBindingSource, "UpdateDate", True))
        Me.UpdateDateTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UpdateDateTextBox.Location = New System.Drawing.Point(635, 3)
        Me.UpdateDateTextBox.Name = "UpdateDateTextBox"
        Me.UpdateDateTextBox.ReadOnly = True
        Me.UpdateDateTextBox.Size = New System.Drawing.Size(132, 20)
        Me.UpdateDateTextBox.TabIndex = 18
        Me.UpdateDateTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'ContentTextBox
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.ContentTextBox, 7)
        Me.ContentTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OperationAmortizationBindingSource, "Content", True))
        Me.ContentTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ContentTextBox.Location = New System.Drawing.Point(148, 53)
        Me.ContentTextBox.MaxLength = 255
        Me.ContentTextBox.Name = "ContentTextBox"
        Me.ContentTextBox.Size = New System.Drawing.Size(619, 20)
        Me.ContentTextBox.TabIndex = 6
        '
        'AccountCostsAccGridComboBox
        '
        Me.AccountCostsAccGridComboBox.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.OperationAmortizationBindingSource, "AccountCosts", True))
        Me.AccountCostsAccGridComboBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AccountCostsAccGridComboBox.EmptyValueString = ""
        Me.AccountCostsAccGridComboBox.FilterPropertyName = ""
        Me.AccountCostsAccGridComboBox.FormattingEnabled = True
        Me.AccountCostsAccGridComboBox.InstantBinding = True
        Me.AccountCostsAccGridComboBox.Location = New System.Drawing.Point(635, 28)
        Me.AccountCostsAccGridComboBox.Name = "AccountCostsAccGridComboBox"
        Me.AccountCostsAccGridComboBox.Size = New System.Drawing.Size(132, 21)
        Me.AccountCostsAccGridComboBox.TabIndex = 7
        '
        'DateDateTimePicker
        '
        Me.DateDateTimePicker.DataBindings.Add(New System.Windows.Forms.Binding("Value", Me.OperationAmortizationBindingSource, "Date", True))
        Me.DateDateTimePicker.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DateDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateDateTimePicker.Location = New System.Drawing.Point(148, 28)
        Me.DateDateTimePicker.Name = "DateDateTimePicker"
        Me.DateDateTimePicker.Size = New System.Drawing.Size(132, 20)
        Me.DateDateTimePicker.TabIndex = 10
        '
        'IDTextBox
        '
        Me.IDTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OperationAmortizationBindingSource, "ID", True))
        Me.IDTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.IDTextBox.Location = New System.Drawing.Point(148, 3)
        Me.IDTextBox.Name = "IDTextBox"
        Me.IDTextBox.ReadOnly = True
        Me.IDTextBox.Size = New System.Drawing.Size(132, 20)
        Me.IDTextBox.TabIndex = 14
        Me.IDTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'InsertDateTextBox
        '
        Me.InsertDateTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OperationAmortizationBindingSource, "InsertDate", True))
        Me.InsertDateTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.InsertDateTextBox.Location = New System.Drawing.Point(374, 3)
        Me.InsertDateTextBox.Name = "InsertDateTextBox"
        Me.InsertDateTextBox.ReadOnly = True
        Me.InsertDateTextBox.Size = New System.Drawing.Size(132, 20)
        Me.InsertDateTextBox.TabIndex = 16
        Me.InsertDateTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'DocumentNumberTextBox
        '
        Me.DocumentNumberTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OperationAmortizationBindingSource, "DocumentNumber", True))
        Me.DocumentNumberTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DocumentNumberTextBox.Location = New System.Drawing.Point(374, 28)
        Me.DocumentNumberTextBox.MaxLength = 30
        Me.DocumentNumberTextBox.Name = "DocumentNumberTextBox"
        Me.DocumentNumberTextBox.Size = New System.Drawing.Size(132, 20)
        Me.DocumentNumberTextBox.TabIndex = 12
        Me.DocumentNumberTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TotalValueChangeAccTextBox
        '
        Me.TotalValueChangeAccTextBox.DataBindings.Add(New System.Windows.Forms.Binding("DecimalValue", Me.OperationAmortizationBindingSource, "TotalValueChange", True))
        Me.TotalValueChangeAccTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TotalValueChangeAccTextBox.KeepBackColorWhenReadOnly = False
        Me.TotalValueChangeAccTextBox.Location = New System.Drawing.Point(148, 78)
        Me.TotalValueChangeAccTextBox.Name = "TotalValueChangeAccTextBox"
        Me.TotalValueChangeAccTextBox.Size = New System.Drawing.Size(132, 20)
        Me.TotalValueChangeAccTextBox.TabIndex = 10
        Me.TotalValueChangeAccTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'AmortizationCalculationsTextBox
        '
        Me.AmortizationCalculationsTextBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AmortizationCalculationsTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OperationAmortizationBindingSource, "AmortizationCalculations", True))
        Me.AmortizationCalculationsTextBox.Location = New System.Drawing.Point(93, 10)
        Me.AmortizationCalculationsTextBox.MaxLength = 1000
        Me.AmortizationCalculationsTextBox.Multiline = True
        Me.AmortizationCalculationsTextBox.Name = "AmortizationCalculationsTextBox"
        Me.AmortizationCalculationsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.AmortizationCalculationsTextBox.Size = New System.Drawing.Size(675, 328)
        Me.AmortizationCalculationsTextBox.TabIndex = 9
        '
        'BackgroundInfoPanel1
        '
        Me.BackgroundInfoPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackgroundInfoPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BackgroundInfoPanel1.Location = New System.Drawing.Point(3, 3)
        Me.BackgroundInfoPanel1.Name = "BackgroundInfoPanel1"
        Me.BackgroundInfoPanel1.Size = New System.Drawing.Size(791, 351)
        Me.BackgroundInfoPanel1.TabIndex = 3
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(805, 383)
        Me.TabControl1.TabIndex = 5
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.TableLayoutPanel1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(797, 357)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Operacijos Duomenys"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.BackgroundInfoPanel1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(797, 357)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Turto duomenys"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(AmortizationCalculationsLabel)
        Me.TabPage3.Controls.Add(Me.AmortizationCalculationsTextBox)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(797, 357)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Paskaičiavimo aprašymas"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.ErrorProvider1.ContainerControl = Me
        Me.ErrorProvider1.DataSource = Me.OperationAmortizationBindingSource
        '
        'F_OperationAmortization
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(805, 427)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Panel2)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "F_OperationAmortization"
        Me.ShowInTaskbar = False
        Me.Text = "Ilgalaikio turto amortizacijos operacija"
        Me.Panel2.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.OperationAmortizationBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.TabPage3.PerformLayout()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents nCancelButton As System.Windows.Forms.Button
    Friend WithEvents ApplyButton As System.Windows.Forms.Button
    Friend WithEvents nOkButton As System.Windows.Forms.Button
    Friend WithEvents BackgroundInfoPanel1 As ApskaitaWUI.BackgroundInfoPanel
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents OperationAmortizationBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ContentTextBox As System.Windows.Forms.TextBox
    Friend WithEvents AccountCostsAccGridComboBox As AccControls.AccGridComboBox
    Friend WithEvents AmortizationCalculationsTextBox As System.Windows.Forms.TextBox
    Friend WithEvents DateDateTimePicker As System.Windows.Forms.DateTimePicker
    Friend WithEvents DocumentNumberTextBox As System.Windows.Forms.TextBox
    Friend WithEvents IDTextBox As System.Windows.Forms.TextBox
    Friend WithEvents InsertDateTextBox As System.Windows.Forms.TextBox
    Friend WithEvents UpdateDateTextBox As System.Windows.Forms.TextBox
    Friend WithEvents RevaluedPortionUnitValueChangeAccTextBox As AccControls.AccTextBox
    Friend WithEvents UnitValueChangeAccTextBox As AccControls.AccTextBox
    Friend WithEvents RevaluedPortionTotalValueChangeAccTextBox As AccControls.AccTextBox
    Friend WithEvents TotalValueChangeAccTextBox As AccControls.AccTextBox
    Friend WithEvents ViewJournalEntryButton As System.Windows.Forms.Button
    Friend WithEvents LimitationsButton As System.Windows.Forms.Button
    Friend WithEvents AmortizationCalculatedForMonthsAccTextBox As AccControls.AccTextBox
    Friend WithEvents CalculateAmortizationButton As System.Windows.Forms.Button
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage3 As System.Windows.Forms.TabPage
    Friend WithEvents ErrorProvider1 As System.Windows.Forms.ErrorProvider
End Class
