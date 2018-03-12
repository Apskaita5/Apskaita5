'---------------------------------------------------------------------
'  Copyright (C) Microsoft Corporation.  All rights reserved.
' 
'THIS CODE AND INFORMATION ARE PROVIDED AS IS WITHOUT WARRANTY OF ANY
'KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
'IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
'PARTICULAR PURPOSE.
'---------------------------------------------------------------------

Imports System.ComponentModel
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports System.Windows.Forms.VisualStyles

''' <summary>
''' Provides a combobox in a DataGridViewColumnHeaderCell.
''' </summary>
Public Class DataGridViewColumnHeaderComboBoxCell
    Inherits DataGridViewColumnHeaderCell

    ''' <summary>
    ''' The ListBox used for all drop-down lists. 
    ''' </summary>
    Private Shared dropDownListBox As New FilterListBox()

    ''' <summary>
    ''' Event that is raised when the user selects a value in the combobox.
    ''' </summary>
    ''' <param name="sender">DataGridViewColumnHeaderComboBoxCell that raised the event.</param>
    ''' <param name="e">Event arguments</param>
    Public Event SelectedValueChanged(ByVal sender As Object, e As SelectedValueChangedEventArgs)


    ''' <summary>
    ''' Initializes a new instance of the DataGridViewColumnHeaderCell 
    ''' class and sets its property values to the property values of the 
    ''' specified DataGridViewColumnHeaderCell.
    ''' </summary>
    ''' <param name="oldHeaderCell">The DataGridViewColumnHeaderCell to 
    ''' copy property values from.</param>
    Public Sub New(ByVal oldHeaderCell As DataGridViewColumnHeaderCell)

        Me.ContextMenuStrip = oldHeaderCell.ContextMenuStrip
        Me.ErrorText = oldHeaderCell.ErrorText
        Me.Tag = oldHeaderCell.Tag
        Me.ToolTipText = oldHeaderCell.ToolTipText
        Me.Value = oldHeaderCell.Value
        Me.ValueType = oldHeaderCell.ValueType

        ' Use HasStyle to avoid creating a new style object
        ' when the Style property has not previously been set. 
        If oldHeaderCell.HasStyle Then
            Me.Style = oldHeaderCell.Style
        End If

        ' Copy this type's properties if the old cell is a combo box cell. 
        ' This enables the Clone method to reuse this constructor. 
        Dim comboBoxCell As DataGridViewColumnHeaderComboBoxCell =
            TryCast(oldHeaderCell, DataGridViewColumnHeaderComboBoxCell)
        If comboBoxCell IsNot Nothing Then
            Me.ComboBoxEnabled = comboBoxCell.ComboBoxEnabled
            Me.DataSource = comboBoxCell.DataSource
            Me.AutomaticSortingEnabled = comboBoxCell.AutomaticSortingEnabled
            Me.DropDownListBoxMaxLines = comboBoxCell.DropDownListBoxMaxLines
            Me.currentDropDownButtonPaddingOffset =
                comboBoxCell.currentDropDownButtonPaddingOffset
            Me._UseTagForSelectedValue = comboBoxCell._UseTagForSelectedValue
        End If

    End Sub

    ''' <summary>
    ''' Initializes a new instance of the DataGridViewColumnHeaderCell 
    ''' class. 
    ''' </summary>
    Public Sub New()
    End Sub

    ''' <summary>
    ''' Creates an exact copy of this cell.
    ''' </summary>
    ''' <returns>An object that represents the cloned 
    ''' DataGridViewAutoFilterColumnHeaderCell.</returns>
    Public Overrides Function Clone() As Object
        Return New DataGridViewColumnHeaderComboBoxCell(Me)
    End Function

    ''' <summary>
    ''' Called when the value of the DataGridView property changes
    ''' in order to perform initialization that requires access to the 
    ''' owning control and column. 
    ''' </summary>
    Protected Overrides Sub OnDataGridViewChanged()

        ' Continue only if there is a DataGridView. 
        If Me.DataGridView Is Nothing Then
            Return
        End If

        ' Disable sorting and filtering for columns that can't make
        ' effective use of them. 
        If OwningColumn IsNot Nothing Then

            If TypeOf OwningColumn Is DataGridViewImageColumn OrElse
                (TypeOf OwningColumn Is DataGridViewButtonColumn AndAlso
                CType(OwningColumn, DataGridViewButtonColumn).UseColumnTextForButtonValue) _
                OrElse (TypeOf OwningColumn Is DataGridViewLinkColumn AndAlso
                CType(OwningColumn, DataGridViewLinkColumn).UseColumnTextForLinkValue) Then

                AutomaticSortingEnabled = False

            End If

            ' Ensure that the column SortMode property value is not Automatic.
            ' This prevents sorting when the user clicks the drop-down button.
            If OwningColumn.SortMode = DataGridViewColumnSortMode.Automatic Then
                OwningColumn.SortMode = DataGridViewColumnSortMode.Programmatic
            End If

        End If

        ' Add handlers to DataGridView events. 
        HandleDataGridViewEvents()

        ' Initialize the drop-down button bounds so that any initial
        ' column autosizing will accommodate the button width. 
        SetDropDownButtonBounds()

        ' Call the OnDataGridViewChanged method on the base class to 
        ' raise the DataGridViewChanged event.
        MyBase.OnDataGridViewChanged()

    End Sub 'OnDataGridViewChanged

