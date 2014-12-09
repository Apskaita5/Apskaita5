Imports System.ComponentModel
Imports System.Reflection

<ToolboxItem(True)> _
<ToolboxBitmap(GetType(ComboBox))> _
<System.ComponentModel.DefaultBindingProperty("SelectedValue")> _
<System.ComponentModel.LookupBindingProperties("DataSource", "DisplayMember", "ValueMember", "SelectedValue")> _
Partial Public Class AccGridComboBox
    Inherits ComboBox

    Private Const WM_LBUTTONDOWN As UInt32 = &H201
    Private Const WM_LBUTTONDBLCLK As UInt32 = &H203
    Private Const WM_KEYF4 As UInt32 = &H134

    Private myDataGridView As ToolStripDataGridView = Nothing
    Private myDropDown As ToolStripDropDown
    Private _SelectedValue As Object = Nothing
    Private _CloseOnSingleClick As Boolean = True
    Private _InstantBinding As Boolean = True
    Private _FilterPropertyName As String = ""
    Private _InternalTextChange As Boolean = False

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


    Public Sub New()

        InitializeComponent()

        myDropDown = New ToolStripDropDown()
        myDropDown.AutoSize = False
        AddHandler myDropDown.Closed, AddressOf ToolStripDropDown_Closed

    End Sub


    Public ReadOnly Property HasAttachedGrid() As Boolean
        Get
            Return Not myDataGridView Is Nothing
        End Get
    End Property

    Public ReadOnly Property AttachedGrid() As DataGridView
        Get
            If Not myDataGridView Is Nothing Then Return myDataGridView.DataGridViewControl
            Return Nothing
        End Get
    End Property

    Protected Overridable ReadOnly Property DisposeToolStripDataGridView() As Boolean
        Get
            Return True
        End Get
    End Property

    Public Property InstantBinding() As Boolean
        Get
            Return _InstantBinding
        End Get
        Set(ByVal value As Boolean)
            _InstantBinding = value
        End Set
    End Property

    Public Property FilterPropertyName() As String
        Get
            Return _FilterPropertyName
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then value = ""
            _FilterPropertyName = value
        End Set
    End Property

    Public Overloads Property SelectedValue() As Object
        Get
            Return _SelectedValue
        End Get
        Set(ByVal value As Object)
            SetValue(value, True)
            MyBase.OnSelectedValueChanged(New EventArgs)
        End Set
    End Property


    Public Sub AddDataGridView(ByVal nDataGridView As DataGridView, ByVal nCloseOnSingleClick As Boolean)
        If Not myDataGridView Is Nothing Then Throw New Exception( _
            "Error. DataGridView is already assigned to the AccGridComboBox.")
        myDataGridView = New ToolStripDataGridView(nDataGridView, Me, nCloseOnSingleClick)
        myDropDown.Width = Math.Max(Me.Width, myDataGridView.MinDropDownWidth)
        myDropDown.Height = nDataGridView.Height
        myDropDown.Items.Clear()
        myDropDown.Items.Add(Me.myDataGridView)
    End Sub

    Friend Sub AddToolStripDataGridView(ByVal nToolStripDataGridView As ToolStripDataGridView)
        If nToolStripDataGridView Is Nothing OrElse (Not myDataGridView Is Nothing _
            AndAlso myDataGridView Is nToolStripDataGridView) Then Exit Sub
        myDataGridView = nToolStripDataGridView
        myDataGridView.SetParent(Me)
        myDropDown.Width = Math.Max(Me.Width, myDataGridView.MinDropDownWidth)
        myDropDown.Height = myDataGridView.DropDownHeight
        myDropDown.Items.Clear()
        myDropDown.Items.Add(Me.myDataGridView)
    End Sub

    Private Sub ToolStripDropDown_Closed(ByVal sender As Object, ByVal e As ToolStripDropDownClosedEventArgs)
        If e.CloseReason = ToolStripDropDownCloseReason.ItemClicked Then
            If Not MyBase.Focused Then MyBase.Focus()
            If myDataGridView.DataGridViewControl.CurrentRow Is Nothing Then
                SetValue(Nothing, False)
            Else
                SetValue(myDataGridView.DataGridViewControl.CurrentRow.DataBoundItem, False)
            End If
            MyBase.OnSelectedValueChanged(New EventArgs)
            If _InstantBinding Then
                For Each b As Binding In MyBase.DataBindings
                    b.WriteValue()
                Next
            End If
        End If
    End Sub

    Private Sub SetValue(ByVal value As Object, ByVal IsValueMemberValue As Boolean)

        _InternalTextChange = True

        If value Is Nothing Then
            Me.Text = ""
            _SelectedValue = Nothing

        Else

            If Me.ValueMember Is Nothing OrElse String.IsNullOrEmpty(Me.ValueMember.Trim) _
                OrElse IsValueMemberValue Then

                Me.Text = value.ToString
                _SelectedValue = value

            Else

                Dim newValue As Object = GetValueMemberValue(value)

                If newValue Is Nothing Then
                    Me.Text = value.ToString
                    _SelectedValue = value
                Else
                    Me.Text = newValue.ToString
                    _SelectedValue = newValue
                End If

            End If

        End If

        _InternalTextChange = False

    End Sub

    Private Function CalculatePoz() As Point

        Dim point As New Point(0, Me.Height)

        If (Me.PointToScreen(New Point(0, 0)).Y + Me.Height + Me.myDataGridView.Height) _
            > Screen.PrimaryScreen.WorkingArea.Height Then
            point.Y = -Me.myDataGridView.Height - 7
        End If

        Return point

    End Function

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
        If Not Me.myDataGridView Is Nothing Then minWidth = Me.myDataGridView.MinDropDownWidth
        myDropDown.Width = Math.Max(Me.Width, minWidth)
        If Not myDataGridView Is Nothing Then
            myDataGridView.Width = Math.Max(Me.Width, minWidth)
            myDataGridView.DataGridViewControl.Width = Math.Max(Me.Width, minWidth)
            myDataGridView.DataGridViewControl.AutoResizeColumns()
        End If
        MyBase.OnResize(e)
    End Sub

    Protected Overrides Sub OnKeyDown(ByVal e As System.Windows.Forms.KeyEventArgs)
        If Not myDropDown.Visible AndAlso e.KeyCode <> Keys.Enter AndAlso e.KeyCode <> Keys.Tab _
            AndAlso e.KeyCode <> Keys.Left AndAlso e.KeyCode <> Keys.Right Then
            ShowDropDown()
            e.Handled = True
        End If
        MyBase.OnKeyDown(e)
    End Sub

    Private Sub ShowDropDown()
        If Not Me.myDataGridView Is Nothing Then

            If Not myDropDown.Items.Contains(Me.myDataGridView) Then
                myDropDown.Items.Clear()
                myDropDown.Items.Add(Me.myDataGridView)
            End If

            myDropDown.Width = Math.Max(Me.Width, Me.myDataGridView.MinDropDownWidth)
            myDataGridView.Size = myDropDown.Size
            myDataGridView.DataGridViewControl.Size = myDropDown.Size
            myDataGridView.DataGridViewControl.AutoResizeColumns()

            If _SelectedValue Is Nothing OrElse IsDBNull(_SelectedValue) Then
                myDataGridView.DataGridViewControl.CurrentCell = Nothing

            ElseIf Not Me.ValueMember Is Nothing AndAlso Not String.IsNullOrEmpty(Me.ValueMember.Trim) Then

                If myDataGridView.DataGridViewControl.Rows.Count < 1 OrElse _
                    myDataGridView.DataGridViewControl.Rows(0).DataBoundItem Is Nothing OrElse _
                    myDataGridView.DataGridViewControl.Rows(0).DataBoundItem.GetType. _
                    GetProperty(Me.ValueMember.Trim, BindingFlags.Public OrElse BindingFlags.Instance) Is Nothing Then

                    myDataGridView.DataGridViewControl.CurrentCell = Nothing

                Else

                    Dim CurrentValue As Object
                    For Each r As DataGridViewRow In myDataGridView.DataGridViewControl.Rows
                        If Not r.DataBoundItem Is Nothing Then
                            CurrentValue = GetValueMemberValue(r.DataBoundItem)
                            If _SelectedValue = CurrentValue Then
                                myDataGridView.DataGridViewControl.CurrentCell = _
                                    myDataGridView.DataGridViewControl.Item(0, r.Index)
                                Exit For
                            End If
                        End If
                    Next

                End If

            Else

                Dim SelectionFound As Boolean = False
                For Each r As DataGridViewRow In myDataGridView.DataGridViewControl.Rows
                    Try
                        If _SelectedValue = r.DataBoundItem Then
                            myDataGridView.DataGridViewControl.CurrentCell = _
                                myDataGridView.DataGridViewControl.Item(0, r.Index)
                            SelectionFound = True
                            Exit For
                        End If
                    Catch ex As Exception
                        Try
                            If _SelectedValue Is r.DataBoundItem Then
                                myDataGridView.DataGridViewControl.CurrentCell = _
                                    myDataGridView.DataGridViewControl.Item(0, r.Index)
                                SelectionFound = True
                                Exit For
                            End If
                        Catch e As Exception
                        End Try
                    End Try
                Next
                If Not SelectionFound Then myDataGridView.DataGridViewControl.CurrentCell = Nothing

            End If

            myDropDown.Show(Me, CalculatePoz) 'New Point(0, Me.Height)

            SendKeys.Send("{down}")

        End If

    End Sub

    Private Function GetValueMemberValue(ByVal DataboundItem As Object) As Object
        Dim newValue As Object = Nothing
        Try
            newValue = DataboundItem.GetType.GetProperty(Me.ValueMember.Trim, BindingFlags.Public _
                OrElse BindingFlags.Instance).GetValue(DataboundItem, Nothing)
        Catch ex As Exception
        End Try
        Return newValue
    End Function

End Class