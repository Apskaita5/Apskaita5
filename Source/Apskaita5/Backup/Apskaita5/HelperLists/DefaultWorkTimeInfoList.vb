Imports System.Security
Imports ApskaitaObjects.Settings.XmlProxies

Namespace HelperLists

    ''' <summary>
    ''' Represents general data about gauge work time and public holiday in a country.
    ''' </summary>
    ''' <remarks>Exists a single instance accross all of the databases.
    ''' Persisted using xml proxies as a part of <see cref="ApskaitaObjects.Settings.CommonSettings">Settings.CommonSettings</see>.</remarks>
    <Serializable()> _
    Public NotInheritable Class DefaultWorkTimeInfoList
        Inherits ReadOnlyListBase(Of DefaultWorkTimeInfoList, DefaultWorkTimeInfo)

#Region " Business Methods "

        Private _PublicHolidays As List(Of Date) = Nothing

        ''' <summary>
        ''' Gets dates of public holiday.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property PublicHolidays() As List(Of Date)
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If _PublicHolidays Is Nothing Then
                    _PublicHolidays = New List(Of Date)
                End If
                Return _PublicHolidays
            End Get
        End Property


        ''' <summary>
        ''' Gets a DefaultWorkTimeInfo for given year an month.
        ''' If there is no entry found in list, returns calculated (approximately) info.
        ''' </summary>
        ''' <param name="year">a year to get the info for</param>
        ''' <param name="month">a month to get the info for</param>
        ''' <remarks></remarks>
        Public Function GetDefaultWorkTimeInfo(ByVal year As Integer, _
            ByVal month As Integer) As DefaultWorkTimeInfo

            For Each info As DefaultWorkTimeInfo In Me
                If info.Year = year AndAlso info.Month = month Then Return info
            Next

            Return DefaultWorkTimeInfo.NewDefaultWorkTimeInfo(year, month, _PublicHolidays)

        End Function

        ''' <summary>
        ''' Check whether the given date is a public holiday day. Retuns TRUE if so.
        ''' </summary>
        ''' <param name="year">a year of the date to check</param>
        ''' <param name="month">a month of the date to check</param>
        ''' <param name="day">a day of the date to check</param>
        ''' <remarks></remarks>
        Public Function IsPublicHolidays(ByVal year As Integer, _
            ByVal month As Integer, ByVal day As Integer) As Boolean

            Dim dateToCheck As Date
            Try
                dateToCheck = New Date(year, month, day)
            Catch ex As Exception
                Throw New Exception(String.Format(My.Resources.HelperLists_DefaultWorkTimeInfoList_InvalidDate, _
                    year.ToString("0000"), month.ToString("00"), day.ToString("00")), ex)
            End Try

            Return IsPublicHolidays(dateToCheck)

        End Function

        ''' <summary>
        ''' Check whether the given date is a public holiday day. Retuns TRUE if so.
        ''' </summary>
        ''' <param name="dateToCheck">a date to check</param>
        ''' <remarks></remarks>
        Public Function IsPublicHolidays(ByVal dateToCheck As Date) As Boolean
            Return PublicHolidays.Contains(dateToCheck.Date)
        End Function

#End Region

