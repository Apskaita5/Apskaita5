Namespace ActiveReports.Declarations

    ''' <summary>
    ''' Represents an implementation of a <see cref="Declaration">Declaration</see>
    ''' for a state tax inspectorate (VMI) report No. FR0572 version 2.
    ''' </summary>
    ''' <remarks>Object is responsible for fetching the report data to a dataset
    ''' and transforming the dataset to ffdata format (required by the FormFiller application).</remarks>
    <Serializable()> _
    Public Class DeclarationFR0600_2
        Implements IDeclaration

        Private Const DECLARATION_NAME As String = "FR0600 v.2"
        Private Const FILENAMEMXFDFR0600_2 As String = "MXFD\FR0600(2).mxfd"
        Private Const FILENAMEFFDATAFR0600_2 As String = "FFData\FR0600(2).ffdata"


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
                Return "R_Declaration_FR0600_2.rdlc"
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
            sd.Columns.Add("B_MM_Pavad", GetType(String))
            sd.Columns.Add("B_MM_ID", GetType(String))
            sd.Columns.Add("B_MM_PVM", GetType(String))
            sd.Columns.Add("B_MM_Adresas", GetType(String))
            sd.Columns.Add("B_MM_Epastas", GetType(String))
            sd.Columns.Add("B_UzpildData", GetType(String))
            sd.Columns.Add("B_ML_DataNuo", GetType(String))
            sd.Columns.Add("B_ML_DataIki", GetType(String))
            sd.Columns.Add("E11", GetType(Double))
            sd.Columns.Add("E12", GetType(Double))
            sd.Columns.Add("E13", GetType(Double))
            sd.Columns.Add("E14", GetType(Double))
            sd.Columns.Add("E15", GetType(Double))
            sd.Columns.Add("E16", GetType(Integer))
            sd.Columns.Add("E17", GetType(Double))
            sd.Columns.Add("E18", GetType(Double))
            sd.Columns.Add("E19", GetType(Double))
            sd.Columns.Add("E20", GetType(Double))
            sd.Columns.Add("E21", GetType(Double))
            sd.Columns.Add("E22", GetType(Double))
            sd.Columns.Add("E23", GetType(Double))
            sd.Columns.Add("E24", GetType(Double))
            sd.Columns.Add("E25", GetType(Double))
            sd.Columns.Add("E26", GetType(Double))
            sd.Columns.Add("E27", GetType(Integer))
            sd.Columns.Add("E28", GetType(Double))
            sd.Columns.Add("E29", GetType(Double))
            sd.Columns.Add("E30", GetType(Double))
            sd.Columns.Add("E31", GetType(Double))
            sd.Columns.Add("E32", GetType(Double))
            sd.Columns.Add("E33", GetType(Double))
            sd.Columns.Add("E34", GetType(Double))
            sd.Columns.Add("E35", GetType(Double))
            sd.Columns.Add("E36", GetType(Double))
            sd.Rows.Add()

            sd.Rows(0).Item("B_MM_Pavad") = GetLimitedLengthString(GetCurrentCompany.Name, 30).ToUpper
            sd.Rows(0).Item("B_MM_ID") = GetCurrentCompany.Code
            If Not String.IsNullOrEmpty(GetCurrentCompany.CodeVat.Trim) AndAlso _
                GetCurrentCompany.CodeVat.Trim.Length > 2 Then
                sd.Rows(0).Item("B_MM_PVM") = GetCurrentCompany.CodeVat.Trim.Substring(2)
            Else
                sd.Rows(0).Item("B_MM_PVM") = ""
            End If
            sd.Rows(0).Item("B_MM_Adresas") = GetLimitedLengthString(GetCurrentCompany.Address, 30).ToUpper
            sd.Rows(0).Item("B_MM_Epastas") = GetLimitedLengthString(GetCurrentCompany.Email, 30).ToUpper
            sd.Rows(0).Item("B_UzpildData") = GetDateInFFDataFormat(criteria.Date.Date)
            sd.Rows(0).Item("B_ML_DataNuo") = GetDateInFFDataFormat(New Date(criteria.Year, criteria.Month, 1))
            sd.Rows(0).Item("B_ML_DataIki") = GetDateInFFDataFormat(New Date(criteria.Year, criteria.Month, _
                Date.DaysInMonth(criteria.Year, criteria.Month)))

            For i As Integer = 11 To 36
                sd.Rows(0).Item("E" & i.ToString) = 0.0
            Next

            Try

                Dim myComm As New SQLCommand("FetchDeclarationFR0600_2")
                myComm.AddParam("?DF", New Date(criteria.Year, criteria.Month, 1))
                myComm.AddParam("?DT", New Date(criteria.Year, criteria.Month, _
                    Date.DaysInMonth(criteria.Year, criteria.Month)))

                Using myData As DataTable = myComm.Fetch

                    If myData.Rows.Count < 1 Then
                        Throw New Exception("Klaida. Negauti duomenys apie išrašytas ir gautas sąskaitas.")
                    End If

                    Dim curColumnName As String
                    For i As Integer = 11 To 36
                        curColumnName = "E" & i.ToString
                        For Each dr As DataRow In myData.Rows
                            If CStrSafe(dr.Item(0)).Trim.ToUpper = curColumnName Then
                                sd.Rows(0).Item(curColumnName) = CRound(sd.Rows(0).Item(curColumnName) + _
                                    CDblSafe(dr.Item(1), 2, 0), 2)
                            End If
                        Next
                    Next

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

            ' read ffdata xml structure to dataset
            Dim formDataSet As DataSet
            Using formFileStream As IO.FileStream = New IO.FileStream( _
                IO.Path.Combine(AppPath(), FILENAMEFFDATAFR0600_2), IO.FileMode.Open)
                formDataSet = New DataSet
                formDataSet.ReadXml(formFileStream)
                formFileStream.Close()
            End Using

            formDataSet.Tables(0).Rows(0).Item(3) = GetCurrentIdentity.Name
            formDataSet.Tables(0).Rows(0).Item(4) = GetDateInFFDataFormat(Today)
            formDataSet.Tables(1).Rows(0).Item(2) = IO.Path.Combine(AppPath(), FILENAMEMXFDFR0600_2)

            Dim dr As DataRow = declarationDataSet.Tables("Specific").Rows(0)
            For i As Integer = 1 To formDataSet.Tables(8).Rows.Count
                For Each col As DataColumn In declarationDataSet.Tables("Specific").Columns
                    If formDataSet.Tables(8).Rows(i - 1).Item(0).ToString.Trim.ToUpper = col.ColumnName.Trim.ToUpper Then
                        If col.DataType Is GetType(Double) Then
                            formDataSet.Tables(8).Rows(i - 1).Item(1) = CInt(dr.Item(col))
                        Else
                            formDataSet.Tables(8).Rows(i - 1).Item(1) = dr.Item(col)
                        End If
                        Exit For
                    End If
                Next
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