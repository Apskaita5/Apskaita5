Namespace Goods

    <Serializable()> _
    Public Class ConsignmentItemList
        Inherits BusinessListBase(Of ConsignmentItemList, ConsignmentItem)

#Region " Business Methods "

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

        Public Function GetTotalValueChange() As Double
            Dim result As Double = 0
            For Each i As ConsignmentItem In Me
                result = CRound(result + i.TotalValueChange)
            Next
            Return result
        End Function

        Public Function GetMaxDate() As Date

            Dim result As Date = Date.MinValue

            For Each i As ConsignmentItem In Me
                If i.TotalValueChange > 0 AndAlso i.AcquisitionDate.Date > result.Date Then _
                    result = i.AcquisitionDate.Date
            Next

            Return result

        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function NewConsignmentItemList(ByVal GoodsID As Integer, _
            ByVal WarehouseID As Integer, ByVal ChangeIsNegative As Boolean, _
            ByVal FinancialDataCanChange As Boolean) As ConsignmentItemList
            Return New ConsignmentItemList(0, GoodsID, WarehouseID, _
                ChangeIsNegative, FinancialDataCanChange)
        End Function

        Friend Shared Function GetConsignmentItemList(ByVal OperationID As Integer, _
            ByVal GoodsID As Integer, ByVal WarehouseID As Integer, _
            ByVal ChangeIsNegative As Boolean, ByVal FinancialDataCanChange As Boolean) As ConsignmentItemList
            Return New ConsignmentItemList(OperationID, GoodsID, WarehouseID, _
                ChangeIsNegative, FinancialDataCanChange)
        End Function


        Private Sub New()
            ' require use of factory methods
            MarkAsChild()
            Me.AllowEdit = True
            Me.AllowNew = False
            Me.AllowRemove = False
        End Sub

        Private Sub New(ByVal OperationID As Integer, ByVal GoodsID As Integer, _
            ByVal WarehouseID As Integer, ByVal ChangeIsNegative As Boolean, _
            ByVal FinancialDataCanChange As Boolean)
            ' require use of factory methods
            MarkAsChild()
            Me.AllowEdit = FinancialDataCanChange
            Me.AllowNew = False
            Me.AllowRemove = False
            Fetch(OperationID, GoodsID, WarehouseID, ChangeIsNegative, FinancialDataCanChange)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal OperationID As Integer, ByVal GoodsID As Integer, _
            ByVal WarehouseID As Integer, ByVal ChangeIsNegative As Boolean, _
            ByVal FinancialDataCanChange As Boolean)

            Dim myComm As New SQLCommand("FetchConsignmentItemList")
            myComm.AddParam("?OD", OperationID)
            myComm.AddParam("?GD", GoodsID)
            myComm.AddParam("?WD", WarehouseID)

            Using myData As DataTable = myComm.Fetch

                RaiseListChangedEvents = False

                For Each dr As DataRow In myData.Rows
                    Add(ConsignmentItem.GetConsignmentItem(dr, ChangeIsNegative, FinancialDataCanChange))
                Next

                RaiseListChangedEvents = True

            End Using

        End Sub

        Friend Sub Update(ByVal parentID As Integer)

            RaiseListChangedEvents = False

            DeletedList.Clear()

            For Each item As ConsignmentItem In Me
                If item.IsNew AndAlso item.TotalValueChange > 0 Then
                    item.Insert(parentID)
                ElseIf Not item.IsNew AndAlso item.IsDirty AndAlso item.TotalValueChange > 0 Then
                    item.Update(parentID)
                ElseIf Not item.IsNew AndAlso item.IsDirty AndAlso Not item.TotalValueChange > 0 Then
                    item.DeleteSelf()
                End If
            Next

            RaiseListChangedEvents = True

        End Sub

#End Region

    End Class

End Namespace