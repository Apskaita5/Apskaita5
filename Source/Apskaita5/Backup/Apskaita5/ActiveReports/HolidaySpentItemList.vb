Namespace ActiveReports

    ''' <summary>
    ''' Represents a collection of info about holiday used by a worker.
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public NotInheritable Class HolidaySpentItemList
        Inherits ReadOnlyListBase(Of HolidaySpentItemList, HolidaySpentItem)

#Region " Business Methods "

        Friend Function GetTotal() As Double
            Dim result As Double = 0
            For Each i As HolidaySpentItem In Me
                result = CRound(result + i.Total, ROUNDACCUMULATEDHOLIDAY)
            Next
            Return result
        End Function

        Friend Function GetSpent() As Integer
            Dim result As Integer = 0
            For Each i As HolidaySpentItem In Me
                result = result + i.Spent
            Next
            Return result
        End Function

        Friend Function GetCompensated() As Double
            Dim result As Double = 0
            For Each i As HolidaySpentItem In Me
                result = CRound(result + i.Compensated, ROUNDACCUMULATEDHOLIDAY)
            Next
            Return result
        End Function

        Friend Function GetCorrections() As Double
            Dim result As Double = 0
            For Each i As HolidaySpentItem In Me
                result = CRound(result + i.Correction, ROUNDACCUMULATEDHOLIDAY)
            Next
            Return result
        End Function

        Friend Function IncludesCompensation() As Boolean
            For Each i As HolidaySpentItem In Me
                If i.Type = HolidaySpentItemType.Compensated Then Return True
            Next
            Return False
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function NewHolidaySpentItemList() As HolidaySpentItemList
            Return New HolidaySpentItemList
        End Function

        Friend Shared Function GetHolidaySpentItemList(ByVal myData As DataTable) As HolidaySpentItemList
            Return New HolidaySpentItemList(myData)
        End Function


        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal myData As DataTable)
            ' require use of factory methods
            Fetch(myData)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal myData As DataTable)

            RaiseListChangedEvents = False
            IsReadOnly = False

            For Each dr As DataRow In myData.Rows
                Add(HolidaySpentItem.GetHolidaySpentItem(dr))
            Next

            IsReadOnly = True
            RaiseListChangedEvents = True

        End Sub

#End Region

    End Class

End Namespace