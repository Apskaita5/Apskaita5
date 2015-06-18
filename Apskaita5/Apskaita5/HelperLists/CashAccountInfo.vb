Imports ApskaitaObjects.Documents
Namespace HelperLists

    ''' <summary>
    ''' Represents a <see cref="Documents.CashAccount">cash account</see> value object.
    ''' </summary>
    ''' <remarks>Values are stored in the database table cashaccounts.</remarks>
    <Serializable()> _
    Public Class CashAccountInfo
        Inherits ReadOnlyBase(Of CashAccountInfo)
        Implements IComparable, IValueObjectIsEmpty

#Region " Business Methods "

        Private _ID As Integer = 0
        Private _Type As CashAccountType = CashAccountType.BankAccount
        Private _ManagingPersonID As Integer = 0
        Private _BankFeeCostsAccount As Long = 0
        Private _Account As Long = 0
        Private _Name As String = ""
        Private _BankAccountNumber As String = ""
        Private _BankName As String = ""
        Private _BankCode As String = ""
        Private _IsLitasEsisCompliant As Boolean = False
        Private _CurrencyCode As String = GetCurrentCompany.BaseCurrency
        Private _EnforceUniqueOperationID As Boolean = False
        Private _BankFeeLimit As Integer = 0
        Private _IsObsolete As Boolean = False


        ''' <summary>
        ''' Whether an object is a place holder (does not represent a real cash account).
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
        ''' Gets an ID of the cash account that is assigned by a database (AUTOINCREMENT).
        ''' </summary>
        ''' <remarks>Value is stored in the database table cashaccounts.ID.</remarks>
        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' Gets <see cref="CashAccountType">a type of the cash account</see>.
        ''' </summary>
        ''' <remarks>Value is stored in the database table cashaccounts.TypeID.</remarks>
        Public ReadOnly Property [Type]() As CashAccountType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Type
            End Get
        End Property

        ''' <summary>
        ''' Gets <see cref="General.Account.ID">an account</see> for the cash account.
        ''' </summary>
        ''' <remarks>Value is stored in the database table cashaccounts.Account.</remarks>
        Public ReadOnly Property Account() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Account
            End Get
        End Property

        ''' <summary>
        ''' Gets <see cref="General.Account.ID">an account</see> for the bank fee costs.
        ''' </summary>
        ''' <remarks>Value is stored in the database table cashaccounts.Account.</remarks>
        Public ReadOnly Property BankFeeCostsAccount() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _BankFeeCostsAccount
            End Get
        End Property

        ''' <summary>
        ''' Gets an ID of the person that is responsible for the cash account administration 
        ''' (e.g. some bank, PayPal, etc.).
        ''' </summary>
        ''' <remarks>Value is stored in the database table cashaccounts.ManagingPersonID.</remarks>
        Public ReadOnly Property ManagingPersonID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ManagingPersonID
            End Get
        End Property

        ''' <summary>
        ''' Gets a name of the cash account. 
        ''' </summary>
        ''' <remarks>Value is stored in the database table cashaccounts.Name.</remarks>
        Public ReadOnly Property Name() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Name.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a number of the cash account (e.g. IBAN).
        ''' </summary>
        ''' <remarks>Value is stored in the database table cashaccounts.BankAccountNumber.</remarks>
        Public ReadOnly Property BankAccountNumber() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _BankAccountNumber.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a name of the bank that is administering the cash account.
        ''' </summary>
        ''' <remarks>Value is stored in the database table cashaccounts.BankName.</remarks>
        Public ReadOnly Property BankName() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _BankName.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a company registration code of the bank that is administering the cash account.
        ''' </summary>
        ''' <remarks>Value is stored in the database table cashaccounts.BankCode.</remarks>
        Public ReadOnly Property BankCode() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _BankCode.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets whether the bank, that is administering the cash account, 
        ''' supports LITAS-ESIS standard for electronic bank data.
        ''' </summary>
        ''' <remarks>Value is stored in the database table cashaccounts.BankCode.</remarks>
        Public ReadOnly Property IsLitasEsisCompliant() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsLitasEsisCompliant
            End Get
        End Property

        ''' <summary>
        ''' Gets the currency of the cash account.
        ''' </summary>
        ''' <remarks>Value is stored in the database table cashaccounts.CurrencyCode.</remarks>
        Public ReadOnly Property CurrencyCode() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _CurrencyCode.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets whether to enforce the uniqueness of <see cref="BankOperation.UniqueCode">BankOperation.UniqueCode</see>.
        ''' </summary>
        ''' <remarks>Value is stored in the database table cashaccounts.EnforceUniqueOperationID.</remarks>
        Public ReadOnly Property EnforceUniqueOperationID() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _EnforceUniqueOperationID
            End Get
        End Property

        ''' <summary>
        ''' Gets an amount of a withdrawal that is treated as a bank fee when importing data.
        ''' </summary>
        ''' <remarks>Value is stored in the database table cashaccounts.BankFeeLimit.</remarks>
        Public ReadOnly Property BankFeeLimit() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _BankFeeLimit
            End Get
        End Property

        ''' <summary>
        ''' Gets whether the cash account is obsolete, no longer in use.
        ''' </summary>
        ''' <remarks>Value is stored in the database table cashaccounts.IsObsolete.</remarks>
        Public ReadOnly Property IsObsolete() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsObsolete
            End Get
        End Property



        Public Shared Operator =(ByVal a As CashAccountInfo, ByVal b As CashAccountInfo) As Boolean
            If a Is Nothing AndAlso b Is Nothing Then Return True
            If a Is Nothing OrElse b Is Nothing Then Return False
            Return a.ID = b.ID
        End Operator

        Public Shared Operator <>(ByVal a As CashAccountInfo, ByVal b As CashAccountInfo) As Boolean
            Return Not a = b
        End Operator

        Public Shared Operator >(ByVal a As CashAccountInfo, ByVal b As CashAccountInfo) As Boolean
            If a Is Nothing Then Return False
            If a IsNot Nothing And b Is Nothing Then Return True
            Return a.ToString > b.ToString
        End Operator

        Public Shared Operator <(ByVal a As CashAccountInfo, ByVal b As CashAccountInfo) As Boolean
            If a Is Nothing And b Is Nothing Then Return False
            If a Is Nothing Then Return True
            If b Is Nothing Then Return False
            Return a.ToString < b.ToString
        End Operator

        Public Function CompareTo(ByVal obj As Object) As Integer _
        Implements System.IComparable.CompareTo
            Dim tmp As CashAccountInfo = TryCast(obj, CashAccountInfo)
            If Me = tmp Then Return 0
            If Me > tmp Then Return 1
            Return -1
        End Function


        Protected Overrides Function GetIdValue() As Object
            Return _ID
        End Function

        Public Overrides Function ToString() As String
            Return String.Format(My.Resources.HelperLists_CashAccountInfo_ToString, _
                _Account.ToString, _CurrencyCode.Trim.ToUpper, _Name)
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function GetCashAccountInfo(ByVal dr As DataRow, ByVal Offset As Integer) As CashAccountInfo
            Return New CashAccountInfo(dr, Offset)
        End Function

        Friend Shared Function NewCashAccountInfo() As CashAccountInfo
            Return New CashAccountInfo()
        End Function


        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal dr As DataRow, ByVal Offset As Integer)
            Fetch(dr, Offset)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal dr As DataRow, ByVal offset As Integer)

            _ID = CIntSafe(dr.Item(0 + offset), 0)
            _Name = CStrSafe(dr.Item(1 + offset)).Trim
            _Account = CLongSafe(dr.Item(2 + offset), 0)
            _BankAccountNumber = CStrSafe(dr.Item(3 + offset)).Trim
            _BankName = CStrSafe(dr.Item(4 + offset)).Trim
            _BankCode = CStrSafe(dr.Item(5 + offset)).Trim
            _IsLitasEsisCompliant = ConvertDbBoolean(CIntSafe(dr.Item(6 + offset), 0))
            _CurrencyCode = CStrSafe(dr.Item(7 + offset)).Trim
            _EnforceUniqueOperationID = ConvertDbBoolean(CIntSafe(dr.Item(8 + offset), 0))
            _BankFeeLimit = CIntSafe(dr.Item(9 + offset), 0)
            _Type = EnumValueAttribute.ConvertDatabaseID(Of CashAccountType)(CIntSafe(dr.Item(10 + offset), 0))
            _IsObsolete = ConvertDbBoolean(CIntSafe(dr.Item(11 + offset), 0))
            _BankFeeCostsAccount = CLongSafe(dr.Item(12 + offset), 0)
            _ManagingPersonID = CIntSafe(dr.Item(13 + offset), 0)

        End Sub

#End Region

    End Class

End Namespace