Namespace ActiveReports.Declarations

    ''' <summary>
    ''' Represents an implementation of a <see cref="Declaration">Declaration</see>
    ''' for a state tax inspectorate (VMI) report No. GPM312 version 1.
    ''' </summary>
    ''' <remarks>Object is responsible for fetching the report data to a dataset
    ''' and transforming the dataset to ffdata format (required by the FormFiller application).</remarks>
    <Serializable()>
    Public Class DeclarationGPM312_1
        Implements IDeclaration

        Private Const DECLARATION_NAME As String = "GPM312 v.1"
        Private Const FILENAMEMXFDGPM312_1 As String = "MXFD\GPM312(1).mxfd"
        Private Const FILENAMEFFDATAGPM312_1 As String = "FFData\GPM312(1).ffdata"


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
                Return New Date(2018, 1, 1)
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
                Return 2
            End Get
        End Property

        ''' <summary>
        ''' Gets a name of the rdlc file that should be used to print the declaration.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property RdlcFileName() As String _
            Implements IDeclaration.RdlcFileName
            Get
                Return "R_Declaration_GPM312_1.rdlc"
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
            sd.Columns.Add("G4")
            sd.Columns.Add("G5")
            sd.Columns.Add("G6")

            sd.Rows.Add()

            sd.Rows(0).Item(0) = criteria.Date.ToShortDateString
            sd.Rows(0).Item(1) = criteria.Year

            Dim gpmCode As String = ""
            Try
                gpmCode = ApskaitaObjects.Settings.CommonSettings.GetCurrentProxy().CodeWageGPM
            Catch ex As Exception
            End Try

            Dim AppendixL As New DataTable("AppendixL")
            For i As Integer = 1 To 11
                AppendixL.Columns.Add(String.Format("L{0}", i.ToString()))
            Next
            Dim AppendixU As New DataTable("AppendixU")
            For i As Integer = 1 To 15
                AppendixU.Columns.Add(String.Format("U{0}", i.ToString()))
            Next

            Dim G4 As Double = 0.0
            Dim G5 As Double = 0.0
            Dim G6 As Double = 0.0

            Try

                Dim myComm As New SQLCommand(IIf(criteria.Year >= 2019, _
                    "FetchDeclarationGPM312_2AppendixL", _
                    "FetchDeclarationGPM312_1AppendixL").ToString())
                myComm.AddParam("?YR", criteria.Year)
                myComm.AddParam("?DS", gpmCode)
                myComm.AddParam("?LT", StateCodeLith.ToUpper())
                If criteria.Year >= 2019 Then myComm.AddParam("?LG", "03")

                Using myData As DataTable = myComm.Fetch()
                    For Each dr As DataRow In myData.Rows
                        AppendixL.Rows.Add()
                        AppendixL.Rows(AppendixL.Rows.Count - 1).Item(0) = GetLimitedLengthString(CStrSafe(dr.Item(0)), 20)
                        AppendixL.Rows(AppendixL.Rows.Count - 1).Item(1) = CIntSafe(dr.Item(1), 1).ToString()
                        AppendixL.Rows(AppendixL.Rows.Count - 1).Item(2) = GetLimitedLengthString(CStrSafe(dr.Item(2)), 34)
                        AppendixL.Rows(AppendixL.Rows.Count - 1).Item(3) = GetLimitedLengthString(CStrSafe(dr.Item(3)), 1)
                        AppendixL.Rows(AppendixL.Rows.Count - 1).Item(4) = GetLimitedLengthString(CStrSafe(dr.Item(4)), 2).PadLeft(2, "0"c)
                        AppendixL.Rows(AppendixL.Rows.Count - 1).Item(5) = GetLimitedLengthString(CStrSafe(dr.Item(5)), 1)
                        AppendixL.Rows(AppendixL.Rows.Count - 1).Item(6) = GetLimitedLengthString(CStrSafe(dr.Item(6)), 1)
                        AppendixL.Rows(AppendixL.Rows.Count - 1).Item(7) = DblParser(CDblSafe(dr.Item(7)))
                        AppendixL.Rows(AppendixL.Rows.Count - 1).Item(8) = DblParser(CDblSafe(dr.Item(8)))
                        AppendixL.Rows(AppendixL.Rows.Count - 1).Item(9) = DblParser(CDblSafe(dr.Item(9)))
                        AppendixL.Rows(AppendixL.Rows.Count - 1).Item(10) = DblParser(CDblSafe(dr.Item(10)))
                        G4 = CRound(G4 + CDblSafe(dr.Item(7)))
                        G5 = CRound(G5 + CDblSafe(dr.Item(9)))
                        G6 = CRound(G6 + CDblSafe(dr.Item(10)))
                    Next
                End Using

                myComm = New SQLCommand(IIf(criteria.Year >= 2019, _
                    "FetchDeclarationGPM312_2AppendixU", _
                    "FetchDeclarationGPM312_1AppendixU").ToString())
                myComm.AddParam("?YR", criteria.Year)
                myComm.AddParam("?DS", gpmCode)
                myComm.AddParam("?LT", StateCodeLith.ToUpper())
                If criteria.Year >= 2019 Then myComm.AddParam("?LG", "03")

                Using myData As DataTable = myComm.Fetch()
                    For Each dr As DataRow In myData.Rows
                        AppendixU.Rows.Add()
                        AppendixU.Rows(AppendixU.Rows.Count - 1).Item(0) = GetLimitedLengthString(CStrSafe(dr.Item(0)), 20).ToUpper()
                        AppendixU.Rows(AppendixU.Rows.Count - 1).Item(1) = CIntSafe(dr.Item(1), 1).ToString()
                        AppendixU.Rows(AppendixU.Rows.Count - 1).Item(2) = GetLimitedLengthString(CStrSafe(dr.Item(2)), 34).ToUpper()
                        AppendixU.Rows(AppendixU.Rows.Count - 1).Item(3) = GetLimitedLengthString(CStrSafe(dr.Item(3)), 1).ToUpper()
                        AppendixU.Rows(AppendixU.Rows.Count - 1).Item(4) = GetLimitedLengthString(CStrSafe(dr.Item(4)), 2).PadLeft(2, "0"c)
                        AppendixU.Rows(AppendixU.Rows.Count - 1).Item(5) = GetLimitedLengthString(CStrSafe(dr.Item(5)), 1).ToUpper()
                        AppendixU.Rows(AppendixU.Rows.Count - 1).Item(6) = GetLimitedLengthString(CStrSafe(dr.Item(6)), 1).ToUpper()
                        AppendixU.Rows(AppendixU.Rows.Count - 1).Item(7) = DblParser(CDblSafe(dr.Item(7)))
                        AppendixU.Rows(AppendixU.Rows.Count - 1).Item(8) = DblParser(CDblSafe(dr.Item(8)))
                        AppendixU.Rows(AppendixU.Rows.Count - 1).Item(9) = DblParser(CDblSafe(dr.Item(9)))
                        AppendixU.Rows(AppendixU.Rows.Count - 1).Item(10) = DblParser(CDblSafe(dr.Item(10)))
                        AppendixU.Rows(AppendixU.Rows.Count - 1).Item(11) = GetLimitedLengthString(CStrSafe(dr.Item(11)), 10)
                        AppendixU.Rows(AppendixU.Rows.Count - 1).Item(12) = GetLimitedLengthString(CStrSafe(dr.Item(12)), 24).ToUpper()
                        AppendixU.Rows(AppendixU.Rows.Count - 1).Item(13) = GetLimitedLengthString(CStrSafe(dr.Item(13)), 2).ToUpper()
                        AppendixU.Rows(AppendixU.Rows.Count - 1).Item(14) = GetLimitedLengthString(CStrSafe(dr.Item(14)), 68).ToUpper()
                        G4 = CRound(G4 + CDblSafe(dr.Item(7)))
                        G5 = CRound(G5 + CDblSafe(dr.Item(9)))
                        G6 = CRound(G6 + CDblSafe(dr.Item(10)))
                    Next
                End Using

            Catch ex As Exception
                sd.Dispose()
                AppendixL.Dispose()
                AppendixU.Dispose()
                result.Dispose()
                Throw
            End Try

            sd.Rows(0).Item(2) = G4
            sd.Rows(0).Item(3) = G5
            sd.Rows(0).Item(4) = G6

            result.Tables.Add(sd)
            result.Tables.Add(AppendixL)
            result.Tables.Add(AppendixU)

            Return result

        End Function

        ''' <summary>
        ''' Gets a ffdata format dataset.
        ''' </summary>
        ''' <param name="declarationDataSet">a declaration dataset fetched by the <see cref="GetBaseDataSet">GetBaseDataSet</see> method.</param>
        ''' <param name="preparatorName">a name of the person who prepared the declaration.</param>
        ''' <remarks></remarks>
        Public Function GetFfDataDataSet(ByVal declarationDataSet As DataSet,
            ByVal preparatorName As String) As DataSet Implements IDeclaration.GetFfDataDataSet

            If declarationDataSet Is Nothing Then
                Throw New ArgumentNullException("declarationDataSet")
            End If

            If preparatorName Is Nothing Then
                preparatorName = ""
            End If

            Dim dds As DataSet = declarationDataSet
            Dim i As Integer
            Dim currentUser As AccDataAccessLayer.Security.AccIdentity = GetCurrentIdentity()

            Dim myDoc As New Xml.XmlDocument
            myDoc.Load(IO.Path.Combine(AppPath(), FILENAMEFFDATAGPM312_1))

            myDoc.SelectNodes("//FFData")(0).Attributes(2).Value = currentUser.Name
            myDoc.SelectNodes("//FFData")(0).Attributes(3).Value = GetDateInFFDataFormat(Today)
            myDoc.SelectNodes("//Form")(0).Attributes(1).Value = IO.Path.Combine(AppPath(), FILENAMEMXFDGPM312_1)

            For Each element As System.Xml.XmlElement In myDoc.SelectNodes("//Field[@Name='B_MM_ID']")
                element.InnerText = dds.Tables("General").Rows(0).Item(1).ToString
            Next
            For Each element As System.Xml.XmlElement In myDoc.SelectNodes("//Field[@Name='B_MM_Pavad']")
                element.InnerText = GetLimitedLengthString(dds.Tables("General").Rows(0).Item(0).ToString, 116).ToUpper
            Next
            For Each element As System.Xml.XmlElement In myDoc.SelectNodes("//Field[@Name='B_ML_Metai']")
                element.InnerText = dds.Tables("Specific").Rows(0).Item(1).ToString
            Next
            For Each element As System.Xml.XmlElement In myDoc.SelectNodes("//Field[@Name='G4']")
                element.InnerText = GetNumberInFFDataFormat(CDbl(dds.Tables("Specific").Rows(0).Item(2)))
            Next
            For Each element As System.Xml.XmlElement In myDoc.SelectNodes("//Field[@Name='G5']")
                element.InnerText = GetNumberInFFDataFormat(CDbl(dds.Tables("Specific").Rows(0).Item(3)))
            Next
            For Each element As System.Xml.XmlElement In myDoc.SelectNodes("//Field[@Name='G6']")
                element.InnerText = GetNumberInFFDataFormat(CDbl(dds.Tables("Specific").Rows(0).Item(4)))
            Next

            Dim appendixLPage As Xml.XmlElement = DirectCast(myDoc.ChildNodes(1).ChildNodes(0).
                    ChildNodes(1).ChildNodes(1).Clone, Xml.XmlElement)
            Dim appendixLGroup As Xml.XmlElement = DirectCast(myDoc.ChildNodes(1).ChildNodes(0).ChildNodes(0).
                    ChildNodes(0).ChildNodes(1).Clone, Xml.XmlElement)
            Dim appendixUPage As Xml.XmlElement = DirectCast(myDoc.ChildNodes(1).ChildNodes(0).
                    ChildNodes(1).ChildNodes(2).Clone, Xml.XmlElement)
            Dim appendixUGroup As Xml.XmlElement = DirectCast(myDoc.ChildNodes(1).ChildNodes(0).ChildNodes(0).
                    ChildNodes(0).ChildNodes(2).Clone, Xml.XmlElement)

            Dim appendixLPageCount As Integer = Convert.ToInt32(Math.Ceiling(dds.Tables("AppendixL").Rows.Count / 10))
            Dim appendixUPageCount As Integer = Convert.ToInt32(Math.Ceiling(dds.Tables("AppendixU").Rows.Count / 5))
            If dds.Tables("AppendixU").Rows.Count < 1 Then appendixUPageCount = 0

            If appendixLPageCount > 1 Then
                For i = 1 To appendixLPageCount - 1
                    Dim newPage As Xml.XmlElement = DirectCast(appendixLPage.Clone(), Xml.XmlElement)
                    newPage.Attributes(1).Value = (i + 2).ToString
                    newPage.ChildNodes(0).ChildNodes(134).InnerText = (i + 1).ToString
                    myDoc.ChildNodes(1).ChildNodes(0).ChildNodes(1).InsertAfter(newPage,
                            myDoc.ChildNodes(1).ChildNodes(0).ChildNodes(1).ChildNodes(1))
                    Dim newGroup As Xml.XmlElement = DirectCast(appendixLGroup.Clone(), Xml.XmlElement)
                    myDoc.ChildNodes(1).ChildNodes(0).ChildNodes(0).ChildNodes(0).InsertAfter(newGroup,
                            myDoc.ChildNodes(1).ChildNodes(0).ChildNodes(0).ChildNodes(0).ChildNodes(1))
                Next
            End If

            myDoc.ChildNodes(1).ChildNodes(0).ChildNodes(1).ChildNodes(appendixLPageCount + 1).
                    Attributes(1).Value = (appendixLPageCount + 2).ToString

            If appendixUPageCount > 1 Then
                For i = 1 To appendixUPageCount - 1
                    Dim newPage As Xml.XmlElement = DirectCast(appendixUPage.Clone(), Xml.XmlElement)
                    newPage.Attributes(1).Value = (i + appendixLPageCount + 2).ToString
                    newPage.ChildNodes(0).ChildNodes(79).InnerText = (i + 1).ToString
                    myDoc.ChildNodes(1).ChildNodes(0).ChildNodes(1).AppendChild(newPage)
                    Dim newGroup As Xml.XmlElement = DirectCast(appendixUGroup.Clone(), Xml.XmlElement)
                    myDoc.ChildNodes(1).ChildNodes(0).ChildNodes(0).ChildNodes(0).AppendChild(newGroup)
                Next
            ElseIf appendixUPageCount < 1 Then
                myDoc.ChildNodes(1).ChildNodes(0).ChildNodes(1).RemoveChild(
                    myDoc.ChildNodes(1).ChildNodes(0).ChildNodes(1).ChildNodes(appendixLPageCount + 1))
            End If

            myDoc.ChildNodes(1).ChildNodes(0).ChildNodes(1).Attributes(0).Value =
                (1 + appendixLPageCount + appendixUPageCount).ToString

            Dim appendixesL As Xml.XmlNodeList = myDoc.SelectNodes("//Page[@PageDefName='GPM312L']")
            Dim appendixLData As DataTable = dds.Tables("AppendixL")
            Dim j, p As Integer

            For i = 1 To appendixLPageCount
                p = 10 * (i - 1)
                For j = 1 To Math.Min(10, appendixLData.Rows.Count - p)
                    appendixesL(i - 1).ChildNodes(0).ChildNodes(0 + ((j - 1) * 13)).InnerText = appendixLData.Rows(p + j - 1).Item(0).ToString
                    appendixesL(i - 1).ChildNodes(0).ChildNodes(1 + ((j - 1) * 13)).InnerText = appendixLData.Rows(p + j - 1).Item(1).ToString
                    appendixesL(i - 1).ChildNodes(0).ChildNodes(2 + ((j - 1) * 13)).InnerText = appendixLData.Rows(p + j - 1).Item(2).ToString
                    appendixesL(i - 1).ChildNodes(0).ChildNodes(3 + ((j - 1) * 13)).InnerText = appendixLData.Rows(p + j - 1).Item(3).ToString
                    appendixesL(i - 1).ChildNodes(0).ChildNodes(4 + ((j - 1) * 13)).InnerText = appendixLData.Rows(p + j - 1).Item(4).ToString
                    appendixesL(i - 1).ChildNodes(0).ChildNodes(5 + ((j - 1) * 13)).InnerText = appendixLData.Rows(p + j - 1).Item(5).ToString
                    appendixesL(i - 1).ChildNodes(0).ChildNodes(6 + ((j - 1) * 13)).InnerText = appendixLData.Rows(p + j - 1).Item(6).ToString
                    appendixesL(i - 1).ChildNodes(0).ChildNodes(7 + ((j - 1) * 13)).InnerText = GetNumberInFFDataFormat(CDbl(appendixLData.Rows(p + j - 1).Item(7)))
                    appendixesL(i - 1).ChildNodes(0).ChildNodes(8 + ((j - 1) * 13)).InnerText = GetNumberInFFDataFormat(CDbl(appendixLData.Rows(p + j - 1).Item(8)))
                    appendixesL(i - 1).ChildNodes(0).ChildNodes(9 + ((j - 1) * 13)).InnerText = GetNumberInFFDataFormat(CDbl(appendixLData.Rows(p + j - 1).Item(9)))
                    appendixesL(i - 1).ChildNodes(0).ChildNodes(10 + ((j - 1) * 13)).InnerText = GetNumberInFFDataFormat(CDbl(appendixLData.Rows(p + j - 1).Item(10)))
                Next
            Next

            If appendixUPageCount > 0 Then

                Dim appendixesU As Xml.XmlNodeList = myDoc.SelectNodes("//Page[@PageDefName='GPM312U']")
                Dim appendixUData As DataTable = dds.Tables("AppendixU")

                For i = 1 To appendixLPageCount
                    p = 5 * (i - 1)
                    For j = 1 To Math.Min(5, appendixUData.Rows.Count - p)
                        appendixesU(i - 1).ChildNodes(0).ChildNodes(0 + ((j - 1) * 15)).InnerText = appendixUData.Rows(p + j - 1).Item(0).ToString
                        appendixesU(i - 1).ChildNodes(0).ChildNodes(1 + ((j - 1) * 15)).InnerText = appendixUData.Rows(p + j - 1).Item(1).ToString
                        appendixesU(i - 1).ChildNodes(0).ChildNodes(2 + ((j - 1) * 15)).InnerText = appendixUData.Rows(p + j - 1).Item(2).ToString
                        appendixesU(i - 1).ChildNodes(0).ChildNodes(3 + ((j - 1) * 15)).InnerText = appendixUData.Rows(p + j - 1).Item(3).ToString
                        appendixesU(i - 1).ChildNodes(0).ChildNodes(4 + ((j - 1) * 15)).InnerText = appendixUData.Rows(p + j - 1).Item(4).ToString
                        appendixesU(i - 1).ChildNodes(0).ChildNodes(5 + ((j - 1) * 15)).InnerText = appendixUData.Rows(p + j - 1).Item(5).ToString
                        appendixesU(i - 1).ChildNodes(0).ChildNodes(6 + ((j - 1) * 15)).InnerText = appendixUData.Rows(p + j - 1).Item(6).ToString
                        appendixesU(i - 1).ChildNodes(0).ChildNodes(7 + ((j - 1) * 15)).InnerText = GetNumberInFFDataFormat(CDbl(appendixUData.Rows(p + j - 1).Item(7)))
                        appendixesU(i - 1).ChildNodes(0).ChildNodes(8 + ((j - 1) * 15)).InnerText = GetNumberInFFDataFormat(CDbl(appendixUData.Rows(p + j - 1).Item(8)))
                        appendixesU(i - 1).ChildNodes(0).ChildNodes(9 + ((j - 1) * 15)).InnerText = GetNumberInFFDataFormat(CDbl(appendixUData.Rows(p + j - 1).Item(9)))
                        appendixesU(i - 1).ChildNodes(0).ChildNodes(10 + ((j - 1) * 15)).InnerText = GetNumberInFFDataFormat(CDbl(appendixUData.Rows(p + j - 1).Item(10)))
                        appendixesU(i - 1).ChildNodes(0).ChildNodes(11 + ((j - 1) * 15)).InnerText = appendixUData.Rows(p + j - 1).Item(11).ToString
                        appendixesU(i - 1).ChildNodes(0).ChildNodes(12 + ((j - 1) * 15)).InnerText = appendixUData.Rows(p + j - 1).Item(12).ToString
                        appendixesU(i - 1).ChildNodes(0).ChildNodes(13 + ((j - 1) * 15)).InnerText = appendixUData.Rows(p + j - 1).Item(13).ToString
                        appendixesU(i - 1).ChildNodes(0).ChildNodes(14 + ((j - 1) * 15)).InnerText = appendixUData.Rows(p + j - 1).Item(14).ToString
                    Next
                Next

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

            Dim currentError As String = ""

            If Not criteria.TryValidateYear(False, currentError) Then Return currentError

            Return ""

        End Function

        Public Function GetAllWarnings(ByVal criteria As DeclarationCriteria) As String _
            Implements IDeclaration.GetAllWarnings
            Return ""
        End Function

        Public Function HasWarnings(ByVal criteria As DeclarationCriteria) As Boolean _
            Implements IDeclaration.HasWarnings
            Return False
        End Function

        Public Function IsValid(ByVal criteria As DeclarationCriteria) As Boolean _
            Implements IDeclaration.IsValid

            Dim currentError As String = ""
            Dim currentIsWarning As Boolean = False

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


