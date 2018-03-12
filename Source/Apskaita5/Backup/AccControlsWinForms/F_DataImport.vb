Public Class F_DataImport

    Private Const UnMapedColumnName As String = "UnMapedColumn"
    Private Const UnMapedColumnCaption As String = ""
    Private ReadOnly DateTimeFormats As String() = New String() {"yyyy-MM-dd HH:mm:ss", "yyyy-MM-dd", _
        "yyyyMMdd", "yyyy/MM/dd", "yyyy\MM\dd"}

    Private _Template As DataTable
    Private _FilePath As String = String.Empty
    Private _Encoding As System.Text.Encoding = System.Text.Encoding.UTF8
    Private WithEvents _RawData As DataTable = Nothing
    Private _Result As DataTable = Nothing


    Public ReadOnly Property Result() As DataTable
        Get
            Return _Result
        End Get
    End Property


    Private ReadOnly Property FieldDelimiter() As String
        Get
            If FieldsTabDelimitedCheckBox.Checked Then Return vbTab
            Return FieldDelimiterTextBox.Text
        End Get
    End Property

    Private ReadOnly Property QuotationMark() As String
        Get
            If QuotationMarksUsedCheckBox.Checked Then Return QuotationMarkTextBox.Text
            Return String.Empty
        End Get
    End Property



    Private Sub New(ByVal template As DataTable, ByVal filePath As String, ByVal fileEncoding As System.Text.Encoding)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

        _Template = template
        _FilePath = filePath
        _Encoding = fileEncoding

    End Sub



    Private Sub F_DataImport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        RefreshButton_Click(RefreshButton, EventArgs.Empty)
    End Sub


    Private Sub RefreshButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles RefreshButton.Click

        Dim source As DataTable
        Dim sourceString As String

        If _FilePath Is Nothing OrElse String.IsNullOrEmpty(_FilePath.Trim) Then

            sourceString = System.Windows.Forms.Clipboard.GetText()
            If sourceString Is Nothing OrElse String.IsNullOrEmpty(sourceString.Trim) Then
                MsgBox("Klaida. Clipboard'e nėra nukopijuota jokio teksto.", MsgBoxStyle.Exclamation, "Klaida")
                Exit Sub
            End If

            Try
                source = ParseString(sourceString)
            Catch ex As Exception
                ShowError(ex)
                Exit Sub
            End Try

        Else

            Try
                sourceString = System.IO.File.ReadAllText(_FilePath, _Encoding)
            Catch ex As Exception
                ShowError(ex)
                Exit Sub
            End Try

            If sourceString Is Nothing OrElse String.IsNullOrEmpty(sourceString.Trim) Then
                MsgBox(String.Format("Klaida. Failas {0} tuščias.", _FilePath), MsgBoxStyle.Exclamation, "Klaida")
                Exit Sub
            End If

            source = ParseString(sourceString)

        End If

        DataGridView1.DataSource = Nothing

        If Not _RawData Is Nothing Then
            RemoveHandler _RawData.ColumnChanged, AddressOf RawData_ColumnChanging
            _RawData.Dispose()
            _RawData = Nothing
        End If

        _RawData = source
        AddHandler _RawData.ColumnChanged, AddressOf RawData_ColumnChanging

        DataGridView1.DataSource = _RawData

        SetGridColumnHeaderText()
        ValidateAllValues()

    End Sub

    Private Sub FieldsTabDelimitedCheckBox_CheckedChanged(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles FieldsTabDelimitedCheckBox.CheckedChanged
        FieldDelimiterTextBox.ReadOnly = FieldsTabDelimitedCheckBox.Checked
    End Sub

    Private Sub LinesCrLfDelimitedCheckBox_CheckedChanged(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles LinesCrLfDelimitedCheckBox.CheckedChanged
        LinesDelimiterTextBox.ReadOnly = LinesCrLfDelimitedCheckBox.Checked
    End Sub

    Private Sub QuotationMarksUsedCheckBox_CheckedChanged(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles QuotationMarksUsedCheckBox.CheckedChanged
        QuotationMarkTextBox.ReadOnly = Not QuotationMarksUsedCheckBox.Checked
    End Sub

    Private Sub DataGridView1_ColumnDisplayIndexChanged(ByVal sender As Object, _
        ByVal e As System.Windows.Forms.DataGridViewColumnEventArgs) Handles DataGridView1.ColumnDisplayIndexChanged
        SetGridColumnHeaderText()
        ValidateAllValues()
    End Sub

    Private Sub AddColumnButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles AddColumnButton.Click

        If _RawData Is Nothing Then Exit Sub

        If _RawData.Columns.Count < _Template.Columns.Count Then
            _RawData.Columns.Add(New DataColumn(_Template.Columns(_RawData.Columns.Count).ColumnName, GetType(String)))
            ValidateAllValuesInColumn(_RawData.Columns(_RawData.Columns.Count - 1))
        Else
            _RawData.Columns.Add(New DataColumn(UnMapedColumnName & (_RawData.Columns.Count - _
                _Template.Columns.Count + 1).ToString, GetType(String)))
        End If

        SetGridColumnHeaderText()

    End Sub

    Private Sub ImportButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImportButton.Click

        If _RawData Is Nothing Then Exit Sub

        If _RawData.Columns.Count < _Template.Columns.Count Then
            MsgBox("Klaida. Importuotų duomenų stulpelių skaičius yra mažesnis nei reikalaujama.", _
                MsgBoxStyle.Exclamation, "Klaida")
            Exit Sub
        ElseIf _RawData.GetErrors().Length > 0 Then
            If Not YesOrNo("Ne visus įkrautus duomenis galima konvertuoti į reikalingą formatą. Ar tikrai norite tęsti?") Then Exit Sub
        End If

        Dim res As DataTable = GetTemplateClone()
        For i As Integer = 1 To _RawData.Rows.Count
            res.Rows.Add()
        Next

        Try
            For i As Integer = 1 To _Template.Columns.Count

                Dim col As DataColumn = GetColumnAtDisplayIndex(i - 1)
                Dim requiredType As Type = _Template.Columns(i - 1).DataType

                For j As Integer = 1 To _RawData.Rows.Count

                    If requiredType Is GetType(String) Then
                        res.Rows(j - 1).Item(i - 1) = _RawData.Rows(j - 1).Item(col).ToString
                    ElseIf requiredType Is GetType(Long) Then
                        Dim value As Long = 0
                        TryParse(_RawData.Rows(j - 1).Item(col).ToString, value)
                        res.Rows(j - 1).Item(i - 1) = value
                    ElseIf requiredType Is GetType(Integer) Then
                        Dim value As Integer = 0
                        TryParse(_RawData.Rows(j - 1).Item(col).ToString, value)
                        res.Rows(j - 1).Item(i - 1) = value
                    ElseIf requiredType Is GetType(Byte) Then
                        Dim value As Byte = 0
                        TryParse(_RawData.Rows(j - 1).Item(col).ToString, value)
                        res.Rows(j - 1).Item(i - 1) = value
                    ElseIf requiredType Is GetType(Boolean) Then
                        Dim value As Boolean = False
                        TryParse(_RawData.Rows(j - 1).Item(col).ToString, value)
                        res.Rows(j - 1).Item(i - 1) = value
                    ElseIf requiredType Is GetType(Double) Then
                        Dim value As Double = 0
                        TryParse(_RawData.Rows(j - 1).Item(col).ToString, value)
                        res.Rows(j - 1).Item(i - 1) = value
                    ElseIf requiredType Is GetType(Decimal) Then
                        Dim value As Decimal = 0
                        TryParse(_RawData.Rows(j - 1).Item(col).ToString, value)
                        res.Rows(j - 1).Item(i - 1) = value
                    ElseIf requiredType Is GetType(DateTime) Then
                        Dim value As DateTime = Today
                        TryParse(_RawData.Rows(j - 1).Item(col).ToString, value)
                        res.Rows(j - 1).Item(i - 1) = value
                    Else
                        Throw New NotImplementedException(String.Format("Klaida. Duomenų tipas {0} neimplementuotas.", _
                            requiredType.FullName))
                    End If

                Next

            Next
        Catch ex As Exception
            ShowError(ex)
            Exit Sub
        End Try

        _Result = res

        Me.Close()

    End Sub

    Private Function GetTemplateClone() As DataTable
        Dim result As New DataTable
        For Each col As DataColumn In _Template.Columns
            Dim newCol As New DataColumn(col.ColumnName, col.DataType)
            newCol.Caption = col.Caption
            result.Columns.Add(newCol)
        Next
        Return result
    End Function

    Private Function GetColumnAtDisplayIndex(ByVal i As Integer)
        For Each col As System.Windows.Forms.DataGridViewColumn In DataGridView1.Columns
            If col.DisplayIndex = i Then
                If _RawData.Columns(col.DataPropertyName) Is Nothing Then
                    Throw New Exception("Klaida. Nerastas stulpelis pagal indeksą.")
                End If
                Return _RawData.Columns(col.DataPropertyName)
            End If
        Next
        Throw New Exception("Klaida. Nerastas stulpelis pagal indeksą.")
    End Function

    Private Sub DiscardButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DiscardButton.Click
        Me.Close()
    End Sub



    Public Shared Function GetImportedData(ByVal template As DataTable) As DataTable
        If Not ValidateTemplate(template) Then Return Nothing
        Using frm As New F_DataImport(template, String.Empty, System.Text.Encoding.UTF8)
            frm.ShowDialog()
            Return frm.Result
        End Using
    End Function

    Public Shared Function GetImportedData(ByVal template As DataTable, ByVal filePath As String) As DataTable
        If Not ValidateTemplate(template) OrElse Not ValidateFilePath(filePath) Then Return Nothing
        Using frm As New F_DataImport(template, filePath, System.Text.Encoding.UTF8)
            frm.ShowDialog()
            Return frm.Result
        End Using
    End Function

    Public Shared Function GetImportedData(ByVal template As DataTable, ByVal filePath As String, _
        ByVal fileEncoding As System.Text.Encoding) As DataTable
        If Not ValidateTemplate(template) OrElse Not ValidateFilePath(filePath) Then Return Nothing
        If fileEncoding Is Nothing Then
            fileEncoding = System.Text.Encoding.UTF8
        End If
        Using frm As New F_DataImport(template, filePath, fileEncoding)
            frm.ShowDialog()
            Return frm.Result
        End Using
    End Function

    Private Shared Function ValidateTemplate(ByVal template As DataTable) As Boolean
        If template Is Nothing Then
            ShowError(New ArgumentNullException("template"))
            Return False
        ElseIf template.Columns.Count < 1 Then
            ShowError(New ArgumentException("Template should contain at least 1 column.", "template"))
            Return False
        End If

        For Each col As DataColumn In template.Columns
            If col.Caption Is Nothing OrElse String.IsNullOrEmpty(col.Caption.Trim) Then
                ShowError(New ArgumentException("All template columns should have a non empty Caption.", "template"))
                Return False
            ElseIf col.ColumnName Is Nothing OrElse String.IsNullOrEmpty(col.ColumnName.Trim) Then
                ShowError(New ArgumentException("All template columns should have a non empty ColumnName.", "template"))
                Return False
            ElseIf col.DataType Is Nothing Then
                ShowError(New ArgumentException("All template columns should have a DataType set.", "template"))
                Return False
            ElseIf Not col.DataType Is GetType(Double) AndAlso Not col.DataType Is GetType(Integer) _
                AndAlso Not col.DataType Is GetType(Long) AndAlso Not col.DataType Is GetType(Byte) _
                AndAlso Not col.DataType Is GetType(DateTime) AndAlso Not col.DataType Is GetType(Boolean) _
                AndAlso Not col.DataType Is GetType(String) Then
                ShowError(New ArgumentException(String.Format("Column DataType {0} is not supported.", _
                    col.DataType.ToString), "template"))
                Return False
            End If
        Next

        Return True

    End Function

    Private Shared Function ValidateFilePath(ByVal filePath As String) As Boolean
        If filePath Is Nothing OrElse String.IsNullOrEmpty(filePath) Then
            ShowError(New ArgumentNullException("filePath"))
            Return False
        ElseIf Not System.IO.File.Exists(filePath) Then
            ShowError(New System.IO.FileNotFoundException(String.Format("File {0} does not exist.", filePath), filePath))
            Return False
        End If
        Return True
    End Function

    Private Function ParseString(ByVal source As String) As DataTable

        Dim result As DataTable = GetRawDataTemplate()

        Dim nextUnmapedColumnIndex As Integer = 1

        For Each line As String In Split(source, GetLinesDelimiter(source), QuotationMark, True)

            result.Rows.Add()

            Dim fields As String() = Split(line, FieldDelimiter, QuotationMark, False)

            For i As Integer = 1 To fields.Length
                If i >= result.Columns.Count Then
                    Dim newCol As New DataColumn(UnMapedColumnName & nextUnmapedColumnIndex.ToString, GetType(String))
                    newCol.Caption = UnMapedColumnCaption
                    result.Columns.Add(newCol)
                    nextUnmapedColumnIndex += 1
                End If
                result.Rows(result.Rows.Count - 1).Item(i - 1) = fields(i - 1)
            Next

        Next

        Return result

    End Function

    Private Function GetRawDataTemplate() As DataTable
        Dim result As New DataTable
        For Each col As DataColumn In _Template.Columns
            result.Columns.Add(New DataColumn(col.ColumnName, GetType(String)))
        Next
        Return result
    End Function

    Private Function GetLinesDelimiter(ByVal source As String) As String
        If Not LinesCrLfDelimitedCheckBox.Checked Then Return LinesDelimiterTextBox.Text
        If source.Contains(vbCrLf) Then Return vbCrLf
        If source.Contains(vbCr) Then Return vbCr
        If source.Contains(vbLf) Then Return vbLf
        Return vbCrLf
    End Function

    Private Function Split(ByVal source As String, ByVal delimiter As String, ByVal quote As String, _
        ByVal removeEmptyEntries As Boolean) As String()

        Dim rawResult As String()
        If removeEmptyEntries Then
            rawResult = source.Split(source.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
        Else
            rawResult = source.Split(source.ToCharArray(), StringSplitOptions.None)
        End If

        If quote Is Nothing OrElse String.IsNullOrEmpty(quote.Trim) Then Return rawResult

        Dim result As New List(Of String)

        For Each entry As String In rawResult
            If result.Count > 0 AndAlso (result(result.Count - 1).Split(quote.ToCharArray(), _
                StringSplitOptions.None).Length - 1) Mod 2 > 0 Then
                result(result.Count - 1) = result(result.Count - 1) & entry
            Else
                result.Add(entry)
            End If
        Next

        Return result.ToArray()

    End Function


    Private Sub SetGridColumnHeaderText()
        For Each col As System.Windows.Forms.DataGridViewColumn In DataGridView1.Columns
            Try
                col.HeaderText = _Template.Columns(col.DisplayIndex).Caption
            Catch ex As Exception
                col.HeaderText = UnMapedColumnCaption
            End Try
        Next
    End Sub


    Private Sub RawData_ColumnChanging(ByVal sender As Object, ByVal e As DataColumnChangeEventArgs)

        Dim requiredType As Type = Nothing

        For Each col As System.Windows.Forms.DataGridViewColumn In DataGridView1.Columns
            If col.DataPropertyName = e.Column.ColumnName Then
                If col.DisplayIndex < _Template.Columns.Count Then
                    requiredType = _Template.Columns(col.DisplayIndex).DataType
                    Exit For
                Else
                    e.Row.SetColumnError(e.Column, String.Empty)
                    Exit Sub
                End If
            End If
        Next

        If requiredType Is Nothing Then
            MsgBox("Klaida.Nerastas stulpelio indeksas.", MsgBoxStyle.Critical, "Klaida")
            Exit Sub
        End If

        ValidateValue(e.ProposedValue, e.Row, e.Column, requiredType)

    End Sub

    Private Sub ValidateAllValues()

        For Each col As DataColumn In _RawData.Columns

            ValidateAllValuesInColumn(col)

        Next

    End Sub

    Private Sub ValidateAllValuesInColumn(ByVal col As DataColumn)

        Dim requiredType As Type = Nothing

        For Each gridCol As System.Windows.Forms.DataGridViewColumn In DataGridView1.Columns
            If gridCol.DataPropertyName = col.ColumnName Then
                If gridCol.DisplayIndex < _Template.Columns.Count Then
                    requiredType = _Template.Columns(gridCol.DisplayIndex).DataType
                    Exit For
                Else
                    For Each row As DataRow In _RawData.Rows
                        row.SetColumnError(col, String.Empty)
                    Next
                    Exit Sub
                End If
            End If
        Next

        If requiredType Is Nothing Then
            MsgBox("Klaida.Nerastas stulpelio indeksas.", MsgBoxStyle.Critical, "Klaida")
            Exit Sub
        End If

        For Each row As DataRow In _RawData.Rows
            ValidateValue(row.Item(col).ToString, row, col, requiredType)
        Next

    End Sub

    Private Sub ValidateValue(ByVal value As String, ByVal row As DataRow, ByVal col As DataColumn, ByVal requiredType As Type)

        If requiredType Is Nothing Then
            MsgBox("Klaida.Nerastas stulpelio indeksas.", MsgBoxStyle.Critical, "Klaida")
            Exit Sub
        End If

        If requiredType Is GetType(String) Then
            row.SetColumnError(col, String.Empty)
        ElseIf requiredType Is GetType(Long) Then
            If TryParse(value, New Long) Then
                row.SetColumnError(col, String.Empty)
            Else
                row.SetColumnError(col, "Nesikonvertuoja į skaičių.")
            End If
        ElseIf requiredType Is GetType(Integer) Then
            If TryParse(value, New Integer) Then
                row.SetColumnError(col, String.Empty)
            Else
                row.SetColumnError(col, "Nesikonvertuoja į skaičių.")
            End If
        ElseIf requiredType Is GetType(Byte) Then
            If TryParse(value, New Byte) Then
                row.SetColumnError(col, String.Empty)
            Else
                row.SetColumnError(col, "Nesikonvertuoja į skaičių arba didesnis nei 255.")
            End If
        ElseIf requiredType Is GetType(Boolean) Then
            If TryParse(value, New Boolean) Then
                row.SetColumnError(col, String.Empty)
            Else
                row.SetColumnError(col, "Nesikonvertuoja į loginę išraišką (Taip/Ne, True/False).")
            End If
        ElseIf requiredType Is GetType(Double) Then
            If TryParse(value, New Double) Then
                row.SetColumnError(col, String.Empty)
            Else
                row.SetColumnError(col, "Nesikonvertuoja į skaičių.")
            End If
        ElseIf requiredType Is GetType(Decimal) Then
            If TryParse(value, New Decimal) Then
                row.SetColumnError(col, String.Empty)
            Else
                row.SetColumnError(col, "Nesikonvertuoja į skaičių.")
            End If
        ElseIf requiredType Is GetType(DateTime) Then
            If TryParse(value, New DateTime) Then
                row.SetColumnError(col, String.Empty)
            Else
                row.SetColumnError(col, String.Format("Nesikonvertuoja į datą, galimi formatai: {0}.", _
                    String.Join(", ", DateTimeFormats)))
            End If
        Else
            row.SetColumnError(col, String.Format("Klaida. Duomenų tipas {0} neimplementuotas.", requiredType.FullName))
        End If

    End Sub


    Private Function TryParse(ByVal stringValue As String, ByRef value As Integer) As Boolean
        If stringValue Is Nothing Then Return False
        Return Integer.TryParse(stringValue, System.Globalization.NumberStyles.Any, _
            System.Globalization.CultureInfo.CurrentCulture, value)
    End Function

    Private Function TryParse(ByVal stringValue As String, ByRef value As Long) As Boolean
        If stringValue Is Nothing Then Return False
        Return Long.TryParse(stringValue, System.Globalization.NumberStyles.Any, _
            System.Globalization.CultureInfo.CurrentCulture, value)
    End Function

    Private Function TryParse(ByVal stringValue As String, ByRef value As Byte) As Boolean
        If stringValue Is Nothing Then Return False
        Return Byte.TryParse(stringValue, System.Globalization.NumberStyles.Any, _
            System.Globalization.CultureInfo.CurrentCulture, value)
    End Function

    Private Function TryParse(ByVal stringValue As String, ByRef value As Double) As Boolean
        If stringValue Is Nothing Then Return False
        Return Double.TryParse(stringValue, System.Globalization.NumberStyles.Any, _
            System.Globalization.CultureInfo.CurrentCulture, value)
    End Function

    Private Function TryParse(ByVal stringValue As String, ByRef value As Decimal) As Boolean
        If stringValue Is Nothing Then Return False
        Return Decimal.TryParse(stringValue, System.Globalization.NumberStyles.Any, _
            System.Globalization.CultureInfo.CurrentCulture, value)
    End Function

    Private Function TryParse(ByVal stringValue As String, ByRef value As Boolean) As Boolean

        If stringValue Is Nothing Then Return False

        Dim intValue As Integer

        If stringValue.Trim.ToUpper = "TRUE" OrElse stringValue.Trim.ToUpper = "T" OrElse _
            stringValue.Trim.ToUpper = "TAIP" OrElse stringValue.Trim.ToUpper = "X" Then
            value = True
            Return True
        ElseIf stringValue.Trim.ToUpper = "FALSE" OrElse stringValue.Trim.ToUpper = "F" OrElse _
            stringValue.Trim.ToUpper = "NE" OrElse stringValue.Trim.ToUpper = "N" OrElse String.IsNullOrEmpty(stringValue.Trim) Then
            value = False
            Return True
        ElseIf TryParse(stringValue, intValue) Then
            value = intValue > 0
            Return True
        End If

        Return False

    End Function

    Private Function TryParse(ByVal stringValue As String, ByRef value As DateTime) As Boolean

        If stringValue Is Nothing Then Return False

        Try
            value = DateTime.ParseExact(stringValue, DateTimeFormats, _
                System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None)
            Return True
        Catch ex As Exception
        End Try

        Return False

    End Function
    
    
End Class