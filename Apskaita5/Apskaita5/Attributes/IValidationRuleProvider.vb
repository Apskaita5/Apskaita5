Namespace Attributes

    ''' <summary>
    ''' Implement this interface to use <see cref="CommonValidation.BusinessAttributeValidation">BusinessAttributeValidation</see>
    ''' rule (a generalized way to choose appropriate validation rule).
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface IValidationRuleProvider

        ''' <summary>
        ''' Gets a concrete validation rule method to validate the property value.
        ''' </summary>
        ''' <remarks></remarks>
        Function GetValidationRule() As Csla.Validation.RuleHandler

    End Interface

End Namespace