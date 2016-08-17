Imports Csla.Validation
Imports ApskaitaObjects.My.Resources
Imports System.IO

Namespace Goods

    ''' <summary>
    ''' Represents a simple goods redeem to a supplier operation, registers a (back) transfer
    ''' of the goods that were previously bought from the supplier.
    ''' </summary>
    ''' <remarks>See methodology for BAS No 9 ""Stores"" para. 40 for details.
    ''' Has an associated <see cref="General.JournalEntry">JournalEntry</see>, e.g. invoice.
    ''' Values are stored using <see cref="OperationPersistenceObject">OperationPersistenceObject</see>.
    ''' Has an encapsulated <see cref="ConsignmentDiscardPersistenceObjectList">ConsignmentDiscardPersistenceObjectList</see>
    ''' irrespective of goods accounting type, because you have to ""annul"" the 
    ''' acquisition consignments that are made irrespective of goods accounting type.</remarks>
    <Serializable()> _
    Public NotInheritable Class GoodsOperationRedeemToSupplier
        Inherits BusinessBase(Of GoodsOperationRedeemToSupplier)
        Implements IIsDirtyEnough, IGetErrorForListItem, IValidationMessageProvider

#Region " Business Methods "

        ''' <summary>
        ''' Types of the (journal entry) documents that could be attached 
        ''' to the operation, i.e. could be a redeem base.
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared ReadOnly AllowedJournalEntryTypes As DocumentType() _
            = New DocumentType() {DocumentType.BankOperation, _
            DocumentType.None, DocumentType.TillIncomeOrder, _
            DocumentType.AdvanceReport}

        ''' <summary>
        ''' Types of the (journal entry) documents that act as a parent
        ''' of the operation, i.e. the operation could only be changed
        ''' within the approprate document context.
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared ReadOnly ParentJournalEntryTypes As DocumentType() _
            = New DocumentType() {DocumentType.InvoiceMade, _
            DocumentType.InvoiceReceived, DocumentType.GoodsInternalTransfer}

        Private ReadOnly _Guid As Guid = Guid.NewGuid()
        Private _ID As Integer = 0
        Private _GoodsInfo As GoodsSummary = Nothing
        Private _OperationLimitations As OperationalLimitList = Nothing
        Private _ComplexOperationID As Integer = 0
        Private _ComplexOperationType As GoodsComplexOperationType = GoodsComplexOperationType.InternalTransfer
        Private _ComplexOperationHumanReadable As String = ""
        Private _JournalEntryID As Integer = 0
        Private _JournalEntryDate As Date = Today
        Private _JournalEntryContent As String = ""
        Private _JournalEntryCorrespondence As String = ""
        Private _JournalEntryRelatedPerson As String = ""
        Private _JournalEntryType As DocumentType = DocumentType.None
        Private _JournalEntryTypeHumanReadable As String = ""
        Private _JournalEntryDocNo As String = ""
        Private _Warehouse As WarehouseInfo = Nothing
        Private _OldWarehouseID As Integer = 0
        Private _Date As Date = Today
        Private _Description As String = ""
        Private _Amount As Double = 0
        Private _UnitCost As Double = 0
        Private _TotalCost As Double = 0
        Private _InsertDate As DateTime = Now
        Private _UpdateDate As DateTime = Now


        ''' <summary>
        ''' Gets an ID of the operation that is assigned by a database (AUTOINCREMENT).
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="OperationPersistenceObject.ID">OperationPersistenceObject.ID</see>.</remarks>
        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' Gets the date and time when the operation was inserted into the database.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="OperationPersistenceObject.InsertDate">OperationPersistenceObject.InsertDate</see>.</remarks>
        Public ReadOnly Property InsertDate() As DateTime
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _InsertDate
            End Get
        End Property

        ''' <summary>
        ''' Gets the date and time when the operation was last updated.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="OperationPersistenceObject.UpdateDate">OperationPersistenceObject.UpdateDate</see>.</remarks>
        Public ReadOnly Property UpdateDate() As DateTime
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _UpdateDate
            End Get
        End Property

        ''' <summary>
        ''' Gets an <see cref="ComplexOperationPersistenceObject.ID">ID of the complex 
        ''' goods operation</see> that the operation is a part of (if any).
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="OperationPersistenceObject.ComplexOperationID">OperationPersistenceObject.ComplexOperationID</see>.</remarks>
        Public ReadOnly Property ComplexOperationID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ComplexOperationID
            End Get
        End Property

        ''' <summary>
        ''' Gets a <see cref="ComplexOperationPersistenceObject.OperationType">type 
        ''' of the complex goods operation</see> that the operation is a part of.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="OperationPersistenceObject.ComplexOperationType">OperationPersistenceObject.ComplexOperationType</see>.</remarks>
        Public ReadOnly Property ComplexOperationType() As GoodsComplexOperationType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ComplexOperationType
            End Get
        End Property

        ''' <summary>
        ''' Gets a <see cref="ComplexOperationPersistenceObject.OperationType">localized
        ''' human readable type of the complex goods operation</see> that the operation 
        ''' is a part of (if any).
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="OperationPersistenceObject.ComplexOperationHumanReadable">OperationPersistenceObject.ComplexOperationHumanReadable</see>.</remarks>
        Public ReadOnly Property ComplexOperationHumanReadable() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ComplexOperationHumanReadable.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets <see cref="GoodsSummary">general information about the goods</see> 
        ''' that are redeemed by the operation.
        ''' </summary>
        ''' <remarks>Is set when creating a new operation and cannot be changed afterwards.
        ''' Corresponds to <see cref="OperationPersistenceObject.GoodsInfo">OperationPersistenceObject.GoodsInfo</see>.</remarks>
        Public ReadOnly Property GoodsInfo() As GoodsSummary
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _GoodsInfo
            End Get
        End Property

        ''' <summary>
        ''' Gets <see cref="IChronologicValidator">IChronologicValidator</see> object 
        ''' that contains business restraints on updating the operation data.
        ''' </summary>
        ''' <remarks>A <see cref="OperationalLimitList">OperationalLimitList</see> 
        ''' is used to validate a goods operation chronological business rules.</remarks>
        Public ReadOnly Property OperationLimitations() As OperationalLimitList
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _OperationLimitations
            End Get
        End Property

        ''' <summary>
        ''' Gets an <see cref="General.JournalEntry.ID">ID of the journal entry</see>
        ''' that is associated with the operation.
        ''' </summary>
        ''' <remarks>Invoke method <see cref="LoadAssociatedJournalEntry">LoadAssociatedJournalEntry</see>
        ''' to associate a journal entry with the operation.
        ''' Corresponds to <see cref="OperationPersistenceObject.JournalEntryID">OperationPersistenceObject.JournalEntryID</see>.</remarks>
        Public ReadOnly Property JournalEntryID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryID
            End Get
        End Property

        ''' <summary>
        ''' Gets a <see cref="General.JournalEntry.[Date]">date of the journal entry</see>
        ''' that is associated with the operation.
        ''' </summary>
        ''' <remarks>Invoke method <see cref="LoadAssociatedJournalEntry">LoadAssociatedJournalEntry</see>
        ''' to associate a journal entry with the operation.
        ''' Corresponds to <see cref="OperationPersistenceObject.JournalEntryDate">OperationPersistenceObject.JournalEntryDate</see>.</remarks>
        Public ReadOnly Property JournalEntryDate() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryDate
            End Get
        End Property

        ''' <summary>
        ''' Gets a <see cref="General.JournalEntry.Content">content of the journal entry</see>
        ''' that is associated with the operation.
        ''' </summary>
        ''' <remarks>Invoke method <see cref="LoadAssociatedJournalEntry">LoadAssociatedJournalEntry</see>
        ''' to associate a journal entry with the operation.
        ''' Corresponds to <see cref="OperationPersistenceObject.JournalEntryContent">OperationPersistenceObject.JournalEntryContent</see>.</remarks>
        Public ReadOnly Property JournalEntryContent() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryContent.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a <see cref="ActiveReports.JournalEntryInfo.BookEntries">
        ''' description of the book entries of the journal entry</see>
        ''' that is associated with the operation.
        ''' </summary>
        ''' <remarks>Invoke method <see cref="LoadAssociatedJournalEntry">LoadAssociatedJournalEntry</see>
        ''' to associate a journal entry with the operation.
        ''' Corresponds to <see cref="OperationPersistenceObject.JournalEntryCorrespondence">OperationPersistenceObject.JournalEntryCorrespondence</see>.</remarks>
        Public ReadOnly Property JournalEntryCorrespondence() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryCorrespondence.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a <see cref="General.JournalEntry.Person">person of the journal entry</see>
        ''' that is associated with the operation.
        ''' </summary>
        ''' <remarks>Invoke method <see cref="LoadAssociatedJournalEntry">LoadAssociatedJournalEntry</see>
        ''' to associate a journal entry with the operation.
        ''' Corresponds to <see cref="OperationPersistenceObject.JournalEntryRelatedPerson">OperationPersistenceObject.JournalEntryRelatedPerson</see>.</remarks>
        Public ReadOnly Property JournalEntryRelatedPerson() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryRelatedPerson.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a <see cref="General.JournalEntry.DocType">type of the journal entry</see>
        ''' that is associated with the operation.
        ''' </summary>
        ''' <remarks>Invoke method <see cref="LoadAssociatedJournalEntry">LoadAssociatedJournalEntry</see>
        ''' to associate a journal entry with the operation.
        ''' Corresponds to <see cref="OperationPersistenceObject.JournalEntryType">OperationPersistenceObject.JournalEntryType</see>.</remarks>
        Public ReadOnly Property JournalEntryType() As DocumentType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryType
            End Get
        End Property

        ''' <summary>
        ''' Gets a <see cref="General.JournalEntry.DocType">type of the journal entry</see>,
        ''' that is associated with the operation, as a localized human readable string.
        ''' </summary>
        ''' <remarks>Invoke method <see cref="LoadAssociatedJournalEntry">LoadAssociatedJournalEntry</see>
        ''' to associate a journal entry with the operation.
        ''' Corresponds to <see cref="OperationPersistenceObject.JournalEntryTypeHumanReadable">OperationPersistenceObject.JournalEntryTypeHumanReadable</see>.</remarks>
        Public ReadOnly Property JournalEntryTypeHumanReadable() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryTypeHumanReadable.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a <see cref="General.JournalEntry.DocNumber">document number 
        ''' of the journal entry</see> that is associated with the operation.
        ''' </summary>
        ''' <remarks>Invoke method <see cref="LoadAssociatedJournalEntry">LoadAssociatedJournalEntry</see>
        ''' to associate a journal entry with the operation.
        ''' Corresponds to <see cref="OperationPersistenceObject.JournalEntryDocNo">OperationPersistenceObject.JournalEntryDocNo</see>.</remarks>
        Public ReadOnly Property JournalEntryDocNo() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryDocNo.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets a <see cref="Goods.Warehouse">warehouse</see> that the goods 
        ''' are redeemed (transfered) from.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="OperationPersistenceObject.Warehouse">OperationPersistenceObject.Warehouse</see>.</remarks>
        Public Property Warehouse() As WarehouseInfo
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Warehouse
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As WarehouseInfo)
                CanWriteProperty(True)
                If WarehouseIsReadOnly Then Exit Property
                If Not (_Warehouse Is Nothing AndAlso value Is Nothing) _
                    AndAlso Not (Not _Warehouse Is Nothing AndAlso Not value Is Nothing _
                    AndAlso _Warehouse.ID = value.ID) Then

                    _Warehouse = value

                    _OperationLimitations.SetWarehouse(_Warehouse)

                    PropertyHasChanged()
                    PropertyHasChanged("AccountGoodsWarehouse")
                    PropertyHasChanged("OperationLimitations")

                    SetNullCosts()

                End If
            End Set
        End Property

        Public ReadOnly Property OldWarehouseID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _OldWarehouseID
            End Get
        End Property

        ''' <summary>
        ''' Gets a current <see cref="Goods.Warehouse.WarehouseAccount">warehouse account</see>.
        ''' Returns null (zero) if the goods are accounted using
        ''' <see cref="GoodsAccountingMethod.Periodic">Periodic accounting method</see>
        ''' or the <see cref="Warehouse">Warehouse</see> property is not set.
        ''' </summary>
        ''' <remarks>A proxy property to the <see cref="Warehouse">Warehouse</see>
        ''' to provide databing in a datagridview.</remarks>
        Public ReadOnly Property AccountGoodsWarehouse() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If _GoodsInfo.AccountingMethod = GoodsAccountingMethod.Periodic OrElse _
                    _Warehouse Is Nothing OrElse _Warehouse.IsEmpty Then Return 0
                Return _Warehouse.WarehouseAccount
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets a date of the operation.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="OperationPersistenceObject.OperationDate">OperationPersistenceObject.OperationDate</see>.</remarks>
        Public Property [Date]() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Date
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Date)
                CanWriteProperty(True)
                If DateIsReadOnly Then Exit Property
                If _Date.Date <> value.Date Then
                    _Date = value.Date
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a content (description) of the operation.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="OperationPersistenceObject.Content">OperationPersistenceObject.Content</see>.</remarks>
        <StringField(ValueRequiredLevel.Recommended, 255)> _
        Public Property Description() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Description.Trim
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As String)
                CanWriteProperty(True)
                If DescriptionIsReadOnly Then Exit Property
                If value Is Nothing Then value = ""
                If _Description.Trim <> value.Trim Then
                    _Description = value.Trim
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets an amount of the goods redeemed (transfered) by the operation.
        ''' </summary>
        ''' <remarks>Corresponds to minus <see cref="OperationPersistenceObject.Amount">OperationPersistenceObject.Amount</see>,
        ''' <see cref="ConsignmentDiscardPersistenceObject.Amount">ConsignmentDiscardPersistenceObject.Amount</see>
        ''' and (subject to the accounting method) minus 
        ''' <see cref="OperationPersistenceObject.AmountInWarehouse">OperationPersistenceObject.AmountInWarehouse</see>.</remarks>
        <DoubleField(ValueRequiredLevel.Mandatory, False, ROUNDAMOUNTGOODS)> _
        Public Property Amount() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_Amount, ROUNDAMOUNTGOODS)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Double)
                CanWriteProperty(True)
                If AmountIsReadOnly Then Exit Property
                If CRound(_Amount, ROUNDAMOUNTGOODS) <> CRound(value, ROUNDAMOUNTGOODS) Then

                    _Amount = CRound(value, ROUNDAMOUNTGOODS)
                    PropertyHasChanged()

                    SetNullCosts()

                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets a redeemed (transfered) goods value per unit.
        ''' </summary>
        ''' <remarks>Use <see cref="GoodsCostItem">goods cost query object</see> to fetch
        ''' costs for a discarded amount.
        ''' Is calculated as <see cref="TotalCost">TotalCost</see>
        ''' divided by <see cref="Amount">Amount</see>.
        ''' Final value is set by <see cref="ConsignmentDiscardPersistenceObjectList">
        ''' consignment discards persistence object</see>.
        ''' Corresponds to <see cref="OperationPersistenceObject.UnitValue">OperationPersistenceObject.UnitValue</see>.</remarks>
        <DoubleField(ValueRequiredLevel.Recommended, False, ROUNDUNITGOODS)> _
        Public ReadOnly Property UnitCost() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_UnitCost, ROUNDUNITGOODS)
            End Get
        End Property

        ''' <summary>
        ''' Gets a total redeemed (transfered) goods value.
        ''' </summary>
        ''' <remarks>Use <see cref="GoodsCostItem">goods cost query object</see> to fetch
        ''' costs for a discarded amount.
        ''' Final value is set by <see cref="ConsignmentDiscardPersistenceObjectList">
        ''' consignment discards persistence object</see>.
        ''' Corresponds to minus <see cref="OperationPersistenceObject.TotalValue">OperationPersistenceObject.TotalValue</see>,
        ''' <see cref="OperationPersistenceObject.AccountOperationValue">OperationPersistenceObject.AccountOperationValue</see>,
        ''' <see cref="ConsignmentDiscardPersistenceObject.TotalValue">ConsignmentDiscardPersistenceObject.TotalValue</see>
        ''' and subject to the <see cref="GoodsItem.AccountingMethod">accounting method 
        ''' of the goods</see> minus <see cref="OperationPersistenceObject.AccountGeneral">OperationPersistenceObject.AccountGeneral</see>.</remarks>
        <DoubleField(ValueRequiredLevel.Recommended, False, 2)> _
        Public ReadOnly Property TotalCost() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_TotalCost)
            End Get
        End Property




        ''' <summary>
        ''' Whether the <see cref="Warehouse">Warehouse</see> property is readonly.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property WarehouseIsReadOnly() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return Not _OperationLimitations.FinancialDataCanChange OrElse _
                    _ComplexOperationID > 0 OrElse (Not Me.IsChild AndAlso IsChildOperation)
            End Get
        End Property

        ''' <summary>
        ''' Whether the <see cref="Date">Date</see> property is readonly.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DateIsReadOnly() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ComplexOperationID > 0 OrElse (Not Me.IsChild AndAlso IsChildOperation)
            End Get
        End Property

        ''' <summary>
        ''' Whether the <see cref="Description">Description</see> property is readonly.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DescriptionIsReadOnly() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return Not Me.IsChild AndAlso IsChildOperation
            End Get
        End Property

        ''' <summary>
        ''' Whether the <see cref="Amount">Amount</see> property is readonly.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property AmountIsReadOnly() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return Not _OperationLimitations.FinancialDataCanChange OrElse _
                    (Not Me.IsChild AndAlso IsChildOperation)
            End Get
        End Property

        ''' <summary>
        ''' Whether the <see cref="LoadAssociatedJournalEntry">LoadAssociatedJournalEntry</see> 
        ''' method could be invoked (a new journal entry associated with the operation).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property AssociatedJournalEntryIsReadOnly() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return Me.IsChild OrElse IsChildOperation
            End Get
        End Property

        ''' <summary>
        ''' Whether the operation is actualy a child of a complex goods operation or
        ''' other document.
        ''' </summary>
        ''' <remarks>The <see cref="GoodsOperationAdditionalCosts.IsChild">IsChild</see>
        ''' property defines the current state of the object, i.e. whether the object was
        ''' fetched/created as a child). The IsChildOperation property defines a 
        ''' persistence state of the object, i.e. whether the object was originaly
        ''' saved as a child object.</remarks>
        Public ReadOnly Property IsChildOperation() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ComplexOperationID > 0 OrElse (_JournalEntryID > 0 AndAlso _
                    Not Array.IndexOf(ParentJournalEntryTypes, _JournalEntryType) < 0)
            End Get
        End Property


        Public ReadOnly Property IsDirtyEnough() As Boolean _
            Implements IIsDirtyEnough.IsDirtyEnough
            Get
                If Not IsNew Then Return IsDirty
                Return (Not String.IsNullOrEmpty(_Description.Trim) OrElse _
                    (Not _Warehouse Is Nothing AndAlso _Warehouse.ID > 0) OrElse _
                    CRound(_Amount, ROUNDAMOUNTGOODS) > 0)
            End Get
        End Property

        Public Overrides ReadOnly Property IsValid() As Boolean _
            Implements IValidationMessageProvider.IsValid
            Get
                Return MyBase.IsValid
            End Get
        End Property



        Public Function GetAllBrokenRules() As String _
            Implements IValidationMessageProvider.GetAllBrokenRules
            Dim result As String = ""
            If Not MyBase.IsValid Then result = AddWithNewLine(result, _
                Me.BrokenRulesCollection.ToString(Validation.RuleSeverity.Error), False)
            Return result
        End Function

        Public Function GetAllWarnings() As String _
            Implements IValidationMessageProvider.GetAllWarnings
            Dim result As String = ""
            If MyBase.BrokenRulesCollection.WarningCount > 0 Then
                result = AddWithNewLine(result, _
                    Me.BrokenRulesCollection.ToString(Validation.RuleSeverity.Warning), False)
            End If
            Return result
        End Function

        Public Function HasWarnings() As Boolean _
            Implements IValidationMessageProvider.HasWarnings
            Return MyBase.BrokenRulesCollection.WarningCount > 0
        End Function

        Public Function GetErrorString() As String _
            Implements IGetErrorForListItem.GetErrorString
            If IsValid Then Return ""
            Return String.Format(My.Resources.Common_ErrorInItem, Me.ToString, _
                vbCrLf, Me.GetAllBrokenRules())
        End Function

        Public Function GetWarningString() As String _
            Implements IGetErrorForListItem.GetWarningString
            If Not HasWarnings() Then Return ""
            Return String.Format(My.Resources.Common_WarningInItem, Me.ToString, _
                vbCrLf, Me.GetAllWarnings())
        End Function


        Public Overrides Function Save() As GoodsOperationRedeemToSupplier
            Return MyBase.Save
        End Function


        ''' <summary>
        ''' Associates a journal entry with the operation, i.e.
        ''' considers the journal entry as a base for the goods redeem (transfer) 
        ''' in the general ledger.
        ''' </summary>
        ''' <param name="entry">a journal entry to associate the operation with</param>
        ''' <remarks></remarks>
        Public Sub LoadAssociatedJournalEntry(ByVal entry As ActiveReports.JournalEntryInfo)

            If AssociatedJournalEntryIsReadOnly Then Exit Sub

            If entry Is Nothing OrElse Not entry.Id > 0 Then Exit Sub

            If Not Array.IndexOf(ParentJournalEntryTypes, entry.DocType) < 0 Then
                Throw New Exception(String.Format(Goods_GoodsOperationRedeemToSupplier_CannotAttachParentType, _
                    entry.DocTypeHumanReadable))
            ElseIf Array.IndexOf(AllowedJournalEntryTypes, entry.DocType) < 0 Then
                Throw New Exception(String.Format(Goods_GoodsOperationRedeemToSupplier_InvalidJournalEntryType, _
                    entry.DocTypeHumanReadable))
            End If

            _JournalEntryID = entry.Id
            _JournalEntryDate = entry.Date
            _JournalEntryContent = entry.Content
            _JournalEntryCorrespondence = entry.BookEntries
            _JournalEntryRelatedPerson = entry.Person
            _JournalEntryType = entry.DocType
            _JournalEntryTypeHumanReadable = entry.DocTypeHumanReadable
            _JournalEntryDocNo = entry.DocNumber

            PropertyHasChanged("JournalEntryID")
            PropertyHasChanged("JournalEntryDate")
            PropertyHasChanged("JournalEntryContent")
            PropertyHasChanged("JournalEntryCorrespondence")
            PropertyHasChanged("JournalEntryRelatedPerson")
            PropertyHasChanged("JournalEntryType")
            PropertyHasChanged("JournalEntryTypeHumanReadable")
            PropertyHasChanged("JournalEntryDocNo")

        End Sub

        ''' <summary>
        ''' Loads <see cref="UnitCost">UnitCost</see> and <see cref="TotalCost">TotalCost</see>
        ''' values from a <see cref="GoodsCostItem">query object</see>.
        ''' </summary>
        ''' <param name="costInfo">a query object</param>
        ''' <remarks></remarks>
        Public Sub LoadCostInfo(ByVal costInfo As GoodsCostItem)

            If _GoodsInfo.AccountingMethod = Goods.GoodsAccountingMethod.Periodic Then
                Throw New Exception(Goods_GoodsOperationTransfer_LoadCostInfoInvalid)
            End If
            If costInfo.GoodsID <> _GoodsInfo.ID Then
                Throw New ArgumentException(Goods_GoodsOperationTransfer_GoodsIdMismatch, "costInfo")
            End If
            If Not _Warehouse Is Nothing AndAlso costInfo.WarehouseID <> _Warehouse.ID Then
                Throw New ArgumentException(Goods_GoodsOperationTransfer_WarehouseIdMismatch, "costInfo")
            End If
            If costInfo.Amount <> CRound(_Amount, ROUNDAMOUNTGOODS) Then
                Throw New ArgumentException(Goods_GoodsOperationTransfer_AmountMismatch, "costInfo")
            End If
            If costInfo.ValuationMethod <> _GoodsInfo.ValuationMethod Then
                Throw New ArgumentException(Goods_GoodsOperationTransfer_ValuationMethodMismatch, "costInfo")
            End If
            If Not _OperationLimitations.FinancialDataCanChange Then
                Throw New Exception(String.Format(Goods_GoodsOperationTransfer_CannotChangeFinancialData, _
                    _GoodsInfo.Name, vbCrLf, _OperationLimitations.FinancialDataCanChangeExplanation))
            End If

            _UnitCost = costInfo.UnitCosts
            _TotalCost = costInfo.TotalCosts
            PropertyHasChanged("UnitCost")
            PropertyHasChanged("TotalCost")

        End Sub

        ''' <summary>
        ''' Gets a param object for a <see cref="GoodsCostItem">query object</see>.
        ''' </summary>
        ''' <remarks></remarks>
        Friend Function GetGoodsCostParam() As GoodsCostParam
            If _Warehouse Is Nothing OrElse _Warehouse.IsEmpty Then
                Throw New Exception(Goods_GoodsOperationDiscard_WarehouseNull)
            End If
            Return GoodsCostParam.NewGoodsCostParam(_GoodsInfo.ID, _Warehouse.ID, _
                _Amount, _ID, 0, _GoodsInfo.ValuationMethod, _Date)
        End Function

        ''' <summary>
        ''' Sets a new operation date as provided by the parent document.
        ''' </summary>
        ''' <param name="parentDate"></param>
        ''' <remarks></remarks>
        Friend Sub SetParentDate(ByVal parentDate As Date)
            If _Date.Date <> parentDate.Date Then
                _Date = parentDate.Date
                PropertyHasChanged("Date")
            End If
        End Sub

        ''' <summary>
        ''' Sets properties that are handled by a parent document 
        ''' and do not require realtime validation but do require validation before update.
        ''' </summary>
        ''' <param name="parentDocumentNumber">A parent document number.</param>
        ''' <param name="parentContent">A parent content.</param>
        ''' <remarks>Should be invoked before a parent document updates the operation.</remarks>
        Friend Sub SetParentProperties(ByVal parentDocumentNumber As String, _
            ByVal parentContent As String)

            ' nothing to do, method is kept for consistency accross the operations

        End Sub

        ''' <summary>
        ''' Sets <see cref="Description">Description</see> property.
        ''' </summary>
        ''' <param name="parentDescription">a parent content</param>
        ''' <remarks>If necessary, should be invoked before a parent document updates the operation.</remarks>
        Friend Sub SetDescription(ByVal parentDescription As String)

            If _Description.Trim <> parentDescription.Trim Then
                _Description = parentDescription.Trim
                PropertyHasChanged("Description")
            End If

        End Sub

        ''' <summary>
        ''' Sets a <see cref="Warehouse">warehouse</see> that is handled by a parent document.
        ''' </summary>
        ''' <param name="newWarehouse"></param>
        ''' <remarks>Should be invoked before a parent document updates the operation.</remarks>
        Friend Sub SetParentWarehouse(ByVal newWarehouse As WarehouseInfo)
            If Not (_Warehouse Is Nothing AndAlso newWarehouse Is Nothing) AndAlso _
                (newWarehouse Is Nothing OrElse _Warehouse Is Nothing OrElse _Warehouse.ID _
                 <> newWarehouse.ID) Then

                If Not _OperationLimitations.FinancialDataCanChange Then
                    Throw New Exception(String.Format(Goods_GoodsOperationRedeemToSupplier_CannotChangeFinancialData, _
                        _GoodsInfo.Name, vbCrLf, _OperationLimitations.FinancialDataCanChangeExplanation))
                End If

                _Warehouse = newWarehouse
                _OperationLimitations.SetWarehouse(_Warehouse)

                PropertyHasChanged("Warehouse")
                PropertyHasChanged("OperationLimitations")

                SetNullCosts()

            End If
        End Sub

        Friend Sub SetCosts(ByVal nUnitCost As Double, ByVal nTotalCost As Double)
            If CRound(_UnitCost, ROUNDUNITGOODS) <> CRound(nUnitCost, ROUNDUNITGOODS) Then
                _UnitCost = nUnitCost
                PropertyHasChanged("UnitCost")
            End If
            If CRound(_TotalCost, 2) <> CRound(nTotalCost, 2) Then
                _TotalCost = nTotalCost
                PropertyHasChanged("TotalCost")
            End If
        End Sub

        Private Sub SetNullCosts()
            _UnitCost = 0
            _TotalCost = 0
            PropertyHasChanged("UnitCost")
            PropertyHasChanged("TotalCost")
        End Sub


        Protected Overrides Function GetIdValue() As Object
            Return _Guid
        End Function

        Public Overrides Function ToString() As String
            Return String.Format(Goods_GoodsOperationRedeemToSupplier_ToString, _
                _GoodsInfo.Name, _ID.ToString)
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()

            ValidationRules.AddRule(AddressOf CommonValidation.DoubleFieldValidation, _
                New RuleArgs("Amount"))
            ValidationRules.AddRule(AddressOf CommonValidation.ValueObjectFieldValidation, _
                New CommonValidation.ExtendedRuleArgs("Warehouse", RuleSeverity.Error))

            ValidationRules.AddRule(AddressOf CommonValidation.ChronologyValidation, _
                New CommonValidation.ChronologyRuleArgs("Date", "OperationLimitations"))

            ValidationRules.AddRule(AddressOf DescriptionValidation, New RuleArgs("Description"))
            ValidationRules.AddRule(AddressOf JournalEntryValidation, New RuleArgs("JournalEntryID"))


            ValidationRules.AddDependantProperty("Date", "JournalEntryID", False)
            ValidationRules.AddDependantProperty("Warehouse", "Date", False)
            ValidationRules.AddDependantProperty("OperationLimitations", "Date", False)

        End Sub

        ''' <summary>
        ''' Rule ensuring that the value of property Description is valid.
        ''' </summary>
        ''' <param name="target">Object containing the data to validate</param>
        ''' <param name="e">Arguments parameter specifying the name of the string
        ''' property to validate</param>
        ''' <returns><see langword="false" /> if the rule is broken</returns>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
        Private Shared Function DescriptionValidation(ByVal target As Object, _
            ByVal e As Validation.RuleArgs) As Boolean
            If DirectCast(target, GoodsOperationRedeemToSupplier).IsChild Then Return True
            Return CommonValidation.StringFieldValidation(target, e)
        End Function

        ''' <summary>
        ''' Rule ensuring that associated journal entry is valid.
        ''' </summary>
        ''' <param name="target">Object containing the data to validate</param>
        ''' <param name="e">Arguments parameter specifying the name of the string
        ''' property to validate</param>
        ''' <returns><see langword="false" /> if the rule is broken</returns>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
        Private Shared Function JournalEntryValidation(ByVal target As Object, _
            ByVal e As Validation.RuleArgs) As Boolean

            Dim valObj As GoodsOperationRedeemToSupplier = DirectCast(target, GoodsOperationRedeemToSupplier)

            If valObj.IsChild Then Return True

            If Not valObj._JournalEntryID > 0 Then
                e.Description = Goods_GoodsOperationRedeemToSupplier_JournalEntryNull
                e.Severity = Validation.RuleSeverity.Error
                Return False
            ElseIf valObj._JournalEntryDate.Date <> valObj._Date.Date Then
                e.Description = Goods_GoodsOperationRedeemToSupplier_JournalEntryDateMismatch
                e.Severity = Validation.RuleSeverity.Error
                Return False
            End If

            Return True

        End Function

#End Region

#Region " Authorization Rules "

        Protected Overrides Sub AddAuthorizationRules()
            AuthorizationRules.AllowWrite("Goods.GoodsOperationRedeemToSupplier2")
        End Sub

        Public Shared Function CanGetObject() As Boolean
            Return ApplicationContext.User.IsInRole("Goods.GoodsOperationRedeemToSupplier1")
        End Function

        Public Shared Function CanAddObject() As Boolean
            Return ApplicationContext.User.IsInRole("Goods.GoodsOperationRedeemToSupplier2")
        End Function

        Public Shared Function CanEditObject() As Boolean
            Return ApplicationContext.User.IsInRole("Goods.GoodsOperationRedeemToSupplier3")
        End Function

        Public Shared Function CanDeleteObject() As Boolean
            Return ApplicationContext.User.IsInRole("Goods.GoodsOperationRedeemToSupplier3")
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets a new GoodsOperationRedeemToSupplier instance.
        ''' </summary>
        ''' <param name="goodsID">an <see cref="GoodsItem.ID">ID of the goods</see>
        ''' to redeem (transfer)</param>
        ''' <param name="warehouseID">an <see cref="Goods.Warehouse.ID">ID of the warehouse</see>
        ''' to redeem (transfer) the goods from</param>
        ''' <remarks></remarks>
        Public Shared Function NewGoodsOperationRedeemToSupplier(ByVal goodsID As Integer, _
            ByVal warehouseID As Integer) As GoodsOperationRedeemToSupplier
            Return DataPortal.Create(Of GoodsOperationRedeemToSupplier) _
                (New Criteria(goodsID, warehouseID))
        End Function

        ''' <summary>
        ''' Gets a new GoodsOperationRedeemToSupplier instance as a child of a
        ''' parent document (complex goods operation, invoice, etc.).
        ''' </summary>
        ''' <param name="summary">a <see cref="GoodsSummary">general info about
        ''' the goods to redeem (transfer)</see></param>
        ''' <param name="warehouse">a <see cref="Goods.Warehouse">warehouse</see>
        ''' to redeem (transfer) the goods from</param>
        ''' <param name="parentValidator">a chronologic validator of the parent document (if any)</param>
        ''' <remarks></remarks>
        Friend Shared Function NewGoodsOperationRedeemToSupplierChild(ByVal summary As GoodsSummary, _
            ByVal warehouse As WarehouseInfo, ByVal parentValidator As IChronologicValidator) As GoodsOperationRedeemToSupplier
            Return New GoodsOperationRedeemToSupplier(summary, warehouse, parentValidator)
        End Function

        ''' <summary>
        ''' Gets a new GoodsOperationRedeemToSupplier instance as a child of a
        ''' parent document (complex goods operation, invoice, etc.).
        ''' </summary>
        ''' <param name="goodsID">an <see cref="GoodsItem.ID">ID of the goods</see>
        ''' to redeem (transfer)</param>
        ''' <param name="warehouseID">an <see cref="Goods.Warehouse.ID">ID of the warehouse</see>
        ''' to redeem (transfer) the goods from</param>
        ''' <param name="parentValidator">a chronologic validator of the parent document (if any)</param>
        ''' <remarks></remarks>
        Friend Shared Function NewGoodsOperationRedeemToSupplierChild(ByVal goodsID As Integer, _
            ByVal warehouseID As Integer, ByVal parentValidator As IChronologicValidator) As GoodsOperationRedeemToSupplier
            Return New GoodsOperationRedeemToSupplier(goodsID, warehouseID, parentValidator)
        End Function

        ''' <summary>
        ''' Gets an existing GoodsOperationRedeemToSupplier instance from a database.
        ''' </summary>
        ''' <param name="id">an <see cref="ID">ID of the operation</see> to fetch</param>
        ''' <remarks></remarks>
        Public Shared Function GetGoodsOperationRedeemToSupplier(ByVal id As Integer) As GoodsOperationRedeemToSupplier
            Return DataPortal.Fetch(Of GoodsOperationRedeemToSupplier)(New Criteria(id))
        End Function

        ''' <summary>
        ''' Gets an existing GoodsOperationRedeemToSupplier instance as a child of a 
        ''' parent document from a database.
        ''' </summary>
        ''' <param name="obj">a persistence object containing the operation data</param>
        ''' <param name="parentValidator">a chronologic validator of the parent document (if any)</param>
        ''' <param name="limitationsDataSource">a datasource for the 
        ''' <see cref="OperationalLimitList">chronologic validator</see></param>
        ''' <remarks></remarks>
        Friend Shared Function GetGoodsOperationRedeemToSupplierChild( _
            ByVal obj As OperationPersistenceObject, _
            ByVal parentValidator As IChronologicValidator, _
            ByVal limitationsDataSource As DataTable) As GoodsOperationRedeemToSupplier
            Return New GoodsOperationRedeemToSupplier(obj, parentValidator, limitationsDataSource)
        End Function

        ''' <summary>
        ''' Gets an existing GoodsOperationRedeemToSupplier instance as a child of a 
        ''' parent document from a database.
        ''' </summary>
        ''' <param name="id">an <see cref="ID">ID of the operation</see> to fetch</param>
        ''' <param name="parentValidator">a chronologic validator of the parent document (if any)</param>
        ''' <remarks></remarks>
        Friend Shared Function GetGoodsOperationRedeemToSupplierChild(ByVal id As Integer, _
            ByVal parentValidator As IChronologicValidator) As GoodsOperationRedeemToSupplier
            Return New GoodsOperationRedeemToSupplier(id, parentValidator)
        End Function

        ''' <summary>
        ''' Deletes an existing GoodsOperationRedeemToSupplier instance from a database.
        ''' </summary>
        ''' <param name="id">an <see cref="ID">ID of the operation</see> to delete</param>
        ''' <remarks></remarks>
        Public Shared Sub DeleteGoodsOperationRedeemToSupplier(ByVal id As Integer)
            DataPortal.Delete(New Criteria(id))
        End Sub

        ''' <summary>
        ''' Deletes an existing GoodsOperationRedeemToSupplier instance as a child
        ''' of a parent document from a database.
        ''' </summary>
        ''' <remarks></remarks>
        Friend Sub DeleteGoodsOperationRedeemToSupplierChild()
            DoDelete()
        End Sub


        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal summary As GoodsSummary, ByVal warehouse As WarehouseInfo, _
            ByVal parentValidator As IChronologicValidator)
            ' require use of factory methods
            MarkAsChild()
            Create(summary, warehouse, parentValidator)
        End Sub

        Private Sub New(ByVal goodsID As Integer, ByVal warehouseID As Integer, _
            ByVal parentValidator As IChronologicValidator)
            ' require use of factory methods
            MarkAsChild()
            Create(goodsID, warehouseID, parentValidator)
        End Sub

        Private Sub New(ByVal obj As OperationPersistenceObject, _
            ByVal parentValidator As IChronologicValidator, ByVal limitationsDataSource As DataTable)
            ' require use of factory methods
            MarkAsChild()
            Fetch(obj, parentValidator, limitationsDataSource)
        End Sub

        Private Sub New(ByVal operationID As Integer, ByVal parentValidator As IChronologicValidator)
            ' require use of factory methods
            MarkAsChild()
            Fetch(operationID, parentValidator)
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> _
        Private Class Criteria
            Private mId As Integer
            Private mWarehouseId As Integer
            Public ReadOnly Property Id() As Integer
                Get
                    Return mId
                End Get
            End Property
            Public ReadOnly Property WarehouseId() As Integer
                Get
                    Return mWarehouseId
                End Get
            End Property
            Public Sub New(ByVal id As Integer)
                mId = id
                mWarehouseId = 0
            End Sub
            Public Sub New(ByVal nGoodsID As Integer, ByVal nWarehouseID As Integer)
                mId = nGoodsID
                mWarehouseId = nWarehouseID
            End Sub
        End Class


        <NonSerialized(), NotUndoable()> _
        Private DiscardList As ConsignmentDiscardPersistenceObjectList = Nothing

        Private Overloads Sub DataPortal_Create(ByVal criteria As Criteria)

            If Not CanAddObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecurityInsertDenied)

            Create(criteria.Id, criteria.WarehouseId, Nothing)

        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)

            If Not CanGetObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecuritySelectDenied)

            Fetch(criteria.Id, Nothing)

        End Sub

        Private Sub Fetch(ByVal operationID As Integer, ByVal parentValidator As IChronologicValidator)
            Dim obj As OperationPersistenceObject = OperationPersistenceObject. _
                GetOperationPersistenceObject(operationID, GoodsOperationType.RedeemToSupplier)
            Fetch(obj, parentValidator, Nothing)
        End Sub

        Private Sub Fetch(ByVal obj As OperationPersistenceObject, _
            ByVal parentValidator As IChronologicValidator, _
            ByVal limitationsDataSource As DataTable)

            If obj.OperationType <> GoodsOperationType.RedeemToSupplier Then
                Throw New Exception(String.Format(Goods_OperationPersistenceObject_OperationTypeMismatch, _
                    _ID.ToString, ConvertLocalizedName(obj.OperationType), _
                    ConvertLocalizedName(GoodsOperationType.RedeemToSupplier)))
            End If

            _ID = obj.ID
            _ComplexOperationID = obj.ComplexOperationID
            _ComplexOperationType = obj.ComplexOperationType
            _ComplexOperationHumanReadable = obj.ComplexOperationHumanReadable
            _Date = obj.OperationDate
            _Description = obj.Content
            _Amount = -obj.Amount
            _UnitCost = obj.UnitValue
            _TotalCost = -obj.TotalValue
            _Warehouse = obj.Warehouse
            _OldWarehouseID = obj.WarehouseID
            _JournalEntryID = obj.JournalEntryID
            _JournalEntryDate = obj.JournalEntryDate
            _JournalEntryContent = obj.JournalEntryContent
            _JournalEntryCorrespondence = obj.JournalEntryCorrespondence
            _JournalEntryRelatedPerson = obj.JournalEntryRelatedPerson
            _JournalEntryType = obj.JournalEntryType
            _JournalEntryTypeHumanReadable = obj.JournalEntryTypeHumanReadable
            _JournalEntryDocNo = obj.JournalEntryDocNo
            _InsertDate = obj.InsertDate
            _UpdateDate = obj.UpdateDate
            _GoodsInfo = obj.GoodsInfo

            _OperationLimitations = OperationalLimitList.GetOperationalLimitList( _
                _GoodsInfo, GoodsOperationType.RedeemToSupplier, _ID, _Date, _
                obj.WarehouseID, parentValidator, limitationsDataSource)

            MarkOld()

            ValidationRules.CheckRules()

        End Sub

        Private Sub Create(ByVal goodsID As Integer, ByVal warehouseID As Integer, _
            ByVal parentValidator As IChronologicValidator)
            Dim curSummary As GoodsSummary = GoodsSummary.NewGoodsSummary(goodsID)
            Dim curWarehouse As WarehouseInfo = WarehouseInfoList.GetListChild.GetItem( _
                warehouseID, curSummary.DefaultWarehouseID, False)
            Create(curSummary, curWarehouse, parentValidator)
        End Sub

        Private Sub Create(ByVal summary As GoodsSummary, ByVal warehouse As WarehouseInfo, _
            ByVal parentValidator As IChronologicValidator)

            _GoodsInfo = summary

            _Warehouse = warehouse
            Dim wd As Integer = 0
            If Not _Warehouse Is Nothing AndAlso Not _Warehouse.IsEmpty Then
                wd = _Warehouse.ID
            End If

            _OperationLimitations = OperationalLimitList.NewOperationalLimitList( _
                _GoodsInfo, GoodsOperationType.RedeemToSupplier, wd, parentValidator)

            MarkNew()

            ValidationRules.CheckRules()

        End Sub


        Protected Overrides Sub DataPortal_Insert()

            If Not CanAddObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecurityInsertDenied)

            CheckIfCanSaveRoot()

            CheckIfCanUpdate(Nothing, Nothing)

            DoSave(True, False, False)

            _OperationLimitations = OperationalLimitList.GetOperationalLimitList( _
                _GoodsInfo, GoodsOperationType.RedeemToSupplier, _ID, _Date, _
                _Warehouse.ID, Nothing, Nothing)

        End Sub

        Protected Overrides Sub DataPortal_Update()

            If Not CanEditObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecurityUpdateDenied)

            CheckIfCanSaveRoot()

            CheckIfCanUpdate(Nothing, Nothing)

            DoSave(True, False, False)

            _OperationLimitations = OperationalLimitList.GetOperationalLimitList( _
                _GoodsInfo, GoodsOperationType.RedeemToSupplier, _ID, _Date, _
                _Warehouse.ID, Nothing, Nothing)

        End Sub

        ''' <summary>
        ''' Saves a child GoodsOperationRedeemToSupplier instance to a database.
        ''' </summary>
        ''' <param name="parentJournalEntryID">an <see cref="General.JournalEntry.ID">
        ''' ID of the journal entry</see> of the parent document</param>
        ''' <param name="parentComplexOperationID">an <see cref="ComplexOperationPersistenceObject.ID">
        ''' ID of the parent complex goods operation</see></param>
        ''' <param name="manageConsignments">whether the operation should manage
        ''' required goods consignments discards by itself (or leave it to the parent document)</param>
        ''' <param name="forceFinancialChangeForPeriodic">whether the transfer reduces
        ''' goods amount in the <see cref="GoodsItem.AccountPurchases">purchases account</see>
        ''' (as far as I'm concerned only true for the internal transfer).</param>
        ''' <param name="financialDataReadOnly">whether the parent document allows
        ''' the operation to change it's financial data</param>
        ''' <remarks></remarks>
        Friend Sub SaveChild(ByVal parentJournalEntryID As Integer, _
            ByVal parentComplexOperationID As Integer, ByVal manageConsignments As Boolean, _
            ByVal forceFinancialChangeForPeriodic As Boolean, ByVal financialDataReadOnly As Boolean)
            _JournalEntryID = parentJournalEntryID
            _ComplexOperationID = parentComplexOperationID
            DoSave(manageConsignments, forceFinancialChangeForPeriodic, financialDataReadOnly)
        End Sub

        Private Sub DoSave(ByVal manageConsignments As Boolean, _
            ByVal forceFinancialChangeForPeriodic As Boolean, _
            ByVal financialDataReadOnly As Boolean)

            Dim discards As ConsignmentDiscardPersistenceObjectList = Nothing
            If manageConsignments AndAlso Not financialDataReadOnly Then
                discards = GetDiscardList()
            End If

            Dim obj As OperationPersistenceObject = GetPersistenceObj(forceFinancialChangeForPeriodic)

            Using transaction As New SqlTransaction

                Try

                    obj = obj.Save(_OperationLimitations.FinancialDataCanChange _
                        AndAlso Not financialDataReadOnly)

                    If IsNew Then
                        _ID = obj.ID
                        _InsertDate = obj.InsertDate
                    End If
                    _UpdateDate = obj.UpdateDate

                    If Not discards Is Nothing Then
                        discards.Update(_ID)
                    End If

                    transaction.Commit()

                Catch ex As Exception
                    transaction.SetNonSqlException(ex)
                    Throw
                End Try

            End Using

            _OldWarehouseID = _Warehouse.ID

            MarkOld()

        End Sub

        Private Function GetPersistenceObj(ByVal forceFinancialChangeForPeriodic As Boolean) As OperationPersistenceObject

            If Not IsChild Then forceFinancialChangeForPeriodic = False

            Dim obj As OperationPersistenceObject
            If IsNew Then
                obj = OperationPersistenceObject.NewOperationPersistenceObject( _
                    GoodsOperationType.RedeemToSupplier, _GoodsInfo.ID)
            Else
                obj = OperationPersistenceObject.GetOperationPersistenceObjectForSave( _
                    _ID, GoodsOperationType.RedeemToSupplier, _UpdateDate, IsChild)
            End If

            If _GoodsInfo.AccountingMethod = GoodsAccountingMethod.Persistent Then

                obj.AccountGeneral = -_TotalCost
                obj.AccountOperationValue = _TotalCost
                obj.AmountInWarehouse = -_Amount
                obj.AmountInPurchases = 0
                obj.TotalValue = -_TotalCost
                obj.UnitValue = _UnitCost

            Else

                If forceFinancialChangeForPeriodic Then
                    obj.AmountInPurchases = -_Amount
                    obj.AccountPurchases = -_TotalCost
                    obj.TotalValue = -_TotalCost
                    obj.UnitValue = _UnitCost
                Else
                    obj.AmountInPurchases = 0
                    obj.AccountPurchases = 0
                    obj.TotalValue = 0
                    obj.UnitValue = 0
                End If
                obj.AmountInWarehouse = 0

            End If

            obj.Amount = -_Amount
            obj.Content = _Description
            obj.DocNo = ""
            obj.JournalEntryID = _JournalEntryID
            obj.OperationDate = _Date
            obj.Warehouse = _Warehouse
            obj.WarehouseID = _Warehouse.ID
            obj.ComplexOperationID = _ComplexOperationID

            Return obj

        End Function


        Protected Overrides Sub DataPortal_DeleteSelf()
            DataPortal_Delete(New Criteria(_ID))
        End Sub

        Protected Overrides Sub DataPortal_Delete(ByVal criteria As Object)

            If Not CanDeleteObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecurityUpdateDenied)

            Dim operationToDelete As GoodsOperationRedeemToSupplier = New GoodsOperationRedeemToSupplier
            operationToDelete.Fetch(DirectCast(criteria, Criteria).Id, Nothing)

            If operationToDelete.ComplexOperationID > 0 Then
                Throw New Exception(String.Format(Goods_GoodsOperationRedeemToSupplier_InvalidSaveChild, _
                    operationToDelete._ComplexOperationHumanReadable))
            ElseIf Not Array.IndexOf(ParentJournalEntryTypes, operationToDelete._JournalEntryType) < 0 Then
                Throw New Exception(String.Format(Goods_GoodsOperationRedeemToSupplier_InvalidSaveChild, _
                    operationToDelete._JournalEntryTypeHumanReadable))
            End If

            If Not operationToDelete._OperationLimitations.FinancialDataCanChange Then
                Throw New Exception(String.Format(Goods_GoodsOperationRedeemToSupplier_InvalidDelete, _
                    operationToDelete._GoodsInfo.Name, vbCrLf, operationToDelete._OperationLimitations. _
                    FinancialDataCanChangeExplanation))
            End If

            operationToDelete.DoDelete()

        End Sub

        Private Sub DoDelete()

            Using transaction As New SqlTransaction

                Try

                    OperationPersistenceObject.Delete(_ID, False, True)

                    transaction.Commit()

                Catch ex As Exception
                    transaction.SetNonSqlException(ex)
                    Throw
                End Try

            End Using

            MarkNew()

        End Sub


        ''' <summary>
        ''' Gets a total book entry list required for the goods redeem (transfer).
        ''' </summary>
        ''' <param name="preloadCostsValues">whether to preload 
        ''' <see cref="TotalCost">TotalCost</see> and <see cref="UnitCost">UnitCost</see> values</param>
        ''' <remarks>The method inter alia preloads <see cref="TotalCost">TotalCost</see> and
        ''' <see cref="UnitCost">UnitCost</see> values in order to fetch correct 
        ''' book entries. However preload yields invalid results if a parent operation 
        ''' contains multiple transfer (or discard) operations with the same goods 
        ''' in the same warehouse. (they are not ""visible"" to each other 
        ''' therefore the consignments to discard overlap) In the latter case
        ''' a journal entry has to be saved twice by the parent operation -
        ''' before the transfer (or discard) operations (to pass an ID of the journal entry)
        ''' and after the transfer (or discard) operations (to get correct book entries values).</remarks>
        Friend Function GetTotalBookEntryList(ByVal preloadCostsValues As Boolean) As BookEntryInternalList

            Dim result As BookEntryInternalList = _
               BookEntryInternalList.NewBookEntryInternalList(BookEntryType.Debetas)

            If _GoodsInfo.AccountingMethod = GoodsAccountingMethod.Periodic Then Return result

            If preloadCostsValues AndAlso _OperationLimitations.FinancialDataCanChange Then
                GetDiscardList()
            End If

            result.Add(BookEntryInternal.NewBookEntryInternal(BookEntryType.Kreditas, _
                _Warehouse.WarehouseAccount, CRound(_TotalCost)))

            'result.Add(BookEntryInternal.NewBookEntryInternal(BookEntryType.Debetas, _
            '    _AccountGoodsCost, CRound(_TotalCost)))

            Return result

        End Function

        Friend Sub CheckIfCanDelete(ByVal limitationsDataSource As DataTable, _
            ByVal parentValidator As IChronologicValidator)

            If IsNew Then Exit Sub

            _OperationLimitations = OperationalLimitList.GetUpdatedOperationalLimitList( _
                _OperationLimitations, parentValidator, limitationsDataSource)

            If Not _OperationLimitations.FinancialDataCanChange Then
                Throw New Exception(String.Format(Goods_GoodsOperationRedeemToSupplier_InvalidDelete, _
                    _GoodsInfo.Name, vbCrLf, _OperationLimitations.FinancialDataCanChangeExplanation))
            End If

        End Sub

        Friend Sub CheckIfCanUpdate(ByVal limitationsDataSource As DataTable, _
            ByVal parentValidator As IChronologicValidator)

            _OperationLimitations = OperationalLimitList.GetUpdatedOperationalLimitList( _
                _OperationLimitations, parentValidator, limitationsDataSource)

            ValidationRules.CheckRules()
            If Not IsValid Then
                Throw New Exception(String.Format(My.Resources.Common_ContainsErrors, vbCrLf, _
                    GetErrorString()))
            End If

        End Sub


        ''' <summary>
        ''' Fetches the final <see cref="TotalCost">TotalCost</see> and
        ''' <see cref="UnitCost">UnitCost</see> values. However it yields invalid
        ''' results if a parent operation contains multiple transfer (or discard)
        ''' operations with the same goods in the same warehouse. (they are not 
        ''' ""visible"" to each other therefore the consignments to discard overlap) 
        ''' In the latter case a journal entry has to be saved twice by the parent operation -
        ''' before the transfer (or discard) operations (to pass an ID of the journal entry)
        ''' and after the transfer (or discard) operations (to get correct book entries values).
        ''' </summary>
        ''' <remarks></remarks>
        Friend Sub PreloadCosts()
            GetDiscardList()
        End Sub

        Private Function GetDiscardList() As ConsignmentDiscardPersistenceObjectList

            Dim result As ConsignmentDiscardPersistenceObjectList = Nothing

            If Not _OperationLimitations.FinancialDataCanChange OrElse _
                _GoodsInfo.AccountingMethod = Goods.GoodsAccountingMethod.Periodic Then
                Return result
            End If

            Dim consignments As ConsignmentPersistenceObjectList = _
                ConsignmentPersistenceObjectList.NewConsignmentPersistenceObjectList( _
                _GoodsInfo.ID, _Warehouse.ID, _ID, 0, (_GoodsInfo.ValuationMethod _
                = Goods.GoodsValuationMethod.LIFO))

            consignments.RemoveLateEntries(_Date)

            Dim discards As ConsignmentDiscardPersistenceObjectList = _
                ConsignmentDiscardPersistenceObjectList.NewConsignmentDiscardPersistenceObjectList( _
                consignments, _Amount, _GoodsInfo.Name)

            If Not IsNew Then

                Dim currentDiscards As ConsignmentDiscardPersistenceObjectList = _
                    ConsignmentDiscardPersistenceObjectList. _
                    GetConsignmentDiscardPersistenceObjectList(_ID)

                currentDiscards.MergeChangedList(discards)

                result = currentDiscards

            Else

                result = discards

            End If

            _TotalCost = result.GetTotalValue
            _Amount = result.GetTotalAmount

            If Not CRound(_Amount, ROUNDAMOUNTGOODS) > 0 Then
                Throw New InvalidDataException(Goods_GoodsOperationTransfer_InvalidAmountInDiscardList)
            End If

            _UnitCost = CRound(_TotalCost / _Amount, ROUNDUNITGOODS)

            Return result

        End Function

        Private Sub CheckIfCanSaveRoot()

            If _ComplexOperationID > 0 Then
                Throw New Exception(String.Format(Goods_GoodsOperationRedeemToSupplier_InvalidSaveChild, _
                    _ComplexOperationHumanReadable))
            ElseIf Not Array.IndexOf(ParentJournalEntryTypes, _JournalEntryType) < 0 Then
                Throw New Exception(String.Format(Goods_GoodsOperationRedeemToSupplier_InvalidSaveChild, _
                    _JournalEntryTypeHumanReadable))
            End If

        End Sub

#End Region

    End Class

End Namespace