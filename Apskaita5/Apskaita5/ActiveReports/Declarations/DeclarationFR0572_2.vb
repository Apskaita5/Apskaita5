Namespace ActiveReports.Declarations

    ''' <summary>
    ''' Represents an implementation of a <see cref="Declaration">Declaration</see>
    ''' for a state tax inspectorate (VMI) report No. FR0572 version 2.
    ''' </summary>
    ''' <remarks>Object is responsible for fetching the report data to a dataset
    ''' and transforming the dataset to ffdata format (required by the FormFiller application).</remarks>
    <Serializable()> _
    Public Class DeclarationFR0572_2
        Implements IDeclaration

        Private Const DECLARATION_NAME As String = "FR0572 v.2"
        Private Const FILENAMEMXFDFR0572_2 As String = "\MXFD\FR0572(2).mxfd"
        Private Const FILENAMEFFDATAFR0572_2 As String = "\FFData\FR0572.ffdata"


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
                Return 0
            End Get
        End Property

        ''' <summary>
        ''' Gets a name of the rdlc file that should be used to print the declaration.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property RdlcFileName() As String _
            Implements IDeclaration.RdlcFileName
            Get
                Return "R_Declaration_FR0572_2.rdlc"
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
            sd.Columns.Add("LabourIncome")
            sd.Columns.Add("LabourIncomeTaxBefore15")
            sd.Columns.Add("LabourIncomeTaxAfter15")
            sd.Columns.Add("OtherIncome")
            sd.Columns.Add("OtherIncomeTaxBefore15")
            sd.Columns.Add("OtherIncomeTaxAfter15")

            sd.Rows.Add()

            sd.Rows(0).Item(0) = criteria.MunicipalityCode
            sd.Rows(0).Item(1) = criteria.Date.ToString("yyyy-MM-dd")
            sd.Rows(0).Item(2) = criteria.Year
            sd.Rows(0).Item(3) = criteria.Month

            Try
                sd.Rows(0).Item(4) = Declaration.GetEmployeesCount(New Date(criteria.Year, _
                    criteria.Month, Date.DaysInMonth(criteria.Year, criteria.Month)))

                Dim myComm As New SQLCommand("FetchDeclarationFR0572(2)")
                myComm.AddParam("?YR", criteria.Year)
                myComm.AddParam("?MN", criteria.Month)

                Using myData As DataTable = myComm.Fetch

                    If myData.Rows.Count < 1 Then myData.Rows.Add()
                    Declaration.ClearDatatable(myData, 0)

                    sd.Rows(0).Item(5) = DblParser(CDbl(myData.Rows(0).Item(0)))
                    sd.Rows(0).Item(6) = DblParser(CDbl(myData.Rows(0).Item(1)))
                    sd.Rows(0).Item(7) = DblParser(CDbl(myData.Rows(0).Item(2)))
                    sd.Rows(0).Item(8) = DblParser(CDbl(myData.Rows(0).Item(3)))
                    sd.Rows(0).Item(9) = DblParser(CDbl(myData.Rows(0).Item(4)))
                    sd.Rows(0).Item(10) = DblParser(CDbl(myData.Rows(0).Item(5)))

                End Using

                result.Tables.Add(sd)

            Catch ex As Exception
                sd.Dispose()
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
            Dim currentUser As AccDataAccessLayer.Security.AccIdentity = GetCurrentIdentity()

            ' read ffdata xml structure to dataset
            Dim formDataSet As New DataSet
            Using formFileStream As IO.FileStream = New IO.FileStream( _
                AppPath() & FILENAMEFFDATAFR0572_2, IO.FileMode.Open)
                formDataSet.ReadXml(formFileStream)
                formFileStream.Close()
            End Using

            formDataSet.Tables(0).Rows(0).Item(2) = currentUser.Name
            formDataSet.Tables(0).Rows(0).Item(3) = GetDateInFFDataFormat(Today)
            formDataSet.Tables(1).Rows(0).Item(2) = AppPath() & FILENAMEMXFDFR0572_2

            Dim specificDataRow As DataRow = dds.Tables("Specific").Rows(0)
            For i As Integer = 1 To formDataSet.Tables(8).Rows.Count
                If formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "B_MM_ID" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = dds.Tables("General").Rows(0).Item(1)
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "B_MM_Pavad" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetLimitedLengthString( _
                        dds.Tables("General").Rows(0).Item(0).ToString, 45).ToUpper
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "B_MM_Adresas" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetLimitedLengthString( _
                        dds.Tables("General").Rows(0).Item(2).ToString, 45).ToUpper
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "B_MM_Tel" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetLimitedLengthString( _
                        dds.Tables("General").Rows(0).Item(8).ToString, 12)
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "B_MM_Epastas" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetLimitedLengthString( _
                        dds.Tables("General").Rows(0).Item(7).ToString, 35).ToUpper
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "B_UzpildData" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetDateInFFDataFormat( _
                        CDate(specificDataRow.Item(1)))
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "B_ML_Metai" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = specificDataRow.Item(2).ToString
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "B_ML_Menuo" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = specificDataRow.Item(3).ToString
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "B_MM_SavKodas" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = specificDataRow.Item(0).ToString
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "E11" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = specificDataRow.Item(4).ToString
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "E18" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat( _
                        Convert.ToDouble(specificDataRow.Item(5)))
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "E19" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat( _
                        Convert.ToDouble(specificDataRow.Item(6)))
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "E20" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat( _
                        Convert.ToDouble(specificDataRow.Item(7)))
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "E21" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat( _
                        Convert.ToDouble(specificDataRow.Item(8)))
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "E22" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat( _
                        Convert.ToDouble(specificDataRow.Item(9)))
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "E23" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat( _
                        Convert.ToDouble(specificDataRow.Item(10)))
                End If
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