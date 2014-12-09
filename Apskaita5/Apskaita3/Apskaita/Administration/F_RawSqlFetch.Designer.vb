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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.ExecuteButton = New System.Windows.Forms.Button
        Me.SqlQueryTextBox = New System.Windows.Forms.TextBox
        Me.DatabaseGaugeTreeView = New System.Windows.Forms.TreeView
        Me.ResultDataGridView = New System.Windows.Forms.DataGridView
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.ResultDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Top
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.ExecuteButton)
        Me.SplitContainer1.Panel1.Controls.Add(Me.SqlQueryTextBox)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.DatabaseGaugeTreeView)
        Me.SplitContainer1.Size = New System.Drawing.Size(681, 159)
        Me.SplitContainer1.SplitterDistance = 448
        Me.SplitContainer1.TabIndex = 0
        '
        'ExecuteButton
        '
        Me.ExecuteButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ExecuteButton.Location = New System.Drawing.Point(370, 128)
        Me.ExecuteButton.Name = "ExecuteButton"
        Me.ExecuteButton.Size = New System.Drawing.Size(75, 23)
        Me.ExecuteButton.TabIndex = 1
        Me.ExecuteButton.Text = "Execute"
        Me.ExecuteButton.UseVisualStyleBackColor = True
        '
        'SqlQueryTextBox
        '
        Me.SqlQueryTextBox.AllowDrop = True
        Me.SqlQueryTextBox.Location = New System.Drawing.Point(3, 3)
        Me.SqlQueryTextBox.MaxLength = 5000
        Me.SqlQueryTextBox.Multiline = True
        Me.SqlQueryTextBox.Name = "SqlQueryTextBox"
        Me.SqlQueryTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.SqlQueryTextBox.Size = New System.Drawing.Size(442, 119)
        Me.SqlQueryTextBox.TabIndex = 0
        '
        'DatabaseGaugeTreeView
        '
        Me.DatabaseGaugeTreeView.CausesValidation = False
        Me.DatabaseGaugeTreeView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DatabaseGaugeTreeView.HotTracking = True
        Me.DatabaseGaugeTreeView.Location = New System.Drawing.Point(0, 0)
        Me.DatabaseGaugeTreeView.Name = "DatabaseGaugeTreeView"
        Me.DatabaseGaugeTreeView.Size = New System.Drawing.Size(229, 159)
        Me.DatabaseGaugeTreeView.TabIndex = 0
        '
        'ResultDataGridView
        '
        Me.ResultDataGridView.AllowUserToAddRows = False
        Me.ResultDataGridView.AllowUserToDeleteRows = False
        Me.ResultDataGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells
        Me.ResultDataGridView.CausesValidation = False
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
        Me.ResultDataGridView.Location = New System.Drawing.Point(0, 159)
        Me.ResultDataGridView.Name = "ResultDataGridView"
        Me.ResultDataGridView.RowHeadersVisible = False
        Me.ResultDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.ResultDataGridView.Size = New System.Drawing.Size(681, 279)
        Me.ResultDataGridView.TabIndex = 1
        '
        'F_RawSqlFetch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(681, 438)
        Me.Controls.Add(Me.ResultDataGridView)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "F_RawSqlFetch"
        Me.Text = "SQL užklausos"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.ResultDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents ExecuteButton As System.Windows.Forms.Button
    Friend WithEvents SqlQueryTextBox As System.Windows.Forms.TextBox
    Friend WithEvents DatabaseGaugeTreeView As System.Windows.Forms.TreeView
    Friend WithEvents ResultDataGridView As System.Windows.Forms.DataGridView
End Class
