Public Class F_PrintReport

    Private ReportDataSet As ReportData
    Private _NumberOfTablesInUse As Byte
    Private _ReportFileName As String = ""
    Private _ReportFileStream As Byte() = Nothing
    Private _DefaultFileName As String

    Public Sub New(ByVal NewReportDataSet As ReportData, ByVal NumberOfTablesInUse As Byte, _
        ByVal ReportFileName As String, Optional ByVal DefaultFileName As String = "Report")

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ReportDataSet = NewReportDataSet
        _NumberOfTablesInUse = NumberOfTablesInUse
        _DefaultFileName = DefaultFileName
        _ReportFileName = ReportFileName

    End Sub

    Public Sub New(ByVal NewReportDataSet As ReportData, ByVal NumberOfTablesInUse As Byte, _
        ByVal nReportFileStream As Byte(), Optional ByVal DefaultFileName As String = "Report")

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ReportDataSet = NewReportDataSet
        _NumberOfTablesInUse = NumberOfTablesInUse
        _DefaultFileName = DefaultFileName
        _ReportFileStream = nReportFileStream

    End Sub

    Private Sub On_FormClosing(ByVal sender As Object, _
        ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        DisposeLocalReportDatasources(Viewer.LocalReport)
        Viewer.Dispose()
        ReportDataSet.Dispose()

    End Sub

    Private Sub F_PrintReport_Load(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load

        AddReportWithDatasource(Viewer.LocalReport, ReportDataSet, _
            _NumberOfTablesInUse, _ReportFileStream, _ReportFileName)
        Me.Viewer.LocalReport.DisplayName = _DefaultFileName
        Me.Viewer.RefreshReport()

    End Sub

    Private Sub OnRenderingComplete(ByVal sender As Object, _
        ByVal e As Microsoft.Reporting.WinForms.RenderingCompleteEventArgs) _
        Handles Viewer.RenderingComplete
        Viewer.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout)
        Viewer.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.Percent
        Viewer.ZoomPercent = 75
        Viewer.LocalReport.DisplayName = _DefaultFileName
    End Sub

End Class