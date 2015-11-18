Namespace General

    ''' <summary>
    ''' Represents default <see cref="General.CompanyAccount">account</see> value types.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum DefaultAccountType
        ''' <summary>
        ''' Default <see cref="General.CompanyAccount">account</see> that stores bank transaction data.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(0, "BK")> _
        Bank
        ''' <summary>
        ''' Default <see cref="General.CompanyAccount">account</see> that stores till transaction data.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(3, "KS")> _
        Till
        ''' <summary>
        ''' Default <see cref="General.CompanyAccount">account</see> that stores (buyers) debts to the company.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(1, "PR")> _
        Buyers
        ''' <summary>
        ''' Default <see cref="General.CompanyAccount">account</see> that stores company's debts to suppliers.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(2, "TK")> _
        Suppliers
        ''' <summary>
        ''' Default <see cref="General.CompanyAccount">account</see> that stores wages payable.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(9, "DU")> _
        WagePayable
        ''' <summary>
        ''' Default <see cref="General.CompanyAccount">account</see> that stores social insurance contributions (SODRA) payable.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(11, "SD")> _
        WageSodraPayable
        ''' <summary>
        ''' Default <see cref="General.CompanyAccount">account</see> that stores health insurance contributions (PSD) payable to SODRA.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(10, "SS")> _
        WagePsdPayable
        ''' <summary>
        ''' Default <see cref="General.CompanyAccount">account</see> that stores health insurance contributions (PSD) payable to VMI.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(13, "SV")> _
        WagePsdPayableToVMI
        ''' <summary>
        ''' Default <see cref="General.CompanyAccount">account</see> that stores bankruptcy insurance contributions payable.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(7, "GR")> _
        WageGuaranteeFundPayable
        ''' <summary>
        ''' Default <see cref="General.CompanyAccount">account</see> that stores personal income tax (GPM) payable.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(6, "GP")> _
        WageGpmPayable
        ''' <summary>
        ''' Default <see cref="General.CompanyAccount">account</see> that stores VAT payable.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(4, "PV")> _
        VatPayable
        ''' <summary>
        ''' Default <see cref="General.CompanyAccount">account</see> that stores VAT receivable.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(5, "PG")> _
        VatReceivable
        ''' <summary>
        ''' Default <see cref="General.CompanyAccount">account</see> that stores wage imprest payable.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(8, "DA")> _
        WageImprestPayable
        ''' <summary>
        ''' Default <see cref="General.CompanyAccount">account</see> that stores deductions from wage.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(12, "IS")> _
        WageWithdraw
        ''' <summary>
        ''' Default technical <see cref="General.CompanyAccount">account</see> that is used for <see cref=" General.ClosingEntriesCommand">closing of the nominal accounts</see>.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(14, "SU")> _
        ClosingSummary
        ''' <summary>
        ''' Default <see cref="General.CompanyAccount">account</see> that stores company results (income/loss) for the current period.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(15, "PL")> _
        CurrentProfit
        ''' <summary>
        ''' Default <see cref="General.CompanyAccount">account</see> that stores company results (income/loss) for the previous periods.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(16, "PA")> _
        FormerProfit
        ''' <summary>
        ''' Default <see cref="General.CompanyAccount">account</see> that stores non wage related social insurance contributions (SODRA) payable.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(19, "OS")> _
        OtherSodraPayable
        ''' <summary>
        ''' Default <see cref="General.CompanyAccount">account</see> that stores non wage related health insurance contributions (PSD) payable.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(18, "OP")> _
        OtherPsdPayable
        ''' <summary>
        ''' Default <see cref="General.CompanyAccount">account</see> that stores non wage related personal income tax (GPM) payable.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(17, "OG")> _
        OtherGpmPayable
        ''' <summary>
        ''' Default <see cref="General.CompanyAccount">account</see> that stores goods sales net costs.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(20, "GS")> _
        GoodsSalesNetCosts
        ''' <summary>
        ''' Default <see cref="General.CompanyAccount">account</see> that stores goods purchase costs (periodic accounting).
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(21, "GQ")> _
        GoodsPurchases
        ''' <summary>
        ''' Default <see cref="General.CompanyAccount">account</see> that stores received discounts for goods (periodic accounting).
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(22, "GD")> _
        GoodsDiscounts
        ''' <summary>
        ''' Default <see cref="General.CompanyAccount">account</see> that stores goods value reduction costs (when net cost is higher then the market value).
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(23, "GV")> _
        GoodsValueReduction
        ''' <summary>
        ''' Default <see cref="General.CompanyAccount">account</see> that stores holiday reserve.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(24, "HR")> _
        HolidayReserve
    End Enum

End Namespace