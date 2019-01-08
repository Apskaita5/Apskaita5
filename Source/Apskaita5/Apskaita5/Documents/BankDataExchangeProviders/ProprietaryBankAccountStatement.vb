Namespace Documents.BankDataExchangeProviders

    ''' <summary>
    ''' Represents a converter object that converts tab delimited 
    ''' bank operations data of predefined structure 
    ''' to the canonical format suitable for import.
    ''' </summary>
    ''' <remarks>Is used as a parameter for <see cref="BankOperationItemList.GetBankOperationItemList">BankOperationItemList.GetBankOperationItemList</see> method.
    ''' The format is defined by this object.</remarks>
    <Serializable()> _
    Public Class ProprietaryBankAccountStatement
        Implements IBankAccountStatement

        Private Const PASTE_COLUMN_COUNT As Integer = 13

        Private _Items As List(Of BankAccountStatementItem) = Nothing
        Private _Income As Double = 0
        Private _Spendings As Double = 0


        ''' <summary>
        ''' Gets a human readable (localized) description of the bank data
        ''' format that the object handles.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property SourceType() As String _
            Implements IBankAccountStatement.SourceType
            Get
                Return My.Resources.Documents_BankDataExchangeProviders_ProprietaryBankAccountStatement_SourceType
            End Get
        End Property

        ''' <summary>
        ''' Gets a description of an imported bank data file standard format.
        ''' </summary>
        ''' <remarks>Returns 'Tab Delimited Text File (*.txt)'.</remarks>
        Public ReadOnly Property FileFormatDescription() As String _
            Implements IBankAccountStatement.FileFormatDescription
            Get
                Return "Tab Delimited Text File (*.txt)"
            End Get
        End Property

        ''' <summary>
        ''' Gets an imported bank data file standard extension.
        ''' </summary>
        ''' <remarks>Returns txt.</remarks>
        Public ReadOnly Property FileExtension() As String _
            Implements IBankAccountStatement.FileExtension
            Get
                Return "txt"
            End Get
        End Property

        ''' <summary>
        ''' Whether the bank data format contains bank account currency.
        ''' </summary>
        ''' <remarks>Returns FALSE, because the tab delimited format does not provide
        ''' any space for metadata.</remarks>
        Public ReadOnly Property FormatContainsAccountCurrency() As Boolean _
            Implements IBankAccountStatement.FormatContainsAccountCurrency
            Get
                Return False
            End Get
        End Property

        ''' <summary>
        ''' Whether the bank data format contains bank account IBAN number.
        ''' </summary>
        ''' <remarks>Returns FALSE, because the tab delimited format does not provide
        ''' any space for metadata.</remarks>
        Public ReadOnly Property FormatContainsAccountIBAN() As Boolean _
            Implements IBankAccountStatement.FormatContainsAccountIBAN
            Get
                Return False
            End Get
        End Property

        ''' <summary>
        ''' Gets the bank account currency as provided in the imported bank data.
        ''' </summary>
        ''' <remarks>Returns an empty string, because the tab delimited format does not provide
        ''' any space for metadata.</remarks>
        Public ReadOnly Property AccountCurrency() As String _
            Implements IBankAccountStatement.AccountCurrency
            Get
                Return ""
            End Get
        End Property

        ''' <summary>
        ''' Gets the bank account IBAN number as provided in the imported bank data.
        ''' </summary>
        ''' <remarks>Returns an empty string, because the tab delimited format does not provide
        ''' any space for metadata.</remarks>
        Public ReadOnly Property AccountIBAN() As String _
            Implements IBankAccountStatement.AccountIBAN
            Get
                Return ""
            End Get
        End Property

        ''' <summary>
        ''' Gets the bank account statement period start date as provided 
        ''' in the imported bank data.
        ''' </summary>
        ''' <remarks>Returns Today, because the tab delimited format does not provide
        ''' any space for metadata.</remarks>
        Public ReadOnly Property PeriodStart() As Date _
            Implements IBankAccountStatement.PeriodStart
            Get
                Return Today
            End Get
        End Property

        ''' <summary>
        ''' Gets the bank account statement period end date as provided 
        ''' in the imported bank data.
        ''' </summary>
        ''' <remarks>Returns Today, because the tab delimited format does not provide
        ''' any space for metadata.</remarks>
        Public ReadOnly Property PeriodEnd() As Date _
            Implements IBankAccountStatement.PeriodEnd
            Get
                Return Today
            End Get
        End Property

        ''' <summary>
        ''' Gets the bank account balance at the start of the period as provided 
        ''' in the imported bank data.
        ''' </summary>
        ''' <remarks>Returns 0, because the tab delimited format does not provide
        ''' any space for metadata.</remarks>
        Public ReadOnly Property BalanceStart() As Double _
            Implements IBankAccountStatement.BalanceStart
            Get
                Return 0
            End Get
        End Property

        ''' <summary>
        ''' Gets the total sum of money transfered into the account as provided 
        ''' in the imported bank data.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Income() As Double _
            Implements IBankAccountStatement.Income
            Get
                Return CRound(_Income, 2)
            End Get
        End Property

        ''' <summary>
        ''' Gets the total sum of money transfered from the account as provided 
        ''' in the imported bank data.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Spendings() As Double _
            Implements IBankAccountStatement.Spendings
            Get
                Return CRound(_Spendings, 2)
            End Get
        End Property

        ''' <summary>
        ''' Gets the bank account balance at the end of the period as provided 
        ''' in the imported bank data.
        ''' </summary>
        ''' <remarks>Returns 0, because the tab delimited format does not provide
        ''' any space for metadata.</remarks>
        Public ReadOnly Property BalanceEnd() As Double _
            Implements IBankAccountStatement.BalanceEnd
            Get
                Return 0
            End Get
        End Property

        ''' <summary>
        ''' Gets a human readable (localized) description of the general 
        ''' bank account statement information.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Description() As String _
            Implements IBankAccountStatement.Description
            Get
                Return My.Resources.Documents_BankDataExchangeProviders_ProprietaryBankAccountStatement_Description
            End Get
        End Property

        ''' <summary>
        ''' A list of the bank operation data in canonical format.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Items() As List(Of BankAccountStatementItem) _
            Implements IBankAccountStatement.Items
            Get
                If _Items Is Nothing Then
                    _Items = New List(Of BankAccountStatementItem)
                End If
                Return _Items
            End Get
        End Property



        ''' <summary>
        ''' Loads data from a text file.
        ''' </summary>
        ''' <param name="fileName">A path to the file containing tab delimited bank operation data.</param>
        ''' <param name="encoding">A file encoding if it is known to be not standard (not Unicode) for a given file type.</param>
        ''' <remarks></remarks>
        Public Sub LoadDataFromFile(ByVal fileName As String, _
            Optional ByVal encoding As System.Text.Encoding = Nothing) _
            Implements IBankAccountStatement.LoadDataFromFile

            If StringIsNullOrEmpty(fileName) Then
                Throw New Exception(My.Resources.Documents_BankOperationItemList_FileNameNull)
            ElseIf Not IO.File.Exists(fileName) Then
                Throw New Exception(String.Format(My.Resources.Documents_BankOperationItemList_FileNotFound, fileName))
            End If

            If encoding Is Nothing Then
                encoding = System.Text.Encoding.Unicode
            End If

            Dim content As String = IO.File.ReadAllText(fileName, encoding)

            If StringIsNullOrEmpty(content) Then
                Throw New Exception(My.Resources.Documents_BankOperationItemList_FileEmpty)
            End If

            LoadDataFromString(content)

        End Sub

        Public Sub LoadDataFromTable(ByVal source As DataTable)

            If source Is Nothing Then Throw New ArgumentNullException("source")

            _Items = New List(Of BankAccountStatementItem)

            For Each dr As DataRow In source.Rows
                _Items.Add(GetBankAccountStatementItem(dr))
            Next

            _Income = 0
            _Spendings = 0
            For Each i As BankAccountStatementItem In _Items
                If i.Inflow Then
                    _Income = CRound(_Income + i.SumInAccount, 2)
                Else
                    _Spendings = CRound(_Income + i.SumInAccount, 2)
                End If
            Next

        End Sub

        ''' <summary>
        ''' Gets a datatable which columns corresponds to the required imported data 
        ''' (property name, data type and regionalized caption).
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function GetDataTableSpecification() As DataTable
            Return Utilities.GetDataTableSpecification(GetType(BankAccountStatementItem),
                New String() {})
        End Function

        ''' <summary>
        ''' Loads data from a string.
        ''' </summary>
        ''' <param name="source">A string containing tab delimited bank operation data.</param>
        ''' <remarks></remarks>
        Public Sub LoadDataFromString(ByVal source As String) _
            Implements IBankAccountStatement.LoadDataFromString

            If StringIsNullOrEmpty(source) Then
                Throw New Exception(My.Resources.Documents_BankOperationItemList_SourceStringNull)
            End If

            Dim lineDelim As Char() = {ControlChars.Cr, ControlChars.Lf}
            Dim colDelim As Char() = {ControlChars.Tab}

            Dim lines As String() = source.Split(lineDelim, StringSplitOptions.RemoveEmptyEntries)

            If lines(0).Split(colDelim, StringSplitOptions.None).Length <> PASTE_COLUMN_COUNT Then
                Throw New Exception(String.Format(My.Resources.Documents_BankDataExchangeProviders_ProprietaryBankAccountStatement_InvalidColumnCountInPasteString, _
                    PASTE_COLUMN_COUNT.ToString(), lines(0).Split(colDelim, StringSplitOptions.None).Length.ToString, _
                    vbCrLf, GetPasteStringColumnsDescription()))
            End If

            _Items = New List(Of BankAccountStatementItem)

            For Each s As String In lines
                _Items.Add(GetBankAccountStatementItem(s))
            Next

            _Income = 0
            _Spendings = 0
            For Each i As BankAccountStatementItem In _Items
                If i.Inflow Then
                    _Income = CRound(_Income + i.SumInAccount, 2)
                Else
                    _Spendings = CRound(_Income + i.SumInAccount, 2)
                End If
            Next

        End Sub

        Private Function GetBankAccountStatementItem(ByVal source As String) As BankAccountStatementItem

            Dim result As New BankAccountStatementItem

            Dim valueArray As String() = source.Split(New Char() {ControlChars.Tab}, StringSplitOptions.None)

            Try
                result.Date = Convert.ToDateTime(GetValueSafe(valueArray, 0).Trim)
                result.DocumentNumber = GetLimitedLengthString(GetValueSafe(valueArray, 1).Trim, 30)
                result.PersonCode = GetValueSafe(valueArray, 2).Trim
                result.PersonName = GetValueSafe(valueArray, 3).Trim
                result.Content = GetLimitedLengthString(GetValueSafe(valueArray, 4).Trim, 255)
                result.Inflow = (StringIsNullOrEmpty(GetValueSafe(valueArray, 5)))
                result.Currency = GetValueSafe(valueArray, 6).Trim.ToUpper
                result.OriginalSum = CDblSafe(GetValueSafe(valueArray, 7).Trim, 2, 0)
                result.SumLTL = CDblSafe(GetValueSafe(valueArray, 8).Trim, 2, 0)
                result.SumInAccount = CDblSafe(GetValueSafe(valueArray, 9).Trim, 2, 0)
                If Not CRound(result.OriginalSum) > 0 Then
                    result.OriginalSum = result.SumInAccount
                End If
                result.UniqueCode = GetValueSafe(valueArray, 10).Trim
                result.PersonBankAccount = GetValueSafe(valueArray, 11).Trim
                result.PersonBankName = GetValueSafe(valueArray, 12).Trim

            Catch ex As Exception
                Throw New Exception(String.Format(My.Resources.Documents_BankDataExchangeProviders_ProprietaryBankAccountStatement_InvalidSourceString, _
                    vbCrLf, source, vbCrLf, GetPasteStringColumnsDescription()))
            End Try

            Return result

        End Function

        Private Function GetBankAccountStatementItem(ByVal dr As DataRow) As BankAccountStatementItem

            Dim result As New BankAccountStatementItem

            result.Date = DirectCast(dr.Item("Date"), Date)
            result.DocumentNumber = dr.Item("DocumentNumber").ToString
            result.PersonCode = dr.Item("PersonCode").ToString
            result.PersonName = dr.Item("PersonName").ToString
            result.PersonBankAccount = dr.Item("PersonBankAccount").ToString
            result.PersonBankName = dr.Item("PersonBankName").ToString
            result.Content = dr.Item("Content").ToString
            result.Inflow = DirectCast(dr.Item("Inflow"), Boolean)
            result.Currency = dr.Item("Currency").ToString.ToUpper
            result.CurrencyRate = DirectCast(dr.Item("CurrencyRate"), Double)
            result.OriginalSum = DirectCast(dr.Item("OriginalSum"), Double)
            result.SumInAccount = DirectCast(dr.Item("SumInAccount"), Double)
            result.SumLTL = DirectCast(dr.Item("SumLTL"), Double)
            result.UniqueCode = dr.Item("UniqueCode").ToString

            Return result

        End Function

        Private Function GetValueSafe(ByVal source As String(), ByVal index As Integer) As String
            If index >= source.Length Then
                Return ""
            Else
                Return source(index)
            End If
        End Function


        ''' <summary>
        ''' Gets expected fields count in (tab delimited) paste string.
        ''' </summary>
        Public Shared Function GetPasteStringColumnCount() As Integer
            Return PASTE_COLUMN_COUNT
        End Function

        ''' <summary>
        ''' Gets an array of expected fields sequence in (tab delimited) paste string.
        ''' </summary>
        Public Shared Function GetPasteStringColumns() As String()
            Return My.Resources.Documents_BankDataExchangeProviders_ProprietaryBankAccountStatement_PasteColumns.Split(New String() {"<BR>"}, _
                StringSplitOptions.RemoveEmptyEntries)
        End Function

        ''' <summary>
        ''' Gets a human readable description of expected fields sequence in (tab delimited) paste string.
        ''' </summary>
        Public Shared Function GetPasteStringColumnsDescription() As String
            Return String.Format(My.Resources.Documents_BankDataExchangeProviders_ProprietaryBankAccountStatement_PasteColumnsDescription, PASTE_COLUMN_COUNT.ToString(), _
                String.Join(", ", My.Resources.Documents_BankDataExchangeProviders_ProprietaryBankAccountStatement_PasteColumns.Split(New String() {"<BR>"}, _
                StringSplitOptions.RemoveEmptyEntries)))
        End Function

    End Class

End Namespace