Namespace ActiveReports

    ''' <summary>
    ''' Represents a shares account report.
    ''' Contains information about operations with a particular shares class for a particular person.
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()>
    Public NotInheritable Class SharesAccountEntryList
        Inherits ReadOnlyListBase(Of SharesAccountEntryList, SharesAccountEntry)

#Region " Business Methods "

        Private _ShareHolder As PersonInfo = Nothing
        Private _Class As SharesClassInfo = Nothing
        Private _AsOfDate As Date = Today
        Private _AccountNo As String = ""


        ''' <summary>
        ''' Gets a shareholder that the report is fetched for. Null or empty shareholder means
        ''' that the company is a shareholder of itself.
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property ShareHolder() As PersonInfo
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _ShareHolder
            End Get
        End Property

        ''' <summary>
        ''' Gets a shares class that the report is fetched for.
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property [Class]() As SharesClassInfo
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _Class
            End Get
        End Property

        ''' <summary>
        ''' Gets a date that the report is fetched for.
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property AsOfDate As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _AsOfDate
            End Get
        End Property

        ''' <summary>
        ''' Gets a number of the shares account.
        ''' </summary>
        ''' <returns>Equals <see cref="General.SharesClass.ID">SharesClass.ID</see>-<see cref="General.Person.ID">Person.ID</see></returns>
        Public ReadOnly Property AccountNo() As String
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _AccountNo.Trim
            End Get
        End Property

#End Region

#Region " Authorization Rules "

        Public Shared Function CanGetObject() As Boolean
            Return ApplicationContext.User.IsInRole("Reports.SharesAccountEntryList1")
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets a new instance of SharesAccountEntryList report.
        ''' </summary>
        ''' <param name="shareHolder">a shareholder to fetch the report for</param>
        ''' <param name="sharesClass">a shares class to fetch the report for</param>
        ''' <param name="asOfDate">a date to fetch the report for</param>
        ''' <returns></returns>
        Public Shared Function GetSharesAccountEntryList(ByVal shareHolder As PersonInfo,
            ByVal sharesClass As SharesClassInfo, ByVal asOfDate As Date) As SharesAccountEntryList
            Return DataPortal.Fetch(Of SharesAccountEntryList)(New Criteria(shareHolder, sharesClass, asOfDate))
        End Function

        Private Sub New()
            ' require use of factory methods
        End Sub

#End Region

#Region " Data Access "

        <Serializable()>
        Private Class Criteria
            Private _ShareHolder As PersonInfo = Nothing
            Private _Class As SharesClassInfo = Nothing
            Private _AsOfDate As Date = Today
            Public ReadOnly Property ShareHolder() As PersonInfo
                Get
                    Return _ShareHolder
                End Get
            End Property
            Public ReadOnly Property [Class]() As SharesClassInfo
                Get
                    Return _Class
                End Get
            End Property
            Public ReadOnly Property AsOfDate As Date
                Get
                    Return _AsOfDate
                End Get
            End Property
            Public Sub New(ByVal shareHolder As PersonInfo,
                ByVal sharesClass As SharesClassInfo, ByVal asOfDate As Date)
                _ShareHolder = shareHolder
                _Class = sharesClass
                _AsOfDate = asOfDate
            End Sub
        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)

            If Not CanGetObject() Then Throw New System.Security.SecurityException(
                My.Resources.Common_SecuritySelectDenied)

            If criteria.Class = SharesClassInfo.Empty Then Throw New Exception(
                My.Resources.ActiveReports_SharesAccountEntryList_ClassNull)

            Dim myComm As New SQLCommand("FetchSharesAccountEntryList")
            myComm.AddParam("?CD", criteria.Class.ID)
            myComm.AddParam("?DT", criteria.AsOfDate.Date)
            If criteria.ShareHolder = PersonInfo.Empty Then
                myComm.AddParam("?PD", 0)
            Else
                myComm.AddParam("?PD", criteria.ShareHolder.ID)
            End If

            Using myData As DataTable = myComm.Fetch

                RaiseListChangedEvents = False
                IsReadOnly = False

                Dim initialAmount As Double = 0

                For Each dr As DataRow In myData.Rows
                    Add(SharesAccountEntry.GetSharesAccountEntry(dr, initialAmount))
                Next

                _ShareHolder = criteria.ShareHolder
                _AsOfDate = criteria.AsOfDate
                _Class = criteria.Class
                If criteria.ShareHolder = PersonInfo.Empty Then
                    _AccountNo = String.Format("{0}-{1}", _Class.ID.ToString, "0")
                Else
                    _AccountNo = String.Format("{0}-{1}", _Class.ID.ToString, _ShareHolder.ID.ToString)
                End If

                IsReadOnly = True
                RaiseListChangedEvents = True

            End Using

        End Sub

#End Region

    End Class

End Namespace
