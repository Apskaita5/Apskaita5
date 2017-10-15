<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Friend Class F_WorkersVDUInfo
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
        Dim DateLabel As System.Windows.Forms.Label
        Dim ContractSerialLabel As System.Windows.Forms.Label
        Dim ContractNumberLabel As System.Windows.Forms.Label
        Dim ConventionalExtraPayLabel As System.Windows.Forms.Label
        Dim ConventionalWageLabel As System.Windows.Forms.Label
        Dim WageTypeHumanReadableLabel As System.Windows.Forms.Label
        Dim PersonNameLabel As System.Windows.Forms.Label
        Dim PersonCodeLabel As System.Windows.Forms.Label
        Dim PositionLabel As System.Windows.Forms.Label
        Dim StandartDaysForTheCurrentMonthLabel As System.Windows.Forms.Label
        Dim StandartHoursForTheCurrentMonthLabel As System.Windows.Forms.Label
        Dim TotalWageLabel As System.Windows.Forms.Label
        Dim TotalWorkDaysLabel As System.Windows.Forms.Label
        Dim TotalWorkHoursLabel As System.Windows.Forms.Label
        Dim WageVDUDailyLabel As System.Windows.Forms.Label
        Dim WageVDUHourlyLabel As System.Windows.Forms.Label
        Dim WorkLoadLabel As System.Windows.Forms.Label
        Dim BonusYearlyLabel As System.Windows.Forms.Label
        Dim BonusQuarterlyLabel As System.Windows.Forms.Label
        Dim BonusBaseLabel As System.Windows.Forms.Label
        Dim TotalScheduledDaysLabel As System.Windows.Forms.Label
        Dim TotalScheduledHoursLabel As System.Windows.Forms.Label
        Dim BonusVDUDailyLabel As System.Windows.Forms.Label
        Dim BonusVDUHourlyLabel As System.Windows.Forms.Label
        Dim ApplicableVDUDailyLabel As System.Windows.Forms.Label
        Dim ApplicableVDUHourlyLabel As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(F_WorkersVDUInfo))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.DateDateTimePicker = New System.Windows.Forms.DateTimePicker
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.LabourContractAccListComboBox = New AccControlsWinForms.AccListComboBox
        Me.RefreshButton = New System.Windows.Forms.Button
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel
        Me.TableLayoutPanel9 = New System.Windows.Forms.TableLayoutPanel
        Me.ApplicableVDUHourlyAccTextBox = New AccControlsWinForms.AccTextBox
        Me.WorkersVDUInfoBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.BonusVDUHourlyAccTextBox = New AccControlsWinForms.AccTextBox
        Me.WageVDUHourlyAccTextBox = New AccControlsWinForms.AccTextBox
        Me.TableLayoutPanel12 = New System.Windows.Forms.TableLayoutPanel
        Me.BonusVDUDailyAccTextBox = New AccControlsWinForms.AccTextBox
        Me.ApplicableVDUDailyAccTextBox = New AccControlsWinForms.AccTextBox
        Me.WageVDUDailyAccTextBox = New AccControlsWinForms.AccTextBox
        Me.TableLayoutPanel11 = New System.Windows.Forms.TableLayoutPanel
        Me.TotalWageAccTextBox = New AccControlsWinForms.AccTextBox
        Me.TotalWorkDaysTextBox = New System.Windows.Forms.TextBox
        Me.TotalWorkHoursAccTextBox = New AccControlsWinForms.AccTextBox
        Me.TableLayoutPanel10 = New System.Windows.Forms.TableLayoutPanel
        Me.TotalScheduledDaysTextBox = New System.Windows.Forms.TextBox
        Me.TotalScheduledHoursAccTextBox = New AccControlsWinForms.AccTextBox
        Me.TableLayoutPanel7 = New System.Windows.Forms.TableLayoutPanel
        Me.StandartDaysForTheCurrentMonthTextBox = New System.Windows.Forms.TextBox
        Me.WorkLoadAccTextBox = New AccControlsWinForms.AccTextBox
        Me.StandartHoursForTheCurrentMonthAccTextBox = New AccControlsWinForms.AccTextBox
        Me.IncludeCurrentMonthCheckBox = New System.Windows.Forms.CheckBox
        Me.TableLayoutPanel8 = New System.Windows.Forms.TableLayoutPanel
        Me.BonusYearlyAccTextBox = New AccControlsWinForms.AccTextBox
        Me.BonusQuarterlyAccTextBox = New AccControlsWinForms.AccTextBox
        Me.BonusBaseAccTextBox = New AccControlsWinForms.AccTextBox
        Me.TableLayoutPanel6 = New System.Windows.Forms.TableLayoutPanel
        Me.PersonCodeTextBox = New System.Windows.Forms.TextBox
        Me.PersonNameTextBox = New System.Windows.Forms.TextBox
        Me.PositionTextBox = New System.Windows.Forms.TextBox
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel
        Me.DateDateTimePicker1 = New System.Windows.Forms.DateTimePicker
        Me.ContractNumberTextBox = New System.Windows.Forms.TextBox
        Me.ContractSerialTextBox = New System.Windows.Forms.TextBox
        Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel
        Me.ConventionalWageAccTextBox = New AccControlsWinForms.AccTextBox
        Me.WageTypeHumanReadableTextBox = New System.Windows.Forms.TextBox
        Me.ConventionalExtraPayAccTextBox = New AccControlsWinForms.AccTextBox
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel
        Me.TableLayoutPanel13 = New System.Windows.Forms.TableLayoutPanel
        Me.IncludeCurrentCheckBox = New System.Windows.Forms.CheckBox
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.WageListDataListView = New BrightIdeasSoftware.DataListView
        Me.OlvColumn1 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn2 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn3 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn4 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn5 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn6 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn7 = New BrightIdeasSoftware.OLVColumn
        Me.WageListBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.BonusListListDataListView = New BrightIdeasSoftware.DataListView
        Me.OlvColumn8 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn9 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn10 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn11 = New BrightIdeasSoftware.OLVColumn
        Me.BonusListBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.TableLayoutPanel14 = New System.Windows.Forms.TableLayoutPanel
        Me.ProgressFiller1 = New AccControlsWinForms.ProgressFiller
        Me.ProgressFiller2 = New AccControlsWinForms.ProgressFiller
        DateLabel = New System.Windows.Forms.Label
        ContractSerialLabel = New System.Windows.Forms.Label
        ContractNumberLabel = New System.Windows.Forms.Label
        ConventionalExtraPayLabel = New System.Windows.Forms.Label
        ConventionalWageLabel = New System.Windows.Forms.Label
        WageTypeHumanReadableLabel = New System.Windows.Forms.Label
        PersonNameLabel = New System.Windows.Forms.Label
        PersonCodeLabel = New System.Windows.Forms.Label
        PositionLabel = New System.Windows.Forms.Label
        StandartDaysForTheCurrentMonthLabel = New System.Windows.Forms.Label
        StandartHoursForTheCurrentMonthLabel = New System.Windows.Forms.Label
        TotalWageLabel = New System.Windows.Forms.Label
        TotalWorkDaysLabel = New System.Windows.Forms.Label
        TotalWorkHoursLabel = New System.Windows.Forms.Label
        WageVDUDailyLabel = New System.Windows.Forms.Label
        WageVDUHourlyLabel = New System.Windows.Forms.Label
        WorkLoadLabel = New System.Windows.Forms.Label
        BonusYearlyLabel = New System.Windows.Forms.Label
        BonusQuarterlyLabel = New System.Windows.Forms.Label
        BonusBaseLabel = New System.Windows.Forms.Label
        TotalScheduledDaysLabel = New System.Windows.Forms.Label
        TotalScheduledHoursLabel = New System.Windows.Forms.Label
        BonusVDUDailyLabel = New System.Windows.Forms.Label
        BonusVDUHourlyLabel = New System.Windows.Forms.Label
        ApplicableVDUDailyLabel = New System.Windows.Forms.Label
        ApplicableVDUHourlyLabel = New System.Windows.Forms.Label
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TableLayoutPanel9.SuspendLayout()
        CType(Me.WorkersVDUInfoBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel12.SuspendLayout()
        Me.TableLayoutPanel11.SuspendLayout()
        Me.TableLayoutPanel10.SuspendLayout()
        Me.TableLayoutPanel7.SuspendLayout()
        Me.TableLayoutPanel8.SuspendLayout()
        Me.TableLayoutPanel6.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        Me.TableLayoutPanel5.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.TableLayoutPanel13.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.WageListDataListView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.WageListBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BonusListListDataListView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.BonusListBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel14.SuspendLayout()
        Me.SuspendLayout()
        '
        'DateLabel
        '
        DateLabel.AutoSize = True
        DateLabel.Dock = System.Windows.Forms.DockStyle.Fill
        DateLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        DateLabel.Location = New System.Drawing.Point(3, 5)
        DateLabel.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        DateLabel.Name = "DateLabel"
        DateLabel.Size = New System.Drawing.Size(145, 25)
        DateLabel.TabIndex = 5
        DateLabel.Text = "Data:"
        DateLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'ContractSerialLabel
        '
        ContractSerialLabel.AutoSize = True
        ContractSerialLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        ContractSerialLabel.Location = New System.Drawing.Point(188, 5)
        ContractSerialLabel.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        ContractSerialLabel.Name = "ContractSerialLabel"
        ContractSerialLabel.Size = New System.Drawing.Size(88, 13)
        ContractSerialLabel.TabIndex = 6
        ContractSerialLabel.Text = "Sutarties Ser.:"
        '
        'ContractNumberLabel
        '
        ContractNumberLabel.AutoSize = True
        ContractNumberLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        ContractNumberLabel.Location = New System.Drawing.Point(467, 5)
        ContractNumberLabel.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        ContractNumberLabel.Name = "ContractNumberLabel"
        ContractNumberLabel.Size = New System.Drawing.Size(82, 13)
        ContractNumberLabel.TabIndex = 8
        ContractNumberLabel.Text = "Sutarties Nr.:"
        '
        'ConventionalExtraPayLabel
        '
        ConventionalExtraPayLabel.AutoSize = True
        ConventionalExtraPayLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        ConventionalExtraPayLabel.Location = New System.Drawing.Point(471, 5)
        ConventionalExtraPayLabel.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        ConventionalExtraPayLabel.Name = "ConventionalExtraPayLabel"
        ConventionalExtraPayLabel.Size = New System.Drawing.Size(53, 13)
        ConventionalExtraPayLabel.TabIndex = 3
        ConventionalExtraPayLabel.Text = "Priedas:"
        '
        'ConventionalWageLabel
        '
        ConventionalWageLabel.AutoSize = True
        ConventionalWageLabel.Dock = System.Windows.Forms.DockStyle.Fill
        ConventionalWageLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        ConventionalWageLabel.Location = New System.Drawing.Point(3, 65)
        ConventionalWageLabel.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        ConventionalWageLabel.Name = "ConventionalWageLabel"
        ConventionalWageLabel.Size = New System.Drawing.Size(145, 25)
        ConventionalWageLabel.TabIndex = 5
        ConventionalWageLabel.Text = "Darbo užmokestis:"
        ConventionalWageLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'WageTypeHumanReadableLabel
        '
        WageTypeHumanReadableLabel.AutoSize = True
        WageTypeHumanReadableLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        WageTypeHumanReadableLabel.Location = New System.Drawing.Point(213, 5)
        WageTypeHumanReadableLabel.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        WageTypeHumanReadableLabel.Name = "WageTypeHumanReadableLabel"
        WageTypeHumanReadableLabel.Size = New System.Drawing.Size(42, 13)
        WageTypeHumanReadableLabel.TabIndex = 7
        WageTypeHumanReadableLabel.Text = "Tipas:"
        '
        'PersonNameLabel
        '
        PersonNameLabel.AutoSize = True
        PersonNameLabel.Dock = System.Windows.Forms.DockStyle.Fill
        PersonNameLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        PersonNameLabel.Location = New System.Drawing.Point(3, 35)
        PersonNameLabel.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        PersonNameLabel.Name = "PersonNameLabel"
        PersonNameLabel.Size = New System.Drawing.Size(145, 25)
        PersonNameLabel.TabIndex = 3
        PersonNameLabel.Text = "Darbuotojas:"
        PersonNameLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'PersonCodeLabel
        '
        PersonCodeLabel.AutoSize = True
        PersonCodeLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        PersonCodeLabel.Location = New System.Drawing.Point(194, 5)
        PersonCodeLabel.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        PersonCodeLabel.Name = "PersonCodeLabel"
        PersonCodeLabel.Size = New System.Drawing.Size(93, 13)
        PersonCodeLabel.TabIndex = 5
        PersonCodeLabel.Text = "Asmens Kodas:"
        '
        'PositionLabel
        '
        PositionLabel.AutoSize = True
        PositionLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        PositionLabel.Location = New System.Drawing.Point(484, 5)
        PositionLabel.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        PositionLabel.Name = "PositionLabel"
        PositionLabel.Size = New System.Drawing.Size(60, 13)
        PositionLabel.TabIndex = 7
        PositionLabel.Text = "Pareigos:"
        '
        'StandartDaysForTheCurrentMonthLabel
        '
        StandartDaysForTheCurrentMonthLabel.AutoSize = True
        StandartDaysForTheCurrentMonthLabel.Dock = System.Windows.Forms.DockStyle.Fill
        StandartDaysForTheCurrentMonthLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        StandartDaysForTheCurrentMonthLabel.Location = New System.Drawing.Point(3, 95)
        StandartDaysForTheCurrentMonthLabel.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        StandartDaysForTheCurrentMonthLabel.Name = "StandartDaysForTheCurrentMonthLabel"
        StandartDaysForTheCurrentMonthLabel.Size = New System.Drawing.Size(145, 25)
        StandartDaysForTheCurrentMonthLabel.TabIndex = 5
        StandartDaysForTheCurrentMonthLabel.Text = "Einamojo mėn. d.d.:"
        StandartDaysForTheCurrentMonthLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'StandartHoursForTheCurrentMonthLabel
        '
        StandartHoursForTheCurrentMonthLabel.AutoSize = True
        StandartHoursForTheCurrentMonthLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        StandartHoursForTheCurrentMonthLabel.Location = New System.Drawing.Point(97, 5)
        StandartHoursForTheCurrentMonthLabel.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        StandartHoursForTheCurrentMonthLabel.Name = "StandartHoursForTheCurrentMonthLabel"
        StandartHoursForTheCurrentMonthLabel.Size = New System.Drawing.Size(119, 13)
        StandartHoursForTheCurrentMonthLabel.TabIndex = 7
        StandartHoursForTheCurrentMonthLabel.Text = "Einamojo mėn. d.v.:"
        '
        'TotalWageLabel
        '
        TotalWageLabel.AutoSize = True
        TotalWageLabel.Dock = System.Windows.Forms.DockStyle.Fill
        TotalWageLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        TotalWageLabel.Location = New System.Drawing.Point(3, 185)
        TotalWageLabel.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        TotalWageLabel.Name = "TotalWageLabel"
        TotalWageLabel.Size = New System.Drawing.Size(145, 25)
        TotalWageLabel.TabIndex = 3
        TotalWageLabel.Text = "Viso Darbo Užmokesčio:"
        TotalWageLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'TotalWorkDaysLabel
        '
        TotalWorkDaysLabel.AutoSize = True
        TotalWorkDaysLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        TotalWorkDaysLabel.Location = New System.Drawing.Point(204, 5)
        TotalWorkDaysLabel.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        TotalWorkDaysLabel.Name = "TotalWorkDaysLabel"
        TotalWorkDaysLabel.Size = New System.Drawing.Size(61, 13)
        TotalWorkDaysLabel.TabIndex = 5
        TotalWorkDaysLabel.Text = "Viso d.d.:"
        '
        'TotalWorkHoursLabel
        '
        TotalWorkHoursLabel.AutoSize = True
        TotalWorkHoursLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        TotalWorkHoursLabel.Location = New System.Drawing.Point(472, 5)
        TotalWorkHoursLabel.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        TotalWorkHoursLabel.Name = "TotalWorkHoursLabel"
        TotalWorkHoursLabel.Size = New System.Drawing.Size(61, 13)
        TotalWorkHoursLabel.TabIndex = 7
        TotalWorkHoursLabel.Text = "Viso d.v.:"
        '
        'WageVDUDailyLabel
        '
        WageVDUDailyLabel.AutoSize = True
        WageVDUDailyLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        WageVDUDailyLabel.Location = New System.Drawing.Point(167, 5)
        WageVDUDailyLabel.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        WageVDUDailyLabel.Name = "WageVDUDailyLabel"
        WageVDUDailyLabel.Size = New System.Drawing.Size(154, 13)
        WageVDUDailyLabel.TabIndex = 9
        WageVDUDailyLabel.Text = "Dienos VDU (užmokestis):"
        '
        'WageVDUHourlyLabel
        '
        WageVDUHourlyLabel.AutoSize = True
        WageVDUHourlyLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        WageVDUHourlyLabel.Location = New System.Drawing.Point(178, 5)
        WageVDUHourlyLabel.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        WageVDUHourlyLabel.Name = "WageVDUHourlyLabel"
        WageVDUHourlyLabel.Size = New System.Drawing.Size(137, 13)
        WageVDUHourlyLabel.TabIndex = 11
        WageVDUHourlyLabel.Text = "Val. VDU (užmokestis):"
        '
        'WorkLoadLabel
        '
        WorkLoadLabel.AutoSize = True
        WorkLoadLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        WorkLoadLabel.Location = New System.Drawing.Point(316, 5)
        WorkLoadLabel.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        WorkLoadLabel.Name = "WorkLoadLabel"
        WorkLoadLabel.Size = New System.Drawing.Size(90, 13)
        WorkLoadLabel.TabIndex = 13
        WorkLoadLabel.Text = "Krūvis (etatai):"
        '
        'BonusYearlyLabel
        '
        BonusYearlyLabel.AutoSize = True
        BonusYearlyLabel.Dock = System.Windows.Forms.DockStyle.Fill
        BonusYearlyLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        BonusYearlyLabel.Location = New System.Drawing.Point(3, 125)
        BonusYearlyLabel.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        BonusYearlyLabel.Name = "BonusYearlyLabel"
        BonusYearlyLabel.Size = New System.Drawing.Size(145, 25)
        BonusYearlyLabel.TabIndex = 13
        BonusYearlyLabel.Text = "Metinė Premija:"
        BonusYearlyLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'BonusQuarterlyLabel
        '
        BonusQuarterlyLabel.AutoSize = True
        BonusQuarterlyLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        BonusQuarterlyLabel.Location = New System.Drawing.Point(188, 5)
        BonusQuarterlyLabel.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        BonusQuarterlyLabel.Name = "BonusQuarterlyLabel"
        BonusQuarterlyLabel.Size = New System.Drawing.Size(86, 13)
        BonusQuarterlyLabel.TabIndex = 15
        BonusQuarterlyLabel.Text = "Ketv. Premija:"
        '
        'BonusBaseLabel
        '
        BonusBaseLabel.AutoSize = True
        BonusBaseLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        BonusBaseLabel.Location = New System.Drawing.Point(465, 5)
        BonusBaseLabel.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        BonusBaseLabel.Name = "BonusBaseLabel"
        BonusBaseLabel.Size = New System.Drawing.Size(84, 13)
        BonusBaseLabel.TabIndex = 17
        BonusBaseLabel.Text = "Premijų Bazė:"
        '
        'TotalScheduledDaysLabel
        '
        TotalScheduledDaysLabel.AutoSize = True
        TotalScheduledDaysLabel.Dock = System.Windows.Forms.DockStyle.Fill
        TotalScheduledDaysLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        TotalScheduledDaysLabel.Location = New System.Drawing.Point(3, 155)
        TotalScheduledDaysLabel.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        TotalScheduledDaysLabel.Name = "TotalScheduledDaysLabel"
        TotalScheduledDaysLabel.Size = New System.Drawing.Size(145, 25)
        TotalScheduledDaysLabel.TabIndex = 19
        TotalScheduledDaysLabel.Text = "Viso d.d. grafike:"
        TotalScheduledDaysLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'TotalScheduledHoursLabel
        '
        TotalScheduledHoursLabel.AutoSize = True
        TotalScheduledHoursLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        TotalScheduledHoursLabel.Location = New System.Drawing.Point(173, 5)
        TotalScheduledHoursLabel.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        TotalScheduledHoursLabel.Name = "TotalScheduledHoursLabel"
        TotalScheduledHoursLabel.Size = New System.Drawing.Size(104, 13)
        TotalScheduledHoursLabel.TabIndex = 21
        TotalScheduledHoursLabel.Text = "Viso d.v. grafike:"
        '
        'BonusVDUDailyLabel
        '
        BonusVDUDailyLabel.AutoSize = True
        BonusVDUDailyLabel.Dock = System.Windows.Forms.DockStyle.Fill
        BonusVDUDailyLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        BonusVDUDailyLabel.Location = New System.Drawing.Point(3, 215)
        BonusVDUDailyLabel.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        BonusVDUDailyLabel.Name = "BonusVDUDailyLabel"
        BonusVDUDailyLabel.Size = New System.Drawing.Size(145, 25)
        BonusVDUDailyLabel.TabIndex = 23
        BonusVDUDailyLabel.Text = "Dienos VDU (premija):"
        BonusVDUDailyLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'BonusVDUHourlyLabel
        '
        BonusVDUHourlyLabel.AutoSize = True
        BonusVDUHourlyLabel.Dock = System.Windows.Forms.DockStyle.Fill
        BonusVDUHourlyLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        BonusVDUHourlyLabel.Location = New System.Drawing.Point(3, 245)
        BonusVDUHourlyLabel.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        BonusVDUHourlyLabel.Name = "BonusVDUHourlyLabel"
        BonusVDUHourlyLabel.Size = New System.Drawing.Size(145, 25)
        BonusVDUHourlyLabel.TabIndex = 25
        BonusVDUHourlyLabel.Text = "Val. VDU (premija):"
        BonusVDUHourlyLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'ApplicableVDUDailyLabel
        '
        ApplicableVDUDailyLabel.AutoSize = True
        ApplicableVDUDailyLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        ApplicableVDUDailyLabel.Location = New System.Drawing.Point(491, 5)
        ApplicableVDUDailyLabel.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        ApplicableVDUDailyLabel.Name = "ApplicableVDUDailyLabel"
        ApplicableVDUDailyLabel.Size = New System.Drawing.Size(80, 13)
        ApplicableVDUDailyLabel.TabIndex = 29
        ApplicableVDUDailyLabel.Text = "Dienos VDU:"
        '
        'ApplicableVDUHourlyLabel
        '
        ApplicableVDUHourlyLabel.AutoSize = True
        ApplicableVDUHourlyLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        ApplicableVDUHourlyLabel.Location = New System.Drawing.Point(496, 5)
        ApplicableVDUHourlyLabel.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        ApplicableVDUHourlyLabel.Name = "ApplicableVDUHourlyLabel"
        ApplicableVDUHourlyLabel.Size = New System.Drawing.Size(63, 13)
        ApplicableVDUHourlyLabel.TabIndex = 30
        ApplicableVDUHourlyLabel.Text = "Val. VDU:"
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.AutoSize = True
        Me.TableLayoutPanel1.ColumnCount = 6
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 70.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 21.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.DateDateTimePicker, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label3, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 3, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.LabourContractAccListComboBox, 4, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(835, 29)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'DateDateTimePicker
        '
        Me.DateDateTimePicker.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DateDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateDateTimePicker.Location = New System.Drawing.Point(47, 3)
        Me.DateDateTimePicker.Name = "DateDateTimePicker"
        Me.DateDateTimePicker.Size = New System.Drawing.Size(193, 20)
        Me.DateDateTimePicker.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label3.Location = New System.Drawing.Point(3, 5)
        Me.Label3.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(38, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Data:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label1.Location = New System.Drawing.Point(266, 5)
        Me.Label1.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(79, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Darbuotojas:"
        '
        'LabourContractAccListComboBox
        '
        Me.LabourContractAccListComboBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabourContractAccListComboBox.EmptyValueString = ""
        Me.LabourContractAccListComboBox.FilterString = ""
        Me.LabourContractAccListComboBox.FormattingEnabled = True
        Me.LabourContractAccListComboBox.InstantBinding = True
        Me.LabourContractAccListComboBox.Location = New System.Drawing.Point(351, 3)
        Me.LabourContractAccListComboBox.Name = "LabourContractAccListComboBox"
        Me.LabourContractAccListComboBox.Size = New System.Drawing.Size(459, 21)
        Me.LabourContractAccListComboBox.TabIndex = 4
        '
        'RefreshButton
        '
        Me.RefreshButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RefreshButton.Image = Global.AccDataBindingsWinForms.My.Resources.Resources.Button_Reload_icon_24p
        Me.RefreshButton.Location = New System.Drawing.Point(845, 12)
        Me.RefreshButton.Name = "RefreshButton"
        Me.RefreshButton.Size = New System.Drawing.Size(33, 33)
        Me.RefreshButton.TabIndex = 1
        Me.RefreshButton.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 2
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel9, 1, 8)
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel12, 1, 7)
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel11, 1, 6)
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel10, 1, 5)
        Me.TableLayoutPanel2.Controls.Add(BonusVDUHourlyLabel, 0, 8)
        Me.TableLayoutPanel2.Controls.Add(TotalScheduledDaysLabel, 0, 5)
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel7, 1, 3)
        Me.TableLayoutPanel2.Controls.Add(BonusVDUDailyLabel, 0, 7)
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel8, 1, 4)
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel6, 1, 1)
        Me.TableLayoutPanel2.Controls.Add(PersonNameLabel, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel4, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(StandartDaysForTheCurrentMonthLabel, 0, 3)
        Me.TableLayoutPanel2.Controls.Add(DateLabel, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(ConventionalWageLabel, 0, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel5, 1, 2)
        Me.TableLayoutPanel2.Controls.Add(BonusYearlyLabel, 0, 4)
        Me.TableLayoutPanel2.Controls.Add(TotalWageLabel, 0, 6)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 67)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 10
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(889, 275)
        Me.TableLayoutPanel2.TabIndex = 1
        '
        'TableLayoutPanel9
        '
        Me.TableLayoutPanel9.ColumnCount = 8
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 21.0!))
        Me.TableLayoutPanel9.Controls.Add(Me.ApplicableVDUHourlyAccTextBox, 6, 0)
        Me.TableLayoutPanel9.Controls.Add(ApplicableVDUHourlyLabel, 5, 0)
        Me.TableLayoutPanel9.Controls.Add(Me.BonusVDUHourlyAccTextBox, 0, 0)
        Me.TableLayoutPanel9.Controls.Add(WageVDUHourlyLabel, 2, 0)
        Me.TableLayoutPanel9.Controls.Add(Me.WageVDUHourlyAccTextBox, 3, 0)
        Me.TableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel9.Location = New System.Drawing.Point(151, 240)
        Me.TableLayoutPanel9.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel9.Name = "TableLayoutPanel9"
        Me.TableLayoutPanel9.RowCount = 1
        Me.TableLayoutPanel9.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel9.Size = New System.Drawing.Size(738, 30)
        Me.TableLayoutPanel9.TabIndex = 28
        '
        'ApplicableVDUHourlyAccTextBox
        '
        Me.ApplicableVDUHourlyAccTextBox.DataBindings.Add(New System.Windows.Forms.Binding("DecimalValue", Me.WorkersVDUInfoBindingSource, "ApplicableVDUHourly", True))
        Me.ApplicableVDUHourlyAccTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ApplicableVDUHourlyAccTextBox.KeepBackColorWhenReadOnly = False
        Me.ApplicableVDUHourlyAccTextBox.Location = New System.Drawing.Point(565, 3)
        Me.ApplicableVDUHourlyAccTextBox.Name = "ApplicableVDUHourlyAccTextBox"
        Me.ApplicableVDUHourlyAccTextBox.ReadOnly = True
        Me.ApplicableVDUHourlyAccTextBox.Size = New System.Drawing.Size(149, 20)
        Me.ApplicableVDUHourlyAccTextBox.TabIndex = 31
        Me.ApplicableVDUHourlyAccTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'WorkersVDUInfoBindingSource
        '
        Me.WorkersVDUInfoBindingSource.DataSource = GetType(ApskaitaObjects.ActiveReports.WorkersVDUInfo)
        '
        'BonusVDUHourlyAccTextBox
        '
        Me.BonusVDUHourlyAccTextBox.DataBindings.Add(New System.Windows.Forms.Binding("DecimalValue", Me.WorkersVDUInfoBindingSource, "BonusVDUHourly", True))
        Me.BonusVDUHourlyAccTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BonusVDUHourlyAccTextBox.KeepBackColorWhenReadOnly = False
        Me.BonusVDUHourlyAccTextBox.Location = New System.Drawing.Point(3, 3)
        Me.BonusVDUHourlyAccTextBox.Name = "BonusVDUHourlyAccTextBox"
        Me.BonusVDUHourlyAccTextBox.ReadOnly = True
        Me.BonusVDUHourlyAccTextBox.Size = New System.Drawing.Size(149, 20)
        Me.BonusVDUHourlyAccTextBox.TabIndex = 26
        Me.BonusVDUHourlyAccTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'WageVDUHourlyAccTextBox
        '
        Me.WageVDUHourlyAccTextBox.DataBindings.Add(New System.Windows.Forms.Binding("DecimalValue", Me.WorkersVDUInfoBindingSource, "WageVDUHourly", True))
        Me.WageVDUHourlyAccTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WageVDUHourlyAccTextBox.KeepBackColorWhenReadOnly = False
        Me.WageVDUHourlyAccTextBox.Location = New System.Drawing.Point(321, 3)
        Me.WageVDUHourlyAccTextBox.Name = "WageVDUHourlyAccTextBox"
        Me.WageVDUHourlyAccTextBox.ReadOnly = True
        Me.WageVDUHourlyAccTextBox.Size = New System.Drawing.Size(149, 20)
        Me.WageVDUHourlyAccTextBox.TabIndex = 12
        Me.WageVDUHourlyAccTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TableLayoutPanel12
        '
        Me.TableLayoutPanel12.ColumnCount = 8
        Me.TableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel12.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel12.Controls.Add(Me.BonusVDUDailyAccTextBox, 0, 0)
        Me.TableLayoutPanel12.Controls.Add(WageVDUDailyLabel, 2, 0)
        Me.TableLayoutPanel12.Controls.Add(Me.ApplicableVDUDailyAccTextBox, 6, 0)
        Me.TableLayoutPanel12.Controls.Add(ApplicableVDUDailyLabel, 5, 0)
        Me.TableLayoutPanel12.Controls.Add(Me.WageVDUDailyAccTextBox, 3, 0)
        Me.TableLayoutPanel12.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel12.Location = New System.Drawing.Point(151, 210)
        Me.TableLayoutPanel12.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel12.Name = "TableLayoutPanel12"
        Me.TableLayoutPanel12.RowCount = 1
        Me.TableLayoutPanel12.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel12.Size = New System.Drawing.Size(738, 30)
        Me.TableLayoutPanel12.TabIndex = 29
        '
        'BonusVDUDailyAccTextBox
        '
        Me.BonusVDUDailyAccTextBox.DataBindings.Add(New System.Windows.Forms.Binding("DecimalValue", Me.WorkersVDUInfoBindingSource, "BonusVDUDaily", True))
        Me.BonusVDUDailyAccTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BonusVDUDailyAccTextBox.KeepBackColorWhenReadOnly = False
        Me.BonusVDUDailyAccTextBox.Location = New System.Drawing.Point(3, 3)
        Me.BonusVDUDailyAccTextBox.Name = "BonusVDUDailyAccTextBox"
        Me.BonusVDUDailyAccTextBox.ReadOnly = True
        Me.BonusVDUDailyAccTextBox.Size = New System.Drawing.Size(138, 20)
        Me.BonusVDUDailyAccTextBox.TabIndex = 24
        Me.BonusVDUDailyAccTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'ApplicableVDUDailyAccTextBox
        '
        Me.ApplicableVDUDailyAccTextBox.DataBindings.Add(New System.Windows.Forms.Binding("DecimalValue", Me.WorkersVDUInfoBindingSource, "ApplicableVDUDaily", True))
        Me.ApplicableVDUDailyAccTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ApplicableVDUDailyAccTextBox.KeepBackColorWhenReadOnly = False
        Me.ApplicableVDUDailyAccTextBox.Location = New System.Drawing.Point(577, 3)
        Me.ApplicableVDUDailyAccTextBox.Name = "ApplicableVDUDailyAccTextBox"
        Me.ApplicableVDUDailyAccTextBox.ReadOnly = True
        Me.ApplicableVDUDailyAccTextBox.Size = New System.Drawing.Size(138, 20)
        Me.ApplicableVDUDailyAccTextBox.TabIndex = 30
        Me.ApplicableVDUDailyAccTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'WageVDUDailyAccTextBox
        '
        Me.WageVDUDailyAccTextBox.DataBindings.Add(New System.Windows.Forms.Binding("DecimalValue", Me.WorkersVDUInfoBindingSource, "WageVDUDaily", True))
        Me.WageVDUDailyAccTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WageVDUDailyAccTextBox.KeepBackColorWhenReadOnly = False
        Me.WageVDUDailyAccTextBox.Location = New System.Drawing.Point(327, 3)
        Me.WageVDUDailyAccTextBox.Name = "WageVDUDailyAccTextBox"
        Me.WageVDUDailyAccTextBox.ReadOnly = True
        Me.WageVDUDailyAccTextBox.Size = New System.Drawing.Size(138, 20)
        Me.WageVDUDailyAccTextBox.TabIndex = 10
        Me.WageVDUDailyAccTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TableLayoutPanel11
        '
        Me.TableLayoutPanel11.ColumnCount = 8
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel11.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 21.0!))
        Me.TableLayoutPanel11.Controls.Add(Me.TotalWageAccTextBox, 0, 0)
        Me.TableLayoutPanel11.Controls.Add(TotalWorkDaysLabel, 2, 0)
        Me.TableLayoutPanel11.Controls.Add(Me.TotalWorkDaysTextBox, 3, 0)
        Me.TableLayoutPanel11.Controls.Add(TotalWorkHoursLabel, 5, 0)
        Me.TableLayoutPanel11.Controls.Add(Me.TotalWorkHoursAccTextBox, 6, 0)
        Me.TableLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel11.Location = New System.Drawing.Point(151, 180)
        Me.TableLayoutPanel11.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel11.Name = "TableLayoutPanel11"
        Me.TableLayoutPanel11.RowCount = 1
        Me.TableLayoutPanel11.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel11.Size = New System.Drawing.Size(738, 30)
        Me.TableLayoutPanel11.TabIndex = 29
        '
        'TotalWageAccTextBox
        '
        Me.TotalWageAccTextBox.DataBindings.Add(New System.Windows.Forms.Binding("DecimalValue", Me.WorkersVDUInfoBindingSource, "TotalWage", True))
        Me.TotalWageAccTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TotalWageAccTextBox.KeepBackColorWhenReadOnly = False
        Me.TotalWageAccTextBox.Location = New System.Drawing.Point(3, 3)
        Me.TotalWageAccTextBox.Name = "TotalWageAccTextBox"
        Me.TotalWageAccTextBox.ReadOnly = True
        Me.TotalWageAccTextBox.Size = New System.Drawing.Size(175, 20)
        Me.TotalWageAccTextBox.TabIndex = 4
        Me.TotalWageAccTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TotalWorkDaysTextBox
        '
        Me.TotalWorkDaysTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.WorkersVDUInfoBindingSource, "TotalWorkDays", True))
        Me.TotalWorkDaysTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TotalWorkDaysTextBox.Location = New System.Drawing.Point(271, 3)
        Me.TotalWorkDaysTextBox.Name = "TotalWorkDaysTextBox"
        Me.TotalWorkDaysTextBox.ReadOnly = True
        Me.TotalWorkDaysTextBox.Size = New System.Drawing.Size(175, 20)
        Me.TotalWorkDaysTextBox.TabIndex = 6
        Me.TotalWorkDaysTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TotalWorkHoursAccTextBox
        '
        Me.TotalWorkHoursAccTextBox.DataBindings.Add(New System.Windows.Forms.Binding("DecimalValue", Me.WorkersVDUInfoBindingSource, "TotalWorkHours", True))
        Me.TotalWorkHoursAccTextBox.DecimalLength = 4
        Me.TotalWorkHoursAccTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TotalWorkHoursAccTextBox.KeepBackColorWhenReadOnly = False
        Me.TotalWorkHoursAccTextBox.Location = New System.Drawing.Point(539, 3)
        Me.TotalWorkHoursAccTextBox.Name = "TotalWorkHoursAccTextBox"
        Me.TotalWorkHoursAccTextBox.ReadOnly = True
        Me.TotalWorkHoursAccTextBox.Size = New System.Drawing.Size(175, 20)
        Me.TotalWorkHoursAccTextBox.TabIndex = 8
        Me.TotalWorkHoursAccTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TableLayoutPanel10
        '
        Me.TableLayoutPanel10.ColumnCount = 8
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334!))
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100.0!))
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334!))
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 35.0!))
        Me.TableLayoutPanel10.Controls.Add(Me.TotalScheduledDaysTextBox, 0, 0)
        Me.TableLayoutPanel10.Controls.Add(TotalScheduledHoursLabel, 2, 0)
        Me.TableLayoutPanel10.Controls.Add(Me.TotalScheduledHoursAccTextBox, 3, 0)
        Me.TableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel10.Location = New System.Drawing.Point(151, 150)
        Me.TableLayoutPanel10.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel10.Name = "TableLayoutPanel10"
        Me.TableLayoutPanel10.RowCount = 1
        Me.TableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel10.Size = New System.Drawing.Size(738, 30)
        Me.TableLayoutPanel10.TabIndex = 29
        '
        'TotalScheduledDaysTextBox
        '
        Me.TotalScheduledDaysTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.WorkersVDUInfoBindingSource, "TotalScheduledDays", True))
        Me.TotalScheduledDaysTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TotalScheduledDaysTextBox.Location = New System.Drawing.Point(3, 3)
        Me.TotalScheduledDaysTextBox.Name = "TotalScheduledDaysTextBox"
        Me.TotalScheduledDaysTextBox.ReadOnly = True
        Me.TotalScheduledDaysTextBox.Size = New System.Drawing.Size(144, 20)
        Me.TotalScheduledDaysTextBox.TabIndex = 20
        Me.TotalScheduledDaysTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TotalScheduledHoursAccTextBox
        '
        Me.TotalScheduledHoursAccTextBox.DataBindings.Add(New System.Windows.Forms.Binding("DecimalValue", Me.WorkersVDUInfoBindingSource, "TotalScheduledHours", True))
        Me.TotalScheduledHoursAccTextBox.DecimalLength = 4
        Me.TotalScheduledHoursAccTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TotalScheduledHoursAccTextBox.KeepBackColorWhenReadOnly = False
        Me.TotalScheduledHoursAccTextBox.Location = New System.Drawing.Point(283, 3)
        Me.TotalScheduledHoursAccTextBox.Name = "TotalScheduledHoursAccTextBox"
        Me.TotalScheduledHoursAccTextBox.ReadOnly = True
        Me.TotalScheduledHoursAccTextBox.Size = New System.Drawing.Size(145, 20)
        Me.TotalScheduledHoursAccTextBox.TabIndex = 22
        Me.TotalScheduledHoursAccTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TableLayoutPanel7
        '
        Me.TableLayoutPanel7.ColumnCount = 10
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel7.Controls.Add(WorkLoadLabel, 5, 0)
        Me.TableLayoutPanel7.Controls.Add(Me.StandartDaysForTheCurrentMonthTextBox, 0, 0)
        Me.TableLayoutPanel7.Controls.Add(Me.WorkLoadAccTextBox, 6, 0)
        Me.TableLayoutPanel7.Controls.Add(Me.StandartHoursForTheCurrentMonthAccTextBox, 3, 0)
        Me.TableLayoutPanel7.Controls.Add(StandartHoursForTheCurrentMonthLabel, 2, 0)
        Me.TableLayoutPanel7.Controls.Add(Me.IncludeCurrentMonthCheckBox, 8, 0)
        Me.TableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel7.Location = New System.Drawing.Point(151, 90)
        Me.TableLayoutPanel7.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel7.Name = "TableLayoutPanel7"
        Me.TableLayoutPanel7.RowCount = 1
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel7.Size = New System.Drawing.Size(738, 30)
        Me.TableLayoutPanel7.TabIndex = 9
        '
        'StandartDaysForTheCurrentMonthTextBox
        '
        Me.StandartDaysForTheCurrentMonthTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.WorkersVDUInfoBindingSource, "StandartDaysForTheCurrentMonth", True))
        Me.StandartDaysForTheCurrentMonthTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.StandartDaysForTheCurrentMonthTextBox.Location = New System.Drawing.Point(3, 3)
        Me.StandartDaysForTheCurrentMonthTextBox.Name = "StandartDaysForTheCurrentMonthTextBox"
        Me.StandartDaysForTheCurrentMonthTextBox.ReadOnly = True
        Me.StandartDaysForTheCurrentMonthTextBox.Size = New System.Drawing.Size(68, 20)
        Me.StandartDaysForTheCurrentMonthTextBox.TabIndex = 6
        Me.StandartDaysForTheCurrentMonthTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'WorkLoadAccTextBox
        '
        Me.WorkLoadAccTextBox.DataBindings.Add(New System.Windows.Forms.Binding("DecimalValue", Me.WorkersVDUInfoBindingSource, "WorkLoad", True))
        Me.WorkLoadAccTextBox.DecimalLength = 4
        Me.WorkLoadAccTextBox.KeepBackColorWhenReadOnly = False
        Me.WorkLoadAccTextBox.Location = New System.Drawing.Point(412, 3)
        Me.WorkLoadAccTextBox.Name = "WorkLoadAccTextBox"
        Me.WorkLoadAccTextBox.ReadOnly = True
        Me.WorkLoadAccTextBox.Size = New System.Drawing.Size(67, 20)
        Me.WorkLoadAccTextBox.TabIndex = 14
        Me.WorkLoadAccTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'StandartHoursForTheCurrentMonthAccTextBox
        '
        Me.StandartHoursForTheCurrentMonthAccTextBox.DataBindings.Add(New System.Windows.Forms.Binding("DecimalValue", Me.WorkersVDUInfoBindingSource, "StandartHoursForTheCurrentMonth", True))
        Me.StandartHoursForTheCurrentMonthAccTextBox.DecimalLength = 4
        Me.StandartHoursForTheCurrentMonthAccTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.StandartHoursForTheCurrentMonthAccTextBox.KeepBackColorWhenReadOnly = False
        Me.StandartHoursForTheCurrentMonthAccTextBox.Location = New System.Drawing.Point(222, 3)
        Me.StandartHoursForTheCurrentMonthAccTextBox.Name = "StandartHoursForTheCurrentMonthAccTextBox"
        Me.StandartHoursForTheCurrentMonthAccTextBox.ReadOnly = True
        Me.StandartHoursForTheCurrentMonthAccTextBox.Size = New System.Drawing.Size(68, 20)
        Me.StandartHoursForTheCurrentMonthAccTextBox.TabIndex = 8
        Me.StandartHoursForTheCurrentMonthAccTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'IncludeCurrentMonthCheckBox
        '
        Me.IncludeCurrentMonthCheckBox.AutoSize = True
        Me.IncludeCurrentMonthCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("CheckState", Me.WorkersVDUInfoBindingSource, "IncludeCurrentMonth", True))
        Me.IncludeCurrentMonthCheckBox.Enabled = False
        Me.IncludeCurrentMonthCheckBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.IncludeCurrentMonthCheckBox.Location = New System.Drawing.Point(506, 5)
        Me.IncludeCurrentMonthCheckBox.Margin = New System.Windows.Forms.Padding(3, 5, 3, 3)
        Me.IncludeCurrentMonthCheckBox.Name = "IncludeCurrentMonthCheckBox"
        Me.IncludeCurrentMonthCheckBox.Size = New System.Drawing.Size(209, 17)
        Me.IncludeCurrentMonthCheckBox.TabIndex = 4
        Me.IncludeCurrentMonthCheckBox.Text = "Įskaičiuojamas einamasis mėnuo"
        Me.IncludeCurrentMonthCheckBox.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel8
        '
        Me.TableLayoutPanel8.ColumnCount = 8
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 21.0!))
        Me.TableLayoutPanel8.Controls.Add(Me.BonusYearlyAccTextBox, 0, 0)
        Me.TableLayoutPanel8.Controls.Add(BonusQuarterlyLabel, 2, 0)
        Me.TableLayoutPanel8.Controls.Add(Me.BonusQuarterlyAccTextBox, 3, 0)
        Me.TableLayoutPanel8.Controls.Add(BonusBaseLabel, 5, 0)
        Me.TableLayoutPanel8.Controls.Add(Me.BonusBaseAccTextBox, 6, 0)
        Me.TableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel8.Location = New System.Drawing.Point(151, 120)
        Me.TableLayoutPanel8.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel8.Name = "TableLayoutPanel8"
        Me.TableLayoutPanel8.RowCount = 1
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel8.Size = New System.Drawing.Size(738, 30)
        Me.TableLayoutPanel8.TabIndex = 27
        '
        'BonusYearlyAccTextBox
        '
        Me.BonusYearlyAccTextBox.DataBindings.Add(New System.Windows.Forms.Binding("DecimalValue", Me.WorkersVDUInfoBindingSource, "BonusYearly", True))
        Me.BonusYearlyAccTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BonusYearlyAccTextBox.KeepBackColorWhenReadOnly = False
        Me.BonusYearlyAccTextBox.Location = New System.Drawing.Point(3, 3)
        Me.BonusYearlyAccTextBox.Name = "BonusYearlyAccTextBox"
        Me.BonusYearlyAccTextBox.ReadOnly = True
        Me.BonusYearlyAccTextBox.Size = New System.Drawing.Size(159, 20)
        Me.BonusYearlyAccTextBox.TabIndex = 14
        Me.BonusYearlyAccTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'BonusQuarterlyAccTextBox
        '
        Me.BonusQuarterlyAccTextBox.DataBindings.Add(New System.Windows.Forms.Binding("DecimalValue", Me.WorkersVDUInfoBindingSource, "BonusQuarterly", True))
        Me.BonusQuarterlyAccTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BonusQuarterlyAccTextBox.KeepBackColorWhenReadOnly = False
        Me.BonusQuarterlyAccTextBox.Location = New System.Drawing.Point(280, 3)
        Me.BonusQuarterlyAccTextBox.Name = "BonusQuarterlyAccTextBox"
        Me.BonusQuarterlyAccTextBox.ReadOnly = True
        Me.BonusQuarterlyAccTextBox.Size = New System.Drawing.Size(159, 20)
        Me.BonusQuarterlyAccTextBox.TabIndex = 16
        Me.BonusQuarterlyAccTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'BonusBaseAccTextBox
        '
        Me.BonusBaseAccTextBox.DataBindings.Add(New System.Windows.Forms.Binding("DecimalValue", Me.WorkersVDUInfoBindingSource, "BonusBase", True))
        Me.BonusBaseAccTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BonusBaseAccTextBox.KeepBackColorWhenReadOnly = False
        Me.BonusBaseAccTextBox.Location = New System.Drawing.Point(555, 3)
        Me.BonusBaseAccTextBox.Name = "BonusBaseAccTextBox"
        Me.BonusBaseAccTextBox.ReadOnly = True
        Me.BonusBaseAccTextBox.Size = New System.Drawing.Size(159, 20)
        Me.BonusBaseAccTextBox.TabIndex = 18
        Me.BonusBaseAccTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TableLayoutPanel6
        '
        Me.TableLayoutPanel6.ColumnCount = 8
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel6.Controls.Add(PositionLabel, 5, 0)
        Me.TableLayoutPanel6.Controls.Add(Me.PersonCodeTextBox, 3, 0)
        Me.TableLayoutPanel6.Controls.Add(PersonCodeLabel, 2, 0)
        Me.TableLayoutPanel6.Controls.Add(Me.PersonNameTextBox, 0, 0)
        Me.TableLayoutPanel6.Controls.Add(Me.PositionTextBox, 6, 0)
        Me.TableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel6.Location = New System.Drawing.Point(151, 30)
        Me.TableLayoutPanel6.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel6.Name = "TableLayoutPanel6"
        Me.TableLayoutPanel6.RowCount = 1
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel6.Size = New System.Drawing.Size(738, 30)
        Me.TableLayoutPanel6.TabIndex = 9
        '
        'PersonCodeTextBox
        '
        Me.PersonCodeTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.WorkersVDUInfoBindingSource, "PersonCode", True))
        Me.PersonCodeTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PersonCodeTextBox.Location = New System.Drawing.Point(293, 3)
        Me.PersonCodeTextBox.Name = "PersonCodeTextBox"
        Me.PersonCodeTextBox.ReadOnly = True
        Me.PersonCodeTextBox.Size = New System.Drawing.Size(165, 20)
        Me.PersonCodeTextBox.TabIndex = 8
        Me.PersonCodeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'PersonNameTextBox
        '
        Me.PersonNameTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.WorkersVDUInfoBindingSource, "PersonName", True))
        Me.PersonNameTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PersonNameTextBox.Location = New System.Drawing.Point(3, 3)
        Me.PersonNameTextBox.Name = "PersonNameTextBox"
        Me.PersonNameTextBox.ReadOnly = True
        Me.PersonNameTextBox.Size = New System.Drawing.Size(165, 20)
        Me.PersonNameTextBox.TabIndex = 4
        Me.PersonNameTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'PositionTextBox
        '
        Me.PositionTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.WorkersVDUInfoBindingSource, "Position", True))
        Me.PositionTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PositionTextBox.Location = New System.Drawing.Point(550, 3)
        Me.PositionTextBox.Name = "PositionTextBox"
        Me.PositionTextBox.ReadOnly = True
        Me.PositionTextBox.Size = New System.Drawing.Size(165, 20)
        Me.PositionTextBox.TabIndex = 6
        Me.PositionTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 8
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 21.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.DateDateTimePicker1, 0, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.ContractNumberTextBox, 6, 0)
        Me.TableLayoutPanel4.Controls.Add(ContractNumberLabel, 5, 0)
        Me.TableLayoutPanel4.Controls.Add(ContractSerialLabel, 2, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.ContractSerialTextBox, 3, 0)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(151, 0)
        Me.TableLayoutPanel4.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 1
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(738, 30)
        Me.TableLayoutPanel4.TabIndex = 3
        '
        'DateDateTimePicker1
        '
        Me.DateDateTimePicker1.DataBindings.Add(New System.Windows.Forms.Binding("Value", Me.WorkersVDUInfoBindingSource, "Date", True))
        Me.DateDateTimePicker1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DateDateTimePicker1.Enabled = False
        Me.DateDateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateDateTimePicker1.Location = New System.Drawing.Point(3, 3)
        Me.DateDateTimePicker1.Name = "DateDateTimePicker1"
        Me.DateDateTimePicker1.Size = New System.Drawing.Size(159, 20)
        Me.DateDateTimePicker1.TabIndex = 4
        '
        'ContractNumberTextBox
        '
        Me.ContractNumberTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.WorkersVDUInfoBindingSource, "ContractNumber", True))
        Me.ContractNumberTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ContractNumberTextBox.Location = New System.Drawing.Point(555, 3)
        Me.ContractNumberTextBox.Name = "ContractNumberTextBox"
        Me.ContractNumberTextBox.ReadOnly = True
        Me.ContractNumberTextBox.Size = New System.Drawing.Size(159, 20)
        Me.ContractNumberTextBox.TabIndex = 6
        Me.ContractNumberTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'ContractSerialTextBox
        '
        Me.ContractSerialTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.WorkersVDUInfoBindingSource, "ContractSerial", True))
        Me.ContractSerialTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ContractSerialTextBox.Location = New System.Drawing.Point(282, 3)
        Me.ContractSerialTextBox.Name = "ContractSerialTextBox"
        Me.ContractSerialTextBox.ReadOnly = True
        Me.ContractSerialTextBox.Size = New System.Drawing.Size(159, 20)
        Me.ContractSerialTextBox.TabIndex = 4
        Me.ContractSerialTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TableLayoutPanel5
        '
        Me.TableLayoutPanel5.ColumnCount = 8
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 21.0!))
        Me.TableLayoutPanel5.Controls.Add(Me.ConventionalWageAccTextBox, 0, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.WageTypeHumanReadableTextBox, 3, 0)
        Me.TableLayoutPanel5.Controls.Add(WageTypeHumanReadableLabel, 2, 0)
        Me.TableLayoutPanel5.Controls.Add(ConventionalExtraPayLabel, 5, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.ConventionalExtraPayAccTextBox, 6, 0)
        Me.TableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(151, 60)
        Me.TableLayoutPanel5.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 1
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(738, 30)
        Me.TableLayoutPanel5.TabIndex = 9
        '
        'ConventionalWageAccTextBox
        '
        Me.ConventionalWageAccTextBox.DataBindings.Add(New System.Windows.Forms.Binding("DecimalValue", Me.WorkersVDUInfoBindingSource, "ConventionalWage", True))
        Me.ConventionalWageAccTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ConventionalWageAccTextBox.KeepBackColorWhenReadOnly = False
        Me.ConventionalWageAccTextBox.Location = New System.Drawing.Point(3, 3)
        Me.ConventionalWageAccTextBox.Name = "ConventionalWageAccTextBox"
        Me.ConventionalWageAccTextBox.ReadOnly = True
        Me.ConventionalWageAccTextBox.Size = New System.Drawing.Size(184, 20)
        Me.ConventionalWageAccTextBox.TabIndex = 6
        Me.ConventionalWageAccTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'WageTypeHumanReadableTextBox
        '
        Me.WageTypeHumanReadableTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.WorkersVDUInfoBindingSource, "WageTypeHumanReadable", True))
        Me.WageTypeHumanReadableTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WageTypeHumanReadableTextBox.Location = New System.Drawing.Point(261, 3)
        Me.WageTypeHumanReadableTextBox.Name = "WageTypeHumanReadableTextBox"
        Me.WageTypeHumanReadableTextBox.ReadOnly = True
        Me.WageTypeHumanReadableTextBox.Size = New System.Drawing.Size(184, 20)
        Me.WageTypeHumanReadableTextBox.TabIndex = 4
        Me.WageTypeHumanReadableTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'ConventionalExtraPayAccTextBox
        '
        Me.ConventionalExtraPayAccTextBox.DataBindings.Add(New System.Windows.Forms.Binding("DecimalValue", Me.WorkersVDUInfoBindingSource, "ConventionalExtraPay", True))
        Me.ConventionalExtraPayAccTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ConventionalExtraPayAccTextBox.KeepBackColorWhenReadOnly = False
        Me.ConventionalExtraPayAccTextBox.Location = New System.Drawing.Point(530, 3)
        Me.ConventionalExtraPayAccTextBox.Name = "ConventionalExtraPayAccTextBox"
        Me.ConventionalExtraPayAccTextBox.ReadOnly = True
        Me.ConventionalExtraPayAccTextBox.Size = New System.Drawing.Size(184, 20)
        Me.ConventionalExtraPayAccTextBox.TabIndex = 4
        Me.ConventionalExtraPayAccTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel3.ColumnCount = 1
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.TableLayoutPanel13, 0, 1)
        Me.TableLayoutPanel3.Controls.Add(Me.TableLayoutPanel1, 0, 0)
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel3.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 2
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(835, 58)
        Me.TableLayoutPanel3.TabIndex = 2
        '
        'TableLayoutPanel13
        '
        Me.TableLayoutPanel13.ColumnCount = 8
        Me.TableLayoutPanel13.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel13.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel13.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 105.0!))
        Me.TableLayoutPanel13.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel13.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel13.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 88.0!))
        Me.TableLayoutPanel13.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel13.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel13.Controls.Add(Me.IncludeCurrentCheckBox, 0, 0)
        Me.TableLayoutPanel13.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel13.Location = New System.Drawing.Point(0, 29)
        Me.TableLayoutPanel13.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel13.Name = "TableLayoutPanel13"
        Me.TableLayoutPanel13.RowCount = 1
        Me.TableLayoutPanel13.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel13.Size = New System.Drawing.Size(835, 29)
        Me.TableLayoutPanel13.TabIndex = 3
        '
        'IncludeCurrentCheckBox
        '
        Me.IncludeCurrentCheckBox.AutoSize = True
        Me.IncludeCurrentCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("CheckState", Me.WorkersVDUInfoBindingSource, "IncludeCurrentMonth", True))
        Me.IncludeCurrentCheckBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.IncludeCurrentCheckBox.Location = New System.Drawing.Point(3, 5)
        Me.IncludeCurrentCheckBox.Margin = New System.Windows.Forms.Padding(3, 5, 3, 3)
        Me.IncludeCurrentCheckBox.Name = "IncludeCurrentCheckBox"
        Me.IncludeCurrentCheckBox.Size = New System.Drawing.Size(156, 17)
        Me.IncludeCurrentCheckBox.TabIndex = 5
        Me.IncludeCurrentCheckBox.Text = "Įskaičiuoti einam. mėn."
        Me.IncludeCurrentCheckBox.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.AutoSize = True
        Me.Panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel1.Controls.Add(Me.TableLayoutPanel3)
        Me.Panel1.Controls.Add(Me.RefreshButton)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(889, 58)
        Me.Panel1.TabIndex = 3
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 344)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.AutoScroll = True
        Me.SplitContainer1.Panel1.Controls.Add(Me.WageListDataListView)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.AutoScroll = True
        Me.SplitContainer1.Panel2.Controls.Add(Me.BonusListListDataListView)
        Me.SplitContainer1.Size = New System.Drawing.Size(895, 177)
        Me.SplitContainer1.SplitterDistance = 485
        Me.SplitContainer1.TabIndex = 4
        '
        'WageListDataListView
        '
        Me.WageListDataListView.AllColumns.Add(Me.OlvColumn1)
        Me.WageListDataListView.AllColumns.Add(Me.OlvColumn2)
        Me.WageListDataListView.AllColumns.Add(Me.OlvColumn3)
        Me.WageListDataListView.AllColumns.Add(Me.OlvColumn4)
        Me.WageListDataListView.AllColumns.Add(Me.OlvColumn5)
        Me.WageListDataListView.AllColumns.Add(Me.OlvColumn6)
        Me.WageListDataListView.AllColumns.Add(Me.OlvColumn7)
        Me.WageListDataListView.AllowColumnReorder = True
        Me.WageListDataListView.AutoGenerateColumns = False
        Me.WageListDataListView.CausesValidation = False
        Me.WageListDataListView.CellEditUseWholeCell = False
        Me.WageListDataListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.OlvColumn1, Me.OlvColumn2, Me.OlvColumn3, Me.OlvColumn4, Me.OlvColumn5, Me.OlvColumn6, Me.OlvColumn7})
        Me.WageListDataListView.Cursor = System.Windows.Forms.Cursors.Default
        Me.WageListDataListView.DataSource = Me.WageListBindingSource
        Me.WageListDataListView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WageListDataListView.FullRowSelect = True
        Me.WageListDataListView.HasCollapsibleGroups = False
        Me.WageListDataListView.HeaderWordWrap = True
        Me.WageListDataListView.HideSelection = False
        Me.WageListDataListView.IncludeColumnHeadersInCopy = True
        Me.WageListDataListView.Location = New System.Drawing.Point(0, 0)
        Me.WageListDataListView.Name = "WageListDataListView"
        Me.WageListDataListView.RenderNonEditableCheckboxesAsDisabled = True
        Me.WageListDataListView.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.Submenu
        Me.WageListDataListView.SelectedBackColor = System.Drawing.Color.PaleGreen
        Me.WageListDataListView.SelectedForeColor = System.Drawing.Color.Black
        Me.WageListDataListView.ShowCommandMenuOnRightClick = True
        Me.WageListDataListView.ShowGroups = False
        Me.WageListDataListView.ShowImagesOnSubItems = True
        Me.WageListDataListView.ShowItemCountOnGroups = True
        Me.WageListDataListView.ShowItemToolTips = True
        Me.WageListDataListView.Size = New System.Drawing.Size(485, 177)
        Me.WageListDataListView.TabIndex = 4
        Me.WageListDataListView.UnfocusedSelectedBackColor = System.Drawing.Color.PaleGreen
        Me.WageListDataListView.UnfocusedSelectedForeColor = System.Drawing.Color.Black
        Me.WageListDataListView.UseCellFormatEvents = True
        Me.WageListDataListView.UseCompatibleStateImageBehavior = False
        Me.WageListDataListView.UseFilterIndicator = True
        Me.WageListDataListView.UseFiltering = True
        Me.WageListDataListView.UseHotItem = True
        Me.WageListDataListView.UseNotifyPropertyChanged = True
        Me.WageListDataListView.View = System.Windows.Forms.View.Details
        '
        'OlvColumn1
        '
        Me.OlvColumn1.AspectName = "Year"
        Me.OlvColumn1.CellEditUseWholeCell = True
        Me.OlvColumn1.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn1.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn1.IsEditable = False
        Me.OlvColumn1.Text = "Metai"
        Me.OlvColumn1.ToolTipText = ""
        Me.OlvColumn1.Width = 55
        '
        'OlvColumn2
        '
        Me.OlvColumn2.AspectName = "Month"
        Me.OlvColumn2.CellEditUseWholeCell = True
        Me.OlvColumn2.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn2.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn2.IsEditable = False
        Me.OlvColumn2.Text = "Mėn."
        Me.OlvColumn2.ToolTipText = ""
        Me.OlvColumn2.Width = 42
        '
        'OlvColumn3
        '
        Me.OlvColumn3.AspectName = "WorkDays"
        Me.OlvColumn3.CellEditUseWholeCell = True
        Me.OlvColumn3.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn3.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn3.IsEditable = False
        Me.OlvColumn3.Text = "D.D."
        Me.OlvColumn3.ToolTipText = ""
        Me.OlvColumn3.Width = 48
        '
        'OlvColumn4
        '
        Me.OlvColumn4.AspectName = "WorkHours"
        Me.OlvColumn4.AspectToStringFormat = "{0:##,0.0000}"
        Me.OlvColumn4.CellEditUseWholeCell = True
        Me.OlvColumn4.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn4.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn4.IsEditable = False
        Me.OlvColumn4.Text = "D.V."
        Me.OlvColumn4.ToolTipText = ""
        Me.OlvColumn4.Width = 59
        '
        'OlvColumn5
        '
        Me.OlvColumn5.AspectName = "ScheduledDays"
        Me.OlvColumn5.CellEditUseWholeCell = True
        Me.OlvColumn5.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn5.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn5.IsEditable = False
        Me.OlvColumn5.Text = "D.D. Grafike"
        Me.OlvColumn5.ToolTipText = ""
        Me.OlvColumn5.Width = 76
        '
        'OlvColumn6
        '
        Me.OlvColumn6.AspectName = "ScheduledHours"
        Me.OlvColumn6.AspectToStringFormat = "{0:##,0.0000}"
        Me.OlvColumn6.CellEditUseWholeCell = True
        Me.OlvColumn6.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn6.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn6.IsEditable = False
        Me.OlvColumn6.Text = "D.V. Grafike"
        Me.OlvColumn6.ToolTipText = ""
        Me.OlvColumn6.Width = 73
        '
        'OlvColumn7
        '
        Me.OlvColumn7.AspectName = "Wage"
        Me.OlvColumn7.AspectToStringFormat = "{0:##,0.00}"
        Me.OlvColumn7.CellEditUseWholeCell = True
        Me.OlvColumn7.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn7.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn7.IsEditable = False
        Me.OlvColumn7.Text = "Darbo Užmokestis"
        Me.OlvColumn7.ToolTipText = ""
        Me.OlvColumn7.Width = 122
        '
        'WageListBindingSource
        '
        Me.WageListBindingSource.DataMember = "WageList"
        Me.WageListBindingSource.DataSource = Me.WorkersVDUInfoBindingSource
        '
        'BonusListListDataListView
        '
        Me.BonusListListDataListView.AllColumns.Add(Me.OlvColumn8)
        Me.BonusListListDataListView.AllColumns.Add(Me.OlvColumn9)
        Me.BonusListListDataListView.AllColumns.Add(Me.OlvColumn10)
        Me.BonusListListDataListView.AllColumns.Add(Me.OlvColumn11)
        Me.BonusListListDataListView.AllowColumnReorder = True
        Me.BonusListListDataListView.AutoGenerateColumns = False
        Me.BonusListListDataListView.CausesValidation = False
        Me.BonusListListDataListView.CellEditUseWholeCell = False
        Me.BonusListListDataListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.OlvColumn8, Me.OlvColumn9, Me.OlvColumn10, Me.OlvColumn11})
        Me.BonusListListDataListView.Cursor = System.Windows.Forms.Cursors.Default
        Me.BonusListListDataListView.DataSource = Me.BonusListBindingSource
        Me.BonusListListDataListView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.BonusListListDataListView.FullRowSelect = True
        Me.BonusListListDataListView.HasCollapsibleGroups = False
        Me.BonusListListDataListView.HeaderWordWrap = True
        Me.BonusListListDataListView.HideSelection = False
        Me.BonusListListDataListView.IncludeColumnHeadersInCopy = True
        Me.BonusListListDataListView.Location = New System.Drawing.Point(0, 0)
        Me.BonusListListDataListView.Name = "BonusListListDataListView"
        Me.BonusListListDataListView.RenderNonEditableCheckboxesAsDisabled = True
        Me.BonusListListDataListView.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.Submenu
        Me.BonusListListDataListView.SelectedBackColor = System.Drawing.Color.PaleGreen
        Me.BonusListListDataListView.SelectedForeColor = System.Drawing.Color.Black
        Me.BonusListListDataListView.ShowCommandMenuOnRightClick = True
        Me.BonusListListDataListView.ShowGroups = False
        Me.BonusListListDataListView.ShowImagesOnSubItems = True
        Me.BonusListListDataListView.ShowItemCountOnGroups = True
        Me.BonusListListDataListView.ShowItemToolTips = True
        Me.BonusListListDataListView.Size = New System.Drawing.Size(406, 177)
        Me.BonusListListDataListView.TabIndex = 4
        Me.BonusListListDataListView.UnfocusedSelectedBackColor = System.Drawing.Color.PaleGreen
        Me.BonusListListDataListView.UnfocusedSelectedForeColor = System.Drawing.Color.Black
        Me.BonusListListDataListView.UseCellFormatEvents = True
        Me.BonusListListDataListView.UseCompatibleStateImageBehavior = False
        Me.BonusListListDataListView.UseFilterIndicator = True
        Me.BonusListListDataListView.UseFiltering = True
        Me.BonusListListDataListView.UseHotItem = True
        Me.BonusListListDataListView.UseNotifyPropertyChanged = True
        Me.BonusListListDataListView.View = System.Windows.Forms.View.Details
        '
        'OlvColumn8
        '
        Me.OlvColumn8.AspectName = "Year"
        Me.OlvColumn8.CellEditUseWholeCell = True
        Me.OlvColumn8.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn8.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn8.IsEditable = False
        Me.OlvColumn8.Text = "Metai"
        Me.OlvColumn8.ToolTipText = ""
        Me.OlvColumn8.Width = 57
        '
        'OlvColumn9
        '
        Me.OlvColumn9.AspectName = "Month"
        Me.OlvColumn9.CellEditUseWholeCell = True
        Me.OlvColumn9.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn9.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn9.IsEditable = False
        Me.OlvColumn9.Text = "Mėn."
        Me.OlvColumn9.ToolTipText = ""
        Me.OlvColumn9.Width = 51
        '
        'OlvColumn10
        '
        Me.OlvColumn10.AspectName = "BonusTypeHumanReadable"
        Me.OlvColumn10.CellEditUseWholeCell = True
        Me.OlvColumn10.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn10.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn10.IsEditable = False
        Me.OlvColumn10.Text = "Tipas"
        Me.OlvColumn10.ToolTipText = ""
        Me.OlvColumn10.Width = 100
        '
        'OlvColumn11
        '
        Me.OlvColumn11.AspectName = "BonusAmount"
        Me.OlvColumn11.AspectToStringFormat = "{0:##,0.00}"
        Me.OlvColumn11.CellEditUseWholeCell = True
        Me.OlvColumn11.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn11.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn11.IsEditable = False
        Me.OlvColumn11.Text = "Suma"
        Me.OlvColumn11.ToolTipText = ""
        Me.OlvColumn11.Width = 100
        '
        'BonusListBindingSource
        '
        Me.BonusListBindingSource.DataMember = "BonusList"
        Me.BonusListBindingSource.DataSource = Me.WorkersVDUInfoBindingSource
        '
        'TableLayoutPanel14
        '
        Me.TableLayoutPanel14.ColumnCount = 1
        Me.TableLayoutPanel14.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel14.Controls.Add(Me.Panel1, 0, 0)
        Me.TableLayoutPanel14.Controls.Add(Me.TableLayoutPanel2, 0, 1)
        Me.TableLayoutPanel14.Dock = System.Windows.Forms.DockStyle.Top
        Me.TableLayoutPanel14.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel14.Name = "TableLayoutPanel14"
        Me.TableLayoutPanel14.RowCount = 2
        Me.TableLayoutPanel14.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel14.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel14.Size = New System.Drawing.Size(895, 344)
        Me.TableLayoutPanel14.TabIndex = 5
        '
        'ProgressFiller1
        '
        Me.ProgressFiller1.Location = New System.Drawing.Point(150, 349)
        Me.ProgressFiller1.Name = "ProgressFiller1"
        Me.ProgressFiller1.Size = New System.Drawing.Size(166, 66)
        Me.ProgressFiller1.TabIndex = 6
        Me.ProgressFiller1.Visible = False
        '
        'ProgressFiller2
        '
        Me.ProgressFiller2.Location = New System.Drawing.Point(346, 353)
        Me.ProgressFiller2.Name = "ProgressFiller2"
        Me.ProgressFiller2.Size = New System.Drawing.Size(249, 49)
        Me.ProgressFiller2.TabIndex = 7
        Me.ProgressFiller2.Visible = False
        '
        'F_WorkersVDUInfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(895, 521)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.TableLayoutPanel14)
        Me.Controls.Add(Me.ProgressFiller2)
        Me.Controls.Add(Me.ProgressFiller1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "F_WorkersVDUInfo"
        Me.ShowInTaskbar = False
        Me.Text = "Pažyma apie darbuotojo VDU"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.TableLayoutPanel9.ResumeLayout(False)
        Me.TableLayoutPanel9.PerformLayout()
        CType(Me.WorkersVDUInfoBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel12.ResumeLayout(False)
        Me.TableLayoutPanel12.PerformLayout()
        Me.TableLayoutPanel11.ResumeLayout(False)
        Me.TableLayoutPanel11.PerformLayout()
        Me.TableLayoutPanel10.ResumeLayout(False)
        Me.TableLayoutPanel10.PerformLayout()
        Me.TableLayoutPanel7.ResumeLayout(False)
        Me.TableLayoutPanel7.PerformLayout()
        Me.TableLayoutPanel8.ResumeLayout(False)
        Me.TableLayoutPanel8.PerformLayout()
        Me.TableLayoutPanel6.ResumeLayout(False)
        Me.TableLayoutPanel6.PerformLayout()
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.TableLayoutPanel4.PerformLayout()
        Me.TableLayoutPanel5.ResumeLayout(False)
        Me.TableLayoutPanel5.PerformLayout()
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        Me.TableLayoutPanel13.ResumeLayout(False)
        Me.TableLayoutPanel13.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.WageListDataListView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.WageListBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BonusListListDataListView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.BonusListBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel14.ResumeLayout(False)
        Me.TableLayoutPanel14.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents DateDateTimePicker As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents RefreshButton As System.Windows.Forms.Button
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel4 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents WorkersVDUInfoBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ConventionalExtraPayAccTextBox As AccControlsWinForms.AccTextBox
    Friend WithEvents ConventionalWageAccTextBox As AccControlsWinForms.AccTextBox
    Friend WithEvents TableLayoutPanel6 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel5 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents IncludeCurrentMonthCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents StandartDaysForTheCurrentMonthTextBox As System.Windows.Forms.TextBox
    Friend WithEvents StandartHoursForTheCurrentMonthAccTextBox As AccControlsWinForms.AccTextBox
    Friend WithEvents TableLayoutPanel7 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents PersonCodeTextBox As System.Windows.Forms.TextBox
    Friend WithEvents PersonNameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents PositionTextBox As System.Windows.Forms.TextBox
    Friend WithEvents DateDateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents ContractNumberTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ContractSerialTextBox As System.Windows.Forms.TextBox
    Friend WithEvents WageTypeHumanReadableTextBox As System.Windows.Forms.TextBox
    Friend WithEvents TotalWageAccTextBox As AccControlsWinForms.AccTextBox
    Friend WithEvents WorkLoadAccTextBox As AccControlsWinForms.AccTextBox
    Friend WithEvents TotalWorkDaysTextBox As System.Windows.Forms.TextBox
    Friend WithEvents TotalWorkHoursAccTextBox As AccControlsWinForms.AccTextBox
    Friend WithEvents WageVDUDailyAccTextBox As AccControlsWinForms.AccTextBox
    Friend WithEvents WageVDUHourlyAccTextBox As AccControlsWinForms.AccTextBox
    Friend WithEvents BonusYearlyAccTextBox As AccControlsWinForms.AccTextBox
    Friend WithEvents BonusQuarterlyAccTextBox As AccControlsWinForms.AccTextBox
    Friend WithEvents BonusBaseAccTextBox As AccControlsWinForms.AccTextBox
    Friend WithEvents TotalScheduledDaysTextBox As System.Windows.Forms.TextBox
    Friend WithEvents TotalScheduledHoursAccTextBox As AccControlsWinForms.AccTextBox
    Friend WithEvents BonusVDUDailyAccTextBox As AccControlsWinForms.AccTextBox
    Friend WithEvents BonusVDUHourlyAccTextBox As AccControlsWinForms.AccTextBox
    Friend WithEvents TableLayoutPanel8 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel9 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel10 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ApplicableVDUHourlyAccTextBox As AccControlsWinForms.AccTextBox
    Friend WithEvents TableLayoutPanel12 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ApplicableVDUDailyAccTextBox As AccControlsWinForms.AccTextBox
    Friend WithEvents TableLayoutPanel11 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel13 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents IncludeCurrentCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents WageListBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents BonusListBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents TableLayoutPanel14 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents WageListDataListView As BrightIdeasSoftware.DataListView
    Friend WithEvents BonusListListDataListView As BrightIdeasSoftware.DataListView
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
    Friend WithEvents ProgressFiller1 As AccControlsWinForms.ProgressFiller
    Friend WithEvents ProgressFiller2 As AccControlsWinForms.ProgressFiller
    Friend WithEvents LabourContractAccListComboBox As AccControlsWinForms.AccListComboBox
End Class
