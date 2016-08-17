Imports ApskaitaObjects.Goods
Namespace ActiveReports

    ''' <summary>
    ''' Represents an item in a <see cref="GoodsTurnoverInfoList">goods turnover report</see>,
    ''' contains information about aggregated turnover of a specific goods item
    ''' within the report period subject to the report filter criteria.
    ''' </summary>
    ''' <remarks>Should only be used as a child of <see cref="GoodsTurnoverInfoList">GoodsTurnoverInfoList</see>.</remarks>
    <Serializable()> _
    Public NotInheritable Class GoodsTurnoverInfo
        Inherits ReadOnlyBase(Of GoodsTurnoverInfo)

#Region " Business Methods "

        Private _ID As Integer = 0
        Private _Name As String = ""
        Private _MeasureUnit As String = ""
        Private _AccountPurchases As Long = 0
        Private _AccountSalesNetCosts As Long = 0
        Private _AccountDiscounts As Long = 0
        Private _AccountValueReduction As Long = 0
        Private _GroupName As String = ""
        Private _PricePurchase As Double = 0
        Private _DefaultVatRatePurchase As Double = 0
        Private _PriceSale As Double = 0
        Private _DefaultVatRateSales As Double = 0
        Private _AccountingMethod As String = ""
        Private _ValuationMethod As String = ""
        Private _TradeType As String = ""
        Private _Code As String = ""
        Private _BarCode As String = ""
        Private _DefaultWarehouse As String = ""
        Private _DefaultWarehouseAccount As Long = 0
        Private _IsObsolete As Boolean = False
        Private _AmountPeriodStart As Double = 0
        Private _AmountInWarehousePeriodStart As Double = 0
        Private _AmountPendingPeriodStart As Double = 0
        Private _AmountAcquisitions As Double = 0
        Private _AmountTransfered As Double = 0
        Private _AmountDiscarded As Double = 0
        Private _AmountAcquisitionsInWarehouse As Double = 0
        Private _AmountTransferedInWarehouse As Double = 0
        Private _AmountDiscardedInWarehouse As Double = 0
        Private _AmountChangeInventorization As Double = 0
        Private _AmountPeriodEnd As Double = 0
        Private _AmountInWarehousePeriodEnd As Double = 0
        Private _AmountPendingPeriodEnd As Double = 0
        Private _AmountPurchasesPeriodStart As Double = 0
        Private _AmountPurchasesPeriodEnd As Double = 0
        Private _AmountChange As Double = 0
        Private _AmountInWarehouseChange As Double = 0
        Private _AmountInPurchasesChange As Double = 0
        Private _UnitValuePeriodStart As Double = 0
        Private _UnitValueInWarehousePeriodStart As Double = 0
        Private _UnitValuePeriodEnd As Double = 0
        Private _UnitValueInWarehousePeriodEnd As Double = 0
        Private _TotalValuePeriodStart As Double = 0
        Private _TotalValueInWarehousePeriodStart As Double = 0
        Private _TotalValuePeriodEnd As Double = 0
        Private _TotalValueInWarehousePeriodEnd As Double = 0
        Private _TotalAdditionalCosts As Double = 0
        Private _TotalDiscounts As Double = 0
        Private _TotalAdditionalCostsForDiscardedGoods As Double = 0
        Private _TotalDiscountsForDiscardedGoods As Double = 0
        Private _AccountPurchasesDebit As Double = 0
        Private _AccountPurchasesCredit As Double = 0
        Private _AccountWarehouseDebit As Double = 0
        Private _AccountWarehouseCredit As Double = 0
        Private _AccountSalesNetCostsDebit As Double = 0
        Private _AccountSalesNetCostsCredit As Double = 0
        Private _AccountDiscountsDebit As Double = 0
        Private _AccountDiscountsCredit As Double = 0
        Private _AccountValueReductionDebit As Double = 0
        Private _AccountValueReductionCredit As Double = 0
        Private _AccountPurchasesPeriodStart As Double = 0
        Private _AccountPurchasesPeriodEnd As Double = 0
        Private _AccountWarehousePeriodStart As Double = 0
        Private _AccountWarehousePeriodEnd As Double = 0
        Private _AccountSalesNetCostsPeriodStart As Double = 0
        Private _AccountSalesNetCostsPeriodEnd As Double = 0
        Private _AccountDiscountsPeriodStart As Double = 0
        Private _AccountDiscountsPeriodEnd As Double = 0
        Private _AccountValueReductionPeriodStart As Double = 0
        Private _AccountValueReductionPeriodEnd As Double = 0


        ''' <summary>
        ''' Gets an <see cref="Goods.GoodsItem.ID">ID of the goods</see>.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' Gets a <see cref="Goods.GoodsItem.Name">name of the goods</see>.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="Goods.GoodsItem.Name">GoodsItem.Name</see>.</remarks>
        Public ReadOnly Property Name() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Name.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a <see cref="Goods.GoodsItem.MeasureUnit">measure unit of the goods</see>.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="Goods.GoodsItem.MeasureUnit">GoodsItem.MeasureUnit</see>.</remarks>
        Public ReadOnly Property MeasureUnit() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _MeasureUnit.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets an <see cref="General.Account.ID">account</see> that is used for
        ''' the value of goods received (bought) by the <see cref="GoodsAccountingMethod.Periodic">
        ''' periodic accounting method</see>, not applicable for persistent accounting method,
        ''' that uses <see cref="Warehouse.WarehouseAccount">warehouse account</see>
        ''' for the same purpose.
        ''' </summary>
        ''' <remarks>See methodology for BAS No 9 ""Stores"" para. 8.
        ''' Corresponds to <see cref="Goods.GoodsItem.AccountPurchases">GoodsItem.AccountPurchases</see>.</remarks>
        Public ReadOnly Property AccountPurchases() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AccountPurchases
            End Get
        End Property

        ''' <summary>
        ''' Gets an <see cref="General.Account.ID">account</see> that is used for
        ''' the value of goods discarded (sold etc.). 
        ''' If the accounting method is set to<see cref="GoodsAccountingMethod.Periodic">
        ''' Periodic</see>, this account is fixed and mainly used by an <see cref="GoodsComplexOperationInventorization">
        ''' inventorization</see> operation (also in some cases by discount and additional costs). 
        ''' If the accounting method is set to<see cref="GoodsAccountingMethod.Persistent">
        ''' Persistent</see>, this account is used as a default goods discard costs
        ''' account by almost every operation, i.e. an operation can override it.
        ''' </summary>
        ''' <remarks>See methodology for BAS No 9 ""Stores"" para. 5.2 and 40.
        ''' Corresponds to <see cref="Goods.GoodsItem.AccountSalesNetCosts">GoodsItem.AccountSalesNetCosts</see>.</remarks>
        Public ReadOnly Property AccountSalesNetCosts() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AccountSalesNetCosts
            End Get
        End Property

        ''' <summary>
        ''' Gets an <see cref="General.Account.ID">account</see> that is used for
        ''' discounts received by the <see cref="GoodsAccountingMethod.Periodic">
        ''' periodic accounting method</see>, not applicable for persistent accounting method.
        ''' </summary>
        ''' <remarks>See methodology for BAS No 9 ""Stores"" para. 5.2.
        ''' Corresponds to <see cref="Goods.GoodsItem.AccountDiscounts">GoodsItem.AccountDiscounts</see>.</remarks>
        Public ReadOnly Property AccountDiscounts() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AccountDiscounts
            End Get
        End Property

        ''' <summary>
        ''' Gets an <see cref="General.Account.ID">account</see> that is used for
        ''' goods value reduction (when goods are revalued to match market prices). 
        ''' Handling of this account does not depend on the accounting method.
        ''' </summary>
        ''' <remarks>See methodology for BAS No 9 ""Stores"" para. 24 - 33.
        ''' Corresponds to <see cref="Goods.GoodsItem.AccountValueReduction">GoodsItem.AccountValueReduction</see>.</remarks>
        Public ReadOnly Property AccountValueReduction() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AccountValueReduction
            End Get
        End Property

        ''' <summary>
        ''' Gets a custom goods group that the goods are assigned to.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="Goods.GoodsItem.Group">GoodsItem.Group</see>.</remarks>
        Public ReadOnly Property GroupName() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _GroupName.Trim
            End Get
        End Property

        <DoubleField(ValueRequiredLevel.Optional, False, ROUNDUNITINVOICERECEIVED)> _
        Public ReadOnly Property PricePurchase() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_PricePurchase, ROUNDUNITINVOICERECEIVED)
            End Get
        End Property

        ''' <summary>
        ''' Gets a default VAT rate for the goods beeing purchased.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="Goods.GoodsItem.DefaultVatRatePurchase">GoodsItem.DefaultVatRatePurchase</see>.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property DefaultVatRatePurchase() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_DefaultVatRatePurchase)
            End Get
        End Property

        <DoubleField(ValueRequiredLevel.Optional, False, ROUNDUNITINVOICEMADE)> _
        Public ReadOnly Property PriceSale() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_PriceSale, ROUNDUNITINVOICEMADE)
            End Get
        End Property

        ''' <summary>
        ''' Gets a default VAT rate for the goods beeing sold.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="Goods.GoodsItem.DefaultVatRateSales">GoodsItem.DefaultVatRateSales</see>.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property DefaultVatRateSales() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_DefaultVatRateSales)
            End Get
        End Property

        ''' <summary>
        ''' Gets a goods accounting method (periodic/persistent) 
        ''' as a localized human readable string.
        ''' </summary>
        ''' <remarks>Cannot be changed after the first operation with the goods.
        ''' Corresponds to <see cref="Goods.GoodsItem.AccountingMethod">GoodsItem.AccountingMethod</see>.</remarks>
        Public ReadOnly Property AccountingMethod() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AccountingMethod.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a goods valuation method (FIFO, LIFO, average, etc.) 
        ''' as a localized human readable string.
        ''' </summary>
        ''' <remarks>Cannot be changed after the first operation with the goods, 
        ''' but could be prospectively overriden by a <see cref="GoodsOperationValuationMethod">
        ''' valuation method change operation</see>.
        ''' Default value corresponds to <see cref="Goods.GoodsItem.DefaultValuationMethod">GoodsItem.DefaultValuationMethod</see>.
        ''' Actual value depends on the last <see cref="Goods.GoodsOperationValuationMethod">
        ''' goods valuation method change operation</see> before the end of the report period.</remarks>
        Public ReadOnly Property ValuationMethod() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ValuationMethod.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets how the goods is used in trade operations (sale, purchase, etc.) 
        ''' as a localized human readable string.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="Goods.GoodsItem.TradedType">GoodsItem.TradedType</see>.</remarks>
        Public ReadOnly Property TradeType() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _TradeType.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a custom goods code. (for internal company use or for integration 
        ''' with external CRM systems)
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="Goods.GoodsItem.InternalCode">GoodsItem.InternalCode</see>.</remarks>
        Public ReadOnly Property Code() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Code.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a goods barcode.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="Goods.GoodsItem.Barcode">GoodsItem.Barcode</see>.</remarks>
        Public ReadOnly Property BarCode() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _BarCode.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a default <see cref="Goods.Warehouse.Name">warehouse</see> 
        ''' that is used to initialize goods operations.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="Goods.GoodsItem.DefaultWarehouse">GoodsItem.DefaultWarehouse</see>.</remarks>
        Public ReadOnly Property DefaultWarehouse() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DefaultWarehouse.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets an <see cref="Goods.Warehouse.WarehouseAccount">account of the 
        ''' default warehouse</see> that is used to initialize goods operations.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="Goods.GoodsItem.DefaultWarehouse">GoodsItem.DefaultWarehouse</see>.</remarks>
        Public ReadOnly Property DefaultWarehouseAccount() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DefaultWarehouseAccount
            End Get
        End Property

        ''' <summary>
        ''' Gets whether the goods are obsolete (no longer in use).
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="Goods.GoodsItem.IsObsolete">GoodsItem.IsObsolete</see>.</remarks>
        Public ReadOnly Property IsObsolete() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsObsolete
            End Get
        End Property

        <DoubleField(ValueRequiredLevel.Optional, False, ROUNDAMOUNTGOODS)> _
        Public ReadOnly Property AmountPeriodStart() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AmountPeriodStart, ROUNDAMOUNTGOODS)
            End Get
        End Property

        <DoubleField(ValueRequiredLevel.Optional, False, ROUNDAMOUNTGOODS)> _
        Public ReadOnly Property AmountInWarehousePeriodStart() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AmountInWarehousePeriodStart, ROUNDAMOUNTGOODS)
            End Get
        End Property

        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDAMOUNTGOODS)> _
        Public ReadOnly Property AmountChange() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AmountChange)
            End Get
        End Property

        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDAMOUNTGOODS)> _
        Public ReadOnly Property AmountInWarehouseChange() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AmountInWarehouseChange)
            End Get
        End Property

        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDAMOUNTGOODS)> _
        Public ReadOnly Property AmountInPurchasesChange() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AmountInPurchasesChange)
            End Get
        End Property

        <DoubleField(ValueRequiredLevel.Optional, False, ROUNDAMOUNTGOODS)> _
        Public ReadOnly Property AmountPendingPeriodStart() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AmountPendingPeriodStart, ROUNDAMOUNTGOODS)
            End Get
        End Property

        <DoubleField(ValueRequiredLevel.Optional, False, ROUNDAMOUNTGOODS)> _
        Public ReadOnly Property AmountAcquisitions() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AmountAcquisitions, ROUNDAMOUNTGOODS)
            End Get
        End Property

        <DoubleField(ValueRequiredLevel.Optional, False, ROUNDAMOUNTGOODS)> _
        Public ReadOnly Property AmountTransfered() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AmountTransfered, ROUNDAMOUNTGOODS)
            End Get
        End Property

        <DoubleField(ValueRequiredLevel.Optional, False, ROUNDAMOUNTGOODS)> _
        Public ReadOnly Property AmountDiscarded() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AmountDiscarded, ROUNDAMOUNTGOODS)
            End Get
        End Property

        <DoubleField(ValueRequiredLevel.Optional, False, ROUNDAMOUNTGOODS)> _
        Public ReadOnly Property AmountAcquisitionsInWarehouse() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AmountAcquisitionsInWarehouse, ROUNDAMOUNTGOODS)
            End Get
        End Property

        <DoubleField(ValueRequiredLevel.Optional, False, ROUNDAMOUNTGOODS)> _
        Public ReadOnly Property AmountTransferedInWarehouse() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AmountTransferedInWarehouse, ROUNDAMOUNTGOODS)
            End Get
        End Property

        <DoubleField(ValueRequiredLevel.Optional, False, ROUNDAMOUNTGOODS)> _
        Public ReadOnly Property AmountDiscardedInWarehouse() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AmountDiscardedInWarehouse, ROUNDAMOUNTGOODS)
            End Get
        End Property

        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDAMOUNTGOODS)> _
        Public ReadOnly Property AmountChangeInventorization() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AmountChangeInventorization, ROUNDAMOUNTGOODS)
            End Get
        End Property

        <DoubleField(ValueRequiredLevel.Optional, False, ROUNDAMOUNTGOODS)> _
        Public ReadOnly Property AmountPeriodEnd() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AmountPeriodEnd, ROUNDAMOUNTGOODS)
            End Get
        End Property

        <DoubleField(ValueRequiredLevel.Optional, False, ROUNDAMOUNTGOODS)> _
        Public ReadOnly Property AmountInWarehousePeriodEnd() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AmountInWarehousePeriodEnd, ROUNDAMOUNTGOODS)
            End Get
        End Property

        <DoubleField(ValueRequiredLevel.Optional, False, ROUNDAMOUNTGOODS)> _
        Public ReadOnly Property AmountPendingPeriodEnd() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AmountPendingPeriodEnd, ROUNDAMOUNTGOODS)
            End Get
        End Property

        <DoubleField(ValueRequiredLevel.Optional, False, ROUNDAMOUNTGOODS)> _
        Public ReadOnly Property AmountPurchasesPeriodStart() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AmountPurchasesPeriodStart, ROUNDAMOUNTGOODS)
            End Get
        End Property

        <DoubleField(ValueRequiredLevel.Optional, False, ROUNDAMOUNTGOODS)> _
        Public ReadOnly Property AmountPurchasesPeriodEnd() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AmountPurchasesPeriodEnd, ROUNDAMOUNTGOODS)
            End Get
        End Property

        <DoubleField(ValueRequiredLevel.Optional, False, ROUNDUNITGOODS)> _
        Public ReadOnly Property UnitValuePeriodStart() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_UnitValuePeriodStart, ROUNDUNITGOODS)
            End Get
        End Property

        <DoubleField(ValueRequiredLevel.Optional, False, ROUNDUNITGOODS)> _
        Public ReadOnly Property UnitValueInWarehousePeriodStart() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_UnitValueInWarehousePeriodStart, ROUNDUNITGOODS)
            End Get
        End Property

        <DoubleField(ValueRequiredLevel.Optional, False, ROUNDUNITGOODS)> _
        Public ReadOnly Property UnitValuePeriodEnd() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_UnitValuePeriodEnd, ROUNDUNITGOODS)
            End Get
        End Property

        <DoubleField(ValueRequiredLevel.Optional, False, ROUNDUNITGOODS)> _
        Public ReadOnly Property UnitValueInWarehousePeriodEnd() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_UnitValueInWarehousePeriodEnd, ROUNDUNITGOODS)
            End Get
        End Property

        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property TotalValuePeriodStart() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_TotalValuePeriodStart)
            End Get
        End Property

        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property TotalValueInWarehousePeriodStart() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_TotalValueInWarehousePeriodStart)
            End Get
        End Property

        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property TotalValuePeriodEnd() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_TotalValuePeriodEnd)
            End Get
        End Property

        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property TotalValueInWarehousePeriodEnd() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_TotalValueInWarehousePeriodEnd)
            End Get
        End Property

        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property TotalAdditionalCosts() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_TotalAdditionalCosts)
            End Get
        End Property

        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property TotalDiscounts() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_TotalDiscounts)
            End Get
        End Property

        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property TotalAdditionalCostsForDiscardedGoods() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_TotalAdditionalCostsForDiscardedGoods)
            End Get
        End Property

        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property TotalDiscountsForDiscardedGoods() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_TotalDiscountsForDiscardedGoods)
            End Get
        End Property

        ''' <summary>
        ''' Gets a balance of the <see cref="AccountPurchases">AccountPurchases</see>
        ''' at the start of the report period.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number (invalid) represents credit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property AccountPurchasesPeriodStart() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AccountPurchasesPeriodStart)
            End Get
        End Property

        ''' <summary>
        ''' Gets a balance of the warehouse account at the start of the report period.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number (invalid) represents credit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property AccountWarehousePeriodStart() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AccountWarehousePeriodStart)
            End Get
        End Property

        ''' <summary>
        ''' Gets a balance of the <see cref="AccountSalesNetCosts">AccountSalesNetCosts</see>
        ''' at the start of the report period.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number (invalid) represents credit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property AccountSalesNetCostsPeriodStart() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AccountSalesNetCostsPeriodStart)
            End Get
        End Property

        ''' <summary>
        ''' Gets a balance of the <see cref="AccountDiscounts">AccountDiscounts</see>
        ''' at the start of the report period.
        ''' </summary>
        ''' <remarks>A positive number (invalid) represents debit balance, a negative number represents credit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property AccountDiscountsPeriodStart() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AccountDiscountsPeriodStart)
            End Get
        End Property

        ''' <summary>
        ''' Gets a balance of the <see cref="AccountValueReduction">AccountValueReduction</see>
        ''' at the start of the report period.
        ''' </summary>
        ''' <remarks>A positive number (invalid) represents debit balance, a negative number represents credit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property AccountValueReductionPeriodStart() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AccountValueReductionPeriodStart)
            End Get
        End Property

        ''' <summary>
        ''' Gets a debit turnover of the <see cref="AccountPurchases">AccountPurchases</see>
        ''' within the report period.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property AccountPurchasesDebit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AccountPurchasesDebit)
            End Get
        End Property

        ''' <summary>
        ''' Gets a credit turnover of the <see cref="AccountPurchases">AccountPurchases</see>
        ''' within the report period.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property AccountPurchasesCredit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AccountPurchasesCredit)
            End Get
        End Property

        ''' <summary>
        ''' Gets a debit turnover of the warehouse account within the report period.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property AccountWarehouseDebit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AccountWarehouseDebit)
            End Get
        End Property

        ''' <summary>
        ''' Gets a credit turnover of the warehouse account within the report period.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property AccountWarehouseCredit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AccountWarehouseCredit)
            End Get
        End Property

        ''' <summary>
        ''' Gets a debit turnover of the <see cref="AccountSalesNetCosts">AccountSalesNetCosts</see>
        ''' within the report period.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property AccountSalesNetCostsDebit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AccountSalesNetCostsDebit)
            End Get
        End Property

        ''' <summary>
        ''' Gets a credit turnover of the <see cref="AccountSalesNetCosts">AccountSalesNetCosts</see>
        ''' within the report period.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property AccountSalesNetCostsCredit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AccountSalesNetCostsCredit)
            End Get
        End Property

        ''' <summary>
        ''' Gets a debit turnover of the <see cref="AccountDiscounts">AccountDiscounts</see>
        ''' within the report period.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property AccountDiscountsDebit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AccountDiscountsDebit)
            End Get
        End Property

        ''' <summary>
        ''' Gets a credit turnover of the <see cref="AccountDiscounts">AccountDiscounts</see>
        ''' within the report period.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property AccountDiscountsCredit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AccountDiscountsCredit)
            End Get
        End Property

        ''' <summary>
        ''' Gets a debit turnover of the <see cref="AccountValueReduction">AccountValueReduction</see>
        ''' within the report period.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property AccountValueReductionDebit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AccountValueReductionDebit)
            End Get
        End Property

        ''' <summary>
        ''' Gets a credit turnover of the <see cref="AccountValueReduction">AccountValueReduction</see>
        ''' within the report period.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property AccountValueReductionCredit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AccountValueReductionCredit)
            End Get
        End Property

        ''' <summary>
        ''' Gets a balance of the <see cref="AccountPurchases">AccountPurchases</see>
        ''' at the end of the report period.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number (invalid) represents credit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property AccountPurchasesPeriodEnd() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AccountPurchasesPeriodEnd)
            End Get
        End Property

        ''' <summary>
        ''' Gets a balance of the warehouse account at the end of the report period.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number (invalid) represents credit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property AccountWarehousePeriodEnd() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AccountWarehousePeriodEnd)
            End Get
        End Property

        ''' <summary>
        ''' Gets a balance of the <see cref="AccountSalesNetCosts">AccountSalesNetCosts</see>
        ''' at the end of the report period.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number (invalid) represents credit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property AccountSalesNetCostsPeriodEnd() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AccountSalesNetCostsPeriodEnd)
            End Get
        End Property

        ''' <summary>
        ''' Gets a balance of the <see cref="AccountDiscounts">AccountDiscounts</see>
        ''' at the end of the report period.
        ''' </summary>
        ''' <remarks>A positive number (invalid) represents debit balance, a negative number represents credit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property AccountDiscountsPeriodEnd() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AccountDiscountsPeriodEnd)
            End Get
        End Property

        ''' <summary>
        ''' Gets a balance of the <see cref="AccountValueReduction">AccountValueReduction</see>
        ''' at the end of the report period.
        ''' </summary>
        ''' <remarks>A positive number (invalid) represents debit balance, a negative number represents credit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property AccountValueReductionPeriodEnd() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AccountValueReductionPeriodEnd)
            End Get
        End Property


        ''' <summary>
        ''' Returns whether the there is any goods turnover within the item.
        ''' </summary>
        ''' <remarks></remarks>
        Public Function HasTurnover() As Boolean

            Return (CRound(_AmountAcquisitions, ROUNDAMOUNTGOODS) > 0 OrElse CRound(_AmountAcquisitionsInWarehouse, ROUNDAMOUNTGOODS) > 0 OrElse _
                    CRound(_AmountChangeInventorization, ROUNDAMOUNTGOODS) > 0 OrElse CRound(_AmountDiscarded, ROUNDAMOUNTGOODS) > 0 OrElse _
                    CRound(_AmountDiscardedInWarehouse, ROUNDAMOUNTGOODS) > 0 OrElse CRound(_AmountInWarehousePeriodEnd, ROUNDAMOUNTGOODS) > 0 OrElse _
                    CRound(_AmountPeriodEnd, ROUNDAMOUNTGOODS) > 0 OrElse CRound(_AmountPurchasesPeriodEnd, ROUNDAMOUNTGOODS) > 0 OrElse _
                    CRound(_AmountTransfered, ROUNDAMOUNTGOODS) > 0 OrElse CRound(_AmountTransferedInWarehouse, ROUNDAMOUNTGOODS) > 0 OrElse _
                    CRound(_TotalAdditionalCosts, 2) > 0 OrElse CRound(_TotalDiscounts, 2) > 0 OrElse _
                    CRound(_TotalValuePeriodEnd, 2) > 0)

        End Function


        Protected Overrides Function GetIdValue() As Object
            Return _ID
        End Function

        Public Overrides Function ToString() As String
            Return _Name
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function GetGoodsTurnoverInfo(ByVal dr As DataRow) As GoodsTurnoverInfo
            Return New GoodsTurnoverInfo(dr)
        End Function


        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal dr As DataRow)
            Fetch(dr)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal dr As DataRow)

            _ID = CIntSafe(dr.Item(0), 0)
            _Name = CStrSafe(dr.Item(1)).Trim
            _MeasureUnit = CStrSafe(dr.Item(2)).Trim
            _AccountPurchases = CLongSafe(dr.Item(3), 0)
            _AccountSalesNetCosts = CLongSafe(dr.Item(4), 0)
            _AccountDiscounts = CLongSafe(dr.Item(5), 0)
            _AccountValueReduction = CLongSafe(dr.Item(6), 0)
            _GroupName = CStrSafe(dr.Item(7)).Trim
            _PricePurchase = CDblSafe(dr.Item(8), ROUNDUNITINVOICERECEIVED, 0)
            _DefaultVatRatePurchase = CDblSafe(dr.Item(9), 2, 0)
            _PriceSale = CDblSafe(dr.Item(10), ROUNDUNITINVOICEMADE, 0)
            _DefaultVatRateSales = CDblSafe(dr.Item(11), 2, 0)
            _AccountingMethod = ConvertLocalizedName(ConvertDatabaseID(Of GoodsAccountingMethod) _
                (CIntSafe(dr.Item(12), 0)))
            _ValuationMethod = ConvertLocalizedName(ConvertDatabaseID(Of GoodsValuationMethod) _
                (CIntSafe(dr.Item(13), 0)))
            _TradeType = ConvertLocalizedName(ConvertDatabaseID(Of Documents.TradedItemType) _
                (CIntSafe(dr.Item(14), 0)))
            _Code = CStrSafe(dr.Item(15)).Trim
            _BarCode = CStrSafe(dr.Item(16)).Trim
            _DefaultWarehouse = CStrSafe(dr.Item(17)).Trim
            _DefaultWarehouseAccount = CLongSafe(dr.Item(18), 0)
            _IsObsolete = ConvertDbBoolean(CIntSafe(dr.Item(19), 0))
            _AmountPeriodStart = CDblSafe(dr.Item(20), ROUNDAMOUNTGOODS, 0)
            _AmountInWarehousePeriodStart = CDblSafe(dr.Item(21), ROUNDAMOUNTGOODS, 0)
            _AmountChange = CDblSafe(dr.Item(22), ROUNDAMOUNTGOODS, 0)
            _AmountInWarehouseChange = CDblSafe(dr.Item(23), ROUNDAMOUNTGOODS, 0)
            _AmountInPurchasesChange = CDblSafe(dr.Item(24), ROUNDAMOUNTGOODS, 0)
            _AmountAcquisitions = CDblSafe(dr.Item(25), ROUNDAMOUNTGOODS, 0)
            _AmountTransfered = CDblSafe(dr.Item(26), ROUNDAMOUNTGOODS, 0)
            _AmountDiscarded = CDblSafe(dr.Item(27), ROUNDAMOUNTGOODS, 0)
            _AmountAcquisitionsInWarehouse = CDblSafe(dr.Item(28), ROUNDAMOUNTGOODS, 0)
            _AmountTransferedInWarehouse = CDblSafe(dr.Item(29), ROUNDAMOUNTGOODS, 0)
            _AmountDiscardedInWarehouse = CDblSafe(dr.Item(30), ROUNDAMOUNTGOODS, 0)
            _AmountChangeInventorization = CDblSafe(dr.Item(31), ROUNDAMOUNTGOODS, 0)
            _AmountPeriodEnd = CRound(_AmountPeriodStart + _AmountChange, ROUNDAMOUNTGOODS)
            _AmountInWarehousePeriodEnd = CDblSafe(dr.Item(32), ROUNDAMOUNTGOODS, 0)
            _AmountPurchasesPeriodStart = CDblSafe(dr.Item(33), ROUNDAMOUNTGOODS, 0)
            _AmountPurchasesPeriodEnd = CDblSafe(dr.Item(34), ROUNDAMOUNTGOODS, 0)
            _TotalAdditionalCosts = CDblSafe(dr.Item(35), 2, 0)
            _TotalDiscounts = CDblSafe(dr.Item(36), 2, 0)
            _TotalAdditionalCostsForDiscardedGoods = CDblSafe(dr.Item(37), 2, 0)
            _TotalDiscountsForDiscardedGoods = CDblSafe(dr.Item(38), 2, 0)
            _AccountPurchasesPeriodStart = CDblSafe(dr.Item(39), 2, 0)
            _AccountPurchasesDebit = CDblSafe(dr.Item(40), 2, 0)
            _AccountPurchasesCredit = -CDblSafe(dr.Item(41), 2, 0)
            _AccountWarehousePeriodStart = CDblSafe(dr.Item(42), 2, 0)
            _AccountWarehouseDebit = CDblSafe(dr.Item(43), 2, 0)
            _AccountWarehouseCredit = -CDblSafe(dr.Item(44), 2, 0)
            _AccountSalesNetCostsPeriodStart = CDblSafe(dr.Item(45), 2, 0)
            _AccountSalesNetCostsDebit = CDblSafe(dr.Item(46), 2, 0)
            _AccountSalesNetCostsCredit = -CDblSafe(dr.Item(47), 2, 0)
            _AccountDiscountsPeriodStart = CDblSafe(dr.Item(48), 2, 0)
            _AccountDiscountsDebit = CDblSafe(dr.Item(49), 2, 0)
            _AccountDiscountsCredit = -CDblSafe(dr.Item(50), 2, 0)
            _AccountValueReductionPeriodStart = CDblSafe(dr.Item(51), 2, 0)
            _AccountValueReductionDebit = CDblSafe(dr.Item(52), 2, 0)
            _AccountValueReductionCredit = -CDblSafe(dr.Item(53), 2, 0)

            _AccountPurchasesPeriodEnd = CRound(_AccountPurchasesPeriodStart _
                + _AccountPurchasesDebit - _AccountPurchasesCredit)
            _AccountWarehousePeriodEnd = CRound(_AccountWarehousePeriodStart _
                + _AccountWarehouseDebit - _AccountWarehouseCredit)
            _AccountSalesNetCostsPeriodEnd = CRound(_AccountSalesNetCostsPeriodStart _
                + _AccountSalesNetCostsDebit - _AccountSalesNetCostsCredit)
            _AccountDiscountsPeriodEnd = CRound(_AccountDiscountsPeriodStart _
                + _AccountDiscountsDebit - _AccountDiscountsCredit)
            _AccountValueReductionPeriodEnd = CRound(_AccountValueReductionPeriodStart _
                + _AccountValueReductionDebit - _AccountValueReductionCredit)

            _TotalValueInWarehousePeriodStart = _AccountWarehousePeriodStart
            _TotalValueInWarehousePeriodEnd = _AccountWarehousePeriodEnd

            If CRound(_AmountInWarehousePeriodStart, ROUNDAMOUNTGOODS) <> 0 Then
                _UnitValueInWarehousePeriodStart = CRound(_TotalValueInWarehousePeriodStart _
                    / _AmountInWarehousePeriodStart, ROUNDUNITGOODS)
            Else
                _UnitValueInWarehousePeriodStart = 0
            End If
            If CRound(_AmountInWarehousePeriodEnd, ROUNDAMOUNTGOODS) <> 0 Then
                _UnitValueInWarehousePeriodEnd = CRound(_TotalValueInWarehousePeriodEnd _
                    / _AmountInWarehousePeriodEnd, ROUNDUNITGOODS)
            Else
                _UnitValueInWarehousePeriodEnd = 0
            End If

            If ConvertDatabaseID(Of GoodsAccountingMethod)(CIntSafe(dr.Item(12), 0)) _
                = GoodsAccountingMethod.Periodic Then

                _TotalValuePeriodStart = CRound(_AccountPurchasesPeriodStart _
                    + _AccountDiscountsPeriodStart + _AccountWarehousePeriodStart)

                If CRound(_AmountPurchasesPeriodStart + _AmountInWarehousePeriodStart, ROUNDAMOUNTGOODS) <> 0 Then
                    _UnitValuePeriodStart = CRound(_TotalValuePeriodStart _
                        / (_AmountPurchasesPeriodStart + _AmountInWarehousePeriodStart), ROUNDUNITGOODS)
                Else
                    _UnitValuePeriodStart = 0
                End If

                _AmountPendingPeriodStart = _AmountPurchasesPeriodStart

                _TotalValuePeriodEnd = CRound(_AccountPurchasesPeriodEnd _
                    + _AccountDiscountsPeriodEnd + _AccountWarehousePeriodEnd)

                If CRound(_AmountPurchasesPeriodEnd + _AmountInWarehousePeriodEnd, ROUNDAMOUNTGOODS) <> 0 Then
                    _UnitValuePeriodEnd = CRound(_TotalValuePeriodEnd _
                        / (_AmountPurchasesPeriodEnd + _AmountInWarehousePeriodEnd), ROUNDUNITGOODS)
                Else
                    _UnitValuePeriodEnd = 0
                End If

                _AmountPendingPeriodEnd = CRound(_AmountPurchasesPeriodStart _
                    + _AmountInPurchasesChange, ROUNDAMOUNTGOODS)

            Else
                _TotalValuePeriodStart = _AccountWarehousePeriodStart
                _TotalValuePeriodEnd = _AccountWarehousePeriodEnd
                If CRound(_AmountPeriodStart, ROUNDAMOUNTGOODS) <> 0 Then
                    _UnitValuePeriodStart = CRound(_AccountWarehousePeriodStart _
                        / _AmountPeriodStart, ROUNDUNITGOODS)
                Else
                    _UnitValuePeriodStart = 0
                End If
                If CRound(_AmountPeriodEnd, ROUNDAMOUNTGOODS) <> 0 Then
                    _UnitValuePeriodEnd = CRound(_AccountWarehousePeriodEnd _
                        / _AmountPeriodEnd, ROUNDUNITGOODS)
                Else
                    _UnitValuePeriodEnd = 0
                End If
            End If

            _TotalValuePeriodStart = CRound(_TotalValuePeriodStart + _AccountValueReductionPeriodStart, 2)
            _TotalValuePeriodEnd = CRound(_TotalValuePeriodEnd + _AccountValueReductionPeriodEnd, 2)
            If CRound(_AmountPeriodStart, ROUNDAMOUNTGOODS) <> 0 Then
                _UnitValuePeriodStart = CRound(_TotalValuePeriodStart _
                    / _AmountPeriodStart, ROUNDUNITGOODS)
            Else
                _UnitValuePeriodStart = 0
            End If
            If CRound(_AmountPeriodEnd, ROUNDAMOUNTGOODS) <> 0 Then
                _UnitValuePeriodEnd = CRound(_TotalValuePeriodEnd / _AmountPeriodEnd, ROUNDUNITGOODS)
            Else
                _UnitValuePeriodEnd = 0
            End If

        End Sub

#End Region

    End Class

End Namespace