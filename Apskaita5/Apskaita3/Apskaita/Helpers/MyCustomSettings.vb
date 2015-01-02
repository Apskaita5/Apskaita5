Imports System.Xml.Serialization
Imports System.Xml
Imports System.Text
Public Module MyCustomSettings

    Public Const PortableInstalationIndicator As String = "Portable.dat"
    Public Const PortableInstalationIniFile As String = "config.ini"
    Public Const LastUpdateFileName As String = "LastUpdateA5.txt"
    Public Const UpdateFileName As String = "Apskaita5_setup.exe"
    Public Const UpdateFileNamePortable As String = "Apskaita5Portable.exe"

    Private _SettingsCache As SettingsPersistenceObject = Nothing

    Public Property UseThreadingForDataTransfer() As Boolean
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            InitCache()
            Return _SettingsCache.UseThreadingForDataTransfer
        End Get
        Set(ByVal value As Boolean)
            InitCache()
            _SettingsCache.UseThreadingForDataTransfer = value
        End Set
    End Property

    Public Property AutoReloadData() As Boolean
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            InitCache()
            Return _SettingsCache.AutoReloadData
        End Get
        Set(ByVal value As Boolean)
            InitCache()
            _SettingsCache.AutoReloadData = value
        End Set
    End Property

    Public Property UserName() As String
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            InitCache()
            Return _SettingsCache.UserName.Trim
        End Get
        Set(ByVal value As String)
            InitCache()
            _SettingsCache.UserName = value
        End Set
    End Property

    Public Property UserEmailAccount() As String
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            InitCache()
            Return _SettingsCache.UserEmailAccount.Trim
        End Get
        Set(ByVal value As String)
            InitCache()
            _SettingsCache.UserEmailAccount = value
        End Set
    End Property

    Public Property SmtpServer() As String
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            InitCache()
            Return _SettingsCache.SmtpServer.Trim
        End Get
        Set(ByVal value As String)
            InitCache()
            _SettingsCache.SmtpServer = value
        End Set
    End Property

    Public Property UserEmail() As String
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            InitCache()
            Return _SettingsCache.UserEmail.Trim
        End Get
        Set(ByVal value As String)
            InitCache()
            _SettingsCache.UserEmail = value
        End Set
    End Property

    Public Property UserEmailPassword() As String
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            InitCache()
            Return _SettingsCache.UserEmailPassword
        End Get
        Set(ByVal value As String)
            InitCache()
            _SettingsCache.UserEmailPassword = value
        End Set
    End Property

    Public Property UseSslForEmail() As Boolean
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            InitCache()
            Return _SettingsCache.UseSslForEmail
        End Get
        Set(ByVal value As Boolean)
            InitCache()
            _SettingsCache.UseSslForEmail = value
        End Set
    End Property

    Public Property UseAuthForEmail() As Boolean
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            InitCache()
            Return _SettingsCache.UseAuthForEmail
        End Get
        Set(ByVal value As Boolean)
            InitCache()
            _SettingsCache.UseAuthForEmail = value
        End Set
    End Property

    Public Property SmtpPort() As String
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            InitCache()
            Return _SettingsCache.SmtpPort.Trim
        End Get
        Set(ByVal value As String)
            InitCache()
            _SettingsCache.SmtpPort = value
        End Set
    End Property

    Public Property ShowToolStrip() As Boolean
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            InitCache()
            Return _SettingsCache.ShowToolStrip
        End Get
        Set(ByVal value As Boolean)
            InitCache()
            _SettingsCache.ShowToolStrip = value
        End Set
    End Property

    Public Property AutoUpdate() As Boolean
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            InitCache()
            Return _SettingsCache.AutoUpdate
        End Get
        Set(ByVal value As Boolean)
            InitCache()
            _SettingsCache.AutoUpdate = value
        End Set
    End Property

    Public Property EmailMessageText() As String
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            InitCache()
            Return _SettingsCache.EmailMessageText.Trim
        End Get
        Set(ByVal value As String)
            InitCache()
            _SettingsCache.EmailMessageText = value
        End Set
    End Property

    Public Property CacheTimeout() As Integer
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            InitCache()
            Return _SettingsCache.CacheTimeout
        End Get
        Set(ByVal value As Integer)
            InitCache()
            _SettingsCache.CacheTimeout = value
        End Set
    End Property

    Public Property SignInvoicesWithCompanySignature() As Boolean
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            InitCache()
            Return _SettingsCache.SignInvoicesWithCompanySignature
        End Get
        Set(ByVal value As Boolean)
            InitCache()
            _SettingsCache.SignInvoicesWithCompanySignature = value
        End Set
    End Property

    Public Property SignInvoicesWithLocalUserSignature() As Boolean
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            InitCache()
            Return _SettingsCache.SignInvoicesWithLocalUserSignature
        End Get
        Set(ByVal value As Boolean)
            InitCache()
            _SettingsCache.SignInvoicesWithLocalUserSignature = value
        End Set
    End Property

    Public Property SignInvoicesWithRemoteUserSignature() As Boolean
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            InitCache()
            Return _SettingsCache.SignInvoicesWithRemoteUserSignature
        End Get
        Set(ByVal value As Boolean)
            InitCache()
            _SettingsCache.SignInvoicesWithRemoteUserSignature = value
        End Set
    End Property

    Public Property AllwaysLoginAsLocalUser() As Boolean
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            InitCache()
            Return _SettingsCache.AllwaysLoginAsLocalUser
        End Get
        Set(ByVal value As Boolean)
            InitCache()
            _SettingsCache.AllwaysLoginAsLocalUser = value
        End Set
    End Property

    Public Property ShowDefaultMailClientWindow() As Boolean
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            InitCache()
            Return _SettingsCache.ShowDefaultMailClientWindow
        End Get
        Set(ByVal value As Boolean)
            InitCache()
            _SettingsCache.ShowDefaultMailClientWindow = value
        End Set
    End Property

    Public Property UseDefaultEmailClient() As Boolean
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            InitCache()
            Return _SettingsCache.UseDefaultEmailClient
        End Get
        Set(ByVal value As Boolean)
            InitCache()
            _SettingsCache.UseDefaultEmailClient = value
        End Set
    End Property

    Public Property AutoSizeForm() As Boolean
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            InitCache()
            Return _SettingsCache.AutoSizeForm
        End Get
        Set(ByVal value As Boolean)
            InitCache()
            _SettingsCache.AutoSizeForm = value
        End Set
    End Property

    Public Property AutoSizeDataGridViewColumns() As Boolean
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            InitCache()
            Return _SettingsCache.AutoSizeDataGridViewColumns
        End Get
        Set(ByVal value As Boolean)
            InitCache()
            _SettingsCache.AutoSizeDataGridViewColumns = value
        End Set
    End Property

    Public Property FormPropertiesList() As System.Collections.Specialized.StringCollection
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            InitCache()
            Return _SettingsCache.FormPropertiesList
        End Get
        Set(ByVal value As System.Collections.Specialized.StringCollection)
            InitCache()
            _SettingsCache.FormPropertiesList = value
        End Set
    End Property

    Public Property DataGridViewColumnPropertiesList() As System.Collections.Specialized.StringCollection
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            InitCache()
            Return _SettingsCache.DataGridViewColumnPropertiesList
        End Get
        Set(ByVal value As System.Collections.Specialized.StringCollection)
            InitCache()
            _SettingsCache.DataGridViewColumnPropertiesList = value
        End Set
    End Property

    Public Property AllwaysShowWageSettings() As Boolean
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            InitCache()
            Return _SettingsCache.AllwaysShowWageSettings
        End Get
        Set(ByVal value As Boolean)
            InitCache()
            _SettingsCache.AllwaysShowWageSettings = value
        End Set
    End Property

    Public Property SQLQueryTimeOut() As Integer
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            InitCache()
            Return _SettingsCache.SQLQueryTimeOut
        End Get
        Set(ByVal value As Integer)
            InitCache()
            _SettingsCache.SQLQueryTimeOut = value
        End Set
    End Property

    Public Property LocalUsers() As String
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            InitCache()
            Return _SettingsCache.LocalUsers.Trim
        End Get
        Set(ByVal value As String)
            InitCache()
            _SettingsCache.LocalUsers = value
        End Set
    End Property

    Public Property UserSignature() As String
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            InitCache()
            Return _SettingsCache.UserSignature.Trim
        End Get
        Set(ByVal value As String)
            InitCache()
            _SettingsCache.UserSignature = value
        End Set
    End Property

    Public Property CommonSettings() As String
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            InitCache()
            Return _SettingsCache.CommonSettings.Trim
        End Get
        Set(ByVal value As String)
            InitCache()
            _SettingsCache.CommonSettings = value
        End Set
    End Property

    Public Property BankDocumentPrefix() As String
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            InitCache()
            Return _SettingsCache.BankDocumentPrefix.Trim
        End Get
        Set(ByVal value As String)
            InitCache()
            _SettingsCache.BankDocumentPrefix = value
        End Set
    End Property

    Public Property IgnoreWrongIBAN() As Boolean
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            InitCache()
            Return _SettingsCache.IgnoreWrongIBAN
        End Get
        Set(ByVal value As Boolean)
            InitCache()
            _SettingsCache.IgnoreWrongIBAN = value
        End Set
    End Property

    Public Property UpdateUrl() As String
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            InitCache()
            Return _SettingsCache.UpdateUrl.Trim
        End Get
        Set(ByVal value As String)
            InitCache()
            _SettingsCache.UpdateUrl = value
        End Set
    End Property

    Public ReadOnly Property IsPortableInstalation() As Boolean
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            InitCache()
            Return _SettingsCache.IsPortableInstalation
        End Get
    End Property

    Public ReadOnly Property LastUpdateDate() As Date
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            InitCache()
            Return _SettingsCache.GetLastUpdateDate
        End Get
    End Property

    Public Property AlwaysUseExternalIdInvoicesMade() As Boolean
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            InitCache()
            Return _SettingsCache.AlwaysUseExternalIdInvoicesMade
        End Get
        Set(ByVal value As Boolean)
            InitCache()
            _SettingsCache.AlwaysUseExternalIdInvoicesMade = value
        End Set
    End Property

    Public Property AlwaysUseExternalIdInvoicesReceived() As Boolean
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            InitCache()
            Return _SettingsCache.AlwaysUseExternalIdInvoicesReceived
        End Get
        Set(ByVal value As Boolean)
            InitCache()
            _SettingsCache.AlwaysUseExternalIdInvoicesReceived = value
        End Set
    End Property

    Public Property DefaultInvoiceReceivedItemIsGoods() As Boolean
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            InitCache()
            Return _SettingsCache.DefaultInvoiceReceivedItemIsGoods
        End Get
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Set(ByVal value As Boolean)
            InitCache()
            _SettingsCache.DefaultInvoiceReceivedItemIsGoods = value
        End Set
    End Property

    Public Property DefaultInvoiceMadeItemIsGoods() As Boolean
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            InitCache()
            Return _SettingsCache.DefaultInvoiceMadeItemIsGoods
        End Get
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Set(ByVal value As Boolean)
            InitCache()
            _SettingsCache.DefaultInvoiceMadeItemIsGoods = value
        End Set
    End Property

    Public Property CheckInvoiceReceivedNumber() As Boolean
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            InitCache()
            Return _SettingsCache.CheckInvoiceReceivedNumber
        End Get
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Set(ByVal value As Boolean)
            InitCache()
            _SettingsCache.CheckInvoiceReceivedNumber = value
        End Set
    End Property

    Public Property CheckInvoiceReceivedNumberWithDate() As Boolean
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            InitCache()
            Return _SettingsCache.CheckInvoiceReceivedNumberWithDate
        End Get
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Set(ByVal value As Boolean)
            InitCache()
            _SettingsCache.CheckInvoiceReceivedNumberWithDate = value
        End Set
    End Property

    Public Property CheckInvoiceReceivedNumberWithSupplier() As Boolean
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            InitCache()
            Return _SettingsCache.CheckInvoiceReceivedNumberWithSupplier
        End Get
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Set(ByVal value As Boolean)
            InitCache()
            _SettingsCache.CheckInvoiceReceivedNumberWithSupplier = value
        End Set
    End Property


    Public Sub Save()
        InitCache()
        _SettingsCache.Save()
    End Sub

    Private Sub InitCache()
        If _SettingsCache Is Nothing Then _SettingsCache = _
            SettingsPersistenceObject.GetSettingsPersistenceObject
    End Sub


    <Serializable()> _
