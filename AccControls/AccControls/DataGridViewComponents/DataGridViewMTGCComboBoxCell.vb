Public Class DataGridViewMTGCComboBoxCell
    Inherits DataGridViewTextBoxCell

    Public Overrides ReadOnly Property EditType() As Type
        Get
            ' Return the type of the editing contol that Cell uses.
            Return GetType(MTGCComboBoxEditingControl)
        End Get
    End Property

    Public Overrides ReadOnly Property ValueType() As Type
        Get
            ' Return the type of the value that Cell contains.
            Return GetType(String)
        End Get
    End Property

    Public Overrides Sub InitializeEditingControl(ByVal rowIndex As Integer, _
      ByVal initialFormattedValue As Object, _
      ByVal dataGridViewCellStyle As DataGridViewCellStyle)
        ' Set the value of the editing control to the current cell value.
        MyBase.InitializeEditingControl(rowIndex, _
          initialFormattedValue, dataGridViewCellStyle)
        _MTGCComboBoxEditingControl = CType(DataGridView.EditingControl, MTGCComboBoxEditingControl)


        _MTGCComboBoxEditingControl.ColumnNum = CType(OwningColumn, DataGridViewMTGCComboBoxColumn).ColumnNum
        _MTGCComboBoxEditingControl.ColumnWidth = CType(OwningColumn, DataGridViewMTGCComboBoxColumn).ColumnWidth
        _MTGCComboBoxEditingControl.BorderStyle = CType(OwningColumn, DataGridViewMTGCComboBoxColumn).BorderStyle

        If CType(OwningColumn, DataGridViewMTGCComboBoxColumn).DropDownArrowBackColor <> Color.Empty Then _
            _MTGCComboBoxEditingControl.DropDownArrowBackColor = _
            CType(OwningColumn, DataGridViewMTGCComboBoxColumn).DropDownArrowBackColor
        If CType(OwningColumn, DataGridViewMTGCComboBoxColumn).DropDownBackColor <> Color.Empty Then _
            _MTGCComboBoxEditingControl.DropDownBackColor = _
            CType(OwningColumn, DataGridViewMTGCComboBoxColumn).DropDownBackColor
        If CType(OwningColumn, DataGridViewMTGCComboBoxColumn).DropDownForeColor <> Color.Empty Then _
            _MTGCComboBoxEditingControl.DropDownForeColor = _
            CType(OwningColumn, DataGridViewMTGCComboBoxColumn).DropDownForeColor

        _MTGCComboBoxEditingControl.DropDownStyle = CType(OwningColumn, DataGridViewMTGCComboBoxColumn).DropDownStyle

        If CType(OwningColumn, DataGridViewMTGCComboBoxColumn).DropDownWidth > 0 Then _
            _MTGCComboBoxEditingControl.DropDownWidth = _
            CType(OwningColumn, DataGridViewMTGCComboBoxColumn).DropDownWidth

        _MTGCComboBoxEditingControl.FlatStyle = CType(OwningColumn, DataGridViewMTGCComboBoxColumn).FlatStyle

        If CType(OwningColumn, DataGridViewMTGCComboBoxColumn).GridLineColor <> Color.Empty Then _
            _MTGCComboBoxEditingControl.GridLineColor = _
            CType(OwningColumn, DataGridViewMTGCComboBoxColumn).GridLineColor

        _MTGCComboBoxEditingControl.GridLineHorizontal = _
            CType(OwningColumn, DataGridViewMTGCComboBoxColumn).GridLineHorizontal
        _MTGCComboBoxEditingControl.GridLineVertical = _
            CType(OwningColumn, DataGridViewMTGCComboBoxColumn).GridLineVertical
        _MTGCComboBoxEditingControl.LoadingType = _
            CType(OwningColumn, DataGridViewMTGCComboBoxColumn).LoadingType
        _MTGCComboBoxEditingControl.MaxDropDownItems = _
            CType(OwningColumn, DataGridViewMTGCComboBoxColumn).MaxDropDownItems
        _MTGCComboBoxEditingControl.SourceObjectAddEmptyItem = _
            CType(OwningColumn, DataGridViewMTGCComboBoxColumn).SourceObjectAddEmptyItem
        _MTGCComboBoxEditingControl.ValueForNothing = _
            CType(OwningColumn, DataGridViewMTGCComboBoxColumn).ValueForNothing

        If _MTGCComboBoxEditingControl.LoadingType = MTGCComboBox.CaricamentoCombo.CustomObject Then

            _MTGCComboBoxEditingControl.SourcePropertiesString = _
                CType(OwningColumn, DataGridViewMTGCComboBoxColumn).SourcePropertiesString
            _MTGCComboBoxEditingControl.SourceObject = Nothing
            _MTGCComboBoxEditingControl.SourceObject = _
                CType(OwningColumn, DataGridViewMTGCComboBoxColumn).SourceObject

        ElseIf _MTGCComboBoxEditingControl.LoadingType = MTGCComboBox.CaricamentoCombo.DataTable Then

            _MTGCComboBoxEditingControl.SourceDataString = _
                CType(OwningColumn, DataGridViewMTGCComboBoxColumn).SourceDataString
            _MTGCComboBoxEditingControl.SourceDataTable = Nothing
            _MTGCComboBoxEditingControl.SourceDataTable = _
                CType(OwningColumn, DataGridViewMTGCComboBoxColumn).SourceDataTable

        Else

            _MTGCComboBoxEditingControl.Items.Clear()
            If CType(OwningColumn, DataGridViewMTGCComboBoxColumn).Items IsNot Nothing Then _
                _MTGCComboBoxEditingControl.Items.AddRange(CType(OwningColumn, DataGridViewMTGCComboBoxColumn).Items)

        End If

        _MTGCComboBoxEditingControl.SelectedValue = Me.Value.ToString
    End Sub

    Private _MTGCComboBoxEditingControl As MTGCComboBoxEditingControl
    Friend ReadOnly Property MTGCComboBoxEditingControl() As MTGCComboBoxEditingControl
        Get
            Return _MTGCComboBoxEditingControl
        End Get
    End Property

    Public Overrides ReadOnly Property DefaultNewRowValue() As Object
        Get
            ' Use an empty string as the default value.
            Return ""
        End Get
    End Property

    Public Sub New()
        MyBase.New()
    End Sub


End Class
