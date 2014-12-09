<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.CookiesTextBox = New System.Windows.Forms.TextBox
        Me.CleanCookiesButton = New System.Windows.Forms.Button
        Me.UrlTextBox = New System.Windows.Forms.TextBox
        Me.GoButton = New System.Windows.Forms.Button
        Me.ResponseTextBox = New System.Windows.Forms.TextBox
        Me.UserAgentTextBox = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.AcceptTextBox = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Header1TextBox = New System.Windows.Forms.TextBox
        Me.Header2TextBox = New System.Windows.Forms.TextBox
        Me.ContentTypeTextBox = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.RefererTextBox = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.ContentTextBox = New System.Windows.Forms.TextBox
        Me.PreprogramedTestButton = New System.Windows.Forms.Button
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.TabControl1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'CookiesTextBox
        '
        Me.CookiesTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CookiesTextBox.Location = New System.Drawing.Point(7, 12)
        Me.CookiesTextBox.Multiline = True
        Me.CookiesTextBox.Name = "CookiesTextBox"
        Me.CookiesTextBox.ReadOnly = True
        Me.CookiesTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.CookiesTextBox.Size = New System.Drawing.Size(685, 87)
        Me.CookiesTextBox.TabIndex = 3
        '
        'CleanCookiesButton
        '
        Me.CleanCookiesButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CleanCookiesButton.Location = New System.Drawing.Point(585, 105)
        Me.CleanCookiesButton.Name = "CleanCookiesButton"
        Me.CleanCookiesButton.Size = New System.Drawing.Size(107, 23)
        Me.CleanCookiesButton.TabIndex = 2
        Me.CleanCookiesButton.Text = "Clean Cookies"
        Me.CleanCookiesButton.UseVisualStyleBackColor = True
        '
        'UrlTextBox
        '
        Me.UrlTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UrlTextBox.Location = New System.Drawing.Point(77, 12)
        Me.UrlTextBox.Multiline = True
        Me.UrlTextBox.Name = "UrlTextBox"
        Me.UrlTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.UrlTextBox.Size = New System.Drawing.Size(610, 103)
        Me.UrlTextBox.TabIndex = 1
        '
        'GoButton
        '
        Me.GoButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GoButton.Location = New System.Drawing.Point(625, 409)
        Me.GoButton.Name = "GoButton"
        Me.GoButton.Size = New System.Drawing.Size(63, 23)
        Me.GoButton.TabIndex = 0
        Me.GoButton.Text = "Go"
        Me.GoButton.UseVisualStyleBackColor = True
        '
        'ResponseTextBox
        '
        Me.ResponseTextBox.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ResponseTextBox.Location = New System.Drawing.Point(3, 3)
        Me.ResponseTextBox.Multiline = True
        Me.ResponseTextBox.Name = "ResponseTextBox"
        Me.ResponseTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.ResponseTextBox.Size = New System.Drawing.Size(694, 327)
        Me.ResponseTextBox.TabIndex = 1
        '
        'UserAgentTextBox
        '
        Me.UserAgentTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.UserAgentTextBox.Location = New System.Drawing.Point(77, 121)
        Me.UserAgentTextBox.Name = "UserAgentTextBox"
        Me.UserAgentTextBox.Size = New System.Drawing.Size(610, 20)
        Me.UserAgentTextBox.TabIndex = 4
        Me.UserAgentTextBox.Text = "Mozilla/5.0 (Windows NT 6.1; rv:11.0) Gecko/20100101 Firefox/15.0"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(45, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(32, 13)
        Me.Label1.TabIndex = 5
        Me.Label1.Text = "URL:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(17, 124)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(60, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "UserAgent:"
        '
        'AcceptTextBox
        '
        Me.AcceptTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AcceptTextBox.Location = New System.Drawing.Point(77, 145)
        Me.AcceptTextBox.Name = "AcceptTextBox"
        Me.AcceptTextBox.Size = New System.Drawing.Size(610, 20)
        Me.AcceptTextBox.TabIndex = 7
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(33, 148)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(44, 13)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Accept:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(26, 175)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(51, 13)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "Header1:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(26, 201)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(51, 13)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "Header2:"
        '
        'Header1TextBox
        '
        Me.Header1TextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Header1TextBox.Location = New System.Drawing.Point(77, 172)
        Me.Header1TextBox.Name = "Header1TextBox"
        Me.Header1TextBox.Size = New System.Drawing.Size(610, 20)
        Me.Header1TextBox.TabIndex = 11
        Me.Header1TextBox.Text = "Accept-Language: en-us,en;q=0.5"
        '
        'Header2TextBox
        '
        Me.Header2TextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Header2TextBox.Location = New System.Drawing.Point(77, 198)
        Me.Header2TextBox.Name = "Header2TextBox"
        Me.Header2TextBox.Size = New System.Drawing.Size(610, 20)
        Me.Header2TextBox.TabIndex = 12
        Me.Header2TextBox.Text = "Accept-Encoding: gzip, deflate"
        '
        'ContentTypeTextBox
        '
        Me.ContentTypeTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ContentTypeTextBox.Location = New System.Drawing.Point(77, 224)
        Me.ContentTypeTextBox.Name = "ContentTypeTextBox"
        Me.ContentTypeTextBox.Size = New System.Drawing.Size(610, 20)
        Me.ContentTypeTextBox.TabIndex = 13
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(6, 227)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(71, 13)
        Me.Label6.TabIndex = 14
        Me.Label6.Text = "ContentType:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(32, 253)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(45, 13)
        Me.Label7.TabIndex = 16
        Me.Label7.Text = "Referer:"
        '
        'RefererTextBox
        '
        Me.RefererTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RefererTextBox.Location = New System.Drawing.Point(77, 250)
        Me.RefererTextBox.Multiline = True
        Me.RefererTextBox.Name = "RefererTextBox"
        Me.RefererTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.RefererTextBox.Size = New System.Drawing.Size(610, 81)
        Me.RefererTextBox.TabIndex = 15
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(31, 341)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(47, 13)
        Me.Label8.TabIndex = 18
        Me.Label8.Text = "Content:"
        '
        'ContentTextBox
        '
        Me.ContentTextBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ContentTextBox.Location = New System.Drawing.Point(78, 338)
        Me.ContentTextBox.Multiline = True
        Me.ContentTextBox.Name = "ContentTextBox"
        Me.ContentTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.ContentTextBox.Size = New System.Drawing.Size(610, 65)
        Me.ContentTextBox.TabIndex = 17
        '
        'PreprogramedTestButton
        '
        Me.PreprogramedTestButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PreprogramedTestButton.Location = New System.Drawing.Point(458, 409)
        Me.PreprogramedTestButton.Name = "PreprogramedTestButton"
        Me.PreprogramedTestButton.Size = New System.Drawing.Size(140, 23)
        Me.PreprogramedTestButton.TabIndex = 19
        Me.PreprogramedTestButton.Text = "Preprogramed Test"
        Me.PreprogramedTestButton.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(708, 490)
        Me.TabControl1.TabIndex = 2
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.PreprogramedTestButton)
        Me.TabPage2.Controls.Add(Me.Label8)
        Me.TabPage2.Controls.Add(Me.UserAgentTextBox)
        Me.TabPage2.Controls.Add(Me.ContentTextBox)
        Me.TabPage2.Controls.Add(Me.UrlTextBox)
        Me.TabPage2.Controls.Add(Me.GoButton)
        Me.TabPage2.Controls.Add(Me.Label1)
        Me.TabPage2.Controls.Add(Me.Label7)
        Me.TabPage2.Controls.Add(Me.Label2)
        Me.TabPage2.Controls.Add(Me.RefererTextBox)
        Me.TabPage2.Controls.Add(Me.Label6)
        Me.TabPage2.Controls.Add(Me.AcceptTextBox)
        Me.TabPage2.Controls.Add(Me.ContentTypeTextBox)
        Me.TabPage2.Controls.Add(Me.Label3)
        Me.TabPage2.Controls.Add(Me.Header2TextBox)
        Me.TabPage2.Controls.Add(Me.Label4)
        Me.TabPage2.Controls.Add(Me.Header1TextBox)
        Me.TabPage2.Controls.Add(Me.Label5)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(700, 464)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Request"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.ResponseTextBox)
        Me.TabPage1.Controls.Add(Me.Panel2)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(700, 464)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Response"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.AutoSize = True
        Me.Panel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.Panel2.Controls.Add(Me.CookiesTextBox)
        Me.Panel2.Controls.Add(Me.CleanCookiesButton)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(3, 330)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(694, 131)
        Me.Panel2.TabIndex = 0
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(708, 490)
        Me.Controls.Add(Me.TabControl1)
        Me.Name = "Form1"
        Me.Text = "Form1"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GoButton As System.Windows.Forms.Button
    Friend WithEvents CookiesTextBox As System.Windows.Forms.TextBox
    Friend WithEvents CleanCookiesButton As System.Windows.Forms.Button
    Friend WithEvents UrlTextBox As System.Windows.Forms.TextBox
    Friend WithEvents ResponseTextBox As System.Windows.Forms.TextBox
    Friend WithEvents UserAgentTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents AcceptTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents ContentTypeTextBox As System.Windows.Forms.TextBox
    Friend WithEvents Header2TextBox As System.Windows.Forms.TextBox
    Friend WithEvents Header1TextBox As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents RefererTextBox As System.Windows.Forms.TextBox
    Friend WithEvents PreprogramedTestButton As System.Windows.Forms.Button
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents ContentTextBox As System.Windows.Forms.TextBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage

End Class
