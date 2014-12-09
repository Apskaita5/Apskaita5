<Serializable()> _
Public Class CustomCacheManager
    Implements AccDataAccessLayer.Security.ICacheManager

    Public Sub AddCompanyInfoToLocalContext(ByVal cDatabase As String, ByVal cPassword As String) _
        Implements AccDataAccessLayer.Security.ICacheManager.AddCompanyInfoToLocalContext

        ApskaitaObjects.Settings.CompanyInfo.LoadCompanyInfoToGlobalContext(cDatabase, cPassword)

    End Sub

    Public Sub ClearCache() _
        Implements AccDataAccessLayer.Security.ICacheManager.ClearCache

    End Sub

End Class
