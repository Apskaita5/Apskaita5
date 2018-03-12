Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Globalization


''' <summary>
''' Custom DateTime picker that is keyboard friendly.
''' </summary>
<DesignerCategory("")> _
<ToolboxItem(True), ToolboxBitmap(GetType(DateTimePicker))> _
<DefaultBindingProperty("Value")> _
<DefaultProperty("Value")> _
<DefaultEvent("OnValueChanged")> _
Public Class AccDatePicker
    Inherits AccComboBoxBase

    Private ReadOnly VALID_CHARS As Char() = New Char() {"0", "1", "2", "3", "4", "5", "6", "7", "8", _
        "9", "/", "-", "+", ".", "\"}

    Private _Calendar As CalendarToolStrip = Nothing

    Private _Value As Date = Today
    Private _Format As String = "yyyy-MM-dd"
    Private _BoldedDates As Date() = Nothing
    Private _MaxDate As Date = New Date(9998, 12, 31)
    Private _MinDate As Date = New Date(1753, 1, 1)
    Private _ShowWeekNumbers As Boolean = True

    Public Event OnValueChanged As EventHandler


    ''' <summary>
    ''' Gets or sets the selected date value.
    ''' </summary>
    <Category("Behavior")> _
    <Description("Gets or sets the current date value.")> _
    <Bindable(True)> _
    <Browsable(True)> _
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Public Property Value() As Date
        Get
            Return _Value.Date
        End Get
        Set(ByVal value As Date)
            If value.Date <> _Value.Date Then
                _Value = value.Date
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
    <Category("Appearance")> _
    <Description("Gets or sets a date format string. The default is yyyy-MM-dd. Set to empty string to use current culture format."), _
    DefaultValue("yyyy-MM-dd")> _
    <Localizable(True)> _
    Public Property Format() As String
        Get
            Return _Format
        End Get
        Set(ByVal value As String)
            _Format = value
            ValueToText()
        End Set
    End Property

    ''' <summary>
    ''' Using this property, you can assign an array of bold dates. When you assign an array of dates, the existing dates are first cleared.
    ''' </summary>
    <Browsable(True)> _
    <Category("Appearance")> _
    <Description("Using this property, you can assign an array of bold dates. When you assign an array of dates, the existing dates are first cleared.")> _
    Public Property BoldedDates() As Date()
        Get
            Return _BoldedDates
        End Get
        Set(ByVal value As Date())
            _BoldedDates = value
            If Not _Calendar Is Nothing Then
                _Calendar.SetBoldedDates(value)
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the minimum allowable date.
    ''' </summary>
    <Browsable(True)> _
    <Category("Behavior")> _
    <Description("Gets or sets the minimum allowable date.")> _
    Public Property MinDate() As Date
        Get
            Return _MinDate
        End Get
        Set(ByVal value As Date)
            _MinDate = value
            If Not _Calendar Is Nothing Then
                _Calendar.SetMinDate(value)
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the maximum allowable date.
    ''' </summary>
    <Browsable(True)> _
    <Category("Behavior")> _
    <Description("Gets or sets the maximum allowable date.")> _
    Public Property MaxDate() As Date
        Get
            Return _MaxDate
        End Get
        Set(ByVal value As Date)
            _MaxDate = value
            If Not _Calendar Is Nothing Then
                _Calendar.SetMaxDate(value)
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a value indicating whether the month calendar control displays week numbers (1-52) to the left of each row of days.
    ''' </summary>
    <Browsable(True)> _
    <Category("Appearance")> _
    <Description("Gets or sets a value indicating whether the month calendar control displays week numbers (1-52) to the left of each row of days.")> _
    Public Property ShowWeekNumbers() As Boolean
        Get
            Return _ShowWeekNumbers
        End Get
        Set(ByVal value As Boolean)
            _ShowWeekNumbers = value
            If Not _Calendar Is Nothing Then
                _Calendar.SetShowWeekNumbers(value)
            End If
        End Set
    End Property


    Public Sub New()

        Me.TextAlign = HorizontalAlignment.Left

        ValueToText()

    End Sub


    ''' <summary>
    ''' Force the control to update the value from the control text, 
    ''' in case the entered value is required before the control looses focus.
    ''' </summary>
    Public Sub RefreshValue()
        Me.Value = ParseDate()
    End Sub


    Protected Overrides Function GetMaxTextLength() As Integer
        Return 10
    End Function

    Protected Overrides Function GetButtonImage() As Image
        Return My.Resources.calendar_x16
    End Function

    Protected Overrides Sub OnPaste()

        Dim clipboardText As String = Clipboard.GetText()
        If clipboardText Is Nothing OrElse String.IsNullOrEmpty(clipboardText) Then Exit Sub

        Dim result As Date
        If TryParseDate(clipboardText, result) Then
            Value = result
        Else
            MsgBox(String.Format("Clipboard text ""{0}"" is not a valid date.", clipboardText), MsgBoxStyle.Exclamation, "Error")
        End If

    End Sub

    Protected Overrides Function AcceptPaste() As Boolean
        Return True
    End Function

    Protected Overridable Sub OnCopy()
        Clipboard.SetText(Me.Text)
    End Sub

    Protected Overrides Sub OnCut()
        Clipboard.SetText(Me.Text)
    End Sub

    Protected Overrides Function GetToolStripControlHost() As ToolStripControlHost

        If _Calendar Is Nothing Then
            _Calendar = New CalendarToolStrip()
            _Calendar.SetBoldedDates(_BoldedDates)
            _Calendar.SetMaxDate(_MaxDate)
            _Calendar.SetMinDate(_MinDate)
            _Calendar.SetShowWeekNumbers(_ShowWeekNumbers)
        End If

        Return _Calendar

    End Function

    Protected Overrides Sub BeforeDropDownOpen()
        _Calendar.SelectedValue = ParseDate()
        _Calendar.SelectionCanceled = True
    End Sub

    Protected Overrides Sub AfterDropDownOpen()
        _Calendar.Focus()
    End Sub

    Protected Overrides Sub AfterDropDownClosed(ByVal reason As ToolStripDropDownCloseReason)
        If reason = ToolStripDropDownCloseReason.ItemClicked _
            AndAlso Not _Calendar Is Nothing AndAlso Not _Calendar.SelectionCanceled Then
            Me.Value = _Calendar.SelectedValue
            If Not Me.Focused Then Me.Focus()
            Me.SelectAll()
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
        Me.Value = ParseDate()
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
