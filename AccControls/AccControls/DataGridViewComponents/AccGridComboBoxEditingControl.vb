Imports System.Windows.Forms
Public Class AccGridComboBoxEditingControl
    Inherits AccGridComboBox
    Implements IDataGridViewEditingControl

    Public Sub New()
        TabStop = False ' control must not be part of the tabbing loop ???
    End Sub


    Private _dataGridView As DataGridView
    Public Property EditingControlDataGridView() As DataGridView _
        Implements IDataGridViewEditingControl.EditingControlDataGridView
        Get
            Return _dataGridView
        End Get
        Set(ByVal value As System.Windows.Forms.DataGridView)
            _dataGridView = value
        End Set
    End Property

    Public Property EditingControlFormattedValue() As Object Implements IDataGridViewEditingControl.EditingControlFormattedValue
        Get
            Return GetEditingControlFormattedValue(DataGridViewDataErrorContexts.Formatting)
        End Get
        Set(ByVal value As Object)
            Me.SelectedItem = value
        End Set
    End Property

    Private _rowIndex As Integer
    Public Property EditingControlRowIndex() As Integer Implements IDataGridViewEditingControl.EditingControlRowIndex
        Get
            Return _rowIndex
        End Get
        Set(ByVal value As Integer)
            _rowIndex = value
        End Set
    End Property

    Private _hasValueChanged As Boolean = False
    Public Property EditingControlValueChanged() As Boolean Implements IDataGridViewEditingControl.EditingControlValueChanged
        Get
            Return _hasValueChanged
        End Get
        Set(ByVal value As Boolean)
            _hasValueChanged = value
        End Set
    End Property

    Public ReadOnly Property EditingPanelCursor() As Cursor Implements IDataGridViewEditingControl.EditingPanelCursor
        Get
            Return MyBase.Cursor
        End Get
    End Property

    Public ReadOnly Property RepositionEditingControlOnValueChange() As Boolean Implements IDataGridViewEditingControl.RepositionEditingControlOnValueChange
        Get
            Return False
        End Get
    End Property

    Protected Overrides ReadOnly Property DisposeToolStripDataGridView() As Boolean
        Get
            Return False
        End Get
    End Property



    Public Sub ApplyCellStyleToEditingControl(ByVal dataGridViewCellStyle As DataGridViewCellStyle) _
            Implements IDataGridViewEditingControl.ApplyCellStyleToEditingControl
        Me.Font = dataGridViewCellStyle.Font
        Me.BackColor = dataGridViewCellStyle.BackColor
        Me.ForeColor = dataGridViewCellStyle.ForeColor
    End Sub

    Private Sub SelectedValueChangedHandler(ByVal sender As Object, ByVal e As EventArgs) Handles Me.SelectedValueChanged
        If Not _hasValueChanged Then
            _hasValueChanged = True
            _dataGridView.NotifyCurrentCellDirty(True)
        End If
    End Sub

    Public Function EditingControlWantsInputKey(ByVal keyData As Keys, ByVal dataGridViewWantsInputKey As Boolean) As Boolean Implements IDataGridViewEditingControl.EditingControlWantsInputKey
        Select Case keyData And Keys.KeyCode
            Case Keys.Up, Keys.Down, Keys.PageDown, Keys.PageUp, Keys.Enter, Keys.Escape, Keys.Delete
                Return True
            Case Else
                Return False
        End Select
    End Function

    Public Function GetEditingControlFormattedValue(ByVal context As DataGridViewDataErrorContexts) As Object Implements System.Windows.Forms.IDataGridViewEditingControl.GetEditingControlFormattedValue
        Return Me.Text
    End Function

    Public Sub PrepareEditingControlForEdit(ByVal selectAll As Boolean) Implements IDataGridViewEditingControl.PrepareEditingControlForEdit

    End Sub

End Class