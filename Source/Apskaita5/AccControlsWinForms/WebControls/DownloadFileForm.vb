Imports System.IO
Imports System.Net.Security
Imports System.Security.Cryptography.X509Certificates

Namespace WebControls

    Public Class DownloadFileForm

        Private _FileUrl As String = ""
        Private _DownloadedFilePath As String = ""
        Private _FileName As String = ""
        Private _DownloadException As Exception = Nothing
        Private _WebClient As System.Net.WebClient = Nothing
        Private _Canceled As Boolean = False


        Public ReadOnly Property DownloadedFilePath() As String
            Get
                Return _DownloadedFilePath
            End Get
        End Property

        Public ReadOnly Property DownloadException() As Exception
            Get
                Return _DownloadException
            End Get
        End Property


        Friend Sub New(ByVal fileUrl As String, ByVal fileName As String, ByVal headerText As String)

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            _FileUrl = fileUrl
            _FileName = fileName
            Me.Text = headerText

        End Sub


        Private Sub DownloadFileForm_Load(ByVal sender As System.Object, _
            ByVal e As System.EventArgs) Handles MyBase.Load

            Try

                Dim updateUrl As New Uri(_FileUrl)

                _DownloadedFilePath = GetFilePath()

                _WebClient = New System.Net.WebClient
                _WebClient.Headers.Add(Net.HttpRequestHeader.UserAgent, "blahhhhhh")
                AddHandler _WebClient.DownloadFileCompleted, AddressOf OnDownloadComplete
                AddHandler _WebClient.DownloadProgressChanged, AddressOf OnDownloadProgressChanged

                Net.ServicePointManager.SecurityProtocol = Net.SecurityProtocolType.Ssl3
                Net.ServicePointManager.ServerCertificateValidationCallback =
                    AddressOf ValidateServerCertificate

                _WebClient.DownloadFileAsync(updateUrl, _DownloadedFilePath)

            Catch ex As Exception

                _DownloadException = ex

                CleanUp()

                Me.DialogResult = System.Windows.Forms.DialogResult.Retry
                Me.Close()

            End Try

        End Sub


        Private Sub CancelUpdateButton_Click(ByVal sender As System.Object, _
            ByVal e As System.EventArgs) Handles CancelUpdateButton.Click

            _WebClient.CancelAsync()

            _Canceled = True

            CleanUp()

            Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
            Me.Close()

        End Sub

        Private Sub OnDownloadComplete(ByVal sender As Object, ByVal e As System.ComponentModel.AsyncCompletedEventArgs)

            If _Canceled OrElse e.Cancelled Then Exit Sub

            If e.Error Is Nothing Then
                Me.DialogResult = System.Windows.Forms.DialogResult.OK
            Else
                _DownloadException = e.Error
                Me.DialogResult = System.Windows.Forms.DialogResult.Retry
            End If

            CleanUp()

            Me.Close()

        End Sub

        Private Sub OnDownloadProgressChanged(ByVal sender As Object, _
            ByVal e As System.Net.DownloadProgressChangedEventArgs)

            Me.ProgressBar1.Value = e.ProgressPercentage

        End Sub


        Private Function GetFilePath() As String

            Dim baseName As String = IO.Path.GetFileNameWithoutExtension(_FileName)
            Dim extension As String = IO.Path.GetExtension(_FileName)

            For i As Integer = 1 To 10000
                Dim path As String = IO.Path.Combine(IO.Path.GetTempPath(), baseName _
                    & "{" & i.ToString() & ")" & extension)
                If Not IO.File.Exists(path) Then Return path
            Next

            Throw New IOException("Temp file folder is full, please clean temp folder.")

        End Function

        Private Shared Function ValidateServerCertificate(sender As Object,
            certificate As X509Certificate, chain As X509Chain, sslPolicyErrors As SslPolicyErrors) As Boolean
            Return True
        End Function

        Private Sub CleanUp()

            If Not _WebClient Is Nothing Then
                Try
                    RemoveHandler _WebClient.DownloadDataCompleted, AddressOf OnDownloadComplete
                Catch eg As Exception
                End Try
                Try
                    RemoveHandler _WebClient.DownloadProgressChanged, AddressOf OnDownloadProgressChanged
                Catch eg As Exception
                End Try
                Try
                    Net.ServicePointManager.ServerCertificateValidationCallback = Nothing
                Catch eg As Exception
                End Try
                Try
                    _WebClient.Dispose()
                Catch eg As Exception
                End Try
            End If

        End Sub

    End Class

End Namespace
