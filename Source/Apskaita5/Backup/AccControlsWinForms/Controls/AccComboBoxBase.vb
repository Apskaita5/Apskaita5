Imports System.Windows.Forms
Imports System.ComponentModel
Imports System.Drawing

''' <summary>
''' A base class for custom comboboxes (DatePicker, NumericTextBox with Calculator, ListBox, etc.).
''' Contains base functionality for drop down button, copy/paste operations and drop down ToolStrip.
''' </summary>
''' <remarks></remarks>
Public MustInherit Class AccComboBoxBase
    Inherits TextBox

    Private Const WM_CUT As Integer = &H300 ' mouse message in ContextMenu
    Private Const WM_COPY As Integer = &H301
    Private Const WM_PASTE As Integer = &H302
    Private Const WM_CLEAR As Integer = &H303

    Private Const SEARCH_BUTTON_WIDTH As Integer = 25

    Private ReadOnly _button As Button
    Private _DropDown As ToolStripDropDown = Nothing
    Private components As System.ComponentModel.IContainer
    Private _DisableDropDown As Boolean = False


#Region "Disabled public properties"

    <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Public Shadows ReadOnly Property Lines() As String()
        Get
            Return MyBase.Lines
        End Get
    End Property

    <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Public Shadows ReadOnly Property ImeMode() As ImeMode
        Get
            Return MyBase.ImeMode
        End Get
    End Property

    <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Public Shadows ReadOnly Property PasswordChar() As Char
        Get
            Return MyBase.PasswordChar
        End Get
    End Property

    <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Public Shadows ReadOnly Property UseSystemPasswordChar() As Boolean
        Get
            Return MyBase.UseSystemPasswordChar
        End Get
    End Property

    <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Public Shadows ReadOnly Property Multiline() As Boolean
        Get
            Return MyBase.Multiline
        End Get
    End Property

    <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Public Shadows ReadOnly Property AcceptsReturn() As Boolean
        Get
            Return MyBase.AcceptsReturn
        End Get
    End Property

    <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Public Shadows ReadOnly Property AcceptsTab() As Boolean
        Get
            Return MyBase.AcceptsTab
        End Get
    End Property

    <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Public Shadows ReadOnly Property AutoCompleteCustomSource() As AutoCompleteStringCollection
        Get
            Return MyBase.AutoCompleteCustomSource
        End Get
    End Property

    <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Public Shadows ReadOnly Property AutoCompleteMode() As AutoCompleteMode
        Get
            Return MyBase.AutoCompleteMode
        End Get
    End Property

    <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Public Shadows ReadOnly Property AutoCompleteSource() As AutoCompleteSource
        Get
            Return MyBase.AutoCompleteSource
        End Get
    End Property

    <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Public Shadows ReadOnly Property CharacterCasing() As CharacterCasing
        Get
            Return MyBase.CharacterCasing
        End Get
    End Property

    <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Public Shadows ReadOnly Property MaxLength() As Integer
        Get
            Return MyBase.MaxLength
        End Get
    End Property

    <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Public Shadows ReadOnly Property ScrollBars() As ScrollBars
        Get
            Return MyBase.ScrollBars
        End Get
    End Property

    <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Public Shadows ReadOnly Property WordWrap() As Boolean
        Get
            Return MyBase.WordWrap
        End Get
    End Property

    <Browsable(False), EditorBrowsable(EditorBrowsableState.Never), _
    Bindable(False), [ReadOnly](True), _
    DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Public Shadows Property Text() As String
        Get
            Return MyBase.Text
        End Get
        Protected Set(ByVal value As String)
            MyBase.Text = value
        End Set
    End Property

#End Region

    ''' <summary>
    ''' Gets or sets whether the dropdown button is visible.
    ''' </summary>
    <Browsable(True)> _
    <Category("Appearance")> _
    <Description("Gets or sets whether the dropdown button is visible."), _
    DefaultValue(True)> _
    Public Property ButtonVisible() As Boolean
        Get
            Return _button.Visible
        End Get
        Set(ByVal value As Boolean)
            If _button.Visible <> value Then
                _button.Visible = value
                MyBase.Invalidate()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets whether the dropdown button is visible.
    ''' </summary>
    <Browsable(True)> _
    <Category("Behaviour")> _
    <Description("Gets or sets whether to disable (do not show) the dropdown."), _
    DefaultValue(False)> _
    Public Property DisableDropDown() As Boolean
        Get
            Return _DisableDropDown
        End Get
        Set(ByVal value As Boolean)
            If _DisableDropDown <> value Then
                _DisableDropDown = value
            End If
        End Set
    End Property

    Protected Property DropDownSize() As Size
        Get
            If _DropDown Is Nothing Then Return Size.Empty
            Return _DropDown.Size
        End Get
        Set(ByVal value As Size)
            If _DropDown Is Nothing Then Exit Property
            _DropDown.Width = value.Width
            _DropDown.Height = value.Height
        End Set
    End Property

    Protected ReadOnly Property DropDownVisible() As Boolean
        Get
            Return Not _DropDown Is Nothing AndAlso _DropDown.Visible
        End Get
    End Property


    Public Sub New()

        MyBase.New()

        MyBase.MaxLength = GetMaxTextLength()

        _button = New Button()
        _button.Size = New Size(GetButtonWidth, MyBase.ClientSize.Height + 2)
        _button.Dock = DockStyle.Right
        _button.Cursor = Cursors.[Default]
        _button.Image = GetButtonImage()
        _button.Text = ""
        _button.FlatStyle = FlatStyle.Flat
        _button.ForeColor = MyBase.BackColor
        _button.FlatAppearance.BorderSize = 0
        _button.TabStop = False
        _button.CausesValidation = False

        AddHandler _button.Click, AddressOf OnButtonClick
        AddHandler _button.SizeChanged, AddressOf OnResizeButton

        MyBase.Controls.Add(_button)

        Dim contextMenu As New ContextMenuStrip()
        contextMenu.Items.Add("Copy", Nothing, AddressOf OnContextMenuItemClicked)
        contextMenu.Items.Add("Cut", Nothing, AddressOf OnContextMenuItemClicked)
        contextMenu.Items.Add("Paste", Nothing, AddressOf OnContextMenuItemClicked)
        contextMenu.Items.Add("Clear", Nothing, AddressOf OnContextMenuItemClicked)
        contextMenu.Items.AddRange(New ToolStripItem() {New ToolStripSeparator()})
        contextMenu.Items.Add("Select All", Nothing, AddressOf OnContextMenuItemClicked)
        contextMenu.ShowImageMargin = False

        contextMenu.Items(0).Enabled = AcceptCopy()
        contextMenu.Items(1).Enabled = AcceptCut()
        contextMenu.Items(2).Enabled = AcceptPaste()
        contextMenu.Items(3).Enabled = AcceptClear()

        MyBase.ContextMenuStrip = contextMenu

    End Sub


    ''' <summary>
    ''' Override in descendant class in order to set specific embeded button width.
    ''' </summary>
    Protected Overridable Function GetButtonWidth() As Integer
        Return SEARCH_BUTTON_WIDTH
    End Function

    ''' <summary>
    ''' Override in descendant class in order to set specific embeded button image.
    ''' </summary>
    Protected Overridable Function GetButtonImage() As Image
        Return My.Resources.arrow_drop_down_16
    End Function

    ''' <summary>
    ''' Override in descendant class in order to set max allowed text length.
    ''' </summary>
    Protected Overridable Function GetMaxTextLength() As Integer
        Return 50
    End Function


    Private Sub OnResizeButton(ByVal sender As Object, ByVal e As EventArgs)
        OnResize(e)
    End Sub

    Protected Overrides Sub OnResize(ByVal e As EventArgs)
        MyBase.OnResize(e)
        _button.Size = New Size(_button.Width, Me.ClientSize.Height + 2)
        _button.Location = New Point(Me.ClientSize.Width - _button.Width, -1)
        ' Send EM_SETMARGINS to prevent text from disappearing underneath the button
        SendMessage(Me.Handle, &HD3, New IntPtr(2), New IntPtr(_button.Width << 16))
    End Sub

    <System.Runtime.InteropServices.DllImport("user32.dll")> _
    Private Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal msg As Integer, _
        ByVal wp As IntPtr, ByVal lp As IntPtr) As IntPtr
    End Function

    Protected Overrides Sub OnReadOnlyChanged(ByVal e As EventArgs)
        MyBase.OnReadOnlyChanged(e)
        Me._button.Enabled = MyBase.Enabled AndAlso Not MyBase.ReadOnly
        MyBase.Invalidate()
    End Sub

    Protected Overrides Sub OnEnabledChanged(ByVal e As EventArgs)
        MyBase.OnEnabledChanged(e)
        Me._button.Enabled = MyBase.Enabled AndAlso Not MyBase.ReadOnly
        MyBase.Invalidate()
    End Sub

    Protected Overrides Sub OnBackColorChanged(ByVal e As EventArgs)
        MyBase.OnBackColorChanged(e)
        _button.ForeColor = MyBase.BackColor
    End Sub


    Protected Overrides Sub WndProc(ByRef m As Message)
        If m.Msg = WM_PASTE Then
            ' mouse paste
            OnPaste()
        ElseIf m.Msg = WM_COPY Then
            ' mouse copy
            OnCopy()
        ElseIf m.Msg = WM_CUT Then
            ' mouse cut or ctrl+x shortcut
            OnCut()
        ElseIf m.Msg = WM_CLEAR Then
            OnClear()
        Else
            MyBase.WndProc(m)
        End If
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, ByVal keyData As Keys) As Boolean

        If keyData = DirectCast(Shortcut.CtrlV, Keys) Then

            OnPaste()

            Return True

        ElseIf keyData = DirectCast(Shortcut.CtrlC, Keys) Then

            OnCopy()
            Return True

        ElseIf keyData = Keys.Down Then

            ShowDropDown()
            Return True

        End If

        Return MyBase.ProcessCmdKey(msg, keyData)

    End Function

    Protected Overridable Sub OnPaste()

    End Sub

    Protected Overridable Function AcceptPaste() As Boolean
        Return False
    End Function

    Protected Overridable Sub OnCopy()
        Clipboard.SetText(MyBase.Text, TextDataFormat.UnicodeText)
    End Sub

    Protected Overridable Function AcceptCopy() As Boolean
        Return True
    End Function

    Protected Overridable Sub OnCut()

    End Sub

    Protected Overridable Function AcceptCut() As Boolean
        Return False
    End Function

    Protected Overridable Sub OnClear()

    End Sub

    Protected Overridable Function AcceptClear() As Boolean
        Return False
    End Function

    Private Sub OnContextMenuItemClicked(ByVal sender As Object, ByVal e As EventArgs)
        If DirectCast(sender, ToolStripItem).Text = "Paste" Then
            OnPaste()
        ElseIf DirectCast(sender, ToolStripItem).Text = "Copy" Then
            OnCopy()
        ElseIf DirectCast(sender, ToolStripItem).Text = "Cut" Then
            OnCut()
        ElseIf DirectCast(sender, ToolStripItem).Text = "Clear" Then
            OnClear()
        ElseIf DirectCast(sender, ToolStripItem).Text = "Select All" Then
            MyBase.SelectAll()
        End If
    End Sub


    Protected Overrides Sub OnMouseDown(ByVal e As MouseEventArgs)
        If e.Button = MouseButtons.Middle Then ShowDropDown()
        'If e.Button = MouseButtons.Right Then ContextMenuStrip1.Show()
    End Sub

    Private Sub OnButtonClick(ByVal sender As Object, ByVal e As EventArgs)
        ShowDropDown()
    End Sub

    Protected Sub ShowDropDown()

        If MyBase.ReadOnly OrElse Not MyBase.Enabled OrElse _DisableDropDown Then Exit Sub

        If _DropDown Is Nothing Then

            If GetToolStripControlHost() Is Nothing Then Throw New InvalidOperationException( _
                "ToolStripControlHost is not initialized.")

            _DropDown = New ToolStripDropDown()
            _DropDown.AutoSize = False
            _DropDown.GripStyle = ToolStripGripStyle.Visible
            _DropDown.Margin = Padding.Empty
            _DropDown.Padding = Padding.Empty
            _DropDown.Size = GetToolStripControlHost.Size

            AddHandler _DropDown.Closed, AddressOf ToolStripDropDown_Closed
            AddHandler _DropDown.Opened, AddressOf ToolStripDropDown_Opened

        End If
        If Not _DropDown.Items.Contains(GetToolStripControlHost()) Then
            _DropDown.Items.Clear()
            _DropDown.Items.Add(GetToolStripControlHost())
        End If

        BeforeDropDownOpen()

        _DropDown.Show(Me, CalculatePoz()) 'New Point(0, Me.Height)

    End Sub

    Private Function CalculatePoz() As Point

        Dim point As New Point(0, Me.Height)

        If (Me.PointToScreen(New Point(0, 0)).Y + Me.Height + GetToolStripControlHost().Height) _
            > Screen.PrimaryScreen.WorkingArea.Height Then
            point.Y = -Me.GetToolStripControlHost().Height - 7
        End If

        Return point

    End Function

    Private Sub ToolStripDropDown_Closed(ByVal sender As Object, _
        ByVal e As ToolStripDropDownClosedEventArgs)
        AfterDropDownClosed(e.CloseReason)
    End Sub

    Private Sub ToolStripDropDown_Opened(ByVal sender As Object, ByVal e As EventArgs)
        AfterDropDownOpen()
    End Sub

    Protected MustOverride Function GetToolStripControlHost() As ToolStripControlHost

    Protected MustOverride Sub BeforeDropDownOpen()

    Protected Overridable Sub AfterDropDownOpen()

    End Sub

    Protected MustOverride Sub AfterDropDownClosed(ByVal reason As ToolStripDropDownCloseReason)

    Private Sub InitializeComponent()
        Me.SuspendLayout()
        Me.ResumeLayout(False)

    End Sub
End Class
