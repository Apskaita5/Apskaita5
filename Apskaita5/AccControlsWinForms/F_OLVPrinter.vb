Imports System.Drawing
Imports BrightIdeasSoftware
Imports System.Drawing.Drawing2D

Public Class F_OLVPrinter

    Private _ListView As ObjectListView


    ''' <summary>
    ''' Gets or sets a report header text.
    ''' </summary>
    ''' <remarks></remarks>
    Public Property HeaderText() As String
        Get
            Return tbHeader.Text
        End Get
        Set(ByVal value As String)
            If value Is Nothing Then value = ""
            tbHeader.Text = "\t" & value
        End Set
    End Property


    Public Sub New(ByVal listView As ObjectListView)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        If listView Is Nothing Then Throw New ArgumentNullException("listView")
        _ListView = listView

    End Sub


    Private Sub F_OLVPrinter_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.listViewPrinter1.ListView = _ListView
        Me.UpdatePrintPreview(Nothing, Nothing)
    End Sub


    Private Sub radioButton1_CheckedChanged(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles radioButton1.CheckedChanged
        Me.printPreviewControl1.Zoom = 2.0
    End Sub

    Private Sub radioButton2_CheckedChanged(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles radioButton2.CheckedChanged
        Me.printPreviewControl1.Zoom = 1.0
    End Sub

    Private Sub radioButton3_CheckedChanged(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles radioButton3.CheckedChanged
        Me.printPreviewControl1.Zoom = 0.5
    End Sub

    Private Sub radioButton4_CheckedChanged(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles radioButton4.CheckedChanged
        Me.printPreviewControl1.Zoom = 1.0
        Me.printPreviewControl1.AutoZoom = True
    End Sub

    Private Sub button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles button1.Click
        Me.listViewPrinter1.PageSetup()
        Me.UpdatePrintPreview(Nothing, Nothing)
    End Sub

    Private Sub button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles button2.Click
        Me.listViewPrinter1.PrintPreview()
    End Sub

    Private Sub button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles button3.Click
        Me.listViewPrinter1.PrintWithDialog()
    End Sub

    Private Sub numericUpDown1_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) _
        Handles numericUpDown1.ValueChanged

        Dim pages As Integer = CInt(numericUpDown1.Value)
        If pages > 0 AndAlso pages < 4 Then
            Me.printPreviewControl1.Rows = 1
            Me.printPreviewControl1.Columns = pages
        Else
            Me.printPreviewControl1.Rows = 2
            Me.printPreviewControl1.Columns = ((pages - 1) / 2) + 1
        End If

    End Sub


    Private Sub UpdatePrintPreview(ByVal sender As Object, ByVal e As EventArgs) _
        Handles cbListHeaderOnEveryPage.CheckedChanged, cbPrintSelection.CheckedChanged, _
        cbShrinkToFit.CheckedChanged, cbUseGridLines.CheckedChanged, rbStyleMinimal.CheckedChanged, _
        rbStyleModern.CheckedChanged, rbStyleOTT.CheckedChanged

        Me.listViewPrinter1.Header = Me.tbHeader.Text.Replace("\\t", "\t")
        Me.listViewPrinter1.Footer = Me.tbFooter.Text.Replace("\\t", "\t")
        Me.listViewPrinter1.Watermark = Me.tbWatermark.Text

        Me.listViewPrinter1.IsShrinkToFit = Me.cbShrinkToFit.Checked
        Me.listViewPrinter1.IsListHeaderOnEachPage = Me.cbListHeaderOnEveryPage.Checked
        Me.listViewPrinter1.IsPrintSelectionOnly = Me.cbPrintSelection.Checked

        If Me.rbStyleMinimal.Checked Then
            ApplyMinimalFormatting()
        ElseIf rbStyleModern.Checked Then
            ApplyModernFormatting()
        ElseIf rbStyleOTT.Checked Then
            ApplyOverTheTopFormatting()
        End If

        If cbUseGridLines.Checked Then
            Me.listViewPrinter1.ListGridPen = Nothing
        End If

        Me.printPreviewControl1.InvalidatePreview()

    End Sub


    Private Sub ApplyMinimalFormatting()

        Me.listViewPrinter1.CellFormat = Nothing
        Me.listViewPrinter1.ListFont = New Font("Tahoma", 9)

        Me.listViewPrinter1.HeaderFormat = BlockFormat.Header()
        Me.listViewPrinter1.HeaderFormat.TextBrush = Brushes.Black
        Me.listViewPrinter1.HeaderFormat.BackgroundBrush = Nothing
        Me.listViewPrinter1.HeaderFormat.SetBorderPen(Sides.Bottom, New Pen(Color.Black, 0.5F))

        Me.listViewPrinter1.FooterFormat = BlockFormat.Footer()
        Me.listViewPrinter1.GroupHeaderFormat = BlockFormat.GroupHeader()
        Me.listViewPrinter1.GroupHeaderFormat.SetBorder(Sides.Bottom, 2, _
            New LinearGradientBrush(New Rectangle(0, 0, 1, 1), Color.Gray, Color.White, LinearGradientMode.Horizontal))

        Me.listViewPrinter1.ListHeaderFormat = BlockFormat.ListHeader()
        Me.listViewPrinter1.ListHeaderFormat.BackgroundBrush = Nothing

        Me.listViewPrinter1.WatermarkFont = Nothing
        Me.listViewPrinter1.WatermarkColor = Color.Empty

    End Sub

    Private Sub ApplyModernFormatting()

        Me.listViewPrinter1.CellFormat = Nothing
        Me.listViewPrinter1.ListFont = New Font("Ms Sans Serif", 9)
        Me.listViewPrinter1.ListGridPen = New Pen(Color.DarkGray, 0.5F)

        Me.listViewPrinter1.HeaderFormat = BlockFormat.Header(New Font("Verdana", 24, FontStyle.Bold))
        Me.listViewPrinter1.HeaderFormat.BackgroundBrush = New LinearGradientBrush( _
            New Rectangle(0, 0, 1, 1), Color.DarkBlue, Color.White, LinearGradientMode.Horizontal)

        Me.listViewPrinter1.FooterFormat = BlockFormat.Footer()
        Me.listViewPrinter1.FooterFormat.BackgroundBrush = New LinearGradientBrush( _
            New Rectangle(0, 0, 1, 1), Color.White, Color.DarkBlue, LinearGradientMode.Horizontal)

        Me.listViewPrinter1.GroupHeaderFormat = BlockFormat.GroupHeader()
        Me.listViewPrinter1.ListHeaderFormat = BlockFormat.ListHeader(New Font("Verdana", 12))

        Me.listViewPrinter1.WatermarkFont = Nothing
        Me.listViewPrinter1.WatermarkColor = Color.Empty

    End Sub

    Private Sub ApplyOverTheTopFormatting()

        Me.listViewPrinter1.CellFormat = Nothing
        Me.listViewPrinter1.ListFont = New Font("Ms Sans Serif", 9)
        Me.listViewPrinter1.ListGridPen = New Pen(Color.Blue, 0.5F)

        Me.listViewPrinter1.HeaderFormat = BlockFormat.Header(New Font("Comic Sans MS", 36))
        Me.listViewPrinter1.HeaderFormat.TextBrush = New LinearGradientBrush( _
            New Rectangle(0, 0, 1, 1), Color.Black, Color.Blue, LinearGradientMode.Horizontal)

        Me.listViewPrinter1.HeaderFormat.BackgroundBrush = New LinearGradientBrush( _
            New Rectangle(0, 0, 1, 1), Color.DarkBlue, Color.White, LinearGradientMode.Horizontal)
        Me.listViewPrinter1.HeaderFormat.SetBorder(Sides.All, 10, _
            New LinearGradientBrush(New Rectangle(0, 0, 1, 1), Color.Purple, _
            Color.Pink, LinearGradientMode.Horizontal))

        Me.listViewPrinter1.FooterFormat = BlockFormat.Footer(New Font("Comic Sans MS", 12))
        Me.listViewPrinter1.FooterFormat.TextBrush = Brushes.Blue
        Me.listViewPrinter1.FooterFormat.BackgroundBrush = New LinearGradientBrush( _
            New Rectangle(0, 0, 1, 1), Color.Gold, Color.Green, LinearGradientMode.Horizontal)
        Me.listViewPrinter1.FooterFormat.SetBorder(Sides.All, 5, New SolidBrush(Color.FromArgb(128, Color.Green)))

        Me.listViewPrinter1.GroupHeaderFormat = BlockFormat.GroupHeader()
        Me.listViewPrinter1.GroupHeaderFormat.SetBorder(Sides.Bottom, 5, _
            New HatchBrush(HatchStyle.LargeConfetti, Color.Blue, Color.Empty))

        Me.listViewPrinter1.ListHeaderFormat = BlockFormat.ListHeader(New Font("Comic Sans MS", 12))
        Me.listViewPrinter1.ListHeaderFormat.BackgroundBrush = Brushes.PowderBlue
        Me.listViewPrinter1.ListHeaderFormat.TextBrush = Brushes.Black

        Me.listViewPrinter1.WatermarkFont = New Font("Comic Sans MS", 72)
        Me.listViewPrinter1.WatermarkColor = Color.Red

    End Sub

End Class