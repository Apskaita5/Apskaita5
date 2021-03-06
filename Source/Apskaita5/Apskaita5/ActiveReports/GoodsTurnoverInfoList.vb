﻿Namespace ActiveReports

    ''' <summary>
    ''' Represents a goods turnover report, contains information about 
    ''' aggregated turnover of goods within the report period 
    ''' subject to the report filter criteria.
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public NotInheritable Class GoodsTurnoverInfoList
        Inherits ReadOnlyListBase(Of GoodsTurnoverInfoList, GoodsTurnoverInfo)

#Region " Business Methods "

        Private _DateFrom As Date = Today
        Private _DateTo As Date = Today
        Private _Group As GoodsGroupInfo = Nothing
        Private _Warehouse As WarehouseInfo = Nothing
        Private _TextInNameOrCode As String = ""
        Private _IncludeWithoutTurnover As Boolean = False


        ''' <summary>
        ''' Gets the starting date of the report period.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DateFrom() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DateFrom
            End Get
        End Property

        ''' <summary>
        ''' Gets the ending date of the report period.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DateTo() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DateTo
            End Get
        End Property

        ''' <summary>
        ''' Gets the goods group that the report is filtered by (if any).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Group() As GoodsGroupInfo
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Group
            End Get
        End Property

        ''' <summary>
        ''' Gets the warehouse that the report is filtered by (if any).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Warehouse() As WarehouseInfo
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Warehouse
            End Get
        End Property

        ''' <summary>
        ''' Gets the text in goods name or goods code that the report is filtered by (if any).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property TextInNameOrCode() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _TextInNameOrCode
            End Get
        End Property

        ''' <summary>
        ''' Gets whether the goods <see cref="GoodsTurnoverInfo.HasTurnover">
        ''' without turnover</see> are included in the report.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property IncludeWithoutTurnover() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IncludeWithoutTurnover
            End Get
        End Property

#End Region

#Region " Authorization Rules "

        Public Shared Function CanGetObject() As Boolean
            Return ApplicationContext.User.IsInRole("Goods.GoodsTurnoverInfoList1")
        End Function

#End Region

#Region " Factory Methods "

        ' used to implement automatic sort in datagridview
        <NonSerialized()> _
        Private _SortedList As Csla.SortedBindingList(Of GoodsTurnoverInfo) = Nothing


        ''' <summary>
        ''' Gets a new instance of the goods turnover report.
        ''' </summary>
        ''' <param name="dateFrom">starting date of the report period</param>
        ''' <param name="dateTo">ending date of the report period</param>
        ''' <param name="group">goods group that the report is filtered by (if any)</param>
        ''' <param name="warehouse">warehouse that the report is filtered by (if any)</param>
        ''' <param name="textInNameOrCode">text in goods name or goods code 
        ''' that the report is filtered by (if any)</param>
        ''' <param name="includeWithoutTurnover">whether the goods without turnover 
        ''' are included in the report</param>
        ''' <remarks></remarks>
        Public Shared Function GetGoodsTurnoverInfoList(ByVal dateFrom As Date, _
            ByVal dateTo As Date, ByVal group As GoodsGroupInfo, _
            ByVal warehouse As WarehouseInfo, ByVal textInNameOrCode As String, _
            ByVal includeWithoutTurnover As Boolean) As GoodsTurnoverInfoList
            Dim result As GoodsTurnoverInfoList = DataPortal.Fetch(Of GoodsTurnoverInfoList) _
                (New Criteria(dateFrom, dateTo, group, warehouse, textInNameOrCode, includeWithoutTurnover))
            Return result
        End Function

        ''' <summary>
        ''' Gets a sortable view of the report.
        ''' </summary>
        ''' <remarks>Used to implement auto sort in a datagridview.</remarks>
        Public Function GetSortedList() As Csla.SortedBindingList(Of GoodsTurnoverInfo)
            If _SortedList Is Nothing Then
                _SortedList = New Csla.SortedBindingList(Of GoodsTurnoverInfo)(Me)
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
            Private _DateFrom As Date = Today
            Private _DateTo As Date = Today
            Private _Group As GoodsGroupInfo = Nothing
            Private _Warehouse As WarehouseInfo = Nothing
            Private _TextInNameOrCode As String = ""
            Private _IncludeWithoutTurnover As Boolean = False
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
            Public ReadOnly Property Group() As GoodsGroupInfo
                Get
                    Return _Group
                End Get
            End Property
            Public ReadOnly Property Warehouse() As WarehouseInfo
                Get
                    Return _Warehouse
                End Get
            End Property
            Public ReadOnly Property TextInNameOrCode() As String
                Get
                    Return _TextInNameOrCode
                End Get
            End Property
            Public ReadOnly Property IncludeWithoutTurnover() As Boolean
                <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
                Get
                    Return _IncludeWithoutTurnover
                End Get
            End Property
            Public Sub New(ByVal nDateFrom As Date, ByVal nDateTo As Date, ByVal nGroup As GoodsGroupInfo, _
                ByVal nWarehouse As WarehouseInfo, ByVal nTextInNameOrCode As String, _
                ByVal nIncludeWithoutTurnover As Boolean)
                _DateFrom = nDateFrom
                _DateTo = nDateTo
                _Group = nGroup
                _Warehouse = nWarehouse
                _Group = nGroup
                _TextInNameOrCode = nTextInNameOrCode
                _IncludeWithoutTurnover = nIncludeWithoutTurnover
            End Sub
        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)

            If Not CanGetObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecuritySelectDenied)

            Dim myComm As New SQLCommand("FetchGoodsTurnoverInfoList")
            myComm.AddParam("?DF", criteria.DateFrom.Date)
            myComm.AddParam("?DT", criteria.DateTo.Date)
            If Not criteria.Group Is Nothing AndAlso Not criteria.Group.IsEmpty Then
                myComm.AddParam("?GD", criteria.Group.ID)
            Else
                myComm.AddParam("?GD", 0)
            End If
            If Not criteria.Warehouse Is Nothing AndAlso Not criteria.Warehouse.IsEmpty Then
                myComm.AddParam("?WD", criteria.Warehouse.ID)
            Else
                myComm.AddParam("?WD", 0)
            End If
            Dim curWildCard As String = GetWildcart()
            If StringIsNullOrEmpty(criteria.TextInNameOrCode) Then
                myComm.AddParam("?TX", curWildCard)
            Else
                myComm.AddParam("?TX", String.Format("{0}{1}{2}", curWildCard, _
                    criteria.TextInNameOrCode.Trim, curWildCard))
            End If

            Using myData As DataTable = myComm.Fetch

                RaiseListChangedEvents = False
                IsReadOnly = False

                For Each dr As DataRow In myData.Rows
                    Dim newItem As GoodsTurnoverInfo = GoodsTurnoverInfo.GetGoodsTurnoverInfo(dr)
                    If criteria.IncludeWithoutTurnover OrElse newItem.HasTurnover Then
                        Add(newItem)
                    End If
                Next

                _DateFrom = criteria.DateFrom
                _DateTo = criteria.DateTo
                _Group = criteria.Group
                _Warehouse = criteria.Warehouse
                If criteria.TextInNameOrCode Is Nothing Then
                    _TextInNameOrCode = ""
                Else
                    _TextInNameOrCode = criteria.TextInNameOrCode
                End If
                _IncludeWithoutTurnover = criteria.IncludeWithoutTurnover

                IsReadOnly = True
                RaiseListChangedEvents = True

            End Using

        End Sub

#End Region

    End Class

End Namespace