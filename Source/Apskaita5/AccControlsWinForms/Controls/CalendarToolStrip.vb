
Imports System.Windows.Forms
Imports System.Drawing

''' <summary>
''' A control that holds a MonthCalendar control and could be added to a ToolStripDropDown. 
''' </summary>
''' <remarks></remarks>
Public Class CalendarToolStrip
    Inherits ToolStripControlHost

    Private _SelectedValue As Date = Date.Today
    Private _SelectionCanceled As Boolean = False

    ''' <summary>
    ''' Gets or sets the selected value.
    ''' </summary>
    ''' <remarks></remarks>
    Public Property SelectedValue() As Date
        Get
            Return _SelectedValue
        End Get
        Set(ByVal value As Date)
            If Not Me.Control Is Nothing Then
                DirectCast(Me.Control, MonthCalendar).SelectionRange = New SelectionRange(value, value)
                _SelectedValue = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Indicates whether the user has canceled the selection.
    ''' </summary>
    ''' <remarks></remarks>
    Public Property SelectionCanceled() As Boolean
        Get
            Return _SelectionCanceled
        End Get
        Set(ByVal value As Boolean)
            _SelectionCanceled = True
        End Set
    End Property


    ' Call the base constructor instance.
    Public Sub New()

        MyBase.New(New MonthCalendar)

        Me.AutoSize = False
        Me.Padding = Padding.Empty
        Me.Margin = Padding.Empty

        Dim calendar As MonthCalendar = DirectCast(Me.Control, MonthCalendar)
        calendar.AutoSize = True
        calendar.Padding = Padding.Empty
        calendar.Margin = Padding.Empty
        calendar.CausesValidation = False
        calendar.FirstDayOfWeek = Day.Monday
        calendar.MaxSelectionCount = 1
        calendar.ShowToday = True
        calendar.ShowTodayCircle = True
        calendar.ShowWeekNumbers = True

    End Sub

    ' disable default constructor access
    Private Sub New(ByVal c As Control)
        MyBase.New(c)
    End Sub


    Friend Sub SetBoldedDates(ByVal dates As Date())
        If Not Me.Control Is Nothing Then
            DirectCast(Me.Control, MonthCalendar).BoldedDates = dates
        End If
    End Sub

    Friend Sub SetMinDate(ByVal minDate As Date)
        If Not Me.Control Is Nothing Then
            DirectCast(Me.Control, MonthCalendar).MinDate = minDate
        End If
    End Sub

    Friend Sub SetMaxDate(ByVal maxDate As Date)
        If Not Me.Control Is Nothing Then
            DirectCast(Me.Control, MonthCalendar).MaxDate = maxDate
        End If
    End Sub

    Friend Sub SetShowWeekNumbers(ByVal showWeekNumbers As Boolean)
        If Not Me.Control Is Nothing Then
            DirectCast(Me.Control, MonthCalendar).ShowWeekNumbers = showWeekNumbers
        End If
    End Sub


    Private Sub OnDateSelected(ByVal sender As Object, ByVal e As DateRangeEventArgs)
        _SelectedValue = DirectCast(Me.Control, MonthCalendar).SelectionStart
        _SelectionCanceled = False
        DirectCast(Me.Owner, ToolStripDropDown).Close(ToolStripDropDownCloseReason.ItemClicked)
    End Sub

    Private Sub OnCalendarKeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            _SelectedValue = DirectCast(Me.Control, MonthCalendar).SelectionStart
            _SelectionCanceled = False
            DirectCast(Me.Owner, ToolStripDropDown).Close(ToolStripDropDownCloseReason.ItemClicked)
        ElseIf e.KeyCode = Keys.Escape Then
            _SelectionCanceled = True
            DirectCast(Me.Owner, ToolStripDropDown).Close(ToolStripDropDownCloseReason.ItemClicked)
            e.Handled = True
        End If
    End Sub


    ' Subscribe and unsubscribe the control events you wish to expose.
    Protected Overrides Sub OnSubscribeControlEvents(ByVal c As Control)
        ' Call the base so the base events are connected.
        MyBase.OnSubscribeControlEvents(c)

        ' Cast the control to a CalculatorUserControl control.
        Dim calendar As MonthCalendar = DirectCast(c, MonthCalendar)

        ' Add the event.
        AddHandler calendar.DateSelected, AddressOf OnDateSelected
        AddHandler calendar.KeyDown, AddressOf OnCalendarKeyDown

    End Sub

    Protected Overrides Sub OnUnsubscribeControlEvents(ByVal c As Control)
        ' Call the base method so the basic events are unsubscribed.
        MyBase.OnUnsubscribeControlEvents(c)

        ' Cast the control to a CalculatorUserControl control.
        Dim calendar As MonthCalendar = DirectCast(c, MonthCalendar)

        ' Remove the event.
        RemoveHandler calendar.DateSelected, AddressOf OnDateSelected
        RemoveHandler calendar.KeyDown, AddressOf OnCalendarKeyDown

    End Sub

End Class
