Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Drawing

''' <summary>
''' A combobox style control that displays a <see cref="BrightIdeasSoftware.DataListView">DataListView</see>
''' in it's dropdown.
''' </summary>
''' <remarks>Inherit from <see cref="InfoListControl">InfoListControl</see>
''' in order to create (design) a <see cref="BrightIdeasSoftware.DataListView">DataListView</see>
''' for a specific value object list.
''' Invoke <see cref="AccListComboBox.AddDataListView">AddDataListView</see> 
''' method to initialize a control with the designed <see cref="InfoListControl">InfoListControl</see>
''' descendant.</remarks>
<ToolboxItem(True)> _
<ToolboxBitmap(GetType(ComboBox))> _
<DefaultBindingProperty("SelectedValue")> _
Public Class AccListComboBox
    Inherits AccComboBoxBase

    Private myListView As DataListViewToolStrip = Nothing
    Private _SelectedValue As Object = Nothing
    Private _InstantBinding As Boolean = True
    Private _EmptyValueString As String = ""

    Public Event OnSelectedValueChanged As EventHandler


    ''' <summary>
    ''' Indicates whether an <see cref="InfoListControl">InfoListControl</see>
    ''' is already assigned to the combobox.
    ''' </summary>
    ''' <remarks></remarks>
    <Browsable(False)> _
    <EditorBrowsable(EditorBrowsableState.Advanced)> _
    <Category("Behaviour")> _
    <Description("Gets whether an InfoListControl is already assigned to the combobox.")> _
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Public ReadOnly Property HasAttachedInfoList() As Boolean
        Get
            Return Not myListView Is Nothing
        End Get
    End Property

    ''' <summary>
    ''' Gets or sets whether to update datasource instantly when the user selects a value
    ''' (as opposed on validation).
    ''' </summary>
    ''' <remarks></remarks>
    <Browsable(True)> _
    <EditorBrowsable(EditorBrowsableState.Always)> _
    <Category("Behaviour")> _
    <Description("Gets or sets whether to update datasource instantly when the user selects a value. (as opposed on validation)"), _
    DefaultValue(True)> _
    Public Property InstantBinding() As Boolean
        Get
            Return _InstantBinding
        End Get
        Set(ByVal value As Boolean)
            _InstantBinding = value
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a selected value object or a selected value of the value object
    ''' <see cref="InfoListControl.ValueMember">ValueMember</see> property (if set).
    ''' </summary>
    ''' <remarks></remarks>
    <Browsable(False)> _
    <EditorBrowsable(EditorBrowsableState.Always)> _
    <Category("Behaviour")> _
    <Description("Gets or sets a selected value object or a selected value of the value object ValueMember property (if set).")> _
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Public Overloads Property SelectedValue() As Object
        Get
            Return _SelectedValue
        End Get
        Set(ByVal value As Object)
            SetValue(value)
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a <see cref="SelectedValue">SelectedValue</see> string expression (ToString)
    ''' that should be displayed as an empty string.
    ''' </summary>
    ''' <remarks></remarks>
    <Browsable(True)> _
    <EditorBrowsable(EditorBrowsableState.Always)> _
    <Category("Appearance")> _
    <Description("Gets or sets a selected value string expression (ToString) that should be displayed as an empty string.")> _
    Public Property EmptyValueString() As String
        Get
            Return _EmptyValueString
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then value = ""
            _EmptyValueString = value
        End Set
    End Property

    ''' <summary>
    ''' Gets a datasource of the nested InfoListControl.
    ''' </summary>
    ''' <remarks></remarks>
    <Browsable(False)> _
    <EditorBrowsable(EditorBrowsableState.Advanced)> _
    <Category("Behaviour")> _
    <Description("Gets a datasource of the nested InfoListControl.")> _
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Public ReadOnly Property InfoListControlDataSource() As Object
        Get
            If myListView Is Nothing Then
                Return Nothing
            Else
                Return myListView.GetDataSource()
            End If
        End Get
    End Property


    ''' <summary>
    ''' Adds an InfoListControl to the combobox.
    ''' </summary>
    ''' <param name="dataView">an InfoListControl to add</param>
    ''' <remarks></remarks>
    Public Sub AddDataListView(ByVal dataView As InfoListControl)

        If Not myListView Is Nothing Then Throw New InvalidOperationException( _
            "Error. DataListView is already assigned to the AccListComboBox.")

        myListView = New DataListViewToolStrip(dataView)

        AddHandler myListView.OnFilterStringChanged, AddressOf OnFilterChanged

    End Sub


    Protected Overrides Function GetMaxTextLength() As Integer
        Return 1000
    End Function

    Protected Overrides Function GetToolStripControlHost() As ToolStripControlHost
        Return myListView
    End Function

    Protected Overrides Sub BeforeDropDownOpen()

        Me.DropDownSize = New Size(Math.Max(Me.Width, Me.myListView.MinDropDownWidth), myListView.Height)
        myListView.Size = Me.DropDownSize

        myListView.SetSelectedValue(_SelectedValue)

    End Sub

    Protected Overrides Sub AfterDropDownOpen()
        SendKeys.Send("{down}")
    End Sub

    Protected Overrides Sub AfterDropDownClosed(ByVal reason As ToolStripDropDownCloseReason)

        If reason = ToolStripDropDownCloseReason.ItemClicked _
            AndAlso Not myListView Is Nothing AndAlso Not myListView.SelectionCanceled Then

            If Not MyBase.Focused Then MyBase.Focus()

            SetValue(myListView.SelectedValue)

            If _InstantBinding Then
                For Each b As Binding In MyBase.DataBindings
                    b.WriteValue()
                Next
            End If

        End If

        If Not myListView Is Nothing Then myListView.ResetFilter()

    End Sub

    Protected Overrides Sub OnClear()
        If _SelectedValue Is Nothing Then Exit Sub
        If _SelectedValue.GetType.IsValueType Then
            SetValue(Activator.CreateInstance(_SelectedValue.GetType))
        Else
            SetValue(Nothing)
        End If
    End Sub

    Protected Overrides Function AcceptClear() As Boolean
        Return True
    End Function


    Private Sub SetValue(ByVal value As Object)

        If value Is Nothing Then
            Me.Text = ""
        ElseIf Not _EmptyValueString Is Nothing AndAlso _
            Not String.IsNullOrEmpty(_EmptyValueString.Trim) AndAlso _
            value.ToString.ToLower.Trim = _EmptyValueString.Trim.ToLower Then
            Me.Text = ""
        Else
            Me.Text = value.ToString
        End If

        _SelectedValue = value

        RaiseEvent OnSelectedValueChanged(Me, EventArgs.Empty)

    End Sub

    Private Sub OnFilterChanged(ByVal sender As Object, ByVal e As EventArgs)
        Me.Text = myListView.FilterString
    End Sub


    Protected Overrides Sub OnResize(ByVal e As EventArgs)
        MyBase.OnResize(e)

        Dim newWidth As Integer = 0
        If Me.myListView Is Nothing Then
            newWidth = Me.Width
        Else
            newWidth = Math.Max(Me.Width, Me.myListView.MinDropDownWidth)
        End If

        If Me.DropDownSize <> Size.Empty Then
            Me.DropDownSize = New Size(newWidth, Me.DropDownSize.Height)
        End If

        If Not myListView Is Nothing Then
            myListView.Width = newWidth
        End If

    End Sub

    Protected Overrides Sub OnKeyDown(ByVal e As System.Windows.Forms.KeyEventArgs)

        If e.Control AndAlso (e.KeyCode = Keys.Insert OrElse e.KeyCode = Keys.Add) Then
            If Not myListView Is Nothing AndAlso Not myListView.Control Is Nothing Then
                DirectCast(myListView.Control, InfoListControl).AddNewItem()
                e.Handled = True
            End If
        ElseIf Not Me.DropDownVisible AndAlso e.KeyCode <> Keys.Enter AndAlso e.KeyCode <> Keys.Tab _
            AndAlso e.KeyCode <> Keys.Left AndAlso e.KeyCode <> Keys.Right AndAlso e.KeyCode <> Keys.ControlKey _
            AndAlso e.KeyCode <> Keys.Control AndAlso e.KeyCode <> Keys.LControlKey _
            AndAlso e.KeyCode <> Keys.RControlKey AndAlso e.KeyCode <> Keys.ShiftKey AndAlso e.KeyCode <> Keys.Down Then
            ShowDropDown()
            Dim unicodeString As String = KeyCodeToUnicode(e.KeyCode)
            If unicodeString.Length = 1 AndAlso Char.IsLetterOrDigit(Convert.ToChar(unicodeString)) Then
                myListView.AppendFilter(Convert.ToChar(unicodeString))
            End If
            e.Handled = True
        ElseIf Not Me.DropDownVisible AndAlso e.KeyCode = Keys.Enter Then
            e.Handled = True
        End If

        MyBase.OnKeyDown(e)

    End Sub

End Class
