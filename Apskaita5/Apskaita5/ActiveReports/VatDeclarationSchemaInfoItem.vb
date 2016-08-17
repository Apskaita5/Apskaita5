Imports ApskaitaObjects.Documents

Namespace ActiveReports

    ''' <summary>
    ''' Represents an item (line) in a VAT declaration schemas report, contains information about a
    ''' <see cref="Documents.VatDeclarationSchema">VAT declaration schema</see> used in the company.
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public NotInheritable Class VatDeclarationSchemaInfoItem
        Inherits ReadOnlyBase(Of VatDeclarationSchemaInfoItem)

#Region " Business Methods "

        Private _ID As Integer = 0
        Private _Name As String = ""
        Private _Description As String = ""
        Private _VatRate As Double = 0
        Private _IsObsolete As Boolean = False
        Private _TradedType As TradedItemType = Nothing
        Private _TradedTypeHumanReadable As String = ""
        Private _ExternalCode As String = ""


        ''' <summary>
        ''' Gets an ID of the VAT declaration schema that is assigned by a database (AUTOINCREMENT).
        ''' </summary>
        ''' <remarks>Value is stored in the database field VatDeclarationSchemas.ID.</remarks>
        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' Gets a name of the VAT declaration schema (as used in dropboxes).
        ''' </summary>
        ''' <remarks>Value is stored in the database field VatDeclarationSchemas.Name.</remarks>
        Public ReadOnly Property Name() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Name.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a description of the VAT declaration schema.
        ''' </summary>
        ''' <remarks>Value is stored in the database field VatDeclarationSchemas.Description.</remarks>
        Public ReadOnly Property Description() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Description.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a VAT rate for the declaration schema.
        ''' </summary>
        ''' <remarks>Value is stored in the database field VatDeclarationSchemas.VatRate.</remarks>
        <DoubleField(ValueRequiredLevel.Recommended, False, 2)> _
        Public ReadOnly Property VatRate() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_VatRate)
            End Get
        End Property

        ''' <summary>
        ''' Gets whether the declaration schema is obsolete, no longer in use.
        ''' </summary>
        ''' <remarks>Value is stored in the database field VatDeclarationSchemas.IsObsolete.</remarks>
        Public ReadOnly Property IsObsolete() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsObsolete
            End Get
        End Property

        ''' <summary>
        ''' Gets how the daclaration schema is used in trade operations (sale, purchase, etc.).
        ''' </summary>
        ''' <remarks>Value is stored in the database field VatDeclarationSchemas.TradedType.</remarks>
        Public ReadOnly Property TradedType() As TradedItemType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _TradedType
            End Get
        End Property

        ''' <summary>
        ''' Gets how the daclaration schema is used in trade operations (sale, purchase, etc.)
        ''' as a human readable string.
        ''' </summary>
        ''' <remarks>Value is stored in the database field VatDeclarationSchemas.TradedType.</remarks>
        Public ReadOnly Property TradedTypeHumanReadable() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _TradedTypeHumanReadable.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a code of the VAT declaration schema that is used for integration with external systems.
        ''' </summary>
        ''' <remarks>Value is stored in the database field VatDeclarationSchemas.ExternalCode.</remarks>
        <StringField(ValueRequiredLevel.Optional, 255)> _
        Public ReadOnly Property ExternalCode() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ExternalCode.Trim
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

        Friend Shared Function GetVatDeclarationSchemaInfoItem(ByVal dr As DataRow) As VatDeclarationSchemaInfoItem
            Return New VatDeclarationSchemaInfoItem(dr)
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
            _Name = CStrSafe(dr.Item(1)).Trim
            _Description = CStrSafe(dr.Item(2)).Trim
            _VatRate = CDblSafe(dr.Item(3), 2, 0)
            _IsObsolete = ConvertDbBoolean(CIntSafe(dr.Item(4), 0))
            _TradedType = ConvertDatabaseID(Of TradedItemType)(CIntSafe(dr.Item(5), 0))
            _TradedTypeHumanReadable = ConvertLocalizedName(_TradedType)
            _ExternalCode = CStrSafe(dr.Item(6)).Trim

        End Sub

#End Region

    End Class

End Namespace