Namespace ActiveReports

    ''' <summary>
    ''' Represents an interface required for a <see cref="InvoiceInfoItemList">InvoiceInfoItemList</see>.
    ''' An object, that implements this interface, represents an invoice register in XMl format (SAF-T)
    ''' (report to a state institution) of a certain version and holds methods 
    ''' that maps InvoiceInfoItemList data to an XML format.
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface IInvoiceRegisterSafT

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
        ''' Gets an XML representation of the report.
        ''' </summary>
        ''' <param name="invoiceRegister">an invoice register report to be exported.</param>
        ''' <remarks></remarks>
        Function GetXmlString(ByVal invoiceRegister As InvoiceInfoItemList, _
            ByVal softwareVersion As String) As String

    End Interface

End Namespace