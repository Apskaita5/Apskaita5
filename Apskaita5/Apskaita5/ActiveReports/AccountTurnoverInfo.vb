Namespace ActiveReports

    <Serializable()> _
    Public Class AccountTurnoverInfo
        Inherits ReadOnlyBase(Of AccountTurnoverInfo)

#Region " Business Methods "

        Private _ID As Long = 0
        Private _Name As String = ""
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


        Public ReadOnly Property ID() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        Public ReadOnly Property Name() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Name.Trim
            End Get
        End Property

        Public ReadOnly Property FinancialStatementItem() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _FinancialStatementItem.Trim
            End Get
        End Property

        Public ReadOnly Property FinancialStatementItemType() As General.FinancialStatementItemType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _FinancialStatementItemType
            End Get
        End Property

        Public ReadOnly Property DebitBalanceFormerPeriodStart() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_DebitBalanceFormerPeriodStart)
            End Get
        End Property

        Public ReadOnly Property CreditBalanceFormerPeriodStart() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_CreditBalanceFormerPeriodStart)
            End Get
        End Property

        Public ReadOnly Property DebitTurnoverFormerPeriod() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_DebitTurnoverFormerPeriod)
            End Get
        End Property

        Public ReadOnly Property CreditTurnoverFormerPeriod() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_CreditTurnoverFormerPeriod)
            End Get
        End Property

        Public ReadOnly Property DebitClosingFormerPeriod() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_DebitClosingFormerPeriod)
            End Get
        End Property

        Public ReadOnly Property CreditClosingFormerPeriod() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_CreditClosingFormerPeriod)
            End Get
        End Property

        Public ReadOnly Property DebitBalanceCurrentPeriodStart() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_DebitBalanceCurrentPeriodStart)
            End Get
        End Property

        Public ReadOnly Property CreditBalanceCurrentPeriodStart() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_CreditBalanceCurrentPeriodStart)
            End Get
        End Property

        Public ReadOnly Property DebitTurnoverCurrentPeriod() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_DebitTurnoverCurrentPeriod)
            End Get
        End Property

        Public ReadOnly Property CreditTurnoverCurrentPeriod() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_CreditTurnoverCurrentPeriod)
            End Get
        End Property

        Public ReadOnly Property DebitClosingCurrentPeriod() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_DebitClosingCurrentPeriod)
            End Get
        End Property

        Public ReadOnly Property CreditClosingCurrentPeriod() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_CreditClosingCurrentPeriod)
            End Get
        End Property

        Public ReadOnly Property DebitBalanceCurrentPeriodEnd() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_DebitBalanceCurrentPeriodEnd)
            End Get
        End Property

        Public ReadOnly Property CreditBalanceCurrentPeriodEnd() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_CreditBalanceCurrentPeriodEnd)
            End Get
        End Property


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
            Return _ID.ToString & " " & _Name
        End Function

#End Region

#Region " Factory Methods "

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
            _FinancialStatementItem = CStrSafe(dr.Item(2)).Trim
            _FinancialStatementItemType = ConvertEnumDatabaseCode(Of General.FinancialStatementItemType) _
                (CIntSafe(dr.Item(3), 0))

            If CDblSafe(dr.Item(4), 2, 0) > CDblSafe(dr.Item(5), 2, 0) Then
                _DebitBalanceFormerPeriodStart = CRound(CDblSafe(dr.Item(4), 2, 0) _
                    - CDblSafe(dr.Item(5), 2, 0))
                _CreditBalanceFormerPeriodStart = 0
            Else
                _DebitBalanceFormerPeriodStart = 0
                _CreditBalanceFormerPeriodStart = CRound(CDblSafe(dr.Item(5), 2, 0) _
                    - CDblSafe(dr.Item(4), 2, 0))
            End If
            _DebitTurnoverFormerPeriod = CDblSafe(dr.Item(6), 2, 0)
            _CreditTurnoverFormerPeriod = CDblSafe(dr.Item(7), 2, 0)
            _DebitClosingFormerPeriod = CDblSafe(dr.Item(8), 2, 0)
            _CreditClosingFormerPeriod = CDblSafe(dr.Item(9), 2, 0)

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

            _DebitTurnoverCurrentPeriod = CDblSafe(dr.Item(10), 2, 0)
            _CreditTurnoverCurrentPeriod = CDblSafe(dr.Item(11), 2, 0)
            _DebitClosingCurrentPeriod = CDblSafe(dr.Item(12), 2, 0)
            _CreditClosingCurrentPeriod = CDblSafe(dr.Item(13), 2, 0)

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
