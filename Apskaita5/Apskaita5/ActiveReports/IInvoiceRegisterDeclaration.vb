Namespace ActiveReports

    ''' <summary>
    ''' Represents an interface required for a <see cref="InvoiceInfoItemList">InvoiceInfoItemList</see>.
    ''' An object, that implements this interface, represents an invoice register declaration 
    ''' (report to a state institution) of a certain version and holds methods 
    ''' that maps InvoiceInfoItemList data to a ffdata format.
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface IInvoiceRegisterDeclaration

        ''' <summary>
        ''' Gets a name of the invoice register declaration.
        ''' </summary>
        ''' <remarks></remarks>
        ReadOnly Property Name() As String

        ''' <summary>
        ''' Gets a start of the period that the invoice register declaration is valid for.
        ''' </summary>
        ''' <remarks></remarks>
        ReadOnly Property ValidFrom() As Date

        ''' <summary>
        ''' Gets an end of the period that the invoice register declaration is valid for.
        ''' </summary>
        ''' <remarks></remarks>
        ReadOnly Property ValidTo() As Date

        ''' <summary>
        ''' Gets a type of the invoice register (made or received) 
        ''' that the declaration is valid for.
        ''' </summary>
        ''' <remarks></remarks>
        ReadOnly Property [Type]() As InvoiceInfoType

        ''' <summary>
        ''' Gets a ffdata format dataset.
        ''' </summary>
        ''' <param name="invoiceRegister">an invoice register report to be exported.</param>
        ''' <remarks></remarks>
        Function GetFfDataDataSet(ByVal invoiceRegister As InvoiceInfoItemList) As DataSet

    End Interface

End Namespace