<Serializable()> _
Friend Class BookEntryInternal
    Inherits BusinessBase(Of BookEntryInternal)

#Region " Business Methods "

    Private _ID As Guid = Guid.NewGuid
    Private _EntryType As BookEntryType = BookEntryType.Debetas
    Private _Account As Long = 0
    Private _Ammount As Double = 0
    Private _Person As PersonInfo = Nothing


    Public Property EntryType() As BookEntryType
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            Return _EntryType
        End Get
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Set(ByVal value As BookEntryType)
            If _EntryType <> value Then
                _EntryType = value
            End If
        End Set
    End Property

    Public Property Account() As Long
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            Return _Account
        End Get
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Set(ByVal value As Long)
            If _Account <> value Then
                _Account = value
            End If
        End Set
    End Property

    Public Property Ammount() As Double
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            Return CRound(_Ammount)
        End Get
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Set(ByVal value As Double)
            If CRound(_Ammount) <> CRound(value) Then
                _Ammount = CRound(value)
            End If
        End Set
    End Property

    Public Property Person() As PersonInfo
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Get
            Return _Person
        End Get
        <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
        Set(ByVal value As PersonInfo)
            CanWriteProperty(True)
            If Not (_Person Is Nothing AndAlso value Is Nothing) _
                AndAlso Not (Not _Person Is Nothing AndAlso Not value Is Nothing AndAlso _Person = value) Then
                _Person = value
                PropertyHasChanged()
            End If
        End Set
    End Property


    Public Shared Operator =(ByVal a As BookEntryInternal, ByVal b As BookEntryInternal) As Boolean

        If a Is Nothing AndAlso b Is Nothing Then Return True
        If a Is Nothing OrElse b Is Nothing Then Return False

        Return a._Account = b._Account AndAlso a._Person = b._Person

    End Operator

    Public Shared Operator <>(ByVal a As BookEntryInternal, ByVal b As BookEntryInternal) As Boolean
        Return Not a = b
    End Operator

    Public Sub AddBookEntryAmmount(ByVal source As BookEntryInternal)
        If Me <> source Then Exit Sub
        If Me._EntryType = source._EntryType Then
            _Ammount = CRound(_Ammount + source._Ammount)
        Else
            _Ammount = CRound(_Ammount - source._Ammount)
        End If
    End Sub

    Public Sub ReAssignType()
        If CRound(_Ammount) < 0 Then
            If _EntryType = BookEntryType.Debetas Then
                _EntryType = BookEntryType.Kreditas
            Else
                _EntryType = BookEntryType.Debetas
            End If
            _Ammount = -CRound(_Ammount)
        End If
    End Sub


    Protected Overrides Function GetIdValue() As Object
        Return _ID
    End Function

    Public Overrides Function ToString() As String
        Return _Account.ToString
    End Function

#End Region

#Region " Factory Methods "

    Friend Shared Function NewBookEntryInternal(ByVal nEntryType As BookEntryType) As BookEntryInternal
        Dim result As New BookEntryInternal
        result._EntryType = nEntryType
        Return result
    End Function

    Friend Shared Function NewBookEntryInternal(ByVal nEntryType As BookEntryType, _
        ByVal nAccount As Long, ByVal nAmmount As Double, ByVal nPerson As PersonInfo) As BookEntryInternal
        Dim result As New BookEntryInternal
        result._EntryType = nEntryType
        result._Account = nAccount
        result._Ammount = CRound(nAmmount)
        result._Person = nPerson
        Return result
    End Function

    Friend Shared Function NewBookEntryInternal(ByVal nEntryType As BookEntryType, _
        ByVal nBookEntry As General.BookEntry) As BookEntryInternal
        Dim result As New BookEntryInternal
        result._EntryType = nEntryType
        result._Account = nBookEntry.Account
        result._Ammount = nBookEntry.Amount
        result._Person = nBookEntry.Person
        Return result
    End Function

    Private Sub New()
        ' require use of factory methods
        MarkAsChild()
    End Sub

#End Region

End Class