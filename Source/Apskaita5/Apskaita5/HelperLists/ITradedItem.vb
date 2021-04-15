Namespace HelperLists

    ''' <summary>
    ''' An interface used to indicate that a value object is a traded item (goods, services, etc.)
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface ITradedItem

        ''' <summary>
        ''' an ID of the traded item
        ''' </summary>
        ''' <remarks></remarks>
        ReadOnly Property ID() As Integer

        ''' <summary>
        ''' indicates how the item is used in trade operations (sale, purchase, etc.)
        ''' </summary>
        ''' <remarks></remarks>
        ReadOnly Property TradedType() As Documents.TradedItemType

    End Interface

End Namespace
