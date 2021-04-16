Namespace Workers

    ''' <summary>
    ''' Represents normal work time duration or rest time for a certain labour contract for a certain month.
    ''' </summary>
    ''' <remarks>Should only be used as a child of a <see cref="WorkTimeItem">WorkTimeItem</see>.
    ''' Values are stored in the database table dayworktimes.</remarks>
    <Serializable()> _
    Public NotInheritable Class DayWorkTimeList
        Inherits BusinessListBase(Of DayWorkTimeList, DayWorkTime)

#Region " Business Methods "

        Public Function GetAllBrokenRules() As String
            Dim result As String = GetAllBrokenRulesForList(Me)

            'Dim GeneralErrorString As String = ""
            'SomeGeneralValidationSub(GeneralErrorString)
            'AddWithNewLine(result, GeneralErrorString, False)

            Return result
        End Function

        Public Function GetAllWarnings() As String
            Dim result As String = GetAllWarningsForList(Me)
            'Dim GeneralErrorString As String = ""
            'SomeGeneralValidationSub(GeneralErrorString)
            'AddWithNewLine(result, GeneralErrorString, False)

            Return result
        End Function


        ''' <summary>
        ''' Gets a DayWorkTime for a certain day of the current month.
        ''' </summary>
        ''' <param name="dayNumber">Number of the day to get a DayWorkTime for.</param>
        ''' <remarks></remarks>
        Public Function GetItemForDay(ByVal dayNumber As Integer) As DayWorkTime
            For Each i As DayWorkTime In Me
                If i.DayNumber = dayNumber Then Return i
            Next
            Return Nothing
        End Function


        Friend Function TrySetLengthForDay(ByVal dayNumber As Integer, ByVal newLength As Double) As Boolean
            For Each i As DayWorkTime In Me
                If i.DayNumber = dayNumber Then Return i.SetLength(newLength)
            Next
            Return False
        End Function

        Friend Function TrySetTypeForDay(ByVal dayNumber As Integer, _
            ByVal newType As WorkTimeClassInfo) As Boolean
            For Each i As DayWorkTime In Me
                If i.DayNumber = dayNumber Then Return i.SetType(newType)
            Next
            Return False
        End Function

        Friend Function GetTotalWorkDays() As Integer
            Dim result As Integer = 0
            For Each i As DayWorkTime In Me
                If (i.Type Is Nothing OrElse Not i.Type.ID > 0) AndAlso i.Length > 0 Then _
                    result += 1
            Next
            Return result
        End Function

        Friend Function GetTotalAbsenceDays(ByVal defaultRestTimeClass As WorkTimeClassInfo, _
            ByVal defaultPublicHolidaysClass As WorkTimeClassInfo) As Integer
            Dim result As Integer = 0
            For Each i As DayWorkTime In Me
                If Not i.Length > 0 AndAlso Not i.Type Is Nothing AndAlso i.Type.ID > 0 _
                    AndAlso i.Type.ID <> defaultRestTimeClass.ID _
                    AndAlso i.Type.ID <> defaultPublicHolidaysClass.ID Then _
                    result += 1
            Next
            Return result
        End Function

        Friend Function GetTotalWorkHours() As Double
            Dim result As Double = 0
            For Each i As DayWorkTime In Me
                If (i.Type Is Nothing OrElse Not i.Type.ID > 0) AndAlso i.Length > 0 Then _
                    result = CRound(result + i.Length, ROUNDWORKHOURS)
            Next
            Return result
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets a new instance of DayWorkTimeList.
        ''' </summary>
        ''' <param name="cYear">Year of the new instance.</param>
        ''' <param name="cMonth">Month of the new instance.</param>
        ''' <param name="workLoad">Workers workload at the month (ratio between contractual work hours and gauge work hours (40H/Week)).</param>
        ''' <param name="contractDate">Labour contract date.</param>
        ''' <param name="contractTerminationDate">Labour contract termination date.</param>
        ''' <param name="restDayInfo">Default type for a rest day.</param>
        ''' <param name="publicHolydaysInfo">Default type for a public holiday day.</param>
        ''' <remarks></remarks>
        Friend Shared Function NewDayWorkTimeList(ByVal cYear As Integer, ByVal cMonth As Integer, _
            ByVal workLoad As Double, ByVal contractDate As Date, ByVal contractTerminationDate As Date, _
            ByVal restDayInfo As WorkTimeClassInfo, _
            ByVal publicHolydaysInfo As WorkTimeClassInfo, _
            ByVal cWorkTimeList As DefaultWorkTimeInfoList) As DayWorkTimeList
            Return New DayWorkTimeList(cYear, cMonth, workLoad, contractDate, _
                contractTerminationDate, restDayInfo, publicHolydaysInfo, cWorkTimeList)
        End Function

        ''' <summary>
        ''' Gets an existing DayWorkTimeList instance from a database.
        ''' </summary>
        ''' <param name="myData">Database query result.</param>
        ''' <param name="parent">WorkTimeItem that encapsulates the DayWorkTimeList instance.</param>
        ''' <param name="year">Year of the DayWorkTimeList.</param>
        ''' <param name="month">Month of the DayWorkTimeList.</param>
        ''' <remarks></remarks>
        Friend Shared Function GetDayWorkTimeList(ByVal myData As DataTable, _
            ByVal parent As WorkTimeItem, ByVal year As Integer, _
            ByVal month As Integer, ByVal cWorkTimeList As DefaultWorkTimeInfoList) As DayWorkTimeList
            Return New DayWorkTimeList(myData, parent, year, month, cWorkTimeList)
        End Function


        Private Sub New()
            ' require use of factory methods
            MarkAsChild()
            Me.AllowEdit = True
            Me.AllowNew = False
            Me.AllowRemove = False
        End Sub

        Private Sub New(ByVal cYear As Integer, ByVal cMonth As Integer, ByVal workLoad As Double, _
            ByVal contractDate As Date, ByVal contractTerminationDate As Date, _
            ByVal restDayInfo As WorkTimeClassInfo, _
            ByVal publicHolydaysInfo As WorkTimeClassInfo, _
            ByVal cWorkTimeList As DefaultWorkTimeInfoList)
            ' require use of factory methods
            MarkAsChild()
            Me.AllowEdit = True
            Me.AllowNew = False
            Me.AllowRemove = False
            Create(cYear, cMonth, workLoad, contractDate, contractTerminationDate, _
                restDayInfo, publicHolydaysInfo, cWorkTimeList)
        End Sub

        Private Sub New(ByVal myData As DataTable, ByVal parent As WorkTimeItem, _
            ByVal year As Integer, ByVal month As Integer, _
            ByVal cWorkTimeList As DefaultWorkTimeInfoList)
            ' require use of factory methods
            MarkAsChild()
            Me.AllowEdit = True
            Me.AllowNew = False
            Me.AllowRemove = False
            Fetch(myData, parent, year, month, cWorkTimeList)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Create(ByVal cYear As Integer, ByVal cMonth As Integer, ByVal workLoad As Double, _
            ByVal contractDate As Date, ByVal contractTerminationDate As Date, _
            ByVal restDayInfo As WorkTimeClassInfo, _
            ByVal publicHolydaysInfo As WorkTimeClassInfo, _
            ByVal cWorkTimeList As DefaultWorkTimeInfoList)

            RaiseListChangedEvents = False

            Dim hasContract As Boolean
            Dim currentDate As Date

            For i As Integer = 1 To 31
                currentDate = New Date(cYear, cMonth, Math.Min(i, Date.DaysInMonth(cYear, cMonth)))
                hasContract = (contractDate.Date < currentDate.Date AndAlso _
                    contractTerminationDate.Date >= currentDate.Date)
                Me.Add(DayWorkTime.NewDayWorkTime(i, cYear, cMonth, workLoad, hasContract, _
                    restDayInfo, publicHolydaysInfo, cWorkTimeList))
            Next

            RaiseListChangedEvents = True

        End Sub

        Private Sub Fetch(ByVal myData As DataTable, ByVal parent As WorkTimeItem, _
            ByVal year As Integer, ByVal month As Integer, _
            ByVal cWorkTimeList As DefaultWorkTimeInfoList)

            RaiseListChangedEvents = False

            Dim maxDayCount As Integer = Date.DaysInMonth(year, month)

            For Each dr As DataRow In myData.Rows
                If CIntSafe(dr.Item(0), 0) = parent.ID Then _
                    Add(DayWorkTime.GetDayWorkTime(dr, maxDayCount))
            Next

            Dim mustHaveList As New List(Of Integer)
            For i As Integer = 1 To 31
                mustHaveList.Add(i)
            Next
            For Each i As DayWorkTime In Me
                If mustHaveList.Contains(i.DayNumber) Then mustHaveList.Remove(i.DayNumber)
            Next
            For Each i As Integer In mustHaveList
                Add(DayWorkTime.NewDayWorkTime(i, year, month, 0, True, Nothing, Nothing, cWorkTimeList))
            Next

            RaiseListChangedEvents = True

        End Sub

        Friend Sub Update(ByVal parent As WorkTimeItem)

            RaiseListChangedEvents = False

            DeletedList.Clear()

            For Each i As DayWorkTime In Me
                If i.IsNew Then
                    i.Insert(parent)
                ElseIf i.IsDirty Then
                    i.Update(parent)
                End If
            Next

            RaiseListChangedEvents = True

        End Sub

#End Region

    End Class

End Namespace