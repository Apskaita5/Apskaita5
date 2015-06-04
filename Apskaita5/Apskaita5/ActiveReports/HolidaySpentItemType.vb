Namespace ActiveReports

    ''' <summary>
    ''' Represents a type of <see cref="HolidaySpentItem">HolidaySpentItem</see>.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum HolidaySpentItemType

        ''' <summary>
        ''' Holiday was granted and spent by a worker.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(0)> _
        Spent

        ''' <summary>
        ''' Unused holiday were compensated when terminating a labour contract.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(1)> _
        Compensated

        ''' <summary>
        ''' Technical (manual) correction of holiday.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(2)> _
        Correction

    End Enum

End Namespace