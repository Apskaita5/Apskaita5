﻿Imports ApskaitaObjects.My.Resources
Imports Csla.Validation

Namespace Goods

    ''' <summary>
    ''' Represents a complex goods internal transfer operation, that is composed of 
    ''' a collection of <see cref="GoodsInternalTransferItem">goods internal transfer
    ''' operations</see>, that in turn are composed of a <see cref="GoodsOperationTransfer">
    ''' simple goods transfer operation</see> and <see cref="GoodsOperationAcquisition">
    ''' simple goods acquisition operation</see> pair. I.e. transfers multiple goods
    ''' from one warehouse to another.
    ''' </summary>
    ''' <remarks>See methodology for BAS No 9 ""Stores"" para. 40 for details.
    ''' Encapsulates a <see cref="General.JournalEntry">JournalEntry</see>
    ''' of type <see cref="DocumentType.GoodsInternalTransfer">DocumentType.GoodsInternalTransfer</see>.
    ''' Values are stored using <see cref="ComplexOperationPersistenceObject">ComplexOperationPersistenceObject</see>.</remarks>
    <Serializable()> _
    Public NotInheritable Class GoodsComplexOperationInternalTransfer
        Inherits BusinessBase(Of GoodsComplexOperationInternalTransfer)
        Implements IIsDirtyEnough, IValidationMessageProvider

