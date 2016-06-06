Imports ApskaitaObjects.General
Namespace Extensibility.General

    Module Extensibility


        ''' <summary>
        ''' Closes revenue and costs nominal <see cref="ApskaitaObjects.General.Account">accounts</see> 
        ''' at the end of the period and creates a <see cref="ApskaitaObjects.General.JournalEntry">JournalEntry</see> 
        ''' of type <see cref="DocumentType.ClosingEntries">ClosingEntries</see> in the general ledger bypassing dataportal.
        ''' </summary>
        ''' <param name="closeAtDate">Date of the closing operation (last day of the period).</param>
        ''' <param name="consolidatedAccount">Technical account used for closure. 
        ''' Credited by the total amount of costs nominal accounts 
        ''' and debited by the total amount of revenue nominal accounts. 
        ''' A <see cref="CompanyAccount">CompanyAccount</see> of type 
        ''' <see cref="DefaultAccountType.ClosingSummary">DefaultAccountType.ClosingSummary</see>
        ''' should be used as default.</param>
        ''' <param name="currentProfitAccount">Account where the result (income/loss) for the current period is stored.
        ''' A <see cref="CompanyAccount">CompanyAccount</see> of type 
        ''' <see cref="DefaultAccountType.CurrentProfit">DefaultAccountType.CurrentProfit</see>
        ''' should be used as default.</param>
        ''' <param name="formerProfitAccount">Account where the result (income/loss) for the previous periods is stored.
        ''' A <see cref="CompanyAccount">CompanyAccount</see> of type 
        ''' <see cref="DefaultAccountType.FormerProfit">DefaultAccountType.FormerProfit</see>
        ''' should be used as default.</param>
        ''' <returns><see cref="ApskaitaObjects.General.JournalEntry.ID">database ID of the closing operation in the general ledger</see></returns>
        ''' <remarks>An extensibility method for the <see cref="ClosingEntriesCommand">ClosingEntriesCommand</see>.</remarks>
        Public Function CloseEntries(ByVal closeAtDate As Date, ByVal consolidatedAccount As Long, _
            ByVal currentProfitAccount As Long, ByVal formerProfitAccount As Long) As Integer
            Return ClosingEntriesCommand.CloseEntries(closeAtDate, consolidatedAccount, currentProfitAccount, formerProfitAccount)
        End Function


        ''' <summary>
        ''' Gets as new BookEntryList of type <paramref name="entryType">entryType</paramref>.
        ''' </summary>
        ''' <param name="EntryType"><see cref="BookEntryType">Type</see> of the transactions in the list.</param>
        Public Function NewBookEntryList(ByVal entryType As BookEntryType) As BookEntryList
            Return BookEntryList.NewBookEntryList(entryType)
        End Function

        ''' <summary>
        ''' Gets as new JournalEntry as a part of another business object.
        ''' </summary>
        ''' <param name="pluginCode">a document type code of the parent business object
        ''' (use null or empty to create a new journal entry of type <see cref="DocumentType.None">DocumentType.None</see>)</param>
        ''' <param name="allowGenericJournalEntry">whether to allow creating of a 
        ''' <see cref="DocumentType.None">generic journal entry</see></param>
        ''' <remarks>Should only be called on server side.</remarks>
        Public Function NewJournalEntryChild(ByVal pluginCode As String, _
            Optional ByVal allowGenericJournalEntry As Boolean = False) As JournalEntry
            If StringIsNullOrEmpty(pluginCode) Then
                Return JournalEntry.NewJournalEntryChild(DocumentType.None, "", allowGenericJournalEntry)
            Else
                Return JournalEntry.NewJournalEntryChild(DocumentType.Custom, pluginCode)
            End If
        End Function


        ''' <summary>
        ''' Gets a current account list from a database bypassing DataPortal.
        ''' </summary>
        ''' <remarks>Should only be invoked on server side.</remarks>
        Public Function GetAccountListChild() As AccountList
            Return AccountList.GetAccountListChild()
        End Function

        ''' <summary>
        ''' Gets a BookEntryList from database.
        ''' </summary>
        ''' <param name="myData">a DataTable containing transaction data (data should be arranged in
        ''' the sequence: ID, BookEntryType, account, amount, personInfo; standard query code BookEntriesFetch
        ''' with a param ?BD=JournalEntryID)</param>
        ''' <param name="entryType">a <see cref="BookEntryType">Type</see> of the transactions in the list</param>
        ''' <param name="financialDataCanChange">whether the parent denies financial changes of the list</param>
        ''' <param name="financialDataCanChangeExplanation">human readable explanation why the parent denies financial changes of the list.</param>
        ''' <param name="accountsToSkip">transaction's accounts that should not be included in the list</param>
        ''' <remarks></remarks>
        Public Function GetBookEntryList(ByVal myData As DataTable, _
            ByVal entryType As BookEntryType, ByVal financialDataCanChange As Boolean, _
            ByVal financialDataCanChangeExplanation As String, _
            ByVal ParamArray accountsToSkip As Long()) As BookEntryList
            Return BookEntryList.GetBookEntryList(myData, entryType, financialDataCanChange, _
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
        Public Function GetBookEntryList(ByVal journalEntryID As Integer, _
            ByVal entryType As BookEntryType, ByVal chronologicValidator As IChronologicValidator, _
            ByVal ParamArray accountsToSkip As Long()) As BookEntryList
            Return BookEntryList.GetBookEntryList(journalEntryID, entryType, chronologicValidator, accountsToSkip)
        End Function

        ''' <summary>
        ''' Gets an existing JournalEntry as a part of another business object.
        ''' </summary>
        ''' <param name="journalEntryID">an <see cref="JournalEntry.ID">ID of the JournalEntry</see></param>
        ''' <param name="expectedPluginCode">a type code of the (external plugin) parent business object
        ''' that the journal entry is fetched for</param>
        ''' <param name="allowGenericJournalEntry">whether to allow fetching of a 
        ''' <see cref="DocumentType.None">generic journal entry</see></param>
        ''' <remarks>Should only be called on server side.</remarks>
        Public Function GetJournalEntryChild(ByVal journalEntryID As Integer, ByVal expectedPluginCode As String, _
            Optional ByVal allowGenericJournalEntry As Boolean = False) As JournalEntry
            If StringIsNullOrEmpty(expectedPluginCode) Then
                Return JournalEntry.GetJournalEntryChild(journalEntryID, DocumentType.None, "", allowGenericJournalEntry)
            Else
                Return JournalEntry.GetJournalEntryChild(journalEntryID, DocumentType.Custom, expectedPluginCode)
            End If
        End Function


        ''' <summary>
        ''' Saves the object as a child of some other object bypassing DataPortal and returns the saved instance.
        ''' </summary>
        ''' <param name="list">an <see cref="AccountList">AccountList</see> to save</param>
        ''' <remarks>Should only be invoked on server side.
        ''' Should only be invoked on child objects that were created or fetched using child factory methods.</remarks>
        Public Function SaveChild(ByVal list As AccountList) As AccountList

            If list Is Nothing Then Throw New ArgumentNullException("list")

            Return list.SaveChild()

        End Function

        ''' <summary>
        ''' Saves the object as a child of some other object bypassing DataPortal and returns the saved instance.
        ''' </summary>
        ''' <param name="transfer">a <see cref="TransferOfBalance">TransferOfBalance</see> to save</param>
        ''' <remarks>Should only be invoked on server side.
        ''' Should only be invoked on child objects that were created or fetched using child factory methods.</remarks>
        Public Function SaveChild(ByVal transfer As TransferOfBalance) As TransferOfBalance

            If transfer Is Nothing Then Throw New ArgumentNullException("transfer")

            Return transfer.SaveChild()

        End Function


        ''' <summary>
        ''' Deletes an existing JournalEntry from database bypassing dataportal.
        ''' </summary>
        ''' <param name="journalEntryID">an <see cref="JournalEntry.ID">ID of the JournalEntry</see> to delete</param>
        ''' <remarks>Should only be invoked on server side.</remarks>
        Public Sub DeleteChild(ByVal journalEntryID As Integer)
            JournalEntry.DeleteJournalEntryChild(journalEntryID)
        End Sub

        ''' <summary>
        ''' Deletes TransferOfBalance instance (single per company) data from database bypassing DataPortal.
        ''' </summary>
        ''' <remarks>Should only be invoked on server side.</remarks>
        Public Sub DeleteTransferOfBalanceChild()
            TransferOfBalance.DeleteTransferOfBalanceChild()
        End Sub

    End Module

End Namespace