Namespace DataAggregator

    Public Class AggregateFunctionAverage
        Implements IAggregateFunction

        Public Function GetAggregateSum(ByVal sourceValues() As Double) As Double _
            Implements IAggregateFunction.GetAggregateSum

            If sourceValues Is Nothing OrElse sourceValues.Length < 1 Then Return 0

            Dim result As Double = 0

            For Each value As Double In sourceValues
                result = result + value
            Next

            Return result / sourceValues.Length

        End Function

        Public Overrides Function ToString() As String
            Return "AVG"
        End Function

    End Class

End Namespace
