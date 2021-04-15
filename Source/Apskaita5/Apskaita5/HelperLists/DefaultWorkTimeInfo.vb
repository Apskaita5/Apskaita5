Imports ApskaitaObjects.Settings.XmlProxies

Namespace HelperLists

    ''' <summary>
    ''' Represents a <see cref="ApskaitaObjects.Settings.DefaultWorkTime">Settings.DefaultWorkTime</see> 
    ''' value object, i.e. information about gauge work time amounts for a certain year
    ''' and a certain month.
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public NotInheritable Class DefaultWorkTimeInfo
        Inherits ReadOnlyBase(Of DefaultWorkTimeInfo)

#Region " Business Methods "

        Private ReadOnly _Guid As Guid = Guid.NewGuid()
        Private _Year As Integer = 0
        Private _Month As Integer = 0
        Private _WorkDaysFor5WorkDayWeek As Integer = 0
        Private _WorkHoursFor5WorkDayWeek As Double = 0
        Private _WorkDaysFor6WorkDayWeek As Integer = 0
        Private _WorkHoursFor6WorkDayWeek As Double = 0


        ''' <summary>
        ''' Gets a year of the data.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Year() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Year
            End Get
        End Property

        ''' <summary>
        ''' Gets a month of the data.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Month() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Month
            End Get
        End Property

        ''' <summary>
        ''' Gets a gauge amount of work days in month as applicable for 5 days work week.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property WorkDaysFor5WorkDayWeek() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _WorkDaysFor5WorkDayWeek
            End Get
        End Property

        ''' <summary>
        ''' Gets a gauge amount of work hours in month as applicable for 5 days work week.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property WorkHoursFor5WorkDayWeek() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_WorkHoursFor5WorkDayWeek, ROUNDWORKHOURS)
            End Get
        End Property

        ''' <summary>
        ''' Gets a gauge amount of work days in month as applicable for 6 days work week.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property WorkDaysFor6WorkDayWeek() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _WorkDaysFor6WorkDayWeek
            End Get
        End Property

        ''' <summary>
        ''' Gets a gauge amount of work hours in month as applicable for 6 days work week.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property WorkHoursFor6WorkDayWeek() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_WorkHoursFor6WorkDayWeek, ROUNDWORKHOURS)
            End Get
        End Property



        Protected Overrides Function GetIdValue() As Object
            Return _Guid
        End Function

        Public Overrides Function ToString() As String
            Return String.Format(My.Resources.HelperLists_DefaultWorkTimeInfo_ToString, _
                _Year.ToString, _Month.ToString, _WorkDaysFor5WorkDayWeek.ToString, _
                DblParser(_WorkHoursFor5WorkDayWeek, ROUNDWORKHOURS), _
                _WorkDaysFor6WorkDayWeek.ToString, _
                DblParser(_WorkHoursFor6WorkDayWeek, ROUNDWORKHOURS))
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Attempts to calculate gauge work time for given year and month.
        ''' </summary>
        ''' <param name="year">a year to calculate gauge work time for</param>
        ''' <param name="month">a month to calculate gauge work time for</param>
        ''' <param name="publicHolidayList">a list of public holiday dates</param>
        ''' <remarks></remarks>
        Friend Shared Function NewDefaultWorkTimeInfo(ByVal year As Integer, _
            ByVal month As Integer, ByVal publicHolidayList As List(Of Date)) As DefaultWorkTimeInfo
            Return New DefaultWorkTimeInfo(year, month, publicHolidayList)
        End Function

        Friend Shared Function GetDefaultWorkTimeInfo(ByVal proxy As DefaultWorkTimeProxy) As DefaultWorkTimeInfo
            Return New DefaultWorkTimeInfo(proxy)
        End Function


        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal year As Integer, ByVal month As Integer, _
            ByVal publicHolidayList As List(Of Date))
            Create(year, month, publicHolidayList)
        End Sub

        Private Sub New(ByVal proxy As DefaultWorkTimeProxy)
            Fetch(proxy)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Create(ByVal year As Integer, ByVal month As Integer, _
            ByVal publicHolidayList As List(Of Date))

            If year > 2100 OrElse year < 1972 Then
                Throw New Exception(String.Format(My.Resources.HelperLists_DefaultWorkTimeInfo_InvalidYear, _
                    year.ToString))
            End If

            If month > 12 OrElse month < 1 Then
                Throw New Exception(String.Format(My.Resources.HelperLists_DefaultWorkTimeInfo_InvalidMonth, _
                    month.ToString))
            End If

            _Year = year
            _Month = month
            Dim daysInMonth As Integer = Date.DaysInMonth(year, month)

            Dim current As Date

            For i As Integer = 1 To daysInMonth

                current = New Date(year, month, i)

                If current.DayOfWeek <> DayOfWeek.Sunday AndAlso (publicHolidayList Is Nothing _
                    OrElse Not publicHolidayList.Contains(current.Date)) Then

                    If current.DayOfWeek = DayOfWeek.Saturday Then

                        _WorkDaysFor6WorkDayWeek += 1
                        _WorkHoursFor6WorkDayWeek = CRound(_WorkHoursFor6WorkDayWeek + 6.6666, ROUNDWORKHOURS)

                        If Not publicHolidayList Is Nothing AndAlso i <> daysInMonth _
                            AndAlso publicHolidayList.Contains(current.AddDays(1).Date) Then
                            _WorkHoursFor6WorkDayWeek = CRound(_WorkHoursFor6WorkDayWeek - 1, ROUNDWORKHOURS)
                        End If

                    Else

                        _WorkDaysFor6WorkDayWeek += 1
                        _WorkHoursFor6WorkDayWeek = CRound(_WorkHoursFor6WorkDayWeek + 6.6666, ROUNDWORKHOURS)
                        _WorkDaysFor5WorkDayWeek += 1
                        _WorkHoursFor5WorkDayWeek = CRound(_WorkHoursFor5WorkDayWeek + 8.0, ROUNDWORKHOURS)

                        If Not publicHolidayList Is Nothing AndAlso i <> daysInMonth _
                            AndAlso publicHolidayList.Contains(current.AddDays(1).Date) Then
                            _WorkHoursFor6WorkDayWeek = CRound(_WorkHoursFor6WorkDayWeek - 1, ROUNDWORKHOURS)
                            _WorkHoursFor5WorkDayWeek = CRound(_WorkHoursFor5WorkDayWeek - 1, ROUNDWORKHOURS)
                        End If

                    End If

                End If

            Next

        End Sub

        Private Sub Fetch(ByVal proxy As DefaultWorkTimeProxy)

            _Year = proxy.Year
            _Month = proxy.Month
            _WorkDaysFor5WorkDayWeek = proxy.WorkDaysFor5WorkDayWeek
            _WorkHoursFor5WorkDayWeek = proxy.WorkHoursFor5WorkDayWeek
            _WorkDaysFor6WorkDayWeek = proxy.WorkDaysFor6WorkDayWeek
            _WorkHoursFor6WorkDayWeek = proxy.WorkHoursFor6WorkDayWeek

        End Sub

#End Region

    End Class

End Namespace
