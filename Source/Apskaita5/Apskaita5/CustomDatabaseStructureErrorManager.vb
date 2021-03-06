﻿Imports AccDataAccessLayer.DatabaseAccess.DatabaseStructure
Imports ApskaitaObjects.Assets
<Serializable()> _
Public Class CustomDatabaseStructureErrorManager
    Implements IDatabaseStructureErrorManager

    Private ReadOnly OldVersionCodes As String() = {ErrorCode_OldVersionLedger.Trim.ToUpper, _
        ErrorCode_OldVersionAssets.Trim.ToUpper, ErrorCode_OldVersionRegionalContent.Trim.ToUpper, _
        ErrorCode_OldVersionRegionalPrices.Trim.ToUpper, ErrorCode_OldVersionAdvanceReports1.Trim.ToUpper, _
        ErrorCode_OldVersionAdvanceReports2.Trim.ToUpper, ErrorCode_OldVersionAdvanceReports3.Trim.ToUpper, _
        ErrorCode_OldVersionCashAccounts.Trim.ToUpper, ErrorCode_OldVersionOffsets.Trim.ToUpper, _
        ErrorCode_OldVersionFinancialStatements.Trim.ToUpper, ErrorCode_OldVersionInvoicesMade.Trim.ToUpper, _
        ErrorCode_OldVersionInvoicesReceived.Trim.ToUpper, ErrorCode_OldVersionSettings.Trim.ToUpper, _
        ErrorCode_OldVersionGoods.Trim.ToUpper}

    Private Const ErrorCode_OldVersionPersons As String = "SV_ASMN"
    Private Const ErrorCode_OldVersionLedger As String = "SV_BZUR"
    Private Const ErrorCode_OldVersionAssets As String = "SV_ITUR"
    Private Const ErrorCode_OldVersionRegionalContent As String = "SV_REGCO"
    Private Const ErrorCode_OldVersionRegionalPrices As String = "SV_REGPR"
    Private Const ErrorCode_OldVersionAdvanceReports1 As String = "SV_APYS1"
    Private Const ErrorCode_OldVersionAdvanceReports2 As String = "SV_APYS2"
    Private Const ErrorCode_OldVersionAdvanceReports3 As String = "SV_APYS3"
    Private Const ErrorCode_OldVersionCashAccounts As String = "SV_CASH"
    Private Const ErrorCode_OldVersionOffsets As String = "SV_OFFSET"
    Private Const ErrorCode_OldVersionFinancialStatements As String = "SV_FINSTR"
    Private Const ErrorCode_OldVersionInvoicesMade As String = "SV_SFD"
    Private Const ErrorCode_OldVersionInvoicesReceived As String = "SV_SFG"
    Private Const ErrorCode_OldVersionSettings As String = "SV_NUS"
    Private Const ErrorCode_OldVersionGoods As String = "SV_GOODS"
    Private Const ErrorCode_CompanyDataMissing As String = "CE_COMPANY"
    Private Const ErrorCode_SerialCodesInvalid As String = "CE_SERIALS"
    Private Const ErrorCode_OldVersionWages As String = "SV_WAGE"
    Private Const ErrorCode_OldVersionAccounts As String = "SV_ACC"
    Private Const ErrorCode_OldVersionAssetOperationDocNo As String = "SV_ITDN"
    Private Const ErrorCode_InvalidJournalEntries As String = "CE_BALANCE"
    Private Const ErrorCode_OrphanAccounts As String = "CE_ORPHANACCOUNTS"
    Private Const ErrorCode_OrphanEntries As String = "CE_ORPHANENTRIES"

    Private oldVersionRelationMismatchesUpgraded As Boolean = False


    Public Sub FetchCustomErrors(ByRef StructureErrorList As DatabaseStructureErrorList, _
        ByVal DeFactoDatabaseStructure As DatabaseStructure, ByVal DatabaseName As String) _
        Implements IDatabaseStructureErrorManager.FetchCustomErrors

        Dim oldVersionAssetsDetected As Boolean = False

        Dim isFound As Boolean

        For Each tbl As DatabaseTable In DeFactoDatabaseStructure.TableList
            If tbl.Name.Trim.ToLower = "asmenys" Then
                For Each col As DatabaseField In tbl.FieldList
                    If col.Name.Trim.ToLower = "grupe" Then
                        StructureErrorList.Add(DatabaseStructureError.GetDatabaseStructureError( _
                            "Senos versijos asmenų lentelė.", "asmenys", "", "", True, _
                            ErrorCode_OldVersionPersons))
                        Exit For
                    End If
                Next
                Exit For
            End If
        Next

        For Each tbl As DatabaseTable In DeFactoDatabaseStructure.TableList
            If tbl.Name.Trim.ToLower = "bz" Then
                For Each col As DatabaseField In tbl.FieldList
                    If col.Name.Trim.ToLower = "op_analitika" Then
                        If col.Type = DatabaseFieldType.Char Then
                            StructureErrorList.Add(DatabaseStructureError.GetDatabaseStructureError( _
                                "Senos versijos bendrojo žurnalo lentelės.", "bz", "", "", True, _
                                ErrorCode_OldVersionLedger))
                        End If
                        Exit For
                    End If
                Next
                Exit For
            End If
        Next

        For Each tbl As DatabaseTable In DeFactoDatabaseStructure.TableList
            If tbl.Name.Trim.ToLower = "turtas_op" Then
                For Each col As DatabaseField In tbl.FieldList
                    If col.Name.Trim.ToLower = "tipas" Then
                        StructureErrorList.Add(DatabaseStructureError.GetDatabaseStructureError( _
                            "Senos versijos ilgalaikio turto lentelės.", "turtas_op", "", "", True, _
                            ErrorCode_OldVersionAssets))
                        oldVersionAssetsDetected = True
                        Exit For
                    End If
                Next
                Exit For
            End If
        Next

        Dim paslaugosExists As Boolean = False
        For Each tbl As DatabaseTable In DeFactoDatabaseStructure.TableList
            If tbl.Name.Trim.ToUpper = "paslaugos".Trim.ToUpper Then
                paslaugosExists = True
                Exit For
            End If
        Next

        If paslaugosExists Then

            For Each err As DatabaseStructureError In StructureErrorList
                If err.Table.Trim.ToLower = "regionalcontents" AndAlso String.IsNullOrEmpty(err.Field.Trim) Then
                    StructureErrorList.Add(DatabaseStructureError.GetDatabaseStructureError( _
                        "Senos versijos prekių ir paslaugų regionalizavimo lentelė 'RegionalContents'.", _
                            "regionalcontents", "", "", True, ErrorCode_OldVersionRegionalContent))
                    Exit For
                End If
            Next

            For Each err As DatabaseStructureError In StructureErrorList
                If err.Table.Trim.ToLower = "regionalprices" AndAlso String.IsNullOrEmpty(err.Field.Trim) Then
                    StructureErrorList.Add(DatabaseStructureError.GetDatabaseStructureError( _
                        "Senos versijos prekių ir paslaugų regionalizavimo lentelė 'RegionalPrices'.", _
                            "regionalprices", "", "", True, ErrorCode_OldVersionRegionalPrices))
                    Exit For
                End If
            Next

        End If

        isFound = False
        For Each tbl As DatabaseTable In DeFactoDatabaseStructure.TableList
            If tbl.Name.Trim.ToLower = "apyskaitos" Then
                For Each col As DatabaseField In tbl.FieldList

                    If col.Name.Trim.ToLower = "pvmkor" Then

                        For Each err As DatabaseStructureError In StructureErrorList
                            If err.Table.Trim.ToLower = "advancereports" AndAlso String.IsNullOrEmpty(err.Field.Trim) Then
                                StructureErrorList.Add(DatabaseStructureError.GetDatabaseStructureError( _
                                    "Senos versijos avanso apyskaitų lentelės.", "apyskaitos", _
                                    "", "", True, ErrorCode_OldVersionAdvanceReports1))
                                isFound = True
                                Exit For
                            End If
                        Next

                        If Not isFound Then
                            StructureErrorList.Add(DatabaseStructureError.GetDatabaseStructureError( _
                                "Senos versijos avanso apyskaitos lentelė.", "apyskaitos", "", "", True, _
                                ErrorCode_OldVersionAdvanceReports2))
                            isFound = True
                        End If

                        Exit For

                    End If

                Next
                Exit For
            End If
        Next

        Dim baseCashTablesExist As Integer = 0
        For Each tbl As DatabaseTable In DeFactoDatabaseStructure.TableList
            If tbl.Name.Trim.ToLower = "kio" OrElse tbl.Name.Trim.ToLower = "kpo" _
                OrElse tbl.Name.Trim.ToLower = "banko" Then baseCashTablesExist += 1
        Next

        If baseCashTablesExist > 2 Then

            For Each err As DatabaseStructureError In StructureErrorList
                If err.Table.Trim.ToLower = "cashaccounts" AndAlso String.IsNullOrEmpty(err.Field.Trim) Then
                    StructureErrorList.Add(DatabaseStructureError.GetDatabaseStructureError( _
                        "Senos versijos lėšų sąskaitų lentelė.", "CashAccounts", "", "", True, _
                        ErrorCode_OldVersionCashAccounts))
                    Exit For
                End If
            Next

            For Each err As DatabaseStructureError In StructureErrorList
                If err.Table.Trim.ToLower = "offsetitems" AndAlso String.IsNullOrEmpty(err.Field.Trim) Then
                    StructureErrorList.Add(DatabaseStructureError.GetDatabaseStructureError( _
                        "Senos versijos užskaitų lentelė.", "OffsetItems", "", "", True, _
                        ErrorCode_OldVersionOffsets))
                    Exit For
                End If
            Next

        End If

        For Each err As DatabaseStructureError In StructureErrorList
            If err.Table.Trim.ToLower = "f_strukt" Then
                For Each erro As DatabaseStructureError In StructureErrorList
                    If erro.Table.Trim.ToLower = "financialstatementsstructure" AndAlso String.IsNullOrEmpty(erro.Field.Trim) Then
                        StructureErrorList.Add(DatabaseStructureError.GetDatabaseStructureError( _
                            "Senos versijos finansinės atskaitomybės struktūros lentelė.", _
                            "FinancialStatementsStructure", "", "", True, ErrorCode_OldVersionFinancialStatements))
                        Exit For
                    End If
                Next
                Exit For
            End If
        Next

        For Each err As DatabaseStructureError In StructureErrorList
            If err.Table.Trim.ToLower = "sfd" AndAlso err.Field.Trim.ToLower = "sumoriginal" Then
                StructureErrorList.Add(DatabaseStructureError.GetDatabaseStructureError( _
                    "Senos versijos išrašytų sąskaitų faktūrų lentelė.", _
                    "sfd", "", "", True, ErrorCode_OldVersionInvoicesMade))
                Exit For
            End If
        Next

        For Each err As DatabaseStructureError In StructureErrorList
            If err.Table.Trim.ToLower = "sfg" AndAlso err.Field.Trim.ToLower = "sumoriginal" Then
                StructureErrorList.Add(DatabaseStructureError.GetDatabaseStructureError( _
                    "Senos versijos gautų sąskaitų faktūrų lentelė.", _
                    "sfg", "", "", True, ErrorCode_OldVersionInvoicesReceived))
                Exit For
            End If
        Next

        For Each err As DatabaseStructureError In StructureErrorList
            If err.Table.Trim.ToLower = "nustatymai" Then
                StructureErrorList.Add(DatabaseStructureError.GetDatabaseStructureError( _
                    "Senos versijos nustatymų lentelė.", "nustatymai", "", "", True, _
                    ErrorCode_OldVersionSettings))
                Exit For
            End If
        Next

        For Each err As DatabaseStructureError In StructureErrorList
            If err.Table.Trim.ToLower = "du_ziniarastis_d" AndAlso _
                err.Field.Trim.ToLower = "basegpm" Then
                StructureErrorList.Add(DatabaseStructureError.GetDatabaseStructureError( _
                    "Senos versijos darbo užmokesčio lentelė.", "du_ziniarastis_d", "", "", True, ErrorCode_OldVersionWages))
                Exit For
            End If
        Next

        For Each err As DatabaseStructureError In StructureErrorList
            If err.Table.Trim.ToLower = "prekes" Then
                StructureErrorList.Add(DatabaseStructureError.GetDatabaseStructureError( _
                    "Senos versijos prekių lentelė.", "prekes", "", "", True, ErrorCode_OldVersionGoods))
                Exit For
            End If
        Next

        For Each err As DatabaseStructureError In StructureErrorList
            If err.Table.Trim.ToLower = "saskaitupl" AndAlso _
                err.Field.Trim.ToLower = "fs_id" AndAlso Not err.Description.ToLower.Contains("indeks") Then
                StructureErrorList.Add(DatabaseStructureError.GetDatabaseStructureError( _
                    "Senos versijos sąskaitų plano lentelė.", "saskaitupl", "", "", True, ErrorCode_OldVersionAccounts))
                Exit For
            End If
        Next

        If Not oldVersionAssetsDetected Then
            For Each err As DatabaseStructureError In StructureErrorList
                If err.Table.Trim.ToLower = "turtas_op" AndAlso _
                    err.Field.Trim.ToLower = "docno" Then
                    StructureErrorList.Add(DatabaseStructureError.GetDatabaseStructureError( _
                        "Senos versijos IT operacijos dokumento numeris.", "turtas_op", "docno", "", True, ErrorCode_OldVersionAssetOperationDocNo))
                    Exit For
                End If
            Next
        End If

        Dim myComm As SQLCommand

        Try
            myComm = New SQLCommand("GetCompanyByDatabase")
            myComm.AddParam("**DBName", DatabaseName.Trim)
            Using CompanyData As DataTable = myComm.Fetch
                If CompanyData.Rows.Count < 1 Then
                    StructureErrorList.Add(DatabaseStructureError.GetDatabaseStructureError( _
                        "Nėra bendrų įmonės duomenų", "imone", "", "", True, ErrorCode_CompanyDataMissing))
                End If
            End Using
        Catch ex As Exception
            StructureErrorList.Add(DatabaseStructureError.GetDatabaseStructureError( _
                "Nėra bendrų įmonės duomenų lentelės arba lentelė sugadinta", "imone", _
                "", "", True, ErrorCode_CompanyDataMissing))
        End Try

        myComm = New SQLCommand("OldVersionSerialCodes")
        Using myData As DataTable = myComm.Fetch
            If myData.Rows.Count > 0 AndAlso CIntSafe(myData.Rows(0).Item(0), 0) > 0 Then
                StructureErrorList.Add(DatabaseStructureError.GetDatabaseStructureError( _
                    "Senos versijos serijų kodai", "imone", "", "", True, ErrorCode_SerialCodesInvalid))
            End If
        End Using

        myComm = New SQLCommand("FetchInvalidJournalEntries")
        Using myData As DataTable = myComm.Fetch
            If myData.Rows.Count > 0 Then

                Dim items As New List(Of String)
                For Each dr As DataRow In myData.Rows
                    items.Add(String.Format("{0} Nr. {1}: {2} (ID={3})", _
                        CDateSafe(dr.Item(1), Date.MinValue).ToString("yyyy-MM-dd"), _
                        CStrSafe(dr.Item(2)), CStrSafe(dr.Item(3)), CLongSafe(dr.Item(0), 0)).ToString())
                Next

                StructureErrorList.Add(DatabaseStructureError.GetDatabaseStructureError( _
                    String.Format("Rasta bendrojo žurnalo operacijų, kuriose debetas nelygus kreditui:{0}{1}", _
                    vbCrLf, String.Join(vbCrLf, items.ToArray())), "bz", "", "", False, _
                    ErrorCode_InvalidJournalEntries))

            End If
        End Using

        myComm = New SQLCommand("FetchOrphanAccounts")
        Using myData As DataTable = myComm.Fetch
            If myData.Rows.Count > 0 Then

                Dim items As New List(Of String)
                For Each dr As DataRow In myData.Rows
                    If CLongSafe(dr.Item(0), 0) > 0 Then items.Add(CLongSafe(dr.Item(0), 0).ToString())
                Next

                If items.Count > 0 Then
                    StructureErrorList.Add(DatabaseStructureError.GetDatabaseStructureError( _
                        String.Format("Rasta bendrajame žurnale naudojamų apskaitos sąskaitų, neįtrauktų į sąskaitų planą: {0}. Norėdami ištaisyti klaidą, įtraukite į sąskaitų planą šias sąskaitas.", _
                        String.Join(", ", items.ToArray())), "bzdata", "", "", False, _
                        ErrorCode_OrphanAccounts))
                End If

            End If
        End Using

        myComm = New SQLCommand("FetchOrphanEntries")
        Using myData As DataTable = myComm.Fetch
            If myData.Rows.Count > 0 Then

                Dim items As New List(Of String)
                For Each dr As DataRow In myData.Rows
                    If CLongSafe(dr.Item(0), 0) > 0 Then items.Add(CLongSafe(dr.Item(0), 0).ToString())
                Next

                If items.Count > 0 Then
                    StructureErrorList.Add(DatabaseStructureError.GetDatabaseStructureError( _
                        String.Format("Rasta bendrojo žurnalo įrašų, nesusietų su jokia bendrojo žurnalo operacija, ID: {0}.", _
                        String.Join(",", items.ToArray())), "bzdata", "", String.Join(",", items.ToArray()), True, _
                        ErrorCode_OrphanEntries))
                End If

            End If
        End Using

    End Sub

    Public Sub RepairCustomError(ByRef StructureErrorList As DatabaseStructureErrorList, _
        ByVal CustomError As DatabaseStructureError) _
        Implements IDatabaseStructureErrorManager.RepairCustomError

        For Each item As DatabaseStructureError In StructureErrorList
            If item.Description.Contains("stored procedure") AndAlso item.IsChecked _
                AndAlso item.CanBeFixedAutomatically AndAlso Not item.IsComplexError Then
                item.Update(StructureErrorList)
                item.IsChecked = False
            End If
        Next

        If Not oldVersionRelationMismatchesUpgraded Then

            If IsUpgrade(StructureErrorList) Then
                UpgradeOldVersionRelationMismatches(StructureErrorList)
            End If

            oldVersionRelationMismatchesUpgraded = True

        End If

        Select Case CustomError.ComplexErrorCode.Trim.ToUpper
            Case ErrorCode_OldVersionPersons.Trim.ToUpper
                UpgradeOldVersionPersons(StructureErrorList)
            Case ErrorCode_OldVersionLedger.Trim.ToUpper
                UpgradeOldVersionLedger(StructureErrorList)
            Case ErrorCode_OldVersionAssets.Trim.ToUpper
                UpgradeOldVersionAssets(StructureErrorList)
            Case ErrorCode_OldVersionRegionalContent.Trim.ToUpper
                UpgradeOldVersionRegionalContents(StructureErrorList)
            Case ErrorCode_OldVersionRegionalPrices.Trim.ToUpper
                UpgradeOldVersionRegionalPrices(StructureErrorList)
            Case ErrorCode_OldVersionAdvanceReports1.Trim.ToUpper
                UpgradeOldVersionAdvanceReports(StructureErrorList, True, True)
            Case ErrorCode_OldVersionAdvanceReports2.Trim.ToUpper
                UpgradeOldVersionAdvanceReports(StructureErrorList, True, False)
            Case ErrorCode_OldVersionAdvanceReports3.Trim.ToUpper
                UpgradeOldVersionAdvanceReports(StructureErrorList, False, True)
            Case ErrorCode_OldVersionCashAccounts.Trim.ToUpper
                UpgradeOldVersionCashAccounts(StructureErrorList)
            Case ErrorCode_OldVersionOffsets.Trim.ToUpper
                UpgradeOldVersionOffsets(StructureErrorList)
            Case ErrorCode_OldVersionFinancialStatements.Trim.ToUpper
                UpgradeOldVersionBalanceStructure(StructureErrorList)
            Case ErrorCode_OldVersionInvoicesMade.Trim.ToUpper
                UpgradeOldVersionInvoicesMade(StructureErrorList)
            Case ErrorCode_OldVersionInvoicesReceived.Trim.ToUpper
                UpgradeOldVersionInvoicesReceived(StructureErrorList)
            Case ErrorCode_OldVersionSettings.Trim.ToUpper
                UpgradeOldVersionSettings(StructureErrorList)
            Case ErrorCode_OldVersionGoods.Trim.ToUpper
                UpgradeOldVersionGoods(StructureErrorList)
            Case ErrorCode_CompanyDataMissing.Trim.ToUpper
                RepairCompanyDataMissing(StructureErrorList)
            Case ErrorCode_SerialCodesInvalid.Trim.ToUpper
                RepairInvalidSerialCodes(StructureErrorList)
            Case ErrorCode_OldVersionWages.Trim.ToUpper
                UpgradeOldVersionWages(StructureErrorList)
            Case ErrorCode_OldVersionAccounts
                UpgradeOldVersionAccounts(StructureErrorList)
            Case ErrorCode_OldVersionAssetOperationDocNo
                UpgradeOldVersionAssetOperationDocNo(StructureErrorList)
            Case ErrorCode_OrphanEntries
                RepairOrphanEntries(StructureErrorList)
        End Select

        If AllCustomErrorsAreFixed(StructureErrorList, CustomError) AndAlso IsUpgrade(StructureErrorList) Then

            Dim myComm As New SQLCommand("CheckIfUpgradeSucceded")
            Using myData As DataTable = myComm.Fetch
                If myData.Rows.Count > 0 Then
                    Dim result As New List(Of String)
                    For i As Integer = 1 To myData.Columns.Count
                        If CIntSafe(myData.Rows(0).Item(i - 1), 0) > 0 Then _
                            result.Add(myData.Columns(i - 1).ColumnName)
                    Next
                    If result.Count > 0 Then Throw New Exception("Klaida. Nepavyko konvertuoti " _
                        & "duomenų į naują versiją: " & String.Join(", ", result.ToArray))
                End If
            End Using

        End If

    End Sub


    Private Function IsUpgrade(ByVal structureErrorList As DatabaseStructureErrorList) As Boolean

        For Each item As DatabaseStructureError In structureErrorList
            If Not item.ComplexErrorCode Is Nothing AndAlso Not String.IsNullOrEmpty(item.ComplexErrorCode.Trim) _
                AndAlso Not Array.IndexOf(OldVersionCodes, item.ComplexErrorCode.Trim.ToUpper) < 0 Then
                Return True
            End If
        Next

        Return False

    End Function

    Private Function AllCustomErrorsAreFixed(ByVal structureErrorList As DatabaseStructureErrorList, _
        ByVal currentError As DatabaseStructureError) As Boolean

        For Each item As DatabaseStructureError In structureErrorList
            If item.IsComplexError AndAlso item.ComplexErrorCode <> currentError.ComplexErrorCode _
                AndAlso item.CanBeFixedAutomatically AndAlso Not item.IsFixed Then Return False
        Next

        Return True

    End Function


    Private Sub UpgradeOldVersionBalanceStructure(ByRef StructureErrorList As DatabaseStructureErrorList)

        Dim OldBalanceStructure As New OldBalanceItem
        OldBalanceStructure.FetchStructure()

        For i As Integer = StructureErrorList.Count To 1 Step -1
            If Not StructureErrorList.Item(i - 1).IsComplexError AndAlso _
                StructureErrorList.Item(i - 1).Table.Trim.ToLower = "financialstatementsstructure" Then

                StructureErrorList.Item(i - 1).Update(StructureErrorList)
                StructureErrorList.Item(i - 1).IsChecked = False

            End If
        Next

        Using transaction As New SqlTransaction

            Try

                OldBalanceStructure.UpdateStructure()

                transaction.Commit()

            Catch ex As Exception
                transaction.SetNonSqlException(ex)
                Throw
            End Try

        End Using

    End Sub

    Public Class OldBalanceItem
        Inherits List(Of OldBalanceItem)

        Private _Name As String
        Private _IsCredit As Boolean
        Private _Type As General.FinancialStatementItemType


        Public ReadOnly Property Name() As String
            Get
                Return _Name.Trim
            End Get
        End Property

        Public ReadOnly Property IsCredit() As Boolean
            Get
                Return _IsCredit
            End Get
        End Property

        Public ReadOnly Property [Type]() As General.FinancialStatementItemType
            Get
                Return _Type
            End Get
        End Property


        Public Sub FetchStructure()

            _Name = My.Resources.General_ConsolidatedReportItem_FinancialStatementsRootName
            _IsCredit = False
            _Type = General.FinancialStatementItemType.HeaderGeneral

            Dim myComm As New SQLCommand("GetConsolidatedReportForm")
            myComm.AddParam("?FC", "b")

            Using myData As DataTable = myComm.Fetch

                ' the first item and its subitems are allways assets
                ' the second item and its subitems are allways capital
                ' the first item and its subitems are allways assets
                ' the second item and its subitems are allways capital
                Dim FirstChildStarted As Boolean = False
                For i As Integer = 1 To myData.Rows.Count
                    If CIntSafe(myData.Rows(i - 1).Item(2)) = 1 AndAlso Not FirstChildStarted Then
                        FirstChildStarted = True
                    ElseIf CIntSafe(myData.Rows(i - 1).Item(2)) = 1 AndAlso FirstChildStarted Then
                        FirstChildStarted = False
                    End If
                    If FirstChildStarted Then
                        myData.Rows(i - 1).Item(3) = 0
                    Else
                        myData.Rows(i - 1).Item(3) = 1
                    End If
                Next

                Dim BalanceItem As New OldBalanceItem
                BalanceItem._Name = My.Resources.General_ConsolidatedReportItem_BalanceStatementRootName
                BalanceItem._IsCredit = False
                BalanceItem._Type = General.FinancialStatementItemType.HeaderStatementOfFinancialPosition

                Dim index As Integer = 0
                BalanceItem.Add(New OldBalanceItem(myData, index, _
                    General.FinancialStatementItemType.StatementOfFinancialPosition))
                BalanceItem.Add(New OldBalanceItem(myData, index, _
                    General.FinancialStatementItemType.StatementOfFinancialPosition))

                Me.Add(BalanceItem)

            End Using

            myComm = New SQLCommand("GetConsolidatedReportForm")
            myComm.AddParam("?FC", "p")

            Using myData As DataTable = myComm.Fetch

                ' all defaults to profits
                For i As Integer = 1 To myData.Rows.Count
                    myData.Rows(i - 1).Item(3) = 1
                Next

                Dim IncomeSheetItem As New OldBalanceItem
                IncomeSheetItem._Name = My.Resources.General_ConsolidatedReportItem_IncomeStatementRootName
                IncomeSheetItem._IsCredit = True
                IncomeSheetItem._Type = General.FinancialStatementItemType.HeaderStatementOfComprehensiveIncome

                IncomeSheetItem.Add(New OldBalanceItem(myData, 0, _
                    General.FinancialStatementItemType.StatementOfComprehensiveIncome))

                Me.Add(IncomeSheetItem)

            End Using

        End Sub

        Public Sub UpdateStructure()
            UpdateItem(1)
        End Sub

        Private Sub UpdateItem(ByRef index As Integer)

            Dim Left As Integer = index

            For Each i As OldBalanceItem In Me
                index += 1
                i.UpdateItem(index)
            Next

            index += 1

            Dim myComm As New SQLCommand("UpgradeOldVersionFinancialStructure")
            myComm.AddParam("?AA", _Name)
            myComm.AddParam("?AB", ConvertDbBoolean(_IsCredit))
            myComm.AddParam("?AC", Utilities.ConvertDatabaseID(_Type))
            myComm.AddParam("?AD", Left)
            myComm.AddParam("?AE", index)

            myComm.Execute()

        End Sub


        Private Sub New(ByVal myData As DataTable, ByRef index As Integer, _
            ByVal nType As General.FinancialStatementItemType)

            ' get a level of this list (1 - parent, 2 - child of parent etc.)
            Dim DefaultLevel As Integer = CIntSafe(myData.Rows(index).Item(2))
            Me.Clear()

            _Name = CStrSafe(myData.Rows(index).Item(1))
            _IsCredit = ConvertDbBoolean(CIntSafe(myData.Rows(index).Item(3)))
            _Type = nType

            Dim i As Integer
            ' start from the index provided
            For i = index + 2 To myData.Rows.Count

                ' if next item is child
                If CIntSafe(myData.Rows(i - 1).Item(2)) > DefaultLevel Then

                    Dim tmpindex As Integer = i - 1
                    Add(New OldBalanceItem(myData, tmpindex, nType))
                    i = tmpindex
                    If i > myData.Rows.Count Then Exit For

                    ' next item is not child
                Else

                    index = i - 1
                    Exit For

                End If

            Next

            If i > myData.Rows.Count Then index += 10000

        End Sub

        Public Sub New()

        End Sub

        Public Overrides Function ToString() As String
            Return _Name & " (ChildrenCount=" & Me.Count.ToString & ")"
        End Function

    End Class


    Private Sub UpgradeOldVersionRelationMismatches(ByRef StructureErrorList As DatabaseStructureErrorList)

        ' fixes possible mismatches between invoices (ledger) ID and related assets or goods operations

        Using transaction As New SqlTransaction

            Try

                Dim myComm As New SQLCommand("FixLongTermAssetsInvoiceRelations1")
                myComm.Execute()

                myComm = New SQLCommand("FixLongTermAssetsInvoiceRelations2")
                myComm.Execute()

                myComm = New SQLCommand("FixGoodsInvoiceRelations1")
                myComm.Execute()

                myComm = New SQLCommand("FixGoodsInvoiceRelations2")
                myComm.Execute()

                transaction.Commit()

            Catch ex As Exception
                transaction.SetNonSqlException(ex)
                Throw
            End Try

        End Using

    End Sub

    Private Sub UpgradeOldVersionPersons(ByRef StructureErrorList As DatabaseStructureErrorList)

        ' do basic person tables' updates
        For i As Integer = StructureErrorList.Count To 1 Step -1

            If Not StructureErrorList.Item(i - 1).IsComplexError AndAlso _
                ((StructureErrorList.Item(i - 1).Table.Trim.ToLower = "asmenys" AndAlso _
                Not StructureErrorList.Item(i - 1).Description.ToLower.Contains("perteklinis")) OrElse _
                StructureErrorList.Item(i - 1).Table.Trim.ToLower = "persons_group" OrElse _
                StructureErrorList.Item(i - 1).Table.ToLower = "persons_group_assignments") Then

                StructureErrorList.Item(i - 1).Update(StructureErrorList)
                StructureErrorList.Item(i - 1).IsChecked = False
            End If

        Next

        Using transaction As New SqlTransaction

            Try

                ' set default language code, currency code, insert and update timestamps
                Dim myComm As New SQLCommand("UpgradePersonsTable1")
                myComm.Execute()

                ' Transfer SODRA code to specialized field
                myComm = New SQLCommand("UpgradePersonsTable2")
                myComm.Execute()

                ' Clear SODRA code from Vat field
                myComm = New SQLCommand("UpgradePersonsTable3")
                myComm.Execute()

                ' Transfer supplier code to specialized field
                myComm = New SQLCommand("UpgradePersonsTable4")
                myComm.Execute()

                ' Transfer person type from char field to TinyInt specialized fields
                myComm = New SQLCommand("UpgradePersonsTable5")
                myComm.Execute()

                ' Transfer client status within the persons with the same code
                myComm = New SQLCommand("UpgradePersonsTable6")
                myComm.Execute()

                ' Transfer supplier status within the persons with the same code
                myComm = New SQLCommand("UpgradePersonsTable7")
                myComm.Execute()

                ' Transfer worker status within the persons with the same code
                myComm = New SQLCommand("UpgradePersonsTable8")
                myComm.Execute()

                ' Transfer Vat code within the persons with the same code
                myComm = New SQLCommand("UpgradePersonsTable9")
                myComm.Execute()

                ' Duplicate persons id's are removed in favour to min ID

                ' Remove duplicate persons id's from apyskaitos
                myComm = New SQLCommand("UpgradePersonsTable10")
                myComm.Execute()

                ' just in case take care that no null's and empty values are in bz table analitic field
                myComm = New SQLCommand("UpgradeBZTable2")
                myComm.Execute()

                ' Remove duplicate persons id's from bz
                myComm = New SQLCommand("UpgradePersonsTable11")
                myComm.Execute()

                ' Change null persons id's to 0 in bzdata
                myComm = New SQLCommand("UpgradePersonsTable12")
                myComm.Execute()

                ' Remove duplicate persons id's from bzdata
                myComm = New SQLCommand("UpgradePersonsTable13")
                myComm.Execute()

                ' Remove duplicate persons id's from d_avansai_d
                myComm = New SQLCommand("UpgradePersonsTable14")
                myComm.Execute()

                ' Remove duplicate persons id's from darbuotojai_d
                myComm = New SQLCommand("UpgradePersonsTable15")
                myComm.Execute()

                ' Remove duplicate persons id's from du_ziniarastis_d
                myComm = New SQLCommand("UpgradePersonsTable16")
                myComm.Execute()

                ' Remove duplicate persons id's from asmenys
                myComm = New SQLCommand("UpgradePersonsTable17")
                myComm.Execute()

                transaction.Commit()

            Catch ex As Exception
                transaction.SetNonSqlException(ex)
                Throw
            End Try

        End Using

    End Sub

    Private Sub UpgradeOldVersionLedger(ByRef StructureErrorList As DatabaseStructureErrorList)

        Using transaction As New SqlTransaction

            Try

                Dim myComm As New SQLCommand("UpgradeBZTable1")
                myComm.Execute()

                myComm = New SQLCommand("UpgradeBZTable2")
                myComm.Execute()

                For i As Integer = StructureErrorList.Count To 1 Step -1
                    If Not StructureErrorList.Item(i - 1).IsComplexError AndAlso _
                        StructureErrorList.Item(i - 1).Table.Trim.ToLower = "bz" Then
                        StructureErrorList.Item(i - 1).Update(StructureErrorList)
                        StructureErrorList.Item(i - 1).IsChecked = False
                    End If
                Next

                myComm = New SQLCommand("UpgradeBZTable3")
                myComm.Execute()

                myComm = New SQLCommand("UpgradeBZTable4")
                myComm.Execute()

                transaction.Commit()

            Catch ex As Exception
                transaction.SetNonSqlException(ex)
                Throw
            End Try

        End Using

    End Sub

    Private Sub UpgradeOldVersionRegionalContents(ByRef StructureErrorList As DatabaseStructureErrorList)

        For i As Integer = StructureErrorList.Count To 1 Step -1
            If Not StructureErrorList.Item(i - 1).IsComplexError AndAlso _
                StructureErrorList.Item(i - 1).Table.Trim.ToLower = "regionalcontents" Then

                StructureErrorList.Item(i - 1).Update(StructureErrorList)
                StructureErrorList.Item(i - 1).IsChecked = False

            End If
        Next

        Dim myComm As New SQLCommand("UpgradeOldVersionRegionalContents1")
        myComm.Execute()

    End Sub

    Private Sub UpgradeOldVersionRegionalPrices(ByRef StructureErrorList As DatabaseStructureErrorList)

        For i As Integer = StructureErrorList.Count To 1 Step -1
            If Not StructureErrorList.Item(i - 1).IsComplexError AndAlso _
                StructureErrorList.Item(i - 1).Table.Trim.ToLower = "regionalprices" Then

                StructureErrorList.Item(i - 1).Update(StructureErrorList)
                StructureErrorList.Item(i - 1).IsChecked = False

            End If
        Next

        Dim myComm As New SQLCommand("UpgradeOldVersionRegionalPrices1")
        myComm.Execute()

    End Sub

    Private Sub UpgradeOldVersionAdvanceReports(ByRef StructureErrorList As DatabaseStructureErrorList, _
        ByVal UpgradeAdvanceReportTable As Boolean, ByVal UpgradeAdvanceReportTableDetails As Boolean)

        For i As Integer = StructureErrorList.Count To 1 Step -1
            If Not StructureErrorList.Item(i - 1).IsComplexError AndAlso _
                ((StructureErrorList.Item(i - 1).Table.Trim.ToLower = "apyskaitos" _
                AndAlso StructureErrorList.Item(i - 1).Field.Trim.ToLower <> "pvmkor") _
                OrElse StructureErrorList.Item(i - 1).Table.Trim.ToLower = "advancereports") Then

                StructureErrorList.Item(i - 1).Update(StructureErrorList)
                StructureErrorList.Item(i - 1).IsChecked = False

            End If
        Next

        Using transaction As New SqlTransaction

            Try

                If UpgradeAdvanceReportTable Then
                    Dim myComm As New SQLCommand("UpgradeOldVersionAdvanceReports")
                    myComm.Execute()
                End If
                If UpgradeAdvanceReportTableDetails Then
                    Dim myComm As New SQLCommand("UpgradeOldVersionAdvanceReports2")
                    myComm.Execute()
                End If

                transaction.Commit()

            Catch ex As Exception
                transaction.SetNonSqlException(ex)
                Throw
            End Try

        End Using

    End Sub

    Private Sub UpgradeOldVersionCashAccounts(ByRef StructureErrorList As DatabaseStructureErrorList)

        For i As Integer = StructureErrorList.Count To 1 Step -1
            If Not StructureErrorList.Item(i - 1).IsComplexError AndAlso _
                (StructureErrorList.Item(i - 1).Table.Trim.ToLower = "cashaccounts" OrElse _
                StructureErrorList.Item(i - 1).Table.Trim.ToLower = "bankoperations" OrElse _
                StructureErrorList.Item(i - 1).Table.Trim.ToLower = "kio" OrElse _
                StructureErrorList.Item(i - 1).Table.Trim.ToLower = "kpo") Then

                StructureErrorList.Item(i - 1).Update(StructureErrorList)
                StructureErrorList.Item(i - 1).IsChecked = False

            End If
        Next

        Using transaction As New SqlTransaction

            Try

                Dim myComm As New SQLCommand("UpgradeCashAccounts")
                myComm.Execute()

                myComm = New SQLCommand("UpgradeBankOperations")
                myComm.Execute()

                myComm = New SQLCommand("UpgradeTillSpendingsOrders")
                myComm.Execute()

                myComm = New SQLCommand("UpgradeTillIncomeOrders")
                myComm.Execute()

                transaction.Commit()

            Catch ex As Exception
                transaction.SetNonSqlException(ex)
                Throw
            End Try

        End Using

    End Sub

    Private Sub UpgradeOldVersionOffsets(ByRef StructureErrorList As DatabaseStructureErrorList)

        For i As Integer = StructureErrorList.Count To 1 Step -1
            If Not StructureErrorList.Item(i - 1).IsComplexError AndAlso _
                StructureErrorList.Item(i - 1).Table.Trim.ToLower = "offsetitems" Then

                StructureErrorList.Item(i - 1).Update(StructureErrorList)
                StructureErrorList.Item(i - 1).IsChecked = False

            End If
        Next

        Dim myComm As New SQLCommand("UpgradeOldVersionOffsets")
        myComm.Execute()

    End Sub

    Private Sub UpgradeOldVersionInvoicesMade(ByRef StructureErrorList As DatabaseStructureErrorList)

        For i As Integer = StructureErrorList.Count To 1 Step -1
            If Not StructureErrorList.Item(i - 1).IsComplexError AndAlso _
                (StructureErrorList.Item(i - 1).Table.Trim.ToLower = "sfd" OrElse _
                StructureErrorList.Item(i - 1).Table.Trim.ToLower = "invoicesmade") _
                AndAlso StructureErrorList.Item(i - 1).Field.Trim.ToLower <> "s_sas" Then
                StructureErrorList.Item(i - 1).Update(StructureErrorList)
                StructureErrorList.Item(i - 1).IsChecked = False
            End If
        Next

        Using transaction As New SqlTransaction

            Try

                Dim myComm As New SQLCommand("UpgradeInvoiceMade1")
                myComm.Execute()

                myComm = New SQLCommand("UpgradeInvoiceMade2")
                myComm.Execute()

                myComm = New SQLCommand("UpgradeInvoiceMade3")
                myComm.Execute()

                transaction.Commit()

            Catch ex As Exception
                transaction.SetNonSqlException(ex)
                Throw
            End Try

        End Using

    End Sub

    Private Sub UpgradeOldVersionInvoicesReceived(ByRef StructureErrorList As DatabaseStructureErrorList)

        For i As Integer = StructureErrorList.Count To 1 Step -1
            If Not StructureErrorList.Item(i - 1).IsComplexError AndAlso _
                (StructureErrorList.Item(i - 1).Table.Trim.ToLower = "sfg" OrElse _
                StructureErrorList.Item(i - 1).Table.Trim.ToLower = "invoicesreceived") _
                AndAlso Not StructureErrorList.Item(i - 1).SqlStatementsToCorrect.Trim.ToLower.Contains("drop ") Then
                StructureErrorList.Item(i - 1).Update(StructureErrorList)
                StructureErrorList.Item(i - 1).IsChecked = False
            End If
        Next

        Using transaction As New SqlTransaction

            Try

                Dim myComm As New SQLCommand("UpgradeInvoiceReceived1")
                myComm.Execute()

                myComm = New SQLCommand("UpgradeInvoiceReceived2")
                myComm.Execute()

                myComm = New SQLCommand("UpgradeInvoiceReceived3")
                myComm.Execute()

                myComm = New SQLCommand("UpgradeInvoiceReceived4")
                myComm.Execute()

                myComm = New SQLCommand("UpgradeInvoiceReceived5")
                myComm.Execute()

                transaction.Commit()

            Catch ex As Exception
                transaction.SetNonSqlException(ex)
                Throw
            End Try

        End Using

    End Sub

    Private Sub UpgradeOldVersionSettings(ByRef StructureErrorList As DatabaseStructureErrorList)

        For i As Integer = StructureErrorList.Count To 1 Step -1
            If Not StructureErrorList.Item(i - 1).IsComplexError AndAlso _
                (StructureErrorList.Item(i - 1).Table.Trim.ToLower = "companyaccounts" OrElse _
                StructureErrorList.Item(i - 1).Table.Trim.ToLower = "companyrates")  Then
                StructureErrorList.Item(i - 1).Update(StructureErrorList)
                StructureErrorList.Item(i - 1).IsChecked = False
            End If
        Next

        Dim myComm As New SQLCommand("FetchOldVersionSettings")

        Using myData As DataTable = myComm.Fetch

            If Not myData.Rows.Count > 0 Then Exit Sub

            Using transaction As New SqlTransaction

                Try

                    myComm = New SQLCommand("InsertCompanyAccount")
                    myComm.AddParam("?AA", Utilities.ConvertDatabaseID(General.DefaultAccountType.Till))
                    myComm.AddParam("?AB", CLongSafe(myData.Rows(0).Item(0), 0))

                    myComm.Execute()

                    myComm = New SQLCommand("InsertCompanyAccount")
                    myComm.AddParam("?AA", Utilities.ConvertDatabaseID(General.DefaultAccountType.Buyers))
                    myComm.AddParam("?AB", CLongSafe(myData.Rows(0).Item(1), 0))

                    myComm.Execute()

                    myComm = New SQLCommand("InsertCompanyAccount")
                    myComm.AddParam("?AA", Utilities.ConvertDatabaseID(General.DefaultAccountType.Suppliers))
                    myComm.AddParam("?AB", CLongSafe(myData.Rows(0).Item(2), 0))

                    myComm.Execute()

                    myComm = New SQLCommand("InsertCompanyAccount")
                    myComm.AddParam("?AA", Utilities.ConvertDatabaseID(General.DefaultAccountType.VatReceivable))
                    myComm.AddParam("?AB", CLongSafe(myData.Rows(0).Item(3), 0))

                    myComm.Execute()

                    myComm = New SQLCommand("InsertCompanyAccount")
                    myComm.AddParam("?AA", Utilities.ConvertDatabaseID(General.DefaultAccountType.WageGpmPayable))
                    myComm.AddParam("?AB", CLongSafe(myData.Rows(0).Item(4), 0))

                    myComm.Execute()

                    myComm = New SQLCommand("InsertCompanyAccount")
                    myComm.AddParam("?AA", Utilities.ConvertDatabaseID(General.DefaultAccountType.WageSodraPayable))
                    myComm.AddParam("?AB", CLongSafe(myData.Rows(0).Item(5), 0))

                    myComm.Execute()

                    myComm = New SQLCommand("InsertCompanyAccount")
                    myComm.AddParam("?AA", Utilities.ConvertDatabaseID(General.DefaultAccountType.WageGuaranteeFundPayable))
                    myComm.AddParam("?AB", CLongSafe(myData.Rows(0).Item(6), 0))

                    myComm.Execute()

                    myComm = New SQLCommand("InsertCompanyAccount")
                    myComm.AddParam("?AA", Utilities.ConvertDatabaseID(General.DefaultAccountType.WagePayable))
                    myComm.AddParam("?AB", CLongSafe(myData.Rows(0).Item(7), 0))

                    myComm.Execute()

                    myComm = New SQLCommand("InsertCompanyAccount")
                    myComm.AddParam("?AA", Utilities.ConvertDatabaseID(General.DefaultAccountType.WageImprestPayable))
                    myComm.AddParam("?AB", CLongSafe(myData.Rows(0).Item(8), 0))

                    myComm.Execute()

                    myComm = New SQLCommand("InsertCompanyAccount")
                    myComm.AddParam("?AA", Utilities.ConvertDatabaseID(General.DefaultAccountType.WageWithdraw))
                    myComm.AddParam("?AB", CLongSafe(myData.Rows(0).Item(9), 0))

                    myComm.Execute()

                    myComm = New SQLCommand("InsertCompanyAccount")
                    myComm.AddParam("?AA", Utilities.ConvertDatabaseID(General.DefaultAccountType.Bank))
                    myComm.AddParam("?AB", CLongSafe(myData.Rows(0).Item(10), 0))

                    myComm.Execute()

                    myComm = New SQLCommand("InsertCompanyAccount")
                    myComm.AddParam("?AA", Utilities.ConvertDatabaseID(General.DefaultAccountType.WagePsdPayable))
                    myComm.AddParam("?AB", CLongSafe(myData.Rows(0).Item(11), 0))

                    myComm.Execute()

                    myComm = New SQLCommand("InsertCompanyAccount")
                    myComm.AddParam("?AA", Utilities.ConvertDatabaseID(General.DefaultAccountType.WagePsdPayableToVMI))
                    myComm.AddParam("?AB", CLongSafe(myData.Rows(0).Item(12), 0))

                    myComm.Execute()


                    myComm = New SQLCommand("InsertCompanyRate")
                    myComm.AddParam("?AA", Utilities.ConvertDatabaseID(General.DefaultRateType.WageRateNight))
                    myComm.AddParam("?AB", CDblSafe(myData.Rows(0).Item(13), 2, 0))

                    myComm.Execute()

                    myComm = New SQLCommand("InsertCompanyRate")
                    myComm.AddParam("?AA", Utilities.ConvertDatabaseID(General.DefaultRateType.WageRateOvertime))
                    myComm.AddParam("?AB", CDblSafe(myData.Rows(0).Item(13), 2, 0))

                    myComm.Execute()

                    myComm = New SQLCommand("InsertCompanyRate")
                    myComm.AddParam("?AA", Utilities.ConvertDatabaseID(General.DefaultRateType.WageRateRestTime))
                    myComm.AddParam("?AB", CDblSafe(myData.Rows(0).Item(14), 2, 0))

                    myComm.Execute()

                    myComm = New SQLCommand("InsertCompanyRate")
                    myComm.AddParam("?AA", Utilities.ConvertDatabaseID(General.DefaultRateType.WageRatePublicHolidays))
                    myComm.AddParam("?AB", CDblSafe(myData.Rows(0).Item(14), 2, 0))

                    myComm.Execute()

                    myComm = New SQLCommand("InsertCompanyRate")
                    myComm.AddParam("?AA", Utilities.ConvertDatabaseID(General.DefaultRateType.WageRateDeviations))
                    myComm.AddParam("?AB", CDblSafe(myData.Rows(0).Item(15), 2, 0))

                    myComm.Execute()

                    myComm = New SQLCommand("InsertCompanyRate")
                    myComm.AddParam("?AA", Utilities.ConvertDatabaseID(General.DefaultRateType.WageRateSickLeave))
                    myComm.AddParam("?AB", CDblSafe(myData.Rows(0).Item(16), 2, 0))

                    myComm.Execute()

                    transaction.Commit()

                Catch ex As Exception
                    transaction.SetNonSqlException(ex)
                    Throw
                End Try

            End Using

        End Using

    End Sub

    Private Sub UpgradeOldVersionGoods(ByRef StructureErrorList As DatabaseStructureErrorList)

        For i As Integer = StructureErrorList.Count To 1 Step -1
            If Not StructureErrorList.Item(i - 1).IsComplexError AndAlso _
                (StructureErrorList.Item(i - 1).Table.Trim.ToLower = "consignmentdiscards" OrElse _
                StructureErrorList.Item(i - 1).Table.Trim.ToLower = "consignments" OrElse _
                StructureErrorList.Item(i - 1).Table.Trim.ToLower = "goods" OrElse _
                StructureErrorList.Item(i - 1).Table.Trim.ToLower = "goodsaccounts" OrElse _
                StructureErrorList.Item(i - 1).Table.Trim.ToLower = "goodscomplexoperations" OrElse _
                StructureErrorList.Item(i - 1).Table.Trim.ToLower = "goodsoperations" OrElse _
                StructureErrorList.Item(i - 1).Table.Trim.ToLower = "goodsvaluationmethods" OrElse _
                StructureErrorList.Item(i - 1).Table.Trim.ToLower = "kalkuliac" OrElse _
                StructureErrorList.Item(i - 1).Table.Trim.ToLower = "kalkuliac_d" OrElse _
                StructureErrorList.Item(i - 1).Table.Trim.ToLower = "warehouses") Then
                StructureErrorList.Item(i - 1).Update(StructureErrorList)
                StructureErrorList.Item(i - 1).IsChecked = False
            End If
        Next

        'DatabaseAccess.TransactionBegin()

        'Dim myComm As New SQLCommand("UpgradeGoods1")
        'myComm.Execute()

        'myComm = New SQLCommand("UpgradeGoods2")
        'myComm.Execute()

        'myComm = New SQLCommand("UpgradeGoods3")
        'myComm.Execute()

        'DatabaseAccess.TransactionCommit()

    End Sub

    Private Sub UpgradeOldVersionAssets(ByRef StructureErrorList As DatabaseStructureErrorList)

        Dim LongTermAssetOperationList As New List(Of OldLongTermOperationAssetInfoList)
        Dim myComm As New SQLCommand("FetchOldFormatLongTermAssetList")
        Using myData As DataTable = myComm.Fetch
            For Each dr As DataRow In myData.Rows
                LongTermAssetOperationList.Add(New OldLongTermOperationAssetInfoList(dr))
            Next
        End Using

        Using transaction As New SqlTransaction

            Try

                myComm = New SQLCommand("UpgradeAssetsTable1")
                myComm.Execute()
                myComm = New SQLCommand("UpgradeAssetsTable2")
                myComm.Execute()

                For i As Integer = StructureErrorList.Count To 1 Step -1
                    If Not StructureErrorList.Item(i - 1).IsComplexError AndAlso _
                        (StructureErrorList.Item(i - 1).Table.Trim.ToLower = "turtas" _
                        OrElse StructureErrorList.Item(i - 1).Table.Trim.ToLower = "turtas_op") Then

                        StructureErrorList.Item(i - 1).Update(StructureErrorList)
                        StructureErrorList.Item(i - 1).IsChecked = False

                    End If
                Next

                myComm = New SQLCommand("UpgradeAssetsTable3")
                myComm.Execute()

                For Each AssetItem As OldLongTermOperationAssetInfoList In LongTermAssetOperationList
                    For Each OperationItem As OldLongTermOperationAssetInfo In AssetItem
                        OperationItem.Update()
                    Next
                Next

                transaction.Commit()

            Catch ex As Exception
                transaction.SetNonSqlException(ex)
                Throw
            End Try

        End Using

    End Sub

    Private Sub UpgradeOldVersionWages(ByRef StructureErrorList As DatabaseStructureErrorList)

        For i As Integer = StructureErrorList.Count To 1 Step -1
            If Not StructureErrorList.Item(i - 1).IsComplexError AndAlso _
                StructureErrorList.Item(i - 1).Table.Trim.ToLower = "du_ziniarastis_d" Then
                StructureErrorList.Item(i - 1).Update(StructureErrorList)
                StructureErrorList.Item(i - 1).IsChecked = False
            End If
        Next

        Using transaction As New SqlTransaction

            Try

                Dim myComm As New SQLCommand("UpgradeWages1")
                myComm.Execute()

                myComm = New SQLCommand("UpgradeWages2")
                myComm.Execute()

                myComm = New SQLCommand("UpgradeWages3")
                myComm.Execute()

                myComm = New SQLCommand("UpgradeWages4")
                myComm.Execute()

                myComm = New SQLCommand("UpgradeWages5")
                myComm.Execute()

                transaction.Commit()

            Catch ex As Exception
                transaction.SetNonSqlException(ex)
                Throw
            End Try

        End Using

    End Sub

    Private Sub UpgradeOldVersionAccounts(ByRef StructureErrorList As DatabaseStructureErrorList)

        For i As Integer = StructureErrorList.Count To 1 Step -1
            If (Not StructureErrorList.Item(i - 1).IsComplexError AndAlso _
                StructureErrorList.Item(i - 1).Table.Trim.ToLower = "saskaitupl") OrElse _
                (StructureErrorList(i - 1).IsComplexError AndAlso _
                 StructureErrorList(i - 1).ComplexErrorCode.Trim.ToUpper _
                 = ErrorCode_OldVersionFinancialStatements.Trim.ToUpper) Then
                StructureErrorList.Item(i - 1).Update(StructureErrorList)
                StructureErrorList.Item(i - 1).IsChecked = False
            End If
        Next

        Using transaction As New SqlTransaction

            Try

                Dim myComm As New SQLCommand("UpgradeAccounts1")
                myComm.Execute()

                myComm = New SQLCommand("UpgradeAccounts2")
                myComm.Execute()

                transaction.Commit()

            Catch ex As Exception
                transaction.SetNonSqlException(ex)
                Throw
            End Try

        End Using

    End Sub

    Private Sub UpgradeOldVersionAssetOperationDocNo(ByRef StructureErrorList As DatabaseStructureErrorList)

        For i As Integer = StructureErrorList.Count To 1 Step -1
            If Not StructureErrorList.Item(i - 1).IsComplexError AndAlso _
                StructureErrorList.Item(i - 1).Table.Trim.ToLower = "turtas_op" AndAlso _
                StructureErrorList.Item(i - 1).Field.Trim.ToLower = "docno" Then
                StructureErrorList.Item(i - 1).Update(StructureErrorList)
                StructureErrorList.Item(i - 1).IsChecked = False
            End If
        Next

        Using transaction As New SqlTransaction

            Try

                Dim myComm As New SQLCommand("UpgradeAssetOperationDocNo1")
                myComm.Execute()

                myComm = New SQLCommand("UpgradeAssetOperationDocNo2")
                myComm.Execute()

                transaction.Commit()

            Catch ex As Exception
                transaction.SetNonSqlException(ex)
                Throw
            End Try

        End Using

    End Sub

    Private Class OldLongTermOperationAssetInfo

        Public Structure DateInterval
            Public DateFrom As Date
            Public DateTo As Date
            Public Sub New(ByVal nDateFrom As Date, ByVal nDateTo As Date)
                DateFrom = nDateFrom
                DateTo = nDateTo
            End Sub
        End Structure

        Public _ID As Integer
        Public _Type As LtaOperationType
        Public _Date As Date
        Public _Content As String
        Public _AccountCorresponding As Long = 0
        Public _JournalEntryID As Integer = 0
        Public _DocNo As String = ""
        Public _NewAmortizationPeriod As Integer = 0

        Public _UnitValueChange As Double = 0
        Public _TotalValueChange As Double = 0
        Public _AmmountChange As Integer = 0
        Public _RevaluedPortionUnitValueChange As Double = 0
        Public _RevaluedPortionTotalValueChange As Double = 0
        Public _AmortizationCalculatedForMonths As Integer = 0
        Public _AcquisitionAccountValueChange As Double = 0
        Public _AcquisitionAccountValueChangePerUnit As Double = 0
        Public _AmortizationAccountValueChange As Double = 0
        Public _AmortizationAccountValueChangePerUnit As Double = 0
        Public _ValueDecreaseAccountValueChange As Double = 0
        Public _ValueDecreaseAccountValueChangePerUnit As Double = 0

        Public Sub New(ByVal dr As DataRow, _
            ByRef nBeforeOperationAcquisitionAccountValue As Double, _
            ByRef nBeforeOperationAcquisitionAccountValuePerUnit As Double, _
            ByRef nBeforeOperationAmortizationAccountValue As Double, _
            ByRef nBeforeOperationAmortizationAccountValuePerUnit As Double, _
            ByRef nBeforeOperationValueDecreaseAccountValue As Double, _
            ByRef nBeforeOperationValueDecreaseAccountValuePerUnit As Double, _
            ByRef nBeforeOperationValue As Double, _
            ByRef nBeforeOperationValuePerUnit As Double, _
            ByRef nBeforeOperationAmmount As Integer, _
            ByRef nBeforeOperationUsagePeriods As List(Of DateInterval), _
            ByRef nBeforeOperationUsage As Boolean)

            _ID = CInt(dr.Item(0))
            _JournalEntryID = CIntSafe(dr.Item(1), 0)
            If CIntSafe(dr.Item(2), 0) > 0 Then
                _DocNo = CIntSafe(dr.Item(2), 0).ToString()
            End If
            _Content = dr.Item(3).ToString
            _Date = CDate(dr.Item(4))

            Dim typeCode As String = dr.Item(7).ToString.Trim.ToLower

            If typeCode = "amo" OrElse typeCode = "nur" Then
                _AccountCorresponding = CLongSafe(dr.Item(5), 0)
            End If

            Dim opValue As Double = CDblSafe(dr.Item(6), ROUNDUNITASSET, 0)

            If typeCode = "nau" AndAlso nBeforeOperationUsage Then
                _Type = LtaOperationType.UsingEnd
                nBeforeOperationUsagePeriods(nBeforeOperationUsagePeriods.Count - 1) = _
                    New DateInterval(nBeforeOperationUsagePeriods( _
                    nBeforeOperationUsagePeriods.Count - 1).DateFrom, _
                    New Date(_Date.Year, _Date.Month, Date.DaysInMonth(_Date.Year, _Date.Month)))
                nBeforeOperationUsage = Not nBeforeOperationUsage

            ElseIf typeCode = "nau" AndAlso Not nBeforeOperationUsage Then
                _Type = LtaOperationType.UsingStart
                If _Date.Month = 12 Then
                    nBeforeOperationUsagePeriods.Add(New DateInterval( _
                        New Date(_Date.Year + 1, 1, 1), Date.MaxValue))
                Else
                    nBeforeOperationUsagePeriods.Add(New DateInterval( _
                        New Date(_Date.Year, _Date.Month + 1, 1), Date.MaxValue))
                End If
                nBeforeOperationUsage = Not nBeforeOperationUsage

            ElseIf typeCode = "alk" Then
                _Type = LtaOperationType.AmortizationPeriod
                _NewAmortizationPeriod = CInt(opValue)

            ElseIf typeCode = "amo" Then
                _Type = LtaOperationType.Amortization
                _TotalValueChange = -CRound(opValue * nBeforeOperationAmmount)
                _UnitValueChange = -CRound(opValue, ROUNDUNITASSET)
                _AmortizationAccountValueChange = -_TotalValueChange
                _AmortizationAccountValueChangePerUnit = -_UnitValueChange

                If nBeforeOperationUsagePeriods.Count > 0 AndAlso _
                    nBeforeOperationUsagePeriods(nBeforeOperationUsagePeriods.Count - 1). _
                    DateTo = Date.MaxValue Then
                    nBeforeOperationUsagePeriods(nBeforeOperationUsagePeriods.Count - 1) = _
                        New DateInterval(nBeforeOperationUsagePeriods( _
                        nBeforeOperationUsagePeriods.Count - 1).DateFrom, _
                        New Date(_Date.Year, _Date.Month, Date.DaysInMonth(_Date.Year, _Date.Month)))
                End If
                For i As Integer = 1 To nBeforeOperationUsagePeriods.Count
                    _AmortizationCalculatedForMonths = _AmortizationCalculatedForMonths + _
                        Assets.LongTermAssetAmortizationCalculation.DateDifferenceInMonths( _
                        nBeforeOperationUsagePeriods(i - 1).DateFrom, _
                        nBeforeOperationUsagePeriods(i - 1).DateTo, True, True)
                Next
                nBeforeOperationUsagePeriods.Clear()
                If nBeforeOperationUsage Then
                    If _Date.Month = 12 Then
                        nBeforeOperationUsagePeriods.Add(New DateInterval( _
                            New Date(_Date.Year + 1, 1, 1), Date.MaxValue))
                    Else
                        nBeforeOperationUsagePeriods.Add(New DateInterval( _
                            New Date(_Date.Year, _Date.Month + 1, 1), Date.MaxValue))
                    End If
                End If


            ElseIf typeCode = "per" OrElse typeCode = "nur" Then

                If typeCode = "per" Then
                    _Type = LtaOperationType.Transfer
                Else
                    _Type = LtaOperationType.Discard
                End If

                _AmmountChange = CInt(opValue)
                If _AmmountChange = nBeforeOperationAmmount Then
                    _TotalValueChange = -nBeforeOperationValue
                    _AcquisitionAccountValueChange = -nBeforeOperationAcquisitionAccountValue
                    _AmortizationAccountValueChange = -nBeforeOperationAmortizationAccountValue
                    _ValueDecreaseAccountValueChange = -nBeforeOperationValueDecreaseAccountValue
                    _RevaluedPortionTotalValueChange = -nBeforeOperationValueDecreaseAccountValue
                Else
                    _TotalValueChange = -CRound(nBeforeOperationValuePerUnit * _AmmountChange)
                    _AcquisitionAccountValueChange = _
                        -CRound(nBeforeOperationAcquisitionAccountValuePerUnit * _AmmountChange)
                    _AmortizationAccountValueChange = _
                        -CRound(nBeforeOperationAmortizationAccountValuePerUnit * _AmmountChange)
                    _ValueDecreaseAccountValueChange = _
                        -CRound(nBeforeOperationValueDecreaseAccountValuePerUnit * _AmmountChange)
                    _RevaluedPortionTotalValueChange = _
                        -CRound(nBeforeOperationValueDecreaseAccountValuePerUnit * _AmmountChange)
                End If


            ElseIf typeCode = "ver" Then

                If CRound(nBeforeOperationValuePerUnit) > CRound(opValue) Then
                    _Type = LtaOperationType.ValueChange
                    _TotalValueChange = -CRound(nBeforeOperationValue - (opValue * nBeforeOperationAmmount))
                    _UnitValueChange = -CRound(nBeforeOperationValuePerUnit - opValue, 4)
                    _ValueDecreaseAccountValueChange = -_TotalValueChange
                    _ValueDecreaseAccountValueChangePerUnit = -_UnitValueChange
                    _RevaluedPortionTotalValueChange = _TotalValueChange
                    _RevaluedPortionUnitValueChange = _UnitValueChange
                Else
                    _Type = LtaOperationType.AcquisitionValueIncrease
                    _TotalValueChange = CRound((opValue * nBeforeOperationAmmount) - nBeforeOperationValue)
                    _UnitValueChange = CRound(opValue - nBeforeOperationValuePerUnit, 4)
                    _AcquisitionAccountValueChange = _TotalValueChange
                    _AcquisitionAccountValueChangePerUnit = _UnitValueChange
                End If

            End If

            nBeforeOperationAcquisitionAccountValue = _
                nBeforeOperationAcquisitionAccountValue + _AcquisitionAccountValueChange
            nBeforeOperationAcquisitionAccountValuePerUnit = _
                nBeforeOperationAcquisitionAccountValuePerUnit + _AcquisitionAccountValueChangePerUnit
            nBeforeOperationAmortizationAccountValue = _
                nBeforeOperationAmortizationAccountValue + _AmortizationAccountValueChange
            nBeforeOperationAmortizationAccountValuePerUnit = _
                nBeforeOperationAmortizationAccountValuePerUnit + _AmortizationAccountValueChangePerUnit
            nBeforeOperationValueDecreaseAccountValue = _
                nBeforeOperationValueDecreaseAccountValue + _ValueDecreaseAccountValueChange
            nBeforeOperationValueDecreaseAccountValuePerUnit = _
                nBeforeOperationValueDecreaseAccountValuePerUnit + _ValueDecreaseAccountValueChangePerUnit
            nBeforeOperationValue = nBeforeOperationValue + _TotalValueChange
            nBeforeOperationValuePerUnit = nBeforeOperationValuePerUnit + _UnitValueChange
            nBeforeOperationAmmount = nBeforeOperationAmmount - _AmmountChange

        End Sub

        Public Sub Update()

            Dim myComm As New SQLCommand("UpgradeAssetsOperationsTable")
            myComm.AddParam("?OD", _ID)
            myComm.AddParam("?DK", Utilities.ConvertDatabaseCharID(_Type))
            myComm.AddParam("?DT", _Date.Date)
            If _Type <> LtaOperationType.AmortizationPeriod AndAlso _
                _Type <> LtaOperationType.UsingEnd AndAlso _Type <> LtaOperationType.UsingStart Then
                myComm.AddParam("?JD", _JournalEntryID)
            Else
                myComm.AddParam("?JD", 0)
            End If
            myComm.AddParam("?CN", _Content.Trim)
            If _Type = LtaOperationType.AccountChange OrElse _Type = LtaOperationType.Amortization _
                OrElse _Type = LtaOperationType.Discard Then
                myComm.AddParam("?AC", _AccountCorresponding)
            Else
                myComm.AddParam("?AC", 0)
            End If
            If _Type = LtaOperationType.Discard OrElse _Type = LtaOperationType.UsingEnd _
                OrElse _Type = LtaOperationType.UsingStart Then
                myComm.AddParam("?NM", _DocNo)
            Else
                myComm.AddParam("?NM", 0)
            End If
            If _Type = LtaOperationType.AcquisitionValueIncrease OrElse _Type = LtaOperationType.Amortization _
                OrElse _Type = LtaOperationType.ValueChange Then
                myComm.AddParam("?UV", CRound(_UnitValueChange, ROUNDUNITASSET))
            Else
                myComm.AddParam("?UV", 0)
            End If
            If _Type = LtaOperationType.Discard OrElse _Type = LtaOperationType.Transfer Then
                myComm.AddParam("?AM", _AmmountChange)
            Else
                myComm.AddParam("?AM", 0)
            End If
            If _Type <> LtaOperationType.AccountChange AndAlso _Type <> LtaOperationType.AmortizationPeriod _
                AndAlso _Type <> LtaOperationType.UsingEnd AndAlso _Type <> LtaOperationType.UsingStart Then
                myComm.AddParam("?TV", CRound(_TotalValueChange))
            Else
                myComm.AddParam("?TV", 0)
            End If
            If _Type = LtaOperationType.AmortizationPeriod Then
                myComm.AddParam("?AP", _NewAmortizationPeriod)
            Else
                myComm.AddParam("?AP", 0)
            End If
            If _Type = LtaOperationType.Amortization Then
                myComm.AddParam("?UT", _AmortizationCalculatedForMonths)
            Else
                myComm.AddParam("?UT", 0)
            End If
            If _Type = LtaOperationType.Amortization OrElse _Type = LtaOperationType.ValueChange Then
                myComm.AddParam("?RU", CRound(_RevaluedPortionUnitValueChange, ROUNDUNITASSET))
            Else
                myComm.AddParam("?RU", 0)
            End If
            If _Type = LtaOperationType.Amortization OrElse _Type = LtaOperationType.ValueChange _
                OrElse _Type = LtaOperationType.Discard OrElse _Type = LtaOperationType.Transfer Then
                myComm.AddParam("?RT", CRound(_RevaluedPortionTotalValueChange))
            Else
                myComm.AddParam("?RT", 0)
            End If
            myComm.AddParam("?DA", CRound(_AcquisitionAccountValueChange))
            myComm.AddParam("?DB", CRound(_AcquisitionAccountValueChangePerUnit, ROUNDUNITASSET))
            myComm.AddParam("?DC", CRound(_AmortizationAccountValueChange))
            myComm.AddParam("?DE", CRound(_AmortizationAccountValueChangePerUnit, ROUNDUNITASSET))
            myComm.AddParam("?DF", CRound(_ValueDecreaseAccountValueChange))
            myComm.AddParam("?DG", CRound(_ValueDecreaseAccountValueChangePerUnit, ROUNDUNITASSET))

            myComm.Execute()

        End Sub

    End Class

    Private Class OldLongTermOperationAssetInfoList
        Inherits List(Of OldLongTermOperationAssetInfo)

        Public Sub New(ByVal dr As DataRow)

            Dim nBeforeOperationAcquisitionAccountValue As Double = _
                CRound(CDbl(dr.Item(1)) * CDbl(dr.Item(2)))
            Dim nBeforeOperationAcquisitionAccountValuePerUnit As Double = _
                CRound(CDbl(dr.Item(1)), 4)
            Dim nBeforeOperationAmortizationAccountValue As Double = _
                CRound(CDbl(dr.Item(3)))
            Dim nBeforeOperationAmortizationAccountValuePerUnit As Double = _
                CRound(CDbl(dr.Item(3)) / CDbl(dr.Item(2)), 4)
            Dim nBeforeOperationValueDecreaseAccountValue As Double = 0
            Dim nBeforeOperationValueDecreaseAccountValuePerUnit As Double = 0
            Dim nBeforeOperationValue As Double = CRound(nBeforeOperationAcquisitionAccountValue _
                - nBeforeOperationAmortizationAccountValue)
            Dim nBeforeOperationValuePerUnit As Double = _
                CRound(nBeforeOperationAcquisitionAccountValuePerUnit _
                - nBeforeOperationAmortizationAccountValuePerUnit)
            Dim nBeforeOperationAmmount As Integer = CInt(dr.Item(2))
            Dim nBeforeOperationUsagePeriods As New List(Of OldLongTermOperationAssetInfo.DateInterval)
            Dim nBeforeOperationUsage As Boolean = False

            Dim myComm As New SQLCommand("FetchOldFormatLongTermOperationList")
            myComm.AddParam("?AD", CInt(dr.Item(0)))

            Using myData As DataTable = myComm.Fetch
                For Each dg As DataRow In myData.Rows
                    Add(New OldLongTermOperationAssetInfo(dg, nBeforeOperationAcquisitionAccountValue, _
                        nBeforeOperationAcquisitionAccountValuePerUnit, _
                        nBeforeOperationAmortizationAccountValue, _
                        nBeforeOperationAmortizationAccountValuePerUnit, _
                        nBeforeOperationValueDecreaseAccountValue, _
                        nBeforeOperationValueDecreaseAccountValuePerUnit, _
                        nBeforeOperationValue, nBeforeOperationValuePerUnit, nBeforeOperationAmmount, _
                        nBeforeOperationUsagePeriods, nBeforeOperationUsage))
                Next
            End Using

        End Sub

    End Class


    Private Sub RepairCompanyDataMissing(ByRef StructureErrorList As DatabaseStructureErrorList)

        For i As Integer = StructureErrorList.Count To 1 Step -1
            If Not StructureErrorList.Item(i - 1).IsComplexError AndAlso _
                StructureErrorList.Item(i - 1).Table.Trim.ToLower = "imone" Then

                StructureErrorList.Item(i - 1).Update(StructureErrorList)
                StructureErrorList.Item(i - 1).IsChecked = False

            End If
        Next

        Dim myComm As New SQLCommand("CompanyRowExists")
        Using myData As DataTable = myComm.Fetch
            If myData.Rows.Count < 1 OrElse CIntSafe(myData.Rows(0).Item(0), 0) < 1 Then

                myComm = New SQLCommand("CompanyInsert")
                myComm.AddParam("?AA", "nežinomas")
                myComm.AddParam("?AB", "nežinomas")
                myComm.AddParam("?AC", "")
                myComm.AddParam("?AD", "")
                myComm.AddParam("?AE", "")
                myComm.AddParam("?AF", "")
                myComm.AddParam("?AG", Nothing, GetType(Byte()))
                myComm.AddParam("?AH", 0)
                myComm.AddParam("?AI", 0)
                myComm.AddParam("?AJ", "")
                myComm.AddParam("?AK", "")
                myComm.AddParam("?AL", 1)
                myComm.AddParam("?AM", 0)
                myComm.AddParam("?AN", 2)
                myComm.AddParam("?AO", 0)
                myComm.AddParam("?AQ", 3)
                myComm.AddParam("?AP", 0)
                myComm.AddParam("?AR", 4)
                myComm.AddParam("?AT", 0)
                myComm.AddParam("?AU", 5)
                myComm.AddParam("?AV", 0)
                myComm.AddParam("?AZ", 6)
                myComm.AddParam("?AW", 0)
                myComm.AddParam("?BA", "")
                myComm.AddParam("?BJ", "")
                myComm.AddParam("?BM", 0)
                myComm.AddParam("?BN", 0)
                myComm.AddParam("?BQ", "")
                myComm.AddParam("?BP", "")
                myComm.AddParam("?BB", "")
                myComm.AddParam("?BC", "")
                myComm.AddParam("?BD", "")
                myComm.AddParam("?BE", "")
                myComm.AddParam("?BF", "")
                myComm.AddParam("?BG", "")
                myComm.AddParam("?BH", "")
                myComm.AddParam("?BI", "")
                myComm.AddParam("?BK", "")
                myComm.AddParam("?BL", "")
                myComm.AddParam("?BO", "EUR")

                myComm.Execute()

            End If

        End Using

    End Sub

    Private Sub RepairInvalidSerialCodes(ByRef StructureErrorList As DatabaseStructureErrorList)

        Dim myComm As New SQLCommand("RepairOldVersionSerialCodes")
        myComm.Execute()

    End Sub

    Private Sub RepairOrphanEntries(ByRef StructureErrorList As DatabaseStructureErrorList)

        Dim ids As New List(Of Long)

        For Each err As DatabaseStructureError In StructureErrorList
            If err.ComplexErrorCode.Trim.ToUpper() = ErrorCode_OrphanEntries Then

                For Each s As String In err.SqlStatementsToCorrect.Split(New String() {","}, _
                    StringSplitOptions.RemoveEmptyEntries)

                    Dim id As Long = 0
                    Long.TryParse(s, id)
                    If id > 0 Then

                        Dim myComm As New SQLCommand("DeleteBookEntry")
                        myComm.AddParam("?BD", id)
                        myComm.Execute()

                    End If

                Next

                Exit For

            End If
        Next

    End Sub

End Class
