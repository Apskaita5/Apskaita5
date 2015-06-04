Namespace ActiveReports

    ''' <summary>
    ''' Represents a wage info data item for calculating VDU (average wage).
    ''' </summary>
    ''' <remarks>Should only be used as a child of WageVDUInfoList.</remarks>
    <Serializable()> _
    Public Class WageVDUInfo
        Inherits ReadOnlyBase(Of WageVDUInfo)

#Region " Business Methods "

        Private ReadOnly _Guid As Guid = Guid.NewGuid()
        Private _Year As Integer = 0
        Private _Month As Integer = 0
        Private _WorkDays As Integer = 0
        Private _WorkHours As Double = 0
        Private _ScheduledDays As Integer = 0
        Private _ScheduledHours As Double = 0
        Private _Wage As Double = 0


        ''' <summary>
        ''' Year which the wage is calculated for.
        ''' </summary>
        ''' <remarks></remarks>
        <IntegerField(ValueRequiredLevel.Optional, False)> _
        Public ReadOnly Property Year() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Year
            End Get
        End Property

        ''' <summary>
        ''' Month which the wage is calculated for.
        ''' </summary>
        ''' <remarks></remarks>
        <IntegerField(ValueRequiredLevel.Optional, False)> _
        Public ReadOnly Property Month() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Month
            End Get
        End Property

        ''' <summary>
        ''' Number of (actual) work days within the month as applicable for the VDU calculation.
        ''' </summary>
        ''' <remarks></remarks>
        <IntegerField(ValueRequiredLevel.Optional, False)> _
        Public ReadOnly Property WorkDays() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _WorkDays
            End Get
        End Property

        ''' <summary>
        ''' Number of (actual) work hours within the month as applicable for the VDU calculation.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, ROUNDWORKHOURS)> _
        Public ReadOnly Property WorkHours() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_WorkHours, ROUNDWORKHOURS)
            End Get
        End Property

        ''' <summary>
        ''' Number of scheduled work days within the month.
        ''' </summary>
        ''' <remarks></remarks>
        <IntegerField(ValueRequiredLevel.Optional, False)> _
        Public ReadOnly Property ScheduledDays() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ScheduledDays
            End Get
        End Property

        ''' <summary>
        ''' Number of scheduled work hours within the month.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, ROUNDWORKHOURS)> _
        Public ReadOnly Property ScheduledHours() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ScheduledHours, ROUNDWORKHOURS)
            End Get
        End Property

        ''' <summary>
        ''' Total calculated wage for the month as applicable for the VDU calculation.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property Wage() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_Wage)
            End Get
        End Property



        Protected Overrides Function GetIdValue() As Object
            Return _Guid
        End Function

        Public Overrides Function ToString() As String
            Return String.Format(My.Resources.ActiveReports_WageVDUInfo_ToString, _Year.ToString, _
                _Month.ToString, _WorkDays.ToString, DblParser(_WorkHours, ROUNDWORKHOURS), _
                DblParser(_Wage), GetCurrentCompany().BaseCurrency)
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function GetWageVDUInfo(ByVal dr As DataRow) As WageVDUInfo
            Return New WageVDUInfo(dr)
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

            _Year = CIntSafe(dr.Item(0), 0)
            _Month = CIntSafe(dr.Item(1), 0)
            _WorkDays = CIntSafe(dr.Item(2), 0)
            _WorkHours = CDblSafe(dr.Item(3), ROUNDWORKHOURS, 0)
            _Wage = CDblSafe(dr.Item(4), 2, 0)
            _ScheduledHours = CDblSafe(dr.Item(5), ROUNDWORKHOURS, 0)
            _ScheduledDays = CIntSafe(dr.Item(6), 0)

        End Sub

#End Region

    End Class

End Namespace