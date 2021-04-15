Namespace ActiveReports

    ''' <summary>
    ''' Represents an interface required for a <see cref="VatDeclaration">VatDeclaration</see>.
    ''' An object, that implements this interface, represents a VAT declaration 
    ''' (report to a state institution) of a certain version and holds methods 
    ''' that maps VatDeclaration data to a ffdata format.
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface IVatDeclaration

        ''' <summary>
        ''' Gets a name of the invoice register declaration.
        ''' </summary>
        ''' <remarks></remarks>
        ReadOnly Property Name() As String

        ''' <summary>
        ''' Gets a start of the period that the VAT declaration is valid for.
        ''' </summary>
        ''' <remarks></remarks>
        ReadOnly Property ValidFrom() As Date

        ''' <summary>
        ''' Gets an end of the period that the VAT declaration is valid for.
        ''' </summary>
        ''' <remarks></remarks>
        ReadOnly Property ValidTo() As Date

        ''' <summary>
        ''' Gets a ffdata format dataset.
        ''' </summary>
        ''' <param name="declaration">a VAT declaration to be exported.</param>
        ''' <remarks></remarks>
        Function GetFfDataDataSet(ByVal declaration As VatDeclaration) As DataSet

    End Interface

End Namespace
