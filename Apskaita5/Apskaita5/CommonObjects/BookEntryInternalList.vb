<Serializable()> _
Friend Class BookEntryInternalList
    Inherits BusinessListBase(Of BookEntryInternalList, BookEntryInternal)

#Region " Business Methods "

    Private _DefaultBookEntryType As BookEntryType

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

    Public Sub AddRange(ByVal list As BookEntryInternalList)
        For Each e As BookEntryInternal In list
            Add(e)
        Next
    End Sub

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

        ReAssignTypes()

    End Sub

    Public Sub ReAssignTypes()
        For Each i As BookEntryInternal In Me
            i.ReAssignType()
        Next
    End Sub

#End Region

#Region " Factory Methods "

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