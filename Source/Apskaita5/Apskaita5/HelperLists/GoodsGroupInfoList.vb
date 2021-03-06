﻿Namespace HelperLists

    ''' <summary>
    ''' Represents a list of <see cref="Goods.GoodsGroup">custom goods group</see> value objects.
    ''' </summary>
    ''' <remarks>Exists a single instance per company.</remarks>
    <Serializable()> _
Public NotInheritable Class GoodsGroupInfoList
        Inherits ReadOnlyListBase(Of GoodsGroupInfoList, GoodsGroupInfo)

#Region " Business Methods "

        ''' <summary>
        ''' Gets a custom goods group info instance by the group ID.
        ''' Returns null if there is no custom goods group with the id value requested.
        ''' </summary>
        ''' <param name="id">a <see cref="Goods.GoodsGroup.ID">group id</see> to look for</param>
        ''' <remarks></remarks>
        Public Function GetItem(ByVal id As Integer) As GoodsGroupInfo
            If Not id > 0 Then Return Nothing
            For Each i As GoodsGroupInfo In Me
                If i.ID = id Then Return i
            Next
            Return Nothing
        End Function

        ''' <summary>
        ''' Gets a custom goods group info instance by the group name.
        ''' Returns null if there is no custom goods group with the name requested.
        ''' </summary>
        ''' <param name="name">a <see cref="Goods.GoodsGroup.Name">group name</see> to look for</param>
        ''' <remarks></remarks>
        Public Function GetItem(ByVal name As String) As GoodsGroupInfo
            If StringIsNullOrEmpty(name) Then Return Nothing
            For Each i As GoodsGroupInfo In Me
                If i.Name.Trim.ToLower = name.Trim.ToLower Then Return i
            Next
            Return Nothing
        End Function

#End Region

