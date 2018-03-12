Imports System.Windows.Forms
Imports System.Drawing

#Region "ENUMS"

Public Enum rsButtonStyle
    Button
    Switch
    DropDown
    DropDownWithSep
End Enum

Public Enum rsButtonState
    Normal
    Up
    Down
    Dropdown
End Enum

#End Region

#Region "DELEGATES"
Public Delegate Sub DropDownEventHandler(ByVal sender As Object, ByVal e As System.EventArgs)
Public Delegate Sub CheckedEventHandler(ByVal sender As Object, ByVal e As System.EventArgs)
#End Region

Public Class AccButton
    Inherits System.Windows.Forms.Control

    Public Sub New()
    End Sub

#Region "EVENTS"
    Public Event ButtonDropDown As DropDownEventHandler
    Public Event ButtonChecked As CheckedEventHandler
#End Region

#Region "PRIVATE MEMBERS"
    Private _checked As Boolean = False
    Private _buttonState As rsButtonState = rsButtonState.Normal
    Private _lastButtonState As rsButtonState = rsButtonState.Down

    Private _borderStyleNormal As Border3DStyle = Border3DStyle.Flat
    Private _borderStyleUp As Border3DStyle = Border3DStyle.RaisedOuter
    Private _borderStyleDown As Border3DStyle = Border3DStyle.SunkenInner

    Private _buttonStyle As rsButtonStyle = rsButtonStyle.Button
    Private _ddWidth As Integer = 12

    Private _textPadding As Integer = 2
    Private _textAlign As ContentAlignment = ContentAlignment.MiddleCenter

    Private _image As Image
    Private _imagePadding As Integer = 2
    Private _imageAlign As ContentAlignment = ContentAlignment.MiddleCenter

    Private _focusRect As Boolean = False
#End Region

#Region "PROPERTIES"

    Public Property ButtonStyle() As rsButtonStyle
        Get
            Return _buttonStyle
        End Get
        Set(ByVal value As rsButtonStyle)
            _buttonStyle = value
            DrawState(Me.CreateGraphics(), True)
        End Set
    End Property

    Public Property DropDownSepWidth() As Integer
        Get
            Return _ddWidth
        End Get
        Set(ByVal value As Integer)
            _ddWidth = value
            DrawState(Me.CreateGraphics(), True)
        End Set
    End Property

    Public Property ImagePadding() As Integer
        Get
            Return _imagePadding
        End Get
        Set(ByVal value As Integer)
            _imagePadding = value
            DrawState(Me.CreateGraphics(), True)
        End Set
    End Property

    Public Property ImageAlign() As ContentAlignment
        Get
            Return _imageAlign
        End Get
        Set(ByVal value As ContentAlignment)
            _imageAlign = value
            DrawState(Me.CreateGraphics(), True)
        End Set
    End Property

    Public Property Image() As Image
        Get
            Return _image
        End Get
        Set(ByVal value As Image)
            _image = value
            DrawState(Me.CreateGraphics(), True)
        End Set
    End Property

    Public Property TextAlign() As ContentAlignment
        Get
            Return _textAlign
        End Get
        Set(ByVal value As ContentAlignment)
            _textAlign = value
            DrawState(Me.CreateGraphics(), True)
        End Set
    End Property

    Public Property TextPadding() As Integer
        Get
            Return _textPadding
        End Get
        Set(ByVal value As Integer)
            _textPadding = value
            DrawState(Me.CreateGraphics(), True)
        End Set
    End Property

    Public Property Checked() As Boolean
        Get
            Return _checked
        End Get
        Set(ByVal value As Boolean)
            _checked = value
            DrawState(Me.CreateGraphics(), True)
        End Set
    End Property

    Public Property BorderStyleNormal() As Border3DStyle
        Get
            Return _borderStyleNormal
        End Get
        Set(ByVal value As Border3DStyle)
            _borderStyleNormal = value
            DrawState(Me.CreateGraphics(), True)
        End Set
    End Property

    Public Property BorderStyleUp() As Border3DStyle
        Get
            Return _borderStyleUp
        End Get
        Set(ByVal value As Border3DStyle)
            _borderStyleUp = value
            DrawState(Me.CreateGraphics(), True)
        End Set
    End Property

    Public Property BorderStyleDown() As Border3DStyle
        Get
            Return _borderStyleDown
        End Get
        Set(ByVal value As Border3DStyle)
            _borderStyleDown = value
            DrawState(Me.CreateGraphics(), True)
        End Set
    End Property

    Public Property FocusRectangle() As Boolean
        Get
            Return _focusRect
        End Get
        Set(ByVal value As Boolean)
            _focusRect = value
        End Set
    End Property

#End Region

