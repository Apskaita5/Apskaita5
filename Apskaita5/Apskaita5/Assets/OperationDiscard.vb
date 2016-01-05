Imports Csla.Validation

Namespace Assets

    ''' <summary>
    ''' Represents a long term asset discard operation.
    ''' </summary>
    ''' <remarks>Values are stored in the database table turtas_op.
    ''' Operation data is persisted by the <see cref="OperationPersistenceObject">OperationPersistenceObject</see>.</remarks>
    <Serializable()> _
    Public Class OperationDiscard
        Inherits BusinessBase(Of OperationDiscard)
        Implements IGetErrorForListItem, IIsDirtyEnough

#Region " Business Methods "

        Private _Background As OperationBackground = Nothing
        Private _ChronologyValidator As OperationChronologicValidator = Nothing

        Private ReadOnly _Guid As Guid = Guid.NewGuid
        Private _ID As Integer = -1
        Private _InsertDate As DateTime = Now
        Private _UpdateDate As DateTime = Now
        Private _IsComplexAct As Boolean = False
        Private _ComplexActID As Integer = 0
        Private _Date As Date = Today.Date
        Private _Content As String = ""
        Private _AccountCosts As Long = 0
        Private _DocumentNumber As String = ""
        Private _JournalEntryID As Integer = -1
        Private _AmountToDiscard As Integer = 0


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
        ''' Gets a type of the long term asset operation, i.e. <see cref="LtaOperationType.Discard">LtaOperationType.Discard</see>.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property [Type]() As LtaOperationType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return LtaOperationType.Discard
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
        ''' Gets <see cref="IChronologicValidator">IChronologicValidator</see> object that contains business restraints on updating the operation.
        ''' </summary>
        ''' <remarks>A <see cref="OperationChronologicValidator">OperationChronologicValidator</see> 
        ''' is used to validate a long term asset Discard operation chronological business rules.</remarks>
        Public ReadOnly Property ChronologyValidator() As OperationChronologicValidator
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ChronologyValidator
            End Get
        End Property

        ''' <summary>
        ''' Gets background information for the operation. 
        ''' Describes the long term asset operation state before and after 
        ''' the operation takes place.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Background() As OperationBackground
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Background
            End Get
        End Property

        ''' <summary>
        ''' Whether the long term asset operation is a part of a complex long term asset operation (mass discard etc.).
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas_op.IsComplexAct.</remarks>
        Public ReadOnly Property IsComplexAct() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsComplexAct
            End Get
        End Property

        ''' <summary>
        ''' Gets an ID of the long term asset complex operation.
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas_op.IsComplexAct.</remarks>
        Public ReadOnly Property ComplexActID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ComplexActID
            End Get
        End Property

#Region " General Asset Data "

        ''' <summary>
        ''' An <see cref="LongTermAsset.ID">ID of the long term asset</see>.
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas_op.T_ID.
        ''' A proxy to the <see cref="Background">Background</see>
        ''' to be used when databinding to a datagridview, because
        ''' datagridview does not support binding to the incapsulated object properties.</remarks>
        Public ReadOnly Property AssetID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Background.AssetID
            End Get
        End Property

        ''' <summary>
        ''' A <see cref="LongTermAsset.Name">name of the long term asset</see>.
        ''' </summary>
        ''' <remarks>A proxy to the <see cref="Background">Background</see>
        ''' to be used when databinding to a datagridview, because
        ''' datagridview does not support binding to the incapsulated object properties.</remarks>
        Public ReadOnly Property AssetName() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Background.AssetName
            End Get
        End Property

        ''' <summary>
        ''' A <see cref="LongTermAsset.MeasureUnit">measure unit of the long term asset</see>.
        ''' </summary>
        ''' <remarks>A proxy to the <see cref="Background">Background</see>
        ''' to be used when databinding to a datagridview, because
        ''' datagridview does not support binding to the incapsulated object properties.</remarks>
        Public ReadOnly Property AssetMeasureUnit() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Background.AssetMeasureUnit
            End Get
        End Property

        ''' <summary>
        ''' A <see cref="LongTermAsset.AcquisitionDate">date of the long term asset aquisition</see>.
        ''' </summary>
        ''' <remarks>A proxy to the <see cref="Background">Background</see>
        ''' to be used when databinding to a datagridview, because
        ''' datagridview does not support binding to the incapsulated object properties.</remarks>
        Public ReadOnly Property AssetDateAcquired() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Background.AssetDateAcquired
            End Get
        End Property

        ''' <summary>
        ''' An <see cref="LongTermAsset.AcquisitionJournalEntryID">ID of the journal entry of the long term asset aquisition</see>.
        ''' </summary>
        ''' <remarks>A proxy to the <see cref="Background">Background</see>
        ''' to be used when databinding to a datagridview, because
        ''' datagridview does not support binding to the incapsulated object properties.</remarks>
        Public ReadOnly Property AssetAquisitionID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Background.AssetAquisitionID
            End Get
        End Property

        ''' <summary>
        ''' A <see cref="LongTermAsset.LiquidationUnitValue">liquidation value of the long term asset per unit</see>.
        ''' </summary>
        ''' <remarks>12 BAS para 57.
        ''' A proxy to the <see cref="Background">Background</see>
        ''' to be used when databinding to a datagridview, because
        ''' datagridview does not support binding to the incapsulated object properties.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property AssetLiquidationValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Background.AssetLiquidationValue
            End Get
        End Property

#End Region

