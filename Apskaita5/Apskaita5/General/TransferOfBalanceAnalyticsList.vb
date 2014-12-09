Namespace General

    <Serializable()> _
    Public Class TransferOfBalanceAnalyticsList
        Inherits BusinessListBase(Of TransferOfBalanceAnalyticsList, TransferOfBalanceAnalytics)

#Region " Business Methods "

        Protected Overrides Function AddNewCore() As Object
            Dim NewItem As TransferOfBalanceAnalytics = TransferOfBalanceAnalytics.NewTransferOfBalanceAnalytics
            Me.Add(NewItem)
            Return NewItem
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

        Friend Shared Function NewTransferOfBalanceAnalyticsList() As TransferOfBalanceAnalyticsList
            Return New TransferOfBalanceAnalyticsList()
        End Function

        Friend Shared Function GetTransferOfBalanceAnalyticsList _
            (ByRef TotalBookEntryList As BookEntryInternalList, _
            ByVal nFinancialDataCanChange As Boolean) As TransferOfBalanceAnalyticsList
            Return New TransferOfBalanceAnalyticsList(TotalBookEntryList, nFinancialDataCanChange)
        End Function


        Private Sub New()
            ' require use of factory methods
            MarkAsChild()
            Me.AllowEdit = True
            Me.AllowNew = True
            Me.AllowRemove = True
        End Sub

        Private Sub New(ByRef TotalBookEntryList As BookEntryInternalList, _
            ByVal nFinancialDataCanChange As Boolean)
            ' require use of factory methods
            MarkAsChild()
            Fetch(TotalBookEntryList, nFinancialDataCanChange)
            Me.AllowEdit = nFinancialDataCanChange
            Me.AllowNew = nFinancialDataCanChange
            Me.AllowRemove = nFinancialDataCanChange
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByRef TotalBookEntryList As BookEntryInternalList, _
            ByVal nFinancialDataCanChange As Boolean)

            RaiseListChangedEvents = False

            Dim CompensatingBookEntryList As BookEntryInternalList = _
                BookEntryInternalList.NewBookEntryInternalList(BookEntryType.Debetas)

            For i As Integer = TotalBookEntryList.Count To 1 Step -1

                If Not TotalBookEntryList(i - 1).Person Is Nothing _
                    AndAlso TotalBookEntryList(i - 1).Person.ID > 0 Then
                    Add(TransferOfBalanceAnalytics.GetTransferOfBalanceAnalytics( _
                        TotalBookEntryList(i - 1), nFinancialDataCanChange))
                End If

                TotalBookEntryList(i - 1).Person = Nothing

            Next

            For Each i As BookEntryInternal In CompensatingBookEntryList
                TotalBookEntryList.Add(i)
            Next

            RaiseListChangedEvents = True

        End Sub

        Friend Sub Update(ByRef CommonBookEntryList As BookEntryInternalList)

            RaiseListChangedEvents = False

            For Each i As TransferOfBalanceAnalytics In Me
                CommonBookEntryList.Add(BookEntryInternal.NewBookEntryInternal( _
                    i.EntryType, i.Account, i.Ammount, i.Person))
                ' compensatory entries that will be dealt by call to Aggregate
                If i.EntryType = BookEntryType.Debetas Then
                    CommonBookEntryList.Add(BookEntryInternal.NewBookEntryInternal( _
                        BookEntryType.Kreditas, i.Account, i.Ammount, Nothing))
                Else
                    CommonBookEntryList.Add(BookEntryInternal.NewBookEntryInternal( _
                        BookEntryType.Debetas, i.Account, i.Ammount, Nothing))
                End If
            Next

            RaiseListChangedEvents = True

        End Sub

#End Region

    End Class

End Namespace