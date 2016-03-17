Namespace ActiveReports

    ''' <summary>
    ''' Represents a collection of work periods which a fixed holiday rate is applied for.
    ''' </summary>
    ''' <remarks>Should only by used as a child of UnusedHolidayInfo.</remarks>
    <Serializable()> _
    Public Class HolidayCalculationPeriodList
        Inherits ReadOnlyListBase(Of HolidayCalculationPeriodList, HolidayCalculationPeriod)

#Region " Business Methods "

        Private _IsFired As Boolean = False

        ''' <summary>
        ''' Whether the last period ends because the worker is fired.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property IsFired() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsFired
            End Get
        End Property


        Friend Function GetTotalPeriodInDays() As Integer
            Dim result As Integer = 0
            For Each i As HolidayCalculationPeriod In Me
                result = result + i.LengthDays
            Next
            Return result
        End Function

        Friend Function GetTotalPeriodInYears() As Double
            Dim result As Double = 0
            For Each i As HolidayCalculationPeriod In Me
                result = CRound(result + i.LengthYears, ROUNDWORKYEARS)
            Next
            Return result
        End Function

        Friend Function GetTotalCumulatedHolidayDays() As Double
            Dim result As Double = 0
            For Each i As HolidayCalculationPeriod In Me
                result = CRound(result + i.CumulatedHolidayDaysPerPeriod, ROUNDACCUMULATEDHOLIDAY)
            Next
            Return result
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function NewHolidayCalculationPeriodList() As HolidayCalculationPeriodList
            Return New HolidayCalculationPeriodList
        End Function

        Friend Shared Function GetHolidayCalculationPeriodList(ByVal myData As DataTable, _
            ByVal calculationDate As Date, ByVal forCompensation As Boolean) As HolidayCalculationPeriodList
            Return New HolidayCalculationPeriodList(myData, calculationDate, forCompensation)
        End Function


        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal myData As DataTable, ByVal calculationDate As Date, ByVal forCompensation As Boolean)
            ' require use of factory methods
            Fetch(myData, calculationDate, forCompensation)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal myData As DataTable, ByVal calculationDate As Date, ByVal forCompensation As Boolean)

            If Not myData.Rows.Count > 0 Then
                Throw New Exception(My.Resources.ActiveReports_HolidayCalculationPeriod_NoDataAvailable)
            End If

            RaiseListChangedEvents = False
            IsReadOnly = False

            For i As Integer = 1 To myData.Rows.Count - 1
                Add(HolidayCalculationPeriod.GetHolidayCalculationPeriod( _
                    myData.Rows(i - 1), myData.Rows(i)))
            Next

            Dim lastRow As DataRow = myData.Rows(myData.Rows.Count - 1)

            If Utilities.ConvertDatabaseCharID(Of Workers.WorkerStatusType) _
                (CStrSafe(lastRow.Item(2))) <> Workers.WorkerStatusType.Fired Then

                _IsFired = False

                If forCompensation Then
                    Throw New Exception(My.Resources.ActiveReports_HolidayCalculationPeriod_WorkerIsNotFired)
                Else
                    Add(HolidayCalculationPeriod.GetHolidayCalculationPeriod(lastRow, calculationDate))
                End If

            Else

                _IsFired = True

            End If

            IsReadOnly = True
            RaiseListChangedEvents = True

        End Sub

#End Region

    End Class

End Namespace