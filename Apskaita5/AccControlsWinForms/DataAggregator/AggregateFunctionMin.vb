Namespace DataAggregator

    Public Class AggregateFunctionMin
        Implements IAggregateFunction

        Public Function GetAggregateSum(ByVal sourceValues() As Double) As Double _
            Implements IAggregateFunction.GetAggregateSum

            If sourceValues Is Nothing OrElse sourceValues.Length < 1 Then Return 0

            Dim result As Double = Double.MaxValue

            For Each value As Double In sourceValues
                If value < result Then result = value
            Next

            Return result

        End Function

        Public Overrides Function ToString() As String
            Return "MIN"
        End Function

    End Class

End Namespace