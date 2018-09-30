Namespace ActiveReports.Declarations

    ''' <summary>
    ''' Represents an implementation of a <see cref="Declaration">Declaration</see>
    ''' for a state tax inspectorate (VMI) report No. GPM313 version 1.
    ''' </summary>
    ''' <remarks>Object is responsible for fetching the report data to a dataset
    ''' and transforming the dataset to ffdata format (required by the FormFiller application).</remarks>
    <Serializable()>
    Public Class DeclarationGPM313_1
        Implements IDeclaration

        Private Const DECLARATION_NAME As String = "GPM313 v.1"
        Private Const FILENAMEMXFD As String = "MXFD\GPM313.mxfd"
        Private Const FILENAMEFFDATA As String = "FFData\GPM313.ffdata"
        Private Shared ReadOnly DECLARATION_VALID_FROM As New Date(2018, 1, 1)


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
                Return DECLARATION_VALID_FROM
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
                Return "R_Declaration_GPM313_1.rdlc"
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
            sd.Columns.Add("LabourIncome")
            sd.Columns.Add("LabourIncomeTaxBefore15")
            sd.Columns.Add("LabourIncomeTaxAfter15")
            sd.Columns.Add("OtherIncome")
            sd.Columns.Add("OtherIncomeTaxBefore15")
            sd.Columns.Add("OtherIncomeTaxAfter15")
            sd.Columns.Add("ClassBIncome")
            sd.Columns.Add("ClassBTaxPayed")

            sd.Rows.Add()

            If criteria.Year < 2017 Then
                sd.Rows(0).Item(0) = criteria.MunicipalityCode
            Else
                sd.Rows(0).Item(0) = ""
            End If
            sd.Rows(0).Item(0) = criteria.Date.ToShortDateString
            sd.Rows(0).Item(1) = criteria.Year
            sd.Rows(0).Item(2) = criteria.Month

            Try

                Dim myComm As New SQLCommand("FetchDeclarationGPM313_1")
                myComm.AddParam("?MN", criteria.Month)
                myComm.AddParam("?YR", criteria.Year)

                Using myData As DataTable = myComm.Fetch

                    If myData.Rows.Count < 1 Then myData.Rows.Add()

                    Dim dr As DataRow = myData.Rows(0)

                    ' wage
                    sd.Rows(0).Item(3) = CRound(CDblSafe(dr.Item(0), 2, 0) +
                        CDblSafe(dr.Item(1), 2, 0)) ' wage + imprest
                    sd.Rows(0).Item(4) = CDblSafe(dr.Item(2), 2, 0) ' GPM before 15
                    sd.Rows(0).Item(5) = CDblSafe(dr.Item(3), 2, 0) ' GPM after 15

                    ' other
                    sd.Rows(0).Item(6) = CDblSafe(dr.Item(4), 2, 0)
                    sd.Rows(0).Item(7) = CDblSafe(dr.Item(5), 2, 0) ' GPM before 15
                    sd.Rows(0).Item(8) = CDblSafe(dr.Item(6), 2, 0) ' GPM after 15

                    ' class B income
                    sd.Rows(0).Item(9) = CDblSafe(dr.Item(7), 2, 0)
                    sd.Rows(0).Item(10) = CDblSafe(dr.Item(8), 2, 0)

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
        Public Function GetFfDataDataSet(ByVal declarationDataSet As DataSet,
            ByVal preparatorName As String) As DataSet _
            Implements IDeclaration.GetFfDataDataSet

            If declarationDataSet Is Nothing Then
                Throw New ArgumentNullException("declarationDataSet")
            End If

            If preparatorName Is Nothing Then
                preparatorName = ""
            End If

            ' read ffdata xml structure to dataset
            Dim formDataSet As New DataSet
            Try
                Using formFileStream As IO.FileStream = New IO.FileStream(
                    IO.Path.Combine(AppPath(), FILENAMEFFDATA), IO.FileMode.Open)
                    formDataSet.ReadXml(formFileStream)
                    formFileStream.Close()
                End Using
            Catch ex As Exception
                formDataSet.Dispose()
                Throw New Exception("Failed to prepare ffdata file.", ex)
            End Try

            formDataSet.Tables(0).Rows(0).Item(3) = GetCurrentIdentity.Name
            formDataSet.Tables(0).Rows(0).Item(4) = GetDateInFFDataFormat(Today)
            formDataSet.Tables(1).Rows(0).Item(2) = IO.Path.Combine(AppPath(), FILENAMEMXFD)

            Dim dds As DataSet = declarationDataSet
            Dim specificDataRow As DataRow = dds.Tables("Specific").Rows(0)

            For i As Integer = 1 To formDataSet.Tables(8).Rows.Count
                If formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "B_MM_ID" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = dds.Tables("General").Rows(0).Item(1)
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "B_MM_Pavadinimas" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetLimitedLengthString(dds.Tables("General").Rows(0).Item(0).ToString, 45).ToUpper
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "B_ML_Metai" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = specificDataRow.Item(1).ToString
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "B_ML_Menuo" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = specificDataRow.Item(2)
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "G5" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat(
                        CDbl(specificDataRow.Item(3)), True)
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "G6" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat(
                        CDbl(specificDataRow.Item(4)), True)
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "G7" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat(
                        CDbl(specificDataRow.Item(5)), True)
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "G8" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat(
                        CDbl(specificDataRow.Item(6)), True)
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "G9" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat(
                        CDbl(specificDataRow.Item(7)), True)
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "G10" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat(
                        CDbl(specificDataRow.Item(8)), True)
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "G11" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat(
                        CDbl(specificDataRow.Item(9)), True)
                ElseIf formDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "G12" Then
                    formDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat(
                        CDbl(specificDataRow.Item(10)), True)
                End If
            Next

            Return formDataSet

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