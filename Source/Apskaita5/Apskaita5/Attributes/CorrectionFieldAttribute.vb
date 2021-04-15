Namespace Attributes

    ''' <summary>
    ''' Represents an attribute that is used to mark business objects' properties 
    ''' holding an integer correction value (e.g. correction of sum calculated 
    ''' as amount * unit value) and to store basic business rules (allowed 
    ''' correction range, etc.).
    ''' </summary>
    ''' <remarks>Used for validation purposes in <see cref="IntegerFieldValidation">IntegerFieldValidation</see> method.
    ''' Could be used by GUI to initialize appropriate controls.</remarks>
    <Serializable()> _
    <AttributeUsage(AttributeTargets.Property, AllowMultiple:=False, Inherited:=True)> _
    Public Class CorrectionFieldAttribute
        Inherits IntegerFieldAttribute

        ''' <summary>
        ''' Creates a new CorrectionFieldAttribute instance.
        ''' </summary>
        ''' <param name="valueMinValue">A minimum allowed property value.</param>
        ''' <param name="valueMaxValue">A maximum allowed property value.</param>
        ''' <param name="valueErrorIfExceedsRange">Whether a property value, that exceeds either <paramref name="valueMinValue">valueMinValue</paramref> 
        ''' or <paramref name="valueMaxValue">valueMaxValue</paramref>, should be treated as an error (not warning).</param>
        ''' <remarks></remarks>
        Public Sub New(Optional ByVal valueMinValue As Integer = -1000, _
            Optional ByVal valueMaxValue As Integer = 1000, _
            Optional ByVal valueErrorIfExceedsRange As Boolean = True)

            MyBase.new(ValueRequiredLevel.Optional, (valueMinValue < 0), _
                True, valueMinValue, valueMaxValue, valueErrorIfExceedsRange)

        End Sub

    End Class

End Namespace
