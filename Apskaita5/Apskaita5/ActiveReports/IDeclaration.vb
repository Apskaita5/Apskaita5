Namespace ActiveReports

    Public Interface IDeclaration

        ReadOnly Property Name() As String
        ReadOnly Property ValidFrom() As Date
        ReadOnly Property ValidTo() As Date
        ReadOnly Property Warnings() As String

        Function GetFfDataDataSet(ByVal declarationDataSet As DataSet, ByVal preparatorName As String) As DataSet
        Function GetBaseDataSet(ByVal criteria As DeclarationCriteria) As DataSet

        Function IsValid(ByVal criteria As DeclarationCriteria) As Boolean
        Function HasWarnings(ByVal criteria As DeclarationCriteria) As Boolean
        Function GetAllErrors(ByVal criteria As DeclarationCriteria) As String
        Function GetAllWarnings(ByVal criteria As DeclarationCriteria) As String

    End Interface

End Namespace