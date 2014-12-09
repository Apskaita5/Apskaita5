Namespace Workers

    <Serializable()> _
    Public Class WorkersVDUInfo

#Region " Business Methods "

        Private _ContractSerial As String
        Private _ContractNumber As Integer
        Private _ApplicableVDUHourly As Double = 0
        Private _ApplicableVDUDaily As Double = 0

        Public ReadOnly Property ContractSerial() As String
            Get
                Return _ContractSerial
            End Get
        End Property

        Public ReadOnly Property ContractNumber() As Integer
            Get
                Return _ContractNumber
            End Get
        End Property

        Public ReadOnly Property ApplicableVDUHourly() As Double
            Get
                Return _ApplicableVDUHourly
            End Get
        End Property

        Public ReadOnly Property ApplicableVDUDaily() As Double
            Get
                Return _ApplicableVDUDaily
            End Get
        End Property

#End Region

#Region " Factory Methods "

        Friend Shared Function GetNewWorkersVDUInfo(ByVal nSerial As String, _
            ByVal nNumber As Integer) As WorkersVDUInfo
            Return New WorkersVDUInfo(nSerial, nNumber)
        End Function

        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal nSerial As String, ByVal nNumber As Integer)
            _ContractSerial = nSerial
            _ContractNumber = nNumber
        End Sub

#End Region

#Region " Data Access "

        Friend Sub Fetch(ByVal nYear As Integer, ByVal nMonth As Integer)

            Dim YearFrom, YearTo, MonthFrom, MonthTo As Integer

            YearFrom = nYear - 1
            MonthFrom = nMonth
            If nMonth = 1 Then
                YearTo = nYear - 1
                MonthTo = 12
            Else
                YearTo = nYear
                MonthTo = nMonth - 1
            End If

            Dim myComm As New SQLCommand("FetchWorkersVDUInfo")
            myComm.AddParam("?YF", YearFrom)
            myComm.AddParam("?YT", YearTo)
            myComm.AddParam("?MF", MonthFrom)
            myComm.AddParam("?MT", MonthTo)
            myComm.AddParam("?CN", _ContractNumber)
            myComm.AddParam("?CS", _ContractSerial.Trim)

            Dim BonusQuarter As Double = 0
            Dim BonusYear As Double = 0
            Dim Hours As Double = 0
            Dim Days As Integer = 0
            Dim Wage As Double = 0
            Dim StandartHours As Double = 0
            Dim StandartDays As Integer = 0

            Dim i As Integer

            Using myData As DataTable = myComm.Fetch

                For i = 1 To Math.Min(myData.Rows.Count, 3)
                    Days = Days + CIntSafe(myData.Rows(i - 1).Item(2), 0)
                    Hours = Hours + CDblSafe(myData.Rows(i - 1).Item(3), 4, 0)
                    Wage = Wage + CDblSafe(myData.Rows(i - 1).Item(4), 2, 0)
                    StandartHours = StandartHours + CDblSafe(myData.Rows(i - 1).Item(5), 4, 0)
                    StandartDays = StandartDays + CIntSafe(myData.Rows(i - 1).Item(6), 0)
                    If CDblSafe(myData.Rows(i - 1).Item(7), 2, 0) > 0 AndAlso Not BonusQuarter > 0 Then _
                        BonusQuarter = CDblSafe(myData.Rows(i - 1).Item(7), 2, 0)
                Next

                For i = 1 To myData.Rows.Count
                    BonusYear = BonusYear + CDblSafe(myData.Rows(i - 1).Item(8), 2, 0)
                Next

            End Using

            BonusQuarter = CRound(BonusQuarter)
            BonusYear = CRound(BonusYear)
            Hours = CRound(Hours, 4)
            Wage = CRound(Wage)
            StandartHours = CRound(StandartHours, 4)


            If Hours > 0 AndAlso Days > 0 Then

                _ApplicableVDUHourly = CRound(Wage / Hours)
                _ApplicableVDUDaily = CRound(Wage / Days)

                If StandartHours > 0 AndAlso StandartDays > 0 Then
                    _ApplicableVDUHourly = CRound(_ApplicableVDUHourly + _
                        CRound((BonusQuarter + CRound(BonusYear / 4)) / StandartHours))
                    _ApplicableVDUDaily = CRound(_ApplicableVDUDaily + _
                        CRound((BonusQuarter + CRound(BonusYear / 4)) / StandartDays))
                End If

            Else

                _ApplicableVDUHourly = 0
                _ApplicableVDUDaily = 0

            End If

        End Sub

#End Region

    End Class

End Namespace