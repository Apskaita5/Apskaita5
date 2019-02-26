Imports ApskaitaObjects.Attributes

Namespace General

    ''' <summary>
    ''' Represents default <see cref="General.CompanyRate">rate</see> types.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum DefaultRateType
        ''' <summary>
        ''' Default (most common) VAT rate.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(6)> _
        Vat
        ''' <summary>
        ''' Rate of social security contributions deducted from wage.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(4)> _
        SodraEmployee
        ''' <summary>
        ''' Rate of social security contributions payed by an employer.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(5)> _
        SodraEmployer
        ''' <summary>
        ''' Rate of health insurance contributions deducted from wage.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(2)> _
        PsdEmployee
        ''' <summary>
        ''' Rate of health insurance contributions payed by an employer.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(3)> _
        PsdEmployer
        ''' <summary>
        ''' Rate of guarantee fund contributions (insolvency insurance for workers).
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(1)> _
        GuaranteeFund
        ''' <summary>
        ''' Personal income tax (GPM) rate.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(0)> _
        GpmWage
        ''' <summary>
        ''' Wage for overtime work rate (against normal wage).
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(9)> _
        WageRateOvertime
        ''' <summary>
        ''' Wage for night work rate (against normal wage).
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(8)> _
        WageRateNight
        ''' <summary>
        ''' Wage for work during public holidays rate (against normal wage).
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(10)> _
        WageRatePublicHolidays
        ''' <summary>
        ''' Wage for work during rest days rate (against normal wage).
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(11)> _
        WageRateRestTime
        ''' <summary>
        ''' Wage for dangerous/unsafe work rate (against normal wage).
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(7)> _
        WageRateDeviations
        ''' <summary>
        ''' Sickness benefit rate as payed by an employer.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(12)>
        WageRateSickLeave
        ''' <summary>
        ''' Personal income tax (GPM) rate for sick leave compensation (for year 2019 and later).
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(13)>
        GpmSickLeave
    End Enum

End Namespace