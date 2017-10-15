Imports System.ComponentModel
Imports BrightIdeasSoftware
Imports System.Reflection
Imports System.Windows.Forms

Namespace DataAggregator

    ''' <summary>
    ''' Represents a list of <see cref="AggregateSum">AggregateSum</see> for a source datatable
    ''' (an item per data column).
    ''' </summary>
    ''' <remarks></remarks>
    Public Class AggregateSumList
        Inherits BindingList(Of AggregateSum)

        ''' <summary>
        ''' Creates a new AggregateSumList instance.
        ''' </summary>
        ''' <param name="table">a datatable containing double data series that should be aggregated</param>
        ''' <remarks></remarks>
        Public Sub New(ByVal table As DataTable)

            If table Is Nothing Then Throw New ArgumentNullException("table")

            Me.RaiseListChangedEvents = False
            For Each column As DataColumn In table.Columns
                Add(New AggregateSum(table, column))
            Next
            Me.RaiseListChangedEvents = True

            Me.AllowEdit = True
            Me.AllowNew = False
            Me.AllowRemove = False

        End Sub

        ''' <summary>
        ''' Extracts data displayed in a ObjectListView to a datatable (only double and decimal values
        ''' are extracted).
        ''' </summary>
        ''' <param name="olv">an ObjectListView that contains some databound data</param>
        ''' <param name="itemType">a type of the items that are databound to the ObjectListView</param>
        ''' <remarks></remarks>
        Public Shared Function GetObjectListViewDataTable(ByVal olv As ObjectListView, _
            ByVal itemType As Type) As DataTable

            If olv Is Nothing Then Throw New ArgumentNullException("olv")
            If itemType Is Nothing Then Throw New ArgumentNullException("itemType")

            Dim table As New DataTable

            Dim propInfo As PropertyInfo
            For Each column As OLVColumn In olv.Columns

                propInfo = Nothing
                Try
                    propInfo = itemType.GetProperty(column.AspectName)
                Catch ex As Exception
                End Try

                If Not propInfo Is Nothing AndAlso (propInfo.PropertyType Is GetType(Double) _
                    OrElse propInfo.PropertyType Is GetType(Decimal)) Then

                    Dim newColumn As DataColumn = table.Columns.Add(column.Text, GetType(Double))
                    newColumn.Caption = column.AspectName

                End If

            Next

            If table.Columns.Count < 1 Then
                Throw New InvalidOperationException("No displayed numeric columns in table.")
            End If

            Dim lastDisplayedItem As OLVListItem = olv.GetLastItemInDisplayOrder()

            Dim parentList As IList = Nothing

            If Not olv.Objects Is Nothing Then

                If TypeOf olv.Objects Is BindingSource Then
                    Try
                        parentList = DirectCast(olv.Objects, BindingSource).List
                    Catch ex As Exception
                    End Try
                Else
                    Try
                        parentList = DirectCast(olv.Objects, IList)
                    Catch ex As Exception
                    End Try
                End If

            End If

            If parentList Is Nothing OrElse parentList.Count < 1 Then Return table

            For i As Integer = 0 To olv.GetDisplayOrderOfItemIndex(lastDisplayedItem.Index)
                Dim current As Object = olv.GetNthItemInDisplayOrder(i).RowObject
                Dim dr As DataRow = table.Rows.Add()
                For Each col As DataColumn In table.Columns
                    dr.Item(col) = Convert.ToDouble(itemType.GetProperty(col.Caption).GetValue(current, Nothing))
                Next
            Next

            Return table

        End Function

    End Class

End Namespace