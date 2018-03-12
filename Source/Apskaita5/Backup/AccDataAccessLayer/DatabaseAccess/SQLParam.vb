Namespace DatabaseAccess

    ''' <summary>
    ''' Provides an abstract parameter name/value/valuetype for a SQL command.
    ''' </summary>
    <Serializable()> _
    Public Class SQLParam
        Private _Name As String = ""
        Private _Value As Object = Nothing
        Private _ValueType As Type

        ''' <summary>
        ''' Gets parameter name.
        ''' </summary>
        Public ReadOnly Property Name() As String
            Get
                If CType(ApplicationContext.User.Identity, Security.AccIdentity).SqlServerType = _
                    SqlServerType.SQLite Then Return _Name.Replace("?", "$")
                Return _Name
            End Get
        End Property

        ''' <summary>
        ''' Gets parameter value.
        ''' </summary>
        Public ReadOnly Property Value() As Object
            Get
                Return _Value
            End Get
        End Property

        ''' <summary>
        ''' Gets the type of parameter value.
        ''' </summary>
        Public ReadOnly Property ValueType() As Type
            Get
                Return _ValueType
            End Get
        End Property

        ''' <summary>
        ''' Returns TRUE if the parameter is supposed to be replaced
        ''' and not to be used as a SQL parameter.
        ''' </summary>
        Public ReadOnly Property ToBeReplaced() As Boolean
            Get
                Return _Name.Trim.Substring(0, 2) = "**"
            End Get
        End Property

        ''' <summary>
        ''' Creates a new SQLParam object.
        ''' </summary>
        ''' <param name="ParName">The name of the parameter.</param>
        ''' <param name="ParValue">The value (object) of the parameter.</param>
        Public Sub New(ByVal ParName As String, ByVal ParValue As Object)
            _Name = ParName
            _Value = ParValue
            If _Value Is Nothing Then
                _ValueType = GetType(String)
            Else
                _ValueType = ParValue.GetType()
            End If
        End Sub

        ''' <summary>
        ''' Creates a new SQLParam object.
        ''' </summary>
        ''' <param name="ParName">The name of the parameter.</param>
        ''' <param name="ParValue">The value (object) of the parameter.</param>
        ''' <param name="ParValueType">The type of the value (object) of the parameter.</param>
        Public Sub New(ByVal ParName As String, ByVal ParValue As Object, ByVal ParValueType As Type)
            _Name = ParName
            _Value = ParValue
            _ValueType = ParValueType
        End Sub

        Public Overrides Function ToString() As String
            Dim tmp As String = _Name & "="
            If _Value Is Nothing Then
                tmp = tmp & "Nothing"
            ElseIf TypeOf _Value Is Array Then
                tmp = tmp & _ValueType.ToString
            ElseIf TypeOf _Value Is Date OrElse TypeOf _Value Is DateTime Then
                tmp = tmp & CType(_Value, Date).ToShortDateString
            Else
                tmp = tmp & _Value.ToString
            End If
            tmp = tmp & "; "
            Return tmp
        End Function

    End Class

End Namespace
