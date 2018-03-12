Imports System.Windows.Forms

''' <summary>
''' A control that holds a CalculatorUserControl and could be added to a ToolStripDropDown. 
''' </summary>
''' <remarks></remarks>
Public Class CalculatorToolStrip
    Inherits ToolStripControlHost

    Private _SelectedValue As Double = 0.0
    Private _SelectionCanceled As Boolean = False

    ''' <summary>
    ''' Gets a value that has been selected by the user.
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly Property SelectedValue() As Double
        Get
            Return _SelectedValue
        End Get
    End Property

    ''' <summary>
    ''' Indicates whether the user has canceled the selection.
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly Property SelectionCanceled() As Boolean
        Get
            Return _SelectionCanceled
        End Get
    End Property


    ' Call the base constructor passing in a CalculatorUserControl instance.
    Public Sub New(ByVal calculator As CalculatorUserControl)
        MyBase.New(calculator)
        Me.AutoSize = False
        Me.Padding = Padding.Empty
        Me.Margin = Padding.Empty
        Me.Size = calculator.Size
        calculator.Dock = DockStyle.Fill
    End Sub

    ' disable default constructor access
    Private Sub New(ByVal c As Control)
        MyBase.New(c)
    End Sub


    ''' <summary>
    ''' Sets ithe initial value of the calculator.
    ''' </summary>
    ''' <param name="value">a value to set</param>
    ''' <remarks></remarks>
    Friend Sub SetSelectedValue(ByVal value As Double)
        If Not Me.Control Is Nothing Then
            DirectCast(Me.Control, CalculatorUserControl).SetValue(value)
        End If
    End Sub


    Private Sub OnCalculatorClosing(ByVal sender As Object, _
        ByVal e As CalculatorUserControl.CalculatorClosingEventArgs)
        _SelectedValue = e.Result
        _SelectionCanceled = e.IsCanceled
        If Not Me.Control Is Nothing Then
            DirectCast(Me.Control, CalculatorUserControl).ClearAll()
        End If
        DirectCast(Me.Owner, ToolStripDropDown).Close(ToolStripDropDownCloseReason.ItemClicked)
    End Sub


    ' Subscribe and unsubscribe the control events you wish to expose.
    Protected Overrides Sub OnSubscribeControlEvents(ByVal c As Control)
        ' Call the base so the base events are connected.
        MyBase.OnSubscribeControlEvents(c)

        ' Cast the control to a CalculatorUserControl control.
        Dim calculator As CalculatorUserControl = DirectCast(c, CalculatorUserControl)

        ' Add the event.
        AddHandler calculator.CalculatorClosing, AddressOf OnCalculatorClosing

    End Sub

    Protected Overrides Sub OnUnsubscribeControlEvents(ByVal c As Control)
        ' Call the base method so the basic events are unsubscribed.
        MyBase.OnUnsubscribeControlEvents(c)

        ' Cast the control to a CalculatorUserControl control.
        Dim calculator As CalculatorUserControl = DirectCast(c, CalculatorUserControl)

        ' Remove the event.
        RemoveHandler calculator.CalculatorClosing, AddressOf OnCalculatorClosing

    End Sub


    'Protected Overrides Sub OnBoundsChanged()
    '    MyBase.OnBoundsChanged()
    '    If Not Control Is Nothing Then
    '        DirectCast(Control, CalculatorUserControl).Size = Me.Size
    '    End If
    'End Sub

End Class
