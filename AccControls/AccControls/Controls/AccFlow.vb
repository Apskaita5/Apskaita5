Imports System.Drawing
Public Class AccFlow

#Region "*** PRIVATE VARIABLES ***"

    ' offset in pixels between the AccUserControl and this control right borders
    Private _Offset As Integer = 6
    ' datasources for AccUserControls' MTGC comboboxes
    ' has to be between 2 and 5 columns, last column for Tag property
    Private WithEvents _MTGC1DataSource As DataTable = Nothing
    Private WithEvents _MTGC2DataSource As DataTable = Nothing
    ' specific datasource for this control; has to be 11 columns: 
    ' 0-4 to the first MTGC combobox, 5 to the textbox, 6-10 to the second MTGC combobox
    Private WithEvents _DataSource As DataTable
    ' maximum simbols to be shown in MTGC comboboxes' column
    Private _MaxSimbolsPerColumn As Integer = 255
    ' minimum width of a MTGC comboboxes' column in pixels
    Private _MinColumnWidth As Integer = 20
    ' offset of MTGC comboboxes columns
    Private _ColumnOffset As Integer = 5
    ' MTGC column used to identify the selection
    Private _MTGC1IndexColumn As AccUserContr.PropertyID
    Private _MTGC2IndexColumn As AccUserContr.PropertyID
    Private _PercMTGC2 As Integer = 30 ' percentage of all width to the second MTGC box
    Private _PercTxtBox As Integer = 30 ' ' percentage of all width to the TextBox

    ' stops handle OnIndexChanged from executing, reverses in New()
    Private FlagInProgress As Boolean = True
    ' stops resizing and redrawing
    Private FlagSuspendLayout As Boolean = False

#End Region

    Public Event ValueChanged As eventhandler

