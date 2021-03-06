﻿Namespace DatabaseAccess.DatabaseStructure

    <Serializable()> _
Public Class DatabaseStructureErrorList
        Inherits BusinessListBase(Of DatabaseStructureErrorList, DatabaseStructureError)

#Region " Business Methods "

        Private _DatabaseName As String
        Private _CustomErrorManager As IDatabaseStructureErrorManager
        Private _LocalFilePassword As String = ""

        Public ReadOnly Property DatabaseName() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DatabaseName.Trim
            End Get
        End Property

        Public ReadOnly Property CustomErrorManager() As IDatabaseStructureErrorManager
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _CustomErrorManager
            End Get
        End Property

        Public ReadOnly Property LocalFilePassword() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _LocalFilePassword
            End Get
        End Property

        Public Function FixableErrorsExist() As Boolean

            Dim FixableCount As Integer = 0
            For Each item As DatabaseStructureError In Me
                If item.IsChecked AndAlso item.CanBeFixedAutomatically Then
                    Return True
                End If
            Next

            Return False

        End Function

        Public Overrides Function Save() As DatabaseStructureErrorList

            If Not CanEditObject() Then Throw New System.Security.SecurityException( _
                "Klaida. Jūsų teisių nepakanka šiai operacijai atlikti.")

            If Not FixableErrorsExist() Then Throw New Exception( _
                "Klaida. Nėra pasirinktų duomenų bazės struktūros klaidų, kurias būtų galima automatiškai ištaisyti.")

            Return MyBase.Save()

        End Function

#End Region

#Region " Authorization Rules "

        Public Shared Function CanAddObject() As Boolean
            Return False
        End Function

        Public Shared Function CanGetObject() As Boolean
            Return ApplicationContext.User.IsInRole(Name_AdminRole)
        End Function

        Public Shared Function CanEditObject() As Boolean
            Return ApplicationContext.User.IsInRole(Name_AdminRole)
        End Function

        Public Shared Function CanDeleteObject() As Boolean
            Return False
        End Function

#End Region

