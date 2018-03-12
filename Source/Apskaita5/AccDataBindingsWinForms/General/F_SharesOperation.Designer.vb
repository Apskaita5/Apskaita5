<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F_SharesOperation
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
        Dim IDLabel As System.Windows.Forms.Label
        Dim InsertDateLabel As System.Windows.Forms.Label
        Dim UpdateDateLabel As System.Windows.Forms.Label
        Dim DateLabel As System.Windows.Forms.Label
        Dim DocumentDateLabel As System.Windows.Forms.Label
        Dim DocumentNumberLabel As System.Windows.Forms.Label
        Dim DocumentNameLabel As System.Windows.Forms.Label
        Dim RemarksLabel As System.Windows.Forms.Label
        Dim Label1 As System.Windows.Forms.Label
        Dim Label2 As System.Windows.Forms.Label
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(F_SharesOperation))
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.RemarksTextBox = New System.Windows.Forms.TextBox()
        Me.SharesOperationBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.DocumentNameTextBox = New System.Windows.Forms.TextBox()
        Me.IDTextBox = New System.Windows.Forms.TextBox()
        Me.DocumentDateAccDatePicker = New AccControlsWinForms.AccDatePicker()
        Me.InsertDateTextBox = New System.Windows.Forms.TextBox()
        Me.DateAccDatePicker = New AccControlsWinForms.AccDatePicker()
        Me.UpdateDateTextBox = New System.Windows.Forms.TextBox()
        Me.DocumentNumberTextBox = New System.Windows.Forms.TextBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.nCancelButton = New System.Windows.Forms.Button()
        Me.ApplyButton = New System.Windows.Forms.Button()
        Me.nOkButton = New System.Windows.Forms.Button()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.AcquisitionsDataListView = New BrightIdeasSoftware.DataListView()
        Me.OlvColumn1 = CType(New BrightIdeasSoftware.OLVColumn(), BrightIdeasSoftware.OLVColumn)
        Me.OlvColumn2 = CType(New BrightIdeasSoftware.OLVColumn(), BrightIdeasSoftware.OLVColumn)
        Me.OlvColumn3 = CType(New BrightIdeasSoftware.OLVColumn(), BrightIdeasSoftware.OLVColumn)
        Me.OlvColumn4 = CType(New BrightIdeasSoftware.OLVColumn(), BrightIdeasSoftware.OLVColumn)
        Me.OlvColumn5 = CType(New BrightIdeasSoftware.OLVColumn(), BrightIdeasSoftware.OLVColumn)
        Me.OlvColumn6 = CType(New BrightIdeasSoftware.OLVColumn(), BrightIdeasSoftware.OLVColumn)
        Me.OlvColumn7 = CType(New BrightIdeasSoftware.OLVColumn(), BrightIdeasSoftware.OLVColumn)
        Me.AcquisitionsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.DiscardsDataListView = New BrightIdeasSoftware.DataListView()
        Me.OlvColumn8 = CType(New BrightIdeasSoftware.OLVColumn(), BrightIdeasSoftware.OLVColumn)
        Me.OlvColumn9 = CType(New BrightIdeasSoftware.OLVColumn(), BrightIdeasSoftware.OLVColumn)
        Me.OlvColumn10 = CType(New BrightIdeasSoftware.OLVColumn(), BrightIdeasSoftware.OLVColumn)
        Me.OlvColumn11 = CType(New BrightIdeasSoftware.OLVColumn(), BrightIdeasSoftware.OLVColumn)
        Me.OlvColumn12 = CType(New BrightIdeasSoftware.OLVColumn(), BrightIdeasSoftware.OLVColumn)
        Me.OlvColumn13 = CType(New BrightIdeasSoftware.OLVColumn(), BrightIdeasSoftware.OLVColumn)
        Me.OlvColumn14 = CType(New BrightIdeasSoftware.OLVColumn(), BrightIdeasSoftware.OLVColumn)
        Me.DiscardsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ProgressFiller1 = New AccControlsWinForms.ProgressFiller()
        Me.ErrorWarnInfoProvider1 = New AccControlsWinForms.ErrorWarnInfoProvider(Me.components)
        IDLabel = New System.Windows.Forms.Label()
        InsertDateLabel = New System.Windows.Forms.Label()
        UpdateDateLabel = New System.Windows.Forms.Label()
        DateLabel = New System.Windows.Forms.Label()
        DocumentDateLabel = New System.Windows.Forms.Label()
        DocumentNumberLabel = New System.Windows.Forms.Label()
        DocumentNameLabel = New System.Windows.Forms.Label()
        RemarksLabel = New System.Windows.Forms.Label()
        Label1 = New System.Windows.Forms.Label()
        Label2 = New System.Windows.Forms.Label()
        Me.TableLayoutPanel1.SuspendLayout()
        CType(Me.SharesOperationBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.AcquisitionsDataListView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.AcquisitionsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.DiscardsDataListView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DiscardsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ErrorWarnInfoProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'IDLabel
        '
        IDLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        IDLabel.AutoSize = True
        IDLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        IDLabel.Location = New System.Drawing.Point(92, 0)
        IDLabel.Name = "IDLabel"
        IDLabel.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        IDLabel.Size = New System.Drawing.Size(24, 16)
        IDLabel.TabIndex = 2
        IDLabel.Text = "ID:"
        '
        'InsertDateLabel
        '
        InsertDateLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        InsertDateLabel.AutoSize = True
        InsertDateLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        InsertDateLabel.Location = New System.Drawing.Point(342, 0)
        InsertDateLabel.Name = "InsertDateLabel"
        InsertDateLabel.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        InsertDateLabel.Size = New System.Drawing.Size(55, 16)
        InsertDateLabel.TabIndex = 3
        InsertDateLabel.Text = "Įtraukta:"
        '
        'UpdateDateLabel
        '
        UpdateDateLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        UpdateDateLabel.AutoSize = True
        UpdateDateLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        UpdateDateLabel.Location = New System.Drawing.Point(609, 0)
        UpdateDateLabel.Name = "UpdateDateLabel"
        UpdateDateLabel.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        UpdateDateLabel.Size = New System.Drawing.Size(60, 16)
        UpdateDateLabel.TabIndex = 5
        UpdateDateLabel.Text = "Pakeista:"
        '
        'DateLabel
        '
        DateLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DateLabel.AutoSize = True
        DateLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DateLabel.Location = New System.Drawing.Point(47, 22)
        DateLabel.Name = "DateLabel"
        DateLabel.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        DateLabel.Size = New System.Drawing.Size(69, 16)
        DateLabel.TabIndex = 7
        DateLabel.Text = "Reg. Data:"
        '
        'DocumentDateLabel
        '
        DocumentDateLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DocumentDateLabel.AutoSize = True
        DocumentDateLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DocumentDateLabel.Location = New System.Drawing.Point(328, 22)
        DocumentDateLabel.Name = "DocumentDateLabel"
        DocumentDateLabel.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        DocumentDateLabel.Size = New System.Drawing.Size(69, 16)
        DocumentDateLabel.TabIndex = 9
        DocumentDateLabel.Text = "Dok. Data:"
        '
        'DocumentNumberLabel
        '
        DocumentNumberLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DocumentNumberLabel.AutoSize = True
        DocumentNumberLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DocumentNumberLabel.Location = New System.Drawing.Point(610, 22)
        DocumentNumberLabel.Name = "DocumentNumberLabel"
        DocumentNumberLabel.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        DocumentNumberLabel.Size = New System.Drawing.Size(59, 16)
        DocumentNumberLabel.TabIndex = 11
        DocumentNumberLabel.Text = "Dok. Nr.:"
        '
        'DocumentNameLabel
        '
        DocumentNameLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        DocumentNameLabel.AutoSize = True
        DocumentNameLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DocumentNameLabel.Location = New System.Drawing.Point(3, 44)
        DocumentNameLabel.Name = "DocumentNameLabel"
        DocumentNameLabel.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        DocumentNameLabel.Size = New System.Drawing.Size(113, 16)
        DocumentNameLabel.TabIndex = 1
        DocumentNameLabel.Text = "Dok. Pavadinimas:"
        '
        'RemarksLabel
        '
        RemarksLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        RemarksLabel.AutoSize = True
        RemarksLabel.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        RemarksLabel.Location = New System.Drawing.Point(53, 66)
        RemarksLabel.Name = "RemarksLabel"
        RemarksLabel.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        RemarksLabel.Size = New System.Drawing.Size(63, 16)
        RemarksLabel.TabIndex = 3
        RemarksLabel.Text = "Pastabos:"
        '
        'Label1
        '
        Label1.AutoSize = True
        Label1.Dock = System.Windows.Forms.DockStyle.Fill
        Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label1.Location = New System.Drawing.Point(0, 0)
        Label1.Name = "Label1"
        Label1.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Label1.Size = New System.Drawing.Size(79, 16)
        Label1.TabIndex = 4
        Label1.Text = "Akcijas įgijo:"
        Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label2
        '
        Label2.AutoSize = True
        Label2.Dock = System.Windows.Forms.DockStyle.Fill
        Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Label2.Location = New System.Drawing.Point(0, 0)
        Label2.Name = "Label2"
        Label2.Padding = New System.Windows.Forms.Padding(0, 3, 0, 0)
        Label2.Size = New System.Drawing.Size(145, 16)
        Label2.TabIndex = 4
        Label2.Text = "Akcijas perleido/nurašė:"
        Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 9
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 22.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.RemarksTextBox, 1, 3)
        Me.TableLayoutPanel1.Controls.Add(RemarksLabel, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(DocumentNumberLabel, 6, 1)
        Me.TableLayoutPanel1.Controls.Add(IDLabel, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.DocumentNameTextBox, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(DocumentNameLabel, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.IDTextBox, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.DocumentDateAccDatePicker, 4, 1)
        Me.TableLayoutPanel1.Controls.Add(DocumentDateLabel, 3, 1)
        Me.TableLayoutPanel1.Controls.Add(InsertDateLabel, 3, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.InsertDateTextBox, 4, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.DateAccDatePicker, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(DateLabel, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(UpdateDateLabel, 6, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.UpdateDateTextBox, 7, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.DocumentNumberTextBox, 7, 1)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 4
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(880, 118)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'RemarksTextBox
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.RemarksTextBox, 7)
        Me.RemarksTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.SharesOperationBindingSource, "Remarks", True))
        Me.RemarksTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RemarksTextBox.Location = New System.Drawing.Point(120, 67)
        Me.RemarksTextBox.Margin = New System.Windows.Forms.Padding(1)
        Me.RemarksTextBox.Multiline = True
        Me.RemarksTextBox.Name = "RemarksTextBox"
        Me.RemarksTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.RemarksTextBox.Size = New System.Drawing.Size(737, 50)
        Me.RemarksTextBox.TabIndex = 4
        '
        'SharesOperationBindingSource
        '
        Me.SharesOperationBindingSource.DataSource = GetType(ApskaitaObjects.General.SharesOperation)
        '
        'DocumentNameTextBox
        '
        Me.TableLayoutPanel1.SetColumnSpan(Me.DocumentNameTextBox, 7)
        Me.DocumentNameTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.SharesOperationBindingSource, "DocumentName", True))
        Me.DocumentNameTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DocumentNameTextBox.Location = New System.Drawing.Point(120, 45)
        Me.DocumentNameTextBox.Margin = New System.Windows.Forms.Padding(1)
        Me.DocumentNameTextBox.Name = "DocumentNameTextBox"
        Me.DocumentNameTextBox.Size = New System.Drawing.Size(737, 20)
        Me.DocumentNameTextBox.TabIndex = 3
        '
        'IDTextBox
        '
        Me.IDTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.SharesOperationBindingSource, "ID", True))
        Me.IDTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.IDTextBox.Location = New System.Drawing.Point(120, 1)
        Me.IDTextBox.Margin = New System.Windows.Forms.Padding(1)
        Me.IDTextBox.Name = "IDTextBox"
        Me.IDTextBox.ReadOnly = True
        Me.IDTextBox.Size = New System.Drawing.Size(184, 20)
        Me.IDTextBox.TabIndex = 3
        Me.IDTextBox.TabStop = False
        Me.IDTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'DocumentDateAccDatePicker
        '
        Me.DocumentDateAccDatePicker.BoldedDates = Nothing
        Me.DocumentDateAccDatePicker.DataBindings.Add(New System.Windows.Forms.Binding("Value", Me.SharesOperationBindingSource, "DocumentDate", True))
        Me.DocumentDateAccDatePicker.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DocumentDateAccDatePicker.Location = New System.Drawing.Point(401, 23)
        Me.DocumentDateAccDatePicker.Margin = New System.Windows.Forms.Padding(1)
        Me.DocumentDateAccDatePicker.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.DocumentDateAccDatePicker.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.DocumentDateAccDatePicker.Name = "DocumentDateAccDatePicker"
        Me.DocumentDateAccDatePicker.ShowWeekNumbers = True
        Me.DocumentDateAccDatePicker.Size = New System.Drawing.Size(184, 20)
        Me.DocumentDateAccDatePicker.TabIndex = 1
        '
        'InsertDateTextBox
        '
        Me.InsertDateTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.SharesOperationBindingSource, "InsertDate", True))
        Me.InsertDateTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.InsertDateTextBox.Location = New System.Drawing.Point(401, 1)
        Me.InsertDateTextBox.Margin = New System.Windows.Forms.Padding(1)
        Me.InsertDateTextBox.Name = "InsertDateTextBox"
        Me.InsertDateTextBox.ReadOnly = True
        Me.InsertDateTextBox.Size = New System.Drawing.Size(184, 20)
        Me.InsertDateTextBox.TabIndex = 4
        Me.InsertDateTextBox.TabStop = False
        Me.InsertDateTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'DateAccDatePicker
        '
        Me.DateAccDatePicker.BoldedDates = Nothing
        Me.DateAccDatePicker.DataBindings.Add(New System.Windows.Forms.Binding("Value", Me.SharesOperationBindingSource, "Date", True))
        Me.DateAccDatePicker.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DateAccDatePicker.Location = New System.Drawing.Point(120, 23)
        Me.DateAccDatePicker.Margin = New System.Windows.Forms.Padding(1)
        Me.DateAccDatePicker.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.DateAccDatePicker.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.DateAccDatePicker.Name = "DateAccDatePicker"
        Me.DateAccDatePicker.ShowWeekNumbers = True
        Me.DateAccDatePicker.Size = New System.Drawing.Size(184, 20)
        Me.DateAccDatePicker.TabIndex = 0
        '
        'UpdateDateTextBox
        '
        Me.UpdateDateTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.SharesOperationBindingSource, "UpdateDate", True))
        Me.UpdateDateTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.UpdateDateTextBox.Location = New System.Drawing.Point(673, 1)
        Me.UpdateDateTextBox.Margin = New System.Windows.Forms.Padding(1)
        Me.UpdateDateTextBox.Name = "UpdateDateTextBox"
        Me.UpdateDateTextBox.ReadOnly = True
        Me.UpdateDateTextBox.Size = New System.Drawing.Size(184, 20)
        Me.UpdateDateTextBox.TabIndex = 6
        Me.UpdateDateTextBox.TabStop = False
        Me.UpdateDateTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'DocumentNumberTextBox
        '
        Me.DocumentNumberTextBox.DataBindings.Add(New System.Windows.Forms.Binding("Text", Me.SharesOperationBindingSource, "DocumentNumber", True))
        Me.DocumentNumberTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DocumentNumberTextBox.Location = New System.Drawing.Point(673, 23)
        Me.DocumentNumberTextBox.Margin = New System.Windows.Forms.Padding(1)
        Me.DocumentNumberTextBox.Name = "DocumentNumberTextBox"
        Me.DocumentNumberTextBox.Size = New System.Drawing.Size(184, 20)
        Me.DocumentNumberTextBox.TabIndex = 2
        Me.DocumentNumberTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Panel2
        '
        Me.Panel2.AutoSize = True
        Me.Panel2.Controls.Add(Me.nCancelButton)
        Me.Panel2.Controls.Add(Me.ApplyButton)
        Me.Panel2.Controls.Add(Me.nOkButton)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 371)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Padding = New System.Windows.Forms.Padding(0, 0, 0, 4)
        Me.Panel2.Size = New System.Drawing.Size(880, 37)
        Me.Panel2.TabIndex = 2
        '
        'nCancelButton
        '
        Me.nCancelButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.nCancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.nCancelButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.nCancelButton.Location = New System.Drawing.Point(793, 7)
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
        Me.ApplyButton.Location = New System.Drawing.Point(712, 7)
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
        Me.nOkButton.Location = New System.Drawing.Point(631, 7)
        Me.nOkButton.Name = "nOkButton"
        Me.nOkButton.Size = New System.Drawing.Size(75, 23)
        Me.nOkButton.TabIndex = 0
        Me.nOkButton.Text = "OK"
        Me.nOkButton.UseVisualStyleBackColor = True
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 118)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.AutoScroll = True
        Me.SplitContainer1.Panel1.Controls.Add(Me.AcquisitionsDataListView)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Panel1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.AutoScroll = True
        Me.SplitContainer1.Panel2.Controls.Add(Me.Panel3)
        Me.SplitContainer1.Panel2.Controls.Add(Me.DiscardsDataListView)
        Me.SplitContainer1.Size = New System.Drawing.Size(880, 253)
        Me.SplitContainer1.SplitterDistance = 430
        Me.SplitContainer1.TabIndex = 1
        '
        'AcquisitionsDataListView
        '
        Me.AcquisitionsDataListView.AllColumns.Add(Me.OlvColumn1)
        Me.AcquisitionsDataListView.AllColumns.Add(Me.OlvColumn2)
        Me.AcquisitionsDataListView.AllColumns.Add(Me.OlvColumn3)
        Me.AcquisitionsDataListView.AllColumns.Add(Me.OlvColumn4)
        Me.AcquisitionsDataListView.AllColumns.Add(Me.OlvColumn5)
        Me.AcquisitionsDataListView.AllColumns.Add(Me.OlvColumn6)
        Me.AcquisitionsDataListView.AllColumns.Add(Me.OlvColumn7)
        Me.AcquisitionsDataListView.AllowColumnReorder = True
        Me.AcquisitionsDataListView.AutoGenerateColumns = False
        Me.AcquisitionsDataListView.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.SingleClickAlways
        Me.AcquisitionsDataListView.CellEditEnterChangesRows = True
        Me.AcquisitionsDataListView.CellEditTabChangesRows = True
        Me.AcquisitionsDataListView.CellEditUseWholeCell = False
        Me.AcquisitionsDataListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.OlvColumn1, Me.OlvColumn2, Me.OlvColumn3, Me.OlvColumn4, Me.OlvColumn5, Me.OlvColumn6, Me.OlvColumn7})
        Me.AcquisitionsDataListView.Cursor = System.Windows.Forms.Cursors.Default
        Me.AcquisitionsDataListView.DataSource = Me.AcquisitionsBindingSource
        Me.AcquisitionsDataListView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.AcquisitionsDataListView.FullRowSelect = True
        Me.AcquisitionsDataListView.HasCollapsibleGroups = False
        Me.AcquisitionsDataListView.HeaderWordWrap = True
        Me.AcquisitionsDataListView.HideSelection = False
        Me.AcquisitionsDataListView.IncludeColumnHeadersInCopy = True
        Me.AcquisitionsDataListView.Location = New System.Drawing.Point(0, 24)
        Me.AcquisitionsDataListView.Name = "AcquisitionsDataListView"
        Me.AcquisitionsDataListView.RenderNonEditableCheckboxesAsDisabled = True
        Me.AcquisitionsDataListView.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.Submenu
        Me.AcquisitionsDataListView.SelectedBackColor = System.Drawing.Color.PaleGreen
        Me.AcquisitionsDataListView.SelectedForeColor = System.Drawing.Color.Black
        Me.AcquisitionsDataListView.ShowCommandMenuOnRightClick = True
        Me.AcquisitionsDataListView.ShowGroups = False
        Me.AcquisitionsDataListView.ShowImagesOnSubItems = True
        Me.AcquisitionsDataListView.ShowItemCountOnGroups = True
        Me.AcquisitionsDataListView.ShowItemToolTips = True
        Me.AcquisitionsDataListView.Size = New System.Drawing.Size(430, 229)
        Me.AcquisitionsDataListView.TabIndex = 0
        Me.AcquisitionsDataListView.UnfocusedSelectedBackColor = System.Drawing.Color.PaleGreen
        Me.AcquisitionsDataListView.UnfocusedSelectedForeColor = System.Drawing.Color.Black
        Me.AcquisitionsDataListView.UseCellFormatEvents = True
        Me.AcquisitionsDataListView.UseCompatibleStateImageBehavior = False
        Me.AcquisitionsDataListView.UseFilterIndicator = True
        Me.AcquisitionsDataListView.UseFiltering = True
        Me.AcquisitionsDataListView.UseHotItem = True
        Me.AcquisitionsDataListView.UseNotifyPropertyChanged = True
        Me.AcquisitionsDataListView.View = System.Windows.Forms.View.Details
        '
        'OlvColumn1
        '
        Me.OlvColumn1.AspectName = "ID"
        Me.OlvColumn1.CellEditUseWholeCell = True
        Me.OlvColumn1.DisplayIndex = 6
        Me.OlvColumn1.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn1.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn1.IsEditable = False
        Me.OlvColumn1.IsVisible = False
        Me.OlvColumn1.Text = "ID"
        Me.OlvColumn1.Width = 50
        '
        'OlvColumn2
        '
        Me.OlvColumn2.AspectName = "ShareHolder"
        Me.OlvColumn2.CellEditUseWholeCell = True
        Me.OlvColumn2.DisplayIndex = 0
        Me.OlvColumn2.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn2.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn2.Text = "Akcininkas"
        Me.OlvColumn2.Width = 140
        '
        'OlvColumn3
        '
        Me.OlvColumn3.AspectName = "Class"
        Me.OlvColumn3.CellEditUseWholeCell = True
        Me.OlvColumn3.DisplayIndex = 1
        Me.OlvColumn3.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn3.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn3.Text = "Akcijų klasė"
        Me.OlvColumn3.Width = 151
        '
        'OlvColumn4
        '
        Me.OlvColumn4.AspectName = "Amount"
        Me.OlvColumn4.CellEditUseWholeCell = True
        Me.OlvColumn4.DisplayIndex = 2
        Me.OlvColumn4.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn4.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn4.Text = "Kiekis"
        Me.OlvColumn4.Width = 74
        '
        'OlvColumn5
        '
        Me.OlvColumn5.AspectName = "IsEmission"
        Me.OlvColumn5.CellEditUseWholeCell = True
        Me.OlvColumn5.CheckBoxes = True
        Me.OlvColumn5.DisplayIndex = 3
        Me.OlvColumn5.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn5.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn5.IsHeaderVertical = True
        Me.OlvColumn5.Text = "Emisija"
        Me.OlvColumn5.Width = 32
        '
        'OlvColumn6
        '
        Me.OlvColumn6.AspectName = "IsCompanyShares"
        Me.OlvColumn6.CellEditUseWholeCell = True
        Me.OlvColumn6.CheckBoxes = True
        Me.OlvColumn6.DisplayIndex = 4
        Me.OlvColumn6.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn6.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn6.IsHeaderVertical = True
        Me.OlvColumn6.Text = "Nuosavos akcijos"
        Me.OlvColumn6.Width = 32
        '
        'OlvColumn7
        '
        Me.OlvColumn7.AspectName = "Remarks"
        Me.OlvColumn7.CellEditUseWholeCell = True
        Me.OlvColumn7.DisplayIndex = 5
        Me.OlvColumn7.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn7.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn7.Text = "Pastabos"
        Me.OlvColumn7.Width = 100
        '
        'AcquisitionsBindingSource
        '
        Me.AcquisitionsBindingSource.DataMember = "Acquisitions"
        Me.AcquisitionsBindingSource.DataSource = Me.SharesOperationBindingSource
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(430, 24)
        Me.Panel1.TabIndex = 0
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Label2)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(446, 24)
        Me.Panel3.TabIndex = 1
        '
        'DiscardsDataListView
        '
        Me.DiscardsDataListView.AllColumns.Add(Me.OlvColumn8)
        Me.DiscardsDataListView.AllColumns.Add(Me.OlvColumn9)
        Me.DiscardsDataListView.AllColumns.Add(Me.OlvColumn10)
        Me.DiscardsDataListView.AllColumns.Add(Me.OlvColumn11)
        Me.DiscardsDataListView.AllColumns.Add(Me.OlvColumn12)
        Me.DiscardsDataListView.AllColumns.Add(Me.OlvColumn13)
        Me.DiscardsDataListView.AllColumns.Add(Me.OlvColumn14)
        Me.DiscardsDataListView.AllowColumnReorder = True
        Me.DiscardsDataListView.AutoGenerateColumns = False
        Me.DiscardsDataListView.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.SingleClickAlways
        Me.DiscardsDataListView.CellEditEnterChangesRows = True
        Me.DiscardsDataListView.CellEditTabChangesRows = True
        Me.DiscardsDataListView.CellEditUseWholeCell = False
        Me.DiscardsDataListView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.OlvColumn8, Me.OlvColumn9, Me.OlvColumn10, Me.OlvColumn11, Me.OlvColumn12, Me.OlvColumn13, Me.OlvColumn14})
        Me.DiscardsDataListView.Cursor = System.Windows.Forms.Cursors.Default
        Me.DiscardsDataListView.DataSource = Me.DiscardsBindingSource
        Me.DiscardsDataListView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DiscardsDataListView.FullRowSelect = True
        Me.DiscardsDataListView.HasCollapsibleGroups = False
        Me.DiscardsDataListView.HeaderWordWrap = True
        Me.DiscardsDataListView.HideSelection = False
        Me.DiscardsDataListView.IncludeColumnHeadersInCopy = True
        Me.DiscardsDataListView.Location = New System.Drawing.Point(0, 0)
        Me.DiscardsDataListView.Name = "DiscardsDataListView"
        Me.DiscardsDataListView.RenderNonEditableCheckboxesAsDisabled = True
        Me.DiscardsDataListView.SelectColumnsOnRightClickBehaviour = BrightIdeasSoftware.ObjectListView.ColumnSelectBehaviour.Submenu
        Me.DiscardsDataListView.SelectedBackColor = System.Drawing.Color.PaleGreen
        Me.DiscardsDataListView.SelectedForeColor = System.Drawing.Color.Black
        Me.DiscardsDataListView.ShowCommandMenuOnRightClick = True
        Me.DiscardsDataListView.ShowGroups = False
        Me.DiscardsDataListView.ShowImagesOnSubItems = True
        Me.DiscardsDataListView.ShowItemCountOnGroups = True
        Me.DiscardsDataListView.ShowItemToolTips = True
        Me.DiscardsDataListView.Size = New System.Drawing.Size(446, 253)
        Me.DiscardsDataListView.TabIndex = 0
        Me.DiscardsDataListView.UnfocusedSelectedBackColor = System.Drawing.Color.PaleGreen
        Me.DiscardsDataListView.UnfocusedSelectedForeColor = System.Drawing.Color.Black
        Me.DiscardsDataListView.UseCellFormatEvents = True
        Me.DiscardsDataListView.UseCompatibleStateImageBehavior = False
        Me.DiscardsDataListView.UseFilterIndicator = True
        Me.DiscardsDataListView.UseFiltering = True
        Me.DiscardsDataListView.UseHotItem = True
        Me.DiscardsDataListView.UseNotifyPropertyChanged = True
        Me.DiscardsDataListView.View = System.Windows.Forms.View.Details
        '
        'OlvColumn8
        '
        Me.OlvColumn8.AspectName = "ID"
        Me.OlvColumn8.CellEditUseWholeCell = True
        Me.OlvColumn8.DisplayIndex = 6
        Me.OlvColumn8.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn8.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn8.IsEditable = False
        Me.OlvColumn8.IsVisible = False
        Me.OlvColumn8.Text = "ID"
        Me.OlvColumn8.Width = 50
        '
        'OlvColumn9
        '
        Me.OlvColumn9.AspectName = "ShareHolder"
        Me.OlvColumn9.CellEditUseWholeCell = True
        Me.OlvColumn9.DisplayIndex = 0
        Me.OlvColumn9.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn9.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn9.Text = "Akcininkas"
        Me.OlvColumn9.Width = 171
        '
        'OlvColumn10
        '
        Me.OlvColumn10.AspectName = "Class"
        Me.OlvColumn10.CellEditUseWholeCell = True
        Me.OlvColumn10.DisplayIndex = 1
        Me.OlvColumn10.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn10.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn10.Text = "Akcijų klasė"
        Me.OlvColumn10.Width = 109
        '
        'OlvColumn11
        '
        Me.OlvColumn11.AspectName = "Amount"
        Me.OlvColumn11.CellEditUseWholeCell = True
        Me.OlvColumn11.DisplayIndex = 2
        Me.OlvColumn11.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn11.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn11.Text = "Kiekis"
        Me.OlvColumn11.Width = 79
        '
        'OlvColumn12
        '
        Me.OlvColumn12.AspectName = "IsCancellation"
        Me.OlvColumn12.CellEditUseWholeCell = True
        Me.OlvColumn12.CheckBoxes = True
        Me.OlvColumn12.DisplayIndex = 3
        Me.OlvColumn12.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn12.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn12.IsHeaderVertical = True
        Me.OlvColumn12.Text = "Anuliavimas"
        Me.OlvColumn12.Width = 40
        '
        'OlvColumn13
        '
        Me.OlvColumn13.AspectName = "IsCompanyShares"
        Me.OlvColumn13.CellEditUseWholeCell = True
        Me.OlvColumn13.CheckBoxes = True
        Me.OlvColumn13.DisplayIndex = 4
        Me.OlvColumn13.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn13.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn13.IsHeaderVertical = True
        Me.OlvColumn13.Text = "Nuosavos akcijos"
        Me.OlvColumn13.Width = 40
        '
        'OlvColumn14
        '
        Me.OlvColumn14.AspectName = "Remarks"
        Me.OlvColumn14.CellEditUseWholeCell = True
        Me.OlvColumn14.DisplayIndex = 5
        Me.OlvColumn14.HeaderFont = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.OlvColumn14.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.OlvColumn14.Text = "Pastabos"
        Me.OlvColumn14.Width = 100
        '
        'DiscardsBindingSource
        '
        Me.DiscardsBindingSource.DataMember = "Discards"
        Me.DiscardsBindingSource.DataSource = Me.SharesOperationBindingSource
        '
        'ProgressFiller1
        '
        Me.ProgressFiller1.Location = New System.Drawing.Point(313, 137)
        Me.ProgressFiller1.Name = "ProgressFiller1"
        Me.ProgressFiller1.Size = New System.Drawing.Size(220, 81)
        Me.ProgressFiller1.TabIndex = 5
        Me.ProgressFiller1.Visible = False
        '
        'ErrorWarnInfoProvider1
        '
        Me.ErrorWarnInfoProvider1.ContainerControl = Me
        Me.ErrorWarnInfoProvider1.DataSource = Me.SharesOperationBindingSource
        '
        'F_SharesOperation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(880, 408)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.ProgressFiller1)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "F_SharesOperation"
        Me.ShowInTaskbar = False
        Me.Text = "Operacija su akcijomis"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        CType(Me.SharesOperationBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.AcquisitionsDataListView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.AcquisitionsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.DiscardsDataListView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DiscardsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ErrorWarnInfoProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents RemarksTextBox As TextBox
    Friend WithEvents SharesOperationBindingSource As BindingSource
    Friend WithEvents DocumentNameTextBox As TextBox
    Friend WithEvents IDTextBox As TextBox
    Friend WithEvents DocumentDateAccDatePicker As AccControlsWinForms.AccDatePicker
    Friend WithEvents InsertDateTextBox As TextBox
    Friend WithEvents DateAccDatePicker As AccControlsWinForms.AccDatePicker
    Friend WithEvents UpdateDateTextBox As TextBox
    Friend WithEvents DocumentNumberTextBox As TextBox
    Friend WithEvents Panel2 As Panel
    Friend WithEvents nCancelButton As Button
    Friend WithEvents ApplyButton As Button
    Friend WithEvents nOkButton As Button
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Panel3 As Panel
    Friend WithEvents AcquisitionsBindingSource As BindingSource
    Friend WithEvents DiscardsBindingSource As BindingSource
    Friend WithEvents AcquisitionsDataListView As BrightIdeasSoftware.DataListView
    Friend WithEvents DiscardsDataListView As BrightIdeasSoftware.DataListView
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
    Friend WithEvents ProgressFiller1 As AccControlsWinForms.ProgressFiller
    Friend WithEvents ErrorWarnInfoProvider1 As AccControlsWinForms.ErrorWarnInfoProvider
End Class
