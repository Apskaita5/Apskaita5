Namespace Documents

    ''' <summary>
    ''' Represents a <see cref="VatDeclarationSchema">VatDeclarationSchema</see> XML proxy, 
    ''' used to XML serialize VatDeclarationSchema data.
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public Class VatDeclarationSchemaProxy

        Private _Name As String = ""
        Private _Description As String = ""
        Private _VatRate As Double = 0
        Private _VatRateIsNull As Boolean = False
        Private _IsObsolete As Boolean = False
        Private _TradedType As TradedItemType = TradedItemType.All
        Private _ExternalCode As String = ""
        Private _ExternalCodeInt As String = ""
        Private _VatIsVirtual As Boolean = False
        Private _DeclarationEntries As New List(Of VatDeclarationEntryProxy)


        ''' <summary>
        ''' Value corresponds to <see cref="VatDeclarationSchema.Name">VatDeclarationSchema.Name</see>
        ''' property.
        ''' </summary>
        ''' <remarks></remarks>
        Public Property Name() As String
            Get
                Return _Name.Trim
            End Get
            Set(ByVal value As String)
                If value Is Nothing Then value = ""
                If _Name.Trim <> value.Trim Then
                    _Name = value.Trim
                End If
            End Set
        End Property

        ''' <summary>
        ''' Value corresponds to <see cref="VatDeclarationSchema.Description">VatDeclarationSchema.Description</see>
        ''' property.
        ''' </summary>
        ''' <remarks></remarks>
        Public Property Description() As String
            Get
                Return _Description.Trim
            End Get
            Set(ByVal value As String)
                If value Is Nothing Then value = ""
                If _Description.Trim <> value.Trim Then
                    _Description = value.Trim
                End If
            End Set
        End Property

        ''' <summary>
        ''' Value corresponds to <see cref="VatDeclarationSchema.VatRate">VatDeclarationSchema.VatRate</see>
        ''' property.
        ''' </summary>
        ''' <remarks></remarks>
        Public Property VatRate() As Double
            Get
                Return CRound(_VatRate)
            End Get
            Set(ByVal value As Double)
                If CRound(_VatRate) <> CRound(value) Then
                    _VatRate = CRound(value)
                End If
            End Set
        End Property

        ''' <summary>
        ''' Value corresponds to <see cref="VatDeclarationSchema.VatRateIsNull">VatDeclarationSchema.VatRateIsNull</see>
        ''' property.
        ''' </summary>
        ''' <remarks></remarks>
        Public Property VatRateIsNull() As Boolean
            Get
                Return _VatRateIsNull
            End Get
            Set(ByVal value As Boolean)
                If _VatRateIsNull <> value Then
                    _VatRateIsNull = value
                End If
            End Set
        End Property

        ''' <summary>
        ''' Value corresponds to <see cref="VatDeclarationSchema.VatIsVirtual">VatDeclarationSchema.VatIsVirtual</see>
        ''' property.
        ''' </summary>
        ''' <remarks></remarks>
        Public Property VatIsVirtual() As Boolean
            Get
                Return _VatIsVirtual
            End Get
            Set(ByVal value As Boolean)
                If _VatIsVirtual <> value Then
                    _VatIsVirtual = value
                End If
            End Set
        End Property

        ''' <summary>
        ''' Value corresponds to <see cref="VatDeclarationSchema.IsObsolete">VatDeclarationSchema.IsObsolete</see>
        ''' property.
        ''' </summary>
        ''' <remarks></remarks>
        Public Property IsObsolete() As Boolean
            Get
                Return _IsObsolete
            End Get
            Set(ByVal value As Boolean)
                If _IsObsolete <> value Then
                    _IsObsolete = value
                End If
            End Set
        End Property

        ''' <summary>
        ''' Value corresponds to <see cref="VatDeclarationSchema.TradedType">VatDeclarationSchema.TradedType</see>
        ''' property.
        ''' </summary>
        ''' <remarks></remarks>
        Public Property TradedType() As TradedItemType
            Get
                Return _TradedType
            End Get
            Set(ByVal value As TradedItemType)
                If _TradedType <> value Then
                    _TradedType = value
                End If
            End Set
        End Property

        ''' <summary>
        ''' Value corresponds to <see cref="VatDeclarationSchema.TaxCode">VatDeclarationSchema.TaxCode</see>
        ''' property.
        ''' </summary>
        ''' <remarks></remarks>
        Public Property ExternalCode() As String
            Get
                Return _ExternalCode.Trim
            End Get
            Set(ByVal value As String)
                If value Is Nothing Then value = ""
                If _ExternalCode.Trim <> value.Trim Then
                    _ExternalCode = value.Trim
                End If
            End Set
        End Property

        ''' <summary>
        ''' Value corresponds to <see cref="VatDeclarationSchema.ExternalCode">VatDeclarationSchema.ExternalCode</see>
        ''' property.
        ''' </summary>
        ''' <remarks></remarks>
        Public Property ExternalCodeInt() As String
            Get
                Return _ExternalCodeInt.Trim
            End Get
            Set(ByVal value As String)
                If value Is Nothing Then value = ""
                If _ExternalCodeInt.Trim <> value.Trim Then
                    _ExternalCodeInt = value.Trim
                End If
            End Set
        End Property

        ''' <summary>
        ''' Value corresponds to <see cref="VatDeclarationSchema.DeclarationEntries">VatDeclarationSchema.DeclarationEntries</see>
        ''' property.
        ''' </summary>
        ''' <remarks></remarks>
        Public Property DeclarationEntries() As List(Of VatDeclarationEntryProxy)
            Get
                Return _DeclarationEntries
            End Get
            Set(ByVal value As List(Of VatDeclarationEntryProxy))
                If value Is Nothing Then value = New List(Of VatDeclarationEntryProxy)
                _DeclarationEntries = value
            End Set
        End Property

    End Class

End Namespace
