Imports ApskaitaObjects.Attributes

Namespace Documents

    ''' <summary>
    ''' Represents a type of account for money turnover (bank, till, etc.)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CashAccountType

        ''' <summary>
        ''' A normal bank account.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(0)> _
        BankAccount

        ''' <summary>
        ''' An account that is administered not by a bank (e.g. PayPal, credit card administrator, etc.).
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(1)> _
        PseudoBankAccount

        ''' <summary>
        ''' A till account (cash).
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(2)> _
        Till

    End Enum

End Namespace
