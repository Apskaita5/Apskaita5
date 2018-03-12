Imports ApskaitaObjects.My.Resources
Imports System.IO

Namespace Settings

    ''' <summary>
    ''' Represents an encapsulated method to download a user report (*.rdl) 
    ''' from the program (either from the install folder or from the (web) server).
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public NotInheritable Class CommandDownloadUserReport
        Inherits CommandBase

#Region " Authorization Rules "

        Public Shared Function CanExecuteCommand() As Boolean
            Return ApplicationContext.User.IsInRole("HelperLists.UserReportInfoList1")
        End Function

#End Region

#Region " Client-side Code "

        Private _Result As String = ""
        Private _FileName As String = ""

        ''' <summary>
        ''' Gets the result of the command execution, i.e. report file content.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Result() As String
            Get
                Return _Result
            End Get
        End Property

        ''' <summary>
        ''' Gets a name of the report file to be downloaded.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property FileName() As String
            Get
                Return _FileName
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
        ''' Downloads a report file (*.rdl) from the program.
        ''' </summary>
        ''' <param name="fileName">a name of the file to download</param>
        ''' <remarks></remarks>
        Public Shared Function TheCommand(ByVal fileName As String) As String

            Dim cmd As New CommandDownloadUserReport
            cmd._FileName = fileName

            cmd.BeforeServer()
            cmd = DataPortal.Execute(Of CommandDownloadUserReport)(cmd)
            cmd.AfterServer()

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

            Dim reportFolder As String = IO.Path.Combine(AppPath(), USERREPORTSFOLDER)

            If Not IO.Directory.Exists(reportFolder) Then
                Throw New FileNotFoundException(Settings_CommandDownloadUserReport_FileNotFound, _
                    _FileName)
            End If

            Dim filePath As String = IO.Path.Combine(reportFolder, _FileName)
            If Not IO.File.Exists(filePath) Then
                Throw New FileNotFoundException(Settings_CommandDownloadUserReport_FileNotFound, _
                    _FileName)
            End If

            _Result = IO.File.ReadAllText(filePath, Text.Encoding.UTF8)

        End Sub

#End Region

    End Class

End Namespace