#Region " Current State "

        ''' <summary>
        ''' A current <see cref="LongTermAsset.AccountAcquisition">acquisition account of the long term asset</see>.
        ''' </summary>
        ''' <remarks>12 BAS para 12.
        ''' A proxy to the <see cref="Background">Background</see>
        ''' to be used when databinding to a datagridview, because
        ''' datagridview does not support binding to the incapsulated object properties.</remarks>
        Public ReadOnly Property CurrentAssetAcquiredAccount() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Background.CurrentAssetAcquiredAccount
            End Get
        End Property

        ''' <summary>
        ''' A current <see cref="LongTermAsset.AccountAccumulatedAmortization">amortization account of the long term asset</see>.
        ''' </summary>
        ''' <remarks>12 BAS para 68.
        ''' A proxy to the <see cref="Background">Background</see>
        ''' to be used when databinding to a datagridview, because
        ''' datagridview does not support binding to the incapsulated object properties.</remarks>
        Public ReadOnly Property CurrentAssetContraryAccount() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Background.CurrentAssetContraryAccount
            End Get
        End Property

        ''' <summary>
        ''' A current <see cref="LongTermAsset.AccountValueDecrease">value decrease account of the long term asset</see>.
        ''' </summary>
        ''' <remarks>12 BAS para 49.
        ''' A proxy to the <see cref="Background">Background</see>
        ''' to be used when databinding to a datagridview, because
        ''' datagridview does not support binding to the incapsulated object properties.</remarks>
        Public ReadOnly Property CurrentAssetValueDecreaseAccount() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Background.CurrentAssetValueDecreaseAccount
            End Get
        End Property

        ''' <summary>
        ''' A current <see cref="LongTermAsset.AccountValueDecrease">value increase account of the long term asset</see>.
        ''' </summary>
        ''' <remarks>12 BAS para 48.
        ''' A proxy to the <see cref="Background">Background</see>
        ''' to be used when databinding to a datagridview, because
        ''' datagridview does not support binding to the incapsulated object properties.</remarks>
        Public ReadOnly Property CurrentAssetValueIncreaseAccount() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Background.CurrentAssetValueIncreaseAccount
            End Get
        End Property

        ''' <summary>
        ''' A current <see cref="LongTermAsset.AccountValueDecrease">amortization account of the long term asset 
        ''' revalued portion (accounted in the <see cref="CurrentAssetValueIncreaseAccount">CurrentAssetValueIncreaseAccount</see>)</see>.
        ''' </summary>
        ''' <remarks>12 BAS para 48.
        ''' A proxy to the <see cref="Background">Background</see>
        ''' to be used when databinding to a datagridview, because
        ''' datagridview does not support binding to the incapsulated object properties.</remarks>
        Public ReadOnly Property CurrentAssetValueIncreaseAmortizationAccount() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Background.CurrentAssetValueIncreaseAmortizationAccount
            End Get
        End Property

        ''' <summary>
        ''' A current balance for the <see cref="CurrentAssetAcquiredAccount">CurrentAssetAcquiredAccount</see>.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.
        ''' A proxy to the <see cref="Background">Background</see>
        ''' to be used when databinding to a datagridview, because
        ''' datagridview does not support binding to the incapsulated object properties.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property CurrentAcquisitionAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Background.CurrentAcquisitionAccountValue
            End Get
        End Property

        ''' <summary>
        ''' A current balance for the <see cref="CurrentAssetAcquiredAccount">CurrentAssetAcquiredAccount</see> per unit.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.
        ''' A proxy to the <see cref="Background">Background</see>
        ''' to be used when databinding to a datagridview, because
        ''' datagridview does not support binding to the incapsulated object properties.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property CurrentAcquisitionAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Background.CurrentAcquisitionAccountValuePerUnit
            End Get
        End Property

        ''' <summary>
        ''' A current balance for the <see cref="CurrentAssetContraryAccount">CurrentAssetContraryAccount</see>.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit balance.
        ''' A proxy to the <see cref="Background">Background</see>
        ''' to be used when databinding to a datagridview, because
        ''' datagridview does not support binding to the incapsulated object properties.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property CurrentAmortizationAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Background.CurrentAmortizationAccountValue
            End Get
        End Property

        ''' <summary>
        ''' A current balance for the <see cref="CurrentAssetContraryAccount">CurrentAssetContraryAccount</see> per unit.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit balance.
        ''' A proxy to the <see cref="Background">Background</see>
        ''' to be used when databinding to a datagridview, because
        ''' datagridview does not support binding to the incapsulated object properties.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property CurrentAmortizationAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Background.CurrentAmortizationAccountValuePerUnit
            End Get
        End Property

        ''' <summary>
        ''' A current balance for the <see cref="CurrentAssetValueDecreaseAccount">CurrentAssetValueDecreaseAccount</see>.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit balance.
        ''' A proxy to the <see cref="Background">Background</see>
        ''' to be used when databinding to a datagridview, because
        ''' datagridview does not support binding to the incapsulated object properties.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property CurrentValueDecreaseAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Background.CurrentValueDecreaseAccountValue
            End Get
        End Property

        ''' <summary>
        ''' A current balance for the <see cref="CurrentAssetValueDecreaseAccount">CurrentAssetValueDecreaseAccount</see> per unit.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit balance.
        ''' A proxy to the <see cref="Background">Background</see>
        ''' to be used when databinding to a datagridview, because
        ''' datagridview does not support binding to the incapsulated object properties.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property CurrentValueDecreaseAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Background.CurrentValueDecreaseAccountValuePerUnit
            End Get
        End Property

        ''' <summary>
        ''' A current balance for the <see cref="CurrentAssetValueIncreaseAccount">CurrentAssetValueIncreaseAccount</see>.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.
        ''' A proxy to the <see cref="Background">Background</see>
        ''' to be used when databinding to a datagridview, because
        ''' datagridview does not support binding to the incapsulated object properties.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property CurrentValueIncreaseAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Background.CurrentValueIncreaseAccountValue
            End Get
        End Property

        ''' <summary>
        ''' A current balance for the <see cref="CurrentAssetValueIncreaseAccount">CurrentAssetValueIncreaseAccount</see> per unit.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.
        ''' A proxy to the <see cref="Background">Background</see>
        ''' to be used when databinding to a datagridview, because
        ''' datagridview does not support binding to the incapsulated object properties.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property CurrentValueIncreaseAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Background.CurrentValueIncreaseAccountValuePerUnit
            End Get
        End Property

        ''' <summary>
        ''' A current balance for the <see cref="CurrentAssetValueIncreaseAmortizationAccount">CurrentAssetValueIncreaseAmortizationAccount</see>.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit balance.
        ''' A proxy to the <see cref="Background">Background</see>
        ''' to be used when databinding to a datagridview, because
        ''' datagridview does not support binding to the incapsulated object properties.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property CurrentValueIncreaseAmortizationAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Background.CurrentValueIncreaseAmortizationAccountValue
            End Get
        End Property

        ''' <summary>
        ''' A current balance for the <see cref="CurrentAssetValueIncreaseAmortizationAccount">CurrentAssetValueIncreaseAmortizationAccount</see> per unit.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit balance.
        ''' A proxy to the <see cref="Background">Background</see>
        ''' to be used when databinding to a datagridview, because
        ''' datagridview does not support binding to the incapsulated object properties.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property CurrentValueIncreaseAmortizationAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Background.CurrentValueIncreaseAmortizationAccountValuePerUnit
            End Get
        End Property

        ''' <summary>
        ''' A current amount of the long term asset.
        ''' </summary>
        ''' <remarks>A proxy to the <see cref="Background">Background</see>
        ''' to be used when databinding to a datagridview, because
        ''' datagridview does not support binding to the incapsulated object properties.</remarks>
        Public ReadOnly Property CurrentAssetAmount() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Background.CurrentAssetAmount
            End Get
        End Property

        ''' <summary>
        ''' A current total value of the long term asset.
        ''' </summary>
        ''' <remarks>A proxy to the <see cref="Background">Background</see>
        ''' to be used when databinding to a datagridview, because
        ''' datagridview does not support binding to the incapsulated object properties.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property CurrentAssetValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Background.CurrentAssetValue
            End Get
        End Property

        ''' <summary>
        ''' A current value of the long term asset unit.
        ''' </summary>
        ''' <remarks>A proxy to the <see cref="Background">Background</see>
        ''' to be used when databinding to a datagridview, because
        ''' datagridview does not support binding to the incapsulated object properties.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property CurrentAssetValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Background.CurrentAssetValuePerUnit
            End Get
        End Property

        ''' <summary>
        ''' A current total value of the revalued portion of the long term asset
        ''' </summary>
        ''' <remarks><see cref="CurrentAssetValueIncreaseAccount">CurrentAssetValueIncreaseAccount</see> 
        ''' minus <see cref="CurrentAssetValueIncreaseAmortizationAccount">CurrentAssetValueIncreaseAmortizationAccount</see>.
        ''' A proxy to the <see cref="Background">Background</see>
        ''' to be used when databinding to a datagridview, because
        ''' datagridview does not support binding to the incapsulated object properties.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property CurrentAssetValueRevaluedPortion() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Background.CurrentAssetValueRevaluedPortion
            End Get
        End Property

        ''' <summary>
        ''' A current value of the revalued portion of the long term asset per unit.
        ''' </summary>
        ''' <remarks><see cref="CurrentAssetValueIncreaseAccount">CurrentAssetValueIncreaseAccount</see> 
        ''' minus <see cref="CurrentAssetValueIncreaseAmortizationAccount">CurrentAssetValueIncreaseAmortizationAccount</see>
        ''' divided by the <see cref="CurrentAssetAmount">CurrentAssetAmount</see>.
        ''' A proxy to the <see cref="Background">Background</see>
        ''' to be used when databinding to a datagridview, because
        ''' datagridview does not support binding to the incapsulated object properties.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property CurrentAssetValueRevaluedPortionPerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Background.CurrentAssetValueRevaluedPortionPerUnit
            End Get
        End Property

        ''' <summary>
        ''' A current number of months that the amortization of the long term asset unit is calculated for.
        ''' </summary>
        ''' <remarks>A proxy to the <see cref="Background">Background</see>
        ''' to be used when databinding to a datagridview, because
        ''' datagridview does not support binding to the incapsulated object properties.</remarks>
        Public ReadOnly Property CurrentUsageTermMonths() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Background.CurrentUsageTermMonths
            End Get
        End Property

        ''' <summary>
        ''' A current amortization period of the long term asset (in years).
        ''' </summary>
        ''' <remarks>A proxy to the <see cref="Background">Background</see>
        ''' to be used when databinding to a datagridview, because
        ''' datagridview does not support binding to the incapsulated object properties.</remarks>
        Public ReadOnly Property CurrentAmortizationPeriod() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Background.CurrentAmortizationPeriod
            End Get
        End Property

        ''' <summary>
        ''' Whether the long term asset is currently operational.
        ''' </summary>
        ''' <remarks>A proxy to the <see cref="Background">Background</see>
        ''' to be used when databinding to a datagridview, because
        ''' datagridview does not support binding to the incapsulated object properties.</remarks>
        Public ReadOnly Property CurrentUsageStatus() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Background.CurrentUsageStatus
            End Get
        End Property

#End Region

#Region " State Delta "

        ''' <summary>
        ''' A change of balance for the <see cref="CurrentAssetAcquiredAccount">CurrentAssetAcquiredAccount</see> made by the operation.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.
        ''' A proxy to the <see cref="Background">Background</see>
        ''' to be used when databinding to a datagridview, because
        ''' datagridview does not support binding to the incapsulated object properties.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property ChangeAcquisitionAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_Background.ChangeAcquisitionAccountValue)
            End Get
        End Property

        ''' <summary>
        ''' A change of balance for the <see cref="CurrentAssetContraryAccount">CurrentAssetContraryAccount</see> made by the operation.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit balance.
        ''' A proxy to the <see cref="Background">Background</see>
        ''' to be used when databinding to a datagridview, because
        ''' datagridview does not support binding to the incapsulated object properties.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property ChangeAmortizationAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_Background.ChangeAmortizationAccountValue)
            End Get
        End Property

        ''' <summary>
        ''' A change of balance for the <see cref="CurrentAssetValueDecreaseAccount">CurrentAssetValueDecreaseAccount</see> made by the operation.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit balance.
        ''' A proxy to the <see cref="Background">Background</see>
        ''' to be used when databinding to a datagridview, because
        ''' datagridview does not support binding to the incapsulated object properties.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property ChangeValueDecreaseAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_Background.ChangeValueDecreaseAccountValue)
            End Get
        End Property

        ''' <summary>
        ''' A change of balance for the <see cref="CurrentAssetValueIncreaseAccount">CurrentAssetValueIncreaseAccount</see> made by the operation.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.
        ''' A proxy to the <see cref="Background">Background</see>
        ''' to be used when databinding to a datagridview, because
        ''' datagridview does not support binding to the incapsulated object properties.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property ChangeValueIncreaseAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_Background.ChangeValueIncreaseAccountValue)
            End Get
        End Property

        ''' <summary>
        ''' A change of balance for the <see cref="CurrentAssetValueIncreaseAmortizationAccount">CurrentAssetValueIncreaseAmortizationAccount</see> made by the operation.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit balance.
        ''' A proxy to the <see cref="Background">Background</see>
        ''' to be used when databinding to a datagridview, because
        ''' datagridview does not support binding to the incapsulated object properties.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property ChangeValueIncreaseAmortizationAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_Background.ChangeValueIncreaseAmortizationAccountValue)
            End Get
        End Property

        ''' <summary>
        ''' A change of the total value of the long term asset made by the operation.
        ''' </summary>
        ''' <remarks>A proxy to the <see cref="Background">Background</see>
        ''' to be used when databinding to a datagridview, because
        ''' datagridview does not support binding to the incapsulated object properties.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property ChangeAssetValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_Background.ChangeAssetValue)
            End Get
        End Property

        ''' <summary>
        ''' A change of the unit value of the revalued portion of the long term asset.
        ''' </summary>
        ''' <remarks>A proxy to the <see cref="Background">Background</see>
        ''' to be used when databinding to a datagridview, because
        ''' datagridview does not support binding to the incapsulated object properties.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property ChangeAssetRevaluedPortionValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_Background.ChangeAssetRevaluedPortionValue)
            End Get
        End Property

