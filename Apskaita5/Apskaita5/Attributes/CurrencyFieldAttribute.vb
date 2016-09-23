Namespace Attributes

    ''' <summary>
    ''' Represents an attribute that is used to mark business objects' properties holding 
    ''' an ISO 4217 currency code value and to store basic business rules (mandatory, etc.).
    ''' </summary>
    ''' <remarks>Used for validation purposes in <see cref="CurrencyFieldValidation">CurrencyFieldValidation</see> method.
    ''' Could be used by GUI to initialize appropriate controls.</remarks>
    <Serializable()> _
    <AttributeUsage(AttributeTargets.Property, AllowMultiple:=False, Inherited:=True)> _
    Public Class CurrencyFieldAttribute
        Inherits System.Attribute
        Implements IDataSourceProvider

        Private _ValueRequired As ValueRequiredLevel = ValueRequiredLevel.Optional


        ''' <summary>
        ''' Whether the property value is mandatory (not null, not empty and not blank spaces only).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property ValueRequired() As ValueRequiredLevel
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ValueRequired
            End Get
        End Property

        ''' <summary>
        ''' Gets a base type of the datasource (<see cref="List(Of String)">List(Of String)</see>).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DataSourceBaseType() As System.Type _
            Implements IDataSourceProvider.DataSourceBaseType
            Get
                Return GetType(List(Of String))
            End Get
        End Property

        ''' <summary>
        ''' Gets a property display value (TypedPropertyValue.ToString) that 
        ''' corresponds to a null value (an empty string represents a null 
        ''' currency code). Indicates that such a value should be displayed 
        ''' as empty string instead.
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
        ''' to a property (an empty string, because currency code is a simple string).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DataSourceValueMember() As String _
            Implements IDataSourceProvider.DataSourceValueMember
            Get
                Return ""
            End Get
        End Property


        ''' <summary>
        ''' Creates a new CurrencyFieldAttribute instance.
        ''' </summary>
        ''' <param name="valueValueRequired">whether the property value is mandatory</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal valueValueRequired As ValueRequiredLevel)

            _ValueRequired = valueValueRequired

        End Sub


        ''' <summary>
        ''' Gets a datasource (a <see cref="List(Of String)">List(Of String)</see>) 
        ''' for a property.
        ''' </summary>
        ''' <remarks></remarks>
        Public Function GetDataSource(ByVal valueObjectIds As List(Of String)) As IList _
            Implements IDataSourceProvider.GetDataSource
            Dim result As New List(Of String)(AccCommon.CurrencyCodes)
            If Me.ValueRequired <> ValueRequiredLevel.Mandatory Then
                result.Insert(0, "")
            End If
            Return result
        End Function

    End Class

End Namespace