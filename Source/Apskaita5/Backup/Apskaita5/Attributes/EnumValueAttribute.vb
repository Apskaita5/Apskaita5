Namespace Attributes
    ''' <summary>
    ''' Represents an attribute that is used to mark ENUM values and to store their DB mapping data.
    ''' </summary>
    ''' <remarks>Used for mapping in <see cref="ConvertDatabaseID">ConvertDatabaseID</see>
    ''' and <see cref="ConvertDatabaseCharID">ConvertDatabaseCharID</see> methods.</remarks>
    <Serializable()> _
    <AttributeUsage(AttributeTargets.Field, AllowMultiple:=False, Inherited:=True)> _
Public Class EnumValueAttribute
        Inherits System.Attribute

        Private _DatabaseID As Integer = -1
        Private _DatabaseCharCode As String = ""

        ''' <summary>
        ''' Gets an integer value that is assigned to the current ENUM field in the database.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DatabaseID() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DatabaseID
            End Get
        End Property

        ''' <summary>
        ''' Gets a string value that is assigned to the current ENUM field in the database.
        ''' </summary>
        ''' <remarks>Only used for backwards compartability in some objects. Not to be used for a new objects.</remarks>
        Public ReadOnly Property DatabaseCharCode() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _DatabaseCharCode.Trim
            End Get
        End Property


        ''' <summary>
        ''' Creates a new EnumValueAttribute instance.
        ''' </summary>
        ''' <param name="valueDatabaseID">Gets an integer value that is assigned to the current ENUM field in the database.</param>
        ''' <param name="valueDatabaseCharCode">Gets a string value that is assigned to the current ENUM field in the database.</param>
        ''' <remarks>Parameter <paramref name="valueDatabaseCharCode">valueDatabaseCharCode</paramref> 
        ''' is only used for backwards compartability in some objects. Not to be used for a new objects.</remarks>
        Public Sub New(ByVal valueDatabaseID As Integer, Optional ByVal valueDatabaseCharCode As String = "")
            _DatabaseID = valueDatabaseID
            _DatabaseCharCode = valueDatabaseCharCode
        End Sub

    End Class
End Namespace