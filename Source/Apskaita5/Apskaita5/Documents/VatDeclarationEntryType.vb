Namespace Documents

    ''' <summary>
    ''' Describes how an invoice item/line value is displayed in a 
    ''' <see cref="VatDeclarationEntry">VAT declaration field</see>.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum VatDeclarationEntryType

        ''' <summary>
        ''' An item price is added to the VAT declaration field.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(0)> _
        AddPrice

        ''' <summary>
        ''' An item VAT is added to the VAT declaration field.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(1)> _
        AddVat

        ''' <summary>
        ''' An item price is subtracted from the VAT declaration field.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(2)> _
        SubtractPrice

        ''' <summary>
        ''' An item VAT is subtracted from the VAT declaration field.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(3)> _
        SubtractVat

    End Enum

End Namespace
