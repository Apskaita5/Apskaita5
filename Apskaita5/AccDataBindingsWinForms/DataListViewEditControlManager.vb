Imports AccControlsWinForms
Imports ApskaitaObjects
Imports ApskaitaObjects.Attributes
Imports BrightIdeasSoftware
Imports System.Reflection
Imports Csla.Core
Imports Csla.Validation
Imports System.Windows.Forms
Imports System.Drawing
Imports AccDataBindingsWinForms.CachedInfoLists

''' <summary>
''' Represents a DataListView wrapper that manages DataListView events and data formats:
''' - adds items when + or Ins is pressed (if the collection.AllowNew is true);
''' - removes selected items when - or Del is pressed (if the item's chronology validators
''' FinancialDataCanChange property allows to);
''' - sets regional labels (menu items) for the DataListView;
''' - sets columns formats subject to the underlying item type properties 
''' (IsEditable, HeaderTextAlign, Searchable, Sortable, UseFiltering, 
''' AspectToStringFormat (for numeric types and accounts) and TextAlign);
''' - sets default edit controls for numeric types, dates, strings, 
''' AccountInfo, CashAccountInfo, PersonGroupInfo and WarehouseInfo
''' (use AddCustomEditControl method to add custom controls);
''' - handles cell edit using default and custom controls set;
''' - checks if the property to be edited is not readonly 
''' using AspectNameIsReadOnly convention;
''' - handles ContextMenuStrip by rooting the items right clicked to the
''' provided business methods (see AddMenuItemHandler method);
''' - handles item double click by showing a dialog with the options available
''' or initializing an action if there is only one action available 
''' and AddCancelButton is set to false (see AddButtonHandler method);
''' - handles validation (error, warning, information icons, tooltips 
''' with the error/warning/information text) for the objects 
''' that derive from the BusinessBase class.
''' </summary>
''' <typeparam name="T">a type of the items in the DataListView</typeparam>
''' <remarks></remarks>
Public Class DataListViewEditControlManager(Of T)
    Implements IDisposable

    Private _CurrentListView As ObjectListView = Nothing


    ''' <summary>
    ''' Creates a new DataListViewEditControlManager instance for a DataListView.
    ''' </summary>
    ''' <param name="listView">a DataListView to be managed</param>
    ''' <param name="contextMenuStrip">a ContextMenuStrip that is displayed
    ''' when an item is right clicked (if any)</param>
    ''' <param name="itemsDeleteHandler">a method that deletes items from
    ''' the collection that is displayed in the parent DataListView (if any)</param>
    ''' <param name="itemAddHandler">a method that adds a new item to
    ''' the collection that is displayed in the parent DataListView (if any)</param>
    ''' <param name="itemActionIsAvailable">a method that checks if a
    ''' specified ContextMenuStrip menu item or dialog button is available
    ''' for the item clicked (if any)</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal listView As ObjectListView, ByVal contextMenuStrip As ContextMenuStrip, _
        ByVal itemsDeleteHandler As ItemsDelete, ByVal itemAddHandler As ItemAdd, _
        ByVal itemActionIsAvailable As ItemActionIsAvailable)

        If listView Is Nothing Then
            Throw New ArgumentNullException("listView")
        End If

        _CurrentListView = listView
        _IsValidationEnabled = GetType(T).IsSubclassOf(GetType(BusinessBase))
        _ContextMenuStrip = contextMenuStrip
        _ItemsDeleteHandler = itemsDeleteHandler
        _ItemAddHandler = itemAddHandler
        _ItemActionIsAvailable = itemActionIsAvailable

        RegionalizeListView(listView)

        InitializeControlsDictionary()

        If listView.CellEditActivation <> ObjectListView.CellEditActivateMode.None Then
            If MyCustomSettings.EditListViewWithDoubleClick Then
                listView.CellEditActivation = ObjectListView.CellEditActivateMode.DoubleClick
            Else
                listView.CellEditActivation = ObjectListView.CellEditActivateMode.SingleClickAlways
            End If
        End If

        AddHandler listView.CellEditStarting, AddressOf DataListView_CellEditStarting
        AddHandler listView.CellEditFinishing, AddressOf DataListView_CellEditFinishing
        If _IsValidationEnabled Then
            AddHandler listView.FormatCell, AddressOf DataListView_FormatCell
        End If
        AddHandler listView.CellToolTipShowing, AddressOf DataListView_CellToolTipShowing
        AddHandler listView.KeyDown, AddressOf DataListView_KeyDown
        If Not contextMenuStrip Is Nothing Then
            AddHandler listView.CellRightClick, AddressOf DataListView_CellRightClick
        End If
        AddHandler listView.CellClick, AddressOf DataListView_CellClick
        AddHandler listView.Disposed, AddressOf DataListView_Disposed

    End Sub

    Private Sub New()
        ' disallow default constructor
    End Sub


    ''' <summary>
    ''' Gets or sets whether the nested DataListView is readonly.
    ''' </summary>
    ''' <remarks></remarks>
    Public Property IsReadOnly() As Boolean
        Get
            Return _CurrentListView.CellEditActivation = ObjectListView.CellEditActivateMode.None
        End Get
        Set(ByVal value As Boolean)
            If value Then
                _CurrentListView.CellEditActivation = ObjectListView.CellEditActivateMode.None
            Else
                If MyCustomSettings.EditListViewWithDoubleClick Then
                    _CurrentListView.CellEditActivation = ObjectListView.CellEditActivateMode.DoubleClick
                Else
                    _CurrentListView.CellEditActivation = ObjectListView.CellEditActivateMode.SingleClickAlways
                End If
            End If
        End Set
    End Property


    ''' <summary>
    ''' Gets an array of business object indexes in the original collection
    ''' in order that the objects are displayed in the DataListView.
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetDisplayOrderIndexes() As List(Of Integer)

        Dim lastDisplayedItem As OLVListItem = _CurrentListView.GetLastItemInDisplayOrder()

        If lastDisplayedItem Is Nothing Then Return New List(Of Integer)

        Dim parentList As IList = Nothing

        If Not _CurrentListView.Objects Is Nothing Then

            If TypeOf _CurrentListView.Objects Is BindingSource Then
                Try
                    parentList = DirectCast(_CurrentListView.Objects, BindingSource).List
                Catch ex As Exception
                End Try
            Else
                Try
                    parentList = DirectCast(_CurrentListView.Objects, IList)
                Catch ex As Exception
                End Try
            End If

        End If

        If parentList Is Nothing Then Return New List(Of Integer)

        Dim result As New List(Of Integer)

        For i As Integer = 0 To _CurrentListView.GetDisplayOrderOfItemIndex(lastDisplayedItem.Index)
            Dim current As Object = _CurrentListView.GetNthItemInDisplayOrder(i).RowObject
            result.Add(parentList.IndexOf(current))
        Next

        Return result

    End Function

    ''' <summary>
    ''' Gets a localized human readable description of the filters that are
    ''' currently applyed to the DataListView.
    ''' </summary>
    ''' <remarks></remarks>
    Public Function GetCurrentFilterDescription() As String

        If Not IsFilteredView() Then Return ""

        Dim result As String = ""

        If Not _CurrentListView.AdditionalFilter Is Nothing _
            AndAlso TypeOf _CurrentListView.AdditionalFilter Is TextMatchFilter _
            AndAlso Not DirectCast(_CurrentListView.AdditionalFilter, TextMatchFilter).ContainsStrings Is Nothing _
            AndAlso DirectCast(_CurrentListView.AdditionalFilter, TextMatchFilter).ContainsStrings.Count > 0 Then

            result = AccCommon.AddWithNewLine(result, String.Format("Bendras tekstinis filtras: *{0}*", _
                DirectCast(_CurrentListView.AdditionalFilter, TextMatchFilter).ContainsStrings(0)), False)

        End If

        If Not _CurrentListView.GetFilteredColumns(View.Details) Is Nothing _
            AndAlso _CurrentListView.GetFilteredColumns(View.Details).Count > 0 Then

            For Each column As OLVColumn In _CurrentListView.GetFilteredColumns(View.Details)

                If Not column.ValuesChosenForFiltering Is Nothing _
                    AndAlso column.ValuesChosenForFiltering.Count > 0 Then

                    result = AccCommon.AddWithNewLine(result, String.Format("Filtro vertės stulpeliui ""{0}"": {1}", _
                        column.AspectName, FilterValueListToString(column.ValuesChosenForFiltering)), False)

                End If

            Next

        End If

        Return result

    End Function

    Private Function FilterValueListToString(ByVal list As IList) As String

        Dim result As New List(Of String)

        For Each o As Object In list
            result.Add(o.ToString)
        Next

        Return String.Join(" / ", result.ToArray)

    End Function

    ''' <summary>
    ''' Indicates if there are any filters currently applyed to the DataListView.
    ''' </summary>
    ''' <remarks></remarks>
    Public Function IsFilteredView() As Boolean

        If Not _CurrentListView.AdditionalFilter Is Nothing _
            AndAlso TypeOf _CurrentListView.AdditionalFilter Is TextMatchFilter _
            AndAlso Not DirectCast(_CurrentListView.AdditionalFilter, TextMatchFilter).ContainsStrings Is Nothing _
            AndAlso DirectCast(_CurrentListView.AdditionalFilter, TextMatchFilter).ContainsStrings.Count > 0 Then

            Return True

        End If

        If Not _CurrentListView.AdditionalFilter Is Nothing _
            AndAlso TypeOf _CurrentListView.AdditionalFilter Is TextMatchFilter Then

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' Sets a DataListView column for a given AspectName as readonly.
    ''' </summary>
    ''' <param name="aspectName">an aspect name</param>
    ''' <param name="isReadOnly">whether the column should be readonly</param>
    ''' <remarks></remarks>
    Public Sub SetColumnReadOnly(ByVal aspectName As String, ByVal isReadOnly As Boolean)

        If _CurrentListView Is Nothing Then Exit Sub

        For Each col As OLVColumn In _CurrentListView.Columns

            If col.AspectName.Trim.ToLower = aspectName.Trim.ToLower Then

                col.IsEditable = Not isReadOnly
                Exit For

            End If

        Next

    End Sub


    Private Sub RegionalizeListView(ByVal listView As ObjectListView)

        FilterMenuBuilder.APPLY_LABEL = "Taikyti filtrą"
        FilterMenuBuilder.FILTERING_LABEL = "Filtruoti"
        FilterMenuBuilder.SELECT_ALL_LABEL = "Pasirinkti visus"
        FilterMenuBuilder.CLEAR_ALL_FILTERS_LABEL = "Pašalinti filtrus"
        listView.MenuLabelColumns = "Stulpeliai"
        listView.MenuLabelGroupBy = "Grupuoti pagal {0}"
        listView.MenuLabelLockGroupingOn = "Užrakinti grupavimą pagal {0}"
        listView.MenuLabelSelectColumns = "Stulpeliai"
        listView.MenuLabelSortAscending = "Rūšiuoti didėjančia tvarka pagal {0}"
        listView.MenuLabelSortDescending = "Rūšiuoti mažėjančia tvarka pagal {0}"
        listView.MenuLabelTurnOffGroups = "Pašalinti grupavimą pagal {0}"
        listView.MenuLabelUnlockGroupingOn = "Atrakinti grupavimą pagal {0}"
        listView.MenuLabelUnsort = "Pašalinti rūšiavimą pagal {0}"

        listView.SelectColumnsMenuStaysOpen = True
        listView.HotTracking = False
        listView.UseHotControls = False

        If listView.CellEditActivation <> ObjectListView.CellEditActivateMode.None Then

            Dim textOverlay As TextOverlay = listView.EmptyListMsgOverlay
            textOverlay.Text = "Norėdami pridėti eilutę paspauskite + arba Insert mygtuką."
            textOverlay.TextColor = Color.Firebrick
            textOverlay.BackColor = Color.AntiqueWhite
            textOverlay.BorderColor = Color.DarkRed
            textOverlay.BorderWidth = 4.0F
            textOverlay.Font = New Font("Chiller", 36)
            textOverlay.Rotation = -5

        End If

    End Sub


#Region " Dialog And Context Menu Methods "

    ''' <summary>
    ''' delegate that the parent form shall implement in order to provide
    ''' functionality for custom business actions when a dialog button or 
    ''' a ContextMenuStrip menu item is clicked for a selected business object
    ''' (see AddMenuItemHandler and AddButtonHandler methods)
    ''' </summary>
    ''' <param name="item">an item that is clicked</param>
    ''' <remarks></remarks>
    Public Delegate Sub ItemAction(ByVal item As T)

    ''' <summary>
    '''  delegate that the parent form shall implement in order to provide
    ''' functionality for customizing dialog and ContextMenuStrip menu items for 
    ''' a selected business object
    ''' </summary>
    ''' <param name="selectedItem">an item that is clicked</param>
    ''' <param name="actionName">a name of the business method in the parent form
    ''' that handles a ContextMenuStrip menu item or a dialog button</param>
    ''' <remarks></remarks>
    Public Delegate Function ItemActionIsAvailable(ByVal selectedItem As T, _
        ByVal actionName As String) As Boolean


    Private _ContextMenuStrip As ContextMenuStrip = Nothing
    Private _Handledbuttons As New Dictionary(Of String, KeyValuePair(Of String, ItemAction))
    Private _AddCancelButton As Boolean = True
    Private _DialogText As String = ""
    Private _ItemActionIsAvailable As ItemActionIsAvailable = Nothing


    ''' <summary>
    ''' Gets or sets whether a cancel button should be added to the dialog
    ''' that is displayed when an item is double clicked.
    ''' </summary>
    ''' <remarks></remarks>
    Public Property AddCancelButton() As Boolean
        Get
            Return _AddCancelButton
        End Get
        Set(ByVal value As Boolean)
            _AddCancelButton = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a text of the dialog that is displayed when an item is double clicked.
    ''' </summary>
    ''' <remarks></remarks>
    Public Property DialogText() As String
        Get
            Return _DialogText
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then value = ""
            _DialogText = value
        End Set
    End Property


    ''' <summary>
    ''' Adds a binding between a ContextMenuStrip menu item and a business action
    ''' (method) in the parent form.
    ''' </summary>
    ''' <param name="menuItem">a menu item</param>
    ''' <param name="itemActionHandler">an action (method) to invoke when 
    ''' the menu item is clicked</param>
    ''' <remarks></remarks>
    Public Sub AddMenuItemHandler(ByVal menuItem As ToolStripMenuItem, _
        ByVal itemActionHandler As ItemAction)

        If menuItem Is Nothing Then Throw New ArgumentNullException("menuItem")
        If itemActionHandler Is Nothing Then Throw New ArgumentNullException("itemActionHandler")

        menuItem.Tag = itemActionHandler
        AddHandler menuItem.Click, AddressOf MenuItem_Click

    End Sub

    ''' <summary>
    ''' Adds a binding between a dialog button and a business action
    ''' (method) in the parent form. (a dialog is displayed when an item is double clicked)
    ''' </summary>
    ''' <param name="caption">a text on the button</param>
    ''' <param name="toolTip">a tooltip text for the button</param>
    ''' <param name="itemActionHandler">an action (method) to invoke when 
    ''' the button is clicked</param>
    ''' <remarks>if only one button binding is added and <see cref="AddCancelButton">AddCancelButton</see>
    ''' is set to FALSE, the action is invoked without showing a dialog</remarks>
    Public Sub AddButtonHandler(ByVal caption As String, ByVal toolTip As String, _
        ByVal itemActionHandler As ItemAction)

        If Not _Handledbuttons.ContainsKey(caption.Trim) Then
            _Handledbuttons.Add(caption.Trim, New KeyValuePair(Of String, ItemAction) _
            (toolTip, itemActionHandler))
        End If

    End Sub


    Private Sub DataListView_CellRightClick(ByVal sender As Object, _
            ByVal e As CellRightClickEventArgs)

        If _ContextMenuStrip Is Nothing Then Exit Sub

        Dim currentItem As T = Nothing
        Try
            currentItem = DirectCast(e.Model, T)
        Catch ex As Exception
        End Try
        If currentItem Is Nothing Then Exit Sub

        If Not ConfigureContextMenuStrip(currentItem) Then Exit Sub

        _ContextMenuStrip.Tag = currentItem

        e.MenuStrip = _ContextMenuStrip

    End Sub

    Private Sub DataListView_CellClick(ByVal sender As Object, ByVal e As CellClickEventArgs)

        If e.ClickCount <> 2 OrElse e.Model Is Nothing Then Exit Sub

        If _CurrentListView.CellEditActivation <> ObjectListView.CellEditActivateMode.None _
            AndAlso Not e.Column Is Nothing AndAlso e.Column.IsEditable Then Exit Sub

        If _Handledbuttons.Count < 1 Then Exit Sub

        Dim currentItem As T = Nothing
        Try
            currentItem = DirectCast(e.Model, T)
        Catch ex As Exception
        End Try
        If currentItem Is Nothing Then Exit Sub

        ' no point in showing dialog with a single button
        If _Handledbuttons.Count = 1 AndAlso Not _AddCancelButton Then

            Dim singleAction As ItemAction = _Handledbuttons.Values(0).Value

            If singleAction Is Nothing OrElse (Not _ItemActionIsAvailable Is Nothing AndAlso _
                Not _ItemActionIsAvailable.Invoke(currentItem, singleAction.Method.Name)) Then

                Exit Sub

            End If

            singleAction.Invoke(currentItem)

            Exit Sub

        End If

        Dim buttons As ButtonStructure() = GetButtons(currentItem)

        If buttons Is Nothing OrElse buttons.Length < 1 Then Exit Sub

        Dim answer As String = Ask(_DialogText, buttons)

        If Not _Handledbuttons.ContainsKey(answer.Trim) OrElse _
            _Handledbuttons(answer.Trim).Value Is Nothing Then Exit Sub

        _Handledbuttons(answer.Trim).Value.Invoke(currentItem)

    End Sub

    Private Sub MenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)

        If _ContextMenuStrip Is Nothing OrElse _ContextMenuStrip.Tag Is Nothing _
            OrElse Not TypeOf _ContextMenuStrip.Tag Is T Then Exit Sub

        Dim clickedItem As ToolStripMenuItem = Nothing
        Try
            clickedItem = DirectCast(sender, ToolStripMenuItem)
        Catch ex As Exception
        End Try
        If clickedItem Is Nothing OrElse clickedItem.Tag Is Nothing _
            OrElse Not TypeOf clickedItem.Tag Is ItemAction Then Exit Sub

        If _ContextMenuStrip.Tag Is Nothing Then
            DirectCast(clickedItem.Tag, ItemAction).Invoke(Nothing)
        Else
            DirectCast(clickedItem.Tag, ItemAction).Invoke(DirectCast(_ContextMenuStrip.Tag, T))
        End If

    End Sub


    Private Function ConfigureContextMenuStrip(ByVal item As T) As Boolean

        If _ContextMenuStrip Is Nothing OrElse Not _ContextMenuStrip.Items.Count > 0 Then Return False

        If _ItemActionIsAvailable Is Nothing Then Return True

        Dim anyActionAvailable As Boolean = False

        For Each child As ToolStripItem In _ContextMenuStrip.Items
            If TypeOf child Is ToolStripMenuItem Then
                ConfigureContextMenuStrip(item, DirectCast(child, ToolStripMenuItem), anyActionAvailable)
            End If
        Next

        Return anyActionAvailable

    End Function

    Private Sub ConfigureContextMenuStrip(ByVal item As T, ByRef menuItem As ToolStripMenuItem, _
        ByRef anyActionAvailable As Boolean)

        If Not menuItem.Tag Is Nothing AndAlso TypeOf menuItem.Tag Is ItemAction Then

            If Not _ItemActionIsAvailable Is Nothing Then
                menuItem.Available = _ItemActionIsAvailable.Invoke(item, _
                    DirectCast(menuItem.Tag, ItemAction).Method.Name)
            Else
                menuItem.Available = True
            End If

            If menuItem.Available Then anyActionAvailable = True

        End If

        For Each child As ToolStripItem In menuItem.DropDownItems
            If TypeOf child Is ToolStripMenuItem Then
                ConfigureContextMenuStrip(item, DirectCast(child, ToolStripMenuItem), anyActionAvailable)
            End If
        Next

    End Sub

    Private Function GetButtons(ByVal item As T) As ButtonStructure()

        Dim result As New List(Of ButtonStructure)

        For Each k As KeyValuePair(Of String, KeyValuePair(Of String, ItemAction)) In _Handledbuttons

            If Not k.Value.Value Is Nothing Then

                If _ItemActionIsAvailable Is Nothing OrElse _
                    _ItemActionIsAvailable.Invoke(item, k.Value.Value.Method.Name) Then

                    result.Add(New ButtonStructure(k.Key, k.Value.Key))

                End If

            End If

        Next

        If result.Count < 1 Then Return Nothing

        If _AddCancelButton Then result.Add(New ButtonStructure( _
            "Atšaukti", "Nieko nedaryti."))

        Return result.ToArray

    End Function

#End Region

#Region " Edit And Format methods "

    Private _ControlsDictionary As New Dictionary(Of String, Control)


    ''' <summary>
    ''' Adds a custom control that should be used when editing a particular column/property.
    ''' </summary>
    ''' <param name="propName">a name of the property</param>
    ''' <param name="cntr">a control to use for editing the property</param>
    ''' <remarks></remarks>
    Public Sub AddCustomEditControl(ByVal propName As String, ByVal cntr As Control)

        If propName Is Nothing OrElse String.IsNullOrEmpty(propName.Trim) Then
            Throw New ArgumentNullException("propName")
        ElseIf cntr Is Nothing Then
            Throw New ArgumentNullException("cntr")
        ElseIf GetType(T).GetProperty(propName) Is Nothing Then
            Throw New ArgumentException(String.Format("Property {0} does not exists on type {1}.", _
                propName, GetType(T).FullName), "propName")
        End If

        If _ControlsDictionary.ContainsKey(propName) Then
            _ControlsDictionary(propName).Dispose()
            _ControlsDictionary.Remove(propName)
        End If

        _ControlsDictionary.Add(propName, cntr)

    End Sub


    Private Sub InitializeControlsDictionary()

        For Each col As OLVColumn In _CurrentListView.Columns

            If Not StringIsNullOrEmpty(col.AspectName) Then

                Dim curProp As PropertyInfo = GetBindingProperty(Of T)(col.AspectName)

                If Not curProp Is Nothing Then SetColumnFormat(curProp, col)

                If Not curProp Is Nothing AndAlso curProp.CanWrite Then

                    If curProp.PropertyType Is GetType(Double) Then
                        AddDoubleControl(curProp)
                    ElseIf curProp.PropertyType Is GetType(Integer) Then
                        AddIntegerControl(curProp)
                    ElseIf curProp.PropertyType Is GetType(Long) Then
                        AddAccountInfoControl(curProp)
                    ElseIf curProp.PropertyType Is GetType(String) Then
                        AddStringControl(curProp)
                    ElseIf curProp.PropertyType Is GetType(Date) Then
                        AddDateControl(curProp)
                    ElseIf curProp.PropertyType Is GetType(HelperLists.CashAccountInfoList) Then
                        AddCashAccountInfoListControl(curProp)
                    ElseIf curProp.PropertyType Is GetType(HelperLists.PersonGroupInfo) Then
                        AddPersonGroupInfoListControl(curProp)
                    ElseIf curProp.PropertyType Is GetType(HelperLists.WarehouseInfo) Then
                        AddWarehouseInfoListControl(curProp)
                    End If

                End If

            End If

        Next

    End Sub


    Private Sub SetColumnFormat(ByVal propInfo As PropertyInfo, ByVal column As OLVColumn)

        If propInfo Is Nothing Then
            Throw New ArgumentNullException("propInfo")
        ElseIf column Is Nothing Then
            Throw New ArgumentNullException("column")
        End If

        column.IsEditable = propInfo.CanWrite
        If propInfo.CanWrite Then
            column.CellEditUseWholeCell = True
        End If
        column.HeaderTextAlign = HorizontalAlignment.Center
        column.Searchable = True
        column.Sortable = True
        column.UseFiltering = True

        If Not column.AspectToStringFormat Is Nothing AndAlso _
            Not String.IsNullOrEmpty(column.AspectToStringFormat.Trim) Then Exit Sub

        If propInfo.PropertyType Is GetType(Double) Then

            Dim curAttribute As DoubleFieldAttribute = _
                GetAttribute(Of DoubleFieldAttribute)(propInfo)

            If curAttribute Is Nothing Then
                column.AspectToStringFormat = "{0:##,0.00}"
            Else
                column.AspectToStringFormat = "{0:##,0." _
                    & "".PadRight(curAttribute.Round, "0") & "}"
            End If

            column.TextAlign = HorizontalAlignment.Center


        ElseIf propInfo.PropertyType Is GetType(Date) Then

            column.AspectToStringFormat = "{0:yyyy-MM-dd}"
            column.TextAlign = HorizontalAlignment.Center

        ElseIf propInfo.PropertyType Is GetType(Integer) Then

            column.TextAlign = HorizontalAlignment.Center

        ElseIf propInfo.PropertyType Is GetType(Long) Then

            Dim curAttribute As AccountFieldAttribute = _
                GetAttribute(Of AccountFieldAttribute)(propInfo)

            If Not curAttribute Is Nothing Then
                column.AspectToStringFormat = "{0:##;(##);''}"
            End If

            column.TextAlign = HorizontalAlignment.Center

        End If

    End Sub


    Private Sub AddDoubleControl(ByVal curProp As PropertyInfo)

        If curProp Is Nothing Then
            Throw New ArgumentNullException("curProp")
        End If

        Dim curAttribute As DoubleFieldAttribute = _
            GetAttribute(Of DoubleFieldAttribute)(curProp)

        Dim control As New AccTextBox
        control.KeepBackColorWhenReadOnly = False
        control.TextAlign = HorizontalAlignment.Center

        If curAttribute Is Nothing Then

            control.DecimalLength = 2
            control.NegativeValue = False

        Else

            control.DecimalLength = curAttribute.Round
            control.NegativeValue = curAttribute.AllowNegative

        End If

        _ControlsDictionary.Add(curProp.Name, control)

    End Sub

    Private Sub AddIntegerControl(ByVal curProp As PropertyInfo)

        If curProp Is Nothing Then
            Throw New ArgumentNullException("curProp")
        End If

        Dim curAttribute As IntegerFieldAttribute = _
            GetAttribute(Of IntegerFieldAttribute)(curProp)

        If curAttribute Is Nothing Then

            If curProp.Name.ToLower.Contains("correction") Then

                Dim control As New NumericUpDown
                control.DecimalPlaces = 0
                control.Increment = 1
                control.Maximum = 100
                control.Minimum = -100
                control.TextAlign = HorizontalAlignment.Center

                _ControlsDictionary.Add(curProp.Name, control)

            Else

                Dim control As New AccTextBox
                control.KeepBackColorWhenReadOnly = False
                control.TextAlign = HorizontalAlignment.Center
                control.DecimalLength = 0
                control.NegativeValue = True

                _ControlsDictionary.Add(curProp.Name, control)

            End If

        Else

            If curProp.Name.ToLower.Contains("correction") Then

                Dim control As New NumericUpDown
                control.DecimalPlaces = 0
                control.Increment = 1
                control.TextAlign = HorizontalAlignment.Center
                If curAttribute.WithinRange Then
                    control.Maximum = curAttribute.MaxValue
                    control.Minimum = curAttribute.MinValue
                ElseIf curAttribute.AllowNegative Then
                    control.Maximum = 100
                    control.Minimum = -100
                Else
                    control.Maximum = 100
                    control.Minimum = 0
                End If

                _ControlsDictionary.Add(curProp.Name, control)

            Else

                Dim control As New AccTextBox
                control.KeepBackColorWhenReadOnly = False
                control.TextAlign = HorizontalAlignment.Center
                control.DecimalLength = 0
                control.NegativeValue = curAttribute.AllowNegative

                _ControlsDictionary.Add(curProp.Name, control)

            End If

        End If

    End Sub

    Private Sub AddAccountInfoControl(ByVal curProp As PropertyInfo)

        If curProp Is Nothing Then
            Throw New ArgumentNullException("curProp")
        End If

        Dim curAttribute As AccountFieldAttribute = _
            GetAttribute(Of AccountFieldAttribute)(curProp)

        If curAttribute Is Nothing Then

            AddIntegerControl(curProp)

        Else

            Dim control As New AccListComboBox
            LoadAccountInfoListToListCombo(control, curAttribute.ValueRequired _
                <> ValueRequiredLevel.Mandatory, curAttribute.AcceptedClasses)

            _ControlsDictionary.Add(curProp.Name, control)

        End If

    End Sub

    Private Sub AddStringControl(ByVal curProp As PropertyInfo)

        If curProp Is Nothing Then
            Throw New ArgumentNullException("curProp")
        End If

        Dim curAttribute As StringFieldAttribute = _
            GetAttribute(Of StringFieldAttribute)(curProp)

        Dim control As New TextBox

        If curAttribute Is Nothing Then

            control.MaxLength = 255

        Else

            control.MaxLength = curAttribute.MaxLength
            If control.MaxLength > 255 Then
                control.Multiline = True
                control.WordWrap = True
                control.ScrollBars = ScrollBars.Both
            Else
                control.Multiline = False
            End If

        End If

        _ControlsDictionary.Add(curProp.Name, control)

    End Sub

    Private Sub AddDateControl(ByVal curProp As PropertyInfo)

        If curProp Is Nothing Then
            Throw New ArgumentNullException("curProp")
        End If

        Dim control As New DateTimePicker
        control.Format = DateTimePickerFormat.Short

        _ControlsDictionary.Add(curProp.Name, control)

    End Sub

    Private Sub AddCashAccountInfoListControl(ByVal curProp As PropertyInfo)

        If curProp Is Nothing Then
            Throw New ArgumentNullException("curProp")
        End If

        Dim control As New AccListComboBox
        LoadCashAccountInfoListToListCombo(control, True)

        _ControlsDictionary.Add(curProp.Name, control)

    End Sub

    Private Sub AddPersonGroupInfoListControl(ByVal curProp As PropertyInfo)

        If curProp Is Nothing Then
            Throw New ArgumentNullException("curProp")
        End If

        Dim control As New AccListComboBox
        LoadPersonGroupInfoListToListCombo(control)

        _ControlsDictionary.Add(curProp.Name, control)

    End Sub

    Private Sub AddWarehouseInfoListControl(ByVal curProp As PropertyInfo)

        If curProp Is Nothing Then
            Throw New ArgumentNullException("curProp")
        End If

        Dim control As New AccListComboBox
        LoadWarehouseInfoListToListCombo(control, True)

        _ControlsDictionary.Add(curProp.Name, control)

    End Sub

    Private Sub DataListView_CellEditFinishing(ByVal sender As Object, ByVal e As CellEditEventArgs)

        If e.Cancel Then Exit Sub

        Dim currentControl As Control = Nothing
        Try
            currentControl = _ControlsDictionary(e.Column.AspectName)
        Catch ex As Exception
        End Try
        If currentControl Is Nothing Then Exit Sub

        If TypeOf currentControl Is NumericUpDown Then
            If GetPropertyType(e.Column.AspectName) Is GetType(Long) Then
                e.NewValue = Convert.ToInt64(DirectCast(currentControl, NumericUpDown).Value)
            Else
                e.NewValue = Convert.ToInt32(DirectCast(currentControl, NumericUpDown).Value)
            End If
        ElseIf TypeOf currentControl Is AccTextBox Then
            If GetPropertyType(e.Column.AspectName) Is GetType(Long) Then
                e.NewValue = Convert.ToInt64(DirectCast(currentControl, AccTextBox).DecimalValue)
            ElseIf GetPropertyType(e.Column.AspectName) Is GetType(Integer) Then
                e.NewValue = Convert.ToInt32(DirectCast(currentControl, AccTextBox).DecimalValue)
            Else
                e.NewValue = Convert.ToDouble(DirectCast(currentControl, AccTextBox).DecimalValue)
            End If
        ElseIf TypeOf currentControl Is AccListComboBox Then
            e.NewValue = DirectCast(currentControl, AccListComboBox).SelectedValue
        ElseIf TypeOf currentControl Is TextBox Then
            e.NewValue = DirectCast(currentControl, TextBox).Text
        ElseIf TypeOf currentControl Is ComboBox Then
            e.NewValue = DirectCast(currentControl, ComboBox).SelectedValue
        ElseIf TypeOf currentControl Is DateTimePicker Then
            e.NewValue = DirectCast(currentControl, DateTimePicker).Value
        Else
            Throw New NotImplementedException(String.Format("Control of type {0} is not implemented.", _
                currentControl.GetType.FullName))
        End If

    End Sub

    Private Sub DataListView_CellEditStarting(ByVal sender As Object, _
        ByVal e As CellEditEventArgs)

        Try
            If PropertyIsReadOnly(DirectCast(e.RowObject, T), e.Column.AspectName) Then
                e.Cancel = True
                Exit Sub
            End If
        Catch ex As Exception
        End Try

        If _ControlsDictionary.ContainsKey(e.Column.AspectName) Then

            Dim currentControl As Control = _ControlsDictionary(e.Column.AspectName)

            If TypeOf currentControl Is NumericUpDown Then
                DirectCast(currentControl, NumericUpDown).Value = e.Value
            ElseIf TypeOf currentControl Is AccTextBox Then
                DirectCast(currentControl, AccTextBox).DecimalValue = e.Value
            ElseIf TypeOf currentControl Is AccListComboBox Then
                DirectCast(currentControl, AccListComboBox).SelectedValue = e.Value
                DirectCast(currentControl, AccListComboBox).FilterString = ""
            ElseIf TypeOf currentControl Is TextBox Then
                DirectCast(currentControl, TextBox).Text = e.Value
            ElseIf TypeOf currentControl Is ComboBox Then
                DirectCast(currentControl, ComboBox).SelectedValue = e.Value
            ElseIf TypeOf currentControl Is DateTimePicker Then
                DirectCast(currentControl, DateTimePicker).Value = e.Value
            Else
                Throw New NotImplementedException(String.Format("Control of type {0} is not implemented.", _
                    currentControl.GetType.FullName))
            End If

            currentControl.Bounds = e.CellBounds
            e.AutoDispose = False

            e.Control = currentControl

        End If

    End Sub


    Private Function PropertyIsReadOnly(ByVal item As T, ByVal aspectName As String) As Boolean

        If item Is Nothing OrElse aspectName Is Nothing _
            OrElse String.IsNullOrEmpty(aspectName.Trim) Then Return False

        Dim result As Boolean = False

        Try
            result = DirectCast(GetType(T).GetProperty(aspectName _
                & "IsReadOnly").GetValue(item, Nothing), Boolean)
        Catch ex As Exception
        End Try

        Return result

    End Function

    Private Function GetPropertyType(ByVal aspectName As String) As Type

        Dim propInfo As PropertyInfo = Nothing
        Try
            propInfo = GetBindingProperty(Of T)(aspectName)
        Catch ex As Exception
        End Try
        If propInfo Is Nothing Then Return Nothing

        Return propInfo.PropertyType

    End Function

#End Region

#Region " Validation Methods "

    Private _IsValidationEnabled As Boolean = False


    Private Sub DataListView_FormatCell(ByVal sender As Object, ByVal e As FormatCellEventArgs)

        Dim current As BusinessBase = Nothing
        Dim propName As String = e.Column.AspectName

        Try
            If e.Column.AspectName.Contains(".") Then
                Dim propInfo As PropertyInfo = GetType(T).GetProperty(e.Column.AspectName. _
                    Split(New Char() {"."c}, StringSplitOptions.RemoveEmptyEntries)(0))
                propName = e.Column.AspectName.Split(New Char() {"."c}, StringSplitOptions.RemoveEmptyEntries)(1)
                current = DirectCast(propInfo.GetValue(e.Model, Nothing), BusinessBase)
            Else
                current = DirectCast(e.Model, BusinessBase)
            End If
        Catch ex As Exception
        End Try

        If current Is Nothing Then Exit Sub

        Dim errors As String = ""
        Dim warnings As String = ""
        Dim information As String = ""

        For Each br As BrokenRule In current.BrokenRulesCollection
            If br.Property.Trim.ToLower = propName.Trim.ToLower() Then
                If br.Severity = RuleSeverity.Error Then
                    errors = errors & vbCrLf & br.Description
                ElseIf br.Severity = RuleSeverity.Warning Then
                    warnings = warnings & vbCrLf & br.Description
                Else
                    information = information & vbCrLf & br.Description
                End If
            End If
        Next

        If Not String.IsNullOrEmpty(errors.Trim) Then
            e.SubItem.Decoration = New ImageDecoration(My.Resources.ErrorIcon_16x16, _
                ContentAlignment.MiddleRight)
        ElseIf Not String.IsNullOrEmpty(warnings.Trim) Then
            e.SubItem.Decoration = New ImageDecoration(My.Resources.WarningIcon_16x16, _
                ContentAlignment.MiddleRight)
        ElseIf Not String.IsNullOrEmpty(information.Trim) Then
            e.SubItem.Decoration = New ImageDecoration(My.Resources.InformationIcon_16x16, _
                ContentAlignment.MiddleRight)
        Else
            e.SubItem.Decoration = Nothing
        End If

    End Sub

    Private Sub DataListView_CellToolTipShowing(ByVal sender As Object, ByVal e As ToolTipShowingEventArgs)

        If Not _IsValidationEnabled Then

            If Not e.SubItem.Text Is Nothing AndAlso e.SubItem.Text.Length > 100 Then
                e.StandardIcon = ToolTipControl.StandardIcons.None
                e.Title = ""
                e.Text = e.SubItem.Text
            Else
                e.SubItem.Text = ""
            End If

            Exit Sub

        End If

        Dim current As BusinessBase = Nothing
        Dim propName As String = e.Column.AspectName

        Try
            If e.Column.AspectName.Contains(".") Then
                Dim propInfo As PropertyInfo = GetType(T).GetProperty(e.Column.AspectName. _
                    Split(New Char() {"."c}, StringSplitOptions.RemoveEmptyEntries)(0))
                propName = e.Column.AspectName.Split(New Char() {"."c}, StringSplitOptions.RemoveEmptyEntries)(1)
                current = DirectCast(propInfo.GetValue(e.Model, Nothing), BusinessBase)
            Else
                current = DirectCast(e.Model, BusinessBase)
            End If
        Catch ex As Exception
        End Try

        If current Is Nothing Then Exit Sub

        Dim errors As String = ""
        Dim warnings As String = ""
        Dim information As String = ""

        For Each br As BrokenRule In current.BrokenRulesCollection
            If br.Property.Trim.ToLower = propName.Trim.ToLower Then
                If br.Severity = RuleSeverity.Error Then
                    errors = errors & vbCrLf & br.Description
                ElseIf br.Severity = RuleSeverity.Warning Then
                    warnings = warnings & vbCrLf & br.Description
                Else
                    information = information & vbCrLf & br.Description
                End If
            End If
        Next

        If Not String.IsNullOrEmpty(errors.Trim) Then

            e.StandardIcon = ToolTipControl.StandardIcons.Error
            e.Title = "Klaida"
            e.Text = errors.Trim

        ElseIf Not String.IsNullOrEmpty(warnings.Trim) Then

            e.StandardIcon = ToolTipControl.StandardIcons.Warning
            e.Title = "Įspėjimas"
            e.Text = warnings.Trim

        ElseIf Not String.IsNullOrEmpty(information.Trim) Then

            e.StandardIcon = ToolTipControl.StandardIcons.Info
            e.Title = "Informacija"
            e.Text = information.Trim

        ElseIf Not e.SubItem.Text Is Nothing AndAlso e.SubItem.Text.Length > 100 Then

            e.StandardIcon = ToolTipControl.StandardIcons.None
            e.Title = ""
            e.Text = e.SubItem.Text

        Else
            e.SubItem.Text = ""
        End If

    End Sub

#End Region

#Region " Add Or Remove Items Methods "

    ''' <summary>
    ''' a delegate that the parent form shall implement in order to provide
    ''' functionality for deleting items
    ''' </summary>
    ''' <param name="item">an array of items to delete</param>
    ''' <remarks>item's chronology validators (all if any) FinancialDataCanChange 
    ''' property is checked before invoking the form delegate</remarks>
    Public Delegate Sub ItemsDelete(ByVal item As T())

    ''' <summary>
    ''' a delegate that the parent form shall implement in order to provide
    ''' functionality for adding new items
    ''' </summary>
    ''' <remarks>collection AllowNew property is checked before invoking the form delegate</remarks>
    Public Delegate Sub ItemAdd()


    Private _ItemsDeleteHandler As ItemsDelete = Nothing
    Private _ItemAddHandler As ItemAdd = Nothing

    Private Sub DataListView_KeyDown(ByVal sender As Object, _
        ByVal e As Windows.Forms.KeyEventArgs)

        If (e.KeyData = Keys.Delete OrElse e.KeyData = Keys.Subtract) _
            AndAlso Not _ItemsDeleteHandler Is Nothing _
            AndAlso Not _CurrentListView.SelectedObjects Is Nothing _
            AndAlso _CurrentListView.SelectedObjects.Count > 0 Then

            Dim message As String = ""
            For Each obj As Object In _CurrentListView.SelectedObjects
                If Not CanDeleteItem(DirectCast(obj, T), message) Then
                    MsgBox(message, MsgBoxStyle.Exclamation, "Klaida")
                    e.Handled = True
                    e.SuppressKeyPress = True
                    Exit Sub
                End If
            Next

            Dim objectsToDelete As New List(Of T)

            For Each obj As Object In _CurrentListView.SelectedObjects
                objectsToDelete.Add(DirectCast(obj, T))
            Next

            _ItemsDeleteHandler.Invoke(objectsToDelete.ToArray())

            e.Handled = True
            e.SuppressKeyPress = True

        ElseIf (e.KeyData = Keys.Add OrElse e.KeyData = Keys.Insert) _
            AndAlso Not _ItemAddHandler Is Nothing Then

            If CanAddItem() Then
                _ItemAddHandler.Invoke()
                Try
                    _CurrentListView.EnsureVisible(_CurrentListView.Items.Count - 1)
                Catch ex As Exception
                End Try
            End If

            e.Handled = True
            e.SuppressKeyPress = True

        End If

    End Sub


    Private Function CanDeleteItem(ByVal item As T, ByRef message As String) As Boolean

        If item Is Nothing Then Return False

        message = ""

        For Each propInfo As PropertyInfo In GetType(T).GetProperties()

            If propInfo.PropertyType Is GetType(IChronologicValidator) _
                OrElse Not Array.IndexOf(propInfo.PropertyType.GetInterfaces(), _
                GetType(IChronologicValidator)) < 0 Then

                Dim validator As IChronologicValidator = DirectCast( _
                    propInfo.GetValue(item, Nothing), IChronologicValidator)

                If Not validator Is Nothing AndAlso Not validator.FinancialDataCanChange Then

                    message = String.Format("Eilutės ""{0}"" pašalinti neleidžiama:{1}{2}", _
                        item.ToString, vbCrLf, validator.FinancialDataCanChangeExplanation)
                    Return False

                ElseIf Not validator Is Nothing AndAlso Not validator.ParentFinancialDataCanChange Then

                    message = String.Format("Dokumento eilučių pašalinti neleidžiama:{0}{1}", _
                        vbCrLf, validator.ParentFinancialDataCanChangeExplanation)
                    Return False

                End If

            End If

        Next

        Return True

    End Function

    Private Function CanAddItem() As Boolean

        Try
            Return DirectCast(GetType(T).GetProperty("Parent", BindingFlags.Instance _
                Or BindingFlags.NonPublic).GetValue(_CurrentListView.Objects(0), Nothing).AllowNew, Boolean)
        Catch ex As Exception
            Return True
        End Try

    End Function

#End Region

#Region " Cleanup Methods "

    ' because cleanup is performed when the parent DataListView is disposed
    ' as well as when the DataListViewEditControlManager is disposed
    Private _CleanedUp As Boolean = False


    Private Sub DataListView_Disposed(ByVal sender As Object, ByVal e As EventArgs)
        DoCleanUp()
    End Sub


    Private Sub DoCleanUp()

        If _CleanedUp Then Exit Sub

        Try
            RemoveHandler _CurrentListView.CellEditStarting, AddressOf DataListView_CellEditStarting
        Catch ex As Exception
        End Try
        Try
            RemoveHandler _CurrentListView.CellEditFinishing, AddressOf DataListView_CellEditFinishing
        Catch ex As Exception
        End Try
        If _IsValidationEnabled Then
            Try
                RemoveHandler _CurrentListView.FormatCell, AddressOf DataListView_FormatCell
            Catch ex As Exception
            End Try
        End If
        Try
            RemoveHandler _CurrentListView.CellToolTipShowing, AddressOf DataListView_CellToolTipShowing
        Catch ex As Exception
        End Try
        Try
            RemoveHandler _CurrentListView.KeyDown, AddressOf DataListView_KeyDown
        Catch ex As Exception
        End Try
        If Not _ContextMenuStrip Is Nothing Then
            Try
                RemoveHandler _CurrentListView.CellRightClick, AddressOf DataListView_CellRightClick
            Catch ex As Exception
            End Try
        End If
        Try
            RemoveHandler _CurrentListView.CellClick, AddressOf DataListView_CellClick
        Catch ex As Exception
        End Try
        Try
            RemoveHandler _CurrentListView.Disposed, AddressOf DataListView_Disposed
        Catch ex As Exception
        End Try

        For Each c As Control In _ControlsDictionary.Values
            Try
                c.Dispose()
            Catch ex As Exception
            End Try
        Next

        _ControlsDictionary.Clear()

        _ControlsDictionary = Nothing
        _CurrentListView = Nothing
        _ContextMenuStrip = Nothing
        _Handledbuttons = Nothing
        _ItemsDeleteHandler = Nothing
        _ItemAddHandler = Nothing
        _ItemActionIsAvailable = Nothing

        _CleanedUp = True

    End Sub


    ' To detect redundant calls
    Private disposedValue As Boolean = False

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: free other state (managed objects).
                DoCleanUp()
            End If

            ' TODO: free your own state (unmanaged objects).
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

#Region " IDisposable Support "
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

#End Region

End Class
