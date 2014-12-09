Imports System
Imports System.Text
Imports System.Diagnostics
Imports System.Globalization
Imports System.Windows.Forms
Imports System.ComponentModel

''' <summary>
''' Custom column type dedicated to the DataGridViewNumericUpDownCell cell type.
''' </summary>
Public Class DataGridViewNumericUpDownColumn
    Inherits DataGridViewColumn

    ''' <summary>
    ''' Constructor for the DataGridViewNumericUpDownColumn class.
    ''' </summary>
    Public Sub New()
        MyBase.New(New DataGridViewNumericUpDownCell)

    End Sub

    ''' <summary>
    ''' Represents the implicit cell that gets cloned when adding rows to the grid.
    ''' </summary>
    <Browsable(False), _
     DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Public Overrides Property CellTemplate() As DataGridViewCell
        Get
            Return MyBase.CellTemplate
        End Get
        Set(ByVal value As DataGridViewCell)
            Dim dataGridViewNumericUpDownCell As DataGridViewNumericUpDownCell = CType(value, DataGridViewNumericUpDownCell)
            If Not value Is Nothing AndAlso dataGridViewNumericUpDownCell Is Nothing Then
                Throw New InvalidCastException("Value provided for CellTemplate must be of type DataGridViewNumericUpDownElements.DataGridViewNumeric" & _
                    "UpDownCell or derive from it.")
            End If
            MyBase.CellTemplate = value
        End Set
    End Property

    ''' <summary>
    ''' Replicates the DecimalPlaces property of the DataGridViewNumericUpDownCell cell type.
    ''' </summary>
    <Category("Appearance"), _
     DefaultValue(DataGridViewNumericUpDownCell.DATAGRIDVIEWNUMERICUPDOWNCELL_defaultDecimalPlaces), _
     Description("Indicates the number of decimal places to display.")> _
    Public Property DecimalPlaces() As Integer
        Get
            If (Me.NumericUpDownCellTemplate Is Nothing) Then
                Throw New InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.")
            End If
            Return Me.NumericUpDownCellTemplate.DecimalPlaces
        End Get
        Set(ByVal value As Integer)
            If (Me.NumericUpDownCellTemplate Is Nothing) Then
                Throw New InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.")
            End If
            ' Update the template cell so that subsequent cloned cells use the new value.
            Me.NumericUpDownCellTemplate.DecimalPlaces = value
            If (Not (Me.DataGridView) Is Nothing) Then
                ' Update all the existing DataGridViewNumericUpDownCell cells in the column accordingly.
                Dim dataGridViewRows As DataGridViewRowCollection = Me.DataGridView.Rows
                Dim rowCount As Integer = dataGridViewRows.Count
                Dim rowIndex As Integer = 0
                Do While (rowIndex < rowCount)
                    ' Be careful not to unshare rows unnecessarily. 
                    ' This could have severe performance repercussions.
                    Dim dataGridViewRow As DataGridViewRow = dataGridViewRows.SharedRow(rowIndex)
                    Dim dataGridViewCell As DataGridViewNumericUpDownCell = CType(dataGridViewRow.Cells(Me.Index), DataGridViewNumericUpDownCell)
                    If (Not (dataGridViewCell) Is Nothing) Then
                        ' Call the internal SetDecimalPlaces method instead of the property to avoid invalidation 
                        ' of each cell. The whole column is invalidated later in a single operation for better performance.
                        dataGridViewCell.SetDecimalPlaces(rowIndex, value)
                    End If
                    rowIndex = (rowIndex + 1)
                Loop
                Me.DataGridView.InvalidateColumn(Me.Index)
                ' TODO: Call the grid's autosizing methods to autosize the column, rows, column headers / row headers as needed.
            End If
        End Set
    End Property

    ''' <summary>
    ''' Replicates the Increment property of the DataGridViewNumericUpDownCell cell type.
    ''' </summary>
    <Category("Data"), _
     Description("Indicates the amount to increment or decrement on each button click.")> _
    Public Property Increment() As Decimal
        Get
            If (Me.NumericUpDownCellTemplate Is Nothing) Then
                Throw New InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.")
            End If
            Return Me.NumericUpDownCellTemplate.Increment
        End Get
        Set(ByVal value As Decimal)
            If (Me.NumericUpDownCellTemplate Is Nothing) Then
                Throw New InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.")
            End If
            Me.NumericUpDownCellTemplate.Increment = value
            If (Not (Me.DataGridView) Is Nothing) Then
                Dim dataGridViewRows As DataGridViewRowCollection = Me.DataGridView.Rows
                Dim rowCount As Integer = dataGridViewRows.Count
                Dim rowIndex As Integer = 0
                Do While (rowIndex < rowCount)
                    Dim dataGridViewRow As DataGridViewRow = dataGridViewRows.SharedRow(rowIndex)
                    Dim dataGridViewCell As DataGridViewNumericUpDownCell = CType(dataGridViewRow.Cells(Me.Index), DataGridViewNumericUpDownCell)
                    If (Not (dataGridViewCell) Is Nothing) Then
                        dataGridViewCell.SetIncrement(rowIndex, value)
                    End If
                    rowIndex = (rowIndex + 1)
                Loop
            End If
        End Set
    End Property

    ''' Indicates whether the Increment property should be persisted.
    Private Function ShouldSerializeIncrement() As Boolean
        Return Not Me.Increment.Equals(DataGridViewNumericUpDownCell. _
            DATAGRIDVIEWNUMERICUPDOWNCELL_defaultIncrement)
    End Function

    ''' <summary>
    ''' Replicates the Maximum property of the DataGridViewNumericUpDownCell cell type.
    ''' </summary>
    <Category("Data"), _
     Description("Indicates the maximum value for the numeric up-down cells."), _
     RefreshProperties(RefreshProperties.All)> _
    Public Property Maximum() As Decimal
        Get
            If (Me.NumericUpDownCellTemplate Is Nothing) Then
                Throw New InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.")
            End If
            Return Me.NumericUpDownCellTemplate.Maximum
        End Get
        Set(ByVal value As Decimal)
            If (Me.NumericUpDownCellTemplate Is Nothing) Then
                Throw New InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.")
            End If
            Me.NumericUpDownCellTemplate.Maximum = value
            If (Not (Me.DataGridView) Is Nothing) Then
                Dim dataGridViewRows As DataGridViewRowCollection = Me.DataGridView.Rows
                Dim rowCount As Integer = dataGridViewRows.Count
                Dim rowIndex As Integer = 0
                Do While (rowIndex < rowCount)
                    Dim dataGridViewRow As DataGridViewRow = dataGridViewRows.SharedRow(rowIndex)
                    Dim dataGridViewCell As DataGridViewNumericUpDownCell = CType(dataGridViewRow.Cells(Me.Index), DataGridViewNumericUpDownCell)
                    If (Not (dataGridViewCell) Is Nothing) Then
                        dataGridViewCell.SetMaximum(rowIndex, value)
                    End If
                    rowIndex = (rowIndex + 1)
                Loop
                Me.DataGridView.InvalidateColumn(Me.Index)
                ' TODO: This column and/or grid rows may need to be autosized depending on their
                '       autosize settings. Call the autosizing methods to autosize the column, rows, 
                '       column headers / row headers as needed.
            End If
        End Set
    End Property

    ''' Indicates whether the Maximum property should be persisted.
    Private Function ShouldSerializeMaximum() As Boolean
        Return Not Me.Maximum.Equals(DataGridViewNumericUpDownCell. _
            DATAGRIDVIEWNUMERICUPDOWNCELL_defaultMaximum)
    End Function

    ''' <summary>
    ''' Replicates the Minimum property of the DataGridViewNumericUpDownCell cell type.
    ''' </summary>
    <Category("Data"), _
     Description("Indicates the minimum value for the numeric up-down cells."), _
     RefreshProperties(RefreshProperties.All)> _
    Public Property Minimum() As Decimal
        Get
            If (Me.NumericUpDownCellTemplate Is Nothing) Then
                Throw New InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.")
            End If
            Return Me.NumericUpDownCellTemplate.Minimum
        End Get
        Set(ByVal value As Decimal)
            If (Me.NumericUpDownCellTemplate Is Nothing) Then
                Throw New InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.")
            End If
            Me.NumericUpDownCellTemplate.Minimum = value
            If (Not (Me.DataGridView) Is Nothing) Then
                Dim dataGridViewRows As DataGridViewRowCollection = Me.DataGridView.Rows
                Dim rowCount As Integer = dataGridViewRows.Count
                Dim rowIndex As Integer = 0
                Do While (rowIndex < rowCount)
                    Dim dataGridViewRow As DataGridViewRow = dataGridViewRows.SharedRow(rowIndex)
                    Dim dataGridViewCell As DataGridViewNumericUpDownCell = CType(dataGridViewRow.Cells(Me.Index), DataGridViewNumericUpDownCell)
                    If (Not (dataGridViewCell) Is Nothing) Then
                        dataGridViewCell.SetMinimum(rowIndex, value)
                    End If
                    rowIndex = (rowIndex + 1)
                Loop
                Me.DataGridView.InvalidateColumn(Me.Index)
                ' TODO: This column and/or grid rows may need to be autosized depending on their
                '       autosize settings. Call the autosizing methods to autosize the column, rows, 
                '       column headers / row headers as needed.
            End If
        End Set
    End Property

    ''' Indicates whether the Maximum property should be persisted.
    Private Function ShouldSerializeMinimum() As Boolean
        Return Not Me.Minimum.Equals(DataGridViewNumericUpDownCell. _
            DATAGRIDVIEWNUMERICUPDOWNCELL_defaultMinimum)
    End Function

    ''' <summary>
    ''' Replicates the ThousandsSeparator property of the DataGridViewNumericUpDownCell cell type.
    ''' </summary>
    <Category("Data"), _
     DefaultValue(DataGridViewNumericUpDownCell.DATAGRIDVIEWNUMERICUPDOWNCELL_defaultThousandsSeparator), _
     Description("Indicates whether the thousands separator will be inserted between every three decimal digits.")> _
    Public Property ThousandsSeparator() As Boolean
        Get
            If (Me.NumericUpDownCellTemplate Is Nothing) Then
                Throw New InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.")
            End If
            Return Me.NumericUpDownCellTemplate.ThousandsSeparator
        End Get
        Set(ByVal value As Boolean)
            If (Me.NumericUpDownCellTemplate Is Nothing) Then
                Throw New InvalidOperationException("Operation cannot be completed because this DataGridViewColumn does not have a CellTemplate.")
            End If
            Me.NumericUpDownCellTemplate.ThousandsSeparator = value
            If (Not (Me.DataGridView) Is Nothing) Then
                Dim dataGridViewRows As DataGridViewRowCollection = Me.DataGridView.Rows
                Dim rowCount As Integer = dataGridViewRows.Count
                Dim rowIndex As Integer = 0
                Do While (rowIndex < rowCount)
                    Dim dataGridViewRow As DataGridViewRow = dataGridViewRows.SharedRow(rowIndex)
                    Dim dataGridViewCell As DataGridViewNumericUpDownCell = CType(dataGridViewRow.Cells(Me.Index), DataGridViewNumericUpDownCell)
                    If (Not (dataGridViewCell) Is Nothing) Then
                        dataGridViewCell.SetThousandsSeparator(rowIndex, value)
                    End If
                    rowIndex = (rowIndex + 1)
                Loop
                Me.DataGridView.InvalidateColumn(Me.Index)
                ' TODO: This column and/or grid rows may need to be autosized depending on their
                '       autosize settings. Call the autosizing methods to autosize the column, rows, 
                '       column headers / row headers as needed.
            End If
        End Set
    End Property

    ''' <summary>
    ''' Small utility function that returns the template cell as a DataGridViewNumericUpDownCell
    ''' </summary>
    Private ReadOnly Property NumericUpDownCellTemplate() As DataGridViewNumericUpDownCell
        Get
            Return CType(Me.CellTemplate, DataGridViewNumericUpDownCell)
        End Get
    End Property

    ''' <summary>
    ''' Returns a standard compact string representation of the column.
    ''' </summary>
    Public Overrides Function ToString() As String
        Dim sb As StringBuilder = New StringBuilder(100)
        sb.Append("DataGridViewNumericUpDownColumn { Name=")
        sb.Append(Me.Name)
        sb.Append(", Index=")
        sb.Append(Me.Index.ToString(CultureInfo.CurrentCulture))
        sb.Append(" }")
        Return sb.ToString
    End Function

End Class