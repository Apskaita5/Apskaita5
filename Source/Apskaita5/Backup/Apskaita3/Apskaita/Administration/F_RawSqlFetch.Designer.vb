<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F_RawSqlFetch
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(F_RawSqlFetch))
        Me.ExecuteButton = New System.Windows.Forms.Button
        Me.SqlQueryTextBox = New System.Windows.Forms.TextBox
        Me.DatabaseGaugeTreeView = New System.Windows.Forms.TreeView
        Me.ResultDataGridView = New System.Windows.Forms.DataGridView
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer
        CType(Me.ResultDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        Me.SuspendLayout()
        '
        'ExecuteButton
        '
        Me.ExecuteButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ExecuteButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ExecuteButton.Location = New System.Drawing.Point(587, 148)
        Me.ExecuteButton.Name = "ExecuteButton"
        Me.ExecuteButton.Size = New System.Drawing.Size(75, 23)
        Me.ExecuteButton.TabIndex = 1
        Me.ExecuteButton.Text = "Execute"
        Me.ExecuteButton.UseVisualStyleBackColor = True
        '
        'SqlQueryTextBox
        '
        Me.SqlQueryTextBox.AllowDrop = True
        Me.SqlQueryTextBox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SqlQueryTextBox.Location = New System.Drawing.Point(4, 3)
        Me.SqlQueryTextBox.MaxLength = 15000
        Me.SqlQueryTextBox.Multiline = True
        Me.SqlQueryTextBox.Name = "SqlQueryTextBox"
        Me.SqlQueryTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.SqlQueryTextBox.Size = New System.Drawing.Size(659, 139)
        Me.SqlQueryTextBox.TabIndex = 0
        '
        'DatabaseGaugeTreeView
        '
        Me.DatabaseGaugeTreeView.CausesValidation = False
        Me.DatabaseGaugeTreeView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DatabaseGaugeTreeView.HotTracking = True
        Me.DatabaseGaugeTreeView.Location = New System.Drawing.Point(0, 0)
        Me.DatabaseGaugeTreeView.Name = "DatabaseGaugeTreeView"
        Me.DatabaseGaugeTreeView.ShowNodeToolTips = True
        Me.DatabaseGaugeTreeView.Size = New System.Drawing.Size(207, 179)
        Me.DatabaseGaugeTreeView.TabIndex = 0
        '
        'ResultDataGridView
        '
        Me.ResultDataGridView.AllowUserToAddRows = False
        Me.ResultDataGridView.AllowUserToDeleteRows = False
        Me.ResultDataGridView.AllowUserToOrderColumns = True
        Me.ResultDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.ResultDataGridView.CausesValidation = False
        Me.ResultDataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText
        Me.ResultDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.ResultDataGridView.DefaultCellStyle = DataGridViewCellStyle1
        Me.ResultDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ResultDataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.ResultDataGridView.Location = New System.Drawing.Point(0, 0)
        Me.ResultDataGridView.Name = "ResultDataGridView"
        Me.ResultDataGridView.ReadOnly = True
        Me.ResultDataGridView.RowHeadersVisible = False
        Me.ResultDataGridView.Size = New System.Drawing.Size(876, 372)
        Me.ResultDataGridView.TabIndex = 1
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.SplitContainer3)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.ResultDataGridView)
        Me.SplitContainer2.Size = New System.Drawing.Size(876, 555)
        Me.SplitContainer2.SplitterDistance = 179
        Me.SplitContainer2.TabIndex = 2
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.FixedPanel = System.Windows.Forms.FixedPanel.Panel2
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.SqlQueryTextBox)
        Me.SplitContainer3.Panel1.Controls.Add(Me.ExecuteButton)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.DatabaseGaugeTreeView)
        Me.SplitContainer3.Size = New System.Drawing.Size(876, 179)
        Me.SplitContainer3.SplitterDistance = 665
        Me.SplitContainer3.TabIndex = 0
        '
        'F_RawSqlFetch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(876, 555)
        Me.Controls.Add(Me.SplitContainer2)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "F_RawSqlFetch"
        Me.Text = "SQL užklausos"
        CType(Me.ResultDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.PerformLayout()
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ExecuteButton As System.Windows.Forms.Button
    Friend WithEvents SqlQueryTextBox As System.Windows.Forms.TextBox
    Friend WithEvents DatabaseGaugeTreeView As System.Windows.Forms.TreeView
    Friend WithEvents ResultDataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
End Class
