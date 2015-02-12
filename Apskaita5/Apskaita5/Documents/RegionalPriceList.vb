Namespace Documents

    <Serializable()> _
    Public Class RegionalPriceList
        Inherits BusinessListBase(Of RegionalPriceList, RegionalPrice)

#Region " Business Methods "

        Protected Overrides Function AddNewCore() As Object
            Dim NewItem As RegionalPrice = RegionalPrice.NewRegionalPrice
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

        Public Function GetLithPriceSale() As Double
            For Each i As RegionalPrice In Me
                If i.CurrencyCode.Trim.ToUpper = GetCurrentCompany.BaseCurrency Then Return i.ValuePerUnitSales
            Next
            Return 0
        End Function

        Public Function GetLithPricePurchase() As Double
            For Each i As RegionalPrice In Me
                If i.CurrencyCode.Trim.ToUpper = GetCurrentCompany.BaseCurrency Then Return i.ValuePerUnitPurchases
            Next
            Return 0
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function NewRegionalPriceList() As RegionalPriceList
            Return New RegionalPriceList
        End Function

        Friend Shared Function GetRegionalPriceList(ByVal ConcetanatedString As String) As RegionalPriceList
            Return New RegionalPriceList(ConcetanatedString)
        End Function

        Friend Shared Function GetRegionalPriceList(Of T As Documents.IRegionalDataObject) _
            (ByVal parentID As Integer) As RegionalPriceList
            Return New RegionalPriceList(GetType(T), parentID)
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
                    Add(RegionalPrice.GetRegionalPrice(dr.Trim))
            Next

            RaiseListChangedEvents = True

        End Sub

        Private Sub Fetch(ByVal parentType As Type, ByVal parentID As Integer)

            Dim myComm As New SQLCommand("FetchRegionalPriceInfoListByParent")
            If parentType Is GetType(Documents.Service) Then
                myComm.AddParam("?AA", 0)
            ElseIf parentType Is GetType(Goods.GoodsItem) Then
                myComm.AddParam("?AA", 1)
            Else
                Throw New NotImplementedException(String.Format("Type {0} is not implemented in method {1}.Fetch.", _
                    parentType.FullName, GetType(RegionalPriceInfoList).FullName))
            End If
            myComm.AddParam("?AB", parentID)

            Using myData As DataTable = myComm.Fetch

                RaiseListChangedEvents = False
                
                For Each dr As DataRow In myData.Rows
                    Add(RegionalPrice.GetRegionalPrice(dr))
                Next

                RaiseListChangedEvents = True

            End Using

        End Sub

        Friend Sub Update(ByVal parent As IRegionalDataObject)

            RaiseListChangedEvents = False

            For Each item As RegionalPrice In DeletedList
                If Not item.IsNew Then item.DeleteSelf()
            Next
            DeletedList.Clear()

            For Each item As RegionalPrice In Me
                If item.IsNew Then
                    item.Insert(parent)
                ElseIf item.IsDirty Then
                    item.Update(parent)
                End If
            Next

            RaiseListChangedEvents = True

        End Sub

        Friend Shared Sub Delete(ByVal ParentID As Integer, ByVal ParentType As Integer)

            Dim myComm As New SQLCommand("DeleteAllItemsInRegionalPrices")
            myComm.AddParam("?CD", ParentID)
            myComm.AddParam("?CT", ParentType)

            myComm.Execute()

        End Sub

#End Region

    End Class

End Namespace