﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Friend Class F_InvoiceReceived
    Inherits System.Windows.Forms.Form

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
        Me.components = New System.ComponentModel.Container
        Dim AccountSupplierLabel As System.Windows.Forms.Label
        Dim CommentsInternalLabel As System.Windows.Forms.Label
        Dim ContentLabel As System.Windows.Forms.Label
        Dim CurrencyCodeLabel As System.Windows.Forms.Label
        Dim CurrencyRateLabel As System.Windows.Forms.Label
        Dim DateLabel As System.Windows.Forms.Label
        Dim IDLabel As System.Windows.Forms.Label
        Dim NumberLabel As System.Windows.Forms.Label
        Dim SumLabel As System.Windows.Forms.Label
        Dim SumLTLLabel As System.Windows.Forms.Label
        Dim SumTotalLabel As System.Windows.Forms.Label
        Dim SumTotalLTLLabel As System.Windows.Forms.Label
        Dim SumVatLabel As System.Windows.Forms.Label
        Dim SumVatLTLLabel As System.Windows.Forms.Label
        Dim SupplierLabel As System.Windows.Forms.Label
        Dim TypeHumanReadableLabel As System.Windows.Forms.Label
        Dim UpdateDateLabel As System.Windows.Forms.Label
        Dim InsertDateLabel As System.Windows.Forms.Label
        Dim ActualDateLabel As System.Windows.Forms.Label
        Dim IndirectVatAccountLabel As System.Windows.Forms.Label
        Dim IndirectVatCostsAccountLabel As System.Windows.Forms.Label
        Dim IndirectVatSumLabel As System.Windows.Forms.Label
        Dim IndirectVatDeclarationSchemaLabel As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(F_InvoiceReceived))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel
        Me.CalculateIndirectVatButton = New System.Windows.Forms.Button
        Me.IndirectVatCostsAccountAccGridComboBox = New AccControlsWinForms.AccListComboBox
        Me.InvoiceReceivedBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.IndirectVatAccountAccGridComboBox = New AccControlsWinForms.AccListComboBox
        Me.IndirectVatSumAccTextBox = New AccControlsWinForms.AccTextBox
        Me.TableLayoutPanel10 = New System.Windows.Forms.TableLayoutPanel
        Me.SumTotalAccTextBox = New AccControlsWinForms.AccTextBox
        Me.SumVatAccTextBox = New AccControlsWinForms.AccTextBox
        Me.SumAccTextBox = New AccControlsWinForms.AccTextBox
        Me.TableLayoutPanel8 = New System.Windows.Forms.TableLayoutPanel
        Me.CommentsInternalTextBox = New System.Windows.Forms.TextBox
        Me.TableLayoutPanel9 = New System.Windows.Forms.TableLayoutPanel
        Me.SumLTLAccTextBox = New AccControlsWinForms.AccTextBox
        Me.SumTotalLTLAccTextBox = New AccControlsWinForms.AccTextBox
        Me.SumVatLTLAccTextBox = New AccControlsWinForms.AccTextBox
        Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel
        Me.AccountSupplierAccGridComboBox = New AccControlsWinForms.AccListComboBox
        Me.ActualDateIsApplicableCheckBox = New System.Windows.Forms.CheckBox
        Me.NumberTextBox = New System.Windows.Forms.TextBox
        Me.ActualDateAccDatePicker = New AccControlsWinForms.AccDatePicker
        Me.TableLayoutPanel7 = New System.Windows.Forms.TableLayoutPanel
        Me.ContentTextBox = New System.Windows.Forms.TextBox
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel
        Me.GetCurrencyRatesButton = New System.Windows.Forms.Button
        Me.CurrencyCodeComboBox = New System.Windows.Forms.ComboBox
        Me.CurrencyRateAccTextBox = New AccControlsWinForms.AccTextBox
        Me.DateAccDatePicker = New AccControlsWinForms.AccDatePicker
        Me.TableLayoutPanel6 = New System.Windows.Forms.TableLayoutPanel
        Me.SupplierAccGridComboBox = New AccControlsWinForms.AccListComboBox
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel
        Me.ViewJournalEntryButton = New System.Windows.Forms.Button
        Me.IDTextBox = New System.Windows.Forms.TextBox
        Me.UpdateDateTextBox = New System.Windows.Forms.TextBox
        Me.InsertDateTextBox = New System.Windows.Forms.TextBox
        Me.TableLayoutPanel11 = New System.Windows.Forms.TableLayoutPanel
        Me.IndirectVatDeclarationSchemaAccListComboBox = New AccControlsWinForms.AccListComboBox
        Me.CopyInvoiceButton = New System.Windows.Forms.Button
        Me.PasteInvoiceButton = New System.Windows.Forms.Button
        Me.AddAttachedObjectButton = New System.Windows.Forms.Button
        Me.NewAdapterTypeComboBox = New System.Windows.Forms.ComboBox
        Me.TypeHumanReadableComboBox = New System.Windows.Forms.ComboBox
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.NewButton = New System.Windows.Forms.Button
        Me.ICancelButton = New System.Windows.Forms.Button
        Me.IOkButton = New System.Windows.Forms.Button
        Me.IApplyButton = New System.Windows.Forms.Button
        Me.InvoiceItemsSortedBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.InvoiceItemsDataListView = New BrightIdeasSoftware.DataListView
        Me.OlvColumn2 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn1 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn3 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn4 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn5 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn6 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn7 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn8 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn9 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn23 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn10 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn11 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn12 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn13 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn14 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn15 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn16 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn17 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn18 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn19 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn20 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn21 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn22 = New BrightIdeasSoftware.OLVColumn
        Me.ProgressFiller2 = New AccControlsWinForms.ProgressFiller
        Me.ProgressFiller1 = New AccControlsWinForms.ProgressFiller
        Me.ErrorWarnInfoProvider1 = New AccControlsWinForms.ErrorWarnInfoProvider(Me.components)
        AccountSupplierLabel = New System.Windows.Forms.Label
        CommentsInternalLabel = New System.Windows.Forms.Label
        ContentLabel = New System.Windows.Forms.Label
        CurrencyCodeLabel = New System.Windows.Forms.Label
        CurrencyRateLabel = New System.Windows.Forms.Label
        DateLabel = New System.Windows.Forms.Label
        IDLabel = New System.Windows.Forms.Label
        NumberLabel = New System.Windows.Forms.Label
        SumLabel = New System.Windows.Forms.Label
        SumLTLLabel = New System.Windows.Forms.Label
        SumTotalLabel = New System.Windows.Forms.Label
        SumTotalLTLLabel = New System.Windows.Forms.Label
        SumVatLabel = New System.Windows.Forms.Label
        SumVatLTLLabel = New System.Windows.Forms.Label
        SupplierLabel = New System.Windows.Forms.Label
        TypeHumanReadableLabel = New System.Windows.Forms.Label
        UpdateDateLabel = New System.Windows.Forms.Label
        InsertDateLabel = New System.Windows.Forms.Label
        ActualDateLabel = New System.Windows.Forms.Label
        IndirectVatAccountLabel = New System.Windows.Forms.Label
        IndirectVatCostsAccountLabel = New System.Windows.Forms.Label
        IndirectVatSumLabel = New System.Windows.Forms.Label
        IndirectVatDeclarationSchemaLabel = New System.Windows.Forms.Label
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        CType(Me.InvoiceReceivedBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel10.SuspendLayout()
        Me.TableLayoutPanel8.SuspendLayout()
        Me.TableLayoutPanel9.SuspendLayout()
        Me.TableLayoutPanel5.SuspendLayout()
        Me.TableLayoutPanel7.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        Me.TableLayoutPanel6.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TableLayoutPanel11.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.InvoiceItemsSortedBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.InvoiceItemsDataListView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ErrorWarnInfoProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'AccountSupplierLabel
        '
        AccountSupplierLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        AccountSupplierLabel.AutoSize = True
        AccountSupplierLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        AccountSupplierLabel.Location = New System.Drawing.Point(520, 3)
        AccountSupplierLabel.Margin = New System.Windows.Forms.Padding(0, 3, 0, 0)
        AccountSupplierLabel.Name = "AccountSupplierLabel"
        AccountSupplierLabel.Size = New System.Drawing.Size(87, 13)
        AccountSupplierLabel.TabIndex = 2
        AccountSupplierLabel.Text = "Tiekėjo sąsk.:"
        '
        'CommentsInternalLabel
        '
        CommentsInternalLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        CommentsInternalLabel.AutoSize = True
        CommentsInternalLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        CommentsInternalLabel.Location = New System.Drawing.Point(54, 161)
        CommentsInternalLabel.Margin = New System.Windows.Forms.Padding(0, 6, 0, 0)
        CommentsInternalLabel.Name = "CommentsInternalLabel"
        CommentsInternalLabel.Size = New System.Drawing.Size(74, 13)
        CommentsInternalLabel.TabIndex = 4
        CommentsInternalLabel.Text = "Komentarai:"
        '
        'ContentLabel
        '
        ContentLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        ContentLabel.AutoSize = True
        ContentLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        ContentLabel.Location = New System.Drawing.Point(76, 130)
        ContentLabel.Margin = New System.Windows.Forms.Padding(0, 6, 0, 0)
        ContentLabel.Name = "ContentLabel"
        ContentLabel.Size = New System.Drawing.Size(52, 13)
        ContentLabel.TabIndex = 6
        ContentLabel.Text = "Turinys:"
        '
        'CurrencyCodeLabel
        '
        CurrencyCodeLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        CurrencyCodeLabel.AutoSize = True
        CurrencyCodeLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        CurrencyCodeLabel.Location = New System.Drawing.Point(222, 3)
        CurrencyCodeLabel.Margin = New System.Windows.Forms.Padding(0, 3, 0, 0)
        CurrencyCodeLabel.Name = "CurrencyCodeLabel"
        CurrencyCodeLabel.Size = New System.Drawing.Size(50, 13)
        CurrencyCodeLabel.TabIndex = 8
        CurrencyCodeLabel.Text = "Valiuta:"
        '
        'CurrencyRateLabel
        '
        CurrencyRateLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        CurrencyRateLabel.AutoSize = True
        CurrencyRateLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        CurrencyRateLabel.Location = New System.Drawing.Point(494, 3)
        CurrencyRateLabel.Margin = New System.Windows.Forms.Padding(0, 3, 0, 0)
        CurrencyRateLabel.Name = "CurrencyRateLabel"
        CurrencyRateLabel.Size = New System.Drawing.Size(49, 13)
        CurrencyRateLabel.TabIndex = 10
        CurrencyRateLabel.Text = "Kursas:"
        '
        'DateLabel
        '
        DateLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DateLabel.AutoSize = True
        DateLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DateLabel.Location = New System.Drawing.Point(90, 37)
        DateLabel.Margin = New System.Windows.Forms.Padding(0, 6, 0, 0)
        DateLabel.Name = "DateLabel"
        DateLabel.Size = New System.Drawing.Size(38, 13)
        DateLabel.TabIndex = 12
        DateLabel.Text = "Data:"
        '
        'IDLabel
        '
        IDLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        IDLabel.AutoSize = True
        IDLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        IDLabel.Location = New System.Drawing.Point(104, 6)
        IDLabel.Margin = New System.Windows.Forms.Padding(0, 6, 0, 0)
        IDLabel.Name = "IDLabel"
        IDLabel.Size = New System.Drawing.Size(24, 13)
        IDLabel.TabIndex = 14
        IDLabel.Text = "ID:"
        '
        'NumberLabel
        '
        NumberLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        NumberLabel.AutoSize = True
        NumberLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        NumberLabel.Location = New System.Drawing.Point(204, 3)
        NumberLabel.Margin = New System.Windows.Forms.Padding(5, 3, 0, 0)
        NumberLabel.Name = "NumberLabel"
        NumberLabel.Size = New System.Drawing.Size(56, 13)
        NumberLabel.TabIndex = 16
        NumberLabel.Text = "Numeris:"
        '
        'SumLabel
        '
        SumLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        SumLabel.AutoSize = True
        SumLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        SumLabel.Location = New System.Drawing.Point(86, 223)
        SumLabel.Margin = New System.Windows.Forms.Padding(0, 6, 0, 0)
        SumLabel.Name = "SumLabel"
        SumLabel.Size = New System.Drawing.Size(42, 13)
        SumLabel.TabIndex = 18
        SumLabel.Text = "Suma:"
        '
        'SumLTLLabel
        '
        SumLTLLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        SumLTLLabel.AutoSize = True
        SumLTLLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        SumLTLLabel.Location = New System.Drawing.Point(60, 192)
        SumLTLLabel.Margin = New System.Windows.Forms.Padding(0, 6, 0, 0)
        SumLTLLabel.Name = "SumLTLLabel"
        SumLTLLabel.Size = New System.Drawing.Size(68, 13)
        SumLTLLabel.TabIndex = 20
        SumLTLLabel.Text = "Suma LTL:"
        '
        'SumTotalLabel
        '
        SumTotalLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        SumTotalLabel.AutoSize = True
        SumTotalLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        SumTotalLabel.Location = New System.Drawing.Point(516, 3)
        SumTotalLabel.Margin = New System.Windows.Forms.Padding(0, 3, 0, 0)
        SumTotalLabel.Name = "SumTotalLabel"
        SumTotalLabel.Size = New System.Drawing.Size(70, 13)
        SumTotalLabel.TabIndex = 22
        SumTotalLabel.Text = "Suma Viso:"
        '
        'SumTotalLTLLabel
        '
        SumTotalLTLLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        SumTotalLTLLabel.AutoSize = True
        SumTotalLTLLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        SumTotalLTLLabel.Location = New System.Drawing.Point(507, 3)
        SumTotalLTLLabel.Margin = New System.Windows.Forms.Padding(0, 3, 0, 0)
        SumTotalLTLLabel.Name = "SumTotalLTLLabel"
        SumTotalLTLLabel.Size = New System.Drawing.Size(96, 13)
        SumTotalLTLLabel.TabIndex = 24
        SumTotalLTLLabel.Text = "Suma Viso LTL:"
        '
        'SumVatLabel
        '
        SumVatLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        SumVatLabel.AutoSize = True
        SumVatLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        SumVatLabel.Location = New System.Drawing.Point(222, 3)
        SumVatLabel.Margin = New System.Windows.Forms.Padding(0, 3, 0, 0)
        SumVatLabel.Name = "SumVatLabel"
        SumVatLabel.Size = New System.Drawing.Size(72, 13)
        SumVatLabel.TabIndex = 26
        SumVatLabel.Text = "Suma PVM:"
        '
        'SumVatLTLLabel
        '
        SumVatLTLLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        SumVatLTLLabel.AutoSize = True
        SumVatLTLLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        SumVatLTLLabel.Location = New System.Drawing.Point(204, 3)
        SumVatLTLLabel.Margin = New System.Windows.Forms.Padding(0, 3, 0, 0)
        SumVatLTLLabel.Name = "SumVatLTLLabel"
        SumVatLTLLabel.Size = New System.Drawing.Size(98, 13)
        SumVatLTLLabel.TabIndex = 28
        SumVatLTLLabel.Text = "Suma PVM LTL:"
        '
        'SupplierLabel
        '
        SupplierLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        SupplierLabel.AutoSize = True
        SupplierLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        SupplierLabel.Location = New System.Drawing.Point(69, 99)
        SupplierLabel.Margin = New System.Windows.Forms.Padding(0, 6, 0, 0)
        SupplierLabel.Name = "SupplierLabel"
        SupplierLabel.Size = New System.Drawing.Size(59, 13)
        SupplierLabel.TabIndex = 30
        SupplierLabel.Text = "Tiekėjas:"
        '
        'TypeHumanReadableLabel
        '
        TypeHumanReadableLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        TypeHumanReadableLabel.AutoSize = True
        TypeHumanReadableLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        TypeHumanReadableLabel.Location = New System.Drawing.Point(212, 6)
        TypeHumanReadableLabel.Margin = New System.Windows.Forms.Padding(0, 6, 0, 0)
        TypeHumanReadableLabel.Name = "TypeHumanReadableLabel"
        TypeHumanReadableLabel.Size = New System.Drawing.Size(42, 13)
        TypeHumanReadableLabel.TabIndex = 56
        TypeHumanReadableLabel.Text = "Tipas:"
        '
        'UpdateDateLabel
        '
        UpdateDateLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        UpdateDateLabel.AutoSize = True
        UpdateDateLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        UpdateDateLabel.Location = New System.Drawing.Point(515, 3)
        UpdateDateLabel.Margin = New System.Windows.Forms.Padding(0, 3, 0, 0)
        UpdateDateLabel.Name = "UpdateDateLabel"
        UpdateDateLabel.Size = New System.Drawing.Size(60, 13)
        UpdateDateLabel.TabIndex = 57
        UpdateDateLabel.Text = "Pakeista:"
        '
        'InsertDateLabel
        '
        InsertDateLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        InsertDateLabel.AutoSize = True
        InsertDateLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        InsertDateLabel.Location = New System.Drawing.Point(227, 3)
        InsertDateLabel.Margin = New System.Windows.Forms.Padding(0, 3, 0, 0)
        InsertDateLabel.Name = "InsertDateLabel"
        InsertDateLabel.Size = New System.Drawing.Size(55, 13)
        InsertDateLabel.TabIndex = 58
        InsertDateLabel.Text = "Įtraukta:"
        '
        'ActualDateLabel
        '
        ActualDateLabel.AutoSize = True
        ActualDateLabel.Dock = System.Windows.Forms.DockStyle.Fill
        ActualDateLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        ActualDateLabel.Location = New System.Drawing.Point(0, 68)
        ActualDateLabel.Margin = New System.Windows.Forms.Padding(0, 6, 0, 0)
        ActualDateLabel.Name = "ActualDateLabel"
        ActualDateLabel.Size = New System.Drawing.Size(128, 25)
        ActualDateLabel.TabIndex = 4
        ActualDateLabel.Text = "Reali Data:"
        ActualDateLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'IndirectVatAccountLabel
        '
        IndirectVatAccountLabel.AutoSize = True
        IndirectVatAccountLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        IndirectVatAccountLabel.Location = New System.Drawing.Point(219, 3)
        IndirectVatAccountLabel.Margin = New System.Windows.Forms.Padding(0, 3, 0, 0)
        IndirectVatAccountLabel.Name = "IndirectVatAccountLabel"
        IndirectVatAccountLabel.Size = New System.Drawing.Size(73, 13)
        IndirectVatAccountLabel.TabIndex = 5
        IndirectVatAccountLabel.Text = "PVM Sąsk.:"
        '
        'IndirectVatCostsAccountLabel
        '
        IndirectVatCostsAccountLabel.AutoSize = True
        IndirectVatCostsAccountLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        IndirectVatCostsAccountLabel.Location = New System.Drawing.Point(486, 3)
        IndirectVatCostsAccountLabel.Margin = New System.Windows.Forms.Padding(0, 3, 0, 0)
        IndirectVatCostsAccountLabel.Name = "IndirectVatCostsAccountLabel"
        IndirectVatCostsAccountLabel.Size = New System.Drawing.Size(127, 13)
        IndirectVatCostsAccountLabel.TabIndex = 9
        IndirectVatCostsAccountLabel.Text = "PVM Sąnaudų Sąsk.:"
        '
        'IndirectVatSumLabel
        '
        IndirectVatSumLabel.AutoSize = True
        IndirectVatSumLabel.Dock = System.Windows.Forms.DockStyle.Fill
        IndirectVatSumLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        IndirectVatSumLabel.Location = New System.Drawing.Point(0, 254)
        IndirectVatSumLabel.Margin = New System.Windows.Forms.Padding(0, 6, 0, 0)
        IndirectVatSumLabel.Name = "IndirectVatSumLabel"
        IndirectVatSumLabel.Size = New System.Drawing.Size(128, 25)
        IndirectVatSumLabel.TabIndex = 11
        IndirectVatSumLabel.Text = "Netiesiog. PVM:"
        IndirectVatSumLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'IndirectVatDeclarationSchemaLabel
        '
        IndirectVatDeclarationSchemaLabel.AutoSize = True
        IndirectVatDeclarationSchemaLabel.Dock = System.Windows.Forms.DockStyle.Fill
        IndirectVatDeclarationSchemaLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        IndirectVatDeclarationSchemaLabel.Location = New System.Drawing.Point(0, 284)
        IndirectVatDeclarationSchemaLabel.Margin = New System.Windows.Forms.Padding(0, 5, 0, 0)
        IndirectVatDeclarationSchemaLabel.Name = "IndirectVatDeclarationSchemaLabel"
        IndirectVatDeclarationSchemaLabel.Size = New System.Drawing.Size(128, 35)
        IndirectVatDeclarationSchemaLabel.TabIndex = 5
        IndirectVatDeclarationSchemaLabel.Text = "Deklaravimo schema:"
        IndirectVatDeclarationSchemaLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Controls.Add(IndirectVatDeclarationSchemaLabel, 0, 9)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel3, 1, 8)
        Me.TableLayoutPanel1.Controls.Add(IndirectVatSumLabel, 0, 8)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel10, 1, 7)
        Me.TableLayoutPanel1.Controls.Add(ActualDateLabel, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel8, 1, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel9, 1, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel5, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel7, 1, 4)
        Me.TableLayoutPanel1.Controls.Add(SumLabel, 0, 7)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel4, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel6, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(IDLabel, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(ContentLabel, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(CommentsInternalLabel, 0, 5)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel2, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(DateLabel, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(SupplierLabel, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(SumLTLLabel, 0, 6)
        Me.TableLayoutPanel1.Controls.Add(Me.TableLayoutPanel11, 1, 9)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 10
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(937, 319)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 9
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.CalculateIndirectVatButton, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.IndirectVatCostsAccountAccGridComboBox, 7, 0)
        Me.TableLayoutPanel3.Controls.Add(IndirectVatCostsAccountLabel, 6, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.IndirectVatAccountAccGridComboBox, 4, 0)
        Me.TableLayoutPanel3.Controls.Add(IndirectVatAccountLabel, 3, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.IndirectVatSumAccTextBox, 1, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(128, 251)
        Me.TableLayoutPanel3.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 1
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(809, 25)
        Me.TableLayoutPanel3.TabIndex = 8
        '
        'CalculateIndirectVatButton
        '
        Me.CalculateIndirectVatButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CalculateIndirectVatButton.Image = Global.AccDataBindingsWinForms.My.Resources.Resources.calculator_16
        Me.CalculateIndirectVatButton.Location = New System.Drawing.Point(0, 0)
        Me.CalculateIndirectVatButton.Margin = New System.Windows.Forms.Padding(0)
        Me.CalculateIndirectVatButton.Name = "CalculateIndirectVatButton"
        Me.CalculateIndirectVatButton.Size = New System.Drawing.Size(25, 25)
        Me.CalculateIndirectVatButton.TabIndex = 0
        Me.CalculateIndirectVatButton.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CalculateIndirectVatButton.UseVisualStyleBackColor = True
        '
        'IndirectVatCostsAccountAccGridComboBox
        '
        Me.IndirectVatCostsAccountAccGridComboBox.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.InvoiceReceivedBindingSource, "IndirectVatCostsAccount", True))
        Me.IndirectVatCostsAccountAccGridComboBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.IndirectVatCostsAccountAccGridComboBox.EmptyValueString = ""
        Me.IndirectVatCostsAccountAccGridComboBox.InstantBinding = True
        Me.IndirectVatCostsAccountAccGridComboBox.Location = New System.Drawing.Point(615, 0)
        Me.IndirectVatCostsAccountAccGridComboBox.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.IndirectVatCostsAccountAccGridComboBox.Name = "IndirectVatCostsAccountAccGridComboBox"
        Me.IndirectVatCostsAccountAccGridComboBox.Size = New System.Drawing.Size(170, 21)
        Me.IndirectVatCostsAccountAccGridComboBox.TabIndex = 2
        '
        'InvoiceReceivedBindingSource
        '
        Me.InvoiceReceivedBindingSource.DataSource = GetType(ApskaitaObjects.Documents.InvoiceReceived)
        '
        'IndirectVatAccountAccGridComboBox
        '
        Me.IndirectVatAccountAccGridComboBox.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.InvoiceReceivedBindingSource, "IndirectVatAccount", True))
        Me.IndirectVatAccountAccGridComboBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.IndirectVatAccountAccGridComboBox.EmptyValueString = ""
        Me.IndirectVatAccountAccGridComboBox.InstantBinding = True
        Me.IndirectVatAccountAccGridComboBox.Location = New System.Drawing.Point(294, 0)
        Me.IndirectVatAccountAccGridComboBox.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.IndirectVatAccountAccGridComboBox.Name = "IndirectVatAccountAccGridComboBox"
        Me.IndirectVatAccountAccGridComboBox.Size = New System.Drawing.Size(170, 21)
        Me.IndirectVatAccountAccGridComboBox.TabIndex = 1
        '
        'IndirectVatSumAccTextBox
        '
        Me.IndirectVatSumAccTextBox.DataBindings.Add(New System.Windows.Forms.Binding("DecimalValue", Me.InvoiceReceivedBindingSource, "IndirectVatSum", True))
        Me.IndirectVatSumAccTextBox.Location = New System.Drawing.Point(27, 0)
        Me.IndirectVatSumAccTextBox.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.IndirectVatSumAccTextBox.Name = "IndirectVatSumAccTextBox"
        Me.IndirectVatSumAccTextBox.NegativeValue = False
        Me.IndirectVatSumAccTextBox.Size = New System.Drawing.Size(163, 20)
        Me.IndirectVatSumAccTextBox.TabIndex = 0
        Me.IndirectVatSumAccTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TableLayoutPanel10
        '
        Me.TableLayoutPanel10.ColumnCount = 8
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33332!))
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334!))
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334!))
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel10.Controls.Add(Me.SumTotalAccTextBox, 6, 0)
        Me.TableLayoutPanel10.Controls.Add(Me.SumVatAccTextBox, 3, 0)
        Me.TableLayoutPanel10.Controls.Add(SumVatLabel, 2, 0)
        Me.TableLayoutPanel10.Controls.Add(Me.SumAccTextBox, 0, 0)
        Me.TableLayoutPanel10.Controls.Add(SumTotalLabel, 5, 0)
        Me.TableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel10.Location = New System.Drawing.Point(128, 220)
        Me.TableLayoutPanel10.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.TableLayoutPanel10.Name = "TableLayoutPanel10"
        Me.TableLayoutPanel10.RowCount = 1
        Me.TableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel10.Size = New System.Drawing.Size(809, 25)
        Me.TableLayoutPanel10.TabIndex = 7
        '
        'SumTotalAccTextBox
        '
        Me.SumTotalAccTextBox.DataBindings.Add(New System.Windows.Forms.Binding("DecimalValue", Me.InvoiceReceivedBindingSource, "SumTotal", True))
        Me.SumTotalAccTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SumTotalAccTextBox.Location = New System.Drawing.Point(588, 0)
        Me.SumTotalAccTextBox.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.SumTotalAccTextBox.Name = "SumTotalAccTextBox"
        Me.SumTotalAccTextBox.ReadOnly = True
        Me.SumTotalAccTextBox.Size = New System.Drawing.Size(198, 20)
        Me.SumTotalAccTextBox.TabIndex = 23
        Me.SumTotalAccTextBox.TabStop = False
        Me.SumTotalAccTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'SumVatAccTextBox
        '
        Me.SumVatAccTextBox.DataBindings.Add(New System.Windows.Forms.Binding("DecimalValue", Me.InvoiceReceivedBindingSource, "SumVat", True))
        Me.SumVatAccTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SumVatAccTextBox.Location = New System.Drawing.Point(296, 0)
        Me.SumVatAccTextBox.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.SumVatAccTextBox.Name = "SumVatAccTextBox"
        Me.SumVatAccTextBox.ReadOnly = True
        Me.SumVatAccTextBox.Size = New System.Drawing.Size(198, 20)
        Me.SumVatAccTextBox.TabIndex = 27
        Me.SumVatAccTextBox.TabStop = False
        Me.SumVatAccTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'SumAccTextBox
        '
        Me.SumAccTextBox.DataBindings.Add(New System.Windows.Forms.Binding("DecimalValue", Me.InvoiceReceivedBindingSource, "Sum", True))
        Me.SumAccTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SumAccTextBox.Location = New System.Drawing.Point(2, 0)
        Me.SumAccTextBox.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.SumAccTextBox.Name = "SumAccTextBox"
        Me.SumAccTextBox.ReadOnly = True
        Me.SumAccTextBox.Size = New System.Drawing.Size(198, 20)
        Me.SumAccTextBox.TabIndex = 19
        Me.SumAccTextBox.TabStop = False
        Me.SumAccTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TableLayoutPanel8
        '
        Me.TableLayoutPanel8.ColumnCount = 2
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel8.Controls.Add(Me.CommentsInternalTextBox, 0, 0)
        Me.TableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel8.Location = New System.Drawing.Point(128, 158)
        Me.TableLayoutPanel8.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.TableLayoutPanel8.Name = "TableLayoutPanel8"
        Me.TableLayoutPanel8.RowCount = 1
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel8.Size = New System.Drawing.Size(809, 25)
        Me.TableLayoutPanel8.TabIndex = 5
        '
        'CommentsInternalTextBox
        '
        Me.CommentsInternalTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.InvoiceReceivedBindingSource, "CommentsInternal", True))
        Me.CommentsInternalTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CommentsInternalTextBox.Location = New System.Drawing.Point(2, 0)
        Me.CommentsInternalTextBox.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.CommentsInternalTextBox.MaxLength = 255
        Me.CommentsInternalTextBox.Name = "CommentsInternalTextBox"
        Me.CommentsInternalTextBox.Size = New System.Drawing.Size(785, 20)
        Me.CommentsInternalTextBox.TabIndex = 0
        '
        'TableLayoutPanel9
        '
        Me.TableLayoutPanel9.ColumnCount = 8
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33332!))
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334!))
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334!))
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel9.Controls.Add(Me.SumLTLAccTextBox, 0, 0)
        Me.TableLayoutPanel9.Controls.Add(SumTotalLTLLabel, 5, 0)
        Me.TableLayoutPanel9.Controls.Add(Me.SumTotalLTLAccTextBox, 6, 0)
        Me.TableLayoutPanel9.Controls.Add(SumVatLTLLabel, 2, 0)
        Me.TableLayoutPanel9.Controls.Add(Me.SumVatLTLAccTextBox, 3, 0)
        Me.TableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel9.Location = New System.Drawing.Point(128, 189)
        Me.TableLayoutPanel9.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.TableLayoutPanel9.Name = "TableLayoutPanel9"
        Me.TableLayoutPanel9.RowCount = 1
        Me.TableLayoutPanel9.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel9.Size = New System.Drawing.Size(809, 25)
        Me.TableLayoutPanel9.TabIndex = 6
        '
        'SumLTLAccTextBox
        '
        Me.SumLTLAccTextBox.DataBindings.Add(New System.Windows.Forms.Binding("DecimalValue", Me.InvoiceReceivedBindingSource, "SumLTL", True))
        Me.SumLTLAccTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SumLTLAccTextBox.Location = New System.Drawing.Point(2, 0)
        Me.SumLTLAccTextBox.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.SumLTLAccTextBox.Name = "SumLTLAccTextBox"
        Me.SumLTLAccTextBox.ReadOnly = True
        Me.SumLTLAccTextBox.Size = New System.Drawing.Size(180, 20)
        Me.SumLTLAccTextBox.TabIndex = 21
        Me.SumLTLAccTextBox.TabStop = False
        Me.SumLTLAccTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'SumTotalLTLAccTextBox
        '
        Me.SumTotalLTLAccTextBox.DataBindings.Add(New System.Windows.Forms.Binding("DecimalValue", Me.InvoiceReceivedBindingSource, "SumTotalLTL", True))
        Me.SumTotalLTLAccTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SumTotalLTLAccTextBox.Location = New System.Drawing.Point(605, 0)
        Me.SumTotalLTLAccTextBox.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.SumTotalLTLAccTextBox.Name = "SumTotalLTLAccTextBox"
        Me.SumTotalLTLAccTextBox.ReadOnly = True
        Me.SumTotalLTLAccTextBox.Size = New System.Drawing.Size(181, 20)
        Me.SumTotalLTLAccTextBox.TabIndex = 25
        Me.SumTotalLTLAccTextBox.TabStop = False
        Me.SumTotalLTLAccTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'SumVatLTLAccTextBox
        '
        Me.SumVatLTLAccTextBox.DataBindings.Add(New System.Windows.Forms.Binding("DecimalValue", Me.InvoiceReceivedBindingSource, "SumVatLTL", True))
        Me.SumVatLTLAccTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SumVatLTLAccTextBox.Location = New System.Drawing.Point(304, 0)
        Me.SumVatLTLAccTextBox.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.SumVatLTLAccTextBox.Name = "SumVatLTLAccTextBox"
        Me.SumVatLTLAccTextBox.ReadOnly = True
        Me.SumVatLTLAccTextBox.Size = New System.Drawing.Size(181, 20)
        Me.SumVatLTLAccTextBox.TabIndex = 29
        Me.SumVatLTLAccTextBox.TabStop = False
        Me.SumVatLTLAccTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TableLayoutPanel5
        '
        Me.TableLayoutPanel5.AutoScroll = True
        Me.TableLayoutPanel5.ColumnCount = 8
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.Controls.Add(Me.AccountSupplierAccGridComboBox, 6, 0)
        Me.TableLayoutPanel5.Controls.Add(AccountSupplierLabel, 5, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.ActualDateIsApplicableCheckBox, 0, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.NumberTextBox, 3, 0)
        Me.TableLayoutPanel5.Controls.Add(NumberLabel, 2, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.ActualDateAccDatePicker, 1, 0)
        Me.TableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(128, 65)
        Me.TableLayoutPanel5.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 1
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(809, 25)
        Me.TableLayoutPanel5.TabIndex = 2
        '
        'AccountSupplierAccGridComboBox
        '
        Me.AccountSupplierAccGridComboBox.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.InvoiceReceivedBindingSource, "AccountSupplier", True))
        Me.AccountSupplierAccGridComboBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AccountSupplierAccGridComboBox.EmptyValueString = ""
        Me.AccountSupplierAccGridComboBox.InstantBinding = True
        Me.AccountSupplierAccGridComboBox.Location = New System.Drawing.Point(609, 0)
        Me.AccountSupplierAccGridComboBox.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.AccountSupplierAccGridComboBox.Name = "AccountSupplierAccGridComboBox"
        Me.AccountSupplierAccGridComboBox.Size = New System.Drawing.Size(176, 21)
        Me.AccountSupplierAccGridComboBox.TabIndex = 3
        '
        'ActualDateIsApplicableCheckBox
        '
        Me.ActualDateIsApplicableCheckBox.AutoSize = True
        Me.ActualDateIsApplicableCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("CheckState", Me.InvoiceReceivedBindingSource, "ActualDateIsApplicable", True))
        Me.ActualDateIsApplicableCheckBox.Location = New System.Drawing.Point(2, 3)
        Me.ActualDateIsApplicableCheckBox.Margin = New System.Windows.Forms.Padding(2, 3, 2, 0)
        Me.ActualDateIsApplicableCheckBox.Name = "ActualDateIsApplicableCheckBox"
        Me.ActualDateIsApplicableCheckBox.Size = New System.Drawing.Size(15, 14)
        Me.ActualDateIsApplicableCheckBox.TabIndex = 0
        '
        'NumberTextBox
        '
        Me.NumberTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.InvoiceReceivedBindingSource, "Number", True))
        Me.NumberTextBox.Location = New System.Drawing.Point(262, 0)
        Me.NumberTextBox.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.NumberTextBox.MaxLength = 50
        Me.NumberTextBox.Name = "NumberTextBox"
        Me.NumberTextBox.Size = New System.Drawing.Size(228, 20)
        Me.NumberTextBox.TabIndex = 2
        Me.NumberTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'ActualDateAccDatePicker
        '
        Me.ActualDateAccDatePicker.BoldedDates = Nothing
        Me.ActualDateAccDatePicker.DataBindings.Add(New System.Windows.Forms.Binding("Value", Me.InvoiceReceivedBindingSource, "ActualDate", True))
        Me.ActualDateAccDatePicker.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ActualDateAccDatePicker.Location = New System.Drawing.Point(21, 0)
        Me.ActualDateAccDatePicker.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.ActualDateAccDatePicker.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.ActualDateAccDatePicker.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.ActualDateAccDatePicker.Name = "ActualDateAccDatePicker"
        Me.ActualDateAccDatePicker.ReadOnly = False
        Me.ActualDateAccDatePicker.ShowWeekNumbers = True
        Me.ActualDateAccDatePicker.Size = New System.Drawing.Size(176, 25)
        Me.ActualDateAccDatePicker.TabIndex = 17
        '
        'TableLayoutPanel7
        '
        Me.TableLayoutPanel7.ColumnCount = 2
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel7.Controls.Add(Me.ContentTextBox, 0, 0)
        Me.TableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel7.Location = New System.Drawing.Point(128, 127)
        Me.TableLayoutPanel7.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.TableLayoutPanel7.Name = "TableLayoutPanel7"
        Me.TableLayoutPanel7.RowCount = 1
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel7.Size = New System.Drawing.Size(809, 25)
        Me.TableLayoutPanel7.TabIndex = 4
        '
        'ContentTextBox
        '
        Me.ContentTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.InvoiceReceivedBindingSource, "Content", True))
        Me.ContentTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ContentTextBox.Location = New System.Drawing.Point(2, 0)
        Me.ContentTextBox.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.ContentTextBox.MaxLength = 255
        Me.ContentTextBox.Name = "ContentTextBox"
        Me.ContentTextBox.Size = New System.Drawing.Size(785, 20)
        Me.ContentTextBox.TabIndex = 0
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.AutoScroll = True
        Me.TableLayoutPanel4.ColumnCount = 9
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33444!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33445!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33111!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.GetCurrencyRatesButton, 6, 0)
        Me.TableLayoutPanel4.Controls.Add(CurrencyCodeLabel, 2, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.CurrencyCodeComboBox, 3, 0)
        Me.TableLayoutPanel4.Controls.Add(CurrencyRateLabel, 5, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.CurrencyRateAccTextBox, 7, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.DateAccDatePicker, 0, 0)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(128, 34)
        Me.TableLayoutPanel4.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 1
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(809, 25)
        Me.TableLayoutPanel4.TabIndex = 1
        '
        'GetCurrencyRatesButton
        '
        Me.GetCurrencyRatesButton.AutoSize = True
        Me.GetCurrencyRatesButton.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.GetCurrencyRatesButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GetCurrencyRatesButton.Location = New System.Drawing.Point(543, 0)
        Me.GetCurrencyRatesButton.Margin = New System.Windows.Forms.Padding(0)
        Me.GetCurrencyRatesButton.Name = "GetCurrencyRatesButton"
        Me.GetCurrencyRatesButton.Size = New System.Drawing.Size(42, 23)
        Me.GetCurrencyRatesButton.TabIndex = 2
        Me.GetCurrencyRatesButton.Text = "$->€"
        Me.GetCurrencyRatesButton.UseVisualStyleBackColor = True
        '
        'CurrencyCodeComboBox
        '
        Me.CurrencyCodeComboBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.InvoiceReceivedBindingSource, "CurrencyCode", True))
        Me.CurrencyCodeComboBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CurrencyCodeComboBox.FormattingEnabled = True
        Me.CurrencyCodeComboBox.Location = New System.Drawing.Point(274, 0)
        Me.CurrencyCodeComboBox.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.CurrencyCodeComboBox.Name = "CurrencyCodeComboBox"
        Me.CurrencyCodeComboBox.Size = New System.Drawing.Size(198, 21)
        Me.CurrencyCodeComboBox.TabIndex = 1
        '
        'CurrencyRateAccTextBox
        '
        Me.CurrencyRateAccTextBox.DataBindings.Add(New System.Windows.Forms.Binding("DecimalValue", Me.InvoiceReceivedBindingSource, "CurrencyRate", True))
        Me.CurrencyRateAccTextBox.DecimalLength = 6
        Me.CurrencyRateAccTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CurrencyRateAccTextBox.Location = New System.Drawing.Point(587, 0)
        Me.CurrencyRateAccTextBox.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.CurrencyRateAccTextBox.Name = "CurrencyRateAccTextBox"
        Me.CurrencyRateAccTextBox.NegativeValue = False
        Me.CurrencyRateAccTextBox.Size = New System.Drawing.Size(198, 20)
        Me.CurrencyRateAccTextBox.TabIndex = 3
        Me.CurrencyRateAccTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'DateAccDatePicker
        '
        Me.DateAccDatePicker.BoldedDates = Nothing
        Me.DateAccDatePicker.DataBindings.Add(New System.Windows.Forms.Binding("Value", Me.InvoiceReceivedBindingSource, "Date", True))
        Me.DateAccDatePicker.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DateAccDatePicker.Location = New System.Drawing.Point(2, 0)
        Me.DateAccDatePicker.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.DateAccDatePicker.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.DateAccDatePicker.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.DateAccDatePicker.Name = "DateAccDatePicker"
        Me.DateAccDatePicker.ReadOnly = False
        Me.DateAccDatePicker.ShowWeekNumbers = True
        Me.DateAccDatePicker.Size = New System.Drawing.Size(198, 25)
        Me.DateAccDatePicker.TabIndex = 0
        '
        'TableLayoutPanel6
        '
        Me.TableLayoutPanel6.ColumnCount = 2
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel6.Controls.Add(Me.SupplierAccGridComboBox, 0, 0)
        Me.TableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel6.Location = New System.Drawing.Point(128, 96)
        Me.TableLayoutPanel6.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.TableLayoutPanel6.Name = "TableLayoutPanel6"
        Me.TableLayoutPanel6.RowCount = 1
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel6.Size = New System.Drawing.Size(809, 25)
        Me.TableLayoutPanel6.TabIndex = 3
        '
        'SupplierAccGridComboBox
        '
        Me.SupplierAccGridComboBox.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.InvoiceReceivedBindingSource, "Supplier", True))
        Me.SupplierAccGridComboBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SupplierAccGridComboBox.EmptyValueString = ""
        Me.SupplierAccGridComboBox.InstantBinding = True
        Me.SupplierAccGridComboBox.Location = New System.Drawing.Point(2, 0)
        Me.SupplierAccGridComboBox.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.SupplierAccGridComboBox.Name = "SupplierAccGridComboBox"
        Me.SupplierAccGridComboBox.Size = New System.Drawing.Size(785, 21)
        Me.SupplierAccGridComboBox.TabIndex = 0
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 9
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.ViewJournalEntryButton, 2, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.IDTextBox, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.UpdateDateTextBox, 7, 0)
        Me.TableLayoutPanel2.Controls.Add(UpdateDateLabel, 6, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.InsertDateTextBox, 4, 0)
        Me.TableLayoutPanel2.Controls.Add(InsertDateLabel, 3, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(128, 3)
        Me.TableLayoutPanel2.Margin = New System.Windows.Forms.Padding(0, 3, 0, 3)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 1
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(809, 25)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'ViewJournalEntryButton
        '
        Me.ViewJournalEntryButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ViewJournalEntryButton.Image = Global.AccDataBindingsWinForms.My.Resources.Resources.lektuvelis_16
        Me.ViewJournalEntryButton.Location = New System.Drawing.Point(203, 0)
        Me.ViewJournalEntryButton.Margin = New System.Windows.Forms.Padding(0)
        Me.ViewJournalEntryButton.Name = "ViewJournalEntryButton"
        Me.ViewJournalEntryButton.Size = New System.Drawing.Size(24, 24)
        Me.ViewJournalEntryButton.TabIndex = 90
        Me.ViewJournalEntryButton.TabStop = False
        Me.ViewJournalEntryButton.UseVisualStyleBackColor = True
        '
        'IDTextBox
        '
        Me.IDTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.InvoiceReceivedBindingSource, "ID", True))
        Me.IDTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.IDTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IDTextBox.Location = New System.Drawing.Point(2, 0)
        Me.IDTextBox.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.IDTextBox.Name = "IDTextBox"
        Me.IDTextBox.ReadOnly = True
        Me.IDTextBox.Size = New System.Drawing.Size(179, 20)
        Me.IDTextBox.TabIndex = 15
        Me.IDTextBox.TabStop = False
        Me.IDTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'UpdateDateTextBox
        '
        Me.UpdateDateTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.InvoiceReceivedBindingSource, "UpdateDate", True))
        Me.UpdateDateTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UpdateDateTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.UpdateDateTextBox.Location = New System.Drawing.Point(577, 0)
        Me.UpdateDateTextBox.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.UpdateDateTextBox.Name = "UpdateDateTextBox"
        Me.UpdateDateTextBox.ReadOnly = True
        Me.UpdateDateTextBox.Size = New System.Drawing.Size(209, 20)
        Me.UpdateDateTextBox.TabIndex = 58
        Me.UpdateDateTextBox.TabStop = False
        Me.UpdateDateTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'InsertDateTextBox
        '
        Me.InsertDateTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.InvoiceReceivedBindingSource, "InsertDate", True))
        Me.InsertDateTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.InsertDateTextBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.InsertDateTextBox.Location = New System.Drawing.Point(284, 0)
        Me.InsertDateTextBox.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.InsertDateTextBox.Name = "InsertDateTextBox"
        Me.InsertDateTextBox.ReadOnly = True
        Me.InsertDateTextBox.Size = New System.Drawing.Size(209, 20)
        Me.InsertDateTextBox.TabIndex = 59
        Me.InsertDateTextBox.TabStop = False
        Me.InsertDateTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TableLayoutPanel11
        '
        Me.TableLayoutPanel11.ColumnCount = 12
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 21.0!))
        Me.TableLayoutPanel11.Controls.Add(Me.IndirectVatDeclarationSchemaAccListComboBox, 0, 0)
        Me.TableLayoutPanel11.Controls.Add(Me.CopyInvoiceButton, 10, 0)
        Me.TableLayoutPanel11.Controls.Add(Me.PasteInvoiceButton, 8, 0)
        Me.TableLayoutPanel11.Controls.Add(Me.AddAttachedObjectButton, 6, 0)
        Me.TableLayoutPanel11.Controls.Add(Me.NewAdapterTypeComboBox, 5, 0)
        Me.TableLayoutPanel11.Controls.Add(Me.TypeHumanReadableComboBox, 3, 0)
        Me.TableLayoutPanel11.Controls.Add(TypeHumanReadableLabel, 2, 0)
        Me.TableLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel11.Location = New System.Drawing.Point(128, 279)
        Me.TableLayoutPanel11.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel11.Name = "TableLayoutPanel11"
        Me.TableLayoutPanel11.RowCount = 1
        Me.TableLayoutPanel11.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel11.Size = New System.Drawing.Size(809, 40)
        Me.TableLayoutPanel11.TabIndex = 9
        '
        'IndirectVatDeclarationSchemaAccListComboBox
        '
        Me.IndirectVatDeclarationSchemaAccListComboBox.DataBindings.Add(New System.Windows.Forms.Binding("SelectedValue", Me.InvoiceReceivedBindingSource, "IndirectVatDeclarationSchema", True))
        Me.IndirectVatDeclarationSchemaAccListComboBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.IndirectVatDeclarationSchemaAccListComboBox.EmptyValueString = ""
        Me.IndirectVatDeclarationSchemaAccListComboBox.InstantBinding = True
        Me.IndirectVatDeclarationSchemaAccListComboBox.Location = New System.Drawing.Point(3, 3)
        Me.IndirectVatDeclarationSchemaAccListComboBox.Name = "IndirectVatDeclarationSchemaAccListComboBox"
        Me.IndirectVatDeclarationSchemaAccListComboBox.Size = New System.Drawing.Size(186, 21)
        Me.IndirectVatDeclarationSchemaAccListComboBox.TabIndex = 0
        '
        'CopyInvoiceButton
        '
        Me.CopyInvoiceButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CopyInvoiceButton.Image = Global.AccDataBindingsWinForms.My.Resources.Resources.Actions_edit_copy_icon_16p
        Me.CopyInvoiceButton.Location = New System.Drawing.Point(758, 0)
        Me.CopyInvoiceButton.Margin = New System.Windows.Forms.Padding(0)
        Me.CopyInvoiceButton.Name = "CopyInvoiceButton"
        Me.CopyInvoiceButton.Size = New System.Drawing.Size(30, 29)
        Me.CopyInvoiceButton.TabIndex = 5
        Me.CopyInvoiceButton.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.CopyInvoiceButton.UseVisualStyleBackColor = True
        '
        'PasteInvoiceButton
        '
        Me.PasteInvoiceButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.PasteInvoiceButton.Image = Global.AccDataBindingsWinForms.My.Resources.Resources.Paste_icon_16p
        Me.PasteInvoiceButton.Location = New System.Drawing.Point(708, 0)
        Me.PasteInvoiceButton.Margin = New System.Windows.Forms.Padding(0)
        Me.PasteInvoiceButton.Name = "PasteInvoiceButton"
        Me.PasteInvoiceButton.Size = New System.Drawing.Size(30, 29)
        Me.PasteInvoiceButton.TabIndex = 4
        Me.PasteInvoiceButton.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.PasteInvoiceButton.UseVisualStyleBackColor = True
        '
        'AddAttachedObjectButton
        '
        Me.AddAttachedObjectButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AddAttachedObjectButton.Location = New System.Drawing.Point(658, 0)
        Me.AddAttachedObjectButton.Margin = New System.Windows.Forms.Padding(0)
        Me.AddAttachedObjectButton.Name = "AddAttachedObjectButton"
        Me.AddAttachedObjectButton.Size = New System.Drawing.Size(30, 29)
        Me.AddAttachedObjectButton.TabIndex = 3
        Me.AddAttachedObjectButton.Text = "+"
        Me.AddAttachedObjectButton.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.AddAttachedObjectButton.UseVisualStyleBackColor = True
        '
        'NewAdapterTypeComboBox
        '
        Me.NewAdapterTypeComboBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.NewAdapterTypeComboBox.FormattingEnabled = True
        Me.NewAdapterTypeComboBox.Location = New System.Drawing.Point(469, 3)
        Me.NewAdapterTypeComboBox.Name = "NewAdapterTypeComboBox"
        Me.NewAdapterTypeComboBox.Size = New System.Drawing.Size(186, 21)
        Me.NewAdapterTypeComboBox.TabIndex = 2
        '
        'TypeHumanReadableComboBox
        '
        Me.TypeHumanReadableComboBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.InvoiceReceivedBindingSource, "TypeHumanReadable", True))
        Me.TypeHumanReadableComboBox.FormattingEnabled = True
        Me.TypeHumanReadableComboBox.Location = New System.Drawing.Point(257, 3)
        Me.TypeHumanReadableComboBox.Name = "TypeHumanReadableComboBox"
        Me.TypeHumanReadableComboBox.Size = New System.Drawing.Size(179, 21)
        Me.TypeHumanReadableComboBox.TabIndex = 1
        '
        'Panel2
        '
        Me.Panel2.AutoSize = True
        Me.Panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel2.Controls.Add(Me.NewButton)
        Me.Panel2.Controls.Add(Me.ICancelButton)
        Me.Panel2.Controls.Add(Me.IOkButton)
        Me.Panel2.Controls.Add(Me.IApplyButton)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 560)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(937, 32)
        Me.Panel2.TabIndex = 2
        '
        'NewButton
        '
        Me.NewButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.NewButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.NewButton.Location = New System.Drawing.Point(839, 6)
        Me.NewButton.Name = "NewButton"
        Me.NewButton.Size = New System.Drawing.Size(89, 23)
        Me.NewButton.TabIndex = 4
        Me.NewButton.Text = "Nauja"
        Me.NewButton.UseVisualStyleBackColor = True
        '
        'ICancelButton
        '
        Me.ICancelButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ICancelButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ICancelButton.Location = New System.Drawing.Point(733, 6)
        Me.ICancelButton.Name = "ICancelButton"
        Me.ICancelButton.Size = New System.Drawing.Size(89, 23)
        Me.ICancelButton.TabIndex = 3
        Me.ICancelButton.Text = "Atšaukti"
        Me.ICancelButton.UseVisualStyleBackColor = True
        '
        'IOkButton
        '
        Me.IOkButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.IOkButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IOkButton.Location = New System.Drawing.Point(527, 6)
        Me.IOkButton.Name = "IOkButton"
        Me.IOkButton.Size = New System.Drawing.Size(89, 23)
        Me.IOkButton.TabIndex = 1
        Me.IOkButton.Text = "Ok"
        Me.IOkButton.UseVisualStyleBackColor = True
        '
        'IApplyButton
        '
        Me.IApplyButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.IApplyButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.IApplyButton.Location = New System.Drawing.Point(631, 6)
        Me.IApplyButton.Name = "IApplyButton"
        Me.IApplyButton.Size = New System.Drawing.Size(89, 23)
        Me.IApplyButton.TabIndex = 2
        Me.IApplyButton.Text = "Išsaugoti"
        Me.IApplyButton.UseVisualStyleBackColor = True
        '
        'InvoiceItemsSortedBindingSource
        '
        Me.InvoiceItemsSortedBindingSource.DataMember = "InvoiceItems"
        Me.InvoiceItemsSortedBindingSource.DataSource = Me.InvoiceReceivedBindingSource
        '
        'InvoiceItemsDataListView
        '
        Me.InvoiceItemsDataListView.AllColumns.Add(Me.OlvColumn2)
        Me.InvoiceItemsDataListView.AllColumns.Add(Me.OlvColumn1)
        Me.InvoiceItemsDataListView.AllColumns.Add(Me.OlvColumn3)
        Me.InvoiceItemsDataListView.AllColumns.Add(Me.OlvColumn4)
        Me.InvoiceItemsDataListView.AllColumns.Add(Me.OlvColumn5)
        Me.InvoiceItemsDataListView.AllColumns.Add(Me.OlvColumn6)
        Me.InvoiceItemsDataListView.AllColumns.Add(Me.OlvColumn7)
        Me.InvoiceItemsDataListView.AllColumns.Add(Me.OlvColumn8)
        Me.InvoiceItemsDataListView.AllColumns.Add(Me.OlvColumn9)
        Me.InvoiceItemsDataListView.AllColumns.Add(Me.OlvColumn23)
        Me.InvoiceItemsDataListView.AllColumns.Add(Me.OlvColumn10)
        Me.InvoiceItemsDataListView.AllColumns.Add(Me.OlvColumn11)
        Me.InvoiceItemsDataListView.AllColumns.Add(Me.OlvColumn12)
        Me.InvoiceItemsDataListView.AllColumns.Add(Me.OlvColumn13)
        Me.InvoiceItemsDataListView.AllColumns.Add(Me.OlvColumn14)
        Me.InvoiceItemsDataListView.AllColumns.Add(Me.OlvColumn15)
        Me.InvoiceItemsDataListView.AllColumns.Add(Me.OlvColumn16)
        Me.InvoiceItemsDataListView.AllColumns.Add(Me.OlvColumn17)
        Me.InvoiceItemsDataListView.AllColumns.Add(Me.OlvColumn18)
        Me.InvoiceItemsDataListView.AllColumns.Add(Me.OlvColumn19)
        Me.InvoiceItemsDataListView.AllColumns.Add(Me.OlvColumn20)
        Me.InvoiceItemsDataListView.AllColumns.Add(Me.OlvColumn21)
        Me.InvoiceItemsDataListView.AllColumns.Add(Me.OlvColumn22)
        Me.InvoiceItemsDataListView.AllowColumnReorder = True
        Me.InvoiceItemsDataListView.AutoGenerateColumns = False
        Me.InvoiceItemsDataListView.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.SingleClickAlways
        Me.InvoiceItemsDataListView.CellEditEnterChangesRows = True
        Me.InvoiceItemsDataListView.CellEditTabChangesRows = True
        Me.InvoiceItemsDataListView.CellEditUseWholeCell = False
        Me.InvoiceItemsDataListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.OlvColumn2, Me.OlvColumn3, Me.OlvColumn4, Me.OlvColumn5, Me.OlvColumn6, Me.OlvColumn7, Me.OlvColumn8, Me.OlvColumn23, Me.OlvColumn10, Me.OlvColumn11, Me.OlvColumn13, Me.OlvColumn14, Me.OlvColumn22})
        Me.InvoiceItemsDataListView.Cursor = System.Windows.Forms.Cursors.Default
        Me.InvoiceItemsDataListView.DataSource = Me.InvoiceItemsSortedBindingSource
        Me.InvoiceItemsDataListView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.InvoiceItemsDataListView.FullRowSelect = True
        Me.InvoiceItemsDataListView.HasCollapsibleGroups = False
        Me.InvoiceItemsDataListView.HeaderWordWrap = True
        Me.InvoiceItemsDataListView.HideSelection = False
        Me.InvoiceItemsDataListView.IncludeColumnHeadersInCopy = True
        Me.InvoiceItemsDataListView.Location = New System.Drawing.Point(0, 319)
        Me.InvoiceItemsDataListView.Name = "InvoiceItemsDataListView"
        Me.InvoiceItemsDataListView.RenderNonEditableCheckboxesAsDisabled = True
        Me.InvoiceItemsDataListView.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.Submenu
        Me.InvoiceItemsDataListView.SelectedBackColor = System.Drawing.Color.PaleGreen
        Me.InvoiceItemsDataListView.SelectedForeColor = System.Drawing.Color.Black
        Me.InvoiceItemsDataListView.ShowCommandMenuOnRightClick = True
        Me.InvoiceItemsDataListView.ShowGroups = False
        Me.InvoiceItemsDataListView.ShowImagesOnSubItems = True
        Me.InvoiceItemsDataListView.ShowItemCountOnGroups = True
        Me.InvoiceItemsDataListView.ShowItemToolTips = True
        Me.InvoiceItemsDataListView.Size = New System.Drawing.Size(937, 241)
        Me.InvoiceItemsDataListView.TabIndex = 1
        Me.InvoiceItemsDataListView.UnfocusedSelectedBackColor = System.Drawing.Color.PaleGreen
        Me.InvoiceItemsDataListView.UnfocusedSelectedForeColor = System.Drawing.Color.Black
        Me.InvoiceItemsDataListView.UseCellFormatEvents = True
        Me.InvoiceItemsDataListView.UseCompatibleStateImageBehavior = False
        Me.InvoiceItemsDataListView.UseFilterIndicator = True
        Me.InvoiceItemsDataListView.UseFiltering = True
        Me.InvoiceItemsDataListView.UseHotItem = True
        Me.InvoiceItemsDataListView.UseNotifyPropertyChanged = True
        Me.InvoiceItemsDataListView.View = System.Windows.Forms.View.Details
        '
        'OlvColumn2
        '
        Me.OlvColumn2.AspectName = "NameInvoice"
        Me.OlvColumn2.CellEditUseWholeCell = True
        Me.OlvColumn2.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn2.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn2.Text = "Turinys"
        Me.OlvColumn2.ToolTipText = ""
        Me.OlvColumn2.Width = 286
        '
        'OlvColumn1
        '
        Me.OlvColumn1.AspectName = "ID"
        Me.OlvColumn1.CellEditUseWholeCell = True
        Me.OlvColumn1.DisplayIndex = 1
        Me.OlvColumn1.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn1.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn1.IsEditable = False
        Me.OlvColumn1.IsVisible = False
        Me.OlvColumn1.Text = "ID"
        Me.OlvColumn1.ToolTipText = ""
        Me.OlvColumn1.Width = 100
        '
        'OlvColumn3
        '
        Me.OlvColumn3.AspectName = "MeasureUnit"
        Me.OlvColumn3.CellEditUseWholeCell = True
        Me.OlvColumn3.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn3.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn3.Text = "Mato Vnt."
        Me.OlvColumn3.ToolTipText = ""
        Me.OlvColumn3.Width = 63
        '
        'OlvColumn4
        '
        Me.OlvColumn4.AspectName = "AccountCosts"
        Me.OlvColumn4.CellEditUseWholeCell = True
        Me.OlvColumn4.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn4.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn4.Text = "Sąnaudų sąsk."
        Me.OlvColumn4.ToolTipText = ""
        Me.OlvColumn4.Width = 85
        '
        'OlvColumn5
        '
        Me.OlvColumn5.AspectName = "AccountVat"
        Me.OlvColumn5.CellEditUseWholeCell = True
        Me.OlvColumn5.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn5.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn5.Text = "PVM sąsk."
        Me.OlvColumn5.ToolTipText = ""
        Me.OlvColumn5.Width = 76
        '
        'OlvColumn6
        '
        Me.OlvColumn6.AspectName = "Ammount"
        Me.OlvColumn6.AspectToStringFormat = "{0:##,0.0000}"
        Me.OlvColumn6.CellEditUseWholeCell = True
        Me.OlvColumn6.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn6.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn6.Text = "Kiekis"
        Me.OlvColumn6.ToolTipText = ""
        Me.OlvColumn6.Width = 69
        '
        'OlvColumn7
        '
        Me.OlvColumn7.AspectName = "UnitValue"
        Me.OlvColumn7.AspectToStringFormat = "{0:##,0.0000}"
        Me.OlvColumn7.CellEditUseWholeCell = True
        Me.OlvColumn7.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn7.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn7.Text = "Vnt. Kaina"
        Me.OlvColumn7.ToolTipText = ""
        Me.OlvColumn7.Width = 81
        '
        'OlvColumn8
        '
        Me.OlvColumn8.AspectName = "Sum"
        Me.OlvColumn8.AspectToStringFormat = "{0:##,0.00}"
        Me.OlvColumn8.CellEditUseWholeCell = True
        Me.OlvColumn8.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn8.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn8.IsEditable = False
        Me.OlvColumn8.Text = "Suma"
        Me.OlvColumn8.ToolTipText = ""
        Me.OlvColumn8.Width = 80
        '
        'OlvColumn9
        '
        Me.OlvColumn9.AspectName = "SumCorrection"
        Me.OlvColumn9.CellEditUseWholeCell = True
        Me.OlvColumn9.DisplayIndex = 8
        Me.OlvColumn9.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn9.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn9.IsVisible = False
        Me.OlvColumn9.Text = "Sumos kor."
        Me.OlvColumn9.ToolTipText = ""
        Me.OlvColumn9.Width = 100
        '
        'OlvColumn23
        '
        Me.OlvColumn23.AspectName = "DeclarationSchema"
        Me.OlvColumn23.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.OlvColumn23.Text = "PVM Deklaravimo Schema"
        '
        'OlvColumn10
        '
        Me.OlvColumn10.AspectName = "VatRate"
        Me.OlvColumn10.CellEditUseWholeCell = True
        Me.OlvColumn10.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn10.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn10.Text = "PVM tarifas"
        Me.OlvColumn10.ToolTipText = ""
        '
        'OlvColumn11
        '
        Me.OlvColumn11.AspectName = "SumVat"
        Me.OlvColumn11.AspectToStringFormat = "{0:##,0.00}"
        Me.OlvColumn11.CellEditUseWholeCell = True
        Me.OlvColumn11.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn11.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn11.IsEditable = False
        Me.OlvColumn11.Text = "Suma PVM"
        Me.OlvColumn11.ToolTipText = ""
        Me.OlvColumn11.Width = 100
        '
        'OlvColumn12
        '
        Me.OlvColumn12.AspectName = "SumVatCorrection"
        Me.OlvColumn12.CellEditUseWholeCell = True
        Me.OlvColumn12.DisplayIndex = 11
        Me.OlvColumn12.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn12.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn12.IsVisible = False
        Me.OlvColumn12.Text = "Sumos PVM kor."
        Me.OlvColumn12.ToolTipText = ""
        Me.OlvColumn12.Width = 100
        '
        'OlvColumn13
        '
        Me.OlvColumn13.AspectName = "IncludeVatInObject"
        Me.OlvColumn13.CellEditUseWholeCell = True
        Me.OlvColumn13.CheckBoxes = True
        Me.OlvColumn13.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn13.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn13.Text = "Įtraukti PVM į Objektą"
        Me.OlvColumn13.ToolTipText = ""
        Me.OlvColumn13.Width = 61
        '
        'OlvColumn14
        '
        Me.OlvColumn14.AspectName = "SumTotal"
        Me.OlvColumn14.AspectToStringFormat = "{0:##,0.00}"
        Me.OlvColumn14.CellEditUseWholeCell = True
        Me.OlvColumn14.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn14.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn14.IsEditable = False
        Me.OlvColumn14.Text = "Suma Viso"
        Me.OlvColumn14.ToolTipText = ""
        Me.OlvColumn14.Width = 100
        '
        'OlvColumn15
        '
        Me.OlvColumn15.AspectName = "UnitValueLTL"
        Me.OlvColumn15.AspectToStringFormat = "{0:##,0.0000}"
        Me.OlvColumn15.CellEditUseWholeCell = True
        Me.OlvColumn15.DisplayIndex = 14
        Me.OlvColumn15.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn15.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn15.IsEditable = False
        Me.OlvColumn15.IsVisible = False
        Me.OlvColumn15.Text = "Vnt. Kaina LTL"
        Me.OlvColumn15.ToolTipText = ""
        Me.OlvColumn15.Width = 100
        '
        'OlvColumn16
        '
        Me.OlvColumn16.AspectName = "UnitValueLTLCorrection"
        Me.OlvColumn16.CellEditUseWholeCell = True
        Me.OlvColumn16.DisplayIndex = 15
        Me.OlvColumn16.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn16.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn16.IsVisible = False
        Me.OlvColumn16.Text = "Vnt. Kainos LTL kor."
        Me.OlvColumn16.ToolTipText = ""
        Me.OlvColumn16.Width = 100
        '
        'OlvColumn17
        '
        Me.OlvColumn17.AspectName = "SumLTL"
        Me.OlvColumn17.AspectToStringFormat = "{0:##,0.00}"
        Me.OlvColumn17.CellEditUseWholeCell = True
        Me.OlvColumn17.DisplayIndex = 16
        Me.OlvColumn17.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn17.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn17.IsEditable = False
        Me.OlvColumn17.IsVisible = False
        Me.OlvColumn17.Text = "Suma LTL"
        Me.OlvColumn17.ToolTipText = ""
        Me.OlvColumn17.Width = 100
        '
        'OlvColumn18
        '
        Me.OlvColumn18.AspectName = "SumLTLCorrection"
        Me.OlvColumn18.CellEditUseWholeCell = True
        Me.OlvColumn18.DisplayIndex = 17
        Me.OlvColumn18.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn18.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn18.IsVisible = False
        Me.OlvColumn18.Text = "Sumos LTL kor."
        Me.OlvColumn18.ToolTipText = ""
        Me.OlvColumn18.Width = 100
        '
        'OlvColumn19
        '
        Me.OlvColumn19.AspectName = "SumVatLTL"
        Me.OlvColumn19.AspectToStringFormat = "{0:##,0.00}"
        Me.OlvColumn19.CellEditUseWholeCell = True
        Me.OlvColumn19.DisplayIndex = 18
        Me.OlvColumn19.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn19.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn19.IsEditable = False
        Me.OlvColumn19.IsVisible = False
        Me.OlvColumn19.Text = "Suma PVM LTL"
        Me.OlvColumn19.ToolTipText = ""
        Me.OlvColumn19.Width = 100
        '
        'OlvColumn20
        '
        Me.OlvColumn20.AspectName = "SumVatLTLCorrection"
        Me.OlvColumn20.CellEditUseWholeCell = True
        Me.OlvColumn20.DisplayIndex = 19
        Me.OlvColumn20.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn20.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn20.IsVisible = False
        Me.OlvColumn20.Text = "Sumos PVM LTL kor."
        Me.OlvColumn20.ToolTipText = ""
        Me.OlvColumn20.Width = 100
        '
        'OlvColumn21
        '
        Me.OlvColumn21.AspectName = "SumTotalLTL"
        Me.OlvColumn21.AspectToStringFormat = "{0:##,0.00}"
        Me.OlvColumn21.CellEditUseWholeCell = True
        Me.OlvColumn21.DisplayIndex = 20
        Me.OlvColumn21.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn21.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn21.IsEditable = False
        Me.OlvColumn21.IsVisible = False
        Me.OlvColumn21.Text = "Suma Viso LTL"
        Me.OlvColumn21.ToolTipText = ""
        Me.OlvColumn21.Width = 100
        '
        'OlvColumn22
        '
        Me.OlvColumn22.AspectName = "AttachedObject"
        Me.OlvColumn22.CellEditUseWholeCell = True
        Me.OlvColumn22.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn22.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn22.IsEditable = False
        Me.OlvColumn22.Text = "Susietas objektas"
        Me.OlvColumn22.ToolTipText = ""
        Me.OlvColumn22.Width = 100
        '
        'ProgressFiller2
        '
        Me.ProgressFiller2.Location = New System.Drawing.Point(322, 340)
        Me.ProgressFiller2.Name = "ProgressFiller2"
        Me.ProgressFiller2.Size = New System.Drawing.Size(165, 83)
        Me.ProgressFiller2.TabIndex = 5
        Me.ProgressFiller2.Visible = False
        '
        'ProgressFiller1
        '
        Me.ProgressFiller1.Location = New System.Drawing.Point(151, 343)
        Me.ProgressFiller1.Name = "ProgressFiller1"
        Me.ProgressFiller1.Size = New System.Drawing.Size(151, 67)
        Me.ProgressFiller1.TabIndex = 4
        Me.ProgressFiller1.Visible = False
        '
        'ErrorWarnInfoProvider1
        '
        Me.ErrorWarnInfoProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.ErrorWarnInfoProvider1.BlinkStyleInformation = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.ErrorWarnInfoProvider1.BlinkStyleWarning = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.ErrorWarnInfoProvider1.ContainerControl = Me
        Me.ErrorWarnInfoProvider1.DataSource = Me.InvoiceReceivedBindingSource
        '
        'F_InvoiceReceived
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(937, 592)
        Me.Controls.Add(Me.InvoiceItemsDataListView)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.ProgressFiller2)
        Me.Controls.Add(Me.ProgressFiller1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "F_InvoiceReceived"
        Me.ShowInTaskbar = False
        Me.Text = "Gauta sąskaita faktūra"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        CType(Me.InvoiceReceivedBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel10.ResumeLayout(False)
        Me.TableLayoutPanel10.PerformLayout()
        Me.TableLayoutPanel8.ResumeLayout(False)
        Me.TableLayoutPanel8.PerformLayout()
        Me.TableLayoutPanel9.ResumeLayout(False)
        Me.TableLayoutPanel9.PerformLayout()
        Me.TableLayoutPanel5.ResumeLayout(False)
        Me.TableLayoutPanel5.PerformLayout()
        Me.TableLayoutPanel7.ResumeLayout(False)
        Me.TableLayoutPanel7.PerformLayout()
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.TableLayoutPanel4.PerformLayout()
        Me.TableLayoutPanel6.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.TableLayoutPanel11.ResumeLayout(False)
        Me.TableLayoutPanel11.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        CType(Me.InvoiceItemsSortedBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.InvoiceItemsDataListView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ErrorWarnInfoProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents InvoiceReceivedBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents AccountSupplierAccGridComboBox As AccControlsWinForms.AccListComboBox
    Friend WithEvents CommentsInternalTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ContentTextBox As System.Windows.Forms.TextBox
    Friend WithEvents CurrencyCodeComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents CurrencyRateAccTextBox As AccControlsWinForms.AccTextBox
    Friend WithEvents IDTextBox As System.Windows.Forms.TextBox
    Friend WithEvents NumberTextBox As System.Windows.Forms.TextBox
    Friend WithEvents SumAccTextBox As AccControlsWinForms.AccTextBox
    Friend WithEvents SumLTLAccTextBox As AccControlsWinForms.AccTextBox
    Friend WithEvents SumTotalAccTextBox As AccControlsWinForms.AccTextBox
    Friend WithEvents SumTotalLTLAccTextBox As AccControlsWinForms.AccTextBox
    Friend WithEvents SumVatAccTextBox As AccControlsWinForms.AccTextBox
    Friend WithEvents SumVatLTLAccTextBox As AccControlsWinForms.AccTextBox
    Friend WithEvents SupplierAccGridComboBox As AccControlsWinForms.AccListComboBox
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel5 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel4 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel8 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel7 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel6 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents GetCurrencyRatesButton As System.Windows.Forms.Button
    Friend WithEvents TableLayoutPanel9 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel10 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel11 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents AddAttachedObjectButton As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents ICancelButton As System.Windows.Forms.Button
    Friend WithEvents IOkButton As System.Windows.Forms.Button
    Friend WithEvents IApplyButton As System.Windows.Forms.Button
    Friend WithEvents InvoiceItemsSortedBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents TypeHumanReadableComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents UpdateDateTextBox As System.Windows.Forms.TextBox
    Friend WithEvents InsertDateTextBox As System.Windows.Forms.TextBox
    Friend WithEvents CopyInvoiceButton As System.Windows.Forms.Button
    Friend WithEvents PasteInvoiceButton As System.Windows.Forms.Button
    Friend WithEvents NewButton As System.Windows.Forms.Button
    Friend WithEvents ViewJournalEntryButton As System.Windows.Forms.Button
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents IndirectVatCostsAccountAccGridComboBox As AccControlsWinForms.AccListComboBox
    Friend WithEvents ActualDateIsApplicableCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents IndirectVatAccountAccGridComboBox As AccControlsWinForms.AccListComboBox
    Friend WithEvents IndirectVatSumAccTextBox As AccControlsWinForms.AccTextBox
    Friend WithEvents CalculateIndirectVatButton As System.Windows.Forms.Button
    Friend WithEvents InvoiceItemsDataListView As BrightIdeasSoftware.DataListView
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
    Friend WithEvents OlvColumn12 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn13 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn14 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn15 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn16 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn17 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn18 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn19 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn20 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn21 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents OlvColumn22 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents ProgressFiller2 As AccControlsWinForms.ProgressFiller
    Friend WithEvents ProgressFiller1 As AccControlsWinForms.ProgressFiller
    Friend WithEvents NewAdapterTypeComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents ErrorWarnInfoProvider1 As AccControlsWinForms.ErrorWarnInfoProvider
    Friend WithEvents OlvColumn23 As BrightIdeasSoftware.OLVColumn
    Friend WithEvents IndirectVatDeclarationSchemaAccListComboBox As AccControlsWinForms.AccListComboBox
    Friend WithEvents DateAccDatePicker As AccControlsWinForms.AccDatePicker
    Friend WithEvents ActualDateAccDatePicker As AccControlsWinForms.AccDatePicker
End Class
