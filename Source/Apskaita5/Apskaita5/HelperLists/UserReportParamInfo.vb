Imports ApskaitaObjects.My.Resources

Namespace HelperLists

    ''' <summary>
    ''' Represents a user report parameter (as defined in an *.rdl file).
    ''' </summary>
    ''' <remarks>Should only be used as a child of <see cref="UserReportParamInfoList">UserReportParamInfoList</see>.</remarks>
    <Serializable()> _
    Public NotInheritable Class UserReportParamInfo
        Inherits ReadOnlyBase(Of UserReportParamInfo)

#Region " Business Methods "

        Private ReadOnly _Guid As Guid = Guid.NewGuid()
        Private _Name As String = ""
        Private _Prompt As String = ""
        Private _AllowNull As Boolean = False
        Private _ParamValues As UserReportParamValueInfoList = Nothing


        ''' <summary>
        ''' Gets a name of the parameter.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Name() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Name.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a user prompt for the parameter (human readable label).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Prompt() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Prompt.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets whether the parameter can be null (empty, not specified by the user).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property AllowNull() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AllowNull
            End Get
        End Property

        ''' <summary>
        ''' Gets a list of possible parameter values (for use in a combobox).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property ParamValues() As UserReportParamValueInfoList
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ParamValues
            End Get
        End Property


        Protected Overrides Function GetIdValue() As Object
            Return _Guid
        End Function

        Public Overrides Function ToString() As String
            Return String.Format(HelperLists_UserReportParamInfo_ToString, _
                _Prompt, _Name, IIf(_AllowNull, "", HelperLists_UserReportParamInfo_MandatoryString))
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function GetUserReportParamInfo(ByVal node As Xml.XmlNode) As UserReportParamInfo
            Return New UserReportParamInfo(node)
        End Function


        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal node As Xml.XmlNode)
            Fetch(node)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal node As Xml.XmlNode)

            Dim nameAttribute As Xml.XmlAttribute = node.Attributes("Name")
            If nameAttribute Is Nothing Then
                Throw New Exception(HelperLists_UserReportParamInfo_FailedToParseParamName)
            End If
            _Name = nameAttribute.InnerText

            For Each childNode As Xml.XmlNode In node.ChildNodes
                If childNode.Name.Trim.ToLower = "prompt" Then
                    _Prompt = childNode.InnerText
                ElseIf childNode.Name.Trim.ToLower = "nullable" Then
                    Boolean.TryParse(childNode.InnerText, _AllowNull)
                ElseIf childNode.Name.Trim.ToLower = "validvalues" Then
                    _ParamValues = UserReportParamValueInfoList. _
                        GetUserReportParamValueInfoList(childNode)
                End If
            Next

            If StringIsNullOrEmpty(_Prompt) Then
                Throw New Exception(String.Format( _
                    HelperLists_UserReportParamInfo_FailedToParseParamPrompt, _Name))
            End If

            If _ParamValues Is Nothing Then
                If _Name.Trim.ToLower.EndsWith("ComboBox".ToLower) Then
                    Throw New Exception(String.Format( _
                        HelperLists_UserReportParamInfo_ParamValuesMissing, _Prompt))
                Else
                    _ParamValues = UserReportParamValueInfoList. _
                        NewUserReportParamValueInfoList()
                End If
            End If

        End Sub

#End Region

    End Class

End Namespace