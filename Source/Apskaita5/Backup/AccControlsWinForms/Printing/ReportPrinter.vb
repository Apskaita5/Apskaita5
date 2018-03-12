Imports System.IO
Imports System.Text
Imports System.Drawing
Imports System.Windows.Forms
Imports Microsoft.Reporting.WinForms
Imports System.Drawing.Printing
Imports System.Drawing.Imaging
Imports System.Runtime.InteropServices

Namespace Printing

    ''' <summary>
    ''' Represents a virtual printer that can print a report to a printer
    ''' or to a PDF (file) stream.
    ''' </summary>
    ''' <remarks></remarks>
    Friend Class ReportPrinter
        Implements IDisposable

        Private _CurrentPageIndex As Integer
        Private _Streams As IList(Of Stream)
        Private _Metafile As Metafile
        Private _Landscape As Boolean = False

        Private _ReportDataSet As ReportData
        Private _NumberOfTablesInUse As Byte
        Private _RdlcFileName As String = ""
        Private _RdlcFileStream As Byte() = Nothing


        ''' <summary>
        ''' Creates a new ReportPrinter instance for a report which (rdlc) form 
        ''' is stored in a file.
        ''' </summary>
        ''' <param name="reportDataSet">a <see cref="ReportData">report data set</see></param>
        ''' <param name="numberOfTablesInUse">a number of tables in the <see cref="ReportData">
        ''' report data set</see> that the report makes use of</param>
        ''' <param name="rdlcFileName">a name of the rdlc file that the report form is stored in</param>
        ''' <remarks></remarks>
        Friend Sub New(ByVal reportDataSet As ReportData, ByVal numberOfTablesInUse As Byte, _
            ByVal rdlcFileName As String)

            If reportDataSet Is Nothing Then
                Throw New ArgumentNullException("reportDataSet")
            ElseIf rdlcFileName Is Nothing OrElse String.IsNullOrEmpty(rdlcFileName.Trim) Then
                Throw New ArgumentNullException("rdlcFileName")
            End If

            _ReportDataSet = reportDataSet
            _NumberOfTablesInUse = numberOfTablesInUse
            _RdlcFileName = rdlcFileName

        End Sub

        ''' <summary>
        ''' Creates a new ReportPrinter instance for a report which (rdlc file) data 
        ''' is stored in a data stream (Byte()).
        ''' </summary>
        ''' <param name="reportDataSet">a <see cref="ReportData">report data set</see></param>
        ''' <param name="numberOfTablesInUse">a number of tables in the <see cref="ReportData">
        ''' report data set</see> that the report makes use of</param>
        ''' <param name="rdlcFileStream">a report form (rdlc file) stream</param>
        ''' <remarks></remarks>
        Friend Sub New(ByVal reportDataSet As ReportData, ByVal numberOfTablesInUse As Byte, _
            ByVal rdlcFileStream As Byte())

            If reportDataSet Is Nothing Then
                Throw New ArgumentNullException("reportDataSet")
            ElseIf rdlcFileStream Is Nothing OrElse rdlcFileStream.Length < 100 Then
                Throw New ArgumentNullException("rdlcFileStream")
            End If

            _ReportDataSet = reportDataSet
            _NumberOfTablesInUse = numberOfTablesInUse
            _RdlcFileStream = rdlcFileStream

        End Sub


        ''' <summary>
        ''' Prints the report to a printer.
        ''' </summary>
        ''' <param name="numberOfCopies">a number of copies to print</param>
        ''' <param name="printerName">a name of the printer to print the report to</param>
        ''' <remarks></remarks>
        Friend Sub Print(ByVal numberOfCopies As Integer, Optional ByVal printerName As String = "")

            If numberOfCopies < 1 Then numberOfCopies = 1

            Using report As New LocalReport()
                AddReportWithDatasource(report, _ReportDataSet, _NumberOfTablesInUse, _
                    _RdlcFileStream, _RdlcFileName)
                Export(report)
                DisposeLocalReportDatasources(report)
            End Using

            PrintInt(numberOfCopies, printerName)

            _CurrentPageIndex = 0

        End Sub

        ''' <summary>
        ''' Prints the report to a pdf (file) stream.
        ''' </summary>
        ''' <remarks></remarks>
        Friend Function PrintToPdfStream() As Byte()

            Dim warnings As Microsoft.Reporting.WinForms.Warning() = Nothing
            Dim streamids As String() = Nothing
            Dim mimeType As String = Nothing
            Dim encoding As String = Nothing
            Dim extension As String = Nothing
            Dim bytes As Byte() = Nothing

            Using report As New LocalReport
                AddReportWithDatasource(report, _ReportDataSet, _NumberOfTablesInUse, _
                    _RdlcFileStream, _RdlcFileName)
                bytes = report.Render("PDF", Nothing, mimeType, _
                    encoding, extension, streamids, warnings)
                DisposeLocalReportDatasources(report)
            End Using

            Return bytes

        End Function


        Private Function CreateStream(ByVal name As String, _
            ByVal fileNameExtension As String, _
            ByVal encoding As Encoding, ByVal mimeType As String, _
            ByVal willSeek As Boolean) As Stream

            Dim stream As Stream = New MemoryStream()
            _Streams.Add(stream)

            Return stream

        End Function

        Private Sub Export(ByVal report As LocalReport)

            Dim pgWidth As String
            Dim pgHeight As String
            Dim mgTop As String
            Dim mgLeft As String
            Dim mgRight As String
            Dim mgBottom As String

            Dim xmlReport As New Xml.XmlDocument()
            Dim ns As New Xml.XmlNamespaceManager(xmlReport.NameTable)

            If _RdlcFileStream Is Nothing Then
                xmlReport.Load(_RdlcFileName)
            Else
                xmlReport.Load(New IO.MemoryStream(_RdlcFileStream))
            End If

            ns.AddNamespace("ns", _
                "http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition")
            ns.AddNamespace("rd", _
                "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")

            pgWidth = GetInnerText(xmlReport, ns, "//ns:Report", "ns:PageWidth")
            _Landscape = (pgWidth.Trim = "11.69in")
            pgHeight = GetInnerText(xmlReport, ns, "//ns:Report", "ns:PageHeight")
            mgTop = GetInnerText(xmlReport, ns, "//ns:Report", "ns:TopMargin")
            mgBottom = GetInnerText(xmlReport, ns, "//ns:Report", "ns:BottomMargin")
            mgLeft = GetInnerText(xmlReport, ns, "//ns:Report", "ns:LeftMargin")
            mgRight = GetInnerText(xmlReport, ns, "//ns:Report", "ns:RightMargin")

            xmlReport = Nothing
            ns = Nothing

            Dim deviceInfo As String = _
                "<DeviceInfo>" & _
                "  <OutputFormat>EMF</OutputFormat>" & _
                "  <PageWidth>" & pgWidth & "</PageWidth>" & _
                "  <PageHeight>" & pgHeight & "</PageHeight>" & _
                "  <MarginLeft>" & mgLeft & "</MarginLeft>" & _
                "  <MarginRight>" & mgRight & "</MarginRight>" & _
                "  <MarginTop>" & mgTop & "</MarginTop>" & _
                "  <MarginBottom>" & mgBottom & "</MarginBottom>" & _
                "  <DpiX>96</DpiX>" & _
                "  <DpiY>96</DpiY>" & _
                "</DeviceInfo>"

            Dim warnings() As Warning = Nothing
            _Streams = New List(Of Stream)()
            report.Render("Image", deviceInfo, AddressOf CreateStream, warnings)

            Dim stream As Stream
            For Each stream In _Streams
                stream.Position = 0
            Next

        End Sub

        Private Function GetInnerText(ByVal xmlReport As Xml.XmlDocument, _
            ByVal ns As Xml.XmlNamespaceManager, ByVal section As String, _
            ByVal key As String) As String

            Dim node As Xml.XmlNode = xmlReport.DocumentElement.SelectSingleNode(section & "/" & key, ns)
            'return the value or nothing if it doesn't exist
            If Not node Is Nothing Then
                Return node.InnerText
            Else
                Return "2cm"
            End If

        End Function

        Private Sub PrintPage(ByVal sender As Object, ByVal ev As PrintPageEventArgs)

            If _Metafile IsNot Nothing Then
                _Metafile.Dispose()
                _Metafile = Nothing
            End If

            ev.PageSettings.Margins.Top = 0
            ev.PageSettings.Margins.Bottom = 0
            ev.PageSettings.Margins.Right = 0
            ev.PageSettings.Margins.Left = 0

            _Metafile = New Metafile(_Streams(_CurrentPageIndex))

            SyncLock Me
                ' Set the metafile delegate. 
                Dim width As Integer = _Metafile.Width
                Dim height As Integer = _Metafile.Height
                Dim m_delegate = New Graphics.EnumerateMetafileProc(AddressOf MetafileCallback)
                ' Draw in the rectangle 
                Dim destRect As New Rectangle(0, 0, _Metafile.Width, _Metafile.Height)
                ev.Graphics.EnumerateMetafile(_Metafile, destRect, m_delegate)
                ' Clean up 
                m_delegate = Nothing
            End SyncLock

            _CurrentPageIndex += 1
            ev.HasMorePages = (_CurrentPageIndex < _Streams.Count)

            _Metafile.Dispose()
            _Metafile = Nothing

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
            _Metafile.PlayRecord(recordType, flags, dataSize, dataArray)

            Return True
        End Function

        Private Sub PrintInt(ByVal numberOfCopies As Integer, ByVal printerName As String)

            If printerName Is Nothing OrElse String.IsNullOrEmpty(printerName.Trim) Then

                Using dlgPrint As New PrintDialog
                    dlgPrint.ShowDialog()
                    printerName = dlgPrint.PrinterSettings.PrinterName
                End Using

            End If

            If _Streams Is Nothing Or _Streams.Count = 0 Then Exit Sub

            Using printDoc As New PrintDocument()

                printDoc.PrinterSettings.PrinterName = printerName
                printDoc.DefaultPageSettings.Landscape = _Landscape
                printDoc.PrinterSettings.Copies = numberOfCopies

                printDoc.PrinterSettings.DefaultPageSettings.Margins.Left = 0
                printDoc.PrinterSettings.DefaultPageSettings.Margins.Right = 0
                printDoc.PrinterSettings.DefaultPageSettings.Margins.Top = 0
                printDoc.PrinterSettings.DefaultPageSettings.Margins.Bottom = 0
                printDoc.DefaultPageSettings.Margins.Left = 0
                printDoc.DefaultPageSettings.Margins.Right = 0
                printDoc.DefaultPageSettings.Margins.Top = 0
                printDoc.DefaultPageSettings.Margins.Bottom = 0

                If Not printDoc.PrinterSettings.IsValid Then
                    Throw New Exception(String.Format("Nerastas printeris {0}.", printerName))
                End If

                AddHandler printDoc.PrintPage, AddressOf PrintPage

                printDoc.Print()

            End Using

        End Sub


        Private disposedValue As Boolean = False        ' To detect redundant calls
        ' IDisposable
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    If Not (_Streams Is Nothing) Then
                        Dim stream As Stream
                        For Each stream In _Streams
                            stream.Close()
                        Next
                        _Streams = Nothing
                    End If
                    _ReportDataSet.Dispose()
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

End Namespace