Imports System.Text
Imports System.IO

Namespace Documents.BankDataExchangeProviders

    ''' <summary>
    ''' Represents a converter object that converts ISO20022 standard v. camt.052
    ''' bank account statement data to the canonical format suitable for import.
    ''' </summary>
    ''' <remarks>Is used as a parameter for <see cref="BankOperationItemList.GetBankOperationItemList">BankOperationItemList.GetBankOperationItemList</see> method.</remarks>
    <Serializable()>
    Public Class ISO20022v052BankAccountStatement
        Implements IBankAccountStatement

        Protected Const NotProvidedPlaceHolder As String = "NOTPROVIDED"

        Protected _AccountCurrency As String = ""
        Protected _AccountIBAN As String = ""
        Protected _PeriodStart As Date = Today
        Protected _PeriodEnd As Date = Today
        Protected _BalanceStart As Double = 0
        Protected _BalanceEnd As Double = 0
        Protected _Income As Double = 0
        Protected _Spendings As Double = 0
        Protected _Items As List(Of BankAccountStatementItem) = Nothing


        ''' <summary>
        ''' Gets a human readable (localized) description of the bank data
        ''' format that the object handles.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property SourceType() As String _
            Implements IBankAccountStatement.SourceType
            Get
                Return My.Resources.Documents_BankDataExchangeProviders_ISO20022v052BankAccountStatement_SourceType
            End Get
        End Property

        ''' <summary>
        ''' Gets a description of an imported bank data file standard format, e.g. ISO20022 (*.xml).
        ''' </summary>
        ''' <remarks>returns 'ISO20022 File (*.xml)'.</remarks>
        Public ReadOnly Property FileFormatDescription() As String _
            Implements IBankAccountStatement.FileFormatDescription
            Get
                Return "ISO20022 File (*.xml)"
            End Get
        End Property

        ''' <summary>
        ''' Gets an imported bank data file standard extension.
        ''' </summary>
        ''' <remarks>Returns xml.</remarks>
        Public ReadOnly Property FileExtension() As String _
            Implements IBankAccountStatement.FileExtension
            Get
                Return "xml"
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
                Return String.Format(My.Resources.Documents_BankDataExchangeProviders_ISO20022v052BankAccountStatement_Description,
                    vbCrLf, _PeriodStart.ToString("yyyy-MM-dd"), _PeriodEnd.ToString("yyyy-MM-dd"),
                    _AccountCurrency.Trim.ToUpper, vbCrLf, DblParser(_BalanceStart),
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
        ''' <param name="encoding">A file encoding if it is known to be not standard (UTF-8) for a given file type.</param>
        ''' <remarks></remarks>
        Public Sub LoadDataFromFile(ByVal fileName As String,
            Optional ByVal encoding As System.Text.Encoding = Nothing) _
            Implements IBankAccountStatement.LoadDataFromFile

            If StringIsNullOrEmpty(fileName) Then
                Throw New Exception(My.Resources.Documents_BankOperationItemList_FileNameNull)
            ElseIf Not IO.File.Exists(fileName) Then
                Throw New Exception(String.Format(My.Resources.Documents_BankOperationItemList_FileNotFound, fileName))
            End If

            If encoding Is Nothing Then
                encoding = Encoding.UTF8
            End If

            Dim content As String = File.ReadAllText(fileName, encoding)

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

            LoadDataFromStringInt(source)

            _Income = 0
            _Spendings = 0
            For Each i As BankAccountStatementItem In Items
                If i.Inflow Then
                    _Income = CRound(_Income + i.SumInAccount, 2)
                Else
                    _Spendings = CRound(_Spendings + i.SumInAccount, 2)
                End If
            Next

        End Sub

        Protected Overridable Sub LoadDataFromStringInt(ByVal source As String)

            Dim data As camt_052_001_02.Document = Nothing
            Try
                data = FromXmlString(Of camt_052_001_02.Document)(source, New Text.UTF8Encoding(False))
            Catch ex As Exception
                Try
                    data = FromXmlString(Of camt_052_001_02.Document)(source, New Text.UTF8Encoding(True))
                Catch ex2 As Exception
                    Throw New Exception(My.Resources.Documents_BankDataExchangeProviders_ISO20022v052BankAccountStatement_InvalidFileFormat, ex)
                End Try
            End Try
            If data Is Nothing Then
                Throw New Exception(My.Resources.Documents_BankDataExchangeProviders_ISO20022v052BankAccountStatement_DeserializedNull)
            End If

            If data.BkToCstmrAcctRpt Is Nothing Then
                Throw New Exception(String.Format(My.Resources.Documents_BankDataExchangeProviders_ISO20022v052BankAccountStatement_InvalidFileFormatNodeMissing, "BkToCstmrAcctRpt"))
            ElseIf data.BkToCstmrAcctRpt.Rpt Is Nothing OrElse data.BkToCstmrAcctRpt.Rpt.Length < 1 Then
                Throw New Exception(String.Format(My.Resources.Documents_BankDataExchangeProviders_ISO20022v052BankAccountStatement_InvalidFileFormatNodeMissing, "BkToCstmrAcctRpt.Rpt"))
            ElseIf data.BkToCstmrAcctRpt.Rpt(0).Acct Is Nothing Then
                Throw New Exception(String.Format(My.Resources.Documents_BankDataExchangeProviders_ISO20022v052BankAccountStatement_InvalidFileFormatNodeMissing, "BkToCstmrAcctRpt.Rpt(0).Acct"))
            ElseIf data.BkToCstmrAcctRpt.Rpt(0).Acct.Id Is Nothing Then
                Throw New Exception(String.Format(My.Resources.Documents_BankDataExchangeProviders_ISO20022v052BankAccountStatement_InvalidFileFormatNodeMissing, "BkToCstmrAcctRpt.Rpt(0).Acct.Id"))
            ElseIf data.BkToCstmrAcctRpt.Rpt(0).Acct.Id.Item Is Nothing Then
                Throw New Exception(String.Format(My.Resources.Documents_BankDataExchangeProviders_ISO20022v052BankAccountStatement_InvalidFileFormatNodeMissing, "BkToCstmrAcctRpt.Rpt(0).Acct.Id.Item"))
            ElseIf data.BkToCstmrAcctRpt.Rpt(0).Bal Is Nothing Then
                Throw New Exception(String.Format(My.Resources.Documents_BankDataExchangeProviders_ISO20022v052BankAccountStatement_InvalidFileFormatNodeMissing, "BkToCstmrAcctRpt.Rpt(0).Bal"))
            ElseIf data.BkToCstmrAcctRpt.Rpt(0).Ntry Is Nothing OrElse data.BkToCstmrAcctRpt.Rpt(0).Ntry.Length < 1 Then
                Throw New Exception(My.Resources.Documents_BankDataExchangeProviders_ISO20022v052BankAccountStatement_NoOperations)
            End If

            _AccountCurrency = data.BkToCstmrAcctRpt.Rpt(0).Acct.Ccy
            _AccountIBAN = data.BkToCstmrAcctRpt.Rpt(0).Acct.Id.Item.ToString.Trim.ToUpper()

            If Not data.BkToCstmrAcctRpt.Rpt(0).FrToDt Is Nothing Then
                _PeriodStart = data.BkToCstmrAcctRpt.Rpt(0).FrToDt.FrDtTm
                _PeriodEnd = data.BkToCstmrAcctRpt.Rpt(0).FrToDt.ToDtTm
            End If

            For Each balance As camt_052_001_02.CashBalance3 In data.BkToCstmrAcctRpt.Rpt(0).Bal
                If Not balance.Tp Is Nothing AndAlso Not balance.Tp.CdOrPrtry Is Nothing _
                    AndAlso Not balance.Tp.CdOrPrtry.Item Is Nothing _
                    AndAlso Not balance.Amt Is Nothing Then
                    If balance.Tp.CdOrPrtry.Item.ToString.Trim.ToUpper = "ITBD" Then
                        _BalanceEnd = balance.Amt.Value
                        If balance.CdtDbtInd = camt_052_001_02.CreditDebitCode.DBIT Then
                            _BalanceEnd = -_BalanceEnd
                        End If
                    ElseIf balance.Tp.CdOrPrtry.Item.ToString.Trim.ToUpper = "OPBD" Then
                        _BalanceStart = balance.Amt.Value
                        If balance.CdtDbtInd = camt_052_001_02.CreditDebitCode.DBIT Then
                            _BalanceStart = -_BalanceEnd
                        End If
                    End If
                End If
            Next

            ' to identify bank fee when no entry content set
            Dim bankName As String = String.Empty
            Try
                bankName = data.BkToCstmrAcctRpt.Rpt(0).Acct.Svcr.FinInstnId.Nm
            Catch ex As Exception
            End Try

            _Items = New List(Of BankAccountStatementItem)

            For Each entry As camt_052_001_02.ReportEntry2 In data.BkToCstmrAcctRpt.Rpt(0).Ntry
                _Items.Add(GetBankAccountStatementItem(entry, bankName))
            Next

        End Sub

        Private Function GetBankAccountStatementItem(entry As camt_052_001_02.ReportEntry2, bankName As String) As BankAccountStatementItem

            If entry.NtryDtls Is Nothing OrElse entry.NtryDtls.Length < 1 Then
                Throw New Exception(String.Format(My.Resources.Documents_BankDataExchangeProviders_ISO20022v052BankAccountStatement_InvalidFileFormatNodeMissing, "NtryDtls"))
            ElseIf entry.NtryDtls(0).TxDtls Is Nothing OrElse entry.NtryDtls(0).TxDtls.Length < 1 Then
                Throw New Exception(String.Format(My.Resources.Documents_BankDataExchangeProviders_ISO20022v052BankAccountStatement_InvalidFileFormatNodeMissing, "NtryDtls.TxDtls"))
            ElseIf entry.BookgDt Is Nothing Then
                Throw New Exception(String.Format(My.Resources.Documents_BankDataExchangeProviders_ISO20022v052BankAccountStatement_InvalidFileFormatNodeMissing, "BookgDt"))
            ElseIf entry.NtryDtls(0).TxDtls(0).AmtDtls Is Nothing AndAlso entry.Amt Is Nothing Then
                Throw New Exception(String.Format(My.Resources.Documents_BankDataExchangeProviders_ISO20022v052BankAccountStatement_InvalidFileFormatNodeMissing, "NtryDtls.TxDtls.AmtDtls/Amt (nėra operacijos sumos)"))
            End If

            Dim result As New BankAccountStatementItem

            result.DocumentNumber = ParseDocumentNumber(entry.NtryDtls(0).TxDtls(0).Refs)
            result.Date = entry.BookgDt.Item
            result.Content = ParseContent(entry.NtryDtls(0).TxDtls(0), bankName)
            result.Inflow = (entry.CdtDbtInd = camt_052_001_02.CreditDebitCode.CRDT)
            If Not entry.NtryDtls(0).TxDtls(0).Refs Is Nothing Then
                result.UniqueCode = entry.NtryDtls(0).TxDtls(0).Refs.AcctSvcrRef
                If StringIsNullOrEmpty(result.UniqueCode) Then result.UniqueCode = entry.NtryDtls(0).TxDtls(0).Refs.TxId
            End If
            ParseSum(entry.NtryDtls(0).TxDtls(0).AmtDtls, entry.Amt, result)
            ResolveSumBase(result)

            ParseRelatedParties(entry.NtryDtls(0).TxDtls(0).RltdPties, result)
            result.PersonBankName = ParsePersonBank(entry.NtryDtls(0).TxDtls(0).RltdAgts, result.Inflow)

            Return result

        End Function

        Private Function ParseDocumentNumber(data As camt_052_001_02.TransactionReferences2) As String

            Dim result As String = data.EndToEndId
            If result Is Nothing Then result = String.Empty

            If result.Trim.ToUpper() = NotProvidedPlaceHolder Then result = String.Empty

            If StringIsNullOrEmpty(result) Then

                result = data.InstrId
                If result Is Nothing Then result = String.Empty

                If result.Trim.ToUpper() = NotProvidedPlaceHolder Then result = String.Empty

                If StringIsNullOrEmpty(result) Then

                    result = data.PmtInfId
                    If result Is Nothing Then result = String.Empty

                    If result.Trim.ToUpper() = NotProvidedPlaceHolder Then result = String.Empty

                End If

            End If

            Return result

        End Function

        Private Function ParseContent(data As camt_052_001_02.EntryTransaction2, bankName As String) As String

            Dim result As String = ""

            If Not data.RmtInf Is Nothing AndAlso Not data.RmtInf.Ustrd Is Nothing _
                AndAlso data.RmtInf.Ustrd.Length > 0 Then
                result = String.Join(" ", data.RmtInf.Ustrd)
            ElseIf Not data.RltdPties Is Nothing AndAlso Not data.RltdPties.Cdtr Is Nothing _
                AndAlso Not StringIsNullOrEmpty(data.RltdPties.Cdtr.Nm) _
                AndAlso data.RltdPties.Cdtr.Nm.Trim = bankName Then
                result = My.Resources.Documents_BankDataExchangeProviders_ISO20022v052BankAccountStatement_OperationContentBankFee
            Else
                result = My.Resources.Documents_BankDataExchangeProviders_ISO20022v052BankAccountStatement_OperationContentNull
            End If
            Try
                Dim paymentCode As String = data.RmtInf.Strd(0).CdtrRefInf.Ref
                If Not StringIsNullOrEmpty(paymentCode) Then
                    result = String.Format(My.Resources.Documents_BankOperationItem_ContentWithPaymentCode, result, paymentCode)
                End If
            Catch ex As Exception
            End Try

            Return GetLimitedLengthString(result, 255)

        End Function

        Private Sub ParseSum(data As camt_052_001_02.AmountAndCurrencyExchange3,
            fallbackData As camt_052_001_02.ActiveOrHistoricCurrencyAndAmount, result As BankAccountStatementItem)

            If data Is Nothing Then

                ' workaround for paysera, they only provide amount in entry.Amt node
                result.SumInAccount = fallbackData.Value
                result.Currency = fallbackData.Ccy
                If StringIsNullOrEmpty(result.Currency) OrElse Not IsValidCurrency(result.Currency, True) Then
                    result.Currency = GetCurrentCompany.BaseCurrency
                End If
                result.OriginalSum = fallbackData.Value
                Exit Sub

            End If

            If Not data.TxAmt Is Nothing AndAlso Not data.TxAmt.Amt Is Nothing Then
                result.SumInAccount = data.TxAmt.Amt.Value
            ElseIf Not data.PrtryAmt Is Nothing AndAlso data.PrtryAmt.Length > 0 AndAlso
                Not data.PrtryAmt(0).Amt Is Nothing Then
                result.SumInAccount = data.PrtryAmt(0).Amt.Value
            Else
                Throw New Exception(String.Format(My.Resources.Documents_BankDataExchangeProviders_ISO20022v052BankAccountStatement_InvalidFileFormatNodeMissing,
                    "NtryDtls.TxDtls.AmtDtls.TxAmt/NtryDtls.TxDtls.AmtDtls.PrtryAmt (operacijos suma sąskaitoje)"))
            End If

            If Not data.InstdAmt Is Nothing AndAlso Not data.InstdAmt.Amt Is Nothing Then

                result.Currency = data.InstdAmt.Amt.Ccy
                If StringIsNullOrEmpty(result.Currency) OrElse Not IsValidCurrency(result.Currency, True) Then
                    result.Currency = GetCurrentCompany.BaseCurrency
                End If
                result.OriginalSum = data.InstdAmt.Amt.Value
                Exit Sub

            End If

            result.Currency = GetCurrentCompany.BaseCurrency
            result.OriginalSum = result.SumInAccount

        End Sub

        Private Sub ParseRelatedParties(data As camt_052_001_02.TransactionParty2, result As BankAccountStatementItem)

            If data Is Nothing Then Exit Sub

            If result.Inflow Then

                If Not data.Dbtr Is Nothing Then

                    Try
                        result.PersonCode = DirectCast(data.Dbtr.Id.Item, camt_052_001_02.PersonIdentification5).Othr(0).Id
                        If StringIsNullOrEmpty(result.PersonCode) Then
                            result.PersonCode = DirectCast(data.Dbtr.Id.Item, camt_052_001_02.OrganisationIdentification4).Othr(0).Id
                        End If
                    Catch ex As Exception
                        Try
                            result.PersonCode = DirectCast(data.Dbtr.Id.Item, camt_052_001_02.OrganisationIdentification4).Othr(0).Id
                        Catch ex2 As Exception
                        End Try
                    End Try

                    result.PersonName = data.Dbtr.Nm

                    If Not data.DbtrAcct Is Nothing AndAlso Not data.DbtrAcct.Id Is Nothing _
                        AndAlso Not data.DbtrAcct.Id.Item Is Nothing Then

                        If TypeOf data.DbtrAcct.Id.Item Is String Then
                            result.PersonBankAccount = data.DbtrAcct.Id.Item.ToString
                        ElseIf TypeOf data.DbtrAcct.Id.Item Is camt_052_001_02.GenericAccountIdentification1 Then
                            result.PersonBankAccount = DirectCast(data.DbtrAcct.Id.Item,
                                camt_052_001_02.GenericAccountIdentification1).Id
                        End If

                    End If

                End If

            Else

                If Not data.Cdtr Is Nothing Then

                    Try
                        result.PersonCode = DirectCast(data.Cdtr.Id.Item, camt_052_001_02.PersonIdentification5).Othr(0).Id
                        If StringIsNullOrEmpty(result.PersonCode) Then
                            result.PersonCode = DirectCast(data.Cdtr.Id.Item,
                                camt_052_001_02.OrganisationIdentification4).Othr(0).Id
                        End If
                    Catch ex As Exception
                        Try
                            result.PersonCode = DirectCast(data.Cdtr.Id.Item,
                                camt_052_001_02.OrganisationIdentification4).Othr(0).Id
                        Catch ex2 As Exception
                        End Try
                    End Try

                    result.PersonName = data.Cdtr.Nm

                    If Not data.CdtrAcct Is Nothing AndAlso Not data.CdtrAcct.Id Is Nothing AndAlso
                        Not data.CdtrAcct.Id.Item Is Nothing Then

                        If TypeOf data.CdtrAcct.Id.Item Is String Then
                            result.PersonBankAccount = data.CdtrAcct.Id.Item.ToString
                        ElseIf TypeOf data.CdtrAcct.Id.Item Is camt_052_001_02.GenericAccountIdentification1 Then
                            result.PersonBankAccount = DirectCast(data.CdtrAcct.Id.Item,
                                camt_052_001_02.GenericAccountIdentification1).Id
                        End If

                    End If

                End If

            End If

        End Sub

        Private Function ParsePersonBank(data As camt_052_001_02.TransactionAgents2, inflow As Boolean) As String

            If data Is Nothing Then Return ""

            If inflow Then
                If Not data.DbtrAgt Is Nothing AndAlso Not data.DbtrAgt.FinInstnId Is Nothing Then _
                    Return data.DbtrAgt.FinInstnId.Nm
            Else
                If Not data.CdtrAgt Is Nothing AndAlso Not data.CdtrAgt.FinInstnId Is Nothing Then _
                    Return data.CdtrAgt.FinInstnId.Nm
            End If

            Return ""

        End Function

        Protected Sub ResolveSumBase(ByVal result As BankAccountStatementItem)

            If Not result.OriginalSum > 0 Then
                result.OriginalSum = result.SumInAccount
            End If
            If IsBaseCurrency(_AccountCurrency, GetCurrentCompany.BaseCurrency) Then
                result.SumLTL = result.SumInAccount
            ElseIf IsBaseCurrency(result.Currency, GetCurrentCompany.BaseCurrency) Then
                result.SumLTL = result.OriginalSum
            Else
                result.SumLTL = 0
            End If

        End Sub

    End Class

End Namespace
