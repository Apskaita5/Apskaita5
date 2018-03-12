Imports System.Text
Imports System.Xml.Serialization
Imports System.Xml
Imports ApskaitaObjects.ActiveReports.Declarations.SafTTemplates.SAFT_2_0
Imports ApskaitaObjects.My.Resources

Namespace ActiveReports.Declarations

    ''' <summary>
    ''' Represents an implementation of an <see cref="IAuditFileSAFT">IInvoiceRegisterSafT</see>
    ''' for a state tax inspectorate (VMI) SAF-T report version 1.1.
    ''' </summary>
    ''' <remarks>Object is responsible for exporting the <see cref="InvoiceInfoItemList">
    ''' invoice register report</see> data to XML format.</remarks>
    Public NotInheritable Class AuditDataFileSAFT_2_0
        Implements IAuditFileSAFT

        Private ReadOnly _NotSpecifiedPlaceHolder As String = "Duomenų nėra"
        Private ReadOnly _Name As String = "SAF-T v. 2.0"
        Private ReadOnly _ValidFrom As Date = Date.MinValue
        Private ReadOnly _ValidTo As Date = Date.MaxValue
        Private ReadOnly _XsdFileName As String = "MXFD\SAFT_2_0.xsd"
        Private ReadOnly _TargetNameSpace As String = "https://www.vmi.lt/cms/saf-t"


        ''' <summary>
        ''' Gets a name of the particular SAF-T audit data file version.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Name() As String _
            Implements IAuditFileSAFT.Name
            Get
                Return _Name
            End Get
        End Property

        ''' <summary>
        ''' Gets a start of the period that the SAF-T audit data file format is valid for.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property ValidFrom() As Date _
            Implements IAuditFileSAFT.ValidFrom
            Get
                Return _ValidFrom
            End Get
        End Property

        ''' <summary>
        ''' Gets an end of the period that the SAF-T audit data file is valid for.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property ValidTo() As Date _
            Implements IAuditFileSAFT.ValidTo
            Get
                Return _ValidTo
            End Get
        End Property

        ''' <summary>
        ''' Gets a name of the xsd file that defines the SAF-T audit data file xml requirements.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property XsdFileName() As String _
            Implements IAuditFileSAFT.XsdFileName
            Get
                Return _XsdFileName
            End Get
        End Property

        ''' <summary>
        ''' Gets a name of the xsd target namespace that is used to validate the generated xml.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property TargetNameSpace As String _
            Implements IAuditFileSAFT.TargetNameSpace
            Get
                Return _TargetNameSpace
            End Get
        End Property

        ''' <summary>
        ''' Gets an XML representation of the report.
        ''' </summary>
        ''' <param name="softwareVersion">a current version of the application</param>
        ''' <param name="periodStart">a starting date of the report data period</param>
        ''' <param name="periodEnd">an ending date of the report data period</param>
        ''' <param name="warnings">an out parameter that returns XML validation warnings</param>
        ''' <remarks></remarks>
        Public Function GetXmlString(ByVal softwareVersion As String, ByVal periodStart As Date,
            ByVal periodEnd As Date) As String Implements IAuditFileSAFT.GetXmlString

            Dim result As New AuditFile

            result.Header = GetHeader(softwareVersion, periodStart, periodEnd)

            result.MasterFiles = GetMasterFiles(periodStart, periodEnd)

            result.GeneralLedgerEntries = New AuditFileGeneralLedgerEntries
            result.GeneralLedgerEntries.Journal = New AuditFileGeneralLedgerEntriesJournal() {New AuditFileGeneralLedgerEntriesJournal}
            result.GeneralLedgerEntries.Journal(0).Description = "Bendrasis žurnalas"
            result.GeneralLedgerEntries.Journal(0).JournalID = "BŽ"
            result.GeneralLedgerEntries.Journal(0).Type = "BŽ"
            Dim transactionCount As Integer = 0
            Dim debitSum As Double = 0.0
            Dim creditSum As Double = 0.0
            result.GeneralLedgerEntries.Journal(0).Transaction = GetGeneralLedgerTransactions(
                periodStart, periodEnd, transactionCount, debitSum, creditSum)
            result.GeneralLedgerEntries.NumberOfEntries = transactionCount.ToString
            result.GeneralLedgerEntries.TotalDebit = New Nullable(Of Decimal)(Convert.ToDecimal(debitSum))
            result.GeneralLedgerEntries.TotalCredit = New Nullable(Of Decimal)(Convert.ToDecimal(creditSum))

            result.SourceDocuments = GetSourceDocuments(periodStart, periodEnd)

            Dim enc As Encoding = Encoding.UTF8
            Dim serializer As New XmlSerializer(result.GetType())
            Dim settings As New XmlWriterSettings

            settings.Indent = True
            settings.IndentChars = " "
            settings.Encoding = enc

            Dim resultString As String

            Using ms As New IO.MemoryStream
                Using writer As XmlWriter = XmlWriter.Create(ms, settings)
                    serializer.Serialize(writer, result)
                    resultString = enc.GetString(ms.ToArray())
                End Using
            End Using

            Return resultString

        End Function


        Public Overrides Function ToString() As String
            Return _Name
        End Function


        Private Function GetHeader(ByVal softwareVersion As String, ByVal periodStart As Date,
            ByVal periodEnd As Date) As AuditFileHeader

            Dim result As New AuditFileHeader()

            result.AuditFileCountry = StateCodeLith
            result.AuditFileDateCreated = Now
            result.AuditFileVersion = "2.00"
            result.DataType = AuditFileHeaderDataType.F
            result.DefaultCurrencyCode = GetCurrentCompany.BaseCurrency
            result.SoftwareCompanyName = "Marius Dagys"
            result.SoftwareID = "Apskaita5"
            result.SoftwareVersion = softwareVersion
            result.TaxAccountingBasis = AuditFileHeaderTaxAccountingBasis.K
            result.Entity = "FULLAUDITDATA"
            result.NumberOfParts = "1"
            result.PartNumber = "1"
            result.FiscalYearFrom = periodStart
            result.FiscalYearTo = periodEnd
            result.SelectionCriteria = New SelectionCriteriaStructure
            result.SelectionCriteria.Items = New Date() {periodStart, periodEnd}
            result.SelectionCriteria.ItemsElementName = New ItemsChoiceType() _
                {ItemsChoiceType.SelectionStartDate, ItemsChoiceType.SelectionEndDate}

            result.Company = New CompanyHeaderStructure()
            result.Company.Name = GetCurrentCompany.Name
            result.Company.RegistrationNumber = GetCurrentCompany.Code
            result.Company.Address = New AddressStructure() {New AddressStructure}
            result.Company.Address(0).AddressType = AddressStructureAddressType.BA
            result.Company.Address(0).Country = StateCodeLith
            result.Company.Address(0).FullAddress = GetCurrentCompany.Address
            result.Company.Address(0).City = _NotSpecifiedPlaceHolder
            result.Company.Address(0).Number = _NotSpecifiedPlaceHolder
            result.Company.Address(0).PostalCode = _NotSpecifiedPlaceHolder
            result.Company.Address(0).StreetName = _NotSpecifiedPlaceHolder
            result.Company.BankAccount = New BankAccountStructure() {New BankAccountStructure()}
            result.Company.BankAccount(0).ItemElementName = ItemChoiceType.IBANNumber
            result.Company.BankAccount(0).Item = GetCurrentCompany.BankAccount
            If String.IsNullOrEmpty(GetCurrentCompany.CodeVat.Trim) Then
                result.Company.TaxRegistration = New TaxIDStructure() {New TaxIDStructure()}
            Else
                result.Company.TaxRegistration = New TaxIDStructure() {New TaxIDStructure(), New TaxIDStructure()}
                result.Company.TaxRegistration(1).TaxRegistrationNumber = GetCurrentCompany.CodeVat
                result.Company.TaxRegistration(1).TaxType = TaxIDStructureTaxType.PVM
            End If
            result.Company.TaxRegistration(0).TaxRegistrationNumber = GetCurrentCompany.Code
            result.Company.TaxRegistration(0).TaxType = TaxIDStructureTaxType.B

            result.Company.Contact = New ContactHeaderStructure() {New ContactHeaderStructure}
            result.Company.Contact(0).ContactPerson = New PersonNameStructure
            result.Company.Contact(0).ContactPerson.FirstName = _NotSpecifiedPlaceHolder
            result.Company.Contact(0).ContactPerson.LastName = _NotSpecifiedPlaceHolder
            result.Company.Contact(0).Email = GetCurrentCompany.Email
            result.Company.Contact(0).Telephone = ""

            Return result

        End Function

        Private Function GetMasterFiles(ByVal periodStart As Date,
            ByVal periodEnd As Date) As AuditFileMasterFiles

            Dim result As New AuditFileMasterFiles

            result.GeneralLedgerAccounts = GetGeneralLedgerAccounts(periodStart, periodEnd)
            Dim myComm As New SQLCommand("FetchSaftCustomersAndSuppliers_2_0")
            myComm.AddParam("?DF", periodStart)
            myComm.AddParam("?DT", periodEnd)
            Using myData As DataTable = myComm.Fetch
                result.Customers = GetCustomers(myData)
                result.Suppliers = GetSuppliers(myData)
            End Using
            result.TaxTable = GetTaxTable()
            result.Products = GetProducts(periodEnd)
            result.PhysicalStock = GetPhysicalStock(periodStart, periodEnd)
            result.Assets = GetAssets(periodStart, periodEnd)
            result.Owners = GetOwners(periodEnd)

            Return result

        End Function

        Private Function GetGeneralLedgerAccounts(ByVal periodStart As Date,
            ByVal periodEnd As Date) As AuditFileMasterFilesAccount()

            Dim result As New List(Of AuditFileMasterFilesAccount)

            Dim myComm As New SQLCommand("FetchSaftGeneralLedgerAccounts_2_0")
            myComm.AddParam("?DF", periodStart)
            myComm.AddParam("?DT", periodEnd)
            Using myData As DataTable = myComm.Fetch
                For Each dr As DataRow In myData.Rows

                    Dim acc As New AuditFileMasterFilesAccount

                    acc.AccountID = CLongSafe(dr.Item(0), 0).ToString
                    acc.AccountDescription = CStrSafe(dr.Item(1))
                    acc.AccountTableID = CLongSafe(dr.Item(2), 0).ToString
                    acc.AccountTableDescription = CStrSafe(dr.Item(3))

                    Select Case General.Account.GetAccountClass(CLongSafe(dr.Item(0), 0))
                        Case 1
                            acc.AccountType = AuditFileMasterFilesAccountAccountType.IT
                        Case 2
                            acc.AccountType = AuditFileMasterFilesAccountAccountType.TT
                        Case 3
                            acc.AccountType = AuditFileMasterFilesAccountAccountType.NK
                        Case 4
                            acc.AccountType = AuditFileMasterFilesAccountAccountType.I
                        Case 5
                            acc.AccountType = AuditFileMasterFilesAccountAccountType.P
                        Case 6
                            acc.AccountType = AuditFileMasterFilesAccountAccountType.S
                        Case Else
                            acc.AccountType = AuditFileMasterFilesAccountAccountType.KT
                    End Select

                    If CDblSafe(dr.Item(4), 2, 0.0) > 0 Then
                        acc.Item = Convert.ToDecimal(CDblSafe(dr.Item(4), 2, 0.0))
                        acc.ItemElementName = ItemChoiceType1.OpeningDebitBalance
                    Else
                        acc.Item = Convert.ToDecimal(-CDblSafe(dr.Item(4), 2, 0.0))
                        acc.ItemElementName = ItemChoiceType1.OpeningCreditBalance
                    End If
                    If CDblSafe(dr.Item(5), 2, 0.0) > 0 Then
                        acc.Item1 = Convert.ToDecimal(CDblSafe(dr.Item(5), 2, 0.0))
                        acc.Item1ElementName = Item1ChoiceType.ClosingDebitBalance
                    Else
                        acc.Item1 = Convert.ToDecimal(-CDblSafe(dr.Item(5), 2, 0.0))
                        acc.Item1ElementName = Item1ChoiceType.ClosingCreditBalance
                    End If

                    result.Add(acc)

                Next
            End Using

            Return result.ToArray

        End Function

        Private Function GetCustomers(myData As DataTable) As AuditFileMasterFilesCustomer()

            Dim result As New List(Of AuditFileMasterFilesCustomer)

            For Each dr As DataRow In myData.Rows
                If CIntSafe(dr.Item(12), 0) > 0 Then

                    Dim prs As New AuditFileMasterFilesCustomer
                    prs.CustomerID = CIntSafe(dr.Item(0), 0).ToString
                    prs.Name = CStrSafe(dr.Item(1))
                    If Not ConvertDbBoolean(CIntSafe(dr.Item(3), 0)) Then
                        prs.RegistrationNumber = CStrSafe(dr.Item(2))
                    End If
                    prs.Address = New AddressStructure() {New AddressStructure}
                    prs.Address(0).AddressType = AddressStructureAddressType.BA
                    prs.Address(0).Country = CStrSafe(dr.Item(4)).ToUpper
                    If String.IsNullOrEmpty(prs.Address(0).Country.Trim) Then _
                        prs.Address(0).Country = StateCodeLith
                    prs.Address(0).FullAddress = CStrSafe(dr.Item(5))
                    prs.Address(0).City = _NotSpecifiedPlaceHolder
                    prs.Address(0).Number = _NotSpecifiedPlaceHolder
                    prs.Address(0).PostalCode = _NotSpecifiedPlaceHolder
                    prs.Address(0).StreetName = _NotSpecifiedPlaceHolder
                    If Not String.IsNullOrEmpty(CStrSafe(dr.Item(7)).Trim) Then
                        prs.BankAccount = New BankAccountStructure() {New BankAccountStructure}
                        prs.BankAccount(0).Item = CStrSafe(dr.Item(7))
                        prs.BankAccount(0).ItemElementName = ItemChoiceType.IBANNumber
                    End If

                    If Not ConvertDbBoolean(CIntSafe(dr.Item(3), 0)) Then

                        If String.IsNullOrEmpty(CStrSafe(dr.Item(6)).Trim) Then
                            prs.TaxRegistration = New TaxRegistrationStructure() {New TaxRegistrationStructure}
                        Else
                            prs.TaxRegistration = New TaxRegistrationStructure() {New TaxRegistrationStructure, New TaxRegistrationStructure}
                            prs.TaxRegistration(1).TaxRegistrationNumber = CStrSafe(dr.Item(6))
                            prs.TaxRegistration(1).TaxType = TaxRegistrationStructureTaxType.PVM
                        End If
                        prs.TaxRegistration(0).TaxRegistrationNumber = CStrSafe(dr.Item(2))
                        prs.TaxRegistration(0).TaxType = TaxRegistrationStructureTaxType.MMR

                    ElseIf Not String.IsNullOrEmpty(CStrSafe(dr.Item(6)).Trim) Then

                        prs.TaxRegistration = New TaxRegistrationStructure() {New TaxRegistrationStructure}
                        prs.TaxRegistration(0).TaxRegistrationNumber = CStrSafe(dr.Item(6))
                        prs.TaxRegistration(0).TaxType = TaxRegistrationStructureTaxType.PVM

                    End If

                    prs.AccountID = New String() {CLongSafe(dr.Item(8), 0).ToString, CLongSafe(dr.Item(9), 0).ToString}
                    prs.SelfBillingIndicator = ""
                    If CDblSafe(dr.Item(10), 2, 0.0) > 0 Then
                        prs.Item = Convert.ToDecimal(CDblSafe(dr.Item(10), 2, 0.0))
                        prs.ItemElementName = ItemChoiceType2.OpeningDebitBalance
                    Else
                        prs.Item = Convert.ToDecimal(-CDblSafe(dr.Item(10), 2, 0.0))
                        prs.ItemElementName = ItemChoiceType2.OpeningCreditBalance
                    End If
                    If CDblSafe(dr.Item(11), 2, 0.0) > 0 Then
                        prs.Item1 = Convert.ToDecimal(CDblSafe(dr.Item(11), 2, 0.0))
                        prs.Item1ElementName = Item1ChoiceType1.ClosingDebitBalance
                    Else
                        prs.Item1 = Convert.ToDecimal(-CDblSafe(dr.Item(11), 2, 0.0))
                        prs.Item1ElementName = Item1ChoiceType1.ClosingCreditBalance
                    End If

                    result.Add(prs)

                End If
            Next

            Return result.ToArray

        End Function

        Private Function GetSuppliers(myData As DataTable) As AuditFileMasterFilesSupplier()

            Dim result As New List(Of AuditFileMasterFilesSupplier)

            For Each dr As DataRow In myData.Rows
                If CIntSafe(dr.Item(13), 0) > 0 Then

                    Dim prs As New AuditFileMasterFilesSupplier
                    prs.SupplierID = CIntSafe(dr.Item(0), 0).ToString
                    prs.Name = CStrSafe(dr.Item(1))
                    If Not ConvertDbBoolean(CIntSafe(dr.Item(3), 0)) Then
                        prs.RegistrationNumber = CStrSafe(dr.Item(2))
                    End If
                    prs.Address = New AddressStructure() {New AddressStructure}
                    prs.Address(0).AddressType = AddressStructureAddressType.BA
                    prs.Address(0).Country = CStrSafe(dr.Item(4)).ToUpper
                    If String.IsNullOrEmpty(prs.Address(0).Country.Trim) Then _
                        prs.Address(0).Country = StateCodeLith
                    prs.Address(0).FullAddress = CStrSafe(dr.Item(5))
                    prs.Address(0).City = _NotSpecifiedPlaceHolder
                    prs.Address(0).Number = _NotSpecifiedPlaceHolder
                    prs.Address(0).PostalCode = _NotSpecifiedPlaceHolder
                    prs.Address(0).StreetName = _NotSpecifiedPlaceHolder
                    If Not String.IsNullOrEmpty(CStrSafe(dr.Item(7)).Trim) Then
                        prs.BankAccount = New BankAccountStructure() {New BankAccountStructure}
                        prs.BankAccount(0).Item = CStrSafe(dr.Item(7))
                        prs.BankAccount(0).ItemElementName = ItemChoiceType.IBANNumber
                    End If

                    If Not Not ConvertDbBoolean(CIntSafe(dr.Item(3), 0)) Then

                        If String.IsNullOrEmpty(CStrSafe(dr.Item(6)).Trim) Then
                            prs.TaxRegistration = New TaxRegistrationStructure() {New TaxRegistrationStructure}
                        Else
                            prs.TaxRegistration = New TaxRegistrationStructure() {New TaxRegistrationStructure, New TaxRegistrationStructure}
                            prs.TaxRegistration(1).TaxRegistrationNumber = CStrSafe(dr.Item(6))
                            prs.TaxRegistration(1).TaxType = TaxRegistrationStructureTaxType.PVM
                        End If
                        prs.TaxRegistration(0).TaxRegistrationNumber = CStrSafe(dr.Item(2))
                        prs.TaxRegistration(0).TaxType = TaxRegistrationStructureTaxType.MMR

                    ElseIf Not String.IsNullOrEmpty(CStrSafe(dr.Item(6)).Trim) Then

                        prs.TaxRegistration = New TaxRegistrationStructure() {New TaxRegistrationStructure}
                        prs.TaxRegistration(0).TaxRegistrationNumber = CStrSafe(dr.Item(6))
                        prs.TaxRegistration(0).TaxType = TaxRegistrationStructureTaxType.PVM

                    End If

                    prs.AccountID = New String() {CLongSafe(dr.Item(8), 0).ToString, CLongSafe(dr.Item(9), 0).ToString}
                    prs.SelfBillingIndicator = ""

                    If CDblSafe(dr.Item(10), 2, 0.0) > 0 Then
                        prs.Item = Convert.ToDecimal(CDblSafe(dr.Item(10), 2, 0.0))
                        prs.ItemElementName = ItemChoiceType3.OpeningDebitBalance
                    Else
                        prs.Item = Convert.ToDecimal(-CDblSafe(dr.Item(10), 2, 0.0))
                        prs.ItemElementName = ItemChoiceType3.OpeningCreditBalance
                    End If
                    If CDblSafe(dr.Item(11), 2, 0.0) > 0 Then
                        prs.Item1 = Convert.ToDecimal(CDblSafe(dr.Item(11), 2, 0.0))
                        prs.Item1ElementName = Item1ChoiceType2.ClosingDebitBalance
                    Else
                        prs.Item1 = Convert.ToDecimal(-CDblSafe(dr.Item(11), 2, 0.0))
                        prs.Item1ElementName = Item1ChoiceType2.ClosingCreditBalance
                    End If

                    result.Add(prs)

                End If
            Next

            Return result.ToArray

        End Function

        Private Function GetTaxTable() As AuditFileMasterFilesTaxTableEntry()

            Dim result As New List(Of AuditFileMasterFilesTaxTableEntryTaxCodeDetails)

            Dim myComm As New SQLCommand("FetchSaftTaxTable_2_0")
            Using myData As DataTable = myComm.Fetch
                For Each dr As DataRow In myData.Rows

                    Dim entry As New AuditFileMasterFilesTaxTableEntryTaxCodeDetails

                    entry.Country = StateCodeLith
                    entry.TaxCode = CIntSafe(dr.Item(0), 0).ToString
                    entry.Description = CStrSafe(dr.Item(1))
                    entry.Item = New System.Nullable(Of Decimal)(Convert.ToDecimal(CDblSafe(dr.Item(2), 2, 0.0)))
                    entry.STITaxCode = CStrSafe(dr.Item(3))

                    result.Add(entry)

                Next
            End Using

            Dim parentEntry As AuditFileMasterFilesTaxTableEntry() = New AuditFileMasterFilesTaxTableEntry() {New AuditFileMasterFilesTaxTableEntry()}
            parentEntry(0).Description = "PVM deklaravimo schemos"
            parentEntry(0).TaxType = "PVM"
            parentEntry(0).TaxCodeDetails = result.ToArray

            Return parentEntry

        End Function

        Private Function GetProducts(periodEnd As Date) As AuditFileMasterFilesProduct()

            Dim result As New List(Of AuditFileMasterFilesProduct)

            Dim myComm As New SQLCommand("FetchSaftGoodsAndServices_2_0")
            myComm.AddParam("?DT", periodEnd)
            myComm.AddParam("?LC", LanguageCodeLith)
            Using myData As DataTable = myComm.Fetch
                For Each dr As DataRow In myData.Rows

                    Dim entry As New AuditFileMasterFilesProduct

                    If CIntSafe(dr.Item(0), 0) > 0 Then
                        entry.GoodsServicesID = AuditFileMasterFilesProductGoodsServicesID.PR
                        entry.ProductCode = "PR" & CIntSafe(dr.Item(1), 0).ToString
                        entry.ValuationMethod = ConvertDatabaseID(Of Goods.GoodsValuationMethod)(CIntSafe(dr.Item(7), 0)).ToString
                    Else
                        entry.GoodsServicesID = AuditFileMasterFilesProductGoodsServicesID.PS
                        entry.ProductCode = "PS" & CIntSafe(dr.Item(1), 0).ToString
                    End If
                    entry.Description = CStrSafe(dr.Item(2))
                    entry.UOMBase = CStrSafe(dr.Item(3))
                    entry.UOMStandard = New String() {CStrSafe(dr.Item(3))}
                    entry.UOMToUOMBaseConversionFactor = New System.Nullable(Of Decimal)() {New System.Nullable(Of Decimal)(Convert.ToDecimal(1.0))}
                    entry.ProductGroup = CStrSafe(dr.Item(4))


                    Dim taxId1 As Integer = CIntSafe(dr.Item(5), 0)
                    Dim taxId2 As Integer = CIntSafe(dr.Item(6), 0)
                    If taxId1 > 0 AndAlso taxId2 > 0 Then

                        entry.Tax = New AuditFileMasterFilesProductTax() {New AuditFileMasterFilesProductTax, New AuditFileMasterFilesProductTax}
                        entry.Tax(0).TaxType = "PVM"
                        entry.Tax(0).TaxCode = taxId1.ToString
                        entry.Tax(1).TaxType = "PVM"
                        entry.Tax(1).TaxCode = taxId2.ToString

                    ElseIf taxId1 > 0 Then

                        entry.Tax = New AuditFileMasterFilesProductTax() {New AuditFileMasterFilesProductTax}
                        entry.Tax(0).TaxType = "PVM"
                        entry.Tax(0).TaxCode = taxId1.ToString

                    ElseIf taxId2 > 0 Then

                        entry.Tax = New AuditFileMasterFilesProductTax() {New AuditFileMasterFilesProductTax}
                        entry.Tax(0).TaxType = "PVM"
                        entry.Tax(0).TaxCode = taxId2.ToString

                    Else

                        entry.Tax = New AuditFileMasterFilesProductTax() {}

                    End If

                    result.Add(entry)

                Next
            End Using

            Return result.ToArray

        End Function

        Private Function GetPhysicalStock(ByVal periodStart As Date,
            ByVal periodEnd As Date) As AuditFileMasterFilesPhysicalStockEntry()

            Dim result As New List(Of AuditFileMasterFilesPhysicalStockEntry)

            Dim myComm As New SQLCommand("FetchSaftGoodsBalance_2_0")
            myComm.AddParam("?DF", periodStart)
            myComm.AddParam("?DT", periodEnd)
            myComm.AddParam("?LC", LanguageCodeLith)
            Using myData As DataTable = myComm.Fetch

                myComm = New SQLCommand("FetchSaftGoodsAcquisitions_2_0")
                myComm.AddParam("?DF", periodStart)

                Using consignmentsData As DataTable = myComm.Fetch

                    For Each dr As DataRow In myData.Rows

                        Dim entry As New AuditFileMasterFilesPhysicalStockEntry

                        Dim entryId As Integer = CIntSafe(dr.Item(0), 0)
                        entry.ProductCode = "PR" & entryId.ToString
                        entry.UOMPhysicalStock = CStrSafe(dr.Item(1))
                        If String.IsNullOrEmpty(entry.UOMPhysicalStock.Trim) Then _
                            entry.UOMPhysicalStock = "vnt."
                        entry.UOMToUOMBaseConversionFactor = Convert.ToDecimal(1)
                        entry.OpeningStockQuantity = Convert.ToDecimal(CDblSafe(dr.Item(2), ROUNDAMOUNTGOODS, 0))
                        entry.OpeningStockValue = Convert.ToDecimal(CDblSafe(dr.Item(3), 2, 0))
                        entry.ClosingStockQuantity = Convert.ToDecimal(CDblSafe(dr.Item(4), ROUNDAMOUNTGOODS, 0))
                        entry.ClosingStockValue = Convert.ToDecimal(CDblSafe(dr.Item(5), 2, 0))

                        Dim acquisitions As New List(Of AuditFileMasterFilesPhysicalStockEntryPhysicalStockAcquisition)

                        For Each aq As DataRow In consignmentsData.Rows
                            If CIntSafe(aq.Item(0), 0) = entryId Then

                                Dim acquisition As New AuditFileMasterFilesPhysicalStockEntryPhysicalStockAcquisition
                                acquisition.DateOfAcquisition = CDateSafe(aq.Item(1), periodStart)
                                acquisition.GLPostingDate = acquisition.DateOfAcquisition
                                acquisition.InvoiceDate = acquisition.DateOfAcquisition
                                acquisition.InvoiceNo = CStrSafe(aq.Item(2))
                                acquisition.Supplier = New AuditFileMasterFilesPhysicalStockEntryPhysicalStockAcquisitionSupplier
                                acquisition.Supplier.Name = CStrSafe(aq.Item(3))
                                acquisition.Supplier.RegistrationNumber = CStrSafe(aq.Item(4))
                                acquisition.AcquiredQuantity = Convert.ToDecimal(CDblSafe(aq.Item(5), ROUNDAMOUNTGOODS, 0))
                                acquisition.StockRemainderQuantity = Convert.ToDecimal(CDblSafe(aq.Item(6), ROUNDAMOUNTGOODS, 0))
                                acquisition.StockRemainderAmount = Convert.ToDecimal(CDblSafe(aq.Item(7), 2, 0))

                                acquisitions.Add(acquisition)

                            End If
                        Next

                        entry.PhysicalStockAcquisition = acquisitions.ToArray

                        result.Add(entry)

                    Next

                End Using

            End Using

            Return result.ToArray

        End Function

        Private Function GetAssets(ByVal periodStart As Date,
            ByVal periodEnd As Date) As AuditFileMasterFilesAsset()

            Dim result As New List(Of AuditFileMasterFilesAsset)

            Dim myComm As New SQLCommand("FetchSaftAssets_2_0")
            myComm.AddParam("?DF", periodStart)
            myComm.AddParam("?DT", periodEnd)
            Using myData As DataTable = myComm.Fetch

                For Each dr As DataRow In myData.Rows

                    Dim asset As New AuditFileMasterFilesAsset

                    asset.AssetID = CIntSafe(dr.Item(0), 0).ToString
                    asset.DateOfAcquisition = CDateSafe(dr.Item(1), Today)
                    asset.DateOfAcquisitionSpecified = True
                    asset.Description = CStrSafe(dr.Item(2))
                    asset.Supplier = New AuditFileMasterFilesAssetSupplier() {New AuditFileMasterFilesAssetSupplier}
                    asset.Supplier(0).SupplierID = CIntSafe(dr.Item(3), 0).ToString
                    asset.Supplier(0).SupplierName = CStrSafe(dr.Item(4))
                    asset.Supplier(0).PostalAddress = New AddressStructure
                    asset.Supplier(0).PostalAddress.AddressType = AddressStructureAddressType.BA
                    asset.Supplier(0).PostalAddress.FullAddress = CStrSafe(dr.Item(5))
                    asset.Supplier(0).PostalAddress.Country = CStrSafe(dr.Item(6))
                    If String.IsNullOrEmpty(asset.Supplier(0).PostalAddress.Country.Trim) Then _
                        asset.Supplier(0).PostalAddress.Country = StateCodeLith
                    asset.Supplier(0).PostalAddress.City = _NotSpecifiedPlaceHolder
                    asset.Supplier(0).PostalAddress.Number = _NotSpecifiedPlaceHolder
                    asset.Supplier(0).PostalAddress.StreetName = _NotSpecifiedPlaceHolder
                    asset.Supplier(0).PostalAddress.PostalCode = _NotSpecifiedPlaceHolder

                    asset.Valuations = New AuditFileMasterFilesAssetValuation() {New AuditFileMasterFilesAssetValuation}

                    asset.Valuations(0).AssetValuationType = AuditFileMasterFilesAssetValuationAssetValuationType.IS
                    asset.Valuations(0).AssetValuationTypeSpecified = True
                    asset.Valuations(0).BookValueBegin = Convert.ToDecimal(CRound(
                        CDblSafe(dr.Item(7), 2, 0) + CDblSafe(dr.Item(8), 2, 0)))
                    asset.Valuations(0).BookValueBeginSpecified = True
                    asset.Valuations(0).BookValueEnd = Convert.ToDecimal(CRound(
                        CDblSafe(dr.Item(7), 2, 0) + CDblSafe(dr.Item(9), 2, 0)))

                    Dim acquisitionAccount As Long = CLongSafe(dr.Item(10), 0)
                    asset.AccountID = acquisitionAccount.ToString
                    asset.Valuations(0).AccountID = acquisitionAccount.ToString
                    asset.Valuations(0).AccountID1 = acquisitionAccount.ToString
                    asset.Valuations(0).AcquisitionAndProductionCostsBegin = Convert.ToDecimal(CRound(
                        CDblSafe(dr.Item(11), 2, 0) + CDblSafe(dr.Item(12), 2, 0)))
                    asset.Valuations(0).AcquisitionAndProductionCostsEnd = Convert.ToDecimal(CRound(
                        CDblSafe(dr.Item(11), 2, 0) + CDblSafe(dr.Item(13), 2, 0)))
                    asset.Valuations(0).InvestmentSupport = Convert.ToDecimal(CDblSafe(dr.Item(14), 2, 0))
                    asset.Valuations(0).InvestmentSupportSpecified = True  ' isigijimo savikainos padidinimas

                    asset.Valuations(0).AccountID2 = CLongSafe(dr.Item(15), 0).ToString ' amortization account
                    asset.Valuations(0).DepreciationMethod = AuditFileMasterFilesAssetValuationDepreciationMethod.T
                    asset.Valuations(0).DepreciationMethodSpecified = True
                    asset.Valuations(0).ValuationClass = GetLimitedLengthString(CStrSafe(dr.Item(16)), 18)
                    asset.Valuations(0).Item = CIntSafe(dr.Item(17), 1)
                    asset.Valuations(0).ItemElementName = ItemChoiceType4.AssetLifeYear
                    asset.Valuations(0).AccumulatedDepreciation = Convert.ToDecimal(CDblSafe(dr.Item(18), 2, 0))
                    asset.Valuations(0).AccumulatedDepreciationSpecified = True
                    asset.Valuations(0).DepreciationForPeriod = Convert.ToDecimal(CDblSafe(dr.Item(19), 2, 0))

                    Dim startUpDate As Date = CDateSafe(dr.Item(20), Date.MaxValue)
                    If startUpDate <> Date.MaxValue Then
                        asset.StartUpDate = startUpDate
                        asset.Valuations(0).DepreciationDate = New Date(startUpDate.AddMonths(1).Year, startUpDate.AddMonths(1).Month, 1)
                        asset.Valuations(0).DepreciationDateSpecified = True
                    End If

                    Dim disposalDate As Date = CDateSafe(dr.Item(21), Date.MaxValue)
                    If disposalDate <> Date.MaxValue Then
                        asset.Valuations(0).AssetDisposal = Convert.ToDecimal(CDblSafe(dr.Item(22), 2, 0))
                        asset.Valuations(0).AssetDisposalDate = disposalDate
                        asset.Valuations(0).AssetDisposalDateSpecified = True
                        asset.Valuations(0).AssetDisposalSpecified = True
                    End If

                    Dim transfersDate As Date = CDateSafe(dr.Item(23), Date.MaxValue)
                    If transfersDate <> Date.MaxValue Then
                        asset.Valuations(0).Transfers = Convert.ToDecimal(CDblSafe(dr.Item(24), 2, 0))
                        asset.Valuations(0).TransfersDate = transfersDate
                        asset.Valuations(0).TransfersDateSpecified = True
                        asset.Valuations(0).TransfersSpecified = True
                    End If

                    asset.Valuations(0).AccumulatedDepreciationOfAppreciation = Convert.ToDecimal(CDblSafe(dr.Item(25), 2, 0))
                    asset.Valuations(0).AccumulatedDepreciationOfAppreciationSpecified = True

                    Dim appreciationDate As Date = CDateSafe(dr.Item(26), Date.MaxValue)
                    Dim appreciation As Double = CRound(CDblSafe(dr.Item(28), 2, 0) + CDblSafe(dr.Item(29), 2, 0))
                    If appreciationDate <> Date.MaxValue AndAlso appreciation > 0 Then
                        asset.Valuations(0).Appreciation = New AuditFileMasterFilesAssetValuationAppreciation() {New AuditFileMasterFilesAssetValuationAppreciation}
                        asset.Valuations(0).Appreciation(0).AccountID = CLongSafe(dr.Item(27), 0).ToString
                        asset.Valuations(0).Appreciation(0).Appreciation = Convert.ToDecimal(appreciation)
                        asset.Valuations(0).Appreciation(0).AppreciationDate = appreciationDate
                        asset.Valuations(0).Appreciation(0).AppreciationDateSpecified = True
                        asset.Valuations(0).Appreciation(0).AppreciationSpecified = True
                        asset.Valuations(0).Appreciation(0).DepreciationForPeriod = Convert.ToDecimal(CDblSafe(dr.Item(30), 2, 0))
                        asset.Valuations(0).Appreciation(0).DepreciationForPeriodSpecified = True
                        asset.Valuations(0).Appreciation(0).DepreciationMethod = AuditFileMasterFilesAssetValuationAppreciationDepreciationMethod.T
                        asset.Valuations(0).Appreciation(0).DepreciationMethodSpecified = True
                    End If

                    Dim impairmentDate As Date = CDateSafe(dr.Item(31), Date.MaxValue)
                    Dim impairment As Double = CRound(CDblSafe(dr.Item(33), 2, 0) + CDblSafe(dr.Item(34), 2, 0))
                    If impairmentDate <> Date.MaxValue AndAlso impairment > 0 Then
                        asset.Valuations(0).ImpairmentOfAssets = New AuditFileMasterFilesAssetValuationImpairmentOfAssets() {New AuditFileMasterFilesAssetValuationImpairmentOfAssets}
                        asset.Valuations(0).ImpairmentOfAssets(0).AccountID = CLongSafe(dr.Item(32), 0).ToString
                        asset.Valuations(0).ImpairmentOfAssets(0).ImpairmentOfAssets = Convert.ToDecimal(impairment)
                        asset.Valuations(0).ImpairmentOfAssets(0).ImpairmentOfAssetsDate = impairmentDate
                        asset.Valuations(0).ImpairmentOfAssets(0).ImpairmentOfAssetsDateSpecified = True
                        asset.Valuations(0).ImpairmentOfAssets(0).ImpairmentOfAssetsSpecified = True
                    End If

                    result.Add(asset)

                Next

            End Using

            Return result.ToArray

        End Function

        Private Function GetOwners(periodEnd As Date) As AuditFileMasterFilesOwner()

            Dim result As New List(Of AuditFileMasterFilesOwner)

            Dim myComm As New SQLCommand("FetchSaftOwners_2_0")
            myComm.AddParam("?DT", periodEnd)

            Using myData As DataTable = myComm.Fetch
                For Each dr As DataRow In myData.Rows

                    Dim entry As New AuditFileMasterFilesOwner

                    entry.OwnerName = CStrSafe(dr.Item(0))
                    entry.OwnerID = CStrSafe(dr.Item(1))
                    entry.Address = New AddressStructure
                    entry.Address.AddressType = AddressStructureAddressType.BA
                    entry.Address.Country = CStrSafe(dr.Item(2))
                    If String.IsNullOrEmpty(entry.Address.Country.Trim) Then _
                        entry.Address.Country = StateCodeLith
                    entry.Address.FullAddress = CStrSafe(dr.Item(3))
                    entry.Address.City = _NotSpecifiedPlaceHolder
                    entry.Address.Number = _NotSpecifiedPlaceHolder
                    entry.Address.StreetName = _NotSpecifiedPlaceHolder
                    entry.Address.PostalCode = _NotSpecifiedPlaceHolder
                    entry.AccountID = CLongSafe(dr.Item(4), 0).ToString
                    Try
                        entry.SharesType = DirectCast([Enum].Parse(GetType(AuditFileMasterFilesOwnerSharesType),
                            CStrSafe(dr.Item(5))), AuditFileMasterFilesOwnerSharesType)
                    Catch ex As Exception
                        entry.SharesType = AuditFileMasterFilesOwnerSharesType.PP
                    End Try
                    entry.SharesTypeSpecified = True
                    Dim valuePerUnit As Double = CDblSafe(dr.Item(6), 2, 0.0)
                    Dim quantity As Double = CDblSafe(dr.Item(7), 2, 0.0)
                    entry.SharesQuantity = New Decimal() {Convert.ToDecimal(quantity)}
                    entry.SharesTransfersDateSpecified = False
                    entry.SharesAmount = Convert.ToDecimal(CRound(quantity * valuePerUnit, 2))
                    entry.SharesAmountSpecified = True
                    entry.SharesAcquisitionDate = CDateSafe(dr.Item(8), Date.MinValue)
                    entry.SharesAcquisitionDateSpecified = True

                    result.Add(entry)

                Next
            End Using

            Return result.ToArray

        End Function

        Private Function GetGeneralLedgerTransactions(ByVal periodStart As Date,
            ByVal periodEnd As Date, ByRef transactionCount As Integer, ByRef debitSum As Double,
            ByRef creditSum As Double) As AuditFileGeneralLedgerEntriesJournalTransaction()

            Dim result As New List(Of AuditFileGeneralLedgerEntriesJournalTransaction)

            Dim myComm As New SQLCommand("FetchSaftGeneralLedgerTransactions_2_0")
            myComm.AddParam("?DF", periodStart)
            myComm.AddParam("?DT", periodEnd)
            Using myData As DataTable = myComm.Fetch

                myComm = New SQLCommand("FetchSaftGeneralLedgerTransactionLines_2_0")
                myComm.AddParam("?DF", periodStart)
                myComm.AddParam("?DT", periodEnd)
                Using detailsData As DataTable = myComm.Fetch

                    For Each dr As DataRow In myData.Rows

                        Dim transaction As New AuditFileGeneralLedgerEntriesJournalTransaction

                        Dim id As Integer = CIntSafe(dr.Item(0), 0)
                        transaction.SystemID = id.ToString
                        transaction.TransactionID = id.ToString
                        Dim transactionDate As Date = CDateSafe(dr.Item(1), periodEnd)
                        transaction.GLPostingDate = transactionDate
                        transaction.SystemEntryDate = transactionDate
                        transaction.Period = transactionDate.Month.ToString
                        transaction.PeriodYear = transactionDate.Year.ToString
                        transaction.Description = CStrSafe(dr.Item(2))
                        transaction.TransactionDate = CDateSafe(dr.Item(3), transactionDate)
                        Dim personID As Integer = CIntSafe(dr.Item(4), 0)
                        If personID > 0 Then
                            transaction.Item = personID.ToString
                            Dim personIsClient As Boolean = ConvertDbBoolean(CIntSafe(dr.Item(5), 0))
                            If personIsClient Then
                                transaction.ItemElementName = ItemChoiceType5.CustomerID
                            Else
                                transaction.ItemElementName = ItemChoiceType5.SupplierID
                            End If
                        Else
                            transaction.Item = ""
                            transaction.ItemElementName = ItemChoiceType5.SupplierID
                        End If

                        Dim lines As New List(Of AuditFileGeneralLedgerEntriesJournalTransactionLine)
                        For Each dd As DataRow In detailsData.Rows

                            If CIntSafe(dd.Item(0), 0) = id Then

                                Dim line As New AuditFileGeneralLedgerEntriesJournalTransactionLine
                                line.SourceDocumentID = id.ToString
                                line.RecordID = CLongSafe(dd.Item(1), 0).ToString
                                line.Description = ""
                                line.AccountID = CLongSafe(dd.Item(2), 0).ToString
                                Dim amount As Double = CDblSafe(dd.Item(3), 2, 0.0)
                                If ConvertDatabaseCharID(Of BookEntryType)(CStrSafe(dd.Item(4))) _
                                    = BookEntryType.Debetas Then
                                    line.Item1ElementName = Item1ChoiceType3.DebitAmount
                                    debitSum = CRound(debitSum + amount)
                                Else
                                    line.Item1ElementName = Item1ChoiceType3.CreditAmount
                                    creditSum = CRound(creditSum + amount)
                                End If
                                line.Item1 = New AmountStructure
                                line.Item1.Amount = Convert.ToDecimal(amount)
                                line.Item1.CurrencyAmount = Convert.ToDecimal(amount)
                                line.Item1.CurrencyCode = GetCurrentCompany.BaseCurrency
                                Dim subPersonID As Integer = CIntSafe(dd.Item(5), 0)
                                If subPersonID > 0 Then
                                    line.Item = subPersonID.ToString
                                    Dim personIsClient As Boolean = ConvertDbBoolean(CIntSafe(dd.Item(6), 0))
                                    If personIsClient Then
                                        line.ItemElementName = ItemChoiceType6.CustomerID
                                    Else
                                        line.ItemElementName = ItemChoiceType6.SupplierID
                                    End If
                                Else
                                    line.Item = ""
                                    line.ItemElementName = ItemChoiceType6.SupplierID
                                End If

                                lines.Add(line)

                            End If

                        Next

                        transaction.Line = lines.ToArray

                        result.Add(transaction)
                        transactionCount += 1

                    Next

                End Using

            End Using

            Return result.ToArray

        End Function

        Private Function GetSourceDocuments(ByVal periodStart As Date,
            ByVal periodEnd As Date) As AuditFileSourceDocuments

            Dim result As New AuditFileSourceDocuments

            Dim transactionCount As Integer = 0
            Dim debitSum As Double = 0.0
            Dim creditSum As Double = 0.0

            result.SalesInvoices = New AuditFileSourceDocumentsSalesInvoices
            result.SalesInvoices.Invoice = GetSalesInvoices(
                periodStart, periodEnd, transactionCount, debitSum, creditSum)
            result.SalesInvoices.NumberOfEntries = transactionCount.ToString
            result.SalesInvoices.TotalDebit = Convert.ToDecimal(debitSum)
            result.SalesInvoices.TotalCredit = Convert.ToDecimal(creditSum)

            result.PurchaseInvoices = New AuditFileSourceDocumentsPurchaseInvoices
            transactionCount = 0
            debitSum = 0.0
            creditSum = 0.0
            result.PurchaseInvoices.Invoice = GetPurchaseInvoices(
                periodStart, periodEnd, transactionCount, debitSum, creditSum)
            result.PurchaseInvoices.NumberOfEntries = transactionCount.ToString
            result.PurchaseInvoices.TotalDebit = Convert.ToDecimal(debitSum)
            result.PurchaseInvoices.TotalCredit = Convert.ToDecimal(creditSum)

            result.Payments = New AuditFileSourceDocumentsPayments
            transactionCount = 0
            debitSum = 0.0
            creditSum = 0.0
            result.Payments.Payment = GetPayments(periodStart, periodEnd,
                transactionCount, debitSum, creditSum)
            result.Payments.NumberOfEntries = transactionCount.ToString
            result.Payments.TotalDebit = Convert.ToDecimal(debitSum)
            result.Payments.TotalCredit = Convert.ToDecimal(creditSum)

            result.MovementOfGoods = New AuditFileSourceDocumentsMovementOfGoods
            transactionCount = 0
            Dim quantityReceived As Double = 0.0
            Dim quantityIssued As Double = 0.0
            result.MovementOfGoods.StockMovement = GetStockMovements(periodStart, periodEnd,
                transactionCount, quantityReceived, quantityIssued)
            result.MovementOfGoods.NumberOfMovementLines = transactionCount.ToString
            result.MovementOfGoods.TotalQuantityReceived = Convert.ToDecimal(quantityReceived)
            result.MovementOfGoods.TotalQuantityIssued = Convert.ToDecimal(quantityIssued)

            result.AssetTransactions = New AuditFileSourceDocumentsAssetTransactions
            transactionCount = 0
            result.AssetTransactions.AssetTransaction = GetAssetTransactions(
                periodStart, periodEnd, transactionCount)
            result.AssetTransactions.NumberOfAssetTransactions = transactionCount.ToString

        End Function

        Private Function GetSalesInvoices(ByVal periodStart As Date,
            ByVal periodEnd As Date, ByRef transactionCount As Integer, ByRef debitSum As Double,
            ByRef creditSum As Double) As InvoiceStructure()

            Dim result As New List(Of InvoiceStructure)

            Dim myComm As New SQLCommand("FetchSaftSalesInvoices_2_0")
            myComm.AddParam("?DF", periodStart)
            myComm.AddParam("?DT", periodEnd)
            Using myData As DataTable = myComm.Fetch

                myComm = New SQLCommand("FetchSaftSalesInvoicesLines_2_0")
                myComm.AddParam("?DF", periodStart)
                myComm.AddParam("?DT", periodEnd)
                Using detailsData As DataTable = myComm.Fetch

                    For Each dr As DataRow In myData.Rows

                        Dim invoice As New InvoiceStructure

                        Dim id As Integer = CIntSafe(dr.Item(0), 0)
                        invoice.SystemID = id.ToString
                        invoice.TransactionID = id.ToString
                        invoice.GLTransactionID = id.ToString
                        invoice.GLPostingDate = CDateSafe(dr.Item(1), periodEnd)
                        invoice.GLPostingDateSpecified = True
                        invoice.InvoiceDate = invoice.GLPostingDate
                        invoice.InvoiceNo = CStrSafe(dr.Item(2))
                        invoice.AccountID = CLongSafe(dr.Item(3), 0).ToString
                        Dim invoiceType As Documents.InvoiceType =
                            ConvertDatabaseID(Of Documents.InvoiceType)(CIntSafe(dr.Item(4), 0))
                        If String.IsNullOrEmpty(GetCurrentCompany.CodeVat.Trim) Then
                            If invoiceType = Documents.InvoiceType.Credit Then
                                invoice.InvoiceType = InvoiceStructureInvoiceType.K
                            ElseIf invoiceType = Documents.InvoiceType.Debit Then
                                invoice.InvoiceType = InvoiceStructureInvoiceType.D
                            Else
                                invoice.InvoiceType = InvoiceStructureInvoiceType.S
                            End If
                        Else
                            If invoiceType = Documents.InvoiceType.Credit Then
                                invoice.InvoiceType = InvoiceStructureInvoiceType.KS
                            ElseIf invoiceType = Documents.InvoiceType.Debit Then
                                invoice.InvoiceType = InvoiceStructureInvoiceType.DS
                            Else
                                invoice.InvoiceType = InvoiceStructureInvoiceType.SF
                            End If
                        End If
                        Dim invoiceCurrency As String = CStrSafe(dr.Item(5)).Trim.ToUpper
                        If invoiceType = Documents.InvoiceType.Debit Then
                            Dim supplier As New InvoiceStructureSupplierInfo

                            supplier.Item = CIntSafe(dr.Item(6), 0).ToString
                            supplier.ItemElementName = ItemChoiceType8.SupplierID
                            supplier.Country = CStrSafe(dr.Item(7)).Trim.ToUpper
                            If String.IsNullOrEmpty(supplier.Country) Then supplier.Country = StateCodeLith
                            Dim vatCode As String = CStrSafe(dr.Item(8))
                            Dim code As String = CStrSafe(dr.Item(9))
                            Dim isFakeCode As Boolean = ConvertDbBoolean(CIntSafe(dr.Item(10), 0))
                            If Not String.IsNullOrEmpty(vatCode.Trim) Then
                                supplier.TaxRegistrationNumber = vatCode
                                supplier.TaxType = InvoiceStructureSupplierInfoTaxType.PVM
                            ElseIf supplier.Country = StateCodeLith AndAlso Not isFakeCode Then
                                supplier.TaxRegistrationNumber = code
                                supplier.TaxType = InvoiceStructureSupplierInfoTaxType.MMR
                            Else
                                supplier.TaxRegistrationNumber = code
                                supplier.TaxType = InvoiceStructureSupplierInfoTaxType.KT
                            End If

                            invoice.Item = supplier

                        Else
                            Dim customer As New InvoiceStructureCustomerInfo
                            customer.Item = CIntSafe(dr.Item(6), 0).ToString
                            customer.ItemElementName = ItemChoiceType7.CustomerID
                            customer.Country = CStrSafe(dr.Item(7)).Trim.ToUpper
                            If String.IsNullOrEmpty(customer.Country) Then customer.Country = StateCodeLith
                            Dim vatCode As String = CStrSafe(dr.Item(8))
                            Dim code As String = CStrSafe(dr.Item(9))
                            Dim isFakeCode As Boolean = ConvertDbBoolean(CIntSafe(dr.Item(10), 0))
                            If Not String.IsNullOrEmpty(vatCode.Trim) Then
                                customer.TaxRegistrationNumber = vatCode
                                customer.TaxType = InvoiceStructureCustomerInfoTaxType.PVM
                            ElseIf customer.Country = StateCodeLith AndAlso Not isFakeCode Then
                                customer.TaxRegistrationNumber = code
                                customer.TaxType = InvoiceStructureCustomerInfoTaxType.MMR
                            Else
                                customer.TaxRegistrationNumber = code
                                customer.TaxType = InvoiceStructureCustomerInfoTaxType.KT
                            End If

                            invoice.Item = customer

                        End If

                        Dim lines As New List(Of InvoiceStructureLine)

                        For Each dd As DataRow In detailsData.Rows
                            If CIntSafe(dd.Item(0), 0) = id Then

                                Dim line As New InvoiceStructureLine

                                line.LineNumber = CIntSafe(dd.Item(1), 0).ToString
                                line.Description = CStrSafe(dd.Item(2))
                                line.InvoiceUOM = CStrSafe(dd.Item(3))
                                line.AccountID = CLongSafe(dd.Item(4)).ToString
                                line.UnitPrice = Convert.ToDecimal(CDblSafe(dd.Item(5), 2, 0.0))
                                line.Quantity = Convert.ToDecimal(CDblSafe(dd.Item(6), 2, 0.0))
                                line.QuantitySpecified = True
                                line.TaxInformation = New TaxInformationStructure() {New TaxInformationStructure}
                                line.TaxInformation(0).TaxAmount = New AmountStructure
                                line.TaxInformation(0).TaxPercentage = Convert.ToDecimal(CDblSafe(dd.Item(7), 2, 0.0))
                                line.TaxInformation(0).TaxPercentageSpecified = True
                                line.TaxInformation(0).TaxAmount.Amount = Convert.ToDecimal(
                                    CRound(CDblSafe(dd.Item(9), 2, 0.0) - CDblSafe(dd.Item(10), 2, 0.0), 2))
                                line.TaxInformation(0).TaxAmount.CurrencyAmount = Convert.ToDecimal(
                                    CRound(CDblSafe(dd.Item(11), 2, 0.0) - CDblSafe(dd.Item(12), 2, 0.0), 2))
                                line.TaxInformation(0).TaxCode = CIntSafe(dd.Item(13), 0).ToString
                                line.TaxInformation(0).TaxType = "PVM"
                                line.InvoiceLineAmount = New AmountStructure
                                line.InvoiceLineAmount.Amount = Convert.ToDecimal(
                                    CRound(CDblSafe(dd.Item(14), 2, 0.0) - CDblSafe(dd.Item(15), 2, 0.0), 2))
                                line.InvoiceLineAmount.CurrencyAmount = Convert.ToDecimal(
                                    CRound(CDblSafe(dd.Item(16), 2, 0.0) - CDblSafe(dd.Item(17), 2, 0.0), 2))

                                Try
                                    line.GoodsServicesID = DirectCast([Enum].Parse(
                                        GetType(InvoiceStructureLineGoodsServicesID), CStrSafe(dd.Item(18))),
                                        InvoiceStructureLineGoodsServicesID)
                                Catch ex As Exception
                                    line.GoodsServicesID = InvoiceStructureLineGoodsServicesID.KT
                                End Try
                                line.GoodsServicesIDSpecified = True
                                line.ProductCode = CStrSafe(dd.Item(18)) & CIntSafe(dd.Item(19), 0).ToString

                                If invoiceType = Documents.InvoiceType.Credit Then
                                    line.DebitCreditIndicator = InvoiceStructureLineDebitCreditIndicator.D
                                    debitSum = CRound(debitSum + line.InvoiceLineAmount.Amount.Value, 2)
                                Else
                                    line.DebitCreditIndicator = InvoiceStructureLineDebitCreditIndicator.K
                                    creditSum = CRound(creditSum + line.InvoiceLineAmount.Amount.Value, 2)
                                End If
                                line.TaxInformation(0).TaxAmount.CurrencyCode = invoiceCurrency
                                line.InvoiceLineAmount.CurrencyCode = invoiceCurrency
                                line.TaxPointDate = invoice.InvoiceDate

                                lines.Add(line)

                            End If
                        Next

                        invoice.Line = lines.ToArray

                        result.Add(invoice)
                        transactionCount += 1

                    Next


                End Using

            End Using


            Return result.ToArray

        End Function

        Private Function GetPurchaseInvoices(ByVal periodStart As Date,
            ByVal periodEnd As Date, ByRef transactionCount As Integer, ByRef debitSum As Double,
            ByRef creditSum As Double) As InvoiceStructure()

            Dim result As New List(Of InvoiceStructure)

            Dim myComm As New SQLCommand("FetchSaftPurchaseInvoices_2_0")
            myComm.AddParam("?DF", periodStart)
            myComm.AddParam("?DT", periodEnd)
            Using myData As DataTable = myComm.Fetch

                myComm = New SQLCommand("FetchSaftPurchaseInvoicesLines_2_0")
                myComm.AddParam("?DF", periodStart)
                myComm.AddParam("?DT", periodEnd)
                Using detailsData As DataTable = myComm.Fetch

                    For Each dr As DataRow In myData.Rows

                        Dim invoice As New InvoiceStructure

                        Dim id As Integer = CIntSafe(dr.Item(0), 0)
                        invoice.SystemID = id.ToString
                        invoice.TransactionID = id.ToString
                        invoice.GLTransactionID = id.ToString
                        invoice.GLPostingDate = CDateSafe(dr.Item(1), periodEnd)
                        invoice.GLPostingDateSpecified = True
                        invoice.InvoiceNo = CStrSafe(dr.Item(2))
                        invoice.AccountID = CLongSafe(dr.Item(3), 0).ToString
                        Dim invoiceType As Documents.InvoiceType =
                            ConvertDatabaseID(Of Documents.InvoiceType)(CIntSafe(dr.Item(4), 0))
                        If String.IsNullOrEmpty(GetCurrentCompany.CodeVat.Trim) Then
                            If invoiceType = Documents.InvoiceType.Credit Then
                                invoice.InvoiceType = InvoiceStructureInvoiceType.K
                            ElseIf invoiceType = Documents.InvoiceType.Debit Then
                                invoice.InvoiceType = InvoiceStructureInvoiceType.D
                            Else
                                invoice.InvoiceType = InvoiceStructureInvoiceType.S
                            End If
                        Else
                            If invoiceType = Documents.InvoiceType.Credit Then
                                invoice.InvoiceType = InvoiceStructureInvoiceType.KS
                            ElseIf invoiceType = Documents.InvoiceType.Debit Then
                                invoice.InvoiceType = InvoiceStructureInvoiceType.DS
                            Else
                                invoice.InvoiceType = InvoiceStructureInvoiceType.SF
                            End If
                        End If
                        Dim invoiceCurrency As String = CStrSafe(dr.Item(5)).Trim.ToUpper
                        If invoiceType = Documents.InvoiceType.Credit Then
                            Dim supplier As New InvoiceStructureSupplierInfo

                            supplier.Item = CIntSafe(dr.Item(6), 0).ToString
                            supplier.ItemElementName = ItemChoiceType8.SupplierID
                            supplier.Country = CStrSafe(dr.Item(7)).Trim.ToUpper
                            If String.IsNullOrEmpty(supplier.Country) Then supplier.Country = StateCodeLith
                            Dim vatCode As String = CStrSafe(dr.Item(8))
                            Dim code As String = CStrSafe(dr.Item(9))
                            Dim isFakeCode As Boolean = ConvertDbBoolean(CIntSafe(dr.Item(10), 0))
                            If Not String.IsNullOrEmpty(vatCode.Trim) Then
                                supplier.TaxRegistrationNumber = vatCode
                                supplier.TaxType = InvoiceStructureSupplierInfoTaxType.PVM
                            ElseIf supplier.Country = StateCodeLith AndAlso Not isFakeCode Then
                                supplier.TaxRegistrationNumber = code
                                supplier.TaxType = InvoiceStructureSupplierInfoTaxType.MMR
                            Else
                                supplier.TaxRegistrationNumber = code
                                supplier.TaxType = InvoiceStructureSupplierInfoTaxType.KT
                            End If

                            invoice.Item = supplier

                        Else
                            Dim customer As New InvoiceStructureCustomerInfo
                            customer.Item = CIntSafe(dr.Item(6), 0).ToString
                            customer.ItemElementName = ItemChoiceType7.CustomerID
                            customer.Country = CStrSafe(dr.Item(7)).Trim.ToUpper
                            If String.IsNullOrEmpty(customer.Country) Then customer.Country = StateCodeLith
                            Dim vatCode As String = CStrSafe(dr.Item(8))
                            Dim code As String = CStrSafe(dr.Item(9))
                            Dim isFakeCode As Boolean = ConvertDbBoolean(CIntSafe(dr.Item(10), 0))
                            If Not String.IsNullOrEmpty(vatCode.Trim) Then
                                customer.TaxRegistrationNumber = vatCode
                                customer.TaxType = InvoiceStructureCustomerInfoTaxType.PVM
                            ElseIf customer.Country = StateCodeLith AndAlso Not isFakeCode Then
                                customer.TaxRegistrationNumber = code
                                customer.TaxType = InvoiceStructureCustomerInfoTaxType.MMR
                            Else
                                customer.TaxRegistrationNumber = code
                                customer.TaxType = InvoiceStructureCustomerInfoTaxType.KT
                            End If

                            invoice.Item = customer

                        End If

                        invoice.InvoiceDate = CDateSafe(dr.Item(11), invoice.GLPostingDate)

                        Dim lines As New List(Of InvoiceStructureLine)

                        For Each dd As DataRow In detailsData.Rows
                            If CIntSafe(dd.Item(0), 0) = id Then

                                Dim line As New InvoiceStructureLine

                                line.LineNumber = CIntSafe(dd.Item(1), 0).ToString
                                line.Description = CStrSafe(dd.Item(2))
                                line.InvoiceUOM = CStrSafe(dd.Item(3))
                                line.AccountID = CLongSafe(dd.Item(4)).ToString
                                line.UnitPrice = Convert.ToDecimal(CDblSafe(dd.Item(5), 2, 0.0))
                                line.Quantity = Convert.ToDecimal(CDblSafe(dd.Item(6), 2, 0.0))
                                line.QuantitySpecified = True
                                line.TaxInformation = New TaxInformationStructure() {New TaxInformationStructure}
                                line.TaxInformation(0).TaxAmount = New AmountStructure
                                line.TaxInformation(0).TaxPercentage = Convert.ToDecimal(CDblSafe(dd.Item(7), 2, 0.0))
                                line.TaxInformation(0).TaxPercentageSpecified = True
                                line.TaxInformation(0).TaxAmount.Amount = Convert.ToDecimal(CDblSafe(dd.Item(8), 2, 0.0))
                                line.TaxInformation(0).TaxAmount.CurrencyAmount = Convert.ToDecimal(
                                    CDblSafe(dd.Item(9), 2, 0.0))
                                line.TaxInformation(0).TaxCode = CIntSafe(dd.Item(10), 0).ToString
                                line.TaxInformation(0).TaxType = "PVM"
                                line.InvoiceLineAmount = New AmountStructure
                                line.InvoiceLineAmount.Amount = Convert.ToDecimal(CDblSafe(dd.Item(11), 2, 0.0))
                                line.InvoiceLineAmount.CurrencyAmount = Convert.ToDecimal(CDblSafe(dd.Item(12), 2, 0.0))

                                Try
                                    line.GoodsServicesID = DirectCast([Enum].Parse(
                                        GetType(InvoiceStructureLineGoodsServicesID), CStrSafe(dd.Item(13))),
                                        InvoiceStructureLineGoodsServicesID)
                                Catch ex As Exception
                                    line.GoodsServicesID = InvoiceStructureLineGoodsServicesID.KT
                                End Try
                                line.GoodsServicesIDSpecified = True
                                line.ProductCode = CStrSafe(dd.Item(13)) & CIntSafe(dd.Item(14), 0).ToString

                                If invoiceType = Documents.InvoiceType.Credit Then
                                    line.DebitCreditIndicator = InvoiceStructureLineDebitCreditIndicator.K
                                    creditSum = CRound(creditSum + line.InvoiceLineAmount.Amount.Value, 2)
                                Else
                                    line.DebitCreditIndicator = InvoiceStructureLineDebitCreditIndicator.D
                                    debitSum = CRound(debitSum + line.InvoiceLineAmount.Amount.Value, 2)
                                End If
                                line.TaxInformation(0).TaxAmount.CurrencyCode = invoiceCurrency
                                line.InvoiceLineAmount.CurrencyCode = invoiceCurrency
                                line.TaxPointDate = invoice.InvoiceDate

                                lines.Add(line)

                            End If
                        Next

                        invoice.Line = lines.ToArray

                        result.Add(invoice)
                        transactionCount += 1

                    Next


                End Using

            End Using


            Return result.ToArray

        End Function

        Private Function GetPayments(ByVal periodStart As Date,
            ByVal periodEnd As Date, ByRef transactionCount As Integer, ByRef debitSum As Double,
            ByRef creditSum As Double) As AuditFileSourceDocumentsPaymentsPayment()

            Dim result As New List(Of AuditFileSourceDocumentsPaymentsPayment)

            Dim myComm As New SQLCommand("FetchSaftPayments_2_0")
            myComm.AddParam("?DF", periodStart)
            myComm.AddParam("?DT", periodEnd)
            Using myData As DataTable = myComm.Fetch

                myComm = New SQLCommand("FetchSaftPaymentDetails_2_0")
                myComm.AddParam("?DF", periodStart)
                myComm.AddParam("?DT", periodEnd)

                Using detailsData As DataTable = myComm.Fetch

                    For Each dr As DataRow In myData.Rows

                        Dim payment As New AuditFileSourceDocumentsPaymentsPayment

                        Try
                            payment.PaymentMethod = DirectCast([Enum].Parse(
                                GetType(AuditFileSourceDocumentsPaymentsPaymentPaymentMethod),
                                CStrSafe(dr.Item(0))), AuditFileSourceDocumentsPaymentsPaymentPaymentMethod)
                        Catch ex As Exception
                            payment.PaymentMethod = AuditFileSourceDocumentsPaymentsPaymentPaymentMethod.KT
                        End Try
                        payment.PaymentMethodSpecified = True
                        Dim id As Integer = CIntSafe(dr.Item(1), 0)
                        payment.SystemID = id.ToString
                        payment.TransactionID = id.ToString
                        payment.PaymentRefNo = id.ToString
                        Dim paymentDate As Date = CDateSafe(dr.Item(2), periodEnd)
                        payment.TransactionDate = paymentDate
                        payment.PeriodYear = paymentDate.Year.ToString
                        payment.Description = CStrSafe(dr.Item(3))
                        payment.GrossTotal = Convert.ToDecimal(CDblSafe(dr.Item(4), 2, 0.0))

                        If payment.PaymentMethod = AuditFileSourceDocumentsPaymentsPaymentPaymentMethod.BP Then

                            payment.Line = New AuditFileSourceDocumentsPaymentsPaymentLine() _
                                {New AuditFileSourceDocumentsPaymentsPaymentLine}
                            payment.Line(0).PaymentLineAmount = New AmountStructure
                            payment.Line(0).PaymentLineAmount.Amount = payment.GrossTotal
                            payment.Line(0).PaymentLineAmount.CurrencyAmount = Convert.ToDecimal(
                                CDblSafe(dr.Item(5), 2, 0.0))
                            payment.Line(0).PaymentLineAmount.CurrencyCode = CStrSafe(dr.Item(6))
                            payment.Line(0).AccountID = CLongSafe(dr.Item(7), 0).ToString
                            Dim personID As Integer = CIntSafe(dr.Item(8), 0)
                            If payment.GrossTotal < 0 Then
                                payment.Line(0).DebitCreditIndicator = AuditFileSourceDocumentsPaymentsPaymentLineDebitCreditIndicator.K
                                If personID > 0 Then payment.Line(0).SupplierID = personID.ToString
                                payment.GrossTotal = -payment.GrossTotal
                                creditSum = CRound(creditSum + Convert.ToDouble(payment.GrossTotal), 2)
                            Else
                                payment.Line(0).DebitCreditIndicator = AuditFileSourceDocumentsPaymentsPaymentLineDebitCreditIndicator.D
                                If personID > 0 Then payment.Line(0).CustomerID = personID.ToString
                                debitSum = CRound(debitSum + Convert.ToDouble(payment.GrossTotal), 2)
                            End If
                            payment.Line(0).Description = payment.Description
                            payment.Line(0).LineNumber = id.ToString & "-1"

                            Dim accountType As Documents.CashAccountType =
                                ConvertDatabaseID(Of Documents.CashAccountType)(CIntSafe(dr.Item(9), 0))
                            If accountType = Documents.CashAccountType.BankAccount Then
                                payment.BankAccount = New BankAccountStructure
                                payment.BankAccount.Item = CStrSafe(dr.Item(10))
                                payment.BankAccount.ItemElementName = ItemChoiceType.IBANNumber
                            ElseIf Not String.IsNullOrEmpty(CStrSafe(dr.Item(10)).Trim) Then
                                payment.BankAccount = New BankAccountStructure
                                payment.BankAccount.Item = CStrSafe(dr.Item(10))
                                payment.BankAccount.ItemElementName = ItemChoiceType.BankAccountNumber
                            End If

                        ElseIf payment.PaymentMethod = AuditFileSourceDocumentsPaymentsPaymentPaymentMethod.KIO _
                            OrElse payment.PaymentMethod = AuditFileSourceDocumentsPaymentsPaymentPaymentMethod.KPO Then

                            payment.Line = New AuditFileSourceDocumentsPaymentsPaymentLine() _
                                {New AuditFileSourceDocumentsPaymentsPaymentLine}
                            payment.Line(0).PaymentLineAmount = New AmountStructure
                            payment.Line(0).PaymentLineAmount.Amount = payment.GrossTotal
                            payment.Line(0).PaymentLineAmount.CurrencyAmount = Convert.ToDecimal(
                                CDblSafe(dr.Item(5), 2, 0.0))
                            payment.Line(0).PaymentLineAmount.CurrencyCode = CStrSafe(dr.Item(6))
                            payment.Line(0).AccountID = CLongSafe(dr.Item(7), 0).ToString
                            Dim personID As Integer = CIntSafe(dr.Item(9), 0)
                            If payment.PaymentMethod = AuditFileSourceDocumentsPaymentsPaymentPaymentMethod.KPO Then
                                payment.Line(0).DebitCreditIndicator = AuditFileSourceDocumentsPaymentsPaymentLineDebitCreditIndicator.D
                                If personID > 0 Then payment.Line(0).CustomerID = personID.ToString
                                debitSum = CRound(debitSum + Convert.ToDouble(payment.GrossTotal), 2)
                            Else
                                payment.Line(0).DebitCreditIndicator = AuditFileSourceDocumentsPaymentsPaymentLineDebitCreditIndicator.K
                                If personID > 0 Then payment.Line(0).SupplierID = personID.ToString
                                creditSum = CRound(creditSum + Convert.ToDouble(payment.GrossTotal), 2)
                            End If
                            payment.Line(0).Description = payment.Description
                            payment.Line(0).LineNumber = id.ToString & "-1"

                        ElseIf payment.PaymentMethod = AuditFileSourceDocumentsPaymentsPaymentPaymentMethod.U Then

                            Dim lines As New List(Of AuditFileSourceDocumentsPaymentsPaymentLine)

                            For Each dd As DataRow In detailsData.Rows
                                If CIntSafe(dd.Item(0), 0) = id Then

                                    Dim line As New AuditFileSourceDocumentsPaymentsPaymentLine
                                    payment.Line(0).LineNumber = id.ToString & "-" & CIntSafe(dd.Item(1), 0).ToString
                                    payment.Line(0).PaymentLineAmount = New AmountStructure
                                    payment.Line(0).PaymentLineAmount.Amount = Convert.ToDecimal(
                                        CDblSafe(dd.Item(2), 2, 0.0))
                                    payment.Line(0).PaymentLineAmount.CurrencyAmount = Convert.ToDecimal(
                                        CDblSafe(dd.Item(3), 2, 0.0))
                                    payment.Line(0).PaymentLineAmount.CurrencyCode = CStrSafe(dd.Item(4))
                                    payment.Line(0).AccountID = CLongSafe(dd.Item(5), 0).ToString
                                    payment.Line(0).Description = CStrSafe(dd.Item(6))
                                    Dim lineType As BookEntryType = ConvertDatabaseCharID(Of BookEntryType)(CStrSafe(dd.Item(7)))
                                    Dim personID As Integer = CIntSafe(dd.Item(8), 0)
                                    If lineType = BookEntryType.Debetas Then
                                        payment.Line(0).DebitCreditIndicator = AuditFileSourceDocumentsPaymentsPaymentLineDebitCreditIndicator.D
                                        If personID > 0 Then payment.Line(0).CustomerID = personID.ToString
                                        debitSum = CRound(debitSum + Convert.ToDouble(payment.Line(0).PaymentLineAmount.Amount), 2)
                                    Else
                                        payment.Line(0).DebitCreditIndicator = AuditFileSourceDocumentsPaymentsPaymentLineDebitCreditIndicator.K
                                        If personID > 0 Then payment.Line(0).SupplierID = personID.ToString
                                        creditSum = CRound(creditSum + Convert.ToDouble(payment.Line(0).PaymentLineAmount.Amount), 2)
                                    End If

                                    lines.Add(line)

                                End If
                            Next

                            payment.Line = lines.ToArray

                        Else

                            payment.Line = New AuditFileSourceDocumentsPaymentsPaymentLine() _
                                {New AuditFileSourceDocumentsPaymentsPaymentLine}
                            payment.Line(0).PaymentLineAmount = New AmountStructure
                            payment.Line(0).PaymentLineAmount.Amount = payment.GrossTotal
                            payment.Line(0).PaymentLineAmount.CurrencyAmount = Convert.ToDecimal(
                                CDblSafe(dr.Item(5), 2, 0.0))
                            payment.Line(0).PaymentLineAmount.CurrencyCode = CStrSafe(dr.Item(6))
                            payment.Line(0).AccountID = CLongSafe(dr.Item(7), 0).ToString
                            Dim personID As Integer = CIntSafe(dr.Item(8), 0)
                            If payment.GrossTotal < 0 Then
                                payment.Line(0).DebitCreditIndicator = AuditFileSourceDocumentsPaymentsPaymentLineDebitCreditIndicator.K
                                If personID > 0 Then payment.Line(0).SupplierID = personID.ToString
                                payment.GrossTotal = -payment.GrossTotal
                                creditSum = CRound(creditSum + Convert.ToDouble(payment.GrossTotal), 2)
                            Else
                                payment.Line(0).DebitCreditIndicator = AuditFileSourceDocumentsPaymentsPaymentLineDebitCreditIndicator.D
                                If personID > 0 Then payment.Line(0).CustomerID = personID.ToString
                                debitSum = CRound(debitSum + Convert.ToDouble(payment.GrossTotal), 2)
                            End If
                            payment.Line(0).Description = payment.Description
                            payment.Line(0).LineNumber = id.ToString & "-1"

                        End If

                        result.Add(payment)
                        transactionCount += 1

                    Next

                End Using

            End Using

            Return result.ToArray

        End Function

        Private Function GetStockMovements(ByVal periodStart As Date, ByVal periodEnd As Date,
            ByRef transactionCount As Integer, ByRef quantityReceived As Double,
            ByRef quantityIssued As Double) As AuditFileSourceDocumentsMovementOfGoodsStockMovement()

            Dim result As New List(Of AuditFileSourceDocumentsMovementOfGoodsStockMovement)

            Dim myComm As New SQLCommand("FetchSaftStockMovements_2_0")
            myComm.AddParam("?DF", periodStart)
            myComm.AddParam("?DT", periodEnd)
            Using myData As DataTable = myComm.Fetch

                myComm = New SQLCommand("FetchSaftStockMovementsDetails_2_0")
                myComm.AddParam("?DF", periodStart)
                myComm.AddParam("?DT", periodEnd)

                Using detailsData As DataTable = myComm.Fetch

                    For Each dr As DataRow In myData.Rows

                        Dim document As New AuditFileSourceDocumentsMovementOfGoodsStockMovement

                        document.SystemID = CStrSafe(dr.Item(0))
                        Dim journalEntryID As Integer = CIntSafe(dr.Item(1), 0)
                        If journalEntryID > 0 Then document.GLTransactionID = journalEntryID.ToString
                        document.MovementReference = document.SystemID
                        document.DocumentReference = New AuditFileSourceDocumentsMovementOfGoodsStockMovementDocumentReference
                        document.DocumentReference.DocumentNumber = CStrSafe(dr.Item(2))
                        document.DocumentReference.DocumentType = ConvertLocalizedName(
                            ConvertDatabaseCharID(Of DocumentType)(CStrSafe(dr.Item(3))))
                        document.MovementDate = CDateSafe(dr.Item(4), periodEnd)
                        document.TaxPointDate = document.MovementDate.Value
                        document.TaxPointDateSpecified = True
                        Dim isComplexDocument As Boolean = ConvertDbBoolean(CIntSafe(dr.Item(5), 0))

                        If isComplexDocument Then

                            Dim complexType As Goods.GoodsComplexOperationType =
                                ConvertDatabaseID(Of Goods.GoodsComplexOperationType) _
                                (CIntSafe(dr.Item(6), 0))
                            If complexType = Goods.GoodsComplexOperationType.BulkDiscard Then
                                document.MovementType = AuditFileSourceDocumentsMovementOfGoodsStockMovementMovementType.N
                            ElseIf complexType = Goods.GoodsComplexOperationType.Production Then
                                document.MovementType = AuditFileSourceDocumentsMovementOfGoodsStockMovementMovementType.PP
                            ElseIf complexType = Goods.GoodsComplexOperationType.InternalTransfer Then
                                document.MovementType = AuditFileSourceDocumentsMovementOfGoodsStockMovementMovementType.VP
                            Else
                                document.MovementType = AuditFileSourceDocumentsMovementOfGoodsStockMovementMovementType.KT
                            End If

                            Dim lines As New List(Of AuditFileSourceDocumentsMovementOfGoodsStockMovementLine)

                            For Each dd As DataRow In detailsData.Rows
                                If CStrSafe(dd.Item(0)).Trim.ToLower = document.SystemID.Trim.ToLower Then

                                    Dim line As New AuditFileSourceDocumentsMovementOfGoodsStockMovementLine
                                    line.TransactionID = document.SystemID
                                    line.LineNumber = document.SystemID & "-" & CStrSafe(dd.Item(1))
                                    line.ProductCode = "PR" & CIntSafe(dd.Item(2), 0).ToString
                                    line.AccountID = CLongSafe(dd.Item(3), 0).ToString
                                    line.Quantity = Convert.ToDecimal(Math.Abs(CDblSafe(dd.Item(4), 2, 0.0)))
                                    line.BookValue = Convert.ToDecimal(Math.Abs(CDblSafe(dd.Item(5), 2, 0.0)))
                                    line.MovementComments = CStrSafe(dd.Item(6))

                                    Dim simpleType As Goods.GoodsOperationType =
                                        ConvertDatabaseID(Of Goods.GoodsOperationType) _
                                        (CIntSafe(dd.Item(7), 0))
                                    line.MovementSubType = ConvertLocalizedName(simpleType)
                                    If complexType = Goods.GoodsComplexOperationType.BulkDiscard Then
                                        line.MovementType = AuditFileSourceDocumentsMovementOfGoodsStockMovementLineMovementType.N
                                        quantityIssued = CRound(quantityIssued + Convert.ToDouble(line.Quantity.Value))
                                    ElseIf complexType = Goods.GoodsComplexOperationType.Production Then
                                        If simpleType = Goods.GoodsOperationType.Acquisition Then
                                            line.MovementType = AuditFileSourceDocumentsMovementOfGoodsStockMovementLineMovementType.PP
                                            quantityReceived = CRound(quantityReceived + Convert.ToDouble(line.Quantity.Value))
                                        Else
                                            line.MovementType = AuditFileSourceDocumentsMovementOfGoodsStockMovementLineMovementType.N
                                            quantityIssued = CRound(quantityIssued + Convert.ToDouble(line.Quantity.Value))
                                        End If
                                    ElseIf complexType = Goods.GoodsComplexOperationType.InternalTransfer Then
                                        line.MovementType = AuditFileSourceDocumentsMovementOfGoodsStockMovementLineMovementType.VP
                                    Else
                                        line.MovementType = AuditFileSourceDocumentsMovementOfGoodsStockMovementLineMovementType.KT
                                        If CDblSafe(dd.Item(4), 2, 0.0) > 0 Then
                                            quantityReceived = CRound(quantityReceived + Convert.ToDouble(line.Quantity.Value))
                                        Else
                                            quantityIssued = CRound(quantityIssued + Convert.ToDouble(line.Quantity.Value))
                                        End If
                                    End If
                                    line.MovementTypeSpecified = True

                                    lines.Add(line)

                                End If
                            Next

                            document.Line = lines.ToArray

                        Else

                            Dim simpleType As Goods.GoodsOperationType =
                                ConvertDatabaseID(Of Goods.GoodsOperationType) _
                                (CIntSafe(dr.Item(6), 0))
                            If simpleType = Goods.GoodsOperationType.Acquisition Then
                                document.MovementType = AuditFileSourceDocumentsMovementOfGoodsStockMovementMovementType.PIR
                            ElseIf simpleType = Goods.GoodsOperationType.Discard Then
                                document.MovementType = AuditFileSourceDocumentsMovementOfGoodsStockMovementMovementType.N
                            ElseIf simpleType = Goods.GoodsOperationType.RedeemToSupplier Then
                                document.MovementType = AuditFileSourceDocumentsMovementOfGoodsStockMovementMovementType.PRG
                            ElseIf simpleType = Goods.GoodsOperationType.Transfer Then
                                document.MovementType = AuditFileSourceDocumentsMovementOfGoodsStockMovementMovementType.PARD
                            Else
                                document.MovementType = AuditFileSourceDocumentsMovementOfGoodsStockMovementMovementType.KT
                            End If

                            document.Line = New AuditFileSourceDocumentsMovementOfGoodsStockMovementLine() _
                                {New AuditFileSourceDocumentsMovementOfGoodsStockMovementLine}
                            document.Line(0).TransactionID = document.SystemID
                            document.Line(0).MovementSubType = ConvertLocalizedName(simpleType)
                            document.Line(0).LineNumber = document.SystemID & "-1"
                            document.Line(0).ProductCode = "PR" & CIntSafe(dr.Item(7), 0).ToString
                            document.Line(0).AccountID = CLongSafe(dr.Item(8), 0).ToString
                            document.Line(0).Quantity = Convert.ToDecimal(Math.Abs(CDblSafe(dr.Item(9), 2, 0.0)))
                            document.Line(0).BookValue = Convert.ToDecimal(Math.Abs(CDblSafe(dr.Item(10), 2, 0.0)))
                            document.Line(0).MovementComments = CStrSafe(dr.Item(11))

                            Dim personID As Integer = CIntSafe(dr.Item(12), 0)
                            If simpleType = Goods.GoodsOperationType.Acquisition Then
                                document.Line(0).MovementType = AuditFileSourceDocumentsMovementOfGoodsStockMovementLineMovementType.PIR
                                If personID > 0 Then document.Line(0).SupplierID = personID.ToString
                                quantityReceived = CRound(quantityReceived + Convert.ToDouble(document.Line(0).Quantity.Value))
                            ElseIf simpleType = Goods.GoodsOperationType.Discard Then
                                document.Line(0).MovementType = AuditFileSourceDocumentsMovementOfGoodsStockMovementLineMovementType.N
                                quantityIssued = CRound(quantityIssued + Convert.ToDouble(document.Line(0).Quantity.Value))
                            ElseIf simpleType = Goods.GoodsOperationType.RedeemToSupplier Then
                                document.Line(0).MovementType = AuditFileSourceDocumentsMovementOfGoodsStockMovementLineMovementType.PRG
                                If personID > 0 Then document.Line(0).SupplierID = personID.ToString
                                quantityIssued = CRound(quantityIssued + Convert.ToDouble(document.Line(0).Quantity.Value))
                            ElseIf simpleType = Goods.GoodsOperationType.RedeemFromBuyer Then
                                document.Line(0).MovementType = AuditFileSourceDocumentsMovementOfGoodsStockMovementLineMovementType.PG
                                If personID > 0 Then document.Line(0).CustomerID = personID.ToString
                                quantityReceived = CRound(quantityReceived + Convert.ToDouble(document.Line(0).Quantity.Value))
                            ElseIf simpleType = Goods.GoodsOperationType.Transfer Then
                                document.Line(0).MovementType = AuditFileSourceDocumentsMovementOfGoodsStockMovementLineMovementType.PARD
                                If personID > 0 Then document.Line(0).CustomerID = personID.ToString
                                quantityIssued = CRound(quantityIssued + Convert.ToDouble(document.Line(0).Quantity.Value))
                            Else
                                document.Line(0).MovementType = AuditFileSourceDocumentsMovementOfGoodsStockMovementLineMovementType.KT
                                If CDblSafe(dr.Item(9), 2, 0.0) > 0 Then
                                    quantityReceived = CRound(quantityReceived + CDblSafe(dr.Item(9), 2, 0.0))
                                Else
                                    quantityIssued = CRound(quantityIssued - CDblSafe(dr.Item(9), 2, 0.0))
                                End If
                            End If
                            document.Line(0).MovementTypeSpecified = True

                        End If

                        result.Add(document)
                        transactionCount += 1

                    Next

                End Using

            End Using

            Return result.ToArray

        End Function

        Private Function GetAssetTransactions(ByVal periodStart As Date, ByVal periodEnd As Date,
            ByRef transactionCount As Integer) As AuditFileSourceDocumentsAssetTransactionsAssetTransaction()

            Dim result As New List(Of AuditFileSourceDocumentsAssetTransactionsAssetTransaction)

            Dim myComm As New SQLCommand("FetchSaftAssetTransactions_2_0")
            myComm.AddParam("?DF", periodStart)
            myComm.AddParam("?DT", periodEnd)
            Using myData As DataTable = myComm.Fetch

                For Each dr As DataRow In myData.Rows

                    Dim transaction As New AuditFileSourceDocumentsAssetTransactionsAssetTransaction
                    Dim id As Integer = CIntSafe(dr.Item(0), 0)
                    transaction.TransactionID = id.ToString
                    transaction.AssetTransactionID = id.ToString
                    transaction.AssetID = CIntSafe(dr.Item(1), 0).ToString
                    transaction.AssetTransactionDate = CDateSafe(dr.Item(2), periodEnd)
                    transaction.Description = CStrSafe(dr.Item(3))
                    Dim operationTypeCode As String = CStrSafe(dr.Item(4))
                    If String.IsNullOrEmpty(operationTypeCode.Trim) Then
                        transaction.AssetTransactionType = AuditFileSourceDocumentsAssetTransactionsAssetTransactionAssetTransactionType.I
                        transaction.TransactionID = "ITI" & id.ToString
                        transaction.AssetTransactionID = "ITI" & id.ToString
                    Else
                        Dim operationType As Assets.LtaOperationType = ConvertDatabaseCharID(Of Assets.LtaOperationType) _
                       (operationTypeCode)
                        If operationType = Assets.LtaOperationType.Discard Then
                            transaction.AssetTransactionType = AuditFileSourceDocumentsAssetTransactionsAssetTransactionAssetTransactionType.NUR
                        ElseIf operationType = Assets.LtaOperationType.Amortization Then
                            transaction.AssetTransactionType = AuditFileSourceDocumentsAssetTransactionsAssetTransactionAssetTransactionType.NUS
                        Else
                            transaction.AssetTransactionType = AuditFileSourceDocumentsAssetTransactionsAssetTransactionAssetTransactionType.KT
                        End If
                    End If

                    transaction.AssetTransactionValuations = New AuditFileSourceDocumentsAssetTransactionsAssetTransactionAssetTransactionValuation() _
                        {New AuditFileSourceDocumentsAssetTransactionsAssetTransactionAssetTransactionValuation}
                    transaction.AssetTransactionValuations(0).AssetTransactionAmount = Convert.ToDecimal(CDblSafe(dr.Item(5), 2, 0.0))
                    transaction.AssetTransactionValuations(0).AssetValuationType = AuditFileSourceDocumentsAssetTransactionsAssetTransactionAssetTransactionValuationAssetValuationType.IS
                    transaction.AssetTransactionValuations(0).AssetValuationTypeSpecified = True
                    transaction.AssetTransactionValuations(0).AcquisitionAndProductionCostsOnTransaction = Convert.ToDecimal(
                        CRound(CDblSafe(dr.Item(6), 2, 0.0) + CDblSafe(dr.Item(7), 2, 0.0)))
                    transaction.AssetTransactionValuations(0).BookValueOnTransaction = Convert.ToDecimal(
                        CRound(CDblSafe(dr.Item(8), 2, 0.0) + CDblSafe(dr.Item(9), 2, 0.0)))

                    result.Add(transaction)

                Next

            End Using

            Return result.ToArray

        End Function

    End Class

End Namespace