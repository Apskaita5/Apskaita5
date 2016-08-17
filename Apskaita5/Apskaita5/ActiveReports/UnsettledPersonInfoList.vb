Namespace ActiveReports

    ''' <summary>
    ''' Represents an unsetled claims report, a list of documents (usualy invoices), that were not settled, grouped by a person.
    ''' </summary>
    ''' <remarks></remarks>
    <Serializable()> _
    Public NotInheritable Class UnsettledPersonInfoList
        Inherits ReadOnlyListBase(Of UnsettledPersonInfoList, UnsettledPersonInfo)

#Region " Business Methods "

        Private _AsOfDate As Date = Today
        Private _ForBuyers As Boolean = True
        Private _Account As Long = 0
        Private _MarginOfError As Double = 0
        Private _PersonGroup As PersonGroupInfo = Nothing

        <NotUndoable(), NonSerialized()> _
        Private _SortedList As Csla.SortedBindingList(Of UnsettledPersonInfo) = Nothing


        ''' <summary>
        ''' Date of calculation.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property AsOfDate() As Date
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _AsOfDate
            End Get
        End Property

        ''' <summary>
        ''' Whether the report is fetched for buyers, i.e. debit is considered as debt.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property ForBuyers() As Boolean
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _ForBuyers
            End Get
        End Property

        ''' <summary>
        ''' <see cref="General.Account.ID">Account</see> that is used for debt accounting.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Account() As Long
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _Account
            End Get
        End Property

        ''' <summary>
        ''' Technical debt margin, i.e. debt below it is not considered as a real debt.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property MarginOfError() As Double
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return CRound(_MarginOfError)
            End Get
        End Property

        ''' <summary>
        ''' A <see cref="General.PersonGroup">person group</see> to filter the results.
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property PersonGroup() As PersonGroupInfo
            <System.Runtime.CompilerServices.MethodImpl(Runtime.CompilerServices.MethodImplOptions.NoInlining)> _
            Get
                Return _PersonGroup
            End Get
        End Property


        ''' <summary>
        ''' Gets a sortable view of the report. Used to implement auto sorting in a grid.
        ''' </summary>
        ''' <remarks></remarks>
        Public Function GetSortedList() As Csla.SortedBindingList(Of UnsettledPersonInfo)
            If _SortedList Is Nothing Then
                _SortedList = New Csla.SortedBindingList(Of UnsettledPersonInfo)(Me)
            End If
            Return _SortedList
        End Function

#End Region

#Region " Authorization Rules "

        Public Shared Function CanGetObject() As Boolean
            Return ApplicationContext.User.IsInRole("Reports.UnsettledPersonInfoList1")
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets a UnsettledPersonInfoList report.
        ''' </summary>
        ''' <param name="nAsOfDate">Date of calculation.</param>
        ''' <param name="nForBuyers">Whether the report is fetched for buyers, i.e. debit is considered as debt.</param>
        ''' <param name="nAccount"><see cref="General.Account.ID">Account</see> that is used for debt accounting.</param>
        ''' <param name="nMarginOfError">Technical debt margin, i.e. debt below it is not considered as a real debt.</param>
        ''' <param name="nPersonGroup">A <see cref="General.PersonGroup">person group</see> to filter the results by.</param>
        ''' <remarks></remarks>
        Public Shared Function GetUnsettledPersonInfoList(ByVal nAsOfDate As Date, _
            ByVal nForBuyers As Boolean, ByVal nAccount As Long, ByVal nMarginOfError As Double, _
            ByVal nPersonGroup As PersonGroupInfo) As UnsettledPersonInfoList
            Return DataPortal.Fetch(Of UnsettledPersonInfoList)(New Criteria( _
                nAsOfDate, nForBuyers, nAccount, nMarginOfError, nPersonGroup))
        End Function


        Private Sub New()
            ' require use of factory methods
        End Sub

#End Region

#Region " Data Access "

        <Serializable()> _
        Private Class Criteria
            Private _AsOfDate As Date = Today
            Private _ForBuyers As Boolean = True
            Private _Account As Long = 0
            Private _MarginOfError As Double = 0
            Private _PersonGroup As PersonGroupInfo = Nothing
            Public ReadOnly Property AsOfDate() As Date
                Get
                    Return _AsOfDate
                End Get
            End Property
            Public ReadOnly Property ForBuyers() As Boolean
                Get
                    Return _ForBuyers
                End Get
            End Property
            Public ReadOnly Property Account() As Long
                Get
                    Return _Account
                End Get
            End Property
            Public ReadOnly Property MarginOfError() As Double
                Get
                    Return CRound(_MarginOfError)
                End Get
            End Property
            Public ReadOnly Property PersonGroup() As PersonGroupInfo
                Get
                    Return _PersonGroup
                End Get
            End Property
            Public Sub New(ByVal nAsOfDate As Date, ByVal nForBuyers As Boolean, ByVal nAccount As Long, _
                ByVal nMarginOfError As Double, ByVal nPersonGroup As PersonGroupInfo)
                _AsOfDate = nAsOfDate
                _ForBuyers = nForBuyers
                _Account = nAccount
                _MarginOfError = nMarginOfError
                _PersonGroup = nPersonGroup
            End Sub
        End Class

        Private Overloads Sub DataPortal_Fetch(ByVal criteria As Criteria)

            If Not CanGetObject() Then Throw New System.Security.SecurityException( _
                My.Resources.Common_SecuritySelectDenied)

            If Not criteria.Account > 0 Then
                Throw New Exception(My.Resources.ActiveReports_UnsettledPersonInfoList_AccountNull)
            End If

            Dim myComm As New SQLCommand("FetchUnsettledPersonInfoList")
            myComm.AddParam("?AC", criteria.Account)
            myComm.AddParam("?DT", criteria.AsOfDate)
            myComm.AddParam("?ME", IIf(criteria.MarginOfError < 0, 0, criteria.MarginOfError))
            If criteria.ForBuyers Then
                myComm.AddParam("?TP", Utilities.ConvertDatabaseCharID(BookEntryType.Debetas))
            Else
                myComm.AddParam("?TP", Utilities.ConvertDatabaseCharID(BookEntryType.Kreditas))
            End If
            If criteria.PersonGroup Is Nothing OrElse criteria.PersonGroup.IsEmpty Then
                myComm.AddParam("?PG", 0)
            Else
                myComm.AddParam("?PG", criteria.PersonGroup.ID)
            End If

            Using myData As DataTable = myComm.Fetch

                Dim personList As New List(Of Integer)
                Dim personID As Integer
                For Each dr As DataRow In myData.Rows
                    personID = CIntSafe(dr.Item(6), 0)
                    If Not personList.Contains(personID) Then personList.Add(personID)
                Next

                RaiseListChangedEvents = False
                IsReadOnly = False

                For Each pid As Integer In personList
                    Add(UnsettledPersonInfo.GetUnsettledPersonInfo(myData, pid))
                Next

                _AsOfDate = criteria.AsOfDate
                _ForBuyers = criteria.ForBuyers
                _Account = criteria.Account
                _MarginOfError = criteria.MarginOfError
                _PersonGroup = criteria.PersonGroup

                IsReadOnly = True
                RaiseListChangedEvents = True

            End Using

        End Sub

#End Region

    End Class

End Namespace