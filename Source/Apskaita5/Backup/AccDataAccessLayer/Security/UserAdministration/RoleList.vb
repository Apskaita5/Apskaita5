Imports AccDataAccessLayer.Security.DatabaseTableAccess
Imports AccDataAccessLayer.DatabaseAccess
Namespace Security.UserAdministration

    <Serializable()> _
    Public Class RoleList
        Inherits BusinessListBase(Of RoleList, Role)

#Region " Factory Methods "

        Friend Shared Function NewRoleList(ByVal RoleDbaGauge As Csla.SortedBindingList(Of DatabaseTableAccessRole)) As RoleList
            Return New RoleList(RoleDbaGauge)
        End Function

        Friend Shared Function GetRoleList(ByVal RoleListDataTable As DataTable, _
            ByVal DatabaseName As String, ByVal RoleDbaGauge As Csla.SortedBindingList(Of DatabaseTableAccessRole)) As RoleList
            Return New RoleList(RoleListDataTable, DatabaseName, RoleDbaGauge)
        End Function


        Private Sub New()
            Me.AllowEdit = True
            Me.AllowNew = False
            Me.AllowRemove = False
            MarkAsChild()
        End Sub

        Private Sub New(ByVal RoleListDataTable As DataTable, ByVal DatabaseName As String, _
            ByVal RoleDbaGauge As Csla.SortedBindingList(Of DatabaseTableAccessRole))
            Me.AllowEdit = True
            Me.AllowNew = False
            Me.AllowRemove = False
            MarkAsChild()
            Fetch(RoleListDataTable, DatabaseName, RoleDbaGauge)
        End Sub

        Private Sub New(ByVal RoleDbaGauge As Csla.SortedBindingList(Of DatabaseTableAccessRole))
            Me.AllowEdit = True
            Me.AllowNew = False
            Me.AllowRemove = False
            MarkAsChild()
            Fetch(RoleDbaGauge)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal RoleListDataTable As DataTable, ByVal DatabaseName As String, _
            ByVal RoleDbaGauge As Csla.SortedBindingList(Of DatabaseTableAccessRole))

            RaiseListChangedEvents = False

            Dim IsFound As Boolean
            For Each roleAccess As DatabaseTableAccessRole In RoleDbaGauge
                If Not roleAccess.IsHelperList Then
                    IsFound = False
                    For Each dr As DataRow In RoleListDataTable.Rows
                        If dr.Item(0).ToString.Trim.ToLower = DatabaseName.Trim.ToLower _
                            AndAlso dr.Item(1).ToString.Trim.ToLower = roleAccess.RoleName.Trim.ToLower Then
                            Add(Role.GetRole(dr, roleAccess))
                            IsFound = True
                            Exit For
                        End If
                    Next
                    If Not IsFound Then Add(Role.NewRole(roleAccess))
                End If
            Next

            RaiseListChangedEvents = True

        End Sub

        Private Sub Fetch(ByVal RoleDbaGauge As Csla.SortedBindingList(Of DatabaseTableAccessRole))

            RaiseListChangedEvents = False

            For Each roleAccess As DatabaseTableAccessRole In RoleDbaGauge
                If Not roleAccess.IsHelperList Then
                    Add(Role.NewRole(roleAccess))
                End If
            Next

            RaiseListChangedEvents = True

        End Sub

        Friend Sub Update(ByVal parent As User, ByVal cDatabaseName As String, _
            ByVal CurrentIdentity As AccIdentity)

            Dim SqlGenerator As SqlServerSpecificMethods.ISqlGenerator = GetSqlGenerator()

            RaiseListChangedEvents = False
            DeletedList.Clear()

            For Each item As Role In Me
                item.Update(parent, cDatabaseName.Trim, SqlGenerator)
            Next

            RaiseListChangedEvents = True

        End Sub

#End Region

    End Class

End Namespace