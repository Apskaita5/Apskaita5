Imports AccDataAccessLayer.DatabaseAccess.DatabaseStructure

Public Class AccServiceDataReader
    Implements IDataReader

    Private ReadOnly ParamKeys As String() = New String() {"?AA", "?AB", _
        "?AC", "?AD", "?AE", "?AF", "?AG", "?AH", "?AI", "?AJ", "?AK", "?AL", _
        "?AM", "?AN", "?AO", "?AP", "?AQ", "?AR", "?AT", "?AW", "?AZ", "?BA", _
        "?BC", "?BD", "?BE", "?BF", "?BG", "?BH", "?BI", "?BJ", "?BK", "?BL", _
        "?BM", "?BN", "?BO", "?BP", "?BQ", "?BR", "?BS", "?BT", "?BW", "?BZ", _
        "?CA", "?CB", "?CC", "?CD", "?CE", "?CF", "?CG", "?CH"}

    Private _Conn As AccServiceConnection
    Private _Cmd As AccServiceCommand
    Private _Behavior As CommandBehavior

    Private _DataTable As DataTable
    Private _Enumerator As IEnumerator


    Public Sub New(ByVal behavior As CommandBehavior, ByVal conn As AccServiceConnection, _
        ByVal cmd As AccServiceCommand)

        _Conn = conn
        _Cmd = cmd
        _Behavior = behavior

        If _Cmd Is Nothing Then
            Throw New ArgumentNullException("cmd")
        End If

        If _Cmd.CommandText.Trim.ToLower = "select tables;" Then
            InitDatabaseSchemaTable()
        Else
            InitQueryTable()
        End If

        _Enumerator = New DataTableEnum(_DataTable)

    End Sub

    Private Sub InitDatabaseSchemaTable()

        Dim dbSchema As DatabaseStructure = DatabaseStructure.GetDatabaseStructure()

        If dbSchema Is Nothing Then
            Throw New InvalidOperationException("Failed to fetch database schema.")
        End If

        _DataTable = New DataTable()
        _DataTable.Columns.Add("TABLE_NAME", GetType(String))
        _DataTable.Columns.Add("TABLE_TYPE", GetType(String))

        For Each tbl As DatabaseTable In dbSchema.TableList
            _DataTable.Rows.Add(tbl.Name, "TABLE")
        Next

    End Sub

    Private Sub InitQueryTable()

        Dim params As New List(Of KeyValuePair(Of String, Object))
        Dim i As Integer = 0
        Dim commandText As String = _Cmd.CommandText

        For Each param As IDataParameter In _Cmd.Parameters

            If commandText.Contains(param.ParameterName) Then

                commandText = commandText.Replace(param.ParameterName, ParamKeys(i))
                params.Add(New KeyValuePair(Of String, Object) _
                    (ParamKeys(i), param.Value))
                i += 1

            End If

        Next

        _DataTable = AccDataAccessLayer.RawSQLFetch.GetRawSQLFetch( _
            commandText, params).GetDataTable()

    End Sub


    Private Class DataTableEnum
        Implements IEnumerator

        Private _DataTable As DataTable

        ' Enumerators are positioned before the first element
        ' until the first MoveNext() call.
        Dim position As Integer = -1

        Public Sub New(ByVal dataTable As DataTable)
            _DataTable = dataTable
        End Sub

        Public Function MoveNext() As Boolean Implements IEnumerator.MoveNext
            position = position + 1
            Return (position < _DataTable.Rows.Count)
        End Function

        Public Sub Reset() Implements IEnumerator.Reset
            position = -1
        End Sub

        Public ReadOnly Property Current() As Object Implements IEnumerator.Current
            Get
                Try
                    Return _DataTable.Rows(position)
                Catch ex As IndexOutOfRangeException
                    Throw New InvalidOperationException()
                End Try
            End Get
        End Property

    End Class

