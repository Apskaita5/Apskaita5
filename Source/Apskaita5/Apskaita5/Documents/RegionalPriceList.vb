Namespace Documents

    ''' <summary>
    ''' Represents list of price info for a particular regionalized object for particular currencies.
    ''' </summary>
    ''' <remarks>Should be only used as a child of <see cref="IRegionalDataObject">IRegionalDataObject</see>
    ''' Values are stored in the database table regionalprices.</remarks>
    <Serializable()> _
    Public NotInheritable Class RegionalPriceList
        Inherits BusinessListBase(Of RegionalPriceList, RegionalPrice)

#Region " Business Methods "

        Protected Overrides Function AddNewCore() As Object
            Dim newItem As RegionalPrice = RegionalPrice.NewRegionalPrice
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
            For Each i As RegionalPrice In Me
                If i.BrokenRulesCollection.WarningCount > 0 Then Return True
            Next
            Return False
        End Function


        Public Function GetPriceSaleBaseCurrency() As Double
            Dim baseCurrency As String = GetCurrentCompany.BaseCurrency
            For Each i As RegionalPrice In Me
                If IsBaseCurrency(i.CurrencyCode, baseCurrency) Then Return i.ValuePerUnitSales
            Next
            Return 0.0
        End Function

        Public Function GetPricePurchaseBaseCurrency() As Double
            Dim baseCurrency As String = GetCurrentCompany.BaseCurrency
            For Each i As RegionalPrice In Me
                If IsBaseCurrency(i.CurrencyCode, baseCurrency) Then Return i.ValuePerUnitPurchases
            Next
            Return 0.0
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function NewRegionalPriceList() As RegionalPriceList
            Return New RegionalPriceList
        End Function

        Friend Shared Function GetRegionalPriceList(ByVal parent As IRegionalDataObject) As RegionalPriceList
            Return New RegionalPriceList(parent)
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

            Dim myComm As New SQLCommand("FetchRegionalPriceInfoListByParent")
            myComm.AddParam("?AA", Utilities.ConvertDatabaseID(parent.RegionalObjectType))
            myComm.AddParam("?AB", parent.RegionalObjectID)

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

        Friend Shared Sub Delete(ByVal parentID As Integer, ByVal parentType As RegionalizedObjectType)

            Dim myComm As New SQLCommand("DeleteAllItemsInRegionalPrices")
            myComm.AddParam("?CD", parentID)
            myComm.AddParam("?CT", Utilities.ConvertDatabaseID(parentType))

            myComm.Execute()

        End Sub

#End Region

    End Class

End Namespace