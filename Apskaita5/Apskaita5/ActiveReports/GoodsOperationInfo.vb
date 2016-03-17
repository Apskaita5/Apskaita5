Namespace ActiveReports

    ''' <summary>
    ''' Represents an item in a <see cref="GoodsOperationInfoListParent">
    ''' goods operation report</see>, contains information about a single
    ''' goods operation.
    ''' </summary>
    ''' <remarks>Aggregates information about <see cref="Goods.OperationPersistenceObject">
    ''' simple goods operations</see>, <see cref="Goods.GoodsOperationAccountChange">
    ''' goods account change operations</see> and <see cref="Goods.GoodsOperationValuationMethod">
    ''' goods valuation method change operatiions</see>.
    ''' Should only be used as a child of <see cref="GoodsOperationInfoList">GoodsOperationInfoList</see>.</remarks>
    <Serializable()> _
    Public Class GoodsOperationInfo
        Inherits ReadOnlyBase(Of GoodsOperationInfo)

#Region " Business Methods "

        Private ReadOnly _Guid As Guid = Guid.NewGuid()
        Private _ID As Integer = 0
        Private _WarehouseID As Integer = 0
        Private _WarehouseName As String = ""
        Private _WarehouseAccount As Long = 0
        Private _Date As Date = Today
        Private _Type As String = ""
        Private _ComplexType As String = ""
        Private _DocNo As String = ""
        Private _Content As String = ""
        Private _Amount As Double = 0
        Private _AmountInWarehouse As Double = 0
        Private _UnitValue As Double = 0
        Private _TotalValue As Double = 0
        Private _AccountGeneral As Double = 0
        Private _AccountSalesNetCosts As Double = 0
        Private _AccountPurchases As Double = 0
        Private _AccountDiscounts As Double = 0
        Private _AccountPriceCut As Double = 0
        Private _AccountOperationValue As Double = 0
        Private _AccountOperation As Long = 0
        Private _ComplexOperationID As Integer = 0
        Private _InsertDate As DateTime = Now
        Private _UpdateDate As DateTime = Now
        Private _JournalEntryID As Integer = 0
        Private _JournalEntryDocNo As String = ""
        Private _JournalEntryDate As Date = Today
        Private _JournalEntryContent As String = ""
        Private _JournalEntryType As String = ""
        Private _JournalEntryCorrespondentions As String = ""


        ''' <summary>
        ''' Gets an ID of the operation.
        ''' </summary>
        ''' <remarks>Subject to the <see cref="Type">Type</see> corresponds to:
        ''' <see cref="Goods.OperationPersistenceObject.ID">OperationPersistenceObject.ID</see>, 
        ''' <see cref="Goods.GoodsOperationAccountChange.ID">GoodsOperationAccountChange.ID</see>
        ''' or <see cref="Goods.GoodsOperationValuationMethod.ID">GoodsOperationValuationMethod.ID</see>.</remarks>
        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' Gets the date and time when the operation was inserted into the database.
        ''' </summary>
        ''' <remarks>Subject to the <see cref="Type">Type</see> corresponds to:
        ''' <see cref="Goods.OperationPersistenceObject.InsertDate">OperationPersistenceObject.InsertDate</see>, 
        ''' <see cref="Goods.GoodsOperationAccountChange.InsertDate">GoodsOperationAccountChange.InsertDate</see>
        ''' or <see cref="Goods.GoodsOperationValuationMethod.InsertDate">GoodsOperationValuationMethod.InsertDate</see>.</remarks>
        Public ReadOnly Property InsertDate() As DateTime
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _InsertDate
            End Get
        End Property

        ''' <summary>
        ''' Gets the date and time when the operation was last updated.
        ''' </summary>
        ''' <remarks>Subject to the <see cref="Type">Type</see> corresponds to:
        ''' <see cref="Goods.OperationPersistenceObject.UpdateDate">OperationPersistenceObject.UpdateDate</see>, 
        ''' <see cref="Goods.GoodsOperationAccountChange.UpdateDate">GoodsOperationAccountChange.UpdateDate</see>
        ''' or <see cref="Goods.GoodsOperationValuationMethod.UpdateDate">GoodsOperationValuationMethod.UpdateDate</see>.</remarks>
        Public ReadOnly Property UpdateDate() As DateTime
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _UpdateDate
            End Get
        End Property

        ''' <summary>
        ''' Gets a type of the goods operation.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property [Type]() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Type.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets an <see cref="Goods.ComplexOperationPersistenceObject.ID">ID of
        ''' the complex goods operation document</see> that the operation belongs to.
        ''' </summary>
        ''' <remarks>Subject to the <see cref="Type">Type</see> corresponds to:
        ''' <see cref="Goods.OperationPersistenceObject.ComplexOperationID">OperationPersistenceObject.ComplexOperationID</see> 
        ''' or zero.</remarks>
        Public ReadOnly Property ComplexOperationID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ComplexOperationID
            End Get
        End Property

        ''' <summary>
        ''' Gets a type (as a localized human readable string) of the complex 
        ''' goods operation document that the operation belongs to.
        ''' </summary>
        ''' <remarks>Subject to the <see cref="Type">Type</see> corresponds to:
        ''' <see cref="Goods.OperationPersistenceObject.ComplexOperationType">OperationPersistenceObject.ComplexOperationType</see> 
        ''' or an empty string.</remarks>
        Public ReadOnly Property ComplexType() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ComplexType.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets an <see cref="Goods.Warehouse.ID">ID of the warehouse</see>
        ''' that the operation operates in.
        ''' </summary>
        ''' <remarks>Subject to the <see cref="Type">Type</see> corresponds to:
        ''' <see cref="Goods.OperationPersistenceObject.WarehouseID">OperationPersistenceObject.WarehouseID</see>.
        ''' or zero.</remarks>
        Public ReadOnly Property WarehouseID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _WarehouseID
            End Get
        End Property

        ''' <summary>
        ''' Gets a <see cref="Goods.Warehouse.Name">name of the warehouse</see>
        ''' that the operation operates in.
        ''' </summary>
        ''' <remarks>Subject to the <see cref="Type">Type</see> corresponds to:
        ''' <see cref="Goods.OperationPersistenceObject.Warehouse">OperationPersistenceObject.Warehouse</see>.
        ''' or an empty string.</remarks>
        Public ReadOnly Property WarehouseName() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _WarehouseName.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets an <see cref="Goods.Warehouse.WarehouseAccount">account of the warehouse</see>
        ''' that the operation operates in.
        ''' </summary>
        ''' <remarks>Subject to the <see cref="Type">Type</see> corresponds to:
        ''' <see cref="Goods.OperationPersistenceObject.Warehouse">OperationPersistenceObject.Warehouse</see>.
        ''' or zero.</remarks>
        Public ReadOnly Property WarehouseAccount() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _WarehouseAccount
            End Get
        End Property

        ''' <summary>
        ''' Gets a date of the operation.
        ''' </summary>
        ''' <remarks>Subject to the <see cref="Type">Type</see> corresponds to:
        ''' <see cref="Goods.OperationPersistenceObject.OperationDate">OperationPersistenceObject.OperationDate</see>, 
        ''' <see cref="Goods.GoodsOperationAccountChange.[Date]">GoodsOperationAccountChange.Date</see>
        ''' or <see cref="Goods.GoodsOperationValuationMethod.[Date]">GoodsOperationValuationMethod.Date</see>.</remarks>
        Public ReadOnly Property [Date]() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Date
            End Get
        End Property

        ''' <summary>
        ''' Gets a document number of the operation.
        ''' </summary>
        ''' <remarks>Subject to the <see cref="Type">Type</see> corresponds to:
        ''' <see cref="Goods.OperationPersistenceObject.DocNo">OperationPersistenceObject.DocNo</see>, 
        ''' <see cref="Goods.GoodsOperationAccountChange.DocumentNumber">GoodsOperationAccountChange.DocumentNumber</see>
        ''' or an empty string.
        ''' The following precedence applies for the document number:
        ''' 1. <see cref="General.JournalEntry.DocNumber">a number of the 
        ''' encapsulated or associated journal entry</see> if any;
        ''' 2. <see cref="Goods.ComplexOperationPersistenceObject.DocNo">
        ''' a number of the complex operation document</see> if any;
        ''' 3. a number of the simple goods operation.</remarks>
        Public ReadOnly Property DocNo() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DocNo.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a content (description) of the operation.
        ''' </summary>
        ''' <remarks><see cref="Goods.OperationPersistenceObject.Content">OperationPersistenceObject.Content</see>, 
        ''' <see cref="Goods.GoodsOperationAccountChange.Description">GoodsOperationAccountChange.Description</see>
        ''' or <see cref="Goods.GoodsOperationValuationMethod.Description">GoodsOperationValuationMethod.Description</see>.
        ''' If the operation is a part of a complex goods operation document, 
        ''' <see cref="Goods.ComplexOperationPersistenceObject.Content">
        ''' a content of the complex operation document</see> is concatenated.</remarks>
        Public ReadOnly Property Content() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Content.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a total change of the goods amount. (irrespective of whether 
        ''' the change is accounted in the general ledger or not)
        ''' </summary>
        ''' <remarks>Could be positive (amount increase) or negative (amount decrease).
        ''' Subject to the <see cref="Type">Type</see> corresponds to:
        ''' <see cref="Goods.OperationPersistenceObject.Amount">OperationPersistenceObject.Amount</see>.
        ''' or zero.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDAMOUNTGOODS)> _
        Public ReadOnly Property Amount() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_Amount, ROUNDAMOUNTGOODS)
            End Get
        End Property

        ''' <summary>
        ''' Gets a change of the goods amount that is accounted in the
        ''' <see cref="Goods.Warehouse.WarehouseAccount">warehouse account</see>.
        '''  </summary>
        ''' <remarks>Could be positive (amount increase) or negative (amount decrease).
        ''' Subject to the <see cref="Type">Type</see> corresponds to:
        ''' <see cref="Goods.OperationPersistenceObject.AmountInWarehouse">OperationPersistenceObject.AmountInWarehouse</see>.
        ''' or zero.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDAMOUNTGOODS)> _
        Public ReadOnly Property AmountInWarehouse() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AmountInWarehouse, ROUNDAMOUNTGOODS)
            End Get
        End Property

        ''' <summary>
        ''' Gets a goods value per unit within the operation. 
        ''' </summary>
        ''' <remarks>Either acquisition price or discard costs, i.e. always positive
        ''' (or zero, if the unit value is not applicable for the operation,
        ''' e.g. <see cref="Goods.GoodsOperationPriceCut">GoodsOperationPriceCut</see>).
        ''' Subject to the <see cref="Type">Type</see> corresponds to:
        ''' <see cref="Goods.OperationPersistenceObject.UnitValue">OperationPersistenceObject.UnitValue</see>.
        ''' or zero.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, False, ROUNDUNITGOODS)> _
        Public ReadOnly Property UnitValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_UnitValue, ROUNDUNITGOODS)
            End Get
        End Property

        ''' <summary>
        ''' Gets a total goods value change within the <see cref="Goods.Warehouse">warehouse</see>. 
        ''' </summary>
        ''' <remarks>Could be positive (acquisition, additional costs) 
        ''' or negative (transfer, discard, discount).
        ''' Subject to the <see cref="Type">Type</see> corresponds to:
        ''' <see cref="Goods.OperationPersistenceObject.TotalValue">OperationPersistenceObject.TotalValue</see>.
        ''' or zero.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property TotalValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_TotalValue)
            End Get
        End Property

        ''' <summary>
        ''' Gets a total balance change of the <see cref="Goods.Warehouse.WarehouseAccount">
        ''' warehouse account</see>. 
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.
        ''' Subject to the <see cref="Type">Type</see> corresponds to:
        ''' <see cref="Goods.OperationPersistenceObject.AccountGeneral">OperationPersistenceObject.AccountGeneral</see>.
        ''' or zero.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property AccountGeneral() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AccountGeneral)
            End Get
        End Property

        ''' <summary>
        ''' Gets a total balance change of the <see cref="Goods.GoodsItem.AccountSalesNetCosts">
        ''' sales net costs account</see>. 
        ''' </summary>
        ''' <remarks>Only applicable when the goods are accounted by the
        ''' <see cref="Goods.GoodsAccountingMethod.Periodic">Periodic</see> method.
        ''' A positive number represents debit balance, a negative number represents credit balance.
        ''' Subject to the <see cref="Type">Type</see> corresponds to:
        ''' <see cref="Goods.OperationPersistenceObject.AccountSalesNetCosts">OperationPersistenceObject.AccountSalesNetCosts</see>.
        ''' or zero.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property AccountSalesNetCosts() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AccountSalesNetCosts)
            End Get
        End Property

        ''' <summary>
        ''' Gets a total balance change of the <see cref="Goods.GoodsItem.AccountPurchases">
        ''' purchases account</see>. 
        ''' </summary>
        ''' <remarks>Only applicable when the goods are accounted by the
        ''' <see cref="Goods.GoodsAccountingMethod.Periodic">Periodic</see> method.
        ''' A positive number represents debit balance, a negative number represents credit balance.
        ''' Subject to the <see cref="Type">Type</see> corresponds to:
        ''' <see cref="Goods.OperationPersistenceObject.AccountPurchases">OperationPersistenceObject.AccountPurchases</see>.
        ''' or zero.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property AccountPurchases() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AccountPurchases)
            End Get
        End Property

        ''' <summary>
        ''' Gets a total balance change of the <see cref="Goods.GoodsItem.AccountDiscounts">
        ''' discounts account</see>. 
        ''' </summary>
        ''' <remarks>Only applicable when the goods are accounted by the
        ''' <see cref="Goods.GoodsAccountingMethod.Periodic">Periodic</see> method.
        ''' A positive number represents debit balance, a negative number represents credit balance.
        ''' Subject to the <see cref="Type">Type</see> corresponds to:
        ''' <see cref="Goods.OperationPersistenceObject.AccountDiscounts">OperationPersistenceObject.AccountDiscounts</see>.
        ''' or zero.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property AccountDiscounts() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AccountDiscounts)
            End Get
        End Property

        ''' <summary>
        ''' Gets a total balance change of the <see cref="Goods.GoodsItem.AccountValueReduction">
        ''' value reduction account</see>. 
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.
        ''' Subject to the <see cref="Type">Type</see> corresponds to:
        ''' <see cref="Goods.OperationPersistenceObject.AccountPriceCut">OperationPersistenceObject.AccountPriceCut</see>.
        ''' or zero.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property AccountPriceCut() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AccountPriceCut)
            End Get
        End Property

        ''' <summary>
        ''' Gets a total balance change of the <see cref="AccountOperation">
        ''' operation specific account</see>.
        ''' </summary>
        ''' <remarks>A positive number represents debit balance, a negative number represents credit balance.
        ''' Subject to the <see cref="Type">Type</see> corresponds to:
        ''' <see cref="Goods.OperationPersistenceObject.AccountOperationValue">OperationPersistenceObject.AccountOperationValue</see>.
        ''' or zero.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property AccountOperationValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_AccountOperationValue)
            End Get
        End Property

        ''' <summary>
        ''' Gets an operation specific <see cref="General.Account.ID">account</see>.
        ''' </summary>
        ''' <remarks>Typicaly it is an actual sales net costs account for the goods
        ''' that are accounted by the <see cref="Goods.GoodsAccountingMethod.Persistent">Persistent</see>
        ''' method (because <see cref="Goods.GoodsItem.AccountSalesNetCosts">GoodsItem.AccountSalesNetCosts</see>
        ''' in this case only provides for the default account not actual).
        ''' Subject to the <see cref="Type">Type</see> corresponds to:
        ''' <see cref="Goods.OperationPersistenceObject.AccountOperation">OperationPersistenceObject.AccountOperation</see>.
        ''' or zero.</remarks>
        Public ReadOnly Property AccountOperation() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AccountOperation
            End Get
        End Property

        ''' <summary>
        ''' Gets an <see cref="General.JournalEntry.ID">ID of the journal entry</see>
        ''' that is encapsulated by (or associated with) the operation.
        ''' </summary>
        ''' <remarks>Subject to the <see cref="Type">Type</see> corresponds to:
        ''' <see cref="Goods.OperationPersistenceObject.JournalEntryID">OperationPersistenceObject.JournalEntryID</see>, 
        ''' <see cref="Goods.GoodsOperationAccountChange.JournalEntryID">GoodsOperationAccountChange.JournalEntryID</see>
        ''' or zero.</remarks>
        Public ReadOnly Property JournalEntryID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryID
            End Get
        End Property

        ''' <summary>
        ''' Gets a <see cref="General.JournalEntry.DocNumber">document number of 
        ''' the journal entry</see> that is encapsulated by (or associated with) the operation.
        ''' </summary>
        ''' <remarks>Subject to the <see cref="Type">Type</see> corresponds to:
        ''' <see cref="Goods.OperationPersistenceObject.JournalEntryDocNo">OperationPersistenceObject.JournalEntryDocNo</see>, 
        ''' <see cref="Goods.GoodsOperationAccountChange.DocumentNumber">GoodsOperationAccountChange.DocumentNumber</see>
        ''' or an empty string.</remarks>
        Public ReadOnly Property JournalEntryDocNo() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryDocNo.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a <see cref="General.JournalEntry.[Date]">date of the journal entry</see>
        ''' that is encapsulated by (or associated with) the operation.
        ''' </summary>
        ''' <remarks>Subject to the <see cref="Type">Type</see> corresponds to:
        ''' <see cref="Goods.OperationPersistenceObject.JournalEntryDate">OperationPersistenceObject.JournalEntryDate</see>, 
        ''' <see cref="Goods.GoodsOperationAccountChange.[Date]">GoodsOperationAccountChange.Date</see>
        ''' or Date.MinValue.</remarks>
        Public ReadOnly Property JournalEntryDate() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryDate
            End Get
        End Property

        ''' <summary>
        ''' Gets a <see cref="General.JournalEntry.Content">content of the journal entry</see>
        ''' that is encapsulated by (or associated with) the operation.
        ''' </summary>
        ''' <remarks>Subject to the <see cref="Type">Type</see> corresponds to:
        ''' <see cref="Goods.OperationPersistenceObject.JournalEntryContent">OperationPersistenceObject.JournalEntryContent</see>, 
        ''' <see cref="Goods.GoodsOperationAccountChange.Description">GoodsOperationAccountChange.Description</see>
        ''' or an empty string.</remarks>
        Public ReadOnly Property JournalEntryContent() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryContent.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a <see cref="General.JournalEntry.DocType">localized human readable 
        ''' document type of the journal entry</see> that is encapsulated by 
        ''' (or associated with) the operation.
        ''' </summary>
        ''' <remarks>Subject to the <see cref="Type">Type</see> corresponds to:
        ''' <see cref="Goods.OperationPersistenceObject.JournalEntryTypeHumanReadable">OperationPersistenceObject.JournalEntryTypeHumanReadable</see>, 
        ''' <see cref="DocumentType.GoodsAccountChange">DocumentType.GoodsAccountChange</see>
        ''' or an empty string.</remarks>
        Public ReadOnly Property JournalEntryType() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryType.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a <see cref="ActiveReports.JournalEntryInfo.BookEntries">description 
        ''' of the book entries of the journal entry</see> that is encapsulated by 
        ''' (or associated with) the operation.
        ''' </summary>
        ''' <remarks>Subject to the <see cref="Type">Type</see> corresponds to:
        ''' <see cref="Goods.OperationPersistenceObject.JournalEntryCorrespondence">OperationPersistenceObject.JournalEntryCorrespondence</see>
        ''' or an empty string.</remarks>
        Public ReadOnly Property JournalEntryCorrespondentions() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _JournalEntryCorrespondentions.Trim
            End Get
        End Property



        Protected Overrides Function GetIdValue() As Object
            ' operations original id's could overlap, because they are stored
            ' in 3 different tables.
            Return _Guid
        End Function

        Public Overrides Function ToString() As String
            If Not _ID > 0 Then Return ""
            Return String.Format(My.Resources.ActiveReports_GoodsOperationInfo_ToString, _
                _Date.ToString("yyyy-MM-dd"), _Type, _DocNo, _ID.ToString)
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function GetGoodsOperationInfo(ByVal dr As DataRow) As GoodsOperationInfo
            Return New GoodsOperationInfo(dr)
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

            _ID = CIntSafe(dr.Item(0), 0)
            _WarehouseID = CIntSafe(dr.Item(1), 0)
            _WarehouseName = CStrSafe(dr.Item(2)).Trim
            _WarehouseAccount = CLongSafe(dr.Item(3), 0)
            _Date = CDateSafe(dr.Item(4), Today)
            _Type = ConvertLocalizedName(ConvertDatabaseID(Of Goods.GoodsOperationType) _
                (CIntSafe(dr.Item(5), 1)))
            _ComplexType = ConvertLocalizedName(ConvertDatabaseID(Of Goods.GoodsComplexOperationType) _
                (CIntSafe(dr.Item(6), 1)))
            _DocNo = CStrSafe(dr.Item(7)).Trim
            _Content = CStrSafe(dr.Item(8)).Trim
            _Amount = CDblSafe(dr.Item(9), ROUNDAMOUNTGOODS, 0)
            _AmountInWarehouse = CDblSafe(dr.Item(10), ROUNDAMOUNTGOODS, 0)
            _UnitValue = CDblSafe(dr.Item(11), ROUNDUNITGOODS, 0)
            _TotalValue = CDblSafe(dr.Item(12), 2, 0)
            _AccountGeneral = CDblSafe(dr.Item(13), 2, 0)
            _AccountSalesNetCosts = CDblSafe(dr.Item(14), 2, 0)
            _AccountPurchases = CDblSafe(dr.Item(15), 2, 0)
            _AccountDiscounts = CDblSafe(dr.Item(16), 2, 0)
            _AccountPriceCut = CDblSafe(dr.Item(17), 2, 0)
            _AccountOperationValue = CDblSafe(dr.Item(18), 2, 0)
            _AccountOperation = CLongSafe(dr.Item(19), 0)
            _ComplexOperationID = CIntSafe(dr.Item(20), 0)
            _InsertDate = CTimeStampSafe(dr.Item(21))
            _UpdateDate = CTimeStampSafe(dr.Item(22))
            _JournalEntryID = CIntSafe(dr.Item(23), 0)
            _JournalEntryDocNo = CStrSafe(dr.Item(24)).Trim
            _JournalEntryDate = CDateSafe(dr.Item(25), System.DateTime.MinValue)
            _JournalEntryContent = CStrSafe(dr.Item(26)).Trim
            _JournalEntryType = ConvertLocalizedName(ConvertDatabaseCharID(Of DocumentType) _
                (CStrSafe(dr.Item(27))))
            _JournalEntryCorrespondentions = CStrSafe(dr.Item(28)).Trim

            If Not _ComplexOperationID > 0 Then _ComplexType = ""

        End Sub

#End Region

    End Class

End Namespace