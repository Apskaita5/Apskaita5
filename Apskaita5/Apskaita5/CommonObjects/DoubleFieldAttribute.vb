''' <summary>
''' Represents an attribute that is used to mark business objects' properties holding a double value
''' and to store basic business rules (mandatory, allowed values, etc.).
''' </summary>
''' <remarks>Used for validation purposes in <see cref="DoubleFieldValidation">DoubleFieldValidation</see>
''' method.
''' Could be used by GUI to initialize appropriate controls.</remarks>
<Serializable()> _
<AttributeUsage(AttributeTargets.Property, AllowMultiple:=False, Inherited:=True)> _
Public Class DoubleFieldAttribute
    Inherits Attribute

    Private _ValueRequired As ValueRequiredLevel = ValueRequiredLevel.Optional
    Private _AllowNegative As Boolean = True
    Private _Round As Integer = 2
    Private _WithinRange As Boolean = False
    Private _MinValue As Double = 0
    Private _MaxValue As Double = 100
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
    ''' Whether the property value can be negative.
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly Property AllowNegative() As Boolean
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            Return _AllowNegative
        End Get
    End Property

    ''' <summary>
    ''' Gets a round order of the property value.
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly Property Round() As Integer
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            Return _Round
        End Get
    End Property

    ''' <summary>
    ''' Wheather the property value should be within a provided range.
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly Property WithinRange() As Boolean
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            Return _WithinRange
        End Get
    End Property

    ''' <summary>
    ''' Minimum allowed property value.
    ''' </summary>
    ''' <remarks>Only used if <see cref="WithinRange">WithinRange</see> property is set to TRUE.</remarks>
    Public ReadOnly Property MinValue() As Double
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            Return _MinValue
        End Get
    End Property

    ''' <summary>
    ''' Maximum allowed property value.
    ''' </summary>
    ''' <remarks>Only used if <see cref="WithinRange">WithinRange</see> property is set to TRUE.</remarks>
    Public ReadOnly Property MaxValue() As Double
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            Return _MaxValue
        End Get
    End Property

    ''' <summary>
    ''' Wheather the property value exceeding the <see cref="MinValue">MinValue</see> 
    ''' or the <see cref="MaxValue">MaxValue</see> should be treated as error (not warning).
    ''' </summary>
    ''' <remarks>Only used if <see cref="WithinRange">WithinRange</see> property is set to TRUE.</remarks>
    Public ReadOnly Property ErrorIfExceedsRange() As Boolean
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            Return _ErrorIfExceedsRange
        End Get
    End Property


    ''' <summary>
    ''' Creates a new instance of DoubleFieldAttribute.
    ''' </summary>
    ''' <param name="valueValueRequired">Whether the property value is mandatory (not equals 0).</param>
    ''' <param name="valueAllowNegative">Whether the property value can be negative.</param>
    ''' <param name="valueRound">A round order of the property value.</param>
    ''' <param name="valueWithinRange">Wheather the property value should be within a provided range.</param>
    ''' <param name="valueMinValue">Minimum allowed property value.</param>
    ''' <param name="valueMaxValue">Maximum allowed property value.</param>
    ''' <param name="valueErrorIfExceedsRange">Wheather the property value exceeding the <see cref="MinValue">MinValue</see> 
    ''' or the <see cref="MaxValue">MaxValue</see> should be treated as error (not warning).</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal valueValueRequired As ValueRequiredLevel, ByVal valueAllowNegative As Boolean, _
        ByVal valueRound As Integer, Optional ByVal valueWithinRange As Boolean = False, _
        Optional ByVal valueMinValue As Double = 0, Optional ByVal valueMaxValue As Double = 1, _
        Optional ByVal valueErrorIfExceedsRange As Boolean = False)

        _ValueRequired = valueValueRequired
        _AllowNegative = valueAllowNegative
        _Round = valueRound
        _WithinRange = valueWithinRange
        _MinValue = valueMinValue
        _MaxValue = valueMaxValue
        _ErrorIfExceedsRange = valueErrorIfExceedsRange

    End Sub

End Class
