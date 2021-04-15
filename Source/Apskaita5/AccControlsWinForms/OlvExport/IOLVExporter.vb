Imports BrightIdeasSoftware
Imports System.Windows.Forms

Namespace OlvExport

    Public Interface IOLVExporter

        ReadOnly Property Name() As String
        ReadOnly Property Icon() As System.Drawing.Image

        Sub Export(ByVal olv As ObjectListView)

    End Interface

End Namespace