#Region "DataGridView events: HandleDataGridViewEvents, DataGridView event handlers, ResetDropDown, ResetFilter"

    ''' <summary>
    ''' Add handlers to various DataGridView events, primarily to invalidate 
    ''' the drop-down button bounds, hide the drop-down list, and reset 
    ''' cached filter values when changes in the DataGridView require it.
    ''' </summary>
    Private Sub HandleDataGridViewEvents()
        AddHandler Me.DataGridView.Scroll, AddressOf DataGridView_Scroll
        AddHandler Me.DataGridView.ColumnDisplayIndexChanged, AddressOf DataGridView_ColumnDisplayIndexChanged
        AddHandler Me.DataGridView.ColumnWidthChanged, AddressOf DataGridView_ColumnWidthChanged
        AddHandler Me.DataGridView.ColumnHeadersHeightChanged, AddressOf DataGridView_ColumnHeadersHeightChanged
        AddHandler Me.DataGridView.SizeChanged, AddressOf DataGridView_SizeChanged
        AddHandler Me.DataGridView.DataSourceChanged, AddressOf DataGridView_DataSourceChanged
        AddHandler Me.DataGridView.DataBindingComplete, AddressOf DataGridView_DataBindingComplete

        ' Add a handler for the ColumnSortModeChanged event to prevent the
        ' column SortMode property from being inadvertently set to Automatic.
        AddHandler Me.DataGridView.ColumnSortModeChanged, AddressOf DataGridView_ColumnSortModeChanged
    End Sub

    ''' <summary>
    ''' Invalidates the drop-down button bounds when 
    ''' the user scrolls horizontally.
    ''' </summary>
    ''' <param name="sender">The object that raised the event.</param>
    ''' <param name="e">A ScrollEventArgs that contains the event data.</param>
    Private Sub DataGridView_Scroll(
        ByVal sender As Object, ByVal e As ScrollEventArgs)
        If e.ScrollOrientation = ScrollOrientation.HorizontalScroll Then
            ResetDropDown()
        End If
    End Sub

    ''' <summary>
    ''' Invalidates the drop-down button bounds when 
    ''' the column display index changes.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub DataGridView_ColumnDisplayIndexChanged(
        ByVal sender As Object, ByVal e As DataGridViewColumnEventArgs)
        ResetDropDown()
    End Sub

    ''' <summary>
    ''' Invalidates the drop-down button bounds when a column width changes
    ''' in the DataGridView control. A width change in any column of the 
    ''' control has the potential to affect the drop-down button location, 
    ''' depending on the current horizontal scrolling position and whether
    ''' the changed column is to the left or right of the current column. 
    ''' It is easier to invalidate the button in all cases. 
    ''' </summary>
    ''' <param name="sender">The object that raised the event.</param>
    ''' <param name="e">A DataGridViewColumnEventArgs that contains the event data.</param>
    Private Sub DataGridView_ColumnWidthChanged(
        ByVal sender As Object, ByVal e As DataGridViewColumnEventArgs)
        ResetDropDown()
    End Sub

    ''' <summary>
    ''' Invalidates the drop-down button bounds when the height of the column headers changes.
    ''' </summary>
    ''' <param name="sender">The object that raised the event.</param>
    ''' <param name="e">An EventArgs that contains the event data.</param>
    Private Sub DataGridView_ColumnHeadersHeightChanged(
        ByVal sender As Object, ByVal e As EventArgs)
        ResetDropDown()
    End Sub

    ''' <summary>
    ''' Invalidates the drop-down button bounds when the size of the DataGridView changes.
    ''' This prevents a painting issue that occurs when the right edge of the control moves 
    ''' to the right and the control contents have previously been scrolled to the right.
    ''' </summary>
    ''' <param name="sender">The object that raised the event.</param>
    ''' <param name="e">An EventArgs that contains the event data.</param>
    Private Sub DataGridView_SizeChanged(
        ByVal sender As Object, ByVal e As EventArgs)
        ResetDropDown()
    End Sub

    ''' <summary>
    ''' Invalidates the drop-down button bounds, hides the drop-down 
    ''' filter list, if it is showing, and resets the cached filter values
    ''' if the filter has been removed. 
    ''' </summary>
    ''' <param name="sender">The object that raised the event.</param>
    ''' <param name="e">A DataGridViewBindingCompleteEventArgs that contains the event data.</param>
    Private Sub DataGridView_DataBindingComplete(
        ByVal sender As Object, ByVal e As DataGridViewBindingCompleteEventArgs)
        If e.ListChangedType = ListChangedType.Reset Then
            ResetDropDown()
        End If
    End Sub

    ''' <summary>
    ''' Verifies that the data source meets requirements, invalidates the 
    ''' drop-down button bounds, hides the drop-down filter list if it is 
    ''' showing, and resets the cached filter values if the filter has been removed. 
    ''' </summary>
    ''' <param name="sender">The object that raised the event.</param>
    ''' <param name="e">An EventArgs that contains the event data.</param>
    Private Sub DataGridView_DataSourceChanged(
        ByVal sender As Object, ByVal e As EventArgs)
        ResetDropDown()
    End Sub

    ''' <summary>
    ''' Invalidates the drop-down button bounds and hides the filter
    ''' list if it is showing.
    ''' </summary>
    Private Sub ResetDropDown()
        InvalidateDropDownButtonBounds()
        If dropDownListBoxShowing Then
            HideDropDownList()
        End If
    End Sub

    ''' <summary>
    ''' Throws an exception when the column sort mode is changed to Automatic.
    ''' </summary>
    ''' <param name="sender">The object that raised the event.</param>
    ''' <param name="e">A DataGridViewColumnEventArgs that contains the event data.</param>
    Private Sub DataGridView_ColumnSortModeChanged(
        ByVal sender As Object, ByVal e As DataGridViewColumnEventArgs)

        If e.Column Is OwningColumn AndAlso
            e.Column.SortMode = DataGridViewColumnSortMode.Automatic Then
            Throw New InvalidOperationException(
                "A SortMode value of Automatic is incompatible with " &
                "the DataGridViewAutoFilterColumnHeaderCell type. " &
                "Use the AutomaticSortingEnabled property instead.")
        End If

    End Sub

#End Region 'DataGridView events

    ''' <summary>
    ''' Paints the column header cell, including the drop-down button. 
    ''' </summary>
    ''' <param name="graphics">The Graphics used to paint the DataGridViewCell.</param>
    ''' <param name="clipBounds">A Rectangle that represents the area of the DataGridView that needs to be repainted.</param>
    ''' <param name="cellBounds">A Rectangle that contains the bounds of the DataGridViewCell that is being painted.</param>
    ''' <param name="rowIndex">The row index of the cell that is being painted.</param>
    ''' <param name="cellState">A bitwise combination of DataGridViewElementStates values that specifies the state of the cell.</param>
    ''' <param name="value">The data of the DataGridViewCell that is being painted.</param>
    ''' <param name="formattedValue">The formatted data of the DataGridViewCell that is being painted.</param>
    ''' <param name="errorText">An error message that is associated with the cell.</param>
    ''' <param name="cellStyle">A DataGridViewCellStyle that contains formatting and style information about the cell.</param>
    ''' <param name="advancedBorderStyle">A DataGridViewAdvancedBorderStyle that contains border styles for the cell that is being painted.</param>
    ''' <param name="paintParts">A bitwise combination of the DataGridViewPaintParts values that specifies which parts of the cell need to be painted.</param>
    Protected Overrides Sub Paint(
        ByVal graphics As Graphics,
        ByVal clipBounds As Rectangle,
        ByVal cellBounds As Rectangle,
        ByVal rowIndex As Integer,
        ByVal cellState As DataGridViewElementStates,
        ByVal value As Object,
        ByVal formattedValue As Object,
        ByVal errorText As String,
        ByVal cellStyle As DataGridViewCellStyle,
        ByVal advancedBorderStyle As DataGridViewAdvancedBorderStyle,
        ByVal paintParts As DataGridViewPaintParts)

        ' Use the base method to paint the default appearance. 
        MyBase.Paint(graphics, clipBounds, cellBounds, rowIndex,
            cellState, value, formattedValue, errorText, cellStyle,
            advancedBorderStyle, paintParts)

        ' Continue only if filtering is enabled and ContentBackground is 
        ' part of the paint request. 
        If Not ComboBoxEnabled OrElse (paintParts And
            DataGridViewPaintParts.ContentBackground) = 0 Then
            Return
        End If

        ' Retrieve the current button bounds. 
        Dim buttonBounds As Rectangle = DropDownButtonBounds

        ' Continue only if the buttonBounds is big enough to draw.
        If buttonBounds.Width < 1 OrElse buttonBounds.Height < 1 Then Return

        ' Paint the button manually or using visual styles if visual styles 
        ' are enabled, using the correct state depending on whether the 
        ' filter list is showing and whether there is a filter in effect 
        ' for the current column. 
        If Application.RenderWithVisualStyles Then

            Dim state As ComboBoxState = ComboBoxState.Normal
            If dropDownListBoxShowing Then
                state = ComboBoxState.Pressed
            End If
            ComboBoxRenderer.DrawDropDownButton(graphics, buttonBounds, state)

        Else

            Dim pressedOffset As Int32 = 0
            Dim state As PushButtonState = PushButtonState.Normal
            If dropDownListBoxShowing Then
                state = PushButtonState.Pressed
                pressedOffset = 1
            End If
            ButtonRenderer.DrawButton(graphics, buttonBounds, state)

            graphics.FillPolygon(SystemBrushes.ControlText, New Point() {
                    New Point(
                        buttonBounds.Width \ 2 +
                            buttonBounds.Left - 1 + pressedOffset,
                        buttonBounds.Height * 3 \ 4 +
                            buttonBounds.Top - 1 + pressedOffset),
                    New Point(
                        buttonBounds.Width \ 4 +
                            buttonBounds.Left + pressedOffset,
                        buttonBounds.Height \ 2 +
                            buttonBounds.Top - 1 + pressedOffset),
                    New Point(
                        buttonBounds.Width * 3 \ 4 +
                            buttonBounds.Left - 1 + pressedOffset,
                        buttonBounds.Height \ 2 +
                            buttonBounds.Top - 1 + pressedOffset)
                })

        End If

    End Sub 'Paint

    ''' <summary>
    ''' Handles mouse clicks to the header cell, displaying the 
    ''' drop-down list or sorting the owning column as appropriate. 
    ''' </summary>
    ''' <param name="e">A DataGridViewCellMouseEventArgs that contains the event data.</param>
    Protected Overrides Sub OnMouseDown(ByVal e As DataGridViewCellMouseEventArgs)

        Debug.Assert(Me.DataGridView IsNot Nothing, "DataGridView is null")

        ' Continue only if the user did not click the drop-down button 
        ' while the drop-down list was displayed. This prevents the 
        ' drop-down list from being redisplayed after being hidden in 
        ' the LostFocus event handler. 
        If lostFocusOnDropDownButtonClick Then
            lostFocusOnDropDownButtonClick = False
            Return
        End If

        ' Retrieve the current size and location of the header cell,
        ' excluding any portion that is scrolled off screen. 
        Dim cellBounds As Rectangle = Me.DataGridView _
            .GetCellDisplayRectangle(e.ColumnIndex, -1, False)

        ' Continue only if the column is not manually resizable or the
        ' mouse coordinates are not within the column resize zone. 
        If Me.OwningColumn.Resizable = DataGridViewTriState.True AndAlso
            (Me.DataGridView.RightToLeft = RightToLeft.No AndAlso
            cellBounds.Width - e.X < 6 OrElse e.X < 6) Then
            Return
        End If

        ' Unless RightToLeft is enabled, store the width of the portion
        ' that is scrolled off screen. 
        Dim scrollingOffset As Int32 = 0
        If Me.DataGridView.RightToLeft = RightToLeft.No AndAlso
            Me.DataGridView.FirstDisplayedScrollingColumnIndex = Me.ColumnIndex Then
            scrollingOffset = Me.DataGridView.FirstDisplayedScrollingColumnHiddenWidth
        End If

        ' Show the drop-down list if filtering is enabled and the mouse click occurred
        ' within the drop-down button bounds. Otherwise, if sorting is enabled and the
        ' click occurred outside the drop-down button bounds, sort by the owning column. 
        ' The mouse coordinates are relative to the cell bounds, so the cell location
        ' and the scrolling offset are needed to determine the client coordinates.
        If ComboBoxEnabled AndAlso
            DropDownButtonBounds.Contains(
            e.X + cellBounds.Left - scrollingOffset, e.Y + cellBounds.Top) Then

            ' If the current cell is in edit mode, commit the edit. 
            If Me.DataGridView.IsCurrentCellInEditMode Then
                ' Commit and end the cell edit.  
                Me.DataGridView.EndEdit()

                ' Commit any change to the underlying data source. 
                Dim source As BindingSource =
                    TryCast(Me.DataGridView.DataSource, BindingSource)
                If source IsNot Nothing Then
                    source.EndEdit()
                End If
            End If
            ShowDropDownList()

        ElseIf AutomaticSortingEnabled AndAlso
            Me.DataGridView.SelectionMode <>
            DataGridViewSelectionMode.ColumnHeaderSelect Then

            SortByColumn()

        End If

        MyBase.OnMouseDown(e)

    End Sub 'OnMouseDown

    ''' <summary>
    ''' Sorts the DataGridView by the current column if AutomaticSortingEnabled is true.
    ''' </summary>
    Private Sub SortByColumn()

        Debug.Assert(Me.DataGridView IsNot Nothing AndAlso
            OwningColumn IsNot Nothing, "DataGridView or OwningColumn is null")

        ' Continue only if the data source supports sorting. 
        If Not AutomaticSortingEnabled Then
            Return
        End If

        ' Determine the sort direction and sort by the owning column. 
        Dim direction As ListSortDirection = ListSortDirection.Ascending
        If Me.DataGridView.SortedColumn Is OwningColumn AndAlso
            Me.DataGridView.SortOrder = SortOrder.Ascending Then
            direction = ListSortDirection.Descending
        End If
        Me.DataGridView.Sort(OwningColumn, direction)

    End Sub

#Region "drop-down list: Show/HideDropDownListBox, SetDropDownListBoxBounds, DropDownListBoxMaxHeightInternal"

    ''' <summary>
    ''' Indicates whether dropDownListBox is currently displayed 
    ''' for this header cell. 
    ''' </summary>
    Private dropDownListBoxShowing As Boolean

    ''' <summary>
    ''' Displays the drop-down filter list. 
    ''' </summary>
    Public Sub ShowDropDownList()

        Debug.Assert(Me.DataGridView IsNot Nothing, "DataGridView is null")

        ' Ensure that the current row is not the row for new records.
        ' This prevents the new row from affecting the filter list and also 
        ' prevents the new row from being added when the filter changes.
        If Me.DataGridView.CurrentRow IsNot Nothing AndAlso
            Me.DataGridView.CurrentRow.IsNewRow Then
            Me.DataGridView.CurrentCell = Nothing
        End If

        dropDownListBox.DataSource = _DataSource
        If _UseTagForSelectedValue AndAlso Not Me.OwningColumn Is Nothing AndAlso
            Not Me.OwningColumn.Tag Is Nothing Then
            Try
                dropDownListBox.SelectedItem = Me.OwningColumn.Tag
            Catch ex As Exception
            End Try
        End If

        ' Add handlers to dropDownListBox events. 
        HandleDropDownListBoxEvents()

        ' Set the size and location of dropDownListBox, then display it. 
        SetDropDownListBoxBounds()
        dropDownListBox.Visible = True
        dropDownListBoxShowing = True

        Debug.Assert(dropDownListBox.Parent Is Nothing,
            "ShowDropDownListBox has been called multiple times before HideDropDownListBox")

        ' Add dropDownListBox to the DataGridView. 
        Me.DataGridView.Controls.Add(dropDownListBox)

        ' Set the input focus to dropDownListBox. 
        dropDownListBox.Focus()

        ' Invalidate the cell so that the drop-down button will repaint
        ' in the pressed state. 
        Me.DataGridView.InvalidateCell(Me)

    End Sub

    ''' <summary>
    ''' Hides the drop-down filter list. 
    ''' </summary>
    Public Sub HideDropDownList()

        Debug.Assert(Me.DataGridView IsNot Nothing, "DataGridView is null")

        ' Hide dropDownListBox, remove handlers from its events, and remove 
        ' it from the DataGridView control. 
        dropDownListBoxShowing = False
        dropDownListBox.Visible = False
        UnhandleDropDownListBoxEvents()
        Me.DataGridView.Controls.Remove(dropDownListBox)

        ' Invalidate the cell so that the drop-down button will repaint
        ' in the unpressed state. 
        Me.DataGridView.InvalidateCell(Me)

    End Sub

    ''' <summary>
    ''' Sets the dropDownListBox size and position based on the formatted 
    ''' values in the filters dictionary and the position of the drop-down 
    ''' button. Called only by ShowDropDownListBox.  
    ''' </summary>
    Private Sub SetDropDownListBoxBounds()

        ' Declare variables that will be used in the calculation, 
        ' initializing dropDownListBoxHeight to account for the 
        ' ListBox borders.
        Dim dropDownListBoxHeight As Int32 = 2
        Dim currentWidth As Int32 = 0
        Dim dropDownListBoxWidth As Int32 = 0
        Dim dropDownListBoxLeft As Int32 = 0

        If Not _DataSource Is Nothing Then
            ' For each value in the combobox datasource,
            ' add its height to dropDownListBoxHeight and, if it is wider than 
            ' all previous values, set dropDownListBoxWidth to its width.
            Dim graphics As Graphics = dropDownListBox.CreateGraphics()
            Try
                For Each item As Object In _DataSource
                    Dim stringSizeF As SizeF =
                        graphics.MeasureString(item.ToString, dropDownListBox.Font)
                    dropDownListBoxHeight += CInt(Int(stringSizeF.Height))
                    currentWidth = CType(stringSizeF.Width, Int32)
                    If dropDownListBoxWidth < currentWidth Then
                        dropDownListBoxWidth = currentWidth
                    End If
                Next

            Finally
                graphics.Dispose()
            End Try
        End If

        ' Increase the width to allow for horizontal margins and borders. 
        dropDownListBoxWidth += 6

        ' Constrain the dropDownListBox height to the 
        ' DropDownListBoxMaxHeightInternal value, which is based on 
        ' the DropDownListBoxMaxLines property value but constrained by
        ' the maximum height available in the DataGridView control.
        If dropDownListBoxHeight > DropDownListBoxMaxHeightInternal Then

            dropDownListBoxHeight = DropDownListBoxMaxHeightInternal

            ' If the preferred height is greater than the available height,
            ' adjust the width to accommodate the vertical scroll bar. 
            dropDownListBoxWidth += SystemInformation.VerticalScrollBarWidth

        End If

        ' Calculate the ideal location of the left edge of dropDownListBox 
        ' based on the location of the drop-down button and taking the 
        ' RightToLeft property value into consideration. 
        If Me.DataGridView.RightToLeft = RightToLeft.No Then
            dropDownListBoxLeft = DropDownButtonBounds.Right - dropDownListBoxWidth + 1
        Else
            dropDownListBoxLeft = DropDownButtonBounds.Left - 1
        End If

        ' Determine the left and right edges of the available horizontal
        ' width of the DataGridView control. 
        Dim clientLeft As Int32 = 1
        Dim clientRight As Int32 = Me.DataGridView.ClientRectangle.Right
        If Me.DataGridView.DisplayedRowCount(False) < Me.DataGridView.RowCount Then
            If Me.DataGridView.RightToLeft = RightToLeft.Yes Then
                clientLeft += SystemInformation.VerticalScrollBarWidth
            Else
                clientRight -= SystemInformation.VerticalScrollBarWidth
            End If
        End If

        ' Adjust the dropDownListBox location and/or width if it would
        ' otherwise overlap the left or right edge of the DataGridView.
        If dropDownListBoxLeft < clientLeft Then
            dropDownListBoxLeft = clientLeft
        End If
        Dim dropDownListBoxRight As Int32 =
            dropDownListBoxLeft + dropDownListBoxWidth + 1
        If dropDownListBoxRight > clientRight Then
            If dropDownListBoxLeft = clientLeft Then
                dropDownListBoxWidth -= dropDownListBoxRight - clientRight
            Else
                dropDownListBoxLeft -= dropDownListBoxRight - clientRight
                If dropDownListBoxLeft < clientLeft Then
                    dropDownListBoxWidth -= clientLeft - dropDownListBoxLeft
                    dropDownListBoxLeft = clientLeft
                End If
            End If
        End If

        ' Set the ListBox.Bounds property using the calculated values. 
        dropDownListBox.Bounds = New Rectangle(dropDownListBoxLeft,
            DropDownButtonBounds.Bottom, dropDownListBoxWidth,
            dropDownListBoxHeight)

    End Sub 'SetDropDownListBoxBounds

    ''' <summary>
    ''' Gets the actual maximum height of the drop-down list, in pixels.
    ''' The maximum height is calculated from the DropDownListBoxMaxLines 
    ''' property value, but is limited to the available height of the 
    ''' DataGridView control. 
    ''' </summary>
    Protected ReadOnly Property DropDownListBoxMaxHeightInternal() As Int32
        Get
            ' Calculate the height of the available client area
            ' in the DataGridView control, taking the horizontal
            ' scroll bar into consideration and leaving room
            ' for the ListBox bottom border. 
            Dim dataGridViewMaxHeight As Int32 =
                Me.DataGridView.Height - Me.DataGridView.ColumnHeadersHeight - 1
            If Me.DataGridView.DisplayedColumnCount(False) <
                Me.DataGridView.ColumnCount Then
                dataGridViewMaxHeight -= SystemInformation.HorizontalScrollBarHeight
            End If

            ' Calculate the height of the list box, using the combined 
            ' height of all items plus 2 for the top and bottom border. 
            Dim listMaxHeight As Int32 =
                dropDownListBoxMaxLinesValue * dropDownListBox.ItemHeight + 2

            ' Return the smaller of the two values. 
            If listMaxHeight < dataGridViewMaxHeight Then
                Return listMaxHeight
            Else
                Return dataGridViewMaxHeight
            End If
        End Get
    End Property

