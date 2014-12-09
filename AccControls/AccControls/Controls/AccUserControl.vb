Public Class AccUserContr

#Region "*** ENUMS AND PRIVATE FIELDS ***"

    Public Enum PropertyID
        Col1
        Col2
        Col3
        Col4
        Tag
    End Enum

    Protected _PercMTGC2 As Integer = 30 ' percentage of all width to the second MTGC box
    Protected _PercTxtBox As Integer = 30 ' ' percentage of all width to the AccBox
    Protected _MTGC1ColumnsCount As Integer = 2
    Protected _MTGC1ColumnWidth As String = "100;50"
    Protected _MTGC2ColumnsCount As Integer = 2
    Protected _MTGC2ColumnWidth As String = "100;50"
    Protected WithEvents _DataSourceMTGC1 As DataTable = Nothing ' datasource for the basic MTGC box
    Protected WithEvents _DataSourceMTGC2 As DataTable = Nothing ' datasource for the second MTGC box
    Protected _MTGC1ID As PropertyID = PropertyID.Col1 ' MTGC box property used for item identification
    Protected _MTGC2ID As PropertyID = PropertyID.Col1
    Protected WithEvents _Datasource As Object = Nothing
    Public Event OnIndexChenged(ByVal sender As Object)
    Private FlagInternalChange As Boolean = False

#End Region

