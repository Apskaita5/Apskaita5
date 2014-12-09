Namespace ActiveReports

    <Serializable()> _
    Public Class BalanceSheetInfo
        Inherits ReadOnlyBase(Of BalanceSheetInfo)

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


        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        Public ReadOnly Property Number() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Number.Trim
            End Get
        End Property

        Public ReadOnly Property Name() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Name.Trim
            End Get
        End Property

        Public ReadOnly Property IsCreditBalance() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsCreditBalance
            End Get
        End Property

        Public ReadOnly Property Level() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Level
            End Get
        End Property

        Public ReadOnly Property Left() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Left
            End Get
        End Property

        Public ReadOnly Property Right() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Right
            End Get
        End Property

        Public ReadOnly Property RelatedAccounts() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _RelatedAccounts.Trim
            End Get
        End Property

        Public ReadOnly Property ActualBalanceCurrent() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ActualBalanceCurrent)
            End Get
        End Property

        Public ReadOnly Property ActualBalanceFormer() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ActualBalanceFormer)
            End Get
        End Property

        Public ReadOnly Property OptimizedBalanceCurrent() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_OptimizedBalanceCurrent)
            End Get
        End Property

        Public ReadOnly Property OptimizedBalanceFormer() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_OptimizedBalanceFormer)
            End Get
        End Property


        Friend Sub UpdateOptimizedBalanceCurrentWithValue(ByVal Value As Double)
            If _IsCreditBalance Then
                _OptimizedBalanceCurrent = CRound(_OptimizedBalanceCurrent - Value)
            Else
                _OptimizedBalanceCurrent = CRound(_OptimizedBalanceCurrent + Value)
            End If
        End Sub

        Friend Sub UpdateOptimizedBalanceFormerWithValue(ByVal Value As Double)
            If _IsCreditBalance Then
                _OptimizedBalanceFormer = CRound(_OptimizedBalanceFormer - Value)
            Else
                _OptimizedBalanceFormer = CRound(_OptimizedBalanceFormer + Value)
            End If
        End Sub

        Friend Sub SetNumber(ByVal ParentNumber As String, ByVal n As Integer)
            If _Level = 3 Then
                _Number = GetNumberInLetter(n)
            ElseIf _Level = 4 Then
                _Number = GetRomanNumber(n)
            Else
                _Number = ParentNumber & n.ToString
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

        Friend Shared Function GetBalanceSheetInfo(ByVal dr As DataRow) As BalanceSheetInfo
            Return New BalanceSheetInfo(dr)
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

            _OptimizedBalanceFormer = _ActualBalanceFormer
            _OptimizedBalanceCurrent = _ActualBalanceCurrent

        End Sub

#End Region

    End Class

End Namespace