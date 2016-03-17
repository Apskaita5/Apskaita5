Imports System.Windows.Forms
Imports System.IO
Imports Microsoft.Reporting.WinForms

Namespace Printing

    ''' <summary>
    ''' Common methods for report printing functionality.
    ''' </summary>
    ''' <remarks></remarks>
    Public Module CommonMethods

        ''' <summary>
        ''' Adds a LocalReport with the datasource.
        ''' </summary>
        ''' <param name="report">a LocalReport to add with datasources</param>
        ''' <param name="datasource">a datasource of type ReportData to add to the LocalReport</param>
        ''' <param name="numberOfTablesInUse">a number of tables in ReportData 
        ''' that are used by the report</param>
        ''' <param name="rdlcStream">rdlc report definition as Byte()</param>
        ''' <param name="rdlcFileName">rdlc report file name (if rdlcStream is nothing).</param>
        Public Sub AddReportWithDatasource(ByVal report As LocalReport, _
            ByVal datasource As ReportData, ByVal numberOfTablesInUse As Byte, _
            ByVal rdlcStream As Byte(), ByVal rdlcFileName As String)

            If report Is Nothing Then
                Throw New ArgumentNullException("report")
            ElseIf datasource Is Nothing Then
                Throw New ArgumentNullException("datasource")
            End If

            If Not rdlcStream Is Nothing AndAlso rdlcStream.Length < 100 Then
                rdlcStream = Nothing
            End If

            If rdlcFileName Is Nothing Then rdlcFileName = ""

            If numberOfTablesInUse < 0 Then numberOfTablesInUse = 0

            Using gaugeDataSet As New ReportData
                If numberOfTablesInUse > gaugeDataSet.Tables.Count - 2 Then _
                    numberOfTablesInUse = gaugeDataSet.Tables.Count - 2
            End Using

            If rdlcStream Is Nothing AndAlso String.IsNullOrEmpty(rdlcFileName.Trim) Then
                Throw New InvalidOperationException("Klaida. Nenurodyta nei forma, nei jos failo pavadinimas.")
            End If

            AddLocalReportDataSource(report, datasource, "TableCompany")
            AddLocalReportDataSource(report, datasource, "TableGeneral")

            For i As Integer = 1 To numberOfTablesInUse
                AddLocalReportDataSource(report, datasource, "Table" & i.ToString)
            Next

            If rdlcStream Is Nothing Then
                report.ReportPath = rdlcFileName.Trim
            Else
                report.LoadReportDefinition(New IO.MemoryStream(rdlcStream))
            End If

        End Sub

        Private Sub AddLocalReportDataSource(ByVal report As LocalReport, _
            ByVal datasource As ReportData, ByVal tableName As String)

            Dim tableSource As New BindingSource(datasource, tableName)
            Dim reportSource As New ReportDataSource
            reportSource.Value = tableSource
            reportSource.Name = "ReportData_" & tableName.Trim
            report.DataSources.Add(reportSource)

        End Sub

        ''' <summary>
        ''' Disposes and clears a LocalReport's datasources.
        ''' </summary>
        ''' <param name="report">a LocalReport to clear the datasources</param>
        ''' <remarks></remarks>
        Public Sub DisposeLocalReportDatasources(ByRef report As LocalReport)

            For Each ds As Microsoft.Reporting.WinForms.ReportDataSource In report.DataSources
                DirectCast(ds.Value, BindingSource).Dispose()
            Next
            report.DataSources.Clear()

        End Sub


        ''' <summary>
        ''' Prints a report.
        ''' </summary>
        ''' <param name="showPreview">whether to show print preview</param>
        ''' <param name="mdiParentForm">an MdiParent form for the print preview form</param>
        ''' <param name="datasource">a datasource for the report</param>
        ''' <param name="numberOfTablesInUse">a number of tables in the datasource
        ''' that are in use by the report</param>
        ''' <param name="rdlcStream">a report definition (rdlc) stream</param>
        ''' <param name="rdlcFileName">a report definition (rdlc) file name</param>
        ''' <param name="exportFileName">a default file name to use when exporting
        ''' the report from a print preview form (to pdf, xls, etc.)</param>
        ''' <param name="printerName">a name of the printer to print the report to
        ''' (if not specified a print dialog is shown to the user)</param>
        ''' <remarks>Either rdlcStream or rdlcFileName or both should be specified.
        ''' If both rdlcStream and rdlcFileName are specified then rdlcStream takes precedence.</remarks>
        Public Sub PrintReport(ByVal showPreview As Boolean, ByVal mdiParentForm As Form, _
            ByVal datasource As ReportData, ByVal numberOfTablesInUse As Integer, _
            ByVal rdlcStream As Byte(), ByVal rdlcFileName As String, _
            ByVal exportFileName As String, ByVal printerName As String)

            If Not rdlcStream Is Nothing AndAlso rdlcStream.Length < 100 Then
                rdlcStream = Nothing
            End If

            If rdlcFileName Is Nothing Then rdlcFileName = ""

            If printerName Is Nothing Then printerName = ""

            If exportFileName Is Nothing Then exportFileName = ""

            If rdlcStream Is Nothing AndAlso String.IsNullOrEmpty(rdlcFileName.Trim) Then
                Throw New Exception("Klaida. Nenurodyta nei ataskaitos forma, nei jos failo pavadinimas.")
            End If


            If showPreview Then

                Dim frm As F_PrintReport

                If rdlcStream Is Nothing Then

                    frm = New F_PrintReport(datasource, numberOfTablesInUse, _
                        rdlcFileName, exportFileName)

                Else

                    frm = New F_PrintReport(datasource, numberOfTablesInUse, _
                        rdlcStream, exportFileName)

                End If

                frm.MdiParent = mdiParentForm
                frm.Show()

            Else

                Dim numberOfCopies As Integer = 1

                If String.IsNullOrEmpty(printerName.Trim) Then

                    Using dlgPrint As New PrintDialog

                        If dlgPrint.ShowDialog() <> DialogResult.OK Then Exit Sub

                        printerName = dlgPrint.PrinterSettings.PrinterName
                        numberOfCopies = dlgPrint.PrinterSettings.Copies

                    End Using

                End If

                If rdlcStream Is Nothing Then
                    Using printer As New ReportPrinter(datasource, numberOfTablesInUse, rdlcFileName)
                        printer.Print(numberOfCopies, printerName)
                    End Using
                Else
                    Using printer As New ReportPrinter(datasource, numberOfTablesInUse, rdlcStream)
                        printer.Print(numberOfCopies, printerName)
                    End Using
                End If

            End If

        End Sub

        ''' <summary>
        ''' Prints a report to pdf (file) stream.
        ''' </summary>
        ''' <param name="datasource">a datasource for the report</param>
        ''' <param name="numberOfTablesInUse">a number of tables in the datasource
        ''' that are in use by the report</param>
        ''' <param name="rdlcStream">a report definition (rdlc) stream</param>
        ''' <param name="rdlcFileName">a report definition (rdlc) file name</param>
        ''' <remarks>Either rdlcStream or rdlcFileName or both should be specified.
        ''' If both rdlcStream and rdlcFileName are specified then rdlcStream takes precedence.</remarks>
        Public Function PrintReportToPdfStream(ByVal datasource As ReportData, _
            ByVal numberOfTablesInUse As Integer, ByVal rdlcStream As Byte(), _
            ByVal rdlcFileName As String) As Byte()

            Dim result As Byte() = Nothing

            If rdlcStream Is Nothing Then
                Using printer As New ReportPrinter(datasource, numberOfTablesInUse, rdlcFileName)
                    result = printer.PrintToPdfStream()
                End Using
            Else
                Using printer As New ReportPrinter(datasource, numberOfTablesInUse, rdlcStream)
                    result = printer.PrintToPdfStream()
                End Using
            End If

            Return result

        End Function

        ''' <summary>
        ''' Prints a report to a temporary pdf file.
        ''' </summary>
        ''' <param name="datasource">a datasource for the report</param>
        ''' <param name="numberOfTablesInUse">a number of tables in the datasource
        ''' that are in use by the report</param>
        ''' <param name="rdlcStream">a report definition (rdlc) stream</param>
        ''' <param name="rdlcFileName">a report definition (rdlc) file name</param>
        ''' <param name="pdfFileName">a name (without path) of the file to save 
        ''' (if not specified then a default name 'Report' is used)</param>
        ''' <returns>a full path to the saved pdf file</returns>
        ''' <remarks>a file is saved to the <see cref="IO.Path.GetTempPath">Temp</see> folder</remarks>
        Public Function PrintReportToTempPdfFile(ByVal datasource As ReportData, _
            ByVal numberOfTablesInUse As Integer, ByVal rdlcStream As Byte(), _
            ByVal rdlcFileName As String, ByVal pdfFileName As String) As String

            If Not rdlcStream Is Nothing AndAlso rdlcStream.Length < 100 Then
                rdlcStream = Nothing
            End If

            If rdlcFileName Is Nothing Then rdlcFileName = ""

            If rdlcStream Is Nothing AndAlso String.IsNullOrEmpty(rdlcFileName.Trim) Then
                Throw New Exception("Klaida. Nenurodyta nei ataskaitos forma, nei jos failo pavadinimas.")
            End If

            If pdfFileName Is Nothing OrElse String.IsNullOrEmpty(pdfFileName.Trim) Then
                pdfFileName = GetAvailableTempFileName("Report({0}).pdf")
            Else
                pdfFileName = IO.Path.Combine(IO.Path.GetTempPath, pdfFileName.Trim & ".pdf")
                If IO.File.Exists(pdfFileName) Then IO.File.Delete(pdfFileName)
            End If

            Dim bytes As Byte() = PrintReportToPdfStream(datasource, numberOfTablesInUse, _
                rdlcStream, rdlcFileName)

            Using fs As New IO.FileStream(pdfFileName, IO.FileMode.Create)
                fs.Write(bytes, 0, bytes.Length)
                fs.Close()
            End Using

            Return pdfFileName

        End Function

        Private Function GetAvailableTempFileName(ByVal template As String) As String

            Dim result As String = ""

            For i As Integer = 1 To 5000
                result = IO.Path.Combine(IO.Path.GetTempPath, _
                    String.Format(template, i.ToString()))
                If Not IO.File.Exists(result) Then
                    Return result
                End If
            Next

            Throw New IOException(String.Format("Nebėra laisvų temp failų, būtina išvalyti {0} folderį.", _
                IO.Path.GetTempPath()))

        End Function

    End Module

End Namespace