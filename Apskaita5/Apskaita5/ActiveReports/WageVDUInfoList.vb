Namespace ActiveReports

    ''' <summary>
    ''' Represents a collection of wage info data (wage per month) item for calculating VDU (average wage).
    ''' </summary>
    ''' <remarks>Should only be used as a child of WorkersVDUInfo.</remarks>
    <Serializable()> _
    Public NotInheritable Class WageVDUInfoList
        Inherits ReadOnlyListBase(Of WageVDUInfoList, WageVDUInfo)

#Region " Business Methods "

        Friend Function GetAverageWagePerDay() As Double

            Dim wage As Double = 0
            Dim days As Integer = 0

            For Each i As WageVDUInfo In Me
                wage = CRound(wage + i.Wage)
                days = days + i.WorkDays
            Next

            If CRound(wage) > 0 AndAlso days > 0 Then
                Return CRound(wage / days)
            Else
                Return 0
            End If

        End Function

        Friend Function GetAverageWagePerHour() As Double

            Dim wage As Double = 0
            Dim hours As Double = 0

            For Each i As WageVDUInfo In Me
                wage = CRound(wage + i.Wage)
                hours = CRound(hours + i.WorkHours, ROUNDWORKHOURS)
            Next

            If CRound(wage) > 0 AndAlso hours > 0 Then
                Return CRound(wage / hours)
            Else
                Return 0
            End If

        End Function

        Friend Function GetTotalScheduledDays() As Integer
            Dim result As Integer = 0
            For Each i As WageVDUInfo In Me
                result = result + i.ScheduledDays
            Next
            Return result
        End Function

        Friend Function GetTotalScheduledHours() As Double
            Dim result As Double = 0
            For Each i As WageVDUInfo In Me
                result = CRound(result + i.ScheduledHours, ROUNDWORKHOURS)
            Next
            Return result
        End Function

        Friend Function GetTotalWorkDays() As Integer
            Dim result As Integer = 0
            For Each i As WageVDUInfo In Me
                result = result + i.WorkDays
            Next
            Return result
        End Function

        Friend Function GetTotalWorkHours() As Double
            Dim result As Double = 0
            For Each i As WageVDUInfo In Me
                result = CRound(result + i.WorkHours, ROUNDWORKHOURS)
            Next
            Return result
        End Function

        Friend Function GetTotalWage() As Double
            Dim result As Double = 0
            For Each i As WageVDUInfo In Me
                result = CRound(result + i.Wage)
            Next
            Return result
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function NewWageVDUInfoList() As WageVDUInfoList
            Return New WageVDUInfoList
        End Function

        Friend Shared Function GetWageVDUInfoList(ByVal contractSerial As String, _
            ByVal contractNumber As Integer, ByVal calculationDate As Date, _
            ByVal includeCurrentMonth As Boolean) As WageVDUInfoList
            Return New WageVDUInfoList(contractSerial, contractNumber, calculationDate, includeCurrentMonth)
        End Function


        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal contractSerial As String, ByVal contractNumber As Integer, _
            ByVal calculationDate As Date, ByVal includeCurrentMonth As Boolean)
            ' require use of factory methods
            Fetch(contractSerial, contractNumber, calculationDate, includeCurrentMonth)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal contractSerial As String, ByVal contractNumber As Integer, _
            ByVal calculationDate As Date, ByVal includeCurrentMonth As Boolean)

            Dim maxYear, maxMonth As Integer
            maxYear = calculationDate.Year
            maxMonth = calculationDate.Month
            If Not includeCurrentMonth Then
                If maxMonth = 1 Then
                    maxYear = maxYear - 1
                    maxMonth = 12
                Else
                    maxMonth = maxMonth - 1
                End If
            End If

            Dim myComm As New SQLCommand("FetchWageVDUInfoList")
            myComm.AddParam("?CS", contractSerial)
            myComm.AddParam("?CN", contractNumber)
            myComm.AddParam("?YR", maxYear)
            myComm.AddParam("?MN", maxMonth)

            Using myData As DataTable = myComm.Fetch()

                RaiseListChangedEvents = False
                IsReadOnly = False

                For Each dr As DataRow In myData.Rows
                    Add(WageVDUInfo.GetWageVDUInfo(dr))
                Next

                IsReadOnly = True
                RaiseListChangedEvents = True

            End Using

        End Sub

#End Region

    End Class

End Namespace