#End Region 'drop-down list

#Region "ListBox events: HandleDropDownListBoxEvents, UnhandleDropDownListBoxEvents, ListBox event handlers"

    ''' <summary>
    ''' Adds handlers to ListBox events for handling mouse
    ''' and keyboard input.
    ''' </summary>
    Private Sub HandleDropDownListBoxEvents()
        AddHandler dropDownListBox.MouseClick, AddressOf DropDownListBox_MouseClick
        AddHandler dropDownListBox.LostFocus, AddressOf DropDownListBox_LostFocus
        AddHandler dropDownListBox.KeyDown, AddressOf DropDownListBox_KeyDown
    End Sub

    ''' <summary>
    ''' Removes the ListBox event handlers. 
    ''' </summary>
    Private Sub UnhandleDropDownListBoxEvents()
        RemoveHandler dropDownListBox.MouseClick, AddressOf DropDownListBox_MouseClick
        RemoveHandler dropDownListBox.LostFocus, AddressOf DropDownListBox_LostFocus
        RemoveHandler dropDownListBox.KeyDown, AddressOf DropDownListBox_KeyDown
    End Sub

    ''' <summary>
    ''' Adjusts the filter in response to a user selection from the drop-down list. 
    ''' </summary>
    ''' <param name="sender">The object that raised the event.</param>
    ''' <param name="e">A MouseEventArgs that contains the event data.</param>
    Private Sub DropDownListBox_MouseClick(
        ByVal sender As Object, ByVal e As MouseEventArgs)

        Debug.Assert(Me.DataGridView IsNot Nothing, "DataGridView is null")

        ' Continue only if the mouse click was in the content area
        ' and not on the scroll bar. 
        If Not dropDownListBox.DisplayRectangle.Contains(e.X, e.Y) OrElse Me.OwningColumn Is Nothing Then Exit Sub

        If _UseTagForSelectedValue Then Me.OwningColumn.Tag = dropDownListBox.SelectedValue
        RaiseEvent SelectedValueChanged(Me, New SelectedValueChangedEventArgs(
            dropDownListBox.SelectedValue, Me.OwningColumn))

        HideDropDownList()

    End Sub

    ''' <summary>
    ''' Indicates whether the drop-down list lost focus because the
    ''' user clicked the drop-down button. 
    ''' </summary>
    Private lostFocusOnDropDownButtonClick As Boolean

    ''' <summary>
    ''' Hides the drop-down list when it loses focus. 
    ''' </summary>
    ''' <param name="sender">The object that raised the event.</param>
    ''' <param name="e">An EventArgs that contains the event data.</param>
    Private Sub DropDownListBox_LostFocus(ByVal sender As Object, ByVal e As EventArgs)
        ' If the focus was lost because the user clicked the drop-down
        ' button, store a value that prevents the subsequent OnMouseDown
        ' call from displaying the drop-down list again. 
        If DropDownButtonBounds.Contains(Me.DataGridView.PointToClient(
            New Point(Control.MousePosition.X, Control.MousePosition.Y))) Then
            lostFocusOnDropDownButtonClick = True
        End If
        HideDropDownList()
    End Sub

    ''' <summary>
    ''' Handles the ENTER and ESC keys.
    ''' </summary>
    ''' <param name="sender">The object that raised the event.</param>
    ''' <param name="e">A KeyEventArgs that contains the event data.</param>
    Sub DropDownListBox_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs)
        Select Case e.KeyCode
            Case Keys.Enter
                If Not Me.OwningColumn Is Nothing Then
                    If _UseTagForSelectedValue Then Me.OwningColumn.Tag = dropDownListBox.SelectedValue
                    RaiseEvent SelectedValueChanged(Me, New SelectedValueChangedEventArgs(
                        dropDownListBox.SelectedValue, Me.OwningColumn))
                End If
                HideDropDownList()
            Case Keys.Escape
                HideDropDownList()
        End Select
    End Sub

