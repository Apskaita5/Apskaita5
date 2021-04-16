Imports ApskaitaObjects.Attributes

Namespace General

    ''' <summary>
    ''' Represents types of <see cref="General.ConsolidatedReportItem">financial statements</see> 
    ''' structural elements.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum FinancialStatementItemType
        ''' <summary>
        ''' Represents an item of the balance sheet.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(0)> _
        StatementOfFinancialPosition
        ''' <summary>
        ''' Represents an item of the income statement.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(1)> _
        StatementOfComprehensiveIncome
        ''' <summary>
        ''' Represents a root item of the balance sheet (assets or capital).
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(2)> _
        HeaderStatementOfFinancialPosition
        ''' <summary>
        ''' Represents a root item of the income statement.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(3)> _
        HeaderStatementOfComprehensiveIncome
        ''' <summary>
        ''' Represents a root item of the consolidated financial reports common structure.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(4)> _
        HeaderGeneral
    End Enum

End Namespace


