Imports ApskaitaObjects.Attributes

Namespace Goods

    ''' <summary>
    ''' Represents a type of the component consumed during production of goods.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum ProductionComponentType

        ''' <summary>
        ''' Goods (components, stock, etc.) that are consumed when producing 
        ''' the product goods, see <see cref="ProductionComponentItem">ProductionComponentItem</see>.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(0, "a")> _
        Component

        ''' <summary>
        ''' Production costs that should be added to the value of the
        ''' goods produced, see <see cref="ProductionCostItem">ProductionCostItem</see>.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(1, "s")> _
        Costs

    End Enum

End Namespace
