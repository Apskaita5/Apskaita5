Imports ApskaitaObjects.Attributes

Namespace ActiveReports

    ''' <summary>
    ''' Represents a type of <see cref="InvoiceInfoItemList">an invoice register report</see>.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum InvoiceInfoType

        ''' <summary>
        ''' A register of <see cref="Documents.InvoiceReceived">invoices received</see>.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(0)> _
        InvoiceReceived

        ''' <summary>
        ''' A register of <see cref="Documents.InvoiceMade">invoices made</see>.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(1)> _
        InvoiceMade

        ''' <summary>
        ''' A register of <see cref="Documents.ProformaInvoiceMade">proforma invoices made</see>.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(2)> _
        ProformaInvoiceMade

    End Enum

End Namespace