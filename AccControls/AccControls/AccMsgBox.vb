Imports System.Media

''' <summary>
''' An extended MessageBox with lot of customizing capabilities.
''' (c) http://www.codeproject.com/Articles/9656/Dissecting-the-MessageBox
''' </summary>
Public Class AccMsgBox

    ' for strings measuring
    Private MGraphics As Graphics

#Region "CONSTANTS"
    Private Const C_TextVerticalPadding As Integer = 5 ' min y coordinate of msgtxt label
    Private Const C_TextHorizontalPadding As Integer = 5 ' min x coordinate of msgtxt label
    Private Const C_FI As Double = 1.618
    Private Const C_ButtonPaddingOut As Integer = 7 ' padding between the buttons
    Private Const C_ButtonPaddingIn As Integer = 9 ' padding between button text and border
    Private Const C_ButtonMinWidth As Integer = 74
    Private Const C_ButtonHeight As Integer = 26
    Private Const C_ExceptionTextHeight As Integer = 200
#End Region

    ' define form size restrictions in the user system
#Region "CALCULATED CONSTANTS"
    Private C_CaptionHeight As Integer
    Private C_FormBorderWidth As Integer
    Private C_MinFormWidth As Integer ' minimum form width and heigth to comply with FI proportion
    Private C_MinFormHeight As Integer
    Private C_MaxFormWidth As Integer ' max form width to fit the monitor's screen
    Private C_ButtonWidth As Integer
    Private C_ButtonsWidth As Integer

#End Region

#Region "PROPERTIES AND RELATED PRIVATE FIELDS"

    Private PossibleComb As New List(Of String)
    Private Words As New List(Of String)
    Private FIS As New List(Of Double)
    Private _Buttons As List(Of String)
    Private _ButtonPressed As String = ""
    Private ex As Exception = Nothing

    Public Property MessageText() As String
        Get
            Return MsgTxt.Text
        End Get
        Set(ByVal value As String)
            Dim s() As String
            s = value.Split(" ")
            Words.Clear()
            For i As Integer = 1 To s.Length
                Words.Add(s(i - 1))
            Next
        End Set
    End Property

    Public Property ExceptionToShow() As Exception
        Get
            Return ex
        End Get
        Set(ByVal value As Exception)
            ex = value
        End Set
    End Property

    Public Property Buttons() As List(Of String)
        Get
            If _Buttons Is Nothing Then _Buttons = New List(Of String)
            Return _Buttons
        End Get
        Set(ByVal value As List(Of String))
            _Buttons = value
        End Set
    End Property

    Public ReadOnly Property ButtonPressed() As String
        Get
            Return _ButtonPressed
        End Get
    End Property

#End Region

