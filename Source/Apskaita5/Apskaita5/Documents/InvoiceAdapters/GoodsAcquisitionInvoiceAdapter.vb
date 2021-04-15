Imports ApskaitaObjects.Goods
Imports Csla.Validation

Namespace Documents.InvoiceAdapters

    ''' <summary>
    ''' Represents an invoice item adapter for <see cref="GoodsOperationAcquisition">
    ''' goods acquisition</see>.
    ''' </summary>
    ''' <remarks>An adapter between a <see cref="GoodsOperationAcquisition">GoodsOperationAcquisition</see> 
    ''' and <see cref="InvoiceMadeItem">InvoiceMadeItem</see>
    ''' or an <see cref="InvoiceReceivedItem">InvoiceReceivedItem</see>.
    ''' Can be added to an invoice by invoking <see cref="InvoiceMade.AttachNewObject">InvoiceMade.AttachNewObject</see>
    ''' or <see cref="InvoiceReceived.AttachNewObject">InvoiceReceived.AttachNewObject</see> methods.</remarks>
    <Serializable()> _
    Public NotInheritable Class GoodsAcquisitionInvoiceAdapter
        Inherits BusinessBase(Of GoodsAcquisitionInvoiceAdapter)
        Implements IInvoiceAdapter, IRegionalDataObject

#Region " Business Methods "

        Private ReadOnly _Guid As Guid = Guid.NewGuid
        Private WithEvents _GoodsAcquisition As GoodsOperationAcquisition = Nothing
        Private _IsForInvoiceMade As Boolean


        ''' <summary>
        ''' Whether the attached operation is created for an <see cref="InvoiceMade">invoice made</see>.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property IsForInvoiceMade() As Boolean Implements IInvoiceAdapter.IsForInvoiceMade
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsForInvoiceMade
            End Get
        End Property

        ''' <summary>
        ''' Gets an ID of the attached operation.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="GoodsOperationAcquisition.ID">GoodsOperationAcquisition.ID</see>.</remarks>
        Public ReadOnly Property Id() As Integer Implements IInvoiceAdapter.Id
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _GoodsAcquisition.ID
            End Get
        End Property

        ''' <summary>
        ''' Gets an ID of the object that the attached operation acts on.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="GoodsOperationAcquisition.GoodsInfo">GoodsOperationAcquisition.GoodsInfo.ID</see>.</remarks>
        Public ReadOnly Property ObjectId() As Integer Implements IInvoiceAdapter.ObjectId
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _GoodsAcquisition.GoodsInfo.ID
            End Get
        End Property

        ''' <summary>
        ''' Gets an ID of the warehouse of the attached operation.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="GoodsOperationAcquisition.Warehouse">GoodsOperationAcquisition.Warehouse.ID</see>.</remarks>
        Public ReadOnly Property WarehouseId() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If _GoodsAcquisition.Warehouse Is Nothing Then Return 0
                Return _GoodsAcquisition.Warehouse.ID
            End Get
        End Property

        ''' <summary>
        ''' Gets a type of the attached object.
        ''' </summary>
        ''' <remarks>Returns <see cref="InvoiceAdapterType.GoodsAcquisition">InvoiceAdapterType.GoodsAcquisition</see>.</remarks>
        Public ReadOnly Property [Type]() As InvoiceAdapterType Implements IInvoiceAdapter.Type
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return InvoiceAdapterType.GoodsAcquisition
            End Get
        End Property

        ''' <summary>
        ''' Gets an underlying attached operation.
        ''' </summary>
        ''' <remarks>Returns an incapsulated <see cref="GoodsOperationAcquisition">GoodsOperationAcquisition</see> instance.</remarks>
        Public ReadOnly Property ValueObject() As Object Implements IInvoiceAdapter.ValueObject
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _GoodsAcquisition
            End Get
        End Property

        ''' <summary>
        ''' Gets a type of the underlying attached operation.
        ''' </summary>
        ''' <remarks>Returns <see cref="GoodsOperationAcquisition">GoodsOperationAcquisition</see>.</remarks>
        Public ReadOnly Property ValueObjectType() As System.Type Implements IInvoiceAdapter.ValueObjectType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return GetType(GoodsOperationAcquisition)
            End Get
        End Property

        ''' <summary>
        ''' Gets an underlying goods operation.
        ''' </summary>
        ''' <remarks>Returns an incapsulated <see cref="GoodsOperationAcquisition">GoodsOperationAcquisition</see> instance.</remarks>
        Public ReadOnly Property GoodsOperation() As GoodsOperationAcquisition
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _GoodsAcquisition
            End Get
        End Property

        ''' <summary>
        ''' Gets an <see cref="IChronologicValidator">IChronologicValidator</see> object that contains business restraints on updating the operation.
        ''' </summary>
        ''' <remarks>Returns <see cref="GoodsOperationAcquisition.OperationLimitations">GoodsOperationAcquisition.OperationLimitations</see>.</remarks>
        Public ReadOnly Property ChronologyValidator() As IChronologicValidator Implements IInvoiceAdapter.ChronologyValidator
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _GoodsAcquisition.OperationLimitations
            End Get
        End Property

        ''' <summary>
        ''' Whether the underlying attached operation has any <see cref="csla.Validation.RuleSeverity.[Error]">errors</see>.
        ''' </summary>
        ''' <remarks>Returns <see cref="GoodsOperationAcquisition.IsValid">GoodsOperationAcquisition.IsValid</see>.</remarks>
        Public ReadOnly Property ValueObjectHasErrors() As Boolean Implements IInvoiceAdapter.ValueObjectHasErrors
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return Not _GoodsAcquisition.IsValid
            End Get
        End Property

        ''' <summary>
        ''' Whether the underlying attached operation has any <see cref="csla.Validation.RuleSeverity.Warning">warnings</see>.
        ''' </summary>
        ''' <remarks>Returns whether <see cref="GoodsOperationAcquisition.BrokenRulesCollection">GoodsOperationAcquisition.BrokenRulesCollection.WarningCount>0</see>.</remarks>
        Public ReadOnly Property ValueObjectHasWarnings() As Boolean Implements IInvoiceAdapter.ValueObjectHasWarnings
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _GoodsAcquisition.BrokenRulesCollection.WarningCount > 0
            End Get
        End Property

        ''' <summary>
        ''' Whether the underlying attached operation is dirty.
        ''' </summary>
        ''' <remarks>A proxy property to the <see cref="Csla.Core.BusinessBase.IsDirty">Csla.Core.BusinessBase.IsDirty</see>.</remarks>
        Public ReadOnly Property ValueObjectIsDirty() As Boolean Implements IInvoiceAdapter.ValueObjectIsDirty
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _GoodsAcquisition.IsDirty
            End Get
        End Property

        ''' <summary>
        ''' Whether the underlying attached operation is new.
        ''' </summary>
        ''' <remarks>A proxy property to the <see cref="Csla.Core.BusinessBase.IsNew">Csla.Core.BusinessBase.IsNew</see>.</remarks>
        Public ReadOnly Property ValueObjectIsNew() As Boolean Implements IInvoiceAdapter.ValueObjectIsNew
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _GoodsAcquisition.IsNew
            End Get
        End Property

        ''' <summary>
        ''' Whether the underlying attached operation implements 
        ''' <see cref="GetCopy">GetCopy</see> method, i.e. can be copied to a new invoice.
        ''' </summary>
        ''' <remarks>Returns FALSE for a goods acquisition adapter.</remarks>
        Public ReadOnly Property ImplementsCopy() As Boolean Implements IInvoiceAdapter.ImplementsCopy
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return False
            End Get
        End Property



        ''' <summary>
        ''' Whether the <see cref="InvoiceMadeItem.NameInvoice">InvoiceMadeItem.NameInvoice</see>
        ''' or the <see cref="InvoiceReceivedItem.NameInvoice">InvoiceReceivedItem.NameInvoice</see>
        ''' properties should set by the corresponding attached operation property and vice versa 
        ''' (only to replace empty value, not to enforce equality).
        ''' </summary>
        ''' <remarks>Not to be used for regionalization.
        ''' Returns FALSE for goods acquisition.</remarks>
        Public ReadOnly Property HandlesNameInvoice() As Boolean Implements IInvoiceAdapter.HandlesNameInvoice
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return False
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets a value of the attached operation property that corresponds to the 
        ''' <see cref="InvoiceMadeItem.NameInvoice">InvoiceMadeItem.NameInvoice</see>
        ''' or the <see cref="InvoiceReceivedItem.NameInvoice">InvoiceReceivedItem.NameInvoice</see>
        ''' properties if the <see cref="HandlesNameInvoice">HandlesNameInvoice</see> is set to TRUE.
        ''' </summary>
        ''' <remarks>Not to be used for regionalization.
        ''' Returns an empty string, sets nothing.</remarks>
        Public Property NameInvoice() As String Implements IInvoiceAdapter.NameInvoice
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return ""
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As String)

            End Set
        End Property

        ''' <summary>
        ''' Whether the <see cref="InvoiceMadeItem.NameInvoice">InvoiceMadeItem.NameInvoice</see>
        ''' or the <see cref="InvoiceReceivedItem.NameInvoice">InvoiceReceivedItem.NameInvoice</see>
        ''' properties should be initialized by the corresponding attached operation property.
        ''' </summary>
        ''' <remarks>Not to be used for regionalization.
        ''' Returns TRUE for a goods acquisition.</remarks>
        Public ReadOnly Property ProvidesDefaultNameInvoice() As Boolean Implements IInvoiceAdapter.ProvidesDefaultNameInvoice
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return True
            End Get
        End Property

        ''' <summary>
        ''' Gets a value of the attached operation property that provides a default (initial) value for the 
        ''' <see cref="InvoiceMadeItem.NameInvoice">InvoiceMadeItem.NameInvoice</see>
        ''' or the <see cref="InvoiceReceivedItem.NameInvoice">InvoiceReceivedItem.NameInvoice</see>
        ''' properties if the <see cref="ProvidesDefaultNameInvoice">ProvidesDefaultNameInvoice</see> is set to TRUE.
        ''' </summary>
        ''' <remarks>Not to be used for regionalization.
        ''' Returns <see cref="GoodsOperationAcquisition.GoodsInfo">GoodsOperationAcquisition.GoodsInfo.Name</see>.</remarks>
        Public ReadOnly Property DefaultNameInvoice() As String Implements IInvoiceAdapter.DefaultNameInvoice
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _GoodsAcquisition.GoodsInfo.Name
            End Get
        End Property

        ''' <summary>
        ''' Whether the <see cref="InvoiceMadeItem.MeasureUnit">InvoiceMadeItem.MeasureUnit</see>
        ''' or the <see cref="InvoiceReceivedItem.MeasureUnit">InvoiceReceivedItem.MeasureUnit</see>
        ''' properties should set by the corresponding attached operation property and vice versa 
        ''' (only to replace empty value, not to enforce equality).
        ''' </summary>
        ''' <remarks>Not to be used for regionalization.
        ''' Returns FALSE for a good acquisition.</remarks>
        Public ReadOnly Property HandlesMeasureUnit() As Boolean Implements IInvoiceAdapter.HandlesMeasureUnit
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return False
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets a value of the attached operation property that corresponds to the 
        ''' <see cref="InvoiceMadeItem.MeasureUnit">InvoiceMadeItem.MeasureUnit</see>
        ''' or the <see cref="InvoiceReceivedItem.MeasureUnit">InvoiceReceivedItem.MeasureUnit</see>
        ''' properties if the <see cref="HandlesMeasureUnit">HandlesMeasureUnit</see> is set to TRUE.
        ''' </summary>
        ''' <remarks>Not to be used for regionalization.
        ''' Returns an empty string, sets nothing.</remarks>
        Public Property MeasureUnit() As String Implements IInvoiceAdapter.MeasureUnit
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return ""
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As String)

            End Set
        End Property

        ''' <summary>
        ''' Whether the <see cref="InvoiceMadeItem.MeasureUnit">InvoiceMadeItem.MeasureUnit</see>
        ''' or the <see cref="InvoiceReceivedItem.MeasureUnit">InvoiceReceivedItem.MeasureUnit</see>
        ''' properties should be initialized by the corresponding attached operation property.
        ''' </summary>
        ''' <remarks>Not to be used for regionalization.
        ''' Returns TRUE for a goods acquisition.</remarks>
        Public ReadOnly Property ProvidesDefaultMeasureUnit() As Boolean Implements IInvoiceAdapter.ProvidesDefaultMeasureUnit
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return True
            End Get
        End Property

        ''' <summary>
        ''' Gets a value of the attached operation property that provides a default (initial) value for the 
        ''' <see cref="InvoiceMadeItem.MeasureUnit">InvoiceMadeItem.MeasureUnit</see>
        ''' or the <see cref="InvoiceReceivedItem.MeasureUnit">InvoiceReceivedItem.MeasureUnit</see>
        ''' properties if the <see cref="ProvidesDefaultMeasureUnit">ProvidesDefaultMeasureUnit</see> is set to TRUE.
        ''' </summary>
        ''' <remarks>Not to be used for regionalization.
        ''' Returns <see cref="GoodsOperationAcquisition.GoodsInfo">GoodsOperationAcquisition.GoodsInfo.MeasureUnit</see>.</remarks>
        Public ReadOnly Property DefaultMeasureUnit() As String Implements IInvoiceAdapter.DefaultMeasureUnit
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _GoodsAcquisition.GoodsInfo.MeasureUnit
            End Get
        End Property

        ''' <summary>
        ''' Whether the <see cref="InvoiceMadeItem.AccountIncome">InvoiceMadeItem.AccountIncome</see>
        ''' or the <see cref="InvoiceReceivedItem.AccountCosts">InvoiceReceivedItem.AccountCosts</see>
        ''' properties should set by the corresponding attached operation property and vice versa 
        ''' (to enforce equality).
        ''' </summary>
        ''' <remarks>Returns TRUE for a good acquisition.</remarks>
        Public ReadOnly Property HandlesAccount() As Boolean Implements IInvoiceAdapter.HandlesAccount
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return True
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets a value of the attached operation property that corresponds to the 
        ''' <see cref="InvoiceMadeItem.AccountIncome">InvoiceMadeItem.AccountIncome</see>
        ''' or the <see cref="InvoiceReceivedItem.AccountCosts">InvoiceReceivedItem.AccountCosts</see>
        ''' properties if the <see cref="HandlesAccount">HandlesAccount</see> is set to TRUE.
        ''' </summary>
        ''' <remarks>Returns <see cref="GoodsOperationAcquisition.AcquisitionAccount">GoodsOperationAcquisition.AcquisitionAccount</see>.
        ''' Sets nothing because AcquisitionAccount can only be changed by changing warehouse.
        ''' </remarks>
        Public Property Account() As Long Implements IInvoiceAdapter.Account
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _GoodsAcquisition.AcquisitionAccount
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Long)

            End Set
        End Property

        ''' <summary>
        ''' Whether the <see cref="InvoiceMadeItem.AccountIncome">InvoiceMadeItem.AccountIncome</see>
        ''' or the <see cref="InvoiceReceivedItem.AccountCosts">InvoiceReceivedItem.AccountCosts</see>
        ''' properties should be initialized by the corresponding attached operation property.
        ''' </summary>
        ''' <remarks>Returns TRUE for a goods acquisition.</remarks>
        Public ReadOnly Property ProvidesDefaultAccount() As Boolean Implements IInvoiceAdapter.ProvidesDefaultAccount
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return True
            End Get
        End Property

        ''' <summary>
        ''' Gets a value of the attached operation property that provides a default (initial) value for the 
        ''' <see cref="InvoiceReceivedItem.AccountCosts">InvoiceReceivedItem.AccountCosts</see>
        ''' or the <see cref="InvoiceMadeItem.AccountIncome">InvoiceMadeItem.AccountIncome</see>
        ''' properties if the <see cref="ProvidesDefaultAccount">ProvidesDefaultAccount</see> is set to TRUE.
        ''' </summary>
        ''' <remarks>Returns <see cref="GoodsOperationAcquisition.AcquisitionAccount">GoodsOperationAcquisition.AcquisitionAccount</see>.</remarks>
        Public ReadOnly Property DefaultAccount() As Long Implements IInvoiceAdapter.DefaultAccount
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _GoodsAcquisition.AcquisitionAccount
            End Get
        End Property

        ''' <summary>
        ''' Whether the <see cref="InvoiceMadeItem.Ammount">InvoiceMadeItem.Ammount</see>
        ''' or the <see cref="InvoiceReceivedItem.Ammount">InvoiceReceivedItem.Ammount</see>
        ''' properties should set by the corresponding attached operation property and vice versa 
        ''' (to enforce equality).
        ''' </summary>
        ''' <remarks>Returns TRUE for a goods acquisition.</remarks>
        Public ReadOnly Property HandlesAmount() As Boolean Implements IInvoiceAdapter.HandlesAmount
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return True
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets a value of the attached operation property that corresponds to the 
        ''' <see cref="InvoiceMadeItem.Ammount">InvoiceMadeItem.Ammount</see>
        ''' or the <see cref="InvoiceReceivedItem.Ammount">InvoiceReceivedItem.Ammount</see>
        ''' properties if the <see cref="HandlesAmount">HandlesAmount</see> is set to TRUE.
        ''' </summary>
        ''' <remarks>Returns <see cref="GoodsOperationAcquisition.Ammount">GoodsOperationAcquisition.Ammount</see>.</remarks>
        Public ReadOnly Property Amount() As Double Implements IInvoiceAdapter.Amount
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If _IsForInvoiceMade Then
                    Return CRound(-_GoodsAcquisition.Ammount, ROUNDAMOUNTINVOICEMADE)
                Else
                    Return CRound(_GoodsAcquisition.Ammount, ROUNDAMOUNTINVOICERECEIVED)
                End If
            End Get
        End Property

        ''' <summary>
        ''' Whether the <see cref="InvoiceMadeItem.SumLTL">InvoiceMadeItem.SumLTL</see>
        ''' or the <see cref="InvoiceReceivedItem.SumLTL">InvoiceReceivedItem.SumLTL</see>
        ''' properties should set by the corresponding attached operation property and vice versa 
        ''' (to enforce equality).
        ''' </summary>
        ''' <remarks>Returns TRUE for a goods acquisition.</remarks>
        Public ReadOnly Property HandlesSum() As Boolean Implements IInvoiceAdapter.HandlesSum
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return True
            End Get
        End Property

        ''' <summary>
        ''' Gets a value of the attached operation property that corresponds to the 
        ''' <see cref="InvoiceMadeItem.SumLTL">InvoiceMadeItem.SumLTL</see>
        ''' or the <see cref="InvoiceReceivedItem.SumLTL">InvoiceReceivedItem.SumLTL</see>
        ''' properties if the <see cref="HandlesSum">HandlesSum</see> is set to TRUE.
        ''' </summary>
        ''' <remarks>Returns <see cref="GoodsOperationAcquisition.TotalCost">>GoodsOperationAcquisition.TotalCost</see>.</remarks>
        Public ReadOnly Property Sum() As Double Implements IInvoiceAdapter.Sum
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If _IsForInvoiceMade Then
                    Return -_GoodsAcquisition.TotalCost
                Else
                    Return _GoodsAcquisition.TotalCost
                End If
            End Get
        End Property

        ''' <summary>
        ''' Whether the <see cref="InvoiceMadeItem.VatRate">InvoiceMadeItem.VatRate</see>
        ''' or the <see cref="InvoiceReceivedItem.VatRate">InvoiceReceivedItem.VatRate</see>
        ''' properties should set by the corresponding attached operation property and vice versa 
        ''' (to enforce equality).
        ''' </summary>
        ''' <remarks>Returns FALSE for a goods acquisition.</remarks>
        Public ReadOnly Property HandlesVatRate() As Boolean Implements IInvoiceAdapter.HandlesVatRate
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return False
            End Get
        End Property

        ''' <summary>
        ''' Gets a value of the attached operation property that corresponds to the 
        ''' <see cref="InvoiceMadeItem.VatRate">InvoiceMadeItem.VatRate</see>
        ''' or the <see cref="InvoiceReceivedItem.VatRate">InvoiceReceivedItem.VatRate</see>
        ''' properties if the <see cref="HandlesSum">HandlesVatRate</see> is set to TRUE.
        ''' </summary>
        ''' <remarks>Returns 0 for a goods acquisition.</remarks>
        Public ReadOnly Property VatRate() As Double Implements IInvoiceAdapter.VatRate
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return 0
            End Get
        End Property

        ''' <summary>
        ''' Whether the <see cref="InvoiceMadeItem.VatRate">InvoiceMadeItem.VatRate</see>
        ''' or the <see cref="InvoiceReceivedItem.VatRate">InvoiceReceivedItem.VatRate</see>
        ''' properties should be initialized by the corresponding attached operation property.
        ''' </summary>
        ''' <remarks>Returns TRUE for a goods acquisition.</remarks>
        Public ReadOnly Property ProvidesDefaultVatRate() As Boolean Implements IInvoiceAdapter.ProvidesDefaultVatRate
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return True
            End Get
        End Property

        ''' <summary>
        ''' Gets a value of the attached operation property that provides a default (initial) value for the 
        ''' <see cref="InvoiceMadeItem.VatRate">InvoiceMadeItem.VatRate</see>
        ''' or the <see cref="InvoiceReceivedItem.VatRate">InvoiceReceivedItem.VatRate</see>
        ''' properties if the <see cref="ProvidesDefaultVatRate">ProvidesDefaultVatRate</see> is set to TRUE.
        ''' </summary>
        ''' <remarks>Returns <see cref="GoodsOperationAcquisition.GoodsInfo">GoodsOperationAcquisition.GoodsInfo.DefaultVatRatePurchase</see>.</remarks>
        Public ReadOnly Property DefaultVatRate() As Double Implements IInvoiceAdapter.DefaultVatRate
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If _IsForInvoiceMade Then
                    Return _GoodsAcquisition.GoodsInfo.DefaultVatRateSales
                Else
                    Return _GoodsAcquisition.GoodsInfo.DefaultVatRatePurchase
                End If
            End Get
        End Property

        ''' <summary>
        ''' Gets a value of the attached operation property that provides a default (initial) value for the 
        ''' <see cref="InvoiceMadeItem.DeclarationSchema">InvoiceMadeItem.DeclarationSchema</see>
        ''' or the <see cref="InvoiceReceivedItem.DeclarationSchema">InvoiceReceivedItem.DeclarationSchema</see>
        ''' properties if the <see cref="ProvidesDefaultVatRate">ProvidesDefaultVatRate</see> is set to TRUE.
        ''' </summary>
        ''' <remarks>Returns <see cref="GoodsOperationAcquisition.GoodsInfo">GoodsOperationAcquisition.GoodsInfo.DeclarationSchemaSales
        ''' or GoodsOperationAcquisition.GoodsInfo.DeclarationSchemaPurchase</see>
        ''' subject to the invoice type.</remarks>
        Public ReadOnly Property DefaultDeclarationSchema() As VatDeclarationSchemaInfo _
            Implements IInvoiceAdapter.DefaultDeclarationSchema
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If _IsForInvoiceMade Then
                    Return _GoodsAcquisition.GoodsInfo.DeclarationSchemaSales
                Else
                    Return _GoodsAcquisition.GoodsInfo.DeclarationSchemaPurchase
                End If
            End Get
        End Property

        ''' <summary>
        ''' Whether the <see cref="InvoiceMadeItem.AccountVat">InvoiceMadeItem.AccountVat</see>
        ''' or the <see cref="InvoiceReceivedItem.AccountVat">InvoiceReceivedItem.AccountVat</see>
        ''' properties should be initialized by the corresponding attached operation property.
        ''' </summary>
        ''' <remarks>Returns FALSE for a goods acquisition.</remarks>
        Public ReadOnly Property ProvidesDefaultVatAccount() As Boolean Implements IInvoiceAdapter.ProvidesDefaultVatAccount
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return False
            End Get
        End Property

        ''' <summary>
        ''' Gets a value of the attached operation property that provides a default (initial) value for the 
        ''' <see cref="InvoiceReceivedItem.AccountVat">InvoiceReceivedItem.AccountVat</see>
        ''' or the <see cref="InvoiceMadeItem.AccountVat">InvoiceMadeItem.AccountVat</see>
        ''' properties if the <see cref="ProvidesDefaultVatRate">ProvidesDefaultAccountVat</see> is set to TRUE.
        ''' </summary>
        ''' <remarks>Returns 0 for a goods acquisition.</remarks>
        Public ReadOnly Property DefaultVatAccount() As Long Implements IInvoiceAdapter.DefaultVatAccount
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return 0
            End Get
        End Property


        ''' <summary>
        ''' Whether the operation is compartible with a discount within an invoice item,
        ''' i.e. <see cref="InvoiceMadeItem.Discount">InvoiceMadeItem.Discount</see>
        ''' and other discount related properties can take non zero values.
        ''' </summary>
        ''' <remarks>Returns FALSE for a goods acquisition.
        ''' One can only acquire goods by a credit invoice made (item). 
        ''' Discount on credit item doesn't make sense.</remarks>
        Public ReadOnly Property DiscountIsAllowed() As Boolean Implements IInvoiceAdapter.DiscountIsAllowed
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return False
            End Get
        End Property

        ''' <summary>
        ''' Whether the <see cref="InvoiceMadeItem.SumVatLTL">InvoiceMadeItem.SumVatLTL</see>
        ''' or the <see cref="InvoiceReceivedItem.SumVatLTL">InvoiceReceivedItem.SumVatLTL</see>
        ''' can be handled by the attached operation for the purpose of generating book entries. 
        ''' E.g. included into the aquisition costs of an asset.
        ''' </summary>
        ''' <remarks>Returns TRUE for a goods acquisition.</remarks>
        Public ReadOnly Property SumVatIsHandledOnRequest() As Boolean Implements IInvoiceAdapter.SumVatIsHandledOnRequest
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return True
            End Get
        End Property


        ''' <summary>
        ''' Gets an ID of the regionalizable object.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="GoodsOperationAcquisition.GoodsInfo">GoodsOperationAcquisition.GoodsInfo.ID</see>.</remarks>
        Public ReadOnly Property RegionalObjectID() As Integer Implements IRegionalDataObject.RegionalObjectID
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _GoodsAcquisition.GoodsInfo.ID
            End Get
        End Property

        ''' <summary>
        ''' Gets a type of the regionalizable object.
        ''' </summary>
        ''' <remarks>Returnes <see cref="RegionalizedObjectType.Goods">RegionalizedObjectType.Goods</see>.</remarks>
        Public ReadOnly Property RegionalObjectType() As RegionalizedObjectType Implements IRegionalDataObject.RegionalObjectType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return RegionalizedObjectType.Goods
            End Get
        End Property



        ''' <summary>
        ''' Gets all the <see cref="csla.Validation.RuleSeverity.[Error]">errors</see> within the attached operation.
        ''' </summary>
        ''' <remarks>Returns <see cref="GoodsOperationAcquisition.BrokenRulesCollection">LongTermAssetOperation.BrokenRulesCollection.ToString</see>.</remarks>
        Public Function GetAllErrors() As String Implements IInvoiceAdapter.GetAllErrors
            Return _GoodsAcquisition.BrokenRulesCollection.ToString(RuleSeverity.Error)
        End Function

        ''' <summary>
        ''' Gets all the <see cref="csla.Validation.RuleSeverity.Warning">warnings</see> within the attached operation.
        ''' </summary>
        ''' <remarks>Returns <see cref="GoodsOperationAcquisition.BrokenRulesCollection">LongTermAssetOperation.BrokenRulesCollection.ToString</see>.</remarks>
        Public Function GetAllWarnings() As String Implements IInvoiceAdapter.GetAllWarnings
            Return _GoodsAcquisition.BrokenRulesCollection.ToString(RuleSeverity.Warning)
        End Function


        ''' <summary>
        ''' Sets the attached operation date to invoice date.
        ''' </summary>
        ''' <param name="newInvoiceDate"></param>
        ''' <remarks>Invokes <see cref="GoodsOperationAcquisition.SetParentDate">GoodsOperationAcquisition.SetParentDate</see>
        ''' method on encapsulated goods acquisition operation object.</remarks>
        Public Sub SetInvoiceDate(ByVal newInvoiceDate As Date) Implements IInvoiceAdapter.SetInvoiceDate
            _GoodsAcquisition.SetParentDate(newInvoiceDate)
        End Sub

        ''' <summary>
        ''' Sets the attached operation financial data (unit value, amount and total sum) 
        ''' with the values of InvoiceMadeItem.
        ''' </summary>
        ''' <param name="parentInvoiceItem"></param>
        ''' <remarks></remarks>
        Public Sub SetInvoiceFinancialData(ByVal parentInvoiceItem As InvoiceMadeItem) Implements IInvoiceAdapter.SetInvoiceFinancialData

            If Not _GoodsAcquisition.OperationLimitations.FinancialDataCanChange Then Exit Sub

            ' can only acquire goods by a credit invoice (item)
            If parentInvoiceItem.Ammount > 0 Then
                _GoodsAcquisition.Ammount = 0
            Else
                _GoodsAcquisition.Ammount = -parentInvoiceItem.Ammount
            End If

            Dim totalValue As Double = -parentInvoiceItem.GetTotalSumForInvoiceAdapter
            If totalValue < 0 Then
                totalValue = 0
            End If

            If _GoodsAcquisition.Ammount > 0 Then

                _GoodsAcquisition.UnitCost = totalValue / _GoodsAcquisition.Ammount

            Else

                _GoodsAcquisition.UnitCost = 0

            End If

            _GoodsAcquisition.TotalCost = totalValue

        End Sub

        ''' <summary>
        ''' Sets the attached operation financial data (unit value, amount and total sum) 
        ''' with the values of InvoiceReceivedItem.
        ''' </summary>
        ''' <param name="parentInvoiceItem"></param>
        ''' <remarks></remarks>
        Public Sub SetInvoiceFinancialData(ByVal parentInvoiceItem As InvoiceReceivedItem) Implements IInvoiceAdapter.SetInvoiceFinancialData

            If Not _GoodsAcquisition.OperationLimitations.FinancialDataCanChange Then Exit Sub

            ' can only acquire goods by a debit invoice (item)
            If parentInvoiceItem.Ammount < 0 Then
                _GoodsAcquisition.Ammount = 0
            Else
                _GoodsAcquisition.Ammount = parentInvoiceItem.Ammount
            End If

            Dim totalValue As Double = parentInvoiceItem.GetTotalSumForInvoiceAdapter
            If totalValue < 0 Then
                totalValue = 0
            End If

            If _GoodsAcquisition.Ammount > 0 Then

                _GoodsAcquisition.UnitCost = totalValue / _GoodsAcquisition.Ammount

            Else

                _GoodsAcquisition.UnitCost = 0

            End If

            _GoodsAcquisition.TotalCost = totalValue

        End Sub

        ''' <summary>
        ''' Gets a copy of the adapter. 
        ''' Not implemented for the current type of invoice adapter, throws a NotImplementedException.
        ''' </summary>
        ''' <remarks>Used to support invoice copy functionality.
        ''' Should always check <see cref="ImplementsCopy">ImplementsCopy</see>
        ''' property before invoking this method.</remarks>
        Friend Function GetCopy() As IInvoiceAdapter Implements IInvoiceAdapter.GetCopy
            Throw New NotImplementedException(String.Format( _
                My.Resources.Documents_InvoiceAdapters_IInvoiceAdapter_GetCopyIsNotImplemented, _
                Me.ToString()))
        End Function


        Private Sub Child_Changed(ByVal sender As Object, _
            ByVal e As System.ComponentModel.PropertyChangedEventArgs) Handles _GoodsAcquisition.PropertyChanged
            PropertyHasChanged()
        End Sub

        ''' <summary>
        ''' Helper method. Takes care of child objects loosing their handlers.
        ''' </summary>
        Protected Overrides Function GetClone() As Object
            Dim result As GoodsAcquisitionInvoiceAdapter = DirectCast(MyBase.GetClone(), GoodsAcquisitionInvoiceAdapter)
            result.RestoreChildListsHandles()
            Return result
        End Function

        Protected Overrides Sub OnDeserialized(ByVal context As System.Runtime.Serialization.StreamingContext)
            MyBase.OnDeserialized(context)
            RestoreChildListsHandles()
        End Sub

        Protected Overrides Sub UndoChangesComplete()
            MyBase.UndoChangesComplete()
            RestoreChildListsHandles()
        End Sub

        ''' <summary>
        ''' Helper method. Takes care of ReportItems loosing its handler. See GetClone method.
        ''' </summary>
        Friend Sub RestoreChildListsHandles()
            Try
                RemoveHandler _GoodsAcquisition.PropertyChanged, AddressOf Child_Changed
            Catch ex As Exception
            End Try
            AddHandler _GoodsAcquisition.PropertyChanged, AddressOf Child_Changed
        End Sub


        ''' <summary>
        ''' Checks if some other adapter is compatible with the current one.
        ''' </summary>
        ''' <param name="adapter">an IInvoiceAdapter to check for compatibility</param>
        ''' <param name="explanation">out parameter, returns explanation of incompatibility</param>
        ''' <remarks></remarks>
        Friend Function IsCompatibleWithAdapter(ByVal adapter As IInvoiceAdapter, _
            ByRef explanation As String) As Boolean Implements IInvoiceAdapter.IsCompatibleWithAdapter
            explanation = ""
            Return True
        End Function

        Public Function GetGoodsInfo() As GoodsInfo Implements IInvoiceAdapter.GetGoodsInfo
            Return GoodsInfoList.GetList.GetItem(_GoodsAcquisition.GoodsInfo.ID)
        End Function


        Protected Overrides Function GetIdValue() As Object
            Return _Guid
        End Function

        Public Overrides Function ToString() As String
            Return String.Format(My.Resources.Documents_InvoiceAdapters_GoodsAcquisitionInvoiceAdapter_ToString, _
                _GoodsAcquisition.GoodsInfo.Name)
        End Function

