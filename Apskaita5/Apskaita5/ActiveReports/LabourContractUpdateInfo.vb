Namespace ActiveReports

    ''' <summary>
    ''' Describes parameters of a <see cref="Workers.Contract">labour contract's update</see>.
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public Class LabourContractUpdateInfo
        Inherits ReadOnlyBase(Of LabourContractUpdateInfo)

#Region " Business Methods "

        Private _ID As Integer = 0
        Private _InsertDate As Date = Today
        Private _UpdateDate As Date = Today
        Private _Date As Date = Today
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
        ''' Gets the date and time when the contract update was inserted into the database.
        ''' </summary>
        ''' <remarks>Value is stored in in the database field darbuotojai_d.InsertDate
        ''' (for each status row).</remarks>
        Public ReadOnly Property InsertDate() As Date
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
        Public ReadOnly Property UpdateDate() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _UpdateDate
            End Get
        End Property

        ''' <summary>
        ''' Gets the date of the contract update.
        ''' </summary>
        ''' <remarks>Value is stored in in the database field darbuotojai_d.Nuo
        ''' (for each status row).</remarks>
        Public ReadOnly Property [Date]() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Date
            End Get
        End Property

        ''' <summary>
        ''' Gets the description of the contract update.
        ''' </summary>
        ''' <remarks>Value is stored in in the database field darbuotojai_d.Pagrindas
        ''' (for each status row)</remarks>
        Public ReadOnly Property Content() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Content.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets the updated position of the worker.
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
        ''' Gets the updated wage.
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
        ''' Gets the updated <see cref="Workers.WageType">wage type</see>.
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
        ''' Gets the updated <see cref="Workers.WageType">wage type</see> as a human readable string.
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
        ''' Gets the updated extra pay.
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
        ''' Gets the updated annual holiday rate (holiday days per work year).
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
        ''' e.g. when holiday is not calculated for some time (when raising children etc.).
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
        ''' Gets the updated applicable NPD (non-taxable personal income size).
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
        ''' Gets the updated applicable PNPD (supplementary non-taxable personal income size).
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
        ''' Gets the updated work load (ratio between contractual work hours and gauge work hours (40H/Week)).
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



        Protected Overrides Function GetIdValue() As Object
            Return _ID
        End Function

        Public Overrides Function ToString() As String
            Return My.Resources.Workers_ContractUpdate_TypeName
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function GetLabourContractUpdateInfo(ByVal dr As DataRow) As LabourContractUpdateInfo
            Return New LabourContractUpdateInfo(dr)
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

            _ID = CIntSafe(dr.Item(3), 0)
            _Date = CDateSafe(dr.Item(4), Today)
            _Content = CStrSafe(dr.Item(5)).Trim
            _InsertDate = CDateTimeSafe(dr.Item(6), Today)
            _UpdateDate = CDateTimeSafe(dr.Item(7), Today)

            If Not IsDBNull(dr.Item(9)) Then _ExtraPay = DblParser(CDblSafe(dr.Item(9), 2, 0), 2)
            If Not IsDBNull(dr.Item(10)) Then _AnnualHoliday = CIntSafe(dr.Item(10), 0).ToString
            If Not IsDBNull(dr.Item(11)) Then _HolidayCorrection = DblParser(CDblSafe(dr.Item(11), ROUNDACCUMULATEDHOLIDAY, 0), ROUNDACCUMULATEDHOLIDAY)
            If Not IsDBNull(dr.Item(12)) Then _NPD = DblParser(CDblSafe(dr.Item(12), 2, 0), 2)
            If Not IsDBNull(dr.Item(13)) Then _PNPD = DblParser(CDblSafe(dr.Item(13), 2, 0), 2)
            If Not IsDBNull(dr.Item(14)) Then _Wage = DblParser(CDblSafe(dr.Item(14), 2, 0), 2)
            If Not IsDBNull(dr.Item(15)) Then
                _WageType = EnumValueAttribute.ConvertDatabaseCharID(Of Workers.WageType) _
                    (CStrSafe(dr.Item(15)).Trim)
                _WageTypeHumanReadable = EnumValueAttribute.ConvertLocalizedName(_WageType)
            End If
            If Not IsDBNull(dr.Item(16)) Then _WorkLoad = DblParser(CDblSafe(dr.Item(16), ROUNDWORKLOAD, 0), ROUNDWORKLOAD)
            _Position = CStrSafe(dr.Item(17)).Trim

        End Sub

#End Region

    End Class

End Namespace