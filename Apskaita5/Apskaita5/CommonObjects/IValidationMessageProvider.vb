''' <summary>
''' Provides a generic interface for accessing business objects validation info.
''' </summary>
''' <remarks></remarks>
Public Interface IValidationMessageProvider

    ''' <summary>
    ''' Whether the object contains valid data, i.e. there are broken rules with status <see cref="csla.Validation.RuleSeverity.[Error]">Error</see>.
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    ReadOnly Property IsValid() As Boolean

    ''' <summary>
    ''' Wheather the object contains any warnings, i.e. there are broken rules with status <see cref="csla.Validation.RuleSeverity.Warning">Warning</see>.
    ''' </summary>
    ''' <remarks></remarks>
    Function HasWarnings() As Boolean

    ''' <summary>
    ''' Gets a human readable description of all the validation <see cref="csla.Validation.RuleSeverity.[Error]">errors</see>.
    ''' </summary>
    ''' <remarks></remarks>
    Function GetAllBrokenRules() As String

    ''' <summary>
    ''' Gets a human readable description of all the validation <see cref="csla.Validation.RuleSeverity.Warning">warnings</see>.
    ''' </summary>
    ''' <remarks></remarks>
    Function GetAllWarnings() As String

End Interface
