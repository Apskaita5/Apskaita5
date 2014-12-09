Imports ApskaitaObjects.Goods
Namespace ActiveReports

    <Serializable()> _
    Public Class GoodsTurnoverInfo
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


        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        Public ReadOnly Property Name() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Name.Trim
            End Get
        End Property

        Public ReadOnly Property MeasureUnit() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _MeasureUnit.Trim
            End Get
        End Property

        Public ReadOnly Property AccountPurchases() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AccountPurchases
            End Get
        End Property

        Public ReadOnly Property AccountSalesNetCosts() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AccountSalesNetCosts
            End Get
        End Property

        Public ReadOnly Property AccountDiscounts() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AccountDiscounts
            End Get
        End Property

        Public ReadOnly Property AccountValueReduction() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AccountValueReduction
            End Get
        End Property

        Public ReadOnly Property GroupName() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _GroupName.Trim
            End Get
        End Property

        Public ReadOnly Property PricePurchase() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_PricePurchase, 6)
            End Get
        End Property

        Public ReadOnly Property DefaultVatRatePurchase() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_DefaultVatRatePurchase)
            End Get
        End Property

        Public ReadOnly Property PriceSale() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_PriceSale, 6)
            End Get
        End Property

        Public ReadOnly Property DefaultVatRateSales() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_DefaultVatRateSales)
            End Get
        End Property

        Public ReadOnly Property AccountingMethod() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AccountingMethod.Trim
            End Get
        End Property

        Public ReadOnly Property ValuationMethod() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ValuationMethod.Trim
            End Get
        End Property

        Public ReadOnly Property TradeType() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _TradeType.Trim
            End Get
        End Property

        Public ReadOnly Property Code() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Code.Trim
            End Get
        End Property

        Public ReadOnly Property BarCode() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _BarCode.Trim
            End Get
        End Property

        Public ReadOnly Property DefaultWarehouse() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DefaultWarehouse.Trim
            End Get
        End Property

        Public ReadOnly Property DefaultWarehouseAccount() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DefaultWarehouseAccount
            End Get
        End Property

        Public ReadOnly Property IsObsolete() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsObsolete
            End Get
        End Property

        Public ReadOnly Property AmountPeriodStart() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AmountPeriodStart, 6)
            End Get
        End Property

        Public ReadOnly Property AmountInWarehousePeriodStart() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AmountInWarehousePeriodStart, 6)
            End Get
        End Property

        Public ReadOnly Property AmountPendingPeriodStart() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AmountPendingPeriodStart, 6)
            End Get
        End Property

        Public ReadOnly Property AmountAcquisitions() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AmountAcquisitions, 6)
            End Get
        End Property

        Public ReadOnly Property AmountTransfered() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AmountTransfered, 6)
            End Get
        End Property

        Public ReadOnly Property AmountDiscarded() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AmountDiscarded, 6)
            End Get
        End Property

        Public ReadOnly Property AmountAcquisitionsInWarehouse() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AmountAcquisitionsInWarehouse, 6)
            End Get
        End Property

        Public ReadOnly Property AmountTransferedInWarehouse() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AmountTransferedInWarehouse, 6)
            End Get
        End Property

        Public ReadOnly Property AmountDiscardedInWarehouse() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AmountDiscardedInWarehouse, 6)
            End Get
        End Property

        Public ReadOnly Property AmountChangeInventorization() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AmountChangeInventorization, 6)
            End Get
        End Property

        Public ReadOnly Property AmountPeriodEnd() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AmountPeriodEnd, 6)
            End Get
        End Property

        Public ReadOnly Property AmountInWarehousePeriodEnd() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AmountInWarehousePeriodEnd, 6)
            End Get
        End Property

        Public ReadOnly Property AmountPendingPeriodEnd() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AmountPendingPeriodEnd, 6)
            End Get
        End Property

        Public ReadOnly Property AmountPurchasesPeriodStart() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AmountPurchasesPeriodStart, 6)
            End Get
        End Property

        Public ReadOnly Property AmountPurchasesPeriodEnd() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AmountPurchasesPeriodEnd, 6)
            End Get
        End Property

        Public ReadOnly Property UnitValuePeriodStart() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_UnitValuePeriodStart, 6)
            End Get
        End Property

        Public ReadOnly Property UnitValueInWarehousePeriodStart() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_UnitValueInWarehousePeriodStart, 6)
            End Get
        End Property

        Public ReadOnly Property UnitValuePeriodEnd() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_UnitValuePeriodEnd, 6)
            End Get
        End Property

        Public ReadOnly Property UnitValueInWarehousePeriodEnd() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_UnitValueInWarehousePeriodEnd, 6)
            End Get
        End Property

        Public ReadOnly Property TotalValuePeriodStart() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_TotalValuePeriodStart)
            End Get
        End Property

        Public ReadOnly Property TotalValueInWarehousePeriodStart() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_TotalValueInWarehousePeriodStart)
            End Get
        End Property

        Public ReadOnly Property TotalValuePeriodEnd() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_TotalValuePeriodEnd)
            End Get
        End Property

        Public ReadOnly Property TotalValueInWarehousePeriodEnd() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_TotalValueInWarehousePeriodEnd)
            End Get
        End Property

        Public ReadOnly Property TotalAdditionalCosts() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_TotalAdditionalCosts)
            End Get
        End Property

        Public ReadOnly Property TotalDiscounts() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_TotalDiscounts)
            End Get
        End Property

        Public ReadOnly Property TotalAdditionalCostsForDiscardedGoods() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_TotalAdditionalCostsForDiscardedGoods)
            End Get
        End Property

        Public ReadOnly Property TotalDiscountsForDiscardedGoods() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_TotalDiscountsForDiscardedGoods)
            End Get
        End Property

        Public ReadOnly Property AccountPurchasesPeriodStart() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AccountPurchasesPeriodStart)
            End Get
        End Property

        Public ReadOnly Property AccountWarehousePeriodStart() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AccountWarehousePeriodStart)
            End Get
        End Property

        Public ReadOnly Property AccountSalesNetCostsPeriodStart() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AccountSalesNetCostsPeriodStart)
            End Get
        End Property

        Public ReadOnly Property AccountDiscountsPeriodStart() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AccountDiscountsPeriodStart)
            End Get
        End Property

        Public ReadOnly Property AccountValueReductionPeriodStart() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AccountValueReductionPeriodStart)
            End Get
        End Property

        Public ReadOnly Property AccountPurchasesDebit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AccountPurchasesDebit)
            End Get
        End Property

        Public ReadOnly Property AccountPurchasesCredit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AccountPurchasesCredit)
            End Get
        End Property

        Public ReadOnly Property AccountWarehouseDebit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AccountWarehouseDebit)
            End Get
        End Property

        Public ReadOnly Property AccountWarehouseCredit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AccountWarehouseCredit)
            End Get
        End Property

        Public ReadOnly Property AccountSalesNetCostsDebit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AccountSalesNetCostsDebit)
            End Get
        End Property

        Public ReadOnly Property AccountSalesNetCostsCredit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AccountSalesNetCostsCredit)
            End Get
        End Property

        Public ReadOnly Property AccountDiscountsDebit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AccountDiscountsDebit)
            End Get
        End Property

        Public ReadOnly Property AccountDiscountsCredit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AccountDiscountsCredit)
            End Get
        End Property

        Public ReadOnly Property AccountValueReductionDebit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AccountValueReductionDebit)
            End Get
        End Property

        Public ReadOnly Property AccountValueReductionCredit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AccountValueReductionCredit)
            End Get
        End Property

        Public ReadOnly Property AccountPurchasesPeriodEnd() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AccountPurchasesPeriodEnd)
            End Get
        End Property

        Public ReadOnly Property AccountWarehousePeriodEnd() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AccountWarehousePeriodEnd)
            End Get
        End Property

        Public ReadOnly Property AccountSalesNetCostsPeriodEnd() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AccountSalesNetCostsPeriodEnd)
            End Get
        End Property

        Public ReadOnly Property AccountDiscountsPeriodEnd() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AccountDiscountsPeriodEnd)
            End Get
        End Property

        Public ReadOnly Property AccountValueReductionPeriodEnd() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AccountValueReductionPeriodEnd)
            End Get
        End Property


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
            _PricePurchase = CDblSafe(dr.Item(8), 6, 0)
            _DefaultVatRatePurchase = CDblSafe(dr.Item(9), 2, 0)
            _PriceSale = CDblSafe(dr.Item(10), 6, 0)
            _DefaultVatRateSales = CDblSafe(dr.Item(11), 2, 0)
            _AccountingMethod = ConvertEnumHumanReadable(ConvertEnumDatabaseCode(Of GoodsAccountingMethod) _
                (CIntSafe(dr.Item(12), 0)))
            _ValuationMethod = ConvertEnumHumanReadable(ConvertEnumDatabaseCode(Of GoodsValuationMethod) _
                (CIntSafe(dr.Item(13), 0)))
            _TradeType = ConvertEnumHumanReadable(ConvertEnumDatabaseCode(Of Documents.TradedItemType) _
                (CIntSafe(dr.Item(14), 0)))
            _Code = CStrSafe(dr.Item(15)).Trim
            _BarCode = CStrSafe(dr.Item(16)).Trim
            _DefaultWarehouse = CStrSafe(dr.Item(17)).Trim
            _DefaultWarehouseAccount = CLongSafe(dr.Item(18), 0)
            _IsObsolete = ConvertDbBoolean(CIntSafe(dr.Item(19), 0))
            _AmountPeriodStart = CDblSafe(dr.Item(20), 6, 0)
            _AmountInWarehousePeriodStart = CDblSafe(dr.Item(21), 6, 0)
            _AmountAcquisitions = CDblSafe(dr.Item(22), 6, 0)
            _AmountTransfered = CDblSafe(dr.Item(23), 6, 0)
            _AmountDiscarded = CDblSafe(dr.Item(24), 6, 0)
            _AmountAcquisitionsInWarehouse = CDblSafe(dr.Item(25), 6, 0)
            _AmountTransferedInWarehouse = CDblSafe(dr.Item(26), 6, 0)
            _AmountDiscardedInWarehouse = CDblSafe(dr.Item(27), 6, 0)
            _AmountChangeInventorization = CDblSafe(dr.Item(28), 6, 0)
            _AmountPeriodEnd = CRound(_AmountPeriodStart + _AmountAcquisitions + _AmountTransfered _
                + _AmountDiscarded + _AmountChangeInventorization, 6)
            _AmountInWarehousePeriodEnd = CDblSafe(dr.Item(29), 6, 0)
            _AmountPurchasesPeriodStart = CDblSafe(dr.Item(30), 6, 0)
            _AmountPurchasesPeriodEnd = CDblSafe(dr.Item(31), 6, 0)
            _TotalAdditionalCosts = CDblSafe(dr.Item(32), 2, 0)
            _TotalDiscounts = CDblSafe(dr.Item(33), 2, 0)
            _TotalAdditionalCostsForDiscardedGoods = CDblSafe(dr.Item(34), 2, 0)
            _TotalDiscountsForDiscardedGoods = CDblSafe(dr.Item(35), 2, 0)
            _AccountPurchasesPeriodStart = CDblSafe(dr.Item(36), 2, 0)
            _AccountPurchasesDebit = CDblSafe(dr.Item(37), 2, 0)
            _AccountPurchasesCredit = -CDblSafe(dr.Item(38), 2, 0)
            _AccountWarehousePeriodStart = CDblSafe(dr.Item(39), 2, 0)
            _AccountWarehouseDebit = CDblSafe(dr.Item(40), 2, 0)
            _AccountWarehouseCredit = -CDblSafe(dr.Item(41), 2, 0)
            _AccountSalesNetCostsPeriodStart = CDblSafe(dr.Item(42), 2, 0)
            _AccountSalesNetCostsDebit = CDblSafe(dr.Item(43), 2, 0)
            _AccountSalesNetCostsCredit = -CDblSafe(dr.Item(44), 2, 0)
            _AccountDiscountsPeriodStart = CDblSafe(dr.Item(45), 2, 0)
            _AccountDiscountsDebit = CDblSafe(dr.Item(46), 2, 0)
            _AccountDiscountsCredit = -CDblSafe(dr.Item(47), 2, 0)
            _AccountValueReductionPeriodStart = CDblSafe(dr.Item(48), 2, 0)
            _AccountValueReductionDebit = CDblSafe(dr.Item(49), 2, 0)
            _AccountValueReductionCredit = -CDblSafe(dr.Item(50), 2, 0)

            _AccountPurchasesPeriodEnd = CRound(_AccountPurchasesPeriodStart + _AccountPurchasesDebit _
                - _AccountPurchasesCredit)
            _AccountWarehousePeriodEnd = CRound(_AccountWarehousePeriodStart + _AccountWarehouseDebit _
                - _AccountWarehouseCredit)
            _AccountSalesNetCostsPeriodEnd = CRound(_AccountSalesNetCostsPeriodStart + _AccountSalesNetCostsDebit _
                - _AccountSalesNetCostsCredit)
            _AccountDiscountsPeriodEnd = CRound(_AccountDiscountsPeriodStart + _AccountDiscountsDebit _
                - _AccountDiscountsCredit)
            _AccountValueReductionPeriodEnd = CRound(_AccountValueReductionPeriodStart + _AccountValueReductionDebit _
                - _AccountValueReductionCredit)

            _TotalValueInWarehousePeriodStart = _AccountWarehousePeriodStart
            _TotalValueInWarehousePeriodEnd = _AccountWarehousePeriodEnd

            If CRound(_AmountInWarehousePeriodStart, 6) <> 0 Then
                _UnitValueInWarehousePeriodStart = CRound(_TotalValueInWarehousePeriodStart / _
                    _AmountInWarehousePeriodStart, 6)
            Else
                _UnitValueInWarehousePeriodStart = 0
            End If
            If CRound(_AmountInWarehousePeriodEnd, 6) <> 0 Then
                _UnitValueInWarehousePeriodEnd = CRound(_TotalValueInWarehousePeriodEnd / _
                    _AmountInWarehousePeriodEnd, 6)
            Else
                _UnitValueInWarehousePeriodEnd = 0
            End If
            If ConvertEnumDatabaseCode(Of GoodsAccountingMethod) _
                (CIntSafe(dr.Item(12), 0)) = GoodsAccountingMethod.Periodic Then

                _TotalValuePeriodStart = CRound(_AccountPurchasesPeriodStart _
                    + _AccountDiscountsPeriodStart + _AccountWarehousePeriodStart)

                If CRound(_AmountPurchasesPeriodStart + _AmountInWarehousePeriodStart, 6) <> 0 Then
                    _UnitValuePeriodStart = CRound(_TotalValuePeriodStart _
                        / (_AmountPurchasesPeriodStart + _AmountInWarehousePeriodStart), 6)
                Else
                    _UnitValuePeriodStart = 0
                End If

                _AmountPeriodStart = CRound(_AmountPurchasesPeriodStart + _AmountInWarehousePeriodStart, 6)

                _AmountPendingPeriodStart = CRound(_AmountPeriodStart - _AmountInWarehousePeriodStart, 6)

                _TotalValuePeriodEnd = CRound(_AccountPurchasesPeriodEnd _
                    + _AccountDiscountsPeriodEnd + _AccountWarehousePeriodEnd)

                If CRound(_AmountPurchasesPeriodEnd + _AmountInWarehousePeriodEnd, 6) <> 0 Then
                    _UnitValuePeriodEnd = CRound(_TotalValuePeriodEnd _
                        / (_AmountPurchasesPeriodEnd + _AmountInWarehousePeriodEnd), 6)
                Else
                    _UnitValuePeriodEnd = 0
                End If

                _AmountPeriodEnd = CRound(_AmountPeriodStart + _AmountAcquisitions + _AmountTransfered _
                    + _AmountDiscarded + _AmountChangeInventorization, 6)

                _AmountPendingPeriodEnd = CRound(_AmountPeriodEnd - _AmountInWarehousePeriodEnd, 6)

            Else
                _TotalValuePeriodStart = _AccountWarehousePeriodStart
                _TotalValuePeriodEnd = _AccountWarehousePeriodEnd
                If CRound(_AmountPeriodStart, 6) <> 0 Then
                    _UnitValuePeriodStart = CRound(_AccountWarehousePeriodStart / _AmountPeriodStart, 6)
                Else
                    _UnitValuePeriodStart = 0
                End If
                If CRound(_AmountPeriodEnd, 6) <> 0 Then
                    _UnitValuePeriodEnd = CRound(_AccountWarehousePeriodEnd / _AmountPeriodEnd, 6)
                Else
                    _UnitValuePeriodEnd = 0
                End If
            End If

            _TotalValuePeriodStart = CRound(_TotalValuePeriodStart + _AccountValueReductionPeriodStart, 2)
            _TotalValuePeriodEnd = CRound(_TotalValuePeriodEnd + _AccountValueReductionPeriodEnd, 2)
            If CRound(_AmountPeriodStart, 6) <> 0 Then
                _UnitValuePeriodStart = CRound(_TotalValuePeriodStart / _AmountPeriodStart, 6)
            Else
                _UnitValuePeriodStart = 0
            End If
            If CRound(_AmountPeriodEnd, 6) <> 0 Then
                _UnitValuePeriodEnd = CRound(_TotalValuePeriodEnd / _AmountPeriodEnd, 6)
            Else
                _UnitValuePeriodEnd = 0
            End If

        End Sub

#End Region

    End Class

End Namespace