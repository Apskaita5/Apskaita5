
Imports System.ComponentModel
Public Class DataGridViewMTGCComboBoxColumn
    Inherits System.Windows.Forms.DataGridViewColumn

    Public Sub New()
        MyBase.CellTemplate = New DataGridViewMTGCComboBoxCell
    End Sub

    Private _ColumnNum As Integer
    <[ReadOnly](True)> <Browsable(False)> _
    Public Property ColumnNum() As Integer
        Get
            Return _ColumnNum
        End Get
        Set(ByVal value As Integer)
            _ColumnNum = value
            For Each row As DataGridViewRow In Me.DataGridView.Rows
                If CType(row.Cells(Me.Index), DataGridViewMTGCComboBoxCell). _
                    MTGCComboBoxEditingControl IsNot Nothing Then

                    CType(row.Cells(Me.Index), DataGridViewMTGCComboBoxCell). _
                        MTGCComboBoxEditingControl.ColumnNum = value

                End If
            Next
        End Set
    End Property

    Private _ColumnWidth As String = ""
    <[ReadOnly](True)> <Browsable(False)> _
    Public Property ColumnWidth() As String
        Get
            Return _ColumnWidth
        End Get
        Set(ByVal value As String)
            _ColumnWidth = value
            For Each row As DataGridViewRow In Me.DataGridView.Rows
                If CType(row.Cells(Me.Index), DataGridViewMTGCComboBoxCell). _
                    MTGCComboBoxEditingControl IsNot Nothing Then

                    CType(row.Cells(Me.Index), DataGridViewMTGCComboBoxCell). _
                        MTGCComboBoxEditingControl.ColumnWidth = value

                End If
            Next
        End Set
    End Property

    Private _BorderStyle As MTGCComboBox.TipiBordi = MTGCComboBox.TipiBordi.FlatXP
    <[ReadOnly](True)> <Browsable(False)> _
    Public Property BorderStyle() As MTGCComboBox.TipiBordi
        Get
            Return _BorderStyle
        End Get
        Set(ByVal value As MTGCComboBox.TipiBordi)
            _BorderStyle = value
            For Each row As DataGridViewRow In Me.DataGridView.Rows
                If CType(row.Cells(Me.Index), DataGridViewMTGCComboBoxCell). _
                    MTGCComboBoxEditingControl IsNot Nothing Then

                    CType(row.Cells(Me.Index), DataGridViewMTGCComboBoxCell). _
                        MTGCComboBoxEditingControl.BorderStyle = value

                End If
            Next
        End Set
    End Property

    Private _DropDownArrowBackColor As Color = Color.Empty
    <[ReadOnly](True)> <Browsable(False)> _
    Public Property DropDownArrowBackColor() As Color
        Get
            Return _DropDownArrowBackColor
        End Get
        Set(ByVal value As Color)
            _DropDownArrowBackColor = value
            For Each row As DataGridViewRow In Me.DataGridView.Rows
                If CType(row.Cells(Me.Index), DataGridViewMTGCComboBoxCell). _
                    MTGCComboBoxEditingControl IsNot Nothing Then

                    CType(row.Cells(Me.Index), DataGridViewMTGCComboBoxCell). _
                        MTGCComboBoxEditingControl.DropDownArrowBackColor = value

                End If
            Next
        End Set
    End Property

    Private _DropDownBackColor As Color = Color.Empty
    <[ReadOnly](True)> <Browsable(False)> _
    Public Property DropDownBackColor() As Color
        Get
            Return _DropDownBackColor
        End Get
        Set(ByVal value As Color)
            _DropDownBackColor = value
            For Each row As DataGridViewRow In Me.DataGridView.Rows
                If CType(row.Cells(Me.Index), DataGridViewMTGCComboBoxCell). _
                    MTGCComboBoxEditingControl IsNot Nothing Then

                    CType(row.Cells(Me.Index), DataGridViewMTGCComboBoxCell). _
                        MTGCComboBoxEditingControl.DropDownBackColor = value

                End If
            Next
        End Set
    End Property

    Private _DropDownForeColor As Color = Color.Empty
    <[ReadOnly](True)> <Browsable(False)> _
    Public Property DropDownForeColor() As Color
        Get
            Return _DropDownForeColor
        End Get
        Set(ByVal value As Color)
            _DropDownForeColor = value
            For Each row As DataGridViewRow In Me.DataGridView.Rows
                If CType(row.Cells(Me.Index), DataGridViewMTGCComboBoxCell). _
                    MTGCComboBoxEditingControl IsNot Nothing Then

                    CType(row.Cells(Me.Index), DataGridViewMTGCComboBoxCell). _
                        MTGCComboBoxEditingControl.DropDownForeColor = value

                End If
            Next
        End Set
    End Property

    Private _DropDownStyle As MTGCComboBox.CustomDropDownStyle = MTGCComboBox.CustomDropDownStyle.DropDown
    <[ReadOnly](True)> <Browsable(False)> _
    Public Property DropDownStyle() As MTGCComboBox.CustomDropDownStyle
        Get
            Return _DropDownStyle
        End Get
        Set(ByVal value As MTGCComboBox.CustomDropDownStyle)
            _DropDownStyle = value
            For Each row As DataGridViewRow In Me.DataGridView.Rows
                If CType(row.Cells(Me.Index), DataGridViewMTGCComboBoxCell). _
                    MTGCComboBoxEditingControl IsNot Nothing Then

                    CType(row.Cells(Me.Index), DataGridViewMTGCComboBoxCell). _
                        MTGCComboBoxEditingControl.DropDownStyle = value

                End If
            Next
        End Set
    End Property

    Private _DropDownWidth As Integer = 0
    <[ReadOnly](True)> <Browsable(False)> _
    Public Property DropDownWidth() As Integer
        Get
            Return _DropDownWidth
        End Get
        Set(ByVal value As Integer)
            _DropDownWidth = value
            For Each row As DataGridViewRow In Me.DataGridView.Rows
                If CType(row.Cells(Me.Index), DataGridViewMTGCComboBoxCell). _
                    MTGCComboBoxEditingControl IsNot Nothing Then

                    CType(row.Cells(Me.Index), DataGridViewMTGCComboBoxCell). _
                        MTGCComboBoxEditingControl.DropDownWidth = value

                End If
            Next
        End Set
    End Property

    Private _FlatStyle As FlatStyle = Windows.Forms.FlatStyle.System
    <[ReadOnly](True)> <Browsable(False)> _
    Public Property FlatStyle() As FlatStyle
        Get
            Return _FlatStyle
        End Get
        Set(ByVal value As FlatStyle)
            _FlatStyle = value
            For Each row As DataGridViewRow In Me.DataGridView.Rows
                If CType(row.Cells(Me.Index), DataGridViewMTGCComboBoxCell). _
                    MTGCComboBoxEditingControl IsNot Nothing Then

                    CType(row.Cells(Me.Index), DataGridViewMTGCComboBoxCell). _
                        MTGCComboBoxEditingControl.FlatStyle = value

                End If
            Next
        End Set
    End Property

    Private _GridLineColor As Color = Color.Empty
    <[ReadOnly](True)> <Browsable(False)> _
    Public Property GridLineColor() As Color
        Get
            Return _GridLineColor
        End Get
        Set(ByVal value As Color)
            _GridLineColor = value
            For Each row As DataGridViewRow In Me.DataGridView.Rows
                If CType(row.Cells(Me.Index), DataGridViewMTGCComboBoxCell). _
                    MTGCComboBoxEditingControl IsNot Nothing Then

                    CType(row.Cells(Me.Index), DataGridViewMTGCComboBoxCell). _
                        MTGCComboBoxEditingControl.GridLineColor = value

                End If
            Next
        End Set
    End Property

    Private _GridLineHorizontal As Boolean = False
    <[ReadOnly](True)> <Browsable(False)> _
    Public Property GridLineHorizontal() As Boolean
        Get
            Return _GridLineHorizontal
        End Get
        Set(ByVal value As Boolean)
            _GridLineHorizontal = value
            For Each row As DataGridViewRow In Me.DataGridView.Rows
                If CType(row.Cells(Me.Index), DataGridViewMTGCComboBoxCell). _
                    MTGCComboBoxEditingControl IsNot Nothing Then

                    CType(row.Cells(Me.Index), DataGridViewMTGCComboBoxCell). _
                        MTGCComboBoxEditingControl.GridLineHorizontal = value

                End If
            Next
        End Set
    End Property

    Private _GridLineVertical As Boolean = True
    <[ReadOnly](True)> <Browsable(False)> _
    Public Property GridLineVertical() As Boolean
        Get
            Return _GridLineVertical
        End Get
        Set(ByVal value As Boolean)
            _GridLineVertical = value
            For Each row As DataGridViewRow In Me.DataGridView.Rows
                If CType(row.Cells(Me.Index), DataGridViewMTGCComboBoxCell). _
                    MTGCComboBoxEditingControl IsNot Nothing Then

                    CType(row.Cells(Me.Index), DataGridViewMTGCComboBoxCell). _
                        MTGCComboBoxEditingControl.GridLineVertical = value

                End If
            Next
        End Set
    End Property

    Private _LoadingType As MTGCComboBox.CaricamentoCombo = MTGCComboBox.CaricamentoCombo.CustomObject
    ''' <summary>
    ''' Gets or sets a type of loading. 
    ''' If set to CustomObject, provide SourcePropertiesString, ValueForNothing, 
    ''' SourcePropertiesString and SourceObject properties.
    ''' If set to Datatable, provide SourceDataString and SourceDataTable properties.
    ''' If set to ComboboxItem, provide Items property.
    ''' </summary>
    <[ReadOnly](True)> <Browsable(False)> _
    Public Property LoadingType() As MTGCComboBox.CaricamentoCombo
        Get
            Return _LoadingType
        End Get
        Set(ByVal value As MTGCComboBox.CaricamentoCombo)
            _LoadingType = value
            For Each row As DataGridViewRow In Me.DataGridView.Rows
                If CType(row.Cells(Me.Index), DataGridViewMTGCComboBoxCell). _
                    MTGCComboBoxEditingControl IsNot Nothing Then

                    CType(row.Cells(Me.Index), DataGridViewMTGCComboBoxCell). _
                        MTGCComboBoxEditingControl.LoadingType = value

                End If
            Next
        End Set
    End Property

    Private _MaxDropDownItems As Integer = 8
    <[ReadOnly](True)> <Browsable(False)> _
    Public Property MaxDropDownItems() As Integer
        Get
            Return _MaxDropDownItems
        End Get
        Set(ByVal value As Integer)
            _MaxDropDownItems = value
            For Each row As DataGridViewRow In Me.DataGridView.Rows
                If CType(row.Cells(Me.Index), DataGridViewMTGCComboBoxCell). _
                    MTGCComboBoxEditingControl IsNot Nothing Then

                    CType(row.Cells(Me.Index), DataGridViewMTGCComboBoxCell). _
                        MTGCComboBoxEditingControl.MaxDropDownItems = value

                End If
            Next
        End Set
    End Property

    Private _SourceDataString As String() = Nothing
    ''' <summary>
    ''' Gets or sets a list of datatable column names to be displayed, 
    ''' If LoadingType is set to Datatable.
    ''' </summary>
    <[ReadOnly](True)> <Browsable(False)> _
    Public Property SourceDataString() As String()
        Get
            Return _SourceDataString
        End Get
        Set(ByVal value As String())
            _SourceDataString = value
            For Each row As DataGridViewRow In Me.DataGridView.Rows
                If CType(row.Cells(Me.Index), DataGridViewMTGCComboBoxCell). _
                    MTGCComboBoxEditingControl IsNot Nothing Then

                    CType(row.Cells(Me.Index), DataGridViewMTGCComboBoxCell). _
                        MTGCComboBoxEditingControl.SourceDataString = value

                End If
            Next
        End Set
    End Property

    Private _SourceDataTable As DataTable = Nothing
    <[ReadOnly](True)> <Browsable(False)> _
    Public Property SourceDataTable() As DataTable
        Get
            Return _SourceDataTable
        End Get
        Set(ByVal value As DataTable)
            _SourceDataTable = value
            For Each row As DataGridViewRow In Me.DataGridView.Rows
                If CType(row.Cells(Me.Index), DataGridViewMTGCComboBoxCell). _
                    MTGCComboBoxEditingControl IsNot Nothing Then

                    CType(row.Cells(Me.Index), DataGridViewMTGCComboBoxCell). _
                        MTGCComboBoxEditingControl.SourceDataTable = value

                End If
            Next
        End Set
    End Property

    Private _SourceObject As Object = Nothing
    <[ReadOnly](True)> <Browsable(False)> _
    Public Property SourceObject() As Object
        Get
            Return _SourceObject
        End Get
        Set(ByVal value As Object)
            _SourceObject = value
            For Each row As DataGridViewRow In Me.DataGridView.Rows
                If CType(row.Cells(Me.Index), DataGridViewMTGCComboBoxCell). _
                    MTGCComboBoxEditingControl IsNot Nothing Then

                    CType(row.Cells(Me.Index), DataGridViewMTGCComboBoxCell). _
                        MTGCComboBoxEditingControl.SourceObject = Nothing
                    CType(row.Cells(Me.Index), DataGridViewMTGCComboBoxCell). _
                        MTGCComboBoxEditingControl.SourceObject = value

                End If
            Next
        End Set
    End Property

    Private _SourceObjectAddEmptyItem As Boolean = False
    <[ReadOnly](True)> <Browsable(False)> _
    Public Property SourceObjectAddEmptyItem() As Boolean
        Get
            Return _SourceObjectAddEmptyItem
        End Get
        Set(ByVal value As Boolean)
            _SourceObjectAddEmptyItem = value
            For Each row As DataGridViewRow In Me.DataGridView.Rows
                If CType(row.Cells(Me.Index), DataGridViewMTGCComboBoxCell). _
                    MTGCComboBoxEditingControl IsNot Nothing Then

                    CType(row.Cells(Me.Index), DataGridViewMTGCComboBoxCell). _
                        MTGCComboBoxEditingControl.SourceObjectAddEmptyItem = value

                End If
            Next
        End Set
    End Property

    Private _SourcePropertiesString As String = ""
    ''' <summary>
    ''' Gets or sets a coma separated list of item properties to be displayed, 
    ''' if LoadingType is set to CustomObject. E.g. "ID, Name"
    ''' </summary>
    <[ReadOnly](True)> <Browsable(False)> _
    Public Property SourcePropertiesString() As String
        Get
            Return _SourcePropertiesString
        End Get
        Set(ByVal value As String)
            _SourcePropertiesString = value
            For Each row As DataGridViewRow In Me.DataGridView.Rows
                If CType(row.Cells(Me.Index), DataGridViewMTGCComboBoxCell). _
                    MTGCComboBoxEditingControl IsNot Nothing Then

                    CType(row.Cells(Me.Index), DataGridViewMTGCComboBoxCell). _
                        MTGCComboBoxEditingControl.SourcePropertiesString = value

                End If
            Next
        End Set
    End Property

    Private _ValueForNothing As String = ""
    <[ReadOnly](True)> <Browsable(False)> _
    Public Property ValueForNothing() As String
        Get
            Return _ValueForNothing
        End Get
        Set(ByVal value As String)
            _ValueForNothing = value
            For Each row As DataGridViewRow In Me.DataGridView.Rows
                If CType(row.Cells(Me.Index), DataGridViewMTGCComboBoxCell). _
                    MTGCComboBoxEditingControl IsNot Nothing Then

                    CType(row.Cells(Me.Index), DataGridViewMTGCComboBoxCell). _
                        MTGCComboBoxEditingControl.ValueForNothing = value

                End If
            Next
        End Set
    End Property

    Private _Items As MTGCComboBoxItem()
    <[ReadOnly](True)> <Browsable(False)> _
    Public Property Items() As MTGCComboBoxItem()
        Get
            Return _Items
        End Get
        Set(ByVal value As MTGCComboBoxItem())
            _Items = value
            For Each row As DataGridViewRow In Me.DataGridView.Rows
                If CType(row.Cells(Me.Index), DataGridViewMTGCComboBoxCell). _
                    MTGCComboBoxEditingControl IsNot Nothing Then

                    CType(row.Cells(Me.Index), DataGridViewMTGCComboBoxCell). _
                        MTGCComboBoxEditingControl.Items.Clear()
                    CType(row.Cells(Me.Index), DataGridViewMTGCComboBoxCell). _
                        MTGCComboBoxEditingControl.Items.AddRange(value)
                    CType(row.Cells(Me.Index), DataGridViewMTGCComboBoxCell). _
                        MTGCComboBoxEditingControl.SelectedValue = _
                        CType(row.Cells(Me.Index), DataGridViewMTGCComboBoxCell). _
                        MTGCComboBoxEditingControl.SelectedText
                    CType(row.Cells(Me.Index), DataGridViewMTGCComboBoxCell). _
                        MTGCComboBoxEditingControl.Invalidate()

                End If
            Next
        End Set
    End Property


End Class
