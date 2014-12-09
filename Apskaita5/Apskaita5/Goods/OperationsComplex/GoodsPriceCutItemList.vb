Namespace Goods

    <Serializable()> _
    Public Class GoodsPriceCutItemList
        Inherits BusinessListBase(Of GoodsPriceCutItemList, GoodsPriceCutItem)

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


        Public Function ContainsGoods(ByVal GoodsID As Integer) As Boolean
            For Each i As GoodsPriceCutItem In Me
                If i.GoodsInfo.ID = GoodsID Then Return True
            Next
            For Each i As GoodsPriceCutItem In Me.DeletedList
                If Not i.IsNew AndAlso i.GoodsInfo.ID = GoodsID Then Return True
            Next
            Return False
        End Function

        Friend Sub RefreshValuesInWarehouse(ByVal values As GoodsPriceInWarehouseItemList)

            RaiseListChangedEvents = False

            For Each value As GoodsPriceInWarehouseItem In values
                For Each i As GoodsPriceCutItem In Me
                    If i.GoodsInfo.ID = value.GoodsID Then
                        i.RefreshValuesInWarehouse(value)
                        Exit For
                    End If
                Next
            Next

            RaiseListChangedEvents = True

            Me.ResetBindings()

        End Sub

        Friend Function GetGoodsPriceInWarehouseParams() As GoodsPriceInWarehouseParam()
            Dim result As New List(Of GoodsPriceInWarehouseParam)
            For Each i As GoodsPriceCutItem In Me
                result.Add(i.GetGoodsPriceInWarehouseParam)
            Next
            Return result.ToArray
        End Function

        Friend Sub ResetValuesInWarehouse()

            RaiseListChangedEvents = False

            For Each i As GoodsPriceCutItem In Me
                i.ResetValuesInWarehouse()
            Next

            RaiseListChangedEvents = True

            Me.ResetBindings()

        End Sub


        Friend Function GetLimitations() As OperationalLimitList()
            Dim result As New List(Of OperationalLimitList)
            For Each i As GoodsPriceCutItem In Me
                result.Add(i.OperationLimitations)
            Next
            Return result.ToArray
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function NewGoodsPriceCutItemList() As GoodsPriceCutItemList
            Return New GoodsPriceCutItemList
        End Function

        Friend Shared Function GetGoodsPriceCutItemList(ByVal myData As DataTable, _
            ByVal LimitationsDataSource As DataTable, ByVal parentValidator As IChronologicValidator) As GoodsPriceCutItemList
            Return New GoodsPriceCutItemList(myData, LimitationsDataSource, parentValidator)
        End Function


        Private Sub New()
            ' require use of factory methods
            MarkAsChild()
            Me.AllowEdit = True
            Me.AllowNew = False
            Me.AllowRemove = True
        End Sub

        Private Sub New(ByVal myData As DataTable, ByVal LimitationsDataSource As DataTable, _
            ByVal parentValidator As IChronologicValidator)
            ' require use of factory methods
            MarkAsChild()
            Me.AllowEdit = True
            Me.AllowNew = False
            Me.AllowRemove = parentValidator.FinancialDataCanChange
            Fetch(myData, LimitationsDataSource, parentValidator)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal myData As DataTable, ByVal LimitationsDataSource As DataTable, _
            ByVal parentValidator As IChronologicValidator)

            RaiseListChangedEvents = False

            For Each dr As DataRow In myData.Rows
                Add(GoodsPriceCutItem.GetGoodsPriceCutItem(dr, LimitationsDataSource, parentValidator))
            Next

            RaiseListChangedEvents = True

        End Sub

        Friend Sub Update(ByVal parent As GoodsComplexOperationPriceCut)

            RaiseListChangedEvents = False

            For Each item As GoodsPriceCutItem In DeletedList
                If Not item.IsNew Then item.DeleteSelf()
            Next
            DeletedList.Clear()

            For Each item As GoodsPriceCutItem In Me
                If item.IsNew OrElse item.IsDirty Then
                    item.Update(parent)
                End If
            Next

            RaiseListChangedEvents = True

        End Sub


        Friend Function CheckIfCanUpdate(ByVal LimitationsDataSource As DataTable, _
            ByVal ThrowOnInvalid As Boolean, ByVal nDate As Date, _
            ByVal parentValidator As IChronologicValidator) As String

            Dim result As String = ""

            For Each i As GoodsPriceCutItem In Me
                result = AddWithNewLine(result, i.CheckIfCanUpdate(LimitationsDataSource, _
                    ThrowOnInvalid, nDate, parentValidator), False)
            Next

            For Each i As GoodsPriceCutItem In Me.DeletedList
                result = AddWithNewLine(result, i.CheckIfCanDelete(LimitationsDataSource, _
                    ThrowOnInvalid, parentValidator), False)
            Next

            Return result

        End Function

        Friend Sub ReloadLimitations(ByVal LimitationsDataSource As DataTable, _
            ByVal parentValidator As IChronologicValidator)
            RaiseListChangedEvents = False
            For Each i As GoodsPriceCutItem In Me
                If Not i.IsNew Then i.ReloadLimitations(LimitationsDataSource, parentValidator)
            Next
            RaiseListChangedEvents = True
        End Sub

#End Region

    End Class

End Namespace