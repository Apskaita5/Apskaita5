Imports System.Windows.Forms

''' <summary>
''' Represents a helper object to switch cursor to the <see cref="Cursors.WaitCursor">Cursors.WaitCursor</see>
''' while some potentialy lengthy operations are executed synchronosly.
''' Should be used with a USING statement.
''' </summary>
''' <remarks></remarks>
Public Class StatusBusy
    Implements IDisposable

    Private _OldCursor As Cursor


    Public Sub New()

        Dim parentForm As Form = Application.OpenForms(0)

        If Not parentForm Is Nothing Then
            _OldCursor = parentForm.Cursor
            parentForm.Cursor = Cursors.WaitCursor
        End If

    End Sub


    ' IDisposable
    Private disposedValue As Boolean = False ' To detect redundant calls
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then

                Dim parentForm As Form = Application.OpenForms(0)

                If Not parentForm Is Nothing Then
                    parentForm.Cursor = _OldCursor
                End If

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