Imports BrightIdeasSoftware
Imports System.Windows.Forms

Namespace OlvExport

    Public Class OLVExporterClipboard
        Implements IOLVExporter


        Public ReadOnly Property Icon() As System.Drawing.Image Implements IOLVExporter.Icon
            Get
                Return My.Resources.Paste_icon_16p
            End Get
        End Property

        Public ReadOnly Property Name() As String Implements IOLVExporter.Name
            Get
                Return "Clipboard'as"
            End Get
        End Property


        Public Sub Export(ByVal olv As ObjectListView) Implements IOLVExporter.Export
            Try
                Using busy As New StatusBusy
                    Dim exporter As New OLVExporter(olv, olv.FilteredObjects)
                    Clipboard.SetText(exporter.ExportTo(OLVExporter.ExportFormat.TabSeparated), TextDataFormat.UnicodeText)
                End Using
            Catch ex As Exception
                ShowError(ex, Nothing)
                Exit Sub
            End Try
            MsgBox("Lentelės turinys buvo sėkmingai nukopijuotas į clipboard'ą.", MsgBoxStyle.Information, "Info")
        End Sub

        Public Shared Function GetToolStripItem(ByVal clickHandler As EventHandler) As System.Windows.Forms.ToolStripMenuItem
            Dim exporter As New OLVExporterClipboard
            Dim result As New System.Windows.Forms.ToolStripMenuItem(exporter.Name, exporter.Icon)
            result.Tag = exporter
            AddHandler result.Click, clickHandler
            Return result
        End Function

    End Class

End Namespace