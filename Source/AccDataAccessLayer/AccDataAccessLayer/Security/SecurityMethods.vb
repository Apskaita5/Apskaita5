Imports System.Configuration
Namespace Security

    Public Module SecurityMethods

        Public Function IsWebServer(ByVal cIsWebServerImpersonation As Boolean) As Boolean

            Return cIsWebServerImpersonation OrElse (ApplicationContext.ExecutionLocation <> ExecutionLocations.Server _
                AndAlso Not ConfigurationManager.AppSettings(AppSettingsKey_IsWebServer) Is Nothing _
                AndAlso Not String.IsNullOrEmpty(ConfigurationManager.AppSettings(AppSettingsKey_IsWebServer).Trim) _
                AndAlso Boolean.Parse(ConfigurationManager.AppSettings(AppSettingsKey_IsWebServer).Trim))

        End Function

        Public Function WebServerUsesCache() As Boolean

            Dim isserver As Boolean = False
            Try
                isserver = DatabaseAccess.GetCurrentIdentity.IsWebServerImpersonation
            Catch ex As Exception
            End Try

            Return (IsWebServer(isserver) AndAlso Not ConfigurationManager.AppSettings(AppSettingsKey_WebServerUsesCache) Is Nothing _
                AndAlso Not String.IsNullOrEmpty(ConfigurationManager.AppSettings(AppSettingsKey_WebServerUsesCache).Trim) _
                AndAlso Boolean.Parse(ConfigurationManager.AppSettings(AppSettingsKey_WebServerUsesCache).Trim))

        End Function

        Public Function AuthenticateOnServer() As Boolean
            If ApplicationContext.ExecutionLocation <> ExecutionLocations.Server Then Return True
            DatabaseAccess.GetCurrentIdentity.AuthenticateOnServer()
            Return True
        End Function

    End Module

End Namespace

