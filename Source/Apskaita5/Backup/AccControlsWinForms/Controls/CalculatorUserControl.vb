Imports System.Windows.Forms

Public Class CalculatorUserControl

    Private ReadOnly ValidChars As Char() = New Char() {"0"c, "1"c, "2"c, "3"c, "4"c, "5"c, _
        "6"c, "7"c, "8"c, "9"c, "0"c, "."c, "+"c, "-"c, "*"c, "/"c, "%"c}
    Private Const SignChar As Char = "s"c
    Private Const SqrtChar As Char = "r"c
    Private Const PercentageChar As Char = "%"c
    Private Const ReverseDivideChar As Char = "d"c
    Private Const ResultChar As Char = "="c
    Private Const BackSpaceChar As Char = "b"c
    Private Const ClearChar As Char = "c"c
    Private Const ClearAllChar As Char = "a"c

    Public Delegate Sub CalculatorClosingEventHandler(ByVal sender As Object, ByVal e As CalculatorClosingEventArgs)
    Public Event CalculatorClosing As CalculatorClosingEventHandler

    Public Class CalculatorClosingEventArgs
        Inherits EventArgs

        Private ReadOnly _isCanceled As Boolean = True
        Private ReadOnly _Result As Double = 0


        Public ReadOnly Property IsCanceled() As Boolean
            Get
                Return _isCanceled
            End Get
        End Property

        Public ReadOnly Property Result() As Double
            Get
                Return _Result
            End Get
        End Property


        Public Sub New()
            _isCanceled = True
            _Result = Double.NaN
        End Sub

        Public Sub New(ByVal value As Double)
            _isCanceled = False
            _Result = value
        End Sub

    End Class

    Protected Overridable Sub OnCalculatorClosing(ByVal e As CalculatorClosingEventArgs)
        RaiseEvent CalculatorClosing(Me, e)
    End Sub

    Private Sub OkButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles OkButton.Click
        OnCalculatorClosing(New CalculatorClosingEventArgs(GetCurrentValue()))
    End Sub

    Private Sub DiscardButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles DiscardButton.Click
        OnCalculatorClosing(New CalculatorClosingEventArgs())
    End Sub


    Private _isDialog As Boolean = True

    Public Property IsDialog() As Boolean
        Get
            Return _isDialog
        End Get
        Set(ByVal value As Boolean)
            If value <> _isDialog Then
                If value Then
                    OkButton.Visible = True
                    DiscardButton.Visible = True
                Else
                    OkButton.Visible = False
                    DiscardButton.Visible = False
                End If
                _isDialog = value
            End If
        End Set
    End Property


    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.SetStyle(ControlStyles.ContainerControl, False)
        Me.SetStyle(ControlStyles.Selectable, True)

    End Sub


    Private Sub Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles Button1.Click, Button2.Click, Button3.Click, Button4.Click, Button5.Click, _
        Button6.Click, Button7.Click, Button8.Click, Button9.Click, Button0.Click, _
        SeparatorButton.Click, SumButton.Click, DivideButton.Click, MultiplyButton.Click, _
        PercentageButton.Click, MinusButton.Click

        Dim btn As Button = Nothing
        Try
            btn = DirectCast(sender, Button)
        Catch ex As Exception
        End Try
        If btn Is Nothing Then Exit Sub

        ProcessChar(btn.Text)

    End Sub

    Private Sub SignButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles SignButton.Click
        ProcessChar(SignChar)
    End Sub

    Private Sub SqrtButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles SqrtButton.Click
        ProcessChar(SqrtChar)
    End Sub

    Private Sub ReverseDivideButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles ReverseDivideButton.Click
        ProcessChar(ReverseDivideChar)
    End Sub

    Private Sub ResultButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles ResultButton.Click
        ProcessChar(ResultChar)
    End Sub

    Private Sub BackSpaceButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles BackSpaceButton.Click
        ProcessChar(BackSpaceChar)
    End Sub

    Private Sub CButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) _
        Handles CButton.Click
        ProcessChar(ClearChar)
    End Sub

    Private Sub CeButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CeButton.Click
        ProcessChar(ClearAllChar)
    End Sub


    Private Sub CalculatorUserControl_KeyDown(ByVal sender As Object, _
        ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        ProcessKeyEventArgsInt(e)
    End Sub

    Private Sub ResultTextBox_KeyDown(ByVal sender As Object, _
        ByVal e As System.Windows.Forms.KeyEventArgs) Handles ResultTextBox.KeyDown
        ProcessKeyEventArgsInt(e)
    End Sub

    Friend Sub ProcessKeyEventArgsInt(ByVal e As KeyEventArgs)

        If e.Control AndAlso e.KeyCode = Keys.C Then
            Clipboard.SetText(GetCurrentValue().ToString())
            e.SuppressKeyPress = True
        ElseIf e.Control AndAlso e.KeyCode = Keys.P Then

            e.SuppressKeyPress = True

            Dim clipboardString As String = Clipboard.GetText()
            If clipboardString Is Nothing OrElse String.IsNullOrEmpty(clipboardString.Trim) Then Exit Sub
            Dim clipboardValue As Double = Double.NaN
            Try
                clipboardValue = Double.Parse(clipboardString)
            Catch ex As Exception
                Exit Sub
            End Try
            If IsValueZero(clipboardValue) Then Exit Sub

            If restartText Then
                lastResult = GetCurrentValue()
                SetCurrentValue(clipboardValue)
                restartText = False
            Else
                SetCurrentValue(clipboardValue)
            End If

        End If

    End Sub

    Private Sub CalculatorUserControl_KeyPress(ByVal sender As Object, _
        ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Me.KeyPress
        ProcessKeyPressEventArgs(e)
    End Sub

    Private Sub ResultTextBox_KeyPress(ByVal sender As Object, _
        ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles ResultTextBox.KeyPress
        ProcessKeyPressEventArgs(e)
    End Sub

    Friend Sub ProcessKeyPressEventArgs(ByVal e As KeyPressEventArgs)

        If Not Array.IndexOf(ValidChars, e.KeyChar) < 0 Then
            ProcessChar(e.KeyChar)
            e.Handled = True
        ElseIf e.KeyChar = ","c Then
            ProcessChar("."c)
            e.Handled = True
        ElseIf e.KeyChar = ChrW(Keys.Back) Then
            ProcessChar(BackSpaceChar)
            e.Handled = True

        ElseIf e.KeyChar = ChrW(Keys.Enter) Then
            ProcessChar(ResultChar)
            If _isDialog Then OkButton.PerformClick()
            e.Handled = True

        ElseIf e.KeyChar = ChrW(Keys.Escape) Then
            If _isDialog Then
                DiscardButton.PerformClick()
            Else
                ProcessChar(ClearChar)
            End If
            e.Handled = True

        End If

    End Sub


    Public Sub SetValue(ByVal value As Double)
        If restartText Then
            lastResult = GetCurrentValue()
            SetCurrentValue(value)
            restartText = False
        Else
            SetCurrentValue(value)
        End If
    End Sub

    Public Sub ClearAll()
        ProcessChar(ClearAllChar)
    End Sub


    Private Const EmptyOperationChar As Char = "~"c
    'Intermediate Result buffer
    Private lastResult As Double = 0.0
    'To hold the active operation
    Private activeOperator As Char = EmptyOperationChar
    'Boolean flag to check whether to add the key-ins or to restart the key-ins
    Private restartText = True

    Private Sub ProcessChar(ByVal inputChar As Char)

        Select Case inputChar

            Case BackSpaceChar
                If ResultTextBox.Text.Length > 0 Then
                    ResultTextBox.Text = ResultTextBox.Text.Substring(0, ResultTextBox.Text.Length - 1)
                    If ResultTextBox.Text.Length = 0 Then
                        ResultTextBox.Text = "0"
                    End If
                End If

            Case "0"c, "1"c, "2"c, "3"c, "4"c, "5"c, "6"c, "7"c, "8"c, "9"c, "0"c
                If Not restartText Then
                    ResultTextBox.Text = ResultTextBox.Text & inputChar
                Else
                    lastResult = GetCurrentValue()
                    ResultTextBox.Text = inputChar
                    restartText = False
                End If

            Case "+"c, "-"c, "*"c, "/"c, PercentageChar
                If activeOperator <> EmptyOperationChar Then
                    ProcessResult()
                End If
                activeOperator = inputChar
                restartText = True

            Case "."c
                If ResultTextBox.Text.IndexOf(".") < 0 Then
                    ResultTextBox.Text = ResultTextBox.Text & "."
                End If

            Case SqrtChar
                SetCurrentValue(Math.Sqrt(GetCurrentValue()))
                restartText = True

            Case ReverseDivideChar
                If Not IsCurrentValueZero() Then
                    SetCurrentValue(1.0 / GetCurrentValue())
                Else
                    MsgBox("Divide By Zero", MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, "Error")
                End If
                restartText = True

            Case SignChar
                SetCurrentValue(-GetCurrentValue())

            Case ResultChar
                ProcessResult()

            Case ClearChar
                SetCurrentValue(0.0)
                restartText = True

            Case ClearAllChar
                SetCurrentValue(0.0)
                lastResult = 0.0
                activeOperator = EmptyOperationChar
                restartText = True

                ' Unknown Keycode
            Case Else
                MsgBox(String.Format("Key char ""{0}"" is not recognized.", inputChar), _
                    MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, "Error")
        End Select

        Me.Focus()

    End Sub

    Private Sub ProcessResult()
        Select Case activeOperator
            Case "+"c
                SetCurrentValue(lastResult + GetCurrentValue())
            Case "/"c
                If Not IsCurrentValueZero() Then
                    SetCurrentValue(lastResult / GetCurrentValue())
                Else
                    MsgBox("Divide By Zero", MsgBoxStyle.OkOnly + MsgBoxStyle.Critical, "Error")
                End If
            Case "%"c
                SetCurrentValue(lastResult Mod GetCurrentValue())
            Case "*"c
                SetCurrentValue(lastResult * GetCurrentValue())
            Case "-"c
                SetCurrentValue(lastResult - GetCurrentValue())
        End Select
        lastResult = GetCurrentValue()
        activeOperator = EmptyOperationChar
        RestartText = True
    End Sub


    Private Function GetCurrentValue() As Double

        If String.IsNullOrEmpty(ResultTextBox.Text.Trim) Then Return 0

        Dim strValue As String = String.Empty

        If ResultTextBox.Text.Trim.EndsWith(".", StringComparison.InvariantCulture) Then
            strValue = ResultTextBox.Text.Trim.Substring(0, ResultTextBox.Text.Trim.Length - 1)
        Else
            strValue = ResultTextBox.Text.Trim
        End If

        Return Double.Parse(strValue, System.Globalization.CultureInfo.InvariantCulture)

    End Function

    Private Sub SetCurrentValue(ByVal value As Double)
        ResultTextBox.Text = value.ToString(System.Globalization.CultureInfo.InvariantCulture)
    End Sub

    Private Function IsCurrentValueZero() As Boolean
        Return IsValueZero(GetCurrentValue())
    End Function

    Private Shared Function IsValueZero(ByVal val As Double) As Boolean
        Return Not (val > Double.Epsilon OrElse val < -Double.Epsilon)
    End Function

End Class
