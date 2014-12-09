Namespace HelperLists

    <Serializable()> _
    Public Class IndirectRelationInfo
        Inherits ReadOnlyBase(Of IndirectRelationInfo)

#Region " Business Methods "

        Private _Guid As Guid = Guid.NewGuid
        Private _ID As Integer = 0
        Private _Type As IndirectRelationType = IndirectRelationType.LongTermAssetsOperation
        Private _TypeHumanReadable As String = ""
        Private _Date As Date = Today
        Private _DocumentNumber As String = ""
        Private _Content As String = ""
        Private _GoodsOperationType As Goods.GoodsOperationType = Goods.GoodsOperationType.Acquisition
        Private _AssetOperationType As Assets.LtaOperationType = Assets.LtaOperationType.Transfer


        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        Public ReadOnly Property [Type]() As IndirectRelationType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Type
            End Get
        End Property

        Public ReadOnly Property TypeHumanReadable() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _TypeHumanReadable.Trim
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

        Public ReadOnly Property GoodsOperationType() As Goods.GoodsOperationType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _GoodsOperationType
            End Get
        End Property

        Public ReadOnly Property AssetOperationType() As Assets.LtaOperationType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AssetOperationType
            End Get
        End Property

        Public ReadOnly Property GetMe() As IndirectRelationInfo
            Get
                Return Me
            End Get
        End Property



        Protected Overrides Function GetIdValue() As Object
            Return _Guid
        End Function

        Public Overrides Function ToString() As String
            Return "Susietos operacijos tipas - " & _TypeHumanReadable & ", dokumentas " _
                & _Date.ToShortDateString & " Nr. " & _DocumentNumber & ": " & _Content
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function GetIndirectRelationInfo(ByVal dr As DataRow) As IndirectRelationInfo
            Return New IndirectRelationInfo(dr)
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

            _Type = ConvertEnumDatabaseCode(Of IndirectRelationType)(CIntSafe(dr.Item(0), 0))
            _ID = CIntSafe(dr.Item(1), 0)
            _Date = CDateSafe(dr.Item(2), Today)
            _DocumentNumber = CStrSafe(dr.Item(3)).Trim
            _Content = CStrSafe(dr.Item(4)).Trim
            _TypeHumanReadable = ConvertEnumHumanReadable(_Type)

            If _Type = IndirectRelationType.GoodsOperation Then
                _GoodsOperationType = ConvertEnumDatabaseCode(Of Goods.GoodsOperationType) _
                    (CIntSafe(dr.Item(5), 1))
                _TypeHumanReadable = _TypeHumanReadable & ":" & ConvertEnumHumanReadable(_GoodsOperationType)
            ElseIf _Type = IndirectRelationType.LongTermAssetsOperation Then
                _AssetOperationType = ConvertEnumDatabaseStringCode(Of Assets.LtaOperationType) _
                    (CIntSafe(dr.Item(5), 1))
                _TypeHumanReadable = _TypeHumanReadable & ":" & ConvertEnumHumanReadable(_AssetOperationType)
            End If

        End Sub

#End Region

    End Class

End Namespace