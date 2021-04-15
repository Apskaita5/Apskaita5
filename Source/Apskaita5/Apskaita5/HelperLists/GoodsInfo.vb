Imports ApskaitaObjects.Goods
Imports ApskaitaObjects.Documents

Namespace HelperLists

    ''' <summary>
    ''' Represents a <see cref="Goods.GoodsItem">goods</see> value object.
    ''' </summary>
    ''' <remarks>Values are stored in the database table goods.</remarks>
    <Serializable()> _
    Public NotInheritable Class GoodsInfo
        Inherits ReadOnlyBase(Of GoodsInfo)
        Implements IValueObject, IComparable, ITradedItem, IRegionalDataObject

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
            Implements IValueObject.IsEmpty
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return Not _ID > 0
            End Get
        End Property

        ''' <summary>
        ''' Gets an ID of the goods that is assigned by a database (AUTOINCREMENT).
        ''' </summary>
        ''' <remarks>Data is stored in database field goods.ID.</remarks>
        Public ReadOnly Property ID() As Integer _
            Implements ITradedItem.ID, IRegionalDataObject.RegionalObjectID
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
        Public ReadOnly Property TradeItemType() As Documents.TradedItemType _
            Implements ITradedItem.TradedType
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

        ''' <summary>
        ''' a type of the regionalizable object
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property RegionalObjectType() As RegionalizedObjectType _
            Implements IRegionalDataObject.RegionalObjectType
            Get
                Return RegionalizedObjectType.Goods
            End Get
        End Property


        Public Shared Operator =(ByVal a As GoodsInfo, ByVal b As GoodsInfo) As Boolean

            Dim aId, bId As Integer
            If a Is Nothing OrElse a.IsEmpty Then
                aId = 0
            Else
                aId = a.ID
            End If
            If b Is Nothing OrElse b.IsEmpty Then
                bId = 0
            Else
                bId = b.ID
            End If

            Return aId = bId

        End Operator

        Public Shared Operator <>(ByVal a As GoodsInfo, ByVal b As GoodsInfo) As Boolean
            Return Not a = b
        End Operator

        Public Shared Operator >(ByVal a As GoodsInfo, ByVal b As GoodsInfo) As Boolean

            Dim aToString, bToString As String
            If a Is Nothing OrElse a.IsEmpty Then
                aToString = ""
            Else
                aToString = a.ToString
            End If
            If b Is Nothing OrElse b.IsEmpty Then
                bToString = ""
            Else
                bToString = b.ToString
            End If

            Return aToString > bToString

        End Operator

        Public Shared Operator <(ByVal a As GoodsInfo, ByVal b As GoodsInfo) As Boolean

            Dim aToString, bToString As String
            If a Is Nothing OrElse a.IsEmpty Then
                aToString = ""
            Else
                aToString = a.ToString
            End If
            If b Is Nothing OrElse b.IsEmpty Then
                bToString = ""
            Else
                bToString = b.ToString
            End If

            Return aToString < bToString

        End Operator

        Public Function CompareTo(ByVal obj As Object) As Integer Implements System.IComparable.CompareTo
            Dim tmp As GoodsInfo = TryCast(obj, GoodsInfo)
            If Me = tmp Then Return 0
            If Me > tmp Then Return 1
            Return -1
        End Function


        Friend Function GetValueObjectIdString() As String
            If Me.IsEmpty Then Return ""
            Return _ID.ToString(Globalization.CultureInfo.InvariantCulture)
        End Function


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

        Private Shared _Empty As GoodsInfo = Nothing

        ''' <summary>
        ''' Gets an empty GoodsInfo (placeholder).
        ''' </summary>
        Public Shared Function Empty() As GoodsInfo
            If _Empty Is Nothing Then
                _Empty = New GoodsInfo
            End If
            Return _Empty
        End Function

        Friend Shared Function GetGoodsInfo(ByVal dr As DataRow, ByVal offset As Integer) As GoodsInfo
            Return New GoodsInfo(dr, offset)
        End Function

        Friend Shared Function GetGoodsInfo(ByVal goodsID As Integer) As GoodsInfo
            Return New GoodsInfo(goodsID)
        End Function


        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal dr As DataRow, ByVal offset As Integer)
            Fetch(dr, offset)
        End Sub

        Private Sub New(ByVal goodsID As Integer)
            Fetch(goodsID)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal goodsID As Integer)

            Dim myComm As New SQLCommand("FetchGoodsInfo")
            myComm.AddParam("?GD", goodsID)

            Using myData As DataTable = myComm.Fetch()

                If myData.Rows.Count < 1 Then Throw New Exception(String.Format( _
                    My.Resources.Common_ObjectNotFound, My.Resources.Goods_GoodsItem_TypeName, _
                    goodsID.ToString()))

                Fetch(myData.Rows(0), 0)

            End Using

        End Sub

        Private Sub Fetch(ByVal dr As DataRow, ByVal offset As Integer)

            _ID = CIntSafe(dr.Item(0 + offset), 0)
            _Name = CStrSafe(dr.Item(1 + offset)).Trim
            _MeasureUnit = CStrSafe(dr.Item(2 + offset)).Trim
            _GoodsBarcode = CStrSafe(dr.Item(3 + offset)).Trim
            _GoodsCode = CStrSafe(dr.Item(4 + offset)).Trim
            _TradeItemType = Utilities.ConvertDatabaseID(Of Documents.TradedItemType)(CIntSafe(dr.Item(5 + offset), 0))
            _IsObsolete = ConvertDbBoolean(CIntSafe(dr.Item(6 + offset), 0))
            _AccountSalesNetCosts = CLongSafe(dr.Item(7 + offset), 0)
            _GroupID = CIntSafe(dr.Item(8 + offset), 0)
            _DefaultVatRateSales = CDblSafe(dr.Item(9 + offset), 2, 0)
            _DefaultVatRatePurchase = CDblSafe(dr.Item(10 + offset), 2, 0)
            _AccountingMethod = Utilities.ConvertDatabaseID(Of GoodsAccountingMethod) _
                (CIntSafe(dr.Item(11 + offset), 0))
            _DefaultWarehouseID = CIntSafe(dr.Item(12 + offset), 0)

        End Sub

#End Region

    End Class

End Namespace