Public Class SettingsPersistenceObject

#Region " Business Methods "

        Private _UseThreadingForDataTransfer As Boolean = False
        Private _AutoReloadData As Boolean = False
        Private _UserName As String = ""
        Private _UserEmailAccount As String = ""
        Private _SmtpServer As String = ""
        Private _UserEmail As String = ""
        Private _UserEmailPassword As String = ""
        Private _UseSslForEmail As Boolean = False
        Private _UseAuthForEmail As Boolean = False
        Private _SmtpPort As String = ""
        Private _ShowToolStrip As Boolean = True
        Private _AutoUpdate As Boolean = True
        Private _EmailMessageText As String = ""
        Private _CacheTimeout As Integer = 0
        Private _SignInvoicesWithCompanySignature As Boolean = False
        Private _SignInvoicesWithLocalUserSignature As Boolean = False
        Private _SignInvoicesWithRemoteUserSignature As Boolean = False
        Private _AllwaysLoginAsLocalUser As Boolean = False
        Private _ShowDefaultMailClientWindow As Boolean = False
        Private _UseDefaultEmailClient As Boolean = True
        Private _AutoSizeForm As Boolean = False
        Private _AutoSizeDataGridViewColumns As Boolean = False
        Private _FormPropertiesList As System.Collections.Specialized.StringCollection = Nothing
        Private _DataGridViewColumnPropertiesList As System.Collections.Specialized.StringCollection = Nothing
        Private _AllwaysShowWageSettings As Boolean = False
        Private _SQLQueryTimeOut As Integer = 0
        Private _SQLQueryTimeOutOld As Integer = 0
        Private _LocalUsers As String = ""
        Private _UserSignature As String = ""
        Private _CommonSettings As String = ""
        Private _BankDocumentPrefix As String = ""
        Private _IgnoreWrongIBAN As Boolean = False
        Private _UpdateUrl As String = "http://79.142.116.110:8088/files/"
        Private _IsPortableInstalation As Boolean = False
        Private _LastUpdateDate As Date = Today
        Private _AlwaysUseExternalIdInvoicesMade As Boolean = True
        Private _AlwaysUseExternalIdInvoicesReceived As Boolean = True
        Private _DefaultInvoiceReceivedItemIsGoods As Boolean = False
        Private _DefaultInvoiceMadeItemIsGoods As Boolean = False
        Private _CheckInvoiceReceivedNumber As Boolean = True
        Private _CheckInvoiceReceivedNumberWithDate As Boolean = False
        Private _CheckInvoiceReceivedNumberWithSupplier As Boolean = False



        Public Property UseThreadingForDataTransfer() As Boolean
            Get
                Return _UseThreadingForDataTransfer
            End Get
            Set(ByVal value As Boolean)
                If _UseThreadingForDataTransfer <> value Then
                    _UseThreadingForDataTransfer = value
                End If
            End Set
        End Property

        Public Property AutoReloadData() As Boolean
            Get
                Return _AutoReloadData
            End Get
            Set(ByVal value As Boolean)
                If _AutoReloadData <> value Then
                    _AutoReloadData = value
                End If
            End Set
        End Property

        Public Property UserName() As String
            Get
                Return _UserName.Trim
            End Get
            Set(ByVal value As String)
                If value Is Nothing Then value = ""
                If _UserName.Trim <> value.Trim Then
                    _UserName = value.Trim
                End If
            End Set
        End Property

        Public Property UserEmailAccount() As String
            Get
                Return _UserEmailAccount.Trim
            End Get
            Set(ByVal value As String)
                If value Is Nothing Then value = ""
                If _UserEmailAccount.Trim <> value.Trim Then
                    _UserEmailAccount = value.Trim
                End If
            End Set
        End Property

        Public Property SmtpServer() As String
            Get
                Return _SmtpServer.Trim
            End Get
            Set(ByVal value As String)
                If value Is Nothing Then value = ""
                If _SmtpServer.Trim <> value.Trim Then
                    _SmtpServer = value.Trim
                End If
            End Set
        End Property

        Public Property UserEmail() As String
            Get
                Return _UserEmail.Trim
            End Get
            Set(ByVal value As String)
                If value Is Nothing Then value = ""
                If _UserEmail.Trim <> value.Trim Then
                    _UserEmail = value.Trim
                End If
            End Set
        End Property

        Public Property UserEmailPassword() As String
            Get
                Return _UserEmailPassword.Trim
            End Get
            Set(ByVal value As String)
                If value Is Nothing Then value = ""
                If _UserEmailPassword.Trim <> value.Trim Then
                    _UserEmailPassword = value.Trim
                End If
            End Set
        End Property

        Public Property UseSslForEmail() As Boolean
            Get
                Return _UseSslForEmail
            End Get
            Set(ByVal value As Boolean)
                If _UseSslForEmail <> value Then
                    _UseSslForEmail = value
                End If
            End Set
        End Property

        Public Property UseAuthForEmail() As Boolean
            Get
                Return _UseAuthForEmail
            End Get
            Set(ByVal value As Boolean)
                If _UseAuthForEmail <> value Then
                    _UseAuthForEmail = value
                End If
            End Set
        End Property

        Public Property SmtpPort() As String
            Get
                Return _SmtpPort.Trim
            End Get
            Set(ByVal value As String)
                If value Is Nothing Then value = ""
                If _SmtpPort.Trim <> value.Trim Then
                    _SmtpPort = value.Trim
                End If
            End Set
        End Property

        Public Property ShowToolStrip() As Boolean
            Get
                Return _ShowToolStrip
            End Get
            Set(ByVal value As Boolean)
                If _ShowToolStrip <> value Then
                    _ShowToolStrip = value
                End If
            End Set
        End Property

        Public Property AutoUpdate() As Boolean
            Get
                Return _AutoUpdate
            End Get
            Set(ByVal value As Boolean)
                If _AutoUpdate <> value Then
                    _AutoUpdate = value
                End If
            End Set
        End Property

        Public Property EmailMessageText() As String
            Get
                Return _EmailMessageText.Trim
            End Get
            Set(ByVal value As String)
                If value Is Nothing Then value = ""
                If _EmailMessageText.Trim <> value.Trim Then
                    _EmailMessageText = value.Trim
                End If
            End Set
        End Property

        Public Property CacheTimeout() As Integer
            Get
                Return _CacheTimeout
            End Get
            Set(ByVal value As Integer)
                If _CacheTimeout <> value Then
                    _CacheTimeout = value
                End If
            End Set
        End Property

        Public Property SignInvoicesWithCompanySignature() As Boolean
            Get
                Return _SignInvoicesWithCompanySignature
            End Get
            Set(ByVal value As Boolean)
                If _SignInvoicesWithCompanySignature <> value Then
                    _SignInvoicesWithCompanySignature = value
                End If
            End Set
        End Property

        Public Property SignInvoicesWithLocalUserSignature() As Boolean
            Get
                Return _SignInvoicesWithLocalUserSignature
            End Get
            Set(ByVal value As Boolean)
                If _SignInvoicesWithLocalUserSignature <> value Then
                    _SignInvoicesWithLocalUserSignature = value
                End If
            End Set
        End Property

        Public Property SignInvoicesWithRemoteUserSignature() As Boolean
            Get
                Return _SignInvoicesWithRemoteUserSignature
            End Get
            Set(ByVal value As Boolean)
                If _SignInvoicesWithRemoteUserSignature <> value Then
                    _SignInvoicesWithRemoteUserSignature = value
                End If
            End Set
        End Property

        Public Property AllwaysLoginAsLocalUser() As Boolean
            Get
                Return _AllwaysLoginAsLocalUser
            End Get
            Set(ByVal value As Boolean)
                If _AllwaysLoginAsLocalUser <> value Then
                    _AllwaysLoginAsLocalUser = value
                End If
            End Set
        End Property

        Public Property ShowDefaultMailClientWindow() As Boolean
            Get
                Return _ShowDefaultMailClientWindow
            End Get
            Set(ByVal value As Boolean)
                If _ShowDefaultMailClientWindow <> value Then
                    _ShowDefaultMailClientWindow = value
                End If
            End Set
        End Property

        Public Property UseDefaultEmailClient() As Boolean
            Get
                Return _UseDefaultEmailClient
            End Get
            Set(ByVal value As Boolean)
                If _UseDefaultEmailClient <> value Then
                    _UseDefaultEmailClient = value
                End If
            End Set
        End Property

        Public Property AutoSizeForm() As Boolean
            Get
                Return _AutoSizeForm
            End Get
            Set(ByVal value As Boolean)
                If _AutoSizeForm <> value Then
                    _AutoSizeForm = value
                End If
            End Set
        End Property

        Public Property AutoSizeDataGridViewColumns() As Boolean
            Get
                Return _AutoSizeDataGridViewColumns
            End Get
            Set(ByVal value As Boolean)
                If _AutoSizeDataGridViewColumns <> value Then
                    _AutoSizeDataGridViewColumns = value
                End If
            End Set
        End Property

        Public Property FormPropertiesList() As System.Collections.Specialized.StringCollection
            Get
                Return _FormPropertiesList
            End Get
            Set(ByVal value As System.Collections.Specialized.StringCollection)
                If Not (_FormPropertiesList Is Nothing AndAlso value Is Nothing) _
                    AndAlso Not (Not _FormPropertiesList Is Nothing AndAlso Not value Is Nothing _
                    AndAlso _FormPropertiesList Is value) Then
                    _FormPropertiesList = value
                End If
            End Set
        End Property

        Public Property DataGridViewColumnPropertiesList() As System.Collections.Specialized.StringCollection
            Get
                Return _DataGridViewColumnPropertiesList
            End Get
            Set(ByVal value As System.Collections.Specialized.StringCollection)
                If Not (_DataGridViewColumnPropertiesList Is Nothing AndAlso value Is Nothing) _
                    AndAlso Not (Not _DataGridViewColumnPropertiesList Is Nothing _
                    AndAlso Not value Is Nothing AndAlso _DataGridViewColumnPropertiesList Is value) Then
                    _DataGridViewColumnPropertiesList = value
                End If
            End Set
        End Property

        Public Property AllwaysShowWageSettings() As Boolean
            Get
                Return _AllwaysShowWageSettings
            End Get
            Set(ByVal value As Boolean)
                If _AllwaysShowWageSettings <> value Then
                    _AllwaysShowWageSettings = value
                End If
            End Set
        End Property

        Public Property SQLQueryTimeOut() As Integer
            Get
                Return _SQLQueryTimeOut
            End Get
            Set(ByVal value As Integer)
                If _SQLQueryTimeOut <> value Then
                    _SQLQueryTimeOut = value
                End If
            End Set
        End Property

        Public Property LocalUsers() As String
            Get
                Return _LocalUsers.Trim
            End Get
            Set(ByVal value As String)
                If value Is Nothing Then value = ""
                If _LocalUsers.Trim <> value.Trim Then
                    _LocalUsers = value.Trim
                End If
            End Set
        End Property

        Public Property UserSignature() As String
            Get
                Return _UserSignature.Trim
            End Get
            Set(ByVal value As String)
                If value Is Nothing Then value = ""
                If _UserSignature.Trim <> value.Trim Then
                    _UserSignature = value.Trim
                End If
            End Set
        End Property

        Public Property CommonSettings() As String
            Get
                Return _CommonSettings.Trim
            End Get
            Set(ByVal value As String)
                If value Is Nothing Then value = ""
                If _CommonSettings.Trim <> value.Trim Then
                    _CommonSettings = value.Trim
                End If
            End Set
        End Property

        Public Property BankDocumentPrefix() As String
            Get
                Return _BankDocumentPrefix.Trim
            End Get
            Set(ByVal value As String)
                If value Is Nothing Then value = ""
                If _BankDocumentPrefix.Trim <> value.Trim Then
                    _BankDocumentPrefix = value.Trim
                End If
            End Set
        End Property

        Public Property IgnoreWrongIBAN() As Boolean
            Get
                Return _IgnoreWrongIBAN
            End Get
            Set(ByVal value As Boolean)
                If _IgnoreWrongIBAN <> value Then
                    _IgnoreWrongIBAN = value
                End If
            End Set
        End Property

        Public Property UpdateUrl() As String
            Get
                Return _UpdateUrl.Trim
            End Get
            Set(ByVal value As String)
                If value Is Nothing Then value = ""
                If _UpdateUrl.Trim <> value.Trim Then
                    _UpdateUrl = value.Trim
                End If
            End Set
        End Property

        Public Property AlwaysUseExternalIdInvoicesMade() As Boolean
            Get
                Return _AlwaysUseExternalIdInvoicesMade
            End Get
            Set(ByVal value As Boolean)
                If _AlwaysUseExternalIdInvoicesMade <> value Then
                    _AlwaysUseExternalIdInvoicesMade = value
                End If
            End Set
        End Property

        Public Property AlwaysUseExternalIdInvoicesReceived() As Boolean
            Get
                Return _AlwaysUseExternalIdInvoicesReceived
            End Get
            Set(ByVal value As Boolean)
                If _AlwaysUseExternalIdInvoicesReceived <> value Then
                    _AlwaysUseExternalIdInvoicesReceived = value
                End If
            End Set
        End Property

        Public Property DefaultInvoiceReceivedItemIsGoods() As Boolean
            Get
                Return _DefaultInvoiceReceivedItemIsGoods
            End Get
            Set(ByVal value As Boolean)
                If _DefaultInvoiceReceivedItemIsGoods <> value Then
                    _DefaultInvoiceReceivedItemIsGoods = value
                End If
            End Set
        End Property

        Public Property DefaultInvoiceMadeItemIsGoods() As Boolean
            Get
                Return _DefaultInvoiceMadeItemIsGoods
            End Get
            Set(ByVal value As Boolean)
                If _DefaultInvoiceMadeItemIsGoods <> value Then
                    _DefaultInvoiceMadeItemIsGoods = value
                End If
            End Set
        End Property

        Public Property CheckInvoiceReceivedNumber() As Boolean
            Get
                Return _CheckInvoiceReceivedNumber
            End Get
            Set(ByVal value As Boolean)
                If _CheckInvoiceReceivedNumber <> value Then
                    _CheckInvoiceReceivedNumber = value
                End If
            End Set
        End Property

        Public Property CheckInvoiceReceivedNumberWithDate() As Boolean
            Get
                Return _CheckInvoiceReceivedNumberWithDate
            End Get
            Set(ByVal value As Boolean)
                If _CheckInvoiceReceivedNumberWithDate <> value Then
                    _CheckInvoiceReceivedNumberWithDate = value
                End If
            End Set
        End Property

        Public Property CheckInvoiceReceivedNumberWithSupplier() As Boolean
            Get
                Return _CheckInvoiceReceivedNumberWithSupplier
            End Get
            Set(ByVal value As Boolean)
                If _CheckInvoiceReceivedNumberWithSupplier <> value Then
                    _CheckInvoiceReceivedNumberWithSupplier = value
                End If
            End Set
        End Property


        Public Function IsPortableInstalation() As Boolean
            Return _IsPortableInstalation
        End Function

        Public Function GetLastUpdateDate() As Date
            Return _LastUpdateDate
        End Function


        Public Sub Save()

            If _IsPortableInstalation Then

                Dim serializer As New XmlSerializer(GetType(SettingsPersistenceObject))
                Dim settings As New XmlWriterSettings

                settings.Indent = True
                settings.IndentChars = " "
                settings.Encoding = New System.Text.UnicodeEncoding()

                Using ms As New IO.MemoryStream
                    Using writer As XmlWriter = XmlWriter.Create(ms, settings)
                        serializer.Serialize(writer, Me)
                        IO.File.WriteAllText(IO.Path.Combine(AppPath, PortableInstalationIniFile), _
                            Encoding.Unicode.GetString(ms.ToArray()))
                    End Using
                End Using

            Else

                My.Settings.UseThreadingForDataTransfer = _UseThreadingForDataTransfer
                My.Settings.AutoReloadData = _AutoReloadData
                My.Settings.UserName = _UserName
                My.Settings.UserEmailAccount = _UserEmailAccount
                My.Settings.SmtpServer = _SmtpServer
                My.Settings.UserEmail = _UserEmail
                My.Settings.UserEmailPassword = _UserEmailPassword
                My.Settings.UseSslForEmail = _UseSslForEmail
                My.Settings.UseAuthForEmail = _UseAuthForEmail
                My.Settings.SmtpPort = _SmtpPort
                My.Settings.ShowToolStrip = _ShowToolStrip
                My.Settings.AutoUpdate = _AutoUpdate
                My.Settings.EmailMessageText = _EmailMessageText
                My.Settings.CacheTimeout = Convert.ToUInt32(_CacheTimeout)
                My.Settings.SignInvoicesWithCompanySignature = _SignInvoicesWithCompanySignature
                My.Settings.SignInvoicesWithLocalUserSignature = _SignInvoicesWithLocalUserSignature
                My.Settings.SignInvoicesWithRemoteUserSignature = _SignInvoicesWithRemoteUserSignature
                My.Settings.AllwaysLoginAsLocalUser = _AllwaysLoginAsLocalUser
                My.Settings.ShowDefaultMailClientWindow = _ShowDefaultMailClientWindow
                My.Settings.UseDefaultEmailClient = _UseDefaultEmailClient
                My.Settings.AutoSizeForm = _AutoSizeForm
                My.Settings.AutoSizeDataGridViewColumns = _AutoSizeDataGridViewColumns
                My.Settings.AllwaysShowWageSettings = _AllwaysShowWageSettings
                My.Settings.SQLQueryTimeOut = _SQLQueryTimeOut
                My.Settings.LocalUsers = _LocalUsers
                My.Settings.UserSignature = _UserSignature
                My.Settings.CommonSettings = _CommonSettings
                My.Settings.BankDocumentPrefix = _BankDocumentPrefix
                My.Settings.IgnoreWrongIBAN = _IgnoreWrongIBAN
                My.Settings.UpdateUrl = _UpdateUrl
                My.Settings.FormPropertiesList = _FormPropertiesList
                My.Settings.DataGridViewColumnPropertiesList = _DataGridViewColumnPropertiesList
                My.Settings.AlwaysUseExternalIdInvoicesMade = _AlwaysUseExternalIdInvoicesMade
                My.Settings.AlwaysUseExternalIdInvoicesReceived = _AlwaysUseExternalIdInvoicesReceived
                My.Settings.DefaultInvoiceMadeItemIsGoods = _DefaultInvoiceMadeItemIsGoods
                My.Settings.DefaultInvoiceReceivedItemIsGoods = _DefaultInvoiceReceivedItemIsGoods
                My.Settings.CheckInvoiceReceivedNumber = _CheckInvoiceReceivedNumber
                My.Settings.CheckInvoiceReceivedNumberWithDate = _CheckInvoiceReceivedNumberWithDate
                My.Settings.CheckInvoiceReceivedNumberWithSupplier = _CheckInvoiceReceivedNumberWithSupplier

                My.Settings.Save()

            End If

            If _SQLQueryTimeOut <> _SQLQueryTimeOutOld Then AddSqlQueryTimeoutToLocalContext(_SQLQueryTimeOut)
            _SQLQueryTimeOutOld = _SQLQueryTimeOut

        End Sub


        Public Overrides Function ToString() As String
            Return "SettingsPersistenceObject"
        End Function

