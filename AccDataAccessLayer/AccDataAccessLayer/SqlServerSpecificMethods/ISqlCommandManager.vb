Imports AccDataAccessLayer.DatabaseAccess
Namespace SqlServerSpecificMethods

    Friend Interface ISqlCommandManager

        Sub TransactionBegin(Optional ByVal ThrowOnTransactionExists As Boolean = True)

        Sub TransactionCommit()

        Sub TransactionRollBack(ByVal ex As Exception)

        Sub TryLogin(ByVal CurrentIdentity As Security.AccIdentity, ByRef cRoles As List(Of String))

        Function ExecuteCommand(ByVal CommandToExecute As SQLCommand, _
            Optional ByRef RowsAffected As Integer = 0) As Integer

        Function FetchCommand(ByVal SelectCommand As SQLCommand) As DataTable

        Function DateToDbString(ByVal nDate As Date) As String

        Function DateTimeToDbString(ByVal nDate As Date) As String

        Function TimeToDbString(ByVal nDate As Date) As String

        Sub FetchCompanyFromDbFile(ByVal DbFilePath As String, _
            ByVal cPassword As String, ByRef cCompanyName As String, _
            ByRef cCompanyCode As String)

        Sub ChangePassword(ByVal NewPassword As String)

        Sub DropDatabase(ByVal DatabaseName As String)

        Function GetAccesibleDatabaseList() As List(Of String)

        Sub FetchCompanyInfoData(ByVal DatabaseName As String, ByRef CompanyName As String, _
            ByRef CompanyID As String)

        Function GetConnectionString(ByVal TargetIdentity As Security.AccIdentity) As String

        Function GetSqlDepositoryFileName() As String

    End Interface

End Namespace