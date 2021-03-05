Imports System.IO
Imports ApskaitaObjects.Extensibility

Public Class InvoiceImportPluginManager

    Private Const PluginFolderName As String = "InvoiceAdapters"
    Private Const PluginFileName As String = "A5Plugin"
    Private ReadOnly Shared _loadedPlugins As New List(Of String)
    Private ReadOnly Shared _adapters As New List(Of IInvoiceAdapter)


    Private Sub New()
    End Sub


    Public Shared Function GetAdapters() As List(Of IInvoiceAdapter)
        Init()
        Return _adapters
    End Function


    Private Shared Sub Init()

        Dim pluginFolderPath = Path.Combine(AppPath(), PluginFolderName)

        If Not Directory.Exists(pluginFolderPath) Then Exit Sub

        For Each plugin As FileInfo In (New DirectoryInfo(pluginFolderPath)) _
            .GetFiles("*.dll", SearchOption.AllDirectories)
            
            If IsPlugin(plugin) AndAlso Not _loadedPlugins.Contains(plugin.Directory.Name) Then

                Dim assembly As Reflection.Assembly = Reflection.Assembly.LoadFrom(plugin.FullName)
                For Each t As Type In assembly.GetTypes()
                    Dim isAdapter As Boolean = (t.IsClass AndAlso Not t.IsAbstract AndAlso _
                        t.IsPublic AndAlso GetType(IInvoiceAdapter).IsAssignableFrom(t))
                    If isAdapter Then
                        _adapters.Add(DirectCast(Activator.CreateInstance(t), IInvoiceAdapter))
                    End If
                Next

                _loadedPlugins.Add(plugin.Directory.Name)

            End If

        Next

    End Sub

    Private Shared Function IsPlugin(dllFile As FileInfo) As Boolean
        Return dllFile.Name.Equals(PluginFileName + ".dll", StringComparison.OrdinalIgnoreCase) _
            AndAlso Not dllFile.Directory.Name.Equals(PluginFolderName, StringComparison.OrdinalIgnoreCase)
    End Function

End Class
