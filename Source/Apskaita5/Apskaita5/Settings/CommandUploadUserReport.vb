Imports ApskaitaObjects.My.Resources

Namespace Settings

    ''' <summary>
    ''' Represents an encapsulated method to upload a user report (*.rdl) 
    ''' to the program (either to the install folder or to the (web) server).
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public NotInheritable Class CommandUploadUserReport
        Inherits CommandBase

#Region " Authorization Rules "

        Public Shared Function CanExecuteCommand() As Boolean
            Return ApplicationContext.User.IsInRole("HelperLists.UserReportInfoList2")
        End Function

#End Region

#Region " Client-side Code "

        Private _Result As Boolean = False
        Private _Content As String = ""

        ''' <summary>
        ''' Gets the result of the command execution, i.e. whether 
        ''' the command completed succesfully.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Result() As Boolean
            Get
                Return _Result
            End Get
        End Property

        ''' <summary>
        ''' Gets a content of the report file beeing uploaded.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Content() As String
            Get
                Return _Content
            End Get
        End Property


        Private Sub BeforeServer()
            ' implement code to run on client
            ' before server is called
        End Sub

        Private Sub AfterServer()
            ' implement code to run on client
            ' after server is called
        End Sub

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Uploads a report file (*.rdl) to the program.
        ''' </summary>
        ''' <param name="fileName">a path to the file to upload</param>
        ''' <param name="fileEncoding">an encoding of the file to upload
        ''' (if set to nothing a default UTF8 (with BOM) encoding will be used)</param>
        ''' <remarks></remarks>
        Public Shared Function TheCommand(ByVal fileName As String, _
            ByVal fileEncoding As Text.Encoding) As Boolean

            If fileEncoding Is Nothing Then
                fileEncoding = Text.Encoding.UTF8
            End If

            Return TheCommand(IO.File.ReadAllText(fileName, fileEncoding))

        End Function

        ''' <summary>
        ''' Uploads a report file (*.rdl) to the program.
        ''' </summary>
        ''' <param name="fileContent">a content of the file to upload</param>
        ''' <remarks></remarks>
        Public Shared Function TheCommand(ByVal fileContent As String) As Boolean

            Dim cmd As New CommandUploadUserReport
            cmd._Content = fileContent

            cmd.BeforeServer()
            cmd = DataPortal.Execute(Of CommandUploadUserReport)(cmd)
            cmd.AfterServer()

            UserReportInfoList.InvalidateCache()

            Return cmd.Result

        End Function


        Private Sub New()
            ' require use of factory methods
        End Sub

#End Region

#Region " Server-side Code "

        Protected Overrides Sub DataPortal_Execute()

            If Not CanExecuteCommand() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecuritySelectDenied)

            ValidateReportFile()

            Dim newFileName As String = GetNewReportFileName()

            IO.File.WriteAllText(newFileName, _Content, Text.Encoding.UTF8)

            _Result = True

        End Sub


        Private Sub ValidateReportFile()

            If StringIsNullOrEmpty(_Content) Then
                Throw New Exception(Settings_CommandUploadUserReport_FileContentNull)
            End If

            Dim currentList As UserReportInfoList = UserReportInfoList.GetListChild()
            Dim newReportInfo As UserReportInfo

            Try

                newReportInfo = UserReportInfo.GetUserReportInfo(_Content)

            Catch ex As Exception
                Throw New Exception(String.Format(Settings_CommandUploadUserReport_FileContentInvalid, _
                    vbCrLf, ex.Message), ex)
            End Try

            If currentList.Exists(newReportInfo) Then

                Throw New Exception(String.Format(Settings_CommandUploadUserReport_ReportAlreadyExists, _
                    newReportInfo.Name, newReportInfo.Author, _
                    newReportInfo.Params.Count.ToString))

            End If

        End Sub

        Private Function GetNewReportFileName() As String

            Dim reportFolder As String = IO.Path.Combine(AppPath(), USERREPORTSFOLDER)

            If Not IO.Directory.Exists(reportFolder) Then
                IO.Directory.CreateDirectory(reportFolder)
            End If

            Dim result As String = Nothing

            For i As Integer = 1 To 1000
                result = IO.Path.Combine(reportFolder, _
                    String.Format(USERREPORTFILENAME, i.ToString))
                If Not IO.File.Exists(result) Then
                    Return result
                End If
            Next

            Throw New Exception(String.Format(Settings_CommandUploadUserReport_OutOfFileNames))

        End Function

#End Region

    End Class

End Namespace
