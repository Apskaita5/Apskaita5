Namespace General

    ''' <summary>
    ''' Represents a ledger transaction template list grouped by <see cref="General.BookEntryList.Type">transaction type</see>.
    ''' </summary>
    ''' <remarks>Can only be used as a child object for <see cref="General.TemplateJournalEntry">TemplateJournalEntry</see>.
    ''' Values are stored in the database table tipines_data.</remarks>
    <Serializable()> _
    Public Class TemplateBookEntryList
        Inherits BusinessListBase(Of TemplateBookEntryList, TemplateBookEntry)

#Region " Business Methods "

        Private _Type As BookEntryType = BookEntryType.Debetas


        ''' <summary>
        ''' <see cref="BookEntryType">Type</see> (credit/debit) of the transaction templates in the list.
        ''' </summary>
        Public ReadOnly Property [Type]() As BookEntryType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Type
            End Get
        End Property


        Protected Overrides Function AddNewCore() As Object
            Dim NewItem As TemplateBookEntry = TemplateBookEntry.NewTemplateBookEntry
            Me.Add(NewItem)
            Return NewItem
        End Function


        Public Function GetAllBrokenRules() As String
            If Me.IsValid Then Return ""
            Return GetAllBrokenRulesForList(Me)
        End Function

        Public Function GetAllWarnings() As String
            If Not HasWarnings() Then Return ""
            Return GetAllWarningsForList(Me)
        End Function

        Public Function HasWarnings() As Boolean
            For Each i As TemplateBookEntry In Me
                If i.BrokenRulesCollection.WarningCount > 0 Then Return True
            Next
            Return False
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets as new TemplateBookEntryList of type <paramref name=" EntryType">EntryType</paramref>.
        ''' </summary>
        ''' <param name="entryType"><see cref="BookEntryType">Type</see> of the transaction templates in the list.</param>
        Friend Shared Function NewTemplateBookEntryList(ByVal entryType As BookEntryType) As TemplateBookEntryList
            Return New TemplateBookEntryList(entryType)
        End Function

        ''' <summary>
        ''' Gets a BookEntryList from database.
        ''' </summary>
        ''' <param name="myData">DataTable containing transaction data.</param>
        ''' <param name="entryType"><see cref="BookEntryType">Type</see> of the transaction templates in the list.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Friend Shared Function GetTemplateBookEntryList(ByVal myData As DataTable, _
            ByVal entryType As BookEntryType) As TemplateBookEntryList
            Return New TemplateBookEntryList(myData, entryType)
        End Function


        Private Sub New()
            ' require use of factory methods
            MarkAsChild()
            Me.AllowEdit = True
            Me.AllowNew = True
            Me.AllowRemove = True
        End Sub

        Private Sub New(ByVal entryType As BookEntryType)
            ' require use of factory methods
            MarkAsChild()
            Me.AllowEdit = True
            Me.AllowNew = True
            Me.AllowRemove = True
            Create(entryType)
        End Sub

        Private Sub New(ByVal myData As DataTable, ByVal entryType As BookEntryType)
            ' require use of factory methods
            MarkAsChild()
            Me.AllowEdit = True
            Me.AllowNew = True
            Me.AllowRemove = True
            Fetch(myData, entryType)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Create(ByVal entryType As BookEntryType)
            _Type = entryType
        End Sub

        Private Sub Fetch(ByVal myData As DataTable, ByVal entryType As BookEntryType)

            RaiseListChangedEvents = False

            For Each dr As DataRow In myData.Rows
                If Utilities.ConvertDatabaseCharID(Of BookEntryType)(CStrSafe(dr.Item(1))) = entryType Then _
                    Add(TemplateBookEntry.GetTemplateBookEntry(dr))
            Next

            _Type = entryType

            RaiseListChangedEvents = True

        End Sub

        Friend Sub Update(ByVal parent As TemplateJournalEntry)

            RaiseListChangedEvents = False

            For Each item As TemplateBookEntry In DeletedList
                If Not item.IsNew Then item.DeleteSelf()
            Next
            DeletedList.Clear()

            For Each item As TemplateBookEntry In Me
                If item.IsNew Then
                    item.Insert(Me, parent)
                ElseIf item.IsDirty Then
                    item.Update(Me, parent)
                End If
            Next

            RaiseListChangedEvents = True

        End Sub

#End Region

    End Class

End Namespace