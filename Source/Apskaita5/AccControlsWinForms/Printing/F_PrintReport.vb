Namespace Printing

    ''' <summary>
    ''' Represents a print preview form.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class F_PrintReport

        Private _ReportDataSet As ReportData = Nothing
        Private _NumberOfTablesInUse As Byte = 0
        Private _RdlcFileName As String = ""
        Private _RdlcFileStream As Byte() = Nothing
        Private _ExportFileName As String


        ''' <summary>
        ''' Creates a new print preview form for a report which (rdlc) form 
        ''' is stored in a file.
        ''' </summary>
        ''' <param name="reportDataSet">a <see cref="ReportData">report data set</see></param>
        ''' <param name="numberOfTablesInUse">a number of tables in the <see cref="ReportData">
        ''' report data set</see> that the report makes use of</param>
        ''' <param name="rdlcFileName">a name of the rdlc file that the report form is stored in</param>
        ''' <param name="exportFileName">a default file name to use when exporting
        ''' the report from a ReportViewer (to pdf, xls, etc.)</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal reportDataSet As ReportData, ByVal numberOfTablesInUse As Byte, _
            ByVal rdlcFileName As String, Optional ByVal exportFileName As String = "Report")

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            If reportDataSet Is Nothing Then
                Throw New ArgumentNullException("reportDataSet")
            ElseIf rdlcFileName Is Nothing OrElse String.IsNullOrEmpty(rdlcFileName.Trim) Then
                Throw New ArgumentNullException("rdlcFileName")
            End If

            _ReportDataSet = reportDataSet
            _NumberOfTablesInUse = numberOfTablesInUse
            _ExportFileName = exportFileName
            _RdlcFileName = rdlcFileName

        End Sub

        ''' <summary>
        ''' Creates a new print preview form for a report which (rdlc) data 
        ''' is stored in a data stream (Byte()).
        ''' </summary>
        ''' <param name="reportDataSet">a <see cref="ReportData">report data set</see></param>
        ''' <param name="numberOfTablesInUse">a number of tables in the <see cref="ReportData">
        ''' report data set</see> that the report makes use of</param>
        ''' <param name="rdlcFileStream">a report form (rdlc file) stream</param>
        ''' <param name="exportFileName">a default file name to use when exporting
        ''' the report from a ReportViewer (to pdf, xls, etc.)</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal reportDataSet As ReportData, ByVal numberOfTablesInUse As Byte, _
            ByVal rdlcFileStream As Byte(), Optional ByVal exportFileName As String = "Report")

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            If reportDataSet Is Nothing Then
                Throw New ArgumentNullException("reportDataSet")
            ElseIf rdlcFileStream Is Nothing OrElse rdlcFileStream.Length < 100 Then
                Throw New ArgumentNullException("rdlcFileStream")
            End If

            _ReportDataSet = reportDataSet
            _NumberOfTablesInUse = numberOfTablesInUse
            _ExportFileName = exportFileName
            _RdlcFileStream = rdlcFileStream

        End Sub


        Private Sub On_FormClosing(ByVal sender As Object, _
            ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

            Try
                DisposeLocalReportDatasources(Viewer.LocalReport)
            Catch ex As Exception
            End Try
            Try
                Viewer.Dispose()
            Catch ex As Exception
            End Try

            Try
                _ReportDataSet.Dispose()
            Catch ex As Exception
            End Try

        End Sub

        Private Sub F_PrintReport_Load(ByVal sender As System.Object, _
            ByVal e As System.EventArgs) Handles MyBase.Load

            AddReportWithDatasource(Viewer.LocalReport, _ReportDataSet, _
                _NumberOfTablesInUse, _RdlcFileStream, _RdlcFileName)
            Me.Viewer.LocalReport.DisplayName = _ExportFileName
            Me.Viewer.RefreshReport()

        End Sub

        Private Sub OnRenderingComplete(ByVal sender As Object, _
            ByVal e As Microsoft.Reporting.WinForms.RenderingCompleteEventArgs) _
            Handles Viewer.RenderingComplete

            Viewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout)
            Viewer.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent
            Viewer.ZoomPercent = 75
            Viewer.LocalReport.DisplayName = _ExportFileName

        End Sub

    End Class

End Namespace