#Region "MESSAGE FORMATING METHODS"

    Private Sub AddPossibleCombs()
        Dim MinLen, MaxLen, MinLenW, MaxLenW As Integer
        MinLen = FirstLineMinLength(MinLenW)
        MaxLen = FirstLineMaxLength(MaxLenW)

        Dim cl, LineCount, i As Integer
        For i = MinLenW To MaxLenW
            cl = Math.Floor(MeasureStringWidth(CreateString(0, i - 1)))
            PossibleComb.Add(GetPossibleComb(cl, i, LineCount))
            FIS.Add(Math.Round((cl + C_FormBorderWidth + (2 * C_TextHorizontalPadding)) _
            / (MeasureStringHeight(PossibleComb(PossibleComb.Count - 1)) + C_TextVerticalPadding _
            + C_CaptionHeight + ButtonsPanel.Height), 4))
        Next
    End Sub

    Private Function GetPossibleComb(ByVal MaxLen As Integer, ByVal MaxLenW As Integer, _
        ByRef LineCount As Integer) As String
        Dim result As String = CreateString(0, MaxLenW - 1)
        LineCount = 1
        If MaxLenW = Words.Count Then Return result

        Dim s As String = ""
        For i As Integer = MaxLenW To Words.Count - 1
            If Math.Floor(MeasureStringWidth(s & " " & Words(i))) > MaxLen Then
                If s = "" Then
                    result = result & vbCrLf & Words(i)
                Else
                    result = result & vbCrLf & s
                    s = Words(i)
                End If
                LineCount = LineCount + 1
            Else
                If i = MaxLenW OrElse s = "" Then
                    s = Words(i)
                Else
                    s = s & " " & Words(i)
                End If
            End If
        Next
        If s <> "" Then
            result = result & vbCrLf & s
        Else
            LineCount = LineCount - 1
        End If

        Return result
    End Function

    Private Function FirstLineMinLength(ByRef FLength As Integer) As Integer
        Dim s As String
        For i As Integer = 1 To Words.Count
            s = CreateString(0, i - 1)
            If FormWidthByString(s) >= C_MinFormWidth Then
                FLength = i
                Return Math.Floor(MeasureStringWidth(s))
            End If
        Next
        FLength = Words.Count
        Return Math.Floor(MeasureStringWidth(CreateString(0, Words.Count - 1)))
    End Function

    Private Function FirstLineMaxLength(ByRef FLength As Integer) As Integer
        Dim s As String
        For i As Integer = Words.Count To 1 Step -1
            s = CreateString(0, i - 1)
            If FormWidthByString(s) <= C_MaxFormWidth Then
                FLength = i
                Return Math.Floor(MeasureStringWidth(s))
            End If
        Next
        FLength = 1
        Return Math.Floor(MeasureStringWidth(Words(0)))
    End Function

    Private Function CreateString(ByVal FromW As Integer, ByVal ToW As Integer) As String
        Dim s As String = Words(FromW)
        For i As Integer = FromW + 1 To ToW
            s = s & " " & Words(i)
        Next
        Return s
    End Function

    Private Function FormWidthByString(ByVal s As String) As Integer
        Return Math.Floor(MeasureStringWidth(s)) + 2 * C_TextHorizontalPadding + C_FormBorderWidth
    End Function

    Private Function MeasureStringWidth(ByVal s As String) As Integer
        Return Math.Ceiling(MGraphics.MeasureString(s, MsgTxt.Font).Width)
    End Function

    Private Function MeasureStringHeight(ByVal s As String) As Integer
        Return Math.Ceiling(MGraphics.MeasureString(s, MsgTxt.Font).Height)
    End Function

    Private Function GetOptimalString() As String
        Dim Cur_FI As Double = 0
        Dim Cur_Idx As Integer
        For i As Integer = 1 To PossibleComb.Count
            If Cur_FI = 0 OrElse (Math.Abs(FIS(i - 1) - C_FI) < Math.Abs(Cur_FI - C_FI)) Then
                Cur_Idx = i - 1
                Cur_FI = FIS(i - 1)
            End If
        Next
        Return PossibleComb(Cur_Idx)
    End Function

