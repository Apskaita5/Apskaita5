Public Class StatusBusy
    Implements IDisposable
    Private mOldStatus As String
    Private mOldCursor As Cursor
    'Private cForm As Form

    Public Sub New()
        'cForm = MDIParent1.ActiveForm
        mOldCursor = MDIParent1.Cursor
        MDIParent1.Cursor = Cursors.WaitCursor
        MDIParent1.StatusLabel.Visible = True

        'splash = New SplashScreen

        'Dim progressBar As ProgressBar = New ProgressBar()
        'progressBar.Name = "ProgressBar"
        'progressBar.Value = 0
        'progressBar.Size = New Size(200, 20)
        'progressBar.Location = New Point(0, 0)
        'progressBar.Style = ProgressBarStyle.Marquee



        'splash.Create()
        'splash.TransparentKey = Color.Fuchsia
        'Dim tmp As New Bitmap(200, 20)
        'Using G As Graphics = Graphics.FromImage(tmp)
        'G.FillRectangle(Brushes.Fuchsia, 0, 0, 200, 20)
        'End Using
        'splash.FormBackgroundImage = tmp
        'splash.CreateControl(ProgressBar)
        'splash.ShowSplashScreen()

        'Threading.Thread.CurrentThread.Sleep(5000)

    End Sub

    'Private Shared Function GetResourceStream(ByVal resource As [String]) As System.IO.Stream
    'Dim ea = System.Reflection.Assembly.GetExecutingAssembly()
    '   For Each curResource As [String] In ea.GetManifestResourceNames()
    '        If curResource.EndsWith(resource) Then
    '            Return ea.GetManifestResourceStream(curResource)
    '        End If
    '    Next
    '    Return Nothing
    'End Function

    ' IDisposable
    Private disposedValue As Boolean = False ' To detect redundant calls
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                MDIParent1.Cursor = mOldCursor
                MDIParent1.StatusLabel.Visible = False
                'cForm.Activate()
                'splash.CloseSplashScreen()
            End If
        End If
        Me.disposedValue = True
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  
        ' Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

End Class