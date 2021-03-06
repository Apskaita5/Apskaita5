﻿<Serializable()> _
Public Class InvoiceItemInfo

#Region " Private Backing Fields "

    Private _ID As String = String.Empty
    Private _NameInvoice As String = String.Empty
    Private _NameInvoiceAltLng As String = String.Empty
    Private _MeasureUnit As String = String.Empty
    Private _MeasureUnitAltLng As String = String.Empty
    Private _Ammount As Double = 0.0
    Private _UnitValueLTL As Double = 0.0
    Private _SumLTL As Double = 0.0
    Private _VatRate As Double = 0.0
    Private _VatIsVirtual As Boolean = False
    Private _VatDDeclarationSchemaID As String = String.Empty
    Private _SumVatLTL As Double = 0.0
    Private _SumTotalLTL As Double = 0.0
    Private _UnitValue As Double = 0.0
    Private _Sum As Double = 0.0
    Private _SumVat As Double = 0.0
    Private _SumTotal As Double = 0.0
    Private _DiscountLTL As Double = 0.0
    Private _Discount As Double = 0.0
    Private _DiscountVatLTL As Double = 0.0
    Private _DiscountVat As Double = 0.0
    Private _ProjectCode As String = String.Empty
    Private _Comments As String = String.Empty
    Private _SumReceived As Double = 0.0
    Private _AccountVat As Long = 0
    Private _AccountIncome As Long = 0
    Private _ServiceCode As String = String.Empty

