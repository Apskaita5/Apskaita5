Imports ApskaitaObjects.Attributes

Namespace Assets

    ''' <summary>
    ''' Represents a helper object that contains information about a change
    ''' in long term asset financial state (accounts balancies).
    ''' </summary>
    ''' <remarks>Should only be used as a child of a <see cref="OperationDeltaList">OperationDeltaList</see>.</remarks>
    <Serializable()> _
    Public Class OperationDelta
        Inherits ReadOnlyBase(Of OperationDelta)

#Region " Business Methods "

        Private _ID As Integer = 0
        Private _OperationType As LtaOperationType = LtaOperationType.Discard
        Private _AccountChangeType As LtaAccountChangeType = LtaAccountChangeType.AcquisitionAccount
        Private _Date As Date = Today
        Private _NewAccount As Long = 0
        Private _AcquisitionAccountValueChange As Double = 0
        Private _AcquisitionAccountValueChangePerUnit As Double = 0
        Private _AmortizationAccountValueChange As Double = 0
        Private _AmortizationAccountValueChangePerUnit As Double = 0
        Private _ValueDecreaseAccountValueChange As Double = 0
        Private _ValueDecreaseAccountValueChangePerUnit As Double = 0
        Private _ValueIncreaseAccountValueChange As Double = 0
        Private _ValueIncreaseAccountValueChangePerUnit As Double = 0
        Private _ValueIncreaseAmmortizationAccountValueChange As Double = 0
        Private _ValueIncreaseAmmortizationAccountValueChangePerUnit As Double = 0
        Private _ValueChange As Double = 0
        Private _ValueChangePerUnit As Double = 0
        Private _AmmountChange As Integer = 0
        Private _NewAmortizationPeriod As Integer = 0
        Private _UsageLength As Integer = 0


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
        ''' Gets a type of the long term asset account change operation.
        ''' </summary>
        ''' <remarks>Only relevant when the <see cref="OperationType">OperationType</see>
        ''' is set to <see cref="LtaOperationType.AccountChange">LtaOperationType.AccountChange</see>.
        ''' Value is stored in the database field turtas_op.AccountOperationType.</remarks>
        Public ReadOnly Property AccountChangeType() As LtaAccountChangeType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AccountChangeType
            End Get
        End Property

        ''' <summary>
        ''' Gets a date of the long term asset operation.
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas_op.OperationDate.</remarks>
        Public ReadOnly Property [Date]() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Date
            End Get
        End Property

        ''' <summary>
        ''' Gets a new <see cref="General.Account.ID">account</see> for the long term asset 
        ''' of type <see cref="AccountChangeType">AccountChangeType</see>.
        ''' </summary>
        ''' <remarks>Only relevant when the <see cref="OperationType">OperationType</see>
        ''' is set to <see cref="LtaOperationType.AccountChange">LtaOperationType.AccountChange</see>.
        ''' Value is stored in the database field turtas_op.AccountCorresponding.</remarks>
        Public ReadOnly Property NewAccount() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _NewAccount
            End Get
        End Property

        ''' <summary>
        ''' A change of the balance for the <see cref="LongTermAsset.AccountAcquisition">AccountAcquisition</see> made by the operation.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property AcquisitionAccountValueChange() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AcquisitionAccountValueChange)
            End Get
        End Property

        ''' <summary>
        ''' A change of the balance for the <see cref="LongTermAsset.AccountAcquisition">AccountAcquisition</see> per unit made by the operation.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property AcquisitionAccountValueChangePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AcquisitionAccountValueChangePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        ''' <summary>
        ''' A change of the balance for the <see cref="LongTermAsset.AccountAccumulatedAmortization">AccountAccumulatedAmortization</see> made by the operation.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property AmortizationAccountValueChange() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AmortizationAccountValueChange)
            End Get
        End Property

        ''' <summary>
        ''' A change of the balance for the <see cref="LongTermAsset.AccountAccumulatedAmortization">AccountAccumulatedAmortization</see> per unit made by the operation.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property AmortizationAccountValueChangePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AmortizationAccountValueChangePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        ''' <summary>
        ''' A change of the balance for the <see cref="LongTermAsset.AccountValueDecrease">AccountValueDecrease</see> made by the operation.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property ValueDecreaseAccountValueChange() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ValueDecreaseAccountValueChange)
            End Get
        End Property

        ''' <summary>
        ''' A change of the balance for the <see cref="LongTermAsset.AccountValueDecrease">AccountValueDecrease</see> per unit made by the operation.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property ValueDecreaseAccountValueChangePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ValueDecreaseAccountValueChangePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        ''' <summary>
        ''' A change of the balance for the <see cref="LongTermAsset.AccountValueIncrease">AccountValueIncrease</see> made by the operation.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property ValueIncreaseAccountValueChange() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ValueIncreaseAccountValueChange)
            End Get
        End Property

        ''' <summary>
        ''' A change of the balance for the <see cref="LongTermAsset.AccountValueIncrease">AccountValueIncrease</see> per unit made by the operation.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property ValueIncreaseAccountValueChangePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ValueIncreaseAccountValueChangePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        ''' <summary>
        ''' A change of the balance for the <see cref="LongTermAsset.AccountRevaluedPortionAmmortization">AccountRevaluedPortionAmmortization</see> made by the operation.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property ValueIncreaseAmmortizationAccountValueChange() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ValueIncreaseAmmortizationAccountValueChange)
            End Get
        End Property

        ''' <summary>
        ''' A change of the balance for the <see cref="LongTermAsset.AccountRevaluedPortionAmmortization">AccountRevaluedPortionAmmortization</see> per unit made by the operation.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property ValueIncreaseAmmortizationAccountValueChangePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ValueIncreaseAmmortizationAccountValueChangePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        ''' <summary>
        ''' A change of the total value of the long term asset made by the operation.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property ValueChange() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ValueChange)
            End Get
        End Property

        ''' <summary>
        ''' A change of the unit value of the long term asset made by the operation.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDUNITASSET)> _
        Public ReadOnly Property ValueChangePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ValueChangePerUnit, ROUNDUNITASSET)
            End Get
        End Property

        ''' <summary>
        ''' A change of the amount of the long term asset made by the operation.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property AmmountChange() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AmmountChange
            End Get
        End Property

        ''' <summary>
        ''' Gets a new amortization period for the asset set by the long term asset operation.
        ''' </summary>
        ''' <remarks>Only relevant when the <see cref="OperationType">OperationType</see>
        ''' is set to <see cref="LtaOperationType.AmortizationPeriod">LtaOperationType.AmortizationPeriod</see>.
        ''' Value is stored in the database field turtas_op.NewAmortizationPeriod.</remarks>
        Public ReadOnly Property NewAmortizationPeriod() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _NewAmortizationPeriod
            End Get
        End Property

        ''' <summary>
        ''' Gets a length of the period that the amortization is calculated for.
        ''' </summary>
        ''' <remarks>Only relevant when the <see cref="OperationType">OperationType</see>
        ''' is set to <see cref="LtaOperationType.Amortization">LtaOperationType.Amortization</see>.
        ''' Value is stored in the database field turtas_op.NewAmortizationPeriod.</remarks>
        Public ReadOnly Property UsageLength() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _UsageLength
            End Get
        End Property


        Protected Overrides Function GetIdValue() As Object
            Return _ID
        End Function

        Public Overrides Function ToString() As String
            Return String.Format("{0}, {1}, ID={2}", _Date.ToShortDateString, _
                _OperationType.ToString(), _ID.ToString())
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function GetOperationDelta(ByVal dr As DataRow) As OperationDelta
            Return New OperationDelta(dr)
        End Function


        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal dr As DataRow)
            Fetch(dr)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal dr As DataRow)

            _ID = CIntSafe(dr.Item(1), 0)
            _OperationType = Utilities.ConvertDatabaseCharID(Of LtaOperationType) _
                (CStrSafe(dr.Item(2)))
            If _OperationType = LtaOperationType.AccountChange _
                AndAlso Not StringIsNullOrEmpty(CStrSafe(dr.Item(3))) Then
                _AccountChangeType = Utilities.ConvertDatabaseCharID(Of LtaAccountChangeType)(CStrSafe(dr.Item(3)))
            End If
            _Date = CDateSafe(dr.Item(4), Today)
            _NewAccount = CLongSafe(dr.Item(5), 0)
            _ValueChangePerUnit = CDblSafe(dr.Item(6), ROUNDUNITASSET, 0)
            _AmmountChange = CIntSafe(dr.Item(7), 0)
            _ValueChange = CDblSafe(dr.Item(8), 2, 0)
            _NewAmortizationPeriod = CIntSafe(dr.Item(9), 0)
            _UsageLength = CIntSafe(dr.Item(10), 0)
            _AcquisitionAccountValueChange = CDblSafe(dr.Item(11), 2, 0)
            _AcquisitionAccountValueChangePerUnit = CDblSafe(dr.Item(12), ROUNDUNITASSET, 0)
            _AmortizationAccountValueChange = CDblSafe(dr.Item(13), 2, 0)
            _AmortizationAccountValueChangePerUnit = CDblSafe(dr.Item(14), ROUNDUNITASSET, 0)
            _ValueDecreaseAccountValueChange = CDblSafe(dr.Item(15), 2, 0)
            _ValueDecreaseAccountValueChangePerUnit = CDblSafe(dr.Item(16), ROUNDUNITASSET, 0)
            _ValueIncreaseAccountValueChange = CDblSafe(dr.Item(17), 2, 0)
            _ValueIncreaseAccountValueChangePerUnit = CDblSafe(dr.Item(18), ROUNDUNITASSET, 0)
            _ValueIncreaseAmmortizationAccountValueChange = CDblSafe(dr.Item(19), 2, 0)
            _ValueIncreaseAmmortizationAccountValueChangePerUnit = CDblSafe(dr.Item(20), ROUNDUNITASSET, 0)

        End Sub

#End Region

    End Class

End Namespace