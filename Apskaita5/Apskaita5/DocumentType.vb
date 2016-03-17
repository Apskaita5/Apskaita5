Imports ApskaitaObjects.Attributes

''' <summary>
''' Represents a type of a document that encapsulates a <see cref="General.JournalEntry">JournalEntry</see>.
''' </summary>
''' <remarks></remarks>
Public Enum DocumentType

    ''' <summary>
    ''' An <see cref="Documents.InvoiceMade">InvoiceMade</see>.
    ''' </summary>
    ''' <remarks></remarks>
    <EnumValue(4, "sf")> _
    InvoiceMade

    ''' <summary>
    ''' An <see cref="Documents.InvoiceReceived">InvoiceReceived</see>.
    ''' </summary>
    ''' <remarks></remarks>
    <EnumValue(5, "sg")> _
    InvoiceReceived

    ''' <summary>
    ''' A <see cref="Documents.TillIncomeOrder">TillIncomeOrder</see>.
    ''' </summary>
    ''' <remarks></remarks>
    <EnumValue(3, "kpo")> _
    TillIncomeOrder

    ''' <summary>
    ''' A <see cref="Documents.TillSpendingsOrder">TillSpendingsOrder</see>.
    ''' </summary>
    ''' <remarks></remarks>
    <EnumValue(2, "kio")> _
    TillSpendingOrder

    ''' <summary>
    ''' A <see cref="Workers.WageSheet">WageSheet</see>.
    ''' </summary>
    ''' <remarks></remarks>
    <EnumValue(1, "du")> _
    WageSheet

    ''' <summary>
    ''' An <see cref="Workers.ImprestSheet">ImprestSheet</see>.
    ''' </summary>
    ''' <remarks></remarks>
    <EnumValue(0, "av")> _
    ImprestSheet

    ''' <summary>
    ''' A <see cref="General.ClosingEntriesCommand">ClosingEntriesCommand</see>.
    ''' </summary>
    ''' <remarks></remarks>
    <EnumValue(12, "uz")> _
    ClosingEntries

    ''' <summary>
    ''' A <see cref="Assets.OperationDiscard">long term asset OperationDiscard</see>.
    ''' </summary>
    ''' <remarks></remarks>
    <EnumValue(10, "t")> _
    LongTermAssetDiscard

    ''' <summary>
    ''' A <see cref="Assets.OperationAccountChange">long term asset OperationAccountChange</see>.
    ''' </summary>
    ''' <remarks></remarks>
    <EnumValue(16, "tsp")> _
    LongTermAssetAccountChange

    ''' <summary>
    ''' A <see cref="Assets.OperationAmortization">long term asset OperationAmortization</see>.
    ''' </summary>
    ''' <remarks></remarks>
    <EnumValue(7, "amo")> _
    Amortization

    ''' <summary>
    ''' A <see cref="Goods.GoodsComplexOperationInternalTransfer">GoodsComplexOperationInternalTransfer</see>.
    ''' </summary>
    ''' <remarks></remarks>
    <EnumValue(6, "a")> _
    GoodsInternalTransfer

    ''' <summary>
    ''' A <see cref="Goods.GoodsComplexOperationPriceCut">GoodsComplexOperationPriceCut</see>.
    ''' </summary>
    ''' <remarks></remarks>
    <EnumValue(13, "v")> _
    GoodsRevalue

    ''' <summary>
    ''' A <see cref="Goods.GoodsComplexOperationProduction">GoodsComplexOperationProduction</see>.
    ''' </summary>
    ''' <remarks></remarks>
    <EnumValue(9, "g")> _
    GoodsProduction

    ''' <summary>
    ''' A <see cref="Goods.GoodsOperationDiscard">GoodsOperationDiscard</see>.
    ''' </summary>
    ''' <remarks></remarks>
    <EnumValue(11, "tn")> _
    GoodsWriteOff

    ''' <summary>
    ''' A <see cref="Goods.GoodsComplexOperationInventorization">GoodsComplexOperationInventorization</see>.
    ''' </summary>
    ''' <remarks></remarks>
    <EnumValue(19, "gi")> _
    GoodsInventorization

    ''' <summary>
    ''' A <see cref="Goods.GoodsOperationAccountChange">GoodsOperationAccountChange</see>.
    ''' </summary>
    ''' <remarks></remarks>
    <EnumValue(20, "ga")> _
    GoodsAccountChange

    ''' <summary>
    ''' A <see cref="Documents.BankOperation">BankOperation</see>.
    ''' </summary>
    ''' <remarks></remarks>
    <EnumValue(8, "b")> _
    BankOperation

    ''' <summary>
    ''' A <see cref="General.TransferOfBalance">TransferOfBalance</see>.
    ''' </summary>
    ''' <remarks></remarks>
    <EnumValue(14, "lik")> _
    TransferOfBalance

    ''' <summary>
    ''' An <see cref="Documents.AdvanceReport">AdvanceReport</see>.
    ''' </summary>
    ''' <remarks></remarks>
    <EnumValue(15, "ap")> _
    AdvanceReport

    ''' <summary>
    ''' An <see cref="Documents.Offset">Offset</see>.
    ''' </summary>
    ''' <remarks></remarks>
    <EnumValue(17, "sk")> _
    Offset

    ''' <summary>
    ''' An <see cref="Documents.AccumulativeCosts">AccumulativeCosts</see>.
    ''' </summary>
    ''' <remarks></remarks>
    <EnumValue(21, "ac")> _
    AccumulatedCosts

    ''' <summary>
    ''' A <see cref="Workers.HolidayPayReserve">HolidayPayReserve</see>.
    ''' </summary>
    ''' <remarks></remarks>
    <EnumValue(22, "hr")> _
    HolidayReserve

    ''' <summary>
    ''' A standalone journal entry without a parent document.
    ''' </summary>
    ''' <remarks></remarks>
    <EnumValue(18, "")> _
    None

End Enum