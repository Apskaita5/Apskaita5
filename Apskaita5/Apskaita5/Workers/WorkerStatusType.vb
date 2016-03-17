Imports ApskaitaObjects.Attributes

Namespace Workers

    ''' <summary>
    ''' Represents a type of an employee's status change.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum WorkerStatusType

        ''' <summary>
        ''' Represents the fact of entering a labour contract.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(0, "d")> _
        Employed

        ''' <summary>
        ''' Represents the fact of terminating a labour contract.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(2, "n")> _
        Fired

        ''' <summary>
        ''' Represents a change of an applicable NPD (amount of the non-taxable personal income size).
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(5, "p")> _
        NPD

        ''' <summary>
        ''' Represents a change of an applicable PNPD (supplementary amount of the non-taxable personal income size)
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(6, "r")> _
        PNPD

        ''' <summary>
        ''' Represents a change of wage.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(7, "u")> _
        Wage

        ''' <summary>
        ''' Represents a change of extra pay.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(1, "i")> _
        ExtraPay

        ''' <summary>
        ''' Represents a change of an annual holiday rate (days per work year).
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(3, "a")> _
        Holiday

        ''' <summary>
        ''' Represents a change of a work load (proportion to the 40 hours standard work week).
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(8, "k")> _
        WorkLoad

        ''' <summary>
        ''' Represents a correction of cumulated annual holidays.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(4, "o")> _
        HolidayCorrection

        ''' <summary>
        ''' Represents a change of the employee's position.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(9, "f")> _
        Position

    End Enum

End Namespace