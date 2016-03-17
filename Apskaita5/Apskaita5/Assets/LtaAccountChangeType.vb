Imports ApskaitaObjects.Attributes

Namespace Assets

    ''' <summary>
    ''' Represents a type of long term asset account change. 
    ''' Corresponds to the accounts that are used for a long term asset accounting.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum LtaAccountChangeType

        ''' <summary>
        ''' A change of the <see cref="LongTermAsset.AccountAcquisition">AccountAcquisition</see>.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(0, "aqs")> _
        AcquisitionAccount

        ''' <summary>
        ''' A change of the <see cref="LongTermAsset.AccountValueIncrease">AccountValueIncrease</see>.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(3, "vli")> _
        ValueIncreaseAccount

        ''' <summary>
        ''' A change of the <see cref="LongTermAsset.AccountValueDecrease">AccountValueDecrease</see>.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(2, "vld")> _
        ValueDecreaseAccount

        ''' <summary>
        ''' A change of the <see cref="LongTermAsset.AccountAccumulatedAmortization">AccountAccumulatedAmortization</see>.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(1, "amr")> _
        AmortizationAccount

        ''' <summary>
        ''' A change of the <see cref="LongTermAsset.AccountRevaluedPortionAmmortization">AccountRevaluedPortionAmmortization</see>.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(4, "vam")> _
        ValueIncreaseAmortizationAccount

    End Enum

End Namespace