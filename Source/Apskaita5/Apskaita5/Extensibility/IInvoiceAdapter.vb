Imports System.IO

Namespace Extensibility

    ''' <summary>
    ''' Provides a common interface for invoice import (plugins).
    ''' </summary>
Public Interface IInvoiceAdapter

        ''' <summary>
        ''' Gets a value indicating whether the adapter can import invoices made
        ''' (otherwise - received).
        ''' </summary>
        ReadOnly Property ForInvoicesMade() As Boolean

        ''' <summary>
        ''' Gets a default invoice id prefix (to differentiate multiple integration endpoints).
        ''' </summary>
        ReadOnly Property IdPrefix() As String

        ''' <summary>
        ''' Gets a default extension for the files that contain invoice data to be imported
        ''' (e.g. xml).
        ''' </summary>
        ReadOnly Property FileExtension() As String

        ''' <summary>
        ''' Gets a value indicating whether the adapter requires a default value
        ''' for invoice line content, i.e. original data may not contain item names.
        ''' </summary>
        ReadOnly Property RequiresDefaultLineContent() As Boolean

        ''' <summary>
        ''' Gets a value indicating whether the adapter requires a default value
        ''' for income/costs account, i.e. original data may not contain account data.
        ''' </summary>
        ReadOnly Property RequiresDefaultAccount() As Boolean


        
        ''' <summary>
        ''' Loads invoices from the data stream specified.
        ''' </summary>
        ''' <param name="dataStream">a data stream that contains invoice data</param>
        ''' <param name="companyCode">a (national) code of the current company
        ''' (to make sure that the data is meant for it)</param>
        ''' <param name="invoiceIdPrefix">a prefix to use for original invoice id's
        ''' in order to differentiate multiple integration endpoints</param>
        ''' <param name="defaultAccount">a default income/costs account to use
        ''' (if it's not specified in the original invoice data)</param>
        ''' <param name="defaultVatAccount">a default VAT account to use
        ''' (if it's not specified in the original invoice data)</param>
        ''' <param name="defaultVatSchema">a default VAT schema to use
        ''' (if it's not specified in the original invoice data)</param>
        ''' <param name="defaultContent">a default invoice content (description) to use
        ''' (if it's not specified in the original invoice data)</param>
        ''' <param name="defaultLineContent">a default invoice item (line) content to use
        ''' (if it's not specified in the original invoice data)</param>
        ''' <param name="defaultMeasureUnit">a default measure unit to use
        ''' (if it's not specified in the original invoice data)</param>
        ''' <param name="accountLookup">a lookup for accounts (to validate if they
        ''' are specified in the original invoice data)</param>
        ''' <param name="allowNullVatSchema">whether to allow null VAT schemas in data</param>
        ''' <param name="vatSchemaLookup">a lookup for VAT schemas (to validate if they
        ''' are specified in the original invoice data)</param>
        ''' <param name="serviceLookup">a lookup for services (to set relevant invoice data
        ''' using service data if the invoice reference a service)</param>
        Function LoadInvoices(dataStream As Stream, companyCode As String, _
            invoiceIdPrefix As String, defaultAccount As Long, defaultVatAccount As Long, _
            defaultVatSchema As VatDeclarationSchemaInfo, defaultContent As String, _
            defaultLineContent As String, defaultMeasureUnit As String, _
            allowNullVatSchema As Boolean, accountLookup As IList(of AccountInfo), _
            vatSchemaLookup As IList(of VatDeclarationSchemaInfo), _
            serviceLookup As IList(of ServiceInfo))As List(of InvoiceInfo.InvoiceInfo)

End Interface

End Namespace
