Namespace ActiveReports

    Public Interface IDeclaration

        ReadOnly Property Name() As String
        ReadOnly Property ValidFrom() As Date
        ReadOnly Property ValidTo() As Date
        ReadOnly Property RequiredDateInterval() As Boolean
        ReadOnly Property RequiredSodraDepartment() As Boolean
        ReadOnly Property RequiredSodraRate() As Boolean
        ReadOnly Property RequiredYear() As Boolean
        ReadOnly Property RequiredQuarter() As Boolean
        ReadOnly Property RequiredMonth() As Boolean
        ReadOnly Property RequiredSodraAccount() As Boolean
        ReadOnly Property RequiredSodraAccount2() As Boolean
        ReadOnly Property RequiredMunicipalityCode() As Boolean
        ReadOnly Property RequiredDeclarationItemCode() As Boolean
        ReadOnly Property Warnings() As String

        Function GetFFDataDataSet(ByVal declarationDataSet As DataSet, ByVal preparatorName As String) As DataSet
        Function GetBaseDataSet() As DataSet
        Function ValidateParams(ByRef Errors As String, ByRef Warnings As String) As Boolean

        Sub SetParams(ByVal nDate As Date, ByVal nDateFrom As Date, ByVal nDateTo As Date, _
            ByVal nSODRADepartment As String, ByVal nSODRARate As Double, ByVal nYear As Integer, _
            ByVal nQuarter As Integer, ByVal nMonth As Integer, ByVal nSODRAAccount As Long, _
            ByVal nSODRAAccount2 As Long, ByVal nMunicipalityCode As String, _
            ByVal nDeclarationItemCode As String)

    End Interface

End Namespace