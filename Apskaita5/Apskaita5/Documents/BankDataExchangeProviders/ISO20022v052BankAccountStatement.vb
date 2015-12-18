Namespace Documents.BankDataExchangeProviders

    ''' <summary>
    ''' Represents a converter object that converts ISO20022 standard v. camt.052
    ''' bank account statement data to the canonical format suitable for import.
    ''' </summary>
    ''' <remarks>Is used as a parameter for <see cref="BankOperationItemList.GetBankOperationItemList">BankOperationItemList.GetBankOperationItemList</see> method.</remarks>
    <Serializable()> _
    Public Class ISO20022v052BankAccountStatement
        Implements IBankAccountStatement

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
                Return String.Format(My.Resources.Documents_BankDataExchangeProviders_ISO20022v052BankAccountStatement_Description, _
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
        ''' <param name="encoding">A file encoding if it is known to be not standard (UTF-8) for a given file type.</param>
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
                encoding = System.Text.Encoding.UTF8
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

            LoadDataFromStringInt(source)

            _Income = 0
            _Spendings = 0
            For Each i As BankAccountStatementItem In Items
                If i.Inflow Then
                    _Income = CRound(_Income + i.SumInAccount, 2)
                Else
                    _Spendings = CRound(_Income + i.SumInAccount, 2)
                End If
            Next

        End Sub

        Protected Overridable Sub LoadDataFromStringInt(ByVal source As String)

            Dim dirtyDocumentTag As String = "<Document xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""urn:iso:std:iso:20022:tech:xsd:camt.052.001.02"">"
            Dim dirtyDocumentTagAlt As String = "<Document xmlns=""urn:iso:std:iso:20022:tech:xsd:camt.052.001.02"">"
            Dim cleanDocumentTag As String = "<Document>"

            If source.Contains(dirtyDocumentTag) Then
                source = source.Replace(dirtyDocumentTag, cleanDocumentTag)
            ElseIf source.Contains(dirtyDocumentTagAlt) Then
                source = source.Replace(dirtyDocumentTagAlt, cleanDocumentTag)
            Else
                ' minimum implemented version, no downgrade options available
                Throw New Exception(My.Resources.Documents_BankDataExchangeProviders_ISO20022v052BankAccountStatement_InvalidFileFormat)
            End If

            Dim document As System.Xml.XmlDocument = New System.Xml.XmlDocument()
            document.LoadXml(source)

            _AccountCurrency = GetISO20022ValueAsString(document, _
                "/Document/BkToCstmrAcctRpt/Rpt/Acct/Ccy", True).Trim.ToUpper()
            _AccountIBAN = GetISO20022ValueAsString(document, _
                "/Document/BkToCstmrAcctRpt/Rpt/Acct/Id/IBAN", True).Trim.ToUpper()
            _PeriodStart = GetISO20022ValueAsDate(document, _
                "/Document/BkToCstmrAcctRpt/Rpt/FrToDt/FrDtTm", True)
            _PeriodEnd = GetISO20022ValueAsDate(document, _
                "/Document/BkToCstmrAcctRpt/Rpt/FrToDt/ToDtTm", True)

            For Each n As Xml.XmlNode In GetISO20022NodeList(document, "/Document/BkToCstmrAcctRpt/Rpt/Bal", True)
                Dim balanceType As String = GetISO20022ValueAsString(n, "./Tp/CdOrPrtry/Cd", False).Trim.ToUpper
                If balanceType = "ITBD" Then
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
            For Each n As Xml.XmlNode In GetISO20022NodeList(document, "/Document/BkToCstmrAcctRpt/Rpt/Ntry", True)
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
                document, "/Ntry/NtryDtls/TxDtls/Refs/AcctSvcrRef", True)
            result.Date = GetISO20022ValueAsDate(document, "/Ntry/BookgDt/Dt", True)
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
            result.SumInAccount = GetISO20022ValueAsDouble(document, _
                "/Ntry/NtryDtls/TxDtls/AmtDtls/TxAmt/Amt", 2, False)

            If Not CRound(result.OriginalSum) > 0 Then
                result.OriginalSum = result.SumInAccount
            End If
            If IsBaseCurrency(_AccountCurrency, GetCurrentCompany.BaseCurrency) Then
                result.SumLTL = result.SumInAccount
            ElseIf IsBaseCurrency(result.Currency, GetCurrentCompany.BaseCurrency) Then
                result.SumLTL = result.OriginalSum
            Else
                result.SumLTL = 0
            End If

            Return result

        End Function


        Protected Shared Function GetISO20022ValueAsString(ByVal node As System.Xml.XmlNode, _
            ByVal xpath As String, ByVal throwOnNotFound As Boolean) As String

            Try
                Return node.SelectSingleNode(xpath).InnerText
            Catch ex As Exception
                If throwOnNotFound Then
                    Throw New Exception(String.Format(My.Resources.Documents_BankDataExchangeProviders_ISO20022v052BankAccountStatement_InvalidFileFormatNodeMissing, xpath), ex)
                End If
            End Try

            Return ""

        End Function

        Protected Shared Function GetISO20022ValueAsDate(ByVal node As System.Xml.XmlNode, _
            ByVal xpath As String, ByVal throwOnNotFound As Boolean) As Date

            Try
                Return DateTime.Parse(node.SelectSingleNode(xpath).InnerText, _
                    System.Globalization.CultureInfo.InvariantCulture)
            Catch ex As Exception
                If throwOnNotFound Then
                    Throw New Exception(String.Format(My.Resources.Documents_BankDataExchangeProviders_ISO20022v052BankAccountStatement_InvalidFileFormatNodeMissing, xpath), ex)
                End If
            End Try

            Return Date.MinValue

        End Function

        Protected Shared Function GetISO20022ValueAsDouble(ByVal node As System.Xml.XmlNode, _
            ByVal xpath As String, ByVal roundOrder As Integer, ByVal throwOnNotFound As Boolean) As Double

            Try
                Return CRound(Double.Parse(node.SelectSingleNode(xpath).InnerText, _
                    System.Globalization.CultureInfo.InvariantCulture), roundOrder)
            Catch ex As Exception
                If throwOnNotFound Then
                    Throw New Exception(String.Format(My.Resources.Documents_BankDataExchangeProviders_ISO20022v052BankAccountStatement_InvalidFileFormatNodeMissing, xpath), ex)
                End If
            End Try

            Return 0

        End Function

        Protected Shared Function GetISO20022ValueAsEntryType(ByVal node As System.Xml.XmlNode, _
            ByVal xpath As String, ByVal throwOnNotFound As Boolean) As BookEntryType

            Try
                Dim code As String = node.SelectSingleNode(xpath).InnerText
                Dim result As BookEntryType = BookEntryType.Debetas
                If code.Trim.ToUpper() = "CRDT" Then result = BookEntryType.Kreditas
                Return result
            Catch ex As Exception
                If throwOnNotFound Then
                    Throw New Exception(String.Format(My.Resources.Documents_BankDataExchangeProviders_ISO20022v052BankAccountStatement_InvalidFileFormatNodeMissing, xpath), ex)
                End If
            End Try

            Return ""

        End Function

        Protected Shared Function GetISO20022NodeList(ByVal node As System.Xml.XmlNode, _
            ByVal xpath As String, ByVal throwOnNotFound As Boolean) As System.Xml.XmlNodeList

            Try
                Dim result As System.Xml.XmlNodeList = node.SelectNodes(xpath)
                If result Is Nothing Then
                    Throw New Exception(String.Format(My.Resources.Documents_BankDataExchangeProviders_ISO20022v052BankAccountStatement_InvalidFileFormatNodeMissing, xpath))
                End If
                Return result
            Catch ex As Exception
                If throwOnNotFound Then
                    Throw New Exception(String.Format(My.Resources.Documents_BankDataExchangeProviders_ISO20022v052BankAccountStatement_InvalidFileFormatNodeMissing, xpath), ex)
                End If
            End Try

            Return Nothing

        End Function

        Protected Shared Function GetISO20022AttributeAsString(ByVal node As System.Xml.XmlNode, _
            ByVal xpath As String, ByVal attributeName As String, ByVal throwOnNotFound As Boolean) As String

            Try
                Dim result As System.Xml.XmlNode = node.SelectSingleNode(xpath)
                If result Is Nothing Then
                    Throw New Exception(String.Format(My.Resources.Documents_BankDataExchangeProviders_ISO20022v052BankAccountStatement_InvalidFileFormatNodeMissing, xpath))
                End If
                result = result.Attributes.GetNamedItem(attributeName)
                If result Is Nothing Then
                    Throw New Exception(String.Format(My.Resources.Documents_BankDataExchangeProviders_ISO20022v052BankAccountStatement_InvalidFileFormatNodeMissing, xpath & ":" & attributeName))
                End If
                Return result.Value
            Catch ex As Exception
                If throwOnNotFound Then
                    Throw New Exception(String.Format(My.Resources.Documents_BankDataExchangeProviders_ISO20022v052BankAccountStatement_InvalidFileFormatNodeMissing, xpath & ":" & attributeName), ex)
                End If
            End Try

            Return ""

        End Function

    End Class

End Namespace