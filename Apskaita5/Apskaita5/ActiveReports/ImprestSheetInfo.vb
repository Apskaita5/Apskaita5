Namespace ActiveReports

    <Serializable()> _
    Public Class ImprestSheetInfo
        Inherits ReadOnlyBase(Of ImprestSheetInfo)

#Region " Business Methods "

        Private _ID As Integer = 0
        Private _Date As Date = Today
        Private _Number As Integer = 0
        Private _Year As Integer = 0
        Private _Month As Integer = 0
        Private _TotalSum As Double = 0
        Private _TotalSumPayedOut As Double = 0
        Private _WorkersCount As Integer = 0
        Private _IsPayedOut As Boolean = False


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

        Public ReadOnly Property Number() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Number
            End Get
        End Property

        Public ReadOnly Property Year() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Year
            End Get
        End Property

        Public ReadOnly Property Month() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Month
            End Get
        End Property

        Public ReadOnly Property TotalSum() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_TotalSum)
            End Get
        End Property

        Public ReadOnly Property TotalSumPayedOut() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_TotalSumPayedOut)
            End Get
        End Property

        Public ReadOnly Property WorkersCount() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _WorkersCount
            End Get
        End Property

        Public ReadOnly Property IsPayedOut() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsPayedOut
            End Get
        End Property



        Protected Overrides Function GetIdValue() As Object
            Return _ID
        End Function

        Public Overrides Function ToString() As String
            If Not _ID > 0 Then Return ""
            Return _Date.ToShortDateString
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function GetImprestSheetInfo(ByVal dr As DataRow) As ImprestSheetInfo
            Return New ImprestSheetInfo(dr)
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
            _Number = CIntSafe(dr.Item(1), 0)
            _Date = CDateSafe(dr.Item(2), Today)
            _Year = CIntSafe(dr.Item(3), 0)
            _Month = CIntSafe(dr.Item(4), 0)
            _WorkersCount = CIntSafe(dr.Item(5), 0)
            _TotalSum = CDblSafe(dr.Item(6), 2, 0)
            _TotalSumPayedOut = CDblSafe(dr.Item(7), 2, 0)
            _IsPayedOut = (CRound(_TotalSum) = CRound(_TotalSumPayedOut))

        End Sub

#End Region

    End Class

End Namespace
