Namespace ActiveReports.Declarations

    ''' <summary>
    ''' Represents an implementation of a <see cref="Declaration">Declaration</see>
    ''' for a state social security administration (SODRA) report No. SD13 version 5.
    ''' </summary>
    ''' <remarks>Object is responsible for fetching the report data to a dataset
    ''' and transforming the dataset to ffdata format (required by the FormFiller application).</remarks>
    <Serializable()> _
    Public Class DeclarationSD13_5
        Implements IDeclaration

        Private Const DECLARATION_NAME As String = "SD13 v.5"
        Private Const FILENAMEMXFDSD13_5 As String = "MXFD\13-SD-v05.mxfd"
        Private Const FILENAMEFFDATASD13_5 As String = "FFData\13-SD-v05.ffdata"


        ''' <summary>
        ''' Gets a name of the declaration.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Name() As String Implements IDeclaration.Name
            Get
                Return DECLARATION_NAME
            End Get
        End Property

        ''' <summary>
        ''' Gets a start of the period that the declaration is valid for.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property ValidFrom() As Date Implements IDeclaration.ValidFrom
            Get
                Return Date.MinValue
            End Get
        End Property

        ''' <summary>
        ''' Gets an end of the period that the declaration is valid for.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property ValidTo() As Date Implements IDeclaration.ValidTo
            Get
                Return Date.MaxValue
            End Get
        End Property

        ''' <summary>
        ''' Gets a number of details tables within the declaration.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DetailsTableCount() As Integer _
            Implements IDeclaration.DetailsTableCount
            Get
                Return 1
            End Get
        End Property

        ''' <summary>
        ''' Gets a name of the rdlc file that should be used to print the declaration.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property RdlcFileName() As String _
            Implements IDeclaration.RdlcFileName
            Get
                Return "R_Declaration_SD13.rdlc"
            End Get
        End Property


        ''' <summary>
        ''' Gets a declaration data from a database in a form of a dataset.
        ''' </summary>
        ''' <param name="criteria">criteria of the declaration that holds data required to fetch the declaration data</param>
        ''' <param name="warnings">output parameter containg warnings that were issued during the fetch procedure
        ''' (indicates some discrepancies in data, that are not critical for the data fetched)</param>
        ''' <remarks></remarks>
        Public Function GetBaseDataSet(ByVal criteria As DeclarationCriteria, ByRef warnings As String) As DataSet _
            Implements IDeclaration.GetBaseDataSet

            If Not IsValid(criteria) Then
                Throw New Exception(String.Format(My.Resources.ActiveReports_IDeclaration_ArgumentsNull, _
                    vbCrLf, GetAllErrors(criteria)))
            End If

            warnings = ""

            Dim result As New DataSet
            result.Tables.Add(Declaration.FetchGeneralDataTable)

            Dim sd As New DataTable("Specific")
            sd.Columns.Add("Date")
            sd.Columns.Add("SODRADepartment")
            sd.Columns.Add("InsuredCount")
            sd.Columns.Add("TotalIncome")
            sd.Columns.Add("TotalPayments")
            sd.Columns.Add("TarifForInsurant")
            sd.Columns.Add("TarifForAssured")
            sd.Columns.Add("TarifTotal")
            sd.Columns.Add("DateFrom")
            sd.Columns.Add("DateTo")
            sd.Rows.Add()

            Dim dd As New DataTable("Details")
            dd.Columns.Add("Count")
            dd.Columns.Add("PersonCode")
            dd.Columns.Add("SODRASerial")
            dd.Columns.Add("SODRACode")
            dd.Columns.Add("Date")
            dd.Columns.Add("Income")
            dd.Columns.Add("Payment")
            dd.Columns.Add("PersonName")
            dd.Columns.Add("ReasonCode")
            dd.Columns.Add("ReasonText")

            Try

                Dim myComm As New SQLCommand("FetchDeclarationSD13")
                myComm.AddParam("?DF", criteria.DateFrom.Date)
                myComm.AddParam("?DT", criteria.DateTo.Date)

                Using myData As DataTable = myComm.Fetch

                    Dim smB As Double = 0
                    Dim smI As Double = 0
                    Dim trIs As Double = 0
                    Dim trPri As Double = 0
                    Dim codes As CodeInfoList = CodeInfoList.GetListChild()
                    Dim i As Integer = 0

                    For Each dr As DataRow In myData.Rows
                        dd.Rows.Add()
                        dd.Rows(i).Item(0) = i + 1
                        dd.Rows(i).Item(1) = dr.Item(0).ToString
                        If Not IsDBNull(dr.Item(1)) AndAlso Not String.IsNullOrEmpty(dr.Item(1).ToString.Trim) Then
                            dd.Rows(i).Item(2) = dr.Item(1).ToString.Trim.Substring(0, 2)
                            dd.Rows(i).Item(3) = GetNumericPart(dr.Item(1).ToString.Trim)
                        Else
                            dd.Rows(i).Item(2) = ""
                            dd.Rows(i).Item(3) = ""
                        End If
                        dd.Rows(i).Item(4) = CDate(dr.Item(2)).ToShortDateString
                        dd.Rows(i).Item(5) = DblParser(CDbl(dr.Item(3)))
                        dd.Rows(i).Item(6) = DblParser(CDbl(dr.Item(4)))
                        dd.Rows(i).Item(7) = GetLimitedLengthString(dr.Item(5).ToString.Trim, 68)

                        If Not IsDBNull(dr.Item(6)) AndAlso Not String.IsNullOrEmpty(dr.Item(6).ToString.Trim) Then
                            Dim sci As CodeInfo = codes.GetItemByCode( _
                                ApskaitaObjects.Settings.CodeType.SodraDeclaration, _
                                CIntSafe(dr.Item(6), 0))
                            If sci Is Nothing Then Throw New Exception(String.Format( _
                                My.Resources.ActiveReports_Declarations_DeclarationSD13_1_UnknownReasonCode, CStrSafe(dr.Item(6))))
                            dd.Rows(i).Item(8) = sci.CodeInt
                            dd.Rows(i).Item(9) = sci.Name
                        Else
                            dd.Rows(i).Item(8) = ""
                            dd.Rows(i).Item(9) = ""
                        End If

                        smB = smB + CDbl(dr.Item(3))
                        smI = smI + CDbl(dr.Item(4))

                        If CDblSafe(dr.Item(7), 2, 0) > 0 Then trIs = CDblSafe(dr.Item(7), 2, 0)
                        If CDblSafe(dr.Item(8), 2, 0) > 0 Then trPri = CDblSafe(dr.Item(8), 2, 0)

                        i += 1

                    Next

                    sd.Rows(0).Item("SODRADepartment") = criteria.SodraDepartment
                    sd.Rows(0).Item("InsuredCount") = myData.Rows.Count
                    sd.Rows(0).Item("TotalIncome") = DblParser(smB)
                    sd.Rows(0).Item("TotalPayments") = DblParser(smI)
                    sd.Rows(0).Item("TarifForInsurant") = CRound(trIs)
                    sd.Rows(0).Item("TarifForAssured") = CRound(trPri)
                    sd.Rows(0).Item("TarifTotal") = CRound(trIs + trPri)
                    sd.Rows(0).Item("DateFrom") = criteria.DateFrom.ToShortDateString
                    sd.Rows(0).Item("DateTo") = criteria.DateTo.ToShortDateString
                    sd.Rows(0).Item("Date") = criteria.Date.ToShortDateString

                End Using

                result.Tables.Add(sd)
                result.Tables.Add(dd)

            Catch ex As Exception
                sd.Dispose()
                dd.Dispose()
                result.Dispose()
                Throw
            End Try

            Return result

        End Function

        ''' <summary>
        ''' Gets a ffdata format dataset.
        ''' </summary>
        ''' <param name="declarationDataSet">a declaration dataset fetched by the <see cref="GetBaseDataSet">GetBaseDataSet</see> method.</param>
        ''' <param name="preparatorName">a name of the person who prepared the declaration.</param>
        ''' <remarks></remarks>
        Public Function GetFfDataDataSet(ByVal declarationDataSet As DataSet, _
            ByVal preparatorName As String) As DataSet _
            Implements IDeclaration.GetFfDataDataSet

            If declarationDataSet Is Nothing Then
                Throw New ArgumentNullException("declarationDataSet")
            End If

            If preparatorName Is Nothing Then
                preparatorName = ""
            End If

            Dim dds As DataSet = declarationDataSet
            Dim i As Integer
            Dim currentUser As AccDataAccessLayer.Security.AccIdentity = GetCurrentIdentity()

            Dim declarationFilePath As String = IO.Path.Combine(AppPath(), FILENAMEFFDATASD13_5)
            Dim tempPath As String = IO.Path.Combine(AppPath(), "temp.ffdata")
            Try
                If IO.File.Exists(tempPath) Then
                    IO.File.Delete(tempPath)
                End If
            Catch ex As Exception
            End Try

            Dim pageCount As Integer = 2
            If dds.Tables("Details").Rows.Count > 6 Then

                Dim myDoc As New Xml.XmlDocument
                myDoc.Load(declarationFilePath)

                pageCount = Convert.ToInt32(Math.Ceiling((dds.Tables("Details").Rows.Count - 6) / 5) + 2)
                For i = 1 To pageCount - 2
                    Dim addPg As Xml.XmlElement = DirectCast(myDoc.ChildNodes(1).ChildNodes(0).ChildNodes(0). _
                        ChildNodes(0).ChildNodes(1).Clone, Xml.XmlElement)
                    myDoc.ChildNodes(1).ChildNodes(0).ChildNodes(0).ChildNodes(0).AppendChild(addPg)
                    Dim addTb As Xml.XmlElement = DirectCast(myDoc.ChildNodes(1).ChildNodes(0). _
                        ChildNodes(1).ChildNodes(1).Clone, Xml.XmlElement)
                    addTb.Attributes(1).Value = (i + 2).ToString
                    myDoc.ChildNodes(1).ChildNodes(0).ChildNodes(1).AppendChild(addTb)
                Next
                myDoc.ChildNodes(1).ChildNodes(0).ChildNodes(1).Attributes(0).Value = pageCount.ToString

                myDoc.Save(tempPath)

            Else

                IO.File.Copy(declarationFilePath, tempPath)

            End If

            ' read ffdata xml structure to dataset
            Dim formDataSet As New DataSet
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

            formDataSet.Tables(0).Rows(0).Item(3) = currentUser.Name
            formDataSet.Tables(0).Rows(0).Item(4) = GetDateInFFDataFormat(Today)
            formDataSet.Tables(1).Rows(0).Item(2) = IO.Path.Combine(AppPath(), FILENAMEMXFDSD13_5)

            Dim specificDataRow As DataRow = dds.Tables("Specific").Rows(0)

            For i = 1 To formDataSet.Tables(8).Rows.Count ' bendri duomenys
                If formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "InsurerName" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetLimitedLengthString( _
                        dds.Tables("General").Rows(0).Item(0).ToString, 68).ToUpper
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "InsurerCode" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = dds.Tables("General").Rows(0).Item(3)
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "JuridicalPersonCode" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = dds.Tables("General").Rows(0).Item(1)
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "InsurerPhone" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetLimitedLengthString(dds.Tables("General").Rows(0).Item(8).ToString, 15)
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "InsurerAddress" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetLimitedLengthString( _
                        dds.Tables("General").Rows(0).Item(2).ToString, 68).ToUpper
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "RecipientDepName" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = specificDataRow.Item(1).ToString
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "DocDate" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetDateInFFDataFormat( _
                        CDate(specificDataRow.Item(0)))
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "DocNumber" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = "1"
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "TaxRateInsurer" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat( _
                        CDbl(specificDataRow.Item(6)))
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "TaxRatePerson" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat( _
                        CDbl(specificDataRow.Item(5)))
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "TaxRateTotal" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat( _
                        CDbl(specificDataRow.Item(7)))
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "PersonCountTotal" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = specificDataRow.Item(2).ToString
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "InsIncomeTotal" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat( _
                        CDbl(specificDataRow.Item(3)))
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "PaymentTotal" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat( _
                        CDbl(specificDataRow.Item(4)))
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "ManagerJobPosition" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = "DIREKTORIUS"
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "ManagerFullName" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = dds.Tables("General").Rows(0).Item(9).ToString.Trim.ToUpper
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "PreparatorDetails" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = preparatorName.Trim.ToUpper
                End If
            Next

            If Not dds.Tables("Details").Rows.Count > 0 Then Return formDataSet

            Dim detailsDataTable As DataTable = dds.Tables("Details")
            Dim j As Integer
            Dim pageIncome As Double = 0
            Dim pagePayments As Double = 0

            ' first person appears on the title page (not an appendix)
            formDataSet.Tables(8).Rows(18).Item(1) = 1
            formDataSet.Tables(8).Rows(19).Item(1) = detailsDataTable.Rows(0).Item(1)
            formDataSet.Tables(8).Rows(20).Item(1) = detailsDataTable.Rows(0).Item(2)
            formDataSet.Tables(8).Rows(21).Item(1) = detailsDataTable.Rows(0).Item(3)
            formDataSet.Tables(8).Rows(22).Item(1) = GetDateInFFDataFormat(CDate(detailsDataTable.Rows(0).Item(4)))
            formDataSet.Tables(8).Rows(23).Item(1) = GetNumberInFFDataFormat(CDbl(detailsDataTable.Rows(0).Item(5)))
            formDataSet.Tables(8).Rows(24).Item(1) = GetNumberInFFDataFormat(CDbl(detailsDataTable.Rows(0).Item(6)))
            formDataSet.Tables(8).Rows(25).Item(1) = detailsDataTable.Rows(0).Item(7)
            formDataSet.Tables(8).Rows(26).Item(1) = detailsDataTable.Rows(0).Item(8)
            formDataSet.Tables(8).Rows(27).Item(1) = detailsDataTable.Rows(0).Item(9)

            For i = 1 To pageCount - 1
                pageIncome = 0
                pagePayments = 0
                For j = 1 To Math.Min(5, detailsDataTable.Rows.Count - (i - 1) * 5 - 1)
                    formDataSet.Tables(8).Rows(38 + (j - 1) * 10 + (i - 1) * 59).Item(1) = (i - 1) * 5 + j + 1
                    formDataSet.Tables(8).Rows(39 + (j - 1) * 10 + (i - 1) * 59).Item(1) = _
                        detailsDataTable.Rows((i - 1) * 5 + j).Item(1)
                    formDataSet.Tables(8).Rows(40 + (j - 1) * 10 + (i - 1) * 59).Item(1) = _
                        detailsDataTable.Rows((i - 1) * 5 + j).Item(2)
                    formDataSet.Tables(8).Rows(41 + (j - 1) * 10 + (i - 1) * 59).Item(1) = _
                        detailsDataTable.Rows((i - 1) * 5 + j).Item(3)
                    formDataSet.Tables(8).Rows(42 + (j - 1) * 10 + (i - 1) * 59).Item(1) = _
                        GetDateInFFDataFormat(CDate(detailsDataTable.Rows((i - 1) * 5 + j).Item(4)))
                    formDataSet.Tables(8).Rows(43 + (j - 1) * 10 + (i - 1) * 59).Item(1) = _
                        GetNumberInFFDataFormat(CDbl(detailsDataTable.Rows((i - 1) * 5 + j).Item(5)))
                    formDataSet.Tables(8).Rows(44 + (j - 1) * 10 + (i - 1) * 59).Item(1) = _
                        GetNumberInFFDataFormat(CDbl(detailsDataTable.Rows((i - 1) * 5 + j).Item(6)))
                    formDataSet.Tables(8).Rows(45 + (j - 1) * 10 + (i - 1) * 59).Item(1) = _
                        detailsDataTable.Rows((i - 1) * 5 + j).Item(7)
                    formDataSet.Tables(8).Rows(46 + (j - 1) * 10 + (i - 1) * 59).Item(1) = _
                        detailsDataTable.Rows((i - 1) * 5 + j).Item(8)
                    formDataSet.Tables(8).Rows(47 + (j - 1) * 10 + (i - 1) * 59).Item(1) = _
                        detailsDataTable.Rows((i - 1) * 5 + j).Item(9)
                    pageIncome = pageIncome + CDbl(detailsDataTable.Rows((i - 1) * 5 + j).Item(5))
                    pagePayments = pagePayments + CDbl(detailsDataTable.Rows((i - 1) * 5 + j).Item(6))
                Next
                formDataSet.Tables(8).Rows(88 + (i - 1) * 59).Item(1) = GetNumberInFFDataFormat(pageIncome)
                formDataSet.Tables(8).Rows(89 + (i - 1) * 59).Item(1) = GetNumberInFFDataFormat(pagePayments)
            Next

            Return formDataSet

        End Function


        Public Function GetAllErrors(ByVal criteria As DeclarationCriteria) As String _
           Implements IDeclaration.GetAllErrors

            Dim result As String = ""
            Dim currentError As String = ""
            Dim currentIsWarning As Boolean = False

            If Not criteria.TryValidateSodraDepartment(currentIsWarning, currentError) Then
                If Not currentIsWarning Then
                    result = AddWithNewLine(result, currentError, False)
                End If
            End If

            Return result

        End Function

        Public Function GetAllWarnings(ByVal criteria As DeclarationCriteria) As String _
            Implements IDeclaration.GetAllWarnings

            Dim result As String = ""
            Dim currentError As String = ""
            Dim currentIsWarning As Boolean = False

            If Not criteria.TryValidateSodraDepartment(currentIsWarning, currentError) Then
                If currentIsWarning Then
                    result = AddWithNewLine(result, currentError, False)
                End If
            End If

            Return result

        End Function

        Public Function HasWarnings(ByVal criteria As DeclarationCriteria) As Boolean _
            Implements IDeclaration.HasWarnings

            Dim currentError As String = ""
            Dim currentIsWarning As Boolean = False

            If Not criteria.TryValidateSodraDepartment(currentIsWarning, currentError) Then
                If currentIsWarning Then Return True
            End If

            Return False

        End Function

        Public Function IsValid(ByVal criteria As DeclarationCriteria) As Boolean _
            Implements IDeclaration.IsValid

            Dim currentError As String = ""
            Dim currentIsWarning As Boolean = False

            If Not criteria.TryValidateSodraDepartment(currentIsWarning, currentError) Then
                If Not currentIsWarning Then Return False
            End If

            Return True

        End Function


        Public Overrides Function ToString() As String
            Return DECLARATION_NAME
        End Function

    End Class

End Namespace