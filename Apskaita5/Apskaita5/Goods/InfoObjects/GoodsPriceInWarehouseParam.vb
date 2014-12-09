Namespace Goods

    <Serializable()> _
    Public Class GoodsPriceInWarehouseParam
        Inherits ReadOnlyBase(Of GoodsPriceInWarehouseParam)

#Region " Business Methods "

        Private _GoodsID As Integer = 0
        Private _OperationID As Integer = 0

        Public ReadOnly Property GoodsID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _GoodsID
            End Get
        End Property

        Public ReadOnly Property OperationID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _OperationID
            End Get
        End Property



        Protected Overrides Function GetIdValue() As Object
            Return _GoodsID
        End Function

        Public Overrides Function ToString() As String
            Return _GoodsID.ToString
        End Function

#End Region

#Region " Factory Methods "

        Public Shared Function GetGoodsPriceCutParam(ByVal nGoodsID As Integer, _
            ByVal nOperationID As Integer) As GoodsPriceInWarehouseParam
            Return New GoodsPriceInWarehouseParam(nGoodsID, nOperationID)
        End Function


        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal nGoodsID As Integer, ByVal nOperationID As Integer)
            ' require use of factory methods
            _GoodsID = nGoodsID
            _OperationID = nOperationID
        End Sub

#End Region

    End Class

End Namespace