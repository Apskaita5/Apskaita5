Namespace ActiveReports

    ''' <summary>
    ''' Represents an item of an income statement report (part of <see cref="ActiveReports.FinancialStatementsInfo">FinancialStatementsInfo</see> report).
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public Class IncomeStatementInfo
        Inherits ReadOnlyBase(Of IncomeStatementInfo)

#Region " Business Methods "

        Private _ID As Integer = 0
        Private _Number As String = ""
        Private _Name As String = ""
        Private _IsCreditBalance As Boolean = False
        Private _Level As Integer = 0
        Private _Left As Integer = 0
        Private _Right As Integer = 0
        Private _RelatedAccounts As String = ""
        Private _ActualBalanceCurrent As Double = 0
        Private _ActualBalanceFormer As Double = 0
        Private _OptimizedBalanceCurrent As Double = 0
        Private _OptimizedBalanceFormer As Double = 0


        ''' <summary>
        ''' Gets the ID of the income statement item that is asigned by a database (AUTOINCREMENT).
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="General.ConsolidatedReportItem.ID">ConsolidatedReportItem.ID</see>.</remarks>
        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' Gets the number of the income statement item.
        ''' </summary>
        ''' <remarks>Is calculated by the method <see cref="SetNumber">SetNumber</see>.</remarks>
        Public ReadOnly Property Number() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Number.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets the name of the income statement item.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="General.ConsolidatedReportItem.Name">ConsolidatedReportItem.Name</see>.</remarks>
        Public ReadOnly Property Name() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Name.Trim
            End Get
        End Property

        ''' <summary>
        ''' Whether a balance of type credit is treated as positive number.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="General.ConsolidatedReportItem.IsCredit">ConsolidatedReportItem.IsCredit</see>.</remarks>
        Public ReadOnly Property IsCreditBalance() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsCreditBalance
            End Get
        End Property

        ''' <summary>
        ''' Gets a depth of the income statement item within the hierarchical structure of income statement report.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Level() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Level
            End Get
        End Property

        ''' <summary>
        ''' Item left coordinate in hierarchical structure (Nested Set Model).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Left() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Left
            End Get
        End Property

        ''' <summary>
        ''' Item right coordinate in hierarchical structure (Nested Set Model).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Right() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Right
            End Get
        End Property

        ''' <summary>
        ''' Gets a comma separated list of account ID's that are associated with the income statement item.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property RelatedAccounts() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _RelatedAccounts.Trim
            End Get
        End Property

        ''' <summary>
        ''' Current balance without excluding closing impact.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property ActualBalanceCurrent() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ActualBalanceCurrent)
            End Get
        End Property

        ''' <summary>
        ''' Previous period balance without excluding closing impact.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property ActualBalanceFormer() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ActualBalanceFormer)
            End Get
        End Property

        ''' <summary>
        ''' Current balance excluding closing impact.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property OptimizedBalanceCurrent() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_OptimizedBalanceCurrent)
            End Get
        End Property

        ''' <summary>
        ''' Previous period balance excluding closing impact.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property OptimizedBalanceFormer() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_OptimizedBalanceFormer)
            End Get
        End Property


        ''' <summary>
        ''' Calculates <see cref="OptimizedBalanceCurrent">OptimizedBalanceCurrent</see>
        ''' and <see cref="OptimizedBalanceFormer">OptimizedBalanceFormer</see>
        ''' by adding account turnover and excluding a closing impact.
        ''' </summary>
        ''' <param name="accountTurnover">An account turnover to add.</param>
        ''' <remarks></remarks>
        Friend Sub UpdateOptimizedValues(ByVal accountTurnover As AccountTurnoverInfo)

            If _IsCreditBalance Then
                _OptimizedBalanceCurrent = CRound(_OptimizedBalanceCurrent _
                    + accountTurnover.CreditTurnoverCurrentPeriod _
                    - accountTurnover.DebitTurnoverCurrentPeriod _
                    - accountTurnover.CreditClosingCurrentPeriod _
                    + accountTurnover.DebitClosingCurrentPeriod)
                _OptimizedBalanceFormer = CRound(_OptimizedBalanceFormer _
                    + accountTurnover.CreditTurnoverFormerPeriod _
                    - accountTurnover.DebitTurnoverFormerPeriod _
                    - accountTurnover.CreditClosingFormerPeriod _
                    + accountTurnover.DebitClosingFormerPeriod)
            Else
                _OptimizedBalanceCurrent = CRound(_OptimizedBalanceCurrent _
                    - accountTurnover.CreditTurnoverCurrentPeriod _
                    + accountTurnover.DebitTurnoverCurrentPeriod _
                    + accountTurnover.CreditClosingCurrentPeriod _
                    - accountTurnover.DebitClosingCurrentPeriod)
                _OptimizedBalanceFormer = CRound(_OptimizedBalanceFormer _
                    - accountTurnover.CreditTurnoverFormerPeriod _
                    + accountTurnover.DebitTurnoverFormerPeriod _
                    + accountTurnover.CreditClosingFormerPeriod _
                    - accountTurnover.DebitClosingFormerPeriod)
            End If

        End Sub

        ''' <summary>
        ''' Recursively sets the item <see cref="Number">Number</see>.
        ''' </summary>
        ''' <param name="parentNumber">Number of the parent income statement item, that is included withing current item number.</param>
        ''' <param name="n">Current running nuber within the current group of income statement items.</param>
        ''' <remarks></remarks>
        Friend Sub SetNumber(ByVal parentNumber As String, ByVal n As Integer)
            If Not n > 0 Then
                _Number = parentNumber
            Else
                _Number = parentNumber & n.ToString
            End If
        End Sub


        Protected Overrides Function GetIdValue() As Object
            Return _ID
        End Function

        Public Overrides Function ToString() As String
            If Not _ID > 0 Then Return ""
            Return _Name
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets an income statement info by a database query.
        ''' </summary>
        ''' <param name="dr">Database query result.</param>
        ''' <remarks></remarks>
        Friend Shared Function GetIncomeStatementInfo(ByVal dr As DataRow) As IncomeStatementInfo
            Return New IncomeStatementInfo(dr)
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

            _ID = CIntSafe(dr.Item(1), 0)
            _Name = CStrSafe(dr.Item(2)).Trim
            _Left = CIntSafe(dr.Item(3), 0)
            _Right = CIntSafe(dr.Item(4), 0)
            _IsCreditBalance = ConvertDbBoolean(CIntSafe(dr.Item(5), 0))
            _Level = CIntSafe(dr.Item(6), 0)
            _RelatedAccounts = CStrSafe(dr.Item(7)).Trim
            _ActualBalanceFormer = CDblSafe(dr.Item(8), 2, 0)
            _ActualBalanceCurrent = CDblSafe(dr.Item(9), 2, 0)

            _OptimizedBalanceFormer = 0
            _OptimizedBalanceCurrent = 0

        End Sub

#End Region

    End Class

End Namespace