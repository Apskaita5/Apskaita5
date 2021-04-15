
Imports System.Text.RegularExpressions

Namespace Documents.BankDataExchangeProviders

    ''' <summary>
    ''' Represents a converter object that converts ISO20022 standard v. camt.053
    ''' bank account statement data to the canonical format suitable for import.
    ''' </summary>
    ''' <remarks>Is used as a parameter for <see cref="BankOperationItemList.GetBankOperationItemList">BankOperationItemList.GetBankOperationItemList</see> method.
    ''' If detects invalid version automaticaly downgrades to <see cref="ISO20022v052BankAccountStatement">v. camt.052</see>.</remarks>
    <Serializable()>
    Public Class ISO20022v053BankAccountStatement
        Inherits ISO20022v052BankAccountStatement

        Protected Overrides Sub LoadDataFromStringInt(ByVal source As String)

            Dim data As camt_053_001_02.Document = Nothing
            Try
                data = FromXmlString(Of camt_053_001_02.Document)(source, New Text.UTF8Encoding(False))
            Catch ex As Exception
                Try
                    data = FromXmlString(Of camt_053_001_02.Document)(source, New Text.UTF8Encoding(True))
                Catch ex2 As Exception
                    MyBase.LoadDataFromStringInt(source)
                    Exit Sub
                End Try
            End Try
            If data Is Nothing Then
                Throw New Exception(My.Resources.Documents_BankDataExchangeProviders_ISO20022v053BankAccountStatement_DeserializedNull)
            End If

            If data.BkToCstmrStmt Is Nothing Then
                Throw New Exception(String.Format(My.Resources.Documents_BankDataExchangeProviders_ISO20022v052BankAccountStatement_InvalidFileFormatNodeMissing, "BkToCstmrStmt"))
            ElseIf data.BkToCstmrStmt.Stmt Is Nothing OrElse data.BkToCstmrStmt.Stmt.Length < 1 Then
                Throw New Exception(String.Format(My.Resources.Documents_BankDataExchangeProviders_ISO20022v052BankAccountStatement_InvalidFileFormatNodeMissing, "BkToCstmrStmt.Stmt"))
            ElseIf data.BkToCstmrStmt.Stmt(0).Acct Is Nothing Then
                Throw New Exception(String.Format(My.Resources.Documents_BankDataExchangeProviders_ISO20022v052BankAccountStatement_InvalidFileFormatNodeMissing, "BkToCstmrStmt.Stmt.Acct"))
            ElseIf data.BkToCstmrStmt.Stmt(0).Acct.id Is Nothing Then
                Throw New Exception(String.Format(My.Resources.Documents_BankDataExchangeProviders_ISO20022v052BankAccountStatement_InvalidFileFormatNodeMissing, "BkToCstmrStmt.Stmt.Acct.ID"))
            ElseIf data.BkToCstmrStmt.Stmt(0).Acct.Id.Item Is Nothing Then
                Throw New Exception(String.Format(My.Resources.Documents_BankDataExchangeProviders_ISO20022v052BankAccountStatement_InvalidFileFormatNodeMissing, "BkToCstmrStmt.Stmt.Acct.Id.Item"))
            ElseIf data.BkToCstmrStmt.Stmt(0).Bal Is Nothing Then
                Throw New Exception(String.Format(My.Resources.Documents_BankDataExchangeProviders_ISO20022v052BankAccountStatement_InvalidFileFormatNodeMissing, "BkToCstmrStmt.Stmt.Bal"))
            ElseIf data.BkToCstmrStmt.Stmt(0).Ntry Is Nothing OrElse data.BkToCstmrStmt.Stmt(0).Ntry.Length < 1 Then
                Throw New Exception(My.Resources.Documents_BankDataExchangeProviders_ISO20022v052BankAccountStatement_NoOperations)
            End If

            _AccountCurrency = data.BkToCstmrStmt.Stmt(0).Acct.Ccy
            _AccountIBAN = data.BkToCstmrStmt.Stmt(0).Acct.Id.Item.ToString

            If Not data.BkToCstmrStmt.Stmt(0).FrToDt Is Nothing Then
                _PeriodStart = data.BkToCstmrStmt.Stmt(0).FrToDt.FrDtTm
                _PeriodEnd = data.BkToCstmrStmt.Stmt(0).FrToDt.ToDtTm
            End If

            For Each balance As camt_053_001_02.CashBalance3 In data.BkToCstmrStmt.Stmt(0).Bal
                If Not balance.Tp Is Nothing AndAlso Not balance.Tp.CdOrPrtry Is Nothing _
                    AndAlso Not balance.Tp.CdOrPrtry.Item Is Nothing _
                    AndAlso Not balance.Amt Is Nothing Then
                    If balance.Tp.CdOrPrtry.Item.ToString.Trim.ToUpper = "CLBD" Then
                        _BalanceEnd = balance.Amt.Value
                        If balance.CdtDbtInd = camt_053_001_02.CreditDebitCode.DBIT Then
                            _BalanceEnd = -_BalanceEnd
                        End If
                    ElseIf balance.Tp.CdOrPrtry.Item.ToString.Trim.ToUpper = "OPBD" Then
                        _BalanceStart = balance.Amt.Value
                        If balance.CdtDbtInd = camt_053_001_02.CreditDebitCode.DBIT Then
                            _BalanceStart = -_BalanceEnd
                        End If
                    End If
                End If
            Next

            ' to identify bank fee when no entry content set
            Dim bankName As String = String.Empty
            Try
                bankName = data.BkToCstmrStmt.Stmt(0).Acct.Svcr.FinInstnId.Nm
            Catch ex As Exception
            End Try

            _Items = New List(Of BankAccountStatementItem)

            For Each entry As camt_053_001_02.ReportEntry2 In data.BkToCstmrStmt.Stmt(0).Ntry
                _Items.Add(GetBankAccountStatementItem(entry, bankName))
            Next

        End Sub

        Private Function GetBankAccountStatementItem(entry As camt_053_001_02.ReportEntry2,
            ByVal bankName As String) As BankAccountStatementItem

            If entry.NtryDtls Is Nothing OrElse entry.NtryDtls.Length < 1 Then
                Throw New Exception(String.Format(My.Resources.Documents_BankDataExchangeProviders_ISO20022v052BankAccountStatement_InvalidFileFormatNodeMissing, "NtryDtls"))
            ElseIf entry.NtryDtls(0).TxDtls Is Nothing OrElse entry.NtryDtls(0).TxDtls.Length < 1 Then
                Throw New Exception(String.Format(My.Resources.Documents_BankDataExchangeProviders_ISO20022v052BankAccountStatement_InvalidFileFormatNodeMissing, "NtryDtls.TxDtls"))
            ElseIf entry.BookgDt Is Nothing Then
                Throw New Exception(String.Format(My.Resources.Documents_BankDataExchangeProviders_ISO20022v052BankAccountStatement_InvalidFileFormatNodeMissing, "BookgDt (nėra operacijos datos)"))
                ' workaround for paysera, they only provide amount in entry.Amt node
            ElseIf entry.NtryDtls(0).TxDtls(0).AmtDtls Is Nothing AndAlso entry.Amt Is Nothing Then
                Throw New Exception(String.Format(My.Resources.Documents_BankDataExchangeProviders_ISO20022v052BankAccountStatement_InvalidFileFormatNodeMissing, "NtryDtls.TxDtls.AmtDtls/Amt (nėra operacijos sumos)"))
            End If

            Dim result As New BankAccountStatementItem

            result.DocumentNumber = ParseDocumentNumber(entry.NtryDtls(0).TxDtls(0).Refs)

            If Not entry.NtryDtls(0).TxDtls(0).Refs Is Nothing Then
                result.UniqueCode = entry.NtryDtls(0).TxDtls(0).Refs.AcctSvcrRef
                If StringIsNullOrEmpty(result.UniqueCode) Then result.UniqueCode = entry.NtryDtls(0).TxDtls(0).Refs.TxId
            End If

            result.Date = entry.BookgDt.Item
            result.Content = ParseContent(entry.NtryDtls(0).TxDtls(0), bankName)
            result.Inflow = (entry.CdtDbtInd = camt_053_001_02.CreditDebitCode.CRDT)

            ParseSum(entry.NtryDtls(0).TxDtls(0).AmtDtls, entry.Amt, result)
            ResolveSumBase(result)

            ParseRelatedParties(entry.NtryDtls(0).TxDtls(0).RltdPties, result)
            result.PersonBankName = ParsePersonBank(entry.NtryDtls(0).TxDtls(0).RltdAgts, result.Inflow)

            Return result

        End Function

        Private Sub ParseSum(data As camt_053_001_02.AmountAndCurrencyExchange3,
            fallbackData As camt_053_001_02.ActiveOrHistoricCurrencyAndAmount, result As BankAccountStatementItem)

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

        Private Function ParseDocumentNumber(data As camt_053_001_02.TransactionReferences2) As String

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

        Private Function ParseContent(data As camt_053_001_02.EntryTransaction2, bankName As String) As String

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

        Private Sub ParseRelatedParties(data As camt_053_001_02.TransactionParty2, result As BankAccountStatementItem)

            If data Is Nothing Then Exit Sub

            If result.Inflow Then

                If Not data.Dbtr Is Nothing Then

                    Try
                        result.PersonCode = DirectCast(data.Dbtr.Id.Item,
                            camt_053_001_02.PersonIdentification5).Othr(0).Id
                        If StringIsNullOrEmpty(result.PersonCode) Then
                            result.PersonCode = DirectCast(data.Dbtr.Id.Item,
                                camt_053_001_02.OrganisationIdentification4).Othr(0).Id
                        End If
                    Catch ex As Exception
                        Try
                            result.PersonCode = DirectCast(data.Dbtr.Id.Item,
                                camt_053_001_02.OrganisationIdentification4).Othr(0).Id
                        Catch ex2 As Exception
                        End Try
                    End Try
                    result.PersonName = data.Dbtr.Nm
                    If Not data.DbtrAcct Is Nothing AndAlso Not data.DbtrAcct.Id Is Nothing AndAlso
                        Not data.DbtrAcct.Id.Item Is Nothing Then

                        If TypeOf data.DbtrAcct.Id.Item Is String Then
                            result.PersonBankAccount = data.DbtrAcct.Id.Item.ToString
                        ElseIf TypeOf data.DbtrAcct.Id.Item Is camt_053_001_02.GenericAccountIdentification1 Then
                            result.PersonBankAccount = DirectCast(data.DbtrAcct.Id.Item,
                                camt_053_001_02.GenericAccountIdentification1).Id
                        End If

                    End If

                End If

            Else

                If Not data.Cdtr Is Nothing Then

                    Try
                        result.PersonCode = DirectCast(data.Cdtr.Id.Item,
                            camt_053_001_02.PersonIdentification5).Othr(0).Id
                        If StringIsNullOrEmpty(result.PersonCode) Then
                            result.PersonCode = DirectCast(data.Cdtr.Id.Item,
                                camt_053_001_02.OrganisationIdentification4).Othr(0).Id
                        End If
                    Catch ex As Exception
                        Try
                            result.PersonCode = DirectCast(data.Cdtr.Id.Item,
                                camt_053_001_02.OrganisationIdentification4).Othr(0).Id
                        Catch ex2 As Exception
                        End Try
                    End Try

                    result.PersonName = data.Cdtr.Nm
                    If Not data.CdtrAcct Is Nothing AndAlso Not data.CdtrAcct.Id Is Nothing AndAlso
                        Not data.CdtrAcct.Id.Item Is Nothing Then

                        If TypeOf data.CdtrAcct.Id.Item Is String Then
                            result.PersonBankAccount = data.CdtrAcct.Id.Item.ToString
                        ElseIf TypeOf data.CdtrAcct.Id.Item Is camt_053_001_02.GenericAccountIdentification1 Then
                            result.PersonBankAccount = DirectCast(data.CdtrAcct.Id.Item,
                                camt_053_001_02.GenericAccountIdentification1).Id
                        End If

                    End If

                End If

            End If

        End Sub

        Private Function ParsePersonBank(data As camt_053_001_02.TransactionAgents2, inflow As Boolean) As String

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

    End Class

End Namespace