#End Region 'ListBox events

#Region "button bounds: DropDownButtonBounds, InvalidateDropDownButtonBounds, SetDropDownButtonBounds, AdjustPadding"

    ''' <summary>
    ''' The bounds of the drop-down button, or Rectangle.Empty if filtering 
    ''' is disabled or the button bounds need to be recalculated. 
    ''' </summary>
    Private dropDownButtonBoundsValue As Rectangle = Rectangle.Empty

    ''' <summary>
    ''' The bounds of the drop-down button, or Rectangle.Empty if filtering
    ''' is disabled. Recalculates the button bounds if filtering is enabled
    ''' and the bounds are empty.
    ''' </summary>
    Protected ReadOnly Property DropDownButtonBounds() As Rectangle
        Get
            If Not ComboBoxEnabled Then
                Return Rectangle.Empty
            End If
            If dropDownButtonBoundsValue = Rectangle.Empty Then
                SetDropDownButtonBounds()
            End If
            Return dropDownButtonBoundsValue
        End Get
    End Property

    ''' <summary>
    ''' Sets dropDownButtonBoundsValue to Rectangle.Empty if it isn't already empty. 
    ''' This indicates that the button bounds should be recalculated. 
    ''' </summary>
    Private Sub InvalidateDropDownButtonBounds()
        If Not dropDownButtonBoundsValue.IsEmpty Then
            dropDownButtonBoundsValue = Rectangle.Empty
        End If
    End Sub

    ''' <summary>
    ''' Sets the position and size of dropDownButtonBoundsValue based on the current 
    ''' cell bounds and the preferred cell height for a single line of header text. 
    ''' </summary>
    Private Sub SetDropDownButtonBounds()

        ' Retrieve the cell display rectangle, which is used to 
        ' set the position of the drop-down button. 
        Dim cellBounds As Rectangle =
            Me.DataGridView.GetCellDisplayRectangle(Me.ColumnIndex, -1, False)

        ' Initialize a variable to store the button edge length,
        ' setting its initial value based on the font height. 
        Dim buttonEdgeLength As Int32 = Me.InheritedStyle.Font.Height + 5

        ' Calculate the height of the cell borders and padding.
        Dim borderRect As Rectangle = BorderWidths(
            Me.DataGridView.AdjustColumnHeaderBorderStyle(
            Me.DataGridView.AdvancedColumnHeadersBorderStyle,
            New DataGridViewAdvancedBorderStyle(), False, False))
        Dim borderAndPaddingHeight As Int32 = 2 +
            borderRect.Top + borderRect.Height +
            Me.InheritedStyle.Padding.Vertical
        Dim visualStylesEnabled As Boolean =
            Application.RenderWithVisualStyles AndAlso
            Me.DataGridView.EnableHeadersVisualStyles
        If visualStylesEnabled Then
            borderAndPaddingHeight += 3
        End If

        ' Constrain the button edge length to the height of the 
        ' column headers minus the border and padding height. 
        If buttonEdgeLength >
            Me.DataGridView.ColumnHeadersHeight -
            borderAndPaddingHeight Then
            buttonEdgeLength =
                Me.DataGridView.ColumnHeadersHeight - borderAndPaddingHeight
        End If

        ' Constrain the button edge length to the
        ' width of the cell minus three.
        If buttonEdgeLength > cellBounds.Width - 3 Then
            buttonEdgeLength = cellBounds.Width - 3
        End If

        ' Calculate the location of the drop-down button, with adjustments
        ' based on whether visual styles are enabled. 
        Dim topOffset As Int32
        If visualStylesEnabled Then
            topOffset = 4
        Else
            topOffset = 1
        End If

        Dim top As Int32 = cellBounds.Bottom - buttonEdgeLength - topOffset

        Dim leftOffset As Int32
        If visualStylesEnabled Then
            leftOffset = 3
        Else
            leftOffset = 1
        End If

        Dim left As Int32 = 0

        If Me.DataGridView.RightToLeft = RightToLeft.No Then
            left = cellBounds.Right - buttonEdgeLength - leftOffset
        Else
            left = cellBounds.Left + leftOffset
        End If

        ' Set the dropDownButtonBoundsValue value using the calculated 
        ' values, and adjust the cell padding accordingly.  
        dropDownButtonBoundsValue = New Rectangle(left, top, buttonEdgeLength, buttonEdgeLength)
        AdjustPadding((buttonEdgeLength + leftOffset))

    End Sub 'SetDropDownButtonBounds

    ''' <summary>
    ''' Adjusts the cell padding to widen the header by the drop-down button width.
    ''' </summary>
    ''' <param name="newDropDownButtonPaddingOffset">The new drop-down button width.</param>
    Private Sub AdjustPadding(ByVal newDropDownButtonPaddingOffset As Int32)

        ' Determine the difference between the new and current 
        ' padding adjustment.
        Dim widthChange As Int32 = newDropDownButtonPaddingOffset -
            currentDropDownButtonPaddingOffset

        ' If the padding needs to change, store the new value and 
        ' make the change.
        If widthChange <> 0 Then
            ' Store the offset for the drop-down button separately from 
            ' the padding in case the client needs additional padding.
            currentDropDownButtonPaddingOffset = newDropDownButtonPaddingOffset

            ' Create a new Padding using the adjustment amount, then add it
            ' to the cell's existing Style.Padding property value. 
            Dim dropDownPadding As Padding = New Padding(0, 0, widthChange, 0)
            Me.Style.Padding =
                Padding.Add(Me.InheritedStyle.Padding, dropDownPadding)
        End If

    End Sub

    ''' <summary>
    ''' The current width of the drop-down button. This field is used to adjust the cell padding.  
    ''' </summary>
    Private currentDropDownButtonPaddingOffset As Int32

