Namespace Goods

    ''' <summary>
    ''' Represents a list of production template (""recipe"") costs items 
    ''' for a certain production template (calculation).
    ''' </summary>
    ''' <remarks>Should only be used as a child of <see cref="ProductionCalculation">ProductionCalculation</see>.
    ''' Values are stored in the database table kalkuliac_d.</remarks>
    <Serializable()> _
    Public NotInheritable Class ProductionCostItemList
        Inherits BusinessListBase(Of ProductionCostItemList, ProductionCostItem)

#Region " Business Methods "

        Protected Overrides Function AddNewCore() As Object
            Dim newItem As ProductionCostItem = ProductionCostItem.NewProductionCostItem
            Me.Add(newItem)
            Return newItem
        End Function

        Public Function GetAllBrokenRules() As String
            Dim result As String = GetAllBrokenRulesForList(Me)
            Return result
        End Function

        Public Function GetAllWarnings() As String
            Dim result As String = GetAllWarningsForList(Me)
            Return result
        End Function

        Public Function HasWarnings() As Boolean
            For Each i As ProductionCostItem In Me
                If i.HasWarnings() Then Return True
            Next
            Return False
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function NewProductionCostItemList() As ProductionCostItemList
            Return New ProductionCostItemList
        End Function

        Friend Shared Function GetProductionCostItemList(ByVal myData As DataTable) As ProductionCostItemList
            Return New ProductionCostItemList(myData)
        End Function


        Private Sub New()
            ' require use of factory methods
            MarkAsChild()
            Me.AllowEdit = True
            Me.AllowNew = True
            Me.AllowRemove = True
        End Sub

        Private Sub New(ByVal myData As DataTable)
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
                If Utilities.ConvertDatabaseCharID(Of ProductionComponentType) _
                    (CStrSafe(dr.Item(0))) = ProductionComponentType.Costs Then _
                    Add(ProductionCostItem.GetProductionCostItem(dr))
            Next

            RaiseListChangedEvents = True

        End Sub

        Friend Sub Update(ByVal parent As ProductionCalculation)

            RaiseListChangedEvents = False

            For Each item As ProductionCostItem In DeletedList
                If Not item.IsNew Then item.DeleteSelf()
            Next
            DeletedList.Clear()

            For Each item As ProductionCostItem In Me
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