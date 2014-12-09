Namespace ActiveReports

    <Serializable()> _
    Public Class AdvanceReportInfo
        Inherits ReadOnlyBase(Of AdvanceReportInfo)

#Region " Business Methods "

        Private _ID As Integer = 0
        Private _Date As Date = Today
        Private _DocumentNumber As String = ""
        Private _Content As String = ""
        Private _PersonID As Integer = 0
        Private _PersonName As String = ""
        Private _PersonCode As String = ""
        Private _Account As Long = 0
        Private _ExpensesSum As Double = 0
        Private _ExpensesSumVat As Double = 0
        Private _ExpensesSumTotal As Double = 0
        Private _IncomeSum As Double = 0
        Private _IncomeSumVat As Double = 0
        Private _IncomeSumTotal As Double = 0
        Private _CurrencyCode As String = GetCurrentCompany.BaseCurrency
        Private _CurrencyRate As Double = 1
        Private _ExpensesSumLTL As Double = 0
        Private _ExpensesSumVatLTL As Double = 0
        Private _ExpensesSumTotalLTL As Double = 0
        Private _IncomeSumLTL As Double = 0
        Private _IncomeSumVatLTL As Double = 0
        Private _IncomeSumTotalLTL As Double = 0
        Private _Comments As String = ""
        Private _CommentsInternal As String = ""
        Private _TillOrderID As String = ""
        Private _IsIncomeTillOrder As Boolean = False
        Private _TillOrderData As String = ""

        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        Public ReadOnly Property [Date]() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Date
            End Get
        End Property

        Public ReadOnly Property DocumentNumber() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DocumentNumber.Trim
            End Get
        End Property

        Public ReadOnly Property Content() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Content.Trim
            End Get
        End Property

        Public ReadOnly Property PersonID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _PersonID
            End Get
        End Property

        Public ReadOnly Property PersonName() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _PersonName.Trim
            End Get
        End Property

        Public ReadOnly Property PersonCode() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _PersonCode.Trim
            End Get
        End Property

        Public ReadOnly Property Account() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Account
            End Get
        End Property

        Public ReadOnly Property ExpensesSum() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ExpensesSum)
            End Get
        End Property

        Public ReadOnly Property ExpensesSumVat() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ExpensesSumVat)
            End Get
        End Property

        Public ReadOnly Property ExpensesSumTotal() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ExpensesSumTotal)
            End Get
        End Property

        Public ReadOnly Property IncomeSum() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_IncomeSum)
            End Get
        End Property

        Public ReadOnly Property IncomeSumVat() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_IncomeSumVat)
            End Get
        End Property

        Public ReadOnly Property IncomeSumTotal() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_IncomeSumTotal)
            End Get
        End Property

        Public ReadOnly Property CurrencyCode() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _CurrencyCode.Trim
            End Get
        End Property

        Public ReadOnly Property CurrencyRate() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_CurrencyRate, 6)
            End Get
        End Property

        Public ReadOnly Property ExpensesSumLTL() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ExpensesSumLTL)
            End Get
        End Property

        Public ReadOnly Property ExpensesSumVatLTL() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ExpensesSumVatLTL)
            End Get
        End Property

        Public ReadOnly Property ExpensesSumTotalLTL() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ExpensesSumTotalLTL)
            End Get
        End Property

        Public ReadOnly Property IncomeSumLTL() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_IncomeSumLTL)
            End Get
        End Property

        Public ReadOnly Property IncomeSumVatLTL() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_IncomeSumVatLTL)
            End Get
        End Property

        Public ReadOnly Property IncomeSumTotalLTL() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_IncomeSumTotalLTL)
            End Get
        End Property

        Public ReadOnly Property Comments() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Comments.Trim
            End Get
        End Property

        Public ReadOnly Property CommentsInternal() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _CommentsInternal.Trim
            End Get
        End Property

        Public ReadOnly Property TillOrderID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _TillOrderID
            End Get
        End Property

        Public ReadOnly Property IsIncomeTillOrder() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsIncomeTillOrder
            End Get
        End Property

        Public ReadOnly Property TillOrderData() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _TillOrderData.Trim
            End Get
        End Property



        Protected Overrides Function GetIdValue() As Object
            Return _ID
        End Function

        Public Overrides Function ToString() As String
            If Not _ID > 0 Then Return ""
            Return _Date.ToShortDateString & " Nr. " & _DocumentNumber & " : " & _Content
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function GetAdvanceReportInfo(ByVal dr As DataRow) As AdvanceReportInfo
            Return New AdvanceReportInfo(dr)
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

            _ID = CIntSafe(dr.Item(0), 0)
            _Date = CDateSafe(dr.Item(1), Today)
            _DocumentNumber = CStrSafe(dr.Item(2)).Trim
            _Content = CStrSafe(dr.Item(3)).Trim
            _CurrencyCode = CStrSafe(dr.Item(4)).Trim
            _CurrencyRate = CDblSafe(dr.Item(5), 6, 0)
            _Comments = CStrSafe(dr.Item(6)).Trim
            _CommentsInternal = CStrSafe(dr.Item(7)).Trim
            _ExpensesSum = CDblSafe(dr.Item(8), 2, 0)
            _ExpensesSumVat = CDblSafe(dr.Item(9), 2, 0)
            _ExpensesSumTotal = CRound(_ExpensesSum + _ExpensesSumVat)
            _ExpensesSumLTL = CDblSafe(dr.Item(10), 2, 0)
            _ExpensesSumVatLTL = CDblSafe(dr.Item(11), 2, 0)
            _ExpensesSumTotalLTL = CRound(_ExpensesSumLTL + _ExpensesSumVatLTL)
            _IncomeSum = CDblSafe(dr.Item(12), 2, 0)
            _IncomeSumVat = CDblSafe(dr.Item(13), 2, 0)
            _IncomeSumTotal = CRound(_IncomeSum + _IncomeSumVat)
            _IncomeSumLTL = CDblSafe(dr.Item(14), 2, 0)
            _IncomeSumVatLTL = CDblSafe(dr.Item(15), 2, 0)
            _IncomeSumTotalLTL = CRound(_IncomeSumLTL + _IncomeSumVatLTL)
            _PersonID = CIntSafe(dr.Item(16), 0)
            _PersonName = CStrSafe(dr.Item(17)).Trim
            _PersonCode = CStrSafe(dr.Item(18)).Trim
            _Account = CLongSafe(dr.Item(19), 0)
            _TillOrderID = CIntSafe(dr.Item(20), 0)

            If _TillOrderID > 0 Then
                _IsIncomeTillOrder = ConvertDbBoolean(CIntSafe(dr.Item(23), 0))
                If _IsIncomeTillOrder Then
                    _TillOrderData = "KPO " & CDateSafe(dr.Item(21), Today).ToShortDateString _
                        & " Nr. " & CStrSafe(dr.Item(22)).Trim
                Else
                    _TillOrderData = "KIO " & CDateSafe(dr.Item(21), Today).ToShortDateString _
                        & " Nr. " & CStrSafe(dr.Item(22)).Trim
                End If
            Else
                _IsIncomeTillOrder = (CRound(_IncomeSumTotal) > CRound(_ExpensesSumTotal))
            End If

        End Sub

#End Region

    End Class

End Namespace