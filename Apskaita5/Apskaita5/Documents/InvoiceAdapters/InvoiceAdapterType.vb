
Namespace Documents.InvoiceAdapters

    ''' <summary>
    ''' Represents <see cref="InvoiceAdapters.IInvoiceAdapter.Type">
    ''' a type of an invoice adapter</see> (invoice attached operation type)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum InvoiceAdapterType

        ''' <summary>
        ''' <see cref="AssetAcquisitionInvoiceAdapter">Long term asset acquisition operation</see>.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(4)> _
        LongTermAssetPurchase

        ''' <summary>
        ''' <see cref="AssetSaleInvoiceAdapter">Long term asset transfer (sale)</see>.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(3)> _
        LongTermAssetSale

        ''' <summary>
        ''' <see cref="AssetAcquisitionValueIncreaseInvoiceAdapter">Long term asset acquisition value increase</see>.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(6)> _
        LongTermAssetAcquisitionValueChange

        ''' <summary>
        ''' <see cref="ServiceInvoiceAdapter">Service sold or bought</see>.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(2)> _
        Service

        ''' <summary>
        ''' <see cref="GoodsAcquisitionInvoiceAdapter">Goods acquisition</see>.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(5)> _
        GoodsAcquisition

        ''' <summary>
        ''' <see cref="GoodsSaleInvoiceAdapter">Goods transfer (sale)</see>.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(1)> _
        GoodsTransfer

        ''' <summary>
        ''' <see cref="GoodsDiscountInvoiceAdapter">Goods discount received</see>.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(7)> _
        GoodsConsignmentDiscount

        ''' <summary>
        ''' <see cref="GoodsAddedCostsInvoiceAdapter">Goods acquisition value increase</see>.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(8)> _
        GoodsConsignmentAdditionalCosts

    End Enum

End Namespace