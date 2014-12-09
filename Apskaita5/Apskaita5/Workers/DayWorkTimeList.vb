Namespace Workers

    <Serializable()> _
    Public Class DayWorkTimeList
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


        Public Function GetItemForDay(ByVal DayNumber As Integer) As DayWorkTime
            For Each i As DayWorkTime In Me
                If i.DayNumber = DayNumber Then Return i
            Next
            Return Nothing
        End Function


        Friend Function SetLengthForDay(ByVal DayNumber As Integer, ByVal newLength As Double) As Boolean
            For Each i As DayWorkTime In Me
                If i.DayNumber = DayNumber Then Return i.SetLength(newLength)
            Next
            Return False
        End Function

        Friend Function SetTypeForDay(ByVal DayNumber As Integer, _
            ByVal newType As WorkTimeClassInfo) As Boolean
            For Each i As DayWorkTime In Me
                If i.DayNumber = DayNumber Then Return i.SetType(newType)
            Next
            Return False
        End Function

        Friend Function GetTotalDays() As Integer
            Dim result As Integer = 0
            For Each i As DayWorkTime In Me
                If (i.Type Is Nothing OrElse Not i.Type.ID > 0) AndAlso i.Length > 0 Then _
                    result += 1
            Next
            Return result
        End Function

        Friend Function GetTotalAbsenceDays(ByVal DefaultRestTimeClass As WorkTimeClassInfo, _
            ByVal DefaultPublicHolidaysClass As WorkTimeClassInfo) As Integer
            Dim result As Integer = 0
            For Each i As DayWorkTime In Me
                If Not i.Length > 0 AndAlso Not i.Type Is Nothing AndAlso i.Type.ID > 0 _
                    AndAlso i.Type.ID <> DefaultRestTimeClass.ID _
                    AndAlso i.Type.ID <> DefaultPublicHolidaysClass.ID Then _
                    result += 1
            Next
            Return result
        End Function

        Friend Function GetTotalHours() As Double
            Dim result As Double = 0
            For Each i As DayWorkTime In Me
                If (i.Type Is Nothing OrElse Not i.Type.ID > 0) AndAlso i.Length > 0 Then _
                    result = CRound(result + i.Length, ROUNDWORKTIME)
            Next
            Return result
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function NewDayWorkTimeList(ByVal cYear As Integer, _
            ByVal cMonth As Integer, ByVal cLoad As Double, _
            ByVal RestDayInfo As WorkTimeClassInfo, ByVal PublicHolydaysInfo As WorkTimeClassInfo) As DayWorkTimeList
            Return New DayWorkTimeList(cYear, cMonth, cLoad, RestDayInfo, PublicHolydaysInfo)
        End Function

        Friend Shared Function GetDayWorkTimeList(ByVal myData As DataTable, _
            ByVal parent As WorkTimeItem, ByVal Year As Integer, ByVal Month As Integer) As DayWorkTimeList
            Return New DayWorkTimeList(myData, parent, Year, Month)
        End Function

        Private Sub New()
            ' require use of factory methods
            MarkAsChild()
            Me.AllowEdit = True
            Me.AllowNew = False
            Me.AllowRemove = False
        End Sub

        Private Sub New(ByVal cYear As Integer, ByVal cMonth As Integer, _
            ByVal cLoad As Double, ByVal RestDayInfo As WorkTimeClassInfo, _
            ByVal PublicHolydaysInfo As WorkTimeClassInfo)
            ' require use of factory methods
            MarkAsChild()
            Me.AllowEdit = True
            Me.AllowNew = False
            Me.AllowRemove = False
            Create(cYear, cMonth, cLoad, RestDayInfo, PublicHolydaysInfo)
        End Sub

        Private Sub New(ByVal myData As DataTable, ByVal parent As WorkTimeItem, _
            ByVal Year As Integer, ByVal Month As Integer)
            ' require use of factory methods
            MarkAsChild()
            Me.AllowEdit = True
            Me.AllowNew = False
            Me.AllowRemove = False
            Fetch(myData, parent, Year, Month)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Create(ByVal cYear As Integer, ByVal cMonth As Integer, _
            ByVal cLoad As Double, ByVal RestDayInfo As WorkTimeClassInfo, _
            ByVal PublicHolydaysInfo As WorkTimeClassInfo)

            RaiseListChangedEvents = False

            For i As Integer = 1 To 31
                Me.Add(DayWorkTime.NewDayWorkTime(i, cYear, cMonth, cLoad, _
                    RestDayInfo, PublicHolydaysInfo))
            Next

            RaiseListChangedEvents = True

        End Sub

        Private Sub Fetch(ByVal myData As DataTable, ByVal parent As WorkTimeItem, _
            ByVal Year As Integer, ByVal Month As Integer)

            RaiseListChangedEvents = False

            Dim MaxDayCount As Integer = Date.DaysInMonth(Year, Month)

            For Each dr As DataRow In myData.Rows
                If CIntSafe(dr.Item(0), 0) = parent.ID Then _
                    Add(DayWorkTime.GetDayWorkTime(dr, MaxDayCount))
            Next

            Dim mustHaveList As New List(Of Integer)
            For i As Integer = 1 To 31
                mustHaveList.Add(i)
            Next
            For Each i As DayWorkTime In Me
                If mustHaveList.Contains(i.DayNumber) Then mustHaveList.Remove(i.DayNumber)
            Next
            For Each i As Integer In mustHaveList
                Add(DayWorkTime.NewDayWorkTime(i, Year, Month, 0, Nothing, Nothing))
            Next

            RaiseListChangedEvents = True

        End Sub

        Friend Sub Update(ByVal parent As WorkTimeItem)

            RaiseListChangedEvents = False

            DeletedList.Clear()

            For Each item As DayWorkTime In Me
                If item.IsNew Then
                    item.Insert(parent)
                ElseIf item.IsDirty Then
                    item.Update(parent)
                End If
            Next

            RaiseListChangedEvents = True

        End Sub

#End Region

    End Class

End Namespace