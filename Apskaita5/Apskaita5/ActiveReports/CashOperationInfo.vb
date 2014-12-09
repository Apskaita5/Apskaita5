Namespace ActiveReports

    <Serializable()> _
    Public Class CashOperationInfo
        Inherits ReadOnlyBase(Of CashOperationInfo)

#Region " Business Methods "

        Private _ID As Integer = 0
        Private _OperationType As DocumentType = DocumentType.None
        Private _OperationTypeHumanReadable As String = ""
        Private _AccountName As String = ""
        Private _AccountCurrency As String = ""
        Private _Date As Date = Today
        Private _DocumentNumber As String = ""
        Private _UniqueCode As String = ""
        Private _Content As String = ""
        Private _OriginalContent As String = ""
        Private _Person As String = ""
        Private _OriginalPerson As String = ""
        Private _CurrencyCode As String = GetCurrentCompany.BaseCurrency
        Private _CurrencyRate As Double = 1
        Private _Sum As Double = 0
        Private _SumLTL As Double = 0
        Private _SumBookEntry As Double = 0
        Private _SumInAccount As Double = 0
        Private _CurrencyRateInAccount As Double = 0
        Private _CurrencyRateChangeImpact As Double = 0
        Private _BankCurrencyConversionCosts As Double = 0
        Private _BankCurrencyConversionCostsLTL As Double = 0
        Private _BookEntryList As String = ""


        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        Public ReadOnly Property AccountName() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AccountName.Trim
            End Get
        End Property

        Public ReadOnly Property OperationType() As DocumentType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _OperationType
            End Get
        End Property

        Public ReadOnly Property OperationTypeHumanReadable() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _OperationTypeHumanReadable.Trim
            End Get
        End Property

        Public ReadOnly Property [Date]() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Date
            End Get
        End Property

        Public ReadOnly Property DocumentNumber() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DocumentNumber.Trim
            End Get
        End Property

        Public ReadOnly Property UniqueCode() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _UniqueCode.Trim
            End Get
        End Property

        Public ReadOnly Property Content() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Content.Trim
            End Get
        End Property

        Public ReadOnly Property OriginalContent() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _OriginalContent.Trim
            End Get
        End Property

        Public ReadOnly Property Person() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Person.Trim
            End Get
        End Property

        Public ReadOnly Property OriginalPerson() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _OriginalPerson.Trim
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

        Public ReadOnly Property Sum() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_Sum)
            End Get
        End Property

        Public ReadOnly Property SumLTL() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_SumLTL)
            End Get
        End Property

        Public ReadOnly Property SumBookEntry() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_SumBookEntry)
            End Get
        End Property

        Public ReadOnly Property SumInAccount() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_SumInAccount)
            End Get
        End Property

        Public ReadOnly Property CurrencyRateInAccount() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_CurrencyRateInAccount, 6)
            End Get
        End Property

        Public ReadOnly Property CurrencyRateChangeImpact() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_CurrencyRateChangeImpact)
            End Get
        End Property

        Public ReadOnly Property BankCurrencyConversionCosts() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_BankCurrencyConversionCosts)
            End Get
        End Property

        Public ReadOnly Property BankCurrencyConversionCostsLTL() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_BankCurrencyConversionCostsLTL)
            End Get
        End Property

        Public ReadOnly Property BookEntryList() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _BookEntryList.Trim
            End Get
        End Property


        Protected Overrides Function GetIdValue() As Object
            Return _ID
        End Function

        Public Overrides Function ToString() As String
            If Not _ID > 0 Then Return ""
            Return _Content
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function GetCashOperationInfo(ByVal dr As DataRow) As CashOperationInfo
            Return New CashOperationInfo(dr)
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
            _OperationType = ConvertEnumDatabaseStringCode(Of DocumentType)(CStrSafe(dr.Item(1)).Trim)
            _OperationTypeHumanReadable = ConvertEnumHumanReadable(_OperationType)
            _AccountName = CStrSafe(dr.Item(2)).Trim
            _AccountCurrency = CStrSafe(dr.Item(3)).Trim.ToUpper
            _Date = CDateSafe(dr.Item(4), Today)
            _DocumentNumber = CStrSafe(dr.Item(5)).Trim
            _UniqueCode = CStrSafe(dr.Item(6)).Trim
            _Content = CStrSafe(dr.Item(7)).Trim
            _Person = CStrSafe(dr.Item(8)).Trim
            If Not String.IsNullOrEmpty(_Person) Then _Person = _Person _
                & " (" & CStrSafe(dr.Item(9)).Trim & ")"
            _CurrencyCode = CStrSafe(dr.Item(10)).Trim
            _CurrencyRate = CDblSafe(dr.Item(11), 6, 0)
            _SumBookEntry = CDblSafe(dr.Item(12), 2, 0)
            _Sum = CDblSafe(dr.Item(13), 2, 0)
            _SumLTL = CDblSafe(dr.Item(14), 2, 0)
            _SumInAccount = CDblSafe(dr.Item(15), 2, 0)
            _CurrencyRateInAccount = CDblSafe(dr.Item(16), 6, 0)
            _CurrencyRateChangeImpact = CDblSafe(dr.Item(17), 6, 0)
            _OriginalContent = CStrSafe(dr.Item(18)).Trim
            _OriginalPerson = CStrSafe(dr.Item(19)).Trim
            _BookEntryList = CStrSafe(dr.Item(20)).Trim

            If _AccountCurrency.Trim.ToUpper = _CurrencyCode.Trim.ToUpper Then
                _BankCurrencyConversionCosts = 0
            ElseIf _AccountCurrency.Trim.ToUpper = GetCurrentCompany.BaseCurrency Then
                _BankCurrencyConversionCosts = CRound(CRound(_SumLTL) - CRound(_SumInAccount))
            Else
                If CRound(_CurrencyRateInAccount, 6) > 0 Then
                    _BankCurrencyConversionCosts = CRound(CRound(_SumLTL / _CurrencyRateInAccount) _
                        - CRound(_SumInAccount))
                Else
                    _BankCurrencyConversionCosts = 0
                End If
            End If
            _BankCurrencyConversionCostsLTL = CRound(_BankCurrencyConversionCosts * _CurrencyRateInAccount)

        End Sub

#End Region

    End Class

End Namespace
