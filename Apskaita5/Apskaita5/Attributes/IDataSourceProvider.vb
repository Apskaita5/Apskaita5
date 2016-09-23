Namespace Attributes

    ''' <summary>
    ''' An interface that provides a data source for a property values
    ''' when a property value is ment to choose from a list.
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface IDataSourceProvider

        ''' <summary>
        ''' Gets a base type of the datasource (list of some value objects).
        ''' </summary>
        ''' <remarks>A Datasource mostly is a <see cref="Csla.FilteredBindingList(Of T)">
        ''' FilteredBindingList(Of T)</see> that encapsulates a value object list
        ''' (e.g. <see cref="HelperLists.PersonInfoList">PersonInfoList</see>).
        ''' However in some cases a datasource could be a list of simple values
        ''' (e.g. a List(Of String) for <see cref="HelperLists.DocumentSerialInfoList">
        ''' DocumentSerialInfoList</see>). A base type is the underlying 
        ''' value object list type.</remarks>
        ReadOnly Property DataSourceBaseType() As Type

        ''' <summary>
        ''' Gets a name of the value object property that should be assigned to a property.
        ''' </summary>
        ''' <remarks>A Datasource mostly is a <see cref="Csla.FilteredBindingList(Of T)">
        ''' FilteredBindingList(Of T)</see> that encapsulates a value object list
        ''' (e.g. <see cref="HelperLists.PersonInfoList">PersonInfoList</see>).
        ''' However in some cases a datasource could be a list of simple values
        ''' (e.g. a List(Of String) for <see cref="HelperLists.DocumentSerialInfoList">
        ''' DocumentSerialInfoList</see>). A value member is a property of 
        ''' an underlying value object which value is assigned to the property.
        ''' If a value object itself is assigned to the property value,
        ''' an empty string is specified.</remarks>
        ReadOnly Property DataSourceValueMember() As String

        ''' <summary>
        ''' Gets a property display value (TypedPropertyValue.ToString) that 
        ''' corresponds to a null value (e.g. '0' in case of 
        ''' <see cref="HelperLists.AccountInfoList">AccountInfoList</see>).
        ''' Indicates that such a value should be displayed as empty string instead.
        ''' </summary>
        ''' <remarks></remarks>
        ReadOnly Property DataSourceEmptyValueString() As String


        ''' <summary>
        ''' Gets a datasource (a list of some value objects) for a property.
        ''' </summary>
        ''' <remarks>A Datasource mostly is a <see cref="Csla.FilteredBindingList(Of T)">
        ''' FilteredBindingList(Of T)</see> that encapsulates a value object list
        ''' (e.g. <see cref="HelperLists.PersonInfoList">PersonInfoList</see>).
        ''' However in some cases a datasource could be a list of simple values
        ''' (e.g. a List(Of String) for <see cref="HelperLists.DocumentSerialInfoList">
        ''' DocumentSerialInfoList</see>).</remarks>
        Function GetDataSource(ByVal valueObjectIds As List(Of String)) As IList

    End Interface

End Namespace