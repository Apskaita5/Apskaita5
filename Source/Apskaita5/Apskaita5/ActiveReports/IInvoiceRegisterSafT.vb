Namespace ActiveReports

    ''' <summary>
    ''' Represents an interface required for a <see cref="InvoiceInfoItemList">InvoiceInfoItemList</see>.
    ''' An object, that implements this interface, represents an invoice register in XMl format (i.SAF)
    ''' (report to a state institution) of a certain version and holds methods 
    ''' that maps InvoiceInfoItemList data to an XML format.
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface IInvoiceRegisterISaf

        ''' <summary>
        ''' Gets a name of the invoice register report.
        ''' </summary>
        ''' <remarks></remarks>
        ReadOnly Property Name() As String

        ''' <summary>
        ''' Gets a start of the period that the invoice register report is valid for.
        ''' </summary>
        ''' <remarks></remarks>
        ReadOnly Property ValidFrom() As Date

        ''' <summary>
        ''' Gets an end of the period that the invoice register report is valid for.
        ''' </summary>
        ''' <remarks></remarks>
        ReadOnly Property ValidTo() As Date

        ''' <summary>
        ''' Gets a name of the xsd file that defines the report xml requirements.
        ''' </summary>
        ''' <remarks></remarks>
        ReadOnly Property XsdFileName() As String

        ''' <summary>
        ''' Gets an XML representation of the report.
        ''' </summary>
        ''' <param name="invoiceRegister">an invoice register report to be exported</param>
        ''' <param name="softwareVersion">a current version of the application</param>
        ''' <param name="selectedInvoicesIds">id's of the invoices to be exported 
        ''' (null or empty to export all the invoices)</param>
        ''' <param name="warnings">an out parameter that returns
        ''' XML validation warnings</param>
        ''' <remarks></remarks>
        Function GetXmlString(ByVal invoiceRegister As InvoiceInfoItemList,
            ByVal softwareVersion As String, ByVal selectedInvoicesIds As Integer(),
            ByRef warnings As String) As String

    End Interface

End Namespace
