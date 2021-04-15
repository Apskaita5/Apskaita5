Namespace Attributes

    ''' <summary>
    ''' Represents an attribute that is used to mark business objects' properties holding a reference
    ''' to a <see cref="HelperLists.LongTermAssetCustomGroupInfo">LongTermAssetCustomGroupInfo</see> 
    ''' and to store  basic business rules (mandatory, etc.).
    ''' </summary>
    ''' <remarks>Could be used by GUI to initialize appropriate controls.</remarks>
    <Serializable()> _
    <AttributeUsage(AttributeTargets.Property, AllowMultiple:=False, Inherited:=True)> _
    Public Class LongTermAssetCustomGroupFieldAttribute
        Inherits ValueObjectFieldAttribute
        Implements IDataSourceProvider, IValidationRuleProvider

        ''' <summary>
        ''' Gets a base type of the datasource (<see cref="HelperLists.LongTermAssetCustomGroupInfoList">LongTermAssetCustomGroupInfoList</see>).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DataSourceBaseType() As System.Type _
            Implements IDataSourceProvider.DataSourceBaseType
            Get
                Return GetType(HelperLists.LongTermAssetCustomGroupInfoList)
            End Get
        End Property

        ''' <summary>
        ''' Gets a property display value (TypedPropertyValue.ToString) that 
        ''' corresponds to a null value (en empty string represents a null 
        ''' LongTermAssetCustomGroupInfo for  <see cref="HelperLists.LongTermAssetCustomGroupInfoList">LongTermAssetCustomGroupInfoList</see>).
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
        ''' to a property (an empty string because a LongTermAssetCustomGroupInfo
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
        ''' Creates a new instance of LongTermAssetCustomGroupFieldAttribute.
        ''' </summary>
        ''' <param name="valueValueRequired">whether the property value is mandatory</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal valueValueRequired As ValueRequiredLevel)

            MyBase.new(valueValueRequired)

        End Sub


        ''' <summary>
        ''' Gets a datasource (a <see cref="FilteredBindingList(Of LongTermAssetCustomGroupInfo)">FilteredBindingList(Of LongTermAssetCustomGroupInfo)</see>) 
        ''' for a property.
        ''' </summary>
        ''' <remarks></remarks>
        Public Function GetDataSource(ByVal valueObjectIds As List(Of String)) As IList _
            Implements IDataSourceProvider.GetDataSource
            Return LongTermAssetCustomGroupInfoList.GetCachedFilteredList( _
                Me.ValueRequired <> ValueRequiredLevel.Mandatory)
        End Function

        ''' <summary>
        ''' Gets a concrete validation rule method to validate the property value.
        ''' </summary>
        ''' <remarks></remarks>
        Public Function GetValidationRule() As Csla.Validation.RuleHandler _
            Implements IValidationRuleProvider.GetValidationRule
            Return AddressOf CommonValidation.ValueObjectFieldValidation
        End Function

    End Class

End Namespace
