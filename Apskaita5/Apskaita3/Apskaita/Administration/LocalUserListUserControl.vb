Imports AccDataAccessLayer.Security
Imports AccDataAccessLayer
Public Class LocalUserListUserControl

    Private Obj As LocalUserList = Nothing

    Public Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ConnectionTechnologyHumanReadableDataGridViewComboBoxColumn.Items.Add( _
            ConvertConnectionTypeHumanReadable(ConnectionType.Local))
        ConnectionTechnologyHumanReadableDataGridViewComboBoxColumn.Items.Add( _
            ConvertConnectionTypeHumanReadable(ConnectionType.Remoting))
        ConnectionTechnologyHumanReadableDataGridViewComboBoxColumn.Items.Add( _
            ConvertConnectionTypeHumanReadable(ConnectionType.WebService))
        ConnectionTechnologyHumanReadableDataGridViewComboBoxColumn.Items.Add( _
            ConvertConnectionTypeHumanReadable(ConnectionType.EnerpriseServices))

        SqlServerTypeHumanReadableDataGridViewComboBoxColumn.Items.Add( _
            ConvertSqlServerTypeHumanReadable(SqlServerType.MySQL))
        SqlServerTypeHumanReadableDataGridViewComboBoxColumn.Items.Add( _
            ConvertSqlServerTypeHumanReadable(SqlServerType.SQLite))

    End Sub

    Public ReadOnly Property UnderlyingDataGridView() As System.Windows.Forms.DataGridView
        Get
            Return LocalUserListDataGridView
        End Get
    End Property

    Public ReadOnly Property IsDirty() As Boolean
        Get
            If Obj Is Nothing Then Return False
            Return Obj.IsDirty
        End Get
    End Property

    Public ReadOnly Property IsValid() As Boolean
        Get
            If Obj Is Nothing Then Return True
            Return Obj.IsValid
        End Get
    End Property

    Public ReadOnly Property BusinessErrorList() As String
        Get
            If Obj Is Nothing Then Return ""
            Return Obj.GetAllBrokenRules
        End Get
    End Property

    Public Sub LoadLocalUserListData(ByVal SettingsString As String)

        If LocalUserListBindingSource.DataSource Is Nothing Then
            Obj = Security.LocalUserList.GetLocalUserList(SettingsString)
            LocalUserListBindingSource.DataSource = Obj
        Else
            LocalUserListBindingSource.RaiseListChangedEvents = False
            Try
                Obj = Security.LocalUserList.GetLocalUserList(SettingsString)
            Catch ex As Exception
                LocalUserListBindingSource.RaiseListChangedEvents = True
                Exit Sub
            End Try
            LocalUserListBindingSource.DataSource = Nothing
            LocalUserListBindingSource.DataSource = Obj
            LocalUserListBindingSource.RaiseListChangedEvents = True
            LocalUserListBindingSource.ResetBindings(False)
        End If

    End Sub

    Public Sub AddNewLocalUser()
        If Obj Is Nothing Then Exit Sub
        Obj.Add(Security.LocalUser.NewLocalUser)
    End Sub

    Public Function GetLocalUserListSettingsString() As String
        If Obj Is Nothing Then Return ""
        Return Obj.GetSettingsString
    End Function

End Class
