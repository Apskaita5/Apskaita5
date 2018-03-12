Imports AccDataAccessLayer.Security.DatabaseTableAccess
Imports AccDataAccessLayer.DatabaseAccess
Namespace Security.UserAdministration

    <Serializable()> _
Public Class Role
        Inherits BusinessBase(Of Role)

#Region " Business Methods "

        Private _GID As Guid = Guid.NewGuid
        Private _RoleName As String = ""
        Private _RoleNameHumanReadable As String = ""
        Private _RoleLevel As RoleAccessType = RoleAccessType.None
        Private _RoleLevelOld As RoleAccessType = RoleAccessType.None


        Public ReadOnly Property RoleName() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _RoleName.Trim
            End Get
        End Property

        Public ReadOnly Property RoleNameHumanReadable() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _RoleNameHumanReadable.Trim
            End Get
        End Property

        Public Property RoleLevel() As RoleAccessType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _RoleLevel
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As RoleAccessType)
                CanWriteProperty(True)
                If _RoleLevel <> value Then
                    _RoleLevel = value
                    PropertyHasChanged()
                    PropertyHasChanged("LevelRead")
                    PropertyHasChanged("LevelWrite")
                    PropertyHasChanged("LevelUpdate")
                End If
            End Set
        End Property

        Public ReadOnly Property RoleLevelOld() As RoleAccessType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _RoleLevelOld
            End Get
        End Property

        Public Property LevelRead() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return (_RoleLevel <> RoleAccessType.None)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Boolean)
                CanWriteProperty(True)
                If Not LevelRead AndAlso value Then
                    _RoleLevel = RoleAccessType.Read
                    PropertyHasChanged()
                    PropertyHasChanged("RoleLevel")
                ElseIf LevelRead AndAlso Not value Then
                    _RoleLevel = RoleAccessType.None
                    PropertyHasChanged()
                    PropertyHasChanged("RoleLevel")
                    PropertyHasChanged("LevelWrite")
                    PropertyHasChanged("LevelUpdate")
                End If
            End Set
        End Property

        Public Property LevelWrite() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return (_RoleLevel = RoleAccessType.Write OrElse _RoleLevel = RoleAccessType.Update)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Boolean)
                CanWriteProperty(True)
                If Not LevelWrite AndAlso value Then
                    _RoleLevel = RoleAccessType.Write
                    PropertyHasChanged()
                    PropertyHasChanged("RoleLevel")
                    PropertyHasChanged("LevelRead")
                ElseIf LevelWrite AndAlso Not value Then
                    _RoleLevel = RoleAccessType.Read
                    PropertyHasChanged()
                    PropertyHasChanged("RoleLevel")
                    PropertyHasChanged("LevelUpdate")
                End If
            End Set
        End Property

        Public Property LevelUpdate() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return (_RoleLevel = RoleAccessType.Update)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Boolean)
                CanWriteProperty(True)
                If Not LevelUpdate AndAlso value Then
                    _RoleLevel = RoleAccessType.Update
                    PropertyHasChanged()
                    PropertyHasChanged("RoleLevel")
                    PropertyHasChanged("LevelRead")
                    PropertyHasChanged("LevelWrite")
                ElseIf LevelUpdate AndAlso Not value Then
                    _RoleLevel = RoleAccessType.Write
                    PropertyHasChanged()
                    PropertyHasChanged("RoleLevel")
                End If
            End Set
        End Property



        Protected Overrides Function GetIdValue() As Object
            Return _GID
        End Function

#End Region

#Region " Authorization Rules "

        Protected Overrides Sub AddAuthorizationRules()
            ' TODO: add authorization rules
            'AuthorizationRules.AllowWrite("", "")
        End Sub

#End Region

