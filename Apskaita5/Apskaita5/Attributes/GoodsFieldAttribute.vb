Namespace Attributes

    ''' <summary>
    ''' Represents an attribute that is used to mark business objects' properties holding a reference
    ''' to a <see cref="HelperLists.GoodsInfo">GoodsInfo</see> and to store  basic business rules 
    ''' (mandatory, allowed types, etc.).
    ''' </summary>
    ''' <remarks>Could be used by GUI to initialize appropriate controls.</remarks>
    <Serializable()> _
    <AttributeUsage(AttributeTargets.Property, AllowMultiple:=False, Inherited:=True)> _
    Public Class GoodsFieldAttribute
        Inherits ValueObjectFieldAttribute
        Implements IValueObjectIdProvider, IDataSourceProvider

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
        ''' Gets a base type of the datasource (<see cref="HelperLists.GoodsInfoList">GoodsInfoList</see>).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DataSourceBaseType() As System.Type _
            Implements IDataSourceProvider.DataSourceBaseType
            Get
                Return GetType(HelperLists.GoodsInfoList)
            End Get
        End Property

        ''' <summary>
        ''' Gets a property display value (TypedPropertyValue.ToString) that 
        ''' corresponds to a null value (en empty string represents a null 
        ''' GoodsInfoList for  <see cref="HelperLists.GoodsInfoList">GoodsInfoList</see>).
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
        ''' to a property (an empty string because a GoodsInfo itself
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
        ''' Creates a new instance of GoodsFieldAttribute.
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

            Dim value As GoodsInfo = Nothing
            Try
                value = DirectCast(prop.GetValue(obj, Nothing), GoodsInfo)
            Catch ex As Exception
            End Try

            If value = GoodsInfo.Empty Then Return ""

            Return value.GetValueObjectIdString()

        End Function

        Friend Function GetValueObjectType() As Type _
            Implements IValueObjectIdProvider.GetValueObjectType
            Return GetType(GoodsInfoList)
        End Function

        ''' <summary>
        ''' Gets a datasource (a <see cref="FilteredBindingList(Of GoodsInfo)">FilteredBindingList(Of GoodsInfo)</see>) 
        ''' for a property.
        ''' </summary>
        ''' <remarks></remarks>
        Public Function GetDataSource(ByVal valueObjectIds As List(Of String)) As IList _
            Implements IDataSourceProvider.GetDataSource
            Return GoodsInfoList.GetCachedFilteredList(False, True, _TradedType, valueObjectIds)
        End Function

    End Class

End Namespace