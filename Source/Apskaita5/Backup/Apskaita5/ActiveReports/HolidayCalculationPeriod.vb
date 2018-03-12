Imports ApskaitaObjects.Attributes

Namespace ActiveReports

    ''' <summary>
    ''' Represents a work period which a fixed holiday rate is applied for.
    ''' </summary>
    ''' <remarks>Should only by used as a child of HolidayCalculationPeriodList.</remarks>
    <Serializable()> _
    Public NotInheritable Class HolidayCalculationPeriod
        Inherits ReadOnlyBase(Of HolidayCalculationPeriod)

#Region " Business Methods "

        Private ReadOnly _Guid As Guid = Guid.NewGuid()
        Private _DateBegin As Date = Today
        Private _DateEnd As Date = Today
        Private _LengthDays As Integer = 0
        Private _LengthYears As Double = 0
        Private _HolidayRate As Integer = 0
        Private _CumulatedHolidayDaysPerPeriod As Double = 0
        Private _StatusDescription As String = ""


        ''' <summary>
        ''' Gets a start date of the calculation period.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DateBegin() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DateBegin
            End Get
        End Property

        ''' <summary>
        ''' Gets an end date of the calculation period.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DateEnd() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DateEnd
            End Get
        End Property

        ''' <summary>
        ''' Gets a length of the calculation period in days.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property LengthDays() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _LengthDays
            End Get
        End Property

        ''' <summary>
        ''' Gets a length of the calculation period in years.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, ROUNDWORKYEARS)> _
        Public ReadOnly Property LengthYears() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_LengthYears, ROUNDWORKYEARS)
            End Get
        End Property

        ''' <summary>
        ''' Gets a holiday rate (holiday days per work year) that is applicable for the calculation period.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property HolidayRate() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _HolidayRate
            End Get
        End Property

        ''' <summary>
        ''' Gets an amount of cumulated holiday days for the calculation period
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, ROUNDACCUMULATEDHOLIDAY)> _
        Public ReadOnly Property CumulatedHolidayDaysPerPeriod() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_CumulatedHolidayDaysPerPeriod, ROUNDACCUMULATEDHOLIDAY)
            End Get
        End Property

        ''' <summary>
        ''' Gets a description of the HolidayRate change at the begining of the calculation period.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property StatusDescription() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _StatusDescription.Trim
            End Get
        End Property



        Protected Overrides Function GetIdValue() As Object
            Return _Guid
        End Function

        Public Overrides Function ToString() As String
            Return String.Format(My.Resources.ActiveReports_HolidayCalculationPeriod_ToString, _
                _DateBegin.ToString("yyyy-MM-dd"), _DateEnd.ToString("yyyy-MM-dd"), _
                DblParser(_CumulatedHolidayDaysPerPeriod, ROUNDACCUMULATEDHOLIDAY))
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function GetHolidayCalculationPeriod(ByVal drStart As DataRow, _
            ByVal drEnd As DataRow) As HolidayCalculationPeriod
            Return New HolidayCalculationPeriod(drStart, drEnd)
        End Function

        Friend Shared Function GetHolidayCalculationPeriod(ByVal dr As DataRow, _
            ByVal calculationDate As Date) As HolidayCalculationPeriod
            Return New HolidayCalculationPeriod(dr, calculationDate)
        End Function


        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal drStart As DataRow, ByVal drEnd As DataRow)
            Fetch(drStart, drEnd)
        End Sub

        Private Sub New(ByVal dr As DataRow, ByVal calculationDate As Date)
            Fetch(dr, calculationDate)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal drStart As DataRow, ByVal drEnd As DataRow)

            _DateBegin = CDateSafe(drStart.Item(0), Today)
            _DateEnd = CDateSafe(drEnd.Item(0), Today)
            _HolidayRate = CIntSafe(drStart.Item(1), 0)
            Dim statusChangeType As Workers.WorkerStatusType = _
                Utilities.ConvertDatabaseCharID(Of Workers.WorkerStatusType)(CStrSafe(drStart.Item(2)))
            If statusChangeType = Workers.WorkerStatusType.Employed Then
                _StatusDescription = My.Resources.ActiveReports_HolidayCalculationPeriod_LabourContractDate
            Else
                _StatusDescription = CStrSafe(drStart.Item(3))
                If Utilities.ConvertDatabaseCharID(Of Workers.WorkerStatusType) _
                    (CStrSafe(drEnd.Item(2))) = Workers.WorkerStatusType.Fired Then
                    _StatusDescription = _StatusDescription & My.Resources.ActiveReports_HolidayCalculationPeriod_LabourContractTerminationDate
                End If
            End If
            Calculate()

        End Sub

        Private Sub Fetch(ByVal dr As DataRow, ByVal calculationDate As Date)

            _DateBegin = CDateSafe(dr.Item(0), Today)
            _DateEnd = calculationDate
            _HolidayRate = CIntSafe(dr.Item(1), 0)
            Dim statusChangeType As Workers.WorkerStatusType = _
                Utilities.ConvertDatabaseCharID(Of Workers.WorkerStatusType)(CStrSafe(dr.Item(2)))
            If statusChangeType = Workers.WorkerStatusType.Employed Then
                _StatusDescription = My.Resources.ActiveReports_HolidayCalculationPeriod_LabourContractDate
            Else
                _StatusDescription = CStrSafe(dr.Item(3))
            End If
            _StatusDescription = _StatusDescription & My.Resources.ActiveReports_HolidayCalculationPeriod_CalculationEndDate
            Calculate()

        End Sub

        Private Sub Calculate()
            _LengthDays = Convert.ToInt32(DateDiff(DateInterval.Day, _DateBegin, _DateEnd))
            _LengthYears = CRound(_LengthDays / AVERAGEDAYSINYEAR, ROUNDWORKYEARS)
            _CumulatedHolidayDaysPerPeriod = CRound(_LengthYears * _HolidayRate, ROUNDACCUMULATEDHOLIDAY)
        End Sub

#End Region

    End Class

End Namespace