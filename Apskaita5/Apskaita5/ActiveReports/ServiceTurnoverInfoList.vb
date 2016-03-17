Imports System.Security
Imports ApskaitaObjects.Documents

Namespace ActiveReports

    ''' <summary>
    ''' Represents a service turnover report. Contains information about services,
    ''' their sales and purchases over the report period.
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public Class ServiceTurnoverInfoList
        Inherits ReadOnlyListBase(Of ServiceTurnoverInfoList, ServiceTurnoverInfo)

#Region " Business Methods "

        Private _DateFrom As Date = Today
        Private _DateTo As Date = Today
        Private _ShowWithoutTurnover As Boolean = True
        Private _TradedType As TradedItemType = TradedItemType.All


        ''' <summary>
        ''' Starting date of the report period.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DateFrom() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DateFrom
            End Get
        End Property

        ''' <summary>
        ''' Ending date of the report period.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DateTo() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DateTo
            End Get
        End Property

        ''' <summary>
        ''' Whether to show information about services without turnover. 
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property ShowWithoutTurnover() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ShowWithoutTurnover
            End Get
        End Property

        ''' <summary>
        ''' Gets a trade type filter used to filter the report. 
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property TradedTypeInt() As TradedItemType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _TradedType
            End Get
        End Property

        ''' <summary>
        ''' Gets a trade type filter used to filter the report as a human readable localized string. 
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property TradedType() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return Utilities.ConvertLocalizedName(_TradedType)
            End Get
        End Property

#End Region

#Region " Authorization Rules "

        Public Shared Function CanGetObject() As Boolean
            Return ApplicationContext.User.IsInRole("ActiveReports.ServiceTurnoverInfoList1")
        End Function

#End Region

#Region " Factory Methods "

        ' used to implement automatic sort in datagridview
        <NonSerialized()> _
        Private _SortedList As Csla.SortedBindingList(Of ServiceTurnoverInfo) = Nothing

        Public Shared Function GetServiceTurnoverInfoList(ByVal dateFrom As Date, _
            ByVal dateTo As Date, ByVal showWithoutTurnover As Boolean, _
            ByVal tradedType As TradedItemType) As ServiceTurnoverInfoList
            Return DataPortal.Fetch(Of ServiceTurnoverInfoList) _
                (New Criteria(dateFrom, dateTo, showWithoutTurnover, tradedType))
        End Function


        ''' <summary>
        ''' Gets a sortable view of the report.
        ''' </summary>
        ''' <remarks>Used to implement auto sort in a datagridview.</remarks>
        Public Function GetSortedList() As Csla.SortedBindingList(Of ServiceTurnoverInfo)

            If _SortedList Is Nothing Then
                _SortedList = New Csla.SortedBindingList(Of ServiceTurnoverInfo)(Me)
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
            Private _ShowWithoutTurnover As Boolean
            Private _TradedType As TradedItemType
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
            Public ReadOnly Property ShowWithoutTurnover() As Boolean
                Get
                    Return _ShowWithoutTurnover
                End Get
            End Property
            Public ReadOnly Property TradedType() As TradedItemType
                Get
                    Return _TradedType
                End Get
            End Property
            Public Sub New(ByVal nDateFrom As Date, ByVal nDateTo As Date, _
                ByVal nShowWithoutTurnover As Boolean, ByVal nTradedType As TradedItemType)
                _DateFrom = nDateFrom
                _DateTo = nDateTo
                _ShowWithoutTurnover = nShowWithoutTurnover
                _TradedType = nTradedType
            End Sub
        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)

            If Not CanGetObject() Then Throw New SecurityException( _
                My.Resources.Common_SecuritySelectDenied)

            Dim myComm As New SQLCommand("FetchServiceTurnoverInfoList")
            myComm.AddParam("?DF", criteria.DateFrom)
            myComm.AddParam("?DT", criteria.DateTo)

            Using myData As DataTable = myComm.Fetch

                RaiseListChangedEvents = False
                IsReadOnly = False

                For Each dr As DataRow In myData.Rows
                    Dim newItem As ServiceTurnoverInfo = ServiceTurnoverInfo. _
                        GetServiceTurnoverInfo(dr)
                    If (criteria.ShowWithoutTurnover OrElse newItem.HasTurnover()) _
                        AndAlso newItem.MatchesTradedType(criteria.TradedType) Then
                        Add(newItem)
                    End If
                Next

                _DateFrom = criteria.DateFrom
                _DateTo = criteria.DateTo
                _ShowWithoutTurnover = criteria.ShowWithoutTurnover
                _TradedType = criteria.TradedType

                IsReadOnly = True
                RaiseListChangedEvents = True

            End Using

        End Sub

#End Region

    End Class

End Namespace