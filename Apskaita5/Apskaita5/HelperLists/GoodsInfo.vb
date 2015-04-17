Namespace HelperLists

    <Serializable()> _
    Public Class GoodsInfo
        Inherits ReadOnlyBase(Of GoodsInfo)
        Implements IValueObjectIsEmpty

#Region " Business Methods "

        Private _ID As Integer = 0
        Private _Name As String = ""
        Private _MeasureUnit As String = ""
        Private _TradeItemType As Documents.TradedItemType = Documents.TradedItemType.All
        Private _GoodsBarcode As String = ""
        Private _GoodsCode As String = ""
        Private _IsObsolete As Boolean = False


        Public ReadOnly Property IsEmpty() As Boolean _
            Implements IValueObjectIsEmpty.IsEmpty
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return Not _ID > 0
            End Get
        End Property

        Public ReadOnly Property ID() As Integer
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

        Public ReadOnly Property MeasureUnit() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _MeasureUnit.Trim
            End Get
        End Property

        Public ReadOnly Property TradeItemType() As Documents.TradedItemType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _TradeItemType
            End Get
        End Property

        Public ReadOnly Property GoodsBarcode() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _GoodsBarcode.Trim
            End Get
        End Property

        Public ReadOnly Property GoodsCode() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _GoodsCode.Trim
            End Get
        End Property

        Public ReadOnly Property IsObsolete() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsObsolete
            End Get
        End Property

        Public ReadOnly Property GetMe() As GoodsInfo
            Get
                Return Me
            End Get
        End Property



        Protected Overrides Function GetIdValue() As Object
            Return _ID
        End Function

        Public Overrides Function ToString() As String
            If Not _ID > 0 Then Return ""
            Return _Name & " (" & _GoodsCode & ")"
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function NewGoodsInfo() As GoodsInfo
            Return New GoodsInfo()
        End Function

        Friend Shared Function GetGoodsInfo(ByVal dr As DataRow, ByVal offset As Integer) As GoodsInfo
            Return New GoodsInfo(dr, offset)
        End Function


        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal dr As DataRow, ByVal offset As Integer)
            Fetch(dr, offset)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal dr As DataRow, ByVal offset As Integer)

            _ID = CIntSafe(dr.Item(0 + offset), 0)
            _Name = CStrSafe(dr.Item(1 + offset)).Trim
            _MeasureUnit = CStrSafe(dr.Item(2 + offset)).Trim
            _GoodsBarcode = CStrSafe(dr.Item(3 + offset)).Trim
            _GoodsCode = CStrSafe(dr.Item(4 + offset)).Trim
            _TradeItemType = ConvertEnumDatabaseCode(Of Documents.TradedItemType)(CIntSafe(dr.Item(5 + offset), 0))
            _IsObsolete = ConvertDbBoolean(CIntSafe(dr.Item(6 + offset), 0))

        End Sub

#End Region

    End Class

End Namespace