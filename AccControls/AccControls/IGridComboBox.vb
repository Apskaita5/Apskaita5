Public Interface IGridComboBox


    Function HasAtachedGrid() As Boolean
    Function GetBindingContext() As BindingContext
    Sub SetNestedDataGridView(ByVal grid As DataGridView)
    Sub SetCloseOnSingleClick(ByVal nCloseOnSingleClick As Boolean)
    Sub SetFilterPropertyName(ByVal nFilterPropertyName As String)
    Sub SetValueMember(ByVal nValueMember As String)
    Sub SetEmptyValueString(ByVal nEmptyValueString As String)

End Interface
