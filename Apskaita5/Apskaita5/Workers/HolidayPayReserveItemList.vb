Namespace Workers

    ''' <summary>
    ''' Represents a collection of holiday pay reserve items.
    ''' </summary>
    ''' <remarks>Should only be used as a child of a <see cref="ImprestSheet">ImprestSheet</see>.
    ''' Values are stored in the database table d_avansai_d.</remarks>
    <Serializable()> _
    Public Class HolidayPayReserveItemList
        Inherits BusinessListBase(Of HolidayPayReserveItemList, HolidayPayReserveItem)

#Region " Business Methods "

        Friend Function GetTotalSum() As Double
            Dim result As Double = 0
            For Each i As HolidayPayReserveItem In Me
                result = CRound(result + i.HolidayPayReserveValue)
            Next
            Return result
        End Function


        Public Function GetAllBrokenRules() As String
            Dim result As String = GetAllBrokenRulesForList(Me)
            Return result
        End Function

        Public Function GetAllWarnings() As String
            Dim result As String = GetAllWarningsForList(Me)
            Return result
        End Function

        Public Function HasWarnings() As Boolean
            For Each i As HolidayPayReserveItem In Me
                If i.BrokenRulesCollection.WarningCount > 0 Then Return True
            Next
            Return False
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function NewHolidayPayReserveItemList(ByVal nDate As Date) As HolidayPayReserveItemList
            Return New HolidayPayReserveItemList(nDate)
        End Function

        Friend Shared Function GetHolidayPayReserveItemList(ByVal nID As Integer, _
            ByVal nDate As Date, ByVal nFinancialDataCanChange As Boolean) As HolidayPayReserveItemList
            Return New HolidayPayReserveItemList(nID, nDate, nFinancialDataCanChange)
        End Function


        Private Sub New()
            ' require use of factory methods
            MarkAsChild()
            Me.AllowEdit = True
            Me.AllowNew = False
            Me.AllowRemove = True
        End Sub


        Private Sub New(ByVal nDate As Date)
            ' require use of factory methods
            MarkAsChild()
            Me.AllowEdit = True
            Me.AllowNew = False
            Me.AllowRemove = True
            Create(nDate)
        End Sub

        Private Sub New(ByVal nID As Integer, ByVal nDate As Date, ByVal nFinancialDataCanChange As Boolean)
            ' require use of factory methods
            MarkAsChild()
            Me.AllowEdit = nFinancialDataCanChange
            Me.AllowNew = False
            Me.AllowRemove = nFinancialDataCanChange
            Fetch(nID, nDate, nFinancialDataCanChange)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Create(ByVal nDate As Date)

            RaiseListChangedEvents = False

            Dim wt As WorkTime = WorkTime.GetWorktime(nDate.Year, nDate.Month)

            Dim myComm As New SQLCommand("CreateHolidayPayReserveItemList")
            myComm.AddParam("?DT", nDate.Date)

            Using myData As DataTable = myComm.Fetch()
                For Each dr As DataRow In myData.Rows
                    Add(HolidayPayReserveItem.NewHolidayPayReserveItem(nDate, _
                        wt.WorkHoursFor5WorkDayWeek, wt.WorkDaysFor5WorkDayWeek, dr))
                Next
            End Using

            RaiseListChangedEvents = True

        End Sub

        Private Sub Fetch(ByVal nID As Integer, ByVal nDate As Date, ByVal nFinancialDataCanChange As Boolean)

            RaiseListChangedEvents = False

            Dim wt As WorkTime = WorkTime.GetWorktime(nDate.Year, nDate.Month)

            Dim myComm As New SQLCommand("FetchHolidayPayReserveItemList")
            myComm.AddParam("?DT", nDate.Date)
            myComm.AddParam("?PD", nID)

            Using myData As DataTable = myComm.Fetch()
                For Each dr As DataRow In myData.Rows
                    Add(HolidayPayReserveItem.GetHolidayPayReserveItem(nDate, _
                        wt.WorkHoursFor5WorkDayWeek, wt.WorkDaysFor5WorkDayWeek, _
                        dr, nFinancialDataCanChange))
                Next
            End Using

            RaiseListChangedEvents = True

        End Sub

        Friend Sub Update(ByVal parent As HolidayPayReserve)

            RaiseListChangedEvents = False

            For Each i As HolidayPayReserveItem In DeletedList
                If Not i.IsNew Then
                    i.DeleteSelf()
                End If
            Next
            DeletedList.Clear()

            For Each i As HolidayPayReserveItem In Me
                If i.IsNew Then
                    i.Insert(parent)
                ElseIf i.IsDirty Then
                    i.Update(parent)
                End If
            Next

            RaiseListChangedEvents = True

        End Sub

#End Region

    End Class

End Namespace
