Namespace Attributes
    ''' <summary>
    ''' Describes whether a property value is mandatory, recommended or optional.
    ''' </summary>
    ''' <remarks>Used in custom property attribute classes to provide broken rule severity level.</remarks>
    Public Enum ValueRequiredLevel
        ''' <summary>
        ''' Property value is mandatory. Null or empty value is treated as <see cref="Csla.Validation.RuleSeverity.[Error]">Error</see>.
        ''' </summary>
        ''' <remarks></remarks>
        Mandatory
        ''' <summary>
        ''' Property value is recommended. Null or empty value is treated as <see cref="Csla.Validation.RuleSeverity.Warning">Warning</see>.
        ''' </summary>
        ''' <remarks></remarks>
        Recommended
        ''' <summary>
        ''' Property value is optional. Null or empty value is allowed without any warnings.
        ''' </summary>
        ''' <remarks></remarks>
        [Optional]
    End Enum
End Namespace