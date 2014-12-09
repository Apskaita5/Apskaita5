Public Class AccTextBoxDataGridViewEditingControl
    Inherits AccTextBox
    Implements IDataGridViewEditingControl

    Private _dataGridView As DataGridView  ' grid owning this editing control
    Private _valueChanged As Boolean ' editing control's value has changed or not
    Private _rowIndex As Integer ' row index in which the editing control resides

    Public Sub New()
        TabStop = False ' control must not be part of the tabbing loop ???
        SupressFormating = True
    End Sub

    Public Sub ApplyCellStyleToEditingControl(ByVal dataGridViewCellStyle As _
        System.Windows.Forms.DataGridViewCellStyle) _
        Implements System.Windows.Forms.IDataGridViewEditingControl.ApplyCellStyleToEditingControl

        Me.Font = dataGridViewCellStyle.Font
        If (dataGridViewCellStyle.BackColor.A < 255) Then
            ' The NumericUpDown control does not support transparent back colors
            Dim opaqueBackColor As Color = Color.FromArgb(255, dataGridViewCellStyle.BackColor)
            Me.BackColor = opaqueBackColor
            _dataGridView.EditingPanel.BackColor = opaqueBackColor
        Else
            Me.BackColor = dataGridViewCellStyle.BackColor
        End If
        Me.ForeColor = dataGridViewCellStyle.ForeColor
        Me.TextAlign = DataGridViewNumericUpDownCell.TranslateAlignment(dataGridViewCellStyle.Alignment)

    End Sub

    Public Property EditingControlDataGridView() As System.Windows.Forms.DataGridView _
        Implements System.Windows.Forms.IDataGridViewEditingControl.EditingControlDataGridView
        Get
            Return _dataGridView
        End Get
        Set(ByVal value As System.Windows.Forms.DataGridView)
            _dataGridView = value
        End Set
    End Property

    Public Property EditingControlFormattedValue() As Object Implements System.Windows.Forms.IDataGridViewEditingControl.EditingControlFormattedValue
        Get
            Return GetEditingControlFormattedValue(DataGridViewDataErrorContexts.Formatting)
        End Get
        Set(ByVal value As Object)
            Dim result As Double
            If Not value Is Nothing AndAlso Double.TryParse(value, result) Then
                Me.DecimalValue = result
            End If
        End Set
    End Property

    Public Property EditingControlRowIndex() As Integer _
        Implements System.Windows.Forms.IDataGridViewEditingControl.EditingControlRowIndex
        Get
            Return _rowIndex
        End Get
        Set(ByVal value As Integer)
            _rowIndex = value
        End Set
    End Property

    Public Property EditingControlValueChanged() As Boolean _
        Implements System.Windows.Forms.IDataGridViewEditingControl.EditingControlValueChanged
        Get
            Return _valueChanged
        End Get
        Set(ByVal value As Boolean)
            _valueChanged = value
        End Set
    End Property

    Public Function EditingControlWantsInputKey(ByVal keyData As System.Windows.Forms.Keys, _
        ByVal dataGridViewWantsInputKey As Boolean) As Boolean _
        Implements System.Windows.Forms.IDataGridViewEditingControl.EditingControlWantsInputKey

        Select Case keyData And Keys.KeyCode
            Case Keys.Right
            Case Keys.Left
            Case Keys.Down
            Case Keys.Up
            Case Keys.Home
            Case Keys.End
            Case Keys.Delete
                Return True
        End Select

        Return Not dataGridViewWantsInputKey

    End Function

    Public ReadOnly Property EditingPanelCursor() As System.Windows.Forms.Cursor _
        Implements System.Windows.Forms.IDataGridViewEditingControl.EditingPanelCursor
        Get
            Return Cursors.Default
        End Get
    End Property

    Public Function GetEditingControlFormattedValue(ByVal context As _
        System.Windows.Forms.DataGridViewDataErrorContexts) As Object _
        Implements System.Windows.Forms.IDataGridViewEditingControl.GetEditingControlFormattedValue
        Return Me.Text
    End Function

    Public Sub PrepareEditingControlForEdit(ByVal selectAll As Boolean) _
        Implements System.Windows.Forms.IDataGridViewEditingControl.PrepareEditingControlForEdit

        If selectAll Then
            Me.SelectAll()
        Else
            Me.SelectionStart = Me.Text.Length
        End If

    End Sub

    Public ReadOnly Property RepositionEditingControlOnValueChange() As Boolean _
        Implements System.Windows.Forms.IDataGridViewEditingControl.RepositionEditingControlOnValueChange
        Get
            Return False
        End Get
    End Property

    Private Sub ValueChanged(ByVal sender As Object, ByVal e As EventArgs) _
        Handles Me.OnDecimalValueChanged
        If Not _valueChanged Then
            _valueChanged = True
            _dataGridView.NotifyCurrentCellDirty(True)
        End If
    End Sub

End Class
