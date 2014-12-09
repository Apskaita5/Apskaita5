Namespace ActiveReports

    <Serializable()> _
    Public Class ProductionCalculationItem
        Inherits ReadOnlyBase(Of ProductionCalculationItem)

#Region " Business Methods "

        Private _ID As Integer = 0
        Private _Amount As Double = 0
        Private _Date As Date = Today
        Private _IsObsolete As Boolean = False
        Private _Description As String = ""
        Private _InsertDate As DateTime = Now
        Private _UpdateDate As DateTime = Now
        Private _GoodsID As Integer = 0
        Private _GoodsName As String = ""
        Private _GoodsMeasureUnit As String = ""
        Private _GoodsCode As String = ""
        Private _ComponentCount As Integer = 0
        Private _CostsSum As Double = 0


        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        Public ReadOnly Property Amount() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_Amount, 6)
            End Get
        End Property

        Public ReadOnly Property [Date]() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Date
            End Get
        End Property

        Public ReadOnly Property IsObsolete() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsObsolete
            End Get
        End Property

        Public ReadOnly Property Description() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Description.Trim
            End Get
        End Property

        Public ReadOnly Property InsertDate() As DateTime
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _InsertDate
            End Get
        End Property

        Public ReadOnly Property UpdateDate() As DateTime
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _UpdateDate
            End Get
        End Property

        Public ReadOnly Property GoodsID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _GoodsID
            End Get
        End Property

        Public ReadOnly Property GoodsName() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _GoodsName.Trim
            End Get
        End Property

        Public ReadOnly Property GoodsMeasureUnit() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _GoodsMeasureUnit.Trim
            End Get
        End Property

        Public ReadOnly Property GoodsCode() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _GoodsCode.Trim
            End Get
        End Property

        Public ReadOnly Property ComponentCount() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ComponentCount
            End Get
        End Property

        Public ReadOnly Property CostsSum() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_CostsSum)
            End Get
        End Property



        Protected Overrides Function GetIdValue() As Object
            Return _ID
        End Function

        Public Overrides Function ToString() As String
            Return _Date.ToString("yyyy-MM-dd") & " " & _GoodsName & " -> " & _Description
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function GetProductionCalculationItem(ByVal dr As DataRow) As ProductionCalculationItem
            Return New ProductionCalculationItem(dr)
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

            _ID = CIntSafe(dr.item(0), 0)
            _Amount = CDblSafe(dr.Item(1), 6, 0)
            _Date = CDateSafe(dr.Item(2), Today)
            _IsObsolete = ConvertDbBoolean(CIntSafe(dr.Item(3), 0))
            _Description = CStrSafe(dr.Item(4)).Trim
            _InsertDate = DateTime.SpecifyKind(CDateTimeSafe(dr.Item(5), Date.UtcNow), _
                DateTimeKind.Utc).ToLocalTime
            _UpdateDate = DateTime.SpecifyKind(CDateTimeSafe(dr.Item(6), Date.UtcNow), _
                DateTimeKind.Utc).ToLocalTime
            _GoodsID = CIntSafe(dr.Item(7), 0)
            _GoodsName = CStrSafe(dr.Item(8)).Trim
            _GoodsMeasureUnit = CStrSafe(dr.Item(9)).Trim
            _GoodsCode = CStrSafe(dr.Item(10)).Trim
            _ComponentCount = CIntSafe(dr.Item(11), 0)
            _CostsSum = CDblSafe(dr.Item(12), 2, 0)

        End Sub

#End Region

    End Class

End Namespace