#Region "IDataReader Members"

    Public ReadOnly Property RecordsAffected() As Integer _
        Implements IDataReader.RecordsAffected
        Get
            Return 0
        End Get
    End Property

    Public ReadOnly Property IsClosed() As Boolean _
        Implements IDataReader.IsClosed
        Get
            Return _DataTable Is Nothing
        End Get
    End Property

    Public Function NextResult() As Boolean _
        Implements IDataReader.NextResult
        Return False
    End Function

    Public Sub Close() Implements IDataReader.Close
        _Enumerator = Nothing
        If Not _DataTable Is Nothing Then
            _DataTable.Dispose()
        End If
        _DataTable = Nothing
    End Sub

    Public Function Read() As Boolean _
        Implements IDataReader.Read
        If _Enumerator Is Nothing OrElse Not _Enumerator.MoveNext() Then
            Return False
        End If
        Return True
    End Function

    Public ReadOnly Property Depth() As Integer _
        Implements IDataReader.Depth
        Get
            Return 0
        End Get
    End Property

    Public Function GetSchemaTable() As DataTable _
        Implements IDataReader.GetSchemaTable

        If _DataTable Is Nothing Then
            Throw New InvalidOperationException("Data is not loaded or already closed.")
        End If

        Dim result As New DataTable
        result.Columns.Add("ColumnName", GetType(String))
        result.Columns.Add("DataType", GetType(Type))
        result.Columns.Add("ColumnSize", GetType(Long))
        result.Columns.Add("IsKeyColumn", GetType(Boolean))
        result.Columns.Add("IsAutoIncrement", GetType(Boolean))

        For Each col As DataColumn In _DataTable.Columns
            Dim size As Long = 0
            Try
                size = System.Runtime.InteropServices.Marshal.SizeOf(col.DataType)
            Catch ex As Exception
            End Try
            result.Rows.Add(col.ColumnName, col.DataType, size, False, col.AutoIncrement)
        Next

        Return result

    End Function

#End Region

#Region "IDisposable Members"

    Private disposedValue As Boolean = False        ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                Try
                    Me.Close()
                Catch ex As Exception
                End Try
            End If

            ' TODO: free your own state (unmanaged objects).
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

#End Region

