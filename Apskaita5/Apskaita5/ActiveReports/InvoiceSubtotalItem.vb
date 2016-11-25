Namespace ActiveReports

    ''' <summary>
    ''' Represents report information about an invoice amount and (VAT) tax
    ''' for a particular invoice and a particular tax code.
    ''' </summary>
    ''' <remarks>Should only be used as a child of <see cref="InvoiceSubtotalItemList">InvoiceSubtotalItemList</see>.</remarks>
    <Serializable()> _
    Public NotInheritable Class InvoiceSubtotalItem
        Inherits ReadOnlyBase(Of InvoiceSubtotalItem)

#Region " Business Methods "

        Private _TaxCode As String = ""
        Private _TaxableValue As Double = 0
        Private _TaxPercentage As Double = 0
        Private _TaxAmount As Double = 0
        Private _VatIsVirtual As Boolean = False
        Private _VatRateIsNull As Boolean = False


        ''' <summary>
        ''' A (VAT) tax code, see <see cref="Documents.VatDeclarationSchema.ExternalCode">VatDeclarationSchema.ExternalCode</see>.
        ''' </summary>
        ''' <remarks></remarks>
        <StringField(ValueRequiredLevel.Mandatory, 50)> _
        Public ReadOnly Property TaxCode() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _TaxCode.Trim
            End Get
        End Property

        ''' <summary>
        ''' An invoice amount that is (VAT) taxable under the specific <see cref="TaxCode">tax code</see>.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Mandatory, True, 2)> _
        Public ReadOnly Property TaxableValue() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_TaxableValue)
            End Get
        End Property

        ''' <summary>
        ''' A (VAT) tax rate under the specific <see cref="TaxCode">tax code</see>.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Mandatory, True, 2)> _
        Public ReadOnly Property TaxPercentage() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_TaxPercentage)
            End Get
        End Property

        ''' <summary>
        ''' A (VAT) tax amount under the specific <see cref="TaxCode">tax code</see>.
        ''' </summary>
        ''' <remarks></remarks>
        <DoubleField(ValueRequiredLevel.Mandatory, True, 2)> _
        Public ReadOnly Property TaxAmount() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_TaxAmount)
            End Get
        End Property

        ''' <summary>
        ''' Whether the (VAT) tax is virtual (indirect).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property VatIsVirtual() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _VatIsVirtual
            End Get
        End Property

        ''' <summary>
        ''' Gets whether the VAT rate is null, i.e. should not be displayed in tax reports.
        ''' </summary>
        ''' <remarks>Value is stored in the database field VatDeclarationSchemas.VatRateIsNull.</remarks>
        Public ReadOnly Property VatRateIsNull() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _VatRateIsNull
            End Get
        End Property


        Protected Overrides Function GetIdValue() As Object
            Return _TaxCode
        End Function

        Public Overrides Function ToString() As String
            Return String.Format(My.Resources.ActiveReports_InvoiceSubtotalItem_ToString, _
                _TaxCode, DblParser(_TaxPercentage, 2), DblParser(_TaxableValue, 2), _
                DblParser(_TaxAmount, 2))
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function GetInvoiceSubtotalItem(ByVal dr As DataRow) As InvoiceSubtotalItem
            Return New InvoiceSubtotalItem(dr)
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

            _TaxCode = CStrSafe(dr.Item(1)).Trim
            _TaxPercentage = CDblSafe(dr.Item(2), 2, 0)
            _TaxableValue = CDblSafe(dr.Item(3), 2, 0)
            _TaxAmount = CDblSafe(dr.Item(4), 2, 0)
            _VatIsVirtual = ConvertDbBoolean(CIntSafe(dr.Item(5), 0))
            _VatRateIsNull = ConvertDbBoolean(CIntSafe(dr.Item(6), 0))

        End Sub

#End Region

    End Class

End Namespace