#End Region

    Private Sub AccMsgBox_Load(ByVal sender As System.Object, _
            ByVal e As System.EventArgs) Handles MyBase.Load
        MGraphics = CreateGraphics()
        C_CaptionHeight = Me.Height - TextPanel.Height - ButtonsPanel.Height
        C_FormBorderWidth = Me.Width - TextPanel.Width
        C_MinFormHeight = C_CaptionHeight + ButtonsPanel.Height + C_TextVerticalPadding + MsgTxt.Height
        C_MinFormWidth = Math.Round(C_FI * C_MinFormHeight)
        C_MaxFormWidth = Screen.PrimaryScreen.WorkingArea.Width - 100

        If ex IsNot Nothing Then
            Buttons.Clear()
            _Buttons.Add("Ok")
            _Buttons.Add("Detaliau")
            If Not ex.Message.StartsWith("Klaida") Then
                MessageText = "Klaida. " & ex.Message
            Else
                MessageText = ex.Message
            End If
            ShowExceptionText()
        End If

        MeasureButtonsWidth()
        If ex IsNot Nothing Then C_MinFormWidth = Math.Max(C_MinFormWidth, C_MaxFormWidth / 3)
        AddPossibleCombs()
        MsgTxt.Text = GetOptimalString()

        Me.Height = C_CaptionHeight + C_TextVerticalPadding + ButtonsPanel.Height + MsgTxt.Height
        Me.Width = Math.Max(Math.Max(2 * C_TextHorizontalPadding + C_FormBorderWidth + MsgTxt.Width, _
            C_MinFormWidth), Math.Ceiling(Me.Height * C_FI))

        Dim COffset As Integer

        COffset = (Me.TextPanel.Width - MsgTxt.Width - 2 * C_TextHorizontalPadding) / 2
        Me.MsgTxt.Location = New Point(C_TextHorizontalPadding + COffset, C_TextVerticalPadding)

        COffset = (Me.ButtonsPanel.Width - C_ButtonsWidth) / 2
        For i As Integer = 1 To _Buttons.Count
            Dim Btn As New Button
            Btn.Font = New Font(Btn.Font, FontStyle.Bold)
            Btn.Width = C_ButtonWidth
            Btn.Height = ButtonsPanel.Height * 26 / 50
            Btn.Text = _Buttons(i - 1)
            If ex IsNot Nothing AndAlso i = 2 Then
                AddHandler Btn.Click, AddressOf OnShowException
            Else
                AddHandler Btn.Click, AddressOf OnButtonPressed
            End If
            Me.ButtonsPanel.Panel1.Controls.Add(Btn)
            Btn.Location = New Point(COffset + (C_ButtonPaddingOut * i) + (C_ButtonWidth * (i - 1)), 14)
        Next

        Me.CenterToScreen()
        MGraphics.Dispose()

        Dim sound As SystemSound = SystemSounds.Question
        sound.Play()
    End Sub

    Private Sub MeasureButtonsWidth()
        Dim w As Integer = 0
        Dim mw As Integer = 0
        For i As Integer = 1 To _Buttons.Count
            w = MGraphics.MeasureString(_Buttons(i - 1), New Font(MsgTxt.Font, FontStyle.Bold)).Width
            If w > mw Then mw = w
        Next
        mw = mw + 2 * C_ButtonPaddingIn
        C_ButtonWidth = mw
        mw = (mw * _Buttons.Count) + (C_ButtonPaddingOut * (_Buttons.Count + 1))
        If mw + C_FormBorderWidth > C_MinFormWidth And mw + C_FormBorderWidth < C_MaxFormWidth Then
            C_MinFormWidth = mw + C_FormBorderWidth
        End If
        C_ButtonsWidth = mw
    End Sub

    Private Sub OnButtonPressed(ByVal sender As Object, ByVal e As EventArgs)
        _ButtonPressed = sender.text
        Me.Hide()
        Me.Close()
    End Sub

    Private Sub OnShowException(ByVal sender As Object, ByVal e As EventArgs)
        ButtonsPanel.Panel2Collapsed = Not ButtonsPanel.Panel2Collapsed
        If Not ButtonsPanel.Panel2Collapsed Then
            Me.Height = Me.Height + C_ExceptionTextHeight
            ButtonsPanel.Height = ButtonsPanel.Height + C_ExceptionTextHeight
            ButtonsPanel.SplitterDistance = 44
        Else
            Me.Height = Me.Height - C_ExceptionTextHeight
            ButtonsPanel.Height = ButtonsPanel.Height - C_ExceptionTextHeight
        End If
    End Sub

    Private Sub ShowExceptionText()
        If ex Is Nothing Then Exit Sub

        Dim cex As Exception = ex
        Dim TopLv As Integer = 0
        Dim s As String = ""

        While cex IsNot Nothing
            If TopLv < 1 Then
                s = "Top level exception:" & vbCrLf
            Else
                s = s & "Inner exception (" & TopLv.ToString & "):" & vbCrLf
            End If
            s = s & cex.Message & vbCrLf
            s = s & "Source: " & cex.Source & vbCrLf
            If cex.TargetSite IsNot Nothing Then _
                s = s & "TargetSite: " & cex.TargetSite.Name & vbCrLf
            s = s & "Stack trace: " & cex.StackTrace & vbCrLf
            cex = cex.InnerException
        End While
        ExceptionText.Text = s
    End Sub

End Class
