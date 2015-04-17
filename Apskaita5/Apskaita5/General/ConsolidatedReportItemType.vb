Namespace General

    Public Enum ConsolidatedReportItemType
        ''' <summary>
        ''' Income statement item that reflects income (credit residual).
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(0)> _
        Income
        ''' <summary>
        ''' Income statement item that reflects costs (debit residual).
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(1)> _
        Costs
        ''' <summary>
        ''' Balance sheet item that reflects assets (debit residual).
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(2)> _
        Assets
        ''' <summary>
        ''' Balance sheet item that reflects equity (credit residual).
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(3)> _
        Capital
    End Enum

End Namespace