#Region "PUBLIC METHODS"

    Public Sub CancelDropDown()
        _buttonState = rsButtonState.Normal
        DrawState(Me.CreateGraphics(), False)
    End Sub

    Public Sub DoDropDown()
        RaiseEvent ButtonDropDown(Me, New System.EventArgs()) ' keista C# sintakse
        If ContextMenu IsNot Nothing Then
            Me.ContextMenu.Show(Me, New Point(0, Me.Height))
            _buttonState = rsButtonState.Normal
        End If
    End Sub

#End Region

#Region "OVERRIDES"

    Protected Overrides Sub OnMouseEnter(ByVal e As System.EventArgs)
        Select Case _buttonStyle
            Case rsButtonStyle.Button
                _buttonState = rsButtonState.Up
            Case rsButtonStyle.Switch
                If _checked Then
                    _buttonState = rsButtonState.Down
                Else
                    _buttonState = rsButtonState.Up
                End If
            Case rsButtonStyle.DropDownWithSep, rsButtonStyle.DropDown
                If _buttonState <> rsButtonState.Dropdown Then _
                    _buttonState = rsButtonState.Up
        End Select

        DrawState(Me.CreateGraphics(), False)
        MyBase.OnMouseEnter(e)
    End Sub

    Protected Overrides Sub OnMouseLeave(ByVal e As System.EventArgs)
        Select Case _buttonStyle
            Case rsButtonStyle.Button
                _buttonState = rsButtonState.Normal
            Case rsButtonStyle.Switch
                If _checked Then
                    _buttonState = rsButtonState.Down
                Else
                    _buttonState = rsButtonState.Normal
                End If
            Case rsButtonStyle.DropDownWithSep, rsButtonStyle.DropDown
                If _buttonState <> rsButtonState.Dropdown Then _
                    _buttonState = rsButtonState.Normal
        End Select
        DrawState(Me.CreateGraphics(), False)
        MyBase.OnMouseLeave(e)
    End Sub

    Protected Overrides Sub OnMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
        Me.Focus()
        Select Case _buttonStyle
            Case rsButtonStyle.Switch, rsButtonStyle.Button
                _buttonState = rsButtonState.Down
            Case rsButtonStyle.DropDown
                _buttonState = rsButtonState.Dropdown
            Case rsButtonStyle.DropDownWithSep
                If e.X > (Me.Width - _ddWidth) Then
                    _buttonState = rsButtonState.Dropdown
                Else
                    _buttonState = rsButtonState.Down
                End If
        End Select
        _checked = Not _checked
        If ((_buttonStyle = rsButtonStyle.Switch) AndAlso _checked) Then _
            RaiseEvent ButtonChecked(Me, New System.EventArgs()) ' raise event ???

        DrawState(Me.CreateGraphics(), True)

        MyBase.OnMouseDown(e)
    End Sub

    Protected Overrides Sub OnMouseUp(ByVal e As System.Windows.Forms.MouseEventArgs)
        Select Case _buttonStyle
            Case rsButtonStyle.Button
                _buttonState = rsButtonState.Up
            Case rsButtonStyle.Switch
                If _checked Then
                    _buttonState = rsButtonState.Down
                Else
                    _buttonState = rsButtonState.Up
                End If
            Case rsButtonStyle.DropDownWithSep, rsButtonStyle.DropDown
                If _buttonState <> rsButtonState.Dropdown Then _buttonState = rsButtonState.Up
        End Select

        DrawState(Me.CreateGraphics(), False)
        MyBase.OnMouseUp(e)
    End Sub

    Protected Overrides Sub OnResize(ByVal e As System.EventArgs)
        DrawState(Me.CreateGraphics(), True)
        MyBase.OnResize(e)
    End Sub

    Protected Overrides Sub OnTextChanged(ByVal e As System.EventArgs)
        DrawState(Me.CreateGraphics(), True)
        MyBase.OnTextChanged(e)
    End Sub

    Protected Overrides Sub OnClick(ByVal e As System.EventArgs)
        If _buttonState = rsButtonState.Dropdown Then
            Me.DoDropDown()
            Exit Sub
        End If
        MyBase.OnClick(e)
    End Sub
		
    Protected Overrides Sub OnPaint(ByVal pevent As System.Windows.Forms.PaintEventArgs)
        DrawState(pevent.Graphics, True)
    End Sub

    Protected Overrides Sub OnLostFocus(ByVal e As System.EventArgs)
        If _focusRect Then DrawState(Me.CreateGraphics(), True)
        MyBase.OnLostFocus(e)
    End Sub

    Protected Overrides Sub OnGotFocus(ByVal e As System.EventArgs)
        If _focusRect Then DrawState(Me.CreateGraphics(), True)
        MyBase.OnGotFocus(e)
    End Sub

    Protected Overrides Sub OnEnabledChanged(ByVal e As System.EventArgs)
        DrawState(Me.CreateGraphics(), True)
        MyBase.OnEnabledChanged(e)
    End Sub

    Protected Overrides Sub OnKeyUp(ByVal e As System.Windows.Forms.KeyEventArgs)
        Select Case e.KeyCode
            Case Keys.Enter
                If _buttonStyle <> rsButtonStyle.DropDown Then
                    Dim eM As System.EventArgs = New System.EventArgs()
                    Me.OnMouseLeave(eM)
                End If
        End Select
    End Sub

    Protected Overrides Sub OnKeyDown(ByVal e As System.Windows.Forms.KeyEventArgs)
        Select Case e.KeyCode
            Case Keys.Enter
                If _buttonStyle = rsButtonStyle.DropDown Then
                    _buttonState = rsButtonState.Dropdown
                    DrawState(Me.CreateGraphics(), False)
                    Me.DoDropDown()
                    _buttonState = rsButtonState.Normal
                    DrawState(Me.CreateGraphics(), False)
                Else
                    Dim eM2 As MouseEventArgs = New MouseEventArgs(Windows.Forms.MouseButtons.Left, 1, 1, 1, 0)
                    Me.OnMouseDown(eM2)
                End If

            Case Keys.Space
                Select Case _buttonStyle
                    Case rsButtonStyle.DropDownWithSep, rsButtonStyle.DropDown
                        _buttonState = rsButtonState.Dropdown
                        DrawState(Me.CreateGraphics(), False)
                        Me.DoDropDown()
                        _buttonState = rsButtonState.Normal
                        DrawState(Me.CreateGraphics(), False)
                End Select
        End Select

        MyBase.OnKeyDown(e)
    End Sub

