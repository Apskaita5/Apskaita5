Namespace Attributes

    ''' <summary>
    ''' Represents an attribute that is used to mark business objects' properties holding a reference
    ''' to a <see cref="HelperLists.SharesClassInfo">SharesClassInfo</see> and to store basic business rules 
    ''' (mandatory, allowed types, etc.).
    ''' </summary>
    ''' <remarks>Used for validation purposes in <see cref="ValueObjectFieldValidation">ValueObjectFieldValidation</see> method.
    ''' Could be used by GUI to initialize appropriate controls.</remarks>
    <Serializable()>
    <AttributeUsage(AttributeTargets.Property, AllowMultiple:=False, Inherited:=True)>
    Public Class SharesClassFieldAttribute
        Inherits ValueObjectFieldAttribute
        Implements IValueObjectIdProvider, IDataSourceProvider, IValidationRuleProvider

        ''' <summary>
        ''' Gets a base type of the datasource (<see cref="HelperLists.SharesClassInfoList">SharesClassInfoList</see>).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DataSourceBaseType() As System.Type _
            Implements IDataSourceProvider.DataSourceBaseType
            Get
                Return GetType(HelperLists.SharesClassInfoList)
            End Get
        End Property

        ''' <summary>
        ''' Gets a property display value (TypedPropertyValue.ToString) that 
        ''' corresponds to a null value (en empty string represents a null 
        ''' SharesClass for  <see cref="HelperLists.SharesClassInfoList">SharesClassInfoList</see>).
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
        ''' to a property (an empty string because a SharesClassInfo itself
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
        ''' Creates a new instance of an SharesClassFieldAttribute class.
        ''' </summary>
        ''' <param name="valueValueRequired">Whether the property value is mandatory.</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal valueValueRequired As ValueRequiredLevel)

            MyBase.New(valueValueRequired)

        End Sub


        Friend Function GetValueObjectId(ByVal obj As Object,
            ByVal prop As System.Reflection.PropertyInfo) As String _
            Implements IValueObjectIdProvider.GetValueObjectId

            If obj Is Nothing Then
                Throw New ArgumentNullException("obj")
            End If

            If prop Is Nothing Then
                Throw New ArgumentNullException("prop")
            End If

            Dim value As SharesClassInfo = Nothing
            Try
                value = DirectCast(prop.GetValue(obj, Nothing), SharesClassInfo)
            Catch ex As Exception
            End Try

            If value = SharesClassInfo.Empty Then Return ""

            Return value.ID.ToString(System.Globalization.CultureInfo.InvariantCulture)

        End Function

        Friend Function GetValueObjectType() As Type _
            Implements IValueObjectIdProvider.GetValueObjectType
            Return GetType(SharesClassInfo)
        End Function


        ''' <summary>
        ''' Gets a datasource (a <see cref="FilteredBindingList(Of SharesClassInfo)">FilteredBindingList(Of SharesClassInfo)</see>) 
        ''' for a property.
        ''' </summary>
        ''' <remarks></remarks>
        Public Function GetDataSource(ByVal valueObjectIds As List(Of String)) As IList _
            Implements IDataSourceProvider.GetDataSource
            Return SharesClassInfoList.GetCachedFilteredList(False)
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
