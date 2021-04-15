Imports ApskaitaObjects.Attributes
Imports ApskaitaObjects.My.Resources

Namespace Assets

    ''' <summary>
    ''' Represents a complex document that contains a collection of long term asset 
    ''' amortization (depreciation) operations.
    ''' </summary>
    ''' <remarks>Does not have a dedicated database table. Operation values
    ''' are derived from the encapsulated JournalEntry and child items.
    ''' Child operation values are stored in the database table turtas_op.</remarks>
    <Serializable()> _
    Public NotInheritable Class ComplexOperationAmortization
        Inherits BusinessBase(Of ComplexOperationAmortization)
        Implements IIsDirtyEnough, IValidationMessageProvider

#Region " Business Methods "

        Private _ID As Integer = -1
        Private _ChronologyValidator As ComplexChronologicValidator
        Private _InsertDate As DateTime = Now
        Private _UpdateDate As DateTime = Now
        Private _Date As Date = Today.Date
        Private _Content As String = ""
        Private _DocumentNumber As String = ""
        Private _JournalEntryID As Integer = -1
        Private _TotalValueChange As Double = 0
        Private WithEvents _Items As OperationAmortizationList

        <NonSerialized()> _
        <NotUndoable()> _
        Private _ItemsSorted As SortedBindingList(Of OperationAmortization) = Nothing


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
        ''' is used to validate a long term assets amortization complex document 
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
        ''' Gets or sets a number of the long term asset complex operation document.
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas_op.DocNo.
        ''' (same for all the child operations)</remarks>
        <StringField(ValueRequiredLevel.Mandatory, 30)> _
        Public Property DocumentNumber() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DocumentNumber
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As String)
                CanWriteProperty(True)
                If value Is Nothing Then value = ""
                If _DocumentNumber.Trim <> value.Trim Then
                    _DocumentNumber = value.Trim
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets an <see cref="General.JournalEntry.ID">ID of the journal entry</see> 
        ''' that is encapsulated by the long term asset complex amortization operation.
        ''' </summary>
        ''' <remarks>A journal entry is encapsulated by the operation.
        ''' Value is stored in the database field turtas_op.JE_ID.
        ''' (same for all the child operations)</remarks>
        Public ReadOnly Property JournalEntryID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryID
            End Get
        End Property

        ''' <summary>
        ''' Gets the total long term asset value change.
        ''' </summary>
        ''' <remarks>Value is calculated.</remarks>
        <DoubleField(ValueRequiredLevel.Mandatory, False, 2)> _
        Public ReadOnly Property TotalValueChange() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_TotalValueChange)
            End Get
        End Property

        ''' <summary>
        ''' Gets a collection of long term asset amortization operations 
        ''' within the complex amortization operation document.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Items() As OperationAmortizationList
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Items
            End Get
        End Property

        ''' <summary>
        ''' Gets a sortable view of the collection of long term asset amortization operations 
        ''' within the complex amortization operation document.
        ''' </summary>
        ''' <remarks>Used to implement autosort in a datagridview.</remarks>
        Public ReadOnly Property ItemsSorted() As SortedBindingList(Of OperationAmortization)
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If _ItemsSorted Is Nothing Then
                    _ItemsSorted = New SortedBindingList(Of OperationAmortization)(_Items)
                End If
                Return _ItemsSorted
            End Get
        End Property


        Public Overrides ReadOnly Property IsValid() As Boolean _
            Implements IValidationMessageProvider.IsValid
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
                Return (Not String.IsNullOrEmpty(_DocumentNumber.Trim) _
                    OrElse Not String.IsNullOrEmpty(_Content.Trim) _
                    OrElse _Items.Count > 0)
            End Get
        End Property



        ''' <summary>
        ''' Sets the operations properties with the amortization calculation data provided.
        ''' </summary>
        ''' <param name="calculationList">A collection of amortization calculation data.</param>
        ''' <param name="warnings">Out parameter, returns non critical exceptions
        ''' that occured during the data import. Returns an empty string if no exception encountered.</param>
        ''' <remarks></remarks>
        Public Sub SetCalculations(ByVal calculationList As LongTermAssetAmortizationCalculationList, _
            ByRef warnings As String)

            If Not _ChronologyValidator.FinancialDataCanChange Then
                Throw New Exception(String.Format( _
                    My.Resources.Assets_ComplexOperationAmortization_CannotChangeFinancialData, _
                    vbCrLf, _ChronologyValidator.FinancialDataCanChangeExplanation))
            End If

            _Items.SetCalculations(calculationList, warnings)

        End Sub

        ''' <summary>
        ''' Sets the operations properties with the amortization calculation data provided.
        ''' </summary>
        ''' <param name="calculation">An amortization calculation data for an asset.</param>
        ''' <remarks></remarks>
        Public Sub SetCalculations(ByVal calculation As LongTermAssetAmortizationCalculation)

            If Not _ChronologyValidator.FinancialDataCanChange Then
                Throw New Exception(String.Format( _
                    My.Resources.Assets_ComplexOperationAmortization_CannotChangeFinancialData, _
                    vbCrLf, _ChronologyValidator.FinancialDataCanChangeExplanation))
            End If

            _Items.SetCalculations(calculation)

        End Sub

        ''' <summary>
        ''' Gets amortization calculations for the document.
        ''' </summary>
        ''' <remarks></remarks>
        Public Function GetCalculations() As LongTermAssetAmortizationCalculationList

            If _Items.Count < 1 Then
                Throw New Exception(My.Resources.Assets_ComplexOperationAmortization_DocumentEmpty)
            End If

            If Not _ChronologyValidator.FinancialDataCanChange Then
                Throw New Exception(String.Format( _
                    My.Resources.Assets_ComplexOperationAmortization_CannotChangeFinancialData, _
                    vbCrLf, _ChronologyValidator.FinancialDataCanChangeExplanation))
            End If

            Dim resultAssetID As New List(Of Integer)
            Dim resultOperationID As New List(Of Integer)

            For Each item As OperationAmortization In _Items
                resultAssetID.Add(item.AssetID)
                resultOperationID.Add(item.ID)
            Next

            Return LongTermAssetAmortizationCalculationList.GetList( _
                resultAssetID.ToArray(), resultOperationID.ToArray(), _Date)

        End Function

        ''' <summary>
        ''' Adds items in the list to the current collection.
        ''' </summary>
        ''' <param name="list"></param>
        ''' <remarks>Invoke <see cref="OperationAmortizationList.GetOperationAmortizationList">OperationAmortizationList.GetOperationAmortizationList</see>
        ''' to get a list of new operations by asset ID's.</remarks>
        Public Sub AddRange(ByVal list As OperationAmortizationList)

            If Not _ChronologyValidator.FinancialDataCanChange Then
                Throw New Exception(String.Format( _
                    My.Resources.Assets_ComplexOperationAmortization_CannotChangeFinancialDataFull, _
                    vbCrLf, _ChronologyValidator.FinancialDataCanChangeExplanation))
            End If

            list.SetParentDate(_Date)

            _Items.AddRange(list)

            For Each i As OperationAmortization In list
                _ChronologyValidator.MergeNewValidationItem(i.ChronologyValidator)
            Next

        End Sub

        ''' <summary>
        ''' Sets the same <see cref="OperationAmortization.AccountCosts">
        ''' assets amorization costs account</see> for all the asset items within the document.
        ''' </summary>
        ''' <param name="accountCosts">an <see cref="General.Account.ID">account</see> to set</param>
        ''' <remarks></remarks>
        Public Sub SetCommonAccountCosts(ByVal accountCosts As Long)

            If Not _ChronologyValidator.ChildrenFinancialDataCanChange Then
                Throw New Exception(String.Format(Assets_ComplexOperationAmortization_SetCommonAccountCostsInvalidForChildItems, _
                    vbCrLf, _ChronologyValidator.ChildrenFinancialDataCanChangeExplanation))
            ElseIf Not _ChronologyValidator.FinancialDataCanChange Then
                Throw New Exception(String.Format(Assets_ComplexOperationAmortization_SetCommonAccountCostsInvalidForDocument, _
                    vbCrLf, _ChronologyValidator.FinancialDataCanChangeExplanation))
            End If

            _Items.SetCommonAccountCosts(accountCosts)

        End Sub


        Public Function GetAllBrokenRules() As String _
            Implements IValidationMessageProvider.GetAllBrokenRules
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

        Public Function GetAllWarnings() As String _
            Implements IValidationMessageProvider.GetAllWarnings
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

        Public Function HasWarnings() As Boolean _
            Implements IValidationMessageProvider.HasWarnings
            Return (MyBase.BrokenRulesCollection.WarningCount > 0 OrElse _Items.HasWarnings())
        End Function


        Public Overrides Function Save() As ComplexOperationAmortization

            Me.ValidationRules.CheckRules()
            If Not Me.IsValid Then
                Throw New Exception(String.Format(My.Resources.Common_ContainsErrors, vbCrLf, _
                    GetAllBrokenRules()))
            End If

            Return MyBase.Save()

        End Function


        Private Sub Items_Changed(ByVal sender As Object, _
            ByVal e As System.ComponentModel.ListChangedEventArgs) Handles _Items.ListChanged

            _TotalValueChange = _Items.GetTotalCosts()
            PropertyHasChanged("TotalValueChange")

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
            Dim result As ComplexOperationAmortization = DirectCast(MyBase.GetClone(), ComplexOperationAmortization)
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
        ''' Helper method. Takes care of ReportItems loosing its handler. See GetClone method.
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
            Return String.Format(My.Resources.Assets_ComplexOperationAmortization_ToString, _
                _Date.ToString("yyyy-MM-dd"), _DocumentNumber, _ID.ToString())
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()

            ValidationRules.AddRule(AddressOf CommonValidation.CommonValidation.DoubleFieldValidation, _
                New Csla.Validation.RuleArgs("TotalValueChange"))
            ValidationRules.AddRule(AddressOf CommonValidation.CommonValidation.StringFieldValidation, _
                New Csla.Validation.RuleArgs("Content"))
            ValidationRules.AddRule(AddressOf CommonValidation.CommonValidation.StringFieldValidation, _
                New Csla.Validation.RuleArgs("DocumentNumber"))
            ValidationRules.AddRule(AddressOf CommonValidation.CommonValidation.ChronologyValidation, _
                New CommonValidation.CommonValidation.ChronologyRuleArgs("Date", "ChronologyValidator"))

            ValidationRules.AddDependantProperty("ChronologyValidator", "Date", False)

        End Sub

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
        ''' Gets a new ComplexOperationAmortization instance.
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Function NewComplexOperationAmortization() As ComplexOperationAmortization
            Dim result As New ComplexOperationAmortization
            result.Create()
            Return result
        End Function


        ''' <summary>
        ''' Gets an existing ComplexOperationAmortization instance from a database.
        ''' </summary>
        ''' <param name="id">An <see cref="ComplexOperationAmortization.ID">ID of the operation</see> 
        ''' or an <see cref="OperationAmortization.JournalEntryID">ID the journal entry</see>  
        ''' that is encapsulated by the operation.</param>
        ''' <param name="nFetchByJournalEntryID">Whether the <paramref name="id">id</paramref>
        ''' param is an <see cref="ComplexOperationAmortization.JournalEntryID">ID the journal entry</see> 
        ''' that is encapsulated by the operation.</param>
        ''' <remarks></remarks>
        Public Shared Function GetComplexOperationAmortization(ByVal id As Integer, _
            ByVal nFetchByJournalEntryID As Boolean) As ComplexOperationAmortization
            Return DataPortal.Fetch(Of ComplexOperationAmortization) _
                (New Criteria(id, nFetchByJournalEntryID))
        End Function


        ''' <summary>
        ''' Deletes an existing ComplexOperationAmortization instance from a database.
        ''' </summary>
        ''' <param name="id">An <see cref="ComplexOperationAmortization.ID">ID of the operation</see> 
        ''' to delete.</param>
        ''' <remarks></remarks>
        Public Shared Sub DeleteComplexOperationAmortization(ByVal id As Integer)
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
                My.Resources.Assets_ComplexOperationAmortization_TypeName, Nothing)

            _ChronologyValidator = ComplexChronologicValidator.NewComplexChronologicValidator( _
                My.Resources.Assets_ComplexOperationAmortization_TypeName, _
                baseValidator, Nothing, Nothing)

            _Items = OperationAmortizationList.NewOperationAmortizationList()

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
                Throw New Exception(My.Resources.Assets_ComplexOperationAmortization_OperationIDNull)
            End If

            Dim list As List(Of OperationPersistenceObject) = OperationPersistenceObject. _
                GetOperationPersistenceObjectList(operationID, LtaOperationType.Amortization)

            If list.Count < 1 Then Throw New Exception(String.Format( _
                My.Resources.Common_ObjectNotFound, My.Resources.Assets_ComplexOperationAmortization_TypeName, _
                operationID.ToString()))

            _ID = operationID
            _Date = list(0).OperationDate
            _Content = list(0).Content
            _DocumentNumber = list(0).DocumentNumber
            _JournalEntryID = list(0).JournalEntryID

            Dim baseValidator As SimpleChronologicValidator = SimpleChronologicValidator. _
                GetSimpleChronologicValidator(_JournalEntryID, _Date, _
                My.Resources.Assets_ComplexOperationAmortization_TypeName, Nothing)

            Using generalData As DataTable = OperationBackground.GetDataSourceGeneral(operationID)
                Using deltaData As DataTable = OperationBackground.GetDataSourceDelta(operationID)
                    _Items = OperationAmortizationList.GetOperationAmortizationList( _
                        list, generalData, deltaData, baseValidator)
                End Using
            End Using

            _InsertDate = _Items.GetInsertDate
            _UpdateDate = _Items.GetUpdateDate

            _TotalValueChange = _Items.GetTotalCosts()

            _ChronologyValidator = ComplexChronologicValidator.GetComplexChronologicValidator( _
                _JournalEntryID, _Date, My.Resources.Assets_ComplexOperationAmortization_TypeName, _
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
                Throw New Exception(My.Resources.Assets_ComplexOperationAmortization_DocumentEmpty)
            End If

            _Items.SetParentDate(_Date) ' just in case

            _Items.CheckIfCanSave(Me)

            If IsNew Then

                Dim baseValidator As SimpleChronologicValidator = _
                    SimpleChronologicValidator.NewSimpleChronologicValidator( _
                    My.Resources.Assets_ComplexOperationAmortization_TypeName, Nothing)

                _ChronologyValidator = ComplexChronologicValidator.NewComplexChronologicValidator( _
                    My.Resources.Assets_ComplexOperationAmortization_TypeName, _
                    baseValidator, Nothing, _Items.GetChronologyValidators())

            Else

                Dim baseValidator As SimpleChronologicValidator = _
                    SimpleChronologicValidator.GetSimpleChronologicValidator( _
                    _JournalEntryID, _ChronologyValidator.CurrentOperationDate, _
                    My.Resources.Assets_ComplexOperationAmortization_TypeName, Nothing)

                _ChronologyValidator = ComplexChronologicValidator.GetComplexChronologicValidator( _
                    _JournalEntryID, _ChronologyValidator.CurrentOperationDate, _
                    My.Resources.Assets_ComplexOperationAmortization_TypeName, _
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

            Dim entry As General.JournalEntry = GetJournalEntry()

            Using transaction As New SqlTransaction

                Try

                    entry = entry.SaveChild()

                    If IsNew Then
                        _JournalEntryID = entry.ID
                    End If

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


        Private Function GetJournalEntry() As General.JournalEntry

            Dim result As General.JournalEntry = Nothing

            If IsNew Then
                result = General.JournalEntry.NewJournalEntryChild(DocumentType.Amortization)
            Else
                result = General.JournalEntry.GetJournalEntryChild(_JournalEntryID, _
                    DocumentType.Amortization)
            End If

            result.Date = _Date.Date
            result.Person = Nothing
            result.Content = _Content
            result.DocNumber = _DocumentNumber

            Dim commonBookEntryList As BookEntryInternalList = _Items.GetTotalBookEntryList()
            commonBookEntryList.Aggregate()
            result.DebetList.LoadBookEntryListFromInternalList(commonBookEntryList, False, False)
            result.CreditList.LoadBookEntryListFromInternalList(commonBookEntryList, False, False)

            If Not result.IsValid Then
                Throw New Exception(String.Format(My.Resources.Common_FailedToCreateJournalEntry, _
                    vbCrLf, result.ToString, vbCrLf, result.GetAllBrokenRules))
            End If

            Return result

        End Function


        Private Overloads Sub DataPortal_Delete(ByVal criteria As Criteria)

            If Not CanDeleteObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecurityUpdateDenied)

            Dim operationToDelete As New ComplexOperationAmortization

            operationToDelete.DataPortal_Fetch(criteria)

            operationToDelete.CheckIfCanDelete()

            operationToDelete.DoDelete()

        End Sub

        Private Sub CheckIfCanDelete()

            If Not _ChronologyValidator.FinancialDataCanChange Then
                Throw New Exception(String.Format( _
                    My.Resources.Assets_ComplexOperationAmortization_InvalidDelete, _
                    vbCrLf, _ChronologyValidator.FinancialDataCanChangeExplanation))
            End If

            _Items.CheckIfCanDelete(_ChronologyValidator)

            IndirectRelationInfoList.CheckIfJournalEntryCanBeDeleted( _
                _JournalEntryID, DocumentType.Amortization)

        End Sub

        Private Sub DoDelete()

            Using transaction As New SqlTransaction

                Try

                    General.JournalEntry.DeleteJournalEntryChild(_JournalEntryID)

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
