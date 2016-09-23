Namespace Attributes

    ''' <summary>
    ''' Represents an attribute that is used to mark business objects' properties holding 
    ''' an ISO 639-1 language code value and to store basic business rules (mandatory, etc.).
    ''' </summary>
    ''' <remarks>Used for validation purposes in <see cref="LanguageCodeValidation">LanguageCodeValidation</see> method.
    ''' Could be used by GUI to initialize appropriate controls.</remarks>
    <Serializable()> _
    <AttributeUsage(AttributeTargets.Property, AllowMultiple:=False, Inherited:=True)> _
    Public Class LanguageCodeFieldAttribute
        Inherits System.Attribute
        Implements IDataSourceProvider

        Private _ValueRequired As ValueRequiredLevel = ValueRequiredLevel.Optional
        Private _WithRegionalSettingsOnly As Boolean = False

        ''' <summary>
        ''' Gets whether the property value is mandatory.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property ValueRequired() As ValueRequiredLevel
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ValueRequired
            End Get
        End Property

        ''' <summary>
        ''' Gets whether only to use languages that the <see cref="General.CompanyRegionalData">regional 
        ''' settings are created for</see>.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property WithRegionalSettingsOnly() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _WithRegionalSettingsOnly
            End Get
        End Property

        ''' <summary>
        ''' Gets a base type of the datasource (<see cref="HelperLists.CompanyRegionalInfoList">CompanyRegionalInfoList</see>).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DataSourceBaseType() As System.Type _
            Implements IDataSourceProvider.DataSourceBaseType
            Get
                Return GetType(HelperLists.CompanyRegionalInfoList)
            End Get
        End Property

        ''' <summary>
        ''' Gets a property display value (TypedPropertyValue.ToString) that 
        ''' corresponds to a null value (an empty string represents a null 
        ''' language code for <see cref="HelperLists.CompanyRegionalInfo">CompanyRegionalInfoList</see>).
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
        ''' to a property (an empty string, because language codes are provided 
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
        ''' Creates a new LanguageCodeFieldAttribute instance.
        ''' </summary>
        ''' <param name="valueValueRequired">whether the property value is mandatory</param>
        ''' <param name="valueWithRegionalSettingsOnly">whether only to use languages that the 
        ''' <see cref="General.CompanyRegionalData">regional settings are created for</see></param>
        ''' <remarks></remarks>
        Public Sub New(ByVal valueValueRequired As ValueRequiredLevel, ByVal valueWithRegionalSettingsOnly As Boolean)

            _ValueRequired = valueValueRequired
            _WithRegionalSettingsOnly = valueWithRegionalSettingsOnly

        End Sub


        ''' <summary>
        ''' Gets a datasource (a <see cref="List(Of String)">List(Of String)</see>) 
        ''' for a property.
        ''' </summary>
        ''' <remarks></remarks>
        Public Function GetDataSource(ByVal valueObjectIds As List(Of String)) As IList _
            Implements IDataSourceProvider.GetDataSource
            If _WithRegionalSettingsOnly Then
                Return CompanyRegionalInfoList.GetCachedFilteredList( _
                    Me.ValueRequired <> ValueRequiredLevel.Mandatory, True)
            Else
                Return GetLanguageCodeList(Me.ValueRequired <> ValueRequiredLevel.Mandatory)
            End If
        End Function

    End Class

End Namespace