#End Region

#Region " State After The Operation "

        ''' <summary>
        ''' A balance for the <see cref="CurrentAssetAcquiredAccount">CurrentAssetAcquiredAccount</see> after the operation.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.
        ''' A proxy to the <see cref="Background">Background</see>
        ''' to be used when databinding to a datagridview, because
        ''' datagridview does not support binding to the incapsulated object properties.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property AfterOperationAcquisitionAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Background.AfterOperationAcquisitionAccountValue
            End Get
        End Property

        ''' <summary>
        ''' A balance for the <see cref="CurrentAssetContraryAccount">CurrentAssetContraryAccount</see> after the operation.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit balance.
        ''' A proxy to the <see cref="Background">Background</see>
        ''' to be used when databinding to a datagridview, because
        ''' datagridview does not support binding to the incapsulated object properties.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2, True, Double.MinValue, 0, True)> _
        Public ReadOnly Property AfterOperationAmortizationAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Background.AfterOperationAmortizationAccountValue
            End Get
        End Property

        ''' <summary>
        ''' A balance for the <see cref="CurrentAssetValueDecreaseAccount">CurrentAssetValueDecreaseAccount</see> after the operation.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit balance.
        ''' A proxy to the <see cref="Background">Background</see>
        ''' to be used when databinding to a datagridview, because
        ''' datagridview does not support binding to the incapsulated object properties.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2, True, Double.MinValue, 0, True)> _
        Public ReadOnly Property AfterOperationValueDecreaseAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Background.AfterOperationValueDecreaseAccountValue
            End Get
        End Property

        ''' <summary>
        ''' A balance for the <see cref="CurrentAssetValueIncreaseAccount">CurrentAssetValueIncreaseAccount</see> after the operation.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.
        ''' A proxy to the <see cref="Background">Background</see>
        ''' to be used when databinding to a datagridview, because
        ''' datagridview does not support binding to the incapsulated object properties.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property AfterOperationValueIncreaseAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Background.AfterOperationValueIncreaseAccountValue
            End Get
        End Property

        ''' <summary>
        ''' A balance for the <see cref="CurrentAssetValueIncreaseAmortizationAccount">CurrentAssetValueIncreaseAmortizationAccount</see> after the operation.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit balance.
        ''' A proxy to the <see cref="Background">Background</see>
        ''' to be used when databinding to a datagridview, because
        ''' datagridview does not support binding to the incapsulated object properties.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2, True, Double.MinValue, 0, True)> _
        Public ReadOnly Property AfterOperationValueIncreaseAmortizationAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Background.AfterOperationValueIncreaseAmortizationAccountValue
            End Get
        End Property

        ''' <summary>
        ''' An amount of the long term asset after the operation.
        ''' </summary>
        ''' <remarks></remarks>
        <IntegerField(ValueRequiredLevel.Optional, False)> _
        Public ReadOnly Property AfterOperationAssetAmount() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Background.AfterOperationAssetAmount
            End Get
        End Property

        ''' <summary>
        ''' A total value of the long term asset after the operation.
        ''' </summary>
        ''' <remarks>A proxy to the <see cref="Background">Background</see>
        ''' to be used when databinding to a datagridview, because
        ''' datagridview does not support binding to the incapsulated object properties.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property AfterOperationAssetValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Background.AfterOperationAssetValue
            End Get
        End Property

        ''' <summary>
        ''' A unit value of the long term asset after the operation.
        ''' </summary>
        ''' <remarks>A proxy to the <see cref="Background">Background</see>
        ''' to be used when databinding to a datagridview, because
        ''' datagridview does not support binding to the incapsulated object properties.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, ROUNDUNITASSET)> _
        Public ReadOnly Property AfterOperationAssetValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Background.AfterOperationAssetValuePerUnit
            End Get
        End Property

        ''' <summary>
        ''' A total value of the revalued portion of the long term asset after the operation.
        ''' </summary>
        ''' <remarks>A proxy to the <see cref="Background">Background</see>
        ''' to be used when databinding to a datagridview, because
        ''' datagridview does not support binding to the incapsulated object properties.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property AfterOperationAssetValueRevaluedPortion() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Background.AfterOperationAssetValueRevaluedPortion
            End Get
        End Property

        ''' <summary>
        ''' A unit value of the revalued portion of the long term asset after the operation.
        ''' </summary>
        ''' <remarks>A proxy to the <see cref="Background">Background</see>
        ''' to be used when databinding to a datagridview, because
        ''' datagridview does not support binding to the incapsulated object properties.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, ROUNDUNITASSET)> _
        Public ReadOnly Property AfterOperationAssetValueRevaluedPortionPerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Background.AfterOperationAssetValueRevaluedPortionPerUnit
            End Get
        End Property

#End Region

        ''' <summary>
        ''' Gets or sets a date of the long term asset operation.
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas_op.OperationDate.</remarks>
        Public Property [Date]() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Date
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Date)
                CanWriteProperty(True)
                If DateIsReadOnly Then Exit Property
                SetParentDate(value)
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a content (description) of the long term asset operation.
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas_op.Content.</remarks>
        <StringField(ValueRequiredLevel.Mandatory, 255)> _
        Public Property Content() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Content
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As String)
                CanWriteProperty(True)
                If ContentIsReadOnly Then Exit Property
                If value Is Nothing Then value = ""
                If _Content.Trim <> value.Trim Then
                    _Content = value.Trim
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets an <see cref="General.Account.ID">account</see> 
        ''' for the long term asset discard costs.
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas_op.AccountCorresponding.</remarks>
        <AccountField(ValueRequiredLevel.Mandatory, False, 3, 6)> _
        Public Property AccountCosts() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AccountCosts
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Long)
                CanWriteProperty(True)
                If AccountCostsIsReadOnly Then Exit Property
                If _AccountCosts <> value Then
                    _AccountCosts = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a number of the long term asset operation document.
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas_op.DocNo.</remarks>
        <StringField(ValueRequiredLevel.Mandatory, 30)> _
        Public Property DocumentNumber() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DocumentNumber
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As String)
                CanWriteProperty(True)
                If DocumentNumberIsReadOnly Then Exit Property
                If value Is Nothing Then value = ""
                If _DocumentNumber.Trim <> value.Trim Then
                    _DocumentNumber = value.Trim
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets an <see cref="General.JournalEntry.ID">ID of the journal entry</see> 
        ''' that is encapsulated by the long term asset discard operation.
        ''' </summary>
        ''' <remarks>A journal entry is encapsulated by the operation.
        ''' Value is stored in the database field turtas_op.JE_ID.</remarks>
        Public ReadOnly Property JournalEntryID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryID
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets an amount of the long term asset to discard.
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas_op.AmortizationCalculatedForMonths.</remarks>
        <IntegerField(ValueRequiredLevel.Mandatory, False)> _
        Public Property AmountToDiscard() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AmountToDiscard
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Integer)
                CanWriteProperty(True)
                If AmountToDiscardIsReadOnly Then Exit Property
                If _AmountToDiscard <> value Then
                    _AmountToDiscard = value
                    PropertyHasChanged()
                    Recalculate(True)
                End If
            End Set
        End Property


        ''' <summary>
        ''' Whether the <see cref="Date">Date</see> property is readonly.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DateIsReadOnly() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return (IsChild OrElse _IsComplexAct)
            End Get
        End Property

        ''' <summary>
        ''' Whether the <see cref="Content">Content</see> property is readonly.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property ContentIsReadOnly() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return (IsChild OrElse _IsComplexAct)
            End Get
        End Property

        ''' <summary>
        ''' Whether the <see cref="AccountCosts">AccountCosts</see> property is readonly.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property AccountCostsIsReadOnly() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return ((Not IsChild AndAlso _IsComplexAct) OrElse _
                    Not _ChronologyValidator.FinancialDataCanChange OrElse _
                    Not _ChronologyValidator.ParentFinancialDataCanChange)
            End Get
        End Property

        ''' <summary>
        ''' Whether the <see cref="DocumentNumber">DocumentNumber</see> property is readonly.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DocumentNumberIsReadOnly() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return (IsChild OrElse _IsComplexAct)
            End Get
        End Property

        ''' <summary>
        ''' Whether the <see cref="AmountToDiscard">AmountToDiscard</see> property is readonly.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property AmountToDiscardIsReadOnly() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return ((Not IsChild AndAlso _IsComplexAct) OrElse _
                    Not _ChronologyValidator.FinancialDataCanChange OrElse _
                    Not _ChronologyValidator.ParentFinancialDataCanChange)
            End Get
        End Property


        Public Overrides ReadOnly Property IsValid() As Boolean
            Get
                Return MyBase.IsValid AndAlso _Background.IsValid
            End Get
        End Property

        ''' <summary>
        ''' Returnes TRUE if the object is new and contains some user provided data 
        ''' OR
        ''' object is not new and was changed by the user.
        ''' </summary>
        Public ReadOnly Property IsDirtyEnough() As Boolean _
            Implements IIsDirtyEnough.IsDirtyEnough
            Get
                If Not IsNew Then Return IsDirty
                Return (Not StringIsNullOrEmpty(_DocumentNumber) _
                    OrElse Not StringIsNullOrEmpty(_Content) _
                    OrElse _AccountCosts > 0 _
                    OrElse AmountToDiscard > 0)
            End Get
        End Property


        ''' <summary>
        ''' Sets properties that are handled by a parent document 
        ''' and do not require realtime validation but do require validation before update.
        ''' </summary>
        ''' <param name="parentDocumentNumber">A parent document number.</param>
        ''' <param name="parentContent">A parent content.</param>
        ''' <remarks>Should be invoked before a parent document updates the operation.</remarks>
        Friend Sub SetParentProperties(ByVal parentDocumentNumber As String, _
            ByVal parentContent As String)

            If _DocumentNumber.Trim <> parentDocumentNumber.Trim Then
                _DocumentNumber = parentDocumentNumber.Trim
                PropertyHasChanged("DocumentNumber")
            End If
            If _Content.Trim <> parentContent.Trim Then
                _Content = parentContent.Trim
                PropertyHasChanged("Content")
            End If

        End Sub

        ''' <summary>
        ''' Sets a new operation date as provided by the parent document 
        ''' and recalculates the long term asset state.
        ''' </summary>
        ''' <param name="parentDate"></param>
        ''' <remarks></remarks>
        Friend Sub SetParentDate(ByVal parentDate As Date)

            If _Date.Date <> parentDate.Date Then

                _Date = parentDate.Date
                _Background.SetCurrentDate(_Date)

                PropertyHasChanged("Date")
                PropertyHasChanged("Background")

                ' affected proxy properties
                If IsChild Then
                    PropertyHasChanged("CurrentAssetAmount")
                    PropertyHasChanged("CurrentAcquisitionAccountValue")
                    PropertyHasChanged("CurrentAcquisitionAccountValuePerUnit")
                    PropertyHasChanged("CurrentAmortizationAccountValue")
                    PropertyHasChanged("CurrentAmortizationAccountValuePerUnit")
                    PropertyHasChanged("CurrentValueDecreaseAccountValue")
                    PropertyHasChanged("CurrentValueDecreaseAccountValuePerUnit")
                    PropertyHasChanged("CurrentValueIncreaseAccountValue")
                    PropertyHasChanged("CurrentValueIncreaseAccountValuePerUnit")
                    PropertyHasChanged("CurrentValueIncreaseAmortizationAccountValue")
                    PropertyHasChanged("CurrentValueIncreaseAmortizationAccountValuePerUnit")
                    PropertyHasChanged("CurrentAssetValue")
                    PropertyHasChanged("CurrentAssetValuePerUnit")
                    PropertyHasChanged("CurrentAssetValueRevaluedPortion")
                    PropertyHasChanged("CurrentAssetValueRevaluedPortionPerUnit")
                    PropertyHasChanged("CurrentAssetAcquiredAccount")
                    PropertyHasChanged("CurrentAssetContraryAccount")
                    PropertyHasChanged("CurrentAssetValueDecreaseAccount")
                    PropertyHasChanged("CurrentAssetValueIncreaseAccount")
                    PropertyHasChanged("CurrentAssetValueIncreaseAmortizationAccount")
                    PropertyHasChanged("CurrentUsageStatus")
                    PropertyHasChanged("CurrentAmortizationPeriod")
                    PropertyHasChanged("CurrentUsageTermMonths")
                    PropertyHasChanged("AfterOperationAcquisitionAccountValue")
                    PropertyHasChanged("AfterOperationAmortizationAccountValue")
                    PropertyHasChanged("AfterOperationValueDecreaseAccountValue")
                    PropertyHasChanged("AfterOperationValueIncreaseAccountValue")
                    PropertyHasChanged("AfterOperationValueIncreaseAmortizationAccountValue")
                    PropertyHasChanged("AfterOperationAssetAmount")
                    PropertyHasChanged("AfterOperationAssetValue")
                    PropertyHasChanged("AfterOperationAssetValuePerUnit")
                    PropertyHasChanged("AfterOperationAssetValueRevaluedPortion")
                    PropertyHasChanged("AfterOperationAssetValueRevaluedPortionPerUnit")
                End If

                Recalculate(True)

            End If

        End Sub



        Private Sub Recalculate(ByVal raisePropertyChanged As Boolean)

            _Background.DisableCalculations = True

            _Background.ChangeAssetAmount = -_AmountToDiscard

            Dim applicableAmount As Integer = _AmountToDiscard
            If (_Background.CurrentAssetAmount - _AmountToDiscard) < 0 Then
                applicableAmount = _Background.CurrentAssetAmount
            End If
            Dim amountLeft As Integer = _Background.CurrentAssetAmount - applicableAmount

            If applicableAmount < 1 Then

                _Background.ChangeAcquisitionAccountValue = 0
                _Background.ChangeAmortizationAccountValue = 0
                _Background.ChangeValueDecreaseAccountValue = 0
                _Background.ChangeValueIncreaseAccountValue = 0
                _Background.ChangeValueIncreaseAmortizationAccountValue = 0

            ElseIf applicableAmount = _Background.CurrentAssetAmount Then

                _Background.ChangeAcquisitionAccountValue = _
                    -_Background.CurrentAcquisitionAccountValue
                _Background.ChangeAmortizationAccountValue = _
                    -_Background.CurrentAmortizationAccountValue
                _Background.ChangeValueDecreaseAccountValue = _
                    -_Background.CurrentValueDecreaseAccountValue
                _Background.ChangeValueIncreaseAccountValue = _
                    -_Background.CurrentValueIncreaseAccountValue
                _Background.ChangeValueIncreaseAmortizationAccountValue = _
                    -_Background.CurrentValueIncreaseAmortizationAccountValue

            Else

                _Background.ChangeAcquisitionAccountValue = -CRound(_Background.CurrentAcquisitionAccountValue _
                    - CRound(amountLeft * _Background.CurrentAcquisitionAccountValuePerUnit, 2), 2)
                _Background.ChangeAmortizationAccountValue = -CRound(_Background.CurrentAmortizationAccountValue _
                    - CRound(amountLeft * _Background.CurrentAmortizationAccountValuePerUnit, 2), 2)
                _Background.ChangeValueDecreaseAccountValue = -CRound(_Background.CurrentValueDecreaseAccountValue _
                    - CRound(amountLeft * _Background.CurrentValueDecreaseAccountValuePerUnit, 2), 2)
                _Background.ChangeValueIncreaseAccountValue = -CRound(_Background.CurrentValueIncreaseAccountValue _
                    - CRound(amountLeft * _Background.CurrentValueIncreaseAccountValuePerUnit, 2), 2)
                _Background.ChangeValueIncreaseAmortizationAccountValue = _
                    -CRound(_Background.CurrentValueIncreaseAmortizationAccountValue _
                    - CRound(amountLeft * _Background.CurrentValueIncreaseAmortizationAccountValuePerUnit, 2), 2)

            End If

            _Background.DisableCalculations = False

            _Background.CalculateAfterOperationProperties()

            If raisePropertyChanged Then
                PropertyHasChanged("Background")
                ' proxy properties
                If IsChild Then
                    PropertyHasChanged("ChangeAcquisitionAccountValue")
                    PropertyHasChanged("ChangeAmortizationAccountValue")
                    PropertyHasChanged("ChangeValueDecreaseAccountValue")
                    PropertyHasChanged("ChangeValueIncreaseAccountValue")
                    PropertyHasChanged("ChangeValueIncreaseAmortizationAccountValue")
                    PropertyHasChanged("ChangeAssetValue")
                    PropertyHasChanged("ChangeAssetRevaluedPortionValue")
                    PropertyHasChanged("AfterOperationAcquisitionAccountValue")
                    PropertyHasChanged("AfterOperationAmortizationAccountValue")
                    PropertyHasChanged("AfterOperationValueDecreaseAccountValue")
                    PropertyHasChanged("AfterOperationValueIncreaseAccountValue")
                    PropertyHasChanged("AfterOperationValueIncreaseAmortizationAccountValue")
                    PropertyHasChanged("AfterOperationAssetAmount")
                    PropertyHasChanged("AfterOperationAssetValue")
                    PropertyHasChanged("AfterOperationAssetValuePerUnit")
                    PropertyHasChanged("AfterOperationAssetValueRevaluedPortion")
                    PropertyHasChanged("AfterOperationAssetValueRevaluedPortionPerUnit")
                End If
            End If

        End Sub


        Public Function GetAllBrokenRules() As String
            Dim result As String = ""
            If Not MyBase.IsValid Then result = AddWithNewLine(result, _
                Me.BrokenRulesCollection.ToString(Validation.RuleSeverity.Error), False)
            If Not _Background.IsValid Then result = AddWithNewLine(result, _
                _Background.BrokenRulesCollection.ToString(RuleSeverity.Error), False)
            Return result
        End Function

        Public Function GetAllWarnings() As String
            Dim result As String = ""
            If MyBase.BrokenRulesCollection.WarningCount > 0 Then
                result = AddWithNewLine(result, _
                    Me.BrokenRulesCollection.ToString(Validation.RuleSeverity.Warning), False)
            End If
            If _Background.BrokenRulesCollection.WarningCount > 0 Then
                result = AddWithNewLine(result, _
                    _Background.BrokenRulesCollection.ToString(Validation.RuleSeverity.Warning), False)
            End If
            Return result
        End Function

        Public Function HasWarnings() As Boolean
            Return (MyBase.BrokenRulesCollection.WarningCount > 0 OrElse _
                _Background.BrokenRulesCollection.WarningCount > 0)
        End Function

        Public Function GetErrorString() As String _
            Implements IGetErrorForListItem.GetErrorString
            If IsValid Then Return ""
            Return String.Format(My.Resources.Common_ErrorInItem, Me.ToString, _
                vbCrLf, Me.GetAllBrokenRules())
        End Function

        Public Function GetWarningString() As String _
            Implements IGetErrorForListItem.GetWarningString
            If Not HasWarnings() Then Return ""
            Return String.Format(My.Resources.Common_WarningInItem, Me.ToString, _
                vbCrLf, Me.GetAllWarnings())
        End Function


        Public Overrides Function Save() As OperationDiscard

            Me.ValidationRules.CheckRules()
            If Not Me.IsValid Then
                Throw New Exception(String.Format(My.Resources.Common_ContainsErrors, vbCrLf, _
                    GetAllBrokenRules()))
            End If

            Return MyBase.Save()

        End Function


        Protected Overrides Function GetIdValue() As Object
            Return _Guid
        End Function

        Public Overrides Function ToString() As String
            If IsChild Then
                Return String.Format(My.Resources.Assets_OperationDiscard_ToStringChild, _
                _Background.AssetName, _ID.ToString())
            Else
                Return String.Format(My.Resources.Assets_OperationDiscard_ToString, _
                    _Date.ToString("yyyy-MM-dd"), _Background.AssetName, _DocumentNumber, _
                    _ID.ToString())
            End If
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()

            ValidationRules.AddRule(AddressOf CommonValidation.AccountFieldValidation, _
                New Csla.Validation.RuleArgs("AccountCosts"))
            ValidationRules.AddRule(AddressOf CommonValidation.IntegerFieldValidation, _
                "AmountToDiscard")
            ValidationRules.AddRule(AddressOf CommonValidation.ChronologyValidation, _
                New CommonValidation.ChronologyRuleArgs("Date", "ChronologyValidator"))

            ValidationRules.AddRule(AddressOf ChildStringPropertyValidation, _
                New Csla.Validation.RuleArgs("Content"))
            ValidationRules.AddRule(AddressOf ChildStringPropertyValidation, _
                New Csla.Validation.RuleArgs("DocumentNumber"))

            ValidationRules.AddDependantProperty("ChronologyValidator", "Date", False)

        End Sub

        ''' <summary>
        ''' Rule ensuring that the parent managed string properties are only validated 
        ''' if the operation is not a child.
        ''' </summary>
        ''' <param name="target">Object containing the data to validate</param>
        ''' <param name="e">Arguments parameter specifying the name of the string
        ''' property to validate</param>
        ''' <returns><see langword="false" /> if the rule is broken</returns>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
        Private Shared Function ChildStringPropertyValidation(ByVal target As Object, _
          ByVal e As Validation.RuleArgs) As Boolean

            If DirectCast(target, OperationDiscard).IsChild Then
                Return True
            Else
                Return CommonValidation.StringFieldValidation(target, e)
            End If

        End Function

