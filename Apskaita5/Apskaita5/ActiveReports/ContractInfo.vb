Namespace ActiveReports

    <Serializable()> _
    Public Class ContractInfo
        Inherits ReadOnlyBase(Of ContractInfo)

#Region " Business Methods "

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


        Public ReadOnly Property ID() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If Not _ID > 0 Then Return ""
                Return _ID.ToString
            End Get
        End Property

        Public ReadOnly Property InsertDate() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If _InsertDate = Date.MaxValue Then Return ""
                Return _InsertDate.ToString("yyyy-MM-dd HH:mm:ss")
            End Get
        End Property

        Public ReadOnly Property UpdateDate() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If _UpdateDate = Date.MaxValue Then Return ""
                Return _UpdateDate.ToString("yyyy-MM-dd HH:mm:ss")
            End Get
        End Property

        Public ReadOnly Property PersonID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _PersonID
            End Get
        End Property

        Public ReadOnly Property PersonName() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _PersonName.Trim
            End Get
        End Property

        Public ReadOnly Property PersonCode() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _PersonCode.Trim
            End Get
        End Property

        Public ReadOnly Property PersonSodraCode() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _PersonSodraCode.Trim
            End Get
        End Property

        Public ReadOnly Property PersonAddress() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _PersonAddress.Trim
            End Get
        End Property

        Public ReadOnly Property Serial() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Serial.Trim
            End Get
        End Property

        Public ReadOnly Property Number() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If Not _Number > 0 Then Return ""
                Return _Number.ToString
            End Get
        End Property

        Public ReadOnly Property [Date]() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If _Date = Date.MaxValue Then Return ""
                Return _Date.ToString("yyyy-MM-dd")
            End Get
        End Property

        Public ReadOnly Property DateTermination() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If _DateTermination = Date.MaxValue Then Return ""
                Return _DateTermination.ToString("yyyy-MM-dd")
            End Get
        End Property

        Public ReadOnly Property Content() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Content.Trim
            End Get
        End Property

        Public ReadOnly Property Position() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Position.Trim
            End Get
        End Property

        Public ReadOnly Property Wage() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Wage.Trim
            End Get
        End Property

        Public ReadOnly Property WageType() As Workers.WageType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _WageType
            End Get
        End Property

        Public ReadOnly Property WageTypeHumanReadable() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _WageTypeHumanReadable.Trim
            End Get
        End Property

        Public ReadOnly Property ExtraPay() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ExtraPay.Trim
            End Get
        End Property

        Public ReadOnly Property AnnualHoliday() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AnnualHoliday.Trim
            End Get
        End Property

        Public ReadOnly Property HolidayCorrection() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _HolidayCorrection.Trim
            End Get
        End Property

        Public ReadOnly Property NPD() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _NPD.Trim
            End Get
        End Property

        Public ReadOnly Property PNPD() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _PNPD.Trim
            End Get
        End Property

        Public ReadOnly Property WorkLoad() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _WorkLoad.Trim
            End Get
        End Property

        Public ReadOnly Property UpdatesList() As Csla.SortedBindingList(Of LabourContractUpdateInfo)
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If _UpdatesList Is Nothing Then Return Nothing
                Return _UpdatesList.GetSortedList
            End Get
        End Property


        Protected Overrides Function GetIdValue() As Object
            Return _ID
        End Function

        Public Overrides Function ToString() As String
            If Not _ID > 0 Then Return ""
            Return "Darbo sutartis " & _Date.ToShortDateString & " Nr. " & _Serial & _Number.ToString
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
            If Not IsDBNull(dr.Item(13)) Then _ExtraPay = CDblSafe(dr.Item(13), 2, 0).ToString("#,##0.00")
            If Not IsDBNull(dr.Item(14)) Then _AnnualHoliday = CIntSafe(dr.Item(14), 0).ToString
            If Not IsDBNull(dr.Item(15)) Then _HolidayCorrection = CDblSafe(dr.Item(15), 4, 0).ToString("#,##0.0000")
            If Not IsDBNull(dr.Item(16)) Then _NPD = CDblSafe(dr.Item(16), 2, 0).ToString("#,##0.00")
            If Not IsDBNull(dr.Item(17)) Then _PNPD = CDblSafe(dr.Item(17), 2, 0).ToString("#,##0.00")
            If Not IsDBNull(dr.Item(18)) Then _Wage = CDblSafe(dr.Item(18), 2, 0).ToString("#,##0.00")
            If Not IsDBNull(dr.Item(19)) Then _WageType = ConvertEnumDatabaseStringCode(Of Workers.WageType) _
                (CStrSafe(dr.Item(19)).Trim)
            If Not IsDBNull(dr.Item(19)) Then _WageTypeHumanReadable = ConvertEnumHumanReadable(_WageType)
            If Not IsDBNull(dr.Item(20)) Then _WorkLoad = CDblSafe(dr.Item(20), 4, 0).ToString("#,##0.0000")
            _Position = CStrSafe(dr.Item(21)).Trim
            
            _UpdatesList = LabourContractUpdateInfoList.GetLabourContractUpdateInfoList( _
                myData, _Serial, _Number)

        End Sub

#End Region

    End Class

End Namespace