Imports ApskaitaObjects.Workers

Namespace ActiveReports

    ''' <summary>
    ''' Represents an interface required for a <see cref="PayOutNaturalPersonWithoutTaxesList">PayOutNaturalPersonWithoutTaxesList</see>.
    ''' An object, that implements this interface, represents a payouts to natural persons declaration 
    ''' (report to a state institution) of a certain version and holds methods 
    ''' that maps PayOutNaturalPersonWithoutTaxesList data to a ffdata format.
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface IPayOutsNaturalPersonsWithoutTaxesDeclaration

        ''' <summary>
        ''' Gets a name of the payouts declaration.
        ''' </summary>
        ''' <remarks></remarks>
        ReadOnly Property Name() As String

        ''' <summary>
        ''' Gets a start of the period that the payouts declaration is valid for.
        ''' </summary>
        ''' <remarks></remarks>
        ReadOnly Property ValidFrom() As Date

        ''' <summary>
        ''' Gets an end of the period that the payouts declaration is valid for.
        ''' </summary>
        ''' <remarks></remarks>
        ReadOnly Property ValidTo() As Date

        ''' <summary>
        ''' Gets a ffdata format data as an XML string.
        ''' </summary>
        ''' <param name="payouts">a list of the payouts data to be exported</param>
        ''' <remarks></remarks>
        Function GetFfDataString(ByVal payouts As PayOutNaturalPersonWithoutTaxesList) As String

    End Interface

End Namespace