
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
            ElseIf data.BkToCstmrStmt.Stmt(0).FrToDt Is Nothing Then
                Throw New Exception(String.Format(My.Resources.Documents_BankDataExchangeProviders_ISO20022v052BankAccountStatement_InvalidFileFormatNodeMissing, "BkToCstmrStmt.Stmt.FrToDt"))
            ElseIf data.BkToCstmrStmt.Stmt(0).Bal Is Nothing Then
                Throw New Exception(String.Format(My.Resources.Documents_BankDataExchangeProviders_ISO20022v052BankAccountStatement_InvalidFileFormatNodeMissing, "BkToCstmrStmt.Stmt.Bal"))
            ElseIf data.BkToCstmrStmt.Stmt(0).Ntry Is Nothing OrElse data.BkToCstmrStmt.Stmt(0).Ntry.Length < 1 Then
                Throw New Exception(My.Resources.Documents_BankDataExchangeProviders_ISO20022v052BankAccountStatement_NoOperations)
            End If

            _AccountCurrency = data.BkToCstmrStmt.Stmt(0).Acct.Ccy
            _AccountIBAN = data.BkToCstmrStmt.Stmt(0).Acct.Id.Item.ToString
            _PeriodStart = data.BkToCstmrStmt.Stmt(0).FrToDt.FrDtTm
            _PeriodEnd = data.BkToCstmrStmt.Stmt(0).FrToDt.ToDtTm

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

            _Items = New List(Of BankAccountStatementItem)

            For Each entry As camt_053_001_02.ReportEntry2 In data.BkToCstmrStmt.Stmt(0).Ntry
                _Items.Add(GetBankAccountStatementItem(entry))
            Next

        End Sub

        Private Function GetBankAccountStatementItem(entry As camt_053_001_02.ReportEntry2) As BankAccountStatementItem

            If entry.NtryDtls Is Nothing OrElse entry.NtryDtls.Length < 1 Then
                Throw New Exception(String.Format(My.Resources.Documents_BankDataExchangeProviders_ISO20022v052BankAccountStatement_InvalidFileFormatNodeMissing, "NtryDtls"))
            ElseIf entry.NtryDtls(0).TxDtls Is Nothing OrElse entry.NtryDtls(0).TxDtls.Length < 1 Then
                Throw New Exception(String.Format(My.Resources.Documents_BankDataExchangeProviders_ISO20022v052BankAccountStatement_InvalidFileFormatNodeMissing, "NtryDtls.TxDtls"))
            ElseIf entry.NtryDtls(0).TxDtls(0).Refs Is Nothing Then
                Throw New Exception(String.Format(My.Resources.Documents_BankDataExchangeProviders_ISO20022v052BankAccountStatement_InvalidFileFormatNodeMissing, "NtryDtls.TxDtls.Refs"))
            ElseIf entry.BookgDt Is Nothing Then
                Throw New Exception(String.Format(My.Resources.Documents_BankDataExchangeProviders_ISO20022v052BankAccountStatement_InvalidFileFormatNodeMissing, "BookgDt"))
            ElseIf entry.NtryDtls(0).TxDtls(0).RmtInf Is Nothing Then
                Throw New Exception(String.Format(My.Resources.Documents_BankDataExchangeProviders_ISO20022v052BankAccountStatement_InvalidFileFormatNodeMissing, "NtryDtls.TxDtls.RmtInf"))
            ElseIf entry.NtryDtls(0).TxDtls(0).RltdPties Is Nothing Then
                Throw New Exception(String.Format(My.Resources.Documents_BankDataExchangeProviders_ISO20022v052BankAccountStatement_InvalidFileFormatNodeMissing, "entry.NtryDtls.TxDtls.RltdPties"))
            ElseIf entry.NtryDtls(0).TxDtls(0).AmtDtls Is Nothing Then
                Throw New Exception(String.Format(My.Resources.Documents_BankDataExchangeProviders_ISO20022v052BankAccountStatement_InvalidFileFormatNodeMissing, "NtryDtls.TxDtls.AmtDtls"))
            End If

            Dim result As New BankAccountStatementItem

            result.DocumentNumber = entry.NtryDtls(0).TxDtls(0).Refs.EndToEndId
            If result.DocumentNumber.Trim.ToUpper() = NotProvidedPlaceHolder Then _
                result.DocumentNumber = String.Empty
            If String.IsNullOrEmpty(result.DocumentNumber.Trim) Then
                result.DocumentNumber = entry.NtryDtls(0).TxDtls(0).Refs.InstrId
                If result.DocumentNumber.Trim.ToUpper() = NotProvidedPlaceHolder Then _
                    result.DocumentNumber = String.Empty
                If String.IsNullOrEmpty(result.DocumentNumber.Trim) Then
                    result.DocumentNumber = entry.NtryDtls(0).TxDtls(0).Refs.PmtInfId
                    If result.DocumentNumber.Trim.ToUpper() = NotProvidedPlaceHolder Then _
                        result.DocumentNumber = String.Empty
                End If
            End If
            result.UniqueCode = entry.NtryDtls(0).TxDtls(0).Refs.AcctSvcrRef
            If StringIsNullOrEmpty(result.UniqueCode) Then _
                result.UniqueCode = entry.NtryDtls(0).TxDtls(0).Refs.TxId
            result.Date = entry.BookgDt.Item
            result.Inflow = (entry.CdtDbtInd = camt_053_001_02.CreditDebitCode.CRDT)
            If Not entry.NtryDtls(0).TxDtls(0).AmtDtls.TxAmt Is Nothing AndAlso
                    Not entry.NtryDtls(0).TxDtls(0).AmtDtls.TxAmt.Amt Is Nothing Then
                result.SumInAccount = entry.NtryDtls(0).TxDtls(0).AmtDtls.TxAmt.Amt.Value
            ElseIf Not entry.NtryDtls(0).TxDtls(0).AmtDtls.PrtryAmt Is Nothing AndAlso
                    entry.NtryDtls(0).TxDtls(0).AmtDtls.PrtryAmt.Length > 0 AndAlso
                    Not entry.NtryDtls(0).TxDtls(0).AmtDtls.PrtryAmt(0).Amt Is Nothing Then
                result.SumInAccount = entry.NtryDtls(0).TxDtls(0).AmtDtls.PrtryAmt(0).Amt.Value
            Else
                Throw New Exception(String.Format(My.Resources.Documents_BankDataExchangeProviders_ISO20022v052BankAccountStatement_InvalidFileFormatNodeMissing, "NtryDtls.TxDtls.AmtDtls.TxAmt/NtryDtls.TxDtls.AmtDtls.PrtryAmt"))
            End If
            If entry.NtryDtls(0).TxDtls(0).AmtDtls.InstdAmt Is Nothing OrElse
                 entry.NtryDtls(0).TxDtls(0).AmtDtls.InstdAmt.Amt Is Nothing Then
                result.Currency = GetCurrentCompany.BaseCurrency
                result.OriginalSum = result.SumInAccount
            Else
                result.Currency = entry.NtryDtls(0).TxDtls(0).AmtDtls.InstdAmt.Amt.Ccy
                If StringIsNullOrEmpty(result.Currency) OrElse Not IsValidCurrency(result.Currency, True) Then
                    result.Currency = GetCurrentCompany.BaseCurrency
                End If
                result.OriginalSum = entry.NtryDtls(0).TxDtls(0).AmtDtls.InstdAmt.Amt.Value
            End If
            ResolveSumBase(result)

            If Not entry.NtryDtls(0).TxDtls(0).RmtInf.Ustrd Is Nothing _
                AndAlso entry.NtryDtls(0).TxDtls(0).RmtInf.Ustrd.Length > 0 Then
                result.Content = String.Join(" ", entry.NtryDtls(0).TxDtls(0).RmtInf.Ustrd)
            End If
            Try
                Dim paymentCode As String = entry.NtryDtls(0).TxDtls(0).RmtInf.Strd(0).CdtrRefInf.Ref
                If Not StringIsNullOrEmpty(paymentCode) Then
                    result.Content = String.Format(My.Resources.Documents_BankOperationItem_ContentWithPaymentCode,
                        result.Content, paymentCode)
                End If
            Catch ex As Exception
            End Try
            result.Content = GetLimitedLengthString(result.Content, 255)

            If result.Inflow Then

                If Not entry.NtryDtls(0).TxDtls(0).RltdPties.Dbtr Is Nothing Then

                    Try
                        result.PersonCode = DirectCast(entry.NtryDtls(0).TxDtls(0).RltdPties.Dbtr.Id.Item,
                            camt_053_001_02.PersonIdentification5).Othr(0).Id
                        If StringIsNullOrEmpty(result.PersonCode) Then
                            result.PersonCode = DirectCast(entry.NtryDtls(0).TxDtls(0).RltdPties.Dbtr.Id.Item,
                                camt_053_001_02.OrganisationIdentification4).Othr(0).Id
                        End If
                    Catch ex As Exception
                        Try
                            result.PersonCode = DirectCast(entry.NtryDtls(0).TxDtls(0).RltdPties.Dbtr.Id.Item,
                                camt_053_001_02.OrganisationIdentification4).Othr(0).Id
                        Catch ex2 As Exception
                        End Try
                    End Try
                    result.PersonName = entry.NtryDtls(0).TxDtls(0).RltdPties.Dbtr.Nm
                    If Not entry.NtryDtls(0).TxDtls(0).RltdPties.DbtrAcct Is Nothing AndAlso
                        Not entry.NtryDtls(0).TxDtls(0).RltdPties.DbtrAcct.Id Is Nothing AndAlso
                        Not entry.NtryDtls(0).TxDtls(0).RltdPties.DbtrAcct.Id.Item Is Nothing Then _
                        result.PersonBankAccount = entry.NtryDtls(0).TxDtls(0).RltdPties.DbtrAcct.Id.Item.ToString
                    If Not entry.NtryDtls(0).TxDtls(0).RltdAgts Is Nothing AndAlso
                        Not entry.NtryDtls(0).TxDtls(0).RltdAgts.DbtrAgt Is Nothing AndAlso
                        Not entry.NtryDtls(0).TxDtls(0).RltdAgts.DbtrAgt.FinInstnId Is Nothing Then _
                        result.PersonBankName = entry.NtryDtls(0).TxDtls(0).RltdAgts.DbtrAgt.FinInstnId.Nm

                End If

            Else

                If Not entry.NtryDtls(0).TxDtls(0).RltdPties.Cdtr Is Nothing Then

                    Try
                        result.PersonCode = DirectCast(entry.NtryDtls(0).TxDtls(0).RltdPties.Cdtr.Id.Item,
                            camt_053_001_02.PersonIdentification5).Othr(0).Id
                        If StringIsNullOrEmpty(result.PersonCode) Then
                            result.PersonCode = DirectCast(entry.NtryDtls(0).TxDtls(0).RltdPties.Cdtr.Id.Item,
                                camt_053_001_02.OrganisationIdentification4).Othr(0).Id
                        End If
                    Catch ex As Exception
                        Try
                            result.PersonCode = DirectCast(entry.NtryDtls(0).TxDtls(0).RltdPties.Cdtr.Id.Item,
                                camt_053_001_02.OrganisationIdentification4).Othr(0).Id
                        Catch ex2 As Exception
                        End Try
                    End Try

                    result.PersonName = entry.NtryDtls(0).TxDtls(0).RltdPties.Cdtr.Nm
                    If Not entry.NtryDtls(0).TxDtls(0).RltdPties.CdtrAcct Is Nothing AndAlso
                        Not entry.NtryDtls(0).TxDtls(0).RltdPties.CdtrAcct.Id Is Nothing AndAlso
                        Not entry.NtryDtls(0).TxDtls(0).RltdPties.CdtrAcct.Id.Item Is Nothing Then _
                        result.PersonBankAccount = entry.NtryDtls(0).TxDtls(0).RltdPties.CdtrAcct.Id.Item.ToString
                    If Not entry.NtryDtls(0).TxDtls(0).RltdAgts Is Nothing AndAlso
                        Not entry.NtryDtls(0).TxDtls(0).RltdAgts.CdtrAgt Is Nothing AndAlso
                        Not entry.NtryDtls(0).TxDtls(0).RltdAgts.CdtrAgt.FinInstnId Is Nothing Then _
                        result.PersonBankName = entry.NtryDtls(0).TxDtls(0).RltdAgts.CdtrAgt.FinInstnId.Nm

                End If

            End If

            Return result

        End Function

    End Class

End Namespace