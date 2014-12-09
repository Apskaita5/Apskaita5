Public Class AccBoxEditingControl
    Inherits AccBox
    Implements IDataGridViewEditingControl

    Public Sub ApplyCellStyleToEditingControl(ByVal dataGridViewCellStyle _
        As System.Windows.Forms.DataGridViewCellStyle) _
        Implements System.Windows.Forms.IDataGridViewEditingControl.ApplyCellStyleToEditingControl

    End Sub

    Private _dataGridView As DataGridView
    Public Property EditingControlDataGridView() As System.Windows.Forms.DataGridView _
        Implements System.Windows.Forms.IDataGridViewEditingControl.EditingControlDataGridView
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
            Return Me.D_Parser(Me.Text, Me.AddZeros)
        End Get
        Set(ByVal value As Object)
            If Not value Is Nothing AndAlso Not System.Convert.IsDBNull(value) _
                AndAlso Double.TryParse(value.ToString.Replace(System.Threading.Thread. _
                CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator, ""), New Double) Then
                Me.DecimalValue = CRound(CDbl(value))
            Else
                Me.DecimalValue = 0
            End If
        End Set
    End Property

    Private _rowIndex As Integer
    Public Property EditingControlRowIndex() As Integer _
        Implements System.Windows.Forms.IDataGridViewEditingControl.EditingControlRowIndex
        Get
            Return _rowIndex
        End Get
        Set(ByVal value As Integer)
            _rowIndex = value
        End Set
    End Property

    Private _hasValueChanged As Boolean = False
    Public Property EditingControlValueChanged() As Boolean _
        Implements System.Windows.Forms.IDataGridViewEditingControl.EditingControlValueChanged
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

        If keyData = Keys.Left OrElse keyData = Keys.Right _
            OrElse keyData = Keys.Home OrElse keyData = Keys.End _
            OrElse keyData = Keys.Delete Then Return True
        Return Not dataGridViewWantsInputKey

    End Function

    Public ReadOnly Property EditingPanelCursor() As System.Windows.Forms.Cursor _
        Implements System.Windows.Forms.IDataGridViewEditingControl.EditingPanelCursor
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

    Public Sub OnValueChanged(ByVal sender As Object, _
        ByVal e As EventArgs) Handles Me.OnDecimalValueChanged

        _hasValueChanged = True
        Me.EditingControlDataGridView.NotifyCurrentCellDirty(True)

    End Sub

End Class
