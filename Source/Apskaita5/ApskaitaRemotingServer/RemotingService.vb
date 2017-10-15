Imports System.Runtime.Remoting
Imports System.Runtime.Remoting.Channels.Http
Imports System.Configuration
Imports System.Runtime.Remoting.Channels

Public Class RemotingService

    Private _Channel As HttpServerChannel = Nothing


    Protected Overrides Sub OnStart(ByVal args() As String)
        ' Add code here to start your service. This method should set things
        ' in motion so your service can do its work.

        If _Channel Is Nothing Then

            Dim port As Integer = 8080
            Try
                port = Convert.ToInt32(ConfigurationManager.AppSettings("ExternalServicePort"))
            Catch ex As Exception
            End Try

            Dim serviceNameInUrl As String = Nothing
            Try
                serviceNameInUrl = ConfigurationManager.AppSettings("ExternalServiceName")
            Catch ex As Exception
            End Try

            If serviceNameInUrl Is Nothing OrElse String.IsNullOrEmpty(serviceNameInUrl.Trim) Then
                serviceNameInUrl = "Apskaita5Remoting"
            End If

            _Channel = New HttpServerChannel(port)

            ChannelServices.RegisterChannel(_Channel, False)

            RemotingConfiguration.RegisterWellKnownServiceType( _
                GetType(Csla.Server.Hosts.RemotingPortal), _
                String.Format("{0}.rem", serviceNameInUrl), WellKnownObjectMode.Singleton)

        End If

        _Channel.StartListening(Nothing)

    End Sub

    Protected Overrides Sub OnStop()
        ' Add code here to perform any tear-down necessary to stop your service.

        If Not _Channel Is Nothing Then
            _Channel.StopListening(Nothing)
        End If

    End Sub

End Class
