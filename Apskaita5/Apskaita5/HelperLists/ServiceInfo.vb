Namespace HelperLists

    <Serializable()> _
    Public Class ServiceInfo
        Inherits ReadOnlyBase(Of ServiceInfo)
        Implements IComparable, IValueObjectIsEmpty

#Region " Business Methods "

        Private _ID As Integer = 0
        Private _Type As Documents.TradedItemType = Documents.TradedItemType.All
        Private _TypeHumanReadable As String = ConvertEnumHumanReadable(Documents.TradedItemType.All)
        Private _NameShort As String = ""
        Private _Amount As Double = 0
        Private _AccountSales As Long = 0
        Private _RateVatSales As Double = 0
        Private _AccountVatSales As Long = 0
        Private _AccountPurchase As Long = 0
        Private _RateVatPurchase As Double = 0
        Private _AccountVatPurchase As Long = 0
        Private _RegionalContents As RegionalContentInfoList
        Private _RegionalPrices As RegionalPriceInfoList
        Private _IsObsolete As Boolean = False
        Private _IsInUse As Boolean = False


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

        Public ReadOnly Property [Type]() As Documents.TradedItemType
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

        Public ReadOnly Property NameShort() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _NameShort.Trim
            End Get
        End Property

        Public ReadOnly Property Amount() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_Amount, 4)
            End Get
        End Property

        Public ReadOnly Property AccountSales() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AccountSales
            End Get
        End Property

        Public ReadOnly Property RateVatSales() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_RateVatSales)
            End Get
        End Property

        Public ReadOnly Property AccountVatSales() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AccountVatSales
            End Get
        End Property

        Public ReadOnly Property AccountPurchase() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AccountPurchase
            End Get
        End Property

        Public ReadOnly Property RateVatPurchase() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_RateVatPurchase)
            End Get
        End Property

        Public ReadOnly Property AccountVatPurchase() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AccountVatPurchase
            End Get
        End Property

        Public ReadOnly Property RegionalContents() As RegionalContentInfoList
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _RegionalContents
            End Get
        End Property

        Public ReadOnly Property RegionalPrices() As RegionalPriceInfoList
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _RegionalPrices
            End Get
        End Property

        Public ReadOnly Property IsObsolete() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsObsolete
            End Get
        End Property

        Public ReadOnly Property IsInUse() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsInUse
            End Get
        End Property


        Public ReadOnly Property GetMe() As ServiceInfo
            Get
                Return Me
            End Get
        End Property


        Public Function GetRegionalContent(ByVal LanguageCode As String) As RegionalContentInfo
            If _RegionalContents Is Nothing Then Return Nothing
            For Each r As RegionalContentInfo In _RegionalContents
                If r.LanguageCode.Trim.ToUpper = LanguageCode.Trim.ToUpper Then Return r
            Next
            Return Nothing
        End Function

        Public Function GetRegionalPrices(ByVal CurrencyCode As String) As RegionalPriceInfo
            If _RegionalPrices Is Nothing Then Return Nothing
            For Each r As RegionalPriceInfo In _RegionalPrices
                If r.CurrencyCode.Trim.ToUpper = CurrencyCode.Trim.ToUpper Then Return r
            Next
            Return Nothing
        End Function


        Public Shared Operator =(ByVal a As ServiceInfo, ByVal b As ServiceInfo) As Boolean
            If a Is Nothing AndAlso b Is Nothing Then Return True
            If a Is Nothing OrElse b Is Nothing Then Return False
            Return a.ID = b.ID
        End Operator

        Public Shared Operator <>(ByVal a As ServiceInfo, ByVal b As ServiceInfo) As Boolean
            Return Not a = b
        End Operator

        Public Shared Operator >(ByVal a As ServiceInfo, ByVal b As ServiceInfo) As Boolean
            If a Is Nothing Then Return False
            If a IsNot Nothing And b Is Nothing Then Return True
            Return a.ToString > b.ToString
        End Operator

        Public Shared Operator <(ByVal a As ServiceInfo, ByVal b As ServiceInfo) As Boolean
            If a Is Nothing And b Is Nothing Then Return False
            If a Is Nothing Then Return True
            If b Is Nothing Then Return False
            Return a.ToString < b.ToString
        End Operator

        Public Function CompareTo(ByVal obj As Object) As Integer _
        Implements System.IComparable.CompareTo
            Dim tmp As ServiceInfo = TryCast(obj, ServiceInfo)
            If Me = tmp Then Return 0
            If Me > tmp Then Return 1
            Return -1
        End Function

        Protected Overrides Function GetIdValue() As Object
            Return _ID
        End Function

        Public Overrides Function ToString() As String
            If Not _ID > 0 Then Return ""
            Return _NameShort
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function NewServiceInfo() As ServiceInfo
            Dim result As New ServiceInfo
            result._RegionalContents = RegionalContentInfoList.NewRegionalContentInfoList
            result._RegionalPrices = RegionalPriceInfoList.NewRegionalPriceInfoList
            Return result
        End Function

        Friend Shared Function GetServiceInfo(ByVal dr As DataRow, ByVal contentData As DataTable, _
            ByVal priceData As DataTable) As ServiceInfo
            Return New ServiceInfo(dr, contentData, priceData)
        End Function

        Friend Shared Function GetServiceInfo(ByVal ServiceID As Integer) As ServiceInfo
            Return New ServiceInfo(ServiceID)
        End Function


        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal dr As DataRow, ByVal contentData As DataTable, ByVal priceData As DataTable)
            Fetch(dr, contentData, priceData)
        End Sub

        Private Sub New(ByVal ServiceID As Integer)
            Fetch(ServiceID)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal dr As DataRow, ByVal contentData As DataTable, _
            ByVal priceData As DataTable)

            _ID = CIntSafe(dr.Item(0), 0)
            _Type = ConvertEnumDatabaseCode(Of Documents.TradedItemType)(CIntSafe(dr.Item(1), 0))
            _TypeHumanReadable = ConvertEnumHumanReadable(_Type)
            _NameShort = CStrSafe(dr.Item(2)).Trim
            _Amount = CDblSafe(dr.Item(3), 4, 0)
            _RateVatSales = CDblSafe(dr.Item(4), 2, 0)
            _RateVatPurchase = CDblSafe(dr.Item(5), 2, 0)
            _IsObsolete = ConvertDbBoolean(CIntSafe(dr.Item(6), 0))
            _AccountSales = CLongSafe(dr.Item(7), 0)
            _AccountVatSales = CLongSafe(dr.Item(8), 0)
            _AccountPurchase = CLongSafe(dr.Item(9), 0)
            _AccountVatPurchase = CLongSafe(dr.Item(10), 0)

            If contentData Is Nothing Then
                _RegionalContents = RegionalContentInfoList.GetRegionalContentInfoList(Of Documents.Service)(_ID)
            Else
                _RegionalContents = RegionalContentInfoList.GetRegionalContentInfoList(_ID, contentData)
            End If

            If priceData Is Nothing Then
                _RegionalPrices = RegionalPriceInfoList.GetRegionalPriceInfoList(Of Documents.Service)(_ID)
            Else
                _RegionalPrices = RegionalPriceInfoList.GetRegionalPriceInfoList(_ID, priceData)
            End If

            '_IsInUse = ConvertDbBoolean(CIntSafe(dr.Item(0), 0))

        End Sub

        Private Sub Fetch(ByVal ServiceID As Integer)

            Dim myComm As New SQLCommand("FetchServiceInfo")
            myComm.AddParam("?SD", ServiceID)

            Using myData As DataTable = myComm.Fetch
                If myData.Rows.Count < 1 Then Exit Sub
                Fetch(myData.Rows(0), Nothing, Nothing)
            End Using

        End Sub

#End Region

    End Class

End Namespace