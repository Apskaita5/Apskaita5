Namespace Attributes

    ''' <summary>
    ''' Represents an attribute that is used to mark business objects' properties holding a 
    ''' <see cref="ApskaitaObjects.Settings.DocumentSerial.Serial">document serial</see> 
    ''' and to store basic business rules (mandatory, type, etc.).
    ''' </summary>
    ''' <remarks>Could be used by GUI to initialize appropriate controls.</remarks>
    <Serializable()> _
    <AttributeUsage(AttributeTargets.Property, AllowMultiple:=False, Inherited:=True)> _
    Public Class DocumentSerialFieldAttribute
        Inherits StringFieldAttribute
        Implements IDataSourceProvider

        Private _Type As ApskaitaObjects.Settings.DocumentSerialType

        ''' <summary>
        ''' Gets a type of the serial document.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property [Type]() As ApskaitaObjects.Settings.DocumentSerialType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Type
            End Get
        End Property

        ''' <summary>
        ''' Gets a base type of the datasource (<see cref="HelperLists.DocumentSerialInfoList">DocumentSerialInfoList</see>).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DataSourceBaseType() As System.Type _
            Implements IDataSourceProvider.DataSourceBaseType
            Get
                Return GetType(HelperLists.DocumentSerialInfoList)
            End Get
        End Property

        ''' <summary>
        ''' Gets a property display value (TypedPropertyValue.ToString) that 
        ''' corresponds to a null value (an empty string represents a null 
        ''' serial for <see cref="HelperLists.DocumentSerialInfo">DocumentSerialInfo</see>).
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
        ''' to a property (<see cref="HelperLists.DocumentSerialInfo.Serial">Serial</see>).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DataSourceValueMember() As String _
            Implements IDataSourceProvider.DataSourceValueMember
            Get
                Return "Serial"
            End Get
        End Property


        ''' <summary>
        ''' Creates a new instance of an DocumentSerialFieldAttribute class.
        ''' </summary>
        ''' <param name="valueValueRequired">whether the property value is mandatory.</param>
        ''' <param name="valueType">a type of the serial document</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal valueValueRequired As ValueRequiredLevel, ByVal valueType As ApskaitaObjects.Settings.DocumentSerialType)

            MyBase.New(valueValueRequired, 50, False)
            _Type = valueType

        End Sub


        ''' <summary>
        ''' Gets a datasource (a <see cref="FilteredBindingList(Of DocumentSerialInfo)">FilteredBindingList(Of AccountInfo)</see>) 
        ''' for a property.
        ''' </summary>
        ''' <remarks></remarks>
        Public Function GetDataSource(ByVal valueObjectIds As List(Of String)) As IList _
            Implements IDataSourceProvider.GetDataSource
            Return DocumentSerialInfoList.GetCachedFilteredList( _
                Me.ValueRequired <> ValueRequiredLevel.Mandatory, _Type)
        End Function

    End Class

End Namespace