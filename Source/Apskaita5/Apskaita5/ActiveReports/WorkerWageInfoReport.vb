﻿Imports ApskaitaObjects.Attributes

Namespace ActiveReports

    ''' <summary>
    ''' Represents a worker wage report. Contains information about worker's wage parameters, 
    ''' payments and unused annual holiday balance within the report period.
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public NotInheritable Class WorkerWageInfoReport
        Inherits ReadOnlyBase(Of WorkerWageInfoReport)

#Region " Business Methods "

        Private _Items As WorkerWageInfoList
        Private _DateFrom As Date = Today
        Private _DateTo As Date = Today
        Private _ContractNumber As Integer = 0
        Private _ContractSerial As String = ""
        Private _IsConsolidated As Boolean = False
        Private _PersonID As Integer = 0
        Private _PersonInfo As String = ""
        Private _DebtAtTheStart As Double = 0
        Private _DebtAtEnd As Double = 0
        Private _UnusedHolidaysAtStart As Double = 0
        Private _UnusedHolidaysAtEnd As Double = 0


        ''' <summary>
        ''' Report child items that contain information about worker's wage parameters, 
        ''' payments for each month within the report period.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Items() As WorkerWageInfoList
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Items
            End Get
        End Property

        ''' <summary>
        ''' The starting date of the report period.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DateFrom() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DateFrom
            End Get
        End Property

        ''' <summary>
        ''' The ending date of the report period.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DateTo() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DateTo
            End Get
        End Property

        ''' <summary>
        ''' A number of the worker's contract.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property ContractNumber() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ContractNumber
            End Get
        End Property

        ''' <summary>
        ''' A serial of the worker's contract.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property ContractSerial() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ContractSerial.Trim
            End Get
        End Property

        ''' <summary>
        ''' Whether the report is consolidated, i.e. contains aggregated data
        ''' of all the labour contracts of the worker.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property IsConsolidated() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsConsolidated
            End Get
        End Property

        ''' <summary>
        ''' A worker's <see cref="General.Person.ID">ID</see>.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property PersonID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _PersonID
            End Get
        End Property

        ''' <summary>
        ''' A short description of the labour contract. In case the report is consolidated -
        ''' a worker's name and personal code.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property PersonInfo() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _PersonInfo.Trim
            End Get
        End Property

        ''' <summary>
        ''' Wage debt at the begining of the report period.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property DebtAtTheStart() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_DebtAtTheStart)
            End Get
        End Property

        ''' <summary>
        ''' Wage debt at the end of the report period.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property DebtAtEnd() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_DebtAtEnd)
            End Get
        End Property

        ''' <summary>
        ''' Unused holiday amount at the begining of the report period.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDACCUMULATEDHOLIDAY)> _
        Public ReadOnly Property UnusedHolidaysAtStart() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_UnusedHolidaysAtStart, ROUNDACCUMULATEDHOLIDAY)
            End Get
        End Property

        ''' <summary>
        ''' Unused holiday amount at the end of the report period.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDACCUMULATEDHOLIDAY)> _
        Public ReadOnly Property UnusedHolidaysAtEnd() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_UnusedHolidaysAtEnd, ROUNDACCUMULATEDHOLIDAY)
            End Get
        End Property


        Protected Overrides Function GetIdValue() As Object
            Return _PersonID
        End Function

        Public Overrides Function ToString() As String
            If _IsConsolidated Then Return "Darbuotojo " & _PersonInfo _
                & " konsoliduota darbo užmokesčio kortelė"
            Return "Darbuotojo " & _PersonInfo & " darbo užmokesčio kortelė pgl. " _
                & "darbo sutartį Nr. " & _ContractSerial & _ContractNumber.ToString
        End Function

#End Region

