Namespace ActiveReports

    ''' <summary>
    ''' Represents an income statement report (part of <see cref="ActiveReports.FinancialStatementsInfo">FinancialStatementsInfo</see> report).
    ''' </summary>
    ''' <remarks>Should only be used as a child of of <see cref="ActiveReports.FinancialStatementsInfo">FinancialStatementsInfo</see>.</remarks>
    <Serializable()> _
    Public Class IncomeStatementInfoList
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

            For i As Integer = myData.Rows.Count To 1 Step -1
                If EnumValueAttribute.ConvertDatabaseID(Of General.FinancialStatementItemType) _
                    (CIntSafe(myData.Rows(i - 1).Item(0), 4)) = General.FinancialStatementItemType. _
                    StatementOfComprehensiveIncome Then Add(IncomeStatementInfo.GetIncomeStatementInfo(myData.Rows(i - 1)))
            Next

            SetNumbers()

            IsReadOnly = True
            RaiseListChangedEvents = True

        End Sub


        ''' <summary>
        ''' Recursively sets the income statement items <see cref="IncomeStatementInfo.Number">Number</see> property.
        ''' </summary>
        ''' <remarks></remarks>
        Private Sub SetNumbers()

            If Me.Count < 1 Then Exit Sub

            Me.Item(0).SetNumber(GetRomanNumber(1), 0)
            Dim CurrentLevel As Integer = Me.Item(0).Level
            Dim CurrentNumber As Integer = 2
            For i As Integer = 2 To Me.Count
                If Me.Item(i - 1).Level < CurrentLevel Then CurrentLevel = Me.Item(i - 1).Level
                If Me.Item(i - 1).Level <= CurrentLevel Then
                    Me.Item(i - 1).SetNumber(GetRomanNumber(CurrentNumber), 0)
                    CurrentNumber += 1
                End If
            Next

            Dim MaxLevel As Integer = 0
            For Each b As IncomeStatementInfo In Me
                If b.Level > MaxLevel Then MaxLevel = b.Level
            Next

            For i As Integer = 3 To MaxLevel
                CurrentNumber = 1
                For Each b As IncomeStatementInfo In Me
                    If b.Level < i Then
                        CurrentNumber = 1
                    ElseIf b.Level = i AndAlso String.IsNullOrEmpty(b.Number) Then
                        b.SetNumber(GetDirectParentNumber(b), CurrentNumber)
                        CurrentNumber += 1
                    End If
                Next
            Next

            For i As Integer = Me.Count To 2 Step -1
                If Me.Item(i - 1).Number.Contains(".") AndAlso Me.Item(i - 1).Number.IndexOf(".") _
                    <> Me.Item(i - 1).Number.Length - 1 Then
                    SwapPositions(i - 1, Me.IndexOf(GetDirectParent(Me.Item(i - 1))))
                End If
            Next

        End Sub

        Private Sub SwapPositions(ByVal rowToSwap1 As Integer, ByVal rowToSwap2 As Integer)

            Dim swapItem1 As IncomeStatementInfo
            Dim swapItem2 As IncomeStatementInfo

            If rowToSwap1 < 0 OrElse rowToSwap1 >= Me.Count OrElse rowToSwap2 < 0 _
                OrElse rowToSwap2 >= Me.Count Then Throw New IndexOutOfRangeException( _
                "Valid range of IncomeStatementInfoList collection is 0 to " & (Me.Count - 1).ToString() _
                & ". Indexes provided were " & rowToSwap1.ToString() & " and " & rowToSwap2.ToString() & ".")


            swapItem1 = Items(rowToSwap1).Clone
            swapItem2 = Items(rowToSwap2).Clone

            Me.RemoveAt(rowToSwap1)
            Me.Insert(rowToSwap1, swapItem2)
            Me.RemoveAt(rowToSwap2)
            Me.Insert(rowToSwap2, swapItem1)

        End Sub

        Private Function GetDirectParentNumber(ByVal child As IncomeStatementInfo) As String
            Dim result As IncomeStatementInfo = GetDirectParent(child)
            If result Is Nothing Then Return ""
            Return result.Number & "."
        End Function

        Private Function GetDirectParent(ByVal child As IncomeStatementInfo) As IncomeStatementInfo

            Dim result As IncomeStatementInfo = Nothing

            For Each b As IncomeStatementInfo In Me
                If child.ID <> b.ID AndAlso child.Left > b.Left AndAlso child.Left < b.Right _
                    AndAlso child.Right < b.Right AndAlso (result Is Nothing OrElse _
                    (result.Right - result.Left) > (b.Right - b.Left)) Then result = b
            Next

            Return result

        End Function

#End Region

    End Class

End Namespace