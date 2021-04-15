Namespace HelperLists

    ''' <summary>
    ''' Represents an item of <see cref="IndirectRelationInfoList">IndirectRelationInfoList</see>.
    ''' Contains information about indirect referencies to the journal entry by other documents.
    ''' (when a journal entry is attached to some document, not created and managed by the document)
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public NotInheritable Class IndirectRelationInfo
        Inherits ReadOnlyBase(Of IndirectRelationInfo)

#Region " Business Methods "

        Private ReadOnly _Guid As Guid = Guid.NewGuid
        Private _ID As Integer = 0
        Private _Type As IndirectRelationType = IndirectRelationType.LongTermAssetsOperation
        Private _TypeHumanReadable As String = ""
        Private _Date As Date = Today
        Private _DocumentNumber As String = ""
        Private _Content As String = ""
        Private _GoodsOperationType As Goods.GoodsOperationType = Goods.GoodsOperationType.Acquisition
        Private _AssetOperationType As Assets.LtaOperationType = Assets.LtaOperationType.Transfer


        ''' <summary>
        ''' Gets an ID of the document or operation that references the journal entry.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' Gets a type of the document or operation that references the journal entry.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property [Type]() As IndirectRelationType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Type
            End Get
        End Property

        ''' <summary>
        ''' Gets a type of the document or operation, that references the journal entry, 
        ''' as a localized human readable string.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property TypeHumanReadable() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _TypeHumanReadable.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a date of the document or operation that references the journal entry.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property [Date]() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Date
            End Get
        End Property

        ''' <summary>
        ''' Gets a number of the document or operation that references the journal entry.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DocumentNumber() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DocumentNumber.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a content (description) of the document or operation that references the journal entry.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Content() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Content.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a type of the goods operation that references the journal entry.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property GoodsOperationType() As Goods.GoodsOperationType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _GoodsOperationType
            End Get
        End Property

        ''' <summary>
        ''' Gets a type of the asset operation that references the journal entry.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property AssetOperationType() As Assets.LtaOperationType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AssetOperationType
            End Get
        End Property



        Protected Overrides Function GetIdValue() As Object
            Return _Guid
        End Function

        Public Overrides Function ToString() As String
            Return String.Format(My.Resources.HelperLists_IndirectRelationInfo_ToString, _
                _Date.ToString("yyyy-MM-dd"), _TypeHumanReadable, _DocumentNumber, _ID.ToString(), _Content)
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

            _Type = Utilities.ConvertDatabaseID(Of IndirectRelationType)(CIntSafe(dr.Item(0), 0))
            _ID = CIntSafe(dr.Item(1), 0)
            _Date = CDateSafe(dr.Item(2), Today)
            _DocumentNumber = CStrSafe(dr.Item(3)).Trim
            _Content = CStrSafe(dr.Item(4)).Trim
            _TypeHumanReadable = Utilities.ConvertLocalizedName(_Type)

            If _Type = IndirectRelationType.GoodsOperation Then
                _GoodsOperationType = Utilities.ConvertDatabaseID(Of Goods.GoodsOperationType) _
                    (CIntSafe(dr.Item(5), 1))
                _TypeHumanReadable = String.Format("{0}: {1}", _TypeHumanReadable, _
                    Utilities.ConvertLocalizedName(_GoodsOperationType))
            ElseIf _Type = IndirectRelationType.LongTermAssetsOperation Then
                _AssetOperationType = Utilities.ConvertDatabaseCharID(Of Assets.LtaOperationType) _
                    (CStrSafe(dr.Item(5)))
                _TypeHumanReadable = String.Format("{0}: {1}", _TypeHumanReadable, _
                    Utilities.ConvertLocalizedName(_AssetOperationType))
            End If

        End Sub

#End Region

    End Class

End Namespace