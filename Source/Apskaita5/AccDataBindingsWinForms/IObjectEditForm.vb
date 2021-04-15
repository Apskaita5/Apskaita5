''' <summary>
''' Used to desvribe a form as a business object edit form.
''' </summary>
''' <remarks></remarks>
Public Interface IObjectEditForm

    ''' <summary>
    ''' A type of the business object that the form can edit.
    ''' </summary>
    ''' <remarks></remarks>
    ReadOnly Property ObjectType() As Type

    ''' <summary>
    ''' An ID of the current business object in the form.
    ''' </summary>
    ''' <remarks>Should return Integer.MinValue if the form does not contain
    ''' any business object or the business object is new.</remarks>
    ReadOnly Property ObjectID() As Integer

End Interface
