Namespace Settings

    <Serializable()> _
    Public Class CompanyRateInfoList
        Inherits ReadOnlyListBase(Of CompanyRateInfoList, CompanyRateInfo)

#Region " Business Methods "

        Public Function GetRate(ByVal RateTypeToGet As RateType) As Double
            For Each i As CompanyRateInfo In Me
                If i.Type = RateTypeToGet Then Return i.Value
            Next
            Return 0
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function GetCompanyRateInfoList() As CompanyRateInfoList
            Dim result As CompanyRateInfoList = New CompanyRateInfoList()
            Return result
        End Function

        Private Sub New()
            ' require use of factory methods
            Fetch()
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch()

            Dim myComm As New SQLCommand("FetchCompanyRateList")

            Using myData As DataTable = myComm.Fetch

                RaiseListChangedEvents = False
                IsReadOnly = False

                For Each dr As DataRow In myData.Rows
                    Add(CompanyRateInfo.GetCompanyRateInfo(dr))
                Next

                IsReadOnly = True
                RaiseListChangedEvents = True

            End Using

        End Sub

#End Region

    End Class

End Namespace