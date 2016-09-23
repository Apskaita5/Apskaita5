Namespace Attributes

    ''' <summary>
    ''' Represents an attribute that is used to mark business objects' properties holding a reference
    ''' to a <see cref="HelperLists.CashAccountInfo">CashAccountInfo</see> and to store  basic business rules 
    ''' (mandatory, allowed types, etc.).
    ''' </summary>
    ''' <remarks>Used for validation purposes in <see cref="CashAccountFieldValidation">CashAccountFieldValidation</see> method.
    ''' Could be used by GUI to initialize appropriate controls.</remarks>
    <Serializable()> _
    <AttributeUsage(AttributeTargets.Property, AllowMultiple:=False, Inherited:=True)> _
    Public Class CashAccountFieldAttribute
        Inherits ValueObjectFieldAttribute
        Implements IValueObjectIdProvider, IDataSourceProvider

        Private _AcceptedTypes As Documents.CashAccountType()
        Private _ErrorOnTypeMismatch As Boolean = False

        ''' <summary>
        ''' A list of the allowed <see cref="Documents.CashAccountType">cash account types</see>.
        ''' </summary>
        ''' <remarks>Defaults to <see cref="Documents.CashAccountType">all the possible cash account types</see>.</remarks>
        Public ReadOnly Property AcceptedTypes() As Documents.CashAccountType()
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If _AcceptedTypes Is Nothing OrElse _AcceptedTypes.Length < 1 Then
                    _AcceptedTypes = New Documents.CashAccountType() {}
                End If
                Return _AcceptedTypes
            End Get
        End Property

        ''' <summary>
        ''' Whether to treat a cash account with an invalid type as an error (not warning).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property ErrorOnTypeMismatch() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ErrorOnTypeMismatch
            End Get
        End Property

        ''' <summary>
        ''' Gets a base type of the datasource (<see cref="HelperLists.CashAccountInfoList">CashAccountInfoList</see>).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DataSourceBaseType() As System.Type _
            Implements IDataSourceProvider.DataSourceBaseType
            Get
                Return GetType(HelperLists.CashAccountInfoList)
            End Get
        End Property

        ''' <summary>
        ''' Gets a property display value (TypedPropertyValue.ToString) that 
        ''' corresponds to a null value (en empty string represents a null 
        ''' CashAccountInfo for  <see cref="HelperLists.CashAccountInfoList">CashAccountInfoList</see>).
        ''' Indicates that such a value should be displayed as empty string instead.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DataSourceEmptyValueString() As String _
            Implements IDataSourceProvider.DataSourceEmptyValueString
            Get
                Return ""
            End Get
        End Property

        ''' <summary>
        ''' Gets a name of the value object property that should be assigned 
        ''' to a property (an empty string because a CashAccountInfo itself
        ''' is assigned to a property).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DataSourceValueMember() As String _
            Implements IDataSourceProvider.DataSourceValueMember
            Get
                Return ""
            End Get
        End Property


        ''' <summary>
        ''' Creates a new instance of an CashAccountFieldAttribute class.
        ''' </summary>
        ''' <param name="valueValueRequired">Whether the property value is mandatory.</param>
        ''' <param name="valueErrorOnTypeMismatch">Whether to treat account value with an invalid base class as an error (not warning).</param>
        ''' <param name="valueAcceptedTypes">A list of the allowed account base class values for the property as provided by 
        ''' <see cref="General.Account.GetAccountClass">Account.GetAccountClass</see> method.</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal valueValueRequired As ValueRequiredLevel, ByVal valueErrorOnTypeMismatch As Boolean, _
            ByVal ParamArray valueAcceptedTypes As Documents.CashAccountType())

            MyBase.new(valueValueRequired)

            _ErrorOnTypeMismatch = valueErrorOnTypeMismatch
            _AcceptedTypes = valueAcceptedTypes

            If _AcceptedTypes Is Nothing OrElse _AcceptedTypes.Length < 1 Then
                _AcceptedTypes = New Documents.CashAccountType() {}
            End If

        End Sub


        Friend Function GetValueObjectId(ByVal obj As Object, _
            ByVal prop As System.Reflection.PropertyInfo) As String _
            Implements IValueObjectIdProvider.GetValueObjectId

            If obj Is Nothing Then
                Throw New ArgumentNullException("obj")
            End If

            If prop Is Nothing Then
                Throw New ArgumentNullException("prop")
            End If

            Dim value As CashAccountInfo = Nothing
            Try
                value = DirectCast(prop.GetValue(obj, Nothing), CashAccountInfo)
            Catch ex As Exception
            End Try

            If value = CashAccountInfo.Empty Then Return ""

            Return value.GetValueObjectIdString()

        End Function

        Friend Function GetValueObjectType() As Type _
            Implements IValueObjectIdProvider.GetValueObjectType
            Return GetType(CashAccountInfoList)
        End Function


        ''' <summary>
        ''' Gets a datasource (a <see cref="FilteredBindingList(Of CashAccountInfo)">FilteredBindingList(Of CashAccountInfo)</see>) 
        ''' for a property.
        ''' </summary>
        ''' <remarks></remarks>
        Public Function GetDataSource(ByVal valueObjectIds As List(Of String)) As IList _
            Implements IDataSourceProvider.GetDataSource
            Return CashAccountInfoList.GetCachedFilteredList( _
                False, True, valueObjectIds, _AcceptedTypes)
        End Function

    End Class

End Namespace