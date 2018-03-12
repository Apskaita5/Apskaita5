Namespace DataAggregator

    ''' <summary>
    ''' An interface that should be implemented by an aggregation operation in order to use 
    ''' it in <see cref="AggregateSum.AggregateFunction">AggregateSum.AggregateFunction</see>.
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface IAggregateFunction

        ''' <summary>
        ''' Gets an aggregated value for the sourceValues.
        ''' </summary>
        ''' <param name="sourceValues">a collection of double values to aggregate</param>
        ''' <remarks></remarks>
        Function GetAggregateSum(ByVal sourceValues As Double()) As Double

    End Interface

End Namespace