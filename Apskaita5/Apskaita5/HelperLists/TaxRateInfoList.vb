Imports System.Security
Imports ApskaitaObjects.Settings.XmlProxies
Imports ApskaitaObjects.Settings

Namespace HelperLists

    ''' <summary>
    ''' Represents a list of <see cref="ApskaitaObjects.Settings.TaxRate">Settings.TaxRate</see>
    ''' value objects, i.e. rates of different taxes.
    ''' </summary>
    ''' <remarks>Exists a single instance accross all of the databases, however
    ''' historical values need to be added from the current database
    ''' in order to provide consistent datasource.</remarks>
    <Serializable()> _
    Public NotInheritable Class TaxRateInfoList
        Inherits ReadOnlyListBase(Of TaxRateInfoList, TaxRateInfo)

#Region " Business Methods "

        ''' <summary>
        ''' Gets a list of rates for a specified tax type as a list of double values.
        ''' </summary>
        ''' <param name="ofType">A type of tax to get the rates for.</param>
        ''' <param name="includeObsolete">Whether to include obsolete rates.</param>
        ''' <remarks></remarks>
        Public Function GetRateList(ByVal ofType As TaxRateType, _
            ByVal includeObsolete As Boolean) As List(Of Double)

            Dim result As New List(Of Double)
            For Each r As TaxRateInfo In Me
                If r.Type = ofType AndAlso (includeObsolete OrElse Not r.IsObsolete) Then
                    result.Add(r.Rate)
                End If
            Next
            result.Sort()
            Return result

        End Function

#End Region

#Region " Authorization Rules "

        Public Shared Function CanGetObject() As Boolean
            Return ApplicationContext.User.IsInRole("HelperLists.TaxRateInfoList1")
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets a current TaxRateInfoList from database.
        ''' </summary>
        ''' <remarks>Result is cached.
        ''' Required by <see cref="AccDataAccessLayer.CacheManager">AccDataAccessLayer.CacheManager</see>.</remarks>
        Public Shared Function GetList() As TaxRateInfoList

            Dim result As TaxRateInfoList = CacheManager.GetItemFromCache(Of TaxRateInfoList)( _
                GetType(TaxRateInfoList), Nothing)

            If result Is Nothing Then
                result = DataPortal.Fetch(Of TaxRateInfoList)(New Criteria())
                CacheManager.AddCacheItem(GetType(TaxRateInfoList), result, Nothing)
            End If

            Return result

        End Function

        ''' <summary>
        ''' Gets a filtered view of the current TaxRateInfoList.
        ''' </summary>
        ''' <param name="ofType">Type of the taxes to include.</param>
        ''' <param name="showObsolete">Wheather to include tax rates that are obsolete (no loger in use).</param>
        ''' <remarks>Result is cached.
        ''' Required by <see cref="AccDataAccessLayer.CacheManager">AccDataAccessLayer.CacheManager</see>.</remarks>
        Public Shared Function GetCachedFilteredList(ByVal ofType As TaxRateType, _
            ByVal showObsolete As Boolean) As Csla.FilteredBindingList(Of TaxRateInfo)

            Dim filterToApply(1) As Object
            filterToApply(0) = Utilities.ConvertDatabaseID(ofType)
            filterToApply(1) = ConvertDbBoolean(showObsolete)

            Dim result As Csla.FilteredBindingList(Of TaxRateInfo) = _
                CacheManager.GetItemFromCache(Of Csla.FilteredBindingList(Of TaxRateInfo)) _
                (GetType(TaxRateInfoList), filterToApply)

            If result Is Nothing Then

                Dim baseList As TaxRateInfoList = TaxRateInfoList.GetList
                result = New Csla.FilteredBindingList(Of TaxRateInfo)(baseList, AddressOf TaxRateInfoFilter)
                result.ApplyFilter("", filterToApply)
                CacheManager.AddCacheItem(GetType(TaxRateInfoList), result, filterToApply)

            End If

            Return result

        End Function

        ''' <summary>
        ''' Invalidates the current TaxRateInfoList cache 
        ''' so that the next <see cref="GetList">GetList</see> call would hit the database.
        ''' </summary>
        ''' <remarks>Required by <see cref="AccDataAccessLayer.CacheManager">AccDataAccessLayer.CacheManager</see>.</remarks>
        Public Shared Sub InvalidateCache()
            CacheManager.InvalidateCache(GetType(TaxRateInfoList))
        End Sub

        ''' <summary>
        ''' Returnes true if the cache does not contain a current TaxRateInfoList.
        ''' </summary>
        ''' <remarks>Required by <see cref="AccDataAccessLayer.CacheManager">AccDataAccessLayer.CacheManager</see>.</remarks>
        Public Shared Function CacheIsInvalidated() As Boolean
            Return CacheManager.CacheIsInvalidated(GetType(TaxRateInfoList))
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
        ''' Gets a current TaxRateInfoList from database bypassing dataportal.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>Should only be called server side.
        ''' Required by <see cref="AccDataAccessLayer.CacheManager">AccDataAccessLayer.CacheManager</see>.</remarks>
        Private Shared Function GetListOnServer() As TaxRateInfoList
            Dim result As New TaxRateInfoList
            result.DataPortal_Fetch(New Criteria)
            Return result
        End Function


        Private Shared Function TaxRateInfoFilter(ByVal item As Object, ByVal filterValue As Object) As Boolean

            If filterValue Is Nothing OrElse DirectCast(filterValue, Object()).Length < 2 Then Return True

            Dim ofType As TaxRateType = Utilities.ConvertDatabaseID(Of TaxRateType) _
                (DirectCast(DirectCast(filterValue, Object())(0), Integer))
            Dim showObsolete As Boolean = ConvertDbBoolean( _
                DirectCast(DirectCast(filterValue, Object())(1), Integer))

            Dim current As TaxRateInfo = DirectCast(item, TaxRateInfo)

            If Not showObsolete AndAlso current.IsObsolete Then Return False
            If current.Type <> ofType Then Return False

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

            If Not CanGetObject() Then Throw New SecurityException( _
                My.Resources.Common_SecuritySelectDenied)

            Dim settingsProxy As CommonSettingsProxy = _
                ApskaitaObjects.Settings.CommonSettings.GetCurrentProxy()

            RaiseListChangedEvents = False
            IsReadOnly = False

            For Each proxy As TaxRateProxy In settingsProxy.TaxRates
                Add(TaxRateInfo.GetTaxRateInfo(proxy))
            Next

            If GetCurrentIdentity.IsAuthenticatedWithDB Then

                Dim myComm As New SQLCommand("FetchTaxRateListInUse")

                Using myData As DataTable = myComm.Fetch()
                    For Each dr As DataRow In myData.Rows
                        AddIfNotExists(TaxRateInfo.GetTaxRateInfo(dr))
                    Next
                End Using

            End If

            IsReadOnly = True
            RaiseListChangedEvents = True

        End Sub

        Private Sub AddIfNotExists(ByVal newItem As TaxRateInfo)

            For Each n As TaxRateInfo In Me
                If n.Type = newItem.Type AndAlso n.Rate = newItem.Rate Then Exit Sub
            Next

            Add(newItem)

        End Sub

#End Region

    End Class

End Namespace