Imports ApskaitaObjects.Attributes

Namespace Documents

    ''' <summary>
    ''' Represents a subtype of an <see cref="InvoiceMade">InvoiceMade</see> 
    ''' or an <see cref="InvoiceReceived">InvoiceReceived</see>.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum InvoiceType

        ''' <summary>
        ''' An ordinary invoice.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(0)> _
        Normal

        ''' <summary>
        ''' A credit invoice.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(1)> _
        Credit

        ''' <summary>
        ''' A debit invoice.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(2)> _
        Debit

    End Enum

End Namespace
