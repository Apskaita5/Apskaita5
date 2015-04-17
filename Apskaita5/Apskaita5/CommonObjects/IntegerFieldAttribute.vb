''' <summary>
''' Represents an attribute that is used to mark business objects' properties holding an integer value 
''' and to store basic business rules (mandatory, allowed values, etc.).
''' </summary>
''' <remarks>Used for validation purposes in <see cref="IntegerFieldValidation">IntegerFieldValidation</see> method.
''' Could be used by GUI to initialize appropriate controls.</remarks>
<Serializable()> _
<AttributeUsage(AttributeTargets.Property, AllowMultiple:=False, Inherited:=True)> _
Public Class IntegerFieldAttribute
    Inherits System.Attribute

    Private _ValueRequired As ValueRequiredLevel = ValueRequiredLevel.Optional
    Private _AllowNegative As Boolean = True
    Private _WithinRange As Boolean = False
    Private _MinValue As Integer = 0
    Private _MaxValue As Integer = 100
    Private _ErrorIfExceedsRange As Boolean = True

    ''' <summary>
    ''' Whether the property value is mandatory (not equals 0).
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly Property ValueRequired() As ValueRequiredLevel
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            Return _ValueRequired
        End Get
    End Property

    ''' <summary>
    ''' Whether a negative property value is allowed.
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly Property AllowNegative() As Boolean
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            Return _AllowNegative
        End Get
    End Property

    ''' <summary>
    ''' Whether a property value should be within a certain range.
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly Property WithinRange() As Boolean
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            Return _WithinRange
        End Get
    End Property

    ''' <summary>
    ''' A minimum allowed property value.
    ''' </summary>
    ''' <remarks>Only used if the <see cref="WithinRange">WithinRange</see> property is set to TRUE.</remarks>
    Public ReadOnly Property MinValue() As Integer
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            Return _MinValue
        End Get
    End Property

    ''' <summary>
    ''' A maximum allowed property value.
    ''' </summary>
    ''' <remarks>Only used if the <see cref="WithinRange">WithinRange</see> property is set to TRUE.</remarks>
    Public ReadOnly Property MaxValue() As Integer
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            Return _MaxValue
        End Get
    End Property

    ''' <summary>
    ''' Whether a property value, that exceeds either <see cref="MinValue">MinValue</see> 
    ''' or <see cref="MaxValue">MaxValue</see>, should be treated as an error (not warning).
    ''' </summary>
    ''' <remarks>Only used if the <see cref="WithinRange">WithinRange</see> property is set to TRUE.</remarks>
    Public ReadOnly Property ErrorIfExceedsRange() As Boolean
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            Return _ErrorIfExceedsRange
        End Get
    End Property


    ''' <summary>
    ''' Creates a new IntegerFieldAttribute instance.
    ''' </summary>
    ''' <param name="valueIsMandatory">Whether the property value is mandatory (not equals 0).</param>
    ''' <param name="valueAllowNegative">Whether a negative property value is allowed.</param>
    ''' <param name="valueWithinRange">Whether a property value should be within a certain range.</param>
    ''' <param name="valueMinValue">A minimum allowed property value.</param>
    ''' <param name="valueMaxValue">A maximum allowed property value.</param>
    ''' <param name="valueErrorIfExceedsRange">Whether a property value, that exceeds either <paramref name="valueMinValue">valueMinValue</paramref> 
    ''' or <paramref name="valueMaxValue">valueMaxValue</paramref>, should be treated as an error (not warning).</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal valueValueRequired As ValueRequiredLevel, ByVal valueAllowNegative As Boolean, _
        Optional ByVal valueWithinRange As Boolean = False, Optional ByVal valueMinValue As Integer = 0, _
        Optional ByVal valueMaxValue As Integer = 1, Optional ByVal valueErrorIfExceedsRange As Boolean = True)

        _ValueRequired = valueValueRequired
        _AllowNegative = valueAllowNegative
        _WithinRange = valueWithinRange
        _MinValue = valueMinValue
        _MaxValue = valueMaxValue
        _ErrorIfExceedsRange = valueErrorIfExceedsRange

    End Sub

End Class
