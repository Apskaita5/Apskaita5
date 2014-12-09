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
            Dim result As RegionalPriceInfoList = New RegionalPriceInfoList
            Return result
        End Function

        Friend Shared Function GetRegionalPriceInfoList(ByVal ConcetanatedString As String) As RegionalPriceInfoList
            Dim result As RegionalPriceInfoList = New RegionalPriceInfoList(ConcetanatedString)
            Return result
        End Function

        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal ConcetanatedString As String)
            ' require use of factory methods
            Fetch(ConcetanatedString)
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

#End Region

    End Class

End Namespace