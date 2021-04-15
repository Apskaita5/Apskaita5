Namespace General

    ''' <summary>
    '''  Represents a list of shares acquisition (sub) operations of a particular shares operation.
    ''' </summary>
    ''' <remarks>Should only be used as a child of a <see cref="SharesOperation">SharesOperation</see></remarks>
    <Serializable()>
    Public NotInheritable Class SharesAcquisitionList
        Inherits BusinessListBase(Of SharesAcquisitionList, SharesAcquisition)

#Region " Business Methods "

        Protected Overrides Function AddNewCore() As Object
            Dim NewItem As SharesAcquisition = SharesAcquisition.NewSharesAcquisition
            Me.Add(NewItem)
            Return NewItem
        End Function


        Public Function GetAllBrokenRules() As String
            If Me.IsValid Then Return ""
            Return GetAllBrokenRulesForList(Me)
        End Function

        Public Function GetAllWarnings() As String
            If Not HasWarnings() Then Return ""
            Return GetAllWarningsForList(Me)
        End Function

        Public Function HasWarnings() As Boolean
            For Each i As SharesAcquisition In Me
                If i.BrokenRulesCollection.WarningCount > 0 Then Return True
            Next
            Return False
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function NewSharesAcquisitionList() As SharesAcquisitionList
            Return New SharesAcquisitionList
        End Function

        Friend Shared Function GetSharesAcquisitionList(myData As DataTable) As SharesAcquisitionList
            Return New SharesAcquisitionList(myData)
        End Function

        Private Sub New()
            ' require use of factory methods
            MarkAsChild()
            Me.AllowEdit = True
            Me.AllowNew = True
            Me.AllowRemove = True
        End Sub

        Private Sub New(myData As DataTable)
            ' require use of factory methods
            MarkAsChild()
            Me.AllowEdit = True
            Me.AllowNew = True
            Me.AllowRemove = True
            Fetch(myData)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal myData As DataTable)

            RaiseListChangedEvents = False

            For Each dr As DataRow In myData.Rows
                If ConvertDatabaseID(Of SharesOperationType)(CIntSafe(dr.Item(0), 0)) = SharesOperationType.Acquisition Then _
                    Add(SharesAcquisition.GetSharesAcquisition(dr))
            Next

            RaiseListChangedEvents = True

        End Sub

        Friend Sub Update(ByVal parent As SharesOperation)

            RaiseListChangedEvents = False

            For Each item As SharesAcquisition In DeletedList
                If Not item.IsNew Then item.DeleteSelf()
            Next
            DeletedList.Clear()

            For Each item As SharesAcquisition In Me
                If item.IsNew Then
                    item.Insert(parent)
                ElseIf item.IsDirty Then
                    item.Update(parent)
                End If
            Next

            RaiseListChangedEvents = True

        End Sub

#End Region

    End Class

End Namespace
