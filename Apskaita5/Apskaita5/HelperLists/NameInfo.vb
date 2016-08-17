Imports ApskaitaObjects.Settings
Imports ApskaitaObjects.Settings.XmlProxies

Namespace HelperLists

    ''' <summary>
    ''' Represents a <see cref="Name">Name</see> value object, i.e. a predefined single 
    ''' string value to use in business objects for lookup reference, 
    ''' e.g. names of SODRA administrative branches, legal groups etc.
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public NotInheritable Class NameInfo
        Inherits ReadOnlyBase(Of NameInfo)
        Implements IValueObject, IComparable

#Region " Business Methods "

        Private ReadOnly _Guid As Guid = Guid.NewGuid()
        Private _Type As NameType = NameType.SodraBranch
        Private _Name As String = ""
        Private _IsObsolete As Boolean = False


        ''' <summary>
        ''' Whether an object is a palace holder (does not represent a real name).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property IsEmpty() As Boolean _
            Implements IValueObject.IsEmpty
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return StringIsNullOrEmpty(_Name)
            End Get
        End Property

        ''' <summary>
        ''' Gets a type of the name.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property [Type]() As NameType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Type
            End Get
        End Property

        ''' <summary>
        ''' Gets a name value.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Name() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Name.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets whether the name is obsolete, not longer in use.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property IsObsolete() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsObsolete
            End Get
        End Property


        Public Shared Operator =(ByVal a As NameInfo, ByVal b As NameInfo) As Boolean

            Dim aId, bId As String
            If a Is Nothing OrElse a.IsEmpty Then
                aId = ""
            Else
                aId = a.Type.ToString & a.Name.Trim.ToLower
            End If
            If b Is Nothing OrElse b.IsEmpty Then
                bId = ""
            Else
                bId = b.Type.ToString & b.Name.Trim.ToLower
            End If

            Return aId = bId

        End Operator

        Public Shared Operator <>(ByVal a As NameInfo, ByVal b As NameInfo) As Boolean
            Return Not a = b
        End Operator

        Public Shared Operator >(ByVal a As NameInfo, ByVal b As NameInfo) As Boolean

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

        Public Shared Operator <(ByVal a As NameInfo, ByVal b As NameInfo) As Boolean

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
            Dim tmp As NameInfo = TryCast(obj, NameInfo)
            If Me = tmp Then Return 0
            If Me > tmp Then Return 1
            Return -1
        End Function


        Protected Overrides Function GetIdValue() As Object
            Return _Guid
        End Function

        Public Overrides Function ToString() As String
            Return _Name
        End Function

#End Region

#Region " Factory Methods "

        Private Shared _Empty As NameInfo = Nothing

        ''' <summary>
        ''' Gets an empty NameInfo (placeholder).
        ''' </summary>
        Public Shared Function Empty() As NameInfo
            If _Empty Is Nothing Then
                _Empty = New NameInfo
            End If
            Return _Empty
        End Function

        Friend Shared Function GetNameInfo(ByVal dr As DataRow) As NameInfo
            Return New NameInfo(dr)
        End Function

        Friend Shared Function GetNameInfo(ByVal proxy As NameProxy) As NameInfo
            Return New NameInfo(proxy)
        End Function


        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal dr As DataRow)
            Fetch(dr)
        End Sub

        Private Sub New(ByVal proxy As NameProxy)
            Fetch(proxy)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal proxy As NameProxy)

            _Name = proxy.Name
            _IsObsolete = proxy.IsObsolete
            _Type = proxy.Type

        End Sub

        Private Sub Fetch(ByVal dr As DataRow)

            _Type = Utilities.ConvertDatabaseID(Of NameType)(CIntSafe(dr.Item(0), 0))
            _Name = CStrSafe(dr.Item(1)).Trim
            _IsObsolete = True

        End Sub

#End Region

    End Class

End Namespace