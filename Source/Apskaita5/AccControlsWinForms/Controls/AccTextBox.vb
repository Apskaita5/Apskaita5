Imports System.Windows.Forms
Imports System.ComponentModel
Imports System.Drawing

''' <summary>
''' A textbox that only accepts numeric input.
''' </summary>
''' <remarks></remarks>
<ToolboxItem(True)> _
<ToolboxBitmap(GetType(TextBox))> _
<System.ComponentModel.DefaultBindingProperty("DecimalValue")> _
Public Class AccTextBox
    Inherits AccComboBoxBase

    Private Const _MaxDecimalLength As Integer = 10 ' max dot length
    Private Const _MaxValueLength As Integer = 27 ' decimal can be 28 bits.

    Private Const WM_CHAR As Integer = &H102 ' char key message

    Private _decimalLength As Integer = 2
    Private _allowNegative As Boolean = True
    Private _valueFormatStr As String = String.Empty
    Private _value As Double = 0

    Private _ApplicableCulture As System.Globalization.CultureInfo
    Private _decimalSeparator As Char = "."c
    Private _alternateDecimalSeparator As Char = "."c
    Private _negativeSign As Char = "-"c
    Private _isInEditMode As Boolean = False

    Private _Calculator As CalculatorToolStrip = Nothing

    Public Event OnDecimalValueChanged As EventHandler


    <Category("Behavior")> _
    <Description("Get or set decimal precision. (0 - integer)")> _
    <Browsable(True), EditorBrowsable(EditorBrowsableState.Always)> _
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

                If _isInEditMode Then
                    MyBase.Text = _value.ToString
                Else
                    MyBase.Text = _value.ToString(_valueFormatStr)
                End If

                RaiseEvent OnDecimalValueChanged(Me, New EventArgs)

            End If
        End Set
    End Property

    <Description("Get decimal value of textbox.")> _
    <DefaultValue(GetType(Double), "0")> _
    <Bindable(True)> _
    <Browsable(True), EditorBrowsable(EditorBrowsableState.Always)> _
    Public Property DecimalValue() As Double
        Get
            Return _value
        End Get
        Set(ByVal value As Double)

            If value <> _value AndAlso Not (Not _allowNegative AndAlso value < 0) Then
                _value = value
                If Me._isInEditMode Then
                    MyBase.Text = _value.ToString
                Else
                    MyBase.Text = _value.ToString(_valueFormatStr)
                End If
                RaiseEvent OnDecimalValueChanged(Me, New EventArgs)
            End If

        End Set
    End Property

    <Category("Behavior")> _
    <Description("Gets or sets whether a negative value could be entered.")> _
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


    Public Sub New()

        MyBase.New()

        MyBase.ButtonVisible = False
        MyBase.TextAlign = HorizontalAlignment.Right

        _ApplicableCulture = System.Threading.Thread.CurrentThread.CurrentCulture
        _decimalSeparator = _ApplicableCulture.NumberFormat.NumberDecimalSeparator(0)
        If _decimalSeparator = "," Then
            _alternateDecimalSeparator = "."
        Else
            _alternateDecimalSeparator = ","
        End If
        _negativeSign = _ApplicableCulture.NumberFormat.NegativeSign(0)

        Me.SetValueFormatStr()

        MyBase.Text = _value.ToString(_valueFormatStr)

    End Sub


    Protected Overrides Function GetMaxTextLength() As Integer
        Return 50
    End Function

    Protected Overrides Function GetButtonImage() As Image
        Return My.Resources.calculator_x16
    End Function

    Protected Overrides Sub OnPaste()
        Me.ClearSelection()
        ' SendKeys.Send(Clipboard.GetText())
        For Each c As Char In Clipboard.GetText()
            SendCharKey(c)
        Next
        MyBase.OnTextChanged(EventArgs.Empty)
    End Sub

    Protected Overrides Function AcceptPaste() As Boolean
        Return True
    End Function

    Protected Overrides Sub OnCopy()
        If Me.SelectedText Is Nothing OrElse String.IsNullOrEmpty(Me.SelectedText.Trim) Then Exit Sub
        Clipboard.SetText(Me.SelectedText)
    End Sub

    Protected Overrides Sub OnCut()
        If Me.SelectedText Is Nothing OrElse String.IsNullOrEmpty(Me.SelectedText.Trim) Then Exit Sub
        Clipboard.SetText(Me.SelectedText)
        Me.ClearSelection()
        MyBase.OnTextChanged(EventArgs.Empty)
    End Sub

    Protected Overrides Function AcceptCut() As Boolean
        Return True
    End Function

    Protected Overrides Sub OnClear()
        Me.ClearSelection()
        MyBase.OnTextChanged(EventArgs.Empty)
    End Sub

    Protected Overrides Function AcceptClear() As Boolean
        Return True
    End Function

    Protected Overrides Function GetToolStripControlHost() As ToolStripControlHost
        If _Calculator Is Nothing Then
            _Calculator = New CalculatorToolStrip(New CalculatorUserControl())
        End If
        Return _Calculator
    End Function

    Protected Overrides Sub BeforeDropDownOpen()

        Dim currentValue As Double = 0.0
        Double.TryParse(MyBase.Text.Trim, Globalization.NumberStyles.Any, _ApplicableCulture, currentValue)

        _Calculator.SetSelectedValue(currentValue)

    End Sub

    Protected Overrides Sub AfterDropDownOpen()
        _Calculator.Focus()
    End Sub

    Protected Overrides Sub AfterDropDownClosed(ByVal reason As ToolStripDropDownCloseReason)

        If reason = ToolStripDropDownCloseReason.ItemClicked _
            AndAlso Not _Calculator Is Nothing AndAlso Not _Calculator.SelectionCanceled Then

            If Not MyBase.Focused Then MyBase.Focus()

            _value = _Calculator.SelectedValue
            MyBase.Text = _value.ToString
            _isInEditMode = True

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

        ElseIf keyData = Keys.Down Then

            ShowDropDown()
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

        MyBase.Text = _value.ToString(_valueFormatStr)

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

End Class
