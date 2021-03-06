﻿Imports ApskaitaObjects.Attributes

Namespace Assets

    ''' <summary>
    ''' Represents a general long term asset (acquisition) data.
    ''' </summary>
    ''' <remarks>Values are stored in the database table turtas.</remarks>
    <Serializable()> _
Public NotInheritable Class LongTermAsset
        Inherits BusinessBase(Of LongTermAsset)
        Implements IGetErrorForListItem, IValidationMessageProvider

#Region " Business Methods "

        Private Const PASTE_COLUMN_COUNT As Integer = 24

        Private _ID As Integer = 0
        Private _InsertDate As DateTime = Now
        Private _UpdateDate As DateTime = Now
        Private _ChronologyValidator As AcquisitionChronologicValidator = Nothing
        Private _Name As String = ""
        Private _MeasureUnit As String = My.Resources.Assets_LongTermAsset_DefaultMeasureUnit
        Private _LegalGroup As String = ""
        Private _CustomGroupInfo As LongTermAssetCustomGroupInfo = Nothing
        Private _AcquisitionDate As Date = Today
        Private _AcquisitionJournalEntryID As Integer = -1
        Private _AcquisitionJournalEntryDocNumber As String = ""
        Private _AcquisitionJournalEntryDocContent As String = ""
        Private _AcquisitionJournalEntryDocType As DocumentType
        Private _InventoryNumber As String = ""
        Private _AccountAcquisition As Long = 0
        Private _AccountAccumulatedAmortization As Long = 0
        Private _AccountValueIncrease As Long = 0
        Private _AccountValueDecrease As Long = 0
        Private _AccountRevaluedPortionAmmortization As Long = 0

        Private _AcquisitionAccountValue As Double = 0
        Private _AcquisitionAccountValueCorrection As Integer = 0
        Private _AcquisitionAccountValuePerUnit As Double = 0
        Private _AmortizationAccountValue As Double = 0
        Private _AmortizationAccountValueCorrection As Integer = 0
        Private _AmortizationAccountValuePerUnit As Double = 0
        Private _ValueDecreaseAccountValue As Double = 0
        Private _ValueDecreaseAccountValueCorrection As Integer = 0
        Private _ValueDecreaseAccountValuePerUnit As Double = 0
        Private _ValueIncreaseAccountValue As Double = 0
        Private _ValueIncreaseAccountValueCorrection As Integer = 0
        Private _ValueIncreaseAccountValuePerUnit As Double = 0
        Private _ValueIncreaseAmmortizationAccountValue As Double = 0
        Private _ValueIncreaseAmmortizationAccountValueCorrection As Integer = 0
        Private _ValueIncreaseAmmortizationAccountValuePerUnit As Double = 0

        Private _Amount As Integer = 0

        Private _LiquidationUnitValue As Double = 0
        Private _ContinuedUsage As Boolean = False
        Private _DefaultAmortizationPeriod As Integer = 0
        Private _AmortizationCalculatedForMonths As Integer = 0

        Private _IsInvoiceBound As Boolean = False

        Private _Value As Double = 0
        Private _ValuePerUnit As Double = 0
        Private _ValueRevaluedPortion As Double = 0
        Private _ValueRevaluedPortionPerUnit As Double = 0


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
        ''' Gets the date and time when the LongTermAsset was inserted into the database.
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas.InsertDate.</remarks>
        Public ReadOnly Property InsertDate() As DateTime
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _InsertDate
            End Get
        End Property

        ''' <summary>
        ''' Gets the date and time when the LongTermAsset was last updated.
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas.UpdateDate.</remarks>
        Public ReadOnly Property UpdateDate() As DateTime
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _UpdateDate
            End Get
        End Property

        ''' <summary>
        ''' Gets <see cref="IChronologicValidator">IChronologicValidator</see> object that contains business restraints on updating the asset data.
        ''' </summary>
        ''' <remarks>A <see cref="AcquisitionChronologicValidator">AcquisitionChronologicValidator</see> 
        ''' is used to validate a long term asset chronological business rules.</remarks>
        Public ReadOnly Property ChronologyValidator() As AcquisitionChronologicValidator
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ChronologyValidator
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets a name of the long term asset.
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas.Turtas.</remarks>
        <StringField(ValueRequiredLevel.Mandatory, 255)> _
        Public Property Name() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Name
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As String)
                CanWriteProperty(True)
                If AllDataIsReadOnly Then Exit Property
                If _Name.Trim <> value.Trim Then
                    _Name = value.Trim
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a measure unit of the long term asset.
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas.Vnt.</remarks>
        <StringField(ValueRequiredLevel.Mandatory, 50)> _
        Public Property MeasureUnit() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _MeasureUnit
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As String)
                CanWriteProperty(True)
                If AllDataIsReadOnly Then Exit Property
                If _MeasureUnit.Trim <> value.Trim Then
                    _MeasureUnit = value.Trim
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a name of the legal group of the long term asset.
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas.Grupe.</remarks>
        <NameField(ValueRequiredLevel.Recommended, ApskaitaObjects.Settings.NameType.LongTermAssetLegalGroup)> _
        Public Property LegalGroup() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _LegalGroup
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As String)
                CanWriteProperty(True)
                If AllDataIsReadOnly Then Exit Property
                If value Is Nothing Then value = ""
                If _LegalGroup.Trim <> value.Trim Then
                    _LegalGroup = value.Trim
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a custom group of the long term asset.
        ''' </summary>
        ''' <remarks>Use a <see cref="HelperLists.LongTermAssetCustomGroupInfoList">LongTermAssetCustomGroupInfoList</see> for a datasource.
        ''' Value is stored in the database field turtas.CustomGroupID.</remarks>
        <LongTermAssetCustomGroupField(ValueRequiredLevel.Optional)> _
        Public Property CustomGroupInfo() As LongTermAssetCustomGroupInfo
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _CustomGroupInfo
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As LongTermAssetCustomGroupInfo)
                CanWriteProperty(True)
                If AllDataIsReadOnly Then Exit Property
                If _CustomGroupInfo <> value Then
                    _CustomGroupInfo = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets an acquisition date of the long term asset.
        ''' </summary>
        ''' <remarks>Value corresponds to the date of <see cref="AcquisitionJournalEntryID">
        ''' the attached journal entry</see>.</remarks>
        Public Property AcquisitionDate() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AcquisitionDate
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Date)
                CanWriteProperty(True)
                If AcquisitionDateIsReadOnly Then Exit Property
                If _AcquisitionDate.Date <> value.Date Then
                    _AcquisitionDate = value.Date
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets an ID of the journal entry that substantiates the acquisition of the long term asset.
        ''' </summary>
        ''' <remarks>Use <see cref="LoadAssociatedJournalEntry">LoadAssociatedJournalEntry</see> 
        ''' method to attach a journal entry.
        ''' Value is stored in the database field turtas.Isigijimo_dok.</remarks>
        Public ReadOnly Property AcquisitionJournalEntryID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AcquisitionJournalEntryID
            End Get
        End Property

        ''' <summary>
        ''' Gets a document number of the journal entry that substantiates the acquisition of the long term asset.
        ''' </summary>
        ''' <remarks>Use <see cref="LoadAssociatedJournalEntry">LoadAssociatedJournalEntry</see> 
        ''' method to attach a journal entry.</remarks>
        Public ReadOnly Property AcquisitionJournalEntryDocNumber() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AcquisitionJournalEntryDocNumber
            End Get
        End Property

        ''' <summary>
        ''' Gets a content of the journal entry that substantiates the acquisition of the long term asset.
        ''' </summary>
        ''' <remarks>Use <see cref="LoadAssociatedJournalEntry">LoadAssociatedJournalEntry</see> 
        ''' method to attach a journal entry.</remarks>
        Public ReadOnly Property AcquisitionJournalEntryDocContent() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AcquisitionJournalEntryDocContent
            End Get
        End Property

        ''' <summary>
        ''' Gets a document type of the journal entry that substantiates the acquisition of the long term asset.
        ''' </summary>
        ''' <remarks>Use <see cref="LoadAssociatedJournalEntry">LoadAssociatedJournalEntry</see> 
        ''' method to attach a journal entry.</remarks>
        Public ReadOnly Property AcquisitionJournalEntryDocType() As DocumentType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AcquisitionJournalEntryDocType
            End Get
        End Property

        ''' <summary>
        ''' Gets a document type of the journal entry that substantiates the acquisition of the long term asset (as a localized human readable string).
        ''' </summary>
        ''' <remarks>Use <see cref="LoadAssociatedJournalEntry">LoadAssociatedJournalEntry</see> 
        ''' method to attach a journal entry.</remarks>
        Public ReadOnly Property AcquisitionJournalEntryDocTypeHumanReadable() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return Utilities.ConvertLocalizedName(_AcquisitionJournalEntryDocType)
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets an inventory number of the long term asset.
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas.InvNr.</remarks>
        <StringField(ValueRequiredLevel.Optional, 50)> _
        Public Property InventoryNumber() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _InventoryNumber
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As String)
                CanWriteProperty(True)
                If AllDataIsReadOnly Then Exit Property
                If _InventoryNumber.Trim <> value.Trim Then
                    _InventoryNumber = value.Trim
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets <see cref="General.Account.ID">an asset acquisitinion account</see>.
        ''' </summary>
        ''' <remarks>12 BAS para 12.
        ''' Value is stored in the database field turtas.Saskaita.</remarks>
        <AccountField(ValueRequiredLevel.Mandatory, True, 1, 2)> _
        Public Property AccountAcquisition() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AccountAcquisition
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Long)
                CanWriteProperty(True)
                If FinancialDataIsReadOnly Then Exit Property
                If _AccountAcquisition <> value Then
                    _AccountAcquisition = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets <see cref="General.Account.ID">an asset amortization (depreciation) account</see>.
        ''' </summary>
        ''' <remarks>12 BAS para 68.
        ''' Value is stored in the database field turtas.AccountAmortization.</remarks>
        <AccountField(ValueRequiredLevel.Mandatory, True, 1, 2)> _
        Public Property AccountAccumulatedAmortization() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AccountAccumulatedAmortization
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Long)
                CanWriteProperty(True)
                If FinancialDataIsReadOnly Then Exit Property
                If _AccountAccumulatedAmortization <> value Then
                    _AccountAccumulatedAmortization = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets <see cref="General.Account.ID">an asset value increase account</see>.
        ''' </summary>
        ''' <remarks>12 BAS para 48.
        ''' Value is stored in the database field turtas.AccountValueIncrease.</remarks>
        <AccountField(ValueRequiredLevel.Mandatory, True, 1, 2)> _
        Public Property AccountValueIncrease() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AccountValueIncrease
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Long)
                CanWriteProperty(True)
                If FinancialDataIsReadOnly Then Exit Property
                If _AccountValueIncrease <> value Then
                    _AccountValueIncrease = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets <see cref="General.Account.ID">an asset value decrease account</see>.
        ''' </summary>
        ''' <remarks>12 BAS para 49.
        ''' Value is stored in the database field turtas.AccountValueDecrease.</remarks>
        <AccountField(ValueRequiredLevel.Mandatory, True, 1, 2)> _
        Public Property AccountValueDecrease() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AccountValueDecrease
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Long)
                CanWriteProperty(True)
                If FinancialDataIsReadOnly Then Exit Property
                If _AccountValueDecrease <> value Then
                    _AccountValueDecrease = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets <see cref="General.Account.ID">an asset revalued portion amortization account</see>.
        ''' </summary>
        ''' <remarks>12 BAS para 48.
        ''' Value is stored in the database field turtas.AccountRevaluedPortionAmmortization.</remarks>
        <AccountField(ValueRequiredLevel.Mandatory, True, 1, 2)> _
        Public Property AccountRevaluedPortionAmmortization() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AccountRevaluedPortionAmmortization
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Long)
                CanWriteProperty(True)
                If FinancialDataIsReadOnly Then Exit Property
                If _AccountRevaluedPortionAmmortization <> value Then
                    _AccountRevaluedPortionAmmortization = value
                    PropertyHasChanged()
                End If
            End Set
        End Property


        ''' <summary>
        ''' Gets the initial balance of <see cref="AccountAcquisition">asset acquisitinion account</see>.
        ''' </summary>
        ''' <remarks>Debit value as positive, credit value (negative) is not allowed.
        ''' Value is stored in the database field turtas.TotalValue.</remarks>
        <DoubleField(ValueRequiredLevel.Mandatory, False, 2)> _
        Public ReadOnly Property AcquisitionAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AcquisitionAccountValue)
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets the technical discrepancy between <see cref="AccountAcquisition">asset acquisitinion account</see>
        ''' total value and unit value.
        ''' </summary>
        ''' <remarks>Value is calculated as: <see cref="AcquisitionAccountValue">AcquisitionAccountValue</see>
        ''' minus <see cref="AcquisitionAccountValuePerUnit">AcquisitionAccountValuePerUnit</see>
        ''' multiplied by <see cref="Ammount">Ammount</see>.</remarks>
        <CorrectionField()> _
        Public Property AcquisitionAccountValueCorrection() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AcquisitionAccountValueCorrection
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Integer)
                CanWriteProperty(True)
                If FinancialDataIsReadOnly Then Exit Property
                If _AcquisitionAccountValueCorrection <> value Then
                    _AcquisitionAccountValueCorrection = value
                    PropertyHasChanged()
                    _AcquisitionAccountValue = CRound((_AcquisitionAccountValuePerUnit * _Amount) _
                        + (_AcquisitionAccountValueCorrection / 100), 2)
                    PropertyHasChanged("AcquisitionAccountValue")
                    RecalculateValues(True)
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the initial balance of <see cref="AccountAcquisition">asset acquisitinion account</see> 
        ''' per asset unit.
        ''' </summary>
        ''' <remarks>Debit value as positive, credit value (negative) is not allowed.
        ''' Value is stored in the database field turtas.Vnt_kaina.</remarks>
        <DoubleField(ValueRequiredLevel.Mandatory, False, ROUNDUNITASSET)> _
        Public Property AcquisitionAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AcquisitionAccountValuePerUnit, ROUNDUNITASSET)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Double)
                CanWriteProperty(True)
                If FinancialDataIsReadOnly Then Exit Property
                If CRound(_AcquisitionAccountValuePerUnit, ROUNDUNITASSET) <> CRound(value, ROUNDUNITASSET) Then
                    _AcquisitionAccountValuePerUnit = CRound(value, ROUNDUNITASSET)
                    PropertyHasChanged()
                    _AcquisitionAccountValue = CRound((_AcquisitionAccountValuePerUnit * _Amount) _
                        + (_AcquisitionAccountValueCorrection / 100), 2)
                    PropertyHasChanged("AcquisitionAccountValue")
                    RecalculateValues(True)
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets the initial balance of  <see cref="AccountAccumulatedAmortization">asset amortization account</see>.
        ''' </summary>
        ''' <remarks>Credit value as positive, debit value (negative) is not allowed.
        ''' Value is stored in the database field turtas.S_Amort.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property AmortizationAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AmortizationAccountValue)
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets the technical discrepancy between <see cref="AccountAccumulatedAmortization">asset amortization (depreciation) account</see>
        ''' total value and unit value.
        ''' </summary>
        ''' <remarks>Value is calculated as: <see cref="AmortizationAccountValue">AmortizationAccountValue</see>
        ''' minus <see cref="AmortizationAccountValuePerUnit">AmortizationAccountValuePerUnit</see>
        ''' multiplied by <see cref="Ammount">Ammount</see>.</remarks>
        <CorrectionField()> _
        Public Property AmortizationAccountValueCorrection() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AmortizationAccountValueCorrection
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Integer)
                CanWriteProperty(True)
                If FinancialDataIsReadOnly Then Exit Property
                If _AmortizationAccountValueCorrection <> value Then
                    _AmortizationAccountValueCorrection = value
                    PropertyHasChanged()
                    _AmortizationAccountValue = CRound((_AmortizationAccountValuePerUnit * _Amount) _
                        + (_AmortizationAccountValueCorrection / 100), 2)
                    PropertyHasChanged("AmortizationAccountValue")
                    RecalculateValues(True)
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the initial balance of  <see cref="AccountAccumulatedAmortization">asset amortization account</see> 
        ''' per asset unit.
        ''' </summary>
        ''' <remarks>Credit value as positive, debit value (negative) is not allowed.
        ''' Value is stored in the database field turtas.AccumulatedAmortizationPerUnit.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, ROUNDUNITASSET)> _
        Public Property AmortizationAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AmortizationAccountValuePerUnit, ROUNDUNITASSET)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Double)
                CanWriteProperty(True)
                If FinancialDataIsReadOnly Then Exit Property
                If CRound(_AmortizationAccountValuePerUnit, ROUNDUNITASSET) <> CRound(value, ROUNDUNITASSET) Then
                    _AmortizationAccountValuePerUnit = CRound(value, ROUNDUNITASSET)
                    PropertyHasChanged()
                    _AmortizationAccountValue = CRound((_AmortizationAccountValuePerUnit * _Amount) _
                        + (_AmortizationAccountValueCorrection / 100), 2)
                    PropertyHasChanged("AmortizationAccountValue")
                    RecalculateValues(True)
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets the initial balance of <see cref="AccountValueDecrease">asset value decrease account</see>.
        ''' </summary>
        ''' <remarks>Credit value as positive, debit value (negative) is not allowed.
        ''' Value is stored in the database field turtas.AcquisitionRevaluedPortionTotalValue as a negative number.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property ValueDecreaseAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ValueDecreaseAccountValue)
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets the technical discrepancy between <see cref="AccountValueDecrease">asset value decrease account</see>
        ''' total value and unit value.
        ''' </summary>
        ''' <remarks>Value is calculated as: <see cref="ValueDecreaseAccountValue">ValueDecreaseAccountValue</see>
        ''' minus <see cref="ValueDecreaseAccountValuePerUnit">ValueDecreaseAccountValuePerUnit</see>
        ''' multiplied by <see cref="Ammount">Ammount</see>.</remarks>
        <CorrectionField()> _
        Public Property ValueDecreaseAccountValueCorrection() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ValueDecreaseAccountValueCorrection
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Integer)
                CanWriteProperty(True)
                If FinancialDataIsReadOnly Then Exit Property
                If _ValueDecreaseAccountValueCorrection <> value Then
                    _ValueDecreaseAccountValueCorrection = value
                    PropertyHasChanged()
                    _ValueDecreaseAccountValue = CRound((_ValueDecreaseAccountValuePerUnit * _Amount) _
                        + (_ValueDecreaseAccountValueCorrection / 100), 2)
                    PropertyHasChanged("ValueDecreaseAccountValue")
                    RecalculateValues(True)
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the initial balance of <see cref="AccountValueDecrease">asset value decrease account</see> 
        ''' per asset unit.
        ''' </summary>
        ''' <remarks>Credit value as positive, debit value (negative) is not allowed.
        ''' Value is stored in the database field turtas.AcquisitionRevaluedPortionUnitValue as a negative number.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, ROUNDUNITASSET)> _
        Public Property ValueDecreaseAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ValueDecreaseAccountValuePerUnit, ROUNDUNITASSET)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Double)
                CanWriteProperty(True)
                If FinancialDataIsReadOnly Then Exit Property
                If CRound(_ValueDecreaseAccountValuePerUnit, ROUNDUNITASSET) <> CRound(value, ROUNDUNITASSET) Then
                    _ValueDecreaseAccountValuePerUnit = CRound(value, ROUNDUNITASSET)
                    PropertyHasChanged()
                    _ValueDecreaseAccountValue = CRound((_ValueDecreaseAccountValuePerUnit * _Amount) _
                        + (_ValueDecreaseAccountValueCorrection / 100), 2)
                    PropertyHasChanged("ValueDecreaseAccountValue")
                    RecalculateValues(True)
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the initial balance of <see cref="AccountValueIncrease">asset value increase account</see>.
        ''' </summary>
        ''' <remarks>Debit value as positive, credit value (negative) is not allowed.
        ''' Value is stored in the database field turtas.AcquisitionRevaluedPortionTotalValue as a positive number.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property ValueIncreaseAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ValueIncreaseAccountValue)
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets the technical discrepancy between <see cref="AccountValueIncrease">asset value increase account</see>
        ''' total value and unit value.
        ''' </summary>
        ''' <remarks>Value is calculated as: <see cref="ValueIncreaseAccountValue">ValueIncreaseAccountValue</see>
        ''' minus <see cref="ValueIncreaseAccountValuePerUnit">ValueIncreaseAccountValuePerUnit</see>
        ''' multiplied by <see cref="Ammount">Ammount</see>.</remarks>
        <CorrectionField()> _
        Public Property ValueIncreaseAccountValueCorrection() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ValueIncreaseAccountValueCorrection
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Integer)
                CanWriteProperty(True)
                If FinancialDataIsReadOnly Then Exit Property
                If _ValueIncreaseAccountValueCorrection <> value Then
                    _ValueIncreaseAccountValueCorrection = value
                    PropertyHasChanged()
                    _ValueIncreaseAccountValue = CRound((_ValueIncreaseAccountValuePerUnit * _Amount) _
                        + (_ValueIncreaseAccountValueCorrection / 100), 2)
                    PropertyHasChanged("ValueIncreaseAccountValue")
                    RecalculateValues(True)
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the initial balance of <see cref="AccountValueIncrease">asset value increase account</see> 
        ''' per asset unit.
        ''' </summary>
        ''' <remarks>Debit value as positive, credit value (negative) is not allowed.
        ''' Value is stored in the database field turtas.AcquisitionRevaluedPortionUnitValue as a positive number.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, ROUNDUNITASSET)> _
        Public Property ValueIncreaseAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ValueIncreaseAccountValuePerUnit, ROUNDUNITASSET)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Double)
                CanWriteProperty(True)
                If FinancialDataIsReadOnly Then Exit Property
                If CRound(_ValueIncreaseAccountValuePerUnit, ROUNDUNITASSET) <> CRound(value, ROUNDUNITASSET) Then
                    _ValueIncreaseAccountValuePerUnit = CRound(value, ROUNDUNITASSET)
                    PropertyHasChanged()
                    _ValueIncreaseAccountValue = CRound((_ValueIncreaseAccountValuePerUnit * _Amount) _
                        + (_ValueIncreaseAccountValueCorrection / 100), 2)
                    PropertyHasChanged("ValueIncreaseAccountValue")
                    RecalculateValues(True)
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the amount of <see cref="LongTermAsset.AccountRevaluedPortionAmmortization">
        ''' asset revalued portion amortization account</see>.
        ''' </summary>
        ''' <remarks>Credit value as positive, debit value (negative) is not allowed.
        ''' Value is stored in the database field turtas.AccumulatedAmortizationRevaluedPortionTotal.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property ValueIncreaseAmmortizationAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ValueIncreaseAmmortizationAccountValue)
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets the technical discrepancy between <see cref="AccountRevaluedPortionAmmortization">asset revalued portion amortization account</see>
        ''' total value and unit value.
        ''' </summary>
        ''' <remarks>Value is calculated as: <see cref="ValueIncreaseAmmortizationAccountValue">ValueIncreaseAmmortizationAccountValue</see>
        ''' minus <see cref="ValueIncreaseAmmortizationAccountValuePerUnit">ValueIncreaseAmmortizationAccountValuePerUnit</see>
        ''' multiplied by <see cref="Ammount">Ammount</see>.</remarks>
        <CorrectionField()> _
        Public Property ValueIncreaseAmmortizationAccountValueCorrection() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ValueIncreaseAmmortizationAccountValueCorrection
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Integer)
                CanWriteProperty(True)
                If FinancialDataIsReadOnly Then Exit Property
                If _ValueIncreaseAmmortizationAccountValueCorrection <> value Then
                    _ValueIncreaseAmmortizationAccountValueCorrection = value
                    PropertyHasChanged()
                    _ValueIncreaseAmmortizationAccountValue = CRound((_ValueIncreaseAmmortizationAccountValuePerUnit * _Amount) _
                        + (_ValueIncreaseAmmortizationAccountValueCorrection / 100), 2)
                    PropertyHasChanged("ValueIncreaseAmmortizationAccountValue")
                    RecalculateValues(True)
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets the initial balance of <see cref="AccountRevaluedPortionAmmortization">
        ''' asset revalued portion amortization account</see> per asset unit.
        ''' </summary>
        ''' <remarks>Credit value as positive, debit value (negative) is not allowed.
        ''' Value is stored in the database field turtas.AccumulatedAmortizationRevaluedPortionPerUnit.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, ROUNDUNITASSET)> _
        Public Property ValueIncreaseAmmortizationAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ValueIncreaseAmmortizationAccountValuePerUnit, ROUNDUNITASSET)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Double)
                CanWriteProperty(True)
                If FinancialDataIsReadOnly Then Exit Property
                If CRound(_ValueIncreaseAmmortizationAccountValuePerUnit, ROUNDUNITASSET) _
                    <> CRound(value, ROUNDUNITASSET) Then
                    _ValueIncreaseAmmortizationAccountValuePerUnit = CRound(value, ROUNDUNITASSET)
                    PropertyHasChanged()
                    _ValueIncreaseAmmortizationAccountValue = CRound((_ValueIncreaseAmmortizationAccountValuePerUnit * _Amount) _
                        + (_ValueIncreaseAmmortizationAccountValueCorrection / 100), 2)
                    PropertyHasChanged("ValueIncreaseAmmortizationAccountValue")
                    RecalculateValues(True)
                End If
            End Set
        End Property


        ''' <summary>
        ''' Gets the accounting value of the long term asset.
        ''' </summary>
        ''' <remarks>Value is calculated by the <see cref="RecalculateValues">RecalculateValues</see> method.</remarks>
        <DoubleField(ValueRequiredLevel.Mandatory, False, 2)> _
        Public ReadOnly Property Value() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_Value)
            End Get
        End Property

        ''' <summary>
        ''' Gets the accounting unit value of the long term asset.
        ''' </summary>
        ''' <remarks>Value is calculated by the <see cref="RecalculateValues">RecalculateValues</see> method.</remarks>
        <DoubleField(ValueRequiredLevel.Mandatory, False, ROUNDUNITASSET)> _
        Public ReadOnly Property ValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets the initial amount of the long term asset.
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas.Kiekis.</remarks>
        <IntegerField(ValueRequiredLevel.Mandatory, False)> _
        Public Property Ammount() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Amount
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Integer)
                CanWriteProperty(True)
                If FinancialDataIsReadOnly Then Exit Property
                If _Amount <> value Then

                    _Amount = value
                    PropertyHasChanged()

                    _AcquisitionAccountValue = CRound((_AcquisitionAccountValuePerUnit * _Amount) _
                        + (_AcquisitionAccountValueCorrection / 100), 2)
                    PropertyHasChanged("AcquisitionAccountValue")
                    _AmortizationAccountValue = CRound((_AmortizationAccountValuePerUnit * _Amount) _
                        + (_AmortizationAccountValueCorrection / 100), 2)
                    PropertyHasChanged("AmortizationAccountValue")
                    _ValueDecreaseAccountValue = CRound((_ValueDecreaseAccountValuePerUnit * _Amount) _
                        + (_ValueDecreaseAccountValueCorrection / 100), 2)
                    PropertyHasChanged("ValueDecreaseAccountValue")
                    _ValueIncreaseAccountValue = CRound((_ValueIncreaseAccountValuePerUnit * _Amount) _
                        + (_ValueIncreaseAccountValueCorrection / 100), 2)
                    PropertyHasChanged("ValueIncreaseAccountValue")
                    _ValueIncreaseAmmortizationAccountValue = CRound((_ValueIncreaseAmmortizationAccountValuePerUnit * _Amount) _
                        + (_ValueIncreaseAmmortizationAccountValueCorrection / 100), 2)
                    PropertyHasChanged("ValueIncreaseAmmortizationAccountValue")

                    RecalculateValues(True)

                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets the accounting value of the revalued portion of the long term asset.
        ''' </summary>
        ''' <remarks>Value is calculated by the <see cref="RecalculateValues">RecalculateValues</see> method.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, 2)> _
        Public ReadOnly Property ValueRevaluedPortion() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ValueRevaluedPortion)
            End Get
        End Property

        ''' <summary>
        ''' Gets the accounting unit value of the revalued portion of the long term asset.
        ''' </summary>
        ''' <remarks>Value is calculated by the <see cref="RecalculateValues">RecalculateValues</see> method.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, ROUNDUNITASSET)> _
        Public ReadOnly Property ValueRevaluedPortionPerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ValueRevaluedPortionPerUnit, ROUNDUNITASSET)
            End Get
        End Property


        ''' <summary>
        ''' Gets or sets a liquidation (salvage) value of the long term asset per unit.
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas.Likutine.</remarks>
        <DoubleField(ValueRequiredLevel.Mandatory, False, ROUNDUNITASSET)> _
        Public Property LiquidationUnitValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_LiquidationUnitValue, ROUNDUNITASSET)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Double)
                CanWriteProperty(True)
                If AmortizationDataIsReadOnly Then Exit Property
                If CRound(_LiquidationUnitValue, ROUNDUNITASSET) <> CRound(value, ROUNDUNITASSET) Then
                    _LiquidationUnitValue = CRound(value, ROUNDUNITASSET)
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets whether the long term asset is already operational.
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas.ContinuedUsage.</remarks>
        Public Property ContinuedUsage() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ContinuedUsage
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Boolean)
                CanWriteProperty(True)
                If AmortizationDataIsReadOnly Then Exit Property
                If _ContinuedUsage <> value Then
                    _ContinuedUsage = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a default amortization (depreciation) period for the long term asset.
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas.ContinuedUsage.</remarks>
        <IntegerField(ValueRequiredLevel.Mandatory, False, True, 1, 99, False)> _
        Public Property DefaultAmortizationPeriod() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DefaultAmortizationPeriod
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Integer)
                CanWriteProperty(True)
                If AmortizationDataIsReadOnly Then Exit Property
                If _DefaultAmortizationPeriod <> value Then
                    _DefaultAmortizationPeriod = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' An initial number of months that the amortization of the long term asset unit is calculated for.
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas.WasUsedMonths.</remarks>
        <IntegerField(ValueRequiredLevel.Optional, False)> _
        Public Property AmortizationCalculatedForMonths() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AmortizationCalculatedForMonths
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Integer)
                CanWriteProperty(True)
                If AmortizationDataIsReadOnly Then Exit Property
                If _AmortizationCalculatedForMonths <> value Then
                    _AmortizationCalculatedForMonths = value
                    PropertyHasChanged()
                End If
            End Set
        End Property


        ''' <summary>
        ''' Gets whether the long term asset is acquired by an invoice, 
        ''' i.e. the object is part of the invoice, not a stand-alone object.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property IsInvoiceBound() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsInvoiceBound
            End Get
        End Property

        Public Overrides ReadOnly Property IsValid() As Boolean _
            Implements IValidationMessageProvider.IsValid
            Get
                Return MyBase.IsValid
            End Get
        End Property

        ''' <summary>
        ''' Gets whether the <see cref="AcquisitionDate">AcquisitionDate</see> property is readonly.
        ''' </summary>
        ''' <remarks>For child object and parent object bound to an invoice.</remarks>
        Public ReadOnly Property AcquisitionDateIsReadOnly() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsInvoiceBound OrElse IsChild
            End Get
        End Property

        ''' <summary>
        ''' Gets whether the amortization related object properties are readonly.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property AmortizationDataIsReadOnly() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return Me.AllDataIsReadOnly OrElse _ChronologyValidator.AmortizationIsCalculated
            End Get
        End Property

        ''' <summary>
        ''' Gets whether the financial object properties are readonly.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property FinancialDataIsReadOnly() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return Me.AllDataIsReadOnly OrElse Not _ChronologyValidator.FinancialDataCanChange _
                    OrElse Not _ChronologyValidator.ParentFinancialDataCanChange
            End Get
        End Property

        ''' <summary>
        ''' Gets whether all of the object properties are readonly.
        ''' </summary>
        ''' <remarks>For parent object bound to an invoice.</remarks>
        Public ReadOnly Property AllDataIsReadOnly() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsInvoiceBound AndAlso Not IsChild
            End Get
        End Property

        ''' <summary>
        ''' Whether the operation is actualy a child of another operation or
        ''' other document.
        ''' </summary>
        ''' <remarks>The <see cref="longtermasset.IsChild">IsChild</see>
        ''' property defines the current state of the object, i.e. whether the object was
        ''' fetched/created as a child). The IsChildOperation property defines a 
        ''' persistence state of the object, i.e. whether the object was originaly
        ''' saved as a child object.</remarks>
        Public ReadOnly Property IsChildOperation() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsInvoiceBound OrElse IsChild
            End Get
        End Property


        ''' <summary>
        ''' Loads an attached journal entry that substantiates the asset acquisition.
        ''' </summary>
        ''' <param name="entryInfo">A journal entry info to load.</param>
        ''' <remarks></remarks>
        Public Sub LoadAssociatedJournalEntry(ByVal entryInfo As ActiveReports.JournalEntryInfo)

            If _IsInvoiceBound OrElse IsChild Then
                Throw New Exception(My.Resources.Assets_LongTermAsset_InvalidLoadAssociatedJournalEntry)
            End If

            If entryInfo Is Nothing Then

                _AcquisitionJournalEntryID = 0
                _AcquisitionJournalEntryDocType = DocumentType.None
                _AcquisitionJournalEntryDocContent = My.Resources.Assets_LongTermAsset_AcquisitionJournalEntryDocContentNull
                _AcquisitionJournalEntryDocNumber = ""

            Else

                If entryInfo.DocType = DocumentType.InvoiceMade OrElse _
                   entryInfo.DocType = DocumentType.InvoiceReceived Then
                    Throw New Exception(My.Resources.Assets_LongTermAsset_InvalidJournalEntryType)
                End If

                _AcquisitionJournalEntryID = entryInfo.Id
                _AcquisitionJournalEntryDocType = entryInfo.DocType
                _AcquisitionJournalEntryDocContent = entryInfo.Content
                _AcquisitionJournalEntryDocNumber = entryInfo.DocNumber

            End If

            PropertyHasChanged("AcquisitionJournalEntryDocNumber")
            PropertyHasChanged("AcquisitionJournalEntryDocContent")
            PropertyHasChanged("AcquisitionJournalEntryDocType")
            PropertyHasChanged("AcquisitionJournalEntryID")

        End Sub


        Private Sub RecalculateValues(ByVal raisePropertyChanged As Boolean)

            _Value = CRound(_AcquisitionAccountValue - AmortizationAccountValue _
                - _ValueDecreaseAccountValue + _ValueIncreaseAccountValue _
                - _ValueIncreaseAmmortizationAccountValue)
            _ValueRevaluedPortion = CRound(_ValueIncreaseAccountValue - _ValueDecreaseAccountValue _
                - _ValueIncreaseAmmortizationAccountValue)
            _ValuePerUnit = CRound(_AcquisitionAccountValuePerUnit - AmortizationAccountValuePerUnit _
                - _ValueDecreaseAccountValuePerUnit + _ValueIncreaseAccountValuePerUnit _
                - _ValueIncreaseAmmortizationAccountValuePerUnit, ROUNDUNITASSET)
            _ValueRevaluedPortionPerUnit = CRound(_ValueIncreaseAccountValuePerUnit _
                - _ValueDecreaseAccountValuePerUnit _
                - _ValueIncreaseAmmortizationAccountValuePerUnit, ROUNDUNITASSET)

            If raisePropertyChanged Then
                OnPropertyChanged("Value")
                OnPropertyChanged("ValueRevaluedPortion")
                OnPropertyChanged("ValuePerUnit")
                OnPropertyChanged("ValueRevaluedPortionPerUnit")
            End If

        End Sub


        Friend Sub SetValues(ByVal nAmount As Integer, ByVal nValue As Double, ByVal nValuePerUnit As Double)

            If Not _ChronologyValidator.FinancialDataCanChange Then Exit Sub

            If _Amount <> nAmount Then
                _Amount = nAmount
                PropertyHasChanged("Ammount")
            End If

            AcquisitionAccountValuePerUnit = CRound(nValuePerUnit + _AmortizationAccountValuePerUnit _
                + _ValueDecreaseAccountValuePerUnit - _ValueIncreaseAccountValuePerUnit _
                + _ValueIncreaseAmmortizationAccountValuePerUnit, ROUNDUNITASSET)

            If CRound(nValue, 2) <> Me.Value Then

                _AcquisitionAccountValueCorrection = Convert.ToInt32(Math.Floor(CRound(nValue _
                    - (nValuePerUnit * _Amount), 2) * 100))
                PropertyHasChanged("AcquisitionAccountValueCorrection")
                RecalculateValues(True)

            End If

        End Sub

        Friend Sub SetDate(ByVal newDate As Date)
            If _AcquisitionDate.Date <> newDate.Date Then
                _AcquisitionDate = newDate.Date
                PropertyHasChanged("AcquisitionDate")
            End If
        End Sub


        Public Function GetErrorString() As String _
            Implements IGetErrorForListItem.GetErrorString
            If IsValid Then Return ""
            Return String.Format(My.Resources.Common_ErrorInItem, Me.ToString, _
                vbCrLf, Me.BrokenRulesCollection.ToString(Validation.RuleSeverity.Error))
        End Function

        Public Function GetWarningString() As String _
            Implements IGetErrorForListItem.GetWarningString
            If BrokenRulesCollection.WarningCount < 1 Then Return ""
            Return String.Format(My.Resources.Common_WarningInItem, Me.ToString, _
                vbCrLf, Me.BrokenRulesCollection.ToString(Validation.RuleSeverity.Warning))
        End Function


        Public Function GetAllBrokenRules() As String _
            Implements IValidationMessageProvider.GetAllBrokenRules
            Dim result As String = ""
            If Not MyBase.IsValid Then result = AddWithNewLine(result, _
                Me.BrokenRulesCollection.ToString(Validation.RuleSeverity.Error), False)
            Return result
        End Function

        Public Function GetAllWarnings() As String _
            Implements IValidationMessageProvider.GetAllWarnings
            Dim result As String = ""
            If MyBase.BrokenRulesCollection.WarningCount > 0 Then result = AddWithNewLine(result, _
                Me.BrokenRulesCollection.ToString(Validation.RuleSeverity.Warning), False)
            Return result
        End Function

        Public Function HasWarnings() As Boolean _
            Implements IValidationMessageProvider.HasWarnings
            Return MyBase.BrokenRulesCollection.WarningCount > 0
        End Function


        ''' <summary>
        ''' Gets expected fields count in (tab delimited) paste string.
        ''' </summary>
        Public Shared Function GetPasteStringColumnCount() As Integer
            Return PASTE_COLUMN_COUNT
        End Function

        ''' <summary>
        ''' Gets an array of expected fields sequence in (tab delimited) paste string.
        ''' </summary>
        Public Shared Function GetPasteStringColumns() As String()
            Return My.Resources.Assets_LongTermAsset_PasteColumns.Split(New String() {"<BR>"}, _
                StringSplitOptions.RemoveEmptyEntries)
        End Function

        ''' <summary>
        ''' Gets a human readable description of expected fields sequence in (tab delimited) paste string.
        ''' </summary>
        Public Shared Function GetPasteStringColumnsDescription() As String
            Return String.Format(My.Resources.Assets_LongTermAsset_PasteColumnsDescription, PASTE_COLUMN_COUNT, _
                String.Join(", ", My.Resources.Assets_LongTermAsset_PasteColumns.Split(New String() {"<BR>"}, _
                StringSplitOptions.RemoveEmptyEntries)))
        End Function

        ''' <summary>
        ''' Gets a datatable which columns corresponds to the required imported data 
        ''' (propert name, data type and regionalized caption).
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function GetDataTableSpecification() As DataTable
            Return GetDataTableSpecificationForProperties(GetType(LongTermAsset), New String() _
                {"Name", "MeasureUnit", "LegalGroup", "InventoryNumber", "InventoryNumber",
                "AccountAcquisition", "AccountAccumulatedAmortization", "AccountValueDecrease",
                "AccountValueIncrease", "AccountRevaluedPortionAmmortization", "LiquidationUnitValue",
                "DefaultAmortizationPeriod", "Ammount", "AcquisitionAccountValue", "AcquisitionAccountValuePerUnit",
                "AmortizationAccountValue", "AmortizationAccountValuePerUnit", "ValueDecreaseAccountValue",
                "ValueDecreaseAccountValuePerUnit", "ValueIncreaseAccountValue", "ValueIncreaseAccountValuePerUnit",
                "ValueIncreaseAmmortizationAccountValue", "ValueIncreaseAmmortizationAccountValuePerUnit",
                "ContinuedUsage", "AmortizationCalculatedForMonths"})
        End Function


        Public Overrides Function Save() As LongTermAsset

            Me.ValidationRules.CheckRules()
            If Not Me.IsValid Then
                Throw New Exception(String.Format(My.Resources.Common_ContainsErrors, vbCrLf, _
                    GetAllBrokenRules()))
            End If

            If IsChild OrElse _AcquisitionJournalEntryDocType = DocumentType.InvoiceReceived _
                OrElse _AcquisitionJournalEntryDocType = DocumentType.InvoiceMade Then
                Throw New Exception(My.Resources.Assets_LongTermAsset_InvalidSave)
            End If

            Return MyBase.Save()

        End Function


        Protected Overrides Function GetIdValue() As Object
            Return _ID
        End Function

        Public Overrides Function ToString() As String
            Return String.Format(My.Resources.Assets_LongTermAsset_ToString, _
                _Name, _InventoryNumber)
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()

            ValidationRules.AddRule(AddressOf CommonValidation.CommonValidation.StringFieldValidation, _
                New Csla.Validation.RuleArgs("Name"))
            ValidationRules.AddRule(AddressOf CommonValidation.CommonValidation.StringFieldValidation, _
                New Csla.Validation.RuleArgs("MeasureUnit"))
            ValidationRules.AddRule(AddressOf CommonValidation.CommonValidation.StringFieldValidation, _
                New Csla.Validation.RuleArgs("LegalGroup"))
            ValidationRules.AddRule(AddressOf CommonValidation.CommonValidation.StringFieldValidation, _
                New Csla.Validation.RuleArgs("InventoryNumber"))
            ValidationRules.AddRule(AddressOf CommonValidation.CommonValidation.IntegerFieldValidation, _
                New Csla.Validation.RuleArgs("Ammount"))
            ValidationRules.AddRule(AddressOf CommonValidation.CommonValidation.IntegerFieldValidation, _
                New Csla.Validation.RuleArgs("AmortizationCalculatedForMonths"))
            ValidationRules.AddRule(AddressOf CommonValidation.CommonValidation.AccountFieldValidation, _
                New Csla.Validation.RuleArgs("AccountAccumulatedAmortization"))
            ValidationRules.AddRule(AddressOf CommonValidation.CommonValidation.AccountFieldValidation, _
                New Csla.Validation.RuleArgs("AccountAcquisition"))
            ValidationRules.AddRule(AddressOf CommonValidation.CommonValidation.AccountFieldValidation, _
                New Csla.Validation.RuleArgs("AccountRevaluedPortionAmmortization"))
            ValidationRules.AddRule(AddressOf CommonValidation.CommonValidation.AccountFieldValidation, _
                New Csla.Validation.RuleArgs("AccountValueDecrease"))
            ValidationRules.AddRule(AddressOf CommonValidation.CommonValidation.AccountFieldValidation, _
                New Csla.Validation.RuleArgs("AccountValueIncrease"))
            ValidationRules.AddRule(AddressOf CommonValidation.CommonValidation.ChronologyValidation, _
                New CommonValidation.CommonValidation.ChronologyRuleArgs("AcquisitionDate", "ChronologyValidator"))


            ValidationRules.AddRule(AddressOf LiquidationValueValidation, "LiquidationUnitValue")
            ValidationRules.AddDependantProperty("AcquisitionAccountValuePerUnit", "LiquidationUnitValue", False)

            ValidationRules.AddRule(AddressOf AcquisitionAccountValueValidation, "AcquisitionAccountValue")
            ValidationRules.AddRule(AddressOf AcquisitionAccountValuePerUnitValidation, _
                "AcquisitionAccountValuePerUnit")
            ValidationRules.AddDependantProperty("AmortizationAccountValue", _
                "AcquisitionAccountValue", False)
            ValidationRules.AddDependantProperty("ValueDecreaseAccountValue", _
                "AcquisitionAccountValue", False)
            ValidationRules.AddDependantProperty("AmortizationAccountValuePerUnit", _
                "AcquisitionAccountValuePerUnit", False)
            ValidationRules.AddDependantProperty("ValueDecreaseAccountValuePerUnit", _
                "AcquisitionAccountValuePerUnit", False)

            ValidationRules.AddRule(AddressOf ValueDecreaseAccountValueValidation, _
                "ValueDecreaseAccountValue")
            ValidationRules.AddRule(AddressOf ValueDecreaseAccountValuePerUnitValidation, _
                "ValueDecreaseAccountValuePerUnit")
            ValidationRules.AddDependantProperty("ValueIncreaseAccountValue", _
                "ValueDecreaseAccountValue", False)
            ValidationRules.AddDependantProperty("ValueIncreaseAmmortizationAccountValue", _
                "ValueDecreaseAccountValue", False)
            ValidationRules.AddDependantProperty("ValueIncreaseAccountValuePerUnit", _
                "ValueDecreaseAccountValuePerUnit", False)
            ValidationRules.AddDependantProperty("ValueIncreaseAmmortizationAccountValuePerUnit", _
                "ValueDecreaseAccountValuePerUnit", False)

            ValidationRules.AddRule(AddressOf ValueIncreaseAccountValueValidation, _
                "ValueIncreaseAccountValue")
            ValidationRules.AddRule(AddressOf ValueIncreaseAccountValuePerUnitValidation, _
                "ValueIncreaseAccountValuePerUnit")
            ValidationRules.AddDependantProperty("ValueIncreaseAmmortizationAccountValue", _
                "ValueIncreaseAccountValue", False)
            ValidationRules.AddDependantProperty("ValueIncreaseAmmortizationAccountValuePerUnit", _
                "ValueIncreaseAccountValuePerUnit", False)

            ValidationRules.AddRule(AddressOf DefaultAmortizationPeriodValidation, _
                "DefaultAmortizationPeriod")
            ValidationRules.AddDependantProperty("AmortizationCalculatedForMonths", _
                "DefaultAmortizationPeriod", False)

            ValidationRules.AddRule(AddressOf JournalEntryValidation, "AcquisitionJournalEntryID")

            ValidationRules.AddRule(AddressOf CorrectionValidation, _
                New Csla.Validation.RuleArgs("AcquisitionAccountValueCorrection"))
            ValidationRules.AddRule(AddressOf CorrectionValidation, _
                New Csla.Validation.RuleArgs("AmortizationAccountValueCorrection"))
            ValidationRules.AddRule(AddressOf CorrectionValidation, _
                New Csla.Validation.RuleArgs("ValueDecreaseAccountValueCorrection"))
            ValidationRules.AddRule(AddressOf CorrectionValidation, _
                New Csla.Validation.RuleArgs("ValueIncreaseAccountValueCorrection"))
            ValidationRules.AddRule(AddressOf CorrectionValidation, _
                New Csla.Validation.RuleArgs("ValueIncreaseAmmortizationAccountValueCorrection"))

            ValidationRules.AddDependantProperty("Ammount", _
                "AcquisitionAccountValueCorrection", False)
            ValidationRules.AddDependantProperty("Ammount", _
                "AmortizationAccountValueCorrection", False)
            ValidationRules.AddDependantProperty("Ammount", _
                "ValueDecreaseAccountValueCorrection", False)
            ValidationRules.AddDependantProperty("Ammount", _
                "ValueIncreaseAccountValueCorrection", False)
            ValidationRules.AddDependantProperty("Ammount", _
                "ValueIncreaseAmmortizationAccountValueCorrection", False)

        End Sub

        ''' <summary>
        ''' Rule ensuring liquidation value is set and valid, i.e. less then 10% of the unit value.
        ''' </summary>
        ''' <param name="target">Object containing the data to validate</param>
        ''' <param name="e">Arguments parameter specifying the name of the string
        ''' property to validate</param>
        ''' <returns><see langword="false" /> if the rule is broken</returns>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
        Private Shared Function LiquidationValueValidation(ByVal target As Object, _
            ByVal e As Validation.RuleArgs) As Boolean

            If Not CommonValidation.CommonValidation.DoubleFieldValidation(target, e) Then Return False

            Dim valObj As LongTermAsset = DirectCast(target, LongTermAsset)

            If CRound(valObj._AcquisitionAccountValuePerUnit * 0.1, ROUNDUNITASSET) < _
                CRound(valObj._LiquidationUnitValue, ROUNDUNITASSET) Then

                e.Description = String.Format(My.Resources.Assets_LongTermAsset_InvalidLiquidationValue, _
                    DblParser(valObj._AcquisitionAccountValuePerUnit * 0.1, ROUNDUNITASSET))
                e.Severity = Validation.RuleSeverity.Error
                Return False

            End If

            Return True

        End Function

        ''' <summary>
        ''' Rule ensuring Acquisition account value is acceptable.
        ''' </summary>
        ''' <param name="target">Object containing the data to validate</param>
        ''' <param name="e">Arguments parameter specifying the name of the string
        ''' property to validate</param>
        ''' <returns><see langword="false" /> if the rule is broken</returns>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
        Private Shared Function AcquisitionAccountValueValidation(ByVal target As Object, _
            ByVal e As Validation.RuleArgs) As Boolean

            If Not CommonValidation.CommonValidation.DoubleFieldValidation(target, e) Then Return False

            Dim valObj As LongTermAsset = DirectCast(target, LongTermAsset)

            If CRound(valObj._AcquisitionAccountValue - valObj._AmortizationAccountValue _
                - valObj._ValueDecreaseAccountValue) < 0 Then

                e.Description = My.Resources.Assets_LongTermAsset_InvalidAcquisitionAccountValue
                e.Severity = Validation.RuleSeverity.Error
                Return False

            End If

            Return True

        End Function

        ''' <summary>
        ''' Rule ensuring Acquisition account value per unit is acceptable.
        ''' </summary>
        ''' <param name="target">Object containing the data to validate</param>
        ''' <param name="e">Arguments parameter specifying the name of the string
        ''' property to validate</param>
        ''' <returns><see langword="false" /> if the rule is broken</returns>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
        Private Shared Function AcquisitionAccountValuePerUnitValidation(ByVal target As Object, _
            ByVal e As Validation.RuleArgs) As Boolean

            If Not CommonValidation.CommonValidation.DoubleFieldValidation(target, e) Then Return False

            Dim valObj As LongTermAsset = DirectCast(target, LongTermAsset)

            If CRound(valObj._AcquisitionAccountValuePerUnit - valObj._AmortizationAccountValuePerUnit _
                - valObj._ValueDecreaseAccountValuePerUnit, ROUNDUNITASSET) < 0 Then

                e.Description = My.Resources.Assets_LongTermAsset_InvalidAcquisitionAccountValuePerUnit
                e.Severity = Validation.RuleSeverity.Error
                Return False

            End If

            Return True

        End Function

        ''' <summary>
        ''' Rule ensuring value decrease account value is acceptable.
        ''' </summary>
        ''' <param name="target">Object containing the data to validate</param>
        ''' <param name="e">Arguments parameter specifying the name of the string
        ''' property to validate</param>
        ''' <returns><see langword="false" /> if the rule is broken</returns>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
        Private Shared Function ValueDecreaseAccountValueValidation(ByVal target As Object, _
            ByVal e As Validation.RuleArgs) As Boolean

            If Not CommonValidation.CommonValidation.DoubleFieldValidation(target, e) Then Return False

            Dim valObj As LongTermAsset = DirectCast(target, LongTermAsset)

            If CRound(valObj._ValueDecreaseAccountValue) > 0 AndAlso _
                CRound(valObj._ValueIncreaseAccountValue - _
                valObj._ValueIncreaseAmmortizationAccountValue) > 0 Then

                e.Description = My.Resources.Assets_LongTermAsset_InvalidValueDecreaseAccountValue
                e.Severity = Validation.RuleSeverity.Error
                Return False

            End If

            Return True

        End Function

        ''' <summary>
        ''' Rule ensuring value decrease account value per unit is acceptable.
        ''' </summary>
        ''' <param name="target">Object containing the data to validate</param>
        ''' <param name="e">Arguments parameter specifying the name of the string
        ''' property to validate</param>
        ''' <returns><see langword="false" /> if the rule is broken</returns>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
        Private Shared Function ValueDecreaseAccountValuePerUnitValidation(ByVal target As Object, _
            ByVal e As Validation.RuleArgs) As Boolean

            If Not CommonValidation.CommonValidation.DoubleFieldValidation(target, e) Then Return False

            Dim valObj As LongTermAsset = DirectCast(target, LongTermAsset)

            If CRound(valObj._ValueDecreaseAccountValuePerUnit, ROUNDUNITASSET) > 0 AndAlso _
                CRound(valObj._ValueIncreaseAccountValuePerUnit - _
                valObj._ValueIncreaseAmmortizationAccountValuePerUnit, ROUNDUNITASSET) > 0 Then

                e.Description = My.Resources.Assets_LongTermAsset_InvalidValueDecreaseAccountValuePerUnit
                e.Severity = Validation.RuleSeverity.Error
                Return False

            End If

            Return True

        End Function

        ''' <summary>
        ''' Rule ensuring value increase account value is acceptable.
        ''' </summary>
        ''' <param name="target">Object containing the data to validate</param>
        ''' <param name="e">Arguments parameter specifying the name of the string
        ''' property to validate</param>
        ''' <returns><see langword="false" /> if the rule is broken</returns>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
        Private Shared Function ValueIncreaseAccountValueValidation(ByVal target As Object, _
          ByVal e As Validation.RuleArgs) As Boolean

            If Not CommonValidation.CommonValidation.DoubleFieldValidation(target, e) Then Return False

            Dim valObj As LongTermAsset = DirectCast(target, LongTermAsset)

            If CRound(valObj._ValueIncreaseAccountValue - _
                valObj._ValueIncreaseAmmortizationAccountValue) < 0 Then

                e.Description = My.Resources.Assets_LongTermAsset_InvalidValueIncreaseAccountValue
                e.Severity = Validation.RuleSeverity.Error
                Return False

            End If

            Return True

        End Function

        ''' <summary>
        ''' Rule ensuring value increase account value per unit is acceptable.
        ''' </summary>
        ''' <param name="target">Object containing the data to validate</param>
        ''' <param name="e">Arguments parameter specifying the name of the string
        ''' property to validate</param>
        ''' <returns><see langword="false" /> if the rule is broken</returns>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
        Private Shared Function ValueIncreaseAccountValuePerUnitValidation(ByVal target As Object, _
            ByVal e As Validation.RuleArgs) As Boolean

            If Not CommonValidation.CommonValidation.DoubleFieldValidation(target, e) Then Return False

            Dim valObj As LongTermAsset = DirectCast(target, LongTermAsset)

            If CRound(valObj._ValueIncreaseAccountValuePerUnit - _
                valObj._ValueIncreaseAmmortizationAccountValuePerUnit, ROUNDUNITASSET) < 0 Then

                e.Description = My.Resources.Assets_LongTermAsset_InvalidValueIncreaseAccountValuePerUnit
                e.Severity = Validation.RuleSeverity.Error
                Return False

            End If

            Return True

        End Function

        ''' <summary>
        ''' Rule ensuring journal entry is assigned correctly.
        ''' </summary>
        ''' <param name="target">Object containing the data to validate</param>
        ''' <param name="e">Arguments parameter specifying the name of the string
        ''' property to validate</param>
        ''' <returns><see langword="false" /> if the rule is broken</returns>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
        Private Shared Function JournalEntryValidation(ByVal target As Object, _
            ByVal e As Validation.RuleArgs) As Boolean

            Dim valObj As LongTermAsset = DirectCast(target, LongTermAsset)

            If Not valObj.IsChild AndAlso Not valObj._AcquisitionJournalEntryID > 0 Then

                e.Description = My.Resources.Assets_LongTermAsset_JournalEntryNull
                e.Severity = Validation.RuleSeverity.Error
                Return False

            End If

            Return True

        End Function

        ''' <summary>
        ''' Rule ensuring default amortization period is acceptable.
        ''' </summary>
        ''' <param name="target">Object containing the data to validate</param>
        ''' <param name="e">Arguments parameter specifying the name of the string
        ''' property to validate</param>
        ''' <returns><see langword="false" /> if the rule is broken</returns>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
        Private Shared Function DefaultAmortizationPeriodValidation(ByVal target As Object, _
            ByVal e As Validation.RuleArgs) As Boolean

            If Not CommonValidation.CommonValidation.IntegerFieldValidation(target, e) Then Return False

            Dim valObj As LongTermAsset = DirectCast(target, LongTermAsset)

            If Math.Ceiling(valObj._AmortizationCalculatedForMonths / 12) > _
                valObj._DefaultAmortizationPeriod Then

                e.Description = My.Resources.Assets_LongTermAsset_InvalidDefaultAmortizationPeriod
                e.Severity = Validation.RuleSeverity.Error
                Return False

            End If

            Return True

        End Function

        ''' <summary>
        ''' Rule ensuring that the corrections can only be entered when amount is more then one.
        ''' </summary>
        ''' <param name="target">Object containing the data to validate</param>
        ''' <param name="e">Arguments parameter specifying the name of the string
        ''' property to validate</param>
        ''' <returns><see langword="false" /> if the rule is broken</returns>
        <System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods")> _
        Private Shared Function CorrectionValidation(ByVal target As Object, _
            ByVal e As Validation.RuleArgs) As Boolean

            Dim value As Integer = DirectCast(CallByName(target, e.PropertyName, CallType.Get), Integer)
            Dim valObj As LongTermAsset = DirectCast(target, LongTermAsset)

            If Not valObj._Amount > 1 AndAlso value <> 0 Then
                e.Description = My.Resources.Assets_LongTermAsset_CorrectionInvalid
                e.Severity = Validation.RuleSeverity.Error
                Return False
            End If

            Return CommonValidation.CommonValidation.IntegerFieldValidation(target, e)

        End Function

#End Region

#Region " Authorization Rules "

        Protected Overrides Sub AddAuthorizationRules()
            AuthorizationRules.AllowWrite("Assets.LongTermAsset2")
        End Sub

        Public Shared Function CanAddObject() As Boolean
            Return ApplicationContext.User.IsInRole("Assets.LongTermAsset2")
        End Function

        Public Shared Function CanGetObject() As Boolean
            Return ApplicationContext.User.IsInRole("Assets.LongTermAsset1")
        End Function

        Public Shared Function CanEditObject() As Boolean
            Return ApplicationContext.User.IsInRole("Assets.LongTermAsset3")
        End Function

        Public Shared Function CanDeleteObject() As Boolean
            Return ApplicationContext.User.IsInRole("Assets.LongTermAsset3")
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets a new LongTermAsset instance.
        ''' </summary>
        ''' <remarks></remarks>
        Public Shared Function GetNewLongTermAsset() As LongTermAsset
            Return New LongTermAsset(False, Nothing)
        End Function

        ''' <summary>
        ''' Gets a new LongTermAsset child instance (for use as a composite object, e.g. invoice).
        ''' </summary>
        ''' <param name="parentChronologyValidator">a parent chronologic validator</param>
        ''' <remarks></remarks>
        Friend Shared Function NewLongTermAssetChild(ByVal parentChronologyValidator _
            As IChronologicValidator) As LongTermAsset
            Return New LongTermAsset(True, parentChronologyValidator)
        End Function

        ''' <summary>
        ''' Gets a new LongTermAsset child instance using imported data.
        ''' </summary>
        ''' <param name="columns">imported data as string array</param>
        ''' <param name="parentChronologyValidator">a parent chronologic validator</param>
        '''  <remarks></remarks>
        Friend Shared Function NewLongTermAssetChild(ByVal columns As String(),
            ByVal parentChronologyValidator As IChronologicValidator,
            ByVal accounList As AccountInfoList) As LongTermAsset
            Return New LongTermAsset(columns, parentChronologyValidator, accounList)
        End Function

        ''' <summary>
        ''' Gets a new LongTermAsset child instance using imported data.
        ''' </summary>
        ''' <param name="dr">template datarow containing the data to import, 
        ''' see <see cref="GetDataTableSpecification()">GetDataTableSpecification</see> method</param>
        ''' <param name="parentChronologyValidator">a parent chronologic validator</param>
        '''  <remarks></remarks>
        Friend Shared Function NewLongTermAssetChild(ByVal dr As DataRow,
            ByVal parentChronologyValidator As IChronologicValidator,
            ByVal accounList As AccountInfoList) As LongTermAsset
            Return New LongTermAsset(dr, parentChronologyValidator, accounList)
        End Function

        ''' <summary>
        ''' Gets an existing LongTermAsset instance from a database.
        ''' </summary>
        ''' <param name="id">An ID of the long term asset to get.</param>
        ''' <remarks></remarks>
        Public Shared Function GetLongTermAsset(ByVal id As Integer) As LongTermAsset
            Return DataPortal.Fetch(Of LongTermAsset)(New Criteria(id))
        End Function

        ''' <summary>
        ''' Gets an existing LongTermAsset child instance from a database (for use as a composite object, e.g. invoice).
        ''' </summary>
        ''' <param name="id">An ID of the long term asset to get.</param>
        ''' <remarks>Should only be invoked server side.</remarks>
        Friend Shared Function GetLongTermAssetChild(ByVal id As Integer, _
            ByVal parentChronologyValidator As IChronologicValidator) As LongTermAsset
            Return New LongTermAsset(id, parentChronologyValidator)
        End Function

        ''' <summary>
        ''' Gets an existing LongTermAsset child instance from a database 
        ''' (for use as a composite object, e.g. transfer of balance).
        ''' </summary>
        ''' <param name="dr">a datarow containing the long term asset data.</param>
        ''' <remarks>Should only be invoked server side.</remarks>
        Friend Shared Function GetLongTermAssetChild(ByVal dr As DataRow, _
            ByVal parentChronologyValidator As IChronologicValidator) As LongTermAsset
            Return New LongTermAsset(dr, parentChronologyValidator)
        End Function

        ''' <summary>
        ''' Deletes an existing LongTermAsset instance from a database.
        ''' </summary>
        ''' <param name="id">An ID of the long term asset to delete.</param>
        ''' <remarks></remarks>
        Public Shared Sub DeleteLongTermAsset(ByVal id As Integer)
            DataPortal.Delete(New Criteria(id))
        End Sub

        ''' <summary>
        ''' Does a delete operation from server side. 
        ''' </summary>
        ''' <param name="nID">An ID of the long term asset to delete.</param>
        ''' <remarks>Should only be invoked server side. Doesn't check for critical rules
        ''' (fetch or programatical error within transaction crashes program).
        ''' Critical rule CheckIfAnyOperationExists needs to be invoked before starting transaction.</remarks>
        Friend Shared Sub DeleteChild(ByVal nID As Integer)
            DoDelete(nID)
        End Sub


        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal columns As String(), ByVal parentChronologyValidator As IChronologicValidator, _
            ByVal accounList As AccountInfoList)
            MarkAsChild()
            Create(columns, parentChronologyValidator, accounList)
        End Sub

        Private Sub New(ByVal dr As DataRow, ByVal parentChronologyValidator As IChronologicValidator,
            ByVal accounList As AccountInfoList)
            MarkAsChild()
            Create(dr, parentChronologyValidator, accounList)
        End Sub

        Private Sub New(ByVal createChild As Boolean, ByVal parentChronologyValidator As IChronologicValidator)
            If createChild Then MarkAsChild()
            Create(parentChronologyValidator)
        End Sub

        Private Sub New(ByVal id As Integer, ByVal parentChronologyValidator As IChronologicValidator)
            MarkAsChild()
            Fetch(id, parentChronologyValidator)
        End Sub

        Private Sub New(ByVal dr As DataRow, ByVal parentChronologyValidator As IChronologicValidator)
            MarkAsChild()
            Fetch(dr, parentChronologyValidator)
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


        Private Sub Create(ByVal parentChronologyValidator As IChronologicValidator)
            _ChronologyValidator = AcquisitionChronologicValidator. _
                NewAcquisitionChronologicValidator(parentChronologyValidator)
            ValidationRules.CheckRules()
        End Sub

        Private Sub Create(ByVal columns As String(), ByVal parentChronologyValidator As IChronologicValidator, _
            ByVal accounList As AccountInfoList)

            _Name = CStrSafe(GetItem(columns, 0)).Trim
            _MeasureUnit = CStrSafe(GetItem(columns, 1)).Trim
            _LegalGroup = CStrSafe(GetItem(columns, 2)).Trim
            _InventoryNumber = CStrSafe(GetItem(columns, 3)).Trim
            _AccountAcquisition = CLongSafe(GetItem(columns, 4), 0)
            _AccountAccumulatedAmortization = CLongSafe(GetItem(columns, 5), 0)
            _AccountValueDecrease = CLongSafe(GetItem(columns, 6), 0)
            _AccountValueIncrease = CLongSafe(GetItem(columns, 7), 0)
            _AccountRevaluedPortionAmmortization = CLongSafe(GetItem(columns, 8), 0)
            _LiquidationUnitValue = CDblSafe(GetItem(columns, 9), ROUNDUNITASSET, 0)
            _DefaultAmortizationPeriod = CIntSafe(GetItem(columns, 10), 0)
            _Amount = CIntSafe(GetItem(columns, 11), 0)
            _AcquisitionAccountValue = CDblSafe(GetItem(columns, 12), 2, 0)
            _AcquisitionAccountValuePerUnit = CDblSafe(GetItem(columns, 13), ROUNDUNITASSET, 0)
            _AmortizationAccountValue = CDblSafe(GetItem(columns, 14), 2, 0)
            _AmortizationAccountValuePerUnit = CDblSafe(GetItem(columns, 15), ROUNDUNITASSET, 0)
            _ValueDecreaseAccountValue = CDblSafe(GetItem(columns, 16), 2, 0)
            _ValueDecreaseAccountValuePerUnit = CDblSafe(GetItem(columns, 17), ROUNDUNITASSET, 0)
            _ValueIncreaseAccountValue = CDblSafe(GetItem(columns, 18), 2, 0)
            _ValueIncreaseAccountValuePerUnit = CDblSafe(GetItem(columns, 19), ROUNDUNITASSET, 0)
            _ValueIncreaseAmmortizationAccountValue = CDblSafe(GetItem(columns, 20), 2, 0)
            _ValueIncreaseAmmortizationAccountValuePerUnit = CDblSafe(GetItem(columns, 21), ROUNDUNITASSET, 0)
            _ContinuedUsage = ConvertDbBoolean(CIntSafe(GetItem(columns, 22), 0))
            _AmortizationCalculatedForMonths = CIntSafe(GetItem(columns, 23), 0)

            _AcquisitionAccountValueCorrection = Convert.ToInt32(Math.Floor(CRound(_AcquisitionAccountValue _
                - (_AcquisitionAccountValuePerUnit * _Amount), 2) * 100))
            _AmortizationAccountValueCorrection = Convert.ToInt32(Math.Floor(CRound(_AmortizationAccountValue _
                - (_AmortizationAccountValuePerUnit * _Amount), 2) * 100))
            _ValueDecreaseAccountValueCorrection = Convert.ToInt32(Math.Floor(CRound(_ValueDecreaseAccountValue _
                - (_ValueDecreaseAccountValuePerUnit * _Amount), 2) * 100))
            _ValueIncreaseAccountValueCorrection = Convert.ToInt32(Math.Floor(CRound(_ValueIncreaseAccountValue _
                - (_ValueIncreaseAccountValuePerUnit * _Amount), 2) * 100))
            _ValueIncreaseAmmortizationAccountValueCorrection = Convert.ToInt32(Math.Floor(CRound(_ValueIncreaseAmmortizationAccountValue _
                - (_ValueIncreaseAmmortizationAccountValuePerUnit * _Amount), 2) * 100))

            If accounList.GetAccountByID(_AccountAcquisition) Is Nothing Then
                _AccountAcquisition = 0
            End If
            If accounList.GetAccountByID(_AccountAccumulatedAmortization) Is Nothing Then
                _AccountAccumulatedAmortization = 0
            End If
            If accounList.GetAccountByID(_AccountValueDecrease) Is Nothing Then
                _AccountValueDecrease = 0
            End If
            If accounList.GetAccountByID(_AccountValueIncrease) Is Nothing Then
                _AccountValueIncrease = 0
            End If
            If accounList.GetAccountByID(_AccountRevaluedPortionAmmortization) Is Nothing Then
                _AccountRevaluedPortionAmmortization = 0
            End If

            RecalculateValues(False)

            _ChronologyValidator = AcquisitionChronologicValidator. _
                NewAcquisitionChronologicValidator(parentChronologyValidator)

            ValidationRules.CheckRules()

        End Sub

        Private Sub Create(ByVal dr As DataRow, ByVal parentChronologyValidator As IChronologicValidator,
            ByVal accounList As AccountInfoList)

            _Name = DirectCast(dr.Item("Name"), String)
            _MeasureUnit = DirectCast(dr.Item("MeasureUnit"), String)
            _LegalGroup = DirectCast(dr.Item("LegalGroup"), String)
            _InventoryNumber = DirectCast(dr.Item("InventoryNumber"), String)
            _AccountAcquisition = DirectCast(dr.Item("AccountAcquisition"), Long)
            _AccountAccumulatedAmortization = DirectCast(dr.Item("AccountAccumulatedAmortization"), Long)
            _AccountValueDecrease = DirectCast(dr.Item("AccountValueDecrease"), Long)
            _AccountValueIncrease = DirectCast(dr.Item("AccountValueIncrease"), Long)
            _AccountRevaluedPortionAmmortization = DirectCast(dr.Item("AccountRevaluedPortionAmmortization"), Long)
            _LiquidationUnitValue = CRound(DirectCast(dr.Item("LiquidationUnitValue"), Double), ROUNDUNITASSET)
            _DefaultAmortizationPeriod = DirectCast(dr.Item("DefaultAmortizationPeriod"), Integer)
            _Amount = DirectCast(dr.Item("Ammount"), Integer)
            _AcquisitionAccountValue = CRound(DirectCast(dr.Item("AcquisitionAccountValue"), Double), 2)
            _AcquisitionAccountValuePerUnit = CRound(DirectCast(dr.Item("AcquisitionAccountValuePerUnit"), Double), ROUNDUNITASSET)
            _AmortizationAccountValue = CRound(DirectCast(dr.Item("AmortizationAccountValue"), Double), 2)
            _AmortizationAccountValuePerUnit = CRound(DirectCast(dr.Item("AmortizationAccountValuePerUnit"), Double), ROUNDUNITASSET)
            _ValueDecreaseAccountValue = CRound(DirectCast(dr.Item("ValueDecreaseAccountValue"), Double), 2)
            _ValueDecreaseAccountValuePerUnit = CRound(DirectCast(dr.Item("ValueDecreaseAccountValuePerUnit"), Double), ROUNDUNITASSET)
            _ValueIncreaseAccountValue = CRound(DirectCast(dr.Item("ValueIncreaseAccountValue"), Double), 2)
            _ValueIncreaseAccountValuePerUnit = CRound(DirectCast(dr.Item("ValueIncreaseAccountValuePerUnit"), Double), ROUNDUNITASSET)
            _ValueIncreaseAmmortizationAccountValue = CRound(DirectCast(dr.Item("ValueIncreaseAmmortizationAccountValue"), Double), 2)
            _ValueIncreaseAmmortizationAccountValuePerUnit = CRound(DirectCast(dr.Item("ValueIncreaseAmmortizationAccountValuePerUnit"), Double), ROUNDUNITASSET)
            _ContinuedUsage = DirectCast(dr.Item("ContinuedUsage"), Boolean)
            _AmortizationCalculatedForMonths = DirectCast(dr.Item("AmortizationCalculatedForMonths"), Integer)

            _AcquisitionAccountValueCorrection = Convert.ToInt32(Math.Floor(CRound(_AcquisitionAccountValue _
                - (_AcquisitionAccountValuePerUnit * _Amount), 2) * 100))
            _AmortizationAccountValueCorrection = Convert.ToInt32(Math.Floor(CRound(_AmortizationAccountValue _
                - (_AmortizationAccountValuePerUnit * _Amount), 2) * 100))
            _ValueDecreaseAccountValueCorrection = Convert.ToInt32(Math.Floor(CRound(_ValueDecreaseAccountValue _
                - (_ValueDecreaseAccountValuePerUnit * _Amount), 2) * 100))
            _ValueIncreaseAccountValueCorrection = Convert.ToInt32(Math.Floor(CRound(_ValueIncreaseAccountValue _
                - (_ValueIncreaseAccountValuePerUnit * _Amount), 2) * 100))
            _ValueIncreaseAmmortizationAccountValueCorrection = Convert.ToInt32(Math.Floor(CRound(_ValueIncreaseAmmortizationAccountValue _
                - (_ValueIncreaseAmmortizationAccountValuePerUnit * _Amount), 2) * 100))

            If accounList.GetAccountByID(_AccountAcquisition) Is Nothing Then
                _AccountAcquisition = 0
            End If
            If accounList.GetAccountByID(_AccountAccumulatedAmortization) Is Nothing Then
                _AccountAccumulatedAmortization = 0
            End If
            If accounList.GetAccountByID(_AccountValueDecrease) Is Nothing Then
                _AccountValueDecrease = 0
            End If
            If accounList.GetAccountByID(_AccountValueIncrease) Is Nothing Then
                _AccountValueIncrease = 0
            End If
            If accounList.GetAccountByID(_AccountRevaluedPortionAmmortization) Is Nothing Then
                _AccountRevaluedPortionAmmortization = 0
            End If

            RecalculateValues(False)

            _ChronologyValidator = AcquisitionChronologicValidator.
                NewAcquisitionChronologicValidator(parentChronologyValidator)

            ValidationRules.CheckRules()

        End Sub

        Private Function GetItem(ByVal s As String(), ByVal index As Integer) As String
            If s Is Nothing OrElse index + 1 > s.Length Then Return ""
            Return s(index)
        End Function


        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)
            If Not CanGetObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecuritySelectDenied)
            Fetch(criteria.Id, Nothing)
        End Sub

        Private Sub Fetch(ByVal nAssetID As Integer, ByVal parentValidator As IChronologicValidator)

            Dim myComm As New SQLCommand("FetchLongTermAssetOperationInfoListParent")
            myComm.AddParam("?AD", nAssetID)

            Using myData As DataTable = myComm.Fetch

                If myData.Rows.Count < 1 Then Throw New Exception(String.Format( _
                    My.Resources.Common_ObjectNotFound, My.Resources.Assets_LongTermAsset_TypeName, _
                    nAssetID.ToString()))

                Fetch(myData.Rows(0), parentValidator)

            End Using

        End Sub

        Private Sub Fetch(ByVal dr As DataRow, ByVal parentChronologyValidator As IChronologicValidator)

            _Name = CStrSafe(dr.Item(0)).Trim
            _MeasureUnit = CStrSafe(dr.Item(1)).Trim
            _LegalGroup = CStrSafe(dr.Item(2)).Trim
            _CustomGroupInfo = LongTermAssetCustomGroupInfo.GetLongTermAssetCustomGroupInfo(dr, 3)
            _InventoryNumber = CStrSafe(dr.Item(5)).Trim
            _AcquisitionJournalEntryID = CIntSafe(dr.Item(6), 0)
            _AcquisitionDate = CDateSafe(dr.Item(7), Today)
            _AcquisitionJournalEntryDocNumber = CStrSafe(dr.Item(8)).Trim
            _AcquisitionJournalEntryDocContent = CStrSafe(dr.Item(9)).Trim
            _AcquisitionJournalEntryDocType = Utilities.ConvertDatabaseCharID(Of DocumentType) _
                (CStrSafe(dr.Item(10)))
            _AccountAcquisition = CLongSafe(dr.Item(11), 0)
            _AccountAccumulatedAmortization = CLongSafe(dr.Item(12), 0)
            _AccountValueDecrease = CLongSafe(dr.Item(13), 0)
            _AccountValueIncrease = CLongSafe(dr.Item(14), 0)
            _AccountRevaluedPortionAmmortization = CLongSafe(dr.Item(15), 0)
            _LiquidationUnitValue = CDblSafe(dr.Item(16), ROUNDUNITASSET, 0)
            _DefaultAmortizationPeriod = CIntSafe(dr.Item(17), 0)
            _AcquisitionAccountValuePerUnit = CDblSafe(dr.Item(18), ROUNDUNITASSET, 0)
            _Amount = CIntSafe(dr.Item(19), 0)
            _AcquisitionAccountValue = CDblSafe(dr.Item(20), 2, 0)
            _AmortizationAccountValue = CDblSafe(dr.Item(23), 2, 0)
            _AmortizationAccountValuePerUnit = CDblSafe(dr.Item(24), ROUNDUNITASSET, 0)
            _ValueIncreaseAmmortizationAccountValue = CDblSafe(dr.Item(25), 2, 0)
            _ValueIncreaseAmmortizationAccountValuePerUnit = CDblSafe(dr.Item(26), ROUNDUNITASSET, 0)
            _ContinuedUsage = ConvertDbBoolean(CIntSafe(dr.Item(27), 0))
            _AmortizationCalculatedForMonths = CIntSafe(dr.Item(28), 0)
            _InsertDate = CTimeStampSafe(dr.Item(29))
            _UpdateDate = CTimeStampSafe(dr.Item(30))
            _ID = CIntSafe(dr.Item(31), 0)

            If CDblSafe(dr.Item(21), ROUNDUNITASSET, 0) < 0 Then
                _ValueDecreaseAccountValuePerUnit = -CDblSafe(dr.Item(21), ROUNDUNITASSET, 0)
                _ValueIncreaseAccountValuePerUnit = 0
            ElseIf CDblSafe(dr.Item(21), ROUNDUNITASSET, 0) > 0 Then
                _ValueIncreaseAccountValuePerUnit = CDblSafe(dr.Item(21), ROUNDUNITASSET, 0)
                _ValueDecreaseAccountValuePerUnit = 0
            Else
                _ValueIncreaseAccountValuePerUnit = 0
                _ValueDecreaseAccountValuePerUnit = 0
            End If
            If CDblSafe(dr.Item(22), 0) < 0 Then
                _ValueDecreaseAccountValue = -CDblSafe(dr.Item(22), 2, 0)
                _ValueIncreaseAccountValue = 0
            ElseIf CDblSafe(dr.Item(22), 0) > 0 Then
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

            _IsInvoiceBound = (_AcquisitionJournalEntryDocType = DocumentType.InvoiceReceived _
                OrElse _AcquisitionJournalEntryDocType = DocumentType.InvoiceMade)

            _AcquisitionAccountValueCorrection = Convert.ToInt32(Math.Floor(CRound(_AcquisitionAccountValue _
                - (_AcquisitionAccountValuePerUnit * _Amount), 2) * 100))
            _AmortizationAccountValueCorrection = Convert.ToInt32(Math.Floor(CRound(_AmortizationAccountValue _
                - (_AmortizationAccountValuePerUnit * _Amount), 2) * 100))
            _ValueDecreaseAccountValueCorrection = Convert.ToInt32(Math.Floor(CRound(_ValueDecreaseAccountValue _
                - (_ValueDecreaseAccountValuePerUnit * _Amount), 2) * 100))
            _ValueIncreaseAccountValueCorrection = Convert.ToInt32(Math.Floor(CRound(_ValueIncreaseAccountValue _
                - (_ValueIncreaseAccountValuePerUnit * _Amount), 2) * 100))
            _ValueIncreaseAmmortizationAccountValueCorrection = Convert.ToInt32(Math.Floor(CRound(_ValueIncreaseAmmortizationAccountValue _
                - (_ValueIncreaseAmmortizationAccountValuePerUnit * _Amount), 2) * 100))

            RecalculateValues(False)

            _ChronologyValidator = AcquisitionChronologicValidator.GetAcquisitionChronologicValidator( _
                _ID, _AcquisitionDate, _ContinuedUsage, parentChronologyValidator)

            MarkOld()

            ValidationRules.CheckRules()

        End Sub


        Protected Overrides Sub DataPortal_Insert()
            If Not CanAddObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecurityInsertDenied)
            CheckIfCanUpdate()
            DoSave(False)
        End Sub

        Protected Overrides Sub DataPortal_Update()
            If Not CanEditObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecurityUpdateDenied)
            CheckIfCanUpdate()
            DoSave(False)
        End Sub

        ''' <summary>
        ''' Does a save operation from server side. Doesn't check for critical rules 
        ''' (fetch or programatical error within transaction crashes program).
        ''' Critical rules CheckIfInventoryNumberUnique, CheckIfLimitingOperationsExists, 
        ''' CheckIfDateIsValid needs to be invoked before starting a transaction.
        ''' </summary>
        Friend Sub SaveChild(ByVal journalEntryID As Integer, ByVal financialDataReadOnly As Boolean)
            _AcquisitionJournalEntryID = journalEntryID
            DoSave(financialDataReadOnly)
        End Sub

        Private Sub DoSave(ByVal financialDataReadOnly As Boolean)

            Dim myComm As SQLCommand
            If IsNew Then

                myComm = New SQLCommand("InsertLongTermAsset")
                AddAcountsAndValuesParams(myComm)
                AddAmortizationRelatedParams(myComm)

            Else

                If _ChronologyValidator.FinancialDataCanChange AndAlso Not financialDataReadOnly Then

                    myComm = New SQLCommand("UpdateLongTermAsset1")
                    AddAcountsAndValuesParams(myComm)
                    AddAmortizationRelatedParams(myComm)

                ElseIf (Not _ChronologyValidator.FinancialDataCanChange OrElse financialDataReadOnly) _
                    AndAlso Not _ChronologyValidator.AmortizationIsCalculated Then

                    myComm = New SQLCommand("UpdateLongTermAsset2")
                    AddAmortizationRelatedParams(myComm)

                ElseIf (Not _ChronologyValidator.FinancialDataCanChange OrElse financialDataReadOnly) _
                    AndAlso _ChronologyValidator.AmortizationIsCalculated Then

                    myComm = New SQLCommand("UpdateLongTermAsset3")

                Else
                    Throw New Exception(String.Format(My.Resources.Assets_LongTermAsset_InvalidRulesConfiguration, _
                        _IsInvoiceBound.ToString, _ChronologyValidator.FinancialDataCanChange.ToString, _
                        _ChronologyValidator.AmortizationIsCalculated.ToString))
                End If

                myComm.AddParam("?TD", _ID)

            End If

            myComm.AddParam("?JD", _AcquisitionJournalEntryID)
            myComm.AddParam("?NM", _Name.Trim)
            myComm.AddParam("?GP", _LegalGroup.Trim)
            myComm.AddParam("?MU", _MeasureUnit.Trim)
            myComm.AddParam("?VN", _InventoryNumber.Trim)
            If CustomGroupInfo Is Nothing OrElse CustomGroupInfo.IsEmpty Then
                myComm.AddParam("?CG", 0)
            Else
                myComm.AddParam("?CG", CustomGroupInfo.ID)
            End If

            _UpdateDate = GetCurrentTimeStamp()
            If Me.IsNew Then _InsertDate = _UpdateDate
            myComm.AddParam("?UD", _UpdateDate.ToUniversalTime)

            myComm.Execute()

            If IsNew Then _ID = Convert.ToInt32(myComm.LastInsertID)

            MarkOld()

        End Sub

        Private Sub AddAcountsAndValuesParams(ByRef myComm As SQLCommand)
            myComm.AddParam("?CN", _Amount)
            myComm.AddParam("?UV", CRound(_AcquisitionAccountValuePerUnit, ROUNDUNITASSET))
            myComm.AddParam("?AA", _AccountAcquisition)
            myComm.AddParam("?CA", CRound(_AmortizationAccountValue))
            myComm.AddParam("?TV", CRound(_AcquisitionAccountValue))
            myComm.AddParam("?AC", _AccountAccumulatedAmortization)
            myComm.AddParam("?FA", _AccountValueIncrease)
            myComm.AddParam("?FB", _AccountValueDecrease)
            myComm.AddParam("?FC", _AccountRevaluedPortionAmmortization)
            myComm.AddParam("?FD", ValueRevaluedPortionPerUnit)
            myComm.AddParam("?FE", ValueRevaluedPortion)
            myComm.AddParam("?FG", CRound(_AmortizationAccountValuePerUnit, ROUNDUNITASSET))
            myComm.AddParam("?FH", CRound(_ValueIncreaseAmmortizationAccountValuePerUnit, ROUNDUNITASSET))
            myComm.AddParam("?FI", CRound(_ValueIncreaseAmmortizationAccountValue))
        End Sub

        Private Sub AddAmortizationRelatedParams(ByRef myComm As SQLCommand)
            myComm.AddParam("?LQ", CRound(_LiquidationUnitValue, ROUNDUNITASSET))
            myComm.AddParam("?DP", _DefaultAmortizationPeriod)
            myComm.AddParam("?FJ", ConvertDbBoolean(_ContinuedUsage))
            myComm.AddParam("?FK", _AmortizationCalculatedForMonths)
        End Sub


        Private Overloads Sub DataPortal_Delete(ByVal criteria As Criteria)
            If Not CanDeleteObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecurityUpdateDenied)
            CheckIfAnyOperationExists(criteria.Id, True)
            DoDelete(criteria.Id)
        End Sub

        Private Shared Sub DoDelete(ByVal nID As Integer)
            Dim myComm As New SQLCommand("DeleteLongTermAsset")
            myComm.AddParam("?LD", nID)
            myComm.Execute()
        End Sub



        ''' <summary>
        ''' Throws an error if there are any operations with the LTA with ID=nID. Used before deletion.
        ''' </summary>
        Friend Shared Sub CheckIfAnyOperationExists(ByVal nID As Integer, _
            ByVal throwOnInvoiceBasedAcquisition As Boolean)

            Dim myComm As New SQLCommand("CheckIfLongTermAssetCanBeDeleted")
            myComm.AddParam("?AD", nID)

            Using myData As DataTable = myComm.Fetch

                If myData.Rows.Count < 1 Then Throw New Exception(String.Format( _
                    My.Resources.Common_ObjectNotFound, My.Resources.Assets_LongTermAsset_TypeName, _
                    nID.ToString()))

                If myData.Rows.Count > 1 Then
                    Throw New Exception(String.Format(My.Resources.Assets_LongTermAsset_OperationsExist, nID.ToString()))
                End If

                If throwOnInvoiceBasedAcquisition Then

                    Dim journalEntryDocType As DocumentType = Utilities.ConvertDatabaseCharID(Of DocumentType) _
                        (CStrSafe(myData.Rows(0).Item(0)))

                    If journalEntryDocType = DocumentType.InvoiceMade OrElse _
                        journalEntryDocType = DocumentType.InvoiceReceived Then

                        Throw New Exception(My.Resources.Assets_LongTermAsset_InvalidDelete)

                    End If

                End If

            End Using

        End Sub

        ''' <summary>
        ''' Throws an error if the inventory number specified is not unique
        ''' </summary>
        Friend Sub CheckIfInventoryNumberUnique()

            If StringIsNullOrEmpty(_InventoryNumber) Then Exit Sub

            Dim myComm As New SQLCommand("CheckIfInventoryNumUniqueForLTA")
            myComm.AddParam("?LD", _ID)
            myComm.AddParam("?NM", _InventoryNumber.Trim)

            Using myData As DataTable = myComm.Fetch
                If myData.Rows.Count > 0 Then
                    Throw New Exception(String.Format(My.Resources.Assets_LongTermAsset_InventoryNumberNotUnique, Me.ToString()))
                End If
            End Using

        End Sub

        Friend Sub CheckIfCanDelete()
            CheckIfAnyOperationExists(_ID, False)
        End Sub

        Friend Sub CheckIfCanUpdate()

            CheckIfInventoryNumberUnique()

            If Not IsNew Then

                _ChronologyValidator = AcquisitionChronologicValidator. _
                    GetAcquisitionChronologicValidator(_ChronologyValidator)

                Me.ValidationRules.CheckRules()
                If Not Me.IsValid Then
                    Throw New Exception(String.Format(My.Resources.Common_ContainsErrors, vbCrLf, _
                        GetAllBrokenRules()))
                End If

                CheckIfUpdateDateChanged()

            End If

        End Sub

        Private Sub CheckIfUpdateDateChanged()

            Dim myComm As New SQLCommand("CheckIfLongTermAssetUpdateDateChanged")
            myComm.AddParam("?CD", _ID)

            Using myData As DataTable = myComm.Fetch

                If myData.Rows.Count < 1 OrElse CDateTimeSafe(myData.Rows(0).Item(0), _
                    Date.MinValue) = Date.MinValue Then

                    Throw New Exception(String.Format(My.Resources.Common_ObjectNotFound, _
                        My.Resources.Assets_LongTermAsset_TypeName, _ID.ToString))

                End If

                If CTimeStampSafe(myData.Rows(0).Item(0)) <> _UpdateDate Then

                    Throw New Exception(My.Resources.Common_UpdateDateHasChanged)

                End If

            End Using

        End Sub


        Friend Function GetTotalBookEntryList() As BookEntryInternalList

            Dim result As BookEntryInternalList = _
               BookEntryInternalList.NewBookEntryInternalList(BookEntryType.Debetas)

            Dim debetSum As Double = 0
            Dim creditSum As Double = 0

            Dim acquisitionAccountBookEntry As BookEntryInternal = _
                BookEntryInternal.NewBookEntryInternal(BookEntryType.Debetas)
            acquisitionAccountBookEntry.Account = _AccountAcquisition
            acquisitionAccountBookEntry.Ammount = CRound(_AcquisitionAccountValue)
            result.Add(acquisitionAccountBookEntry)

            debetSum = CRound(_AcquisitionAccountValue)

            If CRound(_AmortizationAccountValue) > 0 Then

                Dim amortizationAccountBookEntry As BookEntryInternal = _
                    BookEntryInternal.NewBookEntryInternal(BookEntryType.Kreditas)
                amortizationAccountBookEntry.Account = _AccountAccumulatedAmortization
                amortizationAccountBookEntry.Ammount = CRound(_AmortizationAccountValue)
                result.Add(amortizationAccountBookEntry)

                creditSum = CRound(creditSum + _AmortizationAccountValue)

            End If

            If CRound(_ValueDecreaseAccountValue) > 0 Then

                Dim valueDecreaseAccountBookEntry As BookEntryInternal = _
                    BookEntryInternal.NewBookEntryInternal(BookEntryType.Kreditas)
                valueDecreaseAccountBookEntry.Account = _AccountValueDecrease
                valueDecreaseAccountBookEntry.Ammount = CRound(_ValueDecreaseAccountValue)
                result.Add(valueDecreaseAccountBookEntry)

                creditSum = CRound(creditSum + _ValueDecreaseAccountValue)

            End If

            If CRound(_ValueIncreaseAccountValue) > 0 Then

                Dim valueIncreaseAccountBookEntry As BookEntryInternal = _
                    BookEntryInternal.NewBookEntryInternal(BookEntryType.Debetas)
                valueIncreaseAccountBookEntry.Account = _AccountValueIncrease
                valueIncreaseAccountBookEntry.Ammount = CRound(_ValueIncreaseAccountValue)
                result.Add(valueIncreaseAccountBookEntry)

                debetSum = CRound(debetSum + _ValueIncreaseAccountValue)

            End If

            If CRound(_ValueIncreaseAmmortizationAccountValue) > 0 Then

                Dim valueIncreaseAmmortizationAccountBookEntry As BookEntryInternal = _
                    BookEntryInternal.NewBookEntryInternal(BookEntryType.Kreditas)
                valueIncreaseAmmortizationAccountBookEntry.Account = _AccountRevaluedPortionAmmortization
                valueIncreaseAmmortizationAccountBookEntry.Ammount = CRound(_ValueIncreaseAmmortizationAccountValue)
                result.Add(valueIncreaseAmmortizationAccountBookEntry)

                creditSum = CRound(creditSum + _ValueIncreaseAmmortizationAccountValue)

            End If

            Return result

        End Function

#End Region

    End Class

End Namespace