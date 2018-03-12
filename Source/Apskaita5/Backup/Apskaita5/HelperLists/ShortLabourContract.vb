Namespace HelperLists

    ''' <summary>
    ''' A value object that provides serial, number and date of an existing labour contract.
    ''' </summary>
    ''' <remarks>Should only be used as a child of <see cref="ShortLabourContractList">ShortLabourContractList</see>.</remarks>
    <Serializable()> _
    Public NotInheritable Class ShortLabourContract
        Inherits ReadOnlyBase(Of ServiceInfo)
        Implements IValueObject, IComparable

#Region " Business Methods "

        Private ReadOnly _Guid As Guid = Guid.NewGuid()
        Private _PersonID As Integer = 0
        Private _PersonName As String = ""
        Private _PersonCode As String = ""
        Private _Number As Integer = 0
        Private _Serial As String = ""
        Private _Date As Date = DateTime.MinValue
        Private _TerminationDate As Date = DateTime.MaxValue
        Private _Position As String = ""


        ''' <summary>
        ''' Gets whether an object is a place holder (does not represent a real labour contract).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property IsEmpty() As Boolean _
            Implements IValueObject.IsEmpty
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return Not _Number > 0
            End Get
        End Property

        ''' <summary>
        ''' Gets an ID of the person/worker (assigned automaticaly by DB AUTOINCREMENT).
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="General.Person.ID">Person.ID</see> property.
        ''' Value is stored in the database field asmenys.ID.</remarks>
        Public ReadOnly Property PersonID() As Integer
            Get
                Return _PersonID
            End Get
        End Property

        ''' <summary>
        ''' Gets an official name of the person (worker).
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="General.Person.Name">Person.Name</see> property.
        ''' Value is stored in the database field asmenys.Pavad.</remarks>
        Public ReadOnly Property PersonName() As String
            Get
                Return _PersonName
            End Get
        End Property

        ''' <summary>
        ''' Gets an official personal code of the person (worker).
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="General.Person.Code">Person.Code</see> property.
        ''' Value is stored in the database field asmenys.Kodas.</remarks>
        Public ReadOnly Property PersonCode() As String
            Get
                Return _PersonCode
            End Get
        End Property

        ''' <summary>
        ''' Number of the labour contract.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Number() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Number
            End Get
        End Property

        ''' <summary>
        ''' Serial of the labour contract.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Serial() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Serial
            End Get
        End Property

        ''' <summary>
        ''' Date of the labour contract.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property [Date]() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Date
            End Get
        End Property

        ''' <summary>
        ''' Date of the termination of the labour contract.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property TerminationDate() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _TerminationDate
            End Get
        End Property

        ''' <summary>
        ''' Date of the termination of the labour contract as a string value.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property TerminationDateString() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                If _TerminationDate = System.DateTime.MaxValue Then
                    Return ""
                Else
                    Return _TerminationDate.ToString("yyyy-MM-dd")
                End If
            End Get
        End Property

        ''' <summary>
        ''' Current position of the worker according to the labour contract.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Position() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Position
            End Get
        End Property


        Public Shared Operator =(ByVal a As ShortLabourContract, ByVal b As ShortLabourContract) As Boolean

            Dim aId, bId As String
            If a Is Nothing OrElse a.IsEmpty Then
                aId = ""
            Else
                aId = a._Serial.Trim.ToUpper & a._Number.ToString
            End If
            If b Is Nothing OrElse b.IsEmpty Then
                bId = ""
            Else
                bId = b._Serial.Trim.ToUpper & b._Number.ToString
            End If

            Return aId = bId

        End Operator

        Public Shared Operator <>(ByVal a As ShortLabourContract, ByVal b As ShortLabourContract) As Boolean
            Return Not a = b
        End Operator

        Public Shared Operator >(ByVal a As ShortLabourContract, ByVal b As ShortLabourContract) As Boolean

            Dim aToString, bToString As String
            If a Is Nothing OrElse a.IsEmpty Then
                aToString = ""
            Else
                aToString = a.ToString
            End If
            If b Is Nothing OrElse b.IsEmpty Then
                bToString = ""
            Else
                bToString = b.ToString
            End If

            Return aToString > bToString

        End Operator

        Public Shared Operator <(ByVal a As ShortLabourContract, ByVal b As ShortLabourContract) As Boolean

            Dim aToString, bToString As String
            If a Is Nothing OrElse a.IsEmpty Then
                aToString = ""
            Else
                aToString = a.ToString
            End If
            If b Is Nothing OrElse b.IsEmpty Then
                bToString = ""
            Else
                bToString = b.ToString
            End If

            Return aToString < bToString

        End Operator

        Public Function CompareTo(ByVal obj As Object) As Integer Implements System.IComparable.CompareTo
            Dim tmp As ShortLabourContract = TryCast(obj, ShortLabourContract)
            If Me = tmp Then Return 0
            If Me > tmp Then Return 1
            Return -1
        End Function


        Protected Overrides Function GetIdValue() As Object
            Return _Guid
        End Function

        Public Overrides Function ToString() As String
            If Not _Number > 0 Then Return ""
            Return String.Format(My.Resources.HelperLists_ShortLabourContract_ToString, _
                _Position, _PersonName, _Date.ToString("yyyy-MM-dd"), _
                _Serial, _Number.ToString)
        End Function

#End Region

#Region " Factory Methods "

        Private Shared _Empty As ShortLabourContract = Nothing

        ''' <summary>
        ''' Gets an empty ShortLabourContract (placeholder).
        ''' </summary>
        Public Shared Function Empty() As ShortLabourContract
            If _Empty Is Nothing Then
                _Empty = New ShortLabourContract
            End If
            Return _Empty
        End Function

        Friend Shared Function GetShortLabourContract(ByVal dr As DataRow) As ShortLabourContract
            Return New ShortLabourContract(dr)
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

            _PersonID = CIntSafe(dr.Item(0), 0)
            _PersonName = CStrSafe(dr.Item(1))
            _PersonCode = CStrSafe(dr.Item(2))
            _Number = CIntSafe(dr.Item(3), 0)
            _Serial = CStrSafe(dr.Item(4))
            _Date = CDateSafe(dr.Item(5), DateTime.MinValue)
            _TerminationDate = CDateSafe(dr.Item(6), DateTime.MaxValue)
            _Position = CStrSafe(dr.Item(7))

        End Sub

#End Region

    End Class

End Namespace

