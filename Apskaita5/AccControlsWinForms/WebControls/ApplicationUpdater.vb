Imports System.Text
Imports System.Windows.Forms

Namespace WebControls

    ''' <summary>
    ''' Represents a control that encapsulates application online update functionality.
    ''' </summary>
    ''' <remarks></remarks>
    Public NotInheritable Class ApplicationUpdater

        Public Delegate Sub CloseApplicationForUpdate()

        Private _ApplicationName As String = ""
        Private _ApplicationCurrentVersion As Date = Today
        Private _IsPortableVersion As Boolean = False
        Private _UpdateFileUrl As String = ""
        Private _UpdateFileName As String = ""
        Private _UpdateInstallFileName As String = ""
        Private _UpdateFileEncoding As Encoding = Nothing
        Private _CloseApplicationForUpdateMethod As CloseApplicationForUpdate = Nothing
        Private _WebClient As System.Net.WebClient = Nothing


        ''' <summary>
        ''' Gets or sets a name of the application which online update is handled.
        ''' </summary>
        ''' <remarks></remarks>
        Public Property ApplicationName() As String
            Get
                Return _ApplicationName.Trim
            End Get
            Set(ByVal value As String)
                If value Is Nothing Then value = ""
                If _ApplicationName.Trim <> value.Trim Then
                    _ApplicationName = value.Trim
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a current versio of the application which online update is handled
        ''' (version means last update date).
        ''' </summary>
        ''' <remarks></remarks>
        Public Property ApplicationCurrentVersion() As Date
            Get
                Return _ApplicationCurrentVersion
            End Get
            Set(ByVal value As Date)
                If _ApplicationCurrentVersion.Date <> value.Date Then
                    _ApplicationCurrentVersion = value.Date
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets whether the application which online update is handled
        ''' is portable (version), i.e. not installed.
        ''' </summary>
        ''' <remarks></remarks>
        Public Property IsPortableVersion() As Boolean
            Get
                Return _IsPortableVersion
            End Get
            Set(ByVal value As Boolean)
                If _IsPortableVersion <> value Then
                    _IsPortableVersion = value
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets an URL of the file that contains update data (version, url's to download
        ''' the update files, description).
        ''' </summary>
        ''' <remarks></remarks>
        Public Property UpdateFileUrl() As String
            Get
                Return _UpdateFileUrl.Trim
            End Get
            Set(ByVal value As String)
                If value Is Nothing Then value = ""
                If _UpdateFileUrl.Trim <> value.Trim Then
                    _UpdateFileUrl = value.Trim
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a name of the file that contains update data (version, url's to download
        ''' the update files, description).
        ''' </summary>
        ''' <remarks></remarks>
        Public Property UpdateFileName() As String
            Get
                Return _UpdateFileName.Trim
            End Get
            Set(ByVal value As String)
                If value Is Nothing Then value = ""
                If _UpdateFileName.Trim <> value.Trim Then
                    _UpdateFileName = value.Trim
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a name of the update install file name as it is saved on the user machine.
        ''' </summary>
        ''' <remarks></remarks>
        Public Property UpdateInstallFileName() As String
            Get
                Return _UpdateInstallFileName.Trim
            End Get
            Set(ByVal value As String)
                If value Is Nothing Then value = ""
                If _UpdateInstallFileName.Trim <> value.Trim Then
                    _UpdateInstallFileName = value.Trim
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a text encoding of the file that contains update data (version, url's to download
        ''' the update files, description). (if null default UTF-8 is used)
        ''' </summary>
        ''' <remarks></remarks>
        Public Property UpdateFileEncoding() As Encoding
            Get
                Return _UpdateFileEncoding
            End Get
            Set(ByVal value As Encoding)
                If value Is Nothing Then
                    value = New UTF8Encoding()
                End If
                _UpdateFileEncoding = value
            End Set
        End Property


        ''' <summary>
        ''' Creates a new instance of ApplicationUpdater.
        ''' </summary>
        ''' <param name="applicationName">a name of the application which online update is handled</param>
        ''' <param name="applicationCurrentVersion">a current versio of the application which online 
        ''' update is handled (version means last update date)</param>
        ''' <param name="isPortableVersion">whether the application is portable, i.e. not installed</param>
        ''' <param name="updateFileUrl">an URL of the file that contains update data (version, 
        ''' url's to download the update files, description)</param>
        ''' <param name="updateFileName">a name of the file that contains update data 
        ''' (version, url's to download the update files, description)</param>
        ''' <param name="updateFileEncoding">a text encoding of the file that contains update 
        ''' data (version, url's to download the update files, description). (if null default UTF-8 is used)</param>
        ''' <param name="updateInstallFileName">a name of the update install file name 
        ''' as it is saved on the user machine</param>
        ''' <param name="closeApplicationForUpdateMethod">a method that closes the application 
        ''' in order to update it</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal applicationName As String, ByVal applicationCurrentVersion As Date, _
            ByVal updateFileUrl As String, ByVal updateFileName As String, ByVal isPortableVersion As Boolean, _
            ByVal updateFileEncoding As Encoding, ByVal updateInstallFileName As String, _
            ByVal closeApplicationForUpdateMethod As CloseApplicationForUpdate)

            If applicationName Is Nothing OrElse String.IsNullOrEmpty(applicationName.Trim) Then
                Throw New ArgumentNullException("applicationName")
            ElseIf updateFileUrl Is Nothing OrElse String.IsNullOrEmpty(updateFileUrl.Trim) Then
                Throw New ArgumentNullException("updateFileUrl")
            ElseIf updateFileName Is Nothing OrElse String.IsNullOrEmpty(updateFileName.Trim) Then
                Throw New ArgumentNullException("updateFileName")
            ElseIf closeApplicationForUpdateMethod Is Nothing Then
                Throw New ArgumentNullException("closeApplicationForUpdateMethod")
            End If

            _ApplicationName = applicationName
            _ApplicationCurrentVersion = applicationCurrentVersion
            _IsPortableVersion = isPortableVersion
            _UpdateFileUrl = updateFileUrl
            _UpdateFileName = updateFileName
            _UpdateFileEncoding = updateFileEncoding
            _UpdateInstallFileName = updateInstallFileName
            _CloseApplicationForUpdateMethod = closeApplicationForUpdateMethod

            If _UpdateFileEncoding Is Nothing Then
                _UpdateFileEncoding = New UTF8Encoding()
            End If

        End Sub


        Public Sub CheckForUpdate()

            Dim content As String = ""

            Using frm As New DownloadDataForm(GetFullUpdateUrl(), _UpdateFileEncoding, False, "Ieškoma atnaujinimo...")
                Dim result As DialogResult
                result = frm.ShowDialog()
                If result = DialogResult.Cancel Then
                    Exit Sub
                ElseIf frm.DownloadException IsNot Nothing Then
                    ShowError(frm.DownloadException)
                    Exit Sub
                Else
                    content = frm.DownloadedContent
                End If
            End Using

            ParseDownloadedData(content, False)

        End Sub

        Public Sub CheckForUpdateInBackground()

            Try

                Dim updateUrl As New Uri(GetFullUpdateUrl())

                If _WebClient Is Nothing Then
                    _WebClient = New System.Net.WebClient
                    AddHandler _WebClient.DownloadDataCompleted, AddressOf OnDataFetchedFromWeb
                End If

                _WebClient.DownloadDataAsync(updateUrl)

            Catch ex As Exception
            End Try

        End Sub

        Private Sub OnDataFetchedFromWeb(ByVal sender As Object, ByVal e As System.Net.DownloadDataCompletedEventArgs)

            If e.Cancelled OrElse Not e.Error Is Nothing OrElse e.Result Is Nothing OrElse e.Result.Length < 1 Then Exit Sub

            Dim downloadedContent As String = ""

            Try
                downloadedContent = _UpdateFileEncoding.GetString(e.Result)
            Catch ex As Exception
            End Try

            If downloadedContent Is Nothing OrElse String.IsNullOrEmpty(downloadedContent) Then Exit Sub

            ParseDownloadedData(downloadedContent, True)

        End Sub


        Private Function GetFullUpdateUrl() As String

            If _UpdateFileUrl.Contains("{0}") Then
                Return String.Format(_UpdateFileUrl, _UpdateFileName)
            ElseIf _UpdateFileUrl.Trim.EndsWith("/") Then
                Return String.Format("{0}{1}", _UpdateFileUrl, _UpdateFileName)
            Else
                Return String.Format("{0}/{1}", _UpdateFileUrl, _UpdateFileName)
            End If

        End Function

        Private Sub ParseDownloadedData(ByVal downloadedData As String, ByVal isBackground As Boolean)

            Dim lines As String()
            If downloadedData.Contains(vbCrLf) Then
                lines = downloadedData.Split(New String() {vbCrLf}, StringSplitOptions.RemoveEmptyEntries)
            ElseIf downloadedData.Contains(vbCr) Then
                lines = downloadedData.Split(New String() {vbCr}, StringSplitOptions.RemoveEmptyEntries)
            ElseIf downloadedData.Contains(vbLf) Then
                lines = downloadedData.Split(New String() {vbLf}, StringSplitOptions.RemoveEmptyEntries)
            Else
                If isBackground Then
                    Exit Sub
                Else
                    ShowError(New Exception(String.Format("Failed to parse update file data:{0}----------------{1}{2}", _
                        vbCrLf, vbCrLf, downloadedData)))
                    Exit Sub
                End If
            End If

            If lines.Length < 3 Then

                If isBackground Then
                    Exit Sub
                Else
                    ShowError(New Exception(String.Format("Invalid line count in update file:{0}----------------{1}{2}", _
                        vbCrLf, vbCrLf, downloadedData)))
                    Exit Sub
                End If

            End If

            Dim updateDateInFile As Date
            Try
                updateDateInFile = New Date(Integer.Parse(lines(0).Trim.Substring(0, 4)), _
                    Integer.Parse(lines(0).Trim.Substring(5, 2)), _
                    Integer.Parse(lines(0).Trim.Substring(8, 2)))
            Catch ex As Exception
                If isBackground Then
                    Exit Sub
                Else
                    ShowError(New Exception(String.Format("Failed to parse date in update file: {0}", _
                        lines(0))))
                    Exit Sub
                End If
            End Try

            Dim updateUrl As String = lines(1)
            If _IsPortableVersion Then
                updateUrl = lines(2)
            End If

            If updateUrl Is Nothing OrElse String.IsNullOrEmpty(updateUrl.Trim) Then
                If isBackground Then
                    Exit Sub
                Else
                    ShowError(New Exception(String.Format("Failed to parse update file url:{0}----------------{1}{2}", _
                        vbCrLf, vbCrLf, downloadedData)))
                    Exit Sub
                End If
            End If

            Dim updateDescription As String = ""
            For i As Integer = 4 To lines.Length
                updateDescription = updateDescription & lines(i - 1) & vbCrLf
            Next
            updateDescription = updateDescription.Trim

            InstallIfNewer(updateDateInFile, updateUrl, updateDescription, isBackground)

        End Sub

        Private Sub InstallIfNewer(ByVal lastUpdateDate As Date, ByVal updateUrl As String, _
            ByVal updateDescription As String, ByVal isBackground As Boolean)

            If lastUpdateDate.Date > _ApplicationCurrentVersion.Date Then

                Dim msg As String

                If updateDescription Is Nothing OrElse String.IsNullOrEmpty(updateDescription.Trim) Then
                    msg = String.Format("Rastas naujas programos {0} atnaujinimas, paskelbtas {1}.{2}{3}Atnaujinti programą?", _
                        _ApplicationName, lastUpdateDate.ToString("yyyy-MM-dd"), vbCrLf, vbCrLf)
                Else
                    msg = String.Format("Rastas naujas programos {0} atnaujinimas, paskelbtas {1}.{2}Atnaujinimo aprašymas:{3}{4}{5}{6}Atnaujinti programą?", _
                        _ApplicationName, lastUpdateDate.ToString("yyyy-MM-dd"), vbCrLf, vbCrLf, _
                        updateDescription, vbCrLf, vbCrLf)
                End If

                If Not YesOrNo(msg) Then Exit Sub

                Dim installName As String = _UpdateInstallFileName
                If installName Is Nothing OrElse String.IsNullOrEmpty(installName.Trim) Then
                    installName = "AccAppUpdate.exe"
                End If

                Dim updateFilePath As String = ""
                Using fdf As New DownloadFileForm(updateUrl, installName, "Parsisiunčiamas atnaujinimas...")

                    Dim result As DialogResult = fdf.ShowDialog()

                    If result = DialogResult.Cancel Then
                        Exit Sub
                    ElseIf Not fdf.DownloadException Is Nothing Then
                        ShowError(fdf.DownloadException)
                        Exit Sub
                    Else
                        updateFilePath = fdf.DownloadedFilePath
                    End If

                End Using

                Dim shellProcess As New Process

                shellProcess.StartInfo.FileName = updateFilePath
                If _IsPortableVersion Then
                    shellProcess.StartInfo.Arguments = String.Format("-o""{0}""", AppPath())
                End If
                shellProcess.StartInfo.UseShellExecute = True
                shellProcess.StartInfo.WindowStyle = ProcessWindowStyle.Normal

                shellProcess.Start()

                _CloseApplicationForUpdateMethod.Invoke()

            Else

                If Not isBackground Then

                    MsgBox("Programa yra naujausios versijos.", MsgBoxStyle.Information, "Info")

                End If

            End If

        End Sub

    End Class

End Namespace