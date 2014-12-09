Public Interface IChronologicValidator

    ReadOnly Property CurrentOperationID() As Integer

    ReadOnly Property CurrentOperationDate() As Date

    ReadOnly Property CurrentOperationName() As String

    ReadOnly Property FinancialDataCanChange() As Boolean

    ReadOnly Property ParentFinancialDataCanChange() As Boolean

    ReadOnly Property MinDateApplicable() As Boolean

    ReadOnly Property MaxDateApplicable() As Boolean

    ReadOnly Property MinDate() As Date

    ReadOnly Property MaxDate() As Date

    ReadOnly Property FinancialDataCanChangeExplanation() As String

    ReadOnly Property ParentFinancialDataCanChangeExplanation() As String

    ReadOnly Property MinDateExplanation() As String

    ReadOnly Property MaxDateExplanation() As String

    ReadOnly Property LimitsExplanation() As String

    ReadOnly Property BackgroundExplanation() As String


    Function ValidateOperationDate(ByVal OperationDate As Date, ByRef ErrorMessage As String, _
        ByRef ErrorSeverity As Csla.Validation.RuleSeverity) As Boolean

End Interface
