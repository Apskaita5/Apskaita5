Namespace Settings

    ''' <summary>
    ''' Represents a <see cref="General.CompanyAccount">company's account</see> value object.
    ''' </summary>
    ''' <remarks>Should only be used as a child of <see cref="CompanyRateInfoList">CompanyRateInfoList</see>.
    ''' Values are stored in the database table companyrates.</remarks>
    <Serializable()> _
    Public NotInheritable Class CompanyRateInfo
        Inherits ReadOnlyBase(Of CompanyRateInfo)

#Region " Business Methods "

        Private ReadOnly _Guid As Guid = Guid.NewGuid
        Private _ID As Integer = 0
        Private _Type As General.DefaultRateType = General.DefaultRateType.Vat
        Private _TypeHumanReadable As String = ""
        Private _Value As Double = 0

        ''' <summary>
        ''' Gets an ID of the company rate (assigned automaticaly by DB AUTOINCREMENT).
        ''' </summary>
        ''' <remarks>Value is stored in the database field companyrates.ID.</remarks>
        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' Gets a rate type.
        ''' </summary>
        ''' <remarks>Value is stored in the database field companyrates.Code.</remarks>
        Public ReadOnly Property [Type]() As General.DefaultRateType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Type
            End Get
        End Property

        ''' <summary>
        ''' Gets a human readable (localized) description of rate type.
        ''' </summary>
        ''' <remarks>Value is stored in the database field companyrates.Code.</remarks>
        Public ReadOnly Property TypeHumanReadable() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _TypeHumanReadable.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets the rate value.
        ''' </summary>
        ''' <remarks>Value is stored in the database field companyrates.RateValue.</remarks>
        Public ReadOnly Property Value() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_Value)
            End Get
        End Property


        Protected Overrides Function GetIdValue() As Object
            Return _Guid
        End Function

        Public Overrides Function ToString() As String
            Return String.Format("{0} = {1}", _TypeHumanReadable, DblParser(_Value))
        End Function

#End Region

#Region " Authorization Rules "

        Protected Overrides Sub AddAuthorizationRules()

        End Sub

#End Region

#Region " Factory Methods "

        Friend Shared Function GetCompanyRateInfo(ByVal dr As DataRow) As CompanyRateInfo
            Return New CompanyRateInfo(dr)
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
            _Type = Utilities.ConvertDatabaseID(Of General.DefaultRateType)(CIntSafe(dr.Item(1), 0))
            _TypeHumanReadable = Utilities.ConvertLocalizedName(_Type)
            _Value = CDblSafe(dr.Item(2), 2, 0)

        End Sub

#End Region

    End Class

End Namespace