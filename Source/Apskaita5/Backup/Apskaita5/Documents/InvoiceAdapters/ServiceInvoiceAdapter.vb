Namespace Documents.InvoiceAdapters

    ''' <summary>
    ''' Represents an invoice item adapter for <see cref="Documents.Service">Service</see>.
    ''' </summary>
    ''' <remarks>An adapter between a <see cref="Service">Service</see> and
    ''' an <see cref="InvoiceMadeItem">InvoiceMadeItem</see>
    ''' or an <see cref="InvoiceReceivedItem">InvoiceReceivedItem</see>.
    ''' Can be added to an invoice by invoking <see cref="InvoiceMade.AttachNewObject">InvoiceMade.AttachNewObject</see>
    ''' or <see cref="InvoiceReceived.AttachNewObject">InvoiceReceived.AttachNewObject</see> methods.</remarks>
    <Serializable()> _
    Public NotInheritable Class ServiceInvoiceAdapter
        Inherits BusinessBase(Of ServiceInvoiceAdapter)
        Implements IRegionalDataObject, IInvoiceAdapter

#Region " Business Methods "

        Private ReadOnly _Guid As Guid = Guid.NewGuid
        Private _Service As ServiceInfo = Nothing
        Private _ChronologyValidator As IChronologicValidator = Nothing
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
        ''' <remarks>Corresponds to <see cref="Service.ID">Service.ID</see>.</remarks>
        Public ReadOnly Property Id() As Integer Implements IInvoiceAdapter.Id
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Service.ID
            End Get
        End Property

        ''' <summary>
        ''' Gets an ID of the object that the attached operation acts on.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="Service.ID">Service.ID</see>.</remarks>
        Public ReadOnly Property ObjectId() As Integer Implements IInvoiceAdapter.ObjectId
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Service.ID
            End Get
        End Property

        ''' <summary>
        ''' Gets a type of the attached object.
        ''' </summary>
        ''' <remarks>Returns <see cref="InvoiceAdapterType.Service">InvoiceAdapterType.Service</see>.</remarks>
        Public ReadOnly Property [Type]() As InvoiceAdapterType Implements IInvoiceAdapter.Type
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return InvoiceAdapterType.Service
            End Get
        End Property

        ''' <summary>
        ''' Gets an underlying attached operation.
        ''' </summary>
        ''' <remarks>Returns an incapsulated <see cref="HelperLists.ServiceInfo">ServiceInfo</see> instance.</remarks>
        Public ReadOnly Property ValueObject() As Object Implements IInvoiceAdapter.ValueObject
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Service
            End Get
        End Property

        ''' <summary>
        ''' Gets a type of the underlying attached operation.
        ''' </summary>
        ''' <remarks>Returns <see cref="HelperLists.ServiceInfo">HelperLists.ServiceInfo</see>.</remarks>
        Public ReadOnly Property ValueObjectType() As System.Type Implements IInvoiceAdapter.ValueObjectType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return GetType(HelperLists.ServiceInfo)
            End Get
        End Property

        ''' <summary>
        ''' Gets an <see cref="IChronologicValidator">IChronologicValidator</see> object that contains business restraints on updating the operation.
        ''' </summary>
        ''' <remarks>Always returns an instance of <see cref="EmptyChronologicValidator">EmptyChronologicValidator</see> for service.</remarks>
        Public ReadOnly Property ChronologyValidator() As IChronologicValidator Implements IInvoiceAdapter.ChronologyValidator
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ChronologyValidator
            End Get
        End Property

        ''' <summary>
        ''' Whether the underlying attached operation has any <see cref="csla.Validation.RuleSeverity.[Error]">errors</see>.
        ''' </summary>
        ''' <remarks>Service cannot be invalid. Returns FALSE.</remarks>
        Public ReadOnly Property ValueObjectHasErrors() As Boolean Implements IInvoiceAdapter.ValueObjectHasErrors
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return False
            End Get
        End Property

        ''' <summary>
        ''' Whether the underlying attached operation has any <see cref="csla.Validation.RuleSeverity.Warning">warnings</see>.
        ''' </summary>
        ''' <remarks>Service cannot be invalid. Returns FALSE.</remarks>
        Public ReadOnly Property ValueObjectHasWarnings() As Boolean Implements IInvoiceAdapter.ValueObjectHasWarnings
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return False
            End Get
        End Property

        ''' <summary>
        ''' Whether the underlying attached operation is dirty.
        ''' </summary>
        ''' <remarks>A proxy property to the <see cref="Csla.Core.BusinessBase.IsDirty">Csla.Core.BusinessBase.IsDirty</see>.</remarks>
        Public ReadOnly Property ValueObjectIsDirty() As Boolean Implements IInvoiceAdapter.ValueObjectIsDirty
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return MyBase.IsDirty
            End Get
        End Property

        ''' <summary>
        ''' Whether the underlying attached operation is new.
        ''' </summary>
        ''' <remarks>A proxy property to the <see cref="Csla.Core.BusinessBase.IsNew">Csla.Core.BusinessBase.IsNew</see>.</remarks>
        Public ReadOnly Property ValueObjectIsNew() As Boolean Implements IInvoiceAdapter.ValueObjectIsNew
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return MyBase.IsNew
            End Get
        End Property

        ''' <summary>
        ''' Whether the underlying attached operation implements 
        ''' <see cref="GetCopy">GetCopy</see> method, i.e. can be copied to a new invoice.
        ''' </summary>
        ''' <remarks>Returns TRUE for a service adapter.</remarks>
        Public ReadOnly Property ImplementsCopy() As Boolean Implements IInvoiceAdapter.ImplementsCopy
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return True
            End Get
        End Property


        ''' <summary>
        ''' Whether the <see cref="InvoiceMadeItem.NameInvoice">InvoiceMadeItem.NameInvoice</see>
        ''' or the <see cref="InvoiceReceivedItem.NameInvoice">InvoiceReceivedItem.NameInvoice</see>
        ''' properties should set by the corresponding attached operation property and vice versa 
        ''' (only to replace empty value, not to enforce equality).
        ''' </summary>
        ''' <remarks>Not to be used for regionalization.
        ''' Returns FALSE for service.</remarks>
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
        ''' Does nothing for service.</remarks>
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
        ''' Returns TRUE for service.</remarks>
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
        ''' Corresponds to <see cref="HelperLists.ServiceInfo.NameInvoice">ServiceInfo.NameInvoice</see>.</remarks>
        Public ReadOnly Property DefaultNameInvoice() As String Implements IInvoiceAdapter.DefaultNameInvoice
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Service.NameInvoice
            End Get
        End Property

        ''' <summary>
        ''' Whether the <see cref="InvoiceMadeItem.MeasureUnit">InvoiceMadeItem.MeasureUnit</see>
        ''' or the <see cref="InvoiceReceivedItem.MeasureUnit">InvoiceReceivedItem.MeasureUnit</see>
        ''' properties should set by the corresponding attached operation property and vice versa 
        ''' (only to replace empty value, not to enforce equality).
        ''' </summary>
        ''' <remarks>Not to be used for regionalization.
        ''' Returns FALSE for service.</remarks>
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
        ''' Does nothing for service.</remarks>
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
        ''' Returns TRUE for service.</remarks>
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
        ''' Corresponds to <see cref="HelperLists.ServiceInfo.MeasureUnit">ServiceInfo.MeasureUnit</see>.</remarks>
        Public ReadOnly Property DefaultMeasureUnit() As String Implements IInvoiceAdapter.DefaultMeasureUnit
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Service.MeasureUnit
            End Get
        End Property

        ''' <summary>
        ''' Whether the <see cref="InvoiceMadeItem.AccountIncome">InvoiceMadeItem.AccountIncome</see>
        ''' or the <see cref="InvoiceReceivedItem.AccountCosts">InvoiceReceivedItem.AccountCosts</see>
        ''' properties should set by the corresponding attached operation property and vice versa 
        ''' (to enforce equality).
        ''' </summary>
        ''' <remarks>Returnes FALSE for service.</remarks>
        Public ReadOnly Property HandlesAccount() As Boolean Implements IInvoiceAdapter.HandlesAccount
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return False
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets a value of the attached operation property that corresponds to the 
        ''' <see cref="InvoiceMadeItem.AccountIncome">InvoiceMadeItem.AccountIncome</see>
        ''' or the <see cref="InvoiceReceivedItem.AccountCosts">InvoiceReceivedItem.AccountCosts</see>
        ''' properties if the <see cref="HandlesAccount">HandlesAccount</see> is set to TRUE.
        ''' </summary>
        ''' <remarks>Does nothing in case of service.</remarks>
        Public Property Account() As Long Implements IInvoiceAdapter.Account
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return 0
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
        ''' <remarks>Returns TRUE for service.</remarks>
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
        ''' <remarks>Corresponds to <see cref="Service.AccountPurchase">Service.AccountPurchase</see>.</remarks>
        Public ReadOnly Property DefaultAccount() As Long Implements IInvoiceAdapter.DefaultAccount
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If _IsForInvoiceMade Then
                    Return _Service.AccountSales
                Else
                    Return _Service.AccountPurchase
                End If
            End Get
        End Property

        ''' <summary>
        ''' Whether the <see cref="InvoiceMadeItem.Ammount">InvoiceMadeItem.Ammount</see>
        ''' or the <see cref="InvoiceReceivedItem.Ammount">InvoiceReceivedItem.Ammount</see>
        ''' properties should set by the corresponding attached operation property and vice versa 
        ''' (to enforce equality).
        ''' </summary>
        ''' <remarks>Returns FALSE for service.</remarks>
        Public ReadOnly Property HandlesAmount() As Boolean Implements IInvoiceAdapter.HandlesAmount
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return False
            End Get
        End Property

        ''' <summary>
        ''' Gets or sets a value of the attached operation property that corresponds to the 
        ''' <see cref="InvoiceMadeItem.Ammount">InvoiceMadeItem.Ammount</see>
        ''' or the <see cref="InvoiceReceivedItem.Ammount">InvoiceReceivedItem.Ammount</see>
        ''' properties if the <see cref="HandlesAmount">HandlesAmount</see> is set to TRUE.
        ''' </summary>
        ''' <remarks>Does nothing in case of service.</remarks>
        Public ReadOnly Property Amount() As Double Implements IInvoiceAdapter.Amount
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return 0
            End Get
        End Property

        ''' <summary>
        ''' Whether the <see cref="InvoiceMadeItem.SumLTL">InvoiceMadeItem.SumLTL</see>
        ''' or the <see cref="InvoiceReceivedItem.SumLTL">InvoiceReceivedItem.SumLTL</see>
        ''' properties should set by the corresponding attached operation property and vice versa 
        ''' (to enforce equality).
        ''' </summary>
        ''' <remarks>Returns FALSE for service.</remarks>
        Public ReadOnly Property HandlesSum() As Boolean Implements IInvoiceAdapter.HandlesSum
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return False
            End Get
        End Property

        ''' <summary>
        ''' Gets a value of the attached operation property that corresponds to the 
        ''' <see cref="InvoiceMadeItem.SumLTL">InvoiceMadeItem.SumLTL</see>
        ''' or the <see cref="InvoiceReceivedItem.SumLTL">InvoiceReceivedItem.SumLTL</see>
        ''' properties if the <see cref="HandlesSum">HandlesSum</see> is set to TRUE.
        ''' </summary>
        ''' <remarks>Always returns 0 for service.</remarks>
        Public ReadOnly Property Sum() As Double Implements IInvoiceAdapter.Sum
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return 0
            End Get
        End Property

        ''' <summary>
        ''' Whether the <see cref="InvoiceMadeItem.VatRate">InvoiceMadeItem.VatRate</see>
        ''' or the <see cref="InvoiceReceivedItem.VatRate">InvoiceReceivedItem.VatRate</see>
        ''' properties should set by the corresponding attached operation property and vice versa 
        ''' (to enforce equality).
        ''' </summary>
        ''' <remarks>Returns FALSE for service.</remarks>
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
        ''' <remarks>Returns 0 for service.</remarks>
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
        ''' <remarks>Returns TRUE for service.</remarks>
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
        ''' <remarks>Corresponds to <see cref="Service.RateVatPurchase">Service.RateVatPurchase</see>.</remarks>
        Public ReadOnly Property DefaultVatRate() As Double Implements IInvoiceAdapter.DefaultVatRate
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If _IsForInvoiceMade Then
                    Return _Service.RateVatSales
                Else
                    Return _Service.RateVatPurchase
                End If
            End Get
        End Property

        ''' <summary>
        ''' Gets a value of the attached operation property that provides a default (initial) value for the 
        ''' <see cref="InvoiceMadeItem.DeclarationSchema">InvoiceMadeItem.DeclarationSchema</see>
        ''' or the <see cref="InvoiceReceivedItem.DeclarationSchema">InvoiceReceivedItem.DeclarationSchema</see>
        ''' properties if the <see cref="ProvidesDefaultVatRate">ProvidesDefaultVatRate</see> is set to TRUE.
        ''' </summary>
        ''' <remarks>Returns <see cref="ServiceInfo.DeclarationSchemaSales">ServiceInfo.DeclarationSchemaSales</see>
        ''' or <see cref="ServiceInfo.DeclarationSchemaPurchase">ServiceInfo.DeclarationSchemaPurchase</see>
        ''' subject to the invoice type.</remarks>
        Public ReadOnly Property DefaultDeclarationSchema() As VatDeclarationSchemaInfo _
            Implements IInvoiceAdapter.DefaultDeclarationSchema
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If _IsForInvoiceMade Then
                    Return _Service.DeclarationSchemaSales
                Else
                    Return _Service.DeclarationSchemaPurchase
                End If
            End Get
        End Property

        ''' <summary>
        ''' Whether the <see cref="InvoiceMadeItem.AccountVat">InvoiceMadeItem.AccountVat</see>
        ''' or the <see cref="InvoiceReceivedItem.AccountVat">InvoiceReceivedItem.AccountVat</see>
        ''' properties should be initialized by the corresponding attached operation property.
        ''' </summary>
        ''' <remarks>Returns TRUE for service.</remarks>
        Public ReadOnly Property ProvidesDefaultVatAccount() As Boolean Implements IInvoiceAdapter.ProvidesDefaultVatAccount
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return True
            End Get
        End Property

        ''' <summary>
        ''' Gets a value of the attached operation property that provides a default (initial) value for the 
        ''' <see cref="InvoiceReceivedItem.AccountVat">InvoiceReceivedItem.AccountVat</see>
        ''' or the <see cref="InvoiceMadeItem.AccountVat">InvoiceMadeItem.AccountVat</see>
        ''' properties if the <see cref="ProvidesDefaultVatRate">ProvidesDefaultAccountVat</see> is set to TRUE.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="Service.AccountVatPurchase">Service.AccountVatPurchase</see>.</remarks>
        Public ReadOnly Property DefaultVatAccount() As Long Implements IInvoiceAdapter.DefaultVatAccount
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If _IsForInvoiceMade Then
                    Return _Service.AccountVatSales
                Else
                    Return _Service.AccountVatPurchase
                End If
            End Get
        End Property


        ''' <summary>
        ''' Whether the operation is compartible with a discount within an invoice item,
        ''' i.e. <see cref="InvoiceMadeItem.Discount">InvoiceMadeItem.Discount</see>
        ''' and other discount related properties can take non zero values.
        ''' </summary>
        ''' <remarks>Returns TRUE for service.</remarks>
        Public ReadOnly Property DiscountIsAllowed() As Boolean Implements IInvoiceAdapter.DiscountIsAllowed
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return True
            End Get
        End Property

        ''' <summary>
        ''' Whether the <see cref="InvoiceMadeItem.SumVatLTL">InvoiceMadeItem.SumVatLTL</see>
        ''' or the <see cref="InvoiceReceivedItem.SumVatLTL">InvoiceReceivedItem.SumVatLTL</see>
        ''' can be handled by the attached operation for the purpose of generating book entries. 
        ''' E.g. included into the aquisition costs of an asset.
        ''' </summary>
        ''' <remarks>Returnes FALSE for service.</remarks>
        Public ReadOnly Property SumVatIsHandledOnRequest() As Boolean Implements IInvoiceAdapter.SumVatIsHandledOnRequest
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return False
            End Get
        End Property


        ''' <summary>
        ''' Gets an ID of the regionalizable object.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="Service.ID">Service.ID</see>.</remarks>
        Public ReadOnly Property RegionalObjectID() As Integer Implements IRegionalDataObject.RegionalObjectID
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Service.ID
            End Get
        End Property

        ''' <summary>
        ''' Gets a type of the regionalizable object.
        ''' </summary>
        ''' <remarks>Returnes <see cref="RegionalizedObjectType.Service">RegionalizedObjectType.Service</see>.</remarks>
        Public ReadOnly Property RegionalObjectType() As RegionalizedObjectType Implements IRegionalDataObject.RegionalObjectType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return RegionalizedObjectType.Service
            End Get
        End Property



        ''' <summary>
        ''' Gets all the <see cref="csla.Validation.RuleSeverity.[Error]">errors</see> within the attached operation.
        ''' </summary>
        ''' <remarks>Service cannot be invalid. Returns an empty string.</remarks>
        Public Function GetAllErrors() As String Implements IInvoiceAdapter.GetAllErrors
            Return ""
        End Function

        ''' <summary>
        ''' Gets all the <see cref="csla.Validation.RuleSeverity.Warning">warnings</see> within the attached operation.
        ''' </summary>
        ''' <remarks>Service cannot be invalid. Returns an empty string.</remarks>
        Public Function GetAllWarnings() As String Implements IInvoiceAdapter.GetAllWarnings
            Return ""
        End Function


        ''' <summary>
        ''' Sets the attached operation date to invoice date.
        ''' </summary>
        ''' <param name="newInvoiceDate"></param>
        ''' <remarks>Does nothing for service.</remarks>
        Public Sub SetInvoiceDate(ByVal newInvoiceDate As Date) Implements IInvoiceAdapter.SetInvoiceDate

        End Sub

        ''' <summary>
        ''' Sets the attached operation financial data (unit value, amount and total sum) 
        ''' with the values of InvoiceMadeItem.
        ''' </summary>
        ''' <param name="parentInvoiceMadeItem"></param>
        ''' <remarks>Does nothing for service.</remarks>
        Public Sub SetInvoiceFinancialData(ByVal parentInvoiceMadeItem As InvoiceMadeItem) Implements IInvoiceAdapter.SetInvoiceFinancialData

        End Sub

        ''' <summary>
        ''' Sets the attached operation financial data (unit value, amount and total sum) 
        ''' with the values of InvoiceReceivedItem.
        ''' </summary>
        ''' <param name="parentInvoiceReceivedItem"></param>
        ''' <remarks>Does nothing for service.</remarks>
        Public Sub SetInvoiceFinancialData(ByVal parentInvoiceReceivedItem As InvoiceReceivedItem) Implements IInvoiceAdapter.SetInvoiceFinancialData

        End Sub

        ''' <summary>
        ''' Gets a copy of the adapter. 
        ''' </summary>
        ''' <remarks>Used to support invoice copy functionality.
        ''' Should always check <see cref="ImplementsCopy">ImplementsCopy</see>
        ''' property before invoking this method.</remarks>
        Friend Function GetCopy() As IInvoiceAdapter Implements IInvoiceAdapter.GetCopy
            Return Me.Clone
        End Function


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


        Protected Overrides Function GetIdValue() As Object
            Return _Guid
        End Function

        Public Overrides Function ToString() As String
            Return String.Format(My.Resources.Documents_InvoiceAdapters_ServiceInvoiceAdapter_ToString, _
                _Service.ToString)
        End Function

