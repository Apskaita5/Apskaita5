Imports fyiReporting.Data

Public Class AccServiceDataParameter
    Implements IDbDataParameter

    Private _Name As String = ""
    Private _Value As Object = Nothing
    Private _Precision As Byte = 2
    Private _DbType As DbType = DbType.String


    Public Sub New()
    End Sub


#Region "IDbDataParameter Members"

    Public Property Precision() As Byte _
        Implements IDbDataParameter.Precision
        Get
            Return _Precision
        End Get
        Set(ByVal value As Byte)
            _Precision = value
        End Set
    End Property

    Public Property Scale() As Byte _
        Implements IDbDataParameter.Scale
        Get
            Return 0
        End Get
        Set(ByVal value As Byte)
            Throw New NotImplementedException("Scale setting is not implemented")
        End Set
    End Property

    Public Property Size() As Integer _
        Implements IDbDataParameter.Size
        Get
            Return 0
        End Get
        Set(ByVal value As Integer)
            Throw New NotImplementedException("Size setting is not implemented")
        End Set
    End Property

#End Region

#Region "IDataParameter Members"

    Public Property Direction() As ParameterDirection _
        Implements IDataParameter.Direction
        Get
            ' only support input parameter
            Return ParameterDirection.Input
        End Get
        Set(ByVal value As ParameterDirection)
            If value <> ParameterDirection.Input Then
                Throw New ArgumentException("Parameter Direction must be Input")
            End If
        End Set
    End Property

    Public Property DbType() As DbType _
        Implements IDataParameter.DbType
        Get
            Return _DbType
        End Get
        Set(ByVal value As System.Data.DbType)
            _DbType = value
        End Set
    End Property

    Public Property Value() As Object _
        Implements IDataParameter.Value
        Get
            Return _Value
        End Get
        Set(ByVal value As Object)
            _Value = value

            'If _Value Is Nothing Then
            '    Return DbType.String
            'ElseIf TypeOf _Value Is DateTime Then
            '    Return DbType.DateTime
            'ElseIf TypeOf _Value Is Date Then
            '    Return DbType.Date
            'ElseIf TypeOf _Value Is System.Int16 Then
            '    Return DbType.Int16
            'ElseIf TypeOf _Value Is System.Int32 Then
            '    Return DbType.Int32
            'ElseIf TypeOf _Value Is System.Int64 Then
            '    Return DbType.Int64
            'ElseIf TypeOf _Value Is System.Byte Then
            '    Return DbType.Byte
            'ElseIf TypeOf _Value Is System.Double Then
            '    Return DbType.Double
            'ElseIf TypeOf _Value Is System.Decimal Then
            '    Return DbType.Decimal
            'ElseIf TypeOf _Value Is System.Byte() Then
            '    Return DbType.Binary
            'ElseIf TypeOf _Value Is System.Boolean Then
            '    Return DbType.Boolean
            'Else
            '    Return DbType.Object
            'End If

        End Set
    End Property

    Public ReadOnly Property IsNullable() As Boolean _
        Implements IDataParameter.IsNullable
        Get
            Return False
        End Get
    End Property

    Public Property SourceVersion() As DataRowVersion _
        Implements IDataParameter.SourceVersion
        Get
            Return DataRowVersion.Current
        End Get
        Set(ByVal value As System.Data.DataRowVersion)
            Throw New NotImplementedException("Setting DataRowVersion is not implemented.")
        End Set
    End Property

    Public Property ParameterName() As String _
        Implements IDataParameter.ParameterName
        Get
            Return _Name
        End Get
        Set(ByVal value As String)
            If value Is Nothing OrElse String.IsNullOrEmpty(value.Trim) Then
                _Name = ""
            Else
                _Name = value
            End If
        End Set
    End Property

    Public Property SourceColumn() As String _
        Implements IDataParameter.SourceColumn
        Get
            Return Nothing
        End Get
        Set(ByVal value As String)
            Throw New NotImplementedException("Setting SourceColumn is not implemented.")
        End Set
    End Property

#End Region

End Class
