Namespace ActiveReports

    ''' <summary>
    ''' Represents a balance sheet report (part of <see cref="ActiveReports.FinancialStatementsInfo">FinancialStatementsInfo</see> report).
    ''' </summary>
    ''' <remarks>Should only be used as a child of <see cref="ActiveReports.FinancialStatementsInfo">FinancialStatementsInfo</see>.</remarks>
    <Serializable()> _
    Public NotInheritable Class BalanceSheetInfoList
        Inherits ReadOnlyListBase(Of BalanceSheetInfoList, BalanceSheetInfo)

#Region " Business Methods "

        ''' <summary>
        ''' Updates <see cref="BalanceSheetInfo.OptimizedBalanceCurrent">current period balance</see> with a corrective value to simulate closing.
        ''' </summary>
        ''' <param name="closingSum">Value by which the correction ir performed. Positive number stands for debit type.</param>
        ''' <param name="closingSummaryBalanceItem"><see cref="BalanceSheetInfo.Name">Name</see> of the balance item that is associated with the <see cref="General.DefaultAccountType.ClosingSummary">closing summary account</see>.</param>
        ''' <remarks></remarks>
        Friend Sub UpdateOptimizedCurrentPeriodBalance(ByVal closingSum As Double, _
            ByVal closingSummaryBalanceItem As String)

            If CRound(closingSum) = 0 Then Exit Sub

            For Each b As BalanceSheetInfo In Me
                If b.Name.Trim.ToUpper = closingSummaryBalanceItem.Trim.ToUpper Then

                    b.UpdateOptimizedBalanceCurrentWithValue(closingSum)

                    For Each c As BalanceSheetInfo In Me
                        If c.ID <> b.ID AndAlso c.Left < b.Left AndAlso c.Left < b.Right _
                            AndAlso c.Right > b.Right Then
                            c.UpdateOptimizedBalanceCurrentWithValue(closingSum)
                        End If
                    Next

                    Exit For

                End If
            Next

        End Sub

        ''' <summary>
        ''' Updates <see cref="BalanceSheetInfo.OptimizedBalanceFormer">former period balance</see> with a corrective value to simulate closing.
        ''' </summary>
        ''' <param name="closingSum">Value by which the correction ir performed. Positive number stands for debit type.</param>
        ''' <param name="closingSummaryBalanceItem"><see cref="BalanceSheetInfo.Name">Name</see> of the balance item that is associated with the <see cref="General.DefaultAccountType.ClosingSummary">closing summary account</see>.</param>
        ''' <remarks></remarks>
        Friend Sub UpdateOptimizedFormerPeriodBalance(ByVal ClosingSum As Double, _
            ByVal ClosingSummaryBalanceItem As String)

            If CRound(ClosingSum) = 0 Then Exit Sub

            For Each b As BalanceSheetInfo In Me
                If b.Name.Trim.ToUpper = ClosingSummaryBalanceItem.Trim.ToUpper Then

                    b.UpdateOptimizedBalanceFormerWithValue(ClosingSum)

                    For Each c As BalanceSheetInfo In Me
                        If c.ID <> b.ID AndAlso c.Left < b.Left AndAlso c.Left < b.Right _
                            AndAlso c.Right > b.Right Then
                            c.UpdateOptimizedBalanceFormerWithValue(ClosingSum)
                        End If
                    Next

                    Exit For

                End If
            Next

        End Sub

#End Region

#Region " Factory Methods "

        ''' <summary>
        ''' Gets a balance sheet report by a database query result.
        ''' </summary>
        ''' <param name="myData">A database query result</param>
        ''' <remarks></remarks>
        Friend Shared Function GetBalanceSheetInfoList(ByVal myData As DataTable) As BalanceSheetInfoList
            Return New BalanceSheetInfoList(myData)
        End Function

        ''' <summary>
        ''' Gets a balance sheet report by a list of items provided by the 
        ''' <see cref="General.ConsolidatedReport.GetBalanceSheetInfoList">ConsolidatedReport.GetBalanceSheetInfoList</see> method.
        ''' </summary>
        ''' <param name="source">a list of items</param>
        ''' <remarks></remarks>
        Friend Shared Function GetBalanceSheetInfoList(ByVal source As List(Of BalanceSheetInfo)) As BalanceSheetInfoList
            Return New BalanceSheetInfoList(source)
        End Function


        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal myData As DataTable)
            ' require use of factory methods
            Fetch(myData)
        End Sub

        Private Sub New(ByVal source As List(Of BalanceSheetInfo))
            ' require use of factory methods
            Fetch(source)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal myData As DataTable)

            RaiseListChangedEvents = False
            IsReadOnly = False

            For Each dr As DataRow In myData.Rows
                If Utilities.ConvertDatabaseID(Of General.FinancialStatementItemType) _
                    (CIntSafe(dr.Item(0), 4)) = General.FinancialStatementItemType. _
                    StatementOfFinancialPosition Then Add(BalanceSheetInfo.GetBalanceSheetInfo(dr))
            Next

            IsReadOnly = True
            RaiseListChangedEvents = True

        End Sub

        Private Sub Fetch(ByVal source As List(Of BalanceSheetInfo))

            RaiseListChangedEvents = False
            IsReadOnly = False

            For Each statementItem As BalanceSheetInfo In source
                Add(statementItem)
            Next

            IsReadOnly = True
            RaiseListChangedEvents = True

        End Sub

#End Region

    End Class

End Namespace