#Region "*** PROPERTIES ***"

    Public Property PercMTGC2() As Integer
        Get
            Return _PercMTGC2
        End Get
        Set(ByVal value As Integer)
            _PercMTGC2 = value
            SetControlsPosition()
        End Set
    End Property

    Public Property PercTxtBox() As Integer
        Get
            Return _PercTxtBox
        End Get
        Set(ByVal value As Integer)
            _PercTxtBox = value
            SetControlsPosition()
        End Set
    End Property

    Public Property DataSourceMTGC1() As DataTable
        Get
            Return _DataSourceMTGC1
        End Get
        Set(ByVal value As DataTable)

            If value Is Nothing Then
                _DataSourceMTGC1 = value
                If Not Mtgc1 Is Nothing Then Mtgc1.Items.Clear()
                Exit Property
            End If

            If value.Columns.Count <> Mtgc1.ColumnNum + 1 Then
                Throw New Exception("Control data error. Datasource columns number is " _
                    & "not equal to the control columns number plus one (tag column).")
                Exit Property
            End If

            CheckDataSource(value)
            _DataSourceMTGC1 = value

            If Not _DataSourceMTGC1.Rows.Count > 0 Then
                Mtgc1.Items.Clear()
                Exit Property
            End If

            Dim BackedUpValue As String = "" ' backing up selected value
            If _MTGC1ID = PropertyID.Col1 Then
                BackedUpValue = GetSelectedItem(Mtgc1, PropertyID.Col1)
            ElseIf _MTGC1ID = PropertyID.Col2 Then
                BackedUpValue = GetSelectedItem(Mtgc1, PropertyID.Col2)
            ElseIf _MTGC1ID = PropertyID.Col3 Then
                BackedUpValue = GetSelectedItem(Mtgc1, PropertyID.Col3)
            ElseIf _MTGC1ID = PropertyID.Col4 Then
                BackedUpValue = GetSelectedItem(Mtgc1, PropertyID.Col4)
            Else ' Tag
                BackedUpValue = GetSelectedItem(Mtgc1, PropertyID.Tag)
            End If

            Dim Amount As Integer = _DataSourceMTGC1.Rows.Count ' array of items with one empty item at the begining
            Dim ItemsList(Amount) As MTGCComboBoxItem
            If _Mtgc1.ColumnNum = 1 Then
                ItemsList(0) = New MTGCComboBoxItem("")
            ElseIf _Mtgc1.ColumnNum = 2 Then
                ItemsList(0) = New MTGCComboBoxItem("", "")
            ElseIf _Mtgc1.ColumnNum = 3 Then
                ItemsList(0) = New MTGCComboBoxItem("", "", "")
            Else
                ItemsList(0) = New MTGCComboBoxItem("", "", "", "")
            End If
            ItemsList(0).Tag = -1

            For i As Integer = 1 To _DataSourceMTGC1.Rows.Count ' filling the array of items
                If Mtgc1.ColumnNum = 1 Then
                    ItemsList(i) = New MTGCComboBoxItem(_DataSourceMTGC1.Rows(i - 1).Item(0).ToString)
                ElseIf Mtgc1.ColumnNum = 2 Then
                    ItemsList(i) = New MTGCComboBoxItem(_DataSourceMTGC1.Rows(i - 1).Item(0).ToString, _
                        _DataSourceMTGC1.Rows(i - 1).Item(1).ToString)
                ElseIf Mtgc1.ColumnNum = 3 Then
                    ItemsList(i) = New MTGCComboBoxItem(_DataSourceMTGC1.Rows(i - 1).Item(0).ToString, _
                        _DataSourceMTGC1.Rows(i - 1).Item(1).ToString, _
                        _DataSourceMTGC1.Rows(i - 1).Item(2).ToString)
                Else
                    ItemsList(i) = New MTGCComboBoxItem(_DataSourceMTGC1.Rows(i - 1).Item(0).ToString, _
                        _DataSourceMTGC1.Rows(i - 1).Item(1).ToString, _
                        _DataSourceMTGC1.Rows(i - 1).Item(2).ToString, _
                        _DataSourceMTGC1.Rows(i - 1).Item(3).ToString)
                End If
                ItemsList(i).Tag = _DataSourceMTGC1.Rows(i - 1).Item _
                    (_DataSourceMTGC1.Columns.Count - 1).ToString
            Next

            FlagInternalChange = True
            Mtgc1.SelectedIndex = -1 ' loading items array to the control
            Mtgc1.Items.Clear()
            Mtgc1.LoadingType = MTGCComboBox.CaricamentoCombo.ComboBoxItem 'loadinam duomenis
            Mtgc1.Items.AddRange(ItemsList)
            FlagInternalChange = False

            If BackedUpValue = "" Then Exit Property ' restoring backuped value if needed and possible
            If _MTGC1ID = PropertyID.Col1 Then
                SelectItem(Mtgc1, PropertyID.Col1, BackedUpValue)
            ElseIf _MTGC1ID = PropertyID.Col2 Then
                SelectItem(Mtgc1, PropertyID.Col2, BackedUpValue)
            ElseIf _MTGC1ID = PropertyID.Col3 Then
                SelectItem(Mtgc1, PropertyID.Col3, BackedUpValue)
            ElseIf _MTGC1ID = PropertyID.Col4 Then
                SelectItem(Mtgc1, PropertyID.Col4, BackedUpValue)
            Else ' Tag
                SelectItem(Mtgc1, PropertyID.Tag, BackedUpValue)
            End If
            OnMTGC1IndexChanged(Me, New eventargs)
        End Set
    End Property

    Public Property DataSourceMTGC2() As DataTable
        Get
            Return _DataSourceMTGC2
        End Get
        Set(ByVal value As DataTable)

            If value Is Nothing Then
                _DataSourceMTGC2 = value
                If Not Mtgc2 Is Nothing Then Mtgc2.Items.Clear()
                Exit Property
            End If

            If value.Columns.Count <> Mtgc2.ColumnNum + 1 Then
                Throw New Exception("Control data error. Datasource columns number is " _
                    & "not equal to the control columns number plus one (tag column).")
                Exit Property
            End If

            CheckDataSource(value)
            _DataSourceMTGC2 = value

            If Not _DataSourceMTGC2.Rows.Count > 0 Then
                Mtgc2.Items.Clear()
                Exit Property
            End If

            Dim BackedUpValue As String = "" ' backing up selected value
            If _MTGC2ID = PropertyID.Col1 Then
                BackedUpValue = GetSelectedItem(Mtgc2, PropertyID.Col1)
            ElseIf _MTGC2ID = PropertyID.Col2 Then
                BackedUpValue = GetSelectedItem(Mtgc2, PropertyID.Col2)
            ElseIf _MTGC2ID = PropertyID.Col3 Then
                BackedUpValue = GetSelectedItem(Mtgc2, PropertyID.Col3)
            ElseIf _MTGC2ID = PropertyID.Col4 Then
                BackedUpValue = GetSelectedItem(Mtgc2, PropertyID.Col4)
            Else ' Tag
                BackedUpValue = GetSelectedItem(Mtgc2, PropertyID.Tag)
            End If

            Dim Amount As Integer = _DataSourceMTGC2.Rows.Count ' array of items with one empty item at the begining
            Dim ItemsList(Amount) As MTGCComboBoxItem
            If Mtgc2.ColumnNum = 1 Then
                ItemsList(0) = New MTGCComboBoxItem("")
            ElseIf Mtgc2.ColumnNum = 2 Then
                ItemsList(0) = New MTGCComboBoxItem("", "")
            ElseIf Mtgc2.ColumnNum = 3 Then
                ItemsList(0) = New MTGCComboBoxItem("", "", "")
            Else
                ItemsList(0) = New MTGCComboBoxItem("", "", "", "")
            End If
            ItemsList(0).Tag = -1

            For i As Integer = 1 To _DataSourceMTGC2.Rows.Count ' filling the array of items
                If Mtgc2.ColumnNum = 1 Then
                    ItemsList(i) = New MTGCComboBoxItem(_DataSourceMTGC2.Rows(i - 1).Item(0).ToString)
                ElseIf Mtgc2.ColumnNum = 2 Then
                    ItemsList(i) = New MTGCComboBoxItem(_DataSourceMTGC2.Rows(i - 1).Item(0).ToString, _
                        _DataSourceMTGC2.Rows(i - 1).Item(1).ToString)
                ElseIf Mtgc2.ColumnNum = 3 Then
                    ItemsList(i) = New MTGCComboBoxItem(_DataSourceMTGC2.Rows(i - 1).Item(0).ToString, _
                        _DataSourceMTGC2.Rows(i - 1).Item(1).ToString, _
                        _DataSourceMTGC2.Rows(i - 1).Item(2).ToString)
                Else
                    ItemsList(i) = New MTGCComboBoxItem(_DataSourceMTGC2.Rows(i - 1).Item(0).ToString, _
                        _DataSourceMTGC2.Rows(i - 1).Item(1).ToString, _
                        _DataSourceMTGC2.Rows(i - 1).Item(2).ToString, _
                        _DataSourceMTGC2.Rows(i - 1).Item(3).ToString)
                End If
                ItemsList(i).Tag = _DataSourceMTGC2.Rows(i - 1).Item _
                    (_DataSourceMTGC2.Columns.Count - 1).ToString
            Next

            FlagInternalChange = True
            Mtgc2.SelectedIndex = -1 ' loading items array to the control
            Mtgc2.Items.Clear()
            Mtgc2.LoadingType = MTGCComboBox.CaricamentoCombo.ComboBoxItem 'loadinam duomenis
            Mtgc2.Items.AddRange(ItemsList)
            FlagInternalChange = False

            If BackedUpValue = "" Then Exit Property ' restoring backuped value if needed and possible
            If _MTGC2ID = PropertyID.Col1 Then
                SelectItem(Mtgc2, PropertyID.Col1, BackedUpValue)
            ElseIf _MTGC2ID = PropertyID.Col2 Then
                SelectItem(Mtgc2, PropertyID.Col2, BackedUpValue)
            ElseIf _MTGC2ID = PropertyID.Col3 Then
                SelectItem(Mtgc2, PropertyID.Col3, BackedUpValue)
            ElseIf _MTGC2ID = PropertyID.Col4 Then
                SelectItem(Mtgc2, PropertyID.Col4, BackedUpValue)
            Else ' Tag
                SelectItem(Mtgc2, PropertyID.Tag, BackedUpValue)
            End If
            OnMTGC2IndexChanged(Me, New EventArgs)

        End Set
    End Property

    Public Property MTGC1ColumnsCount() As Integer
        Get
            Return _MTGC1ColumnsCount
        End Get
        Set(ByVal value As Integer)
            If value > 4 Then
                _MTGC1ColumnsCount = 4
            ElseIf value < 1 Then
                _MTGC1ColumnsCount = 1
            Else
                _MTGC1ColumnsCount = value
            End If
            If Not Mtgc1 Is Nothing Then Mtgc1.ColumnNum = _MTGC1ColumnsCount
        End Set
    End Property

    Public Property MTGC1ColumnWidth() As String
        Get
            Return _MTGC1ColumnWidth
        End Get
        Set(ByVal value As String)
            Try
                Mtgc1.ColumnWidth = value
                _MTGC1ColumnWidth = value
            Catch ex As Exception
            End Try
        End Set
    End Property

    Public Property MTGC2ColumnsCount() As Integer
        Get
            Return _MTGC2ColumnsCount
        End Get
        Set(ByVal value As Integer)
            If value > 4 Then
                _MTGC2ColumnsCount = 4
            ElseIf value < 1 Then
                _MTGC2ColumnsCount = 1
            Else
                _MTGC2ColumnsCount = value
            End If
            If Not Mtgc2 Is Nothing Then Mtgc2.ColumnNum = _MTGC2ColumnsCount
        End Set
    End Property

    Public Property MTGC2ColumnWidth() As String
        Get
            Return _MTGC2ColumnWidth
        End Get
        Set(ByVal value As String)
            Try
                Mtgc2.ColumnWidth = value
                _MTGC2ColumnWidth = value
            Catch ex As Exception
            End Try
        End Set
    End Property

    Public Property MTGC1ID() As PropertyID
        Get
            Return _MTGC1ID
        End Get
        Set(ByVal value As PropertyID)
            _MTGC1ID = value
        End Set
    End Property

    Public Property MTGC2ID() As PropertyID
        Get
            Return _MTGC2ID
        End Get
        Set(ByVal value As PropertyID)
            _MTGC2ID = value
        End Set
    End Property

    Public Property DataSource() As Object
        Get
            Return _Datasource
        End Get
        Set(ByVal value As Object)
            If value Is Nothing Then
                _Datasource = Nothing
                BindControls()
                Exit Property
            End If

            If Not TypeOf value Is DataRow Then
                _Datasource = Nothing
                BindControls()
                Throw New Exception("Control data error. Only datarow type is accepted as datasource.")
                Exit Property
            End If

            If CType(value, DataRow).Table.Columns.Count <> 11 Then
                _Datasource = Nothing
                BindControls()
                Throw New Exception("Control data error. Column number is not equal 11.")
                Exit Property
            End If

            _Datasource = value
            BindControls()
        End Set
    End Property

