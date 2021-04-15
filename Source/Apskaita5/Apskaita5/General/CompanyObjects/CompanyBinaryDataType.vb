Imports ApskaitaObjects.Attributes

Namespace General

    ''' <summary>
    ''' Represents a type of company's binary data (data stored as byte array)  
    ''' </summary>
    ''' <remarks>Used in <see cref="CompanyRegionalData">CompanyRegionalData</see> to persist large/complex resources.</remarks>
    Public Enum CompanyBinaryDataType

        ''' <summary>
        ''' A company logo image.
        ''' </summary>
        ''' <remarks>Is stored in JPEG format.</remarks>
        <EnumValue(0, "LOG")> _
        Logo

        ''' <summary>
        ''' A company invoice form (rdlc).
        ''' </summary>
        ''' <remarks>Is stored as binary file stream.</remarks>
        <EnumValue(1, "INV")> _
        InvoiceForm

        ''' <summary>
        ''' A company invoice form (rdlc).
        ''' </summary>
        ''' <remarks>Is stored as binary file stream.</remarks>
        <EnumValue(2, "AVA")> _
        ProformaInvoiceForm

    End Enum

End Namespace
