''' <summary>
''' Represents a helper class that is used by various parent objects to create <see cref="General.BookEntry">BookEntry</see> lists.
''' </summary>
''' <remarks></remarks>
<Serializable()> _
Friend Class BookEntryInternalList
    Inherits BusinessListBase(Of BookEntryInternalList, BookEntryInternal)

#Region " Business Methods "

    Private _DefaultBookEntryType As BookEntryType

    ''' <summary>
    ''' Gets a default <see cref="BookEntryType">BookEntryType</see> value to use when adding new items.
    ''' </summary>
    ''' <remarks></remarks>
    Public ReadOnly Property DefaultBookEntryType() As BookEntryType
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            Return _DefaultBookEntryType
        End Get
    End Property


    Protected Overrides Function AddNewCore() As Object
        Dim NewItem As BookEntryInternal = BookEntryInternal.NewBookEntryInternal(BookEntryType.Debetas)
        Me.Add(NewItem)
        Return NewItem
    End Function


    ''' <summary>
    ''' Adds a new BookEntryInternal instance to the list.
    ''' </summary>
    ''' <param name="nEntryType">Type of the general ledger transaction.</param>
    ''' <remarks></remarks>
    Public Overloads Sub AddNew(ByVal nEntryType As BookEntryType)
        Me.Add(BookEntryInternal.NewBookEntryInternal(nEntryType))
    End Sub

    ''' <summary>
    ''' Adds a new BookEntryInternal instance to the list.
    ''' </summary>
    ''' <param name="nEntryType">Type of the general ledger transaction.</param>
    ''' <param name="nAccount">An Account that is affected by the general ledger transaction.</param>
    ''' <param name="nAmmount">An amount of the general ledger transaction.</param>
    ''' <param name="nPerson">A person that is associated with the general ledger transaction.</param>
    ''' <remarks></remarks>
    Public Overloads Sub AddNew(ByVal nEntryType As BookEntryType, _
        ByVal nAccount As Long, ByVal nAmmount As Double, ByVal nPerson As PersonInfo)
        Me.Add(BookEntryInternal.NewBookEntryInternal(nEntryType, nAccount, nAmmount, nPerson))
    End Sub

    ''' <summary>
    ''' Adds a new BookEntryInternal instance to the list.
    ''' </summary>
    ''' <param name="nEntryType">Type of the general ledger transaction.</param>
    ''' <param name="nBookEntry">An instance of <see cref="General.BookEntry">BookEntry</see> 
    ''' that is used to initialize the values of the new BookEntryInternal instance.</param>
    ''' <remarks></remarks>
    Public Overloads Sub AddNew(ByVal nEntryType As BookEntryType, _
        ByVal nBookEntry As General.BookEntry)
        Me.Add(BookEntryInternal.NewBookEntryInternal(nEntryType, nBookEntry))
    End Sub

    ''' <summary>
    ''' Adds all the BookEntryInternal values in <paramref name="list">list</paramref> to the current list.
    ''' </summary>
    ''' <param name="list">A list of BookEntryInternal values to be added.</param>
    ''' <remarks></remarks>
    Public Sub AddRange(ByVal list As BookEntryInternalList)
        For Each e As BookEntryInternal In list
            Add(e)
        Next
    End Sub

    ''' <summary>
    ''' Adds all the BookEntryInternal values in <paramref name="list">nBookEntryLists</paramref> to the current list.
    ''' </summary>
    ''' <param name="nBookEntryLists">A list of lists of BookEntryInternal values to be added.</param>
    ''' <remarks></remarks>
    Public Sub AddBookEntryLists(ByVal ParamArray nBookEntryLists As General.BookEntryList())

        If nBookEntryLists Is Nothing Then Exit Sub

        Me.RaiseListChangedEvents = False

        For Each l As General.BookEntryList In nBookEntryLists
            For Each i As General.BookEntry In l
                Add(BookEntryInternal.NewBookEntryInternal(l.Type, i))
            Next
        Next

        Me.RaiseListChangedEvents = True

    End Sub

    ''' <summary>
    ''' Does a list aggregation <see cref="BookEntryInternal.AddBookEntryAmmount">suming</see> 
    ''' over the BookEntryInternal values grouped by <see cref="BookEntryInternal.Account">Account</see>
    ''' <see cref="BookEntryInternal.Person">Person</see>.
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub Aggregate()

        Dim i, j As Integer

        For i = Me.Count To 2 Step -1

            For j = 1 To i - 1
                If Item(i - 1) = Item(j - 1) Then
                    Item(j - 1).AddBookEntryAmmount(Item(i - 1))
                    RemoveAt(i - 1)
                    Exit For
                End If
            Next

        Next

        For i = Me.Count To 1 Step -1
            If Item(i - 1).Ammount = 0 Then RemoveAt(i - 1)
        Next

    End Sub

#End Region

#Region " Factory Methods "

    ''' <summary>
    ''' Gets a new instance of BookEntryInternalList.
    ''' </summary>
    ''' <param name="nDefaultBookEntryType">A default <see cref="BookEntryType">BookEntryType</see> value to use when adding new items.</param>
    ''' <remarks></remarks>
    Friend Shared Function NewBookEntryInternalList(ByVal nDefaultBookEntryType As BookEntryType) As BookEntryInternalList
        Dim result As BookEntryInternalList = New BookEntryInternalList
        result._DefaultBookEntryType = nDefaultBookEntryType
        Return result
    End Function

    Private Sub New()
        ' require use of factory methods
        MarkAsChild()
        Me.AllowEdit = True
        Me.AllowNew = True
        Me.AllowRemove = True
    End Sub

#End Region

End Class