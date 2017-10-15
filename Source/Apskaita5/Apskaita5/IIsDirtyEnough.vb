''' <summary>
''' An interface used to implement "save object before closing" pattern in GUI.
''' </summary>
''' <remarks><see cref="Csla.BusinessBase.IsDirty">IsDirty</see> property is not sufficient 
''' because one wouldn't want a "save object before closing" message when the object is new 
''' but contains no data.
''' Only ment for GUI, not used internaly.</remarks>
Public Interface IIsDirtyEnough

    ''' <summary>
    ''' Whether the object is new and also contains some data 
    ''' or 
    ''' the object is not new and is dirty (has been changed).
    ''' </summary>
    ''' <remarks></remarks>
    ReadOnly Property IsDirtyEnough() As Boolean

End Interface