#End Region

#Region " Authorization Rules "

        Protected Overrides Sub AddAuthorizationRules()
            AuthorizationRules.AllowWrite("Assets.LongTermAssetOperation2")
        End Sub

        Public Shared Function CanAddObject() As Boolean
            Return ApplicationContext.User.IsInRole("Assets.LongTermAssetOperation2")
        End Function

        Public Shared Function CanGetObject() As Boolean
            Return ApplicationContext.User.IsInRole("Assets.LongTermAssetOperation1")
        End Function

        Public Shared Function CanEditObject() As Boolean
            Return ApplicationContext.User.IsInRole("Assets.LongTermAssetOperation3")
        End Function

        Public Shared Function CanDeleteObject() As Boolean
            Return ApplicationContext.User.IsInRole("Assets.LongTermAssetOperation3")
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets a new OperationDiscard instance as a parent (standalone) document.
        ''' </summary>
        ''' <param name="assetId">An ID of the asset that the operation operates on.</param>
        ''' <remarks></remarks>
        Public Shared Function NewOperationDiscard(ByVal assetId As Integer) As OperationDiscard
            Return DataPortal.Create(Of OperationDiscard)(New Criteria(assetId))
        End Function

        ''' <summary>
        ''' Gets a new OperationDiscard instance as a child of some parent document.
        ''' </summary>
        ''' <param name="assetId">An ID of the asset that the operation operates on.</param>
        ''' <param name="parentValidator">A parent document's IChronologyValidator.</param>
        ''' <param name="parentIsComplexAct">Whether the parent document is a complex 
        ''' long term asset operation (mass discard etc.).</param>
        ''' <remarks></remarks>
        Friend Shared Function NewOperationDiscardChild(ByVal assetId As Integer, _
            ByVal parentValidator As IChronologicValidator, _
            ByVal parentIsComplexAct As Boolean) As OperationDiscard
            Return New OperationDiscard(assetId, parentValidator, parentIsComplexAct)
        End Function


        ''' <summary>
        ''' Gets an existing OperationDiscard instance from a database 
        ''' as a parent (standalone) document.
        ''' </summary>
        ''' <param name="id">An <see cref="OperationDiscard.ID">ID of the operation</see> 
        ''' or an <see cref="OperationDiscard.JournalEntryID">ID the journal entry</see>  
        ''' that is encapsulated by the operation.</param>
        ''' <param name="nFetchByJournalEntryID">Whether the <paramref name="id">id</paramref>
        ''' param is an <see cref="OperationDiscard.JournalEntryID">ID the journal entry</see> 
        ''' that is encapsulated by the operation.</param>
        ''' <remarks></remarks>
        Public Shared Function GetOperationDiscard(ByVal id As Integer, _
            ByVal nFetchByJournalEntryID As Boolean) As OperationDiscard
            Return DataPortal.Fetch(Of OperationDiscard) _
                (New Criteria(id, nFetchByJournalEntryID))
        End Function

        ''' <summary>
        ''' Gets an existing OperationDiscard instance from a database 
        ''' as a child of some parent document.
        ''' </summary>
        ''' <param name="operationID">An <see cref="OperationDiscard.ID">ID of the operation</see>
        ''' to fetch.</param>
        ''' <param name="parentValidator">A parent document's IChronologyValidator.</param>
        ''' <remarks>An overload for the parent documents that do not support bulk 
        ''' background and chronology data loading.</remarks>
        Friend Shared Function GetOperationDiscardChild(ByVal operationID As Integer, _
            ByVal parentValidator As IChronologicValidator) As OperationDiscard
            Return New OperationDiscard(operationID, parentValidator)
        End Function

        ''' <summary>
        ''' Gets an existing OperationDiscard instance from a database 
        ''' as a child of some parent document.
        ''' </summary>
        ''' <param name="persistence">An <see cref="OperationPersistenceObject">OperationPersistenceObject</see> 
        ''' instance that contains the operation data.</param>
        ''' <param name="parentValidator">A parent document's IChronologyValidator.</param>
        ''' <param name="generalData">A general asset data datasource for the <see cref="OperationBackground">OperationBackground</see>.
        ''' (could be null, if the parent document does not support bulk background data fetch)</param>
        ''' <param name="deltaData">An asset delta data datasource for the <see cref="OperationBackground">OperationBackground</see>.
        ''' (could be null, if the parent document does not support bulk background data fetch)</param>
        ''' <remarks></remarks>
        Friend Shared Function GetOperationDiscardChild( _
            ByVal persistence As OperationPersistenceObject, _
            ByVal parentValidator As IChronologicValidator, _
            ByVal generalData As DataTable, ByVal deltaData As DataTable) As OperationDiscard
            Return New OperationDiscard(persistence, parentValidator, generalData, deltaData)
        End Function


        ''' <summary>
        ''' Deletes an existing OperationDiscard instance from a database.
        ''' </summary>
        ''' <param name="id">An <see cref="OperationDiscard.ID">ID of the operation</see> 
        ''' to delete.</param>
        ''' <remarks></remarks>
        Public Shared Sub DeleteOperationDiscard(ByVal id As Integer)
            DataPortal.Delete(New Criteria(id))
        End Sub

        ''' <summary>
        ''' Deletes an existing OperationDiscard child instance from a database.
        ''' </summary>
        ''' <remarks>Does a delete operation on server side. Doesn't check for critical rules 
        ''' (fetch or programatical error within transaction crashes program).
        ''' Critical rules checking method <see cref="CheckIfCanDeleteChild">CheckIfCanDeleteChild</see> 
        ''' needs to be invoked before starting a transaction.</remarks>
        Friend Sub DeleteOperationDiscardChild()
            DoDelete(_ID)
        End Sub


        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal assetId As Integer, _
            ByVal parentValidator As IChronologicValidator, _
            ByVal parentIsComplexAct As Boolean)
            MarkAsChild()
            Create(assetId, parentValidator, parentIsComplexAct)
        End Sub

        Private Sub New(ByVal operationID As Integer, _
            ByVal parentValidator As IChronologicValidator)
            MarkAsChild()
            Fetch(operationID, parentValidator)
        End Sub

        Private Sub New(ByVal persistence As OperationPersistenceObject, _
            ByVal parentValidator As IChronologicValidator, _
            ByVal generalData As DataTable, ByVal deltaData As DataTable)
            MarkAsChild()
            Fetch(persistence, parentValidator, generalData, deltaData)
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> _
        Private Class Criteria
            Private mId As Integer
            Private _FetchByJournalEntryID As Boolean
            Public ReadOnly Property Id() As Integer
                Get
                    Return mId
                End Get
            End Property
            Public ReadOnly Property FetchByJournalEntryID() As Boolean
                Get
                    Return _FetchByJournalEntryID
                End Get
            End Property
            Public Sub New(ByVal id As Integer)
                mId = id
                _FetchByJournalEntryID = False
            End Sub
            Public Sub New(ByVal id As Integer, ByVal nFetchByJournalEntryID As Boolean)
                mId = id
                _FetchByJournalEntryID = nFetchByJournalEntryID
            End Sub
        End Class


        Private Overloads Sub DataPortal_Create(ByVal criteria As Criteria)
            If Not CanAddObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecurityInsertDenied)
            Create(criteria.Id, Nothing, False)
        End Sub

        Private Sub Create(ByVal nAssetId As Integer, _
            ByVal parentValidator As IChronologicValidator, _
            ByVal parentIsComplexAct As Boolean)

            _IsComplexAct = parentIsComplexAct

            _Background = OperationBackground.NewOperationBackgroundChild(nAssetId)

            _ChronologyValidator = OperationChronologicValidator.NewOperationChronologicValidator( _
                _Background, LtaOperationType.Discard, parentValidator)

            ValidationRules.CheckRules()

        End Sub


        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)

            If Not CanGetObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecuritySelectDenied)

            Dim operationID As Integer = criteria.Id

            If criteria.FetchByJournalEntryID Then
                operationID = OperationPersistenceObject.GetOperationIdByJournalEntry(criteria.Id)
            End If

            Fetch(operationID, Nothing)

        End Sub

        Private Sub Fetch(ByVal operationID As Integer, ByVal parentValidator As IChronologicValidator)

            Dim persistence As OperationPersistenceObject = _
                OperationPersistenceObject.GetOperationPersistenceObject( _
                operationID, LtaOperationType.Discard)

            Fetch(persistence, parentValidator, Nothing, Nothing)

        End Sub

        Private Sub Fetch(ByVal persistence As OperationPersistenceObject, _
            ByVal parentValidator As IChronologicValidator, ByVal generalData As DataTable, _
            ByVal deltaData As DataTable)

            _ID = persistence.ID
            _Date = persistence.OperationDate
            _JournalEntryID = persistence.JournalEntryID
            _IsComplexAct = persistence.IsComplexAct
            _Content = persistence.Content
            _AccountCosts = persistence.AccountCorresponding
            _DocumentNumber = persistence.DocumentNumber
            _AmountToDiscard = persistence.AmmountChange
            _InsertDate = persistence.InsertDate
            _UpdateDate = persistence.UpdateDate

            _Background = OperationBackground.GetOperationBackgroundChild( _
                persistence, generalData, deltaData)

            _ChronologyValidator = OperationChronologicValidator.GetOperationChronologicValidator( _
                _Background, LtaOperationType.Discard, _ID, _Date, parentValidator)

            MarkOld()

            ValidationRules.CheckRules()

        End Sub


        Protected Overrides Sub DataPortal_Insert()

            If Not CanAddObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecurityInsertDenied)

            ReloadBackgroundAndChronology(Nothing)

            If Not Me.IsValid Then
                Throw New Exception(String.Format(My.Resources.Common_ContainsErrors, vbCrLf, _
                    GetAllBrokenRules()))
            End If

            Dim entry As General.JournalEntry = GetJournalEntry()

            Using transaction As New SqlTransaction

                Try

                    entry = entry.SaveChild()
                    _JournalEntryID = entry.ID

                    DoSave(False)

                    transaction.Commit()

                Catch ex As Exception
                    transaction.SetNonSqlException(ex)
                    Throw
                End Try

            End Using

            MarkOld()

            ValidationRules.CheckRules()

        End Sub

        Protected Overrides Sub DataPortal_Update()

            If Not CanEditObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecurityUpdateDenied)

            ReloadBackgroundAndChronology(Nothing)

            If Not Me.IsValid Then
                Throw New Exception(String.Format(My.Resources.Common_ContainsErrors, vbCrLf, _
                    GetAllBrokenRules()))
            End If

            Dim entry As General.JournalEntry = GetJournalEntry()

            Using transaction As New SqlTransaction

                Try

                    entry = entry.SaveChild()

                    DoSave(False)

                    transaction.Commit()

                Catch ex As Exception
                    transaction.SetNonSqlException(ex)
                    Throw
                End Try

            End Using

            MarkOld()

            ValidationRules.CheckRules()

        End Sub

        ''' <summary>
        ''' Saves the operation as a child of some parent document.
        ''' </summary>
        ''' <param name="parentComplexActID">A parent document complex document ID
        ''' (only relevant if <see cref="IsComplexAct">IsComplexAct</see> is TRUE).</param>
        ''' <param name="parentJournalEntryID">A parent document encapsulated journal entry ID.</param>
        ''' <param name="financialDataReadOnly">Whether the parent document 
        ''' allows to change the child operation financial data.</param>
        ''' <remarks>Does a save operation on server side. Doesn't check for critical rules 
        ''' (fetch or programatical error within transaction crashes program).
        ''' Critical rules checking method <see cref="CheckIfCanSaveChild">CheckIfCanSaveChild</see> 
        ''' needs to be invoked before starting a transaction.</remarks>
        Friend Sub SaveChild(ByVal parentComplexActID As Integer, _
            ByVal parentJournalEntryID As Integer, _
            ByVal financialDataReadOnly As Boolean)

            If Not IsNew AndAlso Not IsDirty Then Exit Sub

            If IsNew Then
                _JournalEntryID = parentJournalEntryID
                _ComplexActID = parentComplexActID
            End If

            DoSave(financialDataReadOnly)

        End Sub

        Private Sub DoSave(ByVal financialDataReadOnly As Boolean)

            Dim persistence As OperationPersistenceObject = GetPersistenceObject()

            persistence = persistence.SaveChild(_ChronologyValidator.FinancialDataCanChange _
                AndAlso Not financialDataReadOnly)

            If IsNew Then
                _ID = persistence.ID
                _InsertDate = persistence.InsertDate
            End If
            _UpdateDate = persistence.UpdateDate

            _Background.MarkOld(_ID)
            _ChronologyValidator.MarkOld(_ID, _Date)

            MarkOld()

        End Sub


        Private Function GetJournalEntry() As General.JournalEntry

            Dim result As General.JournalEntry = Nothing

            If IsNew Then
                result = General.JournalEntry.NewJournalEntryChild(DocumentType.LongTermAssetDiscard)
            Else
                result = General.JournalEntry.GetJournalEntryChild(_JournalEntryID, _
                    DocumentType.LongTermAssetDiscard)
            End If

            result.Date = _Date.Date
            result.Person = Nothing
            result.Content = _Content
            result.DocNumber = _DocumentNumber

            Dim commonBookEntryList As BookEntryInternalList = GetTotalBookEntryList()

            result.DebetList.LoadBookEntryListFromInternalList(commonBookEntryList, False, False)
            result.CreditList.LoadBookEntryListFromInternalList(commonBookEntryList, False, False)

            If Not result.IsValid Then
                Throw New Exception(String.Format(My.Resources.Common_FailedToCreateJournalEntry, _
                    vbCrLf, result.ToString, vbCrLf, result.GetAllBrokenRules))
            End If

            Return result

        End Function

        Friend Function GetTotalBookEntryList() As BookEntryInternalList

            Dim result As BookEntryInternalList = _
                BookEntryInternalList.NewBookEntryInternalList(BookEntryType.Debetas)

            result.Add(BookEntryInternal.NewBookEntryInternal(BookEntryType.Kreditas, _
                _Background.CurrentAssetAcquiredAccount, _
                -_Background.ChangeAcquisitionAccountValue, Nothing))
            result.Add(BookEntryInternal.NewBookEntryInternal(BookEntryType.Debetas, _
                _Background.CurrentAssetContraryAccount, _
                -_Background.ChangeAmortizationAccountValue, Nothing))
            result.Add(BookEntryInternal.NewBookEntryInternal(BookEntryType.Debetas, _
                _Background.CurrentAssetValueDecreaseAccount, _
                -_Background.ChangeValueDecreaseAccountValue, Nothing))
            result.Add(BookEntryInternal.NewBookEntryInternal(BookEntryType.Kreditas, _
                _Background.CurrentAssetValueIncreaseAccount, _
                -_Background.ChangeValueIncreaseAccountValue, Nothing))
            result.Add(BookEntryInternal.NewBookEntryInternal(BookEntryType.Debetas, _
                _Background.CurrentAssetValueIncreaseAmortizationAccount, _
                -_Background.ChangeValueIncreaseAmortizationAccountValue, Nothing))

            result.Aggregate()

            result.Add(BookEntryInternal.NewBookEntryInternal(BookEntryType.Debetas, _
                _AccountCosts, CRound(result.GetTotalSum(BookEntryType.Kreditas) _
                - result.GetTotalSum(BookEntryType.Debetas), 2), Nothing))

            Return result

        End Function

        Private Function GetPersistenceObject() As OperationPersistenceObject

            Dim result As OperationPersistenceObject

            If IsNew Then
                result = OperationPersistenceObject.NewOperationPersistenceObject( _
                    LtaOperationType.Discard, _Background.AssetID)
                result.JournalEntryID = _JournalEntryID
                result.IsComplexAct = _IsComplexAct
                result.ComplexActID = _ComplexActID
            Else
                result = OperationPersistenceObject.GetOperationPersistenceObject( _
                    _ID, LtaOperationType.Discard)
                If result.UpdateDate <> _UpdateDate Then Throw New Exception( _
                    My.Resources.Common_UpdateDateHasChanged)
            End If

            result.OperationDate = _Date
            result.Content = _Content
            result.AccountCorresponding = _AccountCosts
            result.DocumentNumber = _DocumentNumber
            result.AmmountChange = _AmountToDiscard
            result.AcquisitionAccountChange = _Background.ChangeAcquisitionAccountValue
            result.AmortizationAccountChange = _Background.ChangeAmortizationAccountValue
            result.RevaluedPortionTotalValueChange = _Background.ChangeAssetRevaluedPortionValue
            result.TotalValueChange = _Background.ChangeAssetValue
            result.ValueDecreaseAccountChange = _Background.ChangeValueDecreaseAccountValue
            result.ValueIncreaseAccountChange = _Background.ChangeValueIncreaseAccountValue
            result.ValueIncreaseAmmortizationAccountChange = _Background.ChangeValueIncreaseAmortizationAccountValue

            Return result

        End Function


        Private Overloads Sub DataPortal_Delete(ByVal criteria As Criteria)

            If Not CanDeleteObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecurityUpdateDenied)

            Dim operationToDelete As New OperationDiscard
            operationToDelete.Fetch(criteria.Id, Nothing)

            If operationToDelete.IsComplexAct Then
                Throw New Exception(My.Resources.Assets_OperationDiscard_InvalidDeleteChild)
            End If

            If Not operationToDelete.ChronologyValidator.FinancialDataCanChange Then
                Throw New Exception(String.Format(My.Resources.Assets_OperationDiscard_InvalidDelete, _
                    operationToDelete.AssetName, vbCrLf, _
                    operationToDelete.ChronologyValidator.FinancialDataCanChangeExplanation))
            End If

            IndirectRelationInfoList.CheckIfJournalEntryCanBeDeleted( _
                operationToDelete.JournalEntryID, DocumentType.LongTermAssetDiscard)

            Using transaction As New SqlTransaction

                Try

                    General.JournalEntry.DeleteJournalEntryChild(operationToDelete.JournalEntryID)

                    DoDelete(criteria.Id)

                    transaction.Commit()

                Catch ex As Exception
                    transaction.SetNonSqlException(ex)
                    Throw
                End Try

            End Using

            MarkNew()

        End Sub

        Private Shared Sub DoDelete(ByVal nID As Integer)
            OperationPersistenceObject.DeleteChild(nID)
        End Sub


        ''' <summary>
        ''' Checks if the operation as a child of a document can be deleted.
        ''' Throws exception if not.
        ''' </summary>
        ''' <param name="parentValidator">A parent document's IChronologyValidator.</param>
        ''' <remarks>Should be invokes before the parent document update transaction begins.</remarks>
        Friend Sub CheckIfCanDeleteChild(ByVal parentValidator As IChronologicValidator)

            If IsNew Then Exit Sub

            _ChronologyValidator = OperationChronologicValidator.GetOperationChronologicValidator( _
                _Background, LtaOperationType.Discard, _
                _ID, _ChronologyValidator.CurrentOperationDate, parentValidator)

            If Not _ChronologyValidator.FinancialDataCanChange Then
                Throw New Exception(String.Format(My.Resources.Assets_OperationDiscard_InvalidDelete, _
                    _Background.AssetName, vbCrLf, _ChronologyValidator.FinancialDataCanChangeExplanation))
            End If

        End Sub

        ''' <summary>
        ''' Checks if the operation as a child of a document can be saved (inserted or updated).
        ''' Throws exception if not.
        ''' </summary>
        ''' <param name="parentValidator">A parent document's IChronologyValidator.</param>
        ''' <remarks>Should be invokes before the parent document update transaction begins.</remarks>
        Friend Sub CheckIfCanSaveChild(ByVal parentValidator As IChronologicValidator)

            ReloadBackgroundAndChronology(parentValidator)

            If Not Me.IsValid Then
                Throw New Exception(String.Format(My.Resources.Common_ContainsErrors, vbCrLf, _
                    GetErrorString()))
            End If

        End Sub

        Private Sub ReloadBackgroundAndChronology(ByVal parentValidator As IChronologicValidator)

            _Background = OperationBackground.GetOperationBackgroundChild(_Background)

            If IsNew Then
                _ChronologyValidator = OperationChronologicValidator.NewOperationChronologicValidator( _
                    _Background, LtaOperationType.Discard, parentValidator)
            Else
                _ChronologyValidator = OperationChronologicValidator.GetOperationChronologicValidator( _
                    _Background, LtaOperationType.Discard, _ID, _
                    _ChronologyValidator.CurrentOperationDate, parentValidator)
            End If

            ValidationRules.CheckRules()

        End Sub

#End Region

    End Class

End Namespace