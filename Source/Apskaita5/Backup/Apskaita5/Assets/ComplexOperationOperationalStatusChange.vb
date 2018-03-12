Imports ApskaitaObjects.Attributes
Imports Csla.Validation

Namespace Assets

    ''' <summary>
    ''' Represents a complex document that contains a collection of long term asset 
    ''' operational status change operations.
    ''' </summary>
    ''' <remarks>Does not have a dedicated database table. Operation values
    ''' are derived from the encapsulated JournalEntry and child items.
    ''' Child operation values are stored in the database table turtas_op.</remarks>
    <Serializable()> _
    Public NotInheritable Class ComplexOperationOperationalStatusChange
        Inherits BusinessBase(Of ComplexOperationOperationalStatusChange)
        Implements IIsDirtyEnough, IValidationMessageProvider

#Region " Business Methods "

        Private _ID As Integer = -1
        Private _ChronologyValidator As ComplexChronologicValidator
        Private _InsertDate As DateTime = Now
        Private _UpdateDate As DateTime = Now
        Private _BeginOperationalPeriod As Boolean = True
        Private _Date As Date = Today.Date
        Private _Content As String = ""
        Private _DocumentNumber As String = ""

        Private WithEvents _Items As OperationOperationalStatusChangeList

        <NonSerialized()> _
        <NotUndoable()> _
        Private _ItemsSorted As SortedBindingList(Of OperationOperationalStatusChange) = Nothing


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
        ''' is used to validate a long term assets operational status change complex document 
        ''' chronological business rules.</remarks>
        Public ReadOnly Property ChronologyValidator() As ComplexChronologicValidator
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ChronologyValidator
            End Get
        End Property

        ''' <summary>
        ''' Whether the operation sets long term asset status as operational.
        ''' </summary>
        ''' <remarks>Value designates operation type.</remarks>
        Public ReadOnly Property BeginOperationalPeriod() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _BeginOperationalPeriod
            End Get
        End Property

        ''' <summary>
        ''' Gets a type of the long term asset operation as a human readable (localized) string, 
        ''' i.e. <see cref="LtaOperationType.UsingStart">LtaOperationType.UsingStart</see>
        ''' or <see cref="LtaOperationType.UsingEnd">LtaOperationType.UsingEnd</see>.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property TypeHumanReadable() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If _BeginOperationalPeriod Then
                    Return My.Resources.Assets_OperationOperationalStatusChange_UsingStart
                Else
                    Return My.Resources.Assets_OperationOperationalStatusChange_UsingEnd
                End If
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
        ''' Gets or sets a number of the long term asset document.
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas_op.DocNo.</remarks>
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
        ''' Gets a collection of long term asset operational status change operations 
        ''' within the complex document.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Items() As OperationOperationalStatusChangeList
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Items
            End Get
        End Property

        ''' <summary>
        ''' Gets a sortable view of the collection of long term asset operational 
        ''' status change operations within the complex document.
        ''' </summary>
        ''' <remarks>Used to implement autosort in a datagridview.</remarks>
        Public ReadOnly Property ItemsSorted() As SortedBindingList(Of OperationOperationalStatusChange)
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If _ItemsSorted Is Nothing Then
                    _ItemsSorted = New SortedBindingList(Of OperationOperationalStatusChange)(_Items)
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
                Return (Not StringIsNullOrEmpty(_Content) _
                    OrElse Not StringIsNullOrEmpty(_DocumentNumber) OrElse _Items.Count > 0)
            End Get
        End Property



        ''' <summary>
        ''' Adds items in the list to the current collection.
        ''' </summary>
        ''' <param name="list"></param>
        ''' <remarks>Invoke <see cref="OperationOperationalStatusChangeList.GetOperationOperationalStatusChangeList">OperationOperationalStatusChangeList.GetOperationOperationalStatusChangeList</see>
        ''' to get a list of new operations by asset ID's.</remarks>
        Public Sub AddRange(ByVal list As OperationOperationalStatusChangeList)

            _Items.CheckNewListForConcurrentItems(list)

            If list(0).BeginOperationalPeriod <> _BeginOperationalPeriod Then
                Throw New Exception(My.Resources.Assets_ComplexOperationOperationalStatusChange_NewItemsListTypeInvalid)
            End If

            If Not _ChronologyValidator.FinancialDataCanChange Then
                Throw New Exception(String.Format( _
                    My.Resources.Assets_ComplexOperationOperationalStatusChange_CannotChangeFinancialDataFull, _
                    vbCrLf, _ChronologyValidator.FinancialDataCanChangeExplanation))
            End If

            list.SetParentDate(_Date)

            _Items.AddRange(list)

            For Each i As OperationOperationalStatusChange In list
                _ChronologyValidator.MergeNewValidationItem(i.ChronologyValidator)
            Next

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


        Public Overrides Function Save() As ComplexOperationOperationalStatusChange

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
            Dim result As ComplexOperationOperationalStatusChange _
                = DirectCast(MyBase.GetClone(), ComplexOperationOperationalStatusChange)
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
            Return String.Format(My.Resources.Assets_ComplexOperationOperationalStatusChange_ToString, _
                _Date.ToString("yyyy-MM-dd"), _DocumentNumber, _ID.ToString())
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()

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
        ''' Gets a new ComplexOperationOperationalStatusChange instance.
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Function NewComplexOperationOperationalStatusChange( _
            ByVal beginOperationalPeriod As Boolean) As ComplexOperationOperationalStatusChange
            Return New ComplexOperationOperationalStatusChange(beginOperationalPeriod)
        End Function


        ''' <summary>
        ''' Gets an existing ComplexOperationOperationalStatusChange instance from a database.
        ''' </summary>
        ''' <param name="id">An <see cref="ComplexOperationOperationalStatusChange.ID">ID of the operation</see>.</param>
        ''' <remarks></remarks>
        Public Shared Function GetComplexOperationOperationalStatusChange(ByVal id As Integer) As ComplexOperationOperationalStatusChange
            Return DataPortal.Fetch(Of ComplexOperationOperationalStatusChange)(New Criteria(id))
        End Function


        ''' <summary>
        ''' Deletes an existing ComplexOperationOperationalStatusChange instance from a database.
        ''' </summary>
        ''' <param name="id">An <see cref="ComplexOperationOperationalStatusChange.ID">ID of the operation</see> 
        ''' to delete.</param>
        ''' <remarks></remarks>
        Public Shared Sub DeleteComplexOperationOperationalStatusChange(ByVal id As Integer)
            DataPortal.Delete(New Criteria(id))
        End Sub


        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal nBeginOperationalPeriod As Boolean)
            ' require use of factory methods
            Create(nBeginOperationalPeriod)
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> _
        Private Class Criteria
            Private mId As Integer
            Public ReadOnly Property Id() As Integer
                Get
                    Return mId
                End Get
            End Property
            Public Sub New(ByVal id As Integer)
                mId = id
            End Sub
        End Class


        Private Sub Create(ByVal nBeginOperationalPeriod As Boolean)

            _BeginOperationalPeriod = nBeginOperationalPeriod

            _ChronologyValidator = ComplexChronologicValidator.NewComplexChronologicValidator( _
                My.Resources.Assets_ComplexOperationOperationalStatusChange_TypeName, _
                Nothing, Nothing, Nothing)

            _Items = OperationOperationalStatusChangeList.NewOperationOperationalStatusChangeList()

            ValidationRules.CheckRules()

        End Sub


        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)

            If Not CanGetObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecuritySelectDenied)

            Fetch(criteria.Id)

        End Sub

        Private Sub Fetch(ByVal operationID As Integer)

            If Not operationID > 0 Then
                Throw New Exception(My.Resources.Assets_ComplexOperationOperationalStatusChange_OperationIDNull)
            End If

            Dim list As List(Of OperationPersistenceObject) = OperationPersistenceObject. _
                GetOperationPersistenceObjectList(operationID, LtaOperationType.UsingStart)

            If list.Count < 1 Then Throw New Exception(String.Format( _
                My.Resources.Common_ObjectNotFound, My.Resources.Assets_ComplexOperationOperationalStatusChange_TypeName, _
                operationID.ToString()))

            _ID = operationID
            _Date = list(0).OperationDate
            _Content = list(0).Content
            _DocumentNumber = list(0).DocumentNumber

            Using generalData As DataTable = OperationBackground.GetDataSourceGeneral(operationID)
                Using deltaData As DataTable = OperationBackground.GetDataSourceDelta(operationID)
                    _Items = OperationOperationalStatusChangeList.GetOperationOperationalStatusChangeList( _
                        list, generalData, deltaData, Nothing)
                End Using
            End Using

            _BeginOperationalPeriod = _Items(0).BeginOperationalPeriod
            _InsertDate = _Items.GetInsertDate
            _UpdateDate = _Items.GetUpdateDate

            _ChronologyValidator = ComplexChronologicValidator.GetComplexChronologicValidator( _
                _ID, _Date, My.Resources.Assets_ComplexOperationOperationalStatusChange_TypeName, _
                Nothing, Nothing, _Items.GetChronologyValidators())

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
                Throw New Exception(My.Resources.Assets_ComplexOperationOperationalStatusChange_DocumentEmpty)
            End If

            _Items.SetParentDate(_Date) ' just in case

            _Items.CheckIfCanSave(Me)

            If IsNew Then

                _ChronologyValidator = ComplexChronologicValidator.NewComplexChronologicValidator( _
                    My.Resources.Assets_ComplexOperationOperationalStatusChange_TypeName, _
                    Nothing, Nothing, _Items.GetChronologyValidators())

            Else

                _ChronologyValidator = ComplexChronologicValidator.GetComplexChronologicValidator( _
                    _ID, _ChronologyValidator.CurrentOperationDate, _
                    My.Resources.Assets_ComplexOperationOperationalStatusChange_TypeName, _
                    Nothing, Nothing, _Items.GetChronologyValidators())

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

            Dim operationToDelete As New ComplexOperationOperationalStatusChange

            operationToDelete.DataPortal_Fetch(criteria)

            operationToDelete.CheckIfCanDelete()

            operationToDelete.DoDelete()

        End Sub

        Private Sub CheckIfCanDelete()

            If Not _ChronologyValidator.FinancialDataCanChange Then
                Throw New Exception(String.Format( _
                    My.Resources.Assets_ComplexOperationOperationalStatusChange_InvalidDelete, _
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