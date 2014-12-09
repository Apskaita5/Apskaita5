Namespace HelperLists

    <Serializable()> _
    Public Class CompanyRegionalInfo
        Inherits ReadOnlyBase(Of CompanyRegionalInfo)

#Region " Business Methods "

        Private _ID As Integer = 0
        Private _LanguageCode As String = ""
        Private _LanguageName As String = ""
        Private _Address As String = ""
        Private _BankAccount As String = ""
        Private _Bank As String = ""
        Private _BankSWIFT As String = ""
        Private _BankAddress As String = ""
        Private _Contacts As String = ""
        Private _InvoiceInfoLine As String = ""
        Private _MeasureUnitInvoiceMade As String = ""
        Private _DiscountName As String = ""
        Private _HeadTitle As String = ""
        Private _LogoImage As Byte() = Nothing
        Private _InvoiceForm As Byte() = Nothing

        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        Public ReadOnly Property LanguageCode() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _LanguageCode.Trim
            End Get
        End Property

        Public ReadOnly Property LanguageName() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _LanguageName.Trim
            End Get
        End Property

        Public ReadOnly Property Address() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Address.Trim
            End Get
        End Property

        Public ReadOnly Property BankAccount() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _BankAccount.Trim
            End Get
        End Property

        Public ReadOnly Property Bank() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Bank.Trim
            End Get
        End Property

        Public ReadOnly Property BankSWIFT() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _BankSWIFT.Trim
            End Get
        End Property

        Public ReadOnly Property BankAddress() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _BankAddress.Trim
            End Get
        End Property

        Public ReadOnly Property Contacts() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Contacts.Trim
            End Get
        End Property

        Public ReadOnly Property InvoiceInfoLine() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _InvoiceInfoLine.Trim
            End Get
        End Property

        Public ReadOnly Property MeasureUnitInvoiceMade() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _MeasureUnitInvoiceMade.Trim
            End Get
        End Property

        Public ReadOnly Property DiscountName() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DiscountName.Trim
            End Get
        End Property

        Public ReadOnly Property HeadTitle() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _HeadTitle.Trim
            End Get
        End Property

        Public ReadOnly Property LogoImage() As Byte()
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _LogoImage
            End Get
        End Property

        Public ReadOnly Property InvoiceForm() As Byte()
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _InvoiceForm
            End Get
        End Property



        Protected Overrides Function GetIdValue() As Object
            Return _ID
        End Function

        Public Overrides Function ToString() As String
            If Not _ID > 0 Then Return ""
            Return _LanguageName
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function GetCompanyRegionalInfo(ByVal dr As DataRow) As CompanyRegionalInfo
            Return New CompanyRegionalInfo(dr)
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
            _LanguageCode = CStrSafe(dr.Item(1)).Trim
            _LanguageName = GetLanguageName(_LanguageCode, False)
            _Address = CStrSafe(dr.Item(2)).Trim
            _Bank = CStrSafe(dr.Item(3)).Trim
            _BankAccount = CStrSafe(dr.Item(4)).Trim
            _BankSWIFT = CStrSafe(dr.Item(5)).Trim
            _BankAddress = CStrSafe(dr.Item(6)).Trim
            _Contacts = CStrSafe(dr.Item(7)).Trim
            _InvoiceInfoLine = CStrSafe(dr.Item(8)).Trim
            _MeasureUnitInvoiceMade = CStrSafe(dr.Item(9)).Trim
            _DiscountName = CStrSafe(dr.Item(10)).Trim
            _HeadTitle = CStrSafe(dr.Item(11)).Trim
            _InvoiceForm = CByteArraySafe(dr.Item(12), 50)
            _LogoImage = CByteArraySafe(dr.Item(13), 50)

        End Sub

#End Region

    End Class

End Namespace