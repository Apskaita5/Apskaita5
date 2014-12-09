Public Structure DataGridViewColumnProperties
    Public Visible As Boolean
    Public Width As Integer
    Public DisplayIndex As Integer

    Public Sub New(ByVal nVisible As Boolean, ByVal nWidth As Integer, ByVal nDisplayIndex As Integer)
        Visible = nVisible
        Width = nWidth
        DisplayIndex = nDisplayIndex
    End Sub

    Friend Sub New(ByVal nDataGridViewColumn As DataGridViewColumn)
        Visible = nDataGridViewColumn.Visible
        Width = nDataGridViewColumn.Width
        DisplayIndex = nDataGridViewColumn.DisplayIndex
    End Sub

    Friend Sub New(ByVal SettingsString As String)
        Width = SafeConvertStrToInt(GetElement(SettingsString, 1), 10)
        Visible = (SafeConvertStrToInt(GetElement(SettingsString, 2), 0) > 0)
        DisplayIndex = SafeConvertStrToInt(GetElement(SettingsString, 3), -1)
    End Sub

    Public Overrides Function ToString() As String
        If Visible Then
            Return Chr(9) & Width.ToString & Chr(9) & "1" & Chr(9) & DisplayIndex.ToString
        Else
            Return Chr(9) & Width.ToString & Chr(9) & "0" & Chr(9) & DisplayIndex.ToString
        End If
    End Function

End Structure