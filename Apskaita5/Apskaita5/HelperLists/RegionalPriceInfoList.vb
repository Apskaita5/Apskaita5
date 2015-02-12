Namespace HelperLists

    <Serializable()> _
    Public Class RegionalPriceInfoList
        Inherits ReadOnlyListBase(Of RegionalPriceInfoList, RegionalPriceInfo)

#Region " Business Methods "

        Public Function GetRegionalPrice(ByVal CurrencyCode As String, _
            ByVal ForPurchases As Boolean) As Double
            For Each p As RegionalPriceInfo In Me
                If p.CurrencyCode.Trim.ToUpper = CurrencyCode.Trim.ToUpper Then
                    If ForPurchases Then
                        Return p.ValuePerUnitPurchases
                    Else
                        Return p.ValuePerUnitSales
                    End If
                End If
            Next
            Return 0
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function NewRegionalPriceInfoList() As RegionalPriceInfoList
            Return New RegionalPriceInfoList
        End Function

        Friend Shared Function GetRegionalPriceInfoList(ByVal ConcetanatedString As String) As RegionalPriceInfoList
            Return New RegionalPriceInfoList(ConcetanatedString)
        End Function

        Friend Shared Function GetRegionalPriceInfoList(Of T As Documents.IRegionalDataObject) _
            (ByVal parentID As Integer) As RegionalPriceInfoList
            Return New RegionalPriceInfoList(GetType(T), parentID)
        End Function

        Friend Shared Function GetRegionalPriceInfoList(ByVal parentID As Integer, _
            ByVal myData As DataTable) As RegionalPriceInfoList
            Return New RegionalPriceInfoList(parentID, myData)
        End Function

        Friend Shared Function GetRegionalPriceInfoListDataTable(Of T As Documents.IRegionalDataObject)() As DataTable

            Dim myComm As New SQLCommand("FetchRegionalPriceInfoListByType")
            If GetType(T) Is GetType(Documents.Service) Then
                myComm.AddParam("?AA", 0)
            ElseIf GetType(T) Is GetType(Goods.GoodsItem) Then
                myComm.AddParam("?AA", 1)
            Else
                Throw New NotImplementedException(String.Format("Type {0} is not implemented in method {1}.GetRegionalPriceInfoListDataTable.", _
                    GetType(T).FullName, GetType(RegionalPriceInfoList).FullName))
            End If

            Dim result As DataTable = myComm.Fetch

            Return result

        End Function


        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal ConcetanatedString As String)
            ' require use of factory methods
            Fetch(ConcetanatedString)
        End Sub

        Private Sub New(ByVal parentType As Type, ByVal parentID As Integer)
            ' require use of factory methods
            Fetch(parentType, parentID)
        End Sub

        Private Sub New(ByVal parentID As Integer, ByVal myData As DataTable)
            ' require use of factory methods
            Fetch(parentID, myData)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal ConcetanatedString As String)

            RaiseListChangedEvents = False
            IsReadOnly = False

            For Each dr As String In ConcetanatedString.Split(New String() {"@*#@"}, _
                StringSplitOptions.RemoveEmptyEntries)
                If Not dr Is Nothing AndAlso Not String.IsNullOrEmpty(dr.Trim) Then _
                    Add(RegionalPriceInfo.GetRegionalPriceInfo(dr.Trim))
            Next

            IsReadOnly = True
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
                IsReadOnly = False

                For Each dr As DataRow In myData.Rows
                    Add(RegionalPriceInfo.GetRegionalPriceInfo(dr))
                Next

                IsReadOnly = True
                RaiseListChangedEvents = True

            End Using

        End Sub

        Private Sub Fetch(ByVal parentID As Integer, ByVal myData As DataTable)

            If myData Is Nothing OrElse Not parentID > 0 Then Exit Sub

            RaiseListChangedEvents = False
            IsReadOnly = False

            For Each dr As DataRow In myData.Rows
                If CIntSafe(dr.Item(0), 0) = parentID Then
                    Add(RegionalPriceInfo.GetRegionalPriceInfo(dr))
                End If
            Next

            IsReadOnly = True
            RaiseListChangedEvents = True

        End Sub

#End Region

    End Class

End Namespace