#Region " Business Methods "

        Private _ID As Integer = 0
        Private _JournalEntryID As Integer = 0
        Private _OperationalLimitAcquisition As ComplexChronologicValidator = Nothing
        Private _OperationalLimitTransfer As ComplexChronologicValidator = Nothing
        Private _Date As Date = Today
        Private _DocumentNumber As String = ""
        Private _Content As String = ""
        Private _OldWarehouseFromID As Integer = 0
        Private _OldWarehouseToID As Integer = 0
        Private _WarehouseFrom As WarehouseInfo = Nothing
        Private _WarehouseTo As WarehouseInfo = Nothing
        Private _InsertDate As DateTime = Now
        Private _UpdateDate As DateTime = Now
        Private WithEvents _Items As GoodsInternalTransferItemList

        ' used to implement automatic sort in datagridview
        <NotUndoable()> _
        <NonSerialized()> _
        Private _ItemsSortedList As Csla.SortedBindingList(Of GoodsInternalTransferItem) = Nothing


        ''' <summary>
        ''' Gets an ID of the operation that is assigned by a database (AUTOINCREMENT).
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="ComplexOperationPersistenceObject.ID">ComplexOperationPersistenceObject.ID</see>.</remarks>
        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' Gets an <see cref="General.JournalEntry.ID">ID of the journal entry</see>
        ''' that is encapsulated by the operation.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="ComplexOperationPersistenceObject.JournalEntryID">ComplexOperationPersistenceObject.JournalEntryID</see>.</remarks>
        Public ReadOnly Property JournalEntryID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryID
            End Get
        End Property

        ''' <summary>
        ''' Gets the date and time when the operation was inserted into the database.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="ComplexOperationPersistenceObject.InsertDate">ComplexOperationPersistenceObject.InsertDate</see>.</remarks>
        Public ReadOnly Property InsertDate() As DateTime
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _InsertDate
            End Get
        End Property

        ''' <summary>
        ''' Gets the date and time when the operation was last updated.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="ComplexOperationPersistenceObject.UpdateDate">ComplexOperationPersistenceObject.UpdateDate</see>.</remarks>
        Public ReadOnly Property UpdateDate() As DateTime
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _UpdateDate
            End Get
        End Property

        ''' <summary>
        ''' Gets <see cref="IChronologicValidator">IChronologicValidator</see> object 
        ''' that contains business restraints on updating the operation data
        ''' from the transfer perspective.
        ''' </summary>
        ''' <remarks>A <see cref="ComplexChronologicValidator">ComplexChronologicValidator</see> 
        ''' is used to validate a complex goods operation chronological business rules.</remarks>
        Public ReadOnly Property OperationalLimitTransfer() As ComplexChronologicValidator
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _OperationalLimitTransfer
            End Get
        End Property

        ''' <summary>
        ''' Gets <see cref="IChronologicValidator">IChronologicValidator</see> object 
        ''' that contains business restraints on updating the operation data
        ''' from the acquisition perspective.
        ''' </summary>
        ''' <remarks>A <see cref="ComplexChronologicValidator">ComplexChronologicValidator</see> 
        ''' is used to validate a complex goods operation chronological business rules.</remarks>
        Public ReadOnly Property OperationalLimitAcquisition() As ComplexChronologicValidator
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _OperationalLimitAcquisition
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets a date of the operation.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="ComplexOperationPersistenceObject.OperationDate">ComplexOperationPersistenceObject.OperationDate</see>.</remarks>
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
                    _Items.SetParentDate(_Date)
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a document number of the operation.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="ComplexOperationPersistenceObject.DocNo">ComplexOperationPersistenceObject.DocNo</see>.</remarks>
        <StringField(ValueRequiredLevel.Mandatory, 30)> _
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
        ''' Gets or sets a content (description) of the operation.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="ComplexOperationPersistenceObject.Content">ComplexOperationPersistenceObject.Content</see>.</remarks>
        <StringField(ValueRequiredLevel.Mandatory, 255)> _
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

        Public ReadOnly Property OldWarehouseFromID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _OldWarehouseFromID
            End Get
        End Property

        Public ReadOnly Property OldWarehouseToID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _OldWarehouseToID
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets a <see cref="Goods.Warehouse">warehouse</see> that the goods 
        ''' are transfered from.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="ComplexOperationPersistenceObject.Warehouse">ComplexOperationPersistenceObject.Warehouse</see>.</remarks>
        <WarehouseField(ValueRequiredLevel.Mandatory)> _
        Public Property WarehouseFrom() As WarehouseInfo
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _WarehouseFrom
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As WarehouseInfo)
                CanWriteProperty(True)
                If WarehouseFromIsReadOnly Then Exit Property
                If Not (_WarehouseFrom Is Nothing AndAlso value Is Nothing) _
                    AndAlso Not (Not _WarehouseFrom Is Nothing AndAlso Not value Is Nothing _
                    AndAlso _WarehouseFrom.ID = value.ID) Then

                    _WarehouseFrom = value

                    _Items.SetWarehouseFrom(value)
                    _OperationalLimitTransfer = ComplexChronologicValidator.GetComplexChronologicValidator( _
                        _OperationalLimitTransfer, _Items.GetLimitations(False))

                    PropertyHasChanged()
                    PropertyHasChanged("OperationalLimitTransfer")

                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a <see cref="Goods.Warehouse">warehouse</see> that the goods 
        ''' are transfered to.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="ComplexOperationPersistenceObject.SecondaryWarehouse">ComplexOperationPersistenceObject.SecondaryWarehouse</see>.</remarks>
        <WarehouseField(ValueRequiredLevel.Mandatory)> _
        Public Property WarehouseTo() As WarehouseInfo
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _WarehouseTo
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As WarehouseInfo)
                CanWriteProperty(True)
                If WarehouseToIsReadOnly Then Exit Property
                If Not (_WarehouseTo Is Nothing AndAlso value Is Nothing) _
                    AndAlso Not (Not _WarehouseTo Is Nothing AndAlso Not value Is Nothing _
                    AndAlso _WarehouseTo.ID = value.ID) Then

                    _WarehouseTo = value

                    _Items.SetWarehouseTo(value)
                    _OperationalLimitAcquisition = ComplexChronologicValidator.GetComplexChronologicValidator( _
                        _OperationalLimitAcquisition, _Items.GetLimitations(True))

                    PropertyHasChanged()
                    PropertyHasChanged("OperationalLimitAcquisition")

                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets a collection of goods internal transfer items within the operation.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Items() As GoodsInternalTransferItemList
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Items
            End Get
        End Property

        ''' <summary>
        ''' Gets a sortable collection of goods internal transfer items within the operation.
        ''' </summary>
        ''' <remarks>Used to implement autosort in a datagridview.</remarks>
        Public ReadOnly Property ItemsSorted() As Csla.SortedBindingList(Of GoodsInternalTransferItem)
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If _ItemsSortedList Is Nothing Then
                    _ItemsSortedList = New Csla.SortedBindingList(Of GoodsInternalTransferItem)(_Items)
                End If
                Return _ItemsSortedList
            End Get
        End Property


        ''' <summary>
        ''' Whether the <see cref="WarehouseFrom">WarehouseFrom</see> property is readonly.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property WarehouseFromIsReadOnly() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return (Not _OperationalLimitTransfer.FinancialDataCanChange OrElse _
                    Not _OperationalLimitTransfer.ChildrenFinancialDataCanChange OrElse _
                    Not _OperationalLimitAcquisition.ChildrenFinancialDataCanChange)
            End Get
        End Property

        ''' <summary>
        ''' Whether the <see cref="WarehouseTo">WarehouseTo</see> property is readonly.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property WarehouseToIsReadOnly() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return (Not _OperationalLimitTransfer.FinancialDataCanChange _
                    OrElse Not _OperationalLimitAcquisition.ChildrenFinancialDataCanChange)
            End Get
        End Property

        ''' <summary>
        ''' Whether the <see cref="RefreshCosts">RefreshCosts</see> method could be invoked,
        ''' i.e. <see cref="GoodsInternalTransferItem.UnitCost">UnitCost</see> and 
        ''' <see cref="GoodsInternalTransferItem.TotalCost">TotalCost</see> can be set.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property RefreshCostsIsReadOnly() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return Not _OperationalLimitAcquisition.FinancialDataCanChange _
                    OrElse Not _OperationalLimitAcquisition.ChildrenFinancialDataCanChange _
                    OrElse _OperationalLimitTransfer.ChildrenFinancialDataCanChange
            End Get
        End Property


        Public ReadOnly Property IsDirtyEnough() As Boolean _
            Implements IIsDirtyEnough.IsDirtyEnough
            Get
                If Not IsNew Then Return IsDirty
                Return (Not String.IsNullOrEmpty(_DocumentNumber.Trim) _
                    OrElse Not String.IsNullOrEmpty(_Content.Trim) _
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
            Return Me.BrokenRulesCollection.WarningCount > 0 OrElse _Items.HasWarnings
        End Function


        ''' <summary>
        ''' Gets an array of param objects for a <see cref="GoodsCostItemList">query object</see>.
        ''' </summary>
        ''' <remarks></remarks>
        Public Function GetCostsParams() As GoodsCostParam()
            If _WarehouseFrom Is Nothing OrElse _WarehouseFrom.IsEmpty Then
                Throw New Exception(Goods_GoodsComplexOperationInternalTransfer_WarehouseFromNull)
            End If
            Return _Items.GetGoodsCostsParams()
        End Function

        ''' <summary>
        ''' Sets costs of the goods to transfer using <see cref="GoodsCostItemList">
        ''' query object</see>.
        ''' </summary>
        ''' <param name="values">a query object containing information about the costs 
        ''' of the goods for a given amount, warehouse and date.</param>
        ''' <param name="warnings">an out parameter that returns a description of 
        ''' non critical errors encountered while seting the data</param>
        ''' <remarks></remarks>
        Public Sub RefreshCosts(ByVal values As GoodsCostItemList, _
            ByRef warnings As String)

            warnings = ""

            If Not _OperationalLimitAcquisition.FinancialDataCanChange Then
                Throw New Exception(String.Format(Goods_GoodsComplexOperationInternalTransfer_CannotChangeFinancialDataParent, _
                    vbCrLf, _OperationalLimitAcquisition.FinancialDataCanChangeExplanation))
            End If

            _Items.RefreshCosts(values, warnings)

        End Sub

        ''' <summary>
        ''' Sets costs of the goods to transfer using <see cref="GoodsCostItem">
        ''' query object</see>.
        ''' </summary>
        ''' <param name="value">a query object containing information about the costs 
        ''' of the given goods for a given amount, warehouse and date.</param>
        ''' <remarks></remarks>
        Public Sub RefreshCosts(ByVal value As GoodsCostItem)

            If Not _OperationalLimitAcquisition.FinancialDataCanChange Then
                Throw New Exception(String.Format(Goods_GoodsComplexOperationInternalTransfer_CannotChangeFinancialDataParent, _
                    vbCrLf, _OperationalLimitAcquisition.FinancialDataCanChangeExplanation))
            End If

            _Items.RefreshCosts(value)

        End Sub

        ''' <summary>
        ''' Adds items in the list to the current collection.
        ''' </summary>
        ''' <param name="list"></param>
        ''' <remarks>Invoke <see cref="GoodsInternalTransferItemList.NewGoodsInternalTransferItemList">GoodsInternalTransferItemList.NewGoodsInternalTransferItemList</see>
        ''' to get a list of new child operations by goods ID's.</remarks>
        Public Sub AddRange(ByVal list As GoodsInternalTransferItemList)

            If Not _OperationalLimitAcquisition.FinancialDataCanChange Then
                Throw New Exception(String.Format(Goods_GoodsComplexOperationInternalTransfer_CannotChangeFinancialDataParent, _
                    vbCrLf, _OperationalLimitAcquisition.FinancialDataCanChangeExplanation))
            End If

            list.SetParentDate(_Date)
            list.SetWarehouseFrom(_WarehouseFrom)
            list.SetWarehouseTo(_WarehouseTo)

            _Items.AddRange(list)

            For Each i As GoodsInternalTransferItem In list
                _OperationalLimitAcquisition.MergeNewValidationItem(i.ChronologyValidatorAcquisition)
                _OperationalLimitTransfer.MergeNewValidationItem(i.ChronologyValidatorTransfer)
            Next

            PropertyHasChanged("OperationalLimitAcquisition")
            PropertyHasChanged("OperationalLimitTransfer")

        End Sub


        Public Overrides Function Save() As GoodsComplexOperationInternalTransfer
            Return MyBase.Save
        End Function


        Private Sub Items_Changed(ByVal sender As Object, _
            ByVal e As System.ComponentModel.ListChangedEventArgs) Handles _Items.ListChanged

            If e.ListChangedType = ComponentModel.ListChangedType.ItemAdded Then

                Try
                    _OperationalLimitAcquisition.MergeNewValidationItem( _
                        _Items(e.NewIndex).ChronologyValidatorAcquisition)
                    PropertyHasChanged("OperationalLimitAcquisition")
                Catch ex As Exception
                End Try
                Try
                    _OperationalLimitTransfer.MergeNewValidationItem( _
                        _Items(e.NewIndex).ChronologyValidatorTransfer)
                    PropertyHasChanged("OperationalLimitTransfer")
                Catch ex As Exception
                End Try

            ElseIf e.ListChangedType = ComponentModel.ListChangedType.ItemDeleted Then

                _OperationalLimitAcquisition = ComplexChronologicValidator. _
                    GetComplexChronologicValidator(_OperationalLimitAcquisition, _
                    _Items.GetLimitations(True))
                _OperationalLimitTransfer = ComplexChronologicValidator. _
                    GetComplexChronologicValidator(_OperationalLimitTransfer, _
                    _Items.GetLimitations(False))

                PropertyHasChanged("OperationalLimitAcquisition")
                PropertyHasChanged("OperationalLimitTransfer")

            End If

        End Sub

        ''' <summary>
        ''' Helper method. Takes care of child lists loosing their handlers.
        ''' </summary>
        Protected Overrides Function GetClone() As Object
            Dim result As GoodsComplexOperationInternalTransfer = _
                DirectCast(MyBase.GetClone(), GoodsComplexOperationInternalTransfer)
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
            Return String.Format(Goods_GoodsComplexOperationInternalTransfer_ToString, _
                _Date.ToString("yyyy-MM-dd"), _DocumentNumber, _ID.ToString)
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()

            ValidationRules.AddRule(AddressOf CommonValidation.StringFieldValidation, _
                New RuleArgs("DocumentNumber"))
            ValidationRules.AddRule(AddressOf CommonValidation.StringFieldValidation, _
                New RuleArgs("Content"))
            ValidationRules.AddRule(AddressOf CommonValidation.ValueObjectFieldValidation, _
                New Csla.Validation.RuleArgs("WarehouseFrom"))
            ValidationRules.AddRule(AddressOf CommonValidation.ChronologyValidation, _
                New CommonValidation.ChronologyRuleArgs("Date", "OperationalLimitAcquisition"))
            ValidationRules.AddRule(AddressOf CommonValidation.ChronologyValidation, _
                New CommonValidation.ChronologyRuleArgs("Date", "OperationalLimitTransfer"))

            ValidationRules.AddRule(AddressOf WarehouseToValidation, _
                New Csla.Validation.RuleArgs("WarehouseTo"))

            ValidationRules.AddDependantProperty("WarehouseFrom", "WarehouseTo", False)
            ValidationRules.AddDependantProperty("OperationalLimitAcquisition", "Date", False)
            ValidationRules.AddDependantProperty("OperationalLimitTransfer", "Date", False)

        End Sub

        ''' <summary>
        ''' Rule ensuring that the value of property WarehouseTo is valid.
        ''' </summary>
        ''' <param name="target">Object containing the data to validate</param>
        ''' <param name="e">Arguments parameter specifying the name of the string
        ''' property to validate</param>
        ''' <returns><see langword="false" /> if the rule is broken</returns>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
        Private Shared Function WarehouseToValidation(ByVal target As Object, _
            ByVal e As Validation.RuleArgs) As Boolean

            Dim valObj As GoodsComplexOperationInternalTransfer = _
                DirectCast(target, GoodsComplexOperationInternalTransfer)

            If Not CommonValidation.ValueObjectFieldValidation(target, e) Then Return False

            If valObj._WarehouseTo <> WarehouseInfo.Empty AndAlso valObj._WarehouseFrom <> WarehouseInfo.Empty _
                AndAlso valObj._WarehouseTo.ID = valObj._WarehouseFrom.ID Then
                e.Description = Goods_GoodsComplexOperationInternalTransfer_WarehouseToInvalid
                e.Severity = Validation.RuleSeverity.Error
                Return False
            End If

            Return True

        End Function

#End Region

#Region " Authorization Rules "

        Protected Overrides Sub AddAuthorizationRules()
            AuthorizationRules.AllowWrite("Goods.GoodsOperationInternalTransfer2")
        End Sub

        Public Shared Function CanGetObject() As Boolean
            Return ApplicationContext.User.IsInRole("Goods.GoodsOperationInternalTransfer1")
        End Function

        Public Shared Function CanAddObject() As Boolean
            Return ApplicationContext.User.IsInRole("Goods.GoodsOperationInternalTransfer2")
        End Function

        Public Shared Function CanEditObject() As Boolean
            Return ApplicationContext.User.IsInRole("Goods.GoodsOperationInternalTransfer3")
        End Function

        Public Shared Function CanDeleteObject() As Boolean
            Return ApplicationContext.User.IsInRole("Goods.GoodsOperationInternalTransfer3")
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets a new GoodsComplexOperationInternalTransfer instance.
        ''' </summary>
        ''' <param name="warehouseFromID">an <see cref="Goods.Warehouse.ID">ID of the warehouse</see>
        ''' to transfer the goods from</param>
        ''' <param name="warehouseToID">an <see cref="Goods.Warehouse.ID">ID of the warehouse</see>
        ''' to transfer the goods to</param>
        ''' <remarks></remarks>
        Public Shared Function NewGoodsComplexOperationInternalTransfer( _
            ByVal warehouseFromID As Integer, ByVal warehouseToID As Integer) As GoodsComplexOperationInternalTransfer
            Return DataPortal.Create(Of GoodsComplexOperationInternalTransfer) _
                (New Criteria(warehouseFromID, warehouseToID))
        End Function

        ''' <summary>
        ''' Gets an existing GoodsComplexOperationInternalTransfer instance from a database.
        ''' </summary>
        ''' <param name="id">an <see cref="ID">ID of the operation</see> to fetch</param>
        ''' <remarks></remarks>
        Public Shared Function GetGoodsComplexOperationInternalTransfer(ByVal id As Integer) As GoodsComplexOperationInternalTransfer
            Return DataPortal.Fetch(Of GoodsComplexOperationInternalTransfer) _
                (New Criteria(id))
        End Function

        ''' <summary>
        ''' Deletes an existing GoodsComplexOperationInternalTransfer instance from a database.
        ''' </summary>
        ''' <param name="id">an <see cref="ID">ID of the operation</see> to delete</param>
        ''' <remarks></remarks>
        Public Shared Sub DeleteGoodsComplexOperationInternalTransfer(ByVal id As Integer)
            DataPortal.Delete(New Criteria(id))
        End Sub


        Private Sub New()
            ' require use of factory methods
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> _
        Private Class Criteria
            Private _ID As Integer = 0
            Private _WarehouseFromID As Integer = 0
            Private _WarehouseToID As Integer = 0
            Public ReadOnly Property ID() As Integer
                Get
                    Return _ID
                End Get
            End Property
            Public ReadOnly Property WarehouseFromID() As Integer
                Get
                    Return _WarehouseFromID
                End Get
            End Property
            Public ReadOnly Property WarehouseToID() As Integer
                Get
                    Return _WarehouseToID
                End Get
            End Property
            Public Sub New(ByVal nID As Integer)
                _ID = nID
            End Sub
            Public Sub New(ByVal nWarehouseFromID As Integer, ByVal nWarehouseToID As Integer)
                _WarehouseFromID = nWarehouseFromID
                _WarehouseToID = nWarehouseToID
            End Sub
        End Class


        Private Overloads Sub DataPortal_Create(ByVal criteria As Criteria)

            If Not CanAddObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecurityInsertDenied)

            Create(criteria.WarehouseFromID, criteria.WarehouseToID)

        End Sub

        Private Sub Create(ByVal warehouseFromID As Integer, ByVal warehouseToID As Integer)

            Dim list As WarehouseInfoList = WarehouseInfoList.GetListChild()

            Dim nWarehouseFrom As WarehouseInfo = list.GetItem(warehouseFromID, False)
            Dim nWarehouseTo As WarehouseInfo = list.GetItem(warehouseToID, False)

            Create(nWarehouseFrom, nWarehouseTo)

        End Sub

        Private Sub Create(ByVal nWarehouseFrom As WarehouseInfo, _
            ByVal nWarehouseTo As WarehouseInfo)

            _WarehouseFrom = nWarehouseFrom
            _WarehouseTo = nWarehouseTo

            If Not _WarehouseTo Is Nothing AndAlso Not _WarehouseTo.IsEmpty _
                AndAlso Not _WarehouseFrom Is Nothing AndAlso Not _WarehouseFrom.IsEmpty _
                AndAlso _WarehouseTo.ID = _WarehouseFrom.ID Then
                Throw New Exception(Goods_GoodsComplexOperationInternalTransfer_WarehouseToInvalid)
            End If

            _Items = GoodsInternalTransferItemList.NewGoodsInternalTransferItemList

            Dim baseValidator As SimpleChronologicValidator = _
                SimpleChronologicValidator.NewSimpleChronologicValidator( _
                ConvertLocalizedName(GoodsComplexOperationType.InternalTransfer), Nothing)

            _OperationalLimitAcquisition = ComplexChronologicValidator.NewComplexChronologicValidator( _
                ConvertLocalizedName(GoodsOperationType.Acquisition), baseValidator, Nothing, Nothing)
            _OperationalLimitTransfer = ComplexChronologicValidator.NewComplexChronologicValidator( _
                ConvertLocalizedName(GoodsOperationType.Transfer), baseValidator, Nothing, Nothing)

            ValidationRules.CheckRules()

        End Sub


        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)

            If Not CanGetObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecuritySelectDenied)

            Fetch(criteria.ID)

        End Sub

        Private Sub Fetch(ByVal operationID As Integer)

            Dim obj As ComplexOperationPersistenceObject = ComplexOperationPersistenceObject. _
                GetComplexOperationPersistenceObject(operationID, _
                GoodsComplexOperationType.InternalTransfer, True)

            Fetch(obj)

        End Sub

        Private Sub Fetch(ByVal obj As ComplexOperationPersistenceObject)

            _Content = obj.Content
            _DocumentNumber = obj.DocNo
            _ID = obj.ID
            _InsertDate = obj.InsertDate
            _JournalEntryID = obj.JournalEntryID
            _Date = obj.OperationDate
            _WarehouseTo = obj.SecondaryWarehouse
            _UpdateDate = obj.UpdateDate
            _WarehouseFrom = obj.Warehouse

            Dim baseValidator As IChronologicValidator = SimpleChronologicValidator. _
                GetSimpleChronologicValidator(_ID, _Date, ConvertLocalizedName( _
                GoodsComplexOperationType.InternalTransfer), Nothing)

            Using myData As DataTable = OperationalLimitList.GetDataSourceForComplexOperation(_ID, _Date)
                Dim objList As List(Of OperationPersistenceObject) = _
                    OperationPersistenceObject.GetOperationPersistenceObjectList(_ID)
                _Items = GoodsInternalTransferItemList.GetGoodsInternalTransferItemList( _
                    objList, myData, baseValidator)
            End Using

            _OperationalLimitAcquisition = ComplexChronologicValidator.GetComplexChronologicValidator( _
                _JournalEntryID, _Date, ConvertLocalizedName(GoodsOperationType.Acquisition), _
                baseValidator, Nothing, _Items.GetLimitations(True))
            _OperationalLimitTransfer = ComplexChronologicValidator.GetComplexChronologicValidator( _
                _JournalEntryID, _Date, ConvertLocalizedName(GoodsOperationType.Transfer), _
                baseValidator, Nothing, _Items.GetLimitations(False))

            _OldWarehouseFromID = _WarehouseFrom.ID
            _OldWarehouseToID = _WarehouseTo.ID

            MarkOld()

            ValidationRules.CheckRules()

        End Sub


        Protected Overrides Sub DataPortal_Insert()

            If Not CanAddObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecurityInsertDenied)

            CheckIfCanUpdate()
            DoSave()

        End Sub

        Protected Overrides Sub DataPortal_Update()

            If Not CanEditObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecurityUpdateDenied)

            CheckIfCanUpdate()
            DoSave()

        End Sub

        Private Sub DoSave()

            Dim obj As ComplexOperationPersistenceObject = GetPersistenceObj()

            Dim entry As General.JournalEntry = GetJournalEntry()

            Using transaction As New SqlTransaction

                Try

                    If Not entry Is Nothing Then
                        entry = entry.SaveChild
                        _JournalEntryID = entry.ID
                        obj.JournalEntryID = _JournalEntryID
                    ElseIf entry Is Nothing AndAlso Not IsNew AndAlso _JournalEntryID > 0 Then
                        General.JournalEntry.DeleteJournalEntryChild(_JournalEntryID)
                        _JournalEntryID = 0
                        obj.JournalEntryID = 0
                    End If

                    obj = obj.SaveChild(_OperationalLimitAcquisition.FinancialDataCanChange _
                        AndAlso _OperationalLimitTransfer.FinancialDataCanChange, _
                        _OperationalLimitAcquisition.FinancialDataCanChange _
                        AndAlso _OperationalLimitTransfer.ChildrenFinancialDataCanChange, _
                        _OperationalLimitAcquisition.FinancialDataCanChange _
                        AndAlso _OperationalLimitAcquisition.ChildrenFinancialDataCanChange)

                    If IsNew Then
                        _ID = obj.ID
                        _InsertDate = obj.InsertDate
                    End If
                    _UpdateDate = obj.UpdateDate

                    _Items.Update(Me)

                    transaction.Commit()

                Catch ex As Exception
                    transaction.SetNonSqlException(ex)
                    Throw
                End Try

            End Using

            _OldWarehouseFromID = _WarehouseFrom.ID
            _OldWarehouseToID = _WarehouseTo.ID

            MarkOld()

        End Sub

        Private Function GetPersistenceObj() As ComplexOperationPersistenceObject

            Dim obj As ComplexOperationPersistenceObject
            If IsNew Then
                obj = ComplexOperationPersistenceObject.NewComplexOperationPersistenceObject( _
                    GoodsComplexOperationType.InternalTransfer, 0)
            Else
                obj = ComplexOperationPersistenceObject.GetComplexOperationPersistenceObject( _
                    _ID, GoodsComplexOperationType.InternalTransfer)
                If obj.UpdateDate <> _UpdateDate Then Throw New Exception( _
                    My.Resources.Common_UpdateDateHasChanged)
            End If

            obj.AccountOperation = 0
            obj.Content = _Content
            obj.DocNo = _DocumentNumber
            obj.JournalEntryID = _JournalEntryID
            obj.OperationDate = _Date
            obj.SecondaryWarehouse = _WarehouseTo
            obj.Warehouse = _WarehouseFrom

            Return obj

        End Function


        Protected Overrides Sub DataPortal_DeleteSelf()
            DataPortal_Delete(New Criteria(_ID))
        End Sub

        Protected Overrides Sub DataPortal_Delete(ByVal criteria As Object)

            If Not CanDeleteObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecurityUpdateDenied)

            Dim operationToDelete As GoodsComplexOperationInternalTransfer = _
                New GoodsComplexOperationInternalTransfer
            operationToDelete.Fetch(DirectCast(criteria, Criteria).ID)

            If Not operationToDelete._OperationalLimitAcquisition.FinancialDataCanChange Then
                Throw New Exception(String.Format(Goods_GoodsComplexOperationInternalTransfer_DeleteInvalid, _
                    vbCrLf, operationToDelete._OperationalLimitAcquisition.FinancialDataCanChangeExplanation))
            End If
            If Not operationToDelete._OperationalLimitAcquisition.ChildrenFinancialDataCanChange Then
                Throw New Exception(String.Format(Goods_GoodsComplexOperationInternalTransfer_DeleteInvalid, _
                    operationToDelete._OperationalLimitAcquisition.ChildrenFinancialDataCanChangeExplanation))
            End If
            If Not operationToDelete._OperationalLimitTransfer.ChildrenFinancialDataCanChange Then
                Throw New Exception(String.Format(Goods_GoodsComplexOperationInternalTransfer_DeleteInvalid, _
                    operationToDelete._OperationalLimitTransfer.ChildrenFinancialDataCanChangeExplanation))
            End If

            If operationToDelete.JournalEntryID > 0 Then
                IndirectRelationInfoList.CheckIfJournalEntryCanBeDeleted( _
                    operationToDelete.JournalEntryID, DocumentType.GoodsInternalTransfer)
            End If

            Using transaction As New SqlTransaction

                Try

                    ComplexOperationPersistenceObject.Delete(operationToDelete.ID, _
                        True, True, True)

                    If operationToDelete.JournalEntryID > 0 Then
                        General.JournalEntry.DeleteJournalEntryChild(operationToDelete.JournalEntryID)
                    End If

                    transaction.Commit()

                Catch ex As Exception
                    transaction.SetNonSqlException(ex)
                    Throw
                End Try

            End Using

            MarkNew()

        End Sub


        Private Sub CheckIfCanUpdate()

            If Not _Items.Count > 0 Then
                Throw New Exception(Goods_GoodsComplexOperationInternalTransfer_ItemListEmpty)
            End If

            ' just in case
            _Items.SetParentDate(_Date)

            If IsNew Then
                _Items.CheckIfCanUpdate(Me, Nothing)
            Else
                Using myData As DataTable = OperationalLimitList.GetDataSourceForComplexOperation( _
                    _ID, _OperationalLimitAcquisition.CurrentOperationDate)
                    _Items.CheckIfCanUpdate(Me, myData)
                End Using
            End If

            Me.ValidationRules.CheckRules()
            If Not Me.IsValid Then
                Throw New Exception(String.Format(My.Resources.Common_ContainsErrors, vbCrLf, _
                    GetAllBrokenRules()))
            End If

        End Sub

        Private Function GetJournalEntry() As General.JournalEntry

            Dim result As General.JournalEntry
            If IsNew OrElse Not _JournalEntryID > 0 Then
                result = General.JournalEntry.NewJournalEntryChild(DocumentType.GoodsInternalTransfer)
            Else
                result = General.JournalEntry.GetJournalEntryChild(_JournalEntryID, DocumentType.GoodsInternalTransfer)
            End If

            result.Content = _Content
            result.Date = _Date
            result.DocNumber = _DocumentNumber

            Dim fullBookEntryList As BookEntryInternalList = _Items.GetBookEntryInternalList()

            If Not fullBookEntryList.Count > 0 Then Return Nothing

            If _OperationalLimitAcquisition.FinancialDataCanChange Then

                fullBookEntryList.Aggregate()

                result.DebetList.Clear()
                result.CreditList.Clear()

                result.DebetList.LoadBookEntryListFromInternalList(fullBookEntryList, False, False)
                result.CreditList.LoadBookEntryListFromInternalList(fullBookEntryList, False, False)

            End If

            If Not result.IsValid Then
                Throw New Exception(String.Format(My.Resources.Common_FailedToCreateJournalEntry, _
                    vbCrLf, result.ToString, vbCrLf, result.GetAllBrokenRules))
            End If

            Return result

        End Function

#End Region

    End Class

End Namespace