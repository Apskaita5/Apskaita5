Namespace ActiveReports.Declarations

    ''' <summary>
    ''' Represents an implementation of a <see cref="Declaration">Declaration</see>
    ''' for a state tax inspectorate (VMI) report No. FR0573 version 4.
    ''' </summary>
    ''' <remarks>Object is responsible for fetching the report data to a dataset
    ''' and transforming the dataset to ffdata format (required by the FormFiller application).</remarks>
    <Serializable()> _
    Public Class DeclarationFR0573_4
        Implements IDeclaration

        Private Const DECLARATION_NAME As String = "FR0573 v.4"
        Private Const FILENAMEMXFDFR0573_4 As String = "MXFD\FR0573(4).mxfd"
        Private Const FILENAMEFFDATAFR0573_4 As String = "FFData\FR0573(4).ffdata"


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
                Return New Date(2018, 12, 31)
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
                Return "R_Declaration_FR0573_3.rdlc"
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
            sd.Columns.Add("MNPDTotal")
            sd.Columns.Add("MPNPDTotal")
            sd.Columns.Add("IncomeLabourTotal")
            sd.Columns.Add("IncomeOthersTotal")
            sd.Columns.Add("IncomeTotal")
            sd.Columns.Add("IncomeTaxLabourTotal")
            sd.Columns.Add("IncomeTaxOthersTotal")
            sd.Columns.Add("IncomeTaxTotal")

            sd.Rows.Add()

            sd.Rows(0).Item(0) = criteria.MunicipalityCode
            sd.Rows(0).Item(1) = criteria.Date.ToShortDateString
            sd.Rows(0).Item(2) = criteria.Year

            Dim dd As New DataTable("Details")
            dd.Columns.Add("Count")
            dd.Columns.Add("PersonCode")
            dd.Columns.Add("PersonName")
            dd.Columns.Add("MunicipalityCode")
            dd.Columns.Add("IncomeCode")
            dd.Columns.Add("MNPD")
            dd.Columns.Add("MPNPD")
            dd.Columns.Add("Income")
            dd.Columns.Add("IncomeTaxRate")
            dd.Columns.Add("IncomeTax")
            dd.Columns.Add("IsLabourRelated")

            Dim gpmCode As String = ""
            Try
                gpmCode = ApskaitaObjects.Settings.CommonSettings.GetCurrentProxy().CodeWageGPM
            Catch ex As Exception
            End Try

            Try

                Dim mnpdTotal As Double = 0
                Dim mpnpdTotal As Double = 0
                Dim incomeLabourTotal As Double = 0
                Dim incomeOthersTotal As Double = 0
                Dim incomeTaxLabourTotal As Integer = 0
                Dim incomeTaxOthersTotal As Integer = 0

                Dim myComm As New SQLCommand("FetchDeclarationFR0573(3)")
                myComm.AddParam("?DF", New Date(criteria.Year, 1, 1))
                myComm.AddParam("?DT", New Date(criteria.Year, 12, 31))
                Using myData As DataTable = myComm.Fetch

                    Declaration.ClearDatatable(myData, 0)

                    Dim i As Integer = 0
                    For Each dr As DataRow In myData.Rows
                        dd.Rows.Add()
                        dd.Rows(i).Item(0) = i + 1
                        dd.Rows(i).Item(1) = dr.Item(0).ToString
                        dd.Rows(i).Item(2) = dr.Item(1).ToString.ToUpper
                        dd.Rows(i).Item(3) = criteria.MunicipalityCode
                        If dr.Item(8).ToString.Trim.ToLower = "t" Then
                            dd.Rows(i).Item(4) = GetMinLengthString(gpmCode, 2, "0"c)
                            dd.Rows(i).Item(10) = "X"
                            incomeLabourTotal = incomeLabourTotal + CDbl(dr.Item(6))
                            incomeTaxLabourTotal = incomeTaxLabourTotal + CInt(CRound(CDbl(dr.Item(7)), 0))
                        Else
                            dd.Rows(i).Item(4) = GetMinLengthString(dr.Item(3).ToString.Trim, 2, "0"c)
                            dd.Rows(i).Item(10) = ""
                            incomeOthersTotal = incomeOthersTotal + CDbl(dr.Item(6))
                            incomeTaxOthersTotal = incomeTaxOthersTotal + CInt(CRound(CDbl(dr.Item(7)), 0))
                        End If
                        dd.Rows(i).Item(5) = DblParser(CDbl(dr.Item(4)))
                        dd.Rows(i).Item(6) = DblParser(CDbl(dr.Item(5)))
                        dd.Rows(i).Item(7) = DblParser(CDbl(dr.Item(6)))
                        dd.Rows(i).Item(8) = CInt(dr.Item(2))
                        dd.Rows(i).Item(9) = CInt(CRound(CDbl(dr.Item(7)), 0))

                        mnpdTotal = mnpdTotal + CDbl(dr.Item(4))
                        mpnpdTotal = mpnpdTotal + CDbl(dr.Item(5))

                        i += 1
                    Next

                End Using

                sd.Rows(0).Item(3) = DblParser(mnpdTotal)
                sd.Rows(0).Item(4) = DblParser(mpnpdTotal)
                sd.Rows(0).Item(5) = DblParser(incomeLabourTotal)
                sd.Rows(0).Item(6) = DblParser(incomeOthersTotal)
                sd.Rows(0).Item(7) = DblParser(incomeLabourTotal + incomeOthersTotal)
                sd.Rows(0).Item(8) = incomeTaxLabourTotal
                sd.Rows(0).Item(9) = incomeTaxOthersTotal
                sd.Rows(0).Item(10) = incomeTaxLabourTotal + incomeTaxOthersTotal

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

            Dim declarationFilePath As String = IO.Path.Combine(AppPath(), FILENAMEFFDATAFR0573_4)
            Dim tempPath As String = IO.Path.Combine(AppPath(), "temp.ffdata")
            Try
                If IO.File.Exists(tempPath) Then
                    IO.File.Delete(tempPath)
                End If
            Catch ex As Exception
            End Try

            If dds.Tables("Details").Rows.Count > 1 Then

                Dim myDoc As New Xml.XmlDocument
                myDoc.Load(declarationFilePath)

                For i = 1 To Convert.ToInt32(Math.Ceiling(dds.Tables("Details").Rows.Count / 5) - 1)
                    Dim addSd As Xml.XmlElement = DirectCast(myDoc.ChildNodes(1).ChildNodes(0). _
                        ChildNodes(1).ChildNodes(1).Clone, Xml.XmlElement)
                    addSd.Attributes(1).Value = (i + 2).ToString
                    myDoc.ChildNodes(1).ChildNodes(0).ChildNodes(1).AppendChild(addSd)
                    Dim addPg As Xml.XmlElement = DirectCast(myDoc.ChildNodes(1).ChildNodes(0).ChildNodes(0). _
                        ChildNodes(0).ChildNodes(0).Clone, Xml.XmlElement)
                    myDoc.ChildNodes(1).ChildNodes(0).ChildNodes(0).ChildNodes(0).AppendChild(addPg)
                Next
                myDoc.ChildNodes(1).ChildNodes(0).ChildNodes(1).Attributes(0).Value = _
                    (2 + Math.Ceiling(dds.Tables("Details").Rows.Count / 5) - 1).ToString

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
            formDataSet.Tables(1).Rows(0).Item(2) = IO.Path.Combine(AppPath(), FILENAMEMXFDFR0573_4)

            Dim specificDataRow As DataRow = dds.Tables("Specific").Rows(0)
            For i = 1 To formDataSet.Tables(8).Rows.Count ' bendri duomenys
                If formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "B_MM_ID" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = dds.Tables("General").Rows(0).Item(1)
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "B_MM_Pavad" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetLimitedLengthString( _
                        dds.Tables("General").Rows(0).Item(0).ToString, 43).ToUpper
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "B_MM_Adresas" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetLimitedLengthString( _
                        dds.Tables("General").Rows(0).Item(2).ToString, 43).ToUpper
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "B_MM_Tel" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetLimitedLengthString( _
                        dds.Tables("General").Rows(0).Item(8).ToString, 15)
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "B_MM_Epastas" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetLimitedLengthString( _
                        dds.Tables("General").Rows(0).Item(7).ToString, 35).ToUpper
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "B_UzpildData" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetDateInFFDataFormat( _
                        CDate(specificDataRow.Item(1)))
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "B_ML_Metai" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = specificDataRow.Item(2).ToString
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "E12" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = Math.Ceiling(dds.Tables("Details").Rows.Count / 5)
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "E13" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = dds.Tables("Details").Rows.Count
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "B_MM_SavKodas" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = specificDataRow.Item(0).ToString
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "E16" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat(CDbl(specificDataRow.Item(7)))
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "E17" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = specificDataRow.Item(10).ToString
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "E18" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = specificDataRow.Item(10).ToString
                End If
            Next

            Dim detailsDataTable As DataTable = dds.Tables("Details")
            Dim j, p As Integer
            Dim pageIncome As Double = 0
            Dim pagePayments As Double = 0

            ' both FR0573(2), FR0573(3) and FR0573(4) forms contain the same data
            ' but different xml structure; the difference is defined by PgSt variable
            ' which makes differents 'steps' when looping through xml
            Dim PgSt As Integer = 84

            For i = 1 To Convert.ToInt32(Math.Ceiling(detailsDataTable.Rows.Count / 5))
                p = 5 * (i - 1)
                pageIncome = 0
                pagePayments = 0
                For j = 1 To Math.Min(5, detailsDataTable.Rows.Count - p)
                    formDataSet.Tables(8).Rows(20 + PgSt * (i - 1) + 15 * (j - 1)).Item(1) = j
                    formDataSet.Tables(8).Rows(21 + PgSt * (i - 1) + 15 * (j - 1)).Item(1) = _
                        detailsDataTable.Rows(p + j - 1).Item(1)
                    formDataSet.Tables(8).Rows(22 + PgSt * (i - 1) + 15 * (j - 1)).Item(1) = "1"
                    formDataSet.Tables(8).Rows(23 + PgSt * (i - 1) + 15 * (j - 1)).Item(1) = _
                        detailsDataTable.Rows(p + j - 1).Item(2).ToString.ToUpper
                    formDataSet.Tables(8).Rows(24 + PgSt * (i - 1) + 15 * (j - 1)).Item(1) = _
                        detailsDataTable.Rows(p + j - 1).Item(3).ToString
                    formDataSet.Tables(8).Rows(25 + PgSt * (i - 1) + 15 * (j - 1)).Item(1) = _
                            detailsDataTable.Rows(p + j - 1).Item(4).ToString
                    formDataSet.Tables(8).Rows(26 + PgSt * (i - 1) + 15 * (j - 1)).Item(1) = _
                        GetNumberInFFDataFormat(CDbl(detailsDataTable.Rows(p + j - 1).Item(5)))
                    formDataSet.Tables(8).Rows(27 + PgSt * (i - 1) + 15 * (j - 1)).Item(1) = _
                        GetNumberInFFDataFormat(CDbl(detailsDataTable.Rows(p + j - 1).Item(6)))
                    formDataSet.Tables(8).Rows(31 + PgSt * (i - 1) + 15 * (j - 1)).Item(1) = _
                        GetNumberInFFDataFormat(CDbl(detailsDataTable.Rows(p + j - 1).Item(7)))
                    formDataSet.Tables(8).Rows(32 + PgSt * (i - 1) + 15 * (j - 1)).Item(1) = _
                        detailsDataTable.Rows(p + j - 1).Item(8).ToString
                    formDataSet.Tables(8).Rows(33 + PgSt * (i - 1) + 15 * (j - 1)).Item(1) = _
                        detailsDataTable.Rows(p + j - 1).Item(9).ToString
                    formDataSet.Tables(8).Rows(34 + PgSt * (i - 1) + 15 * (j - 1)).Item(1) = _
                        detailsDataTable.Rows(p + j - 1).Item(9).ToString
                    pageIncome = pageIncome + CDbl(detailsDataTable.Rows(p + j - 1).Item(7))
                    pagePayments = pagePayments + CInt(detailsDataTable.Rows(p + j - 1).Item(9))
                Next

                formDataSet.Tables(8).Rows(95 + PgSt * (i - 1)).Item(1) = GetNumberInFFDataFormat(pageIncome)
                formDataSet.Tables(8).Rows(96 + PgSt * (i - 1)).Item(1) = GetNumberInFFDataFormat(pagePayments)
                formDataSet.Tables(8).Rows(97 + PgSt * (i - 1)).Item(1) = GetNumberInFFDataFormat(pagePayments)

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
