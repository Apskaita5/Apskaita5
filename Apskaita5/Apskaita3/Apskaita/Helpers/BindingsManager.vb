Public Class BindingsManager
    Implements IDisposable

    Private disposedValue As Boolean = False        ' To detect redundant calls
    Private _PrimaryBindingSource As System.Windows.Forms.BindingSource
    Private _SecondaryBindingSource As System.Windows.Forms.BindingSource
    Private _SecondaryBindingSource2 As System.Windows.Forms.BindingSource

    Public Sub New(ByRef PrimaryBindingSource As System.Windows.Forms.BindingSource, _
        ByRef SecondaryBindingSource As System.Windows.Forms.BindingSource, _
        ByRef SecondaryBindingSource2 As System.Windows.Forms.BindingSource, _
        ByVal ChangeEditLevel As Boolean, ByVal CancelEdit As Boolean)

        _PrimaryBindingSource = PrimaryBindingSource
        _SecondaryBindingSource = SecondaryBindingSource
        _SecondaryBindingSource2 = SecondaryBindingSource2

        PrimaryBindingSource.RaiseListChangedEvents = False
        If Not SecondaryBindingSource Is Nothing Then SecondaryBindingSource.RaiseListChangedEvents = False
        If Not SecondaryBindingSource2 Is Nothing Then SecondaryBindingSource2.RaiseListChangedEvents = False

        'If ChangeEditLevel AndAlso CancelEdit Then

        '    If Not SecondaryBindingSource2 Is Nothing Then UnBindBindingSource(SecondaryBindingSource2, False, True)
        '    If Not SecondaryBindingSource Is Nothing Then UnBindBindingSource(SecondaryBindingSource, False, True)
        '    UnBindBindingSource(PrimaryBindingSource, False, True)

        'ElseIf ChangeEditLevel Then

        '    If Not SecondaryBindingSource2 Is Nothing Then UnBindBindingSource(SecondaryBindingSource2, True, False)
        '    If Not SecondaryBindingSource Is Nothing Then UnBindBindingSource(SecondaryBindingSource, True, False)
        '    UnBindBindingSource(PrimaryBindingSource, True, False)

        'End If

        If ChangeEditLevel AndAlso CancelEdit Then

            If Not SecondaryBindingSource2 Is Nothing Then SecondaryBindingSource2.CancelEdit()
            If Not SecondaryBindingSource Is Nothing Then SecondaryBindingSource.CancelEdit()
            PrimaryBindingSource.CancelEdit()

        ElseIf ChangeEditLevel Then

            If Not SecondaryBindingSource2 Is Nothing Then SecondaryBindingSource2.EndEdit()
            If Not SecondaryBindingSource Is Nothing Then SecondaryBindingSource.EndEdit()
            PrimaryBindingSource.EndEdit()

        End If

    End Sub

    Public Sub SetNewDataSource(ByVal Obj As Object)
        _PrimaryBindingSource.DataSource = Nothing
        If Not _SecondaryBindingSource Is Nothing Then _SecondaryBindingSource.DataSource = _PrimaryBindingSource
        If Not _SecondaryBindingSource2 Is Nothing Then _SecondaryBindingSource2.DataSource = _PrimaryBindingSource
        _PrimaryBindingSource.DataSource = Obj
    End Sub

    Public Shared Sub UnBind(ByVal ChangeEditLevel As Boolean, ByVal CancelEdit As Boolean, _
        ByRef BindingSources As System.Windows.Forms.BindingSource())

        If BindingSources Is Nothing OrElse BindingSources.Length < 1 Then Exit Sub

        For i As Integer = 1 To BindingSources.Length
            If Not BindingSources(i - 1) Is Nothing Then _
                BindingSources(i - 1).RaiseListChangedEvents = False
        Next

        If ChangeEditLevel Then

            If CancelEdit Then

                For i As Integer = BindingSources.Length To 1 Step -1
                    If Not BindingSources(i - 1) Is Nothing Then _
                        BindingSources(i - 1).CancelEdit()
                Next

            Else

                For i As Integer = BindingSources.Length To 1 Step -1
                    If Not BindingSources(i - 1) Is Nothing Then _
                        BindingSources(i - 1).EndEdit()
                Next

            End If

        End If

        For i As Integer = 1 To BindingSources.Length
            If Not BindingSources(i - 1) Is Nothing Then
                BindingSources(i - 1).DataSource = Nothing
                Exit Sub
            End If
        Next

    End Sub

    Private Sub UnBindBindingSource(ByRef source As BindingSource, _
        ByVal apply As Boolean, ByVal cancel As Boolean)

        Dim current As System.ComponentModel.IEditableObject = _
            TryCast(source.Current, System.ComponentModel.IEditableObject)

        If Not TypeOf source.DataSource Is BindingSource Then _
            source.DataSource = Nothing

        If current IsNot Nothing Then
            If apply Then
                current.EndEdit()
            ElseIf cancel Then
                current.CancelEdit()
            End If
        End If

    End Sub

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: free unmanaged resources when explicitly called
                _PrimaryBindingSource.RaiseListChangedEvents = True
                If Not _SecondaryBindingSource Is Nothing Then _
                    _SecondaryBindingSource.RaiseListChangedEvents = True
                If Not _SecondaryBindingSource2 Is Nothing Then _
                    _SecondaryBindingSource2.RaiseListChangedEvents = True
                _PrimaryBindingSource.ResetBindings(False)
                If Not _SecondaryBindingSource Is Nothing Then _
                    _SecondaryBindingSource.ResetBindings(False)
                If Not _SecondaryBindingSource2 Is Nothing Then _
                    _SecondaryBindingSource2.ResetBindings(False)
            End If

            ' TODO: free shared unmanaged resources
        End If
        Me.disposedValue = True
    End Sub

#Region " IDisposable Support "
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
