Namespace HelperLists

    ''' <summary>
    ''' Represents a list of <see cref="Documents.VatDeclarationSchema">VAT declaration schema</see> value objects.
    ''' </summary>
    ''' <remarks>Exists a single instance per company.</remarks>
    <Serializable()> _
    Public NotInheritable Class VatDeclarationSchemaInfoList
        Inherits ReadOnlyListBase(Of VatDeclarationSchemaInfoList, VatDeclarationSchemaInfo)

#Region " Business Methods "

        ''' <summary>
        ''' Gets a <see cref="VatDeclarationSchemaInfo">VatDeclarationSchemaInfo</see> instance
        ''' by it's <see cref="VatDeclarationSchemaInfo.ExternalCode">ExternalCode</see>.
        ''' Returns nothing if not found.
        ''' </summary>
        ''' <param name="externalCode"><see cref="VatDeclarationSchemaInfo.ExternalCode">ExternalCode</see> to look for</param>
        ''' <remarks></remarks>
        Public Function GetItem(ByVal externalCode As String) As VatDeclarationSchemaInfo
            If StringIsNullOrEmpty(externalCode) Then Return Nothing
            For Each s As VatDeclarationSchemaInfo In Me
                If s.ExternalCode.Trim = externalCode.Trim Then Return s
            Next
            Return Nothing
        End Function

#End Region

#Region " Authorization Rules "

        Public Shared Function CanGetObject() As Boolean
            Return ApplicationContext.User.IsInRole("HelperLists.VatDeclarationSchemaInfoList1")
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets a current VAT declaration schema info value object list from database.
        ''' </summary>
        ''' <remarks>Result is cached.
        ''' Required by <see cref="AccDataAccessLayer.CacheManager">AccDataAccessLayer.CacheManager</see>.</remarks>
        Public Shared Function GetList() As VatDeclarationSchemaInfoList

            Dim result As VatDeclarationSchemaInfoList = CacheManager.GetItemFromCache(Of VatDeclarationSchemaInfoList)( _
                GetType(VatDeclarationSchemaInfoList), Nothing)

            If result Is Nothing Then
                result = DataPortal.Fetch(Of VatDeclarationSchemaInfoList)(New Criteria())
                CacheManager.AddCacheItem(GetType(VatDeclarationSchemaInfoList), result, Nothing)
            End If

            Return result

        End Function

        ''' <summary>
        ''' Gets a filtered view of the current VAT declaration schema info value object list.
        ''' </summary>
        ''' <param name="showEmpty">Wheather to include a placeholder object.</param>
        ''' <param name="tradedType">a type of schemas to include</param>
        ''' <param name="usedObjectsIds"><see cref="VatDeclarationSchemaInfo.ID">id's of the schemas</see> that are
        ''' used in the business object, i.e. should be included even if obsolete</param>
        ''' <param name="showObsolete">Wheather to include schemas that are obsolete (no loger in use).</param>
        ''' <remarks>Result is cached.
        ''' Required by <see cref="AccDataAccessLayer.CacheManager">AccDataAccessLayer.CacheManager</see>.</remarks>
        Public Shared Function GetCachedFilteredList(ByVal showEmpty As Boolean, _
            ByVal showObsolete As Boolean, ByVal tradedType As Documents.TradedItemType, _
            ByVal usedObjectsIds As List(Of String)) As Csla.FilteredBindingList(Of VatDeclarationSchemaInfo)

            Dim filterToApply(3) As Object
            filterToApply(0) = showObsolete
            filterToApply(1) = showEmpty
            filterToApply(2) = tradedType
            filterToApply(3) = usedObjectsIds

            Dim result As Csla.FilteredBindingList(Of VatDeclarationSchemaInfo) = _
                CacheManager.GetItemFromCache(Of Csla.FilteredBindingList(Of VatDeclarationSchemaInfo)) _
                (GetType(VatDeclarationSchemaInfoList), filterToApply)

            If result Is Nothing Then

                Dim baseList As VatDeclarationSchemaInfoList = VatDeclarationSchemaInfoList.GetList
                result = New Csla.FilteredBindingList(Of VatDeclarationSchemaInfo)(baseList, _
                    AddressOf VatDeclarationSchemaInfoFilter)
                result.ApplyFilter("", filterToApply)
                CacheManager.AddCacheItem(GetType(VatDeclarationSchemaInfoList), result, filterToApply)

            End If

            Return result

        End Function

        ''' <summary>
        ''' Invalidates the current VAT declaration schema info value object list cache 
        ''' so that the next <see cref="GetList">GetList</see> call would hit the database.
        ''' </summary>
        ''' <remarks>Required by <see cref="AccDataAccessLayer.CacheManager">AccDataAccessLayer.CacheManager</see>.</remarks>
        Public Shared Sub InvalidateCache()
            CacheManager.InvalidateCache(GetType(VatDeclarationSchemaInfoList))
        End Sub

        ''' <summary>
        ''' Returnes true if the cache does not contain a current VAT declaration schema info value object list.
        ''' </summary>
        ''' <remarks>Required by <see cref="AccDataAccessLayer.CacheManager">AccDataAccessLayer.CacheManager</see>.</remarks>
        Public Shared Function CacheIsInvalidated() As Boolean
            Return CacheManager.CacheIsInvalidated(GetType(VatDeclarationSchemaInfoList))
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
        ''' Gets a current VAT declaration schema info value object list from database bypassing dataportal.
        ''' </summary>
        ''' <remarks>Should only be called server side.
        ''' Required by <see cref="AccDataAccessLayer.CacheManager">AccDataAccessLayer.CacheManager</see>.</remarks>
        Private Shared Function GetListOnServer() As VatDeclarationSchemaInfoList
            Dim result As New VatDeclarationSchemaInfoList
            result.DataPortal_Fetch(New Criteria)
            Return result
        End Function

        ''' <summary>
        ''' Gets a current VAT declaration schema info value object list from database bypassing dataportal.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>Should only be invoked server side.</remarks>
        Public Shared Function GetListChild() As VatDeclarationSchemaInfoList
            Dim result As New VatDeclarationSchemaInfoList
            result.DataPortal_Fetch(New Criteria)
            Return result
        End Function


        Private Shared Function VatDeclarationSchemaInfoFilter(ByVal item As Object, ByVal filterValue As Object) As Boolean

            If filterValue Is Nothing OrElse DirectCast(filterValue, Object()).Length < 4 Then Return True

            Dim filterArray As Object() = DirectCast(filterValue, Object())

            Dim showObsolete As Boolean = DirectCast(filterArray(0), Boolean)
            Dim showEmpty As Boolean = DirectCast(filterArray(1), Boolean)
            Dim tradedType As Documents.TradedItemType = _
                DirectCast(filterArray(2), Documents.TradedItemType)
            Dim usedObjectsIds As List(Of String) = DirectCast(filterArray(3), List(Of String))

            ' no criteria to apply
            If showEmpty AndAlso showObsolete AndAlso _
                tradedType = Documents.TradedItemType.All Then Return True

            Dim current As VatDeclarationSchemaInfo = DirectCast(item, VatDeclarationSchemaInfo)

            If current.IsEmpty Then

                Return showEmpty

            Else

                If Not usedObjectsIds Is Nothing AndAlso usedObjectsIds.Contains( _
                    current.GetValueObjectIdString()) Then
                    Return True
                End If

                If Not showObsolete AndAlso current.IsObsolete Then Return False

                If tradedType <> Documents.TradedItemType.All AndAlso _
                    current.TradedType <> Documents.TradedItemType.All AndAlso _
                    current.TradedType <> tradedType Then Return False

            End If

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

            Dim myComm As New SQLCommand("FetchVatDeclarationSchemaInfoList")

            Using myData As DataTable = myComm.Fetch

                RaiseListChangedEvents = False
                IsReadOnly = False

                Add(VatDeclarationSchemaInfo.Empty())

                For Each dr As DataRow In myData.Rows
                    Add(VatDeclarationSchemaInfo.GetVatDeclarationSchemaInfo(dr, 0))
                Next

                IsReadOnly = True
                RaiseListChangedEvents = True

            End Using

        End Sub

#End Region

    End Class

End Namespace
