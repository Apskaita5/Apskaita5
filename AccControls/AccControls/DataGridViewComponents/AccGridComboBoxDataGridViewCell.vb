Imports System.ComponentModel
Public Class AccGridComboBoxDataGridViewCell
    Inherits DataGridViewTextBoxCell

    Friend ReadOnly Property EditingAccGridComboBox() As AccGridComboBoxEditingControl
        Get
            Return TryCast(Me.DataGridView.EditingControl, AccGridComboBoxEditingControl)
        End Get
    End Property

    Public Overrides ReadOnly Property EditType() As Type
        Get
            Return GetType(AccGridComboBoxEditingControl)
        End Get
    End Property

    Public Overrides ReadOnly Property ValueType() As Type
        Get
            Return GetType(Object)
        End Get
    End Property

    Public Overrides Sub InitializeEditingControl(ByVal nRowIndex As Integer, _
        ByVal nInitialFormattedValue As Object, ByVal nDataGridViewCellStyle As DataGridViewCellStyle)

        MyBase.InitializeEditingControl(nRowIndex, nInitialFormattedValue, nDataGridViewCellStyle)

        Dim cEditBox As AccGridComboBoxEditingControl = TryCast(Me.DataGridView.EditingControl, AccGridComboBoxEditingControl)

        If cEditBox IsNot Nothing Then

            If Not MyBase.OwningColumn Is Nothing AndAlso Not DirectCast(MyBase.OwningColumn, _
                DataGridViewAccGridComboBoxColumn).ComboDataGridView Is Nothing Then

                cEditBox.AddToolStripDataGridView(DirectCast(MyBase.OwningColumn, _
                    DataGridViewAccGridComboBoxColumn).GetToolStripDataGridView)
                cEditBox.ValueMember = DirectCast(MyBase.OwningColumn, DataGridViewAccGridComboBoxColumn).ValueMember
                cEditBox.InstantBinding = DirectCast(MyBase.OwningColumn, DataGridViewAccGridComboBoxColumn).InstantBinding
                cEditBox.FilterPropertyName = DirectCast(MyBase.OwningColumn, DataGridViewAccGridComboBoxColumn).FilterPropertyName
                cEditBox.EmptyValueString = DirectCast(MyBase.OwningColumn, DataGridViewAccGridComboBoxColumn).EmptyValueString

            End If

            Try
                cEditBox.SelectedValue = Value
            Catch ex As Exception
                cEditBox.SelectedValue = Nothing
            End Try

        End If

    End Sub

    <EditorBrowsable(EditorBrowsableState.Advanced)> _
    Public Overrides Sub DetachEditingControl()
        Dim cDataGridView As DataGridView = Me.DataGridView
        If cDataGridView Is Nothing OrElse cDataGridView.EditingControl Is Nothing Then
            Throw New InvalidOperationException("Cell is detached or its grid has no editing control.")
        End If

        Dim EditBox As AccGridComboBoxEditingControl = TryCast(cDataGridView.EditingControl, AccGridComboBoxEditingControl)
        If EditBox IsNot Nothing Then
            ' avoid interferences between the editing sessions
            'EditBox.ClearUndo()
        End If

        MyBase.DetachEditingControl()
    End Sub

    Public Overrides Function ToString() As String
        Return "DataGridViewAccGridComboBoxCell{ColIndex=" & Me.ColumnIndex.ToString & _
            ", RowIndex=" & Me.RowIndex.ToString & "}"
    End Function

    Private Sub OnCommonChange()
        If Not Me.DataGridView Is Nothing AndAlso Not Me.DataGridView.IsDisposed _
            AndAlso Not Me.DataGridView.Disposing Then
            If Me.RowIndex = -1 Then
                Me.DataGridView.InvalidateColumn(Me.ColumnIndex)
            Else
                Me.DataGridView.UpdateCellValue(Me.ColumnIndex, Me.RowIndex)
            End If
        End If
    End Sub

    Private Function OwnsEditingControl(ByVal nRowIndex As Integer) As Boolean
        If nRowIndex = -1 OrElse Me.DataGridView Is Nothing OrElse _
            Me.DataGridView.IsDisposed OrElse Me.DataGridView.Disposing Then Return False

        Dim cEditingControl As AccGridComboBoxEditingControl = _
            TryCast(Me.DataGridView.EditingControl, AccGridComboBoxEditingControl)
        Return (cEditingControl IsNot Nothing AndAlso _
            nRowIndex = DirectCast(cEditingControl, IDataGridViewEditingControl).EditingControlRowIndex)
    End Function

    Protected Overrides Function SetValue(ByVal rowIndex As Integer, ByVal value As Object) As Boolean
        If Not Me.DataGridView Is Nothing AndAlso Not Me.DataGridView.EditingControl Is Nothing _
            AndAlso TypeOf Me.DataGridView.EditingControl Is AccGridComboBoxEditingControl Then
            Return MyBase.SetValue(rowIndex, DirectCast(Me.DataGridView.EditingControl, _
                AccGridComboBoxEditingControl).SelectedValue)
        Else
            Return MyBase.SetValue(rowIndex, value)
        End If
    End Function

End Class