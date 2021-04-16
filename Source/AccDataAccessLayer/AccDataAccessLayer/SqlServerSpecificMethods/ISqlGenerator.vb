Namespace SqlServerSpecificMethods

    Public Interface ISqlGenerator

#Region "*** Properties ***"

        ReadOnly Property ServerType() As DataAccessTypes.SqlServerType

        ReadOnly Property DatabaseIsCreatedBySqlStatement() As Boolean

        ReadOnly Property DatabaseIsCreatedByCustomCode() As Boolean

        ReadOnly Property DatabaseIsCreatedByConnectionAutomaticaly() As Boolean

        ReadOnly Property CreateStatementContainsIndexes() As Boolean

        ReadOnly Property ShowTablesResultsIncludeCreateTableStatements() As Boolean

        ReadOnly Property SupportsStoredProcedures() As Boolean

        ReadOnly Property StoredProcedureNameItemInQueryResults() As Integer

        ReadOnly Property StoredProcedureSourceItemInQueryResults() As Integer

        ReadOnly Property ShowProceduresResultsIncludeCreateTableStatements() As Boolean

        ReadOnly Property SupportsUniqueFields() As Boolean

        ReadOnly Property SupportsUsignedFields() As Boolean

        ReadOnly Property SupportsIntLength() As Boolean

        ReadOnly Property SupportsCharLength() As Boolean

        ReadOnly Property SupportsVarCharLength() As Boolean

        ReadOnly Property SupportsDoubleLength() As Boolean

        ReadOnly Property SupportsDecimalLength() As Boolean

        ReadOnly Property SupportsBlobLength() As Boolean

        ReadOnly Property SupportsEnums() As Boolean

        ReadOnly Property SupportsEnumFields() As Boolean

        ReadOnly Property Wildcart() As String

        ReadOnly Property RootName() As String

        ReadOnly Property SqlServerFileBased() As Boolean

        ReadOnly Property TablePrivilegesAreRevokedByRevokeAllPrivileges() As Boolean

        ReadOnly Property SupportsTablePrivileges() As Boolean

#End Region

#Region "*** DatabaseStructure.DatabaseField ***"

        Function GetModifyFieldStatement(ByVal DatabaseName As String, _
            ByVal TableName As String, ByVal field As DatabaseAccess.DatabaseStructure.DatabaseField) As String

        Function GetFieldDbDefinition(ByVal field As DatabaseAccess.DatabaseStructure.DatabaseField) As String

        Function GetDropFieldStatement(ByVal DatabaseName As String, ByVal TableName As String, _
            ByVal field As DatabaseAccess.DatabaseStructure.DatabaseField) As String

        Function GetAddFieldStatement(ByVal DatabaseName As String, ByVal TableName As String, _
            ByVal field As DatabaseAccess.DatabaseStructure.DatabaseField) As String

        Function GetCreateIndexStatement(ByVal DatabaseName As String, ByVal TableName As String, _
            ByVal field As DatabaseAccess.DatabaseStructure.DatabaseField) As String

        Function GetDropIndexStatement(ByVal DatabaseName As String, ByVal TableName As String, _
            ByVal field As DatabaseAccess.DatabaseStructure.DatabaseField) As String

        Function GetIndexDbDefinition(ByVal field As DatabaseAccess.DatabaseStructure.DatabaseField) As String

        Sub FetchDatabaseFieldFromDbDefinition(ByVal DbDefinition As String, ByRef Name As String, _
            ByRef NotNull As Boolean, ByRef Autoincrement As Boolean, ByRef PrimaryKey As Boolean, _
            ByRef Unsigned As Boolean, ByRef FieldType As DataAccessTypes.DatabaseFieldType, _
            ByRef Length As Integer, ByRef EnumValues As String, ByRef Unique As Boolean)

        Function DatabaseFieldTypesSimilar(ByVal fieldType1 As DatabaseFieldType, _
            ByVal fieldType2 As DatabaseFieldType) As Boolean

        Function GetFieldTypeName(ByVal fieldType As DatabaseFieldType) As String

        Function GetFieldTypeFromDbDefinition(ByVal DbDefinition As String) As DatabaseFieldType

