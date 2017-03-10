Namespace General

    ''' <summary>
    ''' Represents a collection of child items of a hierarchical consolidated 
    ''' financial statement report item (balance sheet or income statement item).
    ''' </summary>
    ''' <remarks>Should only be used as a child of a <see cref="ConsolidatedReportItem">
    ''' ConsolidatedReportItem</see> or a <see cref="ConsolidatedReport">ConsolidatedReport</see>.</remarks>
    <Serializable()> _
    Public NotInheritable Class ConsolidatedReportItemList
        Inherits BusinessListBase(Of ConsolidatedReportItemList, ConsolidatedReportItem)

#Region " Business Methods "

#Region " Old interface "

        Friend Function MoveUp(ByVal childToMove As ConsolidatedReportItem) As ConsolidatedReportItem

            Dim rowToSwap1 As Integer = Me.IndexOf(childToMove)
            Dim rowToSwap2 As Integer = rowToSwap1 - 1

            ' if there is no space in the list move up, then return.
            If rowToSwap1 < 0 OrElse rowToSwap2 < 0 OrElse rowToSwap1 >= Me.Count _
                OrElse rowToSwap2 >= Me.Count Then

                Return Nothing

            End If

            Me.RaiseListChangedEvents = False

            Dim swapItem As ConsolidatedReportItem = childToMove.Clone

            Me.Remove(childToMove)
            Me.DeletedList.Remove(childToMove)
            Me.Insert(rowToSwap2, swapItem)
            Me.RaiseListChangedEvents = True
            Me.ResetBindings()

            Return swapItem

        End Function

        Friend Function MoveDown(ByVal childToMove As ConsolidatedReportItem) As ConsolidatedReportItem

            Dim rowToSwap1 As Integer = Me.IndexOf(childToMove)
            Dim rowToSwap2 As Integer = rowToSwap1 + 1

            ' if there is no space in the list move down, then return.
            If rowToSwap1 < 0 OrElse rowToSwap2 < 0 OrElse rowToSwap1 >= Me.Count _
                OrElse rowToSwap2 >= Me.Count Then

                Return Nothing

            End If

            Dim swapItem As ConsolidatedReportItem = childToMove.Clone

            Me.RemoveAt(rowToSwap1)
            Me.Insert(rowToSwap2, swapItem)
            Me.DeletedList.Remove(childToMove)

            Me.RaiseListChangedEvents = True
            Me.ResetBindings()

            Return swapItem

        End Function

