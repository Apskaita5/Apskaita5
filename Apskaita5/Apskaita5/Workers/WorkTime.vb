Namespace Workers

    ''' <summary>
    ''' Represents general data about work time and public holiday in a country.
    ''' </summary>
    ''' <remarks>Data is managed localy in files <see cref="WORKTIMEDATA_FILE"/> and <see cref="PUBLICHOLIDAYSDATA_FILE"/>.</remarks>
    Public Class WorkTime

        Private _Year As Integer
        Private _Month As Integer
        Private _WorkDaysFor5WorkDayWeek As Integer
        Private _WorkHoursFor5WorkDayWeek As Double
        Private _WorkDaysFor6WorkDayWeek As Integer
        Private _WorkHoursFor6WorkDayWeek As Double

        Private Shared _Items As List(Of WorkTime) = Nothing
        Private Shared _PublicHolidaysItems As List(Of Date) = Nothing


        Public ReadOnly Property Year() As Integer
            Get
                Return _Year
            End Get
        End Property

        Public ReadOnly Property Month() As Integer
            Get
                Return _Month
            End Get
        End Property

        Public ReadOnly Property WorkDaysFor5WorkDayWeek() As Integer
            Get
                Return _WorkDaysFor5WorkDayWeek
            End Get
        End Property

        Public ReadOnly Property WorkHoursFor5WorkDayWeek() As Double
            Get
                Return CRound(_WorkHoursFor5WorkDayWeek, ROUNDWORKTIME)
            End Get
        End Property

        Public ReadOnly Property WorkDaysFor6WorkDayWeek() As Integer
            Get
                Return _WorkDaysFor6WorkDayWeek
            End Get
        End Property

        Public ReadOnly Property WorkHoursFor6WorkDayWeek() As Double
            Get
                Return CRound(_WorkHoursFor6WorkDayWeek, ROUNDWORKTIME)
            End Get
        End Property



        Private Function SameMonth(ByVal nYear As Integer, ByVal nMonth As Integer) As Boolean
            Return nYear = _Year AndAlso nMonth = _Month
        End Function


        Public Shared Function GetWorktime(ByVal nYear As Integer, ByVal nMonth As Integer) As WorkTime

            If _Items Is Nothing Then LoadItems()

            For Each w As WorkTime In _Items
                If w.SameMonth(nYear, nMonth) Then Return w
            Next

            Return WorkTime.GetCalculatedWorktime(nYear, nMonth)

        End Function

        Public Shared Function IsPublicHolidays(ByVal nYear As Integer, _
            ByVal nMonth As Integer, ByVal nDay As Integer) As Boolean

            If nYear < 1950 OrElse nMonth < 0 OrElse nYear > 2100 OrElse nMonth > 12 _
                OrElse nday > Date.DaysInMonth(nYear, nMonth) Then Return False

            Return IsPublicHolidays(New Date(nYear, nMonth, nDay))

        End Function

        Public Shared Function IsPublicHolidays(ByVal nDate As Date) As Boolean

            If _PublicHolidaysItems Is Nothing Then LoadPublicHolidaysItems()

            Return _PublicHolidaysItems.Contains(nDate.Date)

        End Function

        Private Shared Function GetCalculatedWorktime(ByVal nYear As Integer, ByVal nMonth As Integer) As WorkTime
            Return New WorkTime(nYear, nMonth)
        End Function

        Private Sub New()

        End Sub

        Private Sub New(ByVal s As String)
            Fetch(s)
        End Sub

        Private Sub New(ByVal nYear As Integer, ByVal nMonth As Integer)
            Fetch(nYear, nMonth)
        End Sub

        Private Sub Fetch(ByVal s As String)
            _Year = CInt(GetElement(s, 0))
            _Month = CInt(GetElement(s, 1))
            _WorkDaysFor5WorkDayWeek = CInt(GetElement(s, 2))
            _WorkHoursFor5WorkDayWeek = CRound(CInt(GetElement(s, 3)) / 1000, 3)
            _WorkDaysFor6WorkDayWeek = CInt(GetElement(s, 4))
            _WorkHoursFor6WorkDayWeek = CRound(CInt(GetElement(s, 5)) / 1000, 3)
        End Sub

        Private Sub Fetch(ByVal nYear As Integer, ByVal nMonth As Integer)

            Dim WDays5 As Integer = 0
            Dim WDays6 As Integer = 0
            Dim WHours5 As Double = 0
            Dim WHours6 As Double = 0

            Dim nDate As Date
            For i As Integer = 1 To Date.DaysInMonth(nYear, nMonth)
                nDate = New Date(nYear, nMonth, i)
                If nDate.DayOfWeek <> DayOfWeek.Sunday AndAlso nDate.DayOfWeek <> DayOfWeek.Saturday Then
                    WDays5 += 1
                    WDays6 += 1
                    WHours5 += 8
                    WHours6 += 6.6666
                ElseIf nDate.DayOfWeek <> DayOfWeek.Sunday Then
                    WDays6 += 1
                    WHours6 += 6.6666
                Else
                    WHours6 = CRound(WHours6, 3)
                End If
            Next

            Me._Year = nYear
            Me._Month = nMonth
            Me._WorkDaysFor5WorkDayWeek = WDays5
            Me._WorkDaysFor6WorkDayWeek = WDays6
            Me._WorkHoursFor5WorkDayWeek = WHours5
            Me._WorkHoursFor6WorkDayWeek = CRound(WHours6, 3)

        End Sub

        Private Shared Sub LoadItems()

            _Items = New List(Of WorkTime)

            Dim readtext As String() = Nothing
            Try
                Dim path As String = IO.Path.Combine(AppPath(), WORKTIMEDATA_FILE)
                readtext = IO.File.ReadAllLines(path)
            Catch ex As Exception
            End Try
            If Not readtext Is Nothing Then
                For Each s As String In readtext 'duombaziu faile ieskom pasirinktos
                    If Not String.IsNullOrEmpty(s.Trim) Then
                        _Items.Add(New WorkTime(s))
                    End If
                Next
            End If

        End Sub

        Private Shared Sub LoadPublicHolidaysItems()

            Dim path As String = IO.Path.Combine(AppPath(), PUBLICHOLIDAYSDATA_FILE)
            _PublicHolidaysItems = New List(Of Date)
            If IO.File.Exists(path) Then
                Dim readtext() As String = IO.File.ReadAllLines(path)
                For Each s As String In readtext 'duombaziu faile ieskom pasirinktos
                    If Not String.IsNullOrEmpty(s.Trim) Then
                        _PublicHolidaysItems.Add(Date.ParseExact(s, "yyyy-MM-dd", _
                            System.Globalization.CultureInfo.InvariantCulture))
                    End If
                Next
            End If

        End Sub

    End Class

End Namespace
