Namespace DatabaseAccess

    ''' <summary>
    ''' Provides an abstract layer for getting raw SQL statements by key.
    ''' </summary>
    Friend Module SQLDictionary

        Private _SQLdictionary As Dictionary(Of String, String) = Nothing
        ''' <summary>
        ''' Gets a raw SQL statement by key from the xml repository file.
        ''' </summary>
        ''' <param name="key">Key of a raw SQL statement in the xml repository file.</param>
        Friend ReadOnly Property GetSQLStatement(ByVal key As String) As String
            Get
                GetSQLDepository()
                If _SQLdictionary.ContainsKey(key) Then Return _SQLdictionary.Item(key)
                Throw New Exception("Klaida. Nežinomas SQL sakinys (statement), kurio kodas '" & key & "'.")
            End Get
        End Property

        ''' <summary>
        ''' Returns true if the specified key exists in the xml repository file.
        ''' </summary>
        ''' <param name="key">Key of a raw SQL statement to be searched in the xml repository file.</param>
        Friend Function SQLDepositoryKeyExists(ByVal key As String) As Boolean
            GetSQLDepository()
            Return _SQLdictionary.ContainsKey(key)
        End Function

        Friend Sub GetSQLDepository(Optional ByVal ReLoad As Boolean = False)
            If _SQLdictionary Is Nothing OrElse ReLoad Then
                Dim tmp As New DataSet
                tmp.ReadXml(GetSqlCommandManager.GetSqlDepositoryFileName())
                Dim tre As New Dictionary(Of String, String)
                For i As Integer = 1 To tmp.Tables(0).Rows.Count
                    tre.Add(tmp.Tables(0).Rows(i - 1).Item(0).ToString, _
                        tmp.Tables(0).Rows(i - 1).Item(1).ToString)
                Next
                _SQLdictionary = tre
            End If
        End Sub

        Friend Sub GetSQLDepository(ByVal nCurrentSqlServerType As SqlServerType)
            Dim tmp As New DataSet
            tmp.ReadXml(GetSqlCommandManager(nCurrentSqlServerType).GetSqlDepositoryFileName)
            Dim tre As New Dictionary(Of String, String)
            For i As Integer = 1 To tmp.Tables(0).Rows.Count
                tre.Add(tmp.Tables(0).Rows(i - 1).Item(0).ToString, _
                    tmp.Tables(0).Rows(i - 1).Item(1).ToString)
            Next

            _SQLdictionary = tre

        End Sub

    End Module

End Namespace
