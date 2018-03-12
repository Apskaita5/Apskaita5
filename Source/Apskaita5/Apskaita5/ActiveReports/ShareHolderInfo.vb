Namespace ActiveReports

    ''' <summary>
    ''' Represents an item of <see cref="ShareHolderInfoList">ShareHolderInfoList</see> report.
    ''' Contains information about a company's share holder.
    ''' </summary>
    ''' <remarks>Should only be used as a child of <see cref="ShareHolderInfoList">ShareHolderInfoList</see>.</remarks>
    <Serializable()>
    Public NotInheritable Class ShareHolderInfo
        Inherits ReadOnlyBase(Of ShareHolderInfo)

#Region " Business Methods "

        Private _Guid As Guid = Guid.NewGuid
        Private _ID As Integer = 0
        Private _Name As String = ""
        Private _Code As String = ""
        Private _StateCode As String = ""
        Private _Address As String = ""
        Private _Email As String = ""
        Private _Class As String = ""
        Private _Description As String = ""
        Private _ValuePerUnit As Double = 0
        Private _Amount As Double = 0
        Private _ValueTotal As Double = 0


        ''' <summary>
        ''' Gets an ID of the person (shareholder).
        ''' </summary>
        ''' <remarks>Corresponds to the <see cref="General.Person.ID">Person.ID</see> property.
        ''' Equals 0 for the company itself.</remarks>
        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' Gets a name of the person (shareholder).
        ''' </summary>
        ''' <remarks>Corresponds to the <see cref="General.Person.Name">Person.Name</see> property.</remarks>
        Public ReadOnly Property Name() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _Name.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a (registration) code of the person (shareholder).
        ''' </summary>
        ''' <remarks>Corresponds to the <see cref="General.Person.Code">Person.Code</see> property.</remarks>
        Public ReadOnly Property Code() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _Code.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets an ISO 3166-1 alpha 2 code of the residence country of the person (shareholder).
        ''' </summary>
        ''' <remarks>Corresponds to the <see cref="General.Person.StateCode">Person.StateCode</see> property.</remarks>
        Public ReadOnly Property StateCode() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _StateCode.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets an address of the person (shareholder).
        ''' </summary>
        ''' <remarks>Corresponds to the <see cref="General.Person.Address">Person.Address</see> property.</remarks>
        Public ReadOnly Property Address() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _Address.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets an email of the person (shareholder).
        ''' </summary>
        ''' <remarks>Corresponds to the <see cref="General.Person.Email">Person.Email</see> property.</remarks>
        Public ReadOnly Property Email() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _Email.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a name (title) of the shares class.
        ''' </summary>
        ''' <remarks>Corresponds to the <see cref="General.SharesClass.Name">SharesClass.Name</see> property.</remarks>
        Public ReadOnly Property [Class]() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _Class.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a description of the shares class.
        ''' </summary>
        ''' <remarks>Corresponds to the <see cref="General.SharesClass.Description">SharesClass.Description</see> property.</remarks>
        Public ReadOnly Property Description() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _Description.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a value per unit of the shares class.
        ''' </summary>
        ''' <remarks>Corresponds to the <see cref="General.SharesClass.ValuePerUnit">SharesClass.ValuePerUnit</see> property.</remarks>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)>
        Public ReadOnly Property ValuePerUnit() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return CRound(_ValuePerUnit)
            End Get
        End Property

        ''' <summary>
        ''' Gets an amount of the shares owned by the shareholder.
        ''' </summary>
        ''' <returns>Aggregated value as of the report date.</returns>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)>
        Public ReadOnly Property Amount() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return CRound(_Amount)
            End Get
        End Property

        ''' <summary>
        ''' Gets a total value of the shares owned by the shareholder.
        ''' </summary>
        ''' <returns>Equals <see cref="ValuePerUnit">ValuePerUnit</see> * <see cref="Amount">Amount</see>.</returns>
        <DoubleField(ValueRequiredLevel.Optional, True, 2)>
        Public ReadOnly Property ValueTotal() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return CRound(_ValueTotal)
            End Get
        End Property



        Protected Overrides Function GetIdValue() As Object
            Return _Guid
        End Function

        Public Overrides Function ToString() As String
            Return String.Format(My.Resources.ActiveReports_ShareHolderInfo_ToString,
                _Name, _Code, _Class, DblParser(_ValuePerUnit), DblParser(_Amount))
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function GetShareHolderInfo(ByVal dr As DataRow) As ShareHolderInfo
            Return New ShareHolderInfo(dr)
        End Function

        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(dr As DataRow)
            Fetch(dr)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal dr As DataRow)

            _ID = CIntSafe(dr.Item(0), 0)
            _Name = CStrSafe(dr.Item(1)).Trim
            _Code = CStrSafe(dr.Item(2)).Trim
            _StateCode = CStrSafe(dr.Item(3)).Trim
            _Address = CStrSafe(dr.Item(4)).Trim
            _Email = CStrSafe(dr.Item(5)).Trim
            _Class = CStrSafe(dr.Item(6)).Trim
            _Description = CStrSafe(dr.Item(7)).Trim
            _ValuePerUnit = CDblSafe(dr.Item(8), 2, 0)
            _Amount = CDblSafe(dr.Item(9), 2, 0)
            _ValueTotal = CRound(_Amount * _ValuePerUnit)

        End Sub

#End Region

    End Class

End Namespace