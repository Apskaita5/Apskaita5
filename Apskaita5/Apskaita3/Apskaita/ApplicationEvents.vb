Imports AccDataAccessLayer.Security
Namespace My

    ' The following events are availble for MyApplication:
    ' 
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active. 
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.
    Partial Friend Class MyApplication

        Friend IsInternalShutdown As Boolean = False

        'One of the global exceptions we are catching is not thread safe, 
        'so we need to make it thread safe first.
        Private Delegate Sub SafeApplicationThreadException(ByVal sender As Object, _
            ByVal e As Threading.ThreadExceptionEventArgs)

        Private Sub MyApplication_Startup(ByVal sender As Object, _
            ByVal e As Microsoft.VisualBasic.ApplicationServices.StartupEventArgs) Handles Me.Startup

            'There are three places to catch all global unhandled exceptions:
            'AppDomain.CurrentDomain.UnhandledException event.
            'System.Windows.Forms.Application.ThreadException event.
            'MyApplication.UnhandledException event.
            AddHandler AppDomain.CurrentDomain.UnhandledException, AddressOf AppDomain_UnhandledException
            AddHandler System.Windows.Forms.Application.ThreadException, AddressOf app_ThreadException

            ApskaitaObjects.Utilities.AttachLocalSettingsMethods(AddressOf SaveCommonSettingsLocal, _
                AddressOf GetCommonSettingsLocal)

            If MyCustomSettings.AllwaysLoginAsLocalUser Then

                AccPrincipal.LoginAsLocalUser(AccDataAccessLayer.SqlServerType.SQLite)
                AddSqlQueryTimeoutToLocalContext(MyCustomSettings.SQLQueryTimeOut)

            Else

                F_Login.ShowDialog()
                If Not IsLoggedIn() Then
                    IsInternalShutdown = True
                    e.Cancel = True
                    Global.System.Windows.Forms.Application.Exit()
                ElseIf GetCurrentIdentity.ConnectionType = AccDataAccessLayer.ConnectionType.Local Then
                    AddSqlQueryTimeoutToLocalContext(MyCustomSettings.SQLQueryTimeOut)
                End If

            End If

        End Sub

        Protected Overrides Function OnInitialize(ByVal commandLineArgs As _
            System.Collections.ObjectModel.ReadOnlyCollection(Of String)) As Boolean

            ' Set the display time to 5000 milliseconds (5 seconds). 

            Return MyBase.OnInitialize(commandLineArgs)

        End Function

        Private Sub app_ThreadException(ByVal sender As Object, _
            ByVal e As Threading.ThreadExceptionEventArgs)

            'This is not thread safe, so make it thread safe.
            If MainForm.InvokeRequired Then
                ' Invoke back to the main thread
                MainForm.Invoke(New SafeApplicationThreadException(AddressOf app_ThreadException), _
                    New Object() {sender, e})
            Else
                ShowError(e.Exception)
                If Not MainForm.ActiveMdiChild Is Nothing Then MainForm.ActiveMdiChild.Close()
            End If

        End Sub

        Private Sub AppDomain_UnhandledException(ByVal sender As Object, _
            ByVal e As UnhandledExceptionEventArgs)

            ShowError(DirectCast(e.ExceptionObject, Exception))
            If Not MainForm.ActiveMdiChild Is Nothing Then MainForm.ActiveMdiChild.Close()

        End Sub

        Private Sub MyApplication_UnhandledException(ByVal sender As Object, _
            ByVal e As Microsoft.VisualBasic.ApplicationServices.UnhandledExceptionEventArgs) _
            Handles Me.UnhandledException

            ShowError(e.Exception)
            If Not MainForm.ActiveMdiChild Is Nothing Then MainForm.ActiveMdiChild.Close()

        End Sub

    End Class

End Namespace

