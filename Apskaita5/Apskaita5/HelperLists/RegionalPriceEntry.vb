Imports ApskaitaObjects.Documents

Namespace HelperLists

    ''' <summary>
    ''' Represents a value object containing a price info for a particular regionalized object 
    ''' for a particular currency.
    ''' </summary>
    ''' <remarks>Should only be used as a child of <see cref="RegionalPriceEntryList">RegionalPriceEntryList</see>.
    ''' Used with <see cref="IRegionalDataObject">localized objects</see> in order to provide localization in runtime.
    ''' Values are stored in the database table regionalprices.</remarks>
    <Serializable()> _
    Public Class RegionalPriceEntry
        Inherits ReadOnlyBase(Of RegionalPriceEntry)

#Region " Business Methods "

        Private ReadOnly _Guid As Guid = Guid.NewGuid()
        Private _ObjectType As RegionalizedObjectType
        Private _ObjectID As Integer = 0
        Private _CurrencyCode As String = ""
        Private _ValuePerUnitSales As Double = 0
        Private _ValuePerUnitPurchases As Double = 0


        ''' <summary>
        ''' Type of the regionalized object for which the prices are provided.
        ''' </summary>
        ''' <remarks>Value is stored in the database field regionalprices.ParentType.</remarks>
        Public ReadOnly Property ObjectType() As RegionalizedObjectType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ObjectType
            End Get
        End Property

        ''' <summary>
        ''' ID of the object for which the prices are provided.
        ''' </summary>
        ''' <remarks>Value is stored in the database field regionalprices.ParentID.</remarks>
        Public ReadOnly Property ObjectID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ObjectID
            End Get
        End Property

        ''' <summary>
        ''' Currency of the prices.
        ''' </summary>
        ''' <remarks>Value is stored in the database field regionalprices.CurrencyCode.</remarks>
        Public ReadOnly Property CurrencyCode() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _CurrencyCode.Trim
            End Get
        End Property

        ''' <summary>
        ''' Price for sale.
        ''' </summary>
        ''' <remarks>Value is stored in the database field regionalprices.ValuePerUnitSales.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property ValuePerUnitSales() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ValuePerUnitSales, ROUNDUNITINVOICEMADE)
            End Get
        End Property

        ''' <summary>
        ''' Price for purchase.
        ''' </summary>
        ''' <remarks>Value is stored in the database field regionalprices.ValuePerUnitPurchases.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)> _
        Public ReadOnly Property ValuePerUnitPurchases() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_ValuePerUnitPurchases, ROUNDUNITINVOICERECEIVED)
            End Get
        End Property



        Protected Overrides Function GetIdValue() As Object
            Return _Guid
        End Function

        Public Overrides Function ToString() As String
            Return String.Format("{0}: {1} - {2}", _CurrencyCode, _
                DblParser(_ValuePerUnitSales, ROUNDUNITINVOICEMADE), _
                DblParser(_ValuePerUnitPurchases, ROUNDUNITINVOICERECEIVED))
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets a RegionalPriceEntry by a database query.
        ''' </summary>
        ''' <param name="dr">Database query result.</param>
        ''' <remarks></remarks>
        Friend Shared Function GetRegionalPriceEntry(ByVal dr As DataRow) As RegionalPriceEntry
            Return New RegionalPriceEntry(dr)
        End Function

        ''' <summary>
        ''' Gets an empty (default) RegionalPriceEntry.
        ''' </summary>
        ''' <remarks></remarks>
        Friend Shared Function NewRegionalPriceEntry() As RegionalPriceEntry
            Return New RegionalPriceEntry()
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

            _ObjectID = CIntSafe(dr.Item(0), 0)
            _ObjectType = EnumValueAttribute.ConvertDatabaseID(Of RegionalizedObjectType) _
                (CIntSafe(dr.Item(1), 0))
            _CurrencyCode = CStrSafe(dr.Item(2)).Trim
            _ValuePerUnitSales = CDblSafe(dr.Item(3), ROUNDUNITINVOICEMADE, 0)
            _ValuePerUnitPurchases = CDblSafe(dr.Item(4), ROUNDUNITINVOICERECEIVED, 0)

        End Sub

#End Region

    End Class

End Namespace