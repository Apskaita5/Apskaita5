Imports System.Windows.Forms
Imports System.ComponentModel
Imports System.Drawing
Imports System.IO
Imports System.Collections.Generic
Imports System.Text

''' <summary>
''' design by: csust.hulihui(mailto: ehulh@163.com, home at: http://blog.csdn.net/hulihui)
''' </summary>
''' <remarks></remarks>
<ToolboxItem(True)> _
<ToolboxBitmap(GetType(TextBox))> _
<System.ComponentModel.DefaultBindingProperty("DecimalValue")> _
Public Class AccTextBox
    Inherits TextBox

#Region "Member fields"

    Private Const _MaxDecimalLength As Integer = 10 ' max dot length
    Private Const _MaxValueLength As Integer = 27 ' decimal can be 28 bits.

    Private Const WM_CHAR As Integer = &H102 ' char key message

    Private Const WM_CUT As Integer = &H300 ' mouse message in ContextMenu
    Private Const WM_COPY As Integer = &H301
    Private Const WM_PASTE As Integer = &H302
    Private Const WM_CLEAR As Integer = &H303

    Private _decimalLength As Integer = 2
    Private _allowNegative As Boolean = True
    Private _valueFormatStr As String = String.Empty
    Private _value As Double = 0
    Private _supressFormating As Boolean = False

    Private _ApplicableCulture As System.Globalization.CultureInfo
    Private _decimalSeparator As Char = "."c
    Private _alternateDecimalSeparator As Char = "."c
    Private _negativeSign As Char = "-"c
    Private _isInEditMode As Boolean = False

    Private _keepBackColorWhenReadOnly As Boolean = True
    Private _backColor As Color
    Private _backColorWhenReadOnly As Color

#End Region

#Region "Class constructor"

    Public Sub New()

        MyBase.Multiline = False
        MyBase.TextAlign = HorizontalAlignment.Right

        _ApplicableCulture = System.Threading.Thread.CurrentThread.CurrentCulture
        _decimalSeparator = _ApplicableCulture.NumberFormat.NumberDecimalSeparator(0)
        If _decimalSeparator = "," Then
            _alternateDecimalSeparator = "."
        Else
            _alternateDecimalSeparator = ","
        End If
        _negativeSign = _ApplicableCulture.NumberFormat.NegativeSign(0)

        If MyBase.ReadOnly Then
            _backColorWhenReadOnly = MyBase.BackColor
            MyBase.ReadOnly = False
            _backColor = MyBase.BackColor
            MyBase.ReadOnly = True
        Else
            _backColor = MyBase.BackColor
            MyBase.ReadOnly = True
            _backColorWhenReadOnly = MyBase.BackColor
            MyBase.ReadOnly = False
        End If

        Me.SetValueFormatStr()
        MyBase.Text = _value.ToString(_valueFormatStr)

    End Sub

#End Region

#Region "Disabled public properties"

    <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
    Public Shadows ReadOnly Property Lines() As String()
        Get
            Return MyBase.Lines
        End Get
    End Property

    <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
    Public Shadows ReadOnly Property ImeMode() As ImeMode
        Get
            Return MyBase.ImeMode
        End Get
    End Property

    <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
    Public Shadows ReadOnly Property PasswordChar() As Char
        Get
            Return MyBase.PasswordChar
        End Get
    End Property

    <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
    Public Shadows ReadOnly Property UseSystemPasswordChar() As Boolean
        Get
            Return MyBase.UseSystemPasswordChar
        End Get
    End Property

    <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
    Public Shadows ReadOnly Property Multiline() As Boolean
        Get
            Return MyBase.Multiline
        End Get
    End Property

    <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
    Public Shadows ReadOnly Property AutoCompleteCustomSource() As AutoCompleteStringCollection
        Get
            Return MyBase.AutoCompleteCustomSource
        End Get
    End Property

    <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
    Public Shadows ReadOnly Property AutoCompleteMode() As AutoCompleteMode
        Get
            Return MyBase.AutoCompleteMode
        End Get
    End Property

    <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
    Public Shadows ReadOnly Property AutoCompleteSource() As AutoCompleteSource
        Get
            Return MyBase.AutoCompleteSource
        End Get
    End Property

    <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
    Public Shadows ReadOnly Property CharacterCasing() As CharacterCasing
        Get
            Return MyBase.CharacterCasing
        End Get
    End Property

    <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
    Public Shadows ReadOnly Property MaxLength() As Integer
        Get
            Return MyBase.MaxLength
        End Get
    End Property

    <Browsable(False), EditorBrowsable(EditorBrowsableState.Never), _
    Bindable(False), [ReadOnly](True), _
    DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Public Shadows Property Text() As String
        Get
            Return MyBase.Text
        End Get
        Set(ByVal value As String)

        End Set
    End Property

#End Region

#Region "Override public properties"

    <DefaultValue(GetType(Color), "Window")> _
    Public Shadows Property BackColor() As Color
        Get
            Return _backColor
        End Get
        Set(ByVal value As Color)
            If _backColor <> value Then
                _backColor = value
                Me.OnBackColorChanged(EventArgs.Empty)
            End If
        End Set
    End Property

    Public Shadows Property [ReadOnly]() As Boolean
        Get
            Return MyBase.[ReadOnly]
        End Get
        Set(ByVal value As Boolean)
            If MyBase.[ReadOnly] <> value Then
                MyBase.[ReadOnly] = value
                Me.OnBackColorChanged(EventArgs.Empty)
            End If
        End Set
    End Property

#End Region

#Region "Custom public properties"

    Public Event OnDecimalValueChanged As eventhandler

    <Category("Custom")> _
    <Description("Set/Get if keep backcolor when textbox is readonly.")> _
    <DefaultValue(True)> _
    Public Property KeepBackColorWhenReadOnly() As Boolean
        Get
            Return _keepBackColorWhenReadOnly
        End Get
        Set(ByVal value As Boolean)
            If _keepBackColorWhenReadOnly <> value Then
                _keepBackColorWhenReadOnly = value
                Me.OnBackColorChanged(EventArgs.Empty)
            End If
        End Set
    End Property

    <Category("Custom")> _
    <Description("Set/Get dot length(0 is integer, 10 is maximum).")> _
    <DefaultValue(2)> _
    Public Property DecimalLength() As Integer
        Get
            Return _decimalLength
        End Get
        Set(ByVal value As Integer)
            If _decimalLength <> value Then
                If value < 0 OrElse value > _MaxDecimalLength Then
                    _decimalLength = 0
                Else
                    _decimalLength = value
                End If
                _value = Math.Round(_value, _decimalLength)
                Me.SetValueFormatStr()
                If _isInEditMode OrElse _supressFormating Then
                    MyBase.Text = _value.ToString
                Else
                    MyBase.Text = _value.ToString(_valueFormatStr)
                End If
                RaiseEvent OnDecimalValueChanged(Me, New EventArgs)
            End If
        End Set
    End Property

    <Category("Custom")> _
    <Description("Get decimal value of textbox.")> _
    <DefaultValue(GetType(Double), "0")> _
    <Bindable(True)> _
    <Browsable(True)> _
    Public Property DecimalValue() As Double
        Get
            Return _value
        End Get
        Set(ByVal value As Double)
            If value <> _value AndAlso Not (Not _allowNegative AndAlso value < 0) Then
                _value = value
                If Me._isInEditMode OrElse _supressFormating Then
                    MyBase.Text = _value.ToString
                Else
                    MyBase.Text = _value.ToString(_valueFormatStr)
                End If
                RaiseEvent OnDecimalValueChanged(Me, New EventArgs)
            End If
        End Set
    End Property

    <Category("Custom")> _
    <Description("Get decimal value of textbox.")> _
    <DefaultValue(False)> _
    Public Property SupressFormating() As Boolean
        Get
            Return _supressFormating
        End Get
        Set(ByVal value As Boolean)
            If value <> _supressFormating Then
                _supressFormating = value
                If _supressFormating Then
                    MyBase.Text = _value.ToString
                Else
                    MyBase.Text = _value.ToString(_valueFormatStr)
                End If
            End If
        End Set
    End Property

    <Category("Custom")> _
    <Description("Get integer value of textbox.")> _
    Public ReadOnly Property IntValue() As Integer
        Get
            Return CInt(_value)
        End Get
    End Property

    <Category("Custom")> _
    <Description("Number can be negative or not.")> _
    <DefaultValue(True)> _
    Public Property NegativeValue() As Boolean
        Get
            Return _allowNegative
        End Get
        Set(ByVal value As Boolean)
            If _allowNegative <> value Then
                _allowNegative = value
            End If
        End Set
    End Property

#End Region

#Region "Override events or methods"

    Protected Overrides Sub OnBackColorChanged(ByVal e As EventArgs)

        MyBase.OnBackColorChanged(e)

        If Me.ReadOnly Then
            If _keepBackColorWhenReadOnly Then
                MyBase.BackColor = _backColor
            Else
                MyBase.BackColor = _backColorWhenReadOnly
            End If
        Else
            MyBase.BackColor = _backColor
        End If

        Me.Invalidate()

    End Sub

    Protected Overrides Sub WndProc(ByRef m As Message)
        If m.Msg = WM_PASTE Then
            ' mouse paste
            Me.ClearSelection()
            SendKeys.Send(Clipboard.GetText())
            MyBase.OnTextChanged(EventArgs.Empty)
        ElseIf m.Msg = WM_COPY Then
            ' mouse copy
            Clipboard.SetText(Me.SelectedText)
        ElseIf m.Msg = WM_CUT Then
            ' mouse cut or ctrl+x shortcut
            Clipboard.SetText(Me.SelectedText)
            Me.ClearSelection()
            MyBase.OnTextChanged(EventArgs.Empty)
        ElseIf m.Msg = WM_CLEAR Then
            Me.ClearSelection()
            MyBase.OnTextChanged(EventArgs.Empty)
        Else
            MyBase.WndProc(m)
        End If
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, ByVal keyData As Keys) As Boolean

        If keyData = DirectCast(Shortcut.CtrlV, Keys) Then

            Me.ClearSelection()

            Dim text As String = Clipboard.GetText()
            For k As Integer = 0 To text.Length - 1 ' can not use SendKeys.Send
                SendCharKey(text(k))
            Next

            Return True

        ElseIf keyData = DirectCast(Shortcut.CtrlC, Keys) Then

            Clipboard.SetText(Me.SelectedText)
            Return True

        End If

        Return MyBase.ProcessCmdKey(msg, keyData)

    End Function

    ''' <summary>
    ''' repostion SelectionStart, recalculate SelectedLength
    ''' </summary>
    Protected Overrides Sub OnKeyPress(ByVal e As KeyPressEventArgs)

        MyBase.OnKeyPress(e)

        If Me.[ReadOnly] Then
            Exit Sub
        End If

        If e.KeyChar = Chr(13) OrElse e.KeyChar = Chr(3) OrElse e.KeyChar = Chr(22) _
            OrElse e.KeyChar = Chr(24) OrElse e.KeyChar = Chr(8) Then
            Exit Sub
        End If

        If e.KeyChar = _alternateDecimalSeparator Then e.KeyChar = _decimalSeparator

        If e.KeyChar = _decimalSeparator AndAlso (_decimalLength = 0 OrElse _
            Not MyBase.Text.IndexOf(_decimalSeparator) < 0) Then
            e.Handled = True
            Exit Sub
        End If

        If Not _allowNegative AndAlso e.KeyChar = _negativeSign Then
            e.Handled = True
            Exit Sub
        End If

        If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> _negativeSign AndAlso _
            e.KeyChar <> _decimalSeparator Then
            e.Handled = True
            Exit Sub
        End If

        If Char.IsDigit(e.KeyChar) AndAlso _decimalLength > 0 AndAlso _
            Not MyBase.Text.IndexOf(_decimalSeparator) < 0 AndAlso _
            Me.SelectionStart > MyBase.Text.IndexOf(_decimalSeparator) AndAlso _
            MyBase.Text.Length - MyBase.Text.IndexOf(_decimalSeparator) > _decimalLength Then
            e.Handled = True
            Exit Sub
        End If

        If MyBase.Text.Length >= _MaxValueLength AndAlso e.KeyChar <> _negativeSign Then
            e.Handled = True
            Exit Sub
        End If

        Dim isNegative As Boolean = (MyBase.Text.Length > 0 AndAlso MyBase.Text(0) = _negativeSign)

        If e.KeyChar = _negativeSign Then
            Dim selStart As Integer = Me.SelectionStart

            If Not isNegative Then
                MyBase.Text = _negativeSign + MyBase.Text
                Me.SelectionStart = selStart + 1
            Else
                MyBase.Text = MyBase.Text.Substring(1, MyBase.Text.Length - 1)
                If selStart >= 1 Then
                    Me.SelectionStart = selStart - 1
                Else
                    Me.SelectionStart = 0
                End If
            End If
            e.Handled = True
            ' minus(-) has been handled
            Exit Sub
        End If

        If e.KeyChar = _decimalSeparator Then

            If isNegative AndAlso Me.SelectionStart < 2 Then

                MyBase.Text = _negativeSign & "0" & _decimalSeparator & MyBase.Text.Substring(1)
                Me.SelectionStart = 3
                e.Handled = True
                Return

            ElseIf Not isNegative AndAlso Me.SelectionStart < 1 Then

                MyBase.Text = "0" & _decimalSeparator & MyBase.Text
                Me.SelectionStart = 2
                e.Handled = True
                Return

            End If

            Return

        End If

    End Sub

    ''' <summary>
    ''' reformat the base.Text
    ''' </summary>
    Protected Overrides Sub OnLeave(ByVal e As EventArgs)

        Dim oldValue As Double = _value

        _value = 0
        Double.TryParse(MyBase.Text.Trim, Globalization.NumberStyles.Any, _ApplicableCulture, _value)

        If _value <> oldValue Then RaiseEvent OnDecimalValueChanged(Me, EventArgs.Empty)

        If _supressFormating Then
            MyBase.Text = _value.ToString()
        Else
            MyBase.Text = _value.ToString(_valueFormatStr)
        End If

        MyBase.OnLeave(e)

        _isInEditMode = False

    End Sub

    ''' <summary>
    ''' reformat the base.Text
    ''' </summary>
    Protected Overrides Sub OnEnter(ByVal e As System.EventArgs)
        MyBase.Text = _value.ToString
        _isInEditMode = True
        MyBase.OnEnter(e)
    End Sub

    Protected Overrides Sub OnTextChanged(ByVal e As System.EventArgs)

        MyBase.OnTextChanged(e)

        Dim oldvalue As Double = _value
        _value = 0
        Double.TryParse(MyBase.Text.Trim, _value)

        If _value <> oldvalue Then RaiseEvent OnDecimalValueChanged(Me, EventArgs.Empty)

    End Sub

#End Region

#Region "Custom private methods"

    Private Sub SendCharKey(ByVal c As Char)

        Dim msg As New Message()

        msg.HWnd = Me.Handle
        msg.Msg = WM_CHAR
        msg.WParam = New IntPtr(Convert.ToInt32(c))
        msg.LParam = IntPtr.Zero

        MyBase.WndProc(msg)

    End Sub

    ''' <summary>
    ''' clear base.SelectedText
    ''' </summary>
    Private Sub ClearSelection()

        If Me.SelectionLength = 0 Then
            Return
        End If

        If Me.SelectedText.Length = MyBase.Text.Length Then
            MyBase.Text = 0.ToString()
            Return
        End If

        Dim selLength As Integer = Me.SelectedText.Length
        If Me.SelectedText.IndexOf(_decimalSeparator) >= 0 Then
            ' selected text contains dot(.), selected length minus 1
            selLength -= 1
        End If

        Me.SelectionStart += Me.SelectedText.Length
        ' after selected text
        Me.SelectionLength = 0

        MyBase.Text = MyBase.Text.Substring(0, Me.SelectionStart) & _
            MyBase.Text.Substring(Me.SelectionStart + Me.SelectionLength)

    End Sub

    Private Sub SetValueFormatStr()
        _valueFormatStr = GetValueFormatString(_decimalLength)
    End Sub

    Friend Shared Function GetValueFormatString(ByVal nDecimalLength As Integer) As String
        If nDecimalLength = 0 Then
            Return "##"
        Else
            Dim result As String = "##,0."
            For i As Integer = 1 To nDecimalLength
                result = result & "0"
            Next
            Return result
        End If
    End Function

#End Region

End Class
