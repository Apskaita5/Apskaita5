Namespace Workers

    <Serializable()> _
    Public Class WageItemList
        Inherits BusinessListBase(Of WageItemList, WageItem)

#Region " Business Methods "

        Private _WageRates As CompanyWageRates = Nothing
        Private _Year As Integer = 0
        Private _Month As Integer = 0

        Friend ReadOnly Property Year() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Year
            End Get
        End Property

        Friend ReadOnly Property Month() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Month
            End Get
        End Property

        Friend ReadOnly Property WageRates() As CompanyWageRates
            Get
                Return _WageRates
            End Get
        End Property


        Friend Sub UpdateTaxRates(ByVal NewWageRates As CompanyWageRates, ByVal RaiseResetBindings As Boolean)

            RaiseListChangedEvents = False

            _WageRates = NewWageRates
            For Each i As WageItem In Me
                If i.IsChecked Then i.RecalculateDeductionsAndTaxes(_WageRates, _Year, _Month, False, False)
            Next

            RaiseListChangedEvents = True

            If RaiseResetBindings Then ResetBindings()

        End Sub

        Friend Sub UpdateWageRates(ByVal NewWageRates As CompanyWageRates, ByVal RaiseResetBindings As Boolean)

            RaiseListChangedEvents = False

            _WageRates = NewWageRates
            For Each i As WageItem In Me
                If i.IsChecked Then i.RecalculatePay(_WageRates, _Year, _Month, False, False)
            Next

            RaiseListChangedEvents = True

            If RaiseResetBindings Then ResetBindings()

        End Sub

        Friend Sub CalculateNPD(ByVal RaiseResetBindings As Boolean)

            RaiseListChangedEvents = False

            For Each i As WageItem In Me
                If i.IsChecked Then i.CalculateNPD(_WageRates, _Year, _Month, False)
            Next

            RaiseListChangedEvents = True

            If RaiseResetBindings Then ResetBindings()

        End Sub

        Friend Sub UpdateWorkersVDUInfo(ByVal NewWorkersVDUInfo As WorkersVDUInfoList, _
            ByVal RaiseResetBindings As Boolean)

            RaiseListChangedEvents = False

            For Each i As WageItem In Me

                Dim itemVDU As WorkersVDUInfo = NewWorkersVDUInfo.GetWorkersVDUInfo( _
                    i.ContractSerial, i.ContractNumber)

                If Not itemVDU Is Nothing Then i.UpdateApplicableVDU(itemVDU, False)

            Next

            RaiseListChangedEvents = True

            If RaiseResetBindings Then ResetBindings()

        End Sub


        Friend Function GetTotalSum() As Double
            Dim result As Double = 0
            For Each i As WageItem In Me
                If i.IsChecked Then result = CRound(result + i.PayOutTotal)
            Next
            Return result
        End Function

        Friend Function GetTotalSumAfterDeductions() As Double
            Dim result As Double = 0
            For Each i As WageItem In Me
                If i.IsChecked Then result = CRound(result + i.PayOutTotalAfterDeductions)
            Next
            Return result
        End Function

        Friend Function GetTotalSumPayable() As Double
            Dim result As Double = 0
            For Each i As WageItem In Me
                If i.IsChecked Then result = CRound(result + i.PayableTotal)
            Next
            Return result
        End Function

        Friend Function GetTotalSumPayed() As Double
            Dim result As Double = 0
            For Each i As WageItem In Me
                If i.IsChecked Then result = CRound(result + i.PayedOutTotalSum)
            Next
            Return result
        End Function

        Friend Function GetTotalSODRAPayments() As Double
            Dim result As Double = 0
            For Each i As WageItem In Me
                If i.IsChecked Then result = CRound(result + i.DeductionSODRA + i.ContributionSODRA)
            Next
            Return result
        End Function

        Friend Function GetTotalPSDPaymentsForSODRA() As Double
            Dim result As Double = 0
            For Each i As WageItem In Me
                If i.IsChecked Then result = CRound(result + i.DeductionPSD + i.ContributionPSD)
            Next
            Return result
        End Function

        Friend Function GetTotalPSDPaymentsForVMI() As Double
            Dim result As Double = 0
            For Each i As WageItem In Me
                If i.IsChecked Then result = CRound(result + i.DeductionPSDSickLeave)
            Next
            Return result
        End Function

        Friend Function GetTotalGPMDeductions() As Double
            Dim result As Double = 0
            For Each i As WageItem In Me
                If i.IsChecked Then result = CRound(result + i.DeductionGPM)
            Next
            Return result
        End Function

        Friend Function GetTotalImprestDeductions() As Double
            Dim result As Double = 0
            For Each i As WageItem In Me
                If i.IsChecked Then result = CRound(result + i.DeductionImprest)
            Next
            Return result
        End Function

        Friend Function GetTotalOtherDeductions() As Double
            Dim result As Double = 0
            For Each i As WageItem In Me
                If i.IsChecked Then result = CRound(result + i.DeductionOtherApplicable)
            Next
            Return result
        End Function

        Friend Function GetTotalGuaranteeFundContributions() As Double
            Dim result As Double = 0
            For Each i As WageItem In Me
                If i.IsChecked Then result = CRound(result + i.ContributionGuaranteeFund)
            Next
            Return result
        End Function

        Friend Function GetTotalCosts() As Double
            Dim result As Double = 0
            For Each i As WageItem In Me
                If i.IsChecked Then result = CRound(result + i.PayOutTotal + i.ContributionGuaranteeFund + _
                    i.ContributionSODRA + i.ContributionPSD)
            Next
            Return result
        End Function


        Public Function GetAllBrokenRules() As String
            Dim result As String = GetAllBrokenRulesForList(Me)

            'Dim GeneralErrorString As String = ""
            'SomeGeneralValidationSub(GeneralErrorString)
            'AddWithNewLine(result, GeneralErrorString, False)

            Return result
        End Function

        Public Function GetAllWarnings() As String
            Dim result As String = GetAllWarningsForList(Me)
            'Dim GeneralErrorString As String = ""
            'SomeGeneralValidationSub(GeneralErrorString)
            'AddWithNewLine(result, GeneralErrorString, False)

            Return result
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function NewWageItemList(ByVal myData As DataTable, _
            ByVal nWageRates As CompanyWageRates, ByVal nYear As Integer, _
            ByVal nMonth As Integer) As WageItemList
            Return New WageItemList(myData, nWageRates, nYear, nMonth)
        End Function

        Friend Shared Function GetWageItemList(ByVal myData As DataTable, _
            ByVal nWageRates As CompanyWageRates, ByVal nYear As Integer, _
            ByVal nMonth As Integer, ByVal nFinancialDataCanChange As Boolean) As WageItemList
            Return New WageItemList(myData, nWageRates, nYear, nMonth, nFinancialDataCanChange)
        End Function


        Private Sub New()
            ' require use of factory methods
            MarkAsChild()
            Me.AllowEdit = True
            Me.AllowNew = False
            Me.AllowRemove = False
        End Sub


        Private Sub New(ByVal myData As DataTable, ByVal nWageRates As CompanyWageRates, _
            ByVal nYear As Integer, ByVal nMonth As Integer)

            ' require use of factory methods

            MarkAsChild()

            Me.AllowEdit = True
            Me.AllowNew = False
            Me.AllowRemove = False

            Create(myData, nWageRates, nYear, nMonth)

        End Sub

        Private Sub New(ByVal myData As DataTable, ByVal nWageRates As CompanyWageRates, _
            ByVal nYear As Integer, ByVal nMonth As Integer, ByVal nFinancialDataCanChange As Boolean)
            ' require use of factory methods

            MarkAsChild()

            Me.AllowEdit = nFinancialDataCanChange
            Me.AllowNew = False
            Me.AllowRemove = False

            Fetch(myData, nWageRates, nYear, nMonth, nFinancialDataCanChange)

        End Sub