#End Region

#Region "*** OVERRIDES SUBS ***"

    Protected Sub OnTblParentChanged(ByVal sender As Object, _
        ByVal e As System.EventArgs) Handles TblLayoutPnl.ParentChanged
        If Not Me.Parent Is Nothing Then
            If Not Mtgc1 Is Nothing Then
                Mtgc1.ColumnNum = _MTGC1ColumnsCount
                Mtgc1.ColumnWidth = _MTGC1ColumnWidth
            End If
            If Not Mtgc2 Is Nothing Then
                Mtgc2.ColumnNum = _MTGC2ColumnsCount
                Mtgc2.ColumnWidth = _MTGC2ColumnWidth
            End If

            SetControlsPosition()
        End If
    End Sub

    Protected Sub OnTblLocationChanged(ByVal sender As Object, _
        ByVal e As System.EventArgs) Handles TblLayoutPnl.LocationChanged
        SetControlsPosition()
    End Sub

    Protected Sub OnTblDockChanged(ByVal sender As Object, _
        ByVal e As System.EventArgs) Handles TblLayoutPnl.DockChanged
        SetControlsPosition()
    End Sub

    Protected Sub OnTblSizeChanged(ByVal sender As Object, _
        ByVal e As System.EventArgs) Handles TblLayoutPnl.SizeChanged
        SetControlsPosition()
    End Sub

    Protected Overrides Sub OnSizeChanged(ByVal e As System.EventArgs)
        SetControlsPosition()
        MyBase.OnSizeChanged(e)
    End Sub

    Protected Sub OnTblResize(ByVal sender As Object, _
        ByVal e As System.EventArgs) Handles TblLayoutPnl.Resize
        SetControlsPosition()
    End Sub

