Namespace Documents.BankDataExchangeProviders

    ''' <summary>
    ''' Represents abstract bank transaction data in a bank account statement.
    ''' Acts as a canonical data model for any bank account statement data.
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public NotInheritable Class BankAccountStatementItem

        Private _Date As Date = Today
        Private _DocumentNumber As String = ""
        Private _PersonCode As String = ""
        Private _PersonName As String = ""
        Private _PersonBankAccount As String = ""
        Private _PersonBankName As String = ""
        Private _Content As String = ""
        Private _Inflow As Boolean = False
        Private _Currency As String = ""
        Private _CurrencyRate As Double = 0
        Private _OriginalSum As Double = 0
        Private _SumInAccount As Double = 0
        Private _SumLTL As Double = 0
        Private _UniqueCode As String = ""


        ''' <summary>
        ''' Gets or sets the date of the bank operation.
        ''' </summary>
        ''' <remarks></remarks>
        Public Property [Date]() As Date
            Get
                Return _Date
            End Get
            Set(ByVal value As Date)
                If _Date.Date <> value.Date Then
                    _Date = value.Date
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the bank operation number.
        ''' </summary>
        ''' <remarks></remarks>
        Public Property DocumentNumber() As String
            Get
                Return _DocumentNumber.Trim
            End Get
            Set(ByVal value As String)
                If value Is Nothing Then value = ""
                If _DocumentNumber.Trim <> value.Trim Then
                    _DocumentNumber = value.Trim
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the original person code as it is specified 
        ''' in the bank account statement (import source).
        ''' </summary>
        ''' <remarks></remarks>
        Public Property PersonCode() As String
            Get
                Return _PersonCode.Trim
            End Get
            Set(ByVal value As String)
                If value Is Nothing Then value = ""
                If _PersonCode.Trim <> value.Trim Then
                    _PersonCode = value.Trim
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the original person name as it is specified 
        ''' in the bank account statement (import source).
        ''' </summary>
        ''' <remarks></remarks>
        Public Property PersonName() As String
            Get
                Return _PersonName.Trim
            End Get
            Set(ByVal value As String)
                If value Is Nothing Then value = ""
                If _PersonName.Trim <> value.Trim Then
                    _PersonName = value.Trim
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the original person bank account (IBAN) as it is specified 
        ''' in the bank account statement (import source).
        ''' </summary>
        ''' <remarks></remarks>
        Public Property PersonBankAccount() As String
            Get
                Return _PersonBankAccount.Trim
            End Get
            Set(ByVal value As String)
                If value Is Nothing Then value = ""
                If _PersonBankAccount.Trim <> value.Trim Then
                    _PersonBankAccount = value.Trim
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the original person bank name as it is specified 
        ''' in the bank account statement (import source).
        ''' </summary>
        ''' <remarks></remarks>
        Public Property PersonBankName() As String
            Get
                Return _PersonBankName.Trim
            End Get
            Set(ByVal value As String)
                If value Is Nothing Then value = ""
                If _PersonBankName.Trim <> value.Trim Then
                    _PersonBankName = value.Trim
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the description of a bank operation as it is specified 
        ''' in the bank account statement (import source).
        ''' </summary>
        ''' <remarks></remarks>
        Public Property Content() As String
            Get
                Return _Content.Trim
            End Get
            Set(ByVal value As String)
                If value Is Nothing Then value = ""
                If _Content.Trim <> value.Trim Then
                    _Content = value.Trim
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets whether the money is transfered into the bank account..
        ''' </summary>
        ''' <remarks></remarks>
        Public Property Inflow() As Boolean
            Get
                Return _Inflow
            End Get
            Set(ByVal value As Boolean)
                If _Inflow <> value Then
                    _Inflow = value
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the original bank operation currency.
        ''' </summary>
        ''' <remarks></remarks>
        Public Property Currency() As String
            Get
                Return _Currency.Trim
            End Get
            Set(ByVal value As String)
                If value Is Nothing Then value = ""
                If _Currency.Trim <> value.Trim Then
                    _Currency = value.Trim
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a currency rate for the money transfered that was applied by the bank
        ''' (a bank proprietary rate, not official).
        ''' </summary>
        ''' <remarks></remarks>
        Public Property CurrencyRate() As Double
            Get
                Return CRound(_CurrencyRate, ROUNDCURRENCYRATE)
            End Get
            Set(ByVal value As Double)
                If CRound(_CurrencyRate, ROUNDCURRENCYRATE) <> CRound(value, ROUNDCURRENCYRATE) Then
                    _CurrencyRate = CRound(value, ROUNDCURRENCYRATE)
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the (original) sum of bank transfer in <see cref="Currency">Currency</see>.
        ''' </summary>
        ''' <remarks></remarks>
        Public Property OriginalSum() As Double
            Get
                Return CRound(_OriginalSum)
            End Get
            Set(ByVal value As Double)
                If CRound(_OriginalSum) <> CRound(value) Then
                    _OriginalSum = CRound(value)
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the sum of bank transfer in <see cref="BankOperationItemList.Account">Account Currency</see>,
        ''' i.e. the sum that was actualy added to the acount balance in the account currency.
        ''' </summary>
        ''' <remarks></remarks>
        Public Property SumInAccount() As Double
            Get
                Return CRound(_SumInAccount)
            End Get
            Set(ByVal value As Double)
                If CRound(_SumInAccount) <> CRound(value) Then
                    _SumInAccount = CRound(value)
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the sum of bank transfer in base currency as it is specified 
        ''' in the bank account statement (import source).
        ''' </summary>
        ''' <remarks></remarks>
        Public Property SumLTL() As Double
            Get
                Return CRound(_SumLTL)
            End Get
            Set(ByVal value As Double)
                If CRound(_SumLTL) <> CRound(value) Then
                    _SumLTL = CRound(value)
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a unique code of the bank operation (specified by the bank 
        ''' in the imported data).
        ''' </summary>
        ''' <remarks>Used to identify already imported operations.</remarks>
        Public Property UniqueCode() As String
            Get
                Return _UniqueCode.Trim
            End Get
            Set(ByVal value As String)
                If value Is Nothing Then value = ""
                If _UniqueCode.Trim <> value.Trim Then
                    _UniqueCode = value.Trim
                End If
            End Set
        End Property

    End Class

End Namespace