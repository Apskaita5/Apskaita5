Namespace ActiveReports

    ''' <summary>
    ''' Represents a collection of items in a <see cref="WorkerWageInfoReport">worker wage report</see>.
    ''' Contains information about worker's wage parameters and payments 
    ''' for each month within the report period.
    ''' </summary>
    ''' <remarks>Should only be used as a child of <see cref="WorkerWageInfoReport">WorkerWageInfoReport</see>.</remarks>
    <Serializable()> _
    Public Class WorkerWageInfoList
        Inherits ReadOnlyListBase(Of WorkerWageInfoList, WorkerWageInfo)

#Region " Business Methods "

        Friend Function GetTotalSum() As Double
            Dim result As Double = 0
            For Each i As WorkerWageInfo In Me
                result = CRound(result + i.PayOutTotal)
            Next
            Return result
        End Function

        Friend Function GetTotalSumPayable() As Double
            Dim result As Double = 0
            For Each i As WorkerWageInfo In Me
                result = CRound(result + i.PayableTotal)
            Next
            Return result
        End Function

        Friend Function GetTotalSumPayed() As Double
            Dim result As Double = 0
            For Each i As WorkerWageInfo In Me
                result = CRound(result + i.PayedOutTotalSum)
            Next
            Return result
        End Function

        Friend Function GetTotalSODRAPayments() As Double
            Dim result As Double = 0
            For Each i As WorkerWageInfo In Me
                result = CRound(result + i.DeductionSODRA + i.ContributionSODRA)
            Next
            Return result
        End Function

        Friend Function GetTotalPSDPaymentsForSODRA() As Double
            Dim result As Double = 0
            For Each i As WorkerWageInfo In Me
                result = CRound(result + i.DeductionPSD + i.ContributionPSD)
            Next
            Return result
        End Function

        Friend Function GetTotalPSDPaymentsForVMI() As Double
            Dim result As Double = 0
            For Each i As WorkerWageInfo In Me
                result = CRound(result + i.DeductionPSDSickLeave)
            Next
            Return result
        End Function

        Friend Function GetTotalGPMDeductions() As Double
            Dim result As Double = 0
            For Each i As WorkerWageInfo In Me
                result = CRound(result + i.DeductionGPM)
            Next
            Return result
        End Function

        Friend Function GetTotalOtherDeductions() As Double
            Dim result As Double = 0
            For Each i As WorkerWageInfo In Me
                result = CRound(result + i.DeductionOther)
            Next
            Return result
        End Function

        Friend Function GetTotalGuaranteeFundContributions() As Double
            Dim result As Double = 0
            For Each i As WorkerWageInfo In Me
                result = CRound(result + i.ContributionGuaranteeFund)
            Next
            Return result
        End Function

        Friend Function GetTotalCosts() As Double
            Dim result As Double = 0
            For Each i As WorkerWageInfo In Me
                result = CRound(result + i.PayOutTotal + i.ContributionGuaranteeFund + _
                    i.ContributionSODRA + i.ContributionPSD)
            Next
            Return result
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function GetWorkerWageInfoList(ByVal myData As DataTable, _
            ByVal paymentsData As DataTable) As WorkerWageInfoList
            Return New WorkerWageInfoList(myData, paymentsData)
        End Function


        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal myData As DataTable, ByVal paymentsData As DataTable)
            ' require use of factory methods
            Fetch(myData, paymentsData)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal myData As DataTable, ByVal paymentsData As DataTable)

            RaiseListChangedEvents = False
            IsReadOnly = False

            Dim periods As List(Of String) = GetYearMonthList(myData, paymentsData)

            For Each dr As String In periods
                Add(WorkerWageInfo.GetWorkerWageInfo(GetRow(dr, myData), GetRow(dr, paymentsData)))
            Next

            IsReadOnly = True
            RaiseListChangedEvents = True

        End Sub


        Private Function GetRow(ByVal listValue As String, _
            ByVal myData As DataTable) As DataRow

            Dim year As Integer = Convert.ToInt32(listValue.Split(New Char() {":"c}, _
                StringSplitOptions.RemoveEmptyEntries)(0))
            Dim month As Integer = Convert.ToInt32(listValue.Split(New Char() {":"c}, _
                StringSplitOptions.RemoveEmptyEntries)(1))

            Return GetRowByMonth(year, month, myData)

        End Function

        Friend Shared Function GetRowByMonth(ByVal year As Integer, ByVal month As Integer, _
            ByVal myData As DataTable) As DataRow

            For Each dr As DataRow In myData.Rows
                If CIntSafe(dr.Item(0), -1) = year AndAlso CIntSafe(dr.Item(1), -1) = month Then
                    Return dr
                End If
            Next

            Return Nothing

        End Function

        Private Function GetYearMonthList(ByVal myData As DataTable, _
            ByVal paymentsData As DataTable) As List(Of String)

            Dim result As New List(Of String)

            Dim current As String
            For Each dr As DataRow In myData.Rows
                current = String.Format("{0}:{1}", CIntSafe(dr.Item(0), -1).ToString(), _
                    GetMinLengthString(CIntSafe(dr.Item(1), -1).ToString(), 2, "0"c, True))
                If Not result.Contains(current) Then result.Add(current)
            Next

            For Each dr As DataRow In paymentsData.Rows
                current = String.Format("{0}:{1}", CIntSafe(dr.Item(0), -1).ToString(), _
                    GetMinLengthString(CIntSafe(dr.Item(1), -1).ToString(), 2, "0"c, True))
                If Not result.Contains(current) Then result.Add(current)
            Next

            result.Sort()

            Return result

        End Function

#End Region

    End Class

End Namespace