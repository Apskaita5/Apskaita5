Namespace General

    <Serializable()> _
    Public Class TemplateBookEntryList
        Inherits BusinessListBase(Of TemplateBookEntryList, TemplateBookEntry)

#Region " Business Methods "

        Private _Type As BookEntryType = BookEntryType.Debetas

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
            Dim result As String = GetAllBrokenRulesForList(Me)

            'Dim GeneralErrorString As String = ""
            'SomeGeneralValidationSub(GeneralErrorString)
            'AddWithNewLine(result, GeneralErrorString, False)

            Return result
        End Function

        Public Function GetAllWarnings() As String
            Dim result As String = GetAllWarningsForList(Me)
            'Dim GeneralErrorString As String = ""
            'SomeGeneralValidationSub(GeneralErrorString)
            'AddWithNewLine(result, GeneralErrorString, False)

            Return result
        End Function

#End Region

#Region " Factory Methods "

        ' used to implement automatic sort in datagridview
        <NonSerialized()> _
        Private _SortedList As Csla.SortedBindingList(Of TemplateBookEntry) = Nothing

        Friend Shared Function NewTemplateBookEntryList(ByVal nType As BookEntryType) As TemplateBookEntryList
            Dim result As TemplateBookEntryList = New TemplateBookEntryList
            result._Type = nType
            Return result
        End Function

        Friend Shared Function GetTemplateBookEntryList(ByVal myData As DataTable, _
            ByVal nType As BookEntryType) As TemplateBookEntryList
            Dim result As TemplateBookEntryList = New TemplateBookEntryList(myData, nType)
            Return result
        End Function

        Public Function GetSortedList() As Csla.SortedBindingList(Of TemplateBookEntry)
            If _SortedList Is Nothing Then _SortedList = New Csla.SortedBindingList(Of TemplateBookEntry)(Me)
            Return _SortedList
        End Function

        Private Sub New()
            ' require use of factory methods
            MarkAsChild()
            Me.AllowEdit = True
            Me.AllowNew = True
            Me.AllowRemove = True
        End Sub

        Private Sub New(ByVal myData As DataTable, ByVal nType As BookEntryType)
            ' require use of factory methods
            MarkAsChild()
            Me.AllowEdit = True
            Me.AllowNew = True
            Me.AllowRemove = True
            Fetch(myData, nType)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal myData As DataTable, ByVal nType As BookEntryType)

            RaiseListChangedEvents = False

            For Each dr As DataRow In myData.Rows
                If ConvertEnumDatabaseStringCode(Of BookEntryType)(CStrSafe(dr.Item(1))) = nType Then _
                    Add(TemplateBookEntry.GetTemplateBookEntry(dr))
            Next

            _Type = nType

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