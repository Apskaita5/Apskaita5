﻿Namespace ActiveReports

    ''' <summary>
    ''' Represents a work time sheet report. Contains information about work time sheets.
    ''' </summary>
    ''' <remarks>Values are stored in the database tables worktimesheets, dayworktimes, worktimeitems and specialworktimeitems.</remarks>
    <Serializable()> _
    Public NotInheritable Class WorkTimeSheetInfoList
        Inherits ReadOnlyListBase(Of WorkTimeSheetInfoList, WorkTimeSheetInfo)

#Region " Business Methods "

        Private _DateFrom As Date = Today
        Private _DateTo As Date = Today

        ''' <summary>
        ''' Minimum date of a sheet within the report.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DateFrom() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DateFrom
            End Get
        End Property

        ''' <summary>
        ''' Maximum date of a sheet within the report.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DateTo() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DateTo
            End Get
        End Property

#End Region

#Region " Authorization Rules "

        Public Shared Function CanGetObject() As Boolean
            Return ApplicationContext.User.IsInRole("Workers.WorkTimeSheetInfoList1")
        End Function

#End Region

#Region " Factory Methods "

        ' used to implement automatic sort in datagridview
        <NonSerialized()> _
        Private _SortedList As Csla.SortedBindingList(Of WorkTimeSheetInfo) = Nothing

        ''' <summary>
        ''' Gets a new instance of the WorkTimeSheetInfoList report.
        ''' </summary>
        ''' <param name="dateFrom">Minimum date of a sheet within the report.</param>
        ''' <param name="dateTo">Maximum date of a sheet within the report.</param>
        ''' <remarks></remarks>
        Public Shared Function GetWorkTimeSheetInfoList(ByVal dateFrom As Date, _
            ByVal dateTo As Date) As WorkTimeSheetInfoList
            Return DataPortal.Fetch(Of WorkTimeSheetInfoList)(New Criteria(dateFrom, dateTo))
        End Function

        ''' <summary>
        ''' Gets a sortable view of the report.
        ''' </summary>
        ''' <remarks>Used to implement auto sort in a datagridview.</remarks>
        Public Function GetSortedList() As Csla.SortedBindingList(Of WorkTimeSheetInfo)
            If _SortedList Is Nothing Then _SortedList = New Csla.SortedBindingList(Of WorkTimeSheetInfo)(Me)
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
            Public Sub New(ByVal nDateFrom As Date, ByVal nDateTo As Date)
                _DateFrom = nDateFrom
                _DateTo = nDateTo
            End Sub
        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)

            If Not CanGetObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecuritySelectDenied)

            Dim myComm As New SQLCommand("FetchWorkTimeSheetInfoList")
            myComm.AddParam("?DF", criteria.DateFrom)
            myComm.AddParam("?DT", criteria.DateTo)

            Using myData As DataTable = myComm.Fetch

                RaiseListChangedEvents = False
                IsReadOnly = False

                For Each dr As DataRow In myData.Rows
                    Add(WorkTimeSheetInfo.GetWorkTimeSheetInfo(dr))
                Next

                _DateFrom = criteria.DateFrom
                _DateTo = criteria.DateTo

                IsReadOnly = True
                RaiseListChangedEvents = True

            End Using

        End Sub

#End Region

    End Class

End Namespace