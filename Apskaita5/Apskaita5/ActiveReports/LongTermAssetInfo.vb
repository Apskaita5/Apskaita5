Namespace ActiveReports

    ''' <summary>
    ''' Represents an item of <see cref="ActiveReports.LongTermAssetInfoList">LongTermAssetInfoList</see> report.
    ''' Contains information about <see cref="Assets.LongTermAsset">a long term asset</see> state.
    ''' </summary>
    ''' <remarks>Should only be used as a child of <see cref="ActiveReports.LongTermAssetInfoList">LongTermAssetInfoList</see>.
    ''' Values are stored in the database table turtas.</remarks>
    <Serializable()> _
    Public Class LongTermAssetInfo
        Inherits ReadOnlyBase(Of LongTermAssetInfo)

#Region " Business Methods "

        Private _ID As Integer = 0
        Private _Name As String = ""
        Private _MeasureUnit As String = ""
        Private _LegalGroup As String = ""
        Private _CustomGroup As String = ""
        Private _AcquisitionDate As Date = Today
        Private _AcquisitionJournalEntryID As Integer = 0
        Private _AcquisitionJournalEntryDocNumber As String = ""
        Private _AcquisitionJournalEntryDocContent As String = ""
        Private _AcquisitionJournalEntryDocType As String = ""
        Private _InventoryNumber As String = ""
        Private _AccountAcquisition As Long = 0
        Private _AccountAccumulatedAmortization As Long = 0
        Private _AccountValueIncrease As Long = 0
        Private _AccountValueDecrease As Long = 0
        Private _AccountRevaluedPortionAmmortization As Long = 0
        Private _LiquidationUnitValue As Double = 0
        Private _ContinuedUsage As Boolean = False
        Private _DefaultAmortizationPeriod As Integer = 0
        Private _AcquisitionAccountValue As Double = 0
        Private _AcquisitionAccountValuePerUnit As Double = 0
        Private _AmortizationAccountValue As Double = 0
        Private _AmortizationAccountValuePerUnit As Double = 0
        Private _ValueDecreaseAccountValue As Double = 0
        Private _ValueDecreaseAccountValuePerUnit As Double = 0
        Private _ValueIncreaseAccountValue As Double = 0
        Private _ValueIncreaseAccountValuePerUnit As Double = 0
        Private _ValueIncreaseAmmortizationAccountValue As Double = 0
        Private _ValueIncreaseAmmortizationAccountValuePerUnit As Double = 0
        Private _Value As Double = 0
        Private _ValuePerUnit As Double = 0
        Private _Ammount As Integer = 0
        Private _ValueRevaluedPortion As Double = 0
        Private _ValueRevaluedPortionPerUnit As Double = 0
        Private _BeforeAcquisitionAccountValue As Double = 0
        Private _BeforeAcquisitionAccountValuePerUnit As Double = 0
        Private _BeforeAmortizationAccountValue As Double = 0
        Private _BeforeAmortizationAccountValuePerUnit As Double = 0
        Private _BeforeValueDecreaseAccountValue As Double = 0
        Private _BeforeValueDecreaseAccountValuePerUnit As Double = 0
        Private _BeforeValueIncreaseAccountValue As Double = 0
        Private _BeforeValueIncreaseAccountValuePerUnit As Double = 0
        Private _BeforeValueIncreaseAmmortizationAccountValue As Double = 0
        Private _BeforeValueIncreaseAmmortizationAccountValuePerUnit As Double = 0
        Private _BeforeValue As Double = 0
        Private _BeforeValuePerUnit As Double = 0
        Private _BeforeAmmount As Integer = 0
        Private _ChangeAcquisitionAccountValue As Double = 0
        Private _ChangeAcquisitionAccountValuePerUnit As Double = 0
        Private _ChangeAmortizationAccountValue As Double = 0
        Private _ChangeAmortizationAccountValuePerUnit As Double = 0
        Private _ChangeValueDecreaseAccountValue As Double = 0
        Private _ChangeValueDecreaseAccountValuePerUnit As Double = 0
        Private _ChangeValueIncreaseAccountValue As Double = 0
        Private _ChangeValueIncreaseAccountValuePerUnit As Double = 0
        Private _ChangeValueIncreaseAmmortizationAccountValue As Double = 0
        Private _ChangeValueIncreaseAmmortizationAccountValuePerUnit As Double = 0
        Private _ChangeValue As Double = 0
        Private _ChangeValuePerUnit As Double = 0
        Private _ChangeAmmount As Integer = 0
        Private _ChangeAmmountAcquired As Integer = 0
        Private _ChangeValueAcquired As Double = 0
        Private _ChangeAmmountTransfered As Integer = 0
        Private _ChangeValueTransfered As Double = 0
        Private _ChangeAmmountDiscarded As Integer = 0
        Private _ChangeValueDiscarded As Double = 0
        Private _AfterAcquisitionAccountValue As Double = 0
        Private _AfterAcquisitionAccountValuePerUnit As Double = 0
        Private _AfterAmortizationAccountValue As Double = 0
        Private _AfterAmortizationAccountValuePerUnit As Double = 0
        Private _AfterValueDecreaseAccountValue As Double = 0
        Private _AfterValueDecreaseAccountValuePerUnit As Double = 0
        Private _AfterValueIncreaseAccountValue As Double = 0
        Private _AfterValueIncreaseAccountValuePerUnit As Double = 0
        Private _AfterValueIncreaseAmmortizationAccountValue As Double = 0
        Private _AfterValueIncreaseAmmortizationAccountValuePerUnit As Double = 0
        Private _AfterValue As Double = 0
        Private _AfterValuePerUnit As Double = 0
        Private _AfterAmmount As Integer = 0


        ''' <summary>
        ''' Gets an ID of the LongTermAsset object (assigned by DB AUTO_INCREMENT).
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas.ID.</remarks>
        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' Gets a name of the long term asset.
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas.Turtas.</remarks>
        Public ReadOnly Property Name() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Name.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a measure unit of the long term asset.
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas.Vnt.</remarks>
        Public ReadOnly Property MeasureUnit() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _MeasureUnit.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a name of the legal group of the long term asset.
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas.Grupe.</remarks>
        Public ReadOnly Property LegalGroup() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _LegalGroup.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a custom group of the long term asset.
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas.CustomGroupID.</remarks>
        Public ReadOnly Property CustomGroup() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _CustomGroup.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets an acquisition date of the long term asset.
        ''' </summary>
        ''' <remarks>Value corresponds to the date of <see cref="AcquisitionJournalEntryID">
        ''' the attached journal entry</see>.</remarks>
        Public ReadOnly Property AcquisitionDate() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AcquisitionDate
            End Get
        End Property

        ''' <summary>
        ''' Gets an ID of the journal entry that substantiates the acquisition of the long term asset.
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas.Isigijimo_dok.</remarks>
        Public ReadOnly Property AcquisitionJournalEntryID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AcquisitionJournalEntryID
            End Get
        End Property

        ''' <summary>
        ''' Gets a document number of the journal entry that substantiates the acquisition of the long term asset.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property AcquisitionJournalEntryDocNumber() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AcquisitionJournalEntryDocNumber.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a content of the journal entry that substantiates the acquisition of the long term asset.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property AcquisitionJournalEntryDocContent() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AcquisitionJournalEntryDocContent.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a type of the journal entry that substantiates the acquisition of the long term asset.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property AcquisitionJournalEntryDocType() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AcquisitionJournalEntryDocType.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets an inventory number of the long term asset.
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas.InvNr.</remarks>
        Public ReadOnly Property InventoryNumber() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _InventoryNumber.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets <see cref="General.Account.ID">an asset acquisitinion account</see>.
        ''' </summary>
        ''' <remarks>12 BAS para 12.
        ''' Value is stored in the database field turtas.Saskaita.</remarks>
        Public ReadOnly Property AccountAcquisition() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AccountAcquisition
            End Get
        End Property

        ''' <summary>
        ''' Gets <see cref="General.Account.ID">an asset amortization (depreciation) account</see>.
        ''' </summary>
        ''' <remarks>12 BAS para 68.
        ''' Value is stored in the database field turtas.AccountAmortization.</remarks>
        Public ReadOnly Property AccountAccumulatedAmortization() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AccountAccumulatedAmortization
            End Get
        End Property

        ''' <summary>
        ''' Gets <see cref="General.Account.ID">an asset value increase account</see>.
        ''' </summary>
        ''' <remarks>12 BAS para 48.
        ''' Value is stored in the database field turtas.AccountValueIncrease.</remarks>
        Public ReadOnly Property AccountValueIncrease() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AccountValueIncrease
            End Get
        End Property

        ''' <summary>
        ''' Gets <see cref="General.Account.ID">an asset value decrease account</see>.
        ''' </summary>
        ''' <remarks>12 BAS para 49.
        ''' Value is stored in the database field turtas.AccountValueDecrease.</remarks>
        Public ReadOnly Property AccountValueDecrease() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AccountValueDecrease
            End Get
        End Property

        ''' <summary>
        ''' Gets <see cref="General.Account.ID">an asset revalued portion amortization account</see>.
        ''' </summary>
        ''' <remarks>12 BAS para 48.
        ''' Value is stored in the database field turtas.AccountRevaluedPortionAmmortization.</remarks>
        Public ReadOnly Property AccountRevaluedPortionAmmortization() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AccountRevaluedPortionAmmortization
            End Get
        End Property

        ''' <summary>
        ''' Gets a liquidation (salvage) value of the long term asset per unit.
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas.Likutine.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property LiquidationUnitValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_LiquidationUnitValue, ROUNDUNITASSET)
            End Get
        End Property

        ''' <summary>
        ''' Gets whether the long term asset is already operational when acquired.
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas.ContinuedUsage.</remarks>
        Public ReadOnly Property ContinuedUsage() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ContinuedUsage
            End Get
        End Property

        ''' <summary>
        ''' Gets a default amortization (depreciation) period for the long term asset.
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas.ContinuedUsage.</remarks>
        Public ReadOnly Property DefaultAmortizationPeriod() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DefaultAmortizationPeriod
            End Get
        End Property

        ''' <summary>
        ''' A balance for the <see cref="AccountAcquisition">AccountAcquisition</see> at the acquisition date.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit (invalid) balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property AcquisitionAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AcquisitionAccountValue)
            End Get
        End Property

        ''' <summary>
        ''' A balance for the <see cref="AccountAcquisition">AccountAcquisition</see> per asset unit at the acquisition date.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit (invalid) balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property AcquisitionAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AcquisitionAccountValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        ''' <summary>
        ''' A balance for the <see cref="AccountAccumulatedAmortization">AccountAccumulatedAmortization</see> at the acquisition date.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit (invalid) balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property AmortizationAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AmortizationAccountValue)
            End Get
        End Property

        ''' <summary>
        ''' A balance for the <see cref="AccountAccumulatedAmortization">AccountAccumulatedAmortization</see> per unit at the acquisition date.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit (invalid) balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property AmortizationAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AmortizationAccountValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        ''' <summary>
        ''' A balance for the <see cref="AccountValueDecrease">AccountValueDecrease</see> at the acquisition date.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit (invalid) balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property ValueDecreaseAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ValueDecreaseAccountValue)
            End Get
        End Property

        ''' <summary>
        ''' A balance for the <see cref="AccountValueDecrease">AccountValueDecrease</see> per unit made at the acquisition date.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit (invalid) balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property ValueDecreaseAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ValueDecreaseAccountValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        ''' <summary>
        ''' A balance for the <see cref="AccountValueIncrease">AccountValueIncrease</see> at the acquisition date.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit (invalid) balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property ValueIncreaseAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ValueIncreaseAccountValue)
            End Get
        End Property

        ''' <summary>
        ''' A balance for the <see cref="AccountValueIncrease">AccountValueIncrease</see> per unit at the acquisition date.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit (invalid) balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property ValueIncreaseAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ValueIncreaseAccountValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        ''' <summary>
        ''' A balance for the <see cref="AccountRevaluedPortionAmmortization">AccountRevaluedPortionAmmortization</see> at the acquisition date.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit (invalid) balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property ValueIncreaseAmmortizationAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ValueIncreaseAmmortizationAccountValue)
            End Get
        End Property

        ''' <summary>
        ''' A balance for the <see cref="AccountRevaluedPortionAmmortization">AccountRevaluedPortionAmmortization</see> per unit made at the acquisition date.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit (invalid) balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property ValueIncreaseAmmortizationAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ValueIncreaseAmmortizationAccountValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        ''' <summary>
        ''' A total value of the long term asset at the acquisition date.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property Value() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_Value)
            End Get
        End Property

        ''' <summary>
        ''' A unit value of the long term asset at the acquisition date.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property ValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        ''' <summary>
        ''' An amount of the long term asset at the acquisition date.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Ammount() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Ammount
            End Get
        End Property

        ''' <summary>
        ''' A total value of the revalued portion of the long term asset at the acquisition date.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property ValueRevaluedPortion() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ValueRevaluedPortion)
            End Get
        End Property

        ''' <summary>
        ''' A unit value of the revalued portion of the long term asset at the acquisition date.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property ValueRevaluedPortionPerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ValueRevaluedPortionPerUnit, ROUNDUNITASSET)
            End Get
        End Property

        ''' <summary>
        ''' A balance for the <see cref="AccountAcquisition">AccountAcquisition</see> at the beginning of the report period.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit (invalid) balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property BeforeAcquisitionAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_BeforeAcquisitionAccountValue)
            End Get
        End Property

        ''' <summary>
        ''' A balance for the <see cref="AccountAcquisition">AccountAcquisition</see> per unit at the beginning of the report period.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit (invalid) balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property BeforeAcquisitionAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_BeforeAcquisitionAccountValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        ''' <summary>
        ''' A balance for the <see cref="AccountAccumulatedAmortization">AccountAccumulatedAmortization</see> at the beginning of the report period.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit (invalid) balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property BeforeAmortizationAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_BeforeAmortizationAccountValue)
            End Get
        End Property

        ''' <summary>
        ''' A balance for the <see cref="AccountAccumulatedAmortization">AccountAccumulatedAmortization</see> per unit at the beginning of the report period.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit (invalid) balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property BeforeAmortizationAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_BeforeAmortizationAccountValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        ''' <summary>
        ''' A balance for the <see cref="AccountValueDecrease">AccountValueDecrease</see> at the beginning of the report period.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit (invalid) balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property BeforeValueDecreaseAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_BeforeValueDecreaseAccountValue)
            End Get
        End Property

        ''' <summary>
        ''' A balance for the <see cref="AccountValueDecrease">AccountValueDecrease</see> per unit at the beginning of the report period.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit (invalid) balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property BeforeValueDecreaseAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_BeforeValueDecreaseAccountValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        ''' <summary>
        ''' A balance for the <see cref="AccountValueIncrease">AccountValueIncrease</see> at the beginning of the report period.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit (invalid) balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property BeforeValueIncreaseAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_BeforeValueIncreaseAccountValue)
            End Get
        End Property

        ''' <summary>
        ''' A balance for the <see cref="AccountValueIncrease">AccountValueIncrease</see> per unit at the beginning of the report period.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit (invalid) balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property BeforeValueIncreaseAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_BeforeValueIncreaseAccountValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        ''' <summary>
        ''' A balance for the <see cref="AccountRevaluedPortionAmmortization">AccountRevaluedPortionAmmortization</see> at the beginning of the report period.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit (invalid) balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property BeforeValueIncreaseAmmortizationAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_BeforeValueIncreaseAmmortizationAccountValue)
            End Get
        End Property

        ''' <summary>
        ''' A balance for the <see cref="AccountRevaluedPortionAmmortization">AccountRevaluedPortionAmmortization</see> per unit at the beginning of the report period.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit (invalid) balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property BeforeValueIncreaseAmmortizationAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_BeforeValueIncreaseAmmortizationAccountValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        ''' <summary>
        ''' A total value of the long term asset at the beginning of the report period.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property BeforeValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_BeforeValue)
            End Get
        End Property

        ''' <summary>
        ''' A unit value of the long term asset at the beginning of the report period.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property BeforeValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_BeforeValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        ''' <summary>
        ''' An amount of the long term asset at the beginning of the report period.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property BeforeAmmount() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _BeforeAmmount
            End Get
        End Property

        ''' <summary>
        ''' A change of balance for the <see cref="AccountAcquisition">AccountAcquisition</see> during the report period.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property ChangeAcquisitionAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ChangeAcquisitionAccountValue)
            End Get
        End Property

        ''' <summary>
        ''' A change of balance for the <see cref="AccountAcquisition">AccountAcquisition</see> per unit during the report period.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property ChangeAcquisitionAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ChangeAcquisitionAccountValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        ''' <summary>
        ''' A change of balance for the <see cref="AccountAccumulatedAmortization">AccountAccumulatedAmortization</see> during the report period.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property ChangeAmortizationAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ChangeAmortizationAccountValue)
            End Get
        End Property

        ''' <summary>
        ''' A change of balance for the <see cref="AccountAccumulatedAmortization">AccountAccumulatedAmortization</see> per unit during the report period.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property ChangeAmortizationAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ChangeAmortizationAccountValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        ''' <summary>
        ''' A change of balance for the <see cref="AccountValueDecrease">AccountValueDecrease</see> during the report period.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property ChangeValueDecreaseAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ChangeValueDecreaseAccountValue)
            End Get
        End Property

        ''' <summary>
        ''' A change of balance for the <see cref="AccountValueDecrease">AccountValueDecrease</see> per unit during the report period.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property ChangeValueDecreaseAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ChangeValueDecreaseAccountValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        ''' <summary>
        ''' A change of balance for the <see cref="AccountValueIncrease">AccountValueIncrease</see> during the report period.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property ChangeValueIncreaseAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ChangeValueIncreaseAccountValue)
            End Get
        End Property

        ''' <summary>
        ''' A change of balance for the <see cref="AccountValueIncrease">AccountValueIncrease</see> per unit during the report period.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property ChangeValueIncreaseAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ChangeValueIncreaseAccountValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        ''' <summary>
        ''' A change of balance for the <see cref="AccountRevaluedPortionAmmortization">AccountRevaluedPortionAmmortization</see> during the report period.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property ChangeValueIncreaseAmmortizationAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ChangeValueIncreaseAmmortizationAccountValue)
            End Get
        End Property

        ''' <summary>
        ''' A change of balance for the <see cref="AccountRevaluedPortionAmmortization">AccountRevaluedPortionAmmortization</see> per unit during the report period.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents denbit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property ChangeValueIncreaseAmmortizationAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ChangeValueIncreaseAmmortizationAccountValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        ''' <summary>
        ''' A change of the total value of the long term asset during the report period.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property ChangeValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ChangeValue)
            End Get
        End Property

        ''' <summary>
        ''' A change of the unit value of the long term asset during the report period.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property ChangeValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ChangeValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        ''' <summary>
        ''' A change of the amount of the long term asset during the report period.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property ChangeAmmount() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ChangeAmmount
            End Get
        End Property

        ''' <summary>
        ''' An amount of the long term asset that was acquired during the report period.
        ''' </summary>
        ''' <remarks>Equals <see cref="Ammount">Ammount</see> if the asset was acquired 
        ''' during the report period. Otherwise equals zero.</remarks>
        Public ReadOnly Property ChangeAmmountAcquired() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ChangeAmmountAcquired
            End Get
        End Property

        ''' <summary>
        ''' An total value of the long term asset that was acquired during the report period.
        ''' </summary>
        ''' <remarks>Equals <see cref="Value">Value</see> if the asset was acquired 
        ''' during the report period. Otherwise equals zero.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property ChangeValueAcquired() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ChangeValueAcquired)
            End Get
        End Property

        ''' <summary>
        ''' An amount of the long term asset that was transfered (sold etc) during the report period.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property ChangeAmmountTransfered() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ChangeAmmountTransfered
            End Get
        End Property

        ''' <summary>
        ''' A total (balance) value of the long term asset that was transfered (sold etc) during the report period.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property ChangeValueTransfered() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ChangeValueTransfered)
            End Get
        End Property

        ''' <summary>
        ''' An amount of the long term asset that was discarded during the report period.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property ChangeAmmountDiscarded() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ChangeAmmountDiscarded
            End Get
        End Property

        ''' <summary>
        ''' A total (balance) value of the long term asset that was discarded during the report period.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property ChangeValueDiscarded() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ChangeValueDiscarded)
            End Get
        End Property

        ''' <summary>
        ''' A balance for the <see cref="AccountAcquisition">AccountAcquisition</see> at the end of the report period.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit (invalid) balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property AfterAcquisitionAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AfterAcquisitionAccountValue)
            End Get
        End Property

        ''' <summary>
        ''' A balance for the <see cref="AccountAcquisition">AccountAcquisition</see> per unit at the end of the report period.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit (invalid) balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property AfterAcquisitionAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AfterAcquisitionAccountValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        ''' <summary>
        ''' A balance for the <see cref="AccountAccumulatedAmortization">AccountAccumulatedAmortization</see> at the end of the report period.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit (invalid) balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property AfterAmortizationAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AfterAmortizationAccountValue)
            End Get
        End Property

        ''' <summary>
        ''' A balance for the <see cref="AccountAccumulatedAmortization">AccountAccumulatedAmortization</see> per unit at the end of the report period.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit (invalid) balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property AfterAmortizationAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AfterAmortizationAccountValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        ''' <summary>
        ''' A balance for the <see cref="AccountValueDecrease">AccountValueDecrease</see> at the end of the report period.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit (invalid) balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property AfterValueDecreaseAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AfterValueDecreaseAccountValue)
            End Get
        End Property

        ''' <summary>
        ''' A balance for the <see cref="AccountValueDecrease">AccountValueDecrease</see> per unit at the end of the report period.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit (invalid) balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property AfterValueDecreaseAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AfterValueDecreaseAccountValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        ''' <summary>
        ''' A balance for the <see cref="AccountValueIncrease">AccountValueIncrease</see> at the end of the report period.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit (invalid) balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property AfterValueIncreaseAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AfterValueIncreaseAccountValue)
            End Get
        End Property

        ''' <summary>
        ''' A balance for the <see cref="AccountValueIncrease">AccountValueIncrease</see> per unit at the end of the report period.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit (invalid) balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property AfterValueIncreaseAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AfterValueIncreaseAccountValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        ''' <summary>
        ''' A balance for the <see cref="AccountRevaluedPortionAmmortization">AccountRevaluedPortionAmmortization</see> at the end of the report period.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit (invalid) balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property AfterValueIncreaseAmmortizationAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AfterValueIncreaseAmmortizationAccountValue)
            End Get
        End Property

        ''' <summary>
        ''' A balance for the <see cref="AccountRevaluedPortionAmmortization">AccountRevaluedPortionAmmortization</see> per unit at the end of the report period.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit (invalid) balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property AfterValueIncreaseAmmortizationAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AfterValueIncreaseAmmortizationAccountValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        ''' <summary>
        ''' A total value of the long term asset at the end of the report period.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property AfterValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AfterValue)
            End Get
        End Property

        ''' <summary>
        ''' A unit value of the long term asset at the end of the report period.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property AfterValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AfterValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        ''' <summary>
        ''' An amount of the long term asset at the end of the report period.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property AfterAmmount() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AfterAmmount
            End Get
        End Property



        Protected Overrides Function GetIdValue() As Object
            Return _ID
        End Function

        Public Overrides Function ToString() As String
            Return _Name
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function GetLongTermAssetInfo(ByVal acquisitionDataRow As DataRow, _
            ByVal statusBeforeDataRow As DataRow, ByVal changesDataRow As DataRow, _
            ByVal dateFrom As Date, ByVal dateTo As Date) As LongTermAssetInfo
            Return New LongTermAssetInfo(acquisitionDataRow, statusBeforeDataRow, _
                changesDataRow, dateFrom, dateTo)
        End Function


        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal acquisitionDataRow As DataRow, _
            ByVal statusBeforeDataRow As DataRow, ByVal changesDataRow As DataRow, _
            ByVal dateFrom As Date, ByVal dateTo As Date)
            Fetch(acquisitionDataRow, statusBeforeDataRow, changesDataRow, dateFrom, dateTo)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal acquisitionDataRow As DataRow, _
            ByVal statusBeforeDataRow As DataRow, ByVal changesDataRow As DataRow, _
            ByVal dateFrom As Date, ByVal dateTo As Date)

            _ID = CIntSafe(acquisitionDataRow.Item(0), 0)
            _Name = CStrSafe(acquisitionDataRow.Item(1)).Trim
            _MeasureUnit = CStrSafe(acquisitionDataRow.Item(2)).Trim
            _LegalGroup = CStrSafe(acquisitionDataRow.Item(3)).Trim
            _CustomGroup = CStrSafe(acquisitionDataRow.Item(4)).Trim
            _InventoryNumber = CStrSafe(acquisitionDataRow.Item(5)).Trim
            _AcquisitionJournalEntryID = CIntSafe(acquisitionDataRow.Item(6), 0)
            _AcquisitionDate = CDateSafe(acquisitionDataRow.Item(7), Today)
            _AcquisitionJournalEntryDocNumber = CStrSafe(acquisitionDataRow.Item(8)).Trim
            _AcquisitionJournalEntryDocContent = CStrSafe(acquisitionDataRow.Item(9)).Trim
            _AcquisitionJournalEntryDocType = ConvertEnumHumanReadable( _
                ConvertEnumDatabaseStringCode(Of DocumentType)(CStrSafe(acquisitionDataRow.Item(10))))
            _AccountAcquisition = CIntSafe(acquisitionDataRow.Item(11), 0)
            _AccountAccumulatedAmortization = CIntSafe(acquisitionDataRow.Item(12), 0)
            _AccountValueDecrease = CIntSafe(acquisitionDataRow.Item(13), 0)
            _AccountValueIncrease = CIntSafe(acquisitionDataRow.Item(14), 0)
            _AccountRevaluedPortionAmmortization = CIntSafe(acquisitionDataRow.Item(15), 0)
            _LiquidationUnitValue = CDblSafe(acquisitionDataRow.Item(16), ROUNDUNITASSET, 0)
            _DefaultAmortizationPeriod = CIntSafe(acquisitionDataRow.Item(17), 0)
            _AcquisitionAccountValuePerUnit = CDblSafe(acquisitionDataRow.Item(18), ROUNDUNITASSET, 0)
            _Ammount = CIntSafe(acquisitionDataRow.Item(19), 0)
            _AcquisitionAccountValue = CDblSafe(acquisitionDataRow.Item(20), 2, 0)
            _AmortizationAccountValue = CDblSafe(acquisitionDataRow.Item(23), 2, 0)
            _AmortizationAccountValuePerUnit = CDblSafe(acquisitionDataRow.Item(24), ROUNDUNITASSET, 0)
            _ValueIncreaseAmmortizationAccountValue = CDblSafe(acquisitionDataRow.Item(25), 2, 0)
            _ValueIncreaseAmmortizationAccountValuePerUnit = CDblSafe(acquisitionDataRow.Item(26), ROUNDUNITASSET, 0)
            _ContinuedUsage = ConvertDbBoolean(CIntSafe(acquisitionDataRow.Item(27), 0))

            If CDblSafe(acquisitionDataRow.Item(21)) < 0 Then
                _ValueDecreaseAccountValuePerUnit = -CDblSafe(acquisitionDataRow.Item(21), ROUNDUNITASSET, 0)
                _ValueIncreaseAccountValuePerUnit = 0
            ElseIf CDblSafe(acquisitionDataRow.Item(21)) > 0 Then
                _ValueIncreaseAccountValuePerUnit = CDblSafe(acquisitionDataRow.Item(21), ROUNDUNITASSET, 0)
                _ValueDecreaseAccountValuePerUnit = 0
            Else
                _ValueIncreaseAccountValuePerUnit = 0
                _ValueDecreaseAccountValuePerUnit = 0
            End If
            If CDblSafe(acquisitionDataRow.Item(22)) < 0 Then
                _ValueDecreaseAccountValue = -CDblSafe(acquisitionDataRow.Item(22), 2, 0)
                _ValueIncreaseAccountValue = 0
            ElseIf CDblSafe(acquisitionDataRow.Item(22)) > 0 Then
                _ValueIncreaseAccountValue = CDblSafe(acquisitionDataRow.Item(22), 2, 0)
                _ValueDecreaseAccountValue = 0
            Else
                _ValueIncreaseAccountValue = 0
                _ValueDecreaseAccountValue = 0
            End If

            _ValueIncreaseAccountValue = CRound(_ValueIncreaseAccountValue + _
                _ValueIncreaseAmmortizationAccountValue)
            _ValueIncreaseAccountValuePerUnit = CRound(_ValueIncreaseAccountValuePerUnit + _
                _ValueIncreaseAmmortizationAccountValuePerUnit, ROUNDUNITASSET)

            _Value = CRound(_AcquisitionAccountValue - _
                _AmortizationAccountValue - _ValueDecreaseAccountValue + _
                _ValueIncreaseAccountValue - _ValueIncreaseAmmortizationAccountValue)
            _ValuePerUnit = CRound(_AcquisitionAccountValuePerUnit - _
                _AmortizationAccountValuePerUnit - _ValueDecreaseAccountValuePerUnit + _
                _ValueIncreaseAccountValuePerUnit - _
                _ValueIncreaseAmmortizationAccountValuePerUnit, ROUNDUNITASSET)
            If _ValueDecreaseAccountValue > 0 Then
                _ValueRevaluedPortion = -CRound(_ValueDecreaseAccountValue)
                _ValueRevaluedPortionPerUnit = -CRound(_ValueDecreaseAccountValuePerUnit, ROUNDUNITASSET)
            ElseIf _ValueIncreaseAccountValue > 0 Then
                _ValueRevaluedPortion = CRound(_ValueIncreaseAccountValue _
                    - _ValueIncreaseAmmortizationAccountValue)
                _ValueRevaluedPortionPerUnit = CRound(_ValueIncreaseAccountValuePerUnit _
                    - _ValueIncreaseAmmortizationAccountValuePerUnit, ROUNDUNITASSET)
            Else
                _ValueRevaluedPortion = 0
                _ValueRevaluedPortionPerUnit = 0
            End If



            If _AcquisitionDate.Date >= dateFrom.Date Then
                _BeforeAcquisitionAccountValue = 0
                _BeforeAcquisitionAccountValuePerUnit = 0
                _BeforeValueDecreaseAccountValue = 0
                _BeforeValueDecreaseAccountValuePerUnit = 0
                _BeforeValueIncreaseAccountValue = 0
                _BeforeValueIncreaseAccountValuePerUnit = 0
                _BeforeAmortizationAccountValue = 0
                _BeforeAmortizationAccountValuePerUnit = 0
                _BeforeValueIncreaseAmmortizationAccountValue = 0
                _BeforeValueIncreaseAmmortizationAccountValuePerUnit = 0
                _BeforeAmmount = 0
            ElseIf statusBeforeDataRow Is Nothing Then
                _BeforeAcquisitionAccountValue = CRound(_AcquisitionAccountValue)
                _BeforeAcquisitionAccountValuePerUnit = CRound(_AcquisitionAccountValuePerUnit, ROUNDUNITASSET)
                _BeforeValueDecreaseAccountValue = CRound(_ValueDecreaseAccountValue)
                _BeforeValueDecreaseAccountValuePerUnit = CRound(_ValueDecreaseAccountValuePerUnit, ROUNDUNITASSET)
                _BeforeValueIncreaseAccountValue = CRound(_ValueIncreaseAccountValue)
                _BeforeValueIncreaseAccountValuePerUnit = CRound(_ValueIncreaseAccountValuePerUnit, ROUNDUNITASSET)
                _BeforeAmortizationAccountValue = CRound(_AmortizationAccountValue)
                _BeforeAmortizationAccountValuePerUnit = CRound(_AmortizationAccountValuePerUnit, ROUNDUNITASSET)
                _BeforeValueIncreaseAmmortizationAccountValue = CRound(_ValueIncreaseAmmortizationAccountValue)
                _BeforeValueIncreaseAmmortizationAccountValuePerUnit = _
                    CRound(_ValueIncreaseAmmortizationAccountValuePerUnit, ROUNDUNITASSET)
                _BeforeAmmount = _Ammount
            Else
                _BeforeAcquisitionAccountValue = CRound(CDblSafe(statusBeforeDataRow.Item(1), 2, 0) + _
                    _AcquisitionAccountValue)
                _BeforeAcquisitionAccountValuePerUnit = CRound(CDblSafe(statusBeforeDataRow.Item(2), ROUNDUNITASSET, 0) + _
                    _AcquisitionAccountValuePerUnit, ROUNDUNITASSET)
                _BeforeValueDecreaseAccountValue = CRound(CDblSafe(statusBeforeDataRow.Item(3), 2, 0) + _
                    _ValueDecreaseAccountValue)
                _BeforeValueDecreaseAccountValuePerUnit = CRound(CDblSafe(statusBeforeDataRow.Item(4), ROUNDUNITASSET, 0) + _
                    _ValueDecreaseAccountValuePerUnit, ROUNDUNITASSET)
                _BeforeValueIncreaseAccountValue = CRound(CDblSafe(statusBeforeDataRow.Item(5), 2, 0) + _
                    _ValueIncreaseAccountValue)
                _BeforeValueIncreaseAccountValuePerUnit = CRound(CDblSafe(statusBeforeDataRow.Item(6), ROUNDUNITASSET, 0) + _
                    _ValueIncreaseAccountValuePerUnit, ROUNDUNITASSET)
                _BeforeAmortizationAccountValue = CRound(CDblSafe(statusBeforeDataRow.Item(7), 2, 0) + _
                    _AmortizationAccountValue)
                _BeforeAmortizationAccountValuePerUnit = CRound(CDblSafe(statusBeforeDataRow.Item(8), ROUNDUNITASSET, 0) + _
                    _AmortizationAccountValuePerUnit, ROUNDUNITASSET)
                _BeforeValueIncreaseAmmortizationAccountValue = CRound(CDblSafe(statusBeforeDataRow.Item(9), 2, 0) + _
                    _ValueIncreaseAmmortizationAccountValue)
                _BeforeValueIncreaseAmmortizationAccountValuePerUnit = _
                    CRound(CDblSafe(statusBeforeDataRow.Item(10), ROUNDUNITASSET, 0) _
                    + _ValueIncreaseAmmortizationAccountValuePerUnit, ROUNDUNITASSET)
                _BeforeAmmount = _Ammount - CIntSafe(statusBeforeDataRow.Item(11))
            End If
            _BeforeValue = CRound(_BeforeAcquisitionAccountValue - _BeforeAmortizationAccountValue _
                - _BeforeValueDecreaseAccountValue - _BeforeValueIncreaseAmmortizationAccountValue _
                + _BeforeValueIncreaseAccountValue)
            _BeforeValuePerUnit = CRound(_BeforeAcquisitionAccountValuePerUnit - _BeforeAmortizationAccountValuePerUnit _
                - _BeforeValueDecreaseAccountValuePerUnit - _BeforeValueIncreaseAmmortizationAccountValuePerUnit _
                + _BeforeValueIncreaseAccountValuePerUnit, ROUNDUNITASSET)



            If changesDataRow Is Nothing Then
                _ChangeAcquisitionAccountValue = 0
                _ChangeAcquisitionAccountValuePerUnit = 0
                _ChangeValueDecreaseAccountValue = 0
                _ChangeValueDecreaseAccountValuePerUnit = 0
                _ChangeValueIncreaseAccountValue = 0
                _ChangeValueIncreaseAccountValuePerUnit = 0
                _ChangeAmortizationAccountValue = 0
                _ChangeAmortizationAccountValuePerUnit = 0
                _ChangeValueIncreaseAmmortizationAccountValue = 0
                _ChangeValueIncreaseAmmortizationAccountValuePerUnit = 0
                _ChangeAmmount = 0
                _ChangeAmmountTransfered = 0
                _ChangeValueTransfered = 0
                _ChangeAmmountDiscarded = 0
                _ChangeValueDiscarded = 0
            Else
                _ChangeAcquisitionAccountValue = CDblSafe(changesDataRow.Item(1), 2, 0)
                _ChangeAcquisitionAccountValuePerUnit = CDblSafe(changesDataRow.Item(2), ROUNDUNITASSET, 0)
                _ChangeValueDecreaseAccountValue = CDblSafe(changesDataRow.Item(3), 2, 0)
                _ChangeValueDecreaseAccountValuePerUnit = CDblSafe(changesDataRow.Item(4), ROUNDUNITASSET, 0)
                _ChangeValueIncreaseAccountValue = CDblSafe(changesDataRow.Item(5), 2, 0)
                _ChangeValueIncreaseAccountValuePerUnit = CDblSafe(changesDataRow.Item(6), ROUNDUNITASSET, 0)
                _ChangeAmortizationAccountValue = CDblSafe(changesDataRow.Item(7), 2, 0)
                _ChangeAmortizationAccountValuePerUnit = CDblSafe(changesDataRow.Item(8), ROUNDUNITASSET, 0)
                _ChangeValueIncreaseAmmortizationAccountValue = CDblSafe(changesDataRow.Item(9), 2, 0)
                _ChangeValueIncreaseAmmortizationAccountValuePerUnit = CDblSafe(changesDataRow.Item(10), ROUNDUNITASSET, 0)
                _ChangeAmmount = -CIntSafe(changesDataRow.Item(11), 0)
                _ChangeAmmountTransfered = CIntSafe(changesDataRow.Item(12), 0)
                _ChangeValueTransfered = CDblSafe(changesDataRow.Item(13), 2, 0)
                _ChangeAmmountDiscarded = CIntSafe(changesDataRow.Item(14), 0)
                _ChangeValueDiscarded = CDblSafe(changesDataRow.Item(15), 2, 0)
            End If
            If _AcquisitionDate.Date >= dateFrom.Date AndAlso _AcquisitionDate.Date <= dateTo.Date Then
                _ChangeAcquisitionAccountValue = CRound(_ChangeAcquisitionAccountValue _
                    + _AcquisitionAccountValue)
                _ChangeAcquisitionAccountValuePerUnit = CRound(_ChangeAcquisitionAccountValuePerUnit _
                    + _AcquisitionAccountValuePerUnit, ROUNDUNITASSET)
                _ChangeValueDecreaseAccountValue = CRound(_ChangeValueDecreaseAccountValue _
                    + _ValueDecreaseAccountValue)
                _ChangeValueDecreaseAccountValuePerUnit = CRound(_ChangeValueDecreaseAccountValuePerUnit _
                    + _ValueDecreaseAccountValuePerUnit, ROUNDUNITASSET)
                _ChangeValueIncreaseAccountValue = CRound(_ChangeValueIncreaseAccountValue _
                    + _ValueIncreaseAccountValue)
                _ChangeValueIncreaseAccountValuePerUnit = CRound(_ChangeValueIncreaseAccountValuePerUnit _
                    + _ValueIncreaseAccountValuePerUnit, ROUNDUNITASSET)
                _ChangeAmortizationAccountValue = CRound(_ChangeAmortizationAccountValue _
                    + AmortizationAccountValue)
                _ChangeAmortizationAccountValuePerUnit = CRound(_ChangeAmortizationAccountValuePerUnit _
                    + AmortizationAccountValuePerUnit, ROUNDUNITASSET)
                _ChangeValueIncreaseAmmortizationAccountValue = _
                    CRound(_ChangeValueIncreaseAmmortizationAccountValue _
                    + _ValueIncreaseAmmortizationAccountValue)
                _ChangeValueIncreaseAmmortizationAccountValuePerUnit = _
                    CRound(_ChangeValueIncreaseAmmortizationAccountValuePerUnit _
                    + _ValueIncreaseAmmortizationAccountValuePerUnit, ROUNDUNITASSET)
                _ChangeAmmount = _ChangeAmmount + _Ammount
                _ChangeAmmountAcquired = _Ammount
                _ChangeValueAcquired = _Value
            Else
                _ChangeAmmountAcquired = 0
                _ChangeValueAcquired = 0
            End If
            _ChangeValue = CRound(_ChangeAcquisitionAccountValue - _ChangeAmortizationAccountValue _
                - _ChangeValueDecreaseAccountValue - _ChangeValueIncreaseAmmortizationAccountValue _
                + _ChangeValueIncreaseAccountValue)
            _ChangeValuePerUnit = CRound(_ChangeAcquisitionAccountValuePerUnit - _ChangeAmortizationAccountValuePerUnit _
               - _ChangeValueDecreaseAccountValuePerUnit - _ChangeValueIncreaseAmmortizationAccountValuePerUnit _
               + _ChangeValueIncreaseAccountValuePerUnit, ROUNDUNITASSET)



            _AfterAcquisitionAccountValue = CRound(_BeforeAcquisitionAccountValue _
                + _ChangeAcquisitionAccountValue)
            _AfterAcquisitionAccountValuePerUnit = CRound(_BeforeAcquisitionAccountValuePerUnit _
               + _ChangeAcquisitionAccountValuePerUnit, ROUNDUNITASSET)
            _AfterAmortizationAccountValue = CRound(_BeforeAmortizationAccountValue _
                + _ChangeAmortizationAccountValue)
            _AfterAmortizationAccountValuePerUnit = CRound(_BeforeAmortizationAccountValuePerUnit _
                + _ChangeAmortizationAccountValuePerUnit, ROUNDUNITASSET)
            _AfterValueDecreaseAccountValue = CRound(_BeforeValueDecreaseAccountValue _
                + _ChangeValueDecreaseAccountValue)
            _AfterValueDecreaseAccountValuePerUnit = CRound(_BeforeValueDecreaseAccountValuePerUnit _
                + _ChangeValueDecreaseAccountValuePerUnit, ROUNDUNITASSET)
            _AfterValueIncreaseAccountValue = CRound(_BeforeValueIncreaseAccountValue _
                + _ChangeValueIncreaseAccountValue)
            _AfterValueIncreaseAccountValuePerUnit = CRound(_BeforeValueIncreaseAccountValuePerUnit _
                + _ChangeValueIncreaseAccountValuePerUnit, ROUNDUNITASSET)
            _AfterValueIncreaseAmmortizationAccountValue = _
                CRound(_BeforeValueIncreaseAmmortizationAccountValue _
                + _ChangeValueIncreaseAmmortizationAccountValue)
            _AfterValueIncreaseAmmortizationAccountValuePerUnit = _
                CRound(_BeforeValueIncreaseAmmortizationAccountValuePerUnit + _
                _ChangeValueIncreaseAmmortizationAccountValuePerUnit, ROUNDUNITASSET)
            _AfterAmmount = _BeforeAmmount + _ChangeAmmount
            _AfterValue = CRound(_AfterAcquisitionAccountValue - _AfterAmortizationAccountValue _
               - _AfterValueDecreaseAccountValue - _AfterValueIncreaseAmmortizationAccountValue _
               + _AfterValueIncreaseAccountValue)
            _AfterValuePerUnit = CRound(_AfterAcquisitionAccountValuePerUnit - _AfterAmortizationAccountValuePerUnit _
               - _AfterValueDecreaseAccountValuePerUnit - _AfterValueIncreaseAmmortizationAccountValuePerUnit _
               + _AfterValueIncreaseAccountValuePerUnit, ROUNDUNITASSET)

        End Sub

#End Region

    End Class

End Namespace