Imports ApskaitaObjects.Attributes

Namespace Assets

    ''' <summary>
    ''' Represents types of the long term asset operations.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum LtaOperationType

        ''' <summary>
        ''' A calculation of an amortization (depreciation).
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(2, "amo")> _
        Amortization

        ''' <summary>
        ''' Setting of a new amortization (depreciation) period.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(3, "alk")> _
        AmortizationPeriod

        ''' <summary>
        ''' Increase of the acquisition value (supplementary costs, etc.).
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(1, "avi")> _
        AcquisitionValueIncrease

        ''' <summary>
        ''' Reassesment of a long term asset.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(8, "avd")> _
        ValueChange

        ''' <summary>
        ''' A change of an account that is used for a long term asset accounting.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(0, "aac")> _
        AccountChange

        ''' <summary>
        ''' Discard of a long term asset.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(4, "nur")> _
        Discard

        ''' <summary>
        ''' Transfer of a long term asset (sale, etc.).
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(5, "per")> _
        Transfer

        ''' <summary>
        ''' Bringing into operational state.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(7, "nau")> _
        UsingStart

        ''' <summary>
        ''' Withdrawing from the operational state.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(6, "nau")> _
        UsingEnd

    End Enum

End Namespace