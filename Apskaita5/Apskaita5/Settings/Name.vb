Imports ApskaitaObjects.Attributes
Imports Csla.Validation
Imports ApskaitaObjects.Settings.XmlProxies
Namespace Settings

    ''' <summary>
    ''' Represents a predefined single string value to use in business objects
    ''' for lookup reference, e.g. names of SODRA administrative branches,
    ''' legal groups etc.
    ''' </summary>
    ''' <remarks>Should only be used as a child of <see cref="NameList">NameList</see>.
    ''' Persisted using xml proxies as a part of <see cref="CommonSettings">CommonSettings</see>.</remarks>
    <Serializable()> _
    Public NotInheritable Class Name
        Inherits BusinessBase(Of Name)
        Implements IGetErrorForListItem

#Region " Business Methods "

        Private ReadOnly _Guid As Guid = Guid.NewGuid()
        Private _Type As NameType = NameType.LongTermAssetLegalGroup
        Private _Name As String = ""
        Private _IsObsolete As Boolean = False


        ''' <summary>
        ''' Gets or sets a type of the name.
        ''' </summary>
        ''' <remarks></remarks>
        Public Property [Type]() As NameType
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Type
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As NameType)
                CanWriteProperty(True)
                If _Type <> value Then
                    _Type = value
                    PropertyHasChanged()
                    PropertyHasChanged("TypeHumanReadable")
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets a type of the name as a human readable (localized) string.
        ''' </summary>
        ''' <remarks></remarks>
        Public Property TypeHumanReadable() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return Utilities.ConvertLocalizedName(_Type)
            End Get
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Set(ByVal value As String)
                CanWriteProperty(True)
                If value Is Nothing Then value = ""
                Dim typedValue As NameType = NameType.LongTermAssetLegalGroup
                Try
                    typedValue = Utilities.ConvertLocalizedName(Of NameType)(value)
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
        ''' Gets or sets a name value.
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
        ''' Gets or sets whether the name is obsolete, not longer in use.
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
            Return String.Format(My.Resources.Settings_Name_ToString, _
                TypeHumanReadable, _Name)
        End Function

#End Region

#Region " Validation Rules "

        Protected Overrides Sub AddBusinessRules()
            ValidationRules.AddRule(AddressOf CommonValidation.CommonValidation.StringFieldValidation, _
                New RuleArgs("Name"))
        End Sub

#End Region

#Region " Authorization Rules "

        Protected Overrides Sub AddAuthorizationRules()

        End Sub

#End Region

#Region " Factory Methods "

        Friend Shared Function NewName() As Name
            Dim result As New Name
            result.ValidationRules.CheckRules()
            Return result
        End Function

        Friend Shared Function GetName(ByVal proxy As NameProxy) As Name
            Return New Name(proxy)
        End Function


        Private Sub New()
            ' require use of factory methods
            MarkAsChild()
        End Sub

        Private Sub New(ByVal proxy As NameProxy)
            MarkAsChild()
            Fetch(proxy)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal proxy As NameProxy)

            _Name = proxy.Name
            _IsObsolete = proxy.IsObsolete
            _Type = proxy.Type

            MarkOld()

        End Sub

        Friend Function GetProxy(ByVal markItemOld As Boolean) As NameProxy
            Dim result As New NameProxy
            result.Name = _Name
            result.IsObsolete = _IsObsolete
            result.Type = _Type
            If markItemOld Then MarkOld()
            Return result
        End Function

#End Region

    End Class

End Namespace