#Region "*** PUBLIC PROPERTIES ***"

    Public Property MTGC1DataSource() As DataTable
        Get
            Return _MTGC1DataSource
        End Get
        Set(ByVal value As DataTable)
            If Not value Is Nothing AndAlso (value.Columns.Count < 2 Or _
                value.Columns.Count > 5) Then Throw New Exception("Column number must be between 2 and 5.")

            FlagInProgress = True

            For i As Integer = 1 To Me.FlowLayoutPanel1.Controls.Count
                If Not value Is Nothing Then
                    CType(Me.FlowLayoutPanel1.Controls(i - 1), _
                        AccUserContr).MTGC1ColumnsCount = value.Columns.Count - 1 ' last column for Tag
                    Try
                        CType(Me.FlowLayoutPanel1.Controls(i - 1), _
                            AccUserContr).DataSourceMTGC1 = value
                        CType(Me.FlowLayoutPanel1.Controls(i - 1), _
                            AccUserContr).MTGC1ColumnWidth = GetColumnWidth(value, _
                            CType(Me.FlowLayoutPanel1.Controls(i - 1), AccUserContr).Mtgc1)
                    Catch ex As Exception
                        Throw New Exception("MTGCBox Datasource error. See the inner exception.", ex)
                        FlagInProgress = False
                        Exit Property
                    End Try
                Else
                    CType(Me.FlowLayoutPanel1.Controls(i - 1), AccUserContr).MTGC1ColumnsCount = 1
                    CType(Me.FlowLayoutPanel1.Controls(i - 1), AccUserContr).MTGC1ColumnWidth = "100"
                    CType(Me.FlowLayoutPanel1.Controls(i - 1), AccUserContr).DataSourceMTGC1 = Nothing
                End If
            Next

            _MTGC1DataSource = value
            FlagInProgress = False
        End Set
    End Property

    Public Property MTGC2DataSource() As DataTable
        Get
            Return _MTGC2DataSource
        End Get
        Set(ByVal value As DataTable)
            If Not value Is Nothing AndAlso (value.Columns.Count < 2 Or _
                value.Columns.Count > 5) Then Throw New Exception("Column number must be between 2 and 5.")

            FlagInProgress = True

            For i As Integer = 1 To Me.FlowLayoutPanel1.Controls.Count
                If Not value Is Nothing Then
                    CType(Me.FlowLayoutPanel1.Controls(i - 1), _
                        AccUserContr).MTGC2ColumnsCount = value.Columns.Count - 1
                    Try
                        CType(Me.FlowLayoutPanel1.Controls(i - 1), _
                            AccUserContr).DataSourceMTGC2 = value
                        CType(Me.FlowLayoutPanel1.Controls(i - 1), _
                            AccUserContr).MTGC2ColumnWidth = GetColumnWidth(value, _
                            CType(Me.FlowLayoutPanel1.Controls(i - 1), AccUserContr).Mtgc2)
                    Catch ex As Exception
                        Throw New Exception("MTGCBox Datasource error. See the inner exception.", ex)
                        FlagInProgress = False
                        Exit Property
                    End Try
                Else
                    CType(Me.FlowLayoutPanel1.Controls(i - 1), AccUserContr).MTGC2ColumnsCount = 1
                    CType(Me.FlowLayoutPanel1.Controls(i - 1), AccUserContr).MTGC2ColumnWidth = "100"
                    CType(Me.FlowLayoutPanel1.Controls(i - 1), AccUserContr).DataSourceMTGC2 = Nothing
                End If
            Next

            _MTGC2DataSource = value
            FlagInProgress = False
        End Set
    End Property

    Public Property DataSource() As DataTable
        Get
            Return _DataSource
        End Get
        Set(ByVal value As DataTable)
            If Not value Is Nothing AndAlso value.Columns.Count <> 11 Then _
                Throw New Exception("Datasource error. Datatable columns count must be equal to 11.")

            FlagInProgress = True
            FlagSuspendLayout = True

            ' clearing control except for the first one to keep its graphical settings
            ' see property VerticalLine
            _DataSource = Nothing
            Dim i As Integer
            For i = 2 To Me.FlowLayoutPanel1.Controls.Count
                Me.FlowLayoutPanel1.Controls.RemoveAt(1)
            Next
            _DataSource = value

            If value Is Nothing Then ' if nothing then create new
                Dim DtsNew As New DataTable
                For i = 1 To 11
                    DtsNew.Columns.Add()
                Next
                _DataSource = DtsNew
            End If

            If _DataSource.Rows.Count < 1 OrElse Not IsEmptyRow(_DataSource.Rows(_DataSource.Rows.Count - 1)) Then
                _DataSource.Rows.Add() ' adding empty row (control) for user input
            End If

            CType(Me.FlowLayoutPanel1.Controls(0), AccUserContr).DataSource = _DataSource.Rows(0)
            CType(Me.FlowLayoutPanel1.Controls(0), AccUserContr).Name = "A1" ' reseting first number

            For i = 2 To _DataSource.Rows.Count ' adding controls as required by the datasource
                AddNewControl(i)
                CType(Me.FlowLayoutPanel1.Controls(i - 1), AccUserContr).DataSource = _DataSource.Rows(i - 1)
            Next

            FlagInProgress = False
            FlagSuspendLayout = False
            RaiseEvent ValueChanged(Me, New EventArgs)
        End Set
    End Property

    Public Property VerticalLine() As Boolean
        Get
            Return CType(Me.FlowLayoutPanel1.Controls(0), AccUserContr).Mtgc1.GridLineVertical
        End Get
        Set(ByVal value As Boolean)
            CType(Me.FlowLayoutPanel1.Controls(0), AccUserContr).Mtgc1.GridLineVertical = value
            RedrawMTGC()
        End Set
    End Property

    Public Property InternalOffset() As Integer
        Get
            Return _Offset
        End Get
        Set(ByVal value As Integer)
            _Offset = value
            OnTblScroll(Me, New EventArgs)
        End Set
    End Property

    Public Property ColumnOffset() As Integer
        Get
            Return _ColumnOffset
        End Get
        Set(ByVal value As Integer)
            _ColumnOffset = value
            RedrawMTGC()
        End Set
    End Property

    Public Property MinColumnWidth() As Integer
        Get
            Return _MinColumnWidth
        End Get
        Set(ByVal value As Integer)
            _MinColumnWidth = value
            RedrawMTGC()
        End Set
    End Property

    Public Property MaxSimbolsPerColumn() As Integer
        Get
            Return _MaxSimbolsPerColumn
        End Get
        Set(ByVal value As Integer)
            _MaxSimbolsPerColumn = value
            RedrawMTGC()
        End Set
    End Property

    Public Property MTGC1IndexColumn() As AccUserContr.PropertyID
        Get
            Return _MTGC1IndexColumn
        End Get
        Set(ByVal value As AccUserContr.PropertyID)
            _MTGC1IndexColumn = value
            For i As Integer = 1 To Me.FlowLayoutPanel1.Controls.Count
                CType(Me.FlowLayoutPanel1.Controls(i - 1), AccUserContr).MTGC1ID = value
            Next
        End Set
    End Property

    Public Property MTGC2IndexColumn() As AccUserContr.PropertyID
        Get
            Return _MTGC2IndexColumn
        End Get
        Set(ByVal value As AccUserContr.PropertyID)
            _MTGC2IndexColumn = value
            For i As Integer = 1 To Me.FlowLayoutPanel1.Controls.Count
                CType(Me.FlowLayoutPanel1.Controls(i - 1), AccUserContr).MTGC2ID = value
            Next
        End Set
    End Property

    Public Property PercMTGC2() As Integer
        Get
            Return _PercMTGC2
        End Get
        Set(ByVal value As Integer)
            _PercMTGC2 = value
            For i As Integer = 1 To Me.FlowLayoutPanel1.Controls.Count
                CType(Me.FlowLayoutPanel1.Controls(i - 1), AccUserContr).PercMTGC2 = value
            Next
        End Set
    End Property

    Public Property PercTxtBox() As Integer
        Get
            Return _PercTxtBox
        End Get
        Set(ByVal value As Integer)
            _PercTxtBox = value
            For i As Integer = 1 To Me.FlowLayoutPanel1.Controls.Count
                CType(Me.FlowLayoutPanel1.Controls(i - 1), AccUserContr).PercTxtBox = value
            Next
        End Set
    End Property

    Public Property Value() As Double
        Get
            Dim d As Double = 0
            For i As Integer = 1 To Me.FlowLayoutPanel1.Controls.Count
                d = d + CType(Me.FlowLayoutPanel1.Controls(i - 1), AccUserContr).TxtBox.DecimalValue
            Next
            Return d
        End Get
        Set(ByVal value As Double)
            RaiseEvent ValueChanged(Me, New EventArgs)
        End Set
    End Property

