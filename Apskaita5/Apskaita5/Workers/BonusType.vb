Imports ApskaitaObjects.Attributes

Namespace Workers

    ''' <summary>
    ''' Represent a type of bonus payed to a worker.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum BonusType
        ''' <summary>
        ''' Annual bonus.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(1, "m")> _
        m
        ''' <summary>
        ''' Quarterage bonus.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(0, "k")> _
        k
    End Enum

End Namespace