#End Region

#Region " Validation Rules "

        ''' <summary>
        ''' Validates a value of the <see cref="InvoiceMadeItem.AccountIncome">InvoiceMadeItem.AccountIncome</see>
        ''' property. Returnes false if the sum is invalid or a warning is generated.
        ''' </summary>
        ''' <param name="invoiceItem"></param>
        ''' <param name="e"></param>
        ''' <remarks>Only used if <see cref="HandlesAccount">HandlesAccount</see> is set to TRUE.</remarks>
        Public Function ValidateAccount(ByVal invoiceItem As InvoiceMadeItem, ByVal e As Csla.Validation.RuleArgs) As Boolean Implements IInvoiceAdapter.ValidateAccount

            If Not invoiceItem.AccountIncome > 0 Then

                e.Description = My.Resources.Documents_InvoiceAdapters_GoodsAcquisitionInvoiceAdapter_AccountIncomeNull
                e.Severity = RuleSeverity.Error
                Return False

            ElseIf invoiceItem.AccountIncome <> _GoodsAcquisition.AcquisitionAccount Then

                e.Description = String.Format(My.Resources.Documents_InvoiceAdapters_GoodsAcquisitionInvoiceAdapter_AccountIncomeInvalid, _
                    invoiceItem.AccountIncome.ToString(), _GoodsAcquisition.AcquisitionAccount.ToString())
                e.Severity = RuleSeverity.Error
                Return False

            End If

            Return True

        End Function

        ''' <summary>
        ''' Validates a value of the <see cref="InvoiceReceivedItem.AccountCosts">InvoiceReceivedItem.AccountCosts</see>
        ''' property. Returnes false if the sum is invalid or a warning is generated.
        ''' </summary>
        ''' <param name="invoiceItem"></param>
        ''' <param name="e"></param>
        ''' <remarks>Only used if <see cref="HandlesAccount">HandlesAccount</see> is set to TRUE.
        ''' Overrides validation of a invoice item. Full validation needs to be performed.</remarks>
        Public Function ValidateAccount(ByVal invoiceItem As InvoiceReceivedItem, ByVal e As Csla.Validation.RuleArgs) As Boolean Implements IInvoiceAdapter.ValidateAccount

            If Not invoiceItem.AccountCosts > 0 Then

                e.Description = My.Resources.Documents_InvoiceAdapters_GoodsAcquisitionInvoiceAdapter_AccountCostsNull
                e.Severity = RuleSeverity.Error
                Return False

            ElseIf invoiceItem.AccountCosts <> _GoodsAcquisition.AcquisitionAccount Then

                e.Description = String.Format(My.Resources.Documents_InvoiceAdapters_GoodsAcquisitionInvoiceAdapter_AccountCostsInvalid, _
                    invoiceItem.AccountCosts.ToString(), _GoodsAcquisition.AcquisitionAccount.ToString())
                e.Severity = RuleSeverity.Error
                Return False

            End If

            Return True

        End Function

        ''' <summary>
        ''' Validates a value of the <see cref="InvoiceMadeItem.Ammount">InvoiceMadeItem.Ammount</see>
        ''' property. Returnes false if the amount is invalid or a warning is generated.
        ''' </summary>
        ''' <param name="invoiceItem"></param>
        ''' <param name="e"></param>
        ''' <remarks>Complements validation performed by an invoice item.</remarks>
        Public Function ValidateAmount(ByVal invoiceItem As InvoiceMadeItem, ByVal e As Csla.Validation.RuleArgs) As Boolean Implements IInvoiceAdapter.ValidateAmount

            Dim commonRoundOrder As Integer = Math.Min(ROUNDAMOUNTGOODS, ROUNDAMOUNTINVOICEMADE)

            If invoiceItem.Ammount > 0 Then

                e.Description = My.Resources.Documents_InvoiceAdapters_GoodsAcquisitionInvoiceAdapter_AmountInvalidMade
                e.Severity = RuleSeverity.Error
                Return False

            ElseIf invoiceItem.Ammount < 0 AndAlso CRound(-invoiceItem.Ammount, commonRoundOrder) _
                <> CRound(_GoodsAcquisition.Ammount, commonRoundOrder) Then

                e.Description = String.Format(My.Resources.Documents_InvoiceAdapters_GoodsAcquisitionInvoiceAdapter_AmountMismatch, _
                    DblParser(-invoiceItem.Ammount, commonRoundOrder), _
                    DblParser(_GoodsAcquisition.Ammount, commonRoundOrder))
                e.Severity = RuleSeverity.Error
                Return False

            End If

            Return True

        End Function

        ''' <summary>
        ''' Validates a value of the <see cref="InvoiceReceivedItem.Ammount">InvoiceReceivedItem.Ammount</see>
        ''' properties. Returnes false if the amount is invalid or a warning is generated.
        ''' </summary>
        ''' <param name="invoiceItem"></param>
        ''' <param name="e"></param>
        ''' <remarks>Complements validation performed by an invoice item.</remarks>
        Public Function ValidateAmount(ByVal invoiceItem As InvoiceReceivedItem, ByVal e As Csla.Validation.RuleArgs) As Boolean Implements IInvoiceAdapter.ValidateAmount

            Dim commonRoundOrder As Integer = Math.Min(ROUNDAMOUNTGOODS, ROUNDAMOUNTINVOICERECEIVED)

            If invoiceItem.Ammount < 0 Then

                e.Description = My.Resources.Documents_InvoiceAdapters_GoodsAcquisitionInvoiceAdapter_AmountInvalidReceived
                e.Severity = RuleSeverity.Error
                Return False

            ElseIf invoiceItem.Ammount > 0 AndAlso CRound(invoiceItem.Ammount, commonRoundOrder) _
                <> CRound(_GoodsAcquisition.Ammount, commonRoundOrder) Then

                e.Description = String.Format(My.Resources.Documents_InvoiceAdapters_GoodsAcquisitionInvoiceAdapter_AmountMismatch, _
                    DblParser(invoiceItem.Ammount, commonRoundOrder), _
                    DblParser(_GoodsAcquisition.Ammount, commonRoundOrder))
                e.Severity = RuleSeverity.Error
                Return False

            End If

            Return True

        End Function

        ''' <summary>
        ''' Validates a value of the <see cref="InvoiceMadeItem.SumLTL">InvoiceMadeItem.SumLTL</see>
        ''' or the <see cref="InvoiceReceivedItem.SumLTL">InvoiceReceivedItem.SumLTL</see>
        ''' property. Returnes false if the sum is invalid or a warning is generated.
        ''' </summary>
        ''' <param name="invoiceItem"></param>
        ''' <param name="e"></param>
        ''' <remarks>Complements validation performed by an invoice item.</remarks>
        Public Function ValidateSum(ByVal invoiceItem As InvoiceMadeItem, ByVal e As Csla.Validation.RuleArgs) As Boolean Implements IInvoiceAdapter.ValidateSum

            If invoiceItem.SumLTL > 0 Then

                e.Description = My.Resources.Documents_InvoiceAdapters_GoodsAcquisitionInvoiceAdapter_SumInvalidMade
                e.Severity = RuleSeverity.Error
                Return False

            ElseIf invoiceItem.SumLTL < 0 AndAlso Not invoiceItem.IncludeVatInObject _
                AndAlso -invoiceItem.GetTotalSumForInvoiceAdapter <> _GoodsAcquisition.TotalCost Then

                e.Description = String.Format(My.Resources.Documents_InvoiceAdapters_GoodsAcquisitionInvoiceAdapter_SumMismatch, _
                    DblParser(-invoiceItem.GetTotalSumForInvoiceAdapter, 2), DblParser(_GoodsAcquisition.TotalCost, 2))
                e.Severity = RuleSeverity.Error
                Return False

            End If

            Return True

        End Function

        ''' <summary>
        ''' Validates a value of the <see cref="InvoiceReceivedItem.SumLTL">InvoiceReceivedItem.SumLTL</see>
        ''' property. Returnes false if the sum is invalid or a warning is generated.
        ''' </summary>
        ''' <param name="invoiceItem"></param>
        ''' <param name="e"></param>
        ''' <remarks>Complements validation performed by an invoice item.</remarks>
        Public Function ValidateSum(ByVal invoiceItem As InvoiceReceivedItem, ByVal e As Csla.Validation.RuleArgs) As Boolean Implements IInvoiceAdapter.ValidateSum

            If invoiceItem.SumLTL < 0 Then

                e.Description = My.Resources.Documents_InvoiceAdapters_GoodsAcquisitionInvoiceAdapter_SumInvalidReceived
                e.Severity = RuleSeverity.Error
                Return False

            ElseIf invoiceItem.SumLTL > 0 AndAlso Not invoiceItem.IncludeVatInObject AndAlso _
                invoiceItem.GetTotalSumForInvoiceAdapter <> _GoodsAcquisition.TotalCost Then

                e.Description = String.Format(My.Resources.Documents_InvoiceAdapters_GoodsAcquisitionInvoiceAdapter_SumMismatch, _
                    DblParser(invoiceItem.GetTotalSumForInvoiceAdapter, 2), DblParser(_GoodsAcquisition.TotalCost, 2))
                e.Severity = RuleSeverity.Error
                Return False

            End If

            Return True

        End Function

        ''' <summary>
        ''' Validates a value of the <see cref="InvoiceMadeItem.SumTotalLTL">InvoiceMadeItem.SumTotalLTL</see>
        ''' property. Returnes false if the sum is invalid or a warning is generated.
        ''' </summary>
        ''' <param name="invoiceItem"></param>
        ''' <param name="e"></param>
        ''' <remarks>Complements validation performed by an invoice item.</remarks>
        Public Function ValidateTotalSum(ByVal invoiceItem As InvoiceMadeItem, ByVal e As Csla.Validation.RuleArgs) As Boolean Implements IInvoiceAdapter.ValidateTotalSum

            If invoiceItem.SumTotalLTL > 0 Then

                e.Description = My.Resources.Documents_InvoiceAdapters_GoodsAcquisitionInvoiceAdapter_SumTotalInvalidMade
                e.Severity = RuleSeverity.Error
                Return False

            ElseIf invoiceItem.SumTotalLTL < 0 AndAlso invoiceItem.IncludeVatInObject _
                AndAlso -invoiceItem.GetTotalSumForInvoiceAdapter <> _GoodsAcquisition.TotalCost Then

                e.Description = String.Format(My.Resources.Documents_InvoiceAdapters_GoodsAcquisitionInvoiceAdapter_SumTotalMismatch, _
                    DblParser(-invoiceItem.GetTotalSumForInvoiceAdapter, 2), DblParser(_GoodsAcquisition.TotalCost, 2))
                e.Severity = RuleSeverity.Error
                Return False

            End If

            Return True

        End Function

        ''' <summary>
        ''' Validates a value of the <see cref="InvoiceReceivedItem.SumTotalLTL">InvoiceReceivedItem.SumTotalLTL</see>
        ''' property. Returnes false if the sum is invalid or a warning is generated.
        ''' </summary>
        ''' <param name="invoiceItem"></param>
        ''' <param name="e"></param>
        ''' <remarks>Complements validation performed by an invoice item.</remarks>
        Public Function ValidateTotalSum(ByVal invoiceItem As InvoiceReceivedItem, ByVal e As Csla.Validation.RuleArgs) As Boolean Implements IInvoiceAdapter.ValidateTotalSum

            If invoiceItem.SumTotalLTL < 0 Then

                e.Description = My.Resources.Documents_InvoiceAdapters_GoodsAcquisitionInvoiceAdapter_SumTotalInvalidReceived
                e.Severity = RuleSeverity.Error
                Return False

            ElseIf invoiceItem.SumTotalLTL > 0 AndAlso invoiceItem.IncludeVatInObject AndAlso _
                invoiceItem.GetTotalSumForInvoiceAdapter <> _GoodsAcquisition.TotalCost Then

                e.Description = String.Format(My.Resources.Documents_InvoiceAdapters_GoodsAcquisitionInvoiceAdapter_SumTotalMismatch, _
                    DblParser(invoiceItem.GetTotalSumForInvoiceAdapter, 2), DblParser(_GoodsAcquisition.TotalCost, 2))
                e.Severity = RuleSeverity.Error
                Return False

            End If

            Return True

        End Function

