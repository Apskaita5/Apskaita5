Imports AccDataAccessLayer.DatabaseAccess

<Serializable()> _
Public Class RawSQLFetch
    Inherits ReadOnlyBase(Of RawSQLFetch)

#Region " Business Methods "

    Private mId As Guid = New Guid
    Private _XmlResponseString As String = ""

    Public ReadOnly Property XmlResponseString() As String
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            Return _XmlResponseString
        End Get
    End Property

    Protected Overrides Function GetIdValue() As Object
        Return mId
    End Function

    Public Function GetDataTable() As DataTable

        If String.IsNullOrEmpty(_XmlResponseString) Then Return Nothing

        Dim result As New DataTable

        Try
            Using reader As New IO.StringReader(_XmlResponseString)
                result.ReadXml(reader)
            End Using
        Catch ex As Exception
            result.Dispose()
            Return Nothing
        End Try

        Return result

    End Function

#End Region

#Region " Authorization Rules "

    Protected Overrides Sub AddAuthorizationRules()

        ' TODO: add authorization rules
        'AuthorizationRules.AllowRead("", "")

    End Sub

    Public Shared Function CanGetObject() As Boolean
        Return ApplicationContext.User.IsInRole("Admin")
    End Function

#End Region

#Region " Factory Methods "

    Public Shared Function GetRawSQLFetch(ByVal RawSQLStatement As String) As RawSQLFetch
        If String.IsNullOrEmpty(RawSQLStatement.Trim) Then Return New RawSQLFetch
        Return DataPortal.Fetch(Of RawSQLFetch)(New Criteria(RawSQLStatement))
    End Function

    Public Shared Function GetRawSQLFetch(ByVal rawSqlStatement As String, _
        ByVal params As List(Of KeyValuePair(Of String, Object))) As RawSQLFetch
        If String.IsNullOrEmpty(rawSQLStatement.Trim) Then Return New RawSQLFetch
        Return DataPortal.Fetch(Of RawSQLFetch)(New Criteria(rawSqlStatement, params))
    End Function

    Private Sub New()
        ' require use of factory methods
    End Sub

#End Region

#Region " Data Access "

    <Serializable()> _
    Private Class Criteria
        Private _RawSQLStatement As String
        Private _Params As List(Of KeyValuePair(Of String, Object)) = Nothing
        Public ReadOnly Property RawSQLStatement() As String
            Get
                Return _RawSQLStatement
            End Get
        End Property
        Public ReadOnly Property Params() As List(Of KeyValuePair(Of String, Object))
            Get
                Return _Params
            End Get
        End Property
        Public Sub New(ByVal nRawSQLStatement As String)
            _RawSQLStatement = nRawSQLStatement
        End Sub
        Public Sub New(ByVal nRawSQLStatement As String, _
            ByVal nParams As List(Of KeyValuePair(Of String, Object)))
            _RawSQLStatement = nRawSQLStatement
            _Params = nParams
        End Sub
    End Class

    Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)

        If Not CanGetObject() Then
            Throw New Exception("Klaida. Vykdyti tiesiogines SQL užklausas gali tik administratorius.")
        End If

        Dim myComm As New SQLCommand("RawSQL", criteria.RawSQLStatement)
        If Not criteria.Params Is Nothing Then
            For Each param As KeyValuePair(Of String, Object) In criteria.Params
                myComm.AddParam(param.Key, param.Value)
            Next
        End If

        Dim result As DataTable = Nothing

        If myComm.CommandType = SQLStatementType.Selection Then
            result = myComm.Fetch
        Else
            Dim tmp As New DataTable
            tmp.Columns.Add("RowsAffected", GetType(Integer))
            tmp.Rows.Add()
            Try
                tmp.Rows(0).Item(0) = myComm.Execute
                result = tmp
            Catch ex As Exception
                tmp.Dispose()
                Throw ex
            End Try
        End If

        Dim SB As New System.Text.StringBuilder
        Using SW As New IO.StringWriter(SB)
            result.TableName = "result"
            result.WriteXml(SW, System.Data.XmlWriteMode.WriteSchema)
            SW.Flush()
            _XmlResponseString = SB.ToString()
        End Using

    End Sub

#End Region

End Class