Imports System.Windows.Forms

Namespace DataAggregator

    ''' <summary>
    ''' A form that displays aggregated values for a given datatable that contains double data series
    ''' in it's columns.
    ''' </summary>
    ''' <remarks></remarks>
    Public Class F_DataAggregator

        Private ReadOnly _DataSource As AggregateSumList
        Private WithEvents _RoundOrderNumericUpDown As New NumericUpDown
        Private WithEvents _AggregateFunctionComboBox As New ComboBox


        ''' <summary>
        ''' Creates a new F_DataAggregator instance.
        ''' </summary>
        ''' <param name="table">a datatable that contains double data series in it's columns</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal table As DataTable)

            ' This call is required by the Windows Form Designer.
            InitializeComponent()

            ' Add any initialization after the InitializeComponent() call.
            _DataSource = New AggregateSumList(table)

        End Sub


        Private Sub F_DataAggregator_Load(ByVal sender As System.Object, _
            ByVal e As System.EventArgs) Handles MyBase.Load

            _AggregateFunctionComboBox.Items.AddRange(New Object() {New AggregateFunctionSum, _
                New AggregateFunctionAverage, New AggregateFunctionMin, New AggregateFunctionMax, _
                New AggregateFunctionCount})

            _RoundOrderNumericUpDown.DecimalPlaces = 0
            _RoundOrderNumericUpDown.Maximum = 10
            _RoundOrderNumericUpDown.Minimum = 0

            AggregateSumListDataListView.DataSource = _DataSource

        End Sub


        Private Sub AggregateSumListDataListView_CellEditFinishing(ByVal sender As Object, _
            ByVal e As BrightIdeasSoftware.CellEditEventArgs) Handles AggregateSumListDataListView.CellEditFinishing

            If e.Cancel Then Exit Sub

            If e.Column.AspectName = "RoundOrder" Then
                e.NewValue = Convert.ToInt32(_RoundOrderNumericUpDown.Value)
            Else
                e.NewValue = _AggregateFunctionComboBox.SelectedItem
            End If

        End Sub

        Private Sub AggregateSumListDataListView_CellEditStarting(ByVal sender As Object, _
            ByVal e As BrightIdeasSoftware.CellEditEventArgs) Handles AggregateSumListDataListView.CellEditStarting

            e.AutoDispose = False

            If e.Column.AspectName = "RoundOrder" Then
                _RoundOrderNumericUpDown.Value = e.Value
                _RoundOrderNumericUpDown.Bounds = e.CellBounds
                e.Control = _RoundOrderNumericUpDown
            Else
                _AggregateFunctionComboBox.SelectedItem = e.Value
                _AggregateFunctionComboBox.Bounds = e.CellBounds
                e.Control = _AggregateFunctionComboBox
            End If

        End Sub

    End Class

End Namespace
