Imports ApskaitaObjects.My.Resources

Namespace ActiveReports

    ''' <summary>
    ''' Represents an item of a <see cref="DebtStatementItemList">DebtStatementItemList</see> report.
    ''' Contains information about a debt transaction, balance of turnover.
    ''' </summary>
    ''' <remarks>Should only be used as a child of <see cref="DebtStatementItemList">DebtStatementItemList</see>.</remarks>
    <Serializable()> _
    Public Class DebtStatementItem
        Inherits ReadOnlyBase(Of DebtStatementItem)

#Region " Business Methods "

        Private ReadOnly _Guid As Guid = Guid.NewGuid()
        Private _PersonData As DebtStatementPerson = Nothing
        Private _ItemType As DebtStatementItemType = DebtStatementItemType.Transaction
        Private _JournalEntryId As Integer = 0
        Private _DocumentType As DocumentType = DocumentType.None
        Private _Date As Date = Today
        Private _DocumentNo As String = ""
        Private _Content As String = ""
        Private _TransactionDebit As Double = 0
        Private _TransactionCredit As Double = 0


        ''' <summary>
        ''' Gets the person data.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property PersonData() As DebtStatementPerson
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _PersonData
            End Get
        End Property

        ''' <summary>
        ''' Gets an ID of the person.
        ''' </summary>
        ''' <remarks>Corresponds to the <see cref="General.Person.ID">Person.ID</see> property.</remarks>
        Public ReadOnly Property PersonId() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If _PersonData Is Nothing Then
                    Return 0
                Else
                    Return _PersonData.PersonId
                End If
            End Get
        End Property

        ''' <summary>
        ''' Gets a name of the person.
        ''' </summary>
        ''' <remarks>Corresponds to the <see cref="General.Person.Name">Person.Name</see> property.</remarks>
        Public ReadOnly Property PersonName() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If _PersonData Is Nothing Then
                    Return ""
                Else
                    Return _PersonData.PersonName
                End If
            End Get
        End Property

        ''' <summary>
        ''' Gets a company/personal code of the person.
        ''' </summary>
        ''' <remarks>Corresponds to the <see cref="General.Person.Code">Person.Code</see> property.</remarks>
        Public ReadOnly Property PersonCode() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If _PersonData Is Nothing Then
                    Return ""
                Else
                    Return _PersonData.PersonCode
                End If
            End Get
        End Property

        ''' <summary>
        ''' Gets an adress of the person.
        ''' </summary>
        ''' <remarks>Corresponds to the <see cref="General.Person.Address">Person.Address</see> property.</remarks>
        Public ReadOnly Property PersonAddress() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If _PersonData Is Nothing Then
                    Return ""
                Else
                    Return _PersonData.PersonAddress
                End If
            End Get
        End Property

        ''' <summary>
        ''' Gets a VAT code of the person.
        ''' </summary>
        ''' <remarks>Corresponds to the <see cref="General.Person.CodeVAT">Person.CodeVAT</see> property.</remarks>
        Public ReadOnly Property PersonVatCode() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If _PersonData Is Nothing Then
                    Return ""
                Else
                    Return _PersonData.PersonVatCode
                End If
            End Get
        End Property

        ''' <summary>
        ''' Gets an email adress of the person.
        ''' </summary>
        ''' <remarks>Corresponds to the <see cref="General.Person.Email">Person.Email</see> property.</remarks>
        Public ReadOnly Property PersonEmail() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If _PersonData Is Nothing Then
                    Return ""
                Else
                    Return _PersonData.PersonEmail
                End If
            End Get
        End Property

        ''' <summary>
        ''' Gets the description of the person (could be used for grouping).
        ''' </summary>
        ''' <remarks>The format depends on the user culture.</remarks>
        Public ReadOnly Property PersonDescription() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If _PersonData Is Nothing Then
                    Return ""
                Else
                    Return _PersonData.PersonDescription
                End If
            End Get
        End Property

        ''' <summary>
        ''' Gets a type of the item.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property ItemType() As DebtStatementItemType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ItemType
            End Get
        End Property

        ''' <summary>
        ''' Gets the ID of the journal entry that owns the transaction.
        ''' </summary>
        ''' <remarks>Corresponds to the <see cref="General.JournalEntry.ID">JournalEntry.ID</see> property.</remarks>
        Public ReadOnly Property JournalEntryId() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryId
            End Get
        End Property

        ''' <summary>
        ''' Gets the type of the journal entry that owns the transaction.
        ''' </summary>
        ''' <remarks>Corresponds to the <see cref="General.JournalEntry.DocType">JournalEntry.DocType</see> property.</remarks>
        Public ReadOnly Property DocumentType() As DocumentType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DocumentType
            End Get
        End Property

        ''' <summary>
        ''' Gets the transaction date.
        ''' </summary>
        ''' <remarks>Corresponds to the <see cref="General.JournalEntry.Date">JournalEntry.Date</see> property.</remarks>
        Public ReadOnly Property [Date]() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Date
            End Get
        End Property

        ''' <summary>
        ''' Gets the transaction document number.
        ''' </summary>
        ''' <remarks>Corresponds to the <see cref="General.JournalEntry.DocNumber">JournalEntry.DocNumber</see> property.</remarks>
        Public ReadOnly Property DocumentNo() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DocumentNo.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets the description of the journal entry that owns the transaction.
        ''' </summary>
        ''' <remarks>Corresponds to the <see cref="General.JournalEntry.Content">JournalEntry.Content</see> property.</remarks>
        Public ReadOnly Property Content() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Content.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets the debit value of the transaction (either individual transaction or turnover/balance).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property TransactionDebit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_TransactionDebit)
            End Get
        End Property

        ''' <summary>
        ''' Gets the credit value of the transaction (either individual transaction or turnover/balance).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property TransactionCredit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_TransactionCredit)
            End Get
        End Property


        Protected Overrides Function GetIdValue() As Object
            Return _Guid
        End Function

        Public Overrides Function ToString() As String
            If _ItemType = DebtStatementItemType.Transaction Then
                Return String.Format(ActiveReports_DebtStatementItem_ToStringTransaction, _
                    _Date, _DocumentNo, _Content, _TransactionDebit, _TransactionCredit)
            Else
                Return String.Format(ActiveReports_DebtStatementItem_ToString, _
                    _Content, _TransactionDebit, _TransactionCredit)
            End If
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function GetBalanceDebtStatementItem(ByVal dr As DataRow, _
            ByVal periodStart As Date) As DebtStatementItem
            Return New DebtStatementItem(dr, periodStart, False)
        End Function

        Friend Shared Function GetNullBalanceDebtStatementItem(ByVal dr As DataRow, _
            ByVal periodStart As Date) As DebtStatementItem
            Return New DebtStatementItem(dr, periodStart, True)
        End Function

        Friend Shared Function GetBalanceDebtStatementItem(ByVal balanceItem As DebtStatementItem, _
            ByVal turnoverItem As DebtStatementItem) As DebtStatementItem
            Return New DebtStatementItem(balanceItem, turnoverItem)
        End Function

        Friend Shared Function GetTransactionDebtStatementItem(ByVal dr As DataRow) As DebtStatementItem
            Return New DebtStatementItem(dr)
        End Function

        Friend Shared Function GetTurnoverDebtStatementItem(ByVal balanceItem As DebtStatementItem, _
            ByVal debitTurnover As Double, ByVal creditTurnover As Double) As DebtStatementItem
            Return New DebtStatementItem(balanceItem, debitTurnover, creditTurnover)
        End Function


        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal dr As DataRow, ByVal periodStart As Date, ByVal isNullBalnceItem As Boolean)
            If isNullBalnceItem Then
                FetchNullBalance(dr, periodStart)
            Else
                FetchBalance(dr, periodStart)
            End If
        End Sub

        Private Sub New(ByVal dr As DataRow)
            FetchTransaction(dr)
        End Sub

        Private Sub New(ByVal balanceItem As DebtStatementItem, ByVal turnoverItem As DebtStatementItem)
            FetchBalanceEnd(balanceItem, turnoverItem)
        End Sub

        Private Sub New(ByVal balanceItem As DebtStatementItem, ByVal debitTurnover As Double, _
            ByVal creditTurnover As Double)
            FetchTurnover(balanceItem, debitTurnover, creditTurnover)
        End Sub

#End Region

#Region " Data Access "

        Private Sub FetchBalance(ByVal dr As DataRow, ByVal periodStart As Date)

            _PersonData = DebtStatementPerson.GetDebtStatementPerson(dr)
            _JournalEntryId = 0
            _Date = periodStart.AddDays(-1)
            _DocumentNo = ""
            _Content = ActiveReports_DebtStatementItem_ContentBalanceStart
            If CDblSafe(dr.Item(6), 2, 0) > CDblSafe(dr.Item(7), 2, 0) Then
                _TransactionDebit = CRound(CDblSafe(dr.Item(6), 2, 0) - CDblSafe(dr.Item(7), 2, 0))
                _TransactionCredit = 0
            Else
                _TransactionDebit = 0
                _TransactionCredit = CRound(CDblSafe(dr.Item(7), 2, 0) - CDblSafe(dr.Item(6), 2, 0))
            End If
            _ItemType = DebtStatementItemType.BalanceStart

        End Sub

        Private Sub FetchNullBalance(ByVal dr As DataRow, ByVal periodStart As Date)

            _PersonData = DebtStatementPerson.GetDebtStatementPerson(dr)
            _JournalEntryId = 0
            _Date = periodStart.AddDays(-1)
            _DocumentNo = ""
            _Content = ActiveReports_DebtStatementItem_ContentBalanceStart
            _TransactionDebit = 0
            _TransactionCredit = 0
            _ItemType = DebtStatementItemType.BalanceStart

        End Sub

        Private Sub FetchTransaction(ByVal dr As DataRow)

            _PersonData = DebtStatementPerson.GetDebtStatementPerson(dr)
            _JournalEntryId = CIntSafe(dr.Item(6), 0)
            _Date = CDateSafe(dr.Item(7), Today)
            _DocumentNo = CStrSafe(dr.Item(8)).Trim
            _Content = CStrSafe(dr.Item(9)).Trim
            _DocumentType = ConvertDatabaseCharID(Of DocumentType)(CStrSafe(dr.Item(10)))
            _TransactionDebit = CDblSafe(dr.Item(11), 2, 0)
            _TransactionCredit = CDblSafe(dr.Item(12), 2, 0)
            _ItemType = DebtStatementItemType.Transaction

        End Sub

        Private Sub FetchTurnover(ByVal balanceItem As DebtStatementItem, ByVal debitTurnover As Double, _
            ByVal creditTurnover As Double)

            _PersonData = balanceItem._PersonData
            _JournalEntryId = 0
            _Date = Date.MaxValue
            _DocumentNo = ""
            _Content = ActiveReports_DebtStatementItem_ContentTurnover
            _TransactionDebit = debitTurnover
            _TransactionCredit = creditTurnover
            _ItemType = DebtStatementItemType.Turnover

        End Sub

        Private Sub FetchBalanceEnd(ByVal balanceItem As DebtStatementItem, ByVal turnoverItem As DebtStatementItem)

            _PersonData = balanceItem._PersonData
            _JournalEntryId = 0
            _Date = Date.MaxValue
            _DocumentNo = ""
            _Content = ActiveReports_DebtStatementItem_ContentBalanceEnd

            Dim balance As Double = CRound(balanceItem._TransactionDebit - balanceItem._TransactionCredit _
                + turnoverItem._TransactionDebit - turnoverItem._TransactionCredit)

            If balance < 0 Then
                _TransactionDebit = 0
                _TransactionCredit = -balance

            Else
                _TransactionDebit = balance
                _TransactionCredit = 0
            End If

            _ItemType = DebtStatementItemType.Turnover

        End Sub

#End Region

    End Class

End Namespace