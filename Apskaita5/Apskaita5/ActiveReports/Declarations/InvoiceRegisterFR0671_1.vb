Namespace ActiveReports.Declarations

    ''' <summary>
    ''' Represents an implementation of an <see cref="IInvoiceRegisterDeclaration">IInvoiceRegisterDeclaration</see>
    ''' for a state tax inspectorate (VMI) report No. FR0671 version 1.
    ''' </summary>
    ''' <remarks>Object is responsible for exporting the <see cref="InvoiceInfoItemList">
    ''' invoice register report</see> data to ffdata format (required by the 
    ''' FormFiller application).</remarks>
    <Serializable()> _
    Public Class InvoiceRegisterFR0671_1
        Implements IInvoiceRegisterDeclaration

        Private Const DECLARATION_NAME As String = "FR0671 v.1"

        ''' <summary>
        ''' Gets a name of the invoice register declaration.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Name() As String Implements IInvoiceRegisterDeclaration.Name
            Get
                Return DECLARATION_NAME
            End Get
        End Property

        ''' <summary>
        ''' Gets a type of the invoice register (made or received) 
        ''' that the declaration is valid for.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Type() As InvoiceInfoType Implements IInvoiceRegisterDeclaration.Type
            Get
                Return InvoiceInfoType.InvoiceReceived
            End Get
        End Property

        ''' <summary>
        ''' Gets a start of the period that the invoice register declaration is valid for.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property ValidFrom() As Date Implements IInvoiceRegisterDeclaration.ValidFrom
            Get
                Return Date.MinValue
            End Get
        End Property

        ''' <summary>
        ''' Gets an end of the period that the invoice register declaration is valid for.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property ValidTo() As Date Implements IInvoiceRegisterDeclaration.ValidTo
            Get
                Return New Date(2013, 12, 31)
            End Get
        End Property

        ''' <summary>
        ''' Gets a ffdata format dataset.
        ''' </summary>
        ''' <param name="invoiceRegister">an invoice register report to be exported.</param>
        ''' <remarks></remarks>
        Public Function GetFfDataDataSet(ByVal invoiceRegister As InvoiceInfoItemList) As DataSet _
            Implements IInvoiceRegisterDeclaration.GetFfDataDataSet

            Dim i As Integer
            Dim currentUser As AccDataAccessLayer.Security.AccIdentity = GetCurrentIdentity()
            Dim currentCompany As Settings.CompanyInfo = GetCurrentCompany()

            Dim declarationFileName As String = AppPath() & FILENAMEFFDATAFR0671

            Dim pageCount As Integer = Convert.ToInt32(Math.Ceiling((invoiceRegister.Count - 14) / 8))

            If pageCount > 0 Then

                Dim myDoc As New Xml.XmlDocument
                myDoc.Load(declarationFileName)

                For i = 1 To pageCount
                    Dim addP As Xml.XmlElement = DirectCast(myDoc.ChildNodes(1).ChildNodes(0).ChildNodes(0). _
                        ChildNodes(0).ChildNodes(1).Clone, Xml.XmlElement)
                    myDoc.ChildNodes(1).ChildNodes(0).ChildNodes(0).ChildNodes(0).AppendChild(addP)
                    Dim addPg As Xml.XmlElement = DirectCast(myDoc.ChildNodes(1).ChildNodes(0). _
                        ChildNodes(1).ChildNodes(1).Clone, Xml.XmlElement)
                    myDoc.ChildNodes(1).ChildNodes(0).ChildNodes(1).AppendChild(addPg)
                    myDoc.ChildNodes(1).ChildNodes(0).ChildNodes(1).ChildNodes(i + 1).Attributes(1).Value = (2 + i).ToString
                Next

                myDoc.ChildNodes(1).ChildNodes(0).ChildNodes(1).Attributes(0).Value = _
                    (2 + pageCount).ToString

                myDoc.Save(AppPath() & FILENAMEFFDATATEMP)

            Else
                IO.File.Copy(declarationFileName, AppPath() & FILENAMEFFDATATEMP)
            End If

            ' read ffdata xml structure to dataset
            Dim formDataSet As New DataSet
            Using formFileStream As IO.FileStream = New IO.FileStream( _
                AppPath() & FILENAMEFFDATATEMP, IO.FileMode.Open)
                formDataSet.ReadXml(formFileStream)
                formFileStream.Close()
            End Using

            ' GENERAL DATA

            formDataSet.Tables(0).Rows(0).Item(3) = currentUser.Name
            formDataSet.Tables(0).Rows(0).Item(4) = GetDateInFFDataFormat(Today.Date)
            formDataSet.Tables(1).Rows(0).Item(2) = AppPath() & FILENAMEMXFDFR0671

            Dim k As Integer = 1
            For i = 1 To formDataSet.Tables(8).Rows.Count ' bendri duomenys
                If formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "B_MM_Pavad" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = currentCompany.Name.ToUpper
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "B_MM_PVM" AndAlso _
                    currentCompany.CodeVat.Trim.Length > 2 Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = currentCompany.CodeVat.Substring(2)
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "B_UzpildData" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetDateInFFDataFormat(Today.Date)
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "B_ML_Nuo" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetDateInFFDataFormat(invoiceRegister.DateFrom.Date)
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "B_ML_Iki" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetDateInFFDataFormat(invoiceRegister.DateTo.Date)
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "E6" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = 1
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "LapoNr" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = k
                    k = k + 1
                End If
            Next

            ' DETAILS DATA

            For i = 1 To Math.Min(6, invoiceRegister.Count)
                formDataSet.Tables(8).Rows((i - 1) * 7 + 5).Item(1) = i
                formDataSet.Tables(8).Rows((i - 1) * 7 + 6).Item(1) = _
                    GetDateInFFDataFormat(invoiceRegister(i - 1).Date)
                formDataSet.Tables(8).Rows((i - 1) * 7 + 7).Item(1) = _
                    invoiceRegister(i - 1).Number.ToUpper
                formDataSet.Tables(8).Rows((i - 1) * 7 + 8).Item(1) = _
                    GetNumberInFFDataFormat(invoiceRegister(i - 1).SumLTL)
                formDataSet.Tables(8).Rows((i - 1) * 7 + 9).Item(1) = _
                    GetNumberInFFDataFormat(invoiceRegister(i - 1).SumVatLTL)
                formDataSet.Tables(8).Rows((i - 1) * 7 + 10).Item(1) = _
                    invoiceRegister(i - 1).PersonVatCode.ToUpper
                formDataSet.Tables(8).Rows((i - 1) * 7 + 11).Item(1) = _
                    GetLimitedLengthString(invoiceRegister(i - 1).PersonName.ToUpper, 36)
            Next

            For j As Integer = 1 To CInt(Math.Max(Math.Ceiling((invoiceRegister.Count - 14) / 8), 0) + 1)
                For i = 1 To Math.Min(8, invoiceRegister.Count - 6 - (j - 1) * 8)
                    formDataSet.Tables(8).Rows((i - 1) * 7 + 52 + 65 * (j - 1)).Item(1) = i + 6 + 8 * (j - 1)
                    formDataSet.Tables(8).Rows((i - 1) * 7 + 53 + 65 * (j - 1)).Item(1) = _
                        GetDateInFFDataFormat(invoiceRegister(i + 5 + 8 * (j - 1)).Date)
                    formDataSet.Tables(8).Rows((i - 1) * 7 + 54 + 65 * (j - 1)).Item(1) = _
                        invoiceRegister(i + 5 + 8 * (j - 1)).Number.ToUpper
                    formDataSet.Tables(8).Rows((i - 1) * 7 + 55 + 65 * (j - 1)).Item(1) = _
                        GetNumberInFFDataFormat(invoiceRegister(i + 5 + 8 * (j - 1)).SumLTL)
                    formDataSet.Tables(8).Rows((i - 1) * 7 + 56 + 65 * (j - 1)).Item(1) = _
                        GetNumberInFFDataFormat(invoiceRegister(i + 5 + 8 * (j - 1)).SumVatLTL)
                    formDataSet.Tables(8).Rows((i - 1) * 7 + 57 + 65 * (j - 1)).Item(1) = _
                        invoiceRegister(i + 5 + 8 * (j - 1)).PersonVatCode.ToUpper
                    formDataSet.Tables(8).Rows((i - 1) * 7 + 58 + 65 * (j - 1)).Item(1) = _
                        GetLimitedLengthString(invoiceRegister(i + 5 + 8 * (j - 1)).PersonName.ToUpper, 36)
                Next
            Next

            Return formDataSet

        End Function

    End Class

End Namespace