#Region " Authorization Rules "

        Public Shared Function CanGetObject() As Boolean
            Return ApplicationContext.User.IsInRole("HelperLists.GoodsGroupInfoList1")
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets a current custom goods group info value object list from database.
        ''' </summary>
        ''' <remarks>Result is cached.
        ''' Required by <see cref="AccDataAccessLayer.CacheManager">AccDataAccessLayer.CacheManager</see>.</remarks>
        Public Shared Function GetList() As GoodsGroupInfoList

            Dim result As GoodsGroupInfoList = CacheManager.GetItemFromCache(Of GoodsGroupInfoList)( _
                GetType(GoodsGroupInfoList), Nothing)

            If result Is Nothing Then
                result = DataPortal.Fetch(Of GoodsGroupInfoList)(New Criteria())
                CacheManager.AddCacheItem(GetType(GoodsGroupInfoList), result, Nothing)
            End If

            Return result

        End Function

        ''' <summary>
        ''' Gets a filtered view of the current custom goods group info value object list.
        ''' </summary>
        ''' <param name="showEmpty">Wheather to include a placeholder object.</param>
        ''' <remarks>Result is cached.
        ''' Required by <see cref="AccDataAccessLayer.CacheManager">AccDataAccessLayer.CacheManager</see>.</remarks>
        Public Shared Function GetCachedFilteredList(ByVal showEmpty As Boolean) _
            As Csla.FilteredBindingList(Of GoodsGroupInfo)

            Dim filterToApply(0) As Object
            filterToApply(0) = ConvertDbBoolean(showEmpty)

            Dim result As Csla.FilteredBindingList(Of GoodsGroupInfo) = _
                CacheManager.GetItemFromCache(Of Csla.FilteredBindingList(Of GoodsGroupInfo)) _
                (GetType(GoodsGroupInfoList), filterToApply)

            If result Is Nothing Then

                Dim baseList As GoodsGroupInfoList = GoodsGroupInfoList.GetList
                result = New Csla.FilteredBindingList(Of GoodsGroupInfo) _
                    (New Csla.SortedBindingList(Of GoodsGroupInfo)(baseList), _
                    AddressOf GoodsGroupInfoListFilter)
                result.ApplyFilter("", filterToApply)
                CacheManager.AddCacheItem(GetType(GoodsGroupInfoList), result, filterToApply)

            End If

            Return result

        End Function

        ''' <summary>
        ''' Invalidates the current custom goods group info value object list cache 
        ''' so that the next <see cref="GetList">GetList</see> call would hit the database.
        ''' </summary>
        ''' <remarks>Required by <see cref="AccDataAccessLayer.CacheManager">AccDataAccessLayer.CacheManager</see>.</remarks>
        Public Shared Sub InvalidateCache()
            CacheManager.InvalidateCache(GetType(GoodsGroupInfoList))
        End Sub

        ''' <summary>
        ''' Returnes true if the cache does not contain a current custom goods group 
        ''' info value object list.
        ''' </summary>
        ''' <remarks>Required by <see cref="AccDataAccessLayer.CacheManager">AccDataAccessLayer.CacheManager</see>.</remarks>
        Public Shared Function CacheIsInvalidated() As Boolean
            Return CacheManager.CacheIsInvalidated(GetType(GoodsGroupInfoList))
        End Function

        ''' <summary>
        ''' Returns true if the collection is common across all the databases.
        ''' I.e. cache is not to be cleared on changing databases.
        ''' </summary>
        ''' <remarks>Required by <see cref="AccDataAccessLayer.CacheManager">AccDataAccessLayer.CacheManager</see>.</remarks>
        Private Shared Function IsApplicationWideCache() As Boolean
            Return False
        End Function

        ''' <summary>
        ''' Gets a current custom goods group info value object list from a database 
        ''' bypassing dataportal.
        ''' </summary>
        ''' <remarks>Should only be called server side.
        ''' Required by <see cref="AccDataAccessLayer.CacheManager">AccDataAccessLayer.CacheManager</see>.</remarks>
        Private Shared Function GetListOnServer() As GoodsGroupInfoList
            Dim result As New GoodsGroupInfoList
            result.DataPortal_Fetch(New Criteria)
            Return result
        End Function

        ''' <summary>
        ''' Gets a current custom goods group info value object list from a database 
        ''' bypassing dataportal.
        ''' </summary>
        ''' <remarks>Should only be called server side.</remarks>
        Friend Shared Function GetGoodsGroupInfoListChild() As GoodsGroupInfoList
            Dim result As New GoodsGroupInfoList
            result.DataPortal_Fetch(New Criteria)
            Return result
        End Function


        Private Shared Function GoodsGroupInfoListFilter(ByVal item As Object, _
            ByVal filterValue As Object) As Boolean

            If filterValue Is Nothing OrElse Not TypeOf filterValue Is Object() _
                OrElse Not DirectCast(filterValue, Object()).Length > 0 Then Return True

            Dim showEmpty As Boolean = ConvertDbBoolean( _
                DirectCast(DirectCast(filterValue, Object())(0), Integer))

            ' no criteria to apply
            If showEmpty Then Return True

            Dim g As GoodsGroupInfo = DirectCast(item, GoodsGroupInfo)

            If Not showEmpty AndAlso Not g.ID > 0 Then Return False

            Return True

        End Function


        Private Sub New()
            ' require use of factory methods
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> _
        Private Class Criteria
            Public Sub New()
            End Sub
        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)

            If Not CanGetObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecuritySelectDenied)

            RaiseListChangedEvents = False
            IsReadOnly = False

            Dim myComm As New SQLCommand("FetchGoodsGroupInfoList")

            Using myData As DataTable = myComm.Fetch

                Add(GoodsGroupInfo.Empty())

                For Each dr As DataRow In myData.Rows
                    Add(GoodsGroupInfo.GetGoodsGroupInfo(dr))
                Next

            End Using

            IsReadOnly = True
            RaiseListChangedEvents = True

        End Sub

#End Region

    End Class

End Namespace