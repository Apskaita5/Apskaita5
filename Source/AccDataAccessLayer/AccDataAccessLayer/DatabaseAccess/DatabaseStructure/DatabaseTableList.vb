﻿Namespace DatabaseAccess.DatabaseStructure

    <Serializable()> _
Public Class DatabaseTableList
        Inherits BusinessListBase(Of DatabaseTableList, DatabaseTable)

#Region " Business Methods "

        Protected Overrides Function AddNewCore() As Object
            Dim NewItem As DatabaseTable = DatabaseTable.NewDatabaseTable
            Me.Add(NewItem)
            Return NewItem
        End Function

        Public Function GetAllBrokenRules() As String

            Dim result As String = ""

            For i As Integer = 1 To Me.Count
                If Not String.IsNullOrEmpty(Me.Item(i - 1).BrokenRulesCollection.ToString.Trim) Then
                    If String.IsNullOrEmpty(result.Trim) Then
                        result = result & Me.Item(i - 1).BrokenRulesCollection.ToString
                    Else
                        result = result & vbCrLf & Me.Item(i - 1).BrokenRulesCollection.ToString
                    End If
                End If
            Next

            If Not IsAllIndexNamesUnique() Then
                If String.IsNullOrEmpty(result.Trim) Then
                    result = "Ne visi indeksai turi unikalius pavadinimus."
                Else
                    result = result & vbCrLf & "Ne visi indeksai turi unikalius pavadinimus."
                End If
            End If
            If Not IsAllTableNamesUnique() Then
                If String.IsNullOrEmpty(result.Trim) Then
                    result = "Ne visos lentelės turi unikalius pavadinimus."
                Else
                    result = result & vbCrLf & "Ne visos lentelės turi unikalius pavadinimus."
                End If
            End If
            If Me.Count < 1 Then
                If String.IsNullOrEmpty(result.Trim) Then
                    result = "Duomenų bazėje privalo būti bent viena lentelė."
                Else
                    result = result & vbCrLf & "Duomenų bazėje privalo būti bent viena lentelė."
                End If
            End If

            Return result
        End Function

        Public Function IsAllTableNamesUnique() As Boolean
            Dim i, j As Integer
            For i = 1 To Me.Count
                For j = i + 1 To Me.Count
                    If Me.Item(i - 1).Name.Trim.ToLower = Me.Item(j - 1).Name.Trim.ToLower Then Return False
                Next
            Next
            Return True
        End Function

        Public Function IsAllIndexNamesUnique() As Boolean

            Dim INL As New List(Of String)

            For Each tbl As DatabaseTable In Me
                For Each col As DatabaseField In tbl.FieldList
                    If Not col.PrimaryKey AndAlso Not String.IsNullOrEmpty(col.IndexName.Trim) _
                        Then INL.Add(col.IndexName.Trim)
                Next
            Next

            Dim i, j As Integer
            For i = 1 To INL.Count - 1
                For j = i + 1 To INL.Count
                    If INL(i - 1).Trim.ToUpper = INL(j - 1).Trim.ToUpper Then Return False
                Next
            Next

            Return True

        End Function

        Public Function GetTableByName(ByVal TblName As String, _
            Optional ByVal ThrowOnNotFound As Boolean = False) As DatabaseTable
            For Each i As DatabaseTable In Me
                If i.Name.Trim.ToLower = TblName.Trim.ToLower Then Return i
            Next
            If ThrowOnNotFound Then Throw New Exception("Klaida. Lentelė '" & _
                TblName.Trim & "' nerasta.")
            Return Nothing
        End Function


        Public Overrides ReadOnly Property IsValid() As Boolean
            Get
                Return MyBase.IsValid AndAlso Me.Count > 0 AndAlso IsAllTableNamesUnique()
            End Get
        End Property

#End Region

