Namespace Attributes

    ''' <summary>
    ''' Represents an attribute that is used to mark business objects' properties holding a reference
    ''' to a <see cref="HelperLists.PersonInfo">PersonInfo</see> and to store  basic business rules 
    ''' (mandatory, allowed types, etc.).
    ''' </summary>
    ''' <remarks>Used for validation purposes in <see cref="PersonValidation">PersonValidation</see> method.
    ''' Could be used by GUI to initialize appropriate controls.</remarks>
    <Serializable()> _
    <AttributeUsage(AttributeTargets.Property, AllowMultiple:=False, Inherited:=True)> _
    Public Class PersonFieldAttribute
        Inherits ValueObjectFieldAttribute
        Implements IValueObjectIdProvider, IDataSourceProvider, IValidationRuleProvider

        Private _AllowBuyers As Boolean = False
        Private _AllowSuppliers As Boolean = False
        Private _AllowWorkers As Boolean = False
        Private _ErrorOnTypeMismatch As Boolean = False

        ''' <summary>
        ''' Gets whether to accept persons that <see cref="General.Person.IsClient">are buyers (clients)</see>.
        ''' </summary>
        ''' <remarks>Defaults to FALSE.</remarks>
        Public ReadOnly Property AllowBuyers() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AllowBuyers
            End Get
        End Property

        ''' <summary>
        ''' Gets whether to accept persons that <see cref="General.Person.IsSupplier">are suppliers</see>.
        ''' </summary>
        ''' <remarks>Defaults to FALSE.</remarks>
        Public ReadOnly Property AllowSuppliers() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AllowSuppliers
            End Get
        End Property

        ''' <summary>
        ''' Gets whether to accept persons that <see cref="General.Person.IsWorker">are workers</see>.
        ''' </summary>
        ''' <remarks>Defaults to FALSE.</remarks>
        Public ReadOnly Property AllowWorkers() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AllowWorkers
            End Get
        End Property

        ''' <summary>
        ''' Gets whether to accept all persons irrespective of their basic group.
        ''' </summary>
        ''' <remarks>Returns TRUE if either all groups are allowed or all groups are disallowed.</remarks>
        Public ReadOnly Property AllowAll() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return (_AllowBuyers AndAlso _AllowSuppliers AndAlso _AllowWorkers) OrElse _
                    (Not _AllowBuyers AndAlso Not _AllowSuppliers AndAlso Not _AllowWorkers)
            End Get
        End Property

        ''' <summary>
        ''' Gets whether to treat a person with an invalid group assignment as an error (not warning).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property ErrorOnTypeMismatch() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ErrorOnTypeMismatch
            End Get
        End Property

        ''' <summary>
        ''' Gets a base type of the datasource (<see cref="HelperLists.PersonInfoList">PersonInfoList</see>).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DataSourceBaseType() As System.Type _
            Implements IDataSourceProvider.DataSourceBaseType
            Get
                Return GetType(HelperLists.PersonInfoList)
            End Get
        End Property

        ''' <summary>
        ''' Gets a property display value (TypedPropertyValue.ToString) that 
        ''' corresponds to a null value (en empty string represents a null 
        ''' PersonInfo for  <see cref="HelperLists.PersonInfoList">PersonInfoList</see>).
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
        ''' to a property (an empty string because a PersonInfo itself
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
        ''' Creates a new instance of PersonFieldAttribute.
        ''' </summary>
        ''' <param name="valueValueRequired">whether the property value is mandatory</param>
        ''' <param name="allowBuyers">whether to accept persons that <see cref="General.Person.IsClient">
        ''' are buyers (clients)</see></param>
        ''' <param name="allowSuppliers">whether to accept persons that <see cref="General.Person.IsSupplier">
        ''' are suppliers</see></param>
        ''' <param name="allowWorkers">whether to accept persons that <see cref="General.Person.IsWorker">
        ''' are workers</see></param>
        ''' <param name="errorOnTypeMismatch">whether to treat a person with an invalid group 
        ''' assignment as an error (not warning)</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal valueValueRequired As ValueRequiredLevel, Optional ByVal allowBuyers As Boolean = True, _
            Optional ByVal allowSuppliers As Boolean = True, Optional ByVal allowWorkers As Boolean = True, _
            Optional ByVal errorOnTypeMismatch As Boolean = False)

            MyBase.new(valueValueRequired)

            _AllowBuyers = allowBuyers
            _AllowSuppliers = allowSuppliers
            _AllowWorkers = allowWorkers
            _ErrorOnTypeMismatch = errorOnTypeMismatch

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

            Dim value As PersonInfo = Nothing
            Try
                value = DirectCast(prop.GetValue(obj, Nothing), PersonInfo)
            Catch ex As Exception
            End Try

            If value = PersonInfo.Empty Then Return ""

            Return value.GetValueObjectIdString()

        End Function

        Friend Function GetValueObjectType() As Type _
            Implements IValueObjectIdProvider.GetValueObjectType
            Return GetType(PersonInfoList)
        End Function


        ''' <summary>
        ''' Gets a datasource (a <see cref="FilteredBindingList(Of PersonInfo)">FilteredBindingList(Of PersonInfo)</see>) 
        ''' for a property.
        ''' </summary>
        ''' <remarks></remarks>
        Public Function GetDataSource(ByVal valueObjectIds As List(Of String)) As IList _
            Implements IDataSourceProvider.GetDataSource
            Return PersonInfoList.GetCachedFilteredList(True, False, _
                _AllowBuyers, _AllowSuppliers, _AllowWorkers, valueObjectIds)
        End Function

        ''' <summary>
        ''' Gets a concrete validation rule method to validate the property value.
        ''' </summary>
        ''' <remarks></remarks>
        Public Function GetValidationRule() As Csla.Validation.RuleHandler _
            Implements IValidationRuleProvider.GetValidationRule
            Return AddressOf CommonValidation.PersonFieldValidation
        End Function

    End Class

End Namespace