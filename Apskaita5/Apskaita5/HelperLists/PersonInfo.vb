Namespace HelperLists

    <Serializable()> _
    Public Class PersonInfo
        Inherits ReadOnlyBase(Of PersonInfo)
        Implements IComparable

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

        Public ReadOnly Property NameUserFriendly() As String
            Get
                If Not _ID > 0 AndAlso String.IsNullOrEmpty(_Name.Trim) Then Return ""
                If Not _ID > 0 Then Return "NEREGISTRUOTAS!!! " & _Name & " (" & _Code & ")"
                Return _Name & " (" & _Code & ")"
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


        Public ReadOnly Property GetMe() As PersonInfo
            Get
                Return Me
            End Get
        End Property


        Protected Overrides Function GetIdValue() As Object
            Return _ID
        End Function

        Public Overrides Function ToString() As String
            Return NameUserFriendly
        End Function

        Public Shared Operator =(ByVal a As PersonInfo, ByVal b As PersonInfo) As Boolean

            If a Is Nothing AndAlso b Is Nothing Then Return True
            If a Is Nothing OrElse b Is Nothing Then Return False

            Return a.ID = b.ID
        End Operator

        Public Shared Operator <>(ByVal a As PersonInfo, ByVal b As PersonInfo) As Boolean
            Return Not a = b
        End Operator

        Public Shared Operator >(ByVal a As PersonInfo, ByVal b As PersonInfo) As Boolean
            If a Is Nothing Then Return False
            If b Is Nothing Then Return True
            Return a.ToString > b.ToString
        End Operator

        Public Shared Operator <(ByVal a As PersonInfo, ByVal b As PersonInfo) As Boolean
            If a Is Nothing And b Is Nothing Then Return False
            If a Is Nothing Then Return True
            If b Is Nothing Then Return False
            Return a.ToString < b.ToString
        End Operator

        Public Function CompareTo(ByVal obj As Object) As Integer Implements System.IComparable.CompareTo
            Dim tmp As PersonInfo = TryCast(obj, PersonInfo)
            If Me = tmp Then Return 0
            If Me > tmp Then Return 1
            Return -1
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function GetPersonInfo(ByVal dr As DataRow, ByVal Offset As Integer) As PersonInfo
            Return New PersonInfo(dr, Offset)
        End Function

        Friend Shared Function GetPersonInfo(ByVal pID As Integer, ByVal pName As String, _
            ByVal pCode As String, ByVal pAdress As String, ByVal pCodeVAT As String, _
            ByVal pBank As String, ByVal pBankAccount As String, ByVal pCodeSODRA As String, _
            ByVal pEmail As String, ByVal pAccountAgainstBankSupplyer As Long, _
            ByVal pAccountAgainstBankBuyer As Long) As PersonInfo
            Return New PersonInfo(pID, pName, pCode, pAdress, pCodeVAT, pBank, pBankAccount, _
                pCodeSODRA, pEmail, pAccountAgainstBankSupplyer, pAccountAgainstBankBuyer)
        End Function

        Friend Shared Function GetPersonInfo(ByVal PersonID As Integer) As PersonInfo
            Dim result As New PersonInfo
            result._ID = PersonID
            Return result
        End Function

        Friend Shared Function GetEmptyPersonInfo() As PersonInfo
            Dim result As PersonInfo = New PersonInfo()
            Return result
        End Function

        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal dr As DataRow, ByVal Offset As Integer)
            Fetch(dr, Offset)
        End Sub

        Private Sub New(ByVal pID As Integer, ByVal pName As String, ByVal pCode As String, _
            ByVal pAdress As String, ByVal pCodeVAT As String, ByVal pBank As String, _
            ByVal pBankAccount As String, ByVal pCodeSODRA As String, ByVal pEmail As String, _
            ByVal pAccountAgainstBankSupplyer As Long, ByVal pAccountAgainstBankBuyer As Long)
            _ID = pID
            _Name = pName
            _Code = pCode
            _Address = pAdress
            _CodeVAT = pCodeVAT
            _Bank = pBank
            _BankAccount = pBankAccount
            _CodeSODRA = pCodeSODRA
            _Email = pEmail
            _AccountAgainstBankSupplyer = pAccountAgainstBankSupplyer
            _AccountAgainstBankBuyer = pAccountAgainstBankBuyer
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal dr As DataRow, ByVal Offset As Integer)

            _ID = CIntSafe(dr.Item(0 + Offset), 0)
            _Name = CStrSafe(dr.Item(1 + Offset)).Trim
            _Code = CStrSafe(dr.Item(2 + Offset)).Trim
            _Address = CStrSafe(dr.Item(3 + Offset)).Trim
            _CodeVAT = CStrSafe(dr.Item(4 + Offset)).Trim
            _BankAccount = CStrSafe(dr.Item(5 + Offset)).Trim
            _Bank = CStrSafe(dr.Item(6 + Offset)).Trim
            _AccountAgainstBankBuyer = CLongSafe(dr.Item(7 + Offset), 0)
            _AccountAgainstBankSupplyer = CLongSafe(dr.Item(8 + Offset), 0)
            _Email = CStrSafe(dr.Item(9 + Offset)).Trim
            _CodeSODRA = CStrSafe(dr.Item(10 + Offset)).Trim
            _ContactInfo = CStrSafe(dr.Item(11 + Offset)).Trim
            _InternalCode = CStrSafe(dr.Item(12 + Offset)).Trim
            _IsObsolete = ConvertDbBoolean(CIntSafe(dr.Item(13 + Offset), 0))
            _IsNaturalPerson = ConvertDbBoolean(CIntSafe(dr.Item(14 + Offset), 0))
            _LanguageCode = CStrSafe(dr.Item(15 + Offset)).Trim
            _LanguageName = GetLanguageName(_LanguageCode, False)
            _CurrencyCode = CStrSafe(dr.Item(16 + Offset)).Trim
            _IsClient = ConvertDbBoolean(CIntSafe(dr.Item(17 + Offset), 0))
            _IsSupplier = ConvertDbBoolean(CIntSafe(dr.Item(18 + Offset), 0))
            _IsWorker = ConvertDbBoolean(CIntSafe(dr.Item(19 + Offset), 0))

        End Sub

#End Region

    End Class

End Namespace