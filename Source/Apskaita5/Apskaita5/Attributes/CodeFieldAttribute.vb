Imports ApskaitaObjects.Settings

Namespace Attributes

    ''' <summary>
    ''' Represents an attribute that is used to mark business objects' properties holding a 
    ''' <see cref="HelperLists.CodeInfo.Code">code</see> and to store basic business rules
    ''' (mandatory, type, etc.).
    ''' </summary>
    ''' <remarks>Used for validation purposes in <see cref="CodeFieldValidation">CodeFieldValidation</see>
    ''' method.
    ''' Could be used by GUI to initialize appropriate controls.</remarks>
    <Serializable()> _
    <AttributeUsage(AttributeTargets.Property, AllowMultiple:=False, Inherited:=True)> _
    Public Class CodeFieldAttribute
        Inherits BusinessFieldAttribute
        Implements IValueObjectIdProvider, IDataSourceProvider, IValidationRuleProvider

        Private _ValueRequired As ValueRequiredLevel = ValueRequiredLevel.Optional
        Private _Type As ApskaitaObjects.Settings.CodeType

        ''' <summary>
        ''' Gets whether the property value is mandatory.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property ValueRequired() As ValueRequiredLevel
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ValueRequired
            End Get
        End Property

        ''' <summary>
        ''' Gets a type of the code.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property [Type]() As ApskaitaObjects.Settings.CodeType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Type
            End Get
        End Property

        ''' <summary>
        ''' Gets a base type of the datasource (<see cref="HelperLists.CodeInfoList">CodeInfoList</see>).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DataSourceBaseType() As System.Type _
            Implements IDataSourceProvider.DataSourceBaseType
            Get
                Return GetType(HelperLists.CodeInfoList)
            End Get
        End Property

        ''' <summary>
        ''' Gets a property display value (TypedPropertyValue.ToString) that 
        ''' corresponds to a null value ('0' represents a null 
        ''' CodeInfo for <see cref="HelperLists.CodeInfoList">CodeInfoList</see>
        ''' integer codes, an empty string represents a null CodeInfo for 
        ''' <see cref="HelperLists.CodeInfoList">CodeInfoList</see> string codes).
        ''' Indicates that such a value should be displayed as empty string instead.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DataSourceEmptyValueString() As String _
            Implements IDataSourceProvider.DataSourceEmptyValueString
            Get
                If Array.IndexOf(CodeInfo.IntegerCodeTypes, _Type) < 0 Then
                    Return ""
                Else
                    Return "0"
                End If
            End Get
        End Property

        ''' <summary>
        ''' Gets a name of the value object property that should be assigned 
        ''' to a property (CodeInt for <see cref="HelperLists.CodeInfoList">CodeInfoList</see>
        ''' integer codes, Code for <see cref="HelperLists.CodeInfoList">CodeInfoList</see>
        ''' string codes).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DataSourceValueMember() As String _
            Implements IDataSourceProvider.DataSourceValueMember
            Get
                If Array.IndexOf(CodeInfo.IntegerCodeTypes, _Type) < 0 Then
                    Return "Code"
                Else
                    Return "CodeInt"
                End If
            End Get
        End Property


        ''' <summary>
        ''' Creates a new instance of an CodeFieldAttribute class.
        ''' </summary>
        ''' <param name="valueValueRequired">whether the property value is mandatory.</param>
        ''' <param name="valueType">a type of the code</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal valueValueRequired As ValueRequiredLevel, ByVal valueType As ApskaitaObjects.Settings.CodeType)

            _ValueRequired = valueValueRequired
            _Type = valueType

        End Sub


        Friend Function GetValueObjectId(ByVal obj As Object, _
            ByVal prop As System.Reflection.PropertyInfo) As String _
            Implements IValueObjectIdProvider.GetValueObjectId

            If obj Is Nothing Then
                Throw New ArgumentNullException("obj")
            End If

            If prop Is Nothing Then
                Throw New ArgumentNullException("prop")
            End If

            If prop.PropertyType Is GetType(Integer) Then

                Dim value As Integer = 0
                Try
                    value = DirectCast(prop.GetValue(obj, Nothing), Integer)
                Catch ex As Exception
                End Try

                Return CodeInfo.GetValueObjectIdString(value, _Type)

            Else

                Dim value As String = ""
                Try
                    value = DirectCast(prop.GetValue(obj, Nothing), String)
                Catch ex As Exception
                End Try

                Return CodeInfo.GetValueObjectIdString(value, _Type)

            End If

        End Function

        Friend Function GetValueObjectType() As Type _
            Implements IValueObjectIdProvider.GetValueObjectType
            Return GetType(CodeInfoList)
        End Function

        ''' <summary>
        ''' Gets a datasource (a <see cref="FilteredBindingList(Of CodeInfo)">FilteredBindingList(Of CodeInfo)</see>) 
        ''' for a property.
        ''' </summary>
        ''' <remarks></remarks>
        Public Function GetDataSource(ByVal valueObjectIds As List(Of String)) As IList _
            Implements IDataSourceProvider.GetDataSource
            Return CodeInfoList.GetCachedFilteredList(_Type, True, False, valueObjectIds)
        End Function

        ''' <summary>
        ''' Gets a concrete validation rule method to validate the property value.
        ''' </summary>
        ''' <remarks></remarks>
        Public Function GetValidationRule() As Csla.Validation.RuleHandler _
            Implements IValidationRuleProvider.GetValidationRule
            Return AddressOf CommonValidation.CodeFieldValidation
        End Function

    End Class

End Namespace
