Namespace General

    <Serializable()> _
    Public Class ConsolidatedReportItemList
        Inherits BusinessListBase(Of ConsolidatedReportItemList, ConsolidatedReportItem)

#Region " Business Methods "

        ''' <summary>
        ''' Scans through tree structure of ConsolidatedReportItem and parses all broken rules to one string.
        ''' </summary>
        Friend Function GetAllBrokenRules() As String
            Dim result As String = ""

            Dim s As String
            For i As Integer = 1 To Me.Count
                If Not Me.Item(i - 1).IsValid Then

                    s = Me.Item(i - 1).BrokenRulesCollection.ToString
                    If Not String.IsNullOrEmpty(s.Trim) Then
                        If Not String.IsNullOrEmpty(result.Trim) Then
                            result = result & vbCrLf & s
                        Else
                            result = s
                        End If
                    End If

                    s = Me.Item(i - 1).Children.GetAllBrokenRules
                    If Not String.IsNullOrEmpty(s.Trim) Then
                        If Not String.IsNullOrEmpty(result.Trim) Then
                            result = result & vbCrLf & s
                        Else
                            result = s
                        End If
                    End If

                End If
            Next

            Return result
        End Function

        Friend Function MoveUp(ByVal ChildToMove As ConsolidatedReportItem) As ConsolidatedReportItem

            Dim rowToSwap1, rowToSwap2 As Integer
            rowToSwap1 = Me.IndexOf(ChildToMove)
            rowToSwap2 = rowToSwap1 - 1

            If rowToSwap1 < 0 OrElse rowToSwap2 < 0 OrElse rowToSwap1 >= Me.Count _
                OrElse rowToSwap2 >= Me.Count Then Return ChildToMove

            SwapPositions(rowToSwap1, rowToSwap2)

            Return Me.Item(rowToSwap2)

        End Function

        Friend Function MoveDown(ByVal ChildToMove As ConsolidatedReportItem) As ConsolidatedReportItem

            Dim rowToSwap1, rowToSwap2 As Integer
            rowToSwap1 = Me.IndexOf(ChildToMove)
            rowToSwap2 = rowToSwap1 + 1

            If rowToSwap1 < 0 OrElse rowToSwap2 < 0 OrElse rowToSwap1 >= Me.Count _
                OrElse rowToSwap2 >= Me.Count Then Return ChildToMove

            SwapPositions(rowToSwap1, rowToSwap2)

            Return Me.Item(rowToSwap2)

        End Function

        Private Sub SwapPositions(ByVal rowToSwap1 As Integer, ByVal rowToSwap2 As Integer)

            Dim swapItem1 As ConsolidatedReportItem
            Dim swapItem2 As ConsolidatedReportItem

            If rowToSwap1 < 0 OrElse rowToSwap1 >= Me.Count OrElse rowToSwap2 < 0 _
                OrElse rowToSwap2 >= Me.Count Then Throw New IndexOutOfRangeException( _
                "Valid range of collection is 0 to " & (Me.Count - 1).ToString() _
                & ". Indexes provided were " & rowToSwap1.ToString() & " and " & rowToSwap2.ToString() & ".")


            swapItem1 = Items(rowToSwap1).Clone
            swapItem2 = Items(rowToSwap2).Clone

            Me.RemoveAt(rowToSwap1)
            Me.Insert(rowToSwap1, swapItem2)
            Me.RemoveAt(rowToSwap2)
            Me.Insert(rowToSwap2, swapItem1)
            Me.DeletedList.Remove(swapItem1)
            Me.DeletedList.Remove(swapItem2)

        End Sub

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Returns a new instance of ConsolidatedReportItemList.
        ''' </summary>
        Friend Shared Function NewConsolidatedReportItemList() As ConsolidatedReportItemList
            Return New ConsolidatedReportItemList
        End Function

        
        Private Sub New()
            MarkAsChild()
        End Sub

#End Region

#Region " Data Access "

        Friend Sub DeleteSelf()

            For Each c As ConsolidatedReportItem In Me
                If Not c.IsNew Then c.DeleteSelf()
            Next
            For Each c As ConsolidatedReportItem In Me.DeletedList
                If Not c.IsNew Then c.DeleteSelf()
            Next

            Me.Clear()
            Me.DeletedList.Clear()

        End Sub

        Friend Sub Delete()

            For Each c As ConsolidatedReportItem In Me.DeletedList
                If Not c.IsNew Then c.DeleteSelf()
            Next
            Me.DeletedList.Clear()

            For Each c As ConsolidatedReportItem In Me
                c.Children.Delete()
            Next

        End Sub

#End Region

    End Class

End Namespace
