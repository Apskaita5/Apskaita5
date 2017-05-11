Imports System.Xml.Serialization

<Serializable()> _
Public Class InvoiceInfo

    Private _ID As String = "0"
    Private _ProjectCode As String = ""
    Private _Payer As ClientInfo = New ClientInfo
    Private _Date As Date = Today
    Private _Serial As String = ""
    Private _Number As Integer = 0
    Private _FullNumber As String = ""
    Private _Content As String = ""
    Private _CurrencyCode As String = ""
    Private _CurrencyRate As Double = 0
    Private _LanguageCode As String = "LT"
    Private _VatExemptions As String = ""
    Private _VatExemptionsAltLng As String = ""
    Private _CustomInfo As String = ""
    Private _CustomInfoAltLng As String = ""
    Private _CommentsInternal As String = ""
    Private _SumLTL As Double = 0
    Private _SumVatLTL As Double = 0
    Private _SumTotalLTL As Double = 0
    Private _Sum As Double = 0
    Private _SumVat As Double = 0
    Private _SumTotal As Double = 0
    Private _DiscountLTL As Double = 0
    Private _DiscountVatLTL As Double = 0
    Private _Discount As Double = 0
    Private _DiscountVat As Double = 0
    Private _SumReceived As Double = 0
    Private _AddDateToNumberOptionWasUsed As Boolean = False
    Private _NumbersInInvoice As Integer = 0
    Private _ExternalID As String = ""
    Private _SystemGuid As String = Guid.NewGuid.ToString
    Private _UpdateDate As DateTime = Now
    Private _InvoiceItems As New List(Of InvoiceItemInfo)


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
            End If
        End Set
    End Property


    Public Sub New()
    End Sub

End Class