Imports ApskaitaObjects.Goods
Namespace HelperLists

    ''' <summary>
    ''' Represents a <see cref="Goods.GoodsItem">goods</see> value object.
    ''' </summary>
    ''' <remarks>Values are stored in the database table goods.</remarks>
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
        Private _AccountSalesNetCosts As Long = 0
        Private _GroupID As Integer = 0
        Private _DefaultVatRateSales As Double = 0
        Private _DefaultVatRatePurchase As Double = 0
        Private _AccountingMethod As GoodsAccountingMethod = Nothing
        Private _DefaultWarehouseID As Integer = 0


        ''' <summary>
        ''' Gets whether an object is a place holder (does not represent real goods).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property IsEmpty() As Boolean _
            Implements IValueObjectIsEmpty.IsEmpty
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return Not _ID > 0
            End Get
        End Property

        ''' <summary>
        ''' Gets an ID of the goods that is assigned by a database (AUTOINCREMENT).
        ''' </summary>
        ''' <remarks>Data is stored in database field goods.ID.</remarks>
        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' Gets a name of the goods for internal use and for use in invoices received. 
        ''' Localized names for the invoices made are handled by the 
        ''' <see cref="RegionalInfoDictionary">RegionalInfoDictionary</see>.
        ''' </summary>
        ''' <remarks>Data is stored in database field goods.Name.</remarks>
        Public ReadOnly Property Name() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Name.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a measure unit of the goods for internal use and for use in invoices 
        ''' received. Localized measure units for the invoices made are handled by the 
        ''' <see cref="RegionalInfoDictionary">RegionalInfoDictionary</see>.
        ''' </summary>
        ''' <remarks>Data is stored in database field goods.MeasureUnit.</remarks>
        Public ReadOnly Property MeasureUnit() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _MeasureUnit.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets how the goods are used in trade operations (sale, purchase, etc.).
        ''' </summary>
        ''' <remarks>Value is stored in the database field goods.TradeItemType.</remarks>
        Public ReadOnly Property TradeItemType() As Documents.TradedItemType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _TradeItemType
            End Get
        End Property

        ''' <summary>
        ''' Gets a goods barcode.
        ''' </summary>
        ''' <remarks>Value is stored in the database field goods.GoodsBarcode.</remarks>
        Public ReadOnly Property GoodsBarcode() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _GoodsBarcode.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a custom goods code. (for internal company use or for integration 
        ''' with external CRM systems)
        ''' </summary>
        ''' <remarks>Value is stored in the database field goods.GoodsCode.</remarks>
        Public ReadOnly Property GoodsCode() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _GoodsCode.Trim
            End Get
        End Property

        ''' <summary>
        ''' Whether the goods are obsolete (no longer in use).
        ''' </summary>
        ''' <remarks>Value is stored in the database field goods.IsObsolete.</remarks>
        Public ReadOnly Property IsObsolete() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsObsolete
            End Get
        End Property

        ''' <summary>
        ''' Gets an <see cref="General.Account.ID">account</see> that is used for
        ''' the value of goods discarded (sold etc.). 
        ''' If the accounting method is set to<see cref="GoodsAccountingMethod.Periodic">
        ''' Periodic</see>, this account is fixed and mainly used by an <see cref="GoodsComplexOperationInventorization">
        ''' inventorization</see> operation (also in some cases by discount and additional costs). 
        ''' If the accounting method is set to<see cref="GoodsAccountingMethod.Persistent">
        ''' Persistent</see>, this account is used as a default goods discard costs
        ''' account by almost every operation, i.e. an operation can override it.
        ''' </summary>
        ''' <remarks>See methodology for BAS No 9 ""Stores"" para. 5.2 and 40.
        ''' Data is stored in database field goods.AccountDiscounts.</remarks>
        Public ReadOnly Property AccountSalesNetCosts() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AccountSalesNetCosts
            End Get
        End Property

        ''' <summary>
        ''' Gets an <see cref="GoodsGroup.ID">ID of the custom goods group</see> 
        ''' that the goods are assigned to.
        ''' </summary>
        ''' <remarks>Value is stored in the database field goods.GroupID.</remarks>
        Public ReadOnly Property GroupID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _GroupID
            End Get
        End Property

        ''' <summary>
        ''' Gets a default VAT rate for the goods beeing sold.
        ''' </summary>
        ''' <remarks>Value is stored in the database field goods.DefaultVatRateSales.</remarks>
        Public ReadOnly Property DefaultVatRateSales() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_DefaultVatRateSales)
            End Get
        End Property

        ''' <summary>
        ''' Gets a default VAT rate for the goods beeing purchased.
        ''' </summary>
        ''' <remarks>Value is stored in the database field goods.DefaultVatRatePurchase.</remarks>
        Public ReadOnly Property DefaultVatRatePurchase() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_DefaultVatRatePurchase)
            End Get
        End Property

        ''' <summary>
        ''' Gets a goods accounting method (periodic/persistent).
        ''' </summary>
        ''' <remarks>Data is stored in database field goods.DefaultAccountingMethod.</remarks>
        Public ReadOnly Property AccountingMethod() As GoodsAccountingMethod
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AccountingMethod
            End Get
        End Property

        ''' <summary>
        ''' Gets an <see cref="Warehouse.ID">ID of the default warehouse</see> 
        ''' that is used to initialize goods operations.
        ''' </summary>
        ''' <remarks>Value is stored in the database field goods.DefaultWarehouseID.</remarks>
        Public ReadOnly Property DefaultWarehouseID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DefaultWarehouseID
            End Get
        End Property


        Protected Overrides Function GetIdValue() As Object
            Return _ID
        End Function

        Public Overrides Function ToString() As String
            If Not _ID > 0 Then Return ""
            If Not StringIsNullOrEmpty(_GoodsBarcode) Then
                Return String.Format(My.Resources.HelperLists_GoodsInfo_ToStringWithCode, _
                    _Name, _GoodsBarcode)
            ElseIf Not StringIsNullOrEmpty(_GoodsCode) Then
                Return String.Format(My.Resources.HelperLists_GoodsInfo_ToStringWithCode, _
                    _Name, _GoodsCode)
            Else
                Return String.Format(My.Resources.HelperLists_GoodsInfo_ToString, _
                    _Name)
            End If
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function EmptyGoodsInfo() As GoodsInfo
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
            _TradeItemType = EnumValueAttribute.ConvertDatabaseID(Of Documents.TradedItemType)(CIntSafe(dr.Item(5 + offset), 0))
            _IsObsolete = ConvertDbBoolean(CIntSafe(dr.Item(6 + offset), 0))
            _AccountSalesNetCosts = CLongSafe(dr.Item(7 + offset), 0)
            _GroupID = CIntSafe(dr.Item(8 + offset), 0)
            _DefaultVatRateSales = CDblSafe(dr.Item(9 + offset), 2, 0)
            _DefaultVatRatePurchase = CDblSafe(dr.Item(10 + offset), 2, 0)
            _AccountingMethod = EnumValueAttribute.ConvertDatabaseID(Of GoodsAccountingMethod) _
                (CIntSafe(dr.Item(11 + offset), 0))
            _DefaultWarehouseID = CIntSafe(dr.Item(12 + offset), 0)

        End Sub

#End Region

    End Class

End Namespace