Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Diagnostics

''' <summary>
''' Defines the editing control for the DataGridViewNumericUpDownCell custom cell type.
''' </summary>
Public Class DataGridViewNumericUpDownEditingControl
    Inherits NumericUpDown
    Implements IDataGridViewEditingControl

    '' Needed to forward keyboard messages to the child TextBox control.
    'Private Declare Function SendMessage Lib "USER32.DLL" (ByVal hWnd As IntPtr, _
    '    ByVal msg As Integer, ByVal wParam As IntPtr, ByVal lParam As IntPtr) As IntPtr

    ' The grid that owns this editing control
    Private _dataGridView As DataGridView

    ' Stores whether the editing control's value has changed or not
    Private _valueChanged As Boolean

    ' Stores the row index in which the editing control resides
    Private _rowIndex As Integer

    ''' <summary>
    ''' Constructor of the editing control class
    ''' </summary>
    Public Sub New()
        MyBase.New()
        ' The editing control must not be part of the tabbing loop
        Me.TabStop = False
    End Sub

    ''' <summary>
    ''' Property which caches the grid that uses this editing control
    ''' </summary>
    Public Overridable Property EditingControlDataGridView() As DataGridView _
        Implements IDataGridViewEditingControl.EditingControlDataGridView
        Get
            Return _dataGridView
        End Get
        Set(ByVal value As DataGridView)
            _dataGridView = value
        End Set
    End Property

    ''' <summary>
    ''' Property which represents the current formatted value of the editing control
    ''' </summary>
    Public Overridable Property EditingControlFormattedValue() As Object _
        Implements IDataGridViewEditingControl.EditingControlFormattedValue
        Get
            Return GetEditingControlFormattedValue(DataGridViewDataErrorContexts.Formatting)
        End Get
        Set(ByVal value As Object)
            Me.Text = value.ToString
        End Set
    End Property

    ''' <summary>
    ''' Property which represents the row in which the editing control resides
    ''' </summary>
    Public Overridable Property EditingControlRowIndex() As Integer _
        Implements IDataGridViewEditingControl.EditingControlRowIndex
        Get
            Return _rowIndex
        End Get
        Set(ByVal value As Integer)
            _rowIndex = value
        End Set
    End Property

    ''' <summary>
    ''' Property which indicates whether the value of the editing control has changed or not
    ''' </summary>
    Public Overridable Property EditingControlValueChanged() As Boolean _
        Implements IDataGridViewEditingControl.EditingControlValueChanged
        Get
            Return Me._valueChanged
        End Get
        Set(ByVal value As Boolean)
            Me._valueChanged = value
        End Set
    End Property

    ''' <summary>
    ''' Property which determines which cursor must be used for the editing panel,
    ''' i.e. the parent of the editing control.
    ''' </summary>
    Public Overridable ReadOnly Property EditingPanelCursor() As Cursor _
        Implements IDataGridViewEditingControl.EditingPanelCursor
        Get
            Return Cursors.Default
        End Get
    End Property

    ''' <summary>
    ''' Property which indicates whether the editing control needs to be repositioned 
    ''' when its value changes.
    ''' </summary>
    Public Overridable ReadOnly Property RepositionEditingControlOnValueChange() As Boolean _
        Implements IDataGridViewEditingControl.RepositionEditingControlOnValueChange
        Get
            Return False
        End Get
    End Property

    ''' <summary>
    ''' Method called by the grid before the editing control is shown so it can adapt to the 
    ''' provided cell style.
    ''' </summary>
    Public Overridable Sub ApplyCellStyleToEditingControl(ByVal dataGridViewCellStyle As DataGridViewCellStyle) _
        Implements IDataGridViewEditingControl.ApplyCellStyleToEditingControl
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

    ''' <summary>
    ''' Method called by the grid on keystrokes to determine if the editing control is
    ''' interested in the key or not.
    ''' </summary>
    Public Overridable Function EditingControlWantsInputKey(ByVal keyData As Keys, ByVal dataGridViewWantsInputKey As Boolean) As Boolean _
        Implements IDataGridViewEditingControl.EditingControlWantsInputKey

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

    ''' <summary>
    ''' Returns the current value of the editing control.
    ''' </summary>
    Public Overridable Function GetEditingControlFormattedValue(ByVal context As DataGridViewDataErrorContexts) As Object _
        Implements IDataGridViewEditingControl.GetEditingControlFormattedValue
        Return Me.Value.ToString()
    End Function

    ''' <summary>
    ''' Called by the grid to give the editing control a chance to prepare itself for
    ''' the editing session.
    ''' </summary>
    Public Overridable Sub PrepareEditingControlForEdit(ByVal selectAll As Boolean) _
        Implements IDataGridViewEditingControl.PrepareEditingControlForEdit
        If selectAll Then
            Me.Select(0, Convert.ToInt32(Me.Value).ToString.Length)
        Else
            Me.Select(0, 0)
        End If
    End Sub

    ''' <summary>
    ''' Small utility function that updates the local dirty state and 
    ''' notifies the grid of the value change.
    ''' </summary>
    Private Sub NotifyDataGridViewOfValueChange()
        If Not _valueChanged Then
            _valueChanged = True
            _dataGridView.NotifyCurrentCellDirty(True)
        End If
    End Sub

    ''' <summary>
    ''' Listen to the KeyPress notification to know when the value changed, and 
    ''' notify the grid of the change.
    ''' </summary>
    Protected Overrides Sub OnKeyPress(ByVal e As KeyPressEventArgs)
        MyBase.OnKeyPress(e)
        ' The value changes when a digit, the decimal separator, the group separator or
        ' the negative sign is pressed.
        Dim notifyValueChange As Boolean = False
        If Char.IsDigit(e.KeyChar) Then
            notifyValueChange = True
        Else
            Dim numberFormatInfo As System.Globalization.NumberFormatInfo = System.Globalization.CultureInfo.CurrentCulture.NumberFormat
            Dim decimalSeparatorStr As String = numberFormatInfo.NumberDecimalSeparator
            Dim groupSeparatorStr As String = numberFormatInfo.NumberGroupSeparator
            Dim negativeSignStr As String = numberFormatInfo.NegativeSign

            If (Not String.IsNullOrEmpty(decimalSeparatorStr) _
                AndAlso (decimalSeparatorStr.Length = 1)) Then
                notifyValueChange = (decimalSeparatorStr(0) = e.KeyChar)
            End If

            If (Not notifyValueChange _
                AndAlso (Not String.IsNullOrEmpty(groupSeparatorStr) _
                AndAlso (groupSeparatorStr.Length = 1))) Then
                notifyValueChange = (groupSeparatorStr(0) = e.KeyChar)
            End If

            If (Not notifyValueChange _
                AndAlso (Not String.IsNullOrEmpty(negativeSignStr) _
                AndAlso (negativeSignStr.Length = 1))) Then
                notifyValueChange = (negativeSignStr(0) = e.KeyChar)
            End If

        End If

        If notifyValueChange Then
            ' Let the DataGridView know about the value change
            NotifyDataGridViewOfValueChange()
        End If

    End Sub

    ''' <summary>
    ''' Listen to the ValueChanged notification to forward the change to the grid.
    ''' </summary>
    Protected Overrides Sub OnValueChanged(ByVal e As EventArgs)
        MyBase.OnValueChanged(e)
        If Me.Focused Then
            ' Let the DataGridView know about the value change
            NotifyDataGridViewOfValueChange()
        End If
    End Sub

    '''' <summary>
    '''' A few keyboard messages need to be forwarded to the inner textbox of the
    '''' NumericUpDown control so that the first character pressed appears in it.
    '''' </summary>
    'Protected Overrides Function ProcessKeyEventArgs(ByRef m As Message) As Boolean
    '    Dim textBox As TextBox = CType(Me.Controls(1), TextBox)
    '    If (Not (textBox) Is Nothing) Then
    '        SendMessage(textBox.Handle, m.Msg, m.WParam, m.LParam)
    '        Return True
    '    Else
    '        Return MyBase.ProcessKeyEventArgs(m)
    '    End If
    'End Function

End Class