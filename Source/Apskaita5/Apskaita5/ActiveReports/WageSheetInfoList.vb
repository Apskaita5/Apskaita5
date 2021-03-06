﻿Namespace ActiveReports

    ''' <summary>
    ''' Represents a wage sheet report. Contains information about <see cref="Workers.WageSheet">WageSheets</see>.
    ''' </summary>
    ''' <remarks>Values are stored in the database tables du_ziniarastis and du_ziniarastis_d.</remarks>
    <Serializable()> _
    Public NotInheritable Class WageSheetInfoList
        Inherits ReadOnlyListBase(Of WageSheetInfoList, WageSheetInfo)

#Region " Business Methods "

        Private _DateFrom As Date
        Private _DateTo As Date
        Private _ShowPayedOut As Boolean = True
        Private _Worker As PersonInfo = Nothing
        Private _TotalSumAfterDeductions As Double
        Private _TotalSumPayedOut As Double

        ''' <summary>
        ''' Minimum date of a wage sheet within the report.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DateFrom() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DateFrom
            End Get
        End Property

        ''' <summary>
        ''' Maximum date of a wage sheet within the report.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DateTo() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DateTo
            End Get
        End Property

        ''' <summary>
        ''' Whether to show information about wage sheets where all the wage is payed out to the workers. 
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property ShowPayedOut() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ShowPayedOut
            End Get
        End Property

        ''' <summary>
        ''' A worker that the report is filtered by.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Worker() As PersonInfo
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Worker
            End Get
        End Property

        ''' <summary>
        ''' Total sum of the wage after deductions within the report.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property TotalSumAfterDeductions() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _TotalSumAfterDeductions
            End Get
        End Property

        ''' <summary>
        ''' Total sum of the wage, that was payed to the workers, within the report.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property TotalSumPayedOut() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _TotalSumPayedOut
            End Get
        End Property


        Private Sub RecalculateSubTotals()

            _TotalSumAfterDeductions = 0
            _TotalSumPayedOut = 0

            For Each i As WageSheetInfo In Me
                If _ShowPayedOut OrElse Not i.IsPayedOut Then
                    _TotalSumAfterDeductions = CRound(_TotalSumAfterDeductions + i.PayOutAfterDeductions)
                    _TotalSumPayedOut = CRound(_TotalSumPayedOut + i.PayedOut)
                End If
            Next

        End Sub

#End Region

#Region " Authorization Rules "

        Public Shared Function CanGetObject() As Boolean
            Return ApplicationContext.User.IsInRole("Workers.WageSheetInfoList1")
        End Function

#End Region

#Region " Factory Methods "

        ' used to implement automatic sort in datagridview
        <NonSerialized()> _
        Private _SortedList As Csla.SortedBindingList(Of WageSheetInfo) = Nothing
        
        ''' <summary>
        ''' Gets a new instance of wage sheet report.
        ''' </summary>
        ''' <param name="datefrom">Minimum date of a wage sheet within the report.</param>
        ''' <param name="dateTo">Maximum date of a wage sheet within the report.</param>
        ''' <remarks></remarks>
        Public Shared Function GetWageSheetInfoList(ByVal datefrom As Date, _
            ByVal dateTo As Date, ByVal worker As PersonInfo, _
            ByVal showPayedOut As Boolean) As WageSheetInfoList
            Return DataPortal.Fetch(Of WageSheetInfoList)(New Criteria( _
                datefrom, dateTo, worker, showPayedOut))
        End Function

        ''' <summary>
        ''' Gets a sortable view of the report.
        ''' </summary>
        ''' <remarks>Used to implement auto sort in a datagridview.</remarks>
        Public Function GetSortedList() As Csla.SortedBindingList(Of WageSheetInfo)

            If _SortedList Is Nothing Then
                _SortedList = New Csla.SortedBindingList(Of WageSheetInfo)(Me)
            End If

            Return _SortedList

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
            Private _ShowPayedOut As Boolean = True
            Private _Worker As PersonInfo = Nothing
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
            Public ReadOnly Property Worker() As PersonInfo
                Get
                    Return _Worker
                End Get
            End Property
            Public ReadOnly Property ShowPayedOut() As Boolean
                Get
                    Return _ShowPayedOut
                End Get
            End Property
            Public Sub New(ByVal nDateFrom As Date, ByVal nDateTo As Date, _
                ByVal nWorker As PersonInfo, ByVal nShowPayedOut As Boolean)
                _DateFrom = nDateFrom
                _DateTo = nDateTo
                _Worker = nWorker
                _ShowPayedOut = nShowPayedOut
            End Sub
        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)

            If Not CanGetObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecuritySelectDenied)

            Dim myComm As New SQLCommand("FetchWageSheetInfoList")
            myComm.AddParam("?YF", criteria.DateFrom.Year)
            myComm.AddParam("?MF", criteria.DateFrom.Month)
            myComm.AddParam("?YT", criteria.DateTo.Year)
            myComm.AddParam("?MT", criteria.DateTo.Month)
            If criteria.Worker Is Nothing OrElse criteria.Worker.IsEmpty Then
                myComm.AddParam("?PD", 0)
            Else
                myComm.AddParam("?PD", criteria.Worker.ID)
            End If

            Using myData As DataTable = myComm.Fetch

                RaiseListChangedEvents = False
                IsReadOnly = False

                _TotalSumAfterDeductions = 0
                _TotalSumPayedOut = 0

                For Each dr As DataRow In myData.Rows

                    Dim itemToAdd As WageSheetInfo = WageSheetInfo.GetWageSheetInfo(dr)

                    If Not itemToAdd.IsPayedOut OrElse criteria.ShowPayedOut Then
                        Add(itemToAdd)
                        _TotalSumAfterDeductions = CRound(_TotalSumAfterDeductions + itemToAdd.PayOutAfterDeductions)
                        _TotalSumPayedOut = CRound(_TotalSumPayedOut + itemToAdd.PayedOut)
                    End If

                Next

                _DateFrom = criteria.DateFrom
                _DateTo = criteria.DateTo
                _ShowPayedOut = criteria.ShowPayedOut
                If criteria.Worker Is Nothing Then
                    _Worker = PersonInfo.Empty()
                Else
                    _Worker = criteria.Worker
                End If

                IsReadOnly = True
                RaiseListChangedEvents = True

            End Using

        End Sub

#End Region

    End Class

End Namespace