Imports ApskaitaObjects.ActiveReports
Namespace Workers

    ''' <summary>
    ''' Represents a holiday pay reserve item, that evaluates future costs of the current unused holiday amount.
    ''' </summary>
    ''' <remarks>Should only be used as a child of a <see cref="HolidayPayReserveItemList">HolidayPayReserveItemList</see>.
    ''' Values are stored in the database table d_avansai_d.</remarks>
    <Serializable()> _
    Public Class HolidayPayReserveItem
        Inherits BusinessBase(Of HolidayPayReserveItem)
        Implements IGetErrorForListItem

#Region " Business Methods "

        Private Const DefaultWorkDaysRatio As Double = 0.7

        Private ReadOnly _Guid As Guid = Guid.NewGuid
        Private _ID As Integer = 0
        Private _FinancialDataCanChange As Boolean = True
        Private _HolidayInfo As WorkerHolidayInfo = Nothing
        Private _VduInfo As WorkersVDUInfo = Nothing
        Private _ApplicableVDUDaily As Double = 0
        Private _ApplicableUnusedHolidayDays As Double = 0
        Private _ApplicableWorkDaysRatio As Double = 0
        Private _Comments As String = ""


        ''' <summary>
        ''' Gets an ID of the holiday pay reserve item that is assigned by a database (AUTOINCREMENT).
        ''' </summary>
        ''' <remarks>Value is stored in the database field d_avansai_d.ID.</remarks>
        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' Returnes TRUE if the parent holiday pay reserve calculation allows financial changes due to business restrains.
        ''' </summary>
        ''' <remarks>Chronologic business restrains are provided by a <see cref="SimpleChronologicValidator">SimpleChronologicValidator</see>.</remarks>
        Public ReadOnly Property FinancialDataCanChange() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _FinancialDataCanChange
            End Get
        End Property

        ''' <summary>
        ''' Gets an <see cref="General.Person.ID">ID of the person</see> (worker) that is assigned to the current labour contract.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property PersonID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _HolidayInfo.PersonID
            End Get
        End Property

        ''' <summary>
        ''' Gets a <see cref="General.Person.Name">name of the person</see> (worker) that is assigned to the current labour contract.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property PersonName() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _HolidayInfo.PersonName.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a <see cref="General.Person.Code">personal code of the person</see> (worker) that is assigned to the current labour contract.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property PersonCode() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _HolidayInfo.PersonCode.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a <see cref="General.Person.CodeSODRA">social security code of the person</see> (worker) that is assigned to the current labour contract.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property PersonCodeSodra() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _HolidayInfo.PersonCodeSodra.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets the serial number (code) of the current labour contract.
        ''' </summary>
        ''' <remarks>A labour contract is identified by a <see cref="Workers.WageItem.ContractSerial">serial number</see> 
        ''' and <see cref="Workers.WageItem.ContractNumber">running number</see> pair.</remarks>
        Public ReadOnly Property ContractSerial() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _HolidayInfo.ContractSerial.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets the running number of the current labour contract.
        ''' </summary>
        ''' <remarks>A labour contract is identified by a <see cref="Workers.WageItem.ContractSerial">serial number</see> 
        ''' and <see cref="Workers.WageItem.ContractNumber">running number</see> pair.</remarks>
        Public ReadOnly Property ContractNumber() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _HolidayInfo.ContractNumber
            End Get
        End Property

        ''' <summary>
        ''' Gets the date of the current labour contract.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property ContractDate() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _HolidayInfo.ContractDate
            End Get
        End Property

        ''' <summary>
        ''' Gets the workers position for the current labour contract.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Position() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _HolidayInfo.Position.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets the holiday rate (holiday days per work year) for the current labour contract for the current month.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property HolidayRate() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _HolidayInfo.HolidayRate
            End Get
        End Property

        ''' <summary>
        ''' Gets the current applicable work load for the current labour contract for the current month.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, ROUNDWORKLOAD)> _
        Public ReadOnly Property WorkLoad() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _HolidayInfo.WorkLoad
            End Get
        End Property

        ''' <summary>
        ''' Gets the total length in days of all the periods that the holiday days are calculated for.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property TotalWorkPeriodInDays() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _HolidayInfo.TotalWorkPeriodInDays
            End Get
        End Property

        ''' <summary>
        ''' Gets the total length in years of all the periods that the holiday days are calculated for.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, ROUNDWORKYEARS)> _
        Public ReadOnly Property TotalWorkPeriodInYears() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _HolidayInfo.TotalWorkPeriodInYears
            End Get
        End Property

        ''' <summary>
        ''' Gets the total calculated holiday days.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, ROUNDACCUMULATEDHOLIDAY)> _
        Public ReadOnly Property TotalCumulatedHolidayDays() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _HolidayInfo.TotalCumulatedHolidayDays
            End Get
        End Property

        ''' <summary>
        ''' Gets the total holiday days that were granted to the worker.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property TotalHolidayDaysGranted() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _HolidayInfo.TotalHolidayDaysGranted
            End Get
        End Property

        ''' <summary>
        ''' Gets the total holiday days in technical (manual) corrections.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, ROUNDACCUMULATEDHOLIDAY)> _
        Public ReadOnly Property TotalHolidayDaysCorrection() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _HolidayInfo.TotalHolidayDaysCorrection
            End Get
        End Property

        ''' <summary>
        ''' Gets the total holiday days in technical (manual) corrections.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, ROUNDACCUMULATEDHOLIDAY)> _
        Public ReadOnly Property TotalHolidayDaysUsed() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _HolidayInfo.TotalHolidayDaysUsed
            End Get
        End Property

        ''' <summary>
        ''' Gets the total unused holiday days (<see cref="TotalCumulatedHolidayDays">TotalCumulatedHolidayDays</see> - <see cref="TotalHolidayDaysUsed">TotalHolidayDaysUsed</see>).
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, ROUNDACCUMULATEDHOLIDAY)> _
        Public ReadOnly Property TotalUnusedHolidayDays() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _HolidayInfo.TotalUnusedHolidayDays
            End Get
        End Property

        ''' <summary>
        ''' Gets the total scheduled work hours for the current labour contract for the current month.
        ''' </summary>
        ''' <remarks>Used to calculate VDU when no historical data is available.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, ROUNDWORKHOURS)> _
        Public ReadOnly Property StandartHoursForTheCurrentMonth() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _VduInfo.StandartHoursForTheCurrentMonth
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets the number of scheduled work days for the current labour contract for the current month.
        ''' </summary>
        ''' <remarks>Used to calculate VDU when no historical data is available.</remarks>
        <IntegerField(ValueRequiredLevel.Optional, False)> _
        Public ReadOnly Property StandartDaysForTheCurrentMonth() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _VduInfo.StandartDaysForTheCurrentMonth
            End Get
        End Property

        ''' <summary>
        ''' Gets the current applicable wage for the current labour contract for the current month.
        ''' </summary>
        ''' <remarks>Used to calculate VDU when no historical data is available.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property ConventionalWage() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _VduInfo.ConventionalWage
            End Get
        End Property

        ''' <summary>
        ''' Gets the <see cref="Workers.WageType">type</see> of the current applicable wage 
        ''' for the current labour contract for the current month.
        ''' </summary>
        ''' <remarks>Used to calculate VDU when no historical data is available.</remarks>
        Public ReadOnly Property WageType() As WageType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _VduInfo.WageType
            End Get
        End Property

        ''' <summary>
        ''' Gets the human readable (regionalized) description of the <see cref="Workers.WageType">type</see> 
        ''' of the current applicable wage for the current labour contract for the current month.
        ''' </summary>
        ''' <remarks>Used to calculate VDU when no historical data is available.</remarks>
        Public ReadOnly Property WageTypeHumanReadable() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _VduInfo.WageTypeHumanReadable
            End Get
        End Property

        ''' <summary>
        ''' Gets the current applicable extra pay for the current labour contract for the current month.
        ''' </summary>
        ''' <remarks>Used to calculate VDU when no historical data is available.
        ''' In case VDU is calculated for the <see cref="WageItem">WageItem</see>, 
        ''' <see cref="WageItem.OtherPayRelatedToWork">OtherPayRelatedToWork</see> is added to the conventional extra pay.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property ConventionalExtraPay() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _VduInfo.ConventionalExtraPay
            End Get
        End Property

        ''' <summary>
        ''' Gets the total amount of scheduled work days within calculation period.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property TotalScheduledDays() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _VduInfo.TotalScheduledDays
            End Get
        End Property

        ''' <summary>
        ''' Gets the total amount of scheduled work hours within calculation period.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, ROUNDWORKHOURS)> _
        Public ReadOnly Property TotalScheduledHours() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _VduInfo.TotalScheduledHours
            End Get
        End Property

        ''' <summary>
        ''' Gets the total amount of actual work days within calculation period.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property TotalWorkDays() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _VduInfo.TotalWorkDays
            End Get
        End Property

        ''' <summary>
        ''' Gets the total amount of actual work hours within calculation period.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, ROUNDWORKHOURS)> _
        Public ReadOnly Property TotalWorkHours() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _VduInfo.TotalWorkHours
            End Get
        End Property

        ''' <summary>
        ''' Gets the total amount of calculated wage within calculation period.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property TotalWage() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _VduInfo.TotalWage
            End Get
        End Property

        ''' <summary>
        ''' Gets the wage component of average hourly wage (hourly VDU).
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property WageVDUHourly() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _VduInfo.WageVDUHourly
            End Get
        End Property

        ''' <summary>
        ''' Gets the wage component of average daily wage (daily VDU).
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property WageVDUDaily() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _VduInfo.WageVDUDaily
            End Get
        End Property

        ''' <summary>
        ''' Gets the total yearly bonus amount within calculation period (12 month).
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property BonusYearly() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _VduInfo.BonusYearly
            End Get
        End Property

        ''' <summary>
        ''' Gets the last quarterly bonus amount within calculation period (12 month).
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property BonusQuarterly() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _VduInfo.BonusQuarterly
            End Get
        End Property

        ''' <summary>
        ''' Gets the total bonus amount within calculation period that is used as a base for VDU calculation.
        ''' </summary>
        ''' <remarks>Equals <see cref="BonusQuarterly">BonusQuarterly</see> plus 1/4 <see cref="BonusYearly">BonusYearly</see>.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property BonusBase() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _VduInfo.BonusBase
            End Get
        End Property

        ''' <summary>
        ''' Gets the bonus component of average hourly wage (hourly VDU).
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property BonusVDUHourly() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _VduInfo.BonusVDUHourly
            End Get
        End Property

        ''' <summary>
        ''' Gets the bonus component of average daily wage (daily VDU).
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property BonusVDUDaily() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _VduInfo.BonusVDUDaily
            End Get
        End Property

        ''' <summary>
        ''' Gets total calculated average hourly wage (hourly VDU) for the current labour contract.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property TotalVDUHourly() As Double
            Get
                Return _VduInfo.ApplicableVDUHourly
            End Get
        End Property

        ''' <summary>
        ''' Gets total calculated average daily wage (daily VDU) for the current labour contract.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property TotalVDUDaily() As Double
            Get
                Return _VduInfo.ApplicableVDUDaily
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets the applicable daily wage (daily VDU) for the current labour contract
        ''' to use for reserve evaluation.
        ''' </summary>
        ''' <remarks>Value is stored in the database table accumulativecosts.Comments.</remarks>
        <DoubleField(ValueRequiredLevel.Mandatory, False, 2)> _
        Public Property ApplicableVDUDaily() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ApplicableVDUDaily, 2)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Double)
                CanWriteProperty(True)
                If Not _FinancialDataCanChange Then Exit Property
                If CRound(_ApplicableVDUDaily, 2) <> CRound(value, 2) Then
                    _ApplicableVDUDaily = CRound(value, 2)
                    PropertyHasChanged()
                    PropertyHasChanged("HolidayPayReserveValue")
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the applicable unused holiday days for the current labour contract
        ''' to use for reserve evaluation.
        ''' </summary>
        ''' <remarks>Value is stored in the database table accumulativecosts.Comments.</remarks>
        <DoubleField(ValueRequiredLevel.Recommended, False, ROUNDACCUMULATEDHOLIDAY)> _
        Public Property ApplicableUnusedHolidayDays() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ApplicableUnusedHolidayDays, ROUNDACCUMULATEDHOLIDAY)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Double)
                CanWriteProperty(True)
                If Not _FinancialDataCanChange Then Exit Property
                If CRound(_ApplicableUnusedHolidayDays, ROUNDACCUMULATEDHOLIDAY) <> _
                    CRound(value, ROUNDACCUMULATEDHOLIDAY) Then
                    _ApplicableUnusedHolidayDays = CRound(value, ROUNDACCUMULATEDHOLIDAY)
                    PropertyHasChanged()
                    PropertyHasChanged("HolidayPayReserveValue")
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the applicable ratio of work and calendar days for 
        ''' the current labour contract to use for reserve evaluation.
        ''' </summary>
        ''' <remarks>Value is stored in the database table accumulativecosts.Comments.</remarks>
        <DoubleField(ValueRequiredLevel.Mandatory, False, ROUNDWORKDAYSRATIO, True, 0.002, 1, True)> _
        Public Property ApplicableWorkDaysRatio() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ApplicableWorkDaysRatio, ROUNDWORKDAYSRATIO)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Double)
                CanWriteProperty(True)
                If Not _FinancialDataCanChange Then Exit Property
                If CRound(_ApplicableWorkDaysRatio, ROUNDWORKDAYSRATIO) <> _
                    CRound(value, ROUNDWORKDAYSRATIO) Then
                    _ApplicableWorkDaysRatio = CRound(value, ROUNDWORKDAYSRATIO)
                    PropertyHasChanged()
                    PropertyHasChanged("HolidayPayReserveValue")
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets the evaluated holiday pay reserve value for the current labour contract.
        ''' </summary>
        ''' <remarks>Value is calculated as <see cref="ApplicableUnusedHolidayDays">ApplicableUnusedHolidayDays</see>
        ''' multiplied by <see cref="ApplicableVDUDaily">ApplicableVDUDaily</see>
        ''' multiplied by <see cref="ApplicableWorkDaysRatio">ApplicableWorkDaysRatio</see>.</remarks>
        <DoubleField(ValueRequiredLevel.Recommended, False, 2)> _
        Public ReadOnly Property HolidayPayReserveValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ApplicableWorkDaysRatio * _ApplicableUnusedHolidayDays _
                    * _ApplicableVDUDaily, 2)
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets the user comments for the evaluation of the current labour contract.
        ''' </summary>
        ''' <remarks>Value is stored in the database table accumulativecosts.Comments.</remarks>
        <StringField(ValueRequiredLevel.Optional, 255, False)> _
        Public Property Comments() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Comments.Trim
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As String)
                CanWriteProperty(True)
                If value Is Nothing Then value = ""
                If _Comments.Trim <> value.Trim Then
                    _Comments = value.Trim
                    PropertyHasChanged()
                End If
            End Set
        End Property


        Public Function GetErrorString() As String _
            Implements IGetErrorForListItem.GetErrorString
            If IsValid Then Return ""
            Return String.Format(My.Resources.Common_ErrorInItem, Me.ToString, _
                vbCrLf, Me.BrokenRulesCollection.ToString(Validation.RuleSeverity.Error))
        End Function

        Public Function GetWarningString() As String _
            Implements IGetErrorForListItem.GetWarningString
            If BrokenRulesCollection.WarningCount < 1 Then Return ""
            Return String.Format(My.Resources.Common_WarningInItem, Me.ToString, _
                vbCrLf, Me.BrokenRulesCollection.ToString(Validation.RuleSeverity.Warning))
        End Function


        Protected Overrides Function GetIdValue() As Object
            Return _Guid
        End Function

        Public Overrides Function ToString() As String
            Return String.Format(My.Resources.Workers_HolidayPayReserveItem_ToString, _
                _VduInfo.PersonName, _VduInfo.ContractSerial, _VduInfo.ContractNumber)
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()

            ValidationRules.AddRule(AddressOf CommonValidation.StringFieldValidation, _
                New Csla.Validation.RuleArgs("Comments"))
            ValidationRules.AddRule(AddressOf CommonValidation.DoubleFieldValidation, _
                New Csla.Validation.RuleArgs("ApplicableVDUDaily"))
            ValidationRules.AddRule(AddressOf CommonValidation.DoubleFieldValidation, _
                New Csla.Validation.RuleArgs("ApplicableUnusedHolidayDays"))
            ValidationRules.AddRule(AddressOf CommonValidation.DoubleFieldValidation, _
                New Csla.Validation.RuleArgs("ApplicableWorkDaysRatio"))
            ValidationRules.AddRule(AddressOf CommonValidation.DoubleFieldValidation, _
                New Csla.Validation.RuleArgs("HolidayPayReserveValue"))

        End Sub

