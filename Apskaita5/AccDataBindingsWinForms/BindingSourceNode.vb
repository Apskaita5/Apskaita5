Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Reflection

''' <summary>
''' Maintains a reference to a BindingSource object on the form.
''' </summary>
Public Class BindingSourceNode

    Private _Source As BindingSource
    Private _Children As List(Of BindingSourceNode)
    Private _Parent As BindingSourceNode


    Friend ReadOnly Property Source() As BindingSource
        Get
            Return _Source
        End Get
    End Property

    Friend ReadOnly Property Children() As List(Of BindingSourceNode)
        Get
            If _Children Is Nothing Then
                _Children = New List(Of BindingSourceNode)()
            End If

            Return _Children
        End Get
    End Property

    Friend Property Parent() As BindingSourceNode
        Get
            Return _Parent
        End Get
        Set(ByVal value As BindingSourceNode)
            _Parent = value
        End Set
    End Property


    ''' <summary>
    ''' Creates an instance of the object.
    ''' </summary>
    ''' <param name="source">
    ''' BindingSource object to be mananaged.
    ''' </param>
    Private Sub New(ByVal source As BindingSource)
        _Source = source
        AddHandler _Source.CurrentChanged, AddressOf BindingSource_CurrentChanged
    End Sub

    Private Sub New()
        ' do not allow creation of an instance
    End Sub


    ''' <summary>
    ''' Sets up BindingSourceNode objects for all
    ''' BindingSource objects related to the provided
    ''' root source.
    ''' </summary>
    ''' <param name="parentForm">
    ''' Container for the components.
    ''' </param>
    ''' <param name="rootSource">
    ''' Root BindingSource object.
    ''' </param>
    ''' <returns></returns>
    Public Shared Function GetBindingSourceTree(ByVal parentForm As Form, _
        ByVal rootSource As BindingSource) As BindingSourceNode

        If rootSource Is Nothing Then
            Throw New ArgumentNullException("rootSource")
        End If

        Dim result As BindingSourceNode = New BindingSourceNode(rootSource)
        Dim bindingSourceList As List(Of BindingSource) = GetBindingSourceList(parentForm)
        result.Children.AddRange(GetChildBindingSources(bindingSourceList, rootSource, result))

        Return result

    End Function

    Private Shared Function GetChildBindingSources(ByVal bindingSourceList As List(Of BindingSource), _
        ByVal parent As BindingSource, ByVal parentNode As BindingSourceNode) As List(Of BindingSourceNode)

        Dim children As New List(Of BindingSourceNode)()

        If Not bindingSourceList Is Nothing Then

            For Each component As BindingSource In bindingSourceList

                If component.DataSource IsNot Nothing AndAlso component.DataSource.Equals(parent) Then
                    Dim childNode As New BindingSourceNode(component)
                    children.Add(childNode)
                    childNode.Children.AddRange(GetChildBindingSources(bindingSourceList, component, childNode))
                    childNode.Parent = parentNode
                End If

            Next

        End If

        Return children

    End Function


    Friend Sub Unbind(ByVal cancel As Boolean)

        If _Source Is Nothing Then Exit Sub

        If _Children.Count > 0 Then
            For Each child As BindingSourceNode In _Children
                child.Unbind(cancel)
            Next
        End If

        Dim current As IEditableObject = TryCast(_Source.Current, IEditableObject)

        If current IsNot Nothing Then
            If cancel Then
                current.CancelEdit()
            Else
                current.EndEdit()
            End If
        End If

        If TypeOf _Source.DataSource Is BindingSource Then
            _Source.DataSource = _Parent.Source
        End If

    End Sub

    Friend Sub EndEdit()

        If Source Is Nothing Then Exit Sub

        If _Children.Count > 0 Then
            For Each child As BindingSourceNode In _Children
                child.EndEdit()
            Next
        End If

        _Source.EndEdit()

    End Sub

    Friend Sub SetEvents(ByVal value As Boolean)

        If _Source Is Nothing Then Exit Sub
        
        _Source.RaiseListChangedEvents = value

        If _Children.Count > 0 Then
            For Each child As BindingSourceNode In _Children
                child.SetEvents(value)
            Next
        End If

    End Sub

    Friend Sub ResetBindings(ByVal refreshMetadata As Boolean)

        If _Source Is Nothing Then Exit Sub

        If _Children.Count > 0 Then
            For Each child As BindingSourceNode In _Children
                child.ResetBindings(refreshMetadata)
            Next
        End If

        _Source.ResetBindings(refreshMetadata)

    End Sub

    ''' <summary>
    ''' Binds a business object to the BindingSource.
    ''' </summary>
    ''' <param name="objectToBind">
    ''' Business object.
    ''' </param>
    Public Sub Bind(ByVal objectToBind As Object)

        Try
            objectToBind.BeginEdit()
        Catch ex As Exception
        End Try

        If objectToBind Is Nothing OrElse _Source.DataSource Is Nothing _
            OrElse Not Object.ReferenceEquals(_Source.DataSource, objectToBind) Then
            _Source.DataSource = Nothing
            _Source.DataSource = objectToBind
        End If

        SetEvents(True)
        ResetBindings(False)

    End Sub

    ''' <summary>
    ''' Applies changes to the business object.
    ''' </summary>
    Public Sub Apply()

        SetEvents(False)
        Unbind(False)
        EndEdit()

        Try
            _Source.DataSource.ApplyEdit()
        Catch ex As Exception
        End Try

    End Sub

    ''' <summary>
    ''' Cancels changes to the business object.
    ''' </summary>
    ''' <param name="businessObject"></param>
    Public Sub Cancel(ByVal businessObject As Object)

        SetEvents(False)
        Unbind(True)

        Try
            _Source.DataSource.CancelEdit()
        Catch ex As Exception
        End Try

        Bind(businessObject)

    End Sub

    ''' <summary>
    ''' Cancels changes to the child business object, i.e. without rebinding.
    ''' </summary>
    Public Sub CancelChild()

        SetEvents(False)
        Unbind(True)

        Try
            _Source.DataSource.CancelEdit()
        Catch ex As Exception
        End Try

    End Sub

    ''' <summary>
    ''' Disconnects from the BindingSource object.
    ''' </summary>
    Public Sub Close()
        SetEvents(False)
        Unbind(True)
    End Sub


    Private Sub BindingSource_CurrentChanged(ByVal sender As Object, ByVal e As EventArgs)
        If _Children.Count > 0 Then
            For Each child As BindingSourceNode In _Children
                child.Source.EndEdit()
            Next
        End If
    End Sub

    Private Shared Function GetBindingSourceList(ByVal parentForm As Form) As List(Of BindingSource)
        Dim result As New List(Of BindingSource)
        GetBindingSources(parentForm, result)
        Return result
    End Function

    Private Shared Sub GetBindingSources(ByVal cntr As Control, ByVal result As List(Of BindingSource))

        Try
            Dim componentsField As FieldInfo = cntr.GetType.GetField("components", _
                Reflection.BindingFlags.Instance Or Reflection.BindingFlags.NonPublic)
            If Not componentsField Is Nothing Then
                Dim components As System.ComponentModel.Container = componentsField.GetValue(cntr)
                If Not components Is Nothing AndAlso Not components.Components Is Nothing Then
                    For Each o As Object In components.Components
                        If TypeOf o Is BindingSource Then
                            result.Add(DirectCast(o, BindingSource))
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
        End Try

        For Each c As Control In cntr.Controls
            GetBindingSources(c, result)
        Next

    End Sub

End Class