Imports ApskaitaObjects.Documents
Imports ApskaitaObjects.Attributes

Namespace ActiveReports

    ''' <summary>
    ''' Represents an item in a <see cref="ServiceTurnoverInfoList">service turnover report</see>,
    ''' contains information about a service, it's sales and purchases (as invoiced) 
    ''' over the report period.
    ''' </summary>
    ''' <remarks>Should only be used as a child of <see cref="ServiceTurnoverInfoList">ServiceTurnoverInfoList</see>.
    ''' It is unclear what could be a meaning of debit invoices with negative sum???</remarks>
    <Serializable()> _
    Public NotInheritable Class ServiceTurnoverInfo
        Inherits ReadOnlyBase(Of ServiceTurnoverInfo)

#Region " Business Methods "

        Private _ID As Integer = 0
        Private _Name As String = ""
        Private _TradedTypeInt As TradedItemType = TradedItemType.All
        Private _TradedType As String = ""
        Private _AccountSales As Long = 0
        Private _AccountPurchase As Long = 0
        Private _DefaultRateVatSales As Double = 0
        Private _DefaultRateVatPurchase As Double = 0
        Private _AccountVatPurchase As Long = 0
        Private _ServiceCode As String = ""
        Private _IsObsolete As Boolean = False
        Private _PurchasedAmount As Double = 0
        Private _PurchasedSum As Double = 0
        Private _PurchasedAmountReturned As Double = 0
        Private _PurchasedSumReturned As Double = 0
        Private _PurchasedSumReductions As Double = 0
        Private _SoldAmount As Double = 0
        Private _SoldSum As Double = 0
        Private _SoldAmountReturned As Double = 0
        Private _SoldSumReturned As Double = 0
        Private _SoldSumReductions As Double = 0
        Private _SoldSumDiscounts As Double = 0


        ''' <summary>
        ''' Gets an ID of the service that is assigned by a database (AUTOINCREMENT).
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="Service.ID">Service.ID</see>.</remarks>
        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' Gets a name of the service.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="Service.NameShort">Service.NameShort</see>.</remarks>
        Public ReadOnly Property Name() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Name.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets how the service is used in trade operations (sale, purchase, etc.).
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="Service.Type">Service.Type</see>.</remarks>
        Friend ReadOnly Property TradedTypeInt() As TradedItemType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _TradedTypeInt
            End Get
        End Property

        ''' <summary>
        ''' Gets how the service is used in trade operations (sale, purchase, etc.)
        ''' as a localized human readable string.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="Service.Type">Service.Type</see>.</remarks>
        Public ReadOnly Property TradedType() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _TradedType.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a default <see cref="General.Account.ID">sales account</see> for the service.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="Service.AccountSales">Service.AccountSales</see>.</remarks>
        Public ReadOnly Property AccountSales() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AccountSales
            End Get
        End Property

        ''' <summary>
        ''' Gets a default <see cref="General.Account.ID">purchase (costs) account</see> for the service.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="Service.AccountPurchase">Service.AccountPurchase</see>.</remarks>
        Public ReadOnly Property AccountPurchase() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AccountPurchase
            End Get
        End Property

        ''' <summary>
        ''' Gets a default VAT rate for the service beeing sold.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="Service.RateVatSales">Service.RateVatSales</see>.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property DefaultRateVatSales() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_DefaultRateVatSales, 2)
            End Get
        End Property

        ''' <summary>
        ''' Gets a default VAT rate for the service beeing purchased.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="Service.RateVatPurchase">Service.RateVatPurchase</see>.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property DefaultRateVatPurchase() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_DefaultRateVatPurchase, 2)
            End Get
        End Property

        ''' <summary>
        ''' Gets a default <see cref="General.Account.ID">purchase VAT account</see> for the service.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="Service.AccountVatPurchase">Service.AccountVatPurchase</see>.</remarks>
        Public ReadOnly Property AccountVatPurchase() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AccountVatPurchase
            End Get
        End Property

        ''' <summary>
        ''' Gets an internal code of the service (as used in the company).
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="Service.ServiceCode">Service.ServiceCode</see>.</remarks>
        Public ReadOnly Property ServiceCode() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ServiceCode.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets whether the service is obsolete (no longer in use).
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="Service.IsObsolete">Service.IsObsolete</see>.</remarks>
        Public ReadOnly Property IsObsolete() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsObsolete
            End Get
        End Property

        ''' <summary>
        ''' Gets a total amount of services purchased over the report period.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDAMOUNTINVOICERECEIVED)> _
        Public ReadOnly Property PurchasedAmount() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_PurchasedAmount, ROUNDAMOUNTINVOICERECEIVED)
            End Get
        End Property

        ''' <summary>
        ''' Gets a total value of services purchased over the report period (excluding VAT).
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property PurchasedSum() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_PurchasedSum, 2)
            End Get
        End Property

        ''' <summary>
        ''' Gets a total amount of services 'returned' from clients over the report period.
        ''' </summary>
        ''' <remarks>Services purchased could be 'returned to a supplier' 
        ''' by a credit invoice received with a negative amount or a debit invoice made.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDAMOUNTINVOICERECEIVED)> _
        Public ReadOnly Property PurchasedAmountReturned() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_PurchasedAmountReturned, ROUNDAMOUNTINVOICERECEIVED)
            End Get
        End Property

        ''' <summary>
        ''' Gets a total value of services 'returned' from clients over the report period (excluding VAT).
        ''' </summary>
        ''' <remarks>''' <summary>
        ''' Gets a total value of services refunded to clients over the report period.
        ''' </summary>
        ''' <remarks>Services purchased could be 'returned to a supplier' 
        ''' by a credit invoice received with a negative amount or a debit invoice made.</remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property PurchasedSumReturned() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_PurchasedSumReturned, 2)
            End Get
        End Property

        ''' <summary>
        ''' Gets a total value of services refunded to clients over the report period.
        ''' </summary>
        ''' <remarks>Value of services purchased could be refunded by a credit invoice received
        ''' with negative unit value or a debit invoice made.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property PurchasedSumReductions() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_PurchasedSumReductions, 2)
            End Get
        End Property

        ''' <summary>
        ''' Gets a total amount of services sold over the report period.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDAMOUNTINVOICEMADE)> _
        Public ReadOnly Property SoldAmount() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_SoldAmount, ROUNDAMOUNTINVOICEMADE)
            End Get
        End Property

        ''' <summary>
        ''' Gets a total value of services sold over the report period (excluding VAT).
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property SoldSum() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_SoldSum, 2)
            End Get
        End Property

        ''' <summary>
        ''' Gets a total amount of services 'returned' to suppliers over the report period.
        ''' </summary>
        ''' <remarks>Services sold could be 'returned by a client' 
        ''' by a credit invoice made with a negative amount or a debit invoice received.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDAMOUNTINVOICEMADE)> _
        Public ReadOnly Property SoldAmountReturned() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_SoldAmountReturned, ROUNDAMOUNTINVOICEMADE)
            End Get
        End Property

        ''' <summary>
        ''' Gets a total value of services 'returned' to suppliers over the report period (excluding VAT).
        ''' </summary>
        ''' <remarks>Services sold could be 'returned by a client' 
        ''' by a credit invoice made with a negative amount or a debit invoice received.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property SoldSumReturned() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_SoldSumReturned, 2)
            End Get
        End Property

        ''' <summary>
        ''' Gets a total value of services refunded by suppliers over the report period.
        ''' </summary>
        ''' <remarks>Value of services sold could be refunded by a credit invoice made
        ''' with a negative unit value or a debit invoice received.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property SoldSumReductions() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_SoldSumReductions, 2)
            End Get
        End Property

        ''' <summary>
        ''' Gets a total value of discounts applied to services sold over the report period (excluding VAT).
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property SoldSumDiscounts() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_SoldSumDiscounts, 2)
            End Get
        End Property


        ''' <summary>
        ''' Whether the service has any turnover for the report period,
        ''' i.e. any turnover value is not zero.
        ''' </summary>
        ''' <remarks></remarks>
        Public Function HasTurnover() As Boolean
            Return (CRound(_PurchasedAmount, ROUNDAMOUNTINVOICERECEIVED) > 0 OrElse _
                CRound(_PurchasedAmountReturned, ROUNDAMOUNTINVOICERECEIVED) > 0 OrElse _
                CRound(_SoldAmount, ROUNDAMOUNTINVOICEMADE) > 0 OrElse _
                CRound(_SoldAmountReturned, ROUNDAMOUNTINVOICEMADE) > 0 OrElse _
                CRound(_PurchasedSum, 2) > 0 OrElse CRound(_PurchasedSumReductions, 2) > 0 OrElse _
                CRound(_PurchasedSumReturned, 2) > 0 OrElse CRound(_SoldSum, 2) > 0 OrElse _
                CRound(_SoldSumDiscounts, 2) > 0 OrElse CRound(_SoldSumReductions, 2) > 0 OrElse _
                CRound(_SoldSumReturned, 2) > 0)
        End Function

        ''' <summary>
        ''' Whether the service trade type mathes filter trade type value.
        ''' </summary>
        ''' <param name="testType">filter trade type value</param>
        ''' <remarks></remarks>
        Public Function MatchesTradedType(ByVal testType As TradedItemType) As Boolean

            If testType = TradedItemType.All OrElse _TradedTypeInt = TradedItemType.All Then

                Return True

            Else

                Return (testType = _TradedTypeInt)

            End If

        End Function


        Protected Overrides Function GetIdValue() As Object
            Return _ID
        End Function

        Public Overrides Function ToString() As String
            Return _Name
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function GetServiceTurnoverInfo(ByVal dr As DataRow) As ServiceTurnoverInfo
            Return New ServiceTurnoverInfo(dr)
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
            _Name = CStrSafe(dr.Item(1)).Trim
            _TradedTypeInt = Utilities.ConvertDatabaseID(Of TradedItemType) _
                (CIntSafe(dr.Item(2), 0))
            _TradedType = Utilities.ConvertLocalizedName(_TradedTypeInt)
            _DefaultRateVatSales = CDblSafe(dr.Item(3), 2, 0)
            _DefaultRateVatPurchase = CDblSafe(dr.Item(4), 2, 0)
            _ServiceCode = CStrSafe(dr.Item(5)).Trim
            _IsObsolete = ConvertDbBoolean(CIntSafe(dr.Item(6), 0))
            _AccountSales = CLongSafe(dr.Item(7), 0)
            _AccountPurchase = CLongSafe(dr.Item(8), 0)
            _AccountVatPurchase = CLongSafe(dr.Item(9), 0)
            _PurchasedAmount = CDblSafe(dr.Item(10), ROUNDAMOUNTINVOICERECEIVED, 0)
            _PurchasedSum = CDblSafe(dr.Item(11), 2, 0)
            _PurchasedAmountReturned = CDblSafe(dr.Item(12), ROUNDAMOUNTINVOICERECEIVED, 0)
            _PurchasedSumReturned = CDblSafe(dr.Item(13), 2, 0)
            _PurchasedSumReductions = CDblSafe(dr.Item(14), 2, 0)
            _SoldAmount = CDblSafe(dr.Item(15), ROUNDAMOUNTINVOICEMADE, 0)
            _SoldSum = CDblSafe(dr.Item(16), 2, 0)
            _SoldAmountReturned = CDblSafe(dr.Item(17), ROUNDAMOUNTINVOICEMADE, 0)
            _SoldSumReturned = CDblSafe(dr.Item(18), 2, 0)
            _SoldSumReductions = CDblSafe(dr.Item(19), 2, 0)
            _SoldSumDiscounts = CDblSafe(dr.Item(20), 2, 0)

        End Sub

#End Region

    End Class

End Namespace