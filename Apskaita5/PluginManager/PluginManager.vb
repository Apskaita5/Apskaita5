Imports System.Web.Hosting
Imports System.IO

<Serializable()> _
Public Class PluginManager

    Private Const PluginFolder As String = "Plugins"

    Private Shared _Current As PluginManager = Nothing
    Private Shared ReadOnly _Lock As New Object

    Private _InitExceptions As String = ""
    Private _Plugins As New Dictionary(Of Reflection.Assembly, AccIPlugin.IPlugin)
    Private _LocalizedDocumentTypeDictionary As New Dictionary(Of String, String)
    Private _LocalizedLtaOperationTypeDictionary As New Dictionary(Of String, String)
    Private _LocalizedGoodsOperationTypeDictionary As New Dictionary(Of String, String)
    Private _LocalizedGoodsComplexOperationTypeDictionary As New Dictionary(Of String, String)


    Public Shared ReadOnly Property Current() As PluginManager
        Get
            If _Current Is Nothing Then
                SyncLock _Lock
                    If _Current Is Nothing Then
                        _Current = New PluginManager()
                    End If
                End SyncLock
            End If
            Return _Current
        End Get
    End Property


    Private Sub New()
        Init()
    End Sub

    Private Sub Init()

        Dim pluginPath As String

        If System.Web.HttpContext.Current Is Nothing Then
            pluginPath = IO.Path.Combine((New FileInfo(Reflection.Assembly.GetExecutingAssembly.Location)).Directory.FullName, PluginFolder)
        Else
            pluginPath = HostingEnvironment.MapPath(String.Format("~/Bin/{0}", PluginFolder))
        End If

        If IO.Directory.Exists(pluginPath) Then
            For Each plugin As FileInfo In (New DirectoryInfo(pluginPath)).GetFiles("*.dll", SearchOption.AllDirectories)
                Dim assembly As Reflection.Assembly = Reflection.Assembly.LoadFrom(plugin.FullName)
                For Each t As Type In assembly.GetTypes()
                    Dim isPluginAssembly As Boolean = (t.IsClass AndAlso Not t.IsAbstract AndAlso _
                        Not Array.IndexOf(t.GetInterfaces, GetType(AccIPlugin.IPlugin)) < 0)
                    If isPluginAssembly Then
                        AddPlugin(assembly, DirectCast(Activator.CreateInstance(t), AccIPlugin.IPlugin))
                        Exit For
                    End If
                Next
            Next
        End If

    End Sub

    Private Sub AddPlugin(ByVal assembly As Reflection.Assembly, ByVal plugin As AccIPlugin.IPlugin)

        For Each entry As KeyValuePair(Of String, String) In plugin.GetLocalizedDocumentTypeDictionary()
            If _LocalizedDocumentTypeDictionary.ContainsKey(entry.Key) Then
                _InitExceptions = _InitExceptions & vbCrLf _
                    & String.Format("Duplicate DocumentType code ""{0}"" for plugin assembly {1}.", _
                    entry.Key, assembly.FullName)
            Else
                _LocalizedDocumentTypeDictionary.Add(entry.Key, entry.Value)
            End If
        Next

        For Each entry As KeyValuePair(Of String, String) In plugin.GetLocalizedLtaOperationTypeDictionary()
            If _LocalizedDocumentTypeDictionary.ContainsKey(entry.Key) Then
                _InitExceptions = _InitExceptions & vbCrLf _
                    & String.Format("Duplicate LtaOperationType code ""{0}"" for plugin assembly {1}.", _
                    entry.Key, assembly.FullName)
            Else
                _LocalizedLtaOperationTypeDictionary.Add(entry.Key, entry.Value)
            End If
        Next

        For Each entry As KeyValuePair(Of String, String) In plugin.GetLocalizedGoodsOperationTypeDictionary()
            If _LocalizedDocumentTypeDictionary.ContainsKey(entry.Key) Then
                _InitExceptions = _InitExceptions & vbCrLf _
                    & String.Format("Duplicate GoodsOperationType code ""{0}"" for plugin assembly {1}.", _
                    entry.Key, assembly.FullName)
            Else
                _LocalizedGoodsOperationTypeDictionary.Add(entry.Key, entry.Value)
            End If
        Next

        For Each entry As KeyValuePair(Of String, String) In plugin.GetLocalizedGoodsComplexOperationTypeDictionary()
            If _LocalizedDocumentTypeDictionary.ContainsKey(entry.Key) Then
                _InitExceptions = _InitExceptions & vbCrLf _
                    & String.Format("Duplicate GoodsComplexOperationType code ""{0}"" for plugin assembly {1}.", _
                    entry.Key, assembly.FullName)
            Else
                _LocalizedGoodsComplexOperationTypeDictionary.Add(entry.Key, entry.Value)
            End If
        Next

        _Plugins.Add(assembly, plugin)

    End Sub


    Public Function GetLocalizedDocumentType(ByVal typeCode As String) As String
        Return _LocalizedDocumentTypeDictionary(typeCode)
    End Function

    Public Function GetLocalizedLtaOperationType(ByVal typeCode As String) As String
        Return _LocalizedLtaOperationTypeDictionary(typeCode)
    End Function

    Public Function GetLocalizedGoodsOperationType(ByVal typeCode As String) As String
        Return _LocalizedGoodsOperationTypeDictionary(typeCode)
    End Function

    Public Function GetLocalizedGoodsComplexOperationType(ByVal typeCode As String) As String
        Return _LocalizedGoodsComplexOperationTypeDictionary(typeCode)
    End Function

    Public Function GetLocalizedPropertyName(ByVal objectType As Type, ByVal propertyName As String) As String
        Dim entry As AccIPlugin.IPlugin = _Plugins(objectType.Assembly)
        If entry Is Nothing Then
            Throw New Exception(String.Format("Plugin assembly '{0}' is unknown (for type - '{1}').", _
                objectType.Assembly.FullName, objectType.FullName))
        End If
        Return entry.GetLocalizedPropertyName(objectType, propertyName)
    End Function

End Class
