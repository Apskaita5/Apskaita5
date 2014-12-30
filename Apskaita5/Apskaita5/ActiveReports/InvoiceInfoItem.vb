
Namespace ActiveReports

    <Serializable()> _
    Public Class InvoiceInfoItem
        Inherits ReadOnlyBase(Of InvoiceInfoItem)

#Region " Business Methods "

        Private _ID As Integer
        Private _Type As InvoiceInfoType
        Private _PersonID As Integer
        Private _PersonName As String
        Private _PersonCode As String
        Private _PersonVatCode As String
        Private _PersonEmail As String
        Private _PersonAccount As Long
        Private _Date As Date
        Private _Number As String
        Private _Content As String
        Private _CurrencyCode As String
        Private _CurrencyRate As Double
        Private _LanguageName As String
        Private _CommentsInternal As String
        Private _VatRate As Double
        Private _Sum As Double
        Private _SumVat As Double
        Private _TotalSum As Double
        Private _SumLTL As Double
        Private _SumVatLTL As Double
        Private _TotalSumLTL As Double
        Private _SumDiscount As Double
        Private _SumVatDiscount As Double
        Private _TotalSumDiscount As Double
        Private _SumDiscountLTL As Double
        Private _SumVatDiscountLTL As Double
        Private _TotalSumDiscountLTL As Double


        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        Public ReadOnly Property Type() As InvoiceInfoType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Type
            End Get
        End Property

        Public ReadOnly Property PersonID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _PersonID
            End Get
        End Property

        Public ReadOnly Property PersonName() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _PersonName.Trim
            End Get
        End Property

        Public ReadOnly Property PersonCode() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _PersonCode.Trim
            End Get
        End Property

        Public ReadOnly Property PersonVatCode() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _PersonVatCode.Trim
            End Get
        End Property

        Public ReadOnly Property PersonEmail() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _PersonEmail.Trim
            End Get
        End Property

        Public ReadOnly Property PersonAccount() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _PersonAccount
            End Get
        End Property

        Public ReadOnly Property [Date]() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Date
            End Get
        End Property

        Public ReadOnly Property Number() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Number.Trim
            End Get
        End Property

        Public ReadOnly Property Content() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Content.Trim
            End Get
        End Property

        Public ReadOnly Property CurrencyCode() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _CurrencyCode.Trim
            End Get
        End Property

        Public ReadOnly Property CurrencyRate() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_CurrencyRate, 6)
            End Get
        End Property

        Public ReadOnly Property LanguageName() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _LanguageName.Trim
            End Get
        End Property

        Public ReadOnly Property CommentsInternal() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _CommentsInternal.Trim
            End Get
        End Property

        Public ReadOnly Property VatRate() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_VatRate)
            End Get
        End Property

        Public ReadOnly Property Sum() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_Sum)
            End Get
        End Property

        Public ReadOnly Property SumVat() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_SumVat)
            End Get
        End Property

        Public ReadOnly Property TotalSum() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_TotalSum)
            End Get
        End Property

        Public ReadOnly Property SumLTL() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_SumLTL)
            End Get
        End Property

        Public ReadOnly Property SumVatLTL() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_SumVatLTL)
            End Get
        End Property

        Public ReadOnly Property TotalSumLTL() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_TotalSumLTL)
            End Get
        End Property

        Public ReadOnly Property SumDiscount() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_SumDiscount)
            End Get
        End Property

        Public ReadOnly Property SumVatDiscount() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_SumVatDiscount)
            End Get
        End Property

        Public ReadOnly Property TotalSumDiscount() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_TotalSumDiscount)
            End Get
        End Property

        Public ReadOnly Property SumDiscountLTL() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_SumDiscountLTL)
            End Get
        End Property

        Public ReadOnly Property SumVatDiscountLTL() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_SumVatDiscountLTL)
            End Get
        End Property

        Public ReadOnly Property TotalSumDiscountLTL() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_TotalSumDiscountLTL)
            End Get
        End Property



        Protected Overrides Function GetIdValue() As Object
            Return _ID
        End Function

        Public Overrides Function ToString() As String
            Return _Content
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

        End Sub

#End Region

    End Class

End Namespace