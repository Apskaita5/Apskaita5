﻿Imports Csla.Validation
Imports ApskaitaObjects.My.Resources

Namespace Goods

    ''' <summary>
    ''' Represents a transfer of a goods (initial) balance in a warehouse
    ''' (used in conjunction with <see cref="General.TransferOfBalance">TransferOfBalance</see>
    ''' to provide bulk data import).
    ''' </summary>
    ''' <remarks>Values are stored using <see cref="OperationPersistenceObject">OperationPersistenceObject</see>.</remarks>
    <Serializable()> _
Public NotInheritable Class GoodsTransferOfBalanceItem
        Inherits BusinessBase(Of GoodsTransferOfBalanceItem)
        Implements IGetErrorForListItem

#Region " Business Methods "

        Private Const PASTE_COLUMN_COUNT As Integer = 10

        Private ReadOnly _Guid As Guid = Guid.NewGuid()
        Private _ID As Integer = 0
        Private _GoodsInfo As GoodsSummary = Nothing
        Private _OperationLimitations As OperationalLimitList = Nothing
        Private _Ammount As Double = 0
        Private _AmountInWarehouse As Double = 0
        Private _UnitCost As Double = 0
        Private _TotalValue As Double = 0
        Private _TotalValueInWarehouse As Double = 0
        Private _SalesNetCosts As Double = 0
        Private _Discounts As Double = 0
        Private _PriceCut As Double = 0
        Private _Warehouse As WarehouseInfo = Nothing
        Private _Remarks As String = ""


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
        ''' Gets <see cref="GoodsSummary">general information about the goods</see> 
        ''' which (initial) balance is transfered by the operation.
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
        ''' Gets a <see cref="GoodsItem.Name">name of the goods</see> 
        ''' which (initial) balance is transfered by the operation.
        ''' </summary>
        ''' <remarks>A proxy property to the <see cref="GoodsInfo">GoodsInfo</see> field
        ''' to enable databinding in a datagridview.</remarks>
        Public ReadOnly Property GoodsName() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _GoodsInfo.Name
            End Get
        End Property

        ''' <summary>
        ''' Gets a <see cref="GoodsItem.MeasureUnit">measure unit of the goods</see> 
        ''' which (initial) balance is transfered by the operation.
        ''' </summary>
        ''' <remarks>A proxy property to the <see cref="GoodsInfo">GoodsInfo</see> field
        ''' to enable databinding in a datagridview.</remarks>
        Public ReadOnly Property GoodsMeasureUnit() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _GoodsInfo.MeasureUnit
            End Get
        End Property

        ''' <summary>
        ''' Gets an <see cref="GoodsItem.AccountingMethod">accounting method for the goods</see> 
        ''' which (initial) balance is transfered by the operation as a localized human readable string.
        ''' </summary>
        ''' <remarks>A proxy property to the <see cref="GoodsInfo">GoodsInfo</see> field
        ''' to enable databinding in a datagridview.</remarks>
        Public ReadOnly Property GoodsAccountingMethodHumanReadable() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _GoodsInfo.AccountingMethodHumanReadable
            End Get
        End Property

        ''' <summary>
        ''' Gets a <see cref="GoodsItem.DefaultValuationMethod">current valuation method 
        ''' for the goods</see> which (initial) balance is transfered by the operation 
        ''' as a localized human readable string.
        ''' </summary>
        ''' <remarks>A proxy property to the <see cref="GoodsInfo">GoodsInfo</see> field
        ''' to enable databinding in a datagridview.</remarks>
        Public ReadOnly Property GoodsValuationMethodHumanReadable() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _GoodsInfo.ValuationMethodHumanReadable
            End Get
        End Property

        ''' <summary>
        ''' Gets a <see cref="GoodsItem.AccountPurchases">purchases account 
        ''' for the goods</see> which (initial) balance is transfered by the operation.
        ''' </summary>
        ''' <remarks>A proxy property to the <see cref="GoodsInfo">GoodsInfo</see> field
        ''' to enable databinding in a datagridview.</remarks>
        Public ReadOnly Property PurchasesAccount() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If _GoodsInfo.AccountingMethod = GoodsAccountingMethod.Persistent Then _
                    Return _Warehouse.WarehouseAccount
                Return _GoodsInfo.AccountPurchases
            End Get
        End Property

        ''' <summary>
        ''' Gets a <see cref="GoodsItem.AccountSalesNetCosts">sales net costs account 
        ''' for the goods</see> which (initial) balance is transfered by the operation.
        ''' </summary>
        ''' <remarks>A proxy property to the <see cref="GoodsInfo">GoodsInfo</see> field
        ''' to enable databinding in a datagridview.</remarks>
        Public ReadOnly Property SalesNetCostsAccount() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If _GoodsInfo.AccountingMethod = GoodsAccountingMethod.Persistent Then Return 0
                Return _GoodsInfo.AccountSalesNetCosts
            End Get
        End Property

        ''' <summary>
        ''' Gets a <see cref="GoodsItem.AccountDiscounts">discount account 
        ''' for the goods</see> which (initial) balance is transfered by the operation.
        ''' </summary>
        ''' <remarks>A proxy property to the <see cref="GoodsInfo">GoodsInfo</see> field
        ''' to enable databinding in a datagridview.</remarks>
        Public ReadOnly Property DiscountsAccount() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If _GoodsInfo.AccountingMethod = GoodsAccountingMethod.Persistent Then Return 0
                Return _GoodsInfo.AccountDiscounts
            End Get
        End Property

        ''' <summary>
        ''' Gets a <see cref="GoodsItem.AccountValueReduction">value reduction account 
        ''' for the goods</see> which (initial) balance is transfered by the operation.
        ''' </summary>
        ''' <remarks>A proxy property to the <see cref="GoodsInfo">GoodsInfo</see> field
        ''' to enable databinding in a datagridview.</remarks>
        Public ReadOnly Property PriceCutCostsAccount() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _GoodsInfo.AccountValueReduction
            End Get
        End Property

        ''' <summary>
        ''' Gets a <see cref="Goods.Warehouse">warehouse</see> that the goods 
        ''' (initial) balance is for.
        ''' </summary>
        ''' <remarks>Is set when creating a new operation and cannot be changed afterwards.
        ''' Corresponds to <see cref="OperationPersistenceObject.Warehouse">OperationPersistenceObject.Warehouse</see>
        ''' and <see cref="ConsignmentPersistenceObject.WarehouseID">ConsignmentPersistenceObject.WarehouseID</see>.</remarks>
        Public ReadOnly Property Warehouse() As WarehouseInfo
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Warehouse
            End Get
        End Property

        ''' <summary>
        ''' Gets a <see cref="Goods.Warehouse.Name">name of the warehouse</see> 
        ''' that the goods (initial) balance is for.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="OperationPersistenceObject.Warehouse">OperationPersistenceObject.Warehouse</see>
        ''' and <see cref="ConsignmentPersistenceObject.WarehouseID">ConsignmentPersistenceObject.WarehouseID</see>.</remarks>
        Public ReadOnly Property WarehouseName() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Warehouse.Name
            End Get
        End Property

        ''' <summary>
        ''' Gets an <see cref="Goods.Warehouse.WarehouseAccount">account of the warehouse</see> 
        ''' that the goods (initial) balance is for.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="OperationPersistenceObject.Warehouse">OperationPersistenceObject.Warehouse</see>
        ''' and <see cref="ConsignmentPersistenceObject.WarehouseID">ConsignmentPersistenceObject.WarehouseID</see>.</remarks>
        Public ReadOnly Property WarehouseAccount() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Warehouse.WarehouseAccount
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
        ''' Gets or sets an (initial) amount of the goods in the <see cref="Warehouse">warehouse</see>
        ''' and in the <see cref="PurchasesAccount">PurchasesAccount</see>.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="OperationPersistenceObject.Amount">OperationPersistenceObject.Amount</see>,
        ''' <see cref="ConsignmentPersistenceObject.Amount">ConsignmentPersistenceObject.Amount</see>
        ''' and (subject to the accounting method) <see cref="OperationPersistenceObject.AmountInWarehouse">OperationPersistenceObject.AmountInWarehouse</see>
        ''' or <see cref="OperationPersistenceObject.AmountInPurchases">OperationPersistenceObject.AmountInPurchases</see>.</remarks>
        <DoubleField(ValueRequiredLevel.Mandatory, False, ROUNDAMOUNTGOODS)> _
        Public Property Ammount() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_Ammount, ROUNDAMOUNTGOODS)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Double)
                CanWriteProperty(True)
                If AmmountIsReadOnly Then Exit Property
                If CRound(_Ammount, ROUNDAMOUNTGOODS) <> CRound(value, ROUNDAMOUNTGOODS) Then
                    _Ammount = CRound(value, ROUNDAMOUNTGOODS)
                    PropertyHasChanged()
                    If _GoodsInfo.AccountingMethod = GoodsAccountingMethod.Persistent Then
                        _AmountInWarehouse = _Ammount
                        PropertyHasChanged("AmountInWarehouse")
                    End If
                    SetTotalCostByUnitCostAndAmmount()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets an (initial) amount of the goods in the warehouse.
        ''' </summary>
        ''' <remarks>Can only be changed if the goods are accounted using 
        ''' <see cref="GoodsAccountingMethod.Periodic">Periodic accounting method</see>,
        ''' otherwise equals <see cref="Ammount">Ammount</see>.
        ''' Corresponds to <see cref="OperationPersistenceObject.Amount">OperationPersistenceObject.Amount</see>,
        ''' <see cref="ConsignmentPersistenceObject.Amount">ConsignmentPersistenceObject.Amount</see>
        ''' and <see cref="OperationPersistenceObject.AmountInWarehouse">OperationPersistenceObject.AmountInWarehouse</see>.</remarks>
        <DoubleField(ValueRequiredLevel.Recommended, False, ROUNDAMOUNTGOODS)> _
        Public Property AmountInWarehouse() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AmountInWarehouse, ROUNDAMOUNTGOODS)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Double)
                CanWriteProperty(True)
                If AmountInWarehouseIsReadOnly Then Exit Property
                If CRound(_AmountInWarehouse, ROUNDAMOUNTGOODS) <> CRound(value, ROUNDAMOUNTGOODS) Then
                    _AmountInWarehouse = CRound(value, ROUNDAMOUNTGOODS)
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a value of the goods per unit.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="OperationPersistenceObject.UnitValue">OperationPersistenceObject.UnitValue</see>
        ''' and <see cref="ConsignmentPersistenceObject.UnitValue">ConsignmentPersistenceObject.UnitValue</see>.</remarks>
        <DoubleField(ValueRequiredLevel.Mandatory, False, ROUNDUNITGOODS)> _
        Public Property UnitCost() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_UnitCost, ROUNDUNITGOODS)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Double)
                CanWriteProperty(True)
                If UnitCostIsReadOnly Then Exit Property
                If CRound(_UnitCost, ROUNDUNITGOODS) <> CRound(value, ROUNDUNITGOODS) Then
                    _UnitCost = CRound(value, ROUNDUNITGOODS)
                    PropertyHasChanged()
                    SetTotalCostByUnitCostAndAmmount()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a total value of the goods in the <see cref="Warehouse">warehouse</see>
        ''' and in the <see cref="PurchasesAccount">PurchasesAccount</see>.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="OperationPersistenceObject.TotalValue">OperationPersistenceObject.TotalValue</see>.</remarks>
        <DoubleField(ValueRequiredLevel.Mandatory, False, 2)> _
        Public Property TotalValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_TotalValue)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Double)
                CanWriteProperty(True)
                If TotalValueIsReadOnly Then Exit Property
                If CRound(_TotalValue) <> CRound(value) Then
                    _TotalValue = CRound(value)
                    PropertyHasChanged()
                    If _GoodsInfo.AccountingMethod = GoodsAccountingMethod.Persistent Then
                        _TotalValueInWarehouse = _TotalValue
                        PropertyHasChanged("TotalValueInWarehouse")
                    End If
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a total value of the goods in the <see cref="Warehouse">warehouse</see>.
        ''' </summary>
        ''' <remarks>Can only be changed if the goods are accounted using 
        ''' <see cref="GoodsAccountingMethod.Periodic">Periodic accounting method</see>,
        ''' otherwise equals <see cref="TotalValue">TotalValue</see>.
        ''' Corresponds to <see cref="OperationPersistenceObject.AccountGeneral">OperationPersistenceObject.AccountGeneral</see>
        ''' and <see cref="ConsignmentPersistenceObject.TotalValue">ConsignmentPersistenceObject.TotalValue</see>.</remarks>
        <DoubleField(ValueRequiredLevel.Recommended, False, 2)> _
        Public Property TotalValueInWarehouse() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_TotalValueInWarehouse)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Double)
                CanWriteProperty(True)
                If TotalValueInWarehouseIsReadOnly Then Exit Property
                If CRound(_TotalValueInWarehouse) <> CRound(value) Then
                    _TotalValueInWarehouse = CRound(value)
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a (initial) balance of the <see cref="SalesNetCostsAccount">SalesNetCostsAccount</see>.
        ''' </summary>
        ''' <remarks>Can only be set if the goods are accounted using 
        ''' <see cref="GoodsAccountingMethod.Periodic">Periodic accounting method</see>,
        ''' otherwise equals zero.
        ''' A positive number represents debit balance, a negative number (invalid) represents credit balance. 
        ''' Corresponds to <see cref="OperationPersistenceObject.AccountSalesNetCosts">OperationPersistenceObject.AccountSalesNetCosts</see>.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public Property SalesNetCosts() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_SalesNetCosts)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Double)
                CanWriteProperty(True)
                If SalesNetCostsIsReadOnly Then Exit Property
                If CRound(_SalesNetCosts) <> CRound(value) Then
                    _SalesNetCosts = CRound(value)
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a (initial) balance of the <see cref="DiscountsAccount">DiscountsAccount</see>.
        ''' </summary>
        ''' <remarks>Can only be set if the goods are accounted using 
        ''' <see cref="GoodsAccountingMethod.Periodic">Periodic accounting method</see>,
        ''' otherwise equals zero.
        ''' A positive number represents credit balance, a negative number (invalid) represents debit balance.
        ''' Corresponds to <see cref="OperationPersistenceObject.AccountDiscounts">OperationPersistenceObject.AccountDiscounts</see>.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public Property Discounts() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_Discounts)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Double)
                CanWriteProperty(True)
                If DiscountsIsReadOnly Then Exit Property
                If CRound(_Discounts) <> CRound(value) Then
                    _Discounts = CRound(value)
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a (initial) balance of the <see cref="PriceCutCostsAccount">PriceCutCostsAccount</see>.
        ''' </summary>
        ''' <remarks>Can only be set if the goods are accounted using 
        ''' <see cref="GoodsAccountingMethod.Periodic">Periodic accounting method</see>,
        ''' otherwise equals zero.
        ''' A positive number represents credit balance, a negative number (invalid) represents debit balance.
        ''' Corresponds to <see cref="OperationPersistenceObject.AccountPriceCut">OperationPersistenceObject.AccountPriceCut</see>.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public Property PriceCut() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_PriceCut)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Double)
                CanWriteProperty(True)
                If PriceCutIsReadOnly Then Exit Property
                If CRound(_PriceCut) <> CRound(value) Then
                    _PriceCut = CRound(value)
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets user remarks regarding the item data.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="OperationPersistenceObject.Content">OperationPersistenceObject.Content</see>.</remarks>
        <StringField(ValueRequiredLevel.Optional, 255)> _
        Public Property Remarks() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Remarks
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As String)
                CanWriteProperty(True)
                If value Is Nothing Then value = ""
                If _Remarks.Trim <> value.Trim Then
                    _Remarks = value.Trim
                    PropertyHasChanged()
                End If
            End Set
        End Property


        ''' <summary>
        ''' Whether the <see cref="Ammount">Ammount</see> property is readonly.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property AmmountIsReadOnly() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return Not _OperationLimitations.FinancialDataCanChange
            End Get
        End Property

        ''' <summary>
        ''' Whether the <see cref="AmountInWarehouse">AmountInWarehouse</see> 
        ''' property is readonly.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property AmountInWarehouseIsReadOnly() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _GoodsInfo.AccountingMethod = GoodsAccountingMethod.Persistent OrElse _
                    Not _OperationLimitations.FinancialDataCanChange
            End Get
        End Property

        ''' <summary>
        ''' Whether the <see cref="UnitCost">UnitCost</see> property is readonly.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property UnitCostIsReadOnly() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return Not _OperationLimitations.FinancialDataCanChange
            End Get
        End Property

        ''' <summary>
        ''' Whether the <see cref="TotalValue">TotalValue</see> property is readonly.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property TotalValueIsReadOnly() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return Not _OperationLimitations.FinancialDataCanChange
            End Get
        End Property

        ''' <summary>
        ''' Whether the <see cref="TotalValueInWarehouse">TotalValueInWarehouse</see> 
        ''' property is readonly.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property TotalValueInWarehouseIsReadOnly() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _GoodsInfo.AccountingMethod = GoodsAccountingMethod.Persistent OrElse _
                    Not _OperationLimitations.FinancialDataCanChange
            End Get
        End Property

        ''' <summary>
        ''' Whether the <see cref="SalesNetCosts">SalesNetCosts</see> property is readonly.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property SalesNetCostsIsReadOnly() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _GoodsInfo.AccountingMethod = GoodsAccountingMethod.Persistent OrElse _
                    Not _OperationLimitations.FinancialDataCanChange
            End Get
        End Property

        ''' <summary>
        ''' Whether the <see cref="Discounts">Discounts</see> property is readonly.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DiscountsIsReadOnly() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _GoodsInfo.AccountingMethod = GoodsAccountingMethod.Persistent OrElse _
                    Not _OperationLimitations.FinancialDataCanChange
            End Get
        End Property

        ''' <summary>
        ''' Whether the <see cref="PriceCut">PriceCut</see> property is readonly.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property PriceCutIsReadOnly() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return Not _OperationLimitations.FinancialDataCanChange
            End Get
        End Property



        Public Sub SetTotalCostByUnitCostAndAmmount()
            TotalValue = CRound(_UnitCost * _Ammount)
        End Sub



        Public Function HasWarnings() As Boolean
            Return MyBase.BrokenRulesCollection.WarningCount > 0
        End Function

        Public Function GetErrorString() As String _
            Implements IGetErrorForListItem.GetErrorString
            If Not MyBase.IsValid Then
                Return String.Format(My.Resources.Common_ErrorInItem, Me.ToString, _
                    vbCrLf, Me.BrokenRulesCollection.ToString(Validation.RuleSeverity.Error))
            End If
            Return ""
        End Function

        Public Function GetWarningString() As String _
            Implements IGetErrorForListItem.GetWarningString
            If Not HasWarnings() Then Return ""
            Return String.Format(My.Resources.Common_WarningInItem, Me.ToString, _
                vbCrLf, Me.BrokenRulesCollection.ToString(Validation.RuleSeverity.Warning))
        End Function


        ''' <summary>
        ''' Gets expected fields count in (tab delimited) paste string.
        ''' </summary>
        Public Shared Function GetPasteStringColumnCount() As Integer
            Return PASTE_COLUMN_COUNT
        End Function

        ''' <summary>
        ''' Gets an array of expected fields sequence in (tab delimited) paste string.
        ''' </summary>
        Public Shared Function GetPasteStringColumns() As String()
            Return My.Resources.Goods_GoodsTransferOfBalanceItem_PasteColumns.Split(New String() {"<BR>"}, _
                StringSplitOptions.RemoveEmptyEntries)
        End Function

        ''' <summary>
        ''' Gets a human readable description of expected fields sequence in (tab delimited) paste string.
        ''' </summary>
        Public Shared Function GetPasteStringColumnsDescription() As String
            Return String.Format(My.Resources.Goods_GoodsTransferOfBalanceItem_PasteColumnsDescription, PASTE_COLUMN_COUNT, _
                String.Join(", ", My.Resources.Goods_GoodsTransferOfBalanceItem_PasteColumns.Split(New String() {"<BR>"}, _
                StringSplitOptions.RemoveEmptyEntries)))
        End Function

        ''' <summary>
        ''' Gets a datatable which columns corresponds to the required imported data 
        ''' (propert name, data type and regionalized caption).
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function GetDataTableSpecification() As DataTable
            Return Utilities.GetDataTableSpecificationForProperties(
                GetType(GoodsTransferOfBalanceItem), New String() _
                {"GoodsName", "WarehouseName", "Ammount", "AmountInWarehouse", "UnitCost",
                "TotalValue", "TotalValueInWarehouse", "SalesNetCosts", "Discounts",
                "PriceCut", "Remarks"})
        End Function


        Protected Overrides Function GetIdValue() As Object
            Return _Guid
        End Function

        Public Overrides Function ToString() As String
            Return String.Format(My.Resources.Goods_GoodsTransferOfBalanceItem_ToString, _
                _GoodsInfo.Name, _Warehouse.Name, _ID.ToString)
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()

            ValidationRules.AddRule(AddressOf CommonValidation.DoubleFieldValidation, _
                New RuleArgs("UnitCost"))
            ValidationRules.AddRule(AddressOf CommonValidation.DoubleFieldValidation, _
                New RuleArgs("TotalValue"))
            ValidationRules.AddRule(AddressOf CommonValidation.DoubleFieldValidation, _
                New RuleArgs("Ammount"))
            ValidationRules.AddRule(AddressOf CommonValidation.DoubleFieldValidation, _
                New RuleArgs("PriceCut"))
            ValidationRules.AddRule(AddressOf CommonValidation.DoubleFieldValidation, _
                New RuleArgs("Discounts"))
            ValidationRules.AddRule(AddressOf CommonValidation.DoubleFieldValidation, _
                New RuleArgs("SalesNetCosts"))

            ValidationRules.AddRule(AddressOf AmountInWarehouseValidation, _
                New RuleArgs("AmountInWarehouse"))
            ValidationRules.AddRule(AddressOf TotalValueInWarehouseValidation, _
                New RuleArgs("TotalValueInWarehouse"))

            ValidationRules.AddDependantProperty("Ammount", "AmountInWarehouse", False)
            ValidationRules.AddDependantProperty("TotalValue", "TotalValueInWarehouse", False)
            ValidationRules.AddDependantProperty("AmountInWarehouse", "TotalValueInWarehouse", False)

        End Sub

        ''' <summary>
        ''' Rule ensuring that AmountInWarehouse is valid.
        ''' </summary>
        ''' <param name="target">Object containing the data to validate</param>
        ''' <param name="e">Arguments parameter specifying the name of the string
        ''' property to validate</param>
        ''' <returns><see langword="false" /> if the rule is broken</returns>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
        Private Shared Function AmountInWarehouseValidation(ByVal target As Object, _
            ByVal e As Validation.RuleArgs) As Boolean

            Dim valObj As GoodsTransferOfBalanceItem = DirectCast(target, GoodsTransferOfBalanceItem)

            If valObj._GoodsInfo.AccountingMethod <> GoodsAccountingMethod.Persistent AndAlso _
                CRound(valObj._AmountInWarehouse, ROUNDAMOUNTGOODS) > _
                CRound(valObj._Ammount, ROUNDAMOUNTGOODS) Then
                e.Description = Goods_GoodsTransferOfBalanceItem_AmountInWarehouseInvalid
                e.Severity = Validation.RuleSeverity.Error
                Return False
            End If

            Return CommonValidation.DoubleFieldValidation(target, e)

        End Function

        ''' <summary>
        ''' Rule ensuring that TotalValueInWarehouse is valid.
        ''' </summary>
        ''' <param name="target">Object containing the data to validate</param>
        ''' <param name="e">Arguments parameter specifying the name of the string
        ''' property to validate</param>
        ''' <returns><see langword="false" /> if the rule is broken</returns>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
        Private Shared Function TotalValueInWarehouseValidation(ByVal target As Object, _
            ByVal e As Validation.RuleArgs) As Boolean

            Dim valObj As GoodsTransferOfBalanceItem = DirectCast(target, GoodsTransferOfBalanceItem)

            If valObj._GoodsInfo.AccountingMethod <> GoodsAccountingMethod.Persistent Then

                If CRound(valObj._TotalValueInWarehouse) > CRound(valObj._TotalValue) Then
                    e.Description = Goods_GoodsTransferOfBalanceItem_TotalValueInWarehouseInvalid
                    e.Severity = Validation.RuleSeverity.Error
                    Return False
                ElseIf CRound(valObj._AmountInWarehouse, ROUNDAMOUNTGOODS) > 0 AndAlso _
                    Not CRound(valObj._TotalValueInWarehouse) > 0 Then
                    e.Description = Goods_GoodsTransferOfBalanceItem_AmountWithoutValue
                    e.Severity = Validation.RuleSeverity.Error
                    Return False
                ElseIf Not CRound(valObj._AmountInWarehouse, ROUNDAMOUNTGOODS) > 0 AndAlso _
                    CRound(valObj._TotalValueInWarehouse) > 0 Then
                    e.Description = Goods_GoodsTransferOfBalanceItem_ValueWithoutAmount
                    e.Severity = Validation.RuleSeverity.Error
                    Return False
                End If

            End If

            Return CommonValidation.DoubleFieldValidation(target, e)

        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets a new GoodsTransferOfBalanceItem instance.
        ''' </summary>
        ''' <param name="goodsID">an <see cref="GoodsItem.ID">ID of the goods</see>
        ''' to transfer the balance for</param>
        ''' <param name="warehouse">a <see cref="Warehouse">warehouse</see>
        ''' to transfer the balance for</param>
        ''' <remarks></remarks>
        Friend Shared Function NewGoodsTransferOfBalanceItem(ByVal goodsID As Integer, _
            ByVal warehouse As WarehouseInfo) As GoodsTransferOfBalanceItem
            Return New GoodsTransferOfBalanceItem(goodsID, warehouse)
        End Function

        ''' <summary>
        ''' Gets a new GoodsTransferOfBalanceItem instance using data in a tab delimited string.
        ''' </summary>
        ''' <param name="goodsItem">an <see cref="GoodsItem.ID">ID of the goods</see>
        ''' to transfer the balance for</param>
        ''' <param name="warehouseItem">a <see cref="Warehouse">warehouse</see>
        ''' to transfer the balance for</param>
        ''' <param name="sourceString">a tab delimited string containing the balance data</param>
        ''' <remarks></remarks>
        Friend Shared Function NewGoodsTransferOfBalanceItem(ByVal goodsItem As GoodsSummary,
            ByVal warehouseItem As WarehouseInfo, ByVal sourceString As String) As GoodsTransferOfBalanceItem
            Return New GoodsTransferOfBalanceItem(goodsItem, warehouseItem, sourceString)
        End Function

        ''' <summary>
        ''' Gets a new GoodsTransferOfBalanceItem instance using data in a template datarow,
        ''' see <see cref="GetDataTableSpecification()">GetDataTableSpecification</see> method.
        ''' </summary>
        ''' <param name="goodsItem">an <see cref="GoodsItem.ID">ID of the goods</see>
        ''' to transfer the balance for</param>
        ''' <param name="warehouseItem">a <see cref="Warehouse">warehouse</see>
        ''' to transfer the balance for</param>
        ''' <param name="dr">a template datarow that contains the data to import</param>
        ''' <remarks></remarks>
        Friend Shared Function NewGoodsTransferOfBalanceItem(ByVal goodsItem As GoodsSummary,
            ByVal warehouseItem As WarehouseInfo, ByVal dr As DataRow) As GoodsTransferOfBalanceItem
            Return New GoodsTransferOfBalanceItem(goodsItem, warehouseItem, dr)
        End Function

        ''' <summary>
        ''' Gets an existing GoodsTransferOfBalanceItem instance using database query results.
        ''' </summary>
        ''' <param name="obj">a data persistence object holding the operation data</param>
        ''' <param name="limitationsDataSource">a datasource for the 
        ''' <see cref="OperationalLimitList">OperationalLimitList</see></param>
        ''' <remarks></remarks>
        Friend Shared Function GetGoodsTransferOfBalanceItem(ByVal obj As OperationPersistenceObject, _
            ByVal limitationsDataSource As DataTable) As GoodsTransferOfBalanceItem
            Return New GoodsTransferOfBalanceItem(obj, limitationsDataSource)
        End Function


        Private Sub New()
            ' require use of factory methods
            MarkAsChild()
        End Sub

        Private Sub New(ByVal goodsID As Integer, ByVal warehouse As WarehouseInfo)
            ' require use of factory methods
            MarkAsChild()
            Create(goodsID, warehouse)
        End Sub

        Private Sub New(ByVal goodsItem As GoodsSummary, ByVal warehouseItem As WarehouseInfo, _
            ByVal sourceString As String)
            ' require use of factory methods
            MarkAsChild()
            Create(goodsItem, warehouseItem, sourceString)
        End Sub

        Private Sub New(ByVal goodsItem As GoodsSummary, ByVal warehouseItem As WarehouseInfo, ByVal dr As DataRow)
            ' require use of factory methods
            MarkAsChild()
            Create(goodsItem, warehouseItem, dr)
        End Sub

        Private Sub New(ByVal obj As OperationPersistenceObject, ByVal LimitationsDataSource As DataTable)
            ' require use of factory methods
            MarkAsChild()
            Fetch(obj, LimitationsDataSource)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Create(ByVal goodsID As Integer, ByVal warehouse As WarehouseInfo)

            If Not goodsID > 0 Then
                Throw New Exception(Goods_GoodsTransferOfBalanceItem_GoodsIdNull)
            ElseIf warehouse Is Nothing OrElse warehouse.IsEmpty Then
                Throw New Exception(Goods_GoodsTransferOfBalanceItem_WarehouseNull)
            End If

            _GoodsInfo = GoodsSummary.NewGoodsSummary(goodsID)
            _Warehouse = warehouse
            _OperationLimitations = OperationalLimitList.NewOperationalLimitList( _
                _GoodsInfo, GoodsOperationType.TransferOfBalance, _Warehouse.ID, Nothing)

            ValidationRules.CheckRules()

        End Sub

        Private Sub Create(ByVal goodsItem As GoodsSummary, ByVal warehouseItem As WarehouseInfo, _
            ByVal sourceString As String)

            _GoodsInfo = GoodsSummary.NewGoodsSummary(goodsItem.ID)
            _Warehouse = warehouseItem
            _OperationLimitations = OperationalLimitList.NewOperationalLimitList( _
                _GoodsInfo, GoodsOperationType.TransferOfBalance, _Warehouse.ID, Nothing)

            Double.TryParse(GetField(sourceString, vbTab, 2), Globalization.NumberStyles.Any, _
                System.Globalization.CultureInfo.InvariantCulture, _Ammount)
            _Ammount = CRound(_Ammount, ROUNDAMOUNTGOODS)

            Double.TryParse(GetField(sourceString, vbTab, 3), Globalization.NumberStyles.Any, _
                System.Globalization.CultureInfo.InvariantCulture, _AmountInWarehouse)
            _AmountInWarehouse = CRound(_AmountInWarehouse, ROUNDAMOUNTGOODS)

            Double.TryParse(GetField(sourceString, vbTab, 4), Globalization.NumberStyles.Any, _
                System.Globalization.CultureInfo.InvariantCulture, _UnitCost)
            _UnitCost = CRound(_UnitCost, ROUNDUNITGOODS)

            Double.TryParse(GetField(sourceString, vbTab, 5), Globalization.NumberStyles.Any, _
                System.Globalization.CultureInfo.InvariantCulture, _TotalValue)
            _TotalValue = CRound(_TotalValue, 2)

            Double.TryParse(GetField(sourceString, vbTab, 6), Globalization.NumberStyles.Any, _
                System.Globalization.CultureInfo.InvariantCulture, _TotalValueInWarehouse)
            _TotalValueInWarehouse = CRound(_TotalValueInWarehouse, 2)

            Double.TryParse(GetField(sourceString, vbTab, 7), Globalization.NumberStyles.Any, _
                System.Globalization.CultureInfo.InvariantCulture, _SalesNetCosts)
            _SalesNetCosts = CRound(_SalesNetCosts, 2)

            Double.TryParse(GetField(sourceString, vbTab, 8), Globalization.NumberStyles.Any, _
                System.Globalization.CultureInfo.InvariantCulture, _Discounts)
            _Discounts = CRound(_Discounts, 2)

            Double.TryParse(GetField(sourceString, vbTab, 9), Globalization.NumberStyles.Any, _
                System.Globalization.CultureInfo.InvariantCulture, _PriceCut)
            _PriceCut = CRound(_PriceCut, 2)

            ValidationRules.CheckRules()

        End Sub

        Private Sub Create(ByVal goodsItem As GoodsSummary, ByVal warehouseItem As WarehouseInfo,
            ByVal dr As DataRow)

            _GoodsInfo = GoodsSummary.NewGoodsSummary(goodsItem.ID)
            _Warehouse = warehouseItem
            _OperationLimitations = OperationalLimitList.NewOperationalLimitList(
                _GoodsInfo, GoodsOperationType.TransferOfBalance, _Warehouse.ID, Nothing)

            _Ammount = CRound(DirectCast(dr.Item("Ammount"), Double), ROUNDAMOUNTGOODS)
            _AmountInWarehouse = CRound(DirectCast(dr.Item("AmountInWarehouse"), Double), ROUNDAMOUNTGOODS)
            _UnitCost = CRound(DirectCast(dr.Item("UnitCost"), Double), ROUNDUNITGOODS)
            _TotalValue = CRound(DirectCast(dr.Item("TotalValue"), Double), 2)
            _TotalValueInWarehouse = CRound(DirectCast(dr.Item("TotalValueInWarehouse"), Double), 2)
            _SalesNetCosts = CRound(DirectCast(dr.Item("SalesNetCosts"), Double), 2)
            _Discounts = CRound(DirectCast(dr.Item("Discounts"), Double), 2)
            _PriceCut = CRound(DirectCast(dr.Item("PriceCut"), Double), 2)
            _Remarks = DirectCast(dr.Item("Remarks"), String)

            ValidationRules.CheckRules()

        End Sub


        Private Sub Fetch(ByVal obj As OperationPersistenceObject, _
            ByVal limitationsDataSource As DataTable)

            If obj.OperationType <> GoodsOperationType.TransferOfBalance Then
                Throw New Exception(String.Format(Goods_OperationPersistenceObject_OperationTypeMismatch, _
                    _ID.ToString, ConvertLocalizedName(obj.OperationType), _
                    ConvertLocalizedName(GoodsOperationType.TransferOfBalance)))
            End If

            _ID = obj.ID
            _GoodsInfo = obj.GoodsInfo
            _Ammount = CRound(obj.AmountInWarehouse + obj.AmountInPurchases, ROUNDAMOUNTGOODS)
            _AmountInWarehouse = obj.AmountInWarehouse
            _UnitCost = obj.UnitValue
            _TotalValue = obj.AccountGeneral + obj.AccountPurchases
            _TotalValueInWarehouse = obj.AccountGeneral
            _SalesNetCosts = obj.AccountSalesNetCosts
            _Discounts = -obj.AccountDiscounts
            _PriceCut = -obj.AccountPriceCut
            _Warehouse = obj.Warehouse

            _OperationLimitations = OperationalLimitList.GetOperationalLimitList( _
                _GoodsInfo, GoodsOperationType.TransferOfBalance, _ID, obj.OperationDate, _
                obj.WarehouseID, Nothing, limitationsDataSource)

            MarkOld()

            ValidationRules.CheckRules()

        End Sub


        Friend Sub Insert(ByVal parent As GoodsComplexOperationTransferOfBalance)

            Dim obj As OperationPersistenceObject = GetPersistenceObj(parent)

            Dim consignment As ConsignmentPersistenceObject = GetConsignment()

            Using transaction As New SqlTransaction

                Try

                    obj = obj.Save(_OperationLimitations.FinancialDataCanChange, False)

                    _ID = obj.ID

                    consignment.Insert(_ID, _Warehouse.ID)

                    transaction.Commit()

                Catch ex As Exception
                    transaction.SetNonSqlException(ex)
                    Throw
                End Try

            End Using

            MarkOld()

        End Sub

        Friend Sub Update(ByVal parent As GoodsComplexOperationTransferOfBalance)

            Dim obj As OperationPersistenceObject = GetPersistenceObj(parent)

            Dim consignment As ConsignmentPersistenceObject = GetConsignment()

            Using transaction As New SqlTransaction

                Try

                    obj.Save(_OperationLimitations.FinancialDataCanChange, False)

                    If _OperationLimitations.FinancialDataCanChange Then
                        consignment.Update(_Warehouse.ID)
                    End If

                    transaction.Commit()

                Catch ex As Exception
                    transaction.SetNonSqlException(ex)
                    Throw
                End Try

            End Using

            MarkOld()

        End Sub

        Private Function GetPersistenceObj(ByVal parent As GoodsComplexOperationTransferOfBalance) As OperationPersistenceObject

            Dim obj As OperationPersistenceObject
            If IsNew Then
                obj = OperationPersistenceObject.NewOperationPersistenceObject( _
                    GoodsOperationType.TransferOfBalance, _GoodsInfo.ID)
            Else
                obj = OperationPersistenceObject.GetOperationPersistenceObjectForSave( _
                    _ID, GoodsOperationType.TransferOfBalance, Now, True)
            End If

            If _GoodsInfo.AccountingMethod = GoodsAccountingMethod.Periodic Then

                obj.Amount = _Ammount
                obj.AmountInPurchases = CRound(_Ammount - _AmountInWarehouse, ROUNDAMOUNTGOODS)
                obj.AmountInWarehouse = _AmountInWarehouse
                obj.AccountDiscounts = -_Discounts
                obj.AccountGeneral = _TotalValueInWarehouse
                obj.AccountPriceCut = -_PriceCut
                obj.AccountPurchases = CRound(_TotalValue - _TotalValueInWarehouse)
                obj.AccountSalesNetCosts = _SalesNetCosts
                obj.TotalValue = _TotalValue
                obj.UnitValue = _UnitCost

            Else

                obj.Amount = _Ammount
                obj.AmountInPurchases = 0
                obj.AmountInWarehouse = _Ammount
                obj.AccountDiscounts = 0
                obj.AccountGeneral = _TotalValue
                obj.AccountPriceCut = -_PriceCut
                obj.AccountPurchases = 0
                obj.AccountSalesNetCosts = 0
                obj.TotalValue = _TotalValue
                obj.UnitValue = _UnitCost

            End If

            obj.Content = _Remarks
            obj.DocNo = ""
            obj.JournalEntryID = parent.JournalEntryID
            obj.OperationDate = parent.JournalEntryDate
            obj.Warehouse = _Warehouse
            obj.WarehouseID = _Warehouse.ID
            obj.ComplexOperationID = parent.ID

            Return obj

        End Function

        Private Function GetConsignment() As ConsignmentPersistenceObject

            Dim result As ConsignmentPersistenceObject = Nothing

            If IsNew Then
                result = ConsignmentPersistenceObject.NewConsignmentPersistenceObject()
            Else
                Dim list As ConsignmentPersistenceObjectList = ConsignmentPersistenceObjectList. _
                    GetConsignmentPersistenceObjectList(_ID)
                If list.Count < 1 Then
                    Throw New Exception(String.Format(Goods_GoodsTransferOfBalanceItem_ConsignmentNotFound, _
                        _GoodsInfo.Name))
                End If
                If list.Count > 1 Then
                    Throw New Exception(String.Format(Goods_GoodsTransferOfBalanceItem_ConsignmentDataCorrupt, _
                        _GoodsInfo.Name))
                End If
                result = list(0)
            End If

            result.Amount = _Ammount
            result.TotalValue = _TotalValue
            result.UnitValue = _UnitCost

            Return result

        End Function


        Friend Sub DeleteSelf()

            Using transaction As New SqlTransaction

                Try

                    OperationPersistenceObject.Delete(_ID, True, False)

                    transaction.Commit()

                Catch ex As Exception
                    transaction.SetNonSqlException(ex)
                    Throw
                End Try

            End Using

            MarkNew()

        End Sub


        Friend Sub CheckIfCanDelete(ByVal limitationsDataSource As DataTable)

            If IsNew Then Exit Sub

            _OperationLimitations = OperationalLimitList.GetUpdatedOperationalLimitList( _
                _OperationLimitations, Nothing, limitationsDataSource)

            If Not _OperationLimitations.FinancialDataCanChange Then
                Throw New Exception(String.Format(Goods_GoodsTransferOfBalanceItem_DeleteInvalid, _
                    _GoodsInfo.Name, vbCrLf, _OperationLimitations.FinancialDataCanChangeExplanation))
            End If

        End Sub

        Friend Sub CheckIfCanUpdate(ByVal limitationsDataSource As DataTable)

            _OperationLimitations = OperationalLimitList.GetUpdatedOperationalLimitList( _
                _OperationLimitations, Nothing, limitationsDataSource)

            ValidationRules.CheckRules()
            If Not IsValid Then
                Throw New Exception(Me.GetErrorString())
            End If

        End Sub

#End Region

    End Class

End Namespace