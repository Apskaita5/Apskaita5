<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Friend Class F_OperationValueChange
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
        Dim ValueChangePerUnitLabel As System.Windows.Forms.Label
        Dim ValueChangeTotalLabel As System.Windows.Forms.Label
        Dim JournalEntryIDLabel As System.Windows.Forms.Label
        Dim JournalEntryDocumentTypeLabel As System.Windows.Forms.Label
        Dim JournalEntryAmountLabel As System.Windows.Forms.Label
        Dim JournalEntryDocumentNumberLabel As System.Windows.Forms.Label
        Dim JournalEntryPersonLabel As System.Windows.Forms.Label
        Dim JournalEntryBookEntriesLabel As System.Windows.Forms.Label
        Dim JournalEntryContentLabel As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(F_OperationValueChange))
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.nCancelButton = New System.Windows.Forms.Button
        Me.ApplyButton = New System.Windows.Forms.Button
        Me.nOkButton = New System.Windows.Forms.Button
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.TableLayoutPanel2 = New System.Windows.Forms.TableLayoutPanel
        Me.JournalEntryContentTextBox = New System.Windows.Forms.TextBox
        Me.OperationValueChangeBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.JournalEntryDocumentNumberTextBox = New System.Windows.Forms.TextBox
        Me.JournalEntryBookEntriesTextBox = New System.Windows.Forms.TextBox
        Me.JournalEntryPersonTextBox = New System.Windows.Forms.TextBox
        Me.JournalEntryAmountAccTextBox = New AccControlsWinForms.AccTextBox
        Me.JournalEntryDocumentTypeTextBox = New System.Windows.Forms.TextBox
        Me.TableLayoutPanel3 = New System.Windows.Forms.TableLayoutPanel
        Me.ViewJournalEntryButton = New System.Windows.Forms.Button
        Me.CreateNewJournalEntryButton = New System.Windows.Forms.Button
        Me.AttachNewJournalEntryButton = New System.Windows.Forms.Button
        Me.JournalEntryIDTextBox = New System.Windows.Forms.TextBox
        Me.ValueChangePerUnitAccTextBox = New AccControlsWinForms.AccTextBox
        Me.ContentTextBox = New System.Windows.Forms.TextBox
        Me.ValueChangeTotalAccTextBox = New AccControlsWinForms.AccTextBox
        Me.IDTextBox = New System.Windows.Forms.TextBox
        Me.InsertDateTextBox = New System.Windows.Forms.TextBox
        Me.UpdateDateTextBox = New System.Windows.Forms.TextBox
        Me.DateAccDatePicker = New AccControlsWinForms.AccDatePicker
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
        ValueChangePerUnitLabel = New System.Windows.Forms.Label
        ValueChangeTotalLabel = New System.Windows.Forms.Label
        JournalEntryIDLabel = New System.Windows.Forms.Label
        JournalEntryDocumentTypeLabel = New System.Windows.Forms.Label
        JournalEntryAmountLabel = New System.Windows.Forms.Label
        JournalEntryDocumentNumberLabel = New System.Windows.Forms.Label
        JournalEntryPersonLabel = New System.Windows.Forms.Label
        JournalEntryBookEntriesLabel = New System.Windows.Forms.Label
        JournalEntryContentLabel = New System.Windows.Forms.Label
        Me.Panel2.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TableLayoutPanel2.SuspendLayout()
        CType(Me.OperationValueChangeBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TableLayoutPanel3.SuspendLayout()
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
        ContentLabel.TabIndex = 1
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
        DateLabel.TabIndex = 3
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
        IDLabel.TabIndex = 5
        IDLabel.Text = "ID:"
        IDLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'InsertDateLabel
        '
        InsertDateLabel.AutoSize = True
        InsertDateLabel.Dock = System.Windows.Forms.DockStyle.Fill
        InsertDateLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        InsertDateLabel.Location = New System.Drawing.Point(258, 0)
        InsertDateLabel.Name = "InsertDateLabel"
        InsertDateLabel.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        InsertDateLabel.Size = New System.Drawing.Size(80, 26)
        InsertDateLabel.TabIndex = 7
        InsertDateLabel.Text = "Įtraukta:"
        InsertDateLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'UpdateDateLabel
        '
        UpdateDateLabel.AutoSize = True
        UpdateDateLabel.Dock = System.Windows.Forms.DockStyle.Fill
        UpdateDateLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        UpdateDateLabel.Location = New System.Drawing.Point(522, 0)
        UpdateDateLabel.Name = "UpdateDateLabel"
        UpdateDateLabel.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        UpdateDateLabel.Size = New System.Drawing.Size(79, 26)
        UpdateDateLabel.TabIndex = 11
        UpdateDateLabel.Text = "Pakeista:"
        UpdateDateLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'ValueChangePerUnitLabel
        '
        ValueChangePerUnitLabel.AutoSize = True
        ValueChangePerUnitLabel.Dock = System.Windows.Forms.DockStyle.Fill
        ValueChangePerUnitLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        ValueChangePerUnitLabel.Location = New System.Drawing.Point(522, 26)
        ValueChangePerUnitLabel.Name = "ValueChangePerUnitLabel"
        ValueChangePerUnitLabel.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        ValueChangePerUnitLabel.Size = New System.Drawing.Size(79, 26)
        ValueChangePerUnitLabel.TabIndex = 13
        ValueChangePerUnitLabel.Text = "Pokytis Vnt.:"
        ValueChangePerUnitLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'ValueChangeTotalLabel
        '
        ValueChangeTotalLabel.AutoSize = True
        ValueChangeTotalLabel.Dock = System.Windows.Forms.DockStyle.Fill
        ValueChangeTotalLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        ValueChangeTotalLabel.Location = New System.Drawing.Point(258, 26)
        ValueChangeTotalLabel.Name = "ValueChangeTotalLabel"
        ValueChangeTotalLabel.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        ValueChangeTotalLabel.Size = New System.Drawing.Size(80, 26)
        ValueChangeTotalLabel.TabIndex = 15
        ValueChangeTotalLabel.Text = "Pokytis Viso:"
        ValueChangeTotalLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'JournalEntryIDLabel
        '
        JournalEntryIDLabel.AutoSize = True
        JournalEntryIDLabel.Dock = System.Windows.Forms.DockStyle.Fill
        JournalEntryIDLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        JournalEntryIDLabel.Location = New System.Drawing.Point(3, 0)
        JournalEntryIDLabel.Name = "JournalEntryIDLabel"
        JournalEntryIDLabel.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        JournalEntryIDLabel.Size = New System.Drawing.Size(86, 28)
        JournalEntryIDLabel.TabIndex = 2
        JournalEntryIDLabel.Text = "ID:"
        JournalEntryIDLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'JournalEntryDocumentTypeLabel
        '
        JournalEntryDocumentTypeLabel.AutoSize = True
        JournalEntryDocumentTypeLabel.Dock = System.Windows.Forms.DockStyle.Fill
        JournalEntryDocumentTypeLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        JournalEntryDocumentTypeLabel.Location = New System.Drawing.Point(252, 0)
        JournalEntryDocumentTypeLabel.Name = "JournalEntryDocumentTypeLabel"
        JournalEntryDocumentTypeLabel.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        JournalEntryDocumentTypeLabel.Size = New System.Drawing.Size(73, 28)
        JournalEntryDocumentTypeLabel.TabIndex = 4
        JournalEntryDocumentTypeLabel.Text = "Dok. Tipas:"
        JournalEntryDocumentTypeLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'JournalEntryAmountLabel
        '
        JournalEntryAmountLabel.AutoSize = True
        JournalEntryAmountLabel.Dock = System.Windows.Forms.DockStyle.Fill
        JournalEntryAmountLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        JournalEntryAmountLabel.Location = New System.Drawing.Point(3, 28)
        JournalEntryAmountLabel.Name = "JournalEntryAmountLabel"
        JournalEntryAmountLabel.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        JournalEntryAmountLabel.Size = New System.Drawing.Size(86, 26)
        JournalEntryAmountLabel.TabIndex = 6
        JournalEntryAmountLabel.Text = "Suma:"
        JournalEntryAmountLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'JournalEntryDocumentNumberLabel
        '
        JournalEntryDocumentNumberLabel.AutoSize = True
        JournalEntryDocumentNumberLabel.Dock = System.Windows.Forms.DockStyle.Fill
        JournalEntryDocumentNumberLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        JournalEntryDocumentNumberLabel.Location = New System.Drawing.Point(252, 28)
        JournalEntryDocumentNumberLabel.Name = "JournalEntryDocumentNumberLabel"
        JournalEntryDocumentNumberLabel.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        JournalEntryDocumentNumberLabel.Size = New System.Drawing.Size(73, 26)
        JournalEntryDocumentNumberLabel.TabIndex = 8
        JournalEntryDocumentNumberLabel.Text = "Dok. Nr.:"
        JournalEntryDocumentNumberLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'JournalEntryPersonLabel
        '
        JournalEntryPersonLabel.AutoSize = True
        JournalEntryPersonLabel.Dock = System.Windows.Forms.DockStyle.Fill
        JournalEntryPersonLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        JournalEntryPersonLabel.Location = New System.Drawing.Point(3, 54)
        JournalEntryPersonLabel.Name = "JournalEntryPersonLabel"
        JournalEntryPersonLabel.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        JournalEntryPersonLabel.Size = New System.Drawing.Size(86, 26)
        JournalEntryPersonLabel.TabIndex = 2
        JournalEntryPersonLabel.Text = "Kontrahentas:"
        JournalEntryPersonLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'JournalEntryBookEntriesLabel
        '
        JournalEntryBookEntriesLabel.AutoSize = True
        JournalEntryBookEntriesLabel.Dock = System.Windows.Forms.DockStyle.Fill
        JournalEntryBookEntriesLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        JournalEntryBookEntriesLabel.Location = New System.Drawing.Point(3, 80)
        JournalEntryBookEntriesLabel.Name = "JournalEntryBookEntriesLabel"
        JournalEntryBookEntriesLabel.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        JournalEntryBookEntriesLabel.Size = New System.Drawing.Size(86, 26)
        JournalEntryBookEntriesLabel.TabIndex = 4
        JournalEntryBookEntriesLabel.Text = "Kontavimai:"
        JournalEntryBookEntriesLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'JournalEntryContentLabel
        '
        JournalEntryContentLabel.AutoSize = True
        JournalEntryContentLabel.Dock = System.Windows.Forms.DockStyle.Fill
        JournalEntryContentLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        JournalEntryContentLabel.Location = New System.Drawing.Point(3, 106)
        JournalEntryContentLabel.Name = "JournalEntryContentLabel"
        JournalEntryContentLabel.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        JournalEntryContentLabel.Size = New System.Drawing.Size(86, 28)
        JournalEntryContentLabel.TabIndex = 6
        JournalEntryContentLabel.Text = "Turinys:"
        JournalEntryContentLabel.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Panel2
        '
        Me.Panel2.AutoSize = True
        Me.Panel2.Controls.Add(Me.nCancelButton)
        Me.Panel2.Controls.Add(Me.ApplyButton)
        Me.Panel2.Controls.Add(Me.nOkButton)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 372)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(0, 0, 0, 4)
        Me.Panel2.Size = New System.Drawing.Size(797, 42)
        Me.Panel2.TabIndex = 0
        '
        'nCancelButton
        '
        Me.nCancelButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.nCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.nCancelButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nCancelButton.Location = New System.Drawing.Point(710, 12)
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
        Me.ApplyButton.Location = New System.Drawing.Point(629, 12)
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
        Me.nOkButton.Location = New System.Drawing.Point(548, 12)
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
        Me.TabControl1.Size = New System.Drawing.Size(797, 372)
        Me.TabControl1.TabIndex = 0
        '
        'TabPage1
        '
        Me.TabPage1.AutoScroll = True
        Me.TabPage1.Controls.Add(Me.TableLayoutPanel1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(789, 346)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Operacijos duomenys"
        Me.TabPage1.UseVisualStyleBackColor = True
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
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 21.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.GroupBox1, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.ValueChangePerUnitAccTextBox, 7, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.ContentTextBox, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(ContentLabel, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(ValueChangePerUnitLabel, 6, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.ValueChangeTotalAccTextBox, 4, 1)
        Me.TableLayoutPanel1.Controls.Add(ValueChangeTotalLabel, 3, 1)
        Me.TableLayoutPanel1.Controls.Add(IDLabel, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.IDTextBox, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(InsertDateLabel, 3, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.InsertDateTextBox, 4, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.UpdateDateTextBox, 7, 0)
        Me.TableLayoutPanel1.Controls.Add(DateLabel, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(UpdateDateLabel, 6, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.DateAccDatePicker, 1, 1)
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
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(783, 340)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'GroupBox1
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.GroupBox1, 9)
        Me.GroupBox1.Controls.Add(Me.TableLayoutPanel2)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(3, 81)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(777, 153)
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
        Me.TableLayoutPanel2.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 24.0!))
        Me.TableLayoutPanel2.Controls.Add(Me.JournalEntryContentTextBox, 1, 4)
        Me.TableLayoutPanel2.Controls.Add(JournalEntryContentLabel, 0, 4)
        Me.TableLayoutPanel2.Controls.Add(Me.JournalEntryDocumentNumberTextBox, 4, 1)
        Me.TableLayoutPanel2.Controls.Add(JournalEntryDocumentNumberLabel, 3, 1)
        Me.TableLayoutPanel2.Controls.Add(Me.JournalEntryBookEntriesTextBox, 1, 3)
        Me.TableLayoutPanel2.Controls.Add(JournalEntryBookEntriesLabel, 0, 3)
        Me.TableLayoutPanel2.Controls.Add(JournalEntryIDLabel, 0, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.JournalEntryPersonTextBox, 1, 2)
        Me.TableLayoutPanel2.Controls.Add(JournalEntryPersonLabel, 0, 2)
        Me.TableLayoutPanel2.Controls.Add(Me.JournalEntryAmountAccTextBox, 1, 1)
        Me.TableLayoutPanel2.Controls.Add(JournalEntryAmountLabel, 0, 1)
        Me.TableLayoutPanel2.Controls.Add(JournalEntryDocumentTypeLabel, 3, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.JournalEntryDocumentTypeTextBox, 4, 0)
        Me.TableLayoutPanel2.Controls.Add(Me.TableLayoutPanel3, 1, 0)
        Me.TableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel2.Location = New System.Drawing.Point(3, 16)
        Me.TableLayoutPanel2.Name = "TableLayoutPanel2"
        Me.TableLayoutPanel2.RowCount = 5
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel2.RowStyles.Add(New System.Windows.Forms.RowStyle)
        Me.TableLayoutPanel2.Size = New System.Drawing.Size(771, 134)
        Me.TableLayoutPanel2.TabIndex = 0
        '
        'JournalEntryContentTextBox
        '
        Me.TableLayoutPanel2.SetColumnSpan(Me.JournalEntryContentTextBox, 7)
        Me.JournalEntryContentTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OperationValueChangeBindingSource, "JournalEntryContent", True))
        Me.JournalEntryContentTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.JournalEntryContentTextBox.Location = New System.Drawing.Point(95, 109)
        Me.JournalEntryContentTextBox.Name = "JournalEntryContentTextBox"
        Me.JournalEntryContentTextBox.ReadOnly = True
        Me.JournalEntryContentTextBox.Size = New System.Drawing.Size(649, 20)
        Me.JournalEntryContentTextBox.TabIndex = 7
        Me.JournalEntryContentTextBox.TabStop = False
        '
        'OperationValueChangeBindingSource
        '
        Me.OperationValueChangeBindingSource.DataSource = GetType(ApskaitaObjects.Assets.OperationValueChange)
        '
        'JournalEntryDocumentNumberTextBox
        '
        Me.TableLayoutPanel2.SetColumnSpan(Me.JournalEntryDocumentNumberTextBox, 4)
        Me.JournalEntryDocumentNumberTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OperationValueChangeBindingSource, "JournalEntryDocumentNumber", True))
        Me.JournalEntryDocumentNumberTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.JournalEntryDocumentNumberTextBox.Location = New System.Drawing.Point(331, 31)
        Me.JournalEntryDocumentNumberTextBox.Name = "JournalEntryDocumentNumberTextBox"
        Me.JournalEntryDocumentNumberTextBox.ReadOnly = True
        Me.JournalEntryDocumentNumberTextBox.Size = New System.Drawing.Size(413, 20)
        Me.JournalEntryDocumentNumberTextBox.TabIndex = 9
        Me.JournalEntryDocumentNumberTextBox.TabStop = False
        Me.JournalEntryDocumentNumberTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'JournalEntryBookEntriesTextBox
        '
        Me.TableLayoutPanel2.SetColumnSpan(Me.JournalEntryBookEntriesTextBox, 7)
        Me.JournalEntryBookEntriesTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OperationValueChangeBindingSource, "JournalEntryBookEntries", True))
        Me.JournalEntryBookEntriesTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.JournalEntryBookEntriesTextBox.Location = New System.Drawing.Point(95, 83)
        Me.JournalEntryBookEntriesTextBox.Name = "JournalEntryBookEntriesTextBox"
        Me.JournalEntryBookEntriesTextBox.ReadOnly = True
        Me.JournalEntryBookEntriesTextBox.Size = New System.Drawing.Size(649, 20)
        Me.JournalEntryBookEntriesTextBox.TabIndex = 5
        Me.JournalEntryBookEntriesTextBox.TabStop = False
        '
        'JournalEntryPersonTextBox
        '
        Me.TableLayoutPanel2.SetColumnSpan(Me.JournalEntryPersonTextBox, 7)
        Me.JournalEntryPersonTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OperationValueChangeBindingSource, "JournalEntryPerson", True))
        Me.JournalEntryPersonTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.JournalEntryPersonTextBox.Location = New System.Drawing.Point(95, 57)
        Me.JournalEntryPersonTextBox.Name = "JournalEntryPersonTextBox"
        Me.JournalEntryPersonTextBox.ReadOnly = True
        Me.JournalEntryPersonTextBox.Size = New System.Drawing.Size(649, 20)
        Me.JournalEntryPersonTextBox.TabIndex = 3
        Me.JournalEntryPersonTextBox.TabStop = False
        '
        'JournalEntryAmountAccTextBox
        '
        Me.JournalEntryAmountAccTextBox.DataBindings.Add(New System.Windows.Forms.Binding("DecimalValue", Me.OperationValueChangeBindingSource, "JournalEntryAmount", True))
        Me.JournalEntryAmountAccTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.JournalEntryAmountAccTextBox.KeepBackColorWhenReadOnly = False
        Me.JournalEntryAmountAccTextBox.Location = New System.Drawing.Point(95, 31)
        Me.JournalEntryAmountAccTextBox.Name = "JournalEntryAmountAccTextBox"
        Me.JournalEntryAmountAccTextBox.ReadOnly = True
        Me.JournalEntryAmountAccTextBox.Size = New System.Drawing.Size(127, 20)
        Me.JournalEntryAmountAccTextBox.TabIndex = 7
        Me.JournalEntryAmountAccTextBox.TabStop = False
        Me.JournalEntryAmountAccTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'JournalEntryDocumentTypeTextBox
        '
        Me.TableLayoutPanel2.SetColumnSpan(Me.JournalEntryDocumentTypeTextBox, 4)
        Me.JournalEntryDocumentTypeTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OperationValueChangeBindingSource, "JournalEntryDocumentType", True))
        Me.JournalEntryDocumentTypeTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.JournalEntryDocumentTypeTextBox.Location = New System.Drawing.Point(331, 3)
        Me.JournalEntryDocumentTypeTextBox.Name = "JournalEntryDocumentTypeTextBox"
        Me.JournalEntryDocumentTypeTextBox.ReadOnly = True
        Me.JournalEntryDocumentTypeTextBox.Size = New System.Drawing.Size(413, 20)
        Me.JournalEntryDocumentTypeTextBox.TabIndex = 5
        Me.JournalEntryDocumentTypeTextBox.TabStop = False
        Me.JournalEntryDocumentTypeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TableLayoutPanel3
        '
        Me.TableLayoutPanel3.ColumnCount = 5
        Me.TableLayoutPanel2.SetColumnSpan(Me.TableLayoutPanel3, 2)
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle)
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel3.Controls.Add(Me.ViewJournalEntryButton, 2, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.CreateNewJournalEntryButton, 1, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.AttachNewJournalEntryButton, 0, 0)
        Me.TableLayoutPanel3.Controls.Add(Me.JournalEntryIDTextBox, 3, 0)
        Me.TableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel3.Location = New System.Drawing.Point(92, 0)
        Me.TableLayoutPanel3.Margin = New System.Windows.Forms.Padding(0)
        Me.TableLayoutPanel3.Name = "TableLayoutPanel3"
        Me.TableLayoutPanel3.RowCount = 1
        Me.TableLayoutPanel3.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel3.Size = New System.Drawing.Size(157, 28)
        Me.TableLayoutPanel3.TabIndex = 10
        '
        'ViewJournalEntryButton
        '
        Me.ViewJournalEntryButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ViewJournalEntryButton.Image = Global.AccDataBindingsWinForms.My.Resources.Resources.lektuvelis_16
        Me.ViewJournalEntryButton.Location = New System.Drawing.Point(48, 0)
        Me.ViewJournalEntryButton.Margin = New System.Windows.Forms.Padding(0)
        Me.ViewJournalEntryButton.Name = "ViewJournalEntryButton"
        Me.ViewJournalEntryButton.Size = New System.Drawing.Size(24, 24)
        Me.ViewJournalEntryButton.TabIndex = 0
        Me.ViewJournalEntryButton.UseVisualStyleBackColor = True
        '
        'CreateNewJournalEntryButton
        '
        Me.CreateNewJournalEntryButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CreateNewJournalEntryButton.Image = Global.AccDataBindingsWinForms.My.Resources.Resources.Action_file_new_icon_16p
        Me.CreateNewJournalEntryButton.Location = New System.Drawing.Point(24, 0)
        Me.CreateNewJournalEntryButton.Margin = New System.Windows.Forms.Padding(0)
        Me.CreateNewJournalEntryButton.Name = "CreateNewJournalEntryButton"
        Me.CreateNewJournalEntryButton.Size = New System.Drawing.Size(24, 24)
        Me.CreateNewJournalEntryButton.TabIndex = 2
        Me.CreateNewJournalEntryButton.UseVisualStyleBackColor = True
        '
        'AttachNewJournalEntryButton
        '
        Me.AttachNewJournalEntryButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AttachNewJournalEntryButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.AttachNewJournalEntryButton.Image = Global.AccDataBindingsWinForms.My.Resources.Resources.attach_icon_16x16
        Me.AttachNewJournalEntryButton.Location = New System.Drawing.Point(0, 0)
        Me.AttachNewJournalEntryButton.Margin = New System.Windows.Forms.Padding(0)
        Me.AttachNewJournalEntryButton.Name = "AttachNewJournalEntryButton"
        Me.AttachNewJournalEntryButton.Size = New System.Drawing.Size(24, 24)
        Me.AttachNewJournalEntryButton.TabIndex = 3
        Me.AttachNewJournalEntryButton.UseVisualStyleBackColor = True
        '
        'JournalEntryIDTextBox
        '
        Me.JournalEntryIDTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OperationValueChangeBindingSource, "JournalEntryID", True))
        Me.JournalEntryIDTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.JournalEntryIDTextBox.Location = New System.Drawing.Point(75, 3)
        Me.JournalEntryIDTextBox.Name = "JournalEntryIDTextBox"
        Me.JournalEntryIDTextBox.ReadOnly = True
        Me.JournalEntryIDTextBox.Size = New System.Drawing.Size(59, 20)
        Me.JournalEntryIDTextBox.TabIndex = 3
        Me.JournalEntryIDTextBox.TabStop = False
        Me.JournalEntryIDTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'ValueChangePerUnitAccTextBox
        '
        Me.ValueChangePerUnitAccTextBox.DataBindings.Add(New System.Windows.Forms.Binding("DecimalValue", Me.OperationValueChangeBindingSource, "ValueChangePerUnit", True))
        Me.ValueChangePerUnitAccTextBox.DecimalLength = 4
        Me.ValueChangePerUnitAccTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ValueChangePerUnitAccTextBox.KeepBackColorWhenReadOnly = False
        Me.ValueChangePerUnitAccTextBox.Location = New System.Drawing.Point(607, 29)
        Me.ValueChangePerUnitAccTextBox.Name = "ValueChangePerUnitAccTextBox"
        Me.ValueChangePerUnitAccTextBox.Size = New System.Drawing.Size(152, 20)
        Me.ValueChangePerUnitAccTextBox.TabIndex = 14
        Me.ValueChangePerUnitAccTextBox.TabStop = False
        Me.ValueChangePerUnitAccTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'ContentTextBox
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.ContentTextBox, 7)
        Me.ContentTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OperationValueChangeBindingSource, "Content", True))
        Me.ContentTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ContentTextBox.Location = New System.Drawing.Point(80, 55)
        Me.ContentTextBox.MaxLength = 255
        Me.ContentTextBox.Name = "ContentTextBox"
        Me.ContentTextBox.Size = New System.Drawing.Size(679, 20)
        Me.ContentTextBox.TabIndex = 2
        '
        'ValueChangeTotalAccTextBox
        '
        Me.ValueChangeTotalAccTextBox.DataBindings.Add(New System.Windows.Forms.Binding("DecimalValue", Me.OperationValueChangeBindingSource, "ValueChangeTotal", True))
        Me.ValueChangeTotalAccTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ValueChangeTotalAccTextBox.KeepBackColorWhenReadOnly = False
        Me.ValueChangeTotalAccTextBox.Location = New System.Drawing.Point(344, 29)
        Me.ValueChangeTotalAccTextBox.Name = "ValueChangeTotalAccTextBox"
        Me.ValueChangeTotalAccTextBox.Size = New System.Drawing.Size(152, 20)
        Me.ValueChangeTotalAccTextBox.TabIndex = 1
        Me.ValueChangeTotalAccTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'IDTextBox
        '
        Me.IDTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OperationValueChangeBindingSource, "ID", True))
        Me.IDTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.IDTextBox.Location = New System.Drawing.Point(80, 3)
        Me.IDTextBox.Name = "IDTextBox"
        Me.IDTextBox.ReadOnly = True
        Me.IDTextBox.Size = New System.Drawing.Size(152, 20)
        Me.IDTextBox.TabIndex = 6
        Me.IDTextBox.TabStop = False
        Me.IDTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'InsertDateTextBox
        '
        Me.InsertDateTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OperationValueChangeBindingSource, "InsertDate", True))
        Me.InsertDateTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.InsertDateTextBox.Location = New System.Drawing.Point(344, 3)
        Me.InsertDateTextBox.Name = "InsertDateTextBox"
        Me.InsertDateTextBox.ReadOnly = True
        Me.InsertDateTextBox.Size = New System.Drawing.Size(152, 20)
        Me.InsertDateTextBox.TabIndex = 8
        Me.InsertDateTextBox.TabStop = False
        Me.InsertDateTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'UpdateDateTextBox
        '
        Me.UpdateDateTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.OperationValueChangeBindingSource, "UpdateDate", True))
        Me.UpdateDateTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UpdateDateTextBox.Location = New System.Drawing.Point(607, 3)
        Me.UpdateDateTextBox.Name = "UpdateDateTextBox"
        Me.UpdateDateTextBox.ReadOnly = True
        Me.UpdateDateTextBox.Size = New System.Drawing.Size(152, 20)
        Me.UpdateDateTextBox.TabIndex = 12
        Me.UpdateDateTextBox.TabStop = False
        Me.UpdateDateTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'DateAccDatePicker
        '
        Me.DateAccDatePicker.BoldedDates = Nothing
        Me.DateAccDatePicker.DataBindings.Add(New System.Windows.Forms.Binding("Value", Me.OperationValueChangeBindingSource, "Date", True))
        Me.DateAccDatePicker.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DateAccDatePicker.Location = New System.Drawing.Point(80, 29)
        Me.DateAccDatePicker.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.DateAccDatePicker.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.DateAccDatePicker.Name = "DateAccDatePicker"
        Me.DateAccDatePicker.ReadOnly = False
        Me.DateAccDatePicker.ShowWeekNumbers = True
        Me.DateAccDatePicker.Size = New System.Drawing.Size(152, 20)
        Me.DateAccDatePicker.TabIndex = 0
        Me.DateAccDatePicker.Value = New Date(2017, 10, 13, 0, 0, 0, 0)
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.BackgroundInfoPanel1)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(789, 346)
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
        Me.BackgroundInfoPanel1.Size = New System.Drawing.Size(783, 340)
        Me.BackgroundInfoPanel1.TabIndex = 0
        '
        'ProgressFiller1
        '
        Me.ProgressFiller1.Location = New System.Drawing.Point(177, 19)
        Me.ProgressFiller1.Name = "ProgressFiller1"
        Me.ProgressFiller1.Size = New System.Drawing.Size(133, 44)
        Me.ProgressFiller1.TabIndex = 1
        Me.ProgressFiller1.Visible = False
        '
        'ProgressFiller2
        '
        Me.ProgressFiller2.Location = New System.Drawing.Point(330, 18)
        Me.ProgressFiller2.Name = "ProgressFiller2"
        Me.ProgressFiller2.Size = New System.Drawing.Size(172, 44)
        Me.ProgressFiller2.TabIndex = 2
        Me.ProgressFiller2.Visible = False
        '
        'ErrorWarnInfoProvider1
        '
        Me.ErrorWarnInfoProvider1.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.ErrorWarnInfoProvider1.BlinkStyleInformation = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.ErrorWarnInfoProvider1.BlinkStyleWarning = System.Windows.Forms.ErrorBlinkStyle.NeverBlink
        Me.ErrorWarnInfoProvider1.ContainerControl = Me
        Me.ErrorWarnInfoProvider1.DataSource = Me.OperationValueChangeBindingSource
        '
        'F_OperationValueChange
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(797, 414)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.ProgressFiller2)
        Me.Controls.Add(Me.ProgressFiller1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "F_OperationValueChange"
        Me.ShowInTaskbar = False
        Me.Text = "Ilgalaikio turto balansinės vertės pakeitimo (pervertinimo) operacija"
        Me.Panel2.ResumeLayout(False)
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.TableLayoutPanel2.ResumeLayout(False)
        Me.TableLayoutPanel2.PerformLayout()
        CType(Me.OperationValueChangeBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TableLayoutPanel3.ResumeLayout(False)
        Me.TableLayoutPanel3.PerformLayout()
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
    Friend WithEvents TableLayoutPanel1 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents ValueChangeTotalAccTextBox As AccControlsWinForms.AccTextBox
    Friend WithEvents OperationValueChangeBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ValueChangePerUnitAccTextBox As AccControlsWinForms.AccTextBox
    Friend WithEvents UpdateDateTextBox As System.Windows.Forms.TextBox
    Friend WithEvents InsertDateTextBox As System.Windows.Forms.TextBox
    Friend WithEvents IDTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ContentTextBox As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents TableLayoutPanel2 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents JournalEntryDocumentNumberTextBox As System.Windows.Forms.TextBox
    Friend WithEvents JournalEntryIDTextBox As System.Windows.Forms.TextBox
    Friend WithEvents JournalEntryAmountAccTextBox As AccControlsWinForms.AccTextBox
    Friend WithEvents JournalEntryDocumentTypeTextBox As System.Windows.Forms.TextBox
    Friend WithEvents JournalEntryContentTextBox As System.Windows.Forms.TextBox
    Friend WithEvents JournalEntryBookEntriesTextBox As System.Windows.Forms.TextBox
    Friend WithEvents JournalEntryPersonTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ViewJournalEntryButton As System.Windows.Forms.Button
    Friend WithEvents AttachNewJournalEntryButton As System.Windows.Forms.Button
    Friend WithEvents CreateNewJournalEntryButton As System.Windows.Forms.Button
    Friend WithEvents BackgroundInfoPanel1 As AccDataBindingsWinForms.BackgroundInfoPanel
    Friend WithEvents ProgressFiller2 As AccControlsWinForms.ProgressFiller
    Friend WithEvents ProgressFiller1 As AccControlsWinForms.ProgressFiller
    Friend WithEvents ErrorWarnInfoProvider1 As AccControlsWinForms.ErrorWarnInfoProvider
    Friend WithEvents TableLayoutPanel3 As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents DateAccDatePicker As AccControlsWinForms.AccDatePicker
End Class
