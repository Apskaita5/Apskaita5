Imports ApskaitaObjects.General
Namespace Settings

    ''' <summary>
    ''' Represents a list of <see cref="General.CompanyAccount">company's default account</see> value objects.
    ''' </summary>
    ''' <remarks>Exists a single instance per company.</remarks>
    <Serializable()> _
    Public Class CompanyAccountInfoList
        Inherits ReadOnlyListBase(Of CompanyAccountInfoList, CompanyAccountInfo)

#Region " Business Methods "

        ''' <summary>
        ''' Gets a default <see cref="General.Account.ID">account</see> of the specified type.
        ''' Returns 0 if the requested type is not present in the list.
        ''' </summary>
        ''' <param name="ofType">Type of the default account to get.</param>
        ''' <remarks></remarks>
        Public Function GetAccount(ByVal ofType As DefaultAccountType) As Long
            For Each i As CompanyAccountInfo In Me
                If i.Type = ofType Then Return i.Value
            Next
            Return 0
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function GetCompanyAccountInfoList() As CompanyAccountInfoList
            Return New CompanyAccountInfoList()
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