Namespace General

    <Serializable()> _
    Public Class CompanyRateList
        Inherits BusinessListBase(Of CompanyRateList, CompanyRate)

#Region " Business Methods "

        Public Function HasWarnings() As Boolean
            For Each i As CompanyRate In Me
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

        Friend Shared Function GetCompanyRateList() As CompanyRateList
            Dim result As CompanyRateList = New CompanyRateList(True)
            Return result
        End Function

        Friend Shared Function NewCompanyRateList() As CompanyRateList
            Dim result As CompanyRateList = New CompanyRateList()
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

            Dim myComm As New SQLCommand("FetchCompanyRateList")

            Using myData As DataTable = myComm.Fetch

                RaiseListChangedEvents = False

                AddRow(myData, RateType.Vat)
                AddRow(myData, RateType.GpmWage)
                AddRow(myData, RateType.PsdEmployee)
                AddRow(myData, RateType.SodraEmployee)
                AddRow(myData, RateType.PsdEmployer)
                AddRow(myData, RateType.SodraEmployer)
                AddRow(myData, RateType.GuaranteeFund)
                AddRow(myData, RateType.WageRateNight)
                AddRow(myData, RateType.WageRateOvertime)
                AddRow(myData, RateType.WageRatePublicHolidays)
                AddRow(myData, RateType.WageRateRestTime)
                AddRow(myData, RateType.WageRateDeviations)
                AddRow(myData, RateType.WageRateSickLeave)

                RaiseListChangedEvents = True

            End Using

        End Sub

        Private Sub AddRow(ByVal myData As DataTable, ByVal OfType As RateType)
            For Each dr As DataRow In myData.Rows
                If ConvertEnumDatabaseCode(Of RateType)(CIntSafe(dr.Item(1), 0)) = OfType Then
                    Add(CompanyRate.GetCompanyRate(dr))
                    Exit Sub
                End If
            Next
            Add(CompanyRate.NewCompanyRate(OfType))
        End Sub

        Friend Sub Update(ByVal parent As Company)

            RaiseListChangedEvents = False

            DeletedList.Clear()

            For Each item As CompanyRate In Me
                If item.IsNew Then
                    item.Insert(Me)
                ElseIf item.IsDirty Then
                    item.Update(Me)
                End If
            Next

            RaiseListChangedEvents = True

        End Sub

#End Region

    End Class

End Namespace