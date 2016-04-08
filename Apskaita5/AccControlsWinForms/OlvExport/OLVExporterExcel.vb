Imports BrightIdeasSoftware
Imports System.Windows.Forms
Imports System.IO
Imports OfficeOpenXml
Imports OfficeOpenXml.Style
Imports System.Reflection

Namespace OlvExport

    Public Class OLVExporterExcel
        Implements IOLVExporter


        Public ReadOnly Property Icon() As System.Drawing.Image Implements IOLVExporter.Icon
            Get
                Return My.Resources.excel
            End Get
        End Property

        Public ReadOnly Property Name() As String Implements IOLVExporter.Name
            Get
                Return "Excel failas"
            End Get
        End Property


        Public Sub Export(ByVal olv As ObjectListView) Implements IOLVExporter.Export

            Dim filePath As String

            Using saveFile As New SaveFileDialog
                saveFile.Filter = "Excel failai (*.xlsx)|*.xlsx|Visi failai|*.*"
                saveFile.AddExtension = True
                saveFile.DefaultExt = ".xlsx"
                If saveFile.ShowDialog() <> Windows.Forms.DialogResult.OK Then Exit Sub
                filePath = saveFile.FileName
            End Using

            If filePath Is Nothing OrElse String.IsNullOrEmpty(filePath.Trim) Then Exit Sub

            Dim fileExisted As Boolean = IO.File.Exists(filePath)

            Try
                Using busy As New StatusBusy
                    Using pck As ExcelPackage = New ExcelPackage(New FileInfo(filePath))

                        Dim sheet As ExcelWorksheet = pck.Workbook.Worksheets.Add("Content")
                        sheet.Name = "ExportedTable"

                        DoExport(olv, sheet)

                        pck.Save()

                    End Using
                End Using
            Catch ex As Exception
                ShowError(ex)
                If Not fileExisted AndAlso IO.File.Exists(filePath) Then
                    Try
                        IO.File.Delete(filePath)
                    Catch e As Exception
                    End Try
                End If
                Exit Sub
            End Try

            If Not YesOrNo("Failas buvo sėkmingai išsaugotas. Atidaryti?") Then Exit Sub

            Process.Start(filePath)

        End Sub

        Private Sub DoExport(ByVal olv As ObjectListView, ByVal sheet As ExcelWorksheet)

            Dim colCounter As Integer = 1

            For Each column As OLVColumn In olv.Columns
                sheet.Cells(1, colCounter).Value = column.Text
                sheet.Cells(1, colCounter).Style.Font.Bold = True
                sheet.Cells(1, colCounter).Style.WrapText = True
                sheet.Cells(1, colCounter).Style.VerticalAlignment = ExcelVerticalAlignment.Bottom
                sheet.Cells(1, colCounter).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center
                sheet.Cells(1, colCounter).Style.Border.Bottom.Style = ExcelBorderStyle.Medium
                sheet.Cells(1, colCounter).Style.Border.Top.Style = ExcelBorderStyle.Medium
                sheet.Column(colCounter).BestFit = True
                colCounter += 1
            Next

            Dim lastDisplayedItem As OLVListItem = olv.GetLastItemInDisplayOrder()

            If lastDisplayedItem Is Nothing Then Exit Sub

            Dim parentList As IList = Nothing

            If Not olv.Objects Is Nothing Then

                If TypeOf olv.Objects Is BindingSource Then
                    Try
                        parentList = DirectCast(olv.Objects, BindingSource).List
                    Catch ex As Exception
                    End Try
                Else
                    Try
                        parentList = DirectCast(olv.Objects, IList)
                    Catch ex As Exception
                    End Try
                End If

            End If

            If parentList Is Nothing OrElse parentList.Count < 1 Then Exit Sub

            Dim propInfo As PropertyInfo
            Dim formatString As String

            colCounter = 1
            For Each column As OLVColumn In olv.Columns

                propInfo = Nothing
                Try
                    propInfo = parentList.Item(0).GetType.GetProperty(column.AspectName)
                Catch ex As Exception
                End Try

                If Not propInfo Is Nothing Then

                    formatString = ""
                    If Not column.AspectToStringFormat Is Nothing AndAlso _
                        Not String.IsNullOrEmpty(column.AspectToStringFormat.Trim) Then
                        formatString = column.AspectToStringFormat.Trim.Replace("{0:", "")
                        formatString = formatString.Substring(0, formatString.Length - 1)
                    End If

                    For i As Integer = 0 To olv.GetDisplayOrderOfItemIndex(lastDisplayedItem.Index)

                        Dim value As Object = propInfo.GetValue(olv.GetNthItemInDisplayOrder(i).RowObject, Nothing)

                        If value Is Nothing Then
                            sheet.Cells(i + 2, colCounter).Value = ""
                        ElseIf Not value.GetType.IsValueType AndAlso Not TypeOf value Is String Then
                            sheet.Cells(i + 2, colCounter).Value = value.ToString()
                        Else
                            sheet.Cells(i + 2, colCounter).Value = value
                            If TypeOf value Is String Then
                                sheet.Cells(i + 2, colCounter).Style.WrapText = True
                                sheet.Cells(i + 2, colCounter).Style.HorizontalAlignment = ExcelHorizontalAlignment.Left
                            Else
                                sheet.Cells(i + 2, colCounter).Style.HorizontalAlignment = ExcelHorizontalAlignment.Center

                                If TypeOf value Is Date Then
                                    sheet.Cells(i + 2, colCounter).Style.Numberformat.Format = "yyyy-MM-dd"
                                ElseIf Not String.IsNullOrEmpty(formatString.Trim) Then
                                    sheet.Cells(i + 2, colCounter).Style.Numberformat.Format = formatString
                                End If

                            End If
                        End If

                        sheet.Cells(i + 2, colCounter).Style.Font.Name = "Times New Roman"
                        sheet.Cells(i + 2, colCounter).Style.Font.Size = 10
                        sheet.Cells(i + 2, colCounter).Style.VerticalAlignment = ExcelVerticalAlignment.Top

                    Next

                End If

                colCounter += 1

            Next

            sheet.Cells(2, 1, olv.GetDisplayOrderOfItemIndex(lastDisplayedItem.Index) + 2, colCounter - 1).AutoFitColumns()

        End Sub

        Private Function GetNumberFormat(ByVal roundOrder As Integer) As String
            Dim result As String = "##,0"
            If roundOrder > 0 Then
                result = result & "." & String.Empty.PadRight(roundOrder, "0"c)
            End If
            Return result
        End Function


        Public Shared Function GetToolStripItem(ByVal clickHandler As EventHandler) As System.Windows.Forms.ToolStripMenuItem
            Dim exporter As New OLVExporterExcel
            Dim result As New System.Windows.Forms.ToolStripMenuItem(exporter.Name, exporter.Icon)
            result.Tag = exporter
            AddHandler result.Click, clickHandler
            Return result
        End Function

    End Class

End Namespace