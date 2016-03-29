<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Friend Class F_WorkerHolidayInfo
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
        Dim ContractDateLabel As System.Windows.Forms.Label
        Dim ContractSerialLabel As System.Windows.Forms.Label
        Dim ContractNumberLabel As System.Windows.Forms.Label
        Dim PersonNameLabel As System.Windows.Forms.Label
        Dim PersonCodeLabel As System.Windows.Forms.Label
        Dim PositionLabel As System.Windows.Forms.Label
        Dim ContractTerminationDateLabel As System.Windows.Forms.Label
        Dim HolidayRateLabel As System.Windows.Forms.Label
        Dim TotalWorkPeriodInDaysLabel As System.Windows.Forms.Label
        Dim TotalWorkPeriodInYearsLabel As System.Windows.Forms.Label
        Dim TotalHolidayDaysGrantedLabel As System.Windows.Forms.Label
        Dim TotalHolidayDaysCorrectionLabel As System.Windows.Forms.Label
        Dim TotalCumulatedHolidayDaysLabel As System.Windows.Forms.Label
        Dim TotalHolidayDaysUsedLabel As System.Windows.Forms.Label
        Dim TotalUnusedHolidayDaysLabel As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(F_WorkerHolidayInfo))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel
        Me.TableLayoutPanel13 = New System.Windows.Forms.TableLayoutPanel
        Me.ForCompensationCheckBox = New System.Windows.Forms.CheckBox
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.RefreshLabourContractsButton = New System.Windows.Forms.Button
        Me.LabourContractComboBox = New System.Windows.Forms.ComboBox
        Me.WorkerAccGridComboBox = New AccControlsWinForms.AccListComboBox
        Me.DateDateTimePicker = New System.Windows.Forms.DateTimePicker
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.RefreshButton = New System.Windows.Forms.Button
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel
        Me.TableLayoutPanel9 = New System.Windows.Forms.TableLayoutPanel
        Me.TotalHolidayDaysGrantedTextBox = New System.Windows.Forms.TextBox
        Me.WorkerHolidayInfoBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.TotalHolidayDaysCorrectionAccTextBox = New AccControlsWinForms.AccTextBox
        Me.TableLayoutPanel8 = New System.Windows.Forms.TableLayoutPanel
        Me.HolidayRateTextBox = New System.Windows.Forms.TextBox
        Me.TotalWorkPeriodInYearsAccTextBox = New AccControlsWinForms.AccTextBox
        Me.TotalWorkPeriodInDaysTextBox = New System.Windows.Forms.TextBox
        Me.TableLayoutPanel5 = New System.Windows.Forms.TableLayoutPanel
        Me.TotalUnusedHolidayDaysAccTextBox = New AccControlsWinForms.AccTextBox
        Me.TotalCumulatedHolidayDaysAccTextBox = New AccControlsWinForms.AccTextBox
        Me.TotalHolidayDaysUsedAccTextBox = New AccControlsWinForms.AccTextBox
        Me.TableLayoutPanel7 = New System.Windows.Forms.TableLayoutPanel
        Me.TotalHolidayDaysCompensatedAccTextBox = New AccControlsWinForms.AccTextBox
        Me.ContractTerminationDateTextBox = New System.Windows.Forms.TextBox
        Me.CompensationIsGrantedCheckBox = New System.Windows.Forms.CheckBox
        Me.IsForCompensationCheckBox = New System.Windows.Forms.CheckBox
        Me.TableLayoutPanel6 = New System.Windows.Forms.TableLayoutPanel
        Me.PersonNameTextBox = New System.Windows.Forms.TextBox
        Me.PersonCodeTextBox = New System.Windows.Forms.TextBox
        Me.PositionTextBox = New System.Windows.Forms.TextBox
        Me.TableLayoutPanel4 = New System.Windows.Forms.TableLayoutPanel
        Me.ContractNumberTextBox = New System.Windows.Forms.TextBox
        Me.DateDateTimePicker1 = New System.Windows.Forms.DateTimePicker
        Me.ContractSerialTextBox = New System.Windows.Forms.TextBox
        Me.ContractDateDateTimePicker = New System.Windows.Forms.DateTimePicker
        Me.TableLayoutPanel10 = New System.Windows.Forms.TableLayoutPanel
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.HolidayCalculatedListDataListView = New BrightIdeasSoftware.DataListView
        Me.OlvColumn1 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn2 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn3 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn4 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn5 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn6 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn7 = New BrightIdeasSoftware.OLVColumn
        Me.HolidayCalculatedListBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.HolidaySpentListDataListView = New BrightIdeasSoftware.DataListView
        Me.OlvColumn8 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn9 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn10 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn11 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn12 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn13 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn14 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn15 = New BrightIdeasSoftware.OLVColumn
        Me.OlvColumn16 = New BrightIdeasSoftware.OLVColumn
        Me.HolidaySpentListBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ProgressFiller1 = New AccControlsWinForms.ProgressFiller
        Me.ProgressFiller2 = New AccControlsWinForms.ProgressFiller
        DateLabel = New System.Windows.Forms.Label
        ContractDateLabel = New System.Windows.Forms.Label
        ContractSerialLabel = New System.Windows.Forms.Label
        ContractNumberLabel = New System.Windows.Forms.Label
        PersonNameLabel = New System.Windows.Forms.Label
        PersonCodeLabel = New System.Windows.Forms.Label
        PositionLabel = New System.Windows.Forms.Label
        ContractTerminationDateLabel = New System.Windows.Forms.Label
        HolidayRateLabel = New System.Windows.Forms.Label
        TotalWorkPeriodInDaysLabel = New System.Windows.Forms.Label
        TotalWorkPeriodInYearsLabel = New System.Windows.Forms.Label
        TotalHolidayDaysGrantedLabel = New System.Windows.Forms.Label
        TotalHolidayDaysCorrectionLabel = New System.Windows.Forms.Label
        TotalCumulatedHolidayDaysLabel = New System.Windows.Forms.Label
        TotalHolidayDaysUsedLabel = New System.Windows.Forms.Label
        TotalUnusedHolidayDaysLabel = New System.Windows.Forms.Label
        Me.Panel1.SuspendLayout()
        Me.TableLayoutPanel3.SuspendLayout()
        Me.TableLayoutPanel13.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        Me.TableLayoutPanel9.SuspendLayout()
        CType(Me.WorkerHolidayInfoBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel8.SuspendLayout()
        Me.TableLayoutPanel5.SuspendLayout()
        Me.TableLayoutPanel7.SuspendLayout()
        Me.TableLayoutPanel6.SuspendLayout()
        Me.TableLayoutPanel4.SuspendLayout()
        Me.TableLayoutPanel10.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.HolidayCalculatedListDataListView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.HolidayCalculatedListBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.HolidaySpentListDataListView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.HolidaySpentListBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'DateLabel
        '
        DateLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DateLabel.AutoSize = True
        DateLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        DateLabel.Location = New System.Drawing.Point(103, 5)
        DateLabel.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        DateLabel.Name = "DateLabel"
        DateLabel.Size = New System.Drawing.Size(38, 13)
        DateLabel.TabIndex = 8
        DateLabel.Text = "Data:"
        DateLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'ContractDateLabel
        '
        ContractDateLabel.AutoSize = True
        ContractDateLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        ContractDateLabel.Location = New System.Drawing.Point(157, 5)
        ContractDateLabel.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        ContractDateLabel.Name = "ContractDateLabel"
        ContractDateLabel.Size = New System.Drawing.Size(92, 13)
        ContractDateLabel.TabIndex = 9
        ContractDateLabel.Text = "Sutarties Data:"
        '
        'ContractSerialLabel
        '
        ContractSerialLabel.AutoSize = True
        ContractSerialLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        ContractSerialLabel.Location = New System.Drawing.Point(409, 5)
        ContractSerialLabel.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        ContractSerialLabel.Name = "ContractSerialLabel"
        ContractSerialLabel.Size = New System.Drawing.Size(43, 13)
        ContractSerialLabel.TabIndex = 10
        ContractSerialLabel.Text = "Serija:"
        '
        'ContractNumberLabel
        '
        ContractNumberLabel.AutoSize = True
        ContractNumberLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        ContractNumberLabel.Location = New System.Drawing.Point(612, 5)
        ContractNumberLabel.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        ContractNumberLabel.Name = "ContractNumberLabel"
        ContractNumberLabel.Size = New System.Drawing.Size(28, 13)
        ContractNumberLabel.TabIndex = 11
        ContractNumberLabel.Text = "Nr.:"
        '
        'PersonNameLabel
        '
        PersonNameLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        PersonNameLabel.AutoSize = True
        PersonNameLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        PersonNameLabel.Location = New System.Drawing.Point(62, 35)
        PersonNameLabel.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        PersonNameLabel.Name = "PersonNameLabel"
        PersonNameLabel.Size = New System.Drawing.Size(79, 13)
        PersonNameLabel.TabIndex = 8
        PersonNameLabel.Text = "Darbuotojas:"
        PersonNameLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'PersonCodeLabel
        '
        PersonCodeLabel.AutoSize = True
        PersonCodeLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        PersonCodeLabel.Location = New System.Drawing.Point(214, 5)
        PersonCodeLabel.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        PersonCodeLabel.Name = "PersonCodeLabel"
        PersonCodeLabel.Size = New System.Drawing.Size(93, 13)
        PersonCodeLabel.TabIndex = 9
        PersonCodeLabel.Text = "Asmens Kodas:"
        '
        'PositionLabel
        '
        PositionLabel.AutoSize = True
        PositionLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        PositionLabel.Location = New System.Drawing.Point(524, 5)
        PositionLabel.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        PositionLabel.Name = "PositionLabel"
        PositionLabel.Size = New System.Drawing.Size(60, 13)
        PositionLabel.TabIndex = 10
        PositionLabel.Text = "Pareigos:"
        '
        'ContractTerminationDateLabel
        '
        ContractTerminationDateLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        ContractTerminationDateLabel.AutoSize = True
        ContractTerminationDateLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        ContractTerminationDateLabel.Location = New System.Drawing.Point(29, 65)
        ContractTerminationDateLabel.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        ContractTerminationDateLabel.Name = "ContractTerminationDateLabel"
        ContractTerminationDateLabel.Size = New System.Drawing.Size(112, 13)
        ContractTerminationDateLabel.TabIndex = 9
        ContractTerminationDateLabel.Text = "Sutartis nutraukta:"
        ContractTerminationDateLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'HolidayRateLabel
        '
        HolidayRateLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        HolidayRateLabel.AutoSize = True
        HolidayRateLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        HolidayRateLabel.Location = New System.Drawing.Point(40, 95)
        HolidayRateLabel.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        HolidayRateLabel.Name = "HolidayRateLabel"
        HolidayRateLabel.Size = New System.Drawing.Size(101, 13)
        HolidayRateLabel.TabIndex = 8
        HolidayRateLabel.Text = "Atostogų Norma:"
        HolidayRateLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'TotalWorkPeriodInDaysLabel
        '
        TotalWorkPeriodInDaysLabel.AutoSize = True
        TotalWorkPeriodInDaysLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        TotalWorkPeriodInDaysLabel.Location = New System.Drawing.Point(192, 5)
        TotalWorkPeriodInDaysLabel.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        TotalWorkPeriodInDaysLabel.Name = "TotalWorkPeriodInDaysLabel"
        TotalWorkPeriodInDaysLabel.Size = New System.Drawing.Size(103, 13)
        TotalWorkPeriodInDaysLabel.TabIndex = 9
        TotalWorkPeriodInDaysLabel.Text = "Viso Stažas k.d.:"
        '
        'TotalWorkPeriodInYearsLabel
        '
        TotalWorkPeriodInYearsLabel.AutoSize = True
        TotalWorkPeriodInYearsLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        TotalWorkPeriodInYearsLabel.Location = New System.Drawing.Point(490, 5)
        TotalWorkPeriodInYearsLabel.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        TotalWorkPeriodInYearsLabel.Name = "TotalWorkPeriodInYearsLabel"
        TotalWorkPeriodInYearsLabel.Size = New System.Drawing.Size(117, 13)
        TotalWorkPeriodInYearsLabel.TabIndex = 10
        TotalWorkPeriodInYearsLabel.Text = "Viso Stažas metais:"
        '
        'TotalHolidayDaysGrantedLabel
        '
        TotalHolidayDaysGrantedLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        TotalHolidayDaysGrantedLabel.AutoSize = True
        TotalHolidayDaysGrantedLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        TotalHolidayDaysGrantedLabel.Location = New System.Drawing.Point(3, 125)
        TotalHolidayDaysGrantedLabel.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        TotalHolidayDaysGrantedLabel.Name = "TotalHolidayDaysGrantedLabel"
        TotalHolidayDaysGrantedLabel.Size = New System.Drawing.Size(138, 13)
        TotalHolidayDaysGrantedLabel.TabIndex = 8
        TotalHolidayDaysGrantedLabel.Text = "Suteikta Atostogų k.d.:"
        TotalHolidayDaysGrantedLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'TotalHolidayDaysCorrectionLabel
        '
        TotalHolidayDaysCorrectionLabel.AutoSize = True
        TotalHolidayDaysCorrectionLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        TotalHolidayDaysCorrectionLabel.Location = New System.Drawing.Point(196, 5)
        TotalHolidayDaysCorrectionLabel.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        TotalHolidayDaysCorrectionLabel.Name = "TotalHolidayDaysCorrectionLabel"
        TotalHolidayDaysCorrectionLabel.Size = New System.Drawing.Size(124, 13)
        TotalHolidayDaysCorrectionLabel.TabIndex = 9
        TotalHolidayDaysCorrectionLabel.Text = "Atostogų Korekcijos:"
        '
        'TotalCumulatedHolidayDaysLabel
        '
        TotalCumulatedHolidayDaysLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        TotalCumulatedHolidayDaysLabel.AutoSize = True
        TotalCumulatedHolidayDaysLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        TotalCumulatedHolidayDaysLabel.Location = New System.Drawing.Point(22, 155)
        TotalCumulatedHolidayDaysLabel.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        TotalCumulatedHolidayDaysLabel.Name = "TotalCumulatedHolidayDaysLabel"
        TotalCumulatedHolidayDaysLabel.Size = New System.Drawing.Size(119, 13)
        TotalCumulatedHolidayDaysLabel.TabIndex = 10
        TotalCumulatedHolidayDaysLabel.Text = "Viso Sukaupta k.d.:"
        TotalCumulatedHolidayDaysLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'TotalHolidayDaysUsedLabel
        '
        TotalHolidayDaysUsedLabel.AutoSize = True
        TotalHolidayDaysUsedLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        TotalHolidayDaysUsedLabel.Location = New System.Drawing.Point(180, 5)
        TotalHolidayDaysUsedLabel.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        TotalHolidayDaysUsedLabel.Name = "TotalHolidayDaysUsedLabel"
        TotalHolidayDaysUsedLabel.Size = New System.Drawing.Size(126, 13)
        TotalHolidayDaysUsedLabel.TabIndex = 11
        TotalHolidayDaysUsedLabel.Text = "Viso Panaudota k.d.:"
        '
        'TotalUnusedHolidayDaysLabel
        '
        TotalUnusedHolidayDaysLabel.AutoSize = True
        TotalUnusedHolidayDaysLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        TotalUnusedHolidayDaysLabel.Location = New System.Drawing.Point(489, 5)
        TotalUnusedHolidayDaysLabel.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        TotalUnusedHolidayDaysLabel.Name = "TotalUnusedHolidayDaysLabel"
        TotalUnusedHolidayDaysLabel.Size = New System.Drawing.Size(130, 13)
        TotalUnusedHolidayDaysLabel.TabIndex = 12
        TotalUnusedHolidayDaysLabel.Text = "Viso Nepanaudų k.d.:"
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
        Me.Panel1.Size = New System.Drawing.Size(943, 58)
        Me.Panel1.TabIndex = 4
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
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(889, 58)
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
        Me.TableLayoutPanel13.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 21.0!))
        Me.TableLayoutPanel13.Controls.Add(Me.ForCompensationCheckBox, 0, 0)
        Me.TableLayoutPanel13.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel13.Location = New System.Drawing.Point(0, 29)
        Me.TableLayoutPanel13.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel13.Name = "TableLayoutPanel13"
        Me.TableLayoutPanel13.RowCount = 1
        Me.TableLayoutPanel13.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel13.Size = New System.Drawing.Size(889, 29)
        Me.TableLayoutPanel13.TabIndex = 3
        '
        'ForCompensationCheckBox
        '
        Me.ForCompensationCheckBox.AutoSize = True
        Me.ForCompensationCheckBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.ForCompensationCheckBox.Location = New System.Drawing.Point(3, 5)
        Me.ForCompensationCheckBox.Margin = New System.Windows.Forms.Padding(3, 5, 3, 3)
        Me.ForCompensationCheckBox.Name = "ForCompensationCheckBox"
        Me.ForCompensationCheckBox.Size = New System.Drawing.Size(197, 17)
        Me.ForCompensationCheckBox.TabIndex = 5
        Me.ForCompensationCheckBox.Text = "Kompensacijos paskaičiavimui"
        Me.ForCompensationCheckBox.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.AutoSize = True
        Me.TableLayoutPanel1.ColumnCount = 10
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.RefreshLabourContractsButton, 7, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.LabourContractComboBox, 8, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.WorkerAccGridComboBox, 4, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.DateDateTimePicker, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 6, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label3, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 3, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 1
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(889, 29)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'RefreshLabourContractsButton
        '
        Me.RefreshLabourContractsButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RefreshLabourContractsButton.Image = Global.AccDataBindingsWinForms.My.Resources.Resources.Button_Reload_icon_16p
        Me.RefreshLabourContractsButton.Location = New System.Drawing.Point(725, 3)
        Me.RefreshLabourContractsButton.Margin = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Me.RefreshLabourContractsButton.Name = "RefreshLabourContractsButton"
        Me.RefreshLabourContractsButton.Size = New System.Drawing.Size(24, 22)
        Me.RefreshLabourContractsButton.TabIndex = 22
        Me.RefreshLabourContractsButton.UseVisualStyleBackColor = True
        '
        'LabourContractComboBox
        '
        Me.LabourContractComboBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.LabourContractComboBox.FormattingEnabled = True
        Me.LabourContractComboBox.Location = New System.Drawing.Point(752, 3)
        Me.LabourContractComboBox.Name = "LabourContractComboBox"
        Me.LabourContractComboBox.Size = New System.Drawing.Size(109, 21)
        Me.LabourContractComboBox.TabIndex = 1
        '
        'WorkerAccGridComboBox
        '
        Me.WorkerAccGridComboBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.WorkerAccGridComboBox.EmptyValueString = ""
        Me.WorkerAccGridComboBox.FilterString = ""
        Me.WorkerAccGridComboBox.FormattingEnabled = True
        Me.WorkerAccGridComboBox.InstantBinding = True
        Me.WorkerAccGridComboBox.Location = New System.Drawing.Point(324, 3)
        Me.WorkerAccGridComboBox.Name = "WorkerAccGridComboBox"
        Me.WorkerAccGridComboBox.Size = New System.Drawing.Size(282, 21)
        Me.WorkerAccGridComboBox.TabIndex = 1
        '
        'DateDateTimePicker
        '
        Me.DateDateTimePicker.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DateDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateDateTimePicker.Location = New System.Drawing.Point(47, 3)
        Me.DateDateTimePicker.Name = "DateDateTimePicker"
        Me.DateDateTimePicker.Size = New System.Drawing.Size(166, 20)
        Me.DateDateTimePicker.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.Label2.Location = New System.Drawing.Point(632, 5)
        Me.Label2.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(90, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Darbo sutartis:"
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
        Me.Label1.Location = New System.Drawing.Point(239, 5)
        Me.Label1.Margin = New System.Windows.Forms.Padding(3, 5, 3, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(79, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Darbuotojas:"
        '
        'RefreshButton
        '
        Me.RefreshButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RefreshButton.Image = Global.AccDataBindingsWinForms.My.Resources.Resources.Button_Reload_icon_24p
        Me.RefreshButton.Location = New System.Drawing.Point(899, 12)
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
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel9, 1, 4)
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel8, 1, 3)
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel5, 1, 5)
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel7, 1, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel6, 1, 1)
        Me.TableLayoutPanel2.Controls.Add(DateLabel, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(TotalCumulatedHolidayDaysLabel, 0, 5)
        Me.TableLayoutPanel2.Controls.Add(ContractTerminationDateLabel, 0, 2)
        Me.TableLayoutPanel2.Controls.Add(HolidayRateLabel, 0, 3)
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel4, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(PersonNameLabel, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(TotalHolidayDaysGrantedLabel, 0, 4)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 67)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 7
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30.0!))
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(943, 188)
        Me.TableLayoutPanel2.TabIndex = 5
        '
        'TableLayoutPanel9
        '
        Me.TableLayoutPanel9.ColumnCount = 8
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90.0!))
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel9.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel9.Controls.Add(Me.TotalHolidayDaysGrantedTextBox, 0, 0)
        Me.TableLayoutPanel9.Controls.Add(TotalHolidayDaysCorrectionLabel, 2, 0)
        Me.TableLayoutPanel9.Controls.Add(Me.TotalHolidayDaysCorrectionAccTextBox, 3, 0)
        Me.TableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel9.Location = New System.Drawing.Point(144, 120)
        Me.TableLayoutPanel9.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel9.Name = "TableLayoutPanel9"
        Me.TableLayoutPanel9.RowCount = 1
        Me.TableLayoutPanel9.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel9.Size = New System.Drawing.Size(799, 30)
        Me.TableLayoutPanel9.TabIndex = 8
        '
        'TotalHolidayDaysGrantedTextBox
        '
        Me.TotalHolidayDaysGrantedTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.WorkerHolidayInfoBindingSource, "TotalHolidayDaysGranted", True))
        Me.TotalHolidayDaysGrantedTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TotalHolidayDaysGrantedTextBox.Location = New System.Drawing.Point(3, 3)
        Me.TotalHolidayDaysGrantedTextBox.Name = "TotalHolidayDaysGrantedTextBox"
        Me.TotalHolidayDaysGrantedTextBox.ReadOnly = True
        Me.TotalHolidayDaysGrantedTextBox.Size = New System.Drawing.Size(167, 20)
        Me.TotalHolidayDaysGrantedTextBox.TabIndex = 9
        Me.TotalHolidayDaysGrantedTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'WorkerHolidayInfoBindingSource
        '
        Me.WorkerHolidayInfoBindingSource.DataSource = GetType(ApskaitaObjects.ActiveReports.WorkerHolidayInfo)
        '
        'TotalHolidayDaysCorrectionAccTextBox
        '
        Me.TotalHolidayDaysCorrectionAccTextBox.DataBindings.Add(New System.Windows.Forms.Binding("DecimalValue", Me.WorkerHolidayInfoBindingSource, "TotalHolidayDaysCorrection", True))
        Me.TotalHolidayDaysCorrectionAccTextBox.DecimalLength = 4
        Me.TotalHolidayDaysCorrectionAccTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TotalHolidayDaysCorrectionAccTextBox.KeepBackColorWhenReadOnly = False
        Me.TotalHolidayDaysCorrectionAccTextBox.Location = New System.Drawing.Point(326, 3)
        Me.TotalHolidayDaysCorrectionAccTextBox.Name = "TotalHolidayDaysCorrectionAccTextBox"
        Me.TotalHolidayDaysCorrectionAccTextBox.ReadOnly = True
        Me.TotalHolidayDaysCorrectionAccTextBox.Size = New System.Drawing.Size(167, 20)
        Me.TotalHolidayDaysCorrectionAccTextBox.TabIndex = 10
        Me.TotalHolidayDaysCorrectionAccTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
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
        Me.TableLayoutPanel8.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel8.Controls.Add(TotalWorkPeriodInYearsLabel, 5, 0)
        Me.TableLayoutPanel8.Controls.Add(Me.HolidayRateTextBox, 0, 0)
        Me.TableLayoutPanel8.Controls.Add(Me.TotalWorkPeriodInYearsAccTextBox, 6, 0)
        Me.TableLayoutPanel8.Controls.Add(TotalWorkPeriodInDaysLabel, 2, 0)
        Me.TableLayoutPanel8.Controls.Add(Me.TotalWorkPeriodInDaysTextBox, 3, 0)
        Me.TableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel8.Location = New System.Drawing.Point(144, 90)
        Me.TableLayoutPanel8.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel8.Name = "TableLayoutPanel8"
        Me.TableLayoutPanel8.RowCount = 1
        Me.TableLayoutPanel8.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel8.Size = New System.Drawing.Size(799, 30)
        Me.TableLayoutPanel8.TabIndex = 8
        '
        'HolidayRateTextBox
        '
        Me.HolidayRateTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.WorkerHolidayInfoBindingSource, "HolidayRate", True))
        Me.HolidayRateTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HolidayRateTextBox.Location = New System.Drawing.Point(3, 3)
        Me.HolidayRateTextBox.Name = "HolidayRateTextBox"
        Me.HolidayRateTextBox.ReadOnly = True
        Me.HolidayRateTextBox.Size = New System.Drawing.Size(163, 20)
        Me.HolidayRateTextBox.TabIndex = 9
        Me.HolidayRateTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TotalWorkPeriodInYearsAccTextBox
        '
        Me.TotalWorkPeriodInYearsAccTextBox.DataBindings.Add(New System.Windows.Forms.Binding("DecimalValue", Me.WorkerHolidayInfoBindingSource, "TotalWorkPeriodInYears", True))
        Me.TotalWorkPeriodInYearsAccTextBox.DecimalLength = 4
        Me.TotalWorkPeriodInYearsAccTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TotalWorkPeriodInYearsAccTextBox.KeepBackColorWhenReadOnly = False
        Me.TotalWorkPeriodInYearsAccTextBox.Location = New System.Drawing.Point(613, 3)
        Me.TotalWorkPeriodInYearsAccTextBox.Name = "TotalWorkPeriodInYearsAccTextBox"
        Me.TotalWorkPeriodInYearsAccTextBox.ReadOnly = True
        Me.TotalWorkPeriodInYearsAccTextBox.Size = New System.Drawing.Size(163, 20)
        Me.TotalWorkPeriodInYearsAccTextBox.TabIndex = 11
        Me.TotalWorkPeriodInYearsAccTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TotalWorkPeriodInDaysTextBox
        '
        Me.TotalWorkPeriodInDaysTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.WorkerHolidayInfoBindingSource, "TotalWorkPeriodInDays", True))
        Me.TotalWorkPeriodInDaysTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TotalWorkPeriodInDaysTextBox.Location = New System.Drawing.Point(301, 3)
        Me.TotalWorkPeriodInDaysTextBox.Name = "TotalWorkPeriodInDaysTextBox"
        Me.TotalWorkPeriodInDaysTextBox.ReadOnly = True
        Me.TotalWorkPeriodInDaysTextBox.Size = New System.Drawing.Size(163, 20)
        Me.TotalWorkPeriodInDaysTextBox.TabIndex = 10
        Me.TotalWorkPeriodInDaysTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
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
        Me.TableLayoutPanel5.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel5.Controls.Add(Me.TotalUnusedHolidayDaysAccTextBox, 6, 0)
        Me.TableLayoutPanel5.Controls.Add(TotalUnusedHolidayDaysLabel, 5, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.TotalCumulatedHolidayDaysAccTextBox, 0, 0)
        Me.TableLayoutPanel5.Controls.Add(TotalHolidayDaysUsedLabel, 2, 0)
        Me.TableLayoutPanel5.Controls.Add(Me.TotalHolidayDaysUsedAccTextBox, 3, 0)
        Me.TableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel5.Location = New System.Drawing.Point(144, 150)
        Me.TableLayoutPanel5.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel5.Name = "TableLayoutPanel5"
        Me.TableLayoutPanel5.RowCount = 1
        Me.TableLayoutPanel5.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel5.Size = New System.Drawing.Size(799, 30)
        Me.TableLayoutPanel5.TabIndex = 7
        '
        'TotalUnusedHolidayDaysAccTextBox
        '
        Me.TotalUnusedHolidayDaysAccTextBox.DataBindings.Add(New System.Windows.Forms.Binding("DecimalValue", Me.WorkerHolidayInfoBindingSource, "TotalUnusedHolidayDays", True))
        Me.TotalUnusedHolidayDaysAccTextBox.DecimalLength = 4
        Me.TotalUnusedHolidayDaysAccTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TotalUnusedHolidayDaysAccTextBox.KeepBackColorWhenReadOnly = False
        Me.TotalUnusedHolidayDaysAccTextBox.Location = New System.Drawing.Point(625, 3)
        Me.TotalUnusedHolidayDaysAccTextBox.Name = "TotalUnusedHolidayDaysAccTextBox"
        Me.TotalUnusedHolidayDaysAccTextBox.ReadOnly = True
        Me.TotalUnusedHolidayDaysAccTextBox.Size = New System.Drawing.Size(151, 20)
        Me.TotalUnusedHolidayDaysAccTextBox.TabIndex = 13
        Me.TotalUnusedHolidayDaysAccTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TotalCumulatedHolidayDaysAccTextBox
        '
        Me.TotalCumulatedHolidayDaysAccTextBox.DataBindings.Add(New System.Windows.Forms.Binding("DecimalValue", Me.WorkerHolidayInfoBindingSource, "TotalCumulatedHolidayDays", True))
        Me.TotalCumulatedHolidayDaysAccTextBox.DecimalLength = 4
        Me.TotalCumulatedHolidayDaysAccTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TotalCumulatedHolidayDaysAccTextBox.KeepBackColorWhenReadOnly = False
        Me.TotalCumulatedHolidayDaysAccTextBox.Location = New System.Drawing.Point(3, 3)
        Me.TotalCumulatedHolidayDaysAccTextBox.Name = "TotalCumulatedHolidayDaysAccTextBox"
        Me.TotalCumulatedHolidayDaysAccTextBox.ReadOnly = True
        Me.TotalCumulatedHolidayDaysAccTextBox.Size = New System.Drawing.Size(151, 20)
        Me.TotalCumulatedHolidayDaysAccTextBox.TabIndex = 11
        Me.TotalCumulatedHolidayDaysAccTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TotalHolidayDaysUsedAccTextBox
        '
        Me.TotalHolidayDaysUsedAccTextBox.DataBindings.Add(New System.Windows.Forms.Binding("DecimalValue", Me.WorkerHolidayInfoBindingSource, "TotalHolidayDaysUsed", True))
        Me.TotalHolidayDaysUsedAccTextBox.DecimalLength = 4
        Me.TotalHolidayDaysUsedAccTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TotalHolidayDaysUsedAccTextBox.KeepBackColorWhenReadOnly = False
        Me.TotalHolidayDaysUsedAccTextBox.Location = New System.Drawing.Point(312, 3)
        Me.TotalHolidayDaysUsedAccTextBox.Name = "TotalHolidayDaysUsedAccTextBox"
        Me.TotalHolidayDaysUsedAccTextBox.ReadOnly = True
        Me.TotalHolidayDaysUsedAccTextBox.Size = New System.Drawing.Size(151, 20)
        Me.TotalHolidayDaysUsedAccTextBox.TabIndex = 12
        Me.TotalHolidayDaysUsedAccTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TableLayoutPanel7
        '
        Me.TableLayoutPanel7.ColumnCount = 7
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel7.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 21.0!))
        Me.TableLayoutPanel7.Controls.Add(Me.TotalHolidayDaysCompensatedAccTextBox, 3, 0)
        Me.TableLayoutPanel7.Controls.Add(Me.ContractTerminationDateTextBox, 0, 0)
        Me.TableLayoutPanel7.Controls.Add(Me.CompensationIsGrantedCheckBox, 2, 0)
        Me.TableLayoutPanel7.Controls.Add(Me.IsForCompensationCheckBox, 5, 0)
        Me.TableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel7.Location = New System.Drawing.Point(144, 60)
        Me.TableLayoutPanel7.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel7.Name = "TableLayoutPanel7"
        Me.TableLayoutPanel7.RowCount = 1
        Me.TableLayoutPanel7.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel7.Size = New System.Drawing.Size(799, 30)
        Me.TableLayoutPanel7.TabIndex = 8
        '
        'TotalHolidayDaysCompensatedAccTextBox
        '
        Me.TotalHolidayDaysCompensatedAccTextBox.DataBindings.Add(New System.Windows.Forms.Binding("DecimalValue", Me.WorkerHolidayInfoBindingSource, "TotalHolidayDaysCompensated", True))
        Me.TotalHolidayDaysCompensatedAccTextBox.DecimalLength = 4
        Me.TotalHolidayDaysCompensatedAccTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TotalHolidayDaysCompensatedAccTextBox.KeepBackColorWhenReadOnly = False
        Me.TotalHolidayDaysCompensatedAccTextBox.Location = New System.Drawing.Point(403, 3)
        Me.TotalHolidayDaysCompensatedAccTextBox.Name = "TotalHolidayDaysCompensatedAccTextBox"
        Me.TotalHolidayDaysCompensatedAccTextBox.ReadOnly = True
        Me.TotalHolidayDaysCompensatedAccTextBox.Size = New System.Drawing.Size(163, 20)
        Me.TotalHolidayDaysCompensatedAccTextBox.TabIndex = 9
        Me.TotalHolidayDaysCompensatedAccTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'ContractTerminationDateTextBox
        '
        Me.ContractTerminationDateTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.WorkerHolidayInfoBindingSource, "ContractTerminationDate", True))
        Me.ContractTerminationDateTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ContractTerminationDateTextBox.Location = New System.Drawing.Point(3, 3)
        Me.ContractTerminationDateTextBox.Name = "ContractTerminationDateTextBox"
        Me.ContractTerminationDateTextBox.ReadOnly = True
        Me.ContractTerminationDateTextBox.Size = New System.Drawing.Size(163, 20)
        Me.ContractTerminationDateTextBox.TabIndex = 10
        Me.ContractTerminationDateTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'CompensationIsGrantedCheckBox
        '
        Me.CompensationIsGrantedCheckBox.AutoSize = True
        Me.CompensationIsGrantedCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("CheckState", Me.WorkerHolidayInfoBindingSource, "CompensationIsGranted", True))
        Me.CompensationIsGrantedCheckBox.Enabled = False
        Me.CompensationIsGrantedCheckBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.CompensationIsGrantedCheckBox.Location = New System.Drawing.Point(192, 5)
        Me.CompensationIsGrantedCheckBox.Margin = New System.Windows.Forms.Padding(3, 5, 3, 3)
        Me.CompensationIsGrantedCheckBox.Name = "CompensationIsGrantedCheckBox"
        Me.CompensationIsGrantedCheckBox.Size = New System.Drawing.Size(205, 17)
        Me.CompensationIsGrantedCheckBox.TabIndex = 9
        Me.CompensationIsGrantedCheckBox.Text = "Kompensacija išmokėta už k.d.:"
        Me.CompensationIsGrantedCheckBox.UseVisualStyleBackColor = True
        '
        'IsForCompensationCheckBox
        '
        Me.IsForCompensationCheckBox.AutoSize = True
        Me.IsForCompensationCheckBox.DataBindings.Add(New System.Windows.Forms.Binding("CheckState", Me.WorkerHolidayInfoBindingSource, "IsForCompensation", True))
        Me.IsForCompensationCheckBox.Enabled = False
        Me.IsForCompensationCheckBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.IsForCompensationCheckBox.Location = New System.Drawing.Point(592, 5)
        Me.IsForCompensationCheckBox.Margin = New System.Windows.Forms.Padding(3, 5, 3, 3)
        Me.IsForCompensationCheckBox.Name = "IsForCompensationCheckBox"
        Me.IsForCompensationCheckBox.Size = New System.Drawing.Size(183, 17)
        Me.IsForCompensationCheckBox.TabIndex = 11
        Me.IsForCompensationCheckBox.Text = "Paskaičiuota kompensacijai"
        Me.IsForCompensationCheckBox.UseVisualStyleBackColor = True
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
        Me.TableLayoutPanel6.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 21.0!))
        Me.TableLayoutPanel6.Controls.Add(PositionLabel, 5, 0)
        Me.TableLayoutPanel6.Controls.Add(Me.PersonNameTextBox, 0, 0)
        Me.TableLayoutPanel6.Controls.Add(PersonCodeLabel, 2, 0)
        Me.TableLayoutPanel6.Controls.Add(Me.PersonCodeTextBox, 3, 0)
        Me.TableLayoutPanel6.Controls.Add(Me.PositionTextBox, 6, 0)
        Me.TableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel6.Location = New System.Drawing.Point(144, 30)
        Me.TableLayoutPanel6.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel6.Name = "TableLayoutPanel6"
        Me.TableLayoutPanel6.RowCount = 1
        Me.TableLayoutPanel6.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel6.Size = New System.Drawing.Size(799, 30)
        Me.TableLayoutPanel6.TabIndex = 8
        '
        'PersonNameTextBox
        '
        Me.PersonNameTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.WorkerHolidayInfoBindingSource, "PersonName", True))
        Me.PersonNameTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PersonNameTextBox.Location = New System.Drawing.Point(3, 3)
        Me.PersonNameTextBox.Name = "PersonNameTextBox"
        Me.PersonNameTextBox.ReadOnly = True
        Me.PersonNameTextBox.Size = New System.Drawing.Size(185, 20)
        Me.PersonNameTextBox.TabIndex = 9
        Me.PersonNameTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'PersonCodeTextBox
        '
        Me.PersonCodeTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.WorkerHolidayInfoBindingSource, "PersonCode", True))
        Me.PersonCodeTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PersonCodeTextBox.Location = New System.Drawing.Point(313, 3)
        Me.PersonCodeTextBox.Name = "PersonCodeTextBox"
        Me.PersonCodeTextBox.ReadOnly = True
        Me.PersonCodeTextBox.Size = New System.Drawing.Size(185, 20)
        Me.PersonCodeTextBox.TabIndex = 10
        Me.PersonCodeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'PositionTextBox
        '
        Me.PositionTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.WorkerHolidayInfoBindingSource, "Position", True))
        Me.PositionTextBox.Location = New System.Drawing.Point(590, 3)
        Me.PositionTextBox.Name = "PositionTextBox"
        Me.PositionTextBox.ReadOnly = True
        Me.PositionTextBox.Size = New System.Drawing.Size(149, 20)
        Me.PositionTextBox.TabIndex = 11
        Me.PositionTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TableLayoutPanel4
        '
        Me.TableLayoutPanel4.ColumnCount = 11
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.0!))
        Me.TableLayoutPanel4.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel4.Controls.Add(Me.ContractNumberTextBox, 9, 0)
        Me.TableLayoutPanel4.Controls.Add(ContractNumberLabel, 8, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.DateDateTimePicker1, 0, 0)
        Me.TableLayoutPanel4.Controls.Add(ContractDateLabel, 2, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.ContractSerialTextBox, 6, 0)
        Me.TableLayoutPanel4.Controls.Add(ContractSerialLabel, 5, 0)
        Me.TableLayoutPanel4.Controls.Add(Me.ContractDateDateTimePicker, 3, 0)
        Me.TableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel4.Location = New System.Drawing.Point(144, 0)
        Me.TableLayoutPanel4.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel4.Name = "TableLayoutPanel4"
        Me.TableLayoutPanel4.RowCount = 1
        Me.TableLayoutPanel4.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel4.Size = New System.Drawing.Size(799, 30)
        Me.TableLayoutPanel4.TabIndex = 6
        '
        'ContractNumberTextBox
        '
        Me.ContractNumberTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.WorkerHolidayInfoBindingSource, "ContractNumber", True))
        Me.ContractNumberTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ContractNumberTextBox.Location = New System.Drawing.Point(646, 3)
        Me.ContractNumberTextBox.Name = "ContractNumberTextBox"
        Me.ContractNumberTextBox.ReadOnly = True
        Me.ContractNumberTextBox.Size = New System.Drawing.Size(128, 20)
        Me.ContractNumberTextBox.TabIndex = 12
        Me.ContractNumberTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'DateDateTimePicker1
        '
        Me.DateDateTimePicker1.DataBindings.Add(New System.Windows.Forms.Binding("Value", Me.WorkerHolidayInfoBindingSource, "Date", True))
        Me.DateDateTimePicker1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DateDateTimePicker1.Enabled = False
        Me.DateDateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateDateTimePicker1.Location = New System.Drawing.Point(3, 3)
        Me.DateDateTimePicker1.Name = "DateDateTimePicker1"
        Me.DateDateTimePicker1.Size = New System.Drawing.Size(128, 20)
        Me.DateDateTimePicker1.TabIndex = 9
        '
        'ContractSerialTextBox
        '
        Me.ContractSerialTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.WorkerHolidayInfoBindingSource, "ContractSerial", True))
        Me.ContractSerialTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ContractSerialTextBox.Location = New System.Drawing.Point(458, 3)
        Me.ContractSerialTextBox.Name = "ContractSerialTextBox"
        Me.ContractSerialTextBox.ReadOnly = True
        Me.ContractSerialTextBox.Size = New System.Drawing.Size(128, 20)
        Me.ContractSerialTextBox.TabIndex = 11
        Me.ContractSerialTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'ContractDateDateTimePicker
        '
        Me.ContractDateDateTimePicker.DataBindings.Add(New System.Windows.Forms.Binding("Value", Me.WorkerHolidayInfoBindingSource, "ContractDate", True))
        Me.ContractDateDateTimePicker.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ContractDateDateTimePicker.Enabled = False
        Me.ContractDateDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.ContractDateDateTimePicker.Location = New System.Drawing.Point(255, 3)
        Me.ContractDateDateTimePicker.Name = "ContractDateDateTimePicker"
        Me.ContractDateDateTimePicker.Size = New System.Drawing.Size(128, 20)
        Me.ContractDateDateTimePicker.TabIndex = 10
        '
        'TableLayoutPanel10
        '
        Me.TableLayoutPanel10.ColumnCount = 1
        Me.TableLayoutPanel10.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel10.Controls.Add(Me.Panel1, 0, 0)
        Me.TableLayoutPanel10.Controls.Add(Me.TableLayoutPanel2, 0, 1)
        Me.TableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Top
        Me.TableLayoutPanel10.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel10.Name = "TableLayoutPanel10"
        Me.TableLayoutPanel10.RowCount = 2
        Me.TableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel10.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel10.Size = New System.Drawing.Size(949, 258)
        Me.TableLayoutPanel10.TabIndex = 6
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 258)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.AutoScroll = True
        Me.SplitContainer1.Panel1.Controls.Add(Me.HolidayCalculatedListDataListView)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.AutoScroll = True
        Me.SplitContainer1.Panel2.Controls.Add(Me.HolidaySpentListDataListView)
        Me.SplitContainer1.Size = New System.Drawing.Size(949, 248)
        Me.SplitContainer1.SplitterDistance = 405
        Me.SplitContainer1.TabIndex = 7
        '
        'HolidayCalculatedListDataListView
        '
        Me.HolidayCalculatedListDataListView.AllColumns.Add(Me.OlvColumn1)
        Me.HolidayCalculatedListDataListView.AllColumns.Add(Me.OlvColumn2)
        Me.HolidayCalculatedListDataListView.AllColumns.Add(Me.OlvColumn3)
        Me.HolidayCalculatedListDataListView.AllColumns.Add(Me.OlvColumn4)
        Me.HolidayCalculatedListDataListView.AllColumns.Add(Me.OlvColumn5)
        Me.HolidayCalculatedListDataListView.AllColumns.Add(Me.OlvColumn6)
        Me.HolidayCalculatedListDataListView.AllColumns.Add(Me.OlvColumn7)
        Me.HolidayCalculatedListDataListView.AllowColumnReorder = True
        Me.HolidayCalculatedListDataListView.AutoGenerateColumns = False
        Me.HolidayCalculatedListDataListView.CausesValidation = False
        Me.HolidayCalculatedListDataListView.CellEditUseWholeCell = False
        Me.HolidayCalculatedListDataListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.OlvColumn1, Me.OlvColumn2, Me.OlvColumn3, Me.OlvColumn4, Me.OlvColumn5, Me.OlvColumn6, Me.OlvColumn7})
        Me.HolidayCalculatedListDataListView.Cursor = System.Windows.Forms.Cursors.Default
        Me.HolidayCalculatedListDataListView.DataSource = Me.HolidayCalculatedListBindingSource
        Me.HolidayCalculatedListDataListView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HolidayCalculatedListDataListView.FullRowSelect = True
        Me.HolidayCalculatedListDataListView.HasCollapsibleGroups = False
        Me.HolidayCalculatedListDataListView.HeaderWordWrap = True
        Me.HolidayCalculatedListDataListView.HideSelection = False
        Me.HolidayCalculatedListDataListView.HighlightBackgroundColor = System.Drawing.Color.PaleGreen
        Me.HolidayCalculatedListDataListView.HighlightForegroundColor = System.Drawing.Color.Black
        Me.HolidayCalculatedListDataListView.IncludeColumnHeadersInCopy = True
        Me.HolidayCalculatedListDataListView.Location = New System.Drawing.Point(0, 0)
        Me.HolidayCalculatedListDataListView.Name = "HolidayCalculatedListDataListView"
        Me.HolidayCalculatedListDataListView.RenderNonEditableCheckboxesAsDisabled = True
        Me.HolidayCalculatedListDataListView.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.Submenu
        Me.HolidayCalculatedListDataListView.SelectedBackColor = System.Drawing.Color.PaleGreen
        Me.HolidayCalculatedListDataListView.SelectedForeColor = System.Drawing.Color.Black
        Me.HolidayCalculatedListDataListView.ShowCommandMenuOnRightClick = True
        Me.HolidayCalculatedListDataListView.ShowGroups = False
        Me.HolidayCalculatedListDataListView.ShowImagesOnSubItems = True
        Me.HolidayCalculatedListDataListView.ShowItemCountOnGroups = True
        Me.HolidayCalculatedListDataListView.ShowItemToolTips = True
        Me.HolidayCalculatedListDataListView.Size = New System.Drawing.Size(405, 248)
        Me.HolidayCalculatedListDataListView.TabIndex = 4
        Me.HolidayCalculatedListDataListView.UnfocusedSelectedBackColor = System.Drawing.Color.PaleGreen
        Me.HolidayCalculatedListDataListView.UnfocusedSelectedForeColor = System.Drawing.Color.Black
        Me.HolidayCalculatedListDataListView.UseCellFormatEvents = True
        Me.HolidayCalculatedListDataListView.UseCompatibleStateImageBehavior = False
        Me.HolidayCalculatedListDataListView.UseFilterIndicator = True
        Me.HolidayCalculatedListDataListView.UseFiltering = True
        Me.HolidayCalculatedListDataListView.UseHotItem = True
        Me.HolidayCalculatedListDataListView.UseNotifyPropertyChanged = True
        Me.HolidayCalculatedListDataListView.View = System.Windows.Forms.View.Details
        '
        'OlvColumn1
        '
        Me.OlvColumn1.AspectName = "DateBegin"
        Me.OlvColumn1.AspectToStringFormat = "{0:d}"
        Me.OlvColumn1.CellEditUseWholeCell = True
        Me.OlvColumn1.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn1.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn1.IsEditable = False
        Me.OlvColumn1.Text = "Nuo"
        Me.OlvColumn1.ToolTipText = ""
        Me.OlvColumn1.Width = 80
        '
        'OlvColumn2
        '
        Me.OlvColumn2.AspectName = "DateEnd"
        Me.OlvColumn2.AspectToStringFormat = "{0:d}"
        Me.OlvColumn2.CellEditUseWholeCell = True
        Me.OlvColumn2.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn2.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn2.IsEditable = False
        Me.OlvColumn2.Text = "Iki"
        Me.OlvColumn2.ToolTipText = ""
        Me.OlvColumn2.Width = 77
        '
        'OlvColumn3
        '
        Me.OlvColumn3.AspectName = "LengthDays"
        Me.OlvColumn3.CellEditUseWholeCell = True
        Me.OlvColumn3.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn3.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn3.IsEditable = False
        Me.OlvColumn3.Text = "Trukmė k.d."
        Me.OlvColumn3.ToolTipText = ""
        Me.OlvColumn3.Width = 58
        '
        'OlvColumn4
        '
        Me.OlvColumn4.AspectName = "LengthYears"
        Me.OlvColumn4.AspectToStringFormat = "{0:##,0.0000}"
        Me.OlvColumn4.CellEditUseWholeCell = True
        Me.OlvColumn4.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn4.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn4.IsEditable = False
        Me.OlvColumn4.Text = "Trukmė metais"
        Me.OlvColumn4.ToolTipText = ""
        Me.OlvColumn4.Width = 55
        '
        'OlvColumn5
        '
        Me.OlvColumn5.AspectName = "HolidayRate"
        Me.OlvColumn5.CellEditUseWholeCell = True
        Me.OlvColumn5.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn5.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn5.IsEditable = False
        Me.OlvColumn5.Text = "Atsotogų Norma"
        Me.OlvColumn5.ToolTipText = ""
        Me.OlvColumn5.Width = 67
        '
        'OlvColumn6
        '
        Me.OlvColumn6.AspectName = "CumulatedHolidayDaysPerPeriod"
        Me.OlvColumn6.AspectToStringFormat = "{0:##,0.0000}"
        Me.OlvColumn6.CellEditUseWholeCell = True
        Me.OlvColumn6.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn6.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn6.IsEditable = False
        Me.OlvColumn6.Text = "Sukaupta Atostogų k.d."
        Me.OlvColumn6.ToolTipText = ""
        Me.OlvColumn6.Width = 70
        '
        'OlvColumn7
        '
        Me.OlvColumn7.AspectName = "StatusDescription"
        Me.OlvColumn7.CellEditUseWholeCell = True
        Me.OlvColumn7.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn7.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn7.IsEditable = False
        Me.OlvColumn7.Text = "Aprašymas"
        Me.OlvColumn7.ToolTipText = ""
        Me.OlvColumn7.Width = 215
        '
        'HolidayCalculatedListBindingSource
        '
        Me.HolidayCalculatedListBindingSource.DataMember = "HolidayCalculatedList"
        Me.HolidayCalculatedListBindingSource.DataSource = Me.WorkerHolidayInfoBindingSource
        '
        'HolidaySpentListDataListView
        '
        Me.HolidaySpentListDataListView.AllColumns.Add(Me.OlvColumn8)
        Me.HolidaySpentListDataListView.AllColumns.Add(Me.OlvColumn9)
        Me.HolidaySpentListDataListView.AllColumns.Add(Me.OlvColumn10)
        Me.HolidaySpentListDataListView.AllColumns.Add(Me.OlvColumn11)
        Me.HolidaySpentListDataListView.AllColumns.Add(Me.OlvColumn12)
        Me.HolidaySpentListDataListView.AllColumns.Add(Me.OlvColumn13)
        Me.HolidaySpentListDataListView.AllColumns.Add(Me.OlvColumn14)
        Me.HolidaySpentListDataListView.AllColumns.Add(Me.OlvColumn15)
        Me.HolidaySpentListDataListView.AllColumns.Add(Me.OlvColumn16)
        Me.HolidaySpentListDataListView.AllowColumnReorder = True
        Me.HolidaySpentListDataListView.AutoGenerateColumns = False
        Me.HolidaySpentListDataListView.CausesValidation = False
        Me.HolidaySpentListDataListView.CellEditUseWholeCell = False
        Me.HolidaySpentListDataListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.OlvColumn8, Me.OlvColumn11, Me.OlvColumn12, Me.OlvColumn13, Me.OlvColumn14, Me.OlvColumn15, Me.OlvColumn16})
        Me.HolidaySpentListDataListView.Cursor = System.Windows.Forms.Cursors.Default
        Me.HolidaySpentListDataListView.DataSource = Me.HolidaySpentListBindingSource
        Me.HolidaySpentListDataListView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.HolidaySpentListDataListView.FullRowSelect = True
        Me.HolidaySpentListDataListView.HasCollapsibleGroups = False
        Me.HolidaySpentListDataListView.HeaderWordWrap = True
        Me.HolidaySpentListDataListView.HideSelection = False
        Me.HolidaySpentListDataListView.HighlightBackgroundColor = System.Drawing.Color.PaleGreen
        Me.HolidaySpentListDataListView.HighlightForegroundColor = System.Drawing.Color.Black
        Me.HolidaySpentListDataListView.IncludeColumnHeadersInCopy = True
        Me.HolidaySpentListDataListView.Location = New System.Drawing.Point(0, 0)
        Me.HolidaySpentListDataListView.Name = "HolidaySpentListDataListView"
        Me.HolidaySpentListDataListView.RenderNonEditableCheckboxesAsDisabled = True
        Me.HolidaySpentListDataListView.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.Submenu
        Me.HolidaySpentListDataListView.SelectedBackColor = System.Drawing.Color.PaleGreen
        Me.HolidaySpentListDataListView.SelectedForeColor = System.Drawing.Color.Black
        Me.HolidaySpentListDataListView.ShowCommandMenuOnRightClick = True
        Me.HolidaySpentListDataListView.ShowGroups = False
        Me.HolidaySpentListDataListView.ShowImagesOnSubItems = True
        Me.HolidaySpentListDataListView.ShowItemCountOnGroups = True
        Me.HolidaySpentListDataListView.ShowItemToolTips = True
        Me.HolidaySpentListDataListView.Size = New System.Drawing.Size(540, 248)
        Me.HolidaySpentListDataListView.TabIndex = 4
        Me.HolidaySpentListDataListView.UnfocusedSelectedBackColor = System.Drawing.Color.PaleGreen
        Me.HolidaySpentListDataListView.UnfocusedSelectedForeColor = System.Drawing.Color.Black
        Me.HolidaySpentListDataListView.UseCellFormatEvents = True
        Me.HolidaySpentListDataListView.UseCompatibleStateImageBehavior = False
        Me.HolidaySpentListDataListView.UseFilterIndicator = True
        Me.HolidaySpentListDataListView.UseFiltering = True
        Me.HolidaySpentListDataListView.UseHotItem = True
        Me.HolidaySpentListDataListView.UseNotifyPropertyChanged = True
        Me.HolidaySpentListDataListView.View = System.Windows.Forms.View.Details
        '
        'OlvColumn8
        '
        Me.OlvColumn8.AspectName = "TypeHumanReadable"
        Me.OlvColumn8.CellEditUseWholeCell = True
        Me.OlvColumn8.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn8.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn8.IsEditable = False
        Me.OlvColumn8.Text = "Tipas"
        Me.OlvColumn8.ToolTipText = ""
        Me.OlvColumn8.Width = 100
        '
        'OlvColumn9
        '
        Me.OlvColumn9.AspectName = "DocumentID"
        Me.OlvColumn9.CellEditUseWholeCell = True
        Me.OlvColumn9.DisplayIndex = 1
        Me.OlvColumn9.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn9.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn9.IsEditable = False
        Me.OlvColumn9.IsVisible = False
        Me.OlvColumn9.Text = "Dok. ID"
        Me.OlvColumn9.ToolTipText = ""
        Me.OlvColumn9.Width = 100
        '
        'OlvColumn10
        '
        Me.OlvColumn10.AspectName = "DocumentNumber"
        Me.OlvColumn10.CellEditUseWholeCell = True
        Me.OlvColumn10.DisplayIndex = 2
        Me.OlvColumn10.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn10.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn10.IsEditable = False
        Me.OlvColumn10.IsVisible = False
        Me.OlvColumn10.Text = "Dok. Nr."
        Me.OlvColumn10.ToolTipText = ""
        Me.OlvColumn10.Width = 100
        '
        'OlvColumn11
        '
        Me.OlvColumn11.AspectName = "DocumentDate"
        Me.OlvColumn11.AspectToStringFormat = "{0:d}"
        Me.OlvColumn11.CellEditUseWholeCell = True
        Me.OlvColumn11.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn11.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn11.IsEditable = False
        Me.OlvColumn11.Text = "Dok. Data"
        Me.OlvColumn11.ToolTipText = ""
        Me.OlvColumn11.Width = 86
        '
        'OlvColumn12
        '
        Me.OlvColumn12.AspectName = "Spent"
        Me.OlvColumn12.CellEditUseWholeCell = True
        Me.OlvColumn12.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn12.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn12.IsEditable = False
        Me.OlvColumn12.Text = "Panaudota"
        Me.OlvColumn12.ToolTipText = ""
        Me.OlvColumn12.Width = 66
        '
        'OlvColumn13
        '
        Me.OlvColumn13.AspectName = "Compensated"
        Me.OlvColumn13.AspectToStringFormat = "{0:##,0.0000}"
        Me.OlvColumn13.CellEditUseWholeCell = True
        Me.OlvColumn13.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn13.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn13.IsEditable = False
        Me.OlvColumn13.Text = "Kompensuota"
        Me.OlvColumn13.ToolTipText = ""
        Me.OlvColumn13.Width = 78
        '
        'OlvColumn14
        '
        Me.OlvColumn14.AspectName = "Correction"
        Me.OlvColumn14.AspectToStringFormat = "{0:##,0.0000}"
        Me.OlvColumn14.CellEditUseWholeCell = True
        Me.OlvColumn14.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn14.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn14.IsEditable = False
        Me.OlvColumn14.Text = "Koreguota"
        Me.OlvColumn14.ToolTipText = ""
        Me.OlvColumn14.Width = 61
        '
        'OlvColumn15
        '
        Me.OlvColumn15.AspectName = "Total"
        Me.OlvColumn15.AspectToStringFormat = "{0:##,0.0000}"
        Me.OlvColumn15.CellEditUseWholeCell = True
        Me.OlvColumn15.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn15.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn15.IsEditable = False
        Me.OlvColumn15.Text = "Viso"
        Me.OlvColumn15.ToolTipText = ""
        '
        'OlvColumn16
        '
        Me.OlvColumn16.AspectName = "DocumentContent"
        Me.OlvColumn16.CellEditUseWholeCell = True
        Me.OlvColumn16.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn16.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn16.IsEditable = False
        Me.OlvColumn16.Text = "Aprašymas"
        Me.OlvColumn16.ToolTipText = ""
        Me.OlvColumn16.Width = 100
        '
        'HolidaySpentListBindingSource
        '
        Me.HolidaySpentListBindingSource.DataMember = "HolidaySpentList"
        Me.HolidaySpentListBindingSource.DataSource = Me.WorkerHolidayInfoBindingSource
        '
        'ProgressFiller1
        '
        Me.ProgressFiller1.Location = New System.Drawing.Point(169, 285)
        Me.ProgressFiller1.Name = "ProgressFiller1"
        Me.ProgressFiller1.Size = New System.Drawing.Size(227, 86)
        Me.ProgressFiller1.TabIndex = 8
        Me.ProgressFiller1.Visible = False
        '
        'ProgressFiller2
        '
        Me.ProgressFiller2.Location = New System.Drawing.Point(427, 288)
        Me.ProgressFiller2.Name = "ProgressFiller2"
        Me.ProgressFiller2.Size = New System.Drawing.Size(243, 83)
        Me.ProgressFiller2.TabIndex = 9
        Me.ProgressFiller2.Visible = False
        '
        'F_WorkerHolidayInfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(949, 506)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.TableLayoutPanel10)
        Me.Controls.Add(Me.ProgressFiller2)
        Me.Controls.Add(Me.ProgressFiller1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "F_WorkerHolidayInfo"
        Me.ShowInTaskbar = False
        Me.Text = "Pažyma apie sukauptas atostogas"
        Me.Panel1.ResumeLayout(False)
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
        Me.TableLayoutPanel13.ResumeLayout(False)
        Me.TableLayoutPanel13.PerformLayout()
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        Me.TableLayoutPanel9.ResumeLayout(False)
        Me.TableLayoutPanel9.PerformLayout()
        CType(Me.WorkerHolidayInfoBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel8.ResumeLayout(False)
        Me.TableLayoutPanel8.PerformLayout()
        Me.TableLayoutPanel5.ResumeLayout(False)
        Me.TableLayoutPanel5.PerformLayout()
        Me.TableLayoutPanel7.ResumeLayout(False)
        Me.TableLayoutPanel7.PerformLayout()
        Me.TableLayoutPanel6.ResumeLayout(False)
        Me.TableLayoutPanel6.PerformLayout()
        Me.TableLayoutPanel4.ResumeLayout(False)
        Me.TableLayoutPanel4.PerformLayout()
        Me.TableLayoutPanel10.ResumeLayout(False)
        Me.TableLayoutPanel10.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.HolidayCalculatedListDataListView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.HolidayCalculatedListBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.HolidaySpentListDataListView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.HolidaySpentListBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel13 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ForCompensationCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents RefreshLabourContractsButton As System.Windows.Forms.Button
    Friend WithEvents LabourContractComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents WorkerAccGridComboBox As AccControlsWinForms.AccListComboBox
    Friend WithEvents DateDateTimePicker As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents RefreshButton As System.Windows.Forms.Button
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel4 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TableLayoutPanel5 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ContractNumberTextBox As System.Windows.Forms.TextBox
    Friend WithEvents WorkerHolidayInfoBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents DateDateTimePicker1 As System.Windows.Forms.DateTimePicker
    Friend WithEvents ContractSerialTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ContractDateDateTimePicker As System.Windows.Forms.DateTimePicker
    Friend WithEvents TableLayoutPanel6 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents PositionTextBox As System.Windows.Forms.TextBox
    Friend WithEvents PersonNameTextBox As System.Windows.Forms.TextBox
    Friend WithEvents PersonCodeTextBox As System.Windows.Forms.TextBox
    Friend WithEvents TableLayoutPanel7 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents ContractTerminationDateTextBox As System.Windows.Forms.TextBox
    Friend WithEvents IsForCompensationCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents CompensationIsGrantedCheckBox As System.Windows.Forms.CheckBox
    Friend WithEvents TableLayoutPanel8 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents HolidayRateTextBox As System.Windows.Forms.TextBox
    Friend WithEvents TotalWorkPeriodInYearsAccTextBox As AccControlsWinForms.AccTextBox
    Friend WithEvents TotalWorkPeriodInDaysTextBox As System.Windows.Forms.TextBox
    Friend WithEvents TotalHolidayDaysCompensatedAccTextBox As AccControlsWinForms.AccTextBox
    Friend WithEvents TableLayoutPanel9 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TotalHolidayDaysGrantedTextBox As System.Windows.Forms.TextBox
    Friend WithEvents TotalHolidayDaysCorrectionAccTextBox As AccControlsWinForms.AccTextBox
    Friend WithEvents TotalUnusedHolidayDaysAccTextBox As AccControlsWinForms.AccTextBox
    Friend WithEvents TotalCumulatedHolidayDaysAccTextBox As AccControlsWinForms.AccTextBox
    Friend WithEvents TotalHolidayDaysUsedAccTextBox As AccControlsWinForms.AccTextBox
    Friend WithEvents TableLayoutPanel10 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents HolidayCalculatedListBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents HolidaySpentListBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents HolidayCalculatedListDataListView As BrightIdeasSoftware.DataListView
    Friend WithEvents HolidaySpentListDataListView As BrightIdeasSoftware.DataListView
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
    Friend WithEvents ProgressFiller1 As AccControlsWinForms.ProgressFiller
    Friend WithEvents ProgressFiller2 As AccControlsWinForms.ProgressFiller
End Class
