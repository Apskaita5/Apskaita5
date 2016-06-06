Imports System.Configuration
Imports AccDataBindingsWinForms.Settings

Module CommonMethods

    Friend Function GetMySettings() As SettingsPersistenceObject

        FixCorruptedSettings()

        Dim result As New AccDataBindingsWinForms.Settings.SettingsPersistenceObject

        result.UseThreadingForDataTransfer = My.Settings.UseThreadingForDataTransfer
        result.AutoReloadData = My.Settings.AutoReloadData
        result.UserName = My.Settings.UserName
        result.UserEmailAccount = My.Settings.UserEmailAccount
        result.SmtpServer = My.Settings.SmtpServer
        result.UserEmail = My.Settings.UserEmail
        result.UserEmailPassword = My.Settings.UserEmailPassword
        result.UseSslForEmail = My.Settings.UseSslForEmail
        result.UseAuthForEmail = My.Settings.UseAuthForEmail
        result.SmtpPort = My.Settings.SmtpPort
        result.ShowToolStrip = My.Settings.ShowToolStrip
        result.AutoUpdate = My.Settings.AutoUpdate
        result.EmailMessageText = My.Settings.EmailMessageText
        result.CacheTimeout = Convert.ToInt32(My.Settings.CacheTimeout)
        result.SignInvoicesWithCompanySignature = My.Settings.SignInvoicesWithCompanySignature
        result.SignInvoicesWithLocalUserSignature = My.Settings.SignInvoicesWithLocalUserSignature
        result.SignInvoicesWithRemoteUserSignature = My.Settings.SignInvoicesWithRemoteUserSignature
        result.AllwaysLoginAsLocalUser = My.Settings.AllwaysLoginAsLocalUser
        result.ShowDefaultMailClientWindow = My.Settings.ShowDefaultMailClientWindow
        result.UseDefaultEmailClient = My.Settings.UseDefaultEmailClient
        result.AutoSizeForm = My.Settings.AutoSizeForm
        result.AutoSizeDataGridViewColumns = My.Settings.AutoSizeDataGridViewColumns
        result.AllwaysShowWageSettings = My.Settings.AllwaysShowWageSettings
        result.SQLQueryTimeOut = My.Settings.SQLQueryTimeOut
        result.LocalUsers = My.Settings.LocalUsers
        result.UserSignature = My.Settings.UserSignature
        result.CommonSettings = My.Settings.CommonSettings
        result.BankDocumentPrefix = My.Settings.BankDocumentPrefix
        result.IgnoreWrongIBAN = My.Settings.IgnoreWrongIBAN
        result.UpdateUrl = My.Settings.UpdateUrl
        result.FormPropertiesList = My.Settings.FormPropertiesList
        result.ObjectListViewColumnPropertiesList = My.Settings.ObjectListViewColumnPropertiesList
        result.EditListViewWithDoubleClick = My.Settings.EditListViewWithDoubleClick
        result.AlwaysUseExternalIdInvoicesMade = My.Settings.AlwaysUseExternalIdInvoicesMade
        result.AlwaysUseExternalIdInvoicesReceived = My.Settings.AlwaysUseExternalIdInvoicesReceived
        result.DefaultInvoiceMadeItemIsGoods = My.Settings.DefaultInvoiceMadeItemIsGoods
        result.DefaultInvoiceReceivedItemIsGoods = My.Settings.DefaultInvoiceReceivedItemIsGoods
        result.CheckInvoiceReceivedNumber = My.Settings.CheckInvoiceReceivedNumber
        result.CheckInvoiceReceivedNumberWithDate = My.Settings.CheckInvoiceReceivedNumberWithDate
        result.CheckInvoiceReceivedNumberWithSupplier = My.Settings.CheckInvoiceReceivedNumberWithSupplier
        result.UseHotTracking = My.Settings.UseHotTracking
        result.ShowEmptyListMessage = My.Settings.ShowEmptyListMessage
        result.ShowGridLines = My.Settings.ShowGridLines

        Return result

    End Function

    Friend Sub SaveMySettings(ByVal settings As SettingsPersistenceObject)

        My.Settings.UseThreadingForDataTransfer = settings.UseThreadingForDataTransfer
        My.Settings.AutoReloadData = settings.AutoReloadData
        My.Settings.UserName = settings.UserName
        My.Settings.UserEmailAccount = settings.UserEmailAccount
        My.Settings.SmtpServer = settings.SmtpServer
        My.Settings.UserEmail = settings.UserEmail
        My.Settings.UserEmailPassword = settings.UserEmailPassword
        My.Settings.UseSslForEmail = settings.UseSslForEmail
        My.Settings.UseAuthForEmail = settings.UseAuthForEmail
        My.Settings.SmtpPort = settings.SmtpPort
        My.Settings.ShowToolStrip = settings.ShowToolStrip
        My.Settings.AutoUpdate = settings.AutoUpdate
        My.Settings.EmailMessageText = settings.EmailMessageText
        My.Settings.CacheTimeout = Convert.ToUInt32(settings.CacheTimeout)
        My.Settings.SignInvoicesWithCompanySignature = settings.SignInvoicesWithCompanySignature
        My.Settings.SignInvoicesWithLocalUserSignature = settings.SignInvoicesWithLocalUserSignature
        My.Settings.SignInvoicesWithRemoteUserSignature = settings.SignInvoicesWithRemoteUserSignature
        My.Settings.AllwaysLoginAsLocalUser = settings.AllwaysLoginAsLocalUser
        My.Settings.ShowDefaultMailClientWindow = settings.ShowDefaultMailClientWindow
        My.Settings.UseDefaultEmailClient = settings.UseDefaultEmailClient
        My.Settings.AutoSizeForm = settings.AutoSizeForm
        My.Settings.AutoSizeDataGridViewColumns = settings.AutoSizeDataGridViewColumns
        My.Settings.AllwaysShowWageSettings = settings.AllwaysShowWageSettings
        My.Settings.SQLQueryTimeOut = settings.SQLQueryTimeOut
        My.Settings.LocalUsers = settings.LocalUsers
        My.Settings.UserSignature = settings.UserSignature
        My.Settings.CommonSettings = settings.CommonSettings
        My.Settings.BankDocumentPrefix = settings.BankDocumentPrefix
        My.Settings.IgnoreWrongIBAN = settings.IgnoreWrongIBAN
        My.Settings.UpdateUrl = settings.UpdateUrl
        My.Settings.FormPropertiesList = settings.FormPropertiesList
        My.Settings.ObjectListViewColumnPropertiesList = settings.ObjectListViewColumnPropertiesList
        My.Settings.EditListViewWithDoubleClick = settings.EditListViewWithDoubleClick
        My.Settings.AlwaysUseExternalIdInvoicesMade = settings.AlwaysUseExternalIdInvoicesMade
        My.Settings.AlwaysUseExternalIdInvoicesReceived = settings.AlwaysUseExternalIdInvoicesReceived
        My.Settings.DefaultInvoiceMadeItemIsGoods = settings.DefaultInvoiceMadeItemIsGoods
        My.Settings.DefaultInvoiceReceivedItemIsGoods = settings.DefaultInvoiceReceivedItemIsGoods
        My.Settings.CheckInvoiceReceivedNumber = settings.CheckInvoiceReceivedNumber
        My.Settings.CheckInvoiceReceivedNumberWithDate = settings.CheckInvoiceReceivedNumberWithDate
        My.Settings.CheckInvoiceReceivedNumberWithSupplier = settings.CheckInvoiceReceivedNumberWithSupplier
        My.Settings.UseHotTracking = settings.UseHotTracking
        My.Settings.ShowEmptyListMessage = settings.ShowEmptyListMessage
        My.Settings.ShowGridLines = settings.ShowGridLines

        My.Settings.Save()

    End Sub

    Private Sub FixCorruptedSettings()

        Try
            Dim k As Boolean = My.Settings.AllwaysLoginAsLocalUser
        Catch ex As System.Configuration.ConfigurationErrorsException

            Dim fileName As String = ""

            If Not StringIsNullOrEmpty(ex.Filename) AndAlso ex.Filename.EndsWith("user.config") Then

                fileName = ex.Filename

            ElseIf Not ex.InnerException Is Nothing Then

                If TypeOf ex.InnerException Is ConfigurationErrorsException Then

                    Dim configurationErrorsException As ConfigurationErrorsException = _
                        DirectCast(ex.InnerException, ConfigurationErrorsException)

                    If Not StringIsNullOrEmpty(configurationErrorsException.Filename) _
                        AndAlso configurationErrorsException.Filename.EndsWith("user.config") Then
                        fileName = configurationErrorsException.Filename
                    End If

                End If

            End If

            If Not StringIsNullOrEmpty(fileName) AndAlso fileName.EndsWith("user.config") Then
                Try
                    IO.File.Delete(fileName)
                Catch e As Exception
                    Throw New Exception(String.Format("Klaida. Dėl nežinomų priežasčių buvo sugadinti programos nustatymai. Siekiant ištaisyti klaidą reikia ištrinti failą {0}.", fileName), ex)
                End Try
                Throw New Exception("Klaida. Dėl nežinomų priežasčių buvo sugadinti programos nustatymai. Bandykite paleisti programą iš naujo.", ex)
            Else
                Throw
            End If

        End Try

    End Sub

End Module
