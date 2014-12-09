Imports System.io
Imports System.text
Imports Microsoft.Reporting.WinForms
Imports System.Drawing.Printing
Imports System.Drawing.Imaging
Imports System.Runtime.InteropServices

Friend Class PrintReportInternal
    Implements IDisposable

    Private m_currentPageIndex As Integer
    Private m_streams As IList(Of Stream)
    Private m_Landscape As Boolean = False

    Private ReportDataSet As ReportData
    Private _NumberOfTablesInUse As Byte
    Private _ReportFileName As String = ""
    Private _ReportFileStream As Byte() = Nothing

    Private m_metafile As Metafile

    Friend Sub New(ByVal ReportDataSource As ReportData, ByVal NumberOfTablesInUse As Byte, _
        ByVal ReportFileName As String)

        ReportDataSet = ReportDataSource
        _NumberOfTablesInUse = NumberOfTablesInUse
        _ReportFileName = ReportFileName

    End Sub

    Friend Sub New(ByVal ReportDataSource As ReportData, ByVal NumberOfTablesInUse As Byte, _
        ByVal ReportFileStream As Byte())

        ReportDataSet = ReportDataSource
        _NumberOfTablesInUse = NumberOfTablesInUse
        _ReportFileStream = ReportFileStream

    End Sub

    Private Function CreateStream(ByVal name As String, _
       ByVal fileNameExtension As String, _
       ByVal encoding As Encoding, ByVal mimeType As String, _
       ByVal willSeek As Boolean) As Stream

        Dim stream As Stream = New MemoryStream()
        m_streams.Add(stream)

        Return stream
    End Function

    Private Sub Export(ByVal report As LocalReport, ByVal PrinterName As String)

        Dim PgWidth As String
        Dim PgHeight As String
        Dim MgTop As String
        Dim MgLeft As String
        Dim MgRight As String
        Dim MgBottom As String

        Dim XMLReport As New Xml.XmlDocument()
        Dim ns As New Xml.XmlNamespaceManager(XMLReport.NameTable)

        If _ReportFileStream Is Nothing Then
            XMLReport.Load(_ReportFileName)
        Else
            XMLReport.Load(New IO.MemoryStream(_ReportFileStream))
        End If

        ns.AddNamespace("ns", _
        "http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")
        ns.AddNamespace("rd", _
        "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")

        PgWidth = GetInnerText(XMLReport, ns, "//ns:Report", "ns:PageWidth")
        m_Landscape = (PgWidth.Trim = "11.69in")
        PgHeight = GetInnerText(XMLReport, ns, "//ns:Report", "ns:PageHeight")
        MgTop = GetInnerText(XMLReport, ns, "//ns:Report", "ns:TopMargin")
        MgBottom = GetInnerText(XMLReport, ns, "//ns:Report", "ns:BottomMargin")
        MgLeft = GetInnerText(XMLReport, ns, "//ns:Report", "ns:LeftMargin")
        MgRight = GetInnerText(XMLReport, ns, "//ns:Report", "ns:RightMargin")

        XMLReport = Nothing
        ns = Nothing

        Dim deviceInfo As String = _
        "<DeviceInfo>" + _
        "  <OutputFormat>EMF</OutputFormat>" + _
        "  <PageWidth>" & PgWidth & "</PageWidth>" + _
        "  <PageHeight>" & PgHeight & "</PageHeight>" + _
        "  <MarginLeft>" & MgLeft & "</MarginLeft>" + _
        "  <MarginRight>" & MgRight & "</MarginRight>" + _
        "  <MarginTop>" & MgTop & "</MarginTop>" + _
        "  <MarginBottom>" & MgBottom & "</MarginBottom>" + _
        "  <DpiX>96</DpiX>" + _
        "  <DpiY>96</DpiY>" + _
        "</DeviceInfo>"


        Dim warnings() As Warning = Nothing
        m_streams = New List(Of Stream)()
        report.Render("Image", deviceInfo, AddressOf CreateStream, _
           warnings)

        Dim stream As Stream
        For Each stream In m_streams
            stream.Position = 0
        Next

    End Sub

    Private Function GetInnerText(ByVal XMLReport As Xml.XmlDocument, ByVal ns As Xml.XmlNamespaceManager, _
        ByVal section As String, ByVal key As String) As String

        Dim Node As Xml.XmlNode = XMLReport.DocumentElement.SelectSingleNode(section & "/" & key, ns)
        'return the value or nothing if it doesn't exist
        If Not Node Is Nothing Then
            Return Node.InnerText
        Else
            Return "2cm"
        End If
    End Function

    Private Sub PrintPage(ByVal sender As Object, _
    ByVal ev As PrintPageEventArgs)
        If m_metafile IsNot Nothing Then
            m_metafile.Dispose()
            m_metafile = Nothing
        End If

        ev.PageSettings.Margins.Top = 0
        ev.PageSettings.Margins.Bottom = 0
        ev.PageSettings.Margins.Right = 0
        ev.PageSettings.Margins.Left = 0

        m_metafile = New Metafile(m_streams(m_currentPageIndex))

        SyncLock Me
            ' Set the metafile delegate. 
            Dim width As Integer = m_metafile.Width
            Dim height As Integer = m_metafile.Height
            Dim m_delegate = New Graphics.EnumerateMetafileProc(AddressOf MetafileCallback)
            ' Draw in the rectangle 
            Dim destRect As New Rectangle(0, 0, m_metafile.Width, m_metafile.Height)
            ev.Graphics.EnumerateMetafile(m_metafile, destRect, m_delegate)
            ' Clean up 
            m_delegate = Nothing
        End SyncLock

        m_currentPageIndex += 1
        ev.HasMorePages = (m_currentPageIndex < m_streams.Count)

        m_metafile.Dispose()
        m_metafile = Nothing

    End Sub

    Private Function MetafileCallback(ByVal recordType As EmfPlusRecordType, _
        ByVal flags As Integer, ByVal dataSize As Integer, ByVal data As IntPtr, _
        ByVal callbackData As PlayRecordCallback) As Boolean

        Dim dataArray As Byte() = Nothing
        ' Dance around unmanaged code. 
        If data <> IntPtr.Zero Then
            ' Copy the unmanaged record to a managed byte buffer 
            ' that can be used by PlayRecord. 
            dataArray = New Byte(dataSize - 1) {}
            Marshal.Copy(data, dataArray, 0, dataSize)
        End If
        ' play the record. 
        m_metafile.PlayRecord(recordType, flags, dataSize, dataArray)

        Return True
    End Function

    Private Sub Print(ByVal NumberOfCopies As Integer, Optional ByVal PrinterName As String = "")

        If String.IsNullOrEmpty(PrinterName.Trim) Then
            Dim dlgPrint As New PrintDialog
            dlgPrint.ShowDialog()
            PrinterName = dlgPrint.PrinterSettings.PrinterName
            dlgPrint.Dispose()
        End If

        If m_streams Is Nothing Or m_streams.Count = 0 Then Exit Sub

        Using printDoc As New PrintDocument()

            printDoc.PrinterSettings.PrinterName = PrinterName
            printDoc.DefaultPageSettings.Landscape = m_Landscape
            printDoc.PrinterSettings.Copies = NumberOfCopies

            printDoc.PrinterSettings.DefaultPageSettings.Margins.Left = 0
            printDoc.PrinterSettings.DefaultPageSettings.Margins.Right = 0
            printDoc.PrinterSettings.DefaultPageSettings.Margins.Top = 0
            printDoc.PrinterSettings.DefaultPageSettings.Margins.Bottom = 0
            printDoc.DefaultPageSettings.Margins.Left = 0
            printDoc.DefaultPageSettings.Margins.Right = 0
            printDoc.DefaultPageSettings.Margins.Top = 0
            printDoc.DefaultPageSettings.Margins.Bottom = 0

            If Not printDoc.PrinterSettings.IsValid Then _
                Throw New Exception("Nerastas printeris " & PrinterName & ".")

            AddHandler printDoc.PrintPage, AddressOf PrintPage
            printDoc.Print()

        End Using

    End Sub

    Friend Sub Run(ByVal NumberOfCopies As Integer, Optional ByVal PrinterName As String = "")

        Using report As New LocalReport()
            AddReportWithDatasource(report, ReportDataSet, _NumberOfTablesInUse, _
                _ReportFileStream, _ReportFileName)
            Export(report, PrinterName)
            DisposeLocalReportDatasources(report)
        End Using

        Print(NumberOfCopies, PrinterName)

        m_currentPageIndex = 0

    End Sub

    Friend Function GetPDFStream() As Byte()

        Dim warnings As Microsoft.Reporting.WinForms.Warning() = Nothing
        Dim streamids As String() = Nothing
        Dim mimeType As String = Nothing
        Dim encoding As String = Nothing
        Dim extension As String = Nothing
        Dim bytes As Byte()

        Using report As New LocalReport
            AddReportWithDatasource(report, ReportDataSet, _NumberOfTablesInUse, _
                _ReportFileStream, _ReportFileName)
            bytes = report.Render("PDF", Nothing, mimeType, _
                encoding, extension, streamids, warnings)
            DisposeLocalReportDatasources(report)
        End Using

        Return bytes

    End Function

    Private disposedValue As Boolean = False        ' To detect redundant calls
    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                If Not (m_streams Is Nothing) Then
                    Dim stream As Stream
                    For Each stream In m_streams
                        stream.Close()
                    Next
                    m_streams = Nothing
                End If
                ReportDataSet.Dispose()
            End If
        End If
        Me.disposedValue = True
    End Sub

#Region " IDisposable Support "
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class