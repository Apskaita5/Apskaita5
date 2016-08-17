Namespace ActiveReports

    ''' <summary>
    ''' Represents a person report item. Contains information about a person that the company operates with (clients, suppliers, workers, etc.).
    ''' </summary>
    ''' <remarks>Values are stored in the database table asmenys.</remarks>
    <Serializable()> _
    Public NotInheritable Class PersonInfoItem
        Inherits ReadOnlyBase(Of PersonInfoItem)

#Region " Business Methods "

        Private _ID As Integer = 0
        Private _Name As String = ""
        Private _Code As String = ""
        Private _CodeIsNotReal As Boolean = False
        Private _Address As String = ""
        Private _Bank As String = ""
        Private _BankAccount As String = ""
        Private _CodeVAT As String = ""
        Private _CodeSODRA As String = ""
        Private _Email As String = ""
        Private _AccountAgainstBankBuyer As Long = 0
        Private _AccountAgainstBankSupplyer As Long = 0
        Private _ContactInfo As String = ""
        Private _InternalCode As String = ""
        Private _LanguageCode As String = LanguageCodeLith
        Private _LanguageName As String = GetLanguageName(LanguageCodeLith, False)
        Private _CurrencyCode As String = GetCurrentCompany.BaseCurrency
        Private _StateCode As String = StateCodeLith
        Private _IsNaturalPerson As Boolean = False
        Private _IsObsolete As Boolean = False
        Private _IsClient As Boolean = False
        Private _IsSupplier As Boolean = False
        Private _IsWorker As Boolean = False


        ''' <summary>
        ''' Gets an ID of the person (assigned automaticaly by DB AUTOINCREMENT).
        ''' </summary>
        ''' <remarks>Value is stored in the database field asmenys.ID.</remarks>
        Public ReadOnly Property ID() As Integer
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' Gets an official name of the person.
        ''' </summary>
        ''' <remarks>Value is stored in the database field asmenys.Pavad.</remarks>
        Public ReadOnly Property Name() As String
            Get
                Return _Name
            End Get
        End Property

        ''' <summary>
        ''' Gets an official registration/personal code of the person.
        ''' </summary>
        ''' <remarks>Value is stored in the database field asmenys.Kodas.</remarks>
        Public ReadOnly Property Code() As String
            Get
                Return _Code
            End Get
        End Property

        ''' <summary>
        ''' Gets whether the <see cref="Code">Code</see> is not real, 
        ''' i.e. assigned by the company (e.g. natural persons are not required
        ''' to provide their personal identification code).
        ''' </summary>
        ''' <remarks>Value is stored in the database field asmenys.CodeIsNotReal.</remarks>
        Public ReadOnly Property CodeIsNotReal() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _CodeIsNotReal
            End Get
        End Property

        ''' <summary>
        ''' Gets an address of the person.
        ''' </summary>
        ''' <remarks>Value is stored in the database field asmenys.Adresas.</remarks>
        Public ReadOnly Property Address() As String
            Get
                Return _Address
            End Get
        End Property

        ''' <summary>
        ''' Gets a name of the bank used by the person.
        ''' </summary>
        ''' <remarks>Value is stored in the database field asmenys.Bank.</remarks>
        Public ReadOnly Property Bank() As String
            Get
                Return _Bank
            End Get
        End Property

        ''' <summary>
        ''' Gets a bank account number used by the person.
        ''' </summary>
        ''' <remarks>Value is stored in the database field asmenys.B_Sask.</remarks>
        Public ReadOnly Property BankAccount() As String
            Get
                Return _BankAccount
            End Get
        End Property

        ''' <summary>
        ''' Gets a VAT payer code of the person.
        ''' </summary>
        ''' <remarks>Value is stored in the database field asmenys.SP_kodas.</remarks>
        Public ReadOnly Property CodeVAT() As String
            Get
                Return _CodeVAT
            End Get
        End Property

        ''' <summary>
        ''' Gets a SODRA (social security) code of the person.
        ''' </summary>
        ''' <remarks>Only applicable to natural persons.
        ''' Value is stored in the database field asmenys.SD_kodas.</remarks>
        Public ReadOnly Property CodeSODRA() As String
            Get
                Return _CodeSODRA
            End Get
        End Property

        ''' <summary>
        ''' Gets an email address of the person.
        ''' </summary>
        ''' <remarks>Value is stored in the database field asmenys.E_Mail.</remarks>
        Public ReadOnly Property Email() As String
            Get
                Return _Email
            End Get
        End Property

        ''' <summary>
        ''' Gets any other person info, e.g. phone number, etc.
        ''' </summary>
        ''' <remarks>Value is stored in the database field asmenys.ContactInfo.</remarks>
        Public ReadOnly Property ContactInfo() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ContactInfo.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets an internal code of the person for company's uses.
        ''' </summary>
        ''' <remarks>Value is stored in the database field asmenys.InternalCode.</remarks>
        Public ReadOnly Property InternalCode() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _InternalCode.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a default language ISO 639-1 code used by the person.
        ''' </summary>
        ''' <remarks>Value is stored in the database field asmenys.LanguageCode.</remarks>
        Public ReadOnly Property LanguageCode() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _LanguageCode.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a default language used by the person.
        ''' </summary>
        ''' <remarks>Use <see cref="HelperLists.CompanyRegionalInfoList">CompanyRegionalInfoList</see> to get available languages.
        ''' Value is stored in the database field asmenys.LanguageCode.</remarks>
        Public ReadOnly Property LanguageName() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _LanguageName.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a default currency code used by the person.
        ''' </summary>
        ''' <remarks>Use <see cref="CurrencyCodes">CurrencyCodes()</see> for a datasource.
        ''' Value is stored in the database field asmenys.CurrencyCode.</remarks>
        Public ReadOnly Property CurrencyCode() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _CurrencyCode.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a state of the origin of the person (ISO 3166–1 alpha 2 code).
        ''' </summary>
        ''' <remarks>Value is stored in the database field asmenys.StateCode.</remarks>
        <StringField(ValueRequiredLevel.Mandatory, 10, False)> _
        Public ReadOnly Property StateCode() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _StateCode.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets an account for buyers' debts.
        ''' </summary>
        ''' <remarks>Used when importing bank operations of type 'money received'. Credits this account, debits bank account.
        ''' Value is stored in the database field asmenys.B_Kor.</remarks>
        Public ReadOnly Property AccountAgainstBankBuyer() As Long
            Get
                Return _AccountAgainstBankBuyer
            End Get
        End Property

        ''' <summary>
        ''' Gets an account for suppliers' debts.
        ''' </summary>
        ''' <remarks>Used when importing bank operations of type 'money transfered'. Debits this account, credits bank account.
        ''' Value is stored in the database field asmenys.B_Kor_Tiek.</remarks>
        Public ReadOnly Property AccountAgainstBankSupplyer() As Long
            Get
                Return _AccountAgainstBankSupplyer
            End Get
        End Property

        ''' <summary>
        ''' Whether the person is a natural person, i.e. not a company.
        ''' </summary>
        ''' <remarks>Value is stored in the database field asmenys.IsNaturalPerson.</remarks>
        Public ReadOnly Property IsNaturalPerson() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsNaturalPerson
            End Get
        End Property

        ''' <summary>
        ''' Whether the person is no longer in use, i.e. not supposed to be displayed in combos.
        ''' </summary>
        ''' <remarks>Value is stored in the database field asmenys.IsObsolete.</remarks>
        Public ReadOnly Property IsObsolete() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsObsolete
            End Get
        End Property

        ''' <summary>
        ''' Whether a person is a client of the company.
        ''' </summary>
        ''' <remarks>Value is stored in the database field asmenys.IsClient.</remarks>
        Public ReadOnly Property IsClient() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsClient
            End Get
        End Property

        ''' <summary>
        ''' Whether a person is a supplier of the company.
        ''' </summary>
        ''' <remarks>Value is stored in the database field asmenys.IsSupplier.</remarks>
        Public ReadOnly Property IsSupplier() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsSupplier
            End Get
        End Property

        ''' <summary>
        ''' Whether a person is a worker of the company.
        ''' </summary>
        ''' <remarks>Value is stored in the database field asmenys.IsWorker.</remarks>
        Public ReadOnly Property IsWorker() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsWorker
            End Get
        End Property


        Protected Overrides Function GetIdValue() As Object
            Return _ID
        End Function

        Public Overrides Function ToString() As String
            Return String.Format("{0} ({1}), {2}", _Name, _Code, _Address)
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function GetPersonInfoItem(ByVal dr As DataRow) As PersonInfoItem
            Return New PersonInfoItem(dr)
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
            _Code = CStrSafe(dr.Item(2)).Trim
            _Address = CStrSafe(dr.Item(3)).Trim
            _CodeVAT = CStrSafe(dr.Item(4)).Trim
            _BankAccount = CStrSafe(dr.Item(5)).Trim
            _Bank = CStrSafe(dr.Item(6)).Trim
            _AccountAgainstBankBuyer = CLongSafe(dr.Item(7), 0)
            _AccountAgainstBankSupplyer = CLongSafe(dr.Item(8), 0)
            _Email = CStrSafe(dr.Item(9)).Trim
            _CodeSODRA = CStrSafe(dr.Item(10)).Trim
            _ContactInfo = CStrSafe(dr.Item(11)).Trim
            _InternalCode = CStrSafe(dr.Item(12)).Trim
            _IsObsolete = ConvertDbBoolean(CIntSafe(dr.Item(13), 0))
            _IsNaturalPerson = ConvertDbBoolean(CIntSafe(dr.Item(14), 0))
            _LanguageCode = CStrSafe(dr.Item(15)).Trim
            _LanguageName = GetLanguageName(_LanguageCode, False)
            _CurrencyCode = CStrSafe(dr.Item(16)).Trim
            _IsClient = ConvertDbBoolean(CIntSafe(dr.Item(17), 0))
            _IsSupplier = ConvertDbBoolean(CIntSafe(dr.Item(18), 0))
            _IsWorker = ConvertDbBoolean(CIntSafe(dr.Item(19), 0))
            _StateCode = CStrSafe(dr.Item(20)).Trim
            _CodeIsNotReal = ConvertDbBoolean(CIntSafe(dr.Item(21), 0))

        End Sub

#End Region

    End Class

End Namespace