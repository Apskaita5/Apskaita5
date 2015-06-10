Imports ApskaitaObjects.General
Namespace Settings

    <Serializable()> _
    Public Class CompanyAccountInfo
        Inherits ReadOnlyBase(Of CompanyAccountInfo)

#Region " Business Methods "

        Private _Guid As Guid = Guid.NewGuid
        Private _ID As Integer = 0
        Private _Type As DefaultAccountType = DefaultAccountType.Bank
        Private _TypeHumanReadable As String = ""
        Private _Value As Long = 0


        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        Public ReadOnly Property [Type]() As DefaultAccountType
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

        Public ReadOnly Property Value() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Value
            End Get
        End Property



        Protected Overrides Function GetIdValue() As Object
            Return _Guid
        End Function

        Public Overrides Function ToString() As String
            Return _TypeHumanReadable & " = " & _Value.ToString
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function GetCompanyAccountInfo(ByVal dr As DataRow) As CompanyAccountInfo
            Return New CompanyAccountInfo(dr)
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
            _Type = EnumValueAttribute.ConvertDatabaseID(Of DefaultAccountType)(CIntSafe(dr.Item(1), 0))
            _TypeHumanReadable = EnumValueAttribute.ConvertLocalizedName(_Type)
            _Value = CIntSafe(dr.Item(2), 0)

        End Sub

#End Region

    End Class

End Namespace