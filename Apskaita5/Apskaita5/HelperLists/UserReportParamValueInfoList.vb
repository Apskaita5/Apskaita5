Imports System.Xml

Namespace HelperLists

    ''' <summary>
    ''' Represents a collection user report parameter values for use in a combobox.
    ''' </summary>
    ''' <remarks>Should only be used as a child of <see cref="UserReportParamInfo">UserReportParamInfo</see>.</remarks>
    <Serializable()> _
    Public NotInheritable Class UserReportParamValueInfoList
        Inherits ReadOnlyListBase(Of UserReportParamValueInfoList, UserReportParamValueInfo)

#Region " Business Methods "

#End Region

#Region " Factory Methods "

        Friend Shared Function NewUserReportParamValueInfoList() As UserReportParamValueInfoList
            Return New UserReportParamValueInfoList
        End Function

        Friend Shared Function GetUserReportParamValueInfoList(ByVal node As XmlNode) As UserReportParamValueInfoList
            Return New UserReportParamValueInfoList(node)
        End Function


        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal node As XmlNode)
            ' require use of factory methods
            Fetch(node)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal node As XmlNode)

            If node.ChildNodes.Count < 1 OrElse node.ChildNodes(0).ChildNodes.Count < 1 Then
                Throw New Exception("Nepavyko perskaityti parametro verčių.")
            End If

            RaiseListChangedEvents = False
            IsReadOnly = False

            For Each childNode As XmlNode In node.ChildNodes(0).ChildNodes
                Add(UserReportParamValueInfo.GetUserReportParamValueInfo(childNode))
            Next

            IsReadOnly = True
            RaiseListChangedEvents = True

        End Sub

#End Region

    End Class

End Namespace