Imports ApskaitaObjects.Attributes

Namespace Goods

    ''' <summary>
    ''' Represents a type of a simple goods operation,
    ''' e.g. <see cref="GoodsOperationType.Discard">Discard</see> for
    ''' <see cref="GoodsOperationDiscard">GoodsOperationDiscard</see>.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum GoodsOperationType

        ''' <summary>
        ''' Acquisition of goods (when goods are added to a warehouse):
        ''' <see cref="GoodsOperationAcquisition">GoodsOperationAcquisition</see>.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(1)> _
        Acquisition

        ''' <summary>
        ''' Transfer of goods (when goods are removed from a warehouse, 
        ''' goods costst are not automaticaly discarded, should be handled 
        ''' by a parent operation):
        ''' <see cref="GoodsOperationTransfer">GoodsOperationTransfer</see>.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(2)> _
        Transfer

        ''' <summary>
        ''' Discard of goods (when goods are removed from a warehouse 
        ''' and their costs are automaticaly discarded):
        ''' <see cref="GoodsOperationDiscard">GoodsOperationDiscard</see>.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(3)> _
        Discard

        ''' <summary>
        ''' Inventorization of goods (can only be a child of <see cref="GoodsComplexOperationInventorization">GoodsComplexOperationInventorization</see>):
        ''' <see cref="GoodsInventorizationItem">GoodsInventorizationItem</see>.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(4)> _
        Inventorization

        ''' <summary>
        ''' Discount received for the goods acquired (supplier's discount):
        ''' <see cref="GoodsOperationDiscount">GoodsOperationDiscount</see>.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(5)> _
        ConsignmentDiscount

        ''' <summary>
        ''' Added costs for goods (e.g. transportation costs that are added to the goods value):
        ''' <see cref="GoodsOperationAdditionalCosts">GoodsOperationAdditionalCosts</see>.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(6)> _
        ConsignmentAdditionalCosts

        ''' <summary>
        ''' Goods price cut (when the balance value is reduced to the market value):
        ''' <see cref="GoodsOperationPriceCut">GoodsOperationPriceCut</see>.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(7)> _
        PriceCut

        ''' <summary>
        ''' A prospective change of <see cref="GoodsItem.AccountSalesNetCosts">AccountSalesNetCosts</see>:
        ''' <see cref="GoodsOperationAccountChange">GoodsOperationAccountChange</see>.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(9)> _
        AccountSalesNetCostsChange

        ''' <summary>
        ''' A prospective change of <see cref="GoodsItem.AccountPurchases">AccountPurchases</see>:
        ''' <see cref="GoodsOperationAccountChange">GoodsOperationAccountChange</see>.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(10)> _
        AccountPurchasesChange

        ''' <summary>
        ''' A prospective change of <see cref="GoodsItem.AccountDiscounts">AccountDiscounts</see>:
        ''' <see cref="GoodsOperationAccountChange">GoodsOperationAccountChange</see>.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(11)> _
        AccountDiscountsChange

        ''' <summary>
        ''' A prospective change of <see cref="GoodsItem.AccountValueReduction">AccountValueReduction</see>:
        ''' <see cref="GoodsOperationAccountChange">GoodsOperationAccountChange</see>.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(12)> _
        AccountValueReductionChange

        ''' <summary>
        ''' A prospective change of <see cref="GoodsItem.DefaultValuationMethod">goods valuation method</see>:
        ''' <see cref="GoodsOperationValuationMethod">GoodsOperationValuationMethod</see>.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(13)> _
        ValuationMethodChange

        ''' <summary>
        ''' Transfer of goods balance (can only be a child of <see cref="GoodsComplexOperationTransferOfBalance">GoodsComplexOperationTransferOfBalance</see>):
        ''' <see cref="GoodsTransferOfBalanceItem">GoodsTransferOfBalanceItem</see>.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(14)> _
        TransferOfBalance

        ''' <summary>
        ''' Redeeming goods from a buyer that previously bougth the goods.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(15)> _
        RedeemFromBuyer

        ''' <summary>
        ''' Redeeming goods to a supplier that previously sold the goods.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(16)> _
        RedeemToSupplier

    End Enum

End Namespace
