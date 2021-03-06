﻿Imports ApskaitaObjects.Attributes

Namespace Documents

    ''' <summary>
    ''' Represents an accumulated costs or revenues operation.
    ''' </summary>
    ''' <remarks>Encapsulates a <see cref="General.JournalEntry">JournalEntry</see> of type <see cref="DocumentType.AccumulatedCosts">DocumentType.AccumulatedCosts</see>.
    ''' Values are stored in the database table accumulativecosts.</remarks>
    <Serializable()> _
    Public NotInheritable Class AccumulativeCosts
        Inherits BusinessBase(Of AccumulativeCosts)
        Implements IIsDirtyEnough, IValidationMessageProvider

#Region " Business Methods "

        Private _ID As Integer = 0
        Private _Date As Date = Today
        Private _DocumentNumber As String = My.Resources.Documents_AccumulativeCosts_DefaultDocNumber
        Private _Content As String = My.Resources.Documents_AccumulativeCosts_DefaultContent
        Private _AccountCosts As Long = 0
        Private _AccountAccumulatedCosts As Long = 0
        Private _AccountDistributedCosts As Long = 0
        Private _Comments As String = ""
        Private _ChronologyValidator As AccumulativeCostsChronologicValidator
        Private _Sum As Double = 0
        Private _IsAccumulatedIncome As Boolean = False
        Private WithEvents _Items As AccumulativeCostsItemList
        Private _SumDistributed As Double = 0
        Private _InsertDate As DateTime = Now
        Private _UpdateDate As DateTime = Now
        Private _ItemsUpdateRequired As Boolean = False

        ' used to implement automatic sort in datagridview
        <NotUndoable()> _
        <NonSerialized()> _
        Private _ItemsSortedList As Csla.SortedBindingList(Of AccumulativeCostsItem) = Nothing


        ''' <summary>
        ''' Gets <see cref="General.JournalEntry.ID">an ID of the journal entry</see> that is created by the operation.
        ''' </summary>
        ''' <remarks>Value is stored in the database table accumulativecosts.ID.</remarks>
        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' Gets the date and time when the operation was inserted into the database.
        ''' </summary>
        ''' <remarks>Value is stored by the encapsulated <see cref="General.JournalEntry.InsertDate">JournalEntry.InsertDate</see>.</remarks>
        Public ReadOnly Property InsertDate() As DateTime
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _InsertDate
            End Get
        End Property

        ''' <summary>
        ''' Gets the date and time when the operation was last updated.
        ''' </summary>
        ''' <remarks>Value is stored by the encapsulated <see cref="General.JournalEntry.UpdateDate">JournalEntry.UpdateDate</see>.</remarks>
        Public ReadOnly Property UpdateDate() As DateTime
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _UpdateDate
            End Get
        End Property


        ''' <summary>
        ''' Gets <see cref="IChronologicValidator">IChronologicValidator</see> object that contains business restraints on updating the operation.
        ''' </summary>
        Public ReadOnly Property ChronologyValidator() As IChronologicValidator
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ChronologyValidator
            End Get
        End Property

        ''' <summary>
        ''' Whether the property <see cref="AccountCosts">AccountCosts</see> is readonly due to the business rules defined by the <see cref="ChronologyValidator">ChronologyValidator</see>.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property AccountCostsIsReadOnly() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return Not _ChronologyValidator Is Nothing AndAlso _
                    Not _ChronologyValidator.FinancialDataCanChange
            End Get
        End Property

        ''' <summary>
        ''' Whether the property <see cref="AccountAccumulatedCosts">AccountAccumulatedCosts</see> is readonly due to the business rules defined by the <see cref="ChronologyValidator">ChronologyValidator</see>.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property AccountAccumulatedCostsIsReadOnly() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return Not _ChronologyValidator Is Nothing AndAlso _
                    (Not _ChronologyValidator.FinancialDataCanChange OrElse _
                    Not _ChronologyValidator.AllChildrenFinancialDataCanChange)
            End Get
        End Property

        ''' <summary>
        ''' Whether the property <see cref="AccountDistributedCosts">AccountDistributedCosts</see> is readonly due to the business rules defined by the <see cref="ChronologyValidator">ChronologyValidator</see>.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property AccountDistributedCostsIsReadOnly() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return Not _ChronologyValidator Is Nothing AndAlso _
                    Not _ChronologyValidator.AllChildrenFinancialDataCanChange
            End Get
        End Property

        ''' <summary>
        ''' Whether the property <see cref="Sum">Sum</see> is readonly due to the business rules defined by the <see cref="ChronologyValidator">ChronologyValidator</see>.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property SumIsReadOnly() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return Not _ChronologyValidator Is Nothing AndAlso _
                    Not _ChronologyValidator.FinancialDataCanChange
            End Get
        End Property

        ''' <summary>
        ''' Whether the property <see cref="IsAccumulatedIncome">IsAccumulatedIncome</see> is readonly due to the business rules defined by the <see cref="ChronologyValidator">ChronologyValidator</see>.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property IsAccumulatedIncomeIsReadOnly() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return Not _ChronologyValidator Is Nothing AndAlso _
                    (Not _ChronologyValidator.FinancialDataCanChange OrElse _
                    Not _ChronologyValidator.AllChildrenFinancialDataCanChange)
            End Get
        End Property


        ''' <summary>
        ''' Gets or sets the date of the operation.
        ''' </summary>
        ''' <remarks>Value is stored by the encapsulated <see cref="General.JournalEntry.Date">JournalEntry.Date</see>.</remarks>
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
        ''' Gets or sets the document number of the operation.
        ''' </summary>
        ''' <remarks>Value is stored by the encapsulated <see cref="General.JournalEntry.DocNumber">JournalEntry.DocNumber</see>.</remarks>
        <StringField(ValueRequiredLevel.Mandatory, 30, False)> _
        Public Property DocumentNumber() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DocumentNumber.Trim
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
        ''' Gets or sets the content (description) of the operation.
        ''' </summary>
        ''' <remarks>Value is stored by the encapsulated <see cref="General.JournalEntry.Content">JournalEntry.Content</see>.</remarks>
        <StringField(ValueRequiredLevel.Mandatory, 255, False)> _
        Public Property Content() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Content.Trim
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
        ''' Gets or sets the (costs or revenues) <see cref="General.Account.ID">account number</see> 
        ''' from which the sum is transfered to the <see cref="AccountAccumulatedCosts">AccountAccumulatedCosts</see>.
        ''' </summary>
        ''' <remarks>Value is stored in the database table accumulativecosts.AccountCosts.</remarks>
        <AccountField(ValueRequiredLevel.Mandatory, True, 5, 6)> _
        Public Property AccountCosts() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AccountCosts
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Long)
                CanWriteProperty(True)
                If AccountCostsIsReadOnly Then Exit Property
                If _AccountCosts <> value Then
                    _AccountCosts = value
                    PropertyHasChanged()
                    If Not _AccountDistributedCosts > 0 Then
                        _AccountDistributedCosts = value
                        PropertyHasChanged("AccountDistributedCosts")
                        _ItemsUpdateRequired = True
                    End If
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the <see cref="General.Account.ID">account number</see> 
        ''' in which the accumulated costs or revenues sum is stored before redistribution.
        ''' </summary>
        ''' <remarks>Value is stored in the database table accumulativecosts.AccountAccumulatedCosts.</remarks>
        <AccountField(ValueRequiredLevel.Mandatory, True, 1, 2, 3, 4)> _
        Public Property AccountAccumulatedCosts() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AccountAccumulatedCosts
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Long)
                CanWriteProperty(True)
                If AccountAccumulatedCostsIsReadOnly Then Exit Property
                If _AccountAccumulatedCosts <> value Then
                    _AccountAccumulatedCosts = value
                    PropertyHasChanged()
                    _ItemsUpdateRequired = True
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the <see cref="General.Account.ID">account number</see> 
        ''' to which the accumulated costs or revenues sum is transfered from 
        ''' <see cref="AccountAccumulatedCosts">AccountAccumulatedCosts</see> when the redistribution occures.
        ''' </summary>
        ''' <remarks>Value usualy equals <see cref="AccountCosts">AccountCosts</see>.
        ''' Value is stored in the database table accumulativecosts.AccountDistributedCosts.</remarks>
        <AccountField(ValueRequiredLevel.Mandatory, True, 5, 6)> _
        Public Property AccountDistributedCosts() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AccountDistributedCosts
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Long)
                CanWriteProperty(True)
                If AccountDistributedCostsIsReadOnly Then Exit Property
                If _AccountDistributedCosts <> value Then
                    _AccountDistributedCosts = value
                    PropertyHasChanged()
                    _ItemsUpdateRequired = True
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the user comments for the operation.
        ''' </summary>
        ''' <remarks>Value is stored in the database table accumulativecosts.Comments.</remarks>
        <StringField(ValueRequiredLevel.Optional, 255, False)> _
        Public Property Comments() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Comments.Trim
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As String)
                CanWriteProperty(True)
                If value Is Nothing Then value = ""
                If _Comments.Trim <> value.Trim Then
                    _Comments = value.Trim
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the sum of costs or revenues that should be accumulated. I.e. temporaly 
        ''' transfered from <see cref="AccountCosts">AccountCosts</see> to <see cref="AccountAccumulatedCosts">AccountAccumulatedCosts</see>
        ''' and then redistributed to the <see cref="AccountDistributedCosts">AccountDistributedCosts</see>.
        ''' </summary>
        ''' <remarks>Value is stored in the database field accumulativecosts.OperationSum.</remarks>
        <DoubleField(ValueRequiredLevel.Mandatory, False, 2)> _
        Public Property Sum() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_Sum)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Double)
                CanWriteProperty(True)
                If SumIsReadOnly Then Exit Property
                If CRound(_Sum) <> CRound(value) Then
                    _Sum = CRound(value)
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets whether the operation accumulates revenues (income) or costs.
        ''' </summary>
        ''' <remarks>Equals TRUE if the database field accumulativecosts.OperationSum value is negative.</remarks>
        Public Property IsAccumulatedIncome() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsAccumulatedIncome
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Boolean)
                CanWriteProperty(True)
                If IsAccumulatedIncomeIsReadOnly Then Exit Property
                If _IsAccumulatedIncome <> value Then
                    _IsAccumulatedIncome = value
                    PropertyHasChanged()
                    _ItemsUpdateRequired = True
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets whether the <seealso cref="Items">Items</seealso> have to be updated because 
        ''' <seealso cref="AccountAccumulatedCosts">AccountAccumulatedCosts</seealso>,
        ''' <seealso cref="AccountDistributedCosts">AccountDistributedCosts</seealso> or
        ''' <seealso cref="IsAccumulatedIncome">IsAccumulatedIncome</seealso> has changed.
        ''' </summary>
        ''' <remarks>Equals TRUE if the database field accumulativecosts.OperationSum value is negative.</remarks>
        Public ReadOnly Property ItemsUpdateRequired() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ItemsUpdateRequired
            End Get
        End Property

        ''' <summary>
        ''' Gets the collection of accumulated revenues or costs redistribution items.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Items() As AccumulativeCostsItemList
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Items
            End Get
        End Property

        ''' <summary>
        ''' Gets a sortable view of the collection of accumulated revenues or costs redistribution items.
        ''' </summary>
        ''' <remarks>Used to implement auto sort in a datagridview.</remarks>
        Public ReadOnly Property ItemsSorted() As Csla.SortedBindingList(Of AccumulativeCostsItem)
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If _Items Is Nothing Then Return Nothing
                If _ItemsSortedList Is Nothing Then _ItemsSortedList = _
                    New Csla.SortedBindingList(Of AccumulativeCostsItem)(_Items)
                Return _ItemsSortedList
            End Get
        End Property

        ''' <summary>
        ''' Gets the sum of accumulated revenues or costs that is redistributed by redistribution items.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Mandatory, False, 2)> _
        Public ReadOnly Property SumDistributed() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_SumDistributed)
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
                    OrElse Not String.IsNullOrEmpty(_Comments.Trim) _
                    OrElse _Items.Count > 0)
            End Get
        End Property



        Public Overrides ReadOnly Property IsDirty() As Boolean
            Get
                Return MyBase.IsDirty OrElse _Items.IsDirty
            End Get
        End Property

        Public Overrides ReadOnly Property IsValid() As Boolean _
            Implements IValidationMessageProvider.IsValid
            Get
                Return MyBase.IsValid AndAlso _Items.IsValid
            End Get
        End Property



        Public Overrides Function Save() As AccumulativeCosts

            Me.ValidationRules.CheckRules()
            If Not Me.IsValid Then
                Throw New Exception(String.Format(My.Resources.Common_ContainsErrors, vbCrLf, _
                    GetAllBrokenRules()))
            End If

            Return MyBase.Save

        End Function



        Private Sub Items_Changed(ByVal sender As Object, _
            ByVal e As System.ComponentModel.ListChangedEventArgs) Handles _Items.ListChanged
            CalculateTotal(True)
        End Sub

        ''' <summary>
        ''' Helper method. Takes care of child lists loosing their handlers.
        ''' </summary>
        Protected Overrides Function GetClone() As Object
            Dim result As AccumulativeCosts = DirectCast(MyBase.GetClone(), AccumulativeCosts)
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
                RemoveHandler _Items.ListChanged, AddressOf Items_Changed
            Catch ex As Exception
            End Try
            AddHandler _Items.ListChanged, AddressOf Items_Changed
        End Sub


        Public Function GetAllBrokenRules() As String _
            Implements IValidationMessageProvider.GetAllBrokenRules
            Dim result As String = ""
            If Not MyBase.IsValid Then result = AddWithNewLine(result, _
                Me.BrokenRulesCollection.ToString(Validation.RuleSeverity.Error), False)
            If Not _Items.IsValid Then result = AddWithNewLine(result, _
                _Items.GetAllBrokenRules, False)
            Return result
        End Function

        Public Function GetAllWarnings() As String _
            Implements IValidationMessageProvider.GetAllWarnings
            Dim result As String = ""
            If Not MyBase.BrokenRulesCollection.WarningCount > 0 Then result = AddWithNewLine(result, _
                Me.BrokenRulesCollection.ToString(Validation.RuleSeverity.Warning), False)
            If _Items.HasWarnings Then result = AddWithNewLine(result, _Items.GetAllWarnings, False)
            Return result
        End Function

        Public Function HasWarnings() As Boolean _
            Implements IValidationMessageProvider.HasWarnings
            Return (MyBase.BrokenRulesCollection.WarningCount > 0 OrElse _Items.HasWarnings)
        End Function


        ''' <summary>
        ''' Distributes the <see cref="Sum">Sum</see> evenly accross all the existing redistrution items.
        ''' </summary>
        ''' <remarks></remarks>
        Public Sub Distribute()
            If _Items Is Nothing Then Exit Sub
            _Items.Distribute(_Sum)
        End Sub

        ''' <summary>
        ''' Clears the current redistrution items, creates new items defined by <paramref name="periodCount">periodCount</paramref>
        ''' and distributes the <see cref="Sum">Sum</see> evenly accross all the new items.
        ''' </summary>
        '''  <param name="startingDate">The date of the first item.</param>
        '''  <param name="periodLength">Length of a period in months.</param>
        ''' <param name="periodCount">Number of items to create.</param>
        ''' <remarks></remarks>
        Public Sub Distribute(ByVal startingDate As Date, ByVal periodLength As Integer, _
            ByVal periodCount As Integer)
            If _Items Is Nothing Then Exit Sub
            _Items.Distribute(_Sum, startingDate, periodLength, periodCount)
        End Sub


        Private Sub CalculateTotal(ByVal raisePropertyHasChanged As Boolean)

            If _Items Is Nothing Then Exit Sub

            _SumDistributed = 0

            For Each a As AccumulativeCostsItem In _Items
                _SumDistributed = CRound(_SumDistributed + a.Sum)
            Next

            If raisePropertyHasChanged Then PropertyHasChanged("SumDistributed")

        End Sub


        Protected Overrides Function GetIdValue() As Object
            Return _ID
        End Function

        Public Overrides Function ToString() As String
            Return String.Format(My.Resources.Documents_AccumulativeCosts_ToString, _
                _Date.ToString("yyyy-MM-dd"), _DocumentNumber, _ID.ToString())
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()

            ValidationRules.AddRule(AddressOf CommonValidation.CommonValidation.StringFieldValidation, _
                New Csla.Validation.RuleArgs("DocumentNumber"))
            ValidationRules.AddRule(AddressOf CommonValidation.CommonValidation.StringFieldValidation, _
                New Csla.Validation.RuleArgs("Content"))
            ValidationRules.AddRule(AddressOf CommonValidation.CommonValidation.StringFieldValidation, _
                New Csla.Validation.RuleArgs("Comments"))
            ValidationRules.AddRule(AddressOf CommonValidation.CommonValidation.DoubleFieldValidation, _
                New Csla.Validation.RuleArgs("Sum"))
            ValidationRules.AddRule(AddressOf CommonValidation.CommonValidation.AccountFieldValidation, _
                New Csla.Validation.RuleArgs("AccountCosts"))
            ValidationRules.AddRule(AddressOf CommonValidation.CommonValidation.AccountFieldValidation, _
                New Csla.Validation.RuleArgs("AccountAccumulatedCosts"))
            ValidationRules.AddRule(AddressOf CommonValidation.CommonValidation.AccountFieldValidation, _
                New Csla.Validation.RuleArgs("AccountDistributedCosts"))

            ValidationRules.AddRule(AddressOf DateValidation, New Validation.RuleArgs("Date"))
            ValidationRules.AddRule(AddressOf SumDistributedValidation, New Validation.RuleArgs("SumDistributed"))
            ValidationRules.AddRule(AddressOf AccountDistributedCostsValidation, _
                New Validation.RuleArgs("AccountDistributedCosts"))

            ValidationRules.AddDependantProperty("SumDistributed", "Date", False)
            ValidationRules.AddDependantProperty("Sum", "SumDistributed", False)
            ValidationRules.AddDependantProperty("AccountCosts", "AccountDistributedCosts", False)

        End Sub

        ''' <summary>
        ''' Rule ensuring that the value of property Date is valid.
        ''' </summary>
        ''' <param name="target">Object containing the data to validate</param>
        ''' <param name="e">Arguments parameter specifying the name of the string
        ''' property to validate</param>
        ''' <returns><see langword="false" /> if the rule is broken</returns>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
        Private Shared Function DateValidation(ByVal target As Object, _
            ByVal e As Validation.RuleArgs) As Boolean

            Dim valObj As AccumulativeCosts = DirectCast(target, AccumulativeCosts)

            If Not valObj._ChronologyValidator.ValidateOperationDate(valObj._Date, e.Description, e.Severity) Then
                Return False
            ElseIf valObj._Items.Count > 0 AndAlso valObj._Items.GetMinDate.Date < valObj._Date.Date Then
                e.Description = String.Format(My.Resources.Documents_AccumulativeCosts_DateInvalid, _
                    valObj._Items.GetMinDate.ToString("yyyy-MM-dd"))
                e.Severity = Validation.RuleSeverity.Warning
                Return False
            End If

            Return True

        End Function

        ''' <summary>
        ''' Rule ensuring that the value of property SumDistributed is valid.
        ''' </summary>
        ''' <param name="target">Object containing the data to validate</param>
        ''' <param name="e">Arguments parameter specifying the name of the string
        ''' property to validate</param>
        ''' <returns><see langword="false" /> if the rule is broken</returns>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
        Private Shared Function SumDistributedValidation(ByVal target As Object, _
            ByVal e As Validation.RuleArgs) As Boolean

            Dim valObj As AccumulativeCosts = DirectCast(target, AccumulativeCosts)

            If Not valObj.Sum > 0 Then Return True

            If valObj.Sum <> valObj.SumDistributed Then
                e.Description = My.Resources.Documents_AccumulativeCosts_SumDistributedInvalid
                e.Severity = Validation.RuleSeverity.Error
                Return False
            End If

            Return True

        End Function

        ''' <summary>
        ''' Rule ensuring that the value of property AccountDistributedCosts is valid.
        ''' </summary>
        ''' <param name="target">Object containing the data to validate</param>
        ''' <param name="e">Arguments parameter specifying the name of the string
        ''' property to validate</param>
        ''' <returns><see langword="false" /> if the rule is broken</returns>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
        Private Shared Function AccountDistributedCostsValidation(ByVal target As Object, _
            ByVal e As Validation.RuleArgs) As Boolean

            Dim valObj As AccumulativeCosts = DirectCast(target, AccumulativeCosts)

            If Not valObj._AccountDistributedCosts > 0 OrElse Not valObj._AccountCosts > 0 Then Return True

            If valObj._AccountDistributedCosts <> valObj._AccountCosts Then
                e.Description = My.Resources.Documents_AccumulativeCosts_AccountDistributedCostsInvalid
                e.Severity = Validation.RuleSeverity.Warning
                Return False
            End If

            Return True

        End Function

#End Region

#Region " Authorization Rules "

        Protected Overrides Sub AddAuthorizationRules()
            AuthorizationRules.AllowWrite("General.AccumulativeCosts2")
        End Sub

        Public Shared Function CanGetObject() As Boolean
            Return ApplicationContext.User.IsInRole("General.AccumulativeCosts1")
        End Function

        Public Shared Function CanAddObject() As Boolean
            Return ApplicationContext.User.IsInRole("General.AccumulativeCosts2")
        End Function

        Public Shared Function CanEditObject() As Boolean
            Return ApplicationContext.User.IsInRole("General.AccumulativeCosts3")
        End Function

        Public Shared Function CanDeleteObject() As Boolean
            Return ApplicationContext.User.IsInRole("General.AccumulativeCosts3")
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets a new AccumulativeCosts instance.
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Function NewAccumulativeCosts() As AccumulativeCosts

            If Not CanAddObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecurityInsertDenied)

            Dim baseValidator As SimpleChronologicValidator = _
                SimpleChronologicValidator.NewSimpleChronologicValidator( _
                Utilities.ConvertLocalizedName(DocumentType.AccumulatedCosts), Nothing)

            Dim result As New AccumulativeCosts
            result._Items = AccumulativeCostsItemList.NewAccumulativeCostsItemList(baseValidator)
            result._ChronologyValidator = AccumulativeCostsChronologicValidator. _
                NewAccumulativeCostsChronologicValidator()
            result.ValidationRules.CheckRules()
            Return result

        End Function

        ''' <summary>
        ''' Gets an existing AccumulativeCosts instance from a database.
        ''' </summary>
        ''' <param name="nID">ID of the AccumulativeCosts instance to get.</param>
        ''' <remarks></remarks>
        Public Shared Function GetAccumulativeCosts(ByVal nID As Integer) As AccumulativeCosts
            Return DataPortal.Fetch(Of AccumulativeCosts)(New Criteria(nID))
        End Function

        ''' <summary>
        ''' Deletes an existing AccumulativeCosts instance from a database.
        ''' </summary>
        ''' <param name="id">ID of the AccumulativeCosts instance to delete.</param>
        ''' <remarks></remarks>
        Public Shared Sub DeleteAccumulativeCosts(ByVal id As Integer)
            DataPortal.Delete(New Criteria(id))
        End Sub


        Private Sub New()
            ' require use of factory methods
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> _
        Private Class Criteria
            Private _ID As Integer
            Public ReadOnly Property ID() As Integer
                Get
                    Return _ID
                End Get
            End Property
            Public Sub New(ByVal nID As Integer)
                _ID = nID
            End Sub
        End Class


        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)

            If Not CanGetObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecuritySelectDenied)

            Dim myComm As New SQLCommand("FetchAccumulativeCosts")
            myComm.AddParam("?PD", criteria.ID)

            Using myData As DataTable = myComm.Fetch

                If myData.Rows.Count < 1 Then Throw New Exception(String.Format( _
                    My.Resources.Common_ObjectNotFound, My.Resources.Documents_AccumulativeCosts_TypeName, _
                    criteria.ID.ToString()))

                Dim dr As DataRow = myData.Rows(0)

                _ID = CIntSafe(dr.Item(0), 0)
                _Date = CDateSafe(dr.Item(1), Today)
                _DocumentNumber = CStrSafe(dr.Item(2)).Trim
                _Content = CStrSafe(dr.Item(3)).Trim
                _Comments = CStrSafe(dr.Item(4)).Trim
                _Sum = CDblSafe(dr.Item(5), 2, 0)
                If CRound(_Sum) < 0 Then
                    _IsAccumulatedIncome = True
                    _Sum = CRound(-_Sum)
                Else
                    _IsAccumulatedIncome = False
                End If
                _AccountCosts = CLongSafe(dr.Item(6), 0)
                _AccountAccumulatedCosts = CLongSafe(dr.Item(7), 0)
                _AccountDistributedCosts = CLongSafe(dr.Item(8), 0)
                _InsertDate = CTimeStampSafe(dr.Item(9))
                _UpdateDate = CTimeStampSafe(dr.Item(10))

            End Using

            Dim baseValidator As SimpleChronologicValidator = _
                SimpleChronologicValidator.GetSimpleChronologicValidator( _
                _ID, _Date, Utilities.ConvertLocalizedName(DocumentType.AccumulatedCosts), Nothing)

            _Items = AccumulativeCostsItemList.GetAccumulativeCostsItemList(_ID, baseValidator)

            CalculateTotal(False)

            _ChronologyValidator = AccumulativeCostsChronologicValidator. _
                GetAccumulativeCostsChronologicValidator(_ID, _Items)

            MarkOld()

            ValidationRules.CheckRules()

        End Sub

        Protected Overrides Sub DataPortal_Insert()

            If Not CanAddObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecurityInsertDenied)

            Me.ValidationRules.CheckRules()
            If Not Me.IsValid Then
                Throw New Exception(String.Format(My.Resources.Common_ContainsErrors, vbCrLf, _
                    GetAllBrokenRules()))
            End If

            _Items.CheckIfCanUpdate(Me)

            Dim entry As General.JournalEntry = GetJournalEntry()

            Using transaction As New SqlTransaction

                Try

                    entry = entry.SaveChild

                    _InsertDate = entry.InsertDate
                    _UpdateDate = entry.UpdateDate

                    _ID = entry.ID

                    Dim myComm As New SQLCommand("InsertAccumulativeCosts")
                    AddWithParams(myComm)
                    myComm.AddParam("?BD", _ID)

                    myComm.Execute()

                    _Items.Update(Me)

                    transaction.Commit()

                Catch ex As Exception
                    transaction.SetNonSqlException(ex)
                    Throw
                End Try

            End Using

            MarkOld()

        End Sub

        Protected Overrides Sub DataPortal_Update()

            If Not CanEditObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecurityUpdateDenied)

            Me.ValidationRules.CheckRules()
            If Not Me.IsValid Then
                Throw New Exception(String.Format(My.Resources.Common_ContainsErrors, vbCrLf, _
                    GetAllBrokenRules()))
            End If

            _Items.CheckIfCanUpdate(Me)

            Dim entry As General.JournalEntry = GetJournalEntry()

            Dim myComm As SQLCommand

            If _ChronologyValidator.FinancialDataCanChange AndAlso _
                _ChronologyValidator.AllChildrenFinancialDataCanChange Then
                myComm = New SQLCommand("UpdateAccumulativeCosts1")
            ElseIf Not _ChronologyValidator.AllChildrenFinancialDataCanChange AndAlso _
                _ChronologyValidator.FinancialDataCanChange Then
                myComm = New SQLCommand("UpdateAccumulativeCosts3")
            ElseIf _ChronologyValidator.AllChildrenFinancialDataCanChange AndAlso _
                Not _ChronologyValidator.FinancialDataCanChange Then
                myComm = New SQLCommand("UpdateAccumulativeCosts2")
            Else
                myComm = New SQLCommand("UpdateAccumulativeCosts4")
            End If

            AddWithParams(myComm)
            myComm.AddParam("?CD", _ID)

            Using transaction As New SqlTransaction

                Try

                    entry = entry.SaveChild

                    _UpdateDate = entry.UpdateDate

                    myComm.Execute()

                    Items.Update(Me)

                    transaction.Commit()

                Catch ex As Exception
                    transaction.SetNonSqlException(ex)
                    Throw
                End Try

            End Using

            MarkOld()

        End Sub

        Protected Overrides Sub DataPortal_DeleteSelf()
            DataPortal_Delete(New Criteria(_ID))
        End Sub

        Protected Overrides Sub DataPortal_Delete(ByVal criteria As Object)

            If Not CanDeleteObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecurityUpdateDenied)

            Dim itemToDelete As AccumulativeCosts = New AccumulativeCosts
            itemToDelete.DataPortal_Fetch(New Criteria(DirectCast(criteria, Criteria).ID))

            itemToDelete.CheckIfCanDelete()

            Dim myComm As New SQLCommand("DeleteAccumulativeCosts")
            myComm.AddParam("?CD", itemToDelete.ID)

            Using transaction As New SqlTransaction

                Try

                    General.JournalEntry.DeleteJournalEntryChild(itemToDelete.ID)

                    myComm.Execute()

                    itemToDelete._Items.Delete(itemToDelete)

                    transaction.Commit()

                Catch ex As Exception
                    transaction.SetNonSqlException(ex)
                    Throw
                End Try

            End Using

            MarkNew()

        End Sub


        Private Sub CheckIfCanDelete()

            If Not _ChronologyValidator.FinancialDataCanChange Then Throw New Exception( _
                String.Format(My.Resources.Documents_AccumulativeCosts_DeleteInvalid, vbCrLf, _
                _ChronologyValidator.FinancialDataCanChangeExplanation))
            If Not _ChronologyValidator.AllChildrenFinancialDataCanChange Then Throw New Exception( _
                String.Format(My.Resources.Documents_AccumulativeCosts_DeleteInvalid, vbCrLf, _
                _ChronologyValidator.AllChildrenFinancialDataCanChangeExplanation))

            IndirectRelationInfoList.CheckIfJournalEntryCanBeDeleted(_ID, DocumentType.AccumulatedCosts)

            _Items.CheckIfCanDelete()

        End Sub

        Private Function GetJournalEntry() As General.JournalEntry

            Dim result As General.JournalEntry
            If IsNew Then
                result = General.JournalEntry.NewJournalEntryChild(DocumentType.AccumulatedCosts)
            Else
                result = General.JournalEntry.GetJournalEntryChild(_ID, DocumentType.AccumulatedCosts)
                If result.UpdateDate <> _UpdateDate Then Throw New Exception( _
                    My.Resources.Common_UpdateDateHasChanged)
            End If

            result.Content = _Content
            result.Date = _Date
            result.DocNumber = _DocumentNumber

            If _ChronologyValidator.FinancialDataCanChange Then

                If result.DebetList.Count <> 1 Then
                    result.DebetList.Clear()
                    result.DebetList.Add(General.BookEntry.NewBookEntry())
                End If

                result.DebetList(0).Amount = CRound(_Sum)

                If result.CreditList.Count <> 1 Then
                    result.CreditList.Clear()
                    result.CreditList.Add(General.BookEntry.NewBookEntry())
                End If

                result.CreditList(0).Amount = CRound(_Sum)

                If _IsAccumulatedIncome Then
                    result.CreditList(0).Account = _AccountAccumulatedCosts
                    result.DebetList(0).Account = _AccountCosts
                Else
                    result.CreditList(0).Account = _AccountCosts
                    result.DebetList(0).Account = _AccountAccumulatedCosts
                End If

            End If

            Return result

        End Function

        Private Sub AddWithParams(ByRef myComm As SQLCommand)

            myComm.AddParam("?AA", _Comments.Trim)

            If _ChronologyValidator.FinancialDataCanChange Then
                If _IsAccumulatedIncome Then
                    myComm.AddParam("?AB", -CRound(_Sum))
                Else
                    myComm.AddParam("?AB", CRound(_Sum))
                End If
                myComm.AddParam("?AC", _AccountCosts)
                If _ChronologyValidator.AllChildrenFinancialDataCanChange Then
                    myComm.AddParam("?AD", _AccountAccumulatedCosts)
                End If
            End If

            If _ChronologyValidator.AllChildrenFinancialDataCanChange Then
                myComm.AddParam("?AE", _AccountDistributedCosts)
            End If

        End Sub

#End Region

    End Class

End Namespace