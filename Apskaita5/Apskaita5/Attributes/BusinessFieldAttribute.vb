Namespace Attributes

    ''' <summary>
    ''' Represents a base attribute class that all the attributes 
    ''' for business fields (e.g. <see cref="IntegerFieldAttribute">IntegerFieldAttribute</see>) 
    ''' should inherit from. Used to make difference between business field
    ''' attributed and the rest of the custom attributes.
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    <AttributeUsage(AttributeTargets.Property, AllowMultiple:=False, Inherited:=True)> _
    Public MustInherit Class BusinessFieldAttribute
        Inherits Attribute

    End Class

End Namespace