#End Region


        Friend Sub MoveToIndex(ByVal child As ConsolidatedReportItem, ByVal indexToMove As Integer)

            If child Is Nothing Then
                Throw New ArgumentNullException("child")
            End If

            If Not Me.Contains(child) Then
                Throw New ArgumentException("Child is not a child.")
            End If

            If indexToMove < 0 OrElse indexToMove + 1 > Me.Count Then
                Throw New IndexOutOfRangeException()
            End If

            Dim indexToInsert As Integer = indexToMove
            If indexToInsert > Me.IndexOf(child) Then indexToInsert -= 1

            Me.Remove(child)
            Me.DeletedList.Remove(child)
            Me.Insert(indexToInsert, child)

        End Sub

        Friend Sub MoveToNewList(ByVal child As ConsolidatedReportItem, ByVal targetList As ConsolidatedReportItemList, _
            ByVal insertIndex As Integer)
            Me.Remove(child)
            Me.DeletedList.Remove(child)
            If insertIndex < 0 Then
                targetList.Add(child)
            Else
                targetList.Insert(insertIndex, child)
            End If
        End Sub


        Public Function GetAllBrokenRules() As String
            Dim result As String = GetAllBrokenRulesForList(Me)
            Return result
        End Function

        Public Function GetAllWarnings() As String
            Dim result As String = GetAllWarningsForList(Me)
            Return result
        End Function

        Public Function HasWarnings() As Boolean
            For Each c As ConsolidatedReportItem In Me
                If c.HasWarnings() Then Return True
            Next
            Return False
        End Function

        ''' <summary>
        ''' databindings don't work with TreeView control in .NET 2.0
        ''' so we need to triger rules checking before saving
        ''' </summary>
        ''' <remarks></remarks>
        Friend Sub CheckRules()
            For Each c As ConsolidatedReportItem In Me
                c.CheckRules()
            Next
        End Sub

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Returns a new instance of ConsolidatedReportItemList for a new
        ''' <see cref="ConsolidatedReportItem">ConsolidatedReportItem</see>.
        ''' </summary>
        Friend Shared Function NewConsolidatedReportItemList() As ConsolidatedReportItemList
            Return New ConsolidatedReportItemList()
        End Function

        ''' <summary>
        ''' Gets a new instance of ConsolidatedReportItemList from xml data.
        ''' </summary>
        ''' <remarks></remarks>
        Friend Shared Function NewConsolidatedReportItemList(ByVal nodeList As Xml.XmlNodeList, _
            ByRef level As Integer) As ConsolidatedReportItemList
            Return New ConsolidatedReportItemList(nodeList, level)
        End Function

        ''' <summary>
        ''' Gets an existing instance of ConsolidatedReportItemList from a database.
        ''' </summary>
        ''' <remarks></remarks>
        Friend Shared Function GetConsolidatedReportItemList(ByVal myData As DataTable, _
            ByRef index As Integer, ByVal parentLevel As Integer) As ConsolidatedReportItemList
            Return New ConsolidatedReportItemList(myData, index, parentLevel)
        End Function


        Private Sub New()
            MarkAsChild()
            Me.AllowEdit = True
            Me.AllowNew = True
            Me.AllowRemove = True
        End Sub

        Private Sub New(ByVal nodeList As Xml.XmlNodeList, ByRef level As Integer)
            MarkAsChild()
            Me.AllowEdit = True
            Me.AllowNew = True
            Me.AllowRemove = True
            Create(nodeList, level)
        End Sub

        Private Sub New(ByVal myData As DataTable, ByRef index As Integer, _
            ByVal parentLevel As Integer)
            MarkAsChild()
            Me.AllowEdit = True
            Me.AllowNew = True
            Me.AllowRemove = True
            Fetch(myData, index, parentLevel)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Create(ByVal nodeList As Xml.XmlNodeList, ByRef level As Integer)

            Me.RaiseListChangedEvents = False

            For Each n As Xml.XmlNode In nodeList
                Me.Add(ConsolidatedReportItem.GetConsolidatedReportItem(n, level + 1))
            Next

            Me.RaiseListChangedEvents = True

        End Sub

        Private Sub Fetch(ByVal myData As DataTable, ByRef index As Integer, _
            ByVal parentLevel As Integer)

            If index < myData.Rows.Count - 1 Then

                Dim nextLevel As Integer

                Me.RaiseListChangedEvents = False

                For i As Integer = index + 1 To myData.Rows.Count - 1

                    nextLevel = CIntSafe(myData.Rows(i).Item(2), 0)

                    If nextLevel = parentLevel + 1 Then
                        Me.Add(ConsolidatedReportItem.GetConsolidatedReportItem(myData, i))
                    ElseIf nextLevel <= parentLevel Then
                        Exit For
                    End If

                Next

                Me.RaiseListChangedEvents = True

            End If

        End Sub

        Friend Sub Update()

            RaiseListChangedEvents = False

            For Each c As ConsolidatedReportItem In Me.DeletedList
                c.DeleteSelf()
            Next
            Me.DeletedList.Clear()

            For Each c As ConsolidatedReportItem In Me
                c.Update()
            Next

            RaiseListChangedEvents = True

        End Sub

        Friend Sub DeleteSelf()

            RaiseListChangedEvents = False

            For Each c As ConsolidatedReportItem In Me
                c.DeleteSelf()
            Next
            For Each c As ConsolidatedReportItem In Me.DeletedList
                c.DeleteSelf()
            Next

            Me.Clear()
            Me.DeletedList.Clear()

            RaiseListChangedEvents = True

        End Sub

        Friend Function HasNewChildren() As Boolean
            For Each c As ConsolidatedReportItem In Me
                If Not c.IsNew Then Return False
            Next
            For Each c As ConsolidatedReportItem In Me.DeletedList
                If Not c.IsNew Then Return False
            Next
            Return Me.Count > 0
        End Function

#End Region

    End Class

End Namespace
