Imports System.Xml.Serialization

<Serializable()> _
Public Class InvoiceInfo

    #Region " Private Backing Fields "

    Private _ID As String = String.Empty
    Private _ProjectCode As String = String.Empty
    Private _Payer As ClientInfo = New ClientInfo
    Private _Date As Date = Today.Date
    Private _Serial As String = String.Empty
    Private _Number As Integer = 0
    Private _FullNumber As String = String.Empty
    Private _Content As String = String.Empty
    Private _CurrencyCode As String = String.Empty
    Private _CurrencyRate As Double = 0.0
    Private _LanguageCode As String = String.Empty
    Private _VatExemptions As String = String.Empty
    Private _VatExemptionsAltLng As String = String.Empty
    Private _CustomInfo As String = String.Empty
    Private _CustomInfoAltLng As String = String.Empty
    Private _CommentsInternal As String = String.Empty
    Private _SumLTL As Double = 0.0
    Private _SumVatLTL As Double = 0.0
    Private _SumTotalLTL As Double = 0.0
    Private _Sum As Double = 0.0
    Private _SumVat As Double = 0.0
    Private _SumTotal As Double = 0.0
    Private _DiscountLTL As Double = 0.0
    Private _DiscountVatLTL As Double = 0.0
    Private _Discount As Double = 0.0
    Private _DiscountVat As Double = 0.0
    Private _SumReceived As Double = 0.0
    Private _AddDateToNumberOptionWasUsed As Boolean = False
    Private _NumbersInInvoice As Integer = 0
    Private _ExternalID As String = String.Empty
    Private _SystemGuid As String = Guid.NewGuid.ToString
    Private _UpdateDate As DateTime = Now
    Private _InvoiceItems As New List(Of InvoiceItemInfo)

