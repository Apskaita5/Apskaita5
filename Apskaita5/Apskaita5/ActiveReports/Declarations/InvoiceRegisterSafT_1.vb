﻿Imports System.Text
Imports System.Xml.Serialization
Imports System.Xml

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
                Return "SAF-T v. 1.1"
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
        ''' Gets an XML representation of the report.
        ''' </summary>
        ''' <param name="invoiceRegister">an invoice register report to be exported</param>
        ''' <param name="softwareVersion">current version of the application</param>
        ''' <remarks></remarks>
        Public Function GetXmlString(ByVal invoiceRegister As InvoiceInfoItemList, _
            ByVal softwareVersion As String) As String _
            Implements IInvoiceRegisterSafT.GetXmlString

            If invoiceRegister Is Nothing Then
                Throw New ArgumentNullException("invoiceRegister")
            ElseIf invoiceRegister.Count < 1 Then
                Throw New ArgumentException("List cannot be empty.")
            End If

            Dim result As New SafTTemplates.SafT_1_1.iSAFFile

            result.Header = New SafTTemplates.SafT_1_1.Header()
            result.Header.FileDescription = New SafTTemplates.SafT_1_1.FileDescription()
            If invoiceRegister.InfoType = InvoiceInfoType.InvoiceMade Then
                result.Header.FileDescription.DataType = _
                    SafTTemplates.SafT_1_1.ISAFDataType.S
            Else
                result.Header.FileDescription.DataType = _
                    SafTTemplates.SafT_1_1.ISAFDataType.P
            End If
            result.Header.FileDescription.FileDateCreated = Now
            result.Header.FileDescription.FileVersion = _
                SafTTemplates.SafT_1_1.ISAFFileVersion.iSAF11
            result.Header.FileDescription.NumberOfParts = 1
            result.Header.FileDescription.PartNumber = "BENDRAS"
            result.Header.FileDescription.RegistrationNumber = _
                Convert.ToUInt64(GetCurrentCompany.Code)
            result.Header.FileDescription.SoftwareCompanyName = "Marius Dagys"
            result.Header.FileDescription.SoftwareName = "Apskaita5"
            result.Header.FileDescription.SoftwareVersion = softwareVersion

            result.Header.FileDescription.SelectionCriteria = _
                New SafTTemplates.SafT_1_1.SelectionCriteria()
            result.Header.FileDescription.SelectionCriteria.SelectionStartDate _
                = invoiceRegister.DateFrom
            result.Header.FileDescription.SelectionCriteria.SelectionEndDate _
                = invoiceRegister.DateTo

            result.SourceDocuments = New SafTTemplates.SafT_1_1.SourceDocuments()
            result.MasterFiles = New SafTTemplates.SafT_1_1.MasterFiles()

            If invoiceRegister.InfoType = InvoiceInfoType.InvoiceMade Then
                result.SourceDocuments.SalesInvoices = GetSalesInvoices(invoiceRegister)
                result.MasterFiles.Customers = GetCustomers( _
                    result.SourceDocuments.SalesInvoices)
            Else
                result.SourceDocuments.PurchaseInvoices = GetPurchaseInvoices(invoiceRegister)
                result.MasterFiles.Suppliers = GetSuppliers( _
                    result.SourceDocuments.PurchaseInvoices)
            End If

            Dim enc As Encoding = Encoding.UTF8
            Dim serializer As New XmlSerializer(result.GetType())
            Dim settings As New XmlWriterSettings

            settings.Indent = True
            settings.IndentChars = " "
            settings.Encoding = enc

            Using ms As New IO.MemoryStream
                Using writer As XmlWriter = XmlWriter.Create(ms, settings)
                    serializer.Serialize(writer, result)
                    Return enc.GetString(ms.ToArray())
                End Using
            End Using

        End Function


        Private Function GetPurchaseInvoices(ByVal invoiceRegister As InvoiceInfoItemList) _
            As SafTTemplates.SafT_1_1.PurchaseInvoice()

            Dim result As New List(Of SafTTemplates.SafT_1_1.PurchaseInvoice)

            For Each item As InvoiceInfoItem In invoiceRegister

                Dim invoice As New SafTTemplates.SafT_1_1.PurchaseInvoice

                invoice.References = New SafTTemplates.SafT_1_1.Reference() {}
                invoice.InvoiceDate = item.ActualDate
                invoice.InvoiceNo = item.Number
                If item.InvoiceType = Documents.InvoiceType.Credit Then
                    invoice.InvoiceType = SafTTemplates.SafT_1_1.ISAFshorttext2Type.KS
                ElseIf item.InvoiceType = Documents.InvoiceType.Debit Then
                    invoice.InvoiceType = SafTTemplates.SafT_1_1.ISAFshorttext2Type.DS
                Else
                    invoice.InvoiceType = SafTTemplates.SafT_1_1.ISAFshorttext2Type.SF
                End If
                invoice.RegistrationAccountDate = item.Date
                ' invoice.VATPointDate= Prekių gavimo arba paslaugų gavimo data,
                ' jeigu ji nesutampa su PVM sąskaitos faktūros išrašymo data.
                ' Elemento reikšmė gali būti nepildoma, jei ši data nenurodyta(PVM)
                ' sąskaitoje faktūroje / nefiksuojama(apskaitoje)/ sutampa su PVM
                ' sąskaitos(faktūros)išrašymo data.

                invoice.SupplierInfo = New SafTTemplates.SafT_1_1.SupplierInfo()
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

                Dim subtotals As New List(Of SafTTemplates.SafT_1_1.PurchaseDocumentTotal)
                For Each subitem As InvoiceSubtotalItem In item.SubtotalList

                    Dim subtotal As New SafTTemplates.SafT_1_1.PurchaseDocumentTotal

                    subtotal.Amount = Convert.ToDecimal(subitem.TaxAmount)
                    subtotal.TaxCode = subitem.TaxCode.Trim.ToUpper()
                    If String.IsNullOrEmpty(subtotal.TaxCode) Then
                        subtotal.TaxCode = "PVM1"
                    End If
                    subtotal.TaxPercentage = Convert.ToDecimal(subitem.TaxPercentage)
                    subtotal.TaxableValue = Convert.ToDecimal(subitem.TaxableValue)

                    subtotals.Add(subtotal)

                Next

                invoice.DocumentTotals = subtotals.ToArray()

                result.Add(invoice)

            Next

            Return result.ToArray()

        End Function

        Private Function GetSalesInvoices(ByVal invoiceRegister As InvoiceInfoItemList) _
            As SafTTemplates.SafT_1_1.SalesInvoice()

            Dim result As New List(Of SafTTemplates.SafT_1_1.SalesInvoice)

            For Each item As InvoiceInfoItem In invoiceRegister

                Dim invoice As New SafTTemplates.SafT_1_1.SalesInvoice

                invoice.References = New SafTTemplates.SafT_1_1.Reference() {}
                invoice.InvoiceDate = item.Date
                invoice.InvoiceNo = item.Number
                ' invoice.InvoiceType = item.Type

                ' invoice.VATPointDate= Prekių gavimo arba paslaugų gavimo data,
                ' jeigu ji nesutampa su PVM sąskaitos faktūros išrašymo data.
                ' Elemento reikšmė gali būti nepildoma, jei ši data nenurodyta(PVM)
                ' sąskaitoje faktūroje / nefiksuojama(apskaitoje)/ sutampa su PVM
                ' sąskaitos(faktūros)išrašymo data.

                invoice.CustomerInfo = New SafTTemplates.SafT_1_1.CustomerInfo()
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

                Dim subtotals As New List(Of SafTTemplates.SafT_1_1.SalesDocumentTotal)
                For Each subitem As InvoiceSubtotalItem In item.SubtotalList

                    Dim subtotal As New SafTTemplates.SafT_1_1.SalesDocumentTotal

                    subtotal.Amount = Convert.ToDecimal(subitem.TaxAmount)
                    subtotal.TaxCode = subitem.TaxCode.Trim.ToUpper()
                    If String.IsNullOrEmpty(subtotal.TaxCode) Then
                        subtotal.TaxCode = "PVM1"
                    End If
                    subtotal.TaxPercentage = Convert.ToDecimal(subitem.TaxPercentage)
                    subtotal.TaxableValue = Convert.ToDecimal(subitem.TaxableValue)

                    subtotals.Add(subtotal)

                Next

                invoice.DocumentTotals = subtotals.ToArray()

                result.Add(invoice)

            Next

            Return result.ToArray()

        End Function

        Private Function GetCustomers(ByVal salesInvoices As SafTTemplates.SafT_1_1.SalesInvoice()) _
            As SafTTemplates.SafT_1_1.Customer()

            Dim result As New List(Of SafTTemplates.SafT_1_1.Customer)

            Dim personAlreadyAdded As Boolean

            For Each invoice As SafTTemplates.SafT_1_1.SalesInvoice In salesInvoices

                personAlreadyAdded = False

                For Each item As SafTTemplates.SafT_1_1.Customer In result
                    If item.CustomerID = invoice.CustomerInfo.CustomerID Then
                        personAlreadyAdded = True
                        Exit For
                    End If
                Next

                If Not personAlreadyAdded Then

                    Dim newPerson As New SafTTemplates.SafT_1_1.Customer
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

        Private Function GetSuppliers(ByVal purchaseInvoices As SafTTemplates.SafT_1_1.PurchaseInvoice()) _
            As SafTTemplates.SafT_1_1.Supplier()

            Dim result As New List(Of SafTTemplates.SafT_1_1.Supplier)

            Dim personAlreadyAdded As Boolean

            For Each invoice As SafTTemplates.SafT_1_1.PurchaseInvoice In purchaseInvoices

                personAlreadyAdded = False

                For Each item As SafTTemplates.SafT_1_1.Supplier In result
                    If item.SupplierID = invoice.SupplierInfo.SupplierID Then
                        personAlreadyAdded = True
                        Exit For
                    End If
                Next

                If Not personAlreadyAdded Then

                    Dim newPerson As New SafTTemplates.SafT_1_1.Supplier
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

    End Class

End Namespace