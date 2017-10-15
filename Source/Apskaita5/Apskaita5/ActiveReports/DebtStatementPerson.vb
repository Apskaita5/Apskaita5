Namespace ActiveReports

    ''' <summary>
    ''' Represents a person data for a <see cref="DebtStatementItem">DebtStatementItemList report line</see>.
    ''' </summary>
    ''' <remarks>Should only be used as a child of <see cref="DebtStatementItem">DebtStatementItem</see>.</remarks>
    <Serializable()> _
    Public Class DebtStatementPerson
        Inherits ReadOnlyBase(Of DebtStatementPerson)

#Region " Business Methods "

        Private ReadOnly _Guid As Guid = Guid.NewGuid()
        Private _PersonId As Integer = 0
        Private _PersonName As String = ""
        Private _PersonCode As String = ""
        Private _PersonAddress As String = ""
        Private _PersonVatCode As String = ""
        Private _PersonEmail As String = ""


        ''' <summary>
        ''' Gets an ID of the person.
        ''' </summary>
        ''' <remarks>Corresponds to the <see cref="General.Person.ID">Person.ID</see> property.</remarks>
        Public ReadOnly Property PersonId() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _PersonId
            End Get
        End Property

        ''' <summary>
        ''' Gets a name of the person.
        ''' </summary>
        ''' <remarks>Corresponds to the <see cref="General.Person.Name">Person.Name</see> property.</remarks>
        Public ReadOnly Property PersonName() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _PersonName.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a company/personal code of the person.
        ''' </summary>
        ''' <remarks>Corresponds to the <see cref="General.Person.Code">Person.Code</see> property.</remarks>
        Public ReadOnly Property PersonCode() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _PersonCode.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets an adress of the person.
        ''' </summary>
        ''' <remarks>Corresponds to the <see cref="General.Person.Address">Person.Address</see> property.</remarks>
        Public ReadOnly Property PersonAddress() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _PersonAddress.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a VAT code of the person.
        ''' </summary>
        ''' <remarks>Corresponds to the <see cref="General.Person.CodeVAT">Person.CodeVAT</see> property.</remarks>
        Public ReadOnly Property PersonVatCode() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _PersonVatCode.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets an email adress of the person.
        ''' </summary>
        ''' <remarks>Corresponds to the <see cref="General.Person.Email">Person.Email</see> property.</remarks>
        Public ReadOnly Property PersonEmail() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _PersonEmail.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets the description of the person (could be used for grouping).
        ''' </summary>
        ''' <remarks>The format depends on the user culture.</remarks>
        Public ReadOnly Property PersonDescription() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return String.Format(My.Resources.ActiveReports_DebtStatementPerson_PersonDescriptionFormat, _
                    _PersonName, _PersonCode, _PersonEmail)
            End Get
        End Property


        Protected Overrides Function GetIdValue() As Object
            Return _Guid
        End Function

        Public Overrides Function ToString() As String
            Return String.Format(My.Resources.ActiveReports_DebtStatementPerson_ToString, _
                _PersonName, _PersonCode)
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function GetDebtStatementPerson(ByVal dr As DataRow) As DebtStatementPerson
            Return New DebtStatementPerson(dr)
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

            _PersonId = CIntSafe(dr.Item(0), 0)
            _PersonName = CStrSafe(dr.Item(1)).Trim
            _PersonCode = CStrSafe(dr.Item(2)).Trim
            _PersonAddress = CStrSafe(dr.Item(3)).Trim
            _PersonVatCode = CStrSafe(dr.Item(4)).Trim
            _PersonEmail = CStrSafe(dr.Item(5)).Trim

        End Sub

#End Region


    End Class

End Namespace