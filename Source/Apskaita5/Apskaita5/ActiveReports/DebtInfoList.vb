﻿Imports System.Web.UI.WebControls
Imports ApskaitaObjects.Documents.BankDataExchangeProviders
Imports ApskaitaObjects.My.Resources

Namespace ActiveReports

    ''' <summary>
    ''' Represents a buyers or suppliers trade turnover and debt report.
    ''' Contains information about buyers or suppliers and their's trade and settlement turnover.
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public NotInheritable Class DebtInfoList
        Inherits ReadOnlyListBase(Of DebtInfoList, DebtInfo)

#Region " Business Methods "

        Private _DateFrom As Date = Today
        Private _DateTo As Date = Today
        Private _Account As Long = 0
        Private _IsBuyer As Boolean = True
        Private _IsSupplier As Boolean = False
        Private _GroupInfo As HelperLists.PersonGroupInfo = Nothing
        Private _ShowZeroDebts As Boolean = False
        Private _MarginOfError As Double = 0


        ''' <summary>
        ''' Gets the start date of the report.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DateFrom() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DateFrom
            End Get
        End Property

        ''' <summary>
        ''' Gets the end date of the report.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DateTo() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DateTo
            End Get
        End Property

        ''' <summary>
        ''' Gets the buyers or suppliers debts <see cref="General.Account.ID">account</see> of the report.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Account() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Account
            End Get
        End Property

        ''' <summary>
        ''' Gets whether the report was fetched for buyers (i.e. debit balance is considered as positive debt).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property IsBuyer() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsBuyer
            End Get
        End Property

        ''' <summary>
        ''' Gets whether the report was fetched for suppliers (i.e. credit balance is considered as positive debt).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property IsSupplier() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsSupplier
            End Get
        End Property

        ''' <summary>
        ''' Gets the person's group that the report was filtered by.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property GroupInfo() As HelperLists.PersonGroupInfo
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _GroupInfo
            End Get
        End Property

        ''' <summary>
        ''' Gets whether to show items with 0 debt.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property ShowZeroDebtsFilterState() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ShowZeroDebts
            End Get
        End Property

        ''' <summary>
        ''' Technical debt margin, i.e. debt below it is not considered as a real debt.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property MarginOfError() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_MarginOfError)
            End Get
        End Property


        Public Function ExportBankPayments() As ExportedBankPaymentList

            If _IsBuyer Then Throw New Exception(ActiveReports_DebtInfoList_InvalidTypeForPaymentsExport)

            Dim personLookup As PersonInfoList = PersonInfoList.GetList()

            Dim result As ExportedBankPaymentList = ExportedBankPaymentList.NewExportedBankPaymentList()
            For Each debt As DebtInfo In Me
                if debt.DebtEnd > 0.0 Then
                    result.Add(ExportedBankPayment.NewExportedBankPayment(debt.PersonID, _
                        debt.DebtEnd, ActiveReports_DebtInfoList_ExportedBankPaymentPurpose, personLookup)) 
                End If
            Next

            If result.Count < 1 Then Throw New Exception(ActiveReports_DebtInfoList_NoPaymentsToExport)

            Return result

        End Function

#End Region

#Region " Authorization Rules "

        Public Shared Function CanGetObject() As Boolean
            Return ApplicationContext.User.IsInRole("Reports.DebtInfoList1")
        End Function

#End Region

#Region " Factory Methods "

        ' used to implement automatic sort in datagridview
        <NonSerialized()> _
        Private _SortedList As Csla.SortedBindingList(Of DebtInfo) = Nothing


        ''' <summary>
        ''' Gets a new DebtInfoList report instance.
        ''' </summary>
        ''' <param name="dateFrom">the start date of the report</param>
        ''' <param name="dateTo">the end date of the report</param>
        ''' <param name="account">the buyers or suppliers debts <see cref="General.Account.ID">account</see> of the report</param>
        ''' <param name="isBuyer">whether to fetch the report for buyers 
        ''' (i.e. debit balance is considered as positive debt).</param>
        ''' <param name="groupInfo">the person's group to filter the report by</param>
        ''' <param name="showZeroDebts">whether to show items with 0 debt</param>
        ''' <param name="marginOfError">technical debt margin, i.e. debt below it is not 
        ''' considered as a real debt</param>
        ''' <remarks></remarks>
        Public Shared Function GetDebtInfoList(ByVal dateFrom As Date, _
            ByVal dateTo As Date, ByVal account As Long, ByVal isBuyer As Boolean, _
            ByVal groupInfo As PersonGroupInfo, ByVal ignorePersonType As Boolean, _
            ByVal showZeroDebts As Boolean, ByVal marginOfError As Double) As DebtInfoList
            Return DataPortal.Fetch(Of DebtInfoList)(New Criteria(dateFrom, dateTo, _
                account, isBuyer, groupInfo, ignorePersonType, showZeroDebts, marginOfError))
        End Function

        ''' <summary>
        ''' Gets a sortable view of the report.
        ''' </summary>
        ''' <remarks>Used to implement auto sort in a datagridview.</remarks>
        Public Function GetSortedList() As Csla.SortedBindingList(Of DebtInfo)

            If _SortedList Is Nothing Then
                _SortedList = New Csla.SortedBindingList(Of DebtInfo)(Me)
            End If

            Return _SortedList

        End Function


        Private Sub New()
            ' require use of factory methods
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> _
        Private Class Criteria
            Private _DateFrom As Date
            Private _DateTo As Date
            Private _Account As Long
            Private _IsBuyer As Boolean
            Private _GroupInfo As HelperLists.PersonGroupInfo = Nothing
            Private _IgnorePersonType As Boolean
            Private _ShowZeroDebts As Boolean
            Private _MarginOfError As Double = 0
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
            Public ReadOnly Property Account() As Long
                Get
                    Return _Account
                End Get
            End Property
            Public ReadOnly Property IsBuyer() As Boolean
                Get
                    Return _IsBuyer
                End Get
            End Property
            Public ReadOnly Property GroupInfo() As HelperLists.PersonGroupInfo
                Get
                    Return _GroupInfo
                End Get
            End Property
            Public ReadOnly Property IgnorePersonType() As Boolean
                Get
                    Return _IgnorePersonType
                End Get
            End Property
            Public ReadOnly Property ShowZeroDebts() As Boolean
                Get
                    Return _ShowZeroDebts
                End Get
            End Property
            Public ReadOnly Property MarginOfError() As Double
                Get
                    Return CRound(_MarginOfError)
                End Get
            End Property
            Public Sub New(ByVal nDateFrom As Date, ByVal nDateTo As Date, _
                ByVal nAccount As Long, ByVal nIsBuyer As Boolean, _
                ByVal nGroupInfo As PersonGroupInfo, ByVal nIgnorePersonType As Boolean, _
                ByVal nShowZeroDebts As Boolean, ByVal nMarginOfError As Double)
                _DateFrom = nDateFrom
                _DateTo = nDateTo
                _Account = nAccount
                _IsBuyer = nIsBuyer
                _GroupInfo = nGroupInfo
                _IgnorePersonType = nIgnorePersonType
                _ShowZeroDebts = nShowZeroDebts
                _MarginOfError = nMarginOfError
            End Sub
        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)

            If Not CanGetObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecuritySelectDenied)

            If Not criteria.Account > 0 Then Throw New Exception( _
                My.Resources.ActiveReports_DebtInfoList_AccountNull)

            Dim myComm As New SQLCommand("FetchDebtInfoList")
            myComm.AddParam("?DF", criteria.DateFrom)
            myComm.AddParam("?DT", criteria.DateTo)
            myComm.AddParam("?AC", criteria.Account)
            If criteria.IsBuyer Then
                myComm.AddParam("?FC", 1)
                myComm.AddParam("?FS", 0)
            Else
                myComm.AddParam("?FC", 0)
                myComm.AddParam("?FS", 1)
            End If
            myComm.AddParam("?GT", ConvertDbBoolean(criteria.IgnorePersonType))
            If Not criteria.GroupInfo Is Nothing AndAlso criteria.GroupInfo.ID > 0 Then
                myComm.AddParam("?CG", criteria.GroupInfo.ID)
            Else
                myComm.AddParam("?CG", 0)
            End If

            Using myData As DataTable = myComm.Fetch

                RaiseListChangedEvents = False
                IsReadOnly = False

                For Each dr As DataRow In myData.Rows
                    Dim itemToAdd As DebtInfo = DebtInfo.GetDebtInfo(dr, criteria.IsBuyer)
                    If itemToAdd.TurnoverCredit <> 0 OrElse itemToAdd.TurnoverDebet <> 0 OrElse _
                        itemToAdd.DebtEnd <> 0 Then
                        If criteria.ShowZeroDebts OrElse Math.Abs(itemToAdd.DebtEnd) > criteria.MarginOfError Then
                            Add(DebtInfo.GetDebtInfo(dr, criteria.IsBuyer))
                        End If
                    End If
                Next

                _DateFrom = criteria.DateFrom
                _DateTo = criteria.DateTo
                _Account = criteria.Account
                _IsBuyer = criteria.IsBuyer
                _IsSupplier = Not _IsBuyer
                _GroupInfo = criteria.GroupInfo
                _ShowZeroDebts = criteria.ShowZeroDebts
                _MarginOfError = criteria.MarginOfError

                IsReadOnly = True
                RaiseListChangedEvents = True

            End Using

        End Sub

#End Region

    End Class

End Namespace