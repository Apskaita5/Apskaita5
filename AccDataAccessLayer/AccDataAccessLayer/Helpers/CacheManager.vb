Imports AccDataAccessLayer.Security
Imports AccDataAccessLayer.DatabaseAccess
Public Module CacheManager

    Private _CacheItemList As List(Of CacheItem) = Nothing

    Public Event BaseTypeCacheIsAdded(ByVal e As CacheChangedEventArgs)

    Public Function GetItemFromCache(ByVal nBaseType As Type, ByVal nType As Type, _
        ByVal nFilterArguments As Object()) As Object

        Dim DatabaseName As String = ""

        If IsWebServer(GetCurrentIdentity.IsWebServerImpersonation) Then
            If WebServerUsesCache() Then
                DatabaseName = GetCurrentIdentity().Database.Trim
            Else
                Return Nothing
            End If
        End If

        If _CacheItemList Is Nothing Then Return Nothing

        Dim ItemToFind As New CacheItem(nBaseType, nType, nFilterArguments, DatabaseName)

        For Each item As CacheItem In _CacheItemList
            If item = ItemToFind Then Return item.CachedObject
        Next

        Return Nothing

    End Function

    Public Sub InvalidateCache(ByVal ParamArray nBaseType() As Type)

        If _CacheItemList Is Nothing OrElse _CacheItemList.Count < 1 Then Exit Sub

        Dim DatabaseName As String = ""

        If IsWebServer(GetCurrentIdentity.IsWebServerImpersonation) Then
            If WebServerUsesCache() Then
                DatabaseName = GetCurrentIdentity().Database.Trim
            Else
                Exit Sub
            End If
        End If

        For i As Integer = _CacheItemList.Count To 1 Step -1
            If Not Array.IndexOf(nBaseType, _CacheItemList(i - 1).BaseType) < 0 _
                AndAlso (_CacheItemList(i - 1).DatabaseName.Trim.ToLower = DatabaseName _
                OrElse _CacheItemList(i - 1).IsApplicationWide) Then _
                _CacheItemList.RemoveAt(i - 1)
        Next

    End Sub

    Public Sub AddCacheItem(ByVal nBaseType As Type, ByVal nCachedObject As Object, _
        ByVal nFilterArguments As Object())

        Dim DatabaseName As String = ""

        If IsWebServer(GetCurrentIdentity.IsWebServerImpersonation) Then
            If WebServerUsesCache() Then
                DatabaseName = GetCurrentIdentity().Database.Trim
            Else
                Exit Sub
            End If
        End If

        If _CacheItemList Is Nothing Then _CacheItemList = New List(Of CacheItem)

        Dim NewCacheItem As New CacheItem(nBaseType, nFilterArguments, nCachedObject, DatabaseName)

        If _CacheItemList.Count > 0 Then
            If NewCacheItem.BaseType Is NewCacheItem.Type Then
                For i As Integer = _CacheItemList.Count To 1 Step -1
                    If _CacheItemList(i - 1).BaseType Is NewCacheItem.BaseType _
                        AndAlso (NewCacheItem.IsApplicationWide _
                        OrElse NewCacheItem.DatabaseName.Trim.ToLower = _
                        _CacheItemList(i - 1).DatabaseName.Trim.ToLower) Then _CacheItemList.RemoveAt(i - 1)
                Next
            Else
                For i As Integer = _CacheItemList.Count To 1 Step -1
                    If _CacheItemList(i - 1) = NewCacheItem Then _CacheItemList.RemoveAt(i - 1)
                Next
            End If
        End If

        _CacheItemList.Add(NewCacheItem)

        ' web server has no awareness of the client -> no means to get the browser of the cache change
        ' -> no point in rising event
        If String.IsNullOrEmpty(DatabaseName.Trim) AndAlso NewCacheItem.BaseType Is NewCacheItem.Type Then _
            RaiseEvent BaseTypeCacheIsAdded(New CacheChangedEventArgs(NewCacheItem.BaseType))

    End Sub

    Public Function CacheIsInvalidated(ByVal nBaseType As Type) As Boolean

        If _CacheItemList Is Nothing Then Return True

        Dim DatabaseName As String = ""

        If IsWebServer(GetCurrentIdentity.IsWebServerImpersonation) Then
            If WebServerUsesCache() Then
                DatabaseName = GetCurrentIdentity().Database.Trim
            Else
                Exit Function
            End If
        End If

        For Each item As CacheItem In _CacheItemList
            If item.BaseType Is nBaseType AndAlso (item.IsApplicationWide _
                OrElse item.DatabaseName.Trim.ToLower = DatabaseName.Trim.ToLower) Then Return False
        Next

        Return True

    End Function

    Friend Sub InvalidateCompanyCache()

        If _CacheItemList Is Nothing OrElse _CacheItemList.Count < 1 Then Exit Sub

        Dim DatabaseName As String = ""

        If IsWebServer(GetCurrentIdentity.IsWebServerImpersonation) Then
            If WebServerUsesCache() Then
                DatabaseName = GetCurrentIdentity().Database.Trim
            Else
                Exit Sub
            End If
        End If

        For i As Integer = _CacheItemList.Count To 1 Step -1
            If Not _CacheItemList(i - 1).IsApplicationWide AndAlso _
                _CacheItemList(i - 1).DatabaseName.Trim.ToLower = _
                DatabaseName.Trim.ToLower Then _CacheItemList.RemoveAt(i - 1)
        Next

    End Sub

End Module
