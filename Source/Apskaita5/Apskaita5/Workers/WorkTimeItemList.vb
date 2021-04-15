Namespace Workers

    ''' <summary>
    ''' Represents a general (standard/normal) type of work hours for specific workers for a specific month.
    ''' </summary>
    ''' <remarks>Should only be used as a child of <see cref="WorkTimeSheet">WorkTimeSheet</see>.
    ''' Values are stored by an encapsulated <see cref="DayWorkTimeList">DayWorkTimeList</see> object
    ''' and a databse table worktimeitems.</remarks>
    <Serializable()> _
    Public NotInheritable Class WorkTimeItemList
        Inherits BusinessListBase(Of WorkTimeItemList, WorkTimeItem)

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

        Public Function HasWarning() As Boolean
            For Each i As WorkTimeItem In Me
                If i.BrokenRulesCollection.WarningCount > 0 Then Return True
            Next
            Return False
        End Function

#End Region

#Region " Factory Methods "

        ' used to implement automatic sort in datagridview
        <NonSerialized()> _
        Private _SortedList As Csla.SortedBindingList(Of WorkTimeItem) = Nothing


        Friend Shared Function NewWorkTimeItemList(ByVal parent As WorkTimeSheet, _
            ByVal restDayInfo As WorkTimeClassInfo, ByVal publicHolydaysInfo As WorkTimeClassInfo) As WorkTimeItemList
            Return New WorkTimeItemList(parent, restDayInfo, publicHolydaysInfo)
        End Function

        Friend Shared Function GetWorkTimeItemList(ByVal parent As WorkTimeSheet) As WorkTimeItemList
            Return New WorkTimeItemList(parent)
        End Function


        Public Function GetSortedList() As Csla.SortedBindingList(Of WorkTimeItem)
            If _SortedList Is Nothing Then _SortedList = New Csla.SortedBindingList(Of WorkTimeItem)(Me)
            Return _SortedList
        End Function


        Private Sub New()
            ' require use of factory methods
            MarkAsChild()
            Me.AllowEdit = True
            Me.AllowNew = False
            Me.AllowRemove = False
        End Sub

        Private Sub New(ByVal parent As WorkTimeSheet, ByVal restDayInfo As WorkTimeClassInfo, _
            ByVal publicHolydaysInfo As WorkTimeClassInfo)
            ' require use of factory methods
            MarkAsChild()
            Me.AllowEdit = True
            Me.AllowNew = False
            Me.AllowRemove = False
            Create(parent, restDayInfo, publicHolydaysInfo)
        End Sub

        Private Sub New(ByVal parent As WorkTimeSheet)
            ' require use of factory methods
            MarkAsChild()
            Me.AllowEdit = True
            Me.AllowNew = False
            Me.AllowRemove = False
            Fetch(parent)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Create(ByVal parent As WorkTimeSheet, ByVal restDayInfo As WorkTimeClassInfo, _
            ByVal publicHolydaysInfo As WorkTimeClassInfo)

            Dim myComm As New SQLCommand("CreateWorkTimeItemList")
            myComm.AddParam("?DT", New Date(parent.Year, parent.Month, _
                Date.DaysInMonth(parent.Year, parent.Month)).Date)
            myComm.AddParam("?DF", New Date(parent.Year, parent.Month, 1).Date)
            myComm.AddParam("?YR", parent.Year)
            myComm.AddParam("?MN", parent.Month)

            Using myData As DataTable = myComm.Fetch

                RaiseListChangedEvents = False

                Dim wtl As DefaultWorkTimeInfoList = DefaultWorkTimeInfoList.GetListChild()
                Dim wt As DefaultWorkTimeInfo = wtl.GetDefaultWorkTimeInfo(parent.Year, parent.Month)

                For Each dr As DataRow In myData.Rows
                    Add(WorkTimeItem.NewWorkTimeItem(dr, parent.Year, parent.Month, _
                        restDayInfo, publicHolydaysInfo, wt, wtl))
                Next

                RaiseListChangedEvents = True

            End Using

        End Sub

        Private Sub Fetch(ByVal parent As WorkTimeSheet)

            Dim myComm As New SQLCommand("FetchWorkTimeItemList")
            myComm.AddParam("?DT", New Date(parent.Year, parent.Month, _
                Date.DaysInMonth(parent.Year, parent.Month)).Date)
            myComm.AddParam("?PD", parent.ID)

            Using myData As DataTable = myComm.Fetch

                myComm = New SQLCommand("FetchDayWorkTimeList")
                myComm.AddParam("?PD", parent.ID)

                Using dayWorkTimeDataTable As DataTable = myComm.Fetch

                    RaiseListChangedEvents = False

                    Dim wtl As DefaultWorkTimeInfoList = DefaultWorkTimeInfoList.GetListChild()

                    For Each dr As DataRow In myData.Rows
                        Add(WorkTimeItem.GetWorkTimeItem(dr, dayWorkTimeDataTable, _
                            parent.Year, parent.Month, wtl))
                    Next

                    RaiseListChangedEvents = True

                End Using

            End Using

        End Sub

        Friend Sub Update(ByVal parent As WorkTimeSheet)

            RaiseListChangedEvents = False

            DeletedList.Clear()

            Me.AllowRemove = True

            For i As Integer = Me.Count To 1 Step -1
                If Item(i - 1).IsNew AndAlso Item(i - 1).IsChecked Then
                    Item(i - 1).Insert(parent)
                ElseIf Item(i - 1).IsDirty AndAlso Item(i - 1).IsChecked Then
                    Item(i - 1).Update(parent)
                ElseIf Item(i - 1).IsDirty Then
                    Item(i - 1).DeleteSelf()
                    Me.RemoveAt(i - 1)
                End If
            Next

            Me.AllowRemove = False

            Me.DeletedList.Clear()

            RaiseListChangedEvents = True

        End Sub

#End Region

    End Class

End Namespace