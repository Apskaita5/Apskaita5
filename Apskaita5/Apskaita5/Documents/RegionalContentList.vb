Namespace Documents

    ''' <summary>
    ''' Represents a list of description info for a particular <see cref="IRegionalDataObject">
    ''' regionalized object</see> for particular languages.
    ''' </summary>
    ''' <remarks>Should be only used as a child of <see cref="IRegionalDataObject">IRegionalDataObject</see>.
    ''' Values are stored in the database table regionalcontents.</remarks>
    <Serializable()> _
    Public NotInheritable Class RegionalContentList
        Inherits BusinessListBase(Of RegionalContentList, RegionalContent)

#Region " Business Methods "

        Protected Overrides Function AddNewCore() As Object
            Dim newItem As RegionalContent = RegionalContent.NewRegionalContent
            Me.Add(newItem)
            Return newItem
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

        Public Function HasWarnings() As Boolean
            For Each i As RegionalContent In Me
                If i.BrokenRulesCollection.WarningCount > 0 Then Return True
            Next
            Return False
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function NewRegionalContentList() As RegionalContentList
            Return New RegionalContentList
        End Function

        Friend Shared Function GetRegionalContentList(ByVal parent As IRegionalDataObject) As RegionalContentList
            Return New RegionalContentList(parent)
        End Function


        Private Sub New()
            ' require use of factory methods
            MarkAsChild()
            Me.AllowEdit = True
            Me.AllowNew = True
            Me.AllowRemove = True
        End Sub

        Private Sub New(ByVal parent As IRegionalDataObject)
            ' require use of factory methods
            MarkAsChild()
            Me.AllowEdit = True
            Me.AllowNew = True
            Me.AllowRemove = True
            Fetch(parent)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal parent As IRegionalDataObject)

            Dim myComm As New SQLCommand("FetchRegionalContentInfoListByParent")
            myComm.AddParam("?AA", Utilities.ConvertDatabaseID(parent.RegionalObjectType))
            myComm.AddParam("?AB", parent.RegionalObjectID)

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

        Friend Shared Sub Delete(ByVal parentID As Integer, ByVal parentType As RegionalizedObjectType)

            Dim myComm As New SQLCommand("DeleteAllItemsInRegionalContents")
            myComm.AddParam("?CD", parentID)
            myComm.AddParam("?CT", Utilities.ConvertDatabaseID(parentType))
            myComm.Execute()

        End Sub

#End Region

    End Class

End Namespace