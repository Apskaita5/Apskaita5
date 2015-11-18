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

    End Enum

End Namespace