Imports ApskaitaObjects.Documents

Namespace HelperLists

    ''' <summary>
    ''' Represents a dictionary of price info for regionalized objects.
    ''' </summary>
    ''' <remarks>Values are stored in the database table regionalprices.</remarks>
    <Serializable()> _
    Public NotInheritable Class RegionalPriceEntryList
        Inherits ReadOnlyListBase(Of RegionalPriceEntryList, RegionalPriceEntry)

#Region " Business Methods "

        Friend Function GetRegionalPriceEntry(ByVal objectType As RegionalizedObjectType, _
            ByVal objectID As Integer, ByVal currencyCode As String) As RegionalPriceEntry

            If Not objectID > 0 Then
                Return RegionalPriceEntry.NewRegionalPriceEntry()
            End If

            Dim baseCurrency As String = GetCurrentCompany().BaseCurrency

            For Each i As RegionalPriceEntry In Me
                If i.ObjectType = objectType AndAlso i.ObjectID = objectID AndAlso _
                    CurrenciesEquals(i.CurrencyCode, currencyCode, baseCurrency) Then
                    Return i
                End If
            Next

            Return RegionalPriceEntry.NewRegionalPriceEntry()

        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets a GetRegionalPriceEntryList from database.
        ''' </summary>
        ''' <remarks></remarks>
        Friend Shared Function GetRegionalPriceEntryList() As RegionalPriceEntryList
            Return New RegionalPriceEntryList(True)
        End Function


        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal doFetch As Boolean)
            ' require use of factory methods
            If doFetch Then Fetch()
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch()

            RaiseListChangedEvents = False
            IsReadOnly = False

            Dim myComm As New SQLCommand("FetchRegionalPriceEntryList")

            Using myData As DataTable = myComm.Fetch()
                For Each dr As DataRow In myData.Rows
                    Add(RegionalPriceEntry.GetRegionalPriceEntry(dr))
                Next
            End Using

            IsReadOnly = True
            RaiseListChangedEvents = True

        End Sub

#End Region

    End Class

End Namespace