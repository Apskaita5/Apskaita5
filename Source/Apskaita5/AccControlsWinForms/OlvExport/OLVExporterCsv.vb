Imports BrightIdeasSoftware
Imports System.Windows.Forms

Namespace OlvExport

    Public Class OLVExporterCsv
        Implements IOLVExporter


        Public ReadOnly Property Icon() As System.Drawing.Image Implements IOLVExporter.Icon
            Get
                Return My.Resources.csv_text
            End Get
        End Property

        Public ReadOnly Property Name() As String Implements IOLVExporter.Name
            Get
                Return "Csv failas"
            End Get
        End Property


        Public Sub Export(ByVal olv As ObjectListView) Implements IOLVExporter.Export

            Dim filePath As String

            Using saveFile As New SaveFileDialog
                saveFile.Filter = "Csv failai (*.csv)|*.csv|Visi failai|*.*"
                saveFile.AddExtension = True
                saveFile.DefaultExt = ".csv"
                If saveFile.ShowDialog() <> Windows.Forms.DialogResult.OK Then Exit Sub
                filePath = saveFile.FileName
            End Using

            If filePath Is Nothing OrElse String.IsNullOrEmpty(filePath.Trim) Then Exit Sub

            Try
                Using busy As New StatusBusy
                    Dim exporter As New OLVExporter(olv, olv.FilteredObjects)
                    IO.File.WriteAllText(filePath, exporter.ExportTo(OLVExporter.ExportFormat.CSV), _
                        System.Text.Encoding.Unicode)
                End Using
            Catch ex As Exception
                ShowError(ex, Nothing)
                Exit Sub
            End Try

            If Not YesOrNo("Failas buvo sėkmingai išsaugotas. Atidaryti?") Then Exit Sub

            Process.Start(filePath)

        End Sub

        Public Shared Function GetToolStripItem(ByVal clickHandler As EventHandler) As System.Windows.Forms.ToolStripMenuItem
            Dim exporter As New OLVExporterCsv
            Dim result As New System.Windows.Forms.ToolStripMenuItem(exporter.Name, exporter.Icon)
            result.Tag = exporter
            AddHandler result.Click, clickHandler
            Return result
        End Function

    End Class

End Namespace