#End Region

#Region "*** DatabaseStructure.DatabaseFieldList ***"

        Function GetIndexNameFromDbDefinition(ByVal DbDefinition As String) As String

        Function GetIndexTableNameFromDbDefinition(ByVal DbDefinition As String) As String

        Function GetIndexColumnNameFromDbDefinition(ByVal DbDefinition As String) As String

        Function IsValidFieldDbDefinition(ByVal DbDefinition As String) As Boolean

        Function IsValidIndexDbDefinition(ByVal DbDefinition As String) As Boolean

        Function IsPrimaryKeyDbDefinition(ByVal DbDefinition As String) As Boolean

        Function IsIndexUniqueInDbDefinition(ByVal DbDefinition As String) As Boolean

#End Region

#Region "*** DatabaseStructure.DatabaseTable ***"

        Function GetCreateTableStatementHeader(ByVal DatabaseName As String, _
            ByVal table As DatabaseAccess.DatabaseStructure.DatabaseTable) As String

        Function GetCreateTableStatementTail(ByVal table As DatabaseAccess.DatabaseStructure.DatabaseTable) As String

        Function GetCreateTableStatementFieldSeparator() As String

        Function GetCreateTableStatementLeftBracket() As String

        Function GetCreateTableStatementRightBracket() As String

        Function GetDropTableStatement(ByVal DatabaseName As String, _
            ByVal TableName As String) As String

        Sub FetchDatabaseTableFromDbDefinition(ByVal DbDefinition As String, _
            ByRef Name As String, ByRef EngineName As String, ByRef CharsetName As String)

#End Region

#Region "*** DatabaseStructure.DatabaseTableList ***"

        Function GetShowTablesStatement(ByVal DatabaseName As String) As String

        Function GetShowCreateTableStatement(ByVal DatabaseName As String, _
            ByVal TableName As String) As String

        Function GetShowIndexesStatement(ByVal DatabaseName As String) As String

#End Region

#Region "*** DatabaseStructure.DatabaseStoredProcedure ***"

        Function GetDropProcedureStatement(ByVal DatabaseName As String, _
            ByVal procedure As DatabaseAccess.DatabaseStructure.DatabaseStoredProcedure) As String

        Function GetCreateProcedureStatement(ByVal DatabaseName As String, _
            ByVal procedure As DatabaseAccess.DatabaseStructure.DatabaseStoredProcedure) As String

        Function GetUpdateProcedureStatement(ByVal DatabaseName As String, _
            ByVal procedure As DatabaseAccess.DatabaseStructure.DatabaseStoredProcedure) As String

        Sub FetchStoredProcedureFromDbDefinition(ByVal DbDefinition As String, _
            ByRef Name As String, ByRef SourceCode As String, ByRef SourceCodeComparable As String)

#End Region

#Region "*** DatabaseStructure.DatabaseStoredProcedureList ***"

        Function GetShowProceduresStatement(ByVal DatabaseName As String) As String

        Function GetShowCreateProcedureStatement(ByVal DatabaseName As String, _
            ByVal ProcedureName As String) As String

#End Region

#Region "*** SqlUtilities ***"

        Function GetCreateDatabaseStatement(ByVal DatabaseName As String, ByVal CharsetName As String) As String

        Sub CreateDatabaseCustomCode(ByVal DatabaseName As String, ByVal CharsetName As String)

        Sub DoCreateDatabase(ByVal DatabaseName As String, ByVal CharsetName As String)

#End Region

#Region "*** UserProfile ***"

        Function GetFetchUserProfileStatement() As String

        Function GetUpdateUserProfileStatement() As String

#End Region

