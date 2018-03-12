''' <summary>
''' Indicates that a particular type of business object has a builtin functionality
''' for syncing with external systems (CRM, etc.)
''' </summary>
''' <remarks></remarks>
Public Interface ISyncable

    ''' <summary>
    ''' an ID of the business object in an external system
    ''' </summary>
    ''' <remarks></remarks>
    ReadOnly Property ExternalID() As String

End Interface