#End Region


    ''' <summary>
    ''' Gets or sets an ID of the invoice in the source system.
    ''' </summary>
    ''' <remarks>Required.
    ''' Used to determine whether the invoice has been already imported
    ''' and needs to be updated or the invoice should be added as a new one.
    ''' As the ids are commonly defined as integers, it is recommended to prefix
    ''' them with a system id in order to differentiate between multiple source systems.</remarks>
    Public Property ID() As String
        Get
            Return _ID.Trim
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then value = ""
            If _ID.Trim <> value.Trim Then
                _ID = value.Trim
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a relevant project code (if the source system supports such a concept).
    ''' </summary>
    ''' <returns>Optional.</returns>
    Public Property ProjectCode() As String
        Get
            Return _ProjectCode.Trim
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then value = ""
            If _ProjectCode.Trim <> value.Trim Then
                _ProjectCode = value.Trim
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets the issuer of the invoice (client or supplier).
    ''' </summary>
    ''' <remarks>Required.
    ''' Cannot change the property name to an appropriate one due to backwards compatibility.</remarks>
    Public Property Payer() As ClientInfo
        Get
            Return _Payer
        End Get
        Set(ByVal value As ClientInfo)
            If Not value Is Nothing Then
                _Payer = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a date of the invoice.
    ''' </summary>
    ''' <remarks>Required.
    ''' Serialized as DateString element.</remarks>
    <XmlIgnore()> _
    Public Property [Date]() As DateTime
        Get
            Return _Date
        End Get
        Set(ByVal value As DateTime)
            If _Date <> value Then
                _Date = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' For the serialization only. Do not use this property.
    ''' </summary>
    Public Property DateString() As String
        Get
            Return _Date.ToString(Globalization.CultureInfo.InvariantCulture)
        End Get
        Set(ByVal value As String)
            If value Is Nothing OrElse String.IsNullOrEmpty(value.Trim) Then Exit Property
            Dim newDate As Date = Date.Parse(value, Globalization.CultureInfo.InvariantCulture)
            If _Date <> newDate Then
                _Date = newDate
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a serial of the invoice.
    ''' </summary>
    ''' <remarks>Only required for invoices made.</remarks>
    Public Property Serial() As String
        Get
            Return _Serial.Trim
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then value = ""
            If _Serial.Trim <> value.Trim Then
                _Serial = value.Trim
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a sequential No of the invoice.
    ''' </summary>
    ''' <remarks>Only required for invoices made.</remarks>
    Public Property Number() As Integer
        Get
            Return _Number
        End Get
        Set(ByVal value As Integer)
            If _Number <> value Then
                _Number = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a formatted (full) invoice No (as printed on the invoice).
    ''' </summary>
    ''' <remarks>Required.</remarks>
    Public Property FullNumber() As String
        Get
            Return _FullNumber.Trim
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then value = ""
            If _FullNumber.Trim <> value.Trim Then
                _FullNumber = value.Trim
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a description of the invoice (e. g. as described in invoice register/index).
    ''' </summary>
    ''' <remarks>Optional.</remarks>
    Public Property Content() As String
        Get
            Return _Content.Trim
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then value = ""
            If _Content.Trim <> value.Trim Then
                _Content = value.Trim
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets an ISO4217 currency code (3-letter) for the original invoice currency.
    ''' </summary>
    ''' <remarks>Optional.
    ''' Empty or null string corresponds to the base currency of the target system.</remarks>
    Public Property CurrencyCode() As String
        Get
            Return _CurrencyCode.Trim
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then value = ""
            If _CurrencyCode.Trim <> value.Trim Then
                _CurrencyCode = value.Trim
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a currency rate (if the invoice original currency is different
    ''' from the target system base currency).
    ''' </summary>
    ''' <remarks>Required if the invoice original currency is different
    ''' from the target system base currency.</remarks>
    Public Property CurrencyRate() As Double
        Get
            Return _CurrencyRate
        End Get
        Set(ByVal value As Double)
            If _CurrencyRate <> value Then
                _CurrencyRate = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets an ISO 639-1 language code (2-letter) for the original language
    ''' that the invoice was issued in.
    ''' </summary>
    ''' <remarks>Optional. (defaults to base language for the target system)
    ''' Only applicable for invoices made.</remarks>
    Public Property LanguageCode() As String
        Get
            Return _LanguageCode.Trim
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then value = ""
            If _LanguageCode.Trim <> value.Trim Then
                _LanguageCode = value.Trim
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a description of the VAT exempts applicable for the invoice
    ''' (e.g. reverse charge) in the base language of the target system.
    ''' </summary>
    ''' <remarks>Optional.
    ''' Only applicable for invoices made.</remarks>
    Public Property VatExemptions() As String
        Get
            Return _VatExemptions.Trim
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then value = ""
            If _VatExemptions.Trim <> value.Trim Then
                _VatExemptions = value.Trim
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a description of the VAT exempts applicable for the invoice
    ''' (e.g. reverse charge) in the original invoice language.
    ''' </summary>
    ''' <remarks>Only required if the <see cref="VatExemptions"/> are specified
    ''' and <see cref="LanguageCode">the original invoice language</see> does not match
    ''' the base language of the target system.
    ''' Only applicable for invoices made.</remarks>
    Public Property VatExemptionsAltLng() As String
        Get
            Return _VatExemptionsAltLng.Trim
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then value = ""
            If _VatExemptionsAltLng.Trim <> value.Trim Then
                _VatExemptionsAltLng = value.Trim
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets arbitrary custom info about the invoice in the base language of the target system
    ''' that shall be "printed" in the invoice.
    ''' </summary>
    ''' <remarks>Optional.
    ''' Only applicable for invoices made.</remarks>
    Public Property CustomInfo() As String
        Get
            Return _CustomInfo.Trim
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then value = ""
            If _CustomInfo.Trim <> value.Trim Then
                _CustomInfo = value.Trim
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets arbitrary custom info about the invoice
    ''' in <see cref="LanguageCode">the original invoice language</see>
    ''' that shall be "printed" in the invoice.
    ''' </summary>
    ''' <remarks>Only required if the <see cref="CustomInfo"/> is specified
    ''' and <see cref="LanguageCode">the original invoice language</see> does not match
    ''' the base language of the target system.
    ''' Only applicable for invoices made.</remarks>
    Public Property CustomInfoAltLng() As String
        Get
            Return _CustomInfoAltLng.Trim
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then value = ""
            If _CustomInfoAltLng.Trim <> value.Trim Then
                _CustomInfoAltLng = value.Trim
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets arbitrary info about the invoice for an accountant
    ''' (that shall NEVER be "printed" in the invoice).
    ''' </summary>
    ''' <remarks>Optional.</remarks>
    Public Property CommentsInternal() As String
        Get
            Return _CommentsInternal.Trim
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then value = ""
            If _CommentsInternal.Trim <> value.Trim Then
                _CommentsInternal = value.Trim
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a subtotal value (excl. VAT) of the goods/services sold/purchased in base currency.
    ''' </summary>       
    ''' <remarks>Required.
    ''' Could be negative (aka credit note).
    ''' If discount values are used, it shall reflect a subtotal value BEFORE the discount.</remarks>
    Public Property SumLTL() As Double
        Get
            Return _SumLTL
        End Get
        Set(ByVal value As Double)
            If _SumLTL <> value Then
                _SumLTL = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a VAT amount in base currency.
    ''' </summary>
    ''' <remarks>Optional (depends on respective values in the invoice items).
    ''' Could be negative (aka credit note).
    ''' If discount values are used, it shall reflect a total VAT value BEFORE the discount.</remarks>
    Public Property SumVatLTL() As Double
        Get
            Return _SumVatLTL
        End Get
        Set(ByVal value As Double)
            If _SumVatLTL <> value Then
                _SumVatLTL = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a total value (incl. VAT) of the goods/services sold/purchased in base currency.
    ''' </summary> 
    ''' <remarks>Required.
    ''' Could be negative (aka credit note).
    ''' If discount values are used, it shall reflect a total value BEFORE the discount.</remarks>
    Public Property SumTotalLTL() As Double
        Get
            Return _SumTotalLTL
        End Get
        Set(ByVal value As Double)
            If _SumTotalLTL <> value Then
                _SumTotalLTL = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a subtotal (excl. VAT) of the goods/services sold/purchased
    ''' in original invoice currency (see <see cref="CurrencyCode"/>).
    ''' </summary>      
    ''' <remarks>Required.
    ''' Could be negative (aka credit note).
    ''' If discount values are used, it shall reflect a subtotal value BEFORE the discount.</remarks>
    Public Property Sum() As Double
        Get
            Return _Sum
        End Get
        Set(ByVal value As Double)
            If _Sum <> value Then
                _Sum = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a VAT amount in original invoice currency (see <see cref="CurrencyCode"/>).
    ''' </summary> 
    ''' <remarks>Optional (depends on respective values in the invoice items).
    ''' Could be negative (aka credit note).
    ''' If discount values are used, it shall reflect a total VAT value BEFORE the discount.</remarks>
    Public Property SumVat() As Double
        Get
            Return _SumVat
        End Get
        Set(ByVal value As Double)
            If _SumVat <> value Then
                _SumVat = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a total value (incl. VAT) of the goods/services sold/purchased
    ''' in original invoice currency (see <see cref="InvoiceInfo.CurrencyCode"/>).
    ''' </summary> 
    ''' <remarks>Required.
    ''' Could be negative (aka credit note).
    ''' Depends on the <see cref="Sum"/> and <see cref="SumVat"/>.
    ''' If discount values are used, it shall reflect a total value BEFORE the discount.</remarks>
    Public Property SumTotal() As Double
        Get
            Return _SumTotal
        End Get
        Set(ByVal value As Double)
            If _SumTotal <> value Then
                _SumTotal = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a discount value (excl. VAT) for the goods/services sold/purchased
    ''' in base currency.
    ''' </summary> 
    ''' <remarks>Optional.
    ''' Only applicable for invoices made.
    ''' Cannot be negative.
    ''' If used, the discount amount should be included in <see cref="SumLTL"/>
    ''' so that amount payable (excl. VAT) in base currency equals <see cref="SumLTL"/>
    ''' minus DiscountLTL.</remarks>
    Public Property DiscountLTL() As Double
        Get
            Return _DiscountLTL
        End Get
        Set(ByVal value As Double)
            If _DiscountLTL <> value Then
                _DiscountLTL = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a VAT amount per discount in base currency.
    ''' </summary> 
    ''' <remarks>Only applicable to invoices made.
    ''' If used, the discount VAT amount should be included in <see cref="SumVatLTL"/>
    ''' so that actual VAT amount in base currency equals <see cref="SumVatLTL"/>
    ''' minus DiscountVatLTL.</remarks>
    Public Property DiscountVatLTL() As Double
        Get
            Return _DiscountVatLTL
        End Get
        Set(ByVal value As Double)
            If _DiscountVatLTL <> value Then
                _DiscountVatLTL = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a discount value (excl. VAT) for the goods/services sold/purchased
    ''' in original invoice currency (see <see cref="InvoiceInfo.CurrencyCode"/>).
    ''' </summary> 
    ''' <remarks>Optional.
    ''' Only applicable for invoices made.
    ''' If used, the discount amount should be included in <see cref="Sum"/>
    ''' so that amount payable (excl. VAT) in original currency equals <see cref="Sum"/>
    ''' minus Discount.</remarks>
    Public Property Discount() As Double
        Get
            Return _Discount
        End Get
        Set(ByVal value As Double)
            If _Discount <> value Then
                _Discount = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a VAT amount per discount in the original invoice currency
    ''' (see <see cref="CurrencyCode"/>).
    ''' </summary> 
    ''' <remarks>Only applicable to invoices made.
    ''' If used, the discount VAT amount should be included in <see cref="SumVat"/>
    ''' so that actual VAT amount in original invoice currency equals <see cref="SumVat"/>
    ''' minus DiscountVat.</remarks>
    Public Property DiscountVat() As Double
        Get
            Return _DiscountVat
        End Get
        Set(ByVal value As Double)
            If _DiscountVat <> value Then
                _DiscountVat = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets an amount actually paid/received for the invoice.
    ''' </summary>
    ''' <returns>Optional.</returns>
    Public Property SumReceived() As Double
        Get
            Return _SumReceived
        End Get
        Set(ByVal value As Double)
            If _SumReceived <> value Then
                _SumReceived = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a value indicating whether the invoice date is used to
    ''' compose a <see cref="FullNumber">full (formatted) invoice No.</see>,
    ''' i.e. when <see cref="Number">a sequential No</see> is only unique within a day.
    ''' </summary>
    ''' <returns>Only required for invoices made.</returns>
    Public Property AddDateToNumberOptionWasUsed() As Boolean
        Get
            Return _AddDateToNumberOptionWasUsed
        End Get
        Set(ByVal value As Boolean)
            If _AddDateToNumberOptionWasUsed <> value Then
                _AddDateToNumberOptionWasUsed = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a value indicating minimum digits within
    ''' a <see cref="FullNumber">full (formatted) invoice No.</see>,
    ''' e.g. value 4 means formatted No 0001.
    ''' </summary>
    ''' <returns>Only required for invoices made.</returns>
    Public Property NumbersInInvoice() As Integer
        Get
            Return _NumbersInInvoice
        End Get
        Set(ByVal value As Integer)
            If _NumbersInInvoice <> value Then
                _NumbersInInvoice = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets an original ID of the invoice (if the source system is not an original owner).
    ''' </summary>
    ''' <remarks>Optional.</remarks>
    Public Property ExternalID() As String
        Get
            Return _ExternalID.Trim
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then value = ""
            If _ExternalID.Trim <> value.Trim Then
                _ExternalID = value.Trim
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a last update timestamp.
    ''' </summary>
    ''' <remarks>Optional.
    ''' Serialized as UpdateDateString element.</remarks>
    <XmlIgnore()> _
    Public Property UpdateDate() As DateTime
        Get
            Return _UpdateDate
        End Get
        Set(ByVal value As DateTime)
            If _UpdateDate <> value Then
                _UpdateDate = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' For serialization only. Do not use this property.
    ''' </summary>
    Public Property UpdateDateString() As String
        Get
            Return _UpdateDate.ToString(Globalization.CultureInfo.InvariantCulture)
        End Get
        Set(ByVal value As String)
            If value Is Nothing OrElse String.IsNullOrEmpty(value.Trim) Then Exit Property
            Dim newDate As Date = Date.Parse(value, Globalization.CultureInfo.InvariantCulture)
            If _UpdateDate <> newDate Then
                _UpdateDate = newDate
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a guid (any unique identifier) of the source system.
    ''' </summary>
    ''' <remarks>Used to identify copy behaviour, i.e. when the source and target
    ''' systems are actually the same system, an invoice should be copied not imported.
    ''' Use your system guid or Guid.NewGuid to avoid unintended conflicts.
    ''' Is initialized with Guid.NewGuid.ToString, therefore do not use if not required.</remarks>
    Public Property SystemGuid() As String
        Get
            Return _SystemGuid
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then value = ""
            If _SystemGuid.Trim <> value.Trim Then
                _SystemGuid = value.Trim
            End If
        End Set
    End Property

    Public Property InvoiceItems() As List(Of InvoiceItemInfo)
        Get
            Return _InvoiceItems
        End Get
        Set(ByVal value As List(Of InvoiceItemInfo))
            If Not value Is Nothing Then
                _InvoiceItems = value
            Else 
                _InvoiceItems = New List(Of InvoiceItemInfo)()
            End If
        End Set
    End Property


    ''' <summary>
    ''' Helper method to calculate subtotals for the invoice items
    ''' for <See cref="CurrencyCode">original invoice currency</See>.
    ''' </summary>
    ''' <remarks>Used when the source system does not provide subtotals.</remarks>
    Public Sub CalculateSubtotals()

        Me._Discount = 0.0
        Me._DiscountVat = 0.0
        Me._Sum = 0.0
        Me._SumVat = 0.0
        Me._SumTotal = 0.0

        If _InvoiceItems Is Nothing Then Exit Sub

        For Each item As InvoiceItemInfo In _InvoiceItems
            Me._Discount = Math.Round(Me._Discount + item.Discount, 2, MidpointRounding.AwayFromZero)
            Me._DiscountVat = Math.Round(Me._DiscountVat + item.DiscountVat, 2, MidpointRounding.AwayFromZero)
            Me._Sum = Math.Round(Me._Sum + item.Sum, 2, MidpointRounding.AwayFromZero)
            Me._SumVat = Math.Round(Me._SumVat + item.SumVat, 2, MidpointRounding.AwayFromZero)
            Me._SumTotal = Math.Round(Me._SumTotal + item.SumTotal, 2, MidpointRounding.AwayFromZero)
        Next 

    End Sub

    ''' <summary>
    ''' Helper method to calculate subtotals for the invoice items in base currency.
    ''' </summary>
    ''' <remarks>Used when the source system does not provide subtotals.</remarks>
    Public Sub CalculateSubtotalsLTL()

        Me._DiscountLTL = 0.0
        Me._DiscountVatLTL = 0.0
        Me._SumLTL = 0.0
        Me._SumVatLTL = 0.0
        Me._SumTotalLTL = 0.0

        If _InvoiceItems Is Nothing Then Exit Sub

        For Each item As InvoiceItemInfo In _InvoiceItems
            Me._DiscountLTL = Math.Round(Me._DiscountLTL + item.DiscountLTL, 2, MidpointRounding.AwayFromZero)
            Me._DiscountVatLTL = Math.Round(Me._DiscountVatLTL + item.DiscountVatLTL, 2, MidpointRounding.AwayFromZero)
            Me._SumLTL = Math.Round(Me._SumLTL + item.SumLTL, 2, MidpointRounding.AwayFromZero)
            Me._SumVatLTL = Math.Round(Me._SumVatLTL + item.SumVatLTL, 2, MidpointRounding.AwayFromZero)
            Me._SumTotalLTL = Math.Round(Me._SumTotalLTL + item.SumTotalLTL, 2, MidpointRounding.AwayFromZero)
        Next 

    End Sub

End Class