#End Region

#Region " Authorization Rules "

        Protected Overrides Sub AddAuthorizationRules()

        End Sub

#End Region

#Region " Factory Methods "

        Friend Shared Function NewHolidayPayReserveItem(ByVal nDate As Date, _
            ByVal nStandartHoursForTheCurrentMonth As Double, _
            ByVal nStandartDaysForTheCurrentMonth As Integer, _
            ByVal dr As DataRow) As HolidayPayReserveItem
            Return New HolidayPayReserveItem(nDate, nStandartHoursForTheCurrentMonth, _
                nStandartDaysForTheCurrentMonth, dr)
        End Function

        Friend Shared Function GetHolidayPayReserveItem(ByVal nDate As Date, _
            ByVal nStandartHoursForTheCurrentMonth As Double, _
            ByVal nStandartDaysForTheCurrentMonth As Integer, _
            ByVal dr As DataRow, ByVal nFinancialDataCanChange As Boolean) As HolidayPayReserveItem
            Return New HolidayPayReserveItem(nDate, nStandartHoursForTheCurrentMonth, _
                nStandartDaysForTheCurrentMonth, dr, nFinancialDataCanChange)
        End Function


        Private Sub New()
            ' require use of factory methods
            MarkAsChild()
        End Sub


        Private Sub New(ByVal nDate As Date, ByVal nStandartHoursForTheCurrentMonth As Double, _
            ByVal nStandartDaysForTheCurrentMonth As Integer, ByVal dr As DataRow)
            MarkAsChild()
            Create(nDate, nStandartHoursForTheCurrentMonth, _
                nStandartDaysForTheCurrentMonth, dr)
        End Sub

        Private Sub New(ByVal nDate As Date, ByVal nStandartHoursForTheCurrentMonth As Double, _
            ByVal nStandartDaysForTheCurrentMonth As Integer, ByVal dr As DataRow, _
            ByVal nFinancialDataCanChange As Boolean)
            MarkAsChild()
            Fetch(nDate, nStandartHoursForTheCurrentMonth, _
                nStandartDaysForTheCurrentMonth, dr, nFinancialDataCanChange)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Create(ByVal nDate As Date, ByVal nStandartHoursForTheCurrentMonth As Double, _
            ByVal nStandartDaysForTheCurrentMonth As Integer, ByVal dr As DataRow)

            _HolidayInfo = WorkerHolidayInfo.GetWorkerHolidayInfoChild(nDate, dr)
            _VduInfo = WorkersVDUInfo.GetWorkersVDUInfoChild(nDate, _
                nStandartHoursForTheCurrentMonth, nStandartDaysForTheCurrentMonth, dr)

            _ApplicableVDUDaily = _VduInfo.ApplicableVDUDaily
            _ApplicableUnusedHolidayDays = _HolidayInfo.TotalUnusedHolidayDays
            _ApplicableWorkDaysRatio = DefaultWorkDaysRatio

            MarkNew()

            ValidationRules.CheckRules()

        End Sub


        Private Sub Fetch(ByVal nDate As Date, ByVal nStandartHoursForTheCurrentMonth As Double, _
            ByVal nStandartDaysForTheCurrentMonth As Integer, ByVal dr As DataRow, _
            ByVal nFinancialDataCanChange As Boolean)

            _HolidayInfo = WorkerHolidayInfo.GetWorkerHolidayInfoChild(nDate, dr)
            _VduInfo = WorkersVDUInfo.GetWorkersVDUInfoChild(nDate, _
                nStandartHoursForTheCurrentMonth, nStandartDaysForTheCurrentMonth, dr)

            _ID = CIntSafe(dr.Item(14), 0)
            _ApplicableVDUDaily = CDblSafe(dr.Item(15), 2, 0)
            _ApplicableUnusedHolidayDays = CDblSafe(dr.Item(16), ROUNDACCUMULATEDHOLIDAY, 0)
            _ApplicableWorkDaysRatio = CDblSafe(dr.Item(17), ROUNDWORKDAYSRATIO, 0)
            _Comments = CStrSafe(dr.Item(18))

            _FinancialDataCanChange = nFinancialDataCanChange

            MarkOld()

            ValidationRules.CheckRules()

        End Sub


        Friend Sub Insert(ByVal parentDocument As HolidayPayReserve)

            Dim myComm As New SQLCommand("InsertHolidayPayReserveItem")
            myComm.AddParam("?AA", parentDocument.ID)
            myComm.AddParam("?AE", _Comments.Trim)
            myComm.AddParam("?AF", _HolidayInfo.ContractSerial)
            myComm.AddParam("?AG", _HolidayInfo.ContractNumber)
            AddWithFinancialParams(myComm)

            myComm.Execute()

            _ID = Convert.ToInt32(myComm.LastInsertID)

            MarkOld()

        End Sub

        Friend Sub Update(ByVal parentDocument As HolidayPayReserve)

            Dim myComm As SQLCommand
            If _FinancialDataCanChange Then
                myComm = New SQLCommand("UpdateHolidayPayReserveItem")
                AddWithFinancialParams(myComm)
            Else
                myComm = New SQLCommand("UpdateHolidayPayReserveItemNonFinancial")
            End If
            myComm.AddParam("?AE", _Comments.Trim)
            myComm.AddParam("?CD", _ID)

            myComm.Execute()

            MarkOld()

        End Sub

        Private Sub AddWithFinancialParams(ByRef myComm As SQLCommand)

            myComm.AddParam("?AB", CRound(_ApplicableVDUDaily, 2))
            myComm.AddParam("?AC", CRound(_ApplicableUnusedHolidayDays, ROUNDACCUMULATEDHOLIDAY))
            myComm.AddParam("?AD", CRound(_ApplicableWorkDaysRatio, ROUNDWORKDAYSRATIO))

        End Sub


        Friend Sub DeleteSelf()

            Dim myComm As New SQLCommand("DeleteHolidayPayReserveItem")
            myComm.AddParam("?CD", _ID)

            myComm.Execute()

            MarkNew()

        End Sub

#End Region

    End Class

End Namespace