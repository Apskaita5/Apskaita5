Imports System.Net
Imports System.Text
Imports HtmlAgilityPack
Imports AccWebCrawler

Public Class Form1

    Private myCookieCollection As New CookieCollection


    Private Sub GoButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles GoButton.Click

        Using frm As New F_LaunchWebCrawler(UrlTextBox.Text, True)
            frm.ShowDialog()
        End Using
        Exit Sub

        If String.IsNullOrEmpty(UserAgentTextBox.Text.Trim) Then
            MsgBox("UserAgent is not specified.", MsgBoxStyle.Exclamation, "Error")
            Exit Sub
        ElseIf String.IsNullOrEmpty(AcceptTextBox.Text.Trim) Then
            MsgBox("Accept is not specified.", MsgBoxStyle.Exclamation, "Error")
            Exit Sub
        ElseIf String.IsNullOrEmpty(UrlTextBox.Text.Trim) Then
            MsgBox("URL is not specified.", MsgBoxStyle.Exclamation, "Error")
            Exit Sub
        End If

        Dim CurrentCursor As Cursor = Me.Cursor
        Me.Cursor = Cursors.WaitCursor

        Try

            Dim request As HttpWebRequest = WebRequest.Create(UrlTextBox.Text.Trim)

            request.UserAgent = UserAgentTextBox.Text.Trim
            request.Accept = AcceptTextBox.Text.Trim
            If Not String.IsNullOrEmpty(Header1TextBox.Text.Trim) Then _
                request.Headers.Add(Header1TextBox.Text.Trim)
            If Not String.IsNullOrEmpty(Header2TextBox.Text.Trim) Then _
                request.Headers.Add(Header2TextBox.Text.Trim)
            request.KeepAlive = True
            request.CookieContainer = New CookieContainer()
            request.Method = WebRequestMethods.Http.Post
            If Not String.IsNullOrEmpty(ContentTypeTextBox.Text.Trim) Then _
                request.ContentType = ContentTypeTextBox.Text.Trim
            If Not String.IsNullOrEmpty(RefererTextBox.Text.Trim) Then _
                request.Referer = RefererTextBox.Text.Trim

            If Not String.IsNullOrEmpty(ContentTextBox.Text.Trim) Then

                Dim byteArray As Byte() = Encoding.ASCII.GetBytes(ContentTextBox.Text.Trim)
                request.ContentLength = byteArray.Length
                Using newStream As IO.Stream = request.GetRequestStream()
                    newStream.Write(byteArray, 0, byteArray.Length)
                    newStream.Close()
                End Using

            Else

                request.ContentLength = 0

            End If

            Dim response As HttpWebResponse = request.GetResponse

            myCookieCollection.Add(response.Cookies)

            CookiesTextBox.Text = GetCookiesText()

            ResponseTextBox.Text = response.Headers.ToString

            Using sr As New IO.StreamReader(response.GetResponseStream())
                ResponseTextBox.Text += vbCrLf & vbCrLf & sr.ReadToEnd
                sr.Close()
            End Using

            response.Close()

            Me.Cursor = CurrentCursor

            Me.TabControl1.SelectedTab = Me.TabPage1

        Catch ex As Exception
            Me.Cursor = CurrentCursor
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Error")
        End Try

    End Sub

    Private Sub CleanCookiesButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles CleanCookiesButton.Click
        myCookieCollection = New CookieCollection
        CookiesTextBox.Text = ""
    End Sub


    Private Function GetCookiesText() As String
        Dim result As String = ""
        For Each c As Cookie In myCookieCollection
            result += c.ToString & vbCrLf
        Next
        Return result.Trim
    End Function

    Private Sub PreprogramedTestButton_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles PreprogramedTestButton.Click

        Dim CurrentCursor As Cursor = Me.Cursor
        Me.Cursor = Cursors.WaitCursor

        Try

            Dim result As PersonInfo() = PersonInfo.GetPersonInfoListRekvizitai("achemos")
            Using frm As New PersonInfoListDialog(result)
                frm.ShowDialog()
            End Using
            Me.Cursor = CurrentCursor

            Me.TabControl1.SelectedTab = Me.TabPage1

        Catch ex As Exception
            Me.Cursor = CurrentCursor
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, "Error")
        End Try

    End Sub

End Class
