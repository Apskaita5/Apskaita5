﻿Imports MySql.Data.MySqlClient
Imports System.Data.SQLite
Imports System.text

Namespace DatabaseAccess

    ''' <summary>
    ''' Provides an abstract SQL commands (statements) execution layer.
    ''' </summary>
    Public Module SQLUtilities

#Region "*** Public methods ***"

        Public Function GetCurrentIdentity() As Security.AccIdentity

            If Csla.ApplicationContext.User Is Nothing OrElse _
                Not TypeOf Csla.ApplicationContext.User Is Security.AccPrincipal OrElse _
                Not Csla.ApplicationContext.User.Identity.IsAuthenticated Then _
                throw new System.Security.SecurityException( _
                "Klaida. Vartotojas neautentifikuotas (neprisijungęs).")

            Return CType(Csla.ApplicationContext.User.Identity, Security.AccIdentity)

        End Function

        Public Function GetRootName() As String
            Return GetSqlGenerator.RootName
        End Function

        ''' <summary>
        ''' Returns wildcart character for the server type. E.g. '%' for MySQL.
        ''' </summary>
        Public Function GetWildcart() As String
            Return GetSqlGenerator.Wildcart
        End Function

        Public Function IsLoggedIn() As Boolean

            If Csla.ApplicationContext.User Is Nothing OrElse _
                Not TypeOf Csla.ApplicationContext.User Is Security.AccPrincipal OrElse _
                Not Csla.ApplicationContext.User.Identity.IsAuthenticated Then Return False

            Return True

        End Function

        Public Function IsLoggedInDB() As Boolean

            If Csla.ApplicationContext.User Is Nothing OrElse _
                Not TypeOf Csla.ApplicationContext.User Is Security.AccPrincipal OrElse _
                Not Csla.ApplicationContext.User.Identity.IsAuthenticated OrElse _
                String.IsNullOrEmpty(CType(Csla.ApplicationContext.User.Identity, _
                Security.AccIdentity).Database.Trim) Then Return False

            Return True

        End Function

        Public Function GetFullPathToSQLiteDbFile(ByVal DbFileName As String) As String
            Return AppPath() & Path_FileServerDatabaseFiles & "\" & DbFileName & Name_FileServerDatabaseFilesExtension
        End Function

        Public Sub AddSqlQueryTimeoutToLocalContext(ByVal NewSqlQueryTimeoutValue As Integer)
            If (GetCurrentIdentity.IsLocalUser OrElse GetCurrentIdentity.ConnectionType _
                = ConnectionType.Local) AndAlso CurrentSqlQueryTimeOut <> NewSqlQueryTimeoutValue Then _
                CurrentSqlQueryTimeOut = NewSqlQueryTimeoutValue
        End Sub

        ''' <summary>
        ''' Returns TRUE if distributed (interobject) MySQL transaction is started.
        ''' </summary>
        Public Function TransactionExists() As Boolean
            Return Csla.ApplicationContext.LocalContext.Contains(LC_TransactionObjectKey)
        End Function

        ''' <summary>
        ''' Begins distributed (interobject) SQL transaction.
        ''' </summary>
        ''' <param name="ThrowOnTransactionExists">Indicates whether to throw an 
        ''' exception if a transaction already exists. If set to false, this method
        ''' will exit silently and existing transaction will be used.</param>
        Public Sub TransactionBegin(Optional ByVal ThrowOnTransactionExists As Boolean = True)

            If TransactionExists() Then
                If ThrowOnTransactionExists Then TransactionRollBack(New Exception( _
                    "Klaida. Vienu metu leidžiama tik viena SQL paskirstytoji transakcija."))
                Exit Sub
            End If

            GetSqlCommandManager.TransactionBegin()

        End Sub

        ''' <summary>
        ''' Commits (tryes to) active SQL transaction.
        ''' </summary>
        Public Sub TransactionCommit()

            If Not TransactionExists() Then Throw New Exception( _
                "Klaida. Nėra transakcijos, kurią būtų galima baigti.")

            GetSqlCommandManager.TransactionCommit()

        End Sub

        Public Sub FetchCompanyFromDbFile(ByVal DbFilePath As String, _
            ByVal cSqlServerType As SqlServerType, ByVal cPassword As String, _
            ByRef cCompanyName As String, ByRef cCompanyCode As String)
            GetSqlCommandManager.FetchCompanyFromDbFile(DbFilePath, cPassword, cCompanyName, cCompanyCode)
        End Sub

        Public Delegate Sub CustomCompanyCreateRoutine(ByVal NewDatabaseName As String)

        Public Sub CreateCompany(ByVal CompanyName As String, _
            ByVal CustomCreateRoutine As CustomCompanyCreateRoutine)

            Dim Gauge As DatabaseStructure.DatabaseStructure
            Try
                Gauge = DatabaseStructure.DatabaseStructure.GetDatabaseStructureServerSide()
            Catch ex As Exception
                Throw New Exception("Klaida. Nepavyko gauti duomenų bazės šablono: " & ex.Message, ex)
            End Try

            Dim NewDatabaseName As String = Gauge.CreateDatabase(CompanyName)

            Using ChangedDb As New ChangedDatabase(NewDatabaseName)
                CustomCreateRoutine.Invoke(NewDatabaseName)
            End Using

        End Sub

        Public Delegate Sub CustomCompanyInfoFetchRoutine()

        Public Sub FetchCompanyInfo(ByVal DatabaseName As String, ByVal Password As String, _
            ByVal CustomFetchRoutine As CustomCompanyInfoFetchRoutine)

            If DatabaseName Is Nothing OrElse String.IsNullOrEmpty(DatabaseName.Trim) Then
                CustomFetchRoutine.Invoke()
                Exit Sub
            End If

            Dim CurrentIdentity As AccDataAccessLayer.Security.AccIdentity = GetCurrentIdentity()
            Dim OldDatabase As String = CurrentIdentity.Database
            Dim OldPassword As String = CurrentIdentity.Password

            CurrentIdentity.Database = DatabaseName
            If CurrentIdentity.IsLocalUser Then CurrentIdentity.Password = Password

            Try
                CustomFetchRoutine.Invoke()
            Catch ex As Exception
                CurrentIdentity.Database = OldDatabase
                If CurrentIdentity.IsLocalUser Then CurrentIdentity.Password = OldPassword
                Throw ex
            End Try

        End Sub

        Public Function GetSqlGenerator(ByVal ForSqlServerType As SqlServerType) As SqlServerSpecificMethods.ISqlGenerator
            If ForSqlServerType = SqlServerType.MySQL Then
                Return New SqlServerSpecificMethods.MySqlGenerator
            ElseIf ForSqlServerType = SqlServerType.SQLite Then
                Return New SqlServerSpecificMethods.SQLiteGenerator
            Else
                Throw New NotSupportedException("Klaida. Serverio tipas '" & _
                    ForSqlServerType.ToString & "' nepalaikomas.")
            End If
        End Function