#End Region

#Region "*** INTERNAL HANDLES ***"

    Private Sub OnMTGC2IndexChanged(ByVal sender As Object, ByVal e As EventArgs) _
        Handles Mtgc2.SelectedIndexChanged
        If FlagInternalChange Then Exit Sub
        If Not _Datasource Is Nothing Then
            CType(_Datasource, DataRow).Item(6) = GetSelectedItem(Mtgc2, PropertyID.Col1)
            CType(_Datasource, DataRow).Item(7) = GetSelectedItem(Mtgc2, PropertyID.Col2)
            CType(_Datasource, DataRow).Item(8) = GetSelectedItem(Mtgc2, PropertyID.Col3)
            CType(_Datasource, DataRow).Item(9) = GetSelectedItem(Mtgc2, PropertyID.Col4)
            CType(_Datasource, DataRow).Item(10) = GetSelectedItem(Mtgc2, PropertyID.Tag)
        End If
    End Sub

    Private Sub OnMTGC1IndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles Mtgc1.SelectedIndexChanged
        If FlagInternalChange Then Exit Sub
        If Not _Datasource Is Nothing Then
            CType(_Datasource, DataRow).Item(0) = GetSelectedItem(Mtgc1, PropertyID.Col1)
            CType(_Datasource, DataRow).Item(1) = GetSelectedItem(Mtgc1, PropertyID.Col2)
            CType(_Datasource, DataRow).Item(2) = GetSelectedItem(Mtgc1, PropertyID.Col3)
            CType(_Datasource, DataRow).Item(3) = GetSelectedItem(Mtgc1, PropertyID.Col4)
            CType(_Datasource, DataRow).Item(4) = GetSelectedItem(Mtgc1, PropertyID.Tag)
        End If
        RaiseEvent OnIndexChenged(Me)
    End Sub

    Private Sub OnTxtBoxChanged(ByVal sender As Object, ByVal e As EventArgs) Handles TxtBox.Validated
        If FlagInternalChange Then Exit Sub
        If Not _Datasource Is Nothing Then
            Try
                CType(_Datasource, DataRow).Item(5) = CDbl(TxtBox.Text)
            Catch ex As Exception
                CType(_Datasource, DataRow).Item(5) = 0
                TxtBox.Text = 0
            End Try
        End If

    End Sub

