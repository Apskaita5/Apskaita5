Public Structure FormProperties
    Public Width As Integer
    Public Height As Integer
    Public LocationX As Integer
    Public LocationY As Integer
    Public Maximized As Boolean

    Public Sub New(ByVal nWidth As Integer, ByVal nHeight As Integer, _
        ByVal nLocationX As Integer, ByVal nLocationY As Integer, ByVal nMaximized As Boolean)
        Width = nWidth
        Height = nHeight
        LocationX = nLocationX
        LocationY = nLocationY
        Maximized = nMaximized
    End Sub

    Friend Sub New(ByVal SettingsString As String)
        Width = SafeConvertStrToInt(GetElement(SettingsString, 1), 100)
        Height = SafeConvertStrToInt(GetElement(SettingsString, 2), 100)
        LocationX = SafeConvertStrToInt(GetElement(SettingsString, 3), 50)
        LocationY = SafeConvertStrToInt(GetElement(SettingsString, 4), 50)
        Maximized = (SafeConvertStrToInt(GetElement(SettingsString, 5), 0) > 0)
    End Sub

    Friend Sub New(ByVal nForm As Form)
        Width = nForm.Width
        Height = nForm.Height
        LocationX = nForm.Location.X
        LocationY = nForm.Location.Y
        Maximized = (nForm.WindowState = FormWindowState.Maximized)
    End Sub

    Public Overrides Function ToString() As String
        If Maximized Then
            Return Chr(9) & Width.ToString & Chr(9) & Height.ToString & Chr(9) _
                & LocationX.ToString & Chr(9) & LocationY.ToString & Chr(9) & "1"
        Else
            Return Chr(9) & Width.ToString & Chr(9) & Height.ToString & Chr(9) _
                & LocationX.ToString & Chr(9) & LocationY.ToString & Chr(9) & "0"
        End If
    End Function

End Structure