#Region " Factory Methods "

        Public Shared Function GetDatabaseStructureErrorList(ByVal DatabaseName As String, _
            ByVal CustomErrorManager As IDatabaseStructureErrorManager, _
            ByVal nLocalFilePassword As String) As DatabaseStructureErrorList

            If Not CanGetObject() Then Throw New System.Security.SecurityException( _
                "Klaida. Jūsų teisių nepakanka šiai operacijai atlikti.")

            Return DataPortal.Fetch(Of DatabaseStructureErrorList) _
                (New Criteria(DatabaseName, CustomErrorManager, nLocalFilePassword))

        End Function

        Private Sub New()
            ' require use of factory methods
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> _
        Private Class Criteria
            Private _DatabaseName As String
            Private _CustomErrorManager As IDatabaseStructureErrorManager
            Private _LocalFilePassword As String
            Public ReadOnly Property DatabaseName() As String
                Get
                    Return _DatabaseName
                End Get
            End Property
            Public ReadOnly Property CustomErrorManager() As IDatabaseStructureErrorManager
                Get
                    Return _CustomErrorManager
                End Get
            End Property
            Public ReadOnly Property LocalFilePassword() As String
                Get
                    Return _LocalFilePassword
                End Get
            End Property
            Public Sub New(ByVal nDatabaseName As String, _
                ByVal nCustomErrorManager As IDatabaseStructureErrorManager, _
                ByVal nLocalFilePassword As String)
                _DatabaseName = nDatabaseName
                _CustomErrorManager = nCustomErrorManager
                _LocalFilePassword = nLocalFilePassword
            End Sub
        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)

            _DatabaseName = criteria.DatabaseName
            _CustomErrorManager = criteria.CustomErrorManager
            _LocalFilePassword = criteria.LocalFilePassword

            Dim SqlGenerator As SqlServerSpecificMethods.ISqlGenerator = GetSqlGenerator()

            Dim DbMirror As DatabaseStructure = DatabaseStructure.GetDatabaseStructureServerSide( _
                _DatabaseName, criteria.LocalFilePassword)
            Dim DbGauge As DatabaseStructure = DatabaseStructure.GetDatabaseStructureServerSide()

            RaiseListChangedEvents = False

            Dim i, j As Integer
            Dim IsFound As Boolean

            ' Checking if all gauge tables are present in the actual database (mirror)
            For i = 1 To DbGauge.TableList.Count
                IsFound = False
                For j = 1 To DbMirror.TableList.Count
                    If DbGauge.TableList(i - 1).Name.Trim.ToLower = _
                        DbMirror.TableList(j - 1).Name.Trim.ToLower Then

                        ' If an actual table is present then checking if it is in conformity with gauge
                        FetchTableErrors(DbGauge.TableList(i - 1), DbMirror.TableList(j - 1), _
                            SqlGenerator)
                        IsFound = True
                        Exit For

                    End If
                Next

                ' if an actual database table is not present, adding script to fix it
                If Not IsFound Then

                    Dim ErrorDescription As String = "Nerasta lentelė " & DbGauge.TableList(i - 1).Name _
                        & ", kurios apibrėžimas - " & DbGauge.TableList(i - 1).GetAddTableStatement( _
                        _DatabaseName, SqlGenerator) & "."

                    Dim SqlStatementsToCorrect As String
                    Dim CanBeFixed As Boolean
                    Try
                        SqlStatementsToCorrect = DbGauge.TableList(i - 1).GetAddTableStatement( _
                            _DatabaseName, SqlGenerator)
                        CanBeFixed = True
                    Catch ex As Exception
                        SqlStatementsToCorrect = ""
                        CanBeFixed = False
                        ErrorDescription = ErrorDescription & " Klaidos neįmanoma ištaisyti " _
                            & "automatiškai dėl šių priežasčių: " & ex.Message & "."
                    End Try

                    Add(DatabaseStructureError.GetDatabaseStructureError(ErrorDescription, _
                        DbGauge.TableList(i - 1).Name, "", SqlStatementsToCorrect, CanBeFixed, _
                        DatabaseStructureErrorType.TableMissing))

                End If

            Next

            ' Checking for obsolete tables (which are present in the actual database but not in the gauge)
            For i = 1 To DbMirror.TableList.Count

                IsFound = False
                For j = 1 To DbGauge.TableList.Count
                    If DbGauge.TableList(j - 1).Name.Trim.ToLower = _
                        DbMirror.TableList(i - 1).Name.Trim.ToLower Then
                        IsFound = True
                        Exit For
                    End If
                Next

                ' if an obsolete table is found - suggest to drop it
                If Not IsFound Then

                    Dim ErrorDescription As String = "Lentelė " & DbMirror.TableList(i - 1).Name _
                        & ", kurios apibrėžimas - " & DbMirror.TableList(i - 1).GetAddTableStatement( _
                        _DatabaseName, SqlGenerator) & " - yra perteklinė. Siūlytina ją panaikinti."

                    Dim SqlStatementsToCorrect As String
                    Dim CanBeFixed As Boolean
                    Try
                        SqlStatementsToCorrect = DbMirror.TableList(i - 1).GetDropTableStatement( _
                            _DatabaseName, SqlGenerator)
                        CanBeFixed = True
                    Catch ex As Exception
                        SqlStatementsToCorrect = ""
                        CanBeFixed = False
                        ErrorDescription = ErrorDescription & " Klaidos neįmanoma ištaisyti " _
                            & "automatiškai dėl šių priežasčių: " & ex.Message & "."
                    End Try

                    Add(DatabaseStructureError.GetDatabaseStructureError(ErrorDescription, _
                        DbMirror.TableList(i - 1).Name, "", SqlStatementsToCorrect, CanBeFixed, _
                        DatabaseStructureErrorType.TableObsolete))

                End If

            Next


            If SqlGenerator.SupportsStoredProcedures Then

                ' Checking if all gauge stored procedures are present
                For i = 1 To DbGauge.StoredProcedureList.Count
                    IsFound = False
                    For j = 1 To DbMirror.StoredProcedureList.Count
                        If DbGauge.StoredProcedureList(i - 1).Name.ToLower = _
                            DbMirror.StoredProcedureList(j - 1).Name.ToLower Then

                            ' If actual stored procedure is present, 
                            'then checking if it is in conformity with the gauge
                            FetchStoredProcedureErrors(DbGauge.StoredProcedureList(i - 1), _
                                DbMirror.StoredProcedureList(j - 1), SqlGenerator)
                            IsFound = True

                        End If
                    Next

                    ' if an actual stored procedure is not present, adding script to add it
                    If Not IsFound Then

                        Dim ErrorDescription As String = "Nerasta stored procedure " _
                            & DbGauge.StoredProcedureList(i - 1).Name & ", kurios apibrėžimas - " _
                            & DbGauge.StoredProcedureList(i - 1).GetCreateProcedureStatement( _
                            _DatabaseName, SqlGenerator) & "."

                        Dim SqlStatementsToCorrect As String
                        Dim CanBeFixed As Boolean
                        Try
                            SqlStatementsToCorrect = DbGauge.StoredProcedureList(i - 1). _
                                GetCreateProcedureStatement(_DatabaseName, SqlGenerator)
                            CanBeFixed = True
                        Catch ex As Exception
                            SqlStatementsToCorrect = ""
                            CanBeFixed = False
                            ErrorDescription = ErrorDescription & " Klaidos neįmanoma ištaisyti " _
                                & "automatiškai dėl šių priežasčių: " & ex.Message & "."
                        End Try

                        Add(DatabaseStructureError.GetDatabaseStructureError(ErrorDescription, _
                            "", "", SqlStatementsToCorrect, CanBeFixed, _
                            DatabaseStructureErrorType.ProcedureMissing))

                    End If
                Next

                ' Checking for obsolete stored procedures 
                '(which are present in the actual database but not in the gauge)
                For i = 1 To DbMirror.StoredProcedureList.Count
                    IsFound = False
                    For j = 1 To DbGauge.StoredProcedureList.Count
                        If DbGauge.StoredProcedureList(j - 1).Name.ToLower = _
                            DbMirror.StoredProcedureList(i - 1).Name.ToLower Then
                            IsFound = True
                            Exit For
                        End If
                    Next

                    ' if an obsolete stored procedure is found - suggest to remove it
                    If Not IsFound Then

                        Dim ErrorDescription As String = "Stored procedure - " _
                            & DbMirror.StoredProcedureList(i - 1).Name & ", kurios apibrėžimas - " _
                            & DbMirror.StoredProcedureList(i - 1).GetCreateProcedureStatement( _
                            _DatabaseName, SqlGenerator) & " - yra perteklinė. Siūlytina panaikinti."

                        Dim SqlStatementsToCorrect As String
                        Dim CanBeFixed As Boolean
                        Try
                            SqlStatementsToCorrect = DbMirror.StoredProcedureList(i - 1). _
                                GetDropProcedureStatement(_DatabaseName, SqlGenerator)
                            CanBeFixed = True
                        Catch ex As Exception
                            SqlStatementsToCorrect = ""
                            CanBeFixed = False
                            ErrorDescription = ErrorDescription & " Klaidos neįmanoma ištaisyti " _
                                & "automatiškai dėl šių priežasčių: " & ex.Message & "."
                        End Try

                        Add(DatabaseStructureError.GetDatabaseStructureError(ErrorDescription, _
                            "", "", SqlStatementsToCorrect, CanBeFixed, _
                            DatabaseStructureErrorType.ProcedureObsolete))

                    End If

                Next

            End If

            ' check for custom errors, defined by business developer
            If Not _CustomErrorManager Is Nothing Then

                Using changedDb As New ChangedDatabase(_DatabaseName)
                    _CustomErrorManager.FetchCustomErrors(Me, DbMirror, _DatabaseName)
                End Using

            End If

            RaiseListChangedEvents = True

        End Sub

        Private Sub FetchTableErrors(ByVal GaugeTable As DatabaseTable, _
            ByVal MirrorTable As DatabaseTable, ByVal SqlGenerator As SqlServerSpecificMethods.ISqlGenerator)


            Dim i, j As Integer
            Dim IsFound As Boolean

            ' Checking if all gauge fields are present
            For i = 1 To GaugeTable.FieldList.Count
                IsFound = False
                For j = 1 To MirrorTable.FieldList.Count
                    If GaugeTable.FieldList(i - 1).Name.Trim.ToLower = _
                        MirrorTable.FieldList(j - 1).Name.Trim.ToLower Then

                        ' If an actual field is present, then checking if it is in conformity with the gauge
                        FetchFieldErrors(GaugeTable.FieldList(i - 1), MirrorTable.FieldList(j - 1), _
                            MirrorTable.Name, SqlGenerator)
                        IsFound = True
                        Exit For

                    End If
                Next

                ' If an actual field is not present, adding script to add it
                If Not IsFound Then

                    Dim ErrorDescription As String = "Nerastas lentelės " & GaugeTable.Name _
                        & " lauko " & GaugeTable.FieldList(i - 1).Name & " apibrėžimas. Turi būti - " _
                        & GaugeTable.FieldList(i - 1).GetFieldDbDefinition(SqlGenerator) & "."

                    Dim SqlStatementsToCorrect As String
                    Dim CanBeFixed As Boolean
                    Try
                        SqlStatementsToCorrect = GaugeTable.FieldList(i - 1).GetAddFieldStatement( _
                            _DatabaseName, MirrorTable.Name, SqlGenerator)
                        CanBeFixed = True
                    Catch ex As Exception
                        SqlStatementsToCorrect = ""
                        CanBeFixed = False
                        ErrorDescription = ErrorDescription & " Klaidos neįmanoma ištaisyti " _
                            & "automatiškai dėl šių priežasčių: " & ex.Message & "."
                    End Try

                    Add(DatabaseStructureError.GetDatabaseStructureError(ErrorDescription, _
                        MirrorTable.Name, GaugeTable.FieldList(i - 1).Name, _
                        SqlStatementsToCorrect, CanBeFixed, DatabaseStructureErrorType.FieldMissing))

                End If

            Next

            ' Checking for obsolete fields
            For i = 1 To MirrorTable.FieldList.Count

                IsFound = False
                For j = 1 To GaugeTable.FieldList.Count
                    If GaugeTable.FieldList(j - 1).Name.Trim.ToLower = _
                        MirrorTable.FieldList(i - 1).Name.Trim.ToLower Then
                        IsFound = True
                        Exit For
                    End If
                Next

                If Not IsFound Then

                    Dim ErrorDescription As String = "Lentelės " & MirrorTable.Name _
                        & " laukas " & MirrorTable.FieldList(i - 1).Name & ", kurio apibrėžimas - " _
                        & MirrorTable.FieldList(i - 1).GetFieldDbDefinition(SqlGenerator) _
                        & " yra perteklinis. Siūlytina jį panaikinti."

                    Dim SqlStatementsToCorrect As String
                    Dim CanBeFixed As Boolean
                    Try
                        SqlStatementsToCorrect = MirrorTable.FieldList(i - 1).GetDropFieldStatement( _
                            _DatabaseName, MirrorTable.Name, SqlGenerator)
                        CanBeFixed = True
                    Catch ex As Exception
                        SqlStatementsToCorrect = ""
                        CanBeFixed = False
                        ErrorDescription = ErrorDescription & " Klaidos neįmanoma ištaisyti " _
                            & "automatiškai dėl šių priežasčių: " & ex.Message & "."
                    End Try

                    Add(DatabaseStructureError.GetDatabaseStructureError(ErrorDescription, _
                        MirrorTable.Name, MirrorTable.FieldList(i - 1).Name, _
                        SqlStatementsToCorrect, CanBeFixed, DatabaseStructureErrorType.FieldObsolete))

                End If

            Next

        End Sub

        Private Sub FetchFieldErrors(ByVal GaugeField As DatabaseField, _
            ByVal MirrorField As DatabaseField, ByVal TableName As String, _
            ByVal SqlGenerator As SqlServerSpecificMethods.ISqlGenerator)

            If DatabaseField.AreFieldsDifferent(GaugeField, MirrorField, SqlGenerator) Then

                Dim ErrorDescription As String = "Klaidingas lentelės " & TableName.Trim _
                    & " lauko " & MirrorField.Name.Trim & " apibrėžimas. Turi būti - " _
                    & GaugeField.GetFieldDbDefinition(SqlGenerator) & ", o yra - " _
                    & MirrorField.GetFieldDbDefinition(SqlGenerator) & "."

                Dim SqlStatementsToCorrect As String
                Dim CanBeFixed As Boolean
                Try
                    SqlStatementsToCorrect = GaugeField.GetModifyFieldStatement(_DatabaseName, _
                        TableName.Trim, SqlGenerator)
                    CanBeFixed = True
                Catch ex As Exception
                    SqlStatementsToCorrect = ""
                    CanBeFixed = False
                    ErrorDescription = ErrorDescription & " Klaidos neįmanoma ištaisyti " _
                        & "automatiškai dėl šių priežasčių: " & ex.Message & "."
                End Try

                Add(DatabaseStructureError.GetDatabaseStructureError(ErrorDescription, _
                    TableName.Trim, MirrorField.Name, SqlStatementsToCorrect, CanBeFixed, _
                    DatabaseStructureErrorType.FieldDefinitionObsolete))

            End If

            If GaugeField.Indexed AndAlso Not MirrorField.Indexed Then

                Dim ErrorDescription As String = "Lentelės " & TableName.Trim _
                    & " laukas " & MirrorField.Name.Trim & " turi būti indeksuojamas."

                Dim SqlStatementsToCorrect As String
                Dim CanBeFixed As Boolean
                Try
                    SqlStatementsToCorrect = GaugeField.GetCreateIndexStatement(_DatabaseName, _
                        TableName.Trim, SqlGenerator)
                    CanBeFixed = True
                Catch ex As Exception
                    SqlStatementsToCorrect = ""
                    CanBeFixed = False
                    ErrorDescription = ErrorDescription & " Klaidos neįmanoma ištaisyti " _
                        & "automatiškai dėl šių priežasčių: " & ex.Message & "."
                End Try

                Add(DatabaseStructureError.GetDatabaseStructureError(ErrorDescription, _
                    TableName.Trim, MirrorField.Name, SqlStatementsToCorrect, CanBeFixed, _
                    DatabaseStructureErrorType.IndexMissing))

            ElseIf Not GaugeField.Indexed AndAlso MirrorField.Indexed Then

                Dim ErrorDescription As String = "Lentelės " & TableName.Trim _
                    & " laukas " & MirrorField.Name.Trim & " neturi būti indeksuojamas."

                Dim SqlStatementsToCorrect As String
                Dim CanBeFixed As Boolean
                Try
                    SqlStatementsToCorrect = MirrorField.GetDropIndexStatement(_DatabaseName, _
                        TableName.Trim, SqlGenerator)
                    CanBeFixed = True
                Catch ex As Exception
                    SqlStatementsToCorrect = ""
                    CanBeFixed = False
                    ErrorDescription = ErrorDescription & " Klaidos neįmanoma ištaisyti " _
                        & "automatiškai dėl šių priežasčių: " & ex.Message & "."
                End Try

                Add(DatabaseStructureError.GetDatabaseStructureError(ErrorDescription, _
                    TableName.Trim, MirrorField.Name, SqlStatementsToCorrect, CanBeFixed, _
                    DatabaseStructureErrorType.IndexObsolete))

            End If

        End Sub

        Private Sub FetchStoredProcedureErrors(ByVal GaugeProcedure As DatabaseStoredProcedure, _
            ByVal MirrorProcedure As DatabaseStoredProcedure, ByVal SqlGenerator As SqlServerSpecificMethods.ISqlGenerator)

            If GaugeProcedure.SourceCodeComparable.ToLower.Replace(VbCrLf, " "). _
                Replace(VbCr, " ").Replace(VbLf, " ").Replace("  ", " ").Replace("  ", " ") <> _
                MirrorProcedure.SourceCodeComparable.ToLower.Replace(VbCrLf, " "). _
                Replace(VbCr, " ").Replace(VbLf, " ").Replace("  ", " ").Replace("  ", " ") Then

                Dim ErrorDescription As String = "Klaidingas stored procedure " _
                    & MirrorProcedure.Name.Trim & " apibrėžimas. Turi būti - " _
                    & GaugeProcedure.GetCreateProcedureStatement(_DatabaseName, SqlGenerator) _
                    & ", o yra - " & MirrorProcedure.GetCreateProcedureStatement(_DatabaseName, _
                    SqlGenerator) & "."

                Dim SqlStatementsToCorrect As String
                Dim CanBeFixed As Boolean
                Try
                    SqlStatementsToCorrect = GaugeProcedure.GetUpdateProcedureStatement( _
                        _DatabaseName, SqlGenerator)
                    CanBeFixed = True
                Catch ex As Exception
                    SqlStatementsToCorrect = ""
                    CanBeFixed = False
                    ErrorDescription = ErrorDescription & " Klaidos neįmanoma ištaisyti " _
                        & "automatiškai dėl šių priežasčių: " & ex.Message & "."
                End Try

                Add(DatabaseStructureError.GetDatabaseStructureError(ErrorDescription, _
                    "", "", SqlStatementsToCorrect, CanBeFixed, _
                    DatabaseStructureErrorType.ProcedureDefinitionObsolete))

            End If

        End Sub


        Protected Overrides Sub DataPortal_Update()

            If GetCurrentIdentity.IsLocalUser Then GetCurrentIdentity.SetPasswordForLocalUser(_LocalFilePassword)

            RaiseListChangedEvents = False

            DeletedList.Clear()

            Using curDatabase As New ChangedDatabase(_DatabaseName)

                For Each item As DatabaseStructureError In Me
                    If item.IsChecked AndAlso item.CanBeFixedAutomatically _
                        AndAlso Not item.IsFixed AndAlso item.IsComplexError Then item.Update(Me)
                Next

                For Each item As DatabaseStructureError In Me
                    If item.IsChecked AndAlso item.CanBeFixedAutomatically _
                        AndAlso Not item.IsFixed Then item.Update(Me)
                Next

                For i As Integer = Me.Count To 1 Step -1
                    If Me.Item(i - 1).IsFixed Then Me.RemoveAt(i - 1)
                Next
                DeletedList.Clear()

            End Using

            RaiseListChangedEvents = True

        End Sub

#End Region

    End Class

End Namespace