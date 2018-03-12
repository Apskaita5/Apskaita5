Namespace Attributes

    ''' <summary>
    ''' Represents an attribute that is used to mark business objects' properties holding a reference
    ''' to the <see cref="General.Account.ID">Account.ID</see> property and to store basic business rules
    ''' (mandatory, allowed values, etc.).
    ''' </summary>
    ''' <remarks>Used for validation purposes in <see cref="AccountFieldValidation">AccountFieldValidation</see>
    ''' method.
    ''' Could be used by GUI to initialize appropriate controls.</remarks>
    <Serializable()> _
    <AttributeUsage(AttributeTargets.Property, AllowMultiple:=False, Inherited:=True)> _
Public Class AccountFieldAttribute
        Inherits BusinessFieldAttribute
        Implements IDataSourceProvider, IValidationRuleProvider

        ''' <summary>
        ''' A list of all the possible account base class values as provided by 
        ''' <see cref="General.Account.GetAccountClass">Account.GetAccountClass</see> method.
        ''' </summary>
        ''' <remarks></remarks>
        Private ReadOnly AllAccountClasses As Integer() = New Integer() {1, 2, 3, 4, 5, 6}

        Private _ValueRequired As ValueRequiredLevel = ValueRequiredLevel.Optional
        Private _AcceptedClasses As Integer() = AllAccountClasses
        Private _ErrorOnClassMismatch As Boolean = False

        ''' <summary>
        ''' Whether the property value is mandatory.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property ValueRequired() As ValueRequiredLevel
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ValueRequired
            End Get
        End Property

        ''' <summary>
        ''' A list of the allowed account base class values for the property as provided by 
        ''' <see cref="General.Account.GetAccountClass">Account.GetAccountClass</see> method.
        ''' </summary>
        ''' <remarks>Defaults to <see cref="AllAccountClasses">all the possible account base class values.</see></remarks>
        Public ReadOnly Property AcceptedClasses() As Integer()
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If _AcceptedClasses Is Nothing OrElse _AcceptedClasses.Length < 1 Then
                    _AcceptedClasses = AllAccountClasses
                End If
                Return _AcceptedClasses
            End Get
        End Property

        ''' <summary>
        ''' Whether to treat account value with an invalid base class as an error (not warning).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property ErrorOnClassMismatch() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ErrorOnClassMismatch
            End Get
        End Property

        ''' <summary>
        ''' Gets a base type of the datasource (<see cref="HelperLists.AccountInfoList">AccountInfoList</see>).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DataSourceBaseType() As System.Type _
            Implements IDataSourceProvider.DataSourceBaseType
            Get
                Return GetType(HelperLists.AccountInfoList)
            End Get
        End Property

        ''' <summary>
        ''' Gets a property display value (TypedPropertyValue.ToString) that 
        ''' corresponds to a null value ('0' represents a null account for 
        ''' <see cref="HelperLists.AccountInfoList">AccountInfoList</see>).
        ''' Indicates that such a value should be displayed as empty string instead.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DataSourceEmptyValueString() As String _
            Implements IDataSourceProvider.DataSourceEmptyValueString
            Get
                Return "0"
            End Get
        End Property

        ''' <summary>
        ''' Gets a name of the value object property that should be assigned 
        ''' to a property (<see cref="HelperLists.AccountInfo.ID">ID</see>).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DataSourceValueMember() As String _
            Implements IDataSourceProvider.DataSourceValueMember
            Get
                Return "ID"
            End Get
        End Property


        ''' <summary>
        ''' Creates a new instance of an AccountFieldAttribute class.
        ''' </summary>
        ''' <param name="valueValueRequired">Whether the property value is mandatory.</param>
        ''' <param name="valueErrorOnClassMismatch">Whether to treat account value with an invalid base class as an error (not warning).</param>
        ''' <param name="valueAcceptedClasses">A list of the allowed account base class values for the property as provided by 
        ''' <see cref="General.Account.GetAccountClass">Account.GetAccountClass</see> method.</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal valueValueRequired As ValueRequiredLevel, ByVal valueErrorOnClassMismatch As Boolean, _
            ByVal ParamArray valueAcceptedClasses As Integer())

            _ValueRequired = valueValueRequired
            _ErrorOnClassMismatch = valueErrorOnClassMismatch
            _AcceptedClasses = valueAcceptedClasses

            If _AcceptedClasses Is Nothing OrElse _AcceptedClasses.Length < 1 Then
                _AcceptedClasses = AllAccountClasses
            End If

        End Sub


        ''' <summary>
        ''' Gets <see cref="AcceptedClasses">a list of AcceptedClasses</see> formated as comma separated string.
        ''' </summary>
        Public Function GetAcceptedClassesString() As String

            If _AcceptedClasses Is Nothing OrElse _AcceptedClasses.Length < 1 Then
                _AcceptedClasses = AllAccountClasses
            End If

            Dim result As New List(Of String)

            For Each n As Integer In _AcceptedClasses
                result.Add(n.ToString)
            Next

            Return String.Join(", ", result.ToArray)

        End Function

        ''' <summary>
        ''' Gets a datasource (a <see cref="FilteredBindingList(Of AccountInfo)">FilteredBindingList(Of AccountInfo)</see>) 
        ''' for a property.
        ''' </summary>
        ''' <remarks></remarks>
        Public Function GetDataSource(ByVal valueObjectIds As List(Of String)) As IList _
            Implements IDataSourceProvider.GetDataSource
            Return AccountInfoList.GetCachedFilteredList(True, _AcceptedClasses)
        End Function

        ''' <summary>
        ''' Gets a concrete validation rule method to validate the property value.
        ''' </summary>
        ''' <remarks></remarks>
        Public Function GetValidationRule() As Csla.Validation.RuleHandler _
            Implements IValidationRuleProvider.GetValidationRule
            Return AddressOf CommonValidation.AccountFieldValidation
        End Function

    End Class

End Namespace