#Region " Factory Methods "

        Friend Shared Function NewDatabaseTableList() As DatabaseTableList
            Return New DatabaseTableList
        End Function

        Friend Shared Function GetDatabaseTableList(ByVal node As Xml.XmlNode) As DatabaseTableList
            Return New DatabaseTableList(node)
        End Function

        Friend Shared Function GetDatabaseTableList(ByVal DatabaseName As String, _
            ByVal SqlGenerator As SqlServerSpecificMethods.ISqlGenerator) As DatabaseTableList
            Return New DatabaseTableList(DatabaseName, SqlGenerator)
        End Function

        Private Sub New()
            Me.AllowEdit = True
            Me.AllowNew = True
            Me.AllowRemove = True
            MarkAsChild()
        End Sub

        Private Sub New(ByVal node As Xml.XmlNode)
            Me.AllowEdit = True
            Me.AllowNew = True
            Me.AllowRemove = True
            MarkAsChild()
            Fetch(node)
        End Sub

        Private Sub New(ByVal DatabaseName As String, ByVal SqlGenerator As SqlServerSpecificMethods.ISqlGenerator)
            Me.AllowEdit = True
            Me.AllowNew = True
            Me.AllowRemove = True
            MarkAsChild()
            Fetch(DatabaseName, SqlGenerator)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal node As Xml.XmlNode)

            RaiseListChangedEvents = False

            For Each childnode As Xml.XmlNode In node.ChildNodes
                If childnode.LocalName = "DatabaseTable" Then _
                    Add(DatabaseTable.GetDatabaseTable(childnode))
            Next

            RaiseListChangedEvents = True

        End Sub

        Private Sub Fetch(ByVal DatabaseName As String, ByVal SqlGenerator As SqlServerSpecificMethods.ISqlGenerator)

            Dim myComm As New SQLCommand("RawSQL", SqlGenerator.GetShowTablesStatement(DatabaseName))

            Using myData As DataTable = myComm.Fetch

                RaiseListChangedEvents = False

                If SqlGenerator.ShowTablesResultsIncludeCreateTableStatements Then

                    For Each dr As DataRow In myData.Rows
                        Add(DatabaseTable.GetDatabaseTable(dr, SqlGenerator))
                    Next

                Else

                    For Each dr As DataRow In myData.Rows

                        myComm = New SQLCommand("RawSQL", _
                            SqlGenerator.GetShowCreateTableStatement(DatabaseName, dr.Item(0).ToString.Trim))

                        Using tableData As DataTable = myComm.Fetch
                            Add(DatabaseTable.GetDatabaseTable(tableData.Rows(0), SqlGenerator))
                        End Using

                    Next

                End If

                RaiseListChangedEvents = True

            End Using

            If Not SqlGenerator.CreateStatementContainsIndexes Then

                myComm = New SQLCommand("RawSQL", SqlGenerator.GetShowIndexesStatement(DatabaseName))

                Using myData As DataTable = myComm.Fetch
                    For Each dr As DataRow In myData.Rows
                        ' ignore table name if it is included in index name
                        If dr.Item(0).ToString.Trim.ToLower.StartsWith(dr.Item(1).ToString.Trim.ToLower) _
                            AndAlso dr.Item(0).ToString.Trim.ToLower <> dr.Item(1).ToString.Trim.ToLower Then _
                            dr.Item(0) = dr.Item(0).ToString.Trim.Substring(dr.Item(1).ToString.Trim.Length)
                        Me.GetTableByName(dr.Item(1).ToString.Trim, True).FieldList. _
                            GetColumnByName(SqlGenerator.GetIndexColumnNameFromDbDefinition( _
                            dr.Item(2).ToString.Trim), True).SetIndex(dr.Item(0).ToString.Trim, _
                            SqlGenerator.IsIndexUniqueInDbDefinition(dr.Item(2).ToString))
                    Next
                End Using

            End If

        End Sub


        Friend Sub Update(ByVal writer As Xml.XmlWriter)

            RaiseListChangedEvents = False

            DeletedList.Clear()

            writer.WriteStartElement("DatabaseTableList")

            For Each item As DatabaseTable In Me
                item.Insert(writer)
            Next

            writer.WriteEndElement()

            RaiseListChangedEvents = True

        End Sub

#End Region

    End Class

End Namespace