#End Region

#Region "*** RESIZE HANDLES ***"

    Protected Overrides Sub OnSizeChanged(ByVal e As System.EventArgs)
        Dim TmpFlg As Boolean = FlagSuspendLayout
        FlagSuspendLayout = True
        MyBase.OnSizeChanged(e)
        FlagSuspendLayout = TmpFlg
        If Not FlagSuspendLayout Then
            FlagSuspendLayout = True
            For i As Integer = 1 To Me.FlowLayoutPanel1.Controls.Count
                CType(Me.FlowLayoutPanel1.Controls.Item(i - 1), AccUserContr).Width = _
                    Me.FlowLayoutPanel1.Width - SystemInformation.VerticalScrollBarWidth - _Offset
            Next
            FlagSuspendLayout = False
        End If
        Me.PerformLayout()
    End Sub

    Private Sub OnTblScroll(ByVal sender As Object, ByVal e As EventArgs) Handles FlowLayoutPanel1.ClientSizeChanged
        If Not FlagSuspendLayout Then
            For i As Integer = 1 To Me.FlowLayoutPanel1.Controls.Count
                CType(Me.FlowLayoutPanel1.Controls.Item(i - 1), AccUserContr).Width = _
                    Me.FlowLayoutPanel1.Width - SystemInformation.VerticalScrollBarWidth - _Offset
            Next
        End If
    End Sub

