Public Module ProjectConstants

    ''' <summary>
    ''' Provides LocalContext key name for a transaction object. 
    ''' Object type depends on the type of a server in use. (MySQLCommand for MySQL)
    ''' </summary>
    Friend Const LC_TransactionObjectKey As String = "TransComm"
    ''' <summary>
    ''' Provides LocalContext key name for SQL commands executed in the current transaction.
    ''' </summary>
    Friend Const LC_TransactionStatementListKey As String = "TransList"

    ' All paths are relative to:
    ' In case of winforms - to program instalation folder.
    ' In case of webservice - to App_Data. (see Helpers\Utilities.AppPath)
    Friend Const Path_DatabaseStructureGauge As String = "\DbStructure\DatabaseStructureGauge.xml"
    Friend Const Path_RoleDatabaseAccessGauge As String = "\DbStructure\RoleDatabaseAccessGauge.xml"
    ''' <summary>
    ''' All database files should be in this folder (for SQL file server, e.g. SQLite).
    ''' </summary>
    Friend Const Path_FileServerDatabaseFiles As String = "\Data"

    ''' <summary>
    ''' All database files should have this extension (for SQL file server, e.g. SQLite).
    ''' </summary>
    Friend Const Name_FileServerDatabaseFilesExtension As String = ".db"
    ''' <summary>
    ''' Could be used when database part of connection string could not be added to the
    ''' end of string, stored in AppSettingsKey_SqlConnectionString. (e.g. SQLite)
    ''' When generating connection string for database this tag is replaced by database name.
    ''' </summary>
    Friend Const Name_DatabaseTagInConnString As String = "**DBName"
    ''' <summary>
    ''' Security database name is made by replacing the end of database naming convention 
    ''' with Name_SecurityDatabaseCode
    ''' </summary>
    Friend Const Name_SecurityDatabaseCode As String = "sec"
    Public Const Name_AdminRole As String = "Admin"
    Friend Const Default_SqlQueryTimeOut As Integer = 1200

    ' AppSettings key-value pairs, used by application server to identify SQL server
    ' See WebServiceTemplates\web.config for details.
    Friend Const AppSettingsKey_SqlServerName As String = "SQLServerName"
    Friend Const AppSettingsKey_SqlPort As String = "SQLServerPort"
    Friend Const AppSettingsKey_SqlServerType As String = "SQLServerType"
    Friend Const AppSettingsKey_SqlDefaultUserHost As String = "SQLDefaultUserHost"
    Friend Const AppSettingsKey_SqlQueryTimeOut As String = "SQLQueryTimeOut"
    Friend Const AppSettingsKey_DenyUserProfileUpdate As String = "DenyUserProfileUpdate"
    Friend Const AppSettingsKey_DatabaseNamingConvention As String = "DatabaseNamingConvention"
    Friend Const AppSettingsKey_UseSSL As String = "UseSSLForSQL"
    Friend Const AppSettingsKey_SSLCertificateFile As String = "SSLCertificateFile"
    Friend Const AppSettingsKey_SSLCertificatePassword As String = "SSLCertificatePassword"
    Friend Const AppSettingsKey_SSLCertificateInstalled As String = "SSLCertificateInstalled"
    Friend Const AppSettingsKey_CannotSetSqlGrants As String = "CannotSetSqlGrants"
    Friend Const AppSettingsKey_SecuritySystemInternal As String = "SecuritySystemInternal"
    Friend Const AppSettingsKey_SqlConnectionString As String = "SqlConnectionString"
    Friend Const AppSettingsKey_IsWebServer As String = "IsWebServer"
    Friend Const AppSettingsKey_WebServerUsesCache As String = "WebServerUsesCache"
    Friend Const AppSettingsKey_ApplicationServerSecret As String = "ApplicationServerSecret"

    ' AppSettings key-value pairs, used by CSLA framework to select connection technology
    ' DO NOT MODIFY
    Friend Const AppSettingsKey_Proxy As String = "CslaDataPortalProxy"
    Friend Const AppSettingsKey_URL As String = "CslaDataPortalUrl"
    Friend Const AppSettingsKey_Remoting As String = "Csla.DataPortalClient.RemotingProxy, Csla"
    Friend Const AppSettingsKey_Enterprise As String = "EnterpriseServicesHost.EnterpriseServicesProxy,"
    Friend Const AppSettingsKey_WebService As String = "Csla.DataPortalClient.WebServicesProxy, Csla"

End Module
