﻿Imports ApskaitaObjects.Attributes

Namespace Workers

    ''' <summary>
    ''' Represents a labour contract update (amendment).
    ''' </summary>
    ''' <remarks>Values are stored in in the database table darbuotojai_d as a collection of status items
    ''' with the same serial, number and date. The collection should not contain status row of type
    ''' <see cref="WorkerStatusType.Employed">WorkerStatusType.Employed</see> as such collection 
    ''' represents a <see cref="Contract">Contract</see>.</remarks>
    <Serializable()> _
    Public NotInheritable Class ContractUpdate
        Inherits BusinessBase(Of ContractUpdate)
        Implements IIsDirtyEnough, IValidationMessageProvider

#Region " Business Methods "

        Private Const FIELDCHANGEDPOSTFIX As String = "Changed"

        Private _ID As Integer = 0
        Private _ChronologicValidator As ContractChronologicValidator
        Private _InsertDate As DateTime = Now
        Private _UpdateDate As DateTime = Now
        Private _ExtraPayID As Integer = 0
        Private _AnnualHolidayID As Integer = 0
        Private _HolidayCorrectionID As Integer = 0
        Private _NpdID As Integer = 0
        Private _PnpdID As Integer = 0
        Private _WageID As Integer = 0
        Private _WorkLoadID As Integer = 0
        Private _PositionID As Integer = 0

        Private _PersonID As Integer = 0
        Private _PersonName As String = ""
        Private _PersonCode As String = ""
        Private _PersonSodraCode As String = ""
        Private _PersonAddress As String = ""
        Private _Serial As String = ""
        Private _Number As Integer = 0

        Private _Date As Date = Today
        Private _OldDate As Date = Today
        Private _Content As String = ""
        Private _PositionChanged As Boolean = False
        Private _WageChanged As Boolean = False
        Private _ExtraPayChanged As Boolean = False
        Private _AnnualHolidayChanged As Boolean = False
        Private _HolidayCorrectionChanged As Boolean = False
        Private _NpdChanged As Boolean = False
        Private _PnpdChanged As Boolean = False
        Private _WorkLoadChanged As Boolean = False
        Private _Position As String = ""
        Private _Wage As Double = 0
        Private _WageType As WageType = Workers.WageType.Position
        Private _HumanReadableWageType As String = Utilities.ConvertLocalizedName(Workers.WageType.Position)
        Private _ExtraPay As Double = 0
        Private _AnnualHoliday As Integer = 28
        Private _HolidayCorrection As Double = 0
        Private _NPD As Double = 0
        Private _PNPD As Double = 0
        Private _WorkLoad As Double = 1


        ''' <summary>
        ''' Gets an ID of the contract update that is assigned by a database (AUTOINCREMENT).
        ''' </summary>
        ''' <remarks>Value is stored in the database field darbuotojai_d.ID
        ''' (the least value of all the status rows within the contract update).</remarks>
        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' Gets <see cref="IChronologicValidator">IChronologicValidator</see> object that contains business restraints on updating the contract update.
        ''' </summary>
        ''' <remarks>Underlying type is <see cref="ContractChronologicValidator">ContractChronologicValidator</see>.</remarks>
        Public ReadOnly Property ChronologicValidator() As ContractChronologicValidator
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ChronologicValidator
            End Get
        End Property

        ''' <summary>
        ''' Gets the date and time when the contract update was inserted into the database.
        ''' </summary>
        ''' <remarks>Value is stored in in the database field darbuotojai_d.InsertDate
        ''' (for each status row).</remarks>
        Public ReadOnly Property InsertDate() As DateTime
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _InsertDate
            End Get
        End Property

        ''' <summary>
        ''' Gets the date and time when the contract update was last updated.
        ''' </summary>
        ''' <remarks>Value is stored in in the database field darbuotojai_d.UpdateDate
        ''' (for each status row).</remarks>
        Public ReadOnly Property UpdateDate() As DateTime
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _UpdateDate
            End Get
        End Property

        ''' <summary>
        ''' Gets an ID for the status row of type <see cref="WorkerStatusType.ExtraPay">WorkerStatusType.ExtraPay</see>
        ''' that is assigned by a database (AUTOINCREMENT).
        ''' </summary>
        ''' <remarks>Value is stored in in the database field darbuotojai_d.ID.</remarks>
        Public ReadOnly Property ExtraPayID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ExtraPayID
            End Get
        End Property

        ''' <summary>
        ''' Gets an ID for the status row of type <see cref="WorkerStatusType.Holiday">WorkerStatusType.Holiday</see>
        ''' that is assigned by a database (AUTOINCREMENT).
        ''' </summary>
        ''' <remarks>Value is stored in in the database field darbuotojai_d.ID.</remarks>
        Public ReadOnly Property AnnualHolidayID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AnnualHolidayID
            End Get
        End Property

        ''' <summary>
        ''' Gets an ID for the status row of type <see cref="WorkerStatusType.HolidayCorrection">WorkerStatusType.HolidayCorrection</see>
        ''' that is assigned by a database (AUTOINCREMENT).
        ''' </summary>
        ''' <remarks>Value is stored in in the database field darbuotojai_d.ID.</remarks>
        Public ReadOnly Property HolidayCorrectionID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _HolidayCorrectionID
            End Get
        End Property

        ''' <summary>
        ''' Gets an ID for the status row of type <see cref="WorkerStatusType.NPD">WorkerStatusType.NPD</see>
        ''' that is assigned by a database (AUTOINCREMENT).
        ''' </summary>
        ''' <remarks>Value is stored in in the database field darbuotojai_d.ID.</remarks>
        Public ReadOnly Property NpdID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _NpdID
            End Get
        End Property

        ''' <summary>
        ''' Gets an ID for the status row of type <see cref="WorkerStatusType.PNPD">WorkerStatusType.PNPD</see>
        ''' that is assigned by a database (AUTOINCREMENT).
        ''' </summary>
        ''' <remarks>Value is stored in in the database field darbuotojai_d.ID.</remarks>
        Public ReadOnly Property PnpdID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _PnpdID
            End Get
        End Property

        ''' <summary>
        ''' Gets an ID for the status row of type <see cref="WorkerStatusType.Wage">WorkerStatusType.Wage</see>
        ''' that is assigned by a database (AUTOINCREMENT).
        ''' </summary>
        ''' <remarks>Value is stored in in the database field darbuotojai_d.ID.</remarks>
        Public ReadOnly Property WageID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _WageID
            End Get
        End Property

        ''' <summary>
        ''' Gets an ID for the status row of type <see cref="WorkerStatusType.WorkLoad">WorkerStatusType.WorkLoad</see>
        ''' that is assigned by a database (AUTOINCREMENT).
        ''' </summary>
        ''' <remarks>Value is stored in in the database field darbuotojai_d.ID.</remarks>
        Public ReadOnly Property WorkLoadID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _WorkLoadID
            End Get
        End Property

        ''' <summary>
        ''' Gets an ID for the status row of type <see cref="WorkerStatusType.Position">WorkerStatusType.Position</see>
        ''' that is assigned by a database (AUTOINCREMENT).
        ''' </summary>
        ''' <remarks>Value is stored in in the database field darbuotojai_d.ID.</remarks>
        Public ReadOnly Property PositionID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _PositionID
            End Get
        End Property

        ''' <summary>
        ''' Gets an <see cref="General.Person.ID">ID of the person</see> (worker) that is assigned to the current labour contract.
        ''' </summary>
        ''' <remarks>Value is stored in the database table darbuotojai_d.AK. (for each status row)</remarks>
        Public ReadOnly Property PersonID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _PersonID
            End Get
        End Property

        ''' <summary>
        ''' Gets a <see cref="General.Person.Name">name of the person</see> (worker) that is assigned to the current labour contract.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="General.Person.Name">General.Person.Name</see>.</remarks>
        Public ReadOnly Property PersonName() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _PersonName.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a <see cref="General.Person.Code">personal code of the person</see> (worker) that is assigned to the current labour contract.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="General.Person.Code">General.Person.Code</see>.</remarks>
        Public ReadOnly Property PersonCode() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _PersonCode.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a <see cref="General.Person.CodeSODRA">social security code of the person</see> (worker) that is assigned to the current labour contract.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="General.Person.CodeSODRA">General.Person.CodeSODRA</see>.</remarks>
        Public ReadOnly Property PersonSodraCode() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _PersonSodraCode.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a <see cref="General.Person.Address">address of the person</see> (worker) that is assigned to the current labour contract.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="General.Person.Address">General.Person.Address</see>.</remarks>
        Public ReadOnly Property PersonAddress() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _PersonAddress.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets the serial of the contract.
        ''' </summary>
        ''' <remarks>Value is stored in in the database field darbuotojai_d.DS_Serija
        ''' (for each status row).</remarks>
        Public ReadOnly Property Serial() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Serial.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets the number of the contract.
        ''' </summary>
        ''' <remarks>Value is stored in in the database field darbuotojai_d.DS_NR
        ''' (for each status row).</remarks>
        Public ReadOnly Property Number() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Number
            End Get
        End Property

        ''' <summary>
        ''' Whether the compensation for unused holiday days was payed.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property HolidayCompensationPayed() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return Not _ChronologicValidator.TerminationCanBeCanceled
            End Get
        End Property

        ''' <summary>
        ''' Gets the date of the contract.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property ContractDate() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ChronologicValidator.ContractDate
            End Get
        End Property

        ''' <summary>
        ''' Gets the contract termination date.
        ''' </summary>
        ''' <remarks>If the contract is not terminated, underlying value equals Date.MaxValue 
        ''' and an empty string is returned.
        ''' Value is stored in in the database field darbuotojai_d.Nuo
        ''' for the status row of type <see cref="WorkerStatusType.Fired">WorkerStatusType.Fired</see>.</remarks>
        Public ReadOnly Property ContractTerminationDate() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If _ChronologicValidator.ContractTerminationDate = Date.MaxValue Then Return ""
                Return _ChronologicValidator.ContractTerminationDate.ToString("yyyy-MM-dd")
            End Get
        End Property

        ''' <summary>
        ''' Gets the original (old) value of the <see cref="[Date]">Date</see> property. (as fetched from a database)
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property OldDate() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _OldDate
            End Get
        End Property


        ''' <summary>
        ''' Gets or sets the date of the contract update.
        ''' </summary>
        ''' <remarks>Value is stored in in the database field darbuotojai_d.Nuo
        ''' (for each status row).</remarks>
        Public Property [Date]() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Date
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Date)
                CanWriteProperty(True)
                If _Date.Date <> value.Date Then
                    _Date = value.Date
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the description of the contract update.
        ''' </summary>
        ''' <remarks>Value is stored in in the database field darbuotojai_d.Pagrindas
        ''' (for each status row)</remarks>
        <StringField(ValueRequiredLevel.Recommended, 255, False)> _
        Public Property Content() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Content.Trim
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As String)
                CanWriteProperty(True)
                If value Is Nothing Then value = ""
                If _Content.Trim <> value.Trim Then
                    _Content = value.Trim
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Whether the worker's position is changed by the contract update.
        ''' </summary>
        ''' <remarks></remarks>
        Public Property PositionChanged() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _PositionChanged
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Boolean)
                CanWriteProperty(True)
                If _PositionChanged <> value Then
                    _PositionChanged = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Whether the worker's wage is changed by the contract update.
        ''' </summary>
        ''' <remarks></remarks>
        Public Property WageChanged() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _WageChanged
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Boolean)
                If Not IsNew AndAlso Not _ChronologicValidator.FinancialDataCanChange Then
                    PropertyHasChanged()
                Else
                    CanWriteProperty(True)
                    If _WageChanged <> value Then
                        _WageChanged = value
                        PropertyHasChanged()
                    End If
                End If
            End Set
        End Property

        ''' <summary>
        ''' Whether the worker's extra pay is changed by the contract update.
        ''' </summary>
        ''' <remarks></remarks>
        Public Property ExtraPayChanged() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ExtraPayChanged
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Boolean)
                If Not IsNew AndAlso Not _ChronologicValidator.FinancialDataCanChange Then
                    PropertyHasChanged()
                Else
                    CanWriteProperty(True)
                    If _ExtraPayChanged <> value Then
                        _ExtraPayChanged = value
                        PropertyHasChanged()
                    End If
                End If
            End Set
        End Property

        ''' <summary>
        ''' Whether the holiday rate is changed by the contract update.
        ''' </summary>
        ''' <remarks></remarks>
        Public Property AnnualHolidayChanged() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AnnualHolidayChanged
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Boolean)
                CanWriteProperty(True)
                If _AnnualHolidayChanged <> value Then
                    _AnnualHolidayChanged = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Whether the technical holiday correction is set by the contract update.
        ''' </summary>
        ''' <remarks></remarks>
        Public Property HolidayCorrectionChanged() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _HolidayCorrectionChanged
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Boolean)
                CanWriteProperty(True)
                If _HolidayCorrectionChanged <> value Then
                    _HolidayCorrectionChanged = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Whether the applicable NPD is changed by the contract update.
        ''' </summary>
        ''' <remarks></remarks>
        Public Property NpdChanged() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _NpdChanged
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Boolean)
                If Not IsNew AndAlso Not _ChronologicValidator.FinancialDataCanChange Then
                    PropertyHasChanged()
                Else
                    CanWriteProperty(True)
                    If _NpdChanged <> value Then
                        _NpdChanged = value
                        PropertyHasChanged()
                    End If
                End If
            End Set
        End Property

        ''' <summary>
        ''' Whether the applicable PNPD is changed by the contract update.
        ''' </summary>
        ''' <remarks></remarks>
        Public Property PnpdChanged() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _PnpdChanged
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Boolean)
                If Not IsNew AndAlso Not _ChronologicValidator.FinancialDataCanChange Then
                    PropertyHasChanged()
                Else
                    CanWriteProperty(True)
                    If _PnpdChanged <> value Then
                        _PnpdChanged = value
                        PropertyHasChanged()
                    End If
                End If
            End Set
        End Property

        ''' <summary>
        ''' Whether the worker's work load is changed by the contract update.
        ''' </summary>
        ''' <remarks></remarks>
        Public Property WorkLoadChanged() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _WorkLoadChanged
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Boolean)
                CanWriteProperty(True)
                If _WorkLoadChanged <> value Then
                    _WorkLoadChanged = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the updated position of the worker.
        ''' </summary>
        ''' <remarks>Value is stored in in the database field darbuotojai_d.DU_tipas
        ''' for the status row of type <see cref="WorkerStatusType.Position">WorkerStatusType.Position</see>.</remarks>
        <StringField(ValueRequiredLevel.Mandatory, 100, False)> _
        Public Property Position() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Position.Trim
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As String)
                CanWriteProperty(True)
                If value Is Nothing Then value = ""
                If _Position.Trim <> value.Trim Then
                    _Position = value.Trim
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the updated wage.
        ''' </summary>
        ''' <remarks>Value is stored in in the database field darbuotojai_d.Dydis
        ''' for the status row of type <see cref="WorkerStatusType.Wage">WorkerStatusType.Wage</see>.</remarks>
        <DoubleField(ValueRequiredLevel.Mandatory, False, 2)> _
        Public Property Wage() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_Wage)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Double)
                If Not IsNew AndAlso Not _ChronologicValidator.FinancialDataCanChange Then
                    PropertyHasChanged()
                Else
                    CanWriteProperty(True)
                    If value < 0 Then value = 0
                    If CRound(_Wage) <> CRound(value) Then
                        _Wage = CRound(value)
                        PropertyHasChanged()
                    End If
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the updated <see cref="Workers.WageType">wage type</see>.
        ''' </summary>
        ''' <remarks>Value is stored in in the database field darbuotojai_d.DU_tipas
        ''' for the status row of type <see cref="WorkerStatusType.Wage">WorkerStatusType.Wage</see>.</remarks>
        Public Property WageType() As WageType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _WageType
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As WageType)
                If Not IsNew AndAlso Not _ChronologicValidator.FinancialDataCanChange Then
                    PropertyHasChanged()
                Else
                    CanWriteProperty(True)
                    If _WageType <> value Then
                        _WageType = value
                        _HumanReadableWageType = Utilities.ConvertLocalizedName(_WageType)
                        PropertyHasChanged()
                        PropertyHasChanged("HumanReadableWageType")
                    End If
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the updated <see cref="Workers.WageType">wage type</see> as a human readable string.
        ''' </summary>
        ''' <remarks>Value is stored in in the database field darbuotojai_d.DU_tipas
        ''' for the status row of type <see cref="WorkerStatusType.Wage">WorkerStatusType.Wage</see>.</remarks>
        <LocalizedEnumField(GetType(WageType), False, "")> _
        Public Property HumanReadableWageType() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _HumanReadableWageType.Trim
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As String)
                If Not IsNew AndAlso Not _ChronologicValidator.FinancialDataCanChange Then
                    PropertyHasChanged()
                Else
                    CanWriteProperty(True)
                    If value Is Nothing Then value = ""
                    Dim enumValue As WageType = Utilities.ConvertLocalizedName(Of WageType)(value)
                    If enumValue <> _WageType Then
                        _WageType = enumValue
                        _HumanReadableWageType = Utilities.ConvertLocalizedName(_WageType)
                        PropertyHasChanged()
                        PropertyHasChanged("WageType")
                    End If
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the updated extra pay.
        ''' </summary>
        ''' <remarks>Value is stored in in the database field darbuotojai_d.Dydis
        ''' for the status row of type <see cref="WorkerStatusType.ExtraPay">WorkerStatusType.ExtraPay</see>.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public Property ExtraPay() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ExtraPay)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Double)
                If Not IsNew AndAlso Not _ChronologicValidator.FinancialDataCanChange Then
                    PropertyHasChanged()
                Else
                    CanWriteProperty(True)
                    If value < 0 Then value = 0
                    If CRound(_ExtraPay) <> CRound(value) Then
                        _ExtraPay = CRound(value)
                        PropertyHasChanged()
                    End If
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the updated annual holiday rate (holiday days per work year).
        ''' </summary>
        ''' <remarks>Value is stored in in the database field darbuotojai_d.Dydis
        ''' for the status row of type <see cref="WorkerStatusType.Holiday">WorkerStatusType.Holiday</see>.</remarks>
        <IntegerField(ValueRequiredLevel.Mandatory, False)> _
        Public Property AnnualHoliday() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AnnualHoliday
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Integer)
                CanWriteProperty(True)
                If value < 0 Then value = 0
                If _AnnualHoliday <> value Then
                    _AnnualHoliday = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the annual holiday technical correction, 
        ''' e.g. when holiday is not calculated for some time (when raising children etc.).
        ''' </summary>
        ''' <remarks>Value is stored in in the database field darbuotojai_d.Dydis
        ''' for the status row of type <see cref="WorkerStatusType.HolidayCorrection">WorkerStatusType.HolidayCorrection</see>.</remarks>
        <DoubleField(ValueRequiredLevel.Mandatory, True, ROUNDACCUMULATEDHOLIDAY)> _
        Public Property HolidayCorrection() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_HolidayCorrection, ROUNDACCUMULATEDHOLIDAY)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Double)
                CanWriteProperty(True)
                If CRound(_HolidayCorrection, ROUNDACCUMULATEDHOLIDAY) <> CRound(value, ROUNDACCUMULATEDHOLIDAY) Then
                    _HolidayCorrection = CRound(value, ROUNDACCUMULATEDHOLIDAY)
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the updated applicable NPD (non-taxable personal income size).
        ''' </summary>
        ''' <remarks>Value is stored in in the database field darbuotojai_d.Dydis
        ''' for the status row of type <see cref="WorkerStatusType.NPD">WorkerStatusType.NPD</see>.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public Property NPD() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_NPD)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Double)
                If Not IsNew AndAlso Not _ChronologicValidator.FinancialDataCanChange Then
                    PropertyHasChanged()
                Else
                    CanWriteProperty(True)
                    If value < 0 Then value = 0
                    If CRound(_NPD) <> CRound(value) Then
                        _NPD = CRound(value)
                        PropertyHasChanged()
                    End If
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the updated applicable PNPD (supplementary non-taxable personal income size).
        ''' </summary>
        ''' <remarks>Value is stored in in the database field darbuotojai_d.Dydis
        ''' for the status row of type <see cref="WorkerStatusType.PNPD">WorkerStatusType.PNPD</see>.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public Property PNPD() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_PNPD)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Double)
                If Not IsNew AndAlso Not _ChronologicValidator.FinancialDataCanChange Then
                    PropertyHasChanged()
                Else
                    CanWriteProperty(True)
                    If value < 0 Then value = 0
                    If CRound(_PNPD) <> CRound(value) Then
                        _PNPD = CRound(value)
                        PropertyHasChanged()
                    End If
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the updated work load (ratio between contractual work hours and gauge work hours (40H/Week)).
        ''' </summary>
        ''' <remarks>Value is stored in in the database field darbuotojai_d.Dydis
        ''' for the status row of type <see cref="WorkerStatusType.WorkLoad">WorkerStatusType.WorkLoad</see>.</remarks>
        <DoubleField(ValueRequiredLevel.Mandatory, False, ROUNDWORKLOAD, True, 0.025, 1.0, False)> _
        Public Property WorkLoad() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_WorkLoad, ROUNDWORKLOAD)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Double)
                CanWriteProperty(True)
                If value < 0 Then value = 0
                If CRound(_WorkLoad, ROUNDWORKLOAD) <> CRound(value, ROUNDWORKLOAD) Then
                    _WorkLoad = CRound(value, ROUNDWORKLOAD)
                    PropertyHasChanged()
                End If
            End Set
        End Property


        ''' <summary>
        ''' Returnes TRUE if the object is new and contains some user provided data 
        ''' OR
        ''' object is not new and was changed by the user.
        ''' </summary>
        Public ReadOnly Property IsDirtyEnough() As Boolean _
            Implements IIsDirtyEnough.IsDirtyEnough
            Get
                If Not IsNew Then Return IsDirty
                Return (Not String.IsNullOrEmpty(_Content.Trim) _
                    OrElse Not String.IsNullOrEmpty(_Position.Trim))
            End Get
        End Property

        Public Overrides ReadOnly Property IsValid() As Boolean _
            Implements IValidationMessageProvider.IsValid
            Get
                Return MyBase.IsValid
            End Get
        End Property



        Public Overrides Function Save() As ContractUpdate

            Me.ValidationRules.CheckRules()
            If Not Me.IsValid Then
                Throw New Exception(String.Format(My.Resources.Common_ContainsErrors, vbCrLf, _
                    GetAllBrokenRules()))
            End If

            Dim result As ContractUpdate = MyBase.Save
            HelperLists.ShortLabourContractList.InvalidateCache()
            Return result

        End Function


        Public Function GetAllBrokenRules() As String _
            Implements IValidationMessageProvider.GetAllBrokenRules
            Dim result As String = ""
            If Not MyBase.IsValid Then result = AddWithNewLine(result, _
                Me.BrokenRulesCollection.ToString(Validation.RuleSeverity.Error), False)
            Return result
        End Function

        Public Function GetAllWarnings() As String _
            Implements IValidationMessageProvider.GetAllWarnings
            Dim result As String = ""
            If Not MyBase.BrokenRulesCollection.WarningCount > 0 Then result = AddWithNewLine(result, _
                Me.BrokenRulesCollection.ToString(Validation.RuleSeverity.Warning), False)
            Return result
        End Function

        Public Function HasWarnings() As Boolean _
            Implements IValidationMessageProvider.HasWarnings
            Return Me.BrokenRulesCollection.WarningCount > 0
        End Function


        Protected Overrides Function GetIdValue() As Object
            Return _ID
        End Function

        Public Overrides Function ToString() As String
            Return String.Format(My.Resources.Workers_ContractUpdate_ToString, _
                _ChronologicValidator.ContractDate.ToString("yyyy-MM-dd"), _Serial, _Number.ToString)
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()

            ValidationRules.AddRule(AddressOf CommonValidation.CommonValidation.ChronologyValidation, _
                New CommonValidation.CommonValidation.ChronologyRuleArgs("Date", "ChronologicValidator"))

            ValidationRules.AddRule(AddressOf CommonValidation.CommonValidation.DoubleFieldValidation, _
                New Csla.Validation.RuleArgs("ExtraPay"))
            ValidationRules.AddRule(AddressOf CommonValidation.CommonValidation.DoubleFieldValidation, _
                New Csla.Validation.RuleArgs("NPD"))
            ValidationRules.AddRule(AddressOf CommonValidation.CommonValidation.DoubleFieldValidation, _
                New Csla.Validation.RuleArgs("PNPD"))

            ValidationRules.AddRule(AddressOf StringRequiredWhenCheckedValidation, _
                New Csla.Validation.RuleArgs("Position"))
            ValidationRules.AddRule(AddressOf DoubleRequiredWhenCheckedValidation, _
                New Csla.Validation.RuleArgs("Wage"))
            ValidationRules.AddRule(AddressOf DoubleRequiredWhenCheckedValidation, _
                New Csla.Validation.RuleArgs("HolidayCorrection"))
            ValidationRules.AddRule(AddressOf DoubleRequiredWhenCheckedValidation, _
                New Csla.Validation.RuleArgs("WorkLoad"))
            ValidationRules.AddRule(AddressOf IntegerRequiredWhenCheckedValidation, _
                New Csla.Validation.RuleArgs("AnnualHoliday"))

            ValidationRules.AddRule(AddressOf ContentValidation, New Validation.RuleArgs("Content"))

            ValidationRules.AddDependantProperty("PositionChanged", "Position", False)
            ValidationRules.AddDependantProperty("WageChanged", "Wage", False)
            ValidationRules.AddDependantProperty("AnnualHolidayChanged", "AnnualHoliday", False)
            ValidationRules.AddDependantProperty("HolidayCorrectionChanged", "HolidayCorrection", False)
            ValidationRules.AddDependantProperty("WorkLoadChanged", "WorkLoad", False)
            ValidationRules.AddDependantProperty("PositionChanged", "Content", False)
            ValidationRules.AddDependantProperty("WageChanged", "Content", False)
            ValidationRules.AddDependantProperty("AnnualHolidayChanged", "Content", False)
            ValidationRules.AddDependantProperty("HolidayCorrectionChanged", "Content", False)
            ValidationRules.AddDependantProperty("WorkLoadChanged", "Content", False)
            ValidationRules.AddDependantProperty("ExtraPayChanged", "Content", False)
            ValidationRules.AddDependantProperty("NpdChanged", "Content", False)
            ValidationRules.AddDependantProperty("PnpdChanged", "Content", False)

        End Sub

        ''' <summary>
        ''' Rule ensuring that at least one status item is selected to be updated.
        ''' </summary>
        ''' <param name="target">Object containing the data to validate</param>
        ''' <param name="e">Arguments parameter specifying the name of the string
        ''' property to validate</param>
        ''' <returns><see langword="false" /> if the rule is broken</returns>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
        Private Shared Function ContentValidation(ByVal target As Object, _
            ByVal e As Validation.RuleArgs) As Boolean

            Dim valObj As ContractUpdate = DirectCast(target, ContractUpdate)

            If Not valObj._AnnualHolidayChanged AndAlso Not valObj._ExtraPayChanged _
                AndAlso Not valObj._HolidayCorrectionChanged AndAlso Not valObj._NpdChanged _
                AndAlso Not valObj._PnpdChanged AndAlso Not valObj._PositionChanged _
                AndAlso Not valObj._WageChanged AndAlso Not valObj._WorkLoadChanged Then
                e.Description = "Nepasirinktas nė vienas keistinas darbo sutarties parametras."
                e.Severity = Validation.RuleSeverity.Error
                Return False
            ElseIf String.IsNullOrEmpty(valObj._Content) Then
                e.Description = "Nėra darbo sutarties (parametrų) pakeitimo aprašymas."
                e.Severity = Validation.RuleSeverity.Warning
                Return False
            End If

            Return True

        End Function

        ''' <summary>
        ''' Rule ensuring that required and recommended double values are set when the item is checked.
        ''' </summary>
        ''' <param name="target">Object containing the data to validate</param>
        ''' <param name="e">Arguments parameter specifying the name of the string
        ''' property to validate</param>
        ''' <returns><see langword="false" /> if the rule is broken</returns>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
        Private Shared Function DoubleRequiredWhenCheckedValidation( _
            ByVal target As Object, ByVal e As Validation.RuleArgs) As Boolean

            If Not DirectCast(CallByName(target, e.PropertyName & FIELDCHANGEDPOSTFIX, CallType.Get), Boolean) Then Return True

            Return DoubleFieldValidation(target, e)

        End Function

        ''' <summary>
        ''' Rule ensuring that required and recommended integer values are set when the item is checked.
        ''' </summary>
        ''' <param name="target">Object containing the data to validate</param>
        ''' <param name="e">Arguments parameter specifying the name of the string
        ''' property to validate</param>
        ''' <returns><see langword="false" /> if the rule is broken</returns>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
        Private Shared Function IntegerRequiredWhenCheckedValidation( _
            ByVal target As Object, ByVal e As Validation.RuleArgs) As Boolean

            If Not DirectCast(CallByName(target, e.PropertyName & FIELDCHANGEDPOSTFIX, CallType.Get), Boolean) Then Return True

            Return IntegerFieldValidation(target, e)

        End Function

        ''' <summary>
        ''' Rule ensuring that required and recommended string values are set when the item is checked.
        ''' </summary>
        ''' <param name="target">Object containing the data to validate</param>
        ''' <param name="e">Arguments parameter specifying the name of the string
        ''' property to validate</param>
        ''' <returns><see langword="false" /> if the rule is broken</returns>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
        Private Shared Function StringRequiredWhenCheckedValidation( _
            ByVal target As Object, ByVal e As Validation.RuleArgs) As Boolean

            If Not DirectCast(CallByName(target, e.PropertyName & FIELDCHANGEDPOSTFIX, CallType.Get), Boolean) Then Return True

            Return StringFieldValidation(target, e)

        End Function

#End Region

#Region " Authorization Rules "

        Protected Overrides Sub AddAuthorizationRules()
            AuthorizationRules.AllowWrite("Workers.Contract2")
        End Sub

        Public Shared Function CanGetObject() As Boolean
            Return ApplicationContext.User.IsInRole("Workers.Contract1")
        End Function

        Public Shared Function CanAddObject() As Boolean
            Return ApplicationContext.User.IsInRole("Workers.Contract2")
        End Function

        Public Shared Function CanEditObject() As Boolean
            Return ApplicationContext.User.IsInRole("Workers.Contract3")
        End Function

        Public Shared Function CanDeleteObject() As Boolean
            Return ApplicationContext.User.IsInRole("Workers.Contract3")
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Get a new contract update instance.
        ''' </summary>
        ''' <param name="contractSerial">A serial of the contract to update.</param>
        ''' <param name="contractNumber">A number of the contract to update.</param>
        ''' <remarks></remarks>
        Public Shared Function NewContractUpdate(ByVal contractSerial As String, _
            ByVal contractNumber As Integer) As ContractUpdate

            Dim result As ContractUpdate = DataPortal.Create(Of ContractUpdate) _
                (New Criteria(contractSerial, contractNumber, Today))
            result.MarkNew()
            Return result

        End Function

        ''' <summary>
        ''' Get an existing contract update from a database.
        ''' </summary>
        ''' <param name="nID">An ID of the contract update to get (of any status row).</param>
        ''' <remarks></remarks>
        Public Shared Function GetContractUpdate(ByVal nID As Integer) As ContractUpdate
            Return DataPortal.Fetch(Of ContractUpdate)(New Criteria(nID))
        End Function

        ''' <summary>
        ''' Get an existing contract update from a database.
        ''' </summary>
        ''' <param name="contractSerial">A serial of the contract to get.</param>
        ''' <param name="contractNumber">A number of the contract to get.</param>
        ''' <param name="contractDate">A contract update date.</param>
        ''' <remarks></remarks>
        Public Shared Function GetContractUpdate(ByVal contractSerial As String, _
            ByVal contractNumber As Integer, ByVal contractDate As Date) As ContractUpdate
            Return DataPortal.Fetch(Of ContractUpdate)(New Criteria(contractSerial, _
                contractNumber, contractDate))
        End Function

        ''' <summary>
        ''' Delete an existing contract update from a database.
        ''' </summary>
        ''' <param name="id">An ID of the contract update to delete. (of any status row)</param>
        ''' <remarks></remarks>
        Public Shared Sub DeleteContractUpdate(ByVal id As Integer)
            DataPortal.Delete(New Criteria(id))
            HelperLists.ShortLabourContractList.InvalidateCache()
        End Sub

        ''' <summary>
        ''' Delete an existing contract update from a database.
        ''' </summary>
        ''' <param name="contractSerial">A serial of the contract.</param>
        ''' <param name="contractNumber">A number of the contract.</param>
        ''' <param name="contractDate">A contract update date.</param>
        ''' <remarks></remarks>
        Public Shared Sub DeleteContractUpdate(ByVal contractSerial As String, _
            ByVal contractNumber As Integer, ByVal contractDate As Date)
            DataPortal.Delete(New Criteria(contractSerial, contractNumber, contractDate))
            HelperLists.ShortLabourContractList.InvalidateCache()
        End Sub


        Private Sub New()
            ' require use of factory methods
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> _
        Private Class Criteria
            Private ReadOnly _ID As Integer = 0
            Private ReadOnly _ContractSerial As String = ""
            Private ReadOnly _ContractNumber As Integer = 0
            Private ReadOnly _ContractDate As Date = Today
            Public ReadOnly Property ID() As Integer
                Get
                    Return _ID
                End Get
            End Property
            Public ReadOnly Property ContractSerial() As String
                Get
                    Return _ContractSerial
                End Get
            End Property
            Public ReadOnly Property ContractNumber() As Integer
                Get
                    Return _ContractNumber
                End Get
            End Property
            Public ReadOnly Property ContractDate() As Date
                Get
                    Return _ContractDate
                End Get
            End Property
            Public Sub New(ByVal nID As Integer)
                _ID = nID
            End Sub
            Public Sub New(ByVal nContractSerial As String, ByVal nContractNumber As Integer, _
                ByVal nContractDate As Date)
                _ContractSerial = nContractSerial
                _ContractNumber = nContractNumber
                _ContractDate = nContractDate
            End Sub
        End Class


        Private Overloads Sub DataPortal_Create(ByVal criteria As Criteria)

            If Not CanAddObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecurityInsertDenied)

            If Not criteria.ID > 0 AndAlso Not criteria.ContractNumber > 0 Then _
                Throw New Exception(My.Resources.Workers_Contract_ContractIDNull)

            Dim myComm As SQLCommand
            If criteria.ID > 0 Then
                myComm = New SQLCommand("CreateNewContractUpdate")
                myComm.AddParam("?CD", criteria.ID)
            Else
                myComm = New SQLCommand("CreateNewContractUpdateBySerialNumber")
                myComm.AddParam("?DS", criteria.ContractSerial.Trim)
                myComm.AddParam("?DN", criteria.ContractNumber)
            End If

            Using myData As DataTable = myComm.Fetch

                If myData.Rows.Count < 1 Then
                    If criteria.ID > 0 Then
                        Throw New Exception(String.Format(My.Resources.Common_ObjectNotFound, _
                            My.Resources.Workers_Contract_TypeName, criteria.ID.ToString()))
                    Else
                        Throw New Exception(String.Format(My.Resources.Common_ObjectNotFound, _
                            My.Resources.Workers_Contract_TypeName, criteria.ContractSerial _
                            & criteria.ContractNumber.ToString()))
                    End If
                End If

                Dim dr As DataRow = myData.Rows(0)

                _Serial = CStrSafe(dr.Item(0)).Trim
                _Number = CIntSafe(dr.Item(1), 0)
                _PersonID = CIntSafe(dr.Item(2), 0)
                _PersonName = CStrSafe(dr.Item(3)).Trim
                _PersonCode = CStrSafe(dr.Item(4)).Trim
                _PersonAddress = CStrSafe(dr.Item(5)).Trim
                _PersonSodraCode = CStrSafe(dr.Item(6)).Trim

            End Using

            _ChronologicValidator = ContractChronologicValidator.NewContractChronologicValidator(_Serial, _Number)

            MarkNew()

            ValidationRules.CheckRules()

        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)

            If Not CanGetObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecuritySelectDenied)

            Dim myComm As SQLCommand
            If criteria.ID > 0 Then
                myComm = New SQLCommand("FetchContractUpdate")
                myComm.AddParam("?CD", criteria.ID)
            Else
                myComm = New SQLCommand("FetchContractUpdateBySerialNumber")
                myComm.AddParam("?DS", criteria.ContractSerial.Trim)
                myComm.AddParam("?DN", criteria.ContractNumber)
                myComm.AddParam("?DT", criteria.ContractDate)
            End If

            Using myData As DataTable = myComm.Fetch

                If myData.Rows.Count < 1 Then
                    If criteria.ID > 0 Then
                        Throw New Exception(String.Format(My.Resources.Common_ObjectNotFound, _
                            My.Resources.Workers_ContractUpdate_TypeName, criteria.ID.ToString()))
                    Else
                        Throw New Exception(String.Format(My.Resources.Common_ObjectNotFound, _
                            My.Resources.Workers_ContractUpdate_TypeName, GetFullID(criteria.ContractSerial, _
                            criteria.ContractNumber, criteria.ContractDate)))
                    End If
                End If

                Dim generalDataAcquired As Boolean = False
                Dim curID As Integer = Integer.MaxValue
                Dim curInsertDate As DateTime = Date.MaxValue
                Dim curTimeStamp As DateTime

                For Each dr As DataRow In myData.Rows

                    ' ID equals the least of all the rows ID's
                    If CIntSafe(dr.Item(0), 0) < curID Then curID = CIntSafe(dr.Item(0), 0)

                    ' InsertDate is the earliest of all the rows inserts
                    curTimeStamp = CTimeStampSafe(dr.Item(6))
                    If curTimeStamp < curInsertDate Then curInsertDate = curTimeStamp

                    ' general data is aquired from the first row (the rest of the rows contain the same general data)
                    If Not generalDataAcquired Then

                        _Date = CDateSafe(dr.Item(2), Date.MinValue)
                        _OldDate = _Date
                        _Content = CStrSafe(dr.Item(5)).Trim
                        ' all rows have the same update date as all of them are always updated
                        _UpdateDate = CTimeStampSafe(dr.Item(7))
                        _Serial = CStrSafe(dr.Item(8)).Trim
                        _Number = CIntSafe(dr.Item(9), 0)
                        _PersonID = CIntSafe(dr.Item(10), 0)
                        _PersonName = CStrSafe(dr.Item(11)).Trim
                        _PersonCode = CStrSafe(dr.Item(12)).Trim
                        _PersonAddress = CStrSafe(dr.Item(13)).Trim
                        _PersonSodraCode = CStrSafe(dr.Item(14)).Trim

                        generalDataAcquired = True

                    End If

                    Select Case Utilities.ConvertDatabaseCharID(Of WorkerStatusType)(CStrSafe(dr.Item(1)))

                        Case WorkerStatusType.Employed

                            Throw New Exception(My.Resources.Workers_Contract_InvalidFetchResult)

                        Case WorkerStatusType.ExtraPay

                            _ExtraPayID = CIntSafe(dr.Item(0), 0)
                            _ExtraPay = CDblSafe(dr.Item(3), 0)
                            _ExtraPayChanged = True

                        Case WorkerStatusType.Fired

                            Throw New Exception(My.Resources.Workers_Contract_InvalidFetchResult)

                        Case WorkerStatusType.Holiday

                            _AnnualHolidayID = CIntSafe(dr.Item(0), 0)
                            _AnnualHoliday = CIntSafe(dr.Item(3), 0)
                            _AnnualHolidayChanged = True

                        Case WorkerStatusType.HolidayCorrection

                            _HolidayCorrectionID = CIntSafe(dr.Item(0), 0)
                            _HolidayCorrection = CDblSafe(dr.Item(3), ROUNDACCUMULATEDHOLIDAY, 0)
                            _HolidayCorrectionChanged = True

                        Case WorkerStatusType.NPD

                            _NpdID = CIntSafe(dr.Item(0), 0)
                            _NPD = CDblSafe(dr.Item(3), 2, 0)
                            _NpdChanged = True

                        Case WorkerStatusType.PNPD

                            _PnpdID = CIntSafe(dr.Item(0), 0)
                            _PNPD = CDblSafe(dr.Item(3), 2, 0)
                            _PnpdChanged = True

                        Case WorkerStatusType.Wage

                            _WageID = CIntSafe(dr.Item(0), 0)
                            _Wage = CDblSafe(dr.Item(3), 2, 0)
                            _WageType = Utilities.ConvertDatabaseCharID(Of WageType)(CStrSafe(dr.Item(4)))
                            _HumanReadableWageType = Utilities.ConvertLocalizedName(_WageType)
                            _WageChanged = True

                        Case WorkerStatusType.WorkLoad

                            _WorkLoadID = CIntSafe(dr.Item(0), 0)
                            _WorkLoad = CDblSafe(dr.Item(3), ROUNDWORKLOAD, 0)
                            _WorkLoadChanged = True

                        Case WorkerStatusType.Position

                            _PositionID = CIntSafe(dr.Item(0), 0)
                            _Position = CStrSafe(dr.Item(4))
                            _PositionChanged = True

                    End Select

                Next

                _InsertDate = curInsertDate
                _ID = curID

            End Using

            _ChronologicValidator = ContractChronologicValidator.GetContractChronologicValidator(_ID)

            MarkOld()

            ValidationRules.CheckRules()

        End Sub


        Protected Overrides Sub DataPortal_Insert()
            If Not CanAddObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecurityInsertDenied)
            DoSave()
        End Sub

        Protected Overrides Sub DataPortal_Update()
            If Not CanEditObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecurityUpdateDenied)
            DoSave()
        End Sub

        Private Sub DoSave()

            CheckIfContractSerialNumberUnique()

            If IsNew Then
                _ChronologicValidator = ContractChronologicValidator.NewContractChronologicValidator(_Serial, _Number)
            Else
                _ChronologicValidator = ContractChronologicValidator.GetContractChronologicValidator(_ID)
            End If

            Me.ValidationRules.CheckRules()
            If Not Me.IsValid Then
                Throw New Exception(String.Format(My.Resources.Common_ContainsErrors, vbCrLf, _
                    GetAllBrokenRules()))
            End If

            If Not IsNew Then CheckIfUpdateDateChanged()

            _UpdateDate = GetCurrentTimeStamp()
            If Me.IsNew Then _InsertDate = _UpdateDate

            Using transaction As New SqlTransaction

                Try

                    UpdateParam(WorkerStatusType.ExtraPay)
                    UpdateParam(WorkerStatusType.Holiday)
                    UpdateParam(WorkerStatusType.HolidayCorrection)
                    UpdateParam(WorkerStatusType.NPD)
                    UpdateParam(WorkerStatusType.PNPD)
                    UpdateParam(WorkerStatusType.Position)
                    UpdateParam(WorkerStatusType.Wage)
                    UpdateParam(WorkerStatusType.WorkLoad)

                    transaction.Commit()

                Catch ex As Exception
                    transaction.SetNonSqlException(ex)
                    Throw
                End Try

            End Using

            MarkOld()

        End Sub


        Protected Overrides Sub DataPortal_DeleteSelf()
            DataPortal_Delete(New Criteria(_ID))
        End Sub

        Protected Overrides Sub DataPortal_Delete(ByVal criteria As Object)

            If Not CanDeleteObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecurityUpdateDenied)

            _Serial = DirectCast(criteria, Criteria).ContractSerial
            _Number = DirectCast(criteria, Criteria).ContractNumber
            _OldDate = DirectCast(criteria, Criteria).ContractDate

            Dim myComm As SQLCommand

            If DirectCast(criteria, Criteria).ID > 0 Then

                myComm = New SQLCommand("FetchContractUpdateSerialNumber")
                myComm.AddParam("?CD", DirectCast(criteria, Criteria).ID)

                Using myData As DataTable = myComm.Fetch

                    If myData.Rows.Count < 1 Then Throw New Exception( _
                        String.Format(My.Resources.Common_ObjectNotFound, _
                            My.Resources.Workers_ContractUpdate_TypeName, _
                            DirectCast(criteria, Criteria).ID.ToString()))

                    _Serial = CStrSafe(myData.Rows(0).Item(0))
                    _Number = CIntSafe(myData.Rows(0).Item(1), 0)
                    _OldDate = CDateSafe(myData.Rows(0).Item(2), Date.MinValue)

                    If Not _Number > 0 OrElse _OldDate = Date.MinValue Then Throw New Exception( _
                        String.Format(My.Resources.Common_ObjectNotFound, _
                            My.Resources.Workers_ContractUpdate_TypeName, _
                            DirectCast(criteria, Criteria).ID.ToString()))

                End Using

            End If

            myComm = New SQLCommand("CheckIfContractUpdateCanBeDeleted")
            myComm.AddParam("?DN", _Number)
            myComm.AddParam("?DS", _Serial)
            myComm.AddParam("?DT", _OldDate)

            Using myData As DataTable = myComm.Fetch

                If Not myData.Rows.Count > 0 OrElse Not CIntSafe(myData.Rows(0).Item(0), 0) > 0 Then _
                    Throw New Exception(String.Format(My.Resources.Common_ObjectNotFound, _
                        My.Resources.Workers_ContractUpdate_TypeName, GetFullID(_Serial, _
                        _Number, _OldDate)))
                If CIntSafe(myData.Rows(0).Item(1), 0) > 0 Then Throw New Exception( _
                    My.Resources.Workers_Contract_InvalidFetchResult)
                If CDateSafe(myData.Rows(0).Item(2), Date.MinValue) <> Date.MinValue Then _
                    Throw New Exception(String.Format(My.Resources.Workers_ContractUpdate_BlockingSheet, _
                    CDateSafe(myData.Rows(0).Item(2), Date.MinValue).ToString("yyyy-MM-dd")))

            End Using

            myComm = New SQLCommand("DeleteContractUpdate")
            myComm.AddParam("?DS", _Serial.Trim)
            myComm.AddParam("?DN", _Number)
            myComm.AddParam("?DT", _OldDate)

            myComm.Execute()

            MarkNew()

        End Sub


        Private Sub UpdateParam(ByVal paramType As WorkerStatusType)

            ' do not update fields, that are out of scope
            If paramType = WorkerStatusType.Employed OrElse paramType = WorkerStatusType.Fired Then Exit Sub

            Dim paramID As Integer = GetParamIDByType(paramType)

            If IsNew OrElse Not paramID > 0 Then

                ' do not insert empty values and fields, that are limited by business logic
                If Not ParamValueHasStatusItem(paramType) OrElse IsParamValueReadonly(paramType) Then Exit Sub

                Dim myComm As New SQLCommand("InsertWorkerStatus")
                AddWithParams(myComm, paramType)
                myComm.AddParam("?PD", _PersonID)
                myComm.AddParam("?NM", _Number)
                myComm.AddParam("?SR", _Serial.Trim)
                myComm.AddParam("?TP", Utilities.ConvertDatabaseCharID(paramType))

                myComm.Execute()

                paramID = Convert.ToInt32(myComm.LastInsertID)
                SetParamIDByType(paramType, paramID)

            Else

                ' remove empty values
                If Not IsParamValueReadonly(paramType) AndAlso Not ParamValueHasStatusItem(paramType) Then

                    Dim myComm As New SQLCommand("DeleteWorkerStatus")
                    myComm.AddParam("?SD", paramID)

                    myComm.Execute()

                    SetParamIDByType(paramType, 0)

                Else

                    If IsParamValueReadonly(paramType) Then

                        Dim myComm As New SQLCommand("UpdateWorkerStatusWithoutValue")
                        AddWithParams(myComm, paramType)
                        myComm.AddParam("?SD", paramID)

                        myComm.Execute()

                    Else

                        Dim myComm As New SQLCommand("UpdateWorkerStatus")
                        AddWithParams(myComm, paramType)
                        myComm.AddParam("?SD", paramID)

                        myComm.Execute()

                    End If

                End If

            End If

        End Sub

        Private Sub AddWithParams(ByRef myComm As SQLCommand, ByVal paramType As WorkerStatusType)

            myComm.AddParam("?DT", _Date.Date)
            myComm.AddParam("?UD", _UpdateDate.ToUniversalTime)

            Select Case paramType
                Case WorkerStatusType.ExtraPay
                    myComm.AddParam("?VL", CRound(_ExtraPay, 2))
                    myComm.AddParam("?OP", "")
                Case WorkerStatusType.Holiday
                    myComm.AddParam("?VL", CRound(_AnnualHoliday, 2))
                    myComm.AddParam("?OP", "")
                Case WorkerStatusType.HolidayCorrection
                    myComm.AddParam("?VL", CRound(_HolidayCorrection, ROUNDACCUMULATEDHOLIDAY))
                    myComm.AddParam("?OP", "")
                Case WorkerStatusType.NPD
                    myComm.AddParam("?VL", CRound(_NPD, 2))
                    myComm.AddParam("?OP", "")
                Case WorkerStatusType.PNPD
                    myComm.AddParam("?VL", CRound(_PNPD, 2))
                    myComm.AddParam("?OP", "")
                Case WorkerStatusType.Wage
                    myComm.AddParam("?VL", CRound(_Wage, 2))
                    myComm.AddParam("?OP", Utilities.ConvertDatabaseCharID(_WageType))
                Case WorkerStatusType.WorkLoad
                    myComm.AddParam("?VL", CRound(_WorkLoad, ROUNDWORKLOAD))
                    myComm.AddParam("?OP", "")
                Case WorkerStatusType.Position
                    myComm.AddParam("?VL", 0, GetType(Double))
                    myComm.AddParam("?OP", _Position.Trim)
                Case Else
                    myComm.AddParam("?VL", 0, GetType(Double))
                    myComm.AddParam("?OP", "")
            End Select

            myComm.AddParam("?CN", _Content.Trim)

        End Sub

        Private Function GetParamIDByType(ByVal paramType As WorkerStatusType) As Integer
            Select Case paramType
                Case WorkerStatusType.Employed
                    Return 0
                Case WorkerStatusType.ExtraPay
                    Return _ExtraPayID
                Case WorkerStatusType.Fired
                    Return 0
                Case WorkerStatusType.Holiday
                    Return _AnnualHolidayID
                Case WorkerStatusType.HolidayCorrection
                    Return _HolidayCorrectionID
                Case WorkerStatusType.NPD
                    Return _NpdID
                Case WorkerStatusType.PNPD
                    Return _PnpdID
                Case WorkerStatusType.Position
                    Return _PositionID
                Case WorkerStatusType.Wage
                    Return _WageID
                Case WorkerStatusType.WorkLoad
                    Return _WorkLoadID
                Case Else
                    Return 0
            End Select
        End Function

        Private Sub SetParamIDByType(ByVal paramType As WorkerStatusType, ByVal paramID As Integer)
            Select Case paramType
                Case WorkerStatusType.Employed

                Case WorkerStatusType.ExtraPay
                    _ExtraPayID = paramID
                Case WorkerStatusType.Fired

                Case WorkerStatusType.Holiday
                    _AnnualHolidayID = paramID
                Case WorkerStatusType.HolidayCorrection
                    _HolidayCorrectionID = paramID
                Case WorkerStatusType.NPD
                    _NpdID = paramID
                Case WorkerStatusType.PNPD
                    _PnpdID = paramID
                Case WorkerStatusType.Position
                    _PositionID = paramID
                Case WorkerStatusType.Wage
                    _WageID = paramID
                Case WorkerStatusType.WorkLoad
                    _WorkLoadID = paramID
            End Select
        End Sub

        Private Function IsParamValueReadonly(ByVal paramType As WorkerStatusType) As Boolean
            Return Not IsNew AndAlso Not _ChronologicValidator.FinancialDataCanChange AndAlso _
                (paramType = WorkerStatusType.ExtraPay OrElse paramType = WorkerStatusType.NPD _
                OrElse paramType = WorkerStatusType.PNPD OrElse paramType = WorkerStatusType.Wage)
        End Function

        Private Function ParamValueHasStatusItem(ByVal paramType As WorkerStatusType) As Boolean

            If (paramType = WorkerStatusType.ExtraPay AndAlso Not _ExtraPayChanged) _
                OrElse (paramType = WorkerStatusType.Fired OrElse paramType = WorkerStatusType.Employed) _
                OrElse (paramType = WorkerStatusType.HolidayCorrection AndAlso _
                    CRound(_HolidayCorrection, ROUNDACCUMULATEDHOLIDAY) = 0) _
                OrElse (paramType = WorkerStatusType.HolidayCorrection AndAlso Not _HolidayCorrectionChanged) _
                OrElse (paramType = WorkerStatusType.NPD AndAlso Not _NpdChanged) _
                OrElse (paramType = WorkerStatusType.PNPD AndAlso Not _PnpdChanged) _
                OrElse (paramType = WorkerStatusType.Position AndAlso Not _PositionChanged) _
                OrElse (paramType = WorkerStatusType.Holiday AndAlso Not _AnnualHolidayChanged) _
                OrElse (paramType = WorkerStatusType.Wage AndAlso Not _WageChanged) _
                OrElse (paramType = WorkerStatusType.WorkLoad AndAlso Not _WorkLoadChanged) Then _
                Return False

            Return True

        End Function

        Private Sub CheckIfContractSerialNumberUnique()

            If Not IsNew AndAlso _Date.Date = _OldDate.Date Then Exit Sub

            Dim myComm As New SQLCommand("CheckIfContractUpdateSerialNumberUnique")
            myComm.AddParam("?DS", _Serial.Trim)
            myComm.AddParam("?DN", _Number)
            myComm.AddParam("?DT", _Date.Date)
            If IsNew Then
                myComm.AddParam("?OD", Today.AddYears(100))
            Else
                myComm.AddParam("?OD", _OldDate.Date)
            End If

            Using myData As DataTable = myComm.Fetch
                If myData.Rows.Count > 0 AndAlso CIntSafe(myData.Rows(0).Item(0), 0) > 0 Then _
                    Throw New Exception(My.Resources.Workers_ContractUpdate_SerialNumberAlreadyExists)
            End Using

        End Sub

        Private Sub CheckIfUpdateDateChanged()

            Dim myComm As New SQLCommand("CheckIfContractUpdateUpdateDateChanged")
            myComm.AddParam("?CD", _ID)

            Using myData As DataTable = myComm.Fetch

                If myData.Rows.Count < 1 OrElse CDateTimeSafe(myData.Rows(0).Item(0), _
                    Date.MinValue) = Date.MinValue Then

                    Throw New Exception(String.Format(My.Resources.Common_ObjectNotFound, _
                        My.Resources.Workers_ContractUpdate_TypeName, _ID.ToString))

                End If

                If CTimeStampSafe(myData.Rows(0).Item(0)) <> _UpdateDate Then

                    Throw New Exception(My.Resources.Common_UpdateDateHasChanged)

                End If

            End Using

        End Sub

        Private Function GetFullID(ByVal contractSerial As String, ByVal contractNumber As Integer, _
            ByVal nDate As Date) As String
            Return String.Format("{0}{1}:{2}", contractSerial, contractNumber.ToString(), _
                nDate.ToString("yyyy-MM-dd"))
        End Function

#End Region

    End Class

End Namespace