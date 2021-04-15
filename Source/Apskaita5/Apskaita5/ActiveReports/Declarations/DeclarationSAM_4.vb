Namespace ActiveReports.Declarations

    ''' <summary>
    ''' Represents an implementation of a <see cref="Declaration">Declaration</see>
    ''' for a state social security administration (SODRA) report No. SAM version 4.
    ''' </summary>
    ''' <remarks>Object is responsible for fetching the report data to a dataset
    ''' and transforming the dataset to ffdata format (required by the FormFiller application).</remarks>
    <Serializable()> _
    Public Class DeclarationSAM_4
        Implements IDeclaration

        Private Const DECLARATION_NAME As String = "SAM v.4"
        Private Const FILENAMEMXFDSAM04 As String = "MXFD\SAM-v04.mxfd"
        Private Const FILENAMEFFDATASAM04 As String = "FFData\SAM-v04.ffdata"


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
                Return "R_Declaration_SAM_2.rdlc"
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
            sd.Columns.Add("Year")
            sd.Columns.Add("Month")
            sd.Columns.Add("Tarif")
            sd.Columns.Add("3SDPageCount")
            sd.Columns.Add("TotalWorkerCount")
            sd.Columns.Add("TotalIncome")
            sd.Columns.Add("TotalPayments")

            sd.Rows.Add()

            sd.Rows(0).Item(0) = criteria.Date.ToShortDateString
            sd.Rows(0).Item(1) = criteria.SodraDepartment
            sd.Rows(0).Item(2) = criteria.Year
            sd.Rows(0).Item(3) = criteria.Month

            Dim dd As New DataTable("Details")
            dd.Columns.Add("Count")
            dd.Columns.Add("PersonCode")
            dd.Columns.Add("SODRACodeSerial")
            dd.Columns.Add("SODRACode")
            dd.Columns.Add("PersonName")
            dd.Columns.Add("Income")
            dd.Columns.Add("Payment")

            Try

                Dim myComm As SQLCommand

                sd.Rows(0).Item(4) = DblParser(criteria.SodraRate)

                myComm = New SQLCommand("FetchDeclarationSAM(3)_1")
                myComm.AddParam("?YR", criteria.Year)
                myComm.AddParam("?MN", criteria.Month)
                myComm.AddParam("?RT", criteria.SodraRate)
                myComm.AddParam("?DT", New Date(criteria.Year, criteria.Month, _
                    Date.DaysInMonth(criteria.Year, criteria.Month)))

                Dim totalIncome As Double = 0
                Dim totalPayments As Double = 0
                Dim i As Integer = 0

                Using myData As DataTable = myComm.Fetch
                    For Each dr As DataRow In myData.Rows

                        dd.Rows.Add()

                        dd.Rows(i).Item(0) = i + 1
                        dd.Rows(i).Item(1) = CStrSafe(dr.Item(1)).Trim
                        If CStrSafe(dr.Item(2)).Trim.Length > 1 Then
                            dd.Rows(i).Item(2) = CStrSafe(dr.Item(2)).Trim.Substring(0, 2)
                            dd.Rows(i).Item(3) = GetNumericPart(CStrSafe(dr.Item(2)).Trim)
                        Else
                            dd.Rows(i).Item(2) = ""
                            dd.Rows(i).Item(3) = ""
                        End If
                        dd.Rows(i).Item(4) = CStrSafe(dr.Item(0)).Trim.ToUpper
                        dd.Rows(i).Item(5) = DblParser(CDblSafe(dr.Item(3), 2, 0))
                        dd.Rows(i).Item(6) = DblParser(CDblSafe(dr.Item(4), 2, 0))

                        totalIncome = CRound(totalIncome + CDblSafe(dr.Item(3), 2, 0), 2)
                        totalPayments = CRound(totalPayments + CDblSafe(dr.Item(4), 2, 0), 2)

                        i += 1

                    Next
                End Using

                sd.Rows(0).Item(5) = Math.Ceiling(dd.Rows.Count / 9)
                sd.Rows(0).Item(6) = dd.Rows.Count
                sd.Rows(0).Item(7) = DblParser(totalIncome)
                sd.Rows(0).Item(8) = DblParser(totalPayments)

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

            Dim i As Integer
            Dim dds As DataSet = declarationDataSet
            Dim currentUser As AccDataAccessLayer.Security.AccIdentity = GetCurrentIdentity()

            Dim declarationFilePath As String = IO.Path.Combine(AppPath(), FILENAMEFFDATASAM04)
            Dim tempPath As String = IO.Path.Combine(AppPath(), "temp.ffdata")
            Try
                If IO.File.Exists(tempPath) Then
                    IO.File.Delete(tempPath)
                End If
            Catch ex As Exception
            End Try

            ' Add 3SD appendixes to the ffdata xml structure if needed
            ' and copy form structure to the temp file
            If CInt(dds.Tables("Specific").Rows(0).Item(6)) > 1 Then

                Dim myDoc As New Xml.XmlDocument
                myDoc.Load(declarationFilePath)

                For i = 1 To Convert.ToInt32(Math.Ceiling(CInt(dds.Tables("Specific").Rows(0).Item(6)) / 9) - 1)
                    Dim addSd As Xml.XmlElement = DirectCast(myDoc.ChildNodes(1).ChildNodes(0). _
                        ChildNodes(1).ChildNodes(1).Clone, Xml.XmlElement)
                    addSd.Attributes(1).Value = (i + 2).ToString
                    addSd.ChildNodes(0).ChildNodes(2).InnerText = (i + 1).ToString
                    addSd.ChildNodes(0).ChildNodes(3).InnerText = _
                        Math.Ceiling(CInt(dds.Tables("Specific").Rows(0).Item(6)) / 9).ToString
                    myDoc.ChildNodes(1).ChildNodes(0).ChildNodes(1).AppendChild(addSd)
                    Dim addPg As Xml.XmlElement = DirectCast(myDoc.ChildNodes(1).ChildNodes(0). _
                        ChildNodes(0).ChildNodes(0).ChildNodes(1).Clone, Xml.XmlElement)
                    myDoc.ChildNodes(1).ChildNodes(0).ChildNodes(0).ChildNodes(0).AppendChild(addPg)
                Next
                myDoc.ChildNodes(1).ChildNodes(0).ChildNodes(1).Attributes(0).Value = _
                    (Math.Ceiling(CInt(dds.Tables("Specific").Rows(0).Item(6)) / 9) + 1).ToString

                myDoc.Save(tempPath)

            Else

                IO.File.Copy(declarationFilePath, tempPath)

            End If

            ' read ffdata xml structure to dataset
            Dim formDataSet As New DataSet
            Using formFileStream As IO.FileStream = New IO.FileStream(tempPath, IO.FileMode.Open)
                formDataSet.ReadXml(formFileStream)
                formFileStream.Close()
            End Using

            Try
                IO.File.Delete(tempPath)
            Catch ex As Exception
            End Try

            formDataSet.Tables(0).Rows(0).Item(3) = currentUser.Name
            formDataSet.Tables(0).Rows(0).Item(4) = GetDateInFFDataFormat(Today)
            formDataSet.Tables(1).Rows(0).Item(2) = IO.Path.Combine(AppPath(), FILENAMEMXFDSAM04)

            Dim specificDataRow As DataRow = dds.Tables("Specific").Rows(0)
            For i = 1 To formDataSet.Tables(8).Rows.Count
                If formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "InsurerName" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetLimitedLengthString( _
                        dds.Tables("General").Rows(0).Item(0).ToString, 68).ToUpper
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "InsurerCode" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = dds.Tables("General").Rows(0).Item(3)
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "PreparatorDetails" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetLimitedLengthString( _
                        preparatorName, 68).ToUpper
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "InsurerPhone" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetLimitedLengthString( _
                        dds.Tables("General").Rows(0).Item(8).ToString, 15)
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "JuridicalPersonCode" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = dds.Tables("General").Rows(0).Item(1)
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "InsurerAddress" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetLimitedLengthString( _
                        dds.Tables("General").Rows(0).Item(2).ToString, 68).ToUpper
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "RecipientDepName" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = specificDataRow.Item(1).ToString.ToUpper
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "DocDate" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetDateInFFDataFormat(CDate(specificDataRow.Item(0)))
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "TaxRateRep" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat(CDbl(specificDataRow.Item(4)))
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "CycleYear" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = specificDataRow.Item(2)
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "CycleMonth" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = specificDataRow.Item(3)
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "Appendixes2" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = 1
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "Apdx2PageCount" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = Math.Ceiling(CInt(specificDataRow.Item(6)) / 9).ToString
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "Apdx2PersonCount" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = specificDataRow.Item(6)
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "Apdx2InsIncomeSum" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat(CDbl(specificDataRow.Item(7)))
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "Apdx2PaymentSum" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat(CDbl(specificDataRow.Item(8)))
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "ManagerJobPosition" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = "DIREKTORIUS"
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "ManagerFullName" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetLimitedLengthString( _
                        dds.Tables("General").Rows(0).Item(9).ToString, 68).ToUpper
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "ApdxPageCountTotal" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = Math.Ceiling(CInt(specificDataRow.Item(6)) / 9).ToString
                End If
            Next


            Dim j, p As Integer
            Dim pageIncome, pagePayments As Double
            Dim detailsDataTable As DataTable = dds.Tables("Details")
            For i = 1 To Convert.ToInt32(Math.Ceiling(detailsDataTable.Rows.Count / 9))
                pageIncome = 0
                pagePayments = 0
                p = 9 * (i - 1)

                For j = 1 To Math.Min(9, detailsDataTable.Rows.Count - p)
                    formDataSet.Tables(8).Rows(38 + 7 * (j - 1) + (i - 1) * 72).Item(1) = p + j
                    formDataSet.Tables(8).Rows(39 + 7 * (j - 1) + (i - 1) * 72).Item(1) = _
                        detailsDataTable.Rows(p + j - 1).Item(1)
                    formDataSet.Tables(8).Rows(40 + 7 * (j - 1) + (i - 1) * 72).Item(1) = _
                        detailsDataTable.Rows(p + j - 1).Item(2)
                    formDataSet.Tables(8).Rows(41 + 7 * (j - 1) + (i - 1) * 72).Item(1) = _
                        detailsDataTable.Rows(p + j - 1).Item(3)
                    formDataSet.Tables(8).Rows(42 + 7 * (j - 1) + (i - 1) * 72).Item(1) = _
                        GetNumberInFFDataFormat(CDbl(detailsDataTable.Rows(p + j - 1).Item(5)))
                    formDataSet.Tables(8).Rows(43 + 7 * (j - 1) + (i - 1) * 72).Item(1) = _
                        GetNumberInFFDataFormat(CDbl(detailsDataTable.Rows(p + j - 1).Item(6)))
                    formDataSet.Tables(8).Rows(44 + 7 * (j - 1) + (i - 1) * 72).Item(1) = _
                        detailsDataTable.Rows(p + j - 1).Item(4).ToString.ToUpper

                    pageIncome = pageIncome + CDbl(detailsDataTable.Rows(p + j - 1).Item(5))
                    pagePayments = pagePayments + CDbl(detailsDataTable.Rows(p + j - 1).Item(6))
                Next

                formDataSet.Tables(8).Rows(101 + (i - 1) * 72).Item(1) = GetNumberInFFDataFormat(pageIncome)
                formDataSet.Tables(8).Rows(102 + (i - 1) * 72).Item(1) = GetNumberInFFDataFormat(pagePayments)
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

            currentError = ""
            currentIsWarning = False

            If Not criteria.TryValidateSodraRate(currentIsWarning, currentError) Then
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

            currentError = ""
            currentIsWarning = False

            If Not criteria.TryValidateSodraRate(currentIsWarning, currentError) Then
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

            currentError = ""
            currentIsWarning = False

            If Not criteria.TryValidateSodraRate(currentIsWarning, currentError) Then
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

            currentError = ""
            currentIsWarning = False

            If Not criteria.TryValidateSodraRate(currentIsWarning, currentError) Then
                If Not currentIsWarning Then Return False
            End If

            Return True

        End Function


        Public Overrides Function ToString() As String
            Return DECLARATION_NAME
        End Function

    End Class

End Namespace
