Namespace Documents

    ''' <summary>
    ''' Provides a common interface to work with regionalizable (localizable) objects,
    ''' that contain <see cref="RegionalContentList">RegionalContentList</see> and <see cref="RegionalPriceList">RegionalPriceList</see>.
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface IRegionalDataObject

        ''' <summary>
        ''' Gets an ID of the regionalizable object.
        ''' </summary>
        ''' <remarks>Value is stored in the database field regionalcontents.ParentID or regionalprices.ParentID.</remarks>
        ReadOnly Property RegionalObjectID() As Integer

        ''' <summary>
        ''' Gets a type of the regionalizable object.
        ''' </summary>
        ''' <remarks>Value is stored in the database field regionalcontents.ParentType or regionalprices.ParentType.</remarks>
        ReadOnly Property RegionalObjectType() As RegionalizedObjectType

    End Interface

End Namespace
