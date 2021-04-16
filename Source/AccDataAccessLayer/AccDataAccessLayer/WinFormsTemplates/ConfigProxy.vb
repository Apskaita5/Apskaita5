Imports System
Imports System.Collections.Specialized
Imports System.Configuration
Imports System.Configuration.Internal
Imports System.Reflection

Public NotInheritable Class ConfigProxy
    Implements IInternalConfigSystem

    ReadOnly baseconf As IInternalConfigSystem

    Public Sub New(ByVal baseconf As IInternalConfigSystem)
        Me.baseconf = baseconf
    End Sub

    Private appsettings As Object
    Public Function GetSection(ByVal configKey As String) As Object _
        Implements System.Configuration.Internal.IInternalConfigSystem.GetSection

        If configKey = "appSettings" AndAlso Me.appsettings IsNot Nothing Then Return Me.appsettings

        Dim o As Object = baseconf.GetSection(configKey)
        If configKey = "appSettings" AndAlso TypeOf o Is NameValueCollection Then
            ' create a new collection because the underlying collection is read-only
            Dim cfg As Object = New NameValueCollection(DirectCast(o, NameValueCollection))
            o = InlineAssignHelper(Me.appsettings, cfg)
        End If
        Return o
    End Function

    Public Sub RefreshConfig(ByVal sectionName As String) _
        Implements System.Configuration.Internal.IInternalConfigSystem.RefreshConfig
        If sectionName = "appSettings" Then
            appsettings = Nothing
        End If
        baseconf.RefreshConfig(sectionName)
    End Sub

    Public ReadOnly Property SupportsUserConfig() As Boolean _
        Implements System.Configuration.Internal.IInternalConfigSystem.SupportsUserConfig
        Get
            Return baseconf.SupportsUserConfig
        End Get
    End Property
    Private Shared Function InlineAssignHelper(Of T)(ByRef target As T, ByVal value As T) As T
        target = value
        Return value
    End Function

    ''' <summary>
    ''' This method should be called before application starts 
    ''' (i.e. Static Void Main() or MyProject\Application.Designer New()) 
    ''' to make appSettings not readonly which is required to dynamicaly change CSLA dataportal.
    ''' </summary>
    Public Shared Sub ReplaceSettings()

        ' initialize the ConfigurationManager
        Dim o As Object = System.Configuration.ConfigurationManager.AppSettings
        ' hack your proxy IInternalConfigSystem into the ConfigurationManager
        Dim s_configSystem As System.Reflection.FieldInfo = _
            GetType(System.Configuration.ConfigurationManager).GetField("s_configSystem", _
                System.Reflection.BindingFlags.Static Or _
                System.Reflection.BindingFlags.NonPublic)
        s_configSystem.SetValue(Nothing, New AccDataAccessLayer.ConfigProxy( _
            DirectCast(s_configSystem.GetValue(Nothing), System.Configuration.Internal.IInternalConfigSystem)))

    End Sub

End Class