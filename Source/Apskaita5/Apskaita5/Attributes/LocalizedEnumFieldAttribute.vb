Imports System.ComponentModel

Namespace Attributes

    ''' <summary>
    ''' Represents an attribute that is used to mark <see cref="ConvertLocalizedName">
    ''' localized ENUM values</see>.
    ''' </summary>
    ''' <remarks>Could be used by GUI to initialize appropriate controls, 
    ''' see <see cref="GetLocalizedNameList">GetLocalizedNameList</see>.</remarks>
    <Serializable()> _
    <AttributeUsage(AttributeTargets.Property, AllowMultiple:=False, Inherited:=True)> _
    Public Class LocalizedEnumFieldAttribute
        Inherits BusinessFieldAttribute
        Implements IDataSourceProvider

        Private _EnumType As Type
        Private _AddNotSpecifiedItem As Boolean
        Private _NotSpecifiedItem As String
        Private _EnumValues As Object()


        ''' <summary>
        ''' Gets an underlying ENUM type.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property EnumType() As Type
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _EnumType
            End Get
        End Property

        ''' <summary>
        ''' Gets whether to add a 'not specified' item to the datasource.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property AddNotSpecifiedItem() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AddNotSpecifiedItem
            End Get
        End Property

        ''' <summary>
        ''' Gets a 'not specified' text that is displayed to the user.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property NotSpecifiedItem() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _NotSpecifiedItem
            End Get
        End Property

        ''' <summary>
        ''' Gets an array of allowed ENUM values (null or empty for all the enum values).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property EnumValues() As Object()
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _EnumValues
            End Get
        End Property

        ''' <summary>
        ''' Gets a base type of the datasource (assigned Enum type).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DataSourceBaseType() As System.Type _
            Implements IDataSourceProvider.DataSourceBaseType
            Get
                Return _EnumType
            End Get
        End Property

        ''' <summary>
        ''' Gets a property display value (TypedPropertyValue.ToString) that 
        ''' corresponds to a null value (empty string because enum value cannot be null).
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
        ''' to a property (an empty string, because enum values are provided 
        ''' as a simple List(Of String)).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DataSourceValueMember() As String _
            Implements IDataSourceProvider.DataSourceValueMember
            Get
                Return ""
            End Get
        End Property


        ''' <summary>
        ''' Creates a new instance of an LocalizedEnumFieldAttribute class.
        ''' </summary>
        ''' <param name="valueEnumType">a type of the underlying ENUM</param>
        ''' <param name="valueEnumValues">allowed ENUM values (null or empty 
        ''' for all the enum values)</param>
        ''' <param name="valueAddNotSpecifiedItem">whether to add 
        ''' a 'not specified' item to the datasource</param>
        ''' <param name="valueNotSpecifiedItem">a 'not specified' text 
        ''' that is displayed to the user</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal valueEnumType As Type, ByVal valueAddNotSpecifiedItem As Boolean, _
            ByVal valueNotSpecifiedItem As String, ByVal ParamArray valueEnumValues As Object())

            _EnumType = valueEnumType
            _EnumValues = valueEnumValues
            _AddNotSpecifiedItem = valueAddNotSpecifiedItem
            _NotSpecifiedItem = valueNotSpecifiedItem

        End Sub


        ''' <summary>
        ''' Gets a datasource (a <see cref="FilteredBindingList(Of DocumentSerialInfo)">FilteredBindingList(Of AccountInfo)</see>) 
        ''' for a property.
        ''' </summary>
        ''' <remarks></remarks>
        Public Function GetDataSource(ByVal valueObjectIds As List(Of String)) As IList _
            Implements IDataSourceProvider.GetDataSource
            Dim result As List(Of String)
            If _EnumValues Is Nothing OrElse _EnumValues.Length < 1 Then
                result = GetLocalizedNameList(_EnumType)
            Else
                result = New List(Of String)
                For Each value As [Enum] In _EnumValues
                    result.Add(ConvertLocalizedName(value))
                Next
                Return result
            End If
            If _AddNotSpecifiedItem Then
                result.Insert(0, _NotSpecifiedItem)
            End If
            Return result
        End Function

    End Class

End Namespace
