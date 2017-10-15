Namespace Settings

    ''' <summary>
    ''' Represents a collection of <see cref="General.CompanyAccount">company's account</see> value objects.
    ''' </summary>
    ''' <remarks>Should only be used as a child of <see cref="CompanyInfo">CompanyInfo</see>.
    ''' Values are stored in the database table companyrates.</remarks>
    <Serializable()> _
    Public NotInheritable Class CompanyRateInfoList
        Inherits ReadOnlyListBase(Of CompanyRateInfoList, CompanyRateInfo)

#Region " Business Methods "

        ''' <summary>
        ''' Gets a default rate of the requested type.
        ''' Returnes 0 if the requested type is not available in the list.
        ''' </summary>
        ''' <param name="rateTypeToGet">Type of the default rate to get.</param>
        ''' <remarks></remarks>
        Public Function GetRate(ByVal rateTypeToGet As General.DefaultRateType) As Double
            For Each i As CompanyRateInfo In Me
                If i.Type = rateTypeToGet Then Return i.Value
            Next
            Return 0
        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function GetCompanyRateInfoList() As CompanyRateInfoList
            Return New CompanyRateInfoList()
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