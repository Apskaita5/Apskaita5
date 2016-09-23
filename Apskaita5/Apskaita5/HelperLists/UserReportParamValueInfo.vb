Imports System.Xml
Imports ApskaitaObjects.My.Resources

Namespace HelperLists

    ''' <summary>
    ''' Represents a user report parameter value for use in a combobox.
    ''' </summary>
    ''' <remarks>Should only be used as a child of <see cref="UserReportParamValueInfoList">UserReportParamValueInfoList</see>.</remarks>
    <Serializable()> _
    Public NotInheritable Class UserReportParamValueInfo
        Inherits ReadOnlyBase(Of UserReportParamValueInfo)

#Region " Business Methods "

        Private ReadOnly _Guid As Guid = Guid.NewGuid()
        Private _Name As String = ""
        Private _Value As String = ""


        ''' <summary>
        ''' Gets a name of the parameter value for display in a combobox.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Name() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Name.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets a parameter value to pass to the user report.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Value() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Value.Trim
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

        Friend Shared Function GetUserReportParamValueInfo(ByVal node As XmlNode) As UserReportParamValueInfo
            Return New UserReportParamValueInfo(node)
        End Function


        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal node As XmlNode)
            Fetch(node)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal node As XmlNode)

            Dim valueNodeFound As Boolean = False
            Dim nameNodeFound As Boolean = False
            For Each childNode As XmlNode In node.ChildNodes
                If childNode.Name.Trim.ToLower = "Value".ToLower Then
                    _Value = childNode.InnerText
                    valueNodeFound = True
                ElseIf childNode.Name.Trim.ToLower = "Label".ToLower Then
                    _Name = childNode.InnerText
                    nameNodeFound = True
                End If
            Next

            If Not valueNodeFound Then
                Throw New Exception(HelperLists_UserReportParamValueInfo_FailedToParseParamValue)
            ElseIf Not nameNodeFound Then
                Throw New Exception(HelperLists_UserReportParamValueInfo_FailedToParseParamName)
            End If

        End Sub

#End Region

    End Class

End Namespace