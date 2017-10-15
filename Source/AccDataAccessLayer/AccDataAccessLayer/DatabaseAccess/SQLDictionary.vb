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

                Dim result As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
                Using data As New DataSet
                    data.ReadXml(GetSqlCommandManager.GetSqlDepositoryFileName())
                    For Each row As DataRow In data.Tables(0).Rows
                        Try
                            result.Add(row(0).ToString, row(1).ToString)
                        Catch ex As Exception
                            Dim kl As Integer = 0
                        End Try
                    Next
                End Using
                _SQLdictionary = result

            End If
        End Sub

        Friend Sub GetSQLDepository(ByVal nCurrentSqlServerType As SqlServerType)

            Dim result As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
            Using data As New DataSet
                data.ReadXml(GetSqlCommandManager(nCurrentSqlServerType).GetSqlDepositoryFileName)
                For Each row As DataRow In data.Tables(0).Rows
                    result.Add(row(0).ToString, row(1).ToString)
                Next
            End Using
            _SQLdictionary = result

        End Sub

    End Module

End Namespace
