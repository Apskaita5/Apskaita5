Namespace ActiveReports.Declarations

    <Serializable()> _
    Public Class DeclarationSam1
        Implements IDeclaration

        Private Const DeclarationName As String = "SAM v.1"

        Private _Warnings As String = ""
        Private _Date As Date = Today
        Private _SODRADepartment As String = ""
        Private _Year As Integer = Today.Year
        Private _Quarter As Integer = 1
        Private _SODRAAccount As Long = 0
        Private _SODRAAccount2 As Long = 0


        Public ReadOnly Property [Date]() As Date
            Get
                Return _Date
            End Get
        End Property

        Public ReadOnly Property SODRADepartment() As String
            Get
                Return _SODRADepartment
            End Get
        End Property

        Public ReadOnly Property Year() As Integer
            Get
                Return _Year
            End Get
        End Property

        Public ReadOnly Property Quarter() As Integer
            Get
                Return _Quarter
            End Get
        End Property

        Public ReadOnly Property SODRAAccount() As Long
            Get
                Return _SODRAAccount
            End Get
        End Property

        Public ReadOnly Property SODRAAccount2() As Long
            Get
                Return _SODRAAccount2
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
                Return False
            End Get
        End Property

        Public ReadOnly Property RequiredMunicipalityCode() As Boolean Implements IDeclaration.RequiredMunicipalityCode
            Get
                Return False
            End Get
        End Property

        Public ReadOnly Property RequiredQuarter() As Boolean Implements IDeclaration.RequiredQuarter
            Get
                Return True
            End Get
        End Property

        Public ReadOnly Property RequiredSodraAccount() As Boolean Implements IDeclaration.RequiredSodraAccount
            Get
                Return True
            End Get
        End Property

        Public ReadOnly Property RequiredSodraAccount2() As Boolean Implements IDeclaration.RequiredSodraAccount2
            Get
                Return True
            End Get
        End Property

        Public ReadOnly Property RequiredSodraDepartment() As Boolean Implements IDeclaration.RequiredSodraDepartment
            Get
                Return True
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


        Public Sub SetParams(ByVal nDate As Date, ByVal nDateFrom As Date, ByVal nDateTo As Date, _
            ByVal nSODRADepartment As String, ByVal nSODRARate As Double, ByVal nYear As Integer, _
            ByVal nQuarter As Integer, ByVal nMonth As Integer, ByVal nSODRAAccount As Long, _
            ByVal nSODRAAccount2 As Long, ByVal nMunicipalityCode As String, _
            ByVal nDeclarationItemCode As String) _
            Implements IDeclaration.SetParams

            _Date = nDate.Date
            _SODRADepartment = nSODRADepartment
            _Year = nYear
            _Quarter = nQuarter
            _SODRAAccount = nSODRAAccount
            _SODRAAccount2 = nSODRAAccount2

        End Sub


        Public Function ValidateParams(ByRef Errors As String, ByRef Warnings As String) As Boolean _
            Implements IDeclaration.ValidateParams

            Errors = ""
            Warnings = ""

            If StringIsNullOrEmpty(_SODRADepartment) Then
                Errors = AddWithNewLine(Errors, My.Resources.ActiveReports_IDeclaration_SodraDepartmentNull, False)
            End If
            If Not _Year > 0 Then
                Errors = AddWithNewLine(Errors, My.Resources.ActiveReports_IDeclaration_YearNull, False)
            End If
            If Not _Quarter > 0 Then
                Errors = AddWithNewLine(Errors, My.Resources.ActiveReports_IDeclaration_QuarterNull, False)
            End If
            If Not _SODRAAccount > 0 Then
                Errors = AddWithNewLine(Errors, My.Resources.ActiveReports_IDeclaration_SodraAccountNull, False)
            End If

            Return StringIsNullOrEmpty(Errors)

        End Function

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
            SD.Columns.Add("Date")
            SD.Columns.Add("SODRADepartment")
            SD.Columns.Add("Year")
            SD.Columns.Add("Quarter")
            SD.Columns.Add("Tarif")
            SD.Columns.Add("3SDPageCount")
            SD.Columns.Add("TotalPageCount")
            SD.Columns.Add("WorkerCount")
            SD.Columns.Add("TotalIncome")
            SD.Columns.Add("TotalPayments")
            SD.Columns.Add("F1")
            SD.Columns.Add("F2")
            SD.Columns.Add("F3")
            SD.Columns.Add("F4")
            SD.Columns.Add("F6")
            SD.Columns.Add("M1")
            SD.Columns.Add("M2")
            SD.Columns.Add("M3")
            SD.Columns.Add("M3.1")
            SD.Columns.Add("M3.2")
            SD.Columns.Add("M3.3")
            SD.Columns.Add("M4")
            SD.Columns.Add("M5")
            SD.Columns.Add("M6")
            SD.Columns.Add("M7")
            SD.Columns.Add("M7.1")
            SD.Columns.Add("M7.2")
            SD.Columns.Add("M7.3")
            SD.Columns.Add("M8")
            SD.Columns.Add("M11")

            SD.Rows.Add()

            Dim DD As New DataTable("Details")
            DD.Columns.Add("Count")
            DD.Columns.Add("PersonName")
            DD.Columns.Add("PersonCode")
            DD.Columns.Add("SODRACode")
            DD.Columns.Add("Income")
            DD.Columns.Add("Payment")

            Try
                Dim LastDayInQuarter As Integer = 31
                If _Quarter = 2 OrElse _Quarter = 3 Then LastDayInQuarter = 30

                Dim myComm As New SQLCommand("FetchDeclarationSAM(1)_1")
                myComm.AddParam("?YR", _Year)
                myComm.AddParam("?MF", ((_Quarter - 1) * 3) + 1)
                myComm.AddParam("?MT", ((_Quarter - 1) * 3) + 3)
                myComm.AddParam("?DT", New Date(_Year, ((_Quarter - 1) * 3) + 3, LastDayInQuarter))

                Dim TotalIncome As Double = 0
                Dim TotalPayments As Double = 0
                Dim TotalTarif As Double
                Dim i As Integer

                Using myData As DataTable = myComm.Fetch
                    Declaration.ClearDatatable(myData, 0)
                    i = 0
                    For Each dr As DataRow In myData.Rows
                        DD.Rows.Add()
                        DD.Rows(i).Item(0) = i + 1
                        DD.Rows(i).Item(1) = GetLimitedLengthString(dr.Item(0).ToString.Trim, 68).ToUpper
                        DD.Rows(i).Item(2) = dr.Item(1).ToString.Trim
                        DD.Rows(i).Item(3) = dr.Item(2).ToString.Trim
                        DD.Rows(i).Item(4) = DblParser(CDbl(dr.Item(3)))
                        DD.Rows(i).Item(5) = DblParser(CDbl(dr.Item(4)))
                        TotalIncome = TotalIncome + CDbl(dr.Item(3))
                        TotalPayments = TotalPayments + CDbl(dr.Item(4))
                        i += 1
                    Next
                End Using

                myComm = New SQLCommand("FetchDeclarationSAM(1)_2")
                myComm.AddParam("?YR", _Year)
                myComm.AddParam("?MF", ((_Quarter - 1) * 3) + 1)
                myComm.AddParam("?MT", ((_Quarter - 1) * 3) + 3)

                Using myData As DataTable = myComm.Fetch
                    Declaration.ClearDatatable(myData, 0)

                    If myData.Rows.Count > 1 Then

                        Dim ratesList As New List(Of String)

                        For Each dr As DataRow In myData.Rows
                            ratesList.Add(String.Format(My.Resources.ActiveReports_Declarations_DeclarationSam1_MultipleRateItem, _
                                CDblSafe(dr.Item(0), 2, 0).ToString, CDblSafe(dr.Item(2), 2, 0).ToString, _
                                CDblSafe(dr.Item(1), 2, 0).ToString, CDblSafe(dr.Item(3), 2, 0).ToString))
                        Next

                        _Warnings = AddWithNewLine(_Warnings, String.Format( _
                            My.Resources.ActiveReports_Declarations_DeclarationSam1_MultipleRates, _
                            vbCrLf, String.Join(vbCrLf, ratesList.ToArray), vbCrLf), False)

                    ElseIf myData.Rows.Count < 1 Then

                        myData.Rows.Add()
                        myData.Rows(0).Item(0) = GetCurrentCompany.Rates.GetRate(General.DefaultRateType.SodraEmployee)
                        myData.Rows(0).Item(1) = GetCurrentCompany.Rates.GetRate(General.DefaultRateType.SodraEmployer)
                        myData.Rows(0).Item(2) = GetCurrentCompany.Rates.GetRate(General.DefaultRateType.PsdEmployee)
                        myData.Rows(0).Item(3) = GetCurrentCompany.Rates.GetRate(General.DefaultRateType.PsdEmployer)

                        _Warnings = AddWithNewLine(_Warnings, My.Resources.ActiveReports_Declarations_DeclarationSam1_NullRates, False)

                    End If

                    If _Year < 2009 Then
                        TotalTarif = CDbl(myData.Rows(0).Item(0)) + CDbl(myData.Rows(0).Item(1))
                    Else
                        TotalTarif = CDbl(myData.Rows(0).Item(0)) + CDbl(myData.Rows(0).Item(1)) _
                            + CDbl(myData.Rows(0).Item(2)) + CDbl(myData.Rows(0).Item(3))
                    End If

                End Using

                SD.Rows(0).Item(0) = _Date.ToShortDateString
                SD.Rows(0).Item(1) = _SODRADepartment
                SD.Rows(0).Item(2) = _Year
                SD.Rows(0).Item(3) = _Quarter
                SD.Rows(0).Item(4) = DblParser(TotalTarif)
                SD.Rows(0).Item(5) = Math.Ceiling(DD.Rows.Count / 9)
                SD.Rows(0).Item(6) = Math.Ceiling(DD.Rows.Count / 9) + 1
                SD.Rows(0).Item(7) = DD.Rows.Count
                SD.Rows(0).Item(8) = DblParser(TotalIncome)
                SD.Rows(0).Item(9) = DblParser(TotalPayments)

                myComm = New SQLCommand("FetchDeclarationSAM(1)_3")
                myComm.AddParam("?DF", New Date(_Year, ((_Quarter - 1) * 3) + 1, 1))
                myComm.AddParam("?DT", New Date(_Year, ((_Quarter - 1) * 3) + 3, LastDayInQuarter))

                Using myData As DataTable = myComm.Fetch
                    SD.Rows(0).Item(10) = CInt(myData.Rows(0).Item(1)) - CInt(myData.Rows(1).Item(1))
                    SD.Rows(0).Item(11) = CInt(myData.Rows(2).Item(1))
                    SD.Rows(0).Item(12) = CInt(myData.Rows(3).Item(1))
                    SD.Rows(0).Item(13) = CInt(myData.Rows(0).Item(1)) - _
                        CInt(myData.Rows(1).Item(1)) + CInt(myData.Rows(2).Item(1)) _
                        - CInt(myData.Rows(3).Item(1))
                End Using

                Dim BalanceYearStart As Double = 0
                Dim PaymentsDueQuarterStart As Double = 0
                Dim PaymentsDueQuarter As Double = 0
                Dim PaymentsDue1 As Double = 0
                Dim PaymentsDue2 As Double = 0
                Dim PaymentsDue3 As Double = 0
                Dim PaymentsDueQuarterEnd As Double = 0
                Dim PaymentsBalanceQuarterEnd As Double = 0
                Dim PaymentsMadeQuarterStart As Double = 0
                Dim PaymentsMadeQuarter As Double = 0
                Dim PaymentsMade1 As Double = 0
                Dim PaymentsMade2 As Double = 0
                Dim PaymentsMade3 As Double = 0
                Dim PaymentsMadeQuarterEnd As Double = 0
                Dim BalanceQuarterEnd As Double = 0

                Dim TotalIncomePerQuarter As Double = 0

                ' there is a problem with this query:
                ' a journal entry that transfers sums between pripary and secondary
                ' accounts is treated as affecting balance at the begining of the year
                myComm = New SQLCommand("FetchDeclarationSAM(1)_4")
                myComm.AddParam("?YR", _Year)
                myComm.AddParam("?SP", _SODRAAccount)
                myComm.AddParam("?SS", _SODRAAccount2)

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
                    Dim QSM As Integer = ((_Quarter - 1) * 3) + 1
                    Dim QEM As Integer = ((_Quarter - 1) * 3) + 3

                    For Each dr As DataRow In myData.Rows
                        If CInt(dr.Item(0)) < QSM Then
                            PaymentsDueQuarterStart = PaymentsDueQuarterStart + CDbl(dr.Item(2))
                            PaymentsMadeQuarterStart = PaymentsMadeQuarterStart + CDbl(dr.Item(3))

                        ElseIf CInt(dr.Item(0)) >= QSM AndAlso CInt(dr.Item(0)) <= QEM Then

                            If CInt(dr.Item(0)) = QSM Then
                                PaymentsDue1 = PaymentsDue1 + CDbl(dr.Item(2))
                                PaymentsMade1 = PaymentsMade1 + CDbl(dr.Item(3))
                            ElseIf CInt(dr.Item(0)) = QEM Then
                                PaymentsDue3 = PaymentsDue3 + CDbl(dr.Item(2))
                                PaymentsMade3 = PaymentsMade3 + CDbl(dr.Item(3))
                            Else
                                PaymentsDue2 = PaymentsDue2 + CDbl(dr.Item(2))
                                PaymentsMade2 = PaymentsMade2 + CDbl(dr.Item(3))
                            End If

                            TotalIncomePerQuarter = TotalIncomePerQuarter + CDbl(dr.Item(1))

                        ElseIf CInt(dr.Item(0)) = 13 Then
                            BalanceYearStart = BalanceYearStart + CDbl(dr.Item(3))

                        End If
                    Next

                End Using

                PaymentsDueQuarter = CRound(PaymentsDue1 + PaymentsDue2 + PaymentsDue3, 2)
                PaymentsDueQuarterEnd = CRound(PaymentsDueQuarterStart + PaymentsDueQuarter, 2)
                PaymentsBalanceQuarterEnd = CRound(BalanceYearStart + PaymentsDueQuarterEnd, 2)

                PaymentsMadeQuarter = CRound(PaymentsMade1 + PaymentsMade2 + PaymentsMade3, 2)
                PaymentsMadeQuarterEnd = CRound(PaymentsMadeQuarterStart + PaymentsMadeQuarter, 2)
                BalanceQuarterEnd = CRound(PaymentsBalanceQuarterEnd - PaymentsMadeQuarterEnd, 2)

                SD.Rows(0).Item(14) = DblParser(TotalIncomePerQuarter)
                SD.Rows(0).Item(15) = DblParser(BalanceYearStart)
                SD.Rows(0).Item(16) = DblParser(PaymentsDueQuarterStart)
                SD.Rows(0).Item(17) = DblParser(PaymentsDueQuarter)
                SD.Rows(0).Item(18) = DblParser(PaymentsDue1)
                SD.Rows(0).Item(19) = DblParser(PaymentsDue2)
                SD.Rows(0).Item(20) = DblParser(PaymentsDue3)
                SD.Rows(0).Item(21) = DblParser(PaymentsDueQuarterEnd)
                SD.Rows(0).Item(22) = DblParser(PaymentsBalanceQuarterEnd)
                SD.Rows(0).Item(23) = DblParser(PaymentsMadeQuarterStart)
                SD.Rows(0).Item(24) = DblParser(PaymentsMadeQuarter)
                SD.Rows(0).Item(25) = DblParser(PaymentsMade1)
                SD.Rows(0).Item(26) = DblParser(PaymentsMade2)
                SD.Rows(0).Item(27) = DblParser(PaymentsMade3)
                SD.Rows(0).Item(28) = DblParser(PaymentsMadeQuarterEnd)
                SD.Rows(0).Item(29) = DblParser(BalanceQuarterEnd)

                If _Quarter = 1 Then
                    SD.Rows(0).Item(16) = ""
                    SD.Rows(0).Item(23) = ""
                End If

                result.Tables.Add(SD)
                result.Tables.Add(DD)

            Catch ex As Exception
                SD.Dispose()
                DD.Dispose()
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

            Dim i As Integer
            Dim DDS As DataSet = declarationDataSet

            Dim CurrentUser As AccDataAccessLayer.Security.AccIdentity = GetCurrentIdentity()

            ' Add 3SD appendixes to the ffdata xml structure if needed
            ' and copy form structure to the temp file
            If CInt(DDS.Tables("Specific").Rows(0).Item(5)) > 1 Then
                Dim myDoc As New Xml.XmlDocument
                myDoc.Load(AppPath() & FILENAMEFFDATASAM01)
                For i = 1 To CInt(DDS.Tables("Specific").Rows(0).Item(5)) - 1
                    Dim AddSD As Xml.XmlElement = DirectCast(myDoc.ChildNodes(1).ChildNodes(0). _
                        ChildNodes(1).ChildNodes(2).Clone, Xml.XmlElement)
                    AddSD.Attributes(1).Value = (i + 3).ToString
                    myDoc.ChildNodes(1).ChildNodes(0).ChildNodes(1).AppendChild(AddSD)
                    Dim AddPg As Xml.XmlElement = DirectCast(myDoc.ChildNodes(1).ChildNodes(0). _
                        ChildNodes(0).ChildNodes(0).ChildNodes(2).Clone, Xml.XmlElement)
                    myDoc.ChildNodes(1).ChildNodes(0).ChildNodes(0).ChildNodes(0).AppendChild(AddPg)
                Next
                myDoc.ChildNodes(1).ChildNodes(0).ChildNodes(1).Attributes(0).Value = _
                    (2 + CInt(DDS.Tables("Specific").Rows(0).Item(5))).ToString
                myDoc.Save(AppPath() & FILENAMEFFDATATEMP)
            Else
                IO.File.Copy(AppPath() & FILENAMEFFDATASAM01, AppPath() & FILENAMEFFDATATEMP)
            End If

            ' read ffdata xml structure to dataset
            Dim FormDataSet As New DataSet
            Using FormFileStream As IO.FileStream = New IO.FileStream( _
                AppPath() & FILENAMEFFDATATEMP, IO.FileMode.Open)
                FormDataSet.ReadXml(FormFileStream)
                FormFileStream.Close()
            End Using

            FormDataSet.Tables(0).Rows(0).Item(3) = CurrentUser.Name
            FormDataSet.Tables(0).Rows(0).Item(4) = GetDateInFFDataFormat(Today)
            FormDataSet.Tables(1).Rows(0).Item(2) = AppPath() & FILENAMEMXFDSAM01

            Dim SD3F As Boolean = False
            Dim SpecificDataRow As DataRow = DDS.Tables("Specific").Rows(0)
            For i = 1 To FormDataSet.Tables(8).Rows.Count
                If FormDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "FormCode" Then _
                    SD3F = (FormDataSet.Tables(8).Rows(i - 1).Item(1).ToString = "SAM3SD")
                If FormDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "InsurerName" Then
                    FormDataSet.Tables(8).Rows(i - 1).Item(1) = GetLimitedLengthString( _
                        DDS.Tables("General").Rows(0).Item(0).ToString, 68).ToUpper
                ElseIf FormDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "InsurerCode" Then
                    FormDataSet.Tables(8).Rows(i - 1).Item(1) = DDS.Tables("General").Rows(0).Item(3)
                ElseIf FormDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "PreparatorDetails" Then
                    FormDataSet.Tables(8).Rows(i - 1).Item(1) = GetLimitedLengthString( _
                        preparatorName, 68).ToUpper
                ElseIf FormDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "InsurerPhone" Then
                    FormDataSet.Tables(8).Rows(i - 1).Item(1) = GetLimitedLengthString(DDS.Tables("General").Rows(0).Item(8).ToString, 15)
                ElseIf FormDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "JuridicalPersonCode" Then
                    FormDataSet.Tables(8).Rows(i - 1).Item(1) = DDS.Tables("General").Rows(0).Item(1)
                ElseIf FormDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "InsurerAddress" Then
                    FormDataSet.Tables(8).Rows(i - 1).Item(1) = GetLimitedLengthString( _
                        DDS.Tables("General").Rows(0).Item(2).ToString, 68).ToUpper
                ElseIf FormDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "RecipientDepName" Then
                    FormDataSet.Tables(8).Rows(i - 1).Item(1) = SpecificDataRow.Item(1).ToString.ToUpper
                ElseIf FormDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "DocDate" Then
                    FormDataSet.Tables(8).Rows(i - 1).Item(1) = GetDateInFFDataFormat(CDate(SpecificDataRow.Item(0)))
                ElseIf FormDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "TaxRateRep" Then
                    FormDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat(CDbl(SpecificDataRow.Item(4)))
                ElseIf FormDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "CycleYear" Then
                    FormDataSet.Tables(8).Rows(i - 1).Item(1) = SpecificDataRow.Item(2)
                ElseIf FormDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "CycleQuarter" Then
                    FormDataSet.Tables(8).Rows(i - 1).Item(1) = SpecificDataRow.Item(3)
                ElseIf FormDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "Appendixes1" Then
                    FormDataSet.Tables(8).Rows(i - 1).Item(1) = 1
                ElseIf FormDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "Apdx1PageCount" Then
                    FormDataSet.Tables(8).Rows(i - 1).Item(1) = 1
                ElseIf FormDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "Appendixes2" Then
                    FormDataSet.Tables(8).Rows(i - 1).Item(1) = 1
                ElseIf FormDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "Apdx2PageCount" Then
                    FormDataSet.Tables(8).Rows(i - 1).Item(1) = SpecificDataRow.Item(5)
                ElseIf FormDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "PageTotal" And SD3F Then
                    FormDataSet.Tables(8).Rows(i - 1).Item(1) = SpecificDataRow.Item(5)
                ElseIf FormDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "Apdx2PersonCount" Then
                    FormDataSet.Tables(8).Rows(i - 1).Item(1) = SpecificDataRow.Item(7)
                ElseIf FormDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "Apdx2InsIncomeSum" Then
                    FormDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat(CDbl(SpecificDataRow.Item(8)))
                ElseIf FormDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "Apdx2PaymentSum" Then
                    FormDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat(CDbl(SpecificDataRow.Item(9)))
                ElseIf FormDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "ManagerJobPosition" Then
                    FormDataSet.Tables(8).Rows(i - 1).Item(1) = "DIREKTORIUS"
                ElseIf FormDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "ManagerFullName" Then
                    FormDataSet.Tables(8).Rows(i - 1).Item(1) = GetLimitedLengthString( _
                        DDS.Tables("General").Rows(0).Item(9).ToString, 68).ToUpper
                ElseIf FormDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "PersonCountQuarterStart" Then
                    FormDataSet.Tables(8).Rows(i - 1).Item(1) = SpecificDataRow.Item(10)
                ElseIf FormDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "PersonCountStarted" Then
                    FormDataSet.Tables(8).Rows(i - 1).Item(1) = SpecificDataRow.Item(11)
                ElseIf FormDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "PersonCountEnded" Then
                    FormDataSet.Tables(8).Rows(i - 1).Item(1) = SpecificDataRow.Item(12)
                ElseIf FormDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "PersonCountQuarterEnd" Then
                    FormDataSet.Tables(8).Rows(i - 1).Item(1) = SpecificDataRow.Item(13)
                ElseIf FormDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "IncomeSum" Then
                    FormDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat( _
                        CDbl(SpecificDataRow.Item(14)))
                ElseIf FormDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "ChargedDebtYearStart" Then
                    If String.IsNullOrEmpty(SpecificDataRow.Item(15).ToString.Trim) Then
                        FormDataSet.Tables(8).Rows(i - 1).Item(1) = ""
                    Else
                        FormDataSet.Tables(8).Rows(i - 1).Item(1) = _
                            GetNumberInFFDataFormat(CDbl(SpecificDataRow.Item(15)))
                    End If
                ElseIf FormDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "ChargedDebtQuarterEnd" Then
                    If String.IsNullOrEmpty(SpecificDataRow.Item(16).ToString.Trim) Then
                        FormDataSet.Tables(8).Rows(i - 1).Item(1) = ""
                    Else
                        FormDataSet.Tables(8).Rows(i - 1).Item(1) = _
                            GetNumberInFFDataFormat(CDbl(SpecificDataRow.Item(16)))
                    End If
                ElseIf FormDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "ChargedMonthly" Then
                    FormDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat( _
                        CDbl(SpecificDataRow.Item(17)))
                ElseIf FormDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "ChargedMonth1" Then
                    FormDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat( _
                        CDbl(SpecificDataRow.Item(18)))
                ElseIf FormDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "ChargedMonth2" Then
                    FormDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat( _
                        CDbl(SpecificDataRow.Item(19)))
                ElseIf FormDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "ChargedMonth3" Then
                    FormDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat( _
                        CDbl(SpecificDataRow.Item(20)))
                ElseIf FormDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "ChargedYearTotal" Then
                    FormDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat( _
                        CDbl(SpecificDataRow.Item(21)))
                ElseIf FormDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "ChargedTotal" Then
                    FormDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat( _
                        CDbl(SpecificDataRow.Item(22)))
                ElseIf FormDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "TransferQuarterEnd" Then
                    If String.IsNullOrEmpty(SpecificDataRow.Item(23).ToString.Trim) Then
                        FormDataSet.Tables(8).Rows(i - 1).Item(1) = ""
                    Else
                        FormDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat( _
                            CDbl(SpecificDataRow.Item(23)))
                    End If
                ElseIf FormDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "TransferMonthly" Then
                    FormDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat( _
                        CDbl(SpecificDataRow.Item(24)))
                ElseIf FormDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "TransferMonth1" Then
                    FormDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat( _
                        CDbl(SpecificDataRow.Item(25)))
                ElseIf FormDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "TransferMonth2" Then
                    FormDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat( _
                        CDbl(SpecificDataRow.Item(26)))
                ElseIf FormDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "TransferMonth3" Then
                    FormDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat( _
                        CDbl(SpecificDataRow.Item(27)))
                ElseIf FormDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "TransferYearTotal" Then
                    FormDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat( _
                        CDbl(SpecificDataRow.Item(28)))
                ElseIf FormDataSet.Tables(8).Rows(i - 1).Item(0).ToString = "DebtQuarterEnd" Then
                    FormDataSet.Tables(8).Rows(i - 1).Item(1) = GetNumberInFFDataFormat( _
                        CDbl(SpecificDataRow.Item(29)))
                End If
            Next

            Dim j, p As Integer
            Dim PageIncome As Double = 0
            Dim PagePayments As Double = 0
            Dim DetailsDataTable As DataTable = DDS.Tables("Details")
            For i = 1 To CInt(SpecificDataRow.Item(5))
                p = 9 * (i - 1)
                For j = 1 To Math.Min(9, CInt(SpecificDataRow.Item(7)) - p)
                    If i = 1 Then
                        FormDataSet.Tables(8).Rows(87 + 7 * (j - 1)).Item(1) = j
                        FormDataSet.Tables(8).Rows(88 + 7 * (j - 1)).Item(1) = _
                            DetailsDataTable.Rows(p + j - 1).Item(2)
                        FormDataSet.Tables(8).Rows(89 + 7 * (j - 1)).Item(1) = _
                            DetailsDataTable.Rows(p + j - 1).Item(3).ToString.Trim.Substring(0, 2)
                        FormDataSet.Tables(8).Rows(90 + 7 * (j - 1)).Item(1) = _
                            GetNumericPart(DetailsDataTable.Rows(p + j - 1).Item(3).ToString.Trim)
                        FormDataSet.Tables(8).Rows(91 + 7 * (j - 1)).Item(1) = _
                            GetNumberInFFDataFormat(CDbl(DetailsDataTable.Rows(p + j - 1).Item(4)))
                        FormDataSet.Tables(8).Rows(92 + 7 * (j - 1)).Item(1) = _
                            GetNumberInFFDataFormat(CDbl(DetailsDataTable.Rows(p + j - 1).Item(5)))
                        FormDataSet.Tables(8).Rows(93 + 7 * (j - 1)).Item(1) = _
                            DetailsDataTable.Rows(p + j - 1).Item(1).ToString.ToUpper
                    Else
                        FormDataSet.Tables(8).Rows(87 + 7 * (j - 1) + (i - 1) * 72).Item(1) = j
                        FormDataSet.Tables(8).Rows(88 + 7 * (j - 1) + (i - 1) * 72).Item(1) = _
                            DetailsDataTable.Rows(p + j - 1).Item(2)
                        FormDataSet.Tables(8).Rows(89 + 7 * (j - 1) + (i - 1) * 72).Item(1) = _
                            DetailsDataTable.Rows(p + j - 1).Item(3).ToString.Trim.Substring(0, 2)
                        FormDataSet.Tables(8).Rows(90 + 7 * (j - 1) + (i - 1) * 72).Item(1) = _
                            GetNumericPart(DetailsDataTable.Rows(p + j - 1).Item(3).ToString.Trim)
                        FormDataSet.Tables(8).Rows(91 + 7 * (j - 1) + (i - 1) * 72).Item(1) = _
                            GetNumberInFFDataFormat(CDbl(DetailsDataTable.Rows(p + j - 1).Item(4)))
                        FormDataSet.Tables(8).Rows(92 + 7 * (j - 1) + (i - 1) * 72).Item(1) = _
                            GetNumberInFFDataFormat(CDbl(DetailsDataTable.Rows(p + j - 1).Item(5)))
                        FormDataSet.Tables(8).Rows(93 + 7 * (j - 1) + (i - 1) * 72).Item(1) = _
                            DetailsDataTable.Rows(p + j - 1).Item(1).ToString.ToUpper
                    End If
                    PageIncome = PageIncome + CDbl(DetailsDataTable.Rows(p + j - 1).Item(4))
                    PagePayments = PagePayments + CDbl(DetailsDataTable.Rows(p + j - 1).Item(5))
                Next
                If i = 1 Then
                    FormDataSet.Tables(8).Rows(150).Item(1) = GetNumberInFFDataFormat(PageIncome)
                    FormDataSet.Tables(8).Rows(151).Item(1) = GetNumberInFFDataFormat(PagePayments)
                Else
                    FormDataSet.Tables(8).Rows(150 + (i - 1) * 72).Item(1) = GetNumberInFFDataFormat(PageIncome)
                    FormDataSet.Tables(8).Rows(151 + (i - 1) * 72).Item(1) = GetNumberInFFDataFormat(PagePayments)
                End If
                PageIncome = 0
                PagePayments = 0
            Next

            Return FormDataSet

        End Function

    End Class

End Namespace