#End Region


    ''' <summary>
    ''' Gets or sets an ID of the invoice item in the source system.
    ''' </summary>
    ''' <remarks>Depends on the target system, usually optional.</remarks> 
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
    ''' Gets or sets a content (description) of the item (goods, services etc.)
    ''' in base language.
    ''' </summary>
    ''' <remarks>Required.</remarks>
    Public Property NameInvoice() As String
        Get
            Return _NameInvoice.Trim
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then value = ""
            If _NameInvoice.Trim <> value.Trim Then
                _NameInvoice = value.Trim
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a content (description) of the item (goods, services etc.)
    ''' in original invoice language (only applicable to invoices made).
    ''' </summary>
    ''' <remarks>Only required for invoices made and only if the
    ''' <see cref="InvoiceInfo.LanguageCode">original invoice language</see>
    ''' is a foreign (non base) language.</remarks>
    Public Property NameInvoiceAltLng() As String
        Get
            Return _NameInvoiceAltLng.Trim
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then value = ""
            If _NameInvoiceAltLng.Trim <> value.Trim Then
                _NameInvoiceAltLng = value.Trim
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a measure unit of the item (goods, services etc.) in base language.
    ''' </summary> 
    ''' <remarks>Required.</remarks>
    Public Property MeasureUnit() As String
        Get
            Return _MeasureUnit.Trim
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then value = ""
            If _MeasureUnit.Trim <> value.Trim Then
                _MeasureUnit = value.Trim
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a measure unit of the item (goods, services etc.)
    ''' in original invoice language (only applicable to invoices made).
    ''' </summary>
    ''' <remarks>Only required for invoices made and only if the
    ''' <see cref="InvoiceInfo.LanguageCode">original invoice language</see>
    ''' is a foreign (non base) language.</remarks>
    Public Property MeasureUnitAltLng() As String
        Get
            Return _MeasureUnitAltLng.Trim
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then value = ""
            If _MeasureUnitAltLng.Trim <> value.Trim Then
                _MeasureUnitAltLng = value.Trim
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets an amount of the goods/services sold/purchased
    ''' in measure units defined by <see cref="MeasureUnit"/>.
    ''' </summary>       
    ''' <remarks>Required.
    ''' Could only be negative for returns.
    ''' Amount and <see cref="UnitValue"/> cannot be both negative.</remarks>
    Public Property Ammount() As Double
        Get
            Return _Ammount
        End Get
        Set(ByVal value As Double)
            If _Ammount <> value Then
                _Ammount = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a price of the goods/services sold/purchased per unit
    ''' (see <see cref="MeasureUnit"/>) in base currency.
    ''' </summary>      
    ''' <remarks>Required.
    ''' Could only be negative for discounts/price reductions.
    ''' Unit value and <see cref="Ammount"/> cannot be both negative.</remarks>
    Public Property UnitValueLTL() As Double
        Get
            Return _UnitValueLTL
        End Get
        Set(ByVal value As Double)
            If _UnitValueLTL <> value Then
                _UnitValueLTL = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a subtotal value (excl. VAT) of the goods/services sold/purchased in base currency.
    ''' </summary>       
    ''' <remarks>Required.
    ''' Could be negative if either <see cref="Ammount"/> or <see cref="UnitValueLTL"/> is negative.
    ''' It is safe with regard to arbitrary rounding methods.</remarks>
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
    ''' Gets or sets an applicable VAT rate rate in percents (e.g. value 21.0 translates to 21.0%).
    ''' </summary>       
    ''' <remarks>Optional.
    ''' Could NOT be negative.</remarks>
    Public Property VatRate() As Double
        Get
            Return _VatRate
        End Get
        Set(ByVal value As Double)
            If _VatRate <> value Then
                _VatRate = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a value indicating whether the VAT is virtual, i.e. "printed" on invoice
    ''' yet NOT creating a legal obligation to state (e.g. reverse charge).
    ''' </summary>
    Public Property VatIsVirtual() As Boolean
        Get
            Return _VatIsVirtual
        End Get
        Set(ByVal value As Boolean)
            If _VatIsVirtual <> value Then
                _VatIsVirtual = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a code of applicable VAT schema (as defined by the target system).
    ''' </summary>      
    ''' <remarks>Optional. Recommended if the source system supports invoice item attributes.</remarks>
    Public Property VatDDeclarationSchemaID() As String
        Get
            Return _VatDDeclarationSchemaID
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then value = ""
            If _VatDDeclarationSchemaID <> value Then
                _VatDDeclarationSchemaID = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a VAT amount in base currency.
    ''' </summary>
    ''' <remarks>Depends on the <see cref="VatRate"/> and <see cref="SumLTL"/>.
    ''' It is safe with regard to arbitrary rounding methods.</remarks>
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
    ''' Depends on the <see cref="SumLTL"/> and <see cref="SumVatLTL"/>.
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
    ''' Gets or sets a price of the goods/services sold/purchased per unit
    ''' (see <see cref="MeasureUnit"/>) in original invoice currency (see <see cref="InvoiceInfo.CurrencyCode"/>).
    ''' </summary>      
    ''' <remarks>Required.
    ''' Could only be negative for discounts/price reductions.
    ''' Unit value and <see cref="Ammount"/> cannot be both negative.</remarks>
    Public Property UnitValue() As Double
        Get
            Return _UnitValue
        End Get
        Set(ByVal value As Double)
            If _UnitValue <> value Then
                _UnitValue = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a subtotal (excl. VAT) of the goods/services sold/purchased
    ''' in original invoice currency (see <see cref="InvoiceInfo.CurrencyCode"/>).
    ''' </summary>      
    ''' <remarks>Required.
    ''' Could be negative if either <see cref="Ammount"/> or <see cref="UnitValue"/> is negative.
    ''' It is safe with regard to arbitrary rounding methods.</remarks>
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
    ''' Gets or sets a VAT amount in original invoice currency (see <see cref="InvoiceInfo.CurrencyCode"/>).
    ''' </summary> 
    ''' <remarks>Depends on the <see cref="VatRate"/> and <see cref="Sum"/>.
    ''' It is safe with regard to arbitrary rounding methods.</remarks>
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
    ''' Gets or sets a VAT amount per discount in base currency.
    ''' </summary> 
    ''' <remarks>Only applicable to invoices made.
    ''' Depends on the <see cref="VatRate"/> and <see cref="DiscountLTL"/>.
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
    ''' Gets or sets a VAT amount per discount in the original invoice currency
    ''' (see <see cref="InvoiceInfo.CurrencyCode"/>).
    ''' </summary> 
    ''' <remarks>Only applicable to invoices made.
    ''' Depends on the <see cref="VatRate"/> and <see cref="Discount"/>.
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
    ''' Gets or sets arbitrary comments about the invoice item.
    ''' </summary>
    ''' <returns>Optional.</returns>
    Public Property Comments() As String
        Get
            Return _Comments.Trim
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then value = ""
            If _Comments.Trim <> value.Trim Then
                _Comments = value.Trim
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets an amount actually paid/received for the invoice item.
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
    ''' Gets or sets a code for the VAT account as defined by the target system.
    ''' </summary>
    ''' <remarks>Depends on the target system, usually optional.</remarks>
    Public Property AccountVat() As Long
        Get
            Return _AccountVat
        End Get
        Set(ByVal value As Long)
            If _AccountVat <> value Then
                _AccountVat = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a code for the income/costs account as defined by the target system.
    ''' </summary>
    ''' <remarks>Depends on the target system, usually required.</remarks>
    Public Property AccountIncome() As Long
        Get
            Return _AccountIncome
        End Get
        Set(ByVal value As Long)
            If _AccountIncome <> value Then
                _AccountIncome = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a code of the services provided/purchased (as defined by the target system).
    ''' </summary>
    ''' <remarks>Optional. If it is a service being provided or purchased.</remarks>
    Public Property ServiceCode() As String
        Get
            Return _ServiceCode.Trim
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then value = ""
            If _ServiceCode.Trim <> value.Trim Then
                _ServiceCode = value.Trim
            End If
        End Set
    End Property

End Class