#Region " Authorization Rules "

        Public Shared Function CanGetObject() As Boolean
            Return ApplicationContext.User.IsInRole("HelperLists.DefaultWorkTimeInfoList1")
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets a current DefaultWorkTimeInfoList from database.
        ''' </summary>
        ''' <remarks>Result is cached.
        ''' Required by <see cref="AccDataAccessLayer.CacheManager">AccDataAccessLayer.CacheManager</see>.</remarks>
        Public Shared Function GetList() As DefaultWorkTimeInfoList

            Dim result As DefaultWorkTimeInfoList = CacheManager.GetItemFromCache(Of DefaultWorkTimeInfoList)( _
                GetType(DefaultWorkTimeInfoList), Nothing)

            If result Is Nothing Then
                result = DataPortal.Fetch(Of DefaultWorkTimeInfoList)(New Criteria())
                CacheManager.AddCacheItem(GetType(DefaultWorkTimeInfoList), result, Nothing)
            End If

            Return result

        End Function

        ''' <summary>
        ''' Gets a filtered view of the current DefaultWorkTimeInfoList.
        ''' </summary>
        ''' <remarks>Result is cached.
        ''' Required by <see cref="AccDataAccessLayer.CacheManager">AccDataAccessLayer.CacheManager</see>.</remarks>
        Public Shared Function GetCachedFilteredList() As Csla.FilteredBindingList(Of DefaultWorkTimeInfo)

            Dim result As Csla.FilteredBindingList(Of DefaultWorkTimeInfo) = _
                CacheManager.GetItemFromCache(Of Csla.FilteredBindingList(Of DefaultWorkTimeInfo)) _
                (GetType(DefaultWorkTimeInfoList), Nothing)

            If result Is Nothing Then

                Dim baseList As DefaultWorkTimeInfoList = DefaultWorkTimeInfoList.GetList
                result = New Csla.FilteredBindingList(Of DefaultWorkTimeInfo)(baseList)
                CacheManager.AddCacheItem(GetType(DefaultWorkTimeInfoList), result, Nothing)

            End If

            Return result

        End Function

        ''' <summary>
        ''' Invalidates the current DefaultWorkTimeInfoList cache 
        ''' so that the next <see cref="GetList">GetList</see> call would hit the database.
        ''' </summary>
        ''' <remarks>Required by <see cref="AccDataAccessLayer.CacheManager">AccDataAccessLayer.CacheManager</see>.</remarks>
        Public Shared Sub InvalidateCache()
            CacheManager.InvalidateCache(GetType(DefaultWorkTimeInfoList))
        End Sub

        ''' <summary>
        ''' Returnes true if the cache does not contain a current DefaultWorkTimeInfoList.
        ''' </summary>
        ''' <remarks>Required by <see cref="AccDataAccessLayer.CacheManager">AccDataAccessLayer.CacheManager</see>.</remarks>
        Public Shared Function CacheIsInvalidated() As Boolean
            Return CacheManager.CacheIsInvalidated(GetType(DefaultWorkTimeInfoList))
        End Function

        ''' <summary>
        ''' Returns true if the collection is common across all the databases.
        ''' I.e. cache is not to be cleared on changing databases.
        ''' </summary>
        ''' <remarks>Required by <see cref="AccDataAccessLayer.CacheManager">AccDataAccessLayer.CacheManager</see>.</remarks>
        Private Shared Function IsApplicationWideCache() As Boolean
            Return True
        End Function

        ''' <summary>
        ''' Gets a current DefaultWorkTimeInfoList from database bypassing dataportal.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>Should only be called server side.
        ''' Required by <see cref="AccDataAccessLayer.CacheManager">AccDataAccessLayer.CacheManager</see>.</remarks>
        Private Shared Function GetListOnServer() As DefaultWorkTimeInfoList
            Dim result As New DefaultWorkTimeInfoList
            result.DataPortal_Fetch(New Criteria)
            Return result
        End Function

        ''' <summary>
        ''' Gets a current DefaultWorkTimeInfoList from database bypassing dataportal.
        ''' </summary>
        ''' <remarks></remarks>
        Friend Shared Function GetListChild() As DefaultWorkTimeInfoList
            Dim result As New DefaultWorkTimeInfoList
            result.DoFetch()
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

            DoFetch()

        End Sub

        Private Sub DoFetch()

            Dim settingsProxy As CommonSettingsProxy = _
                ApskaitaObjects.Settings.CommonSettings.GetCurrentProxy()

            RaiseListChangedEvents = False
            IsReadOnly = False

            For Each proxy As DefaultWorkTimeProxy In settingsProxy.DefaultWorkTimes
                Add(DefaultWorkTimeInfo.GetDefaultWorkTimeInfo(proxy))
            Next

            _PublicHolidays = New List(Of Date)
            For Each proxy As PublicHolidayProxy In settingsProxy.PublicHolidays
                _PublicHolidays.Add(proxy.PublicHolidayDate.Date)
            Next

            IsReadOnly = True
            RaiseListChangedEvents = True

        End Sub

#End Region

    End Class

End Namespace