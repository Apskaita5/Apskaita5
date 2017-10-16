Imports System.ComponentModel
Imports System.Drawing
Imports System.Windows.Forms


''' <summary>
''' Custom DateTime picker that is keyboard friendly.
''' </summary>
<DesignerCategory("")> _
<ToolboxItem(True), ToolboxBitmap(GetType(DateTimePicker))> _
<DefaultBindingProperty("Value")> _
<DefaultProperty("Value")> _
<DefaultEvent("OnValueChanged")> _
Public Class AccDatePicker

    Private _Calendar As CalendarToolStrip = Nothing
    Private _DropDown As ToolStripDropDown = Nothing
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
            Return ValueTextBox.Value
        End Get
        Set(ByVal value As Date)
            ValueTextBox.Value = value
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
            Return ValueTextBox.Format
        End Get
        Set(ByVal value As String)
            ValueTextBox.Format = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets whether the control is readonly.
    ''' </summary>
    <Category("Behavior")> _
    <Description("Gets or sets whether the control is readonly.")> _
    <Browsable(True)> _
    Public Property [ReadOnly]() As Boolean
        Get
            Return ValueTextBox.ReadOnly
        End Get
        Set(ByVal value As Boolean)
            ValueTextBox.ReadOnly = value
            DropdownButton.Enabled = Not value
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

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.


    End Sub


    Private Sub ValueTextBox_MouseClick(ByVal sender As Object, _
        ByVal e As System.Windows.Forms.MouseEventArgs) Handles ValueTextBox.MouseClick
        If Not ValueTextBox.ReadOnly AndAlso e.Button = MouseButtons.Middle Then ShowDropDown()
    End Sub

    Private Sub ValueTextBox_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles ValueTextBox.KeyDown
        If Not ValueTextBox.ReadOnly AndAlso e.KeyCode = Keys.Down Then
            e.Handled = True
            ShowDropDown()
        End If
    End Sub

    Private Sub DropdownButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles DropdownButton.Click
        If Not ValueTextBox.ReadOnly Then ShowDropDown()
        ValueTextBox.Focus()
    End Sub

    Private Sub ValueTextBox_OnValueChanged(ByVal sender As Object, _
        ByVal e As System.EventArgs) Handles ValueTextBox.OnValueChanged
        RaiseEvent OnValueChanged(Me, EventArgs.Empty)
    End Sub


    Protected Overrides Sub OnEnabledChanged(ByVal e As EventArgs)
        MyBase.OnEnabledChanged(e)
        Me.ValueTextBox.Enabled = Me.Enabled
        Me.DropdownButton.Enabled = Me.Enabled AndAlso Not Me.ValueTextBox.ReadOnly
    End Sub


    Private Sub ShowDropDown()

        If _Calendar Is Nothing Then
            _Calendar = New CalendarToolStrip()
            _Calendar.SetBoldedDates(_BoldedDates)
            _Calendar.SetMaxDate(_MaxDate)
            _Calendar.SetMinDate(_MinDate)
            _Calendar.SetShowWeekNumbers(_ShowWeekNumbers)
        End If
        If _DropDown Is Nothing Then

            _DropDown = New ToolStripDropDown()
            _DropDown.AutoSize = False
            _DropDown.GripStyle = SizeGripStyle.Show
            _DropDown.Margin = Padding.Empty
            _DropDown.Padding = Padding.Empty

            AddHandler _DropDown.Closed, AddressOf ToolStripDropDown_Closed
            AddHandler _DropDown.Opened, AddressOf ToolStripDropDown_Opened

        End If
        If Not _DropDown.Items.Contains(_Calendar) Then
            _DropDown.Items.Clear()
            _DropDown.Items.Add(_Calendar)
        End If

        _Calendar.SelectedValue = ValueTextBox.ParseDate()
        _Calendar.SelectionCanceled = True

        _DropDown.Show(Me, CalculatePoz()) 'New Point(0, Me.Height)

        _DropDown.Size = _Calendar.Size

    End Sub

    Private Function CalculatePoz() As Point

        Dim point As New Point(0, Me.Height)

        If (Me.PointToScreen(New Point(0, 0)).Y + Me.Height + Me._Calendar.Height) _
            > Screen.PrimaryScreen.WorkingArea.Height Then
            point.Y = -Me._Calendar.Height - 7
        End If

        Return point

    End Function

    Private Sub ToolStripDropDown_Closed(ByVal sender As Object, _
        ByVal e As ToolStripDropDownClosedEventArgs)

        If e.CloseReason = ToolStripDropDownCloseReason.ItemClicked _
            AndAlso Not _Calendar Is Nothing AndAlso Not _Calendar.SelectionCanceled Then
            Me.ValueTextBox.Value = _Calendar.SelectedValue
            If Not Me.ValueTextBox.Focused Then Me.ValueTextBox.Focus()
            Me.ValueTextBox.SelectAll()
        End If

    End Sub

    Private Sub ToolStripDropDown_Opened(ByVal sender As Object, ByVal e As EventArgs)
        _Calendar.Focus()
    End Sub

End Class
