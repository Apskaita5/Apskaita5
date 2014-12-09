Namespace ActiveReports

    <Serializable()> _
    Public Class GoodsOperationInfo
        Inherits ReadOnlyBase(Of GoodsOperationInfo)

#Region " Business Methods "

        Private _ID As Integer = 0
        Private _WarehouseID As Integer = 0
        Private _WarehouseName As String = ""
        Private _WarehouseAccount As Long = 0
        Private _Date As Date = Today
        Private _Type As String = ""
        Private _ComplexType As String = ""
        Private _DocNo As String = ""
        Private _Content As String = ""
        Private _Amount As Double = 0
        Private _AmountInWarehouse As Double = 0
        Private _UnitValue As Double = 0
        Private _TotalValue As Double = 0
        Private _AccountGeneral As Double = 0
        Private _AccountSalesNetCosts As Double = 0
        Private _AccountPurchases As Double = 0
        Private _AccountDiscounts As Double = 0
        Private _AccountPriceCut As Double = 0
        Private _AccountOperationValue As Double = 0
        Private _AccountOperation As Long = 0
        Private _ComplexOperationID As Integer = 0
        Private _InsertDate As DateTime = Now
        Private _UpdateDate As DateTime = Now
        Private _JournalEntryID As Integer = 0
        Private _JournalEntryDocNo As String = ""
        Private _JournalEntryDate As Date = Today
        Private _JournalEntryContent As String = ""
        Private _JournalEntryType As String = ""
        Private _JournalEntryCorrespondentions As String = ""


        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        Public ReadOnly Property WarehouseID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _WarehouseID
            End Get
        End Property

        Public ReadOnly Property WarehouseName() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _WarehouseName.Trim
            End Get
        End Property

        Public ReadOnly Property WarehouseAccount() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _WarehouseAccount
            End Get
        End Property

        Public ReadOnly Property [Date]() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Date
            End Get
        End Property

        Public ReadOnly Property [Type]() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Type.Trim
            End Get
        End Property

        Public ReadOnly Property ComplexType() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ComplexType.Trim
            End Get
        End Property

        Public ReadOnly Property DocNo() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DocNo.Trim
            End Get
        End Property

        Public ReadOnly Property Content() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Content.Trim
            End Get
        End Property

        Public ReadOnly Property Amount() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_Amount, 6)
            End Get
        End Property

        Public ReadOnly Property AmountInWarehouse() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AmountInWarehouse, 6)
            End Get
        End Property

        Public ReadOnly Property UnitValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_UnitValue, 6)
            End Get
        End Property

        Public ReadOnly Property TotalValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_TotalValue)
            End Get
        End Property

        Public ReadOnly Property AccountGeneral() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AccountGeneral)
            End Get
        End Property

        Public ReadOnly Property AccountSalesNetCosts() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AccountSalesNetCosts)
            End Get
        End Property

        Public ReadOnly Property AccountPurchases() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AccountPurchases)
            End Get
        End Property

        Public ReadOnly Property AccountDiscounts() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AccountDiscounts)
            End Get
        End Property

        Public ReadOnly Property AccountPriceCut() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AccountPriceCut)
            End Get
        End Property

        Public ReadOnly Property AccountOperationValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AccountOperationValue)
            End Get
        End Property

        Public ReadOnly Property AccountOperation() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AccountOperation
            End Get
        End Property

        Public ReadOnly Property ComplexOperationID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ComplexOperationID
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

        Public ReadOnly Property JournalEntryID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryID
            End Get
        End Property

        Public ReadOnly Property JournalEntryDocNo() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryDocNo.Trim
            End Get
        End Property

        Public ReadOnly Property JournalEntryDate() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryDate
            End Get
        End Property

        Public ReadOnly Property JournalEntryContent() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryContent.Trim
            End Get
        End Property

        Public ReadOnly Property JournalEntryType() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryType.Trim
            End Get
        End Property

        Public ReadOnly Property JournalEntryCorrespondentions() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryCorrespondentions.Trim
            End Get
        End Property



        Protected Overrides Function GetIdValue() As Object
            Return _ID
        End Function

        Public Overrides Function ToString() As String
            If Not _ID > 0 Then Return ""
            Return _Content
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function GetGoodsOperationInfo(ByVal dr As DataRow) As GoodsOperationInfo
            Return New GoodsOperationInfo(dr)
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
            _WarehouseID = CIntSafe(dr.Item(1), 0)
            _WarehouseName = CStrSafe(dr.Item(2)).Trim
            _WarehouseAccount = CLongSafe(dr.Item(3), 0)
            _Date = CDateSafe(dr.Item(4), Today)
            _Type = ConvertEnumHumanReadable(ConvertEnumDatabaseCode(Of Goods.GoodsOperationType) _
                (CIntSafe(dr.Item(5), 1)))
            _ComplexType = ConvertEnumHumanReadable(ConvertEnumDatabaseCode(Of Goods.GoodsComplexOperationType) _
                (CIntSafe(dr.Item(6), 1)))
            _DocNo = CStrSafe(dr.Item(7)).Trim
            _Content = CStrSafe(dr.Item(8)).Trim
            _Amount = CDblSafe(dr.Item(9), 2, 0)
            _AmountInWarehouse = CDblSafe(dr.Item(10), 2, 0)
            _UnitValue = CDblSafe(dr.Item(11), 2, 0)
            _TotalValue = CDblSafe(dr.Item(12), 2, 0)
            _AccountGeneral = CDblSafe(dr.Item(13), 2, 0)
            _AccountSalesNetCosts = CDblSafe(dr.Item(14), 2, 0)
            _AccountPurchases = CDblSafe(dr.Item(15), 2, 0)
            _AccountDiscounts = CDblSafe(dr.Item(16), 2, 0)
            _AccountPriceCut = CDblSafe(dr.Item(17), 2, 0)
            _AccountOperationValue = CDblSafe(dr.Item(18), 2, 0)
            _AccountOperation = CLongSafe(dr.Item(19), 0)
            _ComplexOperationID = CIntSafe(dr.Item(20), 0)
            _InsertDate = DateTime.SpecifyKind(CDateTimeSafe(dr.Item(21), Date.UtcNow), _
                DateTimeKind.Utc).ToLocalTime
            _UpdateDate = DateTime.SpecifyKind(CDateTimeSafe(dr.Item(22), Date.UtcNow), _
                DateTimeKind.Utc).ToLocalTime
            _JournalEntryID = CIntSafe(dr.Item(23), 0)
            _JournalEntryDocNo = CStrSafe(dr.Item(24)).Trim
            _JournalEntryDate = CDateSafe(dr.Item(25), Today)
            _JournalEntryContent = CStrSafe(dr.Item(26)).Trim
            _JournalEntryType = ConvertEnumHumanReadable(ConvertEnumDatabaseStringCode(Of DocumentType) _
                (CStrSafe(dr.Item(27))))
            _JournalEntryCorrespondentions = CStrSafe(dr.Item(28)).Trim

            If Not _ComplexOperationID > 0 Then _ComplexType = ""

        End Sub

#End Region

    End Class

End Namespace