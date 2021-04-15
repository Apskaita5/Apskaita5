''' <summary>
''' A class that manages login to a company database and cache.
''' </summary>
''' <remarks></remarks>
<Serializable()> _
Public Class CustomCacheManager
    Implements AccDataAccessLayer.Security.ICacheManager

    ''' <summary>
    ''' Loads a <see cref="ApskaitaObjects.Settings.CompanyInfo">CompanyInfo</see>
    ''' to the cache after the login.
    ''' </summary>
    ''' <param name="cDatabase">A name of the database to load a company info from.</param>
    ''' <param name="cPassword">A password of the database (only applicable to file based databases, e.g. SQLite)</param>
    ''' <remarks></remarks>
    Public Sub AddCompanyInfoToLocalContext(ByVal cDatabase As String, ByVal cPassword As String) _
        Implements AccDataAccessLayer.Security.ICacheManager.AddCompanyInfoToLocalContext

        ApskaitaObjects.Settings.CompanyInfo.LoadCompanyInfoToGlobalContext(cDatabase, cPassword)

    End Sub

    ''' <summary>
    ''' Clears local cache after the logout from a company database.
    ''' </summary>
    ''' <remarks>Not used because <see cref="AccDataAccessLayer.CacheManager">CacheManager</see>
    ''' is used for caching.</remarks>
    Public Sub ClearCache() _
        Implements AccDataAccessLayer.Security.ICacheManager.ClearCache

    End Sub

End Class
