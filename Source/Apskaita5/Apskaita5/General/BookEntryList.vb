﻿Namespace General

    ''' <summary>
    ''' Represents a ledger transaction list grouped by <see cref="General.BookEntryList.Type">transaction type</see>.
    ''' </summary>
    ''' <remarks>Can only be used as a child object for parent object that allows manual entry of ledger transactions.
    ''' Data is stored in database table bzdata.</remarks>
    <Serializable()> _
    Public NotInheritable Class BookEntryList
        Inherits BusinessListBase(Of BookEntryList, BookEntry)

#Region " Business Methods "

        Private _Type As BookEntryType = BookEntryType.Debetas
        Private _FinancialDataCanChange As Boolean = True
        Private _FinancialDataCanChangeExplanation As String = ""

        ''' <summary>
        ''' <see cref="BookEntryType">Type</see> (credit/debit) of the transactions in the list.
        ''' </summary>
        Public ReadOnly Property [Type]() As BookEntryType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Type
            End Get
        End Property


        ''' <summary>
        ''' Returnes TRUE if the parent object allows financial changes in transactions due to business restrains.
        ''' </summary>
        Friend ReadOnly Property FinancialDataCanChange() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _FinancialDataCanChange
            End Get
        End Property

        ''' <summary>
        ''' If there are any business restrains, that wouldn't allow financial transaction data to be changed, 
        ''' returns human readable description of the business restrains.
        ''' </summary>
        Friend ReadOnly Property FinancialDataCanChangeExplanation() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _FinancialDataCanChangeExplanation
            End Get
        End Property


        Protected Overrides Function AddNewCore() As Object
            If Not _FinancialDataCanChange Then
                Throw New Exception(String.Format(My.Resources.General_BookEntryList_FinancialDataChangeDenied, _
                    vbCrLf, _FinancialDataCanChangeExplanation))
            End If
            Dim NewItem As BookEntry = BookEntry.NewBookEntry
            Me.Add(NewItem)
            Return NewItem
        End Function


        Public Function GetAllBrokenRules() As String
            If Me.IsValid Then Return ""
            Return GetAllBrokenRulesForList(Me)
        End Function

        Public Function GetAllWarnings() As String
            If Not HasWarnings() Then Return ""
            Return GetAllWarningsForList(Me)
        End Function

        Public Function HasWarnings() As Boolean
            For Each i As BookEntry In Me
                If i.BrokenRulesCollection.WarningCount > 0 Then Return True
            Next
            Return False
        End Function


        ''' <summary>
        ''' Gets a sum of all the BookEntry items' amounts in the list.
        ''' </summary>
        Public Function GetSum() As Double
            Dim result As Double = 0
            For Each b As BookEntry In Me
                result = CRound(result + b.Amount)
            Next
            Return result
        End Function

        ''' <summary>
        ''' Gets a sum of the BookEntry items' ammounts within certain account.
        ''' </summary>
        Public Function GetSumInAccount(ByVal nAccount As Long) As Double
            If Not nAccount > 0 Then Return 0
            Dim result As Double = 0
            For Each b As BookEntry In Me
                If b.Account = nAccount Then result = CRound(result + b.Amount)
            Next
            Return result
        End Function

        ''' <summary>
        ''' Silently (without raising events) removes entries 
        ''' that contain reserved (for programatic access only) accounts.
        ''' </summary>
        ''' <param name="reservedAccounts">Reserved (for programatic access only) accounts.</param>
        ''' <remarks></remarks>
        Public Sub RemoveReservedAccounts(ByVal ParamArray reservedAccounts As Long())

            If Me.Count < 1 OrElse reservedAccounts Is Nothing OrElse reservedAccounts.Length < 1 Then Exit Sub

            Me.RaiseListChangedEvents = False

            For i As Integer = Me.Count To 1 Step -1
                If Not Array.IndexOf(reservedAccounts, Me.Item(i - 1).Account) < 0 Then
                    Me.RemoveAt(i - 1)
                End If
            Next

            Me.RaiseListChangedEvents = True

        End Sub


        ''' <summary>
        ''' Aggregates book entries by Account and PersonInfo, 
        ''' i.e. entries with the same Account and PersonInfo are replaced by one entry with total ammount
        ''' </summary>
        Public Sub OptimizeEntries(ByVal RaiseResetBindings As Boolean)

            If Not _FinancialDataCanChange Then
                Throw New Exception(String.Format(My.Resources.General_BookEntryList_FinancialDataChangeDenied, _
                    vbCrLf, _FinancialDataCanChangeExplanation))
            End If

            Dim raiseListChangedEventsWasDisabled As Boolean = (Me.RaiseListChangedEvents = False)

            If Not raiseListChangedEventsWasDisabled Then Me.RaiseListChangedEvents = False

            For i As Integer = Me.Count To 2 Step -1
                For j As Integer = 1 To i - 1
                    If Me.Item(i - 1).Account = Me.Item(j - 1).Account AndAlso _
                        Me.Item(i - 1).Person = Me.Item(j - 1).Person Then

                        Me.Item(j - 1).Amount = CRound(Me.Item(j - 1).Amount + Me.Item(i - 1).Amount)
                        Me.RemoveAt(i - 1)
                        Exit For

                    End If
                Next
            Next

            If Not raiseListChangedEventsWasDisabled Then Me.RaiseListChangedEvents = True

            If RaiseResetBindings Then Me.ResetBindings()

        End Sub

        ''' <summary>
        ''' Clears the list and loads new BookEntry's by account ID's specified in <paramref name="AccountList">AccountList</paramref> as comma delimited list.
        ''' </summary>
        ''' <param name="accountList">A list of accounts' ID's.</param>
        ''' <param name="RaiseResetBindings">Wheather to invoke ResetBindings when the load is complete.</param>
        ''' <remarks></remarks>
        Friend Sub LoadBookEntryListFromTemplate(ByVal accountList As Long(), _
            ByVal RaiseResetBindings As Boolean)

            ' nothing to load
            If accountList Is Nothing OrElse accountList.Length < 1 Then Exit Sub

            If Not _FinancialDataCanChange Then
                Throw New Exception(String.Format(My.Resources.General_BookEntryList_FinancialDataChangeDenied, _
                    vbCrLf, _FinancialDataCanChangeExplanation))
            End If

            RaiseListChangedEvents = False

            Me.Clear()

            For Each s As Long In accountList
                Dim NewItem As BookEntry = BookEntry.NewBookEntry()
                NewItem.Account = s
                Add(NewItem)
            Next

            RaiseListChangedEvents = True

            If RaiseResetBindings Then Me.ResetBindings()

        End Sub

        ''' <summary>
        ''' Clears the list and loads new BookEntry's by internal transaction list.
        ''' </summary>
        ''' <param name="source">Comma delimited lists of accounts' ID's.</param>
        ''' <param name="nOptimizeEntries">Wheather to invoke <see cref="General.BookEntryList.OptimizeEntries">OptimizeEntries</see> when the load is complete.</param>
        ''' <remarks></remarks>
        Friend Sub LoadBookEntryListFromInternalList(ByVal source As BookEntryInternalList, _
            ByVal nOptimizeEntries As Boolean, ByVal suppressResetBindings As Boolean)

            If Not _FinancialDataCanChange Then
                Throw New Exception(String.Format(My.Resources.General_BookEntryList_FinancialDataChangeDenied, _
                    vbCrLf, _FinancialDataCanChangeExplanation))
            End If

            RaiseListChangedEvents = False

            Me.Clear()

            For Each i As BookEntryInternal In source
                If i.EntryType = _Type Then Add(BookEntry.NewBookEntry(i))
            Next

            If nOptimizeEntries Then
                OptimizeEntries(False)
            End If

            RaiseListChangedEvents = True

            If Not suppressResetBindings Then
                Me.ResetBindings()
            End If

        End Sub


        ''' <summary>
        ''' Gets a comma separated list of transaction accounts prefixed by transaction type letter.
        ''' </summary>
        Friend Function GetEntryListString() As String

            If Me.Count < 1 Then Return ""

            Dim typeChar As String
            If _Type = BookEntryType.Debetas Then
                typeChar = My.Resources.General_BookEntryList_CharForDebit
            Else
                typeChar = My.Resources.General_BookEntryList_CharForCredit
            End If

            Dim result As New List(Of String)

            For Each i As BookEntry In Me
                result.Add(String.Format("{0}{1}", typeChar, i.Account.ToString))
            Next

            Return String.Join(", ", result.ToArray)

        End Function


        ''' <summary>
        ''' Adds BookEntry's from paste string.
        ''' </summary>
        ''' <param name="pasteString">String containing transaction data. 
        ''' Lines should be delimited by CrLf, fields should be delimited by Tab.</param>
        ''' <param name="overwrite">Wheather to clear the current items.</param>
        Friend Sub Paste(ByVal pasteString As String, ByVal overwrite As Boolean)

            If StringIsNullOrEmpty(pasteString.Trim) Then
                Throw New Exception(My.Resources.General_BookEntryList_ClipBoardEmpty)
            End If

            Dim accountsInfo As AccountInfoList = Nothing
            Try
                accountsInfo = AccountInfoList.GetList
            Catch ex As Exception
                Throw New Exception(String.Format(My.Resources.General_BookEntryList_FailedToFetchAccountInfoList, _
                    ex.Message), ex)
            End Try

            Dim personsInfo As PersonInfoList = Nothing
            Try
                personsInfo = PersonInfoList.GetList
            Catch ex As Exception
                Throw New Exception(String.Format(My.Resources.General_BookEntryList_FailedToFetchPersonInfoList, _
                    ex.Message), ex)
            End Try

            If accountsInfo.Count < 1 Then
                Throw New Exception(My.Resources.General_BookEntryList_AccountListEmpty)
            End If

            RaiseListChangedEvents = False

            If overwrite Then Me.Clear()

            For Each dr As String In pasteString.Split(New String() {vbCrLf}, StringSplitOptions.RemoveEmptyEntries)
                Add(BookEntry.NewBookEntry(dr, personsInfo, accountsInfo))
            Next

            RaiseListChangedEvents = True

            Me.ResetBindings()

        End Sub


        Public Overrides Function ToString() As String

            Dim result As New List(Of String)

            Dim accountPrefix As String = ""
            If _Type = BookEntryType.Debetas Then
                accountPrefix = My.Resources.General_BookEntryList_CharForDebit
            Else
                accountPrefix = My.Resources.General_BookEntryList_CharForCredit
            End If

            For Each c As BookEntry In Me
                result.Add(c.ToString(accountPrefix))
            Next

            Return String.Join(", ", result.ToArray)

        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets as new BookEntryList of type <paramref name="entryType">entryType</paramref>.
        ''' </summary>
        ''' <param name="entryType"><see cref="BookEntryType">Type</see> of the transactions in the list.</param>
        Friend Shared Function NewBookEntryList(ByVal entryType As BookEntryType) As BookEntryList
            Dim result As BookEntryList = New BookEntryList
            result._Type = entryType
            Return result
        End Function

        ''' <summary>
        ''' Gets a BookEntryList from database.
        ''' </summary>
        ''' <param name="myData">DataTable containing transaction data.</param>
        ''' <param name="entryType"><see cref="BookEntryType">Type</see> of the transactions in the list.</param>
        ''' <param name="financialDataCanChange">Whether the parent denies financial changes of the list.</param>
        ''' <param name="financialDataCanChangeExplanation">Human readable explanation why the parent denies financial changes of the list.</param>
        ''' <param name="accountsToSkip">Transactions' accounts that should not be included in the list.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Friend Shared Function GetBookEntryList(ByVal myData As DataTable, _
            ByVal entryType As BookEntryType, ByVal financialDataCanChange As Boolean, _
            ByVal financialDataCanChangeExplanation As String, _
            ByVal ParamArray accountsToSkip As Long()) As BookEntryList
            Return New BookEntryList(myData, entryType, financialDataCanChange, _
                financialDataCanChangeExplanation, accountsToSkip)
        End Function

        ''' <summary>
        ''' Gets a BookEntryList of the specified type from a database by a journal entry ID.
        ''' </summary>
        ''' <param name="journalEntryID">An ID of a journal entry for which to fetch the BookEntryList.</param>
        ''' <param name="entryType">Required type of the BookEntryList.</param>
        ''' <param name="chronologicValidator">Chronologic validator of the parent object.</param>
        ''' <param name="accountsToSkip">List of accounts that should not be included in the BookEntryList.</param>
        ''' <remarks></remarks>
        Friend Shared Function GetBookEntryList(ByVal journalEntryID As Integer, _
            ByVal entryType As BookEntryType, ByVal chronologicValidator As IChronologicValidator, _
            ByVal ParamArray accountsToSkip As Long()) As BookEntryList

            If chronologicValidator Is Nothing Then
                chronologicValidator = EmptyChronologicValidator.NewEmptyChronologicValidator( _
                    "", Nothing)
            End If

            Return New BookEntryList(journalEntryID, entryType, chronologicValidator, accountsToSkip)

        End Function

        ''' <summary>
        ''' Only for object TransferOfBalance.
        ''' </summary>
        ''' <remarks>Only for object TransferOfBalance.</remarks>
        Friend Shared Function GetBookEntryList(ByVal source As BookEntryInternalList, _
            ByVal entryType As BookEntryType, ByVal markItemsAsOld As Boolean, _
            ByVal financialDataCanChange As Boolean, ByVal financialDataCanChangeExplanation As String, _
            ByVal ParamArray accountsToSkip As Long()) As BookEntryList
            Return New BookEntryList(source, entryType, markItemsAsOld, _
                financialDataCanChange, financialDataCanChangeExplanation, accountsToSkip)
        End Function



        Private Sub New()
            ' require use of factory methods
            MarkAsChild()
            Me.AllowEdit = True
            Me.AllowNew = True
            Me.AllowRemove = True
        End Sub


        Private Sub New(ByVal myData As DataTable, ByVal entryType As BookEntryType, _
            ByVal nFinancialDataCanChange As Boolean, ByVal nFinancialDataCanChangeExplanation As String, _
            ByVal accountsToSkip As Long())
            ' require use of factory methods
            MarkAsChild()
            Fetch(myData, entryType, nFinancialDataCanChange, nFinancialDataCanChangeExplanation, accountsToSkip)
            Me.AllowEdit = nFinancialDataCanChange
            Me.AllowNew = nFinancialDataCanChange
            Me.AllowRemove = nFinancialDataCanChange
        End Sub

        Private Sub New(ByVal journalEntryID As Integer, ByVal entryType As BookEntryType, _
            ByVal chronologicValidator As IChronologicValidator, ByVal accountsToSkip As Long())
            ' require use of factory methods
            MarkAsChild()
            Fetch(journalEntryID, entryType, chronologicValidator, accountsToSkip)
            Me.AllowEdit = chronologicValidator.FinancialDataCanChange
            Me.AllowNew = chronologicValidator.FinancialDataCanChange
            Me.AllowRemove = chronologicValidator.FinancialDataCanChange
        End Sub

        Private Sub New(ByVal nBookEntryInternalList As BookEntryInternalList, _
            ByVal entryType As BookEntryType, ByVal markItemsAsOld As Boolean, _
            ByVal nFinancialDataCanChange As Boolean, ByVal nFinancialDataCanChangeExplanation As String, _
            ByVal accountsToSkip As Long())

            ' require use of factory methods

            MarkAsChild()

            Fetch(nBookEntryInternalList, entryType, markItemsAsOld, nFinancialDataCanChange, _
                nFinancialDataCanChangeExplanation, accountsToSkip)

            Me.AllowEdit = nFinancialDataCanChange
            Me.AllowNew = nFinancialDataCanChange
            Me.AllowRemove = nFinancialDataCanChange

        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal myData As DataTable, ByVal entryType As BookEntryType, _
            ByVal nFinancialDataCanChange As Boolean, ByVal nFinancialDataCanChangeExplanation As String, _
            ByVal accountsToSkip As Long())

            RaiseListChangedEvents = False

            _Type = entryType

            For Each dr As DataRow In myData.Rows
                If Utilities.ConvertDatabaseCharID(Of BookEntryType)(CStrSafe(dr.Item(1))) = entryType _
                    AndAlso (accountsToSkip Is Nothing OrElse accountsToSkip.Length < 1 OrElse _
                    Array.IndexOf(accountsToSkip, CLongSafe(dr.Item(2), 0)) < 0) Then _
                    Add(BookEntry.GetBookEntry(dr, nFinancialDataCanChange))
            Next

            _FinancialDataCanChange = nFinancialDataCanChange
            _FinancialDataCanChangeExplanation = nFinancialDataCanChangeExplanation

            RaiseListChangedEvents = True

        End Sub

        Private Sub Fetch(ByVal journalEntryID As Integer, ByVal entryType As BookEntryType, _
            ByVal chronologicValidator As IChronologicValidator, ByVal accountsToSkip As Long())

            Dim myComm As SQLCommand = New SQLCommand("BookEntriesFetch")
            myComm.AddParam("?BD", journalEntryID)

            Using myData As DataTable = myComm.Fetch
                Fetch(myData, entryType, chronologicValidator.FinancialDataCanChange, _
                      chronologicValidator.FinancialDataCanChangeExplanation, accountsToSkip)
            End Using

        End Sub

        Private Sub Fetch(ByVal source As BookEntryInternalList, _
            ByVal entryType As BookEntryType, ByVal markItemsAsOld As Boolean, _
            ByVal nFinancialDataCanChange As Boolean, ByVal nFinancialDataCanChangeExplanation As String, _
            ByVal accountsToSkip As Long())

            RaiseListChangedEvents = False

            _FinancialDataCanChange = nFinancialDataCanChange
            _FinancialDataCanChangeExplanation = nFinancialDataCanChangeExplanation

            _Type = entryType

            For Each i As BookEntryInternal In source
                If i.EntryType = entryType AndAlso (accountsToSkip Is Nothing OrElse _
                    accountsToSkip.Length < 1 OrElse Array.IndexOf(accountsToSkip, i.Account) < 0) Then _
                        Add(BookEntry.GetBookEntry(i, markItemsAsOld))
            Next

            RaiseListChangedEvents = True

        End Sub

        Friend Sub Update(ByVal parent As JournalEntry)

            RaiseListChangedEvents = False

            If parent.ChronologyValidator.FinancialDataCanChange Then
                For Each item As BookEntry In DeletedList
                    If Not item.IsNew Then item.DeleteSelf()
                Next
            End If
            DeletedList.Clear()

            For Each item As BookEntry In Me
                If item.IsNew AndAlso parent.ChronologyValidator.FinancialDataCanChange Then
                    item.Insert(Me, parent)
                ElseIf Not item.IsNew AndAlso item.IsDirty Then
                    item.Update(Me, parent)
                End If
            Next

            RaiseListChangedEvents = True

        End Sub

#End Region

    End Class

End Namespace