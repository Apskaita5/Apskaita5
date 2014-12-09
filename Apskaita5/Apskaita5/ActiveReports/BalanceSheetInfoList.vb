Namespace ActiveReports

    <Serializable()> _
    Public Class BalanceSheetInfoList
        Inherits ReadOnlyListBase(Of BalanceSheetInfoList, BalanceSheetInfo)

#Region " Business Methods "

        Friend Sub UpdateOptimizedCurrentPeriodBalance(ByVal ClosingSum As Double, _
            ByVal ClosingSummaryBalanceItem As String)

            If CRound(ClosingSum) = 0 Then Exit Sub

            For Each b As BalanceSheetInfo In Me
                If b.Name.Trim.ToUpper = ClosingSummaryBalanceItem.Trim.ToUpper Then

                    b.UpdateOptimizedBalanceCurrentWithValue(ClosingSum)

                    For Each c As BalanceSheetInfo In Me
                        If c.ID <> b.ID AndAlso c.Left < b.Left AndAlso c.Left < b.Right _
                            AndAlso c.Right > b.Right Then
                            c.UpdateOptimizedBalanceCurrentWithValue(ClosingSum)
                        End If
                    Next

                    Exit For

                End If
            Next

        End Sub

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

        Friend Sub SetNumbers()

            Dim MaxLevel As Integer = 0
            For Each b As BalanceSheetInfo In Me
                If b.Level > MaxLevel Then MaxLevel = b.Level
            Next

            Dim CurrentNumber As Integer
            For i As Integer = 3 To MaxLevel
                CurrentNumber = 1
                For Each b As BalanceSheetInfo In Me
                    If b.Level < i Then
                        CurrentNumber = 1
                    ElseIf b.Level = i Then
                        b.SetNumber(GetDirectParentNumber(b), CurrentNumber)
                        CurrentNumber += 1
                    End If
                Next
            Next

        End Sub

        Private Function GetDirectParentNumber(ByVal child As BalanceSheetInfo) As String

            Dim result As BalanceSheetInfo = Nothing

            For Each b As BalanceSheetInfo In Me
                If child.ID <> b.ID AndAlso child.Left > b.Left AndAlso child.Left < b.Right _
                    AndAlso child.Right < b.Right AndAlso (result Is Nothing OrElse _
                    (result.Right - result.Left) > (b.Right - b.Left)) Then result = b
            Next

            If result Is Nothing OrElse result.Level < 4 Then Return ""
            Return result.Number & "."

        End Function

#End Region

#Region " Factory Methods "

        Friend Shared Function GetBalanceSheetInfoList(ByVal myData As DataTable) As BalanceSheetInfoList
            Dim result As BalanceSheetInfoList = New BalanceSheetInfoList(myData)
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
                If ConvertEnumDatabaseCode(Of General.FinancialStatementItemType) _
                    (CIntSafe(dr.Item(0), 4)) = General.FinancialStatementItemType. _
                    StatementOfFinancialPosition Then Add(BalanceSheetInfo.GetBalanceSheetInfo(dr))
            Next

            SetNumbers()

            IsReadOnly = True
            RaiseListChangedEvents = True

        End Sub

#End Region

    End Class

End Namespace