#End Region

#Region "PRIVATE METHODS"

    Private Sub DrawState(ByVal g As Graphics, ByVal Refresh As Boolean)
        If Not Refresh Then
            If _lastButtonState = _buttonState Then Exit Sub
        End If

        _lastButtonState = _buttonState
        Dim plus As Integer = 0
        Dim r As Rectangle = New Rectangle(0, 0, Me.Width, Me.Height)

        g.Clear(Me.BackColor)

        Select Case _buttonState
            Case rsButtonState.Normal
                If _buttonStyle = rsButtonStyle.DropDownWithSep Then
                    ControlPaint.DrawBorder3D(g, New Rectangle(r.X, r.Y, r.Width - _ddWidth, r.Height), _borderStyleNormal)
                    ControlPaint.DrawBorder3D(g, New Rectangle(r.Width - _ddWidth, r.Y, _ddWidth, r.Height), _borderStyleNormal)
                Else
                    ControlPaint.DrawBorder3D(g, r, _borderStyleNormal)
                End If
                plus = 0

            Case rsButtonState.Up
                If _buttonStyle = rsButtonStyle.DropDownWithSep Then
                    ControlPaint.DrawBorder3D(g, New Rectangle(r.X, r.Y, r.Width - _ddWidth, r.Height), _borderStyleUp)
                    ControlPaint.DrawBorder3D(g, New Rectangle(r.Width - _ddWidth, r.Y, _ddWidth, r.Height), _borderStyleUp)
                Else
                    ControlPaint.DrawBorder3D(g, r, _borderStyleUp)
                End If
                plus = 0

            Case rsButtonState.Down
                If _buttonStyle = rsButtonStyle.DropDownWithSep Then
                    ControlPaint.DrawBorder3D(g, New Rectangle(r.X, r.Y, r.Width - _ddWidth, r.Height), _borderStyleDown)
                    ControlPaint.DrawBorder3D(g, New Rectangle(r.Width - _ddWidth, r.Y, _ddWidth, r.Height), _borderStyleUp)
                Else
                    ControlPaint.DrawBorder3D(g, r, _borderStyleDown)
                End If
                plus = 1

            Case rsButtonState.Dropdown
                If _buttonStyle = rsButtonStyle.DropDownWithSep Then
                    ControlPaint.DrawBorder3D(g, New Rectangle(r.X, r.Y, r.Width - _ddWidth, r.Height), _borderStyleUp)
                    ControlPaint.DrawBorder3D(g, New Rectangle(r.Width - _ddWidth, r.Y, _ddWidth, r.Height), _borderStyleDown)
                ElseIf _buttonStyle = rsButtonStyle.DropDown Then
                    ControlPaint.DrawBorder3D(g, r, _borderStyleDown)
                End If
                plus = IIf(_buttonStyle = rsButtonStyle.DropDownWithSep, 0, 1)
        End Select

        If _focusRect AndAlso Me.Focused Then
            Dim fR As Rectangle = r
            fR.Inflate(New Size(-3, -3))
            fR.Width = fR.Width - IIf(_buttonStyle = rsButtonStyle.DropDownWithSep, _ddWidth, 0)
            ControlPaint.DrawFocusRectangle(g, fR)
        End If

        If _image IsNot Nothing Then
            Dim pX As Integer = 0
            Dim pY As Integer = 0

            Select Case _imageAlign
                Case ContentAlignment.TopCenter, ContentAlignment.BottomCenter, ContentAlignment.MiddleCenter
                    pX = (r.Width - _image.Width - IIf(_buttonStyle = rsButtonStyle.DropDownWithSep, _ddWidth, 0)) / 2
                Case ContentAlignment.TopRight, ContentAlignment.BottomRight, ContentAlignment.MiddleRight
                    pX = r.Width - _image.Width - _imagePadding - IIf(_buttonStyle = rsButtonStyle.DropDownWithSep, _ddWidth, 0)
                Case ContentAlignment.TopLeft, ContentAlignment.BottomLeft, ContentAlignment.MiddleLeft
                    pX = _imagePadding
            End Select
            Select Case _imageAlign
                Case ContentAlignment.MiddleRight, ContentAlignment.MiddleCenter, ContentAlignment.MiddleLeft
                    pY = (r.Height - _image.Height) / 2
                Case ContentAlignment.BottomRight, ContentAlignment.BottomLeft, ContentAlignment.BottomCenter
                    pY = r.Height - _image.Height - _imagePadding
                Case ContentAlignment.TopRight, ContentAlignment.TopLeft, ContentAlignment.TopCenter
                    pY = 0 + _imagePadding
            End Select

            If Me.Enabled Then
                g.DrawImage(_image, pX + plus, pY + plus)
            Else
                ControlPaint.DrawImageDisabled(g, _image, pX + plus, pY + plus, Me.BackColor)
            End If
        End If

        If Me.Text.Trim() <> "" Then
            Dim tX As Single = 0
            Dim tY As Single = 0
            Dim sF As StringFormat = New StringFormat()
            Dim tW As Single = Me.Width
            tW = tW - IIf(_buttonStyle = rsButtonStyle.DropDownWithSep, _ddWidth, 0)
            Dim tS As SizeF = g.MeasureString(Me.Text, Me.Font, System.Convert.ToInt32(tW))

            Select Case _textAlign
                Case ContentAlignment.TopCenter, ContentAlignment.BottomCenter, ContentAlignment.MiddleCenter
                    tX = (r.Width - tS.Width - IIf(_buttonStyle = rsButtonStyle.DropDownWithSep, _ddWidth, 0)) / 2
                    sF.Alignment = StringAlignment.Center
                Case ContentAlignment.TopRight, ContentAlignment.BottomRight, ContentAlignment.MiddleRight
                    tX = r.Width - tS.Width - _textPadding - IIf(_buttonStyle = rsButtonStyle.DropDownWithSep, _ddWidth, 0)
                    sF.Alignment = StringAlignment.Far
                Case ContentAlignment.TopLeft, ContentAlignment.BottomLeft, ContentAlignment.MiddleLeft
                    tX = _textPadding
                    sF.Alignment = StringAlignment.Near
            End Select
            Select Case _textAlign
                Case ContentAlignment.MiddleRight, ContentAlignment.MiddleLeft, ContentAlignment.MiddleCenter
                    tY = (r.Height - tS.Height) / 2
                Case ContentAlignment.BottomRight, ContentAlignment.BottomLeft, ContentAlignment.BottomCenter
                    tY = r.Height - tS.Height - _textPadding
                Case ContentAlignment.TopRight, ContentAlignment.TopCenter, ContentAlignment.TopLeft
                    tY = _textPadding
            End Select

            g.DrawString(Me.Text, Me.Font, New SolidBrush(IIf(Me.Enabled, Me.ForeColor, SystemColors.ControlDark)), _
                New RectangleF(tX + plus, tY + plus, tS.Width, tS.Height), sF)
        End If

        If _buttonStyle = rsButtonStyle.DropDown OrElse _buttonStyle = rsButtonStyle.DropDownWithSep Then
            If _buttonState = rsButtonState.Dropdown Then
                plus = 1
            Else
                plus = 0
            End If

            Dim sX As Integer = r.Width - (System.Math.Abs(_ddWidth / 2)) - 3 + plus
            Dim sY As Integer = (System.Math.Abs(r.Height / 2)) - 1 + plus
            Dim p(2) As Point
            p(0) = New Point(sX, sY)
            p(1) = New Point(sX + 5, sY)
            p(2) = New Point(sX + 2, sY + 3)
            g.FillPolygon(New SolidBrush(Me.ForeColor), p)

        End If


    End Sub

#End Region

End Class
