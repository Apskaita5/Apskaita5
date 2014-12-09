Namespace ActiveReports

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

        Friend Function GetTotalSumAfterDeductions() As Double
            Dim result As Double = 0
            For Each i As WorkerWageInfo In Me
                result = CRound(result + i.PayOutTotalAfterDeductions)
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

        Friend Function GetTotalImprestDeductions() As Double
            Dim result As Double = 0
            For Each i As WorkerWageInfo In Me
                result = CRound(result + i.DeductionImprest)
            Next
            Return result
        End Function

        Friend Function GetTotalOtherDeductions() As Double
            Dim result As Double = 0
            For Each i As WorkerWageInfo In Me
                result = CRound(result + i.DeductionOtherApplicable)
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

        Friend Shared Function GetWorkerWageInfoList(ByVal myData As DataTable) As WorkerWageInfoList
            Dim result As WorkerWageInfoList = New WorkerWageInfoList(myData)
            Return result
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
                Add(WorkerWageInfo.GetWorkerWageInfo(dr))
            Next

            Dim i, j As Integer
            For i = Me.Count To 1 Step -1
                For j = 1 To i - 1
                    If Item(i - 1).Year = Item(j - 1).Year AndAlso Item(i - 1).Month = Item(j - 1).Month Then
                        Item(j - 1).AddWageItemForInfoObject(Item(i - 1))
                        MyBase.Remove(Item(i - 1))
                        Exit For
                    End If
                Next
            Next

            IsReadOnly = True
            RaiseListChangedEvents = True

        End Sub

#End Region

    End Class

End Namespace