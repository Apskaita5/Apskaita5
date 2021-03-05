<Serializable()> _
Public Class ClientInfo

#Region " Private Backing Fields "

    Private _ID As String = String.Empty
    Private _Name As String = String.Empty
    Private _Code As String = String.Empty
    Private _CodeVAT As String = String.Empty
    Private _Address As String = String.Empty
    Private _CurrencyCode As String = String.Empty
    Private _LanguageCode As String = String.Empty
    Private _CountryCode As String = String.Empty
    Private _VatExemption As String = String.Empty
    Private _VatExemptionAltLng As String = String.Empty
    Private _IsObsolete As Boolean = False
    Private _Email As String = String.Empty
    Private _Contacts As String = String.Empty
    Private _BankAccount As String = String.Empty
    Private _BankName As String = String.Empty
    Private _BalanceAtBegining As Double = 0.0
    Private _IsClient As Boolean = False
    Private _IsSupplier As Boolean = False
    Private _IsNaturalPerson As Boolean = False
    Private _IsCodeLocal As Boolean = False
    Private _BreedCode As String = String.Empty
    Private _IsWorker As Boolean = False
    Private _ExternalID As String = String.Empty

#End Region

    ''' <summary>
    ''' Gets or sets an ID of the person in the source system.
    ''' </summary>
    ''' <remarks>Depends on the target system, usually optional.
    ''' Used to determine whether the person has been already imported
    ''' and needs to be updated or the person should be added as a new one.
    ''' As the ids are commonly defined as integers, it is recommended to prefix
    ''' them with a system id in order to differentiate between multiple source systems.
    ''' Person data are usually NOT updated on target systems, therefore this ID
    ''' is rarely used.</remarks>
    Public Property ID() As String
        Get
            Return _ID.Trim
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then value = String.Empty
            If _ID.Trim <> value.Trim Then
                _ID = value.Trim
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets an official name of the person.
    ''' </summary>
    ''' <remarks>Required.</remarks>
    Public Property Name() As String
        Get
            Return _Name.Trim
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then value = String.Empty
            If _Name.Trim <> value.Trim Then
                _Name = value.Trim
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets an official registration/personal code of the person.
    ''' </summary>
    ''' <remarks>Required.
    ''' It is typically used to check if the person data is already present on the target system.</remarks>
    Public Property Code() As String
        Get
            Return _Code.Trim
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then value = String.Empty
            If _Code.Trim <> value.Trim Then
                _Code = value.Trim
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a VAT payer code of the person.
    ''' </summary>
    ''' <remarks>Optional.</remarks>
    Public Property CodeVAT() As String
        Get
            Return _CodeVAT.Trim
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then value = String.Empty
            If _CodeVAT.Trim <> value.Trim Then
                _CodeVAT = value.Trim
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a (non-structured) address of the person.
    ''' </summary>
    ''' <remarks>Optional.</remarks>
    Public Property Address() As String
        Get
            Return _Address.Trim
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then value = String.Empty
            If _Address.Trim <> value.Trim Then
                _Address = value.Trim
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets an ISO4217 currency code (3-letter) for the currency used by the person.
    ''' </summary>
    ''' <remarks>Optional.
    ''' Empty or null string corresponds to the base currency of the target system.</remarks>
    Public Property CurrencyCode() As String
        Get
            Return _CurrencyCode.Trim
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then value = String.Empty
            If _CurrencyCode.Trim <> value.Trim Then
                _CurrencyCode = value.Trim
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets an ISO 639-1 language code (2-letter) for the original language
    ''' that the invoices for the person shall be issued in.
    ''' </summary>
    ''' <remarks>Optional. (defaults to the base language for the target system)</remarks>
    Public Property LanguageCode() As String
        Get
            Return _LanguageCode.Trim
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then value = String.Empty
            If _LanguageCode.Trim <> value.Trim Then
                _LanguageCode = value.Trim
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets an ISO 3166-1 country code (2-letter) for the country of residence.
    ''' </summary>
    ''' <remarks>Required.</remarks>
    Public Property CountryCode() As String
        Get
            Return _CountryCode.Trim
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then value = String.Empty
            If _CountryCode.Trim <> value.Trim Then
                _CountryCode = value.Trim
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a No of bank account of the person (usually IBAN).
    ''' </summary>
    ''' <remarks>Optional.</remarks>
    Public Property BankAccount() As String
        Get
            Return _BankAccount.Trim
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then value = String.Empty
            If _BankAccount.Trim <> value.Trim Then
                _BankAccount = value.Trim
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a name of the bank of the person (usually IBAN).
    ''' </summary>
    ''' <remarks>Optional.</remarks>
    Public Property BankName() As String
        Get
            Return _BankName.Trim
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then value = String.Empty
            If _BankName.Trim <> value.Trim Then
                _BankName = value.Trim
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a default VAT exempt text in target system base language
    ''' that should be printed on invoices for the person.
    ''' </summary>
    ''' <remarks>Optional.</remarks>
    Public Property VatExemption() As String
        Get
            Return _VatExemption.Trim
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then value = String.Empty
            If _VatExemption.Trim <> value.Trim Then
                _VatExemption = value.Trim
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a default VAT exempt text in <see cref="LanguageCode">person preferred language</see>
    ''' that should be printed on invoices for the person.
    ''' </summary>
    ''' <remarks>Optional.</remarks>
    Public Property VatExemptionAltLng() As String
        Get
            Return _VatExemptionAltLng.Trim
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then value = String.Empty
            If _VatExemptionAltLng.Trim <> value.Trim Then
                _VatExemptionAltLng = value.Trim
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a value indicating whether the person data is archived (no longer actively used).
    ''' </summary>
    Public Property IsObsolete() As Boolean
        Get
            Return _IsObsolete
        End Get
        Set(ByVal value As Boolean)
            If _IsObsolete <> value Then
                _IsObsolete = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets an email address of the person.
    ''' </summary>
    ''' <remarks>Optional.</remarks>
    Public Property Email() As String
        Get
            Return _Email.Trim
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then value = String.Empty
            If _Email.Trim <> value.Trim Then
                _Email = value.Trim
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a contact info of the person (phone, web site etc.).
    ''' </summary>
    ''' <remarks>Optional.</remarks>
    Public Property Contacts() As String
        Get
            Return _Contacts.Trim
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then value = String.Empty
            If _Contacts.Trim <> value.Trim Then
                _Contacts = value.Trim
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a person "inherited" debt amount.
    ''' </summary>
    ''' <remarks>Optional.</remarks>
    Public Property BalanceAtBegining() As Double
        Get
            Return _BalanceAtBegining
        End Get
        Set(ByVal value As Double)
            If _BalanceAtBegining <> value Then
                _BalanceAtBegining = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a value indicating whether the person is a client of the company.
    ''' </summary>
    Public Property IsClient() As Boolean
        Get
            Return _IsClient
        End Get
        Set(ByVal value As Boolean)
            If _IsClient <> value Then
                _IsClient = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a value indicating whether the person is a supplier of the company.
    ''' </summary>
    Public Property IsSupplier() As Boolean
        Get
            Return _IsSupplier
        End Get
        Set(ByVal value As Boolean)
            If _IsSupplier <> value Then
                _IsSupplier = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a value indicating whether the person is a natural person
    ''' (as opposed to company/organization).
    ''' </summary>
    Public Property IsNaturalPerson() As Boolean
        Get
            Return _IsNaturalPerson
        End Get
        Set(ByVal value As Boolean)
            If _IsNaturalPerson <> value Then
                _IsNaturalPerson = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a value indicating whether the <see cref="Code">person code</see>
    ''' is NOT a valid national/legal ID.
    ''' </summary>
    Public Property IsCodeLocal() As Boolean
        Get
            Return _IsCodeLocal
        End Get
        Set(ByVal value As Boolean)
            If _IsCodeLocal <> value Then
                _IsCodeLocal = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a person code that systems might use to group persons by.
    ''' </summary>
    ''' <remarks>Optional.</remarks>
    Public Property BreedCode() As String
        Get
            Return _BreedCode.Trim
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then value = String.Empty
            If _BreedCode.Trim <> value.Trim Then
                _BreedCode = value.Trim
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets a value indicating whether the person is an employee of the company.
    ''' </summary>
    Public Property IsWorker() As Boolean
        Get
            Return _IsWorker
        End Get
        Set(ByVal value As Boolean)
            If _IsWorker <> value Then
                _IsWorker = value
            End If
        End Set
    End Property

    ''' <summary>
    ''' Gets or sets an original ID of the person (if the source system is not an original owner).
    ''' </summary>
    ''' <remarks>Optional.</remarks>
    Public Property ExternalID() As String
        Get
            Return _ExternalID.Trim
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then value = String.Empty
            If _ExternalID.Trim <> value.Trim Then
                _ExternalID = value.Trim
            End If
        End Set
    End Property

End Class