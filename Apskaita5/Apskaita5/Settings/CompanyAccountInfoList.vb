Imports ApskaitaObjects.General
Namespace Settings

    <Serializable()> _
    Public Class CompanyAccountInfoList
        Inherits ReadOnlyListBase(Of CompanyAccountInfoList, CompanyAccountInfo)

#Region " Business Methods "

        Public Function GetAccount(ByVal OfType As DefaultAccountType) As Long
            For Each i As CompanyAccountInfo In Me
                If i.Type = OfType Then Return i.Value
            Next
            Return 0
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function GetCompanyAccountInfoList() As CompanyAccountInfoList
            Dim result As CompanyAccountInfoList = New CompanyAccountInfoList()
            Return result
        End Function

        Private Sub New()
            ' require use of factory methods
            Fetch()
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch()

            Dim myComm As New SQLCommand("FetchCompanyAccountList")

            Using myData As DataTable = myComm.Fetch

                RaiseListChangedEvents = False
                IsReadOnly = False

                For Each dr As DataRow In myData.Rows
                    Add(CompanyAccountInfo.GetCompanyAccountInfo(dr))
                Next

                IsReadOnly = True
                RaiseListChangedEvents = True

            End Using

        End Sub

#End Region

    End Class

End Namespace