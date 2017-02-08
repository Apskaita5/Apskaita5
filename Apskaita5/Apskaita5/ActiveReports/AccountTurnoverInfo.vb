Imports ApskaitaObjects.Attributes

Namespace ActiveReports

    ''' <summary>
    ''' Represents an item of an account turnover report (part of <see cref="ActiveReports.FinancialStatementsInfo">FinancialStatementsInfo</see> report).
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public NotInheritable Class AccountTurnoverInfo
        Inherits ReadOnlyBase(Of AccountTurnoverInfo)

#Region " Business Methods "

        Private _ID As Long = 0
        Private _Name As String = ""
        Private _FinancialStatementItemId As Integer = 0
        Private _FinancialStatementItem As String = ""
        Private _FinancialStatementItemType As General.FinancialStatementItemType = General.FinancialStatementItemType.HeaderGeneral
        Private _DebitBalanceFormerPeriodStart As Double = 0
        Private _CreditBalanceFormerPeriodStart As Double = 0
        Private _DebitTurnoverFormerPeriod As Double = 0
        Private _CreditTurnoverFormerPeriod As Double = 0
        Private _DebitClosingFormerPeriod As Double = 0
        Private _CreditClosingFormerPeriod As Double = 0
        Private _DebitBalanceCurrentPeriodStart As Double = 0
        Private _CreditBalanceCurrentPeriodStart As Double = 0
        Private _DebitTurnoverCurrentPeriod As Double = 0
        Private _CreditTurnoverCurrentPeriod As Double = 0
        Private _DebitClosingCurrentPeriod As Double = 0
        Private _CreditClosingCurrentPeriod As Double = 0
        Private _DebitBalanceCurrentPeriodEnd As Double = 0
        Private _CreditBalanceCurrentPeriodEnd As Double = 0


        ''' <summary>
        ''' Account <see cref="General.Account.ID">ID</see>.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property ID() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' Account <see cref="General.Account.Name">name</see>.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Name() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Name.Trim
            End Get
        End Property

        ''' <summary>
        ''' Account <see cref="General.Account.AssociatedReportItem">associated financial report item</see>.
        ''' (a "line" of an income statement or a balance sheet)
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property FinancialStatementItemId() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _FinancialStatementItemId
            End Get
        End Property

        ''' <summary>
        ''' Account <see cref="General.Account.AssociatedReportItem">associated financial report item</see>.
        ''' (a "line" of an income statement or a balance sheet)
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property FinancialStatementItem() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _FinancialStatementItem.Trim
            End Get
        End Property

        ''' <summary>
        ''' A <see cref="General.FinancialStatementItemType">type</see> of <see cref="General.Account.AssociatedReportItem">associated financial report item</see>.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property FinancialStatementItemType() As General.FinancialStatementItemType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _FinancialStatementItemType
            End Get
        End Property

        ''' <summary>
        ''' Account debit balance (if any) at <see cref="FinancialStatementsInfo.FirstPeriodDateStart">FinancialStatementsInfo.FirstPeriodDateStart</see>.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property DebitBalanceFormerPeriodStart() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_DebitBalanceFormerPeriodStart)
            End Get
        End Property

        ''' <summary>
        ''' Account credit balance (if any) at <see cref="FinancialStatementsInfo.FirstPeriodDateStart">FinancialStatementsInfo.FirstPeriodDateStart</see>.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property CreditBalanceFormerPeriodStart() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_CreditBalanceFormerPeriodStart)
            End Get
        End Property

        ''' <summary>
        ''' Account debit turnover (if any) during period from 
        ''' <see cref="FinancialStatementsInfo.FirstPeriodDateStart">FinancialStatementsInfo.FirstPeriodDateStart</see>
        ''' to <see cref="FinancialStatementsInfo.SecondPeriodDateStart">FinancialStatementsInfo.SecondPeriodDateStart</see>.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property DebitTurnoverFormerPeriod() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_DebitTurnoverFormerPeriod)
            End Get
        End Property

        ''' <summary>
        ''' Account credit turnover (if any) during period from 
        ''' <see cref="FinancialStatementsInfo.FirstPeriodDateStart">FinancialStatementsInfo.FirstPeriodDateStart</see>
        ''' to <see cref="FinancialStatementsInfo.SecondPeriodDateStart">FinancialStatementsInfo.SecondPeriodDateStart</see>.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property CreditTurnoverFormerPeriod() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_CreditTurnoverFormerPeriod)
            End Get
        End Property

        ''' <summary>
        ''' Account debit turnover produced by <see cref="General.ClosingEntriesCommand">ClosingEntriesCommand</see> transactions (if any) during period from 
        ''' <see cref="FinancialStatementsInfo.FirstPeriodDateStart">FinancialStatementsInfo.FirstPeriodDateStart</see>
        ''' to <see cref="FinancialStatementsInfo.SecondPeriodDateStart">FinancialStatementsInfo.SecondPeriodDateStart</see>.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property DebitClosingFormerPeriod() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_DebitClosingFormerPeriod)
            End Get
        End Property

        ''' <summary>
        ''' Account credit turnover produced by <see cref="General.ClosingEntriesCommand">ClosingEntriesCommand</see> transactions (if any) during period from 
        ''' <see cref="FinancialStatementsInfo.FirstPeriodDateStart">FinancialStatementsInfo.FirstPeriodDateStart</see>
        ''' to <see cref="FinancialStatementsInfo.SecondPeriodDateStart">FinancialStatementsInfo.SecondPeriodDateStart</see>.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property CreditClosingFormerPeriod() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_CreditClosingFormerPeriod)
            End Get
        End Property

        ''' <summary>
        ''' Account debit balance (if any) at <see cref="FinancialStatementsInfo.SecondPeriodDateStart">FinancialStatementsInfo.SecondPeriodDateStart</see>.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property DebitBalanceCurrentPeriodStart() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_DebitBalanceCurrentPeriodStart)
            End Get
        End Property

        ''' <summary>
        ''' Account credit balance (if any) at <see cref="FinancialStatementsInfo.SecondPeriodDateStart">FinancialStatementsInfo.SecondPeriodDateStart</see>.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property CreditBalanceCurrentPeriodStart() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_CreditBalanceCurrentPeriodStart)
            End Get
        End Property

        ''' <summary>
        ''' Account debit turnover (if any) during the period from 
        ''' <see cref="FinancialStatementsInfo.SecondPeriodDateStart">FinancialStatementsInfo.SecondPeriodDateStart</see>
        ''' to <see cref="FinancialStatementsInfo.SecondPeriodDateEnd">FinancialStatementsInfo.SecondPeriodDateEnd</see>.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property DebitTurnoverCurrentPeriod() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_DebitTurnoverCurrentPeriod)
            End Get
        End Property

        ''' <summary>
        ''' Account credit turnover (if any) during the period from 
        ''' <see cref="FinancialStatementsInfo.SecondPeriodDateStart">FinancialStatementsInfo.SecondPeriodDateStart</see>
        ''' to <see cref="FinancialStatementsInfo.SecondPeriodDateEnd">FinancialStatementsInfo.SecondPeriodDateEnd</see>.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property CreditTurnoverCurrentPeriod() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_CreditTurnoverCurrentPeriod)
            End Get
        End Property

        ''' <summary>
        ''' Account debit turnover produced by <see cref="General.ClosingEntriesCommand">ClosingEntriesCommand</see> transactions (if any) during period from 
        ''' <see cref="FinancialStatementsInfo.SecondPeriodDateStart">FinancialStatementsInfo.SecondPeriodDateStart</see>
        ''' to <see cref="FinancialStatementsInfo.SecondPeriodDateEnd">FinancialStatementsInfo.SecondPeriodDateEnd</see>.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property DebitClosingCurrentPeriod() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_DebitClosingCurrentPeriod)
            End Get
        End Property

        ''' <summary>
        ''' Account credit turnover produced by <see cref="General.ClosingEntriesCommand">ClosingEntriesCommand</see> transactions (if any) during period from 
        ''' <see cref="FinancialStatementsInfo.SecondPeriodDateStart">FinancialStatementsInfo.SecondPeriodDateStart</see>
        ''' to <see cref="FinancialStatementsInfo.SecondPeriodDateEnd">FinancialStatementsInfo.SecondPeriodDateEnd</see>.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property CreditClosingCurrentPeriod() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_CreditClosingCurrentPeriod)
            End Get
        End Property

        ''' <summary>
        ''' Account debit balance (if any) at <see cref="FinancialStatementsInfo.SecondPeriodDateEnd">FinancialStatementsInfo.SecondPeriodDateEnd</see>.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property DebitBalanceCurrentPeriodEnd() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_DebitBalanceCurrentPeriodEnd)
            End Get
        End Property

        ''' <summary>
        ''' Account credit balance (if any) at <see cref="FinancialStatementsInfo.SecondPeriodDateEnd">FinancialStatementsInfo.SecondPeriodDateEnd</see>.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property CreditBalanceCurrentPeriodEnd() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_CreditBalanceCurrentPeriodEnd)
            End Get
        End Property


        ''' <summary>
        ''' Wheather the account is treated as having some turnover.
        ''' </summary>
        ''' <remarks></remarks>
        Public Function HasTurnover() As Boolean
            Return (CRound(_CreditBalanceCurrentPeriodEnd) > 0 OrElse CRound(_DebitBalanceCurrentPeriodEnd) > 0 _
                OrElse CRound(_CreditClosingCurrentPeriod) > 0 OrElse CRound(_DebitClosingCurrentPeriod) > 0 _
                OrElse CRound(_CreditTurnoverCurrentPeriod) > 0 OrElse CRound(_DebitTurnoverCurrentPeriod) > 0 _
                OrElse CRound(_CreditClosingFormerPeriod) > 0 OrElse CRound(_DebitClosingFormerPeriod) > 0 _
                OrElse CRound(_CreditTurnoverFormerPeriod) > 0 OrElse CRound(_DebitTurnoverFormerPeriod) > 0)
        End Function


        Protected Overrides Function GetIdValue() As Object
            Return _ID
        End Function

        Public Overrides Function ToString() As String
            If Not _ID > 0 Then Return ""
            Return String.Format("{0} {1}", _ID.ToString, _Name)
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets an account turnover info by a database query.
        ''' </summary>
        ''' <param name="dr">Database query result.</param>
        ''' <remarks></remarks>
        Friend Shared Function GetAccountTurnoverInfo(ByVal dr As DataRow) As AccountTurnoverInfo
            Return New AccountTurnoverInfo(dr)
        End Function


        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal dr As DataRow)
            Fetch(dr)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal dr As DataRow)

            _ID = CLongSafe(dr.Item(0), 0)
            _Name = CStrSafe(dr.Item(1)).Trim
            _FinancialStatementItemId = CIntSafe(dr.Item(2), 0)
            _FinancialStatementItem = CStrSafe(dr.Item(3)).Trim
            _FinancialStatementItemType = Utilities.ConvertDatabaseID(Of General.FinancialStatementItemType) _
                (CIntSafe(dr.Item(4), 0))

            If CDblSafe(dr.Item(5), 2, 0) > CDblSafe(dr.Item(6), 2, 0) Then
                _DebitBalanceFormerPeriodStart = CRound(CDblSafe(dr.Item(5), 2, 0) _
                    - CDblSafe(dr.Item(6), 2, 0))
                _CreditBalanceFormerPeriodStart = 0
            Else
                _DebitBalanceFormerPeriodStart = 0
                _CreditBalanceFormerPeriodStart = CRound(CDblSafe(dr.Item(6), 2, 0) _
                    - CDblSafe(dr.Item(5), 2, 0))
            End If
            _DebitTurnoverFormerPeriod = CDblSafe(dr.Item(7), 2, 0)
            _CreditTurnoverFormerPeriod = CDblSafe(dr.Item(8), 2, 0)
            _DebitClosingFormerPeriod = CDblSafe(dr.Item(9), 2, 0)
            _CreditClosingFormerPeriod = CDblSafe(dr.Item(10), 2, 0)

            If CRound(_DebitBalanceFormerPeriodStart - _CreditBalanceFormerPeriodStart _
                + _DebitTurnoverFormerPeriod - _CreditTurnoverFormerPeriod) > 0 Then

                _DebitBalanceCurrentPeriodStart = CRound(_DebitBalanceFormerPeriodStart _
                    - _CreditBalanceFormerPeriodStart + _DebitTurnoverFormerPeriod _
                    - _CreditTurnoverFormerPeriod)
                _CreditBalanceCurrentPeriodStart = 0

            Else

                _DebitBalanceCurrentPeriodStart = 0
                _CreditBalanceCurrentPeriodStart = CRound(_CreditBalanceFormerPeriodStart _
                    - _DebitBalanceFormerPeriodStart + _CreditTurnoverFormerPeriod _
                    - _DebitTurnoverFormerPeriod)

            End If

            _DebitTurnoverCurrentPeriod = CDblSafe(dr.Item(11), 2, 0)
            _CreditTurnoverCurrentPeriod = CDblSafe(dr.Item(12), 2, 0)
            _DebitClosingCurrentPeriod = CDblSafe(dr.Item(13), 2, 0)
            _CreditClosingCurrentPeriod = CDblSafe(dr.Item(14), 2, 0)

            If CRound(_DebitBalanceCurrentPeriodStart - _CreditBalanceCurrentPeriodStart _
                + _DebitTurnoverCurrentPeriod - _CreditTurnoverCurrentPeriod) > 0 Then

                _DebitBalanceCurrentPeriodEnd = CRound(_DebitBalanceCurrentPeriodStart _
                    - _CreditBalanceCurrentPeriodStart + _DebitTurnoverCurrentPeriod _
                    - _CreditTurnoverCurrentPeriod)
                _CreditBalanceCurrentPeriodEnd = 0

            Else

                _DebitBalanceCurrentPeriodEnd = 0
                _CreditBalanceCurrentPeriodEnd = CRound(_CreditBalanceCurrentPeriodStart _
                    - _DebitBalanceCurrentPeriodStart + _CreditTurnoverCurrentPeriod _
                    - _DebitTurnoverCurrentPeriod)

            End If

        End Sub

#End Region

    End Class

End Namespace
