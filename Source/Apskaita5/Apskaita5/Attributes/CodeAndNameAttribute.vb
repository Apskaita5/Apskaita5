Imports ApskaitaObjects.Settings

Namespace Attributes

    ''' <summary>
    ''' Represents an attribute that is used to mark business objects' properties holding a 
    ''' <see cref="HelperLists.CodeInfo">code and name</see> and to store basic business rules
    ''' (mandatory, type, etc.).
    ''' </summary>
    ''' <remarks>Used for validation purposes in <see cref="CodeFieldValidation">CodeFieldValidation</see>
    ''' method.
    ''' Could be used by GUI to initialize appropriate controls.</remarks>
    <Serializable()>
    <AttributeUsage(AttributeTargets.Property, AllowMultiple:=False, Inherited:=True)>
    Public Class CodeAndNameFieldAttribute
        Inherits ValueObjectFieldAttribute
        Implements IValueObjectIdProvider, IDataSourceProvider, IValidationRuleProvider

        Private _Type As ApskaitaObjects.Settings.CodeType

        ''' <summary>
        ''' Gets a type of the code.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property [Type]() As ApskaitaObjects.Settings.CodeType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
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
        ''' to a property.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property DataSourceValueMember() As String _
            Implements IDataSourceProvider.DataSourceValueMember
            Get
                Return ""
            End Get
        End Property


        ''' <summary>
        ''' Creates a new instance of an CodeFieldAttribute class.
        ''' </summary>
        ''' <param name="valueValueRequired">whether the property value is mandatory.</param>
        ''' <param name="valueType">a type of the code</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal valueValueRequired As ValueRequiredLevel, ByVal valueType As ApskaitaObjects.Settings.CodeType)
            MyBase.New(valueValueRequired)
            _Type = valueType

        End Sub


        Friend Function GetValueObjectId(ByVal obj As Object,
            ByVal prop As System.Reflection.PropertyInfo) As String _
            Implements IValueObjectIdProvider.GetValueObjectId

            If obj Is Nothing Then
                Throw New ArgumentNullException("obj")
            End If

            If prop Is Nothing Then
                Throw New ArgumentNullException("prop")
            End If

            Dim value As CodeInfo = Nothing
            Try
                value = DirectCast(prop.GetValue(obj, Nothing), CodeInfo)
            Catch ex As Exception
            End Try

            If value Is Nothing Then
                Return ""
            Else
                Return value.GetValueObjectIdString()
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
            Return AddressOf CommonValidation.CodeAndNameFieldValidation
        End Function

    End Class

End Namespace
