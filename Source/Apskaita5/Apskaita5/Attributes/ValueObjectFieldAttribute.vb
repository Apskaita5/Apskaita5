Namespace Attributes

    ''' <summary>
    ''' Represents an attribute that is used to mark business objects' properties holding a reference
    ''' to a value objects, e.g. <see cref="HelperLists.PersonInfo">PersonInfo</see>, and  to store 
    ''' basic business rules (mandatory, allowed values, etc.).
    ''' </summary>
    ''' <remarks>Used for validation purposes in <see cref="ValueObjectFieldValidation">ValueObjectFieldValidation</see> method.
    ''' Could be used by GUI to initialize appropriate controls.</remarks>
    <Serializable()> _
    <AttributeUsage(AttributeTargets.Property, AllowMultiple:=False, Inherited:=True)> _
    Public MustInherit Class ValueObjectFieldAttribute
        Inherits BusinessFieldAttribute

        Private _ValueRequired As ValueRequiredLevel = ValueRequiredLevel.Optional

        ''' <summary>
        ''' Whether the property value is mandatory.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property ValueRequired() As ValueRequiredLevel
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ValueRequired
            End Get
        End Property


        ''' <summary>
        ''' Creates a new instance of an ValueObjectFieldAttribute class.
        ''' </summary>
        ''' <param name="valueValueRequired">Whether the property value is mandatory.</param>
        ''' <remarks></remarks>
        Protected Sub New(ByVal valueValueRequired As ValueRequiredLevel)

            _ValueRequired = valueValueRequired

        End Sub

    End Class

End Namespace
