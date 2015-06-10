''' <summary>
''' Represents an attribute that is used to mark ENUM values and to store their DB mapping data.
''' </summary>
''' <remarks>Used for mapping in <see cref="EnumValueAttribute.ConvertDatabaseID">ConvertDatabaseID</see>
''' and <see cref="EnumValueAttribute.ConvertDatabaseCharID">ConvertDatabaseCharID</see> methods.</remarks>
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


    ''' <summary>
    ''' Converts an ENUM value to the localized human readable name.
    ''' </summary>
    ''' <param name="enumValue">An ENUM value to convert.</param>
    ''' <remarks>Convention for the naming of the resource is full type name excluding assembly name
    ''' and an ENUM value name with "." replaced by "_", e.g. General_DefaultAccountType_Till.</remarks>
    Public Shared Function ConvertLocalizedName(ByVal enumValue As [Enum]) As String

        If enumValue Is Nothing Then Throw New ArgumentNullException("enumValue")

        Dim resourceName As String = GetResourceName(enumValue)
        Dim defaultName As String = GetEnumValueName(enumValue)

        Return GetResourceValue(resourceName, defaultName)

    End Function

    ''' <summary>
    ''' Converts a localized human readable name to an ENUM value.
    ''' </summary>
    ''' <typeparam name="T">Type of the ENUM.</typeparam>
    ''' <param name="localizedName">A localized human readable name to convert.</param>
    ''' <remarks>Convention for the naming of the resource is full type name excluding assembly name
    ''' and an ENUM value name with "." replaced by "_", e.g. General_DefaultAccountType_Till.</remarks>
    Public Shared Function ConvertLocalizedName(Of T)(ByVal localizedName As String) As T

        If StringIsNullOrEmpty(localizedName) Then Throw New ArgumentNullException("localizedName")

        If Not GetType(T).IsEnum Then Throw New InvalidOperationException(String.Format( _
            My.Resources.EnumValueAttribute_InvalidType, "ConvertLocalizedName", GetType(T).FullName))

        For Each value As [Enum] In [Enum].GetValues(GetType(T))

            If ConvertLocalizedName(value).Trim.ToLower = localizedName.Trim.ToLower Then
                Return DirectCast(DirectCast(value, Object), T)
            End If

        Next

        Return Nothing

    End Function

    ''' <summary>
    ''' Converts an ENUM value to the database integer value using <see cref="EnumValueAttribute.DatabaseID">DatabaseID</see> property.
    ''' </summary>
    ''' <param name="enumValue">An ENUM value to convert.</param>
    ''' <remarks></remarks>
    Public Shared Function ConvertDatabaseID(ByVal enumValue As [Enum]) As Integer

        If enumValue Is Nothing Then Throw New ArgumentNullException("enumValue")

        Dim enumAttribute As EnumValueAttribute = GetEnumValueAttribute(enumValue)

        If enumAttribute Is Nothing Then
            Throw New NotImplementedException(My.Resources.EnumValueAttribute_AttributeNull)
        End If

        Return enumAttribute._DatabaseID

    End Function

    ''' <summary>
    ''' Converts a database integer value to an ENUM value using <see cref="EnumValueAttribute.DatabaseID">DatabaseID</see> property.
    ''' </summary>
    ''' <typeparam name="T">Type of the ENUM.</typeparam>
    ''' <param name="valueDatabaseID">A database integer value that encodes an ENUM value.</param>
    ''' <remarks></remarks>
    Public Shared Function ConvertDatabaseID(Of T)(ByVal valueDatabaseID As Integer) As T

        If Not GetType(T).IsEnum Then Throw New InvalidOperationException(String.Format( _
            My.Resources.EnumValueAttribute_InvalidType, "ConvertDatabaseID", GetType(T).FullName))

        Dim enumAttribute As EnumValueAttribute

        For Each value As [Enum] In [Enum].GetValues(GetType(T))

            enumAttribute = GetEnumValueAttribute(value)

            If Not enumAttribute Is Nothing AndAlso enumAttribute._DatabaseID = valueDatabaseID Then
                Return DirectCast(DirectCast(value, Object), T)
            End If

        Next

        Return Nothing

    End Function

    ''' <summary>
    ''' Converts an ENUM value to the database string value using <see cref="EnumValueAttribute.DatabaseCharCode">DatabaseCharCode</see> property.
    ''' </summary>
    ''' <param name="enumValue">An ENUM value to convert.</param>
    ''' <remarks>Only used for backwards compartability in some objects. Not to be used for a new objects.</remarks>
    Public Shared Function ConvertDatabaseCharID(ByVal enumValue As [Enum]) As String

        If enumValue Is Nothing Then Throw New ArgumentNullException("enumValue")

        Dim enumAttribute As EnumValueAttribute = GetEnumValueAttribute(enumValue)

        If enumAttribute Is Nothing Then
            Throw New NotImplementedException(My.Resources.EnumValueAttribute_AttributeNull)
        End If

        Return enumAttribute._DatabaseCharCode

    End Function

    ''' <summary>
    ''' Converts a database string value to an ENUM value using <see cref="EnumValueAttribute.DatabaseCharCode">DatabaseCharCode</see> property.
    ''' </summary>
    ''' <typeparam name="T">Type of the ENUM.</typeparam>
    ''' <param name="valueDatabaseCharID">A database string value that encodes an ENUM value.</param>
    ''' <remarks>Only used for backwards compartability in some objects. Not to be used for a new objects.</remarks>
    Public Shared Function ConvertDatabaseCharID(Of T)(ByVal valueDatabaseCharID As String) As T

        If StringIsNullOrEmpty(valueDatabaseCharID) Then Throw New ArgumentNullException("valueDatabaseCharID")

        If Not GetType(T).IsEnum Then Throw New InvalidOperationException(String.Format( _
            My.Resources.EnumValueAttribute_InvalidType, "ConvertDatabaseCharID", GetType(T).FullName))

        Dim enumAttribute As EnumValueAttribute

        For Each value As [Enum] In [Enum].GetValues(GetType(T))

            enumAttribute = GetEnumValueAttribute(value)

            If Not enumAttribute Is Nothing AndAlso enumAttribute._DatabaseCharCode.Trim.ToLower _
                = valueDatabaseCharID.Trim.ToLower Then
                Return DirectCast(DirectCast(value, Object), T)
            End If

        Next

        Return Nothing

    End Function

    ''' <summary>
    ''' Gets a list of localized human readable names of ENUM values.
    ''' </summary>
    ''' <typeparam name="T">Type of the ENUM.</typeparam>
    ''' <remarks>Convention for the naming of the resource is full type name excluding assembly name
    ''' and an ENUM value name with "." replaced by "_", e.g. General_DefaultAccountType_Till.</remarks>
    Public Shared Function GetLocalizedNameList(ByVal enumType As Type) As List(Of String)

        If enumType Is Nothing Then Throw New ArgumentNullException("enumType")

        If Not enumType.IsEnum Then Throw New InvalidOperationException(String.Format( _
            My.Resources.EnumValueAttribute_InvalidType, "GetLocalizedNameList", enumType.FullName))

        Dim result As New List(Of String)

        For Each value As [Enum] In [Enum].GetValues(enumType)
            result.Add(ConvertLocalizedName(value).Trim)
        Next

        Return result

    End Function


    Private Shared Function GetResourceName(ByVal enumValue As [Enum]) As String

        If enumValue Is Nothing Then Return ""

        Return enumValue.GetType.FullName.Substring(enumValue.GetType.FullName.IndexOf(".") + 1) _
            .Replace(".", "_") & "_" & [Enum].GetName(enumValue.GetType, enumValue)

    End Function

    Private Shared Function GetResourceValue(ByVal resourceName As String, _
        ByVal defaultValue As String) As String

        Dim result As String = defaultValue
        Try
            result = My.Resources.ResourceManager.GetString(resourceName)
        Catch ex As Exception
        End Try

        Return result

    End Function

    Private Shared Function GetEnumValueName(ByVal enumValue As [Enum]) As String

        Dim result As String = "undefined"

        Try
            result = [Enum].GetName(enumValue.GetType, enumValue)
        Catch e As Exception
        End Try

        Return result

    End Function

    Private Shared Function GetEnumValueAttribute(ByVal enumValue As [Enum]) As EnumValueAttribute

        Dim fi As System.Reflection.FieldInfo = enumValue.GetType().GetField(enumValue.ToString())

        Dim result As Object() = fi.GetCustomAttributes(GetType(EnumValueAttribute), True)

        If result Is Nothing OrElse result.Length < 1 Then Return Nothing

        Return DirectCast(result(0), EnumValueAttribute)

    End Function

End Class
