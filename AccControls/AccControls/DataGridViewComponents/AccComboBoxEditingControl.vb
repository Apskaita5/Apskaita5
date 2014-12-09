Public Class AccComboBoxEditingControl
    Inherits AccComboBox
    Implements IDataGridViewEditingControl


    Public Sub ApplyCellStyleToEditingControl(ByVal dataGridViewCellStyle _
        As System.Windows.Forms.DataGridViewCellStyle) _
        Implements System.Windows.Forms.IDataGridViewEditingControl.ApplyCellStyleToEditingControl

    End Sub

    Private _dataGridView As DataGridView
    Public Property EditingControlDataGridView() As System.Windows.Forms.DataGridView Implements System.Windows.Forms.IDataGridViewEditingControl.EditingControlDataGridView
        Get
            Return _dataGridView
        End Get
        Set(ByVal value As System.Windows.Forms.DataGridView)
            _dataGridView = value
        End Set
    End Property

    Public Property EditingControlFormattedValue() As Object _
        Implements System.Windows.Forms.IDataGridViewEditingControl.EditingControlFormattedValue
        Get
            Return Me.SelectedItem
        End Get
        Set(ByVal value As Object)
            Me.SelectedItem = value
        End Set
    End Property

    Private _rowIndex As Integer
    Public Property EditingControlRowIndex() As Integer Implements System.Windows.Forms.IDataGridViewEditingControl.EditingControlRowIndex
        Get
            Return _rowIndex
        End Get
        Set(ByVal value As Integer)
            _rowIndex = value
        End Set
    End Property

    Protected Overrides Sub OnSelectedValueChanged(ByVal e As System.EventArgs)
        _hasValueChanged = True
        Me.EditingControlDataGridView.NotifyCurrentCellDirty(True)
        MyBase.OnSelectedItemChanged(e)

    End Sub

    Protected Overrides Sub OnDropDownClosed(ByVal e As System.EventArgs)
        _hasValueChanged = True
        Me.EditingControlDataGridView.NotifyCurrentCellDirty(True)
        MyBase.OnDropDownClosed(e)
    End Sub

    Private _hasValueChanged As Boolean = False
    Public Property EditingControlValueChanged() As Boolean Implements System.Windows.Forms.IDataGridViewEditingControl.EditingControlValueChanged
        Get
            Return _hasValueChanged
        End Get
        Set(ByVal value As Boolean)
            _hasValueChanged = value
        End Set
    End Property

    Public Function EditingControlWantsInputKey(ByVal keyData As System.Windows.Forms.Keys, _
        ByVal dataGridViewWantsInputKey As Boolean) As Boolean _
        Implements System.Windows.Forms.IDataGridViewEditingControl.EditingControlWantsInputKey
        Select Case keyData And Keys.KeyCode
            Case Keys.Up, Keys.Down, Keys.PageDown, Keys.PageUp
                Return True
            Case Else
                Return False
        End Select
    End Function

    Public ReadOnly Property EditingPanelCursor() As System.Windows.Forms.Cursor Implements System.Windows.Forms.IDataGridViewEditingControl.EditingPanelCursor
        Get
            Return MyBase.Cursor
        End Get
    End Property

    Public Function GetEditingControlFormattedValue(ByVal context As _
        System.Windows.Forms.DataGridViewDataErrorContexts) As Object _
        Implements System.Windows.Forms.IDataGridViewEditingControl.GetEditingControlFormattedValue
        Return EditingControlFormattedValue
    End Function

    Public Sub PrepareEditingControlForEdit(ByVal selectAll As Boolean) _
        Implements System.Windows.Forms.IDataGridViewEditingControl.PrepareEditingControlForEdit

    End Sub

    Public ReadOnly Property RepositionEditingControlOnValueChange() As Boolean _
        Implements System.Windows.Forms.IDataGridViewEditingControl.RepositionEditingControlOnValueChange
        Get
            Return False
        End Get
    End Property

End Class
