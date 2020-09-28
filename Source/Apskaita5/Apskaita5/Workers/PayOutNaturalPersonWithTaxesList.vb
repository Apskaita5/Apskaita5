Imports ApskaitaObjects.ActiveReports
Imports ApskaitaObjects.My.Resources

Namespace Workers

    ''' <summary>
    ''' Represents a list of payments to natural persons when when some taxes are deducted or payed by the company. 
    ''' Used in tax declarations.
    ''' </summary>
    ''' <remarks>Provides additional info on top of a <see cref="General.JournalEntry">JournalEntry</see>.
    ''' Values are stored in the database table d_kitos.</remarks>
    <Serializable()>
    Public NotInheritable Class PayOutNaturalPersonWithTaxesList
        Inherits BusinessListBase(Of PayOutNaturalPersonWithTaxesList, PayOutNaturalPersonWithTaxes)
        Implements IIsDirtyEnough, IValidationMessageProvider

#Region " Business Methods "

        Private Shared ReadOnly AllowedDocumentTypes As DocumentType() = New DocumentType() _
            {DocumentType.BankOperation, DocumentType.None, DocumentType.Offset, DocumentType.TillSpendingOrder}

        Private _DateFrom As Date = Today
        Private _DateTo As Date = Today
        Private _PersonFilter As PersonInfo = Nothing


        ''' <summary>
        ''' Gets a starting date of the list period.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DateFrom() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _DateFrom
            End Get
        End Property

        ''' <summary>
        ''' Gets an ending date of the list period.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DateTo() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _DateTo
            End Get
        End Property

        ''' <summary>
        ''' Gets a person that the list is filtered by.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property PersonFilter() As PersonInfo
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _PersonFilter
            End Get
        End Property


        Public Overrides ReadOnly Property IsValid() As Boolean _
            Implements IValidationMessageProvider.IsValid
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return MyBase.IsValid
            End Get
        End Property

        Public ReadOnly Property IsDirtyEnough() As Boolean _
            Implements IIsDirtyEnough.IsDirtyEnough
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                For Each i As PayOutNaturalPersonWithTaxes In Me
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
            For Each i As PayOutNaturalPersonWithTaxes In Me
                If i.BrokenRulesCollection.WarningCount > 0 Then Return True
            Next
            Return False
        End Function


        ''' <summary>
        ''' Adds new payments to natural persons using the payment list provided (use 
        ''' <see cref="CreatePayOutNaturalPersonWithTaxesList">CreatePayOutNaturalPersonWithTaxesList</see>
        ''' method to create a new payment list for journal entries).
        ''' </summary>
        ''' <param name="range">a range of new payments to add to the list</param>
        ''' <remarks></remarks>
        Public Sub AddNewRange(ByVal range As PayOutNaturalPersonWithTaxesList)
            If range Is Nothing OrElse range.Count < 1 Then
                Throw New Exception(My.Resources.Workers_PayOutNaturalPersonWithTaxesList_RangeEmpty)
            End If

            Me.RaiseListChangedEvents = False
            For Each newItem As PayOutNaturalPersonWithTaxes In range
                Me.Add(newItem)
            Next
            Me.RaiseListChangedEvents = True
            Me.ResetBindings()
        End Sub


        Public Overrides Function Save() As PayOutNaturalPersonWithTaxesList
            Return MyBase.Save()
        End Function

#End Region

#Region " Authorization Rules "

        Public Shared Function CanGetObject() As Boolean
            Return ApplicationContext.User.IsInRole("Workers.PayOutNaturalPerson1")
        End Function

        Public Shared Function CanAddObject() As Boolean
            Return ApplicationContext.User.IsInRole("Workers.PayOutNaturalPerson2")
        End Function

        Public Shared Function CanEditObject() As Boolean
            Return ApplicationContext.User.IsInRole("Workers.PayOutNaturalPerson3")
        End Function

        Public Shared Function CanDeleteObject() As Boolean
            Return ApplicationContext.User.IsInRole("Workers.PayOutNaturalPerson3")
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets a new empty list of payments to natural persons.
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Function NewPayOutNaturalPersonWithTaxesList() As PayOutNaturalPersonWithTaxesList
            Return New PayOutNaturalPersonWithTaxesList
        End Function

        ''' <summary>
        ''' Creates a new list of payments to natural persons for the journal entries provided.
        ''' </summary>
        ''' <param name="journalEntries">an array of <see cref="JournalEntryInfo">journal entries</see>
        ''' to create payments for</param>
        ''' <remarks></remarks>
        Public Shared Function CreatePayOutNaturalPersonWithTaxesList(ByVal journalEntries As JournalEntryInfo()) As PayOutNaturalPersonWithTaxesList
            Return DataPortal.Create(Of PayOutNaturalPersonWithTaxesList)(New Criteria(journalEntries))
        End Function

        ''' <summary>
        ''' Gets existing payments to natural persons for a period requested.
        ''' </summary>
        ''' <param name="dateFrom">a starting date of the list period</param>
        ''' <param name="dateTo">an ending date of the list period</param>
        ''' <param name="personFilter">a person to filter the list by</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Shared Function GetPayOutNaturalPersonWithTaxesList(ByVal dateFrom As Date,
            ByVal dateTo As Date, ByVal personFilter As PersonInfo) As PayOutNaturalPersonWithTaxesList
            Return DataPortal.Fetch(Of PayOutNaturalPersonWithTaxesList)(New Criteria(dateFrom, dateTo, personFilter))
        End Function


        Private Sub New()
            ' require use of factory methods
            Me.AllowEdit = True
            Me.AllowNew = False
            Me.AllowRemove = True
        End Sub

#End Region

#Region " Data Access "

        <Serializable()>
        Private Class Criteria
            Private _JournalEntries As JournalEntryInfo() = Nothing
            Private _DateFrom As Date = Today
            Private _DateTo As Date = Today
            Private _PersonFilter As PersonInfo = Nothing
            Public ReadOnly Property JournalEntries() As JournalEntryInfo()
                Get
                    Return _JournalEntries
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
            Public Sub New(ByVal nJournalEntries As JournalEntryInfo())
                _JournalEntries = nJournalEntries
            End Sub
            Public Sub New(ByVal nDateFrom As Date, ByVal nDateTo As Date, ByVal nPersonFilter As PersonInfo)
                _DateFrom = nDateFrom
                _DateTo = nDateTo
                _PersonFilter = nPersonFilter
            End Sub
        End Class


        Private Overloads Sub DataPortal_Create(ByVal criteria As Criteria)

            If Not CanAddObject() Then Throw New System.Security.SecurityException(
                My.Resources.Common_SecurityInsertDenied)

            If criteria.JournalEntries Is Nothing OrElse criteria.JournalEntries.Length < 1 Then
                Throw New Exception(My.Resources.Workers_PayOutNaturalPersonWithTaxesList_JournalEntriesIdsNull)
            End If

            For Each entry As JournalEntryInfo In criteria.JournalEntries
                If Array.IndexOf(AllowedDocumentTypes, entry.DocType) < 0 Then
                    Throw New Exception(String.Format(Workers_PayOutNaturalPersonWithTaxesList_DocumentTypeInvalid,
                        entry.DocTypeHumanReadable, entry.Date.ToString("yyyy-MM-dd"), entry.DocNumber,
                            entry.Content))
                ElseIf entry.PersonID < 1 Then
                    Throw New Exception(String.Format(Workers_PayOutNaturalPersonWithTaxesList_PersonCannotBeNullForDocument,
                        entry.Date.ToString("yyyy-MM-dd"), entry.DocNumber, entry.Content))
                End If
            Next

            For Each entry As JournalEntryInfo In criteria.JournalEntries
                Add(PayOutNaturalPersonWithTaxes.NewPayOutNaturalPersonWithTaxes(entry.Id))
            Next

            _PersonFilter = PersonInfo.Empty()
            _DateFrom = Date.MaxValue
            _DateTo = Date.MinValue
            For Each payment As PayOutNaturalPersonWithTaxes In Me
                If payment.Date.Date > _DateTo.Date Then _DateTo = payment.Date.Date
                If payment.Date.Date < _DateFrom.Date Then _DateFrom = payment.Date.Date
            Next

        End Sub

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)

            If Not CanGetObject() Then Throw New System.Security.SecurityException(
                My.Resources.Common_SecuritySelectDenied)

            Dim myComm As New SQLCommand("FetchPayOutNaturalPersonWithTaxesList")
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
                    Add(PayOutNaturalPersonWithTaxes.GetPayOutNaturalPersonWithTaxes(dr))
                Next

                _DateFrom = criteria.DateFrom
                _DateTo = criteria.DateTo
                _PersonFilter = criteria.PersonFilter

                RaiseListChangedEvents = True

            End Using

        End Sub

        Protected Overrides Sub DataPortal_Update()

            If Not CanEditObject() Then
                For Each i As PayOutNaturalPersonWithTaxes In Me
                    If Not i.IsNew AndAlso i.IsDirty Then
                        Throw New System.Security.SecurityException(My.Resources.Common_SecurityUpdateDenied)
                    End If
                Next
                For Each i As PayOutNaturalPersonWithTaxes In Me.DeletedList
                    If Not i.IsNew Then
                        Throw New System.Security.SecurityException(My.Resources.Common_SecurityUpdateDenied)
                    End If
                Next
            End If
            If Not CanAddObject() Then
                Throw New System.Security.SecurityException(My.Resources.Common_SecurityInsertDenied)
            End If

            Dim potentialUpdatespending As Boolean = False

            For Each i As PayOutNaturalPersonWithTaxes In Me
                If i.IsNew OrElse i.IsDirty Then
                    potentialUpdatespending = True
                    Exit For
                End If
            Next
            If Not potentialUpdatespending Then
                For Each i As PayOutNaturalPersonWithTaxes In Me.DeletedList
                    If Not i.IsNew Then
                        potentialUpdatespending = True
                        Exit For
                    End If
                Next
            End If

            If Not potentialUpdatespending Then
                Throw New Exception(My.Resources.Workers_PayOutNaturalPersonWithTaxesList_ListIsNotDirty)
            End If

            If Not Me.IsValid Then
                Throw New Exception(String.Format(My.Resources.Common_ContainsErrors, vbCrLf,
                    GetAllBrokenRules()))
            End If

            DatabaseAccess.TransactionBegin()

            RaiseListChangedEvents = False

            For Each item As PayOutNaturalPersonWithTaxes In DeletedList
                If Not item.IsNew Then item.DeleteSelf()
            Next
            DeletedList.Clear()

            For Each item As PayOutNaturalPersonWithTaxes In Me
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