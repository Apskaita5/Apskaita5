<Serializable()> _
Public Class ClientInfo

    Private _ID As String = ""
    Private _Name As String = ""
    Private _Code As String = ""
    Private _CodeVAT As String = ""
    Private _Address As String = ""
    Private _CurrencyCode As String = ""
    Private _LanguageCode As String = ""
    Private _VatExemption As String = ""
    Private _VatExemptionAltLng As String = ""
    Private _IsObsolete As Boolean = False
    Private _Email As String = ""
    Private _Contacts As String = ""
    Private _BalanceAtBegining As Double = 0
    Private _IsClient As Boolean = False
    Private _IsSupplier As Boolean = False
    Private _IsNaturalPerson As Boolean = False
    Private _IsCodeLocal As Boolean = False
    Private _BreedCode As String = ""
    Private _IsWorker As Boolean = False
    Private _ExternalID As String = ""


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

    Public Property Name() As String
        Get
            Return _Name.Trim
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then value = ""
            If _Name.Trim <> value.Trim Then
                _Name = value.Trim
            End If
        End Set
    End Property

    Public Property Code() As String
        Get
            Return _Code.Trim
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then value = ""
            If _Code.Trim <> value.Trim Then
                _Code = value.Trim
            End If
        End Set
    End Property

    Public Property CodeVAT() As String
        Get
            Return _CodeVAT.Trim
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then value = ""
            If _CodeVAT.Trim <> value.Trim Then
                _CodeVAT = value.Trim
            End If
        End Set
    End Property

    Public Property Address() As String
        Get
            Return _Address.Trim
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then value = ""
            If _Address.Trim <> value.Trim Then
                _Address = value.Trim
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

    Public Property VatExemption() As String
        Get
            Return _VatExemption.Trim
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then value = ""
            If _VatExemption.Trim <> value.Trim Then
                _VatExemption = value.Trim
            End If
        End Set
    End Property

    Public Property VatExemptionAltLng() As String
        Get
            Return _VatExemptionAltLng.Trim
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then value = ""
            If _VatExemptionAltLng.Trim <> value.Trim Then
                _VatExemptionAltLng = value.Trim
            End If
        End Set
    End Property

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

    Public Property Email() As String
        Get
            Return _Email.Trim
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then value = ""
            If _Email.Trim <> value.Trim Then
                _Email = value.Trim
            End If
        End Set
    End Property

    Public Property Contacts() As String
        Get
            Return _Contacts.Trim
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then value = ""
            If _Contacts.Trim <> value.Trim Then
                _Contacts = value.Trim
            End If
        End Set
    End Property

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

    Public Property BreedCode() As String
        Get
            Return _BreedCode.Trim
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then value = ""
            If _BreedCode.Trim <> value.Trim Then
                _BreedCode = value.Trim
            End If
        End Set
    End Property

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


    Public Sub New()

    End Sub

End Class
