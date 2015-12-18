Imports Csla.Validation

Namespace Assets

    ''' <summary>
    ''' Represents a complex document that contains a collection of long term asset 
    ''' balance value change (reevaluation) operations.
    ''' </summary>
    ''' <remarks>Does not have a dedicated database table. Operation values
    ''' are derived from the encapsulated JournalEntry and child items.
    ''' Child operation values are stored in the database table turtas_op.</remarks>
    <Serializable()> _
    Public Class ComplexOperationValueChange
        Inherits BusinessBase(Of ComplexOperationValueChange)
        Implements IIsDirtyEnough

#Region " Business Methods "

        Private _ID As Integer = -1
        Private _ChronologyValidator As ComplexChronologicValidator
        Private _InsertDate As DateTime = Now
        Private _UpdateDate As DateTime = Now
        Private _Date As Date = Today.Date
        Private _Content As String = ""
        Private _DocumentNumber As String = ""
        Private _JournalEntryID As Integer = 0
        Private _JournalEntryDocumentNumber As String = ""
        Private _JournalEntryDate As Date = Today
        Private _JournalEntryContent As String = ""
        Private _JournalEntryPersonID As Integer = 0
        Private _JournalEntryPerson As String = ""
        Private _JournalEntryAmount As Double = 0
        Private _JournalEntryBookEntries As String = ""
        Private _JournalEntryDocumentType As DocumentType = DocumentType.None
        Private WithEvents _Items As OperationValueChangeList

        <NonSerialized()> _
        <NotUndoable()> _
        Private _ItemsSorted As SortedBindingList(Of OperationValueChange) = Nothing


        ''' <summary>
        ''' Gets an ID of the operation that is assigned by the <see cref="OperationPersistenceObject.GetNewComplexOperationID">
        ''' OperationPersistenceObject.GetNewComplexOperationID</see> method.
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas_op.IsComplexAct.</remarks>
        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' Gets the date and time when the operation was inserted into the database.
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas_op.InsertDate.</remarks>
        Public ReadOnly Property InsertDate() As DateTime
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _InsertDate
            End Get
        End Property

        ''' <summary>
        ''' Gets the date and time when the operation was last updated.
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas_op.UpdateDate.</remarks>
        Public ReadOnly Property UpdateDate() As DateTime
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _UpdateDate
            End Get
        End Property

        ''' <summary>
        ''' Gets <see cref="IChronologicValidator">IChronologicValidator</see> object 
        ''' that contains business restraints on updating the document.
        ''' </summary>
        ''' <remarks>A <see cref="ComplexChronologicValidator">ComplexChronologicValidator</see> 
        ''' is used to validate a long term assets reevaluation complex document 
        ''' chronological business rules.</remarks>
        Public ReadOnly Property ChronologyValidator() As ComplexChronologicValidator
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ChronologyValidator
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets a date of the long term asset complex operation.
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas_op.OperationDate
        ''' (same for all the child operations).</remarks>
        Public Property [Date]() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Date
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Date)
                CanWriteProperty(True)
                If _Date.Date <> value.Date Then
                    _Date = value
                    PropertyHasChanged()
                    _Items.SetParentDate(value)
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a content (description) of the long term asset complex operation.
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas_op.Content.
        ''' (same for all the child operations)</remarks>
        <StringField(ValueRequiredLevel.Mandatory, 255)> _
        Public Property Content() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Content
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As String)
                CanWriteProperty(True)
                If value Is Nothing Then value = ""
                If _Content.Trim <> value.Trim Then
                    _Content = value.Trim
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets a number of the long term asset operation document
        ''' that should be the same as the document number of the associated journal entry.
        ''' </summary>
        ''' <remarks>Value is not stored, assigned by the associated journal entry.
        ''' Corresponds to <see cref="general.JournalEntry.DocNumber">JournalEntry.DocNumber</see>.</remarks>
        <StringField(ValueRequiredLevel.Mandatory, 30)> _
        Public ReadOnly Property DocumentNumber() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DocumentNumber
            End Get
        End Property

        ''' <summary>
        ''' Gets an <see cref="General.JournalEntry.ID">ID of the journal entry</see> 
        ''' that is attached to the long term asset complex balance value change 
        ''' (reevaluation) operation.
        ''' </summary>
        ''' <remarks>A journal entry is attached to the operation.
        ''' Value is stored in the database field turtas_op.JE_ID.
        ''' (same for all the child operations)</remarks>
        Public ReadOnly Property JournalEntryID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryID
            End Get
        End Property

        ''' <summary>
        ''' Gets a document number of the journal entry that is attached to the operation.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="general.JournalEntry.DocNumber">JournalEntry.DocNumber</see>.</remarks>
        Public ReadOnly Property JournalEntryDocumentNumber() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryDocumentNumber.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a date of the journal entry that is attached to the operation.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="general.JournalEntry.Date">JournalEntry.Date</see>.</remarks>
        Public ReadOnly Property JournalEntryDate() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryDate
            End Get
        End Property

        ''' <summary>
        ''' Gets a content of the journal entry that is attached to the operation.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="general.JournalEntry.Content">JournalEntry.Content</see>.</remarks>
        Public ReadOnly Property JournalEntryContent() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryContent.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets an ID of the person in the journal entry that is attached to the operation.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="general.JournalEntry.Person">JournalEntry.Person</see>.</remarks>
        Public ReadOnly Property JournalEntryPersonID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryPersonID
            End Get
        End Property

        ''' <summary>
        ''' Gets a name of the person in the journal entry that is attached to the operation.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="general.JournalEntry.Person">JournalEntry.Person</see>.</remarks>
        Public ReadOnly Property JournalEntryPerson() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryPerson.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a total book entries' amount of the journal entry that is attached to the operation.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="general.JournalEntry.DebetSum">JournalEntry.DebetSum</see>.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property JournalEntryAmount() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_JournalEntryAmount)
            End Get
        End Property

        ''' <summary>
        ''' Gets a comma separated list of book entries in the journal entry 
        ''' that is attached to the operation.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="general.JournalEntry.DebetList">JournalEntry.DebetList</see>
        ''' and <see cref="general.JournalEntry.CreditList">JournalEntry.CreditList</see>.</remarks>
        Public ReadOnly Property JournalEntryBookEntries() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryBookEntries.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a human readable (localized) type of the document that owns the journal entry 
        ''' that is attached to the operation.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="general.JournalEntry.DocType">JournalEntry.DocType</see>.</remarks>
        Public ReadOnly Property JournalEntryDocumentType() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return ConvertEnumHumanReadable(_JournalEntryDocumentType)
            End Get
        End Property

        ''' <summary>
        ''' Gets a collection of long term asset balance value change (reevaluation) operations 
        ''' within the complex reevaluation operation document.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Items() As OperationValueChangeList
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Items
            End Get
        End Property

        ''' <summary>
        ''' Gets a sortable view of the collection of long term asset balance 
        ''' value change (reevaluation) operations within the complex reevaluation document.
        ''' </summary>
        ''' <remarks>Used to implement autosort in a datagridview.</remarks>
        Public ReadOnly Property ItemsSorted() As SortedBindingList(Of OperationValueChange)
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If _ItemsSorted Is Nothing Then
                    _ItemsSorted = New SortedBindingList(Of OperationValueChange)(_Items)
                End If
                Return _ItemsSorted
            End Get
        End Property

        ''' <summary>
        ''' Whether the attached journal entry could not be changed.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property AssociatedJournalEntryIsReadOnly() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return (Not _ChronologyValidator.FinancialDataCanChange)
            End Get
        End Property


        Public Overrides ReadOnly Property IsValid() As Boolean
            Get
                Return MyBase.IsValid AndAlso _Items.IsValid
            End Get
        End Property

        Public Overrides ReadOnly Property IsDirty() As Boolean
            Get
                Return MyBase.IsDirty OrElse _Items.IsDirty
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
                Return (Not StringIsNullOrEmpty(_Content) _
                    OrElse _Items.Count > 0 OrElse _JournalEntryID > 0)
            End Get
        End Property




        ''' <summary>
        ''' Adds items in the list to the current collection.
        ''' </summary>
        ''' <param name="list"></param>
        ''' <remarks>Invoke <see cref="OperationValueChangeList.GetOperationValueChangeList">OperationValueChangeList.GetOperationValueChangeList</see>
        ''' to get a list of new operations by asset ID's.</remarks>
        Public Sub AddRange(ByVal list As OperationValueChangeList)

            If Not _ChronologyValidator.FinancialDataCanChange Then
                Throw New Exception(String.Format( _
                    My.Resources.Assets_ComplexOperationValueChange_CannotChangeFinancialDataFull, _
                    vbCrLf, _ChronologyValidator.FinancialDataCanChangeExplanation))
            End If

            list.SetParentDate(_Date)

            _Items.AddRange(list)

            For Each i As OperationValueChange In list
                _ChronologyValidator.MergeNewValidationItem(i.ChronologyValidator)
            Next

        End Sub


        Public Function GetAllBrokenRules() As String
            Dim result As String = ""
            If Not MyBase.IsValid Then
                result = AddWithNewLine(result, _
                    Me.BrokenRulesCollection.ToString(Validation.RuleSeverity.Error), False)
            End If
            If Not _Items.IsValid Then
                result = AddWithNewLine(result, _Items.GetAllBrokenRules(), False)
            End If
            Return result
        End Function

        Public Function GetAllWarnings() As String
            Dim result As String = ""
            If Not MyBase.BrokenRulesCollection.WarningCount > 0 Then
                result = AddWithNewLine(result, _
                    Me.BrokenRulesCollection.ToString(Validation.RuleSeverity.Warning), False)
            End If
            If _Items.HasWarnings() Then
                result = AddWithNewLine(result, _Items.GetAllWarnings(), False)
            End If
            Return result
        End Function

        Public Function HasWarnings() As Boolean
            Return (MyBase.BrokenRulesCollection.WarningCount > 0 OrElse _Items.HasWarnings())
        End Function


        ''' <summary>
        ''' Attaches a journal entry to the operation.
        ''' </summary>
        ''' <param name="entry">A journal entry info.</param>
        ''' <remarks>The operation does not handle journal entry. It should be
        ''' handled by other (parent) object or by a user manualy.</remarks>
        Public Sub LoadAssociatedJournalEntry(ByVal entry As ActiveReports.JournalEntryInfo)

            If entry Is Nothing OrElse Not entry.Id > 0 Then Exit Sub

            If Not Array.IndexOf(OperationValueChange.ParentJournalEntryTypes, entry.DocType) < 0 Then
                Throw New Exception(String.Format(My.Resources.Assets_OperationValueChange_CannotAttachParentType, _
                    entry.DocTypeHumanReadable))
            ElseIf Array.IndexOf(OperationValueChange.AllowedJournalEntryTypes, entry.DocType) < 0 Then
                Throw New Exception(String.Format(My.Resources.Assets_OperationValueChange_InvalidJournalEntryType, _
                    entry.DocTypeHumanReadable))
            End If

            _JournalEntryID = entry.Id
            _JournalEntryDate = entry.Date
            _JournalEntryDocumentNumber = entry.DocNumber
            _JournalEntryContent = entry.Content
            _JournalEntryBookEntries = entry.BookEntries
            _JournalEntryPersonID = entry.PersonID
            _JournalEntryPerson = entry.Person
            _JournalEntryDocumentType = entry.DocType
            _JournalEntryAmount = entry.Ammount

            _DocumentNumber = entry.DocNumber

            PropertyHasChanged("JournalEntryID")
            PropertyHasChanged("JournalEntryDate")
            PropertyHasChanged("JournalEntryContent")
            PropertyHasChanged("JournalEntryDocumentNumber")
            PropertyHasChanged("JournalEntryBookEntries")
            PropertyHasChanged("JournalEntryPersonID")
            PropertyHasChanged("JournalEntryPersonName")
            PropertyHasChanged("JournalEntryDocumentType")
            PropertyHasChanged("JournalEntryAmount")
            PropertyHasChanged("DocumentNumber")

        End Sub

        ''' <summary>
        ''' Gets a new <see cref="General.JournalEntry">journal entry</see>
        ''' that contains all the book entries requered for balance value change by the
        ''' long term asset states.
        ''' </summary>
        ''' <remarks>Could be used as a helper method when registering balance change operations.</remarks>
        Public Function NewJournalEntry() As General.JournalEntry

            Dim result As General.JournalEntry = General.JournalEntry.NewJournalEntry()

            result.Date = _Date.Date
            result.Person = Nothing
            result.Content = _Content
            result.DocNumber = _DocumentNumber

            Dim commonBookEntryList As BookEntryInternalList = _Items.GetTotalBookEntryList()

            If commonBookEntryList.GetTotalSum(BookEntryType.Debetas) _
                > commonBookEntryList.GetTotalSum(BookEntryType.Kreditas) Then
                commonBookEntryList.Add(BookEntryInternal.NewBookEntryInternal(BookEntryType.Kreditas, _
                    0, commonBookEntryList.GetTotalSum(BookEntryType.Debetas) _
                    - commonBookEntryList.GetTotalSum(BookEntryType.Kreditas), Nothing))
            Else
                commonBookEntryList.Add(BookEntryInternal.NewBookEntryInternal(BookEntryType.Debetas, _
                    0, commonBookEntryList.GetTotalSum(BookEntryType.Kreditas) _
                    - commonBookEntryList.GetTotalSum(BookEntryType.Debetas), Nothing))
            End If

            result.DebetList.LoadBookEntryListFromInternalList(commonBookEntryList, False, False)
            result.CreditList.LoadBookEntryListFromInternalList(commonBookEntryList, False, False)

            Return result

        End Function


        Public Overrides Function Save() As ComplexOperationValueChange

            Me.ValidationRules.CheckRules()
            If Not Me.IsValid Then
                Throw New Exception(String.Format(My.Resources.Common_ContainsErrors, vbCrLf, _
                    GetAllBrokenRules()))
            End If

            Return MyBase.Save()

        End Function


        Private Sub Items_Changed(ByVal sender As Object, _
            ByVal e As System.ComponentModel.ListChangedEventArgs) Handles _Items.ListChanged



            If e.ListChangedType = ComponentModel.ListChangedType.ItemAdded Then

                Try
                    _ChronologyValidator.MergeNewValidationItem(_Items(e.NewIndex).ChronologyValidator)
                    PropertyHasChanged("ChronologyValidator")
                Catch ex As Exception
                End Try

            ElseIf e.ListChangedType = ComponentModel.ListChangedType.ItemDeleted Then

                _ChronologyValidator = ComplexChronologicValidator.GetComplexChronologicValidator( _
                    _ChronologyValidator, _Items.GetChronologyValidators())

                PropertyHasChanged("ChronologyValidator")

            End If

        End Sub

        ''' <summary>
        ''' Helper method. Takes care of child lists loosing their handlers.
        ''' </summary>
        Protected Overrides Function GetClone() As Object
            Dim result As ComplexOperationValueChange = DirectCast(MyBase.GetClone(), ComplexOperationValueChange)
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
        ''' Helper method. Takes care of Items loosing its handler. See GetClone method.
        ''' </summary>
        Friend Sub RestoreChildListsHandles()
            Try
                RemoveHandler _Items.ListChanged, AddressOf Items_Changed
            Catch ex As Exception
            End Try
            AddHandler _Items.ListChanged, AddressOf Items_Changed
        End Sub


        Protected Overrides Function GetIdValue() As Object
            Return _ID
        End Function

        Public Overrides Function ToString() As String
            Return String.Format(My.Resources.Assets_ComplexOperationValueChange_ToString, _
                _Date.ToString("yyyy-MM-dd"), _DocumentNumber, _ID.ToString())
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()

            ValidationRules.AddRule(AddressOf CommonValidation.StringFieldValidation, _
                New Csla.Validation.RuleArgs("Content"))

            ValidationRules.AddRule(AddressOf DateValidation, _
                New CommonValidation.ChronologyRuleArgs("Date", "ChronologyValidator"))
            ValidationRules.AddRule(AddressOf JournalEntryIDValidation, _
                New Csla.Validation.RuleArgs("JournalEntryID"))

            ValidationRules.AddDependantProperty("JournalEntryDate", "Date", False)
            ValidationRules.AddDependantProperty("ChronologyValidator", "Date", False)

        End Sub


        ''' <summary>
        ''' Rule ensuring that the operation date is valid.
        ''' </summary>
        ''' <param name="target">Object containing the data to validate</param>
        ''' <param name="e">Arguments parameter specifying the name of the string
        ''' property to validate</param>
        ''' <returns><see langword="false" /> if the rule is broken</returns>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
        Private Shared Function DateValidation(ByVal target As Object, _
            ByVal e As Validation.RuleArgs) As Boolean

            Dim valObj As ComplexOperationValueChange = _
                DirectCast(target, ComplexOperationValueChange)

            If valObj._JournalEntryID > 0 AndAlso valObj._Date.Date <> valObj._JournalEntryDate.Date Then
                e.Description = My.Resources.Assets_OperationValueChange_DateInvalid
                e.Severity = RuleSeverity.Error
                Return False
            End If

            Return CommonValidation.ChronologyValidation(target, e)

        End Function

        ''' <summary>
        ''' Rule ensuring that a journal entry is attached.
        ''' </summary>
        ''' <param name="target">Object containing the data to validate</param>
        ''' <param name="e">Arguments parameter specifying the name of the string
        ''' property to validate</param>
        ''' <returns><see langword="false" /> if the rule is broken</returns>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
        Private Shared Function JournalEntryIDValidation(ByVal target As Object, _
          ByVal e As Validation.RuleArgs) As Boolean

            If Not DirectCast(target, ComplexOperationValueChange)._JournalEntryID > 0 Then
                e.Description = My.Resources.Assets_OperationValueChange_JournalEntryNull
                e.Severity = RuleSeverity.Error
                Return False
            End If

            Return True

        End Function

#End Region

#Region " Authorization Rules "

        Protected Overrides Sub AddAuthorizationRules()
            AuthorizationRules.AllowWrite("Assets.LongTermAssetOperation2")
        End Sub

        Public Shared Function CanAddObject() As Boolean
            Return ApplicationContext.User.IsInRole("Assets.LongTermAssetOperation2")
        End Function

        Public Shared Function CanGetObject() As Boolean
            Return ApplicationContext.User.IsInRole("Assets.LongTermAssetOperation1")
        End Function

        Public Shared Function CanEditObject() As Boolean
            Return ApplicationContext.User.IsInRole("Assets.LongTermAssetOperation3")
        End Function

        Public Shared Function CanDeleteObject() As Boolean
            Return ApplicationContext.User.IsInRole("Assets.LongTermAssetOperation3")
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets a new ComplexOperationValueChange instance.
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Function NewComplexOperationValueChange() As ComplexOperationValueChange
            Dim result As New ComplexOperationValueChange
            result.Create()
            Return result
        End Function


        ''' <summary>
        ''' Gets an existing ComplexOperationValueChange instance from a database.
        ''' </summary>
        ''' <param name="id">An <see cref="ComplexOperationValueChange.ID">ID of the operation</see> 
        ''' or an <see cref="ComplexOperationValueChange.JournalEntryID">ID the journal entry</see>  
        ''' that is encapsulated by the operation.</param>
        ''' <param name="nFetchByJournalEntryID">Whether the <paramref name="id">id</paramref>
        ''' param is an <see cref="ComplexOperationValueChange.JournalEntryID">ID the journal entry</see> 
        ''' that is encapsulated by the operation.</param>
        ''' <remarks></remarks>
        Public Shared Function GetComplexOperationValueChange(ByVal id As Integer, _
            ByVal nFetchByJournalEntryID As Boolean) As ComplexOperationValueChange
            Return DataPortal.Fetch(Of ComplexOperationValueChange) _
                (New Criteria(id, nFetchByJournalEntryID))
        End Function


        ''' <summary>
        ''' Deletes an existing ComplexOperationValueChange instance from a database.
        ''' </summary>
        ''' <param name="id">An <see cref="ComplexOperationValueChange.ID">ID of the operation</see> 
        ''' to delete.</param>
        ''' <remarks></remarks>
        Public Shared Sub DeleteComplexOperationValueChange(ByVal id As Integer)
            DataPortal.Delete(New Criteria(id))
        End Sub


        Private Sub New()
            ' require use of factory methods
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> _
        Private Class Criteria
            Private mId As Integer
            Private _FetchByJournalEntryID As Boolean
            Public ReadOnly Property Id() As Integer
                Get
                    Return mId
                End Get
            End Property
            Public ReadOnly Property FetchByJournalEntryID() As Boolean
                Get
                    Return _FetchByJournalEntryID
                End Get
            End Property
            Public Sub New(ByVal id As Integer)
                mId = id
                _FetchByJournalEntryID = False
            End Sub
            Public Sub New(ByVal id As Integer, ByVal nFetchByJournalEntryID As Boolean)
                mId = id
                _FetchByJournalEntryID = nFetchByJournalEntryID
            End Sub
        End Class


        Private Sub Create()

            Dim baseValidator As SimpleChronologicValidator = _
                SimpleChronologicValidator.NewSimpleChronologicValidator( _
                My.Resources.Assets_ComplexOperationValueChange_TypeName, Nothing)

            _ChronologyValidator = ComplexChronologicValidator.NewComplexChronologicValidator( _
                My.Resources.Assets_ComplexOperationValueChange_TypeName, _
                baseValidator, Nothing, Nothing)

            _Items = OperationValueChangeList.NewOperationValueChangeList()

            ValidationRules.CheckRules()

        End Sub


        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)

            If Not CanGetObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecuritySelectDenied)

            Dim operationID As Integer = criteria.Id

            If criteria.FetchByJournalEntryID Then
                operationID = OperationPersistenceObject.GetComplexOperationIdByJournalEntry(criteria.Id)
            End If

            Fetch(operationID)

        End Sub

        Private Sub Fetch(ByVal operationID As Integer)

            If Not operationID > 0 Then
                Throw New Exception(My.Resources.Assets_ComplexOperationValueChange_OperationIDNull)
            End If

            Dim list As List(Of OperationPersistenceObject) = OperationPersistenceObject. _
                GetOperationPersistenceObjectList(operationID, LtaOperationType.ValueChange)

            If list.Count < 1 Then Throw New Exception(String.Format( _
                My.Resources.Common_ObjectNotFound, My.Resources.Assets_ComplexOperationValueChange_TypeName, _
                operationID.ToString()))

            _ID = operationID
            _Date = list(0).OperationDate
            _Content = list(0).Content
            _DocumentNumber = list(0).JournalEntryDocumentNumber
            _JournalEntryID = list(0).JournalEntryID

            Dim baseValidator As SimpleChronologicValidator = SimpleChronologicValidator. _
                GetSimpleChronologicValidator(_JournalEntryID, _Date, _
                My.Resources.Assets_ComplexOperationValueChange_TypeName, Nothing)

            Using generalData As DataTable = OperationBackground.GetDataSourceGeneral(operationID)
                Using deltaData As DataTable = OperationBackground.GetDataSourceDelta(operationID)
                    _Items = OperationValueChangeList.GetOperationValueChangeList( _
                        list, generalData, deltaData, baseValidator)
                End Using
            End Using

            _InsertDate = _Items.GetInsertDate
            _UpdateDate = _Items.GetUpdateDate

            _ChronologyValidator = ComplexChronologicValidator.GetComplexChronologicValidator( _
                _JournalEntryID, _Date, My.Resources.Assets_ComplexOperationValueChange_TypeName, _
                baseValidator, Nothing, _Items.GetChronologyValidators())

            MarkOld()

            ValidationRules.CheckRules()

        End Sub


        Protected Overrides Sub DataPortal_Insert()

            If Not CanAddObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecurityInsertDenied)

            CheckIfCanSave()
            DoSave()

        End Sub

        Protected Overrides Sub DataPortal_Update()

            If Not CanEditObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecurityUpdateDenied)

            CheckIfCanSave()
            DoSave()

        End Sub

        Private Sub CheckIfCanSave()

            If _Items.Count < 1 Then
                Throw New Exception(My.Resources.Assets_ComplexOperationValueChange_DocumentEmpty)
            End If

            _Items.SetParentDate(_Date) ' just in case

            _Items.CheckIfCanSave(Me)

            If IsNew Then

                Dim baseValidator As SimpleChronologicValidator = _
                    SimpleChronologicValidator.NewSimpleChronologicValidator( _
                    My.Resources.Assets_ComplexOperationValueChange_TypeName, Nothing)

                _ChronologyValidator = ComplexChronologicValidator.NewComplexChronologicValidator( _
                    My.Resources.Assets_ComplexOperationValueChange_TypeName, _
                    baseValidator, Nothing, _Items.GetChronologyValidators())

            Else

                Dim baseValidator As SimpleChronologicValidator = _
                    SimpleChronologicValidator.GetSimpleChronologicValidator( _
                    _JournalEntryID, _ChronologyValidator.CurrentOperationDate, _
                    My.Resources.Assets_ComplexOperationValueChange_TypeName, Nothing)

                _ChronologyValidator = ComplexChronologicValidator.GetComplexChronologicValidator( _
                    _JournalEntryID, _ChronologyValidator.CurrentOperationDate, _
                    My.Resources.Assets_ComplexOperationValueChange_TypeName, _
                    baseValidator, Nothing, _Items.GetChronologyValidators())

            End If

            ValidationRules.CheckRules()
            If Not Me.IsValid Then
                Throw New Exception(String.Format(My.Resources.Common_ContainsErrors, vbCrLf, _
                    GetAllBrokenRules()))
            End If

        End Sub

        Private Sub DoSave()

            If IsNew Then
                _ID = OperationPersistenceObject.GetNewComplexOperationID()
            End If

            Using transaction As New SqlTransaction

                Try

                    _Items.Update(Me)

                    transaction.Commit()

                    If IsNew Then
                        _InsertDate = _Items.GetInsertDate
                    End If
                    _UpdateDate = _Items.GetUpdateDate

                Catch ex As Exception
                    transaction.SetNonSqlException(ex)
                    Throw
                End Try

            End Using

            MarkOld()

        End Sub


        Private Overloads Sub DataPortal_Delete(ByVal criteria As Criteria)

            If Not CanDeleteObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecurityUpdateDenied)

            Dim operationToDelete As New ComplexOperationValueChange

            operationToDelete.DataPortal_Fetch(criteria)

            operationToDelete.CheckIfCanDelete()

            operationToDelete.DoDelete()

        End Sub

        Private Sub CheckIfCanDelete()

            If Not _ChronologyValidator.FinancialDataCanChange Then
                Throw New Exception(String.Format( _
                    My.Resources.Assets_ComplexOperationValueChange_InvalidDelete, _
                    vbCrLf, _ChronologyValidator.FinancialDataCanChangeExplanation))
            End If

            _Items.CheckIfCanDelete(_ChronologyValidator)

        End Sub

        Private Sub DoDelete()

            Using transaction As New SqlTransaction

                Try

                    _Items.DeleteChildren()

                    transaction.Commit()

                Catch ex As Exception
                    transaction.SetNonSqlException(ex)
                    Throw
                End Try

            End Using

        End Sub

#End Region

    End Class

End Namespace