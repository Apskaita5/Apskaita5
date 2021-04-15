Imports ApskaitaObjects.General
Namespace Settings

    ''' <summary>
    ''' Represents a <see cref="General.CompanyAccount">company's default account</see> value object.
    ''' </summary>
    ''' <remarks>Values are stored in the database table companyaccounts.</remarks>
    <Serializable()> _
    Public NotInheritable Class CompanyAccountInfo
        Inherits ReadOnlyBase(Of CompanyAccountInfo)

#Region " Business Methods "

        Private ReadOnly _Guid As Guid = Guid.NewGuid
        Private _ID As Integer = 0
        Private _Type As DefaultAccountType = DefaultAccountType.Bank
        Private _TypeHumanReadable As String = ""
        Private _Value As Long = 0


        ''' <summary>
        ''' Gets an ID of the company account (assigned automaticaly by DB AUTOINCREMENT).
        ''' </summary>
        ''' <remarks>Value is stored in the database field companyaccounts.ID.</remarks>
        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' Gets a <see cref="General.DefaultAccountType">type</see> of the company account.
        ''' </summary>
        ''' <remarks>Value is stored in the database field companyaccounts.Code.</remarks>
        Public ReadOnly Property [Type]() As DefaultAccountType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Type
            End Get
        End Property

        ''' <summary>
        ''' Gets a human readable <see cref="General.DefaultAccountType">type</see> of the company account.
        ''' </summary>
        ''' <remarks>Value is stored in the database field companyaccounts.Code.</remarks>
        Public ReadOnly Property TypeHumanReadable() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _TypeHumanReadable.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets the <see cref="General.Account.ID">account</see>.
        ''' </summary>
        ''' <remarks>Value is stored in the database field companyaccounts.AccountValue.</remarks>
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
            Return String.Format("{0} = {1}", _TypeHumanReadable, _Value.ToString)
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
            _Type = Utilities.ConvertDatabaseID(Of DefaultAccountType)(CIntSafe(dr.Item(1), 0))
            _TypeHumanReadable = Utilities.ConvertLocalizedName(_Type)
            _Value = CLongSafe(dr.Item(2), 0)

        End Sub

#End Region

    End Class

End Namespace
