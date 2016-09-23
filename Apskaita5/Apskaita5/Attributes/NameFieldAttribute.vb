Namespace Attributes

    ''' <summary>
    ''' Represents an attribute that is used to mark business objects' properties holding a reference
    ''' to a <see cref="HelperLists.NameInfo">name string</see> and to store  basic business rules 
    ''' (mandatory, type, etc.).
    ''' </summary>
    ''' <remarks>Could be used by GUI to initialize appropriate controls.</remarks>
    <Serializable()> _
    <AttributeUsage(AttributeTargets.Property, AllowMultiple:=False, Inherited:=True)> _
    Public Class NameFieldAttribute
        Inherits StringFieldAttribute
        Implements IValueObjectIdProvider, IDataSourceProvider

        Private _Type As ApskaitaObjects.Settings.NameType


        ''' <summary>
        ''' Gets a type of the name.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property [Type]() As ApskaitaObjects.Settings.NameType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Type
            End Get
        End Property

        ''' <summary>
        ''' Gets a base type of the datasource (<see cref="HelperLists.NameInfoList">NameInfoList</see>).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DataSourceBaseType() As System.Type _
            Implements IDataSourceProvider.DataSourceBaseType
            Get
                Return GetType(HelperLists.NameInfoList)
            End Get
        End Property

        ''' <summary>
        ''' Gets a property display value (TypedPropertyValue.ToString) that 
        ''' corresponds to a null value (an empty string represents a null 
        ''' NameInfo for <see cref="HelperLists.NameInfoList">NameInfoList</see>).
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
        ''' to a property (an empty string, because names are provided 
        ''' as simple List(Of String)).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DataSourceValueMember() As String _
            Implements IDataSourceProvider.DataSourceValueMember
            Get
                Return ""
            End Get
        End Property


        ''' <summary>
        ''' Creates a new instance of an NameFieldAttribute class.
        ''' </summary>
        ''' <param name="valueValueRequired">whether the property value is mandatory.</param>
        ''' <param name="valueType">a type of the name</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal valueValueRequired As ValueRequiredLevel, ByVal valueType As ApskaitaObjects.Settings.NameType)

            MyBase.new(valueValueRequired, 255, False)

            _Type = valueType

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

            Dim value As String = ""
            Try
                value = DirectCast(prop.GetValue(obj, Nothing), String)
            Catch ex As Exception
            End Try

            Return NameInfo.GetValueObjectIdString(value, _Type)

        End Function

        Friend Function GetValueObjectType() As Type _
            Implements IValueObjectIdProvider.GetValueObjectType
            Return GetType(NameInfoList)
        End Function


        ''' <summary>
        ''' Gets a datasource (a <see cref="List(Of String)">List(Of String)</see>) 
        ''' for a property.
        ''' </summary>
        ''' <remarks></remarks>
        Public Function GetDataSource(ByVal valueObjectIds As List(Of String)) As IList _
            Implements IDataSourceProvider.GetDataSource
            Return NameInfoList.GetCachedFilteredList(_Type, _
                Me.ValueRequired <> ValueRequiredLevel.Mandatory, False, _
                valueObjectIds)
        End Function

    End Class

End Namespace