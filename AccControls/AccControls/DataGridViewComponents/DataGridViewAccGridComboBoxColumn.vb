Imports System.ComponentModel
Public Class DataGridViewAccGridComboBoxColumn
    Inherits DataGridViewTextBoxColumn
    Implements IGridComboBox

    Private _IsLegalDispose As Boolean = False

    Public Sub New()
        Dim cell As AccGridComboBoxDataGridViewCell = New AccGridComboBoxDataGridViewCell
        MyBase.CellTemplate = cell
        MyBase.SortMode = DataGridViewColumnSortMode.Automatic
    End Sub

    Private ReadOnly Property AccGridComboBoxDataGridViewCellTemplate() As AccGridComboBoxDataGridViewCell
        Get
            Dim cell As AccGridComboBoxDataGridViewCell = TryCast(Me.CellTemplate, AccGridComboBoxDataGridViewCell)
            If cell Is Nothing Then Throw New InvalidOperationException( _
                "DataGridViewAccGridComboBoxColumn does not have a CellTemplate.")
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
            Dim cell As AccGridComboBoxDataGridViewCell = TryCast(value, AccGridComboBoxDataGridViewCell)
            If Not value Is Nothing AndAlso cell Is Nothing Then Throw New InvalidCastException( _
                "Value provided for CellTemplate must be of type DataGridViewAccTextBoxCell or derive from it.")
            MyBase.CellTemplate = value
        End Set
    End Property

    Private WithEvents myDataGridView As ToolStripDataGridView = Nothing
    Public Property ComboDataGridView() As DataGridView
        Get
            If Not myDataGridView Is Nothing Then Return myDataGridView.DataGridViewControl
            Return Nothing
        End Get
        Set(ByVal value As DataGridView)

            If Not myDataGridView Is Nothing Then
                Try
                    RemoveHandler myDataGridView.Disposed, AddressOf CheckForInvalidDispose
                Catch ex As Exception
                End Try
                DoDispose()
                myDataGridView = Nothing
                _IsLegalDispose = False
            End If

            If Not value Is Nothing Then

                myDataGridView = New ToolStripDataGridView(value, Nothing, _CloseOnSingleClick)
                AddHandler myDataGridView.Disposed, AddressOf CheckForInvalidDispose


            End If

        End Set
    End Property

    Private _ValueMember As String = ""
    Public Property ValueMember() As String
        Get
            Return _ValueMember
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then value = ""
            _ValueMember = value
        End Set
    End Property

    Private _FilterPropertyName As String = ""
    Public Property FilterPropertyName() As String
        Get
            Return _FilterPropertyName
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then value = ""
            _FilterPropertyName = value
        End Set
    End Property

    Private _CloseOnSingleClick As Boolean = True
    Public Property CloseOnSingleClick() As Boolean
        Get
            Return _CloseOnSingleClick
        End Get
        Set(ByVal value As Boolean)
            _CloseOnSingleClick = value
            If Not myDataGridView Is Nothing Then myDataGridView.CloseOnSingleClick = value
        End Set
    End Property

    Private _InstantBinding As Boolean = True
    Public Property InstantBinding() As Boolean
        Get
            Return _InstantBinding
        End Get
        Set(ByVal value As Boolean)
            _InstantBinding = value
        End Set
    End Property

    Private _EmptyValueString As String = ""
    Public Property EmptyValueString() As String
        Get
            Return _EmptyValueString
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then value = ""
            _EmptyValueString = value
        End Set
    End Property

    Public Overrides Function ToString() As String
        Return "DataGridViewAccGridComboBoxColumn{Name=" & Me.Name & ", Index=" & Me.Index.ToString & "}"
    End Function

    Friend Function GetToolStripDataGridView() As ToolStripDataGridView
        Return myDataGridView
    End Function

    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            DoDispose()
        End If
        MyBase.Dispose(disposing)
    End Sub


    Private Sub DoDispose()
        _IsLegalDispose = True
        If Not myDataGridView Is Nothing _
            AndAlso Not myDataGridView.DataGridViewControl Is Nothing _
            AndAlso Not myDataGridView.DataGridViewControl.IsDisposed Then _
            myDataGridView.DataGridViewControl.Dispose()
        If Not myDataGridView Is Nothing AndAlso Not myDataGridView.IsDisposed Then _
            myDataGridView.Dispose()
    End Sub

    Private Sub CheckForInvalidDispose(ByVal sender As Object, ByVal e As EventArgs)

        If Not _IsLegalDispose Then
            Throw New InvalidOperationException("ToolStripDataGridView was disposed prior to DataGridViewAccGridComboBoxColumn.")
        End If

    End Sub


    Public Function GetBindingContext() As System.Windows.Forms.BindingContext _
        Implements IGridComboBox.GetBindingContext
        If Me.DataGridView Is Nothing OrElse Me.DataGridView.FindForm Is Nothing Then Return Nothing
        Return Me.DataGridView.FindForm.BindingContext
    End Function

    Public Function HasAtachedGrid() As Boolean _
        Implements IGridComboBox.HasAtachedGrid
        Return Not myDataGridView Is Nothing
    End Function

    Public Sub SetCloseOnSingleClick(ByVal nCloseOnSingleClick As Boolean) _
        Implements IGridComboBox.SetCloseOnSingleClick
        _CloseOnSingleClick = nCloseOnSingleClick
        If Not myDataGridView Is Nothing Then myDataGridView.CloseOnSingleClick = nCloseOnSingleClick
    End Sub

    Public Sub SetFilterPropertyName(ByVal nFilterPropertyName As String) _
        Implements IGridComboBox.SetFilterPropertyName
        If nFilterPropertyName Is Nothing Then nFilterPropertyName = ""
        _FilterPropertyName = nFilterPropertyName
    End Sub

    Public Sub SetNestedDataGridView(ByVal grid As System.Windows.Forms.DataGridView) _
        Implements IGridComboBox.SetNestedDataGridView
        Me.ComboDataGridView = grid
    End Sub

    Public Sub SetValueMember(ByVal nValueMember As String) _
        Implements IGridComboBox.SetValueMember
        Me.ValueMember = nValueMember
    End Sub

    Public Sub SetEmptyValueString(ByVal nEmptyValueString As String) _
        Implements IGridComboBox.SetEmptyValueString
        Me.EmptyValueString = nEmptyValueString
    End Sub

End Class