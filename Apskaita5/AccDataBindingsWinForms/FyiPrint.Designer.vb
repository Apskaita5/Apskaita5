<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FyiPrint
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
        Me.RdlViewer1 = New fyiReporting.RdlViewer.RdlViewer
        Me.ViewerToolstrip1 = New fyiReporting.RdlViewer.ViewerToolstrip
        Me.SuspendLayout()
        '
        'RdlViewer1
        '
        Me.RdlViewer1.Cursor = System.Windows.Forms.Cursors.Default
        Me.RdlViewer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.RdlViewer1.dSubReportGetContent = Nothing
        Me.RdlViewer1.Folder = Nothing
        Me.RdlViewer1.HighlightAll = False
        Me.RdlViewer1.HighlightAllColor = System.Drawing.Color.Fuchsia
        Me.RdlViewer1.HighlightCaseSensitive = False
        Me.RdlViewer1.HighlightItemColor = System.Drawing.Color.Aqua
        Me.RdlViewer1.HighlightPageItem = Nothing
        Me.RdlViewer1.HighlightText = Nothing
        Me.RdlViewer1.Location = New System.Drawing.Point(0, 25)
        Me.RdlViewer1.Name = "RdlViewer1"
        Me.RdlViewer1.PageCurrent = 1
        Me.RdlViewer1.Parameters = ""
        Me.RdlViewer1.ReportName = Nothing
        Me.RdlViewer1.ScrollMode = fyiReporting.RdlViewer.ScrollModeEnum.Continuous
        Me.RdlViewer1.SelectTool = False
        Me.RdlViewer1.ShowFindPanel = False
        Me.RdlViewer1.ShowParameterPanel = False
        Me.RdlViewer1.ShowWaitDialog = True
        Me.RdlViewer1.Size = New System.Drawing.Size(660, 260)
        Me.RdlViewer1.SourceFile = Nothing
        Me.RdlViewer1.SourceRdl = Nothing
        Me.RdlViewer1.TabIndex = 0
        Me.RdlViewer1.UseTrueMargins = True
        Me.RdlViewer1.Zoom = 0.75!
        Me.RdlViewer1.ZoomMode = fyiReporting.RdlViewer.ZoomEnum.UseZoom
        '
        'ViewerToolstrip1
        '
        Me.ViewerToolstrip1.Location = New System.Drawing.Point(0, 0)
        Me.ViewerToolstrip1.Name = "ViewerToolstrip1"
        Me.ViewerToolstrip1.Size = New System.Drawing.Size(660, 25)
        Me.ViewerToolstrip1.TabIndex = 1
        Me.ViewerToolstrip1.Text = "ViewerToolstrip1"
        Me.ViewerToolstrip1.Viewer = Me.RdlViewer1
        '
        'FyiPrint
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(660, 285)
        Me.Controls.Add(Me.RdlViewer1)
        Me.Controls.Add(Me.ViewerToolstrip1)
        Me.Name = "FyiPrint"
        Me.Text = "FyiPrint"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents RdlViewer1 As fyiReporting.RdlViewer.RdlViewer
    Friend WithEvents ViewerToolstrip1 As fyiReporting.RdlViewer.ViewerToolstrip
End Class