#End Region 'button bounds

#Region "public properties: ComboBoxEnabled, DataSource, UseTagForSelectedValue, AutomaticSortingEnabled, DropDownListBoxMaxLines" '

    ''' <summary>
    ''' Indicates whether combobox is enabled for the owning column. 
    ''' </summary>
    Private comboBoxEnabledValue As Boolean = True

    ''' <summary>
    ''' Gets or sets a value indicating whether combobox is enabled.
    ''' </summary>
    <DefaultValue(True)>
    Public Property ComboBoxEnabled() As Boolean
        Get
            Return comboBoxEnabledValue
        End Get

        Set(ByVal value As Boolean)
            ' If filtering is disabled, remove the padding adjustment
            ' and invalidate the button bounds. 
            If Not value Then
                AdjustPadding(0)
                InvalidateDropDownButtonBounds()
            End If

            comboBoxEnabledValue = value
        End Set
    End Property

    ''' <summary>
    ''' Indicates whether to use column tag property for storing the selected value. 
    ''' </summary>
    Private _UseTagForSelectedValue As Boolean = True

    ''' <summary>
    ''' Gets or sets a value indicating whether to use column tag property for storing the selected value.
    ''' </summary>
    <DefaultValue(True)>
    Public Property UseTagForSelectedValue() As Boolean
        Get
            Return _UseTagForSelectedValue
        End Get
        Set(ByVal value As Boolean)
            _UseTagForSelectedValue = value
        End Set
    End Property

    ''' <summary>
    ''' Datasource for the combobox. 
    ''' </summary>
    Private _DataSource As IList = Nothing

    ''' <summary>
    ''' Gets or sets a data sorce for the combobox.
    ''' </summary>
    Public Property DataSource() As IList
        Get
            Return _DataSource
        End Get

        Set(ByVal value As IList)
            _DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' Indicates whether automatic sorting is enabled. 
    ''' </summary>
    Private automaticSortingEnabledValue As Boolean = True

    ''' <summary>
    ''' Gets or sets a value indicating whether automatic sorting is 
    ''' enabled for the owning column. 
    ''' </summary>
    <DefaultValue(True)> _
    Public Property AutomaticSortingEnabled() As Boolean
        Get
            Return automaticSortingEnabledValue
        End Get
        Set(ByVal value As Boolean)
            automaticSortingEnabledValue = value
            If OwningColumn IsNot Nothing Then
                If value Then
                    OwningColumn.SortMode = _
                        DataGridViewColumnSortMode.Programmatic
                Else
                    OwningColumn.SortMode = _
                        DataGridViewColumnSortMode.NotSortable
                End If
            End If
        End Set
    End Property

    ''' <summary>
    ''' The maximum number of lines in the drop-down list. 
    ''' </summary>
    Private dropDownListBoxMaxLinesValue As Int32 = 20

    ''' <summary>
    ''' Gets or sets the maximum number of lines to display in the drop-down filter list. 
    ''' The actual height of the drop-down list is constrained by the DataGridView height. 
    ''' </summary>
    <DefaultValue(20)> _
    Public Property DropDownListBoxMaxLines() As Int32
        Get
            Return dropDownListBoxMaxLinesValue
        End Get
        Set(ByVal value As Int32)
            dropDownListBoxMaxLinesValue = value
        End Set
    End Property

