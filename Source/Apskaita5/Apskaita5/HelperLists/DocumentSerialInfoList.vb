﻿Namespace HelperLists

    ''' <summary>
    ''' Represents a list of <see cref="ApskaitaObjects.Settings.DocumentSerial">document serial</see> value objects.
    ''' </summary>
    ''' <remarks>Exists a single instance per company.</remarks>
    <Serializable()> _
    Public NotInheritable Class DocumentSerialInfoList
        Inherits ReadOnlyListBase(Of DocumentSerialInfoList, DocumentSerialInfo)

#Region " Business Methods "

#End Region

#Region " Authorization Rules "

        Public Shared Function CanGetObject() As Boolean
            Return ApplicationContext.User.IsInRole("HelperLists.DocumentSerialInfoList1")
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets a current document serial value object list from database.
        ''' </summary>
        ''' <remarks>Result is cached.
        ''' Required by <see cref="AccDataAccessLayer.CacheManager">AccDataAccessLayer.CacheManager</see>.</remarks>
        Public Shared Function GetList() As DocumentSerialInfoList

            Dim result As DocumentSerialInfoList = CacheManager.GetItemFromCache(Of DocumentSerialInfoList)( _
                GetType(DocumentSerialInfoList), Nothing)

            If result Is Nothing Then
                result = DataPortal.Fetch(Of DocumentSerialInfoList)(New Criteria())
                CacheManager.AddCacheItem(GetType(DocumentSerialInfoList), result, Nothing)
            End If

            Return result

        End Function

        ''' <summary>
        ''' Gets a filtered view of the current document serial value object list.
        ''' </summary>
        ''' <param name="showEmpty">Wheather to include a placeholder object.</param>
        ''' <param name="forDocumentType">A type of the document for which the list is requested.</param>
        ''' <remarks>Result is cached.
        ''' Required by <see cref="AccDataAccessLayer.CacheManager">AccDataAccessLayer.CacheManager</see>.</remarks>
        Public Shared Function GetCachedFilteredList(ByVal showEmpty As Boolean, _
            ByVal forDocumentType As Settings.DocumentSerialType) _
            As Csla.FilteredBindingList(Of DocumentSerialInfo)

            Dim filterToApply(1) As Object
            filterToApply(0) = ConvertDbBoolean(showEmpty)
            filterToApply(1) = Utilities.ConvertDatabaseID(forDocumentType)

            Dim result As Csla.FilteredBindingList(Of DocumentSerialInfo) = _
                CacheManager.GetItemFromCache(Of Csla.FilteredBindingList(Of DocumentSerialInfo)) _
                (GetType(DocumentSerialInfoList), filterToApply)

            If result Is Nothing Then

                Dim baseList As DocumentSerialInfoList = DocumentSerialInfoList.GetList
                result = New Csla.FilteredBindingList(Of DocumentSerialInfo) _
                    (baseList, AddressOf DocumentSerialInfoFilter)
                result.ApplyFilter("", filterToApply)
                CacheManager.AddCacheItem(GetType(DocumentSerialInfoList), result, filterToApply)

            End If

            Return result

        End Function

        ''' <summary>
        ''' Invalidates the current document serial value object list cache 
        ''' so that the next <see cref="GetList">GetList</see> call would hit the database.
        ''' </summary>
        ''' <remarks>Required by <see cref="AccDataAccessLayer.CacheManager">AccDataAccessLayer.CacheManager</see>.</remarks>
        Public Shared Sub InvalidateCache()
            CacheManager.InvalidateCache(GetType(DocumentSerialInfoList))
        End Sub

        ''' <summary>
        ''' Returnes true if the cache does not contain a current document serial value object list.
        ''' </summary>
        ''' <remarks>Required by <see cref="AccDataAccessLayer.CacheManager">AccDataAccessLayer.CacheManager</see>.</remarks>
        Public Shared Function CacheIsInvalidated() As Boolean
            Return CacheManager.CacheIsInvalidated(GetType(DocumentSerialInfoList))
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
        ''' Gets a current document serial value object list from database bypassing dataportal.
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks>Should only be called server side.
        ''' Required by <see cref="AccDataAccessLayer.CacheManager">AccDataAccessLayer.CacheManager</see>.</remarks>
        Private Shared Function GetListOnServer() As DocumentSerialInfoList
            Dim result As New DocumentSerialInfoList
            result.DataPortal_Fetch(New Criteria)
            Return result
        End Function


        Private Shared Function DocumentSerialInfoFilter(ByVal item As Object, _
                    ByVal filterValue As Object) As Boolean

            If filterValue Is Nothing OrElse DirectCast(filterValue, Object()).Length < 2 Then Return True

            Dim showEmpty As Boolean = ConvertDbBoolean( _
                DirectCast(DirectCast(filterValue, Object())(0), Integer))
            Dim forDocumentType As Settings.DocumentSerialType = _
                Utilities.ConvertDatabaseID(Of Settings.DocumentSerialType) _
                (DirectCast(DirectCast(filterValue, Object())(1), Integer))

            Dim current As DocumentSerialInfo = DirectCast(item, DocumentSerialInfo)

            If Not showEmpty AndAlso Not current.ID > 0 Then Return False
            If current.ID > 0 AndAlso current.DocumentType <> forDocumentType Then Return False

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

            Dim myComm As New SQLCommand("FetchDocumentSerialInfoList")

            Using myData As DataTable = myComm.Fetch

                RaiseListChangedEvents = False
                IsReadOnly = False

                For Each dr As DataRow In myData.Rows
                    Add(DocumentSerialInfo.GetDocumentSerialInfo(dr))
                Next

                IsReadOnly = True
                RaiseListChangedEvents = True

            End Using

        End Sub

#End Region

    End Class

End Namespace