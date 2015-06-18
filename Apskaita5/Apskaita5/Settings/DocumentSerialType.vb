Namespace Settings

    ''' <summary>
    ''' Represents a type of the document that the serial is ment for.
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum DocumentSerialType

        ''' <summary>
        ''' An <see cref="Documents.InvoiceMade">invoice</see>.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(0, "sf")> _
        Invoice

        ''' <summary>
        ''' A <see cref="Documents.TillIncomeOrder">till income order</see>.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(2, "kpo")> _
        TillIncomeOrder

        ''' <summary>
        ''' A <see cref="Documents.TillSpendingsOrder">till spendings order</see>.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(3, "kio")> _
        TillSpendingsOrder

        ''' <summary>
        ''' A <see cref="Workers.Contract">labour contract</see>.
        ''' </summary>
        ''' <remarks></remarks>
        <EnumValue(1, "ds")> _
        LabourContract

    End Enum

End Namespace