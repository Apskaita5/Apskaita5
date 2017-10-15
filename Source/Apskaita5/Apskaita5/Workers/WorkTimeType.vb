Imports ApskaitaObjects.Attributes

Namespace Workers

    ''' <summary>
    ''' Represents generalized work and rest time types to map <see cref="WorkTimeSheet">WorkTimeSheet</see> data to <see cref="WageSheet">WageSheet</see>.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum WorkTimeType

        ''' <summary>
        ''' Work time at night.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(0)> _
        NightWork

        ''' <summary>
        ''' Overtime work.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(3)> _
        OvertimeWork

        ''' <summary>
        ''' Work during public holiday or rest day.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(4)> _
        PublicHolidaysAndRestDayWork

        ''' <summary>
        ''' Work done in specific work environment.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(6)> _
        UnusualWork

        ''' <summary>
        ''' Truancy time.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(5)> _
        Truancy

        ''' <summary>
        ''' Down time.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(7)> _
        DownTime

        ''' <summary>
        ''' Other time included in work time.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(2)> _
        OtherIncluded

        ''' <summary>
        ''' Other time excluded from work time.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(1)> _
        OtherExcluded

        ''' <summary>
        ''' Sickness time.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(8)> _
        SickDays

        ''' <summary>
        ''' Annual holiday time.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(9)> _
        AnnualHolidays

        ''' <summary>
        ''' Other holiday time.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(10)> _
        OtherHolidays

    End Enum

End Namespace
