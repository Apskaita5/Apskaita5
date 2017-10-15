Imports ApskaitaObjects.Documents

Namespace HelperLists

    ''' <summary>
    ''' Represents a dictionary of description info for regionalized objects.
    ''' </summary>
    ''' <remarks>Values are stored in the database table regionalcontents.</remarks>
    <Serializable()> _
    Public NotInheritable Class RegionalContentEntryList
        Inherits ReadOnlyListBase(Of RegionalContentEntryList, RegionalContentEntry)

#Region " Business Methods "

        Friend Function GetRegionalContentEntry(ByVal objectType As RegionalizedObjectType, _
            ByVal objectID As Integer, ByVal languageCode As String) As RegionalContentEntry

            If Not objectID > 0 Then
                Return RegionalContentEntry.NewRegionalContentEntry()
            End If

            Dim baseCurrency As String = GetCurrentCompany().BaseCurrency

            For Each i As RegionalContentEntry In Me
                If i.ObjectType = objectType AndAlso i.ObjectID = objectID AndAlso _
                    LanguagesEquals(i.LanguageCode, languageCode, LanguageCodeLith) Then
                    Return i
                End If
            Next

            Return RegionalContentEntry.NewRegionalContentEntry()

        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets a RegionalContentEntryList from database.
        ''' </summary>
        ''' <remarks></remarks>
        Friend Shared Function GetRegionalContentEntryList() As RegionalContentEntryList
            Return New RegionalContentEntryList(True)
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

            Dim myComm As New SQLCommand("FetchRegionalContentEntryList")

            Using myData As DataTable = myComm.Fetch()
                For Each dr As DataRow In myData.Rows
                    Add(RegionalContentEntry.GetRegionalContentEntry(dr))
                Next
            End Using

            IsReadOnly = True
            RaiseListChangedEvents = True

        End Sub

#End Region

    End Class

End Namespace