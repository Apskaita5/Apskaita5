Namespace HelperLists

    ''' <summary>
    ''' Allows to query value objects if the current instance is merely a place holder.
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface IValueObjectIsEmpty

        ''' <summary>
        ''' Whether the value object is merely a place holder.
        ''' </summary>
        ''' <remarks></remarks>
        ReadOnly Property IsEmpty() As Boolean

    End Interface

End Namespace