Namespace HelperLists

    ''' <summary>
    ''' Represents a value object for <see cref="General.CompanyRegionalData">general company data for a certain language</see>.
    ''' </summary>
    ''' <remarks>Values for the base language are stored in the database table imone. 
    ''' Values for other languages are stored in the database table companyregionaldata.</remarks>
    <Serializable()> _
    Public Class CompanyRegionalInfo
        Inherits ReadOnlyBase(Of CompanyRegionalInfo)
        Implements IValueObjectIsEmpty

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

        ''' <summary>
        ''' Whether an object is a palace holder (does not represent real data).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property IsEmpty() As Boolean _
            Implements IValueObjectIsEmpty.IsEmpty
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return Not _ID > 0
            End Get
        End Property

        ''' <summary>
        ''' Gets an ID of the regional data that is asigned by a database (AUTOINCREMENT).
        ''' Returns 0 for base language.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="General.CompanyRegionalData.ID">CompanyRegionalData.ID</see>.
        ''' Value for the base language is always 0. 
        ''' Value for any other language is stored in the database field companyregionaldata.ID.</remarks>
        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' Gets a language code for the current data.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="General.CompanyRegionalData.LanguageCode">CompanyRegionalData.LanguageCode</see>.
        ''' Value for the base language is always base language code. 
        ''' Value for any other language is stored in the database field companyregionaldata.LanguageCode.</remarks>
        Public ReadOnly Property LanguageCode() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _LanguageCode.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a language name for the current data.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="General.CompanyRegionalData.LanguageName">CompanyRegionalData.LanguageName</see>.
        ''' Value for the base language is always base language name. 
        ''' Value for any other language is stored in the database field companyregionaldata.LanguageCode.</remarks>
        Public ReadOnly Property LanguageName() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _LanguageName.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a company address in <see cref="LanguageName">LanguageName</see>.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="General.CompanyRegionalData.Address">CompanyRegionalData.Address</see>.
        ''' Value for the base language is stored in the database field imone.I_Adresas. 
        ''' Value for any other language is stored in the database field companyregionaldata.Address.</remarks>
        Public ReadOnly Property Address() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Address.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a company's bank account in <see cref="LanguageName">LanguageName</see>.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="General.CompanyRegionalData.BankAccount">CompanyRegionalData.BankAccount</see>.
        ''' Value for the base language is stored in the database field imone.I_Banko_sask. 
        ''' Value for any other language is stored in the database field companyregionaldata.BankAccount.</remarks>
        Public ReadOnly Property BankAccount() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _BankAccount.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a company's bank name in <see cref="LanguageName">LanguageName</see>.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="General.CompanyRegionalData.Bank">CompanyRegionalData.Bank</see>.
        ''' Value for the base language is stored in the database field imone.I_Bankas. 
        ''' Value for any other language is stored in the database field companyregionaldata.Bank.</remarks>
        Public ReadOnly Property Bank() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Bank.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a company's bank's SWIFT code in <see cref="LanguageName">LanguageName</see>.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="General.CompanyRegionalData.BankSWIFT">CompanyRegionalData.BankSWIFT</see>.
        ''' Value for the base language is stored in the database field imone.BankSWIFT. 
        ''' Value for any other language is stored in the database field companyregionaldata.BankSWIFT.</remarks>
        Public ReadOnly Property BankSWIFT() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _BankSWIFT.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a company's bank's address in <see cref="LanguageName">LanguageName</see>.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="General.CompanyRegionalData.BankAddress">CompanyRegionalData.BankAddress</see>.
        ''' Value for the base language is stored in the database field imone.BankAddress. 
        ''' Value for any other language is stored in the database field companyregionaldata.BankAddress.</remarks>
        Public ReadOnly Property BankAddress() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _BankAddress.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a company contact data in <see cref="LanguageName">LanguageName</see>.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="General.CompanyRegionalData.Contacts">CompanyRegionalData.Contacts</see>.
        ''' Value for the base language is stored in the database field imone.I_Koordinates. 
        ''' Value for any other language is stored in the database field companyregionaldata.Contacts.</remarks>
        Public ReadOnly Property Contacts() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Contacts.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets additinal information that could be displayed in invoices made in <see cref="LanguageName">LanguageName</see>.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="General.CompanyRegionalData.InvoiceInfoLine">CompanyRegionalData.InvoiceInfoLine</see>.
        ''' Value for the base language is stored in the database field imone.InvoiceInfoLine. 
        ''' Value for any other language is stored in the database field companyregionaldata.InvoiceInfoLine.</remarks>
        Public ReadOnly Property InvoiceInfoLine() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _InvoiceInfoLine.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a default measure unit name for invoices made in <see cref="LanguageName">LanguageName</see>.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="General.CompanyRegionalData.MeasureUnitInvoiceMade">CompanyRegionalData.MeasureUnitInvoiceMade</see>.
        ''' Value for the base language is stored in the database field imone.MeasureUnitInvoiceMade. 
        ''' Value for any other language is stored in the database field companyregionaldata.MeasureUnitInvoiceMade.</remarks>
        Public ReadOnly Property MeasureUnitInvoiceMade() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _MeasureUnitInvoiceMade.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a discount name for invoices made in <see cref="LanguageName">LanguageName</see>.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="General.CompanyRegionalData.DiscountName">CompanyRegionalData.DiscountName</see>.
        ''' Value for the base language is stored in the database field imone.DiscountName. 
        ''' Value for any other language is stored in the database field companyregionaldata.DiscountName.</remarks>
        Public ReadOnly Property DiscountName() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DiscountName.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a company's head title name in <see cref="LanguageName">LanguageName</see>.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="General.CompanyRegionalData.HeadTitle">CompanyRegionalData.HeadTitle</see>.
        ''' Value for the base language is stored in the database field imone.HeadTitle. 
        ''' Value for any other language is stored in the database field companyregionaldata.HeadTitle.</remarks>
        Public ReadOnly Property HeadTitle() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _HeadTitle.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a company logo in <see cref="LanguageName">LanguageName</see>.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="General.CompanyRegionalData.LogoImage">CompanyRegionalData.LogoImage</see>.
        ''' Values are stored in the database table companyforms 
        ''' where field FormToken equals <see cref="TokenCompanyLogo">TokenCompanyLogo</see>.</remarks>
        Public ReadOnly Property LogoImage() As Byte()
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _LogoImage
            End Get
        End Property

        ''' <summary>
        ''' Gets an invoice form (.rdlc format) in <see cref="LanguageName">LanguageName</see>.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="General.CompanyRegionalData.InvoiceForm">CompanyRegionalData.InvoiceForm</see>.
        ''' Values are stored in the database table companyforms 
        ''' where field FormToken equals <see cref="TokenInvoiceForm">TokenInvoiceForm</see>.</remarks>
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

        ''' <summary>
        ''' Get existing <see cref="General.CompanyRegionalData">general company data for a certain language</see> by a database query.
        ''' </summary>
        ''' <param name="dr">DataRow containing data.</param>
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