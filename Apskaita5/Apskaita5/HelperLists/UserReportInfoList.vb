Imports ApskaitaObjects.My.Resources

Namespace HelperLists

    ''' <summary>
    ''' Represents a collection of user reports (information about *.rdl files)
    ''' that were uploaded to the program (see <see cref="ApskaitaObjects.Settings.CommandUploadUserReport">CommandUploadUserReport</see>).
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public Class UserReportInfoList
        Inherits ReadOnlyListBase(Of UserReportInfoList, UserReportInfo)

#Region " Business Methods "

        Private _Warnings As String = ""

        ''' <summary>
        ''' Gets a description of non critical errors that occured 
        ''' while fething data (if any).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Warnings() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Warnings.Trim
            End Get
        End Property


        Friend Function Exists(ByVal info As UserReportInfo) As Boolean

            For Each report As UserReportInfo In Me
                If report = info Then Return True
            Next

            Return False

        End Function

#End Region

#Region " Authorization Rules "

        Public Shared Function CanGetObject() As Boolean
            Return ApplicationContext.User.IsInRole("UserReportInfoList1")
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets a current user report list (from the program install folder 
        ''' or the (web) server).
        ''' </summary>
        ''' <remarks>Result is cached.
        ''' Required by <see cref="AccDataAccessLayer.CacheManager">AccDataAccessLayer.CacheManager</see>.</remarks>
        Public Shared Function GetList() As UserReportInfoList

            Dim result As UserReportInfoList = CacheManager.GetItemFromCache(Of UserReportInfoList)( _
                GetType(UserReportInfoList), Nothing)

            If result Is Nothing Then
                result = DataPortal.Fetch(Of UserReportInfoList)(New Criteria())
                CacheManager.AddCacheItem(GetType(UserReportInfoList), result, Nothing)
            End If

            Return result

        End Function

        ''' <summary>
        ''' Gets a filtered view of the current user report list (does no filtering
        ''' only for compartability with the cache manager).
        ''' </summary>
        ''' <remarks>Required by <see cref="AccDataAccessLayer.CacheManager">AccDataAccessLayer.CacheManager</see>.</remarks>
        Public Shared Function GetCachedFilteredList() As Csla.FilteredBindingList(Of UserReportInfo)

            Dim result As Csla.FilteredBindingList(Of UserReportInfo) = _
                CacheManager.GetItemFromCache(Of Csla.FilteredBindingList(Of UserReportInfo)) _
                (GetType(UserReportInfoList), Nothing)

            If result Is Nothing Then

                Dim baseList As UserReportInfoList = UserReportInfoList.GetList
                result = New Csla.FilteredBindingList(Of UserReportInfo)(baseList)
                CacheManager.AddCacheItem(GetType(UserReportInfoList), result, Nothing)

            End If

            Return result

        End Function

        ''' <summary>
        ''' Invalidates the current user report list cache so that the next 
        ''' <see cref="GetList">GetList</see> call would hit the database.
        ''' </summary>
        ''' <remarks>Required by <see cref="AccDataAccessLayer.CacheManager">AccDataAccessLayer.CacheManager</see>.</remarks>
        Public Shared Sub InvalidateCache()
            CacheManager.InvalidateCache(GetType(UserReportInfoList))
        End Sub

        ''' <summary>
        ''' Returnes true if the cache does not contain a current user report list.
        ''' </summary>
        ''' <remarks>Required by <see cref="AccDataAccessLayer.CacheManager">AccDataAccessLayer.CacheManager</see>.</remarks>
        Public Shared Function CacheIsInvalidated() As Boolean
            Return CacheManager.CacheIsInvalidated(GetType(UserReportInfoList))
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
        ''' Gets a current user report list bypassing dataportal.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>Should only be called server side.
        ''' Required by <see cref="AccDataAccessLayer.CacheManager">AccDataAccessLayer.CacheManager</see>.</remarks>
        Private Shared Function GetListOnServer() As UserReportInfoList
            Dim result As New UserReportInfoList
            result.DataPortal_Fetch(New Criteria)
            Return result
        End Function

        ''' <summary>
        ''' Gets a current user report list bypassing dataportal.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>Should only be called server side.</remarks>
        Friend Shared Function GetListChild() As UserReportInfoList
            Dim result As New UserReportInfoList
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
        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)

            If Not CanGetObject() Then Throw New System.Security.SecurityException( _
               My.Resources.Common_SecuritySelectDenied)

            DoFetch()

        End Sub

        Private Sub DoFetch()

            Dim reportFolder As String = IO.Path.Combine(AppPath(), USERREPORTSFOLDER)

            If Not IO.Directory.Exists(reportFolder) Then
                _Warnings = String.Format(HelperLists_UserReportInfoList_UserReportFolderDoesNotExist, _
                    USERREPORTSFOLDER)
                Exit Sub
            End If

            Dim files As String() = IO.Directory.GetFiles(reportFolder)

            If files Is Nothing OrElse files.Length < 1 Then
                _Warnings = HelperLists_UserReportInfoList_NoAvailableReports
                Exit Sub
            End If

            RaiseListChangedEvents = False
            IsReadOnly = False

            Dim info As IO.FileInfo

            Dim counter As Integer = 0

            For Each fileName As String In files

                Try
                    info = New IO.FileInfo(IO.Path.Combine(reportFolder, fileName))
                Catch ex As Exception
                    _Warnings = AddWithNewLine(_Warnings, String.Format( _
                        HelperLists_UserReportInfoList_FailedToOpenFile, _
                        fileName, vbCrLf, ex.Message), False)
                    info = Nothing
                    counter += 1
                End Try

                If Not info Is Nothing AndAlso (info.Extension.Trim.ToLower = ".rdl" _
                    OrElse info.Extension.Trim.ToLower = ".rdlc") Then

                    Try
                        Add(UserReportInfo.GetUserReportInfo(info))
                    Catch ex As Exception
                        _Warnings = AddWithNewLine(_Warnings, String.Format( _
                            HelperLists_UserReportInfoList_FailedToParseReport, _
                            fileName, vbCrLf, ex.Message), False)
                    End Try

                    counter += 1

                End If

            Next

            If counter < 1 Then
                _Warnings = HelperLists_UserReportInfoList_NoAvailableReports
            End If

            IsReadOnly = True
            RaiseListChangedEvents = True

        End Sub

#End Region

    End Class

End Namespace