#End Region

#Region " Authorization Rules "

        Protected Overrides Sub AddAuthorizationRules()

        End Sub

        Public Shared Function CanGetObject() As Boolean
            Return InvoiceMade.CanAddObject OrElse InvoiceReceived.CanAddObject
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets a new instance of GoodsAcquisitionInvoiceAdapter to use it for creating a new invoice item.
        ''' </summary>
        ''' <param name="goodsId">An ID of the goods to be used as the base.</param>
        ''' <param name="parentChronologyValidator">A parent invoice validator.</param>
        ''' <param name="forInvoiceMade">Whether the object is ment fo adding to an InvoiceMade
        ''' (otherwise InvoiceReceived).</param>
        ''' <remarks></remarks>
        Public Shared Function NewGoodsAcquisitionInvoiceAdapter(ByVal goodsId As Integer, _
            ByVal parentChronologyValidator As IChronologicValidator, _
            ByVal forInvoiceMade As Boolean) As GoodsAcquisitionInvoiceAdapter
            Return DataPortal.Create(Of GoodsAcquisitionInvoiceAdapter) _
                (New Criteria(goodsId, parentChronologyValidator, forInvoiceMade))
        End Function

        ''' <summary>
        ''' Gets an existing instance of GoodsAcquisitionInvoiceAdapter from a database.
        ''' </summary>
        ''' <param name="attachedObjectId">An ID of the GoodsOperationAcquisition to be fetched.</param>
        ''' <param name="parentChronologyValidator">A parent invoice validator.</param>
        ''' <param name="forInvoiceMade">Whether the object is ment fo adding to an InvoiceMade
        ''' (otherwise InvoiceReceived).</param>
        ''' <remarks></remarks>
        Friend Shared Function GetGoodsAcquisitionInvoiceAdapter(ByVal attachedObjectId As Integer, _
            ByVal parentChronologyValidator As IChronologicValidator, _
            ByVal forInvoiceMade As Boolean) As GoodsAcquisitionInvoiceAdapter
            Return New GoodsAcquisitionInvoiceAdapter(attachedObjectId, parentChronologyValidator, forInvoiceMade)
        End Function


        Private Sub New()
            ' require use of factory methods
            MarkAsChild()
        End Sub

        Private Sub New(ByVal attachedObjectId As Integer, _
            ByVal parentChronologyValidator As IChronologicValidator, ByVal forInvoiceMade As Boolean)
            MarkAsChild()
            Fetch(attachedObjectId, parentChronologyValidator, forInvoiceMade)
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> _
       Private Class Criteria
            Private _ID As Integer
            Private _ParentChronologyValidator As IChronologicValidator
            Private _IsForInvoiceMade As Boolean
            Public ReadOnly Property ID() As Integer
                Get
                    Return _ID
                End Get
            End Property
            Public ReadOnly Property ParentChronologyValidator() As IChronologicValidator
                Get
                    Return _ParentChronologyValidator
                End Get
            End Property
            Public ReadOnly Property IsForInvoiceMade() As Boolean
                Get
                    Return _IsForInvoiceMade
                End Get
            End Property
            Public Sub New(ByVal nid As Integer, _
                ByVal nParentChronologyValidator As IChronologicValidator, ByVal forInvoiceMade As Boolean)
                _ID = nid
                _ParentChronologyValidator = nParentChronologyValidator
                _IsForInvoiceMade = forInvoiceMade
            End Sub
        End Class


        Private Overloads Sub DataPortal_Create(ByVal criteria As Criteria)

            If Not CanGetObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecuritySelectDenied)

            _GoodsAcquisition = GoodsOperationAcquisition.NewGoodsOperationAcquisitionChild( _
                criteria.ID, 0, criteria.ParentChronologyValidator)

            _IsForInvoiceMade = criteria.IsForInvoiceMade

        End Sub


        Private Sub Fetch(ByVal attachedObjectId As Integer, _
            ByVal parentChronologyValidator As IChronologicValidator, ByVal forInvoiceMade As Boolean)

            _GoodsAcquisition = GoodsOperationAcquisition.GetGoodsOperationAcquisitionChild( _
                attachedObjectId, parentChronologyValidator)
            _IsForInvoiceMade = forInvoiceMade

            MarkOld()

        End Sub


        ''' <summary>
        ''' Inserts the attached operation data into a database or updates the data in a database.
        ''' </summary>
        ''' <remarks>Inserts or updates the goods acquisition operation data.</remarks>
        Friend Sub Update(ByVal parentInvoice As InvoiceMade) Implements IInvoiceAdapter.Update
            _GoodsAcquisition.SaveChild(parentInvoice.ID, 0, True, _
                Not parentInvoice.ChronologyValidator.FinancialDataCanChange)
            MarkOld()
        End Sub

        ''' <summary>
        ''' Inserts the attached operation data into a database or updates the data in a database.
        ''' </summary>
        ''' <remarks>Inserts or updates the goods acquisition operation data.</remarks>
        Friend Sub Update(ByVal parentInvoice As InvoiceReceived) Implements IInvoiceAdapter.Update
            _GoodsAcquisition.SaveChild(parentInvoice.ID, 0, True, _
                Not parentInvoice.ChronologyValidator.FinancialDataCanChange)
            MarkOld()
        End Sub


        ''' <summary>
        ''' Deletes the attached operation data from a database.
        ''' </summary>
        ''' <remarks>Deletes goods acquisition operation data from a database.</remarks>
        Friend Sub DeleteSelf() Implements IInvoiceAdapter.DeleteSelf
            _GoodsAcquisition.DeleteGoodsOperationAcquisitionChild()
            MarkNew()
        End Sub


        ''' <summary>
        ''' Gets a book entries required by the service.
        ''' </summary>
        ''' <param name="invoiceItem"></param>
        ''' <remarks>Invokes <see cref="GoodsOperationAcquisition.GetTotalBookEntryList">GoodsOperationAcquisition.GetTotalBookEntryList</see>
        ''' method on encapsulated goods acquisition operation and returns the result.</remarks>
        Friend Function GetBookEntryList(ByVal invoiceItem As InvoiceMadeItem) As BookEntryInternalList Implements IInvoiceAdapter.GetBookEntryList

            Return _GoodsAcquisition.GetTotalBookEntryList()

        End Function

        ''' <summary>
        ''' Gets a book entries required by the service.
        ''' </summary>
        ''' <param name="invoiceItem"></param>
        ''' <remarks>Invokes <see cref="GoodsOperationAcquisition.GetTotalBookEntryList">GoodsOperationAcquisition.GetTotalBookEntryList</see>
        ''' method on encapsulated goods acquisition operation and returns the result.</remarks>
        Friend Function GetBookEntryList(ByVal invoiceItem As InvoiceReceivedItem) As BookEntryInternalList Implements IInvoiceAdapter.GetBookEntryList

            Return _GoodsAcquisition.GetTotalBookEntryList

        End Function

        ''' <summary>
        ''' Checks if the attached operation data can be deleted from a database. 
        ''' Throws an exception if not.
        ''' </summary>
        ''' <param name="parentChronologyValidator"></param>
        ''' <remarks>Invokes <see cref="GoodsOperationAcquisition.CheckIfCanDelete">GoodsOperationAcquisition.CheckIfCanDelete</see>
        ''' method on encapsulated goods acquisition operation.</remarks>
        Friend Sub CheckIfCanDelete(ByVal parentChronologyValidator As IChronologicValidator) Implements IInvoiceAdapter.CheckIfCanDelete
            _GoodsAcquisition.CheckIfCanDelete(parentChronologyValidator, Nothing)
        End Sub

        ''' <summary>
        ''' Checks if the attached operation data can be inserted into a database or updated. 
        ''' Throws an exception if not.
        ''' </summary>
        ''' <param name="parentChronologyValidator"></param>
        ''' <remarks>Invokes <see cref="GoodsOperationAcquisition.CheckIfCanUpdate">GoodsOperationAcquisition.CheckIfCanUpdate</see>
        ''' method on encapsulated goods acquisition operation.</remarks>
        Friend Sub CheckIfCanUpdate(ByVal parentChronologyValidator As IChronologicValidator) Implements IInvoiceAdapter.CheckIfCanUpdate
            _GoodsAcquisition.CheckIfCanUpdate(parentChronologyValidator, Nothing)
        End Sub

        ''' <summary>
        ''' Sets the encapsulated operation properties that are handled by the
        ''' parent <see cref="InvoiceMade">InvoiceMade</see>, e.g. date, document number, etc.
        ''' </summary>
        ''' <param name="parentInvoice"></param>
        ''' <remarks>Is invoked on each invoice item adapter before other validation 
        ''' and actual update is performed.</remarks>
        Public Sub SetParentData(ByVal parentInvoice As InvoiceMade) Implements IInvoiceAdapter.SetParentData
            _GoodsAcquisition.SetParentProperties(parentInvoice.Serial & parentInvoice.FullNumber, _
                String.Format(My.Resources.Documents_InvoiceAdapters_GoodsAcquisitionInvoiceAdapter_ContentInvoiceMade, _
                parentInvoice.Content))
            _GoodsAcquisition.SetDescription(String.Format(My.Resources.Documents_InvoiceAdapters_GoodsAcquisitionInvoiceAdapter_ContentInvoiceMade, _
                parentInvoice.Content))
        End Sub

        ''' <summary>
        ''' Sets the encapsulated operation properties that are handled by the
        ''' parent <see cref="InvoiceReceived">InvoiceReceived</see>, e.g. date, document number, etc.
        ''' </summary>
        ''' <param name="parentInvoice"></param>
        ''' <remarks>Is invoked on each invoice item adapter before other validation 
        ''' and actual update is performed.</remarks>
        Public Sub SetParentData(ByVal parentInvoice As InvoiceReceived) Implements IInvoiceAdapter.SetParentData
            _GoodsAcquisition.SetParentProperties(parentInvoice.Number, _
                String.Format(My.Resources.Documents_InvoiceAdapters_GoodsAcquisitionInvoiceAdapter_ContentInvoiceReceived, _
                parentInvoice.Content))
            _GoodsAcquisition.SetDescription(String.Format(My.Resources.Documents_InvoiceAdapters_GoodsAcquisitionInvoiceAdapter_ContentInvoiceReceived, _
                parentInvoice.Content))
        End Sub

#End Region

    End Class

End Namespace
