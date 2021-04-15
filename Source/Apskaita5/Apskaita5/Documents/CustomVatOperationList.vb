Imports ApskaitaObjects.My.Resources

Namespace Documents

    ''' <summary>
    ''' Represents a list of custom VAT operations (not invoice based operations that affect VAT obligations).
    ''' </summary>
    ''' <remarks>Values are stored in the database table CustomVatOperations.</remarks>
    <Serializable()> _
    Public Class CustomVatOperationList
        Inherits BusinessListBase(Of CustomVatOperationList, CustomVatOperation)
        Implements IIsDirtyEnough, IValidationMessageProvider

#Region " Business Methods "

        Private _DateFrom As Date = Today
        Private _DateTo As Date = Today
        Private _ByJournalEntry As Boolean = False


        ''' <summary>
        ''' Gets a starting date of the list period.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DateFrom() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DateFrom
            End Get
        End Property

        ''' <summary>
        ''' Gets an ending date of the list period.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DateTo() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DateTo
            End Get
        End Property

        ''' <summary>
        ''' Gets whether the list period is applied to the associated journal entry dates (not operation dates).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property ByJournalEntry() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ByJournalEntry
            End Get
        End Property

        Public Overrides ReadOnly Property IsValid() As Boolean _
            Implements IValidationMessageProvider.IsValid
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return MyBase.IsValid
            End Get
        End Property

        Public ReadOnly Property IsDirtyEnough() As Boolean _
            Implements IIsDirtyEnough.IsDirtyEnough
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                For Each i As CustomVatOperation In Me
                    If i.IsNew OrElse i.IsDirty Then Return True
                Next
                Return False
            End Get
        End Property


        Public Function GetAllBrokenRules() As String _
            Implements IValidationMessageProvider.GetAllBrokenRules
            Dim result As String = GetAllBrokenRulesForList(Me)

            'Dim GeneralErrorString As String = ""
            'SomeGeneralValidationSub(GeneralErrorString)
            'AddWithNewLine(result, GeneralErrorString, False)

            Return result
        End Function

        Public Function GetAllWarnings() As String _
            Implements IValidationMessageProvider.GetAllWarnings
            Dim result As String = GetAllWarningsForList(Me)
            'Dim GeneralErrorString As String = ""
            'SomeGeneralValidationSub(GeneralErrorString)
            'AddWithNewLine(result, GeneralErrorString, False)

            Return result
        End Function

        Public Function HasWarnings() As Boolean _
            Implements IValidationMessageProvider.HasWarnings
            For Each i As CustomVatOperation In Me
                If i.BrokenRulesCollection.WarningCount > 0 Then Return True
            Next
            Return False
        End Function


        Protected Overrides Function AddNewCore() As Object
            Dim newItem As CustomVatOperation = CustomVatOperation.NewCustomVatOperation
            Me.Add(newItem)
            Return newItem
        End Function

        ''' <summary>
        ''' Adds new operations to the list (use <see cref="NewCustomVatOperationList">NewCustomVatOperationList</see>
        ''' method to create a new operation list for journal entries).
        ''' </summary>
        ''' <param name="range">a range of new operations to add to the list</param>
        ''' <remarks></remarks>
        Public Sub AddNewRange(ByVal range As CustomVatOperationList)

            If range Is Nothing OrElse range.Count < 1 Then
                Throw New Exception(Documents_CustomVatOperationList_RangeNull)
            End If

            Me.RaiseListChangedEvents = False
            For Each newItem As CustomVatOperation In range
                Me.Add(newItem)
            Next
            Me.RaiseListChangedEvents = True
            Me.ResetBindings()

        End Sub


        Public Overrides Function Save() As CustomVatOperationList
            Return MyBase.Save()
        End Function

#End Region

#Region " Authorization Rules "

        Public Shared Function CanGetObject() As Boolean
            Return ApplicationContext.User.IsInRole("Documents.CustomVatOperationList1")
        End Function

        Public Shared Function CanAddObject() As Boolean
            Return ApplicationContext.User.IsInRole("Documents.CustomVatOperationList2")
        End Function

        Public Shared Function CanEditObject() As Boolean
            Return ApplicationContext.User.IsInRole("Documents.CustomVatOperationList3")
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets a new empty custom VAT operations list.
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Function NewCustomVatOperationList() As CustomVatOperationList
            Return New CustomVatOperationList
        End Function

        ''' <summary>
        ''' Gets a new custom VAT operations list for the journal entries requested.
        ''' </summary>
        ''' <param name="journalEntriesIds"><see cref="General.JournalEntry.ID">ID's of the journal entries</see>
        ''' to create the VAT operations for</param>
        ''' <remarks></remarks>
        Public Shared Function NewCustomVatOperationList(ByVal journalEntriesIds As Integer()) As CustomVatOperationList
            Return DataPortal.Create(Of CustomVatOperationList)(New Criteria(journalEntriesIds))
        End Function

        ''' <summary>
        ''' Gets a list of custom VAT operations for the specified period.
        ''' </summary>
        ''' <param name="dateFrom">a starting date of the list period</param>
        ''' <param name="dateTo">an ending date of the list period</param>
        ''' <param name="byJournalEntry">whether the list period is applied to 
        ''' the associated journal entry dates (not operation dates)</param>
        ''' <remarks></remarks>
        Public Shared Function GetCustomVatOperationList(ByVal dateFrom As Date, _
            ByVal dateTo As Date, ByVal byJournalEntry As Boolean) As CustomVatOperationList
            Return DataPortal.Fetch(Of CustomVatOperationList)(New Criteria(dateFrom, dateTo, byJournalEntry))
        End Function

        Private Sub New()
            ' require use of factory methods
            Me.AllowEdit = True
            Me.AllowNew = True
            Me.AllowRemove = True
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> _
        Private Class Criteria
            Private _DateFrom As Date = Today
            Private _DateTo As Date = Today
            Private _ByJournalEntry As Boolean = False
            Private _JournalEntriesIds As Integer() = Nothing
            Public ReadOnly Property DateFrom() As Date
                Get
                    Return _DateFrom
                End Get
            End Property
            Public ReadOnly Property DateTo() As Date
                Get
                    Return _DateTo
                End Get
            End Property
            Public ReadOnly Property ByJournalEntry() As Boolean
                Get
                    Return _ByJournalEntry
                End Get
            End Property
            Public ReadOnly Property JournalEntriesIds() As Integer()
                Get
                    Return _JournalEntriesIds
                End Get
            End Property
            Public Sub New(ByVal nDateFrom As Date, ByVal nDateTo As Date, ByVal nByJournalEntry As Boolean)
                _DateFrom = nDateFrom
                _DateTo = nDateTo
                _ByJournalEntry = nByJournalEntry
            End Sub
            Public Sub New(ByVal nJournalEntriesIds As Integer())
                _JournalEntriesIds = nJournalEntriesIds
            End Sub
        End Class

        Private Overloads Sub DataPortal_Create(ByVal criteria As Criteria)

            If Not CanAddObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecurityInsertDenied)

            If criteria.JournalEntriesIds Is Nothing OrElse criteria.JournalEntriesIds.Length < 1 Then
                Throw New Exception(Documents_CustomVatOperationList_JournalEntriesIdsNull)
            End If

            For Each journalEntryId As Integer In criteria.JournalEntriesIds
                Add(CustomVatOperation.NewCustomVatOperation(journalEntryId))
            Next

        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)

            If Not CanGetObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecuritySelectDenied)

            Dim myComm As SQLCommand
            If criteria.ByJournalEntry Then
                myComm = New SQLCommand("FetchCustomVatOperationListByJournalEntry")
            Else
                myComm = New SQLCommand("FetchCustomVatOperationList")
            End If
            myComm.AddParam("?DF", criteria.DateFrom)
            myComm.AddParam("?DT", criteria.DateTo)

            Using myData As DataTable = myComm.Fetch

                RaiseListChangedEvents = False

                For Each dr As DataRow In myData.Rows
                    Add(CustomVatOperation.GetCustomVatOperation(dr))
                Next

                _DateFrom = criteria.DateFrom
                _DateTo = criteria.DateTo
                _ByJournalEntry = criteria.ByJournalEntry

                RaiseListChangedEvents = True

            End Using

        End Sub

        Protected Overrides Sub DataPortal_Update()

            Dim potentialUpdatespending As Boolean = False

            If Not CanEditObject() Then
                For Each i As CustomVatOperation In Me
                    If Not i.IsNew AndAlso i.IsDirty Then
                        If Not CanEditObject() Then Throw New System.Security.SecurityException( _
                            My.Resources.Common_SecurityUpdateDenied)
                    End If
                Next
                For Each i As CustomVatOperation In Me.DeletedList
                    If Not i.IsNew Then
                        If Not CanEditObject() Then Throw New System.Security.SecurityException( _
                            My.Resources.Common_SecurityUpdateDenied)
                    End If
                Next
            End If

            For Each i As CustomVatOperation In Me
                If i.IsNew OrElse i.IsDirty Then
                    potentialUpdatespending = True
                    Exit For
                End If
            Next
            If Not potentialUpdatespending Then
                For Each i As CustomVatOperation In Me.DeletedList
                    If Not i.IsNew Then
                        potentialUpdatespending = True
                        Exit For
                    End If
                Next
            End If

            If Not potentialUpdatespending Then
                Throw New Exception(Documents_CustomVatOperationList_ListIsNotDirty)
            End If

            If Not Me.IsValid Then
                Throw New Exception(String.Format(My.Resources.Common_ContainsErrors, vbCrLf, _
                    GetAllBrokenRules()))
            End If

            DatabaseAccess.TransactionBegin()

            RaiseListChangedEvents = False

            For Each item As CustomVatOperation In DeletedList
                If Not item.IsNew Then item.DeleteSelf()
            Next
            DeletedList.Clear()

            For Each item As CustomVatOperation In Me
                If item.IsNew Then
                    item.Insert()
                ElseIf item.IsDirty Then
                    item.Update()
                End If
            Next

            RaiseListChangedEvents = True

            DatabaseAccess.TransactionCommit()

        End Sub

#End Region

    End Class

End Namespace
