Namespace ActiveReports

    ''' <summary>
    ''' Represents a type of <see cref="DebtStatementItem">DebtStatementItem</see>.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum DebtStatementItemType

        ''' <summary>
        ''' The item that contains data about the debt balance at the start of the period.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(0)> _
        BalanceStart = 0

        ''' <summary>
        ''' The item that contains data about an individual transaction during the period.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(1)> _
        Transaction = 1

        ''' <summary>
        ''' The item that contains data about the total turnover during the period.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(2)> _
        Turnover = 2

        ''' <summary>
        ''' The item that contains data about the debt balance at the end of the period.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(3)> _
        BalanceEnd = 3

    End Enum

End Namespace
