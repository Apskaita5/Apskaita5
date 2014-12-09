Imports ApskaitaObjects.Documents
Namespace HelperLists

    <Serializable()> _
    Public Class CashAccountInfo
        Inherits ReadOnlyBase(Of CashAccountInfo)
        Implements IComparable

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


        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        Public ReadOnly Property [Type]() As CashAccountType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Type
            End Get
        End Property

        Public ReadOnly Property Account() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Account
            End Get
        End Property

        Public ReadOnly Property BankFeeCostsAccount() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _BankFeeCostsAccount
            End Get
        End Property

        Public ReadOnly Property ManagingPersonID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ManagingPersonID
            End Get
        End Property

        Public ReadOnly Property Name() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Name.Trim
            End Get
        End Property

        Public ReadOnly Property BankAccountNumber() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _BankAccountNumber.Trim
            End Get
        End Property

        Public ReadOnly Property BankName() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _BankName.Trim
            End Get
        End Property

        Public ReadOnly Property BankCode() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _BankCode.Trim
            End Get
        End Property

        Public ReadOnly Property IsLitasEsisCompliant() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsLitasEsisCompliant
            End Get
        End Property

        Public ReadOnly Property CurrencyCode() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _CurrencyCode.Trim
            End Get
        End Property

        Public ReadOnly Property EnforceUniqueOperationID() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _EnforceUniqueOperationID
            End Get
        End Property

        Public ReadOnly Property BankFeeLimit() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _BankFeeLimit
            End Get
        End Property

        Public ReadOnly Property IsObsolete() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsObsolete
            End Get
        End Property

        Public ReadOnly Property GetMe() As CashAccountInfo
            Get
                Return Me
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
            If Not _ID > 0 Then Return ""
            Return _Account.ToString & " (" & _CurrencyCode.Trim.ToUpper & ") - " & _Name
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

        Private Sub Fetch(ByVal dr As DataRow, ByVal Offset As Integer)

            _ID = CIntSafe(dr.Item(0 + Offset), 0)
            _Name = CStrSafe(dr.Item(1 + Offset)).Trim
            _Account = CLongSafe(dr.Item(2 + Offset), 0)
            _BankAccountNumber = CStrSafe(dr.Item(3 + Offset)).Trim
            _BankName = CStrSafe(dr.Item(4 + Offset)).Trim
            _BankCode = CStrSafe(dr.Item(5 + Offset)).Trim
            _IsLitasEsisCompliant = ConvertDbBoolean(CIntSafe(dr.Item(6 + Offset), 0))
            _CurrencyCode = CStrSafe(dr.Item(7 + Offset)).Trim
            _EnforceUniqueOperationID = ConvertDbBoolean(CIntSafe(dr.Item(8 + Offset), 0))
            _BankFeeLimit = CIntSafe(dr.Item(9 + Offset), 0)
            _Type = ConvertEnumDatabaseCode(Of CashAccountType)(CIntSafe(dr.Item(10 + Offset), 0))
            _IsObsolete = ConvertDbBoolean(CIntSafe(dr.Item(11 + Offset), 0))
            _BankFeeCostsAccount = CLongSafe(dr.Item(12 + Offset), 0)
            _ManagingPersonID = CIntSafe(dr.Item(13 + Offset), 0)

        End Sub

#End Region

    End Class

End Namespace