Namespace Documents.BankDataExchangeProviders

    ''' <summary>
    ''' Represents a common interface for objects that convert various bank
    ''' data formats to the canonical format suitable for import.
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface IBankAccountStatement

        ''' <summary>
        ''' Gets a human readable (localized) description of the bank data
        ''' format that the object handles.
        ''' </summary>
        ''' <remarks></remarks>
        ReadOnly Property SourceType() As String

        ''' <summary>
        ''' Gets a human readable (localized) description of the general 
        ''' bank account statement information, e.g. balance, owner, etc.
        ''' </summary>
        ''' <remarks></remarks>
        ReadOnly Property Description() As String

        ''' <summary>
        ''' Gets the bank account statement period start date as provided 
        ''' in the imported bank data.
        ''' </summary>
        ''' <remarks></remarks>
        ReadOnly Property PeriodStart() As Date

        ''' <summary>
        ''' Gets the bank account statement period end date as provided 
        ''' in the imported bank data.
        ''' </summary>
        ''' <remarks></remarks>
        ReadOnly Property PeriodEnd() As Date

        ''' <summary>
        ''' Gets the bank account balance at the start of the period as provided 
        ''' in the imported bank data.
        ''' </summary>
        ''' <remarks></remarks>
        ReadOnly Property BalanceStart() As Double

        ''' <summary>
        ''' Gets the total sum of money transfered into the account as provided 
        ''' in the imported bank data.
        ''' </summary>
        ''' <remarks></remarks>
        ReadOnly Property Income() As Double

        ''' <summary>
        ''' Gets the total sum of money transfered from the account as provided 
        ''' in the imported bank data.
        ''' </summary>
        ''' <remarks></remarks>
        ReadOnly Property Spendings() As Double

        ''' <summary>
        ''' Gets the bank account balance at the end of the period as provided 
        ''' in the imported bank data.
        ''' </summary>
        ''' <remarks></remarks>
        ReadOnly Property BalanceEnd() As Double

        ''' <summary>
        ''' Gets the bank account currency as provided in the imported bank data.
        ''' </summary>
        ''' <remarks></remarks>
        ReadOnly Property AccountCurrency() As String

        ''' <summary>
        ''' Gets the bank account IBAN number as provided in the imported bank data.
        ''' </summary>
        ''' <remarks></remarks>
        ReadOnly Property AccountIBAN() As String

        ''' <summary>
        ''' Whether the bank data format contains bank account currency.
        ''' </summary>
        ''' <remarks></remarks>
        ReadOnly Property FormatContainsAccountCurrency() As Boolean

        ''' <summary>
        ''' Whether the bank data format contains bank account IBAN number.
        ''' </summary>
        ''' <remarks></remarks>
        ReadOnly Property FormatContainsAccountIBAN() As Boolean

        ''' <summary>
        ''' Gets a description of an imported bank data file standard format, e.g. ISO20022 (*.xml).
        ''' </summary>
        ''' <remarks></remarks>
        ReadOnly Property FileFormatDescription() As String

        ''' <summary>
        ''' Gets an imported bank data file standard extension, e.g. xml.
        ''' </summary>
        ''' <remarks></remarks>
        ReadOnly Property FileExtension() As String

        ''' <summary>
        ''' A list of the bank operation data in canonical format.
        ''' </summary>
        ''' <remarks></remarks>
        ReadOnly Property Items() As List(Of BankAccountStatementItem)

        ''' <summary>
        ''' Loads data from a file.
        ''' </summary>
        ''' <param name="fileName">A path to the file containing bank account statement data.</param>
        ''' <param name="encoding">A file encoding if it is known to be not standard for a given file type.</param>
        ''' <remarks></remarks>
        Sub LoadDataFromFile(ByVal fileName As String, _
            Optional ByVal encoding As System.Text.Encoding = Nothing)

        ''' <summary>
        ''' Loads data from a string.
        ''' </summary>
        ''' <param name="source">A string containing bank account statement data.</param>
        ''' <remarks></remarks>
        Sub LoadDataFromString(ByVal source As String)

    End Interface

End Namespace