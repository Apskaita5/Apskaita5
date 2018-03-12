Namespace DataAggregator

    Public Class AggregateFunctionCount
        Implements IAggregateFunction

        Public Function GetAggregateSum(ByVal sourceValues() As Double) As Double _
            Implements IAggregateFunction.GetAggregateSum
            If sourceValues Is Nothing Then Return 0
            Return sourceValues.Length
        End Function

        Public Overrides Function ToString() As String
            Return "COUNT"
        End Function

    End Class

End Namespace