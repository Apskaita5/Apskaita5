Imports System.Reflection

Public Class FyiPrint

    Public Sub New(ByVal dataSource As ActiveReports.InvoiceInfoItemList)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        Dim rdlcFileName As String = ""

        Dim reportSource As AccControlsWinForms.ReportData = _
            Printing.MapObjToReport(dataSource, rdlcFileName, 2, 0, "", Nothing)

        Me.RdlViewer1.SourceFile = New Uri("D:\R_InvoiceInfoList.rdlc")

        'Dim data As New Xml.XmlDocument()
        'data.LoadXml(IO.File.ReadAllText("D:\testReport.rdl"))

        Dim reportDataSource As AccControlsWinForms.ReportData = _
            Printing.MapObjToReport(dataSource, "", 1, 0, "", Nothing)

        Me.RdlViewer1.Report.DataSets("ReportData_TableCompany").SetData(DirectCast(reportDataSource.TableCompany, DataTable))
        Me.RdlViewer1.Report.DataSets("ReportData_TableGeneral").SetData(DirectCast(reportDataSource.TableGeneral, DataTable))
        Me.RdlViewer1.Report.DataSets("ReportData_Table1").SetData(DirectCast(reportDataSource.Table1, DataTable))

        'Dim companyInfo As New DataTable("CompanyInfo")
        'companyInfo.Columns.Add("CompanyName", GetType(String))
        'companyInfo.Columns.Add("CompanyCode", GetType(String))
        'companyInfo.Rows.Add(GetCurrentCompany.Name, GetCurrentCompany.Code)

        'Me.RdlViewer1.Report.DataSets("CompanyInfo").SetData(companyInfo)

        'Dim invoiceInfoItemList As New DataTable("InvoiceInfoItemList")

        'Dim propList As PropertyInfo() = GetType(ActiveReports.InvoiceInfoItem). _
        '    GetProperties(BindingFlags.Instance Or BindingFlags.Public)

        'For Each prop As PropertyInfo In propList
        '    invoiceInfoItemList.Columns.Add(prop.Name, prop.PropertyType)
        'Next

        'For Each item As ActiveReports.InvoiceInfoItem In dataSource
        '    Dim dr As DataRow = invoiceInfoItemList.Rows.Add()
        '    For Each prop As PropertyInfo In propList
        '        dr.Item(prop.Name) = prop.GetValue(item, Nothing)
        '    Next
        'Next

        'Me.RdlViewer1.Report.DataSets("InvoiceInfoItemList").SetData(invoiceInfoItemList)

    End Sub

    Private Sub FyiPrint_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.RdlViewer1.Rebuild()
        'Me.RdlViewer1.Controls.Remove(Me.RdlViewer1.Controls(4))
        'Me.RdlViewer1.Controls.Remove(Me.RdlViewer1.Controls(2))
        'Me.RdlViewer1.Controls(4).Dock = DockStyle.Fill
    End Sub



End Class
