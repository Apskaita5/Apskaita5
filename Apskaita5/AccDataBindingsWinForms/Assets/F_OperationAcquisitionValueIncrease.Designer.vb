<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Friend Class F_OperationAcquisitionValueIncrease
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
        Dim DateLabel As System.Windows.Forms.Label
        Dim IDLabel As System.Windows.Forms.Label
        Dim InsertDateLabel As System.Windows.Forms.Label
        Dim UpdateDateLabel As System.Windows.Forms.Label
        Dim ValueIncreaseLabel As System.Windows.Forms.Label
        Dim ValueIncreasePerUnitLabel As System.Windows.Forms.Label
        Dim JournalEntryIDLabel As System.Windows.Forms.Label
        Dim JournalEntryDocumentTypeLabel As System.Windows.Forms.Label
        Dim JournalEntryAmountLabel As System.Windows.Forms.Label
        Dim JournalEntryDocumentNumberLabel As System.Windows.Forms.Label
        Dim JournalEntryContentLabel As System.Windows.Forms.Label
        Dim JournalEntryPersonLabel As System.Windows.Forms.Label
        Dim JournalEntryBookEntriesLabel As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(F_OperationAcquisitionValueIncrease))
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.nCancelButton = New System.Windows.Forms.Button
        Me.ApplyButton = New System.Windows.Forms.Button
        Me.nOkButton = New System.Windows.Forms.Button
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.AttachNewJournalEntryButton = New System.Windows.Forms.Button
        Me.RefreshJournalEntryInfoListButton = New System.Windows.Forms.Button
        Me.JournalEntryInfoListComboBox = New System.Windows.Forms.ComboBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel
        Me.JournalEntryContentTextBox = New System.Windows.Forms.TextBox
        Me.OperationAcquisitionValueIncreaseBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.JournalEntryBookEntriesTextBox = New System.Windows.Forms.TextBox
        Me.JournalEntryDocumentNumberTextBox = New System.Windows.Forms.TextBox
        Me.JournalEntryPersonTextBox = New System.Windows.Forms.TextBox
        Me.ViewJournalEntryButton = New System.Windows.Forms.Button
        Me.JournalEntryAmountAccTextBox = New AccControlsWinForms.AccTextBox
        Me.JournalEntryIDTextBox = New System.Windows.Forms.TextBox
        Me.JournalEntryDocumentTypeTextBox = New System.Windows.Forms.TextBox
        Me.ValueIncreasePerUnitAccTextBox = New AccControlsWinForms.AccTextBox
        Me.ContentTextBox = New System.Windows.Forms.TextBox
        Me.IDTextBox = New System.Windows.Forms.TextBox
        Me.ValueIncreaseAccTextBox = New AccControlsWinForms.AccTextBox
        Me.InsertDateTextBox = New System.Windows.Forms.TextBox
        Me.UpdateDateTextBox = New System.Windows.Forms.TextBox
        Me.DateDateTimePicker = New System.Windows.Forms.DateTimePicker
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.BackgroundInfoPanel1 = New AccDataBindingsWinForms.BackgroundInfoPanel
        Me.ProgressFiller1 = New AccControlsWinForms.ProgressFiller
        Me.ProgressFiller2 = New AccControlsWinForms.ProgressFiller
        Me.ErrorWarnInfoProvider1 = New AccControlsWinForms.ErrorWarnInfoProvider(Me.components)
        ContentLabel = New System.Windows.Forms.Label
        DateLabel = New System.Windows.Forms.Label
        IDLabel = New System.Windows.Forms.Label
        InsertDateLabel = New System.Windows.Forms.Label
        UpdateDateLabel = New System.Windows.Forms.Label
        ValueIncreaseLabel = New System.Windows.Forms.Label
        ValueIncreasePerUnitLabel = New System.Windows.Forms.Label
        JournalEntryIDLabel = New System.Windows.Forms.Label
        JournalEntryDocumentTypeLabel = New System.Windows.Forms.Label
        JournalEntryAmountLabel = New System.Windows.Forms.Label
        JournalEntryDocumentNumberLabel = New System.Windows.Forms.Label
        JournalEntryContentLabel = New System.Windows.Forms.Label
        JournalEntryPersonLabel = New System.Windows.Forms.Label
        JournalEntryBookEntriesLabel = New System.Windows.Forms.Label
        Me.Panel2.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.OperationAcquisitionValueIncreaseBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.ErrorWarnInfoProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ContentLabel
        '
        ContentLabel.AutoSize = True
        ContentLabel.Dock = System.Windows.Forms.DockStyle.Fill
        ContentLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        ContentLabel.Location = New System.Drawing.Point(3, 52)
        ContentLabel.Name = "ContentLabel"
        ContentLabel.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        ContentLabel.Size = New System.Drawing.Size(71, 26)
        ContentLabel.TabIndex = 6
        ContentLabel.Text = "Aprašymas:"
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
        DateLabel.Size = New System.Drawing.Size(71, 26)
        DateLabel.TabIndex = 7
        DateLabel.Text = "Data:"
        DateLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'IDLabel
        '
        IDLabel.AutoSize = True
        IDLabel.Dock = System.Windows.Forms.DockStyle.Fill
        IDLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        IDLabel.Location = New System.Drawing.Point(3, 0)
        IDLabel.Name = "IDLabel"
        IDLabel.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        IDLabel.Size = New System.Drawing.Size(71, 26)
        IDLabel.TabIndex = 8
        IDLabel.Text = "ID:"
        IDLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'InsertDateLabel
        '
        InsertDateLabel.AutoSize = True
        InsertDateLabel.Dock = System.Windows.Forms.DockStyle.Fill
        InsertDateLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        InsertDateLabel.Location = New System.Drawing.Point(242, 0)
        InsertDateLabel.Name = "InsertDateLabel"
        InsertDateLabel.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        InsertDateLabel.Size = New System.Drawing.Size(106, 26)
        InsertDateLabel.TabIndex = 9
        InsertDateLabel.Text = "Įtraukta:"
        InsertDateLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'UpdateDateLabel
        '
        UpdateDateLabel.AutoSize = True
        UpdateDateLabel.Dock = System.Windows.Forms.DockStyle.Fill
        UpdateDateLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        UpdateDateLabel.Location = New System.Drawing.Point(516, 0)
        UpdateDateLabel.Name = "UpdateDateLabel"
        UpdateDateLabel.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        UpdateDateLabel.Size = New System.Drawing.Size(105, 26)
        UpdateDateLabel.TabIndex = 11
        UpdateDateLabel.Text = "Pakeista:"
        UpdateDateLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'ValueIncreaseLabel
        '
        ValueIncreaseLabel.AutoSize = True
        ValueIncreaseLabel.Dock = System.Windows.Forms.DockStyle.Fill
        ValueIncreaseLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        ValueIncreaseLabel.Location = New System.Drawing.Point(242, 26)
        ValueIncreaseLabel.Name = "ValueIncreaseLabel"
        ValueIncreaseLabel.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        ValueIncreaseLabel.Size = New System.Drawing.Size(106, 26)
        ValueIncreaseLabel.TabIndex = 13
        ValueIncreaseLabel.Text = "Padidėjimas Viso:"
        ValueIncreaseLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'ValueIncreasePerUnitLabel
        '
        ValueIncreasePerUnitLabel.AutoSize = True
        ValueIncreasePerUnitLabel.Dock = System.Windows.Forms.DockStyle.Fill
        ValueIncreasePerUnitLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        ValueIncreasePerUnitLabel.Location = New System.Drawing.Point(516, 26)
        ValueIncreasePerUnitLabel.Name = "ValueIncreasePerUnitLabel"
        ValueIncreasePerUnitLabel.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        ValueIncreasePerUnitLabel.Size = New System.Drawing.Size(105, 26)
        ValueIncreasePerUnitLabel.TabIndex = 15
        ValueIncreasePerUnitLabel.Text = "Padidėjimas Vnt.:"
        ValueIncreasePerUnitLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'JournalEntryIDLabel
        '
        JournalEntryIDLabel.AutoSize = True
        JournalEntryIDLabel.Dock = System.Windows.Forms.DockStyle.Fill
        JournalEntryIDLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        JournalEntryIDLabel.Location = New System.Drawing.Point(3, 0)
        JournalEntryIDLabel.Name = "JournalEntryIDLabel"
        JournalEntryIDLabel.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        JournalEntryIDLabel.Size = New System.Drawing.Size(86, 26)
        JournalEntryIDLabel.TabIndex = 7
        JournalEntryIDLabel.Text = "ID:"
        JournalEntryIDLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'JournalEntryDocumentTypeLabel
        '
        JournalEntryDocumentTypeLabel.AutoSize = True
        JournalEntryDocumentTypeLabel.Dock = System.Windows.Forms.DockStyle.Fill
        JournalEntryDocumentTypeLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        JournalEntryDocumentTypeLabel.Location = New System.Drawing.Point(253, 0)
        JournalEntryDocumentTypeLabel.Name = "JournalEntryDocumentTypeLabel"
        JournalEntryDocumentTypeLabel.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        JournalEntryDocumentTypeLabel.Size = New System.Drawing.Size(73, 26)
        JournalEntryDocumentTypeLabel.TabIndex = 8
        JournalEntryDocumentTypeLabel.Text = "Dok. Tipas:"
        JournalEntryDocumentTypeLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'JournalEntryAmountLabel
        '
        JournalEntryAmountLabel.AutoSize = True
        JournalEntryAmountLabel.Dock = System.Windows.Forms.DockStyle.Fill
        JournalEntryAmountLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        JournalEntryAmountLabel.Location = New System.Drawing.Point(3, 26)
        JournalEntryAmountLabel.Name = "JournalEntryAmountLabel"
        JournalEntryAmountLabel.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        JournalEntryAmountLabel.Size = New System.Drawing.Size(86, 26)
        JournalEntryAmountLabel.TabIndex = 9
        JournalEntryAmountLabel.Text = "Suma:"
        JournalEntryAmountLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'JournalEntryDocumentNumberLabel
        '
        JournalEntryDocumentNumberLabel.AutoSize = True
        JournalEntryDocumentNumberLabel.Dock = System.Windows.Forms.DockStyle.Fill
        JournalEntryDocumentNumberLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        JournalEntryDocumentNumberLabel.Location = New System.Drawing.Point(253, 26)
        JournalEntryDocumentNumberLabel.Name = "JournalEntryDocumentNumberLabel"
        JournalEntryDocumentNumberLabel.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        JournalEntryDocumentNumberLabel.Size = New System.Drawing.Size(73, 26)
        JournalEntryDocumentNumberLabel.TabIndex = 10
        JournalEntryDocumentNumberLabel.Text = "Dok. Nr.:"
        JournalEntryDocumentNumberLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'JournalEntryContentLabel
        '
        JournalEntryContentLabel.AutoSize = True
        JournalEntryContentLabel.Dock = System.Windows.Forms.DockStyle.Fill
        JournalEntryContentLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        JournalEntryContentLabel.Location = New System.Drawing.Point(3, 104)
        JournalEntryContentLabel.Name = "JournalEntryContentLabel"
        JournalEntryContentLabel.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        JournalEntryContentLabel.Size = New System.Drawing.Size(86, 29)
        JournalEntryContentLabel.TabIndex = 7
        JournalEntryContentLabel.Text = "Turinys:"
        JournalEntryContentLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'JournalEntryPersonLabel
        '
        JournalEntryPersonLabel.AutoSize = True
        JournalEntryPersonLabel.Dock = System.Windows.Forms.DockStyle.Fill
        JournalEntryPersonLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        JournalEntryPersonLabel.Location = New System.Drawing.Point(3, 52)
        JournalEntryPersonLabel.Name = "JournalEntryPersonLabel"
        JournalEntryPersonLabel.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        JournalEntryPersonLabel.Size = New System.Drawing.Size(86, 26)
        JournalEntryPersonLabel.TabIndex = 8
        JournalEntryPersonLabel.Text = "Kontrahentas:"
        JournalEntryPersonLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'JournalEntryBookEntriesLabel
        '
        JournalEntryBookEntriesLabel.AutoSize = True
        JournalEntryBookEntriesLabel.Dock = System.Windows.Forms.DockStyle.Fill
        JournalEntryBookEntriesLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        JournalEntryBookEntriesLabel.Location = New System.Drawing.Point(3, 78)
        JournalEntryBookEntriesLabel.Name = "JournalEntryBookEntriesLabel"
        JournalEntryBookEntriesLabel.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        JournalEntryBookEntriesLabel.Size = New System.Drawing.Size(86, 26)
        JournalEntryBookEntriesLabel.TabIndex = 9
        JournalEntryBookEntriesLabel.Text = "Kontavimai:"
        JournalEntryBookEntriesLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Panel2
        '
        Me.Panel2.AutoSize = True
        Me.Panel2.Controls.Add(Me.nCancelButton)
        Me.Panel2.Controls.Add(Me.ApplyButton)
        Me.Panel2.Controls.Add(Me.nOkButton)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 373)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(0, 0, 0, 4)
        Me.Panel2.Size = New System.Drawing.Size(802, 42)
        Me.Panel2.TabIndex = 1
        '
        'nCancelButton
        '
        Me.nCancelButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.nCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.nCancelButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nCancelButton.Location = New System.Drawing.Point(715, 12)
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
        Me.ApplyButton.Location = New System.Drawing.Point(634, 12)
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
        Me.nOkButton.Location = New System.Drawing.Point(553, 12)
        Me.nOkButton.Name = "nOkButton"
        Me.nOkButton.Size = New System.Drawing.Size(75, 23)
        Me.nOkButton.TabIndex = 1
        Me.nOkButton.Text = "OK"
        Me.nOkButton.UseVisualStyleBackColor = True
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 9
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.GroupBox2, 0, 4)
        Me.TableLayoutPanel1.Controls.Add(Me.GroupBox1, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.ValueIncreasePerUnitAccTextBox, 7, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.ContentTextBox, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(ContentLabel, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(ValueIncreasePerUnitLabel, 6, 1)
        Me.TableLayoutPanel1.Controls.Add(IDLabel, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.IDTextBox, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.ValueIncreaseAccTextBox, 4, 1)
        Me.TableLayoutPanel1.Controls.Add(ValueIncreaseLabel, 3, 1)
        Me.TableLayoutPanel1.Controls.Add(InsertDateLabel, 3, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.InsertDateTextBox, 4, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.UpdateDateTextBox, 7, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.DateDateTimePicker, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(DateLabel, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(UpdateDateLabel, 6, 0)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(3, 3)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 6
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(788, 341)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.BackColor = System.Drawing.SystemColors.Info
        Me.TableLayoutPanel1.SetColumnSpan(Me.GroupBox2, 9)
        Me.GroupBox2.Controls.Add(Me.AttachNewJournalEntryButton)
        Me.GroupBox2.Controls.Add(Me.RefreshJournalEntryInfoListButton)
        Me.GroupBox2.Controls.Add(Me.JournalEntryInfoListComboBox)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox2.Location = New System.Drawing.Point(3, 239)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(782, 47)
        Me.GroupBox2.TabIndex = 3
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Pridėti Naują Susietą Dokumentą (Bendrojo Žurnalo Operaciją)"
        '
        'AttachNewJournalEntryButton
        '
        Me.AttachNewJournalEntryButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AttachNewJournalEntryButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AttachNewJournalEntryButton.Location = New System.Drawing.Point(704, 16)
        Me.AttachNewJournalEntryButton.Name = "AttachNewJournalEntryButton"
        Me.AttachNewJournalEntryButton.Size = New System.Drawing.Size(72, 24)
        Me.AttachNewJournalEntryButton.TabIndex = 2
        Me.AttachNewJournalEntryButton.Text = "Pridėti"
        Me.AttachNewJournalEntryButton.UseVisualStyleBackColor = True
        '
        'RefreshJournalEntryInfoListButton
        '
        Me.RefreshJournalEntryInfoListButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RefreshJournalEntryInfoListButton.Image = Global.AccDataBindingsWinForms.My.Resources.Resources.Button_Reload_icon_16p
        Me.RefreshJournalEntryInfoListButton.Location = New System.Drawing.Point(658, 16)
        Me.RefreshJournalEntryInfoListButton.Margin = New System.Windows.Forms.Padding(0)
        Me.RefreshJournalEntryInfoListButton.Name = "RefreshJournalEntryInfoListButton"
        Me.RefreshJournalEntryInfoListButton.Size = New System.Drawing.Size(24, 24)
        Me.RefreshJournalEntryInfoListButton.TabIndex = 1
        Me.RefreshJournalEntryInfoListButton.UseVisualStyleBackColor = True
        '
        'JournalEntryInfoListComboBox
        '
        Me.JournalEntryInfoListComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.JournalEntryInfoListComboBox.FormattingEnabled = True
        Me.JournalEntryInfoListComboBox.Location = New System.Drawing.Point(6, 19)
        Me.JournalEntryInfoListComboBox.Name = "JournalEntryInfoListComboBox"
        Me.JournalEntryInfoListComboBox.Size = New System.Drawing.Size(649, 21)
        Me.JournalEntryInfoListComboBox.TabIndex = 0
        '
        'GroupBox1
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.GroupBox1, 9)
        Me.GroupBox1.Controls.Add(Me.TableLayoutPanel2)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(3, 81)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(782, 152)
        Me.GroupBox1.TabIndex = 4
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Susietas Dokumentas (Bendrojo Žurnalo Operacija)"
        '
        'TableLayoutPanel2
        '
        Me.TableLayoutPanel2.ColumnCount = 9
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 25.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.JournalEntryContentTextBox, 1, 4)
        Me.TableLayoutPanel2.Controls.Add(JournalEntryContentLabel, 0, 4)
        Me.TableLayoutPanel2.Controls.Add(Me.JournalEntryBookEntriesTextBox, 1, 3)
        Me.TableLayoutPanel2.Controls.Add(JournalEntryBookEntriesLabel, 0, 3)
        Me.TableLayoutPanel2.Controls.Add(Me.JournalEntryDocumentNumberTextBox, 4, 1)
        Me.TableLayoutPanel2.Controls.Add(JournalEntryDocumentNumberLabel, 3, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.JournalEntryPersonTextBox, 1, 2)
        Me.TableLayoutPanel2.Controls.Add(JournalEntryPersonLabel, 0, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.ViewJournalEntryButton, 2, 0)
        Me.TableLayoutPanel2.Controls.Add(JournalEntryIDLabel, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.JournalEntryAmountAccTextBox, 1, 1)
        Me.TableLayoutPanel2.Controls.Add(JournalEntryAmountLabel, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.JournalEntryIDTextBox, 1, 0)
        Me.TableLayoutPanel2.Controls.Add(JournalEntryDocumentTypeLabel, 3, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.JournalEntryDocumentTypeTextBox, 4, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 16)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 5
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(776, 133)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'JournalEntryContentTextBox
        '
        Me.TableLayoutPanel2.SetColumnSpan(Me.JournalEntryContentTextBox, 7)
        Me.JournalEntryContentTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OperationAcquisitionValueIncreaseBindingSource, "JournalEntryContent", True))
        Me.JournalEntryContentTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.JournalEntryContentTextBox.Location = New System.Drawing.Point(95, 107)
        Me.JournalEntryContentTextBox.Name = "JournalEntryContentTextBox"
        Me.JournalEntryContentTextBox.ReadOnly = True
        Me.JournalEntryContentTextBox.Size = New System.Drawing.Size(653, 20)
        Me.JournalEntryContentTextBox.TabIndex = 8
        Me.JournalEntryContentTextBox.TabStop = False
        '
        'OperationAcquisitionValueIncreaseBindingSource
        '
        Me.OperationAcquisitionValueIncreaseBindingSource.DataSource = GetType(ApskaitaObjects.Assets.OperationAcquisitionValueIncrease)
        '
        'JournalEntryBookEntriesTextBox
        '
        Me.TableLayoutPanel2.SetColumnSpan(Me.JournalEntryBookEntriesTextBox, 7)
        Me.JournalEntryBookEntriesTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OperationAcquisitionValueIncreaseBindingSource, "JournalEntryBookEntries", True))
        Me.JournalEntryBookEntriesTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.JournalEntryBookEntriesTextBox.Location = New System.Drawing.Point(95, 81)
        Me.JournalEntryBookEntriesTextBox.Name = "JournalEntryBookEntriesTextBox"
        Me.JournalEntryBookEntriesTextBox.ReadOnly = True
        Me.JournalEntryBookEntriesTextBox.Size = New System.Drawing.Size(653, 20)
        Me.JournalEntryBookEntriesTextBox.TabIndex = 10
        Me.JournalEntryBookEntriesTextBox.TabStop = False
        '
        'JournalEntryDocumentNumberTextBox
        '
        Me.TableLayoutPanel2.SetColumnSpan(Me.JournalEntryDocumentNumberTextBox, 4)
        Me.JournalEntryDocumentNumberTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OperationAcquisitionValueIncreaseBindingSource, "JournalEntryDocumentNumber", True))
        Me.JournalEntryDocumentNumberTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.JournalEntryDocumentNumberTextBox.Location = New System.Drawing.Point(332, 29)
        Me.JournalEntryDocumentNumberTextBox.Name = "JournalEntryDocumentNumberTextBox"
        Me.JournalEntryDocumentNumberTextBox.ReadOnly = True
        Me.JournalEntryDocumentNumberTextBox.Size = New System.Drawing.Size(416, 20)
        Me.JournalEntryDocumentNumberTextBox.TabIndex = 11
        Me.JournalEntryDocumentNumberTextBox.TabStop = False
        Me.JournalEntryDocumentNumberTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'JournalEntryPersonTextBox
        '
        Me.TableLayoutPanel2.SetColumnSpan(Me.JournalEntryPersonTextBox, 7)
        Me.JournalEntryPersonTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OperationAcquisitionValueIncreaseBindingSource, "JournalEntryPerson", True))
        Me.JournalEntryPersonTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.JournalEntryPersonTextBox.Location = New System.Drawing.Point(95, 55)
        Me.JournalEntryPersonTextBox.Name = "JournalEntryPersonTextBox"
        Me.JournalEntryPersonTextBox.ReadOnly = True
        Me.JournalEntryPersonTextBox.Size = New System.Drawing.Size(653, 20)
        Me.JournalEntryPersonTextBox.TabIndex = 9
        Me.JournalEntryPersonTextBox.TabStop = False
        '
        'ViewJournalEntryButton
        '
        Me.ViewJournalEntryButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ViewJournalEntryButton.Image = Global.AccDataBindingsWinForms.My.Resources.Resources.lektuvelis_16
        Me.ViewJournalEntryButton.Location = New System.Drawing.Point(226, 0)
        Me.ViewJournalEntryButton.Margin = New System.Windows.Forms.Padding(0)
        Me.ViewJournalEntryButton.Name = "ViewJournalEntryButton"
        Me.ViewJournalEntryButton.Size = New System.Drawing.Size(24, 24)
        Me.ViewJournalEntryButton.TabIndex = 22
        Me.ViewJournalEntryButton.UseVisualStyleBackColor = True
        '
        'JournalEntryAmountAccTextBox
        '
        Me.JournalEntryAmountAccTextBox.DataBindings.Add(New System.Windows.Forms.Binding("DecimalValue", Me.OperationAcquisitionValueIncreaseBindingSource, "JournalEntryAmount", True))
        Me.JournalEntryAmountAccTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.JournalEntryAmountAccTextBox.KeepBackColorWhenReadOnly = False
        Me.JournalEntryAmountAccTextBox.Location = New System.Drawing.Point(95, 29)
        Me.JournalEntryAmountAccTextBox.Name = "JournalEntryAmountAccTextBox"
        Me.JournalEntryAmountAccTextBox.ReadOnly = True
        Me.JournalEntryAmountAccTextBox.Size = New System.Drawing.Size(128, 20)
        Me.JournalEntryAmountAccTextBox.TabIndex = 10
        Me.JournalEntryAmountAccTextBox.TabStop = False
        Me.JournalEntryAmountAccTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'JournalEntryIDTextBox
        '
        Me.JournalEntryIDTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OperationAcquisitionValueIncreaseBindingSource, "JournalEntryID", True))
        Me.JournalEntryIDTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.JournalEntryIDTextBox.Location = New System.Drawing.Point(95, 3)
        Me.JournalEntryIDTextBox.Name = "JournalEntryIDTextBox"
        Me.JournalEntryIDTextBox.ReadOnly = True
        Me.JournalEntryIDTextBox.Size = New System.Drawing.Size(128, 20)
        Me.JournalEntryIDTextBox.TabIndex = 8
        Me.JournalEntryIDTextBox.TabStop = False
        Me.JournalEntryIDTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'JournalEntryDocumentTypeTextBox
        '
        Me.TableLayoutPanel2.SetColumnSpan(Me.JournalEntryDocumentTypeTextBox, 4)
        Me.JournalEntryDocumentTypeTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OperationAcquisitionValueIncreaseBindingSource, "JournalEntryDocumentType", True))
        Me.JournalEntryDocumentTypeTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.JournalEntryDocumentTypeTextBox.Location = New System.Drawing.Point(332, 3)
        Me.JournalEntryDocumentTypeTextBox.Name = "JournalEntryDocumentTypeTextBox"
        Me.JournalEntryDocumentTypeTextBox.ReadOnly = True
        Me.JournalEntryDocumentTypeTextBox.Size = New System.Drawing.Size(416, 20)
        Me.JournalEntryDocumentTypeTextBox.TabIndex = 9
        Me.JournalEntryDocumentTypeTextBox.TabStop = False
        Me.JournalEntryDocumentTypeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'ValueIncreasePerUnitAccTextBox
        '
        Me.ValueIncreasePerUnitAccTextBox.DataBindings.Add(New System.Windows.Forms.Binding("DecimalValue", Me.OperationAcquisitionValueIncreaseBindingSource, "ValueIncreasePerUnit", True))
        Me.ValueIncreasePerUnitAccTextBox.DecimalLength = 4
        Me.ValueIncreasePerUnitAccTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ValueIncreasePerUnitAccTextBox.KeepBackColorWhenReadOnly = False
        Me.ValueIncreasePerUnitAccTextBox.Location = New System.Drawing.Point(627, 29)
        Me.ValueIncreasePerUnitAccTextBox.Name = "ValueIncreasePerUnitAccTextBox"
        Me.ValueIncreasePerUnitAccTextBox.NegativeValue = False
        Me.ValueIncreasePerUnitAccTextBox.ReadOnly = True
        Me.ValueIncreasePerUnitAccTextBox.Size = New System.Drawing.Size(136, 20)
        Me.ValueIncreasePerUnitAccTextBox.TabIndex = 16
        Me.ValueIncreasePerUnitAccTextBox.TabStop = False
        Me.ValueIncreasePerUnitAccTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'ContentTextBox
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.ContentTextBox, 7)
        Me.ContentTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OperationAcquisitionValueIncreaseBindingSource, "Content", True))
        Me.ContentTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ContentTextBox.Location = New System.Drawing.Point(80, 55)
        Me.ContentTextBox.MaxLength = 255
        Me.ContentTextBox.Name = "ContentTextBox"
        Me.ContentTextBox.Size = New System.Drawing.Size(683, 20)
        Me.ContentTextBox.TabIndex = 2
        '
        'IDTextBox
        '
        Me.IDTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OperationAcquisitionValueIncreaseBindingSource, "ID", True))
        Me.IDTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.IDTextBox.Location = New System.Drawing.Point(80, 3)
        Me.IDTextBox.Name = "IDTextBox"
        Me.IDTextBox.ReadOnly = True
        Me.IDTextBox.Size = New System.Drawing.Size(136, 20)
        Me.IDTextBox.TabIndex = 9
        Me.IDTextBox.TabStop = False
        Me.IDTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'ValueIncreaseAccTextBox
        '
        Me.ValueIncreaseAccTextBox.DataBindings.Add(New System.Windows.Forms.Binding("DecimalValue", Me.OperationAcquisitionValueIncreaseBindingSource, "ValueIncrease", True))
        Me.ValueIncreaseAccTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ValueIncreaseAccTextBox.KeepBackColorWhenReadOnly = False
        Me.ValueIncreaseAccTextBox.Location = New System.Drawing.Point(354, 29)
        Me.ValueIncreaseAccTextBox.Name = "ValueIncreaseAccTextBox"
        Me.ValueIncreaseAccTextBox.NegativeValue = False
        Me.ValueIncreaseAccTextBox.Size = New System.Drawing.Size(136, 20)
        Me.ValueIncreaseAccTextBox.TabIndex = 1
        Me.ValueIncreaseAccTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'InsertDateTextBox
        '
        Me.InsertDateTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OperationAcquisitionValueIncreaseBindingSource, "InsertDate", True))
        Me.InsertDateTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.InsertDateTextBox.Location = New System.Drawing.Point(354, 3)
        Me.InsertDateTextBox.Name = "InsertDateTextBox"
        Me.InsertDateTextBox.ReadOnly = True
        Me.InsertDateTextBox.Size = New System.Drawing.Size(136, 20)
        Me.InsertDateTextBox.TabIndex = 10
        Me.InsertDateTextBox.TabStop = False
        Me.InsertDateTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'UpdateDateTextBox
        '
        Me.UpdateDateTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OperationAcquisitionValueIncreaseBindingSource, "UpdateDate", True))
        Me.UpdateDateTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UpdateDateTextBox.Location = New System.Drawing.Point(627, 3)
        Me.UpdateDateTextBox.Name = "UpdateDateTextBox"
        Me.UpdateDateTextBox.ReadOnly = True
        Me.UpdateDateTextBox.Size = New System.Drawing.Size(136, 20)
        Me.UpdateDateTextBox.TabIndex = 12
        Me.UpdateDateTextBox.TabStop = False
        Me.UpdateDateTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'DateDateTimePicker
        '
        Me.DateDateTimePicker.DataBindings.Add(New System.Windows.Forms.Binding("Value", Me.OperationAcquisitionValueIncreaseBindingSource, "Date", True))
        Me.DateDateTimePicker.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DateDateTimePicker.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.DateDateTimePicker.Location = New System.Drawing.Point(80, 29)
        Me.DateDateTimePicker.Name = "DateDateTimePicker"
        Me.DateDateTimePicker.Size = New System.Drawing.Size(136, 20)
        Me.DateDateTimePicker.TabIndex = 0
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(802, 373)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.AutoScroll = True
        Me.TabPage1.Controls.Add(Me.TableLayoutPanel1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(794, 347)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Operacijos duomenys"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.BackgroundInfoPanel1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(794, 345)
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
        Me.BackgroundInfoPanel1.Size = New System.Drawing.Size(788, 339)
        Me.BackgroundInfoPanel1.TabIndex = 0
        '
        'ProgressFiller1
        '
        Me.ProgressFiller1.Location = New System.Drawing.Point(149, 22)
        Me.ProgressFiller1.Name = "ProgressFiller1"
        Me.ProgressFiller1.Size = New System.Drawing.Size(165, 59)
        Me.ProgressFiller1.TabIndex = 2
        Me.ProgressFiller1.Visible = False
        '
        'ProgressFiller2
        '
        Me.ProgressFiller2.Location = New System.Drawing.Point(331, 18)
        Me.ProgressFiller2.Name = "ProgressFiller2"
        Me.ProgressFiller2.Size = New System.Drawing.Size(176, 62)
        Me.ProgressFiller2.TabIndex = 3
        Me.ProgressFiller2.Visible = False
        '
        'ErrorWarnInfoProvider1
        '
        Me.ErrorWarnInfoProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.ErrorWarnInfoProvider1.BlinkStyleInformation = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.ErrorWarnInfoProvider1.BlinkStyleWarning = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.ErrorWarnInfoProvider1.ContainerControl = Me
        Me.ErrorWarnInfoProvider1.DataSource = Me.OperationAcquisitionValueIncreaseBindingSource
        '
        'F_OperationAcquisitionValueIncrease
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(802, 415)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.ProgressFiller2)
        Me.Controls.Add(Me.ProgressFiller1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "F_OperationAcquisitionValueIncrease"
        Me.ShowInTaskbar = False
        Me.Text = "Ilgalaiki turto įsigijimo savikainos padidinimo operacija"
        Me.Panel2.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        CType(Me.OperationAcquisitionValueIncreaseBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        CType(Me.ErrorWarnInfoProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents nCancelButton As System.Windows.Forms.Button
    Friend WithEvents ApplyButton As System.Windows.Forms.Button
    Friend WithEvents nOkButton As System.Windows.Forms.Button
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents ValueIncreasePerUnitAccTextBox As AccControlsWinForms.AccTextBox
    Friend WithEvents OperationAcquisitionValueIncreaseBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ContentTextBox As System.Windows.Forms.TextBox
    Friend WithEvents IDTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ValueIncreaseAccTextBox As AccControlsWinForms.AccTextBox
    Friend WithEvents InsertDateTextBox As System.Windows.Forms.TextBox
    Friend WithEvents UpdateDateTextBox As System.Windows.Forms.TextBox
    Friend WithEvents DateDateTimePicker As System.Windows.Forms.DateTimePicker
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents JournalEntryDocumentTypeTextBox As System.Windows.Forms.TextBox
    Friend WithEvents JournalEntryIDTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ViewJournalEntryButton As System.Windows.Forms.Button
    Friend WithEvents JournalEntryDocumentNumberTextBox As System.Windows.Forms.TextBox
    Friend WithEvents JournalEntryAmountAccTextBox As AccControlsWinForms.AccTextBox
    Friend WithEvents JournalEntryBookEntriesTextBox As System.Windows.Forms.TextBox
    Friend WithEvents JournalEntryPersonTextBox As System.Windows.Forms.TextBox
    Friend WithEvents JournalEntryContentTextBox As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents AttachNewJournalEntryButton As System.Windows.Forms.Button
    Friend WithEvents RefreshJournalEntryInfoListButton As System.Windows.Forms.Button
    Friend WithEvents JournalEntryInfoListComboBox As System.Windows.Forms.ComboBox
    Friend WithEvents BackgroundInfoPanel1 As AccDataBindingsWinForms.BackgroundInfoPanel
    Friend WithEvents ProgressFiller2 As AccControlsWinForms.ProgressFiller
    Friend WithEvents ProgressFiller1 As AccControlsWinForms.ProgressFiller
    Friend WithEvents ErrorWarnInfoProvider1 As AccControlsWinForms.ErrorWarnInfoProvider
End Class
