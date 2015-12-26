Imports Csla.Validation
Imports ApskaitaObjects.Settings.XmlProxies

Namespace Settings

    ''' <summary>
    ''' Represents a code with a name/description that is used by business objects,
    ''' e.g. income type for various tax declarations.
    ''' </summary>
    ''' <remarks>Should only be used as a child of <see cref="CodeList">CodeList</see>.
    ''' Persisted using xml proxies as a part of <see cref="CommonSettings">CommonSettings</see>.</remarks>
    <Serializable()> _
    Public Class Code
        Inherits BusinessBase(Of Code)
        Implements IGetErrorForListItem

#Region " Business Methods "

        Private ReadOnly _Guid As Guid = Guid.NewGuid()
        Private _Type As CodeType = CodeType.GpmDeclaration
        Private _Code As Integer = 0
        Private _Name As String = ""
        Private _IsObsolete As Boolean = False


        ''' <summary>
        ''' Gets or sets a type of the code.
        ''' </summary>
        ''' <remarks></remarks>
        Public Property [Type]() As CodeType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Type
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As CodeType)
                CanWriteProperty(True)
                If _Type <> value Then
                    _Type = value
                    PropertyHasChanged()
                    PropertyHasChanged("TypeHumanReadable")
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a type of the code as a human readable (localized) string.
        ''' </summary>
        ''' <remarks></remarks>
        Public Property TypeHumanReadable() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return EnumValueAttribute.ConvertLocalizedName(_Type)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As String)
                CanWriteProperty(True)
                If value Is Nothing Then value = ""
                Dim typedValue As CodeType = CodeType.GpmDeclaration
                Try
                    typedValue = EnumValueAttribute.ConvertLocalizedName(Of CodeType)(value)
                Catch ex As Exception
                End Try
                If _Type <> typedValue Then
                    _Type = typedValue
                    PropertyHasChanged()
                    PropertyHasChanged("Type")
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a code value.
        ''' </summary>
        ''' <remarks></remarks>
        <IntegerField(ValueRequiredLevel.Mandatory, True)> _
        Public Property Code() As Integer
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Code
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Integer)
                CanWriteProperty(True)
                If _Code <> value Then
                    _Code = value
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a code name (description).
        ''' </summary>
        ''' <remarks></remarks>
        <StringField(ValueRequiredLevel.Mandatory, 255)> _
        Public Property Name() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Name.Trim
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As String)
                CanWriteProperty(True)
                If value Is Nothing Then value = ""
                If _Name.Trim <> value.Trim Then
                    _Name = value.Trim
                    PropertyHasChanged()
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets whether the code is obsolete, not longer in use.
        ''' </summary>
        ''' <remarks></remarks>
        Public Property IsObsolete() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _IsObsolete
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As Boolean)
                CanWriteProperty(True)
                If _IsObsolete <> value Then
                    _IsObsolete = value
                    PropertyHasChanged()
                End If
            End Set
        End Property


        Public Function HasWarnings() As Boolean
            Return (Me.BrokenRulesCollection.WarningCount > 0)
        End Function

        Public Function GetErrorString() As String _
            Implements IGetErrorForListItem.GetErrorString
            If IsValid Then Return ""
            Return String.Format(My.Resources.Common_ErrorInItem, Me.ToString, _
                vbCrLf, Me.BrokenRulesCollection.ToString(RuleSeverity.Error))
        End Function

        Public Function GetWarningString() As String _
            Implements IGetErrorForListItem.GetWarningString
            If BrokenRulesCollection.WarningCount < 1 Then Return ""
            Return String.Format(My.Resources.Common_WarningInItem, Me.ToString, _
                vbCrLf, Me.BrokenRulesCollection.ToString(RuleSeverity.Warning))
        End Function


        Protected Overrides Function GetIdValue() As Object
            Return _Guid
        End Function

        Public Overrides Function ToString() As String
            Return String.Format(My.Resources.Settings_Code_ToString, _
                TypeHumanReadable, _Code.ToString("00"), _Name)
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()

            ValidationRules.AddRule(AddressOf CommonValidation.IntegerFieldValidation, _
                New RuleArgs("Code"))
            ValidationRules.AddRule(AddressOf CommonValidation.StringFieldValidation, _
                New RuleArgs("Name"))
        End Sub

#End Region

#Region " Authorization Rules "

        Protected Overrides Sub AddAuthorizationRules()

        End Sub

#End Region

#Region " Factory Methods "

        Friend Shared Function NewCode() As Code
            Dim result As New Code
            result.ValidationRules.CheckRules()
            Return result
        End Function

        Friend Shared Function GetCode(ByVal proxy As CodeProxy) As Code
            Return New Code(proxy)
        End Function


        Private Sub New()
            ' require use of factory methods
            MarkAsChild()
        End Sub

        Private Sub New(ByVal proxy As CodeProxy)
            MarkAsChild()
            Fetch(proxy)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal proxy As CodeProxy)

            _Code = proxy.Code
            _Name = proxy.Name
            _IsObsolete = proxy.IsObsolete
            _Type = proxy.Type

            MarkOld()

        End Sub

        Friend Function GetProxy(ByVal markItemOld As Boolean) As CodeProxy

            Dim result As New CodeProxy
            result.Name = _Name
            result.Code = _Code
            result.IsObsolete = _IsObsolete
            result.Type = _Type
            If markItemOld Then MarkOld()
            Return result

        End Function

#End Region

    End Class

End Namespace