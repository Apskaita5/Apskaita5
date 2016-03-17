Namespace General

    ''' <summary>
    ''' Represents a list of company specific default rates (tax, wage, etc.), 
    ''' that are used by other objects for initialization.
    ''' </summary>
    ''' <remarks>Only used as a child of <see cref="Company">Company</see>.
    ''' Related helper object is <see cref="Settings.CompanyInfo">CompanyInfo</see>, 
    ''' method <see cref="Settings.CompanyInfo.GetDefaultRate">GetDefaultRate</see>.
    ''' Values are stored in the database table companyrates.</remarks>
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

        ''' <summary>
        ''' Gets a new empty CompanyRateList for a new company.
        ''' </summary>
        ''' <remarks></remarks>
        Friend Shared Function NewCompanyRateList() As CompanyRateList
            Return New CompanyRateList(False)
        End Function

        ''' <summary>
        ''' Gets the CompanyRateList for the current company.
        ''' </summary>
        ''' <remarks></remarks>
        Friend Shared Function GetCompanyRateList() As CompanyRateList
            Return New CompanyRateList(True)
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

                AddRow(myData, DefaultRateType.Vat)
                AddRow(myData, DefaultRateType.GpmWage)
                AddRow(myData, DefaultRateType.PsdEmployee)
                AddRow(myData, DefaultRateType.SodraEmployee)
                AddRow(myData, DefaultRateType.PsdEmployer)
                AddRow(myData, DefaultRateType.SodraEmployer)
                AddRow(myData, DefaultRateType.GuaranteeFund)
                AddRow(myData, DefaultRateType.WageRateNight)
                AddRow(myData, DefaultRateType.WageRateOvertime)
                AddRow(myData, DefaultRateType.WageRatePublicHolidays)
                AddRow(myData, DefaultRateType.WageRateRestTime)
                AddRow(myData, DefaultRateType.WageRateDeviations)
                AddRow(myData, DefaultRateType.WageRateSickLeave)

                RaiseListChangedEvents = True

            End Using

        End Sub

        Private Sub AddRow(ByVal myData As DataTable, ByVal ofType As DefaultRateType)
            For Each dr As DataRow In myData.Rows
                If Utilities.ConvertDatabaseID(Of DefaultRateType)(CIntSafe(dr.Item(1), 0)) = ofType Then
                    Add(CompanyRate.GetCompanyRate(dr))
                    Exit Sub
                End If
            Next
            Add(CompanyRate.NewCompanyRate(ofType))
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