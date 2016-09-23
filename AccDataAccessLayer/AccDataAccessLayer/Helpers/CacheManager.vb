Imports AccDataAccessLayer.Security
Imports AccDataAccessLayer.DatabaseAccess
Public Module CacheManager

    Private _CacheItemList As List(Of CacheItem) = Nothing

    Public Event BaseTypeCacheIsAdded(ByVal e As CacheChangedEventArgs)


    Public Function GetItemFromCache(Of T)(ByVal baseType As Type, ByVal filterArguments As Object()) As T

        If _CacheItemList Is Nothing Then Return Nothing

        Dim databaseName As String = ""

        If IsWebServer(GetCurrentIdentity.IsWebServerImpersonation) Then
            If WebServerUsesCache() Then
                databaseName = GetCurrentIdentity().Database.Trim
            Else
                Return Nothing
            End If
        End If

        Dim itemToFind As New CacheItem(baseType, GetType(T), filterArguments, databaseName)

        For Each item As CacheItem In _CacheItemList
            If item = itemToFind Then Return DirectCast(item.CachedObject, T)
        Next

        Return Nothing

    End Function

    Public Sub InvalidateCache(ByVal ParamArray baseTypes() As Type)

        If _CacheItemList Is Nothing OrElse _CacheItemList.Count < 1 Then Exit Sub

        Dim databaseName As String = ""

        If IsWebServer(GetCurrentIdentity.IsWebServerImpersonation) Then
            If WebServerUsesCache() Then
                databaseName = GetCurrentIdentity().Database.Trim
            Else
                Exit Sub
            End If
        End If

        For i As Integer = _CacheItemList.Count To 1 Step -1
            If Not Array.IndexOf(baseTypes, _CacheItemList(i - 1).BaseType) < 0 _
                AndAlso (_CacheItemList(i - 1).DatabaseName.Trim.ToLower = databaseName _
                OrElse _CacheItemList(i - 1).IsApplicationWide) Then

                _CacheItemList.RemoveAt(i - 1)

            End If
        Next

    End Sub

    Public Sub AddCacheItem(ByVal baseType As Type, ByVal cachedObject As Object, _
        ByVal filterArguments As Object())

        Dim databaseName As String = ""

        If IsWebServer(GetCurrentIdentity.IsWebServerImpersonation) Then
            If WebServerUsesCache() Then
                databaseName = GetCurrentIdentity().Database.Trim
            Else
                Exit Sub
            End If
        End If

        If _CacheItemList Is Nothing Then _CacheItemList = New List(Of CacheItem)

        Dim newCacheItem As New CacheItem(baseType, filterArguments, cachedObject, databaseName)

        If _CacheItemList.Count > 0 Then
            If newCacheItem.BaseType Is newCacheItem.Type Then

                For i As Integer = _CacheItemList.Count To 1 Step -1
                    If _CacheItemList(i - 1).BaseType Is newCacheItem.BaseType _
                        AndAlso (newCacheItem.IsApplicationWide _
                        OrElse newCacheItem.DatabaseName.Trim.ToLower = _
                        _CacheItemList(i - 1).DatabaseName.Trim.ToLower) Then

                        _CacheItemList.RemoveAt(i - 1)

                    End If
                Next

            Else
                For i As Integer = _CacheItemList.Count To 1 Step -1
                    If _CacheItemList(i - 1) = newCacheItem Then
                        _CacheItemList.RemoveAt(i - 1)
                    End If
                Next
            End If
        End If

        _CacheItemList.Add(newCacheItem)

        ' web server has no awareness of the client -> no means to get the browser of the cache change
        ' -> no point in rising event
        If String.IsNullOrEmpty(databaseName.Trim) AndAlso newCacheItem.BaseType Is newCacheItem.Type Then
            RaiseEvent BaseTypeCacheIsAdded(New CacheChangedEventArgs(newCacheItem.BaseType))
        End If

    End Sub

    Public Function CacheIsInvalidated(ByVal baseType As Type) As Boolean

        If _CacheItemList Is Nothing Then Return True

        Dim databaseName As String = ""

        If IsWebServer(GetCurrentIdentity.IsWebServerImpersonation) Then
            If WebServerUsesCache() Then
                databaseName = GetCurrentIdentity().Database.Trim
            Else
                Exit Function
            End If
        End If

        For Each item As CacheItem In _CacheItemList
            If item.BaseType Is baseType AndAlso (item.IsApplicationWide _
                OrElse item.DatabaseName.Trim.ToLower = databaseName.Trim.ToLower) Then Return False
        Next

        Return True

    End Function

    Friend Sub InvalidateCompanyCache()

        If _CacheItemList Is Nothing OrElse _CacheItemList.Count < 1 Then Exit Sub

        Dim databaseName As String = ""

        If IsWebServer(GetCurrentIdentity.IsWebServerImpersonation) Then
            If WebServerUsesCache() Then
                databaseName = GetCurrentIdentity().Database.Trim
            Else
                Exit Sub
            End If
        End If

        For i As Integer = _CacheItemList.Count To 1 Step -1
            If Not _CacheItemList(i - 1).IsApplicationWide AndAlso _
               _CacheItemList(i - 1).DatabaseName.Trim.ToLower = _
               databaseName.Trim.ToLower Then

                _CacheItemList.RemoveAt(i - 1)

            End If
        Next

    End Sub

End Module
