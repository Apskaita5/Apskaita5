Namespace Assets

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


        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        Public ReadOnly Property Name() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Name.Trim
            End Get
        End Property

        Public ReadOnly Property MeasureUnit() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _MeasureUnit.Trim
            End Get
        End Property

        Public ReadOnly Property LegalGroup() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _LegalGroup.Trim
            End Get
        End Property

        Public ReadOnly Property CustomGroup() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _CustomGroup.Trim
            End Get
        End Property

        Public ReadOnly Property AcquisitionDate() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AcquisitionDate
            End Get
        End Property

        Public ReadOnly Property AcquisitionJournalEntryID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AcquisitionJournalEntryID
            End Get
        End Property

        Public ReadOnly Property AcquisitionJournalEntryDocNumber() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AcquisitionJournalEntryDocNumber.Trim
            End Get
        End Property

        Public ReadOnly Property AcquisitionJournalEntryDocContent() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AcquisitionJournalEntryDocContent.Trim
            End Get
        End Property

        Public ReadOnly Property InventoryNumber() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _InventoryNumber.Trim
            End Get
        End Property

        Public ReadOnly Property AccountAcquisition() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AccountAcquisition
            End Get
        End Property

        Public ReadOnly Property AccountAccumulatedAmortization() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AccountAccumulatedAmortization
            End Get
        End Property

        Public ReadOnly Property AccountValueIncrease() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AccountValueIncrease
            End Get
        End Property

        Public ReadOnly Property AccountValueDecrease() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AccountValueDecrease
            End Get
        End Property

        Public ReadOnly Property AccountRevaluedPortionAmmortization() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AccountRevaluedPortionAmmortization
            End Get
        End Property

        Public ReadOnly Property LiquidationUnitValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_LiquidationUnitValue, ROUNDUNITASSET)
            End Get
        End Property

        Public ReadOnly Property ContinuedUsage() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ContinuedUsage
            End Get
        End Property

        Public ReadOnly Property DefaultAmortizationPeriod() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DefaultAmortizationPeriod
            End Get
        End Property

        Public ReadOnly Property AcquisitionAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AcquisitionAccountValue)
            End Get
        End Property

        Public ReadOnly Property AcquisitionAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AcquisitionAccountValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        Public ReadOnly Property AmortizationAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AmortizationAccountValue)
            End Get
        End Property

        Public ReadOnly Property AmortizationAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AmortizationAccountValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        Public ReadOnly Property ValueDecreaseAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ValueDecreaseAccountValue)
            End Get
        End Property

        Public ReadOnly Property ValueDecreaseAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ValueDecreaseAccountValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        Public ReadOnly Property ValueIncreaseAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ValueIncreaseAccountValue)
            End Get
        End Property

        Public ReadOnly Property ValueIncreaseAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ValueIncreaseAccountValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        Public ReadOnly Property ValueIncreaseAmmortizationAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ValueIncreaseAmmortizationAccountValue)
            End Get
        End Property

        Public ReadOnly Property ValueIncreaseAmmortizationAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ValueIncreaseAmmortizationAccountValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        Public ReadOnly Property Value() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_Value)
            End Get
        End Property

        Public ReadOnly Property ValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        Public ReadOnly Property Ammount() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Ammount
            End Get
        End Property

        Public ReadOnly Property ValueRevaluedPortion() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ValueRevaluedPortion)
            End Get
        End Property

        Public ReadOnly Property ValueRevaluedPortionPerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ValueRevaluedPortionPerUnit, ROUNDUNITASSET)
            End Get
        End Property

        Public ReadOnly Property BeforeAcquisitionAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_BeforeAcquisitionAccountValue)
            End Get
        End Property

        Public ReadOnly Property BeforeAcquisitionAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_BeforeAcquisitionAccountValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        Public ReadOnly Property BeforeAmortizationAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_BeforeAmortizationAccountValue)
            End Get
        End Property

        Public ReadOnly Property BeforeAmortizationAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_BeforeAmortizationAccountValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        Public ReadOnly Property BeforeValueDecreaseAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_BeforeValueDecreaseAccountValue)
            End Get
        End Property

        Public ReadOnly Property BeforeValueDecreaseAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_BeforeValueDecreaseAccountValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        Public ReadOnly Property BeforeValueIncreaseAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_BeforeValueIncreaseAccountValue)
            End Get
        End Property

        Public ReadOnly Property BeforeValueIncreaseAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_BeforeValueIncreaseAccountValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        Public ReadOnly Property BeforeValueIncreaseAmmortizationAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_BeforeValueIncreaseAmmortizationAccountValue)
            End Get
        End Property

        Public ReadOnly Property BeforeValueIncreaseAmmortizationAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_BeforeValueIncreaseAmmortizationAccountValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        Public ReadOnly Property BeforeValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_BeforeValue)
            End Get
        End Property

        Public ReadOnly Property BeforeValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_BeforeValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        Public ReadOnly Property BeforeAmmount() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _BeforeAmmount
            End Get
        End Property

        Public ReadOnly Property ChangeAcquisitionAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ChangeAcquisitionAccountValue)
            End Get
        End Property

        Public ReadOnly Property ChangeAcquisitionAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ChangeAcquisitionAccountValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        Public ReadOnly Property ChangeAmortizationAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ChangeAmortizationAccountValue)
            End Get
        End Property

        Public ReadOnly Property ChangeAmortizationAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ChangeAmortizationAccountValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        Public ReadOnly Property ChangeValueDecreaseAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ChangeValueDecreaseAccountValue)
            End Get
        End Property

        Public ReadOnly Property ChangeValueDecreaseAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ChangeValueDecreaseAccountValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        Public ReadOnly Property ChangeValueIncreaseAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ChangeValueIncreaseAccountValue)
            End Get
        End Property

        Public ReadOnly Property ChangeValueIncreaseAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ChangeValueIncreaseAccountValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        Public ReadOnly Property ChangeValueIncreaseAmmortizationAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ChangeValueIncreaseAmmortizationAccountValue)
            End Get
        End Property

        Public ReadOnly Property ChangeValueIncreaseAmmortizationAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ChangeValueIncreaseAmmortizationAccountValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        Public ReadOnly Property ChangeValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ChangeValue)
            End Get
        End Property

        Public ReadOnly Property ChangeValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ChangeValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        Public ReadOnly Property ChangeAmmount() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ChangeAmmount
            End Get
        End Property

        Public ReadOnly Property ChangeAmmountAcquired() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ChangeAmmountAcquired
            End Get
        End Property

        Public ReadOnly Property ChangeValueAcquired() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ChangeValueAcquired)
            End Get
        End Property

        Public ReadOnly Property ChangeAmmountTransfered() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ChangeAmmountTransfered
            End Get
        End Property

        Public ReadOnly Property ChangeValueTransfered() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ChangeValueTransfered)
            End Get
        End Property

        Public ReadOnly Property ChangeAmmountDiscarded() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ChangeAmmountDiscarded
            End Get
        End Property

        Public ReadOnly Property ChangeValueDiscarded() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ChangeValueDiscarded)
            End Get
        End Property

        Public ReadOnly Property AfterAcquisitionAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AfterAcquisitionAccountValue)
            End Get
        End Property

        Public ReadOnly Property AfterAcquisitionAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AfterAcquisitionAccountValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        Public ReadOnly Property AfterAmortizationAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AfterAmortizationAccountValue)
            End Get
        End Property

        Public ReadOnly Property AfterAmortizationAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AfterAmortizationAccountValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        Public ReadOnly Property AfterValueDecreaseAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AfterValueDecreaseAccountValue)
            End Get
        End Property

        Public ReadOnly Property AfterValueDecreaseAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AfterValueDecreaseAccountValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        Public ReadOnly Property AfterValueIncreaseAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AfterValueIncreaseAccountValue)
            End Get
        End Property

        Public ReadOnly Property AfterValueIncreaseAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AfterValueIncreaseAccountValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        Public ReadOnly Property AfterValueIncreaseAmmortizationAccountValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AfterValueIncreaseAmmortizationAccountValue)
            End Get
        End Property

        Public ReadOnly Property AfterValueIncreaseAmmortizationAccountValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AfterValueIncreaseAmmortizationAccountValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        Public ReadOnly Property AfterValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AfterValue)
            End Get
        End Property

        Public ReadOnly Property AfterValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AfterValuePerUnit, ROUNDUNITASSET)
            End Get
        End Property

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

        Friend Shared Function GetLongTermAssetInfo(ByVal AcquisitionDataRow As DataRow, _
            ByVal StatusBeforeDataRow As DataRow, ByVal ChangesDataRow As DataRow, _
            ByVal DateFrom As Date, ByVal DateTo As Date) As LongTermAssetInfo
            Return New LongTermAssetInfo(AcquisitionDataRow, StatusBeforeDataRow, _
                ChangesDataRow, DateFrom, DateTo)
        End Function

        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal AcquisitionDataRow As DataRow, _
            ByVal StatusBeforeDataRow As DataRow, ByVal ChangesDataRow As DataRow, _
            ByVal DateFrom As Date, ByVal DateTo As Date)
            Fetch(AcquisitionDataRow, StatusBeforeDataRow, ChangesDataRow, DateFrom, DateTo)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal AcquisitionDataRow As DataRow, _
            ByVal StatusBeforeDataRow As DataRow, ByVal ChangesDataRow As DataRow, _
            ByVal DateFrom As Date, ByVal DateTo As Date)

            _ID = CIntSafe(AcquisitionDataRow.Item(0), 0)
            _Name = CStrSafe(AcquisitionDataRow.Item(1)).Trim
            _MeasureUnit = CStrSafe(AcquisitionDataRow.Item(2)).Trim
            _LegalGroup = CStrSafe(AcquisitionDataRow.Item(3)).Trim
            _CustomGroup = CStrSafe(AcquisitionDataRow.Item(4)).Trim
            _InventoryNumber = CStrSafe(AcquisitionDataRow.Item(5)).Trim
            _AcquisitionJournalEntryID = CIntSafe(AcquisitionDataRow.Item(6), 0)
            _AcquisitionDate = CDateSafe(AcquisitionDataRow.Item(7), Today)
            _AcquisitionJournalEntryDocNumber = CStrSafe(AcquisitionDataRow.Item(8)).Trim
            _AcquisitionJournalEntryDocContent = CStrSafe(AcquisitionDataRow.Item(9)).Trim
            _AcquisitionJournalEntryDocType = ConvertEnumHumanReadable( _
                ConvertEnumDatabaseStringCode(Of DocumentType)(CStrSafe(AcquisitionDataRow.Item(10))))
            _AccountAcquisition = CIntSafe(AcquisitionDataRow.Item(11), 0)
            _AccountAccumulatedAmortization = CIntSafe(AcquisitionDataRow.Item(12), 0)
            _AccountValueDecrease = CIntSafe(AcquisitionDataRow.Item(13), 0)
            _AccountValueIncrease = CIntSafe(AcquisitionDataRow.Item(14), 0)
            _AccountRevaluedPortionAmmortization = CIntSafe(AcquisitionDataRow.Item(15), 0)
            _LiquidationUnitValue = CDblSafe(AcquisitionDataRow.Item(16), ROUNDUNITASSET, 0)
            _DefaultAmortizationPeriod = CIntSafe(AcquisitionDataRow.Item(17), 0)
            _AcquisitionAccountValuePerUnit = CDblSafe(AcquisitionDataRow.Item(18), ROUNDUNITASSET, 0)
            _Ammount = CIntSafe(AcquisitionDataRow.Item(19), 0)
            _AcquisitionAccountValue = CDblSafe(AcquisitionDataRow.Item(20), 2, 0)
            _AmortizationAccountValue = CDblSafe(AcquisitionDataRow.Item(23), 2, 0)
            _AmortizationAccountValuePerUnit = CDblSafe(AcquisitionDataRow.Item(24), ROUNDUNITASSET, 0)
            _ValueIncreaseAmmortizationAccountValue = CDblSafe(AcquisitionDataRow.Item(25), 2, 0)
            _ValueIncreaseAmmortizationAccountValuePerUnit = CDblSafe(AcquisitionDataRow.Item(26), ROUNDUNITASSET, 0)
            _ContinuedUsage = ConvertDbBoolean(CIntSafe(AcquisitionDataRow.Item(27), 0))

            If CDblSafe(AcquisitionDataRow.Item(21)) < 0 Then
                _ValueDecreaseAccountValuePerUnit = -CDblSafe(AcquisitionDataRow.Item(21), ROUNDUNITASSET, 0)
                _ValueIncreaseAccountValuePerUnit = 0
            ElseIf CDblSafe(AcquisitionDataRow.Item(21)) > 0 Then
                _ValueIncreaseAccountValuePerUnit = CDblSafe(AcquisitionDataRow.Item(21), ROUNDUNITASSET, 0)
                _ValueDecreaseAccountValuePerUnit = 0
            Else
                _ValueIncreaseAccountValuePerUnit = 0
                _ValueDecreaseAccountValuePerUnit = 0
            End If
            If CDblSafe(AcquisitionDataRow.Item(22)) < 0 Then
                _ValueDecreaseAccountValue = -CDblSafe(AcquisitionDataRow.Item(22), 2, 0)
                _ValueIncreaseAccountValue = 0
            ElseIf CDblSafe(AcquisitionDataRow.Item(22)) > 0 Then
                _ValueIncreaseAccountValue = CDblSafe(AcquisitionDataRow.Item(22), 2, 0)
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



            If _AcquisitionDate.Date >= DateFrom.Date Then
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
            ElseIf StatusBeforeDataRow Is Nothing Then
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
                _BeforeAcquisitionAccountValue = CRound(CDblSafe(StatusBeforeDataRow.Item(1), 2, 0) + _
                    _AcquisitionAccountValue)
                _BeforeAcquisitionAccountValuePerUnit = CRound(CDblSafe(StatusBeforeDataRow.Item(2), ROUNDUNITASSET, 0) + _
                    _AcquisitionAccountValuePerUnit, ROUNDUNITASSET)
                _BeforeValueDecreaseAccountValue = CRound(CDblSafe(StatusBeforeDataRow.Item(3), 2, 0) + _
                    _ValueDecreaseAccountValue)
                _BeforeValueDecreaseAccountValuePerUnit = CRound(CDblSafe(StatusBeforeDataRow.Item(4), ROUNDUNITASSET, 0) + _
                    _ValueDecreaseAccountValuePerUnit, ROUNDUNITASSET)
                _BeforeValueIncreaseAccountValue = CRound(CDblSafe(StatusBeforeDataRow.Item(5), 2, 0) + _
                    _ValueIncreaseAccountValue)
                _BeforeValueIncreaseAccountValuePerUnit = CRound(CDblSafe(StatusBeforeDataRow.Item(6), ROUNDUNITASSET, 0) + _
                    _ValueIncreaseAccountValuePerUnit, ROUNDUNITASSET)
                _BeforeAmortizationAccountValue = CRound(CDblSafe(StatusBeforeDataRow.Item(7), 2, 0) + _
                    _AmortizationAccountValue)
                _BeforeAmortizationAccountValuePerUnit = CRound(CDblSafe(StatusBeforeDataRow.Item(8), ROUNDUNITASSET, 0) + _
                    _AmortizationAccountValuePerUnit, ROUNDUNITASSET)
                _BeforeValueIncreaseAmmortizationAccountValue = CRound(CDblSafe(StatusBeforeDataRow.Item(9), 2, 0) + _
                    _ValueIncreaseAmmortizationAccountValue)
                _BeforeValueIncreaseAmmortizationAccountValuePerUnit = _
                    CRound(CDblSafe(StatusBeforeDataRow.Item(10), ROUNDUNITASSET, 0) _
                    + _ValueIncreaseAmmortizationAccountValuePerUnit, ROUNDUNITASSET)
                _BeforeAmmount = _Ammount - CIntSafe(StatusBeforeDataRow.Item(11))
            End If
            _BeforeValue = CRound(_BeforeAcquisitionAccountValue - _BeforeAmortizationAccountValue _
                - _BeforeValueDecreaseAccountValue - _BeforeValueIncreaseAmmortizationAccountValue _
                + _BeforeValueIncreaseAccountValue)
            _BeforeValuePerUnit = CRound(_BeforeAcquisitionAccountValuePerUnit - _BeforeAmortizationAccountValuePerUnit _
                - _BeforeValueDecreaseAccountValuePerUnit - _BeforeValueIncreaseAmmortizationAccountValuePerUnit _
                + _BeforeValueIncreaseAccountValuePerUnit, ROUNDUNITASSET)



            If ChangesDataRow Is Nothing Then
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
                _ChangeAcquisitionAccountValue = CDblSafe(ChangesDataRow.Item(1), 2, 0)
                _ChangeAcquisitionAccountValuePerUnit = CDblSafe(ChangesDataRow.Item(2), ROUNDUNITASSET, 0)
                _ChangeValueDecreaseAccountValue = CDblSafe(ChangesDataRow.Item(3), 2, 0)
                _ChangeValueDecreaseAccountValuePerUnit = CDblSafe(ChangesDataRow.Item(4), ROUNDUNITASSET, 0)
                _ChangeValueIncreaseAccountValue = CDblSafe(ChangesDataRow.Item(5), 2, 0)
                _ChangeValueIncreaseAccountValuePerUnit = CDblSafe(ChangesDataRow.Item(6), ROUNDUNITASSET, 0)
                _ChangeAmortizationAccountValue = CDblSafe(ChangesDataRow.Item(7), 2, 0)
                _ChangeAmortizationAccountValuePerUnit = CDblSafe(ChangesDataRow.Item(8), ROUNDUNITASSET, 0)
                _ChangeValueIncreaseAmmortizationAccountValue = CDblSafe(ChangesDataRow.Item(9), 2, 0)
                _ChangeValueIncreaseAmmortizationAccountValuePerUnit = CDblSafe(ChangesDataRow.Item(10), ROUNDUNITASSET, 0)
                _ChangeAmmount = -CIntSafe(ChangesDataRow.Item(11), 0)
                _ChangeAmmountTransfered = CIntSafe(ChangesDataRow.Item(12), 0)
                _ChangeValueTransfered = CDblSafe(ChangesDataRow.Item(13), 2, 0)
                _ChangeAmmountDiscarded = CIntSafe(ChangesDataRow.Item(14), 0)
                _ChangeValueDiscarded = CDblSafe(ChangesDataRow.Item(15), 2, 0)
            End If
            If _AcquisitionDate.Date >= DateFrom.Date AndAlso _AcquisitionDate.Date <= DateTo.Date Then
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