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
    Public Class CodeInfo
        Inherits ReadOnlyBase(Of CodeInfo)

#Region " Business Methods "

        Private ReadOnly _Guid As Guid = Guid.NewGuid()
        Private _Type As CodeType = CodeType.GpmDeclaration
        Private _Code As Integer = 0
        Private _Name As String = ""
        Private _IsObsolete As Boolean = False


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
        ''' Gets a code value.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Code() As Integer
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



        Protected Overrides Function GetIdValue() As Object
            Return _Guid
        End Function

        Public Overrides Function ToString() As String
            Return _Code.ToString("00")
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function NewCodeInfo() As CodeInfo
            Return New CodeInfo()
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
            _Code = CIntSafe(dr.Item(1), 0)
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