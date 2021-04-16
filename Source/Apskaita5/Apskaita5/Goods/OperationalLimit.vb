Namespace Goods

    ''' <summary>
    ''' Represents a goods operation chronology value (overall, last before, first after).
    ''' </summary>
    ''' <remarks>Aggregates operations becouce it would be too costly to keep
    ''' all the operations info (as compared to a long term asset).</remarks>
    <Serializable()> _
    Public NotInheritable Class OperationalLimit
        Inherits ReadOnlyBase(Of OperationalLimit)

#Region " Business Methods "

        Private ReadOnly _Guid As Guid = Guid.NewGuid
        Private _OperationType As GoodsOperationType = GoodsOperationType.Acquisition
        Private _WarehouseID As Integer = 0
        Private _ChronologyType As OperationChronologyType = OperationChronologyType.Overall
        Private _Date As Date = Date.MaxValue


        ''' <summary>
        ''' Gets a type of the goods operation that the chronology data is for.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property OperationType() As GoodsOperationType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _OperationType
            End Get
        End Property

        ''' <summary>
        ''' Gets an <see cref="Warehouse.ID">ID of the warehouse</see> 
        ''' that the chronology data is for.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property WarehouseID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _WarehouseID
            End Get
        End Property

        ''' <summary>
        ''' Gets a chronology date.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property [Date]() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Date
            End Get
        End Property

        ''' <summary>
        ''' Gets a chronology date type.
        ''' </summary>
        ''' <remarks></remarks>
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
            Return String.Format("{0}: {1}={2} (ID={3})", _
                _ChronologyType.ToString, _OperationType.ToString, _
                _Date.ToString("yyyy-MM-dd"), _WarehouseID.ToString)
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

            _OperationType = ConvertDatabaseID(Of GoodsOperationType) _
                (CIntSafe(dr.Item(1), 0))
            _WarehouseID = CIntSafe(dr.Item(2), 0)
            _Date = CDateSafe(dr.Item(3), Date.MinValue)
            _ChronologyType = ConvertDatabaseID(Of OperationChronologyType) _
                (CIntSafe(dr.Item(4), 0))

        End Sub

#End Region

    End Class

End Namespace