#End Region

    Private Sub OnIndexChanged(ByVal sender As Object)
        If FlagInProgress Then Exit Sub
        FlagInProgress = True

        Dim tre As MTGCComboBoxItem = CType(CType(sender, AccUserContr).Mtgc1.SelectedItem, MTGCComboBoxItem)
        Dim sn As Integer = CInt(CType(sender, AccUserContr).Name.Substring(1))

        If tre Is Nothing OrElse (Double.TryParse(tre.Tag, New Double) AndAlso tre.Tag = -1) Then
            ' user selected empty field, which means he wants to remove the control
            ' but he can't remove the last row (else any input won't be possible)
            If _DataSource.Rows.Count = sn Or Me.FlowLayoutPanel1.Controls.Count < 2 Then
                FlagInProgress = False
                Exit Sub
            End If

            Me.FlowLayoutPanel1.Controls.Remove(CType(sender, AccUserContr)) ' removing the control
            _DataSource.Rows.RemoveAt(sn - 1) ' and the related datarow (control's datasource)
            For i As Integer = 1 To Me.FlowLayoutPanel1.Controls.Count ' fixing numeration (minus one)
                If CInt(CType(Me.FlowLayoutPanel1.Controls(i - 1), AccUserContr).Name.Substring(1)) > sn Then _
                    CType(Me.FlowLayoutPanel1.Controls(i - 1), AccUserContr).Name = "A" & _
                    CInt(CType(Me.FlowLayoutPanel1.Controls(i - 1), AccUserContr).Name.Substring(1)) - 1
            Next
            FlagInProgress = False
            RaiseEvent ValueChanged(Me, New EventArgs)
            Exit Sub
        End If

        ' if user changed the last row control, adding new control, else finishing
        If sn < _DataSource.Rows.Count Then
            FlagInProgress = False
            Exit Sub
        End If

        _DataSource.Rows.Add()
        AddNewControl()
        CType(Me.FlowLayoutPanel1.Controls(Me.FlowLayoutPanel1.Controls.Count - 1), _
            AccUserContr).DataSource = _DataSource.Rows(_DataSource.Rows.Count - 1)

        ' scroling the new control into user view
        If Me.FlowLayoutPanel1.VerticalScroll.Visible Then _
            Me.FlowLayoutPanel1.ScrollControlIntoView( _
            Me.FlowLayoutPanel1.Controls.Item( _
            Me.FlowLayoutPanel1.Controls.Count - 1))
        FlagInProgress = False

    End Sub

    Private Sub OnIndexChanged1(ByVal sender As Object) Handles A1.OnIndexChenged
        OnIndexChanged(sender)
    End Sub

    Private Sub OnDatasourceChanged(ByVal sender As Object, _
        ByVal e As DataColumnChangeEventArgs) Handles _DataSource.ColumnChanged
        If e.Column.Ordinal = 5 And Not FlagInProgress Then _
            RaiseEvent ValueChanged(Me, New EventArgs)
    End Sub

