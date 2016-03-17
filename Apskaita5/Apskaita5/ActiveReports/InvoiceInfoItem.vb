
Imports ApskaitaObjects.Attributes

Namespace ActiveReports

    ''' <summary>
    ''' Represents report information about an <see cref="Documents.InvoiceMade">InvoiceMade</see>
    ''' or an <see cref="Documents.InvoiceReceived">InvoiceReceived</see>.
    ''' </summary>
    ''' <remarks>Should only be used as a child of <see cref="InvoiceInfoItemList">InvoiceInfoItemList</see>.</remarks>
    <Serializable()> _
    Public Class InvoiceInfoItem
        Inherits ReadOnlyBase(Of InvoiceInfoItem)

#Region " Business Methods "

        Private _ID As Integer = 0
        Private _Type As InvoiceInfoType = InvoiceInfoType.InvoiceMade
        Private _PersonID As Integer = 0
        Private _PersonName As String = ""
        Private _PersonCode As String = ""
        Private _PersonVatCode As String = ""
        Private _PersonEmail As String = ""
        Private _PersonAccount As Long = 0
        Private _Date As Date = Today
        Private _Number As String = ""
        Private _Content As String = ""
        Private _CurrencyCode As String = ""
        Private _CurrencyRate As Double = 0
        Private _LanguageName As String = ""
        Private _CommentsInternal As String = ""
        Private _VatRate As Double = 0
        Private _Sum As Double = 0
        Private _SumVat As Double = 0
        Private _TotalSum As Double = 0
        Private _SumLTL As Double = 0
        Private _SumVatLTL As Double = 0
        Private _TotalSumLTL As Double = 0
        Private _SumDiscount As Double = 0
        Private _SumVatDiscount As Double = 0
        Private _TotalSumDiscount As Double = 0
        Private _SumDiscountLTL As Double = 0
        Private _SumVatDiscountLTL As Double = 0
        Private _TotalSumDiscountLTL As Double = 0
        Private _SumVatVirtual As Double = 0
        Private _SumVatLTLVirtual As Double = 0


        ''' <summary>
        ''' Gets an ID of the invoice.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="Documents.InvoiceMade.ID">InvoiceMade.ID</see>
        ''' or <see cref="Documents.InvoiceReceived.ID">InvoiceReceived.ID</see> properties.
        ''' Matches <see cref="General.JournalEntry.ID">the ID 
        ''' of the journal entry</see> that is created by the invoice.</remarks>
        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' Gets a <see cref="InvoiceInfoType">type of the invoice</see> (made or received).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Type() As InvoiceInfoType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Type
            End Get
        End Property

        ''' <summary>
        ''' Gets an ID of the person in the invoice.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="Documents.InvoiceMade.Payer">InvoiceMade.Payer</see>
        ''' or <see cref="Documents.InvoiceReceived.Supplier">InvoiceReceived.Supplier</see> properties.</remarks>
        Public ReadOnly Property PersonID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _PersonID
            End Get
        End Property

        ''' <summary>
        ''' Gets a name of the person in the invoice.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="Documents.InvoiceMade.Payer">InvoiceMade.Payer</see>
        ''' or <see cref="Documents.InvoiceReceived.Supplier">InvoiceReceived.Supplier</see> properties.</remarks>
        Public ReadOnly Property PersonName() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _PersonName.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets an official registration code of the person in the invoice.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="Documents.InvoiceMade.Payer">InvoiceMade.Payer</see>
        ''' or <see cref="Documents.InvoiceReceived.Supplier">InvoiceReceived.Supplier</see> properties.</remarks>
        Public ReadOnly Property PersonCode() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _PersonCode.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a VAT code of the person in the invoice.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="Documents.InvoiceMade.Payer">InvoiceMade.Payer</see>
        ''' or <see cref="Documents.InvoiceReceived.Supplier">InvoiceReceived.Supplier</see> properties.</remarks>
        Public ReadOnly Property PersonVatCode() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _PersonVatCode.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets an email address of the person in the invoice.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="Documents.InvoiceMade.Payer">InvoiceMade.Payer</see>
        ''' or <see cref="Documents.InvoiceReceived.Supplier">InvoiceReceived.Supplier</see> properties.</remarks>
        Public ReadOnly Property PersonEmail() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _PersonEmail.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets the <see cref="General.Account.ID">account</see> that is debited/credited
        ''' by the total sum receivable/the total sum payable from/to the person in the invoice.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="Documents.InvoiceMade.AccountPayer">InvoiceMade.AccountPayer</see>
        ''' or <see cref="Documents.InvoiceReceived.AccountSupplier">InvoiceReceived.AccountSupplier</see> properties.</remarks>
        <AccountField(ValueRequiredLevel.Optional, False, 1, 2, 3, 4, 5, 6)> _
        Public ReadOnly Property PersonAccount() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _PersonAccount
            End Get
        End Property

        ''' <summary>
        ''' Gets a date of the invoice.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="Documents.InvoiceMade.Date">InvoiceMade.Date</see>
        ''' or <see cref="Documents.InvoiceReceived.Date">InvoiceReceived.Date</see> properties.</remarks>
        Public ReadOnly Property [Date]() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Date
            End Get
        End Property

        ''' <summary>
        ''' Gets a serial and number (concetanated) of the invoice.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="Documents.InvoiceMade.FullNumber">InvoiceMade.FullNumber</see>
        ''' or <see cref="Documents.InvoiceReceived.Number">InvoiceReceived.Number</see> properties.</remarks>
        Public ReadOnly Property Number() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Number.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a content (description) of the invoice.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="Documents.InvoiceMade.Content">InvoiceMade.Content</see>
        ''' or <see cref="Documents.InvoiceReceived.Content">InvoiceReceived.Content</see> properties.</remarks>
        Public ReadOnly Property Content() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Content.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a code of the original currency of the invoice.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="Documents.InvoiceMade.CurrencyCode">InvoiceMade.CurrencyCode</see>
        ''' or <see cref="Documents.InvoiceReceived.CurrencyCode">InvoiceReceived.CurrencyCode</see> properties.</remarks>
        Public ReadOnly Property CurrencyCode() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _CurrencyCode.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a rate of the original currency of the invoice.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="Documents.InvoiceMade.CurrencyRate">InvoiceMade.CurrencyRate</see>
        ''' or <see cref="Documents.InvoiceReceived.CurrencyRate">InvoiceReceived.CurrencyRate</see> properties.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, ROUNDCURRENCYRATE)> _
        Public ReadOnly Property CurrencyRate() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_CurrencyRate, ROUNDCURRENCYRATE)
            End Get
        End Property

        ''' <summary>
        ''' Gets an original language of the invoice.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="Documents.InvoiceMade.LanguageName">InvoiceMade.LanguageName</see>
        ''' property. Equals base language for an InvoiceReceived.</remarks>
        Public ReadOnly Property LanguageName() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _LanguageName.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets internal comments (for an accountant) of the invoice.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="Documents.InvoiceMade.CommentsInternal">InvoiceMade.CommentsInternal</see>
        ''' or <see cref="Documents.InvoiceReceived.CommentsInternal">InvoiceReceived.CommentsInternal</see> properties.</remarks>
        Public ReadOnly Property CommentsInternal() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _CommentsInternal.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a maximum VAT rate in the invoice.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="Documents.InvoiceMadeItem.VatRate">InvoiceMadeItem.VatRate</see>
        ''' or <see cref="Documents.InvoiceReceivedItem.VatRate">InvoiceReceivedItem.VatRate</see> properties.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property VatRate() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_VatRate)
            End Get
        End Property

        ''' <summary>
        ''' Gets a total sum of the invoice in the invoice original currency 
        ''' including discount, excluding VAT.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property Sum() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_Sum)
            End Get
        End Property

        ''' <summary>
        ''' Gets a total VAT sum of the invoice in the invoice original currency 
        ''' including discount VAT.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property SumVat() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_SumVat)
            End Get
        End Property

        ''' <summary>
        ''' Gets a total sum of the invoice in the invoice original currency
        ''' including discount and VAT.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property TotalSum() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_TotalSum)
            End Get
        End Property

        ''' <summary>
        ''' Gets a total sum of the invoice in the base currency 
        ''' including discount, excluding VAT.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property SumLTL() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_SumLTL)
            End Get
        End Property

        ''' <summary>
        ''' Gets a total VAT sum of the invoice in the base currency 
        ''' including discount VAT.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property SumVatLTL() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_SumVatLTL)
            End Get
        End Property

        ''' <summary>
        ''' Gets a total sum of the invoice in the base currency
        ''' including discount and VAT.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property TotalSumLTL() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_TotalSumLTL)
            End Get
        End Property

        ''' <summary>
        ''' Gets a discount sum in the invoice in the invoice original currency excluding VAT.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property SumDiscount() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_SumDiscount)
            End Get
        End Property

        ''' <summary>
        ''' Gets a discount VAT sum in the invoice in the invoice original currency.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property SumVatDiscount() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_SumVatDiscount)
            End Get
        End Property

        ''' <summary>
        ''' Gets a total discount sum in the invoice in the invoice original currency 
        ''' including VAT.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property TotalSumDiscount() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_TotalSumDiscount)
            End Get
        End Property

        ''' <summary>
        ''' Gets a discount sum in the invoice in the base currency excluding VAT.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property SumDiscountLTL() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_SumDiscountLTL)
            End Get
        End Property

        ''' <summary>
        ''' Gets a discount VAT sum in the invoice in the base currency.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property SumVatDiscountLTL() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_SumVatDiscountLTL)
            End Get
        End Property

        ''' <summary>
        ''' Gets a total discount sum in the invoice in the base currency including VAT.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property TotalSumDiscountLTL() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_TotalSumDiscountLTL)
            End Get
        End Property

        ''' <summary>
        ''' Gets a total virtual (indirect) VAT sum in the invoice 
        ''' in the invoice original currency.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property SumVatVirtual() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_SumVatVirtual)
            End Get
        End Property

        ''' <summary>
        ''' Gets a total virtual (indirect) VAT sum in the invoice in the base currency.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="Documents.InvoiceMadeItem.VatIsVirtual">InvoiceMadeItem.VatIsVirtual</see>
        ''' or <see cref="Documents.InvoiceReceived.IndirectVatSum">InvoiceReceived.IndirectVatSum</see> properties.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property SumVatLTLVirtual() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_SumVatLTLVirtual)
            End Get
        End Property


        Protected Overrides Function GetIdValue() As Object
            Return _ID
        End Function

        Public Overrides Function ToString() As String
            Return String.Format(My.Resources.ActiveReports_InvoiceInfoItem_ToString, _
                _Date.ToString("yyyy-MM-dd"), _Number, _Content)
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function GetInvoiceInfoItem(ByVal dr As DataRow, _
            ByVal nType As InvoiceInfoType) As InvoiceInfoItem
            Return New InvoiceInfoItem(dr, nType)
        End Function

        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal dr As DataRow, ByVal nType As InvoiceInfoType)
            Fetch(dr, nType)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal dr As DataRow, ByVal nType As InvoiceInfoType)

            _ID = CIntSafe(dr.Item(0), 0)
            _Type = nType
            _PersonID = CIntSafe(dr.Item(1), 0)
            _PersonName = CStrSafe(dr.Item(2)).Trim
            _PersonCode = CStrSafe(dr.Item(3)).Trim
            _PersonVatCode = CStrSafe(dr.Item(4)).Trim
            _PersonEmail = CStrSafe(dr.Item(5)).Trim
            _Date = CDateSafe(dr.Item(6), Today)
            _Number = CStrSafe(dr.Item(7)).Trim
            _Content = CStrSafe(dr.Item(8)).Trim
            _CurrencyCode = CStrSafe(dr.Item(9)).Trim
            _CurrencyRate = CDblSafe(dr.Item(10), 6, 0)
            _LanguageName = GetLanguageName(CStrSafe(dr.Item(11)).Trim, False)
            _CommentsInternal = CStrSafe(dr.Item(12)).Trim
            _VatRate = CDblSafe(dr.Item(13), 2, 0)

            _SumDiscount = CDblSafe(dr.Item(18), 2, 0)
            _SumVatDiscount = CDblSafe(dr.Item(19), 2, 0)
            _TotalSumDiscount = CRound(_SumDiscount + _SumVatDiscount)
            _SumDiscountLTL = CDblSafe(dr.Item(20), 2, 0)
            _SumVatDiscountLTL = CDblSafe(dr.Item(21), 2, 0)
            _TotalSumDiscountLTL = CRound(_SumDiscountLTL + _SumVatDiscountLTL)

            _Sum = CRound(CDblSafe(dr.Item(14), 2, 0) - _SumDiscount)
            _SumVat = CRound(CDblSafe(dr.Item(15), 2, 0) - _SumVatDiscount)
            _TotalSum = CRound(_Sum + _SumVat)
            _SumLTL = CRound(CDblSafe(dr.Item(16), 2, 0) - _SumDiscountLTL)
            _SumVatLTL = CRound(CDblSafe(dr.Item(17), 2, 0) - _SumVatDiscountLTL)
            _TotalSumLTL = CRound(_SumLTL + _SumVatLTL)

            _PersonAccount = CLongSafe(dr.Item(22), 0)
            _SumVatVirtual = CDblSafe(dr.Item(23), 2, 0)
            _SumVatLTLVirtual = CDblSafe(dr.Item(24), 2, 0)

        End Sub

#End Region

    End Class

End Namespace