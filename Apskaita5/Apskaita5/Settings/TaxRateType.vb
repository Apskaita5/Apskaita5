Imports ApskaitaObjects.Attributes

Namespace Settings
    ''' <summary>
    ''' Represents a tax rate type.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum TaxRateType

        ''' <summary>
        ''' VAT (value added tax)
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(0, "PVM")> _
    Vat

        ''' <summary>
        ''' GPM (personal income tax)
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(1, "GPM")> _
    GPM

        ''' <summary>
        ''' PSD (health insurance contribution) that is deducted from an amount payable to a person.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(2, "PSDI")> _
    PSDForPerson

        ''' <summary>
        ''' PSD (health insurance contribution) that is payed by a company.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(3, "PSDP")> _
    PSDForCompany

        ''' <summary>
        ''' SODRA (social security contribution) that is deducted from an amount payable to a person.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(4, "SODRAI")> _
    SodraForPerson

        ''' <summary>
        ''' SODRA (social security contribution) that is payed by a company.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(5, "SODRAP")> _
    SodraForCompany

    End Enum
End Namespace