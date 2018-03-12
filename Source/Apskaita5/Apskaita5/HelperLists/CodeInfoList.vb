Imports System.Security
Imports ApskaitaObjects.Settings.XmlProxies
Imports ApskaitaObjects.Settings

Namespace HelperLists

    ''' <summary>
    ''' Represents a list of <see cref="Code">Code</see> value objects, i.e.
    ''' various codes with names/descriptions that are used by business objects, 
    ''' e.g. income type for various tax declarations.
    ''' </summary>
    ''' <remarks>Exists a single instance accross all of the databases, however
    ''' historical values need to be added from the current database
    ''' in order to provide consistent datasource.</remarks>
    <Serializable()> _
    Public NotInheritable Class CodeInfoList
        Inherits ReadOnlyListBase(Of CodeInfoList, CodeInfo)

#Region " Business Methods "

        Private _CodeWageGPM As String = ""


        ''' <summary>
        ''' Gets a code for wage used in personal income tax declaration.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property CodeWageGPM() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _CodeWageGPM
            End Get
        End Property


        ''' <summary>
        ''' Gets a CodeInfo instance by requested code type and code value.
        ''' </summary>
        ''' <param name="ofType">a type of code to look for</param>
        ''' <param name="code">a code value to look for</param>
        ''' <remarks></remarks>
        Public Function GetItemByCode(ByVal ofType As CodeType, ByVal code As Integer) As CodeInfo
            For Each c As CodeInfo In Me
                If c.CodeInt = code AndAlso c.Type = ofType Then Return c
            Next
            Return Nothing
        End Function

        ''' <summary>
        ''' Gets a CodeInfo instance by requested code type, code value and code name.
        ''' </summary>
        ''' <param name="ofType">a type of code to look for</param>
        ''' <param name="code">a code value to look for</param>
        ''' <param name="name">a code name to look for</param>
        ''' <remarks></remarks>
        Public Function GetItemByCode(ByVal ofType As CodeType, ByVal code As String, name As String) As CodeInfo
            For Each c As CodeInfo In Me
                If c.Type = ofType AndAlso c.Code.Trim.ToLower = code.Trim.ToLower _
                    AndAlso c.Name.Trim.ToLower = name.Trim.ToLower Then Return c
            Next
            Return Nothing
        End Function

        ''' <summary>
        ''' Gets a CodeInfo instance by requested code type and code value.
        ''' </summary>
        ''' <param name="ofType">a type of code to look for</param>
        ''' <param name="code">a code value to look for</param>
        ''' <remarks></remarks>
        Public Function GetItemByCode(ByVal ofType As CodeType, ByVal code As String) As CodeInfo
            If code Is Nothing Then Return Nothing
            For Each c As CodeInfo In Me
                If c.Code.Trim.ToLower() = code.Trim.ToLower() _
                    AndAlso c.Type = ofType Then Return c
            Next
            Return Nothing
        End Function

#End Region

#Region " Authorization Rules "

        Public Shared Function CanGetObject() As Boolean
            Return ApplicationContext.User.IsInRole("HelperLists.CodeInfoList1")
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets a current CodeInfoList from database.
        ''' </summary>
        ''' <remarks>Result is cached.
        ''' Required by <see cref="AccDataAccessLayer.CacheManager">AccDataAccessLayer.CacheManager</see>.</remarks>
        Public Shared Function GetList() As CodeInfoList

            Dim result As CodeInfoList = CacheManager.GetItemFromCache(Of CodeInfoList)( _
                GetType(CodeInfoList), Nothing)

            If result Is Nothing Then
                result = DataPortal.Fetch(Of CodeInfoList)(New Criteria())
                CacheManager.AddCacheItem(GetType(CodeInfoList), result, Nothing)
            End If

            Return result

        End Function

        ''' <summary>
        ''' Gets a filtered view of the current CodeInfoList.
        ''' </summary>
        ''' <param name="showEmpty">Wheather to include a placeholder object.</param>
        ''' <param name="ofType">Type of the codes to include.</param>
        ''' <param name="showObsolete">Wheather to include codes that are obsolete (no loger in use).</param>
        ''' <remarks>Result is cached.
        ''' Required by <see cref="AccDataAccessLayer.CacheManager">AccDataAccessLayer.CacheManager</see>.</remarks>
        Public Shared Function GetCachedFilteredList(ByVal ofType As CodeType, _
            ByVal showEmpty As Boolean, ByVal showObsolete As Boolean, _
            ByVal usedObjectsIds As List(Of String)) As Csla.FilteredBindingList(Of CodeInfo)

            Dim filterToApply(3) As Object
            filterToApply(0) = showEmpty
            filterToApply(1) = ofType
            filterToApply(2) = showObsolete
            filterToApply(3) = usedObjectsIds

            Dim result As Csla.FilteredBindingList(Of CodeInfo) = _
                CacheManager.GetItemFromCache(Of Csla.FilteredBindingList(Of CodeInfo)) _
                (GetType(CodeInfoList), filterToApply)

            If result Is Nothing Then

                Dim baseList As CodeInfoList = CodeInfoList.GetList
                result = New Csla.FilteredBindingList(Of CodeInfo)(baseList, AddressOf CodeInfoFilter)
                result.ApplyFilter("", filterToApply)
                CacheManager.AddCacheItem(GetType(CodeInfoList), result, filterToApply)

            End If

            Return result

        End Function

        ''' <summary>
        ''' Invalidates the current sCodeInfoList cache 
        ''' so that the next <see cref="GetList">GetList</see> call would hit the database.
        ''' </summary>
        ''' <remarks>Required by <see cref="AccDataAccessLayer.CacheManager">AccDataAccessLayer.CacheManager</see>.</remarks>
        Public Shared Sub InvalidateCache()
            CacheManager.InvalidateCache(GetType(CodeInfoList))
        End Sub

        ''' <summary>
        ''' Returnes true if the cache does not contain a current CodeInfoList.
        ''' </summary>
        ''' <remarks>Required by <see cref="AccDataAccessLayer.CacheManager">AccDataAccessLayer.CacheManager</see>.</remarks>
        Public Shared Function CacheIsInvalidated() As Boolean
            Return CacheManager.CacheIsInvalidated(GetType(CodeInfoList))
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
        ''' Gets a current CodeInfoList from database bypassing dataportal.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>Should only be called server side.
        ''' Required by <see cref="AccDataAccessLayer.CacheManager">AccDataAccessLayer.CacheManager</see>.</remarks>
        Private Shared Function GetListOnServer() As CodeInfoList
            Dim result As New CodeInfoList
            result.DataPortal_Fetch(New Criteria)
            Return result
        End Function

        ''' <summary>
        ''' Gets a current CodeInfoList from database bypassing dataportal.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>Should only be called server side.</remarks>
        Friend Shared Function GetListChild() As CodeInfoList
            Dim result As New CodeInfoList
            result.DoFetch()
            Return result
        End Function


        Private Shared Function CodeInfoFilter(ByVal item As Object, ByVal filterValue As Object) As Boolean

            If filterValue Is Nothing OrElse DirectCast(filterValue, Object()).Length < 4 Then Return True

            Dim filterArray As Object() = DirectCast(filterValue, Object())

            Dim showEmpty As Boolean = DirectCast(filterArray(0), Boolean)
            Dim ofType As CodeType = DirectCast(filterArray(1), CodeType)
            Dim showObsolete As Boolean = DirectCast(filterArray(2), Boolean)
            Dim usedObjectsIds As List(Of String) = DirectCast(filterArray(3), List(Of String))

            Dim current As CodeInfo = DirectCast(item, CodeInfo)

            If current.IsEmpty Then

                Return showEmpty

            Else

                If Not usedObjectsIds Is Nothing AndAlso usedObjectsIds.Contains( _
                    current.GetValueObjectIdString()) Then
                    Return True
                End If

                If current.Type <> ofType OrElse (Not showObsolete AndAlso _
                    current.IsObsolete) Then Return False

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

            If Not CanGetObject() Then Throw New SecurityException( _
                My.Resources.Common_SecuritySelectDenied)

            DoFetch()

        End Sub

        Private Sub DoFetch()

            Dim settingsProxy As CommonSettingsProxy = _
                ApskaitaObjects.Settings.CommonSettings.GetCurrentProxy()

            RaiseListChangedEvents = False
            IsReadOnly = False

            _CodeWageGPM = settingsProxy.CodeWageGPM

            Add(CodeInfo.Empty())

            For Each proxy As CodeProxy In settingsProxy.Codes
                Add(CodeInfo.GetCodeInfo(proxy))
            Next

            If GetCurrentIdentity.IsAuthenticatedWithDB Then

                Dim myComm As New SQLCommand("FetchCodeInfoList")

                Using myData As DataTable = myComm.Fetch()
                    For Each dr As DataRow In myData.Rows
                        AddIfNotExists(CodeInfo.GetCodeInfo(dr))
                    Next
                End Using

            End If

            IsReadOnly = True
            RaiseListChangedEvents = True

        End Sub

        Private Sub AddIfNotExists(ByVal newItem As CodeInfo)

            If newItem.IsEmpty Then Exit Sub

            If newItem.Type = CodeType.SaftAccountType OrElse newItem.Type = CodeType.SaftSharesType Then
                For Each n As CodeInfo In Me
                    If n.Type = newItem.Type AndAlso n.Code.Trim.ToLower() _
                        = newItem.Code.Trim.ToLower() AndAlso n.Name.Trim.ToLower() _
                        = newItem.Name.Trim.ToLower() Then Exit Sub
                Next
            Else
                For Each n As CodeInfo In Me
                    If n.Type = newItem.Type AndAlso n.Code.Trim.ToLower() _
                        = newItem.Code.Trim.ToLower() Then Exit Sub
                Next
            End If

            Add(newItem)

        End Sub

#End Region

    End Class

End Namespace