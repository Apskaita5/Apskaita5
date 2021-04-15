Namespace ActiveReports

    ''' <summary>
    ''' Represents a company's shareholders report.
    ''' Contains information about company's shareholders as of the report date.
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()>
    Public NotInheritable Class ShareHolderInfoList
        Inherits ReadOnlyListBase(Of ShareHolderInfoList, ShareHolderInfo)

#Region " Business Methods "

        Private _AsOfDate As Date = Today


        ''' <summary>
        ''' Gets the date of the report.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property AsOfDate() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _AsOfDate
            End Get
        End Property

#End Region

#Region " Authorization Rules "

        Public Shared Function CanGetObject() As Boolean
            Return ApplicationContext.User.IsInRole("Reports.ShareHolderInfoList1")
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets a new ShareHolderInfoList report instance.
        ''' </summary>
        ''' <param name="asOfDate">the date to fetch the shareholder's data for</param>
        ''' <returns></returns>
        Public Shared Function GetShareHolderInfoList(asOfDate As Date) As ShareHolderInfoList
            Return DataPortal.Fetch(Of ShareHolderInfoList)(New Criteria(asOfDate))
        End Function

        Private Sub New()
            ' require use of factory methods
        End Sub

#End Region

#Region " Data Access "

        <Serializable()>
        Private Class Criteria
            Private _AsOfDate As Date
            Public ReadOnly Property AsOfDate As Date
                Get
                    Return _AsOfDate
                End Get
            End Property
            Public Sub New(ByVal asOfDate As Date)
                _AsOfDate = asOfDate
            End Sub
        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)

            Dim myComm As New SQLCommand("FetchShareHolderInfoList")
            myComm.AddParam("?DT", criteria.AsOfDate.Date)

            Using myData As DataTable = myComm.Fetch

                RaiseListChangedEvents = False
                IsReadOnly = False

                For Each dr As DataRow In myData.Rows
                    Add(ShareHolderInfo.GetShareHolderInfo(dr))
                Next

                IsReadOnly = True
                RaiseListChangedEvents = True

            End Using

        End Sub

#End Region

    End Class

End Namespace