#Region " Authorization Rules "

        Protected Overrides Sub AddAuthorizationRules()

        End Sub

        Public Shared Function CanGetObject() As Boolean
            Return ApplicationContext.User.IsInRole("Workers.WageInfo1")
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets a new instance of WorkerWageInfoReport.
        ''' </summary>
        ''' <param name="dateFrom">The starting date of the report period.</param>
        ''' <param name="dateTo">The ending date of the report period.</param>
        ''' <param name="personID">A worker's <see cref="General.Person.ID">ID</see>.</param>
        ''' <param name="contractSerial">A serial of the worker's contract.</param>
        ''' <param name="contractNumber">A number of the worker's contract.</param>
        ''' <param name="fetchConsolidatedInfo">Whether the report is consolidated, 
        ''' i.e. contains aggregated data of all the labour contracts of the worker.</param>
        ''' <remarks></remarks>
        Public Shared Function GetWorkerWageInfoReport(ByVal dateFrom As Date, _
            ByVal dateTo As Date, ByVal personID As Integer, ByVal contractSerial As String, _
            ByVal contractNumber As Integer, ByVal fetchConsolidatedInfo As Boolean) As WorkerWageInfoReport

            Return DataPortal.Fetch(Of WorkerWageInfoReport)(New Criteria(dateFrom, _
                dateTo, personID, contractSerial, contractNumber, fetchConsolidatedInfo))

        End Function


        Private Sub New()
            ' require use of factory methods
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> _
        Private Class Criteria
            Private _DateFrom As Date
            Private _DateTo As Date
            Private _FetchConsolidatedInfo As Boolean
            Private _ContractNumber As Integer
            Private _ContractSerial As String
            Private _PersonID As Integer
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
            Public ReadOnly Property PersonID() As Integer
                Get
                    Return _PersonID
                End Get
            End Property
            Public ReadOnly Property DateFrom() As Date
                Get
                    Return _DateFrom
                End Get
            End Property
            Public ReadOnly Property DateTo() As Date
                Get
                    Return _DateTo
                End Get
            End Property
            Public ReadOnly Property FetchConsolidatedInfo() As Boolean
                Get
                    Return _FetchConsolidatedInfo
                End Get
            End Property
            Public Sub New(ByVal nDateFrom As Date, ByVal nDateTo As Date, ByVal nPersonID As Integer, _
                ByVal nContractSerial As String, ByVal nContractNumber As Integer, _
                ByVal nFetchConsolidatedInfo As Boolean)
                _DateFrom = nDateFrom
                _DateTo = nDateTo
                _PersonID = nPersonID
                If nContractSerial Is Nothing Then
                    _ContractSerial = ""
                Else
                    _ContractSerial = nContractSerial
                End If
                _ContractNumber = nContractNumber
                _FetchConsolidatedInfo = nFetchConsolidatedInfo
            End Sub
        End Class


        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)

            If Not CanGetObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecuritySelectDenied)

            If Not criteria.PersonID > 0 Then
                Throw New Exception(My.Resources.ActiveReports_WorkerWageInfoReport_PersonIdNull)
            ElseIf Not criteria.FetchConsolidatedInfo AndAlso Not criteria.ContractNumber > 0 Then
                Throw New Exception(My.Resources.ActiveReports_WorkerWageInfoReport_ContractNumberNull)
            End If

            Dim myComm As New SQLCommand("FetchWorkerWagePayable")
            myComm.AddParam("?MF", criteria.DateFrom.Month)
            myComm.AddParam("?YF", criteria.DateFrom.Year)
            myComm.AddParam("?MT", criteria.DateTo.Month)
            myComm.AddParam("?YT", criteria.DateTo.Year)
            If criteria.FetchConsolidatedInfo Then
                myComm.AddParam("?PD", criteria.PersonID)
                myComm.AddParam("?CS", "")
                myComm.AddParam("?CN", 0)
            Else
                myComm.AddParam("?PD", 0)
                If StringIsNullOrEmpty(criteria.ContractSerial) Then
                    myComm.AddParam("?CS", "")
                Else
                    myComm.AddParam("?CS", criteria.ContractSerial.Trim)
                End If
                myComm.AddParam("?CN", criteria.ContractNumber)
            End If

            Using myData As DataTable = myComm.Fetch

                myComm = New SQLCommand("FetchWorkerWagePayments")
                myComm.AddParam("?MF", criteria.DateFrom.Month)
                myComm.AddParam("?YF", criteria.DateFrom.Year)
                myComm.AddParam("?PD", criteria.PersonID)
                myComm.AddParam("?CS", criteria.ContractSerial.Trim)
                myComm.AddParam("?CN", criteria.ContractNumber)
                myComm.AddParam("?DF", criteria.DateFrom)
                myComm.AddParam("?DT", criteria.DateTo)

                Using paymentsData As DataTable = myComm.Fetch()

                    _Items = WorkerWageInfoList.GetWorkerWageInfoList(myData, paymentsData)

                    Dim dr As DataRow = WorkerWageInfoList.GetRowByMonth(0, 0, paymentsData)
                    If Not dr Is Nothing Then
                        _DebtAtTheStart = CRound(CDblSafe(dr.Item(3), 2, 0) _
                            - CDblSafe(dr.Item(2), 2, 0), 2)
                    End If

                End Using

            End Using

            _DebtAtEnd = CRound(_DebtAtTheStart + _Items.GetTotalSumPayable - _Items.GetTotalSumPayed)

            If criteria.FetchConsolidatedInfo Then

                _UnusedHolidaysAtStart = 0
                _UnusedHolidaysAtEnd = 0

                Dim info As PersonInfo = HelperLists.PersonInfo.GetPersonInfoChild( _
                    criteria.PersonID, True)
                _PersonInfo = String.Format(My.Resources.ActiveReports_WorkerWageInfoReport_PersonInfo, _
                    info.Name, info.Code)

            Else

                Dim holidaysAtEnd As WorkerHolidayInfo = WorkerHolidayInfo.GetWorkerHolidayInfoChild( _
                    criteria.DateTo, criteria.ContractSerial, criteria.ContractNumber, False)
                _UnusedHolidaysAtEnd = holidaysAtEnd.TotalUnusedHolidayDays

                If holidaysAtEnd.ContractDate.Date >= criteria.DateFrom.Date Then

                    _UnusedHolidaysAtStart = 0

                Else

                    Dim holidaysAtStart As WorkerHolidayInfo = WorkerHolidayInfo.GetWorkerHolidayInfoChild( _
                        criteria.DateFrom, criteria.ContractSerial, criteria.ContractNumber, False)

                    _UnusedHolidaysAtStart = holidaysAtStart.TotalUnusedHolidayDays

                End If

                _PersonInfo = String.Format(My.Resources.ActiveReports_WorkerWageInfoReport_ContractInfo, _
                    holidaysAtEnd.Position, holidaysAtEnd.PersonName, holidaysAtEnd.PersonCode, _
                    holidaysAtEnd.ContractDate.ToString("yyyy-MM-dd"), _
                    holidaysAtEnd.ContractSerial, holidaysAtEnd.ContractNumber.ToString)

            End If

            _DateFrom = criteria.DateFrom
            _DateTo = criteria.DateTo
            _ContractNumber = criteria.ContractNumber
            _ContractSerial = criteria.ContractSerial
            _IsConsolidated = criteria.FetchConsolidatedInfo
            _PersonID = criteria.PersonID

        End Sub

#End Region

    End Class

End Namespace