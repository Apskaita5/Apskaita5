Imports ApskaitaObjects.Attributes

Namespace Goods

    ''' <summary>
    ''' Represents a goods accounting method.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum GoodsAccountingMethod

        ''' <summary>
        ''' A persistent goods accounting method.
        ''' </summary>
        ''' <remarks>A general ledger transaction is inserted and the goods
        ''' costs are calculated for every goods transaction.</remarks>
        <EnumValue(0)> _
        Persistent

        ''' <summary>
        ''' A periodic goods accounting method.
        ''' </summary>
        ''' <remarks>General ledger transactions are not inserted for any
        ''' goods transactions, goods costs are not calculated for transactions.
        ''' Subtotal general ledger transactions are only inserted and the total goods costs
        ''' are only calculated by <see cref="GoodsComplexOperationInventorization">
        ''' inventorization operations</see> for a certain periods.</remarks>
        <EnumValue(1)> _
        Periodic

    End Enum

End Namespace