Namespace CsvReader

    <Flags()> _
    Public Enum ValueTrimmingOptions
        None = 0
        UnquotedOnly = 1
        QuotedOnly = 2
        All = UnquotedOnly Or QuotedOnly
    End Enum

End Namespace