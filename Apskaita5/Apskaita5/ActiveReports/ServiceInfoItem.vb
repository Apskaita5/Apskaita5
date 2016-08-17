Imports ApskaitaObjects.Attributes

Namespace ActiveReports

    ''' <summary>
    ''' Represents an item in a <see cref="ServiceInfoItemList">service report</see>, 
    ''' contains data about a <see cref="Documents.Service">service</see>.
    ''' </summary>
    ''' <remarks>Values are stored in the database table paslaugos.</remarks>
    <Serializable()> _
    Public NotInheritable Class ServiceInfoItem
        Inherits ReadOnlyBase(Of ServiceInfoItem)

#Region " Business Methods "

        Private _ID As Integer = 0
        Private _Type As Documents.TradedItemType = Documents.TradedItemType.All
        Private _TypeHumanReadable As String = Utilities.ConvertLocalizedName(Documents.TradedItemType.All)
        Private _NameShort As String = ""
        Private _Amount As Double = 0
        Private _AccountSales As Long = 0
        Private _RateVatSales As Double = 0
        Private _AccountVatSales As Long = 0
        Private _AccountPurchase As Long = 0
        Private _RateVatPurchase As Double = 0
        Private _AccountVatPurchase As Long = 0
        Private _IsObsolete As Boolean = False
        Private _NameInvoice As String = ""
        Private _MeasureUnit As String = ""


        ''' <summary>
        ''' Gets an ID of the service that is assigned by a database (AUTOINCREMENT).
        ''' </summary>
        ''' <remarks>Value is stored in the database field paslaugos.ID.</remarks>
        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' Gets how the service is used in trade operations (sale, purchase, etc.).
        ''' </summary>
        ''' <remarks>Value is stored in the database field paslaugos.Tip.</remarks>
        Public ReadOnly Property [Type]() As Documents.TradedItemType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Type
            End Get
        End Property

        ''' <summary>
        ''' Gets how the service is used in trade operations (sale, purchase, etc.) 
        ''' as localized human readable string.
        ''' </summary>
        ''' <remarks>Value is stored in the database field paslaugos.Tip.</remarks>
        Public ReadOnly Property TypeHumanReadable() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _TypeHumanReadable.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a short name of the service (as used in dropboxes).
        ''' </summary>
        ''' <remarks>Value is stored in the database field paslaugos.TrPav.</remarks>
        Public ReadOnly Property NameShort() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _NameShort.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a default amount of the service.
        ''' </summary>
        ''' <remarks>Value is stored in the database field paslaugos.Amount.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDAMOUNTINVOICEMADE)> _
        Public ReadOnly Property Amount() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_Amount, ROUNDAMOUNTINVOICEMADE)
            End Get
        End Property

        ''' <summary>
        ''' Gets a default <see cref="General.Account.ID">sales account</see> for the service.
        ''' </summary>
        ''' <remarks>Value is stored in the database field paslaugos.S_Sask.</remarks>
        Public ReadOnly Property AccountSales() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AccountSales
            End Get
        End Property

        ''' <summary>
        ''' Gets a default VAT rate for the service beeing sold.
        ''' </summary>
        ''' <remarks>Value is stored in the database field paslaugos.PVM.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property RateVatSales() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_RateVatSales)
            End Get
        End Property

        ''' <summary>
        ''' Gets a default <see cref="General.Account.ID">sales VAT account</see> for the service.
        ''' </summary>
        ''' <remarks>Value is stored in the database field paslaugos.P_Sask.</remarks>
        Public ReadOnly Property AccountVatSales() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AccountVatSales
            End Get
        End Property

        ''' <summary>
        ''' Gets a default <see cref="General.Account.ID">purchase (costs) account</see> for the service.
        ''' </summary>
        ''' <remarks>Value is stored in the database field paslaugos.AccountPurchase.</remarks>
        Public ReadOnly Property AccountPurchase() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AccountPurchase
            End Get
        End Property

        ''' <summary>
        ''' Gets a default VAT rate for the service beeing purchased.
        ''' </summary>
        ''' <remarks>Value is stored in the database field paslaugos.RateVatPurchase.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property RateVatPurchase() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_RateVatPurchase)
            End Get
        End Property

        ''' <summary>
        ''' Gets a default <see cref="General.Account.ID">purchase VAT account</see> for the service.
        ''' </summary>
        ''' <remarks>Value is stored in the database field paslaugos.AccountVatPurchase.</remarks>
        Public ReadOnly Property AccountVatPurchase() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AccountVatPurchase
            End Get
        End Property

        ''' <summary>
        ''' Gets whether the service is obsolete (no longer in use).
        ''' </summary>
        ''' <remarks>Value is stored in the database field paslaugos.Obsol.</remarks>
        Public ReadOnly Property IsObsolete() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsObsolete
            End Get
        End Property

        ''' <summary>
        ''' Gets a service name in the base language.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property NameInvoice() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _NameInvoice.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a service measure unit in the base language.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property MeasureUnit() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _MeasureUnit.Trim
            End Get
        End Property


        Protected Overrides Function GetIdValue() As Object
            Return _ID
        End Function

        Public Overrides Function ToString() As String
            Return _NameShort
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function GetServiceInfoItem(ByVal dr As DataRow) As ServiceInfoItem
            Return New ServiceInfoItem(dr)
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
            _Type = Utilities.ConvertDatabaseID(Of Documents.TradedItemType)(CIntSafe(dr.Item(1), 0))
            _TypeHumanReadable = Utilities.ConvertLocalizedName(_Type)
            _NameShort = CStrSafe(dr.Item(2)).Trim
            _Amount = CDblSafe(dr.Item(3), 4, 0)
            _RateVatSales = CDblSafe(dr.Item(4), 2, 0)
            _RateVatPurchase = CDblSafe(dr.Item(5), 2, 0)
            _IsObsolete = ConvertDbBoolean(CIntSafe(dr.Item(6), 0))
            _AccountSales = CLongSafe(dr.Item(7), 0)
            _AccountVatSales = CLongSafe(dr.Item(8), 0)
            _AccountPurchase = CLongSafe(dr.Item(9), 0)
            _AccountVatPurchase = CLongSafe(dr.Item(10), 0)
            _NameInvoice = CStrSafe(dr.Item(11)).Trim
            _MeasureUnit = CStrSafe(dr.Item(12)).Trim

        End Sub

#End Region

    End Class

End Namespace