﻿Namespace DatabaseAccess.DatabaseStructure

    <Serializable()> _
Public Class DatabaseStructure
        Inherits BusinessBase(Of DatabaseStructure)

#Region " Business Methods "

        Private _GID As Guid = Guid.NewGuid
        Private _Name As String = ""
        Private _Description As String = ""
        Private _CharsetName As String = ""
        Private _FilePath As String = ""
        Private _DatabaseName As String = ""
        Private _Type As DatabaseStructureType = DatabaseStructureType.Other
        Private _TableList As DatabaseTableList = Nothing
        Private _StoredProcedureList As DatabaseStoredProcedureList = Nothing


        Public Property Name() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Name.Trim
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As String)
                CanWriteProperty(True)
                If value Is Nothing Then value = ""
                If _Name.Trim <> value.Trim Then
                    _Name = value.Trim
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property Description() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Description.Trim
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As String)
                CanWriteProperty(True)
                If value Is Nothing Then value = ""
                If _Description.Trim <> value.Trim Then
                    _Description = value.Trim
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public Property CharsetName() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _CharsetName.Trim
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As String)
                CanWriteProperty(True)
                If value Is Nothing Then value = ""
                If _CharsetName.Trim <> value.Trim Then
                    _CharsetName = value.Trim
                    PropertyHasChanged()
                End If
            End Set
        End Property

        Public ReadOnly Property FilePath() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _FilePath.Trim
            End Get
        End Property

        Public ReadOnly Property DatabaseName() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DatabaseName.Trim
            End Get
        End Property

        Public ReadOnly Property TableList() As DatabaseTableList
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _TableList
            End Get
        End Property

        Public ReadOnly Property StoredProcedureList() As DatabaseStoredProcedureList
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _StoredProcedureList
            End Get
        End Property

        Public ReadOnly Property DatabaseStructureSource() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If _Type = DatabaseStructureType.DatabaseMirror Then _
                    Return "Database on server: " & _DatabaseName & "."
                If _Type = DatabaseStructureType.GaugeFile Then _
                    Return "Gauge file on server."
                If IsNew Then Return "New file."
                Return "Local file: " & _FilePath & "."
            End Get
        End Property


        Public Overrides ReadOnly Property IsDirty() As Boolean
            Get
                Return MyBase.IsDirty OrElse _TableList.IsDirty OrElse _StoredProcedureList.IsDirty
            End Get
        End Property

        Public Overrides ReadOnly Property IsValid() As Boolean
            Get
                Return MyBase.IsValid AndAlso _TableList.IsValid AndAlso _StoredProcedureList.IsValid
            End Get
        End Property


        Public Overrides Function Save() As DatabaseStructure

            If Not IsValid Then Throw New Exception("Duomenų bazės struktūros apraše yra klaidų: " _
                & vbCrLf & GetAllBrokenRules())

            If _Type <> DatabaseStructureType.GaugeFile Then
                Me.DoSave()
                Return Me
            End If

            If Not CanEditObject() Then Throw New System.Security.SecurityException( _
                "Jūsų teisių nepakanka šiai operacijai atlikti.")

            Return MyBase.Save()

        End Function

        Public Function SaveAs(ByVal newFilePath As String) As DatabaseStructure
            _Type = DatabaseStructureType.Other
            _FilePath = newFilePath
            Me.DoSave()
            Return Me
        End Function


        Public Function GetAllBrokenRules() As String

            Dim result As String = _StoredProcedureList.GetAllBrokenRules.Trim

            If Not _TableList.IsValid Then
                If String.IsNullOrEmpty(result) Then
                    result = _TableList.GetAllBrokenRules
                Else
                    result = _TableList.GetAllBrokenRules & vbCrLf & result
                End If
            End If

            If Not MyBase.IsValid Then
                If String.IsNullOrEmpty(result) Then
                    result = Me.BrokenRulesCollection.ToString
                Else
                    result = Me.BrokenRulesCollection.ToString & vbCrLf & result
                End If
            End If

            Return result

        End Function


        Public Function GetCreateSql(ByVal ServerType As SqlServerType) As String

            If Not _TableList.Count > 0 Then Return "No tables to create."

            Dim SqlGenerator As SqlServerSpecificMethods.ISqlGenerator = GetSqlGenerator(ServerType)

            Dim result As String = ""

            For Each tbl As DatabaseTable In _TableList
                result = result & tbl.GetAddTableStatement("test", SqlGenerator) & vbCrLf
            Next

            For Each proc As DatabaseStoredProcedure In _StoredProcedureList
                result = result & proc.GetCreateProcedureStatement("test", SqlGenerator) & vbCrLf
            Next

            Return result

        End Function


        Protected Overrides Function GetIdValue() As Object
            Return _GID
        End Function

        ''' <summary>
        ''' Creates a new database from gauge. Should only be used server side 
        ''' in custom Company object dataportal Insert method.
        ''' </summary>
        ''' <param name="CompanyName">Name of the company to create database to.
        ''' Should only be set when in local mode using .db files.</param>
        ''' <returns>Returns the name of the created database.</returns>
        Friend Function CreateDatabase(ByVal CompanyName As String) As String

            If _TableList.Count < 1 Then Throw New Exception( _
                "Klaida. Duomenų bazės struktūros šablonas tuščias.")

            Dim CurrentIdentity As Security.AccIdentity = GetCurrentIdentity()

            If Not CurrentIdentity.IsInRole(Name_AdminRole) Then Throw New Exception( _
                "Klaida. Tik administratoriaus teises turintis vartotojas gali sukurti naują duomenų bazę.")

            Dim DbList As Security.DatabaseInfoList = Security.DatabaseInfoList.GetDatabaseListServerSide

            Dim cNewDatabaseName As String
            If CurrentIdentity.IsLocalUser Then

                cNewDatabaseName = DbList.GetNewLocalDatabaseName(CompanyName)

            Else

                cNewDatabaseName = DbList.GetNewDatabaseNameByNamingConvention

            End If

            Dim SqlGenerator As SqlServerSpecificMethods.ISqlGenerator = GetSqlGenerator(CurrentIdentity.SqlServerType)

            If SqlGenerator.SqlServerFileBased Then

                If IO.File.Exists(IO.Path.Combine(IO.Path.Combine(AppPath(), "Data"), _
                    cNewDatabaseName & Name_FileServerDatabaseFilesExtension)) Then
                    Throw New Exception(String.Format("Duomenų bazė su pavadinimu {0} jau egzistuoja.", _
                        cNewDatabaseName & Name_FileServerDatabaseFilesExtension))
                End If

            End If

            SqlGenerator.DoCreateDatabase(cNewDatabaseName, _CharsetName)

            Dim CreatedTableList As New List(Of String)
            Dim CreatedProceduresList As New List(Of String)
            Dim CreatedIndexList As New List(Of String)
            Dim TableResponsibleForError As DatabaseTable = Nothing
            Dim ProcedureResponsibleForError As DatabaseStoredProcedure = Nothing
            Dim IndexResponsibleForError As DatabaseField = Nothing

            Dim CurrentSqlCommand As String = ""

            Using ChangedDb As New ChangedDatabase(cNewDatabaseName)

                Try

                    Dim myComm As SQLCommand

                    For Each tbl As DatabaseTable In _TableList
                        CurrentSqlCommand = tbl.GetAddTableStatement(cNewDatabaseName, SqlGenerator)
                        myComm = New SQLCommand("RawSQL", CurrentSqlCommand)
                        Try
                            myComm.Execute()
                        Catch ex As Exception
                            TableResponsibleForError = tbl
                            Throw ex
                        End Try
                        CreatedTableList.Add(tbl.Name)
                    Next

                    If SqlGenerator.SupportsStoredProcedures Then
                        For Each proc As DatabaseStoredProcedure In _StoredProcedureList
                            CurrentSqlCommand = proc.GetCreateProcedureStatement(cNewDatabaseName, SqlGenerator)
                            myComm = New SQLCommand("RawSQL", CurrentSqlCommand)
                            Try
                                myComm.Execute()
                            Catch ex As Exception
                                ProcedureResponsibleForError = proc
                                Throw ex
                            End Try
                            CreatedProceduresList.Add(proc.Name)
                        Next
                    End If

                    If Not SqlGenerator.CreateStatementContainsIndexes Then
                        For Each t As DatabaseTable In _TableList
                            For Each f As DatabaseField In t.FieldList
                                If Not String.IsNullOrEmpty(f.IndexName.Trim) AndAlso Not f.PrimaryKey Then
                                    CurrentSqlCommand = f.GetCreateIndexStatement( _
                                        cNewDatabaseName, t.Name, SqlGenerator)
                                    myComm = New SQLCommand("RawSQL", CurrentSqlCommand)
                                    Try
                                        myComm.Execute()
                                    Catch ex As Exception
                                        IndexResponsibleForError = f
                                        Throw ex
                                    End Try
                                    CreatedIndexList.Add(t.Name & "." & f.Name & "." & f.IndexName)
                                End If
                            Next
                        Next
                    End If

                Catch ex As Exception

                    Dim ErrorMessage As String = "KRITINĖ KLAIDA - NEBAIGTA KURTI DUOMENŲ BAZĖ. "
                    If Not TableResponsibleForError Is Nothing Then
                        ErrorMessage = ErrorMessage & "Klaida kilo bandant sukurti lentelę '" _
                            & TableResponsibleForError.Name & "'. "
                    ElseIf Not ProcedureResponsibleForError Is Nothing Then
                        ErrorMessage = ErrorMessage & "Klaida kilo bandant sukurti procedūrą '" _
                            & ProcedureResponsibleForError.Name & "'. "
                    ElseIf Not IndexResponsibleForError Is Nothing Then
                        ErrorMessage = ErrorMessage & "Klaida kilo bandant sukurti indeksą '" _
                            & IndexResponsibleForError.IndexName & "'. "
                    End If
                    If CreatedTableList.Count > 0 Then
                        ErrorMessage = ErrorMessage & "Iki klaidos atsiradimo buvo sukurtos " _
                            & "šios duomenų bazės lentelės: " & String.Join(", ", _
                            CreatedTableList.ToArray) & "."
                    Else
                        ErrorMessage = ErrorMessage & "Iki klaidos atsiradimo nebuvo sukurta " _
                            & "nė viena lentelė šioje duomenų bazėje."
                    End If
                    If CreatedProceduresList.Count > 0 Then
                        ErrorMessage = ErrorMessage & "Iki klaidos atsiradimo buvo sukurtos " _
                            & "šios duomenų bazės procedūros: " & String.Join(", ", _
                            CreatedProceduresList.ToArray) & "."
                    Else
                        ErrorMessage = ErrorMessage & "Iki klaidos atsiradimo nebuvo sukurta " _
                            & "nė viena procedūra šioje duomenų bazėje."
                    End If
                    If CreatedIndexList.Count > 0 Then
                        ErrorMessage = ErrorMessage & "Iki klaidos atsiradimo buvo sukurti " _
                            & "šie indeksai: " & String.Join(", ", CreatedIndexList.ToArray) & "."
                    ElseIf Not SqlGenerator.CreateStatementContainsIndexes Then
                        ErrorMessage = ErrorMessage & "Iki klaidos atsiradimo nebuvo sukurtas " _
                            & "nė vienas indeksas šioje duomenų bazėje."
                    End If
                    ErrorMessage = ErrorMessage & vbCrLf & "SQL serverio klaidos pranešimas yra:" _
                        & vbCrLf & ex.Message & vbCrLf & "Klaidą sukėlė ši SQL komanda:" _
                        & vbCrLf & CurrentSqlCommand & vbCrLf & vbCrLf _
                        & "SIŪLYTINA ŠIO PRANEŠIMO TEKSTĄ PATEIKTI SISTEMOS ADMINISTRATORIUI."

                    Throw New Exception(ErrorMessage, ex)

                End Try

            End Using

            Return cNewDatabaseName

        End Function


#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()
            ValidationRules.AddRule(AddressOf CommonValidation.StringRequired, _
                New CommonValidation.StringRequiredRuleArgs("Name", "duombazės pavadinimas"))
        End Sub

#End Region

#Region " Authorization Rules "

        Protected Overrides Sub AddAuthorizationRules()
            AuthorizationRules.AllowWrite(Name_AdminRole)
        End Sub

        Public Shared Function CanAddObject() As Boolean
            Return ApplicationContext.User.IsInRole(Name_AdminRole)
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

        Public Shared Function NewDatabaseStructure() As DatabaseStructure
            Return New DatabaseStructure
        End Function

        Public Shared Function GetDatabaseStructure() As DatabaseStructure

            If Not CanGetObject() Then Throw New System.Security.SecurityException( _
                "Klaida. Jūsų teisių nepakanka šiai operacijai atlikti.")

            Return DataPortal.Fetch(Of DatabaseStructure)(New Criteria())

        End Function

        Public Shared Function GetDatabaseStructure(ByVal DatabaseName As String, _
            ByVal LocalFilePassword As String) As DatabaseStructure

            If Not CanEditObject() Then Throw New System.Security.SecurityException( _
                "Klaida. Jūsų teisių nepakanka šiai operacijai atlikti.")

            Return DataPortal.Fetch(Of DatabaseStructure)(New Criteria(DatabaseName, LocalFilePassword))

        End Function

        Public Shared Function GetDatabaseStructureFromFile(ByVal nFilePath As String) As DatabaseStructure
            Return New DatabaseStructure(nFilePath)
        End Function

        Public Shared Function GetDatabaseStructureServerSide( _
            Optional ByVal DatabaseName As String = "", _
            Optional ByVal LocalFilePassword As String = "") As DatabaseStructure

            Dim result As DatabaseStructure = New DatabaseStructure
            result.DoFetch(DatabaseName, LocalFilePassword)

            Return result

        End Function

        ''' <summary>
        ''' Database structure XML files deletion is not supported. Method will throw an exception.
        ''' </summary>
        Public Shared Sub DeleteDatabaseStructure()
            Throw New NotSupportedException("Klaida. Failų trynimas nepalaikomas.")
        End Sub

        Private Sub New()
            _TableList = DatabaseTableList.NewDatabaseTableList
            _StoredProcedureList = DatabaseStoredProcedureList.NewDatabaseStoredProcedureList
        End Sub

        Private Sub New(ByVal nFilePath As String)
            FetchFromFile(nFilePath)
            MarkOld()
            ValidationRules.CheckRules()
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> _
        Private Class Criteria
            Private _DatabaseName As String = ""
            Private _LocalFilePassword As String = ""
            Public ReadOnly Property DatabaseName() As String
                Get
                    Return _DatabaseName
                End Get
            End Property
            Public ReadOnly Property LocalFilePassword() As String
                Get
                    Return _LocalFilePassword
                End Get
            End Property
            Public Sub New()

            End Sub
            Public Sub New(ByVal nDatabaseName As String, ByVal nLocalFilePassword As String)
                _DatabaseName = nDatabaseName
                _LocalFilePassword = nLocalFilePassword
            End Sub
        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            DoFetch(criteria.DatabaseName, criteria.LocalFilePassword)
        End Sub

        Private Sub DoFetch(ByVal DatabaseName As String, ByVal LocalFilePassword As String)

            If String.IsNullOrEmpty(DatabaseName.Trim) Then
                FetchFromFile(IO.Path.Combine(AppPath(), Path_DatabaseStructureGauge))
                _Type = DatabaseStructureType.GaugeFile
                _FilePath = ""
            Else
                FetchFromDatabase(DatabaseName, LocalFilePassword)
                _Type = DatabaseStructureType.DatabaseMirror
            End If

            MarkOld()

            ValidationRules.CheckRules()

        End Sub

        Private Sub FetchFromFile(ByVal nFilePath As String)

            Dim doc As New Xml.XmlDocument
            Dim enc As New Text.UTF8Encoding(False)
            doc.LoadXml(IO.File.ReadAllText(nFilePath, enc))

            Dim ParentNodeFound As Boolean = False

            For Each node As Xml.XmlNode In doc.ChildNodes
                If node.LocalName = "DatabaseStructure" Then
                    _Name = node.Attributes.GetNamedItem("Name").Value
                    _Description = node.Attributes.GetNamedItem("Description").Value
                    _CharsetName = node.Attributes.GetNamedItem("CharsetName").Value
                    For Each childnode As Xml.XmlNode In node
                        If childnode.LocalName = "DatabaseTableList" Then
                            _TableList = DatabaseTableList.GetDatabaseTableList(childnode)
                        ElseIf childnode.LocalName = "DatabaseStoredProcedureList" Then
                            _StoredProcedureList = DatabaseStoredProcedureList. _
                                GetDatabaseStoredProcedureList(childnode)
                        End If
                    Next

                    ParentNodeFound = True

                    Exit For

                End If
            Next

            If Not ParentNodeFound Then Throw New Exception( _
                "Klaida. Failo tagai arba formatas neatitinka standarto.")

            If _TableList Is Nothing Then _TableList = DatabaseTableList.NewDatabaseTableList
            If _StoredProcedureList Is Nothing Then _StoredProcedureList = _
                DatabaseStoredProcedureList.NewDatabaseStoredProcedureList

            doc = Nothing

            _FilePath = nFilePath

        End Sub

        Private Sub FetchFromDatabase(ByVal DatabaseName As String, ByVal LocalFilePassword As String)

            If GetCurrentIdentity.IsLocalUser Then GetCurrentIdentity.SetPasswordForLocalUser(LocalFilePassword)

            Dim SqlGenerator As SqlServerSpecificMethods.ISqlGenerator = GetSqlGenerator()

            Using ChangedDb As New ChangedDatabase(DatabaseName)
                _TableList = DatabaseTableList.GetDatabaseTableList(DatabaseName, SqlGenerator)
                _StoredProcedureList = DatabaseStoredProcedureList.GetDatabaseStoredProcedureList( _
                    DatabaseName, SqlGenerator)
            End Using

            _DatabaseName = DatabaseName
            _Name = DatabaseName
            _Type = DatabaseStructureType.DatabaseMirror

        End Sub

        Protected Overrides Sub DataPortal_Insert()
            DoSave()
        End Sub

        Protected Overrides Sub DataPortal_Update()
            DoSave()
        End Sub

        Private Sub DoSave()

            If _Type = DatabaseStructureType.GaugeFile Then
                _FilePath = IO.Path.Combine(AppPath(), Path_DatabaseStructureGauge)
            ElseIf _Type = DatabaseStructureType.DatabaseMirror Then
                Throw New Exception("Klaida. Neįmanoma išsaugoti duomenų bazės veidrodžio (mirror).")
            End If

            Dim settings As New Xml.XmlWriterSettings
            settings.Encoding = New Text.UTF8Encoding(False)
            settings.Indent = True
            settings.NewLineOnAttributes = True
            settings.OmitXmlDeclaration = False

            Using writer As Xml.XmlWriter = Xml.XmlWriter.Create(_FilePath, settings)

                writer.WriteStartDocument()

                writer.WriteStartElement("DatabaseStructure")

                writer.WriteStartAttribute("Name")
                writer.WriteValue(_Name)
                writer.WriteEndAttribute()

                writer.WriteStartAttribute("Description")
                writer.WriteValue(_Description)
                writer.WriteEndAttribute()

                writer.WriteStartAttribute("CharsetName")
                writer.WriteValue(_CharsetName)
                writer.WriteEndAttribute()

                _TableList.Update(writer)
                _StoredProcedureList.Update(writer)

                writer.WriteEndElement()

                writer.WriteEndDocument()
                writer.Flush()
                writer.Close()

            End Using

            settings = Nothing

            If _Type = DatabaseStructureType.GaugeFile Then _FilePath = ""

        End Sub

#End Region

    End Class

End Namespace