Imports System.Reflection

Namespace Attributes

    ''' <summary>
    ''' An interface that needs to be implemented by a value object attribute
    ''' in order to provide a value object ID string that is used in value 
    ''' object filter to include obsolete items that are in use in 
    ''' a particular business object.
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface IValueObjectIdProvider

        ''' <summary>
        ''' Gets a value object ID string that is used in value object filter
        ''' to include obsolete items that are in use in a particular business
        ''' object.
        ''' </summary>
        ''' <param name="obj">a business object that holds a value object</param>
        ''' <param name="prop">a property that holds a value object</param>
        ''' <remarks></remarks>
        Function GetValueObjectId(ByVal obj As Object, ByVal prop As PropertyInfo) As String

        ''' <summary>
        ''' Gets a value object type in the property.
        ''' </summary>
        ''' <remarks></remarks>
        Function GetValueObjectType() As Type

    End Interface

End Namespace
