Imports AccDataAccessLayer.Security.DatabaseTableAccess
Namespace Security.UserAdministration

    <Serializable()> _
    Public Class RoleListForDatabaseList
        Inherits BusinessListBase(Of RoleListForDatabaseList, RoleListForDatabase)

        Friend Function HasAnyRights() As Boolean
            For Each d As RoleListForDatabase In Me
                For Each r As Role In d.RoleList
                    If r.RoleLevel <> RoleAccessType.None Then Return True
                Next
            Next
            Return False
        End Function

#Region " Factory Methods "

        Friend Shared Function NewRoleListForDatabaseList(ByVal DbInfoList As DatabaseInfoList, _
            ByVal RoleDbaGauge As DatabaseTableAccessRoleList) As RoleListForDatabaseList
            Return New RoleListForDatabaseList(DbInfoList, RoleDbaGauge)
        End Function

        Friend Shared Function GetRoleListForDatabaseList(ByVal DbInfoList As DatabaseInfoList, _
            ByVal RoleListDataTable As DataTable, ByVal RoleDbaGauge As DatabaseTableAccessRoleList) _
            As RoleListForDatabaseList
            Return New RoleListForDatabaseList(DbInfoList, RoleListDataTable, RoleDbaGauge)
        End Function

        Private Sub New()
            MarkAsChild()
            Me.AllowEdit = True
            Me.AllowNew = False
            Me.AllowRemove = False
        End Sub

        Private Sub New(ByVal DbInfoList As DatabaseInfoList, ByVal RoleDbaGauge As DatabaseTableAccessRoleList)
            MarkAsChild()
            Me.AllowEdit = True
            Me.AllowNew = False
            Me.AllowRemove = False
            Fetch(DbInfoList, RoleDbaGauge)
        End Sub

        Private Sub New(ByVal DbInfoList As DatabaseInfoList, ByVal RoleListDataTable As DataTable, _
            ByVal RoleDbaGauge As DatabaseTableAccessRoleList)
            MarkAsChild()
            Me.AllowEdit = True
            Me.AllowNew = False
            Me.AllowRemove = False
            Fetch(DbInfoList, RoleListDataTable, RoleDbaGauge)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal DbInfoList As DatabaseInfoList, ByVal RoleListDataTable As DataTable, _
            ByVal RoleDbaGauge As DatabaseTableAccessRoleList)

            RaiseListChangedEvents = False

            Dim list As New Csla.SortedBindingList(Of DatabaseTableAccessRole)(RoleDbaGauge)
            list.ApplySort("VisibleIndex", ComponentModel.ListSortDirection.Ascending)

            For Each DbInfo As DatabaseInfo In DbInfoList
                Add(RoleListForDatabase.GetDatabaseRoleList(DbInfo, RoleListDataTable, list))
            Next

            RaiseListChangedEvents = True

        End Sub

        Private Sub Fetch(ByVal DbInfoList As DatabaseInfoList, ByVal RoleDbaGauge As DatabaseTableAccessRoleList)

            RaiseListChangedEvents = False

            Dim list As New Csla.SortedBindingList(Of DatabaseTableAccessRole)(RoleDbaGauge)
            list.ApplySort("VisibleIndex", ComponentModel.ListSortDirection.Ascending)

            For Each DbInfo As DatabaseInfo In DbInfoList
                Add(RoleListForDatabase.NewDatabaseRoleList(DbInfo, list))
            Next

            RaiseListChangedEvents = True

        End Sub

        Friend Sub Update(ByVal parent As User, ByVal CurrentIdentity As AccIdentity)

            RaiseListChangedEvents = False
            DeletedList.Clear()

            For Each item As RoleListForDatabase In Me
                item.Update(parent, CurrentIdentity)
            Next

            RaiseListChangedEvents = True

        End Sub

        Friend Sub RevokeAllDatabasePrivileges(ByVal CurrentIdentity As AccIdentity, _
            ByVal UserOfRole As User, ByVal UserHost As String, _
            ByVal RoleToSqlGauge As DatabaseTableAccessRoleList, _
            ByVal cSqlGenerator As SqlServerSpecificMethods.ISqlGenerator)
            For Each r As RoleListForDatabase In Me
                r.RevokeDatabasePrivileges(CurrentIdentity, UserOfRole, UserHost, RoleToSqlGauge, cSqlGenerator)
            Next
        End Sub

        Friend Sub GrantDatabasePrivileges(ByVal CurrentIdentity As AccIdentity, _
            ByVal UserOfRole As User, ByVal UserHost As String, _
            ByVal RoleToSqlGauge As DatabaseTableAccessRoleList, _
            ByVal cSqlGenerator As SqlServerSpecificMethods.ISqlGenerator)
            For Each d As RoleListForDatabase In Me
                d.GrantDatabasePrivileges(CurrentIdentity, UserOfRole, UserHost, RoleToSqlGauge, cSqlGenerator)
            Next
        End Sub

#End Region

    End Class

End Namespace