#End Region 'public properties

    ''' <summary>
    ''' Represents a ListBox control used as a drop-down filter list
    ''' in a DataGridView control.
    ''' </summary>
    Private Class FilterListBox
        Inherits ListBox

        ''' <summary>
        ''' Initializes a new instance of the FilterListBox class.
        ''' </summary>
        Public Sub New()
            Visible = False
            IntegralHeight = True
            BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
            TabStop = False
        End Sub

        ''' <summary>
        ''' Indicates that the FilterListBox will handle (or ignore) all 
        ''' keystrokes that are not handled by the operating system. 
        ''' </summary>
        ''' <param name="keyData">A Keys value that represents the keyboard input.</param>
        ''' <returns>true in all cases.</returns>
        Protected Overrides Function IsInputKey(ByVal keyData As Keys) As Boolean
            Return True
        End Function

        ''' <summary>
        ''' Processes a keyboard message directly, preventing it from being
        ''' intercepted by the parent DataGridView control.
        ''' </summary>
        ''' <param name="m">A Message, passed by reference, that 
        ''' represents the window message to process.</param>
        ''' <returns>true if the message was processed by the control;
        ''' otherwise, false.</returns>
        Protected Overrides Function ProcessKeyMessage(
            ByRef m As Message) As Boolean
            Return ProcessKeyEventArgs(m)
        End Function

    End Class 'FilterListBox

    Public Class SelectedValueChangedEventArgs
        Inherits EventArgs

        Private _Value As Object = Nothing
        Private _Column As DataGridViewColumn = Nothing

        Public ReadOnly Property Value() As Object
            Get
                Return _Value
            End Get
        End Property

        Public ReadOnly Property Column() As DataGridViewColumn
            Get
                Return _Column
            End Get
        End Property

        Public Sub New(nValue As Object, nColumn As DataGridViewColumn)
            _Value = nValue
            _Column = nColumn
        End Sub

    End Class

End Class 'DataGridViewAutoFilterColumnHeaderCell

