Namespace Assets

    ''' <summary>
    ''' Represents a helper object that persists data of a long term asset operation.
    ''' </summary>
    ''' <remarks>Should only be used as a child of a long term asset operation.
    ''' Values are stored in the database table turtas_op.</remarks>
    <Serializable()> _
    Friend Class OperationPersistenceObject

#Region " Business Methods "

        Private _ID As Integer = 0
        Private _InsertDate As DateTime = Now
        Private _UpdateDate As DateTime = Now
        Private _AssetID As Integer = 0
        Private _OperationType As LtaOperationType = LtaOperationType.Discard
        Private _AccountOperationType As LtaAccountChangeType = LtaAccountChangeType.AcquisitionAccount
        Private _OperationDate As Date = Today
        Private _JournalEntryID As Integer = 0
        Private _JournalEntryDocumentNumber As String = ""
        Private _JournalEntryDate As Date = Today
        Private _JournalEntryContent As String = ""
        Private _JournalEntryPersonID As Integer = 0
        Private _JournalEntryPerson As String = ""
        Private _JournalEntryPersonName As String = ""
        Private _JournalEntryPersonCode As String = ""
        Private _JournalEntryAmount As Double = 0
        Private _JournalEntryBookEntries As String = ""
        Private _JournalEntryDocumentType As DocumentType = DocumentType.None
        Private _IsComplexAct As Boolean = False
        Private _ComplexActID As Integer = 0
        Private _Content As String = ""
        Private _AccountCorresponding As Long = 0
        Private _DocumentNumber As String = ""
        Private _UnitValueChange As Double = 0
        Private _AmmountChange As Integer = 0
        Private _TotalValueChange As Double = 0
        Private _NewAmortizationPeriod As Integer = 0
        Private _AmortizationCalculations As String = ""
        Private _RevaluedPortionUnitValueChange As Double = 0
        Private _RevaluedPortionTotalValueChange As Double = 0
        Private _AcquisitionAccountChange As Double = 0
        Private _AcquisitionAccountChangePerUnit As Double = 0
        Private _AmortizationAccountChange As Double = 0
        Private _AmortizationAccountChangePerUnit As Double = 0
        Private _ValueDecreaseAccountChange As Double = 0
        Private _ValueDecreaseAccountChangePerUnit As Double = 0
        Private _ValueIncreaseAccountChange As Double = 0
        Private _ValueIncreaseAccountChangePerUnit As Double = 0
        Private _ValueIncreaseAmmortizationAccountChange As Double = 0
        Private _ValueIncreaseAmmortizationAccountChangePerUnit As Double = 0
        Private _AmortizationCalculatedForMonths As Integer = 0


        ''' <summary>
        ''' Gets an ID of the operation that is assigned by a database (AUTOINCREMENT).
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas_op.ID.</remarks>
        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' Gets the date and time when the operation was inserted into the database.
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas_op.InsertDate.</remarks>
        Public ReadOnly Property InsertDate() As DateTime
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _InsertDate
            End Get
        End Property

        ''' <summary>
        ''' Gets the date and time when the operation was last updated.
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas_op.UpdateDate.</remarks>
        Public ReadOnly Property UpdateDate() As DateTime
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _UpdateDate
            End Get
        End Property

        ''' <summary>
        ''' Gets <see cref="LongTermAsset.ID">an ID of the long term asset</see> that is affected by the operation.
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas_op.T_ID.</remarks>
        Public ReadOnly Property AssetID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AssetID
            End Get
        End Property

        ''' <summary>
        ''' Gets a type of the long term asset operation.
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas_op.OperationType.</remarks>
        Public ReadOnly Property OperationType() As LtaOperationType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _OperationType
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets a type of the long term asset account change operation.
        ''' </summary>
        ''' <remarks>Only relevant when the <see cref="OperationType">OperationType</see>
        ''' is set to <see cref="LtaOperationType.AccountChange">LtaOperationType.AccountChange</see>.
        ''' Value is stored in the database field turtas_op.AccountOperationType.</remarks>
        Public Property AccountOperationType() As LtaAccountChangeType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AccountOperationType
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As LtaAccountChangeType)
                If _AccountOperationType <> value Then
                    _AccountOperationType = value
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a date of the long term asset operation.
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas_op.OperationDate.</remarks>
        Public Property OperationDate() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _OperationDate
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Date)
                If _OperationDate.Date <> value.Date Then
                    _OperationDate = value.Date
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets an <see cref="General.JournalEntry.ID">ID of the journal entry</see> 
        ''' that is attached to or encapsulated by the long term asset operation.
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas_op.JE_ID.</remarks>
        Public Property JournalEntryID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryID
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Integer)
                If _JournalEntryID <> value Then
                    _JournalEntryID = value
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets a document number of the journal entry that is attached to or encapsulated by the long term asset operation.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="general.JournalEntry.DocNumber">JournalEntry.DocNumber</see>.</remarks>
        Public ReadOnly Property JournalEntryDocumentNumber() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryDocumentNumber.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a date of the journal entry that is attached to or encapsulated by the long term asset operation.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="general.JournalEntry.Date">JournalEntry.Date</see>.</remarks>
        Public ReadOnly Property JournalEntryDate() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryDate
            End Get
        End Property

        ''' <summary>
        ''' Gets a content of the journal entry that is attached to or encapsulated by the long term asset operation.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="general.JournalEntry.Content">JournalEntry.Content</see>.</remarks>
        Public ReadOnly Property JournalEntryContent() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryContent.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets an ID of the person in the journal entry that is attached to or encapsulated by the long term asset operation.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="general.JournalEntry.Person">JournalEntry.Person</see>.</remarks>
        Public ReadOnly Property JournalEntryPersonID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryPersonID
            End Get
        End Property

        ''' <summary>
        ''' Gets a formated name and code of the person in the journal entry 
        ''' that is attached to or encapsulated by the long term asset operation.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="general.JournalEntry.Person">JournalEntry.Person</see>.</remarks>
        Public ReadOnly Property JournalEntryPerson() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryPerson.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a name of the person in the journal entry that is attached to or encapsulated by the long term asset operation.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="general.JournalEntry.Person">JournalEntry.Person</see>.</remarks>
        Public ReadOnly Property JournalEntryPersonName() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryPersonName.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a registration/personal code of the person in the journal entry that is attached to or encapsulated by the long term asset operation.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="general.JournalEntry.Person">JournalEntry.Person</see>.</remarks>
        Public ReadOnly Property JournalEntryPersonCode() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryPersonCode.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a total book entries' amount of the journal entry that is attached to or encapsulated by the long term asset operation.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="general.JournalEntry.DebetSum">JournalEntry.DebetSum</see>.</remarks>
        Public ReadOnly Property JournalEntryAmount() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_JournalEntryAmount)
            End Get
        End Property

        ''' <summary>
        ''' Gets a comma separated list of book entries in the journal entry that is attached to or encapsulated by the long term asset operation.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="general.JournalEntry.DebetList">JournalEntry.DebetList</see>
        ''' and <see cref="general.JournalEntry.CreditList">JournalEntry.CreditList</see>.</remarks>
        Public ReadOnly Property JournalEntryBookEntries() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryBookEntries.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a type of the document that owns the journal entry that is attached to or encapsulated by the long term asset operation.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="general.JournalEntry.DocType">JournalEntry.DocType</see>.</remarks>
        Public ReadOnly Property JournalEntryDocumentType() As DocumentType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryDocumentType
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets whether the long term asset operation is a part of a complex long term asset operation (mass discard etc.).
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas_op.IsComplexAct.</remarks>
        Public Property IsComplexAct() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsComplexAct
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Boolean)
                If _IsComplexAct <> value Then
                    _IsComplexAct = value
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the ID of the long term asset complex operation.
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas_op.IsComplexAct.</remarks>
        Public Property ComplexActID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ComplexActID
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Integer)
                If _ComplexActID <> value Then
                    _ComplexActID = value
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a content (description) of the long term asset operation.
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas_op.Content.</remarks>
        Public Property Content() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Content.Trim
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As String)
                If value Is Nothing Then value = ""
                If _Content.Trim <> value.Trim Then
                    _Content = value.Trim
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a <see cref="General.Account.ID">corresponding account</see> of the long term asset operation
        ''' (e.g. discard costs account, amortization costs account etc.).
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas_op.AccountCorresponding.</remarks>
        Public Property AccountCorresponding() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AccountCorresponding
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Long)
                If _AccountCorresponding <> value Then
                    _AccountCorresponding = value
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a number of document (act) that is drawn by the long term asset operation.
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas_op.DocNo.</remarks>
        Public Property DocumentNumber() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DocumentNumber.Trim
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As String)
                If value Is Nothing Then value = ""
                If _DocumentNumber.Trim <> value.Trim Then
                    _DocumentNumber = value.Trim
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the amount of asset unit value change made by the long term asset operation.
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas_op.UnitValueChange.</remarks>
        Public Property UnitValueChange() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_UnitValueChange, ROUNDUNITASSET)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Double)
                If CRound(_UnitValueChange, ROUNDUNITASSET) <> CRound(value, ROUNDUNITASSET) Then
                    _UnitValueChange = CRound(value, ROUNDUNITASSET)
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the change of the asset amount made by the long term asset operation.
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas_op.AmmountChange.</remarks>
        Public Property AmmountChange() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AmmountChange
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Integer)
                If _AmmountChange <> value Then
                    _AmmountChange = value
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the amount of total asset value change made by the long term asset operation.
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas_op.TotalValueChange.</remarks>
        Public Property TotalValueChange() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_TotalValueChange)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Double)
                If CRound(_TotalValueChange) <> CRound(value) Then
                    _TotalValueChange = CRound(value)
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a new amortization period for the asset set by the long term asset operation.
        ''' </summary>
        ''' <remarks>Only relevant when the <see cref="OperationType">OperationType</see>
        ''' is set to <see cref="LtaOperationType.AmortizationPeriod">LtaOperationType.AmortizationPeriod</see>.
        ''' Value is stored in the database field turtas_op.NewAmortizationPeriod.</remarks>
        Public Property NewAmortizationPeriod() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _NewAmortizationPeriod
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Integer)
                If _NewAmortizationPeriod <> value Then
                    _NewAmortizationPeriod = value
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a (human readable) description of the amortization calculation made by the long term asset operation.
        ''' </summary>
        ''' <remarks>Only relevant when the <see cref="OperationType">OperationType</see>
        ''' is set to <see cref="LtaOperationType.Amortization">LtaOperationType.Amortization</see>.
        ''' Value is stored in the database field turtas_op.AmortizationCalculations.</remarks>
        Public Property AmortizationCalculations() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AmortizationCalculations.Trim
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As String)
                If value Is Nothing Then value = ""
                If _AmortizationCalculations.Trim <> value.Trim Then
                    _AmortizationCalculations = value.Trim
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the amount of asset revalued portion unit value change made by the long term asset operation.
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas_op.RevaluedPortionUnitValueChange.</remarks>
        Public Property RevaluedPortionUnitValueChange() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_RevaluedPortionUnitValueChange, ROUNDUNITASSET)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Double)
                If CRound(_RevaluedPortionUnitValueChange, ROUNDUNITASSET) <> CRound(value, ROUNDUNITASSET) Then
                    _RevaluedPortionUnitValueChange = CRound(value, ROUNDUNITASSET)
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the amount of asset revalued portion total value change made by the long term asset operation.
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas_op.RevaluedPortionTotalValueChange.</remarks>
        Public Property RevaluedPortionTotalValueChange() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_RevaluedPortionTotalValueChange)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Double)
                If CRound(_RevaluedPortionTotalValueChange) <> CRound(value) Then
                    _RevaluedPortionTotalValueChange = CRound(value)
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the amount of <see cref="LongTermAsset.AccountAcquisition">asset acquisitinion account</see> 
        ''' change made by the long term asset operation.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.
        ''' Value is stored in the database field turtas_op.AcquisitionAccountChange.</remarks>
        Public Property AcquisitionAccountChange() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AcquisitionAccountChange)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Double)
                If CRound(_AcquisitionAccountChange) <> CRound(value) Then
                    _AcquisitionAccountChange = CRound(value)
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the amount of <see cref="LongTermAsset.AccountAcquisition">asset acquisitinion account</see> 
        ''' change per asset unit made by the long term asset operation.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.
        ''' Value is stored in the database field turtas_op.AcquisitionAccountChangePerUnit.</remarks>
        Public Property AcquisitionAccountChangePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AcquisitionAccountChangePerUnit, ROUNDUNITASSET)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Double)
                If CRound(_AcquisitionAccountChangePerUnit, ROUNDUNITASSET) <> CRound(value, ROUNDUNITASSET) Then
                    _AcquisitionAccountChangePerUnit = CRound(value, ROUNDUNITASSET)
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the amount of <see cref="LongTermAsset.AccountAccumulatedAmortization">asset amortization account</see> 
        ''' change made by the long term asset operation.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit balance.
        ''' Value is stored in the database field turtas_op.AmortizationAccountChange.</remarks>
        Public Property AmortizationAccountChange() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AmortizationAccountChange)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Double)
                If CRound(_AmortizationAccountChange) <> CRound(value) Then
                    _AmortizationAccountChange = CRound(value)
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the amount of <see cref="LongTermAsset.AccountAccumulatedAmortization">asset amortization account</see> 
        ''' change per asset unit made by the long term asset operation.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit balance.
        ''' Value is stored in the database field turtas_op.AmortizationAccountChangePerUnit.</remarks>
        Public Property AmortizationAccountChangePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AmortizationAccountChangePerUnit, ROUNDUNITASSET)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Double)
                If CRound(_AmortizationAccountChangePerUnit, ROUNDUNITASSET) <> CRound(value, ROUNDUNITASSET) Then
                    _AmortizationAccountChangePerUnit = CRound(value, ROUNDUNITASSET)
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the amount of <see cref="LongTermAsset.AccountValueDecrease">asset value decrease account</see> 
        ''' change made by the long term asset operation.
        ''' </summary>A positive number represents credit balance, a negative number represents debit balance.
        ''' <remarks>Value is stored in the database field turtas_op.ValueDecreaseAccountChange.</remarks>
        Public Property ValueDecreaseAccountChange() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ValueDecreaseAccountChange)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Double)
                If CRound(_ValueDecreaseAccountChange) <> CRound(value) Then
                    _ValueDecreaseAccountChange = CRound(value)
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the amount of <see cref="LongTermAsset.AccountValueDecrease">asset value decrease account</see> 
        ''' change per asset unit made by the long term asset operation.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit balance.
        ''' Value is stored in the database field turtas_op.ValueDecreaseAccountChangePerUnit.</remarks>
        Public Property ValueDecreaseAccountChangePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ValueDecreaseAccountChangePerUnit, ROUNDUNITASSET)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Double)
                If CRound(_ValueDecreaseAccountChangePerUnit, ROUNDUNITASSET) <> CRound(value, ROUNDUNITASSET) Then
                    _ValueDecreaseAccountChangePerUnit = CRound(value, ROUNDUNITASSET)
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the amount of <see cref="LongTermAsset.AccountValueIncrease">asset value increase account</see> 
        ''' change made by the long term asset operation.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.
        ''' Value is stored in the database field turtas_op.ValueIncreaseAccountChange.</remarks>
        Public Property ValueIncreaseAccountChange() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ValueIncreaseAccountChange)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Double)
                If CRound(_ValueIncreaseAccountChange) <> CRound(value) Then
                    _ValueIncreaseAccountChange = CRound(value)
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the amount of <see cref="LongTermAsset.AccountValueIncrease">asset value increase account</see> 
        ''' change per asset unit made by the long term asset operation.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.
        ''' Value is stored in the database field turtas_op.ValueIncreaseAccountChangePerUnit.</remarks>
        Public Property ValueIncreaseAccountChangePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ValueIncreaseAccountChangePerUnit, ROUNDUNITASSET)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Double)
                If CRound(_ValueIncreaseAccountChangePerUnit, ROUNDUNITASSET) <> CRound(value, ROUNDUNITASSET) Then
                    _ValueIncreaseAccountChangePerUnit = CRound(value, ROUNDUNITASSET)
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the amount of <see cref="LongTermAsset.AccountRevaluedPortionAmmortization">
        ''' asset revalued portion amortization account</see> change made by the long term asset operation.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit balance.
        ''' Value is stored in the database field turtas_op.ValueIncreaseAmmortizationAccountChange.</remarks>
        Public Property ValueIncreaseAmmortizationAccountChange() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ValueIncreaseAmmortizationAccountChange)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Double)
                If CRound(_ValueIncreaseAmmortizationAccountChange) <> CRound(value) Then
                    _ValueIncreaseAmmortizationAccountChange = CRound(value)
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the amount of <see cref="LongTermAsset.AccountRevaluedPortionAmmortization">
        ''' asset revalued portion amortization account</see> change per asset unit made by the long term asset operation.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit balance.
        ''' Value is stored in the database field turtas_op.ValueIncreaseAmmortizationAccountChangePerUnit.</remarks>
        Public Property ValueIncreaseAmmortizationAccountChangePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ValueIncreaseAmmortizationAccountChangePerUnit, ROUNDUNITASSET)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Double)
                If CRound(_ValueIncreaseAmmortizationAccountChangePerUnit, ROUNDUNITASSET) <> CRound(value, ROUNDUNITASSET) Then
                    _ValueIncreaseAmmortizationAccountChangePerUnit = CRound(value, ROUNDUNITASSET)
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a number of months that the amortization calculation is made for by the long term asset operation.
        ''' </summary>
        ''' <remarks>Only relevant when the <see cref="OperationType">OperationType</see>
        ''' is set to <see cref="LtaOperationType.Amortization">LtaOperationType.Amortization</see>.
        ''' Value is stored in the database field turtas_op.AmortizationCalculatedForMonths.</remarks>
        Public Property AmortizationCalculatedForMonths() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AmortizationCalculatedForMonths
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Integer)
                If _AmortizationCalculatedForMonths <> value Then
                    _AmortizationCalculatedForMonths = value
                End If
            End Set
        End Property


        ''' <summary>
        ''' Saves the operation data to a database.
        ''' </summary>
        ''' <remarks></remarks>
        Friend Function SaveChild(ByVal financialDataCanChange As Boolean) As OperationPersistenceObject

            Dim result As OperationPersistenceObject = Clone(Of OperationPersistenceObject)(Me)

            If result._ID > 0 Then
                result.Update(financialDataCanChange)
            Else
                result.Insert()
            End If

            Return result

        End Function

        ''' <summary>
        ''' Gets a new ID value for a complex long term asset operation.
        ''' </summary>
        ''' <remarks></remarks>
        Friend Shared Function GetNewComplexOperationID() As Integer

            Dim result As Integer

            Dim myComm As New SQLCommand("FetchLongTermAssetOperationChildListParentLastNumber")

            Using myData As DataTable = myComm.Fetch
                Try
                    result = CIntSafe(myData.Rows(0).Item(0), 0) + 1
                Catch ex As Exception
                    result = 1
                End Try
            End Using

            Return result

        End Function


        Public Overrides Function ToString() As String
            Return String.Format(My.Resources.Assets_OperationPersistenceObject_ToString, _
                _OperationDate.ToString("yyyy-MM-dd"), _
                Utilities.ConvertLocalizedName(_OperationType), _Content)
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets a new OperationPersistenceObject instance for a new long term asset operation.
        ''' </summary>
        ''' <param name="newOperationType">Type of the new long term asset operation.</param>
        ''' <param name="parentAssetID">ID of the asset that the operation affects.</param>
        ''' <remarks></remarks>
        Friend Shared Function NewOperationPersistenceObject(ByVal newOperationType As LtaOperationType, _
            ByVal parentAssetID As Integer) As OperationPersistenceObject
            Return New OperationPersistenceObject(newOperationType, parentAssetID)
        End Function

        ''' <summary>
        ''' Gets an existing OperationPersistenceObject instance from a database.
        ''' </summary>
        ''' <param name="operationID">An ID of the operation to get.</param>
        ''' <param name="expectedType">An operation type that the parent long term asset operation expects to get.</param>
        ''' <param name="throwOnTypeMismatch">Whether to throw an exception if the operation type is not of expectedType.</param>
        ''' <remarks></remarks>
        Friend Shared Function GetOperationPersistenceObject(ByVal operationID As Integer, _
            ByVal expectedType As LtaOperationType, _
            Optional ByVal throwOnTypeMismatch As Boolean = True) As OperationPersistenceObject
            Return New OperationPersistenceObject(operationID, expectedType, throwOnTypeMismatch)
        End Function

        ''' <summary>
        ''' Gets an existing OperationPersistenceObject instance list 
        ''' for a complex operation from a database.
        ''' </summary>
        ''' <param name="complexOperationID">An ID of the complex operation.</param>
        ''' <param name="expectedType">An operation type that the parent long term asset operation expects to get.</param>
        ''' <param name="throwOnTypeMismatch">Whether to throw an exception if the operation type is not of expectedType.</param>
        ''' <remarks></remarks>
        Friend Shared Function GetOperationPersistenceObjectList(ByVal complexOperationID As Integer, _
            ByVal expectedType As LtaOperationType, _
            Optional ByVal throwOnTypeMismatch As Boolean = True) As List(Of OperationPersistenceObject)

            If Not complexOperationID > 0 Then
                Throw New Exception(My.Resources.Assets_OperationPersistenceObject_IdNull)
            End If

            Dim result As New List(Of OperationPersistenceObject)

            Dim myComm As New SQLCommand("FetchAssetOperationPersistenceObjectList")
            myComm.AddParam("?OD", complexOperationID)

            Using myData As DataTable = myComm.Fetch()

                If myData.Rows.Count < 1 Then Throw New Exception(String.Format( _
                    My.Resources.Common_ObjectNotFound, My.Resources.Assets_OperationPersistenceObject_TypeNameComplex, _
                    complexOperationID.ToString()))

                For Each dr As DataRow In myData.Rows
                    result.Add(New OperationPersistenceObject(dr))
                Next

                If throwOnTypeMismatch AndAlso Not OperationTypesMatch(expectedType, _
                    result(0).OperationType) Then
                    Throw New Exception(String.Format( _
                        My.Resources.Assets_OperationPersistenceObject_UnexpectedType, complexOperationID.ToString(), _
                        Utilities.ConvertLocalizedName(expectedType), _
                        Utilities.ConvertLocalizedName(result(0).OperationType)))
                End If

            End Using

            Return result

        End Function


        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal newOperationType As LtaOperationType, ByVal parentAssetID As Integer)
            Create(newOperationType, parentAssetID)
        End Sub

        Private Sub New(ByVal operationID As Integer, ByVal expectedType As LtaOperationType, _
            ByVal throwOnTypeMismatch As Boolean)
            Fetch(operationID, expectedType, throwOnTypeMismatch)
        End Sub

        Private Sub New(ByVal dr As DataRow)
            Fetch(dr)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Create(ByVal newOperationType As LtaOperationType, ByVal parentAssetID As Integer)
            _OperationType = newOperationType
            _AssetID = parentAssetID
        End Sub

        Private Sub Fetch(ByVal operationID As Integer, ByVal expectedType As LtaOperationType, _
            ByVal throwOnTypeMismatch As Boolean)

            If Not operationID > 0 Then
                Throw New Exception(My.Resources.Assets_OperationPersistenceObject_IdNull)
            End If

            Dim myComm As New SQLCommand("FetchAssetOperationPersistenceObject")
            myComm.AddParam("?OD", operationID)

            Using myData As DataTable = myComm.Fetch()

                If myData.Rows.Count < 1 Then Throw New Exception(String.Format( _
                    My.Resources.Common_ObjectNotFound, My.Resources.Assets_OperationPersistenceObject_TypeName, _
                    operationID.ToString()))

                Fetch(myData.Rows(0))

                If throwOnTypeMismatch AndAlso Not OperationTypesMatch(expectedType, _OperationType) Then
                    Throw New Exception(String.Format(My.Resources.Assets_OperationPersistenceObject_UnexpectedType, _
                        _ID.ToString(), Utilities.ConvertLocalizedName(expectedType), _
                        Utilities.ConvertLocalizedName(_OperationType)))
                End If

            End Using

        End Sub

        Private Sub Fetch(ByVal dr As DataRow)

            _ID = CIntSafe(dr.Item(0), 0)
            _AssetID = CIntSafe(dr.Item(1), 0)
            _OperationType = Utilities.ConvertDatabaseCharID(Of LtaOperationType)(CStrSafe(dr.Item(2)))
            If _OperationType = LtaOperationType.AccountChange Then
                _AccountOperationType = Utilities.ConvertDatabaseCharID(Of LtaAccountChangeType)(CStrSafe(dr.Item(3)))
            End If
            _OperationDate = CDateSafe(dr.Item(4), Today)
            _ComplexActID = CIntSafe(dr.Item(5), 0)
            _IsComplexAct = (_ComplexActID > 0)
            _Content = CStrSafe(dr.Item(6)).Trim
            _AccountCorresponding = CLongSafe(dr.Item(7), 0)
            _DocumentNumber = CStrSafe(dr.Item(8))
            _UnitValueChange = CDblSafe(dr.Item(9), ROUNDUNITASSET, 0)
            _AmmountChange = CIntSafe(dr.Item(10), 0)
            _TotalValueChange = CDblSafe(dr.Item(11), 2, 0)
            _NewAmortizationPeriod = CIntSafe(dr.Item(12), 0)
            _AmortizationCalculations = CStrSafe(dr.Item(13)).Trim
            _RevaluedPortionUnitValueChange = CDblSafe(dr.Item(14), ROUNDUNITASSET, 0)
            _RevaluedPortionTotalValueChange = CDblSafe(dr.Item(15), 2, 0)
            _AcquisitionAccountChange = CDblSafe(dr.Item(16), 2, 0)
            _AcquisitionAccountChangePerUnit = CDblSafe(dr.Item(17), ROUNDUNITASSET, 0)
            _AmortizationAccountChange = CDblSafe(dr.Item(18), 2, 0)
            _AmortizationAccountChangePerUnit = CDblSafe(dr.Item(19), ROUNDUNITASSET, 0)
            _ValueDecreaseAccountChange = CDblSafe(dr.Item(20), 2, 0)
            _ValueDecreaseAccountChangePerUnit = CDblSafe(dr.Item(21), ROUNDUNITASSET, 0)
            _ValueIncreaseAccountChange = CDblSafe(dr.Item(22), 2, 0)
            _ValueIncreaseAccountChangePerUnit = CDblSafe(dr.Item(23), ROUNDUNITASSET, 0)
            _ValueIncreaseAmmortizationAccountChange = CDblSafe(dr.Item(24), 2, 0)
            _ValueIncreaseAmmortizationAccountChangePerUnit = CDblSafe(dr.Item(25), ROUNDUNITASSET, 0)
            _AmortizationCalculatedForMonths = CIntSafe(dr.Item(26), 0)
            _InsertDate = CTimeStampSafe(dr.Item(27))
            _UpdateDate = CTimeStampSafe(dr.Item(28))
            If _OperationType = LtaOperationType.UsingStart OrElse _
                _OperationType = LtaOperationType.UsingEnd Then
                If CIntSafe(dr.Item(29), 0) Mod 2 > 0 Then
                    _OperationType = LtaOperationType.UsingEnd
                Else
                    _OperationType = LtaOperationType.UsingStart
                End If
            End If
            _JournalEntryID = CIntSafe(dr.Item(31), 0)
            _JournalEntryDocumentNumber = CStrSafe(dr.Item(32)).Trim
            _JournalEntryDate = CDateSafe(dr.Item(33), Today)
            _JournalEntryContent = CStrSafe(dr.Item(34)).Trim
            _JournalEntryDocumentType = Utilities.ConvertDatabaseCharID(Of DocumentType)(CStrSafe(dr.Item(35)))
            _JournalEntryPersonID = CIntSafe(dr.Item(36), 0)
            _JournalEntryPersonName = CStrSafe(dr.Item(37)).Trim
            _JournalEntryPersonCode = CStrSafe(dr.Item(38)).Trim
            _JournalEntryBookEntries = CStrSafe(dr.Item(39)).Trim
            _JournalEntryAmount = CDblSafe(dr.Item(40), 2, 0)

            If _JournalEntryPersonID > 0 Then
                _JournalEntryPerson = String.Format("{0} ({1})", _JournalEntryPersonName, _
                    _JournalEntryPersonCode)
            Else
                _JournalEntryPerson = ""
            End If

        End Sub

        Private Sub Insert()

            Dim myComm As New SQLCommand("InsertAssetOperationPersistenceObject")
            AddWithGeneralParams(myComm)
            AddWithFinancialParams(myComm)
            If _IsComplexAct Then
                myComm.AddParam("?AC", _ComplexActID)
            Else
                myComm.AddParam("?AC", 0)
            End If
            myComm.AddParam("?BA", _AssetID)
            myComm.AddParam("?BB", Utilities.ConvertDatabaseCharID(_OperationType))
            If _OperationType = LtaOperationType.AccountChange Then
                myComm.AddParam("?BC", Utilities.ConvertDatabaseCharID(_AccountOperationType))
            Else
                myComm.AddParam("?BC", "")
            End If

            myComm.Execute()

            _ID = Convert.ToInt32(myComm.LastInsertID)

        End Sub

        Private Sub Update(ByVal financialDataCanChange As Boolean)

            Dim myComm As SQLCommand
            If financialDataCanChange Then
                myComm = New SQLCommand("UpdateAssetOperationPersistenceObject")
                AddWithFinancialParams(myComm)
            Else
                myComm = New SQLCommand("UpdateAssetOperationPersistenceObjectGeneral")
            End If
            myComm.AddParam("?OD", _ID)
            AddWithGeneralParams(myComm)

            myComm.Execute()

        End Sub


        ''' <summary>
        ''' Deletes an existing OperationPersistenceObject instance from a database.
        ''' </summary>
        ''' <param name="operationID">An ID of the operation to delete.</param>
        ''' <remarks></remarks>
        Friend Shared Sub DeleteChild(ByVal operationID As Integer)

            Dim myComm As New SQLCommand("DeleteAssetOperationPersistenceObject")
            myComm.AddParam("?OD", operationID)

            myComm.Execute()

        End Sub

        ''' <summary>
        ''' Gets a long term asset operation ID by the operation's journal entry ID.
        ''' </summary>
        ''' <param name="journalEntryID">A journal entry ID to find an operation ID by.</param>
        ''' <remarks></remarks>
        Friend Shared Function GetOperationIdByJournalEntry(ByVal journalEntryID As Integer) As Integer

            If Not journalEntryID > 0 Then Throw New Exception( _
                My.Resources.Assets_OperationPersistenceObject_JournalEntryIdNull)

            Dim myComm As New SQLCommand("FetchLongTermAssetOperationIdByJournalEntryId")
            myComm.AddParam("?JD", journalEntryID)

            Using myData As DataTable = myComm.Fetch

                If myData.Rows.Count < 1 OrElse Not CIntSafe(myData.Rows(0).Item(0), 0) > 0 Then
                    Throw New Exception(String.Format(My.Resources.Assets_OperationPersistenceObject_OperationNotFoundByJournalID, _
                        journalEntryID.ToString))
                End If

                Return CIntSafe(myData.Rows(0).Item(0), 0)

            End Using

        End Function

        ''' <summary>
        ''' Gets a complex long term asset operation ID by the operation's journal entry ID.
        ''' </summary>
        ''' <param name="journalEntryID">A journal entry ID to find a complex operation ID by.</param>
        ''' <remarks></remarks>
        Friend Shared Function GetComplexOperationIdByJournalEntry(ByVal journalEntryID As Integer) As Integer

            If Not journalEntryID > 0 Then Throw New Exception( _
                My.Resources.Assets_OperationPersistenceObject_JournalEntryIdNull)

            Dim myComm As New SQLCommand("FetchLongTermAssetComplexOperationIdByJournalEntryId")
            myComm.AddParam("?JD", journalEntryID)

            Using myData As DataTable = myComm.Fetch

                If myData.Rows.Count < 1 OrElse Not CIntSafe(myData.Rows(0).Item(0), 0) > 0 Then
                    Throw New Exception(String.Format(My.Resources.Assets_OperationPersistenceObject_OperationNotFoundByJournalID, _
                        journalEntryID.ToString))
                End If

                Return CIntSafe(myData.Rows(0).Item(0), 0)

            End Using

        End Function

        Private Sub AddWithGeneralParams(ByRef myComm As SQLCommand)

            myComm.AddParam("?AA", _OperationDate.Date)
            myComm.AddParam("?AD", _Content.Trim)
            myComm.AddParam("?AJ", _AmortizationCalculations.Trim)
            myComm.AddParam("?AW", _AmortizationCalculatedForMonths)
            myComm.AddParam("?AB", _JournalEntryID)
            myComm.AddParam("?AE", _DocumentNumber)

            _UpdateDate = GetCurrentTimeStamp()
            If Not _ID > 0 Then _InsertDate = _UpdateDate
            myComm.AddParam("?BE", _UpdateDate.ToUniversalTime())

        End Sub

        Private Sub AddWithFinancialParams(ByRef myComm As SQLCommand)

            myComm.AddParam("?AF", CRound(_UnitValueChange, ROUNDUNITASSET))
            myComm.AddParam("?AG", _AmmountChange)
            myComm.AddParam("?AH", CRound(_TotalValueChange))
            myComm.AddParam("?AK", CRound(_RevaluedPortionUnitValueChange, ROUNDUNITASSET))
            myComm.AddParam("?AL", CRound(_RevaluedPortionTotalValueChange))
            myComm.AddParam("?AM", CRound(_AcquisitionAccountChange))
            myComm.AddParam("?AN", CRound(_AcquisitionAccountChangePerUnit, ROUNDUNITASSET))
            myComm.AddParam("?AO", CRound(_AmortizationAccountChange))
            myComm.AddParam("?AQ", CRound(_AmortizationAccountChangePerUnit, ROUNDUNITASSET))
            myComm.AddParam("?AP", CRound(_ValueDecreaseAccountChange))
            myComm.AddParam("?AR", CRound(_ValueDecreaseAccountChangePerUnit, ROUNDUNITASSET))
            myComm.AddParam("?AT", CRound(_ValueIncreaseAccountChange))
            myComm.AddParam("?AU", CRound(_ValueIncreaseAccountChangePerUnit, ROUNDUNITASSET))
            myComm.AddParam("?AV", CRound(_ValueIncreaseAmmortizationAccountChange))
            myComm.AddParam("?AZ", CRound(_ValueIncreaseAmmortizationAccountChangePerUnit, ROUNDUNITASSET))
            myComm.AddParam("?BD", _AccountCorresponding)
            myComm.AddParam("?AI", _NewAmortizationPeriod)

        End Sub

        Private Shared Function OperationTypesMatch(ByVal type1 As LtaOperationType, _
            ByVal type2 As LtaOperationType) As Boolean

            If (type1 = LtaOperationType.UsingStart _
                OrElse type1 = LtaOperationType.UsingEnd) AndAlso _
                (type2 = LtaOperationType.UsingStart _
                OrElse type2 = LtaOperationType.UsingEnd) Then

                Return True

            Else

                Return type1 = type2

            End If

        End Function

#End Region

    End Class

End Namespace