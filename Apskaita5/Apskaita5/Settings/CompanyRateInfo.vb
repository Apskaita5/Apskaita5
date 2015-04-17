Namespace Settings

    <Serializable()> _
    Public Class CompanyRateInfo
        Inherits ReadOnlyBase(Of CompanyRateInfo)

#Region " Business Methods "

        Private _Guid As Guid = Guid.NewGuid
        Private _ID As Integer = 0
        Private _Type As General.DefaultRateType = General.DefaultRateType.Vat
        Private _TypeHumanReadable As String = ""
        Private _Value As Double = 0


        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        Public ReadOnly Property [Type]() As General.DefaultRateType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Type
            End Get
        End Property

        Public ReadOnly Property TypeHumanReadable() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _TypeHumanReadable.Trim
            End Get
        End Property

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
            Return _TypeHumanReadable & " = " & _Value.ToString("##,0.00")
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
            _Type = ConvertEnumDatabaseCode(Of General.DefaultRateType)(CIntSafe(dr.Item(1), 0))
            _TypeHumanReadable = ConvertEnumHumanReadable(_Type)
            _Value = CDblSafe(dr.Item(2), 2, 0)

        End Sub

#End Region

    End Class

End Namespace