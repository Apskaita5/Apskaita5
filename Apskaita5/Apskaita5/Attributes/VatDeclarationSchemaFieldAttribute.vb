Namespace Attributes

    ''' <summary>
    ''' Represents an attribute that is used to mark business objects' properties holding a reference
    ''' to a <see cref="HelperLists.VatDeclarationSchemaInfo">VatDeclarationSchemaInfo</see> 
    ''' and to store  basic business rules (mandatory, allowed types, etc.).
    ''' </summary>
    ''' <remarks>Could be used by GUI to initialize appropriate controls.</remarks>
    <Serializable()> _
    <AttributeUsage(AttributeTargets.Property, AllowMultiple:=False, Inherited:=True)> _
    Public Class VatDeclarationSchemaFieldAttribute
        Inherits ValueObjectFieldAttribute
        Implements IValueObjectIdProvider, IDataSourceProvider, IValidationRuleProvider

        Private _TradedType As Documents.TradedItemType = Documents.TradedItemType.All


        ''' <summary>
        ''' Gets a trade type to filter by.
        ''' </summary>
        ''' <remarks>Defaults to <see cref="Documents.TradedItemType.All">TradedItemType.All</see>.</remarks>
        Public ReadOnly Property TradedType() As Documents.TradedItemType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _TradedType
            End Get
        End Property

        ''' <summary>
        ''' Gets a base type of the datasource (<see cref="HelperLists.VatDeclarationSchemaInfoList">VatDeclarationSchemaInfoList</see>).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DataSourceBaseType() As System.Type _
            Implements IDataSourceProvider.DataSourceBaseType
            Get
                Return GetType(HelperLists.VatDeclarationSchemaInfoList)
            End Get
        End Property

        ''' <summary>
        ''' Gets a property display value (TypedPropertyValue.ToString) that 
        ''' corresponds to a null value (en empty string represents a null 
        ''' VatDeclarationSchemaInfo for  <see cref="HelperLists.VatDeclarationSchemaInfoList">VatDeclarationSchemaInfoList</see>).
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
        ''' to a property (an empty string because a VatDeclarationSchemaInfo
        ''' itself is assigned to a property).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DataSourceValueMember() As String _
            Implements IDataSourceProvider.DataSourceValueMember
            Get
                Return ""
            End Get
        End Property


        ''' <summary>
        ''' Creates a new instance of VatDeclarationSchemaFieldAttribute.
        ''' </summary>
        ''' <param name="valueValueRequired">whether the property value is mandatory</param>
        ''' <param name="filterTradedType">a trade type to filter by</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal valueValueRequired As ValueRequiredLevel, Optional ByVal filterTradedType As Documents.TradedItemType = Documents.TradedItemType.All)

            MyBase.new(valueValueRequired)

            _TradedType = filterTradedType

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

            Dim value As VatDeclarationSchemaInfo = Nothing
            Try
                value = DirectCast(prop.GetValue(obj, Nothing), VatDeclarationSchemaInfo)
            Catch ex As Exception
            End Try

            If value = VatDeclarationSchemaInfo.Empty Then Return ""

            Return value.GetValueObjectIdString()

        End Function

        Friend Function GetValueObjectType() As Type _
            Implements IValueObjectIdProvider.GetValueObjectType
            Return GetType(VatDeclarationSchemaInfoList)
        End Function

        ''' <summary>
        ''' Gets a datasource (a <see cref="FilteredBindingList(Of VatDeclarationSchemaInfo)">FilteredBindingList(Of VatDeclarationSchemaInfo)</see>) 
        ''' for a property.
        ''' </summary>
        ''' <remarks></remarks>
        Public Function GetDataSource(ByVal valueObjectIds As List(Of String)) As IList _
            Implements IDataSourceProvider.GetDataSource
            Return VatDeclarationSchemaInfoList.GetCachedFilteredList( _
                Me.ValueRequired <> ValueRequiredLevel.Mandatory, False, _
                _TradedType, valueObjectIds)
        End Function

        ''' <summary>
        ''' Gets a concrete validation rule method to validate the property value.
        ''' </summary>
        ''' <remarks></remarks>
        Public Function GetValidationRule() As Csla.Validation.RuleHandler _
            Implements IValidationRuleProvider.GetValidationRule
            Return AddressOf CommonValidation.VatDeclarationSchemaFieldValidation
        End Function

    End Class

End Namespace