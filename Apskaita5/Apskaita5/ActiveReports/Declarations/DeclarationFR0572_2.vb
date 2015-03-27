Namespace ActiveReports.Declarations

    <Serializable()> _
    Public Class DeclarationFR0572_2
        Implements IDeclaration

        Private Const DeclarationName As String = "FR0572 v.2"

        Private _Warnings As String = ""
        Private _Date As Date
        Private _Year As Integer
        Private _Month As Integer
        Private _MunicipalityCode As String


        Public ReadOnly Property [Date]() As Date
            Get
                Return _Date
            End Get
        End Property

        Public ReadOnly Property Year() As Integer
            Get
                Return _Year
            End Get
        End Property
        
        Public ReadOnly Property Month() As Integer
            Get
                Return _Month
            End Get
        End Property
        
        Public ReadOnly Property MunicipalityCode() As String
            Get
                Return _MunicipalityCode
            End Get
        End Property


        Public ReadOnly Property Name() As String Implements IDeclaration.Name
            Get
                Return DeclarationName
            End Get
        End Property

        Public ReadOnly Property RequiredDateInterval() As Boolean Implements IDeclaration.RequiredDateInterval
            Get
                Return False
            End Get
        End Property

        Public ReadOnly Property RequiredDeclarationItemCode() As Boolean Implements IDeclaration.RequiredDeclarationItemCode
            Get
                Return False
            End Get
        End Property

        Public ReadOnly Property RequiredMonth() As Boolean Implements IDeclaration.RequiredMonth
            Get
                Return True
            End Get
        End Property

        Public ReadOnly Property RequiredMunicipalityCode() As Boolean Implements IDeclaration.RequiredMunicipalityCode
            Get
                Return True
            End Get
        End Property

        Public ReadOnly Property RequiredQuarter() As Boolean Implements IDeclaration.RequiredQuarter
            Get
                Return False
            End Get
        End Property

        Public ReadOnly Property RequiredSodraAccount() As Boolean Implements IDeclaration.RequiredSodraAccount
            Get
                Return False
            End Get
        End Property

        Public ReadOnly Property RequiredSodraAccount2() As Boolean Implements IDeclaration.RequiredSodraAccount2
            Get
                Return False
            End Get
        End Property

        Public ReadOnly Property RequiredSodraDepartment() As Boolean Implements IDeclaration.RequiredSodraDepartment
            Get
                Return False
            End Get
        End Property

        Public ReadOnly Property RequiredSodraRate() As Boolean Implements IDeclaration.RequiredSodraRate
            Get
                Return False
            End Get
        End Property

        Public ReadOnly Property RequiredYear() As Boolean Implements IDeclaration.RequiredYear
            Get
                Return True
            End Get
        End Property

        Public ReadOnly Property ValidFrom() As Date Implements IDeclaration.ValidFrom
            Get
                Return Date.MinValue
            End Get
        End Property

        Public ReadOnly Property ValidTo() As Date Implements IDeclaration.ValidTo
            Get
                Return New Date(2009, 12, 31)
            End Get
        End Property

        Public ReadOnly Property Warnings() As String Implements IDeclaration.Warnings
            Get
                Return _Warnings
            End Get
        End Property



        Public Function GetBaseDataSet() As DataSet _
            Implements IDeclaration.GetBaseDataSet

            Dim errors As String = ""
            If Not ValidateParams(errors, _Warnings) Then
                Throw New Exception(String.Format(My.Resources.ActiveReports_IDeclaration_ArgumentsNull, _
                    vbCrLf, errors))
            End If

            Dim result As New DataSet
            result.Tables.Add(Declaration.FetchGeneralDataTable)

            Dim SD As New DataTable("Specific")
            SD.Columns.Add("MunicipalityCode")
            SD.Columns.Add("Date")
            SD.Columns.Add("Year")
            SD.Columns.Add("Month")
            SD.Columns.Add("WorkerCount")
            SD.Columns.Add("LabourIncome")
            SD.Columns.Add("LabourIncomeTaxBefore15")
            SD.Columns.Add("LabourIncomeTaxAfter15")
            SD.Columns.Add("OtherIncome")
            SD.Columns.Add("OtherIncomeTaxBefore15")
            SD.Columns.Add("OtherIncomeTaxAfter15")

            SD.Rows.Add()

            SD.Rows(0).Item(0) = _MunicipalityCode
            SD.Rows(0).Item(1) = _Date.ToShortDateString
            SD.Rows(0).Item(2) = _Year
            SD.Rows(0).Item(3) = _Month

            Try
                SD.Rows(0).Item(4) = Declaration.GetEmployeesCount(New Date(_Year, _
                    _Month, Date.DaysInMonth(_Year, _Month)))

                Dim myComm As New SQLCommand("FetchDeclarationFR0572(2)")
                myComm.AddParam("?YR", _Year)
                myComm.AddParam("?MN", _Month)

                Using myData As DataTable = myComm.Fetch

                    If myData.Rows.Count < 1 Then myData.Rows.Add()
                    Declaration.ClearDatatable(myData, 0)

                    SD.Rows(0).Item(5) = DblParser(CDbl(myData.Rows(0).Item(0)))
                    SD.Rows(0).Item(6) = DblParser(CDbl(myData.Rows(0).Item(1)))
                    SD.Rows(0).Item(7) = DblParser(CDbl(myData.Rows(0).Item(2)))
                    SD.Rows(0).Item(8) = DblParser(CDbl(myData.Rows(0).Item(3)))
                    SD.Rows(0).Item(9) = DblParser(CDbl(myData.Rows(0).Item(4)))
                    SD.Rows(0).Item(10) = DblParser(CDbl(myData.Rows(0).Item(5)))

                End Using

                result.Tables.Add(SD)

            Catch ex As Exception
                SD.Dispose()
                result.Dispose()
                Throw ex
            End Try

            Return result

        End Function

        Public Function GetFFDataDataSet(ByVal declarationDataSet As DataSet, _
            ByVal preparatorName As String) As DataSet _
            Implements IDeclaration.GetFFDataDataSet

            If declarationDataSet Is Nothing Then
                Throw New ArgumentNullException("declarationDataSet")
            End If

            If preparatorName Is Nothing Then
                preparatorName = ""
            End If

            Dim DDS As DataSet = declarationDataSet
            Dim CurrentUser As AccDataAccessLayer.Security.AccIdentity = GetCurrentIdentity()

            ' read ffdata xml structure to dataset
            Dim FormDataSet As New DataSet
            Using FormFileStream As IO.FileStream = New IO.FileStream( _
                AppPath() & FILENAMEFFDATAFR0572_2, IO.FileMode.Open)
                FormDataSet.ReadXml(FormFileStream)
                FormFileStream.Close()
            End Using

            FormDataSet.Tables(0).Rows(0).Item(2) = CurrentUser.Name
            FormDataSet.Tables(0).Rows(0).Item(3) = GetDateInFFDataFormat(Today)
            FormDataSet.Tables(1).Rows(0).Item(2) = AppPath() & FILENAMEMXFDFR0572_2

            Dim SpecificDataRow As DataRow = DDS.Tables("Specific").Rows(0)
            For i As Integer = 1 To FormDataSet.Tables(8).Rows.Count
                If FormDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "B_MM_ID" Then
                    FormDataSet.Tables(8).Rows(i - 1).Item(1) = DDS.Tables("General").Rows(0).Item(1)
                ElseIf FormDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "B_MM_Pavad" Then
                    FormDataSet.Tables(8).Rows(i - 1).Item(1) = GetLimitedLengthString( _
                        DDS.Tables("General").Rows(0).Item(0).ToString, 45).ToUpper
                ElseIf FormDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "B_MM_Adresas" Then
                    FormDataSet.Tables(8).Rows(i - 1).Item(1) = GetLimitedLengthString( _
                        DDS.Tables("General").Rows(0).Item(2).ToString, 45).ToUpper
                ElseIf FormDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "B_MM_Tel" Then
                    FormDataSet.Tables(8).Rows(i - 1).Item(1) = GetLimitedLengthString( _
                        DDS.Tables("General").Rows(0).Item(8).ToString, 12)
                ElseIf FormDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "B_MM_Epastas" Then
                    FormDataSet.Tables(8).Rows(i - 1).Item(1) = GetLimitedLengthString( _
                        DDS.Tables("General").Rows(0).Item(7).ToString, 35).ToUpper
                ElseIf FormDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "B_UzpildData" Then
                    FormDataSet.Tables(8).Rows(i - 1).Item(1) = GetDateInFFDataFormat( _
                        CDate(SpecificDataRow.Item(1)))
                ElseIf FormDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "B_ML_Metai" Then
                    FormDataSet.Tables(8).Rows(i - 1).Item(1) = SpecificDataRow.Item(2).ToString
                ElseIf FormDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "B_ML_Menuo" Then
                    FormDataSet.Tables(8).Rows(i - 1).Item(1) = SpecificDataRow.Item(3).ToString
                ElseIf FormDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "B_MM_SavKodas" Then
                    FormDataSet.Tables(8).Rows(i - 1).Item(1) = SpecificDataRow.Item(0).ToString
                ElseIf FormDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "E11" Then
                    FormDataSet.Tables(8).Rows(i - 1).Item(1) = SpecificDataRow.Item(4).ToString
                ElseIf FormDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "E18" Then
                    FormDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat( _
                        Convert.ToDouble(SpecificDataRow.Item(5)))
                ElseIf FormDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "E19" Then
                    FormDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat( _
                        Convert.ToDouble(SpecificDataRow.Item(6)))
                ElseIf FormDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "E20" Then
                    FormDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat( _
                        Convert.ToDouble(SpecificDataRow.Item(7)))
                ElseIf FormDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "E21" Then
                    FormDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat( _
                        Convert.ToDouble(SpecificDataRow.Item(8)))
                ElseIf FormDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "E22" Then
                    FormDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat( _
                        Convert.ToDouble(SpecificDataRow.Item(9)))
                ElseIf FormDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "E23" Then
                    FormDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat( _
                        Convert.ToDouble(SpecificDataRow.Item(10)))
                End If
            Next

            Return FormDataSet

        End Function

        Public Sub SetParams(ByVal nDate As Date, ByVal nDateFrom As Date, ByVal nDateTo As Date, _
            ByVal nSODRADepartment As String, ByVal nSODRARate As Double, ByVal nYear As Integer, _
            ByVal nQuarter As Integer, ByVal nMonth As Integer, ByVal nSODRAAccount As Long, _
            ByVal nSODRAAccount2 As Long, ByVal nMunicipalityCode As String, _
            ByVal nDeclarationItemCode As String) _
            Implements IDeclaration.SetParams

            _Date = nDate.Date
            _Year = nYear
            _Month = nMonth
            _MunicipalityCode = nMunicipalityCode

        End Sub

        Public Function ValidateParams(ByRef Errors As String, ByRef Warnings As String) As Boolean _
            Implements IDeclaration.ValidateParams

            Errors = ""
            Warnings = ""

            If StringIsNullOrEmpty(_MunicipalityCode) Then
                Errors = AddWithNewLine(Errors, My.Resources.ActiveReports_IDeclaration_MunicipalityCodeNull, False)
            End If
            If Not _Year > 0 Then
                Errors = AddWithNewLine(Errors, My.Resources.ActiveReports_IDeclaration_YearNull, False)
            End If
            If Not _Month > 0 Then
                Errors = AddWithNewLine(Errors, My.Resources.ActiveReports_IDeclaration_MonthNull, False)
            End If

            Return StringIsNullOrEmpty(Errors)

        End Function

    End Class

End Namespace