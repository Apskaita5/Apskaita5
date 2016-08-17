Imports ApskaitaObjects.Attributes

Namespace Settings

    ''' <summary>
    ''' Represents a type of codes used in business objects.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CodeType

        ''' <summary>
        ''' A personal income type code used in <see cref="Workers.PayOutNaturalPerson">
        ''' payments to natural persons</see> to match the type with the personal income 
        ''' tax declaration.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(0)> _
        GpmDeclaration

        ''' <summary>
        ''' A personal income type code used in <see cref="Workers.PayOutNaturalPerson">
        ''' payments to natural persons</see> to match the type with the social security 
        ''' declaration.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(1)> _
        SodraDeclaration

        ''' <summary>
        ''' A municipality code used in various tax declarations by the state tax inspectorate (VMI).
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(2)> _
        VmiMunicipality

        ''' <summary>
        ''' A state code (ISO 3166–1 alpha 2) used in various tax declarations 
        ''' by the state tax inspectorate (VMI).
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(3)> _
        VmiState

        ''' <summary>
        ''' A VAT schema type used in various tax declarations 
        ''' by the state tax inspectorate (VMI).
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(4)> _
        VmiVatType

    End Enum

End Namespace