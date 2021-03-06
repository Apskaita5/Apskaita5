﻿Imports ApskaitaObjects.Assets
Imports ApskaitaObjects.Attributes

Namespace ActiveReports

    ''' <summary>
    ''' Represents a report containing info about long term asset's operations.
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
Public NotInheritable Class LongTermAssetOperationInfoListParent
        Inherits ReadOnlyBase(Of LongTermAssetOperationInfoListParent)

#Region " Business Methods "

        Private _ID As Integer = 0
        Private _Name As String = ""
        Private _MeasureUnit As String = ""
        Private _LegalGroup As String = ""
        Private _CustomGroupID As Integer = 0
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
        Private _CurrentlyOperational As Boolean = False
        Private _LiquidationUnitValue As Double = 0
        Private _ContinuedUsage As Boolean = False
        Private _DefaultAmortizationPeriod As Integer = 0
        Private _OperationList As LongTermAssetOperationInfoList = Nothing


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
                Return _Name
            End Get
        End Property

        ''' <summary>
        ''' Gets a measure unit of the long term asset.
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas.Vnt.</remarks>
        Public ReadOnly Property MeasureUnit() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _MeasureUnit
            End Get
        End Property

        ''' <summary>
        ''' Gets a name of the legal group of the long term asset.
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas.Grupe.</remarks>
        Public ReadOnly Property LegalGroup() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _LegalGroup
            End Get
        End Property

        ''' <summary>
        ''' Gets <see cref="LongTermAssetCustomGroup.ID">an ID of the custom group</see> that the long term asset is assigned to.
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas.CustomGroupID.</remarks>
        Public ReadOnly Property CustomGroupID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _CustomGroupID
            End Get
        End Property

        ''' <summary>
        ''' Gets <see cref="LongTermAssetCustomGroup.Name">a name of the custom group</see> that the long term asset is assigned to.
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas.CustomGroupID.</remarks>
        Public ReadOnly Property CustomGroup() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _CustomGroup
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
                Return _AcquisitionJournalEntryDocNumber
            End Get
        End Property

        ''' <summary>
        ''' Gets a content of the journal entry that substantiates the acquisition of the long term asset.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property AcquisitionJournalEntryDocContent() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AcquisitionJournalEntryDocContent
            End Get
        End Property

        ''' <summary>
        ''' Gets a type of the journal entry that substantiates the acquisition of the long term asset.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property AcquisitionJournalEntryDocType() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AcquisitionJournalEntryDocType
            End Get
        End Property

        ''' <summary>
        ''' Gets an inventory number of the long term asset.
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas.InvNr.</remarks>
        Public ReadOnly Property InventoryNumber() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _InventoryNumber
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
        ''' A balance for the <see cref="AccountAcquisition">AccountAcquisition</see> per unit at the acquisition date.
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
        ''' A balance for the <see cref="AccountValueDecrease">AccountValueDecrease</see> per unit at the acquisition date.
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
        ''' A balance for the <see cref="AccountRevaluedPortionAmmortization">AccountRevaluedPortionAmmortization</see> per unit at the acquisition date.
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
        ''' A total balance value of the long term asset at the acquisition date.
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
        ''' A unit balance value of the long term asset at the acquisition date.
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
        <IntegerField(ValueRequiredLevel.Optional, True)> _
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
        ''' A current balance for the <see cref="AccountAcquisition">AccountAcquisition</see>.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit (invalid) balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property CurrentAcquisitionAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If Not _OperationList Is Nothing AndAlso _OperationList.Count > 0 Then _
                    Return _OperationList(_OperationList.Count - 1).AfterOperationAcquisitionAccountValue
                Return CRound(_AcquisitionAccountValue)
            End Get
        End Property

        ''' <summary>
        ''' A current balance for the <see cref="AccountAcquisition">AccountAcquisition</see> per unit.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit (invalid) balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property CurrentAcquisitionAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If Not _OperationList Is Nothing AndAlso _OperationList.Count > 0 Then _
                    Return _OperationList(_OperationList.Count - 1).AfterOperationAcquisitionAccountValuePerUnit
                Return CRound(_AcquisitionAccountValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        ''' <summary>
        ''' A current balance for the <see cref="AccountAccumulatedAmortization">AccountAccumulatedAmortization</see>.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit (invalid) balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property CurrentAmortizationAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If Not _OperationList Is Nothing AndAlso _OperationList.Count > 0 Then _
                    Return _OperationList(_OperationList.Count - 1).AfterOperationAmortizationAccountValue
                Return CRound(_AmortizationAccountValue)
            End Get
        End Property

        ''' <summary>
        ''' A current balance for the <see cref="AccountAccumulatedAmortization">AccountAccumulatedAmortization</see> per unit.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit (invalid) balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property CurrentAmortizationAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If Not _OperationList Is Nothing AndAlso _OperationList.Count > 0 Then _
                    Return _OperationList(_OperationList.Count - 1).AfterOperationAmortizationAccountValuePerUnit
                Return CRound(_AmortizationAccountValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        ''' <summary>
        ''' A current balance for the <see cref="AccountValueDecrease">AccountValueDecrease</see>.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit (invalid) balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property CurrentValueDecreaseAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If Not _OperationList Is Nothing AndAlso _OperationList.Count > 0 Then _
                    Return _OperationList(_OperationList.Count - 1).AfterOperationValueDecreaseAccountValue
                Return CRound(_ValueDecreaseAccountValue)
            End Get
        End Property

        ''' <summary>
        ''' A current balance for the <see cref="AccountValueDecrease">AccountValueDecrease</see> per unit.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit (invalid) balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property CurrentValueDecreaseAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If Not _OperationList Is Nothing AndAlso _OperationList.Count > 0 Then _
                    Return _OperationList(_OperationList.Count - 1).AfterOperationValueDecreaseAccountValuePerUnit
                Return CRound(_ValueDecreaseAccountValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        ''' <summary>
        ''' A current balance for the <see cref="AccountValueIncrease">AccountValueIncrease</see>.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit (invalid) balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property CurrentValueIncreaseAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If Not _OperationList Is Nothing AndAlso _OperationList.Count > 0 Then _
                    Return _OperationList(_OperationList.Count - 1).AfterOperationValueIncreaseAccountValue
                Return CRound(_ValueIncreaseAccountValue)
            End Get
        End Property

        ''' <summary>
        ''' A current balance for the <see cref="AccountValueIncrease">AccountValueIncrease</see> per unit.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit (invalid) balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property CurrentValueIncreaseAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If Not _OperationList Is Nothing AndAlso _OperationList.Count > 0 Then _
                    Return _OperationList(_OperationList.Count - 1).AfterOperationValueIncreaseAccountValuePerUnit
                Return CRound(_ValueIncreaseAccountValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        ''' <summary>
        ''' A current balance for the <see cref="AccountRevaluedPortionAmmortization">AccountRevaluedPortionAmmortization</see>.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit (invalid) balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property CurrentValueIncreaseAmmortizationAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If Not _OperationList Is Nothing AndAlso _OperationList.Count > 0 Then _
                    Return _OperationList(_OperationList.Count - 1).AfterOperationValueIncreaseAmmortizationAccountValue
                Return CRound(_ValueIncreaseAmmortizationAccountValue)
            End Get
        End Property

        ''' <summary>
        ''' A current balance for the <see cref="AccountRevaluedPortionAmmortization">AccountRevaluedPortionAmmortization</see> per unit.
        ''' </summary>
        ''' <remarks>A positive number represents credit balance, a negative number represents debit (invalid) balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property CurrentValueIncreaseAmmortizationAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If Not _OperationList Is Nothing AndAlso _OperationList.Count > 0 Then _
                    Return _OperationList(_OperationList.Count - 1).AfterOperationValueIncreaseAmmortizationAccountValuePerUnit
                Return CRound(_ValueIncreaseAmmortizationAccountValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        ''' <summary>
        ''' A current total balance value of the long term asset.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property CurrentValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If Not _OperationList Is Nothing AndAlso _OperationList.Count > 0 Then _
                    Return _OperationList(_OperationList.Count - 1).AfterOperationValue
                Return CRound(_Value)
            End Get
        End Property

        ''' <summary>
        ''' A current unit balance value of the long term asset.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property CurrentValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If Not _OperationList Is Nothing AndAlso _OperationList.Count > 0 Then _
                    Return _OperationList(_OperationList.Count - 1).AfterOperationValuePerUnit
                Return CRound(_ValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        ''' <summary>
        ''' A current amount of the long term asset.
        ''' </summary>
        ''' <remarks></remarks>
        <IntegerField(ValueRequiredLevel.Optional, True)> _
        Public ReadOnly Property CurrentAmount() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If Not _OperationList Is Nothing AndAlso _OperationList.Count > 0 Then _
                    Return _OperationList(_OperationList.Count - 1).AfterOperationAmmount
                Return _Ammount
            End Get
        End Property

        ''' <summary>
        ''' Gets whether the long term asset is currently operational.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property CurrentlyOperational() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _CurrentlyOperational
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
        ''' Gets a list of the long term asset's operations.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property OperationList() As LongTermAssetOperationInfoList
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _OperationList
            End Get
        End Property



        Protected Overrides Function GetIdValue() As Object
            Return _ID
        End Function

#End Region

#Region " Authorization Rules "

        Protected Overrides Sub AddAuthorizationRules()

        End Sub

        Public Shared Function CanGetObject() As Boolean
            Return ApplicationContext.User.IsInRole("Assets.LongTermAssetOperationInfoList1")
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets a new instance of the LongTermAssetOperationInfoListParent report.
        ''' </summary>
        ''' <param name="assetId">An ID of the asset wchich the report is fetched for</param>
        ''' <remarks></remarks>
        Public Shared Function GetLongTermAssetOperationInfoListParent(ByVal assetId As Integer) As LongTermAssetOperationInfoListParent
            Return DataPortal.Fetch(Of LongTermAssetOperationInfoListParent)(New Criteria(assetId))
        End Function

        Private Sub New()
            ' require use of factory methods
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> _
        Private Class Criteria
            Private mId As Integer
            Public ReadOnly Property Id() As Integer
                Get
                    Return mId
                End Get
            End Property
            Public Sub New(ByVal id As Integer)
                mId = id
            End Sub
        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)

            If Not CanGetObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecuritySelectDenied)

            Dim myComm As New SQLCommand("FetchLongTermAssetOperationInfoListParent")
            myComm.AddParam("?AD", criteria.Id)

            Using myData As DataTable = myComm.Fetch

                If myData.Rows.Count < 1 Then Throw New Exception(String.Format( _
                    My.Resources.Common_ObjectNotFound, My.Resources.Assets_LongTermAsset_TypeName, _
                    criteria.Id.ToString()))

                Dim dr As DataRow = myData.Rows(0)

                _Name = CStrSafe(dr.Item(0)).Trim
                _MeasureUnit = CStrSafe(dr.Item(1)).Trim
                _LegalGroup = CStrSafe(dr.Item(2)).Trim
                _CustomGroupID = CIntSafe(dr.Item(3), 0)
                _CustomGroup = CStrSafe(dr.Item(4)).Trim
                _InventoryNumber = CStrSafe(dr.Item(5)).Trim
                _AcquisitionJournalEntryID = CIntSafe(dr.Item(6), 0)
                _AcquisitionDate = CDateSafe(dr.Item(7), Today)
                _AcquisitionJournalEntryDocNumber = CStrSafe(dr.Item(8)).Trim
                _AcquisitionJournalEntryDocContent = CStrSafe(dr.Item(9)).Trim
                _AcquisitionJournalEntryDocType = Utilities.ConvertLocalizedName( _
                    Utilities.ConvertDatabaseCharID(Of DocumentType)(CStrSafe(dr.Item(10))))
                _AccountAcquisition = CLongSafe(dr.Item(11), 0)
                _AccountAccumulatedAmortization = CLongSafe(dr.Item(12), 0)
                _AccountValueDecrease = CLongSafe(dr.Item(13), 0)
                _AccountValueIncrease = CLongSafe(dr.Item(14), 0)
                _AccountRevaluedPortionAmmortization = CLongSafe(dr.Item(15), 0)
                _LiquidationUnitValue = CDblSafe(dr.Item(16), ROUNDUNITASSET, 0)
                _DefaultAmortizationPeriod = CIntSafe(dr.Item(17), 0)
                _AcquisitionAccountValuePerUnit = CDblSafe(dr.Item(18), ROUNDUNITASSET, 0)
                _Ammount = CIntSafe(dr.Item(19), 0)
                _AcquisitionAccountValue = CDblSafe(dr.Item(20), 2, 0)
                _AmortizationAccountValue = CDblSafe(dr.Item(23))
                _AmortizationAccountValuePerUnit = CDblSafe(dr.Item(24), ROUNDUNITASSET, 0)
                _ValueIncreaseAmmortizationAccountValue = CDblSafe(dr.Item(25), 2, 0)
                _ValueIncreaseAmmortizationAccountValuePerUnit = CDblSafe(dr.Item(26), ROUNDUNITASSET, 0)
                _ContinuedUsage = ConvertDbBoolean(CIntSafe(dr.Item(27), 0))

                If CDbl(dr.Item(21)) < 0 Then
                    _ValueDecreaseAccountValuePerUnit = -CDblSafe(dr.Item(21), ROUNDUNITASSET, 0)
                    _ValueIncreaseAccountValuePerUnit = 0
                ElseIf CDbl(dr.Item(21)) > 0 Then
                    _ValueIncreaseAccountValuePerUnit = CDblSafe(dr.Item(21), ROUNDUNITASSET, 0)
                    _ValueDecreaseAccountValuePerUnit = 0
                Else
                    _ValueIncreaseAccountValuePerUnit = 0
                    _ValueDecreaseAccountValuePerUnit = 0
                End If
                If CDbl(dr.Item(22)) < 0 Then
                    _ValueDecreaseAccountValue = -CDblSafe(dr.Item(22), 2, 0)
                    _ValueIncreaseAccountValue = 0
                ElseIf CDbl(dr.Item(22)) > 0 Then
                    _ValueIncreaseAccountValue = CDblSafe(dr.Item(22), 2, 0)
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

            End Using

            _ID = criteria.Id

            myComm = New SQLCommand("FetchLongTermOperationInfoList")
            myComm.AddParam("?AD", criteria.Id)

            Using myData As DataTable = myComm.Fetch
                _OperationList = LongTermAssetOperationInfoList.GetList(Me, myData)
            End Using

            _CurrentlyOperational = _ContinuedUsage
            For Each o As LongTermAssetOperationInfo In _OperationList

                If o.Type = LtaOperationType.AccountChange Then

                    Select Case o.AccountType
                        Case LtaAccountChangeType.AcquisitionAccount
                            _AccountAcquisition = o.CorrespondingAccount
                        Case LtaAccountChangeType.AmortizationAccount
                            _AccountAccumulatedAmortization = o.CorrespondingAccount
                        Case LtaAccountChangeType.ValueDecreaseAccount
                            _AccountValueDecrease = o.CorrespondingAccount
                        Case LtaAccountChangeType.ValueIncreaseAccount
                            _AccountValueIncrease = o.CorrespondingAccount
                        Case LtaAccountChangeType.ValueIncreaseAmortizationAccount
                            _AccountRevaluedPortionAmmortization = o.CorrespondingAccount
                    End Select

                ElseIf o.Type = LtaOperationType.UsingStart OrElse _
                    o.Type = LtaOperationType.UsingEnd Then

                    _CurrentlyOperational = Not _CurrentlyOperational

                ElseIf o.Type = LtaOperationType.AmortizationPeriod Then

                    _DefaultAmortizationPeriod = o.NewAmortizationPeriod

                End If

            Next

        End Sub

#End Region

    End Class

End Namespace