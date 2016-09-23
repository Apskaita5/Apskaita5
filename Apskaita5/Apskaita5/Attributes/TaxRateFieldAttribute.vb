Namespace Attributes

    ''' <summary>
    ''' Represents an attribute that is used to mark business objects' properties holding a reference
    ''' to a <see cref="HelperLists.TaxRateInfo">tax rate value</see> and to store  basic business rules 
    ''' (mandatory, type, etc.).
    ''' </summary>
    ''' <remarks>Could be used by GUI to initialize appropriate controls.</remarks>
    <Serializable()> _
    <AttributeUsage(AttributeTargets.Property, AllowMultiple:=False, Inherited:=True)> _
    Public Class TaxRateFieldAttribute
        Inherits DoubleFieldAttribute
        Implements IValueObjectIdProvider, IDataSourceProvider

        Private _Type As ApskaitaObjects.Settings.TaxRateType


        ''' <summary>
        ''' Gets a type of the tax.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property [Type]() As ApskaitaObjects.Settings.TaxRateType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Type
            End Get
        End Property

        ''' <summary>
        ''' Gets a base type of the datasource (<see cref="HelperLists.TaxRateInfoList">TaxRateInfoList</see>).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DataSourceBaseType() As System.Type _
            Implements IDataSourceProvider.DataSourceBaseType
            Get
                Return GetType(HelperLists.TaxRateInfoList)
            End Get
        End Property

        ''' <summary>
        ''' Gets a property display value (TypedPropertyValue.ToString) that 
        ''' corresponds to a null value (an empty string, because TaxRateInfo
        ''' underlying value is Double and cannot be null).
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
        ''' to a property (an empty string, because tax rates are provided 
        ''' as simple List(Of Double)).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DataSourceValueMember() As String _
            Implements IDataSourceProvider.DataSourceValueMember
            Get
                Return ""
            End Get
        End Property


        ''' <summary>
        ''' Creates a new instance of an TaxRateFieldAttribute class.
        ''' </summary>
        ''' <param name="valueValueRequired">whether the property value is mandatory.</param>
        ''' <param name="valueType">a type of the tax</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal valueValueRequired As ValueRequiredLevel, ByVal valueType As ApskaitaObjects.Settings.TaxRateType, _
            Optional ByVal valueMinValue As Double = 0, Optional ByVal valueMaxValue As Double = 99, _
            Optional ByVal valueErrorIfExceedsRange As Boolean = False)

            MyBase.New(valueValueRequired, False, 2, True, valueMinValue, valueMaxValue, valueErrorIfExceedsRange)

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

            Dim value As Double = 0
            Try
                value = DirectCast(prop.GetValue(obj, Nothing), Double)
            Catch ex As Exception
            End Try

            Return TaxRateInfo.GetValueObjectIdString(value, _Type)

        End Function

        Friend Function GetValueObjectType() As Type _
            Implements IValueObjectIdProvider.GetValueObjectType
            Return GetType(TaxRateInfoList)
        End Function


        ''' <summary>
        ''' Gets a datasource (a <see cref="FilteredBindingList(Of TaxRateInfo)">FilteredBindingList(Of TaxRateInfo)</see>) 
        ''' for a property.
        ''' </summary>
        ''' <remarks></remarks>
        Public Function GetDataSource(ByVal valueObjectIds As List(Of String)) As IList _
            Implements IDataSourceProvider.GetDataSource
            Return TaxRateInfoList.GetCachedFilteredList(_Type, False, valueObjectIds)
        End Function

    End Class

End Namespace