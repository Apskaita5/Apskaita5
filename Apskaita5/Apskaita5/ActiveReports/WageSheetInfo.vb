Namespace ActiveReports

    ''' <summary>
    ''' Represents an item of a <see cref="WageSheetInfoList">wage sheet report</see>. 
    ''' Contains information about a <see cref="Workers.WageSheet">WageSheet</see>.
    ''' </summary>
    ''' <remarks>Values are stored in the database tables du_ziniarastis and du_ziniarastis_d.</remarks>
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


        ''' <summary>
        ''' Gets <see cref="General.JournalEntry.ID">an ID of the journal entry</see> that is created by the wage sheet.
        ''' </summary>
        ''' <remarks>Value is stored in the database table du_ziniarastis.ID.</remarks>
        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' Gets the date of the sheet.
        ''' </summary>
        ''' <remarks>Value is stored in the database field du_ziniarastis.Z_data.</remarks>
        Public ReadOnly Property [Date]() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Date
            End Get
        End Property

        ''' <summary>
        ''' Gets the number of the sheet.
        ''' </summary>
        ''' <remarks>Value is stored in the database field du_ziniarastis.Nr.</remarks>
        Public ReadOnly Property Number() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Number
            End Get
        End Property

        ''' <summary>
        ''' Gets the year of the wage calculations within the sheet.
        ''' </summary>
        ''' <remarks>Value is stored in the database field du_ziniarastis.Metai.</remarks>
        Public ReadOnly Property Year() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Year
            End Get
        End Property

        ''' <summary>
        ''' Gets the month of the wage calculations within the sheet.
        ''' </summary>
        ''' <remarks>Value is stored in the database field du_ziniarastis.Men.</remarks>
        Public ReadOnly Property Month() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Month
            End Get
        End Property

        ''' <summary>
        ''' Wage for work during public holidays and rest days rate (against normal wage).
        ''' </summary>
        ''' <remarks>Value is stored in the database table du_ziniarastis.P_S.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property RateHR() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_RateHR)
            End Get
        End Property

        ''' <summary>
        ''' Wage for overtime and night work rate (against normal wage).
        ''' </summary>
        ''' <remarks>Value is stored in the database table du_ziniarastis.N_V.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property RateON() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_RateON)
            End Get
        End Property

        ''' <summary>
        ''' Wage for dangerous/unsafe work rate (against normal wage).
        ''' </summary>
        ''' <remarks>Value is stored in the database table du_ziniarastis.Y_S.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property RateSC() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_RateSC)
            End Get
        End Property

        ''' <summary>
        ''' Sickness benefit rate as payed by an employer.
        ''' </summary>
        ''' <remarks>Value is stored in the database table du_ziniarastis.Nedarb.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property RateSickLeave() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_RateSickLeave)
            End Get
        End Property

        ''' <summary>
        ''' Personal income tax (GPM) rate.
        ''' </summary>
        ''' <remarks>Value is stored in the database table du_ziniarastis.GPM.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property RateGPM() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_RateGPM)
            End Get
        End Property

        ''' <summary>
        ''' Rate of health insurance contributions deducted from wage.
        ''' </summary>
        ''' <remarks>Value is stored in the database table du_ziniarastis.PSDW.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property RateSODRAEmployee() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_RateSODRAEmployee)
            End Get
        End Property

        ''' <summary>
        ''' Rate of social security contributions payed by an employer.
        ''' </summary>
        ''' <remarks>Value is stored in the database table du_ziniarastis.SD_v.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property RateSODRAEmployer() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_RateSODRAEmployer)
            End Get
        End Property

        ''' <summary>
        ''' Rate of health insurance contributions deducted from wage.
        ''' </summary>
        ''' <remarks>Value is stored in the database table du_ziniarastis.PSDW.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property RatePSDEmployee() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_RatePSDEmployee)
            End Get
        End Property

        ''' <summary>
        ''' Rate of health insurance contributions payed by an employer.
        ''' </summary>
        ''' <remarks>Value is stored in the database table du_ziniarastis.PSDE.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property RatePSDEmployer() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_RatePSDEmployer)
            End Get
        End Property

        ''' <summary>
        ''' Rate of guarantee fund contributions (insolvency insurance for workers).
        ''' </summary>
        ''' <remarks>Value is stored in the database table du_ziniarastis.Garant.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property RateGuaranteeFund() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_RateGuaranteeFund)
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets a boolean values indicating whether the sheet is final for the current month,
        ''' i.e. labour contracts within the sheet will not appear on other sheets for the same month.
        ''' </summary>
        ''' <remarks>Value is stored in the database field du_ziniarastis.Dalin.</remarks>
        Public ReadOnly Property IsNonClosing() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsNonClosing
            End Get
        End Property

        ''' <summary>
        ''' A formula used to calculate a (minimum) not-taxable personal income (NPD).
        ''' </summary>
        ''' <remarks>Value is stored in the database table du_ziniarastis.NPDF.</remarks>
        Public ReadOnly Property FormulaNPD() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _FormulaNPD.Trim
            End Get
        End Property

        ''' <summary>
        ''' Total count of workers within the sheet.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property WorkersCount() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _WorkersCount
            End Get
        End Property

        ''' <summary>
        ''' Total hours worked within the sheet.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, ROUNDWORKHOURS)> _
        Public ReadOnly Property HoursWorked() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_HoursWorked, ROUNDWORKHOURS)
            End Get
        End Property

        ''' <summary>
        ''' Total days worked within the sheet.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DaysWorked() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DaysWorked
            End Get
        End Property

        ''' <summary>
        ''' Total calculated amount of <see cref="Workers.WageItem.ConventionalWage">wage</see> within the sheet.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property PayOutWage() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_PayOutWage)
            End Get
        End Property

        ''' <summary>
        ''' Total calculated pay for the annual holidays.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property PayOutHoliday() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_PayOutHoliday)
            End Get
        End Property

        ''' <summary>
        ''' Total calculated benefit (pay) for sick leave (payed by the employer).
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property PayOutSickLeave() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_PayOutSickLeave)
            End Get
        End Property

        ''' <summary>
        ''' Total amount of redundancy pay (compensation, benefits).
        ''' </summary>
        ''' <remarks>Value is stored in the database table du_ziniarastis_d.II.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property PayOutRedundancy() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_PayOutRedundancy)
            End Get
        End Property

        ''' <summary>
        ''' Total calculated personal income tax (GPM), that is deducted from wage.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property DeductionsGPM() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_DeductionsGPM)
            End Get
        End Property

        ''' <summary>
        ''' Total calculated social security insurance contribution (SODRA), that is deducted from wage.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property DeductionsSODRA() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_DeductionsSODRA)
            End Get
        End Property

        ''' <summary>
        ''' Total calculated health insurance contribution (PSD), that is deducted from wage and payed to VMI.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property DeductionsPSD() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_DeductionsPSD)
            End Get
        End Property

        ''' <summary>
        ''' Total calculated health insurance contribution (PSD), that is deducted from wage and payed to VMI.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property DeductionsPSDSickLeave() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_DeductionsPSDSickLeave)
            End Get
        End Property

        ''' <summary>
        ''' Total amount of imprest, that is deducted from wage.
        ''' </summary>
        ''' <remarks>Value is stored in the database table du_ziniarastis_d.Avans.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property DeductionsImprest() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_DeductionsImprest)
            End Get
        End Property

        ''' <summary>
        ''' Total amount of other deductions (e.g. to lay damages).
        ''' </summary>
        ''' <remarks>Value is stored in the database table du_ziniarastis_d.Issk.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property DeductionsOther() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_DeductionsOther)
            End Get
        End Property

        ''' <summary>
        ''' Total netto wage minus imprest (part of wage already payed in advance).
        ''' </summary>
        ''' <remarks> Value is stored in the database table du_ziniarastis_d.DU_is.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property PayOutAfterDeductions() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_PayOutAfterDeductions)
            End Get
        End Property

        ''' <summary>
        ''' Total calculated social security insurance contribution (SODRA), that is payed by the employer.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property ContributionsSODRA() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ContributionsSODRA)
            End Get
        End Property

        ''' <summary>
        ''' Total calculated guarantee fund (insolvency insurance) contribution.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property ContributionsGuaranteeFund() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ContributionsGuaranteeFund)
            End Get
        End Property

        ''' <summary>
        ''' Total amount of wage that was payed out.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property PayedOut() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_PayedOut)
            End Get
        End Property

        ''' <summary>
        ''' Total amount of wage that was NOT payed out (wage debt).
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property Debt() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_Debt)
            End Get
        End Property

        ''' <summary>
        ''' Whether all the wage within the current sheet has already been payed.
        ''' </summary>
        ''' <remarks>Value is stored in the database table du_ziniarastis_d.Ismoketa (not null).</remarks>
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
            Return String.Format(My.Resources.Workers_WageSheet_ToString, _
                _Date.ToString("yyyy-MM-dd"), _Number.ToString(), _Year.ToString(), _
                _Month.ToString(), _ID.ToString())
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
            _HoursWorked = CDblSafe(dr.Item(18), ROUNDWORKHOURS, 0)
            _DaysWorked = CIntSafe(dr.Item(19), 0)
            _PayOutWage = CRound(CDblSafe(dr.Item(20), 2, 0) + CDblSafe(dr.Item(25), 2, 0) _
                - CDblSafe(dr.Item(21), 2, 0) - CDblSafe(dr.Item(22), 2, 0) - CDblSafe(dr.Item(25), 2, 0), 2)
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