#End Region

#Region " Factory And Data Access Methods "

        Public Shared Function GetSettingsPersistenceObject() As SettingsPersistenceObject

            Dim result As SettingsPersistenceObject

            If IO.File.Exists(IO.Path.Combine(AppPath, PortableInstalationIndicator)) Then

                If IO.File.Exists(IO.Path.Combine(AppPath, PortableInstalationIniFile)) Then

                    Dim serializer As New XmlSerializer(GetType(SettingsPersistenceObject))

                    Using ms As New IO.MemoryStream(Encoding.Unicode.GetBytes( _
                        IO.File.ReadAllText(IO.Path.Combine(AppPath, PortableInstalationIniFile))))
                        result = DirectCast(serializer.Deserialize(ms), SettingsPersistenceObject)
                        result._IsPortableInstalation = True
                    End Using

                Else

                    result = New SettingsPersistenceObject
                    result.FetchMySettingsDefault()

                End If

            Else

                result = New SettingsPersistenceObject
                result.FetchMySettings()

            End If

            result.InitializeCommonSettings()
            result.FetchLastUpdateDate()
            result._SQLQueryTimeOutOld = result._SQLQueryTimeOut
            Return result

        End Function


        Public Sub New()
            ' require use of factory methods
        End Sub


        Private Sub FetchMySettings()

            _UseThreadingForDataTransfer = My.Settings.UseThreadingForDataTransfer
            _AutoReloadData = My.Settings.AutoReloadData
            _UserName = My.Settings.UserName
            _UserEmailAccount = My.Settings.UserEmailAccount
            _SmtpServer = My.Settings.SmtpServer
            _UserEmail = My.Settings.UserEmail
            _UserEmailPassword = My.Settings.UserEmailPassword
            _UseSslForEmail = My.Settings.UseSslForEmail
            _UseAuthForEmail = My.Settings.UseAuthForEmail
            _SmtpPort = My.Settings.SmtpPort
            _ShowToolStrip = My.Settings.ShowToolStrip
            _AutoUpdate = My.Settings.AutoUpdate
            _EmailMessageText = My.Settings.EmailMessageText
            _CacheTimeout = Convert.ToInt32(My.Settings.CacheTimeout)
            _SignInvoicesWithCompanySignature = My.Settings.SignInvoicesWithCompanySignature
            _SignInvoicesWithLocalUserSignature = My.Settings.SignInvoicesWithLocalUserSignature
            _SignInvoicesWithRemoteUserSignature = My.Settings.SignInvoicesWithRemoteUserSignature
            _AllwaysLoginAsLocalUser = My.Settings.AllwaysLoginAsLocalUser
            _ShowDefaultMailClientWindow = My.Settings.ShowDefaultMailClientWindow
            _UseDefaultEmailClient = My.Settings.UseDefaultEmailClient
            _AutoSizeForm = My.Settings.AutoSizeForm
            _AutoSizeDataGridViewColumns = My.Settings.AutoSizeDataGridViewColumns
            _AllwaysShowWageSettings = My.Settings.AllwaysShowWageSettings
            _SQLQueryTimeOut = My.Settings.SQLQueryTimeOut
            _LocalUsers = My.Settings.LocalUsers
            _UserSignature = My.Settings.UserSignature
            _CommonSettings = My.Settings.CommonSettings
            _BankDocumentPrefix = My.Settings.BankDocumentPrefix
            _IgnoreWrongIBAN = My.Settings.IgnoreWrongIBAN
            _UpdateUrl = My.Settings.UpdateUrl
            _FormPropertiesList = My.Settings.FormPropertiesList
            _DataGridViewColumnPropertiesList = My.Settings.DataGridViewColumnPropertiesList
            _AlwaysUseExternalIdInvoicesMade = My.Settings.AlwaysUseExternalIdInvoicesMade
            _AlwaysUseExternalIdInvoicesReceived = My.Settings.AlwaysUseExternalIdInvoicesReceived
            _DefaultInvoiceMadeItemIsGoods = My.Settings.DefaultInvoiceMadeItemIsGoods
            _DefaultInvoiceReceivedItemIsGoods = My.Settings.DefaultInvoiceReceivedItemIsGoods
            _CheckInvoiceReceivedNumber = My.Settings.CheckInvoiceReceivedNumber
            _CheckInvoiceReceivedNumberWithDate = My.Settings.CheckInvoiceReceivedNumberWithDate
            _CheckInvoiceReceivedNumberWithSupplier = My.Settings.CheckInvoiceReceivedNumberWithSupplier

        End Sub

        Private Sub FetchMySettingsDefault()

            _UseThreadingForDataTransfer = Boolean.Parse(DirectCast(My.Settings.Properties("UseThreadingForDataTransfer").DefaultValue, String))
            _AutoReloadData = Boolean.Parse(DirectCast(My.Settings.Properties("AutoReloadData").DefaultValue, String))
            _UserName = DirectCast(My.Settings.Properties("UserName").DefaultValue, String)
            _UserEmailAccount = DirectCast(My.Settings.Properties("UserEmailAccount").DefaultValue, String)
            _SmtpServer = DirectCast(My.Settings.Properties("SmtpServer").DefaultValue, String)
            _UserEmail = DirectCast(My.Settings.Properties("UserEmail").DefaultValue, String)
            _UserEmailPassword = DirectCast(My.Settings.Properties("UserEmailPassword").DefaultValue, String)
            _UseSslForEmail = Boolean.Parse(DirectCast(My.Settings.Properties("UseSslForEmail").DefaultValue, String))
            _UseAuthForEmail = Boolean.Parse(DirectCast(My.Settings.Properties("UseAuthForEmail").DefaultValue, String))
            _SmtpPort = DirectCast(My.Settings.Properties("SmtpPort").DefaultValue, String)
            _ShowToolStrip = Boolean.Parse(DirectCast(My.Settings.Properties("ShowToolStrip").DefaultValue, String))
            _AutoUpdate = Boolean.Parse(DirectCast(My.Settings.Properties("AutoUpdate").DefaultValue, String))
            _EmailMessageText = DirectCast(My.Settings.Properties("EmailMessageText").DefaultValue, String)
            _CacheTimeout = Integer.Parse(DirectCast(My.Settings.Properties("CacheTimeout").DefaultValue, String))
            _SignInvoicesWithCompanySignature = Boolean.Parse(DirectCast(My.Settings.Properties("SignInvoicesWithCompanySignature").DefaultValue, String))
            _SignInvoicesWithLocalUserSignature = Boolean.Parse(DirectCast(My.Settings.Properties("SignInvoicesWithLocalUserSignature").DefaultValue, String))
            _SignInvoicesWithRemoteUserSignature = Boolean.Parse(DirectCast(My.Settings.Properties("SignInvoicesWithRemoteUserSignature").DefaultValue, String))
            _AllwaysLoginAsLocalUser = Boolean.Parse(DirectCast(My.Settings.Properties("AllwaysLoginAsLocalUser").DefaultValue, String))
            _ShowDefaultMailClientWindow = Boolean.Parse(DirectCast(My.Settings.Properties("ShowDefaultMailClientWindow").DefaultValue, String))
            _UseDefaultEmailClient = Boolean.Parse(DirectCast(My.Settings.Properties("UseDefaultEmailClient").DefaultValue, String))
            _AutoSizeForm = Boolean.Parse(DirectCast(My.Settings.Properties("AutoSizeForm").DefaultValue, String))
            _AutoSizeDataGridViewColumns = Boolean.Parse(DirectCast(My.Settings.Properties("AutoSizeDataGridViewColumns").DefaultValue, String))
            _AllwaysShowWageSettings = Boolean.Parse(DirectCast(My.Settings.Properties("AllwaysShowWageSettings").DefaultValue, String))
            _SQLQueryTimeOut = Integer.Parse(DirectCast(My.Settings.Properties("SQLQueryTimeOut").DefaultValue, String))
            _LocalUsers = DirectCast(My.Settings.Properties("LocalUsers").DefaultValue, String)
            _UserSignature = DirectCast(My.Settings.Properties("UserSignature").DefaultValue, String)
            _CommonSettings = DirectCast(My.Settings.Properties("CommonSettings").DefaultValue, String)
            _BankDocumentPrefix = DirectCast(My.Settings.Properties("BankDocumentPrefix").DefaultValue, String)
            _IgnoreWrongIBAN = Boolean.Parse(DirectCast(My.Settings.Properties("IgnoreWrongIBAN").DefaultValue, String))
            _UpdateUrl = DirectCast(My.Settings.Properties("UpdateUrl").DefaultValue, String)
            _FormPropertiesList = DirectCast(My.Settings.Properties("FormPropertiesList").DefaultValue, System.Collections.Specialized.StringCollection)
            _DataGridViewColumnPropertiesList = DirectCast(My.Settings.Properties("DataGridViewColumnPropertiesList").DefaultValue, System.Collections.Specialized.StringCollection)
            _AlwaysUseExternalIdInvoicesMade = Boolean.Parse(DirectCast(My.Settings.Properties("AlwaysUseExternalIdInvoicesMade").DefaultValue, String))
            _AlwaysUseExternalIdInvoicesReceived = Boolean.Parse(DirectCast(My.Settings.Properties("AlwaysUseExternalIdInvoicesReceived").DefaultValue, String))
            _DefaultInvoiceMadeItemIsGoods = Boolean.Parse(DirectCast(My.Settings.Properties("DefaultInvoiceMadeItemIsGoods").DefaultValue, String))
            _DefaultInvoiceReceivedItemIsGoods = Boolean.Parse(DirectCast(My.Settings.Properties("DefaultInvoiceReceivedItemIsGoods").DefaultValue, String))
            _CheckInvoiceReceivedNumber = Boolean.Parse(DirectCast(My.Settings.Properties("CheckInvoiceReceivedNumber").DefaultValue, String))
            _CheckInvoiceReceivedNumberWithDate = Boolean.Parse(DirectCast(My.Settings.Properties("CheckInvoiceReceivedNumberWithDate").DefaultValue, String))
            _CheckInvoiceReceivedNumberWithSupplier = Boolean.Parse(DirectCast(My.Settings.Properties("CheckInvoiceReceivedNumberWithSupplier").DefaultValue, String))

            _IsPortableInstalation = True

        End Sub

        Private Sub InitializeCommonSettings()

            ' skip if already initialized
            If Not _CommonSettings Is Nothing AndAlso Not String.IsNullOrEmpty(_CommonSettings.Trim) Then Exit Sub
            _CommonSettings = ApskaitaObjects.Settings.CommonSettings.GetSerializedLocalCopy

        End Sub

        Private Sub FetchLastUpdateDate()
            _LastUpdateDate = Date.Parse(IO.File.ReadAllText(IO.Path.Combine( _
                AppPath(), MyCustomSettings.LastUpdateFileName)).Trim)
        End Sub

#End Region

    End Class

End Module
