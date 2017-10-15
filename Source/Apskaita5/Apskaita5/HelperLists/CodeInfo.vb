Imports ApskaitaObjects.Settings
Imports ApskaitaObjects.Settings.XmlProxies
Namespace HelperLists

    ''' <summary>
    ''' Represents a <see cref="Code">Code</see> value object, i.e.
    ''' a code with a name/description that is used by business objects,
    ''' e.g. income type for various tax declarations.
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public NotInheritable Class CodeInfo
        Inherits ReadOnlyBase(Of CodeInfo)
        Implements IComparable, IValueObject

#Region " Business Methods "

        Friend Shared ReadOnly IntegerCodeTypes As CodeType() = New CodeType() _
            {CodeType.GpmDeclaration, CodeType.SodraDeclaration, _
             CodeType.VmiMunicipality}

        Private ReadOnly _Guid As Guid = Guid.NewGuid()
        Private _IsEmpty As Boolean = False
        Private _Type As CodeType = CodeType.GpmDeclaration
        Private _Code As String = ""
        Private _Name As String = ""
        Private _IsObsolete As Boolean = False


        ''' <summary>
        ''' Whether an object is a place holder (does not represent a real code).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property IsEmpty() As Boolean _
            Implements IValueObject.IsEmpty
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsEmpty
            End Get
        End Property

        ''' <summary>
        ''' Gets a type of the code.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property [Type]() As CodeType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Type
            End Get
        End Property

        ''' <summary>
        ''' Gets a code value as an integer.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property CodeInt() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Dim result As Integer
                If Not Integer.TryParse(_Code, result) Then Return 0
                Return result
            End Get
        End Property

        ''' <summary>
        ''' Gets a code value as an integer.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Code() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Code
            End Get
        End Property

        ''' <summary>
        ''' Gets a code name (description).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Name() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Name.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets whether the code is obsolete, not longer in use.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property IsObsolete() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsObsolete
            End Get
        End Property


        Public Shared Operator =(ByVal a As CodeInfo, ByVal b As CodeInfo) As Boolean

            Dim aId, bId As String
            If a Is Nothing OrElse a.IsEmpty Then
                aId = ""
            Else
                aId = a.Code
            End If
            If b Is Nothing OrElse b.IsEmpty Then
                bId = ""
            Else
                bId = b.Code
            End If

            Return aId.Trim.ToLower() = bId.Trim.ToLower()

        End Operator

        Public Shared Operator <>(ByVal a As CodeInfo, ByVal b As CodeInfo) As Boolean
            Return Not a = b
        End Operator

        Public Shared Operator >(ByVal a As CodeInfo, ByVal b As CodeInfo) As Boolean

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

        Public Shared Operator <(ByVal a As CodeInfo, ByVal b As CodeInfo) As Boolean

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
            Dim tmp As CodeInfo = TryCast(obj, CodeInfo)
            If Me = tmp Then Return 0
            If Me > tmp Then Return 1
            Return -1
        End Function


        Friend Shared Function GetValueObjectIdString(ByVal value As String, _
            ByVal valueType As CodeType) As String

            If StringIsNullOrEmpty(value) Then value = ""

            Return String.Format("{0}:={1}", valueType.ToString(), _
                value.Trim.ToUpper())

        End Function

        Friend Shared Function GetValueObjectIdString(ByVal value As Integer, _
            ByVal valueType As CodeType) As String

            If value = 0 Then Return ""

            Return String.Format("{0}:={1}", valueType.ToString(), _
                value.ToString(Globalization.CultureInfo.InvariantCulture))

        End Function

        Friend Function GetValueObjectIdString() As String

            If Array.IndexOf(IntegerCodeTypes, _Type) < 0 Then
                Return GetValueObjectIdString(_Code, _Type)
            Else
                Return GetValueObjectIdString(Me.CodeInt, _Type)
            End If

        End Function


        Protected Overrides Function GetIdValue() As Object
            Return _Guid
        End Function

        Public Overrides Function ToString() As String
            If _IsEmpty Then Return ""
            Return _Code
        End Function

#End Region

#Region " Factory Methods "

        Private Shared _Empty As CodeInfo = Nothing

        ''' <summary>
        ''' Gets an empty CodeInfo (placeholder).
        ''' </summary>
        Public Shared Function Empty() As CodeInfo
            If _Empty Is Nothing Then
                _Empty = New CodeInfo
                _Empty._IsEmpty = True
            End If
            Return _Empty
        End Function

        Friend Shared Function GetCodeInfo(ByVal dr As DataRow) As CodeInfo
            Return New CodeInfo(dr)
        End Function

        Friend Shared Function GetCodeInfo(ByVal proxy As CodeProxy) As CodeInfo
            Return New CodeInfo(proxy)
        End Function


        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal dr As DataRow)
            Fetch(dr)
        End Sub

        Private Sub New(ByVal proxy As CodeProxy)
            Fetch(proxy)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal dr As DataRow)

            _Type = Utilities.ConvertDatabaseID(Of CodeType)(CIntSafe(dr.Item(0), 0))
            _Code = CStrSafe(dr.Item(1))
            _Name = My.Resources.HelperLists_CodeInfo_UnknownCodeName
            _IsObsolete = True

        End Sub

        Private Sub Fetch(ByVal proxy As CodeProxy)

            _Code = proxy.Code
            _Name = proxy.Name
            _IsObsolete = proxy.IsObsolete
            _Type = proxy.Type

        End Sub

#End Region

    End Class

End Namespace