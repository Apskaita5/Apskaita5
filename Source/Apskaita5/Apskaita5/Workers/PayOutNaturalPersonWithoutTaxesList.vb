Imports ApskaitaObjects.My.Resources

Namespace Workers

    ''' <summary>
    ''' Represents a list of payments to natural persons when no taxes are deducted or payed by the company. 
    ''' Used in tax declarations.
    ''' </summary>
    ''' <remarks>Values are stored in the database table PayOutNaturalPersonWithoutTaxes.</remarks>
    <Serializable()> _
    Public NotInheritable Class PayOutNaturalPersonWithoutTaxesList
        Inherits BusinessListBase(Of PayOutNaturalPersonWithoutTaxesList, PayOutNaturalPersonWithoutTaxes)
        Implements IIsDirtyEnough, IValidationMessageProvider

#Region " Business Methods "

        Private _DateFrom As Date = Today
        Private _DateTo As Date = Today
        Private _PersonFilter As PersonInfo = Nothing


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
        ''' Gets a person that the list is filtered by.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property PersonFilter() As PersonInfo
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _PersonFilter
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
                For Each i As PayOutNaturalPersonWithoutTaxes In Me
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
            For Each i As PayOutNaturalPersonWithoutTaxes In Me
                If i.BrokenRulesCollection.WarningCount > 0 Then Return True
            Next
            Return False
        End Function


        ''' <summary>
        ''' Adds new payments to natural persons using the payment list provided (use 
        ''' <see cref="CreatePayOutNaturalPersonWithoutTaxesList">CreatePayOutNaturalPersonWithoutTaxesList</see>
        ''' method to create a new payment list for journal entries).
        ''' </summary>
        ''' <param name="range">a range of new payments to add to the list</param>
        ''' <remarks></remarks>
        Public Sub AddNewRange(ByVal range As PayOutNaturalPersonWithoutTaxesList)

            If range Is Nothing OrElse range.Count < 1 Then
                Throw New Exception(Workers_PayOutNaturalPersonWithoutTaxesList_RangeEmpty)
            End If

            Me.RaiseListChangedEvents = False
            For Each newItem As PayOutNaturalPersonWithoutTaxes In range
                Me.Add(newItem)
            Next
            Me.RaiseListChangedEvents = True
            Me.ResetBindings()

        End Sub


        Public Overrides Function Save() As PayOutNaturalPersonWithoutTaxesList
            Return MyBase.Save()
        End Function

#End Region

#Region " Authorization Rules "

        Public Shared Function CanGetObject() As Boolean
            Return ApplicationContext.User.IsInRole("Workers.PayOutNaturalPersonWithoutTaxesList1")
        End Function

        Public Shared Function CanAddObject() As Boolean
            Return ApplicationContext.User.IsInRole("Workers.PayOutNaturalPersonWithoutTaxesList2")
        End Function

        Public Shared Function CanEditObject() As Boolean
            Return ApplicationContext.User.IsInRole("Workers.PayOutNaturalPersonWithoutTaxesList3")
        End Function

        Public Shared Function CanDeleteObject() As Boolean
            Return ApplicationContext.User.IsInRole("Workers.PayOutNaturalPersonWithoutTaxesList3")
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets a new empty list of payments to natural persons.
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Function NewPayOutNaturalPersonWithoutTaxesList() As PayOutNaturalPersonWithoutTaxesList
            Return New PayOutNaturalPersonWithoutTaxesList
        End Function

        ''' <summary>
        ''' Creates a new list of payments to natural persons for the journal entries provided.
        ''' </summary>
        ''' <param name="journalEntriesIds">an array of <see cref="General.JournalEntry.ID">journal entries' id's</see>
        ''' to create payments for</param>
        ''' <remarks></remarks>
        Public Shared Function CreatePayOutNaturalPersonWithoutTaxesList(ByVal journalEntriesIds As Integer()) As PayOutNaturalPersonWithoutTaxesList
            Return DataPortal.Create(Of PayOutNaturalPersonWithoutTaxesList)(New Criteria(journalEntriesIds))
        End Function

        ''' <summary>
        ''' Gets existing payments to natural persons for a period requested.
        ''' </summary>
        ''' <param name="dateFrom">a starting date of the list period</param>
        ''' <param name="dateTo">an ending date of the list period</param>
        ''' <param name="personFilter">a person to filter the list by</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetPayOutNaturalPersonWithoutTaxesList(ByVal dateFrom As Date, _
            ByVal dateTo As Date, ByVal personFilter As PersonInfo) As PayOutNaturalPersonWithoutTaxesList
            Return DataPortal.Fetch(Of PayOutNaturalPersonWithoutTaxesList)(New Criteria(dateFrom, dateTo, personFilter))
        End Function


        Private Sub New()
            ' require use of factory methods
            Me.AllowEdit = True
            Me.AllowNew = False
            Me.AllowRemove = True
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> _
        Private Class Criteria
            Private _JournalEntriesIds As Integer() = Nothing
            Private _DateFrom As Date = Today
            Private _DateTo As Date = Today
            Private _PersonFilter As PersonInfo = Nothing
            Public ReadOnly Property JournalEntriesIds() As Integer()
                Get
                    Return _JournalEntriesIds
                End Get
            End Property
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
            Public ReadOnly Property PersonFilter() As PersonInfo
                Get
                    Return _PersonFilter
                End Get
            End Property
            Public Sub New(ByVal nJournalEntriesIds As Integer())
                _JournalEntriesIds = nJournalEntriesIds
            End Sub
            Public Sub New(ByVal nDateFrom As Date, ByVal nDateTo As Date, ByVal nPersonFilter As PersonInfo)
                _DateFrom = nDateFrom
                _DateTo = nDateTo
                _PersonFilter = nPersonFilter
            End Sub
        End Class


        Private Overloads Sub DataPortal_Create(ByVal criteria As Criteria)

            If Not CanAddObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecurityInsertDenied)

            If criteria.JournalEntriesIds Is Nothing OrElse criteria.JournalEntriesIds.Length < 1 Then
                Throw New Exception(Workers_PayOutNaturalPersonWithoutTaxesList_JournalEntriesIdsNull)
            End If

            For Each journalEntryId As Integer In criteria.JournalEntriesIds
                Add(PayOutNaturalPersonWithoutTaxes.NewPayOutNaturalPersonWithoutTaxes(journalEntryId))
            Next

        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)

            If Not CanGetObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecuritySelectDenied)

            Dim myComm As New SQLCommand("FetchPayOutNaturalPersonWithoutTaxesList")
            myComm.AddParam("?DF", criteria.DateFrom)
            myComm.AddParam("?DT", criteria.DateTo)
            If criteria.PersonFilter = PersonInfo.Empty Then
                myComm.AddParam("?PD", 0)
            Else
                myComm.AddParam("?PD", criteria.PersonFilter.ID)
            End If

            Using myData As DataTable = myComm.Fetch

                RaiseListChangedEvents = False

                For Each dr As DataRow In myData.Rows
                    Add(PayOutNaturalPersonWithoutTaxes.GetPayOutNaturalPersonWithoutTaxes(dr))
                Next

                _DateFrom = criteria.DateFrom
                _DateTo = criteria.DateTo
                _PersonFilter = criteria.PersonFilter

                RaiseListChangedEvents = True

            End Using

        End Sub

        Protected Overrides Sub DataPortal_Update()

            Dim potentialUpdatespending As Boolean = False

            If Not CanEditObject() Then
                For Each i As PayOutNaturalPersonWithoutTaxes In Me
                    If Not i.IsNew AndAlso i.IsDirty Then
                        If Not CanEditObject() Then Throw New System.Security.SecurityException( _
                            My.Resources.Common_SecurityUpdateDenied)
                    End If
                Next
                For Each i As PayOutNaturalPersonWithoutTaxes In Me.DeletedList
                    If Not i.IsNew Then
                        If Not CanEditObject() Then Throw New System.Security.SecurityException( _
                            My.Resources.Common_SecurityUpdateDenied)
                    End If
                Next
            End If

            For Each i As PayOutNaturalPersonWithoutTaxes In Me
                If i.IsNew OrElse i.IsDirty Then
                    potentialUpdatespending = True
                    Exit For
                End If
            Next
            If Not potentialUpdatespending Then
                For Each i As PayOutNaturalPersonWithoutTaxes In Me.DeletedList
                    If Not i.IsNew Then
                        potentialUpdatespending = True
                        Exit For
                    End If
                Next
            End If

            If Not potentialUpdatespending Then
                Throw New Exception(Workers_PayOutNaturalPersonWithoutTaxesList_ListIsNotDirty)
            End If

            If Not Me.IsValid Then
                Throw New Exception(String.Format(My.Resources.Common_ContainsErrors, vbCrLf, _
                    GetAllBrokenRules()))
            End If

            DatabaseAccess.TransactionBegin()

            RaiseListChangedEvents = False

            For Each item As PayOutNaturalPersonWithoutTaxes In DeletedList
                If Not item.IsNew Then item.DeleteSelf()
            Next
            DeletedList.Clear()

            For Each item As PayOutNaturalPersonWithoutTaxes In Me
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