#End Region

#Region " Data Access "

        Private Sub Create(ByVal myData As DataTable, ByVal nWageRates As CompanyWageRates, _
            ByVal nYear As Integer, ByVal nMonth As Integer)

            RaiseListChangedEvents = False

            Dim cWorkTime As WorkTime = WorkTime.GetWorktime(nYear, nMonth)
            Dim wDays As Integer = cWorkTime.WorkDaysFor5WorkDayWeek
            Dim wHours As Double = cWorkTime.WorkHoursFor5WorkDayWeek

            _Year = nYear
            _Month = nMonth
            _WageRates = nWageRates

            For Each dr As DataRow In myData.Rows
                Add(WageItem.NewWageItem(dr, wDays, wHours, _WageRates, _Year, _Month))
            Next

            RaiseListChangedEvents = True

        End Sub

        Private Sub Fetch(ByVal myData As DataTable, ByVal nWageRates As CompanyWageRates, _
            ByVal nYear As Integer, ByVal nMonth As Integer, ByVal nFinancialDataCanChange As Boolean)

            RaiseListChangedEvents = False

            _Year = nYear
            _Month = nMonth
            _WageRates = nWageRates

            For Each dr As DataRow In myData.Rows
                Add(WageItem.GetWageItem(dr, _WageRates, _Year, _Month, nFinancialDataCanChange))
            Next

            RaiseListChangedEvents = True

        End Sub

        Friend Sub Update(ByVal parent As WageSheet)

            RaiseListChangedEvents = False
            DeletedList.Clear()

            For Each item As WageItem In Me
                If item.IsNew AndAlso item.IsChecked Then
                    item.Insert(parent)
                ElseIf Not item.IsNew AndAlso Not item.IsChecked Then
                    item.DeleteSelf()
                ElseIf Not item.IsNew AndAlso item.IsDirty Then
                    item.Update(parent)
                End If
            Next

            Me.AllowRemove = True
            For i As Integer = Me.Count To 1 Step -1
                If Not Item(i - 1).IsChecked Then Me.RemoveAt(i - 1)
            Next
            Me.AllowRemove = False
            DeletedList.Clear()

            RaiseListChangedEvents = True

        End Sub

#End Region

    End Class

End Namespace