Namespace Extensibility

    Public Module Extensibility

        ''' <summary>
        ''' Creates a new BookEntryInternal instance.
        ''' </summary>
        ''' <param name="entryType">Type of the general ledger transaction.</param>
        ''' <remarks></remarks>
        Public Function NewBookEntryInternal(ByVal entryType As BookEntryType) As BookEntryInternal
            Return BookEntryInternal.NewBookEntryInternal(entryType)
        End Function

        ''' <summary>
        ''' Creates a new BookEntryInternal instance without setting a <see cref="BookEntryInternal.Person">person</see>.
        ''' </summary>
        ''' <param name="entryType">Type of the general ledger transaction.</param>
        ''' <param name="account">An Account that is affected by the general ledger transaction.</param>
        ''' <param name="amount">An amount of the general ledger transaction.</param>
        ''' <remarks></remarks>
        Public Function NewBookEntryInternal(ByVal entryType As BookEntryType, _
            ByVal account As Long, ByVal amount As Double) As BookEntryInternal
            Return BookEntryInternal.NewBookEntryInternal(entryType, account, amount)
        End Function

        ''' <summary>
        ''' Creates a new BookEntryInternal instance.
        ''' </summary>
        ''' <param name="entryType">Type of the general ledger transaction.</param>
        ''' <param name="account">An Account that is affected by the general ledger transaction.</param>
        ''' <param name="amount">An amount of the general ledger transaction.</param>
        ''' <param name="person">A person that is associated with the general ledger transaction.</param>
        ''' <remarks></remarks>
        Public Function NewBookEntryInternal(ByVal entryType As BookEntryType, _
            ByVal account As Long, ByVal amount As Double, ByVal person As PersonInfo) As BookEntryInternal
            Return BookEntryInternal.NewBookEntryInternal(entryType, account, amount, person)
        End Function

        ''' <summary>
        ''' Creates a new BookEntryInternal instance.
        ''' </summary>
        ''' <param name="entryType">Type of the general ledger transaction.</param>
        ''' <param name="bookEntry">An instance of <see cref="ApskaitaObjects.General.BookEntry">BookEntry</see> 
        ''' that is used to initialize the values of the new BookEntryInternal instance.</param>
        ''' <remarks></remarks>
        Public Function NewBookEntryInternal(ByVal entryType As BookEntryType, _
            ByVal bookEntry As ApskaitaObjects.General.BookEntry) As BookEntryInternal
            Return BookEntryInternal.NewBookEntryInternal(entryType, bookEntry)
        End Function

        ''' <summary>
        ''' Gets a new instance of BookEntryInternalList.
        ''' </summary>
        ''' <param name="defaultBookEntryType">A default <see cref="BookEntryType">BookEntryType</see> value to use when adding new items.</param>
        ''' <remarks></remarks>
        Public Function NewBookEntryInternalList(ByVal defaultBookEntryType As BookEntryType) As BookEntryInternalList
            Return BookEntryInternalList.NewBookEntryInternalList(defaultBookEntryType)
        End Function

    End Module

End Namespace
