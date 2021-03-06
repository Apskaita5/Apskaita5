﻿Imports ApskaitaObjects.Attributes

Namespace General

    ''' <summary>
    ''' Represents balance transfer document.
    ''' </summary>
    ''' <remarks>Only one instance of TransferOfBalance exists per company.
    ''' The object is a specific view of <see cref="JournalEntry">JournalEntry</see>.
    ''' No data is stored outside a general <see cref="JournalEntry">JournalEntry</see> data scope.</remarks>
    <Serializable()> _
    Public NotInheritable Class TransferOfBalance
        Inherits BusinessBase(Of TransferOfBalance)
        Implements IIsDirtyEnough, IValidationMessageProvider

#Region " Business Methods "

        Private _Guid As Guid = Guid.NewGuid
        Private _ID As Integer = 0
        Private _ChronologicValidator As TransferOfBalanceChronologicValidator
        Private _Date As Date = Today
        Private _InsertDate As DateTime = Now
        Private _UpdateDate As DateTime = Now
        Private WithEvents _DebetList As BookEntryList
        Private WithEvents _CreditList As BookEntryList
        Private WithEvents _AnalyticsList As TransferOfBalanceAnalyticsList
        Private _DebetSum As Double = 0
        Private _CreditSum As Double = 0


        ' used to implement automatic sort in datagridview
        <NotUndoable()> _
        <NonSerialized()> _
        Private _AnalyticsListSortedList As Csla.SortedBindingList(Of TransferOfBalanceAnalytics) = Nothing
        ' used to implement automatic sort in datagridview
        <NotUndoable()> _
        <NonSerialized()> _
        Private _DebetListSortedList As Csla.SortedBindingList(Of BookEntry) = Nothing
        ' used to implement automatic sort in datagridview
        <NotUndoable()> _
        <NonSerialized()> _
        Private _CreditListSortedList As Csla.SortedBindingList(Of BookEntry) = Nothing


        ''' <summary>
        ''' Gets an ID of the JournalEntry object (assigned by DB AUTO_INCREMENT).
        ''' </summary>
        ''' <remarks>TransferOfBalance is a specificaly formated JournalEntry.</remarks>
        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' Gets <see cref="IChronologicValidator">IChronologicValidator</see> object that contains business restraints on updating the operation.
        ''' </summary>
        Public ReadOnly Property ChronologicValidator() As TransferOfBalanceChronologicValidator
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ChronologicValidator
            End Get
        End Property

        ''' <summary>
        ''' Gets the date and time when the operation was inserted into the database.
        ''' </summary>
        Public ReadOnly Property InsertDate() As DateTime
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _InsertDate
            End Get
        End Property

        ''' <summary>
        ''' Gets the date and time when the operation was last updated.
        ''' </summary>
        Public ReadOnly Property UpdateDate() As DateTime
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _UpdateDate
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets a date of the balance transfer operation.
        ''' </summary>
        Public Property [Date]() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Date
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Date)
                CanWriteProperty(True)
                If _Date.Date <> value.Date Then
                    _Date = value.Date
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets a BookEntryList containing balances of type Debit. 
        ''' </summary>
        Public ReadOnly Property DebetList() As BookEntryList
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DebetList
            End Get
        End Property

        ''' <summary>
        ''' Gets a BookEntryList containing balances of type Credit. 
        ''' </summary>
        Public ReadOnly Property CreditList() As BookEntryList
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _CreditList
            End Get
        End Property

        ''' <summary>
        ''' Gets a a list of <see cref="General.TransferOfBalanceAnalytics">balance transfer records by person</see>
        ''' </summary>
        Public ReadOnly Property AnalyticsList() As TransferOfBalanceAnalyticsList
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AnalyticsList
            End Get
        End Property

        ''' <summary>
        ''' Gets a sortable view of <see cref="DebetList">DebetList</see>.
        ''' </summary>
        Public ReadOnly Property DebetListSorted() As Csla.SortedBindingList(Of BookEntry)
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If _DebetListSortedList Is Nothing Then _DebetListSortedList = _
                    New Csla.SortedBindingList(Of BookEntry)(_DebetList)
                Return _DebetListSortedList
            End Get
        End Property

        ''' <summary>
        ''' Gets a sortable view of <see cref="CreditList">CreditList</see>.
        ''' </summary>
        Public ReadOnly Property CreditListSorted() As Csla.SortedBindingList(Of BookEntry)
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If _CreditListSortedList Is Nothing Then _CreditListSortedList = _
                    New Csla.SortedBindingList(Of BookEntry)(_CreditList)
                Return _CreditListSortedList
            End Get
        End Property

        ''' <summary>
        ''' Gets a sortable view of <see cref="AnalyticsList">AnalyticsList</see>.
        ''' </summary>
        Public ReadOnly Property AnalyticsListSorted() As Csla.SortedBindingList(Of TransferOfBalanceAnalytics)
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If _AnalyticsListSortedList Is Nothing Then _AnalyticsListSortedList = _
                    New Csla.SortedBindingList(Of TransferOfBalanceAnalytics)(_AnalyticsList)
                Return _AnalyticsListSortedList
            End Get
        End Property

        ''' <summary>
        ''' Gets a total sum of transfered balance of type Debit.
        ''' </summary>
        <DoubleField(ValueRequiredLevel.Mandatory, False, 2)> _
        Public ReadOnly Property DebetSum() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_DebetSum)
            End Get
        End Property

        ''' <summary>
        ''' Gets a total sum of transfered balance of type Credit.
        ''' </summary>
        <DoubleField(ValueRequiredLevel.Mandatory, False, 2)> _
        Public ReadOnly Property CreditSum() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_CreditSum)
            End Get
        End Property


        ''' <summary>
        ''' Returnes TRUE if the object is new and contains some user provided data 
        ''' OR
        ''' object is not new and was changed by the user.
        ''' </summary>
        Public ReadOnly Property IsDirtyEnough() As Boolean _
            Implements IIsDirtyEnough.IsDirtyEnough
            Get
                If Not IsNew Then Return IsDirty
                Return _DebetList.Count > 0 OrElse _CreditList.Count > 0 _
                    OrElse _AnalyticsList.Count > 0
            End Get
        End Property


        Public Overrides ReadOnly Property IsDirty() As Boolean
            Get
                Return MyBase.IsDirty OrElse _AnalyticsList.IsDirty _
                    OrElse _DebetList.IsDirty OrElse _CreditList.IsDirty
            End Get
        End Property

        Public Overrides ReadOnly Property IsValid() As Boolean _
            Implements IValidationMessageProvider.IsValid
            Get
                Return MyBase.IsValid AndAlso _AnalyticsList.IsValid _
                    AndAlso _DebetList.IsValid AndAlso _CreditList.IsValid
            End Get
        End Property



        Public Overrides Function Save() As TransferOfBalance

            If Not Me.IsValid Then
                Throw New Exception(String.Format(My.Resources.Common_ContainsErrors, vbCrLf, _
                    GetAllBrokenRules()))
            End If

            Return MyBase.Save

        End Function


        Private Sub AnalyticsList_Changed(ByVal sender As Object, _
            ByVal e As System.ComponentModel.ListChangedEventArgs) Handles _AnalyticsList.ListChanged


        End Sub

        Private Sub DebetList_Changed(ByVal sender As Object, _
            ByVal e As System.ComponentModel.ListChangedEventArgs) Handles _DebetList.ListChanged

            _DebetSum = _DebetList.GetSum
            PropertyHasChanged("DebetSum")

        End Sub

        Private Sub CreditList_Changed(ByVal sender As Object, _
            ByVal e As System.ComponentModel.ListChangedEventArgs) Handles _CreditList.ListChanged

            _CreditSum = _CreditList.GetSum
            PropertyHasChanged("CreditSum")

        End Sub

        ''' <summary>
        ''' Helper method. Takes care of child lists loosing their handlers.
        ''' </summary>
        Protected Overrides Function GetClone() As Object
            Dim result As TransferOfBalance = DirectCast(MyBase.GetClone(), TransferOfBalance)
            result.RestoreChildListsHandles()
            Return result
        End Function

        Protected Overrides Sub OnDeserialized(ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.OnDeserialized(context)
            RestoreChildListsHandles()
        End Sub

        Protected Overrides Sub UndoChangesComplete()
            MyBase.UndoChangesComplete()
            RestoreChildListsHandles()
        End Sub

        ''' <summary>
        ''' Helper method. Takes care of TaskTimeSpans loosing its handler. See GetClone method.
        ''' </summary>
        Friend Sub RestoreChildListsHandles()

            Try
                RemoveHandler _AnalyticsList.ListChanged, AddressOf AnalyticsList_Changed
                RemoveHandler _DebetList.ListChanged, AddressOf DebetList_Changed
                RemoveHandler _CreditList.ListChanged, AddressOf CreditList_Changed
            Catch ex As Exception
            End Try
            AddHandler _AnalyticsList.ListChanged, AddressOf AnalyticsList_Changed
            AddHandler _DebetList.ListChanged, AddressOf DebetList_Changed
            AddHandler _CreditList.ListChanged, AddressOf CreditList_Changed

        End Sub


        Public Function GetAllBrokenRules() As String _
            Implements IValidationMessageProvider.GetAllBrokenRules
            Dim result As String = ""
            If Not MyBase.IsValid Then result = AddWithNewLine(result, _
                Me.BrokenRulesCollection.ToString(Validation.RuleSeverity.Error), False)
            If Not _DebetList.IsValid Then result = AddWithNewLine(result, _
                _DebetList.GetAllBrokenRules, False)
            If Not _CreditList.IsValid Then result = AddWithNewLine(result, _
                _CreditList.GetAllBrokenRules, False)
            If Not _AnalyticsList.IsValid Then result = AddWithNewLine(result, _
                _AnalyticsList.GetAllBrokenRules, False)
            Return result
        End Function

        Public Function GetAllWarnings() As String _
            Implements IValidationMessageProvider.GetAllWarnings
            Dim result As String = ""
            If Me.BrokenRulesCollection.WarningCount > 0 Then
                result = AddWithNewLine(result, Me.BrokenRulesCollection.ToString(Validation.RuleSeverity.Warning), False)
            End If
            If _DebetList.HasWarnings Then
                result = AddWithNewLine(result, _DebetList.GetAllWarnings, False)
            End If
            If _CreditList.HasWarnings Then
                result = AddWithNewLine(result, _CreditList.GetAllWarnings, False)
            End If
            If _AnalyticsList.HasWarnings Then
                result = AddWithNewLine(result, _AnalyticsList.GetAllWarnings, False)
            End If
            Return result
        End Function

        Public Function HasWarnings() As Boolean _
            Implements IValidationMessageProvider.HasWarnings
            Return Me.BrokenRulesCollection.WarningCount > 0 OrElse _DebetList.HasWarnings _
                OrElse _CreditList.HasWarnings OrElse _AnalyticsList.HasWarnings
        End Function


        Public Sub PasteDebetList(ByVal pasteString As String, ByVal overwrite As Boolean)

            If Not _ChronologicValidator.FinancialDataCanChange Then Throw New Exception( _
                String.Format(My.Resources.General_TransferOfBalance_FinancialDataChangeDenied, _
                vbCrLf, _ChronologicValidator.FinancialDataCanChangeExplanation))

            _DebetList.Paste(pasteString, overwrite)

        End Sub

        Public Sub PasteCreditList(ByVal pasteString As String, ByVal overwrite As Boolean)

            If Not _ChronologicValidator.FinancialDataCanChange Then Throw New Exception( _
                String.Format(My.Resources.General_TransferOfBalance_FinancialDataChangeDenied, _
                vbCrLf, _ChronologicValidator.FinancialDataCanChangeExplanation))

            _CreditList.Paste(pasteString, overwrite)

        End Sub

        Public Sub PasteAnalyticsList(ByVal pasteString As String, ByVal overwrite As Boolean)

            If Not _ChronologicValidator.FinancialDataCanChange Then Throw New Exception(
                String.Format(My.Resources.General_TransferOfBalance_FinancialDataChangeDenied,
                vbCrLf, _ChronologicValidator.FinancialDataCanChangeExplanation))

            _AnalyticsList.Paste(pasteString, overwrite)

        End Sub

        ''' <summary>
        ''' Adds TransferOfBalanceAnalytics's from a template datatable,
        ''' see <see cref="TransferOfBalanceAnalytics.GetDataTableSpecification()">TransferOfBalanceAnalytics.GetDataTableSpecification</see> method.
        ''' </summary>
        ''' <param name="table">a template datatable containing transaction data</param>
        ''' <param name="overwrite">wheather to clear the current items</param>
        Public Sub LoadAnalyticsListFromTemplateDataTable(ByVal table As DataTable, ByVal overwrite As Boolean)

            If Not _ChronologicValidator.FinancialDataCanChange Then Throw New Exception(
                String.Format(My.Resources.General_TransferOfBalance_FinancialDataChangeDenied,
                vbCrLf, _ChronologicValidator.FinancialDataCanChangeExplanation))

            _AnalyticsList.LoadFromTemplateDataTable(table, overwrite)

        End Sub


        Protected Overrides Function GetIdValue() As Object
            If IsNew Then Return _Guid.ToString
            Return _ID.ToString
        End Function

        Public Overrides Function ToString() As String
            Return String.Format(My.Resources.General_JournalEntry_ToString, _
                IIf(IsNew, "Nauja", "Registruota"), _Date.ToString("yyyy-MM-dd"), _
                _ID.ToString, vbCrLf, _DebetList.ToString(), _CreditList.ToString(), _
                vbCrLf, _AnalyticsList.ToString)
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()

            ValidationRules.AddRule(AddressOf CommonValidation.DoubleFieldValidation, _
                New Csla.Validation.RuleArgs("DebetSum"))
            ValidationRules.AddRule(AddressOf CommonValidation.DoubleFieldValidation, _
                New Csla.Validation.RuleArgs("CreditSum"))

            ValidationRules.AddRule(AddressOf CommonValidation.ChronologyValidation, _
                New CommonValidation.ChronologyRuleArgs("Date", "ChronologicValidator"))

            ValidationRules.AddRule(AddressOf DebetEqualsCreditValidation, _
                New Validation.RuleArgs("DebetSum"))

            ValidationRules.AddDependantProperty("CreditSum", "DebetSum", False)

        End Sub

        ''' <summary>
        ''' Rule ensuring that Debet BookEntries.GetSum = Credit BookEntries.GetSum.
        ''' </summary>
        ''' <param name="target">Object containing the data to validate</param>
        ''' <param name="e">Arguments parameter specifying the name of the string
        ''' property to validate</param>
        ''' <returns><see langword="false" /> if the rule is broken</returns>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
        Private Shared Function DebetEqualsCreditValidation(ByVal target As Object, _
            ByVal e As Validation.RuleArgs) As Boolean

            Dim ValObj As TransferOfBalance = DirectCast(target, TransferOfBalance)

            If Not CRound(ValObj._DebetSum) > 0 OrElse Not CRound(ValObj._CreditSum) > 0 Then Return True

            If CRound(ValObj._DebetSum) <> CRound(ValObj._CreditSum) Then
                e.Description = My.Resources.General_JournalEntry_DebitNotEqualsCredit
                e.Severity = Validation.RuleSeverity.Error
                Return False
            End If

            Return True

        End Function

#End Region

#Region " Authorization Rules "

        Protected Overrides Sub AddAuthorizationRules()
            AuthorizationRules.AllowWrite("General.TransferOfBalance3")
        End Sub

        Public Shared Function CanGetObject() As Boolean
            Return ApplicationContext.User.IsInRole("General.TransferOfBalance1")
        End Function

        Public Shared Function CanAddObject() As Boolean
            Return ApplicationContext.User.IsInRole("General.TransferOfBalance3")
        End Function

        Public Shared Function CanEditObject() As Boolean
            Return ApplicationContext.User.IsInRole("General.TransferOfBalance3")
        End Function

        Public Shared Function CanDeleteObject() As Boolean
            Return ApplicationContext.User.IsInRole("General.TransferOfBalance3")
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets a TransferOfBalance instance (single per company).
        ''' </summary>
        Public Shared Function GetTransferOfBalance() As TransferOfBalance
            Return DataPortal.Fetch(Of TransferOfBalance)(New Criteria())
        End Function

        ''' <summary>
        ''' Gets a TransferOfBalance instance (single per company) bypassing DataPortal.
        ''' </summary>
        ''' <remarks>Should only be invoked on server side.</remarks>
        Friend Shared Function GetTransferOfBalanceChild() As TransferOfBalance
            Dim result As New TransferOfBalance
            result.MarkAsChild()
            result.DataPortal_Fetch(New Criteria)
            Return result
        End Function

        ''' <summary>
        ''' Deletes TransferOfBalance instance (single per company) data from database.
        ''' </summary>
        Public Shared Sub DeleteTransferOfBalance()
            DataPortal.Delete(New Criteria())
        End Sub

        ''' <summary>
        ''' Deletes TransferOfBalance instance (single per company) data from database bypassing DataPortal.
        ''' </summary>
        ''' <remarks>Should only be invoked on server side.</remarks>
        Friend Shared Sub DeleteTransferOfBalanceChild()
            Dim result As New TransferOfBalance
            result.DataPortal_Delete(New Criteria)
        End Sub


        Private Sub New()
            ' require use of factory methods
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> _
        Private Class Criteria
            Public Sub New()
            End Sub
        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)

            If Not CanGetObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecuritySelectDenied)

            _ChronologicValidator = TransferOfBalanceChronologicValidator. _
                GetTransferOfBalanceChronologicValidator

            _ID = _ChronologicValidator.CurrentOperationID

            If _ID > 0 Then
                Fetch()
            Else
                Create()
            End If

        End Sub

        Private Sub Create()

            If Not _ChronologicValidator.FinancialDataCanChange Then
                Throw New Exception(String.Format(My.Resources.General_TransferOfBalance_FinancialDataInsertDenied, _
                    vbCrLf, _ChronologicValidator.FinancialDataCanChangeExplanation))
            End If

            _DebetList = BookEntryList.NewBookEntryList(BookEntryType.Debetas)
            _CreditList = BookEntryList.NewBookEntryList(BookEntryType.Kreditas)
            _AnalyticsList = TransferOfBalanceAnalyticsList. _
                NewTransferOfBalanceAnalyticsList

            ValidationRules.CheckRules()

        End Sub

        Private Sub Fetch()

            Dim JE As JournalEntry = JournalEntry.GetJournalEntryChild(_ID, DocumentType.TransferOfBalance)

            _Date = JE.Date
            _InsertDate = JE.InsertDate
            _UpdateDate = JE.UpdateDate

            Dim CommonBookEntryList As BookEntryInternalList = _
                BookEntryInternalList.NewBookEntryInternalList(BookEntryType.Debetas)

            CommonBookEntryList.AddBookEntryLists(JE.DebetList, JE.CreditList)

            _AnalyticsList = TransferOfBalanceAnalyticsList.GetTransferOfBalanceAnalyticsList( _
                CommonBookEntryList, _ChronologicValidator.FinancialDataCanChange)

            CommonBookEntryList.Aggregate()

            _DebetList = BookEntryList.GetBookEntryList(CommonBookEntryList, BookEntryType.Debetas, _
                True, _ChronologicValidator.FinancialDataCanChange, _ChronologicValidator.FinancialDataCanChangeExplanation)
            _CreditList = BookEntryList.GetBookEntryList(CommonBookEntryList, BookEntryType.Kreditas, _
                True, _ChronologicValidator.FinancialDataCanChange, _ChronologicValidator.FinancialDataCanChangeExplanation)

            _DebetSum = _DebetList.GetSum
            _CreditSum = _CreditList.GetSum

            MarkOld()

        End Sub


        ''' <summary>
        ''' Saves the object as a child of some other object bypassing DataPortal and returns the saved instance.
        ''' </summary>
        ''' <remarks>Should only be invoked on server side.
        ''' Should only be invoked on child objects that were created or fetched using child factory methods.</remarks>
        Friend Function SaveChild() As TransferOfBalance

            If Not IsChild Then Throw New Exception(My.Resources.Common_InvalidSaveChild)

            Dim result As TransferOfBalance = Me.Clone()
            result.DoSave()
            Return result

        End Function

        Protected Overrides Sub DataPortal_Insert()
            If Not CanEditObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecurityUpdateDenied)
            DoSave()
        End Sub

        Protected Overrides Sub DataPortal_Update()
            If Not CanEditObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecurityUpdateDenied)
            DoSave()
        End Sub

        Private Sub DoSave()

            _ChronologicValidator = TransferOfBalanceChronologicValidator. _
                GetTransferOfBalanceChronologicValidator

            Me.ValidationRules.CheckRules()
            If Not Me.IsValid Then
                Throw New Exception(String.Format(My.Resources.Common_ContainsErrors, vbCrLf, _
                    GetAllBrokenRules()))
            End If

            If _ChronologicValidator.CurrentOperationID <> _ID Then
                Throw New Exception(My.Resources.General_TransferOfBalance_IdHasChanged)
            End If

            If Not _ID > 0 AndAlso Not _ChronologicValidator.FinancialDataCanChange Then
                Throw New Exception(String.Format(My.Resources.General_TransferOfBalance_FinancialDataInsertDenied, _
                    vbCrLf, _ChronologicValidator.FinancialDataCanChangeExplanation))
            End If


            Dim result As JournalEntry

            If _ID > 0 Then

                result = JournalEntry.GetJournalEntryChild(_ID, DocumentType.TransferOfBalance)

                If result.UpdateDate <> _UpdateDate Then
                    Throw New Exception(My.Resources.Common_UpdateDateHasChanged)
                End If

            Else

                result = JournalEntry.NewJournalEntryChild(DocumentType.TransferOfBalance)
                result.Content = My.Resources.General_TransferOfBalance_Content
                result.DocNumber = My.Resources.General_TransferOfBalance_DocNumber

            End If

            result.Date = _Date.Date

            If _ChronologicValidator.FinancialDataCanChange Then

                Dim CommonBookEntryList As BookEntryInternalList = _
                BookEntryInternalList.NewBookEntryInternalList(BookEntryType.Debetas)
                CommonBookEntryList.AddBookEntryLists(_DebetList, _CreditList)
                For Each i As BookEntryInternal In CommonBookEntryList
                    i.Person = Nothing
                Next
                _AnalyticsList.Update(CommonBookEntryList)
                CommonBookEntryList.Aggregate()

                result.DebetList.LoadBookEntryListFromInternalList(CommonBookEntryList, False, False)
                result.CreditList.LoadBookEntryListFromInternalList(CommonBookEntryList, False, False)

            End If

            If Not result.IsValid Then
                Throw New Exception(String.Format(My.Resources.Common_FailedToCreateJournalEntry, _
                    vbCrLf, result.ToString, vbCrLf, result.GetAllBrokenRules))
            End If

            result = result.SaveChild()

            _ID = result.ID
            _InsertDate = result.InsertDate
            _UpdateDate = result.UpdateDate

            MarkOld()

        End Sub


        Protected Overrides Sub DataPortal_DeleteSelf()
            DataPortal_Delete(New Criteria())
        End Sub

        Protected Overrides Sub DataPortal_Delete(ByVal criteria As Object)

            If Not CanDeleteObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecurityUpdateDenied)

            Dim validator As TransferOfBalanceChronologicValidator = _
                TransferOfBalanceChronologicValidator.GetTransferOfBalanceChronologicValidator

            If Not validator.CurrentOperationID > 0 Then
                Throw New Exception(String.Format(My.Resources.Common_ObjectNotFound, _
                    My.Resources.General_TransferOfBalance_TypeName, "N/A"))
            ElseIf Not validator.FinancialDataCanChange Then
                Throw New Exception(String.Format(My.Resources.General_TransferOfBalance_FinancialDataDeleteDenied, _
                    vbCrLf, validator.FinancialDataCanChangeExplanation))
            End If

            IndirectRelationInfoList.CheckIfJournalEntryCanBeDeleted(validator.CurrentOperationID, _
                DocumentType.TransferOfBalance)

            General.JournalEntry.DeleteJournalEntryChild(validator.CurrentOperationID)

            MarkNew()

            ApskaitaObjects.Settings.CompanyInfo.LoadCompanyInfoToGlobalContext("", "")

        End Sub

#End Region

    End Class

End Namespace