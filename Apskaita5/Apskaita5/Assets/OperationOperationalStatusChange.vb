Imports Csla.Validation

Namespace Assets

    ''' <summary>
    ''' Represents a long term asset operational status change operation, 
    ''' i.e. starting or ending the use of the asset in an economic activity.
    ''' </summary>
    ''' <remarks>Values are stored in the database table turtas_op.
    ''' Operation data is persisted by the <see cref="OperationPersistenceObject">OperationPersistenceObject</see>.</remarks>
    <Serializable()> _
    Public Class OperationOperationalStatusChange
        Inherits BusinessBase(Of OperationOperationalStatusChange)
        Implements IGetErrorForListItem, IIsDirtyEnough

#Region " Business Methods "

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
        Private _BeginOperationalPeriod As Boolean = True


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
        ''' Gets a type of the long term asset operation, 
        ''' i.e. <see cref="LtaOperationType.UsingStart">LtaOperationType.UsingStart</see>
        ''' or <see cref="LtaOperationType.UsingEnd">LtaOperationType.UsingEnd</see>.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property [Type]() As LtaOperationType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If _BeginOperationalPeriod Then
                    Return LtaOperationType.UsingStart
                Else
                    Return LtaOperationType.UsingEnd
                End If
            End Get
        End Property

        ''' <summary>
        ''' Gets a type of the long term asset operation as a human readable (localized) string, 
        ''' i.e. <see cref="LtaOperationType.UsingStart">LtaOperationType.UsingStart</see>
        ''' or <see cref="LtaOperationType.UsingEnd">LtaOperationType.UsingEnd</see>.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property TypeHumanReadable() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return EnumValueAttribute.ConvertLocalizedName(Me.Type)
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
        ''' is used to validate a long term asset operational state change operation chronological business rules.</remarks>
        Public ReadOnly Property ChronologyValidator() As OperationChronologicValidator2
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ChronologyValidator
            End Get
        End Property

        ''' <summary>
        ''' Gets background information for the operation. 
        ''' Describes the long term asset state before and after 
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
        ''' Gets or sets a number of the long term asset operation document.
        ''' </summary>
        ''' <remarks>Value is stored in the database field turtas_op.ActNumber.</remarks>
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
        ''' Whether the operation sets long term asset status as operational.
        ''' </summary>
        ''' <remarks>Value designates operation type.</remarks>
        Public ReadOnly Property BeginOperationalPeriod() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _BeginOperationalPeriod
            End Get
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
        ''' Whether the <see cref="DocumentNumber">DocumentNumber</see> property is readonly.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DocumentNumberIsReadOnly() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return (IsChild OrElse _IsComplexAct)
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
                    OrElse Not StringIsNullOrEmpty(_DocumentNumber))
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
                    PropertyHasChanged("CurrentAmortizationAccountValue")
                    PropertyHasChanged("CurrentValueDecreaseAccountValue")
                    PropertyHasChanged("CurrentValueIncreaseAccountValue")
                    PropertyHasChanged("CurrentValueIncreaseAmortizationAccountValue")
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
                    PropertyHasChanged("CurrentAccount")
                    PropertyHasChanged("CurrentAccountBalance")

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


        Public Overrides Function Save() As OperationOperationalStatusChange

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
                Return String.Format(My.Resources.Assets_OperationOperationalStatusChange_ToStringChild, _
                _Background.AssetName, _ID.ToString())
            Else
                Return String.Format(My.Resources.Assets_OperationOperationalStatusChange_ToString, _
                    _Date.ToString("yyyy-MM-dd"), _Background.AssetName, _DocumentNumber, _
                    _ID.ToString())
            End If
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()

            ValidationRules.AddRule(AddressOf ChildStringPropertyValidation, _
                New Csla.Validation.RuleArgs("Content"))
            ValidationRules.AddRule(AddressOf ChildStringPropertyValidation, _
                New Csla.Validation.RuleArgs("DocumentNumber"))

            ValidationRules.AddRule(AddressOf DateValidation, _
                New CommonValidation.ChronologyRuleArgs("Date", "ChronologyValidator"))

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

            If DirectCast(target, OperationOperationalStatusChange).IsChild Then
                Return True
            Else
                Return CommonValidation.StringFieldValidation(target, e)
            End If

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

            Dim valObj As OperationOperationalStatusChange = _
                DirectCast(target, OperationOperationalStatusChange)

            If valObj._Background.CurrentAssetAmount < 1 Then
                e.Description = My.Resources.Assets_OperationOperationalStatusChange_CurrentAmountNull
                e.Severity = RuleSeverity.Error
                Return False
            ElseIf valObj._Background.CurrentUsageStatus AndAlso valObj._BeginOperationalPeriod Then
                e.Description = My.Resources.Assets_OperationOperationalStatusChange_AssetAlreadyOperational
                e.Severity = RuleSeverity.Error
                Return False
            ElseIf Not valObj._Background.CurrentUsageStatus AndAlso Not valObj._BeginOperationalPeriod Then
                e.Description = My.Resources.Assets_OperationOperationalStatusChange_AssetAlreadyNotOperational
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
        ''' Gets a new OperationOperationalStatusChange instance as a parent (standalone) document.
        ''' </summary>
        ''' <param name="beginOperationalPeriod">Whether the operation sets long term asset 
        ''' status as operational. If false, the operation sets the status as not operational.</param>
        ''' <param name="assetId">An ID of the asset that the operation operates on.</param>
        ''' <remarks></remarks>
        Public Shared Function NewOperationOperationalStatusChange(ByVal assetId As Integer, _
            ByVal beginOperationalPeriod As Boolean) As OperationOperationalStatusChange
            Return DataPortal.Create(Of OperationOperationalStatusChange) _
                (New Criteria(assetId, beginOperationalPeriod))
        End Function

        ''' <summary>
        ''' Gets a new OperationOperationalStatusChange instance as a child of some parent document.
        ''' </summary>
        ''' <param name="assetId">An ID of the asset that the operation operates on.</param>
        ''' <param name="beginOperationalPeriod">Whether the operation sets long term asset 
        ''' status as operational. If false, the operation sets the status as not operational.</param>
        ''' <param name="parentValidator">A parent document's IChronologyValidator.</param>
        ''' <param name="parentIsComplexAct">Whether the parent document is a complex 
        ''' long term asset operation.</param>
        ''' <remarks></remarks>
        Friend Shared Function NewOperationOperationalStatusChangeChild(ByVal assetId As Integer, _
            ByVal beginOperationalPeriod As Boolean, ByVal parentValidator As IChronologicValidator, _
            ByVal parentIsComplexAct As Boolean) As OperationOperationalStatusChange
            Return New OperationOperationalStatusChange(assetId, beginOperationalPeriod, _
                parentValidator, parentIsComplexAct)
        End Function


        ''' <summary>
        ''' Gets an existing OperationOperationalStatusChange instance from a database 
        ''' as a parent (standalone) document.
        ''' </summary>
        ''' <param name="id">An <see cref="OperationOperationalStatusChange.ID">ID of the operation</see>.</param>
        ''' <remarks></remarks>
        Public Shared Function GetOperationOperationalStatusChange(ByVal id As Integer) As OperationOperationalStatusChange
            Return DataPortal.Fetch(Of OperationOperationalStatusChange)(New Criteria(id))
        End Function

        ''' <summary>
        ''' Gets an existing OperationOperationalStatusChange instance from a database 
        ''' as a child of some parent document.
        ''' </summary>
        ''' <param name="operationID">An <see cref="OperationOperationalStatusChange.ID">ID of the operation</see>
        ''' to fetch.</param>
        ''' <param name="parentValidator">A parent document's IChronologyValidator.</param>
        ''' <remarks>An overload for the parent documents that do not support bulk 
        ''' background and chronology data loading.</remarks>
        Friend Shared Function GetOperationOperationalStatusChangeChild(ByVal operationID As Integer, _
            ByVal parentValidator As IChronologicValidator) As OperationOperationalStatusChange
            Return New OperationOperationalStatusChange(operationID, parentValidator)
        End Function

        ''' <summary>
        ''' Gets an existing OperationOperationalStatusChange instance from a database 
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
        Friend Shared Function GetOperationOperationalStatusChangeChild( _
            ByVal persistence As OperationPersistenceObject, _
            ByVal parentValidator As IChronologicValidator, _
            ByVal generalData As DataTable, ByVal deltaData As DataTable) As OperationOperationalStatusChange
            Return New OperationOperationalStatusChange(persistence, parentValidator, generalData, deltaData)
        End Function


        ''' <summary>
        ''' Deletes an existing OperationOperationalStatusChange instance from a database.
        ''' </summary>
        ''' <param name="id">An <see cref="OperationOperationalStatusChange.ID">ID of the operation</see> 
        ''' to delete.</param>
        ''' <remarks></remarks>
        Public Shared Sub DeleteOperationOperationalStatusChange(ByVal id As Integer)
            DataPortal.Delete(New Criteria(id))
        End Sub

        ''' <summary>
        ''' Deletes an existing OperationOperationalStatusChange child instance from a database.
        ''' </summary>
        ''' <remarks>Does a delete operation on server side. Doesn't check for critical rules 
        ''' (fetch or programatical error within transaction crashes program).
        ''' Critical rules checking method <see cref="CheckIfCanDeleteChild">CheckIfCanDeleteChild</see> 
        ''' needs to be invoked before starting a transaction.</remarks>
        Friend Sub DeleteOperationOperationalStatusChangeChild()
            DoDelete(_ID)
        End Sub


        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal assetId As Integer, ByVal nBeginOperationalPeriod As Boolean, _
            ByVal parentValidator As IChronologicValidator, _
            ByVal parentIsComplexAct As Boolean)
            MarkAsChild()
            Create(assetId, nBeginOperationalPeriod, parentValidator, parentIsComplexAct)
        End Sub

        Private Sub New(ByVal operationID As Integer, ByVal parentValidator As IChronologicValidator)
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
            Private _BeginOperationalPeriod As Boolean
            Public ReadOnly Property Id() As Integer
                Get
                    Return mId
                End Get
            End Property
            Public ReadOnly Property BeginOperationalPeriod() As Boolean
                Get
                    Return _BeginOperationalPeriod
                End Get
            End Property
            Public Sub New(ByVal id As Integer)
                mId = id
                _BeginOperationalPeriod = True
            End Sub
            Public Sub New(ByVal id As Integer, ByVal nBeginOperationalPeriod As Boolean)
                mId = id
                _BeginOperationalPeriod = nBeginOperationalPeriod
            End Sub
        End Class


        Private Overloads Sub DataPortal_Create(ByVal criteria As Criteria)
            If Not CanAddObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecurityInsertDenied)
            Create(criteria.Id, criteria.BeginOperationalPeriod, Nothing, False)
        End Sub

        Private Sub Create(ByVal nAssetId As Integer, ByVal nBeginOperationalPeriod As Boolean, _
            ByVal parentValidator As IChronologicValidator, _
            ByVal parentIsComplexAct As Boolean)

            _BeginOperationalPeriod = nBeginOperationalPeriod
            _IsComplexAct = parentIsComplexAct

            _Background = OperationBackground.NewOperationBackgroundChild(nAssetId)

            _ChronologyValidator = OperationChronologicValidator2.NewOperationChronologicValidator( _
                _Background, Me.Type, parentValidator)

            ValidationRules.CheckRules()

        End Sub


        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)

            If Not CanGetObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecuritySelectDenied)

            Fetch(criteria.Id, Nothing)

        End Sub

        Private Sub Fetch(ByVal operationID As Integer, ByVal parentValidator As IChronologicValidator)

            Dim persistence As OperationPersistenceObject = _
                OperationPersistenceObject.GetOperationPersistenceObject( _
                operationID, LtaOperationType.AmortizationPeriod)

            Fetch(persistence, parentValidator, Nothing, Nothing)

        End Sub

        Private Sub Fetch(ByVal persistence As OperationPersistenceObject, _
            ByVal parentValidator As IChronologicValidator, ByVal generalData As DataTable, _
            ByVal deltaData As DataTable)

            _ID = persistence.ID
            _Date = persistence.OperationDate
            _IsComplexAct = persistence.IsComplexAct
            _Content = persistence.Content
            _DocumentNumber = persistence.ActNumber
            _BeginOperationalPeriod = (persistence.OperationType = LtaOperationType.UsingStart)
            _InsertDate = persistence.InsertDate
            _UpdateDate = persistence.UpdateDate

            _Background = OperationBackground.GetOperationBackgroundChild( _
                persistence.AssetID, _ID, _Date, generalData, deltaData)

            _Background.DisableCalculations = True
            _Background.InitializeOldData(_Date)
            _Background.DisableCalculations = False

            _ChronologyValidator = OperationChronologicValidator2.GetOperationChronologicValidator( _
                _Background, Me.Type, _ID, _Date, parentValidator)

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
        ''' <param name="financialDataReadOnly">Whether the parent document 
        ''' allows to change the child operation financial data.</param>
        ''' <remarks>Does a save operation on server side. Doesn't check for critical rules 
        ''' (fetch or programatical error within transaction crashes program).
        ''' Critical rules checking method <see cref="CheckIfCanSaveChild">CheckIfCanSaveChild</see> 
        ''' needs to be invoked before starting a transaction.</remarks>
        Friend Sub SaveChild(ByVal parentComplexActID As Integer, _
            ByVal financialDataReadOnly As Boolean)

            If Not IsNew AndAlso Not IsDirty Then Exit Sub

            If IsNew Then
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


        Private Function GetPersistenceObject() As OperationPersistenceObject

            Dim result As OperationPersistenceObject

            If IsNew Then
                If _BeginOperationalPeriod Then
                    result = OperationPersistenceObject.NewOperationPersistenceObject( _
                        LtaOperationType.UsingStart, _Background.AssetID)
                Else
                    result = OperationPersistenceObject.NewOperationPersistenceObject( _
                        LtaOperationType.UsingEnd, _Background.AssetID)
                End If
                result.IsComplexAct = _IsComplexAct
                result.ComplexActID = _ComplexActID
            Else
                result = OperationPersistenceObject.GetOperationPersistenceObject( _
                    _ID, LtaOperationType.UsingStart)
                If result.UpdateDate <> _UpdateDate Then Throw New Exception( _
                    My.Resources.Common_UpdateDateHasChanged)
            End If

            result.OperationDate = _Date
            result.Content = _Content
            result.ActNumber = _DocumentNumber

            Return result

        End Function


        Private Overloads Sub DataPortal_Delete(ByVal criteria As Criteria)

            If Not CanDeleteObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecurityUpdateDenied)

            Dim operationToDelete As New OperationOperationalStatusChange
            operationToDelete.Fetch(criteria.Id, Nothing)

            If operationToDelete.IsComplexAct Then
                Throw New Exception(My.Resources.Assets_OperationOperationalStatusChange_InvalidDeleteComplexDocumentChild)
            End If

            If Not operationToDelete.ChronologyValidator.FinancialDataCanChange Then
                Throw New Exception(String.Format(My.Resources.Assets_OperationOperationalStatusChange_InvalidDelete, _
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
                _Background, Me.Type, _ID, _ChronologyValidator.CurrentOperationDate, parentValidator)

            If Not _ChronologyValidator.FinancialDataCanChange Then
                Throw New Exception(String.Format(My.Resources.Assets_OperationOperationalStatusChange_InvalidDelete, _
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
                    _Background, Me.Type, parentValidator)
            Else
                _Background = OperationBackground.GetOperationBackgroundChild( _
                    _Background.AssetID, _ID, _Date)
                _ChronologyValidator = OperationChronologicValidator2.GetOperationChronologicValidator( _
                    _Background, Me.Type, _ID, _Date, parentValidator)
            End If

            ValidationRules.CheckRules()

        End Sub

#End Region

    End Class

End Namespace