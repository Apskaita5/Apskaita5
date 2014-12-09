Namespace General

    <Serializable()> _
    Public Class CompanyAccountList
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

        Friend Shared Function GetCompanyAccountList() As CompanyAccountList
            Dim result As CompanyAccountList = New CompanyAccountList(True)
            Return result
        End Function

        Friend Shared Function NewCompanyAccountList() As CompanyAccountList
            Dim result As CompanyAccountList = New CompanyAccountList()
            Return result
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
                AddRow(myData, DefaultAccountType.ClosingSummary)
                AddRow(myData, DefaultAccountType.CurrentProfit)
                AddRow(myData, DefaultAccountType.FormerProfit)

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

        Private Sub AddRow(ByVal myData As DataTable, ByVal OfType As DefaultAccountType)
            For Each dr As DataRow In myData.Rows
                If ConvertEnumDatabaseCode(Of DefaultAccountType)(CIntSafe(dr.Item(1), 0)) = OfType Then
                    Add(CompanyAccount.GetCompanyAccount(dr))
                    Exit Sub
                End If
            Next
            Add(CompanyAccount.NewCompanyAccount(OfType))
        End Sub

#End Region

    End Class

End Namespace