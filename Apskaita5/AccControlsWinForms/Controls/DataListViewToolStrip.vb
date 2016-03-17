Imports System.Windows.Forms

''' <summary>
''' A control that holds a InfoListControl and could be added to a ToolStripDropDown. 
''' </summary>
''' <remarks></remarks>
Public Class DataListViewToolStrip
    Inherits ToolStripControlHost

    Private _MinDropDownWidth As Integer = 100
    Private _DropDownHeight As Integer = 100
    Private _SelectedValue As Object = Nothing
    Private _SelectionCanceled As Boolean = False


    ''' <summary>
    ''' Gets or sets a currently applyed filter string.
    ''' </summary>
    ''' <remarks>A proxy property to the <see cref="InfoListControl.FilterString">
    ''' FilterString property of the nested InfoListControl</see>.</remarks>
    Public Property FilterString() As String
        Get
            If Control Is Nothing Then Return ""
            Return DirectCast(Control, InfoListControl).FilterString
        End Get
        Set(ByVal value As String)
            If Control Is Nothing Then Exit Property
            If value Is Nothing Then value = ""
            If value <> DirectCast(Control, InfoListControl).FilterString Then
                DirectCast(Control, InfoListControl).FilterString = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets a minimum required dropdown width.
    ''' </summary>
    ''' <remarks>Equals <see cref="BrightIdeasSoftware.DataListView.Width">
    ''' the width of the nested DataListView</see>.</remarks>
    Public ReadOnly Property MinDropDownWidth() As Integer
        Get
            Return _MinDropDownWidth
        End Get
    End Property

    ''' <summary>
    ''' Gets a required dropdown height.
    ''' </summary>
    ''' <remarks>Equals <see cref="BrightIdeasSoftware.DataListView.Height">
    ''' the height of the nested DataListView</see>.</remarks>
    Public ReadOnly Property DropDownHeight() As Integer
        Get
            Return _DropDownHeight
        End Get
    End Property

    ''' <summary>
    ''' Gets a value that has been selected by the user.
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly Property SelectedValue() As Object
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


    ' Call the base constructor passing in a InfoListControl instance.
    Public Sub New(ByVal listView As InfoListControl)
        MyBase.New(listView)
        Me.AutoSize = False
        Me._MinDropDownWidth = listView.Width
        Me._DropDownHeight = listView.Height
    End Sub

    ' disable default constructor access
    Private Sub New(ByVal c As Control)
        MyBase.New(c)
    End Sub


    ''' <summary>
    ''' Selects a value object in the encapsulated <see cref="BrightIdeasSoftware.DataListView">DataListView</see>.
    ''' </summary>
    ''' <param name="value">a value object or a value of the value object
    ''' <see cref="InfoListControl.ValueMember">InfoListControl.ValueMember</see> 
    ''' property (if set).</param>
    ''' <remarks></remarks>
    Friend Sub SetSelectedValue(ByVal value As Object)
        If Not Me.Control Is Nothing Then
            DirectCast(Me.Control, InfoListControl).SetSelectedValue(value)
        End If
    End Sub

    Friend Function GetDataSource() As Object
        If Not Me.Control Is Nothing Then
            Return DirectCast(Me.Control, InfoListControl).DataSource
        End If
        Return Nothing
    End Function


    Private Sub OnDataListViewValueSelected(ByVal sender As Object, _
        ByVal e As InfoListControl.ValueChangedEventArgs)
        _SelectedValue = e.SelectedValue
        _SelectionCanceled = e.SelectionCanceled
        DirectCast(Me.Owner, ToolStripDropDown).Close(ToolStripDropDownCloseReason.ItemClicked)
    End Sub


    ' Subscribe and unsubscribe the control events you wish to expose.
    Protected Overrides Sub OnSubscribeControlEvents(ByVal c As Control)
        ' Call the base so the base events are connected.
        MyBase.OnSubscribeControlEvents(c)

        ' Cast the control to a InfoListControl control.
        Dim nDataListView As InfoListControl = DirectCast(c, InfoListControl)

        ' Add the event.
        AddHandler nDataListView.ValueSelected, AddressOf OnDataListViewValueSelected

    End Sub

    Protected Overrides Sub OnUnsubscribeControlEvents(ByVal c As Control)
        ' Call the base method so the basic events are unsubscribed.
        MyBase.OnUnsubscribeControlEvents(c)

        ' Cast the control to a InfoListControl control.
        Dim nDataListView As InfoListControl = DirectCast(c, InfoListControl)

        ' Remove the event.
        RemoveHandler nDataListView.ValueSelected, AddressOf OnDataListViewValueSelected

    End Sub


    Protected Overrides Sub OnBoundsChanged()
        MyBase.OnBoundsChanged()
        If Not Control Is Nothing Then
            DirectCast(Control, InfoListControl).Size = Me.Size
        End If
    End Sub

End Class
