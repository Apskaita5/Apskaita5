Namespace Documents

    ''' <summary>
    ''' Describes how the particular object (service, goods, etc.) is used in trade operations (sale, purchase, etc.).
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum TradedItemType

        ''' <summary>
        ''' Objects that are sold by the company.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(1)> _
        Sales

        ''' <summary>
        ''' Objects that are purchased by the company.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(0)> _
        Purchases

        ''' <summary>
        ''' Objects that are purchased and sold by the company.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(2)> _
        All

    End Enum

End Namespace

