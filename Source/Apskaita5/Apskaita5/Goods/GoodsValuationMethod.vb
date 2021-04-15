Imports ApskaitaObjects.Attributes

Namespace Goods

    ''' <summary>
    ''' Represents a goods valuation method (a method to calculate costs
    ''' of the goods discarded or transfered).
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum GoodsValuationMethod

        ''' <summary>
        ''' FIFO - first in first out, see methodology for BAS No 9 ""Stores"" para. 36.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(0)> _
        FIFO

        ''' <summary>
        ''' LIFO - last in first out, see methodology for BAS No 9 ""Stores"" para. 37.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(1)> _
        LIFO

        ''' <summary>
        ''' Current average values, see methodology for BAS No 9 ""Stores"" para. 38.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(2)> _
        Average

    End Enum

End Namespace