#End Region

#Region " Validation Rules "

        ''' <summary>
        ''' Validates a value of the <see cref="InvoiceMadeItem.AccountIncome">InvoiceMadeItem.AccountIncome</see>
        ''' property. Returnes false if the sum is invalid or a warning is generated.
        ''' </summary>
        ''' <param name="invoiceItem"></param>
        ''' <param name="e"></param>
        ''' <remarks>Only used if <see cref="HandlesAccount">HandlesAccount</see> is set to TRUE.
        ''' Overrides validation of a invoice item. Full validation needs to be performed.
        ''' A service does not handle account and cannot replace account validation, 
        ''' throws an exception if invoked.</remarks>
        Public Function ValidateAccount(ByVal invoiceItem As InvoiceMadeItem, ByVal e As Csla.Validation.RuleArgs) As Boolean Implements IInvoiceAdapter.ValidateAccount
            Throw New InvalidOperationException(String.Format( _
                My.Resources.Documents_InvoiceAdapters_IInvoiceAdapter_InvalidAccountValidationOperation, _
                My.Resources.Documents_InvoiceAdapters_ServiceInvoiceAdapter_TypeName))
        End Function

        ''' <summary>
        ''' Validates a value of the <see cref="InvoiceReceivedItem.AccountCosts">InvoiceReceivedItem.AccountCosts</see>
        ''' property. Returnes false if the sum is invalid or a warning is generated.
        ''' </summary>
        ''' <param name="invoiceItem"></param>
        ''' <param name="e"></param>
        ''' <remarks>Only used if <see cref="HandlesAccount">HandlesAccount</see> is set to TRUE.
        ''' Overrides validation of a invoice item. Full validation needs to be performed.
        ''' A service does not handle account and cannot replace account validation, 
        ''' throws an exception if invoked.</remarks>
        Public Function ValidateAccount(ByVal invoiceItem As InvoiceReceivedItem, ByVal e As Csla.Validation.RuleArgs) As Boolean Implements IInvoiceAdapter.ValidateAccount
            Throw New InvalidOperationException(String.Format( _
                My.Resources.Documents_InvoiceAdapters_IInvoiceAdapter_InvalidAccountValidationOperation, _
                My.Resources.Documents_InvoiceAdapters_ServiceInvoiceAdapter_TypeName))
        End Function

        ''' <summary>
        ''' Validates a value of the <see cref="InvoiceMadeItem.Ammount">InvoiceMadeItem.Ammount</see>
        ''' property. Returnes false if the amount is invalid or a warning is generated.
        ''' </summary>
        ''' <param name="invoiceItem"></param>
        ''' <param name="e"></param>
        ''' <remarks>Complements validation performed by an invoice item.
        ''' A service does not handle amount, no validation occures (returns TRUE).</remarks>
        Public Function ValidateAmount(ByVal invoiceItem As InvoiceMadeItem, ByVal e As Csla.Validation.RuleArgs) As Boolean Implements IInvoiceAdapter.ValidateAmount
            Return True
        End Function

        ''' <summary>
        ''' Validates a value of the <see cref="InvoiceReceivedItem.Ammount">InvoiceReceivedItem.Ammount</see>
        ''' properties. Returnes false if the amount is invalid or a warning is generated.
        ''' </summary>
        ''' <param name="invoiceItem"></param>
        ''' <param name="e"></param>
        ''' <remarks>Complements validation performed by an invoice item.
        ''' A service does not handle amount, no validation occures (returns TRUE).</remarks>
        Public Function ValidateAmount(ByVal invoiceItem As InvoiceReceivedItem, ByVal e As Csla.Validation.RuleArgs) As Boolean Implements IInvoiceAdapter.ValidateAmount
            Return True
        End Function

        ''' <summary>
        ''' Validates a value of the <see cref="InvoiceMadeItem.SumLTL">InvoiceMadeItem.SumLTL</see>
        ''' property. Returnes false if the sum is invalid or a warning is generated.
        ''' </summary>
        ''' <param name="invoiceItem"></param>
        ''' <param name="e"></param>
        ''' <remarks>Complements validation performed by an invoice item.
        ''' A service does not handle sum, no validation occures (returns TRUE).</remarks>
        Public Function ValidateSum(ByVal invoiceItem As InvoiceMadeItem, ByVal e As Csla.Validation.RuleArgs) As Boolean Implements IInvoiceAdapter.ValidateSum
            Return True
        End Function

        ''' <summary>
        ''' Validates a value of the <see cref="InvoiceReceivedItem.SumLTL">InvoiceReceivedItem.SumLTL</see>
        ''' property. Returnes false if the sum is invalid or a warning is generated.
        ''' </summary>
        ''' <param name="invoiceItem"></param>
        ''' <param name="e"></param>
        ''' <remarks>Complements validation performed by an invoice item.
        ''' A service does not handle sum, no validation occures (returns TRUE).</remarks>
        Public Function ValidateSum(ByVal invoiceItem As InvoiceReceivedItem, ByVal e As Csla.Validation.RuleArgs) As Boolean Implements IInvoiceAdapter.ValidateSum
            Return True
        End Function

        ''' <summary>
        ''' Validates a value of the <see cref="InvoiceMadeItem.SumTotalLTL">InvoiceMadeItem.SumTotalLTL</see>
        ''' property. Returnes false if the sum is invalid or a warning is generated.
        ''' </summary>
        ''' <param name="invoiceItem"></param>
        ''' <param name="e"></param>
        ''' <remarks>Complements validation performed by an invoice item.
        ''' A service does not handle sum, no validation occures (returns TRUE).</remarks>
        Public Function ValidateTotalSum(ByVal invoiceItem As InvoiceMadeItem, ByVal e As Csla.Validation.RuleArgs) As Boolean Implements IInvoiceAdapter.ValidateTotalSum
            Return True
        End Function

        ''' <summary>
        ''' Validates a value of the <see cref="InvoiceReceivedItem.SumTotalLTL">InvoiceReceivedItem.SumTotalLTL</see>
        ''' property. Returnes false if the sum is invalid or a warning is generated.
        ''' </summary>
        ''' <param name="invoiceItem"></param>
        ''' <param name="e"></param>
        ''' <remarks>Complements validation performed by an invoice item.
        ''' A service does not handle sum, no validation occures (returns TRUE).</remarks>
        Public Function ValidateTotalSum(ByVal invoiceItem As InvoiceReceivedItem, ByVal e As Csla.Validation.RuleArgs) As Boolean Implements IInvoiceAdapter.ValidateTotalSum
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
        ''' Gets a new instance of ServiceInvoiceAdapter to use it for creating a new invoice item.
        ''' </summary>
        ''' <param name="serviceId">An ID of the ServiceInfo instance to be used as the base.</param>
        ''' <param name="parentChronologyValidator">A parent invoice validator.</param>
        ''' <param name="forInvoiceMade">Whether the object is ment fo adding to an InvoiceMade
        ''' (otherwise InvoiceReceived).</param>
        ''' <remarks></remarks>
        Public Shared Function NewServiceInvoiceAdapter(ByVal serviceId As Integer, _
            ByVal parentChronologyValidator As IChronologicValidator, _
            ByVal forInvoiceMade As Boolean) As ServiceInvoiceAdapter
            Return DataPortal.Create(Of ServiceInvoiceAdapter) _
                (New Criteria(serviceId, parentChronologyValidator, forInvoiceMade))
        End Function

        ''' <summary>
        ''' Gets a new instance of ServiceInvoiceAdapter to use it for creating a new invoice item.
        ''' </summary>
        ''' <param name="nService">A ServiceInfo instance to be used as the base.</param>
        ''' <param name="parentChronologyValidator">A parent invoice validator.</param>
        ''' <param name="forInvoiceMade">Whether the object is ment fo adding to an InvoiceMade
        ''' (otherwise InvoiceReceived).</param>
        ''' <remarks></remarks>
        Public Shared Function NewServiceInvoiceAdapter(ByVal nService As ServiceInfo, _
            ByVal parentChronologyValidator As IChronologicValidator, _
            ByVal forInvoiceMade As Boolean) As ServiceInvoiceAdapter
            Return New ServiceInvoiceAdapter(nService, parentChronologyValidator, forInvoiceMade)
        End Function

        ''' <summary>
        ''' Gets an existing instance of ServiceInvoiceAdapter from a database.
        ''' </summary>
        ''' <param name="attachedObjectId">An ID of the ServiceInfo to be fetched.</param>
        ''' <param name="parentChronologyValidator">A parent invoice validator.</param>
        ''' <param name="forInvoiceMade">Whether the object is ment fo adding to an InvoiceMade
        ''' (otherwise InvoiceReceived).</param>
        ''' <remarks></remarks>
        Friend Shared Function GetServiceInvoiceAdapter(ByVal attachedObjectId As Integer, _
            ByVal parentChronologyValidator As IChronologicValidator, _
            ByVal forInvoiceMade As Boolean) As ServiceInvoiceAdapter
            Return New ServiceInvoiceAdapter(attachedObjectId, parentChronologyValidator, forInvoiceMade)
        End Function


        Private Sub New()
            ' require use of factory methods
            MarkAsChild()
        End Sub

        Private Sub New(ByVal nService As ServiceInfo, _
            ByVal parentChronologyValidator As IChronologicValidator, ByVal forInvoiceMade As Boolean)
            MarkAsChild()
            Create(nService, parentChronologyValidator, forInvoiceMade)
        End Sub

        Private Sub New(ByVal attachedObjectID As Integer, _
            ByVal parentChronologyValidator As IChronologicValidator, ByVal forInvoiceMade As Boolean)
            MarkAsChild()
            Fetch(attachedObjectID, parentChronologyValidator, forInvoiceMade)
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> _
        Private Class Criteria
            Private _ID As Integer
            Private _ParentChronologyValidator As IChronologicValidator
            Private _IsForInvoiceMade As Boolean
            Public ReadOnly Property IsForInvoiceMade() As Boolean
                Get
                    Return _IsForInvoiceMade
                End Get
            End Property
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
            Public Sub New(ByVal nid As Integer, _
                ByVal nParentChronologyValidator As IChronologicValidator, ByVal forInvoiceMade As Boolean)
                _ID = nid
                _ParentChronologyValidator = nParentChronologyValidator
                _IsForInvoiceMade = forInvoiceMade
            End Sub
        End Class


        Private Overloads Sub DataPortal_Create(ByVal criteria As Criteria)
            Dim newService As ServiceInfo = ServiceInfo.GetServiceInfo(criteria.ID)
            Create(newService, criteria.ParentChronologyValidator, criteria.IsForInvoiceMade)
        End Sub

        Private Sub Create(ByVal nService As ServiceInfo, _
            ByVal parentChronologyValidator As IChronologicValidator, ByVal forInvoiceMade As Boolean)

            If nService Is Nothing OrElse nService.IsEmpty Then
                Throw New ArgumentNullException("nService")
            End If

            If Not CanGetObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecuritySelectDenied)

            _Service = nService
            _ChronologyValidator = EmptyChronologicValidator.NewEmptyChronologicValidator( _
                My.Resources.Documents_InvoiceAdapters_ServiceInvoiceAdapter_TypeName, parentChronologyValidator)
            _IsForInvoiceMade = forInvoiceMade

        End Sub


        Private Sub Fetch(ByVal attachedObjectId As Integer, _
            ByVal parentChronologyValidator As IChronologicValidator, ByVal forInvoiceMade As Boolean)

            _Service = HelperLists.ServiceInfo.GetServiceInfo(attachedObjectId)
            _ChronologyValidator = EmptyChronologicValidator.NewEmptyChronologicValidator( _
                My.Resources.Documents_InvoiceAdapters_ServiceInvoiceAdapter_TypeName, parentChronologyValidator)
            _IsForInvoiceMade = forInvoiceMade

            MarkOld()

        End Sub


        ''' <summary>
        ''' Inserts the attached operation data into a database or updates the data in a database.
        ''' </summary>
        ''' <remarks>Does nothing for service.</remarks>
        Friend Sub Update(ByVal parentInvoice As InvoiceMade) Implements IInvoiceAdapter.Update
            ' nothing to do for service
            MarkOld()
        End Sub

        ''' <summary>
        ''' Inserts the attached operation data into a database or updates the data in a database.
        ''' </summary>
        ''' <remarks>Does nothing for service.</remarks>
        Friend Sub Update(ByVal parentInvoice As InvoiceReceived) Implements IInvoiceAdapter.Update
            ' nothing to do for service
            MarkOld()
        End Sub


        ''' <summary>
        ''' Deletes the attached operation data from a database.
        ''' </summary>
        ''' <remarks>Does nothing for service.</remarks>
        Friend Sub DeleteSelf() Implements IInvoiceAdapter.DeleteSelf
            ' nothing to do for service
            MarkNew()
        End Sub


        ''' <summary>
        ''' Gets a book entries required by the service.
        ''' </summary>
        ''' <param name="invoiceItem"></param>
        ''' <remarks></remarks>
        Friend Function GetBookEntryList(ByVal invoiceItem As InvoiceMadeItem) As BookEntryInternalList Implements IInvoiceAdapter.GetBookEntryList

            Dim result As BookEntryInternalList = BookEntryInternalList. _
                NewBookEntryInternalList(BookEntryType.Debetas)

            If CRound(invoiceItem.SumLTL) > 0 Then

                result.Add(BookEntryInternal.NewBookEntryInternal(BookEntryType.Kreditas, _
                    invoiceItem.AccountIncome, CRound(invoiceItem.SumLTL), Nothing))

            Else

                result.Add(BookEntryInternal.NewBookEntryInternal(BookEntryType.Debetas, _
                    invoiceItem.AccountIncome, CRound(-invoiceItem.SumLTL), Nothing))

            End If

            Return result

        End Function

        ''' <summary>
        ''' Gets a book entries required by the service.
        ''' </summary>
        ''' <param name="invoiceItem"></param>
        ''' <remarks></remarks>
        Friend Function GetBookEntryList(ByVal invoiceItem As InvoiceReceivedItem) As BookEntryInternalList Implements IInvoiceAdapter.GetBookEntryList

            Dim result As BookEntryInternalList = BookEntryInternalList. _
                NewBookEntryInternalList(BookEntryType.Debetas)

            If CRound(invoiceItem.SumLTL) > 0 Then

                result.Add(BookEntryInternal.NewBookEntryInternal(BookEntryType.Debetas, _
                    invoiceItem.AccountCosts, CRound(invoiceItem.SumLTL), Nothing))

            Else

                result.Add(BookEntryInternal.NewBookEntryInternal(BookEntryType.Kreditas, _
                    invoiceItem.AccountCosts, CRound(-invoiceItem.SumLTL), Nothing))

            End If

            Return result

        End Function

        ''' <summary>
        ''' Checks if the attached operation data can be deleted from a database. 
        ''' Throws an exception if not.
        ''' </summary>
        ''' <param name="parentChronologyValidator"></param>
        ''' <remarks>Does nothing for service.</remarks>
        Friend Sub CheckIfCanDelete(ByVal parentChronologyValidator As IChronologicValidator) Implements IInvoiceAdapter.CheckIfCanDelete
            ' nothing to do for service
        End Sub

        ''' <summary>
        ''' Checks if the attached operation data can be inserted into a database or updated. 
        ''' Throws an exception if not.
        ''' </summary>
        ''' <param name="parentChronologyValidator"></param>
        ''' <remarks>Does nothing for service.</remarks>
        Friend Sub CheckIfCanUpdate(ByVal parentChronologyValidator As IChronologicValidator) Implements IInvoiceAdapter.CheckIfCanUpdate
            ' nothing to do for service
        End Sub

        ''' <summary>
        ''' Sets the encapsulated operation properties that are handled by the
        ''' parent <see cref="InvoiceMade">InvoiceMade</see>, e.g. date, document number, etc.
        ''' </summary>
        ''' <param name="parentInvoice"></param>
        ''' <remarks>Is invoked on each invoice item adapter before other validation 
        ''' and actual update is performed.
        ''' In case of service nothing needs to be set.</remarks>
        Public Sub SetParentData(ByVal parentInvoice As InvoiceMade) Implements IInvoiceAdapter.SetParentData
            ' In case of service nothing needs to be set.
        End Sub

        ''' <summary>
        ''' Sets the encapsulated operation properties that are handled by the
        ''' parent <see cref="InvoiceReceived">InvoiceReceived</see>, e.g. date, document number, etc.
        ''' </summary>
        ''' <param name="parentInvoice"></param>
        ''' <remarks>Is invoked on each invoice item adapter before other validation 
        ''' and actual update is performed.
        ''' In case of service nothing needs to be set.</remarks>
        Public Sub SetParentData(ByVal parentInvoice As InvoiceReceived) Implements IInvoiceAdapter.SetParentData
            ' In case of service nothing needs to be set.
        End Sub

#End Region

    End Class

End Namespace