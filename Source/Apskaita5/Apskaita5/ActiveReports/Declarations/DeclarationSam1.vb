Namespace ActiveReports.Declarations

    ''' <summary>
    ''' Represents an implementation of a <see cref="Declaration">Declaration</see>
    ''' for a state social security administration (SODRA) report No. SAM version 1.
    ''' </summary>
    ''' <remarks>Object is responsible for fetching the report data to a dataset
    ''' and transforming the dataset to ffdata format (required by the FormFiller application).</remarks>
    <Serializable()> _
    Public Class DeclarationSam1
        Implements IDeclaration

        Private Const DECLARATION_NAME As String = "SAM v.1"
        Private Const FILENAMEFFDATASAM01 As String = "FFData\SAM-v01.ffdata"
        Private Const FILENAMEMXFDSAM01 As String = "MXFD\SAM-v01.mxfd"


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
                Return "R_Declaration_SAM_1.rdlc"
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
            sd.Columns.Add("Quarter")
            sd.Columns.Add("Tarif")
            sd.Columns.Add("3SDPageCount")
            sd.Columns.Add("TotalPageCount")
            sd.Columns.Add("WorkerCount")
            sd.Columns.Add("TotalIncome")
            sd.Columns.Add("TotalPayments")
            sd.Columns.Add("F1")
            sd.Columns.Add("F2")
            sd.Columns.Add("F3")
            sd.Columns.Add("F4")
            sd.Columns.Add("F6")
            sd.Columns.Add("M1")
            sd.Columns.Add("M2")
            sd.Columns.Add("M3")
            sd.Columns.Add("M3.1")
            sd.Columns.Add("M3.2")
            sd.Columns.Add("M3.3")
            sd.Columns.Add("M4")
            sd.Columns.Add("M5")
            sd.Columns.Add("M6")
            sd.Columns.Add("M7")
            sd.Columns.Add("M7.1")
            sd.Columns.Add("M7.2")
            sd.Columns.Add("M7.3")
            sd.Columns.Add("M8")
            sd.Columns.Add("M11")

            sd.Rows.Add()

            Dim dd As New DataTable("Details")
            dd.Columns.Add("Count")
            dd.Columns.Add("PersonName")
            dd.Columns.Add("PersonCode")
            dd.Columns.Add("SODRACode")
            dd.Columns.Add("Income")
            dd.Columns.Add("Payment")

            Try
                Dim lastDayInQuarter As Integer = 31
                If criteria.Quarter = 2 OrElse criteria.Quarter = 3 Then lastDayInQuarter = 30

                Dim myComm As New SQLCommand("FetchDeclarationSAM(1)_1")
                myComm.AddParam("?YR", criteria.Year)
                myComm.AddParam("?MF", ((criteria.Quarter - 1) * 3) + 1)
                myComm.AddParam("?MT", ((criteria.Quarter - 1) * 3) + 3)
                myComm.AddParam("?DT", New Date(criteria.Year, ((criteria.Quarter - 1) * 3) + 3, lastDayInQuarter))

                Dim totalIncome As Double = 0
                Dim totalPayments As Double = 0
                Dim totalTarif As Double
                Dim i As Integer

                Using myData As DataTable = myComm.Fetch
                    Declaration.ClearDatatable(myData, 0)
                    i = 0
                    For Each dr As DataRow In myData.Rows
                        dd.Rows.Add()
                        dd.Rows(i).Item(0) = i + 1
                        dd.Rows(i).Item(1) = GetLimitedLengthString(dr.Item(0).ToString.Trim, 68).ToUpper
                        dd.Rows(i).Item(2) = dr.Item(1).ToString.Trim
                        dd.Rows(i).Item(3) = dr.Item(2).ToString.Trim
                        dd.Rows(i).Item(4) = DblParser(CDbl(dr.Item(3)))
                        dd.Rows(i).Item(5) = DblParser(CDbl(dr.Item(4)))
                        totalIncome = totalIncome + CDbl(dr.Item(3))
                        totalPayments = totalPayments + CDbl(dr.Item(4))
                        i += 1
                    Next
                End Using

                myComm = New SQLCommand("FetchDeclarationSAM(1)_2")
                myComm.AddParam("?YR", criteria.Year)
                myComm.AddParam("?MF", ((criteria.Quarter - 1) * 3) + 1)
                myComm.AddParam("?MT", ((criteria.Quarter - 1) * 3) + 3)

                Using myData As DataTable = myComm.Fetch
                    Declaration.ClearDatatable(myData, 0)

                    If myData.Rows.Count > 1 Then

                        Dim ratesList As New List(Of String)

                        For Each dr As DataRow In myData.Rows
                            ratesList.Add(String.Format(My.Resources.ActiveReports_Declarations_DeclarationSam1_MultipleRateItem, _
                                CDblSafe(dr.Item(0), 2, 0).ToString, CDblSafe(dr.Item(2), 2, 0).ToString, _
                                CDblSafe(dr.Item(1), 2, 0).ToString, CDblSafe(dr.Item(3), 2, 0).ToString))
                        Next

                        warnings = AddWithNewLine(warnings, String.Format( _
                            My.Resources.ActiveReports_Declarations_DeclarationSam1_MultipleRates, _
                            vbCrLf, String.Join(vbCrLf, ratesList.ToArray), vbCrLf), False)

                    ElseIf myData.Rows.Count < 1 Then

                        myData.Rows.Add()
                        myData.Rows(0).Item(0) = GetCurrentCompany.Rates.GetRate(General.DefaultRateType.SodraEmployee)
                        myData.Rows(0).Item(1) = GetCurrentCompany.Rates.GetRate(General.DefaultRateType.SodraEmployer)
                        myData.Rows(0).Item(2) = GetCurrentCompany.Rates.GetRate(General.DefaultRateType.PsdEmployee)
                        myData.Rows(0).Item(3) = GetCurrentCompany.Rates.GetRate(General.DefaultRateType.PsdEmployer)

                        warnings = AddWithNewLine(warnings, My.Resources.ActiveReports_Declarations_DeclarationSam1_NullRates, False)

                    End If

                    If criteria.Year < 2009 Then
                        totalTarif = CDbl(myData.Rows(0).Item(0)) + CDbl(myData.Rows(0).Item(1))
                    Else
                        totalTarif = CDbl(myData.Rows(0).Item(0)) + CDbl(myData.Rows(0).Item(1)) _
                            + CDbl(myData.Rows(0).Item(2)) + CDbl(myData.Rows(0).Item(3))
                    End If

                End Using

                sd.Rows(0).Item(0) = criteria.Date.ToString("yyyy-MM-dd")
                sd.Rows(0).Item(1) = criteria.SodraDepartment
                sd.Rows(0).Item(2) = criteria.Year
                sd.Rows(0).Item(3) = criteria.Quarter
                sd.Rows(0).Item(4) = DblParser(totalTarif)
                sd.Rows(0).Item(5) = Math.Ceiling(dd.Rows.Count / 9)
                sd.Rows(0).Item(6) = Math.Ceiling(dd.Rows.Count / 9) + 1
                sd.Rows(0).Item(7) = dd.Rows.Count
                sd.Rows(0).Item(8) = DblParser(totalIncome)
                sd.Rows(0).Item(9) = DblParser(totalPayments)

                myComm = New SQLCommand("FetchDeclarationSAM(1)_3")
                myComm.AddParam("?DF", New Date(criteria.Year, ((criteria.Quarter - 1) * 3) + 1, 1))
                myComm.AddParam("?DT", New Date(criteria.Year, ((criteria.Quarter - 1) * 3) + 3, lastDayInQuarter))

                Using myData As DataTable = myComm.Fetch
                    sd.Rows(0).Item(10) = CInt(myData.Rows(0).Item(1)) - CInt(myData.Rows(1).Item(1))
                    sd.Rows(0).Item(11) = CInt(myData.Rows(2).Item(1))
                    sd.Rows(0).Item(12) = CInt(myData.Rows(3).Item(1))
                    sd.Rows(0).Item(13) = CInt(myData.Rows(0).Item(1)) - _
                        CInt(myData.Rows(1).Item(1)) + CInt(myData.Rows(2).Item(1)) _
                        - CInt(myData.Rows(3).Item(1))
                End Using

                Dim balanceYearStart As Double = 0
                Dim paymentsDueQuarterStart As Double = 0
                Dim paymentsDueQuarter As Double = 0
                Dim paymentsDue1 As Double = 0
                Dim paymentsDue2 As Double = 0
                Dim paymentsDue3 As Double = 0
                Dim paymentsDueQuarterEnd As Double = 0
                Dim paymentsBalanceQuarterEnd As Double = 0
                Dim paymentsMadeQuarterStart As Double = 0
                Dim paymentsMadeQuarter As Double = 0
                Dim paymentsMade1 As Double = 0
                Dim paymentsMade2 As Double = 0
                Dim paymentsMade3 As Double = 0
                Dim paymentsMadeQuarterEnd As Double = 0
                Dim balanceQuarterEnd As Double = 0

                Dim totalIncomePerQuarter As Double = 0

                ' there is a problem with this query:
                ' a journal entry that transfers sums between pripary and secondary
                ' accounts is treated as affecting balance at the begining of the year
                myComm = New SQLCommand("FetchDeclarationSAM(1)_4")
                myComm.AddParam("?YR", criteria.Year)
                myComm.AddParam("?SP", criteria.SodraAccount)
                myComm.AddParam("?SS", criteria.SodraAccount2)

                Using myData As DataTable = myComm.Fetch

                    Declaration.ClearDatatable(myData, 0)

                    ' simplification (joining) by month
                    Dim j As Integer
                    For i = myData.Rows.Count To 1 Step -1
                        For j = 1 To i - 1
                            If CInt(myData.Rows(i - 1).Item(0)) = CInt(myData.Rows(j - 1).Item(0)) Then
                                myData.Rows(j - 1).Item(1) = CRound(CDbl(myData.Rows(j - 1).Item(1))) _
                                    + CRound(CDbl(myData.Rows(i - 1).Item(1)))
                                myData.Rows(j - 1).Item(2) = CRound(CDbl(myData.Rows(j - 1).Item(2))) _
                                    + CRound(CDbl(myData.Rows(i - 1).Item(2)))
                                myData.Rows(j - 1).Item(3) = CRound(CDbl(myData.Rows(j - 1).Item(3))) _
                                    + CRound(CDbl(myData.Rows(i - 1).Item(3)))
                                myData.Rows.RemoveAt(i - 1)
                                Exit For
                            End If
                        Next
                    Next

                    ' QuarterStartMonth and QuarterEndMonth
                    Dim qsm As Integer = ((criteria.Quarter - 1) * 3) + 1
                    Dim qem As Integer = ((criteria.Quarter - 1) * 3) + 3

                    For Each dr As DataRow In myData.Rows
                        If CInt(dr.Item(0)) < qsm Then
                            paymentsDueQuarterStart = paymentsDueQuarterStart + CDbl(dr.Item(2))
                            paymentsMadeQuarterStart = paymentsMadeQuarterStart + CDbl(dr.Item(3))

                        ElseIf CInt(dr.Item(0)) >= qsm AndAlso CInt(dr.Item(0)) <= qem Then

                            If CInt(dr.Item(0)) = qsm Then
                                paymentsDue1 = paymentsDue1 + CDbl(dr.Item(2))
                                paymentsMade1 = paymentsMade1 + CDbl(dr.Item(3))
                            ElseIf CInt(dr.Item(0)) = qem Then
                                paymentsDue3 = paymentsDue3 + CDbl(dr.Item(2))
                                paymentsMade3 = paymentsMade3 + CDbl(dr.Item(3))
                            Else
                                paymentsDue2 = paymentsDue2 + CDbl(dr.Item(2))
                                paymentsMade2 = paymentsMade2 + CDbl(dr.Item(3))
                            End If

                            totalIncomePerQuarter = totalIncomePerQuarter + CDbl(dr.Item(1))

                        ElseIf CInt(dr.Item(0)) = 13 Then
                            balanceYearStart = balanceYearStart + CDbl(dr.Item(3))

                        End If
                    Next

                End Using

                paymentsDueQuarter = CRound(paymentsDue1 + paymentsDue2 + paymentsDue3, 2)
                paymentsDueQuarterEnd = CRound(paymentsDueQuarterStart + paymentsDueQuarter, 2)
                paymentsBalanceQuarterEnd = CRound(balanceYearStart + paymentsDueQuarterEnd, 2)

                paymentsMadeQuarter = CRound(paymentsMade1 + paymentsMade2 + paymentsMade3, 2)
                paymentsMadeQuarterEnd = CRound(paymentsMadeQuarterStart + paymentsMadeQuarter, 2)
                balanceQuarterEnd = CRound(paymentsBalanceQuarterEnd - paymentsMadeQuarterEnd, 2)

                sd.Rows(0).Item(14) = DblParser(totalIncomePerQuarter)
                sd.Rows(0).Item(15) = DblParser(balanceYearStart)
                sd.Rows(0).Item(16) = DblParser(paymentsDueQuarterStart)
                sd.Rows(0).Item(17) = DblParser(paymentsDueQuarter)
                sd.Rows(0).Item(18) = DblParser(paymentsDue1)
                sd.Rows(0).Item(19) = DblParser(paymentsDue2)
                sd.Rows(0).Item(20) = DblParser(paymentsDue3)
                sd.Rows(0).Item(21) = DblParser(paymentsDueQuarterEnd)
                sd.Rows(0).Item(22) = DblParser(paymentsBalanceQuarterEnd)
                sd.Rows(0).Item(23) = DblParser(paymentsMadeQuarterStart)
                sd.Rows(0).Item(24) = DblParser(paymentsMadeQuarter)
                sd.Rows(0).Item(25) = DblParser(paymentsMade1)
                sd.Rows(0).Item(26) = DblParser(paymentsMade2)
                sd.Rows(0).Item(27) = DblParser(paymentsMade3)
                sd.Rows(0).Item(28) = DblParser(paymentsMadeQuarterEnd)
                sd.Rows(0).Item(29) = DblParser(balanceQuarterEnd)

                If criteria.Quarter = 1 Then
                    sd.Rows(0).Item(16) = ""
                    sd.Rows(0).Item(23) = ""
                End If

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

            Dim declarationFilePath As String = IO.Path.Combine(AppPath(), FILENAMEFFDATASAM01)
            Dim tempPath As String = IO.Path.Combine(AppPath(), "temp.ffdata")
            Try
                If IO.File.Exists(tempPath) Then
                    IO.File.Delete(tempPath)
                End If
            Catch ex As Exception
            End Try

            ' Add 3SD appendixes to the ffdata xml structure if needed
            ' and copy form structure to the temp file
            If CInt(dds.Tables("Specific").Rows(0).Item(5)) > 1 Then

                Dim myDoc As New Xml.XmlDocument
                myDoc.Load(declarationFilePath)

                For i = 1 To CInt(dds.Tables("Specific").Rows(0).Item(5)) - 1
                    Dim addSd As Xml.XmlElement = DirectCast(myDoc.ChildNodes(1).ChildNodes(0). _
                        ChildNodes(1).ChildNodes(2).Clone, Xml.XmlElement)
                    addSd.Attributes(1).Value = (i + 3).ToString
                    myDoc.ChildNodes(1).ChildNodes(0).ChildNodes(1).AppendChild(addSd)
                    Dim addPg As Xml.XmlElement = DirectCast(myDoc.ChildNodes(1).ChildNodes(0). _
                        ChildNodes(0).ChildNodes(0).ChildNodes(2).Clone, Xml.XmlElement)
                    myDoc.ChildNodes(1).ChildNodes(0).ChildNodes(0).ChildNodes(0).AppendChild(addPg)
                Next
                myDoc.ChildNodes(1).ChildNodes(0).ChildNodes(1).Attributes(0).Value = _
                    (2 + CInt(dds.Tables("Specific").Rows(0).Item(5))).ToString

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
            formDataSet.Tables(1).Rows(0).Item(2) = IO.Path.Combine(AppPath(), FILENAMEMXFDSAM01)

            Dim SD3F As Boolean = False
            Dim specificDataRow As DataRow = dds.Tables("Specific").Rows(0)
            For i = 1 To formDataSet.Tables(8).Rows.Count
                If formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "FormCode" Then _
                    SD3F = (formDataSet.Tables(8).Rows(i - 1).Item(1).ToString = "SAM3SD")
                If formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "InsurerName" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetLimitedLengthString( _
                        dds.Tables("General").Rows(0).Item(0).ToString, 68).ToUpper
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "InsurerCode" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = dds.Tables("General").Rows(0).Item(3)
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "PreparatorDetails" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetLimitedLengthString( _
                        preparatorName, 68).ToUpper
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "InsurerPhone" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetLimitedLengthString(dds.Tables("General").Rows(0).Item(8).ToString, 15)
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
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "CycleQuarter" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = specificDataRow.Item(3)
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "Appendixes1" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = 1
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "Apdx1PageCount" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = 1
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "Appendixes2" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = 1
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "Apdx2PageCount" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = specificDataRow.Item(5)
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "PageTotal" And SD3F Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = specificDataRow.Item(5)
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "Apdx2PersonCount" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = specificDataRow.Item(7)
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "Apdx2InsIncomeSum" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat(CDbl(specificDataRow.Item(8)))
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "Apdx2PaymentSum" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat(CDbl(specificDataRow.Item(9)))
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "ManagerJobPosition" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = "DIREKTORIUS"
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "ManagerFullName" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetLimitedLengthString( _
                        dds.Tables("General").Rows(0).Item(9).ToString, 68).ToUpper
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "PersonCountQuarterStart" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = specificDataRow.Item(10)
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "PersonCountStarted" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = specificDataRow.Item(11)
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "PersonCountEnded" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = specificDataRow.Item(12)
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "PersonCountQuarterEnd" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = specificDataRow.Item(13)
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "IncomeSum" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat( _
                        CDbl(specificDataRow.Item(14)))
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "ChargedDebtYearStart" Then
                    If String.IsNullOrEmpty(specificDataRow.Item(15).ToString.Trim) Then
                        formDataSet.Tables(8).Rows(i - 1).Item(1) = ""
                    Else
                        formDataSet.Tables(8).Rows(i - 1).Item(1) = _
                            GetNumberInFFDataFormat(CDbl(specificDataRow.Item(15)))
                    End If
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "ChargedDebtQuarterEnd" Then
                    If String.IsNullOrEmpty(specificDataRow.Item(16).ToString.Trim) Then
                        formDataSet.Tables(8).Rows(i - 1).Item(1) = ""
                    Else
                        formDataSet.Tables(8).Rows(i - 1).Item(1) = _
                            GetNumberInFFDataFormat(CDbl(specificDataRow.Item(16)))
                    End If
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "ChargedMonthly" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat( _
                        CDbl(specificDataRow.Item(17)))
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "ChargedMonth1" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat( _
                        CDbl(specificDataRow.Item(18)))
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "ChargedMonth2" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat( _
                        CDbl(specificDataRow.Item(19)))
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "ChargedMonth3" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat( _
                        CDbl(specificDataRow.Item(20)))
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "ChargedYearTotal" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat( _
                        CDbl(specificDataRow.Item(21)))
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "ChargedTotal" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat( _
                        CDbl(specificDataRow.Item(22)))
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "TransferQuarterEnd" Then
                    If String.IsNullOrEmpty(specificDataRow.Item(23).ToString.Trim) Then
                        formDataSet.Tables(8).Rows(i - 1).Item(1) = ""
                    Else
                        formDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat( _
                            CDbl(specificDataRow.Item(23)))
                    End If
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "TransferMonthly" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat( _
                        CDbl(specificDataRow.Item(24)))
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "TransferMonth1" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat( _
                        CDbl(specificDataRow.Item(25)))
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "TransferMonth2" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat( _
                        CDbl(specificDataRow.Item(26)))
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "TransferMonth3" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat( _
                        CDbl(specificDataRow.Item(27)))
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "TransferYearTotal" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat( _
                        CDbl(specificDataRow.Item(28)))
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "DebtQuarterEnd" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat( _
                        CDbl(specificDataRow.Item(29)))
                End If
            Next

            Dim j, p As Integer
            Dim pageIncome As Double = 0
            Dim pagePayments As Double = 0
            Dim detailsDataTable As DataTable = dds.Tables("Details")
            For i = 1 To CInt(specificDataRow.Item(5))
                p = 9 * (i - 1)
                For j = 1 To Math.Min(9, CInt(specificDataRow.Item(7)) - p)
                    If i = 1 Then
                        formDataSet.Tables(8).Rows(87 + 7 * (j - 1)).Item(1) = j
                        formDataSet.Tables(8).Rows(88 + 7 * (j - 1)).Item(1) = _
                            detailsDataTable.Rows(p + j - 1).Item(2)
                        formDataSet.Tables(8).Rows(89 + 7 * (j - 1)).Item(1) = _
                            detailsDataTable.Rows(p + j - 1).Item(3).ToString.Trim.Substring(0, 2)
                        formDataSet.Tables(8).Rows(90 + 7 * (j - 1)).Item(1) = _
                            GetNumericPart(detailsDataTable.Rows(p + j - 1).Item(3).ToString.Trim)
                        formDataSet.Tables(8).Rows(91 + 7 * (j - 1)).Item(1) = _
                            GetNumberInFFDataFormat(CDbl(detailsDataTable.Rows(p + j - 1).Item(4)))
                        formDataSet.Tables(8).Rows(92 + 7 * (j - 1)).Item(1) = _
                            GetNumberInFFDataFormat(CDbl(detailsDataTable.Rows(p + j - 1).Item(5)))
                        formDataSet.Tables(8).Rows(93 + 7 * (j - 1)).Item(1) = _
                            detailsDataTable.Rows(p + j - 1).Item(1).ToString.ToUpper
                    Else
                        formDataSet.Tables(8).Rows(87 + 7 * (j - 1) + (i - 1) * 72).Item(1) = j
                        formDataSet.Tables(8).Rows(88 + 7 * (j - 1) + (i - 1) * 72).Item(1) = _
                            detailsDataTable.Rows(p + j - 1).Item(2)
                        formDataSet.Tables(8).Rows(89 + 7 * (j - 1) + (i - 1) * 72).Item(1) = _
                            detailsDataTable.Rows(p + j - 1).Item(3).ToString.Trim.Substring(0, 2)
                        formDataSet.Tables(8).Rows(90 + 7 * (j - 1) + (i - 1) * 72).Item(1) = _
                            GetNumericPart(detailsDataTable.Rows(p + j - 1).Item(3).ToString.Trim)
                        formDataSet.Tables(8).Rows(91 + 7 * (j - 1) + (i - 1) * 72).Item(1) = _
                            GetNumberInFFDataFormat(CDbl(detailsDataTable.Rows(p + j - 1).Item(4)))
                        formDataSet.Tables(8).Rows(92 + 7 * (j - 1) + (i - 1) * 72).Item(1) = _
                            GetNumberInFFDataFormat(CDbl(detailsDataTable.Rows(p + j - 1).Item(5)))
                        formDataSet.Tables(8).Rows(93 + 7 * (j - 1) + (i - 1) * 72).Item(1) = _
                            detailsDataTable.Rows(p + j - 1).Item(1).ToString.ToUpper
                    End If
                    pageIncome = pageIncome + CDbl(detailsDataTable.Rows(p + j - 1).Item(4))
                    pagePayments = pagePayments + CDbl(detailsDataTable.Rows(p + j - 1).Item(5))
                Next
                If i = 1 Then
                    formDataSet.Tables(8).Rows(150).Item(1) = GetNumberInFFDataFormat(pageIncome)
                    formDataSet.Tables(8).Rows(151).Item(1) = GetNumberInFFDataFormat(pagePayments)
                Else
                    formDataSet.Tables(8).Rows(150 + (i - 1) * 72).Item(1) = GetNumberInFFDataFormat(pageIncome)
                    formDataSet.Tables(8).Rows(151 + (i - 1) * 72).Item(1) = GetNumberInFFDataFormat(pagePayments)
                End If
                pageIncome = 0
                pagePayments = 0
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

            If Not criteria.TryValidateQuarter(currentIsWarning, currentError) Then
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

            If Not criteria.TryValidateSodraAccount(currentIsWarning, currentError) Then
                If Not currentIsWarning Then
                    result = AddWithNewLine(result, currentError, False)
                End If
            End If

            currentError = ""
            currentIsWarning = False

            If Not criteria.TryValidateSodraAccount2(currentIsWarning, currentError) Then
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

            If Not criteria.TryValidateQuarter(currentIsWarning, currentError) Then
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

            If Not criteria.TryValidateSodraAccount(currentIsWarning, currentError) Then
                If currentIsWarning Then
                    result = AddWithNewLine(result, currentError, False)
                End If
            End If

            currentError = ""
            currentIsWarning = False

            If Not criteria.TryValidateSodraAccount2(currentIsWarning, currentError) Then
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

            If Not criteria.TryValidateQuarter(currentIsWarning, currentError) Then
                If currentIsWarning Then Return True
            End If

            currentError = ""
            currentIsWarning = False

            If Not criteria.TryValidateYear(currentIsWarning, currentError) Then
                If currentIsWarning Then Return True
            End If

            currentError = ""
            currentIsWarning = False

            If Not criteria.TryValidateSodraAccount(currentIsWarning, currentError) Then
                If currentIsWarning Then Return True
            End If

            currentError = ""
            currentIsWarning = False

            If Not criteria.TryValidateSodraAccount2(currentIsWarning, currentError) Then
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

            If Not criteria.TryValidateQuarter(currentIsWarning, currentError) Then
                If Not currentIsWarning Then Return False
            End If

            currentError = ""
            currentIsWarning = False

            If Not criteria.TryValidateYear(currentIsWarning, currentError) Then
                If Not currentIsWarning Then Return False
            End If

            currentError = ""
            currentIsWarning = False

            If Not criteria.TryValidateSodraAccount(currentIsWarning, currentError) Then
                If Not currentIsWarning Then Return False
            End If

            currentError = ""
            currentIsWarning = False

            If Not criteria.TryValidateSodraAccount2(currentIsWarning, currentError) Then
                If Not currentIsWarning Then Return False
            End If

            Return True

        End Function


        Public Overrides Function ToString() As String
            Return DECLARATION_NAME
        End Function

    End Class

End Namespace