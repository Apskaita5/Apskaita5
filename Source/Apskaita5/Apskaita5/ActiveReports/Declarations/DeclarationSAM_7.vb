Namespace ActiveReports.Declarations

    ''' <summary>
    ''' Represents an implementation of a <see cref="Declaration">Declaration</see>
    ''' for a state social security administration (SODRA) report No. SAM version 7.
    ''' </summary>
    ''' <remarks>Object is responsible for fetching the report data to a dataset
    ''' and transforming the dataset to ffdata format (required by the FormFiller application).</remarks>
    <Serializable()>
    Public Class DeclarationSAM_7
        Implements IDeclaration

        Private Const DECLARATION_NAME As String = "SAM v.7"
        Private Const FILENAMEMXFDSAM07 As String = "MXFD\SAM-v07.mxfd"
        Private Const FILENAMEFFDATASAM07 As String = "FFData\SAM-v07.ffdata"


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
                Return New Date(2019, 1, 1)
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
                Return "R_Declaration_SAM_7.rdlc"
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
                Throw New Exception(String.Format(My.Resources.ActiveReports_IDeclaration_ArgumentsNull,
                    vbCrLf, GetAllErrors(criteria)))
            End If

            warnings = ""

            Dim result As New DataSet
            result.Tables.Add(Declaration.FetchGeneralDataTable)

            Dim sd As New DataTable("Specific")
            sd.Columns.Add("Date")
            sd.Columns.Add("Year")
            sd.Columns.Add("Month")
            sd.Columns.Add("3SDPageCount")
            sd.Columns.Add("TotalWorkerCount")
            sd.Columns.Add("TotalIncome")
            sd.Columns.Add("TotalPayments")

            sd.Rows.Add()

            sd.Rows(0).Item(0) = criteria.Date.ToString("yyyy-MM-dd")
            sd.Rows(0).Item(1) = criteria.Year.ToString
            sd.Rows(0).Item(2) = criteria.Month.ToString

            Dim dd As New DataTable("Details")
            dd.Columns.Add("RowNumber")
            dd.Columns.Add("PersonCode")
            dd.Columns.Add("InsuranceSeries")
            dd.Columns.Add("InsuranceNumber")
            dd.Columns.Add("InsIncomeSum")
            dd.Columns.Add("TaxRate")
            dd.Columns.Add("PaymentSum")
            dd.Columns.Add("PersonFirstName")
            dd.Columns.Add("PersonLastName")

            Try

                Dim myComm As New SQLCommand("FetchDeclarationSAM_7")
                myComm.AddParam("?YR", criteria.Year)
                myComm.AddParam("?MN", criteria.Month)
                myComm.AddParam("?DT", New Date(criteria.Year, criteria.Month,
                    Date.DaysInMonth(criteria.Year, criteria.Month)))

                Dim totalIncome As Double = 0
                Dim totalPayments As Double = 0
                Dim i As Integer = 0

                Using myData As DataTable = myComm.Fetch
                    For Each dr As DataRow In myData.Rows

                        dd.Rows.Add()

                        dd.Rows(i).Item(0) = (i + 1).ToString
                        dd.Rows(i).Item(1) = GetLimitedLengthString(CStrSafe(dr.Item(0)).Trim, 11)
                        Dim SerialAndNumber As String = CStrSafe(dr.Item(1)).Trim.ToUpper
                        If SerialAndNumber.Length > 2 Then
                            dd.Rows(i).Item(2) = SerialAndNumber.Substring(0, 2)
                            dd.Rows(i).Item(3) = SerialAndNumber.Substring(2)
                        Else
                            dd.Rows(i).Item(2) = ""
                            dd.Rows(i).Item(3) = ""
                        End If
                        dd.Rows(i).Item(4) = DblParser(CDblSafe(dr.Item(2), 2, 0))
                        dd.Rows(i).Item(5) = DblParser(CDblSafe(dr.Item(3), 2, 0))
                        dd.Rows(i).Item(6) = DblParser(CDblSafe(dr.Item(4), 2, 0))
                        Dim NameAndSurname As String() = CStrSafe(dr.Item(5)).Trim.ToUpper.
                            Split(New Char() {" "c}, StringSplitOptions.RemoveEmptyEntries)
                        dd.Rows(i).Item(7) = NameAndSurname(0)
                        dd.Rows(i).Item(8) = NameAndSurname(NameAndSurname.Length - 1)

                        totalIncome = CRound(totalIncome + CDblSafe(dr.Item(2), 2, 0), 2)
                        totalPayments = CRound(totalPayments + CDblSafe(dr.Item(4), 2, 0), 2)

                        i += 1

                    Next
                End Using

                sd.Rows(0).Item(3) = Math.Ceiling(dd.Rows.Count / 6)
                sd.Rows(0).Item(4) = dd.Rows.Count
                sd.Rows(0).Item(5) = DblParser(totalIncome)
                sd.Rows(0).Item(6) = DblParser(totalPayments)

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
        Public Function GetFfDataDataSet(ByVal declarationDataSet As DataSet,
            ByVal preparatorName As String) As DataSet _
            Implements IDeclaration.GetFfDataDataSet

            If declarationDataSet Is Nothing Then
                Throw New ArgumentNullException("declarationDataSet")
            End If

            If preparatorName Is Nothing Then
                preparatorName = ""
            End If

            Dim i As Integer
            Dim dds As DataSet = declarationDataSet

            ' read ffdata xml structure to dataset
            Dim formDataSet As DataSet = GetFormDataSet(declarationDataSet)

            formDataSet.Tables(1).Rows(0).Item(2) = IO.Path.Combine(AppPath(), FILENAMEMXFDSAM07)
            formDataSet.Tables(0).Rows(0).Item(3) = GetCurrentIdentity.Name
            formDataSet.Tables(0).Rows(0).Item(4) = GetDateInFFDataFormat(Today)

            Dim specificDataRow As DataRow = dds.Tables("Specific").Rows(0)
            For i = 1 To formDataSet.Tables(8).Rows.Count
                If formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "InsurerName" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetLimitedLengthString(
                        dds.Tables("General").Rows(0).Item(0).ToString, 68).ToUpper
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "InsurerCode" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = dds.Tables("General").Rows(0).Item(3)
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "PreparatorDetails" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetLimitedLengthString(
                        preparatorName, 68).ToUpper
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "InsurerPhone" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetLimitedLengthString(
                        dds.Tables("General").Rows(0).Item(8).ToString, 15)
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "JuridicalPersonCode" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = dds.Tables("General").Rows(0).Item(1)
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "InsurerAddress" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetLimitedLengthString(
                        dds.Tables("General").Rows(0).Item(7).ToString, 68).ToUpper  ' email
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "DocDate" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = specificDataRow.Item(0)
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "CycleYear" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = specificDataRow.Item(1)
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "CycleMonth" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = specificDataRow.Item(2)
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "Appendixes2" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = 1
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "Apdx2PageCount" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = Math.Ceiling(CInt(specificDataRow.Item(4)) / 6).ToString
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "Apdx2PersonCount" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = specificDataRow.Item(4)
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "Apdx2InsIncomeSum" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat(CDbl(specificDataRow.Item(5)))
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "Apdx2PaymentSum" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat(CDbl(specificDataRow.Item(6)))
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "ApdxPageCountTotal" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = Math.Ceiling(CInt(specificDataRow.Item(4)) / 6).ToString
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "ManagerFullName" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetLimitedLengthString(
                        dds.Tables("General").Rows(0).Item(9).ToString, 68).ToUpper
                End If
            Next


            Dim j, p As Integer
            Dim pageIncome, pagePayments As Double
            Dim detailsDataTable As DataTable = dds.Tables("Details")
            For i = 1 To Convert.ToInt32(Math.Ceiling(detailsDataTable.Rows.Count / 6))
                pageIncome = 0
                pagePayments = 0
                p = 6 * (i - 1)

                For j = 1 To Math.Min(6, detailsDataTable.Rows.Count - p)

                    If formDataSet.Tables(8).Rows.Count <= (35 + 9 * (j - 1) + (i - 1) * 63) Then _
                        Throw New IndexOutOfRangeException(String.Format("formDataSet index out of range for line: page {0}, line {1}, row {2}.",
                            i.ToString(), j.ToString(), (35 + 9 * (j - 1) + (i - 1) * 63).ToString()))
                    If detailsDataTable.Rows.Count <= (p + j - 1) Then Throw New IndexOutOfRangeException(
                        String.Format("detailsDataTable index out of range: page {0}, line {1}, row {2}.",
                            i.ToString(), j.ToString(), (p + j - 1).ToString()))

                    formDataSet.Tables(8).Rows(27 + 9 * (j - 1) + (i - 1) * 63).Item(1) = p + j
                    formDataSet.Tables(8).Rows(28 + 9 * (j - 1) + (i - 1) * 63).Item(1) =
                        detailsDataTable.Rows(p + j - 1).Item(1)
                    formDataSet.Tables(8).Rows(29 + 9 * (j - 1) + (i - 1) * 63).Item(1) =
                        detailsDataTable.Rows(p + j - 1).Item(2)
                    formDataSet.Tables(8).Rows(30 + 9 * (j - 1) + (i - 1) * 63).Item(1) =
                        detailsDataTable.Rows(p + j - 1).Item(3)
                    formDataSet.Tables(8).Rows(31 + 9 * (j - 1) + (i - 1) * 63).Item(1) =
                        GetNumberInFFDataFormat(CDbl(detailsDataTable.Rows(p + j - 1).Item(4)))
                    formDataSet.Tables(8).Rows(32 + 9 * (j - 1) + (i - 1) * 63).Item(1) =
                        GetNumberInFFDataFormat(CDbl(detailsDataTable.Rows(p + j - 1).Item(5)))
                    formDataSet.Tables(8).Rows(33 + 9 * (j - 1) + (i - 1) * 63).Item(1) =
                        GetNumberInFFDataFormat(CDbl(detailsDataTable.Rows(p + j - 1).Item(6)))
                    formDataSet.Tables(8).Rows(34 + 9 * (j - 1) + (i - 1) * 63).Item(1) =
                        detailsDataTable.Rows(p + j - 1).Item(7).ToString.ToUpper
                    formDataSet.Tables(8).Rows(35 + 9 * (j - 1) + (i - 1) * 63).Item(1) =
                        detailsDataTable.Rows(p + j - 1).Item(8).ToString.ToUpper

                    pageIncome = pageIncome + CDbl(detailsDataTable.Rows(p + j - 1).Item(4))
                    pagePayments = pagePayments + CDbl(detailsDataTable.Rows(p + j - 1).Item(6))
                Next

                If formDataSet.Tables(8).Rows.Count <= (82 + (i - 1) * 63) Then _
                        Throw New IndexOutOfRangeException(String.Format("formDataSet index out of range for page subtotals: page {0}, row {1}.",
                            i.ToString(), (82 + (i - 1) * 63).ToString()))

                formDataSet.Tables(8).Rows(81 + (i - 1) * 63).Item(1) = GetNumberInFFDataFormat(pageIncome)
                formDataSet.Tables(8).Rows(82 + (i - 1) * 63).Item(1) = GetNumberInFFDataFormat(pagePayments)

            Next

            Return formDataSet

        End Function

        Private Function GetFormDataSet(ByVal declarationDataSet As DataSet) As DataSet

            Dim myDoc As New Xml.XmlDocument()
            myDoc.Load(IO.Path.Combine(AppPath(), FILENAMEFFDATASAM07))

            myDoc.ChildNodes(1).ChildNodes(0).ChildNodes(1).ChildNodes(1).ChildNodes(0).ChildNodes(56).InnerText =
                  declarationDataSet.Tables("General").Rows(0).Item(3).ToString
            myDoc.ChildNodes(1).ChildNodes(0).ChildNodes(1).ChildNodes(1).ChildNodes(0).ChildNodes(59).InnerText =
                  declarationDataSet.Tables("Specific").Rows(0).Item(0).ToString

            ' Add 3SD appendixes to the ffdata xml structure if needed
            ' and copy form structure to the temp file
            If CInt(declarationDataSet.Tables("Specific").Rows(0).Item(3)) > 1 Then

                Dim pageCount As Integer = Convert.ToInt32(Math.Ceiling(Convert.ToInt32(
                    declarationDataSet.Tables("Specific").Rows(0).Item(4)) / 6))

                myDoc.ChildNodes(1).ChildNodes(0).ChildNodes(1).ChildNodes(1).ChildNodes(0).ChildNodes(58).InnerText =
                  pageCount.ToString

                For i As Integer = 1 To pageCount - 1
                    Dim addSd As Xml.XmlElement = DirectCast(myDoc.ChildNodes(1).ChildNodes(0).
                        ChildNodes(1).ChildNodes(1).Clone, Xml.XmlElement)
                    addSd.Attributes(1).Value = (i + 2).ToString
                    addSd.ChildNodes(0).ChildNodes(57).InnerText = (i + 1).ToString
                    addSd.ChildNodes(0).ChildNodes(58).InnerText = pageCount.ToString
                    myDoc.ChildNodes(1).ChildNodes(0).ChildNodes(1).AppendChild(addSd)
                    Dim addPg As Xml.XmlElement = DirectCast(myDoc.ChildNodes(1).ChildNodes(0).
                        ChildNodes(0).ChildNodes(0).ChildNodes(1).Clone, Xml.XmlElement)
                    myDoc.ChildNodes(1).ChildNodes(0).ChildNodes(0).ChildNodes(0).AppendChild(addPg)
                Next
                myDoc.ChildNodes(1).ChildNodes(0).ChildNodes(1).Attributes(0).Value = (pageCount + 1).ToString

            End If

            Dim formXml As String = ToXmlString(myDoc)

            Using sr As New IO.StringReader(formXml)

                Dim result As New DataSet
                Try
                    result.ReadXml(sr)
                Catch ex As Exception
                    result.Dispose()
                    Throw
                End Try

                Return result

            End Using

        End Function


        Public Function GetAllErrors(ByVal criteria As DeclarationCriteria) As String _
           Implements IDeclaration.GetAllErrors

            Dim result As String = ""
            Dim currentError As String = ""
            Dim currentIsWarning As Boolean = False

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
