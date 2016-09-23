Imports ApskaitaObjects.Settings
Imports System.Security
Imports ApskaitaObjects.Settings.XmlProxies

Namespace HelperLists

    ''' <summary>
    ''' Represents a list of <see cref="Name">Name</see> value objects, i.e.
    ''' a collection of predefined single string values to use in business objects
    ''' for lookup reference, e.g. names of SODRA administrative branches, legal groups etc.
    ''' </summary>
    ''' <remarks>Exists a single instance accross all of the databases, however
    ''' historical values need to be added from the current database
    ''' in order to provide consistent datasource.</remarks>
    <Serializable()> _
    Public NotInheritable Class NameInfoList
        Inherits ReadOnlyListBase(Of NameInfoList, NameInfo)

#Region " Business Methods "

        ''' <summary>
        ''' Gets a list of strings that contains the names of the type requested.
        ''' </summary>
        ''' <param name="ofType">a type of names to be returned</param>
        ''' <param name="addEmptyString">whether to add an empty string at the
        ''' beginning of the list</param>
        ''' <remarks>To use as a datasource for combobox etc.</remarks>
        Public Function GetStringList(ByVal ofType As NameType, _
            ByVal addEmptyString As Boolean) As List(Of String)
            Dim result As New List(Of String)
            For Each n As NameInfo In Me
                If n.Type = ofType Then result.Add(n.Name)
            Next
            If addEmptyString Then result.Insert(0, "")
            Return result
        End Function

#End Region

#Region " Authorization Rules "

        Public Shared Function CanGetObject() As Boolean
            Return ApplicationContext.User.IsInRole("HelperLists.NameInfoList1")
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets a current NameInfoList from database.
        ''' </summary>
        ''' <remarks>Result is cached.
        ''' Required by <see cref="AccDataAccessLayer.CacheManager">AccDataAccessLayer.CacheManager</see>.</remarks>
        Public Shared Function GetList() As NameInfoList

            Dim result As NameInfoList = CacheManager.GetItemFromCache(Of NameInfoList)( _
                GetType(NameInfoList), Nothing)

            If result Is Nothing Then
                result = DataPortal.Fetch(Of NameInfoList)(New Criteria())
                CacheManager.AddCacheItem(GetType(NameInfoList), result, Nothing)
            End If

            Return result

        End Function

        ''' <summary>
        ''' Gets a filtered view of the current NameInfoList.
        ''' </summary>
        ''' <remarks>Result is cached.
        ''' Required by <see cref="AccDataAccessLayer.CacheManager">AccDataAccessLayer.CacheManager</see>.</remarks>
        Public Shared Function GetCachedFilteredList(ByVal ofType As NameType, _
            ByVal showEmpty As Boolean, ByVal showObsolete As Boolean, _
            ByVal usedObjectsIds As List(Of String)) As System.ComponentModel.BindingList(Of String)

            Dim filterToApply(3) As Object
            filterToApply(0) = showEmpty
            filterToApply(1) = ofType
            filterToApply(2) = showObsolete
            filterToApply(3) = usedObjectsIds

            Dim result As System.ComponentModel.BindingList(Of String) = _
                CacheManager.GetItemFromCache(Of System.ComponentModel.BindingList(Of String)) _
                (GetType(NameInfoList), filterToApply)

            If result Is Nothing Then

                Dim baseList As NameInfoList = NameInfoList.GetList
                result = New System.ComponentModel.BindingList(Of String)

                For Each name As NameInfo In baseList
                    If name.Type = ofType Then

                        If showEmpty OrElse Not name.IsEmpty Then

                            If (Not usedObjectsIds Is Nothing AndAlso usedObjectsIds.Contains( _
                                name.GetValueObjectIdString())) OrElse _
                                showObsolete OrElse Not name.IsObsolete Then
                                result.Add(name.Name)
                            End If

                        End If

                    End If
                Next

                CacheManager.AddCacheItem(GetType(NameInfoList), result, filterToApply)

            End If

            Return result

        End Function

        ''' <summary>
        ''' Invalidates the current NameInfoList cache 
        ''' so that the next <see cref="GetList">GetList</see> call would hit the database.
        ''' </summary>
        ''' <remarks>Required by <see cref="AccDataAccessLayer.CacheManager">AccDataAccessLayer.CacheManager</see>.</remarks>
        Public Shared Sub InvalidateCache()
            CacheManager.InvalidateCache(GetType(NameInfoList))
        End Sub

        ''' <summary>
        ''' Returnes true if the cache does not contain a current NameInfoList.
        ''' </summary>
        ''' <remarks>Required by <see cref="AccDataAccessLayer.CacheManager">AccDataAccessLayer.CacheManager</see>.</remarks>
        Public Shared Function CacheIsInvalidated() As Boolean
            Return CacheManager.CacheIsInvalidated(GetType(NameInfoList))
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
        ''' Gets a current NameInfoList from database bypassing dataportal.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>Should only be called server side.
        ''' Required by <see cref="AccDataAccessLayer.CacheManager">AccDataAccessLayer.CacheManager</see>.</remarks>
        Private Shared Function GetListOnServer() As NameInfoList
            Dim result As New NameInfoList
            result.DataPortal_Fetch(New Criteria)
            Return result
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

            For Each proxy As NameProxy In settingsProxy.Names
                Add(NameInfo.GetNameInfo(proxy))
            Next

            If GetCurrentIdentity.IsAuthenticatedWithDB Then

                Dim myComm As New SQLCommand("FetchNameInfoList")

                Using myData As DataTable = myComm.Fetch()
                    For Each dr As DataRow In myData.Rows
                        AddIfNotExists(NameInfo.GetNameInfo(dr))
                    Next
                End Using

            End If

            IsReadOnly = True
            RaiseListChangedEvents = True

        End Sub

        Private Sub AddIfNotExists(ByVal newItem As NameInfo)

            For Each n As NameInfo In Me
                If n.Type = newItem.Type AndAlso n.Name.Trim.ToLower _
                    = newItem.Name.Trim.ToLower Then Exit Sub
            Next

            Add(newItem)

        End Sub

#End Region

    End Class

End Namespace