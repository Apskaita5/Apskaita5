Namespace HelperLists

    ''' <summary>
    ''' Represents a list of <see cref="General.SharesClass">shares class</see> value objects.
    ''' </summary>
    ''' <remarks>Exists a single instance per company.</remarks>
    <Serializable()>
    Public NotInheritable Class SharesClassInfoList
        Inherits ReadOnlyListBase(Of SharesClassInfoList, SharesClassInfo)

#Region " Authorization Rules "

        Public Shared Function CanGetObject() As Boolean
            Return ApplicationContext.User.IsInRole("HelperLists.SharesClassInfoList1")
        End Function

#End Region

#Region " Factory Methods "


        ''' <summary>
        ''' Gets a current shares class value object list from database.
        ''' </summary>
        ''' <remarks>Result is cached.
        ''' Required by <see cref="AccDataAccessLayer.CacheManager">AccDataAccessLayer.CacheManager</see>.</remarks>
        Public Shared Function GetList() As SharesClassInfoList

            Dim result As SharesClassInfoList = CacheManager.GetItemFromCache(Of SharesClassInfoList)(
                GetType(SharesClassInfoList), Nothing)

            If result Is Nothing Then
                result = DataPortal.Fetch(Of SharesClassInfoList)(New Criteria())
                CacheManager.AddCacheItem(GetType(SharesClassInfoList), result, Nothing)
            End If

            Return result

        End Function

        ''' <summary>
        ''' Gets a filtered view of the current shares class value object list.
        ''' </summary>
        ''' <param name="showEmpty">Wheather to include a placeholder object.</param>
        ''' <remarks>Result is cached.
        ''' Required by <see cref="AccDataAccessLayer.CacheManager">AccDataAccessLayer.CacheManager</see>.</remarks>
        Public Shared Function GetCachedFilteredList(ByVal showEmpty As Boolean) As Csla.FilteredBindingList(Of SharesClassInfo)

            Dim filterToApply As Object() = New Object() {ConvertDbBoolean(showEmpty)}

            Dim result As Csla.FilteredBindingList(Of SharesClassInfo) =
                CacheManager.GetItemFromCache(Of Csla.FilteredBindingList(Of SharesClassInfo)) _
                (GetType(SharesClassInfoList), filterToApply)

            If result Is Nothing Then

                Dim BaseList As SharesClassInfoList = SharesClassInfoList.GetList
                result = New Csla.FilteredBindingList(Of SharesClassInfo)(BaseList, AddressOf SharesClassInfoListFilter)
                result.ApplyFilter("", filterToApply)
                CacheManager.AddCacheItem(GetType(SharesClassInfoList), result, filterToApply)

            End If

            Return result

        End Function

        ''' <summary>
        ''' Invalidates the current shares class value object list cache 
        ''' so that the next <see cref="GetList">GetList</see> call would hit the database.
        ''' </summary>
        ''' <remarks>Required by <see cref="AccDataAccessLayer.CacheManager">AccDataAccessLayer.CacheManager</see>.</remarks>
        Public Shared Sub InvalidateCache()
            CacheManager.InvalidateCache(GetType(SharesClassInfoList))
        End Sub

        ''' <summary>
        ''' Returnes true if the cache does not contain a current shares class value object list.
        ''' </summary>
        ''' <remarks>Required by <see cref="AccDataAccessLayer.CacheManager">AccDataAccessLayer.CacheManager</see>.</remarks>
        Public Shared Function CacheIsInvalidated() As Boolean
            Return CacheManager.CacheIsInvalidated(GetType(SharesClassInfoList))
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
        ''' Gets a current shares class value object list from database bypassing dataportal.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>Should only be called server side.
        ''' Required by <see cref="AccDataAccessLayer.CacheManager">AccDataAccessLayer.CacheManager</see>.</remarks>
        Private Shared Function GetListOnServer() As SharesClassInfoList
            Dim result As New SharesClassInfoList
            result.DataPortal_Fetch(New Criteria)
            Return result
        End Function

        ''' <summary>
        ''' Gets a current share class value object list from database bypassing dataportal.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>Should only be invoked server side.</remarks>
        Friend Shared Function GetListChild() As SharesClassInfoList
            Dim result As New SharesClassInfoList
            result.DataPortal_Fetch(New Criteria)
            Return result
        End Function


        Private Shared Function SharesClassInfoListFilter(ByVal item As Object, ByVal filterValue As Object) As Boolean

            If filterValue Is Nothing OrElse Not TypeOf filterValue Is Object() _
                OrElse Not DirectCast(filterValue, Object()).Length > 0 Then Return True

            Dim showEmpty As Boolean = ConvertDbBoolean(
                DirectCast(DirectCast(filterValue, Object())(0), Integer))

            ' no criteria to apply
            If showEmpty Then Return True

            Dim CI As SharesClassInfo = DirectCast(item, SharesClassInfo)

            If Not showEmpty AndAlso Not CI.ID > 0 Then Return False

            Return True

        End Function


        Private Sub New()
            ' require use of factory methods

        End Sub

#End Region

#Region " Data Access "

        <Serializable()>
        Private Class Criteria
            Public Sub New()
            End Sub
        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)

            If Not CanGetObject() Then Throw New System.Security.SecurityException(
                My.Resources.Common_SecuritySelectDenied)

            Dim myComm As New SQLCommand("FetchSharesClassInfoList")

            Using myData As DataTable = myComm.Fetch

                RaiseListChangedEvents = False
                IsReadOnly = False

                Add(SharesClassInfo.Empty)
                For Each dr As DataRow In myData.Rows
                    Add(SharesClassInfo.GetSharesClassInfo(dr, 0))
                Next

                IsReadOnly = True
                RaiseListChangedEvents = True

            End Using

        End Sub

#End Region

    End Class

End Namespace