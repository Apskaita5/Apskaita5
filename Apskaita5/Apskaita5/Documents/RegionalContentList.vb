Namespace Documents

    <Serializable()> _
    Public Class RegionalContentList
        Inherits BusinessListBase(Of RegionalContentList, RegionalContent)

#Region " Business Methods "

        Protected Overrides Function AddNewCore() As Object
            Dim NewItem As RegionalContent = RegionalContent.NewRegionalContent
            Me.Add(NewItem)
            Return NewItem
        End Function

        Public Function GetAllBrokenRules() As String
            Dim result As String = GetAllBrokenRulesForList(Me)

            'Dim GeneralErrorString As String = ""
            'SomeGeneralValidationSub(GeneralErrorString)
            'AddWithNewLine(result, GeneralErrorString, False)

            Return result

        End Function

        Public Function GetAllWarnings() As String
            Dim result As String = GetAllWarningsForList(Me)
            'Dim GeneralErrorString As String = ""
            'SomeGeneralValidationSub(GeneralErrorString)
            'AddWithNewLine(result, GeneralErrorString, False)

            Return result
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function NewRegionalContentList() As RegionalContentList
            Return New RegionalContentList
        End Function

        Friend Shared Function GetRegionalContentList(ByVal ConcetanatedString As String) As RegionalContentList
            Return New RegionalContentList(ConcetanatedString)
        End Function

        Friend Shared Function GetRegionalContentList(Of T As Documents.IRegionalDataObject) _
            (ByVal parentID As Integer) As RegionalContentList
            Return New RegionalContentList(GetType(T), parentID)
        End Function


        Private Sub New()
            ' require use of factory methods
            MarkAsChild()
            Me.AllowEdit = True
            Me.AllowNew = True
            Me.AllowRemove = True
        End Sub

        Private Sub New(ByVal ConcetanatedString As String)
            ' require use of factory methods
            MarkAsChild()
            Me.AllowEdit = True
            Me.AllowNew = True
            Me.AllowRemove = True
            Fetch(ConcetanatedString)
        End Sub

        Private Sub New(ByVal parentType As Type, ByVal parentID As Integer)
            ' require use of factory methods
            MarkAsChild()
            Me.AllowEdit = True
            Me.AllowNew = True
            Me.AllowRemove = True
            Fetch(parentType, parentID)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal ConcetanatedString As String)

            RaiseListChangedEvents = False

            For Each dr As String In ConcetanatedString.Split(New String() {"@*#@"}, _
                StringSplitOptions.RemoveEmptyEntries)
                If Not dr Is Nothing AndAlso Not String.IsNullOrEmpty(dr.Trim) Then _
                    Add(RegionalContent.GetRegionalContent(dr.Trim))
            Next

            RaiseListChangedEvents = True

        End Sub

        Private Sub Fetch(ByVal parentType As Type, ByVal parentID As Integer)

            Dim myComm As New SQLCommand("FetchRegionalContentInfoListByParent")
            If parentType Is GetType(Documents.Service) Then
                myComm.AddParam("?AA", 0)
            ElseIf parentType Is GetType(Goods.GoodsItem) Then
                myComm.AddParam("?AA", 1)
            Else
                Throw New NotImplementedException(String.Format("Type {0} is not implemented in method {1}.GetRegionalPriceInfoListDataTable.", _
                    parentType.FullName, GetType(RegionalPriceInfoList).FullName))
            End If
            myComm.AddParam("?AB", parentID)

            Using myData As DataTable = myComm.Fetch

                RaiseListChangedEvents = False

                For Each dr As DataRow In myData.Rows
                    Add(RegionalContent.GetRegionalContent(dr))
                Next

                RaiseListChangedEvents = True

            End Using

        End Sub

        Friend Sub Update(ByVal parent As IRegionalDataObject)

            RaiseListChangedEvents = False

            For Each item As RegionalContent In DeletedList
                If Not item.IsNew Then item.DeleteSelf()
            Next
            DeletedList.Clear()

            For Each item As RegionalContent In Me
                If item.IsNew Then
                    item.Insert(parent)
                ElseIf item.IsDirty Then
                    item.Update(parent)
                End If
            Next

            RaiseListChangedEvents = True

        End Sub

        Friend Shared Sub Delete(ByVal ParentID As Integer, ByVal ParentType As Integer)

            Dim myComm As New SQLCommand("DeleteAllItemsInRegionalContents")
            myComm.AddParam("?CD", ParentID)
            myComm.AddParam("?CT", ParentType)
            myComm.Execute()

        End Sub

#End Region

    End Class

End Namespace