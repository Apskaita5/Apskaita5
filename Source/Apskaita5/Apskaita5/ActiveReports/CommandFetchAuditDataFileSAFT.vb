Namespace ActiveReports

    ''' <summary>
    ''' Represents a command that returns a SAF-T accounting data.
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()>
    Public NotInheritable Class CommandFetchAuditDataFileSAFT
        Inherits CommandBase

#Region " Authorization Rules "

        Public Shared Function CanExecuteCommand() As Boolean
            Return ApplicationContext.User.IsInRole("Reports.AuditFileSAFT1")
        End Function

#End Region

#Region " Client-side Code "

        Private _Result As Byte() = Nothing
        Private _Version As IAuditFileSAFT = Nothing
        Private _DateFrom As Date = Today
        Private _DateTo As Date = Today
        Private _SoftwareVersion As String = String.Empty


        ''' <summary>
        ''' SAF-T (XML) accounting data.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Result() As Byte()
            Get
                Return _Result
            End Get
        End Property

        ''' <summary>
        ''' Gets a version of the XSD schema to use.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Version() As IAuditFileSAFT
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _Version
            End Get
        End Property

        ''' <summary>
        ''' Gets a starting date of the period that the accounting data is fetched for.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DateFrom() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _DateFrom
            End Get
        End Property

        ''' <summary>
        ''' Gets an ending date of the period that the accounting data is fetched for.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DateTo() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _DateTo
            End Get
        End Property

        ''' <summary>
        ''' Gets a version of the Apskaita5 that requested to fetch the audit data.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property SoftwareVersion As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _SoftwareVersion
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
        ''' Gets SAF-T GZIP'ed accounting (XML) data for the period requested.
        ''' </summary>
        ''' <param name="version">a version of the XSD schema to use</param>
        ''' <param name="dateFrom">a starting date of the period that the accounting data is fetched for</param>
        ''' <param name="dateTo">an ending date of the period that the accounting data is fetched for</param>
        ''' <param name="softwareVersion">a version of the Apskaita5 that requested to fetch the audit data</param>
        ''' <remarks></remarks>
        Public Shared Function TheCommand(ByVal version As IAuditFileSAFT, ByVal dateFrom As Date,
            ByVal dateTo As Date, softwareVersion As String) As String

            Dim cmd As New CommandFetchAuditDataFileSAFT
            cmd._Version = version
            cmd._DateFrom = dateFrom
            cmd._DateTo = dateTo
            cmd._SoftwareVersion = softwareVersion

            cmd.BeforeServer()
            cmd = DataPortal.Execute(Of CommandFetchAuditDataFileSAFT)(cmd)
            cmd.AfterServer()

            Return DeCompress(cmd.Result)

        End Function

        Private Sub New()
            ' require use of factory methods
        End Sub

#End Region

#Region " Server-side Code "

        Protected Overrides Sub DataPortal_Execute()

            If Not CanExecuteCommand() Then Throw New System.Security.SecurityException(
                My.Resources.Common_SecuritySelectDenied)

            If _Version Is Nothing Then Throw New Exception("Klaida. Nenurodyta SAF-T versija.")

            _Result = Compress(Version.GetXmlString(_SoftwareVersion, _DateFrom, _DateTo))

        End Sub

        Private Shared Function Compress(ByVal source As String) As Byte()

            Using ms As New System.IO.MemoryStream
                Using gzip As New IO.Compression.GZipStream(ms, IO.Compression.CompressionMode.Compress, True)
                    Dim encoding As New System.Text.UTF8Encoding(False)
                    Dim sourceBytes As Byte() = encoding.GetBytes(source)
                    gzip.Write(sourceBytes, 0, sourceBytes.Length)
                End Using
                Return ms.ToArray()
            End Using

        End Function

        Private Shared Function DeCompress(ByVal source As Byte()) As String

            Using ms As New System.IO.MemoryStream(source)
                Using gzip As New IO.Compression.GZipStream(ms, IO.Compression.CompressionMode.Decompress, False)
                    Dim size As Integer = 4096
                    Dim buffer(size) As Byte
                    Using os As New System.IO.MemoryStream
                        Dim count As Integer = 0
                        Do
                            count = gzip.Read(buffer, 0, size)
                            If count > 0 Then os.Write(buffer, 0, count)
                        Loop While count > 0
                        Dim encoding As New System.Text.UTF8Encoding(False)
                        Return encoding.GetString(os.ToArray())
                    End Using
                End Using
            End Using

        End Function

#End Region

    End Class

End Namespace
