''' <summary>
''' Represents a common interface to validate business objects' dates where there are 
''' chronology related business rules.
''' </summary>
''' <remarks></remarks>
Public Interface IChronologicValidator

    ''' <summary>
    ''' Gets an ID of the validated (parent) object. 
    ''' </summary>
    ''' <remarks></remarks>
    ReadOnly Property CurrentOperationID() As Integer

    ''' <summary>
    ''' Gets the current date of the validated (parent) object (<see cref="Today">Today</see> for a new object). 
    ''' </summary>
    ''' <remarks></remarks>
    ReadOnly Property CurrentOperationDate() As Date

    ''' <summary>
    ''' Gets the human readable name of the validated (parent) object. 
    ''' </summary>
    ''' <remarks></remarks>
    ReadOnly Property CurrentOperationName() As String

    ''' <summary>
    ''' Wheather the financial data of the validated (parent) object is allowed to be changed.
    ''' </summary>
    ''' <remarks></remarks>
    ReadOnly Property FinancialDataCanChange() As Boolean

    ''' <summary>
    ''' Wheather the financial data of the validated (parent) object is allowed 
    ''' to be changed by it's parent document.
    ''' </summary>
    ''' <remarks></remarks>
    ReadOnly Property ParentFinancialDataCanChange() As Boolean

    ''' <summary>
    ''' Wheather there is a minimum allowed date for the validated (parent) object.
    ''' </summary>
    ''' <remarks></remarks>
    ReadOnly Property MinDateApplicable() As Boolean

    ''' <summary>
    ''' Wheather there is a maximum allowed date for the validated (parent) object.
    ''' </summary>
    ''' <remarks></remarks>
    ReadOnly Property MaxDateApplicable() As Boolean

    ''' <summary>
    ''' Gets a minimum allowed date for the validated (parent) object.
    ''' </summary>
    ''' <remarks></remarks>
    ReadOnly Property MinDate() As Date

    ''' <summary>
    ''' Gets a maximum allowed date for the validated (parent) object.
    ''' </summary>
    ''' <remarks></remarks>
    ReadOnly Property MaxDate() As Date

    ''' <summary>
    ''' Gets a human readable explanation of why the financial data of the validated object 
    ''' is NOT allowed to be changed.
    ''' </summary>
    ''' <remarks></remarks>
    ReadOnly Property FinancialDataCanChangeExplanation() As String

    ''' <summary>
    ''' Gets a human readable explanation of why the financial data of the validated object 
    ''' is NOT allowed to be changed by it's parent document.
    ''' </summary>
    ''' <remarks></remarks>
    ReadOnly Property ParentFinancialDataCanChangeExplanation() As String

    ''' <summary>
    ''' Gets a human readable explanation of why there is a minimum allowed date 
    ''' for the validated (parent) object.
    ''' </summary>
    ''' <remarks></remarks>
    ReadOnly Property MinDateExplanation() As String

    ''' <summary>
    ''' Gets a human readable explanation of why there is a maximum allowed date 
    ''' for the validated (parent) object.
    ''' </summary>
    ''' <remarks></remarks>
    ReadOnly Property MaxDateExplanation() As String

    ''' <summary>
    ''' A human readable explanation of all the applicable business rules restrains.
    ''' </summary>
    ReadOnly Property LimitsExplanation() As String

    ''' <summary>
    ''' A human readable explanation of the applicable business rules restrains.
    ''' </summary>
    ''' <remarks>More exhaustive than <see cref="LimitsExplanation">LimitsExplanation</see>.</remarks>
    ReadOnly Property BackgroundExplanation() As String

    ''' <summary>
    ''' Validates the object's date, used in <see cref="ChronologyValidation">ChronologyValidation</see> method.
    ''' </summary>
    ''' <param name="OperationDate">The object's date to validate.</param>
    ''' <param name="ErrorMessage">Returned error/warning message corresponds to <see cref="Csla.Validation.RuleArgs.Description">RuleArgs.Description</see> property.</param>
    ''' <param name="errorSeverity">Returned error severity (error/warning) corresponds to <see cref="Csla.Validation.RuleArgs.Severity">RuleArgs.Severity</see> property.</param>
    ''' <returns>Returnes TRUE if there are no errors or warnings, otherwise - FALSE.</returns>
    ''' <remarks></remarks>
    Function ValidateOperationDate(ByVal operationDate As Date, ByRef errorMessage As String, _
        ByRef errorSeverity As Csla.Validation.RuleSeverity) As Boolean

End Interface
