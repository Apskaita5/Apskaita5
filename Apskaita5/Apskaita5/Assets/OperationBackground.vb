﻿Namespace Assets

    ''' <summary>
    ''' Represents a helper object that contains information about a long term asset state 
    ''' before and after an operation (aggregated information).
    ''' </summary>
    ''' <remarks>Should only be used as a child of a long term asset operation.</remarks>
    <Serializable()> _
    Public Class OperationBackground
        Inherits BusinessBase(Of OperationBackground)

#Region " Business Methods "

        Private Const UnitRoundTolerance As Double = 1.0

#Region " Private Backing Fields "

        Private _DisableCalculations As Boolean = False
        Private _AssetID As Integer = 0
        Private _AssetName As String = ""
        Private _AssetMeasureUnit As String = ""
        Private _AssetDateAcquired As Date = Today
        Private _AssetAquisitionID As Integer = 0
        Private _AssetLiquidationValue As Double = 0

        Private _DeltaList As OperationDeltaList = Nothing

        Private _InitialAssetAcquiredAccount As Long = 0
        Private _InitialAssetContraryAccount As Long = 0
        Private _InitialAssetValueDecreaseAccount As Long = 0
        Private _InitialAssetValueIncreaseAccount As Long = 0
        Private _InitialAssetValueIncreaseAmortizationAccount As Long = 0
        Private _InitialAcquisitionAccountValue As Double = 0
        Private _InitialAcquisitionAccountValuePerUnit As Double = 0
        Private _InitialAmortizationAccountValue As Double = 0
        Private _InitialAmortizationAccountValuePerUnit As Double = 0
        Private _InitialValueDecreaseAccountValue As Double = 0
        Private _InitialValueDecreaseAccountValuePerUnit As Double = 0
        Private _InitialValueIncreaseAccountValue As Double = 0
        Private _InitialValueIncreaseAccountValuePerUnit As Double = 0
        Private _InitialValueIncreaseAmortizationAccountValue As Double = 0
        Private _InitialValueIncreaseAmortizationAccountValuePerUnit As Double = 0
        Private _InitialAssetAmount As Integer = 0
        Private _InitialAssetValue As Double = 0
        Private _InitialAssetValuePerUnit As Double = 0
        Private _InitialAssetValueRevaluedPortion As Double = 0
        Private _InitialAssetValueRevaluedPortionPerUnit As Double = 0
        Private _InitialUsageTermMonths As Integer = 0
        Private _InitialAmortizationPeriod As Integer = 0
        Private _InitialUsageStatus As Boolean = False

        Private _CurrentDate As Date = Today

        Private _CurrentAssetAcquiredAccount As Long = 0
        Private _CurrentAssetContraryAccount As Long = 0
        Private _CurrentAssetValueDecreaseAccount As Long = 0
        Private _CurrentAssetValueIncreaseAccount As Long = 0
        Private _CurrentAssetValueIncreaseAmortizationAccount As Long = 0
        Private _CurrentAcquisitionAccountValue As Double = 0
        Private _CurrentAcquisitionAccountValuePerUnit As Double = 0
        Private _CurrentAmortizationAccountValue As Double = 0
        Private _CurrentAmortizationAccountValuePerUnit As Double = 0
        Private _CurrentValueDecreaseAccountValue As Double = 0
        Private _CurrentValueDecreaseAccountValuePerUnit As Double = 0
        Private _CurrentValueIncreaseAccountValue As Double = 0
        Private _CurrentValueIncreaseAccountValuePerUnit As Double = 0
        Private _CurrentValueIncreaseAmortizationAccountValue As Double = 0
        Private _CurrentValueIncreaseAmortizationAccountValuePerUnit As Double = 0
        Private _CurrentAssetAmount As Integer = 0
        Private _CurrentAssetValue As Double = 0
        Private _CurrentAssetValuePerUnit As Double = 0
        Private _CurrentAssetValueRevaluedPortion As Double = 0
        Private _CurrentAssetValueRevaluedPortionPerUnit As Double = 0
        Private _CurrentUsageTermMonths As Integer = 0
        Private _CurrentAmortizationPeriod As Integer = 0
        Private _CurrentUsageStatus As Boolean = False

        Private _ChangeAssetAmount As Integer = 0
        Private _ChangeAcquisitionAccountValue As Double = 0
        Private _ChangeAcquisitionAccountValuePerUnit As Double = 0
        Private _ChangeAmortizationAccountValue As Double = 0
        Private _ChangeAmortizationAccountValuePerUnit As Double = 0
        Private _ChangeValueDecreaseAccountValue As Double = 0
        Private _ChangeValueDecreaseAccountValuePerUnit As Double = 0
        Private _ChangeValueIncreaseAccountValue As Double = 0
        Private _ChangeValueIncreaseAccountValuePerUnit As Double = 0
        Private _ChangeValueIncreaseAmortizationAccountValue As Double = 0
        Private _ChangeValueIncreaseAmortizationAccountValuePerUnit As Double = 0
        Private _ChangeAssetUnitValue As Double = 0
        Private _ChangeAssetValue As Double = 0
        Private _ChangeAssetRevaluedPortionUnitValue As Double = 0
        Private _ChangeAssetRevaluedPortionValue As Double = 0

        Private _AfterOperationAcquisitionAccountValue As Double = 0
        Private _AfterOperationAcquisitionAccountValuePerUnit As Double = 0
        Private _AfterOperationAmortizationAccountValue As Double = 0
        Private _AfterOperationAmortizationAccountValuePerUnit As Double = 0
        Private _AfterOperationValueDecreaseAccountValue As Double = 0
        Private _AfterOperationValueDecreaseAccountValuePerUnit As Double = 0
        Private _AfterOperationValueIncreaseAccountValue As Double = 0
        Private _AfterOperationValueIncreaseAccountValuePerUnit As Double = 0
        Private _AfterOperationValueIncreaseAmortizationAccountValue As Double = 0
        Private _AfterOperationValueIncreaseAmortizationAccountValuePerUnit As Double = 0
        Private _AfterOperationAssetAmount As Integer = 0
        Private _AfterOperationAssetValue As Double = 0
        Private _AfterOperationAssetValuePerUnit As Double = 0
        Private _AfterOperationAssetValueRevaluedPortion As Double = 0
        Private _AfterOperationAssetValueRevaluedPortionPerUnit As Double = 0

#End Region

#Region " General Asset Data "

        ''' <summary>
        ''' An <see cref="LongTermAsset.ID">ID of the long term asset</see>.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property AssetID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AssetID
            End Get
        End Property

        ''' <summary>
        ''' A <see cref="LongTermAsset.Name">name of the long term asset</see>.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property AssetName() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AssetName.Trim
            End Get
        End Property

        ''' <summary>
        ''' A <see cref="LongTermAsset.MeasureUnit">measure unit of the long term asset</see>.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property AssetMeasureUnit() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AssetMeasureUnit.Trim
            End Get
        End Property

        ''' <summary>
        ''' A <see cref="LongTermAsset.AcquisitionDate">date of the long term asset aquisition</see>.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property AssetDateAcquired() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AssetDateAcquired
            End Get
        End Property

        ''' <summary>
        ''' An <see cref="LongTermAsset.AcquisitionJournalEntryID">ID of the journal entry of the long term asset aquisition</see>.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property AssetAquisitionID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AssetAquisitionID
            End Get
        End Property

        ''' <summary>
        ''' A <see cref="LongTermAsset.LiquidationUnitValue">liquidation value of the long term asset per unit</see>.
        ''' </summary>
        ''' <remarks>12 BAS para 57.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property AssetLiquidationValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AssetLiquidationValue)
            End Get
        End Property

#End Region

#Region " Initial State "

        ''' <summary>
        ''' An Initial <see cref="LongTermAsset.AccountAcquisition">acquisition account of the long term asset</see>.
        ''' </summary>
        ''' <remarks>12 BAS para 12.</remarks>
        Public ReadOnly Property InitialAssetAcquiredAccount() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _InitialAssetAcquiredAccount
            End Get
        End Property

        ''' <summary>
        ''' An Initial <see cref="LongTermAsset.AccountAccumulatedAmortization">amortization account of the long term asset</see>.
        ''' </summary>
        ''' <remarks>12 BAS para 68.</remarks>
        Public ReadOnly Property InitialAssetContraryAccount() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _InitialAssetContraryAccount
            End Get
        End Property

        ''' <summary>
        ''' An Initial <see cref="LongTermAsset.AccountValueDecrease">value decrease account of the long term asset</see>.
        ''' </summary>
        ''' <remarks>12 BAS para 49.</remarks>
        Public ReadOnly Property InitialAssetValueDecreaseAccount() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _InitialAssetValueDecreaseAccount
            End Get
        End Property

        ''' <summary>
        ''' An Initial <see cref="LongTermAsset.AccountValueDecrease">value increase account of the long term asset</see>.
        ''' </summary>
        ''' <remarks>12 BAS para 48.</remarks>
        Public ReadOnly Property InitialAssetValueIncreaseAccount() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _InitialAssetValueIncreaseAccount
            End Get
        End Property

        ''' <summary>
        ''' An Initial <see cref="LongTermAsset.AccountValueDecrease">amortization account of the long term asset 
        ''' revalued portion (accounted in the <see cref="InitialAssetValueIncreaseAccount">InitialAssetValueIncreaseAccount</see>)</see>.
        ''' </summary>
        ''' <remarks>12 BAS para 48.</remarks>
        Public ReadOnly Property InitialAssetValueIncreaseAmortizationAccount() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _InitialAssetValueIncreaseAmortizationAccount
            End Get
        End Property

        ''' <summary>
        ''' An initial balance for the <see cref="InitialAssetAcquiredAccount">InitialAssetAcquiredAccount</see>.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property InitialAcquisitionAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_InitialAcquisitionAccountValue)
            End Get
        End Property

        ''' <summary>
        ''' An initial balance for the <see cref="InitialAssetAcquiredAccount">InitialAssetAcquiredAccount</see> per unit.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property InitialAcquisitionAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_InitialAcquisitionAccountValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        ''' <summary>
        ''' An initial balance for the <see cref="InitialAssetContraryAccount">InitialAssetContraryAccount</see>.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property InitialAmortizationAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_InitialAmortizationAccountValue)
            End Get
        End Property

        ''' <summary>
        ''' An initial balance for the <see cref="InitialAssetContraryAccount">InitialAssetContraryAccount</see> per unit.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property InitialAmortizationAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_InitialAmortizationAccountValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        ''' <summary>
        ''' An initial balance for the <see cref="InitialAssetValueDecreaseAccount">InitialAssetValueDecreaseAccount</see>.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property InitialValueDecreaseAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_InitialValueDecreaseAccountValue)
            End Get
        End Property

        ''' <summary>
        ''' An initial balance for the <see cref="InitialAssetValueDecreaseAccount">InitialAssetValueDecreaseAccount</see> per unit.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property InitialValueDecreaseAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_InitialValueDecreaseAccountValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        ''' <summary>
        ''' An initial balance for the <see cref="InitialAssetValueIncreaseAccount">InitialAssetValueIncreaseAccount</see>.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property InitialValueIncreaseAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_InitialValueIncreaseAccountValue)
            End Get
        End Property

        ''' <summary>
        ''' An initial balance for the <see cref="InitialAssetValueIncreaseAccount">InitialAssetValueIncreaseAccount</see> per unit.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property InitialValueIncreaseAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_InitialValueIncreaseAccountValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        ''' <summary>
        ''' An initial balance for the <see cref="InitialAssetValueIncreaseAmortizationAccount">InitialAssetValueIncreaseAmortizationAccount</see>.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property InitialValueIncreaseAmortizationAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_InitialValueIncreaseAmortizationAccountValue)
            End Get
        End Property

        ''' <summary>
        ''' An initial balance for the <see cref="InitialAssetValueIncreaseAmortizationAccount">InitialAssetValueIncreaseAmortizationAccount</see> per unit.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property InitialValueIncreaseAmortizationAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_InitialValueIncreaseAmortizationAccountValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        ''' <summary>
        ''' An initial amount of the long term asset.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property InitialAssetAmount() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _InitialAssetAmount
            End Get
        End Property

        ''' <summary>
        ''' An initial total value of the long term asset.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property InitialAssetValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_InitialAssetValue)
            End Get
        End Property

        ''' <summary>
        ''' An initial value of the long term asset unit.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property InitialAssetValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_InitialAssetValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        ''' <summary>
        ''' An initial total value of the revalued portion of the long term asset
        ''' </summary>
        ''' <remarks><see cref="InitialAssetValueIncreaseAccount">InitialAssetValueIncreaseAccount</see> 
        ''' minus <see cref="InitialAssetValueIncreaseAmortizationAccount">InitialAssetValueIncreaseAmortizationAccount</see></remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property InitialAssetValueRevaluedPortion() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_InitialAssetValueRevaluedPortion)
            End Get
        End Property

        ''' <summary>
        ''' An initial value of the revalued portion of the long term asset per unit.
        ''' </summary>
        ''' <remarks><see cref="InitialAssetValueIncreaseAccount">InitialAssetValueIncreaseAccount</see> 
        ''' minus <see cref="InitialAssetValueIncreaseAmortizationAccount">InitialAssetValueIncreaseAmortizationAccount</see>
        ''' divided by the <see cref="InitialAssetAmount">InitialAssetAmount</see></remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property InitialAssetValueRevaluedPortionPerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_InitialAssetValueRevaluedPortionPerUnit, ROUNDUNITASSET)
            End Get
        End Property

        ''' <summary>
        ''' An initial number of months that the amortization of the long term asset unit is calculated for.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property InitialUsageTermMonths() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _InitialUsageTermMonths
            End Get
        End Property

        ''' <summary>
        ''' An initial amortization period of the long term asset (in years).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property InitialAmortizationPeriod() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _InitialAmortizationPeriod
            End Get
        End Property

        ''' <summary>
        ''' An initial use state of the long term asset.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property InitialUsageStatus() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _InitialUsageStatus
            End Get
        End Property

#End Region

#Region " Current State "

        ''' <summary>
        ''' A date that the state of the long term asset is calculated.
        ''' </summary>
        ''' <remarks>12 BAS para 12.</remarks>
        Public ReadOnly Property CurrentDate() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _CurrentDate
            End Get
        End Property

        ''' <summary>
        ''' A current <see cref="LongTermAsset.AccountAcquisition">acquisition account of the long term asset</see>.
        ''' </summary>
        ''' <remarks>12 BAS para 12.</remarks>
        Public ReadOnly Property CurrentAssetAcquiredAccount() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _CurrentAssetAcquiredAccount
            End Get
        End Property

        ''' <summary>
        ''' A current <see cref="LongTermAsset.AccountAccumulatedAmortization">amortization account of the long term asset</see>.
        ''' </summary>
        ''' <remarks>12 BAS para 68.</remarks>
        Public ReadOnly Property CurrentAssetContraryAccount() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _CurrentAssetContraryAccount
            End Get
        End Property

        ''' <summary>
        ''' A current <see cref="LongTermAsset.AccountValueDecrease">value decrease account of the long term asset</see>.
        ''' </summary>
        ''' <remarks>12 BAS para 49.</remarks>
        Public ReadOnly Property CurrentAssetValueDecreaseAccount() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _CurrentAssetValueDecreaseAccount
            End Get
        End Property

        ''' <summary>
        ''' A current <see cref="LongTermAsset.AccountValueDecrease">value increase account of the long term asset</see>.
        ''' </summary>
        ''' <remarks>12 BAS para 48.</remarks>
        Public ReadOnly Property CurrentAssetValueIncreaseAccount() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _CurrentAssetValueIncreaseAccount
            End Get
        End Property

        ''' <summary>
        ''' A current <see cref="LongTermAsset.AccountValueDecrease">amortization account of the long term asset 
        ''' revalued portion (accounted in the <see cref="CurrentAssetValueIncreaseAccount">CurrentAssetValueIncreaseAccount</see>)</see>.
        ''' </summary>
        ''' <remarks>12 BAS para 48.</remarks>
        Public ReadOnly Property CurrentAssetValueIncreaseAmortizationAccount() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _CurrentAssetValueIncreaseAmortizationAccount
            End Get
        End Property

        ''' <summary>
        ''' A current balance for the <see cref="CurrentAssetAcquiredAccount">CurrentAssetAcquiredAccount</see>.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property CurrentAcquisitionAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_CurrentAcquisitionAccountValue)
            End Get
        End Property

        ''' <summary>
        ''' A current balance for the <see cref="CurrentAssetAcquiredAccount">CurrentAssetAcquiredAccount</see> per unit.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property CurrentAcquisitionAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_CurrentAcquisitionAccountValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        ''' <summary>
        ''' A current balance for the <see cref="CurrentAssetContraryAccount">CurrentAssetContraryAccount</see>.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property CurrentAmortizationAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_CurrentAmortizationAccountValue)
            End Get
        End Property

        ''' <summary>
        ''' A current balance for the <see cref="CurrentAssetContraryAccount">CurrentAssetContraryAccount</see> per unit.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property CurrentAmortizationAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_CurrentAmortizationAccountValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        ''' <summary>
        ''' A current balance for the <see cref="CurrentAssetValueDecreaseAccount">CurrentAssetValueDecreaseAccount</see>.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property CurrentValueDecreaseAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_CurrentValueDecreaseAccountValue)
            End Get
        End Property

        ''' <summary>
        ''' A current balance for the <see cref="CurrentAssetValueDecreaseAccount">CurrentAssetValueDecreaseAccount</see> per unit.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property CurrentValueDecreaseAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_CurrentValueDecreaseAccountValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        ''' <summary>
        ''' A current balance for the <see cref="CurrentAssetValueIncreaseAccount">CurrentAssetValueIncreaseAccount</see>.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property CurrentValueIncreaseAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_CurrentValueIncreaseAccountValue)
            End Get
        End Property

        ''' <summary>
        ''' A current balance for the <see cref="CurrentAssetValueIncreaseAccount">CurrentAssetValueIncreaseAccount</see> per unit.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property CurrentValueIncreaseAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_CurrentValueIncreaseAccountValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        ''' <summary>
        ''' A current balance for the <see cref="CurrentAssetValueIncreaseAmortizationAccount">CurrentAssetValueIncreaseAmortizationAccount</see>.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property CurrentValueIncreaseAmortizationAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_CurrentValueIncreaseAmortizationAccountValue)
            End Get
        End Property

        ''' <summary>
        ''' A current balance for the <see cref="CurrentAssetValueIncreaseAmortizationAccount">CurrentAssetValueIncreaseAmortizationAccount</see> per unit.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property CurrentValueIncreaseAmortizationAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_CurrentValueIncreaseAmortizationAccountValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        ''' <summary>
        ''' A current amount of the long term asset.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property CurrentAssetAmount() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _CurrentAssetAmount
            End Get
        End Property

        ''' <summary>
        ''' A current total value of the long term asset.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property CurrentAssetValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_CurrentAssetValue)
            End Get
        End Property

        ''' <summary>
        ''' A current value of the long term asset unit.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property CurrentAssetValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_CurrentAssetValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        ''' <summary>
        ''' A current total value of the revalued portion of the long term asset
        ''' </summary>
        ''' <remarks><see cref="CurrentAssetValueIncreaseAccount">CurrentAssetValueIncreaseAccount</see> 
        ''' minus <see cref="CurrentAssetValueIncreaseAmortizationAccount">CurrentAssetValueIncreaseAmortizationAccount</see></remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property CurrentAssetValueRevaluedPortion() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_CurrentAssetValueRevaluedPortion)
            End Get
        End Property

        ''' <summary>
        ''' A current value of the revalued portion of the long term asset per unit.
        ''' </summary>
        ''' <remarks><see cref="CurrentAssetValueIncreaseAccount">CurrentAssetValueIncreaseAccount</see> 
        ''' minus <see cref="CurrentAssetValueIncreaseAmortizationAccount">CurrentAssetValueIncreaseAmortizationAccount</see>
        ''' divided by the <see cref="CurrentAssetAmount">CurrentAssetAmount</see></remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property CurrentAssetValueRevaluedPortionPerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_CurrentAssetValueRevaluedPortionPerUnit, ROUNDUNITASSET)
            End Get
        End Property

        ''' <summary>
        ''' A current number of months that the amortization of the long term asset unit is calculated for.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property CurrentUsageTermMonths() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _CurrentUsageTermMonths
            End Get
        End Property

        ''' <summary>
        ''' A current amortization period of the long term asset (in years).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property CurrentAmortizationPeriod() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _CurrentAmortizationPeriod
            End Get
        End Property

        ''' <summary>
        ''' Whether the long term asset is currently operational.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property CurrentUsageStatus() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _CurrentUsageStatus
            End Get
        End Property

#End Region

#Region " State Delta "

        ''' <summary>
        ''' A change of the amount of the long term asset made by the operation.
        ''' </summary>
        ''' <remarks></remarks>
        <IntegerField(ValueRequiredLevel.Optional, True)> _
        Public Property ChangeAssetAmount() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ChangeAssetAmount
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Friend Set(ByVal value As Integer)
                CanWriteProperty(True)
                If _ChangeAssetAmount <> value Then
                    _ChangeAssetAmount = value
                    PropertyHasChanged()
                    If Not _DisableCalculations Then CalculateAfterOperationProperties(True)
                End If
            End Set
        End Property

        ''' <summary>
        ''' A change of balance for the <see cref="CurrentAssetAcquiredAccount">CurrentAssetAcquiredAccount</see> made by the operation.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public Property ChangeAcquisitionAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ChangeAcquisitionAccountValue)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Friend Set(ByVal value As Double)
                CanWriteProperty(True)
                If CRound(_ChangeAcquisitionAccountValue) <> CRound(value) Then
                    _ChangeAcquisitionAccountValue = CRound(value)
                    PropertyHasChanged()
                    If Not _DisableCalculations Then CalculateAfterOperationProperties(True)
                End If
            End Set
        End Property

        ''' <summary>
        ''' A change of balance for the <see cref="CurrentAssetAcquiredAccount">CurrentAssetAcquiredAccount</see> per unit made by the operation.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public Property ChangeAcquisitionAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ChangeAcquisitionAccountValuePerUnit, ROUNDUNITASSET)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Friend Set(ByVal value As Double)
                CanWriteProperty(True)
                If CRound(_ChangeAcquisitionAccountValuePerUnit, ROUNDUNITASSET) <> CRound(value, ROUNDUNITASSET) Then
                    _ChangeAcquisitionAccountValuePerUnit = CRound(value, ROUNDUNITASSET)
                    PropertyHasChanged()
                    If Not _DisableCalculations Then CalculateAfterOperationProperties(True)
                End If
            End Set
        End Property

        ''' <summary>
        ''' A change of balance for the <see cref="CurrentAssetContraryAccount">CurrentAssetContraryAccount</see> made by the operation.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public Property ChangeAmortizationAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ChangeAmortizationAccountValue)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Friend Set(ByVal value As Double)
                CanWriteProperty(True)
                If CRound(_ChangeAmortizationAccountValue) <> CRound(value) Then
                    _ChangeAmortizationAccountValue = CRound(value)
                    PropertyHasChanged()
                    If Not _DisableCalculations Then CalculateAfterOperationProperties(True)
                End If
            End Set
        End Property

        ''' <summary>
        ''' A change of balance for the <see cref="CurrentAssetContraryAccount">CurrentAssetContraryAccount</see> per unit made by the operation.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public Property ChangeAmortizationAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ChangeAmortizationAccountValuePerUnit, ROUNDUNITASSET)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Friend Set(ByVal value As Double)
                CanWriteProperty(True)
                If CRound(_ChangeAmortizationAccountValuePerUnit, ROUNDUNITASSET) <> CRound(value, ROUNDUNITASSET) Then
                    _ChangeAmortizationAccountValuePerUnit = CRound(value, ROUNDUNITASSET)
                    PropertyHasChanged()
                    If Not _DisableCalculations Then CalculateAfterOperationProperties(True)
                End If
            End Set
        End Property

        ''' <summary>
        ''' A change of balance for the <see cref="CurrentAssetValueDecreaseAccount">CurrentAssetValueDecreaseAccount</see> made by the operation.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public Property ChangeValueDecreaseAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ChangeValueDecreaseAccountValue)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Friend Set(ByVal value As Double)
                CanWriteProperty(True)
                If CRound(_ChangeValueDecreaseAccountValue) <> CRound(value) Then
                    _ChangeValueDecreaseAccountValue = CRound(value)
                    PropertyHasChanged()
                    If Not _DisableCalculations Then CalculateAfterOperationProperties(True)
                End If
            End Set
        End Property

        ''' <summary>
        ''' A change of balance for the <see cref="CurrentAssetValueDecreaseAccount">CurrentAssetValueDecreaseAccount</see> per unit made by the operation.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public Property ChangeValueDecreaseAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ChangeValueDecreaseAccountValuePerUnit, ROUNDUNITASSET)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Friend Set(ByVal value As Double)
                CanWriteProperty(True)
                If CRound(_ChangeValueDecreaseAccountValuePerUnit, ROUNDUNITASSET) <> CRound(value, ROUNDUNITASSET) Then
                    _ChangeValueDecreaseAccountValuePerUnit = CRound(value, ROUNDUNITASSET)
                    PropertyHasChanged()
                    If Not _DisableCalculations Then CalculateAfterOperationProperties(True)
                End If
            End Set
        End Property

        ''' <summary>
        ''' A change of balance for the <see cref="CurrentAssetValueIncreaseAccount">CurrentAssetValueIncreaseAccount</see> made by the operation.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public Property ChangeValueIncreaseAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ChangeValueIncreaseAccountValue)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Friend Set(ByVal value As Double)
                CanWriteProperty(True)
                If CRound(_ChangeValueIncreaseAccountValue) <> CRound(value) Then
                    _ChangeValueIncreaseAccountValue = CRound(value)
                    PropertyHasChanged()
                    If Not _DisableCalculations Then CalculateAfterOperationProperties(True)
                End If
            End Set
        End Property

        ''' <summary>
        ''' A change of balance for the <see cref="CurrentAssetValueIncreaseAccount">CurrentAssetValueIncreaseAccount</see> per unit made by the operation.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public Property ChangeValueIncreaseAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ChangeValueIncreaseAccountValuePerUnit, ROUNDUNITASSET)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Friend Set(ByVal value As Double)
                CanWriteProperty(True)
                If CRound(_ChangeValueIncreaseAccountValuePerUnit, ROUNDUNITASSET) <> CRound(value, ROUNDUNITASSET) Then
                    _ChangeValueIncreaseAccountValuePerUnit = CRound(value, ROUNDUNITASSET)
                    PropertyHasChanged()
                    If Not _DisableCalculations Then CalculateAfterOperationProperties(True)
                End If
            End Set
        End Property

        ''' <summary>
        ''' A change of balance for the <see cref="CurrentAssetValueIncreaseAmortizationAccount">CurrentAssetValueIncreaseAmortizationAccount</see> made by the operation.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public Property ChangeValueIncreaseAmortizationAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ChangeValueIncreaseAmortizationAccountValue)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Friend Set(ByVal value As Double)
                CanWriteProperty(True)
                If CRound(_ChangeValueIncreaseAmortizationAccountValue) <> CRound(value) Then
                    _ChangeValueIncreaseAmortizationAccountValue = CRound(value)
                    PropertyHasChanged()
                    If Not _DisableCalculations Then CalculateAfterOperationProperties(True)
                End If
            End Set
        End Property

        ''' <summary>
        ''' A change of balance for the <see cref="CurrentAssetValueIncreaseAmortizationAccount">CurrentAssetValueIncreaseAmortizationAccount</see> per unit made by the operation.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public Property ChangeValueIncreaseAmortizationAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ChangeValueIncreaseAmortizationAccountValuePerUnit, ROUNDUNITASSET)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Friend Set(ByVal value As Double)
                CanWriteProperty(True)
                If CRound(_ChangeValueIncreaseAmortizationAccountValuePerUnit, ROUNDUNITASSET) <> CRound(value, ROUNDUNITASSET) Then
                    _ChangeValueIncreaseAmortizationAccountValuePerUnit = CRound(value, ROUNDUNITASSET)
                    PropertyHasChanged()
                    If Not _DisableCalculations Then CalculateAfterOperationProperties(True)
                End If
            End Set
        End Property

        ''' <summary>
        ''' A change of the unit value of the long term asset made by the operation.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property ChangeAssetUnitValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ChangeAssetUnitValue, ROUNDUNITASSET)
            End Get
        End Property

        ''' <summary>
        ''' A change of the total value of the long term asset made by the operation.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property ChangeAssetValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ChangeAssetValue)
            End Get
        End Property

        ''' <summary>
        ''' A change of the total value of the revalued portion of the long term asset.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property ChangeAssetRevaluedPortionUnitValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ChangeAssetRevaluedPortionUnitValue, ROUNDUNITASSET)
            End Get
        End Property

        ''' <summary>
        ''' A change of the unit value of the revalued portion of the long term asset.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property ChangeAssetRevaluedPortionValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ChangeAssetRevaluedPortionValue)
            End Get
        End Property

#End Region

#Region " State After The Operation "

        ''' <summary>
        ''' A balance for the <see cref="CurrentAssetAcquiredAccount">CurrentAssetAcquiredAccount</see> after the operation.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property AfterOperationAcquisitionAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AfterOperationAcquisitionAccountValue)
            End Get
        End Property

        ''' <summary>
        ''' A balance for the <see cref="CurrentAssetAcquiredAccount">CurrentAssetAcquiredAccount</see> per unit after the operation.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, ROUNDUNITASSET)> _
        Public ReadOnly Property AfterOperationAcquisitionAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AfterOperationAcquisitionAccountValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        ''' <summary>
        ''' A balance for the <see cref="CurrentAssetContraryAccount">CurrentAssetContraryAccount</see> after the operation.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2, True, Double.MinValue, 0, True)> _
        Public ReadOnly Property AfterOperationAmortizationAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AfterOperationAmortizationAccountValue)
            End Get
        End Property

        ''' <summary>
        ''' A balance for the <see cref="CurrentAssetContraryAccount">CurrentAssetContraryAccount</see> per unit after the operation.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET, True, Double.MinValue, 0, True)> _
        Public ReadOnly Property AfterOperationAmortizationAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AfterOperationAmortizationAccountValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        ''' <summary>
        ''' A balance for the <see cref="CurrentAssetValueDecreaseAccount">CurrentAssetValueDecreaseAccount</see> after the operation.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2, True, Double.MinValue, 0, True)> _
        Public ReadOnly Property AfterOperationValueDecreaseAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AfterOperationValueDecreaseAccountValue)
            End Get
        End Property

        ''' <summary>
        ''' A balance for the <see cref="CurrentAssetValueDecreaseAccount">CurrentAssetValueDecreaseAccount</see> per unit after the operation.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET, True, Double.MinValue, 0, True)> _
        Public ReadOnly Property AfterOperationValueDecreaseAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AfterOperationValueDecreaseAccountValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        ''' <summary>
        ''' A balance for the <see cref="CurrentAssetValueIncreaseAccount">CurrentAssetValueIncreaseAccount</see> after the operation.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property AfterOperationValueIncreaseAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AfterOperationValueIncreaseAccountValue)
            End Get
        End Property

        ''' <summary>
        ''' A balance for the <see cref="CurrentAssetValueIncreaseAccount">CurrentAssetValueIncreaseAccount</see> per unit after the operation.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, ROUNDUNITASSET)> _
        Public ReadOnly Property AfterOperationValueIncreaseAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AfterOperationValueIncreaseAccountValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        ''' <summary>
        ''' A balance for the <see cref="CurrentAssetValueIncreaseAmortizationAccount">CurrentAssetValueIncreaseAmortizationAccount</see> after the operation.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2, True, Double.MinValue, 0, True)> _
        Public ReadOnly Property AfterOperationValueIncreaseAmortizationAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AfterOperationValueIncreaseAmortizationAccountValue)
            End Get
        End Property

        ''' <summary>
        ''' A balance for the <see cref="CurrentAssetValueIncreaseAmortizationAccount">CurrentAssetValueIncreaseAmortizationAccount</see> per unit after the operation.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET, True, Double.MinValue, 0, True)> _
        Public ReadOnly Property AfterOperationValueIncreaseAmortizationAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AfterOperationValueIncreaseAmortizationAccountValuePerUnit, ROUNDUNITASSET)
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
                Return _AfterOperationAssetAmount
            End Get
        End Property

        ''' <summary>
        ''' A total value of the long term asset after the operation.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property AfterOperationAssetValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AfterOperationAssetValue)
            End Get
        End Property

        ''' <summary>
        ''' A unit value of the long term asset after the operation.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, ROUNDUNITASSET)> _
        Public ReadOnly Property AfterOperationAssetValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AfterOperationAssetValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        ''' <summary>
        ''' A total value of the revalued portion of the long term asset after the operation.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property AfterOperationAssetValueRevaluedPortion() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AfterOperationAssetValueRevaluedPortion)
            End Get
        End Property

        ''' <summary>
        ''' A unit value of the revalued portion of the long term asset after the operation.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, ROUNDUNITASSET)> _
        Public ReadOnly Property AfterOperationAssetValueRevaluedPortionPerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AfterOperationAssetValueRevaluedPortionPerUnit, ROUNDUNITASSET)
            End Get
        End Property

#End Region

        ''' <summary>
        ''' Whether to disable calculation of "after operation" properties on PropertyHasChanged.
        ''' </summary>
        ''' <remarks>Should be used when loading initial operation data.</remarks>
        Public Property DisableCalculations() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DisableCalculations
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Friend Set(ByVal value As Boolean)
                CanWriteProperty(True)
                If _DisableCalculations <> value Then
                    _DisableCalculations = value
                End If
            End Set
        End Property

        ''' <summary>
        ''' Sets a new date of the long term asset current status 
        ''' and forces status recalculation.
        ''' </summary>
        ''' <param name="newDate">A new date to calculate the status at.</param>
        ''' <remarks></remarks>
        Friend Sub SetCurrentDate(ByVal newDate As Date)

            If newDate.Date <> _CurrentDate.Date Then
                _CurrentDate = newDate
                PropertyHasChanged("CurrentDate")
                CalculateCurrentProperties(True)
            End If

        End Sub

        ''' <summary>
        ''' Initializes the long term asset status for an existing operation.
        ''' </summary>
        ''' <param name="currentOperationDate"></param>
        ''' <remarks>Initialization is only required for an old operation data.
        ''' Initialization should be done by steps:
        ''' 1. Set <see cref="DisableCalculations">DisableCalculations</see> to TRUE;
        ''' 2. Set state delta properties ('Change') with the operation data;
        ''' 3. Invoke <see cref="InitializeOldData">InitializeOldData</see> 
        ''' with the operation date as param;
        ''' 4. Set <see cref="DisableCalculations">DisableCalculations</see> to FALSE.</remarks>
        Friend Sub InitializeOldData(ByVal currentOperationDate As Date)

            _CurrentDate = currentOperationDate
            CalculateCurrentProperties(False)

            MarkOld()

        End Sub

        ''' <summary>
        ''' Forces recalculation of the long term asset status after the operation.
        ''' </summary>
        ''' <remarks>Used for updating multiple delta ('Change') properties:
        ''' 1. Set <see cref="DisableCalculations">DisableCalculations</see> to TRUE;
        ''' 2. Set state delta properties ('Change') with the operation data;
        ''' 3. Set <see cref="DisableCalculations">DisableCalculations</see> to FALSE;
        ''' 4. Invoke <see cref="CalculateAfterOperationProperties">CalculateAfterOperationProperties</see> 
        ''' to do the calculations.</remarks>
        Friend Sub CalculateAfterOperationProperties()
            CalculateAfterOperationProperties(True)
        End Sub


        Private Sub CalculateCurrentProperties(ByVal raisePropertyHasChanged As Boolean)

            _CurrentAssetAcquiredAccount = _InitialAssetAcquiredAccount
            _CurrentAssetContraryAccount = _InitialAssetContraryAccount
            _CurrentAssetValueDecreaseAccount = _InitialAssetValueDecreaseAccount
            _CurrentAssetValueIncreaseAccount = _InitialAssetValueIncreaseAccount
            _CurrentAssetValueIncreaseAmortizationAccount = _InitialAssetValueIncreaseAccount
            _CurrentUsageStatus = _InitialUsageStatus
            _CurrentAmortizationPeriod = _InitialAmortizationPeriod
            _CurrentUsageTermMonths = _InitialUsageTermMonths
            _CurrentAcquisitionAccountValue = _InitialAcquisitionAccountValue
            _CurrentAcquisitionAccountValuePerUnit = _InitialAcquisitionAccountValuePerUnit
            _CurrentAmortizationAccountValue = _InitialAmortizationAccountValue
            _CurrentAmortizationAccountValuePerUnit = _InitialAmortizationAccountValuePerUnit
            _CurrentValueDecreaseAccountValue = _InitialValueDecreaseAccountValue
            _CurrentValueDecreaseAccountValuePerUnit = _InitialValueDecreaseAccountValuePerUnit
            _CurrentValueIncreaseAccountValue = _InitialValueIncreaseAccountValue
            _CurrentValueIncreaseAccountValuePerUnit = _InitialValueIncreaseAccountValuePerUnit
            _CurrentValueIncreaseAmortizationAccountValue = _InitialValueIncreaseAmortizationAccountValue
            _CurrentValueIncreaseAmortizationAccountValuePerUnit = _InitialValueIncreaseAmortizationAccountValuePerUnit
            _CurrentAssetAmount = _InitialAssetAmount

            For Each delta As OperationDelta In _DeltaList

                If delta.Date.Date <= _CurrentDate.Date Then

                    If delta.OperationType = LtaOperationType.AccountChange Then

                        If delta.AccountChangeType = LtaAccountChangeType.AcquisitionAccount Then
                            _CurrentAssetAcquiredAccount = delta.NewAccount
                        ElseIf delta.AccountChangeType = LtaAccountChangeType.AmortizationAccount Then
                            _CurrentAssetContraryAccount = delta.NewAccount
                        ElseIf delta.AccountChangeType = LtaAccountChangeType.ValueDecreaseAccount Then
                            _CurrentAssetValueDecreaseAccount = delta.NewAccount
                        ElseIf delta.AccountChangeType = LtaAccountChangeType.ValueIncreaseAccount Then
                            _CurrentAssetValueIncreaseAccount = delta.NewAccount
                        ElseIf delta.AccountChangeType = LtaAccountChangeType.ValueIncreaseAmortizationAccount Then
                            _CurrentAssetValueIncreaseAmortizationAccount = delta.NewAccount
                        Else
                            Throw New NotImplementedException("")
                        End If

                    ElseIf delta.OperationType = LtaOperationType.AmortizationPeriod Then

                        _CurrentAmortizationPeriod = delta.NewAmortizationPeriod

                    ElseIf delta.OperationType = LtaOperationType.UsingEnd _
                        OrElse delta.OperationType = LtaOperationType.UsingStart Then

                        _CurrentUsageStatus = Not _CurrentUsageStatus

                    Else

                        _CurrentUsageTermMonths = _CurrentUsageTermMonths + delta.UsageLength

                        _CurrentAcquisitionAccountValue = _
                            CRound(_CurrentAcquisitionAccountValue _
                            + delta.AcquisitionAccountValueChange)
                        _CurrentAcquisitionAccountValuePerUnit = _
                            CRound(_CurrentAcquisitionAccountValuePerUnit _
                            + delta.AcquisitionAccountValueChangePerUnit, ROUNDUNITASSET)
                        _CurrentAmortizationAccountValue = _
                            CRound(_CurrentAmortizationAccountValue _
                            + delta.AmortizationAccountValueChange)
                        _CurrentAmortizationAccountValuePerUnit = _
                            CRound(_CurrentAmortizationAccountValuePerUnit _
                            + delta.AmortizationAccountValueChangePerUnit, ROUNDUNITASSET)
                        _CurrentValueDecreaseAccountValue = _
                            CRound(_CurrentValueDecreaseAccountValue _
                            + delta.ValueDecreaseAccountValueChange)
                        _CurrentValueDecreaseAccountValuePerUnit = _
                            CRound(_CurrentValueDecreaseAccountValuePerUnit _
                            + delta.ValueDecreaseAccountValueChangePerUnit, ROUNDUNITASSET)
                        _CurrentValueIncreaseAccountValue = _
                            CRound(_CurrentValueIncreaseAccountValue _
                            + delta.ValueIncreaseAccountValueChange)
                        _CurrentValueIncreaseAccountValuePerUnit = _
                            CRound(_CurrentValueIncreaseAccountValuePerUnit _
                            + delta.ValueIncreaseAccountValueChangePerUnit, ROUNDUNITASSET)
                        _CurrentValueIncreaseAmortizationAccountValue = _
                            CRound(_CurrentValueIncreaseAmortizationAccountValue _
                            + delta.ValueIncreaseAmmortizationAccountValueChange)
                        _CurrentValueIncreaseAmortizationAccountValuePerUnit = _
                            CRound(_CurrentValueIncreaseAmortizationAccountValuePerUnit _
                            + delta.ValueIncreaseAmmortizationAccountValueChangePerUnit, ROUNDUNITASSET)
                        _CurrentAssetAmount = _CurrentAssetAmount - delta.AmmountChange

                    End If

                End If

            Next

            _CurrentAssetValue = CRound(_CurrentAcquisitionAccountValue _
                - _CurrentAmortizationAccountValue _
                - _CurrentValueDecreaseAccountValue, 2)
            _CurrentAssetValuePerUnit = CRound(_CurrentAcquisitionAccountValuePerUnit _
                - _CurrentAmortizationAccountValuePerUnit _
                - _CurrentValueDecreaseAccountValuePerUnit, ROUNDUNITASSET)

            If _CurrentValueDecreaseAccountValue > 0 Then
                _CurrentAssetValueRevaluedPortion = -CRound(_CurrentValueDecreaseAccountValue)
                _CurrentAssetValueRevaluedPortionPerUnit = _
                    -CRound(_CurrentValueDecreaseAccountValuePerUnit, ROUNDUNITASSET)
            ElseIf _CurrentValueIncreaseAccountValue > 0 Then
                _CurrentAssetValueRevaluedPortion = CRound(_CurrentValueIncreaseAccountValue _
                    - _CurrentValueIncreaseAmortizationAccountValue)
                _CurrentAssetValueRevaluedPortionPerUnit = CRound(_CurrentValueIncreaseAccountValuePerUnit _
                    - _CurrentValueIncreaseAmortizationAccountValuePerUnit, ROUNDUNITASSET)
            Else
                _CurrentAssetValueRevaluedPortion = 0
                _CurrentAssetValueRevaluedPortionPerUnit = 0
            End If

            If raisePropertyHasChanged Then

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

            CalculateAfterOperationProperties(raisePropertyHasChanged)

        End Sub

        Private Sub CalculateAfterOperationProperties(ByVal raisePropertyHasChanged As Boolean)

            _AfterOperationAssetAmount = _CurrentAssetAmount + _ChangeAssetAmount

            _AfterOperationAcquisitionAccountValue = CRound(_CurrentAcquisitionAccountValue _
                + _ChangeAcquisitionAccountValue, 2)
            _AfterOperationAcquisitionAccountValuePerUnit = CRound(_CurrentAcquisitionAccountValuePerUnit _
                + _ChangeAcquisitionAccountValuePerUnit, ROUNDUNITASSET)
            _AfterOperationAmortizationAccountValue = CRound(_CurrentAmortizationAccountValue _
                + _ChangeAmortizationAccountValue, 2)
            _AfterOperationAmortizationAccountValuePerUnit = CRound(_CurrentAmortizationAccountValuePerUnit _
                + _ChangeAmortizationAccountValuePerUnit, ROUNDUNITASSET)
            _AfterOperationValueDecreaseAccountValue = CRound(_CurrentValueDecreaseAccountValue _
                + _ChangeValueDecreaseAccountValue, 2)
            _AfterOperationValueDecreaseAccountValuePerUnit = CRound(_CurrentValueDecreaseAccountValuePerUnit _
                + _ChangeValueDecreaseAccountValuePerUnit, ROUNDUNITASSET)
            _AfterOperationValueIncreaseAccountValue = CRound(_CurrentValueIncreaseAccountValue _
                + _ChangeValueIncreaseAccountValue, 2)
            _AfterOperationValueIncreaseAccountValuePerUnit = CRound(_CurrentValueIncreaseAccountValuePerUnit _
                + _ChangeValueIncreaseAccountValuePerUnit, ROUNDUNITASSET)
            _AfterOperationValueIncreaseAmortizationAccountValue = CRound(_CurrentValueIncreaseAmortizationAccountValue _
                + _ChangeValueIncreaseAmortizationAccountValue, 2)
            _AfterOperationValueIncreaseAmortizationAccountValuePerUnit = CRound(_CurrentValueIncreaseAmortizationAccountValuePerUnit _
                + _ChangeValueIncreaseAmortizationAccountValuePerUnit, ROUNDUNITASSET)

            _AfterOperationAssetValue = CRound(_AfterOperationAcquisitionAccountValue _
                - _AfterOperationAmortizationAccountValue _
                - _AfterOperationValueDecreaseAccountValue, 2)
            _AfterOperationAssetValuePerUnit = CRound(_AfterOperationAcquisitionAccountValuePerUnit _
                - _AfterOperationAmortizationAccountValuePerUnit _
                - _AfterOperationValueDecreaseAccountValuePerUnit, ROUNDUNITASSET)

            If _AfterOperationValueDecreaseAccountValue > 0 Then
                _AfterOperationAssetValueRevaluedPortion = -CRound(_AfterOperationValueDecreaseAccountValue)
                _AfterOperationAssetValueRevaluedPortionPerUnit = _
                    -CRound(_AfterOperationValueDecreaseAccountValuePerUnit, ROUNDUNITASSET)
            ElseIf _CurrentValueIncreaseAccountValue > 0 Then
                _AfterOperationAssetValueRevaluedPortion = CRound(_AfterOperationValueIncreaseAccountValue _
                    - _AfterOperationValueIncreaseAmortizationAccountValue)
                _AfterOperationAssetValueRevaluedPortionPerUnit = CRound(_AfterOperationValueIncreaseAccountValuePerUnit _
                    - _AfterOperationValueIncreaseAmortizationAccountValuePerUnit, ROUNDUNITASSET)
            Else
                _AfterOperationAssetValueRevaluedPortion = 0
                _AfterOperationAssetValueRevaluedPortionPerUnit = 0
            End If

            _ChangeAssetValue = CRound(_AfterOperationAssetValue - _CurrentAssetValue, 2)
            _ChangeAssetUnitValue = CRound(_AfterOperationAssetValuePerUnit - _CurrentAssetValuePerUnit, ROUNDUNITASSET)
            _ChangeAssetRevaluedPortionValue = CRound(_AfterOperationAssetValueRevaluedPortion _
                - _CurrentAssetValueRevaluedPortion, 2)
            _ChangeAssetRevaluedPortionUnitValue = CRound(_AfterOperationAssetValueRevaluedPortionPerUnit _
                - _CurrentAssetValueRevaluedPortionPerUnit, ROUNDUNITASSET)

            If raisePropertyHasChanged Then
                PropertyHasChanged("AfterOperationAssetAmount")
                PropertyHasChanged("AfterOperationAcquisitionAccountValue")
                PropertyHasChanged("AfterOperationAcquisitionAccountValuePerUnit")
                PropertyHasChanged("AfterOperationAmortizationAccountValue")
                PropertyHasChanged("AfterOperationAmortizationAccountValuePerUnit")
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
                PropertyHasChanged("ChangeAssetValue")
                PropertyHasChanged("ChangeAssetUnitValue")
                PropertyHasChanged("ChangeAssetRevaluedPortionValue")
                PropertyHasChanged("ChangeAssetRevaluedPortionUnitValue")
            End If

        End Sub


        Protected Overrides Function GetIdValue() As Object
            Return _AssetID
        End Function

        Public Overrides Function ToString() As String
            Return String.Format(My.Resources.Assets_OperationBackground_ToString, _AssetName)
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()

            ValidationRules.AddRule(AddressOf AfterOperationAssetAmountValidation, _
                New Csla.Validation.RuleArgs("AfterOperationAssetAmount"))
            ValidationRules.AddRule(AddressOf AfterOperationAcquisitionAccountValueValidation, _
                New Csla.Validation.RuleArgs("AfterOperationAcquisitionAccountValue"))
            ValidationRules.AddRule(AddressOf AfterOperationAcquisitionAccountValuePerUnitValidation, _
                New Csla.Validation.RuleArgs("AfterOperationAcquisitionAccountValuePerUnit"))
            ValidationRules.AddRule(AddressOf AfterOperationAcquisitionAccountValueValidation, _
                New Csla.Validation.RuleArgs("AfterOperationAmortizationAccountValue"))
            ValidationRules.AddRule(AddressOf AfterOperationAcquisitionAccountValuePerUnitValidation, _
                New Csla.Validation.RuleArgs("AfterOperationAmortizationAccountValuePerUnit"))
            ValidationRules.AddRule(AddressOf AfterOperationValueDecreaseAccountValueValidation, _
                New Csla.Validation.RuleArgs("AfterOperationValueDecreaseAccountValue"))
            ValidationRules.AddRule(AddressOf AfterOperationValueDecreaseAccountValuePerUnitValidation, _
                New Csla.Validation.RuleArgs("AfterOperationValueDecreaseAccountValuePerUnit"))
            ValidationRules.AddRule(AddressOf AfterOperationValueIncreaseAccountValueValidation, _
                New Csla.Validation.RuleArgs("AfterOperationValueIncreaseAccountValue"))
            ValidationRules.AddRule(AddressOf AfterOperationValueIncreaseAccountValuePerUnitValidation, _
                New Csla.Validation.RuleArgs("AfterOperationValueIncreaseAccountValuePerUnit"))
            ValidationRules.AddRule(AddressOf AfterOperationValueIncreaseAmortizationAccountValueValidation, _
                New Csla.Validation.RuleArgs("AfterOperationValueIncreaseAmortizationAccountValue"))
            ValidationRules.AddRule(AddressOf AfterOperationValueIncreaseAmortizationAccountValuePerUnitValidation, _
                New Csla.Validation.RuleArgs("AfterOperationValueIncreaseAmortizationAccountValuePerUnit"))
            ValidationRules.AddRule(AddressOf ChangeAcquisitionAccountValuePerUnitValidation, _
                New Csla.Validation.RuleArgs("ChangeAcquisitionAccountValuePerUnit"))
            ValidationRules.AddRule(AddressOf ChangeAmortizationAccountValuePerUnitValidation, _
                New Csla.Validation.RuleArgs("ChangeAmortizationAccountValuePerUnit"))
            ValidationRules.AddRule(AddressOf ChangeValueDecreaseAccountValuePerUnitValidation, _
                New Csla.Validation.RuleArgs("ChangeValueDecreaseAccountValuePerUnit"))
            ValidationRules.AddRule(AddressOf ChangeValueIncreaseAccountValuePerUnitValidation, _
                New Csla.Validation.RuleArgs("ChangeValueIncreaseAccountValuePerUnit"))
            ValidationRules.AddRule(AddressOf ChangeValueIncreaseAmortizationAccountValuePerUnitValidation, _
                New Csla.Validation.RuleArgs("ChangeValueIncreaseAmortizationAccountValuePerUnit"))

            ValidationRules.AddDependantProperty("CurrentAssetAmount", _
                "ChangeAcquisitionAccountValuePerUnit", False)
            ValidationRules.AddDependantProperty("ChangeAcquisitionAccountValue", _
                "ChangeAcquisitionAccountValuePerUnit", False)
            ValidationRules.AddDependantProperty("CurrentAssetAmount", _
                "ChangeAmortizationAccountValuePerUnit", False)
            ValidationRules.AddDependantProperty("ChangeAmortizationAccountValue", _
                "ChangeAmortizationAccountValuePerUnit", False)
            ValidationRules.AddDependantProperty("CurrentAssetAmount", _
                "ChangeValueDecreaseAccountValuePerUnit", False)
            ValidationRules.AddDependantProperty("ChangeValueDecreaseAccountValue", _
                "ChangeValueDecreaseAccountValuePerUnit", False)
            ValidationRules.AddDependantProperty("CurrentAssetAmount", _
                "ChangeValueIncreaseAccountValuePerUnit", False)
            ValidationRules.AddDependantProperty("ChangeValueIncreaseAccountValue", _
                "ChangeValueIncreaseAccountValuePerUnit", False)
            ValidationRules.AddDependantProperty("CurrentAssetAmount", _
                "ChangeValueIncreaseAmortizationAccountValuePerUnit", False)
            ValidationRules.AddDependantProperty("ChangeValueIncreaseAmortizationAccountValue", _
                "ChangeValueIncreaseAmortizationAccountValuePerUnit", False)

        End Sub

        ''' <summary>
        ''' Rule ensuring that <see cref="AfterOperationAssetAmount">AfterOperationAssetAmount</see> is valid.
        ''' </summary>
        ''' <param name="target">Object containing the data to validate</param>
        ''' <param name="e">Arguments parameter specifying the name of the string
        ''' property to validate</param>
        ''' <returns><see langword="false" /> if the rule is broken</returns>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
        Private Shared Function AfterOperationAssetAmountValidation(ByVal target As Object, _
            ByVal e As Validation.RuleArgs) As Boolean

            Dim valObj As OperationBackground = DirectCast(target, OperationBackground)

            If valObj._AfterOperationAssetAmount < 0 Then

                e.Description = My.Resources.Assets_OperationBackground_AmountInvalid
                e.Severity = Validation.RuleSeverity.Error
                Return False

            End If

            Return True

        End Function

        ''' <summary>
        ''' Rule ensuring that <see cref="AfterOperationAcquisitionAccountValue">AfterOperationAcquisitionAccountValue</see> is valid.
        ''' </summary>
        ''' <param name="target">Object containing the data to validate</param>
        ''' <param name="e">Arguments parameter specifying the name of the string
        ''' property to validate</param>
        ''' <returns><see langword="false" /> if the rule is broken</returns>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
        Private Shared Function AfterOperationAcquisitionAccountValueValidation(ByVal target As Object, _
            ByVal e As Validation.RuleArgs) As Boolean

            Dim valObj As OperationBackground = DirectCast(target, OperationBackground)

            Return BalanceAfterOperationValidation(valObj._AfterOperationAcquisitionAccountValue, _
                valObj._AfterOperationAssetAmount, e)

        End Function

        ''' <summary>
        ''' Rule ensuring that <see cref="AfterOperationAcquisitionAccountValuePerUnit">AfterOperationAcquisitionAccountValuePerUnit</see> is valid.
        ''' </summary>
        ''' <param name="target">Object containing the data to validate</param>
        ''' <param name="e">Arguments parameter specifying the name of the string
        ''' property to validate</param>
        ''' <returns><see langword="false" /> if the rule is broken</returns>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
        Private Shared Function AfterOperationAcquisitionAccountValuePerUnitValidation(ByVal target As Object, _
            ByVal e As Validation.RuleArgs) As Boolean

            Dim valObj As OperationBackground = DirectCast(target, OperationBackground)

            Return UnitValueValidation(valObj._AfterOperationAcquisitionAccountValuePerUnit, _
                valObj._AfterOperationAcquisitionAccountValue, valObj._AfterOperationAssetAmount, False, e)

        End Function

        ''' <summary>
        ''' Rule ensuring that <see cref="AfterOperationAmortizationAccountValue">AfterOperationAmortizationAccountValue</see> is valid.
        ''' </summary>
        ''' <param name="target">Object containing the data to validate</param>
        ''' <param name="e">Arguments parameter specifying the name of the string
        ''' property to validate</param>
        ''' <returns><see langword="false" /> if the rule is broken</returns>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
        Private Shared Function AfterOperationAmortizationAccountValueValidation(ByVal target As Object, _
            ByVal e As Validation.RuleArgs) As Boolean

            Dim valObj As OperationBackground = DirectCast(target, OperationBackground)

            Return BalanceAfterOperationValidation(valObj._AfterOperationAmortizationAccountValue, _
                valObj._AfterOperationAssetAmount, e)

        End Function

        ''' <summary>
        ''' Rule ensuring that <see cref="AfterOperationAcquisitionAccountValuePerUnit">AfterOperationAcquisitionAccountValuePerUnit</see> is valid.
        ''' </summary>
        ''' <param name="target">Object containing the data to validate</param>
        ''' <param name="e">Arguments parameter specifying the name of the string
        ''' property to validate</param>
        ''' <returns><see langword="false" /> if the rule is broken</returns>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
        Private Shared Function AfterOperationAmortizationAccountValuePerUnitValidation(ByVal target As Object, _
            ByVal e As Validation.RuleArgs) As Boolean

            Dim valObj As OperationBackground = DirectCast(target, OperationBackground)

            If Not UnitValueValidation(valObj._AfterOperationAmortizationAccountValuePerUnit, _
                valObj._AfterOperationAmortizationAccountValue, valObj._AfterOperationAssetAmount, False, e) Then
                Return False
            End If

            If valObj._AfterOperationAssetAmount > 0 AndAlso _
                CRound(valObj._AfterOperationAcquisitionAccountValuePerUnit _
                - valObj._AfterOperationAmortizationAccountValuePerUnit, 2) _
                < CRound(valObj._AssetLiquidationValue, 2) Then

                e.Description = My.Resources.Assets_OperationBackground_AcquisitionValueInvalid
                e.Severity = Validation.RuleSeverity.Error
                Return False

            End If

            Return True

        End Function

        ''' <summary>
        ''' Rule ensuring that either <see cref="AfterOperationValueDecreaseAccountValue">AfterOperationValueDecreaseAccountValue</see>
        ''' or <see cref="AfterOperationValueIncreaseAccountValue">AfterOperationValueIncreaseAccountValue</see> is zero.
        ''' See: 12 BAS para 51.
        ''' </summary>
        ''' <param name="target">Object containing the data to validate</param>
        ''' <param name="e">Arguments parameter specifying the name of the string
        ''' property to validate</param>
        ''' <returns><see langword="false" /> if the rule is broken</returns>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
        Private Shared Function AfterOperationValueDecreaseAccountValueValidation(ByVal target As Object, _
            ByVal e As Validation.RuleArgs) As Boolean

            Dim valObj As OperationBackground = DirectCast(target, OperationBackground)

            If Not BalanceAfterOperationValidation(valObj._AfterOperationValueDecreaseAccountValue, _
                valObj._AfterOperationAssetAmount, e) Then
                Return False
            End If

            If CRound(valObj._AfterOperationValueDecreaseAccountValue) > 0.0 AndAlso _
                CRound(valObj.AfterOperationValueIncreaseAccountValue) > 0.0 Then

                e.Description = My.Resources.Assets_OperationBackground_CannotIncreaseAndDecreaseSimultaniously
                e.Severity = Validation.RuleSeverity.Error
                Return False

            End If

            Return True

        End Function

        ''' <summary>
        ''' Rule ensuring that either <see cref="AfterOperationValueDecreaseAccountValuePerUnit">AfterOperationValueDecreaseAccountValuePerUnit</see>
        ''' or <see cref="AfterOperationValueIncreaseAccountValuePerUnit">AfterOperationValueIncreaseAccountValuePerUnit</see> is zero.
        ''' See: 12 BAS para 51.
        ''' </summary>
        ''' <param name="target">Object containing the data to validate</param>
        ''' <param name="e">Arguments parameter specifying the name of the string
        ''' property to validate</param>
        ''' <returns><see langword="false" /> if the rule is broken</returns>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
        Private Shared Function AfterOperationValueDecreaseAccountValuePerUnitValidation(ByVal target As Object, _
            ByVal e As Validation.RuleArgs) As Boolean

            Dim valObj As OperationBackground = DirectCast(target, OperationBackground)

            If Not UnitValueValidation(valObj._AfterOperationValueDecreaseAccountValuePerUnit, _
                valObj._AfterOperationValueDecreaseAccountValue, valObj._AfterOperationAssetAmount, False, e) Then
                Return False
            End If

            If CRound(valObj._AfterOperationValueDecreaseAccountValuePerUnit) > 0.0 AndAlso _
                CRound(valObj.AfterOperationValueIncreaseAccountValuePerUnit) > 0.0 Then

                e.Description = My.Resources.Assets_OperationBackground_CannotIncreaseAndDecreaseSimultaniously
                e.Severity = Validation.RuleSeverity.Error
                Return False

            End If

            Return True

        End Function

        ''' <summary>
        ''' Rule ensuring that <see cref="AfterOperationValueIncreaseAccountValue">AfterOperationValueIncreaseAccountValue</see> is valid.
        ''' See: 12 BAS para 51.
        ''' </summary>
        ''' <param name="target">Object containing the data to validate</param>
        ''' <param name="e">Arguments parameter specifying the name of the string
        ''' property to validate</param>
        ''' <returns><see langword="false" /> if the rule is broken</returns>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
        Private Shared Function AfterOperationValueIncreaseAccountValueValidation(ByVal target As Object, _
            ByVal e As Validation.RuleArgs) As Boolean

            Dim valObj As OperationBackground = DirectCast(target, OperationBackground)

            If Not BalanceAfterOperationValidation(valObj._AfterOperationValueIncreaseAccountValue, _
                valObj._AfterOperationAssetAmount, e) Then
                Return False
            End If

            If CRound(valObj._AfterOperationValueDecreaseAccountValue) > 0.0 AndAlso _
                CRound(valObj.AfterOperationValueIncreaseAccountValue) > 0.0 Then

                e.Description = My.Resources.Assets_OperationBackground_CannotIncreaseAndDecreaseSimultaniously
                e.Severity = Validation.RuleSeverity.Error
                Return False

            End If

            Return True

        End Function

        ''' <summary>
        ''' Rule ensuring that <see cref="AfterOperationValueIncreaseAccountValuePerUnit">AfterOperationValueIncreaseAccountValuePerUnit</see> is valid.
        ''' See: 12 BAS para 51.
        ''' </summary>
        ''' <param name="target">Object containing the data to validate</param>
        ''' <param name="e">Arguments parameter specifying the name of the string
        ''' property to validate</param>
        ''' <returns><see langword="false" /> if the rule is broken</returns>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
        Private Shared Function AfterOperationValueIncreaseAccountValuePerUnitValidation(ByVal target As Object, _
            ByVal e As Validation.RuleArgs) As Boolean

            Dim valObj As OperationBackground = DirectCast(target, OperationBackground)

            If Not UnitValueValidation(valObj._AfterOperationValueIncreaseAccountValuePerUnit, _
                valObj._AfterOperationValueIncreaseAccountValue, valObj._AfterOperationAssetAmount, False, e) Then
                Return False
            End If

            If CRound(valObj._AfterOperationValueDecreaseAccountValuePerUnit) > 0.0 AndAlso _
                CRound(valObj.AfterOperationValueIncreaseAccountValuePerUnit) > 0.0 Then

                e.Description = My.Resources.Assets_OperationBackground_CannotIncreaseAndDecreaseSimultaniously
                e.Severity = Validation.RuleSeverity.Error
                Return False

            End If

            Return True

        End Function

        ''' <summary>
        ''' Rule ensuring that <see cref="AfterOperationValueIncreaseAmortizationAccountValue">AfterOperationValueIncreaseAmortizationAccountValue</see> is valid.
        ''' See: 12 BAS para 51.
        ''' </summary>
        ''' <param name="target">Object containing the data to validate</param>
        ''' <param name="e">Arguments parameter specifying the name of the string
        ''' property to validate</param>
        ''' <returns><see langword="false" /> if the rule is broken</returns>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
        Private Shared Function AfterOperationValueIncreaseAmortizationAccountValueValidation(ByVal target As Object, _
            ByVal e As Validation.RuleArgs) As Boolean

            Dim valObj As OperationBackground = DirectCast(target, OperationBackground)

            If Not BalanceAfterOperationValidation(valObj._AfterOperationValueIncreaseAmortizationAccountValue, _
                valObj._AfterOperationAssetAmount, e) Then
                Return False
            End If

            If CRound(valObj._AfterOperationValueIncreaseAmortizationAccountValue) > _
                CRound(valObj.AfterOperationValueIncreaseAccountValue) Then

                e.Description = My.Resources.Assets_OperationBackground_RevaluedPortionAmortizationInvalid
                e.Severity = Validation.RuleSeverity.Error
                Return False

            End If

            Return True

        End Function

        ''' <summary>
        ''' Rule ensuring that <see cref="AfterOperationValueIncreaseAmortizationAccountValuePerUnit">AfterOperationValueIncreaseAmortizationAccountValuePerUnit</see> is valid.
        ''' See: 12 BAS para 51.
        ''' </summary>
        ''' <param name="target">Object containing the data to validate</param>
        ''' <param name="e">Arguments parameter specifying the name of the string
        ''' property to validate</param>
        ''' <returns><see langword="false" /> if the rule is broken</returns>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
        Private Shared Function AfterOperationValueIncreaseAmortizationAccountValuePerUnitValidation(ByVal target As Object, _
            ByVal e As Validation.RuleArgs) As Boolean

            Dim valObj As OperationBackground = DirectCast(target, OperationBackground)

            If Not UnitValueValidation(valObj._AfterOperationValueIncreaseAmortizationAccountValuePerUnit, _
                valObj._AfterOperationValueIncreaseAmortizationAccountValue, _
                valObj._AfterOperationAssetAmount, False, e) Then
                Return False
            End If

            If CRound(valObj._AfterOperationValueIncreaseAmortizationAccountValuePerUnit, ROUNDUNITASSET) > _
                CRound(valObj.AfterOperationValueIncreaseAccountValuePerUnit, ROUNDUNITASSET) Then

                e.Description = My.Resources.Assets_OperationBackground_RevaluedPortionAmortizationInvalid
                e.Severity = Validation.RuleSeverity.Error
                Return False

            End If

            Return True

        End Function

        ''' <summary>
        ''' Rule ensuring that <see cref="ChangeAcquisitionAccountValuePerUnit">ChangeAcquisitionAccountValuePerUnit</see> is valid.
        ''' </summary>
        ''' <param name="target">Object containing the data to validate</param>
        ''' <param name="e">Arguments parameter specifying the name of the string
        ''' property to validate</param>
        ''' <returns><see langword="false" /> if the rule is broken</returns>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
        Private Shared Function ChangeAcquisitionAccountValuePerUnitValidation(ByVal target As Object, _
            ByVal e As Validation.RuleArgs) As Boolean

            Dim valObj As OperationBackground = DirectCast(target, OperationBackground)

            Return UnitValueValidation(valObj._ChangeAcquisitionAccountValuePerUnit, _
                valObj._ChangeAcquisitionAccountValue, valObj._CurrentAssetAmount, True, e)

        End Function

        ''' <summary>
        ''' Rule ensuring that <see cref="ChangeAmortizationAccountValuePerUnit">ChangeAmortizationAccountValuePerUnit</see> is valid.
        ''' </summary>
        ''' <param name="target">Object containing the data to validate</param>
        ''' <param name="e">Arguments parameter specifying the name of the string
        ''' property to validate</param>
        ''' <returns><see langword="false" /> if the rule is broken</returns>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
        Private Shared Function ChangeAmortizationAccountValuePerUnitValidation(ByVal target As Object, _
            ByVal e As Validation.RuleArgs) As Boolean

            Dim valObj As OperationBackground = DirectCast(target, OperationBackground)

            Return UnitValueValidation(valObj._ChangeAmortizationAccountValuePerUnit, _
                valObj._ChangeAmortizationAccountValue, valObj._CurrentAssetAmount, True, e)

        End Function

        ''' <summary>
        ''' Rule ensuring that <see cref="ChangeValueDecreaseAccountValuePerUnit">ChangeValueDecreaseAccountValuePerUnit</see> is valid.
        ''' </summary>
        ''' <param name="target">Object containing the data to validate</param>
        ''' <param name="e">Arguments parameter specifying the name of the string
        ''' property to validate</param>
        ''' <returns><see langword="false" /> if the rule is broken</returns>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
        Private Shared Function ChangeValueDecreaseAccountValuePerUnitValidation(ByVal target As Object, _
            ByVal e As Validation.RuleArgs) As Boolean

            Dim valObj As OperationBackground = DirectCast(target, OperationBackground)

            Return UnitValueValidation(valObj._ChangeValueDecreaseAccountValuePerUnit, _
               valObj._ChangeValueDecreaseAccountValue, valObj._CurrentAssetAmount, True, e)

        End Function

        ''' <summary>
        ''' Rule ensuring that <see cref="ChangeValueIncreaseAccountValuePerUnit">ChangeValueIncreaseAccountValuePerUnit</see> is valid.
        ''' </summary>
        ''' <param name="target">Object containing the data to validate</param>
        ''' <param name="e">Arguments parameter specifying the name of the string
        ''' property to validate</param>
        ''' <returns><see langword="false" /> if the rule is broken</returns>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
        Private Shared Function ChangeValueIncreaseAccountValuePerUnitValidation(ByVal target As Object, _
            ByVal e As Validation.RuleArgs) As Boolean

            Dim valObj As OperationBackground = DirectCast(target, OperationBackground)

            Return UnitValueValidation(valObj._ChangeValueIncreaseAccountValuePerUnit, _
               valObj._ChangeValueIncreaseAccountValue, valObj._CurrentAssetAmount, True, e)

        End Function

        ''' <summary>
        ''' Rule ensuring that <see cref="ChangeValueIncreaseAmortizationAccountValuePerUnit">ChangeValueIncreaseAmortizationAccountValuePerUnit</see> is valid.
        ''' </summary>
        ''' <param name="target">Object containing the data to validate</param>
        ''' <param name="e">Arguments parameter specifying the name of the string
        ''' property to validate</param>
        ''' <returns><see langword="false" /> if the rule is broken</returns>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
        Private Shared Function ChangeValueIncreaseAmortizationAccountValuePerUnitValidation(ByVal target As Object, _
            ByVal e As Validation.RuleArgs) As Boolean

            Dim valObj As OperationBackground = DirectCast(target, OperationBackground)

            Return UnitValueValidation(valObj._ChangeValueIncreaseAmortizationAccountValuePerUnit, _
               valObj._ChangeValueIncreaseAmortizationAccountValue, valObj._CurrentAssetAmount, True, e)

        End Function

        Private Shared Function UnitValueValidation(ByVal unitValue As Double, _
            ByVal totalValue As Double, ByVal amount As Integer, ByVal isValueChange As Boolean, _
            ByVal e As Validation.RuleArgs) As Boolean

            If Not isValueChange AndAlso CRound(unitValue, ROUNDUNITASSET) < 0.0 Then

                e.Description = My.Resources.Assets_OperationBackground_BalanceInvalid
                e.Severity = Validation.RuleSeverity.Error
                Return False

            End If

            If amount = 1 AndAlso CRound(totalValue, 2) <> CRound(unitValue, 2) Then

                e.Description = My.Resources.Assets_OperationBackground_UnitValueInvalidForSingleUnit
                e.Severity = Validation.RuleSeverity.Error
                Return False

            ElseIf amount > 1 AndAlso Math.Abs(CRound(totalValue, 2) _
                - CRound(amount * unitValue, 2)) > UnitRoundTolerance Then

                e.Description = String.Format(My.Resources.Assets_OperationBackground_UnitValueInvalid, _
                    DblParser(UnitRoundTolerance, 2))
                e.Severity = Validation.RuleSeverity.Error
                Return False

            ElseIf amount < 1 AndAlso CRound(unitValue, ROUNDUNITASSET) <> 0.0 Then

                If isValueChange Then
                    e.Description = My.Resources.Assets_OperationBackground_UnitValueChangeInvalid
                Else
                    e.Description = My.Resources.Assets_OperationBackground_NullBalanceRequired
                End If
                e.Severity = Validation.RuleSeverity.Error
                Return False

            End If

            Return True

        End Function

        Private Shared Function BalanceAfterOperationValidation(ByVal balance As Double, _
            ByVal amount As Integer, ByVal e As Validation.RuleArgs) As Boolean

            If CRound(balance, 2) < 0 Then

                e.Description = My.Resources.Assets_OperationBackground_BalanceInvalid
                e.Severity = Validation.RuleSeverity.Error
                Return False

            ElseIf amount < 1 AndAlso CRound(balance, 2) > 0 Then

                e.Description = My.Resources.Assets_OperationBackground_NullBalanceRequired
                e.Severity = Validation.RuleSeverity.Error
                Return False

            End If

            Return True

        End Function

#End Region

#Region " Authorization Rules "

        Protected Overrides Sub AddAuthorizationRules()

        End Sub

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets a new OperationBackground instance for a new long term asset operation with the specified asset.
        ''' </summary>
        ''' <param name="nAssetID">An ID of the asset for which the data should be fetched.</param>
        ''' <remarks></remarks>
        Friend Shared Function NewOperationBackgroundChild(ByVal nAssetID As Integer) As OperationBackground
            Return New OperationBackground(nAssetID, Nothing, Nothing)
        End Function

        ''' <summary>
        ''' Gets a new OperationBackground instance for an existing long term asset operation.
        ''' </summary>
        ''' <param name="nAssetId">An ID of the asset for which the data should be fetched.</param>
        ''' <param name="nOperationID">An ID of the long term asset operation for which the data should be fetched.</param>
        ''' <param name="nOperationDate">A date of the long term asset operation for which the data should be fetched.</param>
        ''' <remarks>After the background data is fetched, the following actions should be taken:
        ''' 1. Set <see cref="DisableCalculations">DisableCalculations</see> to TRUE;
        ''' 2. Set state delta properties ('Change') with the operation data;
        ''' 3. Invoke <see cref="InitializeOldData">InitializeOldData</see> 
        ''' with the operation date as param;
        ''' 4. Set <see cref="DisableCalculations">DisableCalculations</see> to FALSE.</remarks>
        Friend Shared Function GetOperationBackgroundChild(ByVal nAssetId As Integer, _
            ByVal nOperationID As Integer, ByVal nOperationDate As Date) As OperationBackground
            Return New OperationBackground(nAssetId, nOperationID, nOperationDate, _
                Nothing, Nothing)
        End Function

        ''' <summary>
        ''' Gets a new OperationBackground instance for an existing long term asset operation.
        ''' </summary>
        ''' <param name="nAssetId">An ID of the asset for which the data should be fetched.</param>
        ''' <param name="nOperationID">An ID of the long term asset operation for which the data should be fetched.</param>
        ''' <param name="nOperationDate">A date of the long term asset operation for which the data should be fetched.</param>
        ''' <param name="generalSource">Datatable containing general asset data.</param>
        ''' <param name="deltaSource">Datatable containing 
        ''' <see cref="OperationDelta">asset operation delta values</see>.</param>
        ''' <remarks>After the background data is fetched, the following actions should be taken:
        ''' 1. Set <see cref="DisableCalculations">DisableCalculations</see> to TRUE;
        ''' 2. Set state delta properties ('Change') with the operation data;
        ''' 3. Invoke <see cref="InitializeOldData">InitializeOldData</see> 
        ''' with the operation date as param;
        ''' 4. Set <see cref="DisableCalculations">DisableCalculations</see> to FALSE.</remarks>
        Friend Shared Function GetOperationBackgroundChild(ByVal nAssetId As Integer, _
            ByVal nOperationID As Integer, ByVal nOperationDate As Date, _
            ByVal generalSource As DataTable, ByVal deltaSource As DataTable) As OperationBackground
            Return New OperationBackground(nAssetId, nOperationID, nOperationDate, _
                generalSource, deltaSource)
        End Function

        ''' <summary>
        ''' Gets a OperationBackground general datasource for an existing 
        ''' complex long term asset operation.
        ''' </summary>
        ''' <param name="complexOperationId">An ID of the long term asset complex operation 
        ''' for which the data should be fetched.</param>
        ''' <remarks></remarks>
        Friend Shared Function GetDataSourceGeneral(ByVal complexOperationId As Integer) As DataTable
            Dim myComm As New SQLCommand("")
            myComm.AddParam("?CD", complexOperationId)
            Return myComm.Fetch()
        End Function

        ''' <summary>
        ''' Gets a OperationBackground delta datasource for an existing 
        ''' complex long term asset operation.
        ''' </summary>
        ''' <param name="complexOperationId">An ID of the long term asset complex operation 
        ''' for which the data should be fetched.</param>
        ''' <remarks></remarks>
        Friend Shared Function GetDataSourceDelta(ByVal complexOperationId As Integer) As DataTable
            Dim myComm As New SQLCommand("")
            myComm.AddParam("?CD", complexOperationId)
            Return myComm.Fetch()
        End Function


        Private Sub New()
            ' require use of factory methods
            MarkAsChild()
        End Sub

        Private Sub New(ByVal nAssetID As Integer, ByVal generalSource As DataTable, _
            ByVal deltaSource As DataTable)
            MarkAsChild()
            Create(nAssetID, generalSource, deltaSource)
        End Sub

        Private Sub New(ByVal nAssetId As Integer, ByVal nOperationID As Integer, _
            ByVal nOperationDate As Date, ByVal generalSource As DataTable, _
            ByVal deltaSource As DataTable)
            MarkAsChild()
            Fetch(nAssetId, nOperationID, nOperationDate, generalSource, deltaSource)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Create(ByVal nAssetID As Integer, ByVal generalSource As DataTable, _
            ByVal deltaSource As DataTable)
            GetGeneralData(nAssetID, generalSource)
            _DeltaList = OperationDeltaList.GetList(nAssetID, 0, deltaSource)
            CalculateCurrentProperties(False)
        End Sub

        Private Sub Fetch(ByVal nAssetId As Integer, ByVal nOperationID As Integer, _
            ByVal nOperationDate As Date, ByVal generalSource As DataTable, _
            ByVal deltaSource As DataTable)

            GetGeneralData(nAssetId, generalSource)
            _CurrentDate = nOperationDate
            _DeltaList = OperationDeltaList.GetList(nAssetId, nOperationID, deltaSource)

            MarkOld()

        End Sub


        Private Sub GetGeneralData(ByVal nAssetID As Integer)

            Dim myComm As New SQLCommand("FetchLongTermAssetOperationBackgroundInfo")
            myComm.AddParam("?TD", nAssetID)
            myComm.AddParam("?OD", 0)
            myComm.AddParam("?DT", Today.AddYears(100))

            Using myData As DataTable = myComm.Fetch
                GetGeneralData(nAssetID, myData)
            End Using

        End Sub

        Private Sub GetGeneralData(ByVal nAssetID As Integer, ByVal myData As DataTable)

            If myData Is Nothing Then
                GetGeneralData(nAssetID)
                Exit Sub
            End If

            Dim isFound As Boolean = False

            For Each dr As DataRow In myData.Rows
                If CIntSafe(dr.Item(0), 0) = nAssetID Then
                    GetGeneralData(dr)
                    isFound = True
                    Exit For
                End If
            Next

            If Not isFound Then
                Throw New Exception(String.Format(My.Resources.Common_ObjectNotFound, _
                    My.Resources.Assets_OperationBackground_TypeName, nAssetID.ToString()))
            End If

        End Sub

        Private Sub GetGeneralData(ByVal dr As DataRow)

            _AssetID = CIntSafe(dr.Item(0))
            _AssetName = CStrSafe(dr.Item(0)).Trim
            _AssetMeasureUnit = CStrSafe(dr.Item(1)).Trim
            _InitialAssetAcquiredAccount = CLongSafe(dr.Item(2))
            _InitialAssetContraryAccount = CLongSafe(dr.Item(3))
            _InitialAssetValueDecreaseAccount = CLongSafe(dr.Item(4), 0)
            _InitialAssetValueIncreaseAccount = CLongSafe(dr.Item(5))
            _InitialAssetValueIncreaseAmortizationAccount = CLongSafe(dr.Item(6), 0)
            _AssetDateAcquired = CDate(dr.Item(7))
            _AssetAquisitionID = CIntSafe(dr.Item(8), 0)
            _AssetLiquidationValue = CDblSafe(dr.Item(9), 2, 0)
            _CurrentUsageStatus = ConvertDbBoolean(CIntSafe(dr.Item(10), 0))
            _CurrentAssetAmount = CIntSafe(dr.Item(11), 0)
            _CurrentAcquisitionAccountValuePerUnit = CDblSafe(dr.Item(12), ROUNDUNITASSET, 0)
            _CurrentAcquisitionAccountValue = CDblSafe(dr.Item(13), 2, 0)
            If CDbl(dr.Item(14)) < 0 Then
                _CurrentValueDecreaseAccountValuePerUnit = -CDblSafe(dr.Item(14), ROUNDUNITASSET, 0)
                _CurrentValueIncreaseAccountValuePerUnit = 0
            ElseIf CDbl(dr.Item(14)) > 0 Then
                _CurrentValueDecreaseAccountValuePerUnit = 0
                _CurrentValueIncreaseAccountValuePerUnit = CDblSafe(dr.Item(14), ROUNDUNITASSET, 0)
            Else
                _CurrentValueDecreaseAccountValuePerUnit = 0
                _CurrentValueIncreaseAccountValuePerUnit = 0
            End If
            If CDbl(dr.Item(15)) < 0 Then
                _CurrentValueDecreaseAccountValue = -CDblSafe(dr.Item(15), 2, 0)
                _CurrentValueIncreaseAccountValue = 0
            ElseIf CDbl(dr.Item(15)) > 0 Then
                _CurrentValueDecreaseAccountValue = 0
                _CurrentValueIncreaseAccountValue = CDblSafe(dr.Item(15), 2, 0)
            Else
                _CurrentValueDecreaseAccountValue = 0
                _CurrentValueIncreaseAccountValue = 0
            End If
            _CurrentAmortizationAccountValuePerUnit = CDblSafe(dr.Item(16), ROUNDUNITASSET, 0)
            _CurrentAmortizationAccountValue = CDblSafe(dr.Item(17), 2, 0)
            _CurrentValueIncreaseAmortizationAccountValuePerUnit = CDblSafe(dr.Item(18), ROUNDUNITASSET, 0)
            _CurrentValueIncreaseAmortizationAccountValue = CDblSafe(dr.Item(19), 2, 0)
            _CurrentAmortizationPeriod = CIntSafe(dr.Item(20), 0)
            _CurrentUsageTermMonths = CIntSafe(dr.Item(21), 0)
            _CurrentAssetAmount = _CurrentAssetAmount + CIntSafe(dr.Item(22), 0)
            _CurrentAcquisitionAccountValue = _
                CRound(_CurrentAcquisitionAccountValue + CDblSafe(dr.Item(27), 2, 0))
            _CurrentAcquisitionAccountValuePerUnit = _
                CRound(_CurrentAcquisitionAccountValuePerUnit + _
                CDblSafe(dr.Item(28), ROUNDUNITASSET, 0), ROUNDUNITASSET)
            _CurrentAmortizationAccountValue = _
                CRound(_CurrentAmortizationAccountValue + CDblSafe(dr.Item(29), 2, 0))
            _CurrentAmortizationAccountValuePerUnit = _
                CRound(_CurrentAmortizationAccountValuePerUnit _
                + CDblSafe(dr.Item(30), ROUNDUNITASSET, 0), ROUNDUNITASSET)
            _CurrentValueDecreaseAccountValue = CRound(_CurrentValueDecreaseAccountValue + _
                CDblSafe(dr.Item(31), 2, 0))
            _CurrentValueDecreaseAccountValuePerUnit = _
                CRound(_CurrentValueDecreaseAccountValuePerUnit _
                + CDblSafe(dr.Item(32), ROUNDUNITASSET, 0), ROUNDUNITASSET)
            _CurrentValueIncreaseAccountValue = _
                CRound(_CurrentValueIncreaseAccountValue + CDblSafe(dr.Item(33), 2, 0))
            _CurrentValueIncreaseAccountValuePerUnit = _
                CRound(_CurrentValueIncreaseAccountValuePerUnit _
                + CDblSafe(dr.Item(34), ROUNDUNITASSET, 0), ROUNDUNITASSET)
            _CurrentValueIncreaseAmortizationAccountValue = _
                CRound(_CurrentValueIncreaseAmortizationAccountValue + CDblSafe(dr.Item(35), 2, 0))
            _CurrentValueIncreaseAmortizationAccountValuePerUnit = _
                CRound(_CurrentValueIncreaseAmortizationAccountValuePerUnit _
                + CDblSafe(dr.Item(36), ROUNDUNITASSET, 0), ROUNDUNITASSET)
            _CurrentUsageTermMonths = _CurrentUsageTermMonths + CIntSafe(dr.Item(37), 0)

            If (Math.Floor(CIntSafe(dr.Item(38), 0) / 2) <> Math.Ceiling(CIntSafe(dr.Item(38), 0) / 2)) Then _
                _CurrentUsageStatus = Not _CurrentUsageStatus

            _CurrentAssetValue = CRound(_CurrentAcquisitionAccountValue + _
                _CurrentAmortizationAccountValue + _CurrentValueDecreaseAccountValue)
            _CurrentAssetValuePerUnit = CRound(_CurrentAcquisitionAccountValuePerUnit + _
                _CurrentAmortizationAccountValuePerUnit + _CurrentValueDecreaseAccountValuePerUnit, ROUNDUNITASSET)
            If _CurrentValueDecreaseAccountValue > 0 Then
                _CurrentAssetValueRevaluedPortion = -CRound(_CurrentValueDecreaseAccountValue)
                _CurrentAssetValueRevaluedPortionPerUnit = _
                    -CRound(_CurrentValueDecreaseAccountValuePerUnit, ROUNDUNITASSET)
            ElseIf _CurrentValueIncreaseAccountValue > 0 Then
                _CurrentAssetValueRevaluedPortion = CRound(_CurrentValueIncreaseAccountValue _
                    + _CurrentValueIncreaseAmortizationAccountValue)
                _CurrentAssetValueRevaluedPortionPerUnit = CRound(_CurrentValueIncreaseAccountValuePerUnit _
                    + _CurrentValueIncreaseAmortizationAccountValuePerUnit, ROUNDUNITASSET)
            Else
                _CurrentAssetValueRevaluedPortion = 0
                _CurrentAssetValueRevaluedPortionPerUnit = 0
            End If

        End Sub

#End Region

    End Class

End Namespace