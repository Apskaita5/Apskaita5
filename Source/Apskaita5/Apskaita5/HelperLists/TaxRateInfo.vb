Imports ApskaitaObjects.Settings.XmlProxies
Imports ApskaitaObjects.Settings

Namespace HelperLists

    ''' <summary>
    ''' Represents a <see cref="ApskaitaObjects.Settings.TaxRate">Settings.TaxRate</see>
    ''' value object, i.e. a rate of some tax.
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public NotInheritable Class TaxRateInfo
        Inherits ReadOnlyBase(Of TaxRateInfo)

#Region " Business Methods "

        Private ReadOnly _Guid As Guid = Guid.NewGuid()
        Private _Type As TaxRateType = TaxRateType.GPM
        Private _Rate As Double = 0
        Private _IsObsolete As Boolean = False


        ''' <summary>
        ''' Gets a type of the tax.
        ''' </summary>
        ''' <value></value>
        Public ReadOnly Property [Type]() As TaxRateType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Type
            End Get
        End Property

        ''' <summary>
        ''' Gets a tax rate.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Rate() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_Rate)
            End Get
        End Property

        ''' <summary>
        ''' Whether the tax rate is obsolete, not longer in use.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property IsObsolete() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsObsolete
            End Get
        End Property


        Friend Shared Function GetValueObjectIdString(ByVal value As Double, _
            ByVal valueType As TaxRateType) As String

            Return String.Format("{0}:={1}", valueType.ToString(), _
                value.ToString(Globalization.CultureInfo.InvariantCulture))

        End Function

        Friend Function GetValueObjectIdString() As String
            Return GetValueObjectIdString(_Rate, _Type)
        End Function


        Protected Overrides Function GetIdValue() As Object
            Return _Guid
        End Function

        Public Overrides Function ToString() As String
            Return String.Format(My.Resources.Settings_TaxRate_ToString, _
                Utilities.ConvertLocalizedName(_Type), DblParser(_Rate, 2))
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function GetTaxRateInfo(ByVal proxy As TaxRateProxy) As TaxRateInfo
            Return New TaxRateInfo(proxy)
        End Function

        Friend Shared Function GetTaxRateInfo(ByVal dr As DataRow) As TaxRateInfo
            Return New TaxRateInfo(dr)
        End Function


        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal proxy As TaxRateProxy)
            Fetch(proxy)
        End Sub

        Private Sub New(ByVal dr As DataRow)
            Fetch(dr)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal proxy As TaxRateProxy)
            _Type = proxy.TaxType
            _Rate = proxy.TaxRate
            _IsObsolete = proxy.IsObsolete
        End Sub

        Private Sub Fetch(ByVal dr As DataRow)
            _Type = Utilities.ConvertDatabaseCharID(Of TaxRateType)(CStrSafe(dr.Item(0)))
            _Rate = CDblSafe(dr.Item(1), 2, 0)
            _IsObsolete = True
        End Sub

#End Region

    End Class

End Namespace
