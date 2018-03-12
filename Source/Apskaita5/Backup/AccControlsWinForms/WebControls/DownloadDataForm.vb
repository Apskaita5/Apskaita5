Imports System.Windows.Forms
Imports System.Text

Namespace WebControls

    Friend Class DownloadDataForm

        Private _UpdateFileUrl As String = ""
        Private _UpdateFileEncoding As Encoding = Nothing
        Private _ShowProgress As Boolean = False
        Private _DownloadedContent As String = ""
        Private _DownloadException As Exception = Nothing
        Private _WebClient As System.Net.WebClient = Nothing
        Private _Canceled As Boolean = False


        Public ReadOnly Property DownloadedContent() As String
            Get
                Return _DownloadedContent
            End Get
        End Property

        Public ReadOnly Property DownloadException() As Exception
            Get
                Return _DownloadException
            End Get
        End Property


        Friend Sub New(ByVal updateFileUrl As String, ByVal updateFileEncoding As Encoding, _
            ByVal showProgress As Boolean, ByVal headerText As String)

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            _UpdateFileUrl = updateFileUrl
            _UpdateFileEncoding = updateFileEncoding
            _ShowProgress = showProgress
            Me.Text = headerText

            If _UpdateFileEncoding Is Nothing Then
                _UpdateFileEncoding = New UTF8Encoding()
            End If

        End Sub


        Private Sub CheckForUpdateForm_Load(ByVal sender As System.Object, _
            ByVal e As System.EventArgs) Handles MyBase.Load

            Try

                Dim updateUrl As New Uri(_UpdateFileUrl)

                _WebClient = New System.Net.WebClient
                AddHandler _WebClient.DownloadDataCompleted, AddressOf OnDownloadCompleted
                If _ShowProgress Then
                    Me.ProgressBar1.Style = ProgressBarStyle.Continuous
                    AddHandler _WebClient.DownloadProgressChanged, AddressOf OnDownloadProgressChanged
                Else
                    Me.ProgressBar1.Style = ProgressBarStyle.Marquee
                End If

                _WebClient.DownloadDataAsync(updateUrl)

            Catch ex As Exception

                _DownloadException = ex

                CleanUp()

                Me.DialogResult = DialogResult.Retry
                Me.Close()

            End Try

        End Sub

        Private Sub CancelDownloadButton_Click(ByVal sender As System.Object, _
            ByVal e As System.EventArgs) Handles CancelDownloadButton.Click

            _WebClient.CancelAsync()

            _Canceled = True

            CleanUp()

            Me.DialogResult = DialogResult.Cancel
            Me.Close()

        End Sub

        Private Sub OnDownloadCompleted(ByVal sender As Object, ByVal e As System.Net.DownloadDataCompletedEventArgs)

            If _Canceled OrElse e.Cancelled Then Exit Sub

            If Not e.Error Is Nothing Then
                _DownloadException = e.Error
            ElseIf e.Result Is Nothing OrElse e.Result.Length < 1 Then
                _DownloadException = New Exception("Web request returned null.")
            Else

                Try
                    _DownloadedContent = _UpdateFileEncoding.GetString(e.Result)
                Catch ex As Exception
                    _DownloadException = New Exception(String.Format("Failed to parse downloaded data: {0}", ex.Message), ex)
                End Try

            End If

            CleanUp()

            If _DownloadException Is Nothing Then
                Me.DialogResult = DialogResult.OK
            Else
                Me.DialogResult = DialogResult.Retry
            End If

            Me.Close()

        End Sub

        Private Sub OnDownloadProgressChanged(ByVal sender As Object, _
            ByVal e As System.Net.DownloadProgressChangedEventArgs)

            Me.ProgressBar1.Value = e.ProgressPercentage

        End Sub


        Private Sub CleanUp()

            If Not _WebClient Is Nothing Then
                Try
                    RemoveHandler _WebClient.DownloadDataCompleted, AddressOf OnDownloadCompleted
                Catch eg As Exception
                End Try
                If _ShowProgress Then
                    Try
                        RemoveHandler _WebClient.DownloadProgressChanged, AddressOf OnDownloadProgressChanged
                    Catch eg As Exception
                    End Try
                End If
                Try
                    _WebClient.Dispose()
                Catch eg As Exception
                End Try
            End If

        End Sub

    End Class

End Namespace