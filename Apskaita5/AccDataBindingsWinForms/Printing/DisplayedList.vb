Namespace Printing

    ''' <summary>
    ''' Represents a helper object to iterate items in a collection by their
    ''' display indexes (or by the original indexes if the display indexes are not provided)
    ''' </summary>
    ''' <typeparam name="T">a type of the items in the collection</typeparam>
    ''' <remarks></remarks>
    Public Class DisplayedList(Of T)
        Implements IEnumerable(Of T)

        Private _BaseList As IList(Of T) = Nothing
        Private _DisplayOrderIndexes As List(Of Integer) = Nothing


        Private Sub New()

        End Sub

        ''' <summary>
        ''' Creates a new DisplayedList instance for a base collection
        ''' using the display indexes provided.
        ''' </summary>
        ''' <param name="baseList">a base collection</param>
        ''' <param name="displayOrderIndexes">a display indexes provided by the
        ''' <see cref="DataListViewEditControlManager(Of T).GetDisplayOrderIndexes">DataListViewEditControlManager.GetDisplayOrderIndexes</see>
        ''' method (if any)</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal baseList As IList(Of T), ByVal displayOrderIndexes As List(Of Integer))

            If baseList Is Nothing Then
                Throw New ArgumentNullException("baseList")
            End If

            _BaseList = baseList
            _DisplayOrderIndexes = displayOrderIndexes

        End Sub

        ''' <summary>
        ''' Creates a new DisplayedList instance for a base collection
        ''' using the display indexes provided.
        ''' </summary>
        ''' <param name="baseList">a base collection</param>
        ''' <param name="displayOrderIndexes">a display indexes provided by the
        ''' <see cref="DataListViewEditControlManager(Of T).GetDisplayOrderIndexes">DataListViewEditControlManager.GetDisplayOrderIndexes</see>
        ''' method (if any)</param>
        ''' <param name="listIndex">an index of the reguired display index list within the array</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal baseList As IList(Of T), ByVal displayOrderIndexes As List(Of Integer)(), _
            ByVal listIndex As Integer)

            If baseList Is Nothing Then
                Throw New ArgumentNullException("baseList")
            End If

            _BaseList = baseList
            If displayOrderIndexes Is Nothing OrElse displayOrderIndexes.Length < listIndex + 1 Then
                _DisplayOrderIndexes = Nothing
            Else
                _DisplayOrderIndexes = displayOrderIndexes(listIndex)
            End If

        End Sub


        Public Function GetEnumerator() As IEnumerator(Of T) _
            Implements IEnumerable(Of T).GetEnumerator
            Return New DisplayedListEnumerator(Of T)(_BaseList, _DisplayOrderIndexes)
        End Function

        Public Function GetEnumerator1() As IEnumerator _
            Implements IEnumerable.GetEnumerator
            Return GetEnumerator()
        End Function


        Private Class DisplayedListEnumerator(Of TC)
            Implements IEnumerator(Of TC)

            Private _BaseList As IList(Of TC) = Nothing
            Private _DisplayOrderIndexes As List(Of Integer) = Nothing
            Private _Position As Integer = -1


            Private Sub New()

            End Sub

            Friend Sub New(ByVal baseList As IList(Of TC), ByVal displayOrderIndexes As List(Of Integer))

                If baseList Is Nothing Then
                    Throw New ArgumentNullException("baseList")
                End If

                _BaseList = baseList
                _DisplayOrderIndexes = displayOrderIndexes

            End Sub


            Public ReadOnly Property Current() As TC _
                Implements IEnumerator(Of TC).Current
                Get

                    If _DisplayOrderIndexes Is Nothing Then

                        Try
                            Return _BaseList(_Position)
                        Catch ex As IndexOutOfRangeException
                            Throw New InvalidOperationException()
                        End Try

                    Else

                        Try
                            Return _BaseList(_DisplayOrderIndexes(_Position))
                        Catch ex As IndexOutOfRangeException
                            Throw New InvalidOperationException()
                        End Try

                    End If

                End Get
            End Property

            Public ReadOnly Property Current1() As Object _
                Implements System.Collections.IEnumerator.Current
                Get
                    Return Current
                End Get
            End Property

            Public Function MoveNext() As Boolean _
                Implements IEnumerator.MoveNext

                _Position = _Position + 1
                If _DisplayOrderIndexes Is Nothing Then
                    Return (_Position < _BaseList.Count)
                Else
                    Return (_Position < _DisplayOrderIndexes.Count)
                End If

            End Function

            Public Sub Reset() _
                Implements IEnumerator.Reset
                _Position = -1
            End Sub


            Private disposedValue As Boolean = False        ' To detect redundant calls

            ' IDisposable
            Protected Overridable Sub Dispose(ByVal disposing As Boolean)
                If Not Me.disposedValue Then
                    If disposing Then
                        ' TODO: free other state (managed objects).
                        _DisplayOrderIndexes = Nothing
                        _BaseList = Nothing
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

        End Class

    End Class

End Namespace