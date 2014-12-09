Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Drawing
Imports System.Data
Imports System.Text
Imports System.Windows.Forms

Namespace DGVColumnSelector

    Partial Public Class UserControlMenu
        Inherits UserControl

        Public Event DoneEvent As EventHandler
        Public Delegate Sub CheckedChanged(ByVal iIndex As Integer, ByVal bChecked As Boolean)
        Public Event CheckedChangedEnent As CheckedChanged
        Public WithEvents timer1 As New Timer

        Public Overridable Sub OnCheckedChanged(ByVal iIndex As Integer, ByVal bChecked As Boolean)
            RaiseEvent CheckedChangedEnent(iIndex, bChecked)
        End Sub

        Public Overridable Sub OnDone()
            RaiseEvent DoneEvent(Me, EventArgs.Empty)
        End Sub

        Private m_pMenuControl As New MenuControl()

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub buttonDone_Click(ByVal sender As Object, ByVal e As EventArgs)
            Parent.Focus()
        End Sub

        Public Sub Initialize(ByVal pDataGridView As DataGridView)
            m_pMenuControl = New MenuControl()

            For Each c As DataGridViewColumn In pDataGridView.Columns
                m_pMenuControl.Add(c.HeaderText, c.Visible)
            Next

            m_pMenuControl.Prepare(CreateGraphics())

            If m_pMenuControl.Height > 349 Then
                Width = m_pMenuControl.Width + 40
            Else
                Width = m_pMenuControl.Width
            End If
            Height = m_pMenuControl.Height

            timer1.Enabled = True
        End Sub

        Private Sub UserControlMenu_Paint(ByVal sender As Object, ByVal e As PaintEventArgs) Handles MyBase.Paint
            m_pMenuControl.Draw(e.Graphics)
        End Sub

        Private Sub UserControlMenu_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseMove
            If m_pMenuControl.HitTestMouseMove(e.X, e.Y) Then
                m_pMenuControl.Draw(CreateGraphics())
            End If
        End Sub

        Private Sub UserControlMenu_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs) Handles MyBase.MouseDown
            If m_pMenuControl.HitTestMouseDown(e.X, e.Y) Then
                If m_pMenuControl.Done Then
                    OnDone()
                Else
                    Dim iHitIndex As Integer = m_pMenuControl.HitIndex
                    If iHitIndex <> -1 Then
                        Dim bChecked As Boolean = m_pMenuControl.ChangeChecked(iHitIndex, CreateGraphics())
                        OnCheckedChanged(iHitIndex, bChecked)
                    End If
                End If
            End If
        End Sub

        Private Sub timer1_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles timer1.Tick
            Dim pPoint As Point = PointToClient(System.Windows.Forms.Cursor.Position)
            If m_pMenuControl.HitTestMouseMove(pPoint.X, pPoint.Y) Then
                m_pMenuControl.Draw(CreateGraphics())
            End If
        End Sub

    End Class

End Namespace