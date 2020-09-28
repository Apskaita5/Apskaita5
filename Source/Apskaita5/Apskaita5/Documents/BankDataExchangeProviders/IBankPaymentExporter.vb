Imports System.IO

Namespace Documents.BankDataExchangeProviders

    ''' <summary>
    ''' Common interface to be implemented by payment export adapters.
    ''' </summary>
Public Interface IBankPaymentExporter

    ''' <summary>
    ''' Gets a human readable (localized) description of the export type.
    ''' </summary>
    ReadOnly Property Description() As String

    ''' <summary>
    ''' Gets a description of an exported bank data file standard format, e.g. ISO20022 (*.xml).
    ''' </summary>
    ReadOnly Property FileFormatDescription() As String

    ''' <summary>
    ''' Gets an exported bank data file standard extension, e.g. xml.
    ''' </summary>
    ReadOnly Property FileExtension() As String


    ''' <summary>
    ''' Exports <paramref ref="payments">payments</paramref> to the format of the specific adapter.
    ''' </summary>
    ''' <param name="payments">payments to export</param>
    ''' <param name="targetStream">stream to write the exported data to</param>
    sub Export(payments As ExportedBankPayments, targetStream As Stream)

End Interface

End Namespace