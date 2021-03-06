﻿Imports ApskaitaObjects.Assets

Namespace ActiveReports

    ''' <summary>
    ''' Represents a report containing info about long term assets and their status.
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public NotInheritable Class LongTermAssetInfoList
        Inherits ReadOnlyListBase(Of LongTermAssetInfoList, LongTermAssetInfo)

#Region " Business Methods "

        Private _DateFrom As Date
        Private _DateTo As Date
        Private _CustomAssetGroup As LongTermAssetCustomGroupInfo


        ''' <summary>
        ''' Gets a start date of the report period.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DateFrom() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DateFrom.Date
            End Get
        End Property

        ''' <summary>
        ''' Gets an end date of the report period.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DateTo() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DateTo.Date
            End Get
        End Property

        ''' <summary>
        ''' Gets a <see cref="LongTermAssetCustomGroup">LongTermAssetCustomGroup</see> that the report is filtered by.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property CustomAssetGroup() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If _CustomAssetGroup Is Nothing Then Return "Visos grupės"
                Return _CustomAssetGroup.ToString
            End Get
        End Property


#End Region

#Region " Authorization Rules "

        Public Shared Function CanGetObject() As Boolean
            Return ApplicationContext.User.IsInRole("Assets.LongTermAssetInfoList1")
        End Function

#End Region

