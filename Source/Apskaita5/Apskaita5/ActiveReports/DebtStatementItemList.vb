Imports ApskaitaObjects.My.Resources

Namespace ActiveReports

    ''' <summary>
    ''' Represents a buyers or suppliers trade debt statement report that is used to reconcile
    ''' the debts with the counter parties.
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public Class DebtStatementItemList
        Inherits ReadOnlyListBase(Of DebtStatementItemList, DebtStatementItem)

#Region " Business Methods "

        Private _PeriodStart As Date = Today
        Private _PeriodEnd As Date = Today
        Private _DebtAccount As Long = 0


        ''' <summary>
        ''' Gets the start date of the report period.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property PeriodStart() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _PeriodStart
            End Get
        End Property

        ''' <summary>
        ''' Gets the end date of the report period.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property PeriodEnd() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _PeriodEnd
            End Get
        End Property

        ''' <summary>
        ''' Gets the buyers or suppliers debts <see cref="General.Account.ID">account</see> of the report.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DebtAccount() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DebtAccount
            End Get
        End Property


        ''' <summary>
        ''' Gets a list of person data in the report.
        ''' </summary>
        ''' <remarks></remarks>
        Public Function GetPersonList() As List(Of DebtStatementPerson)

            Dim dict As New Dictionary(Of Integer, DebtStatementPerson)

            For Each i As DebtStatementItem In Me
                If Not dict.ContainsKey(i.PersonId) Then dict.Add(i.PersonId, i.PersonData)
            Next

            Dim result As New List(Of DebtStatementPerson)
            For Each p As DebtStatementPerson In dict.Values
                result.Add(p)
            Next

            Return result

        End Function

        ''' <summary>
        ''' Gets an email of the person specified.
        ''' </summary>
        ''' <param name="personId">an ID of the person to get the email for</param>
        ''' <remarks></remarks>
        Public Function GetEmail(ByVal personId As Integer) As String
            For Each i As DebtStatementItem In Me
                If i.PersonId = personId Then Return i.PersonEmail
            Next
            Return ""
        End Function

#End Region

#Region " Authorization Rules "

        Public Shared Function CanGetObject() As Boolean
            Return ApplicationContext.User.IsInRole("Reports.DebtStatementItemList1")
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets a new DebtStatementItemList report instance.
        ''' </summary>
        ''' <param name="nPeriodStart">the start date of the report period</param>
        ''' <param name="nPeriodEnd">the end date of the report period</param>
        ''' <param name="nDebtAccount">the buyers or suppliers debts 
        ''' <see cref="General.Account.ID">account</see> of the report</param>
        ''' <remarks></remarks>
        Public Shared Function GetDebtStatementItemList(ByVal nPeriodStart As Date, _
            ByVal nPeriodEnd As Date, ByVal nDebtAccount As Long) As DebtStatementItemList
            Return DataPortal.Fetch(Of DebtStatementItemList)(New Criteria(nPeriodStart, nPeriodEnd, nDebtAccount))
        End Function

        Private Sub New()
            ' require use of factory methods
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> _
        Private Class Criteria
            Private _PeriodStart As Date = Today
            Private _PeriodEnd As Date = Today
            Private _DebtAccount As Long = 0
            Public ReadOnly Property PeriodStart() As Date
                <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
                Get
                    Return _PeriodStart
                End Get
            End Property
            Public ReadOnly Property PeriodEnd() As Date
                <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
                Get
                    Return _PeriodEnd
                End Get
            End Property
            Public ReadOnly Property DebtAccount() As Long
                <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
                Get
                    Return _DebtAccount
                End Get
            End Property
            Public Sub New(ByVal nPeriodStart As Date, ByVal nPeriodEnd As Date, ByVal nDebtAccount As Long)
                _PeriodStart = nPeriodStart
                _PeriodEnd = nPeriodEnd
                _DebtAccount = nDebtAccount
            End Sub
        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)

            If Not CanGetObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecuritySelectDenied)

            If Not criteria.DebtAccount > 0 Then Throw New Exception( _
                ActiveReports_DebtStatementItemList_DebtAccountNull)

            Dim myComm As New SQLCommand("FetchDebtStatementItemListBalance")
            myComm.AddParam("?DF", criteria.PeriodStart)
            myComm.AddParam("?AC", criteria.DebtAccount)

            Using balanceData As DataTable = myComm.Fetch

                myComm = New SQLCommand("FetchDebtStatementItemListTransactions")
                myComm.AddParam("?DF", criteria.PeriodStart)
                myComm.AddParam("?DT", criteria.PeriodEnd)
                myComm.AddParam("?AC", criteria.DebtAccount)

                Using transactionData As DataTable = myComm.Fetch

                    Dim totalList As New List(Of DebtStatementItem)
                    For Each dr As DataRow In balanceData.Rows
                        totalList.Add(DebtStatementItem.GetBalanceDebtStatementItem(dr, criteria.PeriodStart))
                    Next
                    For Each dr As DataRow In transactionData.Rows
                        If Not PersonInList(totalList, dr) Then
                            totalList.Add(DebtStatementItem.GetNullBalanceDebtStatementItem(dr, criteria.PeriodStart))
                        End If
                    Next

                    RaiseListChangedEvents = False
                    IsReadOnly = False

                    Dim creditTurnover As Double
                    Dim debitTurnover As Double

                    For Each balance As DebtStatementItem In totalList

                        Add(balance)

                        creditTurnover = 0
                        debitTurnover = 0

                        For Each dr As DataRow In transactionData.Rows
                            If CIntSafe(dr.Item(0), 0) = balance.PersonId Then
                                Dim newItem As DebtStatementItem = DebtStatementItem. _
                                    GetTransactionDebtStatementItem(dr)
                                Add(newItem)
                                creditTurnover = CRound(creditTurnover + newItem.TransactionCredit, 2)
                                debitTurnover = CRound(debitTurnover + newItem.TransactionDebit, 2)
                            End If
                        Next

                        Dim turnover As DebtStatementItem = DebtStatementItem. _
                            GetTurnoverDebtStatementItem(balance, debitTurnover, creditTurnover)
                        Add(turnover)

                        Add(DebtStatementItem.GetBalanceDebtStatementItem(balance, turnover))

                    Next

                    _PeriodStart = criteria.PeriodStart
                    _PeriodEnd = criteria.PeriodEnd
                    _DebtAccount = criteria.DebtAccount

                    IsReadOnly = True
                    RaiseListChangedEvents = True

                End Using

            End Using

        End Sub

        Private Function PersonInList(ByVal list As List(Of DebtStatementItem), ByVal dr As DataRow) As Boolean
            Dim personId As Integer = CIntSafe(dr.Item(0), 0)
            For Each p As DebtStatementItem In list
                If p.PersonId = personId Then Return True
            Next
            Return False
        End Function

#End Region

    End Class

End Namespace