Imports System.Windows.Forms
Imports System.ComponentModel
Imports System.Globalization

Friend Class AccDateTextBox
    Inherits TextBox

    Private Const WM_CHAR As Integer = &H102 ' char key message

    Private Const WM_CUT As Integer = &H300 ' mouse message in ContextMenu
    Private Const WM_COPY As Integer = &H301
    Private Const WM_PASTE As Integer = &H302
    Private Const WM_CLEAR As Integer = &H303

    Private ReadOnly VALID_CHARS As Char() = New Char() {"0", "1", "2", "3", "4", "5", "6", "7", "8", _
        "9", "/", "-", "+", ".", "\"}

    Private _Value As Date = Today
    Private _Format As String = "yyyy-MM-dd"

    Public Event OnValueChanged As EventHandler


    ''' <summary>
    ''' Gets or sets the selected date value.
    ''' </summary>
    <Category("Custom")> _
    <Description("Gets or sets the current date value.")> _
    <Bindable(True)> _
    <Browsable(True)> _
    Public Property Value() As Date
        Get
            Return _Value
        End Get
        Set(ByVal value As Date)
            If value.Date <> _Value.Date Then
                _Value = value
                ValueToText()
                RaiseEvent OnValueChanged(Me, EventArgs.Empty)
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a date format string. The default is yyyy-MM-dd. 
    ''' Set to empty string to use current culture format.
    ''' </summary>
    ''' <returns>
    ''' A string that represents a custom date format. The default is yyyy-MM-dd.
    ''' </returns>
    <Browsable(True)> _
    <Category("Custom")> _
    <Description("Gets or sets a date format string. The default is yyyy-MM-dd. Set to empty string to use current culture format."), _
    DefaultValue("yyyy-MM-dd")> _
    Public Property Format() As String
        Get
            Return _Format
        End Get
        Set(ByVal value As String)
            _Format = value
            ValueToText()
        End Set
    End Property


    Public Sub New()

        Me.Multiline = False
        Me.TextAlign = HorizontalAlignment.Left
        Me.MaxLength = 10

        ValueToText()

    End Sub


    Protected Overrides Sub WndProc(ByRef m As Message)
        If m.Msg = WM_PASTE Then
            ' mouse paste
            DoPaste()
        ElseIf m.Msg = WM_COPY Then
            ' mouse copy
            Clipboard.SetText(Me.Text)
        ElseIf m.Msg = WM_CUT Then
            ' mouse cut or ctrl+x shortcut
            Clipboard.SetText(Me.Text)
            MyBase.OnTextChanged(EventArgs.Empty)
        ElseIf m.Msg = WM_CLEAR Then
            ' do nothing
        Else
            MyBase.WndProc(m)
        End If
    End Sub

    Protected Overrides Function ProcessCmdKey(ByRef msg As Message, ByVal keyData As Keys) As Boolean

        If keyData = DirectCast(Shortcut.CtrlV, Keys) Then

            DoPaste()

            Return True

        ElseIf keyData = DirectCast(Shortcut.CtrlC, Keys) Then

            Clipboard.SetText(Me.Text)
            Return True

        End If

        Return MyBase.ProcessCmdKey(msg, keyData)

    End Function

    Private Sub DoPaste()

        Dim clipboardText As String = Clipboard.GetText()
        If clipboardText Is Nothing OrElse String.IsNullOrEmpty(clipboardText) Then Exit Sub

        Dim result As Date
        If TryParseDate(clipboardText, result) Then
            Value = result
        Else
            MsgBox(String.Format("Clipboard text ""{0}"" is not a valid date.", clipboardText), MsgBoxStyle.Exclamation, "Error")
        End If

    End Sub

    ''' <summary>
    ''' Filter valid chars.
    ''' </summary>
    Protected Overrides Sub OnKeyPress(ByVal e As KeyPressEventArgs)

        MyBase.OnKeyPress(e)

        If Me.[ReadOnly] Then
            Exit Sub
        End If

        If Not Char.IsControl(e.KeyChar) AndAlso Array.IndexOf(VALID_CHARS, e.KeyChar) < 0 Then
            e.Handled = True
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' reformat the base.Text
    ''' </summary>
    Protected Overrides Sub OnLeave(ByVal e As EventArgs)
        _Value = ParseDate()
        ValueToText()
        RaiseEvent OnValueChanged(Me, EventArgs.Empty)
        MyBase.OnLeave(e)
    End Sub

    Private Delegate Sub SelectAllDelegate()

    Protected Overrides Sub OnEnter(ByVal e As EventArgs)
        BeginInvoke(New SelectAllDelegate(AddressOf Me.SelectAll))
    End Sub




    Private Sub ValueToText()
        If _Format Is Nothing OrElse String.IsNullOrEmpty(_Format.Trim) Then
            Me.Text = _Value.ToString()
        Else
            Me.Text = _Value.ToString(_Format)
        End If
    End Sub

    Friend Function ParseDate() As Date
        Return ParseDate(Me.Text)
    End Function

    Private Function ParseDate(ByVal dateString As String) As Date

        Dim result As Date

        If TryParseDate(dateString, result) Then
            Return result.Date
        End If

        Return _Value.Date

    End Function

    Private Function TryParseDate(ByVal dateString As String, ByRef result As Date) As Boolean

        If dateString Is Nothing OrElse String.IsNullOrEmpty(dateString.Trim) Then Return False

        Dim currentFormat As String = _Format
        If currentFormat Is Nothing OrElse String.IsNullOrEmpty(currentFormat.Trim) _
            OrElse currentFormat.Trim.ToLower = "d" Then
            currentFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern
        End If

        Dim day As Integer = 0

        If Date.TryParseExact(dateString, currentFormat, CultureInfo.InvariantCulture, _
            DateTimeStyles.None, result) Then
            Return True
        ElseIf Date.TryParseExact(dateString, New String() {"yyyy-MM-dd", "yyyyMMdd", "yyMMdd", _
            "yyyy/MM/dd", "yy/MM/dd", "yyyy.MM.dd", "yy.MM.dd"}, _
            CultureInfo.InvariantCulture, DateTimeStyles.None, result) Then
            Return True
        ElseIf Integer.TryParse(dateString, day) Then
            If dateString.Trim.Substring(0, 1) = "-" Then
                Try
                    result = New Date(Today.AddMonths(-1).Year, Today.AddMonths(-1).Month, -day)
                    Return True
                Catch ex As Exception
                End Try
            ElseIf dateString.Trim.Substring(0, 1) = "+" Then
                Try
                    result = New Date(Today.AddMonths(1).Year, Today.AddMonths(1).Month, day)
                    Return True
                Catch ex As Exception
                End Try
            Else
                Try
                    result = New Date(Today.Year, Today.Month, day)
                    Return True
                Catch ex As Exception
                End Try
            End If

        ElseIf dateString.Split(New Char() {"."c, "-"c, "/"c}, StringSplitOptions.RemoveEmptyEntries).Length = 2 Then
            Dim monthPart As String = dateString.Split(New Char() {"."c, "-"c, "/"c}, StringSplitOptions.RemoveEmptyEntries)(0).Trim()
            Dim dayPart As String = dateString.Split(New Char() {"."c, "-"c, "/"c}, StringSplitOptions.RemoveEmptyEntries)(1).Trim()
            Dim dayInt As Integer = 0
            Dim monthInt As Integer = 0
            If Integer.TryParse(monthPart, monthInt) Then
                If Integer.TryParse(dayPart, dayInt) Then
                    Try
                        result = New Date(Today.Year, monthInt, dayInt)
                        Return True
                    Catch ex As Exception
                    End Try
                End If
            End If
        End If

        Return False

    End Function

End Class
