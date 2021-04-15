Namespace ActiveReports.Declarations

    ''' <summary>
    ''' Represents an implementation of an <see cref="IInvoiceRegisterDeclaration">IInvoiceRegisterDeclaration</see>
    ''' for a state tax inspectorate (VMI) report No. FR0671 version 1.
    ''' </summary>
    ''' <remarks>Object is responsible for exporting the <see cref="InvoiceInfoItemList">
    ''' invoice register report</see> data to ffdata format (required by the 
    ''' FormFiller application).</remarks>
    <Serializable()> _
    Public Class InvoiceRegisterFR0671_2
        Implements IInvoiceRegisterDeclaration

        Private Const DECLARATION_NAME As String = "FR0671 v.2"
        Private Const FILENAMEMXFDFR0671_2 As String = "MXFD\FR0671(2).mxfd"
        Private Const FILENAMEFFDATAFR0671_2 As String = "FFData\FR0671(2).ffdata"

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
                Return New Date(2014, 1, 1)
            End Get
        End Property

        ''' <summary>
        ''' Gets an end of the period that the invoice register declaration is valid for.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property ValidTo() As Date Implements IInvoiceRegisterDeclaration.ValidTo
            Get
                Return Date.MaxValue
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

            Dim declarationFilePath As String = IO.Path.Combine(AppPath(), FILENAMEFFDATAFR0671_2)

            Dim pageCount As Integer = Convert.ToInt32(Math.Ceiling((invoiceRegister.Count) / 4))
            Dim sumLtl As Double = 0
            Dim sumVatLtl As Double = 0
            For Each n As InvoiceInfoItem In invoiceRegister
                sumLtl += n.SumLTL
                sumVatLtl += n.SumVatLTL
            Next

            Dim tempPath As String = IO.Path.Combine(AppPath(), "temp.ffdata")
            Try
                If IO.File.Exists(tempPath) Then
                    IO.File.Delete(tempPath)
                End If
            Catch ex As Exception
            End Try

            If pageCount > 1 Then
                Dim myDoc As New Xml.XmlDocument
                myDoc.Load(declarationFilePath)
                For i = 1 To pageCount - 1
                    Dim addP As Xml.XmlElement = DirectCast(myDoc.ChildNodes(1).ChildNodes(0).ChildNodes(0). _
                        ChildNodes(0).ChildNodes(1).Clone, Xml.XmlElement)
                    myDoc.ChildNodes(1).ChildNodes(0).ChildNodes(0).ChildNodes(0).AppendChild(addP)
                    Dim addPg As Xml.XmlElement = DirectCast(myDoc.ChildNodes(1).ChildNodes(0). _
                        ChildNodes(1).ChildNodes(1).Clone, Xml.XmlElement)
                    myDoc.ChildNodes(1).ChildNodes(0).ChildNodes(1).AppendChild(addPg)
                    myDoc.ChildNodes(1).ChildNodes(0).ChildNodes(1).ChildNodes(i + 1).Attributes(1).Value = (2 + i).ToString
                Next
                myDoc.ChildNodes(1).ChildNodes(0).ChildNodes(1).Attributes(0).Value = (pageCount + 1).ToString
                myDoc.Save(tempPath)
            Else
                IO.File.Copy(declarationFilePath, tempPath)
            End If

            Dim formDataSet As New DataSet

            ' read ffdata xml structure to dataset
            Try
                Using formFileStream As IO.FileStream = New IO.FileStream(tempPath, IO.FileMode.Open)
                    formDataSet.ReadXml(formFileStream)
                    formFileStream.Close()
                End Using
            Catch ex As Exception
                Throw New Exception("Failed to prepare ffdata file.", ex)
            End Try

            Try
                IO.File.Delete(tempPath)
            Catch ex As Exception
            End Try

            ' GENERAL DATA

            formDataSet.Tables(0).Rows(0).Item(3) = currentUser.Name
            formDataSet.Tables(0).Rows(0).Item(4) = GetDateInFFDataFormat(Today.Date)
            formDataSet.Tables(1).Rows(0).Item(2) = IO.Path.Combine(AppPath(), FILENAMEMXFDFR0671_2)

            Dim k As Integer = 1
            For i = 1 To formDataSet.Tables(8).Rows.Count ' bendri duomenys
                If formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "B_MM_Pavad" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = currentCompany.Name.ToUpper
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "B_MM_PVM" _
                    AndAlso currentCompany.CodeVat.Length > 2 Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = currentCompany.CodeVat.Substring(2)
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "B_UzpildData" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetDateInFFDataFormat(Today.Date)
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "B_ML_Nuo" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetDateInFFDataFormat(invoiceRegister.DateFrom.Date)
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "B_ML_Iki" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetDateInFFDataFormat(invoiceRegister.DateTo.Date)
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "E6" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = 1
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "E14" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = pageCount
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "E19" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat(sumLtl)
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "E20" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat(sumVatLtl)
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "E21" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat(sumLtl)
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "E22" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat(sumVatLtl)
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "E23" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = 0
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "LapoNr" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = k
                    k = k + 1
                End If
            Next

            ' DETAILS DATA

            For j As Integer = 1 To Convert.ToInt32(Math.Ceiling((invoiceRegister.Count) / 4))
                sumLtl = 0
                sumVatLtl = 0
                For i = 1 To Math.Min(4, invoiceRegister.Count - (4 * (j - 1)))
                    formDataSet.Tables(8).Rows((i - 1) * 8 + 20 + 43 * (j - 1)).Item(1) = i + 4 * (j - 1)
                    formDataSet.Tables(8).Rows((i - 1) * 8 + 21 + 43 * (j - 1)).Item(1) = _
                        GetDateInFFDataFormat(invoiceRegister(4 * (j - 1) + i - 1).Date)
                    formDataSet.Tables(8).Rows((i - 1) * 8 + 22 + 43 * (j - 1)).Item(1) = _
                        invoiceRegister(4 * (j - 1) + i - 1).Number
                    formDataSet.Tables(8).Rows((i - 1) * 8 + 23 + 43 * (j - 1)).Item(1) = _
                        GetNumberInFFDataFormat(invoiceRegister(4 * (j - 1) + i - 1).SumLTL)
                    formDataSet.Tables(8).Rows((i - 1) * 8 + 24 + 43 * (j - 1)).Item(1) = _
                        GetNumberInFFDataFormat(invoiceRegister(4 * (j - 1) + i - 1).SumVatLTL)
                    formDataSet.Tables(8).Rows((i - 1) * 8 + 26 + 43 * (j - 1)).Item(1) = _
                        invoiceRegister(4 * (j - 1) + i - 1).PersonVatCode
                    formDataSet.Tables(8).Rows((i - 1) * 8 + 27 + 43 * (j - 1)).Item(1) = _
                        GetLimitedLengthString(invoiceRegister(4 * (j - 1) + i - 1).PersonName, 32)
                    sumLtl = sumLtl + invoiceRegister(4 * (j - 1) + i - 1).SumLTL
                    sumVatLtl = sumVatLtl + invoiceRegister(4 * (j - 1) + i - 1).SumVatLTL
                Next
                formDataSet.Tables(8).Rows(52 + 43 * (j - 1)).Item(1) = GetNumberInFFDataFormat(sumLtl)
                formDataSet.Tables(8).Rows(53 + 43 * (j - 1)).Item(1) = GetNumberInFFDataFormat(sumVatLtl)
            Next

            Return formDataSet

        End Function

    End Class

End Namespace
