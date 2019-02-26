Imports ApskaitaObjects.Workers

Namespace ActiveReports

    ''' <summary>
    ''' Represents a collection of bonus info data (bonus per month per type) for calculating VDU (average wage).
    ''' </summary>
    ''' <remarks>Should only be used as a child of WorkersVDUInfo.</remarks>
    <Serializable()> _
    Public NotInheritable Class BonusVDUInfoList
        Inherits ReadOnlyListBase(Of BonusVDUInfoList, BonusVDUInfo)

#Region " Business Methods "

        Friend Function GetAverageWagePerDay(ByVal scheduledDays As Integer) As Double
            If Not scheduledDays > 0 Then Return 0
            Return CRound(GetTotalBonusBase() / scheduledDays)
        End Function

        Friend Function GetAverageWagePerHour(ByVal scheduledHours As Double) As Double
            If Not CRound(scheduledHours, ROUNDWORKHOURS) > 0 Then Return 0
            Return CRound(GetTotalBonusBase() / scheduledHours)
        End Function

        Friend Function GetTotalBonusBase() As Double

            Dim bonusQuarter As Double = 0
            Dim bonusYear As Double = 0

            For Each i As BonusVDUInfo In Me
                If i.BonusType = BonusType.k Then
                    bonusQuarter = CRound(bonusQuarter + i.BonusAmount)
                Else
                    bonusYear = CRound(bonusYear + i.BonusAmount)
                End If
            Next

            Return CRound(bonusQuarter + (bonusYear / 4))

        End Function

        Friend Function GetTotalYearlyBonus() As Double

            Dim result As Double = 0

            For Each i As BonusVDUInfo In Me
                If i.BonusType = BonusType.m Then
                    result = CRound(result + i.BonusAmount)
                End If
            Next

            Return result

        End Function

        Friend Function GetTotalMonthlyBonus() As Double

            For Each i As BonusVDUInfo In Me
                If i.BonusType = BonusType.k Then
                    Return i.BonusAmount
                End If
            Next

            Return 0

        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function NewBonusVDUInfoList() As BonusVDUInfoList
            Return New BonusVDUInfoList
        End Function

        Friend Shared Function GetBonusVDUInfoList(ByVal contractSerial As String, ByVal contractNumber As Integer, _
            ByVal calculationDate As Date, ByVal includeCurrentMonth As Boolean) As BonusVDUInfoList
            Return New BonusVDUInfoList(contractSerial, contractNumber, calculationDate, includeCurrentMonth)
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

            Dim maxYear, maxMonth, minYear, minMonth As Integer

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

            minYear = calculationDate.Year - 1
            minMonth = calculationDate.Month
            If Not includeCurrentMonth Then
                If minMonth = 1 Then
                    minYear = minYear - 1
                    minMonth = 12
                Else
                    minMonth = minMonth - 1
                End If
            End If

            Dim myComm As New SQLCommand("FetchBonusVDUInfoList")
            myComm.AddParam("?CS", contractSerial)
            myComm.AddParam("?CN", contractNumber)
            myComm.AddParam("?YF", minYear)
            myComm.AddParam("?MF", minMonth)
            myComm.AddParam("?YT", maxYear)
            myComm.AddParam("?MT", maxMonth)

            Using myData As DataTable = myComm.Fetch()

                RaiseListChangedEvents = False
                IsReadOnly = False

                For Each dr As DataRow In myData.Rows
                    Dim newItem As BonusVDUInfo = BonusVDUInfo.GetBonusVDUInfo(dr, calculationDate)
                    If newItem.BonusType <> BonusType.k OrElse Not ContainsQuarterBonus() Then
                        Add(newItem)
                    End If
                Next

                IsReadOnly = True
                RaiseListChangedEvents = True

            End Using

        End Sub

        Private Function ContainsQuarterBonus() As Boolean
            For Each i As BonusVDUInfo In Me
                If i.BonusType = BonusType.k Then Return True
            Next
            Return False
        End Function

#End Region

    End Class

End Namespace