#Region "*** DatabaseTableAccessItem ***"

        Function GetPrivilegeSqlDefinition(ByVal PrivilegeType As RoleAccessType, _
            ByVal InsertRequiresUpdate As Boolean, ByVal UpdateRequiresInsert As Boolean) As String

#End Region

#Region "*** TableAccessLevel ***"

        Function GetGrantStatement(ByVal DataBaseName As String, ByVal TableName As String, _
            ByVal UserName As String, ByVal HostName As String, _
            ByVal AccessLevel As DatabaseTableAccessType) As String

#End Region

#Region "*** Role ***"

        Function GetInsertRoleStatement() As String

#End Region

#Region "*** RoleListForDatabase ***"

        Function GetRevokeAllPrivilegesForDatabase(ByVal cDatabaseName As String, _
            ByVal cUserName As String, ByVal cUserHost As String) As String

        Function GetPrivilegeSqlDefinition(ByVal PrivilegeType As RoleAccessType) As String

        Function GetRevokePrivilege(ByVal cRole As DataAccessTypes.RoleAccessType, _
            ByVal cDatabaseName As String, ByVal cTableName As String, _
            ByVal cUserName As String, ByVal cUserHost As String) As String

        Function GetGrantAllPrivilegesForDatabase(ByVal cDatabaseName As String, _
            ByVal cUserName As String, ByVal cUserHost As String) As String

        Function GetGrantExecutePrivilegeForDatabase(ByVal cDatabaseName As String, _
            ByVal cUserName As String, ByVal cUserHost As String) As String

        Function GetGrantPrivilege(ByVal cRole As DataAccessTypes.RoleAccessType, _
            ByVal cDatabaseName As String, ByVal cTableName As String, _
            ByVal cUserName As String, ByVal cUserHost As String) As String

#End Region

#Region "*** User ***"

        Function GetFetchUserDataStatement(ByVal cUserName As String, _
            ByVal FetchFromSystemDB As Boolean) As String

        Function GetFetchUserRolesStatement(ByVal cUserName As String) As String

        Function GetInsertUserStatement(ByVal cUser As Security.UserAdministration.User) As String

        Function GetCreateUserStatement(ByVal cUserName As String, _
            ByVal cUserHost As String, ByVal cPassword As String) As String

        Function GetGrantUsageStatement(ByVal cUserName As String, ByVal cUserHost As String) As String

        Function GetInsertAdminRoleStatement(ByVal UserID As Integer, _
            ByVal AdminRoleName As String) As String

        Function GetGrantAdminPrivilegesStatement(ByVal cUserName As String, _
            ByVal cUserHost As String) As String

        Function GetRevokeAllPrivilegesStatement(ByVal cUserName As String, ByVal cUserHost As String) As String

        Function GetRevokeAdminPrivilegesStatement(ByVal cUserName As String, ByVal cUserHost As String) As String

        Function GetUpdateUserStatement(ByVal UpdatePassword As Boolean) As String

        Function GetRenameUserStatement(ByVal cUserName As String, ByVal cUserHost As String, _
            ByVal cUserNameOld As String, ByVal cUserHostOld As String) As String

        Function GetUpdatePasswordForUserStatement(ByVal cUserName As String, _
            ByVal cUserHost As String, ByVal cPassword As String) As String

        Function GetDeleteAllRolesStatement(ByVal UserID As Integer) As String

        Function GetDeleteUserStatement(ByVal UserID As Integer) As String

        Function GetDropUserStatement(ByVal cUserName As String, ByVal cUserHost As String) As String

        Function GetFetchNameIsUniqueStatement() As String

        Function GetFetchNameIsUniqueInSqlServerStatement() As String

#End Region

        Function GetFetchUserInfoListStatement(ByVal ForCurrentDatabase As Boolean) As String

        Function GetFullPathToDbFile(ByVal DbFileName As String) As String


    End Interface

End Namespace
