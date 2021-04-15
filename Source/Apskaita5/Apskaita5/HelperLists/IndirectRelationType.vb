Imports ApskaitaObjects.Attributes

Namespace HelperLists

    ''' <summary>
    ''' Represents a type of document or operation that indirectly references journal entries.
    ''' (when a journal entry is attached to some document, not created and managed by the document)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum IndirectRelationType

        ''' <summary>
        ''' A <see cref="Workers.PayOutNaturalPerson">PayOutNaturalPerson</see> operation.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(0)> _
        PayoutToResident

        ''' <summary>
        ''' A long term asset operation.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(1)> _
        LongTermAssetsOperation

        ''' <summary>
        ''' A <see cref="Assets.LongTermAsset">purchase</see> of a long term asset.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(2)> _
        LongTermAssetsPurchase

        ''' <summary>
        ''' A goods operation.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(3)> _
        GoodsOperation

        ''' <summary>
        ''' An <see cref="Documents.AdvanceReport">advance report.</see>
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(4)> _
        AdvanceReport

        ''' <summary>
        ''' An <see cref="Workers.PayOutNaturalPersonWithoutTaxes">payment to a natural person
        ''' when no taxes are deducted or payed by the company</see>.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(5)> _
        PayoutToResidentWithoutTaxes

        ''' <summary>
        ''' A <see cref="Documents.CustomVatOperation">custom VAT operation</see>.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(6)> _
        CustomVatOperation

    End Enum

End Namespace
