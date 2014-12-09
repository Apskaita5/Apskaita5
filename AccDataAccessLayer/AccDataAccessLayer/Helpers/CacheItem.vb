Imports System.Reflection
Public Class CacheItem
    Implements IComparable

    Private _BaseType As Type
    Private _Type As Type
    Private _FilterArguments As Object()
    Private _CachedObject As Object
    Private _DatabaseName As String
    Private _IsApplicationWide As Boolean = False


    Public ReadOnly Property BaseType() As Type
        Get
            Return _BaseType
        End Get
    End Property

    Public ReadOnly Property [Type]() As Type
        Get
            Return _Type
        End Get
    End Property

    Public ReadOnly Property FilterArguments() As Object()
        Get
            Return _FilterArguments
        End Get
    End Property

    Public ReadOnly Property CachedObject() As Object
        Get
            Return _CachedObject
        End Get
    End Property

    Public ReadOnly Property DatabaseName() As String
        Get
            Return _DatabaseName
        End Get
    End Property

    Public ReadOnly Property IsApplicationWide() As Boolean
        Get
            Return _IsApplicationWide
        End Get
    End Property


    Friend Sub New(ByVal nBaseType As Type, ByVal nFilterArguments As Object(), _
        ByVal nCachedObject As Object, ByVal nDatabaseName As String)

        If nCachedObject Is Nothing Then Throw New ArgumentNullException( _
            "Klaida. CacheManager nurodytas kešuotinas objektas yra null.")
        If nBaseType Is Nothing Then Throw New ArgumentNullException( _
            "Klaida. Nenurodytas kešuojamo objekto bazinis tipas.")
        _BaseType = nBaseType
        _Type = nCachedObject.GetType
        _FilterArguments = nFilterArguments
        _CachedObject = nCachedObject
        _DatabaseName = nDatabaseName

        Try
            Dim MI As MethodInfo = nBaseType.GetMethod("IsApplicationWideCache", _
                BindingFlags.NonPublic Or BindingFlags.Static)
            _IsApplicationWide = DirectCast(MI.Invoke(Nothing, Nothing), Boolean)
        Catch ex As Exception
            _IsApplicationWide = False
        End Try

    End Sub

    Friend Sub New(ByVal nBaseType As Type, ByVal nType As Type, _
        ByVal nFilterArguments As Object(), ByVal nDatabaseName As String)

        If nBaseType Is Nothing OrElse nType Is Nothing Then Throw New ArgumentNullException( _
            "Klaida. Nenurodytas kešuojamo objekto tipas arba bazinis tipas.")
        _BaseType = nBaseType
        _Type = nType
        _FilterArguments = nFilterArguments
        _CachedObject = Nothing
        _DatabaseName = nDatabaseName

        Try
            Dim MI As MethodInfo = nBaseType.GetMethod("IsApplicationWideCache", _
                BindingFlags.NonPublic Or BindingFlags.Static)
            _IsApplicationWide = DirectCast(MI.Invoke(Nothing, Nothing), Boolean)
        Catch ex As Exception
            _IsApplicationWide = False
        End Try

    End Sub

    Public Shared Operator =(ByVal a As CacheItem, ByVal b As CacheItem) As Boolean
        If a Is Nothing AndAlso b Is Nothing Then Return True
        If a Is Nothing OrElse b Is Nothing Then Return False
        Return a._BaseType Is b._BaseType AndAlso a._Type Is b._Type _
            AndAlso a._IsApplicationWide = b._IsApplicationWide _
            AndAlso (a.IsApplicationWide OrElse a.DatabaseName.Trim.ToLower = b.DatabaseName.Trim.ToLower) AndAlso _
            a.AreFilterArgumentsEqual(b._FilterArguments)
    End Operator

    Public Shared Operator <>(ByVal a As CacheItem, ByVal b As CacheItem) As Boolean
        Return Not a = b
    End Operator

    Public Shared Operator >(ByVal a As CacheItem, ByVal b As CacheItem) As Boolean
        If a Is Nothing Then Return False
        If a IsNot Nothing And b Is Nothing Then Return True
        Return a._BaseType.ToString > b._BaseType.ToString
    End Operator

    Public Shared Operator <(ByVal a As CacheItem, ByVal b As CacheItem) As Boolean
        If a Is Nothing And b Is Nothing Then Return False
        If a Is Nothing Then Return True
        If b Is Nothing Then Return False
        Return a._BaseType.ToString < b._BaseType.ToString
    End Operator

    Public Function CompareTo(ByVal obj As Object) As Integer _
        Implements System.IComparable.CompareTo
        Dim tmp As CacheItem = TryCast(obj, CacheItem)
        If Me = tmp Then Return 0
        If Me > tmp Then Return 1
        Return -1
    End Function

    Private Function AreFilterArgumentsEqual(ByVal nFilterArguments As Object()) As Boolean

        If nFilterArguments Is Nothing AndAlso _FilterArguments Is Nothing Then Return True
        If nFilterArguments Is Nothing OrElse _FilterArguments Is Nothing Then Return False
        If nFilterArguments.Length <> _FilterArguments.Length Then Return False

        For i As Integer = 1 To nFilterArguments.Length
            If Not nFilterArguments(i - 1).GetType Is _FilterArguments(i - 1).GetType _
                OrElse (nFilterArguments(i - 1) Is Nothing AndAlso Not _FilterArguments(i - 1) Is Nothing) _
                OrElse (Not nFilterArguments(i - 1) Is Nothing AndAlso _FilterArguments(i - 1) Is Nothing) _
                OrElse (Not nFilterArguments(i - 1) Is Nothing AndAlso Not _FilterArguments(i - 1) Is Nothing _
                AndAlso nFilterArguments(i - 1) <> _FilterArguments(i - 1)) Then Return False
        Next

        Return True

    End Function

End Class
