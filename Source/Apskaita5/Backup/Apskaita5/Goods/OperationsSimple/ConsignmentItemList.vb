Namespace Goods

    ''' <summary>
    ''' Represents a collection of consignments value modification operations, 
    ''' that discard available (not already discarded) consignments 
    ''' and creates new modified consignments (with a reduced or increased value).
    ''' </summary>
    ''' <remarks>Used by goods operations that modify goods acquisition value,
    ''' e.g. <see cref="GoodsOperationAdditionalCosts">GoodsOperationAdditionalCosts</see>,
    ''' <see cref="GoodsOperationDiscount">GoodsOperationDiscount</see>.
    ''' Values are stored using a <see cref="ConsignmentPersistenceObject">ConsignmentPersistenceObject</see>
    ''' and a <see cref="ConsignmentDiscardPersistenceObject">ConsignmentDiscardPersistenceObject</see>.</remarks>
    <Serializable()> _
    Public NotInheritable Class ConsignmentItemList
        Inherits BusinessListBase(Of ConsignmentItemList, ConsignmentItem)

#Region " Business Methods "

        Public Function GetAllBrokenRules() As String
            Dim result As String = GetAllBrokenRulesForList(Me)
            Return result
        End Function

        Public Function GetAllWarnings() As String
            Dim result As String = GetAllWarningsForList(Me)
            Return result
        End Function

        Public Function HasWarnings() As Boolean
            For Each i As ConsignmentItem In Me
                If i.HasWarnings() Then Return True
            Next
            Return False
        End Function

        ''' <summary>
        ''' Gets a total sum of the <see cref="ConsignmentItem.TotalValueChange">
        ''' consignments value change</see> within the collection.
        ''' </summary>
        ''' <remarks></remarks>
        Public Function GetTotalValueChange() As Double
            Dim result As Double = 0
            For Each i As ConsignmentItem In Me
                result = CRound(result + i.TotalValueChange)
            Next
            Return result
        End Function

        ''' <summary>
        ''' Gets a maximum (latest) allowed date of the parent goods operation.
        ''' (a goods operation cannot modify a consignment that was acquired
        ''' later then the operation date)
        ''' </summary>
        ''' <remarks></remarks>
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

        ''' <summary>
        ''' Gets a new ConsignmentItemList instance for a new goods operation
        ''' (an operation that modifies goods acquisition value).
        ''' </summary>
        ''' <param name="goodsID">an <see cref="GoodsItem.ID">ID of the goods</see>
        ''' which consignments are modified by the parent goods operation</param>
        ''' <param name="warehouseID">an <see cref="Warehouse.ID">ID of the warehouse</see> 
        ''' where the replaceable (and modified) consignments are located</param>
        ''' <param name="changeIsNegative">whether the <see cref="ConsignmentItem.TotalValueChange">
        ''' TotalValueChange</see> decreases the value of the replaceable consignments</param>
        ''' <param name="accountingMethod">an <see cref="GoodsItem.AccountingMethod">
        ''' accounting method of the goods</see> which consignments are modified 
        ''' by the parent goods operation</param>
        ''' <remarks>New (available) consignment value change operations are fetched
        ''' irrespective of the acquisition and operations dates. A chronologic
        ''' order is secured by the use of <see cref="GetMaxDate">GetMaxDate</see> 
        ''' method that the parent goods operation must use to validate it's date.</remarks>
        Friend Shared Function NewConsignmentItemList(ByVal goodsID As Integer, _
            ByVal warehouseID As Integer, ByVal changeIsNegative As Boolean, _
            ByVal accountingMethod As GoodsAccountingMethod) As ConsignmentItemList
            Return New ConsignmentItemList(0, goodsID, warehouseID, _
                changeIsNegative, True, accountingMethod)
        End Function

        ''' <summary>
        ''' Gets a new ConsignmentItemList instance for an existing goods operation
        ''' (an operation that modifies goods acquisition value).
        ''' </summary>
        ''' <param name="operationID">an <see cref="OperationPersistenceObject.ID">
        ''' ID of the parent goods operation</see></param>
        ''' <param name="goodsID">an <see cref="GoodsItem.ID">ID of the goods</see>
        ''' which consignments are modified by the parent goods operation</param>
        ''' <param name="warehouseID">an <see cref="Warehouse.ID">ID of the warehouse</see> 
        ''' where the replaceable (and modified) consignments are located</param>
        ''' <param name="changeIsNegative">whether the <see cref="ConsignmentItem.TotalValueChange">
        ''' TotalValueChange</see> decreases the value of the replaceable consignments</param>
        ''' <param name="financialDataCanChange">whether the change of the operations 
        ''' financial data is restricted by the parent goods operation</param>
        ''' <param name="accountingMethod">an <see cref="GoodsItem.AccountingMethod">
        ''' accounting method of the goods</see> which consignments are modified 
        ''' by the parent goods operation</param>
        ''' <remarks>Both existing and new (available) consignment value change
        ''' operations are fetched.
        ''' New (available) consignment value change operations are fetched
        ''' irrespective of the acquisition and operations dates. A chronologic
        ''' order is secured by the use of <see cref="GetMaxDate">GetMaxDate</see> 
        ''' method that the parent goods operation must use to validate it's date.</remarks>
        Friend Shared Function GetConsignmentItemList(ByVal operationID As Integer, _
            ByVal goodsID As Integer, ByVal warehouseID As Integer, _
            ByVal changeIsNegative As Boolean, ByVal financialDataCanChange As Boolean, _
            ByVal accountingMethod As GoodsAccountingMethod) As ConsignmentItemList
            Return New ConsignmentItemList(operationID, goodsID, warehouseID, _
                changeIsNegative, financialDataCanChange, accountingMethod)
        End Function


        Private Sub New()
            ' require use of factory methods
            MarkAsChild()
            Me.AllowEdit = True
            Me.AllowNew = False
            Me.AllowRemove = False
        End Sub

        Private Sub New(ByVal operationID As Integer, ByVal goodsID As Integer, _
            ByVal warehouseID As Integer, ByVal changeIsNegative As Boolean, _
            ByVal financialDataCanChange As Boolean, ByVal accountingMethod As GoodsAccountingMethod)
            ' require use of factory methods
            MarkAsChild()
            Me.AllowEdit = financialDataCanChange
            Me.AllowNew = False
            Me.AllowRemove = False
            Fetch(operationID, goodsID, warehouseID, changeIsNegative, _
                financialDataCanChange, accountingMethod)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal operationID As Integer, ByVal goodsID As Integer, _
            ByVal warehouseID As Integer, ByVal changeIsNegative As Boolean, _
            ByVal financialDataCanChange As Boolean, ByVal accountingMethod As GoodsAccountingMethod)

            If accountingMethod = GoodsAccountingMethod.Periodic Then Exit Sub

            Dim myComm As New SQLCommand("FetchConsignmentItemList")
            myComm.AddParam("?OD", operationID)
            myComm.AddParam("?GD", goodsID)
            myComm.AddParam("?WD", warehouseID)

            Using myData As DataTable = myComm.Fetch

                RaiseListChangedEvents = False

                For Each dr As DataRow In myData.Rows
                    Add(ConsignmentItem.GetConsignmentItem(dr, changeIsNegative, financialDataCanChange))
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