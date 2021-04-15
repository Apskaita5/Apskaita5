Namespace Printing
    <Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
    Partial Class F_PrintReport
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
            Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(F_PrintReport))
            Me.Viewer = New Microsoft.Reporting.WinForms.ReportViewer
            Me.SuspendLayout()
            '
            'Viewer
            '
            Me.Viewer.CausesValidation = False
            Me.Viewer.Dock = System.Windows.Forms.DockStyle.Fill
            Me.Viewer.Location = New System.Drawing.Point(0, 0)
            Me.Viewer.Name = "Viewer"
            Me.Viewer.ShowBackButton = False
            Me.Viewer.ShowContextMenu = False
            Me.Viewer.ShowCredentialPrompts = False
            Me.Viewer.ShowDocumentMapButton = False
            Me.Viewer.ShowFindControls = False
            Me.Viewer.ShowParameterPrompts = False
            Me.Viewer.Size = New System.Drawing.Size(888, 448)
            Me.Viewer.TabIndex = 0
            '
            'F_PrintReport
            '
            Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
            Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
            Me.CausesValidation = False
            Me.ClientSize = New System.Drawing.Size(888, 448)
            Me.Controls.Add(Me.Viewer)
            Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
            Me.Name = "F_PrintReport"
            Me.ShowInTaskbar = False
            Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
            Me.Text = "Ataskaita"
            Me.ResumeLayout(False)

        End Sub
        Friend WithEvents Viewer As Microsoft.Reporting.WinForms.ReportViewer
    End Class
End Namespace
