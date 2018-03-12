<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F_CommandFetchAuditDataFileSAFT
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(F_CommandFetchAuditDataFileSAFT))
        Me.ProgressFiller1 = New AccControlsWinForms.ProgressFiller()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.VersionComboBox = New System.Windows.Forms.ComboBox()
        Me.PeriodEndAccDatePicker = New AccControlsWinForms.AccDatePicker()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.PeriodStartAccDatePicker = New AccControlsWinForms.AccDatePicker()
        Me.RefreshButton = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ValidateXmlCheckBox = New System.Windows.Forms.CheckBox()
        Me.TableLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ProgressFiller1
        '
        Me.ProgressFiller1.Location = New System.Drawing.Point(2, 1)
        Me.ProgressFiller1.Name = "ProgressFiller1"
        Me.ProgressFiller1.Size = New System.Drawing.Size(248, 238)
        Me.ProgressFiller1.TabIndex = 0
        Me.ProgressFiller1.TabStop = False
        Me.ProgressFiller1.Visible = False
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.ColumnCount = 5
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20.0!))
        Me.TableLayoutPanel1.Controls.Add(Me.VersionComboBox, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.PeriodEndAccDatePicker, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label3, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.PeriodStartAccDatePicker, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.RefreshButton, 3, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.Label2, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.Label1, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.ValidateXmlCheckBox, 1, 3)
        Me.TableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 5
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(345, 250)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'VersionComboBox
        '
        Me.VersionComboBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.VersionComboBox.FormattingEnabled = True
        Me.VersionComboBox.Location = New System.Drawing.Point(107, 55)
        Me.VersionComboBox.Name = "VersionComboBox"
        Me.VersionComboBox.Size = New System.Drawing.Size(156, 21)
        Me.VersionComboBox.TabIndex = 2
        '
        'PeriodEndAccDatePicker
        '
        Me.PeriodEndAccDatePicker.BoldedDates = Nothing
        Me.PeriodEndAccDatePicker.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PeriodEndAccDatePicker.Location = New System.Drawing.Point(107, 29)
        Me.PeriodEndAccDatePicker.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.PeriodEndAccDatePicker.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.PeriodEndAccDatePicker.Name = "PeriodEndAccDatePicker"
        Me.PeriodEndAccDatePicker.ShowWeekNumbers = True
        Me.PeriodEndAccDatePicker.Size = New System.Drawing.Size(156, 20)
        Me.PeriodEndAccDatePicker.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(52, 52)
        Me.Label3.Name = "Label3"
        Me.Label3.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.Label3.Size = New System.Drawing.Size(49, 18)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Versija:"
        '
        'PeriodStartAccDatePicker
        '
        Me.PeriodStartAccDatePicker.BoldedDates = Nothing
        Me.PeriodStartAccDatePicker.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PeriodStartAccDatePicker.Location = New System.Drawing.Point(107, 3)
        Me.PeriodStartAccDatePicker.MaxDate = New Date(9998, 12, 31, 0, 0, 0, 0)
        Me.PeriodStartAccDatePicker.MinDate = New Date(1753, 1, 1, 0, 0, 0, 0)
        Me.PeriodStartAccDatePicker.Name = "PeriodStartAccDatePicker"
        Me.PeriodStartAccDatePicker.ShowWeekNumbers = True
        Me.PeriodStartAccDatePicker.Size = New System.Drawing.Size(156, 20)
        Me.PeriodStartAccDatePicker.TabIndex = 0
        '
        'RefreshButton
        '
        Me.RefreshButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.RefreshButton.Image = Global.AccDataBindingsWinForms.My.Resources.Resources.Button_Reload_icon_24p
        Me.RefreshButton.Location = New System.Drawing.Point(289, 44)
        Me.RefreshButton.Name = "RefreshButton"
        Me.TableLayoutPanel1.SetRowSpan(Me.RefreshButton, 3)
        Me.RefreshButton.Size = New System.Drawing.Size(33, 32)
        Me.RefreshButton.TabIndex = 4
        Me.RefreshButton.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(76, 26)
        Me.Label2.Name = "Label2"
        Me.Label2.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.Label2.Size = New System.Drawing.Size(25, 18)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Iki:"
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(3, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Padding = New System.Windows.Forms.Padding(0, 5, 0, 0)
        Me.Label1.Size = New System.Drawing.Size(98, 18)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Laikotarpis nuo:"
        '
        'ValidateXmlCheckBox
        '
        Me.ValidateXmlCheckBox.AutoSize = True
        Me.ValidateXmlCheckBox.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ValidateXmlCheckBox.Location = New System.Drawing.Point(107, 82)
        Me.ValidateXmlCheckBox.Name = "ValidateXmlCheckBox"
        Me.ValidateXmlCheckBox.Size = New System.Drawing.Size(133, 17)
        Me.ValidateXmlCheckBox.TabIndex = 3
        Me.ValidateXmlCheckBox.Text = "Patikrinti duomenis"
        Me.ValidateXmlCheckBox.UseVisualStyleBackColor = True
        '
        'F_CommandFetchAuditDataFileSAFT
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(345, 250)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.ProgressFiller1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "F_CommandFetchAuditDataFileSAFT"
        Me.ShowInTaskbar = False
        Me.Text = "SAF-T audito duomenų failas"
        Me.TableLayoutPanel1.ResumeLayout(False)
        Me.TableLayoutPanel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ProgressFiller1 As AccControlsWinForms.ProgressFiller
    Friend WithEvents TableLayoutPanel1 As TableLayoutPanel
    Friend WithEvents VersionComboBox As ComboBox
    Friend WithEvents PeriodEndAccDatePicker As AccControlsWinForms.AccDatePicker
    Friend WithEvents Label3 As Label
    Friend WithEvents PeriodStartAccDatePicker As AccControlsWinForms.AccDatePicker
    Friend WithEvents RefreshButton As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents ValidateXmlCheckBox As CheckBox
End Class
