﻿Imports Csla.Validation

Namespace Assets

    ''' <summary>
    ''' Represents a long term asset balance value change (reevaluation) operation.
    ''' </summary>
    ''' <remarks>Values are stored in the database table turtas_op.
    ''' Operation data is persisted by the <see cref="OperationPersistenceObject">OperationPersistenceObject</see>.</remarks>
    <Serializable()> _
    Public Class OperationValueChange
        Inherits BusinessBase(Of OperationValueChange)
        Implements IGetErrorForListItem, IIsDirtyEnough

#Region " Business Methods "

        ''' <summary>
        ''' Types of the (journal entry) documents that could be attached 
        ''' to the operation, i.e. could be a base for an asset acquisition value increase.
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared ReadOnly AllowedJournalEntryTypes As DocumentType() _
            = New DocumentType() {DocumentType.None}

        ''' <summary>
        ''' Types of the (journal entry) documents that act as a parent
        ''' of the operation, i.e. the operation could only be changed
        ''' within the approprate document context.
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared ReadOnly ParentJournalEntryTypes As DocumentType() _
            = New DocumentType() {}

        Private _Background As OperationBackground = Nothing
        Private _ChronologyValidator As OperationChronologicValidator2 = Nothing

        Private ReadOnly _Guid As Guid = Guid.NewGuid
        Private _ID As Integer = -1
        Private _InsertDate As DateTime = Now
        Private _UpdateDate As DateTime = Now
        Private _IsComplexAct As Boolean = False
        Private _ComplexActID As Integer = 0
        Private _Date As Date = Today.Date
        Private _Content As String = ""
        Private _DocumentNumber As String = ""
        Private _JournalEntryID As Integer = 0
        Private _JournalEntryDocumentNumber As String = ""
        Private _JournalEntryDate As Date = Today
        Private _JournalEntryContent As String = ""
        Private _JournalEntryPersonID As Integer = 0
        Private _JournalEntryPerson As String = ""
        Private _JournalEntryAmount As Double = 0
        Private _JournalEntryBookEntries As String = ""
        Private _JournalEntryDocumentType As DocumentType = DocumentType.None
        Private _ValueChangeTotal As Double = 0
        Private _ValueChangePerUnit As Double = 0


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
        ''' Gets a type of the long term asset operation, i.e. <see cref="LtaOperationType.ValueChange">LtaOperationType.ValueChange</see>.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property [Type]() As LtaOperationType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return LtaOperationType.ValueChange
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
        ''' is used to validate a long term asset balance value change operation chronological business rules.</remarks>
        Public ReadOnly Property ChronologyValidator() As OperationChronologicValidator2
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
        ''' A change of balance for the <see cref="CurrentAssetValueDecreaseAccount">CurrentAssetValueDecreaseAccount</see> per unit made by the operation.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit balance.
        ''' A proxy to the <see cref="Background">Background</see>
        ''' to be used when databinding to a datagridview, because
        ''' datagridview does not support binding to the incapsulated object properties.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property ChangeValueDecreaseAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_Background.ChangeValueDecreaseAccountValuePerUnit, ROUNDUNITASSET)
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
        ''' A change of balance for the <see cref="CurrentAssetValueIncreaseAccount">CurrentAssetValueIncreaseAccount</see> per unit made by the operation.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.
        ''' A proxy to the <see cref="Background">Background</see>
        ''' to be used when databinding to a datagridview, because
        ''' datagridview does not support binding to the incapsulated object properties.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property ChangeValueIncreaseAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_Background.ChangeValueIncreaseAccountValuePerUnit, ROUNDUNITASSET)
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
        ''' A change of the unit value of the long term asset made by the operation.
        ''' </summary>
        ''' <remarks>A proxy to the <see cref="Background">Background</see>
        ''' to be used when databinding to a datagridview, because
        ''' datagridview does not support binding to the incapsulated object properties.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property ChangeAssetUnitValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_Background.ChangeAssetUnitValue, ROUNDUNITASSET)
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

        ''' <summary>
        ''' A change of the total value of the revalued portion of the long term asset.
        ''' </summary>
        ''' <remarks>A proxy to the <see cref="Background">Background</see>
        ''' to be used when databinding to a datagridview, because
        ''' datagridview does not support binding to the incapsulated object properties.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property ChangeAssetRevaluedPortionUnitValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_Background.ChangeAssetRevaluedPortionUnitValue, ROUNDUNITASSET)
            End Get
        End Property

#End Region

#Region " State After The Operation "

        ''' <summary>
        ''' A balance for the <see cref="CurrentAssetValueDecreaseAccount">CurrentAssetValueDecreaseAccount</see> after the operation.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit balance.
        ''' A proxy to the <see cref="Background">Background</see>
        ''' to be used when databinding to a datagridview, because
        ''' datagridview does not support binding to the incapsulated object properties.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property AfterOperationValueDecreaseAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Background.AfterOperationValueDecreaseAccountValue
            End Get
        End Property

        ''' <summary>
        ''' A balance for the <see cref="CurrentAssetValueDecreaseAccount">CurrentAssetValueDecreaseAccount</see> 
        ''' per asset unit after the operation.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit balance.
        ''' A proxy to the <see cref="Background">Background</see>
        ''' to be used when databinding to a datagridview, because
        ''' datagridview does not support binding to the incapsulated object properties.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property AfterOperationValueDecreaseAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Background.AfterOperationValueDecreaseAccountValuePerUnit
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
        ''' A balance for the <see cref="CurrentAssetValueIncreaseAccount">CurrentAssetValueIncreaseAccount</see> 
        ''' per asset unit after the operation.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.
        ''' A proxy to the <see cref="Background">Background</see>
        ''' to be used when databinding to a datagridview, because
        ''' datagridview does not support binding to the incapsulated object properties.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, ROUNDUNITASSET)> _
        Public ReadOnly Property AfterOperationValueIncreaseAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Background.AfterOperationValueIncreaseAccountValuePerUnit
            End Get
        End Property

        ''' <summary>
        ''' A balance for the <see cref="CurrentAssetValueIncreaseAmortizationAccount">CurrentAssetValueIncreaseAmortizationAccount</see> after the operation.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit balance.
        ''' A proxy to the <see cref="Background">Background</see>
        ''' to be used when databinding to a datagridview, because
        ''' datagridview does not support binding to the incapsulated object properties.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property AfterOperationValueIncreaseAmortizationAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Background.AfterOperationValueIncreaseAmortizationAccountValue
            End Get
        End Property

        ''' <summary>
        ''' A balance for the <see cref="CurrentAssetValueIncreaseAmortizationAccount">CurrentAssetValueIncreaseAmortizationAccount</see> 
        ''' per asset unit after the operation.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit balance.
        ''' A proxy to the <see cref="Background">Background</see>
        ''' to be used when databinding to a datagridview, because
        ''' datagridview does not support binding to the incapsulated object properties.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property AfterOperationValueIncreaseAmortizationAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Background.AfterOperationValueIncreaseAmortizationAccountValuePerUnit
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
        ''' Gets a number of the long term asset operation document
        ''' that should be the same as the document number of the associated journal entry.
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas_op.ActNumber.</remarks>
        <StringField(ValueRequiredLevel.Mandatory, 30)> _
        Public ReadOnly Property DocumentNumber() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DocumentNumber
            End Get
        End Property

        ''' <summary>
        ''' Gets an <see cref="General.JournalEntry.ID">ID of the journal entry</see> 
        ''' that is attached to the long term asset acquisition value increase operation.
        ''' </summary>
        ''' <remarks>The operation does not handle journal entry. It should be
        ''' handled by other (parent) object or by a user manualy.
        ''' Value is stored in the database field turtas_op.JE_ID.</remarks>
        Public ReadOnly Property JournalEntryID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryID
            End Get
        End Property

        ''' <summary>
        ''' Gets a document number of the journal entry that is attached to the long term asset operation.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="general.JournalEntry.DocNumber">JournalEntry.DocNumber</see>.</remarks>
        Public ReadOnly Property JournalEntryDocumentNumber() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryDocumentNumber.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a date of the journal entry that is attached to the long term asset operation.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="general.JournalEntry.Date">JournalEntry.Date</see>.</remarks>
        Public ReadOnly Property JournalEntryDate() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryDate
            End Get
        End Property

        ''' <summary>
        ''' Gets a content of the journal entry that is attached to the long term asset operation.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="general.JournalEntry.Content">JournalEntry.Content</see>.</remarks>
        Public ReadOnly Property JournalEntryContent() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryContent.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets an ID of the person in the journal entry that is attached to the long term asset operation.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="general.JournalEntry.Person">JournalEntry.Person</see>.</remarks>
        Public ReadOnly Property JournalEntryPersonID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryPersonID
            End Get
        End Property

        ''' <summary>
        ''' Gets a name of the person in the journal entry that is attached to the long term asset operation.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="general.JournalEntry.Person">JournalEntry.Person</see>.</remarks>
        Public ReadOnly Property JournalEntryPerson() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryPerson.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a total book entries' amount of the journal entry that is attached 
        ''' to the long term asset operation.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="general.JournalEntry.DebetSum">JournalEntry.DebetSum</see>.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property JournalEntryAmount() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_JournalEntryAmount)
            End Get
        End Property

        ''' <summary>
        ''' Gets a comma separated list of book entries in the journal entry 
        ''' that is attached to the long term asset operation.
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
        ''' Gets a human readable (localized) type of the document that owns the journal entry 
        ''' that is attached to the long term asset operation.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="general.JournalEntry.DocType">JournalEntry.DocType</see>.</remarks>
        Public ReadOnly Property JournalEntryDocumentType() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return ConvertEnumHumanReadable(_JournalEntryDocumentType)
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets a total long term asset balance value change.
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas_op.AmortizationCalculatedForMonths.</remarks>
        <DoubleField(ValueRequiredLevel.Mandatory, True, 2)> _
        Public Property ValueChangeTotal() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ValueChangeTotal, 2)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Double)
                CanWriteProperty(True)
                If ValueChangeTotalIsReadOnly Then Exit Property
                If CRound(_ValueChangeTotal, 2) <> CRound(value, 2) Then
                    _ValueChangeTotal = CRound(value, 2)
                    PropertyHasChanged()
                    Recalculate(True)
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets a long term asset balance value change per unit.
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas_op.AmortizationCalculatedForMonths.</remarks>
        <DoubleField(ValueRequiredLevel.Mandatory, True, ROUNDUNITASSET)> _
        Public ReadOnly Property ValueChangePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ValueChangePerUnit, ROUNDUNITASSET)
            End Get
        End Property


        ''' <summary>
        ''' Whether the <see cref="Date">Date</see> property is readonly.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DateIsReadOnly() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return (IsChild OrElse _IsComplexAct OrElse _
                    Not Array.IndexOf(ParentJournalEntryTypes, _JournalEntryDocumentType) < 0)
            End Get
        End Property

        ''' <summary>
        ''' Whether the <see cref="Content">Content</see> property is readonly.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property ContentIsReadOnly() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return (IsChild OrElse _IsComplexAct OrElse _
                    Not Array.IndexOf(ParentJournalEntryTypes, _JournalEntryDocumentType) < 0)
            End Get
        End Property

        ''' <summary>
        ''' Whether the <see cref="ValueChangeTotal">ValueChangeTotal</see> property is readonly.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property ValueChangeTotalIsReadOnly() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If Not _ChronologyValidator.FinancialDataCanChange OrElse _
                    Not _ChronologyValidator.ParentFinancialDataCanChange Then Return True
                Return (Not IsChild AndAlso (_IsComplexAct OrElse _
                    Not Array.IndexOf(ParentJournalEntryTypes, _JournalEntryDocumentType) < 0))
            End Get
        End Property

        ''' <summary>
        ''' Whether the attached journal entry could not be changed.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property AssociatedJournalEntryIsReadOnly() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return (IsChild OrElse _IsComplexAct OrElse _
                    Not Array.IndexOf(ParentJournalEntryTypes, _JournalEntryDocumentType) < 0)
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
                Return (Not StringIsNullOrEmpty(_Content) _
                    OrElse CRound(_ValueChangeTotal) > 0)
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
                End If

                Recalculate(True)

            End If

        End Sub



        Private Sub Recalculate(ByVal raisePropertyChanged As Boolean)

            SetBackgroundValues(False)

            _Background.CalculateAfterOperationProperties()

            If raisePropertyChanged Then
                PropertyHasChanged("Background")
                PropertyHasChanged("ValueChangePerUnit")
                ' proxy properties
                If IsChild Then
                    PropertyHasChanged("ChangeValueDecreaseAccountValue")
                    PropertyHasChanged("ChangeValueDecreaseAccountValuePerUnit")
                    PropertyHasChanged("ChangeValueIncreaseAccountValue")
                    PropertyHasChanged("ChangeValueIncreaseAccountValuePerUnit")
                    PropertyHasChanged("ChangeAssetValue")
                    PropertyHasChanged("ChangeAssetUnitValue")
                    PropertyHasChanged("ChangeAssetRevaluedPortionValue")
                    PropertyHasChanged("ChangeAssetRevaluedPortionUnitValue")
                    PropertyHasChanged("AfterOperationValueDecreaseAccountValue")
                    PropertyHasChanged("AfterOperationValueDecreaseAccountValuePerUnit")
                    PropertyHasChanged("AfterOperationValueIncreaseAccountValue")
                    PropertyHasChanged("AfterOperationValueIncreaseAccountValuePerUnit")
                    PropertyHasChanged("AfterOperationValueIncreaseAmortizationAccountValue")
                    PropertyHasChanged("AfterOperationValueIncreaseAmortizationAccountValuePerUnit")
                    PropertyHasChanged("AfterOperationAssetValue")
                    PropertyHasChanged("AfterOperationAssetValuePerUnit")
                    PropertyHasChanged("AfterOperationAssetValueRevaluedPortion")
                    PropertyHasChanged("AfterOperationAssetValueRevaluedPortionPerUnit")
                End If
            End If

        End Sub

        Private Sub SetBackgroundValues(ByVal initialize As Boolean)

            If _Background.CurrentAssetAmount < 1 AndAlso Not initialize Then Exit Sub

            If Not initialize Then
                _ValueChangePerUnit = CRound(_ValueChangeTotal _
                    / _Background.CurrentAssetAmount, ROUNDUNITASSET)
            End If

            _Background.DisableCalculations = True


            If CRound(_Background.CurrentValueIncreaseAccountValue _
                - _Background.CurrentValueIncreaseAmortizationAccountValue _
                - _Background.CurrentValueDecreaseAccountValue + _ValueChangeTotal) >= 0 Then

                _Background.ChangeValueIncreaseAccountValue = _
                    CRound(_ValueChangeTotal - _Background.CurrentValueDecreaseAccountValue, 2)
                _Background.ChangeValueIncreaseAccountValuePerUnit = _
                    CRound(_ValueChangePerUnit - _Background.CurrentValueDecreaseAccountValuePerUnit, ROUNDUNITASSET)

                _Background.ChangeValueDecreaseAccountValue = _
                    -_Background.CurrentValueDecreaseAccountValue
                _Background.ChangeValueDecreaseAccountValuePerUnit = _
                    -_Background.CurrentValueDecreaseAccountValuePerUnit

            Else

                _Background.ChangeValueDecreaseAccountValue = _
                    CRound(-_ValueChangeTotal - _Background.CurrentValueIncreaseAccountValue _
                    + _Background.CurrentValueIncreaseAmortizationAccountValue, 2)
                _Background.ChangeValueDecreaseAccountValuePerUnit = _
                    CRound(-_ValueChangePerUnit - _Background.CurrentValueIncreaseAccountValuePerUnit _
                    + _Background.CurrentValueIncreaseAmortizationAccountValuePerUnit, ROUNDUNITASSET)

                _Background.ChangeValueIncreaseAccountValue = _
                    CRound(_Background.CurrentValueIncreaseAccountValue _
                    - _Background.CurrentValueIncreaseAmortizationAccountValue, 2)
                _Background.ChangeValueIncreaseAccountValuePerUnit = _
                    CRound(_Background.CurrentValueIncreaseAccountValuePerUnit _
                    - _Background.CurrentValueIncreaseAmortizationAccountValuePerUnit, ROUNDUNITASSET)

            End If


            If initialize Then _Background.InitializeOldData(_Date)

            _Background.DisableCalculations = False

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
            If Not MyBase.BrokenRulesCollection.WarningCount > 0 Then
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
            If BrokenRulesCollection.WarningCount < 1 Then Return ""
            Return String.Format(My.Resources.Common_WarningInItem, Me.ToString, _
                vbCrLf, Me.GetAllWarnings())
        End Function


        ''' <summary>
        ''' Attaches a journal entry to the operation.
        ''' </summary>
        ''' <param name="entry">A journal entry info.</param>
        ''' <remarks>The operation does not handle journal entry. It should be
        ''' handled by other (parent) object or by a user manualy.</remarks>
        Public Sub LoadAssociatedJournalEntry(ByVal entry As ActiveReports.JournalEntryInfo)

            If AssociatedJournalEntryIsReadOnly Then Exit Sub

            If entry Is Nothing OrElse Not entry.Id > 0 Then Exit Sub

            If Not Array.IndexOf(ParentJournalEntryTypes, entry.DocType) < 0 Then
                Throw New Exception(String.Format(My.Resources.Assets_OperationValueChange_CannotAttachParentType, _
                    entry.DocTypeHumanReadable))
            ElseIf Array.IndexOf(AllowedJournalEntryTypes, entry.DocType) < 0 Then
                Throw New Exception(String.Format(My.Resources.Assets_OperationValueChange_InvalidJournalEntryType, _
                    entry.DocTypeHumanReadable))
            End If

            _JournalEntryID = entry.Id
            _JournalEntryDate = entry.Date
            _JournalEntryDocumentNumber = entry.DocNumber
            _JournalEntryContent = entry.Content
            _JournalEntryBookEntries = entry.BookEntries
            _JournalEntryPersonID = entry.PersonID
            _JournalEntryPerson = entry.Person
            _JournalEntryDocumentType = entry.DocType
            _JournalEntryAmount = entry.Ammount

            _DocumentNumber = entry.DocNumber

            PropertyHasChanged("JournalEntryID")
            PropertyHasChanged("JournalEntryDate")
            PropertyHasChanged("JournalEntryContent")
            PropertyHasChanged("JournalEntryDocumentNumber")
            PropertyHasChanged("JournalEntryBookEntries")
            PropertyHasChanged("JournalEntryPersonID")
            PropertyHasChanged("JournalEntryPersonName")
            PropertyHasChanged("JournalEntryDocumentType")
            PropertyHasChanged("JournalEntryAmount")
            PropertyHasChanged("DocumentNumber")

        End Sub

        ''' <summary>
        ''' Gets a new <see cref="General.JournalEntry">journal entry</see>
        ''' that contains all the book entries requered for balance value change by the
        ''' long term asset state.
        ''' </summary>
        ''' <remarks>Could be used as a helper method when registering balance change operations.</remarks>
        Public Function NewJournalEntry() As General.JournalEntry

            Dim result As General.JournalEntry = General.JournalEntry.NewJournalEntry()

            result.Date = _Date.Date
            result.Person = Nothing
            result.Content = _Content
            result.DocNumber = _DocumentNumber

            Dim commonBookEntryList As BookEntryInternalList = GetTotalBookEntryList()

            If commonBookEntryList.GetTotalSum(BookEntryType.Debetas) _
                > commonBookEntryList.GetTotalSum(BookEntryType.Kreditas) Then
                commonBookEntryList.Add(BookEntryInternal.NewBookEntryInternal(BookEntryType.Kreditas, _
                    0, commonBookEntryList.GetTotalSum(BookEntryType.Debetas) _
                    - commonBookEntryList.GetTotalSum(BookEntryType.Kreditas), Nothing))
            Else
                commonBookEntryList.Add(BookEntryInternal.NewBookEntryInternal(BookEntryType.Debetas, _
                    0, commonBookEntryList.GetTotalSum(BookEntryType.Kreditas) _
                    - commonBookEntryList.GetTotalSum(BookEntryType.Debetas), Nothing))
            End If

            result.DebetList.LoadBookEntryListFromInternalList(commonBookEntryList, False, False)
            result.CreditList.LoadBookEntryListFromInternalList(commonBookEntryList, False, False)

            Return result

        End Function


        Public Overrides Function Save() As OperationValueChange

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
                Return String.Format(My.Resources.Assets_OperationValueChange_ToStringChild, _
                _Background.AssetName, _ID.ToString())
            Else
                Return String.Format(My.Resources.Assets_OperationValueChange_ToString, _
                    _Date.ToString("yyyy-MM-dd"), _Background.AssetName, _DocumentNumber, _
                    _ID.ToString())
            End If
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()

            ValidationRules.AddRule(AddressOf CommonValidation.DoubleFieldValidation, _
                "ValueChangeTotal")

            ValidationRules.AddRule(AddressOf ChildStringPropertyValidation, _
                New Csla.Validation.RuleArgs("Content"))
            ValidationRules.AddRule(AddressOf JournalEntryIDValidation, _
                New Csla.Validation.RuleArgs("JournalEntryID"))
            ValidationRules.AddRule(AddressOf DateValidation, _
                New CommonValidation.ChronologyRuleArgs("Date", "ChronologyValidator"))

            ValidationRules.AddDependantProperty("ChronologyValidator", "Date", False)
            ValidationRules.AddDependantProperty("JournalEntryDate", "Date", False)

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

            If DirectCast(target, OperationValueChange).IsChild Then
                Return True
            Else
                Return CommonValidation.StringFieldValidation(target, e)
            End If

        End Function

        ''' <summary>
        ''' Rule ensuring that a journal entry is attached if the operation is not a child.
        ''' </summary>
        ''' <param name="target">Object containing the data to validate</param>
        ''' <param name="e">Arguments parameter specifying the name of the string
        ''' property to validate</param>
        ''' <returns><see langword="false" /> if the rule is broken</returns>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
        Private Shared Function JournalEntryIDValidation(ByVal target As Object, _
          ByVal e As Validation.RuleArgs) As Boolean

            If Not DirectCast(target, OperationValueChange).IsChild AndAlso _
                Not DirectCast(target, OperationValueChange)._JournalEntryID > 0 Then
                e.Description = My.Resources.Assets_OperationValueChange_JournalEntryNull
                e.Severity = RuleSeverity.Error
                Return False
            End If

            Return True

        End Function

        ''' <summary>
        ''' Rule ensuring that the operation date is valid.
        ''' </summary>
        ''' <param name="target">Object containing the data to validate</param>
        ''' <param name="e">Arguments parameter specifying the name of the string
        ''' property to validate</param>
        ''' <returns><see langword="false" /> if the rule is broken</returns>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
        Private Shared Function DateValidation(ByVal target As Object, _
          ByVal e As Validation.RuleArgs) As Boolean

            Dim valObj As OperationValueChange = DirectCast(target, OperationValueChange)

            If valObj._Background.CurrentAssetAmount < 1 Then
                e.Description = My.Resources.Assets_OperationValueChange_CurrentAmountNull
                e.Severity = RuleSeverity.Error
                Return False
            ElseIf Not valObj.IsChild AndAlso valObj._JournalEntryID > 0 AndAlso _
                valObj._Date.Date <> valObj._JournalEntryDate.Date Then
                e.Description = My.Resources.Assets_OperationValueChange_DateInvalid
                e.Severity = RuleSeverity.Error
                Return False
            End If

            Return CommonValidation.ChronologyValidation(target, e)

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
        ''' Gets a new OperationValueChange instance as a parent (standalone) document.
        ''' </summary>
        ''' <param name="assetId">An ID of the asset that the operation operates on.</param>
        ''' <remarks></remarks>
        Public Shared Function NewOperationValueChange(ByVal assetId As Integer) As OperationValueChange
            Return DataPortal.Create(Of OperationValueChange)(New Criteria(assetId))
        End Function

        ''' <summary>
        ''' Gets a new OperationValueChange instance as a child of some parent document.
        ''' </summary>
        ''' <param name="assetId">An ID of the asset that the operation operates on.</param>
        ''' <param name="parentValidator">A parent document's IChronologyValidator.</param>
        ''' <param name="parentIsComplexAct">Whether the parent document is a complex 
        ''' long term asset operation.</param>
        ''' <remarks></remarks>
        Friend Shared Function NewOperationValueChangeChild(ByVal assetId As Integer, _
            ByVal parentValidator As IChronologicValidator, _
            ByVal parentIsComplexAct As Boolean) As OperationValueChange
            Return New OperationValueChange(assetId, parentValidator, parentIsComplexAct)
        End Function


        ''' <summary>
        ''' Gets an existing OperationValueChange instance from a database 
        ''' as a parent (standalone) document.
        ''' </summary>
        ''' <param name="id">An <see cref="OperationValueChange.ID">ID of the operation</see> 
        ''' or an <see cref="OperationValueChange.JournalEntryID">ID the journal entry</see>  
        ''' that is attached to the operation.</param>
        ''' <param name="nFetchByJournalEntryID">Whether the <paramref name="id">id</paramref>
        ''' param is an <see cref="OperationValueChange.JournalEntryID">ID the journal entry</see> 
        ''' that is attached to the operation.</param>
        ''' <remarks></remarks>
        Public Shared Function GetOperationValueChange(ByVal id As Integer, _
            ByVal nFetchByJournalEntryID As Boolean) As OperationValueChange
            Return DataPortal.Fetch(Of OperationValueChange) _
                (New Criteria(id, nFetchByJournalEntryID))
        End Function

        ''' <summary>
        ''' Gets an existing OperationValueChange instance from a database 
        ''' as a child of some parent document.
        ''' </summary>
        ''' <param name="operationID">An <see cref="OperationValueChange.ID">ID of the operation</see>
        ''' to fetch.</param>
        ''' <param name="parentValidator">A parent document's IChronologyValidator.</param>
        ''' <remarks>An overload for the parent documents that do not support bulk 
        ''' background and chronology data loading.</remarks>
        Friend Shared Function GetOperationValueChangeChild(ByVal operationID As Integer, _
            ByVal parentValidator As IChronologicValidator) As OperationValueChange
            Return New OperationValueChange(operationID, parentValidator)
        End Function

        ''' <summary>
        ''' Gets an existing OperationValueChange instance from a database 
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
        Friend Shared Function GetOperationValueChangeChild( _
            ByVal persistence As OperationPersistenceObject, _
            ByVal parentValidator As IChronologicValidator, _
            ByVal generalData As DataTable, ByVal deltaData As DataTable) As OperationValueChange
            Return New OperationValueChange(persistence, parentValidator, generalData, deltaData)
        End Function


        ''' <summary>
        ''' Deletes an existing OperationValueChange instance from a database.
        ''' </summary>
        ''' <param name="id">An <see cref="OperationValueChange.ID">ID of the operation</see> 
        ''' to delete.</param>
        ''' <remarks></remarks>
        Public Shared Sub DeleteOperationValueChange(ByVal id As Integer)
            DataPortal.Delete(New Criteria(id))
        End Sub

        ''' <summary>
        ''' Deletes an existing OperationValueChange child instance from a database.
        ''' </summary>
        ''' <remarks>Does a delete operation on server side. Doesn't check for critical rules 
        ''' (fetch or programatical error within transaction crashes program).
        ''' Critical rules checking method <see cref="CheckIfCanDeleteChild">CheckIfCanDeleteChild</see> 
        ''' needs to be invoked before starting a transaction.</remarks>
        Friend Sub DeleteOperationValueChangeChild()
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

            _ChronologyValidator = OperationChronologicValidator2.NewOperationChronologicValidator( _
                _Background, LtaOperationType.ValueChange, parentValidator)

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
                operationID, LtaOperationType.ValueChange)

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
            _DocumentNumber = persistence.JournalEntryDocumentNumber
            _ValueChangeTotal = persistence.TotalValueChange
            _ValueChangePerUnit = persistence.UnitValueChange
            _JournalEntryID = persistence.JournalEntryID
            _JournalEntryDate = persistence.JournalEntryDate
            _JournalEntryDocumentNumber = persistence.JournalEntryDocumentNumber
            _JournalEntryContent = persistence.JournalEntryContent
            _JournalEntryBookEntries = persistence.JournalEntryBookEntries
            _JournalEntryPersonID = persistence.JournalEntryPersonID
            _JournalEntryPerson = persistence.JournalEntryPerson
            _JournalEntryDocumentType = persistence.JournalEntryDocumentType
            _JournalEntryAmount = persistence.JournalEntryAmount
            _InsertDate = persistence.InsertDate
            _UpdateDate = persistence.UpdateDate

            _Background = OperationBackground.GetOperationBackgroundChild( _
                persistence.AssetID, _ID, _Date, generalData, deltaData)

            SetBackgroundValues(True)

            _ChronologyValidator = OperationChronologicValidator2.GetOperationChronologicValidator( _
                _Background, LtaOperationType.ValueChange, _ID, _Date, parentValidator)

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

            Using transaction As New SqlTransaction

                Try

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

            Using transaction As New SqlTransaction

                Try

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

            MarkOld()

        End Sub


        Friend Function GetTotalBookEntryList() As BookEntryInternalList

            Dim result As BookEntryInternalList = _
                BookEntryInternalList.NewBookEntryInternalList(BookEntryType.Debetas)

            If _Background.ChangeValueIncreaseAccountValue > 0 Then
                result.Add(BookEntryInternal.NewBookEntryInternal(BookEntryType.Debetas, _
                    _Background.CurrentAssetValueIncreaseAccount, _
                    _Background.ChangeValueIncreaseAccountValue, Nothing))
            Else
                result.Add(BookEntryInternal.NewBookEntryInternal(BookEntryType.Kreditas, _
                    _Background.CurrentAssetValueIncreaseAccount, _
                    -_Background.ChangeValueIncreaseAccountValue, Nothing))
            End If

            If _Background.ChangeValueDecreaseAccountValue > 0 Then
                result.Add(BookEntryInternal.NewBookEntryInternal(BookEntryType.Kreditas, _
                    _Background.CurrentAssetValueDecreaseAccount, _
                    _Background.ChangeValueDecreaseAccountValue, Nothing))
            Else
                result.Add(BookEntryInternal.NewBookEntryInternal(BookEntryType.Debetas, _
                    _Background.CurrentAssetValueDecreaseAccount, _
                    -_Background.ChangeValueDecreaseAccountValue, Nothing))
            End If

            Return result

        End Function

        Private Function GetPersistenceObject() As OperationPersistenceObject

            Dim result As OperationPersistenceObject

            If IsNew Then
                result = OperationPersistenceObject.NewOperationPersistenceObject( _
                    LtaOperationType.ValueChange, _Background.AssetID)
                result.IsComplexAct = _IsComplexAct
                result.ComplexActID = _ComplexActID
            Else
                result = OperationPersistenceObject.GetOperationPersistenceObject( _
                    _ID, LtaOperationType.ValueChange)
                If result.UpdateDate <> _UpdateDate Then Throw New Exception( _
                    My.Resources.Common_UpdateDateHasChanged)
            End If

            result.OperationDate = _Date
            result.Content = _Content
            result.JournalEntryID = _JournalEntryID
            result.ValueIncreaseAccountChange = _Background.ChangeValueIncreaseAccountValue
            result.ValueIncreaseAccountChangePerUnit = _Background.ChangeValueIncreaseAccountValuePerUnit
            result.ValueDecreaseAccountChange = _Background.ChangeValueDecreaseAccountValue
            result.ValueDecreaseAccountChangePerUnit = _Background.ChangeValueDecreaseAccountValuePerUnit
            result.TotalValueChange = _ValueChangeTotal
            result.UnitValueChange = _ValueChangePerUnit
            result.RevaluedPortionUnitValueChange = _ValueChangePerUnit
            result.RevaluedPortionTotalValueChange = _ValueChangeTotal

            Return result

        End Function


        Private Overloads Sub DataPortal_Delete(ByVal criteria As Criteria)

            If Not CanDeleteObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecurityUpdateDenied)

            Dim operationToDelete As New OperationValueChange
            operationToDelete.Fetch(criteria.Id, Nothing)

            If operationToDelete.IsComplexAct Then
                Throw New Exception(My.Resources.Assets_OperationValueChange_InvalidDeleteComplexDocumentChild)
            ElseIf Not Array.IndexOf(ParentJournalEntryTypes, operationToDelete._JournalEntryDocumentType) < 0 Then
                Throw New Exception(String.Format(My.Resources.Assets_OperationValueChange_InvalidDeleteChild, _
                    operationToDelete.JournalEntryDocumentType))
            End If

            If Not operationToDelete.ChronologyValidator.FinancialDataCanChange Then
                Throw New Exception(String.Format(My.Resources.Assets_OperationValueChange_InvalidDelete, _
                    operationToDelete.AssetName, vbCrLf, _
                    operationToDelete.ChronologyValidator.FinancialDataCanChangeExplanation))
            End If

            Using transaction As New SqlTransaction

                Try

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

            _ChronologyValidator = OperationChronologicValidator2.GetOperationChronologicValidator( _
                _Background, LtaOperationType.ValueChange, _
                _ID, _ChronologyValidator.CurrentOperationDate, parentValidator)

            If Not _ChronologyValidator.FinancialDataCanChange Then
                Throw New Exception(String.Format(My.Resources.Assets_OperationValueChange_InvalidDelete, _
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

            If IsNew Then
                _Background = OperationBackground.NewOperationBackgroundChild(_Background.AssetID)
                _ChronologyValidator = OperationChronologicValidator2.NewOperationChronologicValidator( _
                    _Background, LtaOperationType.ValueChange, parentValidator)
            Else
                _Background = OperationBackground.GetOperationBackgroundChild( _
                    _Background.AssetID, _ID, _Date)
                _ChronologyValidator = OperationChronologicValidator2.GetOperationChronologicValidator( _
                    _Background, LtaOperationType.ValueChange, _ID, _Date, parentValidator)
            End If

            SetBackgroundValues(True)

            ValidationRules.CheckRules()

        End Sub

#End Region

    End Class

End Namespace