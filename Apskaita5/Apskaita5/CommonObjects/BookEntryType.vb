''' <summary>
''' Represents <see cref="General.BookEntry">a single ledger transaction</see> type, 
''' whether the account is credited or debited.
''' </summary>
''' <remarks>Inherited usage of database ENUM (char) type for backward compartability.</remarks>
Public Enum BookEntryType
    <EnumValue(1, "Kreditas")> _
    Kreditas
    <EnumValue(0, "Debetas")> _
    Debetas
End Enum