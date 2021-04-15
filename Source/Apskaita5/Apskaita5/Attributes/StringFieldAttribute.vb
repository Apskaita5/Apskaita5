Namespace Attributes

    ''' <summary>
    ''' Represents an attribute that is used to mark business objects' properties holding a string value
    ''' and to store basic business rules (mandatory, max length, etc.).
    ''' </summary>
    ''' <remarks>Used for validation purposes in <see cref="StringFieldValidation">StringFieldValidation</see>
    ''' method.
    ''' Could be used by GUI to initialize appropriate controls.</remarks>
    <Serializable()> _
    <AttributeUsage(AttributeTargets.Property, AllowMultiple:=False, Inherited:=True)> _
Public Class StringFieldAttribute
        Inherits BusinessFieldAttribute
        Implements IValidationRuleProvider

        Private _ValueRequired As ValueRequiredLevel = ValueRequiredLevel.Optional
        Private _MaxLength As Integer = 255
        Private _ErrorIfExceedsMaxLength As Boolean = False

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
        ''' Maximum allowed length of a string value (after TRIM).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property MaxLength() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _MaxLength
            End Get
        End Property

        ''' <summary>
        ''' Whether to treat the property value, that excees <see cref="MaxLength">MaxLength</see> as an error (not warning).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property ErrorIfExceedsMaxLength() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ErrorIfExceedsMaxLength
            End Get
        End Property


        ''' <summary>
        ''' Creates a new StringFieldAttribute instance.
        ''' </summary>
        ''' <param name="valueValueRequired">Whether the property value is mandatory (not null, not empty and not blank spaces only).</param>
        ''' <param name="valueMaxLength">Maximum allowed length of a string value (after TRIM).</param>
        ''' <param name="valueErrorIfExceedsMaxLength">Whether to treat the property value, that excees <paramref name="valueMaxLength">valueMaxLength</paramref> as an error (not warning).</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal valueValueRequired As ValueRequiredLevel, ByVal valueMaxLength As Integer, _
                       Optional ByVal valueErrorIfExceedsMaxLength As Boolean = False)

            _ValueRequired = valueValueRequired
            _MaxLength = valueMaxLength
            _ErrorIfExceedsMaxLength = valueErrorIfExceedsMaxLength

        End Sub


        ''' <summary>
        ''' Gets a concrete validation rule method to validate the property value.
        ''' </summary>
        ''' <remarks></remarks>
        Public Function GetValidationRule() As Csla.Validation.RuleHandler _
            Implements IValidationRuleProvider.GetValidationRule
            Return AddressOf CommonValidation.StringFieldValidation
        End Function

    End Class

End Namespace
