Namespace Documents.BankDataExchangeProviders

    ''' <summary>
    ''' Represents a converter object that converts LITAS-ESIS standard 
    ''' bank account statement data to the canonical format suitable for import.
    ''' </summary>
    ''' <remarks>Is used as a parameter for <see cref="BankOperationItemList.GetBankOperationItemList">BankOperationItemList.GetBankOperationItemList</see> method.</remarks>
    <Serializable()> _
    Public Class LitasEsisBankAccountStatement
        Implements IBankAccountStatement

        Private Const LITAS_ESIS_OPERATION_LINE_CODE As String = "010"
        Private Const LITAS_ESIS_HEADER_LINE_CODE As String = "000"
        Private Const LITAS_ESIS_SUMMARY_LINE_CODE As String = "020"
        Private Const LITAS_ESIS_BALANCE_AT_START As String = "likutispr"
        Private Const LITAS_ESIS_BALANCE_AT_END As String = "likutispb"

        Private _AccountCurrency As String = ""
        Private _AccountIBAN As String = ""
        Private _PeriodStart As Date = Today
        Private _PeriodEnd As Date = Today
        Private _BalanceStart As Double = 0
        Private _Income As Double = 0
        Private _Spendings As Double = 0
        Private _BalanceEnd As Double = 0
        Private _Items As List(Of BankAccountStatementItem) = Nothing


        ''' <summary>
        ''' Gets a human readable (localized) description of the bank data
        ''' format that the object handles.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property SourceType() As String _
            Implements IBankAccountStatement.SourceType
            Get
                Return My.Resources.Documents_BankDataExchangeProviders_LitasEsisBankAccountStatement_SourceType
            End Get
        End Property

        ''' <summary>
        ''' Gets a description of an imported bank data file standard format, e.g. ISO20022 (*.xml).
        ''' </summary>
        ''' <remarks>Returns 'LITAS-ESIS File (*.acc)'.</remarks>
        Public ReadOnly Property FileFormatDescription() As String _
            Implements IBankAccountStatement.FileFormatDescription
            Get
                Return "LITAS-ESIS File (*.acc)"
            End Get
        End Property

        ''' <summary>
        ''' Gets an imported bank data file standard extension, e.g. xml.
        ''' </summary>
        ''' <remarks>Returns acc.</remarks>
        Public ReadOnly Property FileExtension() As String _
            Implements IBankAccountStatement.FileExtension
            Get
                Return "acc"
            End Get
        End Property

        ''' <summary>
        ''' Whether the bank data format contains bank account currency.
        ''' </summary>
        ''' <remarks>Returns TRUE.</remarks>
        Public ReadOnly Property FormatContainsAccountCurrency() As Boolean _
            Implements IBankAccountStatement.FormatContainsAccountCurrency
            Get
                Return True
            End Get
        End Property

        ''' <summary>
        ''' Whether the bank data format contains bank account IBAN number.
        ''' </summary>
        ''' <remarks>Returns TRUE.</remarks>
        Public ReadOnly Property FormatContainsAccountIBAN() As Boolean _
            Implements IBankAccountStatement.FormatContainsAccountIBAN
            Get
                Return True
            End Get
        End Property

        ''' <summary>
        ''' Gets the bank account currency as provided in the imported bank data.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property AccountCurrency() As String _
            Implements IBankAccountStatement.AccountCurrency
            Get
                Return _AccountCurrency
            End Get
        End Property

        ''' <summary>
        ''' Gets the bank account IBAN number as provided in the imported bank data.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property AccountIBAN() As String _
            Implements IBankAccountStatement.AccountIBAN
            Get
                Return _AccountIBAN
            End Get
        End Property

        ''' <summary>
        ''' Gets the bank account statement period start date as provided 
        ''' in the imported bank data.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property PeriodStart() As Date _
            Implements IBankAccountStatement.PeriodStart
            Get
                Return _PeriodStart
            End Get
        End Property

        ''' <summary>
        ''' Gets the bank account statement period end date as provided 
        ''' in the imported bank data.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property PeriodEnd() As Date _
            Implements IBankAccountStatement.PeriodEnd
            Get
                Return _PeriodEnd
            End Get
        End Property

        ''' <summary>
        ''' Gets the bank account balance at the start of the period as provided 
        ''' in the imported bank data.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property BalanceStart() As Double _
            Implements IBankAccountStatement.BalanceStart
            Get
                Return CRound(_BalanceStart, 2)
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
        ''' <remarks></remarks>
        Public ReadOnly Property BalanceEnd() As Double _
            Implements IBankAccountStatement.BalanceEnd
            Get
                Return CRound(_BalanceEnd, 2)
            End Get
        End Property

        ''' <summary>
        ''' Gets a human readable (localized) description of the general 
        ''' bank account statement information, e.g. balance, owner, etc.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Description() As String _
            Implements IBankAccountStatement.Description
            Get
                Return String.Format(My.Resources.Documents_BankDataExchangeProviders_LitasEsisBankAccountStatement_Description, _
                    vbCrLf, _PeriodStart.ToString("yyyy-MM-dd"), _PeriodEnd.ToString("yyyy-MM-dd"), _
                    _AccountCurrency.Trim.ToUpper, vbCrLf, DblParser(_BalanceStart), _
                    vbCrLf, DblParser(_Income), vbCrLf, DblParser(_Spendings), vbCrLf, DblParser(_BalanceEnd))
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
        ''' Loads data from a file.
        ''' </summary>
        ''' <param name="fileName">A path to the file containing bank account statement data.</param>
        ''' <param name="encoding">A file encoding if it is known to be not standard (windows-1257, ISO-8859-13) for a given file type.</param>
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
                encoding = System.Text.Encoding.GetEncoding("windows-1257")
            End If

            Dim content As String = IO.File.ReadAllText(fileName, encoding)

            If StringIsNullOrEmpty(content) Then
                Throw New Exception(My.Resources.Documents_BankOperationItemList_FileEmpty)
            End If

            LoadDataFromString(content)

        End Sub

        ''' <summary>
        ''' Loads data from a string.
        ''' </summary>
        ''' <param name="source">A string containing bank account statement data.</param>
        ''' <remarks></remarks>
        Public Sub LoadDataFromString(ByVal source As String) _
            Implements IBankAccountStatement.LoadDataFromString

            If StringIsNullOrEmpty(source) Then
                Throw New Exception(My.Resources.Documents_BankOperationItemList_SourceStringNull)
            End If

            Dim readText() As String = source.Split(New String() {vbCrLf}, StringSplitOptions.RemoveEmptyEntries)

            Dim operations As New List(Of String)

            For Each s As String In readText

                If GetElement(s, 0).Trim = LITAS_ESIS_OPERATION_LINE_CODE Then

                    operations.Add(s)

                ElseIf GetElement(s, 0).Trim = LITAS_ESIS_HEADER_LINE_CODE Then

                    _AccountCurrency = GetElement(s, 17).Trim.ToUpper
                    _AccountIBAN = GetElement(s, 16).Trim.ToUpper

                ElseIf GetElement(s, 0).Trim = LITAS_ESIS_SUMMARY_LINE_CODE Then

                    If GetElement(s, 1).Trim.ToLower = LITAS_ESIS_BALANCE_AT_START Then

                        _BalanceStart = CRound(CLongSafe(GetElement(s, 4), 0) / 100)

                        Dim tm As String = GetElement(s, 2).Trim
                        If tm.Length = 8 Then
                            _PeriodStart = New Date(CIntSafe(tm.Substring(0, 4)), _
                                CIntSafe(tm.Substring(4, 2)), CIntSafe(tm.Substring(6, 2)))
                        End If

                    ElseIf GetElement(s, 1).Trim.ToLower = LITAS_ESIS_BALANCE_AT_END Then

                        _BalanceEnd = CRound(CLongSafe(GetElement(s, 4), 0) / 100)

                        Dim tm As String = GetElement(s, 2).Trim
                        If tm.Length = 8 Then
                            _PeriodEnd = New Date(CIntSafe(tm.Substring(0, 4)), _
                                CIntSafe(tm.Substring(4, 2)), CIntSafe(tm.Substring(6, 2)))
                        End If

                    End If

                End If

            Next

            If operations.Count < 1 Then
                Throw New Exception(My.Resources.Documents_BankDataExchangeProviders_LitasEsisBankAccountStatement_InvalidFileContent)
            End If

            _Items = New List(Of BankAccountStatementItem)

            For Each s As String In operations
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

            Dim tm As String = GetElement(source, 2).Trim
            result.Date = New Date(CIntSafe(tm.Substring(0, 4)), CIntSafe(tm.Substring(4, 2)), _
                CIntSafe(tm.Substring(6, 2)))

            result.DocumentNumber = GetElement(source, 9).Trim
            result.PersonCode = GetElement(source, 18).Trim
            result.PersonName = GetElement(source, 17).Trim
            result.PersonBankAccount = GetElement(source, 16).Trim
            result.PersonBankName = GetElement(source, 15).Trim

            result.Content = GetElement(source, 13)
            Dim paymentCode As String = GetElement(source, 12).Trim
            If Not StringIsNullOrEmpty(paymentCode) Then
                result.Content = String.Format(My.Resources.Documents_BankOperationItem_ContentWithPaymentCode, _
                    result.Content, paymentCode)
            End If
            result.Content = GetLimitedLengthString(result.Content, 255)

            result.Inflow = (GetElement(source, 6).Trim.ToUpper = "C")
            result.Currency = GetElement(source, 8).Trim.ToUpper
            If Not IsValidCurrency(result.Currency, True) Then
                result.Currency = GetCurrentCompany.BaseCurrency
            End If
            result.OriginalSum = CDblSafe(GetElement(source, 7).Trim, 2, 0) / 100
            result.SumLTL = CDblSafe(GetElement(source, 5).Trim, 2, 0) / 100
            result.SumInAccount = CDblSafe(GetElement(source, 4).Trim, 2, 0) / 100
            If Not CRound(result.OriginalSum) > 0 Then
                result.OriginalSum = result.SumInAccount
            End If
            result.UniqueCode = GetElement(source, 10).Trim

            Return result

        End Function

    End Class

End Namespace