#Region "IDataRecord Members"

    Public Function GetInt32(ByVal i As Integer) As Integer _
        Implements IDataRecord.GetInt32
        If _Enumerator.Current Is Nothing Then Return 0
        Return Convert.ToInt32(DirectCast(_Enumerator.Current, DataRow).Item(i))
    End Function

    Default Public ReadOnly Property Item(ByVal name As String) As Object _
        Implements IDataRecord.Item
        Get
            If _Enumerator.Current Is Nothing Then Return Nothing
            Return DirectCast(_Enumerator.Current, DataRow).Item(Me.GetOrdinal(name))
        End Get
    End Property

    Default Public ReadOnly Property Item(ByVal i As Integer) As Object _
        Implements IDataRecord.Item
        Get
            If _Enumerator.Current Is Nothing Then Return Nothing
            Return DirectCast(_Enumerator.Current, DataRow).Item(i)
        End Get
    End Property

    Public Function GetValue(ByVal i As Integer) As Object _
        Implements IDataRecord.GetValue
        If _Enumerator.Current Is Nothing Then Return Nothing
        Return DirectCast(_Enumerator.Current, DataRow).Item(i)
    End Function

    Public Function IsDBNull(ByVal i As Integer) As Boolean _
        Implements IDataRecord.IsDBNull
        If _Enumerator.Current Is Nothing Then Return True
        Dim result As Object = DirectCast(_Enumerator.Current, DataRow).Item(i)
        Return (result Is Nothing OrElse Convert.IsDBNull(result))
    End Function

    Public Function GetBytes(ByVal i As Integer, ByVal fieldOffset As Long, _
        ByVal buffer As Byte(), ByVal bufferoffset As Integer, _
        ByVal length As Integer) As Long _
        Implements IDataRecord.GetBytes
        Throw New NotSupportedException("GetBytes not supported.")
    End Function

    Public Function GetByte(ByVal i As Integer) As Byte _
        Implements IDataRecord.GetByte
        If _Enumerator.Current Is Nothing Then Return 0
        Return Convert.ToByte(DirectCast(_Enumerator.Current, DataRow).Item(i))
    End Function

    Public Function GetFieldType(ByVal i As Integer) As Type _
        Implements IDataRecord.GetFieldType
        If _DataTable Is Nothing Then
            Throw New InvalidOperationException("Data is not loaded or already closed.")
        End If
        Return _DataTable.Columns(i).DataType
    End Function

    Public Function GetDecimal(ByVal i As Integer) As Decimal _
        Implements IDataRecord.GetDecimal
        If _Enumerator.Current Is Nothing Then Return 0
        Return Convert.ToDecimal(DirectCast(_Enumerator.Current, DataRow).Item(i))
    End Function

    Public Function GetValues(ByVal values As Object()) As Integer _
        Implements IDataRecord.GetValues
        If _Enumerator.Current Is Nothing Then Return Nothing
        For i As Integer = 0 To Math.Min(values.Length - 1, _
            _DataTable.Columns.Count - 1)
            values(i) = DirectCast(_Enumerator.Current, DataRow).Item(i)
        Next
        For i As Integer = _DataTable.Columns.Count To values.Length - 1
            values(i) = System.DBNull.Value
        Next
        Return Math.Min(values.Length, _DataTable.Columns.Count)
    End Function

    Public Function GetName(ByVal i As Integer) As String _
        Implements IDataRecord.GetName
        If _DataTable Is Nothing Then
            Throw New InvalidOperationException("Data is not loaded or already closed.")
        End If
        Return _DataTable.Columns(i).ColumnName
    End Function

    Public ReadOnly Property FieldCount() As Integer _
        Implements IDataRecord.FieldCount
        Get
            If _DataTable Is Nothing Then Return 0
            Return _DataTable.Columns.Count
        End Get
    End Property

    Public Function GetInt64(ByVal i As Integer) As Long _
        Implements IDataRecord.GetInt64
        If _Enumerator.Current Is Nothing Then Return 0
        Return Convert.ToInt64(DirectCast(_Enumerator.Current, DataRow).Item(i))
    End Function

    Public Function GetDouble(ByVal i As Integer) As Double _
        Implements IDataRecord.GetDouble
        If _Enumerator.Current Is Nothing Then Return 0
        Return Convert.ToDouble(DirectCast(_Enumerator.Current, DataRow).Item(i))
    End Function

    Public Function GetBoolean(ByVal i As Integer) As Boolean _
        Implements IDataRecord.GetBoolean
        If _Enumerator.Current Is Nothing Then Return False
        Dim intTypes As Type() = New Type() {GetType(Int16), GetType(Int32), _
            GetType(Int64), GetType(Byte)}
        If Array.IndexOf(intTypes, GetFieldType(i)) < 0 Then
            Return Convert.ToBoolean(DirectCast(_Enumerator.Current, DataRow).Item(i))
        Else
            Return Convert.ToInt64(DirectCast(_Enumerator.Current, DataRow).Item(i)) > 0
        End If
    End Function

    Public Function GetGuid(ByVal i As Integer) As Guid _
        Implements IDataRecord.GetGuid
        Return New Guid(GetString(i))
    End Function

    Public Function GetDateTime(ByVal i As Integer) As DateTime _
        Implements IDataRecord.GetDateTime
        If _Enumerator.Current Is Nothing Then Return DateTime.MinValue
        Return Convert.ToDateTime(DirectCast(_Enumerator.Current, DataRow).Item(i))
    End Function

    Public Function GetOrdinal(ByVal name As String) As Integer _
        Implements IDataRecord.GetOrdinal

        If name Is Nothing OrElse String.IsNullOrEmpty(name.Trim) Then
            Throw New ArgumentNullException("name")
        ElseIf _DataTable Is Nothing Then
            Throw New InvalidOperationException("Data is not loaded or already closed.")
        End If

        For Each col As DataColumn In _DataTable.Columns
            If col.ColumnName.Trim.ToLower = name.Trim.ToLower Then
                Return col.Ordinal
            End If
        Next

        Throw New ArgumentException(String.Format("Column '{0}' not known.", name))

    End Function

    Public Function GetDataTypeName(ByVal i As Integer) As String _
        Implements IDataRecord.GetDataTypeName
        Return Me.GetFieldType(i).ToString()
    End Function

    Public Function GetFloat(ByVal i As Integer) As Single _
        Implements IDataRecord.GetFloat
        If _Enumerator.Current Is Nothing Then Return 0
        Return Convert.ToSingle(DirectCast(_Enumerator.Current, DataRow).Item(i))
    End Function

    Public Function GetData(ByVal i As Integer) As IDataReader _
        Implements IDataRecord.GetData
        Throw New NotSupportedException("GetData not supported.")
    End Function

    Public Function GetChars(ByVal i As Integer, ByVal fieldoffset As Long, _
        ByVal buffer As Char(), ByVal bufferoffset As Integer, _
        ByVal length As Integer) As Long _
        Implements IDataRecord.GetChars
        Throw New NotSupportedException("GetChars not supported.")
    End Function

    Public Function GetString(ByVal i As Integer) As String _
        Implements IDataRecord.GetString
        If _Enumerator.Current Is Nothing Then Return ""
        Return Convert.ToString(DirectCast(_Enumerator.Current, DataRow).Item(i))
    End Function

    Public Function GetChar(ByVal i As Integer) As Char _
        Implements IDataRecord.GetChar
        If _Enumerator.Current Is Nothing Then Return Nothing
        Return Convert.ToChar(DirectCast(_Enumerator.Current, DataRow).Item(i))
    End Function

    Public Function GetInt16(ByVal i As Integer) As Short _
        Implements IDataRecord.GetInt16
        If _Enumerator.Current Is Nothing Then Return 0
        Return Convert.ToInt16(DirectCast(_Enumerator.Current, DataRow).Item(i))
    End Function

#End Region

End Class