#Region " Factory Methods "

        Friend Shared Function NewRole(ByVal RoleAccess As DatabaseTableAccessRole) As Role
            Return New Role(RoleAccess)
        End Function

        Friend Shared Function GetRole(ByVal dr As DataRow, ByVal RoleAccess As DatabaseTableAccessRole) As Role
            Return New Role(dr, RoleAccess)
        End Function

        Private Sub New()
            MarkAsChild()
        End Sub

        Private Sub New(ByVal dr As DataRow, ByVal RoleAccess As DatabaseTableAccessRole)
            MarkAsChild()
            Fetch(dr, RoleAccess)
        End Sub

        Private Sub New(ByVal RoleAccess As DatabaseTableAccessRole)
            MarkAsChild()
            Fetch(RoleAccess)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal dr As DataRow, ByVal RoleAccess As DatabaseTableAccessRole)

            _RoleName = RoleAccess.RoleName
            _RoleNameHumanReadable = RoleAccess.RoleNameHumanReadable
            _RoleLevel = ConvertRoleAccessTypeInteger(CInt(dr.Item(2)))
            _RoleLevelOld = _RoleLevel

            MarkOld()

        End Sub

        Private Sub Fetch(ByVal RoleAccess As DatabaseTableAccessRole)

            _RoleName = RoleAccess.RoleName
            _RoleNameHumanReadable = RoleAccess.RoleNameHumanReadable
            _RoleLevel = RoleAccessType.None

            MarkOld()

        End Sub

        Friend Sub Update(ByVal parent As User, ByVal cDatabaseName As String, _
            ByVal SqlGenerator As SqlServerSpecificMethods.ISqlGenerator)

            If _RoleLevel = RoleAccessType.None Then
                MarkOld()
                Exit Sub
            End If

            Dim myComm As New SQLCommand("RawSQL", SqlGenerator.GetInsertRoleStatement)
            myComm.AddParam("?UD", parent.ID)
            myComm.AddParam("?DN", cDatabaseName.Trim)
            myComm.AddParam("?RN", _RoleName.Trim)
            myComm.AddParam("?RL", GetPrivilegeLevelDbInt(_RoleLevel))

            myComm.Execute()

            MarkOld()

        End Sub

        Friend Sub AddWithPrivilege(ByVal RoleToSqlGauge As DatabaseTableAccessRole, _
            ByRef GrantDictionary As Dictionary(Of String, RoleAccessType), _
            Optional ByVal OldPrivileges As Boolean = False)

            For Each tbl As String In RoleToSqlGauge.TableAccessList.Split(New Char() {","c}, _
                StringSplitOptions.RemoveEmptyEntries)
                If GrantDictionary.ContainsKey(tbl.Trim) Then
                    GrantDictionary.Item(tbl.Trim) = GetHigherPrivilege( _
                        GrantDictionary.Item(tbl.Trim), GetPrivilegeForTable(tbl, OldPrivileges))
                Else
                    If _RoleLevel <> RoleAccessType.None Then GrantDictionary.Add( _
                        tbl.Trim, GetPrivilegeForTable(tbl, OldPrivileges))
                End If
            Next

            For Each tbl As String In RoleToSqlGauge.ReadOnlyTableAccessList.Split(New Char() {","c}, _
                StringSplitOptions.RemoveEmptyEntries)
                If Not GrantDictionary.ContainsKey(tbl.Trim) Then
                    If _RoleLevel <> RoleAccessType.None OrElse (OldPrivileges AndAlso _
                        _RoleLevelOld <> RoleAccessType.None) Then GrantDictionary.Add( _
                        tbl.Trim, RoleAccessType.Read)
                End If
            Next

        End Sub

        Private Function GetPrivilegeForTable(ByVal tbl As String, _
            ByVal OldPrivileges As Boolean) As RoleAccessType

            Dim TargetPrivilege As RoleAccessType
            If OldPrivileges Then
                TargetPrivilege = _RoleLevelOld
            Else
                TargetPrivilege = _RoleLevel
            End If

            Select Case TargetPrivilege
                Case RoleAccessType.None
                    Return RoleAccessType.None
                Case RoleAccessType.Read
                    Return RoleAccessType.Read
                Case RoleAccessType.Write
                    Return RoleAccessType.Write
                Case RoleAccessType.Update
                    Return RoleAccessType.Update
                Case Else
                    Throw New NotSupportedException("Privilegijos tipas '" & _
                        TargetPrivilege.ToString & "' nežinomas. Metodas - " & _
                        "Role.GetPrivilegeForTable.")
            End Select
        End Function

        Private Function GetHigherPrivilege(ByVal Role1 As RoleAccessType, _
            ByVal Role2 As RoleAccessType) As RoleAccessType
            Select Case Role1
                Case RoleAccessType.None
                    Return Role2
                Case RoleAccessType.Read
                    If Role2 = RoleAccessType.None Then
                        Return RoleAccessType.Read
                    Else
                        Return Role2
                    End If
                Case RoleAccessType.Write
                    If Role2 = RoleAccessType.Update Then Return RoleAccessType.Update
                    Return RoleAccessType.Write
                Case RoleAccessType.Update
                    Return RoleAccessType.Update
                Case Else
                    Throw New NotSupportedException("Privilegijos tipas '" & _
                        _RoleLevel.ToString & "' nežinomas. Metodas - " & _
                        "Role.GetPrivilegeForTable.")
            End Select
        End Function

        Private Function GetPrivilegeLevelDbInt(ByVal cRoleLevel As RoleAccessType) As Integer
            Select Case cRoleLevel
                Case RoleAccessType.None
                    Return 0
                Case RoleAccessType.Read
                    Return 1
                Case RoleAccessType.Write
                    Return 2
                Case RoleAccessType.Update
                    Return 3
                Case Else
                    Throw New NotSupportedException("Privilegijos tipas '" & _
                        cRoleLevel.ToString & "' nežinomas. Metodas - " & _
                        "Role.GetPrivilegeLevelDbInt.")
            End Select
        End Function

#End Region

    End Class

End Namespace
