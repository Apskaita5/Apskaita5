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
    Public Class NameInfo
        Inherits ReadOnlyBase(Of NameInfo)

#Region " Business Methods "

        Private ReadOnly _Guid As Guid = Guid.NewGuid()
        Private _Type As NameType = Nothing
        Private _Name As String = ""
        Private _IsObsolete As Boolean = False


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



        Protected Overrides Function GetIdValue() As Object
            Return _Guid
        End Function

        Public Overrides Function ToString() As String
            Return _Name
        End Function

#End Region

#Region " Factory Methods "

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