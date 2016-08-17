Imports System.Xml.Serialization
Imports System.Xml
Imports System.Text

Namespace Settings

    ''' <summary>
    ''' Represents a program settings persistence object that is xml serialized
    ''' and stored in the <see cref="My.Settings">My.Settings</see> (for installed version)
    ''' or an <see cref="MyCustomSettings.LAST_UPDATE_FILE_NAME">ini file</see>
    ''' (for portable version).
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
Public Class SettingsPersistenceObject

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
        Private _AllwaysLoginAsLocalUser As Boolean = True
        Private _ShowDefaultMailClientWindow As Boolean = False
        Private _UseDefaultEmailClient As Boolean = True
        Private _AutoSizeForm As Boolean = False
        Private _AutoSizeDataGridViewColumns As Boolean = False
        Private _FormPropertiesList As System.Collections.Specialized.StringCollection = Nothing
        Private _ObjectListViewColumnPropertiesList As System.Collections.Specialized.StringCollection = Nothing
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
        Private _EditListViewWithDoubleClick As Boolean = False
        Private _UseHotTracking As Boolean = True
        Private _ShowEmptyListMessage As Boolean = True
        Private _ShowGridLines As Boolean = False
        Private _DefaultActionByDoubleClick As Boolean = False


        ''' <summary>
        ''' Gets or sets whether data operations should be invoked on a separate thread.
        ''' </summary>
        ''' <remarks></remarks>
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

        ''' <summary>
        ''' Gets or sets whether data in a report should be automaticaly reloaded 
        ''' when a related business object changes.
        ''' </summary>
        ''' <remarks></remarks>
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

        ''' <summary>
        ''' Gets or sets a name (first and last) of the program user (an accountant).
        ''' </summary>
        ''' <remarks></remarks>
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

        ''' <summary>
        ''' Gets or sets a user email account.
        ''' </summary>
        ''' <remarks>Only applicable when <see cref="UseDefaultEmailClient">UseDefaultEmailClient</see> 
        ''' is set to FALSE.</remarks>
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

        ''' <summary>
        ''' Gets or sets a user email SMTP server address.
        ''' </summary>
        ''' <remarks>Only applicable when <see cref="UseDefaultEmailClient">UseDefaultEmailClient</see> 
        ''' is set to FALSE.</remarks>
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

        ''' <summary>
        ''' Gets or sets a user email address.
        ''' </summary>
        ''' <remarks>Only applicable when <see cref="UseDefaultEmailClient">UseDefaultEmailClient</see> 
        ''' is set to FALSE.</remarks>
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

        ''' <summary>
        ''' Gets or sets a user email password.
        ''' </summary>
        ''' <remarks>Only applicable when <see cref="UseDefaultEmailClient">UseDefaultEmailClient</see> 
        ''' is set to FALSE.</remarks>
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

        ''' <summary>
        ''' Gets or sets whether the user email is accessed via SSL.
        ''' </summary>
        ''' <remarks>Only applicable when <see cref="UseDefaultEmailClient">UseDefaultEmailClient</see> 
        ''' is set to FALSE.</remarks>
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

        ''' <summary>
        ''' Gets or sets whether the user email is accessed using AUTH protocol.
        ''' </summary>
        ''' <remarks>Only applicable when <see cref="UseDefaultEmailClient">UseDefaultEmailClient</see> 
        ''' is set to FALSE.</remarks>
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

        ''' <summary>
        ''' Gets or sets a user email SMTP server port.
        ''' </summary>
        ''' <remarks>Only applicable when <see cref="UseDefaultEmailClient">UseDefaultEmailClient</see> 
        ''' is set to FALSE.</remarks>
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

        ''' <summary>
        ''' Gets or sets whether the tools strip should be shown on the GUI.
        ''' </summary>
        ''' <remarks></remarks>
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

        ''' <summary>
        ''' Gets or sets whether the program should automaticaly check for updates
        ''' every time the program is started.
        ''' </summary>
        ''' <remarks></remarks>
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

        ''' <summary>
        ''' Gets or sets a default email text used when sending documents/emails 
        ''' from the program.
        ''' </summary>
        ''' <remarks></remarks>
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

        ''' <summary>
        ''' Gets or sets a timeout in seconds for the application cached data.
        ''' </summary>
        ''' <remarks></remarks>
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

        ''' <summary>
        ''' Gets or sets whether the invoices should be signed with the
        ''' <see cref="ApskaitaObjects.General.Company.HeadPersonSignature">
        ''' company's head signature</see>.
        ''' </summary>
        ''' <remarks></remarks>
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

        ''' <summary>
        ''' Gets or sets whether the invoices should be signed with the
        ''' <see cref="UserSignature">program user signature</see>.
        ''' </summary>
        ''' <remarks></remarks>
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

        ''' <summary>
        ''' Gets or sets whether the invoices should be signed with the
        ''' <see cref="AccDataAccessLayer.Security.UserProfile.Signature">
        ''' remote user signature</see> (current user data on server).
        ''' </summary>
        ''' <remarks></remarks>
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

        ''' <summary>
        ''' Gets or sets whether to display (remote) login dialog when the program starts.
        ''' </summary>
        ''' <remarks></remarks>
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

        ''' <summary>
        ''' Gets or sets whether an email window of the default email program
        ''' should be displayed before sending an email message.
        ''' </summary>
        ''' <remarks>Only applicable when <see cref="UseDefaultEmailClient">UseDefaultEmailClient</see>
        ''' is set to TRUE.</remarks>
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

        ''' <summary>
        ''' Gets or sets whether emails should be sent using a default email program
        ''' (that is installed on the user computer) as oposed to using an embeded email client.
        ''' </summary>
        ''' <remarks></remarks>
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

        ''' <summary>
        ''' Gets or sets whether forms should be autosized (as oposed to user sized).
        ''' </summary>
        ''' <remarks></remarks>
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

        ''' <summary>
        ''' Gets or sets whether table columns should be autosized (as oposed to user sized).
        ''' </summary>
        ''' <remarks></remarks>
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

        Public Property ObjectListViewColumnPropertiesList() As System.Collections.Specialized.StringCollection
            Get
                Return _ObjectListViewColumnPropertiesList
            End Get
            Set(ByVal value As System.Collections.Specialized.StringCollection)
                If Not (_ObjectListViewColumnPropertiesList Is Nothing AndAlso value Is Nothing) _
                   AndAlso Not (Not _ObjectListViewColumnPropertiesList Is Nothing _
                    AndAlso Not value Is Nothing AndAlso _ObjectListViewColumnPropertiesList Is value) Then
                    _ObjectListViewColumnPropertiesList = value
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets whether company settings should be displayed every time
        ''' before creating a new <see cref="ApskaitaObjects.Workers.WageSheet">WageSheet</see>.
        ''' </summary>
        ''' <remarks></remarks>
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

        ''' <summary>
        ''' Gets or sets an SQL query time out in seconds. 
        ''' A value of 0 indicates a default timeout (depends on a server type).
        ''' </summary>
        ''' <remarks>Only used localy, not passed to a remote server.</remarks>
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

        ''' <summary>
        ''' Gets a list of user data in the (remote) login form.
        ''' </summary>
        ''' <remarks></remarks>
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

        ''' <summary>
        ''' Gets or sets a signature (facsimile) of the program user (an accountant).
        ''' </summary>
        ''' <remarks></remarks>
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

        ''' <summary>
        ''' Gets or sets a <see cref="ApskaitaObjects.Settings.CommonSettings">
        ''' localy stored common settings XML string</see>.
        ''' </summary>
        ''' <remarks>Used to persist common setttings when working without a remote server.
        ''' See <see cref="ApskaitaObjects.Utilities.AttachLocalSettingsMethods">AttachLocalSettingsMethods</see>
        ''' method for details.</remarks>
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

        ''' <summary>
        ''' Gets or sets a default <see cref="ApskaitaObjects.Documents.BankOperationItemList">
        ''' bank operation document number prefix</see>.
        ''' </summary>
        ''' <remarks></remarks>
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

        ''' <summary>
        ''' Gets or sets whether <see cref="ApskaitaObjects.Documents.BankOperationItemList">
        ''' to ignore IBAN number mismatch when importing bank operations data</see>.
        ''' </summary>
        ''' <remarks></remarks>
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

        ''' <summary>
        ''' Gets or sets an url where the program update files are located.
        ''' </summary>
        ''' <remarks></remarks>
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

        ''' <summary>
        ''' Gets or sets whether to add <see cref="ApskaitaObjects.Documents.InvoiceMade.ExternalID">
        ''' an external ID of the invoice made</see> without the user confirmation
        ''' when copying an invoice from an external source.
        ''' </summary>
        ''' <remarks></remarks>
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

        ''' <summary>
        ''' Gets or sets whether to add <see cref="ApskaitaObjects.Documents.InvoiceReceived.ExternalID">
        ''' an external ID of the invoice received</see> without the user confirmation
        ''' when copying an invoice from an external source.
        ''' </summary>
        ''' <remarks></remarks>
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

        ''' <summary>
        ''' Gets or sets whether the default new item in an invoice received
        ''' should be goods purchase (as oposed to a service bought).
        ''' </summary>
        ''' <remarks></remarks>
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

        ''' <summary>
        ''' Gets or sets whether the default new item in an invoice made
        ''' should be goods sale (as oposed to a service sold).
        ''' </summary>
        ''' <remarks></remarks>
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

        ''' <summary>
        ''' Gets or sets whether to check if the invoice received number is unique.
        ''' </summary>
        ''' <remarks></remarks>
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

        ''' <summary>
        ''' Gets or sets whether to check the invoice received number uniqueness
        ''' taking into account the invoice date.
        ''' </summary>
        ''' <remarks></remarks>
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

        ''' <summary>
        ''' Gets or sets whether to check the invoice received number uniqueness
        ''' taking into account the supplier.
        ''' </summary>
        ''' <remarks></remarks>
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

        ''' <summary>
        ''' Gets or sets whether ObjectListView edit is initiated on double click (instead of single click).
        ''' </summary>
        ''' <remarks></remarks>
        Public Property EditListViewWithDoubleClick() As Boolean
            Get
                Return _EditListViewWithDoubleClick
            End Get
            Set(ByVal value As Boolean)
                If _EditListViewWithDoubleClick <> value Then
                    _EditListViewWithDoubleClick = value
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets whether to use hot item traking in the ObjectListView's. (CPU intensive)
        ''' </summary>
        ''' <remarks></remarks>
        Public Property UseHotTracking() As Boolean
            Get
                Return _UseHotTracking
            End Get
            Set(ByVal value As Boolean)
                If _UseHotTracking <> value Then
                    _UseHotTracking = value
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets whether to show a default empty list message in the ObjectListView's.
        ''' </summary>
        ''' <remarks></remarks>
        Public Property ShowEmptyListMessage() As Boolean
            Get
                Return _ShowEmptyListMessage
            End Get
            Set(ByVal value As Boolean)
                If _ShowEmptyListMessage <> value Then
                    _ShowEmptyListMessage = value
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets whether to show gridlines in the ObjectListView's.
        ''' </summary>
        ''' <remarks></remarks>
        Public Property ShowGridLines() As Boolean
            Get
                Return _ShowGridLines
            End Get
            Set(ByVal value As Boolean)
                If _ShowGridLines <> value Then
                    _ShowGridLines = value
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets whether to perform a default action when 
        ''' an ObjectListView is double clicked (as oposed to showing 
        ''' a meniu of available actions).
        ''' </summary>
        ''' <remarks></remarks>
        Public Property DefaultActionByDoubleClick() As Boolean
            Get
                Return _DefaultActionByDoubleClick
            End Get
            Set(ByVal value As Boolean)
                If _DefaultActionByDoubleClick <> value Then
                    _DefaultActionByDoubleClick = value
                End If
            End Set
        End Property


        Public Sub New()

        End Sub


        ''' <summary>
        ''' Indicates whether the current program is running as portable,
        ''' i.e. all resources including settings are stored in the application folder.
        ''' </summary>
        ''' <remarks></remarks>
        Public Function IsPortableInstalation() As Boolean
            Return _IsPortableInstalation
        End Function

        ''' <summary>
        ''' Gets a date when the application was last updated.
        ''' </summary>
        ''' <remarks></remarks>
        Public Function GetLastUpdateDate() As Date
            Return _LastUpdateDate
        End Function


        ''' <summary>
        ''' Converts the current SettingsPersistenceObject instance to an XML string for storage.
        ''' </summary>
        ''' <remarks></remarks>
        Public Function ConvertToXmlString() As String

            Dim serializer As New XmlSerializer(GetType(SettingsPersistenceObject))
            Dim settings As New XmlWriterSettings

            settings.Indent = True
            settings.IndentChars = " "
            settings.Encoding = New System.Text.UnicodeEncoding()

            Using ms As New IO.MemoryStream
                Using writer As XmlWriter = XmlWriter.Create(ms, settings)
                    serializer.Serialize(writer, Me)
                    Return Encoding.Unicode.GetString(ms.ToArray())
                End Using
            End Using

        End Function

        ''' <summary>
        ''' Gets a new SettingsPersistenceObject instance using a stored XML string.
        ''' </summary>
        ''' <param name="xmlString">a stored XML string that contains program settings data
        ''' (see <see cref="ConvertToXmlString">ConvertToXmlString</see> method)</param>
        ''' <remarks></remarks>
        Public Shared Function GetFromXmlString(ByVal xmlString As String) As SettingsPersistenceObject

            Dim serializer As New XmlSerializer(GetType(SettingsPersistenceObject))

            Using ms As New IO.MemoryStream(Encoding.Unicode.GetBytes(xmlString))
                Return DirectCast(serializer.Deserialize(ms), SettingsPersistenceObject)
            End Using

        End Function


        ''' <summary>
        ''' Initializes a new SettingsPersistenceObject instance after retrieving
        ''' it from the storage.
        ''' </summary>
        ''' <param name="markAsPortable">whether the current application is running as portable</param>
        ''' <param name="applicationPath">the current application (install/location) path</param>
        ''' <remarks></remarks>
        Friend Sub Initialize(ByVal markAsPortable As Boolean, ByVal applicationPath As String)

            _IsPortableInstalation = markAsPortable
            _SQLQueryTimeOutOld = _SQLQueryTimeOut
            Try
                _LastUpdateDate = Date.Parse(IO.File.ReadAllText(IO.Path.Combine( _
                    applicationPath, MyCustomSettings.LAST_UPDATE_FILE_NAME)).Trim)
            Catch ex As Exception
                _LastUpdateDate = New Date(1969, 8, 15)
            End Try

        End Sub

        ''' <summary>
        ''' Marks the current SettingsPersistenceObject instance as old after it has been saved
        ''' to the storage.
        ''' </summary>
        ''' <remarks></remarks>
        Friend Sub MarkAsOld()

            If _SQLQueryTimeOut <> _SQLQueryTimeOutOld Then
                AccDataAccessLayer.DatabaseAccess.AddSqlQueryTimeoutToLocalContext(_SQLQueryTimeOut)
            End If
            _SQLQueryTimeOut = _SQLQueryTimeOutOld

        End Sub

    End Class

End Namespace