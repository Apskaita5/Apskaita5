Imports System.ComponentModel

Namespace DataAggregator

    ''' <summary>
    ''' Represents an aggregated value for a data column (series).
    ''' </summary>
    ''' <remarks></remarks>
    Public Class AggregateSum
        Implements INotifyPropertyChanged

        Private ReadOnly _Name As String = ""
        Private ReadOnly _SourceValues As Double() = Nothing
        Private _Value As Double = 0
        Private _RoundOrder As Integer = 2
        Private _AggregateFunction As IAggregateFunction = New AggregateFunctionSum()

        Public Event PropertyChanged As PropertyChangedEventHandler _
            Implements INotifyPropertyChanged.PropertyChanged


        Friend Sub New(ByVal table As DataTable, ByVal column As DataColumn)

            If table Is Nothing Then Throw New ArgumentNullException("table")
            If column Is Nothing Then Throw New ArgumentNullException("column")

            Dim result As New List(Of Double)

            For Each dr As DataRow In table.Rows
                result.Add(Convert.ToDouble(dr.Item(column)))
            Next

            _SourceValues = result.ToArray()
            _Name = column.ColumnName
            _Value = _AggregateFunction.GetAggregateSum(_SourceValues)

        End Sub


        ''' <summary>
        ''' Gets a name of the data column (series).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Name() As String
            Get
                Return _Name.Trim
            End Get
        End Property

        ''' <summary>
        ''' Gets the source data column values (series).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property SourceValues() As Double()
            Get
                Return _SourceValues
            End Get
        End Property

        ''' <summary>
        ''' Gets an aggregatet value for the source data column values (series).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property Value() As Double
            Get
                Return CRound(_Value, _RoundOrder)
            End Get
        End Property

        ''' <summary>
        ''' Gets a formated <see cref="Value">Value</see> (because values could have different round orders,
        ''' i.e. format is per item not per column).
        ''' </summary>
        ''' <remarks></remarks>
        Public ReadOnly Property ValueString() As String
            Get

                Dim formatString As String = "##,0"
                If _RoundOrder > 0 Then
                    formatString = formatString & "." & String.Empty.PadRight(RoundOrder, "0"c)
                End If

                Return _Value.ToString(formatString)

            End Get
        End Property

        ''' <summary>
        ''' Gets or sets the round order of the <see cref="Value">Value</see>.
        ''' </summary>
        ''' <remarks></remarks>
        Public Property RoundOrder() As Integer
            Get
                Return _RoundOrder
            End Get
            Set(ByVal value As Integer)
                If _RoundOrder <> value Then
                    _RoundOrder = value
                    NotifyPropertyChanged("RoundOrder")
                    NotifyPropertyChanged("Value")
                    NotifyPropertyChanged("ValueString")
                End If
            End Set
        End Property

        ''' <summary>
        ''' Gets or sets an <see cref="IAggregateFunction">aggregation function</see> to use
        ''' when calculating the <see cref="Value">Value</see>.
        ''' </summary>
        ''' <remarks></remarks>
        Public Property AggregateFunction() As IAggregateFunction
            Get
                Return _AggregateFunction
            End Get
            Set(ByVal value As IAggregateFunction)
                If Not (_AggregateFunction Is Nothing AndAlso value Is Nothing) _
                    AndAlso Not (Not _AggregateFunction Is Nothing AndAlso Not value Is Nothing _
                    AndAlso _AggregateFunction.ToString.Trim.ToUpper = value.ToString.Trim.ToUpper) Then

                    _AggregateFunction = value
                    NotifyPropertyChanged("AggregateFunction")

                    If Not _AggregateFunction Is Nothing Then
                        _Value = _AggregateFunction.GetAggregateSum(_SourceValues)
                    Else
                        _Value = 0
                    End If
                    NotifyPropertyChanged("Value")
                    NotifyPropertyChanged("ValueString")

                End If
            End Set
        End Property


        Private Sub NotifyPropertyChanged(ByVal info As String)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(info))
        End Sub

    End Class

End Namespace
