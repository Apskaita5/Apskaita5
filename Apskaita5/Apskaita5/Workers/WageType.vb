Namespace Workers

    ''' <summary>
    ''' Represents possible wage types.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum WageType

        ''' <summary>
        ''' Wage is calculated per position per month.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(0, "p")> _
        Position
        ''' <summary>
        ''' Wage is calculated per work hour.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(1, "v")> _
        Hourly

    End Enum

End Namespace