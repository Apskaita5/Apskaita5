Public Module AccMsgBoxMethods

    Public Function Taip_Ne(ByVal Question As String) As Boolean
        Dim frm As New AccMsgBox
        frm.MessageText = Question
        frm.Buttons.Add("Taip")
        frm.Buttons.Add("Ne")
        frm.ShowDialog()
        Return frm.ButtonPressed = "Taip"
    End Function

    Public Function Paklausti(ByVal Question As String, ByVal Button1 As String, _
        ByVal Button2 As String, Optional ByVal Button3 As String = "", _
        Optional ByVal Button4 As String = "", Optional ByVal Button5 As String = "", _
        Optional ByVal Button6 As String = "") As String
        Dim frm As New AccMsgBox
        frm.MessageText = Question
        frm.Buttons.Add(Button1)
        frm.Buttons.Add(Button2)
        If Button3 <> "" Then frm.Buttons.Add(Button3)
        If Button4 <> "" Then frm.Buttons.Add(Button4)
        If Button5 <> "" Then frm.Buttons.Add(Button5)
        If Button6 <> "" Then frm.Buttons.Add(Button6)
        frm.ShowDialog()
        Return frm.ButtonPressed
    End Function

    Public Sub ShowException(ByVal ex As Exception)
        Dim frm As New AccMsgBox
        frm.ExceptionToShow = ex
        frm.ShowDialog()
    End Sub

End Module
