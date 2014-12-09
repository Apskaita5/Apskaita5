
Imports System.Collections.Generic
Imports System.Text
Imports System.Drawing.Imaging
Imports System.Drawing
Imports System.Windows.Forms
Imports System.Drawing.Drawing2D
Imports System.Drawing.Text
Namespace DGVColumnSelector

    Public Class MenuControl
        Private Shared m_iImageColumnWidth As Integer = 24
        Private Shared m_iExtraWidth As Integer = 15

        Private Class MenuCommand

            Public ReadOnly Property Height() As Integer
                Get
                    Return IIf(Separator, 5, 21)
                End Get
            End Property
            Public ReadOnly Property Separator() As Boolean
                Get
                    Return m_csText = "-"
                End Get
            End Property

            Private m_csText As String
            'private int m_iIndex;
            Private m_bChecked As Boolean
            Private m_bDone As Boolean
            Public ReadOnly Property Text() As String
                Get
                    Return m_csText
                End Get
            End Property
            'public int Index { get { return m_iIndex; } }
            Public ReadOnly Property Done() As Boolean
                Get
                    Return m_bDone
                End Get
            End Property
            Public Property Checked() As Boolean
                Get
                    Return m_bChecked
                End Get
                Set(ByVal value As Boolean)
                    m_bChecked = value
                End Set
            End Property
            'public MenuCommand(string csText, int iIndex, bool bChecked)
            Public Sub New(ByVal csText As String, ByVal bChecked As Boolean)
                Me.New(csText, bChecked, False)
            End Sub
            Public Sub New(ByVal csText As String)
                Me.New(csText, False, False)
            End Sub
            Public Sub New(ByVal csText As String, ByVal bChecked As Boolean, ByVal bDone As Boolean)
                m_csText = csText
                'm_iIndex = iIndex;
                m_bChecked = bChecked
                m_bDone = bDone
            End Sub

            Public Shared Operator <>(ByVal a As MenuCommand, ByVal b As MenuCommand) As Boolean
                If a Is Nothing And b Is Nothing Then Return False
                If a Is Nothing OrElse b Is Nothing Then Return True
                Return a.m_csText <> b.m_csText
            End Operator

            Public Shared Operator =(ByVal a As MenuCommand, ByVal b As MenuCommand) As Boolean
                Return Not (a <> b)
            End Operator

        End Class

        Private m_pTracMenuItem As MenuCommand = Nothing
        Private m_pMenuCommands As New List(Of MenuCommand)()

        Private m_pMemBitmap As Bitmap
        ' = new Bitmap(panel1.Width, panel1.Height, PixelFormat.Format32bppArgb);
        Private m_pMemGraphics As Graphics

        Public ReadOnly Property Width() As Integer
            Get
                Return m_pMemBitmap.Width
            End Get
        End Property
        Public ReadOnly Property Height() As Integer
            Get
                Return m_pMemBitmap.Height
            End Get
        End Property

        Public ReadOnly Property Done() As Boolean
            Get
                Return m_pTracMenuItem IsNot Nothing AndAlso m_pTracMenuItem.Done
            End Get
        End Property
        Public ReadOnly Property HitIndex() As Integer
            Get
                Return m_pMenuCommands.IndexOf(m_pTracMenuItem)
            End Get
        End Property

        Public Function ChangeChecked(ByVal iIndex As Integer, ByVal g As Graphics) As Boolean
            m_pMenuCommands(iIndex).Checked = Not m_pMenuCommands(iIndex).Checked
            Draw(g)
            Return m_pMenuCommands(iIndex).Checked
        End Function

        Public Sub Add(ByVal csText As String, ByVal bChecked As Boolean)
            m_pMenuCommands.Add(New MenuCommand(csText, bChecked))
        End Sub

        Public Sub Prepare(ByVal g As Graphics)
            m_pMenuCommands.Add(New MenuCommand("-"))
            Dim pDone As New MenuCommand("Done", False, True)
            m_pMenuCommands.Add(pDone)

            Dim iHeight As Integer = 4
            '(2 + 2 top + bottom);
            Dim fWidth As Single = 0
            For Each pMenuCommand As MenuCommand In m_pMenuCommands
                iHeight += pMenuCommand.Height
                Dim pSizeF As SizeF = g.MeasureString(pMenuCommand.Text, SystemInformation.MenuFont)
                fWidth = Math.Max(fWidth, pSizeF.Width)
            Next
            Dim iWidth As Integer = CInt(fWidth) + m_iImageColumnWidth + m_iExtraWidth

            m_pMemBitmap = New Bitmap(iWidth, iHeight)
            m_pMemGraphics = Graphics.FromImage(m_pMemBitmap)
        End Sub

        Private Function HitTest(ByVal X As Integer, ByVal Y As Integer) As MenuCommand
            If X < 0 OrElse X > Width OrElse Y < 0 OrElse Y > Height Then
                Return Nothing
            End If

            Dim iHeight As Integer = 2
            For Each pMenuCommand As MenuCommand In m_pMenuCommands
                If Y > iHeight AndAlso Y < iHeight + pMenuCommand.Height Then
                    Return IIf(pMenuCommand.Separator, Nothing, pMenuCommand)
                End If
                iHeight += pMenuCommand.Height
            Next
            Return Nothing
        End Function

        Public Function HitTestMouseMove(ByVal X As Integer, ByVal Y As Integer) As Boolean
            Dim pMenuCommand As MenuCommand = HitTest(X, Y)
            If pMenuCommand <> m_pTracMenuItem Then
                m_pTracMenuItem = pMenuCommand
                Return True
            Else
                Return False
            End If
        End Function
        Public Function HitTestMouseDown(ByVal X As Integer, ByVal Y As Integer) As Boolean
            Dim pMenuCommand As MenuCommand = HitTest(X, Y)
            Return pMenuCommand IsNot Nothing
        End Function

        Public Sub Draw(ByVal g As Graphics)
            Dim area As New Rectangle(0, 0, m_pMemBitmap.Width, m_pMemBitmap.Height)

            m_pMemGraphics.Clear(SystemColors.Control)

            ' Draw the background area
            DrawBackground(m_pMemGraphics, area)

            ' Draw the actual menu items
            DrawAllCommands(m_pMemGraphics)

            g.DrawImage(m_pMemBitmap, area, area, GraphicsUnit.Pixel)
        End Sub

        Private Sub DrawBackground(ByVal g As Graphics, ByVal rectWin As Rectangle)
            Dim main As New Rectangle(0, 0, rectWin.Width, rectWin.Height)


            Dim xStart As Integer = 1
            Dim yStart As Integer = 2
            Dim yHeight As Integer = main.Height - yStart - 1

            ' Paint the main area background
            Using backBrush As Brush = New SolidBrush(Color.FromArgb(249, 248, 247))
                g.FillRectangle(backBrush, main)
            End Using

            ' Draw single line border around the main area
            Using mainBorder As New Pen(Color.FromArgb(102, 102, 102))
                g.DrawRectangle(mainBorder, main)
            End Using

            Dim imageRect As New Rectangle(xStart, yStart, m_iImageColumnWidth, yHeight)

            ' Draw the first image column
            Using openBrush As Brush = New LinearGradientBrush(imageRect, Color.FromArgb(248, 247, 246), Color.FromArgb(215, 211, 204), 0.0F)
                g.FillRectangle(openBrush, imageRect)
            End Using

            ' Draw shadow around borders
            Dim rightLeft As Integer = main.Right + 1
            Dim rightTop As Integer = main.Top + 4
            Dim rightBottom As Integer = main.Bottom + 1
            Dim leftLeft As Integer = main.Left + 4
            Dim xExcludeStart As Integer = main.Left
            Dim xExcludeEnd As Integer = main.Left
        End Sub

        Private Sub DrawAllCommands(ByVal g As Graphics)
            Dim iTop As Integer = 2
            For Each pMenuCommand As MenuCommand In m_pMenuCommands
                DrawSingleCommand(g, iTop, pMenuCommand, pMenuCommand = m_pTracMenuItem)
            Next
        End Sub

        Private Sub DrawSingleCommand(ByVal g As Graphics, ByRef iTop As Integer, ByVal pMenuCommand As MenuCommand, ByVal hotCommand As Boolean)
            Dim iHeight As Integer = pMenuCommand.Height
            Dim drawRect As New Rectangle(1, iTop, Width, iHeight)
            iTop += iHeight

            ' Remember some often used values
            Dim textGapLeft As Integer = 4
            Dim imageLeft As Integer = 4

            ' Calculate some common values
            Dim imageColWidth As Integer = 24

            ' Is this item a separator?
            If pMenuCommand.Separator Then
                ' Draw the image column background
                Dim imageCol As New Rectangle(drawRect.Left, drawRect.Top, imageColWidth, drawRect.Height)

                ' Draw the image column
                Using openBrush As Brush = New LinearGradientBrush(imageCol, Color.FromArgb(248, 247, 246), Color.FromArgb(215, 211, 204), 0.0F)
                    g.FillRectangle(openBrush, imageCol)
                End Using

                ' Draw a separator
                Using separatorPen As New Pen(Color.FromArgb(166, 166, 166))
                    ' Draw the separator as a single line
                    g.DrawLine(separatorPen, drawRect.Left + imageColWidth + textGapLeft, drawRect.Top + 2, drawRect.Right - 7, drawRect.Top + 2)
                End Using
            Else
                Dim leftPos As Integer = drawRect.Left + imageColWidth + textGapLeft

                ' Should the command be drawn selected?
                If hotCommand Then
                    Dim selectArea As New Rectangle(drawRect.Left + 1, drawRect.Top, drawRect.Width - 9, drawRect.Height - 1)

                    Using selectBrush As New SolidBrush(Color.FromArgb(182, 189, 210))
                        g.FillRectangle(selectBrush, selectArea)
                    End Using

                    Using selectPen As New Pen(Color.FromArgb(10, 36, 106))
                        g.DrawRectangle(selectPen, selectArea)
                    End Using
                Else
                    Dim imageCol As New Rectangle(drawRect.Left, drawRect.Top, imageColWidth, drawRect.Height)

                    ' Paint the main background color
                    Using backBrush As Brush = New SolidBrush(Color.FromArgb(249, 248, 247))
                        g.FillRectangle(backBrush, New Rectangle(drawRect.Left + 1, drawRect.Top, drawRect.Width - 9, drawRect.Height))
                    End Using

                    Using openBrush As Brush = New LinearGradientBrush(imageCol, Color.FromArgb(248, 247, 246), Color.FromArgb(215, 211, 204), 0.0F)
                        g.FillRectangle(openBrush, imageCol)
                    End Using
                End If

                ' Calculate text drawing rectangle
                Dim strRect As New Rectangle(leftPos, drawRect.Top, Width - imageColWidth - textGapLeft - 5, drawRect.Height)

                ' Left align the text drawing on a single line centered vertically
                ' and process the & character to be shown as an underscore on next character
                Dim format As New StringFormat()
                format.FormatFlags = StringFormatFlags.NoClip Or StringFormatFlags.NoWrap
                format.Alignment = StringAlignment.Near
                format.LineAlignment = StringAlignment.Center
                format.HotkeyPrefix = HotkeyPrefix.Show

                Dim textBrush As New SolidBrush(SystemColors.MenuText)
                g.DrawString(pMenuCommand.Text, SystemInformation.MenuFont, textBrush, strRect, format)

                ' The image offset from top of cell is half the space left after
                ' subtracting the height of the image from the cell height
                Dim imageTop As Integer = drawRect.Top + (drawRect.Height - 16) / 2

                ' Should a check mark be drawn?
                If pMenuCommand.Checked Then
                    Dim boxPen As New Pen(Color.FromArgb(10, 36, 106))
                    Dim boxBrush As Brush

                    If hotCommand Then
                        boxBrush = New SolidBrush(Color.FromArgb(133, 146, 181))
                    Else
                        boxBrush = New SolidBrush(Color.FromArgb(212, 213, 216))
                    End If

                    Dim boxRect As New Rectangle(imageLeft - 1, imageTop - 1, 16 + 2, 16 + 2)

                    ' Fill the checkbox area very slightly
                    g.FillRectangle(boxBrush, boxRect)

                    ' Draw the box around the checkmark area
                    g.DrawRectangle(boxPen, boxRect)

                    boxPen.Dispose()
                    boxBrush.Dispose()

                    Dim pPen As New Pen(Color.Black, 1)
                    g.DrawLine(pPen, New Point(imageLeft + 5, imageTop + 8), New Point(imageLeft + 5 + 2, imageTop + 8 + 2))
                    g.DrawLine(pPen, New Point(imageLeft + 5, imageTop + 9), New Point(imageLeft + 5 + 2, imageTop + 9 + 2))
                    g.DrawLine(pPen, New Point(imageLeft + 5 + 2, imageTop + 8 + 2), New Point(imageLeft + 5 + 2 + 4, imageTop + 8 + 2 - 4))
                    g.DrawLine(pPen, New Point(imageLeft + 5 + 2, imageTop + 9 + 2), New Point(imageLeft + 5 + 2 + 4, imageTop + 9 + 2 - 4))
                End If
            End If
        End Sub

    End Class

End Namespace
