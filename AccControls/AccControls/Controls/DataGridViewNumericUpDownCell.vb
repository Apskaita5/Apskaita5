Imports System
Imports System.Drawing
Imports System.Diagnostics
Imports System.Globalization
Imports System.Windows.Forms
Imports System.Collections.Generic
Imports System.ComponentModel

''' <summary>
''' Defines a NumericUpDown cell type for the System.Windows.Forms.DataGridView control
''' </summary>
Public Class DataGridViewNumericUpDownCell
    Inherits DataGridViewTextBoxCell

    ' Default value of the DecimalPlaces property
    Friend Const DATAGRIDVIEWNUMERICUPDOWNCELL_defaultDecimalPlaces As Integer = 0
    ' Default value of the Increment property
    Friend Const DATAGRIDVIEWNUMERICUPDOWNCELL_defaultIncrement As Decimal = Decimal.One
    ' Default value of the Maximum property
    Friend Const DATAGRIDVIEWNUMERICUPDOWNCELL_defaultMaximum As Decimal = CType(100, Decimal)
    ' Default value of the Minimum property
    Friend Const DATAGRIDVIEWNUMERICUPDOWNCELL_defaultMinimum As Decimal = CType(-100, Decimal)
    ' Default value of the ThousandsSeparator property
    Friend Const DATAGRIDVIEWNUMERICUPDOWNCELL_defaultThousandsSeparator As Boolean = False

    Private _decimalPlaces As Integer
    ' Caches the value of the DecimalPlaces property
    Private _increment As Decimal
    ' Caches the value of the Increment property
    Private _minimum As Decimal
    ' Caches the value of the Minimum property
    Private _maximum As Decimal
    ' Caches the value of the Maximum property
    Private _thousandsSeparator As Boolean

    Public Sub New()
        MyBase.New()

        _decimalPlaces = DATAGRIDVIEWNUMERICUPDOWNCELL_defaultDecimalPlaces
        _increment = DATAGRIDVIEWNUMERICUPDOWNCELL_defaultIncrement
        _minimum = DATAGRIDVIEWNUMERICUPDOWNCELL_defaultMinimum
        _maximum = DATAGRIDVIEWNUMERICUPDOWNCELL_defaultMaximum
        _thousandsSeparator = DATAGRIDVIEWNUMERICUPDOWNCELL_defaultThousandsSeparator

    End Sub

    ''' <summary>
    ''' The DecimalPlaces property replicates the one from the NumericUpDown control
    ''' </summary>
    <DefaultValue(DATAGRIDVIEWNUMERICUPDOWNCELL_defaultDecimalPlaces)> _
    Public Property DecimalPlaces() As Integer
        Get
            Return Me._decimalPlaces
        End Get
        Set(ByVal value As Integer)
            If ((value < 0) OrElse (value > 99)) Then
                Throw New ArgumentOutOfRangeException("The DecimalPlaces property cannot be smaller than 0 or larger than 99.")
            End If
            If (Me._decimalPlaces <> value) Then
                SetDecimalPlaces(Me.RowIndex, value)
                OnCommonChange()
                ' Assure that the cell or column gets repainted and autosized if needed
            End If
        End Set
    End Property

    ''' <summary>
    ''' Returns the current DataGridView EditingControl as a DataGridViewNumericUpDownEditingControl control
    ''' </summary>
    Private ReadOnly Property EditingNumericUpDown() As DataGridViewNumericUpDownEditingControl
        Get
            Return CType(Me.DataGridView.EditingControl, DataGridViewNumericUpDownEditingControl)
        End Get
    End Property

    ''' <summary>
    ''' Define the type of the cell's editing control
    ''' </summary>
    Public Overrides ReadOnly Property EditType() As Type
        Get
            Return GetType(DataGridViewNumericUpDownEditingControl)
            ' the type is DataGridViewNumericUpDownEditingControl
        End Get
    End Property

    ''' <summary>
    ''' The Increment property replicates the one from the NumericUpDown control
    ''' </summary>
    Public Property Increment() As Decimal
        Get
            Return Me._increment
        End Get
        Set(ByVal value As Decimal)
            If (value < CType(0, Decimal)) Then
                Throw New ArgumentOutOfRangeException("The Increment property cannot be smaller than 0.")
            End If
            SetIncrement(Me.RowIndex, value)
            ' No call to OnCommonChange is needed since the increment value does not affect the rendering of the cell.
        End Set
    End Property

    ''' <summary>
    ''' The Maximum property replicates the one from the NumericUpDown control
    ''' </summary>
    Public Property Maximum() As Decimal
        Get
            Return Me._maximum
        End Get
        Set(ByVal value As Decimal)
            If (Me._maximum <> value) Then
                SetMaximum(Me.RowIndex, value)
                OnCommonChange()
            End If
        End Set
    End Property

    ''' <summary>
    ''' The Minimum property replicates the one from the NumericUpDown control
    ''' </summary>
    Public Property Minimum() As Decimal
        Get
            Return Me._minimum
        End Get
        Set(ByVal value As Decimal)
            If (Me._minimum <> value) Then
                SetMinimum(Me.RowIndex, value)
                OnCommonChange()
            End If
        End Set
    End Property

    ''' <summary>
    ''' The ThousandsSeparator property replicates the one from the NumericUpDown control
    ''' </summary>
    <DefaultValue(DATAGRIDVIEWNUMERICUPDOWNCELL_defaultThousandsSeparator)> _
    Public Property ThousandsSeparator() As Boolean
        Get
            Return Me._thousandsSeparator
        End Get
        Set(ByVal value As Boolean)
            If (Me._thousandsSeparator <> value) Then
                SetThousandsSeparator(Me.RowIndex, value)
                OnCommonChange()
            End If
        End Set
    End Property

    ''' <summary>
    ''' Returns the type of the cell's Value property
    ''' </summary>
    Public Overrides ReadOnly Property ValueType() As Type
        Get
            Return GetType(System.Decimal)
        End Get
    End Property

    ''' <summary>
    ''' Clones a DataGridViewNumericUpDownCell cell, copies all the custom properties.
    ''' </summary>
    Public Overrides Function Clone() As Object
        Dim dataGridViewCell As DataGridViewNumericUpDownCell = CType(MyBase.Clone, DataGridViewNumericUpDownCell)
        If (Not (dataGridViewCell) Is Nothing) Then
            dataGridViewCell.DecimalPlaces = Me._decimalPlaces
            dataGridViewCell.Increment = Me._increment
            dataGridViewCell.Maximum = Me._maximum
            dataGridViewCell.Minimum = Me._minimum
            dataGridViewCell.ThousandsSeparator = Me._thousandsSeparator
        End If
        Return dataGridViewCell
    End Function

    ''' <summary>
    ''' Returns the provided value constrained to be within the min and max. 
    ''' </summary>
    Private Function Constrain(ByVal value As Decimal) As Decimal
        If (value < Me._minimum) Then
            value = Me._minimum
        End If
        If (value > Me._maximum) Then
            value = Me._maximum
        End If
        Return value
    End Function

    ''' <summary>
    ''' DetachEditingControl gets called by the DataGridView control when the editing session is ending
    ''' </summary>
    <EditorBrowsable(EditorBrowsableState.Advanced)> _
    Public Overrides Sub DetachEditingControl()
        Dim dataGridView As DataGridView = Me.DataGridView
        If ((dataGridView Is Nothing) OrElse (dataGridView.EditingControl Is Nothing)) Then
            Throw New InvalidOperationException("Cell is detached or its grid has no editing control.")
        End If
        Dim numericUpDown As NumericUpDown = CType(dataGridView.EditingControl, NumericUpDown)
        If (Not (numericUpDown) Is Nothing) Then
            ' Editing controls get recycled. Indeed, when a DataGridViewNumericUpDownCell cell gets edited
            ' after another DataGridViewNumericUpDownCell cell, the same editing control gets reused for 
            ' performance reasons (to avoid an unnecessary control destruction and creation). 
            ' Here the undo buffer of the TextBox inside the NumericUpDown control gets cleared to avoid
            ' interferences between the editing sessions.
            Dim textBox As TextBox = CType(numericUpDown.Controls(1), TextBox)
            If (Not (textBox) Is Nothing) Then
                textBox.ClearUndo()
            End If
        End If
        MyBase.DetachEditingControl()
    End Sub

    ''' <summary>
    ''' Adjusts the location and size of the editing control given the alignment characteristics of the cell
    ''' </summary>
    Private Function GetAdjustedEditingControlBounds(ByVal editingControlBounds As Rectangle, ByVal cellStyle As DataGridViewCellStyle) As Rectangle
        ' Add a 1 pixel padding on the left and right of the editing control
        editingControlBounds.X = (editingControlBounds.X + 1)
        editingControlBounds.Width = Math.Max(0, (editingControlBounds.Width - 2))
        ' Adjust the vertical location of the editing control:
        Dim preferredHeight As Integer = (cellStyle.Font.Height + 3)
        If (preferredHeight < editingControlBounds.Height) Then
            Select Case (cellStyle.Alignment)
                Case DataGridViewContentAlignment.MiddleLeft, DataGridViewContentAlignment.MiddleCenter, DataGridViewContentAlignment.MiddleRight
                    editingControlBounds.Y = (editingControlBounds.Y _
                                + ((editingControlBounds.Height - preferredHeight) _
                                / 2))
                Case DataGridViewContentAlignment.BottomLeft, DataGridViewContentAlignment.BottomCenter, DataGridViewContentAlignment.BottomRight
                    editingControlBounds.Y = (editingControlBounds.Y _
                                + (editingControlBounds.Height - preferredHeight))
            End Select
        End If
        Return editingControlBounds
    End Function

    ''' <summary>
    ''' Customized implementation of the GetErrorIconBounds function in order to draw the potential 
    ''' error icon next to the up/down buttons and not on top of them.
    ''' </summary>
    Protected Overrides Function GetErrorIconBounds(ByVal graphics As Graphics, ByVal cellStyle As DataGridViewCellStyle, ByVal rowIndex As Integer) As Rectangle
        Const ButtonsWidth As Integer = 16
        Dim errorIconBounds As Rectangle = MyBase.GetErrorIconBounds(graphics, cellStyle, rowIndex)
        If (Me.DataGridView.RightToLeft = RightToLeft.Yes) Then
            errorIconBounds.X = (errorIconBounds.Left + ButtonsWidth)
        Else
            errorIconBounds.X = (errorIconBounds.Left - ButtonsWidth)
        End If
        Return errorIconBounds
    End Function

    ''' <summary>
    ''' Customized implementation of the GetFormattedValue function in order to include the decimal and thousand separator
    ''' characters in the formatted representation of the cell value.
    ''' </summary>
    Protected Overrides Function GetFormattedValue(ByVal value As Object, ByVal rowIndex As Integer, ByRef cellStyle As DataGridViewCellStyle, ByVal valueTypeConverter As TypeConverter, ByVal formattedValueTypeConverter As TypeConverter, ByVal context As DataGridViewDataErrorContexts) As Object
        ' By default, the base implementation converts the Decimal 1234.5 into the string "1234.5"
        Dim formattedValue As Object = MyBase.GetFormattedValue(value, rowIndex, cellStyle, valueTypeConverter, formattedValueTypeConverter, context)
        Dim formattedNumber As String = CType(formattedValue, String)
        If Not String.IsNullOrEmpty(formattedNumber) AndAlso Not value Is Nothing Then
            Dim unformattedDecimal As Decimal = System.Convert.ToDecimal(value)
            Dim formattedDecimal As Decimal = System.Convert.ToDecimal(formattedNumber)
            If unformattedDecimal = formattedDecimal Then
                ' The base implementation of GetFormattedValue (which triggers the CellFormatting event) did nothing else than 
                ' the typical 1234.5 to "1234.5" conversion. But depending on the values of ThousandsSeparator and DecimalPlaces,
                ' this may not be the actual string displayed. The real formatted value may be "1,234.500"
                Return formattedDecimal.ToString()
                'TODO: Warning!!!, inline IF is not supported ?
            End If
        End If
        Return formattedValue
    End Function

    ''' <summary>
    ''' Custom implementation of the GetPreferredSize function. This implementation uses the preferred size of the base 
    ''' DataGridViewTextBoxCell cell and adds room for the up/down buttons.
    ''' </summary>
    Protected Overrides Function GetPreferredSize(ByVal graphics As Graphics, ByVal cellStyle As DataGridViewCellStyle, ByVal rowIndex As Integer, ByVal constraintSize As Size) As Size
        If (Me.DataGridView Is Nothing) Then
            Return New Size(-1, -1)
        End If
        Dim preferredSize As Size = MyBase.GetPreferredSize(graphics, cellStyle, rowIndex, constraintSize)
        If (constraintSize.Width = 0) Then
            Const ButtonsWidth As Integer = 16
            ' Account for the width of the up/down buttons.
            Const ButtonMargin As Integer = 8
            ' Account for some blank pixels between the text and buttons.
            preferredSize.Width = (preferredSize.Width _
                        + (ButtonsWidth + ButtonMargin))
        End If
        Return preferredSize
    End Function

    ''' <summary>
    ''' Custom implementation of the InitializeEditingControl function. This function is called by the DataGridView control 
    ''' at the beginning of an editing session. It makes sure that the properties of the NumericUpDown editing control are 
    ''' set according to the cell properties.
    ''' </summary>
    Public Overrides Sub InitializeEditingControl(ByVal rowIndex As Integer, ByVal initialFormattedValue As Object, ByVal dataGridViewCellStyle As DataGridViewCellStyle)
        MyBase.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle)
        Dim numericUpDown As NumericUpDown = CType(Me.DataGridView.EditingControl, NumericUpDown)
        If (Not (numericUpDown) Is Nothing) Then
            numericUpDown.BorderStyle = BorderStyle.None
            numericUpDown.DecimalPlaces = Me._decimalPlaces
            numericUpDown.Increment = Me._increment
            numericUpDown.Maximum = Me._maximum
            numericUpDown.Minimum = Me._minimum
            numericUpDown.ThousandsSeparator = Me._thousandsSeparator
            Dim initialFormattedValueStr As String = CType(initialFormattedValue, String)
            If (initialFormattedValueStr Is Nothing) Then
                numericUpDown.Text = String.Empty
            Else
                numericUpDown.Text = initialFormattedValueStr
            End If
        End If
    End Sub

    ''' <summary>
    ''' Called when a cell characteristic that affects its rendering and/or preferred size has changed.
    ''' This implementation only takes care of repainting the cells. The DataGridView's autosizing methods
    ''' also need to be called in cases where some grid elements autosize.
    ''' </summary>
    Private Sub OnCommonChange()
        If Not Me.DataGridView Is Nothing AndAlso Not Me.DataGridView.IsDisposed _
                    AndAlso Not Me.DataGridView.Disposing Then
            If Me.RowIndex = -1 Then
                ' Invalidate and autosize column
                Me.DataGridView.InvalidateColumn(Me.ColumnIndex)
                ' TODO: Add code to autosize the cell's column, the rows, the column headers 
                ' and the row headers depending on their autosize settings.
                ' The DataGridView control does not expose a public method that takes care of this.
            Else
                ' The DataGridView control exposes a public method called UpdateCellValue
                ' that invalidates the cell so that it gets repainted and also triggers all
                ' the necessary autosizing: the cell's column and/or row, the column headers
                ' and the row headers are autosized depending on their autosize settings.
                Me.DataGridView.UpdateCellValue(Me.ColumnIndex, Me.RowIndex)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Determines whether this cell, at the given row index, shows the grid's editing control or not.
    ''' The row index needs to be provided as a parameter because this cell may be shared among multiple rows.
    ''' </summary>
    Private Function OwnsEditingNumericUpDown(ByVal rowIndex As Integer) As Boolean
        If rowIndex = -1 OrElse Me.DataGridView Is Nothing Then Return False
        Dim numericUpDownEditingControl As DataGridViewNumericUpDownEditingControl = _
            CType(Me.DataGridView.EditingControl, DataGridViewNumericUpDownEditingControl)
        Return Not numericUpDownEditingControl Is Nothing AndAlso rowIndex = _
            CType(numericUpDownEditingControl, IDataGridViewEditingControl).EditingControlRowIndex
    End Function

    ''' <summary>
    ''' Custom implementation of the PositionEditingControl method called by the DataGridView control when it
    ''' needs to relocate and/or resize the editing control.
    ''' </summary>
    Public Overrides Sub PositionEditingControl(ByVal setLocation As Boolean, _
        ByVal setSize As Boolean, ByVal cellBounds As Rectangle, ByVal cellClip As Rectangle, _
        ByVal cellStyle As DataGridViewCellStyle, ByVal singleVerticalBorderAdded As Boolean, _
        ByVal singleHorizontalBorderAdded As Boolean, ByVal isFirstDisplayedColumn As Boolean, _
        ByVal isFirstDisplayedRow As Boolean)

        Dim editingControlBounds As Rectangle = _
            PositionEditingPanel(cellBounds, cellClip, cellStyle, singleVerticalBorderAdded, _
            singleHorizontalBorderAdded, isFirstDisplayedColumn, isFirstDisplayedRow)

        editingControlBounds = GetAdjustedEditingControlBounds(editingControlBounds, cellStyle)

        Me.DataGridView.EditingControl.Location = _
            New Point(editingControlBounds.X, editingControlBounds.Y)
        Me.DataGridView.EditingControl.Size = _
            New Size(editingControlBounds.Width, editingControlBounds.Height)

    End Sub

    ''' <summary>
    ''' Utility function that sets a new value for the DecimalPlaces property of the cell. This function is used by
    ''' the cell and column DecimalPlaces property. The column uses this method instead of the DecimalPlaces
    ''' property for performance reasons. This way the column can invalidate the entire column at once instead of 
    ''' invalidating each cell of the column individually. A row index needs to be provided as a parameter because
    ''' this cell may be shared among multiple rows.
    ''' </summary>
    Friend Sub SetDecimalPlaces(ByVal rowIndex As Integer, ByVal value As Integer)
        Me._decimalPlaces = value
        If OwnsEditingNumericUpDown(rowIndex) Then
            Me.EditingNumericUpDown.DecimalPlaces = value
        End If
    End Sub

    ''' Utility function that sets a new value for the Increment property of the cell. This function is used by
    ''' the cell and column Increment property. A row index needs to be provided as a parameter because
    ''' this cell may be shared among multiple rows.
    Friend Sub SetIncrement(ByVal rowIndex As Integer, ByVal value As Decimal)
        Me._increment = value
        If OwnsEditingNumericUpDown(rowIndex) Then
            Me.EditingNumericUpDown.Increment = value
        End If
    End Sub

    ''' Utility function that sets a new value for the Maximum property of the cell. This function is used by
    ''' the cell and column Maximum property. The column uses this method instead of the Maximum
    ''' property for performance reasons. This way the column can invalidate the entire column at once instead of 
    ''' invalidating each cell of the column individually. A row index needs to be provided as a parameter because
    ''' this cell may be shared among multiple rows.
    Friend Sub SetMaximum(ByVal rowIndex As Integer, ByVal value As Decimal)
        Me._maximum = value
        If Me._minimum > Me._maximum Then
            Me._minimum = Me._maximum
        End If
        If OwnsEditingNumericUpDown(rowIndex) Then
            Me.EditingNumericUpDown.Maximum = value
        End If
    End Sub

    ''' Utility function that sets a new value for the Minimum property of the cell. This function is used by
    ''' the cell and column Minimum property. The column uses this method instead of the Minimum
    ''' property for performance reasons. This way the column can invalidate the entire column at once instead of 
    ''' invalidating each cell of the column individually. A row index needs to be provided as a parameter because
    ''' this cell may be shared among multiple rows.
    Friend Sub SetMinimum(ByVal rowIndex As Integer, ByVal value As Decimal)
        Me._minimum = value
        If Me._minimum > Me._maximum Then
            Me._maximum = value
        End If
        If OwnsEditingNumericUpDown(rowIndex) Then
            Me.EditingNumericUpDown.Minimum = value
        End If
    End Sub

    ''' Utility function that sets a new value for the ThousandsSeparator property of the cell. This function is used by
    ''' the cell and column ThousandsSeparator property. The column uses this method instead of the ThousandsSeparator
    ''' property for performance reasons. This way the column can invalidate the entire column at once instead of 
    ''' invalidating each cell of the column individually. A row index needs to be provided as a parameter because
    ''' this cell may be shared among multiple rows.
    Friend Sub SetThousandsSeparator(ByVal rowIndex As Integer, ByVal value As Boolean)
        Me._thousandsSeparator = value
        If OwnsEditingNumericUpDown(rowIndex) Then
            Me.EditingNumericUpDown.ThousandsSeparator = value
        End If
    End Sub

    ''' <summary>
    ''' Returns a standard textual representation of the cell.
    ''' </summary>
    Public Overrides Function ToString() As String
        Return ("DataGridViewNumericUpDownCell { ColumnIndex=" _
            + (ColumnIndex.ToString(CultureInfo.CurrentCulture) + (", RowIndex=" _
            + (RowIndex.ToString(CultureInfo.CurrentCulture) + " }"))))
    End Function

    ''' <summary>
    ''' Little utility function used by both the cell and column types to translate a DataGridViewContentAlignment value into
    ''' a HorizontalAlignment value.
    ''' </summary>
    Friend Shared Function TranslateAlignment(ByVal align As DataGridViewContentAlignment) As HorizontalAlignment

        If align = DataGridViewContentAlignment.BottomLeft OrElse _
            align = DataGridViewContentAlignment.MiddleLeft OrElse _
            align = DataGridViewContentAlignment.TopLeft Then
            Return HorizontalAlignment.Left
        ElseIf align = DataGridViewContentAlignment.BottomRight OrElse _
            align = DataGridViewContentAlignment.MiddleRight OrElse _
            align = DataGridViewContentAlignment.TopRight Then
            Return HorizontalAlignment.Right
        Else
            Return HorizontalAlignment.Center
        End If

    End Function

End Class
