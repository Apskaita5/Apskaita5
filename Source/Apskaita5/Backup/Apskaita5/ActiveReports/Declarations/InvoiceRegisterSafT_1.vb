Imports System.Text
Imports System.Xml.Serialization
Imports System.Xml
Imports ApskaitaObjects.ActiveReports.Declarations.SafTTemplates.SafT_1_2
Imports ApskaitaObjects.My.Resources

Namespace ActiveReports.Declarations

    ''' <summary>
    ''' Represents an implementation of an <see cref="IInvoiceRegisterSafT">IInvoiceRegisterSafT</see>
    ''' for a state tax inspectorate (VMI) SAF-T report version 1.1.
    ''' </summary>
    ''' <remarks>Object is responsible for exporting the <see cref="InvoiceInfoItemList">
    ''' invoice register report</see> data to XML format.</remarks>
    Public Class InvoiceRegisterSafT_1
        Implements IInvoiceRegisterSafT

        Private Const NullValueString As String = "ND"


        ''' <summary>
        ''' Gets a name of the invoice register report.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Name() As String _
            Implements IInvoiceRegisterSafT.Name
            Get
                Return "SAF-T v. 1.2"
            End Get
        End Property

        ''' <summary>
        ''' Gets a start of the period that the invoice register report is valid for.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property ValidFrom() As Date _
            Implements IInvoiceRegisterSafT.ValidFrom
            Get
                Return Date.MinValue
            End Get
        End Property

        ''' <summary>
        ''' Gets an end of the period that the invoice register report is valid for.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property ValidTo() As Date _
            Implements IInvoiceRegisterSafT.ValidTo
            Get
                Return Date.MaxValue
            End Get
        End Property

        ''' <summary>
        ''' Gets a name of the xsd file that defines the report xml requirements.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property XsdFileName() As String _
            Implements IInvoiceRegisterSafT.XsdFileName
            Get
                Return "MXFD\isaf_1_2.xsd"
            End Get
        End Property


        ''' <summary>
        ''' Gets an XML representation of the report.
        ''' </summary>
        ''' <param name="invoiceRegister">an invoice register report to be exported</param>
        ''' <param name="softwareVersion">a current version of the application</param>
        ''' <param name="selectedInvoicesIds">id's of the invoices to be exported 
        ''' (null or empty to export all the invoices)</param>
        ''' <remarks></remarks>
        Public Function GetXmlString(ByVal invoiceRegister As InvoiceInfoItemList, _
            ByVal softwareVersion As String, ByVal selectedInvoicesIds As Integer(), _
            ByRef warnings As String) As String _
            Implements IInvoiceRegisterSafT.GetXmlString

            If invoiceRegister Is Nothing Then
                Throw New ArgumentNullException("invoiceRegister")
            ElseIf invoiceRegister.Count < 1 Then
                Throw New ArgumentException("List cannot be empty.")
            End If

            Dim invoiceType As InvoiceInfoType = GetRegisterType(invoiceRegister)

            ValidateInvoiceData(invoiceRegister, selectedInvoicesIds)

            Dim result As New SafTTemplates.SafT_1_2.iSAFFile

            result.Header = New SafTTemplates.SafT_1_2.Header()
            result.Header.FileDescription = New SafTTemplates.SafT_1_2.FileDescription()
            If invoiceType = InvoiceInfoType.InvoiceMade Then
                result.Header.FileDescription.DataType = _
                    SafTTemplates.SafT_1_2.ISAFDataType.S
            Else
                result.Header.FileDescription.DataType = _
                    SafTTemplates.SafT_1_2.ISAFDataType.P
            End If
            result.Header.FileDescription.FileDateCreated = Now
            result.Header.FileDescription.FileVersion = _
                SafTTemplates.SafT_1_2.ISAFFileVersion.iSAF12
            result.Header.FileDescription.NumberOfParts = 2
            result.Header.FileDescription.PartNumber = invoiceType.ToString().ToUpper()
            result.Header.FileDescription.RegistrationNumber = _
                Convert.ToUInt64(GetCurrentCompany.Code)
            result.Header.FileDescription.SoftwareCompanyName = "Marius Dagys"
            result.Header.FileDescription.SoftwareName = "Apskaita5"
            result.Header.FileDescription.SoftwareVersion = softwareVersion

            result.Header.FileDescription.SelectionCriteria = _
                New SafTTemplates.SafT_1_2.SelectionCriteria()
            result.Header.FileDescription.SelectionCriteria.SelectionStartDate _
                = invoiceRegister.DateFrom
            result.Header.FileDescription.SelectionCriteria.SelectionEndDate _
                = invoiceRegister.DateTo

            result.SourceDocuments = New SafTTemplates.SafT_1_2.SourceDocuments()
            result.MasterFiles = New SafTTemplates.SafT_1_2.MasterFiles()

            If invoiceType = InvoiceInfoType.InvoiceMade Then
                result.SourceDocuments.SalesInvoices = GetSalesInvoices(invoiceRegister, selectedInvoicesIds)
                result.MasterFiles.Customers = GetCustomers( _
                    result.SourceDocuments.SalesInvoices)
            Else
                result.SourceDocuments.PurchaseInvoices = GetPurchaseInvoices(invoiceRegister, selectedInvoicesIds)
                result.MasterFiles.Suppliers = GetSuppliers( _
                    result.SourceDocuments.PurchaseInvoices)
            End If

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

            Dim errorMessage As String = ""

            If Not XmlValidationErrorBuilder.Validate(resultString, _
                IO.Path.Combine(AppPath(), Me.XsdFileName), _
                "http://www.vmi.lt/cms/imas/isaf", errorMessage, warnings) Then
                Throw New Exception(errorMessage)
            End If

            Return resultString

        End Function


        Private Function GetPurchaseInvoices(ByVal invoiceRegister As InvoiceInfoItemList, _
            ByVal selectedInvoicesIds As Integer()) As SafTTemplates.SafT_1_2.PurchaseInvoice()

            Dim result As New List(Of SafTTemplates.SafT_1_2.PurchaseInvoice)

            For Each item As InvoiceInfoItem In invoiceRegister

                If selectedInvoicesIds Is Nothing OrElse selectedInvoicesIds.Length < 1 OrElse _
                    Not Array.IndexOf(selectedInvoicesIds, item.ID) < 0 Then

                    Dim invoice As New SafTTemplates.SafT_1_2.PurchaseInvoice

                    invoice.References = New SafTTemplates.SafT_1_2.Reference() {}
                    invoice.InvoiceDate = item.ActualDate
                    invoice.InvoiceNo = item.Number
                    If item.InvoiceType = Documents.InvoiceType.Credit Then
                        invoice.InvoiceType = SafTTemplates.SafT_1_2.ISAFshorttext2Type.KS
                    ElseIf item.InvoiceType = Documents.InvoiceType.Debit Then
                        invoice.InvoiceType = SafTTemplates.SafT_1_2.ISAFshorttext2Type.DS
                    Else
                        invoice.InvoiceType = SafTTemplates.SafT_1_2.ISAFshorttext2Type.SF
                    End If
                    invoice.RegistrationAccountDate = item.Date
                    invoice.SpecialTaxation = ISAFSpecialTaxationType.Item
                    ' invoice.VATPointDate= Prekių gavimo arba paslaugų gavimo data,
                    ' jeigu ji nesutampa su PVM sąskaitos faktūros išrašymo data.
                    ' Elemento reikšmė gali būti nepildoma, jei ši data nenurodyta(PVM)
                    ' sąskaitoje faktūroje / nefiksuojama(apskaitoje)/ sutampa su PVM
                    ' sąskaitos(faktūros)išrašymo data.

                    invoice.SupplierInfo = New SafTTemplates.SafT_1_2.SupplierInfo()
                    invoice.SupplierInfo.SupplierID = item.PersonID.ToString()
                    invoice.SupplierInfo.Name = item.PersonName
                    If StringIsNullOrEmpty(item.PersonVatCode) Then
                        invoice.SupplierInfo.VATRegistrationNumber = NullValueString
                    Else
                        invoice.SupplierInfo.VATRegistrationNumber = item.PersonVatCode
                    End If
                    If item.CodeIsNotReal Then
                        invoice.SupplierInfo.RegistrationNumber = NullValueString
                    Else
                        invoice.SupplierInfo.RegistrationNumber = item.PersonCode
                    End If
                    invoice.SupplierInfo.Country = item.StateCode.Trim.ToUpper()

                    Dim subtotals As New List(Of SafTTemplates.SafT_1_2.PurchaseDocumentTotal)
                    For Each subitem As InvoiceSubtotalItem In item.SubtotalList

                        Dim subtotal As New SafTTemplates.SafT_1_2.PurchaseDocumentTotal

                        subtotal.Amount = Convert.ToDecimal(subitem.TaxAmount)
                        subtotal.TaxCode = subitem.TaxCode.Trim.ToUpper()
                        If StringIsNullOrEmpty(subtotal.TaxCode) Then
                            subtotal.TaxCode = "PVM1"
                        End If
                        If subitem.VatRateIsNull Then
                            subtotal.TaxPercentage = Nothing
                        Else
                            subtotal.TaxPercentage = Convert.ToDecimal(subitem.TaxPercentage)
                        End If
                        subtotal.TaxableValue = Convert.ToDecimal(subitem.TaxableValue)

                        subtotals.Add(subtotal)

                    Next

                    invoice.DocumentTotals = subtotals.ToArray()

                    result.Add(invoice)

                End If

            Next

            Return result.ToArray()

        End Function

        Private Function GetSalesInvoices(ByVal invoiceRegister As InvoiceInfoItemList, _
            ByVal selectedInvoicesIds As Integer()) As SafTTemplates.SafT_1_2.SalesInvoice()

            Dim result As New List(Of SafTTemplates.SafT_1_2.SalesInvoice)

            For Each item As InvoiceInfoItem In invoiceRegister

                If selectedInvoicesIds Is Nothing OrElse selectedInvoicesIds.Length < 1 OrElse _
                    Not Array.IndexOf(selectedInvoicesIds, item.ID) < 0 Then

                    Dim invoice As New SafTTemplates.SafT_1_2.SalesInvoice

                    invoice.References = New SafTTemplates.SafT_1_2.Reference() {}
                    invoice.InvoiceDate = item.Date
                    invoice.InvoiceNo = item.Number
                    If item.InvoiceType = Documents.InvoiceType.Credit Then
                        invoice.InvoiceType = SafTTemplates.SafT_1_2.ISAFshorttext2Type.KS
                    ElseIf item.InvoiceType = Documents.InvoiceType.Debit Then
                        invoice.InvoiceType = SafTTemplates.SafT_1_2.ISAFshorttext2Type.DS
                    Else
                        invoice.InvoiceType = SafTTemplates.SafT_1_2.ISAFshorttext2Type.SF
                    End If

                    ' invoice.VATPointDate= Prekių gavimo arba paslaugų gavimo data,
                    ' jeigu ji nesutampa su PVM sąskaitos faktūros išrašymo data.
                    ' Elemento reikšmė gali būti nepildoma, jei ši data nenurodyta(PVM)
                    ' sąskaitoje faktūroje / nefiksuojama(apskaitoje)/ sutampa su PVM
                    ' sąskaitos(faktūros)išrašymo data.

                    invoice.CustomerInfo = New SafTTemplates.SafT_1_2.CustomerInfo()
                    invoice.CustomerInfo.CustomerID = item.PersonID.ToString()
                    invoice.CustomerInfo.Name = item.PersonName
                    If StringIsNullOrEmpty(item.PersonVatCode) Then
                        invoice.CustomerInfo.VATRegistrationNumber = NullValueString
                    Else
                        invoice.CustomerInfo.VATRegistrationNumber = item.PersonVatCode
                    End If
                    If item.CodeIsNotReal Then
                        invoice.CustomerInfo.RegistrationNumber = NullValueString
                    Else
                        invoice.CustomerInfo.RegistrationNumber = item.PersonCode
                    End If
                    invoice.CustomerInfo.Country = item.StateCode.Trim.ToUpper()
                    invoice.SpecialTaxation = ISAFSpecialTaxationType.Item

                    Dim subtotals As New List(Of SafTTemplates.SafT_1_2.SalesDocumentTotal)
                    For Each subitem As InvoiceSubtotalItem In item.SubtotalList

                        Dim subtotal As New SafTTemplates.SafT_1_2.SalesDocumentTotal

                        subtotal.Amount = Convert.ToDecimal(subitem.TaxAmount)
                        subtotal.TaxCode = subitem.TaxCode.Trim.ToUpper()
                        If String.IsNullOrEmpty(subtotal.TaxCode) Then
                            subtotal.TaxCode = "PVM1"
                        End If
                        If subitem.VatRateIsNull Then
                            subtotal.TaxPercentage = Nothing
                        Else
                            subtotal.TaxPercentage = Convert.ToDecimal(subitem.TaxPercentage)
                        End If
                        subtotal.TaxableValue = Convert.ToDecimal(subitem.TaxableValue)

                        subtotals.Add(subtotal)

                    Next

                    invoice.DocumentTotals = subtotals.ToArray()

                    result.Add(invoice)

                End If

            Next

            Return result.ToArray()

        End Function

        Private Function GetCustomers(ByVal salesInvoices As SafTTemplates.SafT_1_2.SalesInvoice()) _
            As SafTTemplates.SafT_1_2.Customer()

            Dim result As New List(Of SafTTemplates.SafT_1_2.Customer)

            Dim personAlreadyAdded As Boolean

            For Each invoice As SafTTemplates.SafT_1_2.SalesInvoice In salesInvoices

                personAlreadyAdded = False

                For Each item As SafTTemplates.SafT_1_2.Customer In result
                    If item.CustomerID = invoice.CustomerInfo.CustomerID Then
                        personAlreadyAdded = True
                        Exit For
                    End If
                Next

                If Not personAlreadyAdded Then

                    Dim newPerson As New SafTTemplates.SafT_1_2.Customer
                    newPerson.Country = invoice.CustomerInfo.Country
                    newPerson.CustomerID = invoice.CustomerInfo.CustomerID
                    newPerson.Name = invoice.CustomerInfo.Name
                    newPerson.RegistrationNumber = invoice.CustomerInfo.RegistrationNumber
                    newPerson.VATRegistrationNumber = invoice.CustomerInfo.VATRegistrationNumber

                    result.Add(newPerson)

                End If

            Next

            Return result.ToArray()

        End Function

        Private Function GetSuppliers(ByVal purchaseInvoices As SafTTemplates.SafT_1_2.PurchaseInvoice()) _
            As SafTTemplates.SafT_1_2.Supplier()

            Dim result As New List(Of SafTTemplates.SafT_1_2.Supplier)

            Dim personAlreadyAdded As Boolean

            For Each invoice As SafTTemplates.SafT_1_2.PurchaseInvoice In purchaseInvoices

                personAlreadyAdded = False

                For Each item As SafTTemplates.SafT_1_2.Supplier In result
                    If item.SupplierID = invoice.SupplierInfo.SupplierID Then
                        personAlreadyAdded = True
                        Exit For
                    End If
                Next

                If Not personAlreadyAdded Then

                    Dim newPerson As New SafTTemplates.SafT_1_2.Supplier
                    newPerson.Country = invoice.SupplierInfo.Country
                    newPerson.SupplierID = invoice.SupplierInfo.SupplierID
                    newPerson.Name = invoice.SupplierInfo.Name
                    newPerson.RegistrationNumber = invoice.SupplierInfo.RegistrationNumber
                    newPerson.VATRegistrationNumber = invoice.SupplierInfo.VATRegistrationNumber

                    result.Add(newPerson)

                End If

            Next

            Return result.ToArray()

        End Function

        Private Function GetRegisterType(ByVal invoiceRegister As InvoiceInfoItemList) As InvoiceInfoType

            If invoiceRegister.InfoTypes.Length > 1 Then
                Throw New Exception(ActiveReports_Declarations_InvoiceRegisterSafT_1_MixedTypesNotSupported)
            End If

            Dim result As InvoiceInfoType = invoiceRegister.InfoTypes(0)

            If result <> InvoiceInfoType.InvoiceMade AndAlso result <> InvoiceInfoType.InvoiceReceived Then
                Throw New Exception(ActiveReports_Declarations_InvoiceRegisterSafT_1_InvalidInvoiceType)
            End If

            Return result

        End Function

        Private Sub ValidateInvoiceData(ByVal invoiceRegister As InvoiceInfoItemList, _
            ByVal selectedInvoicesIds As Integer())

            Dim errors As String = ""
            For Each item As InvoiceInfoItem In invoiceRegister
                If selectedInvoicesIds Is Nothing OrElse selectedInvoicesIds.Length < 1 _
                    OrElse Not Array.IndexOf(selectedInvoicesIds, item.ID) < 0 Then

                    If item.SubtotalList.Count < 1 Then
                        errors = AddWithNewLine(errors, String.Format(ActiveReports_Declarations_InvoiceRegisterSafT_1_VatSchemaNull, _
                            item.Date.ToString("yyyy-MM-dd"), item.Number), False)
                    Else
                        For Each o As InvoiceSubtotalItem In item.SubtotalList
                            If StringIsNullOrEmpty(o.TaxCode) Then
                                errors = AddWithNewLine(errors, String.Format(ActiveReports_Declarations_InvoiceRegisterSafT_1_VatSchemaWithoutVatCode, _
                                    item.Date.ToString("yyyy-MM-dd"), item.Number), False)
                            ElseIf Not ValidateVatCode(o.TaxCode) Then
                                errors = AddWithNewLine(errors, String.Format(ActiveReports_Declarations_InvoiceRegisterSafT_1_VatCodeInvalid, _
                                    item.Date.ToString("yyyy-MM-dd"), item.Number, o.TaxCode), False)
                            End If
                        Next
                    End If

                End If
            Next

            If Not String.IsNullOrEmpty(errors) Then
                Throw New Exception(String.Format(ActiveReports_Declarations_InvoiceRegisterSafT_1_InvoiceDataInvalid, vbCrLf, errors))
            End If

        End Sub

        Private Function ValidateVatCode(ByVal code As String) As Boolean

            If code.Length < 4 OrElse code.Length > 6 OrElse Not code.ToUpper.StartsWith("PVM") Then Return False

            For i As Integer = 3 To code.Length - 1
                If Not Char.IsNumber(code(i)) Then Return False
            Next

            Return True

        End Function

    End Class

End Namespace