#Region "*** PRIVATE SUBS ***"

    Private Function GetColumnWidth(ByVal Dt As DataTable, ByVal TargetBox As MTGCComboBox) As String
        If Dt Is Nothing OrElse Dt.Columns.Count < 2 Then Return "100"
        Dim MaxLn1 As String = ""
        Dim MaxLn2 As String = ""
        Dim MaxLn3 As String = ""
        Dim MaxLn4 As String = ""
        For i As Integer = 1 To Dt.Rows.Count ' getting the longest strings from datasource
            If Dt.Rows(i - 1).Item(0).ToString.Length > MaxLn1.Length Then _
                MaxLn1 = Dt.Rows(i - 1).Item(0).ToString
            If Dt.Columns.Count > 2 Then
                If Dt.Rows(i - 1).Item(1).ToString.Length > MaxLn2.Length Then _
                    MaxLn2 = Dt.Rows(i - 1).Item(1).ToString
            End If
            If Dt.Columns.Count > 3 Then
                If Dt.Rows(i - 1).Item(2).ToString.Length > MaxLn3.Length Then _
                    MaxLn3 = Dt.Rows(i - 1).Item(2).ToString
            End If
            If Dt.Columns.Count > 4 Then
                If Dt.Rows(i - 1).Item(3).ToString.Length > MaxLn4.Length Then _
                    MaxLn4 = Dt.Rows(i - 1).Item(3).ToString
            End If
        Next

        ' checking if the longest strings do not exceed maximum allowed length
        If MaxLn1.Length > MaxSimbolsPerColumn Then MaxLn1 = MaxLn1.Substring(0, MaxSimbolsPerColumn)
        If MaxLn2.Length > MaxSimbolsPerColumn Then MaxLn2 = MaxLn2.Substring(0, MaxSimbolsPerColumn)
        If MaxLn3.Length > MaxSimbolsPerColumn Then MaxLn3 = MaxLn3.Substring(0, MaxSimbolsPerColumn)
        If MaxLn4.Length > MaxSimbolsPerColumn Then MaxLn4 = MaxLn4.Substring(0, MaxSimbolsPerColumn)

        Dim W1, W2, W3, W4 As Integer ' measuring strings in pixels
        Dim TmpLbl As New Label
        Dim e As Graphics = TmpLbl.CreateGraphics
        W1 = e.MeasureString(MaxLn1, TargetBox.Font).Width
        W2 = e.MeasureString(MaxLn2, TargetBox.Font).Width
        W3 = e.MeasureString(MaxLn3, TargetBox.Font).Width
        W4 = e.MeasureString(MaxLn4, TargetBox.Font).Width
        e.Dispose()
        TmpLbl.Dispose()

        ' adding offset and checking if they are not less then minimal allowed
        W1 = W1 + ColumnOffset
        W2 = W2 + ColumnOffset
        W3 = W3 + ColumnOffset
        W4 = W4 + ColumnOffset
        If W1 < MinColumnWidth Then W1 = MinColumnWidth
        If W2 < MinColumnWidth Then W2 = MinColumnWidth
        If W3 < MinColumnWidth Then W3 = MinColumnWidth
        If W4 < MinColumnWidth Then W4 = MinColumnWidth

        If Dt.Columns.Count = 2 Then ' returning in the format required by mtgc combobox
            Return W1.ToString
        ElseIf Dt.Columns.Count = 3 Then
            Return W1.ToString & ";" & W2.ToString
        ElseIf Dt.Columns.Count = 4 Then
            Return W1.ToString & ";" & W2.ToString & ";" & W3.ToString
        Else
            Return W1.ToString & ";" & W2.ToString & ";" & W3.ToString & ";" & W4.ToString
        End If
    End Function

    Private Sub RedrawMTGC()
        Dim i As Integer
        For i = 1 To Me.FlowLayoutPanel1.Controls.Count
            CType(Me.FlowLayoutPanel1.Controls(i - 1), AccUserContr).Mtgc1.GridLineVertical = _
                CType(Me.FlowLayoutPanel1.Controls(0), AccUserContr).Mtgc1.GridLineVertical
            CType(Me.FlowLayoutPanel1.Controls(i - 1), AccUserContr).Mtgc2.GridLineVertical = _
                CType(Me.FlowLayoutPanel1.Controls(0), AccUserContr).Mtgc1.GridLineVertical
            If Not _MTGC1DataSource Is Nothing AndAlso _MTGC1DataSource.Columns.Count > 1 Then
                CType(Me.FlowLayoutPanel1.Controls(i - 1), AccUserContr).MTGC1ColumnsCount = _
                    _MTGC1DataSource.Columns.Count - 1
                CType(Me.FlowLayoutPanel1.Controls(i - 1), AccUserContr).MTGC1ColumnWidth = _
                    GetColumnWidth(_MTGC1DataSource, CType(Me.FlowLayoutPanel1.Controls(i - 1), _
                    AccUserContr).Mtgc1)
            Else
                CType(Me.FlowLayoutPanel1.Controls(i - 1), AccUserContr).MTGC1ColumnsCount = 1
                CType(Me.FlowLayoutPanel1.Controls(i - 1), AccUserContr).MTGC1ColumnWidth = "100"
            End If
            If Not _MTGC2DataSource Is Nothing AndAlso _MTGC2DataSource.Columns.Count > 1 Then
                CType(Me.FlowLayoutPanel1.Controls(i - 1), AccUserContr).MTGC2ColumnsCount = _
                    _MTGC2DataSource.Columns.Count - 1
                CType(Me.FlowLayoutPanel1.Controls(i - 1), AccUserContr).MTGC2ColumnWidth = _
                    GetColumnWidth(_MTGC2DataSource, CType(Me.FlowLayoutPanel1.Controls(i - 1), _
                    AccUserContr).Mtgc2)
            Else
                CType(Me.FlowLayoutPanel1.Controls(i - 1), AccUserContr).MTGC2ColumnsCount = 1
                CType(Me.FlowLayoutPanel1.Controls(i - 1), AccUserContr).MTGC2ColumnWidth = "100"
            End If
        Next
    End Sub

    Private Sub AddNewControl(Optional ByVal NewCntrNum As Integer = -1)
        Dim NC As New AccUserContr
        If Not _MTGC1DataSource Is Nothing Then
            NC.MTGC1ColumnsCount = _MTGC1DataSource.Columns.Count - 1 ' the last one is for Tag
            NC.DataSourceMTGC1 = _MTGC1DataSource
            NC.MTGC1ColumnWidth = CType(Me.FlowLayoutPanel1.Controls(0), AccUserContr).MTGC1ColumnWidth
        Else
            NC.MTGC1ColumnsCount = 1
            NC.MTGC1ColumnWidth = "100"
        End If
        If Not _MTGC2DataSource Is Nothing Then
            NC.MTGC2ColumnsCount = _MTGC2DataSource.Columns.Count - 1
            NC.DataSourceMTGC2 = _MTGC2DataSource
            NC.MTGC2ColumnWidth = CType(Me.FlowLayoutPanel1.Controls(0), AccUserContr).MTGC2ColumnWidth
        Else
            NC.MTGC2ColumnsCount = 1
            NC.MTGC2ColumnWidth = "100"
        End If

        If NewCntrNum > 0 Then
            NC.Name = "A" & NewCntrNum
        Else
            NC.Name = "A" & _DataSource.Rows.Count
        End If
        NC.Mtgc1.GridLineVertical = CType(Me.FlowLayoutPanel1.Controls(0), AccUserContr).Mtgc1.GridLineVertical
        NC.Mtgc2.GridLineVertical = CType(Me.FlowLayoutPanel1.Controls(0), AccUserContr).Mtgc1.GridLineVertical
        NC.MTGC1ID = _MTGC1IndexColumn
        NC.MTGC2ID = _MTGC2IndexColumn
        NC.PercMTGC2 = CType(Me.FlowLayoutPanel1.Controls(0), AccUserContr).PercMTGC2
        NC.PercTxtBox = CType(Me.FlowLayoutPanel1.Controls(0), AccUserContr).PercTxtBox
        AddHandler NC.OnIndexChenged, AddressOf OnIndexChanged
        NC.Width = CType(Me.FlowLayoutPanel1.Controls(0), AccUserContr).Width
        Me.FlowLayoutPanel1.Controls.Add(NC)
    End Sub

