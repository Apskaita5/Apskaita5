Namespace ActiveReports

    <Serializable()> _
    Public Class WageSheetInfo
        Inherits ReadOnlyBase(Of WageSheetInfo)

#Region " Business Methods "

        Private _ID As Integer
        Private _Date As Date
        Private _Number As Integer
        Private _Year As Integer
        Private _Month As Integer
        Private _RateHR As Double
        Private _RateON As Double
        Private _RateSC As Double
        Private _RateSickLeave As Double
        Private _RateGPM As Double
        Private _RateSODRAEmployee As Double
        Private _RateSODRAEmployer As Double
        Private _RatePSDEmployee As Double
        Private _RatePSDEmployer As Double
        Private _RateGuaranteeFund As Double
        Private _IsNonClosing As Boolean
        Private _FormulaNPD As String
        Private _WorkersCount As Integer
        Private _HoursWorked As Double
        Private _DaysWorked As Integer
        Private _PayOutWage As Double
        Private _PayOutHoliday As Double
        Private _PayOutSickLeave As Double
        Private _PayOutRedundancy As Double
        Private _DeductionsGPM As Double
        Private _DeductionsSODRA As Double
        Private _DeductionsPSD As Double
        Private _DeductionsPSDSickLeave As Double
        Private _DeductionsImprest As Double
        Private _DeductionsOther As Double
        Private _PayOutAfterDeductions As Double
        Private _ContributionsSODRA As Double
        Private _ContributionsGuaranteeFund As Double
        Private _PayedOut As Double
        Private _Debt As Double
        Private _IsPayedOut As Boolean


        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        Public ReadOnly Property [Date]() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Date
            End Get
        End Property

        Public ReadOnly Property Number() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Number
            End Get
        End Property

        Public ReadOnly Property Year() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Year
            End Get
        End Property

        Public ReadOnly Property Month() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Month
            End Get
        End Property

        Public ReadOnly Property RateHR() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_RateHR)
            End Get
        End Property

        Public ReadOnly Property RateON() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_RateON)
            End Get
        End Property

        Public ReadOnly Property RateSC() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_RateSC)
            End Get
        End Property

        Public ReadOnly Property RateSickLeave() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_RateSickLeave)
            End Get
        End Property

        Public ReadOnly Property RateGPM() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_RateGPM)
            End Get
        End Property

        Public ReadOnly Property RateSODRAEmployee() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_RateSODRAEmployee)
            End Get
        End Property

        Public ReadOnly Property RateSODRAEmployer() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_RateSODRAEmployer)
            End Get
        End Property

        Public ReadOnly Property RatePSDEmployee() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_RatePSDEmployee)
            End Get
        End Property

        Public ReadOnly Property RatePSDEmployer() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_RatePSDEmployer)
            End Get
        End Property

        Public ReadOnly Property RateGuaranteeFund() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_RateGuaranteeFund)
            End Get
        End Property

        Public ReadOnly Property IsNonClosing() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsNonClosing
            End Get
        End Property

        Public ReadOnly Property FormulaNPD() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _FormulaNPD.Trim
            End Get
        End Property

        Public ReadOnly Property WorkersCount() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _WorkersCount
            End Get
        End Property

        Public ReadOnly Property HoursWorked() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_HoursWorked)
            End Get
        End Property

        Public ReadOnly Property DaysWorked() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DaysWorked
            End Get
        End Property

        Public ReadOnly Property PayOutWage() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_PayOutWage)
            End Get
        End Property

        Public ReadOnly Property PayOutHoliday() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_PayOutHoliday)
            End Get
        End Property

        Public ReadOnly Property PayOutSickLeave() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_PayOutSickLeave)
            End Get
        End Property

        Public ReadOnly Property PayOutRedundancy() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_PayOutRedundancy)
            End Get
        End Property

        Public ReadOnly Property DeductionsGPM() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_DeductionsGPM)
            End Get
        End Property

        Public ReadOnly Property DeductionsSODRA() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_DeductionsSODRA)
            End Get
        End Property

        Public ReadOnly Property DeductionsPSD() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_DeductionsPSD)
            End Get
        End Property

        Public ReadOnly Property DeductionsPSDSickLeave() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_DeductionsPSDSickLeave)
            End Get
        End Property

        Public ReadOnly Property DeductionsImprest() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_DeductionsImprest)
            End Get
        End Property

        Public ReadOnly Property DeductionsOther() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_DeductionsOther)
            End Get
        End Property

        Public ReadOnly Property PayOutAfterDeductions() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_PayOutAfterDeductions)
            End Get
        End Property

        Public ReadOnly Property ContributionsSODRA() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ContributionsSODRA)
            End Get
        End Property

        Public ReadOnly Property ContributionsGuaranteeFund() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ContributionsGuaranteeFund)
            End Get
        End Property

        Public ReadOnly Property PayedOut() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_PayedOut)
            End Get
        End Property

        Public ReadOnly Property Debt() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_Debt)
            End Get
        End Property

        Public ReadOnly Property IsPayedOut() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsPayedOut
            End Get
        End Property



        Protected Overrides Function GetIdValue() As Object
            Return _ID
        End Function

        Public Overrides Function ToString() As String
            If Not _ID > 0 Then Return ""
            Return _Date.ToShortDateString & " Nr. " & _Number
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function GetWageSheetInfo(ByVal dr As DataRow) As WageSheetInfo
            Return New WageSheetInfo(dr)
        End Function

        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal dr As DataRow)
            Fetch(dr)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal dr As DataRow)

            _ID = CIntSafe(dr.Item(0), 0)
            _Number = CIntSafe(dr.Item(1), 0)
            _Date = CDateSafe(dr.Item(2), Today)
            _Year = CIntSafe(dr.Item(3), 0)
            _Month = CIntSafe(dr.Item(4), 0)
            _IsNonClosing = ConvertDbBoolean(CIntSafe(dr.Item(5), 0))
            _RateHR = CDblSafe(dr.Item(6), 2, 0)
            _RateON = CDblSafe(dr.Item(7), 2, 0)
            _RateSC = CDblSafe(dr.Item(8), 2, 0)
            _RateSickLeave = CDblSafe(dr.Item(9), 2, 0)
            _RateGPM = CDblSafe(dr.Item(10), 2, 0)
            _RateGuaranteeFund = CDblSafe(dr.Item(11), 2, 0)
            _RateSODRAEmployee = CDblSafe(dr.Item(12), 2, 0)
            _RateSODRAEmployer = CDblSafe(dr.Item(13), 2, 0)
            _RatePSDEmployee = CDblSafe(dr.Item(14), 2, 0)
            _RatePSDEmployer = CDblSafe(dr.Item(15), 2, 0)
            _FormulaNPD = CStrSafe(dr.Item(16)).Trim
            _WorkersCount = CIntSafe(dr.Item(17), 0)
            _HoursWorked = CDblSafe(dr.Item(18), 2, 0)
            _DaysWorked = CIntSafe(dr.Item(19), 0)
            _PayOutWage = CRound(CDblSafe(dr.Item(20), 0) + CDblSafe(dr.Item(25), 0) _
                - CDblSafe(dr.Item(21), 0) - CDblSafe(dr.Item(22), 0) - CDblSafe(dr.Item(25), 0))
            _PayOutHoliday = CDblSafe(dr.Item(21), 2, 0)
            _PayOutRedundancy = CDblSafe(dr.Item(22), 2, 0)
            _DeductionsOther = CDblSafe(dr.Item(23), 2, 0)
            _DeductionsImprest = CDblSafe(dr.Item(24), 2, 0)
            _PayOutSickLeave = CDblSafe(dr.Item(25), 2, 0)
            _PayOutAfterDeductions = CDblSafe(dr.Item(26), 2, 0)
            _PayedOut = CDblSafe(dr.Item(27), 2, 0)
            _Debt = CRound(_PayOutAfterDeductions - _PayedOut)
            _IsPayedOut = Not (CRound(_Debt) > 0)
            _DeductionsSODRA = CDblSafe(dr.Item(28), 2, 0)
            _ContributionsSODRA = CRound(CDblSafe(dr.Item(29), 2, 0) + CDblSafe(dr.Item(33), 2, 0))
            _DeductionsGPM = CDblSafe(dr.Item(30), 2, 0)
            _ContributionsGuaranteeFund = CDblSafe(dr.Item(31), 2, 0)
            _DeductionsPSD = CDblSafe(dr.Item(32), 2, 0)
            _DeductionsPSDSickLeave = CDblSafe(dr.Item(34), 2, 0)

        End Sub

#End Region

    End Class

End Namespace