#End Region

#Region "*** Friend methods ***"

        Private CurrentSqlGenerator As SqlServerSpecificMethods.ISqlGenerator = Nothing
        Private CurrentSqlCommandManager As SqlServerSpecificMethods.ISqlCommandManager = Nothing
        Private CurrentSqlQueryTimeOut As Integer = -1

        Friend Function GetSqlGenerator() As SqlServerSpecificMethods.ISqlGenerator
            If CurrentSqlGenerator Is Nothing Then CurrentSqlGenerator = _
                GetSqlGenerator(GetCurrentIdentity.SqlServerType)
            Return CurrentSqlGenerator
        End Function

        Friend Function GetSqlCommandManager(ByVal nSqlServerType As SqlServerType) _
            As SqlServerSpecificMethods.ISqlCommandManager

            Select Case nSqlServerType
                Case SqlServerType.MySQL
                    Return New SqlServerSpecificMethods.MySqlCommandManager
                Case SqlServerType.SQLite
                    Return New SqlServerSpecificMethods.SQLiteCommandManager
                Case Else
                    Throw New NotSupportedException("Klaida. Serverio tipas '" & _
                        GetCurrentIdentity.SqlServerType.ToString & "' nepalaikomas.")
            End Select

        End Function

        Friend Function GetSqlCommandManager() As SqlServerSpecificMethods.ISqlCommandManager
            If CurrentSqlCommandManager Is Nothing Then CurrentSqlCommandManager = _
                GetSqlCommandManager(GetCurrentIdentity.SqlServerType)
            Return CurrentSqlCommandManager
        End Function

        Friend Function GetSqlQueryTimeOut() As Integer

            If Not CurrentSqlQueryTimeOut < 0 Then Return CurrentSqlQueryTimeOut

            Try
                ' try to get time out value from dll's settings (i.e. server side)
                If Not System.Configuration.ConfigurationManager. _
                    AppSettings(AppSettingsKey_SqlQueryTimeOut) Is Nothing Then

                    CurrentSqlQueryTimeOut = CInt(System.Configuration.ConfigurationManager. _
                        AppSettings(AppSettingsKey_SqlQueryTimeOut))

                    Return CurrentSqlQueryTimeOut

                End If

            Catch ex As Exception
            End Try

            ' if failed -> return default
            CurrentSqlQueryTimeOut = Default_SqlQueryTimeOut
            Return CurrentSqlQueryTimeOut

        End Function

        ''' <summary>
        ''' Check if the connection to the SQL server is valid.
        ''' If the connection is valid and database is provided, try to get the roles of the user.
        ''' </summary>
        ''' <param name="CurrentIdentity"> Identity of type AccIdentity to be authenticated.</param>
        ''' <param name="cRoles"> List of roles returned if any.</param>
        Friend Sub TryLogin(ByVal CurrentIdentity As Security.AccIdentity, ByRef cRoles As List(Of String))
            GetSqlCommandManager(CurrentIdentity.SqlServerType).TryLogin(CurrentIdentity, cRoles)
        End Sub

        ''' <summary>
        ''' Rollbacks distributed (interobject) SQL transaction.
        ''' </summary>
        Friend Sub TransactionRollBack(ByVal ex As Exception)

            If Not TransactionExists() Then Throw New Exception( _
                "Klaida. Nėra transakcijos, kurią būtų galima baigti.", ex)

            GetSqlCommandManager.TransactionRollBack(ex)

        End Sub

        ''' <summary>
        ''' Executes SQL command (not for select type) and returns LastInsertID.
        ''' </summary>
        ''' <param name="CommandToExecute"> SQL command (not of type SELECT) to be executed.</param>
        ''' <param name="RowsAffected">Returns RowsAffected param for INSERT statement.</param>
        ''' <returns>LastInsertID for INSERT statement.</returns>
        Friend Function ExecuteCommand(ByVal CommandToExecute As SQLCommand, _
            Optional ByRef RowsAffected As Integer = 0) As Long

            If String.IsNullOrEmpty(CommandToExecute.Sentence) Then _
                TransactionSafeThrow("Klaida. Negalima įvykdyti tuščio SQL sakinio.")
            If CommandToExecute.CommandType = SQLStatementType.Selection Then _
                Throw New Exception("Klaida. Negalima įvykdyti (ExecuteNonQuery) SELECT sakinio.")

            Return GetSqlCommandManager.ExecuteCommand(CommandToExecute, RowsAffected)

        End Function

        ''' <summary>
        ''' Executes SQL SELECT statement and returns datatable (not for command type).
        ''' </summary>
        ''' <param name="SelectCommand"> SQL command of type SELECT to be used for data fetching.</param>
        Friend Function FetchCommand(ByVal SelectCommand As SQLCommand) As DataTable

            If String.IsNullOrEmpty(SelectCommand.Sentence) Then _
                Throw New Exception("Klaida. Negalima įvykdyti tuščio SQL sakinio.")
            If SelectCommand.CommandType = SQLStatementType.Command Then _
                TransactionSafeThrow("Klaida. Negalima įvykdyti (Fill) ne SELECT arba CALL tipo sakinio.")

            Return GetSqlCommandManager.FetchCommand(SelectCommand)

        End Function

        ''' <summary>
        ''' Returns commands executed in the SQL transaction. Last command caused the error.
        ''' </summary>
        Friend Function GetExecutedTransactionStatementsList() As List(Of String)
            If Not ApplicationContext.LocalContext.Contains(LC_TransactionStatementListKey) Then _
                Return New List(Of String)
            Return CType(ApplicationContext.LocalContext.Item(LC_TransactionStatementListKey), _
                List(Of String))
        End Function

#End Region

#Region "*** Helper methods ***"

        Private Sub TransactionSafeThrow(ByVal ExceptionToThrow As Exception)
            If TransactionExists() Then
                TransactionRollBack(ExceptionToThrow)
            Else
                Throw (ExceptionToThrow)
            End If
        End Sub

        Private Sub TransactionSafeThrow(ByVal ErrorMessageToThrow As String, _
            Optional ByVal nExceptionType As ExceptionType = ExceptionType.UnknownException, _
            Optional ByVal nInnerException As Exception = Nothing)

            Dim ex As Exception
            If nExceptionType = ExceptionType.NotSupportedException Then
                ex = New NotSupportedException(ErrorMessageToThrow, nInnerException)
            ElseIf nExceptionType = ExceptionType.SecurityException Then
                ex = New System.Security.SecurityException(ErrorMessageToThrow, nInnerException)
            Else
                ex = New Exception(ErrorMessageToThrow, nInnerException)
            End If

            TransactionSafeThrow(ex)

        End Sub

#End Region

    End Module

End Namespace
