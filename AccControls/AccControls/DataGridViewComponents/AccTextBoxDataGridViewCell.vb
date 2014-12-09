Imports System.Windows.Forms
Imports System.ComponentModel
Public Class DataGridViewAccTextBoxCell
    Inherits DataGridViewTextBoxCell

    Friend Const DATAGRIDVIEWACCTEXTBOXCELLDEFAULT_decimalLength As Integer = CType(2, Integer)
    Friend Const DATAGRIDVIEWACCTEXTBOXCELLDEFAULT_allowNegative As Boolean = True

    Private _decimalLength As Integer
    Private _allowNegative As Boolean

    Public Sub New()
        MyBase.New()
        _decimalLength = DATAGRIDVIEWACCTEXTBOXCELLDEFAULT_decimalLength
        _allowNegative = DATAGRIDVIEWACCTEXTBOXCELLDEFAULT_allowNegative
    End Sub


    <Category("Custom")> _
    <Description("Set/Get dot length(0 is integer, 10 is maximum).")> _
    <DefaultValue(DATAGRIDVIEWACCTEXTBOXCELLDEFAULT_decimalLength)> _
    Public Property DecimalLength() As Integer
        Get
            Return _decimalLength
        End Get
        Set(ByVal value As Integer)
            If _decimalLength <> value Then
                SetDecimalLength(Me.RowIndex, value)
                OnCommonChange()
            End If
        End Set
    End Property

    <Category("Custom")> _
    <Description("Number can be negative or not.")> _
    <DefaultValue(DATAGRIDVIEWACCTEXTBOXCELLDEFAULT_allowNegative)> _
    Public Property AllowNegative() As Boolean
        Get
            Return _allowNegative
        End Get
        Set(ByVal value As Boolean)
            If _allowNegative <> value Then
                _allowNegative = value
                EnableNegative(Me.RowIndex, value)
            End If
        End Set
    End Property

    Private ReadOnly Property EditingAccTextBox() As AccTextBoxDataGridViewEditingControl
        Get
            Return TryCast(Me.DataGridView.EditingControl, AccTextBoxDataGridViewEditingControl)
        End Get
    End Property

    Public Overrides ReadOnly Property EditType() As Type
        Get
            Return GetType(AccTextBoxDataGridViewEditingControl)
        End Get
    End Property

    Public Overrides ReadOnly Property ValueType() As Type
        Get
            Return GetType(Double)
        End Get
    End Property

    ''' <summary>
    ''' If set 0/1.23 to two cells, it will throw Exception when sort by clicking column header.
    ''' Override this method to ensure the type of value.
    ''' </summary>
    Protected Overrides Function SetValue(ByVal nRowIndex As Integer, ByVal nValue As Object) As Boolean
        Dim val As Double = 0
        Try
            val = Math.Round(System.Convert.ToDouble(nValue), _decimalLength)
        Catch
        End Try
        Return MyBase.SetValue(nRowIndex, val)
        ' if set 0 and 1.23, it will throw exception when sort
    End Function

    Public Overrides Function Clone() As Object
        Dim cDataGridViewCell As DataGridViewAccTextBoxCell = _
            TryCast(MyBase.Clone(), DataGridViewAccTextBoxCell)
        If cDataGridViewCell IsNot Nothing Then
            cDataGridViewCell.DecimalLength = Me._decimalLength
            cDataGridViewCell.AllowNegative = Me._allowNegative
        End If
        Return cDataGridViewCell
    End Function

    Public Overrides Sub InitializeEditingControl(ByVal nRowIndex As Integer, _
        ByVal nInitialFormattedValue As Object, ByVal nDataGridViewCellStyle As DataGridViewCellStyle)

        MyBase.InitializeEditingControl(nRowIndex, nInitialFormattedValue, nDataGridViewCellStyle)

        Dim cEditBox As AccTextBox = TryCast(Me.DataGridView.EditingControl, AccTextBox)

        If cEditBox IsNot Nothing Then
            cEditBox.SupressFormating = True
            cEditBox.BorderStyle = BorderStyle.None
            cEditBox.DecimalLength = Me.DecimalLength
            cEditBox.NegativeValue = Me.AllowNegative

            Try
                cEditBox.DecimalValue = CType(nInitialFormattedValue, Double)
            Catch ex As Exception
                cEditBox.DecimalValue = 0
            End Try

        End If
    End Sub

    <EditorBrowsable(EditorBrowsableState.Advanced)> _
    Public Overrides Sub DetachEditingControl()
        Dim cDataGridView As DataGridView = Me.DataGridView
        If cDataGridView Is Nothing OrElse cDataGridView.EditingControl Is Nothing Then
            Throw New InvalidOperationException("Cell is detached or its grid has no editing control.")
        End If

        Dim EditBox As AccTextBox = TryCast(cDataGridView.EditingControl, AccTextBox)
        If EditBox IsNot Nothing Then
            ' avoid interferences between the editing sessions
            EditBox.ClearUndo()
        End If

        MyBase.DetachEditingControl()
    End Sub

    ''' <summary>
    ''' Consider the decimal in the formatted representation of the cell value.
    ''' </summary>
    Protected Overrides Function GetFormattedValue(ByVal nValue As Object, _
        ByVal nRowIndex As Integer, ByRef nCellStyle As DataGridViewCellStyle, _
        ByVal nValueTypeConverter As TypeConverter, ByVal nFormattedValueTypeConverter As TypeConverter, _
        ByVal nContext As DataGridViewDataErrorContexts) As Object

        Dim baseFormattedValue As Object = MyBase.GetFormattedValue(nValue, nRowIndex, _
            nCellStyle, nValueTypeConverter, nFormattedValueTypeConverter, nContext)

        Dim formattedText As String = TryCast(baseFormattedValue, String)

        If nValue Is Nothing OrElse String.IsNullOrEmpty(formattedText) Then
            Return baseFormattedValue
        End If

        Dim unformattedDouble As Double = System.Convert.ToDouble(nValue)
        ' 123.1 to "123.1"
        Dim formattedDouble As Double = System.Convert.ToDouble(formattedText)
        ' 123.1 to "123.12" if DecimalLength is 2

        If unformattedDouble = formattedDouble Then

            Dim cValueFormatStr As String = AccTextBox.GetValueFormatString(Me._decimalLength)

            Return formattedDouble.ToString(cValueFormatStr)

        End If

        Return formattedText

    End Function

    Public Overrides Function ToString() As String
        Return "DataGridViewAccTextBoxCell{ColIndex=" & Me.ColumnIndex.ToString & _
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

        Dim cEditingControl As AccTextBoxDataGridViewEditingControl = _
            TryCast(Me.DataGridView.EditingControl, AccTextBoxDataGridViewEditingControl)
        Return (cEditingControl IsNot Nothing AndAlso _
            nRowIndex = DirectCast(cEditingControl, IDataGridViewEditingControl).EditingControlRowIndex)
    End Function

    Friend Sub SetDecimalLength(ByVal nRowIndex As Integer, ByVal nValue As Integer)
        _decimalLength = nValue
        If OwnsEditingControl(nRowIndex) Then
            EditingAccTextBox.DecimalLength = nValue
        End If
    End Sub

    Friend Sub EnableNegative(ByVal nRowIndex As Integer, ByVal nValue As Boolean)
        _allowNegative = Value
        If OwnsEditingControl(nRowIndex) Then
            Me.EditingAccTextBox.NegativeValue = Value
        End If
    End Sub

End Class