#Region " Factory Methods "

        ' used to implement automatic sort in datagridview
        <NonSerialized()> _
        Private _SortedList As Csla.SortedBindingList(Of LongTermAssetInfo) = Nothing

        ''' <summary>
        ''' Gets a new instance of the LongTermAssetInfoList report.
        ''' </summary>
        ''' <param name="nDateFrom">a start date of the report period</param>
        ''' <param name="nDateTo">an end date of the report period</param>
        ''' <param name="nCustomAssetGroup">a <see cref="LongTermAssetCustomGroup">LongTermAssetCustomGroup</see> to filter the report by</param>
        ''' <remarks></remarks>
        Public Shared Function GetLongTermAssetInfoList(ByVal nDateFrom As Date, _
            ByVal nDateTo As Date, ByVal nCustomAssetGroup As LongTermAssetCustomGroupInfo) As LongTermAssetInfoList
            Return DataPortal.Fetch(Of LongTermAssetInfoList) _
                (New Criteria(nDateFrom, nDateTo, nCustomAssetGroup))
        End Function

        ''' <summary>
        ''' Gets a sortable view of the report.
        ''' </summary>
        ''' <remarks>Used to implement auto sort in a datagridview.</remarks>
        Public Function GetSortedList() As Csla.SortedBindingList(Of LongTermAssetInfo)
            If _SortedList Is Nothing Then _SortedList = _
                New Csla.SortedBindingList(Of LongTermAssetInfo)(Me)
            Return _SortedList
        End Function

        ''' <summary>
        ''' Gets a filtered sortable list. 
        ''' </summary>
        ''' <param name="showEmpty">whether to include assets with zero amount</param>
        ''' <remarks></remarks>
        Public Function GetFilteredList(ByVal showEmpty As Boolean) As Csla.FilteredBindingList(Of LongTermAssetInfo)

            Dim sortedList As Csla.SortedBindingList(Of LongTermAssetInfo) = GetSortedList()

            Dim result As New FilteredBindingList(Of LongTermAssetInfo) _
                (sortedList, AddressOf LongTermAssetInfoFilter)

            Dim filter As Object() = New Object() {ConvertDbBoolean(False)}
            result.ApplyFilter("", filter)

            Return result

        End Function


        Private Shared Function LongTermAssetInfoFilter(ByVal item As Object, ByVal filterValue As Object) As Boolean

            If filterValue Is Nothing OrElse DirectCast(filterValue, Object()).Length < 1 Then Return True

            Dim showEmpty As Boolean = ConvertDbBoolean( _
                DirectCast(DirectCast(filterValue, Object())(0), Integer))

            ' no criteria to apply
            If showEmpty Then Return True

            Dim current As LongTermAssetInfo = DirectCast(item, LongTermAssetInfo)

            If Not showEmpty AndAlso current.ID > 0 AndAlso Not current.AfterAmmount > 0 Then Return False

            Return True

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
            Private _CustomAssetGroup As LongTermAssetCustomGroupInfo
            Public ReadOnly Property DateFrom() As Date
                Get
                    Return _DateFrom.Date
                End Get
            End Property
            Public ReadOnly Property DateTo() As Date
                Get
                    Return _DateTo.Date
                End Get
            End Property
            Public ReadOnly Property CustomAssetGroup() As LongTermAssetCustomGroupInfo
                <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
                Get
                    Return _CustomAssetGroup
                End Get
            End Property
            Public Sub New(ByVal nDateFrom As Date, ByVal nDateTo As Date, _
                ByVal nCustomAssetGroup As LongTermAssetCustomGroupInfo)
                _DateFrom = nDateFrom.Date
                _DateTo = nDateTo.Date
                _CustomAssetGroup = nCustomAssetGroup
            End Sub
        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)

            If Not CanGetObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecuritySelectDenied)

            Dim myComm As SQLCommand
            If criteria.CustomAssetGroup Is Nothing OrElse criteria.CustomAssetGroup.IsEmpty Then
                myComm = New SQLCommand("FetchLongTermAssetInfoAcquisitionData1")
            Else
                myComm = New SQLCommand("FetchLongTermAssetInfoAcquisitionData2")
                myComm.AddParam("?GR", criteria.CustomAssetGroup.ID)
            End If

            Using acquisitionDataTable As DataTable = myComm.Fetch

                If criteria.CustomAssetGroup Is Nothing OrElse criteria.CustomAssetGroup.IsEmpty Then
                    myComm = New SQLCommand("FetchLongTermAssetInfoStatusData1")
                Else
                    myComm = New SQLCommand("FetchLongTermAssetInfoStatusData2")
                    myComm.AddParam("?GR", criteria.CustomAssetGroup.ID)
                End If
                myComm.AddParam("?DF", criteria.DateFrom)

                Using statusBeforeDataTable As DataTable = myComm.Fetch

                    If criteria.CustomAssetGroup Is Nothing OrElse criteria.CustomAssetGroup.IsEmpty Then
                        myComm = New SQLCommand("FetchLongTermAssetInfoChangesData1")
                    Else
                        myComm = New SQLCommand("FetchLongTermAssetInfoChangesData2")
                        myComm.AddParam("?GR", criteria.CustomAssetGroup.ID)
                    End If
                    myComm.AddParam("?DF", criteria.DateFrom)
                    myComm.AddParam("?DT", criteria.DateTo)

                    Using changesDataTable As DataTable = myComm.Fetch

                        RaiseListChangedEvents = False
                        IsReadOnly = False

                        For Each dr As DataRow In acquisitionDataTable.Rows
                            Add(LongTermAssetInfo.GetLongTermAssetInfo(dr, _
                                GetDataRow(statusBeforeDataTable, CIntSafe(dr.Item(0))), _
                                GetDataRow(changesDataTable, CIntSafe(dr.Item(0))), _
                                criteria.DateFrom, criteria.DateTo))
                        Next

                        _CustomAssetGroup = criteria.CustomAssetGroup
                        _DateFrom = criteria.DateFrom
                        _DateTo = criteria.DateTo

                        IsReadOnly = True
                        RaiseListChangedEvents = True

                    End Using

                End Using

            End Using

        End Sub


        Private Function GetDataRow(ByVal assetInfoDataTable As DataTable, _
            ByVal nAssetID As Integer) As DataRow

            For i As Integer = 1 To assetInfoDataTable.Rows.Count
                If CInt(assetInfoDataTable.Rows(i - 1).Item(0)) = nAssetID Then _
                    Return assetInfoDataTable.Rows(i - 1)
            Next

            Return Nothing

        End Function

#End Region

    End Class

End Namespace