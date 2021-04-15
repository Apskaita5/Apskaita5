Namespace HelperLists

    ''' <summary>
    ''' Represents a <see cref="General.SharesClass">shares class</see> value object.
    ''' </summary>
    ''' <remarks>Values are stored in the database table SharesClasses.</remarks>
    <Serializable()>
    Public NotInheritable Class SharesClassInfo
        Inherits ReadOnlyBase(Of SharesClassInfo)
        Implements IValueObject, IComparable

#Region " Business Methods "

        Private _ID As Integer = 0
        Private _Name As String = ""
        Private _Description As String = ""
        Private _ValuePerUnit As Double = 0.0


        ''' <summary>
        ''' Whether an object is a place holder (does not represent a real shares class).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property IsEmpty() As Boolean _
            Implements IValueObject.IsEmpty
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return Not _ID > 0
            End Get
        End Property

        ''' <summary>
        ''' Shares class ID - <see cref="Int">Int</see> number that identifies account.
        ''' Equals zero for a placeholder object.
        ''' </summary>
        ''' <value></value>
        ''' <remarks>Corresponds to <see cref="General.SharesClass.ID">SharesClass.ID</see> property.
        ''' Value is stored in the database field SharesClasses.ID.</remarks>
        Public ReadOnly Property ID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _ID
            End Get
        End Property

        ''' <summary>
        ''' Shares class name - a short name of a shares class.
        ''' Equals empty string for a placeholder.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="General.SharesClass.Name">SharesClass.Name</see> property.
        ''' Value is stored in the database field SharesClasses.Name.</remarks>
        Public ReadOnly Property Name() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _Name.Trim
            End Get
        End Property

        ''' <summary>
        ''' Shares class description - a description of a shares class.
        ''' Equals empty string for a placeholder.
        ''' </summary>
        ''' <remarks>Corresponds to <see cref="General.SharesClass.Description">SharesClass.Name</see> property.
        ''' Value is stored in the database field SharesClasses.Description.</remarks>
        Public ReadOnly Property Description() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _Description.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a value of the shares class per unit.
        ''' </summary>
        ''' <remarks>Value is stored in the database table SharesClasses.ValuePerUnit.</remarks>
        Public ReadOnly Property ValuePerUnit As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return CRound(_ValuePerUnit)
            End Get
        End Property


        Public Shared Operator =(ByVal a As SharesClassInfo, ByVal b As SharesClassInfo) As Boolean

            Dim aId, bId As Integer
            If a Is Nothing OrElse a.IsEmpty Then
                aId = 0
            Else
                aId = a.ID
            End If
            If b Is Nothing OrElse b.IsEmpty Then
                bId = 0
            Else
                bId = b.ID
            End If

            Return aId = bId

        End Operator

        Public Shared Operator <>(ByVal a As SharesClassInfo, ByVal b As SharesClassInfo) As Boolean
            Return Not a = b
        End Operator

        Public Shared Operator >(ByVal a As SharesClassInfo, ByVal b As SharesClassInfo) As Boolean

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

        Public Shared Operator <(ByVal a As SharesClassInfo, ByVal b As SharesClassInfo) As Boolean

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
            Dim tmp As SharesClassInfo = TryCast(obj, SharesClassInfo)
            If Me = tmp Then Return 0
            If Me > tmp Then Return 1
            Return -1
        End Function


        Protected Overrides Function GetIdValue() As Object
            Return _ID
        End Function

        Public Overrides Function ToString() As String
            Return String.Format(My.Resources.HelperLists_SharesClassInfo_ToString,
                _Name, DblParser(_ValuePerUnit))
        End Function

#End Region

#Region " Factory Methods "

        Private Shared _Empty As SharesClassInfo = Nothing

        ''' <summary>
        ''' Gets an empty shares class info (placeholder).
        ''' </summary>
        Public Shared Function Empty() As SharesClassInfo
            If _Empty Is Nothing Then
                _Empty = New SharesClassInfo
            End If
            Return _Empty
        End Function

        Friend Shared Function GetSharesClassInfo(ByVal dr As DataRow, startIndex As Integer) As SharesClassInfo
            Return New SharesClassInfo(dr, startIndex)
        End Function

        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(dr As DataRow, startIndex As Integer)
            Fetch(dr, startIndex)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal dr As DataRow, startIndex As Integer)

            _ID = CIntSafe(dr.Item(0 + startIndex), 0)
            _Name = CStrSafe(dr.Item(startIndex + 1)).Trim
            _ValuePerUnit = CDblSafe(dr.Item(startIndex + 2), 2, 0)
            _Description = CStrSafe(dr.Item(startIndex + 3)).Trim

        End Sub

#End Region

    End Class

End Namespace
