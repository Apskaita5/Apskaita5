Namespace General

    ''' <summary>
    ''' Represents a list of company specific default <see cref="General.Account">accounts</see> 
    ''' that are used by other objects for initialization.
    ''' </summary>
    ''' <remarks>Only used as a child of <see cref="Company">Company</see>.
    ''' Related helper object is <see cref="Settings.CompanyInfo">CompanyInfo</see>, 
    ''' method <see cref="Settings.CompanyInfo.GetDefaultAccount">GetDefaultAccount</see>.
    ''' Values are stored in the database table companyaccounts.</remarks>
    <Serializable()> _
    Public NotInheritable Class CompanyAccountList
        Inherits BusinessListBase(Of CompanyAccountList, CompanyAccount)

#Region " Business Methods "

        Public Function HasWarnings() As Boolean
            For Each i As CompanyAccount In Me
                If i.BrokenRulesCollection.WarningCount > 0 Then Return True
            Next
            Return False
        End Function

        Public Function GetAllBrokenRules() As String
            Dim result As String = GetAllBrokenRulesForList(Me)

            'Dim GeneralErrorString As String = ""
            'SomeGeneralValidationSub(GeneralErrorString)
            'AddWithNewLine(result, GeneralErrorString, False)

            Return result
        End Function

        Public Function GetAllWarnings() As String
            Dim result As String = GetAllWarningsForList(Me)
            'Dim GeneralErrorString As String = ""
            'SomeGeneralValidationSub(GeneralErrorString)
            'AddWithNewLine(result, GeneralErrorString, False)

            Return result
        End Function

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets the CompanyAccountList for the current company.
        ''' </summary>
        ''' <remarks></remarks>
        Friend Shared Function GetCompanyAccountList() As CompanyAccountList
            Return New CompanyAccountList(True)
        End Function

        ''' <summary>
        ''' Gets a new empty CompanyAccountList for a new company.
        ''' </summary>
        ''' <remarks></remarks>
        Friend Shared Function NewCompanyAccountList() As CompanyAccountList
            Return New CompanyAccountList(False)
        End Function


        Private Sub New()
            ' require use of factory methods
            MarkAsChild()
            Me.AllowEdit = True
            Me.AllowNew = False
            Me.AllowRemove = False
        End Sub

        Private Sub New(ByVal FetchOld As Boolean)
            ' require use of factory methods
            MarkAsChild()
            Me.AllowEdit = True
            Me.AllowNew = False
            Me.AllowRemove = False
            If FetchOld Then Fetch()
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch()

            Dim myComm As New SQLCommand("FetchCompanyAccountList")

            Using myData As DataTable = myComm.Fetch

                RaiseListChangedEvents = False

                AddRow(myData, DefaultAccountType.VatPayable)
                AddRow(myData, DefaultAccountType.VatReceivable)
                AddRow(myData, DefaultAccountType.Buyers)
                AddRow(myData, DefaultAccountType.Suppliers)
                AddRow(myData, DefaultAccountType.Bank)
                AddRow(myData, DefaultAccountType.Till)
                AddRow(myData, DefaultAccountType.WagePayable)
                AddRow(myData, DefaultAccountType.WageImprestPayable)
                AddRow(myData, DefaultAccountType.WageWithdraw)
                AddRow(myData, DefaultAccountType.WageGpmPayable)
                AddRow(myData, DefaultAccountType.WagePsdPayable)
                AddRow(myData, DefaultAccountType.WagePsdPayableToVMI)
                AddRow(myData, DefaultAccountType.WageSodraPayable)
                AddRow(myData, DefaultAccountType.WageGuaranteeFundPayable)
                AddRow(myData, DefaultAccountType.OtherGpmPayable)
                AddRow(myData, DefaultAccountType.OtherPsdPayable)
                AddRow(myData, DefaultAccountType.OtherSodraPayable)
                AddRow(myData, DefaultAccountType.HolidayReserve)
                AddRow(myData, DefaultAccountType.ClosingSummary)
                AddRow(myData, DefaultAccountType.CurrentProfit)
                AddRow(myData, DefaultAccountType.FormerProfit)
                AddRow(myData, DefaultAccountType.GoodsSalesNetCosts)
                AddRow(myData, DefaultAccountType.GoodsPurchases)
                AddRow(myData, DefaultAccountType.GoodsDiscounts)
                AddRow(myData, DefaultAccountType.GoodsValueReduction)

                RaiseListChangedEvents = True

            End Using

        End Sub

        Friend Sub Update(ByVal parent As Company)

            RaiseListChangedEvents = False

            DeletedList.Clear()

            For Each item As CompanyAccount In Me
                If item.IsNew Then
                    item.Insert(Me)
                ElseIf item.IsDirty Then
                    item.Update(Me)
                End If
            Next

            RaiseListChangedEvents = True

        End Sub

        Private Sub AddRow(ByVal myData As DataTable, ByVal ofType As DefaultAccountType)
            For Each dr As DataRow In myData.Rows
                If Utilities.ConvertDatabaseID(Of DefaultAccountType)(CIntSafe(dr.Item(1), 0)) = ofType Then
                    Add(CompanyAccount.GetCompanyAccount(dr))
                    Exit Sub
                End If
            Next
            Add(CompanyAccount.NewCompanyAccount(ofType))
        End Sub

#End Region

    End Class

End Namespace