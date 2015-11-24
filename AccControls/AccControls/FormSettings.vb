Public Module FormSettings

    Private FormPropertiesDictionary As Dictionary(Of String, FormProperties)
    Private ColumnPropertiesDictionary As Dictionary(Of String, DataGridViewColumnProperties)
    Private XtraGridDictionary As Dictionary(Of String, String)


    Public Sub SetFormProperties(ByRef nForm As Form, ByVal AutoResizeForm As Boolean, _
        ByVal SettingsStringCollection As System.Collections.Specialized.StringCollection)

        If AutoResizeForm Then
            If nForm.WindowState <> FormWindowState.Normal Then _
                nForm.WindowState = FormWindowState.Normal
            Exit Sub
        End If

        If FormPropertiesDictionary Is Nothing Then GetFormPropertiesDictionary(SettingsStringCollection)

        If FormPropertiesDictionary.ContainsKey(GetFullFormName(nForm)) Then

            Dim FormPropertiesToApply As FormProperties = _
                FormPropertiesDictionary(GetFullFormName(nForm))

            If FormPropertiesToApply.Maximized Then
                nForm.WindowState = FormWindowState.Maximized
                Exit Sub
            End If

            nForm.Location = New System.Drawing.Point(FormPropertiesToApply.LocationX, _
                FormPropertiesToApply.LocationY)
            nForm.Size = New System.Drawing.Size(FormPropertiesToApply.Width, _
                FormPropertiesToApply.Height)

        End If

    End Sub

    Public Sub GetFormProperties(ByVal nForm As Form, ByVal AutoResizeForm As Boolean, _
        ByVal SettingsStringCollection As System.Collections.Specialized.StringCollection)

        If AutoResizeForm Then Exit Sub

        If FormPropertiesDictionary Is Nothing Then GetFormPropertiesDictionary(SettingsStringCollection)

        Dim CurrentFormProperties As New FormProperties(nForm)

        If FormPropertiesDictionary.ContainsKey(GetFullFormName(nForm)) Then
            FormPropertiesDictionary.Item(GetFullFormName(nForm)) = CurrentFormProperties
        Else
            FormPropertiesDictionary.Add(GetFullFormName(nForm), CurrentFormProperties)
        End If

    End Sub

    Public Sub SetDataGridViewProperties(ByRef nDataGridView As DataGridView, _
        ByVal AutoResizeDataGridView As Boolean, ByVal SettingsStringCollection _
        As System.Collections.Specialized.StringCollection)

        If ColumnPropertiesDictionary Is Nothing Then GetColumnPropertiesDictionary(SettingsStringCollection)

        For Each column As DataGridViewColumn In nDataGridView.Columns
            If ColumnPropertiesDictionary.ContainsKey(GetFullDataGridViewColumnName(column)) Then
                column.Visible = ColumnPropertiesDictionary.Item( _
                    GetFullDataGridViewColumnName(column)).Visible
                If Not ColumnPropertiesDictionary.Item(GetFullDataGridViewColumnName(column)).DisplayIndex < 0 Then _
                    column.DisplayIndex = ColumnPropertiesDictionary.Item( _
                    GetFullDataGridViewColumnName(column)).DisplayIndex
            End If
        Next

        nDataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        If nDataGridView.RowHeadersVisible Then
            nDataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.EnableResizing
            nDataGridView.RowHeadersWidth = 15
        End If
        nDataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells

        If AutoResizeDataGridView Then
            nDataGridView.AutoSize = True
            nDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
            nDataGridView.AutoResizeColumns()
            nDataGridView.AutoResizeRows()
            nDataGridView.AutoResizeColumnHeadersHeight()
            nDataGridView.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders)
            Exit Sub
        Else
            nDataGridView.AutoSize = False
            nDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
        End If

        For Each column As DataGridViewColumn In nDataGridView.Columns
            If ColumnPropertiesDictionary.ContainsKey(GetFullDataGridViewColumnName(column)) Then
                column.Width = ColumnPropertiesDictionary.Item(GetFullDataGridViewColumnName(column)).Width
            End If
        Next
        nDataGridView.AutoResizeColumnHeadersHeight()

    End Sub

    Public Sub GetDataGridViewProperties(ByVal nDataGridView As DataGridView, _
        ByVal AutoResizeDataGridView As Boolean, ByVal SettingsStringCollection _
        As System.Collections.Specialized.StringCollection)

        If ColumnPropertiesDictionary Is Nothing Then GetColumnPropertiesDictionary(SettingsStringCollection)

        For Each column As DataGridViewColumn In nDataGridView.Columns

            Dim CurrentColumnProperties As New DataGridViewColumnProperties(column)

            If ColumnPropertiesDictionary.ContainsKey(GetFullDataGridViewColumnName(column)) Then
                ColumnPropertiesDictionary.Item(GetFullDataGridViewColumnName(column)) = _
                    CurrentColumnProperties
            Else
                ColumnPropertiesDictionary.Add(GetFullDataGridViewColumnName(column), CurrentColumnProperties)
            End If

        Next

    End Sub

    'Public Sub GetXtraGridViewProperties(ByVal nXtraGrid As DevExpress.XtraGrid.GridControl, _
    '    ByVal AutoResizeDataGridView As Boolean, ByVal SettingsStringCollection _
    '    As System.Collections.Specialized.StringCollection)

    '    If XtraGridDictionary Is Nothing Then GetXtraGridDictionary(SettingsStringCollection)

    '    Dim CurrentXtraGridViewProperties As String

    '    Using ms As New IO.MemoryStream()
    '        Dim options As New DevExpress.Utils.OptionsLayoutGrid()
    '        options.Columns.StoreLayout = True
    '        options.Columns.StoreAppearance = False
    '        options.Columns.RemoveOldColumns = True
    '        options.Columns.AddNewColumns = True
    '        options.StoreAppearance = False
    '        options.StoreAppearance = False
    '        options.StoreDataSettings = False
    '        options.StoreVisualOptions = False
    '        nXtraGrid.MainView.SaveLayoutToStream(ms, options)
    '        CurrentXtraGridViewProperties = Convert.ToBase64String(ms.ToArray())
    '    End Using

    '    If XtraGridDictionary.ContainsKey(GetFullXtraGridName(nXtraGrid)) Then
    '        XtraGridDictionary.Item(GetFullXtraGridName(nXtraGrid)) = CurrentXtraGridViewProperties
    '    Else
    '        XtraGridDictionary.Add(GetFullXtraGridName(nXtraGrid), CurrentXtraGridViewProperties)
    '    End If

    'End Sub

    'Public Sub SetXtraGridProperties(ByRef nXtraGrid As DevExpress.XtraGrid.GridControl, _
    '    ByVal AutoResizeDataGridView As Boolean, ByVal SettingsStringCollection _
    '    As System.Collections.Specialized.StringCollection)

    '    If XtraGridDictionary Is Nothing Then GetXtraGridDictionary(SettingsStringCollection)
    '    If Not XtraGridDictionary.ContainsKey(GetFullXtraGridName(nXtraGrid)) Then Exit Sub

    '    Using ms As New IO.MemoryStream(Convert.FromBase64String( _
    '        XtraGridDictionary(GetFullXtraGridName(nXtraGrid))))
    '        nXtraGrid.MainView.Appearance.RestoreLayoutFromStream(ms)
    '    End Using

    '    'nXtraGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
    '    'nXtraGrid.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader
    '    'nXtraGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells

    '    'If AutoResizeDataGridView Then
    '    '    nXtraGrid.AutoSize = True
    '    '    nXtraGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
    '    '    nXtraGrid.AutoResizeColumns()
    '    '    nXtraGrid.AutoResizeRows()
    '    '    nXtraGrid.AutoResizeColumnHeadersHeight()
    '    '    nXtraGrid.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToDisplayedHeaders)
    '    '    Exit Sub
    '    'Else
    '    '    nXtraGrid.AutoSize = False
    '    '    nXtraGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None
    '    'End If

    '    'nXtraGrid.AutoResizeColumnHeadersHeight()

    'End Sub

    Private Function GetFullFormName(ByVal nForm As Form) As String
        Return nForm.GetType.Namespace.Trim.ToLower & nForm.GetType.Name.Trim.ToLower
    End Function

    Private Function GetFullDataGridViewName(ByVal nDataGridView As DataGridView) As String
        Return GetFullFormName(nDataGridView.FindForm) & nDataGridView.Name.Trim.ToLower
    End Function

    Private Function GetFullDataGridViewColumnName(ByVal nDataGridViewColumn As DataGridViewColumn) As String
        Return GetFullDataGridViewName(nDataGridViewColumn.DataGridView) & _
            nDataGridViewColumn.Name.Trim.ToLower
    End Function

    'Private Function GetFullXtraGridName(ByVal nXtraGrid As DevExpress.XtraGrid.GridControl) As String
    '    Return GetFullFormName(nXtraGrid.FindForm) & nXtraGrid.Name.Trim.ToLower
    'End Function

    Public Sub GetFormPropertiesDictionary(ByVal SettingsStringCollection _
        As System.Collections.Specialized.StringCollection)

        FormPropertiesDictionary = New Dictionary(Of String, FormProperties)
        If SettingsStringCollection Is Nothing Then Exit Sub

        For Each SettingsString As String In SettingsStringCollection
            FormPropertiesDictionary.Add(GetElement(SettingsString, 0).Trim, _
                New FormProperties(SettingsString))
        Next

    End Sub

    Public Sub GetColumnPropertiesDictionary(ByVal SettingsStringCollection _
        As System.Collections.Specialized.StringCollection)

        ColumnPropertiesDictionary = New Dictionary(Of String, DataGridViewColumnProperties)
        If SettingsStringCollection Is Nothing Then Exit Sub

        For Each SettingsString As String In SettingsStringCollection
            ColumnPropertiesDictionary.Add(GetElement(SettingsString, 0).Trim, _
            New DataGridViewColumnProperties(SettingsString))
        Next

    End Sub

    Public Sub GetXtraGridDictionary(ByVal SettingsStringCollection _
        As System.Collections.Specialized.StringCollection)

        XtraGridDictionary = New Dictionary(Of String, String)
        If SettingsStringCollection Is Nothing Then Exit Sub

        Dim KeyValuePairString As String()

        For Each SettingsString As String In SettingsStringCollection
            KeyValuePairString = SettingsString.Split(New Char() {Chr(9)})
            XtraGridDictionary.Add(KeyValuePairString(0), KeyValuePairString(1))
        Next

    End Sub

    Public Function GetFormPropertiesStringCollection(ByVal SettingsStringCollection _
        As System.Collections.Specialized.StringCollection) _
        As System.Collections.Specialized.StringCollection

        Dim result As New System.Collections.Specialized.StringCollection

        If FormPropertiesDictionary Is Nothing Then GetFormPropertiesDictionary(SettingsStringCollection)

        For Each p As KeyValuePair(Of String, FormProperties) In FormPropertiesDictionary
            result.Add(p.Key.Trim & p.Value.ToString)
        Next

        Return result

    End Function

    Public Function GetColumnPropertiesStringCollection(ByVal SettingsStringCollection _
        As System.Collections.Specialized.StringCollection) _
        As System.Collections.Specialized.StringCollection

        Dim result As New System.Collections.Specialized.StringCollection

        If ColumnPropertiesDictionary Is Nothing Then GetColumnPropertiesDictionary(SettingsStringCollection)

        For Each p As KeyValuePair(Of String, DataGridViewColumnProperties) In ColumnPropertiesDictionary
            result.Add(p.Key.Trim & p.Value.ToString)
        Next

        Return result

    End Function

    'Public Function GetXtraGridPropertiesStringCollection(ByVal SettingsStringCollection _
    '    As System.Collections.Specialized.StringCollection) _
    '    As System.Collections.Specialized.StringCollection

    '    Dim result As New System.Collections.Specialized.StringCollection

    '    If XtraGridDictionary Is Nothing Then GetXtraGridDictionary(SettingsStringCollection)

    '    For Each p As KeyValuePair(Of String, String) In XtraGridDictionary
    '        result.Add(p.Key.Trim & Chr(9) & p.Value)
    '    Next

    '    Return result

    'End Function

End Module
