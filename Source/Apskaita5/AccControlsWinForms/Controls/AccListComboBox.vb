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
<System.ComponentModel.DefaultBindingProperty("SelectedValue")> _
<System.ComponentModel.LookupBindingProperties("DataSource", "DisplayMember", "ValueMember", "SelectedValue")> _
Public Class AccListComboBox
    Inherits ComboBox

    Private Const WM_LBUTTONDOWN As UInt32 = &H201
    Private Const WM_LBUTTONDBLCLK As UInt32 = &H203
    Private Const WM_KEYF4 As UInt32 = &H134

    Private myListView As DataListViewToolStrip = Nothing
    Private myDropDown As ToolStripDropDown = Nothing
    Private _SelectedValue As Object = Nothing
    Private _InstantBinding As Boolean = True
    Private _EmptyValueString As String = ""


#Region "Disabled properties"

    <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
    Public Shadows ReadOnly Property Items() As ComboBox.ObjectCollection
        Get
            Return MyBase.Items
        End Get
    End Property

    <Browsable(False), EditorBrowsable(EditorBrowsableState.Never)> _
    Public Shadows ReadOnly Property DataSource() As Object
        Get
            Return Nothing
        End Get
    End Property

#End Region

    ''' <summary>
    ''' Indicates whether an <see cref="InfoListControl">InfoListControl</see>
    ''' is already assigned to the combobox.
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly Property HasAttachedInfoList() As Boolean
        Get
            Return Not myListView Is Nothing
        End Get
    End Property

    ''' <summary>
    ''' Whether to dispose ToolStripDropDown when the combobox control is disposed.
    ''' </summary>
    ''' <remarks>Should be overriden if the ToolStripDropDown is added dynamicaly,
    ''' e.g. by a datagridview column.</remarks>
    Protected Overridable ReadOnly Property DisposeToolStripDropDown() As Boolean
        Get
            Return True
        End Get
    End Property

    ''' <summary>
    ''' Whether to update datasource instantly when the user selects a value
    ''' (as opposed on validation).
    ''' </summary>
    ''' <remarks></remarks>
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
    Public Overloads Property SelectedValue() As Object
        Get
            Return _SelectedValue
        End Get
        Set(ByVal value As Object)
            SetValue(value)
        End Set
    End Property

    ''' <summary>
    ''' A <see cref="SelectedValue">SelectedValue</see> string expression (ToString)
    ''' that should be displayed as an empty string.
    ''' </summary>
    ''' <remarks></remarks>
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
    ''' Gets or sets a currently applyed filter string.
    ''' </summary>
    ''' <remarks>A proxy property to the <see cref="InfoListControl.FilterString">
    ''' FilterString property of the nested InfoListControl</see>.</remarks>
    Public Property FilterString() As String
        Get
            If myListView Is Nothing Then Return ""
            Return myListView.FilterString
        End Get
        Set(ByVal value As String)
            If myListView Is Nothing Then Exit Property
            myListView.FilterString = value
        End Set
    End Property

    ''' <summary>
    ''' Gets a datasource of the nested InfoListControl.
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly Property InfoListControlDataSource() As Object
        Get
            If myListView Is Nothing Then
                Return Nothing
            Else
                Return myListView.GetDataSource()
            End If
        End Get
    End Property


    Public Sub New()

        InitializeComponent()

    End Sub


    ''' <summary>
    ''' Adds an InfoListControl to the combobox.
    ''' </summary>
    ''' <param name="dataView">an InfoListControl to add</param>
    ''' <remarks></remarks>
    Public Sub AddDataListView(ByVal dataView As InfoListControl)

        If Not myListView Is Nothing Then Throw New InvalidOperationException( _
            "Error. DataListView is already assigned to the AccListComboBox.")

        myListView = New DataListViewToolStrip(dataView)

        If myDropDown Is Nothing OrElse myDropDown.IsDisposed Then

            myDropDown = New ToolStripDropDown()
            myDropDown.AutoSize = False
            myDropDown.GripStyle = SizeGripStyle.Show
            AddHandler myDropDown.Closed, AddressOf ToolStripDropDown_Closed

        Else

            myDropDown.Items.Clear()

        End If

        myDropDown.Items.Add(myListView)
        myDropDown.Width = Math.Max(Me.Width, myListView.MinDropDownWidth)
        myDropDown.Height = myListView.Height
        'AddHandler myListView.OnFilterStringChanged, AddressOf OnFilterChanged

    End Sub

    ''' <summary>
    ''' Adds a dropdown if it is handled by some parent object.
    ''' </summary>
    ''' <param name="nDropDown">a ToolStripDropDown to add
    ''' (should contain a DataListViewToolStrip)</param>
    ''' <remarks>See also <seealso cref="DisposeToolStripDropDown">DisposeToolStripDropDown</seealso>.</remarks>
    Friend Sub AddDropDown(ByVal nDropDown As ToolStripDropDown)

        If nDropDown Is Nothing OrElse (Not myDropDown Is Nothing _
            AndAlso myDropDown Is nDropDown) Then Exit Sub

        If nDropDown.Items.Count <> 1 OrElse Not TypeOf nDropDown.Items(0) Is DataListViewToolStrip Then
            Throw New ArgumentException("ToolStripDropDown should contain a DataListViewToolStrip.", "nDropDown")
        End If

        If Not myDropDown Is Nothing Then

            Try
                RemoveHandler myDropDown.Closed, AddressOf ToolStripDropDown_Closed
            Catch ex As Exception
            End Try

            If Not myListView Is Nothing AndAlso Not myListView.IsDisposed Then _
                myListView.Dispose()
            If Not myDropDown.IsDisposed Then myDropDown.Dispose()

            myListView = Nothing
            myDropDown = Nothing

        End If

        myDropDown = nDropDown
        myListView = DirectCast(myDropDown.Items(0), DataListViewToolStrip)

        AddHandler myDropDown.Closed, AddressOf ToolStripDropDown_Closed

    End Sub


    Private Sub ShowDropDown()
        If Not myDropDown Is Nothing AndAlso Not Me.myListView Is Nothing Then

            If Not myDropDown.Items.Contains(Me.myListView) Then
                myDropDown.Items.Clear()
                myDropDown.Items.Add(Me.myListView)
            End If

            myDropDown.Width = Math.Max(Me.Width, Me.myListView.MinDropDownWidth)
            myListView.Size = myDropDown.Size

            myListView.SetSelectedValue(_SelectedValue)

            myDropDown.Show(Me, CalculatePoz()) 'New Point(0, Me.Height)

            SendKeys.Send("{down}")

        End If

    End Sub

    Private Function CalculatePoz() As Point

        Dim point As New Point(0, Me.Height)

        If (Me.PointToScreen(New Point(0, 0)).Y + Me.Height + Me.myListView.Height) _
            > Screen.PrimaryScreen.WorkingArea.Height Then
            point.Y = -Me.myListView.Height - 7
        End If

        Return point

    End Function

    Private Sub ToolStripDropDown_Closed(ByVal sender As Object, _
        ByVal e As ToolStripDropDownClosedEventArgs)

        If e.CloseReason = ToolStripDropDownCloseReason.ItemClicked _
            AndAlso Not myListView Is Nothing AndAlso Not myListView.SelectionCanceled Then

            If Not MyBase.Focused Then MyBase.Focus()

            SetValue(myListView.SelectedValue)

            If _InstantBinding Then
                For Each b As Binding In MyBase.DataBindings
                    b.WriteValue()
                Next
            End If

        End If

        If Not myListView Is Nothing Then myListView.ClearFilter()

    End Sub

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

        MyBase.OnSelectedValueChanged(New EventArgs)

    End Sub

    'Private Sub OnFilterChanged(ByVal sender As Object, ByVal e As EventArgs)
    '    Me.Text = myListView.FilterString
    'End Sub


    Protected Overrides Sub WndProc(ByRef m As Message)

        '#Region "WM_KEYF4"
        If m.Msg = WM_KEYF4 Then
            Me.Focus()
            Me.myDropDown.Refresh()
            If Not Me.myDropDown.Visible Then

                ShowDropDown()

            Else
                myDropDown.Close()

            End If
            Return
        End If
        '#End Region

        '#Region "WM_LBUTTONDBLCLK"
        If m.Msg = WM_LBUTTONDBLCLK OrElse m.Msg = WM_LBUTTONDOWN Then
            If Not Me.myDropDown.Visible Then

                ShowDropDown()

            Else
                myDropDown.Close()

            End If
            Return
        End If
        '#End Region

        MyBase.WndProc(m)

    End Sub

    Protected Overrides Sub OnResize(ByVal e As System.EventArgs)
        Dim minWidth As Integer = 0
        If Not Me.myListView Is Nothing Then minWidth = Me.myListView.MinDropDownWidth
        If Not myDropDown Is Nothing Then
            myDropDown.Width = Math.Max(Me.Width, minWidth)
        End If
        If Not myListView Is Nothing Then
            myListView.Width = Math.Max(Me.Width, minWidth)
            myListView.Width = Math.Max(Me.Width, minWidth)
        End If
        MyBase.OnResize(e)
    End Sub

    Protected Overrides Sub OnKeyDown(ByVal e As System.Windows.Forms.KeyEventArgs)

        If e.Control AndAlso (e.KeyCode = Keys.Insert OrElse e.KeyCode = Keys.Add) Then
            If Not myListView Is Nothing AndAlso Not myListView.Control Is Nothing Then
                DirectCast(myListView.Control, InfoListControl).AddNewItem()
                e.Handled = True
            End If
        ElseIf Not myDropDown.Visible AndAlso e.KeyCode <> Keys.Enter AndAlso e.KeyCode <> Keys.Tab _
            AndAlso e.KeyCode <> Keys.Left AndAlso e.KeyCode <> Keys.Right AndAlso e.KeyCode <> Keys.ControlKey _
            AndAlso e.KeyCode <> Keys.Control AndAlso e.KeyCode <> Keys.LControlKey _
            AndAlso e.KeyCode <> Keys.RControlKey AndAlso e.KeyCode <> Keys.ShiftKey Then
            ShowDropDown()
            Dim unicodeString As String = KeyCodeToUnicode(e.KeyCode)
            If unicodeString.Length = 1 AndAlso Char.IsLetterOrDigit(Convert.ToChar(unicodeString)) Then
                myListView.AppendFilter(Convert.ToChar(unicodeString))
            End If
            e.Handled = True
        End If

        MyBase.OnKeyDown(e)

    End Sub


    ''' <summary>
    ''' Required designer variable.
    ''' </summary>
    Private components As System.ComponentModel.IContainer = Nothing

    ''' <summary>
    ''' Clean up any resources being used.
    ''' </summary>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If components IsNot Nothing Then components.Dispose()
            If Me.DisposeToolStripDropDown Then
                If Not myListView Is Nothing AndAlso Not myListView.IsDisposed Then
                    'Try
                    '    RemoveHandler myListView.OnFilterStringChanged, AddressOf OnFilterChanged
                    'Catch ex As Exception
                    'End Try
                    myListView.Dispose()
                End If
                If Not myDropDown Is Nothing AndAlso Not myDropDown.IsDisposed Then _
                    myDropDown.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

#Region "Component Designer generated code"

    ''' <summary>
    ''' Required method for Designer support - do not modify 
    ''' the contents of this method with the code editor.
    ''' </summary>
    Private Sub InitializeComponent()
        Me.SuspendLayout()
        Me.ResumeLayout(False)
    End Sub

#End Region

End Class
