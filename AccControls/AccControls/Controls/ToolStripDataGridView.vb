Public Class ToolStripDataGridView
    Inherits ToolStripControlHost

    Private _CloseOnSingleClick As Boolean = True
    Private _MinDropDownWidth As Integer
    Private _DropDownHeight As Integer
    Private _Parent As AccGridComboBox

    ' Call the base constructor passing in a MonthCalendar instance.
    Public Sub New(ByVal nDataGridView As DataGridView, ByRef nParent As AccGridComboBox, ByVal nCloseOnSingleClick As Boolean)
        MyBase.New(nDataGridView)
        Me.AutoSize = False
        Me._MinDropDownWidth = nDataGridView.Width
        Me._CloseOnSingleClick = nCloseOnSingleClick
        Me._DropDownHeight = nDataGridView.Height
        _Parent = nParent
    End Sub


    Public Property CloseOnSingleClick() As Boolean
        Get
            Return _CloseOnSingleClick
        End Get
        Set(ByVal value As Boolean)
            _CloseOnSingleClick = value
        End Set
    End Property

    Public ReadOnly Property DataGridViewControl() As DataGridView
        Get
            Return TryCast(Control, DataGridView)
        End Get
    End Property

    Public ReadOnly Property MinDropDownWidth() As Integer
        Get
            Return _MinDropDownWidth
        End Get
    End Property

    Public ReadOnly Property DropDownHeight() As Integer
        Get
            Return _DropDownHeight
        End Get
    End Property

    Friend Sub SetParent(ByRef nParent As AccGridComboBox)
        _Parent = nParent
    End Sub

    Private Sub OnDataGridViewCellMouseEnter(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs)
        If Not e.RowIndex < 0 AndAlso DataGridViewControl.Rows(e.RowIndex).Displayed Then _
            DataGridViewControl.CurrentCell = DataGridViewControl.Item(0, e.RowIndex)
        If Not DataGridViewControl.Focused Then DataGridViewControl.Focus()
    End Sub

    Private Sub OnDataGridViewKeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            DirectCast(Me.Owner, ToolStripDropDown).Close(ToolStripDropDownCloseReason.ItemClicked)
            e.Handled = True

        End If
    End Sub

    Private Sub OnDataGridViewKeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs)

        If DataGridViewControl Is Nothing OrElse _Parent Is Nothing Then Exit Sub
        If Char.IsLetterOrDigit(e.KeyChar) OrElse Char.IsPunctuation(e.KeyChar) OrElse Char.IsSymbol(e.KeyChar) _
            OrElse Char.IsWhiteSpace(e.KeyChar) Then
            If Not _Parent.EmptyValueString Is Nothing AndAlso _
                Not String.IsNullOrEmpty(_Parent.EmptyValueString.Trim) AndAlso _
                _Parent.Text.Trim.ToLower = _Parent.EmptyValueString.Trim.ToLower Then
                _Parent.Text = e.KeyChar
            Else
                _Parent.Text = _Parent.Text & e.KeyChar
            End If

        ElseIf Convert.ToChar(Keys.Back) = e.KeyChar AndAlso _Parent.Text.Length > 0 Then
            _Parent.Text = _Parent.Text.Substring(0, _Parent.Text.Length - 1)
        Else
            e.Handled = False
            Exit Sub
        End If

        If String.IsNullOrEmpty(_Parent.FilterPropertyName.Trim) Then Exit Sub

        Dim prop As System.Reflection.PropertyInfo = Nothing
        For Each dr As DataGridViewRow In DataGridViewControl.Rows
            If prop Is Nothing Then
                prop = dr.DataBoundItem.GetType.GetProperty(_Parent.FilterPropertyName)
                If prop Is Nothing Then Exit Sub
            End If
            If prop.GetValue(dr.DataBoundItem, Nothing).ToString.Trim.ToLower.StartsWith(_Parent.Text.Trim.ToLower) Then
                DataGridViewControl.FirstDisplayedScrollingRowIndex = dr.Index
                DataGridViewControl.ClearSelection()
                DataGridViewControl.CurrentCell = dr.Cells(0)
                dr.Selected = True
                Exit For
            End If
        Next

    End Sub

    Protected Overrides Sub OnKeyPress(ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If Not DataGridViewControl.Focused Then
            DataGridViewControl.Focus()
            OnDataGridViewKeyPress(Me, e)
        End If
        MyBase.OnKeyPress(e)
    End Sub

    Private Sub myDataGridView_DoubleClick(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs)
        DirectCast(Me.Owner, ToolStripDropDown).Close(ToolStripDropDownCloseReason.ItemClicked)
    End Sub

    Private Sub myDataGridView_Click(ByVal sender As Object, ByVal e As DataGridViewCellEventArgs)
        If _CloseOnSingleClick Then DirectCast(Me.Owner, ToolStripDropDown).Close(ToolStripDropDownCloseReason.ItemClicked)
    End Sub

    ' Subscribe and unsubscribe the control events you wish to expose.
    Protected Overrides Sub OnSubscribeControlEvents(ByVal c As Control)
        ' Call the base so the base events are connected.
        MyBase.OnSubscribeControlEvents(c)

        ' Cast the control to a MonthCalendar control.
        Dim nDataGridView As DataGridView = DirectCast(c, DataGridView)

        ' Add the event.
        AddHandler nDataGridView.CellMouseEnter, AddressOf OnDataGridViewCellMouseEnter
        AddHandler nDataGridView.KeyDown, AddressOf OnDataGridViewKeyDown
        AddHandler nDataGridView.CellDoubleClick, AddressOf myDataGridView_DoubleClick
        AddHandler nDataGridView.CellClick, AddressOf myDataGridView_Click
        AddHandler nDataGridView.KeyPress, AddressOf OnDataGridViewKeyPress

    End Sub

    Protected Overrides Sub OnUnsubscribeControlEvents(ByVal c As Control)
        ' Call the base method so the basic events are unsubscribed.
        MyBase.OnUnsubscribeControlEvents(c)

        ' Cast the control to a MonthCalendar control.
        Dim nDataGridView As DataGridView = DirectCast(c, DataGridView)

        ' Remove the event.
        RemoveHandler nDataGridView.CellMouseEnter, AddressOf OnDataGridViewCellMouseEnter
        RemoveHandler nDataGridView.KeyDown, AddressOf OnDataGridViewKeyDown
        RemoveHandler nDataGridView.CellDoubleClick, AddressOf myDataGridView_DoubleClick
        RemoveHandler nDataGridView.CellClick, AddressOf myDataGridView_Click
        RemoveHandler nDataGridView.KeyPress, AddressOf OnDataGridViewKeyPress

    End Sub

    Protected Overrides Sub OnBoundsChanged()
        MyBase.OnBoundsChanged()
        If Not Control Is Nothing Then
            DirectCast(Control, DataGridView).Size = Me.Size
            DirectCast(Control, DataGridView).AutoResizeColumns()
        End If
    End Sub

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        MyBase.Dispose(disposing)
        If Not Control Is Nothing AndAlso Not DirectCast(Control, DataGridView).IsDisposed Then Control.Dispose()
    End Sub

End Class