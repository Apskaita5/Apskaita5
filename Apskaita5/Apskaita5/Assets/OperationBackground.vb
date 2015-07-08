Namespace Assets

    ''' <summary>
    ''' Represents a helper object that contains information about a long term asset state 
    ''' before and after an operation (aggregated information).
    ''' </summary>
    ''' <remarks>Should only be used as a child of a long term asset operation.</remarks>
    <Serializable()> _
    Public Class OperationBackground
        Inherits BusinessBase(Of OperationBackground)

#Region " Business Methods "

        Private _DisableCalculations As Boolean = False
        Private _AssetID As Integer = 0
        Private _AssetName As String = ""
        Private _AssetMeasureUnit As String = ""
        Private _AssetAcquiredAccount As Long = 0
        Private _AssetContraryAccount As Long = 0
        Private _AssetValueDecreaseAccount As Long = 0
        Private _AssetValueIncreaseAccount As Long = 0
        Private _AssetValueIncreaseAmortizationAccount As Long = 0
        Private _AssetDateAcquired As Date = Today
        Private _AssetAquisitionID As Integer = 0
        Private _AssetLiquidationValue As Double = 0
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


        ''' <summary>
        ''' Whether to disable calculation of "after operation" properties on PropertyHasChanged.
        ''' </summary>
        ''' <remarks></remarks>
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
        ''' An <see cref="LongTermAsset.AccountAcquisition">acquisition account of the long term asset</see>.
        ''' </summary>
        ''' <remarks>12 BAS para 12.</remarks>
        Public ReadOnly Property AssetAcquiredAccount() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AssetAcquiredAccount
            End Get
        End Property

        ''' <summary>
        ''' An <see cref="LongTermAsset.AccountAccumulatedAmortization">amortization account of the long term asset</see>.
        ''' </summary>
        ''' <remarks>12 BAS para 68.</remarks>
        Public ReadOnly Property AssetContraryAccount() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AssetContraryAccount
            End Get
        End Property

        ''' <summary>
        ''' A <see cref="LongTermAsset.AccountValueDecrease">value decrease account of the long term asset</see>.
        ''' </summary>
        ''' <remarks>12 BAS para 49.</remarks>
        Public ReadOnly Property AssetValueDecreaseAccount() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AssetValueDecreaseAccount
            End Get
        End Property

        ''' <summary>
        ''' A <see cref="LongTermAsset.AccountValueDecrease">value increase account of the long term asset</see>.
        ''' </summary>
        ''' <remarks>12 BAS para 48.</remarks>
        Public ReadOnly Property AssetValueIncreaseAccount() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AssetValueIncreaseAccount
            End Get
        End Property

        ''' <summary>
        ''' An <see cref="LongTermAsset.AccountValueDecrease">amortization account of the long term asset 
        ''' revalued portion (accounted in the <see cref="AssetValueIncreaseAccount">AssetValueIncreaseAccount</see>)</see>.
        ''' </summary>
        ''' <remarks>12 BAS para 48.</remarks>
        Public ReadOnly Property AssetValueIncreaseAmortizationAccount() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AssetValueIncreaseAmortizationAccount
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

        ''' <summary>
        ''' A current balance for the <see cref="AssetAcquiredAccount">AssetAcquiredAccount</see>.
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
        ''' A current balance for the <see cref="AssetAcquiredAccount">AssetAcquiredAccount</see> per unit.
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
        ''' A current balance for the <see cref="AssetContraryAccount">AssetContraryAccount</see>.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property CurrentAmortizationAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_CurrentAmortizationAccountValue)
            End Get
        End Property

        ''' <summary>
        ''' A current balance for the <see cref="AssetContraryAccount">AssetContraryAccount</see> per unit.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property CurrentAmortizationAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_CurrentAmortizationAccountValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        ''' <summary>
        ''' A current balance for the <see cref="AssetValueDecreaseAccount">AssetValueDecreaseAccount</see>.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property CurrentValueDecreaseAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_CurrentValueDecreaseAccountValue)
            End Get
        End Property

        ''' <summary>
        ''' A current balance for the <see cref="AssetValueDecreaseAccount">AssetValueDecreaseAccount</see> per unit.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property CurrentValueDecreaseAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_CurrentValueDecreaseAccountValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        ''' <summary>
        ''' A current balance for the <see cref="AssetValueIncreaseAccount">AssetValueIncreaseAccount</see>.
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
        ''' A current balance for the <see cref="AssetValueIncreaseAccount">AssetValueIncreaseAccount</see> per unit.
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
        ''' A current balance for the <see cref="AssetValueIncreaseAmortizationAccount">AssetValueIncreaseAmortizationAccount</see>.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property CurrentValueIncreaseAmortizationAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_CurrentValueIncreaseAmortizationAccountValue)
            End Get
        End Property

        ''' <summary>
        ''' A current balance for the <see cref="AssetValueIncreaseAmortizationAccount">AssetValueIncreaseAmortizationAccount</see> per unit.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.</remarks>
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
        ''' <remarks><see cref="AssetValueIncreaseAccount">AssetValueIncreaseAccount</see> 
        ''' minus <see cref="AssetValueIncreaseAmortizationAccount">AssetValueIncreaseAmortizationAccount</see></remarks>
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
        ''' <remarks><see cref="AssetValueIncreaseAccount">AssetValueIncreaseAccount</see> 
        ''' minus <see cref="AssetValueIncreaseAmortizationAccount">AssetValueIncreaseAmortizationAccount</see>
        ''' divided by the <see cref="CurrentAssetAmmount">CurrentAssetAmmount</see></remarks>
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


        ''' <summary>
        ''' A change of the amount of the long term asset made by the operation.
        ''' </summary>
        ''' <remarks></remarks>
        <IntegerField(ValueRequiredLevel.Optional, True, True, Integer.MinValue, 0, True)> _
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
        ''' A change of balance for the <see cref="AssetAcquiredAccount">AssetAcquiredAccount</see> made by the operation.
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
        ''' A change of balance for the <see cref="AssetAcquiredAccount">AssetAcquiredAccount</see> per unit made by the operation.
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
        ''' A change of balance for the <see cref="AssetContraryAccount">AssetContraryAccount</see> made by the operation.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.</remarks>
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
        ''' A change of balance for the <see cref="AssetContraryAccount">AssetContraryAccount</see> per unit made by the operation.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.</remarks>
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
        ''' A change of balance for the <see cref="AssetValueDecreaseAccount">AssetValueDecreaseAccount</see> made by the operation.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.</remarks>
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
        ''' A change of balance for the <see cref="AssetValueDecreaseAccount">AssetValueDecreaseAccount</see> per unit made by the operation.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.</remarks>
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
        ''' A change of balance for the <see cref="AssetValueIncreaseAccount">AssetValueIncreaseAccount</see> made by the operation.
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
        ''' A change of balance for the <see cref="AssetValueIncreaseAccount">AssetValueIncreaseAccount</see> per unit made by the operation.
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
        ''' A change of balance for the <see cref="AssetValueIncreaseAmortizationAccount">AssetValueIncreaseAmortizationAccount</see> made by the operation.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.</remarks>
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
        ''' A change of balance for the <see cref="AssetValueIncreaseAmortizationAccount">AssetValueIncreaseAmortizationAccount</see> per unit made by the operation.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.</remarks>
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


        ''' <summary>
        ''' A balance for the <see cref="AssetAcquiredAccount">AssetAcquiredAccount</see> after the operation.
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
        ''' A balance for the <see cref="AssetAcquiredAccount">AssetAcquiredAccount</see> per unit after the operation.
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
        ''' A balance for the <see cref="AssetContraryAccount">AssetContraryAccount</see> after the operation.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2, True, Double.MinValue, 0, True)> _
        Public ReadOnly Property AfterOperationAmortizationAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AfterOperationAmortizationAccountValue)
            End Get
        End Property

        ''' <summary>
        ''' A balance for the <see cref="AssetContraryAccount">AssetContraryAccount</see> per unit after the operation.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET, True, Double.MinValue, 0, True)> _
        Public ReadOnly Property AfterOperationAmortizationAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AfterOperationAmortizationAccountValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        ''' <summary>
        ''' A balance for the <see cref="AssetValueDecreaseAccount">AssetValueDecreaseAccount</see> after the operation.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2, True, Double.MinValue, 0, True)> _
        Public ReadOnly Property AfterOperationValueDecreaseAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AfterOperationValueDecreaseAccountValue)
            End Get
        End Property

        ''' <summary>
        ''' A balance for the <see cref="AssetValueDecreaseAccount">AssetValueDecreaseAccount</see> per unit after the operation.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET, True, Double.MinValue, 0, True)> _
        Public ReadOnly Property AfterOperationValueDecreaseAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AfterOperationValueDecreaseAccountValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        ''' <summary>
        ''' A balance for the <see cref="AssetValueIncreaseAccount">AssetValueIncreaseAccount</see> after the operation.
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
        ''' A balance for the <see cref="AssetValueIncreaseAccount">AssetValueIncreaseAccount</see> per unit after the operation.
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
        ''' A balance for the <see cref="AssetValueIncreaseAmortizationAccount">AssetValueIncreaseAmortizationAccount</see> after the operation.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2, True, Double.MinValue, 0, True)> _
        Public ReadOnly Property AfterOperationValueIncreaseAmortizationAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AfterOperationValueIncreaseAmortizationAccountValue)
            End Get
        End Property

        ''' <summary>
        ''' A balance for the <see cref="AssetValueIncreaseAmortizationAccount">AssetValueIncreaseAmortizationAccount</see> per unit after the operation.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.</remarks>
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


        Friend Sub CalculateAfterOperationProperties(ByVal raisePropertyHasChanged As Boolean)

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

            _AfterOperationAssetValue = CRound(_AfterOperationAcquisitionAccountValue + _AfterOperationAmortizationAccountValue _
                + _AfterOperationValueDecreaseAccountValue, 2)
            _AfterOperationAssetValuePerUnit = CRound(_AfterOperationAcquisitionAccountValuePerUnit + _AfterOperationAmortizationAccountValuePerUnit _
                + _AfterOperationValueDecreaseAccountValuePerUnit, ROUNDUNITASSET)

            If _AfterOperationValueDecreaseAccountValue > 0 Then
                _AfterOperationAssetValueRevaluedPortion = CRound(_AfterOperationValueDecreaseAccountValue)
                _AfterOperationAssetValueRevaluedPortionPerUnit = _
                    CRound(_AfterOperationValueDecreaseAccountValuePerUnit, ROUNDUNITASSET)
            ElseIf _CurrentValueIncreaseAccountValue > 0 Then
                _AfterOperationAssetValueRevaluedPortion = CRound(_AfterOperationValueIncreaseAccountValue _
                    + _AfterOperationValueIncreaseAmortizationAccountValue)
                _AfterOperationAssetValueRevaluedPortionPerUnit = CRound(_AfterOperationValueIncreaseAccountValuePerUnit _
                    + _AfterOperationValueIncreaseAmortizationAccountValuePerUnit, ROUNDUNITASSET)
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

            ValidationRules.AddRule(AddressOf CommonValidation.DoubleFieldValidation, _
                New Csla.Validation.RuleArgs("AfterOperationAcquisitionAccountValue"))
            ValidationRules.AddRule(AddressOf CommonValidation.DoubleFieldValidation, _
                New Csla.Validation.RuleArgs("AfterOperationAcquisitionAccountValuePerUnit"))
            ValidationRules.AddRule(AddressOf CommonValidation.DoubleFieldValidation, _
                New Csla.Validation.RuleArgs("AfterOperationAmortizationAccountValue"))
            ValidationRules.AddRule(AddressOf CommonValidation.DoubleFieldValidation, _
                New Csla.Validation.RuleArgs("AfterOperationAmortizationAccountValuePerUnit"))
            ValidationRules.AddRule(AddressOf CommonValidation.DoubleFieldValidation, _
                New Csla.Validation.RuleArgs("AfterOperationValueDecreaseAccountValue"))
            ValidationRules.AddRule(AddressOf CommonValidation.DoubleFieldValidation, _
                New Csla.Validation.RuleArgs("AfterOperationValueDecreaseAccountValuePerUnit"))
            ValidationRules.AddRule(AddressOf CommonValidation.DoubleFieldValidation, _
                New Csla.Validation.RuleArgs("AfterOperationValueIncreaseAccountValue"))
            ValidationRules.AddRule(AddressOf CommonValidation.DoubleFieldValidation, _
                New Csla.Validation.RuleArgs("AfterOperationValueIncreaseAccountValuePerUnit"))
            ValidationRules.AddRule(AddressOf CommonValidation.DoubleFieldValidation, _
                New Csla.Validation.RuleArgs("AfterOperationValueIncreaseAmortizationAccountValue"))
            ValidationRules.AddRule(AddressOf CommonValidation.DoubleFieldValidation, _
                New Csla.Validation.RuleArgs("AfterOperationValueIncreaseAmortizationAccountValuePerUnit"))
            ValidationRules.AddRule(AddressOf CommonValidation.DoubleFieldValidation, _
                New Csla.Validation.RuleArgs("AfterOperationAssetValue"))
            ValidationRules.AddRule(AddressOf CommonValidation.DoubleFieldValidation, _
                New Csla.Validation.RuleArgs("AfterOperationAssetValuePerUnit"))
            ValidationRules.AddRule(AddressOf CommonValidation.DoubleFieldValidation, _
                New Csla.Validation.RuleArgs("AfterOperationAssetValueRevaluedPortion"))
            ValidationRules.AddRule(AddressOf CommonValidation.DoubleFieldValidation, _
                New Csla.Validation.RuleArgs("AfterOperationAssetValueRevaluedPortionPerUnit"))
            ValidationRules.AddRule(AddressOf CommonValidation.IntegerFieldValidation, _
                New Csla.Validation.RuleArgs("AfterOperationAssetAmount"))
            ValidationRules.AddRule(AddressOf CommonValidation.IntegerFieldValidation, _
                New Csla.Validation.RuleArgs("ChangeAssetAmount"))

            ValidationRules.AddRule(AddressOf AfterOperationValueDecreaseAccountValueValidation, _
                New Csla.Validation.RuleArgs("AfterOperationValueDecreaseAccountValue"))
            ValidationRules.AddRule(AddressOf AfterOperationValueDecreaseAccountValuePerUnitValidation, _
                New Csla.Validation.RuleArgs("AfterOperationValueDecreaseAccountValuePerUnit"))

            ValidationRules.AddDependantProperty("AfterOperationValueIncreaseAccountValue", _
                "AfterOperationValueDecreaseAccountValue", False)
            ValidationRules.AddDependantProperty("AfterOperationValueIncreaseAccountValuePerUnit", _
                "AfterOperationValueDecreaseAccountValuePerUnit", False)

        End Sub

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

            If CRound(valObj._AfterOperationValueDecreaseAccountValue) <> 0.0 AndAlso _
                CRound(valObj.AfterOperationValueIncreaseAccountValue) <> 0.0 Then

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

            If CRound(valObj._AfterOperationValueDecreaseAccountValuePerUnit) <> 0.0 AndAlso _
                CRound(valObj.AfterOperationValueIncreaseAccountValuePerUnit) <> 0.0 Then

                e.Description = My.Resources.Assets_OperationBackground_CannotIncreaseAndDecreaseSimultaniously
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
            Return New OperationBackground(nAssetID)
        End Function

        ''' <summary>
        ''' Gets a new OperationBackground instance for an existing long term asset operation.
        ''' </summary>
        ''' <param name="nAssetId">An ID of the asset for which the data should be fetched.</param>
        ''' <param name="nOperationID">An ID of the long term asset operation for which the data should be fetched.</param>
        ''' <param name="nOperationDate">A date of the long term asset operation for which the data should be fetched.</param>
        ''' <remarks>After the background data is fetched, the following actions should be taken:
        ''' - set <see cref="DisableCalculations">DisableCalculations</see> property to true;
        ''' - set the "Change" properties with the operation data;
        ''' - invoke <see cref="CalculateAfterOperationProperties">CalculateAfterOperationProperties</see> with
        ''' a raisePropertyHasChanged parameter set to false;
        ''' - set <see cref="DisableCalculations">DisableCalculations</see> property to false.</remarks>
        Friend Shared Function GetOperationBackgroundChild(ByVal nAssetId As Integer, _
            ByVal nOperationID As Integer, ByVal nOperationDate As Date) As OperationBackground
            Return New OperationBackground(nAssetId, nOperationID, nOperationDate)
        End Function


        Private Sub New()
            ' require use of factory methods
            MarkAsChild()
        End Sub

        Private Sub New(ByVal nAssetID As Integer)
            MarkAsChild()
            Create(nAssetID)
        End Sub

        Private Sub New(ByVal nAssetId As Integer, ByVal nOperationID As Integer, ByVal nOperationDate As Date)
            MarkAsChild()
            Fetch(nAssetId, nOperationID, nOperationDate)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Create(ByVal nAssetID As Integer)
            Fetch(nAssetID, 0, Today.AddYears(100))
            CalculateAfterOperationProperties(False)
        End Sub

        Private Sub Fetch(ByVal nAssetId As Integer, ByVal nOperationID As Integer, ByVal nOperationDate As Date)

            Dim myComm As New SQLCommand("FetchLongTermAssetOperationBackgroundInfo")
            myComm.AddParam("?TD", nAssetId)
            myComm.AddParam("?OD", nOperationID)
            myComm.AddParam("?DT", nOperationDate)

            Using myData As DataTable = myComm.Fetch

                If myData.Rows.Count < 1 Then Throw New Exception(String.Format( _
                    My.Resources.Common_ObjectNotFound, My.Resources.Assets_OperationBackground_TypeName, _
                    nAssetId.ToString()))

                _AssetID = nAssetId

                Dim dr As DataRow = myData.Rows(0)

                _AssetName = CStrSafe(dr.Item(0)).Trim
                _AssetMeasureUnit = CStrSafe(dr.Item(1)).Trim
                _AssetAcquiredAccount = CLongSafe(dr.Item(2))
                _AssetContraryAccount = CLongSafe(dr.Item(3))
                _AssetValueDecreaseAccount = CLongSafe(dr.Item(4), 0)
                _AssetValueIncreaseAccount = CLongSafe(dr.Item(5))
                _AssetValueIncreaseAmortizationAccount = CLongSafe(dr.Item(6), 0)
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

            End Using

            _CurrentAssetValue = CRound(_CurrentAcquisitionAccountValue + _
                _CurrentAmortizationAccountValue + _CurrentValueDecreaseAccountValue)
            _CurrentAssetValuePerUnit = CRound(_CurrentAcquisitionAccountValuePerUnit + _
                _CurrentAmortizationAccountValuePerUnit + _CurrentValueDecreaseAccountValuePerUnit , ROUNDUNITASSET)
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

            MarkOld()

        End Sub

#End Region

    End Class

End Namespace