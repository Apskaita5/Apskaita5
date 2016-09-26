Imports ApskaitaObjects.Attributes
Imports ApskaitaObjects.Documents

Namespace HelperLists

    ''' <summary>
    ''' Represents a <see cref="Documents.Service">service's</see> value object.
    ''' </summary>
    ''' <remarks>Values are stored in the database table paslaugos.</remarks>
    <Serializable()> _
    Public NotInheritable Class ServiceInfo
        Inherits ReadOnlyBase(Of ServiceInfo)
        Implements IComparable, IValueObject

#Region " Business Methods "

        Private _ID As Integer = 0
        Private _Type As Documents.TradedItemType = Documents.TradedItemType.All
        Private _TypeHumanReadable As String = Utilities.ConvertLocalizedName(Documents.TradedItemType.All)
        Private _NameShort As String = ""
        Private _Amount As Double = 0
        Private _AccountSales As Long = 0
        Private _RateVatSales As Double = 0
        Private _DeclarationSchemaSales As VatDeclarationSchemaInfo = Nothing
        Private _AccountVatSales As Long = 0
        Private _AccountPurchase As Long = 0
        Private _RateVatPurchase As Double = 0
        Private _DeclarationSchemaPurchase As VatDeclarationSchemaInfo = Nothing
        Private _AccountVatPurchase As Long = 0
        Private _IsObsolete As Boolean = False
        Private _NameInvoice As String = ""
        Private _MeasureUnit As String = ""


        ''' <summary>
        ''' Gets whether an object is a place holder (does not represent a real service).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property IsEmpty() As Boolean _
            Implements IValueObject.IsEmpty
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return Not _ID > 0
            End Get
        End Property

        ''' <summary>
        ''' Gets an ID of the service that is assigned by a database (AUTOINCREMENT).
        ''' </summary>
        ''' <remarks>Value is stored in the database field paslaugos.ID.</remarks>
        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' Gets how the service is used in trade operations (sale, purchase, etc.).
        ''' </summary>
        ''' <remarks>Value is stored in the database field paslaugos.Tip.</remarks>
        Public ReadOnly Property [Type]() As Documents.TradedItemType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Type
            End Get
        End Property

        ''' <summary>
        ''' Gets how the service is used in trade operations (sale, purchase, etc.) 
        ''' as localized human readable string.
        ''' </summary>
        ''' <remarks>Value is stored in the database field paslaugos.Tip.</remarks>
        Public ReadOnly Property TypeHumanReadable() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _TypeHumanReadable.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a short name of the service (as used in dropboxes).
        ''' </summary>
        ''' <remarks>Value is stored in the database field paslaugos.TrPav.</remarks>
        Public ReadOnly Property NameShort() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _NameShort.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a default amount of the service.
        ''' </summary>
        ''' <remarks>Value is stored in the database field paslaugos.Amount.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, ROUNDAMOUNTINVOICEMADE)> _
        Public ReadOnly Property Amount() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_Amount, ROUNDAMOUNTINVOICEMADE)
            End Get
        End Property

        ''' <summary>
        ''' Gets a default <see cref="General.Account.ID">sales account</see> for the service.
        ''' </summary>
        ''' <remarks>Value is stored in the database field paslaugos.S_Sask.</remarks>
        Public ReadOnly Property AccountSales() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AccountSales
            End Get
        End Property

        ''' <summary>
        ''' Gets a default VAT rate for the service beeing sold.
        ''' </summary>
        ''' <remarks>Value is stored in the database field paslaugos.PVM.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property RateVatSales() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_RateVatSales)
            End Get
        End Property

        ''' <summary>
        ''' Gets the applicable VAT declaration schema for the services sold.
        ''' </summary>
        ''' <remarks>Value is stored in the database table paslaugos.DeclarationSchemaIDSales.</remarks>
        <VatDeclarationSchemaField(ValueRequiredLevel.Optional, TradedItemType.Sales)> _
        Public ReadOnly Property DeclarationSchemaSales() As VatDeclarationSchemaInfo
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DeclarationSchemaSales
            End Get
        End Property

        ''' <summary>
        ''' Gets a default <see cref="General.Account.ID">sales VAT account</see> for the service.
        ''' </summary>
        ''' <remarks>Value is stored in the database field paslaugos.P_Sask.</remarks>
        Public ReadOnly Property AccountVatSales() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AccountVatSales
            End Get
        End Property

        ''' <summary>
        ''' Gets a default <see cref="General.Account.ID">purchase (costs) account</see> for the service.
        ''' </summary>
        ''' <remarks>Value is stored in the database field paslaugos.AccountPurchase.</remarks>
        Public ReadOnly Property AccountPurchase() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AccountPurchase
            End Get
        End Property

        ''' <summary>
        ''' Gets a default VAT rate for the service beeing purchased.
        ''' </summary>
        ''' <remarks>Value is stored in the database field paslaugos.RateVatPurchase.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property RateVatPurchase() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_RateVatPurchase)
            End Get
        End Property

        ''' <summary>
        ''' Gets the applicable VAT declaration schema for the services bought.
        ''' </summary>
        ''' <remarks>Value is stored in the database table paslaugos.DeclarationSchemaIDPurchase.</remarks>
        <VatDeclarationSchemaField(ValueRequiredLevel.Optional, TradedItemType.All)> _
        Public ReadOnly Property DeclarationSchemaPurchase() As VatDeclarationSchemaInfo
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DeclarationSchemaPurchase
            End Get
        End Property

        ''' <summary>
        ''' Gets a default <see cref="General.Account.ID">purchase VAT account</see> for the service.
        ''' </summary>
        ''' <remarks>Value is stored in the database field paslaugos.AccountVatPurchase.</remarks>
        Public ReadOnly Property AccountVatPurchase() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AccountVatPurchase
            End Get
        End Property

        ''' <summary>
        ''' Gets whether the service is obsolete (no longer in use).
        ''' </summary>
        ''' <remarks>Value is stored in the database field paslaugos.Obsol.</remarks>
        Public ReadOnly Property IsObsolete() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsObsolete
            End Get
        End Property

        ''' <summary>
        ''' Gets a service name in the base language.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property NameInvoice() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _NameInvoice.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a service measure unit in the base language.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property MeasureUnit() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _MeasureUnit.Trim
            End Get
        End Property


        Public Shared Operator =(ByVal a As ServiceInfo, ByVal b As ServiceInfo) As Boolean

            Dim aId, bId As Integer
            If a Is Nothing OrElse a.IsEmpty Then
                aId = 0
            Else
                aId = a.ID
            End If
            If b Is Nothing OrElse b.IsEmpty Then
                bId = 0
            Else
                bId = b.ID
            End If

            Return aId = bId

        End Operator

        Public Shared Operator <>(ByVal a As ServiceInfo, ByVal b As ServiceInfo) As Boolean
            Return Not a = b
        End Operator

        Public Shared Operator >(ByVal a As ServiceInfo, ByVal b As ServiceInfo) As Boolean

            Dim aToString, bToString As String
            If a Is Nothing OrElse a.IsEmpty Then
                aToString = ""
            Else
                aToString = a.ToString
            End If
            If b Is Nothing OrElse b.IsEmpty Then
                bToString = ""
            Else
                bToString = b.ToString
            End If

            Return aToString > bToString

        End Operator

        Public Shared Operator <(ByVal a As ServiceInfo, ByVal b As ServiceInfo) As Boolean

            Dim aToString, bToString As String
            If a Is Nothing OrElse a.IsEmpty Then
                aToString = ""
            Else
                aToString = a.ToString
            End If
            If b Is Nothing OrElse b.IsEmpty Then
                bToString = ""
            Else
                bToString = b.ToString
            End If

            Return aToString < bToString

        End Operator

        Public Function CompareTo(ByVal obj As Object) As Integer Implements System.IComparable.CompareTo
            Dim tmp As ServiceInfo = TryCast(obj, ServiceInfo)
            If Me = tmp Then Return 0
            If Me > tmp Then Return 1
            Return -1
        End Function


        Friend Function GetValueObjectIdString() As String
            If Me.IsEmpty Then Return ""
            Return _ID.ToString(Globalization.CultureInfo.InvariantCulture)
        End Function


        Protected Overrides Function GetIdValue() As Object
            Return _ID
        End Function

        Public Overrides Function ToString() As String
            Return _NameShort
        End Function

#End Region

#Region " Factory Methods "

        Private Shared _Empty As ServiceInfo = Nothing

        ''' <summary>
        ''' Gets an empty ServiceInfo (placeholder).
        ''' </summary>
        Public Shared Function Empty() As ServiceInfo
            If _Empty Is Nothing Then
                _Empty = New ServiceInfo
            End If
            Return _Empty
        End Function

        Friend Shared Function GetServiceInfo(ByVal dr As DataRow) As ServiceInfo
            Return New ServiceInfo(dr)
        End Function

        Friend Shared Function GetServiceInfo(ByVal serviceID As Integer) As ServiceInfo
            Return New ServiceInfo(serviceID)
        End Function


        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal dr As DataRow)
            Fetch(dr)
        End Sub

        Private Sub New(ByVal serviceID As Integer)
            Fetch(serviceID)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal dr As DataRow)

            _ID = CIntSafe(dr.Item(0), 0)
            _Type = Utilities.ConvertDatabaseID(Of Documents.TradedItemType)(CIntSafe(dr.Item(1), 0))
            _TypeHumanReadable = Utilities.ConvertLocalizedName(_Type)
            _NameShort = CStrSafe(dr.Item(2)).Trim
            _Amount = CDblSafe(dr.Item(3), 4, 0)
            _RateVatSales = CDblSafe(dr.Item(4), 2, 0)
            _RateVatPurchase = CDblSafe(dr.Item(5), 2, 0)
            _IsObsolete = ConvertDbBoolean(CIntSafe(dr.Item(6), 0))
            _AccountSales = CLongSafe(dr.Item(7), 0)
            _AccountVatSales = CLongSafe(dr.Item(8), 0)
            _AccountPurchase = CLongSafe(dr.Item(9), 0)
            _AccountVatPurchase = CLongSafe(dr.Item(10), 0)
            _NameInvoice = CStrSafe(dr.Item(11)).Trim
            _MeasureUnit = CStrSafe(dr.Item(12)).Trim
            _DeclarationSchemaSales = VatDeclarationSchemaInfo.GetVatDeclarationSchemaInfo(dr, 14)
            _DeclarationSchemaPurchase = VatDeclarationSchemaInfo.GetVatDeclarationSchemaInfo(dr, 21)

        End Sub

        Private Sub Fetch(ByVal serviceID As Integer)

            Dim myComm As New SQLCommand("FetchServiceInfo")
            myComm.AddParam("?SD", serviceID)
            myComm.AddParam("?LN", LanguageCodeLith)

            Using myData As DataTable = myComm.Fetch
                If myData.Rows.Count < 1 Then Exit Sub
                Fetch(myData.Rows(0))
            End Using

        End Sub

#End Region

    End Class

End Namespace