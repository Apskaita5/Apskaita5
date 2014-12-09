Namespace ActiveReports

    <Serializable()> _
    Public Class PersonInfoItem
        Inherits ReadOnlyBase(Of PersonInfoItem)

#Region " Business Methods "

        Private _ID As Integer = 0
        Private _Name As String = ""
        Private _Code As String = ""
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
        Private _IsNaturalPerson As Boolean = False
        Private _IsObsolete As Boolean = False
        Private _IsClient As Boolean = False
        Private _IsSupplier As Boolean = False
        Private _IsWorker As Boolean = False


        Public ReadOnly Property ID() As Integer
            Get
                Return _ID
            End Get
        End Property

        Public ReadOnly Property Name() As String
            Get
                Return _Name
            End Get
        End Property

        Public ReadOnly Property Code() As String
            Get
                Return _Code
            End Get
        End Property

        Public ReadOnly Property Address() As String
            Get
                Return _Address
            End Get
        End Property

        Public ReadOnly Property Bank() As String
            Get
                Return _Bank
            End Get
        End Property

        Public ReadOnly Property BankAccount() As String
            Get
                Return _BankAccount
            End Get
        End Property

        Public ReadOnly Property CodeVAT() As String
            Get
                Return _CodeVAT
            End Get
        End Property

        Public ReadOnly Property CodeSODRA() As String
            Get
                Return _CodeSODRA
            End Get
        End Property

        Public ReadOnly Property Email() As String
            Get
                Return _Email
            End Get
        End Property

        ''' <summary>
        ''' Gets any other person info, e.g. phone number, etc.
        ''' </summary>
        Public ReadOnly Property ContactInfo() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ContactInfo.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets an internal code of the person for company's uses.
        ''' </summary>
        Public ReadOnly Property InternalCode() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _InternalCode.Trim
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

        Public ReadOnly Property CurrencyCode() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _CurrencyCode.Trim
            End Get
        End Property

        Public ReadOnly Property AccountAgainstBankBuyer() As Long
            Get
                Return _AccountAgainstBankBuyer
            End Get
        End Property

        Public ReadOnly Property AccountAgainstBankSupplyer() As Long
            Get
                Return _AccountAgainstBankSupplyer
            End Get
        End Property

        ''' <summary>
        ''' Gets if the person is a natural person, i.e. not a company.
        ''' </summary>
        Public ReadOnly Property IsNaturalPerson() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsNaturalPerson
            End Get
        End Property

        ''' <summary>
        ''' Gets if the person is no longer in use, i.e. not supposed to be displayed in combos.
        ''' </summary>
        Public ReadOnly Property IsObsolete() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsObsolete
            End Get
        End Property

        Public ReadOnly Property IsClient() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsClient
            End Get
        End Property

        Public ReadOnly Property IsSupplier() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsSupplier
            End Get
        End Property

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
            Return _Name & " (" & _Code & ")"
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

        End Sub

#End Region

    End Class

End Namespace