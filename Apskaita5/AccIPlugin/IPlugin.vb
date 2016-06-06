Public Interface IPlugin

    Function GetDatabaseStructureGaugeName() As String

    Function GetRoleDatabaseAccessGaugeName() As String

    Function GetSqlQueryDepositoryName() As String

    Function GetLocalizedDocumentTypeDictionary() As List(Of KeyValuePair(Of String, String))

    Function GetLocalizedLtaOperationTypeDictionary() As List(Of KeyValuePair(Of String, String))

    Function GetLocalizedGoodsOperationTypeDictionary() As List(Of KeyValuePair(Of String, String))

    Function GetLocalizedGoodsComplexOperationTypeDictionary() As List(Of KeyValuePair(Of String, String))

    Function GetLocalizedPropertyName(ByVal objectType As Type, ByVal propertyName As String) As String

    Function GetInvoiceAdapter(ByVal adapterTypeCode As String, _
        ByVal operationID As Integer, ByVal parentValidator As Object, _
        ByVal forInvoiceMade As Boolean) As Object

End Interface
