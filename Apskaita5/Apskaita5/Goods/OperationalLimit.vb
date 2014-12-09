Namespace Goods

    <Serializable()> _
    Public Class OperationalLimit
        Inherits ReadOnlyBase(Of OperationalLimit)

#Region " Business Methods "

        Private _Guid As Guid = Guid.NewGuid
        Private _OperationType As GoodsOperationType = GoodsOperationType.Acquisition
        Private _WarehouseID As Integer = 0
        Private _ChronologyType As OperationChronologyType = OperationChronologyType.Overall
        Private _MaxOperationDate As Date = Date.MaxValue


        Public ReadOnly Property OperationType() As GoodsOperationType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _OperationType
            End Get
        End Property

        Public ReadOnly Property WarehouseID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _WarehouseID
            End Get
        End Property

        Public ReadOnly Property MaxOperationDate() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _MaxOperationDate
            End Get
        End Property

        Public ReadOnly Property ChronologyType() As OperationChronologyType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ChronologyType
            End Get
        End Property



        Protected Overrides Function GetIdValue() As Object
            Return _Guid
        End Function

        Public Overrides Function ToString() As String
            Return _OperationType.ToString
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function GetOperationalLimit(ByVal dr As DataRow) As OperationalLimit
            Return New OperationalLimit(dr)
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

            _OperationType = ConvertEnumDatabaseCode(Of GoodsOperationType) _
                (CIntSafe(dr.Item(1), 0))
            _WarehouseID = CIntSafe(dr.Item(2), 0)
            _MaxOperationDate = CDateSafe(dr.Item(3), Date.MinValue)
            _ChronologyType = ConvertEnumDatabaseCode(Of OperationChronologyType) _
                (CIntSafe(dr.Item(4), 0))

        End Sub

#End Region

    End Class

End Namespace