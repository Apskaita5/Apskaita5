Namespace Settings.XmlProxies

    <Serializable()> _
    Public Class DefaultWorkTimeProxy

        Private _Year As Integer = 0
        Private _Month As Integer = 0
        Private _WorkDaysFor5WorkDayWeek As Integer = 0
        Private _WorkHoursFor5WorkDayWeek As Double = 0
        Private _WorkDaysFor6WorkDayWeek As Integer = 0
        Private _WorkHoursFor6WorkDayWeek As Double = 0


        Public Property Year() As Integer
            Get
                Return _Year
            End Get
            Set(ByVal value As Integer)
                If _Year <> value Then
                    _Year = value
                End If
            End Set
        End Property

        Public Property Month() As Integer
            Get
                Return _Month
            End Get
            Set(ByVal value As Integer)
                If _Month <> value Then
                    _Month = value
                End If
            End Set
        End Property

        Public Property WorkDaysFor5WorkDayWeek() As Integer
            Get
                Return _WorkDaysFor5WorkDayWeek
            End Get
            Set(ByVal value As Integer)
                If _WorkDaysFor5WorkDayWeek <> value Then
                    _WorkDaysFor5WorkDayWeek = value
                End If
            End Set
        End Property

        Public Property WorkHoursFor5WorkDayWeek() As Double
            Get
                Return CRound(_WorkHoursFor5WorkDayWeek, ROUNDWORKHOURS)
            End Get
            Set(ByVal value As Double)
                If CRound(_WorkHoursFor5WorkDayWeek, ROUNDWORKHOURS) <> CRound(value, ROUNDWORKHOURS) Then
                    _WorkHoursFor5WorkDayWeek = CRound(value, ROUNDWORKHOURS)
                End If
            End Set
        End Property

        Public Property WorkDaysFor6WorkDayWeek() As Integer
            Get
                Return _WorkDaysFor6WorkDayWeek
            End Get
            Set(ByVal value As Integer)
                If _WorkDaysFor6WorkDayWeek <> value Then
                    _WorkDaysFor6WorkDayWeek = value
                End If
            End Set
        End Property

        Public Property WorkHoursFor6WorkDayWeek() As Double
            Get
                Return CRound(_WorkHoursFor6WorkDayWeek, ROUNDWORKHOURS)
            End Get
            Set(ByVal value As Double)
                If CRound(_WorkHoursFor6WorkDayWeek, ROUNDWORKHOURS) <> CRound(value, ROUNDWORKHOURS) Then
                    _WorkHoursFor6WorkDayWeek = CRound(value, ROUNDWORKHOURS)
                End If
            End Set
        End Property

    End Class

End Namespace