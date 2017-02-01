Namespace ActiveReports

    ''' <summary>
    ''' Represents an income statement report (part of <see cref="ActiveReports.FinancialStatementsInfo">FinancialStatementsInfo</see> report).
    ''' </summary>
    ''' <remarks>Should only be used as a child of of <see cref="ActiveReports.FinancialStatementsInfo">FinancialStatementsInfo</see>.</remarks>
    <Serializable()> _
    Public NotInheritable Class IncomeStatementInfoList
        Inherits ReadOnlyListBase(Of IncomeStatementInfoList, IncomeStatementInfo)

#Region " Business Methods "

        ''' <summary>
        ''' Updates <see cref="IncomeStatementInfo.OptimizedBalanceCurrent">current period balance</see> 
        ''' and <see cref="IncomeStatementInfo.OptimizedBalanceFormer">former period balance</see>
        ''' with a corrective value to exclude closing impact.
        ''' </summary>
        ''' <param name="accountTurnoverList">Account turnover info by which the correction ir performed.</param>
        ''' <remarks></remarks>
        Friend Sub UpdateOptimizedValues(ByVal accountTurnoverList As AccountTurnoverInfoList)

            For Each a As AccountTurnoverInfo In accountTurnoverList

                For Each i As IncomeStatementInfo In Me

                    ' maped statement item is found
                    If i.Name.Trim.ToUpper = a.FinancialStatementItem.Trim.ToUpper Then

                        ' update the item
                        i.UpdateOptimizedValues(a)

                        ' update the item's parents
                        For Each n As IncomeStatementInfo In Me
                            If n.ID <> i.ID AndAlso n.Left < i.Left AndAlso n.Left < i.Right _
                                AndAlso n.Right > i.Right Then
                                n.UpdateOptimizedValues(a)
                            End If
                        Next

                        ' next account
                        Exit For

                    End If

                Next

            Next

        End Sub

#End Region

#Region " Factory Methods "

        Friend Shared Function GetIncomeStatementInfoList(ByVal myData As DataTable) As IncomeStatementInfoList
            Dim result As IncomeStatementInfoList = New IncomeStatementInfoList(myData)
            Return result
        End Function


        Private Sub New()
            ' require use of factory methods
        End Sub

        Private Sub New(ByVal myData As DataTable)
            ' require use of factory methods
            Fetch(myData)
        End Sub

#End Region

#Region " Data Access "

        Private Sub Fetch(ByVal myData As DataTable)

            RaiseListChangedEvents = False
            IsReadOnly = False

            For Each dr As DataRow In myData.Rows
                If Utilities.ConvertDatabaseID(Of General.FinancialStatementItemType) _
                    (CIntSafe(dr.Item(0), 4)) = General.FinancialStatementItemType.StatementOfComprehensiveIncome Then
                    Add(IncomeStatementInfo.GetIncomeStatementInfo(dr))
                End If
            Next

            IsReadOnly = True
            RaiseListChangedEvents = True

        End Sub

#End Region

    End Class

End Namespace