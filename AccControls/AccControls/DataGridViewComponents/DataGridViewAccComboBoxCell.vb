Public Class DataGridViewAccComboBoxCell
    Inherits DataGridViewComboBoxCell

    Public Overrides ReadOnly Property EditType() As System.Type
        Get
            Return GetType(AccComboBoxEditingControl)
        End Get
    End Property

    Public Overrides ReadOnly Property ValueType() As Type
        Get
            ' Return the type of the value that Cell contains.
            Return GetType(Object)
        End Get
    End Property

    Public Overrides Sub InitializeEditingControl(ByVal rowIndex As Integer, _
        ByVal initialFormattedValue As Object, ByVal dataGridViewCellStyle _
        As System.Windows.Forms.DataGridViewCellStyle)

        MyBase.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle)

        _AccComboBoxEditingControl = CType(DataGridView.EditingControl, AccComboBoxEditingControl)

        If CType(OwningColumn, DataGridViewAccComboBoxColumn).DropDownWidth > 0 Then _
            _AccComboBoxEditingControl.DropDownWidth = _
            CType(OwningColumn, DataGridViewAccComboBoxColumn).DropDownWidth
        _AccComboBoxEditingControl.MaxDropDownItems = _
            CType(OwningColumn, DataGridViewAccComboBoxColumn).MaxDropDownItems
        _AccComboBoxEditingControl.FlatStyle = _
            CType(OwningColumn, DataGridViewAccComboBoxColumn).FlatStyle
        _AccComboBoxEditingControl.DataSource = _
            CType(OwningColumn, DataGridViewAccComboBoxColumn).DataSource

        _AccComboBoxEditingControl.SelectedItem = Me.Value

    End Sub

    Private _AccComboBoxEditingControl As AccComboBoxEditingControl
    Friend ReadOnly Property AccComboBoxEditingControl() As AccComboBoxEditingControl
        Get
            Return _AccComboBoxEditingControl
        End Get
    End Property

    Public Overrides ReadOnly Property DefaultNewRowValue() As Object
        Get
            Return Nothing
        End Get
    End Property

    Public Sub New()
        MyBase.New()
    End Sub

    Public Overloads Property Value()
        Get
            Return CType(DataGridView.EditingControl, AccComboBoxEditingControl).SelectedItem
        End Get
        Set(ByVal value)
            CType(DataGridView.EditingControl, AccComboBoxEditingControl).SelectedItem = value
        End Set
    End Property

End Class
