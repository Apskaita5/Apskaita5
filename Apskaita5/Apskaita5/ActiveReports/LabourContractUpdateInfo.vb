Namespace ActiveReports

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


        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        Public ReadOnly Property InsertDate() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _InsertDate
            End Get
        End Property

        Public ReadOnly Property UpdateDate() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _UpdateDate
            End Get
        End Property

        Public ReadOnly Property [Date]() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Date
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



        Protected Overrides Function GetIdValue() As Object
            Return _ID
        End Function

        Public Overrides Function ToString() As String
            Return "Darbo sutarties pakeitimas"
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

            If Not IsDBNull(dr.Item(9)) Then _ExtraPay = CDblSafe(dr.Item(9), 2, 0).ToString("#,##0.00")
            If Not IsDBNull(dr.Item(10)) Then _AnnualHoliday = CIntSafe(dr.Item(10), 0).ToString
            If Not IsDBNull(dr.Item(11)) Then _HolidayCorrection = CDblSafe(dr.Item(11), 4, 0).ToString("#,##0.0000")
            If Not IsDBNull(dr.Item(12)) Then _NPD = CDblSafe(dr.Item(12), 2, 0).ToString("#,##0.00")
            If Not IsDBNull(dr.Item(13)) Then _PNPD = CDblSafe(dr.Item(13), 2, 0).ToString("#,##0.00")
            If Not IsDBNull(dr.Item(14)) Then _Wage = CDblSafe(dr.Item(14), 2, 0).ToString("#,##0.00")
            If Not IsDBNull(dr.Item(15)) Then _WageType = ConvertEnumDatabaseStringCode(Of Workers.WageType) _
                (CStrSafe(dr.Item(15)).Trim)
            If Not IsDBNull(dr.Item(15)) Then _WageTypeHumanReadable = ConvertEnumHumanReadable(_WageType)
            If Not IsDBNull(dr.Item(16)) Then _WorkLoad = CDblSafe(dr.Item(16), 4, 0).ToString("#,##0.0000")
            _Position = CStrSafe(dr.Item(17)).Trim

        End Sub

#End Region

    End Class

End Namespace