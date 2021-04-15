Namespace ActiveReports

    ''' <summary>
    ''' Represents an item of a labour contract report. Describes parameters of a <see cref="Workers.Contract">labour contract</see>.
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public NotInheritable Class ContractInfo
        Inherits ReadOnlyBase(Of ContractInfo)

#Region " Business Methods "

        Private ReadOnly _Guid As Guid = Guid.NewGuid()
        Private _ID As Integer = 0
        Private _InsertDate As Date = Date.MaxValue
        Private _UpdateDate As Date = Date.MaxValue
        Private _PersonID As Integer = 0
        Private _PersonName As String = ""
        Private _PersonCode As String = ""
        Private _PersonSodraCode As String = ""
        Private _PersonAddress As String = ""
        Private _Serial As String = ""
        Private _Number As Integer = 0
        Private _Date As Date = Date.MaxValue
        Private _DateTermination As Date = Date.MaxValue
        Private _Content As String = ""
        Private _Position As String = ""
        Private _Wage As String = ""
        Private _WageType As Workers.WageType = Workers.WageType.Position
        Private _WageTypeHumanReadable As String = ""
        Private _ExtraPay As String = ""
        Private _AnnualHoliday As String = ""
        Private _HolidayCorrection As String = ""
        Private _NPD As String = ""
        Private _PNPD As String = ""
        Private _WorkLoad As String = ""
        Private WithEvents _UpdatesList As LabourContractUpdateInfoList = Nothing


        ''' <summary>
        ''' Gets an ID of the contract that is assigned by a database (AUTOINCREMENT).
        ''' </summary>
        ''' <remarks>Value is stored in in the database field darbuotojai_d.ID
        ''' for status row of type <see cref="Workers.WorkerStatusType.Employed">WorkerStatusType.Employed</see>.</remarks>
        Public ReadOnly Property ID() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If Not _ID > 0 Then Return ""
                Return _ID.ToString
            End Get
        End Property

        ''' <summary>
        ''' Gets the date and time when the contract was inserted into the database.
        ''' </summary>
        ''' <remarks>Value is stored in in the database field darbuotojai_d.InsertDate
        ''' (for each status row).</remarks>
        Public ReadOnly Property InsertDate() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If _InsertDate = Date.MaxValue Then Return ""
                Return _InsertDate.ToString("yyyy-MM-dd HH:mm:ss")
            End Get
        End Property

        ''' <summary>
        ''' Gets the date and time when the contract was last updated.
        ''' </summary>
        ''' <remarks>Value is stored in in the database field darbuotojai_d.UpdateDate
        ''' (for each status row).</remarks>
        Public ReadOnly Property UpdateDate() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If _UpdateDate = Date.MaxValue Then Return ""
                Return _UpdateDate.ToString("yyyy-MM-dd HH:mm:ss")
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
        Public ReadOnly Property Number() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If Not _Number > 0 Then Return ""
                Return _Number.ToString
            End Get
        End Property

        ''' <summary>
        ''' Gets the date of the contract.
        ''' </summary>
        ''' <remarks>An empty string is used to distinguish null value (no labour contract).
        ''' Value is stored in in the database field darbuotojai_d.Nuo
        ''' (for each status row).</remarks>
        Public ReadOnly Property [Date]() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If _Date = Date.MaxValue Then Return ""
                Return _Date.ToString("yyyy-MM-dd")
            End Get
        End Property

        ''' <summary>
        ''' Gets the contract termination date. Returns an empty string if the contract is not terminated.
        ''' </summary>
        ''' <remarks>Value is stored in in the database field darbuotojai_d.Nuo
        ''' for the status row of type <see cref="Workers.WorkerStatusType.Fired">WorkerStatusType.Fired</see>.</remarks>
        Public ReadOnly Property DateTermination() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If _DateTermination = Date.MaxValue Then Return ""
                Return _DateTermination.ToString("yyyy-MM-dd")
            End Get
        End Property

        ''' <summary>
        ''' Gets the description of the contract.
        ''' </summary>
        ''' <remarks>Value is stored in in the database field darbuotojai_d.Pagrindas
        ''' for the status row of type <see cref="Workers.WorkerStatusType.Employed">WorkerStatusType.Employed</see>.</remarks>
        Public ReadOnly Property Content() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Content.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets the position of the worker.
        ''' </summary>
        ''' <remarks>Value is stored in in the database field darbuotojai_d.DU_tipas
        ''' for the status row of type <see cref="Workers.WorkerStatusType.Position">WorkerStatusType.Position</see>.</remarks>
        Public ReadOnly Property Position() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Position.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets the wage.
        ''' </summary>
        ''' <remarks>An empty string is used to distinguish null value (parameter not set).
        ''' Value is stored in in the database field darbuotojai_d.Dydis
        ''' for the status row of type <see cref="Workers.WorkerStatusType.Wage">WorkerStatusType.Wage</see>.</remarks>
        Public ReadOnly Property Wage() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Wage.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets the <see cref="Workers.WageType">wage type</see>.
        ''' </summary>
        ''' <remarks>Value is stored in in the database field darbuotojai_d.DU_tipas
        ''' for the status row of type <see cref="Workers.WorkerStatusType.Wage">WorkerStatusType.Wage</see>.</remarks>
        Public ReadOnly Property WageType() As Workers.WageType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _WageType
            End Get
        End Property

        ''' <summary>
        ''' Gets the <see cref="Workers.WageType">wage type</see> as a human readable string.
        ''' </summary>
        ''' <remarks>Value is stored in in the database field darbuotojai_d.DU_tipas
        ''' for the status row of type <see cref="Workers.WorkerStatusType.Wage">WorkerStatusType.Wage</see>.</remarks>
        Public ReadOnly Property WageTypeHumanReadable() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _WageTypeHumanReadable.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets the extra pay.
        ''' </summary>
        ''' <remarks>An empty string is used to distinguish null value (parameter not set).
        ''' Value is stored in in the database field darbuotojai_d.Dydis
        ''' for the status row of type <see cref="Workers.WorkerStatusType.ExtraPay">WorkerStatusType.ExtraPay</see>.</remarks>
        Public ReadOnly Property ExtraPay() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ExtraPay.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets the annual holiday rate (holiday days per work year).
        ''' </summary>
        ''' <remarks>An empty string is used to distinguish null value (parameter not set).
        ''' Value is stored in in the database field darbuotojai_d.Dydis
        ''' for the status row of type <see cref="Workers.WorkerStatusType.Holiday">WorkerStatusType.Holiday</see>.</remarks>
        Public ReadOnly Property AnnualHoliday() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AnnualHoliday.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets the annual holiday technical correction, 
        ''' e.g. when transfering the data for an old labour contract.
        ''' </summary>
        ''' <remarks>An empty string is used to distinguish null value (parameter not set).
        ''' Value is stored in in the database field darbuotojai_d.Dydis
        ''' for the status row of type <see cref="Workers.WorkerStatusType.HolidayCorrection">WorkerStatusType.HolidayCorrection</see>.</remarks>
        Public ReadOnly Property HolidayCorrection() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _HolidayCorrection.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets the applicable NPD (non-taxable personal income size).
        ''' </summary>
        ''' <remarks>An empty string is used to distinguish null value (parameter not set).
        ''' Value is stored in in the database field darbuotojai_d.Dydis
        ''' for the status row of type <see cref="Workers.WorkerStatusType.NPD">WorkerStatusType.NPD</see>.</remarks>
        Public ReadOnly Property NPD() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _NPD.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets the applicable PNPD (supplementary non-taxable personal income size).
        ''' </summary>
        ''' <remarks>An empty string is used to distinguish null value (parameter not set).
        ''' Value is stored in in the database field darbuotojai_d.Dydis
        ''' for the status row of type <see cref="Workers.WorkerStatusType.PNPD">WorkerStatusType.PNPD</see>.</remarks>
        Public ReadOnly Property PNPD() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _PNPD.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets the work load (ratio between contractual work hours and gauge work hours (40H/Week)).
        ''' </summary>
        ''' <remarks>An empty string is used to distinguish null value (parameter not set).
        ''' Value is stored in in the database field darbuotojai_d.Dydis
        ''' for the status row of type <see cref="Workers.WorkerStatusType.WorkLoad">WorkerStatusType.WorkLoad</see>.</remarks>
        Public ReadOnly Property WorkLoad() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _WorkLoad.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a list of <see cref="Workers.ContractUpdate">updates (amendments) of the contract</see>.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property UpdatesList() As LabourContractUpdateInfoList
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _UpdatesList
            End Get
        End Property

        ''' <summary>
        ''' Gets a sortable list of <see cref="Workers.ContractUpdate">updates (amendments) of the contract</see>.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property UpdatesListSorted() As Csla.SortedBindingList(Of LabourContractUpdateInfo)
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If _UpdatesList Is Nothing Then Return Nothing
                Return _UpdatesList.GetSortedList
            End Get
        End Property


        Protected Overrides Function GetIdValue() As Object
            Return _Guid
        End Function

        Public Overrides Function ToString() As String
            Return String.Format(My.Resources.Workers_Contract_ToString, _
                _Date.ToString("yyyy-MM-dd"), _Serial, _Number.ToString)
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function GetContractInfo(ByVal dr As DataRow, ByVal myData As DataTable) As ContractInfo
            Return New ContractInfo(dr, myData)
        End Function

        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal dr As DataRow, ByVal myData As DataTable)
            Fetch(dr, myData)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal dr As DataRow, ByVal myData As DataTable)

            _PersonID = CIntSafe(dr.Item(0), 0)
            _PersonName = CStrSafe(dr.Item(1)).Trim
            _PersonCode = CStrSafe(dr.Item(2)).Trim
            _PersonSodraCode = CStrSafe(dr.Item(3)).Trim
            _PersonAddress = CStrSafe(dr.Item(4)).Trim
            _Serial = CStrSafe(dr.Item(5)).Trim
            _Number = CIntSafe(dr.Item(6), 0)
            _ID = CIntSafe(dr.Item(7), 0)
            _Date = CDateSafe(dr.Item(8), Date.MaxValue)
            _Content = CStrSafe(dr.Item(9)).Trim
            _InsertDate = CDateTimeSafe(dr.Item(10), Date.MaxValue)
            _UpdateDate = CDateTimeSafe(dr.Item(11), Date.MaxValue)
            _DateTermination = CDateSafe(dr.Item(12), Date.MaxValue)
            If Not IsDBNull(dr.Item(13)) Then _ExtraPay = DblParser(CDblSafe(dr.Item(13), 2, 0), 2)
            If Not IsDBNull(dr.Item(14)) Then _AnnualHoliday = CIntSafe(dr.Item(14), 0).ToString
            If Not IsDBNull(dr.Item(15)) Then _HolidayCorrection = DblParser(CDblSafe(dr.Item(15), ROUNDACCUMULATEDHOLIDAY, 0), ROUNDACCUMULATEDHOLIDAY)
            If Not IsDBNull(dr.Item(16)) Then _NPD = DblParser(CDblSafe(dr.Item(16), 2, 0), 2)
            If Not IsDBNull(dr.Item(17)) Then _PNPD = DblParser(CDblSafe(dr.Item(17), 2, 0), 2)
            If Not IsDBNull(dr.Item(18)) Then _Wage = DblParser(CDblSafe(dr.Item(18), 2, 0), 2)
            If Not IsDBNull(dr.Item(19)) Then
                _WageType = Utilities.ConvertDatabaseCharID(Of Workers.WageType) _
                    (CStrSafe(dr.Item(19)).Trim)
                _WageTypeHumanReadable = Utilities.ConvertLocalizedName(_WageType)
            End If
            If Not IsDBNull(dr.Item(20)) Then _WorkLoad = DblParser(CDblSafe(dr.Item(20), ROUNDWORKLOAD, 0), ROUNDWORKLOAD)
            _Position = CStrSafe(dr.Item(21)).Trim

            _UpdatesList = LabourContractUpdateInfoList.GetLabourContractUpdateInfoList( _
                myData, _Serial, _Number)

        End Sub

#End Region

    End Class

End Namespace
