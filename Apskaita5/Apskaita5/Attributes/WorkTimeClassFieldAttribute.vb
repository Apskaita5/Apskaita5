Namespace Attributes

    ''' <summary>
    ''' Represents an attribute that is used to mark business objects' properties holding a reference
    ''' to a <see cref="HelperLists.WorkTimeClassInfo">WorkTimeClassInfo</see> and to store 
    ''' basic business rules (mandatory, allowed types, etc.).
    ''' </summary>
    ''' <remarks>Could be used by GUI to initialize appropriate controls.</remarks>
    <Serializable()> _
    <AttributeUsage(AttributeTargets.Property, AllowMultiple:=False, Inherited:=True)> _
    Public Class WorkTimeClassFieldAttribute
        Inherits ValueObjectFieldAttribute
        Implements IDataSourceProvider

        Private _ShowWithHours As Boolean = False
        Private _ShowWithoutHours As Boolean = False


        ''' <summary>
        ''' Gets whether to include work time classes with hours.
        ''' </summary>
        ''' <remarks>Defaults to <see cref="Documents.TradedItemType.All">TradedItemType.All</see>.</remarks>
        Public ReadOnly Property ShowWithHours() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ShowWithHours
            End Get
        End Property

        ''' <summary>
        ''' Gets whether to include work time classes without hours.
        ''' </summary>
        ''' <remarks>Defaults to <see cref="Documents.TradedItemType.All">TradedItemType.All</see>.</remarks>
        Public ReadOnly Property ShowWithoutHours() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ShowWithoutHours
            End Get
        End Property

        ''' <summary>
        ''' Gets a base type of the datasource (<see cref="HelperLists.WorkTimeClassInfoList">WorkTimeClassInfoList</see>).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DataSourceBaseType() As System.Type _
            Implements IDataSourceProvider.DataSourceBaseType
            Get
                Return GetType(HelperLists.WorkTimeClassInfoList)
            End Get
        End Property

        ''' <summary>
        ''' Gets a property display value (TypedPropertyValue.ToString) that 
        ''' corresponds to a null value (en empty string represents a null 
        ''' WorkTimeClassInfo for  <see cref="HelperLists.WorkTimeClassInfoList">WorkTimeClassInfoList</see>).
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
        ''' to a property (an empty string because a WorkTimeClassInfo itself
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
        ''' Creates a new instance of WorkTimeClassFieldAttribute.
        ''' </summary>
        ''' <param name="valueValueRequired">whether the property value is mandatory</param>
        ''' <param name="valueShowWithHours">whether to include work time classes with hours</param>
        ''' <param name="valueShowWithoutHours">whether to include work time classes without hours</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal valueValueRequired As ValueRequiredLevel, ByVal valueShowWithHours As Boolean, _
            ByVal valueShowWithoutHours As Boolean)

            MyBase.new(valueValueRequired)

            _ShowWithHours = valueShowWithHours
            _ShowWithoutHours = valueShowWithoutHours

        End Sub


        ''' <summary>
        ''' Gets a datasource (a <see cref="FilteredBindingList(Of WorkTimeClassInfo)">FilteredBindingList(Of WorkTimeClassInfo)</see>) 
        ''' for a property.
        ''' </summary>
        ''' <remarks></remarks>
        Public Function GetDataSource(ByVal valueObjectIds As List(Of String)) As IList _
            Implements IDataSourceProvider.GetDataSource
            Return WorkTimeClassInfoList.GetCachedFilteredList( _
                True, _ShowWithoutHours, _ShowWithHours)
        End Function

    End Class

End Namespace