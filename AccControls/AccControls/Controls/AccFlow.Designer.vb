<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AccFlow
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel
        Me.A1 = New AccUserContr
        Me.FlowLayoutPanel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.AutoScroll = True
        Me.FlowLayoutPanel1.Controls.Add(Me.A1)
        Me.FlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(369, 234)
        Me.FlowLayoutPanel1.TabIndex = 0
        Me.FlowLayoutPanel1.WrapContents = False
        '
        'A1
        '
        Me.A1.DataSource = Nothing
        Me.A1.DataSourceMTGC1 = Nothing
        Me.A1.DataSourceMTGC2 = Nothing
        Me.A1.Location = New System.Drawing.Point(3, 3)
        Me.A1.MTGC1ColumnsCount = 2
        Me.A1.MTGC1ColumnWidth = "100;50"
        Me.A1.MTGC1ID = AccUserContr.PropertyID.Col1
        Me.A1.MTGC2ColumnsCount = 2
        Me.A1.MTGC2ColumnWidth = "100;50"
        Me.A1.MTGC2ID = AccUserContr.PropertyID.Col1
        Me.A1.Name = "A1"
        Me.A1.PercMTGC2 = 30
        Me.A1.PercTxtBox = 30
        Me.A1.Size = New System.Drawing.Size(363, 27)
        Me.A1.TabIndex = 0
        '
        'AccFlow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.Controls.Add(Me.FlowLayoutPanel1)
        Me.Name = "AccFlow"
        Me.Size = New System.Drawing.Size(369, 234)
        Me.FlowLayoutPanel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents FlowLayoutPanel1 As System.Windows.Forms.FlowLayoutPanel
    Friend WithEvents A1 As AccUserContr

End Class
