<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class F_UserReport
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(F_UserReport))
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.RefreshButton = New System.Windows.Forms.Button
        Me.RdlViewer1 = New fyiReporting.RdlViewer.RdlViewer
        Me.ViewerToolstrip1 = New fyiReporting.RdlViewer.ViewerToolstrip
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.UserReportInfoListAccListComboBox = New AccControlsWinForms.AccListComboBox
        Me.ProgressFiller1 = New AccControlsWinForms.ProgressFiller
        Me.Panel1.SuspendLayout()
        Me.ViewerToolstrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.AutoSize = True
        Me.Panel1.Controls.Add(Me.RefreshButton)
        Me.Panel1.Controls.Add(Me.UserReportInfoListAccListComboBox)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(608, 42)
        Me.Panel1.TabIndex = 0
        '
        'RefreshButton
        '
        Me.RefreshButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RefreshButton.Image = Global.AccDataBindingsWinForms.My.Resources.Resources.Button_Reload_icon_24p
        Me.RefreshButton.Location = New System.Drawing.Point(564, 7)
        Me.RefreshButton.Name = "RefreshButton"
        Me.RefreshButton.Size = New System.Drawing.Size(33, 32)
        Me.RefreshButton.TabIndex = 2
        Me.RefreshButton.UseVisualStyleBackColor = True
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
        Me.RdlViewer1.Location = New System.Drawing.Point(0, 67)
        Me.RdlViewer1.Name = "RdlViewer1"
        Me.RdlViewer1.PageCurrent = 1
        Me.RdlViewer1.Parameters = ""
        Me.RdlViewer1.ReportName = Nothing
        Me.RdlViewer1.ScrollMode = fyiReporting.RdlViewer.ScrollModeEnum.Continuous
        Me.RdlViewer1.SelectTool = False
        Me.RdlViewer1.ShowFindPanel = False
        Me.RdlViewer1.ShowParameterPanel = False
        Me.RdlViewer1.ShowWaitDialog = True
        Me.RdlViewer1.Size = New System.Drawing.Size(608, 302)
        Me.RdlViewer1.SourceFile = Nothing
        Me.RdlViewer1.SourceRdl = Nothing
        Me.RdlViewer1.TabIndex = 1
        Me.RdlViewer1.UseTrueMargins = True
        Me.RdlViewer1.Zoom = 0.75!
        Me.RdlViewer1.ZoomMode = fyiReporting.RdlViewer.ZoomEnum.UseZoom
        '
        'ViewerToolstrip1
        '
        Me.ViewerToolstrip1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!)
        Me.ViewerToolstrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripSeparator1, Me.ToolStripSeparator2})
        Me.ViewerToolstrip1.Location = New System.Drawing.Point(0, 42)
        Me.ViewerToolstrip1.Name = "ViewerToolstrip1"
        Me.ViewerToolstrip1.Size = New System.Drawing.Size(608, 25)
        Me.ViewerToolstrip1.TabIndex = 2
        Me.ViewerToolstrip1.Text = "ViewerToolstrip1"
        Me.ViewerToolstrip1.Viewer = Me.RdlViewer1
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'UserReportInfoListAccListComboBox
        '
        Me.UserReportInfoListAccListComboBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UserReportInfoListAccListComboBox.EmptyValueString = ""
        Me.UserReportInfoListAccListComboBox.FilterString = ""
        Me.UserReportInfoListAccListComboBox.FormattingEnabled = True
        Me.UserReportInfoListAccListComboBox.InstantBinding = True
        Me.UserReportInfoListAccListComboBox.Location = New System.Drawing.Point(11, 7)
        Me.UserReportInfoListAccListComboBox.Name = "UserReportInfoListAccListComboBox"
        Me.UserReportInfoListAccListComboBox.Size = New System.Drawing.Size(547, 21)
        Me.UserReportInfoListAccListComboBox.TabIndex = 0
        '
        'ProgressFiller1
        '
        Me.ProgressFiller1.Location = New System.Drawing.Point(12, 188)
        Me.ProgressFiller1.Name = "ProgressFiller1"
        Me.ProgressFiller1.Size = New System.Drawing.Size(85, 32)
        Me.ProgressFiller1.TabIndex = 3
        Me.ProgressFiller1.Visible = False
        '
        'F_UserReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(608, 369)
        Me.Controls.Add(Me.RdlViewer1)
        Me.Controls.Add(Me.ViewerToolstrip1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.ProgressFiller1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "F_UserReport"
        Me.ShowInTaskbar = False
        Me.Text = "Vartotojo ataskaita"
        Me.Panel1.ResumeLayout(False)
        Me.ViewerToolstrip1.ResumeLayout(False)
        Me.ViewerToolstrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents UserReportInfoListAccListComboBox As AccControlsWinForms.AccListComboBox
    Friend WithEvents RefreshButton As System.Windows.Forms.Button
    Friend WithEvents RdlViewer1 As fyiReporting.RdlViewer.RdlViewer
    Friend WithEvents ViewerToolstrip1 As fyiReporting.RdlViewer.ViewerToolstrip
    Friend WithEvents ProgressFiller1 As AccControlsWinForms.ProgressFiller
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
End Class
