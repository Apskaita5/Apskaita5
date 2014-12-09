Public Class CurrencyRateList
    Inherits List(Of CurrencyRate)

    Public Overloads Sub Add(ByVal AtDate As Date, ByVal CurrencyCode As String, _
        Optional ByVal AddEmptyCurrency As Boolean = False, _
        Optional ByVal AddLTL As Boolean = False)

        If CurrencyCode Is Nothing OrElse String.IsNullOrEmpty(CurrencyCode.Trim) _
            AndAlso Not AddEmptyCurrency Then Exit Sub

        If Not CurrencyCode Is Nothing AndAlso CurrencyCode.Trim.ToUpper = "LTL" _
            AndAlso Not AddLTL Then Exit Sub

        MyBase.Add(New CurrencyRate(AtDate, CurrencyCode))

    End Sub

    Friend Function GetDatesInList() As List(Of Date)

        Dim result As New List(Of Date)

        For Each c As CurrencyRate In Me
            If Not result.Contains(c.Date.Date) Then result.Add(c.Date.Date)
        Next

        Return result

    End Function

    Public Sub RemoveDuplicatesFromList()

        Dim i, j As Integer

        For i = Me.Count To 1 Step -1

            If Me(i - 1).CurrencyCode Is Nothing OrElse _
                String.IsNullOrEmpty(Me(i - 1).CurrencyCode.Trim) OrElse _
                Me(i - 1).CurrencyCode.Trim.ToUpper = "LTL" Then

                Me.RemoveAt(i - 1)

            ElseIf Array.IndexOf(CurrencyRate.CurrencyCodes, _
                Me(i - 1).CurrencyCode.Trim.ToUpper) < 0 Then

                Throw New Exception("Klaida. Nežinomas valiutos kodas '" _
                    & Me(i - 1).CurrencyCode.Trim.ToUpper & "'.")

            End If

        Next

        For i = Me.Count To 2 Step -1

            For j = 1 To i - 1

                If Me(i - 1).Date.Date = Me(j - 1).Date.Date _
                    AndAlso Me(i - 1).CurrencyCode.Trim.ToUpper _
                    = Me(j - 1).CurrencyCode.Trim.ToUpper Then
                    Me.RemoveAt(i - 1)
                    Exit For
                End If

            Next

        Next

    End Sub

    Public Function GetCurrencyRate(ByVal AtDate As Date, ByVal CurrencyCode As String, _
        Optional ByVal RateForUndefinedCurrency As Double = 0) As Double

        If CurrencyCode Is Nothing OrElse String.IsNullOrEmpty(CurrencyCode.Trim) Then _
            Return RateForUndefinedCurrency

        If CurrencyCode.Trim.ToUpper = "LTL" Then Return 1

        For Each c As CurrencyRate In Me
            If c.Date.Date = AtDate.Date AndAlso c.CurrencyCode.Trim.ToUpper _
                = CurrencyCode.Trim.ToUpper Then Return c.Rate
        Next

        Return RateForUndefinedCurrency

    End Function

End Class