#End Region

#Region "*** SPEFIFIC SUBS AND FUNCTIONS ***"

    Protected Sub SetControlsPosition()

        If Not Mtgc1 Is Nothing Then
            TblLayoutPnl.ColumnStyles.Item(0).SizeType = SizeType.Percent
            TblLayoutPnl.ColumnStyles.Item(0).Width = (100 - _PercMTGC2 - _PercTxtBox)
        End If

        If Not TxtBox Is Nothing Then
            If _PercTxtBox > 0 Then
                TblLayoutPnl.ColumnStyles.Item(1).SizeType = SizeType.Percent
                TblLayoutPnl.ColumnStyles.Item(1).Width = _PercTxtBox
                TxtBox.Visible = True
            Else
                TblLayoutPnl.ColumnStyles.Item(1).SizeType = SizeType.Percent
                TblLayoutPnl.ColumnStyles.Item(1).Width = 0
                TxtBox.Visible = False
            End If
        End If
        If Not Mtgc2 Is Nothing Then
            If _PercMTGC2 > 0 Then
                TblLayoutPnl.ColumnStyles.Item(2).SizeType = SizeType.Percent
                TblLayoutPnl.ColumnStyles.Item(2).Width = _PercMTGC2
                Mtgc2.Visible = True
            Else
                TblLayoutPnl.ColumnStyles.Item(2).SizeType = SizeType.Percent
                TblLayoutPnl.ColumnStyles.Item(2).Width = 0
                Mtgc2.Visible = False
            End If
        End If
    End Sub

    Private Sub CheckDataSource(ByRef Dt As DataTable)
        If Dt Is Nothing Then Exit Sub
        For i As Integer = 1 To Dt.Rows.Count
            For j As Integer = 1 To Dt.Columns.Count
                Try
                    Dt.Rows(i - 1).Item(j - 1) = Dt.Rows(i - 1).Item(j - 1).ToString
                Catch ex As Exception
                    Dt.Rows(i - 1).Item(j - 1) = ""
                End Try
                If j = 5 AndAlso Dt.Rows(i - 1).Item(j - 1) = "" Then Dt.Rows(i - 1).Item(j - 1) = 0
            Next
        Next
    End Sub

    Private Sub BindControls()
        If _Datasource Is Nothing Then
            If Not Mtgc1 Is Nothing Then Mtgc1.SelectedIndex = -1
            If Not TxtBox Is Nothing Then TxtBox.Text = 0
            If Not Mtgc2 Is Nothing Then Mtgc2.SelectedIndex = -1
            Exit Sub
        End If

        If MTGC1ID = PropertyID.Col1 Then
            SelectItem(Mtgc1, PropertyID.Col1, CType(_Datasource, DataRow).Item(0).ToString)
        ElseIf MTGC1ID = PropertyID.Col2 Then
            SelectItem(Mtgc1, PropertyID.Col2, CType(_Datasource, DataRow).Item(1).ToString)
        ElseIf MTGC1ID = PropertyID.Col3 Then
            SelectItem(Mtgc1, PropertyID.Col3, CType(_Datasource, DataRow).Item(2).ToString)
        ElseIf MTGC1ID = PropertyID.Col4 Then
            SelectItem(Mtgc1, PropertyID.Col4, CType(_Datasource, DataRow).Item(3).ToString)
        Else
            SelectItem(Mtgc1, PropertyID.Tag, CType(_Datasource, DataRow).Item(4).ToString)
        End If

        TxtBox.Text = CType(_Datasource, DataRow).Item(5).ToString
        If CType(_Datasource, DataRow).Item(5).ToString = "" Then
            TxtBox.Text = 0
        Else
            Try
                CType(_Datasource, DataRow).Item(5) = CDbl(CType(_Datasource, DataRow).Item(5).ToString)
            Catch ex As Exception
                TxtBox.Text = 0
                CType(_Datasource, DataRow).Item(5) = 0
            End Try
        End If

        If MTGC2ID = PropertyID.Col1 Then
            SelectItem(Mtgc2, PropertyID.Col1, CType(_Datasource, DataRow).Item(6).ToString)
        ElseIf MTGC2ID = PropertyID.Col2 Then
            SelectItem(Mtgc2, PropertyID.Col2, CType(_Datasource, DataRow).Item(7).ToString)
        ElseIf MTGC2ID = PropertyID.Col3 Then
            SelectItem(Mtgc2, PropertyID.Col3, CType(_Datasource, DataRow).Item(8).ToString)
        ElseIf MTGC2ID = PropertyID.Col4 Then
            SelectItem(Mtgc2, PropertyID.Col4, CType(_Datasource, DataRow).Item(9).ToString)
        Else
            SelectItem(Mtgc2, PropertyID.Tag, CType(_Datasource, DataRow).Item(10).ToString)
        End If
    End Sub

    Private Sub SelectItem(ByRef box As MTGCComboBox, ByVal FieldID As PropertyID, ByVal ValueToSelect As String)
        If box Is Nothing Then Exit Sub
        FlagInternalChange = True

        For i As Integer = 1 To box.Items.Count
            If FieldID = PropertyID.Col1 Then
                If CType(box.Items(i - 1), MTGCComboBoxItem).Col1 = ValueToSelect Then
                    box.SelectedIndex = i - 1
                    FlagInternalChange = False
                    Exit Sub
                End If
            ElseIf FieldID = PropertyID.Col2 Then
                If CType(box.Items(i - 1), MTGCComboBoxItem).Col2 = ValueToSelect Then
                    box.SelectedIndex = i - 1
                    FlagInternalChange = False
                    Exit Sub
                End If
            ElseIf FieldID = PropertyID.Col3 Then
                If CType(box.Items(i - 1), MTGCComboBoxItem).Col3 = ValueToSelect Then
                    box.SelectedIndex = i - 1
                    FlagInternalChange = False
                    Exit Sub
                End If
            ElseIf FieldID = PropertyID.Col4 Then
                If CType(box.Items(i - 1), MTGCComboBoxItem).Col4 = ValueToSelect Then
                    box.SelectedIndex = i - 1
                    FlagInternalChange = False
                    Exit Sub
                End If
            Else ' Tag
                Try
                    If CType(box.Items(i - 1), MTGCComboBoxItem).Tag.ToString = ValueToSelect Then
                        box.SelectedIndex = i - 1
                        FlagInternalChange = False
                        Exit Sub
                    End If
                Catch ex As Exception
                End Try
            End If
        Next
        box.SelectedIndex = -1
        FlagInternalChange = False
    End Sub

    Private Function GetSelectedItem(ByVal box As MTGCComboBox, ByVal FieldID As PropertyID) As String
        If box Is Nothing Then Return ""
        Dim tre As MTGCComboBoxItem = Nothing
        Try
            tre = CType(box.SelectedItem, MTGCComboBoxItem)
        Catch ex As Exception
        End Try
        If tre Is Nothing Then Return ""

        Try
            If FieldID = PropertyID.Col1 Then
                Return tre.Col1
            ElseIf FieldID = PropertyID.Col2 Then
                Return tre.Col2
            ElseIf FieldID = PropertyID.Col3 Then
                Return tre.Col3
            ElseIf FieldID = PropertyID.Col4 Then
                Return tre.Col4
            Else ' Tag
                Return tre.Tag.ToString
            End If
        Catch ex As Exception
            Return ""
        End Try
    End Function

#End Region

    Public Sub SizeChangedExternal()
        SetControlsPosition()
    End Sub

End Class
