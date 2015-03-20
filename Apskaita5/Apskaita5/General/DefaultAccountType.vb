Namespace General

    Public Enum DefaultAccountType
        ''' <summary>
        ''' Default <see cref=" General.Account">account</see> that stores bank transaction data.
        ''' </summary>
        ''' <remarks></remarks>
        Bank
        ''' <summary>
        ''' Default <see cref=" General.Account">account</see> that stores till transaction data.
        ''' </summary>
        ''' <remarks></remarks>
        Till
        ''' <summary>
        ''' Default <see cref=" General.Account">account</see> that stores (buyers) debts to the company.
        ''' </summary>
        ''' <remarks></remarks>
        Buyers
        ''' <summary>
        ''' Default <see cref=" General.Account">account</see> that stores company's debts to suppliers.
        ''' </summary>
        ''' <remarks></remarks>
        Suppliers
        ''' <summary>
        ''' Default <see cref=" General.Account">account</see> that stores wages payable.
        ''' </summary>
        ''' <remarks></remarks>
        WagePayable
        ''' <summary>
        ''' Default <see cref=" General.Account">account</see> that stores social insurance contributions (SODRA) payable.
        ''' </summary>
        ''' <remarks></remarks>
        WageSodraPayable
        ''' <summary>
        ''' Default <see cref=" General.Account">account</see> that stores health insurance contributions (PSD) payable to SODRA.
        ''' </summary>
        ''' <remarks></remarks>
        WagePsdPayable
        ''' <summary>
        ''' Default <see cref=" General.Account">account</see> that stores health insurance contributions (PSD) payable to VMI.
        ''' </summary>
        ''' <remarks></remarks>
        WagePsdPayableToVMI
        ''' <summary>
        ''' Default <see cref=" General.Account">account</see> that stores bankruptcy insurance contributions payable.
        ''' </summary>
        ''' <remarks></remarks>
        WageGuaranteeFundPayable
        ''' <summary>
        ''' Default <see cref=" General.Account">account</see> that stores personal income tax (GPM) payable.
        ''' </summary>
        ''' <remarks></remarks>
        WageGpmPayable
        ''' <summary>
        ''' Default <see cref=" General.Account">account</see> that stores VAT payable.
        ''' </summary>
        ''' <remarks></remarks>
        VatPayable
        ''' <summary>
        ''' Default <see cref=" General.Account">account</see> that stores VAT receivable.
        ''' </summary>
        ''' <remarks></remarks>
        VatReceivable
        ''' <summary>
        ''' Default <see cref=" General.Account">account</see> that stores wage imprest payable.
        ''' </summary>
        ''' <remarks></remarks>
        WageImprestPayable
        ''' <summary>
        ''' Default <see cref=" General.Account">account</see> that stores deductions from wage.
        ''' </summary>
        ''' <remarks></remarks>
        WageWithdraw
        ''' <summary>
        ''' Default technical <see cref=" General.Account">account</see> that is used for <see cref=" General.ClosingEntriesCommand">closing of the nominal accounts</see>.
        ''' </summary>
        ''' <remarks></remarks>
        ClosingSummary
        ''' <summary>
        ''' Default <see cref=" General.Account">account</see> that stores company results (income/loss) for the current period.
        ''' </summary>
        ''' <remarks></remarks>
        CurrentProfit
        ''' <summary>
        ''' Default <see cref=" General.Account">account</see> that stores company results (income/loss) for the previous periods.
        ''' </summary>
        ''' <remarks></remarks>
        FormerProfit
        ''' <summary>
        ''' Default <see cref=" General.Account">account</see> that stores non wage related social insurance contributions (SODRA) payable.
        ''' </summary>
        ''' <remarks></remarks>
        OtherSodraPayable
        ''' <summary>
        ''' Default <see cref=" General.Account">account</see> that stores non wage related health insurance contributions (PSD) payable.
        ''' </summary>
        ''' <remarks></remarks>
        OtherPsdPayable
        ''' <summary>
        ''' Default <see cref=" General.Account">account</see> that stores non wage related personal income tax (GPM) payable.
        ''' </summary>
        ''' <remarks></remarks>
        OtherGpmPayable
        ''' <summary>
        ''' Default <see cref=" General.Account">account</see> that stores goods sales net costs.
        ''' </summary>
        ''' <remarks></remarks>
        GoodsSalesNetCosts
        ''' <summary>
        ''' Default <see cref=" General.Account">account</see> that stores goods purchase costs (periodic accounting).
        ''' </summary>
        ''' <remarks></remarks>
        GoodsPurchases
        ''' <summary>
        ''' Default <see cref=" General.Account">account</see> that stores received discounts for goods (periodic accounting).
        ''' </summary>
        ''' <remarks></remarks>
        GoodsDiscounts
        ''' <summary>
        ''' Default <see cref=" General.Account">account</see> that stores goods value reduction costs (when net cost is higher then the market value).
        ''' </summary>
        ''' <remarks></remarks>
        GoodsValueReduction
    End Enum

End Namespace