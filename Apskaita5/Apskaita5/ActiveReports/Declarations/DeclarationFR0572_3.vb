﻿Namespace ActiveReports.Declarations

    ''' <summary>
    ''' Represents an implementation of a <see cref="Declaration">Declaration</see>
    ''' for a state tax inspectorate (VMI) report No. FR0572 version 3.
    ''' </summary>
    ''' <remarks>Object is responsible for fetching the report data to a dataset
    ''' and transforming the dataset to ffdata format (required by the FormFiller application).</remarks>
    <Serializable()> _
    Public Class DeclarationFR0572_3
        Implements IDeclaration

        Private Const DECLARATION_NAME As String = "FR0572 v.3"


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
                Return New Date(2009, 12, 31)
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
                Return "R_Declaration_FR0572_3.rdlc"
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
            sd.Columns.Add("MunicipalityCode")
            sd.Columns.Add("Date")
            sd.Columns.Add("Year")
            sd.Columns.Add("Month")
            sd.Columns.Add("WorkerCount")
            sd.Columns.Add("AppendixPageCount")
            sd.Columns.Add("AppendixLineCount")
            sd.Columns.Add("LabourIncome")
            sd.Columns.Add("LabourIncomeTaxBefore15")
            sd.Columns.Add("LabourIncomeTaxAfter15")
            sd.Columns.Add("OtherIncome")
            sd.Columns.Add("OtherIncomeTaxBefore15")
            sd.Columns.Add("OtherIncomeTaxAfter15")
            sd.Columns.Add("SalesIncome")
            sd.Columns.Add("SalesIncomeTaxBefore15")
            sd.Columns.Add("SalesIncomeTaxAfter15")
            sd.Columns.Add("PSDBefore15")
            sd.Columns.Add("PSDAfter15")
            sd.Columns.Add("TotalIncome")
            sd.Columns.Add("TotalIncomeTax")
            sd.Columns.Add("TotalPSD")

            sd.Rows.Add()

            sd.Rows(0).Item(0) = criteria.MunicipalityCode
            sd.Rows(0).Item(1) = criteria.Date.ToShortDateString
            sd.Rows(0).Item(2) = criteria.Year
            sd.Rows(0).Item(3) = criteria.Month

            Dim dd As New DataTable("Details")
            dd.Columns.Add("Count")
            dd.Columns.Add("PersonName")
            dd.Columns.Add("PersonCode")
            dd.Columns.Add("MunicipalityCode")
            dd.Columns.Add("Income")
            dd.Columns.Add("IncomeTax")
            dd.Columns.Add("TaxDueDate")
            dd.Columns.Add("IncomeCode")
            dd.Columns.Add("PSD")

            Try

                sd.Rows(0).Item(4) = Declaration.GetEmployeesCount(New Date( _
                    criteria.Year, criteria.Month, Date.DaysInMonth(criteria.Year, criteria.Month)))

                Dim myComm As New SQLCommand("FetchDeclarationFR0572(3)_1")
                myComm.AddParam("?MN", criteria.Month)
                myComm.AddParam("?YR", criteria.Year)
                Using myData As DataTable = myComm.Fetch

                    If myData.Rows.Count < 1 Then myData.Rows.Add()
                    Declaration.ClearDatatable(myData, 0)

                    sd.Rows(0).Item(7) = DblParser(CDbl(myData.Rows(0).Item(0)) + _
                        CDbl(myData.Rows(0).Item(1)) + CDbl(myData.Rows(0).Item(4)))
                    sd.Rows(0).Item(8) = DblParser(CDbl(myData.Rows(0).Item(2)))
                    sd.Rows(0).Item(9) = DblParser(CDbl(myData.Rows(0).Item(3)))

                End Using

                Dim otherIncomeTotal, psdBefore15, psdAfter15 As Double

                myComm = New SQLCommand("FetchDeclarationFR0572(3)_2")
                myComm.AddParam("?MN", criteria.Month)
                myComm.AddParam("?YR", criteria.Year)
                Using myData As DataTable = myComm.Fetch

                    If myData.Rows.Count < 1 Then myData.Rows.Add()
                    Declaration.ClearDatatable(myData, 0)

                    otherIncomeTotal = CRound(CDbl(myData.Rows(0).Item(0)))
                    sd.Rows(0).Item(11) = DblParser(CDbl(myData.Rows(0).Item(1)))
                    sd.Rows(0).Item(12) = DblParser(CDbl(myData.Rows(0).Item(2)))
                    psdBefore15 = CRound(CDbl(myData.Rows(0).Item(3)))
                    psdAfter15 = CRound(CDbl(myData.Rows(0).Item(4)))

                End Using

                Dim salesIncomeTotal As Double = 0
                Dim salesIncomeTaxBefore15 As Double = 0
                Dim salesIncomeTaxAfter15 As Double = 0
                Dim appendixIncomeTotal As Double = 0
                Dim appendixIncomeTaxTotal As Double = 0
                Dim appendixPSDTotal As Double = 0
                Dim i As Integer = 0

                myComm = New SQLCommand("FetchDeclarationFR0572(3)_3")
                myComm.AddParam("?MN", criteria.Month)
                myComm.AddParam("?YR", criteria.Year)
                Using myData As DataTable = myComm.Fetch

                    Declaration.ClearDatatable(myData, 0)

                    For Each dr As DataRow In myData.Rows
                        dd.Rows.Add()
                        dd.Rows(i).Item(0) = i + 1
                        dd.Rows(i).Item(1) = dr.Item(0).ToString
                        dd.Rows(i).Item(2) = dr.Item(1).ToString
                        dd.Rows(i).Item(3) = criteria.MunicipalityCode
                        dd.Rows(i).Item(4) = DblParser(CDbl(dr.Item(2)))
                        dd.Rows(i).Item(5) = DblParser(CDbl(dr.Item(3)))
                        dd.Rows(i).Item(6) = CInt(dr.Item(4))
                        dd.Rows(i).Item(7) = GetMinLengthString(dr.Item(5).ToString, 2, "0"c)
                        dd.Rows(i).Item(8) = DblParser(CDbl(dr.Item(6)))

                        If CInt(dr.Item(5)) > 13 AndAlso CInt(dr.Item(5)) < 19 Then
                            ' it's a sales income tax codes
                            salesIncomeTotal = salesIncomeTotal + CDbl(dr.Item(2))
                            If CInt(dr.Item(4)) = 1 Then
                                salesIncomeTaxBefore15 = salesIncomeTaxBefore15 + CDbl(dr.Item(3))
                            Else
                                salesIncomeTaxAfter15 = salesIncomeTaxAfter15 + CDbl(dr.Item(3))
                            End If

                        ElseIf CInt(dr.Item(5)) = 3 Then
                            ' it's a sick leave tax code
                            otherIncomeTotal = otherIncomeTotal + CDbl(dr.Item(2))
                            If CInt(dr.Item(4)) = 1 Then
                                psdBefore15 = psdBefore15 + CDbl(dr.Item(6))
                            Else
                                psdAfter15 = psdAfter15 + CDbl(dr.Item(6))
                            End If

                        End If

                        appendixIncomeTotal = appendixIncomeTotal + CDbl(dr.Item(2))
                        appendixIncomeTaxTotal = appendixIncomeTaxTotal + CDbl(dr.Item(3))
                        appendixPSDTotal = appendixPSDTotal + CDbl(dr.Item(6))

                        i += 1
                    Next

                End Using

                sd.Rows(0).Item(5) = Math.Ceiling(i / 4)
                sd.Rows(0).Item(6) = Math.Ceiling(i)
                sd.Rows(0).Item(10) = DblParser(otherIncomeTotal)
                sd.Rows(0).Item(13) = DblParser(salesIncomeTotal)
                sd.Rows(0).Item(14) = DblParser(salesIncomeTaxBefore15)
                sd.Rows(0).Item(15) = DblParser(salesIncomeTaxAfter15)
                sd.Rows(0).Item(16) = DblParser(psdBefore15)
                sd.Rows(0).Item(17) = DblParser(psdAfter15)
                sd.Rows(0).Item(18) = DblParser(appendixIncomeTotal)
                sd.Rows(0).Item(19) = DblParser(appendixIncomeTaxTotal)
                sd.Rows(0).Item(20) = DblParser(appendixPSDTotal)

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

            If dds.Tables("Details").Rows.Count < 1 Then
                Dim myDoc As New Xml.XmlDocument
                myDoc.Load(AppPath() & FILENAMEFFDATAFR0572_3)
                myDoc.ChildNodes(1).ChildNodes(0).ChildNodes(0).ChildNodes(0).RemoveChild( _
                    myDoc.ChildNodes(1).ChildNodes(0).ChildNodes(0).ChildNodes(0).ChildNodes(1))
                myDoc.ChildNodes(1).ChildNodes(0).ChildNodes(1).RemoveChild( _
                    myDoc.ChildNodes(1).ChildNodes(0).ChildNodes(1).ChildNodes(1))
                myDoc.ChildNodes(1).ChildNodes(0).ChildNodes(1).Attributes(0).Value = "1"
                myDoc.Save(AppPath() & FILENAMEFFDATATEMP)

            ElseIf dds.Tables("Details").Rows.Count > 4 Then
                Dim myDoc As New Xml.XmlDocument
                myDoc.Load(AppPath() & FILENAMEFFDATAFR0572_3)

                For i = 1 To Convert.ToInt32(Math.Ceiling(dds.Tables("Details").Rows.Count / 4) - 1)
                    Dim addPg1 As Xml.XmlElement = DirectCast(myDoc.ChildNodes(1).ChildNodes(0). _
                        ChildNodes(0).ChildNodes(0).ChildNodes(1).Clone, Xml.XmlElement)
                    myDoc.ChildNodes(1).ChildNodes(0).ChildNodes(0).ChildNodes(0).AppendChild(addPg1)
                    Dim addPg2 As Xml.XmlElement = DirectCast(myDoc.ChildNodes(1).ChildNodes(0). _
                        ChildNodes(1).ChildNodes(1).Clone, Xml.XmlElement)
                    addPg2.Attributes(1).Value = (i + 2).ToString
                    myDoc.ChildNodes(1).ChildNodes(0).ChildNodes(1).AppendChild(addPg2)
                Next
                myDoc.ChildNodes(1).ChildNodes(0).ChildNodes(1).Attributes(0).Value = _
                    Convert.ToInt32(Math.Ceiling(dds.Tables("Details").Rows.Count / 4) + 1).ToString
                myDoc.Save(AppPath() & FILENAMEFFDATATEMP)

            Else
                IO.File.Copy(AppPath() & FILENAMEFFDATAFR0572_3, AppPath() & FILENAMEFFDATATEMP)
            End If

            ' read ffdata xml structure to dataset
            Dim formDataSet As New DataSet
            Using formFileStream As IO.FileStream = New IO.FileStream( _
                AppPath() & FILENAMEFFDATATEMP, IO.FileMode.Open)
                formDataSet.ReadXml(formFileStream)
                formFileStream.Close()
            End Using

            formDataSet.Tables(0).Rows(0).Item(3) = currentUser.Name
            formDataSet.Tables(0).Rows(0).Item(4) = GetDateInFFDataFormat(Today)
            formDataSet.Tables(1).Rows(0).Item(2) = AppPath() & FILENAMEMXFDFR0572_3

            Dim specificDataRow As DataRow = dds.Tables("Specific").Rows(0)

            For i = 1 To formDataSet.Tables(8).Rows.Count
                If formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "B_MM_ID" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = dds.Tables("General").Rows(0).Item(1)
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "B_MM_Pavad" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetLimitedLengthString(dds.Tables("General").Rows(0).Item(0).ToString, 45).ToUpper
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "B_MM_Adresas" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetLimitedLengthString(dds.Tables("General").Rows(0).Item(2).ToString, 45).ToUpper
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "B_MM_Tel" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetLimitedLengthString(dds.Tables("General").Rows(0).Item(8).ToString, 12)
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "B_MM_Epastas" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetLimitedLengthString(dds.Tables("General").Rows(0).Item(7).ToString, 35).ToUpper
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "B_UzpildData" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetDateInFFDataFormat( _
                        CDate(specificDataRow.Item(1)))
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "B_ML_Metai" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = specificDataRow.Item(2).ToString
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "B_ML_Menuo" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = specificDataRow.Item(3)
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "B_MM_SavKodas" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = specificDataRow.Item(0).ToString
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "E11" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = specificDataRow.Item(4).ToString
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "E12" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = specificDataRow.Item(5).ToString
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "E13" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = specificDataRow.Item(6).ToString
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "E18" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat( _
                        CDbl(specificDataRow.Item(7)))
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "E19" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat( _
                        CDbl(specificDataRow.Item(8)))
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "E20" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat( _
                        CDbl(specificDataRow.Item(9)))
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "E21" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat( _
                        CDbl(specificDataRow.Item(10)))
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "E22" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat( _
                        CDbl(specificDataRow.Item(11)))
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "E23" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat( _
                        CDbl(specificDataRow.Item(12)))
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "E24" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat( _
                        CDbl(specificDataRow.Item(13)))
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "E25" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat( _
                        CDbl(specificDataRow.Item(14)))
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "E26" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat( _
                        CDbl(specificDataRow.Item(15)))
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "E27" _
                    AndAlso CInt(specificDataRow.Item(2)) = 2009 Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat( _
                        CDbl(specificDataRow.Item(16)))
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "E28" _
                    AndAlso CInt(specificDataRow.Item(2)) = 2009 Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat( _
                        CDbl(specificDataRow.Item(17)))
                End If
            Next

            Dim detailsDataTable As DataTable = dds.Tables("Details")
            Dim pageIncome, pageGPM, pagePSD As Double
            Dim p As Integer
            For i = 1 To Convert.ToInt32(Math.Ceiling(detailsDataTable.Rows.Count / 4))
                pageIncome = 0
                pageGPM = 0
                pagePSD = 0
                p = (i - 1) * 4
                For j As Integer = 1 To Math.Min(4, detailsDataTable.Rows.Count - p)
                    formDataSet.Tables(8).Rows(28 + 71 * (i - 1) + 15 * (j - 1)).Item(1) = p + j
                    formDataSet.Tables(8).Rows(29 + 71 * (i - 1) + 15 * (j - 1)).Item(1) = _
                        detailsDataTable.Rows(p + j - 1).Item(1).ToString.ToUpper
                    formDataSet.Tables(8).Rows(30 + 71 * (i - 1) + 15 * (j - 1)).Item(1) = _
                        detailsDataTable.Rows(p + j - 1).Item(2)
                    formDataSet.Tables(8).Rows(31 + 71 * (i - 1) + 15 * (j - 1)).Item(1) = _
                        detailsDataTable.Rows(p + j - 1).Item(3)
                    formDataSet.Tables(8).Rows(32 + 71 * (i - 1) + 15 * (j - 1)).Item(1) = _
                        GetNumberInFFDataFormat(CDbl(detailsDataTable.Rows(p + j - 1).Item(4)))
                    formDataSet.Tables(8).Rows(35 + 71 * (i - 1) + 15 * (j - 1)).Item(1) = _
                        GetNumberInFFDataFormat(CDbl(detailsDataTable.Rows(p + j - 1).Item(5)))
                    formDataSet.Tables(8).Rows(37 + 71 * (i - 1) + 15 * (j - 1)).Item(1) = _
                        detailsDataTable.Rows(p + j - 1).Item(6)
                    formDataSet.Tables(8).Rows(38 + 71 * (i - 1) + 15 * (j - 1)).Item(1) = _
                        detailsDataTable.Rows(p + j - 1).Item(7)
                    If detailsDataTable.Columns.Count > 8 AndAlso CInt(specificDataRow.Item(2)) = 2009 Then
                        formDataSet.Tables(8).Rows(40 + 71 * (i - 1) + 15 * (j - 1)).Item(1) = _
                            GetNumberInFFDataFormat(CDbl(detailsDataTable.Rows(p + j - 1).Item(8)))
                        pagePSD = pagePSD + CDbl(detailsDataTable.Rows(p + j - 1).Item(8))
                    End If
                    pageIncome = pageIncome + CDbl(detailsDataTable.Rows(p + j - 1).Item(4))
                    pageGPM = pageGPM + CDbl(detailsDataTable.Rows(p + j - 1).Item(5))
                Next
                formDataSet.Tables(8).Rows(88 + 71 * (i - 1)).Item(1) = GetNumberInFFDataFormat(pageIncome)
                formDataSet.Tables(8).Rows(89 + 71 * (i - 1)).Item(1) = GetNumberInFFDataFormat(pageGPM)
                formDataSet.Tables(8).Rows(90 + 71 * (i - 1)).Item(1) = GetNumberInFFDataFormat(pagePSD)
            Next

            Return formDataSet

        End Function



        Public Function GetAllErrors(ByVal criteria As DeclarationCriteria) As String _
            Implements IDeclaration.GetAllErrors

            Dim result As String = ""
            Dim currentError As String = ""
            Dim currentIsWarning As Boolean = False

            If Not criteria.TryValidateMunicipalityCode(currentIsWarning, currentError) Then
                If Not currentIsWarning Then
                    result = AddWithNewLine(result, currentError, False)
                End If
            End If

            currentError = ""
            currentIsWarning = False

            If Not criteria.TryValidateMonth(currentIsWarning, currentError) Then
                If Not currentIsWarning Then
                    result = AddWithNewLine(result, currentError, False)
                End If
            End If

            currentError = ""
            currentIsWarning = False

            If Not criteria.TryValidateYear(currentIsWarning, currentError) Then
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

            If Not criteria.TryValidateMunicipalityCode(currentIsWarning, currentError) Then
                If currentIsWarning Then
                    result = AddWithNewLine(result, currentError, False)
                End If
            End If

            currentError = ""
            currentIsWarning = False

            If Not criteria.TryValidateMonth(currentIsWarning, currentError) Then
                If currentIsWarning Then
                    result = AddWithNewLine(result, currentError, False)
                End If
            End If

            currentError = ""
            currentIsWarning = False

            If Not criteria.TryValidateYear(currentIsWarning, currentError) Then
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

            If Not criteria.TryValidateMunicipalityCode(currentIsWarning, currentError) Then
                If currentIsWarning Then Return True
            End If

            currentError = ""
            currentIsWarning = False

            If Not criteria.TryValidateMonth(currentIsWarning, currentError) Then
                If currentIsWarning Then Return True
            End If

            currentError = ""
            currentIsWarning = False

            If Not criteria.TryValidateYear(currentIsWarning, currentError) Then
                If currentIsWarning Then Return True
            End If

            Return False

        End Function

        Public Function IsValid(ByVal criteria As DeclarationCriteria) As Boolean _
            Implements IDeclaration.IsValid

            Dim currentError As String = ""
            Dim currentIsWarning As Boolean = False

            If Not criteria.TryValidateMunicipalityCode(currentIsWarning, currentError) Then
                If Not currentIsWarning Then Return False
            End If

            currentError = ""
            currentIsWarning = False

            If Not criteria.TryValidateMonth(currentIsWarning, currentError) Then
                If Not currentIsWarning Then Return False
            End If

            currentError = ""
            currentIsWarning = False

            If Not criteria.TryValidateYear(currentIsWarning, currentError) Then
                If Not currentIsWarning Then Return False
            End If

            Return True

        End Function


        Public Overrides Function ToString() As String
            Return DECLARATION_NAME
        End Function

    End Class

End Namespace