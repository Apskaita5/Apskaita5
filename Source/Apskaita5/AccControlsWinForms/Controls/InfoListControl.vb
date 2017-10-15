Imports BrightIdeasSoftware
Imports System.Reflection
Imports System.Windows.Forms

''' <summary>
''' Base class for value object list controls.
''' </summary>
''' <remarks>A control for a specific value object list should inherit this control
''' and configure required columns.</remarks>
Public Class InfoListControl
    Inherits Windows.Forms.UserControl

    Private _AcceptSingleClick As Boolean = False
    Private _ValueMember As String = ""
    Private _FilterString As String = ""

    Public Delegate Sub ValueSelectedEventHandler(ByVal sender As Object, ByVal e As ValueChangedEventArgs)
    Public Event ValueSelected As ValueSelectedEventHandler


    ''' <summary>
    ''' Whether single click is sufficient to choose an item.
    ''' </summary>
    ''' <remarks></remarks>
    Public Property AcceptSingleClick() As Boolean
        Get
            Return _AcceptSingleClick
        End Get
        Set(ByVal value As Boolean)
            _AcceptSingleClick = value
        End Set
    End Property

    ''' <summary>
    ''' A value object property that holds the required value (if any).
    ''' </summary>
    ''' <remarks></remarks>
    Public Property ValueMember() As String
        Get
            Return _ValueMember
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then value = ""
            _ValueMember = value
        End Set
    End Property

    ''' <summary>
    ''' A <see cref="BindingSource">BindingSource</see> that wraps a value object list.
    ''' </summary>
    ''' <remarks></remarks>
    Public Property DataSource() As Object
        Get
            Return baseDataListView.DataSource
        End Get
        Set(ByVal value As Object)
            baseDataListView.DataSource = value
        End Set
    End Property

    ''' <summary>
    ''' A string that is used to filter the value object list.
    ''' </summary>
    ''' <remarks></remarks>
    Public Property FilterString() As String
        Get
            Return _FilterString
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then value = ""
            If value <> _FilterString Then
                _FilterString = value
                baseDataListView.AdditionalFilter = _
                    TextMatchFilter.Contains(baseDataListView, _FilterString)
            End If
        End Set
    End Property


    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        baseDataListView.DefaultRenderer = New HighlightTextRenderer( _
            TextMatchFilter.Contains(baseDataListView, New String() {""}))
        baseDataListView.SelectColumnsMenuStaysOpen = True

    End Sub


    ''' <summary>
    ''' Selects a value object in the encapsulated <see cref="DataListView">DataListView</see>.
    ''' </summary>
    ''' <param name="value">a value object or a value of the value object
    ''' <see cref="ValueMember">ValueMember</see> property (if set).</param>
    ''' <remarks></remarks>
    Friend Sub SetSelectedValue(ByVal value As Object)

        If value Is Nothing OrElse IsDBNull(value) Then Exit Sub

        If _ValueMember Is Nothing OrElse String.IsNullOrEmpty(_ValueMember.Trim) Then

            For i As Integer = 1 To baseDataListView.GetItemCount()

                Try
                    If value = baseDataListView.GetItem(i - 1).RowObject Then
                        baseDataListView.SelectObject(baseDataListView. _
                            GetItem(i - 1).RowObject, True)
                        baseDataListView.EnsureVisible(i - 1)
                        Exit For
                    End If
                Catch ex As Exception
                    Try
                        If value.Equals(baseDataListView.GetItem(i - 1).RowObject) Then
                            baseDataListView.SelectObject(baseDataListView. _
                            GetItem(i - 1).RowObject, True)
                            baseDataListView.EnsureVisible(i - 1)
                            Exit For
                        End If
                    Catch f As Exception
                        Try
                            If value Is baseDataListView.GetItem(i - 1).RowObject Then
                                baseDataListView.SelectObject(baseDataListView. _
                                GetItem(i - 1).RowObject, True)
                                baseDataListView.EnsureVisible(i - 1)
                                Exit For
                            End If
                        Catch e As Exception
                        End Try
                    End Try
                End Try

            Next

        Else

            If baseDataListView.GetItemCount() < 1 OrElse baseDataListView.GetItem(0). _
                RowObject.GetType().GetProperty(_ValueMember.Trim, BindingFlags.Public _
                OrElse BindingFlags.Instance) Is Nothing Then Exit Sub

            Dim currentValue As Object
            For i As Integer = 1 To baseDataListView.GetItemCount()

                currentValue = GetValueMemberValue(baseDataListView.GetItem(i - 1).RowObject)

                If value = currentValue Then
                    baseDataListView.SelectObject(baseDataListView. _
                        GetItem(i - 1).RowObject, True)
                    baseDataListView.EnsureVisible(i - 1)
                    Exit For
                End If

            Next

        End If

    End Sub


    Protected Sub OnValueSelected(ByVal e As ValueChangedEventArgs)
        RaiseEvent ValueSelected(Me, e)
    End Sub

    Protected Sub OnValueSelected(ByVal currentObject As Object, ByVal isCanceled As Boolean)

        If _ValueMember Is Nothing OrElse String.IsNullOrEmpty(_ValueMember) _
            OrElse currentObject Is Nothing Then

            RaiseEvent ValueSelected(Me, New ValueChangedEventArgs(currentObject, isCanceled))

        Else

            If baseDataListView.GetItemCount() < 1 OrElse baseDataListView.GetItem(0). _
                RowObject.GetType().GetProperty(_ValueMember.Trim, BindingFlags.Public _
                OrElse BindingFlags.Instance) Is Nothing Then

                RaiseEvent ValueSelected(Me, New ValueChangedEventArgs(Nothing, isCanceled))

            Else

                RaiseEvent ValueSelected(Me, New ValueChangedEventArgs( _
                    GetValueMemberValue(currentObject), isCanceled))

            End If

        End If

    End Sub

    Private Sub baseDataListView_CellClick(ByVal sender As Object, _
        ByVal e As CellClickEventArgs) Handles baseDataListView.CellClick

        If Not e.Model Is Nothing AndAlso (e.ClickCount = 2 OrElse _
            (e.ClickCount = 1 AndAlso _AcceptSingleClick)) Then

            OnValueSelected(e.Model, False)

        End If

    End Sub

    Private Sub baseDataListView_KeyDown(ByVal sender As Object, _
        ByVal e As System.Windows.Forms.KeyEventArgs) Handles baseDataListView.KeyDown

        If e.KeyData = Keys.Enter AndAlso Not baseDataListView.SelectedItem Is Nothing _
            AndAlso Not baseDataListView.SelectedItem.RowObject Is Nothing Then

            OnValueSelected(baseDataListView.SelectedItem.RowObject, False)
            e.Handled = True

        ElseIf e.KeyData = Keys.Back Then

            If _FilterString <> "" Then
                _FilterString = _FilterString.Substring(0, _FilterString.Length - 1)
                baseDataListView.AdditionalFilter = _
                    TextMatchFilter.Contains(baseDataListView, _FilterString)
            End If
            e.Handled = True

        ElseIf e.KeyData = Keys.Delete Then

            If _FilterString <> "" Then
                _FilterString = ""
                baseDataListView.AdditionalFilter = _
                    TextMatchFilter.Contains(baseDataListView, _FilterString)
            End If
            e.Handled = True

        ElseIf e.KeyData = Keys.Escape Then

            OnValueSelected(Nothing, True)
            e.Handled = True

        End If

    End Sub

    Private Sub baseDataListView_KeyPress(ByVal sender As Object, _
        ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles baseDataListView.KeyPress

        If Not Char.IsControl(e.KeyChar) AndAlso (Char.IsLetterOrDigit(e.KeyChar) _
            OrElse Char.IsPunctuation(e.KeyChar)) Then

            _FilterString = _FilterString & e.KeyChar
            baseDataListView.AdditionalFilter = _
                TextMatchFilter.Contains(baseDataListView, _FilterString)

        End If

    End Sub


    Private Function GetValueMemberValue(ByVal databoundItem As Object) As Object
        Dim result As Object = Nothing
        Try
            result = databoundItem.GetType.GetProperty(_ValueMember.Trim, _
                BindingFlags.Public OrElse BindingFlags.Instance).GetValue(databoundItem, Nothing)
        Catch ex As Exception
        End Try
        Return result
    End Function


    Public Class ValueChangedEventArgs
        Inherits EventArgs

        Private _SelectedValue As Object = Nothing
        Private _SelectionCanceled As Boolean = False

        Public ReadOnly Property SelectedValue() As Object
            Get
                Return _SelectedValue
            End Get
        End Property

        Public ReadOnly Property SelectionCanceled() As Boolean
            Get
                Return _SelectionCanceled
            End Get
        End Property


        Friend Sub New(ByVal newValue As Object, ByVal isCanceled As Boolean)
            _SelectedValue = newValue
            _SelectionCanceled = isCanceled
        End Sub

    End Class

End Class
