Imports System.Windows.Forms
Imports System.ComponentModel
Imports AccControls.DataGridViewAccTextBoxCell
Public Class DataGridViewAccTextBoxColumn
    Inherits DataGridViewTextBoxColumn

    Private Const m_allowMaxDecimalLength As Integer = 6

    Public Sub New()
        Dim cell As DataGridViewAccTextBoxCell = New DataGridViewAccTextBoxCell
        MyBase.CellTemplate = cell
        MyBase.SortMode = DataGridViewColumnSortMode.Automatic
        MyBase.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
        MyBase.DefaultCellStyle.Format = "##,0.00"
    End Sub

    Private ReadOnly Property AccTextBoxTemplate() As DataGridViewAccTextBoxCell
        Get
            Dim cell As DataGridViewAccTextBoxCell = TryCast(Me.CellTemplate, DataGridViewAccTextBoxCell)
            If cell Is Nothing Then Throw New InvalidOperationException( _
                "TNumEditDataGridViewColumn does not have a CellTemplate.")
            Return cell
        End Get
    End Property

    <Browsable(False)> _
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
    Public Overrides Property CellTemplate() As System.Windows.Forms.DataGridViewCell
        Get
            Return MyBase.CellTemplate
        End Get
        Set(ByVal value As System.Windows.Forms.DataGridViewCell)
            Dim cell As DataGridViewAccTextBoxCell = TryCast(value, DataGridViewAccTextBoxCell)
            If Not value Is Nothing AndAlso cell Is Nothing Then
                Throw New InvalidCastException("Value provided for CellTemplate must be of type DataGridViewAccTextBoxCell or derive from it.")
            End If
            MyBase.CellTemplate = value
        End Set
    End Property

    <Category("Appearance")> _
    <Description("Set/Get dot length(0 is integer, 10 is maximum).")> _
    <DefaultValue(DATAGRIDVIEWACCTEXTBOXCELLDEFAULT_decimalLength)> _
    Public Property DecimalLength() As Integer
        Get
            Return Me.AccTextBoxTemplate.DecimalLength
        End Get
        Set(ByVal value As Integer)
            If Me.AccTextBoxTemplate.DecimalLength <> value Then

                If value < 0 OrElse value > m_allowMaxDecimalLength Then
                    Throw New ArgumentOutOfRangeException("The DecimalLength must be between 0 and " _
                        & m_allowMaxDecimalLength.ToString() & ".")
                End If

                MyBase.DefaultCellStyle.Format = AccTextBox.GetValueFormatString(value)
                Me.AccTextBoxTemplate.DecimalLength = value

                ' Update all the existing DataGridViewAccTextBoxCell cells in the column accordingly.
                If Not Me.DataGridView Is Nothing Then

                    Dim cDataGridViewRows As DataGridViewRowCollection = Me.DataGridView.Rows
                    Dim rowCount As Integer = cDataGridViewRows.Count

                    For i As Integer = 1 To rowCount

                        ' Be careful not to unshare rows unnecessarily. 
                        ' This could have severe performance repercussions.
                        Dim cDataGridViewRow As DataGridViewRow = cDataGridViewRows.SharedRow(i - 1)
                        Dim cDataGridViewCell As DataGridViewAccTextBoxCell = _
                            TryCast(cDataGridViewRow.Cells(Me.Index), DataGridViewAccTextBoxCell)

                        If Not cDataGridViewCell Is Nothing Then
                            ' Call the internal SetDecimalPlaces method instead of the property 
                            ' to avoid invalidation of each cell. The whole column is invalidated 
                            ' later in a single operation for better performance.
                            cDataGridViewCell.SetDecimalLength(i - 1, value)
                        End If

                    Next

                    Me.DataGridView.InvalidateColumn(Me.Index)

                End If
            End If
        End Set
    End Property

    <Category("Appearance")> _
    <Description("Number can be negative or not.")> _
    <DefaultValue(DATAGRIDVIEWACCTEXTBOXCELLDEFAULT_allowNegative)> _
    Public Property AllowNegative() As Boolean
        Get
            Return Me.AccTextBoxTemplate.AllowNegative
        End Get
        Set(ByVal value As Boolean)

            If value <> Me.AccTextBoxTemplate.AllowNegative Then

                Me.AccTextBoxTemplate.AllowNegative = value

                ' Update all the existing DataGridViewAccTextBoxCell cells in the column accordingly.
                If Not Me.DataGridView Is Nothing Then

                    Dim cDataGridViewRows As DataGridViewRowCollection = Me.DataGridView.Rows
                    Dim rowCount As Integer = cDataGridViewRows.Count

                    For i As Integer = 1 To rowCount

                        ' Be careful not to unshare rows unnecessarily. 
                        ' This could have severe performance repercussions.
                        Dim cDataGridViewRow As DataGridViewRow = cDataGridViewRows.SharedRow(i - 1)
                        Dim cDataGridViewCell As DataGridViewAccTextBoxCell = _
                            TryCast(cDataGridViewRow.Cells(Me.Index), DataGridViewAccTextBoxCell)

                        If Not cDataGridViewCell Is Nothing Then
                            ' Call the internal EnableNegative method instead of the property 
                            ' to avoid invalidation of each cell. The whole column is invalidated 
                            ' later in a single operation for better performance.
                            cDataGridViewCell.EnableNegative(i - 1, value)
                        End If

                    Next

                    Me.DataGridView.InvalidateColumn(Me.Index)

                End If

            End If

        End Set
    End Property

    Public Overrides Function ToString() As String
        Return "DataGridViewAccTextBoxColumn{Name=" & Me.Name & ", Index=" & Me.Index.ToString & "}"
    End Function

End Class
