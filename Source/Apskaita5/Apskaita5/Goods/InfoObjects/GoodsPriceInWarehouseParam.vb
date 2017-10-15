Namespace Goods

    ''' <summary>
    ''' Represents a collection of parameters that are used to fetch goods 
    ''' balances in the warehouses.
    ''' </summary>
    ''' <remarks>Used to pass parameters in <see cref="GoodsPriceInWarehouseItem">GoodsPriceInWarehouseItem</see>
    ''' and <see cref="GoodsPriceInWarehouseItemList">GoodsPriceInWarehouseItemList</see>.</remarks>
    <Serializable()> _
    Public NotInheritable Class GoodsPriceInWarehouseParam
        Inherits ReadOnlyBase(Of GoodsPriceInWarehouseParam)

#Region " Business Methods "

        Private ReadOnly _GoodsID As Integer = 0
        Private ReadOnly _ParentOperationID As Integer = 0


        ''' <summary>
        ''' Gets an <see cref="GoodsItem.ID">ID of the goods</see> that the 
        ''' data should be fetched for.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property GoodsID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _GoodsID
            End Get
        End Property

        ''' <summary>
        ''' Gets an <see cref="OperationPersistenceObject.ID">ID of the parent
        ''' goods operation</see>, i.e. the operation's impact is ignored when 
        ''' fetching the data.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property ParentOperationID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ParentOperationID
            End Get
        End Property


        Protected Overrides Function GetIdValue() As Object
            Return String.Format("{0}-{1}", _GoodsID.ToString, _ParentOperationID.ToString)
        End Function

        Public Overrides Function ToString() As String
            Return String.Format(My.Resources.Goods_GoodsPriceInWarehouseParam_ToString, _
                _GoodsID.ToString, _ParentOperationID.ToString)
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets a new GoodsPriceInWarehouseParam instance.
        ''' </summary>
        ''' <param name="goodsID">an <see cref="GoodsItem.ID">ID of the goods</see> 
        ''' that the data should be fetched for</param>
        ''' <param name="parentOperationID">an <see cref="OperationPersistenceObject.ID">
        ''' ID of the parent goods operation</see>, i.e. the operation's impact is 
        ''' ignored when fetching the data</param>
        ''' <remarks></remarks>
        Public Shared Function NewGoodsPriceCutParam(ByVal goodsID As Integer, _
            ByVal parentOperationID As Integer) As GoodsPriceInWarehouseParam
            Return New GoodsPriceInWarehouseParam(goodsID, parentOperationID)
        End Function


        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal nGoodsID As Integer, ByVal nOperationID As Integer)
            ' require use of factory methods
            _GoodsID = nGoodsID
            _ParentOperationID = nOperationID
        End Sub

#End Region

    End Class

End Namespace