''' <summary>
''' Represents a business object that is meant for use by some person, 
''' i.e. the person has a right to access the document.
''' </summary>
''' <remarks>Used by <see cref="BusinessObjectCollection(Of T)">BusinessObjectCollection(Of T)</see>
''' in order to provide mailing functionality.</remarks>
Public Interface IClientEmailProvider

    ''' <summary>
    ''' Gets an email of the client (person) that has a right to access the document.
    ''' </summary>
    ''' <remarks></remarks>
    Function GetClientEmail() As String

    ''' <summary>
    ''' Gets a name of the client (person) that has a right to access the document.
    ''' </summary>
    ''' <remarks></remarks>
    Function GetClientName() As String

End Interface
