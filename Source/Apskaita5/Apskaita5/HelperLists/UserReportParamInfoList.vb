Namespace HelperLists

    ''' <summary>
    ''' Represents a list of user report parameters for a certain user report
    ''' (an *.rdl file).
    ''' </summary>
    ''' <remarks>Should only be used as a child of <see cref="UserReportInfo">UserReportInfo</see>.</remarks>
    <Serializable()> _
    Public NotInheritable Class UserReportParamInfoList
        Inherits ReadOnlyListBase(Of UserReportParamInfoList, UserReportParamInfo)

#Region " Business Methods "

#End Region

#Region " Factory Methods "

        Friend Shared Function GetUserReportParamInfoList(ByVal node As Xml.XmlNode) As UserReportParamInfoList
            Return New UserReportParamInfoList(node)
        End Function


        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal node As Xml.XmlNode)
            ' require use of factory methods
            Fetch(node)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal node As Xml.XmlNode)

            If node Is Nothing Then Exit Sub

            RaiseListChangedEvents = False
            IsReadOnly = False

            For Each childNode As Xml.XmlNode In node.ChildNodes
                Add(UserReportParamInfo.GetUserReportParamInfo(childNode))
            Next

            IsReadOnly = True
            RaiseListChangedEvents = True

        End Sub

#End Region

    End Class

End Namespace
