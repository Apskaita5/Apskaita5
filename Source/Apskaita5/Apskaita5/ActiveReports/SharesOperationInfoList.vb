Namespace ActiveReports

    <Serializable()>
    Public NotInheritable Class SharesOperationInfoList
        Inherits ReadOnlyListBase(Of SharesOperationInfoList, SharesOperationInfo)

#Region " Business Methods "

        Private _ShareHolder As PersonInfo = Nothing
        Private _CompanyShares As Boolean
        Private _Class As SharesClassInfo = Nothing
        Private _DateBegin As Date = Today
        Private _DateEnd As Date = Today


        ''' <summary>
        ''' Gets a shareholder that the report is fetched for.
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property ShareHolder() As PersonInfo
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _ShareHolder
            End Get
        End Property

        ''' <summary>
        ''' Gets whether a shareholder, that the report is fetched for, is the company itself.
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property CompanyShares As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _CompanyShares
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
        ''' Gets a starting date of the report period.
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property DateBegin As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _DateBegin
            End Get
        End Property

        ''' <summary>
        ''' Gets an ending date of the report period.
        ''' </summary>
        ''' <returns></returns>
        Public ReadOnly Property DateEnd As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)>
            Get
                Return _DateEnd
            End Get
        End Property

#End Region

#Region " Authorization Rules "

        Public Shared Function CanGetObject() As Boolean
            Return ApplicationContext.User.IsInRole("Reports.SharesOperationInfoList1")
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets a new instance of SharesOperationInfoList report.
        ''' </summary>
        ''' <param name="shareHolder">a shareholder to fetch the report for</param>
        ''' <param name="companyShares">whether to fetch the report for the company as it's own share holder</param>
        ''' <param name="sharesClass">a shares class to fetch the report for</param>
        ''' <param name="dateBegin">a starting date of the report period</param>
        ''' <param name="dateEnd">an ending date of the report period</param>
        ''' <returns></returns>
        Public Shared Function GetSharesOperationInfoList(ByVal shareHolder As PersonInfo,
            ByVal companyShares As Boolean, ByVal sharesClass As SharesClassInfo,
            ByVal dateBegin As Date, ByVal dateEnd As Date) As SharesOperationInfoList
            Return DataPortal.Fetch(Of SharesOperationInfoList)(New Criteria(
                shareHolder, companyShares, sharesClass, dateBegin, dateEnd))
        End Function

        Private Sub New()
            ' require use of factory methods
        End Sub

#End Region

#Region " Data Access "

        <Serializable()>
        Private Class Criteria
            Private _ShareHolder As PersonInfo = Nothing
            Private _CompanyShares As Boolean
            Private _Class As SharesClassInfo = Nothing
            Private _DateBegin As Date = Today
            Private _DateEnd As Date = Today
            Public ReadOnly Property ShareHolder() As PersonInfo
                Get
                    Return _ShareHolder
                End Get
            End Property
            Public ReadOnly Property CompanyShares As Boolean
                Get
                    Return _CompanyShares
                End Get
            End Property
            Public ReadOnly Property [Class]() As SharesClassInfo
                Get
                    Return _Class
                End Get
            End Property
            Public ReadOnly Property DateBegin As Date
                Get
                    Return _DateBegin
                End Get
            End Property
            Public ReadOnly Property DateEnd As Date
                Get
                    Return _DateEnd
                End Get
            End Property
            Public Sub New(ByVal shareHolder As PersonInfo, ByVal companyShares As Boolean,
                ByVal sharesClass As SharesClassInfo, ByVal dateBegin As Date, ByVal dateEnd As Date)
                _ShareHolder = shareHolder
                _CompanyShares = companyShares
                _Class = sharesClass
                _DateBegin = dateBegin
                _DateEnd = dateEnd
            End Sub
        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)

            Dim myComm As New SQLCommand("FetchSharesOperationInfoList")
            myComm.AddParam("?DF", criteria.DateBegin)
            myComm.AddParam("?DT", criteria.DateEnd)
            If criteria.CompanyShares Then
                myComm.AddParam("?PD", 0)
            ElseIf criteria.ShareHolder = PersonInfo.Empty Then
                myComm.AddParam("?PD", -1)
            Else
                myComm.AddParam("?PD", criteria.ShareHolder.ID)
            End If
            If criteria.Class = SharesClassInfo.Empty Then
                myComm.AddParam("?CD", 0)
            Else
                myComm.AddParam("?CD", criteria.Class.ID)
            End If

            Using myData As DataTable = myComm.Fetch

                RaiseListChangedEvents = False
                IsReadOnly = False

                For Each dr As DataRow In myData.Rows
                    Add(SharesOperationInfo.GetSharesOperationInfo(dr))
                Next

                _Class = criteria.Class
                _CompanyShares = criteria.CompanyShares
                _DateBegin = criteria.DateBegin
                _DateEnd = criteria.DateEnd
                If Not _CompanyShares Then
                    _ShareHolder = criteria.ShareHolder
                End If

                IsReadOnly = True
                RaiseListChangedEvents = True

            End Using

        End Sub

#End Region

    End Class

End Namespace