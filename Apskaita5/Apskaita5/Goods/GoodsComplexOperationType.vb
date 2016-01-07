Namespace Goods

    ''' <summary>
    ''' Represents a type of a complex goods operation,
    ''' e.g. <see cref="GoodsComplexOperationType.BulkDiscard">BulkDiscard</see> for
    ''' <see cref="GoodsComplexOperationDiscard">GoodsComplexOperationDiscard</see>.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum GoodsComplexOperationType

        ''' <summary>
        ''' Transfer of goods between company's warehouses:
        ''' <see cref="GoodsComplexOperationInternalTransfer">GoodsComplexOperationInternalTransfer</see>.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(1)> _
        InternalTransfer

        ''' <summary>
        ''' Production of goods:
        ''' <see cref="GoodsComplexOperationProduction">GoodsComplexOperationProduction</see>.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(2)> _
        Production

        ''' <summary>
        ''' Inventorization of goods:
        ''' <see cref="GoodsComplexOperationInventorization">GoodsComplexOperationInventorization</see>.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(3)> _
        Inventorization

        ''' <summary>
        ''' Discard of goods:
        ''' <see cref="GoodsComplexOperationDiscard">GoodsComplexOperationDiscard</see>.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(4)> _
        BulkDiscard

        ''' <summary>
        ''' Goods price cut (when the balance value is reduced to the market value):
        ''' <see cref="GoodsComplexOperationPriceCut">GoodsComplexOperationPriceCut</see>.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(5)> _
        BulkPriceCut

        ''' <summary>
        ''' Transfer of goods balance:
        ''' <see cref="GoodsComplexOperationTransferOfBalance">GoodsComplexOperationTransferOfBalance</see>.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(6)> _
        TransferOfBalance

    End Enum

End Namespace