#End Region

    Public Sub New()
        _DataSource = New DataTable ' generating empty source for the first control
        For i As Integer = 1 To 11
            _DataSource.Columns.Add()
        Next
        _DataSource.Rows.Add()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        A1.DataSource = _DataSource.Rows(0)
        FlagInProgress = False
        Me.FlowLayoutPanel1.WrapContents = True
        Me.FlowLayoutPanel1.FlowDirection = FlowDirection.LeftToRight
        Me.FlowLayoutPanel1.AutoScroll = True
    End Sub

    Public Shared Function IsEmptyRow(ByVal Row As DataRow) As Boolean
        If Row Is Nothing OrElse Row.Table.Columns.Count <> 11 Then
            Throw New Exception("Row is null or columns count is not equal to 11.")
            Return False
        End If
        For i As Integer = 1 To 11
            If i = 6 Then
                If Double.TryParse(Row.Item(i - 1).ToString, New Double) _
                    AndAlso CDbl(Row.Item(i - 1).ToString) > 0 Then Return False
            Else
                If Not Row.Item(i - 1) Is Nothing AndAlso Row.Item(i - 1).ToString <> "" Then Return False
            End If
        Next
        Return True
    End Function

    Public Sub ClearMTGC2Column()
        For i As Integer = 1 To Me.FlowLayoutPanel1.Controls.Count
            CType(Me.FlowLayoutPanel1.Controls(i - 1), AccUserContr).Mtgc2.SelectedIndex = -1
        Next
    End Sub

End Class
