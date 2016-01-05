
Imports System.Text.RegularExpressions

Namespace Documents.BankDataExchangeProviders

    ''' <summary>
    ''' Represents a converter object that converts ISO20022 standard v. camt.053
    ''' bank account statement data to the canonical format suitable for import.
    ''' </summary>
    ''' <remarks>Is used as a parameter for <see cref="BankOperationItemList.GetBankOperationItemList">BankOperationItemList.GetBankOperationItemList</see> method.
    ''' If detects invalid version automaticaly downgrades to <see cref="ISO20022v052BankAccountStatement">v. camt.052</see>.</remarks>
    <Serializable()> _
    Public Class ISO20022v053BankAccountStatement
        Inherits ISO20022v052BankAccountStatement

        Protected Overrides Sub LoadDataFromStringInt(ByVal source As String)

            If Not source.ToLower.Contains("urn:iso:std:iso:20022:tech:xsd:camt.053.001.02") Then
                ' if not the current version then downgrade to previous version
                MyBase.LoadDataFromStringInt(source)
                Exit Sub
            End If

            Dim document As System.Xml.XmlDocument = New System.Xml.XmlDocument()
            document.LoadXml(StripNamespaces(source))

            _AccountCurrency = GetISO20022ValueAsString(document, _
                "/Document/BkToCstmrStmt/Stmt/Acct/Ccy", True).Trim.ToUpper()
            _AccountIBAN = GetISO20022ValueAsString(document, _
                "/Document/BkToCstmrStmt/Stmt/Acct/Id/IBAN", True).Trim.ToUpper()
            _PeriodStart = GetISO20022ValueAsDate(document, _
                "/Document/BkToCstmrStmt/Stmt/FrToDt/FrDtTm", True)
            _PeriodEnd = GetISO20022ValueAsDate(document, _
                "/Document/BkToCstmrStmt/Stmt/FrToDt/ToDtTm", True)

            For Each n As Xml.XmlNode In GetISO20022NodeList(document, "/Document/BkToCstmrStmt/Stmt/Bal", True)
                Dim balanceType As String = GetISO20022ValueAsString(n, "./Tp/CdOrPrtry/Cd", True).Trim.ToUpper
                If balanceType = "CLBD" Then
                    _BalanceEnd = GetISO20022ValueAsDouble(n, "./Amt", 2, True)
                    If GetISO20022ValueAsEntryType(n, "./CdtDbtInd", True) = BookEntryType.Debetas Then
                        _BalanceEnd = -_BalanceEnd
                    End If
                ElseIf balanceType = "OPBD" Then
                    _BalanceStart = GetISO20022ValueAsDouble(n, "./Amt", 2, True)
                    If GetISO20022ValueAsEntryType(n, "./CdtDbtInd", True) = BookEntryType.Debetas Then
                        _BalanceStart = -_BalanceStart
                    End If
                End If
            Next

            Dim entries As New List(Of String)
            For Each n As Xml.XmlNode In GetISO20022NodeList(document, "/Document/BkToCstmrStmt/Stmt/Ntry", True)
                entries.Add(n.OuterXml)
            Next

            If entries.Count < 1 Then
                Throw New Exception(My.Resources.Documents_BankDataExchangeProviders_ISO20022v052BankAccountStatement_NoOperations)
            End If

            _Items = New List(Of BankAccountStatementItem)

            For Each s As String In entries
                _Items.Add(GetBankAccountStatementItem(s))
            Next

        End Sub

        Private Function GetBankAccountStatementItem(ByVal source As String) As BankAccountStatementItem

            Dim result As New BankAccountStatementItem

            Dim document As System.Xml.XmlDocument = New System.Xml.XmlDocument()
            document.LoadXml(source)

            result.DocumentNumber = GetISO20022ValueAsString(document, _
                "/Ntry/NtryDtls/TxDtls/Refs/EndToEndId", False).Trim
            If result.DocumentNumber.Trim.ToUpper() = "NOTPROVIDED" Then result.DocumentNumber = ""
            If String.IsNullOrEmpty(result.DocumentNumber) Then
                result.DocumentNumber = GetISO20022ValueAsString(document, _
                "/Ntry/NtryDtls/TxDtls/Refs/InstrId", False).Trim
                If result.DocumentNumber.Trim.ToUpper() = "NOTPROVIDED" Then result.DocumentNumber = ""
                If String.IsNullOrEmpty(result.DocumentNumber) Then
                    result.DocumentNumber = GetISO20022ValueAsString(document, _
                        "/Ntry/NtryDtls/TxDtls/Refs/<PmtInfId", False).Trim
                    If result.DocumentNumber.Trim.ToUpper() = "NOTPROVIDED" Then result.DocumentNumber = ""
                End If
            End If
            result.UniqueCode = GetISO20022ValueAsString( _
                document, "/Ntry/NtryDtls/TxDtls/Refs/AcctSvcrRef", False)
            If StringIsNullOrEmpty(result.UniqueCode) Then
                result.UniqueCode = GetISO20022ValueAsString( _
                    document, "/Ntry/NtryDtls/TxDtls/Refs/TxId", True)
            End If
            result.Date = GetISO20022ValueAsDate(document, "/Ntry/BookgDt/Dt", False)
            If result.Date = Date.MinValue Then
                result.Date = GetISO20022ValueAsDate(document, "/Ntry/BookgDt/DtTm", True)
            End If
            result.Inflow = (GetISO20022ValueAsEntryType(document, "/Ntry/CdtDbtInd", True) _
                = BookEntryType.Kreditas)

            result.Content = GetISO20022ValueAsString( _
                document, "/Ntry/NtryDtls/TxDtls/RmtInf/Ustrd", False)
            Dim paymentCode As String = GetISO20022ValueAsString( _
                document, "/Ntry/NtryDtls/TxDtls/RmtInf/Strd/CdtrRefInf/Ref", False)
            If Not StringIsNullOrEmpty(paymentCode) Then
                result.Content = String.Format(My.Resources.Documents_BankOperationItem_ContentWithPaymentCode, _
                    result.Content, paymentCode)
            End If
            result.Content = GetLimitedLengthString(result.Content, 255)

            If result.Inflow Then

                result.PersonCode = GetISO20022ValueAsString(document, _
                    "/Ntry/NtryDtls/TxDtls/RltdPties/Dbtr/Id/PrvtId/Othr/Id", False)
                If StringIsNullOrEmpty(result.PersonCode) Then
                    result.PersonCode = GetISO20022ValueAsString(document, _
                        "/Ntry/NtryDtls/TxDtls/RltdPties/Dbtr/Id/OrgId/Othr/Id", False)
                End If
                result.PersonName = GetISO20022ValueAsString(document, _
                    "/Ntry/NtryDtls/TxDtls/RltdPties/Dbtr/Nm", False)
                result.PersonBankAccount = GetISO20022ValueAsString(document, _
                    "/Ntry/NtryDtls/TxDtls/RltdPties/DbtrAcct/Id/IBAN", False)
                result.PersonBankName = GetISO20022ValueAsString(document, _
                    "/Ntry/NtryDtls/TxDtls/RltdAgts/DbtrAgt/FinInstnId/Nm", False)

            Else

                result.PersonCode = GetISO20022ValueAsString(document, _
                    "/Ntry/NtryDtls/TxDtls/RltdPties/Cdtr/Id/PrvtId/Othr/Id", False)
                If StringIsNullOrEmpty(result.PersonCode) Then
                    result.PersonCode = GetISO20022ValueAsString(document, _
                        "/Ntry/NtryDtls/TxDtls/RltdPties/Cdtr/Id/OrgId/Othr/Id", False)
                End If
                result.PersonName = GetISO20022ValueAsString(document, _
                    "/Ntry/NtryDtls/TxDtls/RltdPties/Cdtr/Nm", False)
                result.PersonBankAccount = GetISO20022ValueAsString(document, _
                    "/Ntry/NtryDtls/TxDtls/RltdPties/CdtrAcct/Id/IBAN", False)
                result.PersonBankName = GetISO20022ValueAsString(document, _
                    "/Ntry/NtryDtls/TxDtls/RltdAgts/CdtrAgt/FinInstnId/Nm", False)

            End If

            result.Currency = GetISO20022AttributeAsString(document, _
                "/Ntry/NtryDtls/TxDtls/AmtDtls/InstdAmt/Amt", "Ccy", False)
            If StringIsNullOrEmpty(result.Currency) OrElse Not IsValidCurrency(result.Currency, True) Then
                result.Currency = GetCurrentCompany.BaseCurrency
            End If
            result.OriginalSum = GetISO20022ValueAsDouble(document, _
                "/Ntry/NtryDtls/TxDtls/AmtDtls/InstdAmt/Amt", 2, False)
            result.SumLTL = GetISO20022ValueAsDouble(document, _
                "/Ntry/NtryDtls/TxDtls/AmtDtls/PrtryAmt/Amt", 2, False)
            result.SumInAccount = GetISO20022ValueAsDouble(document, _
                "/Ntry/NtryDtls/TxDtls/AmtDtls/TxAmt/Amt", 2, False)

            If Not CRound(result.OriginalSum) > 0 Then
                result.OriginalSum = result.SumInAccount
            End If

            Return result

        End Function

    End Class

End Namespace